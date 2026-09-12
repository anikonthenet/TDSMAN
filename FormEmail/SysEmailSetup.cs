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
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Data.SqlClient;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormTrn;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;
// Add EASendMail namespace
//using EASendMail;

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

#endregion


namespace TDSMAN.FormEmail
{
    public partial class SysEmailSetup : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public SysEmailSetup()
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
            //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        }
        #endregion

        #region SysEmailSetup_Load

        private void SysEmailSetup_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            try
            {
                //-----------------------------------------------------------
                GC.Collect();
                //
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                dgvGrid.Visible = true;
                //-----------------------------------------------------------
                //DisableControls();
                //-----------------------------------------------------------
                //ViewGrid.Height = 518;
                ViewGrid.Height = 0;
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
                lblTitle.Text = "Content Email";
                //-----------------------------------------------------------
                if (TDSMAN.Classes.TDSMAN.T_HIDE_SMTP_PASSWORD_FLAG == true)
                    txtSMTPPassword.UseSystemPasswordChar=true;
                //--
                //
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
            //--
            string[,] strSetupInactiveMatrix = {{"SYS_EMAIL_SETUP.INACTIVE_FLAG = 0", "F", "", "T"},
                                               {"SYS_EMAIL_SETUP.INACTIVE_FLAG = 1", "F", "Yes", "T"}};
            //
            string[,] strMatrix1 = {{"SetupID", "0", "", "Right", "", "F", ""},
						            {"Setup Description", "450", "", "", "", "", "T"},
						            {"From Email ID", "390", "", "", "", "", "T"},
                                    {"Inactive", "100", "", "", "", "", "T"}};            
            //-----------------------------------------------------------
            strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            strOrderBy = "SYS_EMAIL_SETUP.EMAIL_SETUP_ID DESC";
            strQuery = "SELECT SYS_EMAIL_SETUP.EMAIL_SETUP_ID   AS EMAIL_SETUP_ID," +
                "              SYS_EMAIL_SETUP.SETUP_DESC  AS SETUP_DESC," +
                "              SYS_EMAIL_SETUP.FROM_EMAIL_ID AS FROM_EMAIL_ID," +
                "             " + cmnService.J_SQLDBFormat(strSetupInactiveMatrix, J_SQLColFormat.Case_End) + " AS INACTIVE " +
                "       FROM   SYS_EMAIL_SETUP," +
                "              MST_COMPANY " +
                "       WHERE  SYS_EMAIL_SETUP.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                "       AND    SYS_EMAIL_SETUP.COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " ";
            //-----------------------------------------------------------
            strSQL = strQuery + " ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            //if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid   
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
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
                txtEmailDescription.Select();
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
                    //Added by Indrajit on 18-02-2013
                    if (Check_Record(lngSearchId) == 0) return;
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
                        //dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
                        dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                    }
                    //
                    cmbCompanyName.Enabled = false;
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    dgvGrid.Visible = false;
                    //
                    chkInactiveSetup.Visible = true;
                    //--------------------------------------------------
                    strCheckFields = "";
                    //--------------------------------------------------
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
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
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //
                cmbCompanyName.Enabled = true;
                //-------------------------------------------
                //if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId) == false)
                if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId) == false)
                    BtnAdd.Select();
                //
                chkInactiveSetup.Visible = false;
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
                txtEmailDescriptionSearch.Select();
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
                if (txtEmailDescriptionSearch.Text.Trim() != "")
                    strCheckFields = "AND SYS_EMAIL_SETUP.SETUP_DESC like '%" + cmnService.J_ReplaceQuote(txtEmailDescriptionSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- DEDUCTEE NAME
                //-------------------------------------------------------------------
                if (txtFromEmailIDSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + " AND SYS_EMAIL_SETUP.FROM_EMAIL_ID like '%" + cmnService.J_ReplaceQuote(txtFromEmailIDSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                ////Added by Shrey Kejriwal on 17/08/2011
                ////-- COMPANY TAN
                ////-------------------------------------------------------------------
                //if (txtTANSearch.Text.Trim() != "")
                //    strCheckFields = strCheckFields + " AND MST_COMPANY.TAN_NO like '" + cmnService.J_ReplaceQuote(txtTANSearch.Text.Trim().ToUpper()) + "%' ";
                
                //----------------------------------------------------------------------
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                //----------------------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                //if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId) == false)
                if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId) == false)
                {
                    txtEmailDescriptionSearch.Select();
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
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
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
            //if (ViewGrid.CurrentRowIndex >= 0)
            if (dgvGrid.CurrentRow != null)
            {
                lblMode.Text = J_Mode.Delete;
                Insert_Update_Delete_Data();
            }
            else
            {
                cmnService.J_UserMessage(J_Msg.DataNotFound);
                if (dsetGridClone == null) return;
                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
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
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-----------------------------------------------------------
                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
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
            if(dgvGrid.CurrentRow!= null)
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
                //dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand,ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-----------------------------------------------------------
                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);

            }
        }
        #endregion

        #region btnTestEmail_Click
        private void btnTestEmail_Click(object sender, EventArgs e)
        {
            if (ValidateFields() == false) return;
            //--
            //if (txtSMTPHost.Text.ToLower().Contains("office365"))
            //{
            if (txtSMTPPort.Text == "465")
            {
                if (SendEmailImplicitSSL(txtFromEmailId.Text,
                        txtFromDisplayName.Text,
                            txtEmailReplyTo.Text,
                                txtEmailCC.Text,
                                    txtEmailCC2.Text,
                                        txtEmailBCC.Text,
                                            txtEmailBCC2.Text,
                                                "This is a mail to test email setup of TDSMAN",
                                                    "If you get this mail, it is implied that the setup you entered at TDSMAN is valid.", txtFromEmailId.Text) == true)
                {
                    cmnService.J_UserMessage("Email send successfully... Please check inbox...");
                }
            }
            else
            {
                if (SendEmailTLS(txtFromEmailId.Text,
                        txtFromDisplayName.Text,
                            txtEmailReplyTo.Text,
                                txtEmailCC.Text,
                                    txtEmailCC2.Text,
                                        txtEmailBCC.Text,
                                            txtEmailBCC2.Text,
                                                "This is a mail to test email setup of TDSMAN",
                                                    "If you get this mail, it is implied that the setup you entered at TDSMAN is valid.", txtFromEmailId.Text) == true)
                {
                    cmnService.J_UserMessage("Email send successfully... Please check inbox...");
                }
            }
            //}
            //else
            //{
            //    if (SendEmail(txtFromEmailId.Text,
            //                txtFromDisplayName.Text,
            //                    txtEmailReplyTo.Text,
            //                        txtEmailCC.Text,
            //                            txtEmailCC2.Text,
            //                                txtEmailBCC.Text,
            //                                    txtEmailBCC2.Text,
            //                                        "This is a mail to test email setup of TDSMAN",
            //                                            "If you get this mail, it is implied that the setup you entered at TDSMAN is valid.", txtFromEmailId.Text) == true)
            //    {
            //        cmnService.J_UserMessage("Email send successfully... Please check inbox...");
            //    }
            //}            
        }
        #endregion 

        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            FormTrn.TrnTANSearch Tan = new TrnTANSearch("SysEmailSetup");
            Tan.ShowDialog();
            //--------------
            if (TDSMAN.Classes.TDSMAN.T_pTAN != "")
                cmbCompanyName.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #endregion 

        #region User Define Functions

        #region NumericControl_KeyPress
        private void NumericControl_KeyPress(object sender, KeyPressEventArgs e, int MaxLength)
        {
            TextBox txtNumeric = (TextBox)sender;
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N," + MaxLength + ",0", txtNumeric, "") == false)
                e.Handled = true;
        }
        #endregion

        #region NumericCurrencyControl_Leave
        private void NumericCurrencyControl_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "." || txtBox.Text == "") txtBox.Text = "0.00";
            txtBox.Text = string.Format("{0:0.00}", Convert.ToDouble(cmnService.J_NumericData(txtBox)));
        }
        #endregion

        #region NumericControl_Leave
        private void NumericControl_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "") txtBox.Text = "0";
        }
        #endregion        

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            //
            txtEmailDescription.Text = "";
            txtFromEmailId.Text = "";
            txtFromDisplayName.Text = "";
            txtSMTPUserName.Text = "";
            txtSMTPPassword.Text = "";
            txtSMTPPort.Text = "0";
            //--
            string[] strSSL = { "Yes", "No" };
            dmlService.J_PopulateComboBox(strSSL, ref cmbSSL, 2, J_ComboBoxSelectedIndex.YES);  
            //--
            txtSMTPHost.Text = "";
            //txtEmailSubject.Text = "";
            txtEmailReplyTo.Text = "";
            txtEmailCC.Text = "";
            txtEmailCC2.Text = "";
            txtEmailBCC.Text = "";
            txtEmailBCC2.Text = "";
            chkInactiveSetup.Checked = false;
            chkServerAuthentication.Checked = false;
            chkDisableTLSSSL.Checked = false;
            //--
            //--------------------
            txtEmailDescriptionSearch.Text = "";
            txtFromEmailIDSearch.Text = "";                   
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
                //string[,] strShowHelpEmployeeMatrix = {{"MST_EMPLOYEE.CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
                //                                       {"MST_EMPLOYEE.CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
                //                                       {"MST_EMPLOYEE.CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
                //                                       {"MST_EMPLOYEE.CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
                //                                       {"MST_EMPLOYEE.CATEGORY = ''", "F", "", "T"}};
                //
                strSQL = "SELECT  SYS_EMAIL_SETUP.EMAIL_SETUP_ID AS EMAIL_SETUP_ID," +
                    "             SYS_EMAIL_SETUP.COMPANY_ID     AS COMPANY_ID," +
                    "             MST_COMPANY.COMPANY_NAME       AS COMPANY_NAME," +
                    "             SYS_EMAIL_SETUP.SETUP_DESC     AS SETUP_DESC," +
                    "             SYS_EMAIL_SETUP.FROM_EMAIL_ID  AS FROM_EMAIL_ID," +
                    "             SYS_EMAIL_SETUP.FROM_NAME      AS FROM_NAME," +
                    "             SYS_EMAIL_SETUP.SMTP_USERNAME  AS SMTP_USERNAME," +
                    "             SYS_EMAIL_SETUP.SMTP_PASSWORD  AS SMTP_PASSWORD," +
                    "             SYS_EMAIL_SETUP.SMTP_PORT      AS SMTP_PORT," +
                    "             SYS_EMAIL_SETUP.SMTP_HOST      AS SMTP_HOST," +
                    "             SYS_EMAIL_SETUP.SMTP_SSL       AS SMTP_SSL," +
                    "             SYS_EMAIL_SETUP.EMAIL_REPLY_TO AS EMAIL_REPLY_TO," +
                    "             SYS_EMAIL_SETUP.EMAIL_CC       AS EMAIL_CC," +
                    "             SYS_EMAIL_SETUP.EMAIL_CC2      AS EMAIL_CC2," +
                    "             SYS_EMAIL_SETUP.EMAIL_BCC      AS EMAIL_BCC," +
                    "             SYS_EMAIL_SETUP.EMAIL_BCC2     AS EMAIL_BCC2," +
                    "             SYS_EMAIL_SETUP.INACTIVE_FLAG AS INACTIVE_FLAG," +
                    "             SYS_EMAIL_SETUP.SERVER_AUTHENTICATION AS SERVER_AUTHENTICATION," +
                    "             SYS_EMAIL_SETUP.DISABLE_SSL_TLS       AS DISABLE_SSL_TLS " +
                    "     FROM    SYS_EMAIL_SETUP," +
                    "             MST_COMPANY " +
                    "     WHERE   SYS_EMAIL_SETUP.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                    "     AND     SYS_EMAIL_SETUP.EMAIL_SETUP_ID = " + Id + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;
                    //--
                    txtEmailDescription.Text = Convert.ToString(drdShowRecord["SETUP_DESC"]);
                    txtFromEmailId.Text = Convert.ToString(drdShowRecord["FROM_EMAIL_ID"]);
                    txtFromDisplayName.Text = Convert.ToString(drdShowRecord["FROM_NAME"]);
                    txtSMTPUserName.Text = Convert.ToString(drdShowRecord["SMTP_USERNAME"]);
                    txtSMTPPassword.Text = Convert.ToString(drdShowRecord["SMTP_PASSWORD"]);
                    txtSMTPPort.Text = Convert.ToString(drdShowRecord["SMTP_PORT"]);
                    txtSMTPHost.Text = Convert.ToString(drdShowRecord["SMTP_HOST"]);
                    //
                    if (Convert.ToString(drdShowRecord["SMTP_SSL"]) == "1")
                        cmbSSL.Text = T_YES_NO.YES;
                    else
                        cmbSSL.Text = T_YES_NO.NO;
                    //
                    txtEmailReplyTo.Text = Convert.ToString(drdShowRecord["EMAIL_REPLY_TO"]);
                    txtEmailCC.Text = Convert.ToString(drdShowRecord["EMAIL_CC"]);
                    txtEmailCC2.Text = Convert.ToString(drdShowRecord["EMAIL_CC2"]);
                    txtEmailBCC.Text = Convert.ToString(drdShowRecord["EMAIL_BCC"]);
                    txtEmailBCC2.Text = Convert.ToString(drdShowRecord["EMAIL_BCC2"]);
                    //--

                    intCompanyIndex = Convert.ToInt32(Convert.ToString(drdShowRecord["COMPANY_ID"]));
                    //
                    if (Convert.ToString(drdShowRecord["INACTIVE_FLAG"]) == "1")
                    {
                        chkInactiveSetup.Visible = true;
                        chkInactiveSetup.Checked = true;
                    }
                    else
                    {
                        chkInactiveSetup.Visible = false;
                    }
                    //
                    if (Convert.ToString(drdShowRecord["SERVER_AUTHENTICATION"]) == "1")
                    {
                        //chkServerAuthentication.Visible = true;
                        chkServerAuthentication.Checked = true;
                    }
                    else
                    {
                        chkServerAuthentication.Checked = false;
                    }
                    //
                    if (Convert.ToString(drdShowRecord["DISABLE_SSL_TLS"]) == "1")
                    {
                        chkDisableTLSSSL.Checked = true;
                    }
                    else
                    {
                        chkDisableTLSSSL.Checked = false;
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
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
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
                        if (dgvGrid.CurrentRow == null)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            if (dsetGridClone == null) return false;
                            //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
                            dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtEmailDescriptionSearch.Text.Trim()          == ""  &&
                            txtFromEmailIDSearch.Text.Trim() == "" )
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtEmailDescriptionSearch.Select();
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
                    //-- EmailDescriptio
                    //-----------------------------------------------------------------------
                    if (txtEmailDescription.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Email Description - Cannot be Blank");
                        txtEmailDescription.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- FromEmailId
                    //-----------------------------------------------------------------------
                    if (txtFromEmailId.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("From Email ID - Cannot be Blank");
                        txtFromEmailId.Select();
                        return false;
                    }
                    else
                    {
                        if (TdsMan.T_CheckEmailFormat(txtFromEmailId.Text) == false)
                        {
                            cmnService.J_UserMessage("From Email ID - Wrong format");                        
                            txtFromEmailId.Select();
                            return false;
                        }              
                    }
                    //-----------------------------------------------------------------------
                    //-- SMTPUserName
                    //-----------------------------------------------------------------------
                    if (txtSMTPUserName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("SMTP UserName - Cannot be Blank");
                        txtSMTPUserName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- SMTPPassword
                    //-----------------------------------------------------------------------
                    if (txtSMTPPassword.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("SMTP Password - Cannot be Blank");
                        txtSMTPPassword.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- SMTP Port
                    //-----------------------------------------------------------------------
                    if (txtSMTPPort.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("SMTP Port - Cannot be Blank");
                        txtSMTPPort.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- SMTP Host
                    //-----------------------------------------------------------------------
                    if (cmbSSL.SelectedIndex == 0)
                    {
                        cmnService.J_UserMessage("SSL - Cannot be Blank");
                        cmbSSL.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- SMTP Host
                    //-----------------------------------------------------------------------
                    if (txtSMTPHost.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("SMTP Host - Cannot be Blank");
                        txtSMTPHost.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- EMPLOYEE NAME & PAN
                    //-----------------------------------------------------------------------
                    strSQL = "SELECT EMAIL_SETUP_ID " +
                        "     FROM   SYS_EMAIL_SETUP " +
                        "     WHERE  SETUP_DESC ='" + cmnService.J_ReplaceQuote(txtEmailDescription.Text) + "'" +
                        "     AND    COMPANY_ID    = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "";
                    if (lblMode.Text == J_Mode.Edit)
                        strSQL = strSQL + "AND EMAIL_SETUP_ID <> " + lngSearchId;
                    //lngDeducteeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //--
                    if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                    {
                        cmnService.J_UserMessage("This Email description exists for the selected Company");
                        txtEmailDescription.Select();
                        return false;
                    }
                    //--
                    if (txtEmailReplyTo.Text.Trim() != "")
                    {
                        if (TdsMan.T_CheckEmailFormat(txtEmailReplyTo.Text) == false)
                        {
                            cmnService.J_UserMessage("Reply Email ID - Wrong format");
                            txtEmailReplyTo.Select();
                            return false;
                        }
                    }
                    //--
                    //--
                    if (txtEmailCC.Text.Trim() != "")
                    {
                        if (TdsMan.T_CheckEmailFormat(txtEmailCC.Text) == false)
                        {
                            cmnService.J_UserMessage("CC Email ID - Wrong format");
                            txtEmailCC.Select();
                            return false;
                        }
                    }
                    //--
                    //--
                    if (txtEmailCC2.Text.Trim() != "")
                    {
                        if (TdsMan.T_CheckEmailFormat(txtEmailCC2.Text) == false)
                        {
                            cmnService.J_UserMessage("CC2 Email ID - Wrong format");
                            txtEmailCC2.Select();
                            return false;
                        }
                    }
                    //--
                    //--
                    if (txtEmailBCC.Text.Trim() != "")
                    {
                        if (TdsMan.T_CheckEmailFormat(txtEmailBCC.Text) == false)
                        {
                            cmnService.J_UserMessage("BCC Email ID - Wrong format");
                            txtEmailBCC.Select();
                            return false;
                        }
                    }
                    //--
                    //--
                    if (txtEmailBCC2.Text.Trim() != "")
                    {
                        if (TdsMan.T_CheckEmailFormat(txtEmailBCC2.Text) == false)
                        {
                            cmnService.J_UserMessage("BCC2 Email ID - Wrong format");
                            txtEmailBCC2.Select();
                            return false;
                        }
                    }
                    //--
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
                int intSSL = 0;
                if (cmbSSL.Text == T_YES_NO.YES)
                    intSSL = 1;
                //--
                int intServerAuthentication=0;
                if(chkServerAuthentication.Checked==true)
                    intServerAuthentication = 1;
                //--
                int intDisbaleSSLTLS = 0;
                if(chkDisableTLSSSL.Checked==true)
                    intDisbaleSSLTLS = 1;
                //--------------------------------------------
                switch (lblMode.Text)
                {
                    #region ADD
                    case J_Mode.Add:
                        //*****  For Insert
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;
                        //-----------------------------------------------------------
                        if (cmnService.J_SaveConfirmationMessage(ref cmbCompanyName) == true) return;
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        strSQL = "INSERT INTO SYS_EMAIL_SETUP(" +
                                "            SETUP_DESC," +
                                "            COMPANY_ID," +
                                "            FROM_EMAIL_ID," +
                                "            FROM_NAME," +
                                "            SMTP_USERNAME," +
                                "            SMTP_PASSWORD," +
                                "            SMTP_PORT," +
                                "            SMTP_HOST," +
                                "            SMTP_SSL," +
                                "            EMAIL_REPLY_TO," +
                                "            EMAIL_CC," +
                                "            EMAIL_CC2," +
                                "            EMAIL_BCC," +
                                "            EMAIL_BCC2," +
                                "            SERVER_AUTHENTICATION," +
                                "            DISABLE_SSL_TLS) " +
                                "     VALUES('" + cmnService.J_ReplaceQuote(txtEmailDescription.Text.Trim()) + "'," +
                                "             " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "," +
                                "            '" + cmnService.J_ReplaceQuote(cmnService.J_ReplaceQuote(txtFromEmailId.Text.Trim())) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(txtFromDisplayName.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(txtSMTPUserName.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(txtSMTPPassword.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(txtSMTPPort.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(txtSMTPHost.Text.Trim()) + "'," +
                                "             " + intSSL + "," +
                                "            '" + cmnService.J_ReplaceQuote(txtEmailReplyTo.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(txtEmailCC.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(txtEmailCC2.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(txtEmailBCC.Text.Trim()) + "'," +
                                "            '" + cmnService.J_ReplaceQuote(txtEmailBCC2.Text) + "'," +
                                "             " + intServerAuthentication + "," +
                                "             " + intDisbaleSSLTLS + ")";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            cmbCompanyName.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        lngSearchId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "SYS_EMAIL_SETUP", "EMAIL_SETUP_ID");
                        if (lngSearchId == 0)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        //-----------------------------------------------------------
                        #region COMMENT
                        ////Added by Indrajit on 11-02-2013
                        ////-----------------------------------------------------------------------
                        ////-- EMPLOYEE NAME
                        ////-----------------------------------------------------------------------
                        //strSQL = "SELECT COUNT(EMPLOYEE_ID) " +
                        //    "     FROM   MST_EMPLOYEE " +
                        //    "     WHERE  EMPLOYEE_NAME ='" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "'" +
                        //    "     AND    EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "'" +
                        //    "     AND    COMPANY_ID    = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " " +
                        //    "     AND    EMPLOYEE_ID  <> " + lngSearchId;
                        ////--
                        //if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        //{
                        //    cmnService.J_UserMessage("Employee Name exists");
                        //    txtEmployeeName.Select();
                        //    dmlService.J_Rollback();
                        //    return;
                        //}
                        //End of add zone  ------------------------------------------
                        #endregion
                        //--
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        //-----------------------------------------------------------
                        ClearControls();
                        //-----------------------------------------------------------
                        txtEmailDescription.Select();
                        //-----------------------------------------------------------
                        break;
                    #endregion

                    #region EDIT
                    case J_Mode.Edit:
                        //*****  For Modify
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;
                        //-----------------------------------------------------------
                        #region COMMENT
                        ////if (cmnService.J_SaveConfirmationMessage(ref cmbEmployeeCategory) == true) return;
                        ////-----------------------------------------------------------
                        ////-----------------------------------------------------------
                        ///*Added by Shrey Kejriwal on 17/08/2011 to check if the employee's company is changed 
                        // and the same is used in the earlier company's transaction then user is not allowed to update*/

                        ////--Checking Employee records in transaction table
                        //strSQL = "SELECT COUNT(*) " +
                        //         "FROM   TRN_BASIC_INFO, " +
                        //         "       TRN_DEDUCTEE_DETAILS " +
                        //         "WHERE  TRN_BASIC_INFO.BASIC_INFO_ID  = TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID " +
                        //         "AND    TRN_BASIC_INFO.FORM_NO        = '" + T_FormNo.F24Q + "' " +
                        //         "AND    TRN_BASIC_INFO.COMPANY_ID     <> " + Support.GetItemData(cmbCompanyName,cmbCompanyName.SelectedIndex) + " " +
                        //         "AND    TRN_DEDUCTEE_DETAILS.PARTY_ID = " + lngSearchId + " ";

                        //intCountRecords = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                        ////Checking employee records in salary detail table

                        //strSQL = "SELECT COUNT(*) " +
                        //         "FROM   TRN_BASIC_INFO, " +
                        //         "       TRN_SALARY_DETAILS " +
                        //         "WHERE  TRN_BASIC_INFO.BASIC_INFO_ID   = TRN_SALARY_DETAILS.BASIC_INFO_ID " +
                        //         "AND    TRN_BASIC_INFO.COMPANY_ID      <> " + Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex) + " " +
                        //         "AND    TRN_SALARY_DETAILS.EMPLOYEE_ID = " + lngSearchId + " ";

                        //intCountRecords = intCountRecords + Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //if (intCountRecords > 0)
                        //{
                        //    cmnService.J_UserMessage(" Employee's company cannot be updated.\n To update this  employee's company, delete all the transactions of this employee in other companies.");
                        //    return;
                        //}
                        //Added by Indrajit on 11-02-2013
                        //if (Check_Record(lngSearchId) == 0) return;
                        #endregion
                        //----------
                        int intInactiveSetup = 0;
                        //
                        if (chkInactiveSetup.Checked == true)
                        {
                            intInactiveSetup = 1;
                        }                        
                        //-----------------------------------------------------------
                        
                        dmlService.J_BeginTransaction();

                        strSQL = "UPDATE SYS_EMAIL_SETUP " +
                                 "SET    SETUP_DESC     = '" + cmnService.J_ReplaceQuote(txtEmailDescription.Text.Trim()) + "'," +
                                 "       FROM_EMAIL_ID  = '" + cmnService.J_ReplaceQuote(txtFromEmailId.Text.Trim()) + "'," +
                                 "       FROM_NAME      = '" + cmnService.J_ReplaceQuote(txtFromDisplayName.Text.Trim()) + "'," +
                                 "       SMTP_USERNAME  = '" + cmnService.J_ReplaceQuote(txtSMTPUserName.Text.Trim()) + "', " +
                                 "       SMTP_PASSWORD  = '" + cmnService.J_ReplaceQuote(txtSMTPPassword.Text) + "', " +
                                 "       SMTP_PORT      = '" + cmnService.J_ReplaceQuote(txtSMTPPort.Text) + "', " +
                                 "       SMTP_HOST      = '" + cmnService.J_ReplaceQuote(txtSMTPHost.Text) + "', " +
                                 "       SMTP_SSL       = " + intSSL + ", " +
                                 "       EMAIL_REPLY_TO = '" + cmnService.J_ReplaceQuote(txtEmailReplyTo.Text) + "', " +
                                 "       EMAIL_CC       = '" + cmnService.J_ReplaceQuote(txtEmailCC.Text) + "', " +
                                 "       EMAIL_CC2      = '" + cmnService.J_ReplaceQuote(txtEmailCC2.Text) + "', " +
                                 "       EMAIL_BCC      = '" + cmnService.J_ReplaceQuote(txtEmailBCC.Text) + "', " +
                                 "       EMAIL_BCC2     = '" + cmnService.J_ReplaceQuote(txtEmailBCC2.Text) + "', " +
                                 "       INACTIVE_FLAG  = " + intInactiveSetup + ", " +
                                 "       SERVER_AUTHENTICATION  = " + intServerAuthentication + ", " +
                                 "       DISABLE_SSL_TLS  = " + intDisbaleSSLTLS + " " +
                                 "WHERE  EMAIL_SETUP_ID =  " + lngSearchId + "";
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
                        //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        dgvGrid.Visible = true;
                        //-----------------------------------------------------------
                        //
                        chkInactiveSetup.Visible = false;
                        //DisableControls();
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //
                        cmbCompanyName.Enabled = true;
                        //-----------------------------------------------------------
                        //dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
                        dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                        break;
                    #endregion

                    #region DELETE
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
                        #region COMMENT
                        //-- TRN_DEDUCTEE_DETAILS
                        //-----------------------------------------------------------------------
                        //strSQL = "SELECT PARTY_ID " +
                        //    "     FROM   TRN_DEDUCTEE_DETAILS " +
                        //    "     WHERE  PARTY_ID = " + lngSearchId + " ";
                        //strSQL = "SELECT PARTY_ID " +
                        //    "     FROM   TRN_DEDUCTEE_DETAILS," +
                        //    "            TRN_BASIC_INFO " +
                        //    "     WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID " +
                        //    "     AND    TRN_BASIC_INFO.FORM_NO             = '" + T_FormNo.F24Q + "' " +
                        //    "     AND    TRN_DEDUCTEE_DETAILS.PARTY_ID      = " + lngSearchId + " ";
                        ////--
                        //if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        //{
                        //    cmnService.J_UserMessage("The Employee cannot be deleted");
                        //    BtnDelete.Select();
                        //    dmlService.J_Rollback();
                        //    return;
                        //}
                        #endregion
                        //-------------------------------------------------
                        //-- TRN_SALARY_DETAILS
                        strSQL = "SELECT EMAIL_SETUP_ID " +
                            "     FROM   TRN_EMAIL_HEADER_LOG " +
                            "     WHERE  EMAIL_SETUP_ID = " + lngSearchId + " ";
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        {
                            cmnService.J_UserMessage("The Email Setup cannot be deleted");
                            BtnDelete.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //..........................................................
                        if (cmnService.J_UserMessage("Proceed Deletion?", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            lblMode.Text = J_Mode.View;
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        strSQL = "DELETE FROM SYS_EMAIL_SETUP WHERE EMAIL_SETUP_ID =  " + lngSearchId + "";
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
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        dgvGrid.Visible = true;
                        //-----------------------------------------------------------
                        //dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
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
        private long Check_Record(long lngSrchId)
        {
            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "SYS_EMAIL_SETUP", "EMAIL_SETUP_ID", lngSrchId) == true) return lngSrchId;

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
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
            if (dsetGridClone == null) return 0;
            //-------------------------------------------
            BtnAdd.Select();
            //-------------------------------------------                            
            return 0;

        }
        #endregion


        #region SendEmail
        private bool SendEmail(string EmailFrom,
                               string DisplayName,
                               string EmailReplyTo,
                               string EmailIdCC,
                               string EmailIdCC2,
                               string EmailIdBCC,
                               string EmailIdBCC2,
                               string EmailSubject,
                               string EmailBody,
                               string EmailIdTo)
        {
            //------------------------------------------------------------------
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient();
            //------------------------------------------------------------------
            MailMessage mail = new MailMessage();
            //--
            try
            {
                //SmtpServer.Credentials = new System.Net.NetworkCredential
                //         (strSMTPUserName, Convert.ToString(strSMTPPassword));
                //SmtpServer.Port = Convert.ToInt32(strSMTPPort);
                //SmtpServer.Host = strSMTPHost;
                //--------
                //SmtpClient client = new SmtpClient();
                //client.Host = "hostname";
                //client.Port = 465;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;
                //client.EnableSsl = true;
                //client.Credentials = new NetworkCredential("User", "Pass);
                //client.Send("from@hostname", "to@hostname", "Subject", "Body");
                SmtpServer.Credentials = new System.Net.NetworkCredential(txtSMTPUserName.Text, Convert.ToString(txtSMTPPassword.Text));//, "MicrosoftOffice365Domain.com");
                SmtpServer.Port = Convert.ToInt32(txtSMTPPort.Text);
                SmtpServer.Host = txtSMTPHost.Text;
                //SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network; //-- 2018/04/18
                //--
                if (chkServerAuthentication.Checked==true)
                    SmtpServer.UseDefaultCredentials = true;
                //--
                if (cmbSSL.Text == T_YES_NO.YES)
                    SmtpServer.EnableSsl = true;
                else
                    SmtpServer.EnableSsl = false;
                //-- 2022/02/14
                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                //SmtpServer.DeliveryMethod = System.Net.SecurityProtocolType.Tls12;// System.Net.Mail.SmtpDeliveryMethod.Network;
                //SmtpServer.ServicePoint.ProtocolVersion = System.Net.SecurityProtocolType.Tls12;
                //SmtpServer.TargetName = "STARTTLS/smtp.office365.com";
                //-- 2022/02/14
                //--------------------------------------------------------------------
                if (EmailIdTo.EndsWith(",") == true)
                    EmailIdTo = EmailIdTo.Substring(0, EmailIdTo.Length - 1);
                //--------------------------------------------------------------------
                //if (EmailIdBCC.EndsWith(",") == true)
                //    EmailIdBCC = EmailIdBCC.Substring(0, EmailIdBCC.Length - 1);
                //--------------------------------------------------------------------
                if (EmailIdTo.Trim() == "") return false;
                //--------------------------------------------------------------------                
                this.Cursor = Cursors.WaitCursor;
                //---------------------------------------------------------------------
                mail.From = new System.Net.Mail.MailAddress(EmailFrom, DisplayName, System.Text.Encoding.UTF8);
                //mail.Priority = MailPriority.High;
                mail.ReplyTo = new System.Net.Mail.MailAddress(EmailReplyTo);
                //--
                #region EMAIL CC
                if (EmailIdCC.Trim() != "")
                {
                    if (EmailIdCC2.Trim() != "")
                        mail.CC.Add(EmailIdCC + "," + EmailIdCC2);
                    else
                        mail.CC.Add(EmailIdCC);
                }
                #endregion
                //--
                #region EMAIL BCC
                if (EmailIdBCC.Trim() != "")
                {
                    if (EmailIdBCC2.Trim() != "")
                        mail.Bcc.Add(EmailIdBCC + "," + EmailIdBCC2);
                    else
                        mail.Bcc.Add(EmailIdBCC);
                }
                #endregion
                //--
                mail.To.Add(EmailIdTo);
                //-- 
                #region EMAILSUBJECT
                //EmailSubject = EmailSubject.Replace("#FA_YEAR#", cmbFAYear.Text);
                //EmailSubject = EmailSubject.Replace("#QTR#", cmbQtr.Text);
                mail.Subject = EmailSubject;
                #endregion
                // 
                #region EMAIL BODY WITH ATTACHMENT
                //
                #region COMMENT
                //-- PDF ATTACHMENT
                //if (File.Exists(pdfPath) == true)
                //{
                //    FileStream fs1 = new FileStream(pdfPath, FileMode.Open, FileAccess.Read);
                //    Attachment a1 = new Attachment(fs1, Path.GetFileName(pdfPath), MediaTypeNames.Application.Octet);
                //    mail.Attachments.Add(a1);
                //}
                //---------------------------------------------------------------------
                //-- BODY
                //EmailBody = EmailBody.Replace("#COMPANY_NAME#", cmnService.J_Mid(cmbCompanyName.Text, 0, cmbCompanyName.Text.Length - 12));
                //EmailBody = EmailBody.Replace("#TAN_NO#", cmnService.J_Right(cmbCompanyName.Text, 11).Replace("]", ""));
                //EmailBody = EmailBody.Replace("#PARTY_NAME#", PartyName);
                //EmailBody = EmailBody.Replace("#PAN_NO#", PartyPAN);
                #endregion
                //                
                StringBuilder htmlString = new StringBuilder();
                htmlString.Append(EmailBody);
                #endregion
                //
                mail.Body = htmlString.ToString();
                mail.IsBodyHtml = false ;
                mail.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;
                SmtpServer.Timeout = 300000;//(5min) 
                //
                SmtpServer.Send(mail);
                //
                this.Cursor = Cursors.Default;
                //
                return true;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.ToString());
                //this.Cursor = Cursors.Default;
                return false;
            }
        }
        #endregion

        #region SendEmailTLSEAMail
        //-- https://www.emailarchitect.net/easendmail/ex/c/3.aspx
        //private bool SendEmailTLSEAMail(string EmailFrom,
        //                       string DisplayName,
        //                       string EmailReplyTo,
        //                       string EmailIdCC,
        //                       string EmailIdCC2,
        //                       string EmailIdBCC,
        //                       string EmailIdBCC2,
        //                       string EmailSubject,
        //                       string EmailBody,
        //                       string EmailIdTo)
        //{
        //    //------------------------------------------------------------------
        //    //System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient();
        //    ////------------------------------------------------------------------
        //    //MailMessage mail = new MailMessage();
        //    //--
        //    try
        //    {
                //if (EmailIdTo.Trim() == "") return false;
                ////--
                //this.Cursor = Cursors.WaitCursor;
                //SmtpMail smtpMail = new SmtpMail("Tryit");
                //// Set sender email address, please change it to yours
                //smtpMail.From = EmailFrom;
                //// Set recipient email address, please change it to yours
                ////--
                //#region EMAIL CC
                //if (EmailIdCC.Trim() != "")
                //{
                //    if (EmailIdCC2.Trim() != "")
                //        smtpMail.Cc.Add(EmailIdCC + "," + EmailIdCC2);
                //    else
                //        smtpMail.Cc.Add(EmailIdCC);
                //}
                //#endregion
                ////--
                //#region EMAIL BCC
                //if (EmailIdBCC.Trim() != "")
                //{
                //    if (EmailIdBCC2.Trim() != "")
                //        smtpMail.Bcc.Add(EmailIdBCC + "," + EmailIdBCC2);
                //    else
                //        smtpMail.Bcc.Add(EmailIdBCC);
                //}
                //#endregion
                ////--
                //smtpMail.To.Add(EmailIdTo);
                //smtpMail.ReplyTo = EmailReplyTo;
                //// Set email subject
                //smtpMail.Subject = EmailSubject;

                //// Set email body
                //smtpMail.TextBody = EmailBody;

                //// Your SMTP server address
                //SmtpServer smtpServer = new SmtpServer(txtSMTPHost.Text);
                //// User and password for ESMTP authentication, if your server doesn't require
                //// User authentication, please remove the following codes.
                //smtpServer.User = txtSMTPUserName.Text;
                //smtpServer.Password = txtSMTPPassword.Text;
                //// Set 25 or 587 port.
                //smtpServer.Port = Convert.ToInt32(txtSMTPPort.Text);
                //smtpServer.DeliveryNotification = EASendMail.DeliveryNotificationOptions.OnFailure;
                //// detect TLS connection automatically
                //smtpServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                ////Console.WriteLine("start to send email ...");
                //EASendMail.SmtpClient smtpClient = new EASendMail.SmtpClient();
                //smtpClient.SendMail(smtpServer, smtpMail);
                //smtpClient.Timeout = 300000;//(5min) 
                ////
                //this.Cursor = Cursors.Default;
                //return true;
        //    }
        //    catch (Exception err)
        //    {
        //        this.Cursor = Cursors.Default;
        //        cmnService.J_UserMessage(err.ToString());
        //        return false;
        //    }
        //}
        #endregion

        #region SendEmailTLS
        private bool SendEmailTLS(string EmailFrom,
                               string DisplayName,
                               string EmailReplyTo,
                               string EmailIdCC,
                               string EmailIdCC2,
                               string EmailIdBCC,
                               string EmailIdBCC2,
                               string EmailSubject,
                               string EmailBody,
                               string EmailIdTo)
        {
            //------------------------------------------------------------------
            //System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient();
            ////------------------------------------------------------------------
            //MailMessage mail = new MailMessage();
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient();
            //------------------------------------------------------------------
            MailMessage mail = new MailMessage();
            //--
            try
            {
                if (EmailIdTo.Trim() == "") return false;
                //--
                this.Cursor = Cursors.WaitCursor;
                //SmtpMail smtpMail = new SmtpMail("Tryit");
                // Set sender email address, please change it to yours
                mail.From = new MailAddress(EmailFrom, DisplayName, System.Text.Encoding.UTF8); ;
                // Set recipient email address, please change it to yours
                //--
                #region EMAIL CC
                if (EmailIdCC.Trim() != "")
                {
                    if (EmailIdCC2.Trim() != "")
                        mail.CC.Add(EmailIdCC + "," + EmailIdCC2);
                    else
                        mail.CC.Add(EmailIdCC);
                }
                #endregion
                //--
                #region EMAIL BCC
                if (EmailIdBCC.Trim() != "")
                {
                    if (EmailIdBCC2.Trim() != "")
                        mail.Bcc.Add(EmailIdBCC + "," + EmailIdBCC2);
                    else
                        mail.Bcc.Add(EmailIdBCC);
                }
                #endregion
                //--
                mail.To.Add(EmailIdTo);
                mail.ReplyTo = new MailAddress(EmailReplyTo);
                // Set email subject
                mail.Subject = EmailSubject;

                // Set email body
                mail.Body = EmailBody;

                // Your SMTP server address
                //SmtpServer smtpServer = new SmtpServer(txtSMTPHost.Text);
                SmtpServer.Host = txtSMTPHost.Text;
                // User and password for ESMTP authentication, if your server doesn't require
                // User authentication, please remove the following codes.
                //SmtpServer.User = txtSMTPUserName.Text;
                //SmtpServer.Password = txtSMTPPassword.Text;
                SmtpServer.Credentials = new System.Net.NetworkCredential
                            (Convert.ToString(txtSMTPUserName.Text), Convert.ToString(txtSMTPPassword.Text));
                // Set 25 or 587 port.
                SmtpServer.Port = Convert.ToInt32(txtSMTPPort.Text);
                //SmtpServer.DeliveryNotification = EASendMail.DeliveryNotificationOptions.OnFailure;

                //
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                // detect TLS connection automatically
                //SmtpServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                if (chkDisableTLSSSL.Checked == false) //-- JANA BANK CASE
                {
                    SmtpServer.EnableSsl = true;
                    //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls
                    //                      | System.Net.SecurityProtocolType.Tls11
                    //                      | System.Net.SecurityProtocolType.Tls12;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.SystemDefault;
                }
                SmtpServer.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

                //-- 2023/06/28
                //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                //if (chkServerAuthentication.Checked == true)
                //    SmtpServer.UseDefaultCredentials = true;
                //Console.WriteLine("start to send email ...");
                //EASendMail.SmtpClient smtpClient = new EASendMail.SmtpClient();
                //smtpClient.SendMail(smtpServer, smtpMail);
                SmtpServer.Timeout = 300000;//(5min)  
                //SmtpServer.SendAsync(mail,null);
                SmtpServer.Send(mail);
                //
                this.Cursor = Cursors.Default;
                return true;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                if(err.Message.Contains("authentic"))
                {
                    cmnService.J_UserMessage("Please check the login details (SMTP username/password)", MessageBoxIcon.Error);
                    txtSMTPPassword.Select();
                }
                else
                    cmnService.J_UserMessage(err.ToString(), MessageBoxIcon.Error);
                return false;
            }
        }
        //--
        private static void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Console.WriteLine("Email send canceled.");
            }
            if (e.Error != null)
            {
                Console.WriteLine("Email send error: " + e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Email sent successfully.");
            }
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=fwz9VzQjA90");
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0037", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }




        #endregion

        #region SendEmailImplicitSSL
        private bool SendEmailImplicitSSL(string EmailFrom,
                                  string DisplayName,
                                  string EmailReplyTo,
                                  string EmailIdCC,
                                  string EmailIdCC2,
                                  string EmailIdBCC,
                                  string EmailIdBCC2,
                                  string EmailSubject,
                                  string EmailBody,
                                  string EmailIdTo)
        {
            try
            {
                if (EmailIdTo.Trim() == "") return false;
                this.Cursor = Cursors.WaitCursor;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(DisplayName, EmailFrom));
                message.ReplyTo.Add(new MailboxAddress("", EmailReplyTo));
                message.To.AddRange(InternetAddressList.Parse(EmailIdTo));

                if (!string.IsNullOrWhiteSpace(EmailIdCC))
                    message.Cc.AddRange(InternetAddressList.Parse(EmailIdCC));
                if (!string.IsNullOrWhiteSpace(EmailIdCC2))
                    message.Cc.AddRange(InternetAddressList.Parse(EmailIdCC2));
                if (!string.IsNullOrWhiteSpace(EmailIdBCC))
                    message.Bcc.AddRange(InternetAddressList.Parse(EmailIdBCC));
                if (!string.IsNullOrWhiteSpace(EmailIdBCC2))
                    message.Bcc.AddRange(InternetAddressList.Parse(EmailIdBCC2));

                message.Subject = EmailSubject;
                message.Body = new TextPart("html") { Text = EmailBody };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Timeout = 300000;

                    client.Connect(txtSMTPHost.Text.Trim(), 465, SecureSocketOptions.SslOnConnect);

                    if (!string.IsNullOrWhiteSpace(txtSMTPUserName.Text))
                    {
                        client.Authenticate(txtSMTPUserName.Text.Trim(), txtSMTPPassword.Text.Trim());
                    }

                    client.Send(message);
                    client.Disconnect(true);
                }

                this.Cursor = Cursors.Default;
                return true;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                if (err.Message.ToLower().Contains("authentic"))
                {
                    cmnService.J_UserMessage("Please check the login details (SMTP username/password)", MessageBoxIcon.Error);
                    txtSMTPPassword.Select();
                }
                else
                {
                    cmnService.J_UserMessage(err.ToString(), MessageBoxIcon.Error);
                }
                return false;
            }
        }
        #endregion


            #endregion

            private void pctManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0086", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        private void chkDisableTLSSSL_CheckedChanged(object sender, EventArgs e)
        {
            if(chkDisableTLSSSL.Checked==true)
            {
                cmnService.J_UserMessage("Ignoring SSL/TLS certificate validation might solve the immediate problem,\n" +
                                        "but it's not recommended as it exposes the application to potential security risks.", MessageBoxIcon.Warning);
                return;
            }
        }
    }
}

