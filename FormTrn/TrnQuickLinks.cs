#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnQuickLinks
Version			: 1.0
Start Date		: 30-03-2011
End Date		: 
Last Updated    : 
Tables Used     : 
Module Desc		: 
________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

//~~~~ System Namespaces ~~~~
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormPar;
using TDSMAN.FormSys;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;


#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnQuickLinks : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnQuickLinks()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        ReportService rptService = new ReportService();
        //
        mdiTDSMAN mdiTDSMAN = new mdiTDSMAN();
        //-----------------------------------------------------------------------
        long lngSearchId;					//For Storing the Id
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        IDataReader drdLoadListBox = null;
        //-----------------------------------------------------------------------
        string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        //-----------------------------------------------------------------------
        
        #endregion

        #region User Defined Events

        #region TrnQuickLinks_Load

        private void TrnQuickLinks_Load(object sender, EventArgs e)
        {
            // AVAILABLE
            strSQL = "SELECT QUICK_LINKS_ID," +
                "            QUICK_LINKS_DESC " +
                "     FROM   MST_QUICK_LINKS " +
                "     WHERE  QUICK_LINKS_RANK = 0 " +
                "     ORDER BY QUICK_LINKS_ID";
            drdLoadListBox = dmlService.J_ExecSqlReturnReader(strSQL);
            if (drdLoadListBox == null)
            {
                drdLoadListBox.Close();
                drdLoadListBox.Dispose();
                return;
            }
            while (drdLoadListBox.Read())
            {
                lstAvailable.Items.Add(new ListBoxItem(Convert.ToString(drdLoadListBox["QUICK_LINKS_DESC"]), Convert.ToInt32(drdLoadListBox["QUICK_LINKS_ID"])));
            }
            drdLoadListBox.Close();
            // DISPLAY
            strSQL = "SELECT QUICK_LINKS_ID, QUICK_LINKS_DESC FROM MST_QUICK_LINKS WHERE QUICK_LINKS_RANK <> 0 " +
                "     ORDER BY QUICK_LINKS_RANK";
            drdLoadListBox = dmlService.J_ExecSqlReturnReader(strSQL);
            if (drdLoadListBox == null)
            {
                drdLoadListBox.Close();
                drdLoadListBox.Dispose();
                return;
            }
            while (drdLoadListBox.Read())
            {
                lstDisplay.Items.Add(new ListBoxItem(Convert.ToString(drdLoadListBox["QUICK_LINKS_DESC"]), Convert.ToInt32(drdLoadListBox["QUICK_LINKS_ID"])));
            }
            drdLoadListBox.Close();
            //
            lblTitle.Text = "My Shortcuts";
            //
        }
        #endregion

        #region btnAddMenu_Click
        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            if ((lstDisplay.Items.Count + 1) > 8)
            {
                cmnService.J_UserMessage("Only 8 items can be added");
                return;
            }
            lstDisplay.Items.Add(lstAvailable.Text);
            lstAvailable.Items.Remove(lstAvailable.SelectedItem);
        }
        #endregion

        #region btnRemoveMenu_Click
        private void btnRemoveMenu_Click(object sender, EventArgs e)
        {
            lstAvailable.Items.Add(lstDisplay.Text);
            lstDisplay.Items.Remove(lstDisplay.SelectedItem);
        }
        #endregion

        #region btnFirstMenu_Click
        private void btnFirstMenu_Click(object sender, EventArgs e)
        {
            int Index = lstDisplay.SelectedIndex;          //Selected Index
            object Swap = lstDisplay.SelectedItem;      //Selected Item
            //if (Index == -1)
            if (Index >= 0)
            {
                //If something is selected...
                //if ((Index + 1) == lstDisplay.Items.Count)
                //    return;
                lstDisplay.Items.RemoveAt(Index);                 //Remove it
                lstDisplay.Items.Insert(0, Swap);        //Add it back in one spot up
                lstDisplay.SelectedItem = Swap;                   //Keep this item selected
            }    
        }
        #endregion

        #region btnUpMenu_Click
        private void btnUpMenu_Click(object sender, EventArgs e)
        {
            int Index = lstDisplay.SelectedIndex;          //Selected Index
            object Swap = lstDisplay.SelectedItem;      //Selected Item
            //if (Index == -1)
            if (Index >= 0)
            {               
                //If something is selected...
                lstDisplay.Items.RemoveAt(Index);                 //Remove it
                lstDisplay.Items.Insert(Index - 1, Swap);        //Add it back in one spot up
                lstDisplay.SelectedItem = Swap;                   //Keep this item selected
            }  
        }
        #endregion

        #region btnDownMenu_Click
        private void btnDownMenu_Click(object sender, EventArgs e)
        {
            int Index = lstDisplay.SelectedIndex;          //Selected Index
            object Swap = lstDisplay.SelectedItem;      //Selected Item
            //if (Index == -1)
            if (Index >= 0)
            {               
                //If something is selected...
                if ((Index + 1) == lstDisplay.Items.Count)
                    return;
                lstDisplay.Items.RemoveAt(Index);                 //Remove it
                lstDisplay.Items.Insert(Index + 1, Swap);        //Add it back in one spot up
                lstDisplay.SelectedItem = Swap;                   //Keep this item selected
            }
        }
        #endregion

        #region btnLastMenu_Click
        private void btnLastMenu_Click(object sender, EventArgs e)
        {
            int Index = lstDisplay.SelectedIndex;          //Selected Index
            object Swap = lstDisplay.SelectedItem;      //Selected Item
            //if (Index == -1)
            if (Index >= 0)
            {
                //If something is selected...
                //if ((Index + 1) == lstDisplay.Items.Count)
                //    return;
                lstDisplay.Items.RemoveAt(Index);                 //Remove it
                lstDisplay.Items.Insert(lstDisplay.Items.Count, Swap);        //Add it back in one spot up
                lstDisplay.SelectedItem = Swap;                   //Keep this item selected
            }  
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDisplay.Items.Count < 8)
                {
                    cmnService.J_UserMessage("Eight Items need to be selected");
                    return;
                }
                //--
                dmlService.J_BeginTransaction();
                //
                strSQL = "UPDATE MST_QUICK_LINKS SET QUICK_LINKS_RANK = 0";
                dmlService.J_ExecSql(strSQL);
                //
                int items = 0;
                int i = 1;
                int intQUICK_LINKS_ID = 0;
                for (int t = 1; t < lstDisplay.Items.Count + 1; t++)
                {
                    strSQL = "UPDATE MST_QUICK_LINKS SET QUICK_LINKS_RANK = " + t + " " +
                        "     WHERE  QUICK_LINKS_DESC ='" + Convert.ToString(lstDisplay.Items[items]) + "'";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "SELECT QUICK_LINKS_ID " +
                        "     FROM   MST_QUICK_LINKS " +
                        "     WHERE  QUICK_LINKS_DESC ='" + Convert.ToString(lstDisplay.Items[items]) + "'";
                    intQUICK_LINKS_ID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ReturnId(strSQL)));
                    //
                    strSQL = "UPDATE TRN_QUICK_LINKS_VISIBLE " +
                        "     SET    QUICK_LINKS" + i + "_ID = " + intQUICK_LINKS_ID;
                    dmlService.J_ExecSql(strSQL);
                    //
                    items = items + 1;
                    i = i + 1;
                }
                //
                dmlService.J_Commit();
                //--
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
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

        #region TrnQuickLinks_Deactivate
        private void TrnQuickLinks_Deactivate(object sender, EventArgs e)
        {
            //cmnService.J_ShowChildForm(new MstQuickLinks(), mdiTDSMAN, "");
            cmnService.J_ShowChildForm(new MstQuickLinksFullScreen(), mdiTDSMAN, "");
        }
        #endregion


        #endregion
    }
}

