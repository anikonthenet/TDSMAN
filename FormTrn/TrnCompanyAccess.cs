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
    public partial class TrnCompanyAccess : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnCompanyAccess()
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
        string strQuery = string.Empty;
        string strOrderBy = string.Empty;
        string strSearch = string.Empty;
        string[,] strMatrix = null;
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
        DataSet dsetGridClone;

        DataTable dtCompany = new DataTable();
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

        #region TrnCompanyAccess_Load
        private void TrnCompanyAccess_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //--
            lblTitle.Text = "Company Access Management";
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
            //DataTable dtCompany = new DataTable();
            dtCompany.Clear();
            strSQL = "SELECT COMPANY_ID,COMPANY_NAME,TAN_NO FROM MST_COMPANY WHERE INACTIVE_FLAG = 0 ORDER BY COMPANY_NAME";

            dtCompany = dmlService.J_ExecSqlReturnDataTable(strSQL);

            //-- Added By Abhishek Dey On 29/11/2018 --
            if (dtCompany.Rows.Count > 0)
            {
                chkSelectDeselect.Visible = true;
                pnlSearch.Visible = true;
                BtnSave.Visible = true;
            }
            //-----------------------------------------            
            PopulateCompanyGrid();
            //-- Added By Abhishek Dey On 04/12/2018 --
            arrSelectedMenus.Clear();
            DataSet dsNew = new DataSet();
            strSQL = "SELECT COMPANY_ID FROM TRN_TAN_USER WHERE SETUP_ID = " + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex));
            dsNew = dmlService.J_ExecSqlReturnDataSet(strSQL);

            if (dsNew.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNew.Tables[0].Rows)
                {
                    arrSelectedMenus.Add(dr["COMPANY_ID"].ToString());
                }
            }
            //-----------------------------------------
            PopulateCompanyRightsGrid(Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)));
            //
            blnStatus = false;
            if (chkSelectDeselect.Checked == true)
                chkSelectDeselect.Checked = false;
            
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
           
            if (e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grdvChildMenus.Rows[e.RowIndex].Cells[0];
                DataGridViewCell cellChildMenuID = (DataGridViewCell)grdvChildMenus.Rows[e.RowIndex].Cells[1];
                //if (cell.Value == null || (bool)cell.Value == false)
                if ((bool)cell.Value == false)
                {
                    
                    //-- Added By Abhishek Dey On 04/12/2018 --
                    arrSelectedMenus.Remove(cellChildMenuID.Value.ToString());
                    //-----------------------------------------
                }
                else if ((bool)cell.Value == true)
                {                    
                    arrSelectedMenus.Add(cellChildMenuID.Value.ToString());                    
                   
                }
            }           
            //--
        }
        #endregion

        #region grdvChildMenus_ColumnHeaderMouseClick
        private void grdvChildMenus_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            chkSelectDeselect.Checked = false;
        }
        #endregion

        #region grdvChildMenus_CellContentClick
        private void grdvChildMenus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grdvChildMenus.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        #endregion


        #region grdvChildMenus_CellClick
        //private void grdvChildMenus_CellClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}
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
            if (cmbSelectUser.SelectedIndex < 0)
                return;
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
                
                strSQL = @"DELETE FROM TRN_TAN_USER WHERE SETUP_ID =" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex));                
                //}
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    this.Cursor = Cursors.Default;
                    return;
                }
                //------------------------------------------------------------------
                dmlService.J_Commit();
                //-------------------------------------------------------

                //-----------------------------------------------------------
                // SET THE TRANSACTION AS BEGIN
                //-----------------------------------------------------------
                dmlService.J_BeginTransaction();

                if (arrSelectedMenus.Count > 0)   //-- Added By Abhishek Dey On 04/12/2018 --
                {
                    //-----------------------------------------------------------
                    // INSERT QUERY & EXECUTION
                    //-----------------------------------------------------------
                    // LOOP OVER STRING ARRAY
                    //-----------------------------------------------------------
                    //foreach (string CompanyId in arrSelectedMenus)
                    //{

                    foreach (DataGridViewRow row in grdvChildMenus.Rows)
                    {
                        if (row.Cells[0].Value == null)
                        { }
                        else if((bool)row.Cells[0].Value == true)
                        {


                            //if (CompanyId == "163") { }
                            strSQL = "INSERT INTO TRN_TAN_USER (SETUP_ID, COMPANY_ID) VALUES (" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + "," + row.Cells[1].Value.ToString() + ")";

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
                    //}



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
                ClearControls();
                //TreeViewMenus.Nodes.Clear();
                grdvChildMenus.DataSource = null;
                grdvChildMenus.Rows.Clear();
                grdvChildMenus.Columns.Clear();
                grdvChildMenus.Refresh();
                chkSelectDeselect.Checked = false;
                chkSelectDeselect.Visible = false;
                pnlSearch.Visible = false;
                txtNewSearch.Text = "";
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
           
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer));
            //
            XmlNodeList xnList = XMLDoc.GetElementsByTagName(T_XML.NODE);
            //
            cmbSelectUser.Items.Clear();
            cmbSelectUser.Enabled = true;
            foreach (XmlNode xn in xnList)
            {
                cmbSelectUser.Items.Add(new ListBoxItem(cmnService.J_Decode(xn[T_XML.MACHINENAME].InnerText), cmnService.J_ReturnInt32Value(cmnService.J_Decode(xn[T_XML.MACHINENO].InnerText))));
                //cmbSelectUser.Items.Add(new ListBoxItem("KK", 69030202));    //-- 04/11/2019 --
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
            BtnSave.Visible = false;
        }
        #endregion
        


        //-- Added By Abhishek Dey On 29/10/2019 --
        #region PopulateCompanyGrid
        private void PopulateCompanyGrid()
        {
            string[,] strLoadCompanyGrid = {{"COMPANY_ID", "0", "", "R", "", "F", ""},
                                            {"COMPANY NAME", "700", "S", "", "", "T", ""},
                                            {"TAN NO", "80", "S", "", "", "T", "fill"},
                                            {"SORT_ORDER", "0", "", "R", "", "F", ""}};
           // { "NAME_TAN", "0", "", "R", "", "F", ""}};
            //-----------------------------------------------------------------------------
            strMatrix = strLoadCompanyGrid;
            //-----------------------------------------------------------------------------

            //-- 11/11/2019 --
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                strOrderBy = @" CASE WHEN ISNULL(CLIENT.COMPANY_ID,0) = 0 THEN 1 ELSE 0 END,
                                    MST_COMPANY.COMPANY_NAME";
                //
                strQuery = @"SELECT MST_COMPANY.COMPANY_ID                                            AS COMPANY_ID,
                                    MST_COMPANY.COMPANY_NAME                                          AS COMPANY_NAME,
                                    MST_COMPANY.TAN_NO                                                AS TAN_NO,
                                    CASE WHEN ISNULL(CLIENT.COMPANY_ID,0) = 0 THEN 1 ELSE 0 END AS SORTORDER
                             FROM (MST_COMPANY LEFT JOIN ( SELECT COMPANY_ID  FROM TRN_TAN_USER WHERE SETUP_ID = " + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + @" ) CLIENT
                                               ON MST_COMPANY.COMPANY_ID = CLIENT.COMPANY_ID)
                             WHERE INACTIVE_FLAG = 0";
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                strOrderBy = @" IIF(TRN_TAN_USER.COMPANY_ID, 1, 0) DESC,
                                    MST_COMPANY.COMPANY_NAME";

                strQuery = @"SELECT MST_COMPANY.COMPANY_ID                        AS COMPANY_ID,
                                    MST_COMPANY.COMPANY_NAME                      AS COMPANY_NAME,
                                    MST_COMPANY.TAN_NO                            AS TAN_NO,
                                    IIF( TRN_TAN_USER.COMPANY_ID, 1, 0)           AS SORT_ORDER
                            FROM (MST_COMPANY LEFT JOIN ( SELECT COMPANY_ID  FROM TRN_TAN_USER WHERE SETUP_ID = " + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + @" ) CLIENT
                                           ON MST_COMPANY.COMPANY_ID = CLIENT.COMPANY_ID)
                            WHERE INACTIVE_FLAG = 0";
            }
            //----------------




            //strOrderBy = @" TRN_TAN_USER.COMPANY_ID DESC,
            //                MST_COMPANY.COMPANY_NAME";

            //strQuery = @"SELECT MST_COMPANY.COMPANY_ID                        AS COMPANY_ID,
            //                    MST_COMPANY.COMPANY_NAME                      AS COMPANY_NAME,
            //                    MST_COMPANY.TAN_NO                            AS TAN_NO,
            //                    MST_COMPANY.COMPANY_NAME + MST_COMPANY.TAN_NO AS NAME_TAN
            //             FROM (MST_COMPANY LEFT JOIN TRN_TAN_USER
            //                               ON MST_COMPANY.COMPANY_ID = TRN_TAN_USER.COMPANY_ID)
            //             WHERE INACTIVE_FLAG = 0 ";
            //AND TRN_TAN_USER.SETUP_ID = " + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)); 

            //strOrderBy = @" COMPANY_NAME ";
            //strQuery = @"SELECT COMPANY_ID,
            //                    COMPANY_NAME,
            //                    TAN_NO
            //             FROM MST_COMPANY
            //             WHERE INACTIVE_FLAG = 0";

            //strQuery = @"SELECT COMPANY_ID,
            //                    COMPANY_NAME,
            //                    TAN_NO,
            //                    NAME_TAN
            //             FROM 
            //                 (SELECT MST_COMPANY.COMPANY_ID                        AS COMPANY_ID,
            //                         MST_COMPANY.COMPANY_NAME                      AS COMPANY_NAME,
            //                         MST_COMPANY.TAN_NO                            AS TAN_NO,
            //                         MST_COMPANY.COMPANY_NAME + MST_COMPANY.TAN_NO AS NAME_TAN
            //                  FROM (MST_COMPANY LEFT JOIN TRN_TAN_USER
            //                                    ON MST_COMPANY.COMPANY_ID = TRN_TAN_USER.COMPANY_ID)
            //                  WHERE INACTIVE_FLAG = 0
            //                  ORDER BY TRN_TAN_USER.COMPANY_ID DESC,
            //                           MST_COMPANY.COMPANY_NAME)
            //            ORDER BY COMPANY_NAME";



           strSQL = strQuery + " ORDER BY " + strOrderBy;

            blnStatus = false;
            //if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strLoadCompanyGrid);       //Show Data into the Grid

            TdsMan.PopulateGridView(grdvChildMenus, dmlService.J_pCommand, strSQL, strMatrix);
            blnStatus = true;

        }
        #endregion

        #region PopulateMenuRightsGrid
        private void PopulateCompanyRightsGrid(long UserID)
        {
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
                //grdvChildMenus.Sort(grdvChildMenus.Columns[1], ListSortDirection.Descending);

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

        private void txtNewSearch_TextChanged(object sender, EventArgs e)
        {
            if (cmbSelectUser.SelectedIndex < 0)
                return;
            strSearch = string.Empty;
            if (!string.IsNullOrEmpty(txtNewSearch.Text.Trim()))
            {
                strSearch = "AND MST_COMPANY.COMPANY_NAME + MST_COMPANY.TAN_NO LIKE '%" + cmnService.J_ReplaceQuote(txtNewSearch.Text.Trim().ToUpper()) + "%' ";

                strSQL = strQuery + strSearch + " ORDER BY " + strOrderBy;

                blnStatus = false;
                TdsMan.PopulateGridView(grdvChildMenus, dmlService.J_pCommand, strSQL, strMatrix);
                blnStatus = true;
            }
            else
            {
                strSQL = strQuery + strSearch + " ORDER BY " + strOrderBy;

                blnStatus = false;
                TdsMan.PopulateGridView(grdvChildMenus, dmlService.J_pCommand, strSQL, strMatrix);
                blnStatus = true;
            }
            PopulateCompanyRightsGrid(Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)));
        }

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0118", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}
