
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


//
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
#endregion

namespace TDSMAN.FormWeb
{
    public partial class TrnChallanPANCorrection : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnChallanPANCorrection()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            ////--
        }
        #endregion

        #region Objects & Variables decleration

        FVUSubmissionByAdhar objAccount = new FVUSubmissionByAdhar();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DateService dtService = new DateService();
      
        DMLService dmlService = new DMLService();

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();
        string strFolderPath = string.Empty;
        string strRequestID = "";

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

        int iCount = 0;
        string strChallanIDQueue = "";
        bool blnDataEnteredbyUser = false;

        #endregion

        #region TrnChallanPANCorrection_Activated
        private void TrnChallanPANCorrection_Activated(object sender, EventArgs e)
        {
            ////if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            ////{
            ////    txtPassword.UseSystemPasswordChar = true;
            ////}
            txtPassword.Visible = true;
            //-----------------------------------------
            //
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

        #region TrnChallanPANCorrection_Load
        private void TrnChallanPANCorrection_Load(object sender, EventArgs e)
        {
            //--
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //----
            //ShowHideLoginDetails(enmRequestType.Next);

            //lblTitle.Text = "Register Digital Signature Certificate";
            //
            lblTitle.Text = "Add Challan / PAN Correction / Challan Correction";

            //  ClearControls(); 
            //--
            if (TDSMAN.Classes.TDSMAN.T_TANOnlineFilling.ToString() != "")
            {
                //txtUserID.Text = TDSMAN.Classes.TDSMAN.T_TANOnlineFilling;
                //txtUserID.ReadOnly = true;
                //txtPassword.Select();
            }

            blnDataEnteredbyUser = false;

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


                //--
                if (!ValidateFields()) return;
                //--
                //if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //if (cmnService.J_UserMessage("This will take you to a webpage outside TDSMAN, for any query regarding the contents of the linked page,\n please contact the concerned website.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                if (cmnService.J_UserMessage("This will take you to TRACES webpage, where you have to enter the captcha for login, for any query regarding the\n contents of the linked page please contact the concerned website.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER

                #region UPDATE LAST USED USER ID AND PASSWORD
                //UPDATE LAST USED USER ID AND PASSWORD
                if (chkRememberMe.Checked == true)
                {
                    strSQL = "UPDATE TRN_LAST_NSDL " +
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserId.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' ";

                    dmlService.J_ExecSql(strSQL);
                }

                //CHEKCING IF TRN_NSDL_DOWNLOAD TABLE IS HAVING THE DATA FOR THIS RETURN

                strSQL = "SELECT COUNT(*) " +
                         "FROM   TRN_NSDL_DOWNLOAD " +
                         "WHERE TAN_NO  = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "' " +
                         "AND   ASST_ID =  " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                         "AND   FORM_NO = '" + cmbFormNo.Text + "' " +
                         "AND   QTR     = '" + cmbQtr.Text + "'";

                iCount = Convert.ToInt16(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    // INSERTING THE RECORD IN TRN_NSDL_DOWNLOAD

                    strSQL = @"INSERT INTO TRN_NSDL_DOWNLOAD (
                                        TAN_NO,
                                        ASST_ID,
                                        FORM_NO,
                                        QTR,
                                        PREVIOUS_RRR_NO,
                                        CHALLAN_NO,
                                        BSR_CODE,
                                        DEPOSIT_DATE,
                                        TOT_TAX,
                                        DEDUCTEE_PAN1,
                                        DEDUCTEE_AMT1,
                                        DEDUCTEE_PAN2,
                                        DEDUCTEE_AMT2,
                                        DEDUCTEE_PAN3,
                                        DEDUCTEE_AMT3,                                      
                                        CHALLAN_SRL_NO) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "', " +
                             "         " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + ", " +
                             "        '" + cmbFormNo.Text + "', " +
                             "        '" + cmbQtr.Text + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtTokenNo.Text.Trim()) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtChallanNo.Text.Trim()) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtBSRCode.Text.Trim()) + "', " +
                             "         " + (mskChallanDate.Text.Trim() == "/  /" ? "NULL" : cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskChallanDate) + cmnService.J_DateOperator()) + ", " +
                             "         " + Convert.ToDouble(txtChallanTax.Text.Trim()) + ", " +
                             "        '" + cmnService.J_ReplaceQuote(txtDeducteePAN1.Text.Trim()) + "', " +
                             "         " + Convert.ToDouble(txtTDSDeducted1.Text.Trim()) + ", " +
                             "        '" + cmnService.J_ReplaceQuote(txtDeducteePAN2.Text.Trim()) + "', " +
                             "         " + Convert.ToDouble(txtTDSDeducted2.Text.Trim()) + ", " +
                             "        '" + cmnService.J_ReplaceQuote(txtDeducteePAN3.Text.Trim()) + "', " +
                             "         " + Convert.ToDouble(txtTDSDeducted3.Text.Trim()) + "," +                          
                             "         " + cmnService.J_ReturnInt32Value(txtSlNo.Text) + ")";

                    dmlService.J_ExecSql(strSQL);
                }
                else
                {
                    //UPDATING THE RETURN RECORD IN TRN_NSDL_DOWNLOAD

                    strSQL = "UPDATE TRN_NSDL_DOWNLOAD " +
                             "SET TAN_NO               = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "', " +
                             "    ASST_ID              =  " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + ", " +
                             "    FORM_NO              = '" + cmbFormNo.Text + "', " +
                             "    QTR                  = '" + cmbQtr.Text + "', " +
                             "    PREVIOUS_RRR_NO      = '" + cmnService.J_ReplaceQuote(txtTokenNo.Text.Trim()) + "', " +
                             "    CHALLAN_NO           = '" + cmnService.J_ReplaceQuote(txtChallanNo.Text.Trim()) + "', " +
                             "    BSR_CODE             = '" + cmnService.J_ReplaceQuote(txtBSRCode.Text.Trim()) + "', " +
                             "    DEPOSIT_DATE         =  " + (mskChallanDate.Text.Trim() == "/  /" ? "NULL" : cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskChallanDate) + cmnService.J_DateOperator()) + ", " +
                             "    TOT_TAX              =  " + Convert.ToDouble(txtChallanTax.Text.Trim()) + ", " +
                             "    DEDUCTEE_PAN1        = '" + cmnService.J_ReplaceQuote(txtDeducteePAN1.Text.Trim()) + "', " +
                             "    DEDUCTEE_AMT1        =  " + Convert.ToDouble(txtTDSDeducted1.Text.Trim()) + ", " +
                             "    DEDUCTEE_PAN2        = '" + cmnService.J_ReplaceQuote(txtDeducteePAN2.Text.Trim()) + "', " +
                             "    DEDUCTEE_AMT2        =  " + Convert.ToDouble(txtTDSDeducted2.Text.Trim()) + ", " +
                             "    DEDUCTEE_PAN3        = '" + cmnService.J_ReplaceQuote(txtDeducteePAN3.Text.Trim()) + "', " +
                             "    DEDUCTEE_AMT3        =  " + Convert.ToDouble(txtTDSDeducted3.Text.Trim()) + "," +                           
                             "    CHALLAN_SRL_NO       =  " + cmnService.J_ReturnInt32Value(txtSlNo.Text) + " " + //-- 2015/07/31
                             "WHERE TAN_NO             = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "' " +
                             "AND   ASST_ID            =  " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                             "AND   FORM_NO            = '" + cmbFormNo.Text + "' " +
                             "AND   QTR                = '" + cmbQtr.Text + "'";

                    dmlService.J_ExecSql(strSQL);

                }
                //--
                strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";
                iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    //insering new record in the tan login master
                    strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
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
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserId.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                             "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";

                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                
                //===========================================================
                CorrectionData objData = new CorrectionData();
                TracesData objTracess = new TracesData();
                TracesLogin objLogIn = new TracesLogin();

                //PROVIDING RETURN DETAILS
                if (cmbFAYear.SelectedIndex > 0)
                    objTracess.FAYear = cmbFAYear.Text.Substring(0, cmbFAYear.Text.IndexOf("-"));

                objTracess.PRN_NO = txtTokenNo.Text.Trim();

                if (cmbQtr.SelectedIndex > 0)
                {
                    switch (cmbQtr.Text)
                    {
                        case "Q1":
                            objTracess.Quarter = "3";
                            break;
                        case "Q2":
                            objTracess.Quarter = "4";
                            break;
                        case "Q3":
                            objTracess.Quarter = "5";
                            break;
                        case "Q4":
                            objTracess.Quarter = "6";
                            break;
                    }
                }
                //--
                if (cmbFormNo.SelectedIndex > 0)
                    objTracess.Forms = cmbFormNo.Text;

                //PROVDING CHALLAN DETAILS
                objTracess.ChallanSerialNo = txtChallanNo.Text.Trim();
                objTracess.BSRCode = txtBSRCode.Text.Trim();
                objTracess.FromChallanDepositDate = mskChallanDate.Text;
                objTracess.ChallanAmount = txtChallanTax.Text;
                objTracess.CDRecordNumber = txtSlNo.Text;

                //PROVIDING DEDUCTEE DETAILS
                objTracess.PAN1 = txtDeducteePAN1.Text;
                objTracess.PAN2 = txtDeducteePAN2.Text;
                objTracess.PAN3 = txtDeducteePAN3.Text;

                if (txtDeducteePAN1.Text.Trim() != "")
                    objTracess.PAN1Amount = txtTDSDeducted1.Text;
                else
                    objTracess.PAN1Amount = "";


                if (txtDeducteePAN2.Text.Trim() != "")
                    objTracess.PAN2Amount = txtTDSDeducted2.Text;
                else
                    objTracess.PAN2Amount = "";


                if (txtDeducteePAN3.Text.Trim() != "")
                    objTracess.PAN3Amount = txtTDSDeducted3.Text;
                else
                    objTracess.PAN3Amount = "";
                //----------------------------------------------
                
                //===========================================================
                objLogIn.UserID = txtUserId.Text;
                objLogIn.Password = txtPassword.Text;
                objLogIn.TAN = txtTANNo.Text;
                //===========================================================
                objData.TracesData = objTracess;
                objData.TracesLogin = objLogIn;

                if(rdPanCorrection.Checked)
                    objData.CorrectionType = 6;     // 3- CHALLAN CORRECTION ,  6- PAN CORRECTION , 9- ADD CHALLAN
                else if(rdChallanCorrection.Checked)
                    objData.CorrectionType = 3;
                else if(rdAddChallanToStatement.Checked)
                    objData.CorrectionType = 9;
                //===========================================================

                //this.Close();

                //===========================================================
                //TrnChallan_PANCorrectionBrowser objBrowser = new TrnChallan_PANCorrectionBrowser(objData);
                TrnChallan_PANCorrectionBrowserNew objBrowser = new TrnChallan_PANCorrectionBrowserNew(objData);
                objBrowser.MdiParent = TrnChallanPANCorrection.ActiveForm;
                objBrowser.Show();
                ////--
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
            if (e.KeyCode == System.Windows.Forms.Keys.Down)
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
                //
                if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true) txtPassword.UseSystemPasswordChar = true;
                //
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
                    ////lstDeducteeHelp.Height = 15;
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

            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstDeducteeHelp, lstDeducteeHelp.SelectedIndex));
            txtTANNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtUserId.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            //--
            lstDeducteeHelp.Visible = false;
            //--
            // txtCaptchaCode.Select();
        }
        #endregion


        #region ClearControls
        public void ClearControls()
        {
            txtUserId.Text = "";
            txtPassword.Text = "";

            txtTANNo.Text = "";

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
            //---------------------------------------------------------------


        }
        #endregion


        #region ValidateFields
        public bool ValidateFields()
        {
            // ------------------------
            // -- TAN
            // ------------------------

            if (txtTANNo.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Please enter a valid TAN");
                txtTANNo.Select();
                return false;
            }
            if (txtTANNo.Text.Length != 10)
            {
                cmnService.J_UserMessage("TAN No. should be of 10 characters");
                txtTANNo.Select();
                return false;
            }
            //---------------------------
            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtTANNo.Text, 4), J_DataType.Character) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                txtTANNo.Select();
                return false;
            }
            //---------------------------
            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtTANNo.Text, 4, 5), J_DataType.Numeric) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                txtTANNo.Select();
                return false;
            }
            //---------------------------
            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtTANNo.Text, 1), J_DataType.Character) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                txtTANNo.Select();
                return false;
            }

            // ------------------------
            // -- USER ID
            // ------------------------

            //if (txtUserId.Text.Trim() == "")
            //{
            //    cmnService.J_UserMessage("Please enter the User Id for TAN No. -" + txtTANNo.Text.Trim());
            //    txtUserId.Select();
            //    return false;
            //}

            // ------------------------
            // -- PASSWORD
            // ------------------------

            if (txtPassword.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Please enter the Password for TAN No. -" + txtTANNo.Text.Trim());
                txtPassword.Select();
                return false;
            }

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

            if (string.IsNullOrEmpty(txtTokenNo.Text))
            {
                cmnService.J_UserMessage("Token Number / Provisional Receipt Number (PRN) is mandatory");
                txtTokenNo.Focus();
                return false;
            }


            return true;
        }
        #endregion


        #region Combo_SelectedIndexChanged

        
        private void Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTracesData();
        }

        #endregion


        #region setTracesData


        private void setTracesData()
        {
            //btnChangeCDDDDetails.Visible = false;

            if (cmbFAYear.SelectedIndex <= 0 || cmbFAYear.SelectedIndex <= 0 || cmbQtr.SelectedIndex <= 0)
            {
                ClearChallanDeducteeDetails();
                btnChangeCDDDDetails.Visible = false;
                return;
            }

            #region TRN_NSDL_DOWNLOAD

            strSQL = "SELECT COUNT(*) " +
                     "FROM   TRN_NSDL_DOWNLOAD " +
                     "WHERE  TAN_NO  ='" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "' " +
                     "AND    ASST_ID = " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                     "AND    FORM_NO ='" + cmbFormNo.Text + "' " +
                     "AND    QTR     ='" + cmbQtr.Text + "'";

           iCount = Convert.ToInt16(dmlService.J_ExecSqlReturnScalar(strSQL));

            if (iCount > 0)
            {
                strSQL = "SELECT PREVIOUS_RRR_NO, " +
                         "       CHALLAN_SRL_NO," +  //-- 2014/12/06
                         "       CHALLAN_NO, " +
                         "       BSR_CODE, " +
                         "       DEPOSIT_DATE, " +
                         "       TOT_TAX, " +
                         "       DEDUCTEE_PAN1, " +
                         "       DEDUCTEE_AMT1, " +
                         "       DEDUCTEE_PAN2, " +
                         "       DEDUCTEE_AMT2, " +
                         "       DEDUCTEE_PAN3, " +
                         "       DEDUCTEE_AMT3 " +
                         "FROM   TRN_NSDL_DOWNLOAD " +
                         "WHERE TAN_NO  = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "' " +
                         "AND   ASST_ID =  " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                         "AND   FORM_NO = '" + cmbFormNo.Text + "' " +
                         "AND   QTR     = '" + cmbQtr.Text + "'";

               IDataReader  reader = dmlService.J_ExecSqlReturnReader(strSQL);

                while (reader.Read())
                {
                    txtTokenNo.Text = reader["PREVIOUS_RRR_NO"].ToString();
                    txtChallanNo.Text = reader["CHALLAN_NO"].ToString();
                    //
                    if (reader["CHALLAN_SRL_NO"].ToString() != "0")  //-- 2014/12/06
                        txtSlNo.Text = reader["CHALLAN_SRL_NO"].ToString();
                    else
                        txtSlNo.Text = "";
                    //
                    txtBSRCode.Text = reader["BSR_CODE"].ToString();
                    mskChallanDate.Text = reader["DEPOSIT_DATE"].ToString();
                    txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOT_TAX"]));
                    txtDeducteePAN1.Text = reader["DEDUCTEE_PAN1"].ToString();
                    txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["DEDUCTEE_AMT1"]));
                    txtDeducteePAN2.Text = reader["DEDUCTEE_PAN2"].ToString();
                    txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["DEDUCTEE_AMT2"]));
                    txtDeducteePAN3.Text = reader["DEDUCTEE_PAN3"].ToString();
                    txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["DEDUCTEE_AMT3"]));
                }
                //
                reader.Close();
                reader.Dispose();
                //
                blnDataEnteredbyUser = false;

                btnChangeCDDDDetails.Visible = true;
            }
            #endregion
            else
            {
                #region CHECK FROM CORRECTION
                //FIRST CHECKING THE RECORD TO BE PRESENT IN THE CORRECTION RETURN TABLE

                strSQL = "SELECT TOP 1 COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "FROM  COR_HDR_BATCH, " +
                         "      COR_HDR_COMPANY " +
                         "WHERE COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "AND   COR_HDR_BATCH.ASST_ID           = " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                         "AND   COR_HDR_BATCH.FORM_NO           = '" + cmbFormNo.Text + "' " +
                         "AND   COR_HDR_BATCH.QTR               = '" + cmbQtr.Text + "' " +
                         "AND   COR_HDR_COMPANY.TAN_NO          = '" + txtTANNo.Text + "' " +
                         "ORDER BY COR_HDR_BATCH.BATCH_HEADER_ID DESC";

                iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                if (iCount > 0)
                {
                    //DATA NOT FOUND IN CORRECTION RETURN DATA
                    ClearChallanDeducteeDetails();

                    //DATA IS AVAILABLE IN CORRECTION RETURN

                    //SELECTING THE PROVISIONAL RECEIPT NO

                    strSQL = "SELECT ORIGINAL_RRR_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + iCount;
                    txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                    //-- GETTING CHALLAN NO OF MAX DEDUCTEES
                    strSQL = "SELECT TOP 1 COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                           "FROM  COR_HDR_CHALLAN, " +
                           "      COR_HDR_DEDUCTEE_DETAILS " +
                           "WHERE COR_HDR_CHALLAN.HDR_CHALLAN_ID = COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID " +
                           "AND   COR_HDR_CHALLAN.BATCH_HEADER_ID = " + iCount + " " +
                           "GROUP BY COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                           "ORDER BY COUNT(COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID) DESC";
                    txtChallanID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                    //--
                    //NOW SELECTING THE CHALLAN RECORD FROM THE REGULAR RETURN TABLE

                    string[,] strTVoucherChallanNo = {{"BOOK_ENTRY =  0" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  2" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  1" , "F", "TRANSFER_VOUCHER_NO", "F"}};

                    strSQL = "SELECT TOP 1 " + cmnService.J_SQLDBFormat(strTVoucherChallanNo, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CHALLAN_NO, " +
                             "             SL_NO, " + //-- 2014/12/06
                             "             BSR_CODE, " +
                             "             DEPOSIT_DATE, " +
                             "             TOT_TAX," +
                             "             BOOK_ENTRY " +
                             "FROM   COR_HDR_CHALLAN " +
                             "WHERE  BATCH_HEADER_ID = " + iCount + " " +
                             "AND    HDR_CHALLAN_ID  = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                 IDataReader reader = dmlService.J_ExecSqlReturnReader(strSQL);

                    if (reader == null)
                        return;

                    while (reader.Read())
                    {
                        txtSlNo.Text = reader["SL_NO"].ToString(); //-- 2014/12/06
                        txtChallanNo.Text = reader["CHALLAN_NO"].ToString();
                        txtBSRCode.Text = reader["BSR_CODE"].ToString();
                        mskChallanDate.Text = reader["DEPOSIT_DATE"].ToString();
                        txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOT_TAX"]));
                        //-- for BOOK ENTRY
                        if (cmnService.J_ReturnInt32Value(Convert.ToString(reader["BOOK_ENTRY"])) > 0)
                        {
                           // chkBookAdjustment.Checked = true;
                            //-- 2013/08/30
                            //txtChallanNo.Text = "";
                            //txtBSRCode.Text = "";
                            //txtChallanNo.ReadOnly = true;
                            //txtBSRCode.ReadOnly = true;
                        }
                        else
                        {
                           // chkBookAdjustment.Checked = false;
                            //txtChallanNo.ReadOnly = false;
                            //txtBSRCode.ReadOnly = false;
                        }
                    }

                    reader.Close();
                    reader.Dispose();

                    ////-- NIL CHALLAN STATEMENT
                    if (txtChallanTax.Text == "0.00")
                    {
                        //if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) FROM COR_HDR_CHALLAN WHERE BATCH_HEADER_ID = " + iCount))) == 0)
                        //    chkNilStatement.Checked = true;
                        //else
                        //    chkNilStatement.Checked = false;
                    }
                    //else
                    //    chkNilStatement.Checked = false;

                    strSQL = "SELECT DISTINCT TOP 3 DEDUCTEE_PAN," +
                             "                      TOTAL_AMOUNT," +
                             "                      INVALID_PAN " +
                             "FROM COR_HDR_DEDUCTEE_DETAILS " +
                             "WHERE BATCH_HEADER_ID = " + iCount + " " +
                        "AND   HDR_CHALLAN_ID  = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                    //" ";

                    reader = dmlService.J_ExecSqlReturnReader(strSQL);

                    int intInvalidPAN = 0;

                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i == 0)
                        {
                            txtDeducteePAN1.Text = reader["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(reader["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                        else if (i == 1)
                        {
                            txtDeducteePAN2.Text = reader["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(reader["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                        else if (i == 2)
                        {
                            txtDeducteePAN3.Text = reader["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(reader["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                    }

                    reader.Close();
                    reader.Dispose();
                    //--
                    //if (intInvalidPAN == 3)
                    //    chkNoValidPAN.Checked = true;
                    ////--
                    //blnDataEnteredbyUser = false;

                    btnChangeCDDDDetails.Visible = true;

                    this.Cursor = Cursors.Default;

                }
                #endregion
                else
                {
                    #region REGULAR RETURN
                    //now checking if the RETURN EXISTS IN THE ORIGINAL RETURN TABLE

                    strSQL = "SELECT BASIC_INFO_ID " +
                             "FROM   TRN_BASIC_INFO, " +
                             "       MST_COMPANY " +
                             "WHERE  MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID " +
                             "AND    TRN_BASIC_INFO.FORM_NO = '" + cmbFormNo.Text + "' " +
                             "AND    TRN_BASIC_INFO.QTR     = '" + cmbQtr.Text + "' " +
                             "AND    TRN_BASIC_INFO.ASST_ID = " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                             "AND    MST_COMPANY.TAN_NO = '" + txtTANNo.Text + "' ";

                    iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                    if (iCount > 0)
                    {
                        //SELECTING THE PROVISIONAL RECEIPT NO

                        strSQL = "SELECT PRN_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + iCount;

                        txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //-- GETTING CHALLAN NO OF MAX DEDUCTEES
                        //strSQL = "SELECT TOP 1 TRN_CHALLAN.CHALLAN_ID " +
                        //       "FROM  TRN_CHALLAN, " +
                        //       "      TRN_DEDUCTEE_DETAILS " +
                        //       "WHERE TRN_CHALLAN.CHALLAN_ID    = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                        //       "AND   TRN_CHALLAN.BASIC_INFO_ID = " + iCount + " " +
                        //       "GROUP BY TRN_CHALLAN.CHALLAN_ID " +
                        //       "ORDER BY COUNT(TRN_DEDUCTEE_DETAILS.CHALLAN_ID) DESC";
                        //-- ANIK @ 2015/12/16
                        strSQL = "SELECT TOP 1 TRN_CHALLAN.CHALLAN_ID " +
                                 "FROM  TRN_CHALLAN LEFT JOIN TRN_DEDUCTEE_DETAILS " +
                                 "ON    TRN_CHALLAN.CHALLAN_ID    = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                                 "WHERE TRN_CHALLAN.BASIC_INFO_ID = " + iCount + " " +
                                 "GROUP BY TRN_CHALLAN.CHALLAN_ID " +
                                 "ORDER BY COUNT(TRN_DEDUCTEE_DETAILS.CHALLAN_ID) DESC";
                        txtChallanID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                        //--                            
                        //NOW SELECTING THE CHALLAN RECORD FROM THE REGULAR RETURN TABLE
                        string[,] strTVoucherChallanNo = {{"BOOK_ENTRY =  0" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  1" , "F", "TRANSFER_VOUCHER_NO", "F"}};
                        //
                        strSQL = "SELECT TOP 1 " + cmnService.J_SQLDBFormat(strTVoucherChallanNo, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CHALLAN_NO, " +
                                 "       SL_NO," + //-- 2014/12/06
                                 "       BSR_CODE, " +
                                 "       DEPOSIT_DATE, " +
                                 "       TOT_TAX," +
                                 "       BOOK_ENTRY " +
                                 "FROM   TRN_CHALLAN " +
                                 "WHERE  BASIC_INFO_ID = " + iCount + " " +
                                 "AND    CHALLAN_ID    = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                        //
                     IDataReader reader = dmlService.J_ExecSqlReturnReader(strSQL);
                        //
                        if (reader == null)
                            return;
                        //
                        while (reader.Read())
                        {
                            txtSlNo.Text = reader["SL_NO"].ToString(); //-- 2014/12/06
                            txtChallanNo.Text = reader["CHALLAN_NO"].ToString();
                            txtBSRCode.Text = reader["BSR_CODE"].ToString();
                            mskChallanDate.Text = reader["DEPOSIT_DATE"].ToString();
                            txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOT_TAX"]));
                            //-- for BOOK ENTRY
                            //if (cmnService.J_ReturnInt32Value(Convert.ToString(reader["BOOK_ENTRY"])) > 0)
                            //    chkBookAdjustment.Checked = true;
                            //else
                            //    chkBookAdjustment.Checked = false;
                        }
                        //
                        reader.Close();
                        reader.Dispose();
                        ////-- NIL CHALLAN STATEMENT
                        if (txtChallanTax.Text == "0.00")
                        {
                            //if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + iCount))) == 0)
                            //    chkNilStatement.Checked = true;
                            //else
                            //    chkNilStatement.Checked = false;
                        }
                        //else
                        //    chkNilStatement.Checked = false;

                        //NOW SELECTING ANY 3 DEDUCTEE RECORD FOR THAT RETURN

                        string strDeducteeTableName = "MST_DEDUCTEE";
                        string strDeducteeIdName = "DEDUCTEE_ID";
                        string strDeducteePANName = "DEDUCTEE_PAN";

                        if (cmbFormNo.Text == T_FormNo.F24Q)
                        {
                            strDeducteeTableName = "MST_EMPLOYEE";
                            strDeducteeIdName = "EMPLOYEE_ID";
                            strDeducteePANName = "EMPLOYEE_PAN";
                        }

                        strSQL = "SELECT DISTINCT TOP 3  " + strDeducteeTableName + "." + strDeducteePANName + " AS DEDUCTEE_PAN, " +
                                 "       TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT AS TOTAL_AMOUNT " +
                                 "FROM   TRN_DEDUCTEE_DETAILS, " +
                                 "       " + strDeducteeTableName + " " +
                                 "WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strDeducteeTableName + "." + strDeducteeIdName + " " +
                                 "AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + iCount + " " +
                                "AND   TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                        reader = dmlService.J_ExecSqlReturnReader(strSQL);

                        int intInvalidPAN = 0;

                        for (int i = 0; reader.Read(); i++)
                        {
                            if (i == 0)
                            {
                                txtDeducteePAN1.Text = reader["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 1)
                            {
                                txtDeducteePAN2.Text = reader["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 2)
                            {
                                txtDeducteePAN3.Text = reader["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                        }

                        reader.Close();
                        reader.Dispose();
                        //--
                        //if (intInvalidPAN == 3)
                        //    chkNoValidPAN.Checked = true;
                        ////--
                        //blnDataEnteredbyUser = false;

                        btnChangeCDDDDetails.Visible = true;

                        this.Cursor = Cursors.Default;
                    }
                    #endregion
                    else
                    {
                        #region DATA DOES NOT EXIST
                        //DATA DOES NOT EXIST IN BOTH CORRECTION AS WELL AS REGULAR
                        ClearChallanDeducteeDetails();
                        #endregion
                    }
                }
            }
        }


        #endregion


        #region ClearChallanDeducteeDetails
        public void ClearChallanDeducteeDetails()
        {            
                txtSlNo.Text = "";
                txtChallanNo.Text = "";
                txtBSRCode.Text = "";
                mskChallanDate.Text = "";
                txtChallanTax.Text = "0.00";
                txtChallanID.Text = "";

                txtDeducteePAN1.Text = "";
                txtDeducteePAN2.Text = "";
                txtDeducteePAN3.Text = "";

                txtTDSDeducted1.Text = "0.00";
                txtTDSDeducted2.Text = "0.00";
                txtTDSDeducted3.Text = "0.00";
                //                
                txtTokenNo.Text = "";
            
        }
        #endregion

        #region btnChangeCDDDDetails_Click
        private void btnChangeCDDDDetails_Click(object sender, EventArgs e)
        {
            FetchCDDDDetails(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex), cmbFormNo.Text, cmbQtr.Text, txtTANNo.Text);
        }
        #endregion

        #region FetchCDDDDetails
        public void FetchCDDDDetails(long FaYearID, string FormNo, string Qtr, string TAN)
        {
            IDataReader rdFetchCDDDDetails = null;
            try
            {
                if (txtChallanID.Text == "")
                {
                    strChallanIDQueue = "";
                    //if (btnChangeCDDDDetails.Visible == true)
                    //{
                    //    cmnService.J_UserMessage("Please click once again...");
                    //strChallanIDQueue = txtChallanID.Text;
                    //return;
                    //}
                    //strSQL = @"SELECT HDR_CHALLAN_ID 
                    //           FROM   COR_HDR_CHALLAN, 
                    //                  COR_HDR_BATCH,
                    //                  COR_HDR_COMPANY 
                    //           WHERE  COR_HDR_CHALLAN.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID 
                    //           AND    COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID 
                    //           AND    COR_HDR_BATCH.ASST_ID           = " + FaYearID + " " +
                    //          "AND    COR_HDR_BATCH.FORM_NO            ='" + FormNo + "' " +
                    //          "AND    COR_HDR_BATCH.QTR                ='" + Qtr + "' " +
                    //          "AND    COR_HDR_COMPANY.TAN_NO           ='" + TAN + "' " +
                    //          "AND    COR_HDR_CHALLAN.SL_NO            = " + txtSlNo.Text + @"
                    //           AND    COR_HDR_CHALLAN.BSR_CODE         ='" + txtBSRCode.Text + @"'
                    //           AND    COR_HDR_CHALLAN.CHALLAN_NO       ='" + txtChallanNo.Text + @"'
                    //           AND    COR_HDR_CHALLAN.TOT_TAX          = " + cmnService.J_ReturnDoubleValue(txtChallanTax.Text);
                    //strChallanIDQueue = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //strChallanIDQueue = txtChallanID.Text;
                    //@@@@@@@@@@@@@@@@@@@@
                    strSQL = "SELECT TOP 1 COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "FROM  COR_HDR_BATCH, " +
                         "      COR_HDR_COMPANY " +
                         "WHERE COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "AND   COR_HDR_BATCH.ASST_ID           = " + FaYearID + " " +
                         "AND   COR_HDR_BATCH.FORM_NO           = '" + FormNo + "' " +
                         "AND   COR_HDR_BATCH.QTR               = '" + Qtr + "' " +
                         "AND   COR_HDR_COMPANY.TAN_NO          = '" + TAN + "' " +
                         "ORDER BY COR_HDR_BATCH.BATCH_HEADER_ID DESC";

                    iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                    if (iCount > 0)
                    {
                        //DATA NOT FOUND IN CORRECTION RETURN DATA
                        ClearChallanDeducteeDetailsCDDDDetails();

                        //DATA IS AVAILABLE IN CORRECTION RETURN

                        //SELECTING THE PROVISIONAL RECEIPT NO

                        strSQL = "SELECT ORIGINAL_RRR_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + iCount;
                        txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //-- GETTING CHALLAN NO OF MAX DEDUCTEES
                        strSQL = "SELECT TOP 1 COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                               "FROM  COR_HDR_CHALLAN, " +
                               "      COR_HDR_DEDUCTEE_DETAILS " +
                               "WHERE COR_HDR_CHALLAN.HDR_CHALLAN_ID = COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID " +
                               "AND   COR_HDR_CHALLAN.BATCH_HEADER_ID = " + iCount + " ";
                        //if (strChallanIDQueue != "")
                        //    strSQL = strSQL + " AND COR_HDR_CHALLAN.HDR_CHALLAN_ID NOT IN (" + strChallanIDQueue + " )";
                        strSQL = strSQL + " GROUP BY COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                               "ORDER BY COUNT(COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID) DESC";
                        txtChallanID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //--
                        //NOW SELECTING THE CHALLAN RECORD FROM THE REGULAR RETURN TABLE

                        string[,] strTVoucherChallanNo = {{"BOOK_ENTRY =  0" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  2" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  1" , "F", "TRANSFER_VOUCHER_NO", "F"}};

                        strSQL = "SELECT TOP 1 " + cmnService.J_SQLDBFormat(strTVoucherChallanNo, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CHALLAN_NO, " +
                                 "       SL_NO, " + //-- 2014/12/06
                                 "       BSR_CODE, " +
                                 "       DEPOSIT_DATE, " +
                                 "       TOT_TAX," +
                                 "       BOOK_ENTRY " +
                                 "FROM   COR_HDR_CHALLAN " +
                                 "WHERE  BATCH_HEADER_ID = " + iCount + " " +
                                 "AND    HDR_CHALLAN_ID  = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);

                        if (rdFetchCDDDDetails == null)
                            return;

                        while (rdFetchCDDDDetails.Read())
                        {
                            txtSlNo.Text = rdFetchCDDDDetails["SL_NO"].ToString(); //-- 2014/12/06
                            txtChallanNo.Text = rdFetchCDDDDetails["CHALLAN_NO"].ToString();
                            txtBSRCode.Text = rdFetchCDDDDetails["BSR_CODE"].ToString();
                            mskChallanDate.Text = rdFetchCDDDDetails["DEPOSIT_DATE"].ToString();
                            txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOT_TAX"]));
                            //-- for BOOK ENTRY
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["BOOK_ENTRY"])) > 0)
                            {
                                //chkBookAdjustment.Checked = true;
                                //-- 2013/08/30
                                //txtChallanNo.Text = "";
                                //txtBSRCode.Text = "";
                                //txtChallanNo.ReadOnly = true;
                                //txtBSRCode.ReadOnly = true;
                            }
                            else
                            {
                                //chkBookAdjustment.Checked = false;
                                //txtChallanNo.ReadOnly = false;
                                //txtBSRCode.ReadOnly = false;
                            }
                        }

                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();

                        ////-- NIL CHALLAN STATEMENT
                        //if (txtChallanTax.Text == "0.00")
                        //{
                        //    if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) FROM COR_HDR_CHALLAN WHERE BATCH_HEADER_ID = " + iCount))) == 0)
                        //        chkNilStatement.Checked = true;
                        //    else
                        //        chkNilStatement.Checked = false;
                        //}
                        //else
                        //    chkNilStatement.Checked = false;

                        strSQL = "SELECT DISTINCT TOP 3 DEDUCTEE_PAN," +
                                 "                      TOTAL_AMOUNT," +
                                 "                      INVALID_PAN " +
                                 "FROM COR_HDR_DEDUCTEE_DETAILS " +
                                 "WHERE BATCH_HEADER_ID = " + iCount + " " +
                            "AND   HDR_CHALLAN_ID  = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                        //" ";

                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);

                        int intInvalidPAN = 0;

                        for (int i = 0; rdFetchCDDDDetails.Read(); i++)
                        {
                            if (i == 0)
                            {
                                txtDeducteePAN1.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 1)
                            {
                                txtDeducteePAN2.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 2)
                            {
                                txtDeducteePAN3.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                        }

                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();
                        //--
                        //if (intInvalidPAN == 3)
                        //    chkNoValidPAN.Checked = true;
                        //--
                        blnDataEnteredbyUser = false;

                        this.Cursor = Cursors.Default;
                        //
                        btnChangeCDDDDetails.Visible = true;
                        cmnService.J_UserMessage("Please click once again...");
                        return;
                        //
                    }
                    #region REGULAR RETURN
                    //now checking if the RETURN EXISTS IN THE ORIGINAL RETURN TABLE

                    strSQL = "SELECT BASIC_INFO_ID " +
                             "FROM   TRN_BASIC_INFO, " +
                             "       MST_COMPANY " +
                             "WHERE  MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID " +
                             "AND    TRN_BASIC_INFO.FORM_NO = '" + cmbFormNo.Text + "' " +
                             "AND    TRN_BASIC_INFO.QTR     = '" + cmbQtr.Text + "' " +
                             "AND    TRN_BASIC_INFO.ASST_ID = " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                             "AND    MST_COMPANY.TAN_NO = '" + txtTANNo.Text + "' ";

                    iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                    if (iCount > 0)
                    {
                        //SELECTING THE PROVISIONAL RECEIPT NO

                        strSQL = "SELECT PRN_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + iCount;

                        txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //-- GETTING CHALLAN NO OF MAX DEDUCTEES
                        //strSQL = "SELECT TOP 1 TRN_CHALLAN.CHALLAN_ID " +
                        //       "FROM  TRN_CHALLAN, " +
                        //       "      TRN_DEDUCTEE_DETAILS " +
                        //       "WHERE TRN_CHALLAN.CHALLAN_ID    = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                        //       "AND   TRN_CHALLAN.BASIC_INFO_ID = " + iCount + " " +
                        //       "GROUP BY TRN_CHALLAN.CHALLAN_ID " +
                        //       "ORDER BY COUNT(TRN_DEDUCTEE_DETAILS.CHALLAN_ID) DESC";
                        //-- ANIK @ 2015/12/16
                        strSQL = "SELECT TOP 1 TRN_CHALLAN.CHALLAN_ID " +
                                 "FROM  TRN_CHALLAN LEFT JOIN TRN_DEDUCTEE_DETAILS " +
                                 "ON    TRN_CHALLAN.CHALLAN_ID    = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                                 "WHERE TRN_CHALLAN.BASIC_INFO_ID = " + iCount + " ";
                        //if (strChallanIDQueue != "")
                        //    strSQL = strSQL + " AND TRN_CHALLAN.CHALLAN_ID NOT IN (" + strChallanIDQueue + " )";
                        strSQL = strSQL + " GROUP BY TRN_CHALLAN.CHALLAN_ID " +
                                 "ORDER BY COUNT(TRN_DEDUCTEE_DETAILS.CHALLAN_ID) DESC";
                        txtChallanID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                        //--                            
                        //NOW SELECTING THE CHALLAN RECORD FROM THE REGULAR RETURN TABLE
                        string[,] strTVoucherChallanNo = {{"BOOK_ENTRY =  0" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  1" , "F", "TRANSFER_VOUCHER_NO", "F"}};
                        //
                        strSQL = "SELECT TOP 1 " + cmnService.J_SQLDBFormat(strTVoucherChallanNo, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CHALLAN_NO, " +
                                 "       SL_NO," + //-- 2014/12/06
                                 "       BSR_CODE, " +
                                 "       DEPOSIT_DATE, " +
                                 "       TOT_TAX," +
                                 "       BOOK_ENTRY " +
                                 "FROM   TRN_CHALLAN " +
                                 "WHERE  BASIC_INFO_ID = " + iCount + " " +
                                 "AND    CHALLAN_ID    = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                        //
                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                        //
                        if (rdFetchCDDDDetails == null)
                            return;
                        //
                        while (rdFetchCDDDDetails.Read())
                        {
                            txtSlNo.Text = rdFetchCDDDDetails["SL_NO"].ToString(); //-- 2014/12/06
                            txtChallanNo.Text = rdFetchCDDDDetails["CHALLAN_NO"].ToString();
                            txtBSRCode.Text = rdFetchCDDDDetails["BSR_CODE"].ToString();
                            mskChallanDate.Text = rdFetchCDDDDetails["DEPOSIT_DATE"].ToString();
                            txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOT_TAX"]));
                            //-- for BOOK ENTRY
                            //if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["BOOK_ENTRY"])) > 0)
                            //    chkBookAdjustment.Checked = true;
                            //else
                            //    chkBookAdjustment.Checked = false;
                        }
                        //
                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();
                        ////-- NIL CHALLAN STATEMENT
                        //if (txtChallanTax.Text == "0.00")
                        //{
                        //    if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + iCount))) == 0)
                        //        chkNilStatement.Checked = true;
                        //    else
                        //        chkNilStatement.Checked = false;
                        //}
                        //else
                        //    chkNilStatement.Checked = false;

                        //NOW SELECTING ANY 3 DEDUCTEE RECORD FOR THAT RETURN

                        string strDeducteeTableName = "MST_DEDUCTEE";
                        string strDeducteeIdName = "DEDUCTEE_ID";
                        string strDeducteePANName = "DEDUCTEE_PAN";

                        if (cmbFormNo.Text == T_FormNo.F24Q)
                        {
                            strDeducteeTableName = "MST_EMPLOYEE";
                            strDeducteeIdName = "EMPLOYEE_ID";
                            strDeducteePANName = "EMPLOYEE_PAN";
                        }

                        //strSQL = "SELECT DISTINCT TOP 3  " + strDeducteeTableName + "." + strDeducteePANName + " AS DEDUCTEE_PAN, " +
                        //         "             TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT AS TOTAL_AMOUNT " +
                        //         "FROM TRN_DEDUCTEE_DETAILS, " +
                        //         "     " + strDeducteeTableName + " " +
                        //         "WHERE TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strDeducteeTableName + "." + strDeducteeIdName + " " +
                        //         "AND   TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + iCount + " " +
                        //         "AND   TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                        strSQL = "SELECT DISTINCT TOP 3  " + strDeducteeTableName + "." + strDeducteePANName + " AS DEDUCTEE_PAN, " +
                                 "       TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT AS TOTAL_AMOUNT " +
                                 "FROM   TRN_DEDUCTEE_DETAILS, " +
                                 "       " + strDeducteeTableName + " " +
                                 "WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strDeducteeTableName + "." + strDeducteeIdName + " " +
                                 "AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + iCount + " " +
                                 "AND   TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                        int intInvalidPAN = 0;
                        //
                        for (int i = 0; rdFetchCDDDDetails.Read(); i++)
                        {
                            if (i == 0)
                            {
                                txtDeducteePAN1.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 1)
                            {
                                txtDeducteePAN2.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 2)
                            {
                                txtDeducteePAN3.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                        }

                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();
                        //--
                        //if (intInvalidPAN == 3)
                        //    chkNoValidPAN.Checked = true;
                        //--
                        blnDataEnteredbyUser = false;

                        this.Cursor = Cursors.Default;
                        //
                        btnChangeCDDDDetails.Visible = true;
                        //

                    }
                    #endregion
                    //@@@@@@@@@@@@@@@@@@@@
                    if (btnChangeCDDDDetails.Visible == true)
                    {
                        cmnService.J_UserMessage("Please click once again...");
                        return;
                    }
                }
                else if (strChallanIDQueue == "")
                    strChallanIDQueue = txtChallanID.Text;
                else
                    strChallanIDQueue = strChallanIDQueue + "," + txtChallanID.Text;
                //
                #region CHECK FROM CORRECTION
                //FIRST CHECKING THE RECORD TO BE PRESENT IN THE CORRECTION RETURN TABLE

                strSQL = "SELECT TOP 1 COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "FROM  COR_HDR_BATCH, " +
                         "      COR_HDR_COMPANY " +
                         "WHERE COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "AND   COR_HDR_BATCH.ASST_ID           = " + FaYearID + " " +
                         "AND   COR_HDR_BATCH.FORM_NO           = '" + FormNo + "' " +
                         "AND   COR_HDR_BATCH.QTR               = '" + Qtr + "' " +
                         "AND   COR_HDR_COMPANY.TAN_NO          = '" + TAN + "' " +
                         "ORDER BY COR_HDR_BATCH.BATCH_HEADER_ID DESC";

                iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                if (iCount > 0)
                {
                    //DATA NOT FOUND IN CORRECTION RETURN DATA
                    ClearChallanDeducteeDetailsCDDDDetails();

                    //DATA IS AVAILABLE IN CORRECTION RETURN

                    //SELECTING THE PROVISIONAL RECEIPT NO

                    //strSQL = "SELECT ORIGINAL_RRR_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + iCount;
                    //txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                    //-- GETTING CHALLAN NO OF MAX DEDUCTEES
                    strSQL = "SELECT TOP 1 COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                           "FROM  COR_HDR_CHALLAN, " +
                           "      COR_HDR_DEDUCTEE_DETAILS " +
                           "WHERE COR_HDR_CHALLAN.HDR_CHALLAN_ID = COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID " +
                           "AND   COR_HDR_CHALLAN.BATCH_HEADER_ID = " + iCount + " ";
                    if (strChallanIDQueue != "")
                        strSQL = strSQL + " AND COR_HDR_CHALLAN.HDR_CHALLAN_ID NOT IN (" + strChallanIDQueue + " )";
                    strSQL = strSQL + " GROUP BY COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                           "ORDER BY COUNT(COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID) DESC";
                    txtChallanID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                    //--
                    //NOW SELECTING THE CHALLAN RECORD FROM THE REGULAR RETURN TABLE

                    string[,] strTVoucherChallanNo = {{"BOOK_ENTRY =  0" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  2" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  1" , "F", "TRANSFER_VOUCHER_NO", "F"}};

                    strSQL = "SELECT TOP 1 " + cmnService.J_SQLDBFormat(strTVoucherChallanNo, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CHALLAN_NO, " +
                             "       SL_NO, " + //-- 2014/12/06
                             "       BSR_CODE, " +
                             "       DEPOSIT_DATE, " +
                             "       TOT_TAX," +
                             "       BOOK_ENTRY " +
                             "FROM   COR_HDR_CHALLAN " +
                             "WHERE  BATCH_HEADER_ID = " + iCount + " " +
                             "AND    HDR_CHALLAN_ID  = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                    rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);

                    if (rdFetchCDDDDetails == null)
                        return;

                    while (rdFetchCDDDDetails.Read())
                    {
                        txtSlNo.Text = rdFetchCDDDDetails["SL_NO"].ToString(); //-- 2014/12/06
                        txtChallanNo.Text = rdFetchCDDDDetails["CHALLAN_NO"].ToString();
                        txtBSRCode.Text = rdFetchCDDDDetails["BSR_CODE"].ToString();
                        mskChallanDate.Text = rdFetchCDDDDetails["DEPOSIT_DATE"].ToString();
                        txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOT_TAX"]));
                        //-- for BOOK ENTRY
                        if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["BOOK_ENTRY"])) > 0)
                        {
                            //chkBookAdjustment.Checked = true;
                            //-- 2013/08/30
                            //txtChallanNo.Text = "";
                            //txtBSRCode.Text = "";
                            //txtChallanNo.ReadOnly = true;
                            //txtBSRCode.ReadOnly = true;
                        }
                        else
                        {
                            //chkBookAdjustment.Checked = false;
                            //txtChallanNo.ReadOnly = false;
                            //txtBSRCode.ReadOnly = false;
                        }
                    }

                    rdFetchCDDDDetails.Close();
                    rdFetchCDDDDetails.Dispose();

                    ////-- NIL CHALLAN STATEMENT
                    //if (txtChallanTax.Text == "0.00")
                    //{
                    //    if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) FROM COR_HDR_CHALLAN WHERE BATCH_HEADER_ID = " + iCount))) == 0)
                    //        chkNilStatement.Checked = true;
                    //    else
                    //        chkNilStatement.Checked = false;
                    //}
                    //else
                    //    chkNilStatement.Checked = false;

                    strSQL = "SELECT DISTINCT TOP 3 DEDUCTEE_PAN," +
                             "                      TOTAL_AMOUNT," +
                             "                      INVALID_PAN " +
                             "FROM COR_HDR_DEDUCTEE_DETAILS " +
                             "WHERE BATCH_HEADER_ID = " + iCount + " " +
                        "AND   HDR_CHALLAN_ID  = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                    //" ";

                    rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);

                    int intInvalidPAN = 0;

                    for (int i = 0; rdFetchCDDDDetails.Read(); i++)
                    {
                        if (i == 0)
                        {
                            txtDeducteePAN1.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                        else if (i == 1)
                        {
                            txtDeducteePAN2.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                        else if (i == 2)
                        {
                            txtDeducteePAN3.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                    }

                    rdFetchCDDDDetails.Close();
                    rdFetchCDDDDetails.Dispose();
                    //--
                    //if (intInvalidPAN == 3)
                    //    chkNoValidPAN.Checked = true;
                    //--
                    blnDataEnteredbyUser = false;

                    this.Cursor = Cursors.Default;
                    //
                    btnChangeCDDDDetails.Visible = true;
                    //
                }
                #endregion
                else
                {
                    #region REGULAR RETURN
                    //now checking if the RETURN EXISTS IN THE ORIGINAL RETURN TABLE

                    strSQL = "SELECT BASIC_INFO_ID " +
                             "FROM   TRN_BASIC_INFO, " +
                             "       MST_COMPANY " +
                             "WHERE  MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID " +
                             "AND    TRN_BASIC_INFO.FORM_NO = '" + cmbFormNo.Text + "' " +
                             "AND    TRN_BASIC_INFO.QTR     = '" + cmbQtr.Text + "' " +
                             "AND    TRN_BASIC_INFO.ASST_ID = " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                             "AND    MST_COMPANY.TAN_NO = '" + txtTANNo.Text + "' ";

                    iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                    if (iCount > 0)
                    {
                        //SELECTING THE PROVISIONAL RECEIPT NO

                        ////strSQL = "SELECT PRN_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + iCount;

                        ////txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //-- GETTING CHALLAN NO OF MAX DEDUCTEES
                        //strSQL = "SELECT TOP 1 TRN_CHALLAN.CHALLAN_ID " +
                        //       "FROM  TRN_CHALLAN, " +
                        //       "      TRN_DEDUCTEE_DETAILS " +
                        //       "WHERE TRN_CHALLAN.CHALLAN_ID    = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                        //       "AND   TRN_CHALLAN.BASIC_INFO_ID = " + iCount + " " +
                        //       "GROUP BY TRN_CHALLAN.CHALLAN_ID " +
                        //       "ORDER BY COUNT(TRN_DEDUCTEE_DETAILS.CHALLAN_ID) DESC";
                        //-- ANIK @ 2015/12/16
                        strSQL = "SELECT TOP 1 TRN_CHALLAN.CHALLAN_ID " +
                                 "FROM  TRN_CHALLAN LEFT JOIN TRN_DEDUCTEE_DETAILS " +
                                 "ON    TRN_CHALLAN.CHALLAN_ID    = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                                 "WHERE TRN_CHALLAN.BASIC_INFO_ID = " + iCount + " ";
                        if (strChallanIDQueue != "")
                            strSQL = strSQL + " AND TRN_CHALLAN.CHALLAN_ID NOT IN (" + strChallanIDQueue + " )";
                        strSQL = strSQL + " GROUP BY TRN_CHALLAN.CHALLAN_ID " +
                                 "ORDER BY COUNT(TRN_DEDUCTEE_DETAILS.CHALLAN_ID) DESC";
                        txtChallanID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                        //--                            
                        //NOW SELECTING THE CHALLAN RECORD FROM THE REGULAR RETURN TABLE
                        string[,] strTVoucherChallanNo = {{"BOOK_ENTRY =  0" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  1" , "F", "TRANSFER_VOUCHER_NO", "F"}};
                        //
                        strSQL = "SELECT TOP 1 " + cmnService.J_SQLDBFormat(strTVoucherChallanNo, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CHALLAN_NO, " +
                                 "       SL_NO," + //-- 2014/12/06
                                 "       BSR_CODE, " +
                                 "       DEPOSIT_DATE, " +
                                 "       TOT_TAX," +
                                 "       BOOK_ENTRY " +
                                 "FROM   TRN_CHALLAN " +
                                 "WHERE  BASIC_INFO_ID = " + iCount + " " +
                                 "AND    CHALLAN_ID    = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                        //
                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                        //
                        if (rdFetchCDDDDetails == null)
                            return;
                        //
                        while (rdFetchCDDDDetails.Read())
                        {
                            txtSlNo.Text = rdFetchCDDDDetails["SL_NO"].ToString(); //-- 2014/12/06
                            txtChallanNo.Text = rdFetchCDDDDetails["CHALLAN_NO"].ToString();
                            txtBSRCode.Text = rdFetchCDDDDetails["BSR_CODE"].ToString();
                            mskChallanDate.Text = rdFetchCDDDDetails["DEPOSIT_DATE"].ToString();
                            txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOT_TAX"]));
                            //-- for BOOK ENTRY
                            //if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["BOOK_ENTRY"])) > 0)
                            //    chkBookAdjustment.Checked = true;
                            //else
                            //    chkBookAdjustment.Checked = false;
                        }
                        //
                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();
                        ////-- NIL CHALLAN STATEMENT
                        //if (txtChallanTax.Text == "0.00")
                        //{
                        //    if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + iCount))) == 0)
                        //        chkNilStatement.Checked = true;
                        //    else
                        //        chkNilStatement.Checked = false;
                        //}
                        //else
                        //    chkNilStatement.Checked = false;

                        //NOW SELECTING ANY 3 DEDUCTEE RECORD FOR THAT RETURN

                        string strDeducteeTableName = "MST_DEDUCTEE";
                        string strDeducteeIdName = "DEDUCTEE_ID";
                        string strDeducteePANName = "DEDUCTEE_PAN";

                        if (cmbFormNo.Text == T_FormNo.F24Q)
                        {
                            strDeducteeTableName = "MST_EMPLOYEE";
                            strDeducteeIdName = "EMPLOYEE_ID";
                            strDeducteePANName = "EMPLOYEE_PAN";
                        }

                        //strSQL = "SELECT DISTINCT TOP 3  " + strDeducteeTableName + "." + strDeducteePANName + " AS DEDUCTEE_PAN, " +
                        //         "             TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT AS TOTAL_AMOUNT " +
                        //         "FROM TRN_DEDUCTEE_DETAILS, " +
                        //         "     " + strDeducteeTableName + " " +
                        //         "WHERE TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strDeducteeTableName + "." + strDeducteeIdName + " " +
                        //         "AND   TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + iCount + " " +
                        //         "AND   TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                        strSQL = "SELECT DISTINCT TOP 3  " + strDeducteeTableName + "." + strDeducteePANName + " AS DEDUCTEE_PAN, " +
                                 "       TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT AS TOTAL_AMOUNT " +
                                 "FROM   TRN_DEDUCTEE_DETAILS, " +
                                 "       " + strDeducteeTableName + " " +
                                 "WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strDeducteeTableName + "." + strDeducteeIdName + " " +
                                 "AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + iCount + " " +
                                 "AND   TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                        int intInvalidPAN = 0;
                        //
                        for (int i = 0; rdFetchCDDDDetails.Read(); i++)
                        {
                            if (i == 0)
                            {
                                txtDeducteePAN1.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 1)
                            {
                                txtDeducteePAN2.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 2)
                            {
                                txtDeducteePAN3.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                        }

                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();
                        //--
                        //if (intInvalidPAN == 3)
                        //    chkNoValidPAN.Checked = true;
                        //--
                        blnDataEnteredbyUser = false;

                        this.Cursor = Cursors.Default;
                        //
                        btnChangeCDDDDetails.Visible = true;
                        //

                    }
                    #endregion
                    else
                    {
                        #region DATA DOES NOT EXIST
                        //DATA DOES NOT EXIST IN BOTH CORRECTION AS WELL AS REGULAR
                        ClearChallanDeducteeDetailsCDDDDetails();
                        //
                        btnChangeCDDDDetails.Visible = false;
                        //

                        #endregion
                    }
                }
                //--
                if (strChallanIDQueue != "" && txtSlNo.Text == "")
                {
                    FetchCDDDDetails(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex), cmbFormNo.Text, cmbQtr.Text, txtTANNo.Text);
                    //cmnService.J_UserMessage("Please click once again...");
                    return;
                }
            }
            catch (Exception err)
            {

            }
        }
        #endregion

        #region ClearChallanDeducteeDetailsCDDDDetails
        public void ClearChallanDeducteeDetailsCDDDDetails()
        {
            if (blnDataEnteredbyUser == false)
            {
                txtSlNo.Text = "";
                txtChallanNo.Text = "";
                txtBSRCode.Text = "";
                mskChallanDate.Text = "";
                txtChallanTax.Text = "0.00";
                txtChallanID.Text = "";

                txtDeducteePAN1.Text = "";
                txtDeducteePAN2.Text = "";
                txtDeducteePAN3.Text = "";

                txtTDSDeducted1.Text = "0.00";
                txtTDSDeducted2.Text = "0.00";
                txtTDSDeducted3.Text = "0.00";
                //
                //chkBookAdjustment.Checked = false;
                //chkNilStatement.Checked = false;
                //chkNoValidPAN.Checked = false;
                ////
                //txtTokenNo.Text = "";
            }
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0093", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        #endregion

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            //InitializeCaptcha();
        }
    }


}

