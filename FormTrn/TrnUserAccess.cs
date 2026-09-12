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
using System.Xml;

using System.Data;
using System.Data.SqlClient;

using System.Diagnostics;

using System.Runtime.InteropServices;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;
//~~~~
using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;
using TDSMAN.Reports.Transaction;
using CrystalDecisions.CrystalReports.Engine;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnUserAccess : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnUserAccess()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        #region Objects & Variables decleration

        TracesConnect objTracesConnect = new TracesConnect();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        ReportClass rptcls;
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;
        string strInvalidPAN = "";
        string strUnmatchedPAN = "";
        long lngBatchBasicInfoID = 0;
        //--            
        ToolTip tllTip = new ToolTip();
        ToolTip tllTipVideoDemo = new ToolTip();
        string strEmpDed = "";
        string strbtnVerification = "&Start Validation";
        string strImportErrorMessage = "Import Failed.";

        int intDefaultInvalidReturnValue = 1;
        //string strLogOff = "Log off";
        //string strLogOn = "Log on";
        string strFVUPath = "";
        string strCheckCompatibilityMessage = "";
        long lngBatchID = 0;
        //
        bool blnStatus = false;
        bool blnVerificationComplete = false;
        long lngDeducteeID = 0;
        bool blRegular = true;
        bool blnSelectComboExit = false;
        bool blnLoadGridExit = false;
        //
        int intPANId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0;
        int intVerifyId = 0;

        bool blnNullSectionFound = false;
        //
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNo = "";
        string strQtr = "";
        string strFormNoBkmark = "";
        string strFAYear = "";
        string OutputFilePath;

        //----------------------------------------------
        string SourceFilePath;
        string strFileName;
        string strFolderPath;
        string strImporttableName = "";
        string strLogFileCreationDate = "";
        string strLogTanNo = "";

        string strFileCreationDate = "";
        string strFileCreationDateFVU5_2 = "";
        string strLogForm = "";
        string strLogFaYear = "";
        string strLogFaYearId = "";
        string strLogQuarter = "";
        //-- 23/02/2018 --
        string strExcelFilePath = string.Empty;
        string strExcelFileName = string.Empty;
        //
        int j = 0;
        //
        //-- Added By Abhishek Dey On 30/11/2018 --
        string strDeSelectedMenu = string.Empty;
        string strSelectedMenu = string.Empty;
        //-- Added By Abhishek Dey On 04/12/2018 --
        ArrayList arrSelectedMenus = new ArrayList();
        //-----------------------------------------
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

        #region User Defined Events

        #region TrnUserAccess_Load
        private void TrnUserAccess_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //--
            lblTitle.Text = "User Access";
            ClearControls();
            //tbcData.Enabled = false;
            BtnRefresh.Enabled = false;
            BtnRefresh.BackColor = Color.LightGray;
            BtnSave.Visible = false; //-- Added By Abhishek Dey On 30/11/2018 --
        }
        #endregion

        #region cmbSelectUser_SelectedIndexChanged
        private void cmbSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtChildNode = new DataTable();

