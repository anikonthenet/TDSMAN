
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
using TDSMAN.FormBrowser;
using TDSMAN.FormTrn;

#endregion

namespace TDSMAN.FormWeb
{
    public partial class TrnViewFiledTDS : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnViewFiledTDS()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        FVUSubmissionByAdhar objAccount = new FVUSubmissionByAdhar();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        DirectoryInfo directorySelected;

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();
        string strFolderPath = string.Empty;
        string strRequestID = "";
        int intRowIndex = 0;
        int intColumnIndex = 0;


        enum enmRequestType
        {
            Next,//Login,
            UserControls,        
            SearchResults, 
            SearchGrid,   
            StatementDetails,
            Download,
            LogOff
        }



        #endregion

        #region TrnViewFiledTDS_Activated
        private void TrnViewFiledTDS_Activated(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //-----------------------------------------
            //
        }
        #endregion

        #region TrnViewFiledTDS
        private void TrnViewFiledTDS_Load(object sender, EventArgs e)
        {
            ShowHideLoginDetails(enmRequestType.Next);

            lblTitle.Text = "View Filed TDS and Download Provisional Receipt";
            //
            ClearControls();
        }
        #endregion              

        #region btnLogin_Click
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    return;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
                    return;
                }
                //--
               
                //---------------------------------------------------------------------
                if (BtnSave.Text.Trim() == "Next")
                {
                    if (!ValidateFields()) return;
                    // -------------------------------------------
                    //pgTimer.Start();
                    //// -------------------------------------------
                    if (cmnService.J_UserMessage("This will take you to 'incometaxindiaefiling.gov.in' webpage, where you have to enter the captcha for login, for any query regarding the\n contents of the linked page please contact the concerned website.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                    //ArrayList objList = new ArrayList();
                    //objList.Add(enmRequestType.Login);
                    //objList.Add(txtUserID.Text.Trim());
                    //objList.Add(txtPassword.Text.Trim());
                    //objList.Add(txtCaptchaCode.Text.Trim());

                    //if (!bgWorker.IsBusy)
                    //{
                    //    //bgWorker.RunWorkerAsync(objList);
                    //}
                    #region ADD/UPDATE THE USER ID AND PASSWORD IN MASTER

                    //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER
                    strSQL = "SELECT COUNT(*) FROM MST_TAN_AADHAAR WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "' ";
                    int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //--
                    if (iCount == 0)
                    {
                        //INSERING NEW RECORD IN THE TAN LOGIN MASTER
                        strSQL = "INSERT INTO MST_TAN_AADHAAR(TAN_NO, USER_PASSWORD) " +
                                    "VALUES( '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                                    "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

                        dmlService.J_ExecSql(strSQL);
                    }
                    else
                    {
                        //updating the existing record in the master
                        strSQL = "UPDATE MST_TAN_AADHAAR " +
                                    "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                                    "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                                    "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "' ";

                        dmlService.J_ExecSql(strSQL);
                    }
                    #endregion
                    //--            
                    TDSMAN.Classes.TDSMAN.T_TANOnlineFilling = txtUserID.Text;
                    TDSMAN.Classes.TDSMAN.T_PasswordOnlineFilling = txtPassword.Text;
                    //
                    this.Close();
                    //--
                    TrnViewUploadedTDSBrowser TrnViewUploadedTDSBrowser = new TrnViewUploadedTDSBrowser();
                    TrnViewUploadedTDSBrowser.MdiParent = TrnRegularReturn.ActiveForm;
                    TrnViewUploadedTDSBrowser.Show();
                    
                }
                else if(BtnSave.Text.Trim() == "Upload")
                {                  

                    //-------------------------------------------
                   // pgTimer.Start();
                   // // -------------------------------------------
                   // ArrayList objList = new ArrayList();
                   //// objList.Add(enmRequestType.Upload);
                   // objList.Add(strRequestID);

                    
                   // if (!bgWorker.IsBusy)
                   //     bgWorker.RunWorkerAsync(objList);
                }
            }

            catch// (Exception err)
            {
                //cmnService.J_UserMessage(err.Message);
                //cmnService.J_UserMessage("We are not able to receive the response from the TRACES webiste.\n It is requested to check your details at TRACES website with the given parameters");
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
            //  ClearValues();
            //   if (!Validates()) return;
            //------------------------------------------------

            //ArrayList objList = new ArrayList();
            //objList.Add(enmRequestType.DeductionDetails);

            //TracesData objData = new TracesData();
            //objData.PAN1 = txtPAN.Text.Trim();
            //objData.FAYear = cmbFAYear.Text.Substring(0, cmbFAYear.Text.IndexOf("-")); ;
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
            //objData.Forms = cmbFormNo.Text; ;
            //objList.Add(objData);
            ////-------------------------------------------
            //pgTimer.Start();
            ////-------------------------------------------
            //if (!bgWorker.IsBusy)
            //    bgWorker.RunWorkerAsync(objList);
        }
        #endregion

        #region btnView_Click


        private void btnView_Click(object sender, EventArgs e)
        {
            // ------------------------
            // -- FINANCIAL YEAR
            // ------------------------
            if (cmbFAYear.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Financial Year");
                cmbFAYear.Select();
                return;
            }
            // ------------------------
            // -- FORM NO
            // ------------------------

            if (cmbFormNo.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Form Name");
                cmbFormNo.Select();
                return;
            }

            // ------------------------
            // -- QUARTER
            // ------------------------

            if (cmbQtr.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Quarter");
                cmbQtr.Select();
                return;
            }
            // ------------------------
            // -- UPLOAD TYPE
            // ------------------------
            if (cmbUploadType.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Upload Type");
                cmbUploadType.Select();
                return;
            }


            //-------------------------------------------------------
            ParamAdhar adhar = new ParamAdhar();
            adhar.TanNo = txtTANNo.Text;
            adhar.FAYear = cmbFAYear.Text.Replace("-", "");
            adhar.Quarter = cmbQtr.Text;
            adhar.Forms = cmbFormNo.Text;
            //  adhar.FileName = adhar.FileName.Substring(0, adhar.FileName.LastIndexOf("."));
            //----------------------------------------
            if (cmbUploadType.Text == "Regular")
            {
                adhar.UploadType = "R";
                adhar.OriginalRRRNo = "";
                adhar.PreviousRRRNo = "";
            }
            else
            {
                adhar.UploadType = "C";
            }
            //----------------------------------------
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.SearchResults);
            objList.Add(adhar);
            //----------------------------------------
            if (!bgWorker.IsBusy)
            {
                dgvUploadDetails.DataSource = null;
                dgvStatementDetails.DataSource = null;

                pgTimer.Start();
                bgWorker.RunWorkerAsync(objList);
                //----------------------------------------
            }
        }
        #endregion


        #region dgvUploadDetails_CellClick
        private void dgvUploadDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //GETTING ROW & COLUMN INDEX OF SELECT ROW
            intRowIndex = e.RowIndex;
            intColumnIndex = e.ColumnIndex;
            //-------------------------------------------------------------------------------
            if (e.ColumnIndex == dgvUploadDetails.Columns["lnkDetails"].Index && e.RowIndex >= 0)
            {
                if (Convert.ToString(dgvUploadDetails.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "view details")
                {

                    dgvStatementDetails.Columns.Clear();
                    dgvStatementDetails.DataSource = null;

                    //-------------------------------------------------------------------------------
                    pgTimerGrid.Start();
                    // MessageBox.Show("Button on row {0} clicked" + e.RowIndex + " " + dgvDownloadDetails.Rows[e.RowIndex].Cells[intColumnIndex].Value);

                    ArrayList objList = new ArrayList();
                    objList.Add(enmRequestType.StatementDetails);

                    //  ArrayList objData = new ArrayList();
                    objList.Add(Convert.ToString(dgvUploadDetails.Rows[e.RowIndex].Cells[0].Value)); // FA YEAR CODE
                    objList.Add(Convert.ToString(dgvUploadDetails.Rows[e.RowIndex].Cells[9].Value)); // QTR CODE
                                                                                                     //-------------------------------------------------------------------------------

                    objList.Add(objList);

                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync(objList);
                    //-------------------------------------------------------------------------------


                }
            }
        }

        #endregion


        #region dgvStatementDetails_CellClick
        private void dgvStatementDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //GETTING ROW & COLUMN INDEX OF SELECT ROW
            intRowIndex = e.RowIndex;
            intColumnIndex = e.ColumnIndex;
            //-------------------------------------------------------------------------------
            if (e.ColumnIndex == dgvStatementDetails.Columns["lnkDownload"].Index && e.RowIndex >= 0)
            {
                if (Convert.ToString(dgvStatementDetails.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "download")
                {
                    //-------------------------------------------------------------------------------
                    DialogResult result = fbdFolder.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        //-------------------------------------------------------------------------------
                        pgTimerGrid2.Start();

                        ArrayList objList = new ArrayList();
                        objList.Add(enmRequestType.Download);

                        //  ArrayList objData = new ArrayList();
                        objList.Add(Convert.ToString(dgvStatementDetails.Rows[e.RowIndex].Cells[0].Value)); // SESSION ID
                        objList.Add(Convert.ToString(dgvStatementDetails.Rows[e.RowIndex].Cells[1].Value)); // PDF ID
                        objList.Add(Convert.ToString(dgvStatementDetails.Rows[e.RowIndex].Cells[4].Value)); // TRANSACTION TYPE

                        objList.Add(fbdFolder.SelectedPath);

                        //   directorySelected = new DirectoryInfo(fbdFolder.SelectedPath);
                        //    strExtractPath = fbdFolder.SelectedPath;

                        //-------------------------------------------------------------------------------
                        objList.Add(objList);

                        if (!bgWorker.IsBusy)
                            bgWorker.RunWorkerAsync(objList);
                        //-------------------------------------------------------------------------------
                    }

                }
            }
        }

        #endregion



        #region bgWorker_DoWork
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //ArrayList objList = (ArrayList)e.Argument;
            //ArrayList objRetval = new ArrayList();
            ////-------------------------------------------------------
            //enmRequestType enReqType = (enmRequestType)objList[0];
            //TracesResponse objResponse;
            ////-------------------------------------------------------
            //switch (enReqType)
            //{
            //    // LOGIN REQUEST
            //    case enmRequestType.Login:
            //        objResponse = objAccount.makeLogin(Convert.ToString(objList[1]), Convert.ToString(objList[2]), Convert.ToString(objList[3]));
            //        //---------------------------------------------------
            //        objRetval.Add(enmRequestType.Login);
            //        objRetval.Add(objResponse);
            //        //---------------------------------------------------
            //        e.Result = objRetval;
            //        //---------------------------------------------------
            //        break;
            //    // LIST OF STATEMENT STATUS FILES
            //    case enmRequestType.SearchResults:
            //         TracesResponse response = objAccount.ViewFiledTDSIncomeTaxSite((ParamAdhar)objList[1]);
            //        objRetval.Add(enmRequestType.SearchGrid);
            //        objRetval.Add(response);
            //        e.Result = objRetval;
            //        break;

            //    // LIST OF STATEMENT STATUS FILES
            //    case enmRequestType.StatementDetails:
            //        response = objAccount.ViewDetailsAcknowledgement(Convert.ToString(objList[1]), Convert.ToString(objList[2]));
            //        objRetval.Add(enmRequestType.StatementDetails);
            //        objRetval.Add(response);
            //        e.Result = objRetval;
            //        break;

            //    // REQUEST FOR DOWNLOAD LIST
            //    case enmRequestType.Download:

            //        string strPath = (string)objList[2];
            //        objResponse = objAccount.RequestForDownloadFile((string)objList[1], (string)objList[2], (string)objList[3], (string)objList[4]);

            //       objRetval.Add(enmRequestType.Download);
            //       objRetval.Add(objResponse);
            //       e.Result = objRetval;

            //        break;



            //    //REQUEST FOR LOG OFF
            //    case enmRequestType.LogOff:
            //        objResponse = objAccount.Logoff();
            //        objRetval.Add(enmRequestType.LogOff);
            //        objRetval.Add(objResponse);
            //        e.Result = objRetval;
            //        break;
            //}
        }

        #endregion

        #region bgWorker_RunWorkerCompleted
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //try
            //{
            //    ArrayList objMessage = (ArrayList)e.Result;
            //    enmRequestType enmReqType = (enmRequestType)objMessage[0];
            //    TracesResponse objResponse = (TracesResponse)objMessage[1];
            //    //---------------------------------------------------------
            //    switch (enmReqType)
            //    {
            //        case enmRequestType.Login:
            //            //pgTimer.Stop();
            //            //pgTimer.Interval = 1000;
            //            //pBar.Value = 99;
            //            //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER
            //            strSQL = "SELECT COUNT(*) FROM MST_TAN_AADHAAR WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "' ";
            //            int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));
            //            //--
            //            if (iCount == 0)
            //            {
            //                //INSERING NEW RECORD IN THE TAN LOGIN MASTER
            //                strSQL = "INSERT INTO MST_TAN_AADHAAR(TAN_NO, USER_PASSWORD) " +
            //                         "VALUES( '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
            //                         "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

            //                dmlService.J_ExecSql(strSQL);
            //            }
            //            else
            //            {
            //                //updating the existing record in the master
            //                strSQL = "UPDATE MST_TAN_AADHAAR " +
            //                         "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
            //                         "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
            //                         "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "' ";

            //                dmlService.J_ExecSql(strSQL);
            //            }
            //            // -------------------------------------------
            //            //---------------------------------------------------
            //            if (objResponse.Respons == enmResponse.Success)
            //            {
            //                ArrayList objList = new ArrayList();
            //                objList.Add(enmRequestType.SearchResults);

            //                ParamAdhar adhar = new ParamAdhar();
            //                adhar.TanNo = txtUserID.Text;
            //                adhar.FAYear = "-1";
            //                adhar.Quarter = "-1";
            //                adhar.Forms = "-1";
            //                adhar.UploadType = "-1";
            //                objList.Add(adhar);

            //                if (!bgWorker.IsBusy)
            //                    bgWorker.RunWorkerAsync(objList);



            //                //  ShowHideLoginDetails(enmRequestType.UserControls);

            //            }
            //            if (objResponse.Respons == enmResponse.Failed)
            //            {
            //                pgTimer.Stop();
            //                pBar.Value = 100;
            //                cmnService.J_UserMessage(objResponse.Message);
            //                InitializeCaptcha();
            //                return;
            //            }
            //            else
            //            {
            //                //---------------------------------------------------
            //                pBar.Value = 0;
            //                pgTimer.Stop();
            //                //---------------------------------------------------
            //            }
            //            break;

            //        case enmRequestType.SearchGrid:

            //            this.pgTimer.Stop();
            //            //this.pgTimer.Interval = 1000;
            //            pBar.Value = 100;

            //            if (objResponse.Respons == enmResponse.SessionTimeout)
            //            {
            //                ShowHideLoginDetails(enmRequestType.Login);
            //                return;
            //            }

            //            if (objResponse.Respons == enmResponse.Failed)
            //            {
            //                cmnService.J_UserMessage(objResponse.Message);
            //                return;
            //            }
            //            if (objResponse.Respons == enmResponse.Success)
            //            {

            //                DataTable dTable = (DataTable)objResponse.CustomeTypes;

            //                PopulateSearchGridView(dTable);
            //                ShowHideLoginDetails(enmRequestType.UserControls);

            //            }
                       
            //            //----------------------------------------------
            //            break;

            //        case enmRequestType.StatementDetails:
            //            pgTimerGrid.Stop();
            //            dgvUploadDetails.Rows[intRowIndex].Cells[12].Value = 100;
            //            //
            //            if (objResponse.Respons == enmResponse.SessionTimeout)
            //            {
            //                cmnService.J_UserMessage(objResponse.Message);
            //                ShowHideLoginDetails(enmRequestType.Login);
            //                return;
            //            }

            //            if (objResponse.Respons == enmResponse.Failed)
            //            {
            //                cmnService.J_UserMessage(objResponse.Message);
            //                ShowHideLoginDetails(enmRequestType.UserControls);
                            
            //                return;
            //            }
            //            //-- ANIK @ 30/01/2018
            //            if (objResponse.Respons == enmResponse.ReturnRejected)
            //            {
            //                DataTable dTable = (DataTable)objResponse.CustomeTypes;

            //                PopulateRejectedGridView(dTable);
            //                return;
            //            }
            //            //
            //            if (objResponse.Respons == enmResponse.Success)
            //            {
            //                DataTable dTable = (DataTable)objResponse.CustomeTypes;

            //                PopulateAcknowledgeMentGridView(dTable);
            //                return;
            //            }
                      
            //                break;

            //        case enmRequestType.Download:
            //            pgTimerGrid2.Stop();
            //            dgvStatementDetails.Rows[intRowIndex].Cells[8].Value = 100;

            //            if (objResponse.Respons == enmResponse.Success)
            //            {
            //                cmnService.J_UserMessage("Download Completed.");

            //            }
            //            else if (objResponse.Respons == enmResponse.Failed)
            //            {
            //                cmnService.J_UserMessage("Download Failed.");

            //            }
            //            else if (objResponse.Respons == enmResponse.SessionTimeout)
            //            {
            //                ShowHideLoginDetails(enmRequestType.Login);
            //            }

            //                break;


            //        case enmRequestType.LogOff:
            //            this.pgTimer.Stop();
            //            pBar.Value = 100;

            //            //txtUserID.Text = "";
            //            //txtPassword.Text = "";

            //            txtCaptchaCode.Text = "";
            //            //  grpControls.Visible = false;
            //            // grpLoginDetails.Visible = true;
            //            // grpProgress.Visible = true;
            //            BtnSave.Enabled = true;
            //            BtnSave.BackColor = Color.Lavender;
            //            InitializeCaptcha();
            //            pBar.Value = 0;
            //            break;
            //    }
            //    //-------------------------------------------------------
            //}
            //catch (Exception err)
            //{
            //    cmnService.J_UserMessage(err.Message, MessageBoxIcon.Error);
            //}
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
            int intValue = Convert.ToInt32(Convert.ToString(dgvUploadDetails.Rows[intRowIndex].Cells[12].Value) == "" ? "0" : dgvUploadDetails.Rows[intRowIndex].Cells[12].Value);
            if (intValue == 100) intValue = 0;

            // Slow down
            this.pgTimerGrid.Interval = (this.pgTimerGrid.Interval * 2);

            //Update progress bar
            if ((intValue + 1) > 100)
            {
                dgvUploadDetails.Rows[intRowIndex].Cells[12].Value = 100;
            }
            else
            {
                intValue += 1;
                dgvUploadDetails.Rows[intRowIndex].Cells[12].Value = intValue;
            }
        }

        #endregion

        #region pgTimerGrid2_Tick
        private void pgTimerGrid2_Tick(object sender, EventArgs e)
        {
            int intValue = Convert.ToInt32(Convert.ToString(dgvStatementDetails.Rows[intRowIndex].Cells[8].Value) == "" ? "0" : dgvStatementDetails.Rows[intRowIndex].Cells[8].Value);
            if (intValue == 100) intValue = 0;

            // Slow down
            this.pgTimerGrid2.Interval = (this.pgTimerGrid2.Interval * 2);

            //Update progress bar
            if ((intValue + 1) > 100)
            {
                dgvStatementDetails.Rows[intRowIndex].Cells[8].Value = 100;
            }
            else
            {
                intValue += 1;
                dgvStatementDetails.Rows[intRowIndex].Cells[8].Value = intValue;
            }
        }

        #endregion


        #region txtUserID_TextChanged
        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            IDataReader drdShowDeducteeHelp = null;
            //--
            try
            {
                if (txtUserID.Text.Trim() == "")
                {
                    lstTAN.Visible = false;
                    return;
                }

                if (blnShowHelp == false)
                    return;
                //-----------------------
                strSQL = "SELECT TAN_AADHAAR_ID," +
                         "       TAN_NO," +
                         "       USER_PASSWORD " +
                         "FROM   MST_TAN_AADHAAR " +
                         "WHERE  TAN_NO LIKE '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "%' " +
                         "ORDER BY TAN_NO, TAN_AADHAAR_ID";

                drdShowDeducteeHelp = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShowDeducteeHelp == null)
                {
                    lstTAN.Visible = false;
                    return;
                }
                else
                {
                    lstTAN.Items.Clear();
                    lstTAN.Height = 15;
                    lstTAN.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        //lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) 
                        //                                        + drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15)
                        //                                        + drdShowDeducteeHelp["USER_PASSWORD"].ToString())); //-- 2015/10/06 AnikG
                        lstTAN.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12)
                                                                + TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdShowDeducteeHelp["USER_PASSWORD"].ToString()).PadRight(10),
                                                                 Convert.ToInt32(drdShowDeducteeHelp["TAN_AADHAAR_ID"])));
                        //--
                        if (lstTAN.Height <= 300)
                            lstTAN.Height = lstTAN.Height + 19;
                    }
                    //--
                    if (lstTAN.Items.Count <= 0)
                        lstTAN.Visible = false;
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

        #region txtUserID_KeyPress
        private void txtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                if (lstTAN.Visible == true)
                {
                    lstTAN.Focus();
                    lstTAN.SelectedIndex = 0;
                }
                else
                    SendKeys.Send("{tab}");
            }
        }
        #endregion

        #region txtUserID_KeyDown
        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (lstTAN.Visible == true)
                {
                    lstTAN.Focus();
                    lstTAN.SelectedIndex = 0;
                }
            }
            //
        }
        #endregion


        #region lstTAN_KeyPress
        private void lstTAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                lstTAN_Click(sender, e);
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstTAN.Visible = false;
                txtTANNo.Select();
            }
        }
        #endregion

        #region lstTAN_Click
        private void lstTAN_Click(object sender, EventArgs e)
        {
            //string strlstDeducteeHelp = lstDeducteeHelp.Text;

            //txtTANNo.Text = cmnService.J_Left(strlstDeducteeHelp, 10);
            //txtUserID.Text = cmnService.J_Mid(strlstDeducteeHelp, strlstDeducteeHelp.IndexOf(' '),
            //                                  strlstDeducteeHelp.LastIndexOf(' ') - strlstDeducteeHelp.IndexOf(' ')).Trim();
            //txtPassword.Text = cmnService.J_Right(strlstDeducteeHelp, strlstDeducteeHelp.Length - strlstDeducteeHelp.LastIndexOf(' ')).Trim();
            //-- 2015/10/06 AnikG
            long lngTANId = Convert.ToInt32(Support.GetItemData(lstTAN, lstTAN.SelectedIndex));
            txtUserID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_AADHAAR WHERE TAN_AADHAAR_ID = " + lngTANId));
            //txtUserID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_AADHAAR WHERE TAN_AADHAAR_ID = " + lngTANId));
            //strCompanyName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_NAME FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngTANId));

            lstTAN.Visible = false;
            //--
            txtCaptchaCode.Select();
        }
        #endregion

        #region lnkNewRegister_LinkClicked
        private void lnkNewRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connectivity not found");
                return;
            }
            if (cmnService.J_UserMessage("This will take you to a webpage outside TDSMAN, for any query regarding the contents of the linked page,\n please contact the concerned website.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //
            //System.Diagnostics.Process.Start("https://incometaxindiaefiling.gov.in/e-Filing/Registration/RegistrationHome.html");
            //System.Diagnostics.Process.Start("https://portal.incometaxindiaefiling.gov.in/e-Filing/Registration/RegistrationHome.html");
            System.Diagnostics.Process.Start("https://www1.incometaxindiaefiling.gov.in/e-FilingGS/Registration/RegistrationHome.html?lang=eng");
            
            //
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
            //if (TdsMan.T_CheckInternetConnectivty() == false)
            //{
            //    cmnService.J_UserMessage("Internet Connectivity not found");
            //    return;
            //}
            ////----------------------------------------------------
            //objAccount = new FVUSubmissionByAdhar();
            ////Stream imgStream = objAccount.MakeInitialRequest();
            //Stream imgStream = objAccount.MakeInitialRequest();
            //Image img = Image.FromStream(imgStream);
            //this.picCaptcha.Image = img;
            ////-------------------------------------------------------
            //txtCaptchaCode.Text = "";
        }

        #endregion

        #region ValidateFields
        bool ValidateFields()
        {
            if (BtnSave.Text.Trim() == "Login")
            {
                if (string.IsNullOrEmpty(txtUserID.Text))
                {
                    cmnService.J_UserMessage("Please enter User ID");
                    txtUserID.Focus();
                    return false;
                }
                else
                {
                    if (txtUserID.Text.Length != 10)
                    {
                        cmnService.J_UserMessage("TAN No. should be of 10 characters");
                        txtUserID.Select();
                        return false;
                    }
                    //---------------------------
                    if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtUserID.Text, 4), J_DataType.Character) == false)
                    {
                        cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                        txtUserID.Select();
                        return false;
                    }
                    //---------------------------
                    if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtUserID.Text, 4, 5), J_DataType.Numeric) == false)
                    {
                        cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                        txtUserID.Select();
                        return false;
                    }
                    //---------------------------
                    if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtUserID.Text, 1), J_DataType.Character) == false)
                    {
                        cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                        txtUserID.Select();
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    cmnService.J_UserMessage("Please enter Password");
                    txtPassword.Focus();
                    return false;
                }
                //if (string.IsNullOrEmpty(txtCaptchaCode.Text))
                //{
                //    cmnService.J_UserMessage("Please enter Captcha Code");
                //    txtCaptchaCode.Focus();
                //    return false;
                //}
            }
            else if (BtnSave.Text.Trim() == "Submit")
            {
                //// ------------------------
                //// -- FINANCIAL YEAR
                //// ------------------------

                //if (cmbFAYear.SelectedIndex <= 0)
                //{
                //    cmnService.J_UserMessage("Please select the Financial year of the return for which the TDS file has to be downloaded");
                //    cmbFAYear.Select();
                //    return false;
                //}

                //// ------------------------
                //// -- FORM NO
                //// ------------------------

                //if (cmbFormNo.SelectedIndex <= 0)
                //{
                //    cmnService.J_UserMessage("Please select the Form No of the return for which the TDS file has to be downloaded");
                //    cmbFormNo.Select();
                //    return false;
                //}

                //// ------------------------
                //// -- QUARTER
                //// ------------------------

                //if (cmbQtr.SelectedIndex <= 0)
                //{
                //    cmnService.J_UserMessage("Please select the Quarter of the return for which the TDS file has to be downloaded");
                //    cmbQtr.Select();
                //    return false;
                //}




            }

            return true;
        }

        #endregion


        #region ShowHideLoginDetails
        void ShowHideLoginDetails(enmRequestType enmStatus)
        {
            switch (enmStatus)
            {
                case enmRequestType.Next:
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    txtCaptchaCode.Text = "";
                    // txtTANNo.Text = "";
                    grpControls.Visible = false;
                    grpLoginDetails.Visible = true;
                    grpStatementDetails.Enabled = true;
                    //InitializeCaptcha();
                    picCaptcha.Image = Properties.Resources.captcha_loading;
                    if (!bgWorkerLoadCaptcha.IsBusy)
                        bgWorkerLoadCaptcha.RunWorkerAsync();
                    //--
                    BtnSave.Text = "Next";
                    BtnSave.Enabled = true;

                    cmbFAYear.SelectedIndex = -1;
                    cmbFormNo.SelectedIndex = -1;
                    cmbQtr.SelectedIndex = -1;
                    cmbUploadType.SelectedIndex = -1;

                    dgvUploadDetails.DataSource = null;
                    dgvStatementDetails.DataSource = null;
                    break;
                case enmRequestType.UserControls:
                    // BtnSave.Text = "Upload";
                    txtTANNo.Text = txtUserID.Text;
                    txtTANPassword.Text = txtTANNo.Text.ToLower();
                    btnView.Enabled = true;
                    grpControls.Visible = true;
                    grpLoginDetails.Visible = false;
                  //  grpStatementDetails.Enabled = true;
                  //  grpOTPDetails.Visible = false;

                    BtnSave.Enabled = false;
                  //  BtnSave.BackColor = Color.LightGray;
                    // ClearValues();
                    break;

                //case enmRequestType.OTPRequest:

                //    btnView.Enabled = false;
                //    BtnSave.Enabled = true;
                //    grpOTPDetails.Visible = true;
                //    grpStatementDetails.Enabled = false;
                //    break;

                //case enmRequestType.Upload:
                //    //txtPAN.Text = "";
                //    cmbFAYear.SelectedIndex = 0;
                //    cmbFormNo.SelectedIndex = 0;
                //    cmbQtr.SelectedIndex = 0;
                   

                //    btnView.Enabled = false;
                //    BtnSave.Enabled = false;
                //    grpOTPDetails.Visible = false;
                //    grpStatementDetails.Enabled = true;
                //    break;






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
            //txtUserID.Text = "";
            //txtPassword.Text = "";
            txtCaptchaCode.Text = "";
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
                "      AND    VISIBILITY_AADHAAR_RETURN_FLAG = 1 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFAYear, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------   
        
            //-- QUARTER
            string[] strQtr = { T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQtr);

            //-- FORM NO.
            //string[] strFormNo = { "FORM NO.24Q - Quarterly Statement of TDS u/s 200(3) [Salary]", "FORM NO.26Q - Quarterly Statement of TDS u/s 200(3) [Other than Salary]", "FORM NO.27Q - Quarterly Statement of TDS u/s 200(3) [Non-Resident  - other than Salary]", "FORM NO.27EQ - Quarterly Statement of TCS u/s 206C" };
            //        dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);

            string[] strFormNo = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);


            string[] strUploadType = { "Regular", "Correction"};
            dmlService.J_PopulateComboBox(strUploadType, ref cmbUploadType);


            blnShowHelp = true;

        }
        #endregion

        #region PopulateSearchGridView
        void PopulateSearchGridView(DataTable dsRecords)
        {
            // DATA BIND TO GRIDVIEW CONTROL
            dgvUploadDetails.Columns.Clear();
            dgvUploadDetails.DataSource = null;
            dgvUploadDetails.DataSource = dsRecords;
            ////---------------------------------------
            ////CHECKING IF RECORD EXISTS OR NOT
            //if (dsRecords.Rows.Count <= 0)
            //{
            //    cmnService.J_UserMessage("No data available for the specified search criteria");
            ////    cmnService.J_UserMessage("No defaults found for the entered TAN.");
            //}
            ////---------------------------------------

            // CREATE DOWNLOAD BUTTON & PROGRESSBAR CONTROL COLUMNS 
            //------------------------------------------------------------
            dgvUploadDetails.Columns[0].Width = 0;
            dgvUploadDetails.Columns[0].Visible = false;
            dgvUploadDetails.Columns[1].Width = 30;
          //  dgvUploadDetails.Columns[1].Visible = false;
            dgvUploadDetails.Columns[2].Width = 80;
            dgvUploadDetails.Columns[3].Width = 85;
            dgvUploadDetails.Columns[4].Width = 40;
            dgvUploadDetails.Columns[5].Width = 70;
            dgvUploadDetails.Columns[6].Width = 37;
            dgvUploadDetails.Columns[7].Width = 80;
            dgvUploadDetails.Columns[8].Width = 60;
            dgvUploadDetails.Columns[9].Width = 80;
            dgvUploadDetails.Columns[10].Width = 102;
           // dgvUploadDetails.Columns[11].Width = 70;
            //dgvUploadDetails.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewLinkColumn btn = new DataGridViewLinkColumn();
            dgvUploadDetails.Columns.Add(btn);
            btn.HeaderText = "";
            btn.Text = "View Details";
            btn.Name = "lnkDetails";
            //------------------------------------------------------------
            btn.UseColumnTextForLinkValue = true;
            //------------------------------------------------------------
            DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            dgvUploadDetails.Columns.Add(prg);
            prg.Name = "";
            prg.ProgressBarColor = Color.LightGreen;
            //
            //CHECKING IF RECORD EXISTS OR NOT
            if (dsRecords.Rows.Count <= 0)
            {
                cmnService.J_UserMessage("No data available for the specified search criteria");
                //cmnService.J_UserMessage("No defaults found for the entered TAN.");
            }
            //---------------------------------------

            /*foreach (DataGridViewRow gridRow in dgvUploadDetails.Rows)
            {
                DataGridViewCell BookButtonCell = gridRow.Cells["Token Number"];

                if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "NO DATA AVAILABLE")
                {
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    gridRow.Cells["lnkDetails"] = new DataGridViewTextBoxCell();
                    //gridRow.Cells["btnDownload"].Value = "........oooops";
                }
            }*/



        }

        #endregion

        #region PopulateAcknowledgeMentGridView
        void PopulateAcknowledgeMentGridView(DataTable dsRecords)
        {
            // DATA BIND TO GRIDVIEW CONTROL
            dgvStatementDetails.Columns.Clear();
            dgvStatementDetails.DataSource = null;
            dgvStatementDetails.DataSource = dsRecords;
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
            dgvStatementDetails.Columns[0].Width = 0;
            dgvStatementDetails.Columns[0].Visible = false;
            dgvStatementDetails.Columns[1].Width = 0;
            dgvStatementDetails.Columns[1].Visible = false;
            //dgvUploadDetails.Columns[2].Width = 130;

            dgvStatementDetails.Columns[4].Width = 150;
            dgvStatementDetails.Columns[5].Width = 150;
            dgvStatementDetails.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            DataGridViewLinkColumn btn = new DataGridViewLinkColumn();
            dgvStatementDetails.Columns.Add(btn);
            btn.HeaderText = "";
            btn.Text = "Download";
            btn.Name = "lnkDownload";
            //------------------------------------------------------------
            btn.UseColumnTextForLinkValue = true;
            //------------------------------------------------------------
            DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            dgvStatementDetails.Columns.Add(prg);
            prg.Name = "";
            prg.ProgressBarColor = Color.LightGreen;
            //
            //CHECKING IF RECORD EXISTS OR NOT
            if (dsRecords.Rows.Count <= 0)
            {
                cmnService.J_UserMessage("No data available for the specified search criteria");
                //cmnService.J_UserMessage("No defaults found for the entered TAN.");
            }
            //---------------------------------------

            /*foreach (DataGridViewRow gridRow in dgvUploadDetails.Rows)
            {
                DataGridViewCell BookButtonCell = gridRow.Cells["Token Number"];

                if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "NO DATA AVAILABLE")
                {
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    gridRow.Cells["lnkDetails"] = new DataGridViewTextBoxCell();
                    //gridRow.Cells["btnDownload"].Value = "........oooops";
                }
            }*/



        }

        #endregion


        #region PopulateRejectedGridView
        void PopulateRejectedGridView(DataTable dsRecords)
        {
            // DATA BIND TO GRIDVIEW CONTROL
            dgvStatementDetails.Columns.Clear();
            dgvStatementDetails.DataSource = null;
            dgvStatementDetails.DataSource = dsRecords;
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
            dgvStatementDetails.Columns[0].Width = 0;
            dgvStatementDetails.Columns[0].Visible = false;
            dgvStatementDetails.Columns[1].Width = 800;
            dgvStatementDetails.Columns[1].Visible = true;
            //dgvUploadDetails.Columns[2].Width = 130;

            //dgvStatementDetails.Columns[4].Width = 150;
            //dgvStatementDetails.Columns[5].Width = 150;
            //dgvStatementDetails.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            //DataGridViewLinkColumn btn = new DataGridViewLinkColumn();
            //dgvStatementDetails.Columns.Add(btn);
            //btn.HeaderText = "";
            //btn.Text = "Download";
            //btn.Name = "lnkDownload";
            ////------------------------------------------------------------
            //btn.UseColumnTextForLinkValue = true;
            ////------------------------------------------------------------
            //DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            //dgvStatementDetails.Columns.Add(prg);
            //prg.Name = "";
            //prg.ProgressBarColor = Color.LightGreen;
            ////
            ////CHECKING IF RECORD EXISTS OR NOT
            //if (dsRecords.Rows.Count <= 0)
            //{
            //    cmnService.J_UserMessage("No data available for the specified search criteria");
            //    //cmnService.J_UserMessage("No defaults found for the entered TAN.");
            //}
            //---------------------------------------

            /*foreach (DataGridViewRow gridRow in dgvUploadDetails.Rows)
            {
                DataGridViewCell BookButtonCell = gridRow.Cells["Token Number"];

                if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "NO DATA AVAILABLE")
                {
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    gridRow.Cells["lnkDetails"] = new DataGridViewTextBoxCell();
                    //gridRow.Cells["btnDownload"].Value = "........oooops";
                }
            }*/



        }

        #endregion

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0069", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }
    }

}

