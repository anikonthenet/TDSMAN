
#region Refered Namespaces & Classes

//~~~~ System Namespaces ~~~~
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//~~~~ User Namespaces ~~~~
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;

#endregion

namespace TDSMAN.FormCmn
{
    public partial class CmnLogin : Form
    {

        #region Object Decleration Section

        private DMLService dmlService;
        private CommonService cmnService;

        private IDataReader reader;

        TDSMAN.Classes.TDSMAN Tdsman = new TDSMAN.Classes.TDSMAN();

        #endregion

        #region Variable Decleration Section

        string strSQL;

        #endregion

        #region CONSTRUCTOR
        public CmnLogin()
        {
            InitializeComponent();

            //-- Object Initialization
            dmlService = new DMLService();
            cmnService = new CommonService();

            reader = null;

            //-- Variable Initialization
            strSQL = "";

        }
        #endregion

        #region DESTRUCTOR
        ~CmnLogin()
        {
            dmlService.Dispose();
            this.Dispose(true);
        }
        #endregion

        #region User Define Methods

        #region void ClearControls
        private void ClearControls()
        {
            //txtPassword.Text = "";
        }
        #endregion

        #region bool ValidateFields
        private bool ValidateFields()
        {

            if (txtPassword.Text == "")
            {
                cmnService.J_UserMessage("Please enter the password");
                txtPassword.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #endregion

        #region System Events

        #region CmnLogin_Load
        private void CmnLogin_Load(object sender, EventArgs e)
        {
            ClearControls();
            this.Text = ":: " + J_Var.J_pProjectName + " - Login ::";
            txtPassword.Select();
            //lblHelpMessage.Text = "If you have forgotten / lost your password, please contact the helpdesk at \nEmail : info@tdsman.com \nPhone : +91-33-22623535, 64596006";
            lblHelpMessage.Text = "If you have forgotten / lost your password, please contact the helpdesk at \nEmail : info@tdsman.com \nPhone : +91-33-22875500, +91-33-40845500, 9836490007";
            //
            
        }
        #endregion

        #region BtnOK_Click
        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //~~~~ Check the Validation
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                if (ValidateFields() == false)
                    return;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //~~ MST_USER Table
                //~~ Make the query string
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                string strDATE = System.DateTime.Now.ToString("yyyyMMdd");
                //if (txtPassword.Text == ChequePrinting.T_GeneratePassword(Convert.ToString(dtService.J_ConvertToIntYYYYMMDD(System.DateTime.Now.ToString()))))
                if (txtPassword.Text == Tdsman.T_GeneratePassword(strDATE))
                {
                    this.Close();
                    this.Dispose();
                    //
                    J_Var.frmMain = new mdiTDSMAN();
                    J_Var.frmMain.ShowDialog();
                    //--
                    return;
                }
                strSQL = "SELECT PASSWD " +
                         "FROM   MST_SETUP " +
                         "WHERE  PASSWD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' ";
                //-- 2014/11/14
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                {
                    if (TDSMAN.Classes.TDSMAN.T_MACHINE_ID > 0)
                        strSQL = strSQL + " AND SETUPID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " ";
                }
                //--

                DataSet ds = dmlService.J_ExecSqlReturnDataSet(strSQL);
                if (ds == null)
                {
                    txtPassword.Select();
                    return;
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //=================================================================
                    this.Close();
                    this.Dispose();
                    //=================================================================
                    J_Var.frmMain = new mdiTDSMAN();
                    J_Var.frmMain.ShowDialog();
                    //=================================================================
                }
                else
                {
                    cmnService.J_UserMessage("Incorrect Credentials !!", MessageBoxIcon.Error);
                    txtPassword.Select(0, txtPassword.Text.Length);
                }
            }
            catch (Exception err_handler)
            { 
                cmnService.J_UserMessage(err_handler.Message);
                txtPassword.Select();
            }
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion
        

        #region txtPassword_KeyPress
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnCancel_Click(sender, e);
        }
        #endregion



        #region lblHidePassword_Click
        private void lblHidePassword_Click(object sender, EventArgs e)
        {
            lblHidePassword.Visible = false;
            lblShowPassword.Visible = true;
            txtPassword.PasswordChar = '\0';
        }
        #endregion

        #region lblShowPassword_Click
        private void lblShowPassword_Click(object sender, EventArgs e)
        {
            lblShowPassword.Visible = false;
            lblHidePassword.Visible = true;
            txtPassword.PasswordChar = '*';
        }
        #endregion




        #endregion


    }
}