
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
    public partial class TrnDeclarationNonFilling : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnDeclarationNonFilling()
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

        private string CurrentCaptchaId = "";
        enum enmRequestType
        {
            Login,
            ListofFiliing,
            AddStatementDetail,
            LogOff
        }

        int intRowIndex = 0;
        int intColumnIndex = 0;

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
            lblTitle.Text = "Request For Submit Declaration of non-filling";
            //
            //InitializeCaptcha();
            picCaptcha.Image = Properties.Resources.captcha_loading;
            if (!bgWorkerLoadCaptcha.IsBusy)
                bgWorkerLoadCaptcha.RunWorkerAsync();
            //--

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
                objLogin.CaptchaId = this.CurrentCaptchaId;
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

            if (cmbFAYear.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Select FA Year");
                cmbFAYear.Select();
                return;
            }
            if (cmbFormNo.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Select Form");
                cmbFormNo.Select();
                return;
            }
            if (cmbQtr.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Select FA Year");
                cmbQtr.Select();
                return;
            }
            if (cmbReason.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Select Reason");
                cmbReason.Select();
                return;
            }
            if (cmbReason.SelectedIndex == 7)
            {
                if (string.IsNullOrWhiteSpace(txtspecReasn.Text))
                {
                    cmnService.J_UserMessage("Specify the exact reason is mandatory");
                    return;
                }
            }



            NonFilling objData = new NonFilling();
            objData.Flag = "X";
            objData.SpecifyReason = "BLANK";

            if (cmbReason.SelectedIndex == 7)
                objData.SpecifyReason = txtspecReasn.Text;

            //------------------------------------------------
            if (cmbReason.SelectedIndex == 4 || cmbReason.SelectedIndex == 6)
            {
                DialogResult result = MessageBox.Show("Please confirm, whether you have requested jurisdictional assessing officer for closure/surrender of TAN.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    objData.Flag = "Y";
                }
                else if (result == DialogResult.No)
                {
                    objData.Flag = "N";
                }

            }


            //------------------------------------------------
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.AddStatementDetail);


            objData.FAYear = cmbFAYear.Text.Substring(0, cmbFAYear.Text.IndexOf("-"));
            if (cmbQtr.SelectedIndex > 0)
            {
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


            if (cmbReason.SelectedIndex > 0)
            {
                if (cmbReason.SelectedIndex == 6)
                    objData.Reason = "7";
                else if (cmbReason.SelectedIndex == 7)
                    objData.Reason = "6";
                else
                    objData.Reason = Convert.ToString(cmbReason.SelectedIndex);
            }


            objData.Forms = cmbFormNo.Text; ;
            objList.Add(objData);
            //-------------------------------------------
            pgTimer.Start();
            //-------------------------------------------
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
                // LOGIN REQUEST
                case enmRequestType.Login:
                    //objResponse = objAccount.makeLoginToTRACES((TracesLogin)objList[1]);
                    objResponse = objAccount.makeLoginToTraces_New((TracesLogin)objList[1]);
                    //---------------------------------------------------
                    objRetval.Add(enmRequestType.Login);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    break;

                // LIST OF STATEMENT STATUS FILES
                case enmRequestType.ListofFiliing:
                    DataTable table;

                    //TracesResponse response = objAccount.RequestForFillingStatus(out table);
                    TracesResponse response = objAccount.RequestForFillingStatus_New(out table);
                    objRetval.Add(enmRequestType.ListofFiliing);
                    objRetval.Add(response);
                    objRetval.Add(table);
                    e.Result = objRetval;

                    break;
                case enmRequestType.AddStatementDetail:

                    NonFilling objdata = (NonFilling)objList[1];
                    //response = objAccount.RequestForAddStatementDetails(objdata, out table);
                    response = objAccount.RequestForAddStatementDetails_New(objdata, out table);
                    objRetval.Add(enmRequestType.AddStatementDetail);
                    objRetval.Add(response);
                    objRetval.Add(table);
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
                    case enmRequestType.Login:
                        //pgTimer.Stop();
                        //pgTimer.Interval = 1000;
                        //pBar.Value = 99;
                        //---------------------------------------------------
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            ArrayList objList = new ArrayList();
                            objList.Add(enmRequestType.ListofFiliing);
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
                    case enmRequestType.ListofFiliing:
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

                        break;

                    case enmRequestType.AddStatementDetail:

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
                        //----------------------------------
                       // dTable = (DataTable)objMessage[2];
                        cmnService.J_UserMessage(objResponse.Message);

                        //  PopulateNonFilingGrid(dTable);
                        //----------------------------------
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
            txtspecReasn.Visible = false;
            if (cmbReason.SelectedIndex > 0)
            {
                if (cmbReason.SelectedIndex == 7)
                    txtspecReasn.Visible = true;
                else
                    txtspecReasn.Visible = false;
            }
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
                //Stream imgStream = objAccount.MakeInitialRequest();
                //Image img = Image.FromStream(imgStream);
                //this.picCaptcha.Image = img;
                var captcha = objAccount.MakeInitialRequest_NEW();
                this.CurrentCaptchaId = captcha.CaptchaId;
                Image captchaImage = captcha.CaptchaImage;
                this.picCaptcha.Image = captchaImage;
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
                    InitializeCaptcha();
                    break;

                case enmRequestType.ListofFiliing:
                    grpListStatement.Visible = true;
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                    grpInputDetails.Visible = false;
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
                    grpInputDetails.Visible = true;
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

            //-- REASON.

            string[] strReason = { "Not Liable to deduct for the selected statement period",
                                    "No Payment made / Credited to Deductee",
                                    "Temporarily Business Closed",
                                    "Permanently Business Closed",
                                    "Payment Below Threshold to Deductee",
                                    "Branch Shifted",
                                    "Any Other Reason" };

            dmlService.J_PopulateComboBox(strReason, ref cmbReason);



        }
        #endregion

        #region PopulateDatagridView
        //void PopulateDatagridView(DataTable dsRecords)
        //{
        //    //CHECKING IF RECORD EXISTS OR NOT

        //    if (dsRecords.Rows.Count <= 0)
        //    {
        //        ShowHideLoginDetails(enmRequestType.AddStatementDetail);
        //        //btnBack.Visible = false;
        //        return;
        //    }
        //    else
        //    {
        //        ShowHideLoginDetails(enmRequestType.ListofFiliing);
        //        //btnBack.Visible = true;
        //    }
        //    //---------------------------------------
        //    // DATA BIND TO GRIDVIEW CONTROL
        //    dgvStatementList.Columns.Clear();
        //    dgvStatementList.DataSource = null;
        //    dgvStatementList.DataSource = dsRecords;
        //    //---------------------------------------
        //    // CREATE DOWNLOAD BUTTON & PROGRESSBAR CONTROL COLUMNS 
        //    //---------------------------------------
        //    dgvStatementList.Columns[6].Width = 0;
        //    dgvStatementList.Columns[6].Visible = false;
        //    dgvStatementList.Columns[6].ReadOnly = true;

        //    dgvStatementList.Columns[7].Width = 0;
        //    dgvStatementList.Columns[7].Visible = false;
        //    dgvStatementList.Columns[7].ReadOnly = true;

        //    dgvStatementList.Columns[8].Width = 0;
        //    dgvStatementList.Columns[8].Visible = false;
        //    dgvStatementList.Columns[8].ReadOnly = true;

        //    dgvStatementList.Columns[9].Width = 0;
        //    dgvStatementList.Columns[9].Visible = false;
        //    dgvStatementList.Columns[9].ReadOnly = true;
        //    //---------------------------------------

        //    dgvStatementList.Columns[1].Width = 130;
        //    dgvStatementList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
        //    dgvStatementList.Columns[2].ReadOnly = true;

        //    dgvStatementList.Columns[3].Width = 150;
        //    dgvStatementList.Columns[3].ReadOnly = true;
        //    dgvStatementList.Columns[4].ReadOnly = true;
        //    dgvStatementList.Columns[5].ReadOnly = true;
        //    //dgvStatementList.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    //dgvStatementList.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    //dgvStatementList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
        //    //dgvStatementList.Columns[6].ReadOnly = false;

        //    //DataGridViewCheckBoxColumn btn = new DataGridViewCheckBoxColumn();
        //    //dgvStatementList.Columns.Add(btn);
        //    //btn.HeaderText = "";
        //    //btn.ValueType = typeof(bool);
        //    //btn.ReadOnly = false;
        //    //btn.Name = "chkDetails";
        //    //btn.Width = 90;
        //    //------------------------------------------------------------
        //    // SET FOCUS 
        //    //if (dsRecords.Rows.Count > 0)
        //    //{
        //    //    dgvStatementList.Rows[0].Cells[6].Selected = true;

        //    //    dgvStatementList.CurrentCell = dgvStatementList.Rows[0].Cells[6];
        //    //    dgvStatementList.BeginEdit(true);

        //    //}
        //}
        void PopulateDatagridView(DataTable dsRecords)
        {
            // CHECKING IF RECORD EXISTS OR NOT
            if (dsRecords == null || dsRecords.Rows.Count <= 0)
            {
                ShowHideLoginDetails(enmRequestType.AddStatementDetail);
                return;
            }
            else
            {
                ShowHideLoginDetails(enmRequestType.ListofFiliing);
            }

            //---------------------------------------
            // DATA BIND TO GRIDVIEW CONTROL
            //---------------------------------------
            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = null;
            dgvStatementList.AutoGenerateColumns = true;
            dgvStatementList.DataSource = dsRecords;

            //---------------------------------------
            // GRID VISUAL SETTINGS
            //---------------------------------------
            dgvStatementList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStatementList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvStatementList.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvStatementList.AllowUserToResizeColumns = false;
            dgvStatementList.AllowUserToResizeRows = false;
            dgvStatementList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStatementList.MultiSelect = false;
            dgvStatementList.ReadOnly = true;
            dgvStatementList.RowHeadersVisible = false;

            dgvStatementList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStatementList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgvStatementList.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            //---------------------------------------
            // HIDE INTERNAL COLUMNS (commId, hidfinYr, hidquat, declId)
            //---------------------------------------
            if (dgvStatementList.Columns.Count > 6)
            {
                dgvStatementList.Columns[6].Visible = false;
                dgvStatementList.Columns[7].Visible = false;
                dgvStatementList.Columns[8].Visible = false;
                dgvStatementList.Columns[9].Visible = false;
            }

            //---------------------------------------
            // CUSTOM WIDTH ADJUSTMENT FOR BETTER READABILITY
            //---------------------------------------
            foreach (DataGridViewColumn col in dgvStatementList.Columns)
            {
                if (!col.Visible) continue;

                // Make Reason column slightly wider
                if (col.HeaderText.Equals("Reason", StringComparison.OrdinalIgnoreCase))
                    col.FillWeight = 200;
                else if (col.HeaderText.Equals("Date", StringComparison.OrdinalIgnoreCase))
                    col.FillWeight = 70;
                else
                    col.FillWeight = 120;

                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.ReadOnly = true;
            }

            //---------------------------------------
            // OPTIONAL: EVENLY SPREAD IF YOU WANT ALL EQUAL WIDTH
            //---------------------------------------
            // int visibleCols = dgvStatementList.Columns.GetColumnCount(DataGridViewElementStates.Visible);
            // foreach (DataGridViewColumn col in dgvStatementList.Columns)
            // {
            //     if (col.Visible)
            //         col.Width = dgvStatementList.Width / visibleCols;
            // }

            //---------------------------------------
            // SET FOCUS TO FIRST ROW (if exists)
            //---------------------------------------
            if (dsRecords.Rows.Count > 0)
            {
                dgvStatementList.ClearSelection();
                dgvStatementList.Rows[0].Selected = true;
            }
        }








        #endregion



        private void btnBack_Click(object sender, EventArgs e)
        {

            grpInputDetails.Visible = false;
            grpListStatement.Visible = true;
        }

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0092", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }
    }

}

