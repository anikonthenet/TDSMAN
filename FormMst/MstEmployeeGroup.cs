
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: MstCompany
Version			: 1.0
Start Date		: 26-10-2010
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
using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormPar;
using TDSMAN.FormSys;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;


#endregion


namespace TDSMAN.FormMst
{
    public partial class MstEmployeeGroup : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public MstEmployeeGroup()
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
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
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
        //-----------------------------------------------------------------------
        string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        //-----------------------------------------------------------------------
        string strFVUPath = "";
        bool IsComboSelection = false;
        bool IsTextSearch = false;
        bool blnSearchAll = false;
        bool blnExitBlank = true;
        bool blnExit = false;
        //
        long lngSelectedGrid = 0;
        //
        string strErrorWorksheetName = "Validation Error";
        //
        string strDefaultText = "ADD NEW EMPLOYEE GROUP HERE ...";
        //
        DataTable dtGridDataItems;
        //-- 12/09/2017 --
        string strEditModeText = "RENAME EMPLOYEE GROUP HERE ...";
        int CountSelectedItems = 0;
        int CountCheckedItems = 0;
        //
        string strExcelPath = "";
        //----------------
        string ExcelFileName = "EXPORTED_EMPLOYEE_DATA";
        string strTempEmployeeMasterPath = "";
        ToolTip tllTip = new ToolTip();
        ToolTip tllTipClose = new ToolTip();

        bool blResize = true;
        //-----------------------------------------------------------------------
        #endregion

        #region SET ENUM

        #region T_BulkExportEmployee
        public struct T_BulkExportEmployee
        {
            //public const string SDSerialNo = "Serial No";
            public const string PAN = "Employee PAN";
            public const string Name = "Employee Name";
            public const string GroupName = "Group Name";

        }
        #endregion

        //-----------------------------------------
        #endregion

        #region User Defined Events
        #region RESIZING WINDOW
        private void _Load(object sender, EventArgs e)
        {
            _form_resize._get_initial_size();
        }

