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
using System.Globalization;
using System.Data.SqlClient;
using System.IO;


//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

using Excel = Microsoft.Office.Interop.Excel;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;


using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

//using Newtonsoft.Json;

using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using System.Linq;
#endregion


namespace TDSMAN.FormMst
{
    public partial class MstReceiptNo : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public MstReceiptNo()
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
        string strImagePath = "";
        //string strImageFolderName= "RECEIPT_IMAGE";
        string strDatabasePath = "";
        //-----------------------------------------------------------------------
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
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


        #region MstReceiptNo_Load

        private void MstReceiptNo_Load(object sender, EventArgs e)
        {
            try
            {
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //-----------------------------------------------------------
                GC.Collect();
                //
                TdsMan.GetDatabasePathExists(out strDatabasePath);
                //
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                BtnAdd.Enabled = false;
                BtnAdd.BackColor = Color.LightGray;
                BtnSearch.Enabled = false;
                BtnSearch.BackColor = Color.LightGray;
                dgvGrid.Visible = true;
                //--
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                    BtnPrint.Visible = false;
                //-----------------------------------------------------------
                //BLOCKED BY DHRUB ON 2015_11_12
                //-----------------------------------------------------------
                //BtnDelete.Enabled = false;
                //BtnDelete.BackColor = Color.LightGray;
                BtnDelete.Enabled = true;
                BtnDelete.BackColor = Color.Lavender;
                //-----------------------------------------------------------
                ViewGrid.Height = 487;
                dgvGrid.Height = 487;
                //DisableControls();
                //-----------------------------------------------------------
                ControlVisible(false);
                ClearControls();
                //-----------------------------------------------------------
                // ----------------------------------------------------------
                // -- Loading all filter options ----------------------------
                // ----------------------------------------------------------
                //Added by Shrey Kejriwal on 04/02/2013
                //Loading Financial Year combo 
                //Selecting only those Financial Years whose return has been filed

                strSQL = "SELECT DISTINCT MST_ASSESSMENT.ASST_ID AS ASST_ID, " +
                         "       MST_ASSESSMENT.FA_YEAR AS FA_YEAR " +
                         "FROM MST_ASSESSMENT, " +
                         "     TRN_BASIC_INFO " +
                         "WHERE MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID " +
                         "ORDER BY MST_ASSESSMENT.FA_YEAR DESC ";

                //dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYearFilter);
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYearFilter) == false) return;

                //Loading Company combo 
                //Selecting only those Company whose return has been filed

                strSQL = "SELECT DISTINCT MST_COMPANY.COMPANY_ID   AS COMPANY_ID, " +
                         "       MST_COMPANY.COMPANY_NAME + ' [' + MST_COMPANY.TAN_NO + ']' AS COMPANY_NAME " +
                         "FROM MST_COMPANY, " +
                         "     TRN_BASIC_INFO " +
                         "WHERE MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID " +
                         "ORDER BY MST_COMPANY.COMPANY_NAME + ' [' + MST_COMPANY.TAN_NO + ']'";// + ' - ' +  MST_COMPANY.TAN_NO";

                dmlService.J_PopulateComboBox(strSQL, ref cmbCompanyFilter);


                //Loading Form No Combo
                //Selecting only those Forms whose return is filed

                //////strSQL = "SELECT DISTINCT 1, FORM_NO " +
                //////         "FROM TRN_BASIC_INFO " +
                //////         "ORDER BY FORM_NO ";
                strSQL = "SELECT DISTINCT " +
                         "IIF(FORM_NO = '24Q', 138, " +
                         "IIF(FORM_NO = '26Q', 140, " +
                         "IIF(FORM_NO = '27Q', 144, " +
                         "IIF(FORM_NO = '27EQ', 143, 0)))) AS FORM_ID, " +

                         "IIF(FORM_NO = '24Q', '138 (24Q)', " +
                         "IIF(FORM_NO = '26Q', '140 (26Q)', " +
                         "IIF(FORM_NO = '27Q', '144 (27Q)', " +
                         "IIF(FORM_NO = '27EQ', '143 (27EQ)', '')))) AS FORM_DISPLAY " +
                         "FROM TRN_BASIC_INFO " +
                         "WHERE FORM_NO IN ('24Q', '26Q', '27Q', '27EQ')";

                dmlService.J_PopulateComboBox(strSQL, ref cmbFormNoFilter);


                //Loading Form No Combo
                //Selecting only hose Forms whose return is filed

                strSQL = "SELECT DISTINCT 1, QTR " +
                         "FROM TRN_BASIC_INFO " +
                         "ORDER BY QTR ";

                dmlService.J_PopulateComboBox(strSQL, ref cmbQuarterFilter);
                //-----------------------------------------------------------
                lblTitle.Text = "Receipt No. Master";
                //-----------------------------------------------------------
                //ViewGrid_Click(sender, e);
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //-- ADDED BY DHRUB ON @2015-11-16
                //-----------------------------------------------------------
                ClearButtonVisibilityState();
                //-- 2021/12/04
                if (TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard == true)
                {
                    BtnEdit_Click(sender, e);
                }
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
                BtnAdd.Enabled = false;
                BtnAdd.BackColor = Color.LightGray;
                BtnSearch.Enabled = false;
                BtnSearch.BackColor = Color.LightGray; 
                lblSearchMode.Text = J_Mode.General;
                dgvGrid.Visible = false;
                //---------------------------------------------
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                //---------------------------------------------
                strCheckFields = "";
                //cmbFormNo.Select();
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
                //if (ViewGrid.CurrentRowIndex >= 0)
                if(dgvGrid.CurrentRow != null)
                {//-- Added By Abhishek Dey On 01/11/2019 --
                    #region IS COMPANY ACCESSABLE FOR CLIENT
                    long lngCompanyId = Convert.ToInt64(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_ID FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + lngSearchId));

                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                    {
                        if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                        {
                            if (TdsMan.IsCompanyAccessible(lngCompanyId, TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
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
                    //-----------------------------------------
                    ControlVisible(true);
                    ClearControls();
                    //--
                    long lngID = 0;
                    if (TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard == true)
                        lngID = TDSMAN.Classes.TDSMAN.T_pBasicInfoId;
                    else
                        lngID = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
                    //--------------------------------------------------
                    //A particular ID wise retriving the data from database
                    if (ShowRecord(lngID) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                    }
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    BtnAdd.Enabled = false;
                    BtnAdd.BackColor = Color.LightGray;
                    BtnDelete.Enabled = false;
                    BtnDelete.BackColor = Color.LightGray;
                    BtnSearch.Enabled = false;
                    BtnSearch.BackColor = Color.LightGray; 
                    lblSearchMode.Text = J_Mode.General;
                    dgvGrid.Visible = false;
                    //--------------------------------------------------
                    strCheckFields = "";
                    //--------------------------------------------------

                    //Added by Shrey Kejriwal on 05/02/2013
                    grpFilter.Visible = false;
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone,  lngSearchId);
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
                if (TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard == true)
                {
                    TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard = false;
                    //
                    GC.Collect();
                    //
                    dmlService.Dispose();
                    this.Close();
                    this.Dispose();
                }
                //-------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
                BtnAdd.Enabled = false;
                BtnAdd.BackColor = Color.LightGray;
                BtnSearch.Enabled = false;
                BtnSearch.BackColor = Color.LightGray;
                dgvGrid.Visible = true;
                //BLOCKED BY DHRUB ON 2015_11_12
                //BtnDelete.Enabled = false;
                //BtnDelete.BackColor = Color.LightGray;
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
                    BtnEdit.Select();
                //-------------------------------------------

                //Added by Shrey Kejriwal on 05/02/2013
                grpFilter.Visible = true;
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
                cmbFinancialYearSearch.Select();
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
                //-- FINANCIAL YEAR
                //-------------------------------------------------------------------
                if (cmbFinancialYearSearch.SelectedIndex > 0)
                    strCheckFields = "AND MST_ASSESSMENT.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYearSearch, cmbFinancialYearSearch.SelectedIndex)) + " ";
                //-------------------------------------------------------------------
                //-- QUARTER
                //-------------------------------------------------------------------
                if (cmbQuarterSearch.SelectedIndex > 0)
                    strCheckFields = strCheckFields + " AND TRN_BASIC_INFO.QTR = '" + cmnService.J_ReplaceQuote(cmbQuarterSearch.Text.Trim().ToUpper()) + "' ";
                //-------------------------------------------------------------------
                //-- COMPANY NAME
                //-------------------------------------------------------------------
                if (txtCompanyNameSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + " AND MST_COMPANY.COMPANY_NAME like '%" + cmnService.J_ReplaceQuote(txtCompanyNameSearch.Text.Trim().ToUpper()) + "%' ";

                //Added by Shrey Kejriwal on 17/08/2011

                //-------------------------------------------------------------------
                //-- TAN No.
                //-------------------------------------------------------------------
                if (txtTANSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + " AND MST_COMPANY.TAN_NO like '%" + cmnService.J_ReplaceQuote(txtTANSearch.Text.Trim().ToUpper()) + "%' ";
                
                //-------------------------------------------------------------------
                //-- FORM NO
                //-------------------------------------------------------------------
                if (cmbFormNoSearch.SelectedIndex > 0)
                    strCheckFields = strCheckFields + " AND TRN_BASIC_INFO.FORM_NO = '" + cmnService.J_ReplaceQuote(cmbFormNoSearch.Text.Trim().ToUpper()) + "' ";
                //-------------------------------------------------------------------
                //-- RECEIPT NO.
                //-------------------------------------------------------------------
                if (txtReceiptNoSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + " AND TRN_BASIC_INFO.PRN_NO like '%" + cmnService.J_ReplaceQuote(txtReceiptNoSearch.Text.Trim().ToUpper()) + "%' ";
                //----------------------------------------------------------------------
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                //----------------------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);  //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId) == false)
                {
                    cmbFinancialYearSearch.Select();
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
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone,  lngSearchId);
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
            //------------------------------
            //BLOCKED BY DHRUB @@2015_11_12
            //------------------------------
            //lblMode.Text = J_Mode.Delete;
            //Insert_Update_Delete_Data();
            //------------------------------


            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //-- ADDED BY DHRUB ON @2015-11-12
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            try
            {
                if (cmnService.J_UserMessage("Do you want to remove receipt details for " + "\n Based on \n " +
                                             "FINANCIAL YEAR \t: " + Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[1].Value) + " \n " +
                                             "FORM NO. \t: " + Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[2].Value) + " \n " +
                                             "QUARTER \t: " + Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[3].Value) + " \n " +
                                             "COMPANY  \t: " + Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[4].Value) + " \n " +
                                             "TAN  \t\t: " + Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[5].Value) + " \n " +
                                             "Proceed??", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
                //--

                string strDateOfFiling = "NULL";
                int intUpdateFlag = 0;
                //-----------------------------------------------------------
                dmlService.J_BeginTransaction();
                //-----------------------------------------------------------
                strSQL = "UPDATE TRN_BASIC_INFO " +
                         "SET    RECEIPT_NO     =  ''," +
                         "       DATE_OF_FILING =  " + strDateOfFiling + "," +
                         "       PRN_NO         =  ''" +
                         "WHERE  BASIC_INFO_ID  =  " + lngSearchId + "";
                //-----------------------------------------------------------
                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    return;
                //-----------------------------------------------------------
                strSQL = "UPDATE TRN_COMPANY_INFO " +
                         "SET    UPDATE_FLAG   = " + intUpdateFlag + " " +
                         "WHERE  BASIC_INFO_ID = " + lngSearchId + "";
                //----------------------------------------------------------
                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    return;
                //----------------------------------------------------------
                dmlService.J_Commit();
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
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                //-----------------------------------------------------------
                ClearButtonVisibilityState();
            }
            catch(Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        }

        #endregion

        #region BtnRefresh_Click

        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-----------------------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                BtnAdd.Enabled = false;
                BtnAdd.BackColor = Color.LightGray;
                BtnSearch.Enabled = false;
                BtnSearch.BackColor = Color.LightGray;
                dgvGrid.Visible = true;
                //BtnDelete.Enabled = false;
                //BtnDelete.BackColor = Color.LightGray;
                //-----------------------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                //-----------------------------------------------------------
                //DisableControls();
                //-----------------------------------------------------------
                ClearControls();
                //-----------------------------------------------------------

                //Added by Shrey Kejriwal on 05/02/2013
                
                //Clearing all the filters
                cmbFinancialYearFilter.SelectedIndex = 0;
                cmbCompanyFilter.SelectedIndex = 0;
                cmbFormNoFilter.SelectedIndex = 0;
                cmbQuarterFilter.SelectedIndex = 0;
                //
                strCheckFields = "";
                strSQL = strQuery + "order by " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-----------------------------------------------------------
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                //-----------------------------------------------------------
                //-- ADDED BY DHRUB ON @2015-11-16
                //-----------------------------------------------------------
                ClearButtonVisibilityState();
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
            if (dgvGrid.CurrentRow == null)
            {
                BtnEdit.Focus();
                return;
            }
            lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));

            //ViewGrid.Select(ViewGrid.CurrentRowIndex);
            //ViewGrid.Select();
            //ViewGrid.Focus();

            //---------------------------------------------------
            //--ADDED BY DHRUB ON @2015-11-16 
            //-- TO CLEAR RECEIPT_NO DETAILS IN ONE CLICK.
            //---------------------------------------------------
            //if (Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 6]) != "" || Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 7]) != "" || Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 8]) != "")
            //{
            //    BtnDelete.Enabled = true;
            //    BtnDelete.BackColor = Color.Lavender;
            //}
            //else
            //{
            //    BtnDelete.Enabled = false;
            //    BtnDelete.BackColor = Color.LightGray;
            //}
            ClearButtonVisibilityState();
            //---------------------------------------------------
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

                if (dgvGrid.CurrentRow == null)
                {
                    BtnEdit.Focus();
                    return;
                }
                lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
                //
                if (e.KeyCode == System.Windows.Forms.Keys.Enter) BtnEdit_Click(sender, e);
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
            //////if (dgvGrid.CurrentRow == null)
            //////{
            //////    BtnEdit.Focus();
            //////    return;
            //////}
            //////lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));

            //---------------------------------------------------
            //--ADDED BY DHRUB ON @2015-11-16 
            //-- TO CLEAR RECEIPT_NO DETAILS IN ONE CLICK.
            //---------------------------------------------------
            //if (Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 6]) != "" || Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 7]) != "" || Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 8]) != "")
            //{
            //    BtnDelete.Enabled = true;
            //    BtnDelete.BackColor = Color.Lavender;
            //}
            //else
            //{
            //    BtnDelete.Enabled = false;
            //    BtnDelete.BackColor = Color.LightGray;
            //}
            //ClearButtonVisibilityState();
            //---------------------------------------------------
        }
        #endregion

