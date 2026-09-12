
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
    public partial class TrnPANVerificationSummary : Form
    {
        #region System Generated Code
        public TrnPANVerificationSummary()
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


       

        #endregion

        #region User Defined Events

        #region TrnPANVerificationSummary_Load
        private void TrnPANVerificationSummary_Load(object sender, EventArgs e)
        {
            try
            {
                ////txtPAN.Text = "BROPK6848J";
                //lblTitle.Text = "PAN Verification";
                //--            
                ShowCaptcha();
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

        #region btnReset_Click
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtPAN.Text = "";
            ShowCaptcha();
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
                    //
                    TracesResponse response = objAccount.VerifyPAN(txtPAN.Text, txtCaptchaCode.Text);

                    if (response.Respons == enmResponse.Failed)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage(response.Message);
                        txtPAN.Select();
                        ShowCaptcha();
                        return;
                    }
                    else
                    {
                        PANVerifierDetails objPan = (PANVerifierDetails)response.CustomeTypes;
                        //
                        grpDetails.Visible = true;
                        //
                        lblDetails.Text = txtPAN.Text;
                        lblSurname.Text = objPan.Surname;
                        lblMiddleName.Text = objPan.MiddleName;
                        lblFirstName.Text = objPan.FirstName;
                        lblAreaCode.Text = objPan.AreaCode;
                        lblAOType.Text = objPan.AOType;
                        lblRangeCode.Text = objPan.RangeCode;
                        lblAONumber.Text = objPan.AONumber;
                        lblJurisdiction.Text = objPan.Jurisdiction;
                        lblBuildingName.Text = objPan.BuildingName;

                        ShowCaptcha();

                        txtCaptchaCode.Text = "";
                    }
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
                rptDialog.PANVerification(lblDetails.Text,
                                          lblSurname.Text,
                                          lblMiddleName.Text,
                                          lblFirstName.Text,
                                          lblAreaCode.Text,
                                          lblAOType.Text,
                                          lblRangeCode.Text,
                                          lblAONumber.Text,
                                          lblJurisdiction.Text,
                                          lblBuildingName.Text);
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region IsValidField
        private bool IsValidField()
        {
            if (string.IsNullOrEmpty(txtPAN.Text.Trim()))
            {
                cmnService.J_UserMessage("Please enter the PAN");
                txtPAN.Focus();
                return false;
            }
            else
            {
                string strMessage = "";
                if (!FormValidation.IsValidPAN(txtPAN.Text, out strMessage))
                {
                    cmnService.J_UserMessage("Please enter a valid PAN");
                    txtPAN.Focus();
                    return false;
                }
            }
            //----------------------------------------------------------
            if (string.IsNullOrEmpty(txtCaptchaCode.Text))
            {
                cmnService.J_UserMessage("Please enter the Captcha Code");
                txtPAN.Focus();
                return false;

            }


            return true;

        }


        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            ShowCaptcha();
        }
        #endregion

        #region txtPAN_KeyPress
        private void txtPAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion 

        #region txtCaptchaCode_KeyPress
        private void txtCaptchaCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #endregion 

        #region User Define Functions

        #region Clearcontrols
        private void Clearcontrols()
        {
           grpDetails.Visible = false;
           lblDetails.Text = "";
           lblSurname.Text = "";
           lblMiddleName.Text = "";
           lblFirstName.Text = "";
           lblAreaCode.Text = "";
           lblAOType.Text = "";
           lblRangeCode.Text = "";
           lblAONumber.Text = "";
           lblJurisdiction.Text = "";
           lblBuildingName.Text = "";
        }
        #endregion        

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region ShowCaptcha
        private void ShowCaptcha()
        {
            //--
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connectivity not found");
                //BtnExit.Select();
                return;
            }
            //
            objAccount = new TracesConnect();

            Stream imgStream = objAccount.GetCaptchaForPANVerify();
            Image img = Image.FromStream(imgStream);
            this.picCaptcha.Image = img;
            //-------------------------------------------------------
            txtCaptchaCode.Text = "";

        }

        #endregion

         

        #endregion

    }

}