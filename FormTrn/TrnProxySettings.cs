using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Net;
using System.Web;

using TDSMAN.Classes;

namespace TDSMAN.FormTrn
{
    public partial class TrnProxySettings : Form
    {
        #region Default Constructor
        public TrnProxySettings()
        {
            InitializeComponent();
        }
        #endregion

        #region User Defined Constructor
        //public TrnProxySettings(long BatchHeaderId)
        //{
        //    InitializeComponent();
        //    lngBatchHeaderId = BatchHeaderId;
        //}
        #endregion

        #region Private Variables Declaration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        
        string strSQL;						//For Storing the Local SQL Query
        
        string strFilePath;
        int intCaratPosition;

        string strHashValue;

        
        long lngBatchHeaderId;

        #endregion

        #region User Defined Events

        #region TrnSelectTDSFile_Load
        private void TrnSelectTDSFile_Load(object sender, EventArgs e)
        {
            IDataReader reader = null;
            try
            {
                //--
                rbnConnectAutomatically_CheckedChanged(sender, e);
                chkProxy_CheckedChanged(sender, e);
                //--
                strSQL = "SELECT PROXY_SETTINGS," +
                         "       HOST_ADDRESS," +
                         "       HOST_PORT," +
                         "       HOST_USER_NAME," +
                         "       HOST_PASSWORD " +
                         "FROM   MST_SETUP ";

                reader = dmlService.J_ExecSqlReturnReader(strSQL);

                while (reader.Read())
                {
                    if (Convert.ToString(reader["PROXY_SETTINGS"]) == "0")
                    {
                        rbnConnectAutomatically.Checked = true;
                        rbnConnectAutomatically.Select();
                        //rbnConnectProxy.Checked = true;
                        //rbnConnectProxy.Select();
                    }
                    else
                    {
                        rbnConnectProxy.Checked = true;
                        rbnConnectProxy.Select();
                        //
                        txtHost.Text = Convert.ToString(reader["HOST_ADDRESS"]);
                        txtPort.Text = Convert.ToString(reader["HOST_PORT"]);
                        //
                        if (Convert.ToString(reader["HOST_USER_NAME"]) != "")
                        {
                            grpAuthentication.Enabled = true;
                            txtUsername.Text = Convert.ToString(reader["HOST_USER_NAME"]);
                            txtPassword.Text = Convert.ToString(reader["HOST_PASSWORD"]);
                        }
                        else
                            grpAuthentication.Enabled = false;
                    }
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception err)
            {
                reader.Close();
                reader.Dispose();
                //
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateFields() == false)
                    return;

                // **************************************************************
                // *** Now Updating the MST_SETUP
                // **************************************************************

                dmlService.J_BeginTransaction();
                //
                int intConnectProxy = 0;
                //
                if (rbnConnectProxy.Checked == true)
                {
                    intConnectProxy = 1;
                    //
                    strSQL = "UPDATE MST_SETUP " +
                             "SET    PROXY_SETTINGS = " + intConnectProxy + ", " +
                             //"       HOST_ADDRESS   ='" + "http://" + cmnService.J_ReplaceQuote(txtHost.Text) + "/" + "', " +
                             "       HOST_ADDRESS   ='" + cmnService.J_ReplaceQuote(txtHost.Text) + "', " +
                             "       HOST_PORT      ='" + cmnService.J_ReplaceQuote(txtPort.Text) + "', " +
                             "       HOST_USER_NAME ='" + cmnService.J_ReplaceQuote(txtUsername.Text) + "', " +
                             "       HOST_PASSWORD  ='" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' ";// +
                            //"WHERE  BATCH_HEADER_ID = " + lngBatchHeaderId;
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                    }
                }
                else if (rbnConnectAutomatically.Checked  == true)
                {
                    //
                    strSQL = "UPDATE MST_SETUP " +
                             "SET    PROXY_SETTINGS = " + intConnectProxy + ", " +
                             "       HOST_ADDRESS   ='', " +
                             "       HOST_PORT      ='', " +
                             "       HOST_USER_NAME ='', " +
                             "       HOST_PASSWORD  ='' ";// +
                            //"WHERE  BATCH_HEADER_ID = " + lngBatchHeaderId;
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                    }
                }
                dmlService.J_Commit();
                //
                //--                        
                //if (TdsMan.GetSetup() == false)
                //    return;
                //
                this.Dispose();
                this.Close();
            }
            catch (Exception err)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(err.Message);
            }

        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
        #endregion

        #region rbnConnectAutomatically_CheckedChanged
        private void rbnConnectAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnConnectAutomatically.Checked == true)
            {
                txtHost.Text = "";
                txtPort.Text = "";
                chkProxy.Checked = false;
                txtUsername.Text = "";
                txtPassword.Text = "";
                //
                grpProxySettings.Enabled = false;
            }
            else if (rbnConnectProxy.Checked == true)
            {
                grpProxySettings.Enabled = true;
                txtHost.Select();
            }
        }
        #endregion

        #region chkProxy_CheckedChanged
        private void chkProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProxy.Checked == false)
            {
                grpAuthentication.Enabled = false;
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
            else
                grpAuthentication.Enabled = true;
        }
        #endregion

        #region btnTestConnection_Click
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                //--
                if (txtHost.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Host Address can not be Blank");
                    txtHost.Select();
                    return;
                }
                //
                if (txtPort.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Port can not be Blank");
                    txtPort.Select();
                    return;
                }
                //
                if (chkProxy.Checked == true)
                {
                    //--
                    if (txtUsername.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Username can not be Blank");
                        txtUsername.Select();
                        return;
                    }
                    //
                    if (txtPassword.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Password can not be Blank");
                        txtPassword.Select();
                        return;
                    }
                }
                //--
                WebProxy proxyObject = new WebProxy(txtHost.Text.Trim(), cmnService.J_ReturnInt32Value(txtPort.Text.Trim()));
                //
                if (chkProxy.Checked == true)
                    proxyObject.Credentials = new NetworkCredential(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                else
                    proxyObject.UseDefaultCredentials = true;
                //
                WebRequest req = WebRequest.Create("http://www.tdsman.com");
                req.Proxy = proxyObject;
                //
                //if(ConnectProxy(txtHost.Text,cmnService.J_ReturnInt32Value(txtPort.Text)) == true)
                cmnService.J_UserMessage("Connection Successful");
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage("Connection Failed !! due to : " + err.Message, MessageBoxIcon.Exclamation);
            }
        }
        #endregion



        #region ConnectProxy
        public static bool ConnectProxy(string Host, Int32 Port)
        {            
            try
            {
                WebClient wc = new WebClient();
                wc.Proxy = new WebProxy(Host, Port);
                wc.UseDefaultCredentials = true;
                wc.DownloadString("http://google.com/ncr");
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region ValidateFields
        public bool ValidateFields()
        {
            try
            {
                // **************************************************
                // **** Blank Check
                // **************************************************
                if (rbnConnectProxy.Checked == true)
                {
                    if (txtHost.Text == "")
                    {
                        cmnService.J_UserMessage("Host cannot be Blank");
                        txtHost.Select();
                        return false;
                    }
                    //
                    if (txtPort.Text == "")
                    {
                        cmnService.J_UserMessage("Port cannot be Blank");
                        txtPort.Select();
                        return false;
                    }
                    //
                    if (chkProxy.Checked == true)
                    {
                        if (txtUsername.Text == "")
                        {
                            cmnService.J_UserMessage("Username cannot be Blank");
                            txtUsername.Select();
                            return false;
                        }
                        //
                        if (txtPassword.Text == "")
                        {
                            cmnService.J_UserMessage("Password cannot be Blank");
                            txtPassword.Select();
                            return false;
                        }
                    }
                }
                return true;
            }
            catch 
            {
                cmnService.J_UserMessage("Validation Failed");
                return false;
            }
        }
        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        

        

        
        
        #endregion

    }
}