        #region ViewGrid_MouseUp

        private void ViewGrid_MouseUp(object sender, MouseEventArgs e)
        {
            ViewGrid_Click(sender, e);
        }
        #endregion

        #region cmbFinancialYearFilter_SelectedIndexChanged

        private void cmbFinancialYearFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Populating Data grid on change of filters
            BtnDelete.Enabled = false;
            BtnDelete.BackColor = Color.LightGray;
            //--
            string strLEN = "";
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                strLEN = "DATALENGTH";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strLEN = "LEN";
            //--
            string strSQLBlankTokenNo = "";
            if (chkBlankTokenNo.Checked == true)
                strSQLBlankTokenNo = "AND TRN_BASIC_INFO.PRN_NO = ''";
            else
                strSQLBlankTokenNo = "";
            //--
            //-- set the Help Grid Column Header Text & behavior
            //-- (0) Header Text
            //-- (1) Width
            //-- (2) Format
            //-- (3) Alignment
            //-- (4) NullToText
            //-- (5) Visible
            //-- (6) AutoSizeMode
            //-----------------------------------------------------------

            //Modified by Shrey Kejriwal on 17/08/2011
            //string[,] strMatrix1 = {{"BasicInfoID", "0", "", "Right", "", "", ""},
            //                        {"Financial Year", "150", "S", "", "", "", ""},
            //                        {"Quarter", "80", "S", "", "", "", ""},
            //                        {"Company Name", "400", "S", "", "", "", ""},
            //                        {"Form No.", "110", "S", "", "", "", ""},
            //                        {"Receipt No.", "200", "S", "", "", "", ""}};

            //
            string[,] strImageMatrix = {{ strLEN + "(TRN_BASIC_INFO.RECEIPT_IMAGE_PATH) > 0", "F", "Yes", "T"},
                                        { strLEN + "(TRN_BASIC_INFO.RECEIPT_IMAGE_PATH) = 0", "F", "", "T"}};
            //--
            string[,] strMatrix1 = {{"BasicInfoID", "0", "", "Right", "", "", ""},
						            {"Tax Year", "100", "S", "", "", "", "T"},
						            {"Form No.", "60", "S", "", "", "", "T"},
						            {"Quarter", "60", "S", "", "", "", "T"},
						            {"Company Name", "310", "S", "", "", "", "T"},
						            {"TAN No.", "100", "S", "", "", "", "T"},
                                    {"Token No.", "200", "S", "", "", "", "T"},
                                    {"Date of Filing", "0", "dd/MM/yyyy", "", "", "F", ""},
                                    {"Receipt No", "0", "S", "", "", "F", ""},
                                    {"Image Available", "100", "S", "", "", "", "T"}};
            //-----------------------------------------------------------
            strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            string strForm = cmbFormNoFilter.Text;
            int intStart = strForm.IndexOf('(');
            int intEnd = strForm.IndexOf(')');
            if (intStart >= 0 && intEnd > intStart)
            {
                strForm = strForm.Substring(intStart + 1, intEnd - intStart - 1);
            }
            //-----------------------------------------------------------
            strOrderBy = "MST_ASSESSMENT.FA_YEAR DESC, TRN_BASIC_INFO.QTR, MST_COMPANY.COMPANY_NAME, TRN_BASIC_INFO.FORM_NO";
            strQuery = "SELECT TRN_BASIC_INFO.BASIC_INFO_ID  AS BASIC_INFO_ID," +
                "              MST_ASSESSMENT.FA_YEAR        AS FA_YEAR," +
                //"              TRN_BASIC_INFO.FORM_NO        AS FORM_NO," +
                    "              IIF(TRN_BASIC_INFO.FORM_NO = '24Q', '138 (24Q)', " +
                    "              IIF(TRN_BASIC_INFO.FORM_NO = '26Q', '140 (26Q)', " +
                    "              IIF(TRN_BASIC_INFO.FORM_NO = '27Q', '144 (27Q)', " +
                    "              IIF(TRN_BASIC_INFO.FORM_NO = '27EQ', '143 (27EQ)', ''))))  AS FORM_NO," +
                "              TRN_BASIC_INFO.QTR            AS QTR," +
                "              MST_COMPANY.COMPANY_NAME      AS COMPANY_NAME," +
                "              MST_COMPANY.TAN_NO            AS TAN_NO," +
                "              TRN_BASIC_INFO.PRN_NO         AS PRN_NO, " +
                //--------------------------------------------------------------------------------------------------------------------------------
                //--ADDED BY DHRUB ON @2015-11-16 [TO MAKE CLEAR BUTTON DISABLE WHILE NONE OF DATA EXISTS INTO PNR_NO,DATE_OF_FILING,RECEIPT_NO]
                //-- TO CLEAR RECEIPT_NO DETAILS IN ONE CLICK.
                //--------------------------------------------------------------------------------------------------------------------------------
                "              TRN_BASIC_INFO.DATE_OF_FILING AS DATE_OF_FILING," +
                "              TRN_BASIC_INFO.RECEIPT_NO     AS RECEIPT_NO," +
                "              " + cmnService.J_SQLDBFormat(strImageMatrix, J_SQLColFormat.Case_End) + " AS RECEIPT_IMAGE_PATH " +
                "       FROM   TRN_BASIC_INFO," +
                "              MST_COMPANY," +
                "              MST_ASSESSMENT " +
                "       WHERE  TRN_BASIC_INFO.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                "       AND    TRN_BASIC_INFO.ASST_ID    = MST_ASSESSMENT.ASST_ID ";

            //APPLYLING FILTER FOR FINANCIAL YEAR
            if (cmbFinancialYearFilter.SelectedIndex > 0)
                strQuery += "AND TRN_BASIC_INFO.ASST_ID = " + Support.GetItemData(cmbFinancialYearFilter, cmbFinancialYearFilter.SelectedIndex) + " ";

            //APPLYING FILTER FOR COMPANY
            if (cmbCompanyFilter.SelectedIndex > 0)
                strQuery += "AND TRN_BASIC_INFO.COMPANY_ID = " + Support.GetItemData(cmbCompanyFilter, cmbCompanyFilter.SelectedIndex) + " ";