        private void _Resize(object sender, EventArgs e)
        {
            if (blResize == true)
                _form_resize._resize();
            //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        }
        #endregion

        #region MstEmployeeGroup_Load
        private void MstEmployeeGroup_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            GC.Collect();
            //
            lblTitle.Text = "Tag Employees to Group";
            //
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);
            //-----------------------------------------------------------
            //
            #region tblTEMP_EMP_GRP_GRID_ITEMS_SELECTED
            //--
            if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EMP_GRP_GRID_ITEMS_SELECTED) == false)
            {
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMP_GRP_GRID_ITEMS_SELECTED + " (" +
                            "              EMPLOYEE_GRP_GRID_ID BIGINT IDENTITY(1,1)," +
                            "              EMPLOYEE_ID          BIGINT      DEFAULT 0," +
                            "              GROUP_ID             BIGINT      DEFAULT 0," +
                            "              EMPLOYEE_PAN         VARCHAR(10) DEFAULT ''," +
                            "              EMPLOYEE_NAME        VARCHAR(75) DEFAULT '')";
                    dmlService.J_ExecSql(strSQL);
                }
                else
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMP_GRP_GRID_ITEMS_SELECTED + " (" +
                            "              EMPLOYEE_GRP_GRID_ID  COUNTER," +
                            "              EMPLOYEE_ID           LONG     DEFAULT 0," +
                            "              GROUP_ID              LONG     DEFAULT 0," +
                            "              EMPLOYEE_PAN          TEXT(10) DEFAULT \"\"," +
                            "              EMPLOYEE_NAME         TEXT(75) DEFAULT \"\")";
                    dmlService.J_ExecSql(strSQL);
                }
                //--
            }
            //--
            #endregion
            //DisableControls();
            //-----------------------------------------------------------
            //ControlVisible(false);
            //-----------------------------------------------------------
            //-- COMPANY
            //-----------
            if (dmlService.J_ReturnNoOfRows("MST_COMPANY") > 0)
            {
                strSQL = " SELECT COMPANY_ID," +
                    "             COMPANY_NAME " + cmnService.J_ConcateSQLSyntaxOperator() + " ' [' " + cmnService.J_ConcateSQLSyntaxOperator() + " TAN_NO " + cmnService.J_ConcateSQLSyntaxOperator() + " ']' " +
                    "      FROM   MST_COMPANY " +
                    "      WHERE  INACTIVE_FLAG = 0 " +
                    "      ORDER BY COMPANY_NAME ";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            }
            else
            {
                grpEmployeeGroup.Enabled = false;
                grpEmployeeList.Enabled = false;
            }
            //
            cmbCompany.Select();
            //
            ClearControls();
        } 
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex <= 0)
            {
                string[,] strGridEmployee ={{"EMPLOYEE ID", "0", "", "R", "", "F", ""},
                                  {"Group ID", "0", "", "R", "", "F", ""},
                                  {"PAN", "0", "", "", "", "", "T"},
                                  {"NAME", "0", "", "", "", "", "T"}};
                strSQL = "SELECT MST_EMPLOYEE.EMPLOYEE_ID         AS EMPLOYEE_ID," +
                       "        MST_EMPLOYEE_GROUP.EMPLOYEE_GROUP_ID AS EMPLOYEE_GROUP_ID," +
                       "        MST_EMPLOYEE.EMPLOYEE_PAN            AS EMPLOYEE_PAN," +
                       "        MST_EMPLOYEE.EMPLOYEE_NAME           AS EMPLOYEE_NAME  " +
                       " FROM  (MST_EMPLOYEE LEFT JOIN MST_EMPLOYEE_GROUP " +
                       "    ON MST_EMPLOYEE.EMPLOYEE_GROUP_ID = MST_EMPLOYEE_GROUP.EMPLOYEE_GROUP_ID) " +
                       " WHERE  1 = 2 ";
                TdsMan.PopulateGridView(grdvEmployees, strSQL, strGridEmployee);
                lngSelectedGrid = 0;
                _form_resize._resize();
                //
                cmbEmployeeGroups.Items.Clear();
                //
                grpEmployeeGroup.Enabled = false;
                grpEmployeeList.Enabled = false;
                //
                _form_resize._resize();
                return;
            }
            else
            {
                grpEmployeeGroup.Enabled = true;
                grpEmployeeList.Enabled = true;
            }
            //--
            strSQL = "SELECT EMPLOYEE_GROUP_ID, EMPLOYEE_GROUP_DESC FROM MST_EMPLOYEE_GROUP WHERE COMPANY_ID IN (0, " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ")";
            dmlService.J_PopulateComboBox(strSQL, ref cmbEmployeeGroups, J_ComboBoxSelectedIndex.YES);
            //--
            cmbEmployeeGroups.Select();
        }
        #endregion

        #region cmbEmployeeGroups_SelectedIndexChanged
        private void cmbEmployeeGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsComboSelection = true;
            lblSelectionValue.Text = "";
            dmlService.J_ExecSql("DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMP_GRP_GRID_ITEMS_SELECTED);
            //--
            string[,] strGridEmployee ={{"EMPLOYEE ID", "0", "", "R", "", "F", ""},
                                  {"Group ID", "0", "", "R", "", "F", ""},
                                  {"PAN", "0", "", "", "", "", "T"},
                                  {"NAME", "0", "", "", "", "", "T"}};
            //--
            if (cmbEmployeeGroups.SelectedIndex <= 0)
            {
                btnSaveGroup.Enabled = false;
                btnSaveGroup.BackColor = Color.LightGray;
                btnCancelGroup.Enabled = false;
                btnCancelGroup.BackColor = Color.LightGray;
                btnEditGroup.Enabled = false;
                btnEditGroup.BackColor = Color.LightGray;
                btnDeleteGroup.Enabled = false;
                btnDeleteGroup.BackColor = Color.LightGray;
                BtnSave.Enabled = false;
                BtnSave.BackColor = Color.LightGray;
                btnTagUsingExcel.Enabled = false;
                btnTagUsingExcel.BackColor = Color.LightGray;
                //
                txtSearchAll.Enabled = false; //-- 11/09/2017 --
                txtSearchAll.Text = string.Empty;
                txtSearchAll.BackColor = Color.AliceBlue;
                chkSelectDeselect.Enabled = false;
                //
                lblMessage.Visible = false;
                //
                txtNewEmployeeGroup.Text = "";
                txtNewEmployeeGroup.Enabled = false;
                //
                strSQL = "SELECT MST_EMPLOYEE.EMPLOYEE_ID         AS EMPLOYEE_ID," +
                    "        MST_EMPLOYEE_GROUP.EMPLOYEE_GROUP_ID AS EMPLOYEE_GROUP_ID," +
                    "        MST_EMPLOYEE.EMPLOYEE_PAN            AS EMPLOYEE_PAN," +
                    "        MST_EMPLOYEE.EMPLOYEE_NAME           AS EMPLOYEE_NAME  " +
                    " FROM  (MST_EMPLOYEE LEFT JOIN MST_EMPLOYEE_GROUP " +
                    "    ON MST_EMPLOYEE.EMPLOYEE_GROUP_ID = MST_EMPLOYEE_GROUP.EMPLOYEE_GROUP_ID) " +
                    " WHERE  1 = 2 " +
                    " AND MST_EMPLOYEE.INACTIVE_FLAG = 0 " +
                    " AND MST_EMPLOYEE.COMPANY_ID    = " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex);
                TdsMan.PopulateGridView(grdvEmployees, strSQL, strGridEmployee);
                lngSelectedGrid = 0;
                _form_resize._resize();
                return;
            }
            //--
            if (Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex) == 1)
            {
                lblMode.Text = J_Mode.Add;
                //
                txtNewEmployeeGroup.Enabled = true;
                txtNewEmployeeGroup.ForeColor = Color.Gray;
                txtNewEmployeeGroup.Font = new Font("Lucida Console", 8, FontStyle.Regular);
                txtNewEmployeeGroup.Text = strDefaultText;
                //
                btnSaveGroup.Enabled = true;
                btnSaveGroup.BackColor = Color.AliceBlue;
                btnCancelGroup.Enabled = false;
                btnCancelGroup.BackColor = Color.LightGray;
                btnEditGroup.Enabled = false;
                btnEditGroup.BackColor = Color.LightGray;
                btnDeleteGroup.Enabled = false;
                btnDeleteGroup.BackColor = Color.LightGray;
                BtnSave.Enabled = false;
                BtnSave.BackColor = Color.LightGray;
                btnTagUsingExcel.Enabled = false;
                btnTagUsingExcel.BackColor = Color.LightGray;
                //
                grdvEmployees.Enabled = false;
                //-- 21/08/2017 --
                blnSearchAll = true;
                //
                txtSearchAll.Enabled = false; 
                txtSearchAll.Text = string.Empty;
                txtSearchAll.BackColor = Color.AliceBlue;
                chkSelectDeselect.Enabled = false;
                lblMessage.Visible = false;
            }
            else
            {
                lblMode.Text = J_Mode.View;        

                txtNewEmployeeGroup.Text = "";
                txtNewEmployeeGroup.Enabled = false;
                //
                btnSaveGroup.Enabled = false;
                btnSaveGroup.BackColor = Color.LightGray;
                btnCancelGroup.Enabled = true;
                btnCancelGroup.BackColor = Color.AliceBlue;
                btnEditGroup.Enabled = true;
                btnEditGroup.BackColor = Color.AliceBlue;
                btnDeleteGroup.Enabled = true;
                btnDeleteGroup.BackColor = Color.AliceBlue;
                BtnSave.Enabled = true;
                BtnSave.BackColor = Color.AliceBlue;
                btnTagUsingExcel.Enabled = true;
                btnTagUsingExcel.BackColor = Color.AliceBlue;
                //
                grdvEmployees.Enabled = true;
                //
                txtSearchAll.Enabled = true; //-- 11/09/2017 --
                txtSearchAll.BackColor = Color.White;
                txtSearchAll.ForeColor = Color.Blue;
                chkSelectDeselect.Enabled = true;
                //--
            }
            //--
            GC.Collect();
            //--
            strQuery = "SELECT MST_EMPLOYEE.EMPLOYEE_ID          AS EMPLOYEE_ID," +
                    "        MST_EMPLOYEE_GROUP.EMPLOYEE_GROUP_ID   AS EMPLOYEE_GROUP_ID," +
                    "        MST_EMPLOYEE.EMPLOYEE_PAN           AS EMPLOYEE_PAN," +
                    "        MST_EMPLOYEE.EMPLOYEE_NAME          AS EMPLOYEE_NAME  " +
                    " FROM  (MST_EMPLOYEE LEFT JOIN MST_EMPLOYEE_GROUP " +
                    "    ON  MST_EMPLOYEE.EMPLOYEE_GROUP_ID = MST_EMPLOYEE_GROUP.EMPLOYEE_GROUP_ID) " +
                    " WHERE  MST_EMPLOYEE.EMPLOYEE_GROUP_ID IN (0, " + Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex) + ") " +
                    " AND MST_EMPLOYEE.INACTIVE_FLAG        = 0 " +
                    " AND MST_EMPLOYEE.EMPLOYEE_PAN         <> 'PANNOTAVBL' " +
                    " AND    MST_EMPLOYEE.COMPANY_ID        = " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex);
            strOrderBy = " ORDER BY MST_EMPLOYEE.EMPLOYEE_GROUP_ID DESC, MST_EMPLOYEE.EMPLOYEE_NAME";
            strSQL = strQuery + strOrderBy; //-- 21/08/2017 --
            TdsMan.PopulateGridView(grdvEmployees, dmlService.J_pCommand, strSQL, strGridEmployee);
            lngSelectedGrid = 0;
            _form_resize._resize();
            //--
            //-- LOAD SELECTION ON THE GRID 
            CountSelectedItems = 0;
            CountCheckedItems = 0;
            foreach (DataGridViewRow row in grdvEmployees.Rows)
            {
                if (row.Cells[2].Value != null && cmnService.J_ReturnInt16Value(Convert.ToString(row.Cells[2].Value)) > 0)
                {
                    row.Cells[0].Value = true;

                    CountSelectedItems++;
                    //-- 23/08/2017 --
                    if (dmlService.J_ReturnNoOfRows("SELECT * FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMP_GRP_GRID_ITEMS_SELECTED + " WHERE EMPLOYEE_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)), J_QueryType.DirectQuery) == 0)
                    {
                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMP_GRP_GRID_ITEMS_SELECTED + " (EMPLOYEE_ID, GROUP_ID, EMPLOYEE_PAN, EMPLOYEE_NAME) VALUES(" +
                                            cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + "," +
                                            Convert.ToInt32(Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex)) + ",'" +
                                            cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[3].Value)) + "','" +
                                            cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[4].Value)) + "')";
                        dmlService.J_ExecSql(strSQL);
                        //
                        //CountCheckedItems++;
                        //intCount = intCount + 1;
                    }
                }
            }
            IsComboSelection = false;
            blnSearchAll = true;
            //--
            if (CountSelectedItems > 0)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "No. Employee(s) tagged to [ " + cmbEmployeeGroups.Text + " ] : " + CountSelectedItems.ToString();
            }
            else
                lblMessage.Visible = false;
            //--
            blnExit = false;
            chkSelectDeselect.Checked = false;
            blnExit = true;
        }
        #endregion
        
        #region BtnExit_Click

        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }

        #endregion
        
        #region btnEditGroup_Click
        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            if (Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex) > 1)
            {
                lblMode.Text = J_Mode.Edit;
                //
                //txtNewEmployeeGroup.Text = strEditModeText; //-- 12/09/2017 --
                //
                txtNewEmployeeGroup.Enabled = true;
                txtNewEmployeeGroup.ForeColor = Color.Black;
                txtNewEmployeeGroup.Font = new Font("Arial", 9, FontStyle.Bold);
                txtNewEmployeeGroup.Text = cmbEmployeeGroups.Text;
                blnExitBlank = false;
                txtNewEmployeeGroup.Select();
                blnExitBlank = true;
                //
                btnSaveGroup.Enabled = true;
                btnSaveGroup.BackColor = Color.AliceBlue;
                btnEditGroup.Enabled = false;
                btnEditGroup.BackColor = Color.LightGray;
                btnDeleteGroup.Enabled = false;
                btnDeleteGroup.BackColor = Color.LightGray;
                BtnSave.Enabled = false;
                BtnSave.BackColor = Color.LightGray;
                btnTagUsingExcel.Enabled = false;
                btnTagUsingExcel.BackColor = Color.LightGray;
                //
                cmbEmployeeGroups.Enabled = false;
                grdvEmployees.Enabled = false;
                chkSelectDeselect.Enabled = false;
                //
                grpCompany.Enabled = false;
            }
        }
        #endregion

        #region btnCancelGroup_Click
        private void btnCancelGroup_Click(object sender, EventArgs e)
        {
            lblMode.Text = J_Mode.View;
            //
            cmbEmployeeGroups.Enabled = true;
            grdvEmployees.Enabled = true;
            chkSelectDeselect.Enabled = true;
            grpCompany.Enabled = true;
            //
            txtNewEmployeeGroup.Text = "";
            txtNewEmployeeGroup.Enabled = false;
            //
            btnSaveGroup.Enabled = false;
            btnSaveGroup.BackColor = Color.LightGray;
            btnEditGroup.Enabled = true;
            btnEditGroup.BackColor = Color.AliceBlue;
            btnDeleteGroup.Enabled = true;
            btnDeleteGroup.BackColor = Color.AliceBlue;
            BtnSave.Enabled = true;
            BtnSave.BackColor = Color.AliceBlue;
            btnTagUsingExcel.Enabled = true;
            btnTagUsingExcel.BackColor = Color.AliceBlue;
            //
            cmbEmployeeGroups.Select();
        }
        #endregion

        #region btnSaveGroup_Click
        private void btnSaveGroup_Click(object sender, EventArgs e)
        {
            //long lngPayeeGroupID = 0;
            try
            {//-- Added By Abhishek Dey On 11/11/2019 --
                #region IS COMPANY ACCESSABLE FOR CLIENT
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                {
                    if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                    {
                        if (TdsMan.IsCompanyAccessible(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                        {
                            cmnService.J_UserMessage("You are not authorised to proceed.");
                            BtnCancel.Select();
                            return;
                        }

                        //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

                    }
                }
                #endregion
                //----------------
                //-- VALIDATE FIELDS
                #region VALIDATE FIELDS
                if (cmbEmployeeGroups.SelectedIndex > 0)
                {
                    //if (Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex) == 1)
                    //{
                    if (txtNewEmployeeGroup.Text.Trim() == "" || txtNewEmployeeGroup.Text.Trim() == strDefaultText || txtNewEmployeeGroup.Text.Trim() == strEditModeText)
                    {
                        cmnService.J_UserMessage("Employee Group - cannot be blank");
                        txtNewEmployeeGroup.Select();
                        return;
                    }
                    else
                    {
                        if (txtNewEmployeeGroup.Text.Trim() == TDSMAN.Classes.TDSMAN.T_AllEmployeesGroupSelectionCertificates.Trim())
                        {
                            cmnService.J_UserMessage("Employee Group - Please enter another group name\n as this is a reserve word");
                            txtNewEmployeeGroup.Select();
                            return;
                        }
                        //-- SAME PAYEE GROUP CHECK
                        //-------------------------
                        strSQL = "SELECT EMPLOYEE_GROUP_ID " +
                            "     FROM   MST_EMPLOYEE_GROUP " +
                            "     WHERE  EMPLOYEE_GROUP_DESC  ='" + cmnService.J_ReplaceQuote(txtNewEmployeeGroup.Text.Trim()) + "' " +
                            "     AND    COMPANY_ID           = " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex);
                        if (lblMode.Text == J_Mode.Edit)
                            strSQL = strSQL + "AND EMPLOYEE_GROUP_ID <> " + Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex);
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                        {
                            cmnService.J_UserMessage("Employee Group with same name in the selected Company already exists", MessageBoxIcon.Information);
                            txtNewEmployeeGroup.Select();
                            return;
                        }

                    }
                    //}
                }
                #endregion
                //--
                if (lblMode.Text == J_Mode.Add)
                    strSQL = @"INSERT INTO MST_EMPLOYEE_GROUP 
                                (EMPLOYEE_GROUP_DESC, 
                                COMPANY_ID) 
                              VALUES (
                                '" + cmnService.J_ReplaceQuote(txtNewEmployeeGroup.Text.Trim()) + @"',
                                 " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex) + @")";
                else if (lblMode.Text == J_Mode.Edit)
                    strSQL = "UPDATE MST_EMPLOYEE_GROUP " +
                        "     SET    EMPLOYEE_GROUP_DESC ='" + cmnService.J_ReplaceQuote(txtNewEmployeeGroup.Text.Trim()) + "' " +
                        "     WHERE  EMPLOYEE_GROUP_ID   = " + Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex);
                dmlService.J_BeginTransaction();
                dmlService.J_ExecSql(strSQL);
                dmlService.J_Commit();
                //
                string strNewPayeeGroup = txtNewEmployeeGroup.Text;
                //
                //strSQL = "SELECT EMPLOYEE_GROUP_ID," +
                //         "       EMPLOYEE_GROUP_DESC " +
                //         "FROM   MST_EMPLOYEE_GROUP " +
                //         "WHERE  INACTIVE_FLAG = 0 " +
                //         "ORDER BY EMPLOYEE_GROUP_DESC ";
                strSQL = "SELECT EMPLOYEE_GROUP_ID, EMPLOYEE_GROUP_DESC FROM MST_EMPLOYEE_GROUP WHERE COMPANY_ID IN (0, " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ")";            
                dmlService.J_PopulateComboBox(strSQL, ref cmbEmployeeGroups, J_ComboBoxSelectedIndex.YES);
                //
                cmbEmployeeGroups.Text = strNewPayeeGroup;
                //
                txtNewEmployeeGroup.Text = "";
                txtNewEmployeeGroup.Enabled = false;
                cmbEmployeeGroups.Enabled = true;
                grpCompany.Enabled = true;
                //
                lblMode.Text = J_Mode.View;
                //-----------------------------------------------------------                        
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region btnDeleteGroup_Click
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (cmbEmployeeGroups.SelectedIndex > 0)
            {
                //-- CHECK
                strSQL = "SELECT COUNT(*) FROM MST_EMPLOYEE WHERE EMPLOYEE_GROUP_ID = " + Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex);
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                {
                    cmnService.J_UserMessage("Payee(s) are tagged to this Payee Group\nUntagged the Employee(s), save & then Delete the Employee Group.");
                    cmbEmployeeGroups.Select();
                    return;
                }
                //--
                if (cmnService.J_UserMessage("Delete [ " + cmbEmployeeGroups.Text + " ] group ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    strSQL = "DELETE FROM MST_EMPLOYEE_GROUP WHERE EMPLOYEE_GROUP_ID = " + Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex);
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return;
                    //
                    strSQL = "UPDATE MST_EMPLOYEE SET EMPLOYEE_GROUP_ID = 0 WHERE EMPLOYEE_GROUP_ID = " + Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex);
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return;
                    //
                    strSQL = "SELECT EMPLOYEE_GROUP_ID, EMPLOYEE_GROUP_DESC FROM MST_EMPLOYEE_GROUP WHERE COMPANY_ID IN (0, " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ")";            
                    dmlService.J_PopulateComboBox(strSQL, ref cmbEmployeeGroups, J_ComboBoxSelectedIndex.YES);
                }
            }
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            long lngEmployeeID = 0;
            //
            try
            { 
                //-- Added By Abhishek Dey On 11/11/2019 --
                #region IS COMPANY ACCESSABLE FOR CLIENT
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                {
                    if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                    {
                        if (TdsMan.IsCompanyAccessible(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                        {
                            cmnService.J_UserMessage("You are not authorised to proceed.");
                            BtnCancel.Select();
                            return;
                        }

                        //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

                    }
                }
                #endregion
                //----------------
                if (cmbEmployeeGroups.SelectedIndex <= 0) { cmnService.J_UserMessage("No Group selected"); cmbEmployeeGroups.Select(); return; }
                //-- 11/09/2017 --
                int rowCount = 0;
                foreach (DataGridViewRow row in grdvEmployees.Rows)
                {
                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                    {
                        rowCount++;
                    }
                }
                if (rowCount == 0)
                {
                    if (CountSelectedItems == 0) //-- 12/09/2017 --
                    {
                        cmnService.J_UserMessage("Employee Group must contain atleast one Employee");
                        return;
                    }
                }
                //----------------

                //
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //
                foreach (DataGridViewRow row in grdvEmployees.Rows)
                {
                    int intEmployeeGroup = 0;
                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                        intEmployeeGroup = Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex);
                    //
                    lngEmployeeID = cmnService.J_ReturnInt16Value(Convert.ToString(row.Cells[1].Value));
                    //
                    strSQL = "UPDATE MST_EMPLOYEE SET EMPLOYEE_GROUP_ID = " + intEmployeeGroup + " WHERE EMPLOYEE_ID = " + lngEmployeeID;
                    dmlService.J_BeginTransaction();
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        return;
                    }
                    dmlService.J_Commit();
                }
                //
                ClearControls();
                //--
                cmnService.J_UserMessage("Saved sucessfully...");
                //--
                cmbEmployeeGroups.Select();
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion


        #region txtNewEmployeeGroup_Enter
        private void txtNewEmployeeGroup_Enter(object sender, EventArgs e)
        {
            if (blnExitBlank == false) return;
            //
            //lblMode.Text = T_Mode.Add; //-- 12/09/2017 --
            //-- 12/09/2017 --
            if (Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex) == 1)
            {
                lblMode.Text =  J_Mode.Add;//
                txtNewEmployeeGroup.ForeColor = Color.Black;
                txtNewEmployeeGroup.Font = new Font("Arial", 9, FontStyle.Bold);
                txtNewEmployeeGroup.Text = "";
            }
            else if (Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex) > 1)
            {
                lblMode.Text = J_Mode.Edit;
            }
            //-----------------
            
        }
        #endregion

        #region txtNewEmployeeGroup_Leave
        private void txtNewEmployeeGroup_Leave(object sender, EventArgs e)
        {
            if (txtNewEmployeeGroup.Text == "")
            {
                //-- 12/09/2017 --
                if (Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex) == 1)
                {
                    lblMode.Text = J_Mode.Add;
                    //txtNewEmployeeGroup.Text = "Add new Employee Group here ..";
                    txtNewEmployeeGroup.Text = strDefaultText;
                }
                else if (Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex) > 1)
                {
                    lblMode.Text = J_Mode.Edit;
                    txtNewEmployeeGroup.Text = strEditModeText; //-- 12/09/2017 --
                }
                //-----------------
                //lblMode.Text = T_Mode.View;   //-- 12/09/2017 --
                //
                txtNewEmployeeGroup.ForeColor = Color.Gray;
                txtNewEmployeeGroup.Font = new Font("Lucida Console", 8, FontStyle.Regular);
                //txtNewEmployeeGroup..Text = "Add new Payee Group here ..";
            }
        }
        #endregion

        #region txtSearchAll_TextChanged
        private void txtSearchAll_TextChanged(object sender, EventArgs e)
        {
            IsTextSearch = true; 
            //
            if (string.IsNullOrEmpty(cmbEmployeeGroups.Text.Trim()))
            {
                //ret
            }
            try
            {
                //--
                GC.Collect();
                //

                //
                string[,] strGridEmployee ={{"EMPLOYEE ID", "0", "", "R", "", "F", ""},
                                  {"Group ID", "0", "", "R", "", "F", ""},
                                  {"PAN", "0", "", "", "", "", "T"},
                                  {"NAME", "0", "", "", "", "", "T"}};
                //
                //--
                if (blnSearchAll == false)
                    return;
                //--
                //if (txtSearchAll.Text == CP.T_pSearchAllText)
                //    return;
                //--
                string strSearch = "";
                //--
                if (txtSearchAll.Text.Length > 0)
                    txtSearchAll.BackColor = Color.White;
                else
                    txtSearchAll.BackColor = Color.AliceBlue;
                //--
                strQuery = "SELECT MST_EMPLOYEE.EMPLOYEE_ID          AS EMPLOYEE_ID," +
                "        MST_EMPLOYEE_GROUP.EMPLOYEE_GROUP_ID   AS EMPLOYEE_GROUP_ID," +
                "        MST_EMPLOYEE.EMPLOYEE_PAN           AS EMPLOYEE_PAN," +
                "        MST_EMPLOYEE.EMPLOYEE_NAME          AS EMPLOYEE_NAME  " +
                " FROM  (MST_EMPLOYEE LEFT JOIN MST_EMPLOYEE_GROUP " +
                "    ON  MST_EMPLOYEE.EMPLOYEE_GROUP_ID = MST_EMPLOYEE_GROUP.EMPLOYEE_GROUP_ID) " +
                " WHERE  MST_EMPLOYEE.EMPLOYEE_GROUP_ID IN (0, " + Support.GetItemData(cmbEmployeeGroups, cmbEmployeeGroups.SelectedIndex) + ") " +
                " AND MST_EMPLOYEE.INACTIVE_FLAG        = 0 " +
                " AND MST_EMPLOYEE.EMPLOYEE_PAN         <> 'PANNOTAVBL' " +
                " AND    MST_EMPLOYEE.COMPANY_ID        = " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex);
                strOrderBy = " ORDER BY MST_EMPLOYEE.EMPLOYEE_GROUP_ID DESC, MST_EMPLOYEE.EMPLOYEE_NAME";
                //--
                strSearch = " AND (MST_EMPLOYEE.EMPLOYEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + @"%' OR
                                   MST_EMPLOYEE.EMPLOYEE_PAN  LIKE '" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%') ";
                //--
                strSQL = strQuery + strSearch + strOrderBy;
                //---------------------------
                TdsMan.PopulateGridView(grdvEmployees, dmlService.J_pCommand, strSQL, strGridEmployee); //-- 23/08/2017 --
                lngSelectedGrid = 0;
                _form_resize._resize();
                //-- 24/08/2017 --
                DataSet dsTemp = dmlService.J_ExecSqlReturnDataSet(" SELECT * FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMP_GRP_GRID_ITEMS_SELECTED + " ");
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        foreach (DataGridViewRow row in grdvEmployees.Rows)
                        {
                            if (dr["EMPLOYEE_ID"].ToString() == Convert.ToString(row.Cells[1].Value))
                            {
                                row.Cells[0].Value = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            IsTextSearch = false;
        }
        #endregion
        
        #region chkSelectDeselect_CheckedChanged
        private void chkSelectDeselect_CheckedChanged(object sender, EventArgs e)
        {
            if (blnExit == false) return;
            //
            if (grdvEmployees.Visible == false) { chkSelectDeselect.Checked = false; return; }
            //if (blnSelectDeselect == false) return;
            //--
            this.Cursor = Cursors.WaitCursor;
            //blnDeleteTempGridRecord = true;
            foreach (DataGridViewRow row in grdvEmployees.Rows)
            {
                if (chkSelectDeselect.Checked == true)//checked all checkbox
                {
                    if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                    {
                        row.Cells[0].Value = true;
                        //lngSelectedGrid = lngSelectedGrid+1;
                    }
                }
                else//Unchecked all checkbox
                {
                    if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                    {
                        //blnExitGrid = false;
                        row.Cells[0].Value = false;
                        //lngSelectedGrid = lngSelectedGrid-1;
                        //blnExitGrid = false;
                    }
                    //strSelectedLabel = "";
                }
            }                
            //--
            //if (lngSelectedGrid > 0)
            //    lblSelectionValue.Text = "Selected : " + lngSelectedGrid.ToString();
            //else
            //    lblSelectionValue.Text = "";
            //--
            //blnDeleteTempGridRecord = false; 
            this.Cursor = Cursors.Default;
        }
        #endregion

        #region btnCloseTaggingUsingExcel_Click
        private void btnCloseTaggingUsingExcel_Click(object sender, EventArgs e)
        {
            grpTagUsingExcel.Visible = false;
            //--
            btnEditGroup.Enabled = true;
            btnEditGroup.BackColor = Color.AliceBlue;
            btnDeleteGroup.Enabled = true;
            btnDeleteGroup.BackColor = Color.AliceBlue;
            btnCancelGroup.Enabled = true;
            btnCancelGroup.BackColor = Color.AliceBlue;
            BtnSave.Enabled = true;
            BtnSave.BackColor = Color.Lavender;
            BtnExit.Enabled = true ;
            BtnExit.BackColor = Color.Lavender;
            grpCompany.Enabled = true;
            grpEmployeeGroup.Enabled = true;
            //--
        }
        #endregion

        #region btnTagUsingExcel_Click
        private void btnTagUsingExcel_Click(object sender, EventArgs e)
        {
            grpTagUsingExcel.Visible = true;
            grpTagUsingExcel.Location = new Point(174, 115);
            //--
            btnEditGroup.Enabled = false;
            btnEditGroup.BackColor = Color.LightGray;
            btnDeleteGroup.Enabled = false;
            btnDeleteGroup.BackColor = Color.LightGray;
            btnCancelGroup.Enabled = false;
            btnCancelGroup.BackColor = Color.LightGray;
            BtnSave.Enabled = false;
            BtnSave.BackColor = Color.LightGray;
            BtnExit.Enabled = false;
            BtnExit.BackColor = Color.LightGray; 
            grpCompany.Enabled = false;
            grpEmployeeGroup.Enabled = false;
            //
            //-----------------------------------------------------------
            if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING) == true)
            {
                strOrderBy = "EMPLOYEE_NAME, EMPLOYEE_PAN";
                strQuery = "SELECT EMPLOYEE_ID," +
                          "        EMPLOYEE_PAN ," +
                          "        EMPLOYEE_NAME," +
                          "        GROUP_NAME " +
                          " FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " WHERE 1=2 ";
                //-----------------------------------------------------------
                strSQL = strQuery + " ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                string[,] strMatrixChallanDetails = {{"EMPLOYEE_ID", "0", "", "Right", "", "", ""},
                                        {"PAN", "100", "", "", "", "", "T"},
                                        {"EMPLOYEE NAME", "280", "", "", "", "", "T"},
                                        {"GROUP NAME", "130", "", "", "", "", "T"}};
                //--
                if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQLGridViewTabPages, strMatrixChallanDetails);       //Show Data into the Grid                
                dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewEmployeeExcel, strSQL, strMatrixChallanDetails);       //Show Data into the Grid
                //--
            }
            txtExcelPath.Text = "";
            grpEmployeeGrid.Enabled = false;
            //--

            _form_resize._resize();
        }
        #endregion
        
        #region btnSelectExcelPath_Click
        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                strFVUPath = cmnService.J_OpenFileDialog("Excel File | *.xlsx", "Excel File | *.xlsx", "Choose the Excel File to import");
            else
                strExcelPath = cmnService.J_OpenFileDialog("Excel File | *.xls; *.xlsx", "Excel File | *.xls; *.xlsx", "Choose the Excel File to import");
            //--
            if (strExcelPath != "")
            {
                txtExcelPath.Text = strExcelPath;
            }
            else
            {
                txtExcelPath.Text = strExcelPath;
            }
        }
        #endregion

        #region btnExportData_Click
        private void btnExportData_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variable Declaration

                string ExcelFilePath = "";
                int intIncrement = 0;

                // SALARY DETAILS
                string strSDSerialNo = string.Empty;
                string strEmployeePan = string.Empty;
                string strEmployeeName = string.Empty;
                //string strTotalTaxDeductedAmount = string.Empty;
                string strGroupName = string.Empty;

                #endregion

                #region Initialize Variable
                // SALARY DETAILS
                //strSDSerialNo = "[Serial No]";
                strEmployeePan  = "[Employee Pan]";
                strEmployeeName = "[Employee Name]";
                strGroupName    = "[Group Name]";


                long lngTotalEmployeeNo = 0;
                #endregion

                strSQL = @"SELECT COUNT(*) FROM MST_EMPLOYEE WHERE COMPANY_ID = " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex) + " AND  EMPLOYEE_PAN <> 'PANNOTAVBL'  AND  EMPLOYEE_GROUP_ID = 0  AND INACTIVE_FLAG = 0"; 
                lngTotalEmployeeNo = Convert.ToInt64(dmlService.J_ExecSqlReturnScalar(strSQL).ToString());

                if (lngTotalEmployeeNo > 0)  //-- 17/11/2018 --
                {
                    if (cmnService.J_UserMessage("Summary : \n No. of Employee(s): " + lngTotalEmployeeNo + " \nProceed ??", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
                else
                {
                    cmnService.J_UserMessage("No Employee available.", MessageBoxButtons.OK);
                    return;
                }


                this.Cursor = Cursors.WaitCursor;
                // Create a new instance of FolderBrowserDialog.
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                // A new folder button will display in FolderBrowserDialog.
                folderBrowserDlg.ShowNewFolderButton = true;
                //Show FolderBrowserDialog
                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                if (dlgResult.Equals(DialogResult.OK))
                {
                    //Show selected folder path in textbox1.
                    ExcelFilePath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                else
                    return;
                //--
                //--
                string strNewExcelFileName = string.Empty; //-- 09/11/2018 --
                strNewExcelFileName = ExcelFileName;
                //--
                do
                {
                    if (cmnService.J_IsFileExist(Path.Combine(ExcelFilePath, ExcelFileName + ".xls")) == true || cmnService.J_IsFileExist(Path.Combine(ExcelFilePath, ExcelFileName + ".xlsx")) == true)
                    {
                        intIncrement++;
                        ExcelFileName = strNewExcelFileName + " (" + intIncrement + ") ";
                    }
                } while (cmnService.J_IsFileExist(Path.Combine(ExcelFilePath, ExcelFileName + ".xls")) == true || cmnService.J_IsFileExist(Path.Combine(ExcelFilePath, ExcelFileName + ".xlsx")) == true);
                //----
                if (intIncrement > 0)
                    ExcelFileName = strNewExcelFileName + " (" + intIncrement + ") ";  //-- 09/11/2018 --
                else
                    ExcelFileName = strNewExcelFileName;
                //
                ExcelFilePath = Path.Combine(ExcelFilePath, ExcelFileName);
                //
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                //Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;


                //xlApp = new Excel.ApplicationClass();
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkBook.SaveAs(ExcelFilePath, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                //------------------------------------
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
                //
                #region EMPLOYEE

                if (CREATE_NEW_WORKSHEET(ExcelFilePath, T_Sheet_Name.BULK_EMPLOYEE_GROUP_TAG) == false) return;
                if (WRITE_COLUMN_HEADER_BULK_DELETION_WORKSHEET(ExcelFilePath, T_Sheet_Name.BULK_EMPLOYEE_GROUP_TAG) == false) return;
                //

                strSQL = @" SELECT   EMPLOYEE_PAN  AS " + strEmployeePan + "," +
                            "        EMPLOYEE_NAME AS " + strEmployeeName + "," +
                            "        ''            AS " + strGroupName + " " +
                            "FROM    MST_EMPLOYEE " +
                            "WHERE   COMPANY_ID        = " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex) + " " +
                            "AND     INACTIVE_FLAG     = 0 " +
                            "AND     EMPLOYEE_GROUP_ID = 0 " +
                            "AND     EMPLOYEE_PAN      <> 'PANNOTAVBL' " +
                            "ORDER BY EMPLOYEE_NAME, EMPLOYEE_PAN ";

                GC.WaitForPendingFinalizers();

                if (ExportToExcelFromDataTable(strSQL, T_Sheet_Name.BULK_EMPLOYEE_GROUP_TAG, ExcelFilePath) == false)
                {
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //
                if (DELETE_WORKSHEET(ExcelFilePath, "Sheet1") == false) //return;
                {
                }
                if (DELETE_WORKSHEET(ExcelFilePath, "Sheet2") == false) //return;
                {
                }
                if (DELETE_WORKSHEET(ExcelFilePath, "Sheet3") == false) //return;
                {
                }

                //if (KILL_EXCEL() == false)
                //    return;

                #endregion
                //
                this.Cursor = Cursors.Default;
                //
                //Deleting Temporary files
                Delete_TMP_Files(ExcelFilePath + ".xlsx");
                //
                this.Cursor = Cursors.Default;

                //OPEN THE EXCEL FILE
                System.Diagnostics.Process.Start(ExcelFilePath + ".xlsx");
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(ERR.Message);
            }
        }
        #endregion

        #region btnExcelValidate_Click
        private void btnExcelValidate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateFields() == false) return;
                //--
                if (cmnService.J_UserMessage("Proceed Excel file Validation?", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                //--                
                this.Cursor = Cursors.WaitCursor;
                //--
                lblValidationMessage.Visible = true;
                lblValidationMessage.Text = "Checking Excel structure started...";
                if (CheckExcelStructure(txtExcelPath.Text) == false)
                {
                    this.Cursor = Cursors.Default;
                    lblValidationMessage.Visible = false;
                    cmnService.J_UserMessage("Selected Excel file is invalid!!");
                    //prgBar.Value = 0;
                    btnSelectExcelPath.Select();
                    return;
                }
                //prgBar.Value = prgBar.Value + 5;
                //this.Refresh();
                //
                lblValidationMessage.Visible = true;
                lblValidationMessage.Text = "Process Started";
                //prgBar.Value prgBar.Value + 5;
                this.Refresh();
                // CREATE TEMP TABLES  
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    //MessageBox.Show("1");
                    if (CREATE_TEMP_TABLES_SQL() == false)
                    {
                        cmnService.J_UserMessage("Temporary Tables Not created", MessageBoxIcon.Exclamation);
                        this.Cursor = Cursors.Default;
                        //prgBar.Value 0;
                        return;
                    }
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    if (CREATE_TEMP_TABLES() == false)
                    {
                        cmnService.J_UserMessage("Temporary Tables Not created");
                        this.Cursor = Cursors.Default;
                        //prgBar.Value 0;
                        return;
                    }
                }
                //prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                // GET DATA FROM EXCEL ACCESS       
                //
                lblValidationMessage.Visible = true;
                lblValidationMessage.Text = "Transferring Data";
                //
                if (GET_DATA_FROM_EXCEL_ACCESS() == false)
                {
                    cmnService.J_UserMessage("Excel data import failed");
                    this.Cursor = Cursors.Default;
                    //prgBar.Value 0;
                    return;
                }
                //prgBar.Value prgBar.Value + 5;
                this.Refresh();
                // VALIDATE DATA       
                //
                lblValidationMessage.Visible = true;
                lblValidationMessage.Text = "Validation Started";
                //--
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING;
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) == 0)
                {
                    this.Cursor = Cursors.Default;
                    lblValidationMessage.Visible = true;
                    lblValidationMessage.Text = "Blank Excel file";
                    //
                    cmnService.J_UserMessage("Blank Excel file!!");
                    return;
                }
                //--
                if (VALIDATE_DATA() == false)
                {
                    cmnService.J_UserMessage("Data Validation failed");
                    this.Cursor = Cursors.Default;
                    //prgBar.Value 0;
                    return;
                }
                //prgBar.Value prgBar.Value + 5;
                this.Refresh();
                //--
                if (DELETE_WORKSHEET(txtExcelPath.Text) == false)
                {
                    cmnService.J_UserMessage("Error Sheet Deletion failed");
                    this.Cursor = Cursors.Default;
                    //prgBar.Value 0;
                    return;
                }
                //
                if (CREATE_NEW_WORKSHEET(txtExcelPath.Text) == false)
                {
                    cmnService.J_UserMessage("Error Sheet Creation failed");
                    this.Cursor = Cursors.Default;
                    //prgBar.Value 0;
                    return;
                }

                //prgBar.Value prgBar.Value + 5;
                this.Refresh();
                //
                lblValidationMessage.Visible = true;
                lblValidationMessage.Text = "New Worksheet created";
                //
                //--
                //prgBar.Value prgBar.Value + 5;
                this.Refresh();
                //
                //--
                lblValidationMessage.Visible = false;
                //
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""))) == 0)
                {
                    if (DELETE_WORKSHEET(txtExcelPath.Text, strErrorWorksheetName) == false) this.Cursor = Cursors.Default;
                    //-- UPDATE THE EMPLOYEE_ID
                    // ADD COLUMN "CONCATINATED_COLUMNS"
                    if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING, "GROUP_ID") == false)
                    {
                        strSQL = strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING, "GROUP_ID", "NUMBER", "", "NOT NULL", "0");
                        dmlService.J_ExecSql(strSQL);
                        strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " SET GROUP_ID = 0";
                        dmlService.J_ExecSql(strSQL);
                    }
                    //--
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + @" 
                         INNER JOIN MST_EMPLOYEE_GROUP 
                         ON         MST_EMPLOYEE_GROUP.EMPLOYEE_GROUP_DESC = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + @".GROUP_NAME
                         SET        " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + @".GROUP_ID = MST_EMPLOYEE_GROUP.EMPLOYEE_GROUP_ID
                         WHERE      MST_EMPLOYEE_GROUP.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));

                    dmlService.J_ExecSql(strSQL);
                    //--
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + @" 
                         INNER JOIN MST_EMPLOYEE 
                         ON         MST_EMPLOYEE.EMPLOYEE_PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + @".EMPLOYEE_PAN 
                         SET        " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + @".EMPLOYEE_ID = MST_EMPLOYEE.EMPLOYEE_ID
                         WHERE      MST_EMPLOYEE.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));

                    dmlService.J_ExecSql(strSQL);
                    //--
                    grpEmployeeGrid.Enabled = true;
                    //--
                    this.Cursor = Cursors.Default;
                    //--
                    cmnService.J_UserMessage("Excel File Validation is completed \nProceed to Tagging ", MessageBoxIcon.Information);
                }
                else
                {
                    if (WRITE_ERROR_WORKSHEET(txtExcelPath.Text) == false)
                    {
                        cmnService.J_UserMessage("Writing Error Sheet failed");
                        this.Cursor = Cursors.Default;
                        //prgBar.Value = 0;
                        return;
                    }
                    //prgBar.Value = prgBar.Value + 5;
                    this.Refresh();
                    this.Cursor = Cursors.Default;
                    //cmnService.J_UserMessage("Excel File Validation failed \n Check the <Validation Error> Sheet of the Excel file ", MessageBoxIcon.Exclamation);
                    if (cmnService.J_UserMessage("Excel File Validation failed \n Do you want to open the Excel file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        if (File.Exists(txtExcelPath.Text) == true)
                        {
                            System.Diagnostics.Process.Start(txtExcelPath.Text);
                        }
                    }
                    //prgBar.Value = 0;
                    return;
                }
                //--
                if (LOAD_IMPORT_INTERFACE() == false)
                {
                    cmnService.J_UserMessage("Loading Import Interface failed");
                    this.Cursor = Cursors.Default;
                    //prgBar.Value = 0;
                    return;
                }
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion
        
        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("MstEmployeeGroup");
            Tan.ShowDialog();
            //--------------
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #region lnkSearchByTAN_MouseMove
        private void lnkSearchByTAN_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(lnkSearchByTAN, "Click here to search the Company by TAN");
        }
        #endregion

        #region btnTag_Click
        private void btnTag_Click(object sender, EventArgs e)
        {

            if (cmnService.J_UserMessage("Proceed Tagging??", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            //--
            //prgImportBar.Value = 0;
            //--
            this.Cursor = Cursors.WaitCursor;
            //--                
            //prgImportBar.Value = prgImportBar.Value + 5;
            this.Refresh();
            //--
            if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING) == true)
            {
                //--
                strSQL = @"UPDATE MST_EMPLOYEE
                         INNER JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + @"  
                         ON         MST_EMPLOYEE.EMPLOYEE_ID = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + @".EMPLOYEE_ID 
                         SET        MST_EMPLOYEE.EMPLOYEE_GROUP_ID = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + @".GROUP_ID
                         WHERE      MST_EMPLOYEE.COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));

                dmlService.J_ExecSql(strSQL);
                //--
                //-----------------------------------------------------------
                string[,] strMatrixChallanDetails = {{"EMPLOYEE_ID", "0", "", "Right", "", "", ""},
                                        {"PAN", "100", "", "", "", "", ""},
                                        {"EMPLOYEE NAME", "280", "", "", "", "", ""},
                                        {"GROUP NAME", "130", "", "", "", "", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                strOrderBy = "EMPLOYEE_NAME, EMPLOYEE_PAN";
                strQuery = "SELECT EMPLOYEE_ID," +
                          "        EMPLOYEE_PAN ," +
                          "        EMPLOYEE_NAME," +
                          "        GROUP_NAME " +
                          " FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " WHERE 1=2 ";
                //-----------------------------------------------------------
                strSQL = strQuery + " ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQLGridViewTabPages, strMatrixChallanDetails);       //Show Data into the Grid                
                dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewEmployeeExcel, strSQL, strMatrixChallanDetails);       //Show Data into the Grid
                //--
                if (DROP_TEMP_TABLES() == false) return;
                //--
            }
            //for (int i = prgImportBar.Minimum; i <= prgImportBar.Maximum; i++)
            //{
            //    prgImportBar.PerformStep();
            //}
            this.Cursor = Cursors.Default;
            //--
            BtnSave.Enabled = false;
            //--
            cmnService.J_UserMessage("Tagging from Excel File completed", MessageBoxIcon.Information);
            //prgImportBar.Value = 0;
            //--
            cmbEmployeeGroups.SelectedIndex = 0;
            btnCloseTaggingUsingExcel_Click(sender, e);
        }
        #endregion

        #region btnCloseTaggingUsingExcel_MouseMove
        private void btnCloseTaggingUsingExcel_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipClose.SetToolTip(btnCloseTaggingUsingExcel, "Close this section");
        }
        #endregion



        #region grdvEmployees_CurrentCellDirtyStateChanged
        private void grdvEmployees_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grdvEmployees.IsCurrentCellDirty)
            {
                grdvEmployees.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        #endregion

        #region grdvEmployees_CellValueChanged
        private void grdvEmployees_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //
            //if (blnStatus == false) return;
            ////
            if (e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grdvEmployees.Rows[e.RowIndex].Cells[0];
                DataGridViewCell cellChildMenuID = (DataGridViewCell)grdvEmployees.Rows[e.RowIndex].Cells[1];
                //if (cell.Value == null || (bool)cell.Value == false)
                if ((bool)cell.Value == false)
                {
                    //grdvNewDescription.MultiSelect = false;
                    //grdvNewDescription.Rows[e.RowIndex].Selected = true;
                    //cell.Value = true;
                    //cmnService.J_UserMessage("UNCHECK"); 
                    //strSQL = "DELETE FROM TRN_USER_MENU_ACCESS WHERE SETUP_ID =" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + " AND CHILD_MENU_ID = " + Convert.ToInt32(cellChildMenuID.Value.ToString());
                    //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                    lngSelectedGrid = lngSelectedGrid - 1;
                }
                else if ((bool)cell.Value == true)
                {
                    //grdvNewDescription.Rows[e.RowIndex].Selected = false;
                    //cell.Value = false;
                    //cmnService.J_UserMessage("CHECK");
                    //strSQL = "INSERT INTO TRN_USER_MENU_ACCESS (SETUP_ID, CHILD_MENU_ID) VALUES (" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + "," + Convert.ToInt32(cellChildMenuID.Value.ToString()) + ")";
                    //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                    lngSelectedGrid = lngSelectedGrid + 1;
                }
                //--
                if (lngSelectedGrid > 0)
                    lblSelectionValue.Text = "Selected : " + lngSelectedGrid.ToString();
                else
                    lblSelectionValue.Text = "";
                //--
            }
            //cmnService.J_UserMessage(cellChildMenuID.Value.ToString());
            //--
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region LOAD_IMPORT_INTERFACE
        private bool LOAD_IMPORT_INTERFACE()
        {
            string strUpper = "";
            //--
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                strUpper = "UPPER";
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                strUpper = "UCASE";
            }
            //-----------------------------------------------------------
            string[,] strMatrixChallanDetails = {{"EMPLOYEE_ID", "0", "", "Right", "", "", ""},
                                        {"PAN", "100", "", "", "", "", ""},
                                        {"EMPLOYEE NAME", "280", "", "", "", "", ""},
                                        {"GROUP NAME", "130", "", "", "", "", ""}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            strOrderBy = "EMPLOYEE_NAME, EMPLOYEE_PAN";
            strQuery = "SELECT EMPLOYEE_ID," +
                      "        " + strUpper + "(EMPLOYEE_PAN)," +
                      "        " + strUpper + "(EMPLOYEE_NAME)," +
                      "        " + strUpper + "(GROUP_NAME)" +
                      " FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQLGridViewTabPages, strMatrixChallanDetails);       //Show Data into the Grid                
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewEmployeeExcel, strSQL, strMatrixChallanDetails);       //Show Data into the Grid
            return true;
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {   
                if (cmbCompany.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select Company");
                    cmbCompany.Select();
                    return false;
                }
                //-----------------------------------------------------------------------
                //-- EXCEL FILE SELECTED
                //-----------------------------------------------------------------------
                if (txtExcelPath.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Excel file not selected");
                    btnSelectExcelPath.Select();
                    return false;
                }
                // FILE SHOULD BE EXCEL
                if (Path.GetExtension(txtExcelPath.Text).ToUpper() != ".XLS" && Path.GetExtension(txtExcelPath.Text).ToUpper() != ".XLSX")
                {
                    cmnService.J_UserMessage("Selected file should be a Excel file");
                    btnSelectExcelPath.Select();
                    return false;
                }
                // FILE EXIST
                if (cmnService.J_IsFileExist(txtExcelPath.Text) == false)
                {
                    cmnService.J_UserMessage("Selected Excel file not found");
                    btnSelectExcelPath.Select();
                    return false;
                }
                // FILE OPEN
                //-- ANIK 2011-09-09
                string strPath = txtExcelPath.Text.ToString();
                //if (cmnService.J_IsProcessOpen(txtExcelPath.Text) == true)
                if (TdsMan.T_isFileOpenOrReadOnly(ref strPath) == true)
                {
                    cmnService.J_UserMessage("Selected Excel file is open");
                    btnSelectExcelPath.Select();
                    return false;
                }
                return true;
                
                //return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            //--
            if (cmbCompany.SelectedIndex > 0)
            {
                strSQL = "SELECT EMPLOYEE_GROUP_ID, EMPLOYEE_GROUP_DESC FROM MST_EMPLOYEE_GROUP WHERE COMPANY_ID IN (0, " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ")";

                dmlService.J_PopulateComboBox(strSQL, ref cmbEmployeeGroups, J_ComboBoxSelectedIndex.YES);
            }
            txtNewEmployeeGroup.Text = "";
            //
            //--
            txtSearchAll.Enabled = false; //-- 11/09/2017 --
            txtSearchAll.Text = string.Empty;
            txtSearchAll.BackColor = Color.AliceBlue;
            chkSelectDeselect.Enabled = false;
            _form_resize._resize();
        }
        #endregion
        
        #region ReleaseObject
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion


        #region DELETE WORKSHEET
        private bool DELETE_WORKSHEET(string ExcelFilePath)
        {
            try
            {
                //if (KILL_EXCEL() == false)
                //    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                foreach (Microsoft.Office.Interop.Excel.Worksheet ws in wb.Sheets)
                {
                    if (ws.Name.ToString().Trim() == strErrorWorksheetName)
                    {
                        ws.Delete();
                        break;
                    }
                }

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DELETE WORKSHEET
        private bool DELETE_WORKSHEET(string ExcelFilePath, string ExcelSheet)
        {
            try
            {
                //if (KILL_EXCEL() == false)
                //    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                foreach (Microsoft.Office.Interop.Excel.Worksheet ws in wb.Sheets)
                {
                    if (ws.Name.ToString().Trim() == ExcelSheet)
                    {
                        ws.Delete();
                        break;
                    }
                }

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion


        #region Delete_TMP_Files
        private void Delete_TMP_Files(string FilePath)
        {
            try
            {
                foreach (string sFile in System.IO.Directory.GetFiles(Path.GetDirectoryName(FilePath)))
                {
                    if (cmnService.J_IsProcessOpen(Path.Combine(Path.GetDirectoryName(FilePath), Convert.ToString(sFile))) == false)
                        if (sFile.ToUpper().EndsWith(".TMP"))
                            System.IO.File.Delete(sFile);
                }
            }
            catch
            {
            }
        }
        #endregion

        #region CREATE NEW WORKSHEET [OVERLOADED METHODS]

        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath)
        {
            try
            {
                //if (KILL_EXCEL() == false)
                //    return false;
                //
                //Microsoft.Office.Interop.Excel.Worksheet WrkSheet;
                //WrkSheet =   (Microsoft.Office.Interop.Excel.Worksheet)Globals.ThisWorkbook.Worksheets.Add(missing, missing, missing, missing);
                //
                //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //string myPath = @"" + ExcelFilePath;
                //excelApp.Workbooks.Open(myPath);
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                excelapp.DisplayAlerts = false;

                //if (excelapp == null) throw new Exception("Can't start Excel");
                if (excelapp == null) return false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                //if I create a new file and then add a worksheet,
                //it will exit normally (i.e. if you uncomment the next two lines
                //and comment out the .Open() line below):
                //Excel.Workbook wb = wbs.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                //wb.SaveAs(filename, m, m, m, m, m, 
                //          Excel.XlSaveAsAccessMode.xlExclusive,
                //          m, m, m, m, m);

                //but if I open an existing file and add a worksheet,
                //it won't exit (leaves zombie excel processes)
                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                Microsoft.Office.Interop.Excel.Worksheet wsnew = sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                wsnew.Name = strErrorWorksheetName;

                //N.B. it doesn't help if I try specifying the parameters in Add() above

                wb.Save();
                wb.Close(m, m, m);

                //overkill to do GC so many times, but shows that doesn't fix it
                //GC();
                //cleanup COM references
                //changing these all to FinalReleaseComObject doesn't help either
                //while (Marshal.ReleaseComObject(wsnew) > 0) { }
                wsnew = null;
                //while (Marshal.ReleaseComObject(sheets) > 0) { }
                sheets = null;
                //while (Marshal.ReleaseComObject(wb) > 0) { }
                wb = null;
                //while (Marshal.ReleaseComObject(wbs) > 0) { }
                wbs = null;
                //GC();
                excelapp.Quit();
                //while (Marshal.ReleaseComObject(excelapp) > 0) { }
                excelapp = null;
                //GC();

                return true;
            }
            catch
            {

                return false;
            }
        }
        #endregion

        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath, string WorksheetName)
        {
            try
            {
                //if (KILL_EXCEL() == false)
                //    return false;
                //
                //Microsoft.Office.Interop.Excel.Worksheet WrkSheet;
                //WrkSheet =   (Microsoft.Office.Interop.Excel.Worksheet)Globals.ThisWorkbook.Worksheets.Add(missing, missing, missing, missing);
                //
                //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //string myPath = @"" + ExcelFilePath;
                //excelApp.Workbooks.Open(myPath);
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                excelapp.DisplayAlerts = false;

                //if (excelapp == null) throw new Exception("Can't start Excel");
                if (excelapp == null) return false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                //if I create a new file and then add a worksheet,
                //it will exit normally (i.e. if you uncomment the next two lines
                //and comment out the .Open() line below):
                //Excel.Workbook wb = wbs.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                //wb.SaveAs(filename, m, m, m, m, m, 
                //          Excel.XlSaveAsAccessMode.xlExclusive,
                //          m, m, m, m, m);

                //but if I open an existing file and add a worksheet,
                //it won't exit (leaves zombie excel processes)
                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                //Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                //                             m, m, m, m, m, m,
                //                             Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook,
                //                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                Microsoft.Office.Interop.Excel.Worksheet wsnew = sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                wsnew.Name = WorksheetName;

                //N.B. it doesn't help if I try specifying the parameters in Add() above

                wb.Save();
                wb.Close(m, m, m);

                //overkill to do GC so many times, but shows that doesn't fix it
                //GC();
                //cleanup COM references
                //changing these all to FinalReleaseComObject doesn't help either
                //while (Marshal.ReleaseComObject(wsnew) > 0) { }
                wsnew = null;
                //while (Marshal.ReleaseComObject(sheets) > 0) { }
                sheets = null;
                //while (Marshal.ReleaseComObject(wb) > 0) { }
                wb = null;
                //while (Marshal.ReleaseComObject(wbs) > 0) { }
                wbs = null;
                //GC();
                excelapp.Quit();
                //while (Marshal.ReleaseComObject(excelapp) > 0) { }
                excelapp = null;
                //GC();

                return true;
            }
            catch (Exception e)
            {
                cmnService.J_UserMessage(e.Message);
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #endregion
        
        #region ExportToExcelFromDataTable
        private bool ExportToExcelFromDataTable(string strSQL, string SheetName, string strExcelFilePath)
        {
            //-- 20/02/2018 --
            //GC.Collect();
            GC.WaitForPendingFinalizers();
            //----------------
            //
            try
            {
                DataSet myDataSet = new DataSet();
                myDataSet = dmlService.J_ExecSqlReturnDataSet(strSQL);
                //
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbooks workbook = app.Workbooks;

                //
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Workbook wb = workbook.Open(strExcelFilePath,
                                         m, m, m, m, m, m,
                                         Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                         m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //

                int colIndex = 0;
                int rowIndex = 1;

                foreach (DataColumn dc in myDataSet.Tables[0].Columns)
                {
                    colIndex++;
                    wsnew.Cells[1, colIndex] = dc.ColumnName;
                }
                //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 -
                foreach (DataRow dr in myDataSet.Tables[0].Rows)
                {
                    rowIndex++;
                    colIndex = 0;

                    foreach (DataColumn dc in myDataSet.Tables[0].Columns)
                    {
                        colIndex++;
                        wsnew.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                    }
                }

                wsnew.Columns.AutoFit();

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                workbook = null;
                //
                wsnew = null;

                //Marshal.ReleaseComObject(wsnew);
                //Marshal.ReleaseComObject(wsnew);

                //wsnew.Delete(); //-- 20/02/2018 --

                app.Quit();
                app = null;
                //--
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
            return true;
        }
        #endregion

        #region WRITE_COLUMN_HEADER_BULK_DELETION_WORKSHEET
        private bool WRITE_COLUMN_HEADER_BULK_DELETION_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                //--
                //if (KILL_EXCEL() == false)
                //    return false;
                ////--
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                int intColumn = 64;
                //

                if (SheetName == T_Sheet_Name.BULK_EMPLOYEE_GROUP_TAG )
                {
                    #region EMPLOYEE
                    //wsnew.get_Range(Convert.ToChar(intColumn += 1) + "1", m).Value2 = T_BulkDeletionSheetSD.SDSerialNo;
                    //wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    //wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Borders.Value = true;
                    //wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).ColumnWidth = 21;
                    //wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).RowHeight = 50;
                    //wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).WrapText = true;
                    //wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Name = "Arial";
                    //wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Bold = true;
                    //wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Size = 10;
                    //wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    //wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

                    wsnew.get_Range(Convert.ToChar(intColumn += 1) + "1", m).Value2 = T_BulkExportEmployee.PAN;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Borders.Value = true;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).ColumnWidth = 21;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).RowHeight = 50;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).WrapText = true;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Name = "Arial";
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Bold = true;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Size = 10;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

                    wsnew.get_Range(Convert.ToChar(intColumn += 1) + "1", m).Value2 = T_BulkExportEmployee.Name;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Borders.Value = true;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).ColumnWidth = 21;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).RowHeight = 50;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).WrapText = true;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Name = "Arial";
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Bold = true;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Size = 10;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

                    wsnew.get_Range(Convert.ToChar(intColumn += 1) + "1", m).Value2 = T_BulkExportEmployee.GroupName;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Borders.Value = true;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).ColumnWidth = 21;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).RowHeight = 50;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).WrapText = true;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Name = "Arial";
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Bold = true;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Size = 10;
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    wsnew.get_Range(Convert.ToChar(intColumn) + "1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

                    #endregion

                    wsnew.get_Range("A:A", "D:D").Locked = true;
                    wsnew.get_Range("A1", "D1").Locked = true;
                }

                //
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Export data to Excel failed");
                //
                return false;
            }
        }
        #endregion

        #region CheckExcelStructure
        private bool CheckExcelStructure(string ExcelFilePath)
        {
            try
            {
                string strConnectionString = "";
                //--
                if (Path.GetExtension(txtExcelPath.Text.Trim()) == ".xls")
                    strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                else if (Path.GetExtension(txtExcelPath.Text.Trim()) == ".xlsx")
                    strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\"";
                //--
                OleDbConnection con = new OleDbConnection(strConnectionString);
                //--  
                con.Open();
                //--------------------
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Sheet_Name.BULK_EMPLOYEE_GROUP_TAG, "Employee PAN", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Sheet_Name.BULK_EMPLOYEE_GROUP_TAG, "Employee Name", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Sheet_Name.BULK_EMPLOYEE_GROUP_TAG, "Group Name", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //--------------------
                con.Close();
                con.Dispose();
                //--
                return true;
            }
            catch (SystemException e)
            {
                cmnService.J_UserMessage("Software is not able to connect to excel file. \nPlease create a new excel file, copy your data and then import the new one");
                return false;
            }
            catch (Exception e)
            {
                cmnService.J_UserMessage(e.Message);
                return false;
            }
        }
        #endregion

        #region CREATE_TEMP_TABLES
        private bool CREATE_TEMP_TABLES()
        {
            try
            {
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "";
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                ////Blocked by INDRAJIT on 16-03-2012
                //if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "") == false)
                //{
                //    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " (" + 
                //         "                  TEMP_MST_EMPLOYEE_GROUP_TAGGING_ID 
                //         "                  EMPLOYEE_PAN       TEXT(255) DEFAULT \"\"," +
                //         "                  EMPLOYEE_PAN_CELL  TEXT(10)  DEFAULT \"\"," +
                //         "                  EMPLOYEE_NAME      TEXT(255) DEFAULT \"\"," +
                //         "                  EMPLOYEE_NAME_CELL TEXT(10)  DEFAULT \"\"," +
                //         "                  GROUP_NAME         TEXT(255) DEFAULT \"\"," +
                //         "                  GROUP_NAME_CELL    TEXT(10)  DEFAULT \"\")";
                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}
                //
                //strSQL = "DELETE FROM TEMP_MST_DEDUCTEE";
                //if (dmlService.J_ExecSql(strSQL) == false)
                //    return false;
                //
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region CREATE_TEMP_ERR_TABLES
        private bool CREATE_TEMP_ERR_TABLES()
        {
            try
            {
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                         "                  ERR_VALIDATION_ID COUNTER," +
                         "                  ERR_TYPE          TEXT(50) DEFAULT \"\"," +
                         "                  ERR_CELL          TEXT(50) DEFAULT \"\"," +
                         "                  ERR_COLUMN        TEXT(50) DEFAULT \"\"," +
                         "                  ERR_SHEET         TEXT(50) DEFAULT \"\"," +
                         "                  ERR_COLOR         TEXT(25) DEFAULT \"\"," +
                         "                  ERR_DESC          TEXT(255) DEFAULT \"\"," +
                         "                  ERR_FORM_NO       TEXT(5) DEFAULT \"\")";
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                if (dmlService.J_ExecSql(strSQL) == false)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region CREATE_TEMP_TABLES_SQL
        private bool CREATE_TEMP_TABLES_SQL()
        {
            try
            {
                #region T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING
                dmlService.J_BeginTransaction();
                //MessageBox.Show("2");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "") == true)
                {
                    //MessageBox.Show("2.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //MessageBox.Show("3");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "") == false)
                {
                    //MessageBox.Show("3.1");
                    strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + @" (
                                            " + cmnService.J_GetDataType("TEMP_MST_EMPLOYEE_GROUP_TAGGING_ID", J_Identity.YES) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_ID", J_ColumnType.Long) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_PAN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_PAN_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_NAME_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("GROUP_NAME", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("GROUP_NAME_CELL", J_ColumnType.Char) + @")";
                    //MessageBox.Show(strSQL);
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                #endregion
                //
                dmlService.J_BeginTransaction();
                //--
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //--
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                         "                  " + cmnService.J_GetDataType("ERR_VALIDATION_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("MODE", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_CELL", J_ColumnType.String, 10) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLUMN", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_SHEET", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLOR", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_DESC", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_FORM_NO", J_ColumnType.String, 25) + ")";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //
                return true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
        }
        #endregion

        #region DROP_TEMP_TABLES
        private bool DROP_TEMP_TABLES()
        {
            try
            {
                dmlService.J_BeginTransaction();
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                dmlService.J_Commit();
                //--
                dmlService.J_BeginTransaction();
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                dmlService.J_Commit();
                //--
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region GET_DATA_FROM_EXCEL_ACCESS
        private bool GET_DATA_FROM_EXCEL_ACCESS()
        {
            try
            {
                #region VARIABLE_DECLARATION
                // DEDUCTEE MASTER
                string strEmployeeName = "";
                string strEmployeePAN = "";
                string strGroup = "";
                //--
                string strStartupPath = "";
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                    strStartupPath = Path.Combine(J_Var.J_pEnterpriseServerPath, "TMP FOLDER");
                else
                    strStartupPath = Path.Combine(Application.StartupPath, "TMP FOLDER");
                if (Directory.Exists(strStartupPath) == false)
                    // DELETE IF THE FILE EXISTS.
                    Directory.CreateDirectory(strStartupPath);
                //
                strTempEmployeeMasterPath = Path.Combine(Application.StartupPath, "EmployeeGroupTagging.txt");
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                {
                    strTempEmployeeMasterPath = Path.Combine(strStartupPath, TDSMAN.Classes.TDSMAN.T_pProductSerial + "_EmployeeGroupTagging.txt");
                    if (File.Exists(strTempEmployeeMasterPath) == true)
                        File.Delete(strTempEmployeeMasterPath);
                }
                string tableName = "";
                string textfileName = "";

                #endregion

                #region INSERTING DATA TO TEXT FILE

                //if (KILL_EXCEL() == false)
                //    return false;
                //// READ EXCEL FILE
                DataSet myDataSet;
                OleDbDataAdapter myCommand;
                string strConnectionString = "";
                //
                if (Path.GetExtension(txtExcelPath.Text.Trim()) == ".xls")
                    strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                else if (Path.GetExtension(txtExcelPath.Text.Trim()) == ".xlsx")
                    strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\"";
                //
                OleDbConnection con = new OleDbConnection(strConnectionString);
                //                  
                con.Open();
                //
                // INSERT DEDUCTEE MASTER
                //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                myDataSet = new DataSet();
                myCommand = new OleDbDataAdapter("SELECT * FROM [" + T_Sheet_Name.BULK_EMPLOYEE_GROUP_TAG + "$]", con);
                myCommand.Fill(myDataSet);
                //con.Close();

                long lngRow = 2;
                long lngColumn = 0;

                int intLineNumber = 0;

                StreamWriter StreamWriterDeductee = cmnService.J_ReturnStreamWriter(strTempEmployeeMasterPath);

                //Travers through each row in the dataset
                foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
                {
                    //lblProgressDisplayMessage.Visible = true;
                    //lblProgressDisplayMessage.Text = "Transferring Data [" + T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER + " : " + lngRow + " ]";
                    //this.Refresh();
                    //////
                    //lngColumn =65;
                    ////Stores info in Datarow into an array
                    Object[] cells = myDataRow.ItemArray;

                    intLineNumber = intLineNumber + 1;
                    int intColumnValue = 64;
                    //
                    strEmployeePAN = Convert.ToString(cells[0]);
                    strEmployeeName = Convert.ToString(cells[1]);
                    strGroup = Convert.ToString(cells[2]);
                    // CHECK BLANK ROW TO EXIT
                    if (strEmployeeName == "" &&
                            strEmployeePAN == "" &&
                                strGroup == "" )
                        break;

                    cmnService.J_WriteLine(ref StreamWriterDeductee, TdsMan.T_WriteField(intLineNumber.ToString()) + TdsMan.T_WriteField("0") +
                                                                     TdsMan.T_WriteField(strEmployeePAN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 1)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strEmployeeName) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 2)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strGroup) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 3)) + (Convert.ToString(intLineNumber + 1))))); 
                }
                //
                myDataSet.Dispose();
                myCommand.Dispose();

                StreamWriterDeductee.Flush();
                StreamWriterDeductee.Close();

                #endregion

                #region TRANSFERRING DATA FROM TEXT FILE TO ACCESS

                //Added by INDRAJIT on 16-03-2012

                //Array to hold data segment name
                tableName = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "";

                textfileName = "EmployeeGroupTagging";

                TdsMan.T_ReplaceDoubleQuotesinFile(strTempEmployeeMasterPath, true);

                ImportTextToTables(tableName, textfileName, strTempEmployeeMasterPath, false);

                // DELETE THE TXT FILE
                if (File.Exists(strTempEmployeeMasterPath) == true)
                    File.Delete(strTempEmployeeMasterPath);

                #endregion

                myDataSet.Dispose();
                myCommand.Dispose();

                //
                con.Close();
                con.Dispose();
                //
                return true;
                //############################################
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Import Text To Tables
        //Added by INDRAJIT on 16-03-2012

        private void ImportTextToTables(string tbl, string txtfile, string FilePath, bool hdr)
        {
            //Check 'n Create SCHEMA file for Temp Tables
            string strFolderPath = cmnService.J_GetDirectoryName(strTempEmployeeMasterPath);

            if (File.Exists(strFolderPath + "\\schema.ini") == true)
            {
                File.Delete(strFolderPath + "\\schema.ini");
            }
            StreamWriter StreamWriter = new StreamWriter(strFolderPath + "\\schema.ini");

            StreamWriter.WriteLine("[" + txtfile + ".txt]");
            StreamWriter.WriteLine("ColNameHeader=" + (hdr == true ? "True" : "False") + "");
            StreamWriter.WriteLine("Format=Delimited(^)");
            StreamWriter.WriteLine("MaxScanRows=0");
            StreamWriter.WriteLine("CharacterSet=ANSI");
            //if (txtfile == "EmployeeMaster")
            //{
                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "
                StreamWriter.WriteLine(@"Col1=TEMP_MST_EMPLOYEE_GROUP_TAGGING_ID Integer
                                         Col2=EMPLOYEE_ID Integer
                                         Col3=EMPLOYEE_PAN Char
                                         Col4=EMPLOYEE_PAN_CELL Char
                                         Col5=EMPLOYEE_NAME Char
                                         Col6=EMPLOYEE_NAME_CELL Char
                                         Col7=GROUP_NAME Char
                                         Col8=GROUP_NAME_CELL Char");
                #endregion
            //}

            StreamWriter.Close();
            //--
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                //Check 'n Create Temp Tables
                if (dmlService.J_IsDatabaseObjectExist(tbl) == true)
                {
                    strSQL = "DELETE FROM [" + tbl + "]";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                //if (Convert.ToString(File.ReadAllText(FilePath)) != "")
                if (Convert.ToString(File.ReadAllLines(FilePath)) != "")
                {
                    strSQL = @"BULK INSERT [" + tbl + "] FROM '" + FilePath + "' WITH (fieldterminator = '^', rowterminator = '\n')";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                TdsMan.SHRINK_DATABASE();
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                //Check 'n Create Temp Tables
                if (dmlService.J_IsDatabaseObjectExist(tbl) == true)
                {
                    strSQL = "DROP TABLE [" + tbl + "]";

                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "SELECT * INTO [" + tbl + "] FROM " +
                         @"[Text; DATABASE=" + strFolderPath + "].[" + txtfile + ".txt]";

                dmlService.J_ExecSql(strSQL);
            }
        }
        #endregion

        #region VALIDATE_DATA
        private bool VALIDATE_DATA()
        {
            string strSheetName = "";
            string strMidSubString = "";
            string strUpper = "";
            int lngRowCount;
            try
            {
                if (CREATE_TEMP_ERR_TABLES() == false)
                    return false;
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    strMidSubString = "SUBSTRING";
                    strUpper = "UPPER";
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    strMidSubString = "MID";
                    strUpper = "UCASE";
                }
                //--
                strSheetName = T_Sheet_Name.BULK_EMPLOYEE_GROUP_TAG;
                
                #region EMPLOYEE PAN

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " SET EMPLOYEE_PAN = '' WHERE EMPLOYEE_PAN IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                    "     WHERE  EMPLOYEE_PAN = ''" +
                    "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                        "     WHERE  EMPLOYEE_PAN = ''" +
                        "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                    "     WHERE  LEN(EMPLOYEE_PAN) <> 10" +
                    "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "             EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                        "     WHERE  LEN(EMPLOYEE_PAN) <> 10" +
                        "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //'PANNOTAVBL' CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                    "     WHERE  EMPLOYEE_PAN = 'PANNOTAVBL' " +
                    "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                        "     WHERE  EMPLOYEE_PAN = 'PANNOTAVBL' " +
                        "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //PAN STRUCTURE CHECK
                strSQL = "SELECT COUNT(*)" +
                 "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                 "     WHERE  (ISNUMERIC(LEFT(EMPLOYEE_PAN,1)) <> 0" +
                 "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,2,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,3,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,4,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,5,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,6,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,7,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,8,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,9,1)) = 0 " +
                 "     OR     ISNUMERIC(RIGHT(EMPLOYEE_PAN,1)) = -1)" +
                 "     AND    EMPLOYEE_PAN <> 'PANNOTAVBL'" +
                 "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                        "     WHERE  (ISNUMERIC(LEFT(EMPLOYEE_PAN,1)) <> 0" +
                        "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,2,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,3,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,4,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,5,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,6,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,7,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,8,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(EMPLOYEE_PAN,9,1)) = 0 " +
                        "     OR     ISNUMERIC(RIGHT(EMPLOYEE_PAN,1)) = -1)" +
                        "     AND    EMPLOYEE_PAN <> 'PANNOTAVBL'" +
                        "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //-- AVAILABILITY
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " " +
                    "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ".EMPLOYEE_PAN NOT IN " +
                    "            (SELECT EMPLOYEE_PAN FROM  MST_EMPLOYEE WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " AND EMPLOYEE_GROUP_ID = 0)" +
                    "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.MISMATCH_CHECK + "'," +
                        "             EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISMATCH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                        "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ".EMPLOYEE_PAN NOT IN " +
                        "            (SELECT EMPLOYEE_PAN FROM  MST_EMPLOYEE WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " AND EMPLOYEE_GROUP_ID = 0)" +
                        "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region EMPLOYEE NAME

                ////UPDATE ALL NULL RECORDS TO ''
                //strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " SET EMPLOYEE_NAME = '' WHERE EMPLOYEE_NAME IS NULL";
                //dmlService.J_ExecSql(strSQL);

                ////BLANK OR 0 CHECK
                //strSQL = "SELECT COUNT(*)" +
                //    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                //    "     WHERE  EMPLOYEE_NAME = ''" +
                //    "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                ////
                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount > 0)
                //{
                //    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //        "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                //        "            EMPLOYEE_NAME_CELL AS ERROR_CELL," +
                //        "            'EMPLOYEE_NAME_CELL'," +
                //        "            '" + strSheetName + "'," +
                //        "            '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                //        "     WHERE  EMPLOYEE_NAME = ''" +
                //        "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";

                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}

                ////LENGTH CHECK
                //strSQL = "SELECT COUNT(*)" +
                //    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                //    "     WHERE  LEN(EMPLOYEE_NAME) > 75" +
                //    "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                ////
                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount > 0)
                //{
                //    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                //        "            EMPLOYEE_NAME_CELL AS ERROR_CELL," +
                //        "            'EMPLOYEE_NAME_CELL'," +
                //        "            '" + strSheetName + "'," +
                //        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                //        "     WHERE  LEN(EMPLOYEE_NAME) > 75" +
                //        "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //    //
                //    //                
                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}

                ////DUPLICATE CHECK
                ////WHEN EMPLOYEE_PAN = 'PANNOTAVBL'
                //strSQL = "SELECT COUNT(EMPLOYEE_PAN) " +
                //         "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " " +
                //         "WHERE  EMPLOYEE_PAN     = 'PANNOTAVBL' " +
                //         "AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                //         "GROUP BY EMPLOYEE_NAME " +
                //         "HAVING COUNT(EMPLOYEE_NAME) > 1";
                ////
                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount > 0)
                //{
                //    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //        "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                //        "             EMPLOYEE_NAME_CELL AS ERROR_CELL," +
                //        "             'EMPLOYEE_NAME_CELL'," +
                //        "             '" + strSheetName + "'," +
                //        "             '" + T_Error_Type_Color.DUPLICATE_CHECK + "'" +
                //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                //        "     WHERE  EMPLOYEE_NAME IN (SELECT EMPLOYEE_NAME " +
                //        "                             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " " +
                //        "                             WHERE  EMPLOYEE_PAN     = 'PANNOTAVBL' " +
                //        "                             GROUP BY EMPLOYEE_NAME,EMPLOYEE_PAN  " +
                //        "                             HAVING COUNT(EMPLOYEE_NAME) > 1) " +
                //        "     AND    EMPLOYEE_PAN     = 'PANNOTAVBL' " +
                //        "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //    //              
                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}
                
                ////-- AVAILABILITY
                //strSQL = "SELECT COUNT(*)" +
                //    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " " +
                //    "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ".EMPLOYEE_NAME NOT IN " +
                //    "            (SELECT EMPLOYEE_NAME FROM  MST_EMPLOYEE WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " AND EMPLOYEE_GROUP_ID = 0)" +
                //    "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                ////
                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount > 0)
                //{
                //    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //        "      SELECT '" + T_Error_Type.MISMATCH_CHECK + "'," +
                //        "             EMPLOYEE_NAME_CELL AS ERROR_CELL," +
                //        "             'EMPLOYEE_NAME_CELL'," +
                //        "             '" + strSheetName + "'," +
                //        "             '" + T_Error_Type_Color.MISMATCH_CHECK + "'" +
                //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                //        "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ".EMPLOYEE_NAME NOT IN " +
                //        "            (SELECT EMPLOYEE_NAME FROM  MST_EMPLOYEE WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " AND EMPLOYEE_GROUP_ID = 0)" +
                //        "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //    //              
                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}
                #endregion

                #region EMPLOYEE ALREADY TAGGED TO GROUP
                strSQL = @"SELECT COUNT(*) " +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ", MST_EMPLOYEE " +
                    "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ".EMPLOYEE_PAN = MST_EMPLOYEE.EMPLOYEE_PAN " +
                    "     AND    MST_EMPLOYEE.EMPLOYEE_GROUP_ID > 0 " +
                    "     AND    MST_EMPLOYEE.COMPANY_ID        = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                    "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.MISMATCH_CHECK + "'," +
                        "             EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISMATCH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ", MST_EMPLOYEE " +
                        "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ".EMPLOYEE_PAN = MST_EMPLOYEE.EMPLOYEE_PAN " +
                        "     AND    MST_EMPLOYEE.EMPLOYEE_GROUP_ID > 0 " +
                        "     AND    MST_EMPLOYEE.COMPANY_ID        = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                        "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion
                // CHECK IF RECORD IS EXISTS OR NOT
                #region CHECK IF RECORD IS EXISTS OR NOT
                // ADD COLUMN "CONCATENATED_COLUMNS"
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING, "CONCATENATED_COLUMNS") == false)
                {
                    strSQL = strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING, "CONCATENATED_COLUMNS", "MEMO", "", "NOT NULL", "\"\"");
                    dmlService.J_ExecSql(strSQL);
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " SET CONCATENATED_COLUMNS = ' '";
                    dmlService.J_ExecSql(strSQL);
                }
                //-----------------------------------------
                // ADD COLUMN "CONCATENATED_COLUMNS"
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING, "CONCATENATED_COLUMNS") == false)
                {
                    strSQL = strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING, "CONCATENATED_COLUMNS", "MEMO", "", "NOT NULL", "\"\"");
                    dmlService.J_ExecSql(strSQL);
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " SET CONCATENATED_COLUMNS = ' '";
                    dmlService.J_ExecSql(strSQL);
                }
                //-----------------------------------------
                #endregion

                #region GROUP NAME

                //UPDATE ALL NULL RECORDS TO ''
                 strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " SET GROUP_NAME = '' WHERE GROUP_NAME IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK

                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                    "     WHERE  GROUP_NAME = '' " +
                    "     AND    GROUP_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             GROUP_NAME_CELL AS ERROR_CELL," +
                        "             'GROUP_NAME_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + "" +
                        "     WHERE  GROUP_NAME = ''" +
                        "     AND    GROUP_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                // VALIDITY CHECK

                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " " +
                    "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ".GROUP_NAME NOT IN " +
                    "             (SELECT EMPLOYEE_GROUP_DESC " +
                    "              FROM   MST_EMPLOYEE_GROUP " +
                    "              WHERE  COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ") " +
                    "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ".GROUP_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             GROUP_NAME_CELL AS ERROR_CELL," +
                        "             'GROUP_NAME_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + " " +
                        "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ".GROUP_NAME NOT IN " +
                        "             (SELECT EMPLOYEE_GROUP_DESC " +
                        "              FROM   MST_EMPLOYEE_GROUP " +
                        "              WHERE  COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ") " +
                        "     AND   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE_GROUP_TAGGING + ".GROUP_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                return true;
            }
            catch (Exception e)
            {
                cmnService.J_UserMessage(e.Message);
                return false;
            }
        }
        #endregion

        #region WRITE ERROR WORKSHEET
        private bool WRITE_ERROR_WORKSHEET(string ExcelFilePath)
        {

            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            string strMatchSheetName = T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER;
            int intSkipIF = 0;
            try
            {
                //if (KILL_EXCEL() == false)
                //    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                //Microsoft.Office.Interop.Excel.Worksheet wsnew =  sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                //wsnew.Name = strErrorWorksheetName;

                //@@@@@@@@@@@@@@@
                //long lngTotalRecordsErrorSheet = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "")));
                // 
                wsnew.get_Range("B:B", m).ColumnWidth = 150;
                wsnew.get_Range("B2", m).Value2 = "Error Validations";
                wsnew.get_Range("B2", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                wsnew.get_Range("B2", m).Font.Size = 15;
                //
                //wsnew.get_Range("B4", m).Value2 = T_Sheet_Name.CHALLAN_DETAILS;
                //wsnew.get_Range("B4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);               
                //
                strSQL = "SELECT ERR_TYPE,  " +
                    "            ERR_CELL,  " +
                    "            ERR_COLUMN," +
                    "            ERR_SHEET, " +
                    "            ERR_DESC   " +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "     ORDER BY ERR_SHEET," +
                    "            ERR_VALIDATION_ID";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    return false;
                }
                while (drdGetErrorSheetRecord.Read())
                {
                    if (intSkipIF == 0)
                    {
                        strMatchSheetName = drdGetErrorSheetRecord["ERR_SHEET"].ToString();
                        //if (strMatchSheetName != T_Sheet_Name.CHALLAN_DETAILS)
                        //{
                        lngErrorSheetRow = lngErrorSheetRow + 2;
                        //
                        wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER;
                        wsnew.get_Range("B" + lngErrorSheetRow, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                        //
                        lngErrorSheetRow = lngErrorSheetRow + 1;
                        intSkipIF = 1;
                        //}
                    }
                    //
                    wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = "Cell : " + drdGetErrorSheetRecord["ERR_CELL"].ToString() + " - [" + drdGetErrorSheetRecord["ERR_COLUMN"].ToString().Replace("_", " ").Replace("CELL", "") + "] " + drdGetErrorSheetRecord["ERR_TYPE"].ToString();
                    wsnew.get_Range("B" + lngErrorSheetRow, m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    //
                    lngErrorSheetRow = lngErrorSheetRow + 1;
                }
                drdGetErrorSheetRecord.Close();
                drdGetErrorSheetRecord.Dispose();
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #endregion

        #region MstEmployeeGroup_Activated
        private void MstEmployeeGroup_Activated(object sender, EventArgs e)
        {
            blResize = false;
        }
        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0106", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }

}
