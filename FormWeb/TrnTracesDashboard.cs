
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
using System.Linq;
using System.Globalization;
using System.Diagnostics;
using System.Text.RegularExpressions;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormTrn;
using TDSMAN.FormSys;


using static TDSMAN.Classes.TracesConnect_OneLogin;


#endregion

namespace TDSMAN.FormWeb
{
    public partial class TrnTracesDashboard : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnTracesDashboard()
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
        //private ToolTip gridToolTip;

        ToolTip gridToolTip = new ToolTip();
        private int lastRow = -1;
        private int lastCol = -1;

        enum enmRequestType
        {
            Login,
            //ListofFiliing,
            //AddStatementDetail,
            //CommuncationDetailGrid1,
            //CommuncationDetailGrid2,
            AfterLogin,
            ViewStatementStatus,
            ViewDefaultPayment,
            ViewOutstandingDemand,
            ViewAlerts,
            ViewUnconsumedChallan,
            ViewTracesActivities,
            ViewDownloadRequest,
            ViewInbox,
            LogOff
        }

        int intRowIndex = 0;
        int intColumnIndex = 0;

        // Key = "Q2-2025-26|26Q"
        private Dictionary<string, Dictionary<string, decimal>> _defaultsStore
            = new Dictionary<string, Dictionary<string, decimal>>();