//            strSQL = @" SELECT DISTINCT CHILD_MENU_DESC  
//                        FROM   MST_CHILD_MENU 
//                        WHERE  PARENT_MENU_ID =" + parentId + "";
            strSQL = "SELECT PARENT_MENU_ID, PARENT_MENU_DESC FROM MST_MENU_PARENT WHERE INACTIVE_FLAG = 0";            

            dtChildNode = dmlService.J_ExecSqlReturnDataTable(strSQL);

            //-- Added By Abhishek Dey On 29/11/2018 --
            if (dtChildNode.Rows.Count > 0)
            {
                chkSelectDeselect.Visible = true;
                BtnSave.Visible = true;
            }
            //-----------------------------------------


            //TreeNode childNode;
            TreeViewMenus.Nodes.Clear();
            foreach (DataRow dr in dtChildNode.Rows)
            {
                //if (ParentNode == null)
                    //childNode = TreeView.  .Nodes.Add(dr["PARENT_MENU_DESC"].ToString());
                TreeViewMenus.Nodes.Add(dr["PARENT_MENU_DESC"].ToString());
                //else
                //    childNode = ParentNode.Nodes.Add(dr["CHILD_MENU_DESC"].ToString());
                //PopulateTreeView(Convert.ToInt32(dr["MNUSUBMENU"].ToString()), childNode);
            }
            PopulateChildMenuGrid("");
           // foreach(DataRow dr in ds.Tables["Categories"].Rows)
           // {
           //TreeNode tn = new TreeNode(dr["CategoryName"].ToString());
           //foreach (DataRow drChild in dr.GetChildRows("Cat_Product"))
           //{
           //       tn.Nodes.Add(drChild["ProductName"].ToString());
           //}
           //treeView1.Nodes.Add(tn);
           // }

            
            //-- Added By Abhishek Dey On 04/12/2018 --
            arrSelectedMenus.Clear();
            DataSet dsNew = new DataSet();
            strSQL = "SELECT CHILD_MENU_ID FROM TRN_USER_MENU_ACCESS WHERE SETUP_ID = " + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex));
            dsNew = dmlService.J_ExecSqlReturnDataSet(strSQL);

            if (dsNew.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNew.Tables[0].Rows)
                {
                    arrSelectedMenus.Add(dr["CHILD_MENU_ID"].ToString());
                }
            }
            //-----------------------------------------
        }
        #endregion

        #region TreeViewMenus_AfterSelect
        private void TreeViewMenus_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string strParentMenu = "";
            if (TreeViewMenus.SelectedNode.Text != null)
            {
                //strSelectedNode = e.Node.Text;
                //strSelectedNode = "'" + TreeView.SelectedNode.FullPath.ToString().Replace("\\", "','") + "'";
                strParentMenu = e.Node.Text;
            //    if (TreeView.SelectedNode.FullPath.ToString().Contains("\\") == true)
            //        blnParentNode = false;
            //    else
            //        blnParentNode = true;
            }
            //--
            PopulateChildMenuGrid(strParentMenu);
            PopulateMenuRightsGrid(strParentMenu, Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)));
            ////-------------------------------------------
            ////string selectedNodeText = e.Node.Text;
            ////-------------------------------------------
            //if (PopulateMenuDetails() == false) return;

            //-- Added By Abhishek Dey On 29/11/2018 --
            blnStatus = false;
            if (chkSelectDeselect.Checked == true)
                chkSelectDeselect.Checked = false;
            //-----------------------------------------
            blnStatus = true;
        }
        #endregion

        #region chkSelectDeselect_CheckedChanged
        private void chkSelectDeselect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (blnStatus == false) return;  //-- Added By Abhishek Dey On 10/12/2018 --
                //
                this.Cursor = Cursors.WaitCursor;
                foreach (DataGridViewRow row in grdvChildMenus.Rows)
                {
                    if (chkSelectDeselect.Checked == true)//checked all checkbox
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                        {
                            row.Cells[0].Value = true;
                        }
                    }
                    //Unchecked all checkbox
                    else
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                        {
                            row.Cells[0].Value = false;
                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region grdvChildMenus_CurrentCellDirtyStateChanged
        private void grdvChildMenus_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grdvChildMenus.IsCurrentCellDirty)
            {
                grdvChildMenus.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        #endregion

        #region grdvChildMenus_CellValueChanged
        private void grdvChildMenus_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //
            bool blnTest = blnStatus;   //-- 04/12/2018 --
            if (blnStatus == false) return;
            ////
            //foreach (DataGridViewRow row in grdvChildMenus.Rows)
            //{
            //    if (row.Cells[0].Value != null)
            //    {
            //        if ((bool)row.Cells[0].Value == true)
            //        {
            //            strSQL = "INSERT INTO TRN_USER_MENU_ACCESS (SETUP_ID, CHILD_MENU_ID) VALUES (" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + "," + Convert.ToInt32(row.Cells[1].Value.ToString()) + ")";
            //            dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            //        }
            //        else if ((bool)row.Cells[0].Value == false)
            //        {
            //            strSQL = "DELETE FROM TRN_USER_MENU_ACCESS WHERE SETUP_ID =" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + " AND CHILD_MENU_ID = " + Convert.ToInt32(row.Cells[1].Value.ToString());
            //            dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            //        }
            //    }
            //    //else if ((bool)row.Cells[0].Selected == false)
            //    //{
            //    //}
            //}
            //
            //--
            if (e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grdvChildMenus.Rows[e.RowIndex].Cells[0];
                DataGridViewCell cellChildMenuID = (DataGridViewCell)grdvChildMenus.Rows[e.RowIndex].Cells[1];
                //if (cell.Value == null || (bool)cell.Value == false)
                if ((bool)cell.Value == false)
                {
                    //grdvNewDescription.MultiSelect = false;
                    //grdvNewDescription.Rows[e.RowIndex].Selected = true;
                    //cell.Value = true;
                    //cmnService.J_UserMessage("UNCHECK"); 
                    //
                    //-- Commented By Abhishek Dey On 30/11/2018 --
                    //strSQL = "DELETE FROM TRN_USER_MENU_ACCESS WHERE SETUP_ID =" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + " AND CHILD_MENU_ID = " + Convert.ToInt32(cellChildMenuID.Value.ToString());
                    //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                    //---------------------------------------------
                    //
                    //-- Added By Abhishek Dey On 30/11/2018 --
                    //if (string.IsNullOrEmpty(strDeSelectedMenu.Trim()))
                    //    strDeSelectedMenu = cellChildMenuID.Value.ToString();
                    //else
                    //    strDeSelectedMenu = strDeSelectedMenu + "," + cellChildMenuID.Value.ToString();
                    //
                    //-- Added By Abhishek Dey On 04/12/2018 --
                    arrSelectedMenus.Remove(cellChildMenuID.Value.ToString());
                    //-----------------------------------------
                }
                else if ((bool)cell.Value == true)
                {
                    //grdvNewDescription.Rows[e.RowIndex].Selected = false;
                    //cell.Value = false;
                    //cmnService.J_UserMessage("CHECK");
                    //
                    //-- Commented By Abhishek Dey On 30/11/2018 --
                    //strSQL = "INSERT INTO TRN_USER_MENU_ACCESS (SETUP_ID, CHILD_MENU_ID) VALUES (" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + "," + Convert.ToInt32(cellChildMenuID.Value.ToString()) + ")";
                    //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                    //---------------------------------------------
                    //
                    //-- Added By Abhishek Dey On 30/11/2018 --
                    //if (string.IsNullOrEmpty(strSelectedMenu.Trim()))
                    //    strSelectedMenu = cellChildMenuID.Value.ToString();
                    //else
                    //    strSelectedMenu = strSelectedMenu + "," + cellChildMenuID.Value.ToString();
                    //
                    //-- Added By Abhishek Dey On 04/12/2018 --
                    arrSelectedMenus.Add(cellChildMenuID.Value.ToString());                    
                    //-----------------------------------------
                    //-----------------------------------------
                }
            }
            //cmnService.J_UserMessage(cellChildMenuID.Value.ToString());
            //--
        }
        #endregion

        #region grdvChildMenus_CellClick
        private void grdvChildMenus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (blnStatus == false) return;
            ////
            //if (e.RowIndex != -1)
            //{
            //    foreach (DataGridViewRow row in grdvChildMenus.Rows)
            //    {
            //        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
            //            row.Cells[0].Value = false;
            //    }
            //    //--
            //    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grdvChildMenus.Rows[e.RowIndex].Cells[0];
            //    DataGridViewCell cellYr = (DataGridViewCell)grdvChildMenus.Rows[e.RowIndex].Cells[1];
            //    //if (cell.Value == null || (bool)cell.Value == false)
            //    if ((bool)cell.Value == false)
            //    {
            //        //grdvNewDescription.MultiSelect = false;
            //        //grdvNewDescription.Rows[e.RowIndex].Selected = true;
            //        //cell.Value = true;
            //        cmnService.J_UserMessage("UNCHECK");
            //    }
            //    else if ((bool)cell.Value == true)
            //    {
            //        //grdvNewDescription.Rows[e.RowIndex].Selected = false;
            //        //cell.Value = false;
            //        cmnService.J_UserMessage("CHECK");
            //    }
            //    //cmnService.J_UserMessage(cellYr.Value.ToString());
            //    //--
            //}
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
            //
        }
        #endregion


        //-- Added By Abhishek Dey On 29/11/2018 --
        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //--
            this.Cursor = Cursors.WaitCursor;
            //--
            Insert_Update_Delete_Data();
            //--
            this.Cursor = Cursors.Default;
            //--
        }
        #endregion
        //-----------------------------------------

        #endregion

        #region User Defined Functions
        
        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {
                //--------------------------------------------
                this.Cursor = Cursors.WaitCursor;
                //--------------------------------------------                
                //-- Commented On 06/12/2018 --
                //if (arrSelectedMenus.Count == 0)  //-- 04/12/2018 --
                //{
                //    cmnService.J_UserMessage("Please select at least one record.");
                //}
                //-----------------------------
                //
                if (cmnService.J_UserMessage("Do you want to Save data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
                //-----------------------------------------------------------
                // SET THE TRANSACTION AS BEGIN
                //-----------------------------------------------------------
                dmlService.J_BeginTransaction();
                //if(!string.IsNullOrEmpty(strDeSelectedMenu))
                //if (arrSelectedMenus.Count > 0)  //-- 06/12/2018 --
                //{
                    //strSelectedMenu = string.Empty;
                    //foreach (string menuItems in arrSelectedMenus)
                    //{
                    //    if (!string.IsNullOrEmpty(strSelectedMenu))
                    //        strSelectedMenu = strSelectedMenu + ',' + menuItems;
                    //    else
                    //        strSelectedMenu = menuItems;
                    //}
                strSQL = @"DELETE FROM TRN_USER_MENU_ACCESS WHERE SETUP_ID =" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex));
                //}
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    this.Cursor = Cursors.Default;
                    return;
                }

                //if (!string.IsNullOrEmpty(strSelectedMenu))
                if (arrSelectedMenus.Count > 0)   //-- Added By Abhishek Dey On 04/12/2018 --
                {
                    //string[] ArrSelectedMenu = strSelectedMenu.Split(',');
                    //
                    //-----------------------------------------------------------
                    // INSERT QUERY & EXECUTION
                    //-----------------------------------------------------------
                    // LOOP OVER STRING ARRAY
                    foreach (string MenuId in arrSelectedMenus)
                    {
                        strSQL = "INSERT INTO TRN_USER_MENU_ACCESS (SETUP_ID, CHILD_MENU_ID) VALUES (" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + "," + MenuId + ")";

                        //------------------------------------------------------------
                        // INSERT THE USER WISE MENU 
                        //------------------------------------------------------------
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }

                }
               
                //------------------------------------------------------------------
                dmlService.J_Commit();
                //-------------------------------------------------------
                cmnService.J_UserMessage("Data Successfully Saved.");
                //-------------------------------------------------------
                //ClearControls();
                //-- Added By Abhishek Dey On 30/11/2018 --
                strDeSelectedMenu = string.Empty;
                strSelectedMenu = string.Empty;
                arrSelectedMenus.Clear();
                //
                //-- Added By Abhishek Dey On 04/12/2018 --
                //-- Commented By Abhishek Dey On 07/12/2018 --
                //arrSelectedMenus.Clear();
                //DataSet dsNew = new DataSet();
                //strSQL = "SELECT CHILD_MENU_ID FROM TRN_USER_MENU_ACCESS WHERE SETUP_ID = " + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex));
                //dsNew = dmlService.J_ExecSqlReturnDataSet(strSQL);

                //if (dsNew.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsNew.Tables[0].Rows)
                //    {
                //        arrSelectedMenus.Add(dr["CHILD_MENU_ID"].ToString());
                //    }
                //}
                //-- Added By Abhishek Dey On 07/12/2018 --
                ClearControls();
                TreeViewMenus.Nodes.Clear();
                grdvChildMenus.DataSource = null;
                grdvChildMenus.Rows.Clear();
                grdvChildMenus.Columns.Clear();
                grdvChildMenus.Refresh();
                chkSelectDeselect.Checked = false;
                chkSelectDeselect.Visible = false;
                //-----------------------------------------
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            //
            blnSelectComboExit = true;
            //-----------
            //-- FINANCIAL YEAR
            //-----------
            //strSQL = " SELECT ASST_ID," +
            //    "             FA_YEAR " +
            //    "      FROM   MST_SETUP " +
            //    "      WHERE  VISIBILITY_FLAG = 0 " +
            //    "      AND    VISIBILITY_AADHAAR_RETURN_FLAG = 1 " +
            //    "      ORDER BY ASST_ID DESC";
            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbSelectUser, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer));
            //
            XmlNodeList xnList = XMLDoc.GetElementsByTagName(T_XML.NODE);
            //
            cmbSelectUser.Items.Clear();
            cmbSelectUser.Enabled = true;
            foreach (XmlNode xn in xnList)
            {
                //dsClientsView.Rows.Add();
                ////
                //dsClientsView.Rows[dsClientsView.Rows.Count - 1].Cells[0].Value = cmnService.J_Decode(xn[T_XML.MACHINENO].InnerText);
                //dsClientsView.Rows[dsClientsView.Rows.Count - 1].Cells[1].Value = cmnService.J_Decode(xn[T_XML.MACHINENAME].InnerText);
                cmbSelectUser.Items.Add(new ListBoxItem(cmnService.J_Decode(xn[T_XML.MACHINENAME].InnerText), cmnService.J_ReturnInt32Value(cmnService.J_Decode(xn[T_XML.MACHINENO].InnerText))));
            }
            //--
            if (cmbSelectUser.Items.Count == 0)
            {
                cmbSelectUser.Text = "No Client linked!!";
                cmbSelectUser.Enabled = false;
        
            }
            //-----------
            blnSelectComboExit = false;
            //-----------

            //-- Added By Abhishek Dey On 30/11/2018 --
            strDeSelectedMenu = string.Empty;
            strSelectedMenu = string.Empty;
        }
        #endregion

        #region PopulateChildMenuGrid
        private void PopulateChildMenuGrid(string ParentMenu)
        {
            string[,] strLoadChildMenusGrid = {{"CHILD_MENU_ID", "0", "", "R", "", "F", ""},
                                            {"PARENT_MENU_ID", "0", "", "R", "", "F", ""},
                                            {"Child Menu", "100", "S", "", "", "T", "fill"},
                                            {"Child Sub Menu", "100", "S", "", "", "T", "fill"},
                                            {"Child Inner Sub Menu", "100", "S", "", "", "T", "fill"}};
            //
            strSQL = @"SELECT  MST_MENU_CHILD.CHILD_MENU_ID,
	                           MST_MENU_CHILD.PARENT_MENU_ID,
                               MST_MENU_CHILD.CHILD_MENU_DESC,
                               MST_MENU_CHILD.CHILD_SUB_MENU_DESC,
                               MST_MENU_CHILD.CHILD_INNER_SUB_MENU_DESC       
                       FROM    MST_MENU_CHILD,
                               MST_MENU_PARENT
                       WHERE   MST_MENU_CHILD.PARENT_MENU_ID = MST_MENU_PARENT.PARENT_MENU_ID
                       AND     MST_MENU_PARENT.PARENT_MENU_DESC ='" + ParentMenu + @"'
                       AND     MST_MENU_CHILD.INACTIVE_FLAG = 0
                       ORDER BY SORT_ORDER";

            blnStatus = false;
            TdsMan.PopulateGridView(grdvChildMenus, dmlService.J_pCommand, strSQL, strLoadChildMenusGrid);
            blnStatus = true;
        }
        #endregion

        #region PopulateMenuRightsGrid
        private void PopulateMenuRightsGrid(string ParentMenu, long UserID)
        {
            #region Commented By Abhishek Dey On 04/12/2018 --
//            strSQL = @"SELECT TRN_USER_MENU_ACCESS.USER_MENU_ACCESS_ID,
//                              TRN_USER_MENU_ACCESS.SETUP_ID,
//                              TRN_USER_MENU_ACCESS.CHILD_MENU_ID
//                       FROM   TRN_USER_MENU_ACCESS,
//                              MST_MENU_CHILD,
//                              MST_MENU_PARENT
//                       WHERE  TRN_USER_MENU_ACCESS.CHILD_MENU_ID = MST_MENU_CHILD.CHILD_MENU_ID
//                       AND    MST_MENU_CHILD.PARENT_MENU_ID      = MST_MENU_PARENT.PARENT_MENU_ID
//                       AND    MST_MENU_PARENT.PARENT_MENU_DESC   ='" + ParentMenu + @"'
//                       AND    TRN_USER_MENU_ACCESS.SETUP_ID      =" + UserID + @"
//                       ORDER BY SORT_ORDER";
//            //--
//            IDataReader drdGetRecord = null;
//            drdGetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
//            if (drdGetRecord == null)
//            {
//                return;
//            }
//            while (drdGetRecord.Read())
//            {
//                foreach (DataGridViewRow row in grdvChildMenus.Rows)
//                {
//                    //if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
//                    //{
//                    //}
//                    //
//                    blnStatus = false;
//                    //
                    
//                        if (row.Cells[1].Value.ToString() == Convert.ToString(drdGetRecord["CHILD_MENU_ID"]))
//                        {
//                            row.Cells[0].Value = true;
//                            //-- Added By Abhishek Dey On 04/12/2018 --
//                            //arrSelectedMenus.Add(Convert.ToString(drdGetRecord["CHILD_MENU_ID"]));
//                            //-----------------------------------------
//                        }
//                        else
//                            row.Cells[0].Value = false;
//                        //blnStatus = true; 

//                        #region COMMENTED On -- 04/12/2018 --
//                        //-- Added By Abhishek Dey On 30/11/2018 --
//                        //if (!string.IsNullOrEmpty(strSelectedMenu.Trim()))
//                        //{

//                        //    if (strSelectedMenu.Trim().Contains(","))
//                        //    {
//                        //        string[] ArrSelectedMenu = strSelectedMenu.Split(',');
//                        //        //
//                        //        foreach (string MenuId in ArrSelectedMenu)
//                        //        {
//                        //            if (row.Cells[1].Value.ToString() == MenuId)
//                        //            {
//                        //                row.Cells[0].Value = true;
//                        //            }
//                        //        }
//                        //    }
//                        //    else
//                        //    {
//                        //        if (row.Cells[1].Value.ToString() == strSelectedMenu.Trim())
//                        //        {
//                        //            row.Cells[0].Value = true;
//                        //        }
//                        //    }

//                        //}

//                        //if (!string.IsNullOrEmpty(strDeSelectedMenu.Trim()))
//                        //{
//                        //    if (strDeSelectedMenu.Contains(","))
//                        //    {
//                        //        string[] ArrDeSelectedMenu = strDeSelectedMenu.Split(',');
//                        //        //
//                        //        foreach (string MenuId in ArrDeSelectedMenu)
//                        //        {
//                        //            if (row.Cells[1].Value.ToString() == MenuId)
//                        //            {
//                        //                row.Cells[0].Value = false;
//                        //            }
//                        //        }
//                        //    }
//                        //    else
//                        //    {
//                        //        if (row.Cells[1].Value.ToString() == strDeSelectedMenu.Trim())
//                        //        {
//                        //            row.Cells[0].Value = false;
//                        //        }
//                        //    }

//                        //}
//                        //-----------------------------------------
//                        #endregion

                        

//                    ////-- Added By Abhishek Dey On 04/12/2018 --
//                    //    if (arrSelectedMenus.Count > 0)
//                    //    {
//                    //        foreach (string item in arrSelectedMenus)
//                    //        {
//                    //            if (row.Cells[1].Value.ToString() == item)
//                    //            {
//                    //                row.Cells[0].Value = true;
//                    //            }
//                    //            else
//                    //            {
//                    //                row.Cells[0].Value = false;
//                    //            }
//                    //        }
//                    //    }
//                    ////-----------------------------------------

//                        blnStatus = true; 
//                }
//            }
//            //
//            drdGetRecord.Close();
//            drdGetRecord.Dispose();
            //--
            #endregion

            //-- Added By Abhishek Dey On 04/12/2018 --
            //-- Added By Abhishek Dey On 04/12/2018 --
            //if (arrSelectedMenus.Count > 0)
            //{
            try
            {
                blnStatus = false;

                foreach (DataGridViewRow row in grdvChildMenus.Rows)
                {
                    if (arrSelectedMenus.Count > 0)
                    {
                        foreach (string item in arrSelectedMenus)
                        {
                            if (row.Cells[1].Value.ToString() == item)
                            {
                                row.Cells[0].Value = true;
                                break;
                            }
                            //else
                            //{
                            //    row.Cells[0].Value = false;
                            //}
                        }
                    }
                    else
                    {
                        row.Cells[0].Value = false;
                    }
                }
                blnStatus = true;
                //}
                //-----------------------------------------
                //------------------------------------------
            }
            catch (Exception err)
            { }

        }




        #endregion

        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0116", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}
