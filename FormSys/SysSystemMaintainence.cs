
#region Developer Information

/*_________________________________________________________________________________________________________

Developed By   : Ripan Paul
Module Name    : SysSystemMaintainence
Start Date     : 31/08/2010
End Date       : 
Main Table     : 
Other Tables   : 
Module Desc    : System Maintainence

//_________________________________________________________________________________________________________*/

#endregion

#region Refered Namespaces & Classes

//~~~~ System Namespaces ~~~~
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//---------------------------
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

//~~~~ User Namespaces ~~~~
using TDSMAN.Classes;

#endregion

namespace TDSMAN.FormSys
{
    public partial class SysSystemMaintainence : Form
    {
        #region System Generated Code
        public SysSystemMaintainence()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        
        string strPath;
        string strSQL;
        string strLocalMachineName;
        
        #endregion

        #region User defined events

        #region SysSystemMaintainence_Load
        private void SysSystemMaintainence_Load(object sender, EventArgs e)
        {
            //---------------------------------------------------
            // Getting the Local Machine Name
            //---------------------------------------------------
            strLocalMachineName = Environment.MachineName;
            //---------------------------------------------------
            txtInfo.Visible = false;
        }
        #endregion

        #region BtnSystemMaintainence_Click
        private void BtnSystemMaintainence_Click(object sender, EventArgs e)
        {
            try
		    {
                if(cmnService.J_UserMessage("Do you want to Maintainence your Database??",
                    J_Var.J_pProjectName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
                cmnService.J_BusyMode();
                dmlService.J_ClearDatabaseLog();


                if (dmlService.J_IsDatabaseObjectExist("MST_STATE", "STATE_CODE", 150) == true)
                {
                    strSQL = "ALTER TABLE MST_STATE ALTER COLUMN STATE_CODE TEXT(50)";
                    dmlService.J_ExecSql(strSQL);
                }
                
                
                
                
                
                
                cmnService.J_NormalMode();
                
                txtInfo.Visible = true;
                txtInfo.Text = "Database Maintainence is completed.";
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
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

        #endregion

    }
}