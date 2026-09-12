
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

namespace TDSMAN.FormTrn
{
    public partial class TrnCertificate197 : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnCertificate197(string CertificateNo, string PANNo, string FaYear)
        {
            InitializeComponent();
            //
            strCertificateNo = CertificateNo;
            strPANNo = PANNo;
            strFaYear = FaYear;
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        #region Objects & Variables declaration

        TracesConnect objAccount = new TracesConnect();
        Certificate197Data objData = new Certificate197Data();
        TracesLogin objLogin = new TracesLogin();


        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();

        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        enum enmRequestType
        {
            Login,
            List,            
            LogOff
        }

        int intRowIndex = 0;
        int intColumnIndex = 0;
        string strCertificateNo = "", strPANNo = "", strFaYear = "";

        private string CurrentCaptchaId = "";
        #endregion

        //-- Added By Abhishek Dey On 22/08/2018 --
        #region TrnCertificate197_Activated
        private void TrnCertificate197_Activated(object sender, EventArgs e)
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

        #region TrnViewReturnStatusOnline_Load
        private void TrnViewReturnStatusOnline_Load(object sender, EventArgs e)
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

            lblTitle.Text = "Validate 197 Certificate";            
            //            
            grpDownloadList.Visible = false;
            grpLoginDetails.Visible = true;
            grpProgress.Visible = true;
            //For Showing the helg grid when tan text is changed
            blnShowHelp = true;
            //
            ClearControls();
            //
            txtTAN.Select();
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
                
                //--
                if (!ValidateFields()) return;
                //--
                //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER 

                strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "' ";
                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

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
                //#################
                //--
                objLogin.UserID = txtUserId.Text;
                objLogin.Password = txtPassword.Text;
                objLogin.TAN = txtTAN.Text;
                objLogin.CaptchaCode = txtCaptchaCode.Text;
                objLogin.CaptchaId = this.CurrentCaptchaId; //-- 2026/04/08
                //----------------------------------------              
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
                    //objResponse = objAccount.makeLoginToTRACES((TracesLogin)objList[1]);
                    objResponse = objAccount.makeLoginToTraces_New((TracesLogin)objList[1]);
                    //---------------------------------------------------
                    objRetval.Add(enmRequestType.Login);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    //---------------------------------------------------
                    break;

                // LIST OF DOWNLOAD FILES
                case enmRequestType.List:
                  
                    //TracesResponse response = objAccount.RequestForCertificate197(objData);
                    TracesResponse response = objAccount.RequestForCertificate197_New(objData);
                    objRetval.Add(enmRequestType.List);
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
                        this.pgTimer.Stop();                      
                        pBar.Value = 100;
                        if (objResponse.Respons == enmResponse.Success)                        
                            ShowHideLoginDetails(enmRequestType.List);                        
                        else
                            ShowHideLoginDetails(enmRequestType.LogOff);   
                        break;

                    case enmRequestType.List:

                        this.pgTimer.Stop();
                        //this.pgTimer.Interval = 1000;
                        pBar.Value = 100;
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            ArrayList objMesg = (ArrayList)objResponse.CustomeTypes;
                            if (Convert.ToInt32(objMesg[0]) > 0)
                            {
                                DataTable dTable = (DataTable)objMesg[1];
                                PopulateDatagridView(dTable);
                                //ShowHideLoginDetails(enmRequestType.List);
                            }
                            else
                            {
                                //InitializeCaptcha();
                                cmnService.J_UserMessage("No data available for the specified search criteria");
                            }
                        }
                        else
                        {
                            cmnService.J_UserMessage(objResponse.Message);                            
                            ShowHideLoginDetails(enmRequestType.LogOff);
                            InitializeCaptcha();
                        }
                        break;
                    //case enmRequestType.Download:
                    //    pgTimerGrid.Stop();
                    //    dgvCertificateList.Rows[intRowIndex].Cells[9].Value = 100;

