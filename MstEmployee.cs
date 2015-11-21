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

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormTrn;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion


namespace TDSMAN.FormMst
{
    public partial class MstEmployee : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public MstEmployee()
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

        #region MstEmployee_Load

        private void MstEmployee_Load(object sender, EventArgs e)
        {
            try
            {
                //-----------------------------------------------------------
                GC.Collect();
                //
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                //-----------------------------------------------------------
                //DisableControls();
                //-----------------------------------------------------------
                ViewGrid.Height = 518;
                //
                ControlVisible(false);
                ClearControls();
                //-----------------------------------------------------------
                //-- COMPANY
                //-----------
                if (dmlService.J_ReturnNoOfRows("MST_COMPANY") > 0)
                {
                    strSQL = " SELECT COMPANY_ID," +
                        "             COMPANY_NAME & ' [' & TAN_NO & ']' " +
                        "      FROM   MST_COMPANY " +
                        "      WHERE  INACTIVE_FLAG = 0 " +
                        "      ORDER BY COMPANY_NAME ";
                    if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompanyName, 1, J_ComboBoxSelectedIndex.YES) == false) return;
                }
                //-----------------------------------------------------------
                lblTitle.Text = "Employee Master";
                //-----------------------------------------------------------
                ViewGrid_Click(sender, e);
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
            string[,] strMatrix1 = {{"EmployeeID", "0", "", "Right", "", "", ""},
						            {"Employee PAN", "100", "S", "", "", "", ""},
						            {"Employee Name", "440", "S", "", "", "", ""},
						            {"Category", "200", "S", "", "", "", ""},
						            {"Ref.No.", "200", "S", "", "", "", ""}};            
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
                "              MST_EMPLOYEE.EMPLOYEE_REF  AS EMPLOYEE_REF " +
                "       FROM   MST_EMPLOYEE," +
                "              MST_COMPANY " +
                "       WHERE  MST_EMPLOYEE.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                "       AND    MST_COMPANY.COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid   
            //
            //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
            //lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
            ViewGrid_Click(sender, e);
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
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;
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
                if (ViewGrid.CurrentRowIndex >= 0)
                {
                    //Added by Indrajit on 18-02-2013
                    if (Check_Record(lngSearchId) == 0) return;
                    //--------------------------------------------------
                    ControlVisible(true);
                    ClearControls();
                    //--------------------------------------------------
                    //A particular ID wise retriving the data from database
                    if (ShowRecord(Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]))) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
                    }
                    //
                    cmbCompanyName.Enabled = false;
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    //--------------------------------------------------
                    strCheckFields = "";
                    //--------------------------------------------------
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
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
                //-------------------------------------------
                //DisableControls();
                //-------------------------------------------
                ControlVisible(false);
                ClearControls();					//Clear all the Controls
                //-------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //
                cmbCompanyName.Enabled = true;
                //-------------------------------------------
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId) == false)
                    BtnAdd.Select();
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
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                //if (dsetGridClone == null) return;
                ////-------------------------------------------
                //lblSearchMode.Text = J_Mode.General;
                //grpSort.Visible = false;
                ////-------------------------------------------
                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "SURVEY_ID", lngSearchId);
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
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                //if (dsetGridClone == null) return;
                ////-------------------------------------------
                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "SURVEY_ID", lngSearchId);
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
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId) == false)
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
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
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
            if (ViewGrid.CurrentRowIndex >= 0)
            {
                lblMode.Text = J_Mode.Delete;
                Insert_Update_Delete_Data();
            }
            else
            {
                cmnService.J_UserMessage(J_Msg.DataNotFound);
                if (dsetGridClone == null) return;
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
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
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-----------------------------------------------------------
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
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

        #region ViewGrid_Click

        private void ViewGrid_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
            {
                BtnAdd.Focus();
                return;
            }
            lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));

            ViewGrid.Select(ViewGrid.CurrentRowIndex);
            ViewGrid.Select();
            ViewGrid.Focus();
        }

        #endregion

        #region ViewGrid_DoubleClick
        private void ViewGrid_DoubleClick(object sender, System.EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }
        #endregion

        #region ViewGrid_KeyDown
        private void ViewGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (ViewGrid.CurrentRowIndex == -1) return;
                lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
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

        #region ViewGrid_CurrentCellChanged
        private void ViewGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
        }
        #endregion

        #region ViewGrid_MouseUp
        private void ViewGrid_MouseUp(object sender, MouseEventArgs e)
        {
            ViewGrid_Click(sender, e);
        }
        #endregion

        #region ViewGrid_MouseMove
        private void ViewGrid_MouseMove(object sender, MouseEventArgs e)
        {
           //cmnService.J_GridToolTip(ViewGrid, e.X, e.Y);
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
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-----------------------------------------------------------
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);

            }
        }
        #endregion

        #region lnkPANVerification_LinkClicked
        private void lnkPANVerification_LinkClicked(object sender, EventArgs e )
        {
            TDSMAN.Classes.TDSMAN.T_GetPANforVerification = TdsMan.ValidatePAN(txtEmployeePAN.Text);
            //
            //this.Cursor = Cursors.WaitCursor;
            //-------------------------------------------------------
            //ADDED BY DHRUB ON 15/01/2014 FOR PAN VERICATION LINK
            //-------------------------------------------------------
            TrnPANVerificationSummary objTrnPANVerificationSummary = new TrnPANVerificationSummary();
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
            //--
            string[] strEmployeeCategorySearch ={ T_EmployeeCategory.General, T_EmployeeCategory.Woman, T_EmployeeCategory.SeniorCitizen, T_EmployeeCategory.SuperSeniorCitizen };
            dmlService.J_PopulateComboBox(strEmployeeCategorySearch, ref cmbCategorySearch);//, 1, J_ComboBoxSelectedIndex.YES);                        
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
                    "             MST_EMPLOYEE.EMPLOYEE_REF  AS EMPLOYEE_REF " +
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
                    txtDesignation.Text  = Convert.ToString(drdShowRecord["DESIGNATION"]);
                    //--
                    //if (Convert.ToString(drdShowRecord["CATEGORY"]) == "W")
                    //    cmbEmployeeCategory.Text = T_EmployeeCategory.Woman;
                    //else if (Convert.ToString(drdShowRecord["CATEGORY"]) == "G")
                    //    cmbEmployeeCategory.Text = T_EmployeeCategory.General;
                    //else if (Convert.ToString(drdShowRecord["CATEGORY"]) == "S")
                    //    cmbEmployeeCategory.Text = T_EmployeeCategory.SeniorCitizen;
                    cmbEmployeeCategory.Text = Convert.ToString(drdShowRecord["CATEGORY"]);

                    intCompanyIndex = Convert.ToInt32(Convert.ToString(drdShowRecord["COMPANY_ID"]));

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
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
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
                    //if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
                    //    return false;
                    //}
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    if (grpSearch.Visible == false)
                    {
                        if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            if (dsetGridClone == null) return false;
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
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
                                "            EMPLOYEE_REF) " +
                                "     VALUES('" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(cmnService.J_ReplaceQuote(txtEmployeeName.Text.Trim())) + "'," +
                                "             " + TDSMAN.Classes.TDSMAN.T_pGroupId + "," +
                                "             " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "," +
                                "            '" + cmnService.J_ReplaceQuote(txtDesignation.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbEmployeeCategory.Text, 1)) + "', " +
                                "            '" + cmnService.J_ReplaceQuote(txtEmployeeRefNo.Text) + "' )";
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
                        
                        dmlService.J_BeginTransaction();

                        strSQL = "UPDATE MST_EMPLOYEE " +
                                 "SET    EMPLOYEE_PAN  = '" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text.Trim()) + "'," +
                                 "       EMPLOYEE_NAME = '" + cmnService.J_ReplaceQuote(cmnService.J_ReplaceQuote(txtEmployeeName.Text.Trim())) + "'," +
                                 "       COMPANY_ID    =  " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "," +
                                 "       DESIGNATION   = '" + cmnService.J_ReplaceQuote(txtDesignation.Text.Trim()) + "'," +
                                 "       CATEGORY      = '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbEmployeeCategory.Text, 1)) + "', " +
                                 "       EMPLOYEE_REF  = '" + cmnService.J_ReplaceQuote(txtEmployeeRefNo.Text) + "' " +
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
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        //-----------------------------------------------------------
                        //DisableControls();
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //
                        cmbCompanyName.Enabled = true;
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
                        break;
                    case J_Mode.Delete:
                        //-----------------------------------------------------------
                        //Added by Indrajit on 11-02-2013
                        //if (Check_Record(lngSearchId) == 0) return;
                        //----------
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
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
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
            //-------------------------------------------
            ControlVisible(false);
            ClearControls();					//Clear all the Controls
            //-------------------------------------------
            strSQL = strQuery + "order by " + strOrderBy;
            //-------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
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

    }
}

