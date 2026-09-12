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
using System.Text;

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormTrn;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion


namespace TDSMAN.FormEmail
{
    public partial class SysEmailFormat : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public SysEmailFormat()
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
        //
        long lngDefaultFormat = 0; string strHTMLFormat="";
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
            //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        }
        #endregion

        #region SysEmailFormat_Load

        private void SysEmailFormat_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            try
            {
                GC.Collect();
                //ViewGrid.Height = 553;
                ViewGrid.Height = 0;
                //-----------------------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                dgvGrid.Visible = true;
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
                string[,] strMatrix1 = {{"FormatID", "0", "", "", "", "F", ""},
							            {"Company Name", "150", "", "", "", "", "T"},
							            {"Certificate", "150", "", "", "", "", "T"},
							            {"Description", "500", "", "", "", "", "T"},
                                        {"HTML", "100", "", "", "", "", "T"},
                                        {"Hide", "100", "", "", "", "", "T"},
                                        {"DEFAULT", "0", "", "", "", "", ""}};
                //-----------------------------------------------------------
                strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                string[,] strDeducteeHideMatrix = {{"SYS_EMAIL_FORMAT.INACTIVE_FLAG = 0", "F", "", "T"},
                                               {"SYS_EMAIL_FORMAT.INACTIVE_FLAG = 1", "F", "Yes", "T"}};
                string[,] strhtmlMatrix = {{"SYS_EMAIL_FORMAT.HTML_FORMAT = 0", "F", "", "T"},
                                               {"SYS_EMAIL_FORMAT.HTML_FORMAT = 1", "F", "Yes", "T"}};
                //-----------------------------------------------------------
                strOrderBy = "SYS_EMAIL_FORMAT.DEFAULT_FORMAT DESC, SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID DESC";
                strQuery = "SELECT SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID         AS EMAIL_FORMAT_ID," +
                    "              MST_COMPANY.COMPANY_NAME " + cmnService.J_ConcateSQLSyntaxOperator() + " '[' " + cmnService.J_ConcateSQLSyntaxOperator() + " MST_COMPANY.TAN_NO " + cmnService.J_ConcateSQLSyntaxOperator() + " ']' AS COMPANY," +
                    "              MST_EMAIL_CERTIFICATES.CERTIFICATE_DESC  AS CERTIFICATE_DESC," +
                    "              SYS_EMAIL_FORMAT.EMAIL_FORMAT_DESC       AS EMAIL_FORMAT_DESC," +
                    "              " + cmnService.J_SQLDBFormat(strhtmlMatrix, J_SQLColFormat.Case_End) + " AS HTML_FORMAT," +
                    "              " + cmnService.J_SQLDBFormat(strDeducteeHideMatrix, J_SQLColFormat.Case_End) + " AS DEDUCTEE_HIDE," +
                    "              SYS_EMAIL_FORMAT.DEFAULT_FORMAT          AS DEFAULT_FORMAT " +
                    "       FROM ((SYS_EMAIL_FORMAT LEFT JOIN MST_COMPANY " +
                    "       ON     SYS_EMAIL_FORMAT.COMPANY_ID = MST_COMPANY.COMPANY_ID) " +
                    "              INNER JOIN MST_EMAIL_CERTIFICATES " +
                    "       ON     SYS_EMAIL_FORMAT.EMAIL_CERTIFICATE_ID = MST_EMAIL_CERTIFICATES.EMAIL_CERTIFICATE_ID) ";
                //-----------------------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                //-----------------------------------------------------------
                lblTitle.Text = "Email Format";
                //-----------------------------------------------------------
                //Added by Indrajit on 12-02-2013
                ViewGrid_Click(sender, e);
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
                lblSearchMode.Text = J_Mode.General;
                dgvGrid.Visible = false;
                //---------------------------------------------
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                //--
                BtnSave.Enabled = true;
                BtnSave.BackColor = Color.Lavender;
                //
                cmbCompanyName.Enabled = true;
                //
                chkHtmlBody.Enabled = true;
                lnkViewHTML.Enabled = true;
                chkInactive.Enabled = true;
                //---------------------------------------------
                strCheckFields = "";
                cmbCompanyName.Select();
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
                    ////Added by Indrajit on 18-02-2013
                    //if (Check_Record(lngSearchId) == 0) return;
                    //--------------------------------------------------
                    ControlVisible(true);
                    ClearControls();
                    //--------------------------------------------------
                    //A particular ID wise retriving the data from database
                    lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
                    if (ShowRecord(lngSearchId) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        //dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMAIL_FORMAT_ID", lngSearchId);
                        dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                    }
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    dgvGrid.Visible = false;
                    //
                    chkInactive.Visible = true;
                    //--------------------------------------------------
                    //lngDefaultFormat = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 5]));
                    lngDefaultFormat = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[6].Value));
                    strHTMLFormat = Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[4].Value);

                    if (lngDefaultFormat == 1)
                    {
                        BtnSave.Enabled = false;
                        BtnSave.BackColor = Color.LightGray;
                        //
                        cmbCompanyName.Enabled = false;
                        //
                        chkHtmlBody.Enabled = false;
                        lnkViewHTML.Enabled = false;
                        chkInactive.Enabled = false;
                        if(strHTMLFormat != "")
                        {
                            chkHtmlBody.Enabled = true;
                            lnkViewHTML.Enabled = true;
                        }
                    }
                    else
                    {
                        BtnSave.Enabled = true;
                        BtnSave.BackColor = Color.Lavender;
                        //
                        cmbCompanyName.Enabled = true;
                        //
                        chkHtmlBody.Enabled = true;
                        lnkViewHTML.Enabled = true;
                        chkInactive.Enabled = true;
                    }
                    //--------------------------------------------------
                    strCheckFields = "";
                    //--------------------------------------------------
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_FORMAT_ID", lngSearchId);
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
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix); 
                if (dsetGridClone == null) return;
                //-------------------------------------------
                //if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_FORMAT_ID", lngSearchId) == false)
                if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId) == false)
                    BtnAdd.Select();
                //
                chkInactive.Visible = false;
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
                //-------------------------------------------
                lblSearchMode.Text = J_Mode.Searching;
                //-------------------------------------------
                if (ValidateFields() == false) return;
                //-------------------------------------------
                grpSort.Visible = false;
                grpSearch.Visible = true;
                //-------------------------------------------
                txtCompanySearch.Select();
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
                if (txtCompanySearch.Text.Trim() != "")
                    strCheckFields = "WHERE MST_COMPANY.COMPANY_NAME + MST_COMPANY.TAN_NO like '%" + cmnService.J_ReplaceQuote(txtCompanySearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- DEDUCTEE NAME
                //-------------------------------------------------------------------
                if (txtDescriptionSearch.Text.Trim() != "")
                    if (strCheckFields == "")
                        strCheckFields = "WHERE SYS_EMAIL_FORMAT.EMAIL_FORMAT_DESC like '" + cmnService.J_ReplaceQuote(txtDescriptionSearch.Text.Trim().ToUpper()) + "%' ";
                    else
                        strCheckFields = strCheckFields + " AND SYS_EMAIL_FORMAT.EMAIL_FORMAT_DESC like '" + cmnService.J_ReplaceQuote(txtDescriptionSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- DEDUCTEE CODE
                //-------------------------------------------------------------------
                if (cmbCertificateSearch.SelectedIndex > 0)
                    if (strCheckFields == "")
                        strCheckFields = "WHERE MST_EMAIL_CERTIFICATES.CERTIFICATE_DESC = '" + cmnService.J_ReplaceQuote(cmbCertificateSearch.Text) + "' ";
                    else
                        strCheckFields = strCheckFields + " AND MST_EMAIL_CERTIFICATES.CERTIFICATE_DESC = '" + cmnService.J_ReplaceQuote(cmbCertificateSearch.Text) + "' ";
                //----------------------------------------------------------------------
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                //----------------------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                //if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_FORMAT_ID", lngSearchId) == false)
                if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId) == false)
                {
                    txtCompanySearch.Select();
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
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_FORMAT_ID", lngSearchId);
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
                //if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
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
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);
                if (dsetGridClone == null) return;
                //-----------------------------------------------------------
                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_FORMAT_ID", lngSearchId);
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

        #region ViewGrid_Click

        private void ViewGrid_Click(object sender, EventArgs e)
        {
            //if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
            //{
            //    BtnAdd.Focus();
            //    return;
            //}
            //lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));

            //ViewGrid.Select(ViewGrid.CurrentRowIndex);
            //ViewGrid.Select();
            //ViewGrid.Focus();
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
                //if (ViewGrid.CurrentRowIndex == -1) return;
                //lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
                //if (e.KeyCode == Keys.Enter) BtnEdit_Click(sender, e);
                //if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);

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
            //lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
        }
        #endregion

        #region ViewGrid_MouseClick
        private void ViewGrid_MouseClick(object sender, MouseEventArgs e)
        {
            ViewGrid_Click(sender, e);
        }
        #endregion


        //--
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
            if (dgvGrid.CurrentRow != null)
                lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
        }
        #endregion

        #region dgvGrid_MouseClick
        private void dgvGrid_MouseClick(object sender, MouseEventArgs e)
        {
            dgvGrid_Click(sender, e);
        }
        #endregion
        //--

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
                //dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvGrid, strSQL, strMatrix);
                if (dsetGridClone == null) return;
                //-----------------------------------------------------------
                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_FORMAT_ID", lngSearchId);
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);

            }
        }
        #endregion

        #region lnkViewHTML_LinkClicked
        private void lnkViewHTML_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (chkHtmlBody.Checked == false)
            {
                cmnService.J_UserMessage("Please check the 'HTML Body'");
                return;
            }
            //--
            if (txtBody.Text.Trim() != "")
            {
                //HtmlDocument doc = new HtmlDocument();
                //doc.LoadHtml(html);

                //if (doc.ParseErrors.Count() > 0)
                //{
                //    //Invalid HTML
                //}
                //--
                string filename = string.Format(@"{0}\{1}",
                        System.IO.Path.GetTempPath(),
                        "EmailFormatSample.htm");
                //--                
                System.IO.File.WriteAllText(filename, txtBody.Text);
                System.Diagnostics.Process.Start(filename);
            }
            else
                cmnService.J_UserMessage("No HTML found");
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
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME " + cmnService.J_ConcateSQLSyntaxOperator() + " '[' " + cmnService.J_ConcateSQLSyntaxOperator() + " TAN_NO " + cmnService.J_ConcateSQLSyntaxOperator() + " ']' " +
                "      FROM   MST_COMPANY " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY COMPANY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompanyName) == false) return;
            //--
            strSQL = " SELECT EMAIL_CERTIFICATE_ID," +
                "             CERTIFICATE_DESC " +
                "      FROM   MST_EMAIL_CERTIFICATES " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY EMAIL_CERTIFICATE_ID ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCertificateID) == false) return;
            //--
            txtEmailDescription.Text = "";
            txtEmailSubject.Text = "";
            txtBody.Text = "";
            //--------------------
            txtCompanySearch.Text = "";
            txtDescriptionSearch.Text = "";
            //
            strSQL = " SELECT EMAIL_CERTIFICATE_ID," +
                "             CERTIFICATE_DESC " +
                "      FROM   MST_EMAIL_CERTIFICATES " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY EMAIL_CERTIFICATE_ID ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCertificateSearch) == false) return;
            //--
            
            //
            chkInactive.Checked = false;
            chkHtmlBody.Checked = false;    
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
                strSQL = "SELECT  SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID        AS EMAIL_FORMAT_ID," +
                    "             SYS_EMAIL_FORMAT.EMAIL_FORMAT_DESC      AS EMAIL_FORMAT_DESC," +
                    "             SYS_EMAIL_FORMAT.COMPANY_ID             AS COMPANY_ID," +
                    "             MST_COMPANY.COMPANY_NAME " + cmnService.J_ConcateSQLSyntaxOperator() + " '[' " + cmnService.J_ConcateSQLSyntaxOperator() + " MST_COMPANY.TAN_NO " + cmnService.J_ConcateSQLSyntaxOperator() + " ']' AS COMPANY," +
                    "             SYS_EMAIL_FORMAT.EMAIL_CERTIFICATE_ID   AS EMAIL_CERTIFICATE_ID," +
                    "             MST_EMAIL_CERTIFICATES.CERTIFICATE_DESC AS CERTIFICATE_DESC," +
                    "             SYS_EMAIL_FORMAT.EMAIL_SUBJECT          AS EMAIL_SUBJECT," +
                    "             SYS_EMAIL_FORMAT.EMAIL_BODY             AS EMAIL_BODY," +
                    "             SYS_EMAIL_FORMAT.HTML_FORMAT            AS HTML_FORMAT," +
                    "             SYS_EMAIL_FORMAT.INACTIVE_FLAG          AS INACTIVE_FLAG " +
                    "       FROM ((SYS_EMAIL_FORMAT LEFT JOIN MST_COMPANY " +
                    "       ON     SYS_EMAIL_FORMAT.COMPANY_ID = MST_COMPANY.COMPANY_ID) " +
                    "              INNER JOIN MST_EMAIL_CERTIFICATES " +
                    "       ON     SYS_EMAIL_FORMAT.EMAIL_CERTIFICATE_ID = MST_EMAIL_CERTIFICATES.EMAIL_CERTIFICATE_ID) " +
                    "     WHERE  SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID = " + Id + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;
                    //--
                    if(Convert.ToString(drdShowRecord["COMPANY"]) == "[]")
                        cmbCompanyName.Text = "";
                    else
                        cmbCompanyName.Text = Convert.ToString(drdShowRecord["COMPANY"]);
                    //--
                    cmbCertificateID.Text = Convert.ToString(drdShowRecord["CERTIFICATE_DESC"]);
                    //--
                    txtEmailDescription.Text = Convert.ToString(drdShowRecord["EMAIL_FORMAT_DESC"]);
                    txtEmailSubject.Text = Convert.ToString(drdShowRecord["EMAIL_SUBJECT"]);
                    txtBody.Text = Convert.ToString(drdShowRecord["EMAIL_BODY"]);
                    //
                    if (Convert.ToString(drdShowRecord["HTML_FORMAT"]) == "1")
                        chkHtmlBody.Checked = true;
                    else
                        chkHtmlBody.Checked = false;
                    //
                    if (Convert.ToString(drdShowRecord["INACTIVE_FLAG"]) == "1")
                        chkInactive.Checked = true;
                    else
                        chkInactive.Checked = false;
                    //
                    //
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    //--
                    cmbCompanyName.Select();
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
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);
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
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_FORMAT_ID", lngSearchId);
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
                            //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_FORMAT_ID", lngSearchId);
                            dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtCompanySearch.Text.Trim() == "" &&
                            txtDescriptionSearch.Text.Trim() == "" &&
                            cmbCertificateSearch.SelectedIndex == 0)
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtCompanySearch.Select();
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
                    if (cmbCompanyName.SelectedIndex < 0)
                    {
                        cmnService.J_UserMessage("Company - Cannot be Blank");
                        cmbCompanyName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    if (cmbCertificateID.SelectedIndex < 0)
                    {
                        cmnService.J_UserMessage("Certificate - Cannot be Blank");
                        cmbCertificateID.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PAN
                    //-----------------------------------------------------------------------
                    if (txtEmailDescription.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Description - Cannot be Blank");
                        txtEmailDescription.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- DEDUCTEE NAME
                    //-----------------------------------------------------------------------
                    if (txtBody.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Email Body - Cannot be Blank");
                        txtBody.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- DEDUCTEE NAME & PAN
                    //-----------------------------------------------------------------------
                    strSQL = "SELECT EMAIL_FORMAT_ID " +
                        "     FROM   SYS_EMAIL_FORMAT " +
                        "     WHERE  EMAIL_FORMAT_DESC ='" + cmnService.J_ReplaceQuote(txtEmailDescription.Text) + "'";
                    if (lblMode.Text == J_Mode.Edit)
                        strSQL = strSQL + "AND EMAIL_FORMAT_ID <> " + lngSearchId;
                    //lngDeducteeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //--
                    if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                    {
                        cmnService.J_UserMessage("The Email description exists");
                        txtEmailDescription.Select();
                        return false;
                    }
                    //                    
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
                int intHTML = 0;
                if (chkHtmlBody.Checked == true)
                    intHTML = 1;                
                //--------------------------------------------
                switch (lblMode.Text)
                {
                    #region ADD
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
                        //if (cmnService.J_SaveConfirmationMessage(ref cmbDeducteeCode) == true)
                        //{
                        //    dmlService.J_Rollback();
                        //    return;
                        //}
                        //-----------------------------------------------------------
                        //-----------------------------------------------------------
                        strSQL = "INSERT INTO SYS_EMAIL_FORMAT (" +
                                 "            EMAIL_FORMAT_DESC," +
                                 "            COMPANY_ID," +
                                 "            EMAIL_CERTIFICATE_ID," +
                                 "            EMAIL_SUBJECT," +
                                 "            EMAIL_BODY," +
                                 "            HTML_FORMAT) " +
                                 "     VALUES('" + cmnService.J_ReplaceQuote(txtEmailDescription.Text) + "'," +
                                 "             " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "," +
                                 "             " + Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) + "," +
                                 "            '" + cmnService.J_ReplaceQuote(txtEmailSubject.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtBody.Text.Trim()) + "'," +
                                 "             " + intHTML + ")";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            cmbCompanyName.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        lngSearchId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "SYS_EMAIL_FORMAT", "EMAIL_FORMAT_ID");
                        if (lngSearchId == 0)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        //-----------------------------------------------------------
                        ////Added by Indrajit on 11-02-2013
                        ////-----------------------------------------------------------------------
                        ////-- DEDUCTEE NAME + PAN
                        ////-----------------------------------------------------------------------
                        //strSQL = "SELECT COUNT(EMAIL_FORMAT_ID) " +
                        //    "     FROM   SYS_EMAIL_FORMAT " +
                        //    "     WHERE  DEDUCTEE_NAME ='" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "'" +
                        //    "     AND    DEDUCTEE_PAN  ='" + cmnService.J_ReplaceQuote(txtPAN.Text) + "'" +
                        //    "     AND    EMAIL_FORMAT_ID  <> " + lngSearchId;
                        ////--
                        //if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        //{
                        //    cmnService.J_UserMessage("Deductee Name exists");
                        //    txtDeducteeName.Select();
                        //    dmlService.J_Rollback();
                        //    return;
                        //}
                        //End of add zone  ------------------------------------------

                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        //-----------------------------------------------------------
                        ClearControls();
                        //-----------------------------------------------------------
                        cmbCompanyName.Select();
                        //-----------------------------------------------------------
                        break;
                    #endregion

                    #region EDIT
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
                        if (chkInactive.Checked == true)
                        {
                            intHideDeductee = 1;
                        }                        
                        ////-----------------------------------------------------------
                        //if (cmnService.J_SaveConfirmationMessage(ref cmbDeducteeCode) == true)
                        //{
                        //    dmlService.J_Rollback();
                        //    return;
                        //}
                        //-----------------------------------------------------------
                        //Added by Indrajit on 11-02-2013
                        //if (Check_Record(lngSearchId) == 0)
                        //{
                        //    dmlService.J_Rollback();
                        //    return;
                        //}
                        //----------
                        //-----------------------------------------------------------
                        strSQL = "UPDATE SYS_EMAIL_FORMAT " +
                                 "SET    EMAIL_FORMAT_DESC    ='" + cmnService.J_ReplaceQuote(txtEmailDescription.Text.Trim()) + "'," +
                                 "       COMPANY_ID           = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "," +
                                 "       EMAIL_CERTIFICATE_ID = " + Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) + "," +
                                 "       EMAIL_SUBJECT        ='" + cmnService.J_ReplaceQuote(txtEmailSubject.Text.Trim()) + "'," +
                                 "       EMAIL_BODY           ='" + cmnService.J_ReplaceQuote(txtBody.Text.Trim()) + "'," +
                                 "       HTML_FORMAT          = " + intHTML + "," +
                                 "       INACTIVE_FLAG        = " + intHideDeductee + " " +
                                 "WHERE  EMAIL_FORMAT_ID      = " + lngSearchId + "";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            cmbCompanyName.Select();
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
                        //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        dgvGrid.Visible = true;
                        //
                        chkInactive.Visible = false;                
                        //-----------------------------------------------------------
                        //DisableControls();
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //-----------------------------------------------------------
                        //dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMAIL_FORMAT_ID", lngSearchId);
                        dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                        break;
                    #endregion

                    #region DELETE
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
                        strSQL = "SELECT EMAIL_HEADER_LOG_ID " +
                            "     FROM   TRN_EMAIL_HEADER_LOG " +
                            "     WHERE  EMAIL_FORMAT_ID      = " + lngSearchId + " ";
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        {
                            cmnService.J_UserMessage("The Email Format cannot be deleted");
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
                        strSQL = "DELETE FROM SYS_EMAIL_FORMAT WHERE EMAIL_FORMAT_ID =  " + lngSearchId + "";
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
                        //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        dgvGrid.Visible = true;
                        //-----------------------------------------------------------
                        //dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMAIL_FORMAT_ID", lngSearchId);
                        dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                        break;
                    #endregion
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
        //private long Check_Record(long lngSrchId)
        //{
        //    //if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "SYS_EMAIL_FORMAT", "EMAIL_FORMAT_ID", lngSrchId) == true) return lngSrchId;

        //    //cmnService.J_UserMessage("Record has been deleted.");
        //    //lngSrchId = 0;
        //    ////-------------------------------------------
        //    //lblMode.Text = J_Mode.View;
        //    //cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
        //    ////-------------------------------------------
        //    //ControlVisible(false);
        //    //ClearControls();					//Clear all the Controls
        //    ////-------------------------------------------
        //    //strSQL = strQuery + "order by " + strOrderBy;
        //    ////-------------------------------------------
        //    //if (dsetGridClone != null) dsetGridClone.Clear();
        //    //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
        //    //if (dsetGridClone == null) return 0;
        //    ////-------------------------------------------
        //    //BtnAdd.Select();
        //    ////-------------------------------------------                            
        //    //return 0;

        //}
        #endregion

        #endregion

        private void pctManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0086", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}

