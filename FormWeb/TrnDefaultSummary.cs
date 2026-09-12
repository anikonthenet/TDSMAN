
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
//ADDED BY DHRUB DHRUN ON 2015-09-23
using TDSMAN.Reports.Transaction;
using CrystalDecisions.CrystalReports.Engine;

using Microsoft.Reporting.WinForms;
using TDSMAN.FormRptRDLC;
using TDSMAN.FormRpt;
#endregion

namespace TDSMAN.FormWeb
{
    public partial class TrnDefaultSummary : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnDefaultSummary()
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

        TracesConnect objAccount = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        DateService dtService = new DateService();
        DMLService dmlService = new DMLService();

        //ADDED BY DHRUB DHRUN ON 2015-09-23
        ReportClass rptcls;

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();

        enum enmRequestType
        {
            Login,
            DefaultFaYearQtr,
            DefaultSummary,            
            LogOff
        }

        string strMessage = "";
        int intRowIndex = 0;
        int intColumnIndex = 0;
        string strCompanyName = "";
        //-- ADDED BY DHRUB On 2015-09-23
        DataTable dtDefaultSummary = new DataTable();
        //--
        DataTable dtStatementSummary = new DataTable();
        DataTable dtDefaultDetails = new DataTable();
        DataTable dtPANErrors = new DataTable();

        bool blResize = true;
        private string CurrentCaptchaId = "";
        #endregion

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

        //-- Added By Abhishek Dey On 22/08/2018 --
        #region TrnDefaultSummary_Activated
        private void TrnDefaultSummary_Activated(object sender, EventArgs e)
        {
            //-- Added By Abhishek Dey On 21/08/2018 --
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //-----------------------------------------
            blResize = false;
            //
        }
        #endregion
        //-----------------------------------------


        #region TrnChallanStatusTraces_Load
        private void TrnChallanStatusTraces_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //--
            //InitializeCaptcha();
            picCaptcha.Image = Properties.Resources.captcha_loading;
            if (!bgWorkerLoadCaptcha.IsBusy)
                bgWorkerLoadCaptcha.RunWorkerAsync();
            //--

            lblTitle.Text = "Default Summary";
            //
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
                    //objResponse = objAccount.RequestForDefaultSummary((TracesLogin)objList[1]);
                    objResponse = objAccount.RequestForDefaultSummary_New((TracesLogin)objList[1]);
                    //---------------------------------------------------
                    objRetval.Add(enmRequestType.Login);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    //---------------------------------------------------
                    break;
                // LIST OF DEFAULT SUMMARY
                case enmRequestType.DefaultSummary:
                    dgvDefaultSummary.Rows[intRowIndex].Cells[7].Value = 0;
                    //objResponse = objAccount.RequestDefaultSummaryDetails((ArrayList)objList[1]);
                    objResponse = objAccount.RequestDefaultSummaryDetails_New((ArrayList)objList[1]);
                    objRetval.Add(enmRequestType.DefaultSummary);
                    objRetval.Add(objResponse);
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
                            ShowHideLoginDetails(enmRequestType.DefaultFaYearQtr);

                            DataTable dTable = (DataTable)objResponse.CustomeTypes;

                            PopulateDatagridView(dTable);

