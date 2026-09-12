
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
    public partial class TrnDownloadFilesTraces : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnDownloadFilesTraces()
        {
            InitializeComponent();
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
            DownloadList,
            Download,
            LogOff
        }

        int intRowIndex = 0;
        int intColumnIndex = 0;

        #endregion

        #region TrnViewReturnStatusOnline_Load
        private void TrnViewReturnStatusOnline_Load(object sender, EventArgs e)
        {
            InitializeCaptcha();

            lblTitle.Text = "Download Requested Files";
            //
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
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
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
                  objResponse = objAccount.makeLoginToTRACES((TracesLogin)objList[1]);
                    //---------------------------------------------------
                    objRetval.Add(enmRequestType.Login);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    //---------------------------------------------------
                    break;
                // LIST OF DOWNLOAD FILES
                case enmRequestType.DownloadList:
                    DataTable table;
                    TracesResponse response = objAccount.RequestForAllDownloadList(out table);
                    objRetval.Add(enmRequestType.DownloadList);
                    objRetval.Add(response);
                    objRetval.Add(table);
                    e.Result = objRetval;
                    break;
                 // REQUEST FOR DOWNLOAD LIST
                case enmRequestType.Download:

                    string strPath = (string)objList[2];
                    objResponse = objAccount.RequestForDownloadFile((string)objList[1], strPath);

                    objRetval.Add(enmRequestType.Download);
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
                            ShowHideLoginDetails(enmRequestType.DownloadList);

                            ArrayList objList = new ArrayList();
                            objList.Add(enmRequestType.DownloadList);
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
                        //---------------------------------------------------
                        pBar.Value = 0;
                        //---------------------------------------------------
                        break;

                    case enmRequestType.DownloadList:

                        this.pgTimer.Stop();
                        //this.pgTimer.Interval = 1000;
                        pBar.Value = 100;
                        DataTable dTable = (DataTable)objMessage[2];

                        PopulateDatagridView(dTable);
                        break;
                    case enmRequestType.Download:
                        pgTimerGrid.Stop();
                        dgvDownloadList.Rows[intRowIndex].Cells[9].Value = 100;

                        if (objResponse.Respons == enmResponse.Success)
                            cmnService.J_UserMessage("Download Completed");
                        else
                        {
                            cmnService.J_UserMessage("Download Failed :: " + objResponse.Message);
                            ShowHideLoginDetails(enmRequestType.Login);
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
            int intValue = Convert.ToInt32(Convert.ToString(dgvDownloadList.Rows[intRowIndex].Cells[9].Value) == "" ? "0" : dgvDownloadList.Rows[intRowIndex].Cells[9].Value);
            if (intValue == 100) intValue = 0;

            // Slow down
            this.pgTimerGrid.Interval = (this.pgTimerGrid.Interval * 2);

            //Update progress bar
            if ((intValue + 1) > 100)
            {
                dgvDownloadList.Rows[intRowIndex].Cells[9].Value = 100;
            }
            else
            {
                intValue += 1;
                dgvDownloadList.Rows[intRowIndex].Cells[9].Value = intValue;
            }
        }

        #endregion

        #region dgvDownloadDetails_CellClick
        private void dgvDownloadList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //GETTING ROW & COLUMN INDEX OF SELECT ROW
            intRowIndex = e.RowIndex;
            intColumnIndex = e.ColumnIndex;
            //-------------------------------------------------------------------------------
            if (e.ColumnIndex == dgvDownloadList.Columns["btnDownload"].Index && e.RowIndex >= 0)
            {
                if (Convert.ToString(dgvDownloadList.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "download")
                {
                    DialogResult result = fbdFolder.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        //-------------------------------------------------------------------------------
                        pgTimerGrid.Start();
                        // MessageBox.Show("Button on row {0} clicked" + e.RowIndex + " " + dgvDownloadDetails.Rows[e.RowIndex].Cells[intColumnIndex].Value);

                        ArrayList objList = new ArrayList();
                        //-------------------------------------------------------------------------------
                        objList.Add(enmRequestType.Download);
                        objList.Add(dgvDownloadList.Rows[e.RowIndex].Cells[1].Value);
                        objList.Add(fbdFolder.SelectedPath);

                        if (!bgWorker.IsBusy)
                            bgWorker.RunWorkerAsync(objList);
                        //-------------------------------------------------------------------------------
                    }

                }
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
                         "       USER_PASSWORD " +
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
                    lstDeducteeHelp.Height = 15;
                    lstDeducteeHelp.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) + drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15) + drdShowDeducteeHelp["USER_PASSWORD"]));
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

            txtTANNo.Text = cmnService.J_Left(strlstDeducteeHelp, 10);

            txtUserID.Text = cmnService.J_Mid(strlstDeducteeHelp, strlstDeducteeHelp.IndexOf(' '),
                                              strlstDeducteeHelp.LastIndexOf(' ') - strlstDeducteeHelp.IndexOf(' ')).Trim();

            txtPassword.Text = cmnService.J_Right(strlstDeducteeHelp, strlstDeducteeHelp.Length - strlstDeducteeHelp.LastIndexOf(' ')).Trim();

            lstDeducteeHelp.Visible = false;
            //--
            txtCaptchaCode.Select();
        }
        #endregion

        #region txtRequestNo_TextChanged
        private void txtRequestNo_TextChanged(object sender, EventArgs e)
        {
            //------------------------------------------------------------
            DataTable dataTable = (DataTable)dgvDownloadList.DataSource;
            DataView dataView = dataTable.DefaultView;
            dataView.RowFilter = string.Format("[Request Number] like '{0}%'", txtRequestNo.Text);
            /* ------------------------------------------------------------
               REMOVE DOWNLOAD BUTTON
            //------------------------------------------------------------ */
            foreach (DataGridViewRow gridRow in dgvDownloadList.Rows)
            {
                DataGridViewCell BookButtonCell = gridRow.Cells["Status"];
                DataGridViewButtonCell EndDateCell = (DataGridViewButtonCell)gridRow.Cells["btnDownload"];

                if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "NOT AVAILABLE" || Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "SUBMITTED")
                {
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    gridRow.Cells["btnDownload"] = new DataGridViewTextBoxCell();
                }
            }
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
            //----------------------------------------------------
            objAccount = new TracesConnect();
            Stream imgStream = objAccount.MakeInitialRequest();
            Image img = Image.FromStream(imgStream);
            this.picCaptcha.Image = img;
            //-------------------------------------------------------
            txtCaptchaCode.Text = "";
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
            dgvDownloadList.Columns.Clear();
            dgvDownloadList.DataSource = null;
            dgvDownloadList.DataSource = dsRecords;
            // CREATE DOWNLOAD BUTTON & PROGRESSBAR CONTROL COLUMNS 
            //------------------------------------------------------------
            dgvDownloadList.Columns[0].Width = 80;

            dgvDownloadList.Columns[1].Width = 70;

            dgvDownloadList.Columns[2].HeaderText = "Financial Year";
            dgvDownloadList.Columns[2].Width = 80;

            dgvDownloadList.Columns[3].Width = 50;

            dgvDownloadList.Columns[4].Width = 80;            
            
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dgvDownloadList.Columns.Add(btn);
            btn.HeaderText = "";
            btn.Text = "Download";
            btn.Name = "btnDownload";
            //------------------------------------------------------------
            btn.UseColumnTextForButtonValue = true;
            //------------------------------------------------------------
            DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            dgvDownloadList.Columns.Add(prg);
            prg.Name = "Progressbar";
            prg.ProgressBarColor = Color.LightGreen;
            /* ------------------------------------------------------------
               REMOVE DOWNLOAD BUTTON
            //------------------------------------------------------------ */
            foreach (DataGridViewRow gridRow in dgvDownloadList.Rows)
            {
                DataGridViewCell BookButtonCell = gridRow.Cells["Status"];
                DataGridViewButtonCell EndDateCell = (DataGridViewButtonCell)gridRow.Cells["btnDownload"];

                if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "NOT AVAILABLE" || Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "SUBMITTED")
                {
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    gridRow.Cells["btnDownload"] = new DataGridViewTextBoxCell();
                    //gridRow.Cells["btnDownload"].Value = "........oooops";
                }
            }
            //------------------------------------------------------------
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
                    grpProgress.Visible = true;
                    InitializeCaptcha();
                    break;
                case enmRequestType.DownloadList:
                    txtRequestNo.Text = "";
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                    grpProgress.Visible = false;
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

        
        
    }
}