            //APPLYING FILTER FOR FORM NO
            if (cmbFormNoFilter.SelectedIndex > 0)
                strQuery += "AND TRN_BASIC_INFO.FORM_NO = '" + strForm + "' ";

            //APPLYING FILTER FOR QUARTER
            if (cmbQuarterFilter.SelectedIndex > 0)
                strQuery += "AND TRN_BASIC_INFO.QTR = '" + cmbQuarterFilter.Text + "' ";
            //--
            strQuery = strQuery + strSQLBlankTokenNo;
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid

        }
        #endregion

        #region lblLink_LinkClicked
        private void lblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://onlineservices.tin.nsdl.com/TIN/JSP/tds/linktoUnAuthorizedInput.jsp");
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
        
        #region btnBrowsePath_Click
        private void btnBrowsePath_Click(object sender, EventArgs e)
        {
            strImagePath = cmnService.J_OpenFileDialog("Image/PDF File | *.jpg; *.bmp; *.pdf", "Image/PDF File | *.jpg; *.bmp; *.pdf", "Choose the Receipt File");
            //--
            if (strImagePath != "")
            {
                txtScannedImagePath.Text = strImagePath;
                btnPreviewImage.Enabled = true;
            }
            else
            {
                txtScannedImagePath.Text = "";
                btnPreviewImage.Enabled = false;
            }
        }
        #endregion

        #region btnPreviewImage_Click
        private void btnPreviewImage_Click(object sender, EventArgs e)
        {
            if (txtScannedImagePath.Text.Trim() == "")
            {
                cmnService.J_UserMessage("No path selected");
                return;
            }
            //--
            if (cmnService.J_IsFileExist(txtScannedImagePath.Text) == false)
            {
                cmnService.J_UserMessage("Selected File not found");
                return;
            }
            //--
            //if(Path.GetExtension(txtScannedImagePath.Text.Trim().ToUpper()) == ".PDF")
            //{
                System.Diagnostics.Process.Start(txtScannedImagePath.Text.Trim());
            //}
            //else
            //{
            //    TDSMAN.Classes.TDSMAN.T_ReceiptImageFilePath = txtScannedImagePath.Text;
            //    //--
            //    MstImagePreview MstImagePreview = new MstImagePreview();
            //    MstImagePreview.StartPosition = FormStartPosition.CenterScreen;
            //    MstImagePreview.ShowDialog();
            //    //--
            //}
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
            txtFinancialYear.Text = "";
            txtQuarter.Text = "";
            txtCompanyName.Text = "";
            txtFormNo.Text = "";
            txtReceiptNo.Text = "";
            mskDateOfFiling.Text = "";
            txtPRNNo.Text = ""; 
            txtScannedImagePath.Text= "";
            txtScannedImagePathM.Text= "";
            btnPreviewImage.Enabled = false;
            TDSMAN.Classes.TDSMAN.T_ReceiptImageFilePath = "";
            //--------------------//-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      ORDER BY ASST_ID DESC";
            //Modified by Indrajit on 04-03-2013
            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYearSearch, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYearSearch) == false) return;
            //--
            string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQuarterSearch);
            //--
            txtCompanyNameSearch.Text = "";

            //--Added by Shrey on 17/08/2011
            txtTANSearch.Text = "";
            //--
            //string[] strFormNo = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            string[] strFormNo = { T_FormNo.F138_24Q, T_FormNo.F140_26Q, T_FormNo.F144_27Q, T_FormNo.F143_27EQ };
            dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNoSearch);
            //--
            txtReceiptNoSearch.Text = "";
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            try
            {
                strSQL ="SELECT TRN_BASIC_INFO.BASIC_INFO_ID     AS BASIC_INFO_ID," +
                    "              MST_ASSESSMENT.FA_YEAR        AS FA_YEAR," +
                    "              TRN_BASIC_INFO.QTR            AS QTR," +
                    "              MST_COMPANY.COMPANY_NAME      AS COMPANY_NAME," +
                    "              MST_COMPANY.TAN_NO            AS TAN_NO," +
                     "             TRN_BASIC_INFO.FORM_NO        AS FORM_NO," +
                    "              TRN_BASIC_INFO.RECEIPT_NO     AS RECEIPT_NO," +
                    "            " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DATE_OF_FILING," +
                    "              TRN_BASIC_INFO.PRN_NO         AS PRN_NO," +
                    "              TRN_BASIC_INFO.RECEIPT_IMAGE_PATH AS RECEIPT_IMAGE_PATH " +
                    "       FROM   TRN_BASIC_INFO," + 
                    "              MST_COMPANY," +
                    "              MST_ASSESSMENT " +
                    "       WHERE  TRN_BASIC_INFO.COMPANY_ID    = MST_COMPANY.COMPANY_ID " +
                    "       AND    TRN_BASIC_INFO.ASST_ID       = MST_ASSESSMENT.ASST_ID " +
                    "       AND    TRN_BASIC_INFO.BASIC_INFO_ID = " + Id + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;

                    txtFinancialYear.Text = Convert.ToString(drdShowRecord["FA_YEAR"]);
                    txtQuarter.Text = Convert.ToString(drdShowRecord["QTR"]);
                    txtCompanyName.Text = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                    //
                    txtTANNo.Text = Convert.ToString(drdShowRecord["TAN_NO"]); //-- 2026/01/13
                    //
                    txtFormNo.Text = Convert.ToString(drdShowRecord["FORM_NO"]);
                    //
                    //'138 (24Q)'
                    txtFormNo.Text = TdsMan.GetFormNoIT2025(Convert.ToString(drdShowRecord["FORM_NO"])) + " (" + txtFormNo.Text + ")";
                    //
                    txtReceiptNo.Text = Convert.ToString(drdShowRecord["RECEIPT_NO"]);
                    mskDateOfFiling.Text = Convert.ToString(drdShowRecord["DATE_OF_FILING"]);
                    txtPRNNo.Text = Convert.ToString(drdShowRecord["PRN_NO"]);
                    //--
                    txtScannedImagePath.Text = Convert.ToString(drdShowRecord["RECEIPT_IMAGE_PATH"]);
                    txtScannedImagePathM.Text = Convert.ToString(drdShowRecord["RECEIPT_IMAGE_PATH"]);
                    if(txtScannedImagePath.Text.Trim() == "")
                        btnPreviewImage.Enabled = false;
                    else
                        btnPreviewImage.Enabled = true;
                    //
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        lblComment.Text = "The Receipt file saved in the installed folder with name <" + TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder + ">.\nFolder Path :- " + strDatabasePath + "\\" + TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder;
                    else
                        lblComment.Text = "The Receipt file saved in the server with folder name <" + TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder + ">.\nFolder Path :- " + strDatabasePath + "\\" + TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder;
                    //--
                    txtReceiptNo.Select();

                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
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
                    //if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "DEDUCTEE_ID", lngSearchId);
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
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "BASIC_INFO_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (cmbFinancialYearSearch.SelectedIndex <= 0 &&
                            cmbQuarterSearch.SelectedIndex <= 0 &&
                            txtCompanyNameSearch.Text.Trim() == "" &&
                            txtTANSearch.Text.Trim() == "" &&
                            cmbFormNoSearch.SelectedIndex <= 0 &&
                            txtReceiptNoSearch.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            cmbFinancialYearSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    //-----------------------------------------------------------------------
                    //-- FINANCIAL YEAR
                    //-----------------------------------------------------------------------
                    if (txtFinancialYear.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Financial Year - Cannot be Blank");
                        txtFinancialYear.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- QUARTER
                    //-----------------------------------------------------------------------
                    if (txtQuarter.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Quarter - Cannot be Blank");
                        txtQuarter.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- FORM NO
                    //-----------------------------------------------------------------------
                    if (txtFormNo.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Form No. - Cannot be Blank");
                        txtFormNo.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- RECEIPT NO
                    //-----------------------------------------------------------------------
                    //if (txtReceiptNo.Text.Trim() == "")
                    //{
                    //    cmnService.J_UserMessage("Receipt No. - Cannot be Blank");
                    //    txtReceiptNo.Select();
                    //    return false;
                    //}
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtReceiptNo.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Receipt No. - '^' not allowed");
                        txtReceiptNo.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtPRNNo.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Token No. - '^' not allowed");
                        txtPRNNo.Select();
                        return false;
                    }
                    //----------------------------------------------------------
                    //-- VALID DATE CHECK
                    //----------------------------------------------------------
                    if (dtService.J_IsBlankDateCheck(ref mskDateOfFiling, J_ShowMessage.NO) == false)
                    {
                        if (dtService.J_IsDateValid(mskDateOfFiling) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the Date of Filing");
                            mskDateOfFiling.Select();
                            return false;
                        }
                    }
                    else
                    {
                        cmnService.J_UserMessage("Date of Filing - Cannot be Blank");
                        mskDateOfFiling.Select();
                        return false;
                    }
                    //-- 
                    if (txtPRNNo.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Token No. - Cannot be Blank");
                        txtPRNNo.Select();
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
                int intUpdateFlag = 0;
                //                
                //--------------------------------------------
                string strDateOfFiling = "";
                //
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        //*****  For Insert
                        break;
                    case J_Mode.Edit:
                        //*****  For Modify
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;
                        //-----------------------------------------------------------
                        if (dtService.J_IsBlankDateCheck(ref mskDateOfFiling, J_ShowMessage.NO) == true)
                        {
                            strDateOfFiling = "NULL";
                        }
                        else
                        {
                            strDateOfFiling = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDateOfFiling) + cmnService.J_DateOperator();
                        }

                        if (txtReceiptNo.Text.Trim() != "" || strDateOfFiling != "NULL" || txtPRNNo.Text.Trim() != "")
                            intUpdateFlag = 1;
                        else
                            intUpdateFlag = 0;

                        //-- COPY/DELETE THE IMAGE FILE
                        if (txtScannedImagePath.Text.Trim() == "")
                        {
                            if (txtScannedImagePathM.Text.Trim() != "")
                            {
                                if (System.IO.File.Exists(txtScannedImagePathM.Text.Trim()) == false)
                                    File.Delete(txtScannedImagePathM.Text.Trim());
                            }
                        }
                        //--
                        if (txtScannedImagePath.Text.Trim() != txtScannedImagePathM.Text.Trim())
                        {
                            if (System.IO.Directory.Exists(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder)) == false)
                                System.IO.Directory.CreateDirectory(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder));
                            //
                            if (System.IO.File.Exists(Path.Combine(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder), lngSearchId + Path.GetExtension(txtScannedImagePath.Text.Trim()))) == false)
                                File.Copy(txtScannedImagePath.Text.Trim(), Path.Combine(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder), lngSearchId + Path.GetExtension(txtScannedImagePath.Text.Trim())));
                            //
                            txtScannedImagePath.Text = Path.Combine(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder), lngSearchId + Path.GetExtension(txtScannedImagePath.Text.Trim()));
                        }
                        //--
                        //if (cmnService.J_SaveConfirmationMessage(ref cmbFormNo) == true) return;
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        strSQL = "UPDATE TRN_BASIC_INFO " +
                                 "SET    RECEIPT_NO     ='" + cmnService.J_ReplaceQuote(txtReceiptNo.Text.Trim()) + "'," +
                                 "       DATE_OF_FILING = " + strDateOfFiling + "," +
                                 "       PRN_NO         ='" + cmnService.J_ReplaceQuote(txtPRNNo.Text.Trim()) + "'," +
                                 "       RECEIPT_IMAGE_PATH ='" + cmnService.J_ReplaceQuote(txtScannedImagePath.Text.Trim()) + "' " +
                                 "WHERE  BASIC_INFO_ID  = " + lngSearchId + "";
                        //----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            txtReceiptNo.Select();
                            return;
                        }
                        //-----------------------------------------------------------
                        strSQL = "UPDATE TRN_COMPANY_INFO " +
                                 "SET    UPDATE_FLAG   = " + intUpdateFlag + " " +
                                 "WHERE  BASIC_INFO_ID = " + lngSearchId + "";
                        //----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            txtReceiptNo.Select();
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
                        BtnAdd.Enabled = false;
                        BtnAdd.BackColor = Color.LightGray;
                        BtnSearch.Enabled = false;
                        BtnSearch.BackColor = Color.LightGray;
                        dgvGrid.Visible = true;
                        //-----------------------------------------------------------
                        //BLOCKED BY DHRUB ON 2015_11_12
                        //BtnDelete.Enabled = false;
                        //BtnDelete.BackColor = Color.LightGray;
                        //-----------------------------------------------------------
                        //-----------------------------------------------------------
                        //DisableControls();
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "BASIC_INFO_ID", lngSearchId);
                        //-----------------------------------------------------------
                        //-- ADDED BY DHRUB ON @2015-11-16
                        //-----------------------------------------------------------
                        ClearButtonVisibilityState();
                        //
                        if (TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard == true)
                        {
                            TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard = false;
                            //
                            GC.Collect();
                            //
                            dmlService.Dispose();
                            this.Close();
                            this.Dispose();
                        }
                        break;
                    case J_Mode.Delete:
                        break;
                }

                //Added by Shrey Kejriwal on 05/02/2013
                grpFilter.Visible = true;
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region ClearButtonVisibilityState
        //---------------------------------------
        //-- ADDED BY DHRUB ON @2015-11-16
        //-- TO CLEAR RECEIPT NO 
        //---------------------------------------
        public void ClearButtonVisibilityState()
        {
            if (lblMode.Text != J_Mode.Edit && dgvGrid.CurrentRow != null)// ViewGrid.VisibleRowCount> 0)
            {
                //if (Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 6]) != "" ||   // TOKEN NO
                //    Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 7]) != "" ||   // DATE OF FILING
                //    Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 8]) != "")     // RECEIPT NO
                if (Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[6].Value) != "" ||   // TOKEN NO
                    Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[7].Value) != "" ||   // DATE OF FILING
                    Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[8].Value) != "")     // RECEIPT NO
                {
                    BtnDelete.Enabled = true;
                    BtnDelete.BackColor = Color.Lavender;
                }
                else
                {
                    BtnDelete.Enabled = false;
                    BtnDelete.BackColor = Color.LightGray;
                }
            }
            //---------------------------------------------------
        }
        #endregion 

        
        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=whLWes0us0o");                      
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0016", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            //System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0026", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0123", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion

        #region BtnPrint_Click
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //--
                //if (ViewGrid.VisibleRowCount <= 0)
                if(dgvGrid.CurrentRow == null)
                {
                    cmnService.J_UserMessage("No record exists");
                    BtnAdd.Select();
                    return;
                }
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
                string strForm = cmbFormNoFilter.Text;
                int intStart = strForm.IndexOf('(');
                int intEnd = strForm.IndexOf(')');
                if (intStart >= 0 && intEnd > intStart)
                {
                    strForm = strForm.Substring(intStart + 1, intEnd - intStart - 1);
                }
                //--
                strSQL = "SELECT TRN_BASIC_INFO.BASIC_INFO_ID  AS BASIC_INFO_ID," +
                    "              MST_ASSESSMENT.FA_YEAR        AS FA_YEAR," +
                    "              TRN_BASIC_INFO.FORM_NO        AS FORM_NO," +
                    "              TRN_BASIC_INFO.QTR            AS QTR," +
                    "              MST_COMPANY.COMPANY_NAME      AS COMPANY_NAME," +
                    "              MST_COMPANY.TAN_NO            AS TAN_NO," +
                    "              TRN_BASIC_INFO.PRN_NO         AS TOKEN_NO, " +
                    //--------------------------------------------------------------------------------------------------------------------------------
                    //--ADDED BY DHRUB ON @2015-11-16 [TO MAKE CLEAR BUTTON DISABLE WHILE NONE OF DATA EXISTS INTO PNR_NO,DATE_OF_FILING,RECEIPT_NO]
                    //-- TO CLEAR RECEIPT_NO DETAILS IN ONE CLICK.
                    //--------------------------------------------------------------------------------------------------------------------------------
                    "              " + cmnService.J_SQLDBFormat(cmnService.J_SQLDBFormat("TRN_BASIC_INFO.DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY), J_SQLColFormat.ConvertToString) + " AS DATE_OF_FILING," +
                    "              TRN_BASIC_INFO.RECEIPT_NO     AS RECEIPT_NO " +
                    "       FROM   TRN_BASIC_INFO," +
                    "              MST_COMPANY," +
                    "              MST_ASSESSMENT " +
                    "       WHERE  TRN_BASIC_INFO.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                    "       AND    TRN_BASIC_INFO.ASST_ID    = MST_ASSESSMENT.ASST_ID ";
                //APPLYLING FILTER FOR FINANCIAL YEAR
                if (cmbFinancialYearFilter.SelectedIndex > 0)
                    strSQL += "AND TRN_BASIC_INFO.ASST_ID = " + Support.GetItemData(cmbFinancialYearFilter, cmbFinancialYearFilter.SelectedIndex) + " ";

                //APPLYING FILTER FOR COMPANY
                if (cmbCompanyFilter.SelectedIndex > 0)
                    strSQL += "AND TRN_BASIC_INFO.COMPANY_ID = " + Support.GetItemData(cmbCompanyFilter, cmbCompanyFilter.SelectedIndex) + " ";

                //APPLYING FILTER FOR FORM NO
                if (cmbFormNoFilter.SelectedIndex > 0)
                    strSQL += "AND TRN_BASIC_INFO.FORM_NO = '" + strForm + "' ";

                //APPLYING FILTER FOR QUARTER
                if (cmbQuarterFilter.SelectedIndex > 0)
                    strSQL += "AND TRN_BASIC_INFO.QTR = '" + cmbQuarterFilter.Text + "' ";
                //-----------------------------------------------------------
                strSQL = strSQL + " ORDER BY MST_ASSESSMENT.FA_YEAR, TRN_BASIC_INFO.QTR, MST_COMPANY.COMPANY_NAME, TRN_BASIC_INFO.FORM_NO";
                //--
                strFileName = "XL_RECEIPT_NO_" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".XLSX";
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
                    if (CREATE_NEW_WORKSHEET(Path.Combine(strPath, strFileName), "RECEIPT NO. LIST") == false) return;
                    //
                    if (WRITE_RECEIPT_NO_REG_WORKSHEET(Path.Combine(strPath, strFileName), "RECEIPT NO. LIST", strSQL) == false) return;
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
                wsnew.get_Range("A1", m).Value2 = "Tax Year";
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
                wsnew.get_Range("B1", m).Value2 = "Form No.";
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
                wsnew.get_Range("C1", m).Value2 = "Quarter";
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
                wsnew.get_Range("D1", m).Value2 = "Company Name";
                wsnew.get_Range("D1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("D1", m).Borders.Value = true;
                wsnew.get_Range("D1", m).ColumnWidth = 50;
                wsnew.get_Range("D1", m).WrapText = true;
                wsnew.get_Range("D1", m).Font.Name = "Arial";
                wsnew.get_Range("D1", m).Font.Bold = true;
                wsnew.get_Range("D1", m).Font.Size = 10;
                wsnew.get_Range("D1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("D1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("E1", m).Value2 = "TAN";
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
                wsnew.get_Range("F1", m).Value2 = "Receipt No.";
                wsnew.get_Range("F1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("F1", m).Borders.Value = true;
                wsnew.get_Range("F1", m).ColumnWidth = 65;
                wsnew.get_Range("F1", m).WrapText = true;
                wsnew.get_Range("F1", m).Font.Name = "Arial";
                wsnew.get_Range("F1", m).Font.Bold = true;
                wsnew.get_Range("F1", m).Font.Size = 10;
                wsnew.get_Range("F1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("F1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("G1", m).Value2 = "Date of Filing";
                wsnew.get_Range("G1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("G1", m).Borders.Value = true;
                wsnew.get_Range("G1", m).ColumnWidth = 40;
                wsnew.get_Range("G1", m).WrapText = true;
                wsnew.get_Range("G1", m).Font.Name = "Arial";
                wsnew.get_Range("G1", m).Font.Bold = true;
                wsnew.get_Range("G1", m).Font.Size = 10;
                wsnew.get_Range("G1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("G1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("H1", m).Value2 = "Token No.";
                wsnew.get_Range("H1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("H1", m).Borders.Value = true;
                wsnew.get_Range("H1", m).ColumnWidth = 65;
                wsnew.get_Range("H1", m).WrapText = true;
                wsnew.get_Range("H1", m).Font.Name = "Arial";
                wsnew.get_Range("H1", m).Font.Bold = true;
                wsnew.get_Range("H1", m).Font.Size = 10;
                wsnew.get_Range("H1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("H1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
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
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["FA_YEAR"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["FORM_NO"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("C" + lngSheetRow, m).Value2 = drdGetSheetRecord["QTR"].ToString();
                    wsnew.get_Range("C" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("D" + lngSheetRow, m).Value2 = drdGetSheetRecord["COMPANY_NAME"].ToString();
                    wsnew.get_Range("D" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("E" + lngSheetRow, m).Value2 =  drdGetSheetRecord["TAN_NO"].ToString();
                    wsnew.get_Range("E" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("F" + lngSheetRow, m).Value2 = "'" + drdGetSheetRecord["RECEIPT_NO"].ToString();
                    //wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("F" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("G" + lngSheetRow, m).Value2 = drdGetSheetRecord["DATE_OF_FILING"].ToString();
                    wsnew.get_Range("G" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("G" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("G" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("G" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("H" + lngSheetRow, m).Value2 = "'" + drdGetSheetRecord["TOKEN_NO"].ToString();
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

        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new FormTrn.TrnTANSearch("MstReceiptNo");
            Tan.ShowDialog();
            //--------------
            cmbCompanyFilter.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #region ChkBlankTokenNo_CheckedChanged
        #endregion

        #region FetchAckFilingDateAndRRR
        //private (string AckNo, string FilingDate, string RRRNo)
        //        FetchAckFilingDateAndRRR(
        //            string tan,
        //            string password,
        //            string financialYear,
        //            string quarter,
        //            string formType
        //        )
        //{

        //    string strOS = TdsMan.GetOSVersion();
        //    string strBrowser = strOS == "XP"
        //                        ? TdsMan.GetSystemDefaultBrowserXP()
        //                        : TdsMan.GetSystemDefaultBrowser();

        //    IWebDriver driver = null;

        //    try
        //    {
        //        // =========================
        //        // 1. BROWSER + LOGIN
        //        // =========================
        //        // REUSE YOUR EXISTING CODE HERE (UNCHANGED)
        //        // - browser launch
        //        // - login using TAN + password
        //        // - reach Dashboard
        //        //
        //        // driver = new ChromeDriver(...) / EdgeDriver(...)
        //        // driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");
        //        // ...
        //        // Dashboard loaded
        //        // =========================
        //        if (strBrowser.Contains("chrome"))
        //        {
        //            var service = ChromeDriverService.CreateDefaultService();
        //            service.HideCommandPromptWindow = true;

        //            var options = new ChromeOptions();
        //            options.AddArgument("--start-maximized");
        //            options.AddArgument("--disable-web-security");
        //            options.AddArgument("--no-proxy-server");
        //            options.AddArgument("--no-sandbox");
        //            options.AddArgument("--disable-blink-features=AutomationControlled");
        //            options.AddUserProfilePreference("credentials_enable_service", false);
        //            options.AddUserProfilePreference("profile.password_manager_enabled", false);

        //            // Use a fresh temp profile directory (isolated)
        //            string tempProfileDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        //            options.AddArgument($"--user-data-dir={tempProfileDir}");

        //            driver = new ChromeDriver(service, options);

        //        }
        //        else if (strBrowser.Contains("IE") || strBrowser.Contains("iexplore.exe"))
        //        {
        //            InternetExplorerDriverService serv = InternetExplorerDriverService.CreateDefaultService();
        //            serv.HideCommandPromptWindow = true;
        //            var options1 = new InternetExplorerOptions();
        //            driver = new InternetExplorerDriver(serv, options1);
        //        }
        //        else if (strBrowser.Contains("MSEdge"))
        //        {
        //            ////EdgeDriverService serv = EdgeDriverService.CreateDefaultService();
        //            ////serv.HideCommandPromptWindow = true;
        //            ////var options3 = new EdgeOptions();
        //            ////driver = new EdgeDriver(serv, options3);
        //            var service = EdgeDriverService.CreateDefaultService();
        //            service.HideCommandPromptWindow = true;

        //            var options = new EdgeOptions();
        //            options.AddArgument("start-maximized");
        //            options.AddArgument("disable-web-security");
        //            options.AddArgument("no-proxy-server");
        //            options.AddArgument("no-sandbox");
        //            options.AddArgument("disable-blink-features=AutomationControlled");

        //            //string tempProfileDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        //            //options.AddArgument($"--user-data-dir={tempProfileDir}");
        //            // Ensure the folder is not already locked or in use
        //            string tempProfileDir = Path.Combine(Path.GetTempPath(), "TDS_" + Guid.NewGuid().ToString());
        //            if (Directory.Exists(tempProfileDir))
        //            {
        //                try { Directory.Delete(tempProfileDir, true); } catch { /* Ignore any cleanup errors */ }
        //            }
        //            Directory.CreateDirectory(tempProfileDir); // Forcefully create it

        //            options.AddArgument($"--user-data-dir={tempProfileDir}");

        //            driver = new EdgeDriver(service, options);
        //        }
        //        else
        //        {
        //            cmnService.J_UserMessage("Please set your default browser to 'Chrome' / 'Edge' / 'Internet Explorer'.");
        //            return ("", "", "");
        //        }

        //        // ==== Validate driver ====
        //        if (driver == null)
        //        {
        //            MessageBox.Show("Browser driver could not be launched.");
        //            return ("", "", "");
        //        }

        //        // ==== Main automation ====
        //        driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");

        //        System.Threading.Thread.Sleep(3000);
        //        IWebElement inputTextBox = driver.FindElement(By.Name("panAdhaarUserId"));
        //        inputTextBox.SendKeys(tan);
        //        //inputTextBox.SendKeys("CALP08143C");

        //        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //        System.Threading.Thread.Sleep(2000);
        //        driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

        //        System.Threading.Thread.Sleep(2000);
        //        driver.FindElement(By.XPath("//mat-checkbox[@id='passwordCheckBox']")).Click();
        //        System.Threading.Thread.Sleep(2000);

        //        inputTextBox = driver.FindElement(By.XPath("//input[@type='password' and @name='loginPasswordField']"));
        //        inputTextBox.SendKeys(password);
        //        //inputTextBox.SendKeys("Pdsinfo@6");

        //        System.Threading.Thread.Sleep(3000);
        //        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[span[contains(text(),'Continue')]]"))).Click();


        //        System.Threading.Thread.Sleep(3000);
        //        //var modalDialogs = driver.FindElements(By.XPath("//div[contains(@class,'modal-dialog modal-dialog-centered')]"));
        //        //////var modalDialogs = driver.FindElements(By.XPath("//div[contains(@class,'modal-content')]"));

        //        //////if (modalDialogs.Count > 6)
        //        //////{
        //        //////    // Look for the 'Login Here' button inside the modal
        //        //////    var loginHereButton = driver.FindElement(By.XPath("//button[contains(text(),'Login Here')]"));

        //        //////    if (loginHereButton.Displayed && loginHereButton.Enabled)
        //        //////    {
        //        //////        loginHereButton.Click();
        //        //////    }
        //        //////}
        //        try
        //        {
        //            WebDriverWait shortWait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

        //            var loginHereBtn = shortWait.Until(
        //                ExpectedConditions.ElementToBeClickable(
        //                    By.XPath("//button[@data-dismiss='modal' and contains(text(),'Login Here')]")
        //                )
        //            );

        //            loginHereBtn.Click();
        //            System.Threading.Thread.Sleep(1500); // allow session switch
        //        }
        //        catch (WebDriverTimeoutException)
        //        {
        //            // Normal login – popup did not appear
        //        }

        //        WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        //        Actions action = new Actions(driver);

        //        // =========================
        //        // 2. DASHBOARD > VIEW FILED FORMS
        //        // =========================
        //        var eFileMenu = wait1.Until(ExpectedConditions.ElementIsVisible(By.Id("e-File")));
        //        action.MoveToElement(eFileMenu).Perform();

        //        var itForms = wait1.Until(ExpectedConditions.ElementIsVisible(
        //            By.XPath("//span[contains(text(),'Income Tax Forms')]")));
        //        action.MoveToElement(itForms).Perform();

        //        wait1.Until(ExpectedConditions.ElementToBeClickable(
        //            By.XPath("//span[contains(text(),'View Filed Forms')]"))).Click();

        //        // =========================
        //        // 3. SELECT FORM
        //        // =========================
        //        wait1.Until(ExpectedConditions.ElementToBeClickable(
        //            By.XPath($"//div[contains(@class,'headFormNameStyle') and contains(text(),'{formType}')]")
        //        )).Click();
        //        //
        //        string fy = financialYear;   // "2025-26"
        //        string qtr = quarter;        // "Q2"

        //        // =========================
        //        // Locate the correct FY + Quarter CARD
        //        // =========================
        //        //var filingCard = wait.Until(ExpectedConditions.ElementIsVisible(
        //        //    By.XPath($@"
        //        //                //mat-card-title
        //        //                    [contains(normalize-space(text()), 'F.Y.{fy}')]
        //        //                    [span[contains(text(),'{qtr}')]]
        //        //                /ancestor::mat-card
        //        //            ")
        //        //));
        //        IWebElement filingCard = null;
        //        bool found = false;

        //        while (true)
        //        {
        //            // Try to find the card on current page
        //            var cards = driver.FindElements(By.XPath($@"
        //                                                //mat-card-title
        //                                                    [contains(normalize-space(.), 'F.Y.{fy}')]
        //                                                    [contains(normalize-space(.), '({qtr})')]
        //                                                /ancestor::mat-card
        //                                            "));

        //            if (cards.Count > 0)
        //            {
        //                filingCard = cards[0];
        //                found = true;
        //                break;
        //            }

        //            // ===== PAGINATION SAFE NEXT PAGE =====
        //            var paginator = driver.FindElements(By.XPath("//mat-paginator"));

        //            if (paginator.Count == 0)
        //            {
        //                break; // no paginator exists
        //            }

        //            // read current page text: "1 - 5 of 7"
        //            string pageInfoBefore = driver.FindElement(
        //                By.XPath("//div[contains(@class,'mat-mdc-paginator-range-label')]")
        //            ).Text;

        //            // find NEXT button (even if visually disabled)
        //            var nextBtn = driver.FindElement(By.XPath(
        //                "//button[@aria-label='Next page']"
        //            ));

        //            // check disabled via attribute
        //            string isDisabled = nextBtn.GetAttribute("disabled");
        //            if (!string.IsNullOrEmpty(isDisabled))
        //            {
        //                break; // last page reached
        //            }

        //            // scroll & JS click
        //            ((IJavaScriptExecutor)driver).ExecuteScript(
        //                "arguments[0].scrollIntoView({block:'center'});", nextBtn);

        //            System.Threading.Thread.Sleep(300);

        //            ((IJavaScriptExecutor)driver).ExecuteScript(
        //                "arguments[0].click();", nextBtn);

        //            // wait for page change
        //            WebDriverWait pageWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        //            pageWait.Until(d =>
        //            {
        //                string pageInfoAfter = d.FindElement(
        //                    By.XPath("//div[contains(@class,'mat-mdc-paginator-range-label')]")
        //                ).Text;

        //                return pageInfoAfter != pageInfoBefore;
        //            });

        //            System.Threading.Thread.Sleep(800);
        //        }

        //        // ---- FINAL VALIDATION ----
        //        if (!found || filingCard == null)
        //        {
        //            throw new Exception(
        //                $"Return not found for FY {fy} {qtr}. Please verify on IT portal."
        //            );
        //            return ("", "", "");
        //        }
        //        // =========================
        //        // 6. ACKNOWLEDGEMENT NO
        //        // =========================
        //        string ackNo = filingCard.FindElement(By.XPath(".//span[contains(@class,'thirdColKey') and contains(text(),'Acknowledgement No')]" +
        //                                                         "/following-sibling::span[contains(@class,'thirdColValue')]")
        //                                              ).Text.Trim();
        //        // =========================
        //        // 7. FORM SUBMITTED DATE
        //        // =========================
        //        string filingDate = filingCard.FindElement(By.XPath(".//div[contains(text(),'Form submitted')]/following-sibling::div")
        //                                                ).Text.Trim();
        //        // =========================
        //        // 8. OPEN RRR MODAL
        //        // =========================
        //        filingCard.FindElement(
        //                            By.XPath(".//span[contains(text(),'RRR Number')]/following-sibling::span[contains(text(),'View')]")
        //                        ).Click();

        //        // =========================
        //        // 9. READ RRR NUMBER
        //        // =========================
        //        var rrrModal = wait1.Until(ExpectedConditions.ElementIsVisible(
        //            By.XPath("//div[contains(@class,'detailscardmodal')]")
        //        ));

        //        string rrrNo = rrrModal.FindElement(
        //            By.XPath(".//div[contains(@class,'modaldetail4')]")
        //        ).Text.Trim();

        //        // CLOSE BROWSER

        //        driver?.Quit();

        //        return (ackNo, filingDate, rrrNo);

        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show("Error during upload:\n" + ex.Message, "Upload TDS/TCS", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        //MessageBox.Show(ex.Message);
        //        cmnService.J_UserMessage(ex.Message);
        //        return ("", "", "");
        //    }
        //    //finally
        //    //{
        //    //    // Optional: keep browser open for debugging
        //    //    // driver?.Quit();
        //    //}
        //}
        #endregion

        //-- 2026/02/06
        #region FetchAckFilingDateAndRRR 
        private (string AckNo, string FilingDate, string RRRNo)
                FetchAckFilingDateAndRRR(
                    string tan,
                    string password,
                    string financialYear,
                    string quarter,
                    string FormNo
                )
        {

            string strOS = TdsMan.GetOSVersion();
            string strBrowser = strOS == "XP"
                                ? TdsMan.GetSystemDefaultBrowserXP()
                                : TdsMan.GetSystemDefaultBrowser();

            IWebDriver driver = null;

            try
            {
                // =========================
                // 1. BROWSER + LOGIN
                // =========================
                // REUSE YOUR EXISTING CODE HERE (UNCHANGED)
                // - browser launch
                // - login using TAN + password
                // - reach Dashboard
                //
                // driver = new ChromeDriver(...) / EdgeDriver(...)
                // driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");
                // ...
                // Dashboard loaded
                // =========================
                if (strBrowser.Contains("chrome"))
                {
                    var service = ChromeDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true;

                    var options = new ChromeOptions();
                    options.AddArgument("--start-maximized");
                    options.AddArgument("--disable-web-security");
                    options.AddArgument("--no-proxy-server");
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--disable-blink-features=AutomationControlled");
                    options.AddUserProfilePreference("credentials_enable_service", false);
                    options.AddUserProfilePreference("profile.password_manager_enabled", false);

                    // Use a fresh temp profile directory (isolated)
                    string tempProfileDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                    options.AddArgument($"--user-data-dir={tempProfileDir}");

                    driver = new ChromeDriver(service, options);

                }
                else if (strBrowser.Contains("IE") || strBrowser.Contains("iexplore.exe"))
                {
                    InternetExplorerDriverService serv = InternetExplorerDriverService.CreateDefaultService();
                    serv.HideCommandPromptWindow = true;
                    var options1 = new InternetExplorerOptions();
                    driver = new InternetExplorerDriver(serv, options1);
                }
                else if (strBrowser.Contains("MSEdge"))
                {
                    //////EdgeDriverService serv = EdgeDriverService.CreateDefaultService();
                    //////serv.HideCommandPromptWindow = true;
                    //////var options3 = new EdgeOptions();
                    //////driver = new EdgeDriver(serv, options3);
                    //var service = EdgeDriverService.CreateDefaultService();
                    //service.HideCommandPromptWindow = true;

                    //var options = new EdgeOptions();
                    //options.AddArgument("start-maximized");
                    //options.AddArgument("disable-web-security");
                    //options.AddArgument("no-proxy-server");
                    //options.AddArgument("no-sandbox");
                    //options.AddArgument("disable-blink-features=AutomationControlled");

                    ////string tempProfileDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                    ////options.AddArgument($"--user-data-dir={tempProfileDir}");
                    //// Ensure the folder is not already locked or in use
                    //string tempProfileDir = Path.Combine(Path.GetTempPath(), "TDS_" + Guid.NewGuid().ToString());
                    //if (Directory.Exists(tempProfileDir))
                    //{
                    //    try { Directory.Delete(tempProfileDir, true); } catch { /* Ignore any cleanup errors */ }
                    //}
                    //Directory.CreateDirectory(tempProfileDir); // Forcefully create it

                    //options.AddArgument($"--user-data-dir={tempProfileDir}");

                    //driver = new EdgeDriver(service, options);

                    string driverPath = Path.Combine(Application.StartupPath);//, "Drivers");

                    var service = EdgeDriverService.CreateDefaultService(driverPath, "msedgedriver.exe");
                    service.HideCommandPromptWindow = true;

                    var options = new EdgeOptions();

                    options.AddArgument("--start-maximized");
                    options.AddArgument("--disable-web-security");
                    options.AddArgument("--no-proxy-server");
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--disable-blink-features=AutomationControlled");

                    string tempProfileDir = Path.Combine(Path.GetTempPath(), "TDS_" + Guid.NewGuid().ToString());
                    Directory.CreateDirectory(tempProfileDir);

                    options.AddArgument($"--user-data-dir={tempProfileDir}");

                    driver = new EdgeDriver(service, options);
                }
                else
                {
                    cmnService.J_UserMessage("Please set your default browser to 'Chrome' / 'Edge' / 'Internet Explorer'.");
                    return ("", "", "");
                }

                // ==== Validate driver ====
                if (driver == null)
                {
                    MessageBox.Show("Browser driver could not be launched.");
                    return ("", "", "");
                }

                // ==== Main automation ====
                driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");

                System.Threading.Thread.Sleep(3000);
                IWebElement inputTextBox = driver.FindElement(By.Name("panAdhaarUserId"));
                inputTextBox.SendKeys(tan);
                //inputTextBox.SendKeys("CALP08143C");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath("//mat-checkbox[@id='passwordCheckBox']")).Click();
                System.Threading.Thread.Sleep(2000);

                inputTextBox = driver.FindElement(By.XPath("//input[@type='password' and @name='loginPasswordField']"));
                inputTextBox.SendKeys(password);
                //inputTextBox.SendKeys("Pdsinfo@6");

                System.Threading.Thread.Sleep(3000);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[span[contains(text(),'Continue')]]"))).Click();


                System.Threading.Thread.Sleep(3000);
                //var modalDialogs = driver.FindElements(By.XPath("//div[contains(@class,'modal-dialog modal-dialog-centered')]"));
                //////var modalDialogs = driver.FindElements(By.XPath("//div[contains(@class,'modal-content')]"));

                //////if (modalDialogs.Count > 6)
                //////{
                //////    // Look for the 'Login Here' button inside the modal
                //////    var loginHereButton = driver.FindElement(By.XPath("//button[contains(text(),'Login Here')]"));

                //////    if (loginHereButton.Displayed && loginHereButton.Enabled)
                //////    {
                //////        loginHereButton.Click();
                //////    }
                //////}
                try
                {
                    WebDriverWait shortWait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

                    var loginHereBtn = shortWait.Until(
                        ExpectedConditions.ElementToBeClickable(
                            By.XPath("//button[@data-dismiss='modal' and contains(text(),'Login Here')]")
                        )
                    );

                    loginHereBtn.Click();
                    System.Threading.Thread.Sleep(1500); // allow session switch
                }
                catch (WebDriverTimeoutException)
                {
                    // Normal login – popup did not appear
                }

                WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                Actions action = new Actions(driver);

                // =========================
                // 2. DASHBOARD > VIEW FILED FORMS
                // =========================
                var eFileMenu = wait1.Until(ExpectedConditions.ElementIsVisible(By.Id("e-File")));
                action.MoveToElement(eFileMenu).Perform();

                var itForms = wait1.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//span[contains(text(),'Income Tax Forms')]")));
                action.MoveToElement(itForms).Perform();

                wait1.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//span[contains(text(),'View Filed Forms')]"))).Click();

                System.Threading.Thread.Sleep(2000);

                #region COMMENTED

                //if (formType == "24Q")
                //{
                //    System.Threading.Thread.Sleep(1000);
                //    var searchBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Search by Form Name/No.']")));
                //    searchBox.Clear();
                //    searchBox.SendKeys("24Q");
                //    searchBox.SendKeys(OpenQA.Selenium.Keys.Enter);

                //    System.Threading.Thread.Sleep(1000);
                //    new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[normalize-space()='View All']"))).Click();
                //}
                //if (formType == "26Q")
                //{
                //    System.Threading.Thread.Sleep(1000);
                //    var searchBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Search by Form Name/No.']")));
                //    searchBox.Clear();
                //    searchBox.SendKeys("26Q");
                //    searchBox.SendKeys(OpenQA.Selenium.Keys.Enter);

                //    System.Threading.Thread.Sleep(1000);
                //    new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[normalize-space()='View All']"))).Click();
                //}
                //if (formType == "27Q")
                //{
                //    System.Threading.Thread.Sleep(1000);
                //    var searchBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Search by Form Name/No.']")));
                //    searchBox.Clear();
                //    searchBox.SendKeys("27Q");
                //    searchBox.SendKeys(OpenQA.Selenium.Keys.Enter);

                //    System.Threading.Thread.Sleep(1000);
                //    new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[normalize-space()='View All']"))).Click();
                //}
                //if (formType == "27EQ")
                //{
                //    System.Threading.Thread.Sleep(1000);
                //    var searchBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Search by Form Name/No.']")));
                //    searchBox.Clear();
                //    searchBox.SendKeys("27EQ");
                //    searchBox.SendKeys(OpenQA.Selenium.Keys.Enter);

                //    System.Threading.Thread.Sleep(1000);
                //    new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[normalize-space()='View All']"))).Click();
                //}
                #endregion
                // ======================================================
                // SELECT CORRECT ACT TAB BASED ON FINANCIAL YEAR
                // ======================================================

                System.Threading.Thread.Sleep(1500);

                // financialYear expected as "2025-26", "2026-27" etc.
                int fyStartYear;

                if (string.IsNullOrWhiteSpace(financialYear) ||
                    financialYear.Length < 4 ||
                    !int.TryParse(financialYear.Substring(0, 4), out fyStartYear))
                {
                    throw new Exception("Invalid Financial Year: " + financialYear);
                }

                string requiredTab;

                // FY 2026-27 onwards -> Income Tax Act 2025
                if (fyStartYear >= 2026)
                {
                    requiredTab = "Forms as per Income Tax Act 2025";
                }
                else
                {
                    // FY 2025-26 and earlier -> Income Tax Act 1961
                    requiredTab = "Forms as per Income Tax Act 1961";
                }

                By requiredTabBy = By.XPath(
                    "//div[@role='tab']" +
                    "[.//span[contains(@class,'mdc-tab__text-label')" +
                    " and normalize-space()='" + requiredTab + "']]"
                );

                IWebElement actTab = wait1.Until(
                    ExpectedConditions.ElementToBeClickable(requiredTabBy)
                );

                // Click only if it is not already selected
                if (actTab.GetAttribute("aria-selected") != "true")
                {
                    try
                    {
                        actTab.Click();
                    }
                    catch
                    {
                        // Angular Material sometimes intercepts normal Selenium click
                        ((IJavaScriptExecutor)driver).ExecuteScript(
                            "arguments[0].click();", actTab);
                    }
                }

                // IMPORTANT:
                // Wait until Angular confirms that this tab is actually selected
                wait1.Until(d =>
                {
                    try
                    {
                        IWebElement tab = d.FindElement(requiredTabBy);

                        return tab.GetAttribute("aria-selected") == "true";
                    }
                    catch
                    {
                        return false;
                    }
                });

                // Small Angular rendering buffer
                System.Threading.Thread.Sleep(700);

                //System.Threading.Thread.Sleep(1000);
                //var searchBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Search by Form Name/No.']")));
                //searchBox.Clear();
                //searchBox.SendKeys(FormNo);
                //searchBox.SendKeys(OpenQA.Selenium.Keys.Enter);

                //System.Threading.Thread.Sleep(1000);
                //new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[normalize-space()='View All']"))).Click();
                // ======================================================
                // SEARCH FORM INSIDE CURRENTLY VISIBLE TAB
                // ======================================================

                IWebElement searchBox = wait1.Until(d =>
                {
                    var boxes = d.FindElements(
                        By.XPath("//input[@placeholder='Search by Form Name/No.']")
                    );

                    foreach (var box in boxes)
                    {
                        if (box.Displayed && box.Enabled)
                            return box;
                    }

                    return null;
                });

                searchBox.Clear();
                searchBox.SendKeys(FormNo);
                searchBox.SendKeys(OpenQA.Selenium.Keys.Enter);

                System.Threading.Thread.Sleep(800);

                // ======================================================
                // CLICK VISIBLE "VIEW ALL"
                // ======================================================

                IWebElement viewAll = wait1.Until(d =>
                {
                    var elements = d.FindElements(
                        By.XPath("//span[normalize-space()='View All']")
                    );

                    foreach (var el in elements)
                    {
                        if (el.Displayed && el.Enabled)
                            return el;
                    }

                    return null;
                });

                try
                {
                    viewAll.Click();
                }
                catch
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(
                        "arguments[0].click();", viewAll);
                }

                System.Threading.Thread.Sleep(800);
                //--
                #region COMMENT
                // =========================
                // 3. SELECT FORM
                // =========================
                //wait1.Until(ExpectedConditions.ElementToBeClickable(
                //    By.XPath($"//div[contains(@class,'headFormNameStyle') and contains(text(),'{formType}')]")
                //)).Click();
                //
                //string fy = financialYear;   // "2025-26"
                //string qtr = quarter;        // "Q2"

                //var fy = "F.Y." + financialYear;// "F.Y.2025-26";
                //var qtr ="(" + quarter + ")";// "(Q3)";

                //var cardTitle = driver.FindElement(By.XPath(
                //    $"//mat-card-title[contains(., '{fy}') and contains(., '{qtr}')]"
                //));

                //// =========================
                //// Locate the correct FY + Quarter CARD
                //// =========================
                ////var filingCard = wait.Until(ExpectedConditions.ElementIsVisible(
                ////    By.XPath($@"
                ////                //mat-card-title
                ////                    [contains(normalize-space(text()), 'F.Y.{fy}')]
                ////                    [span[contains(text(),'{qtr}')]]
                ////                /ancestor::mat-card
                ////            ")
                ////));
                //IWebElement filingCard = null;
                //bool found = false;

                //while (true)
                //{
                //    // Try to find the card on current page
                //    var cards = driver.FindElements(By.XPath($@"
                //                                        //mat-card-title
                //                                            [contains(normalize-space(.), 'F.Y.{fy}')]
                //                                            [contains(normalize-space(.), '({qtr})')]
                //                                        /ancestor::mat-card
                //                                    "));

                //    if (cards.Count > 0)
                //    {
                //        filingCard = cards[0];
                //        found = true;
                //        break;
                //    }

                //    // ===== PAGINATION SAFE NEXT PAGE =====
                //    var paginator = driver.FindElements(By.XPath("//mat-paginator"));

                //    if (paginator.Count == 0)
                //    {
                //        break; // no paginator exists
                //    }

                //    // read current page text: "1 - 5 of 7"
                //    string pageInfoBefore = driver.FindElement(
                //        By.XPath("//div[contains(@class,'mat-mdc-paginator-range-label')]")
                //    ).Text;

                //    // find NEXT button (even if visually disabled)
                //    var nextBtn = driver.FindElement(By.XPath(
                //        "//button[@aria-label='Next page']"
                //    ));

                //    // check disabled via attribute
                //    string isDisabled = nextBtn.GetAttribute("disabled");
                //    if (!string.IsNullOrEmpty(isDisabled))
                //    {
                //        break; // last page reached
                //    }

                //    // scroll & JS click
                //    ((IJavaScriptExecutor)driver).ExecuteScript(
                //        "arguments[0].scrollIntoView({block:'center'});", nextBtn);

                //    System.Threading.Thread.Sleep(300);

                //    ((IJavaScriptExecutor)driver).ExecuteScript(
                //        "arguments[0].click();", nextBtn);

                //    // wait for page change
                //    WebDriverWait pageWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                //    pageWait.Until(d =>
                //    {
                //        string pageInfoAfter = d.FindElement(
                //            By.XPath("//div[contains(@class,'mat-mdc-paginator-range-label')]")
                //        ).Text;

                //        return pageInfoAfter != pageInfoBefore;
                //    });

                //    System.Threading.Thread.Sleep(800);
                //}
                #endregion
                //--
                System.Threading.Thread.Sleep(800);
                //--

                // ======================================================
                // REPLACE FROM HERE
                // ======================================================

                var fy = financialYear;   // "2025-26"
                var qtr = quarter;        // "Q3"

                IWebElement filingCard = null;
                bool found = false;

                int pageCount = 0;
                const int MAX_PAGES = 25;

                while (pageCount < MAX_PAGES)
                {
                    pageCount++;

                    // ===== WAIT FOR VISIBLE CARDS =====
                    new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d =>
                    {
                        var titles = d.FindElements(By.XPath("//mat-card-title"));

                        foreach (var title in titles)
                        {
                            try
                            {
                                if (title.Displayed)
                                    return true;
                            }
                            catch
                            {
                            }
                        }

                        return false;
                    });

                    // ===== FIND REQUIRED FY + QUARTER =====
                    //                var cards = driver.FindElements(By.XPath($@"
                    //    //mat-card[
                    //        .//mat-card-title
                    //            [contains(normalize-space(.), 'F.Y.{fy}')]
                    //            [contains(normalize-space(.), '({qtr})')]
                    //    ]
                    //"));
                    if (fyStartYear >= 2026)
                    {
                        var cards = driver.FindElements(By.XPath($@"
                                            //mat-card[
                                                .//mat-card-title
                                                    [contains(normalize-space(.), 'T.Y.{fy}')]
                                                    [contains(normalize-space(.), '({qtr})')]
                                            ]
                                        "));

                        foreach (var card in cards)
                        {
                            try
                            {
                                if (card.Displayed)
                                {
                                    filingCard = card;
                                    found = true;
                                    break;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        var cards = driver.FindElements(By.XPath($@"
                                            //mat-card[
                                                .//mat-card-title
                                                    [contains(normalize-space(.), 'F.Y.{fy}')]
                                                    [contains(normalize-space(.), '({qtr})')]
                                            ]
                                        "));

                        foreach (var card in cards)
                        {
                            try
                            {
                                if (card.Displayed)
                                {
                                    filingCard = card;
                                    found = true;
                                    break;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }


                    if (found)
                        break;

                    // ===== READ CURRENT PAGE RANGE =====
                    string pageInfoBefore = "";

                    var rangeLabels = driver.FindElements(
                        By.XPath("//div[contains(@class,'mat-mdc-paginator-range-label')]")
                    );

                    foreach (var label in rangeLabels)
                    {
                        try
                        {
                            if (label.Displayed)
                            {
                                pageInfoBefore = label.Text.Trim();
                                break;
                            }
                        }
                        catch
                        {
                        }
                    }

                    // ===== FIND VISIBLE NEXT BUTTON =====
                    IWebElement nextBtn = null;

                    var nextButtons = driver.FindElements(
                        By.XPath("//button[@aria-label='Next page']")
                    );

                    foreach (var btn in nextButtons)
                    {
                        try
                        {
                            if (btn.Displayed &&
                                btn.Enabled &&
                                string.IsNullOrEmpty(btn.GetAttribute("disabled")))
                            {
                                nextBtn = btn;
                                break;
                            }
                        }
                        catch
                        {
                        }
                    }

                    // Last page reached
                    if (nextBtn == null)
                        break;

                    // ===== CLICK NEXT =====
                    ((IJavaScriptExecutor)driver).ExecuteScript(
                        "arguments[0].scrollIntoView({block:'center'});",
                        nextBtn
                    );

                    System.Threading.Thread.Sleep(200);

                    ((IJavaScriptExecutor)driver).ExecuteScript(
                        "arguments[0].click();",
                        nextBtn
                    );

                    // ===== WAIT FOR PAGE NUMBER/RANGE TO CHANGE =====
                    bool pageChanged = false;

                    try
                    {
                        new WebDriverWait(driver, TimeSpan.FromSeconds(7)).Until(d =>
                        {
                            var labelsAfter = d.FindElements(
                                By.XPath("//div[contains(@class,'mat-mdc-paginator-range-label')]")
                            );

                            foreach (var label in labelsAfter)
                            {
                                try
                                {
                                    if (label.Displayed)
                                    {
                                        string pageInfoAfter = label.Text.Trim();

                                        if (!string.IsNullOrEmpty(pageInfoAfter) &&
                                            pageInfoAfter != pageInfoBefore)
                                        {
                                            pageChanged = true;
                                            return true;
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }

                            return false;
                        });
                    }
                    catch (WebDriverTimeoutException)
                    {
                        pageChanged = false;
                    }

                    // Prevent infinite looping
                    if (!pageChanged)
                        break;

                    System.Threading.Thread.Sleep(500);
                }

                // ======================================================
                // REPLACEMENT ENDS HERE
                // ======================================================


                // KEEP YOUR EXISTING CODE BELOW UNCHANGED

                // ---- FINAL VALIDATION ----
                if (!found || filingCard == null)
                {
                    throw new Exception(
                        $"Return not found for FY {fy} {qtr}. Please verify on IT portal."
                    );

                    return ("", "", "");
                }

                // ---- FINAL VALIDATION ----
                if (!found || filingCard == null)
                {
                    throw new Exception(
                        $"Return not found for FY {fy} {qtr}. Please verify on IT portal."
                    );
                    return ("", "", "");
                }
                #region COMMENT
                //// =========================
                //// 6. ACKNOWLEDGEMENT NO
                //// =========================
                ////string ackNo = filingCard.FindElement(By.XPath(".//span[contains(@class,'thirdColKey') and contains(text(),'Acknowledgement No')]" +
                ////                                                 "/following-sibling::span[contains(@class,'thirdColValue')]")
                ////                                      ).Text.Trim();
                //string ackNo = filingCard.FindElement(By.XPath(
                //                ".//span[normalize-space()='Acknowledgement No :']/following-sibling::span"
                //            )).Text.Trim();
                //// =========================
                //// 7. FORM SUBMITTED DATE
                //// =========================
                ////string filingDate = filingCard.FindElement(By.XPath(".//div[contains(text(),'Form submitted')]/following-sibling::div")
                ////                                        ).Text.Trim();
                //string filingDate = filingCard.FindElement(By.XPath(
                //                ".//div[normalize-space()='Filing Date']/following-sibling::div"
                //            )).Text.Trim();
                //// =========================
                //// 8. OPEN RRR MODAL
                //// =========================
                ////filingCard.FindElement(
                ////                    By.XPath(".//span[contains(text(),'RRR Number')]/following-sibling::span[contains(text(),'View')]")
                ////                ).Click();
                //filingCard.FindElement(By.XPath(
                //                            ".//span[normalize-space()='RRR Number :']/following-sibling::span[normalize-space()='View']"
                //                        )).Click();
                //// =========================
                //// 9. READ RRR NUMBER
                //// =========================
                ////var rrrModal = wait1.Until(ExpectedConditions.ElementIsVisible(
                ////    By.XPath("//div[contains(@class,'detailscardmodal')]")
                ////));

                ////string rrrNo = rrrModal.FindElement(
                ////    By.XPath(".//div[contains(@class,'modaldetail4')]")
                ////).Text.Trim();

                ////var rrrModal = wait1.Until(ExpectedConditions.ElementIsVisible(
                ////    By.XPath("//div[contains(@class,'detailscardmodal')]//div[contains(@class,'modaldetail4')]")
                ////));

                ////string rrrNo = rrrModal.Text.Trim();
                //var rrrElement = wait1.Until(d =>
                //{
                //    var el = d.FindElement(By.XPath(
                //        "//div[contains(@class,'detailscardmodal')]//div[contains(@class,'modaldetail4')]"
                //    ));
                //    return string.IsNullOrWhiteSpace(el.Text) ? null : el;
                //});

                //string rrrNo = rrrElement.Text.Trim();
                #endregion
                // ===== ACKNOWLEDGEMENT NO =====
                string ackNo = filingCard.FindElement(By.XPath(
                    ".//span[normalize-space()='Acknowledgement No :']/following-sibling::span"
                )).Text.Trim();
                // ===== FILING DATE =====
                string filingDate = filingCard.FindElement(By.XPath(
                    ".//div[normalize-space()='Filing Date']/following-sibling::div"
                )).Text.Trim();
                //DateTime filingDate;
                //if (!DateTime.TryParseExact(
                //        filingDateText,
                //        "dd-MMM-yyyy",
                //        CultureInfo.InvariantCulture,
                //        DateTimeStyles.None,
                //        out filingDate))
                //{
                //    throw new Exception("Invalid Filing Date: " + filingDateText);
                //}
                // ===== RRR =====
                filingCard.FindElement(By.XPath(
                    ".//span[normalize-space()='RRR Number :']/following-sibling::span[normalize-space()='View']"
                )).Click();
                //
                var rrrElement = wait1.Until(d =>
                {
                    var el = d.FindElement(By.XPath(
                        "//div[contains(@class,'detailscardmodal')]//div[contains(@class,'modaldetail4')]"
                    ));
                    return string.IsNullOrWhiteSpace(el.Text) ? null : el;
                });
                //
                string rrrNo = rrrElement.Text.Trim();
                // CLOSE BROWSER
                driver?.Quit();
                //
                return (ackNo, filingDate.ToString(), rrrNo);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error during upload:\n" + ex.Message, "Upload TDS/TCS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(ex.Message);
                cmnService.J_UserMessage(ex.Message);
                return ("", "", "");
            }
            //finally
            //{
            //    // Optional: keep browser open for debugging
            //    // driver?.Quit();
            //}
        }
        #endregion

        #region LnkFetch_LinkClicked
        private void LnkFetch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            strSQL = "SELECT USER_PASSWORD FROM MST_TAN_AADHAAR WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "'";
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            //
            //grpBasicInformation.Enabled = false;
            grpITDetails.Visible = true;
        }
        #endregion

        #region BtnCloseIT_Click
        private void BtnCloseIT_Click(object sender, EventArgs e)
        {
            grpITDetails.Visible = false;
            //grpBasicInformation.Enabled = true;
        }
        #endregion


        #region BtnGoIT_Click
        private void BtnGoIT_Click(object sender, EventArgs e)
        {

            try
            {
                if(txtPassword.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Enter IT Login - Password");
                    txtPassword.Select();
                    return;
                }
                //
                grpITDetails.Visible = false;
                //
                lblStatus.Text = "Connecting to Income-tax Portal...";
                lblStatus.Refresh();
                //
                string strFormNo = txtFormNo.Text.Trim();
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT ASST_ID FROM MST_ASSESSMENT WHERE FA_YEAR = '" + txtFinancialYear.Text + "'"))) >= T_FinancialYearID.F2026_27ID)
                {
                    //if (txtFormNo.Text == T_FormNo.F24Q)
                    //    strFormNo = T_FormNo.F138_24Q;
                    //else if (txtFormNo.Text == T_FormNo.F26Q)
                    //    strFormNo = T_FormNo.F140_26Q;
                    //else if (txtFormNo.Text == T_FormNo.F27Q)
                    //    strFormNo = T_FormNo.F144_27Q;
                    //else if (txtFormNo.Text == T_FormNo.F27EQ)
                    //    strFormNo = T_FormNo.F143_27EQ;
                    strFormNo = cmnService.J_Left(strFormNo, 3);
                }
                else
                    strFormNo = strFormNo.Substring(strFormNo.IndexOf('(') + 1, strFormNo.IndexOf(')') - strFormNo.IndexOf('(') - 1);
                //
                var result = FetchAckFilingDateAndRRR(
                    tan: txtTANNo.Text.Trim(),                 // or from Company Master
                    password: txtPassword.Text.Trim(),       // secure source
                    financialYear: txtFinancialYear.Text.Trim(),
                    quarter: txtQuarter.Text.Trim(),
                    //formType: txtFormNo.Text.Trim(),
                    FormNo: strFormNo
                );
                //IncomeTaxConnect2025 objIncomeTaxConnect = new IncomeTaxConnect2025();

                //TracesLogin objLogin =    new TracesLogin();

                //objLogin.TAN =
                //    txtTANNo.Text.Trim();

                //objLogin.UserID =
                //    txtTANNo.Text.Trim();

                //objLogin.Password =
                //    txtPassword.Text.Trim();

                //TracesResponse response =
                //    objIncomeTaxConnect.MakeLoginToIncomeTaxPortal(
                //        objLogin);

                //if (response.Respons ==
                //    enmResponse.Failed)
                //{
                //    cmnService.J_UserMessage(
                //        response.Message);

                //    return;
                //}

                //cmnService.J_UserMessage(
                //    "Income Tax portal login successful.");
                //
                txtReceiptNo.Text = result.AckNo;
                //mskDateOfFiling.Text = result.FilingDate;
                mskDateOfFiling.Text = DateTime.ParseExact(result.FilingDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture).ToString();
                txtPRNNo.Text = result.RRRNo;

                //MessageBox.Show(
                //    "Receipt details fetched successfully from IT Portal.",
                //    "Fetch Complete",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Information
                //);
                cmnService.J_UserMessage("Receipt details fetched successfully from IT Portal.", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(
                //    "Failed to fetch receipt details.\n\n" + ex.Message,
                //    "IT Portal Fetch",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Error
                //);
                cmnService.J_UserMessage("Failed to fetch receipt details.\n\n" + ex.Message, MessageBoxIcon.Error);
            }
            finally
            {
                lblStatus.Text = "";
            }
        }
        #endregion

    }
}

