
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

#endregion

namespace TDSMAN.FormWeb
{
    public partial class TrnCorrectionTraces : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnCorrectionTraces()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        TracesConnect objAccount = new TracesConnect();
        TracesData objTraceData = new TracesData();
        DateService dtService = new DateService();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //----

        DMLService dmlService = new DMLService();

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();

        string strRequestNo = "";
        string strFormNo = "";
        string strQuarter = "";
        string strFaYear = "";
        string strFaYearID = "";
        int iCount = 0;

        enum enmRequestType
        {
            ProxyLogin,
            InitializeCaptcha,
            Login,
            Correction,
            CorrectionRequestDetails,
            ValidationDetails,
            TackCorrectionRequest,
            KYCInput,
            AddChallanList,
            ListOfChallans,
            ChallanInput,
            SubmitCorrectionStatement,
            FinalSubmission,
            GetRemainingBal,
            RemoveChallanFromList,
            LogOff
        }

        int intRowIndex = 0;
        int intColumnIndex = 0;

        #endregion

        #region TrnCorrectionTraces_Load
        private void TrnCorrectionTraces_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Request For Correction";
            //
            InitializeCaptcha();

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

            InitializeCaptcha();
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
            if (cmbFAYear.SelectedIndex <= 0) return;
            if (cmbFormNo.SelectedIndex <= 0) return;
            if (cmbQtr.SelectedIndex <= 0) return;
            if (cmbCorrectionCategory.SelectedIndex <= 0) return;
            //------------------------------------------------
            string strStatus = "1";
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.Correction);

            TracesData objData = new TracesData();
            objData.FAYear = cmbFAYear.Text.Substring(0, cmbFAYear.Text.IndexOf("-"));

            if (cmbQtr.SelectedIndex > 0)
            {
                strFaYearID = Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex).ToString();

                switch (cmbQtr.Text)
                {
                    case "Q1":
                        objData.Quarter = "3";
                        break;
                    case "Q2":
                        objData.Quarter = "4";
                        break;
                    case "Q3":
                        objData.Quarter = "5";
                        break;
                    case "Q4":
                        objData.Quarter = "6";
                        break;
                }
            }
            if (cmbCorrectionCategory.SelectedIndex > 0)
            {
                switch (cmbCorrectionCategory.Text)
                {
                    case "Online":
                        strStatus = "1";
                        break;
                    case "Offline":
                        strStatus = "2";
                        break;
                }
            }
            //-------------------------------------------
            objData.Forms = cmbFormNo.Text; ;
            objList.Add(objData);
            objList.Add(strStatus);
            // -------------------------------------------
            pBar.Value = 0;
            pgTimer.Start();
            // -------------------------------------------
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync(objList);
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
                //INITIALIZE CAPTCHA CODE
                case enmRequestType.InitializeCaptcha:
                    objAccount = new TracesConnect();
                    objResponse = objAccount.GetCaptchaFromTraces();

                    objRetval.Add(enmRequestType.InitializeCaptcha);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    break;

                //FAKE LOGIN REQUEST
                case enmRequestType.ProxyLogin:
                    objResponse = objAccount.makeFakeLoginToTRACES();
                    //---------------------------------------------------
                    objRetval.Add(enmRequestType.ProxyLogin);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    //---------------------------------------------------
                    break;

                // LOGIN REQUEST
                case enmRequestType.Login:
                    objResponse = objAccount.makeLoginToTRACES((TracesLogin)objList[1]);
                    //---------------------------------------------------
                    objRetval.Add(enmRequestType.Login);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    //---------------------------------------------------
                    break;
                // LIST OF STATEMENT STATUS FILES
                case enmRequestType.Correction:
                    TracesData objdata = (TracesData)objList[1];
                    TracesResponse response = objAccount.RequestForCorrection(objdata, Convert.ToString(objList[2]));
                    objRetval.Add(enmRequestType.CorrectionRequestDetails);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                case enmRequestType.TackCorrectionRequest:

                    response = objAccount.RequestFoCorrectionRequest();
                    objRetval.Add(enmRequestType.TackCorrectionRequest);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                case enmRequestType.KYCInput:
                    //  string strData = (string)objList[1];
                    response = objAccount.RequestKYCFormRequest(objTraceData, strRequestNo);
                    objRetval.Add(enmRequestType.KYCInput);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                case enmRequestType.ListOfChallans:
                    response = objAccount.RequestFAYearWiseChallanRequest((string)objList[1]);
                    objRetval.Add(enmRequestType.ListOfChallans);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                case enmRequestType.GetRemainingBal:
                    response = objAccount.GetAvailableBalance((string)objList[1]);
                    objRetval.Add(enmRequestType.GetRemainingBal);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                case enmRequestType.AddChallanList:
                    response = objAccount.AddChallanToList((string)objList[1]);
                    objRetval.Add(enmRequestType.AddChallanList);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                case enmRequestType.RemoveChallanFromList:
                    response = objAccount.RemoveChallanFromList((string)objList[1]);
                    objRetval.Add(enmRequestType.RemoveChallanFromList);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                case enmRequestType.SubmitCorrectionStatement:
                    response = objAccount.RequestSubmitCorrection();
                    objRetval.Add(enmRequestType.SubmitCorrectionStatement);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                case enmRequestType.FinalSubmission:
                    response = objAccount.RequestFinalSubmission((ArrayList)objList[1]);
                    objRetval.Add(enmRequestType.FinalSubmission);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;


                //REQUEST FOR LOG OFF
                case enmRequestType.LogOff:
                    objResponse = objAccount.Logoff();
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
                    case enmRequestType.InitializeCaptcha:
                        this.pgTimer.Stop();
                        pBar.Value = 100;
                        btnCaptchaRefresh.Enabled = true;
                        txtCaptchaCode.Text = "";

                        if (objResponse.Respons == enmResponse.Success)
                        {
                            if (objResponse.CustomeTypes != null)
                            {
                                Stream imgStream = (Stream)objResponse.CustomeTypes;
                                Image img = Image.FromStream(imgStream);
                                this.picCaptcha.Image = img;
                            }
                            //-------------------------------------------------------                           
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            objAccount.Dispose();

                            TracesLogin objlog = new TracesLogin();
                            //--------------------------------------------
                            ArrayList objList = new ArrayList();
                            objList.Add(enmRequestType.ProxyLogin);
                            objList.Add(objlog);
                            //-------------------------------------------
                            pgTimer.Start();
                            //-------------------------------------------
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(objList);
                        }

                        break;
                    case enmRequestType.Login:
                        //pgTimer.Stop();
                        //pgTimer.Interval = 1000;
                        //pBar.Value = 99;
                        //---------------------------------------------------
                        if (objResponse.Respons == enmResponse.Success)
                            ShowHideLoginDetails(enmRequestType.Correction);

                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            pgTimer.Stop();
                            pBar.Value = 100;
                            ShowHideLoginDetails(enmRequestType.Login);
                            cmnService.J_UserMessage(objResponse.Message);
                            //InitializeCaptcha();
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

                    case enmRequestType.ProxyLogin:
                        //pgTimer.Stop();
                        //pgTimer.Interval = 1000;
                        //pBar.Value = 99;
                        //---------------------------------------------------
                        //if (objResponse.Respons == enmResponse.Success)
                        //{
                        //    ShowHideLoginDetails(enmRequestType.DownloadList);

                        //    ArrayList objList = new ArrayList();
                        //    objList.Add(enmRequestType.DownloadList);
                        //    //-------------------------------------------
                        //    if (!bgWorker.IsBusy)
                        //        bgWorker.RunWorkerAsync(objList);
                        //}
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            pgTimer.Stop();
                            pBar.Value = 100;
                            // cmnService.J_UserMessage(objResponse.Message);
                            InitializeCaptcha();
                            return;
                        }
                        //---------------------------------------------------
                        pBar.Value = 0;
                        //---------------------------------------------------
                        break;


                    case enmRequestType.Correction:

                        this.pgTimer.Stop();
                        //this.pgTimer.Interval = 1000;
                        pBar.Value = 100;

                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Requested)
                        {
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }
                        //--------------------------------------------------------   

                        break;
                    case enmRequestType.CorrectionRequestDetails:

                        this.pgTimer.Stop();
                        //this.pgTimer.Interval = 1000;
                        pBar.Value = 100;
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Requested)
                        {
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        DataTable dTable = (DataTable)objResponse.CustomeTypes;

                        ShowHideLoginDetails(enmRequestType.CorrectionRequestDetails);
                        PopulateDatagridView(dTable);

                        break;

                    case enmRequestType.TackCorrectionRequest:
                        this.pgTimer.Stop();
                        pBar.Value = 100;
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        ShowHideLoginDetails(enmRequestType.TackCorrectionRequest);
                        dTable = (DataTable)objResponse.CustomeTypes;
                        PopulateDatagridView(dTable);

                        break;

                    case enmRequestType.KYCInput:
                        this.pgTimer.Stop();
                        //this.pgTimer.Interval = 1000;
                        pBar.Value = 100;

                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            ShowHideLoginDetails(enmRequestType.AddChallanList);
                            dTable = (DataTable)objResponse.CustomeTypes;
                            PopulateAddedChallanListGrid(dTable);

                            return;
                        }

                        break;

                    case enmRequestType.ListOfChallans:

                        this.pgTimer.Stop();
                        //this.pgTimer.Interval = 1000;
                        pBar.Value = 100;

                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Success)
                        {
                            ShowHideLoginDetails(enmRequestType.ListOfChallans);
                            dTable = (DataTable)objResponse.CustomeTypes;
                            PopulateChallanGrid(dTable);

                            return;
                        }


                        break;


                    case enmRequestType.GetRemainingBal:

                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Success)
                        {

                            if (Convert.ToString(objResponse.CustomeTypes) != "Nil" && Convert.ToString(objResponse.CustomeTypes) != "0")
                            {
                                lblRemAvlBal.Text = Convert.ToString(objResponse.CustomeTypes);
                            }

                            ShowHideLoginDetails(enmRequestType.ChallanInput);

                            return;
                        }
                        break;


                    case enmRequestType.AddChallanList:

                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Success)
                        {
                            ShowHideLoginDetails(enmRequestType.AddChallanList);
                            dTable = (DataTable)objResponse.CustomeTypes;
                            PopulateAddedChallanListGrid(dTable);
                        }
                        break;

                    case enmRequestType.RemoveChallanFromList:

                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Success)
                        {
                            ShowHideLoginDetails(enmRequestType.AddChallanList);
                            dTable = (DataTable)objResponse.CustomeTypes;
                            PopulateAddedChallanListGrid(dTable);
                        }


                        break;

                    case enmRequestType.FinalSubmission:

                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Success)
                        {
                            cmnService.J_UserMessage(objResponse.Message);

                            // ShowHideLoginDetails(enmRequestType.AddChallanList);
                            // dTable = (DataTable)objResponse.CustomeTypes;
                            //PopulateAddedChallanListGrid(dTable);
                        }

                        break;

                    case enmRequestType.LogOff:
                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtTANNo.Text = "";
                        txtCaptchaCode.Text = "";
                        grpDownloadList.Visible = false;
                        grpLoginDetails.Visible = true;
                        // grpProgress.Visible = true;
                        BtnSave.Enabled = true;
                        BtnSave.BackColor = Color.Lavender;
                        InitializeCaptcha();
                        pBar.Value = 0;
                        break;
                }
                //-------------------------------------------------------
            }
            catch (Exception err)
            {
                this.pgTimer.Stop();
                pBar.Value = 100;
                objAccount.Dispose();
                objAccount = null;
                GC.Collect();
                ShowHideLoginDetails(enmRequestType.Login);
               // cmnService.J_UserMessage(err.Message, MessageBoxIcon.Error);

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
                    lstDeducteeHelp.Height = 15;
                    lstDeducteeHelp.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        //lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) + drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15) + drdShowDeducteeHelp["USER_PASSWORD"]));
                        //-- ANIK @ 2015/04/20
                        lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) +
                                                                  drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15) +
                                                                  drdShowDeducteeHelp["USER_PASSWORD"].ToString().PadRight(10) +
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
            //
            lstDeducteeHelp.Visible = false;
            //--
            txtCaptchaCode.Select();
        }
        #endregion

        #region btnViewAllCorrectionReq_Click       
        private void btnViewAllCorrectionReq_Click(object sender, EventArgs e)
        {
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.TackCorrectionRequest);

            pBar.Value = 0;
            pgTimer.Start();
            // -------------------------------------------
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync(objList);
        }

        #endregion

        #region btnBack_Click


        private void btnBack_Click(object sender, EventArgs e)
        {
            ShowHideLoginDetails(enmRequestType.Correction);
        }
        #endregion

        #region dgvUploadDetails_CellClick
        private void dgvUploadDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //GETTING ROW & COLUMN INDEX OF SELECT ROW
            intRowIndex = e.RowIndex;
            intColumnIndex = e.ColumnIndex;
            //-------------------------------------------------------------------------------
            if (e.ColumnIndex == dgvUploadDetails.Columns["Select"].Index && e.RowIndex >= 0)
            {
                if (Convert.ToString(dgvUploadDetails.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "select")
                {

                    //-------------------------------------------------------------------------------
                    strRequestNo = Convert.ToString(dgvUploadDetails.Rows[e.RowIndex].Cells[2].Value);
                    strFaYear = Convert.ToString(dgvUploadDetails.Rows[e.RowIndex].Cells[3].Value);
                    strQuarter = Convert.ToString(dgvUploadDetails.Rows[e.RowIndex].Cells[4].Value);
                    strFormNo = Convert.ToString(dgvUploadDetails.Rows[e.RowIndex].Cells[5].Value);
                    //-------------------------------------------------------------------------------

                    if (Convert.ToString(dgvUploadDetails.Rows[e.RowIndex].Cells["Status"].Value).ToLower() == "submitted to admin user")
                    {

                        //-------------------------------------------
                        ArrayList objData = new ArrayList();
                        objData.Add(strRequestNo);
                        objData.Add(strFaYear);
                        objData.Add(strQuarter);
                        objData.Add(strFormNo);
                        // -------------------------------------------
                        ArrayList objList = new ArrayList();
                        objList.Add(enmRequestType.FinalSubmission);
                        objList.Add(objData);
                        // -------------------------------------------
                        pBar.Value = 0;
                        pgTimer.Start();
                        // -------------------------------------------
                        if (!bgWorker.IsBusy)
                            bgWorker.RunWorkerAsync(objList);
                        // -------------------------------------------

                        return;
                    }
                    //-------------------------------------------------------------------------------
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
                   
                    txtFAYear.Text = strFaYear;
                    txtQuarter.Text = strQuarter;
                    txtFormNo.Text = strFormNo;

                    ShowHideLoginDetails(enmRequestType.KYCInput);
                    // string s = txtTANNo.Text;

                }
            }
        }

        #endregion

        #region dgvChallanGrid_CellClick
        private void dgvChallanGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //GETTING ROW & COLUMN INDEX OF SELECT ROW
            intRowIndex = e.RowIndex;
            intColumnIndex = e.ColumnIndex;
            //-------------------------------------------------------------------------------
            if (e.ColumnIndex == dgvChallanGrid.Columns["Add Challan"].Index && e.RowIndex >= 0)
            {
                if (Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToUpper() == "ADD CHALLAN")
                {

                    //-------------------------------------------------------------------------------
                    //strRequestNo = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[2].Value);
                    //strFaYear = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[3].Value);
                    //strQuarter = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[4].Value);
                    //strFormNo = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[5].Value);
                    ////-------------------------------------------------------------------------------
                    //txtFAYear.Text = strFaYear;
                    //txtQuarter.Text = strQuarter;
                    //txtFormNo.Text = strFormNo;

                    // ShowHideLoginDetails(enmRequestType.KYCInput);
                    // string s = txtTANNo.Text;

                    lblBSRCode.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[1].Value);
                    lblDateofDeposite.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[2].Value);
                    lblChallanSerial.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[3].Value);
                    txtTDS.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[4].Value);
                    txtSurcharge.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[5].Value);
                    txtEducationCess.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[6].Value);
                    txtIntereset.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[7].Value);
                    txtLevy.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[8].Value);
                    txtOthers.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[9].Value);
                    lblTotalTaxDeposited.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[10].Value);

                    lblTDSDeposited.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[12].Value);

                    txtChequeNo.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[11].Value);
                    lblRemAvlBal.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[13].Value);

                    lblAmountClaimed.Text = "0.00";
                    txtClaimedAmount.Text = "0.00";
                    txtAmountClaimedasOther.Text = "0.00";


                    lblLevy.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[8].Value);
                    lblSecCode.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[15].Value);
                    lblreceiptID.Text = Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[14].Value);


                    ArrayList objList = new ArrayList();
                    objList.Add(enmRequestType.GetRemainingBal);
                    objList.Add(Convert.ToString(dgvChallanGrid.Rows[e.RowIndex].Cells[14].Value));


                    pBar.Value = 0;
                    pgTimer.Start();
                    // -------------------------------------------
                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync(objList);

                }
            }
        }

        #endregion

        #region dgvAddedChlList_CellClick


        private void dgvAddedChlList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cmnService.J_UserMessage("Do you want to remove the challan ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //GETTING ROW & COLUMN INDEX OF SELECT ROW
            intRowIndex = e.RowIndex;
            intColumnIndex = e.ColumnIndex;
            //-------------------------------------------------------------------------------
            if (e.ColumnIndex == dgvAddedChlList.Columns["Remove Challan"].Index && e.RowIndex >= 0)
            {
                if (Convert.ToString(dgvAddedChlList.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToUpper() == "REMOVE CHALLAN")
                {

                    var scchlnId = dgvAddedChlList.Rows[e.RowIndex].Cells["scchlndetlId"].Value;
                    var receiptId = dgvAddedChlList.Rows[e.RowIndex].Cells["id"].Value;
                    var bkFlg = dgvAddedChlList.Rows[e.RowIndex].Cells["bookadj"].Value;

                    var interest = Convert.ToDouble(dgvAddedChlList.Rows[e.RowIndex].Cells["clinterest"].Value);
                    var intlinterest = Convert.ToDouble(dgvAddedChlList.Rows[e.RowIndex].Cells["intlintrst"].Value);
                    var others = Convert.ToDouble(dgvAddedChlList.Rows[e.RowIndex].Cells["clothers"].Value);
                    var intlothers = Convert.ToDouble(dgvAddedChlList.Rows[e.RowIndex].Cells["intlclothers"].Value);
                    var chLevy = Convert.ToDouble(dgvAddedChlList.Rows[e.RowIndex].Cells["lvy"].Value); 
                    var intlLevy = Convert.ToDouble(dgvAddedChlList.Rows[e.RowIndex].Cells["intllvy"].Value);

                    var tottaxdep = Convert.ToDouble(dgvAddedChlList.Rows[e.RowIndex].Cells["clttd"].Value);

                    string strValue = "0";

                    if (string.IsNullOrWhiteSpace(Convert.ToString(dgvAddedChlList.Rows[e.RowIndex].Cells["intlclttd"].Value)) == false)
                        strValue = Convert.ToString(dgvAddedChlList.Rows[e.RowIndex].Cells["intlclttd"].Value);
                    
                    var intltottaxdep = Convert.ToDouble(strValue);


                    var amtDiff = (interest - intlinterest) + (others - intlothers) + (chLevy - intlLevy) + (tottaxdep - intltottaxdep);

                    string strParam = "reqtype=5&scchlnId="+ scchlnId +"&amtDiff="+ amtDiff +"&receiptId="+ receiptId +"&bkFlg="+ bkFlg;
                    // -------------------------------------------
                    ArrayList objList = new ArrayList();
                    objList.Add(enmRequestType.RemoveChallanFromList);
                    objList.Add(strParam);
                    // -------------------------------------------
                    pBar.Value = 0;
                    pgTimer.Start();
                    // -------------------------------------------
                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync(objList);


                }
            }

        }

        #endregion


        #region btnAddChallan_Click


        private void btnAddChallan_Click(object sender, EventArgs e)
        {
            if (cmbFAYearChallan.SelectedIndex <= 0) return;
            //  cmbFAYear.Text

            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.ListOfChallans);
            objList.Add(cmbFAYearChallan.Text);

            pBar.Value = 0;
            pgTimer.Start();
            // -------------------------------------------
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync(objList);

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

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
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



        #region btnShow_Click        
        private void btnShow_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region btnReturnProceed_Click


        private void btnReturnProceed_Click(object sender, EventArgs e)
        {


            //PROVIDING RETURN DETAILS

            objTraceData.FAYear = strFaYear.Substring(0, strFaYear.IndexOf("-"));



            objTraceData.PRN_NO = txtTokenNo.Text.Trim();

            switch (strQuarter)
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

            objTraceData.Forms = strFormNo;

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

            // -----------------------------
            if (!ValidateKYCInputFields()) return;


            //

            #region UPDATE LAST USED USER ID AND PASSWORD



            //if (chkRememberMe.Checked == true)
            // {
            strSQL = "UPDATE TRN_LAST_NSDL " +
                         "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                         "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                         "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' ";

            dmlService.J_ExecSql(strSQL);
            //}

            //NOW ADDING THE USER ID AND PASSWORD IN MASTER 
            //ADDED BY SHREY KEJRIWAL ON 25/10/2012

            //CHEKCING IF TRN_NSDL_DOWNLOAD TABLE IS HAVING THE DATA FOR THIS RETURN

            strSQL = "SELECT COUNT(*) " +
                     "FROM   TRN_NSDL_DOWNLOAD " +
                     "WHERE TAN_NO  = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "' " +
                     "AND   ASST_ID =  " + strFaYearID + " " +
                     "AND   FORM_NO = '" + strFormNo + "' " +
                     "AND   QTR     = '" + strQuarter + "'";

            iCount = Convert.ToInt16(dmlService.J_ExecSqlReturnScalar(strSQL));

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
                         "VALUES( '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "', " +
                         "         " + strFaYearID + ", " +
                         "        '" + strFormNo + "', " +
                         "        '" + strQuarter + "', " +
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
                         "SET TAN_NO               = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "', " +
                         "    ASST_ID              =  " + strFaYearID + ", " +
                         "    FORM_NO              = '" + strFormNo + "', " +
                         "    QTR                  = '" + strQuarter + "', " +
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
                         "WHERE TAN_NO             = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "' " +
                         "AND   ASST_ID            =  " + strFaYearID + " " +
                         "AND   FORM_NO            = '" + strFormNo + "' " +
                         "AND   QTR                = '" + strQuarter + "'";

                dmlService.J_ExecSql(strSQL);

            }
            //--
            strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";
            iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

            if (iCount == 0)
            {
                //insering new record in the tan login master
                strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                         "VALUES( '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                         "        '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
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
                         "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                         "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                         "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                         "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";

                dmlService.J_ExecSql(strSQL);
            }

            #endregion

            //-------------------------------------------------------
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.KYCInput);
            if (bgWorker.IsBusy) return;
            bgWorker.RunWorkerAsync(objList);

            BtnSave.Enabled = false;
            BtnSave.BackColor = Color.LightGray;

            BtnExit.Enabled = false;
            BtnExit.BackColor = Color.LightGray;

            pgTimer.Start();


        }

        #endregion

        #region btnBackgrid_Click       
        private void btnBackgrid_Click(object sender, EventArgs e)
        {
            ShowHideLoginDetails(enmRequestType.TackCorrectionRequest);
        }
        #endregion


        #region btnBackAddedChlList_Click
       
        private void btnBackAddedChlList_Click(object sender, EventArgs e)
        {
            ShowHideLoginDetails(enmRequestType.AddChallanList);
        }
        #endregion


        #region btnAddChallanToChallanList_Click

       
        private void btnAddChallanToChallanList_Click(object sender, EventArgs e)
        {

            //string strparam = "reqtype=1&seccode=194B&chTds=5928&chSurcharge=0&chEducess=0&chInterst=0&chLevy=0&chOthers=0&interst=0&others=0&chequeno=&amtDiff=0&availBal=5928&receiptId=538796831&chlnType=CIN";

            if (string.IsNullOrWhiteSpace(txtTDS.Text))
            {
                cmnService.J_UserMessage("Please enter TDS / TCS Amount");
                txtTDS.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSurcharge.Text))
            {
                cmnService.J_UserMessage("Please enter Surcharge");
                txtSurcharge.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEducationCess.Text))
            {
                cmnService.J_UserMessage("Please enter Education Cess");
                txtEducationCess.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtIntereset.Text))
            {
                cmnService.J_UserMessage("Please enter Intereset");
                txtIntereset.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLevy.Text))
            {
                cmnService.J_UserMessage("Please enter Levy");
                txtLevy.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOthers.Text))
            {
                cmnService.J_UserMessage("Please enter Others");
                txtOthers.Focus();
                return;
            }

            var intlAvlBal = 0.00;
            var totClAmts = 0.00;

            if (lblTDSDeposited.Text.Trim().ToUpper() == "NO")
            {
                var cltottax = lblAmountClaimed.Text;
                var newLevy = txtLevy.Text;
                var clinterest = txtClaimedAmount.Text;
                var clothers = txtOthers.Text;

                if(lblRemAvlBal.Text.Replace("\"", "").Replace('"', ' ').Trim().ToUpper() != "NIL")
                intlAvlBal = Convert.ToDouble(lblRemAvlBal.Text);

                totClAmts = Convert.ToDouble(cltottax) + Convert.ToDouble(clinterest) + Convert.ToDouble(clothers) + Convert.ToDouble(newLevy);

            }

            if (totClAmts > intlAvlBal)
            {
                cmnService.J_UserMessage("Increase in  Amount claimed as 'Interest'(15), Amount claimed as 'Others'(16) and Levy (8) should not be greater than the Remaining Available Balance in challan (13)");
                return;
            }
            else
            {
                var total = Convert.ToDouble(txtTDS.Text) + Convert.ToDouble(txtSurcharge.Text) + Convert.ToDouble(txtEducationCess.Text) + Convert.ToDouble(txtIntereset.Text) + Convert.ToDouble(txtLevy.Text) + Convert.ToDouble(txtOthers.Text);

                if (total == Convert.ToDouble(lblTotalTaxDeposited.Text))
                {
                    var amtDiff = Convert.ToDouble(txtIntereset.Text) + Convert.ToDouble(txtOthers.Text) + (Convert.ToDouble(txtLevy.Text) - Convert.ToDouble(lblLevy.Text));
                    //alert("amtDiff :"+amtDiff);
                    // -------------------------------------------
                    StringBuilder strparam = new StringBuilder();
                    strparam.Append("reqtype=1&seccode=" + lblSecCode.Text);
                    strparam.Append("&chTds=" + txtTDS.Text.Substring(0, txtTDS.Text.IndexOf(".")) + "&chSurcharge=" + txtSurcharge.Text.Substring(0, txtSurcharge.Text.IndexOf(".")) + "&chEducess=" + txtEducationCess.Text.Substring(0, txtEducationCess.Text.IndexOf(".")) + "&chInterst=" + txtIntereset.Text.Substring(0, txtIntereset.Text.IndexOf(".")));
                    strparam.Append("&chLevy=" + txtLevy.Text.Substring(0, txtLevy.Text.IndexOf(".")) + "&chOthers=" + txtOthers.Text.Substring(0, txtOthers.Text.IndexOf(".")) + "&interst=" + txtIntereset.Text.Substring(0, txtIntereset.Text.IndexOf(".")) + "&others=" + txtOthers.Text.Substring(0, txtOthers.Text.IndexOf(".")) + "&chequeno=" + txtChequeNo.Text + "&amtDiff=" + amtDiff + "&availBal=" + intlAvlBal + "&receiptId=" + lblreceiptID.Text + "&chlnType=CIN");

                    // -------------------------------------------
                    ArrayList objList = new ArrayList();
                    objList.Add(enmRequestType.AddChallanList);
                    objList.Add(Convert.ToString(strparam.ToString()));
                    // -------------------------------------------
                    pBar.Value = 0;
                    pgTimer.Start();
                    // -------------------------------------------
                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync(objList);
                    // -------------------------------------------

                }
                else
                {
                    cmnService.J_UserMessage("Sum of TDS/TCS (4), Surcharge (5), Education Cess (6), Interest (7),Levy (8) and Others (9) should be equal to Total tax deposited (10)");
                    return;
                }

            }
        }
        #endregion

        #region btnCancelAddChallan_Click        
        private void btnCancelAddChallan_Click(object sender, EventArgs e)
        {
            ShowHideLoginDetails(enmRequestType.ListOfChallans);
        }

        #endregion
        
        #region lnkLogOff_Click
        private void lnkLogOff_Click(object sender, EventArgs e)
        {
            ShowHideLoginDetails(enmRequestType.LogOff);


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
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    return;
                }
                //----------------------------------------------------
                /*  objAccount = new TracesConnect();
                  Stream imgStream = objAccount.MakeInitialRequest();
                  Image img = Image.FromStream(imgStream);
                  this.picCaptcha.Image = img; */
                //-------------------------------------------------------
                btnCaptchaRefresh.Enabled = false;

                ArrayList objList = new ArrayList();
                objList.Add(enmRequestType.InitializeCaptcha);

                pBar.Value = 0;
                pgTimer.Start();
                // -------------------------------------------
                if (!bgWorker.IsBusy)
                    bgWorker.RunWorkerAsync(objList);

                txtCaptchaCode.Text = "";
            }
            catch (Exception err)
            {




                btnCaptchaRefresh.Enabled = true;

                // objAccount = null;
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
            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                cmnService.J_UserMessage("Please enter User ID");
                txtUserID.Focus();
                return false;
            }
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
            switch (enmStatus)
            {
                case enmRequestType.Login:
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    txtCaptchaCode.Text = "";
                    txtTANNo.Text = "";
                    grpDownloadList.Visible = false;
                    grpLoginDetails.Visible = true;
                    grpFaQtrDetails.Visible = false;
                    grpCorrectionReqDetails.Visible = false;
                    grpReturnDetails.Visible = false;
                    pnlChallanDetails.Visible = false;
                    grpAddChallan.Visible = false;
                    grpChallanList.Visible = false;

                    BtnSave.Enabled = true;
                    InitializeCaptcha();
                    break;
                case enmRequestType.Correction:

                    //dgvUploadDetails.DataSource = null;
                    //dgvUploadDetails.Update();
                    pnlChallanDetails.Visible = false;
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                    grpFaQtrDetails.Visible = true;
                    grpCorrectionReqDetails.Visible = false;
                    //  grpProgress.Visible = false;
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;
                    break;


                case enmRequestType.TackCorrectionRequest:
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                    grpFaQtrDetails.Visible = false;
                    grpCorrectionReqDetails.Visible = true;
                    grpReturnDetails.Visible = false;
                    pnlChallanDetails.Visible = false;
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;
                    break;

                case enmRequestType.CorrectionRequestDetails:
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                    grpFaQtrDetails.Visible = false;
                    grpCorrectionReqDetails.Visible = true;
                    pnlChallanDetails.Visible = false;
                    // grpProgress.Visible = false;
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;
                    break;

                case enmRequestType.KYCInput:
                    grpCorrectionReqDetails.Visible = false;
                    grpReturnDetails.Visible = true;
                    pnlChallanDetails.Visible = false;
                    setAsstID();
                    setKYCControlValue();
                    break;

                case enmRequestType.AddChallanList:
                    // pnlListChallanSelection.Visible = true;
                    grpReturnDetails.Visible = false;
                    grpAddChallan.Visible = true;
                    grpChallanList.Visible = false;
                    btnAddBookEntry.Visible = false;
                    pnlChallanDetails.Visible = false;
                    break;

                case enmRequestType.ListOfChallans:
                    //  pnlListChallanSelection.Visible = false;
                    pnlChallanDetails.Visible = false;
                    grpAddChallan.Visible = false;
                    grpChallanList.Visible = true;

                    break;


                case enmRequestType.ChallanInput:
                    pnlChallanDetails.Visible = true;
                    grpAddChallan.Visible = false;
                    grpChallanList.Visible = false;
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
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      AND    ASST_ID > 2 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFAYear, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------

            //-- QUARTER
            string[] strQtr = { T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQtr);
            //-- FORM NO.
            string[] strFormNo = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);


            //--CORRECTION CATEGORY.
            string[] strCorrCat = { "Online" };
            dmlService.J_PopulateComboBox(strCorrCat, ref cmbCorrectionCategory);


            //--CHALLAN FINNANCIAL YEAR.
            string[] strFaYear = { "2016", "2017", "2018" };
            dmlService.J_PopulateComboBox(strFaYear, ref cmbFAYearChallan);
            cmbFAYearChallan.SelectedIndex = 1;


            //--          

        }
        #endregion

        #region PopulateDatagridView
        void PopulateDatagridView(DataTable dsRecords)
        {
            dgvUploadDetails.DataSource = null;
            dgvUploadDetails.AutoGenerateColumns = false;

            // DATA BIND TO GRIDVIEW CONTROL
            // dgvUploadDetails.Columns.Clear();
            dgvUploadDetails.ColumnCount = 15;


            dgvUploadDetails.DataSource = dsRecords;
            //---------------------------------------
            //CHECKING IF RECORD EXISTS OR NOT

            if (dsRecords.Rows.Count <= 0)
            {
                cmnService.J_UserMessage("No data available for the specified search criteria");
            }

            //if (dgvUploadDetails.ColumnCount <= 0)
            //    dgvUploadDetails.ColumnCount = 15;


            DataGridViewLinkColumn SelectButton = new DataGridViewLinkColumn();
            SelectButton.Name = "Select";
            SelectButton.Text = "Select";
            SelectButton.HeaderText = "";
            SelectButton.UseColumnTextForLinkValue = true;


            if (dgvUploadDetails.Columns["Select"] == null)
            {
                dgvUploadDetails.Columns.Insert(0, SelectButton);
            }

            dgvUploadDetails.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvUploadDetails.Columns[0].ReadOnly = true;
            dgvUploadDetails.Columns[0].Width = 50;

          dgvUploadDetails.Columns[1].Name = "Request Date";
            dgvUploadDetails.Columns[1].HeaderText = "Request Date";
            dgvUploadDetails.Columns[1].DataPropertyName = "Request Date";
            dgvUploadDetails.Columns[1].Width = 80;

            dgvUploadDetails.Columns[2].Name = "Request Number";
            dgvUploadDetails.Columns[2].HeaderText = "Request Number";
            dgvUploadDetails.Columns[2].DataPropertyName = "Request Number";
            dgvUploadDetails.Columns[2].Width = 70;

            dgvUploadDetails.Columns[3].Name = "Finnancial Year";
            dgvUploadDetails.Columns[3].HeaderText = "Finnancial Year";
            dgvUploadDetails.Columns[3].DataPropertyName = "Finnancial Year";
            dgvUploadDetails.Columns[3].Width = 90;

            dgvUploadDetails.Columns[4].Name = "Quarter";
            dgvUploadDetails.Columns[4].HeaderText = "Quarter";
            dgvUploadDetails.Columns[4].DataPropertyName = "Quarter";
            dgvUploadDetails.Columns[4].Width = 70;

            dgvUploadDetails.Columns[5].Name = "Form Type";
            dgvUploadDetails.Columns[5].HeaderText = "Form Type";
            dgvUploadDetails.Columns[5].DataPropertyName = "Form Type";
            dgvUploadDetails.Columns[5].Width = 60;

            dgvUploadDetails.Columns[6].Name = "Latest Token Number";
            dgvUploadDetails.Columns[6].HeaderText = "Latest Token Number";
            dgvUploadDetails.Columns[6].DataPropertyName = "Latest Token Number";
            dgvUploadDetails.Columns[6].Width = 100;

            dgvUploadDetails.Columns[7].Name = "Status";
            dgvUploadDetails.Columns[7].HeaderText = "Status";
            dgvUploadDetails.Columns[7].DataPropertyName = "Status";

            dgvUploadDetails.Columns[8].Name = "Correction Category";
            dgvUploadDetails.Columns[8].HeaderText = "Correction Category";
            dgvUploadDetails.Columns[8].DataPropertyName = "Correction Category";

            dgvUploadDetails.Columns[9].Name = "Remarks";
            dgvUploadDetails.Columns[9].HeaderText = "Remarks";
            dgvUploadDetails.Columns[9].DataPropertyName = "Remarks";

            dgvUploadDetails.Columns[10].Name = "New Token Number";
            dgvUploadDetails.Columns[10].HeaderText = "New Token Number";
            dgvUploadDetails.Columns[10].DataPropertyName = "New Token Number";


            dgvUploadDetails.Columns[11].Name = "Assigned To";
            dgvUploadDetails.Columns[11].HeaderText = "Assigned To";
            dgvUploadDetails.Columns[11].DataPropertyName = "Assigned To";

            dgvUploadDetails.Columns[12].Name = "Processed Date";
            dgvUploadDetails.Columns[12].HeaderText = "Processed Date";
            dgvUploadDetails.Columns[12].DataPropertyName = "Processed Date";


            dgvUploadDetails.Columns[13].Name = "Download";
            dgvUploadDetails.Columns[13].HeaderText = "Download";
            dgvUploadDetails.Columns[13].DataPropertyName = "Download";

            dgvUploadDetails.Columns[14].Name = "Upload";
            dgvUploadDetails.Columns[14].HeaderText = "Upload";
            dgvUploadDetails.Columns[14].DataPropertyName = "Upload";

            // dgvUploadDetails.Columns[15].Visible = false;



            //---------------------------------------

            // CREATE DOWNLOAD BUTTON & PROGRESSBAR CONTROL COLUMNS 

            /*  DataGridViewLinkColumn btn = new DataGridViewLinkColumn();
              dgvUploadDetails.Columns.Add(btn);
              btn.HeaderText = "";
              //btn.Text = "View Details";
              btn.Text = "Select";
              btn.Name = "lnkDetails";
              btn.Width = 150;
              //------------------------------------------------------------
              btn.UseColumnTextForLinkValue = true;*/
            //------------------------------------------------------------

            foreach (DataGridViewRow gridRow in dgvUploadDetails.Rows)
            {
                DataGridViewCell BookButtonCell = gridRow.Cells["Status"];
                DataGridViewCell BookButtonCell2 = gridRow.Cells["Correction Category"];
                // DataGridViewLinkCell EndDateCell = (DataGridViewLinkCell)gridRow.Cells["lnkDetails"];


                if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "CANCELLED" ||
                    Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "PROCESSED" ||
                    Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "SUBMITTED TO ITD")
                {

                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    gridRow.Cells[0] = new DataGridViewTextBoxCell();

                }
                if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "AVAILABLE")
                {
                    if (Convert.ToString(BookButtonCell2.Value).Trim().ToUpper() == "OFFLINE")
                    {
                        DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                        gridRow.Cells["Select"] = new DataGridViewTextBoxCell();
                    }
                }
            }
            dgvUploadDetails.Refresh();
            dgvUploadDetails.Update();
        }









        #endregion

        #region PopulateAddedChallanListGrid
        void PopulateAddedChallanListGrid(DataTable dsRecords)
        {
            dgvAddedChlList.ReadOnly = true;
            dgvAddedChlList.DataSource = null;
            dgvAddedChlList.AutoGenerateColumns = false;

            // DATA BIND TO GRIDVIEW CONTROL
            dgvAddedChlList.Columns.Clear();
            dgvAddedChlList.ColumnCount = 30;
            dgvAddedChlList.DataSource = dsRecords;
            //---------------------------------------
            //CHECKING IF RECORD EXISTS OR NOT

            if (dsRecords.Rows.Count <= 0)
            {
                cmnService.J_UserMessage("No challans in the statement with available balance");
            }

            //if (dgvAddedChlList.ColumnCount <= 0)
            //    dgvAddedChlList.ColumnCount = 15;


            DataGridViewLinkColumn SelectButton = new DataGridViewLinkColumn();
            SelectButton.Name = "Remove Challan";
            SelectButton.Text = "Remove Challan";
            SelectButton.HeaderText = "";
            SelectButton.UseColumnTextForLinkValue = true;


            if (dgvAddedChlList.Columns["Select"] == null)
            {
                dgvAddedChlList.Columns.Insert(0, SelectButton);
            }

            dgvAddedChlList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvAddedChlList.Columns[0].ReadOnly = true;

            dgvAddedChlList.Columns[1].Name = "bsr";
            dgvAddedChlList.Columns[1].HeaderText = "BSR Code/ Receipt Number";
            dgvAddedChlList.Columns[1].DataPropertyName = "bsr";

            dgvAddedChlList.Columns[2].Name = "dtofdep";
            dgvAddedChlList.Columns[2].HeaderText = "Date on which tax deposited";
            dgvAddedChlList.Columns[2].DataPropertyName = "dtofdep";

            dgvAddedChlList.Columns[3].Name = "chlnno";
            dgvAddedChlList.Columns[3].HeaderText = "Challan Serial No./ DDO Serial No. ";
            dgvAddedChlList.Columns[3].DataPropertyName = "chlnno";

            dgvAddedChlList.Columns[4].Name = "tdsinc";
            dgvAddedChlList.Columns[4].HeaderText = "TDS/ TCS(Rs.)";
            dgvAddedChlList.Columns[4].DataPropertyName = "tdsinc";
            dgvAddedChlList.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;


            dgvAddedChlList.Columns[5].Name = "surchrge";
            dgvAddedChlList.Columns[5].HeaderText = "Surcharge (Rs.)";
            dgvAddedChlList.Columns[5].DataPropertyName = "surchrge";
            dgvAddedChlList.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvAddedChlList.Columns[6].Name = "educess";
            dgvAddedChlList.Columns[6].HeaderText = "Education Cess(Rs.)";
            dgvAddedChlList.Columns[6].DataPropertyName = "educess";
            dgvAddedChlList.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvAddedChlList.Columns[7].Name = "intrst";
            dgvAddedChlList.Columns[7].HeaderText = "Interest (Rs.)";
            dgvAddedChlList.Columns[7].DataPropertyName = "intrst";
            dgvAddedChlList.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvAddedChlList.Columns[8].Name = "lvy";
            dgvAddedChlList.Columns[8].HeaderText = "Levy (Rs.)";
            dgvAddedChlList.Columns[8].DataPropertyName = "lvy";
            dgvAddedChlList.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvAddedChlList.Columns[9].Name = "othrs";
            dgvAddedChlList.Columns[9].HeaderText = "Others";
            dgvAddedChlList.Columns[9].DataPropertyName = "othrs";
            dgvAddedChlList.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvAddedChlList.Columns[10].Name = "totamt";
            dgvAddedChlList.Columns[10].HeaderText = "Total Tax Deposited (Rs.)";
            dgvAddedChlList.Columns[10].DataPropertyName = "totamt";
            dgvAddedChlList.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvAddedChlList.Columns[11].Name = "ddno";
            dgvAddedChlList.Columns[11].HeaderText = "Cheque No./ DD No.";
            dgvAddedChlList.Columns[11].DataPropertyName = "ddno";

            dgvAddedChlList.Columns[12].Name = "bookadj";
            dgvAddedChlList.Columns[12].HeaderText = "Whether TDS/ TCS Deposited by Book Adjustment?(Yes/ No)";
            dgvAddedChlList.Columns[12].DataPropertyName = "bookadj";

            dgvAddedChlList.Columns[13].Name = "clttd";
            dgvAddedChlList.Columns[13].HeaderText = "Amount Claimed as Total Tax Deposited(Rs.)";
            dgvAddedChlList.Columns[13].DataPropertyName = "clttd";
            dgvAddedChlList.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[13].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;


            dgvAddedChlList.Columns[14].Name = "clinterest";
            dgvAddedChlList.Columns[14].HeaderText = "Amount Claimed as Interest (Rs.)";
            dgvAddedChlList.Columns[14].DataPropertyName = "clinterest";
            dgvAddedChlList.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[14].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvAddedChlList.Columns[15].Name = "clothers";
            dgvAddedChlList.Columns[15].HeaderText = "Amount Claimed as Others (Rs.)";
            dgvAddedChlList.Columns[15].DataPropertyName = "clothers";
            dgvAddedChlList.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[15].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvAddedChlList.Columns[16].Name = "balance";
            dgvAddedChlList.Columns[16].HeaderText = "Available Balance in Challan(Rs.)";
            dgvAddedChlList.Columns[16].DataPropertyName = "balance";
            dgvAddedChlList.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[16].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvAddedChlList.Columns[17].Name = "balanceavlbl";
            dgvAddedChlList.Columns[17].HeaderText = "Remaining Available Balance in Challan(Rs.)";
            dgvAddedChlList.Columns[17].DataPropertyName = "balanceavlbl";
            dgvAddedChlList.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAddedChlList.Columns[17].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;



            dgvAddedChlList.Columns[18].Name = "match";
            dgvAddedChlList.Columns[18].HeaderText = "match";
            dgvAddedChlList.Columns[18].DataPropertyName = "match";
            dgvAddedChlList.Columns[18].Visible = false;
            dgvAddedChlList.Columns[18].Width = 0;

            dgvAddedChlList.Columns[19].Name = "chlndetlId";
            dgvAddedChlList.Columns[19].HeaderText = "chlndetlId";
            dgvAddedChlList.Columns[19].DataPropertyName = "chlndetlId";
            dgvAddedChlList.Columns[19].Visible = false;
            dgvAddedChlList.Columns[19].Width = 0;


            dgvAddedChlList.Columns[20].Name = "scchlndetlId";
            dgvAddedChlList.Columns[20].HeaderText = "scchlndetlId";
            dgvAddedChlList.Columns[20].DataPropertyName = "scchlndetlId";
            dgvAddedChlList.Columns[20].Visible = false;
            dgvAddedChlList.Columns[20].Width = 0;

            dgvAddedChlList.Columns[21].Name = "id";
            dgvAddedChlList.Columns[21].HeaderText = "id";
            dgvAddedChlList.Columns[21].DataPropertyName = "id";
            dgvAddedChlList.Columns[21].Visible = false;
            dgvAddedChlList.Columns[21].Width = 0;

            dgvAddedChlList.Columns[22].Name = "seccode";
            dgvAddedChlList.Columns[22].HeaderText = "seccode";
            dgvAddedChlList.Columns[22].DataPropertyName = "seccode";
            dgvAddedChlList.Columns[22].Visible = false;
            dgvAddedChlList.Columns[22].Width = 0;

            dgvAddedChlList.Columns[23].Name = "intlsurchrge";
            dgvAddedChlList.Columns[23].HeaderText = "intlsurchrge";
            dgvAddedChlList.Columns[23].DataPropertyName = "intlsurchrge";
            dgvAddedChlList.Columns[23].Visible = false;
            dgvAddedChlList.Columns[23].Width = 0;

            dgvAddedChlList.Columns[24].Name = "intleducess";
            dgvAddedChlList.Columns[24].HeaderText = "intleducess";
            dgvAddedChlList.Columns[24].DataPropertyName = "intleducess";
            dgvAddedChlList.Columns[24].Visible = false;
            dgvAddedChlList.Columns[24].Width = 0;

            dgvAddedChlList.Columns[25].Name = "intlintrst";
            dgvAddedChlList.Columns[25].HeaderText = "intlintrst";
            dgvAddedChlList.Columns[25].DataPropertyName = "intlintrst";
            dgvAddedChlList.Columns[25].Visible = false;
            dgvAddedChlList.Columns[25].Width = 0;

            dgvAddedChlList.Columns[26].Name = "intllvy";
            dgvAddedChlList.Columns[26].HeaderText = "intllvy";
            dgvAddedChlList.Columns[26].DataPropertyName = "intllvy";
            dgvAddedChlList.Columns[26].Visible = false;
            dgvAddedChlList.Columns[26].Width = 0;

            dgvAddedChlList.Columns[27].Name = "intlothrs";
            dgvAddedChlList.Columns[27].HeaderText = "intlothrs";
            dgvAddedChlList.Columns[27].DataPropertyName = "intlothrs";
            dgvAddedChlList.Columns[27].Visible = false;
            dgvAddedChlList.Columns[27].Width = 0;

            dgvAddedChlList.Columns[28].Name = "intlclinterest";
            dgvAddedChlList.Columns[28].HeaderText = "intlclinterest";
            dgvAddedChlList.Columns[28].DataPropertyName = "intlclinterest";
            dgvAddedChlList.Columns[28].Visible = false;
            dgvAddedChlList.Columns[28].Width = 0;

            dgvAddedChlList.Columns[29].Name = "intlclothers";
            dgvAddedChlList.Columns[29].HeaderText = "intlclothers";
            dgvAddedChlList.Columns[29].DataPropertyName = "intlclothers";
            dgvAddedChlList.Columns[29].Visible = false;
            dgvAddedChlList.Columns[29].Width = 0;

            dgvAddedChlList.Columns[30].Name = "intlclttd";
            dgvAddedChlList.Columns[30].HeaderText = "intlclttd";
            dgvAddedChlList.Columns[30].DataPropertyName = "intlclttd";
            dgvAddedChlList.Columns[30].Visible = false;
            dgvAddedChlList.Columns[30].Width = 0;

            //------------------------------------------------------------

            /* foreach (DataGridViewRow gridRow in dgvAddedChlList.Rows)
             {
                 DataGridViewCell BookButtonCell = gridRow.Cells["Status"];
                 DataGridViewCell BookButtonCell2 = gridRow.Cells["Correction Category"];
                 // DataGridViewLinkCell EndDateCell = (DataGridViewLinkCell)gridRow.Cells["lnkDetails"];


                 if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "CANCELLED" ||
                     Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "PROCESSED")
                 {

                     DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                     gridRow.Cells[0] = new DataGridViewTextBoxCell();

                 }
                 if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "AVAILABLE")
                 {
                     if (Convert.ToString(BookButtonCell2.Value).Trim().ToUpper() == "OFFLINE")
                     {
                         DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                         gridRow.Cells["Select"] = new DataGridViewTextBoxCell();
                     }
                 }
             }

             dgvAddedChlList.Refresh();
             dgvAddedChlList.Update();*/


        }

        #endregion

        #region PopulateChallanGrid
        void PopulateChallanGrid(DataTable dsRecords)
        {
            dgvChallanGrid.ReadOnly = true;
            dgvChallanGrid.DataSource = null;
            dgvChallanGrid.Columns.Clear();

            dgvChallanGrid.AutoGenerateColumns = false;

            // DATA BIND TO GRIDVIEW CONTROL
            // dgvChallanGrid.Columns.Clear();

            dgvChallanGrid.ColumnCount = 16;

            dgvChallanGrid.DataSource = dsRecords;
            //---------------------------------------
            //CHECKING IF RECORD EXISTS OR NOT

            if (dsRecords.Rows.Count <= 0)
            {
                cmnService.J_UserMessage("No Oltas Challan(s) available for selected financial year. Please select different financial year from the drop down");
            }

            //if (dgvChallanGrid.ColumnCount <= 0)
            //    dgvChallanGrid.ColumnCount = 15;


            DataGridViewLinkColumn SelectButton = new DataGridViewLinkColumn();
            SelectButton.Name = "Add Challan";
            SelectButton.Text = "Add Challan";
            SelectButton.HeaderText = "";
            SelectButton.UseColumnTextForLinkValue = true;


            if (dgvChallanGrid.Columns["Select"] == null)
            {
                dgvChallanGrid.Columns.Insert(0, SelectButton);
            }

            dgvChallanGrid.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvChallanGrid.Columns[0].ReadOnly = true;

            dgvChallanGrid.Columns[1].Name = "ReceiptNumber";
            dgvChallanGrid.Columns[1].HeaderText = "BSR Code/ Receipt Number";
            dgvChallanGrid.Columns[1].DataPropertyName = "bsr";

            dgvChallanGrid.Columns[2].Name = "Dateonwhichtaxdeposited";
            dgvChallanGrid.Columns[2].HeaderText = "Date on which tax deposited";
            dgvChallanGrid.Columns[2].DataPropertyName = "dtofdep";

            dgvChallanGrid.Columns[3].Name = "ChallanSerialNo";
            dgvChallanGrid.Columns[3].HeaderText = "Challan Serial No/ DDO Serial No";
            dgvChallanGrid.Columns[3].DataPropertyName = "chlnno";


            dgvChallanGrid.Columns[4].Name = "TDS/TCS";
            dgvChallanGrid.Columns[4].HeaderText = "TDS/ TCS(Rs.)";
            dgvChallanGrid.Columns[4].DataPropertyName = "tdsinc";
            dgvChallanGrid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChallanGrid.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvChallanGrid.Columns[5].Name = "Surcharge";
            dgvChallanGrid.Columns[5].HeaderText = "Surcharge (Rs.)";
            dgvChallanGrid.Columns[5].DataPropertyName = "surchrge";
            dgvChallanGrid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChallanGrid.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;


            dgvChallanGrid.Columns[6].Name = "EducationCess";
            dgvChallanGrid.Columns[6].HeaderText = "Education Cess (Rs.)";
            dgvChallanGrid.Columns[6].DataPropertyName = "educess";
            dgvChallanGrid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChallanGrid.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvChallanGrid.Columns[7].Name = "Interest";
            dgvChallanGrid.Columns[7].HeaderText = "Interest (Rs.)";
            dgvChallanGrid.Columns[7].DataPropertyName = "intrst";
            dgvChallanGrid.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChallanGrid.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvChallanGrid.Columns[8].Name = "Levy";
            dgvChallanGrid.Columns[8].HeaderText = "Levy (Rs.)";
            dgvChallanGrid.Columns[8].DataPropertyName = "lvy";
            dgvChallanGrid.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChallanGrid.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvChallanGrid.Columns[9].Name = "Others";
            dgvChallanGrid.Columns[9].HeaderText = "Others (Rs.)";
            dgvChallanGrid.Columns[9].DataPropertyName = "othrs";
            dgvChallanGrid.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChallanGrid.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvChallanGrid.Columns[10].Name = "TotalTaxDeposited";
            dgvChallanGrid.Columns[10].HeaderText = "Total Tax Deposited (Rs.)";
            dgvChallanGrid.Columns[10].DataPropertyName = "totamt";
            dgvChallanGrid.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChallanGrid.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvChallanGrid.Columns[11].Name = "ChequeNo/DD No";
            dgvChallanGrid.Columns[11].HeaderText = "Cheque No/ DD No.";
            dgvChallanGrid.Columns[11].DataPropertyName = "xx";

            dgvChallanGrid.Columns[12].Name = "WhetherTDS/TCSDepositedbyBookAdjustment";
            dgvChallanGrid.Columns[12].HeaderText = "Whether TDS/ TCS Deposited by Book Adjustment";
            dgvChallanGrid.Columns[12].DataPropertyName = "bookadj";


            dgvChallanGrid.Columns[13].Name = "AvailableBalanceinchallan";
            dgvChallanGrid.Columns[13].HeaderText = "Available Balance in challan (Rs.)";
            dgvChallanGrid.Columns[13].DataPropertyName = "balance";
            dgvChallanGrid.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChallanGrid.Columns[13].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvChallanGrid.Columns[14].Name = "id";
            dgvChallanGrid.Columns[14].HeaderText = "id";
            dgvChallanGrid.Columns[14].DataPropertyName = "id";
            dgvChallanGrid.Columns[14].Width = 0;
            dgvChallanGrid.Columns[14].Visible = false;

            dgvChallanGrid.Columns[15].Name = "seccode";
            dgvChallanGrid.Columns[15].HeaderText = "seccode";
            dgvChallanGrid.Columns[15].DataPropertyName = "seccode";
            dgvChallanGrid.Columns[15].Width = 0;
            dgvChallanGrid.Columns[15].Visible = false;

            //------------------------------------------------------------

            /* foreach (DataGridViewRow gridRow in dgvChallanGrid.Rows)
             {
                 DataGridViewCell BookButtonCell = gridRow.Cells["Status"];
                 DataGridViewCell BookButtonCell2 = gridRow.Cells["Correction Category"];
                 // DataGridViewLinkCell EndDateCell = (DataGridViewLinkCell)gridRow.Cells["lnkDetails"];


                 if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "CANCELLED" ||
                     Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "PROCESSED")
                 {

                     DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                     gridRow.Cells[0] = new DataGridViewTextBoxCell();

                 }
                 if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "AVAILABLE")
                 {
                     if (Convert.ToString(BookButtonCell2.Value).Trim().ToUpper() == "OFFLINE")
                     {
                         DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                         gridRow.Cells["Select"] = new DataGridViewTextBoxCell();
                     }
                 }
             }*/

            dgvChallanGrid.Refresh();
            dgvChallanGrid.Update();


        }









        #endregion



        #region setAsstID              
        private void setAsstID()
        {

            strSQL = @"SELECT ASST_ID                              
                      FROM   MST_ASSESSMENT
                      WHERE  FA_YEAR ='" + strFaYear + "'";

            strFaYearID = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

        }
        #endregion


        #region setKYCControlValue

        private void setKYCControlValue()
        {

            #region TRN_NSDL_DOWNLOAD

            strSQL = "SELECT COUNT(*) " +
                     "FROM   TRN_NSDL_DOWNLOAD " +
                     "WHERE  TAN_NO  ='" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "' " +
                     "AND    ASST_ID = " + strFaYearID + " " +
                     "AND    FORM_NO ='" + strFormNo + "' " +
                     "AND    QTR     ='" + strQuarter + "'";

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
                         "WHERE TAN_NO  = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "' " +
                         "AND   ASST_ID =  " + strFaYearID + " " +
                         "AND   FORM_NO = '" + strFormNo + "' " +
                         "AND   QTR     = '" + strQuarter + "'";

                IDataReader reader = dmlService.J_ExecSqlReturnReader(strSQL);

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
                // blnDataEnteredbyUser = false;
            }
            #endregion
            else
            {
                #region CHECK FROM CORRECTION
                //FIRST CHECKING THE RECORD TO BE PRESENT IN THE CORRECTION RETURN TABLE

                strSQL = "SELECT TOP 1 COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "FROM  COR_HDR_BATCH, " +
                         "      COR_HDR_COMPANY " +
                         "WHERE COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "AND   COR_HDR_BATCH.ASST_ID           = " + strFaYearID + " " +
                         "AND   COR_HDR_BATCH.FORM_NO           = '" + strFormNo + "' " +
                         "AND   COR_HDR_BATCH.QTR               = '" + strQuarter + "' " +
                         "AND   COR_HDR_COMPANY.TAN_NO          = '" + txtTANNo.Text + "' " +
                         "ORDER BY COR_HDR_BATCH.BATCH_HEADER_ID DESC";

                iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                if (iCount > 0)
                {
                    //DATA NOT FOUND IN CORRECTION RETURN DATA
                    //  ClearChallanDeducteeDetails();

                    //DATA IS AVAILABLE IN CORRECTION RETURN

                    //SELECTING THE PROVISIONAL RECEIPT NO

                    strSQL = "SELECT ORIGINAL_RRR_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + iCount;
                    txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                    //-- GETTING CHALLAN NO OF MAX DEDUCTEES
                    strSQL = "SELECT TOP 1 COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                           "FROM  COR_HDR_CHALLAN, " +
                           "      COR_HDR_DEDUCTEE_DETAILS " +
                           "WHERE COR_HDR_CHALLAN.HDR_CHALLAN_ID = COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID " +
                           "AND   COR_HDR_CHALLAN.BATCH_HEADER_ID = " + iCount + " " +
                           "GROUP BY COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                           "ORDER BY COUNT(COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID) DESC";
                    txtChallanID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                    //--
                    //NOW SELECTING THE CHALLAN RECORD FROM THE REGULAR RETURN TABLE

                    string[,] strTVoucherChallanNo = {{"BOOK_ENTRY =  0" , "F", "CHALLAN_NO", "F"},
                                                                  {"BOOK_ENTRY =  2" , "F", "CHALLAN_NO", "F"},
                                                                  {"BOOK_ENTRY =  1" , "F", "TRANSFER_VOUCHER_NO", "F"}};

                    strSQL = "SELECT TOP 1 " + cmnService.J_SQLDBFormat(strTVoucherChallanNo, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CHALLAN_NO, " +
                             "             SL_NO, " + //-- 2014/12/06
                             "             BSR_CODE, " +
                             "             DEPOSIT_DATE, " +
                             "             TOT_TAX," +
                             "             BOOK_ENTRY " +
                             "FROM   COR_HDR_CHALLAN " +
                             "WHERE  BATCH_HEADER_ID = " + iCount + " " +
                             "AND    HDR_CHALLAN_ID  = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                    IDataReader reader = dmlService.J_ExecSqlReturnReader(strSQL);

                    if (reader == null)
                        return;

                    while (reader.Read())
                    {
                        txtSlNo.Text = reader["SL_NO"].ToString(); //-- 2014/12/06
                        txtChallanNo.Text = reader["CHALLAN_NO"].ToString();
                        txtBSRCode.Text = reader["BSR_CODE"].ToString();
                        mskChallanDate.Text = reader["DEPOSIT_DATE"].ToString();
                        txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOT_TAX"]));
                        //-- for BOOK ENTRY
                        if (cmnService.J_ReturnInt32Value(Convert.ToString(reader["BOOK_ENTRY"])) > 0)
                        {
                            chkBookAdjustment.Checked = true;
                            //-- 2013/08/30
                            //txtChallanNo.Text = "";
                            //txtBSRCode.Text = "";
                            //txtChallanNo.ReadOnly = true;
                            //txtBSRCode.ReadOnly = true;
                        }
                        else
                        {
                            chkBookAdjustment.Checked = false;
                            //txtChallanNo.ReadOnly = false;
                            //txtBSRCode.ReadOnly = false;
                        }
                    }

                    reader.Close();
                    reader.Dispose();

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

                    reader = dmlService.J_ExecSqlReturnReader(strSQL);

                    int intInvalidPAN = 0;

                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i == 0)
                        {
                            txtDeducteePAN1.Text = reader["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(reader["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                        else if (i == 1)
                        {
                            txtDeducteePAN2.Text = reader["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(reader["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                        else if (i == 2)
                        {
                            txtDeducteePAN3.Text = reader["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(reader["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                    }

                    reader.Close();
                    reader.Dispose();
                    //--
                    if (intInvalidPAN == 3)
                        chkNoValidPAN.Checked = true;
                    //--
                    //   blnDataEnteredbyUser = false;

                    this.Cursor = Cursors.Default;

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
                             "AND    TRN_BASIC_INFO.FORM_NO = '" + strFormNo + "' " +
                             "AND    TRN_BASIC_INFO.QTR     = '" + strQuarter + "' " +
                             "AND    TRN_BASIC_INFO.ASST_ID = " + strFaYearID + " " +
                             "AND    MST_COMPANY.TAN_NO = '" + txtTANNo.Text + "' ";

                    iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                    if (iCount > 0)
                    {
                        //SELECTING THE PROVISIONAL RECEIPT NO

                        strSQL = "SELECT PRN_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + iCount;

                        txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        strSQL = "SELECT TOP 1 TRN_CHALLAN.CHALLAN_ID " +
                                 "FROM  TRN_CHALLAN LEFT JOIN TRN_DEDUCTEE_DETAILS " +
                                 "ON    TRN_CHALLAN.CHALLAN_ID    = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                                 "WHERE TRN_CHALLAN.BASIC_INFO_ID = " + iCount + " " +
                                 "GROUP BY TRN_CHALLAN.CHALLAN_ID " +
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
                        IDataReader reader = dmlService.J_ExecSqlReturnReader(strSQL);
                        //
                        if (reader == null)
                            return;
                        //
                        while (reader.Read())
                        {
                            txtSlNo.Text = reader["SL_NO"].ToString(); //-- 2014/12/06
                            txtChallanNo.Text = reader["CHALLAN_NO"].ToString();
                            txtBSRCode.Text = reader["BSR_CODE"].ToString();
                            mskChallanDate.Text = reader["DEPOSIT_DATE"].ToString();
                            txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOT_TAX"]));
                            //-- for BOOK ENTRY
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(reader["BOOK_ENTRY"])) > 0)
                                chkBookAdjustment.Checked = true;
                            else
                                chkBookAdjustment.Checked = false;
                        }
                        //
                        reader.Close();
                        reader.Dispose();
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

                        strSQL = "SELECT DISTINCT TOP 3  " + strDeducteeTableName + "." + strDeducteePANName + " AS DEDUCTEE_PAN, " +
                                 "       TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT AS TOTAL_AMOUNT " +
                                 "FROM   TRN_DEDUCTEE_DETAILS, " +
                                 "       " + strDeducteeTableName + " " +
                                 "WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strDeducteeTableName + "." + strDeducteeIdName + " " +
                                 "AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + iCount + " " +
                                "AND   TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                        reader = dmlService.J_ExecSqlReturnReader(strSQL);

                        int intInvalidPAN = 0;

                        for (int i = 0; reader.Read(); i++)
                        {
                            if (i == 0)
                            {
                                txtDeducteePAN1.Text = reader["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 1)
                            {
                                txtDeducteePAN2.Text = reader["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 2)
                            {
                                txtDeducteePAN3.Text = reader["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                        }

                        reader.Close();
                        reader.Dispose();
                        //--
                        if (intInvalidPAN == 3)
                            chkNoValidPAN.Checked = true;
                        //--
                        //blnDataEnteredbyUser = false;

                        this.Cursor = Cursors.Default;
                    }
                    #endregion
                    else
                    {
                        #region DATA DOES NOT EXIST
                        //DATA DOES NOT EXIST IN BOTH CORRECTION AS WELL AS REGULAR
                        //   ClearChallanDeducteeDetails();
                        #endregion
                    }
                }
            }




        }



        #endregion

        #region ValidateFields
        public bool ValidateKYCInputFields()
        {

            //-----------------------------
            string strVal = "";
            Dictionary<string, string> objNameval = null;


            //RETRIEVE PARAMETER
            if (objTraceData.IsNoChallan || objTraceData.IsPaymentByBookAdjustment || objTraceData.panAmtValueCheck)
            {
                TracesResponse response = null;

                //if (strFormName == T_NSDL_FORM_TYPE.Form16)
                //    response = objConnect.IsChallanExistsInForm16(objTraceData, objLogin);
                //else if (strFormName == T_NSDL_FORM_TYPE.Form16A)
                //    response = objConnect.IsChallanExistsInForm16A(objTraceData, objLogin);
                //else if (strFormName == T_NSDL_FORM_TYPE.ConsolidatedStatement)
                //    response = objConnect.IsChallanExistsInConsolidate(objTraceData, objLogin);
                //else if (strFormName == T_NSDL_FORM_TYPE.Defaults)
                //    response = objConnect.IsChallanExistsInDefaults(objTraceData, objLogin);
                //---------------------------------------------------
                //TracesResponse response = objConnect.IsChallanExistsInConsolidate(objTraceData, objLogin);
                if (response.Respons == enmResponse.Failed)
                {
                    cmnService.J_UserMessage(response.Message);
                    InitializeCaptcha();
                    txtCaptchaCode.Focus();
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
                    //if (string.IsNullOrEmpty(objTraceData.ChallanSerialNo))
                    //{
                    //    cmnService.J_UserMessage("Challan Serial Number / DDO Serial Number is mandatory");
                    //    txtChallanNo.Focus();
                    //    return false;
                    //}

                    if (string.IsNullOrEmpty(objTraceData.PRN_NO))
                    {
                        cmnService.J_UserMessage("Enter Provisional Receipt No");
                        txtTokenNo.Focus();
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

                    objTraceData.PAN1 = "";
                    objTraceData.PAN1Amount = "";
                    objTraceData.PAN2 = "";
                    objTraceData.PAN2Amount = "";
                    objTraceData.PAN3 = "";
                    objTraceData.PAN3Amount = "";

                    //objTraceData.IsNoChallan = false;
                }
            }


            return true;
        }

















        #endregion

        private void btnSubmitCorrectionStatement_Click(object sender, EventArgs e)
        {
            if(dgvAddedChlList.Rows.Count <= 0)
            {
                cmnService.J_UserMessage("Please add Challan in the List");
                return;
            }

            // -------------------------------------------
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.SubmitCorrectionStatement);
          //  objList.Add(Convert.ToString(strparam.ToString()));
            // -------------------------------------------
            pBar.Value = 0;
            pgTimer.Start();
            // -------------------------------------------
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync(objList);
            // -------------------------------------------


        }
    }

}

