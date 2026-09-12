
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
    public partial class TrnChallanStatusTraces : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnChallanStatusTraces()
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
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();

        enum enmRequestType
        {
            Login,
            CINPP,
            CINParticulars,
            ConsumptionDetails,
            LogOff,
            BINPP,
            BINParticulars,
            BINPP_Details
        }

        string strMessage = "";
        int intRowIndex = 0;
        int intColumnIndex = 0;
        private string CurrentCaptchaId = "";
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

        #region TrnChallanStatusTraces_Activated
        private void TrnChallanStatusTraces_Activated(object sender, EventArgs e)
        {
            //-- Added By Abhishek Dey On 21/08/2018 --
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //-----------------------------------------
        }
        #endregion

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

            lblTitle.Text = "Challan Status Query";
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

                var captcha = objAccount.MakeInitialRequest_NEW();
                this.CurrentCaptchaId = captcha.CaptchaId;
                Image captchaImage = captcha.CaptchaImage;
                this.picCaptcha.Image = captchaImage;
                //this.picCaptcha.Image = img;
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
        void PopulateDatagridView(DataTable dsRecords, enmRequestType enmRecType)
        {
            // DATA BIND TO GRIDVIEW CONTROL
            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = null;
            dgvStatementList.DataSource = dsRecords;
            //---------------------------------------
            //CHECKING IF RECORD EXISTS OR NOT

            if (dsRecords.Rows.Count <= 0)
            {
                cmnService.J_UserMessage("No data available for the specified search criteria");
            }
            //---------------------------------------

            // CREATE DOWNLOAD BUTTON & PROGRESSBAR CONTROL COLUMNS 
            //------------------------------------------------------------
            if (enmRecType == enmRequestType.CINPP || enmRecType == enmRequestType.CINParticulars)
            {
                dgvStatementList.Columns[0].Width = 0;
                dgvStatementList.Columns[0].Visible = false;
                dgvStatementList.Columns[0].ReadOnly = true;
                dgvStatementList.Columns[1].Width = 0;
                dgvStatementList.Columns[1].Visible = false;
                dgvStatementList.Columns[1].ReadOnly = true;
                dgvStatementList.Columns[2].Width = 130;
                dgvStatementList.Columns[2].ReadOnly = true;
                dgvStatementList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvStatementList.Columns[3].Width = 150;
                dgvStatementList.Columns[3].ReadOnly = true; 
                dgvStatementList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvStatementList.Columns[4].ReadOnly = true;
                dgvStatementList.Columns[5].ReadOnly = true;
                dgvStatementList.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatementList.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatementList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;         
                dgvStatementList.Columns[6].ReadOnly = false;


                
            }

            if (enmRecType == enmRequestType.BINPP)
            {
                dgvStatementList.Columns[0].Width = 150;
                dgvStatementList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvStatementList.Columns[1].Width = 150;
                dgvStatementList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dgvStatementList.Columns[3].Visible = false;
                dgvStatementList.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatementList.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatementList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvStatementList.Columns[4].Visible = false;
                dgvStatementList.Columns[5].Visible = false;

                dgvStatementList.Columns[1].ReadOnly = true;
                dgvStatementList.Columns[2].ReadOnly = true;
                dgvStatementList.Columns[3].ReadOnly = false;
                dgvStatementList.Columns[4].ReadOnly = true;
                dgvStatementList.Columns[5].ReadOnly = true;                          
                               
                
            }
                                   
           
            DataGridViewLinkColumn btn = new DataGridViewLinkColumn();
            dgvStatementList.Columns.Add(btn);
            btn.HeaderText = "";
            //btn.Text = "View Details";
            btn.Text = "Match Challan Amount";
            btn.Name = "lnkDetails";
            btn.Width = 150;
            //------------------------------------------------------------
            btn.UseColumnTextForLinkValue = true;
            //------------------------------------------------------------
            DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            dgvStatementList.Columns.Add(prg);
            prg.Name = "";
            prg.ProgressBarColor = Color.LightGreen;
            //------------------------------------------------------------
            // SET FOCUS 
            if (dsRecords.Rows.Count > 0)
            {
                if (enmRecType == enmRequestType.CINPP || enmRecType == enmRequestType.CINParticulars)
                {
                    dgvStatementList.Rows[0].Cells[6].Selected = true;

                    dgvStatementList.CurrentCell = dgvStatementList.Rows[0].Cells[6];
                    dgvStatementList.BeginEdit(true);
                }
                else
                {
                    dgvStatementList.Rows[0].Cells[3].Selected = true;

                    dgvStatementList.CurrentCell = dgvStatementList.Rows[0].Cells[3];
                    dgvStatementList.BeginEdit(true);
                }
            }


            //-- ARUP @ 2014/09/16
            //foreach (DataGridViewRow gridRow in dgvStatementList.Rows)
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
                case enmRequestType.CINPP:
                    ClearControls();
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                    // grpProgress.Visible = false;
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;
                    rbnCINSearch.Checked = true;
                    rbnPeriodofPayment.Checked = true;
                    gbCINpp.Visible = true;
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

            dgvConsumption.Columns.Clear();
            dgvConsumption.DataSource = null;

            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = null;

            //For Showing the helg grid when tan text is changed
            blnShowHelp = true;
            //
            txtTANNo.Select();

            // ----------------------------
            // -- POPULATE FORM COMBO BOXES
            // ----------------------------           
            //-- CHALLAN STATUS
            string[] strQtr ={ "All", "Claimed", "Unclaimed" };
            dmlService.J_PopulateComboBox(strQtr, ref cmbChallanStatus);
            // ----------------------------
            cmbChallanStatus.SelectedIndex = 1;
            txtBSRCode.Text = "";
            txtChallanSerialNo.Text = "";
            txtChallanAmount.Text = "0.00";
            //------------------------------
            mskChallanFromDate.Text = "";
            mskChallanToDate.Text = "";
            mskChallanDate.Text = "";
            //---------------------------------------------------
            txtReceiptNumber.Text = "";
            txtDDOSerialNumber.Text = "";
            txtBinTransferVoucherAmount.Text = "0.00";
            mskTransferVoucherDate.Text = "";
            //--------------------------------
            string[] strQtr1 ={ "All", "Claimed", "Unclaimed" };
            dmlService.J_PopulateComboBox(strQtr, ref cmbTransferStatus);
            cmbTransferStatus.SelectedIndex = 1;
            mskTransferVoucherFromDate.Text = "";
            mskTransferVoucherToDate.Text = "";
            //--------------------------------


        }
        #endregion



        #region btnSearchOpt2Go_Click
        private void btnSearchOpt2Go_Click(object sender, EventArgs e)
        {


        }

        #endregion

        #region dgvStatementList_CellClick
        private void dgvStatementList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //GETTING ROW & COLUMN INDEX OF SELECT ROW
            intRowIndex = e.RowIndex;
            intColumnIndex = e.ColumnIndex;
            //-------------------------------------------------------------------------------
            if (e.ColumnIndex == dgvStatementList.Columns["lnkDetails"].Index && e.RowIndex >= 0)
            {
                //if (Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "view details")
               if (Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "match challan amount" &&
                    Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[4].Value).ToUpper() != "UNCLAIMED")
                {
                    //SEARCH ON CIN
                    if (rbnCINSearch.Checked)
                    {
                        dgvConsumption.Columns.Clear();
                        dgvConsumption.DataSource = null;
                        //--------------------------------

                        //  VALIDATION AMOUNT - update on 08/09/2016
                        string strAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value) == "" ? "0.00" : Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value);

                        if (!cmnService.J_IsNumeric(strAmount))
                        {
                            cmnService.J_UserMessage("Please Enter Challan Amount");
                            dgvStatementList.Rows[e.RowIndex].Cells[6].Selected = true;

                            dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[6];
                            dgvStatementList.BeginEdit(true);
                            return;
                        }
                        else
                        {
                            if (Convert.ToDouble(strAmount) <= 0)
                            {
                                cmnService.J_UserMessage("Please Enter Challan Amount");
                                dgvStatementList.Rows[e.RowIndex].Cells[6].Selected = true;

                                dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[6];
                                dgvStatementList.BeginEdit(true);

                                return;
                            }
                        }


                        dgvStatementList.Rows[e.RowIndex].Cells[6].Value = string.Format("{0:0.00}", Convert.ToDouble(strAmount));

                        
                        ArrayList objList = new ArrayList();
                        TracesData objData = new TracesData();
                        //---------------------------------
                        objData.BSRCode = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[0].Value) + Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[1].Value);
                        objData.FromChallanDepositDate = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[2].Value);
                        objData.ChallanSerialNo = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value);
                        objData.ChallanAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value) ;
                        objData.PRN_NO = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[5].Value);
                        //-- 2025/10/28 for unlaimed/claimed
                        objData.ChallanClaimedUncliamedStatus = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[4].Value);
                        //---------------------------------------------


                        pgTimerGrid.Start();
                        objList.Add(enmRequestType.ConsumptionDetails);
                        objList.Add(objData);

                        if (!bgWorker.IsBusy)
                            bgWorker.RunWorkerAsync(objList);

                    }
                    //-------------------------------------------------------------------------------
                    //FOR BIN SEARCH
                    if (rbnBINSearch.Checked)
                    {
                        dgvConsumption.Columns.Clear();
                        dgvConsumption.DataSource = null;
                        //-------------------------------

                        //  VALIDATION AMOUNT - update on 08/09/2016
                        string strAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value) == "" ? "0.00" : Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value);

                        if (!cmnService.J_IsNumeric(strAmount))
                        {
                            cmnService.J_UserMessage("Please Enter Amount");
                            dgvStatementList.Rows[e.RowIndex].Cells[3].Selected = true;

                            dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[3];
                            dgvStatementList.BeginEdit(true);
                            return;
                        }
                        else
                        {
                            if (Convert.ToDouble(strAmount) <= 0)
                            {
                                cmnService.J_UserMessage("Please Enter Amount");
                                dgvStatementList.Rows[e.RowIndex].Cells[3].Selected = true;

                                dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[3];
                                dgvStatementList.BeginEdit(true);

                                return;
                            }
                        }


                        dgvStatementList.Rows[e.RowIndex].Cells[3].Value = string.Format("{0:0.00}", Convert.ToDouble(strAmount));


                        pgTimerGrid.Start();
                        ArrayList objList = new ArrayList();
                        List<string> strParam = new List<string>();

                        //RECORD_ID
                        strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[5].Value));
                        //RECEIPT NO
                        strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[4].Value));
                        //DDOS NO
                        strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[1].Value));
                        //CHALLAN AMOUNT
                        strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value));
                        //DATE OF DEPOSIT
                        strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[0].Value));


                        objList.Add(enmRequestType.BINPP_Details);
                        objList.Add(strParam);

                        if (!bgWorker.IsBusy)
                            bgWorker.RunWorkerAsync(objList);

                    }

                }
            }
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
                // LIST OF CHALLAN ENQUIRY CIN - Period Payment 
                case enmRequestType.CINPP:
                    //TracesResponse response = objAccount.CIN_Period_Payment((TracesData)objList[1]);
                    TracesResponse response = objAccount.CIN_Period_Payment_New((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.CINPP);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;
                // LIST OF CHALLAN ENQUIRY CIN - CIN/BIN Particulars
                case enmRequestType.CINParticulars:
                    //response = objAccount.CIN_CIN_BINParticulars((TracesData)objList[1]);
                    response = objAccount.CIN_CIN_BINParticulars_New((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.CINParticulars);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                case enmRequestType.BINPP:
                    //response = objAccount.BIN_Period_Payment((TracesData)objList[1]);
                    response = objAccount.BIN_Period_Payment_New((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.BINPP);
                    objRetval.Add(response);
                    e.Result = objRetval;

                    break;

                case enmRequestType.BINParticulars:
                    //response = objAccount.BIN_Particulars((TracesData)objList[1]);
                    response = objAccount.BIN_Particulars_New((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.BINPP);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;


                // VIEW CONSUMPTION DETAILS
                case enmRequestType.ConsumptionDetails:

                    response = objAccount.RequestForConsumptionDetailsCIN((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.ConsumptionDetails);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                // VIEW CONSUMPTION DETAILS
                case enmRequestType.BINPP_Details:

                    response = objAccount.RequestForBIN_Details((List<string>)objList[1]);
                    objRetval.Add(enmRequestType.BINPP_Details);
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
                            ShowHideLoginDetails(enmRequestType.CINPP);

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

                    case enmRequestType.CINPP:
                    case enmRequestType.CINParticulars:
                        this.pgTimer.Stop();
                        pBar.Value = 100;
                        //-------------------------------------------------------
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

                        //--------------------------------------------------------
                        DataTable dTable = (DataTable)objResponse.CustomeTypes;

                        PopulateDatagridView(dTable, enmReqType);
                        break;

                    case enmRequestType.BINPP:
                    case enmRequestType.BINParticulars:
                        this.pgTimer.Stop();
                        pBar.Value = 100;
                        //-------------------------------------------------------
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

                        //--------------------------------------------------------
                        dTable = (DataTable)objResponse.CustomeTypes;

                        PopulateDatagridView(dTable, enmReqType);
                        break;

                    case enmRequestType.BINPP_Details:
                        pgTimerGrid.Stop();
                        dgvStatementList.Rows[intRowIndex].Cells[7].Value = 100;
                        //-----------------------------------------------------
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.LogOff);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            cmnService.J_UserMessage(objResponse.Message);

                            dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex - 3].Selected = true;
                            dgvStatementList.CurrentCell = dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex - 3];
                            dgvStatementList.BeginEdit(true);
                            return;
                        }
                        //-----------------------------------------------------
                        dTable = (DataTable)objResponse.CustomeTypes;
                        //------------------------------------------------------
                        dgvConsumption.Columns.Clear();
                        dgvConsumption.DataSource = null;
                        if (dTable.Rows.Count <= 0)
                        {
                            cmnService.J_UserMessage("No Data Available. Please check after 3 working days from the date of filing of the statement at TIN-FC");
                             return;
                        }
                        dgvConsumption.DataSource = dTable;
                        dgvConsumption.Columns[0].Width = 110;
                        dgvConsumption.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvConsumption.Columns[1].Width = 110;
                        dgvConsumption.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvConsumption.Columns[2].Width = 60;
                        dgvConsumption.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvConsumption.Columns[3].Width = 90;
                        dgvConsumption.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvConsumption.Columns[6].Width = 150;
                        dgvConsumption.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvConsumption.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvConsumption.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvConsumption.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvConsumption.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvConsumption.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvConsumption.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvConsumption.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dgvConsumption.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvConsumption.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvConsumption.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvConsumption.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                        break;

                    case enmRequestType.ConsumptionDetails:
                        pgTimerGrid.Stop();
                        dgvStatementList.Rows[intRowIndex].Cells[8].Value = 100;
                        //-----------------------------------------------------
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.LogOff);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            cmnService.J_UserMessage(objResponse.Message);

                            dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex-1].Selected = true;
                            dgvStatementList.CurrentCell = dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex-1];
                            dgvStatementList.BeginEdit(true);

                            return;
                        }

                        //-----------------------------------------------------
                        dTable = (DataTable)objResponse.CustomeTypes;
                        //------------------------------------------------------
                        dgvConsumption.Columns.Clear();
                        dgvConsumption.DataSource = null;
                        dgvConsumption.DataSource = dTable;
                        dgvConsumption.Columns[0].Width = 150;
                        dgvConsumption.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvConsumption.Columns[1].Width = 120;
                        dgvConsumption.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvConsumption.Columns[4].Width = 120;
                        dgvConsumption.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvConsumption.Columns[6].Width = 150;
                        dgvConsumption.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvConsumption.Columns[7].Width = 150;
                        dgvConsumption.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        //dgvConsumption.Columns[7].Visible = false;

                        //if (dTable.Rows.Count <= 0)
                        //    cmnService.J_UserMessage("Records not found");

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

        #region pgTimerGrid_Tick
        private void pgTimerGrid_Tick(object sender, EventArgs e)
        {
            int intValue = 0;
            if (rbnCINSearch.Checked)
                intValue = Convert.ToInt32(Convert.ToString(dgvStatementList.Rows[intRowIndex].Cells[8].Value) == "" ? "0" : dgvStatementList.Rows[intRowIndex].Cells[8].Value);
            else
                intValue = Convert.ToInt32(Convert.ToString(dgvStatementList.Rows[intRowIndex].Cells[7].Value) == "" ? "0" : dgvStatementList.Rows[intRowIndex].Cells[7].Value);

            if (intValue == 100) intValue = 0;

            // Slow down
            this.pgTimerGrid.Interval = (this.pgTimerGrid.Interval * 2);

            //Update progress bar
            if ((intValue + 1) > 100)
            {
                if (rbnCINSearch.Checked)
                    dgvStatementList.Rows[intRowIndex].Cells[8].Value = 100;
                else
                    dgvStatementList.Rows[intRowIndex].Cells[7].Value = 100;
            }
            else
            {
                intValue += 1;
                if (rbnCINSearch.Checked)
                    dgvStatementList.Rows[intRowIndex].Cells[8].Value = intValue;
                else
                    dgvStatementList.Rows[intRowIndex].Cells[7].Value = intValue;

            }
        }

        #endregion


        #region IsValidCINPeriodPayment
        public bool IsValidCINPeriodPayment(ref TracesData objData)
        {
            if (string.IsNullOrEmpty(objData.FromChallanDepositDate.Trim()) || objData.FromChallanDepositDate == "  /  /")
            {
                cmnService.J_UserMessage("Challan Deposit From Date is mandatory");
                mskChallanFromDate.Focus();
                return false;
            }
            else
            {
                if (!dtService.J_IsDateValid(objData.FromChallanDepositDate.Trim()))
                {
                    cmnService.J_UserMessage("Enter a Valid Challan Deposit From Date ");
                    mskChallanFromDate.Focus();
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
                mskChallanToDate.Focus();
                return false;
            }
            else
            {
                if (!dtService.J_IsDateValid(objData.ToChallanDepositDate.Trim()))
                {
                    cmnService.J_UserMessage("Enter a Valid Challan Deposit From Date ");
                    mskChallanToDate.Focus();
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
                mskChallanFromDate.Focus();
                return false;
            }

            if (FromDate > intCurrentDate || ToDate > intCurrentDate)
            {
                cmnService.J_UserMessage("Invalid Challan Deposit Date");
                mskChallanFromDate.Focus();
                return false;
            }

            if (FromDate > ToDate)
            {
                cmnService.J_UserMessage("Invalid Challan Deposit Date");
                mskChallanFromDate.Focus();
                return false;
            }

            if (!TracesValidation.ValidateStartDate(objData.FromChallanDepositDate))
            {
                cmnService.J_UserMessage("Invalid Challan Deposit Date");
                mskChallanFromDate.Focus();
                return false;
            }

            if (!TracesValidation.validateYearRange(objData.FromChallanDepositDate, objData.ToChallanDepositDate))
            {
                cmnService.J_UserMessage("Date range should be within the same financial year.");
                mskChallanFromDate.Focus();
                return false;
            }


            return true;
        }


        #endregion

        #region IsValidCIN_CINBINParticulars
        public bool IsValidCIN_CINBINParticulars(TracesData objData)
        {
            if (string.IsNullOrEmpty(objData.BSRCode))
            {
                cmnService.J_UserMessage("BSR Code is mandatory");
                txtBSRCode.Focus();
                return false;
            }
            else
            {
                if (!TracesValidation.IsNumeric(objData.BSRCode))
                {
                    cmnService.J_UserMessage("Invalid BSR Code");
                    txtBSRCode.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(objData.TaxDepositedDate) || objData.TaxDepositedDate == "  /  /")
            {
                cmnService.J_UserMessage("Date of Deposit is mandatory");
                mskChallanDate.Focus();
                return false;
            }
            else
            {
                if (!dtService.J_IsDateValid(objData.TaxDepositedDate))
                {
                    cmnService.J_UserMessage("Enter a Valid Date");
                    mskChallanDate.Focus();
                    return false;
                }
                else
                {
                    objData.TaxDepositedDate = dtService.J_ConvertddMMyyyy(mskChallanDate).ToString("dd-MMM-yyyy");
                }
            }
            //-------------------------------------------------------------
            if (string.IsNullOrEmpty(objData.ChallanSerialNo))
            {
                cmnService.J_UserMessage("Challan Serial Number is mandatory");
                txtChallanSerialNo.Focus();
                return false;
            }
            else
            {
                if (!TracesValidation.IsNumeric(objData.ChallanSerialNo))
                {
                    cmnService.J_UserMessage("Invalid Challan Serial Number");
                    txtChallanSerialNo.Focus();
                    return false;
                }
            }
            //-------------------------------------------------------------
            if (string.IsNullOrEmpty(objData.ChallanAmount))
            {
                cmnService.J_UserMessage("Challan Amount is mandatory");
                txtChallanAmount.Focus();
                return false;
            }
            else
            {
                if (!TracesValidation.IsNumeric(objData.ChallanAmount))
                {
                    cmnService.J_UserMessage("Invalid Challan Amount");
                    txtChallanAmount.Focus();
                    return false;
                }
                int intCount = objData.ChallanAmount.IndexOf(".");

                if (intCount == 0)
                {
                    cmnService.J_UserMessage("Amount should be entered in two decimal places");
                    txtChallanAmount.Focus();
                    return false;
                }
            }
            //-------------------------------------------------------------

            return true;
        }


        #endregion

        #region IsValidBINPeriodPayment
        public bool IsValidBINPeriodPayment(ref TracesData objData)
        {
            if (string.IsNullOrEmpty(objData.FromChallanDepositDate.Trim()) || objData.FromChallanDepositDate == "  /  /")
            {
                cmnService.J_UserMessage("Transfer Voucher From Date is mandatory");
                mskTransferVoucherFromDate.Focus();
                return false;
            }
            else
            {
                if (!dtService.J_IsDateValid(objData.FromChallanDepositDate.Trim()))
                {
                    cmnService.J_UserMessage("Enter a Valid Transfer Voucher From Date");
                    mskTransferVoucherFromDate.Focus();
                    return false;
                }
                else
                {
                    objData.FromChallanDepositDate = dtService.J_ConvertddMMyyyy(objData.FromChallanDepositDate).ToString("dd-MMM-yyyy");
                }
            }

            if (string.IsNullOrEmpty(objData.ToChallanDepositDate.Trim()) || objData.ToChallanDepositDate == "  /  /")
            {
                cmnService.J_UserMessage("Transfer Voucher To Date is mandatory");
                mskTransferVoucherToDate.Focus();
                return false;
            }
            else
            {
                if (!dtService.J_IsDateValid(objData.ToChallanDepositDate.Trim()))
                {
                    cmnService.J_UserMessage("Enter a Valid Transfer Voucher To Date ");
                    mskTransferVoucherToDate.Focus();
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
            //---------------------------------------------------------`1x---            
            if (!TracesValidation.IsValidDate(objData.FromChallanDepositDate) || !TracesValidation.IsValidDate(objData.ToChallanDepositDate))
            {
                cmnService.J_UserMessage("Invalid Transfer Voucher Date");
                mskChallanFromDate.Focus();
                return false;
            }

            if (FromDate > intCurrentDate || ToDate > intCurrentDate)
            {
                cmnService.J_UserMessage("Invalid Transfer Voucher Date");
                mskChallanFromDate.Focus();
                return false;
            }

            if (FromDate > ToDate)
            {
                cmnService.J_UserMessage("Invalid Transfer Voucher Date");
                mskChallanFromDate.Focus();
                return false;
            }
            if (FromDate < 20133101)
            {
                cmnService.J_UserMessage("For BIN Details prior to FY 2013-14. Please visit. https://onlineservices.tin.egov-nsdl.com/TIN/JSP/etbaf/ViewBIN.jsp");
                mskChallanFromDate.Focus();
                return false;
            }

            if (!TracesValidation.ValidateStartDate(objData.FromChallanDepositDate))
            {
                cmnService.J_UserMessage("Invalid Transfer Voucher Date");
                mskChallanFromDate.Focus();
                return false;
            }

            if (!TracesValidation.validateYearRange(objData.FromChallanDepositDate, objData.ToChallanDepositDate))
            {
                cmnService.J_UserMessage("Date range should be within the same financial year.");
                mskChallanFromDate.Focus();
                return false;
            }


            return true;
        }


        #endregion


        #region IsValidBINParticular
        public bool IsValidBINParticular(ref TracesData objData)
        {
            if (string.IsNullOrEmpty(objData.BSRCode))
            {
                cmnService.J_UserMessage("Receipt Number of 24G is mandatory");
                txtReceiptNumber.Focus();
                return false;
            }
            else
            {
                if (objData.BSRCode.Length != 7)
                {
                    cmnService.J_UserMessage("Invalid Receipt Number");
                    txtReceiptNumber.Focus();
                    return false;
                }

                if (!cmnService.J_IsNumeric(objData.BSRCode))
                {
                    cmnService.J_UserMessage("Invalid Receipt Number");
                    txtReceiptNumber.Focus();
                    return false;
                }


            }

            if (string.IsNullOrEmpty(objData.TaxDepositedDate) || objData.TaxDepositedDate == "  /  /")
            {
                cmnService.J_UserMessage("Transfer Voucher To Date is mandatory");
                mskTransferVoucherDate.Focus();
                return false;
            }
            else
            {
                if (!dtService.J_IsDateValid(objData.TaxDepositedDate))
                {
                    cmnService.J_UserMessage("Enter a Valid Transfer Voucher To Date ");
                    mskTransferVoucherToDate.Focus();
                    return false;
                }
                else
                {
                    objData.TaxDepositedDate = dtService.J_ConvertddMMyyyy(objData.TaxDepositedDate).ToString("dd-MMM-yyyy");
                }
            }

            //-----------------------------------------------------------
            int ToVoucherDate = TracesValidation.ConvertUserDate(objData.TaxDepositedDate);
            int intCurrentDate = TracesValidation.CurrentDate();
            //---------------------------------------------------------`1x---            
            if (!TracesValidation.IsValidDate(objData.TaxDepositedDate))
            {
                cmnService.J_UserMessage("Invalid Transfer Voucher Date");
                mskChallanFromDate.Focus();
                return false;
            }

            if (ToVoucherDate > intCurrentDate)
            {
                cmnService.J_UserMessage("Invalid Transfer Voucher Date");
                mskChallanFromDate.Focus();
                return false;
            }

            if (ToVoucherDate < 20133101)
            {
                cmnService.J_UserMessage("For BIN Details prior to FY 2013-14. Please visit. https://onlineservices.tin.egov-nsdl.com/TIN/JSP/etbaf/ViewBIN.jsp");
                mskChallanFromDate.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(objData.ChallanSerialNo))
            {
                cmnService.J_UserMessage("DDO Serial No is mandatory");
                txtDDOSerialNumber.Focus();
                return false;
            }
            else
            {
                if (objData.ChallanSerialNo.Length != 5)
                {
                    cmnService.J_UserMessage("Invalid DDO Serial Number");
                    txtDDOSerialNumber.Focus();
                    return false;
                }

                if (!cmnService.J_IsNumeric(objData.ChallanSerialNo))
                {
                    cmnService.J_UserMessage("Invalid DDO Serial Number");
                    txtDDOSerialNumber.Focus();
                    return false;
                }

            }
           
            if (string.IsNullOrEmpty(objData.ChallanAmount))
            {
                cmnService.J_UserMessage("Transfer Voucher Amount is mandatory");
                txtBinTransferVoucherAmount.Focus();
                return false;
            }


            return true;
        }


        #endregion


        #region radioSearch_CheckedChanged
        private void radioSearch_CheckedChanged(object sender, EventArgs e)
        {
            //------------------------------------------
            dgvConsumption.Columns.Clear();
            dgvConsumption.DataSource = null;

            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = null;
            // ----------------------------
            cmbChallanStatus.SelectedIndex = 1;
            txtBSRCode.Text = "";
            txtChallanSerialNo.Text = "";
            txtChallanAmount.Text = "0.00";
            //------------------------------
            mskChallanFromDate.Text = "";
            mskChallanToDate.Text = "";
            mskChallanDate.Text = "";
            //---------------------------------------------------
            txtReceiptNumber.Text = "";
            txtDDOSerialNumber.Text = "";
            txtBinTransferVoucherAmount.Text = "0.00";
            mskTransferVoucherDate.Text = "";
            //-------------------------------------------
            gbCINpp.Visible = false;
            gbCINParticular.Visible = false;
            gbBINParticulars.Visible = false;
            gbBINpp.Visible = false;
            //--------------------------------------
            RadioButton objRad = (RadioButton)sender;
            //--------------------------------------
            if (objRad.Name == "rbnCINSearch")
            {
                if (objRad.Checked)
                {
                    if (rbnPeriodofPayment.Checked)
                        gbCINpp.Visible = true;
                    else if (rbnCINBINParticulars.Checked)
                        gbCINParticular.Visible = true;
                }

            }
            else if (objRad.Name == "rbnBINSearch")
            {
                if (objRad.Checked)
                {
                    if (rbnPeriodofPayment.Checked)
                        gbBINpp.Visible = true;

                    else if (rbnCINBINParticulars.Checked)
                        gbBINParticulars.Visible = true;
                }
            }
            else if (objRad.Name == "rbnPeriodofPayment")
            {
                if (objRad.Checked)
                {
                    if (rbnCINSearch.Checked)
                        gbCINpp.Visible = true;

                    else if (rbnBINSearch.Checked)
                        gbBINpp.Visible = true;
                }
            }
            else if (objRad.Name == "rbnCINBINParticulars")
            {
                if (objRad.Checked)
                {
                    if (rbnCINSearch.Checked)
                        gbCINParticular.Visible = true;

                    else if (rbnBINSearch.Checked)
                        gbBINParticulars.Visible = true;
                }

            }

        }

        #endregion

        #region btnSearchCINPP_Click
        private void btnSearchCINPP_Click(object sender, EventArgs e)
        {
            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = null;

            dgvConsumption.Columns.Clear();
            dgvConsumption.DataSource = null;

            TracesData objData = new TracesData();
            //-------------------------------------
            objData.FromChallanDepositDate = mskChallanFromDate.Text;
            objData.ToChallanDepositDate = mskChallanToDate.Text;

            if (cmbChallanStatus.SelectedIndex > 0)
            {
                switch (cmbChallanStatus.Text)
                {
                    case "All":
                        objData.ChallanStatus = "A";
                        break;
                    case "Claimed":
                        objData.ChallanStatus = "M";
                        break;
                    case "Unclaimed":
                        objData.ChallanStatus = "U";
                        break;
                }
            }

            //-- VALIDATION -----------------------
            if (!IsValidCINPeriodPayment(ref objData)) return;
            ////------------------------------------------------
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.CINPP);
            objList.Add(objData);
            //-------------------------------------------
            pgTimer.Start();
            //-------------------------------------------
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync(objList);
        }

        #endregion

        #region btnSearchCINParticulars_Click
        private void btnSearchCINParticulars_Click(object sender, EventArgs e)
        {
            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = null;

            dgvConsumption.Columns.Clear();
            dgvConsumption.DataSource = null;

            TracesData objData = new TracesData();
            //-------------------------------------
            objData.BSRCode = txtBSRCode.Text.Trim();
            objData.TaxDepositedDate = mskChallanDate.Text;
            objData.ChallanSerialNo = txtChallanSerialNo.Text;
            objData.ChallanAmount = txtChallanAmount.Text;
            //-- VALIDATION -----------------------
            if (!IsValidCIN_CINBINParticulars(objData)) return;
            ////------------------------------------------------
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.CINParticulars);
            objList.Add(objData);
            //-------------------------------------------
            pgTimer.Start();
            //-------------------------------------------
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync(objList);
        }

        #endregion

        #region btnSearchBINPP_Click
        private void btnSearchBINPP_Click(object sender, EventArgs e)
        {
            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = null;

            dgvConsumption.Columns.Clear();
            dgvConsumption.DataSource = null;

            TracesData objData = new TracesData();
            //-------------------------------------
            objData.FromChallanDepositDate = mskTransferVoucherFromDate.Text;
            objData.ToChallanDepositDate = mskTransferVoucherToDate.Text;

            if (cmbTransferStatus.SelectedIndex > 0)
            {
                switch (cmbTransferStatus.Text)
                {
                    case "All":
                        objData.ChallanStatus = "A";
                        break;
                    case "Claimed":
                        objData.ChallanStatus = "M";
                        break;
                    case "Unclaimed":
                        objData.ChallanStatus = "U";
                        break;
                }
            }

            //-- VALIDATION -----------------------
            if (!IsValidCINPeriodPayment(ref objData)) return;
            ////------------------------------------------------
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.BINPP);
            objList.Add(objData);
            //-------------------------------------------
            pgTimer.Start();
            //-------------------------------------------
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync(objList);

        }

        #endregion

        #region btnSearchBINParticulars_Click
        private void btnSearchBINParticulars_Click(object sender, EventArgs e)
        {
            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = null;

            dgvConsumption.Columns.Clear();
            dgvConsumption.DataSource = null;

            TracesData objData = new TracesData();
            //-------------------------------------
            objData.BSRCode = txtReceiptNumber.Text;
            objData.TaxDepositedDate = mskTransferVoucherDate.Text;
            objData.ChallanSerialNo = txtDDOSerialNumber.Text;
            objData.ChallanAmount = txtBinTransferVoucherAmount.Text;

            //-- VALIDATION -----------------------
            if (!IsValidBINParticular(ref objData)) return;
            ////------------------------------------------------
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.BINParticulars);
            objList.Add(objData);
            //-------------------------------------------
            pgTimer.Start();
            //-------------------------------------------
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync(objList);

        }

        #endregion

        #region txtChallanAmount_KeyPress
        private void txtChallanAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtChallanAmount, "") == false)
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

        #region txtBinTransferVoucherAmount_KeyPress
        private void txtBinTransferVoucherAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtBinTransferVoucherAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=LIpc60VggMA&t=1s");
            //V0009
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0009", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0058", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
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
    }

}

