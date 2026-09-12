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
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Diagnostics;

using System.Runtime.InteropServices;




//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnSaveIDandPasswdTracesIT : Form
    {
       
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnSaveIDandPasswdTracesIT()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        #region Objects & Variables declaration

        TracesConnect objTracesConnect = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;
        string strInvalidPAN = "";
        string strUnmatchedPAN = "";
        long lngBasicInfoID = 0;
        //--            
        ToolTip tllTip = new ToolTip();
        string strEmpDed = "";
        string strbtnVerification = "&Start Validation";
        //string strLogOff = "Log off";
        //string strLogOn = "Log on";
        //
        bool blnStatus = false;
        bool blnVerificationComplete = false;
        long lngDeducteeID = 0;
        bool blRegular = true;
        bool blnSelectComboExit = false;
        //
        int intPANId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0;
        int intVerifyId = 0;
        //
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNoBkmark = "";
        string strFAYear = "", strOrderBy = "", strQuery = "" ;
        //
        int j = 0;
        //
        BindingSource objBindingSource = new BindingSource();
        private BindingList<PANDetails> _results = new BindingList<PANDetails>();
        //
        enum enmRequestType
        {
            Login,
            PanValidation,
            LogOff
        }
        //
        string strNOTAVAILABLE = "NOT AVAILABLE";
        //--
        #endregion

        #region User Defined Events

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

        #region TrnDeleteReturn_Load
        private void TrnDeleteReturn_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            try
            {
                //--
                rbnTraces_CheckedChanged(sender, e);
                //--
                //
                this.Cursor = Cursors.Default;
                //-----------
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion


        #region rbnTraces_CheckedChanged
        private void rbnTraces_CheckedChanged(object sender, EventArgs e)
        {
            if(rbnTraces.Checked == true)
            {
                grpTracesDetails.Visible = true;
                txtTracesTANNo.Select();
                txtTracesTANNo.Text = "";
                txtTracesUserID.Text = "";
                txtTracesPassword.Text = "";
                grpeFilingDetails.Visible = false;
            }
            else if(rbneFiling.Checked == true)
            {
                grpTracesDetails.Visible = false;
                grpeFilingDetails.Visible = true;
                txteFilingTANNo.Select();
                txteFilingTANNo.Text = "";
                txteFilingPassword.Text = "";
            }
        }
        #endregion

        #region lstTracesHelp_Click
        private void lstTracesHelp_Click(object sender, EventArgs e)
        {
            long lngTracesId = Convert.ToInt32(Support.GetItemData(lstTracesHelp, lstTracesHelp.SelectedIndex));
            txtTracesTANNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngTracesId));
            txtTracesUserID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngTracesId));
            txtTracesPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngTracesId));
            //--
            lstTracesHelp.Visible = false;
            //--
            //txtCaptchaCode.Select();
        }
        #endregion

        #region lsteFilingHelp_Click
        private void lsteFilingHelp_Click(object sender, EventArgs e)
        {
            //--
            long lngeFilingId = Convert.ToInt32(Support.GetItemData(lsteFilingHelp, lsteFilingHelp.SelectedIndex));
            txteFilingTANNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_AADHAAR WHERE TAN_AADHAAR_ID = " + lngeFilingId));
            txteFilingPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_AADHAAR WHERE TAN_AADHAAR_ID = " + lngeFilingId));
            //--
            lsteFilingHelp.Visible = false;
            //--
        }
        #endregion

        #region lstTracesHelp_KeyPress
        private void lstTracesHelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                lstTracesHelp_Click(sender, e);
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstTracesHelp.Visible = false;
                txtTracesTANNo.Select();
            }
        }
        #endregion

        #region TrnSaveIDandPasswdTracesIT_Activated
        private void TrnSaveIDandPasswdTracesIT_Activated(object sender, EventArgs e)
        {
            //-- Added By Abhishek Dey On 20/08/2018 --
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtTracesPassword.UseSystemPasswordChar = true;
                txteFilingPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtTracesPassword.UseSystemPasswordChar = false;
                txteFilingPassword.UseSystemPasswordChar = false;
            }
        }
        #endregion

        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btnSaveTraces_Click
        private void btnSaveTraces_Click(object sender, EventArgs e)
        {
            if(rbnTraces.Checked==true)
            {
                strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTracesTANNo.Text) + "' ";
                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    //insering new record in the tan login master
                    strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txtTracesTANNo.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtTracesUserID.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtTracesPassword.Text) + "')";
                    dmlService.J_ExecSql(strSQL);
                }
                else
                {
                    strSQL = "UPDATE MST_TAN_ACCOUNT " +
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTracesTANNo.Text) + "', " +
                             "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtTracesUserID.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtTracesPassword.Text) + "' " +
                             "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTracesTANNo.Text) + "' ";
                    dmlService.J_ExecSql(strSQL);
                }
                //--
                txtTracesTANNo.Text = ""; txtTracesUserID.Text = ""; txtTracesPassword.Text = "";
                //--
                cmnService.J_UserMessage("Data Saved");
            }
        }
        #endregion

        #region btnSaveeFiling_Click
        private void btnSaveeFiling_Click(object sender, EventArgs e)
        {
            if (rbneFiling.Checked == true)
            {
                strSQL = "SELECT COUNT(*) FROM MST_TAN_AADHAAR WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txteFilingTANNo.Text) + "' ";
                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    //insering new record in the tan login master
                    strSQL = "INSERT INTO MST_TAN_AADHAAR(TAN_NO, USER_PASSWORD) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txteFilingTANNo.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txteFilingPassword.Text) + "')";

                    dmlService.J_ExecSql(strSQL);
                    //--
                }
                else
                {
                    //updating the existing record in the master

                    strSQL = "UPDATE MST_TAN_AADHAAR " +
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txteFilingTANNo.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txteFilingPassword.Text) + "' " +
                             "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txteFilingTANNo.Text) + "' ";

                    dmlService.J_ExecSql(strSQL);
                }
                //--
                txteFilingTANNo.Text = ""; txteFilingPassword.Text = "";
                //--
                cmnService.J_UserMessage("Data Saved");
            }
        }
        #endregion

        #region txteFilingTANNo_KeyPress
        private void txteFilingTANNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                txteFilingPassword.Select();
            else
                if (TdsMan.gTANNoPANNoValidation(txteFilingTANNo, e, T_TANPAN.TAN) == false)
                e.Handled = true;
        }
        #endregion

        #region txtTracesTANNo_KeyPress
        private void txtTracesTANNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                txtTracesUserID.Select();
            else
                if (TdsMan.gTANNoPANNoValidation(txtTracesTANNo, e, T_TANPAN.TAN) == false)
                e.Handled = true;
        }
        #endregion

        #region txtTracesTANNo_TextChanged
        private void txtTracesTANNo_TextChanged(object sender, EventArgs e)
        {
            IDataReader drdShowTracesHelp = null;
            //--
            try
            {
                if (txtTracesTANNo.Text.Trim() == "")
                {
                    lstTracesHelp.Visible = false;
                    return;
                }

                //if (blnShowHelp == false)
                //    return;
                //-----------------------
                strSQL = "SELECT TAN_ACCOUNT_ID," +
                         "       TAN_NO," +
                         "       LOGIN_ID," +
                         "       USER_PASSWORD," +
                         "       COMPANY_NAME " +
                         "FROM   MST_TAN_ACCOUNT " +
                         "WHERE  TAN_NO LIKE '" + cmnService.J_ReplaceQuote(txtTracesTANNo.Text) + "%' " +
                         "ORDER BY TAN_NO, TAN_ACCOUNT_ID";

                drdShowTracesHelp = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShowTracesHelp == null)
                {
                    lstTracesHelp.Visible = false;
                    return;
                }
                else
                {
                    lstTracesHelp.Items.Clear();
                    //lstDeducteeHelp.Height = 15;
                    lstTracesHelp.Visible = true;
                    lstTracesHelp.BringToFront();
                    while (drdShowTracesHelp.Read())
                    {
                        lstTracesHelp.Items.Add(new ListBoxItem(drdShowTracesHelp["TAN_NO"].ToString().PadRight(12) +
                                                                  drdShowTracesHelp["LOGIN_ID"].ToString().PadRight(15) +
                                                                  TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdShowTracesHelp["USER_PASSWORD"].ToString()).PadRight(10) +
                                                                  " " + drdShowTracesHelp["COMPANY_NAME"].ToString(),
                                                                  Convert.ToInt32(drdShowTracesHelp["TAN_ACCOUNT_ID"])));
                        //--
                        //if (lstDeducteeHelp.Height <= 300)
                        //    lstDeducteeHelp.Height = lstDeducteeHelp.Height + 19;
                    }
                    //--
                    if (lstTracesHelp.Items.Count <= 0)
                        lstTracesHelp.Visible = false;
                }
                //-----------------------------------------------------------
                drdShowTracesHelp.Close();
                drdShowTracesHelp.Dispose();
                //-----------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                drdShowTracesHelp.Close();
                drdShowTracesHelp.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
            }
            //-----------------------
        }
        #endregion


        #region txteFilingTANNo_TextChanged
        private void txteFilingTANNo_TextChanged(object sender, EventArgs e)
        {
            //cmbFAYear_SelectedIndexChanged(sender, e);

            IDataReader drdShoweFilingHelp = null;
            //--
            try
            {
                if (txteFilingTANNo.Text.Trim() == "")
                {
                    lsteFilingHelp.Visible = false;
                    return;
                }

                //if (blnShowHelp == false)
                //    return;
                //-----------------------
                strSQL = "SELECT TAN_AADHAAR_ID," +
                         "       TAN_NO," +
                         "       USER_PASSWORD " +
                         "FROM   MST_TAN_AADHAAR " +
                         "WHERE  TAN_NO LIKE '" + cmnService.J_ReplaceQuote(txteFilingTANNo.Text) + "%' " +
                         "ORDER BY TAN_NO, TAN_AADHAAR_ID";

                drdShoweFilingHelp = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShoweFilingHelp == null)
                {
                    lsteFilingHelp.Visible = false;
                    return;
                }
                else
                {
                    lsteFilingHelp.Items.Clear();
                    //lstUserIdHelpList.Height = 15;
                    lsteFilingHelp.Visible = true;
                    lsteFilingHelp.BringToFront();
                    while (drdShoweFilingHelp.Read())
                    {
                        lsteFilingHelp.Items.Add(new ListBoxItem(drdShoweFilingHelp["TAN_NO"].ToString().PadRight(12)
                                                                + TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdShoweFilingHelp["USER_PASSWORD"].ToString()).PadRight(10),
                                                                Convert.ToInt32(drdShoweFilingHelp["TAN_AADHAAR_ID"])));
                        //--
                        //if (lstUserIdHelpList.Height <= 300)
                        //    lstUserIdHelpList.Height = lstUserIdHelpList.Height + 19;
                    }
                    //--
                    if (lsteFilingHelp.Items.Count <= 0)
                        lsteFilingHelp.Visible = false;
                }
                //-----------------------------------------------------------
                drdShoweFilingHelp.Close();
                drdShoweFilingHelp.Dispose();
                //-----------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                drdShoweFilingHelp.Close();
                drdShoweFilingHelp.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
            }
            //-----------------------
        }
        #endregion

        #endregion

        #region User Define Functions


        #endregion

    }

}