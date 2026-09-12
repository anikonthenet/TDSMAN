
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
using TDSMAN.FormRpt;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnKnowYourTAN : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnKnowYourTAN()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        TracesConnect objAccount = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        Dictionary<string, string> objNameval = new Dictionary<string, string>();

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();

        enum enmState
        {
            BasicInfo,
            OTP,
            TANDetails
        }


        #endregion


        #region btnReset_Click
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtName_TAN.Text = "";

            Clearcontrols();
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Clearcontrols();
                //
                if (IsValidField())
                {
                    this.Cursor = Cursors.WaitCursor;



                    /*string strname = "";
                     bool bnlStatus = objAccount.ExtractPANName(txtPAN.Text, txtCaptchaCode.Text, out strname);

                     if (bnlStatus==false)
                     {
                         this.Cursor = Cursors.Default;
                         cmnService.J_UserMessage(strname);
                         txtPAN.Select();
                         ShowCaptcha();
                         return;
                     }
                     else
                     {
                       //  PANVerifierDetails objPan = (PANVerifierDetails)response.CustomeTypes;

                         grpDetails.Visible = true;

                         lblDetails.Text = txtPAN.Text;
                         lblSurname.Text = strname;
                         //lblMiddleName.Text = objPan.MiddleName;
                         //lblFirstName.Text = objPan.FirstName;
                         //lblAreaCode.Text = objPan.AreaCode;
                         //lblAOType.Text = objPan.AOType;
                         //lblRangeCode.Text = objPan.RangeCode;
                         //lblAONumber.Text = objPan.AONumber;
                         //lblJurisdiction.Text = objPan.Jurisdiction;
                         //lblBuildingName.Text = objPan.BuildingName;

                         ShowCaptcha();

                         txtCaptchaCode.Text = "";
                     } */


                    //ArrayList objData = new ArrayList();
                    //objData.Add(enmState.PanNameExtract);
                    //objData.Add(txtName_TAN.Text);
                    //objData.Add(txtMobileNo.Text);

                    //bgWorker.RunWorkerAsync(objData);


                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message);
            }

        }
        #endregion




        #region TrnPanVarification_Load
        private void TrnPanVarification_Load(object sender, EventArgs e)
        {
            try
            {
                //txtPAN.Text = "BROPK6848J";
                lblTitle.Text = "Know Your TAN";


                //--
                Clearcontrols();
            }
            catch (Exception err)
            {
                grpDetails.Visible = false;
                //
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #region IsValidField
        private bool IsValidField()
        {
            if (string.IsNullOrEmpty(txtName_TAN.Text.Trim()))
            {
                cmnService.J_UserMessage("Please enter the PAN");
                txtName_TAN.Focus();
                return false;
            }
            else
            {
                string strMessage = "";
                if (!FormValidation.IsValidPAN(txtName_TAN.Text, out strMessage))
                {
                    cmnService.J_UserMessage("Please enter a valid PAN");
                    txtName_TAN.Focus();
                    return false;
                }
            }
            //----------------------------------------------------------
            if (string.IsNullOrEmpty(txtMobileNo.Text))
            {
                cmnService.J_UserMessage("Please enter the Captcha Code");
                txtName_TAN.Focus();
                return false;

            }


            return true;

        }


        #endregion



        #region Clearcontrols
        private void Clearcontrols()
        {  
            grpDetails.Visible = false;
            lblTAN.Text = "";
            lblCategoryDeductor.Text = "";
            lblName.Text = "";
            lblAddress.Text = "";
            lblPAN.Text = "";
            lblStatusofTAN.Text = "";
            lblEmailID1.Text = "";
            lblEmailID2.Text = "";
            lblAreaCode.Text = "";
            lblAOType.Text = "";
            lblRangeCode.Text = "";
            lblAONumber.Text = "";
            lblAODescription.Text = "";
            lblBuildingName.Text = "";
            lblEmailID.Text = "";

            txtMobileOTP.Text = "";
            txtMobileNo.Text = "";
            txtName_TAN.Text = "";
            rbnSearchByName.Checked = false;
            rbnSearchByTAN.Checked = false;


            //-----------
            //-- STATE
            //-----------
            strSQL = " SELECT STATE_CODE," +
                "             STATE_NAME " +
                "      FROM   MST_STATE " +
                "      ORDER BY STATE_ID ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbState, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------

            string[] strReason = { "Central/State Government /Statutory Bodies/Autonomous Bodies/Local Authorities",
                                    "Company/Firms/AOP/BOI/AJP/AOP(Trust) and Branches",
                                    "Individual/HUF including Business" };

            dmlService.J_PopulateComboBox(strReason, ref cmbCategoryDeductor);


        }
        #endregion        

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btnPrintPan_Click
        private void btnPrintPan_Click(object sender, EventArgs e)
        {
            try
            {
                RptDialog rptDialog = new RptDialog();
                rptDialog.PANVerification(lblTAN.Text,
                                          lblCategoryDeductor.Text,
                                          lblName.Text,
                                          lblAddress.Text,
                                          lblPAN.Text,
                                          lblStatusofTAN.Text,
                                          lblEmailID1.Text,
                                          lblEmailID2.Text,
                                          lblAreaCode.Text,
                                          lblAOType.Text);
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region bgWorker_DoWork
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //   enmState eState = (enmState)e.Argument;
            ArrayList objResult = new ArrayList();
            objResult = (ArrayList)e.Argument;


            if (enmState.BasicInfo == (enmState)objResult[0])
            {
                string strname = "";
                TracesResponse objtrConnect = objAccount.RequestForKnowYouTAN(Convert.ToString(objResult[3]), Convert.ToString(objResult[4]), Convert.ToString(objResult[1]), Convert.ToString(objResult[2]), Convert.ToString(objResult[5]));
                objResult.Clear();

                objResult.Add(enmState.BasicInfo);
                objResult.Add(objtrConnect);
            }
            else if (enmState.OTP == (enmState)objResult[0])
            {

                TracesResponse objtrConnect = objAccount.RequestForOTPValidation(Convert.ToString(objResult[1]), (Dictionary<string, string>)(objResult[2]));

                objResult.Clear();

                objResult.Add(enmState.OTP);
                objResult.Add(objtrConnect);
            }

            e.Result = objResult;
        }
        #endregion

        #region bgWorker_RunWorkerCompleted

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ArrayList objResult = (ArrayList)e.Result;
            pBar.Value = 100;
            this.pgTimer.Stop();
            this.pgTimer.Interval = 1000;
            //-----------------------------------------
            if (e.Cancelled == true)
            {
                cmnService.J_UserMessage("Request Cancelled");
                // BtnSave.Text = T_RequestButton.Request;

                return;
            }
            //-----------------------------------------
            if (enmState.BasicInfo == (enmState)objResult[0])
            {
                TracesResponse resp = (TracesResponse)objResult[1];

                if (resp.Respons == enmResponse.Success)
                {
                    objNameval = (Dictionary<string, string>)resp.CustomeTypes;

                    grpDetails.Visible = true;

                    //lblTAN.Text = txtName_TAN.Text;
                    //lblCategoryDeductor.Text = Convert.ToString(objResult[2]);

                    grpOTPDetails.Visible = true;
                    grpDetails.Visible = false;
                    grpSearchBox.Visible = false;

                    lblOTPSentMsg.Text = "Please provide the OTP sent to your Mobile Number" + txtMobileNo.Text;
                }
                else
                {

                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage(resp.Message);
                    rbnSearchByTAN.Select();

                }
            }
            else if (enmState.OTP == (enmState)objResult[0])
            {
                TracesResponse resp = (TracesResponse)objResult[1];
                if (resp.Respons == enmResponse.Success)
                {
                    TanDetails objData =(TanDetails)resp.CustomeTypes;
                    //------------------------------------------
                    lblTAN.Text = objData.TAN;
                    lblCategoryDeductor.Text = objData.DeducteeCategory;
                    lblName.Text = objData.Name;
                    lblAddress.Text = objData.Address;
                    lblPAN.Text = objData.PAN;
                    lblStatusofTAN.Text = objData.StatusOfTAN;
                    lblEmailID1.Text = objData.EmailID1;
                    lblEmailID2.Text = objData.EmailID2;
                    lblAreaCode.Text = objData.AreaCode;
                    lblAOType.Text = objData.AOType;
                    lblRangeCode.Text = objData.RangeCode;
                    lblAONumber.Text = objData.AONumber;
                    lblAODescription.Text = objData.AODescription;
                    lblBuildingName.Text = objData.BuildingName;
                    lblEmailID.Text = objData.EmailID;
                    //------------------------------------------
                    grpOTPDetails.Visible = false;
                    grpDetails.Visible = true;
                    grpSearchBox.Visible = false;


                }
                else
                {
                    grpOTPDetails.Visible = false;
                    grpDetails.Visible = false;
                    grpSearchBox.Visible = true;
                    Clearcontrols();
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage(resp.Message);
                    rbnSearchByTAN.Select();
                }
            }
            //-----------------------------------------

        }
        #endregion


        #region btnSubmit_Click


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rbnSearchByName.Checked == false && rbnSearchByTAN.Checked == false)
            {
                cmnService.J_UserMessage("Please select Search criteria");
                rbnSearchByTAN.Select();
                return;
            }

            if (cmbCategoryDeductor.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select category of deductor");
                cmbCategoryDeductor.Focus();
                return;
            }

            if (cmbState.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select state");
                cmbState.Focus();
                return;
            }


            if (txtName_TAN.Text == "")
            {
                if (rbnSearchByName.Checked)
                    cmnService.J_UserMessage("Please enter name");
                if (rbnSearchByTAN.Checked)
                    cmnService.J_UserMessage("Please enter tan");

                txtName_TAN.Focus();
                return;
            }

            if (txtMobileNo.Text == "")
            {
                cmnService.J_UserMessage("Please enter 10 digit Mobile no");
                cmbState.Focus();
                return;
            }
            else
            {
                if (txtMobileNo.Text.Length != 10)
                {
                    cmnService.J_UserMessage("Please enter 10 digit Mobile no");
                    cmbState.Focus();
                    return;
                }

            }
            //--------------------------------------------------
            string strcat = "";
            string strSrchCriteria = "";
            string strStateCode = "";
            string strName = "";
            string strTan = "";
            if (rbnSearchByName.Checked)
            {
                strSrchCriteria = "name";
                strName = txtName_TAN.Text;
            }
            if (rbnSearchByTAN.Checked)
            {
                strSrchCriteria = "tan";
                strTan = txtName_TAN.Text;
            }

            string strSelectVal = Convert.ToString(Support.GetItemData(cmbState, cmbState.SelectedIndex));

            if (strSelectVal.Length < 2)
                strStateCode = "0" + strSelectVal;
            else
                strStateCode = strSelectVal;

            if (cmbCategoryDeductor.SelectedIndex > 0)
            {
                if (cmbCategoryDeductor.SelectedIndex == 1)
                    strcat = "1,2";
                else if (cmbCategoryDeductor.SelectedIndex == 2)
                    strcat = "3,4,7,8";
                else if (cmbCategoryDeductor.SelectedIndex == 3)
                    strcat = "5,6";

            }
            //--------------------------------------------------
            ArrayList objData = new ArrayList();
            objData.Add(enmState.BasicInfo);
            objData.Add(strcat);
            objData.Add(strStateCode);
            objData.Add(strTan);
            objData.Add(strName);
            objData.Add(txtMobileNo.Text.Trim());

            bgWorker.RunWorkerAsync(objData);

            if (!bgWorker.IsBusy)
            {
                pBar.Value = 0;
                pgTimer.Start();
                bgWorker.RunWorkerAsync();

            }

        }
        #endregion



        #region btnValidate_Click

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMobileOTP.Text))
            {
                cmnService.J_UserMessage("Please enter the OTP sent to your mobile no " + txtMobileNo.Text);
                txtMobileOTP.Focus();
                return;
            }
            //-----------------------------------
            ArrayList objData = new ArrayList();
            objData.Add(enmState.OTP);
            objData.Add(txtMobileOTP.Text);
            objData.Add(objNameval);
            //-----------------------------------
            bgWorker.RunWorkerAsync(objData);

            if (!bgWorker.IsBusy)
            {
                pBar.Value = 0;
                pgTimer.Start();
                bgWorker.RunWorkerAsync();
            }
            //-----------------------------------

        }



        #endregion

        #region btnBack_Click
       
        private void btnBack_Click(object sender, EventArgs e)
        {
            grpSearchBox.Visible = true;
            grpOTPDetails.Visible = false;
        }




        #endregion


        #region rbnSearchByName_CheckedChanged
              
        private void rbnSearchByName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnSearchByName.Checked)
            {
                lblNameTAN.Text = "Name";
            }
            if (rbnSearchByTAN.Checked)
            {
                lblNameTAN.Text = "TAN";
            }

        }

        #endregion


        #region pgTimer_Tick
        private void pgTimer_Tick(object sender, EventArgs e)
        {
            // Slow down
            this.pgTimer.Interval = (this.pgTimer.Interval * 2);

            // SLOW DOWN THE INTERVAL
            //this.pgTimer.Interval = 1000;
            //this.pBar.Step = 5;

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

        #region btnBacktoSearch_Click
       
        private void btnBacktoSearch_Click(object sender, EventArgs e)
        {
            Clearcontrols();
            grpSearchBox.Visible = true;
            grpOTPDetails.Visible = false;
            grpDetails.Visible = false;
        }

        #endregion



    }
}