                            //ADDED BY DHRUB ON 2015-09-23
                            dtDefaultSummary = dTable;
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
                    case enmRequestType.DefaultSummary:
                        pgTimerGrid.Stop();
                        dgvDefaultSummary.Rows[intRowIndex].Cells[7].Value = 100;
                        //-------------------------------------------------------
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.LogOff);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Failed )
                        {
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }
                        //--------------------------------------------------------
                        DataSet dsetTable = (DataSet)objResponse.CustomeTypes;

                        PopulateDetails(dsetTable);
                        ShowHideLoginDetails(enmRequestType.DefaultSummary);
                        //--
                        dtStatementSummary = dsetTable.Tables[0];
                        dtDefaultDetails = dsetTable.Tables[2];
                        dtPANErrors = dsetTable.Tables[3];
                        //--
                        break;                   

                    case enmRequestType.LogOff:
                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtTANNo.Text = "";
                        txtCaptchaCode.Text = "";
                        grpDefaultSummaryList.Visible = false;
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
                         "       COMPANY_NAME  " + //-- 2015/10/06 AnikG
                         "FROM   MST_TAN_ACCOUNT " +
                         "WHERE  TAN_NO LIKE '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "%' " +
                         "ORDER BY TAN_NO, TAN_ACCOUNT_ID";

                drdShowDeducteeHelp = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShowDeducteeHelp == null)
                {
                    lstDeducteeHelp.Visible = false;
                    drdShowDeducteeHelp.Close();
                    drdShowDeducteeHelp.Dispose();
                    return;
                }
                else
                {
                    lstDeducteeHelp.Items.Clear();
                    //lstDeducteeHelp.Height = 15;
                    lstDeducteeHelp.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        //lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) 
                        //                                        + drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15)
                        //                                        + drdShowDeducteeHelp["USER_PASSWORD"].ToString())); //-- 2015/10/06 AnikG
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
            //string strlstDeducteeHelp = lstDeducteeHelp.Text;

            //txtTANNo.Text = cmnService.J_Left(strlstDeducteeHelp, 10);
            //txtUserID.Text = cmnService.J_Mid(strlstDeducteeHelp, strlstDeducteeHelp.IndexOf(' '),
            //                                  strlstDeducteeHelp.LastIndexOf(' ') - strlstDeducteeHelp.IndexOf(' ')).Trim();
            //txtPassword.Text = cmnService.J_Right(strlstDeducteeHelp, strlstDeducteeHelp.Length - strlstDeducteeHelp.LastIndexOf(' ')).Trim();
            //-- 2015/10/06 AnikG
            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstDeducteeHelp, lstDeducteeHelp.SelectedIndex));
            txtTANNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtUserID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            strCompanyName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_NAME FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
                
            lstDeducteeHelp.Visible = false;
            //--
            txtCaptchaCode.Select();
        }
        #endregion

        #region lnkLogOff_Click
        private void lnkLogOff_Click(object sender, EventArgs e)
        {
            //
            this.Cursor = Cursors.WaitCursor;
            ShowHideLoginDetails(enmRequestType.LogOff);
            this.Cursor = Cursors.Default;
        }
        #endregion

        #region InitializeCaptcha
        private void InitializeCaptcha()
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
                objAccount = new TracesConnect();
                //Stream imgStream = objAccount.MakeInitialRequest();
                //Image img = Image.FromStream(imgStream);

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

        #region PopulateDatagridView
        void PopulateDatagridView(DataTable dsRecords)
        {
            // DATA BIND TO GRIDVIEW CONTROL
            dgvDefaultSummary.Columns.Clear();
            dgvDefaultSummary.DataSource = null;
            dgvDefaultSummary.DataSource = dsRecords;
            ////---------------------------------------
            ////CHECKING IF RECORD EXISTS OR NOT
            //if (dsRecords.Rows.Count <= 0)
            //{
            //    //cmnService.J_UserMessage("No data available for the specified search criteria");
            //    cmnService.J_UserMessage("No defaults found for the entered TAN.");
            //}
            ////---------------------------------------

            // CREATE DOWNLOAD BUTTON & PROGRESSBAR CONTROL COLUMNS 
            //------------------------------------------------------------
            dgvDefaultSummary.Columns[0].Width = 0;
            dgvDefaultSummary.Columns[0].Visible = false;
            dgvDefaultSummary.Columns[1].Width = 0;
            dgvDefaultSummary.Columns[1].Visible = false;
            dgvDefaultSummary.Columns[2].Width = 130;

            dgvDefaultSummary.Columns[3].Width = 150;
            dgvDefaultSummary.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewLinkColumn btn = new DataGridViewLinkColumn();
            dgvDefaultSummary.Columns.Add(btn);
            btn.HeaderText = "";
            btn.Text = "View Details";
            btn.Name = "lnkDetails";
            //------------------------------------------------------------
            btn.UseColumnTextForLinkValue = true;
            //------------------------------------------------------------
            DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            dgvDefaultSummary.Columns.Add(prg);
            prg.Name = "";
            prg.ProgressBarColor = Color.LightGreen;
            //
            //CHECKING IF RECORD EXISTS OR NOT
            if (dsRecords.Rows.Count <= 0)
            {
                //cmnService.J_UserMessage("No data available for the specified search criteria");
                cmnService.J_UserMessage("No defaults found for the entered TAN.");
            }
            //---------------------------------------
            foreach (DataGridViewRow gridRow in dgvDefaultSummary.Rows)
            {
                DataGridViewCell BookButtonCell = gridRow.Cells["Form Type"];

                if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "NO DATA AVAILABLE")
                {
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    gridRow.Cells["lnkDetails"] = new DataGridViewTextBoxCell();
                    //gridRow.Cells["btnDownload"].Value = "........oooops";
                }
            }
        }

        #endregion

        #region PopulateDetails
        private void PopulateDetails(DataSet dsRecord)
        {
            dgvStatement.DataSource = null;
            
            dgvDefaultSummaryDetails.DataSource = null;
            dgvPanError.DataSource = null;
            lblTotalPayable.Text = "";
            lblCorrectionCount.Text = "";
            lblNetAmount.Text = "";
            lblNetPayable.Text = "";


            dgvStatement.DataSource = dsRecord.Tables[0];
            dgvStatement.Columns[0].Width = 200;
            dgvStatement.Columns[1].Width = 200;
            dgvStatement.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            dgvDefaultSummaryDetails.DataSource = dsRecord.Tables[2];
            dgvDefaultSummaryDetails.Columns[0].Width = 40;
            dgvDefaultSummaryDetails.Columns[1].Width = 240;
            dgvDefaultSummaryDetails.Columns[2].Width = 105;
            dgvDefaultSummaryDetails.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDefaultSummaryDetails.Columns[4].Width = 90;

            dgvPanError.DataSource = dsRecord.Tables[3];
            dgvPanError.Columns[0].Width = 300;
            dgvPanError.Columns[1].Width = 300;

            if (dsRecord.Tables[1].Rows.Count > 0)
            {
                lblCorrectionCount.Text = Convert.ToString(dsRecord.Tables[1].Rows[0][0]);
                lblNetAmount.Text = Convert.ToString(dsRecord.Tables[1].Rows[0][1]);
                lblNetPayable.Text = Convert.ToString(dsRecord.Tables[1].Rows[0][1]);
            }
            double dblAmount = 0;
            foreach (DataGridViewRow gridRow in dgvDefaultSummaryDetails.Rows)
            {
                DataGridViewCell BookButtonCell = gridRow.Cells["Payable(Rs.)"];

                if (Convert.ToString(BookButtonCell.Value).Trim() != "" && Convert.ToString(BookButtonCell.Value).Trim().ToUpper() != "NA")
                {
                    dblAmount += Convert.ToDouble(BookButtonCell.Value);
                }
            }
            lblTotalPayable.Text = String.Format("{0:0.00}",dblAmount);

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
                    grpDefaultSummaryList.Visible = false;
                    grpLoginDetails.Visible = true;
                    // grpProgress.Visible = true;
                    InitializeCaptcha();
                    break;
                case enmRequestType.DefaultSummary:
                    grpDefaultSummaryList.Visible = false;
                    grpLoginDetails.Visible = false;
                    grpSummaryDetails.Visible = true;

                    //------------------------------------
                    //ADDED BY DHRUB ON 2015-09-24
                    //------------------------------------
                    if (strCompanyName == "")
                        lblSelectedTanNo.Text =  txtTANNo.Text.Trim();
                    else
                        lblSelectedTanNo.Text = strCompanyName + " [ " + txtTANNo.Text.Trim() + " ]  ";
                    //------------------------------------
                    lblSelectedFaYear.Text = " Fa Year : " + Convert.ToString(dgvDefaultSummary.Rows[intRowIndex].Cells[2].Value);  // FA YEAR
                    lblSelectedQtr.Text = " Quarter : " + Convert.ToString(dgvDefaultSummary.Rows[intRowIndex].Cells[3].Value);     // QUARTER 
                    lblSelectedFormNo.Text = " Form Type : " + Convert.ToString(dgvDefaultSummary.Rows[intRowIndex].Cells[4].Value);// FORM TYPE
                    //------------------------------------
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;
                    break;

                case enmRequestType.DefaultFaYearQtr:
                   // ClearControls();
                    grpDefaultSummaryList.Visible = true;
                    grpLoginDetails.Visible = false;
                    grpSummaryDetails.Visible = false;
                    //dgvStatement.DataSource = null;

                    //dgvDefaultSummaryDetails.DataSource = null;
                    //dgvPanError.DataSource = null;
                   // lblTotalPayable.Text = "";
                   // lblCorrectionCount.Text = "";
                   // lblNetAmount.Text = "";
                    //lblNetPayable.Text = "";

                    //------------------------------------
                    //ADDED BY DHRUB ON 2015-09-24
                    //------------------------------------
                    if (strCompanyName == "")
                        lblTanNo.Text = "TAN  : " + txtTANNo.Text.Trim();
                    else
                        lblTanNo.Text = strCompanyName + " [ " + txtTANNo.Text.Trim() + " ] ";
                    //------------------------------------
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;
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
            grpDefaultSummaryList.Visible = false;
            grpLoginDetails.Visible = true;
            grpProgress.Visible = true;

            dgvDefaultSummaryDetails.Columns.Clear();
            dgvDefaultSummaryDetails.DataSource = null;

            dgvDefaultSummary.Columns.Clear();
            dgvDefaultSummary.DataSource = null;

            //For Showing the helg grid when tan text is changed
            blnShowHelp = true;
            //
            txtTANNo.Select();

            // ----------------------------
            // -- POPULATE FORM COMBO BOXES
            // ----------------------------           
            //-- CHALLAN STATUS
           

        }
        #endregion
        
        #region dgvStatementList_CellClick
        private void dgvStatementList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //GETTING ROW & COLUMN INDEX OF SELECT ROW
            intRowIndex = e.RowIndex;
            intColumnIndex = e.ColumnIndex;
            //-------------------------------------------------------------------------------
            if (e.ColumnIndex == dgvDefaultSummary.Columns["lnkDetails"].Index && e.RowIndex >= 0)
            {
                if (Convert.ToString(dgvDefaultSummary.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "view details")
                {

                    dgvDefaultSummaryDetails.Columns.Clear();
                    dgvDefaultSummaryDetails.DataSource = null;

                    //-------------------------------------------------------------------------------
                    pgTimerGrid.Start();
                    // MessageBox.Show("Button on row {0} clicked" + e.RowIndex + " " + dgvDownloadDetails.Rows[e.RowIndex].Cells[intColumnIndex].Value);

                    ArrayList objList = new ArrayList();
                    ArrayList objData = new ArrayList();
                    objData.Add(Convert.ToString(dgvDefaultSummary.Rows[e.RowIndex].Cells[0].Value)); // FA YEAR CODE
                    objData.Add(Convert.ToString(dgvDefaultSummary.Rows[e.RowIndex].Cells[1].Value)); // QTR CODE
                    objData.Add(Convert.ToString(dgvDefaultSummary.Rows[e.RowIndex].Cells[2].Value)); // FA YEAR
                    objData.Add(Convert.ToString(dgvDefaultSummary.Rows[e.RowIndex].Cells[3].Value)); // QUARTER 
                    objData.Add(Convert.ToString(dgvDefaultSummary.Rows[e.RowIndex].Cells[4].Value)); // FORM TYPE
                    //-------------------------------------------------------------------------------
                    objList.Add(enmRequestType.DefaultSummary);
                    objList.Add(objData);

                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync(objList);
                    //-------------------------------------------------------------------------------


                }
            }
        }

        #endregion

        #region pgTimerGrid_Tick
        private void pgTimerGrid_Tick(object sender, EventArgs e)
        {
            int intValue = Convert.ToInt32(Convert.ToString(dgvDefaultSummary.Rows[intRowIndex].Cells[7].Value) == "" ? "0" : dgvDefaultSummary.Rows[intRowIndex].Cells[7].Value);
            if (intValue == 100) intValue = 0;

            // Slow down
            this.pgTimerGrid.Interval = (this.pgTimerGrid.Interval * 2);

            //Update progress bar
            if ((intValue + 1) > 100)
            {
                dgvDefaultSummary.Rows[intRowIndex].Cells[7].Value = 100;
            }
            else
            {
                intValue += 1;
                dgvDefaultSummary.Rows[intRowIndex].Cells[7].Value = intValue;

            }
        }

        #endregion

        #region  btnBack_Click
        private void btnBack_Click(object sender, EventArgs e)
        {
            ShowHideLoginDetails(enmRequestType.DefaultFaYearQtr);
        }

        #endregion

        #region  btnPrintDefaultSummary_Click
        private void btnPrintDefaultSummary_Click(object sender, EventArgs e)
        {
            try
            {
                //--------------------------------------------------------------
                this.Cursor = Cursors.WaitCursor;
                //-------------------------------------------------------------- 
                //string strCompanyName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_NAME FROM MST_TAN_ACCOUNT WHERE TAN_NO ='" + txtTANNo.Text + "'"));
                //string strDefaultSummary_TableName = "DEFAULT_SUMMARY_" + dtService.J_ConvertToIntYYYYMMDD(dmlService.J_ReturnServerDate()) + "_" + dmlService.J_ReturnServerTime().Replace(":", "").Substring(1, 6).Trim();
                //string strDefaultSummary_TableName = "DEFAULT_SUMMARY_" + dtService.J_ConvertToIntMMDDYYYY(dmlService.J_ReturnServerDate()) + "_" + dmlService.J_ReturnServerTime().Replace(":", "").Substring(1, 6).Trim();
                string strDefaultSummary_TableName = "TMP_DEFAULT_SUMMARY_" + string.Format("{0:ddMMyy}", System.DateTime.Now.Date) + "_" + string.Format("{0:HHmmss}", System.DateTime.Now) ;
                //
                dmlService.J_BeginTransaction();
                //--------------------------------------------------------------
                if (dmlService.J_IsDatabaseObjectExist(strDefaultSummary_TableName) == true)
                {
                    strSQL = " DROP TABLE " + strDefaultSummary_TableName + " ";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = @" CREATE TABLE " + strDefaultSummary_TableName + @" 
                            (" + cmnService.J_GetDataType("FA_YEAR", J_ColumnType.String, 10, J_DefaultValue.YES) + @",
                                " + cmnService.J_GetDataType("QTR", J_ColumnType.String, 2, J_DefaultValue.YES) + @",
                                " + cmnService.J_GetDataType("FORM_NO", J_ColumnType.String, 4, J_DefaultValue.YES) + @",
                                " + cmnService.J_GetDataType("NET_PAYABLE", J_ColumnType.Double, J_DefaultValue.YES) + ")";

                //strSQL = " CREATE TABLE " + strDefaultSummary_TableName + " " +
                //         " (" +
                //         "      FA_YEAR      TEXT(10)  DEFAULT \"\"," +
                //         "      QTR          TEXT(2) DEFAULT \"\"," +
                //         "      FORM_NO      TEXT(4) DEFAULT \"\"," +
                //         "      NET_PAYABLE  MONEY   DEFAULT 0 " +
                //         " )";
                //--------------------------------------------------------------
                //if (dmlService.J_ExecSql(strSQL) == false)
                //{
                //    dmlService.J_Rollback();
                //    btnPrintDefaultSummary.Select();
                //    this.Cursor = Cursors.Default;
                //    return;
                //}
                dmlService.J_ExecSql(strSQL);
                //--------------------------------------------------------------
                dmlService.J_Commit();
                //--------------------------------------------------------------
                strSQL = "";
                //--------------------------------------------------------------
                for (int i = 0; i < dtDefaultSummary.Rows.Count; i++)
                {
                    dmlService.J_BeginTransaction();
                    strSQL = "insert into " + strDefaultSummary_TableName + "(FA_YEAR, QTR, FORM_NO, NET_PAYABLE) VALUES('"
                                    + dtDefaultSummary.Rows[i]["FA Year"].ToString().Trim() + "','"
                                    + dtDefaultSummary.Rows[i]["Quarter"].ToString().Trim() + "','"
                                    + dtDefaultSummary.Rows[i]["Form Type"].ToString().Trim() + "',"
                                    + cmnService.J_ReturnDoubleValue(dtDefaultSummary.Rows[i]["Net Payable(Rounded-Off)"].ToString().Trim()) + " )";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        btnPrintDefaultSummary.Select();
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    dmlService.J_Commit();
                }
                //--------------------------------------------------------------
                strSQL = @" SELECT FA_YEAR,
                                   QTR,
                                   FORM_NO,
                                   NET_PAYABLE
                            FROM  " + strDefaultSummary_TableName + " ";

                //---------------------------------------------------------------
                //crDefaultSummaryList rptDefaultSummaryList = new crDefaultSummaryList();
                //rptcls = (ReportClass)rptDefaultSummaryList;
                //---------------------------------------------------------------
                //string[,] strArry = {{ "txtTanNo",  "TAN : " + txtTANNo.Text},
                //                     { "txtReportTitle", " Default Summary List"}};
                //-- ANIK @ 2015/09/28
                //string[,] strArry = {{ "txtTanNo",  lblTanNo.Text},
                //                     { "txtReportTitle", " Default Summary List"}};
                //---------------------------------------------------------------
                //ReportService rptService = new ReportService();
                //rptService.J_PreviewReport(ref rptcls, this, strSQL, strArry);
                string[,] strArryDefaultSummaryList = {{ "TANNo",  lblTanNo.Text},
                                     { "ReportHeader", " Default Summary List"}};
                //RptDialog rptDialog = new RptDialog();
                J_PreviewReportRDLC("\\Reports\\crDefaultSummaryList.rdlc", "DataSet1", strSQL, strArryDefaultSummaryList);
                //---------------------------------------------------------------
                if (dmlService.J_ExecSql("DROP TABLE " + strDefaultSummary_TableName) == false)
                {
                    btnPrintDefaultSummary.Select();
                    this.Cursor = Cursors.Default;
                    return;
                }
                //--------------------------------------------------------------
                this.Cursor = Cursors.Default;
                //-------------------------------------------------------------- 
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                dmlService.J_Rollback();
                this.Cursor = Cursors.Default;
                return;
            }

        }
        #endregion

        #region  btnPrintDetails_Click
        private void btnPrintDetails_Click(object sender, EventArgs e)
        {
            try
            {

                //--------------------------------------------------------------
                this.Cursor = Cursors.WaitCursor;
                //--------------------------------------------------------------
                //
                #region DETAILS
                dmlService.J_BeginTransaction();
                //--------------------------------------------------------------
                //" + cmnService.J_GetDataType("BATCH_HEADER_ID", J_Identity.YES) + @",
                //" + cmnService.J_GetDataType("ASST_ID", J_ColumnType.Integer, J_DefaultValue.YES) + @", 
                //" + cmnService.J_GetDataType("COMPANY_ID", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                //" + cmnService.J_GtDataType("FORM_NO", J_ColumnType.String, 4, J_DefaultValue.YES) + @",
                //" + cmnService.J_GetDataType("QTR", J_ColumnType.String, 4, J_DefaultValue.YES) + @",

                //if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS) == true)
                //{
                //    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS;
                //    dmlService.J_ExecSql(strSQL);
                //}
                //
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS) == false)
                {
//                    strSQL = @" CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS + @" 
//                                (" + cmnService.J_GetDataType("TEMP_DEFAULT_SUMMARY_DETAILS_ID", J_Identity.YES) + @",
//                                 " + cmnService.J_GetDataType("SRL_NO", J_ColumnType.String, 10, J_DefaultValue.YES) + @",
//                                 " + cmnService.J_GetDataType("TYPE", J_ColumnType.String, 255, J_DefaultValue.YES) + @",
//                                 " + cmnService.J_GetDataType("DEFAULT_AMT", J_ColumnType.Double, J_DefaultValue.YES) + @",
//                                 " + cmnService.J_GetDataType("STATEMENT_AMT", J_ColumnType.Double, J_DefaultValue.YES) + @",
//                                 " + cmnService.J_GetDataType("PAYABLE_AMT", J_ColumnType.Double, J_DefaultValue.YES) + @",
//                                 " + cmnService.J_GetDataType("NET_PAYABLE", J_ColumnType.Double, J_DefaultValue.YES) + @")";
                    strSQL = @" CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS + @" 
                                (" + cmnService.J_GetDataType("TEMP_DEFAULT_SUMMARY_DETAILS_ID", J_Identity.YES) + @",
                                 " + cmnService.J_GetDataType("SRL_NO", J_ColumnType.String, 10, J_DefaultValue.YES) + @",
                                 " + cmnService.J_GetDataType("TYPE", J_ColumnType.String, 255, J_DefaultValue.YES) + @",
                                 " + cmnService.J_GetDataType("DEFAULT_AMT", J_ColumnType.String, 15, J_DefaultValue.YES) + @",
                                 " + cmnService.J_GetDataType("STATEMENT_AMT", J_ColumnType.String, 15, J_DefaultValue.YES) + @",
                                 " + cmnService.J_GetDataType("PAYABLE_AMT", J_ColumnType.String, 15, J_DefaultValue.YES) + @",
                                 " + cmnService.J_GetDataType("TOTAL_PAYABLE", J_ColumnType.Double, J_DefaultValue.YES) + @",
                                 " + cmnService.J_GetDataType("NET_PAYABLE_AMT", J_ColumnType.Double,  J_DefaultValue.YES) + @")";
                    //--------------------------------------------------------------
                    dmlService.J_ExecSql(strSQL);
                    //if (dmlService.J_ExecSql(strSQL) == false)
                    //{
                    //    dmlService.J_Rollback();
                    //    btnPrintDetails.Select();
                    //    this.Cursor = Cursors.Default;
                    //    return;
                    //}
                }
                else
                {
                    strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS;
                    dmlService.J_ExecSql(strSQL);
                    //--
                    if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS, "TOTAL_PAYABLE") == false)
                    {
                        strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS, "TOTAL_PAYABLE", "MONEY", "", "", "0");
                        dmlService.J_ExecSql(strSQL);
                        //
                        strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS + " SET TOTAL_PAYABLE = 0";
                        dmlService.J_ExecSql(strSQL);
                        //
                    }
                    //--
                    if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS, "NET_PAYABLE_AMT") == false)
                    {
                        strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS, "NET_PAYABLE_AMT", "MONEY", "", "", "0");
                        dmlService.J_ExecSql(strSQL);
                        //
                        strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS + " SET NET_PAYABLE_AMT = 0";
                        dmlService.J_ExecSql(strSQL);
                        //
                    }
                    //--
                }
                //--------------------------------------------------------------
                dmlService.J_Commit();
                //--------------------------------------------------------------
                string dblDefaultAmt = "0", dblReportedAmt = "0", dblPayableAmt = "0";
                for (int i = 0; i < dtDefaultDetails.Rows.Count; i++)
                {
                    //--
                    if (Convert.ToString(dtDefaultDetails.Rows[i][2]).Trim() == "")
                        dblDefaultAmt = "0";
                    else
                        dblDefaultAmt = Convert.ToString(dtDefaultDetails.Rows[i][2]).Trim();
                    //--
                    //--
                    if (Convert.ToString(dtDefaultDetails.Rows[i][3]).Trim() == "")
                        dblReportedAmt = "0";
                    else
                        dblReportedAmt = Convert.ToString(dtDefaultDetails.Rows[i][3]).Trim();
                    //--
                    //--
                    if (Convert.ToString(dtDefaultDetails.Rows[i][4]).Trim() == "")
                        dblPayableAmt = "0";
                    else
                        dblPayableAmt = Convert.ToString(dtDefaultDetails.Rows[i][4]).Trim();
                    //--
                    dmlService.J_BeginTransaction();

                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS + "(SRL_NO, TYPE, DEFAULT_AMT, STATEMENT_AMT, PAYABLE_AMT, TOTAL_PAYABLE, NET_PAYABLE_AMT) VALUES('"
                                    + cmnService.J_ReplaceQuote(Convert.ToString(dtDefaultDetails.Rows[i][0]).Trim()) + "','"
                                    + cmnService.J_ReplaceQuote(Convert.ToString(dtDefaultDetails.Rows[i][1]).Trim()) + "','"
                                    + dblDefaultAmt + "','"
                                    + dblReportedAmt + "','"
                                    + dblPayableAmt + "',"
                                    + Convert.ToDouble(lblTotalPayable.Text) + ","
                                    + Convert.ToDouble(lblNetPayable.Text) + " )";
                    dmlService.J_ExecSql(strSQL);
                    //if (dmlService.J_ExecSql(strSQL) == false)
                    //{
                    //    btnPrintDefaultSummary.Select();
                    //    this.Cursor = Cursors.Default;
                    //    return;
                    //}
                    dmlService.J_Commit();
                }
                #endregion
                //
                #region SUMMARY
                dmlService.J_BeginTransaction();
                //--------------------------------------------------------------
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_SUMMARY) == false)
                {
                    strSQL = @" CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_SUMMARY + @" 
                                (" + cmnService.J_GetDataType("TEMP_DEFAULT_SUMMARY_SUMMARY_ID", J_Identity.YES) + @",
                                 " + cmnService.J_GetDataType("STATEMENT", J_ColumnType.String, 255, J_DefaultValue.YES) + @",
                                 " + cmnService.J_GetDataType("TOKEN_NO", J_ColumnType.String, 255, J_DefaultValue.YES) + @",
                                 " + cmnService.J_GetDataType("ORDER_PASSED_DATE", J_ColumnType.String, 255, J_DefaultValue.YES) + @")";
                    //--------------------------------------------------------------
                    dmlService.J_ExecSql(strSQL);
                    //if (dmlService.J_ExecSql(strSQL) == false)
                    //{
                    //    dmlService.J_Rollback();
                    //    btnPrintDetails.Select();
                    //    this.Cursor = Cursors.Default;
                    //    return;
                    //}
                }
                else
                {
                    strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_SUMMARY;
                    dmlService.J_ExecSql(strSQL);
                }
                //--------------------------------------------------------------
                dmlService.J_Commit();
                //--------------------------------------------------------------
                for (int i = 0; i < dtStatementSummary.Rows.Count; i++)
                {
                    //--
                    dmlService.J_BeginTransaction();
                    //
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_SUMMARY + "(STATEMENT, TOKEN_NO, ORDER_PASSED_DATE) VALUES('"
                                    + cmnService.J_ReplaceQuote(Convert.ToString(dtStatementSummary.Rows[i][0]).Trim()) + "','"
                                    + cmnService.J_ReplaceQuote(Convert.ToString(dtStatementSummary.Rows[i][1]).Trim()) + "','"
                                    + cmnService.J_ReplaceQuote(Convert.ToString(dtStatementSummary.Rows[i][2]).Trim()) + "')";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        btnPrintDefaultSummary.Select();
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    dmlService.J_Commit();
                }
                #endregion
                //
                #region PANs
                dmlService.J_BeginTransaction();
                //--------------------------------------------------------------
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_PAN) == false)
                {
                    strSQL = @" CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_PAN + @" 
                                (" + cmnService.J_GetDataType("TEMP_DEFAULT_SUMMARY_PAN_ID", J_Identity.YES) + @",
                                 " + cmnService.J_GetDataType("DED_WITHOUT_PAN", J_ColumnType.String, 255, J_DefaultValue.YES) + @",
                                 " + cmnService.J_GetDataType("DED_WITH_INVALID_PAN", J_ColumnType.String, 255, J_DefaultValue.YES) + @")";
                    //--------------------------------------------------------------
                    dmlService.J_ExecSql(strSQL);
                    //if (dmlService.J_ExecSql(strSQL) == false)
                    //{
                    //    dmlService.J_Rollback();
                    //    btnPrintDetails.Select();
                    //    this.Cursor = Cursors.Default;
                    //    return;
                    //}
                }
                else
                {
                    strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_PAN;
                    dmlService.J_ExecSql(strSQL);
                }
                //--------------------------------------------------------------
                dmlService.J_Commit();
                //--------------------------------------------------------------
                for (int i = 0; i < dtPANErrors .Rows.Count; i++)
                {
                    //--
                    dmlService.J_BeginTransaction();
                    //
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_PAN + "(DED_WITHOUT_PAN, DED_WITH_INVALID_PAN) VALUES('"
                                    + cmnService.J_ReplaceQuote(Convert.ToString(dtPANErrors.Rows[i][0]).Trim()) + "','"
                                    + cmnService.J_ReplaceQuote(Convert.ToString(dtPANErrors.Rows[i][1]).Trim()) + "')";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        btnPrintDefaultSummary.Select();
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    dmlService.J_Commit();
                }
                #endregion
                //--
                //--
                #region REPORT SQL
                //crDefaultSummaryDetails rptDefaultSummaryDetails = new crDefaultSummaryDetails();
                //rptcls = (ReportClass)rptDefaultSummaryDetails;
                //--
                string strStatementSummary = "SELECT * FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_SUMMARY + " ORDER BY TEMP_DEFAULT_SUMMARY_SUMMARY_ID";
                string strSummaryDetails = "SELECT * FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS + " ORDER BY TEMP_DEFAULT_SUMMARY_DETAILS_ID";
                string strPANSummary = "SELECT * FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_PAN;
                //rptcls.OpenSubreport("crSubSummaryDetails").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strSummaryDetails).Tables[0]);
                //rptcls.OpenSubreport("crSubRptPANSummary").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strPANSummary).Tables[0]);
                //--
                //TextObject objtxtValue;
                //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[0].ReportObjects["txtTanNo"];
                //objtxtValue.Text = lblSelectedTanNo.Text;
                //SETTING REPORT TITLE

                //string[,] strArry = {{ "txtTanNo",  lblTanNo.Text},
                //                     { "txtReportTitle", " Default Summary List"}};
                //string[,] strArry = { { "txtTanNo", lblSelectedTanNo.Text}, 
                //                      { "txtReportTitle", "Default Summary for " + lblSelectedQtr.Text + " of " + lblSelectedFaYear.Text + " for Form " + lblSelectedFormNo.Text } };
                //ReportService rptService = new ReportService();
                //rptService.J_PreviewReport(ref rptcls, this, strStatementSummary, strArry);
                #endregion
                //--
                #region REPORT RDLC

                //RDLC Report
                //string[,] strArryForm3CDDetailsReport = { {"HeaderCompanyName", cmnService.J_Left(cmbCompany.Text, cmbCompany.Text.Length - 12) },
                //                              {"HeaderTANFaYear", "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text },
                //                              { "HeaderReportName", "Form 3CD: TDS / TCS - Section wise details"}
                //                                };

                string[,] strArryDefaultDetails = { { "TANNo", lblSelectedTanNo.Text},
                                      { "ReportHeader", "Default Summary for " + lblSelectedQtr.Text + " of " + lblSelectedFaYear.Text + " for Form " + lblSelectedFormNo.Text } };
                //RptDialog rptDialog = new RptDialog();
                J_PreviewReportRDLC3DataSet("\\Reports\\crDefaultSummaryDetails.rdlc", "DataSet1", strStatementSummary, "DataSet2", strSummaryDetails, "DataSet3", strPANSummary, strArryDefaultDetails);
                #endregion
                //--
                //if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_SUMMARY) == true)
                //{
                //    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_SUMMARY;
                //    dmlService.J_ExecSql(strSQL);
                //}
                //if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS) == true)
                //{
                //    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_DETAILS;
                //    dmlService.J_ExecSql(strSQL);
                //}
                //if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_PAN) == true)
                //{
                //    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEFAULT_SUMMARY_PAN;
                //    dmlService.J_ExecSql(strSQL);
                //}
                //--------------------------------------------------------------
                this.Cursor = Cursors.Default;
                //-------------------------------------------------------------- 
            }
            catch (Exception err)
            {
                dmlService.J_Rollback();
                btnPrintDetails.Select();
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=br6kzNQrD-g");
            //V0048
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0048", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0056", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion


        #region J_PreviewReportRDLC
        public bool J_PreviewReportRDLC(string ReportPath, string DataSetName, string SQLQueryString, string[,] ReportParameterNameANDValue)
        {
            RptPreviewRDLC RptPreview = new RptPreviewRDLC();
            RptPreview.reportViewer1.ProcessingMode = ProcessingMode.Local;
            //RptPreview.reportViewer1.LocalReport.ReportPath = (Application.StartupPath + "\\Reports\\crCompanyList.rdlc");
            RptPreview.reportViewer1.LocalReport.ReportPath = (Application.StartupPath + ReportPath);
            //
            DataSet dsDataPrint = new DataSet();
            dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SQLQueryString);
            //
            if (dsDataPrint.Tables[0].Rows.Count > 0)
            {
                if (ReportParameterNameANDValue != null)
                {
                    for (int iCounter = 0; iCounter <= ReportParameterNameANDValue.GetUpperBound(0); iCounter++)
                    {
                        ReportParameter rptParameter = new ReportParameter(ReportParameterNameANDValue[iCounter, 0], ReportParameterNameANDValue[iCounter, 1]);
                        RptPreview.reportViewer1.LocalReport.SetParameters(rptParameter);
                    }
                }
                //
                ReportDataSource rds = new ReportDataSource(DataSetName.ToString(), dsDataPrint.Tables[0]);
                RptPreview.reportViewer1.LocalReport.DataSources.Clear();
                RptPreview.reportViewer1.LocalReport.Refresh();
                RptPreview.reportViewer1.LocalReport.DataSources.Add(rds);
                RptPreview.reportViewer1.RefreshReport();
                RptPreview.MdiParent = this.MdiParent;
                RptPreview.Show();
                //RptPreview.reportViewer1.PrintingBegin();
                //RptPreview.reportViewer1.DisplayMode = DisplayMode.PrintLayout;
                return true;
            }
            else
            {
                cmnService.J_UserMessage("Record not found.\nPreview not available");
                return false;
            }

        }
        #endregion


        #region J_PreviewReportRDLC3DataSet
        public bool J_PreviewReportRDLC3DataSet(string ReportPath, string DataSetName1, string SQLQueryString1, string DataSetName2, string SQLQueryString2, string DataSetName3, string SQLQueryString3, string[,] ReportParameterNameANDValue)
        {
            RptPreviewRDLC RptPreview = new RptPreviewRDLC();
            RptPreview.reportViewer1.ProcessingMode = ProcessingMode.Local;
            //RptPreview.reportViewer1.LocalReport.ReportPath = (Application.StartupPath + "\\Reports\\crCompanyList.rdlc");
            RptPreview.reportViewer1.LocalReport.ReportPath = (Application.StartupPath + ReportPath);
            //
            DataSet dsDataPrint = new DataSet();
            dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SQLQueryString1);
            //
            DataSet dsDataPrint2 = new DataSet();
            dsDataPrint2 = dmlService.J_ExecSqlReturnDataSet(SQLQueryString2);
            //
            DataSet dsDataPrint3 = new DataSet();
            dsDataPrint3 = dmlService.J_ExecSqlReturnDataSet(SQLQueryString3);
            //
            if (dsDataPrint.Tables[0].Rows.Count > 0)
            {
                if (ReportParameterNameANDValue != null)
                {
                    for (int iCounter = 0; iCounter <= ReportParameterNameANDValue.GetUpperBound(0); iCounter++)
                    {
                        ReportParameter rptParameter = new ReportParameter(ReportParameterNameANDValue[iCounter, 0], ReportParameterNameANDValue[iCounter, 1]);
                        RptPreview.reportViewer1.LocalReport.SetParameters(rptParameter);
                    }
                }
                //
                ReportDataSource rds1 = new ReportDataSource(DataSetName1.ToString(), dsDataPrint.Tables[0]);
                RptPreview.reportViewer1.LocalReport.DataSources.Clear();
                RptPreview.reportViewer1.LocalReport.Refresh();
                //
                ReportDataSource rds2 = new ReportDataSource(DataSetName2.ToString(), dsDataPrint2.Tables[0]);
                RptPreview.reportViewer1.LocalReport.DataSources.Clear();
                RptPreview.reportViewer1.LocalReport.Refresh();
                RptPreview.reportViewer1.LocalReport.Refresh();
                //
                ReportDataSource rds3 = new ReportDataSource(DataSetName3.ToString(), dsDataPrint3.Tables[0]);
                RptPreview.reportViewer1.LocalReport.DataSources.Clear();
                RptPreview.reportViewer1.LocalReport.Refresh();
                //
                RptPreview.reportViewer1.LocalReport.DataSources.Add(rds1);
                RptPreview.reportViewer1.LocalReport.DataSources.Add(rds2);
                RptPreview.reportViewer1.LocalReport.DataSources.Add(rds3);
                //
                RptPreview.reportViewer1.RefreshReport();
                RptPreview.MdiParent = this.MdiParent;
                RptPreview.Show();
                return true;
            }
            else
            {
                cmnService.J_UserMessage("Record not found.\nPreview not available");
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

