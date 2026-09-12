
#region Developer Information

/*_________________________________________________________________________________________________________

Developed By   : Ripan Paul
Module Name    : SysMenuMaintainence
Start Date     : 31/08/2010
End Date       : 
Main Table     : 
Other Tables   : 
Module Desc    : Menu Maintainence

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

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

//~~~~ User Namespaces ~~~~
using TDSMAN.Classes;

#endregion

namespace TDSMAN.FormSys
{
    public partial class SysMenuMaintainence : Form
    {

        #region System Generated Code
        public SysMenuMaintainence()
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

        #region SysMenuMaintainence_Load
        private void SysMenuMaintainence_Load(object sender, EventArgs e)
        {
            //---------------------------------------------------
            // Getting the Local Machine Name
            //---------------------------------------------------
            strLocalMachineName = Environment.MachineName;
            //---------------------------------------------------
            GC.Collect();
            //
            txtInfo.Visible = false;
        }
        #endregion

        #region BtnMenuMaintainence_Click
        private void BtnMenuMaintainence_Click(object sender, EventArgs e)
        {
            try
		    {
                //-----------------------------------------------------------
                if (cmnService.J_UserMessage("Do you want to Maintainence your Database??",
                    J_Var.J_pProjectName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    BtnCancel.Select();
                    return;
                }
                //-----------------------------------------------------------
                cmnService.J_BusyMode();
                //-----------------------------------------------------------
                dmlService.J_BeginTransaction();
                //-----------------------------------------------------------
                //-- Added on 03-09-2010 by Ripan Paul
                //-----------------------------------------------------------
                //-- Normal Menu of Masters
                //-----------------------------------------------------------
                if (InsertMenuData(100, "10", "MASTER", "00100", "mnuUser"   , "User"   , "", true) == false) return;
                if (InsertMenuData(101, "10", "MASTER", "00200", "mnuArea"   , "Area"   , "", true) == false) return;
                if (InsertMenuData(102, "10", "MASTER", "00300", "mnuOfficer", "Officer", "", true) == false) return;
                if (InsertMenuData(103, "10", "MASTER", "00400", "mnuCenter" , "Center" , "", true) == false) return;
                if (InsertMenuData(104, "10", "MASTER", "00500", "mnuGroup"  , "Group"  , "", true) == false) return;
                if (InsertMenuData(105, "10", "MASTER", "00600", "mnuMember" , "Member" , "", true) == false) return;
                //-----------------------------------------------------------
                //-- Cancellation Menu of Masters
                //-----------------------------------------------------------
                if (InsertMenuData(106, "10", "MASTER", "00700", "mnuCancellationArea"   , "Cancellation", "Area"   , true) == false) return;
                if (InsertMenuData(107, "10", "MASTER", "00800", "mnuCancellationOfficer", "Cancellation", "Officer", true) == false) return;
                if (InsertMenuData(108, "10", "MASTER", "00900", "mnuCancellationCenter" , "Cancellation", "Center" , true) == false) return;
                if (InsertMenuData(109, "10", "MASTER", "01000", "mnuCancellationGroup"  , "Cancellation", "Group"  , true) == false) return;
                if (InsertMenuData(110, "10", "MASTER", "01100", "mnuCancellationMember" , "Cancellation", "Member" , true) == false) return;
                //-----------------------------------------------------------
                
                
                //-----------------------------------------------------------
                dmlService.J_Commit();
                //-----------------------------------------------------------
                dmlService.J_ClearDatabaseLog();
                cmnService.J_NormalMode();
                //-----------------------------------------------------------
                txtInfo.Visible = true;
                txtInfo.Text = "Database Maintainence is completed.";
                //-----------------------------------------------------------
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

        #region User defined methods

        #region InsertMenuData
        private bool InsertMenuData(long   MenuId, 
                                    string GroupCode, 
                                    string MenuGroup, 
                                    string SLNo,  
                                    string MenuName, 
                                    string MenuDesc, 
                                    string MenuSubDesc_1, 
                                    bool   MenuVisibility)
        {
            if (dmlService.J_IsRecordExist("MST_MENU", "MENU_ID = " + MenuId + "") == true)
                return true;
            
            int intMenuVisibility = 0;
            if (MenuVisibility == false) intMenuVisibility = 1;

            strSQL = "INSERT INTO MST_MENU (" +
                     "            MENU_ID," +
                     "            MENU_GROUP_CODE," +
                     "            MENU_GROUP_NAME," +
                     "            MENU_SLNO," +
                     "            MENU_NAME," +
                     "            MENU_DESC," +
                     "            MENU_SUB_DESC_1," +
                     "            MENU_VISIBILITY) " +
                     "VALUES    ( " + MenuId + "," +
                     "           '" + GroupCode + "'," +
                     "           '" + MenuGroup + "'," +
                     "           '" + SLNo + "'," +
                     "           '" + MenuName + "'," +
                     "           '" + MenuDesc + "'," +
                     "           '" + MenuSubDesc_1 + "'," +
                     "            " + intMenuVisibility + ")";
            if (dmlService.J_ExecSql(strSQL) == false) return false;
            return true;
        }
        #endregion

        


        #endregion


    }
}