        private bool IsDefaultParameter(string param)
        {
            return param.StartsWith("Short Payment")
                || param.StartsWith("Short Deduction")
                || param.Contains("Interest")
                || param.Contains("Late Filing Fee");
        }

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
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            //lblTitle.Text = "Request For Submit Declaration of non-filling";
            lblTitle.Text = "TRACES - Dashboard";
            //
            //InitializeCaptcha();
            picCaptcha.Image = Properties.Resources.captcha_loading;
            if (!bgWorkerLoadCaptcha.IsBusy)
                bgWorkerLoadCaptcha.RunWorkerAsync();
            //--
            ClearControls();
            //--
            grpLoginDetails.Height = 164;
            //--
            //gridToolTip = new ToolTip
            //{
            //    AutoPopDelay = 5000,
            //    InitialDelay = 300,
            //    ReshowDelay = 100,
            //    ShowAlways = true
            //};
            gridToolTip.ShowAlways = true;
            gridToolTip.AutoPopDelay = 8000;
            gridToolTip.InitialDelay = 200;
            gridToolTip.ReshowDelay = 100;
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
            TracesConnect_OneLogin.Logoff();
            this.Close();
        }

        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
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

                case enmRequestType.ViewOutstandingDemand:
                    string amount;
                    //objResponse = TracesConnect_OneLogin.GetOutstandingDemand_OneLogin(out amount);
                    objResponse = TracesConnect_OneLogin.GetOutstandingDemand_OneLogin_New(out amount);
                    //
                    objRetval.Add(enmRequestType.ViewOutstandingDemand);
                    objRetval.Add(objResponse);
                    objRetval.Add(amount);
                    //
                    e.Result = objRetval;
                    break;

                case enmRequestType.ViewTracesActivities:
                    {
                        List<string> list;
                        //objResponse = TracesConnect_OneLogin.GetYourTRACESActivities_OneLogin(out list);
                        objResponse = TracesConnect_OneLogin.GetYourTRACESActivities_OneLogin_New(out list);

                        objRetval.Add(enmRequestType.ViewTracesActivities);
                        objRetval.Add(objResponse);
                        objRetval.Add(list);
                        e.Result = objRetval;
                        break;
                    }

                case enmRequestType.ViewAlerts:
                    {
                        List<string> list;
                        //objResponse = TracesConnect_OneLogin.GetAlerts_OneLogin(out list);
                        objResponse = TracesConnect_OneLogin.GetAlerts_OneLogin_New(out list);

                        objRetval.Add(enmRequestType.ViewAlerts);
                        objRetval.Add(objResponse);
                        objRetval.Add(list);
                        e.Result = objRetval;
                        break;
                    }


                case enmRequestType.ViewStatementStatus:
                    {
                        TracesResponse objTracesResponse;
                        List<TracesTableRow> tableRows;

                        //objTracesResponse =TracesConnect_OneLogin.GetStatementStatus_FirstTab_OneLogin(out tableRows);
                        //objTracesResponse =TracesConnect_OneLogin.GetStatementStatus_OneLogin(out tableRows);
                        objTracesResponse =TracesConnect_OneLogin.GetStatementStatus_OneLogin_New(out tableRows);

                        objRetval.Add(enmRequestType.ViewStatementStatus);
                        objRetval.Add(objTracesResponse);
                        objRetval.Add(tableRows);

                        e.Result = objRetval;
                        break;
                    }

                case enmRequestType.ViewInbox:
                    {
                        int inboxCount;
                        //objResponse = TracesConnect_OneLogin.GetInbox_OneLogin(out inboxCount);
                        objResponse = TracesConnect_OneLogin.GetInbox_OneLogin_New(out inboxCount);
                        //
                        objRetval.Add(enmRequestType.ViewInbox);
                        objRetval.Add(objResponse);
                        objRetval.Add(inboxCount);
                        //
                        e.Result = objRetval;
                        break;
                    }

                case enmRequestType.ViewDownloadRequest:
                    {
                        int DownloadRequest;
                        //objResponse = TracesConnect_OneLogin.GetDownloadRequests_OneLogin(out DownloadRequest);
                        objResponse = TracesConnect_OneLogin.GetDownloadRequests_OneLogin_New(out DownloadRequest);
                        //
                        objRetval.Add(enmRequestType.ViewDownloadRequest);
                        objRetval.Add(objResponse);
                        objRetval.Add(DownloadRequest);
                        //
                        e.Result = objRetval;
                        break;
                    }
                case enmRequestType.AfterLogin:
                    DataTable table;
                    ////TracesResponse response = objAccount.RequestForCommunicationInboxAction(out table);
                    ////objRetval.Add(enmRequestType.CommuncationDetailGrid1);
                    //objRetval.Add(response);
                    //objRetval.Add(table);
                    //e.Result = objRetval;

                    break;

                // LIST OF STATEMENT STATUS FILES
                //case enmRequestType.CommuncationDetailGrid1:
                //    DataTable table;
                //    TracesResponse response = objAccount.RequestForCommunicationInboxAction(out table);
                //    objRetval.Add(enmRequestType.CommuncationDetailGrid1);
                //    objRetval.Add(response);
                //    objRetval.Add(table);
                //    e.Result = objRetval;

                //    break;
                //case enmRequestType.AddStatementDetail:

                //    NonFilling objdata = (NonFilling)objList[1];
                //    response = objAccount.RequestForAddStatementDetails(objdata, out table);
                //    objRetval.Add(enmRequestType.AddStatementDetail);
                //    objRetval.Add(response);
                //    objRetval.Add(table);
                //    e.Result = objRetval;


                //    break;

                //REQUEST FOR LOG OFF
                case enmRequestType.LogOff:
                    {
                        //objResponse = objAccount.Logoff();
                        objResponse = TracesConnect_OneLogin.Logoff();
                        objRetval.Add(enmRequestType.LogOff);
                        objRetval.Add(objResponse);
                        e.Result = objRetval;
                        break;
                    }
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
                            ArrayList objList = new ArrayList();
                            //objList.Add(enmRequestType.ListofFiliing); 
                            //objList.Add(enmRequestType.CommuncationDetailGrid1); 
                            //objList.Add(enmRequestType.AfterLogin);
                            //ShowHideLoginDetails(enmRequestType.AfterLogin);
                            objList.Add(enmRequestType.ViewOutstandingDemand);
                            ShowHideLoginDetails(enmRequestType.ViewOutstandingDemand);
                            //-------------------------------------------
                            pgTimer.Start();
                            //-------------------------------------------
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(objList);
                            //-- IT WILL STAY...
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            pgTimer.Stop();
                            pBar.Value = 100;
                            cmnService.J_UserMessage(objResponse.Message);
                            pgTimer.Stop();
                            //pgTimer.Interval = 1000;
                            pBar.Value = 0;
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
                    case enmRequestType.ViewOutstandingDemand:                        
                        string amount = objMessage[2].ToString();

                        if (objResponse.Respons == enmResponse.Success)
                        {
                            //cmnService.J_UserMessage("Outstanding Demand: Rs. " + amount);
                            //lblOutstandingDemand.Text = "";
                            lblOutstandingDemand.Text = lblOutstandingDemand.Text + "\n" + amount + " (across all years)";
                        }
                        else
                        {
                            lblOutstandingDemand.Text = "";
                            //cmnService.J_UserMessage("Failed: " + objResponse.Message);
                            //lblOutstandingDemand.Text = "Some error occured";
                        }
                        ArrayList obJList = new ArrayList();
                        obJList.Add(enmRequestType.ViewTracesActivities);
                        //-------------------------------------------
                        pgTimer.Start();
                        //-------------------------------------------
                        if (!bgWorker.IsBusy)
                            bgWorker.RunWorkerAsync(obJList);

                        break;

                    case enmRequestType.ViewTracesActivities:
                        {
                            List<string> list = (List<string>)objMessage[2];

                            if (objResponse.Respons == enmResponse.Success)
                            {
                                //string msg = "Your TRACES Activities:\n\n" +
                                //             string.Join("\n", list);
                                //cmnService.J_UserMessage(msg);
                                lblTRACESActivities.Text = string.Join("\r\n", list);
                            }
                            else
                            {
                                lblTRACESActivities.Text = "Failed to load TRACES Activities";
                            }
                            ArrayList obJListAlerts = new ArrayList();
                            obJListAlerts.Add(enmRequestType.ViewAlerts);
                            //-------------------------------------------
                            pgTimer.Start();
                            //-------------------------------------------
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(obJListAlerts);

                            break;
                        }


                    case enmRequestType.ViewAlerts:
                        {
                            List<string> list = (List<string>)objMessage[2];

                            if (objResponse.Respons == enmResponse.Success)
                            {
                                //string msg = "Your TRACES Activities:\n\n" +
                                //             string.Join("\n", list);
                                //cmnService.J_UserMessage(msg);
                                lblAlerts.Text = string.Join("\r\n\n", list);
                            }
                            else
                            {
                                lblAlerts.Text = "Failed to load Alerts";
                            }
                            ArrayList obJListAlerts = new ArrayList();
                            obJListAlerts.Add(enmRequestType.ViewStatementStatus);
                            //-------------------------------------------
                            pgTimer.Start();
                            //-------------------------------------------
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(obJListAlerts);

                            break;
                        }

                    case enmRequestType.ViewStatementStatus:
                        {
                            TracesResponse resp = (TracesResponse)objMessage[1];
                            List<TracesTableRow> data =
                                (List<TracesTableRow>)objMessage[2];

                            if (resp.Respons != enmResponse.Success)
                            {
                                //MessageBox.Show(resp.Message);
                                cmnService.J_UserMessage(resp.Message);
                                return;
                            }
                            //
                            SetupStatementStatusGridUI();
                            BindStatementStatusGrid(data);
                            //
                            ArrayList obJListAlerts = new ArrayList();
                            obJListAlerts.Add(enmRequestType.ViewInbox);
                            //-------------------------------------------
                            pgTimer.Start();
                            //-------------------------------------------
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(obJListAlerts);

                            break;
                        }

                    case enmRequestType.ViewInbox:
                        int inboxCount = Convert.ToInt32(objMessage[2].ToString());

                        if (objResponse.Respons == enmResponse.Success)
                        {
                            lnkInbox.Text = "Inbox (" + inboxCount.ToString() + " new message(s))";
                        }
                        else
                        {
                            lnkInbox.Text = "";
                        }
                        ArrayList obJListInbox = new ArrayList();
                        obJListInbox.Add(enmRequestType.ViewDownloadRequest);
                        //-------------------------------------------
                        pgTimer.Start();
                        //-------------------------------------------
                        if (!bgWorker.IsBusy)
                            bgWorker.RunWorkerAsync(obJListInbox);
                        //--
                        break;

                    case enmRequestType.ViewDownloadRequest:
                        int ViewDownloadRequest = Convert.ToInt32(objMessage[2].ToString());

                        if (objResponse.Respons == enmResponse.Success)
                        {
                            lnkDownloadRequests.Text = "Download Requests (" + ViewDownloadRequest.ToString() + ")";
                        }
                        else
                        {
                            lnkDownloadRequests.Text = "";
                        }
                        //ArrayList obJListViewDownloadRequest = new ArrayList();
                        //obJListViewDownloadRequest.Add(enmRequestType.ViewDownloadRequest);
                        ////-------------------------------------------
                        //pgTimer.Start();
                        ////-------------------------------------------
                        //if (!bgWorker.IsBusy)
                        //    bgWorker.RunWorkerAsync(obJListViewDownloadRequest);
                        pgTimer.Stop();
                        pBar.Value = 100;
                        ////--
                        break;

                    case enmRequestType.LogOff:
                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtTANNo.Text = "";
                        txtCaptchaCode.Text = "";
                        grpDownloadList.Visible = false;
                        //grpListStatement.Visible = false;
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
            ShowHideLoginDetails(enmRequestType.LogOff);
            ClearControls();
            BtnExit.Visible = true;
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

        #region btnAddStatement_Click

        private void btnAddStatement_Click(object sender, EventArgs e)
        {
            //ShowHideLoginDetails(enmRequestType.AddStatementDetail);
        }
        #endregion


        #region InitializeCaptcha
        //private void InitializeCaptcha()
        //{
        //    try
        //    {
        //        //--
        //        if (TdsMan.T_CheckInternetConnectivty() == false)
        //        {
        //            picCaptcha.Image = Properties.Resources.captcha_loading_failed;
        //            cmnService.J_UserMessage("Internet Connectivity not found");
        //            return;
        //        }
        //        //----------------------------------------------------
        //        objAccount = new TracesConnect();
        //        Stream imgStream = objAccount.MakeInitialRequest();
        //        Image img = Image.FromStream(imgStream);
        //        this.picCaptcha.Image = img;
        //        //-------------------------------------------------------
        //        txtCaptchaCode.Text = "";
        //    }
        //    catch (Exception err)
        //    {
        //        picCaptcha.Image = Properties.Resources.captcha_loading_failed;
        //        cmnService.J_UserMessage(err.Message);
        //    }
        //}
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

                //Stream imgStream = TracesConnect_OneLogin.MakeInitialRequest_OneLogin();
                //Image img = Image.FromStream(imgStream);
                //picCaptcha.Image = img;
                var captchaData = TracesConnect_OneLogin.GetCaptcha_New();

                Image img = Image.FromStream(captchaData.imageStream);
                picCaptcha.Image = img;
                this.CurrentCaptchaId = captchaData.captchaId;

                //string captchaId = captchaData.captchaId;
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

                case enmRequestType.ViewOutstandingDemand: //AfterLogin:
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    txtCaptchaCode.Text = "";
                    txtTANNo.Text = "";
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                    grpAlerts.Visible = true;
                    grpTRACESActivities.Visible = true;
                    grpStatementStatus.Visible = true;
                    // grpProgress.Visible = true;
                    //InitializeCaptcha();
                    //InitializeCaptcha_OneLogin();
                    break;


                case enmRequestType.ViewStatementStatus: //AfterLogin:
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    txtCaptchaCode.Text = "";
                    txtTANNo.Text = "";
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                    //grpAlert.Visible = true;
                    grpStatementStatus.Visible = true;
                    //InitStatementStatusGrid();
                    // grpProgress.Visible = true;
                    //InitializeCaptcha();
                    //InitializeCaptcha_OneLogin();
                    break;
                //case enmRequestType.ListofFiliing:
                //    //grpListStatement.Visible = true;
                //    grpDownloadList.Visible = true;
                //    grpLoginDetails.Visible = false;
                //    //grpInputDetails.Visible = false;
                //    BtnSave.Enabled = false;
                //    BtnSave.BackColor = Color.LightGray;

                //    //ArrayList objList = new ArrayList();
                //    //objList.Add(enmRequestType.ListofFiliing);
                //    ////-------------------------------------------
                //    //pgTimer.Start();
                //    ////-------------------------------------------
                //    //if (!bgWorker.IsBusy)
                //    //    bgWorker.RunWorkerAsync(objList);


                //    break;

                //case enmRequestType.AddStatementDetail:

                //    grpDownloadList.Visible = true;
                //    //grpInputDetails.Visible = true;
                //    //grpListStatement.Visible = false;
                //    grpLoginDetails.Visible = false;

                //    // grpProgress.Visible = false;
                //    BtnSave.Enabled = false;
                //    BtnSave.BackColor = Color.LightGray;
                //    break;

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
            ////-----------
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
            //// -----------------------------
            //// CHECK IF RECORDS EXIST
            //// -----------------------------
            //if (dt == null || dt.Rows.Count == 0)
            //{
            //    ShowHideLoginDetails(enmRequestType.AddStatementDetail);
            //    dgvStatementList.DataSource = null;
            //    return;
            //}
            //else
            //{
            //    ShowHideLoginDetails(enmRequestType.ListofFiliing);
            //}

            //// -----------------------------
            //// BIND DATA
            //// -----------------------------
            //dgvStatementList.Columns.Clear();
            //dgvStatementList.DataSource = dt;

            //// Apply TRACES-style formatting
            //FormatMainGrid();

            //// Select first row
            //if (dt.Rows.Count > 0)
            //{
            //    dgvStatementList.ClearSelection();
            //    dgvStatementList.Rows[0].Selected = true;
            //}
        }


        void HideInternalColumns()
        {
            //string[] hiddenCols = { "commId", "hidfinYr", "hidquat", "declId", "certNum", "commInbId", "comcatid" };

            //foreach (string colName in hiddenCols)
            //{
            //    if (dgvStatementList.Columns.Contains(colName))
            //        dgvStatementList.Columns[colName].Visible = false;
            //}
        }


        void AdjustColumnWidths()
        {
            //foreach (DataGridViewColumn col in dgvStatementList.Columns)
            //{
            //    if (!col.Visible) continue;

            //    string h = col.HeaderText.ToLower();

            //    if (h == "description")
            //        col.FillWeight = 250;
            //    else if (h == "reference no")
            //        col.FillWeight = 90;
            //    else if (h == "date")
            //        col.FillWeight = 70;
            //    else
            //        col.FillWeight = 110;

            //    col.SortMode = DataGridViewColumnSortMode.NotSortable;
            //    col.ReadOnly = true;
            //}
        }

        private void FormatMainGrid()
        {
            //if (dgvStatementList.DataSource == null) return;

            //// Base settings
            //dgvStatementList.ReadOnly = true;
            //dgvStatementList.RowHeadersVisible = false;
            //dgvStatementList.AllowUserToResizeColumns = false;
            //dgvStatementList.AllowUserToResizeRows = false;
            //dgvStatementList.MultiSelect = false;
            //dgvStatementList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //dgvStatementList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgvStatementList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //dgvStatementList.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //// -------------------------------
            //// HEADER STYLE (TRACES LOOK)
            //// -------------------------------
            //DataGridViewCellStyle header = new DataGridViewCellStyle();
            //header.BackColor = Color.FromArgb(224, 236, 255);     // Light blue header (TRACES)
            //header.ForeColor = Color.Black;
            //header.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            //header.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgvStatementList.EnableHeadersVisualStyles = false;
            //dgvStatementList.ColumnHeadersDefaultCellStyle = header;

            //// -------------------------------
            //// ALTERNATE ROW SHADING
            //// -------------------------------
            //dgvStatementList.RowsDefaultCellStyle.BackColor = Color.White;
            //dgvStatementList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            //dgvStatementList.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            //// -------------------------------
            //// HIDE unwanted internal columns
            //// -------------------------------
            //HideInternalColumns();

            //// -------------------------------
            //// COLUMN WIDTH RULES
            //// -------------------------------
            //foreach (DataGridViewColumn col in dgvStatementList.Columns)
            //{
            //    if (!col.Visible) continue;

            //    string h = col.HeaderText.ToLower();

            //    if (h.Contains("reference no"))
            //        col.FillWeight = 90;
            //    else if (h.Contains("category"))
            //        col.FillWeight = 90;
            //    else if (h == "description")
            //        col.FillWeight = 240;        // TRACES wide description
            //    else if (h == "financial year")
            //        col.FillWeight = 70;
            //    else if (h == "quarter")
            //        col.FillWeight = 55;
            //    else if (h == "form type")
            //        col.FillWeight = 60;
            //    else if (h == "date")
            //        col.FillWeight = 70;
            //    else
            //        col.FillWeight = 100;

            //    col.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            ////
            //dgvSubGrid.ClearSelection();
            //dgvSubGrid.CurrentCell = null;
        }




        #endregion
        

        private void btnBack_Click(object sender, EventArgs e)
        {

            //grpInputDetails.Visible = false;
            //grpListStatement.Visible = true;
        }

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0092", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region bgWorkerLoadCaptcha_DoWork
        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            //InitializeCaptcha();
            InitializeCaptcha_OneLogin();
        }
        #endregion

        #region BtnDownloadCertificate_Click
        private void BtnDownloadCertificate_Click(object sender, EventArgs e)
        {
            //if (dgvStatementList.SelectedRows.Count == 0)
            //{
            //    //cmnService.J_UserMessage("Please select a row first.");
            //    cmnService.J_UserMessage("Please select a row first.");
            //    return;
            //}

            //DataGridViewRow row = dgvStatementList.SelectedRows[0];

            //// Get visible values
            //string refNo = row.Cells["Reference No"].Value.ToString();
            //string finYear = row.Cells["Financial Year"].Value.ToString();
            //string formType = row.Cells["Form Type"].Value.ToString();
            //string quarter = row.Cells["Quarter"].Value?.ToString() ?? "NA";
            //string category = row.Cells["Category"].Value.ToString();
            //string description = row.Cells["Description"].Value.ToString();

            //// Only allow certificate downloads
            //if (!category.Contains("Certificate"))
            //{
            //    //cmnService.J_UserMessage("This row does not contain a certificate.");
            //    cmnService.J_UserMessage("This row does not contain a certificate.");
            //    return;
            //}

            //// Try to get hidden commId field first
            //string commId = "";
            //string certNum = "";

            //// Check if we have hidden columns
            //try
            //{
            //    if (dgvStatementList.Columns.Contains("commId"))
            //        commId = row.Cells["commId"].Value?.ToString();
            //    else if (dgvStatementList.Columns.Contains("commInbId"))
            //        commId = row.Cells["commInbId"].Value?.ToString();

            //    if (dgvStatementList.Columns.Contains("certNum"))
            //        certNum = row.Cells["certNum"].Value?.ToString();
            //    else if (dgvStatementList.Columns.Contains("declId"))
            //        certNum = row.Cells["declId"].Value?.ToString();
            //}
            //catch { }

            //// If no hidden commId, derive it from Reference Number
            //// Based on the URL pattern: 730301 -> 73030
            //if (string.IsNullOrEmpty(commId))
            //{
            //    if (refNo.Length >= 5)
            //    {
            //        // Try removing last digit
            //        commId = refNo.Substring(0, refNo.Length - 1);
            //    }
            //    else
            //    {
            //        commId = refNo;
            //    }
            //}

            //// If no certNum found, try using part of reference number
            //if (string.IsNullOrEmpty(certNum))
            //{
            //    certNum = refNo; // or some other derivation
            //}
            ////
            //if (cmnService.J_UserMessage("Proceed with download?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    return;

            //byte[] pdfBytes;
            //TracesResponse resp = objAccount.DownloadCertificate(
            //    refNo,
            //    finYear,
            //    formType,
            //    quarter,
            //    certNum,
            //    description,
            //    out pdfBytes
            //);

            //if (resp.Respons != enmResponse.Success)
            //{
            //    //cmnService.J_UserMessage("Download failed:\n\n" + resp.Message, "Error",
            //    //                MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cmnService.J_UserMessage("Download failed:\n\n" + resp.Message, MessageBoxIcon.Error);
            //    return;
            //}
            //// ----------------------------------------------
            //// ASK USER WHERE TO SAVE THE FILE
            //// ----------------------------------------------
            ////SaveFileDialog dlg = new SaveFileDialog();
            ////dlg.Title = "Save Certificate PDF";
            ////dlg.Filter = "PDF Files (*.pdf)|*.pdf";
            ////dlg.FileName = "Certificate_" + refNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

            ////if (dlg.ShowDialog() != DialogResult.OK)
            ////    return;

            ////string userPath = dlg.FileName;

            //string userPath = cmnService.J_OpenFolderDialog("Select Destination Folder");
            //string FileName = "Certificate_" + refNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            //userPath = Path.Combine(userPath, FileName);

            ////string savePath = @"C:\Certificate_" + refNo + "_" +
            ////                  DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            //File.WriteAllBytes(userPath, pdfBytes);
            ////####
            //string subPath = Path.Combine(Application.StartupPath, "Certificate"); // Your code goes here
            //if (!Directory.Exists(subPath))
            //{
            //    Directory.CreateDirectory(subPath);
            //}
            //File.Copy(userPath, Path.Combine(subPath, FileName));
            ////-- UPDATE THE PATH...
            //strSQL = @"SELECT TRACES_INBOX_HEADER_ID FROM TRN_TRACES_INBOX_HEADER_ACTION WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' AND COMM_INB_ID = '" + commId + "'";
            //string strInboxHeaderId = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            ////
            //strSQL = @"UPDATE TRN_TRACES_INBOX_HEADER_ACTION SET CERT_PATH ='" + Path.Combine(subPath, FileName) + "' WHERE TRACES_INBOX_HEADER_ID = " + strInboxHeaderId;
            //dmlService.J_BeginTransaction();
            //if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
            //{
            //    dmlService.J_Rollback();
            //    return;
            //}
            //dmlService.J_Commit();
            ////
            //cmnService.J_UserMessage("Certificate downloaded successfully!\nSaved to: " + userPath);
            ////
            //System.Diagnostics.Process.Start(userPath);
        }
        #endregion


        #region DgvStatementList_SelectionChanged
        private void DgvStatementList_SelectionChanged(object sender, EventArgs e)
        {
            //ApplyButtonRules();
        }
        #endregion


        #region GetDisplayQuarter
        private static string GetDisplayQuarter(string tracesQuarterCode)
        {
            switch (tracesQuarterCode)
            {
                case "3": return "Q1";
                case "4": return "Q2";
                case "5": return "Q3";
                case "6": return "Q4";
                default: return "Q?";
            }
        }
        #endregion

        #region StatementStatusGridRow
        class StatementStatusGridRow
        {
            public string FinYear { get; set; }
            public string Quarter { get; set; }   // Display Q1–Q4

            public string Q24_Reg { get; set; }
            public string Q24_Corr { get; set; }

            public string Q26_Reg { get; set; }
            public string Q26_Corr { get; set; }

            public string Q27_Reg { get; set; }
            public string Q27_Corr { get; set; }

            public string Q27EQ_Reg { get; set; }
            public string Q27EQ_Corr { get; set; }
        }
        #endregion


        #region Grid population code
        private void BindStatementStatusGrid(List<TracesTableRow> data)
        {
            SetupStatementStatusGridUI(); // IMPORTANT: ensures columns exist

            dgvStatementStatus.Rows.Clear();

            if (data == null || data.Count == 0)
                return;

            var groups = data
                .GroupBy(x => new { x.FinYear, x.Quarter })
                .OrderByDescending(g => g.Key.FinYear)
                .ThenByDescending(g => g.Key.Quarter);

            foreach (var grp in groups)
            {
                string displayQtr = GetDisplayQuarter(grp.Key.Quarter);
                string fy = grp.Key.FinYear;
                string fyDisplay = $"{fy}-{(int.Parse(fy) + 1)}";

                string qtrKey = $"{displayQtr} ({fyDisplay})";

                int rowIndex = dgvStatementStatus.Rows.Add();
                DataGridViewRow gridRow = dgvStatementStatus.Rows[rowIndex];

                gridRow.Cells["FYQtr"].Value = qtrKey;

                foreach (var row in grp)
                {
                    if (row.Cells == null || row.Cells.Count < 5)
                        continue;

                    string param = Convert.ToString(row.Cells[0])?.Trim();

                    // HARD FILTER — CRITICAL
                    if (string.IsNullOrEmpty(param))
                        continue;

                    if (param.Equals("Parameter", StringComparison.OrdinalIgnoreCase))
                        continue;

                    // =========================
                    // REGULAR STATEMENT STATUS
                    // =========================
                    if (param.Equals("Status of Regular Statement", StringComparison.OrdinalIgnoreCase))
                    {
                        ////SetStatusCell(gridRow.Cells["24Q_Reg"], row.Cells[1]);
                        ////SetStatusCell(gridRow.Cells["26Q_Reg"], row.Cells[2]);
                        ////SetStatusCell(gridRow.Cells["27Q_Reg"], row.Cells[3]);
                        ////SetStatusCell(gridRow.Cells["27EQ_Reg"], row.Cells[4]);
                        //###### 2026/01/19
                        string fyQtrText = $"{displayQtr} ({fyDisplay})";
                        //
                        SetStatusCell(gridRow.Cells["24Q_Reg"], row.Cells[1], fyQtrText, "24Q");
                        SetStatusCell(gridRow.Cells["26Q_Reg"], row.Cells[2], fyQtrText, "26Q");
                        SetStatusCell(gridRow.Cells["27Q_Reg"], row.Cells[3], fyQtrText, "27Q");
                        SetStatusCell(gridRow.Cells["27EQ_Reg"], row.Cells[4], fyQtrText, "27EQ");
                        //###### 2026/01/19

                        // Tooltip + metadata only
                        SetRegCell(gridRow.Cells["24Q_Reg"], row.Cells[1]);
                        SetRegCell(gridRow.Cells["26Q_Reg"], row.Cells[2]);
                        SetRegCell(gridRow.Cells["27Q_Reg"], row.Cells[3]);
                        SetRegCell(gridRow.Cells["27EQ_Reg"], row.Cells[4]);

                        continue;
                    }

                    // =========================
                    // CORRECTION COUNT
                    // =========================
                    if (param.Equals("Count of Correction Statements", StringComparison.OrdinalIgnoreCase))
                    {
                        gridRow.Cells["24Q_Cor"].Value = NormalizeNumber(row.Cells[1]);
                        gridRow.Cells["26Q_Cor"].Value = NormalizeNumber(row.Cells[2]);
                        gridRow.Cells["27Q_Cor"].Value = NormalizeNumber(row.Cells[3]);
                        gridRow.Cells["27EQ_Cor"].Value = NormalizeNumber(row.Cells[4]);

                        continue;
                    }

                    // =========================
                    // DEFAULTS (STORE ONLY)
                    // =========================
                    if (IsDefaultParameter(param))
                    {
                        StoreDefault(row, qtrKey);
                    }
                }
                ApplyProcDefOverrides(gridRow, qtrKey);
            }
            //THIS IS THE ONLY CORRECT PLACE
            //ApplyRegCellVisuals();
        }

        #endregion


        #region SetRegCell
        //private void SetRegCell(DataGridViewCell cell, string rawStatus)
        //{
        //    string statusText = GetStatusText(rawStatus);
        //    cell.Value = statusText;

        //    if (cell.OwningRow == null || cell.OwningColumn == null)
        //        return;

        //    string qtr = Convert.ToString(cell.OwningRow.Cells["FYQtr"].Value);
        //    string form = cell.OwningColumn.Name.Split('_')[0];
        //    string key = $"{qtr}|{form}";

        //    bool hasDefaults =
        //        _defaultsStore.ContainsKey(key) &&
        //        HasAnyDefaultAmount(_defaultsStore[key]);

        //    cell.Tag = new
        //    {
        //        IsReg = true,
        //        HasDefaults = hasDefaults
        //    };

        //    //cell.Tag = new RegCellMeta
        //    //{
        //    //    HasDefaults = hasDefaults
        //    //};

        //    // Tooltip (already working)
        //    cell.ToolTipText = string.IsNullOrWhiteSpace(statusText)
        //        ? "Regular Statement - Not Filed"
        //        : "Status of Regular Statement";
        //}

        private void SetRegCell(DataGridViewCell cell, string rawStatus)
        {
            if (cell.OwningRow == null || cell.OwningColumn == null)
                return;

            string qtr = Convert.ToString(cell.OwningRow.Cells["FYQtr"].Value);
            string form = cell.OwningColumn.Name.Split('_')[0];
            string key = $"{qtr}|{form}";

            bool hasDefaults =
                _defaultsStore.ContainsKey(key) &&
                HasAnyDefaultAmount(_defaultsStore[key]);

            cell.Tag = new
            {
                IsReg = true,
                HasDefaults = hasDefaults
            };

            // Tooltip only
            cell.ToolTipText = string.IsNullOrWhiteSpace(cell.Value?.ToString())
                ? "Regular Statement - Not Filed"
                : "Status of Regular Statement";
        }


        private bool HasAnyDefaultAmount(Dictionary<string, decimal> defaults)
        {
            return defaults != null && defaults.Values.Any(v => v > 0);
        }


        #endregion

        #region dgvStatementStatus_CellFormatting
        private void dgvStatementStatus_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var grid = dgvStatementStatus;
            var cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Tag == null)
                return;

            dynamic tag = cell.Tag;

            if (tag.IsReg != true)
                return;

            // RESET font every time (VERY important)
            //e.CellStyle.Font = grid.Font;

            //if (tag.HasDefaults == true)
            //{
                //// ONLY BOLD
                //e.CellStyle.Font = new Font(
                //    grid.Font,
                //    FontStyle.Bold
                //);
                // NORMAL state
                e.CellStyle.Font = new Font(grid.Font, FontStyle.Bold);

                // SELECTION state (THIS WAS MISSING)
                e.CellStyle.SelectionBackColor = grid.DefaultCellStyle.BackColor;
                e.CellStyle.SelectionForeColor = cell.Style.ForeColor;
            //}

            e.FormattingApplied = true;
        }
        #endregion

        #region GetStatusText
        private string GetStatusText(string regStatus)
        {
            if (string.IsNullOrWhiteSpace(regStatus))
                return "";

            string s = regStatus.Trim();

            // TRACES returns color names like: Green, Yellow, Red, Gray, Orange, etc.
            // We follow your rule: Gray/Not Filed => blank
            if (s.Equals("Gray", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("", StringComparison.OrdinalIgnoreCase))
                return "";

            if (s.Equals("Green", StringComparison.OrdinalIgnoreCase))
                return "Processed";     // Processed

            if (s.Equals("Yellow", StringComparison.OrdinalIgnoreCase))
                return "Defaults";    // Defaults

            if (s.Equals("Red", StringComparison.OrdinalIgnoreCase))
                return "Rejected";       // Rejected

            if (s.Equals("Orange", StringComparison.OrdinalIgnoreCase))
                return "Filed";    // Filed

            // fallback (if TRACES adds any new token)
            return s;
        }
        #endregion

        #region ResolveFinalStatus //-- 2026/01/19
        private string ResolveFinalStatus(
                string rawStatus,
                string fyQtrText,
                string form)
        {
            string baseStatus = GetStatusText(rawStatus);

            if (string.IsNullOrEmpty(baseStatus))
                return baseStatus;

            string key = $"{fyQtrText}|{form}";

            // Case 1: Processed + defaults > Proc (Def)
            if (baseStatus.Equals("Processed", StringComparison.OrdinalIgnoreCase)
                && _defaultsStore.ContainsKey(key))
            {
                return "Proc (Def)";
            }

            // Case 2: Defaults dominate everything else
            if (_defaultsStore.ContainsKey(key)
                && baseStatus.Equals("Defaults", StringComparison.OrdinalIgnoreCase))
            {
                return "Defaults";
            }

            return baseStatus;
        }
        #endregion


        #region NormalizeNumber
        private string NormalizeNumber(string val)
        {
            if (string.IsNullOrWhiteSpace(val) || val == "0" || val == "NA")
                return "";

            return val.Trim();
        }
        #endregion

        #region Statement Status Grid UI (2-level header: 24Q/26Q/27Q/27EQ with Reg/Cor)

        private bool _statementGridReady = false;

        // Call this ONCE (Form_Load) BEFORE BindStatementStatusGrid()
        private void SetupStatementStatusGridUI()
        {
            if (_statementGridReady) return;

            dgvStatementStatus.SuspendLayout();
            dgvStatementStatus.Rows.Clear();
            dgvStatementStatus.Columns.Clear();

            dgvStatementStatus.ReadOnly = true;
            dgvStatementStatus.AllowUserToAddRows = false;
            dgvStatementStatus.AllowUserToDeleteRows = false;
            dgvStatementStatus.AllowUserToResizeRows = false;
            dgvStatementStatus.AllowUserToResizeColumns = false;
            dgvStatementStatus.MultiSelect = false;
            dgvStatementStatus.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvStatementStatus.RowHeadersVisible = false;
            dgvStatementStatus.AutoGenerateColumns = false;
            dgvStatementStatus.BackgroundColor = SystemColors.Control;
            dgvStatementStatus.BorderStyle = BorderStyle.FixedSingle;

            dgvStatementStatus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Increase header height (you asked)
            dgvStatementStatus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvStatementStatus.ColumnHeadersHeight = 48; // adjust if needed
            dgvStatementStatus.EnableHeadersVisualStyles = false;

            // Left row header column FYQtr
            dgvStatementStatus.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FYQtr",
                HeaderText = "",     // top merged header will be drawn manually
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true,
                FillWeight = 22
            });

            // Create 8 columns: 24Q(Reg/Cor), 26Q(Reg/Cor), 27Q(Reg/Cor), 27EQ(Reg/Cor)
            AddFormColumns("24Q");
            AddFormColumns("26Q");
            AddFormColumns("27Q");
            AddFormColumns("27EQ");

            // Align
            foreach (DataGridViewColumn col in dgvStatementStatus.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvStatementStatus.Columns["FYQtr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Hook paint event for merged headers
            dgvStatementStatus.Paint -= dgvStatementStatus_Paint;
            dgvStatementStatus.Paint += dgvStatementStatus_Paint;

            dgvStatementStatus.CellPainting -= dgvStatementStatus_CellPainting;
            dgvStatementStatus.CellPainting += dgvStatementStatus_CellPainting;

            _statementGridReady = true;
            dgvStatementStatus.ResumeLayout();
        }

        private void AddFormColumns(string form)
        {
            dgvStatementStatus.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = $"{form}_Reg",
                HeaderText = "Reg",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true,
                FillWeight = 9
            });

            dgvStatementStatus.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = $"{form}_Cor",
                HeaderText = "Cor",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true,
                FillWeight = 9
            });
        }

        // Paint merged top header row (24Q/26Q/27Q/27EQ spanning two columns each)
        private void dgvStatementStatus_Paint(object sender, PaintEventArgs e)
        {
            if (dgvStatementStatus.Columns.Count == 0) return;

            DrawMergedHeader(e.Graphics, "24Q", "24Q_Reg", "24Q_Cor");
            DrawMergedHeader(e.Graphics, "26Q", "26Q_Reg", "26Q_Cor");
            DrawMergedHeader(e.Graphics, "27Q", "27Q_Reg", "27Q_Cor");
            DrawMergedHeader(e.Graphics, "27EQ", "27EQ_Reg", "27EQ_Cor");
        }

        // Draw a merged header cell across 2 columns (Reg+Cor)
        private void DrawMergedHeader(Graphics g, string title, string col1, string col2)
        {
            var grid = dgvStatementStatus;

            if (!grid.Columns.Contains(col1) || !grid.Columns.Contains(col2))
                return;

            Rectangle r1 = grid.GetCellDisplayRectangle(grid.Columns[col1].Index, -1, true);
            Rectangle r2 = grid.GetCellDisplayRectangle(grid.Columns[col2].Index, -1, true);

            // Top half of header area
            Rectangle merged = new Rectangle(
                r1.X,
                r1.Y,
                r1.Width + r2.Width,
                r1.Height / 2);

            using (Brush b = new SolidBrush(grid.ColumnHeadersDefaultCellStyle.BackColor == Color.Empty
                ? Color.Gainsboro
                : grid.ColumnHeadersDefaultCellStyle.BackColor))
            {
                g.FillRectangle(b, merged);
            }

            using (Pen p = new Pen(Color.Gray))
            {
                g.DrawRectangle(p, merged);
            }

            TextRenderer.DrawText(
                g,
                title,
                grid.ColumnHeadersDefaultCellStyle.Font ?? grid.Font,
                merged,
                Color.Black,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // Make the normal header text (Reg/Cor) appear in bottom half only
        private void dgvStatementStatus_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.ClipBounds, false);

                Rectangle r = e.CellBounds;

                // Push "Reg/Cor" down (bottom half)
                r.Y += r.Height / 2;
                r.Height /= 2;

                TextRenderer.DrawText(
                    e.Graphics,
                    Convert.ToString(e.FormattedValue),
                    e.CellStyle.Font,
                    r,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        #endregion

        #region GetStatusColor
        private Color GetStatusColor(string statusText)
        {
            if (string.IsNullOrWhiteSpace(statusText))
                return dgvStatementStatus.DefaultCellStyle.ForeColor;

            switch (statusText)
            {
                case "Processed":
                    return Color.Green;

                case "Proc (Def)":
                case "Defaults":
                    return Color.Goldenrod;   // Yellow that is readable

                case "Rejected":
                    return Color.Red;

                case "Filed":
                    return Color.DarkOrange;

                default:
                    return dgvStatementStatus.DefaultCellStyle.ForeColor;
            }
        }
        #endregion

        #region SetStatusCell
        private void SetStatusCell(DataGridViewCell cell, string rawStatus)
        {
            string text = GetStatusText(rawStatus);

            cell.Value = text;
            cell.Style.ForeColor = GetStatusColor(text);
            //cell.Style.Font = new Font(dgvStatementStatus.Font, FontStyle.Bold);
        }
        #endregion

        #region SetStatusCell (UPDATED)//-- 2026/01/19

        //private void SetStatusCell(
        //    DataGridViewCell cell,
        //    string rawStatus,
        //    string fyQtrText,   // "Qx (YYYY-YYYY)"
        //    string form         // "24Q/26Q/27Q/27EQ"
        //)
        //{
        //    var resolved = ResolveFinalStatus(rawStatus, fyQtrText, form);

        //    cell.Value = resolved.Text;
        //    cell.Style.ForeColor = resolved.Color;
        //}
        //private void SetStatusCell(
        //                        DataGridViewCell cell,
        //                        string rawStatus,
        //                        string fyQtrText,
        //                        string form)
        //{
        //    string finalText = ResolveFinalStatus(rawStatus, fyQtrText, form);

        //    cell.Value = finalText;
        //    cell.Style.ForeColor = GetStatusColor(finalText);
        //}

        private void SetStatusCell(
                    DataGridViewCell cell,
                    string rawStatus,
                    string fyQtrText,
                    string form)
        {
            string finalStatus = ResolveFinalStatus(rawStatus, fyQtrText, form);

            cell.Value = finalStatus;
            cell.Style.ForeColor = GetStatusColor(finalStatus);
        }

        #endregion

        #region IsRegColumn
        private bool IsRegColumn(string columnName)
        {
            return columnName.Equals("24Q_REG", StringComparison.OrdinalIgnoreCase)
                || columnName.Equals("26Q_REG", StringComparison.OrdinalIgnoreCase)
                || columnName.Equals("27Q_REG", StringComparison.OrdinalIgnoreCase)
                || columnName.Equals("27EQ_REG", StringComparison.OrdinalIgnoreCase);
        }
        #endregion

        
        #region dgvStatementStatus_CellClick
        private void dgvStatementStatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var grid = dgvStatementStatus;
            string colName = grid.Columns[e.ColumnIndex].Name;

            if (!IsRegColumn(colName))
                return;

            string form = colName.Split('_')[0]; // 24Q / 26Q etc.
            string qtr = grid.Rows[e.RowIndex].Cells["FYQtr"].Value?.ToString();

            if (string.IsNullOrEmpty(qtr))
                return;

            string key = $"{qtr}|{form}";

            if (!_defaultsStore.ContainsKey(key))
            {
                //-- MessageBox.Show("No defaults for this statement.");
                return;
            }

            ShowDefaults(key);
        }
        #endregion

        #region ShowDefaults
        private void ShowDefaults(string key)
        {
            var lines = _defaultsStore[key]
                .Select(kv => $"{kv.Key}: {kv.Value:0.00}");

            string msg = string.Join(Environment.NewLine, lines);

            if (msg != string.Empty)
            {
                //MessageBox.Show(
                //    msg,
                //    "Default Payable Amount (Breakup)",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Information
                //);
                cmnService.J_UserMessage("Default Payable Amount (Breakup)\n\n" + msg, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ParseAmount
        private decimal ParseAmount(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return 0m;

            string s = raw.Trim();

            // TRACES sends NA for non-applicable
            if (s.Equals("NA", StringComparison.OrdinalIgnoreCase))
                return 0m;

            // Remove commas if any
            s = s.Replace(",", "");

            if (decimal.TryParse(s, out decimal val))
                return val;

            return 0m;
        }
        #endregion
        
        #region StoreDefault
        private void StoreDefault(TracesTableRow row, string qtrKey)
        {
            string[] forms = { "24Q", "26Q", "27Q", "27EQ" };

            for (int i = 0; i < forms.Length; i++)
            {
                decimal amount = ParseAmount(row.Cells[i + 1]);

                if (amount <= 0)
                    continue;

                string key = $"{qtrKey}|{forms[i]}";

                if (!_defaultsStore.ContainsKey(key))
                    _defaultsStore[key] = new Dictionary<string, decimal>();

                _defaultsStore[key][row.Cells[0]] = amount;
            }
        }
        #endregion


        #region LnkInbox_MouseClick
        private void LnkInbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ctxtMnuStripInbox.Show(lnkInbox, new Point(e.X, e.Y));
        }
        #endregion

        #region CtxtMnuInboxAction_Click
        private void CtxtMnuInboxAction_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnTracesCommunication_Action(), J_Var.frmMain, "Inbox - Action");
        }
        #endregion

        #region CtxtMnuNoInboxAction_Click
        private void CtxtMnuNoInboxAction_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnTracesCommunication_NoAction(), J_Var.frmMain, "Inbox - No Action");
        }
        #endregion

        #region Final Status Resolver (NEW)//-- 2026/01/19
        private bool HasDefaults(string fyQtrText, string form)
        {
            // fyQtrText example: "Q2 (2025-2026)"
            string key = $"{fyQtrText}|{form}";
            return _defaultsStore != null && _defaultsStore.ContainsKey(key);
        }

        //private (string Text, Color Color) ResolveFinalStatus(
        //    string rawStatus,          // "Green/Orange/Yellow/Red/Gray"
        //    string fyQtrText,           // "Qx (YYYY-YYYY)"
        //    string form                 // "24Q/26Q/27Q/27EQ"
        //)
        //{
        //    string baseText = GetStatusText(rawStatus); // your existing mapping
        //    bool hasDef = HasDefaults(fyQtrText, form);

        //    // Rejected always dominates
        //    if (baseText == "Rejected")
        //        return ("Rejected", Color.Red);

        //    // Defaults dominate everything else
        //    if (hasDef)
        //    {
        //        // Processed + defaults > Proc (Def)
        //        if (baseText == "Processed")
        //            return ("Proc (Def)", Color.Goldenrod);

        //        // Any other state with defaults > Default
        //        return ("Default", Color.Goldenrod);
        //    }

        //    // No defaults > normal states
        //    switch (baseText)
        //    {
        //        case "Processed":
        //            return ("Processed", Color.Green);

        //        case "Filed":
        //            return ("Filed", Color.LightGreen);

        //        case "Defaults":
        //            return ("Defaults", Color.Goldenrod);

        //        case "Rejected":
        //            return ("Rejected", Color.Red);

        //        default:
        //            // Not Filed / Gray / blank
        //            return ("", dgvStatementStatus.DefaultCellStyle.ForeColor);
        //    }
        //}

        #endregion

        #region ApplyProcDefOverrides
        private void ApplyProcDefOverrides(DataGridViewRow gridRow, string fyQtrText)
        {
            string[] forms = { "24Q", "26Q", "27Q", "27EQ" };

            foreach (string form in forms)
            {
                string key = fyQtrText + "|" + form;

                bool hasDefaults = _defaultsStore.ContainsKey(key);

                DataGridViewCell cell = gridRow.Cells[form + "_Reg"];

                string baseStatus = Convert.ToString(cell.Value);

                if (string.IsNullOrEmpty(baseStatus))
                    continue;

                // Processed + defaults => Proc (Def)
                if (baseStatus.Equals("Processed", StringComparison.OrdinalIgnoreCase) && hasDefaults)
                {
                    cell.Value = "Proc (Def)";
                    cell.Style.ForeColor = GetStatusColor("Proc (Def)");
                    continue;
                }

                // Defaults shown by TRACES, but we want label "Defaults"
                //if (baseStatus.Equals("Defaults", StringComparison.OrdinalIgnoreCase))
                //{
                //    cell.Value = "Defaults";
                //    cell.Style.ForeColor = GetStatusColor("Defaults");
                //    continue;
                //}
                if (baseStatus.Equals("Defaults", StringComparison.OrdinalIgnoreCase) && hasDefaults == false)
                {
                    cell.Value = "Processed";
                    cell.Style.ForeColor = GetStatusColor("Processed");
                    continue;
                }
            }
        }
        #endregion

        #region LnkViewUnconsumedChallan_LinkClicked
        private void LnkViewUnconsumedChallan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnTracesViewUnconsumedChallan(), J_Var.frmMain, "Unconsumed Challan");
        }
        #endregion


        #region dgvStatementStatus_CellMouseMove
        private void dgvStatementStatus_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                dgvStatementStatus.Cursor = Cursors.Default;
                return;
            }

            var grid = dgvStatementStatus;
            var cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];

            // Only REG columns
            if (!IsRegColumn(grid.Columns[e.ColumnIndex].Name))
            {
                dgvStatementStatus.Cursor = Cursors.Default;
                return;
            }

            string text = Convert.ToString(cell.Value);

            if (string.IsNullOrEmpty(text))
            {
                dgvStatementStatus.Cursor = Cursors.Default;
                return;
            }

            // Hand cursor ONLY for Defaults / Proc (Def)
            if (text.Equals("Defaults", StringComparison.OrdinalIgnoreCase) ||
                text.Equals("Proc (Def)", StringComparison.OrdinalIgnoreCase))
            {
                dgvStatementStatus.Cursor = Cursors.Hand;
            }
            else
            {
                dgvStatementStatus.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region dgvStatementStatus_CellMouseLeave
        private void dgvStatementStatus_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvStatementStatus.Cursor = Cursors.Default;
        }
        #endregion

        private void LnkLogOff_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }

}

