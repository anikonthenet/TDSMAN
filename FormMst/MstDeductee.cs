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
    public partial class MstDeductee : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public MstDeductee()
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
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        ToolTip tllTip = new ToolTip();

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

        #region MstDeductee_Load

        private void MstDeductee_Load(object sender, EventArgs e)
        {
            try
            {
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //
                GC.Collect();
                //dgvGrid.Height = 553;
                //-----------------------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                dgvGrid.Visible = true;
                dgvGrid.Height = 556;
                //--
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                    BtnPrint.Visible = false;
                //-----------------------------------------------------------
                //DisableControls();
                //-----------------------------------------------------------
                ControlVisible(false);
                ClearControls();
                //-----------------------------------------------------------
                //-- set the Help Grid Column Header Text & behavior
                //-- (0) Header Text
                //-- (1) Width
                //-- (2) Format
                //-- (3) Alignment
                //-- (4) NullToText
                //-- (5) Visible
                //-- (6) AutoSizeMode
                //-----------------------------------------------------------
                string[,] strMatrix1 = {{"DeducteeID", "0", "", "", "", "F", ""},
							            {"Deductee PAN", "200", "", "", "", "", "T"},
							            {"Deductee Name", "500", "", "", "", "", "T"},
							            //{"Deductee Code", "160", "", "", "", "", "T"},
                                        {"Hide", "80", "", "", "", "", "T"}};
                //-----------------------------------------------------------
                strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                string[,] strDeducteeMatrix = {{"MST_DEDUCTEE.DEDUCTEE_CODE = '01'", "F", "Company", "T"},
                                               {"MST_DEDUCTEE.DEDUCTEE_CODE = '02'", "F", "Non-Company", "T"}};
                //
                string[,] strDeducteeHideMatrix = {{"MST_DEDUCTEE.INACTIVE_FLAG = 0", "F", "", "T"},
                                               {"MST_DEDUCTEE.INACTIVE_FLAG = 1", "F", "Yes", "T"}};
                //-----------------------------------------------------------
                strOrderBy = "MST_DEDUCTEE.DEDUCTEE_NAME";
                strQuery = "SELECT MST_DEDUCTEE.DEDUCTEE_ID   AS DEDUCTEE_ID," +
                    "              MST_DEDUCTEE.DEDUCTEE_PAN  AS DEDUCTEE_PAN," +
                    "              MST_DEDUCTEE.DEDUCTEE_NAME AS DEDUCTEE_NAME," +
                    //"              " + cmnService.J_SQLDBFormat(strDeducteeMatrix, J_SQLColFormat.Case_End) + "     AS DEDUCTEE_CODE," +
                    "              " + cmnService.J_SQLDBFormat(strDeducteeHideMatrix, J_SQLColFormat.Case_End) + " AS DEDUCTEE_HIDE " +
                    "       FROM   MST_DEDUCTEE " ;
                //-----------------------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                //-----------------------------------------------------------
                lblTitle.Text = "Deductee Master";
                //-----------------------------------------------------------
                //Added by Indrajit on 12-02-2013
                //dgvGrid_Click(sender, e);
                //Starting the timer
                tmrGridRefresh.Start();
                //-----------------------------------------------------------
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }

        #endregion

        #region BtnAdd_Click

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                dgvGrid.Visible = false;
                lblSearchMode.Text = J_Mode.General;
                //---------------------------------------------
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                //---------------------------------------------
                strCheckFields = "";
                cmbDeducteeCode.Select();
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
                if (dgvGrid.CurrentRow != null)
                {
                    lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
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
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    dgvGrid.Visible = false;
                    lblSearchMode.Text = J_Mode.General;
                    //
                    chkHideDeductee.Visible = true;
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
                strSQL = strQuery + "order by " + strOrderBy;
                //-------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-------------------------------------------
                if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId) == false)
                    BtnAdd.Select();
                //
                chkHideDeductee.Visible = false;
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
                    strCheckFields = "WHERE MST_DEDUCTEE.DEDUCTEE_PAN like '%" + cmnService.J_ReplaceQuote(txtPANSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- DEDUCTEE NAME
                //-------------------------------------------------------------------
                if (txtDeducteeNameSearch.Text.Trim() != "")
                    if (strCheckFields == "")
                        strCheckFields = "WHERE MST_DEDUCTEE.DEDUCTEE_NAME like '" + cmnService.J_ReplaceQuote(txtDeducteeNameSearch.Text.Trim().ToUpper()) + "%' ";
                    else
                        strCheckFields = strCheckFields + " AND MST_DEDUCTEE.DEDUCTEE_NAME like '" + cmnService.J_ReplaceQuote(txtDeducteeNameSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- DEDUCTEE CODE
                //-------------------------------------------------------------------
                if (cmbDeducteeCodeSearch.SelectedIndex > 0)
                    if (strCheckFields == "")
                        strCheckFields = "WHERE MST_DEDUCTEE.DEDUCTEE_CODE = '" + cmnService.J_ReplaceQuote(cmbDeducteeCodeSearch.Text.Substring(0,2)) + "' ";
                    else
                        strCheckFields = strCheckFields + " AND MST_DEDUCTEE.DEDUCTEE_CODE = '" + cmnService.J_ReplaceQuote(cmbDeducteeCodeSearch.Text.Substring(0, 2)) + "' ";
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
            lblMode.Text = J_Mode.Delete;
            Insert_Update_Delete_Data();
        }

        #endregion

        #region BtnRefresh_Click

        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                //if (Convert.ToInt64(Convert.ToString(dgvGrid.CurrentRow)) < 0)
                //{
                //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                //    return;
                //}
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

            //dgvGrid.Select(dgvGrid.CurrentRow);
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
                //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.CurrentRow, 0]));
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
            //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.CurrentRow, 0]));
            lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
        }
        #endregion

        #region dgvGrid_MouseClick
        private void dgvGrid_MouseClick(object sender, MouseEventArgs e)
        {
            dgvGrid_Click(sender, e);
        }
        #endregion

        #region cmbDeducteeCode_SelectedIndexChanged
        private void cmbDeducteeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDeducteeCode.Text == T_DeducteeCode.Company)
                lblDeducteeCode.Text = "Deductee Type - " + T_DeducteeType.Company;
            else if (cmbDeducteeCode.Text == T_DeducteeCode.NonCompany)
                lblDeducteeCode.Text = "Deductee Type - " + T_DeducteeType.NonCompany;
            else if (cmbDeducteeCode.Text == T_DeducteeCode.HinduUndividedFamily)
                lblDeducteeCode.Text = "Deductee Type - " + T_DeducteeType.HinduUndividedFamily;
            else if (cmbDeducteeCode.Text == T_DeducteeCode.AOPExceptAOPOnlyCompaniesAsitsMembers)
                lblDeducteeCode.Text = "Deductee Type - " + T_DeducteeType.AOPExceptAOPOnlyCompaniesAsitsMembers;
            else if (cmbDeducteeCode.Text == T_DeducteeCode.AOPConsistingAsitsMembers)
                lblDeducteeCode.Text = "Deductee Type - " + T_DeducteeType.AOPConsistingAsitsMembers;
            else if (cmbDeducteeCode.Text == T_DeducteeCode.Co_operativeSociety)
                lblDeducteeCode.Text = "Deductee Type - " + T_DeducteeType.Co_operativeSociety;
            else if (cmbDeducteeCode.Text == T_DeducteeCode.Firm)
                lblDeducteeCode.Text = "Deductee Type - " + T_DeducteeType.Firm;
            else if (cmbDeducteeCode.Text == T_DeducteeCode.BodyOfIndividuals)
                lblDeducteeCode.Text = "Deductee Type - " + T_DeducteeType.BodyOfIndividuals;
            else if (cmbDeducteeCode.Text == T_DeducteeCode.ArtificialJuridicalPerson)
                lblDeducteeCode.Text = "Deductee Type - " + T_DeducteeType.ArtificialJuridicalPerson;
            else if (cmbDeducteeCode.Text == T_DeducteeCode.Others)
                lblDeducteeCode.Text = "Deductee Type - " + T_DeducteeType.Others;
            else
                lblDeducteeCode.Text = "";
        }
        #endregion

        #region txtPAN_Leave
        private void txtPAN_Leave(object sender, EventArgs e)
        {
            if (txtPAN.Text.Trim() == "") txtPAN.Text = "PANNOTAVBL";
        }
        #endregion

        #region txtPAN_KeyPress
        private void txtPAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                txtDeducteeName.Select();
            else
                if (TdsMan.gTANNoPANNoValidation(txtPAN, e, T_TANPAN.PAN) == false)
                    e.Handled = true;
        }
        #endregion

        #region txtPIN_KeyPress
        private void txtPIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,6,0", txtPIN, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtMobile_KeyPress
        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,0", txtMobile, "") == false)
                e.Handled = true;
        }
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
        private void lnkPANVerification_LinkClicked(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_GetPANforVerification = TdsMan.ValidatePAN(txtPAN.Text);
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
            //////cmnService.J_UserMessage(TDSMAN.Classes.TDSMAN.T_PANVerificationDisabledMessage);
        }
        #endregion 

        #region btnVerifyPAN_MouseMove
        private void btnVerifyPAN_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnVerifyPAN, "Verify PAN");
        }
        #endregion

        #region txtEmail_Leave
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() != "")
            {
                #region COMMENT
                //string strEmail = txtEmail.Text.Trim();
                ////
                //if (strEmail.Contains("@") == false)
                //{
                //    cmnService.J_UserMessage("Email should have atleast one '@'");
                //    return;
                //}
                ////--
                //if (strEmail.Contains(".") == false)
                //{
                //    cmnService.J_UserMessage("Email should have atleast one '.'");
                //    return;
                //}
                ////--
                //if (strEmail.StartsWith("@") == true)
                //{
                //    cmnService.J_UserMessage("'@' should be preceded by atleast one character.");
                //    return;
                //}
                ////
                //if (strEmail.EndsWith("@") == true)
                //{
                //    cmnService.J_UserMessage("'@' should be succeeded by atleast one character.");
                //    return;
                //}
                ////--
                //if (strEmail.StartsWith(".") == true)
                //{
                //    cmnService.J_UserMessage("'.' should be preceded by atleast one character.");
                //    return;
                //}
                ////
                //if (strEmail.EndsWith(".") == true)
                //{
                //    cmnService.J_UserMessage("'.' should be succeeded by atleast one character.");
                //    return;
                //}
                ////--
                //if (strEmail.Substring(strEmail.IndexOf("@"), (strEmail.Trim().Length - strEmail.Trim().IndexOf("@"))).Contains(".") == false)
                //{
                //    cmnService.J_UserMessage("At least one '.' should come after '@'");
                //    return;
                //}
                #endregion
                if (TdsMan.T_CheckEmailFormat(txtEmail.Text) == false)
                {
                    txtEmail.Select();
                    return;
                }
            }
        }
        #endregion


        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0019", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            //tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {

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
            //--
            //string[] strDeducteeCode ={ T_DeducteeCode.Company, T_DeducteeCode.NonCompany };
            string[] strDeducteeCode =  {T_DeducteeCode.Company,
                                        T_DeducteeCode.NonCompany,
                                        T_DeducteeCode.HinduUndividedFamily,
                                        T_DeducteeCode.AOPConsistingAsitsMembers,
                                        T_DeducteeCode.AOPExceptAOPOnlyCompaniesAsitsMembers,
                                        T_DeducteeCode.Co_operativeSociety,
                                        T_DeducteeCode.Firm,
                                        T_DeducteeCode.BodyOfIndividuals,
                                        T_DeducteeCode.ArtificialJuridicalPerson,
                                        T_DeducteeCode.Others};
            dmlService.J_PopulateComboBox(strDeducteeCode, ref cmbDeducteeCode, J_ComboBoxDefaultText.NO);
            //--
            txtPAN.Text = "PANNOTAVBL";
            txtOldPAN.Text = "";
            txtDeducteeName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtAddress4.Text = "";
            txtAddress5.Text = "";
            //-----------
            //-- STATE
            //-----------
            strSQL = " SELECT STATE_ID," +
                "             STATE_NAME " +
                "      FROM   MST_STATE " +
                "      ORDER BY STATE_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbState) == false) return;
            //-----------
            txtPIN.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            //--
            txtTaxIdentificationNumber.Text = "";
            txtDeducteeAddress.Text = "";
            //--------------------
            txtPANSearch.Text = "";
            txtDeducteeNameSearch.Text = "";
            //txtDeducteeCodeSearch.Text = "";
            string[] strDeducteeCodeDesc ={ T_DeducteeCodeDesc.Company, T_DeducteeCodeDesc.NonCompany };
            dmlService.J_PopulateComboBox(strDeducteeCodeDesc, ref cmbDeducteeCodeSearch, J_ComboBoxDefaultText.YES); 
            //
            txtRefNo.Text = "";
            //
            chkHideDeductee.Checked = false;                
            //                        
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            string strStateName = string.Empty;
            string strDistrictName = string.Empty;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            try
            {
                strSQL = "SELECT  MST_DEDUCTEE.DEDUCTEE_CODE AS DEDUCTEE_CODE," +
                    "             MST_DEDUCTEE.DEDUCTEE_PAN  AS DEDUCTEE_PAN," +
                    "             MST_DEDUCTEE.DEDUCTEE_NAME AS DEDUCTEE_NAME," +
                    "             MST_DEDUCTEE.ADDRESS1      AS ADDRESS1," +
                    "             MST_DEDUCTEE.ADDRESS2      AS ADDRESS2," +
                    "             MST_DEDUCTEE.ADDRESS3      AS ADDRESS3," +
                    "             MST_DEDUCTEE.ADDRESS4      AS ADDRESS4," +
                    "             MST_DEDUCTEE.ADDRESS5      AS ADDRESS5," +
                    "             MST_DEDUCTEE.STATE_ID      AS STATE_ID," +
                    "             MST_STATE.STATE_NAME       AS STATE_NAME," +
                    "             MST_DEDUCTEE.PIN_CODE      AS PIN_CODE," +
                    "             MST_DEDUCTEE.MOBILE_NO     AS MOBILE_NO," +
                    "             MST_DEDUCTEE.EMAIL         AS EMAIL," +
                    "             MST_DEDUCTEE.DEDUCTEE_REF  AS DEDUCTEE_REF," +
                    "             MST_DEDUCTEE.DEDUCTEE_ADDRESS AS DEDUCTEE_ADDRESS," +
                    "             MST_DEDUCTEE.DEDUCTEE_TAX_ID  AS DEDUCTEE_TAX_ID," +
                    "             MST_DEDUCTEE.INACTIVE_FLAG  AS INACTIVE_FLAG " +
                    "     FROM    MST_DEDUCTEE " +         
                    "     LEFT JOIN MST_STATE " +
                    "             ON MST_DEDUCTEE.STATE_ID = MST_STATE.STATE_ID " +
                    "     WHERE   MST_DEDUCTEE.DEDUCTEE_ID = " + Id + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;

                    cmbDeducteeCode.Text = Convert.ToString(drdShowRecord["DEDUCTEE_CODE"]);
                    //--
                    txtPAN.Text = Convert.ToString(drdShowRecord["DEDUCTEE_PAN"]);
                    txtOldPAN.Text = Convert.ToString(drdShowRecord["DEDUCTEE_PAN"]);
                    txtDeducteeName.Text = Convert.ToString(drdShowRecord["DEDUCTEE_NAME"]);
                    txtAddress1.Text = Convert.ToString(drdShowRecord["ADDRESS1"]);
                    txtAddress2.Text = Convert.ToString(drdShowRecord["ADDRESS2"]);
                    txtAddress3.Text = Convert.ToString(drdShowRecord["ADDRESS3"]);
                    txtAddress4.Text = Convert.ToString(drdShowRecord["ADDRESS4"]);
                    txtAddress5.Text = Convert.ToString(drdShowRecord["ADDRESS5"]);
                    cmbState.Text = Convert.ToString(drdShowRecord["STATE_NAME"]);
                    txtPIN.Text = Convert.ToString(drdShowRecord["PIN_CODE"]);
                    txtMobile.Text = Convert.ToString(drdShowRecord["MOBILE_NO"]);
                    txtEmail.Text = Convert.ToString(drdShowRecord["EMAIL"]);
                    txtRefNo.Text = Convert.ToString(drdShowRecord["DEDUCTEE_REF"]);
                    //-- 2016/09/19
                    txtTaxIdentificationNumber.Text = Convert.ToString(drdShowRecord["DEDUCTEE_TAX_ID"]);
                    txtDeducteeAddress.Text = Convert.ToString(drdShowRecord["DEDUCTEE_ADDRESS"]);
                    //
                    if (Convert.ToString(drdShowRecord["INACTIVE_FLAG"]) == "1")
                    {
                        chkHideDeductee.Visible = true;
                        chkHideDeductee.Checked = true;
                    }
                    else
                        chkHideDeductee.Visible = false;                        
                    //
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();

                    //cmbState.Text = strStateName;
                    //cmbDistrict.Text = strDistrictName;

                    cmbDeducteeCode.Select();
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
                    //if (Convert.ToInt64(Convert.ToString(dgvGrid.CurrentRow)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, "DEDUCTEE_ID", lngSearchId);
                    //    return false;
                    //}
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    if (grpSearch.Visible == false)
                    {
                        if (dgvGrid.CurrentRow == null)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            if (dsetGridClone == null) return false;
                            dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtPANSearch.Text.Trim() == "" &&
                            txtDeducteeNameSearch.Text.Trim() == "" &&
                            cmbDeducteeCodeSearch.SelectedIndex == 0)
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
                    //-- DEDUCTEE CODE
                    //-----------------------------------------------------------------------
                    if (cmbDeducteeCode.SelectedIndex < 0)
                    {
                        cmnService.J_UserMessage("Deductee Code - Cannot be Blank");
                        cmbDeducteeCode.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PAN
                    //-----------------------------------------------------------------------
                    if (txtPAN.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("PAN - Cannot be Blank");
                        txtPAN.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PAN FORMAT
                    //-----------------------------------------------------------------------
                    if (txtPAN.Text.Length != 10)
                    {
                        cmnService.J_UserMessage("PAN No. should be of 10 characters");
                        txtPAN.Select();
                        return false;
                    }
                    if (txtPAN.Text != "PANNOTAVBL")
                    {
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtPAN.Text, 5), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtPAN.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtPAN.Text, 5, 4), J_DataType.Numeric) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtPAN.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtPAN.Text, 1), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtPAN.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- DEDUCTEE NAME
                    //-----------------------------------------------------------------------
                    if (txtDeducteeName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Deductee Name - Cannot be Blank");
                        txtDeducteeName.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtDeducteeName.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Deductee Name - '^' not allowed");
                        txtDeducteeName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- DEDUCTEE NAME & PAN
                    //-----------------------------------------------------------------------
                    strSQL = "SELECT DEDUCTEE_ID " +
                        "     FROM   MST_DEDUCTEE " +
                        "     WHERE  DEDUCTEE_NAME ='" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "'" +
                        "     AND    DEDUCTEE_PAN  ='" + cmnService.J_ReplaceQuote(txtPAN.Text) + "'";
                    if (lblMode.Text == J_Mode.Edit)
                        strSQL = strSQL + "AND DEDUCTEE_ID <> " + lngSearchId;
                    //lngDeducteeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //--
                    if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                    {
                        cmnService.J_UserMessage("The combination of [Deductee Name & PAN] exists");
                        txtDeducteeName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- SAME PAN CHECK
                    //-----------------------------------------------------------------------
                    if (txtPAN.Text != "PANNOTAVBL")
                    {
                        strSQL = "SELECT DEDUCTEE_ID " +
                            "     FROM   MST_DEDUCTEE " +
                            "     WHERE  DEDUCTEE_PAN  ='" + cmnService.J_ReplaceQuote(txtPAN.Text) + "'";
                        //"     AND    GROUP_ID      = " + Convert.ToInt32(Support.GetItemData(cmbGroupName, cmbGroupName.SelectedIndex)) + "";
                        //"     AND    GROUP_ID    = " + Convert.ToInt32(Support.GetItemData(cmbGroup, cmbGroup.SelectedIndex)) + " ";
                        if (lblMode.Text == J_Mode.Edit)
                            strSQL = strSQL + "AND DEDUCTEE_ID <> " + lngSearchId;
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                        {
                            if (cmnService.J_UserMessage("Same PAN already exists - Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                //cmnService.J_UserMessage("The combination of [Employee Name & PAN] exists for the selected Company");
                                txtPAN.Select();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //-----------------------------------------------------------------------
                        //-- Deductee reference no.
                        //-----------------------------------------------------------------------
                        if (TDSMAN.Classes.TDSMAN.T_AllotAutomaticRefNo == false)
                        {
                            if (txtRefNo.Text.Trim() == "")
                            {
                                cmnService.J_UserMessage("Deductee reference no. - Cannot be Blank");
                                txtRefNo.Select();
                                return false;
                            }
                        }
                        //
                        if (txtRefNo.Text.Trim() != "")
                        {
                            strSQL = "SELECT DEDUCTEE_ID " +
                                "     FROM   MST_DEDUCTEE " +
                                "     WHERE  DEDUCTEE_REF  ='" + cmnService.J_ReplaceQuote(txtRefNo.Text) + "'";
                            //
                            if (lblMode.Text == J_Mode.Edit)
                                strSQL = strSQL + "AND DEDUCTEE_ID <> " + lngSearchId;
                            //--
                            if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                            {
                                cmnService.J_UserMessage("Deductee reference no. - Already exists");
                                txtRefNo.Select();
                                return false;
                            }
                            //--
                            if (txtRefNo.Text.Length < 10)
                            {
                                cmnService.J_UserMessage("Deductee reference no. - Should be of 10 characters");
                                txtRefNo.Select();
                                return false;
                            }
                        }
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAddress1.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Address1 - '^' not allowed");
                        txtAddress1.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAddress2.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Address2 - '^' not allowed");
                        txtAddress2.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAddress3.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Address3 - '^' not allowed");
                        txtAddress3.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAddress4.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Address4 - '^' not allowed");
                        txtAddress4.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAddress5.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Address5 - '^' not allowed");
                        txtAddress5.Select();
                        return false;
                    }                    
                    //-----------------------------------------------------------------------
                    //-- PIN
                    //-----------------------------------------------------------------------
                    if ((txtPIN.Text.Trim().Length != 0) && (txtPIN.Text.Trim().Length != 6))
                    {
                        cmnService.J_UserMessage("Incorrect Format of the PIN");
                        txtPIN.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtEmail.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Email - '^' not allowed");
                        txtEmail.Select();
                        return false;
                    }
                    // STATE
                    // 2020/12/23
                    if (cmbState.SelectedIndex > 0)
                    {
                        if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT INACTIVE_FLAG FROM MST_STATE WHERE STATE_ID = " + Convert.ToInt32(Support.GetItemData(cmbState, cmbState.SelectedIndex)))) == "1")
                        {
                            cmnService.J_UserMessage("State - " + cmbState.Text + " is not applicable");
                            cmbState.Select();
                            return false;
                        }
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
                long lngStateId = 0;
                //--------------------------------------------
                //-- STATE
                //--------------------------------------------
                if (cmbState.SelectedIndex <= 0)
                    lngStateId = 0;
                else
                    lngStateId = Convert.ToInt32(Support.GetItemData(cmbState, cmbState.SelectedIndex));
                //--------------------------------------------
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        //*****  For Insert
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        if (ValidateFields() == false)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        if (cmnService.J_SaveConfirmationMessage(ref cmbDeducteeCode) == true)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        //-----------------------------------------------------------
                        strSQL = "INSERT INTO MST_DEDUCTEE (" +
                                 "            DEDUCTEE_CODE," +
                                 "            DEDUCTEE_NAME," +
                                 "            DEDUCTEE_PAN," +
                                 "            GROUP_ID," +
                                 "            ADDRESS1," +
                                 "            ADDRESS2," +
                                 "            ADDRESS3," +
                                 "            ADDRESS4," +
                                 "            ADDRESS5," +
                                 "            STATE_ID," +
                                 "            PIN_CODE," +
                                 "            MOBILE_NO," +
                                 "            EMAIL," +
                                 "            DEDUCTEE_REF," +
                                 "            DEDUCTEE_ADDRESS," +
                                 "            DEDUCTEE_TAX_ID) " +
                                 "     VALUES('" + cmnService.J_ReplaceQuote(cmbDeducteeCode.Text) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtPAN.Text.Trim()) + "'," +
                                 "             " + TDSMAN.Classes.TDSMAN.T_pGroupId + "," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAddress1.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAddress2.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAddress3.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAddress4.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAddress5.Text.Trim()) + "'," +
                                 "             " + Convert.ToInt32(Support.GetItemData(cmbState, cmbState.SelectedIndex)) + "," +
                                 "            '" + cmnService.J_ReplaceQuote(txtPIN.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtMobile.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtEmail.Text.Trim()) + "', " +
                                 "            '" + cmnService.J_ReplaceQuote(txtRefNo.Text) + "', " +
                                 "            '" + cmnService.J_ReplaceQuote(txtDeducteeAddress.Text) + "', " +
                                 "            '" + cmnService.J_ReplaceQuote(txtTaxIdentificationNumber.Text) + "')";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            cmbDeducteeCode.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        lngSearchId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "MST_DEDUCTEE", "DEDUCTEE_ID");
                        if (lngSearchId == 0)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        //-----------------------------------------------------------
                        //Added by Indrajit on 11-02-2013
                        //-----------------------------------------------------------------------
                        //-- DEDUCTEE NAME + PAN
                        //-----------------------------------------------------------------------
                        strSQL = "SELECT COUNT(DEDUCTEE_ID) " +
                            "     FROM   MST_DEDUCTEE " +
                            "     WHERE  DEDUCTEE_NAME ='" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "'" +
                            "     AND    DEDUCTEE_PAN  ='" + cmnService.J_ReplaceQuote(txtPAN.Text) + "'" +
                            "     AND    DEDUCTEE_ID  <> " + lngSearchId;
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        {
                            cmnService.J_UserMessage("Deductee Name exists");
                            txtDeducteeName.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //End of add zone  ------------------------------------------

                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        //-----------------------------------------------------------
                        ClearControls();
                        //-----------------------------------------------------------
                        cmbDeducteeCode.Select();
                        //-----------------------------------------------------------
                        break;
                    case J_Mode.Edit:
                        //*****  For Modify
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        if (ValidateFields() == false)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        int intHideDeductee = 0;
                        //
                        if (chkHideDeductee.Checked == true)
                        {
                            intHideDeductee = 1;
                        }                        
                        //-----------------------------------------------------------
                        if (cmnService.J_SaveConfirmationMessage(ref cmbDeducteeCode) == true)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        //Added by Indrajit on 11-02-2013
                        if (Check_Record(lngSearchId) == 0)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //----------
                        //-----------------------------------------------------------
                        strSQL = "UPDATE MST_DEDUCTEE " +
                                 "SET    DEDUCTEE_CODE =  '" + cmnService.J_ReplaceQuote(cmbDeducteeCode.Text.Trim()) + "'," +
                                 "       DEDUCTEE_NAME = '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text.Trim()) + "'," +
                                 "       DEDUCTEE_PAN  = '" + cmnService.J_ReplaceQuote(txtPAN.Text.Trim()) + "'," +
                                 "       ADDRESS1      = '" + cmnService.J_ReplaceQuote(txtAddress1.Text.Trim()) + "'," +
                                 "       ADDRESS2      = '" + cmnService.J_ReplaceQuote(txtAddress2.Text.Trim()) + "'," +
                                 "       ADDRESS3      = '" + cmnService.J_ReplaceQuote(txtAddress3.Text.Trim()) + "'," +
                                 "       ADDRESS4      = '" + cmnService.J_ReplaceQuote(txtAddress4.Text.Trim()) + "'," +
                                 "       ADDRESS5      = '" + cmnService.J_ReplaceQuote(txtAddress5.Text.Trim()) + "'," +
                                 "       STATE_ID      =  " + Convert.ToInt32(Support.GetItemData(cmbState, cmbState.SelectedIndex)) + "," +
                                 "       PIN_CODE      = '" + cmnService.J_ReplaceQuote(txtPIN.Text.Trim()) + "'," +
                                 "       MOBILE_NO     = '" + cmnService.J_ReplaceQuote(txtMobile.Text.Trim()) + "'," +
                                 "       EMAIL         = '" + cmnService.J_ReplaceQuote(txtEmail.Text.Trim()) + "', " +
                                 "       DEDUCTEE_REF  = '" + cmnService.J_ReplaceQuote(txtRefNo.Text) + "', " +
                                 "       DEDUCTEE_ADDRESS = '" + cmnService.J_ReplaceQuote(txtDeducteeAddress.Text) + "', " +
                                 "       DEDUCTEE_TAX_ID  = '" + cmnService.J_ReplaceQuote(txtTaxIdentificationNumber.Text) + "', " +
                                 "       INACTIVE_FLAG = " + intHideDeductee + " " +
                                 "WHERE  DEDUCTEE_ID   =  " + lngSearchId + "";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            cmbDeducteeCode.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        ////-- UPDATE THE TRANSACTION
                        //if (txtOldPAN.Text.ToUpper() == "PANNOTAVBL")
                        //{
                        //    if (txtOldPAN.Text.ToUpper() != txtPAN.Text.ToUpper())
                        //    {
                        //        strSQL = "UPDATE TRN_DEDUCTEE_DETAILS SET " +
                        //            "            NON_DEDUCTION_FLAG = '' " +
                        //            "     WHERE  PARTY_ID           = " + lngSearchId + " " +
                        //            "     AND    NON_DEDUCTION_FLAG = 'C'";
                        //        dmlService.J_ExecSql(strSQL);
                        //    }
                        //}
                        //

                        //-----------------------------------------------------------
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
                        //
                        chkHideDeductee.Visible = false;                
                        //-----------------------------------------------------------
                        //DisableControls();
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId);
                        break;
                    case J_Mode.Delete:
                        //-----------------------------------------------------------
                        if (dgvGrid.CurrentRow == null)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            return;
                        }//Added by Indrajit on 11-02-2013
                        //if (Check_Record(lngSearchId) == 0)
                        //{
                        //    dmlService.J_Rollback();
                        //    return;
                        //}
                        //-----------------------------------------------------------                        
                        dmlService.J_BeginTransaction();
                        //----------
                        //-----------------------------------------------------------
                        //-- CHECK THE TRANSACTION
                        //-----------------------------------------------------------------------
                        //-- TRN_DEDUCTEE_DETAILS
                        //-----------------------------------------------------------------------
                        strSQL = "SELECT PARTY_ID " +
                            "     FROM   TRN_DEDUCTEE_DETAILS," +
                            "            TRN_BASIC_INFO " +
                            "     WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID " +
                            "     AND    TRN_BASIC_INFO.FORM_NO            <> '" + T_FormNo.F24Q + "' " +
                            "     AND    TRN_DEDUCTEE_DETAILS.PARTY_ID      = " + lngSearchId + " ";
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        {
                            cmnService.J_UserMessage("The Deductee cannot be deleted");
                            dmlService.J_Rollback();
                            return;
                        }
                        //--
                        if (cmnService.J_UserMessage("Proceed with Deletion?", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            lblMode.Text = J_Mode.View;
                            BtnDelete.Select();
                            dmlService.J_Rollback(); 
                            return;
                        }
                        //..........................................................
                        //-----------------------------------------------------------
                        strSQL = "DELETE FROM MST_DEDUCTEE WHERE DEDUCTEE_ID =  " + lngSearchId + "";
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
            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "MST_DEDUCTEE","DEDUCTEE_ID", lngSrchId) == true) return lngSrchId;

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

        #endregion


        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0020", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion


        #region BtnPrint_Click
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //--
            //    if (dgvGrid.CurrentRow == null)
            //    {
            //        cmnService.J_UserMessage("No Employee exists");
            //        BtnAdd.Select();
            //        return;
            //    }
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
            //    string[,] strDeducteeMatrix = {{"MST_DEDUCTEE.DEDUCTEE_CODE = '01'", "F", "Company", "T"},
            //                                   {"MST_DEDUCTEE.DEDUCTEE_CODE = '02'", "F", "Non-Company", "T"}};
            //    //
            //    strSQL = "SELECT MST_DEDUCTEE.DEDUCTEE_ID     AS DEDUCTEE_ID," +
            //        "              MST_DEDUCTEE.DEDUCTEE_PAN  AS DEDUCTEE_PAN," +
            //        "              MST_DEDUCTEE.DEDUCTEE_NAME AS DEDUCTEE_NAME," +
            //        "              " + cmnService.J_SQLDBFormat(strDeducteeMatrix, J_SQLColFormat.Case_End) + " AS DEDUCTEE_CODE," +
            //        "              MST_STATE.STATE_NAME       AS STATE_NAME," +
            //        "              MST_DEDUCTEE.PIN_CODE      AS PIN_CODE," +
            //        "              MST_DEDUCTEE.MOBILE_NO     AS MOBILE_NO," +
            //        "              MST_DEDUCTEE.EMAIL         AS EMAIL," +
            //        "              MST_DEDUCTEE.DEDUCTEE_REF  AS DEDUCTEE_REF " +
            //        "       FROM   MST_DEDUCTEE " +
            //        "     LEFT JOIN MST_STATE " +
            //        "             ON MST_DEDUCTEE.STATE_ID = MST_STATE.STATE_ID ";
            //    //-----------------------------------------------------------
            //    strSQL = strSQL + " ORDER BY MST_DEDUCTEE.DEDUCTEE_NAME";
            //    //--
            //    strFileName = "XL_DEDUCTEE_LIST_"  + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".XLSX";
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
            //        if (CREATE_NEW_WORKSHEET(Path.Combine(strPath, strFileName), "DEDUCTEE LIST") == false) return;
            //        //
            //        if (WRITE_RECEIPT_NO_REG_WORKSHEET(Path.Combine(strPath, strFileName), "DEDUCTEE LIST", strSQL) == false) return;
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
                wsnew.get_Range("C1", m).Value2 = "CODE";
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
                wsnew.get_Range("E1", m).Value2 = "State";
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
                wsnew.get_Range("F1", m).Value2 = "PIN";
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
                wsnew.get_Range("G1", m).Value2 = "Mobile No.";
                wsnew.get_Range("G1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("G1", m).Borders.Value = true;
                wsnew.get_Range("G1", m).ColumnWidth = 20;
                wsnew.get_Range("G1", m).WrapText = true;
                wsnew.get_Range("G1", m).Font.Name = "Arial";
                wsnew.get_Range("G1", m).Font.Bold = true;
                wsnew.get_Range("G1", m).Font.Size = 10;
                wsnew.get_Range("G1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("G1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("H1", m).Value2 = "Email";
                wsnew.get_Range("H1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("H1", m).Borders.Value = true;
                wsnew.get_Range("H1", m).ColumnWidth = 20;
                wsnew.get_Range("H1", m).WrapText = true;
                wsnew.get_Range("H1", m).Font.Name = "Arial";
                wsnew.get_Range("H1", m).Font.Bold = true;
                wsnew.get_Range("H1", m).Font.Size = 10;
                wsnew.get_Range("H1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("H1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
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
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["DEDUCTEE_NAME"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["DEDUCTEE_PAN"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("C" + lngSheetRow, m).Value2 = drdGetSheetRecord["DEDUCTEE_CODE"].ToString();
                    wsnew.get_Range("C" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("D" + lngSheetRow, m).Value2 = drdGetSheetRecord["DEDUCTEE_REF"].ToString();
                    wsnew.get_Range("D" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("E" + lngSheetRow, m).Value2 = drdGetSheetRecord["STATE_NAME"].ToString();
                    wsnew.get_Range("E" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("F" + lngSheetRow, m).Value2 = drdGetSheetRecord["PIN_CODE"].ToString();
                    wsnew.get_Range("F" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("G" + lngSheetRow, m).Value2 = drdGetSheetRecord["MOBILE_NO"].ToString();
                    wsnew.get_Range("G" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("G" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("G" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("G" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("H" + lngSheetRow, m).Value2 = drdGetSheetRecord["EMAIL"].ToString();
                    wsnew.get_Range("H" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("H" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("H" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("H" + lngSheetRow, m).Font.Size = 10;
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

        #region dgvGrid_MouseUp
        private void dgvGrid_MouseUp(object sender, MouseEventArgs e)
        {
            dgvGrid_Click(sender, e);
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

        #region TlStrpMnuCSV_Click
        private void TlStrpMnuCSV_Click(object sender, EventArgs e)
        {
            try
            {
                //--
                if (dgvGrid.CurrentRow == null)
                {
                    cmnService.J_UserMessage("No Deductee exists");
                    BtnAdd.Select();
                    return;
                }
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
                string[,] strDeducteeMatrix = {{"MST_DEDUCTEE.DEDUCTEE_CODE = '01'", "F", "Company", "T"},
                                               {"MST_DEDUCTEE.DEDUCTEE_CODE = '02'", "F", "Non-Company", "T"}};
                //
                strSQL = "SELECT MST_DEDUCTEE.DEDUCTEE_ID     AS DEDUCTEE_ID," +
                    "              MST_DEDUCTEE.DEDUCTEE_PAN  AS DEDUCTEE_PAN," +
                    "              MST_DEDUCTEE.DEDUCTEE_NAME AS DEDUCTEE_NAME," +
                    "              " + cmnService.J_SQLDBFormat(strDeducteeMatrix, J_SQLColFormat.Case_End) + " AS DEDUCTEE_CODE," +
                    "              MST_STATE.STATE_NAME       AS STATE_NAME," +
                    "              MST_DEDUCTEE.PIN_CODE      AS PIN_CODE," +
                    "              MST_DEDUCTEE.MOBILE_NO     AS MOBILE_NO," +
                    "              MST_DEDUCTEE.EMAIL         AS EMAIL," +
                    "              MST_DEDUCTEE.DEDUCTEE_REF  AS DEDUCTEE_REF " +
                    "       FROM   MST_DEDUCTEE " +
                    "     LEFT JOIN MST_STATE " +
                    "             ON MST_DEDUCTEE.STATE_ID = MST_STATE.STATE_ID ";
                //-----------------------------------------------------------
                strSQL = strSQL + " ORDER BY MST_DEDUCTEE.DEDUCTEE_NAME";
                //--
                strFileName = "CSV_DEDUCTEE_LIST_" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".CSV";
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
                //    if (CREATE_NEW_WORKSHEET(Path.Combine(strPath, strFileName), "DEDUCTEE LIST") == false) return;
                //    //
                //    if (WRITE_RECEIPT_NO_REG_WORKSHEET(Path.Combine(strPath, strFileName), "DEDUCTEE LIST", strSQL) == false) return;
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

        #region TlStrpMnuExcel_Click
        private void TlStrpMnuExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //--
                if (dgvGrid.CurrentRow == null)
                {
                    cmnService.J_UserMessage("No Deductee exists");
                    BtnAdd.Select();
                    return;
                }
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
                string[,] strDeducteeMatrix = {{"MST_DEDUCTEE.DEDUCTEE_CODE = '01'", "F", "Company", "T"},
                                               {"MST_DEDUCTEE.DEDUCTEE_CODE = '02'", "F", "Non-Company", "T"}};
                //
                strSQL = "SELECT MST_DEDUCTEE.DEDUCTEE_ID     AS DEDUCTEE_ID," +
                    "              MST_DEDUCTEE.DEDUCTEE_PAN  AS DEDUCTEE_PAN," +
                    "              MST_DEDUCTEE.DEDUCTEE_NAME AS DEDUCTEE_NAME," +
                    "              " + cmnService.J_SQLDBFormat(strDeducteeMatrix, J_SQLColFormat.Case_End) + " AS DEDUCTEE_CODE," +
                    "              MST_STATE.STATE_NAME       AS STATE_NAME," +
                    "              MST_DEDUCTEE.PIN_CODE      AS PIN_CODE," +
                    "              MST_DEDUCTEE.MOBILE_NO     AS MOBILE_NO," +
                    "              MST_DEDUCTEE.EMAIL         AS EMAIL," +
                    "              MST_DEDUCTEE.DEDUCTEE_REF  AS DEDUCTEE_REF " +
                    "       FROM   MST_DEDUCTEE " +
                    "     LEFT JOIN MST_STATE " +
                    "             ON MST_DEDUCTEE.STATE_ID = MST_STATE.STATE_ID ";
                //-----------------------------------------------------------
                strSQL = strSQL + " ORDER BY MST_DEDUCTEE.DEDUCTEE_NAME";
                //--
                strFileName = "XL_DEDUCTEE_LIST_" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".XLSX";
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
                    if (CREATE_NEW_WORKSHEET(Path.Combine(strPath, strFileName), "DEDUCTEE LIST") == false) return;
                    //
                    if (WRITE_RECEIPT_NO_REG_WORKSHEET(Path.Combine(strPath, strFileName), "DEDUCTEE LIST", strSQL) == false) return;
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