                    //    if (objResponse.Respons == enmResponse.Success)
                    //        cmnService.J_UserMessage("Download Completed");
                    //    else
                    //    {
                    //        cmnService.J_UserMessage("Download Failed :: " + objResponse.Message);
                    //        //ShowHideLoginDetails(enmRequestType.Login);
                    //    }

                    //    break;

                    case enmRequestType.LogOff:
                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        txtUserId.Text = "";
                        txtPassword.Text = "";
                        txtTAN.Text = "";
                        txtCaptchaCode.Text = "";
                        grpDownloadList.Visible = false;
                        grpLoginDetails.Visible = true;
                        grpProgress.Visible = true;
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

        #region pgTimerGrid_Tick
        private void pgTimerGrid_Tick(object sender, EventArgs e)
        {
            int intValue = Convert.ToInt32(Convert.ToString(dgvCertificateList.Rows[intRowIndex].Cells[9].Value) == "" ? "0" : dgvCertificateList.Rows[intRowIndex].Cells[9].Value);
            if (intValue == 100) intValue = 0;

            // Slow down
            this.pgTimerGrid.Interval = (this.pgTimerGrid.Interval * 2);

            //Update progress bar
            if ((intValue + 1) > 100)
            {
                dgvCertificateList.Rows[intRowIndex].Cells[9].Value = 100;
            }
            else
            {
                intValue += 1;
                dgvCertificateList.Rows[intRowIndex].Cells[9].Value = intValue;
            }
        }

        #endregion

