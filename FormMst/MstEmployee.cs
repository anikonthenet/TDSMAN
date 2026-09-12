#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: MstDeductee
Version			: 1.0
Start Date		: 18-11-2010
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
using System.IO;
using System.Text;

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormTrn;

using Excel = Microsoft.Office.Interop.Excel;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion


namespace TDSMAN.FormMst
{
    public partial class MstEmployee : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public MstEmployee()
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
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
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

        int intCountRecords = 0;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        ToolTip tllTip = new ToolTip();

        int intCompanyIndex;
        //-----------------------------------------------------------------------
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
            //_form_resize._dgv_Column_Adjust(dgvGrid, true);
        }
        #endregion

        #region MstEmployee_Load

        private void MstEmployee_Load(object sender, EventArgs e)
        {
            try
            {
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //-----------------------------------------------------------
                GC.Collect();
                //
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                dgvGrid.Visible = true;
                //--
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                    BtnPrint.Visible = false;
                //-----------------------------------------------------------
                //DisableControls();
                //-----------------------------------------------------------
                dgvGrid.Height = 518;
                //
                ControlVisible(false);
                ClearControls();
                //-----------------------------------------------------------
                //-- COMPANY
                //-----------
                if (dmlService.J_ReturnNoOfRows("MST_COMPANY") > 0)
                {
                    //strSQL = " SELECT COMPANY_ID," +
                    //    "             COMPANY_NAME & ' [' & TAN_NO & ']' " +
                    //    "      FROM   MST_COMPANY " +
                    //    "      WHERE  INACTIVE_FLAG = 0 " +
                    //    "      ORDER BY COMPANY_NAME ";
                    strSQL = " SELECT COMPANY_ID," +
                        "             COMPANY_NAME " + cmnService.J_ConcateSQLSyntaxOperator() + " ' [' " + cmnService.J_ConcateSQLSyntaxOperator() + " TAN_NO " + cmnService.J_ConcateSQLSyntaxOperator() + " ']' " +
                        "      FROM   MST_COMPANY " +
                        "      WHERE  INACTIVE_FLAG = 0 " +
                        "      ORDER BY COMPANY_NAME ";
                    if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompanyName, 1, J_ComboBoxSelectedIndex.YES) == false) return;
                }
                //-----------------------------------------------------------
                lblTitle.Text = "Employee Master";
                //-----------------------------------------------------------
                dgvGrid_Click(sender, e);
                //Added by Indrajit on 12-02-2013
                //Starting the timer
                tmrGridRefresh.Start();
                //--------------------------------------------                
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }

        #endregion

        #region cmbCompanyName_SelectedIndexChanged
        private void cmbCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //-- set the Help Grid Column Header Text & behavior
            //-- (0) Header Text
            //-- (1) Width
            //-- (2) Format
            //-- (3) Alignment
            //-- (4) NullToText
            //-- (5) Visible
            //-- (6) AutoSizeMode
            //-----------------------------------------------------------
            string[,] strShowHelpEmployeeMatrix = {{"MST_EMPLOYEE.CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = ''", "F", "", "T"}};
            //--
            string[,] strEmployeeHideMatrix = {{"MST_EMPLOYEE.INACTIVE_FLAG = 0", "F", "", "T"},
                                               {"MST_EMPLOYEE.INACTIVE_FLAG = 1", "F", "Yes", "T"}};
            //
            string[,] strMatrix1 = {{"EmployeeID", "0", "", "Right", "", "F", ""},
						            {"Employee PAN", "100", "", "", "", "", "T"},
						            {"Employee Name", "470", "", "", "", "", "T"},
						            {"Category", "150", "", "", "", "", "T"},
						            {"Ref.No.", "150", "", "", "", "", "T"},
                                    {"Hide", "80", "", "", "", "", "T"}};            
            //-----------------------------------------------------------
            strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            strOrderBy = "MST_EMPLOYEE.EMPLOYEE_NAME, MST_COMPANY.COMPANY_NAME";
            strQuery = "SELECT MST_EMPLOYEE.EMPLOYEE_ID   AS EMPLOYEE_ID," +
                "              MST_EMPLOYEE.EMPLOYEE_PAN  AS EMPLOYEE_PAN," +
                "              MST_EMPLOYEE.EMPLOYEE_NAME AS EMPLOYEE_NAME," +
                "             " + cmnService.J_SQLDBFormat(strShowHelpEmployeeMatrix, J_SQLColFormat.Case_End) + " AS CATEGORY," +
                "              MST_EMPLOYEE.EMPLOYEE_REF  AS EMPLOYEE_REF," +
                "             " + cmnService.J_SQLDBFormat(strEmployeeHideMatrix, J_SQLColFormat.Case_End) + " AS EMPLOYEE_HIDE " +
                "       FROM   MST_EMPLOYEE," +
                "              MST_COMPANY " +
                "       WHERE  MST_EMPLOYEE.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                "       AND    MST_COMPANY.COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid   
            //
            //dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
            //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.CurrentRowIndex, 0]));
            dgvGrid_Click(sender, e);
        }
        #endregion 

        #region BtnAdd_Click

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCompanyName.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select the Company");
                    cmbCompanyName.Select();
                    return;
                }
                // --Added By Abhishek Dey On 01 / 11 / 2019--
                #region IS COMPANY ACCESSABLE FOR CLIENT
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                {
                    if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                    {
                        if (TdsMan.IsCompanyAccessible(Convert.ToInt64(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                        {
                            cmnService.J_UserMessage("You are not authorised to proceed.");
                            //BtnCancel.Select();
                            return;
                        }

                        //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

                    }
                }
                #endregion
                //-----------------------------------------
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;
                dgvGrid.Visible = false;
                //---------------------------------------------
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                //---------------------------------------------
                strCheckFields = "";
                //---------------------------------------------
                //-----------
                //
                cmbCompanyName.Enabled = false;
                //--
                txtEmployeePAN.Select();
                //---------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnEdit_Click

        private void BtnEdit_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbCompanyName.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select the Company");
                    cmbCompanyName.Select();
                    return;
                }
                //--
                if (dgvGrid.CurrentRow != null)
                {
                    lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
                    //-- Added By Abhishek Dey On 01/11/2019 --
                    #region IS COMPANY ACCESSABLE FOR CLIENT
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                    {
                        if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                        {
                            if (TdsMan.IsCompanyAccessible(Convert.ToInt64(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                            {
                                cmnService.J_UserMessage("You are not authorised to proceed.");
                                BtnCancel.Select();
                                return;
                            }

                            //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

                        }
                    }
                    #endregion
                    //-----------------------------------------
                    //Added by Indrajit on 18-02-2013
                    if (Check_Record(lngSearchId) == 0) return;
                    //--------------------------------------------------
                    ControlVisible(true);
                    ClearControls();
                    //--------------------------------------------------
                    //A particular ID wise retriving the data from database
                    if (ShowRecord(lngSearchId) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId);
                    }
                    //
                    cmbCompanyName.Enabled = false;
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    dgvGrid.Visible = false;
                    //
                    chkHideEmployee.Visible = true;
                    //--------------------------------------------------
                    strCheckFields = "";
                    //--------------------------------------------------
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                }
            }
            catch (Exception err_handler)
            {
                ControlVisible(false);
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnSave_Click

        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            Insert_Update_Delete_Data();
        }

        #endregion

        #region BtnCancel_Click

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
                dgvGrid.Visible = true;
                //-------------------------------------------
                //DisableControls();
                //-------------------------------------------
                ControlVisible(false);
                ClearControls();					//Clear all the Controls
                //-------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //
                cmbCompanyName.Enabled = true;
                //-------------------------------------------
                if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId) == false)
                    BtnAdd.Select();
                //
                chkHideEmployee.Visible = false;
                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnSort_Click
        private void BtnSort_Click(object sender, System.EventArgs e)
        {
            try
            {
                ////----------------------------------------------------------------------
                //lblSearchMode.Text = J_Mode.Sorting;
                ////----------------------------------------------------------------------
                //if (ValidateFields() == false) return;
                ////----------------------------------------------------------------------
                //grpSort.Visible = true;
                //grpSearch.Visible = false;
                ////----------------------------------------------------------------------
                //rbnSortSurveyDate.Checked = false;
                //rbnSortMemberName.Checked = false;
                //rbnSortAreaName.Checked = false;
                //rbnSortPoliceStaion.Checked = false;
                //rbnSortAsEntered.Checked = false;
                ////----------------------------------------------------------------------
                //if (strOrderBy == "TRN_SURVEY.SURVEY_DATE")
                //    rbnSortSurveyDate.Select();
                //else if (strOrderBy == "TRN_SURVEY.MEMBER_NAME")
                //    rbnSortMemberName.Select();
                //else if (strOrderBy == "MST_AREA.AREA_NAME")
                //    rbnSortAreaName.Select();
                //else if (strOrderBy == "TRN_SURVEY.POLICE_STATION")
                //    rbnSortPoliceStaion.Select();
                //else if (strOrderBy == "TRN_SURVEY.SURVEY_ID")
                //    rbnSortAsEntered.Select();
                ////----------------------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortOK_Click
        private void BtnSortOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-------------------------------------------
                //if (rbnSortSurveyDate.Checked == true)
                //    strOrderBy = "TRN_SURVEY.SURVEY_DATE";
                //else if (rbnSortMemberName.Checked == true)
                //    strOrderBy = "TRN_SURVEY.MEMBER_NAME";
                //else if (rbnSortAreaName.Checked == true)
                //    strOrderBy = "MST_AREA.AREA_NAME";
                //else if (rbnSortPoliceStaion.Checked == true)
                //    strOrderBy = "TRN_SURVEY.POLICE_STATION";
                //else if (rbnSortAsEntered.Checked == true)
                //    strOrderBy = "TRN_SURVEY.SURVEY_ID";
                ////-------------------------------------------
                //strCheckFields = "";
                //if (strCheckFields == "")
                //    strSQL = strQuery + "order by " + strOrderBy;
                //else
                //    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                ////-------------------------------------------
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                //if (dsetGridClone == null) return;
                ////-------------------------------------------
                //lblSearchMode.Text = J_Mode.General;
                //grpSort.Visible = false;
                ////-------------------------------------------
                //dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, "SURVEY_ID", lngSearchId);
                ////-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortOK_KeyPress
        private void BtnSortOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        #region BtnSortCancel_Click
        private void BtnSortCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                ////-------------------------------------------
                //lblSearchMode.Text = J_Mode.General;
                //grpSort.Visible = false;
                ////-------------------------------------------
                //if (strCheckFields == "")
                //    strSQL = strQuery + "order by " + strOrderBy;
                //else
                //    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                ////-------------------------------------------
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                //if (dsetGridClone == null) return;
                ////-------------------------------------------
                //dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, "SURVEY_ID", lngSearchId);
                ////-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortCancel_KeyPress
        private void BtnSortCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        #region BtnSearch_Click
        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbCompanyName.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select the Company");
                    cmbCompanyName.Select();
                    return;
                }
                //-------------------------------------------
                lblSearchMode.Text = J_Mode.Searching;
                //-------------------------------------------
                if (ValidateFields() == false) return;
                //-------------------------------------------
                grpSort.Visible = false;
                grpSearch.Visible = true;
                //-------------------------------------------
                txtPANSearch.Select();
                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_Click
        private void BtnSearchOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-------------------------------------------------------------------
                if (ValidateFields() == false) return;
                strCheckFields = "";
                //-------------------------------------------------------------------
                //--- Storing the Criteria Fiels & Values ---------------------------
                //-------------------------------------------------------------------
                //-- PAN NO 
                //-------------------------------------------------------------------
                if (txtPANSearch.Text.Trim() != "")
                    strCheckFields = "AND MST_EMPLOYEE.EMPLOYEE_PAN like '%" + cmnService.J_ReplaceQuote(txtPANSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- DEDUCTEE NAME
                //-------------------------------------------------------------------
                if (txtEmployeeNameSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + " AND MST_EMPLOYEE.EMPLOYEE_NAME like '%" + cmnService.J_ReplaceQuote(txtEmployeeNameSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- CATEGORY
                //-------------------------------------------------------------------
                if (cmbCategorySearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + " AND MST_EMPLOYEE.CATEGORY = '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbCategorySearch.Text,1)) + "' ";

                ////Added by Shrey Kejriwal on 17/08/2011
                ////-- COMPANY TAN
                ////-------------------------------------------------------------------
                //if (txtTANSearch.Text.Trim() != "")
                //    strCheckFields = strCheckFields + " AND MST_COMPANY.TAN_NO like '" + cmnService.J_ReplaceQuote(txtTANSearch.Text.Trim().ToUpper()) + "%' ";
                
                //----------------------------------------------------------------------
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                //----------------------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId) == false)
                {
                    txtPANSearch.Select();
                    return;
                }
                //----------------------------------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                //----------------------------------------------------------------------
                grpSearch.Visible = false;
                //----------------------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_KeyPress
        private void BtnSearchOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnSearchCancel_Click
        private void BtnSearchCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                //----------------------------------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                grpSearch.Visible = false;
                //----------------------------------------------------------------------
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                //----------------------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                //----------------------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchCancel_KeyPress
        private void BtnSearchCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnDelete_Click
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (cmbCompanyName.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Select the Company");
                cmbCompanyName.Select();
                return;
            }
            //Modified by Indrajit on 02-03-2013
            if (dgvGrid.CurrentRow != null)
            {
                lblMode.Text = J_Mode.Delete;
                Insert_Update_Delete_Data();
            }
            else
            {
                cmnService.J_UserMessage(J_Msg.DataNotFound);
                if (dsetGridClone == null) return;
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
            }
        }

        #endregion

        #region BtnRefresh_Click

        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbCompanyName.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select the Company");
                    cmbCompanyName.Select();
                    return;
                }
                //-----------------------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                dgvGrid.Visible = true;
                //-----------------------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                //-----------------------------------------------------------
                //DisableControls();
                //-----------------------------------------------------------
                ClearControls();
                //-----------------------------------------------------------
                strCheckFields = "";
                strSQL = strQuery + "order by " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-----------------------------------------------------------
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                //-----------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
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

        #region dgvGrid_Click

        private void dgvGrid_Click(object sender, EventArgs e)
        {
            if (dgvGrid.CurrentRow == null)
            {
                BtnAdd.Focus();
                return;
            }
            //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.CurrentRow, 0]));
            lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));

            //dgvGrid.Select(dgvGrid.CurrentRowIndex);
            //dgvGrid.Select();
            //dgvGrid.Focus();
        }

        #endregion

        #region dgvGrid_DoubleClick
        private void dgvGrid_DoubleClick(object sender, System.EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }
        #endregion

        #region dgvGrid_KeyDown
        private void dgvGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (dgvGrid.CurrentRow == null) return;
                //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.CurrentRowIndex, 0]));                
                lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
                if (e.KeyCode == Keys.Enter) BtnEdit_Click(sender, e);
                if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);

                strTempMode = lblMode.Text;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region dgvGrid_CurrentCellChanged
        private void dgvGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.CurrentRowIndex, 0]));
            
            lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
        }
        #endregion

        #region dgvGrid_MouseUp
        private void dgvGrid_MouseUp(object sender, MouseEventArgs e)
        {
            dgvGrid_Click(sender, e);
        }
        #endregion

        #region dgvGrid_MouseMove
        private void dgvGrid_MouseMove(object sender, MouseEventArgs e)
        {
           //cmnService.J_GridToolTip(dgvGrid, e.X, e.Y);
        }
        #endregion

        #region txtPAN_Leave
        private void txtPAN_Leave(object sender, EventArgs e)
        {
            if (txtEmployeePAN.Text.Trim() == "") txtEmployeePAN.Text = "PANNOTAVBL";
        }
        #endregion

        #region txtPAN_KeyPress
        private void txtPAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                txtEmployeeName.Select();
            else
                if (TdsMan.gTANNoPANNoValidation(txtEmployeePAN, e, T_TANPAN.PAN) == false)
                    e.Handled = true;
        }
        #endregion

        #region cmbCompanyName_SelectedIndexChanged
        //Commented by Shrey on 17/08/2011
        //private void cmbCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtCompanyTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_COMPANY WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex))));
        //}
        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region Searching_KeyPress
        private void Searching_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region Sorting_KeyPress
        private void Sorting_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSortOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        //Added by Indrajit on 12-02-2013
        #region tmrGridRefresh_Tick
        private void tmrGridRefresh_Tick(object sender, EventArgs e)
        {
            if (lblMode.Text == J_Mode.View)
            {
                //BtnRefresh_Click(sender, e);

                strCheckFields = "";
                strSQL = strQuery + "order by " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-----------------------------------------------------------
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);

            }
        }
        #endregion

        #region lnkPANVerification_LinkClicked
        private void lnkPANVerification_LinkClicked(object sender, EventArgs e )
        {
            TDSMAN.Classes.TDSMAN.T_GetPANforVerification = TdsMan.ValidatePAN(txtEmployeePAN.Text);
            TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES = cmnService.J_Left(cmnService.J_Right(cmbCompanyName.Text, 11), 10);
            //
            //this.Cursor = Cursors.WaitCursor;
            //-------------------------------------------------------
            //ADDED BY DHRUB ON 15/01/2014 FOR PAN VERICATION LINK
            //-------------------------------------------------------
            //TrnPANVerificationSummary objTrnPANVerificationSummary = new TrnPANVerificationSummary();
            //TrnPANVerificationSummaryTraces objTrnPANVerificationSummary = new TrnPANVerificationSummaryTraces();
            //TrnPANVerificationSummaryTraces_SINGLE_NEW objTrnPANVerificationSummary = new TrnPANVerificationSummaryTraces_SINGLE_NEW();
            TrnPANVerificationSummaryTraces_SINGLE_NEW_Log objTrnPANVerificationSummary = new TrnPANVerificationSummaryTraces_SINGLE_NEW_Log();
            objTrnPANVerificationSummary.ShowDialog();
            this.Refresh();
        }
        #endregion

        #region btnVerifyPAN_MouseMove
        private void btnVerifyPAN_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnVerifyPAN, "Verify PAN");
        }
        #endregion

        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            FormTrn.TrnTANSearch Tan = new TrnTANSearch("MstEmployee");
            Tan.ShowDialog();
            //--------------
            if (TDSMAN.Classes.TDSMAN.T_pTAN != "")
                cmbCompanyName.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #endregion 

        #region User Define Functions

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            ////-----------
            ////-- COMPANY
            ////-----------
            //strSQL = " SELECT COMPANY_ID," +
            //    "             COMPANY_NAME " +
            //    "      FROM   MST_COMPANY " +
            //    "      ORDER BY COMPANY_NAME ";
            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompanyName) == false) return;
            ////--
            //txtCompanyTAN.Text = "";
            txtEmployeePAN.Text = "PANNOTAVBL";
            txtEmployeeName.Text = "";
            txtDesignation.Text = "";
            txtEmployeeRefNo.Text = "";
            //--
            string[] strEmployeeCategory ={ T_EmployeeCategory.General, T_EmployeeCategory.Woman, T_EmployeeCategory.SeniorCitizen, T_EmployeeCategory.SuperSeniorCitizen };
            dmlService.J_PopulateComboBox(strEmployeeCategory, ref cmbEmployeeCategory, 1, J_ComboBoxSelectedIndex.YES);            
            //--------------------
            txtPANSearch.Text = "";
            txtEmployeeNameSearch.Text = "";
            txtEmail.Text = "";
            //--
            string[] strEmployeeCategorySearch ={ T_EmployeeCategory.General, T_EmployeeCategory.Woman, T_EmployeeCategory.SeniorCitizen, T_EmployeeCategory.SuperSeniorCitizen };
            dmlService.J_PopulateComboBox(strEmployeeCategorySearch, ref cmbCategorySearch);//, 1, J_ComboBoxSelectedIndex.YES);  
            //
            chkHideEmployee.Checked = false;                      
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            string strCompanyName = string.Empty;
            string strTanNo = string.Empty;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            try
            {
                string[,] strShowHelpEmployeeMatrix = {{"MST_EMPLOYEE.CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = ''", "F", "", "T"}};
                //
                strSQL = "SELECT  MST_EMPLOYEE.EMPLOYEE_ID   AS EMPLOYEE_ID," +
                    "             MST_COMPANY.COMPANY_ID     AS COMPANY_ID," +
                    "             MST_COMPANY.COMPANY_NAME   AS COMPANY_NAME," +
                    "             MST_COMPANY.TAN_NO         AS TAN_NO," +
                    "             MST_EMPLOYEE.EMPLOYEE_PAN  AS EMPLOYEE_PAN," +
                    "             MST_EMPLOYEE.EMPLOYEE_NAME AS EMPLOYEE_NAME," +
                    "             MST_EMPLOYEE.DESIGNATION   AS DESIGNATION," +
                    "             " + cmnService.J_SQLDBFormat(strShowHelpEmployeeMatrix, J_SQLColFormat.Case_End) + " AS CATEGORY, " +
                    "             MST_EMPLOYEE.EMPLOYEE_REF  AS EMPLOYEE_REF," +
                    "             MST_EMPLOYEE.INACTIVE_FLAG AS INACTIVE_FLAG," +
                    "             MST_EMPLOYEE.EMAIL         AS EMAIL " +
                    "     FROM    MST_EMPLOYEE," +
                    "             MST_COMPANY " +
                    "     WHERE   MST_EMPLOYEE.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                    "     AND     MST_EMPLOYEE.EMPLOYEE_ID = " + Id + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;

                    strCompanyName = Convert.ToString(drdShowRecord["COMPANY_NAME"]);

                    //Added by Shrey on 17/08/2011
                    strTanNo = Convert.ToString(drdShowRecord["TAN_NO"]);
                    //--
                    txtEmployeePAN.Text = Convert.ToString(drdShowRecord["EMPLOYEE_PAN"]);
                    txtEmployeeName.Text = Convert.ToString(drdShowRecord["EMPLOYEE_NAME"]);
                    txtEmployeeRefNo.Text = Convert.ToString(drdShowRecord["EMPLOYEE_REF"]);
                    txtDesignation.Text = Convert.ToString(drdShowRecord["DESIGNATION"]);
                    txtEmail.Text = Convert.ToString(drdShowRecord["EMAIL"]);
                    //--
                    //if (Convert.ToString(drdShowRecord["CATEGORY"]) == "W")
                    //    cmbEmployeeCategory.Text = T_EmployeeCategory.Woman;
                    //else if (Convert.ToString(drdShowRecord["CATEGORY"]) == "G")
                    //    cmbEmployeeCategory.Text = T_EmployeeCategory.General;
                    //else if (Convert.ToString(drdShowRecord["CATEGORY"]) == "S")
                    //    cmbEmployeeCategory.Text = T_EmployeeCategory.SeniorCitizen;
                    cmbEmployeeCategory.Text = Convert.ToString(drdShowRecord["CATEGORY"]);

                    intCompanyIndex = Convert.ToInt32(Convert.ToString(drdShowRecord["COMPANY_ID"]));
                    //
                    if (Convert.ToString(drdShowRecord["INACTIVE_FLAG"]) == "1")
                    {
                        chkHideEmployee.Visible = true;
                        chkHideEmployee.Checked = true;
                    }
                    else
                    {
                        chkHideEmployee.Visible = false;
                    }
                    //
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();

                    cmbCompanyName.Text = strCompanyName + " [" + strTanNo + "]";
                    cmbCompanyName.Select();

                    //drdShowRecord.Close();
                    //drdShowRecord.Dispose();
                    return true;
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                lngSearchId = 0;
                //-----------------------------------------------------------
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (lblSearchMode.Text == J_Mode.Sorting)
                {
                    //if (Convert.ToInt64(Convert.ToString(dgvGrid.CurrentRowIndex)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
                    //    return false;
                    //}
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    if (grpSearch.Visible == false)
                    {
                        if (dgvGrid.CurrentRow== null)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            if (dsetGridClone == null) return false;
                            dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtPANSearch.Text.Trim()          == ""  &&
                            txtEmployeeNameSearch.Text.Trim() == ""  &&
                            cmbCategorySearch.Text.Trim()  == "")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtPANSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    //-----------------------------------------------------------------------
                    //-- COMPANY NAME
                    //-----------------------------------------------------------------------
                    if (cmbCompanyName.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Company - Cannot be Blank");
                        cmbCompanyName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PAN
                    //-----------------------------------------------------------------------
                    if (txtEmployeePAN.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("PAN - Cannot be Blank");
                        txtEmployeePAN.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PAN FORMAT
                    //-----------------------------------------------------------------------
                    if (txtEmployeePAN.Text.Length != 10)
                    {
                        cmnService.J_UserMessage("PAN No. should be of 10 characters");
                        txtEmployeePAN.Select();
                        return false;
                    }
                    if (txtEmployeePAN.Text != "PANNOTAVBL")
                    {
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtEmployeePAN.Text, 5), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtEmployeePAN.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtEmployeePAN.Text, 5, 4), J_DataType.Numeric) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtEmployeePAN.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtEmployeePAN.Text, 1), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtEmployeePAN.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- EMPLOYEE NAME
                    //-----------------------------------------------------------------------
                    if (TdsMan.T_ReplaceSpecialCharacters(txtEmployeeName.Text.Trim(), T_Datatype.Text) == "")
                    {
                        cmnService.J_UserMessage("Employee Name - Cannot be Blank");
                        txtEmployeeName.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtEmployeeName.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Employee Name - '^' not allowed");
                        txtEmployeeName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- CATEGORY
                    //-----------------------------------------------------------------------
                    if (cmbEmployeeCategory.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Employee Category - Cannot be Blank");
                        cmbEmployeeCategory.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- EMPLOYEE NAME & PAN
                    //-----------------------------------------------------------------------
                    strSQL = "SELECT EMPLOYEE_ID " +
                        "     FROM   MST_EMPLOYEE " +
                        "     WHERE  EMPLOYEE_NAME ='" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "'" +
                        "     AND    EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "'" +
                        "     AND    COMPANY_ID    = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "";
                    if (lblMode.Text == J_Mode.Edit)
                        strSQL = strSQL + "AND EMPLOYEE_ID <> " + lngSearchId;
                    //lngDeducteeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //--
                    if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                    {
                        cmnService.J_UserMessage("The combination of [Employee Name & PAN] exists for the selected Company");
                        txtEmployeeName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- SAME PAN CHECK
                    //-----------------------------------------------------------------------
                    if (txtEmployeePAN.Text != "PANNOTAVBL")
                    {
                        strSQL = "SELECT EMPLOYEE_ID " +
                            "     FROM   MST_EMPLOYEE " +
                            "     WHERE  EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "'" +
                            "     AND    COMPANY_ID    = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "";
                        if (lblMode.Text == J_Mode.Edit)
                            strSQL = strSQL + "AND EMPLOYEE_ID <> " + lngSearchId;
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                        {
                            if (cmnService.J_UserMessage("Same PAN already exists in this Company - Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                //cmnService.J_UserMessage("The combination of [Employee Name & PAN] exists for the selected Company");
                                txtEmployeePAN.Select();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //-----------------------------------------------------------------------
                        //-- Employee serial no.
                        //-----------------------------------------------------------------------
                        if (TDSMAN.Classes.TDSMAN.T_AllotAutomaticRefNo == false)
                        {
                            if (txtEmployeeRefNo.Text.Trim() == "")
                            {
                                cmnService.J_UserMessage("Employee serial no. - Cannot be Blank");
                                txtEmployeeRefNo.Select();
                                return false;
                            }
                        }
                        //
                        if (txtEmployeeRefNo.Text.Trim() != "")
                        {
                            strSQL = "SELECT EMPLOYEE_ID " +
                                "     FROM   MST_EMPLOYEE " +
                                "     WHERE  EMPLOYEE_REF  ='" + cmnService.J_ReplaceQuote(txtEmployeeRefNo.Text) + "'";
                            //
                            if (lblMode.Text == J_Mode.Edit)
                                strSQL = strSQL + "AND EMPLOYEE_ID <> " + lngSearchId;
                            //--
                            if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                            {
                                cmnService.J_UserMessage("Employee serial no. - Already exists");
                                txtEmployeeRefNo.Select();
                                return false;
                            }
                            //--
                            if (txtEmployeeRefNo.Text.Length < 10)
                            {
                                cmnService.J_UserMessage("Deductee reference no. - Should be of 10 characters");
                                txtEmployeeRefNo.Select();
                                return false;
                            }
                        }
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtDesignation.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Designation - '^' not allowed");
                        txtDesignation.Select();
                        return false;
                    }                    
                    return true;
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {
                //--------------------------------------------
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        //*****  For Insert
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;
                        //-----------------------------------------------------------
                        if (cmnService.J_SaveConfirmationMessage(ref cmbCompanyName) == true) return;
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        strSQL = "INSERT INTO MST_EMPLOYEE(" +
                                "            EMPLOYEE_PAN," +
                                "            EMPLOYEE_NAME," +
                                "            GROUP_ID," +
                                "            COMPANY_ID," +
                                "            DESIGNATION," +
                                "            CATEGORY," +
                                "            EMPLOYEE_REF," +
                                "            EMAIL) " +
                                "     VALUES('" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(cmnService.J_ReplaceQuote(txtEmployeeName.Text.Trim())) + "'," +
                                "             " + TDSMAN.Classes.TDSMAN.T_pGroupId + "," +
                                "             " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "," +
                                "            '" + cmnService.J_ReplaceQuote(txtDesignation.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbEmployeeCategory.Text, 1)) + "', " +
                                "            '" + cmnService.J_ReplaceQuote(txtEmployeeRefNo.Text) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(txtEmail.Text.Trim()) + "' )";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            cmbCompanyName.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        lngSearchId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "MST_EMPLOYEE", "EMPLOYEE_ID");
                        if (lngSearchId == 0)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        //-----------------------------------------------------------
                        //Added by Indrajit on 11-02-2013
                        //-----------------------------------------------------------------------
                        //-- EMPLOYEE NAME
                        //-----------------------------------------------------------------------
                        strSQL = "SELECT COUNT(EMPLOYEE_ID) " +
                            "     FROM   MST_EMPLOYEE " +
                            "     WHERE  EMPLOYEE_NAME ='" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "'" +
                            "     AND    EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "'" +
                            "     AND    COMPANY_ID    = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " " +
                            "     AND    EMPLOYEE_ID  <> " + lngSearchId;
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        {
                            cmnService.J_UserMessage("Employee Name exists");
                            txtEmployeeName.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //End of add zone  ------------------------------------------

                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        //-----------------------------------------------------------
                        ClearControls();
                        //-----------------------------------------------------------
                        txtEmployeePAN.Select();
                        //-----------------------------------------------------------
                        break;
                    case J_Mode.Edit:
                        //*****  For Modify
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;
                        //-----------------------------------------------------------
                        if (cmnService.J_SaveConfirmationMessage(ref cmbEmployeeCategory) == true) return;
                        //-----------------------------------------------------------
                        //-----------------------------------------------------------
                        /*Added by Shrey Kejriwal on 17/08/2011 to check if the employee's company is changed 
                         and the same is used in the earlier company's transaction then user is not allowed to update*/

                        //--Checking Employee records in transaction table
                        strSQL = "SELECT COUNT(*) " +
                                 "FROM   TRN_BASIC_INFO, " +
                                 "       TRN_DEDUCTEE_DETAILS " +
                                 "WHERE  TRN_BASIC_INFO.BASIC_INFO_ID  = TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID " +
                                 "AND    TRN_BASIC_INFO.FORM_NO        = '" + T_FormNo.F24Q + "' " +
                                 "AND    TRN_BASIC_INFO.COMPANY_ID     <> " + Support.GetItemData(cmbCompanyName,cmbCompanyName.SelectedIndex) + " " +
                                 "AND    TRN_DEDUCTEE_DETAILS.PARTY_ID = " + lngSearchId + " ";

                        intCountRecords = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //Checking employee records in salary detail table

                        strSQL = "SELECT COUNT(*) " +
                                 "FROM   TRN_BASIC_INFO, " +
                                 "       TRN_SALARY_DETAILS " +
                                 "WHERE  TRN_BASIC_INFO.BASIC_INFO_ID   = TRN_SALARY_DETAILS.BASIC_INFO_ID " +
                                 "AND    TRN_BASIC_INFO.COMPANY_ID      <> " + Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex) + " " +
                                 "AND    TRN_SALARY_DETAILS.EMPLOYEE_ID = " + lngSearchId + " ";

                        intCountRecords = intCountRecords + Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                        if (intCountRecords > 0)
                        {
                            cmnService.J_UserMessage(" Employee's company cannot be updated.\n To update this  employee's company, delete all the transactions of this employee in other companies.");
                            return;
                        }
                        //Added by Indrajit on 11-02-2013
                        //if (Check_Record(lngSearchId) == 0) return;
                        //----------
                        int intHideEmployee = 0;
                        //
                        if (chkHideEmployee.Checked == true)
                        {
                            intHideEmployee = 1;
                        }                        
                        //-----------------------------------------------------------
                        
                        dmlService.J_BeginTransaction();

                        strSQL = "UPDATE MST_EMPLOYEE " +
                                 "SET    EMPLOYEE_PAN  = '" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text.Trim()) + "'," +
                                 "       EMPLOYEE_NAME = '" + cmnService.J_ReplaceQuote(cmnService.J_ReplaceQuote(txtEmployeeName.Text.Trim())) + "'," +
                                 "       COMPANY_ID    =  " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "," +
                                 "       DESIGNATION   = '" + cmnService.J_ReplaceQuote(txtDesignation.Text.Trim()) + "'," +
                                 "       CATEGORY      = '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbEmployeeCategory.Text, 1)) + "', " +
                                 "       EMPLOYEE_REF  = '" + cmnService.J_ReplaceQuote(txtEmployeeRefNo.Text) + "', " +
                                 "       INACTIVE_FLAG = " + intHideEmployee + "," +
                                 "       EMAIL         = '" + cmnService.J_ReplaceQuote(txtEmail.Text.Trim()) + "' " +
                                 "WHERE  EMPLOYEE_ID   =  " + lngSearchId + "";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            cmbCompanyName.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        //-- UPDATE THE TRANSACTION
                        //..........................................................
                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.EditModeSave);
                        //-----------------------------------------------------------
                        ClearControls();
                        //-----------------------------------------------------------
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        //-----------------------------------------------------------
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        dgvGrid.Visible = true;
                        //-----------------------------------------------------------
                        //
                        chkHideEmployee.Visible = false;
                        //DisableControls();
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //
                        cmbCompanyName.Enabled = true;
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId);
                        break;
                    case J_Mode.Delete:
                        //-----------------------------------------------------------
                        //Added by Indrajit on 11-02-2013
                        //if (Check_Record(lngSearchId) == 0) return;
                        //----------
                        //-- Added By Abhishek Dey On 01/11/2019 --
                        #region IS COMPANY ACCESSABLE FOR CLIENT
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                            {
                                if (TdsMan.IsCompanyAccessible(Convert.ToInt64(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                                {
                                    cmnService.J_UserMessage("You are not authorised to delete");
                                    //BtnCancel.Select();
                                    return;
                                }

                                //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

                            }
                        }
                        #endregion
                        //-----------------------------------------
                        //-----------------------------------------------------------                        
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        //-- CHECK THE TRANSACTION
                        //-----------------------------------------------------------------------
                        //-- TRN_DEDUCTEE_DETAILS
                        //-----------------------------------------------------------------------
                        //strSQL = "SELECT PARTY_ID " +
                        //    "     FROM   TRN_DEDUCTEE_DETAILS " +
                        //    "     WHERE  PARTY_ID = " + lngSearchId + " ";
                        strSQL = "SELECT PARTY_ID " +
                            "     FROM   TRN_DEDUCTEE_DETAILS," +
                            "            TRN_BASIC_INFO " +
                            "     WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID " +
                            "     AND    TRN_BASIC_INFO.FORM_NO             = '" + T_FormNo.F24Q + "' " +
                            "     AND    TRN_DEDUCTEE_DETAILS.PARTY_ID      = " + lngSearchId + " ";
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        {
                            cmnService.J_UserMessage("The Employee cannot be deleted");
                            BtnDelete.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //-------------------------------------------------
                        //-- TRN_SALARY_DETAILS
                        strSQL = "SELECT EMPLOYEE_ID " +
                            "     FROM   TRN_SALARY_DETAILS " +
                            "     WHERE  EMPLOYEE_ID = " + lngSearchId + " ";
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        {
                            cmnService.J_UserMessage("The Employee cannot be deleted");
                            BtnDelete.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //..........................................................
                        if (cmnService.J_UserMessage("Proceed Deletion?", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            lblMode.Text = J_Mode.View;
                            return;
                        }
                        //-----------------------------------------------------------
                        strSQL = "DELETE FROM MST_EMPLOYEE WHERE EMPLOYEE_ID =  " + lngSearchId + "";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            lblMode.Text = J_Mode.View;
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.DeleteMode);
                        //-----------------------------------------------------------
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        //-----------------------------------------------------------
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        dgvGrid.Visible = true;
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId);
                        break;
                }
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion   

        //Added by Indrajit on 11-02-2013
        #region Check_Record
        private long Check_Record(long lngSrchId)
        {
            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "MST_EMPLOYEE", "EMPLOYEE_ID", lngSrchId) == true) return lngSrchId;

            cmnService.J_UserMessage("Record has been deleted.");
            lngSrchId = 0;
            //-------------------------------------------
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
            dgvGrid.Visible = true;
            //-------------------------------------------
            ControlVisible(false);
            ClearControls();					//Clear all the Controls
            //-------------------------------------------
            strSQL = strQuery + "order by " + strOrderBy;
            //-------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
            if (dsetGridClone == null) return 0;
            //-------------------------------------------
            BtnAdd.Select();
            //-------------------------------------------                            
            return 0;

        }
        #endregion

        private void cmbEmployeeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0021", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region BtnPrint_Click
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cmbCompanyName.SelectedIndex <= 0)
            //    {
            //        cmnService.J_UserMessage("Select the Company");
            //        cmbCompanyName.Select();
            //        return;
            //    }
            //    //--
            //    if (dgvGrid.CurrentRow == null)
            //    {
            //        cmnService.J_UserMessage("No Employee exists");
            //        cmbCompanyName.Select();
            //        return;
            //    }
            //    //--
            //    #region IS COMPANY ACCESSABLE FOR CLIENT
            //        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
            //    {
            //        if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
            //        {
            //            if (TdsMan.IsCompanyAccessible(Convert.ToInt64(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
            //            {
            //                cmnService.J_UserMessage("You are not authorised to proceed.");
            //                //BtnCancel.Select();
            //                return;
            //            }

            //            //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

            //        }
            //    }
            //    #endregion
            //    //--
            //    string strPath = "", strFileName = "";
            //    // Create a new instance of FolderBrowserDialog.
            //    FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
            //    // A new folder button will display in FolderBrowserDialog.
            //    folderBrowserDlg.ShowNewFolderButton = true;
            //    //Show FolderBrowserDialog
            //    DialogResult dlgResult = folderBrowserDlg.ShowDialog();
            //    if (dlgResult.Equals(DialogResult.OK))
            //    {
            //        //Show selected folder path in textbox1.
            //        strPath = folderBrowserDlg.SelectedPath;
            //        //Browsing start from root folder.
            //        Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
            //    }
            //    else
            //        return;
            //    //--
            //    string[,] strShowHelpEmployeeMatrix = {{"MST_EMPLOYEE.CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
            //                                           {"MST_EMPLOYEE.CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
            //                                           {"MST_EMPLOYEE.CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
            //                                           {"MST_EMPLOYEE.CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
            //                                           {"MST_EMPLOYEE.CATEGORY = ''", "F", "", "T"}};
            //    //
            //    strSQL = "SELECT MST_EMPLOYEE.EMPLOYEE_ID    AS EMPLOYEE_ID," +
            //    "              MST_EMPLOYEE.EMPLOYEE_PAN     AS EMPLOYEE_PAN," +
            //    "              MST_EMPLOYEE.EMPLOYEE_NAME    AS EMPLOYEE_NAME," +
            //        "             MST_EMPLOYEE.DESIGNATION   AS DESIGNATION," +
            //        "             MST_EMPLOYEE.EMPLOYEE_REF  AS EMPLOYEE_REF," +
            //        "             MST_EMPLOYEE.EMAIL         AS EMAIL," +
            //    "             " + cmnService.J_SQLDBFormat(strShowHelpEmployeeMatrix, J_SQLColFormat.Case_End) + " AS CATEGORY " +
            //    "       FROM   MST_EMPLOYEE," +
            //    "              MST_COMPANY " +
            //    "       WHERE  MST_EMPLOYEE.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
            //    "       AND    MST_COMPANY.COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " ";
            //    //-----------------------------------------------------------
            //    strSQL = strSQL + "ORDER BY MST_EMPLOYEE.EMPLOYEE_NAME";
            //    //--
            //    strFileName = "XL_EMPLOYEE_LIST_" + cmnService.J_Left(cmnService.J_Right(cmbCompanyName.Text,11),10) + "_"  + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".XLSX";
            //    //--
            //    this.Cursor = Cursors.WaitCursor;
            //    //--
            //    if (CREATE_EXCEL_FILE(Path.Combine(strPath, strFileName)) == false)
            //    {
            //        this.Cursor = Cursors.Default;
            //        cmnService.J_UserMessage("Some error occurred");
            //        return;
            //    }
            //    else
            //    {
            //        // EMPLOYEE DETAILS
            //        if (CREATE_NEW_WORKSHEET(Path.Combine(strPath, strFileName), "EMPLOYEE LIST") == false) return;
            //        //
            //        if (WRITE_RECEIPT_NO_REG_WORKSHEET(Path.Combine(strPath, strFileName), "EMPLOYEE LIST", strSQL) == false) return;
            //    }
            //    this.Cursor = Cursors.Default;
            //    //
            //    cmnService.J_UserMessage("Data Exported");
            //    System.Diagnostics.Process.Start(Path.Combine(strPath, strFileName));
            //}
            //catch (Exception ERR)
            //{
            //    this.Cursor = Cursors.Default;
            //    cmnService.J_UserMessage(ERR.Message);
            //}
        }
        #endregion



        #region CREATE EXCEL FILE
        // SOURCE PATH : http://csharp.net-informations.com/excel/csharp-create-excel.htm
        private bool CREATE_EXCEL_FILE(string ExcelFilePath)
        {
            try
            {
                //MessageBox.Show("5");
                //--
                //if (rbnCSVOption.Checked == true)
                //{
                //    if (Directory.Exists(ExcelFilePath) == false)
                //        Directory.CreateDirectory(ExcelFilePath);
                //    return true; //-- 2019/01/22
                //}
                //MessageBox.Show("5.0.1.1");
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                //Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                //--
                //MessageBox.Show("5.0.1.2");
                //string strExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "." + cmbFileType.Text;
                //--


                //xlApp = new Excel.ApplicationClass();
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);

                //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                //xlWorkSheet.Cells[1, 1] = "http://csharp.net-informations.com";
                //--
                //------------------------------------
                if (Path.GetExtension(ExcelFilePath).ToUpper() == ".XLS")
                    xlWorkBook.SaveAs(ExcelFilePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                else if (Path.GetExtension(ExcelFilePath).ToUpper() == ".XLSX")
                    xlWorkBook.SaveAs(ExcelFilePath, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                //------------------------------------
                //MessageBox.Show("5.0.1.3");
                xlWorkBook.Close(true, misValue, misValue);
                //MessageBox.Show("5.0.1.4");
                xlApp.Quit();
                //MessageBox.Show("5.0.1.5");

                //ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                //MessageBox.Show("5.0.1.6");
                ReleaseObject(xlApp);
                //MessageBox.Show("5.0.1.7");
                //--
                return true;
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
                //cmnService.J_UserMessage(ERR.Message);
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath, string WorksheetName)
        {
            try
            {
                //
                //if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
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

        #region WRITE RECEIPT NO. WORKSHEET
        private bool WRITE_RECEIPT_NO_REG_WORKSHEET(string ExcelFilePath, string SheetName, string SQL)
        {
            try
            {
                //if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
                //--
                IDataReader drdGetSheetRecord = null;
                //--
                if (KILL_EXCEL() == false)
                    return false;
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
                wsnew.get_Range("A1", m).Value2 = "NAME";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 20;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "PAN";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 20;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("C1", m).Value2 = "Category";
                wsnew.get_Range("C1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C1", m).Borders.Value = true;
                wsnew.get_Range("C1", m).ColumnWidth = 20;
                wsnew.get_Range("C1", m).WrapText = true;
                wsnew.get_Range("C1", m).Font.Name = "Arial";
                wsnew.get_Range("C1", m).Font.Bold = true;
                wsnew.get_Range("C1", m).Font.Size = 10;
                wsnew.get_Range("C1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("C1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("D1", m).Value2 = "Reference No.";
                wsnew.get_Range("D1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("D1", m).Borders.Value = true;
                wsnew.get_Range("D1", m).ColumnWidth = 20;
                wsnew.get_Range("D1", m).WrapText = true;
                wsnew.get_Range("D1", m).Font.Name = "Arial";
                wsnew.get_Range("D1", m).Font.Bold = true;
                wsnew.get_Range("D1", m).Font.Size = 10;
                wsnew.get_Range("D1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("D1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("E1", m).Value2 = "Designation";
                wsnew.get_Range("E1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("E1", m).Borders.Value = true;
                wsnew.get_Range("E1", m).ColumnWidth = 20;
                wsnew.get_Range("E1", m).WrapText = true;
                wsnew.get_Range("E1", m).Font.Name = "Arial";
                wsnew.get_Range("E1", m).Font.Bold = true;
                wsnew.get_Range("E1", m).Font.Size = 10;
                wsnew.get_Range("E1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("E1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("F1", m).Value2 = "Email";
                wsnew.get_Range("F1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("F1", m).Borders.Value = true;
                wsnew.get_Range("F1", m).ColumnWidth = 20;
                wsnew.get_Range("F1", m).WrapText = true;
                wsnew.get_Range("F1", m).Font.Name = "Arial";
                wsnew.get_Range("F1", m).Font.Bold = true;
                wsnew.get_Range("F1", m).Font.Size = 10;
                wsnew.get_Range("F1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("F1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                ////@@@@@@@@@@@@@@@
                //strSQL = "SELECT TRN_BASIC_INFO.BASIC_INFO_ID," +
                //    "            " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.RECEIPT_NO", J_SQLColFormat.ConvertToString) + "        AS RECEIPT_NO," +
                //    "            " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DATE_OF_FILING," +
                //    "            " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.PRN_NO", J_SQLColFormat.ConvertToString) + "            AS TOKEN_NO " +
                //    "     FROM   TRN_BASIC_INFO " +
                //    "     WHERE  TRN_BASIC_INFO.BASIC_INFO_ID = " + BasicInfo + " ";
                //if (FormNo == T_FormNo.F24Q)
                //    strSQL = strSQL + "AND SECTION_NO <> '' ";
                //strSQL = strSQL + "ORDER BY SECTION_ID";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(SQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["EMPLOYEE_NAME"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["EMPLOYEE_PAN"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("C" + lngSheetRow, m).Value2 = drdGetSheetRecord["CATEGORY"].ToString();
                    wsnew.get_Range("C" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("D" + lngSheetRow, m).Value2 = drdGetSheetRecord["EMPLOYEE_REF"].ToString();
                    wsnew.get_Range("D" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("E" + lngSheetRow, m).Value2 = drdGetSheetRecord["DESIGNATION"].ToString();
                    wsnew.get_Range("E" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("F" + lngSheetRow, m).Value2 = drdGetSheetRecord["EMAIL"].ToString();
                    wsnew.get_Range("F" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //

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
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
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

        #region KILL EXCEL
        private bool KILL_EXCEL()
        {
            //try
            //{
            //    //--
            //    foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            //    {
            //        if (process.MainModule.ModuleName.ToUpper().Equals("EXCEL.EXE"))
            //        {
            //            process.Kill();
            //            //process.Close();
            //            //process.Dispose();
            //            break;
            //        }
            //    }
            return true;
            //}
            //catch
            //{
            //    this.Cursor = Cursors.Default;
            //    //
            //    cmnService.J_UserMessage("Excel file creation failed");
            //    //
            //    return false;
            //}
        }
        #endregion


        #region BtnPrint_MouseClick
        private void BtnPrint_MouseClick(object sender, MouseEventArgs e)
        {
            //--
            if (e.Button == MouseButtons.Left)
                ctxtmnuExport.Show(BtnPrint, new Point(e.X, e.Y));
        }
        #endregion

        #region TlStrpMnuExcel_Click
        private void TlStrpMnuExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCompanyName.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select the Company");
                    cmbCompanyName.Select();
                    return;
                }
                //--
                if (dgvGrid.CurrentRow == null)
                {
                    cmnService.J_UserMessage("No Employee exists");
                    cmbCompanyName.Select();
                    return;
                }
                //--
                #region IS COMPANY ACCESSABLE FOR CLIENT
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                {
                    if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                    {
                        if (TdsMan.IsCompanyAccessible(Convert.ToInt64(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                        {
                            cmnService.J_UserMessage("You are not authorised to proceed.");
                            //BtnCancel.Select();
                            return;
                        }

                        //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

                    }
                }
                #endregion
                //--
                string strPath = "", strFileName = "";
                // Create a new instance of FolderBrowserDialog.
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                // A new folder button will display in FolderBrowserDialog.
                folderBrowserDlg.ShowNewFolderButton = true;
                //Show FolderBrowserDialog
                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                if (dlgResult.Equals(DialogResult.OK))
                {
                    //Show selected folder path in textbox1.
                    strPath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                else
                    return;
                //--
                string[,] strShowHelpEmployeeMatrix = {{"MST_EMPLOYEE.CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = ''", "F", "", "T"}};
                //
                strSQL = "SELECT MST_EMPLOYEE.EMPLOYEE_ID    AS EMPLOYEE_ID," +
                "              MST_EMPLOYEE.EMPLOYEE_PAN     AS EMPLOYEE_PAN," +
                "              MST_EMPLOYEE.EMPLOYEE_NAME    AS EMPLOYEE_NAME," +
                    "             MST_EMPLOYEE.DESIGNATION   AS DESIGNATION," +
                    "             MST_EMPLOYEE.EMPLOYEE_REF  AS EMPLOYEE_REF," +
                    "             MST_EMPLOYEE.EMAIL         AS EMAIL," +
                "             " + cmnService.J_SQLDBFormat(strShowHelpEmployeeMatrix, J_SQLColFormat.Case_End) + " AS CATEGORY " +
                "       FROM   MST_EMPLOYEE," +
                "              MST_COMPANY " +
                "       WHERE  MST_EMPLOYEE.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                "       AND    MST_COMPANY.COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " ";
                //-----------------------------------------------------------
                strSQL = strSQL + "ORDER BY MST_EMPLOYEE.EMPLOYEE_NAME";
                //--
                strFileName = "XL_EMPLOYEE_LIST_" + cmnService.J_Left(cmnService.J_Right(cmbCompanyName.Text, 11), 10) + "_" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".XLSX";
                //--
                this.Cursor = Cursors.WaitCursor;
                //--
                if (CREATE_EXCEL_FILE(Path.Combine(strPath, strFileName)) == false)
                {
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("Some error occurred");
                    return;
                }
                else
                {
                    // EMPLOYEE DETAILS
                    if (CREATE_NEW_WORKSHEET(Path.Combine(strPath, strFileName), "EMPLOYEE LIST") == false) return;
                    //
                    if (WRITE_RECEIPT_NO_REG_WORKSHEET(Path.Combine(strPath, strFileName), "EMPLOYEE LIST", strSQL) == false) return;
                }
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Data Exported");
                System.Diagnostics.Process.Start(Path.Combine(strPath, strFileName));
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(ERR.Message);
            }
        }
        #endregion

        #region TlStrpMnuCSV_Click
        private void TlStrpMnuCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCompanyName.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select the Company");
                    cmbCompanyName.Select();
                    return;
                }
                //--
                if (dgvGrid.CurrentRow == null)
                {
                    cmnService.J_UserMessage("No Employee exists");
                    cmbCompanyName.Select();
                    return;
                }
                //--
                #region IS COMPANY ACCESSABLE FOR CLIENT
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                {
                    if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                    {
                        if (TdsMan.IsCompanyAccessible(Convert.ToInt64(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                        {
                            cmnService.J_UserMessage("You are not authorised to proceed.");
                            //BtnCancel.Select();
                            return;
                        }

                        //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

                    }
                }
                #endregion
                //--
                string strPath = "", strFileName = "";
                // Create a new instance of FolderBrowserDialog.
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                // A new folder button will display in FolderBrowserDialog.
                folderBrowserDlg.ShowNewFolderButton = true;
                //Show FolderBrowserDialog
                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                if (dlgResult.Equals(DialogResult.OK))
                {
                    //Show selected folder path in textbox1.
                    strPath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                else
                    return;
                //--
                string[,] strShowHelpEmployeeMatrix = {{"MST_EMPLOYEE.CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = ''", "F", "", "T"}};
                //
                strSQL = "SELECT MST_EMPLOYEE.EMPLOYEE_ID    AS EMPLOYEE_ID," +
                "              MST_EMPLOYEE.EMPLOYEE_PAN     AS EMPLOYEE_PAN," +
                "              MST_EMPLOYEE.EMPLOYEE_NAME    AS EMPLOYEE_NAME," +
                    "             MST_EMPLOYEE.DESIGNATION   AS DESIGNATION," +
                    "             MST_EMPLOYEE.EMPLOYEE_REF  AS EMPLOYEE_REF," +
                    "             MST_EMPLOYEE.EMAIL         AS EMAIL," +
                "             " + cmnService.J_SQLDBFormat(strShowHelpEmployeeMatrix, J_SQLColFormat.Case_End) + " AS CATEGORY " +
                "       FROM   MST_EMPLOYEE," +
                "              MST_COMPANY " +
                "       WHERE  MST_EMPLOYEE.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                "       AND    MST_COMPANY.COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " ";
                //-----------------------------------------------------------
                strSQL = strSQL + "ORDER BY MST_EMPLOYEE.EMPLOYEE_NAME";
                //--
                strFileName = "CSV_EMPLOYEE_LIST_" + cmnService.J_Left(cmnService.J_Right(cmbCompanyName.Text, 11), 10) + "_" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".CSV";
                //--
                this.Cursor = Cursors.WaitCursor;
                //--
                ExportToCSV(strSQL, Path.Combine(strPath, strFileName));
                //if (CREATE_EXCEL_FILE(Path.Combine(strPath, strFileName)) == false)
                //{
                //    this.Cursor = Cursors.Default;
                //    cmnService.J_UserMessage("Some error occurred");
                //    return;
                //}
                //else
                //{
                //    // EMPLOYEE DETAILS
                //    if (CREATE_NEW_WORKSHEET(Path.Combine(strPath, strFileName), "EMPLOYEE LIST") == false) return;
                //    //
                //    if (WRITE_RECEIPT_NO_REG_WORKSHEET(Path.Combine(strPath, strFileName), "EMPLOYEE LIST", strSQL) == false) return;
                //}
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Data Exported");
                System.Diagnostics.Process.Start(Path.Combine(strPath, strFileName));
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(ERR.Message);
            }
        }
        #endregion

        #region ExportToCSV
        protected void ExportToCSV(string strSQL, string CSVFile)
        {
            //-- https://www.aspsnippets.com/Articles/Export-DataSet-or-DataTable-to-Word-Excel-PDF-and-CSV-Formats.aspx
            //Get the data from database into datatable
            //string strQuery = "select CustomerID, ContactName, City, PostalCode" +
            //     " from customers";
            //SqlCommand cmd = new SqlCommand(strQuery);
            //DataTable dt = GetData(cmd);
            System.Data.DataTable dt = dmlService.J_ExecSqlReturnDataTable(strSQL);
            //
            StreamWriter StreamWriter = cmnService.J_ReturnStreamWriter(CSVFile);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition",
            //    "attachment;filename=DataTable.csv");
            //Response.Charset = "";
            //Response.ContentType = "application/text";
            StringBuilder sb = new StringBuilder();
            //--
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator
                sb.Append(dt.Columns[k].ColumnName + ',');
            }
            //append new line
            sb.Append("\r\n");
            //--
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    //sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                    if (dt.Rows[i][k].ToString().Contains(",") == true)
                        sb.Append('"' + dt.Rows[i][k].ToString() + '"' + ',');
                    else
                        sb.Append(dt.Rows[i][k].ToString() + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            //Response.Output.Write(sb.ToString());
            cmnService.J_WriteLine(ref StreamWriter, sb.ToString());
            //
            StreamWriter.Flush();
            StreamWriter.Close();
            //Response.Flush();
            //Response.End();
        }
        #endregion
    }
}

