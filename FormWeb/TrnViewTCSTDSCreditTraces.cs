
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
    public partial class TrnViewTCSTDSCreditTraces : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnViewTCSTDSCreditTraces()
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

        DMLService dmlService = new DMLService();

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();

        enum enmRequestType
        {
            Login,
            DeductionDetails,
            LogOff
        }

        int intRowIndex = 0;
        int intColumnIndex = 0;
        private string CurrentCaptchaId = "";

        #endregion

        //-- Added By Abhishek Dey On 22/08/2018 --
        #region TrnViewTCSTDSCreditTraces_Activated
        private void TrnViewTCSTDSCreditTraces_Activated(object sender, EventArgs e)
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


        #region TrnStatementStatusTraces_Load
        private void TrnStatementStatusTraces_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            //InitializeCaptcha();
            picCaptcha.Image = Properties.Resources.captcha_loading;
            if (!bgWorkerLoadCaptcha.IsBusy)
                bgWorkerLoadCaptcha.RunWorkerAsync();
            //--

            lblTitle.Text = "View Deduction Details for Deductee";
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

        #region btnGo_Click
        private void btnGo_Click(object sender, EventArgs e)
        {
            ClearValues();
            if (!Validates()) return;
            //------------------------------------------------

            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.DeductionDetails);

            TracesData objData = new TracesData();
            objData.PAN1 = txtPAN.Text.Trim();
            objData.FAYear = cmbFAYear.Text.Substring(0, cmbFAYear.Text.IndexOf("-")); ;
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
                    //---------------------------------------------------
                    break;
                // LIST OF STATEMENT STATUS FILES
                case enmRequestType.DeductionDetails:
                    TracesResponse response = objAccount.RequestForDeductionDetailsForDeductee_New((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.DeductionDetails);
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
                    case enmRequestType.Login:
                        //pgTimer.Stop();
                        //pgTimer.Interval = 1000;
                        //pBar.Value = 99;
                        //---------------------------------------------------
                        if (objResponse.Respons == enmResponse.Success)                        
                            ShowHideLoginDetails(enmRequestType.DeductionDetails);


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

                    case enmRequestType.DeductionDetails:

                        this.pgTimer.Stop();
                        //this.pgTimer.Interval = 1000;
                        pBar.Value = 100;

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

                        //-------------------------------------------------------
                        ArrayList objList = (ArrayList)objResponse.CustomeTypes;
                        Deductor objDeductor = (Deductor)objList[0];
                        DataTable dTable = (DataTable)objList[1];

                        PopulateDatagridView(dTable);
                        //---------------------------------------------
                        //if (objDeductor.NoOfDetailsRecord > 0)
                        //{
                            lblTAN.Text = objDeductor.TAN;
                            lblAssesmentYear.Text = objDeductor.AssessmentYear;
                            lblFormType.Text = cmbFormNo.Text;
                            lblQuarter.Text = cmbQtr.Text;
                            lblTokenNumber.Text = objDeductor.RegularStatement;
                            lblLatestCorrectionStatement.Text = objDeductor.CorrectionStatement;
                            lblPAN.Text = txtPAN.Text;
                            lblNameOfDeductee.Text = objDeductor.Deductee;

                        //}
                        //else
                        //{
                        //    lblTAN.Text = "";
                        //    lblAssesmentYear.Text = "";
                        //    lblFormType.Text = "";
                        //    lblQuarter.Text = "";
                        //    lblTokenNumber.Text = "";
                        //    lblLatestCorrectionStatement.Text = "";
                        //    lblPAN.Text = "";
                        //    lblNameOfDeductee.Text = "";
                        //}
                        //----------------------------------------------
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
            catch(Exception err)
            {
                cmnService.J_UserMessage(err.Message,MessageBoxIcon.Error);
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
                        //lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) + 
                        //                                          drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15) + 
                        //                                          drdShowDeducteeHelp["USER_PASSWORD"]));
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

        #region PopulateDatagridView
        void PopulateDatagridView(DataTable dsRecords)
        {
            // DATA BIND TO GRIDVIEW CONTROL
            dgvDeducteeDetails.Columns.Clear();
            dgvDeducteeDetails.DataSource = null;
            dgvDeducteeDetails.DataSource = dsRecords;
            //---------------------------------------
            //CHECKING IF RECORD EXISTS OR NOT
            //if(dsRecords.Rows.Count <= 0)
            //{
            //    cmnService.J_UserMessage("Records not found !!");
            //}
            //---------------------------------------

            dgvDeducteeDetails.Columns[3].DefaultCellStyle.Format = "0.00";
            dgvDeducteeDetails.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDeducteeDetails.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //
            foreach (DataGridViewRow row in dgvDeducteeDetails.Rows)
            {
                row.Cells[5].Value = string.Format("{0:0.00}", Convert.ToDouble(row.Cells[5].Value.ToString()));
                row.Cells[7].Value = string.Format("{0:0.00}", Convert.ToDouble(row.Cells[7].Value.ToString()));
            }
            dgvDeducteeDetails.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDeducteeDetails.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //
            dgvDeducteeDetails.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDeducteeDetails.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDeducteeDetails.Columns[1].Width = 65;
            dgvDeducteeDetails.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
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
                   // grpProgress.Visible = true;
                    InitializeCaptcha();
                    break;
                case enmRequestType.DeductionDetails:
                    txtPAN.Text = "";
                    cmbFAYear.SelectedIndex = 0;
                    cmbFormNo.SelectedIndex = 0;
                    cmbQtr.SelectedIndex = 0;
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                   // grpProgress.Visible = false;
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;
                    ClearValues();
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
            string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQtr);
            //-- FORM NO.
            string[] strFormNo ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);
      //---------------------------------------------------------------
            ClearValues();
          
        }
        #endregion

        #region ClearValues
        public void ClearValues()
        {
            //------------------------------------------------------------------------
            lblTAN.Text = "";
            lblAssesmentYear.Text = "";
            lblFormType.Text = "";
            lblQuarter.Text = "";
            lblTokenNumber.Text = "";
            lblLatestCorrectionStatement.Text = "";
            lblPAN.Text = "";
            lblNameOfDeductee.Text = "";
            //----------------------------------------------------------------------
            dgvDeducteeDetails.DataSource = null;
        }

        #endregion
               

        #region Validates
        private bool Validates()
        {
            if(string.IsNullOrEmpty(txtPAN.Text.Trim()))
            {
                cmnService.J_UserMessage("Enter PAN No");
                txtPAN.Focus();
                return false;
            }
            if (cmbFAYear.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Select Financial Year");
                cmbFAYear.Focus();
                return false; ;
            }
            if (cmbFormNo.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Select Form No");
                cmbFormNo.Focus();
                return false;
            }
            if (cmbQtr.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Select Quarter");
                cmbQtr.Focus();
                return false;
            }

            return true;
        }


















        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0059", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }
    }       

}