        #region dgvDownloadDetails_CellClick
        private void dgvDownloadList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
                        lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) +
                                                                  drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15) +
                                                                  TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdShowDeducteeHelp["USER_PASSWORD"].ToString()).PadRight(10) +
                                                                  drdShowDeducteeHelp["COMPANY_NAME"].ToString(),
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
            if (txtTAN.Text.Trim() == "") return;
            //INITIALIZE CAPTCHA CODE
            //InitializeCaptcha();

        }
        #endregion

        #region btnGo_Click
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCertificateNo.Text.Trim()))
            {
                cmnService.J_UserMessage("Please enter Certificate No.");
                txtCertificateNo.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtPAN.Text.Trim()))
            {
                cmnService.J_UserMessage("Please enter PAN");
                txtPAN.Select();
                return;
            }
            else
            {
                if (!TracesValidation.IsValidPAN(txtPAN.Text))
                {
                    cmnService.J_UserMessage("Please enter a Valid PAN");
                    txtPAN.Select();
                    return;
                }
            }

            // ------------------------
            // -- FINANCIAL YEAR
            // ------------------------

            if (cmbFAYear.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Financial year of the return for which the TDS file has to be downloaded");
                cmbFAYear.Select();
                return;
            }
            dgvCertificateList.DataSource = null;
            //=======================================================
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.List);

            objData.CertificateNo = txtCertificateNo.Text;
            objData.PAN = txtPAN.Text;
            if (cmbFAYear.SelectedIndex > 0)
                objData.FinYear = cmbFAYear.Text.Substring(0, cmbFAYear.Text.IndexOf("-"));
            //--------------------------------------------
            pgTimer.Start();
            //-------------------------------------------
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync(objList);
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
            //string strlstDeducteeHelp = lstDeducteeHelp.Text;
            //txtTANNo.Text = cmnService.J_Left(strlstDeducteeHelp, 10);
            //txtUserID.Text = cmnService.J_Mid(strlstDeducteeHelp, strlstDeducteeHelp.IndexOf(' '),
            //                                  strlstDeducteeHelp.LastIndexOf(' ') - strlstDeducteeHelp.IndexOf(' ')).Trim();
            //txtPassword.Text = cmnService.J_Right(strlstDeducteeHelp, strlstDeducteeHelp.Length - strlstDeducteeHelp.LastIndexOf(' ')).Trim();
            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstDeducteeHelp, lstDeducteeHelp.SelectedIndex));
            txtTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtUserId.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            //--
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
                txtCaptchaCode.Text = "";
                picCaptcha.Image = Properties.Resources.captcha_loading_failed;
            }
        }

        #endregion

        #region ValidateFields
        bool ValidateFields()
        {
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connectivity not found");
                BtnExit.Select();
                return false;
            }

            if (string.IsNullOrEmpty(txtTAN.Text.Trim() ))
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

            //if (string.IsNullOrEmpty(txtUserId.Text.Trim()))
            //{
            //    cmnService.J_UserMessage("Please enter the User Id for TAN No. -" + txtTAN.Text.Trim());
            //    txtUserId.Select();
            //    return false;
            //}

            // ------------------------
            // -- PASSWORD
            // ------------------------

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                cmnService.J_UserMessage("Please enter the Password for TAN No. -" + txtTAN.Text.Trim());
                txtPassword.Select();
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
            dgvCertificateList.Columns.Clear();
            dgvCertificateList.DataSource = null;
            dgvCertificateList.DataSource = dsRecords;
            // CREATE DOWNLOAD BUTTON & PROGRESSBAR CONTROL COLUMNS 
            //------------------------------------------------------------
            dgvCertificateList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCertificateList.Columns[0].Width = 35;

            dgvCertificateList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvCertificateList.Columns[1].Width = 85;

            dgvCertificateList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
           // dgvCertificateList.Columns[2].HeaderText = "Tax Year";
            //dgvCertificateList.Columns[2].Width = 80;

            dgvCertificateList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvCertificateList.Columns[3].Width = 50;

            dgvCertificateList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCertificateList.Columns[4].Width = 220;

            dgvCertificateList.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCertificateList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCertificateList.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            #region COMMENT
            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            //dgvCertificateList.Columns.Add(btn);
            //btn.HeaderText = "";
            //btn.Text = "Download";
            //btn.Name = "btnDownload";
            ////------------------------------------------------------------
            //btn.UseColumnTextForButtonValue = true;
            ////------------------------------------------------------------
            //DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            //dgvCertificateList.Columns.Add(prg);
            //prg.Name = "Progressbar";
            //prg.ProgressBarColor = Color.LightGreen;
            /* ------------------------------------------------------------
               REMOVE DOWNLOAD BUTTON
            //------------------------------------------------------------ */
            //foreach (DataGridViewRow gridRow in dgvCertificateList.Rows)
            //{
            //    DataGridViewCell BookButtonCell = gridRow.Cells["Status"];
            //    DataGridViewButtonCell EndDateCell = (DataGridViewButtonCell)gridRow.Cells["btnDownload"];

            //    if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "NOT AVAILABLE" || Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "SUBMITTED")
            //    {
            //        DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
            //        gridRow.Cells["btnDownload"] = new DataGridViewTextBoxCell();
            //        //gridRow.Cells["btnDownload"].Value = "........oooops";
            //    }
            //}
            #endregion
            //------------------------------------------------------------
        }

        #endregion
              
        #region ShowHideLoginDetails
        void ShowHideLoginDetails(enmRequestType enmStatus)
        {
            switch (enmStatus)
            {
                case enmRequestType.List:

                    dgvCertificateList.DataSource = null;
                    txtCertificateNo.Text = "";
                    txtPAN.Text = "";
                    cmbFAYear.SelectedIndex = 0;
                    //
                    txtCertificateNo.Text = strCertificateNo;
                    txtPAN.Text = strPANNo;
                    cmbFAYear.Text = strFaYear;
                    //
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                    //grpProgress.Visible = false;
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
            txtTAN.Text = "";
            txtUserId.Text = "";
            txtPassword.Text = "";
            /* ----------------------------
               POPULATE FORM COMBO BOXES
               FINANCIAL YEAR
               ---------------------------- */
            txtCertificateNo.Text = "";
            txtPAN.Text = "";
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFAYear, J_ComboBoxSelectedIndex.YES) == false) return;
            

        }
        #endregion


        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=hZ5-CFKNGPU");
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0004", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));            
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0060", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion
    }
}

