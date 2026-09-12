
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
using TDSMAN.FormTrn;
using TDSMAN.FormBrowser;

#endregion

namespace TDSMAN.FormWeb
{
    public partial class TrnFVUSubmissionAadhaarOld : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnFVUSubmissionAadhaarOld()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        FVUSubmissionByAdhar objAccount = new FVUSubmissionByAdhar();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();
        string strFolderPath = string.Empty;
        string strRequestID = "";

        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        enum enmRequestType
        {
            Next,
            UserControls,            
            Regular,
            Correction,
            OTPRequest,
            Upload,
            LogOff
        }



        #endregion

        #region TrnFVUSubmissionAdhar_Activated
        private void TrnFVUSubmissionAdhar_Activated(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //-----------------------------------------
            //
        }
        #endregion

        #region TrnFVUSubmissionAdhar
        private void TrnFVUSubmissionAdhar_Load(object sender, EventArgs e)
        {
            ShowHideLoginDetails(enmRequestType.Next);

            lblTitle.Text = "Upload TDS using Aadhaar (using OTP)";
            //
            //UpdtTokenNo("                    Your TDS return have been uploaded successfully and the       Transaction ID is:       5736388034.                    An e-mail confirming the successful upload of your       e-filing has been sent to       RGOENKA@JAYASOFTWARES.COM                   Kindly login after 24 hours to check the status of your       Filing using the token number       3113857197          ");
            ClearControls(); 
            //--
            if (TDSMAN.Classes.TDSMAN.T_TANOnlineFilling.ToString() != "")
            {
                txtUserID.Text = TDSMAN.Classes.TDSMAN.T_TANOnlineFilling;
                txtUserID.ReadOnly = true;
                txtPassword.Select();
            }
            else
                txtUserID.ReadOnly = false;
            //
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
                    //--
                    if (!ValidateFields()) return;
                    //--
                    //if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //if (cmnService.J_UserMessage("This will take you to a webpage outside TDSMAN, for any query regarding the contents of the linked page,\n please contact the concerned website.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    if (cmnService.J_UserMessage("This will take you to 'incometaxindiaefiling.gov.in' webpage, where you have to enter the captcha for login, for any query regarding the\n contents of the linked page please contact the concerned website.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
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
                    // -------------------------------------------
                    //pgTimer.Start();
                    // -------------------------------------------
                    //ArrayList objList = new ArrayList();
                    //objList.Add(enmRequestType.Login);
                    //objList.Add(txtUserID.Text.Trim());
                    //objList.Add(txtPassword.Text.Trim());
                    //objList.Add(txtCaptchaCode.Text.Trim());

                    //if (!bgWorker.IsBusy)
                    //    bgWorker.RunWorkerAsync(objList);
                    //--
                    //string strFVUVersion = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FVU_VERSION FROM MST_ASSESSMENT WHERE ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex))));
                    TDSMAN.Classes.TDSMAN.T_FVUVersionOnlineFilling = "";
                    TDSMAN.Classes.TDSMAN.T_FAYearOnlineFilling = "";
                    TDSMAN.Classes.TDSMAN.T_FormNoOnlineFilling = "";
                    TDSMAN.Classes.TDSMAN.T_QtrOnlineFilling = "";
                    TDSMAN.Classes.TDSMAN.T_UploadTypeOnlineFilling = "";
                    //--
                    TDSMAN.Classes.TDSMAN.T_UploadFilePath = "";
                    //--            
                    TDSMAN.Classes.TDSMAN.T_TANOnlineFilling = txtUserID.Text;
                    TDSMAN.Classes.TDSMAN.T_PasswordOnlineFilling = txtPassword.Text;
                    //
                    this.Close();
                    //--
                    TrnUploadTDSBrowser TrnUploadTDSBrowser = new TrnUploadTDSBrowser();
                    TrnUploadTDSBrowser.MdiParent = TrnRegularReturn.ActiveForm;
                    TrnUploadTDSBrowser.Show();
                }
                else if(BtnSave.Text.Trim() == "Upload")
                {
                    //if (string.IsNullOrWhiteSpace(txtOTPCode.Text))
                    //{
                    //    cmnService.J_UserMessage("Please enter OTP");
                    //    txtOTPCode.Focus();
                    //    return;
                    //}

                    ////-------------------------------------------
                    //pgTimer.Start();
                    //// -------------------------------------------
                    //ArrayList objList = new ArrayList();
                    //objList.Add(enmRequestType.Upload);
                    //objList.Add(txtOTPCode.Text.Trim());
                    //objList.Add(strRequestID);

                    //objList.Add(txtFilePath.Text);
                    //string FileName = txtFilePath.Text.Substring(txtFilePath.Text.LastIndexOf("\\") + 1);
                    //objList.Add(FileName);
                    
                    //if (!bgWorker.IsBusy)
                    //    bgWorker.RunWorkerAsync(objList);
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
            //InitializeCaptcha();
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
            //    case enmRequestType.OTPRequest:
            //         TracesResponse response = objAccount.getOTPfromIncomeTaxSite((ParamAdhar)objList[1]);
            //        objRetval.Add(enmRequestType.OTPRequest);
            //        objRetval.Add(response);
            //        e.Result = objRetval;
            //        break;

            //    // LIST OF STATEMENT STATUS FILES
            //    case enmRequestType.Upload:
            //        response = objAccount.UploadfvuWithAadhaar(Convert.ToString(objList[1]),Convert.ToString(objList[2]), 
            //                                                   Convert.ToString(objList[3]),Convert.ToString(objList[4]));
            //        objRetval.Add(enmRequestType.Upload);
            //        objRetval.Add(response);
            //        e.Result = objRetval;
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
            //            //---------------------------------------------------
            //            if (objResponse.Respons == enmResponse.Success)
            //                ShowHideLoginDetails(enmRequestType.UserControls);


            //            if (objResponse.Respons == enmResponse.Failed)
            //            {
            //                pgTimer.Stop();
            //                pBar.Value = 100;
            //                cmnService.J_UserMessage(objResponse.Message);
            //                //InitializeCaptcha();
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

            //        case enmRequestType.OTPRequest:

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

            //                strRequestID =Convert.ToString(objResponse.CustomeTypes);  
            //                ShowHideLoginDetails(enmRequestType.OTPRequest);
            //            }
                       
            //            //----------------------------------------------
            //            break;

            //        case enmRequestType.Upload:
            //            pgTimer.Stop();
            //            pBar.Value = 100;

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
            //                //--
            //                return;
            //            }

            //            if (objResponse.Respons == enmResponse.Success)
            //            {
            //                cmnService.J_UserMessage(objResponse.Message);
            //                //
            //                if (cmbUploadType.Text == T_FVU_RETURN_AADHAAR.RegularReturn)
            //                    UpdtTokenNo(objResponse.Message.Trim()); //-- 2018/07/25
            //                //
            //                ShowHideLoginDetails(enmRequestType.UserControls);
            //                return;
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
            //            //InitializeCaptcha();
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


        #region txtTANNo_KeyPress
        private void txtTANNo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        #endregion

        #region txtTAN_KeyDown
        private void txtTAN_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion

        #region txtTAN_TextChanged
        private void txtTAN_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region txtTAN_Leave
        private void txtTAN_Leave(object sender, EventArgs e)
        {
            //  if (txtTANNo.Text.Trim() == "") return;
            //INITIALIZE CAPTCHA CODE
            //InitializeCaptcha();

        }
        #endregion

        #region lnkLogOff_Click
        private void lnkLogOff_Click(object sender, EventArgs e)
        {
            ShowHideLoginDetails(enmRequestType.LogOff);
        }
        #endregion
        
        #region cmbUploadType_SelectedIndexChanged
        private void cmbUploadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUploadType.SelectedIndex > 0)
            {
                if (cmbUploadType.Text.ToUpper().Trim() == T_FVU_RETURN_AADHAAR.RegularReturn.ToUpper().Trim())
                    pnlCorrection.Enabled = false;
                else if (cmbUploadType.Text.ToUpper().Trim() == T_FVU_RETURN_AADHAAR.CorrectionReturn.ToUpper().Trim())
                    pnlCorrection.Enabled = true;
                else
                    pnlCorrection.Enabled = false;
            }
            else
                pnlCorrection.Enabled = false;
        }
        #endregion

        #region btnSelectExcelPath_Click
        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
            strFolderPath = cmnService.J_OpenFileDialog("fvu files (*.zip)|*.zip");

            if (strFolderPath != "")
                txtFilePath.Text = strFolderPath;
            //--
            grpEVerify.Enabled = true;
        }
        #endregion

        #region btnOTP_Click
        private void btnOTP_Click(object sender, EventArgs e)
        {
            // ------------------------
            // -- FVU VERSION CHECK
            // ------------------------

            if (cmbFVUVersion.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the FVU Version");
                cmbFVUVersion.Select();
                return;
            }
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
            if (cmbUploadType.Text == T_FVU_RETURN_AADHAAR.CorrectionReturn)
            {
                if (string.IsNullOrWhiteSpace(txtOrgnRRRNo.Text))
                {
                    cmnService.J_UserMessage("Please Enter Valid Original RRR Number");
                    txtOrgnRRRNo.Select();
                    return;
                }
                else
                {
                    if (txtOrgnRRRNo.Text.Length < 15)
                    {
                        cmnService.J_UserMessage("Please Enter Valid Original RRR Number");
                        txtOrgnRRRNo.Select();
                        return;
                    }
                }

                if (string.IsNullOrWhiteSpace(txtPrevRRRNo.Text))
                {
                    cmnService.J_UserMessage("Please Enter Valid Previous RRR Number");
                    txtPrevRRRNo.Select();
                    return;
                }
                else
                    if (txtPrevRRRNo.Text.Length < 15)
                    {
                        cmnService.J_UserMessage("Please Enter Valid Previous RRR Number");
                        txtPrevRRRNo.Select();
                        return;
                    }
            }

            if (string.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                cmnService.J_UserMessage("Please Upload a Zip file");
                cmbUploadType.Select();
                return;
            }

            //-------------------------------------------------------
            ParamAdhar adhar = new ParamAdhar();
            adhar.FVUVersion = cmbFVUVersion.Text;
            adhar.TanNo = txtTANNo.Text;
            adhar.FAYear = cmbFAYear.Text.Replace("-", "");
            adhar.Quarter = cmbQtr.Text;
            adhar.Forms = cmbFormNo.Text;
            adhar.FileLocation = txtFilePath.Text;
            adhar.FileName = txtFilePath.Text.Substring(txtFilePath.Text.LastIndexOf("\\") + 1);
            //  adhar.FileName = adhar.FileName.Substring(0, adhar.FileName.LastIndexOf("."));
            //----------------------------------------
            if (cmbUploadType.Text == T_FVU_RETURN_AADHAAR.RegularReturn)
            {
                adhar.UploadType = "R";
                adhar.OriginalRRRNo = "";
                adhar.PreviousRRRNo = "";
            }
            else
            {
                adhar.UploadType = "C";
                adhar.OriginalRRRNo = txtOrgnRRRNo.Text;
                adhar.PreviousRRRNo = txtPrevRRRNo.Text;
            }
            //----------------------------------------
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.OTPRequest);
            objList.Add(adhar);
            //----------------------------------------
            if (!bgWorker.IsBusy)
            {
                pgTimer.Start();
                bgWorker.RunWorkerAsync(objList);
                //----------------------------------------
            }
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

            System.Diagnostics.Process.Start("https://www1.incometaxindiaefiling.gov.in/e-FilingGS/Registration/RegistrationHome.html?lang=eng");
            //
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

        #region txtCaptchaCode_Enter
        private void txtCaptchaCode_Enter(object sender, EventArgs e)
        {
            BtnSave.Enabled = true;
        }
        #endregion

        #region InitializeCaptcha
        private void InitializeCaptcha()
        {
            //--
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connectivity not found");
                return;
            }
            //----------------------------------------------------
            objAccount = new FVUSubmissionByAdhar();
            //Stream imgStream = objAccount.MakeInitialRequest();
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
                // ------------------------
                // -- FINANCIAL YEAR
                // ------------------------

                if (cmbFAYear.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select the Financial year of the return for which the TDS file has to be downloaded");
                    cmbFAYear.Select();
                    return false;
                }

                // ------------------------
                // -- FORM NO
                // ------------------------

                if (cmbFormNo.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select the Form No of the return for which the TDS file has to be downloaded");
                    cmbFormNo.Select();
                    return false;
                }

                // ------------------------
                // -- QUARTER
                // ------------------------

                if (cmbQtr.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select the Quarter of the return for which the TDS file has to be downloaded");
                    cmbQtr.Select();
                    return false;
                }




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
                    //InitializeCaptcha(); //-- 2018/08/23
                    BtnSave.Text = "Next";
                    break;
                case enmRequestType.UserControls:
                    BtnSave.Text = "Upload";
                    txtTANNo.Text = txtUserID.Text;
                    btnGenerateOTP.Enabled = true;
                    grpControls.Visible = true;
                    grpLoginDetails.Visible = false;
                    grpStatementDetails.Enabled = true;
                    grpEVerify.Enabled = true;
                    grpOTPDetails.Visible = false;

                    BtnSave.Enabled = false;
                  //  BtnSave.BackColor = Color.LightGray;
                    // ClearValues();
                    break;

                case enmRequestType.OTPRequest:

                    btnGenerateOTP.Enabled = false;
                    BtnSave.Enabled = true;
                    grpOTPDetails.Visible = true;
                    grpStatementDetails.Enabled = false;
                    grpEVerify.Enabled = false;
                    break;

                case enmRequestType.Upload:
                    //txtPAN.Text = "";
                    cmbFAYear.SelectedIndex = 0;
                    cmbFormNo.SelectedIndex = 0;
                    cmbQtr.SelectedIndex = 0;
                    cmbFVUVersion.SelectedIndex = 0;
                   

                    btnGenerateOTP.Enabled = false;
                    BtnSave.Enabled = false;
                    grpOTPDetails.Visible = false;
                    grpStatementDetails.Enabled = true;
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
            pnlCorrection.Enabled=false;
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
            //-- FVU VERSION
            //string[] strFVU = { "FVU 5.7" };
            strSQL = "SELECT 'FVU ' + MAX(FVU_VERSION) FROM MST_ASSESSMENT";
            string[] strFVU = { dmlService.J_ExecSqlReturnScalar(strSQL).ToString()};
            dmlService.J_PopulateComboBox(strFVU, ref cmbFVUVersion);

            //-- QUARTER
            string[] strQtr = { T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQtr);

            //-- FORM NO.
            //string[] strFormNo = { "FORM NO.24Q - Quarterly Statement of TDS u/s 200(3) [Salary]", "FORM NO.26Q - Quarterly Statement of TDS u/s 200(3) [Other than Salary]", "FORM NO.27Q - Quarterly Statement of TDS u/s 200(3) [Non-Resident  - other than Salary]", "FORM NO.27EQ - Quarterly Statement of TCS u/s 206C" };
            //        dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);

            string[] strFormNo = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);


            string[] strUploadType = { T_FVU_RETURN_AADHAAR.RegularReturn, T_FVU_RETURN_AADHAAR.CorrectionReturn };
            dmlService.J_PopulateComboBox(strUploadType, ref cmbUploadType);


            txtFilePath.Text = "";
            txtOTPCode.Text = "";
            txtOrgnRRRNo.Text = "";
            txtPrevRRRNo.Text = "";
            blnShowHelp = true;



        }
        #endregion


        #region UpdtTokenNo
        //-- 2018/07/25
        public void UpdtTokenNo(string Message)
        {
            try
            {
                //-- Extracting Token No.
                //Message = Message.Trim();
                if (Message != "")
                {
                    int index = Message.Trim().LastIndexOf(" ");
                    string strTokenNo = cmnService.J_Mid(Message, index, Message.Length - index).Trim();
                    //
                    DateService dtService = new DateService();
                    string strDateOfFiling = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(J_ReturnServerDate()) + cmnService.J_DateOperator();
                    //-- UPDATE

                    //strSQL="UPDATE TRN_BASIC_INFO " +
                    //             "SET    RECEIPT_NO     =  '" + cmnService.J_ReplaceQuote(txtReceiptNo.Text.Trim()) + "'," +
                    //             "       DATE_OF_FILING =  " + strDateOfFiling + "," +
                    //             "       PRN_NO         =  '" + cmnService.J_ReplaceQuote(txtPRNNo.Text.Trim()) + "' " +
                    //             "WHERE  BASIC_INFO_ID  =  " + lngSearchId + "";
                    //dmlService.J_ExecSql(strSQL);
                }
            }
            catch
            {
            }

        }
        #endregion




        #region J_ReturnServerDate
        private string J_ReturnServerDate()
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strSQL = "SELECT FORMAT(DATE(),'dd/MM/yyyy')";
            else
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            //--
            CommonService cmnService = new CommonService();

            return cmnService.J_NullToText(dmlService.J_ExecSqlReturnScalar(strSQL));
        }


        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0069", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0031", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
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

