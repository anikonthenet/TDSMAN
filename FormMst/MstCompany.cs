
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

    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

//~~~~ User Namespaces ~~~~
    using TDSMAN.FormTrn;
    using TDSMAN.FormRpt;
    using TDSMAN.Classes;
    using TDSMAN.FormPar;
    using TDSMAN.FormSys;

    //using Microsoft.Win32;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;
using System.IO;


#endregion


namespace TDSMAN.FormMst
{
    public partial class MstCompany : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public MstCompany()
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
        string strFVUPath = "";       //-- Added By Abhishek On 12/12/2017 --
        //-----------------------------------------------------------------------
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        //
        bool blnchk194P_CheckedChanged = false;
        int elapsedTicks = 0;
        bool fetchCompleted = false;
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

        #region MstCompany_Load

        private void MstCompany_Load(object sender, EventArgs e)
        {
            try
            {
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(), RegistryKeyPermissionCheck.ReadWriteSubTree);
                GC.Collect();
                //-----------------------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                dgvGrid.Visible = true;
                //-----------------------------------------------------------
                //DisableControls();
                //-----------------------------------------------------------
                ControlVisible(false);
                ClearControls();
                //
                lblInactiveCompanyNotes.Text = "If you block a company, then you will not be able to enter any data & create returns for this company. However, reports can be viewed.";
                //
                dgvGrid.Height = 551;
                dgvGrid.Visible = true;
                grpSearch.Location = new Point(569, 386);
                //
                lblTitle.Text = "Company Master (Deductor / Collector)";
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
                string[,] strMatrix = {{"CompID", "0", "", "", "", "F", ""},
							            {"Deductor/ Employer/ Collector Name", "250", "", "", "", "", "T"},
							            {"TAN No.", "80", "", "", "", "", "T"},
							            {"PAN No.", "80", "", "", "", "", "T"},
							            {"Deductor Type", "150", "", "", "", "", "T"},
                                        {"Responsible Person", "180", "", "", "", "", "T"},
                                        {"GSTN", "100", "", "", "", "", "T"},
                                        {"Annex III", "60", "", "", "", "", "T"},
                                        {"CSI password", "60", "", "", "", "", "T"},
                                        {"Block ", "40", "", "", "", "", "T"}};
                
                                        //{"Company Prefix", "100", "", "", "", "", "T"}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                string[,] strCompanyInactiveMatrix = {{"MST_COMPANY.INACTIVE_FLAG = 0", "F", "", "T"},
                                               {"MST_COMPANY.INACTIVE_FLAG = 1", "F", "Yes", "T"}};
                //
                string[,] strCompany194PMatrix = {{"MST_COMPANY.SECTION_194P_FLAG = 0", "F", "", "T"},
                                               {"MST_COMPANY.SECTION_194P_FLAG = 1", "F", "Yes", "T"}};
                //
                string[,] strCSIFileDownloadMatrix = {{"MST_COMPANY.CSI_FILE_DOWNLOAD_OPTION = 1", "F", "Yes", "T"},
                                               {"MST_COMPANY.CSI_FILE_DOWNLOAD_OPTION = 2", "F", "", "T"}};
                //-----------------------------------------------------------
                strOrderBy = "MST_COMPANY.COMPANY_NAME";
                strQuery = "SELECT MST_COMPANY.COMPANY_ID            AS COMPANY_ID," +
                    "              + '  ' + MST_COMPANY.COMPANY_NAME          AS COMPANY_NAME," +
                    "              MST_COMPANY.TAN_NO                AS TAN_NO," +
                    "              MST_COMPANY.PAN_NO                AS PAN_NO," +
                    "              MST_CATEGORY.CATEGORY_CODE + ' - ' + MST_CATEGORY.CATEGORY_DESCRIPTION AS D_STATUS," +
                    "              MST_COMPANY.PERSON_NAME           AS RESPONSIBLE_PERSON_NAME," +
                    "              MST_COMPANY.GSTN                  AS GSTN," +
                    "              " + cmnService.J_SQLDBFormat(strCompany194PMatrix, J_SQLColFormat.Case_End) +     " AS SECTION_194P_FLAG," +
                    "              " + cmnService.J_SQLDBFormat(strCSIFileDownloadMatrix, J_SQLColFormat.Case_End) + " AS CSI_FILE_DOWNLOAD_OPTION," +
                    "              " + cmnService.J_SQLDBFormat(strCompanyInactiveMatrix, J_SQLColFormat.Case_End) + " AS INACTIVE " +
                    "       FROM   MST_COMPANY," +
                    "              MST_CATEGORY " +
                    "       WHERE  MST_COMPANY.D_CATEGORY_ID = MST_CATEGORY.CATEGORY_ID ";
                //-----------------------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                ////if (dsetGridClone != null) dsetGridClone.Clear();
                ////dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                //dgvGrid.AutoGenerateColumns = true;
                if (dsetGridClone != null) dsetGridClone.Clear();
                dgvGrid.DataSource = null;
                dgvGrid.Columns.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvGrid, strSQL, strMatrix);
                if (dsetGridClone == null) return;
                //dgvGrid.Refresh();
                //
                //if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId) == false)
                //    BtnAdd.Select();
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //dgvGrid_Click(sender, e);
                //Added by Indrajit on 12-02-2013
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

        public void BtnAdd_Click(object sender, EventArgs e)
        {
            
            try
            {
                //-- Added By Abhishek Dey On 31/10/2019 --
                #region CAN CLIENT CAN ADD COMPANY
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                {
                    if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                    {
                        if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID)) > 0)
                        {
                            cmnService.J_UserMessage("You are not authorised to add any company");
                            return;
                        }
                    }
                }
                #endregion
                //--
                btnImportConso.Visible = true;
                btnFetchITPortal.Visible = true; //-- 13/08/2025 --
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                dgvGrid.Visible = false;
                lblSearchMode.Text = J_Mode.General;
                //---------------------------------------------
                grpInactiveTAN.Visible = false;
                //--
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                chk194P.Checked = false;
                //---------------------------------------------
                strCheckFields = "";
                txtDedEmpColName.Select();
                //
                if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
                {
                    txtITPassword.UseSystemPasswordChar = true;
                }
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
                btnImportConso.Visible = false; //-- 12/12/2017 --
                btnFetchITPortal.Visible = false; //-- 13/08/2025 --
                if (dgvGrid.CurrentRow != null)
                {
                    lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
                    //-- Added By Abhishek dey On 01/11/2019 --
                    #region IS COMPANY ACCESSIBLE FOR CLIENT
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                    {
                        if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                        {
                            //if (TdsMan.IsCompanyAccessable(lngSearchId, TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                            if (TdsMan.IsCompanyAccessible(Convert.ToInt32(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                            {
                                cmnService.J_UserMessage("You are not authorised to edit this company");
                                BtnCancel.Select();
                                return;
                            }
                        }
                    }
                    #endregion
                    //Added by Indrajit on 18-02-2013
                    if (Check_Record(lngSearchId) == 0) return;
                    //--------------------------------------------------
                    ControlVisible(true);
                    ClearControls();
                    //--------------------------------------------------
                    //A particular ID wise retriving the data from database
                    //if (ShowRecord(Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.RowIndex , 0]))) == false)
                    if (ShowRecord(Convert.ToInt32(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value))) == false)
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
                    //--------------------------------------------------
                    grpInactiveTAN.Visible = true;
                    //--
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
                //if (Convert.ToInt64(Convert.ToString(dgvGrid.RowIndex )) < 0)
                if (dgvGrid.CurrentRow == null)
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
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
                txtNameSearch.Select();
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
                //-- NAME 
                //-------------------------------------------------------------------
                if (txtNameSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + "AND MST_COMPANY.COMPANY_NAME like '" + cmnService.J_ReplaceQuote(txtNameSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- TAN NO
                //-------------------------------------------------------------------
                if (txtTANNoSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + "AND MST_COMPANY.TAN_NO like '%" + cmnService.J_ReplaceQuote(txtTANNoSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- PAN NO
                //-------------------------------------------------------------------
                if (txtPANNoSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + "AND MST_COMPANY.PAN_NO like '%" + cmnService.J_ReplaceQuote(txtPANNoSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- GSTN
                //-------------------------------------------------------------------
                if (txtGSTNSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + "AND MST_COMPANY.GSTN like '%" + cmnService.J_ReplaceQuote(txtGSTNSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- DEDUCTOR TYPE
                //-------------------------------------------------------------------
                if (cmbDeductorTypeSearch.SelectedIndex > 0)
                    strCheckFields = strCheckFields + "AND MST_COMPANY.D_CATEGORY_ID = " + Convert.ToInt32(Support.GetItemData(cmbDeductorTypeSearch, cmbDeductorTypeSearch.SelectedIndex)) + " ";
                //-------------------------------------------------------------------
                //-- RESPONSIBLE PERSON
                //-------------------------------------------------------------------
                if (txtResponsiblePersonSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + "AND MST_COMPANY.PERSON_NAME like '%" + cmnService.J_ReplaceQuote(txtResponsiblePersonSearch.Text.Trim().ToUpper()) + "%' ";
                //----------------------------------------------------------------------
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                //----------------------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone,  lngSearchId) == false)
                {
                    txtNameSearch.Select();
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
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone,  lngSearchId);
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
            //
        }

        #endregion


        //-- Added By Abhishek Dey On 11/12/2017 --
        #region btnImportConso_Click

        private void btnImportConso_Click(object sender, EventArgs e)
        {
            grpCompanyName.Enabled = false;
            grpBasicInformation.Enabled = false;
            grpAddress.Enabled = false;
            grpResponsiblePersonDetails.Enabled = false;
            grpResponsiblePerson.Enabled = false;
            grpInactiveTAN.Enabled = false;
            grpGovtDeductors.Enabled = false;
            grpCITDetails.Enabled = false;
            //
            grpImport.Location = new Point(26, 40);
            grpImport.Visible = true;
            //
            txtFVUPath.Text = string.Empty;
        }

        #endregion

        #region btnImport_Click

        private void btnImport_Click(object sender, EventArgs e)
        {
            // VALIDATE
            // FVU FILE SELECTED
            if (txtFVUPath.Text.Trim() == "")
            {
                cmnService.J_UserMessage("TDS not selected");
                btnSelectTDSPath.Select();
                return;
            }
            // FILE SHOULD BE FVU
            if (Path.GetExtension(txtFVUPath.Text).ToUpper() != ".TDS")
            {
                //if (Path.GetExtension(txtFVUPath.Text).ToUpper() != ".TXT")
                //{
                //cmnService.J_UserMessage("Selected file should be a TDS or TXT file");
                    cmnService.J_UserMessage("Selected file should be a TDS file");
                    btnSelectTDSPath.Select();
                    return;
                //}
            }
            // FILE EXIST
            if (cmnService.J_IsFileExist(txtFVUPath.Text) == false)
            {
                cmnService.J_UserMessage("Selected " + Path.GetExtension(txtFVUPath.Text) + " file not found");
                btnSelectTDSPath.Select();
                return;
            }
            // FILE OPEN
            if (cmnService.J_IsProcessOpen(txtFVUPath.Text) == true)
            {
                cmnService.J_UserMessage("Selected " + Path.GetExtension(txtFVUPath.Text) + " file is open");
                btnSelectTDSPath.Select();
                return;
            }
            //
            if (CheckFVUCompatibility() == false)
                return;
            //
            if (cmnService.J_UserMessage("Are you sure you want to Import - Proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            //
            if (ImportCompanyMasterData(txtFVUPath.Text) == false)
            {
                cmnService.J_UserMessage("There was some problem while importing company. \nPlease contact our Helpdesk.");
                return;
            }

        }

        #endregion

        //-- Added On 12/12/2017 --
        #region btnClose_Click

        private void btnClose_Click(object sender, EventArgs e)
        {
            txtFVUPath.Text = string.Empty;
            //
            grpImport.Visible = false;
            //
            grpCompanyName.Enabled = true;
            grpBasicInformation.Enabled = true;
            grpAddress.Enabled = true;
            grpResponsiblePersonDetails.Enabled = true;
            grpResponsiblePerson.Enabled = true;
            grpInactiveTAN.Enabled = true;
            grpGovtDeductors.Enabled = true;
            grpCITDetails.Enabled = true;
        }

        #endregion

        #region btnSelectTDSPath_Click

        private void btnSelectTDSPath_Click(object sender, EventArgs e)
        {
            //strFVUPath = cmnService.J_OpenFileDialog("TDS File | *.tds; *.txt", "TDS/Text File | *.tds; *.txt", "Choose the TDS/Text File to import");
            strFVUPath = cmnService.J_OpenFileDialog("TDS File | *.tds", "TDS File | *.tds", "Choose the TDS/Conso File to import");
            if (strFVUPath != "")
                txtFVUPath.Text = strFVUPath; 
        }

        #endregion
        //-------------------------
        //-----------------------------------------


        #region dgvGrid_Click


        private void dgvGrid_Click(object sender, EventArgs e)
        {

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

        }
        #endregion

        #region dgvGrid_CurrentCellChanged
        private void dgvGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.RowIndex , 0]));

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


        #region txtDedEmpColName_TextChanged

        private void txtDedEmpColName_TextChanged(object sender, EventArgs e)
        {
            //IDataReader drdCompanyChar = null;
            //string strCompanyChar;
            ////-------------------------------
            ////-- Setting up Company Code (3 char)
            //if (txtDedEmpColName.Text.Trim() == "")
            //{
            //    txtCompanyPrefix.Text = "";
            //    return;
            //}
            ////--
            ////-- 1st Character
            //txtCompanyPrefix.Text = txtDedEmpColName.Text.Substring(0, 1);
            ////--
            //if (txtDedEmpColName.SelectionStart >= 2)
            //{
            //    //-- 2nd Character
            //    if (cmnService.J_CheckAlphabetsNumeric(txtDedEmpColName.Text.Substring(1, 1), J_DataType.Character) == false)
            //    {
            //        strCompanyChar = "SELECT COUNT(*) AS COUNT_CHAR FROM MST_COMPANY WHERE COMPANY_NAME LIKE '%" + cmnService.J_ReplaceQuote(txtDedEmpColName.Text) + "'";
            //        drdCompanyChar = dmlService.J_ExecSqlReturnReader(strCompanyChar);
            //        if (drdCompanyChar == null)
            //        {
            //            drdCompanyChar.Close();
            //            drdCompanyChar.Dispose();
            //        }
            //        while (drdCompanyChar.Read())
            //        {
            //            txtCompanyPrefix.Text = txtCompanyPrefix.Text + (Convert.ToInt16(drdCompanyChar["COUNT_CHAR"]) + 1);
            //        }
            //        drdCompanyChar.Close();
            //        drdCompanyChar.Dispose();
            //    }
            //    else
            //        //--
            //        txtCompanyPrefix.Text = txtCompanyPrefix.Text + txtDedEmpColName.Text.Substring(1, 1);
            //    //--
            //    if (txtDedEmpColName.SelectionStart >= 3)
            //    {
            //        //-- 3rd Character
            //        strCompanyChar = "SELECT COUNT(*) AS COUNT_CHAR FROM MST_COMPANY WHERE COMPANY_NAME LIKE '%" + cmnService.J_ReplaceQuote(txtDedEmpColName.Text) + "'";
            //        drdCompanyChar = dmlService.J_ExecSqlReturnReader(strCompanyChar);
            //        if (drdCompanyChar == null)
            //        {
            //            drdCompanyChar.Close();
            //            drdCompanyChar.Dispose();
            //        }
            //        while (drdCompanyChar.Read())
            //        {
            //            txtCompanyPrefix.Text = txtCompanyPrefix.Text + (Convert.ToInt16(drdCompanyChar["COUNT_CHAR"]) + 1);
            //        }
            //        drdCompanyChar.Close();
            //        drdCompanyChar.Dispose();
            //    }
            //}
            
        }

        #endregion

        #region txtDedEmpColName_KeyPress

        private void txtDedEmpColName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtTANNo_KeyPress

        private void txtTANNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                txtPANNo.Select();
            else
                if (TdsMan.gTANNoPANNoValidation(txtTANNo, e, T_TANPAN.TAN) == false)
                    e.Handled = true;
        }

        #endregion

        #region txtPANNo_KeyPress

        private void txtPANNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                txtBranch.Select();
            else
                if (TdsMan.gTANNoPANNoValidation(txtPANNo, e, T_TANPAN.PAN) == false)
                    e.Handled = true;
        }

        #endregion

        #region txtPANNo_Leave

        private void txtPANNo_Leave(object sender, EventArgs e)
        {
            if (txtPANNo.Text.Trim() == "")
            {
                txtPANNo.Text = "PANNOTREQD";
            }
        }

        #endregion

        #region txtGSTN_KeyPress

        private void txtGSTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                txtGSTN.Select();
            else
                if (TdsMan.gGSTNValidation(txtGSTN, e) == false)
                    e.Handled = true;
        }

        #endregion

        #region txtBranch_KeyPress

        private void txtBranch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region cmbDeductorType_SelectedIndexChanged

        private void cmbDeductorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDeductorType.SelectedIndex <= 0)
            {
                EnableControl(cmbGovtDedState, "F");
                EnableControl(cmbMinistry, "F");
                EnableControl(txtPAOCode, "F");
                EnableControl(txtDDOCode, "F");
                EnableControl(txtPAORegNo, "F");
                EnableControl(txtDDORegNo, "F");
                EnableControl(txtAIN, "F");
                //-- 2018/11/14 @ ANIK MANDATORY MARKS '*'
                lblMandatoryMarkPAOCode.Visible = false;
                lblMandatoryMarkPAORegNo.Visible = false;
                lblMandatoryMarkDDOCode.Visible = false;
                lblMandatoryMarkDDORegNo.Visible = false;
                lblMandatoryMarkGovtDeductorsState.Visible = false;
                lblMandatoryMarkGovtDeductorsMinistry.Visible = false;
                lblMandatoryMarkGovtDeductorsOtherMinistry.Visible = false;
                //
                return;
            }
            //-----------------------------------------------
            //-- For Deductor State
            if (cmbDeductorType.Text.Substring(0, 1) == "S" ||
                cmbDeductorType.Text.Substring(0, 1) == "E" ||
                cmbDeductorType.Text.Substring(0, 1) == "H" ||
                cmbDeductorType.Text.Substring(0, 1) == "N")
                EnableControl(cmbGovtDedState, "T");
            else
                EnableControl(cmbGovtDedState, "F");
            //-----------------------------------------------
            //-- For Ministry Name
            if (cmbDeductorType.Text.Substring(0, 1) == "A" ||
                cmbDeductorType.Text.Substring(0, 1) == "D" ||
                cmbDeductorType.Text.Substring(0, 1) == "G" ||
                cmbDeductorType.Text.Substring(0, 1) == "E" ||
                cmbDeductorType.Text.Substring(0, 1) == "H" ||
                cmbDeductorType.Text.Substring(0, 1) == "L" ||
                cmbDeductorType.Text.Substring(0, 1) == "N")
                EnableControl(cmbMinistry, "T");
            else
                EnableControl(cmbMinistry, "F");
            //-----------------------------------------------
            //-- For PAO Name
            if (cmbDeductorType.Text.Substring(0, 1) == "A" ||
                cmbDeductorType.Text.Substring(0, 1) == "S" ||
                cmbDeductorType.Text.Substring(0, 1) == "D" ||
                cmbDeductorType.Text.Substring(0, 1) == "E" ||
                cmbDeductorType.Text.Substring(0, 1) == "G" ||
                cmbDeductorType.Text.Substring(0, 1) == "H" ||
                cmbDeductorType.Text.Substring(0, 1) == "L" ||
                cmbDeductorType.Text.Substring(0, 1) == "N")
                EnableControl(txtPAOCode, "T");
            else
                EnableControl(txtPAOCode, "F");
            //-----------------------------------------------
            //-- For DDO Name
            if (cmbDeductorType.Text.Substring(0, 1) == "A" ||
                cmbDeductorType.Text.Substring(0, 1) == "S" ||
                cmbDeductorType.Text.Substring(0, 1) == "D" ||
                cmbDeductorType.Text.Substring(0, 1) == "E" ||
                cmbDeductorType.Text.Substring(0, 1) == "G" ||
                cmbDeductorType.Text.Substring(0, 1) == "H" ||
                cmbDeductorType.Text.Substring(0, 1) == "L" ||
                cmbDeductorType.Text.Substring(0, 1) == "N")
                EnableControl(txtDDOCode, "T");
            else
                EnableControl(txtDDOCode, "F");
            //-----------------------------------------------
            //-- For PAO Registration No
            if (cmbDeductorType.Text.Substring(0, 1) == "A" ||
                cmbDeductorType.Text.Substring(0, 1) == "S" ||
                cmbDeductorType.Text.Substring(0, 1) == "D" ||
                cmbDeductorType.Text.Substring(0, 1) == "E" ||
                cmbDeductorType.Text.Substring(0, 1) == "G" ||
                cmbDeductorType.Text.Substring(0, 1) == "H" ||
                cmbDeductorType.Text.Substring(0, 1) == "L" ||
                cmbDeductorType.Text.Substring(0, 1) == "N")
                EnableControl(txtPAORegNo, "T");
            else
                EnableControl(txtPAORegNo, "F");
            //-----------------------------------------------
            //-- For DDO Registration No
            if (cmbDeductorType.Text.Substring(0, 1) == "A" ||
                cmbDeductorType.Text.Substring(0, 1) == "S" ||
                cmbDeductorType.Text.Substring(0, 1) == "D" ||
                cmbDeductorType.Text.Substring(0, 1) == "E" ||
                cmbDeductorType.Text.Substring(0, 1) == "G" ||
                cmbDeductorType.Text.Substring(0, 1) == "H" ||
                cmbDeductorType.Text.Substring(0, 1) == "L" ||
                cmbDeductorType.Text.Substring(0, 1) == "N")
                EnableControl(txtDDORegNo, "T");
            else
                EnableControl(txtDDORegNo, "F");
            //-- FOR AIN NO 2013/06/30
            if (cmbDeductorType.Text.Substring(0, 1) == "A" ||
                cmbDeductorType.Text.Substring(0, 1) == "S" )
                EnableControl(txtAIN, "T");
            else
                EnableControl(txtAIN, "F");
            //-- 2018/11/14 @ ANIK MANDATORY MARKS '*'
            lblMandatoryMarkPAOCode.Visible = false;
            lblMandatoryMarkPAORegNo.Visible = false;
            lblMandatoryMarkDDOCode.Visible = false;
            lblMandatoryMarkDDORegNo.Visible = false;
            lblMandatoryMarkGovtDeductorsState.Visible = false;
            lblMandatoryMarkGovtDeductorsMinistry.Visible = false;
            lblMandatoryMarkGovtDeductorsOtherMinistry.Visible = false;
            //
            if (cmbDeductorType.Text.Substring(0, 1) == "A")
            {
                lblMandatoryMarkPAOCode.Visible = true;
                lblMandatoryMarkDDOCode.Visible = true;
                lblMandatoryMarkGovtDeductorsMinistry.Visible = true;
            }
            else if (cmbDeductorType.Text.Substring(0, 1) == "S" || cmbDeductorType.Text.Substring(0, 1) == "E" || cmbDeductorType.Text.Substring(0, 1) == "H" || cmbDeductorType.Text.Substring(0, 1) == "N")
            {
                lblMandatoryMarkGovtDeductorsState.Visible = true;
            }
            else if (cmbDeductorType.Text.Substring(0, 1) == "D" || cmbDeductorType.Text.Substring(0, 1) == "G")
            {
                lblMandatoryMarkGovtDeductorsMinistry.Visible = true;
            }

            //--
        }

        #endregion
        
        #region cmbDeductorType_KeyPress

        private void cmbDeductorType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion                

        #region txtAddress1_KeyPress

        private void txtAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtAddress2_KeyPress

        private void txtAddress2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtAddress3_KeyPress

        private void txtAddress3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtAddress4_KeyPress

        private void txtAddress4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtAddress5_KeyPress

        private void txtAddress5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region cmbState_KeyPress

        private void cmbState_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
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

        #region txtSTD_KeyPress

        private void txtSTD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,5,0", txtSTD, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtPhone_KeyPress

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,0", txtPhone, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtEmail_KeyPress

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtRPName_KeyPress

        private void txtRPName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtRPFatherName_KeyPress

        private void txtRPFatherName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtRPDesignation_KeyPress

        private void txtRPDesignation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        

        #region chkSameAddress_KeyPress

        private void chkSameAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtRPAddress1_KeyPress

        private void txtRPAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtRPAddress2_KeyPress

        private void txtRPAddress2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtRPAddress3_KeyPress

        private void txtRPAddress3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtRPAddress4_KeyPress

        private void txtRPAddress4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtRPAddress5_KeyPress

        private void txtRPAddress5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region cmbRPState_KeyPress

        private void cmbRPState_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtRPPIN_KeyPress

        private void txtRPPIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,6,0", txtRPPIN, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtRPSTD_KeyPress

        private void txtRPSTD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,5,0", txtRPSTD, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtRPPhone_KeyPress

        private void txtRPPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,0", txtRPPhone, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtRPEmail_KeyPress

        private void txtRPEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtRPMobileNo_KeyPress

        private void txtRPMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,0", txtRPMobileNo, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtPAOCode_KeyPress

        private void txtPAOCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtPAORegNo_KeyPress

        private void txtPAORegNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,7,0", txtPAORegNo, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtDDOCode_KeyPress

        private void txtDDOCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtDDORegNo_KeyPress

        private void txtDDORegNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region cmbGovtDedState_KeyPress

        private void cmbGovtDedState_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region cmbMinistry_SelectedIndexChanged

        private void cmbMinistry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Support.GetItemData(cmbMinistry, cmbMinistry.SelectedIndex)) == 99)
                EnableControl(txtOtherMinistry, "T");
            else
                EnableControl(txtOtherMinistry, "F");
            //-- 2018/11/14 ANIK
            lblMandatoryMarkGovtDeductorsOtherMinistry.Visible = false;
            if (cmbMinistry.Text.ToUpper() == "OTHERS")
            {
                lblMandatoryMarkGovtDeductorsOtherMinistry.Visible = true;
            }
        }

        #endregion

        #region cmbMinistry_KeyPress

        private void cmbMinistry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtOtherMinistry_KeyPress

        private void txtOtherMinistry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtCITCity_KeyPress

        private void txtCITCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }

        #endregion

        #region txtCITPin_KeyPress

        private void txtCITPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSave.Select(); //SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,6,0", txtCITPin, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtCITPin_KeyDown

        //private void txtCITPin_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (Convert.ToInt64(e.KeyCode) == 9) BtnSave.Select(); //SendKeys.Send("{tab}");            
        //}

        #endregion


        #region lnkKnowYourCITDetails_LinkClicked

        private void lnkKnowYourCITDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.tin-nsdl.com/downloads/RCCwiseDetailsofCommissionerofIncomeTax.pdf");
            //-- ANIK 2011/09/03
            System.Diagnostics.Process.Start("http://www.tdsman.com/Downloads/CIT-DETAILS.pdf");
        }

        #endregion

        #region ControlSort_KeyPress
        private void ControlSort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSortOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        #region ControlSearch_KeyPress
        private void ControlSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region rbnSameDifferent_CheckedChanged
        private void rbnSameDifferent_CheckedChanged(object sender, EventArgs e)
        {
            txtRPAddress1.Text = txtAddress1.Text;
            txtRPAddress2.Text = txtAddress2.Text;
            txtRPAddress3.Text = txtAddress3.Text;
            txtRPAddress4.Text = txtAddress4.Text;
            txtRPAddress5.Text = txtAddress5.Text;
            cmbRPState.Text = cmbState.Text;
            txtRPPIN.Text = txtPIN.Text;
            txtRPSTD.Text = txtSTD.Text;
            txtRPPhone.Text = txtPhone.Text;
            txtRPEmail.Text = txtEmail.Text;
            txtRPAltSTD.Text = txtAltSTD.Text;
            txtRPAltPhone.Text = txtAltPhone.Text;
            txtRPAltEmail.Text = txtAltEmail.Text; 
            //
            txtRPISD.Text = txtISD.Text;
            txtRPCountry.Text = txtCountry.Text;
            //
            if (rbnSame.Checked == true)
            {
                txtRPAddress1.Enabled = false;
                txtRPAddress2.Enabled = false;
                txtRPAddress3.Enabled = false;
                txtRPAddress4.Enabled = false;
                txtRPAddress5.Enabled = false;
                cmbRPState.Enabled = false;
                txtRPPIN.Enabled = false;
                txtRPSTD.Enabled = false;
                txtRPPhone.Enabled = false;
                txtRPEmail.Enabled = false;
                txtRPAltSTD.Enabled = false;
                txtRPAltPhone.Enabled = false;
                txtRPAltEmail.Enabled = false;
                //
                txtRPISD.Enabled = false;
                txtRPCountry.Enabled = false;
            }
            else if (rbnDifferent.Checked == true)
            {
                txtRPAddress1.Enabled = true;
                txtRPAddress2.Enabled = true;
                txtRPAddress3.Enabled = true;
                txtRPAddress4.Enabled = true;
                txtRPAddress5.Enabled = true;
                cmbRPState.Enabled = true;
                txtRPPIN.Enabled = true;
                txtRPSTD.Enabled = true;
                txtRPPhone.Enabled = true;
                txtRPEmail.Enabled = true;
                txtRPAltSTD.Enabled = true;
                txtRPAltPhone.Enabled = true;
                txtRPAltEmail.Enabled = true;
                //
                txtRPISD.Enabled = true;
                txtRPCountry.Enabled = true;
            }
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
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone,  lngSearchId);

            }
        }
        #endregion

        #region txtRPPAN_KeyPress

        private void txtRPPAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                rbnSame.Select();
            else
                if (TdsMan.gTANNoPANNoValidation(txtRPPAN, e, T_TANPAN.PAN) == false)
                    e.Handled = true;
        }

        #endregion


        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=RU1QfGTVdYo");
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0002", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));

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
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion


        #region chk194P_CheckedChanged
        private void chk194P_CheckedChanged(object sender, EventArgs e)
        {
            if (lblMode.Text == J_Mode.View) return;
            //-- 2024/02/28
            if(chk194P.Checked==true)
            {
                if(cmnService.J_UserMessage("This applies only to 'Specified Banks' handling Pension of Senior Citizens." , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    chk194P.Checked = false;
                    return;
                }
            }
            //--
            if (lblMode.Text == J_Mode.Edit && blnchk194P_CheckedChanged==false)
            {
                if(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_194P_FLAG FROM MST_COMPANY WHERE COMPANY_ID = " + lngSearchId)) == "1")
                {
                    strSQL = @"SELECT COUNT(*) 
                               FROM ((MST_COMPANY INNER JOIN TRN_BASIC_INFO 
                               ON     MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID)
                               INNER JOIN TRN_SALARY_DETAILS_194P
                               ON     TRN_BASIC_INFO.BASIC_INFO_ID = TRN_SALARY_DETAILS_194P.BASIC_INFO_ID)
                               WHERE  MST_COMPANY.COMPANY_ID = " + lngSearchId;
                    if(cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        blnchk194P_CheckedChanged = true;
                        chk194P.Checked = true;
                        cmnService.J_UserMessage("As data have been enterd in Annexure III, so disabling this feature is not allowed.");
                        blnchk194P_CheckedChanged = false;
                        return;
                    }
                }
            }
        }
        #endregion

        #endregion

        #region User Define Functions

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
            lblMandatoryCaption.Visible = bVisible;
            lblMandatoryMark.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            //--------------------------------------
            txtDedEmpColName.Text = "";
            txtTANNo.Text = "";
            txtPANNo.Text = "";
            txtBranch.Text = "";
            //-----------
            //-- DEDUCTOR TYPE
            //-----------
            strSQL = " SELECT CATEGORY_ID," +
                "             CATEGORY_CODE + ' - ' + CATEGORY_DESCRIPTION " +
                "      FROM   MST_CATEGORY " +
                "      ORDER BY CATEGORY_CODE ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbDeductorType) == false) return;
            //-----------
            //--------------------------------------
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
            txtSTD.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            //---------------------------------------
            txtRPName.Text = "";
            txtRPDesignation.Text = "";
            txtRPFatherName.Text = "";
            rbnDifferent.Checked = false;
            rbnSame.Checked = false;
            txtRPAddress1.Text = "";
            txtRPAddress2.Text = "";
            txtRPAddress3.Text = "";
            txtRPAddress4.Text = "";
            txtRPAddress5.Text = "";
            //-----------
            //-- RP STATE
            //-----------
            strSQL = " SELECT STATE_ID," +
                "             STATE_NAME " +
                "      FROM   MST_STATE " +
                "      ORDER BY STATE_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbRPState) == false) return;
            //-----------
            txtRPPIN.Text = "";
            txtRPSTD.Text = "";
            txtRPPhone.Text = "";
            txtRPMobileNo.Text = "";
            txtRPEmail.Text = "";
            //----------------------------------------
            txtPAOCode.Text = "";
            txtPAORegNo.Text = "";
            txtDDOCode.Text = "";
            txtDDORegNo.Text = "";
            //-----------
            //-- Govt Deductors STATE
            //-----------
            strSQL = " SELECT STATE_ID," +
                "             STATE_NAME " +
                "      FROM   MST_STATE " +
                "      WHERE  STATE_ID <> 36" + 
                "      ORDER BY STATE_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbGovtDedState) == false) return;
            //-----------
            //-- Ministry
            //-----------


            string[,] strMinisrtyOrderby = {{"MINISTRY_ID =  99" , "F", "1", "N"},
                                    {"MINISTRY_ID <> 99" , "F", "0", "N"}};

            strSQL = " SELECT MINISTRY_ID," +
                "             MINISTRY_NAME " +
                "      FROM   MST_MINISTRY " +
                "      ORDER BY " + cmnService.J_SQLDBFormat(strMinisrtyOrderby, J_SQLColFormat.Case_End, J_ElsePart.YES) + ", MINISTRY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbMinistry) == false) return;
            
            //cmnService.J_SQLDBFormat

            //-----------
            txtOtherMinistry.Text = "";
            //-----------
            txtCITAddress.Text = "";
            txtCITCity.Text = "";
            txtCITPin.Text = "";
            //----------------------------------------
            txtNameSearch.Text = "";
            txtTANNoSearch.Text = "";
            txtPANNoSearch.Text = "";
            txtResponsiblePersonSearch.Text = "";
            txtGSTNSearch.Text = "";
            //-----------
            //-- DEDUCTOR TYPE SEARCH
            //-----------
            strSQL = " SELECT CATEGORY_ID," +
                "             CATEGORY_CODE + ' - ' + CATEGORY_DESCRIPTION " +
                "      FROM   MST_CATEGORY " +
                "      ORDER BY CATEGORY_CODE ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbDeductorTypeSearch) == false) return;
            //-----------
            txtAltSTD.Text="";
            txtAltPhone.Text="";
            txtAltEmail.Text="";
            txtRPAltSTD.Text="";
            txtRPAltPhone.Text="";
            txtRPAltEmail.Text="";
            txtAIN.Text="";
            txtTANRegNo.Text = "";
            txtRPPAN.Text = "";
            txtGSTN.Text = "";
            //
            chkInactiveCompany.Checked = false;
            blnchk194P_CheckedChanged = false;
            //chk194P.Checked = false;
            //
            //-- Added By Abhishek Dey On 12/12/2017 --
            grpImport.Visible = false;
            grpITLogin.Visible = false;
            //
            txtFVUPath.Text = string.Empty;
            //
            grpImport.Visible = false;
            //
            grpCompanyName.Enabled = true;
            grpBasicInformation.Enabled = true;
            grpAddress.Enabled = true;
            grpResponsiblePersonDetails.Enabled = true;
            grpResponsiblePerson.Enabled = true;
            grpInactiveTAN.Enabled = true;
            grpGovtDeductors.Enabled = true;
            grpCITDetails.Enabled = true;
            //
            chk194P.Checked = false;
            chkCSIDownloadPassword.Checked = false;
            //
            mskDateOfCreation.Text = "";
            //
            txtISD.Text = "91";
            txtRPISD.Text = "91";
            txtRPCountry.Text = "INDIA";
            txtCountry.Text = "INDIA";
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
                strSQL = "SELECT  MST_COMPANY.COMPANY_ID            AS COMPANY_ID," +
                    "             MST_COMPANY.COMPANY_NAME          AS COMPANY_NAME," +
                    "             MST_COMPANY.TAN_NO                AS TAN_NO," +
                    "             MST_COMPANY.PAN_NO                AS PAN_NO," +
                    "             MST_COMPANY.BRANCH_DIV            AS BRANCH_DIV," +
                    "             MST_COMPANY.D_CATEGORY_ID         AS D_CATEGORY_ID," +
                    "             MST_CATEGORY.CATEGORY_DESCRIPTION AS CATEGORY_DESCRIPTION," +
                    "             MST_CATEGORY.CATEGORY_CODE        AS CATEGORY_CODE," +
                    "             MST_COMPANY.FILE_PREFIX           AS FILE_PREFIX," +
                    "             MST_COMPANY.ADDRESS1              AS ADDRESS1," +
                    "             MST_COMPANY.ADDRESS2              AS ADDRESS2," +
                    "             MST_COMPANY.ADDRESS3              AS ADDRESS3," +
                    "             MST_COMPANY.ADDRESS4              AS ADDRESS4," +
                    "             MST_COMPANY.ADDRESS5              AS ADDRESS5," +
                    "             MST_COMPANY.STATE_ID              AS STATE_ID," +
                    "             MST_STATE.STATE_NAME              AS STATE_NAME," +
                    "             MST_COMPANY.PIN_CODE              AS PIN_CODE," +
                    "             MST_COMPANY.STD                   AS STD," +
                    "             MST_COMPANY.PHONE                 AS PHONE," +
                    "             MST_COMPANY.EMAIL                 AS EMAIL," +
                    "             MST_COMPANY.PERSON_NAME           AS PERSON_NAME," +
                    "             MST_COMPANY.DESIGNATION           AS DESIGNATION," +
                    "             MST_COMPANY.FATHER_NAME           AS FATHER_NAME," +
                    "             MST_COMPANY.P_ADDRESS1            AS P_ADDRESS1," +
                    "             MST_COMPANY.P_ADDRESS2            AS P_ADDRESS2," +
                    "             MST_COMPANY.P_ADDRESS3            AS P_ADDRESS3," +
                    "             MST_COMPANY.P_ADDRESS4            AS P_ADDRESS4," +
                    "             MST_COMPANY.P_ADDRESS5            AS P_ADDRESS5," +
                    "             MST_COMPANY.P_STATE_ID            AS P_STATE_ID," +
                    "             RP_STATE.STATE_NAME               AS RP_STATE_NAME," +
                    "             MST_COMPANY.P_PIN_CODE            AS P_PIN_CODE," +
                    "             MST_COMPANY.P_PHONE               AS P_PHONE," +
                    "             MST_COMPANY.P_STD                 AS P_STD," +
                    "             MST_COMPANY.P_EMAIL               AS P_EMAIL," +
                    "             MST_COMPANY.P_MOBILE              AS P_MOBILE," +
                    "             MST_COMPANY.PAO_CODE              AS PAO_CODE," +
                    "             MST_COMPANY.PAO_REG_NO            AS PAO_REG_NO," +
                    "             MST_COMPANY.DDO_CODE              AS DDO_CODE," +
                    "             MST_COMPANY.DDO_REG_NO            AS DDO_REG_NO," +
                    "             MST_COMPANY.D_STATE_ID            AS D_STATE_ID," +
                    "             D_STATE.STATE_NAME                AS D_STATE_NAME," +
                    "             MST_COMPANY.MINISTRY_ID           AS MINISTRY_ID," +
                    "             MST_MINISTRY.MINISTRY_NAME        AS MINISTRY_NAME," +
                    "             MST_COMPANY.MINISTRY_OTHER        AS MINISTRY_OTHER," +
                    "             MST_COMPANY.CIT_TDS_ADDRESS       AS CIT_TDS_ADDRESS," +
                    "             MST_COMPANY.CIT_TDS_CITY          AS CIT_TDS_CITY," +
                    "             MST_COMPANY.CIT_TDS_PINCODE       AS CIT_TDS_PINCODE," +
                    "             MST_COMPANY.ALT_STD               AS ALT_STD," +
                    "             MST_COMPANY.ALT_PHONE             AS ALT_PHONE," +
                    "             MST_COMPANY.ALT_EMAIL             AS ALT_EMAIL," +
                    "             MST_COMPANY.P_ALT_STD             AS P_ALT_STD," +
                    "             MST_COMPANY.P_ALT_PHONE           AS P_ALT_PHONE," +
                    "             MST_COMPANY.P_ALT_EMAIL           AS P_ALT_EMAIL," +
                    "             MST_COMPANY.AIN_NO                AS AIN_NO," +
                    "             MST_COMPANY.TAN_REG_NO            AS TAN_REG_NO," +
                    "             MST_COMPANY.INACTIVE_FLAG         AS INACTIVE_FLAG," +
                    "             MST_COMPANY.P_PAN                 AS P_PAN," +
                    "             MST_COMPANY.GSTN                  AS GSTN," +
                    "             MST_COMPANY.SECTION_194P_FLAG     AS SECTION_194P_FLAG," +
                    "             MST_COMPANY.CSI_FILE_DOWNLOAD_OPTION     AS CSI_FILE_DOWNLOAD_OPTION," +
                    "             MST_COMPANY.DATE_OF_CREATION      AS DATE_OF_CREATION," +
                    "             MST_COMPANY.ISD_CODE              AS ISD_CODE," +
                    "             MST_COMPANY.P_ISD_CODE            AS P_ISD_CODE," +
                    "             MST_COMPANY.P_COUNTRY             AS P_COUNTRY," +
                    "             MST_COMPANY.COUNTRY               AS COUNTRY " +
                    "     FROM    (((((MST_COMPANY INNER JOIN MST_CATEGORY " +  
                    "             ON MST_COMPANY.D_CATEGORY_ID     = MST_CATEGORY.CATEGORY_ID) " +           
                    "     INNER JOIN MST_STATE " +
                    "             ON MST_COMPANY.STATE_ID    = MST_STATE.STATE_ID) " +
                    "     INNER JOIN MST_STATE AS RP_STATE " +
                    "             ON MST_COMPANY.P_STATE_ID = RP_STATE.STATE_ID) " +
                    "     LEFT JOIN  MST_STATE AS D_STATE " +
                    "             ON MST_COMPANY.D_STATE_ID        = D_STATE.STATE_ID) " +
                    "     LEFT JOIN  MST_MINISTRY " +
                    "             ON MST_COMPANY.MINISTRY_ID       = MST_MINISTRY.MINISTRY_ID) " +
                    "     WHERE   MST_COMPANY.COMPANY_ID      = " + Id + " ";

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

                    txtDedEmpColName.Text = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                    txtTANNo.Text         = Convert.ToString(drdShowRecord["TAN_NO"]);
                    txtPANNo.Text         = Convert.ToString(drdShowRecord["PAN_NO"]);
                    cmbDeductorType.Text  = Convert.ToString(drdShowRecord["CATEGORY_CODE"]) + " - " + Convert.ToString(drdShowRecord["CATEGORY_DESCRIPTION"]);
                    txtBranch.Text        = Convert.ToString(drdShowRecord["BRANCH_DIV"]);
                    txtAddress1.Text      = Convert.ToString(drdShowRecord["ADDRESS1"]);
                    txtAddress2.Text      = Convert.ToString(drdShowRecord["ADDRESS2"]);
                    txtAddress3.Text      = Convert.ToString(drdShowRecord["ADDRESS3"]);
                    txtAddress4.Text      = Convert.ToString(drdShowRecord["ADDRESS4"]);
                    txtAddress5.Text      = Convert.ToString(drdShowRecord["ADDRESS5"]);
                    cmbState.Text         = Convert.ToString(drdShowRecord["STATE_NAME"]);
                    txtPIN.Text           = Convert.ToString(drdShowRecord["PIN_CODE"]);
                    txtSTD.Text           = Convert.ToString(drdShowRecord["STD"]);
                    txtPhone.Text         = Convert.ToString(drdShowRecord["PHONE"]);
                    txtEmail.Text         = Convert.ToString(drdShowRecord["EMAIL"]);
                    txtRPName.Text        = Convert.ToString(drdShowRecord["PERSON_NAME"]);
                    txtRPDesignation.Text = Convert.ToString(drdShowRecord["DESIGNATION"]);
                    txtRPFatherName.Text  = Convert.ToString(drdShowRecord["FATHER_NAME"]);
                    txtRPAddress1.Text    = Convert.ToString(drdShowRecord["P_ADDRESS1"]);
                    txtRPAddress2.Text    = Convert.ToString(drdShowRecord["P_ADDRESS2"]);
                    txtRPAddress3.Text    = Convert.ToString(drdShowRecord["P_ADDRESS3"]);
                    txtRPAddress4.Text    = Convert.ToString(drdShowRecord["P_ADDRESS4"]);
                    txtRPAddress5.Text    = Convert.ToString(drdShowRecord["P_ADDRESS5"]);
                    cmbRPState.Text       = Convert.ToString(drdShowRecord["RP_STATE_NAME"]);
                    txtRPPIN.Text         = Convert.ToString(drdShowRecord["P_PIN_CODE"]);
                    txtRPSTD.Text         = Convert.ToString(drdShowRecord["P_STD"]);
                    txtRPPhone.Text       = Convert.ToString(drdShowRecord["P_PHONE"]);
                    txtRPMobileNo.Text    = Convert.ToString(drdShowRecord["P_MOBILE"]);
                    txtRPEmail.Text       = Convert.ToString(drdShowRecord["P_EMAIL"]);
                    txtPAOCode.Text       = Convert.ToString(drdShowRecord["PAO_CODE"]);
                    txtPAORegNo.Text      = Convert.ToString(drdShowRecord["PAO_REG_NO"]);
                    txtDDOCode.Text       = Convert.ToString(drdShowRecord["DDO_CODE"]);
                    txtDDORegNo.Text      = Convert.ToString(drdShowRecord["DDO_REG_NO"]);
                    cmbGovtDedState.Text  = Convert.ToString(drdShowRecord["D_STATE_NAME"]);
                    cmbMinistry.Text      = Convert.ToString(drdShowRecord["MINISTRY_NAME"]);
                    txtOtherMinistry.Text = Convert.ToString(drdShowRecord["MINISTRY_OTHER"]);
                    txtCITAddress.Text    = Convert.ToString(drdShowRecord["CIT_TDS_ADDRESS"]);
                    txtCITCity.Text       = Convert.ToString(drdShowRecord["CIT_TDS_CITY"]);
                    txtCITPin.Text        = Convert.ToString(drdShowRecord["CIT_TDS_PINCODE"]);
                    txtAltSTD.Text        = Convert.ToString(drdShowRecord["ALT_STD"]);
                    txtAltPhone.Text      = Convert.ToString(drdShowRecord["ALT_PHONE"]);
                    txtAltEmail.Text      = Convert.ToString(drdShowRecord["ALT_EMAIL"]);
                    txtRPAltSTD.Text      = Convert.ToString(drdShowRecord["P_ALT_STD"]);
                    txtRPAltPhone.Text    = Convert.ToString(drdShowRecord["P_ALT_PHONE"]);
                    txtRPAltEmail.Text    = Convert.ToString(drdShowRecord["P_ALT_EMAIL"]);
                    txtAIN.Text           = Convert.ToString(drdShowRecord["AIN_NO"]);
                    txtTANRegNo.Text      = Convert.ToString(drdShowRecord["TAN_REG_NO"]);
                    //
                    if (Convert.ToString(drdShowRecord["INACTIVE_FLAG"]) == "1")
                        chkInactiveCompany.Checked = true;
                    //
                    if (Convert.ToString(drdShowRecord["SECTION_194P_FLAG"]) == "1")
                        chk194P.Checked = true;
                    else
                        chk194P.Checked = false;
                    //
                    txtRPPAN.Text = Convert.ToString(drdShowRecord["P_PAN"]);
                    //
                    txtGSTN.Text = Convert.ToString(drdShowRecord["GSTN"]);
                    //-- 2023/05/15
                    if (Convert.ToString(drdShowRecord["CSI_FILE_DOWNLOAD_OPTION"]) == "1")
                    {
                        chkCSIDownloadPassword.Checked = true;
                    }
                    else
                    {
                        chkCSIDownloadPassword.Checked = false;
                    }
                    //
                    mskDateOfCreation.Text = Convert.ToString(drdShowRecord["DATE_OF_CREATION"]);
                    //
                    txtISD.Text       = Convert.ToString(drdShowRecord["ISD_CODE"]);
                    txtRPISD.Text     = Convert.ToString(drdShowRecord["P_ISD_CODE"]);
                    txtRPCountry.Text = Convert.ToString(drdShowRecord["P_COUNTRY"]);
                    txtCountry.Text = Convert.ToString(drdShowRecord["COUNTRY"]);
                    //
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();

                    //cmbState.Text = strStateName;
                    //cmbDistrict.Text = strDistrictName;

                    txtDedEmpColName.Select();
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
                    //if (Convert.ToInt64(Convert.ToString(dgvGrid.RowIndex )) < 0)
                    if (dgvGrid.CurrentRow == null)
                    {
                        cmnService.J_UserMessage(J_Msg.DataNotFound);
                        if (dsetGridClone == null) return false;
                        dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                        return false;
                    }
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    if (grpSearch.Visible == false)
                    {
                        //if (Convert.ToInt64(Convert.ToString(dgvGrid.RowIndex )) < 0)
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
                        if (txtNameSearch.Text.Trim() == "" &&
                            txtTANNoSearch.Text.Trim() == "" &&
                            txtPANNoSearch.Text.Trim() == "" &&
                            txtGSTNSearch.Text.Trim() == "" &&
                            cmbDeductorTypeSearch.SelectedIndex <= 0 &&
                            txtResponsiblePersonSearch.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtNameSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    //-----------------------------------------------------------------------
                    //-- DEDUCTOR/ EMPLOYER/ COLLECTOR NAME
                    //-----------------------------------------------------------------------
                    if (txtDedEmpColName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Company Name - Cannot be Blank");
                        txtDedEmpColName.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtDedEmpColName.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Company Name - '^' not allowed");
                        txtDedEmpColName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- TAN
                    //-----------------------------------------------------------------------
                    if (txtTANNo.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("TAN No. - Cannot be Blank");
                        txtTANNo.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- TAN FORMAT
                    //-----------------------------------------------------------------------
                    if (txtTANNo.Text.Length != 10)
                    {
                        cmnService.J_UserMessage("TAN No. should be of 10 characters");
                        txtTANNo.Select();
                        return false;
                    }
                    //---------------------------
                    if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtTANNo.Text, 4), J_DataType.Character) == false)
                    {
                        cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                        txtTANNo.Select();
                        return false;
                    }
                    //---------------------------
                    if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtTANNo.Text, 4, 5), J_DataType.Numeric) == false)
                    {
                        cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                        txtTANNo.Select();
                        return false;
                    }
                    //---------------------------
                    if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtTANNo.Text, 1), J_DataType.Character) == false)
                    {
                        cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                        txtTANNo.Select();
                        return false;
                    }
                    else 
                    {
                        if (txtTANNo.Text.Length == 10)
                        {
                            char TANLastCharacter = Convert.ToChar(cmnService.J_Right(txtTANNo.Text, 1));
                            if ((int)TANLastCharacter > 78)
                            {
                                cmnService.J_UserMessage("Incorrect Format of the TAN\nLast character should be between 'A' to 'N'.");
                                txtTANNo.Select();
                                return false;
                            }
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- TAN
                    //-----------------------------------------------------------------------
                    strSQL = "SELECT COMPANY_ID " +
                        "     FROM   MST_COMPANY " +
                        "     WHERE  TAN_NO  ='" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "'";
                    if (lblMode.Text == J_Mode.Edit)
                        strSQL = strSQL + "AND COMPANY_ID <> " + lngSearchId;
                    //--
                    if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                    {
                        cmnService.J_UserMessage("Company with same [TAN] exists");
                        txtTANNo.Select();
                        return false;
                    }                    
                    //-----------------------------------------------------------------------
                    //-- PAN
                    //-----------------------------------------------------------------------
                    if (txtPANNo.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("PAN No. - Cannot be Blank");
                        txtPANNo.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PAN FORMAT
                    //-----------------------------------------------------------------------
                    if (txtPANNo.Text.Length != 10)
                    {
                        cmnService.J_UserMessage("PAN No. should be of 10 characters");
                        txtPANNo.Select();
                        return false;
                    }
                    if (txtPANNo.Text != "PANNOTREQD")
                    {
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtPANNo.Text, 5), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN No.!!");
                            txtPANNo.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtPANNo.Text, 5, 4), J_DataType.Numeric) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN No.!!");
                            txtPANNo.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtPANNo.Text, 1), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN No.!!");
                            txtPANNo.Select();
                            return false;
                        }
                    }
                    //-- DATE OF CREATION //-- 2025/01/02
                    if (dtService.J_IsBlankDateCheck(ref mskDateOfCreation, J_ShowMessage.NO) == false)
                    {
                        //  return false;
                        //----------------------------------------------------------
                        //-- VALID DATE CHECK
                        //----------------------------------------------------------
                        if (dtService.J_IsDateValid(mskDateOfCreation) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the Date of Creation");
                            mskDateOfCreation.Select();
                            return false;
                        }
                    }
                    //-- GSTN
                    if (txtGSTN.Text.Length > 0) //-- 19AAHHR0653K1ZT || 11CALP15144D1DW
                    {
                        if (txtGSTN.Text.Length != 15)
                        {
                            cmnService.J_UserMessage("GSTN should be of 15 characters");
                            txtGSTN.Select();
                            return false;
                        }
                        //--
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtGSTN.Text, 2), J_DataType.Numeric) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the GSTN!!");
                            txtGSTN.Select();
                            return false;
                        }
                        //---------------------------
                        //if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtGSTN.Text, 2, 5), J_DataType.Character) == false)
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtGSTN.Text, 2, 4), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the GSTN!!");
                            txtGSTN.Select();
                            return false;
                        }
                        //---------------------------
                        //-- 2018/12/22
                        if (cmnService.J_IsAlphaNumericString(cmnService.J_Mid(txtGSTN.Text, 5, 1)) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the GSTN!!");
                            txtGSTN.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtGSTN.Text, 7, 4), J_DataType.Numeric) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the GSTN!!");
                            txtGSTN.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtGSTN.Text, 11, 1), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the GSTN!!");
                            txtGSTN.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtGSTN.Text, 12, 1), J_DataType.Numeric) == false)
                        {
                            //cmnService.J_UserMessage("Incorrect Format of the GSTN!!");
                            //txtGSTN.Select();
                            //return false;
                            if (cmnService.J_UserMessage("Make sure that 13th character of the GST entered is [ " + cmnService.J_Mid(txtGSTN.Text, 12, 1) + " ] which is non-numeric.\nProceed??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                txtGSTN.Select();
                                return false;
                            }
                        }
                        //---------------------------    
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtGSTN.Text, 13, 1), J_DataType.Character) == false)
                        {
                            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtGSTN.Text, 13, 1), J_DataType.Numeric) == false)
                            {
                                cmnService.J_UserMessage("Incorrect Format of the GSTN!!");
                                txtGSTN.Select();
                                return false;
                            }
                        }
                        ////---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtGSTN.Text, 1), J_DataType.Character) == false)   // Character 14 ALPHANUMERIC
                        {
                            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtGSTN.Text, 1), J_DataType.Numeric) == false)
                            {
                                cmnService.J_UserMessage("Incorrect Format of the GSTN!!");
                                txtGSTN.Select();
                                return false;
                            }
                        }
                        //----------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtGSTN.Text, 1), J_DataType.Numeric) == false)
                        {
                            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtGSTN.Text, 1), J_DataType.Character) == false)
                            {
                                cmnService.J_UserMessage("Incorrect Format of the GSTN!!");
                                txtGSTN.Select();
                                return false;
                            }
                        }
                        //----------------------------
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtTANRegNo.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("TAN Reg No. - '^' not allowed");
                        txtTANRegNo.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtBranch.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Branch / Division - '^' not allowed");
                        txtBranch.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- DEDUCTOR TYPE
                    //-----------------------------------------------------------------------
                    if (cmbDeductorType.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Deductor Type - Cannot be Blank");
                        cmbDeductorType.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- COMPANY PREFIX
                    //-----------------------------------------------------------------------
                    //if (txtCompanyPrefix.Text.Trim() == "")
                    //{
                    //    cmnService.J_UserMessage("Please enter the Company Prefix");
                    //    txtCompanyPrefix.Select();
                    //    return false;
                    //}
                    //-----------------------------------------------------------------------
                    //-- ADDRESS1
                    //-----------------------------------------------------------------------
                    if (txtAddress1.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Flt/Dr/Blck Number - Cannot be Blank");
                        txtAddress1.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAddress1.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Flt/Dr/Blck Number - '^' not allowed");
                        txtAddress1.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAddress4.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Building - '^' not allowed");
                        txtAddress4.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAddress2.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Rd/Strt/lane - '^' not allowed");
                        txtAddress2.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAddress5.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Area/Locality - '^' not allowed");
                        txtAddress5.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAddress3.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Town - '^' not allowed");
                        txtAddress3.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PIN
                    //-----------------------------------------------------------------------
                    if (txtPIN.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("PIN - Cannot be Blank");
                        txtPIN.Select();
                        return false;
                    }
                    // SIX CHARACTERS
                    if (txtPIN.Text.Length != 6 )
                    {
                        cmnService.J_UserMessage("Incorrect Format of the PIN");
                        txtPIN.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- STATE
                    //-----------------------------------------------------------------------
                    if (cmbState.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("State - Cannot be Blank");
                        cmbState.Select();
                        return false;
                    }
                    // 2020/12/23
                    if (cmbState.SelectedIndex > 0)
                    {
                        if(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT INACTIVE_FLAG FROM MST_STATE WHERE STATE_ID = " + Convert.ToInt32(Support.GetItemData(cmbState, cmbState.SelectedIndex))) ) == "1" )
                        {
                            cmnService.J_UserMessage("State - " + cmbState.Text + " is not applicable");
                            cmbState.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- PHONE & STD
                    //-----------------------------------------------------------------------
                    if (txtSTD.Text.Trim() != "")
                    {
                        if (txtPhone.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Phone No - Cannot be Blank if STD is provided");
                            txtPhone.Select();
                            return false;
                        }
                    }
                    //------------------
                    //if (txtPhone.Text.Trim() != "")
                    //{
                    //    if (txtSTD.Text.Trim() == "")
                    //    {
                    //        cmnService.J_UserMessage("STD - Cannot be Blank if Phone No is provided");
                    //        txtSTD.Select();
                    //        return false;
                    //    }
                    //}
                    //-- PHONE
                    if (txtPhone.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Phone No - Cannot be Blank");
                        txtPhone.Select();
                        return false;
                    }                    
                    // EMAIL
                    if (txtEmail.Text.Trim() != "" && TdsMan.T_ValidateEmail(txtEmail.Text) == false)
                    {
                        cmnService.J_UserMessage("Incorrect Format of Email");
                        txtEmail.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtEmail.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Email - '^' not allowed");
                        txtEmail.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAltEmail.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Alt Email - '^' not allowed");
                        txtAltEmail.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- RESPONSIBLE PERSON NAME
                    //-----------------------------------------------------------------------
                    if (txtRPName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Responsible Person's name - Cannot be Blank");
                        txtRPName.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRPName.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Responsible Person's name - '^' not allowed");
                        txtRPName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- DESIGNATION
                    //-----------------------------------------------------------------------
                    if (txtRPDesignation.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Designation - Cannot be Blank");
                        txtRPDesignation.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRPDesignation.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Designation - '^' not allowed");
                        txtRPDesignation.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRPFatherName.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Father Name - '^' not allowed");
                        txtRPFatherName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- RP ADDRESS1
                    //-----------------------------------------------------------------------
                    if (txtRPAddress1.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Responsible Person's Flt/Dr/Blck Number - Cannot be Blank");
                        txtRPAddress1.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRPAddress1.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Responsible Person's Flt/Dr/Blck Number - '^' not allowed");
                        txtRPAddress1.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRPAddress4.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Responsible Person's Building - '^' not allowed");
                        txtRPAddress4.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRPAddress2.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Responsible Person's Rd/Strt/lane - '^' not allowed");
                        txtRPAddress2.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRPAddress5.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Responsible Person's Area - '^' not allowed");
                        txtRPAddress5.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRPAddress3.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Responsible Person's Town - '^' not allowed");
                        txtRPAddress3.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- RP PIN
                    //-----------------------------------------------------------------------
                    if (txtRPPIN.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Responsible Person's PIN - Cannot be Blank");
                        txtRPPIN.Select();
                        return false;
                    }
                    // SIX CHARACTERS
                    if (txtRPPIN.Text.Length != 6)
                    {
                        cmnService.J_UserMessage("Incorrect Format of the Responsible Person's PIN");
                        txtRPPIN.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- RP STATE
                    //-----------------------------------------------------------------------
                    if (cmbRPState.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Responsible Person's State - Cannot be Blank");
                        cmbRPState.Select();
                        return false;
                    }
                    // 2020/12/23
                    if (cmbRPState.SelectedIndex > 0)
                    {
                        if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT INACTIVE_FLAG FROM MST_STATE WHERE STATE_ID = " + Convert.ToInt32(Support.GetItemData(cmbRPState, cmbRPState.SelectedIndex)))) == "1")
                        {
                            cmnService.J_UserMessage("State - " + cmbRPState.Text + " is not applicable");
                            cmbRPState.Select();
                            return false;
                        }
                    }

                    //-----------------------------------------------------------------------
                    //-- RP_PHONE & RP_STD
                    //-----------------------------------------------------------------------
                    if (txtRPSTD.Text.Trim() != "")
                        if (txtRPPhone.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Responsible Person's Phone No - Cannot be Blank if Responsible Person's STD is provided");
                            txtRPPhone.Select();
                            return false;
                        }
                    //------------------
                    //if (txtRPPhone.Text.Trim() != "")
                    //    if (txtRPSTD.Text.Trim() == "")
                    //    {
                    //        cmnService.J_UserMessage("Responsible Person's STD - Cannot be Blank if Responsible Person's Phone No is provided");
                    //        txtRPSTD.Select();
                    //        return false;
                    //    }
                    if (txtRPPhone.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Responsible Person's Phone No - Cannot be Blank ");
                        txtRPPhone.Select();
                        return false;
                    }
                    ////-----------------------------------------------------------------------
                    ////-- PHONE & RP_PHONE & MOBILE
                    ////-----------------------------------------------------------------------
                    //if (txtPhone.Text.Trim() == "" && txtRPPhone.Text.Trim() == "" && txtRPMobileNo.Text.Trim() == "")
                    //{
                    //    cmnService.J_UserMessage("Deductor Phone/Responsible Person's Phone/Mobile No. - at least one of them should have a value");
                    //    txtPhone.Select();
                    //    return false;
                    //}
                    //------------NEW VALIDATION FOR MOBILE AS PER FVU 3.1-------------------
                    if ((cmbDeductorType.Text.Substring(0, 1) == "A") || (cmbDeductorType.Text.Substring(0, 1) == "S"))
                    {
                        if (txtPhone.Text.Trim() == "" && txtRPPhone.Text.Trim() == "" && txtRPMobileNo.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Deductor Phone/Responsible Person's Phone/Mobile No. - at least one of them should have a value");
                            txtPhone.Select();
                            return false;
                        }
                    }
                    else
                    {
                        if (txtRPMobileNo.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Responsible Mobile Number - Cannot be Blank");
                            txtRPMobileNo.Select();
                            return false;
                        }
                        // MOBILE NUMBER 10 DIGITS
                        if (txtRPMobileNo.Text.Trim().Length != 10)
                        {
                            cmnService.J_UserMessage("Mobile Number should be of 10 digits.");
                            txtRPMobileNo.Select();
                            return false;
                        }
                    
                    }
                    // RP_EMAIL
                    if (txtRPEmail.Text.Trim() != "" && TdsMan.T_ValidateEmail(txtRPEmail.Text) == false)
                    {
                        cmnService.J_UserMessage("Incorrect Format of Responsible Person's Email");
                        txtRPEmail.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRPEmail.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Responsible Person's Email - '^' not allowed");
                        txtRPEmail.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRPAltEmail.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Responsible Person's Alt Email - '^' not allowed");
                        txtRPAltEmail.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- EMAIL
                    //-----------------------------------------------------------------------
                    if (txtEmail.Text.Trim() == "" && txtRPEmail.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Deductor Email/Responsible Person's Email - at least one of them should have a value");
                        txtEmail.Select();
                        return false;
                    }
                    // ISD CODE -- 2026/06/01
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtISD.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("ISD Code - '^' not allowed");
                        txtISD.Select();
                        return false;
                    }
                    if (txtISD.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("ISD Code - Cannot be Blank");
                        txtISD.Select();
                        return false;
                    }
                    // RP ISD CODE -- 2026/06/01
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRPISD.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Responsible Person's ISD Code - '^' not allowed");
                        txtRPISD.Select();
                        return false;
                    }
                    if (txtRPISD.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Responsible Person's ISD Code - Cannot be Blank");
                        txtRPISD.Select();
                        return false;
                    }
                    // RP COUNTRY -- 2026/06/01
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtCountry.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Country - '^' not allowed");
                        txtCountry.Select();
                        return false;
                    }
                    if (txtCountry.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Country - Cannot be Blank");
                        txtCountry.Select();
                        return false;
                    }
                    if (TdsMan.T_DetectCaret(txtRPCountry.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Responsible Person's Country - '^' not allowed");
                        txtRPCountry.Select();
                        return false;
                    }
                    if (txtRPCountry.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Responsible Person's Country - Cannot be Blank");
                        txtRPCountry.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PAO Code & DDO Code
                    //-----------------------------------------------------------------------
                    if (cmbDeductorType.Text.Substring(0, 1) == "A")
                    {
                        //-- PAO Code
                        if (txtPAOCode.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("PAO Code - Cannot be Blank");
                            txtPAOCode.Select();
                            return false;
                        }
                        // CHECK '^'
                        if (TdsMan.T_DetectCaret(txtPAOCode.Text.Trim(), "^") == true)
                        {
                            cmnService.J_UserMessage("PAO Code - '^' not allowed");
                            txtPAOCode.Select();
                            return false;
                        }
                        //-- DDO Code
                        if (txtDDOCode.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("DDO Code - Cannot be Blank");
                            txtDDOCode.Select();
                            return false;
                        }
                        // CHECK '^'
                        if (TdsMan.T_DetectCaret(txtDDOCode.Text.Trim(), "^") == true)
                        {
                            cmnService.J_UserMessage("DDO Code - '^' not allowed");
                            txtDDOCode.Select();
                            return false;
                        }
                        // CHECK '^'
                        if (TdsMan.T_DetectCaret(txtDDORegNo.Text.Trim(), "^") == true)
                        {
                            cmnService.J_UserMessage("DDO Reg No. - '^' not allowed");
                            txtDDORegNo.Select();
                            return false;
                        }                        
                    }
                    //-----------------------------------------------------------------------
                    //-- DEDUCTOR STATE
                    //-----------------------------------------------------------------------
                    if (cmbDeductorType.Text.Substring(0, 1) == "S" || cmbDeductorType.Text.Substring(0, 1) == "E" || cmbDeductorType.Text.Substring(0, 1) == "H" || cmbDeductorType.Text.Substring(0, 1) == "N")
                    {
                        if (cmbGovtDedState.SelectedIndex <= 0)
                        {
                            cmnService.J_UserMessage("Deductor's State - Cannot be Blank");
                            cmbGovtDedState.Select();
                            return false;
                        }
                        // 2020/12/23
                        if (cmbGovtDedState.SelectedIndex > 0)
                        {
                            if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT INACTIVE_FLAG FROM MST_STATE WHERE STATE_ID = " + Convert.ToInt32(Support.GetItemData(cmbGovtDedState, cmbGovtDedState.SelectedIndex)))) == "1")
                            {
                                cmnService.J_UserMessage("State - " + cmbGovtDedState.Text + " is not applicable");
                                cmbGovtDedState.Select();
                                return false;
                            }
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- MINISTRY
                    //-----------------------------------------------------------------------
                    if (cmbDeductorType.Text.Substring(0, 1) == "A" || cmbDeductorType.Text.Substring(0, 1) == "D" || cmbDeductorType.Text.Substring(0, 1) == "G")
                    {
                        if (cmbMinistry.SelectedIndex <= 0)
                        {
                            cmnService.J_UserMessage("Ministry - Cannot be Blank");
                            cmbMinistry.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- OTHER MINISTRY
                    //-----------------------------------------------------------------------                    
                    if (Convert.ToInt32(Support.GetItemData(cmbMinistry, cmbMinistry.SelectedIndex)) == 99)
                    {
                        if (txtOtherMinistry.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Other Ministry - Cannot be Blank");
                            txtOtherMinistry.Select();
                            return false;
                        }
                    }
                    //-- 2014/09/24
                    //-----------------------------------------------------------------------
                    //-- AIN
                    //-----------------------------------------------------------------------                    
                    //if (cmbDeductorType.Text.Substring(0, 1) == "A" || cmbDeductorType.Text.Substring(0, 1) == "S")
                    //{
                    //    if (txtAIN.Text.Trim() == "")
                    //    {
                    //        cmnService.J_UserMessage("Account Office Identification Number - Cannot be Blank");
                    //        txtAIN.Select();
                    //        return false;
                    //    }
                    //}
                    //-----------------------------------------------------------------------
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtCITAddress.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("CIT Address - '^' not allowed");
                        txtCITAddress.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtCITCity.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("CIT City - '^' not allowed");
                        txtCITCity.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtAIN.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("AIN - '^' not allowed");
                        txtAIN.Select();
                        return false;
                    }
                    // 
                    //-----------------------------------------------------------------------
                    //-- PAN
                    //-----------------------------------------------------------------------
                    if (txtRPPAN.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Responsible Person's PAN - Cannot be Blank");
                        txtRPPAN.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PAN FORMAT
                    //-----------------------------------------------------------------------
                    if (txtRPPAN.Text.Length != 10)
                    {
                        cmnService.J_UserMessage("Responsible Person's PAN should be of 10 characters");
                        txtRPPAN.Select();
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
                long lngDStateId = 0;
                long lngMinistryId = 0;
                int int194P = 0; int intCSIFileDownload = 0;
                string strDateOfCreation = "";
                //--------------------------------------------
                //-- D STATE
                //--------------------------------------------
                if (cmbGovtDedState.SelectedIndex <= 0)
                    lngDStateId = 0;
                else
                    lngDStateId = Convert.ToInt32(Support.GetItemData(cmbGovtDedState, cmbGovtDedState.SelectedIndex));
                //--------------------------------------------
                //-- MINISTRY ID
                //--------------------------------------------
                if (cmbMinistry.SelectedIndex <= 0)
                    lngMinistryId = 0;
                else
                    lngMinistryId = Convert.ToInt32(Support.GetItemData(cmbMinistry, cmbMinistry.SelectedIndex));
                //--------------------------------------------
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        #region Add
                        //*****  For Insert
                        //-----------------------------------------------------------
                        if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION ||
                            TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.LITE_EDITION)
                        {
                            //--
                            strSQL = "SELECT COUNT(COMPANY_ID) + 1 AS MAX_COMPANY_COUNT FROM MST_COMPANY";
                            if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > TDSMAN.Classes.TDSMAN.T_pMaxCompanyCount)
                            {
                                if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                                {
                                    TrnTrialMessageBox TrialMessageBox = new TrnTrialMessageBox();
                                    TrialMessageBox.StartPosition = FormStartPosition.CenterScreen;
                                    TrialMessageBox.lblMessage2.Text = "Trial version supports only One Company.";
                                    TrialMessageBox.Show();
                                    return;
                                }
                                else
                                {
                                    cmnService.J_UserMessage("Lite edition supports only one Company.");
                                    return;
                                }
                            }
                            //--
                        }
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;
                        //
                        if (chk194P.Checked == true)                        
                            int194P = 1;
                        else
                            int194P = 0;
                        //--
                        if (chkCSIDownloadPassword.Checked == true)
                            intCSIFileDownload = 1;
                        else
                            intCSIFileDownload = 2;
                        //--
                        if (dtService.J_IsBlankDateCheck(ref mskDateOfCreation, J_ShowMessage.NO) == true)
                            strDateOfCreation = "NULL";
                        else
                            strDateOfCreation = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDateOfCreation) + cmnService.J_DateOperator();
                        //-----------------------------------------------------------
                        if (cmnService.J_SaveConfirmationMessage(ref txtDedEmpColName) == true) return;
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        strSQL = "INSERT INTO MST_COMPANY (" +
                                 "            GROUP_ID," +
                                 "            TAN_NO," +
                                 "            PAN_NO," +
                                 "            COMPANY_NAME," +
                                 "            BRANCH_DIV," +
                                 "            D_CATEGORY_ID," +
                                 "            MINISTRY_ID," +
                                 "            MINISTRY_OTHER," +
                                 "            ADDRESS1," +
                                 "            ADDRESS2," +
                                 "            ADDRESS3," +
                                 "            ADDRESS4," +
                                 "            ADDRESS5," +
                                 "            STATE_ID," +
                                 "            PIN_CODE," +
                                 "            STD," +
                                 "            PHONE," +
                                 "            EMAIL," +
                                 "            PERSON_NAME," +
                                 "            DESIGNATION," +
                                 "            FATHER_NAME," +
                                 "            P_ADDRESS1," +
                                 "            P_ADDRESS2," +
                                 "            P_ADDRESS3," +
                                 "            P_ADDRESS4," +
                                 "            P_ADDRESS5," +
                                 "            P_STATE_ID," +
                                 "            P_PIN_CODE," +
                                 "            P_STD," +
                                 "            P_PHONE," +
                                 "            P_EMAIL," +
                                 "            P_MOBILE," +
                                 "            PAO_CODE," +
                                 "            PAO_REG_NO," +
                                 "            DDO_CODE," +
                                 "            DDO_REG_NO," +
                                 "            D_STATE_ID," +
                                 "            CIT_TDS_ADDRESS," +
                                 "            CIT_TDS_CITY," +
                                 "            CIT_TDS_PINCODE," +
                                 "            ALT_STD," +
                                 "            ALT_PHONE," +
                                 "            ALT_EMAIL," +
                                 "            P_ALT_STD," +
                                 "            P_ALT_PHONE," +
                                 "            P_ALT_EMAIL," +
                                 "            AIN_NO," +
                                 "            TAN_REG_NO," +
                                 "            P_PAN," +
                                 "            GSTN," +
                                 "            SECTION_194P_FLAG," +
                                 "            CSI_FILE_DOWNLOAD_OPTION," +
                                 "            DATE_OF_CREATION," +
                                 "            ISD_CODE," +
                                 "            P_ISD_CODE," +
                                 "            P_COUNTRY," +
                                 "            COUNTRY) " +
                                 "     VALUES( " + TDSMAN.Classes.TDSMAN.T_pGroupId + "," +
                                 "            '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtPANNo.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtDedEmpColName.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtBranch.Text.Trim()) + "'," +
                                 "             " + Convert.ToInt32(Support.GetItemData(cmbDeductorType, cmbDeductorType.SelectedIndex)) + "," +
                                 "             " + lngMinistryId + "," +
                                 "            '" + cmnService.J_ReplaceQuote(txtOtherMinistry.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAddress1.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAddress2.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAddress3.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAddress4.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAddress5.Text.Trim()) + "'," +
                                 "             " + Convert.ToInt32(Support.GetItemData(cmbState, cmbState.SelectedIndex)) + "," +
                                 "            '" + cmnService.J_ReplaceQuote(txtPIN.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtSTD.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtPhone.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtEmail.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPName.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPDesignation.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPFatherName.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPAddress1.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPAddress2.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPAddress3.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPAddress4.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPAddress5.Text.Trim()) + "'," +
                                 "             " + Convert.ToInt32(Support.GetItemData(cmbRPState, cmbRPState.SelectedIndex)) + "," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPPIN.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPSTD.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPPhone.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPEmail.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPMobileNo.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtPAOCode.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtPAORegNo.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtDDOCode.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtDDORegNo.Text.Trim()) + "'," +
                                 "             " + lngDStateId + "," +
                                 "            '" + cmnService.J_ReplaceQuote(txtCITAddress.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtCITCity.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtCITPin.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAltSTD.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAltPhone.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAltEmail.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPAltSTD.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPAltPhone.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPAltEmail.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtAIN.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtTANRegNo.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPPAN.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtGSTN.Text.Trim()) + "'," + 
                                 "             " + int194P + "," + intCSIFileDownload + "," +
                                 "             " + strDateOfCreation + "," +
                                 "            '" + cmnService.J_ReplaceQuote(txtISD.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPISD.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtRPCountry.Text.Trim()) + "'," +
                                 "            '" + cmnService.J_ReplaceQuote(txtCountry.Text.Trim()) + "')";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            txtDedEmpColName.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        lngSearchId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "MST_COMPANY", "COMPANY_ID");
                        if (lngSearchId == 0)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        //-----------------------------------------------------------
                        //Added by Indrajit on 11-02-2013
                        ////-----------------------------------------------------------------------
                        ////-- COMPANY NAME
                        ////-----------------------------------------------------------------------
                        //strSQL = "SELECT COUNT(COMPANY_ID) " +
                        //    "     FROM   MST_COMPANY " +
                        //    "     WHERE  COMPANY_NAME ='" + cmnService.J_ReplaceQuote(txtDedEmpColName.Text) + "'" +
                        //    "     AND    COMPANY_ID  <> " + lngSearchId;
                        ////--
                        //if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        //{
                        //    cmnService.J_UserMessage("Company Name exists");
                        //    txtDedEmpColName.Select();
                        //    dmlService.J_Rollback();
                        //    return;
                        //}
                        //End of add zone  ------------------------------------------

                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        //-----------------------------------------------------------
                        ClearControls();
                        //-----------------------------------------------------------
                        cmnService.J_UserMessage("Record Saved");
                        //-----------------------------------------------------------
                        txtDedEmpColName.Select();
                        //-----------------------------------------------------------
                        break;
                    #endregion
                    case J_Mode.Edit:
                        #region Edit
                        //*****  For Modify
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;
                        //-----------------------------------------------------------
                        if (cmnService.J_SaveConfirmationMessage(ref txtDedEmpColName) == true) return;
                        //-----------------------------------------------------------
                        //Added by Indrajit on 11-02-2013
                        if (Check_Record(lngSearchId) == 0) return;
                        //----------
                        int intInactiveCompany = 0;
                        if (chkInactiveCompany.Checked == true)
                        {
                            if (cmnService.J_UserMessage("Do you want to block this company?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                            else
                                intInactiveCompany = 1;
                        }
                        //--
                        if (chk194P.Checked == true)
                            int194P = 1;
                        else
                            int194P = 0;
                        //--
                        if (chkCSIDownloadPassword.Checked == true)
                            intCSIFileDownload = 1;
                        else
                            intCSIFileDownload = 2;
                        //--
                        if (dtService.J_IsBlankDateCheck(ref mskDateOfCreation, J_ShowMessage.NO) == true)
                            strDateOfCreation = "NULL";
                        else
                            strDateOfCreation = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDateOfCreation) + cmnService.J_DateOperator();
                        //----------
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        //-- UPDATE THE MASTER
                        //-----------------------------------------------------------
                        strSQL = "UPDATE MST_COMPANY " +
                                 "SET    TAN_NO          =  '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "'," +
                                 "       PAN_NO          = '" + cmnService.J_ReplaceQuote(txtPANNo.Text.Trim()) + "'," +
                                 "       COMPANY_NAME    = '" + cmnService.J_ReplaceQuote(txtDedEmpColName.Text.Trim()) + "'," +
                                 "       BRANCH_DIV      = '" + cmnService.J_ReplaceQuote(txtBranch.Text.Trim()) + "'," +
                                 "       D_CATEGORY_ID   =  " + Convert.ToInt32(Support.GetItemData(cmbDeductorType, cmbDeductorType.SelectedIndex)) + "," +
                                 "       MINISTRY_ID     =  " + lngMinistryId + "," +
                                 "       MINISTRY_OTHER  = '" + cmnService.J_ReplaceQuote(txtOtherMinistry.Text.Trim()) + "'," +
                                 "       ADDRESS1        = '" + cmnService.J_ReplaceQuote(txtAddress1.Text.Trim()) + "'," +
                                 "       ADDRESS2        = '" + cmnService.J_ReplaceQuote(txtAddress2.Text.Trim()) + "'," +
                                 "       ADDRESS3        = '" + cmnService.J_ReplaceQuote(txtAddress3.Text.Trim()) + "'," +
                                 "       ADDRESS4        = '" + cmnService.J_ReplaceQuote(txtAddress4.Text.Trim()) + "'," +
                                 "       ADDRESS5        = '" + cmnService.J_ReplaceQuote(txtAddress5.Text.Trim()) + "'," +
                                 "       STATE_ID        =  " + Convert.ToInt32(Support.GetItemData(cmbState, cmbState.SelectedIndex)) + "," +
                                 "       PIN_CODE        = '" + cmnService.J_ReplaceQuote(txtPIN.Text.Trim()) + "'," +
                                 "       STD             = '" + cmnService.J_ReplaceQuote(txtSTD.Text.Trim()) + "'," +
                                 "       PHONE           = '" + cmnService.J_ReplaceQuote(txtPhone.Text.Trim()) + "'," +
                                 "       EMAIL           = '" + cmnService.J_ReplaceQuote(txtEmail.Text.Trim()) + "'," +
                                 "       PERSON_NAME     = '" + cmnService.J_ReplaceQuote(txtRPName.Text.Trim()) + "'," +
                                 "       DESIGNATION     = '" + cmnService.J_ReplaceQuote(txtRPDesignation.Text.Trim()) + "'," +
                                 "       FATHER_NAME     = '" + cmnService.J_ReplaceQuote(txtRPFatherName.Text.Trim()) + "'," +
                                 "       P_ADDRESS1      = '" + cmnService.J_ReplaceQuote(txtRPAddress1.Text.Trim()) + "'," +
                                 "       P_ADDRESS2      = '" + cmnService.J_ReplaceQuote(txtRPAddress2.Text.Trim()) + "'," +
                                 "       P_ADDRESS3      = '" + cmnService.J_ReplaceQuote(txtRPAddress3.Text.Trim()) + "'," +
                                 "       P_ADDRESS4      = '" + cmnService.J_ReplaceQuote(txtRPAddress4.Text.Trim()) + "'," +
                                 "       P_ADDRESS5      = '" + cmnService.J_ReplaceQuote(txtRPAddress5.Text.Trim()) + "'," +
                                 "       P_STATE_ID      =  " + Convert.ToInt32(Support.GetItemData(cmbRPState, cmbRPState.SelectedIndex)) + "," +
                                 "       P_PIN_CODE      = '" + cmnService.J_ReplaceQuote(txtRPPIN.Text.Trim()) + "'," +
                                 "       P_PHONE         = '" + cmnService.J_ReplaceQuote(txtRPPhone.Text.Trim()) + "'," +
                                 "       P_STD           = '" + cmnService.J_ReplaceQuote(txtRPSTD.Text.Trim()) + "'," +
                                 "       P_EMAIL         = '" + cmnService.J_ReplaceQuote(txtRPEmail.Text.Trim()) + "'," +
                                 "       P_MOBILE        = '" + cmnService.J_ReplaceQuote(txtRPMobileNo.Text.Trim()) + "'," +
                                 "       PAO_CODE        = '" + cmnService.J_ReplaceQuote(txtPAOCode.Text.Trim()) + "'," +
                                 "       PAO_REG_NO      = '" + cmnService.J_ReplaceQuote(txtPAORegNo.Text.Trim()) + "'," +
                                 "       DDO_CODE        = '" + cmnService.J_ReplaceQuote(txtDDOCode.Text.Trim()) + "'," +
                                 "       DDO_REG_NO      = '" + cmnService.J_ReplaceQuote(txtDDORegNo.Text.Trim()) + "'," +
                                 "       D_STATE_ID      =  " + lngDStateId + "," +
                                 "       CIT_TDS_ADDRESS = '" + cmnService.J_ReplaceQuote(txtCITAddress.Text.Trim()) + "'," +
                                 "       CIT_TDS_CITY    = '" + cmnService.J_ReplaceQuote(txtCITCity.Text.Trim()) + "'," +
                                 "       CIT_TDS_PINCODE = '" + cmnService.J_ReplaceQuote(txtCITPin.Text.Trim()) + "'," +
                                 "       ALT_STD         = '" + cmnService.J_ReplaceQuote(txtAltSTD.Text.Trim()) + "'," +
                                 "       ALT_PHONE       = '" + cmnService.J_ReplaceQuote(txtAltPhone.Text.Trim()) + "'," +
                                 "       ALT_EMAIL       = '" + cmnService.J_ReplaceQuote(txtAltEmail.Text.Trim()) + "'," +
                                 "       P_ALT_STD       = '" + cmnService.J_ReplaceQuote(txtRPAltSTD.Text.Trim()) + "'," +
                                 "       P_ALT_PHONE     = '" + cmnService.J_ReplaceQuote(txtRPAltPhone.Text.Trim()) + "'," +
                                 "       P_ALT_EMAIL     = '" + cmnService.J_ReplaceQuote(txtRPAltEmail.Text.Trim()) + "'," +
                                 "       AIN_NO          = '" + cmnService.J_ReplaceQuote(txtAIN.Text.Trim()) + "'," +
                                 "       TAN_REG_NO      = '" + cmnService.J_ReplaceQuote(txtTANRegNo.Text.Trim()) + "'," +
                                 "       INACTIVE_FLAG   =  " + intInactiveCompany + "," +
                                 "       P_PAN           = '" + cmnService.J_ReplaceQuote(txtRPPAN.Text.Trim()) + "'," +
                                 "       GSTN            = '" + cmnService.J_ReplaceQuote(txtGSTN.Text.Trim()) + "'," +
                                 "       SECTION_194P_FLAG = " + int194P + "," +
                                 "       CSI_FILE_DOWNLOAD_OPTION = " + intCSIFileDownload + "," +
                                 "       DATE_OF_CREATION = " + strDateOfCreation + "," +
                                 "       ISD_CODE         = '" + cmnService.J_ReplaceQuote(txtISD.Text.Trim()) + "'," +
                                 "       P_ISD_CODE       = '" + cmnService.J_ReplaceQuote(txtRPISD.Text.Trim()) + "'," +
                                 "       P_COUNTRY        = '" + cmnService.J_ReplaceQuote(txtRPCountry.Text.Trim()) + "'," +
                                 "       COUNTRY          = '" + cmnService.J_ReplaceQuote(txtCountry.Text.Trim()) + "' " +
                                 "WHERE  COMPANY_ID       =  " + lngSearchId + "";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            txtDedEmpColName.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        //-- UPDATE THE TRANSACTION
                        //-----------------------------------------------------------
                        strSQL = "UPDATE TRN_COMPANY_INFO " +
                                 "SET    TAN_NO          =  '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "'," +
                                 "       PAN_NO          = '" + cmnService.J_ReplaceQuote(txtPANNo.Text.Trim()) + "'," +
                                 "       COMPANY_NAME    = '" + cmnService.J_ReplaceQuote(txtDedEmpColName.Text.Trim()) + "'," +
                                 "       BRANCH_DIV      = '" + cmnService.J_ReplaceQuote(txtBranch.Text.Trim()) + "'," +
                                 "       D_CATEGORY_ID   =  " + Convert.ToInt32(Support.GetItemData(cmbDeductorType, cmbDeductorType.SelectedIndex)) + "," +
                                 "       MINISTRY_ID     =  " + lngMinistryId + "," +
                                 "       MINISTRY_OTHER  = '" + cmnService.J_ReplaceQuote(txtOtherMinistry.Text.Trim()) + "'," +
                                 "       ADDRESS1        = '" + cmnService.J_ReplaceQuote(txtAddress1.Text.Trim()) + "'," +
                                 "       ADDRESS2        = '" + cmnService.J_ReplaceQuote(txtAddress2.Text.Trim()) + "'," +
                                 "       ADDRESS3        = '" + cmnService.J_ReplaceQuote(txtAddress3.Text.Trim()) + "'," +
                                 "       ADDRESS4        = '" + cmnService.J_ReplaceQuote(txtAddress4.Text.Trim()) + "'," +
                                 "       ADDRESS5        = '" + cmnService.J_ReplaceQuote(txtAddress5.Text.Trim()) + "'," +
                                 "       STATE_ID        =  " + Convert.ToInt32(Support.GetItemData(cmbState, cmbState.SelectedIndex)) + "," +
                                 "       PIN_CODE        = '" + cmnService.J_ReplaceQuote(txtPIN.Text.Trim()) + "'," +
                                 "       STD             = '" + cmnService.J_ReplaceQuote(txtSTD.Text.Trim()) + "'," +
                                 "       PHONE           = '" + cmnService.J_ReplaceQuote(txtPhone.Text.Trim()) + "'," +
                                 "       EMAIL           = '" + cmnService.J_ReplaceQuote(txtEmail.Text.Trim()) + "'," +
                                 "       PERSON_NAME     = '" + cmnService.J_ReplaceQuote(txtRPName.Text.Trim()) + "'," +
                                 "       DESIGNATION     = '" + cmnService.J_ReplaceQuote(txtRPDesignation.Text.Trim()) + "'," +
                                 "       FATHER_NAME     = '" + cmnService.J_ReplaceQuote(txtRPFatherName.Text.Trim()) + "'," +
                                 "       P_ADDRESS1      = '" + cmnService.J_ReplaceQuote(txtRPAddress1.Text.Trim()) + "'," +
                                 "       P_ADDRESS2      = '" + cmnService.J_ReplaceQuote(txtRPAddress2.Text.Trim()) + "'," +
                                 "       P_ADDRESS3      = '" + cmnService.J_ReplaceQuote(txtRPAddress3.Text.Trim()) + "'," +
                                 "       P_ADDRESS4      = '" + cmnService.J_ReplaceQuote(txtRPAddress4.Text.Trim()) + "'," +
                                 "       P_ADDRESS5      = '" + cmnService.J_ReplaceQuote(txtRPAddress5.Text.Trim()) + "'," +
                                 "       P_STATE_ID      =  " + Convert.ToInt32(Support.GetItemData(cmbRPState, cmbRPState.SelectedIndex)) + "," +
                                 "       P_PIN_CODE      = '" + cmnService.J_ReplaceQuote(txtRPPIN.Text.Trim()) + "'," +
                                 "       P_PHONE         = '" + cmnService.J_ReplaceQuote(txtRPPhone.Text.Trim()) + "'," +
                                 "       P_STD           = '" + cmnService.J_ReplaceQuote(txtRPSTD.Text.Trim()) + "'," +
                                 "       P_EMAIL         = '" + cmnService.J_ReplaceQuote(txtRPEmail.Text.Trim()) + "'," +
                                 "       P_MOBILE        = '" + cmnService.J_ReplaceQuote(txtRPMobileNo.Text.Trim()) + "'," +
                                 "       PAO_CODE        = '" + cmnService.J_ReplaceQuote(txtPAOCode.Text.Trim()) + "'," +
                                 "       PAO_REG_NO      = '" + cmnService.J_ReplaceQuote(txtPAORegNo.Text.Trim()) + "'," +
                                 "       DDO_CODE        = '" + cmnService.J_ReplaceQuote(txtDDOCode.Text.Trim()) + "'," +
                                 "       DDO_REG_NO      = '" + cmnService.J_ReplaceQuote(txtDDORegNo.Text.Trim()) + "'," +
                                 "       D_STATE_ID      =  " + lngDStateId + "," +
                                 "       CIT_TDS_ADDRESS = '" + cmnService.J_ReplaceQuote(txtCITAddress.Text.Trim()) + "'," +
                                 "       CIT_TDS_CITY    = '" + cmnService.J_ReplaceQuote(txtCITCity.Text.Trim()) + "'," +
                                 "       CIT_TDS_PINCODE = '" + cmnService.J_ReplaceQuote(txtCITPin.Text.Trim()) + "'," +
                                 "       ALT_STD         = '" + cmnService.J_ReplaceQuote(txtAltSTD.Text.Trim()) + "'," +
                                 "       ALT_PHONE       = '" + cmnService.J_ReplaceQuote(txtAltPhone.Text.Trim()) + "'," +
                                 "       ALT_EMAIL       = '" + cmnService.J_ReplaceQuote(txtAltEmail.Text.Trim()) + "'," +
                                 "       P_ALT_STD       = '" + cmnService.J_ReplaceQuote(txtRPAltSTD.Text.Trim()) + "'," +
                                 "       P_ALT_PHONE     = '" + cmnService.J_ReplaceQuote(txtRPAltPhone.Text.Trim()) + "'," +
                                 "       P_ALT_EMAIL     = '" + cmnService.J_ReplaceQuote(txtRPAltEmail.Text.Trim()) + "'," +
                                 "       AIN_NO          = '" + cmnService.J_ReplaceQuote(txtAIN.Text.Trim()) + "'," +
                                 "       TAN_REG_NO      = '" + cmnService.J_ReplaceQuote(txtTANRegNo.Text.Trim()) + "'," +
                                 "       GSTN            = '" + cmnService.J_ReplaceQuote(txtGSTN.Text.Trim()) + "'," +
                                 "       ISD_CODE        = '" + cmnService.J_ReplaceQuote(txtISD.Text.Trim()) + "'," +
                                 "       P_ISD_CODE      = '" + cmnService.J_ReplaceQuote(txtRPISD.Text.Trim()) + "'," +
                                 "       P_COUNTRY       = '" + cmnService.J_ReplaceQuote(txtRPCountry.Text.Trim()) + "'," +
                                 "       COUNTRY         = '" + cmnService.J_ReplaceQuote(txtCountry.Text.Trim()) + "' " +
                                 "WHERE  COMPANY_ID      =  " + lngSearchId + " " +
                                 "AND    UPDATE_FLAG     = 0";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            txtDedEmpColName.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        //-- 2015/04/21
                        strSQL = @"UPDATE TRN_COMPANY_INFO 
                                   SET    P_PAN       = '" + cmnService.J_ReplaceQuote(txtRPPAN.Text.Trim()) + @"'
                                   WHERE  COMPANY_ID  =  " + lngSearchId + @" 
                                   AND    PERSON_NAME ='" + cmnService.J_ReplaceQuote(txtRPName.Text.Trim()) + @"'
                                   AND    UPDATE_FLAG = 0 ";
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            txtDedEmpColName.Select();
                            dmlService.J_Rollback();
                            return;
                        }
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
                        //DisableControls();
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId);
                        break;
                        #endregion
                    case J_Mode.Delete:
                        #region Delete
                        //if (Convert.ToInt64(Convert.ToString(dgvGrid.RowIndex )) < 0)
                        if (dgvGrid.CurrentRow == null)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            return;
                        }
                        //-- Added By Abhishek Dey On 31/10/2019 --
                        #region IS COMPANY ACCESSIBLE FOR CLIENT
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                            {
                                //if (TdsMan.IsCompanyAccessible(Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.RowIndex, 0])), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                                if (TdsMan.IsCompanyAccessible(Convert.ToInt32(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                                {
                                        cmnService.J_UserMessage("You are not authorised to delete this company");
                                    BtnCancel.Select();
                                    return;
                                }

                                //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

                            }
                        }
                        #endregion
                        //-----------------------------------------
                        //Added by Indrajit on 11-02-2013
                        //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.RowIndex , 0]));

                        lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));

                        //if (Check_Record(lngSearchId) == 0) return;
                        //----------
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //Blocked by Indrajit on 11-02-2013
                        //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.RowIndex , 0]));
                        //-----------------------------------------------------------
                        //-- CHECK THE TRANSACTION
                        //-----------------------------------------------------------------------
                        //-- TRN_BASIC_INFO
                        //-----------------------------------------------------------------------
                        strSQL = "SELECT COMPANY_ID " +
                            "     FROM   TRN_BASIC_INFO " +
                            "     WHERE  COMPANY_ID = " + lngSearchId + " ";
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        {
                            cmnService.J_UserMessage("The Company cannot be deleted");
                            lblMode.Text = J_Mode.View;
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------------------
                        //-- MST_EMPLOYEE
                        //-----------------------------------------------------------------------
                        strSQL = "SELECT COMPANY_ID " +
                            "     FROM   MST_EMPLOYEE " +
                            "     WHERE  COMPANY_ID = " + lngSearchId + " ";
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                        {
                            cmnService.J_UserMessage("The Company cannot be deleted");
                            lblMode.Text = J_Mode.View;
                            dmlService.J_Rollback();
                            return;
                        }
                        //..........................................................
                        if (cmnService.J_UserMessage("Proceed with Deletion?", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            lblMode.Text = J_Mode.View;
                            dmlService.J_Rollback();
                            return;
                        }
                        
                        //-----------------------------------------------------------
                        strSQL = "DELETE FROM MST_COMPANY WHERE COMPANY_ID =  " + lngSearchId + "";
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

        #region EnableControl

        #region EnableControl[1]
        private void EnableControl(TextBox txtBox, string Enablity_T_F)
        {
            try
            {
                if (Enablity_T_F.ToUpper() == "T")
                {
                    txtBox.Text = "";
                    txtBox.Enabled = true;
                    txtBox.BackColor = Color.White;
                }
                else if (Enablity_T_F.ToUpper() == "F")
                {
                    txtBox.Text = "";
                    txtBox.Enabled = false;
                    txtBox.BackColor = Color.LightGray;
                }

            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region EnableControl[2]
        private void EnableControl(ComboBox cmbBox, string Enablity_T_F)
        {
            try
            {
                if (Enablity_T_F.ToUpper() == "T")
                {
                    if (cmbBox.SelectedIndex >= 0)
                        cmbBox.SelectedIndex = 0;
                    cmbBox.Enabled = true;
                    cmbBox.BackColor = Color.White;
                }
                else if (Enablity_T_F.ToUpper() == "F")
                {
                    if(cmbBox.SelectedIndex>=0)
                        cmbBox.SelectedIndex = 0;
                    cmbBox.Enabled = false;
                    cmbBox.BackColor = Color.LightGray;
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        
        #endregion 

        //Added by Indrajit on 11-02-2013
        #region Check_Record
        private long Check_Record(long lngSrchId)
        {
            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "MST_COMPANY", "COMPANY_ID", lngSrchId) == true) return lngSrchId;

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

        //-- Added By Abhishek Dey On 11/12/2017 --
        #region CheckFVUCompatibility

        private bool CheckFVUCompatibility()
        {
            try
            {               
                string strTAN = "";
                string strUploadType = "";

                TextReader txtRdr = new StreamReader(txtFVUPath.Text);
                //
                int NumberOfLines = 2;
                string[] ListLines = new string[NumberOfLines];
                //
                int intCaratPosition = 0;
                //
                for (int i = 0; i < NumberOfLines; i++)
                {
                    ListLines[i] = txtRdr.ReadLine();

                    intCaratPosition = ListLines[i].IndexOf("^");

                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "FH")
                    {
                        // Upload Type
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            //
                        }
                        strUploadType = ListLines[i].Substring(intCaratPosition + 1);
                        strUploadType = ListLines[i].Substring(intCaratPosition + 1, strUploadType.IndexOf("^"));

                        if (strUploadType != "R")
                        {                            
                            cmnService.J_UserMessage("Invalid file selected. Please select either Conso file or text file of Regular returns.");
                            //--
                            txtRdr.Close();
                            txtRdr.Dispose();
                            //
                            btnSelectTDSPath.Select();
                            return false;
                        }
                        //                        
                        //CHECKING DATE OF CREATION OF OF TDS FILE FOR DISPLAY PURPOSE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }                        
                        //

                        // FVU Version [2.126]
                        for (int a = 1; a < 10; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            //
                        }
                        //
                        for (int a = 1; a < 1; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            //
                        }                        
                    }

                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "BH")
                    {
                        // FORM NO.
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                       
                        // TAN
                        for (int a = 1; a < 9; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTAN = ListLines[i].Substring(intCaratPosition + 1, 10);
                        //                       
                        // TAN FETCH
                        
                        if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM MST_COMPANY WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(strTAN.ToUpper()) + "'")) > 0)
                        {
                            string strCompanyNameFromDatabase = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_NAME FROM MST_COMPANY WHERE TAN_NO ='" + cmnService.J_ReplaceQuote(strTAN.ToUpper()) + "'"));
                            //
                            cmnService.J_UserMessage("Company " + strCompanyNameFromDatabase + " [ " + cmnService.J_ReplaceQuote(strTAN.ToUpper()) + " ] already exists.");
                            txtRdr.Close();
                            txtRdr.Dispose();
                            //
                            //cmbCompany.Select();
                            return false;
                        }                      
                        
                    }
                }
                txtRdr.Close();
                txtRdr.Dispose();
                return true;
            }
            catch (Exception err)
            {
                btnSelectTDSPath.Select();
                return false;
            }
        }
        //-----------------------------------------

        #endregion

        #region ImportCompanyMasterData
        public bool ImportCompanyMasterData(string FVUPath)
        {

            #region DECLARATION

            int NumberOfLines = 0;

            //string strFVUVersion = "";
            //double dblFVUVersion = 0;

            string strFormNo = "";
            string strTAN = "";
            string strPAN = "";
            string strAssessmentYear = "";
            string strFinancialYear = "";
            string strQTR = "";
            string strDeductorName = "";
            string strDeductorBranch = "";
            string strDeductorAddress1 = "";
            string strDeductorAddress2 = "";
            string strDeductorAddress3 = "";
            string strDeductorAddress4 = "";
            string strDeductorAddress5 = "";
            string strDeductorStateCode = "";
            long lngDeductorStateID = 0;
            string strDeductorPIN = "";
            string strDeductorEmail = "";
            string strDeductorSTD = "";
            string strDeductorTelePhone = "";
            string strDeductorChangeofAddress = "";
            string strDeductorType = "";
            long lngDeductorCategoryId = 0;
            string strAltSTDCode = "";
            string strAltTelePhone = "";
            string strAltEmail = "";


            string strRPName = "";
            string strRPDesignation = "";
            string strRPAddress1 = "";
            string strRPAddress2 = "";
            string strRPAddress3 = "";
            string strRPAddress4 = "";
            string strRPAddress5 = "";
            string strRPStateCode = "";
            long lngRPStateID = 0;
            string strRPPIN = "";
            string strRPEmail = "";
            string strRPMobile = "";
            string strRPSTD = "";
            string strRPTelePhone = "";
            string strRPChangeofAddress = "";
            string strRPType = "";
            string strRPAltSTDCode = "";
            string strRPAltTelePhone = "";
            string strRPAltEmail = "";
            string strRPPAN = "";

            string strBatchTotal = "";
            string strCountSalaryDetailRecords = "";
            string strBatchTotalSalary = "";
            string strAOApproval = "";
            string strAOApprovalNumber = "";

            string strDStateCode = "";
            long lngDStateID = 0;
            string strPAO = "";
            string strDDO = "";
            string strMinistryName = "";
            long lngMinistryId = 0;
            string strOtherMinistryName = "";
            string strOtherMinistryDetails = "";
            string strPAORegNo = "";
            string strDDORegNo = "";
            string strTANRegNo = "";
            string strAccOfficeIdentificationNo = "";
            string strGSTNo = "";


            string strDDRecordNumber = "";
            string strDeducteeCode = "";
            string strDeducteePAN = "";
            string strDeducteeName = "";

            string strSDRecordNumber = "";
            string strEmployeePAN = "";
            string strEmployeeName = "";
            string strEmployeeCategory = "";

            long lngMaxCompanyId = 0;
            #endregion

            try
            {

                //
                TextReader txtRdrGetNoLines = new StreamReader(FVUPath);
                while (txtRdrGetNoLines.ReadLine() != null)
                {
                    NumberOfLines++;
                }
                txtRdrGetNoLines.Close();
                txtRdrGetNoLines.Dispose();

                string[] ListLines = new string[NumberOfLines];
                //
                #region ReadFromFVUFile
                //cmnService.J_UserMessage(NumberOfLines.ToString());
                //
                //
                TextReader txtRdr = new StreamReader(FVUPath);
                int intCaratPosition = 0;
                //
                for (int i = 0; i < NumberOfLines; i++)
                {
                    ListLines[i] = txtRdr.ReadLine();

                    intCaratPosition = ListLines[i].IndexOf("^");

                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "BH")
                    {
                        #region INSERTING COMPANY DATA -- 2015-07-28 @@DHRUB

                        // 1 LINE NUMBER
                        // 2 RECORD TYPE
                        // 3 BATCH NUMBER
                        // 4 COUNT OF CHALLAN/TRANSFER VOUCHER RECORDS
                        // 5 FORM NO.
                        // 6 TRANSACTION TYPE
                        // 7 BATCH UPDATION INDICATOR
                        // 8 ORIGINAL TOKEN NUMBER
                        // 9 PREVIOUS TOKEN NUMBER
                        // 10 TOKEN NUMBER OF THE STATEMENT SUBMITTED
                        // 11 TOKEN NUMBER DATE
                        // 12 LAST TAN
                        // 13 TAN

                        for (int a = 1; a < 12; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTAN = ListLines[i].Substring(intCaratPosition + 1, 10);


                        // 14 EXPECTED CHALLAN RECORD NO.
                        // 15 PAN
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strPAN = ListLines[i].Substring(intCaratPosition + 1, 10);

                        // 16 ASSESSMENT YEAR
                        // 17 FINANCIAL YEAR
                        // 18 PERIOD/QTR
                        // 19 DEDUCTOR NAME

                        for (int a = 1; a < 5; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorName = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorName = ListLines[i].Substring(intCaratPosition + 1, strDeductorName.IndexOf("^"));

                        // 20 DEDUCTOR BRANCH
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorBranch = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorBranch = ListLines[i].Substring(intCaratPosition + 1, strDeductorBranch.IndexOf("^"));

                        // 21 DEDUCTOR ADDRESS1
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorAddress1 = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorAddress1 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress1.IndexOf("^"));

                        // 22 DEDUCTOR ADDRESS2
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorAddress2 = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorAddress2 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress2.IndexOf("^"));

                        // 23 DEDUCTOR ADDRESS3
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorAddress3 = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorAddress3 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress3.IndexOf("^"));

                        // 24 DEDUCTOR ADDRESS4
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorAddress4 = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorAddress4 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress4.IndexOf("^"));

                        // 25 DEDUCTOR ADDRESS5
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorAddress5 = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorAddress5 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress5.IndexOf("^"));

                        // 26 DEDUCTOR STATE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorStateCode = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorStateCode = ListLines[i].Substring(intCaratPosition + 1, strDeductorStateCode.IndexOf("^"));
                        //
                        lngDeductorStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDeductorStateCode) + "'");

                        // 27 DEDUCTOR PIN
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorPIN = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorPIN = ListLines[i].Substring(intCaratPosition + 1, strDeductorPIN.IndexOf("^"));

                        // 28 DEDUCTOR EMAIL
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorEmail = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorEmail = ListLines[i].Substring(intCaratPosition + 1, strDeductorEmail.IndexOf("^"));

                        // 29 DEDUCTOR STD
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorSTD = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorSTD = ListLines[i].Substring(intCaratPosition + 1, strDeductorSTD.IndexOf("^"));

                        // 30 DEDUCTOR TELEPHONE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorTelePhone = ListLines[i].Substring(intCaratPosition + 1, strDeductorTelePhone.IndexOf("^"));

                        // 31 DEDUCTOR CHANGE OF ADDRESS
                        // 32 DEDUCTOR TYPE
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorType = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorType = ListLines[i].Substring(intCaratPosition + 1, strDeductorType.IndexOf("^"));
                        //
                        lngDeductorCategoryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT CATEGORY_ID FROM MST_CATEGORY WHERE CATEGORY_CODE = '" + cmnService.J_ReplaceQuote(strDeductorType) + "'");

                        if (lngDeductorCategoryId == 0)
                        {
                            string strDeductorTypeFromPANNo = "";
                            //-----------------------------------------------
                            if (strPAN.Substring(4, 1).ToUpper() == "P" || strPAN.Substring(4, 1).ToUpper() == "H")
                                strDeductorTypeFromPANNo = "Q";
                            //-----------------------------------------------
                            else if (strPAN.Substring(4, 1).ToUpper() == "F")
                                strDeductorTypeFromPANNo = "F";
                            //-----------------------------------------------
                            else
                                strDeductorTypeFromPANNo = "K";
                            lngDeductorCategoryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT CATEGORY_ID FROM MST_CATEGORY WHERE CATEGORY_CODE = '" + cmnService.J_ReplaceQuote(strDeductorTypeFromPANNo) + "'");
                        }

                        // 33 RP NAME
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPName = ListLines[i].Substring(intCaratPosition + 1);
                        strRPName = ListLines[i].Substring(intCaratPosition + 1, strRPName.IndexOf("^"));

                        // 34 RP DESIGNATION
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPDesignation = ListLines[i].Substring(intCaratPosition + 1);
                        strRPDesignation = ListLines[i].Substring(intCaratPosition + 1, strRPDesignation.IndexOf("^"));

                        // 35 RP ADDRESS1
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPAddress1 = ListLines[i].Substring(intCaratPosition + 1);
                        strRPAddress1 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress1.IndexOf("^"));

                        // 36 RP ADDRESS2
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPAddress2 = ListLines[i].Substring(intCaratPosition + 1);
                        strRPAddress2 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress2.IndexOf("^"));

                        // 37 RP ADDRESS3
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPAddress3 = ListLines[i].Substring(intCaratPosition + 1);
                        strRPAddress3 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress3.IndexOf("^"));

                        // 38 RP ADDRESS4
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPAddress4 = ListLines[i].Substring(intCaratPosition + 1);
                        strRPAddress4 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress4.IndexOf("^"));

                        // 39 RP ADDRESS5
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPAddress5 = ListLines[i].Substring(intCaratPosition + 1);
                        strRPAddress5 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress5.IndexOf("^"));

                        // 40 RP STATE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPStateCode = ListLines[i].Substring(intCaratPosition + 1);
                        strRPStateCode = ListLines[i].Substring(intCaratPosition + 1, strRPStateCode.IndexOf("^"));
                        //
                        lngRPStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strRPStateCode) + "'");

                        // 41 RP PIN
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPPIN = ListLines[i].Substring(intCaratPosition + 1);
                        strRPPIN = ListLines[i].Substring(intCaratPosition + 1, strRPPIN.IndexOf("^"));

                        // 42 RP EMAIL
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPEmail = ListLines[i].Substring(intCaratPosition + 1);
                        strRPEmail = ListLines[i].Substring(intCaratPosition + 1, strRPEmail.IndexOf("^"));

                        //-- Added By Abhishek Dey On 13/12/2017 --
                        // RESPONSIBLE PERSON MOBILE NUMBER
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPMobile = ListLines[i].Substring(intCaratPosition + 1);
                        strRPMobile = ListLines[i].Substring(intCaratPosition + 1, strRPMobile.IndexOf("^"));
                        //-----------------------------------------
                        if (Path.GetExtension(txtFVUPath.Text).ToUpper() == ".TDS")
                        {
                            #region Initialize Details While TDS File

                            // 43 REMARK
                            // 44 RP STD
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPSTD = ListLines[i].Substring(intCaratPosition + 1);
                            strRPSTD = ListLines[i].Substring(intCaratPosition + 1, strRPSTD.IndexOf("^"));

                            // 45 RP TELEPHONE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                            strRPTelePhone = ListLines[i].Substring(intCaratPosition + 1, strRPTelePhone.IndexOf("^"));

                            // 46 RP CHANGE OF ADDRESS
                            // 47 BATCH TOTAL
                            // 48 RP MOBILE NO.
                            for (int a = 1; a < 4; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            //-- Commented By Abhishek Dey On 13/12/2017 --
                            strRPMobile = ListLines[i].Substring(intCaratPosition + 1);
                            strRPMobile = ListLines[i].Substring(intCaratPosition + 1, strRPMobile.IndexOf("^"));
                            //-- ADDED By Abhishek Dey On 16/03/2018 --
                            //if (string.IsNullOrEmpty(strRPMobile))
                            //{
                            //    strRPMobile = ListLines[i].Substring(intCaratPosition + 1);
                            //    strRPMobile = ListLines[i].Substring(intCaratPosition + 1, strRPMobile.IndexOf("^"));
                            //}

                            //---------------------------------------------

                            // 49 COUNT SALARY DETAIL RECORDS
                            // 50 BATCH GROSS TOTAL INCOME
                            // 51 AO APPROVAL
                            // 52 AO APPROVAL NUMBER
                            // 53 LAST DEDUCTOR TYPE
                            // 54 DEDUCTOR STATE NAME
                            for (int a = 1; a < 7; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strDStateCode = ListLines[i].Substring(intCaratPosition + 1);
                            strDStateCode = ListLines[i].Substring(intCaratPosition + 1, strDStateCode.IndexOf("^"));
                            //
                            lngDStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDStateCode) + "'");

                            // 55 PAO CODE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strPAO = ListLines[i].Substring(intCaratPosition + 1);
                            strPAO = ListLines[i].Substring(intCaratPosition + 1, strPAO.IndexOf("^"));

                            // 56 DDO CODE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strDDO = ListLines[i].Substring(intCaratPosition + 1);
                            strDDO = ListLines[i].Substring(intCaratPosition + 1, strDDO.IndexOf("^"));

                            // 57 MINISTRY
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strMinistryName = ListLines[i].Substring(intCaratPosition + 1);
                            strMinistryName = ListLines[i].Substring(intCaratPosition + 1, strMinistryName.IndexOf("^"));
                            //
                            lngMinistryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT MINISTRY_ID FROM MST_MINISTRY WHERE MINISTRY_CODE = '" + cmnService.J_ReplaceQuote(strMinistryName) + "'");

                            // 58 OTHER MINISTRY
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strOtherMinistryName = ListLines[i].Substring(intCaratPosition + 1);
                            strOtherMinistryName = ListLines[i].Substring(intCaratPosition + 1, strOtherMinistryName.IndexOf("^"));


                            //// 58 (A) OTHER MINISTRY                            
                            //for (int a = 1; a < 2; a++)
                            //{
                            //    intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            //}
                            //strOtherMinistryDetails = ListLines[i].Substring(intCaratPosition + 1);
                            //strOtherMinistryDetails = ListLines[i].Substring(intCaratPosition + 1, strOtherMinistryName.IndexOf("^"));


                            // 59 RP PAN NO
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPPAN = ListLines[i].Substring(intCaratPosition + 1);
                            strRPPAN = ListLines[i].Substring(intCaratPosition + 1, strRPPAN.IndexOf("^"));
                            //-- ANIK.G @ 2015/12/14
                            if (strRPPAN != "")
                            {
                                if (strRPPAN.Length > 10)
                                {
                                    strRPPAN = "";
                                }
                            }

                            //60 PAO REGISTRATION NUMBER
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strPAORegNo = ListLines[i].Substring(intCaratPosition + 1);
                            strPAORegNo = ListLines[i].Substring(intCaratPosition + 1, strPAORegNo.IndexOf("^"));

                            // 61 DDO REGISTRATION NUMBER
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strDDORegNo = ListLines[i].Substring(intCaratPosition + 1);
                            strDDORegNo = ListLines[i].Substring(intCaratPosition + 1, strDDORegNo.IndexOf("^"));

                            // 62 HASH CODE 
                            // 63 ALTERNATE STD CODE.                            
                            //for (int a = 1; a < 2; a++)
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strAltSTDCode = ListLines[i].Substring(intCaratPosition + 1);
                            strAltSTDCode = ListLines[i].Substring(intCaratPosition + 1, strAltSTDCode.IndexOf("^"));

                            // 64 ALTERNATE TEL-PHONE NO. 
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strAltTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                            strAltTelePhone = ListLines[i].Substring(intCaratPosition + 1, strAltTelePhone.IndexOf("^"));

                            // 65 ALTERNATE EMAIL.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strAltEmail = ListLines[i].Substring(intCaratPosition + 1);
                            strAltEmail = ListLines[i].Substring(intCaratPosition + 1, strAltEmail.IndexOf("^"));

                            // 66 RP ALTERNATE STD CODE.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPAltSTDCode = ListLines[i].Substring(intCaratPosition + 1);
                            strRPAltSTDCode = ListLines[i].Substring(intCaratPosition + 1, strRPAltSTDCode.IndexOf("^"));


                            // 67 RP ALTERNATE TEL-PHONE NO.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPAltTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                            strRPAltTelePhone = ListLines[i].Substring(intCaratPosition + 1, strRPAltTelePhone.IndexOf("^"));


                            // 68 RP ALTERNATE EMAIL.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPAltEmail = ListLines[i].Substring(intCaratPosition + 1);
                            strRPAltEmail = ListLines[i].Substring(intCaratPosition + 1, strRPAltEmail.IndexOf("^"));

                            // 69 AIN NO.     
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strAccOfficeIdentificationNo = ListLines[i].Substring(intCaratPosition + 1);
                            if (strAccOfficeIdentificationNo != "")
                                if(strAccOfficeIdentificationNo.Contains("^") == true)
                                    strAccOfficeIdentificationNo = ListLines[i].Substring(intCaratPosition + 1, strAccOfficeIdentificationNo.IndexOf("^"));
                                else
                                    strAccOfficeIdentificationNo = strAccOfficeIdentificationNo;
                            else
                                strAccOfficeIdentificationNo = "";

                            // 70 GSTN NO.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strGSTNo = ListLines[i].Substring(intCaratPosition + 1);
                            if (strGSTNo != "" & strGSTNo.Length == 15)
                                strGSTNo = ListLines[i].Substring(intCaratPosition + 1, strGSTNo.IndexOf("^"));
                            else
                                strGSTNo = "";


                            #endregion
                        }
                        else
                        {
                            #region Initialize Details While Text File
                            // 43 RP MOBILE NO.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPMobile = ListLines[i].Substring(intCaratPosition + 1);
                            strRPMobile = ListLines[i].Substring(intCaratPosition + 1, strRPMobile.IndexOf("^"));

                            // 44 RP STD
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPSTD = ListLines[i].Substring(intCaratPosition + 1);
                            strRPSTD = ListLines[i].Substring(intCaratPosition + 1, strRPSTD.IndexOf("^"));

                            // 45 RP TELEPHONE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                            strRPTelePhone = ListLines[i].Substring(intCaratPosition + 1, strRPTelePhone.IndexOf("^"));

                            // 46 RP CHANGE OF ADDRESS
                            // 47 BATCH TOTAL
                            // 48 UNMATCHED CHALLAN COUNT
                            // 49 COUNT SALARY DETAIL RECORDS
                            // 50 BATCH TOTAL OF GROSS
                            // 51 AO APPROVAL
                            // 52 AO APPROVAL NUMBER
                            // 53 LAST DEDUCTOR TYPE
                            // 54 DEDUCTOR STATE NAME
                            for (int a = 1; a < 10; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strDStateCode = ListLines[i].Substring(intCaratPosition + 1);
                            strDStateCode = ListLines[i].Substring(intCaratPosition + 1, strDStateCode.IndexOf("^"));
                            //
                            lngDStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDStateCode) + "'");

                            // 55 PAO CODE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strPAO = ListLines[i].Substring(intCaratPosition + 1);
                            strPAO = ListLines[i].Substring(intCaratPosition + 1, strPAO.IndexOf("^"));

                            // 56 DDO CODE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strDDO = ListLines[i].Substring(intCaratPosition + 1);
                            strDDO = ListLines[i].Substring(intCaratPosition + 1, strDDO.IndexOf("^"));

                            // 57 MINISTRY
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strMinistryName = ListLines[i].Substring(intCaratPosition + 1);
                            strMinistryName = ListLines[i].Substring(intCaratPosition + 1, strMinistryName.IndexOf("^"));
                            //
                            lngMinistryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT MINISTRY_ID FROM MST_MINISTRY WHERE MINISTRY_CODE = '" + cmnService.J_ReplaceQuote(strMinistryName) + "'");

                            // 58 OTHER MINISTRY
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strOtherMinistryName = ListLines[i].Substring(intCaratPosition + 1);
                            strOtherMinistryName = ListLines[i].Substring(intCaratPosition + 1, strOtherMinistryName.IndexOf("^"));

                            // 59 RP PAN NO
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPPAN = ListLines[i].Substring(intCaratPosition + 1);
                            strRPPAN = ListLines[i].Substring(intCaratPosition + 1, strRPPAN.IndexOf("^"));
                            //-- ANIK.G @ 2015/12/14
                            if (strRPPAN != "")
                            {
                                if (strRPPAN.Length > 10)
                                {
                                    strRPPAN = "";
                                }
                            }
                            //60 PAO REGISTRATION NUMBER
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strPAORegNo = ListLines[i].Substring(intCaratPosition + 1);
                            strPAORegNo = ListLines[i].Substring(intCaratPosition + 1, strPAORegNo.IndexOf("^"));

                            // 61 DDO REGISTRATION NUMBER
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strDDORegNo = ListLines[i].Substring(intCaratPosition + 1);
                            strDDORegNo = ListLines[i].Substring(intCaratPosition + 1, strDDORegNo.IndexOf("^"));

                            // 62 ALTERNATE STD CODE.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strAltSTDCode = ListLines[i].Substring(intCaratPosition + 1);
                            strAltSTDCode = ListLines[i].Substring(intCaratPosition + 1, strAltSTDCode.IndexOf("^"));

                            // 63 ALTERNATE TEL-PHONE NO.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strAltTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                            strAltTelePhone = ListLines[i].Substring(intCaratPosition + 1, strAltTelePhone.IndexOf("^"));

                            // 64 ALTERNATE EMAIL.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strAltEmail = ListLines[i].Substring(intCaratPosition + 1);
                            strAltEmail = ListLines[i].Substring(intCaratPosition + 1, strAltEmail.IndexOf("^"));


                            // 65 RP ALTERNATE STD CODE.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPAltSTDCode = ListLines[i].Substring(intCaratPosition + 1);
                            strRPAltSTDCode = ListLines[i].Substring(intCaratPosition + 1, strRPAltSTDCode.IndexOf("^"));


                            // 66 RP ALTERNATE TEL-PHONE NO.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPAltTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                            strRPAltTelePhone = ListLines[i].Substring(intCaratPosition + 1, strRPAltTelePhone.IndexOf("^"));

                            // 67 RP ALTERNATE EMAIL.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strRPAltEmail = ListLines[i].Substring(intCaratPosition + 1);
                            strRPAltEmail = ListLines[i].Substring(intCaratPosition + 1, strRPAltEmail.IndexOf("^"));

                            // 68 AIN NO.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strAccOfficeIdentificationNo = ListLines[i].Substring(intCaratPosition + 1);
                            strAccOfficeIdentificationNo = ListLines[i].Substring(intCaratPosition + 1, strAccOfficeIdentificationNo.IndexOf("^"));


                            // 69 GSTN NO.
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strGSTNo = ListLines[i].Substring(intCaratPosition + 1);
                            if (strGSTNo != "")
                                strGSTNo = ListLines[i].Substring(intCaratPosition + 1, strGSTNo.IndexOf("^"));


                            // 69 RECORD HASH
                            #endregion
                        }
                        ////
                        //lngSelectedCompanyId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "MST_COMPANY", "COMPANY_ID");                       
                        //--
                        //txtTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_COMPANY WHERE COMPANY_ID = " + lngSelectedCompanyId));
                        //return true;
                        //---------------------     
                        #endregion
                    }
                }
                #endregion
                //
                //  POPULATE COMPANY DATA                        
                txtDedEmpColName.Text = strDeductorName.ToUpper();
                txtTANNo.Text = strTAN.ToUpper();
                txtPANNo.Text = strPAN.ToUpper();
                txtBranch.Text = strDeductorBranch.ToUpper();
                cmbDeductorType.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT CATEGORY_CODE + ' - ' + CATEGORY_DESCRIPTION FROM MST_CATEGORY WHERE CATEGORY_ID = " + lngDeductorCategoryId + " "));
                //txtGSTN.Text = 
                cmbState.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT STATE_NAME FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDeductorStateCode) + "'"));
                txtPAOCode.Text = strPAO.ToUpper();
                txtPAORegNo.Text = strPAORegNo.ToUpper();
                txtDDOCode.Text = strDDO.ToUpper();
                txtDDORegNo.Text = strDDORegNo.ToUpper();
                cmbGovtDedState.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT STATE_NAME FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDStateCode) + "'"));
                cmbMinistry.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MINISTRY_NAME FROM MST_MINISTRY WHERE MINISTRY_ID = " + lngMinistryId + " "));  //-- Added On 13/12/2017 --
                txtOtherMinistry.Text = strOtherMinistryName;
                txtAddress1.Text = strDeductorAddress1.ToUpper();
                txtAddress2.Text = strDeductorAddress3.ToUpper();
                txtAddress3.Text = strDeductorAddress5.ToUpper();
                txtAddress4.Text = strDeductorAddress2.ToUpper();
                txtAddress5.Text = strDeductorAddress4.ToUpper();
                txtPIN.Text = strDeductorPIN;
                txtSTD.Text = strDeductorSTD;
                txtPhone.Text = strDeductorTelePhone;
                txtEmail.Text = strDeductorEmail;
                txtRPName.Text = strRPName.ToUpper();
                txtRPDesignation.Text = strRPDesignation.ToUpper();
                txtRPAddress1.Text = strRPAddress1.ToUpper();
                txtRPAddress2.Text = strRPAddress3.ToUpper();
                txtRPAddress3.Text = strRPAddress5.ToUpper();
                txtRPAddress4.Text = strRPAddress2.ToUpper();
                txtRPAddress5.Text = strRPAddress4.ToUpper();
                txtRPPIN.Text = strRPPIN;
                cmbRPState.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT STATE_NAME FROM MST_STATE WHERE STATE_ID = " + lngRPStateID + " "));
                txtRPPhone.Text = strRPTelePhone;
                txtRPSTD.Text = strRPSTD;
                txtRPEmail.Text = strRPEmail;
                txtRPMobileNo.Text = strRPMobile;
                txtRPPAN.Text = strRPPAN;
                txtAltSTD.Text = strAltSTDCode;
                txtAltPhone.Text = strAltTelePhone;
                txtAltEmail.Text = strAltEmail;
                txtRPAltSTD.Text = strRPAltSTDCode;
                txtRPAltPhone.Text = strRPAltTelePhone;
                txtRPAltEmail.Text = strRPAltEmail;
                txtAIN.Text = strAccOfficeIdentificationNo;
                txtGSTN.Text = strGSTNo;
                //
                grpImport.Visible = false;
                //
                grpCompanyName.Enabled = true;
                grpBasicInformation.Enabled = true;
                grpAddress.Enabled = true;
                grpResponsiblePersonDetails.Enabled = true;
                grpResponsiblePerson.Enabled = true;
                grpInactiveTAN.Enabled = true;
                grpGovtDeductors.Enabled = true;
                grpCITDetails.Enabled = true;

            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
            //
            return true;
        }

        #endregion

        //-----------------------------------------

        #endregion

        //private async void Button1_Click(object sender, EventArgs e)
        //{

        //}

        #region FetchDataAsync
        private async Task FetchDataAsync(string TAN, string Password)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //var json = "{\"uid\":\"CALP08143C\",\"password\":\"Pdsinfo@6\"}";
                    var json = "{\"uid\":\"" + TAN + "\",\"password\":\"" + Password + "\"}";
                    //var json = "{\"uid\":\"MRTS22129C \",\"password\":\"Saurabh#1981\"}";
                    //var json = "{\"uid\":\"DELA45227A\",\"password\":\"Tarun@1236\"}";
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://www.tdsman.com/get-company", content);
                    //response.EnsureSuccessStatusCode();
                    if (!response.IsSuccessStatusCode)
                    {
                        //string errorMsg = await response.Content.ReadAsStringAsync();
                        //MessageBox.Show($"Login failed. Server returned {response.StatusCode}.\nDetails: {errorMsg}");
                        prgFetchITBar.Value = prgFetchITBar.Maximum;
                        // Stop the timer
                        tmrFetchITPortal.Stop();
                        // Set progress bar to full
                        cmnService.J_UserMessage("Login failed.");
                        //--
                        grpITLogin.Visible = false;
                        //
                        grpCompanyName.Enabled = true;
                        grpBasicInformation.Enabled = true;
                        grpAddress.Enabled = true;
                        grpResponsiblePersonDetails.Enabled = true;
                        grpResponsiblePerson.Enabled = true;
                        grpInactiveTAN.Enabled = true;
                        grpGovtDeductors.Enabled = true;
                        grpCITDetails.Enabled = true;
                        //--
                        return; // stop further processing
                    }

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    //File.WriteAllText("company-data.json", jsonResponse);

                    //MessageBox.Show("Data fetched and saved successfully!", "Success",
                    //    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var obj = JObject.Parse(jsonResponse);

                    // Check success
                    if (obj["success"] != null && obj["success"].Value<bool>())
                    {
                        // Extract fields
                        string tan = obj["myProfileData"]?["tanOfOrganization"]?.ToString();
                        txtTANNo.Text = tan;
                        string orgName = obj["myProfileData"]?["organizationName"]?.ToString();
                        txtDedEmpColName.Text = orgName;
                        string pan = obj["myProfileData"]?["panOfOrganization"]?.ToString();
                        if (pan.Length == 10)
                            txtPANNo.Text = pan;
                        //
                        string mobile = obj["myProfileData"]?["mobilePrimarySelf"]?.ToString();
                        txtRPMobileNo.Text = mobile;
                        txtPhone.Text = mobile;
                        string email = obj["myProfileData"]?["emailPrimarySelf"]?.ToString();
                        txtEmail.Text = email;
                        //
                        string address = obj["myProfileData"]?["address"]?.ToString();
                        string country = obj["parsedAddress"]?["country"]?.ToString();
                        //
                        string state = obj["parsedAddress"]?["state"]?.ToString();
                        cmbState.Text = state;
                        string pincode = obj["parsedAddress"]?["pincode"]?.ToString();
                        txtPIN.Text = pincode;
                        //
                        string postOffice = obj["parsedAddress"]?["postOffice"]?.ToString();
                        if(postOffice.Length > 25)
                            txtAddress4.Text = postOffice.Replace(orgName, string.Empty).Substring(0, 25);
                        else
                            txtAddress4.Text = postOffice;
                        //
                        string districtCity = obj["parsedAddress"]?["districtCity"]?.ToString();
                        if (districtCity.Length > 25)
                            txtAddress3.Text = districtCity.Replace(orgName, string.Empty).Substring(0, 25);
                        else
                            txtAddress3.Text = districtCity;
                        //
                        string areaLocality = obj["parsedAddress"]?["areaLocality"]?.ToString();
                        if (areaLocality.Length > 25)
                            txtAddress5.Text = areaLocality.Replace(orgName, string.Empty).Substring(0, 25);
                        else
                            txtAddress5.Text = areaLocality.Replace(orgName, string.Empty);
                        //
                        string flatDoorBuilding = obj["parsedAddress"]?["flatDoorBuilding"]?.ToString();
                        if (flatDoorBuilding.Length > 25)
                            txtAddress1.Text = flatDoorBuilding.Replace(orgName, string.Empty).Substring(0, 25);
                        else
                            txtAddress1.Text = flatDoorBuilding.Replace(orgName, string.Empty);
                        //
                        string roadStreetBlockSector = obj["parsedAddress"]?["roadStreetBlockSector"]?.ToString();
                        if (roadStreetBlockSector.Length > 25)
                            txtAddress2.Text = roadStreetBlockSector.Replace(orgName, string.Empty).Substring(0, 25);
                        else
                            txtAddress2.Text = roadStreetBlockSector;
                        //
                        string keyPersonName = obj["keyPersonData"]?["name"]?.ToString();
                        txtRPName.Text = keyPersonName;
                        string keyPersonPan = obj["keyPersonData"]?["pan"]?.ToString();
                        txtRPPAN.Text = keyPersonPan;

                        // Example: Show in MessageBox
                        //MessageBox.Show(
                        //    $"TAN: {tan}\nOrg: {orgName}\nPAN: {pan}\nMobile: {mobile}\nEmail: {email}\nAddress: {address}\nKey Person: {keyPersonName} ({keyPersonPan})",
                        //    "Extracted Data",
                        //    MessageBoxButtons.OK,
                        //    MessageBoxIcon.Information
                        //);
                    }
                }
                //--
                //
                grpITLogin.Visible = false;
                //
                grpCompanyName.Enabled = true;
                grpBasicInformation.Enabled = true;
                grpAddress.Enabled = true;
                grpResponsiblePersonDetails.Enabled = true;
                grpResponsiblePerson.Enabled = true;
                grpInactiveTAN.Enabled = true;
                grpGovtDeductors.Enabled = true;
                grpCITDetails.Enabled = true;
                //--
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        #endregion

        private void Label69_Click(object sender, EventArgs e)
        {

        }

        #region BtnFetchTANInfo_Click
        private async void BtnFetchTANInfo_Click(object sender, EventArgs e)
        {
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connectivity not found");
                BtnExit.Select();
                return;
            }
            if (txtITUserID.Text.Trim() == "")
            {
                cmnService.J_UserMessage("TAN No. - Cannot be Blank");
                txtITUserID.Select();
                return;
            }
            if (txtITPassword.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Password - Cannot be Blank");
                txtITPassword.Select();
                return;
            }
            //
            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtITUserID.Text.Trim(), 1), J_DataType.Character) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                txtITUserID.Select();
                return ;
            }
            else
            {
                if (txtITUserID.Text.Trim().Length == 10)
                {
                    char TANLastCharacter = Convert.ToChar(cmnService.J_Right(txtITUserID.Text, 1));
                    if ((int)TANLastCharacter > 78)
                    {
                        cmnService.J_UserMessage("Incorrect Format of the TAN\nLast character should be between 'A' to 'N'.");
                        txtITUserID.Select();
                        return ;
                    }
                }
            }
            //
            strSQL = "SELECT COMPANY_ID " +
                        "     FROM   MST_COMPANY " +
                        "     WHERE  TAN_NO  ='" + cmnService.J_ReplaceQuote(txtITUserID.Text.Trim()) + "'";
            //if (lblMode.Text == J_Mode.Edit)
            //    strSQL = strSQL + "AND COMPANY_ID <> " + lngSearchId;
            //--
            if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
            {
                cmnService.J_UserMessage("Company with same [TAN] exists");
                txtITUserID.Select();
                return ;
            }
            //
            if (txtITPassword.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Password - Cannot be Blank");
                txtITPassword.Select();
                return ;
            }
            ////
            //await FetchDataAsync(txtITUserID.Text, txtITPassword.Text);
            ////
            this.Cursor = Cursors.WaitCursor;
            //
            prgFetchITBar.Value = 0; // start at 0%
            elapsedTicks = 0;
            fetchCompleted = false;
            //
            //tmrFetchITPortal.Interval = 10000; // 10 seconds
            //tmrFetchITPortal.Tick += TmrFetchITPortal_Tick;
            //tmrFetchITPortal.Start();
            tmrFetchITPortal.Interval = 1000; // 1 second updates
            tmrFetchITPortal.Tick -= TmrFetchITPortal_Tick;
            tmrFetchITPortal.Tick += TmrFetchITPortal_Tick;
            tmrFetchITPortal.Start();
            //
            //await FetchDataAsync(txtITUserID.Text, txtITPassword.Text);
            //Run FetchDataAsync without blocking UI
            await Task.Run(async () =>
            {
                await FetchDataAsync(txtITUserID.Text.Trim(), txtITPassword.Text.Trim());
                fetchCompleted = true; // mark completion
            });
            prgFetchITBar.Value = 100;
            tmrFetchITPortal.Stop();
            //
            this.Cursor = Cursors.Default;
            //
        }
        #endregion

        #region BtnITClose_Click
        private void BtnITClose_Click(object sender, EventArgs e)
        {
            //
            grpITLogin.Visible = false;
            //
            grpCompanyName.Enabled = true;
            grpBasicInformation.Enabled = true;
            grpAddress.Enabled = true;
            grpResponsiblePersonDetails.Enabled = true;
            grpResponsiblePerson.Enabled = true;
            grpInactiveTAN.Enabled = true;
            grpGovtDeductors.Enabled = true;
            grpCITDetails.Enabled = true;
        }
        #endregion


        #region BtnFetchITPortal_Click
        private void BtnFetchITPortal_Click(object sender, EventArgs e)
        {
            grpCompanyName.Enabled = false;
            grpBasicInformation.Enabled = false;
            grpAddress.Enabled = false;
            grpResponsiblePersonDetails.Enabled = false;
            grpResponsiblePerson.Enabled = false;
            grpInactiveTAN.Enabled = false;
            grpGovtDeductors.Enabled = false;
            grpCITDetails.Enabled = false;
            //
            grpITLogin.Location = new Point(26, 40);
            grpITLogin.Visible = true;
            //
            txtITUserID.Text = string.Empty;
            txtITUserID.Select();
            txtITPassword.Text = string.Empty;
            //
            prgFetchITBar.Value = 0;// prgFetchITBar.Value = 0; // start at 0%
            elapsedTicks = 0;
            //
            //
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtITPassword.UseSystemPasswordChar = true;
            }
            //--
        }
        #endregion


        #region TmrFetchITPortal_Tick
        private void TmrFetchITPortal_Tick(object sender, EventArgs e)
        {
            if (fetchCompleted)
            {
                prgFetchITBar.Value = 100;
                tmrFetchITPortal.Stop();
                return;
            }
            if (tmrFetchITPortal.Interval == 1000)
                tmrFetchITPortal.Interval = 10000;
            elapsedTicks++;
            // Increase progress by 10% each tick
            int progress = elapsedTicks * 10;
            if (progress > 100) progress = 100;
            //
            prgFetchITBar.Value = progress;
            // Stop timer after reaching 100%
            if (progress >= 100)
            {
                tmrFetchITPortal.Stop();
                //MessageBox.Show("Process Completed!");
            }
        }
        #endregion

        private void TxtCountry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
    }
}

