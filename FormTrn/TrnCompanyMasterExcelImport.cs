
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnFVUImport
Version			: 1.0
Start Date		: 30-12-2010
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
    using System.IO;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Net;

    //using Microsoft.Office.Interop.Access;

    //using System.Runtime.InteropServices;
    using System.Data.OleDb;
    //~~~~ User Namespaces ~~~~
    //using TDSMAN.FormTrn;
    using TDSMAN.FormRpt;
    using TDSMAN.Classes;
    //--
    using Excel = Microsoft.Office.Interop.Excel.Worksheet;
    //~~~~ This namespace are using for using VB6 component
    using Microsoft.VisualBasic.Compatibility.VB6;

#endregion



namespace TDSMAN.FormTrn
{
    #region STRUCTURE

    #region T_Error_Type
    //public struct T_Deductee_Master_Error_Type
    //{
    //    public const string BLANK_NULL_CHECK = "Blank/NULL value";
    //    public const string MANADATORY_CHECK = "Manadatory value";
    //    public const string NUMERIC_CHECK    = "should be Numeric";
    //    public const string DUPLICATE_CHECK  = "Duplicate Value not permitted";
    //    public const string LENGTH_CHECK     = "Length Check";
    //    public const string FORMAT_CHECK     = "Format Check";
    //    public const string MISMATCH_CHECK   = "Data not matched";
    //    public const string SEQUENCE_CHECK   = "Not in Sequence";
    //    public const string VALIDITY_CHECK   = "Invalid value";
    //    //
    //    public const string MISC = "MISC";
    //}
    #endregion

    #region T_Error_Type_Color
    //public struct T_Deductee_Master_Error_Type_Color
    //{
    //    public const string BLANK_NULL_CHECK = "Red";
    //    public const string MANADATORY_CHECK = "Salmon";
    //    public const string NUMERIC_CHECK = "SpringGreen";
    //    public const string DUPLICATE_CHECK = "SteelBlue";
    //    public const string LENGTH_CHECK = "Tan";
    //    public const string FORMAT_CHECK = "Tomato";
    //    public const string MISMATCH_CHECK = "Yellow";
    //    public const string SEQUENCE_CHECK = "Beige";
    //    public const string VALIDITY_CHECK = "Chocolate";
    //    //
    //    public const string MISC = "Fuchsia";
    //}
    #endregion

    #region T_Company_Master_Sheet_Name
    public struct T_Company_Master_Sheet_Name
    {
        public const string VALIDATION_ERROR_DETAILS = "Validation Error Details";
        public const string COMPANY_MASTER = "Company Master";
    }
    #endregion

    #region T_Deductee_Master_SaveTag
    //public struct T_Deductee_Master_SaveTag
    //{
    //    public const string IMPORT = "IMPORT";
    //    public const string VALIDATE = "VALIDATE";
    //}
    #endregion

    #region T_Deductee_Master_Updt_Tag
    //public struct T_Deductee_Master_Updt_Tag
    //{
    //    public const string NEW = "New";
    //    public const string UPDATE = "Existing";
    //}
    #endregion

    #endregion      

    public partial class TrnCompanyMasterExcelImport : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnCompanyMasterExcelImport()
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
        ExcelService ExcelService = new ExcelService();
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
        //Microsoft.Office.Interop.Access.Application Access = new Microsoft.Office.Interop.Access.Application();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        //
        int intCaratPosition = 0;
        string strFVUPath = "";
        string strCheckCompatibilityMessage = "";
        long lngBasicInfoID = 0;
        int intLoop = 0;
        string strErrorWorksheetName = "Validation Error";

        int intTotalErrors;

        //-----------------------------------------------------------------------
        string strTempCompanyMasterPath = "";
        //
        bool blnOpenTabPage = false;

        #endregion

        #region set ENUM

        #region T_COMPANY_MASTER_COLUMN

        public enum T_COMPANY_MASTER_COLUMN
        {
            COMPANY_ID = 0,
            COMPANY_NAME = 1,
            COMPANY_NAME_CELL = 2,
            COMPANY_TAN = 3,
            COMPANY_TAN_CELL = 4,
            COMPANY_PAN = 5,
            COMPANY_PAN_CELL = 6,
            BRANCH = 7,
            BRANCH_CELL = 8,
            DED_TYPE = 9,
            DED_TYPE_CELL =10,
            TAN_REG =11,
            TAN_REG_CELL =12,
            FLT_DOOR =13,
            FLT_DOOR_CELL = 14,
            BLDG =15,
            BLDG_CELL =16,
            RD_STRT =17,
            RD_STRT_CELL = 18,
            AREA =19,
            AREA_CELL = 20,
            TOWN = 21,
            TOWN_CELL = 22,
            PIN = 23,
            PIN_CELL = 24,
            STATE = 25,
            STATE_CELL = 26,
            STD = 27,
            STD_CELL = 28,
            PHONE = 29,
            PHONE_CELL = 30,
            EMAIL = 31,
            EMAIL_CELL = 32,
            RP_NAME = 33,
            RP_NAME_CELL = 34,
            DESIG = 35,
            DESIG_CELL = 36,
            FATHER_NAME = 37,
            FATHER_NAME_CELL = 38,
            MOBILE =39,
            MOBILE_CELL = 40,
            RP_FLT_DOOR = 41,
            RP_FLT_DOOR_CELL = 42,
            RP_BLDG = 43,
            RP_BLDG_CELL = 44,
            RP_RD_STRT = 45,
            RP_RD_STRT_CELL = 46,
            RP_AREA = 47,
            RP_AREA_CELL = 48,
            RP_TOWN = 49,
            RP_TOWN_CELL = 50,
            RP_PIN = 51,
            RP_PIN_CELL = 52,
            RP_STATE = 53,
            RP_STATE_CELL = 54,
            RP_STD = 55,
            RP_STD_CELL = 56,
            RP_PHONE = 57,
            RP_PHONE_CELL = 58,
            RP_EMAIL = 59,
            RP_EMAIL_CELL = 60,
            PAO =61,
            PAO_CELL =62,
            PAO_REG =63,
            PAO_REG_CELL = 64,
            DDO = 65,
            DDO_CELL = 66,
            DDO_REG =67,
            DDO_REG_CELL =68,
            MIN_STATE =69,
            MIN_STATE_CELL = 70,
            MIN =71,
            MIN_CELL =72,
            OTH_MIN =73,
            OTH_MIN_CELL =74,
            AOI =75,
            AOI_CELL =75
        }
        #endregion

        #region T_GET_COMPANY_MASTER_DATA_FROM_EXCEL

        public enum T_GET_COMPANY_MASTER_DATA_FROM_EXCEL
        {
            COMPANY_NAME = 0,
            COMPANY_TAN = 1,
            COMPANY_PAN = 2,
            BRANCH = 3,
            DED_TYPE = 4,
            TAN_REG = 5,
            FLT_DOOR = 6,
            BLDG = 7,
            RD_STRT = 8,
            AREA = 9,
            TOWN = 10,
            PIN = 11,
            STATE = 12,
            STD = 13,
            PHONE = 14,
            EMAIL = 15,
            RP_NAME = 16,
            DESIG = 17,
            FATHER_NAME = 18,
            MOBILE = 19,
            RP_FLT_DOOR = 20,
            RP_BLDG = 21,
            RP_RD_STRT = 22,
            RP_AREA = 23,
            RP_TOWN = 24,
            RP_PIN = 25,
            RP_STATE = 26,
            RP_STD = 27,
            RP_PHONE = 28,
            RP_EMAIL = 29,
            PAO = 30,
            PAO_REG = 31,
            DDO = 32,
            DDO_REG = 33,
            MIN_STATE = 34,
            MIN = 35,
            OTH_MIN = 36,
            AOI = 37,
            RP_PAN = 38
        }

        #endregion


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

        #region TrnExcelImport_Load

        private void TrnExcelImport_Load(object sender, EventArgs e)
        {
            //
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            GC.Collect();
            //
            //Added by Indrajit on 26-02-2013
            tmrLoginRefresh.Interval = (int)TDSMAN.Classes.TDSMAN.T_pLockInterval * 60000;
            tmrLoginRefresh.Start();
            //-----
            //-----------
            lblTitle.Text = "Company Master Excel Import";
            //
            lblMessage.Text = "Please Note : - ";
            lblMessage1.Text = "1. Only unique Company Name && TAN should be provided for import.";
            //lblMessage2.Text = "2. For deductees with PANNOTAVBL unique name should be provided.";
            //
            BtnSave.Tag = T_SaveTag.VALIDATE;
            //    
        }

        #endregion

        #region btnSelectExcelPath_Click
        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                strFVUPath = cmnService.J_OpenFileDialog("Excel File | *.xlsx", "Excel File | *.xlsx", "Choose the Excel File to import");
            else
                strFVUPath = cmnService.J_OpenFileDialog("Excel File | *.xls; *.xlsx", "Excel File | *.xls; *.xlsx", "Choose the Excel File to import");
            //--
            if (strFVUPath != "")
                txtExcelPath.Text = strFVUPath;            
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

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            if (Convert.ToString(BtnSave.Tag) == T_SaveTag.VALIDATE)
            {
                //--
                if (ValidateFields() == false) return;
                //--
                if (cmnService.J_UserMessage("Proceed Company Master Excel Import??", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                //--                
                this.Cursor = Cursors.WaitCursor;
                //--
                if(CheckExcelStructure(txtExcelPath.Text) == false)
                {
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("Selected Excel file is invalid");
                    prgBar.Value = 0;
                    btnSelectExcelPath.Select();
                    return;
                }
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //
                // CREATE TEMP TABLES 
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    //MessageBox.Show("1");
                    if (CREATE_TEMP_TABLES_SQL() == false)
                    {
                        cmnService.J_UserMessage("Temporary Tables Not created", MessageBoxIcon.Exclamation);
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                }
                //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                //{
                //    //if (CREATE_TEMP_TABLES() == false)
                //    //{
                //    //    cmnService.J_UserMessage("Temporary Tables Not created", MessageBoxIcon.Exclamation);
                //    //    this.Cursor = Cursors.Default;
                //    //    prgBar.Value = 0;
                //    //    return;
                //    //}
                //}
                lblProgressDisplayMessage.Visible = true;
                lblProgressDisplayMessage.Text = "Process Started";
                //--
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
                {
                    //
                    lblProgressDisplayMessage.Visible = true;
                    lblProgressDisplayMessage.Text = "EXCEL file initialized";
                    //
                    //if (INITIALIZE_COLOR_ERROR_CELLS(txtExcelPath.Text) == false)
                    //{
                    //    cmnService.J_UserMessage("Initialize Coloring Error Cells failed");
                    //    this.Cursor = Cursors.Default;
                    //    prgBar.Value = 0;
                    //    return;
                    //}
                }
                //
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //// CREATE TEMP TABLES        
                //if (CREATE_TEMP_TABLES() == false)
                //{
                //    cmnService.J_UserMessage("Temporary Tables Not created");
                //    this.Cursor = Cursors.Default;
                //    prgBar.Value = 0;
                //    return;
                //}
                //prgBar.Value = prgBar.Value + 5;
                //this.Refresh();
                // GET DATA FROM EXCEL ACCESS       
                //
                lblProgressDisplayMessage.Visible = true;
                lblProgressDisplayMessage.Text = "Transferring Data";
                //
                if (GET_DATA_FROM_EXCEL_ACCESS() == false)
                {
                    cmnService.J_UserMessage("Excel data import failed");
                    this.Cursor = Cursors.Default;
                    prgBar.Value = 0;
                    return;
                }
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();

                // VALIDATE DATA       
                //
                lblProgressDisplayMessage.Visible = true;
                lblProgressDisplayMessage.Text = "Validation Started";
                //
                if (VALIDATE_DATA() == false)
                {
                    cmnService.J_UserMessage("Data Validation failed");
                    this.Cursor = Cursors.Default;
                    prgBar.Value = 0;
                    return;
                }
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //--
                if (DELETE_WORKSHEET(txtExcelPath.Text) == false)
                {
                    cmnService.J_UserMessage("Error Sheet Deletion failed");
                    this.Cursor = Cursors.Default;
                    prgBar.Value = 0;
                    return;
                }

                if (CREATE_NEW_WORKSHEET(txtExcelPath.Text) == false)
                {
                    cmnService.J_UserMessage("Error Sheet Creation failed");
                    this.Cursor = Cursors.Default;
                    prgBar.Value = 0;
                    return;
                }

                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //
                lblProgressDisplayMessage.Visible = true;
                lblProgressDisplayMessage.Text = "New Worksheet created";
                //
                //--
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //
                intTotalErrors = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""));
                //                
                if (intTotalErrors > 0)
                {
                    if (WRITE_ERROR_WORKSHEET(txtExcelPath.Text) == false)
                    {
                        cmnService.J_UserMessage("Writing Error Sheet failed");
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                    prgBar.Value = prgBar.Value + 5;
                    this.Refresh();

                    //--
                    if (chkColorCodingExcelsheet.Checked == true)
                    {
                        //
                        lblProgressDisplayMessage.Visible = true;
                        lblProgressDisplayMessage.Text = "EXCEL file coloring";
                        //
                        if (COLOR_ERROR_CELLS(txtExcelPath.Text) == false)
                        {
                            cmnService.J_UserMessage("Coloring Error Cells failed");
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                    }
                    //-- DELETE ALL .tmp FILES
                    Delete_TMP_Files(txtExcelPath.Text);
                    //
                    lblProgressDisplayMessage.Visible = false;
                    //
                    for (int i = prgBar.Minimum; i <= prgBar.Maximum; i++)
                    {
                        prgBar.PerformStep();
                    }
                    this.Cursor = Cursors.Default;
                    //cmnService.J_UserMessage("Excel File Validation failed \n Check the <Validation Error> Sheet of the Excel file ", MessageBoxIcon.Exclamation);
                    if (cmnService.J_UserMessage("Excel File Validation failed \n Do you want to open the Excel file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        if (File.Exists(txtExcelPath.Text) == true)
                        {
                            System.Diagnostics.Process.Start(txtExcelPath.Text);
                        }
                    }
                    //
                    prgBar.Value = 0;
                    return;
                }
                else
                {
                    //if (UPDT_MODE() == false)
                    //{
                    //    cmnService.J_UserMessage("Updating failed");
                    //    this.Cursor = Cursors.Default;
                    //    prgBar.Value = 0;
                    //    return;
                    //}

                    for (int i = prgBar.Minimum; i <= prgBar.Maximum; i++)
                    {
                        prgBar.PerformStep();
                    }
                    this.Cursor = Cursors.Default;

                    if (DELETE_WORKSHEET(txtExcelPath.Text) == false) this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("Excel File Validation is completed \n     Proceed to Import ", MessageBoxIcon.Information);
                }
                //-- DELETE ALL .tmp FILES
                Delete_TMP_Files(txtExcelPath.Text);
                lblProgressDisplayMessage.Visible = false;
                //--
                //if (LOAD_IMPORT_INTERFACE() == false)
                //{
                //    cmnService.J_UserMessage("Loading Import Interface failed");
                //    this.Cursor = Cursors.Default;
                //    prgBar.Value = 0;
                //    return;
                //}
                //-
                blnOpenTabPage = true;
                tbcExcelImport.TabPages.Remove(tbpValidateExcelFile);
                //-- NEW RECORDS
                lblNewRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ""));
                //--                    
                LoadCompanyMasterGrid();
                //--
                BtnSave.Text = "Import Data";
                BtnSave.Tag = T_SaveTag.IMPORT;
                lblTitle.Text = "Excel Import";
                //                               
            }
            else if (Convert.ToString(BtnSave.Tag) == T_SaveTag.IMPORT)
            {
                //--
                //strSQL = "SELECT CHALLAN_DETAILS_ID " +
                //         "FROM   TEMP_CHALLAN_DETAILS " +
                //         "WHERE  (CDBL(TEMP_CHALLAN_DETAILS.TOTAL_TAX_DEPOSITED) - " +
                //         "       TEMP_CHALLAN_DETAILS.CTRL_TOT_TAX) < 0 ";
                //if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                //{
                //    cmnService.J_UserMessage("Import not possible");
                //    return;
                //}
                //--
                if (cmnService.J_UserMessage("Proceed Import??", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                //--
                prgImportBar.Value = 0;
                //--
                this.Cursor = Cursors.WaitCursor;
                //--                
                prgImportBar.Value = prgImportBar.Value + 5;
                this.Refresh();
                //--
                //--
                strSQL = "SELECT COUNT(COMPANY_ID)  AS MAX_COMPANY_COUNT FROM MST_COMPANY";
                //if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > TDSMAN.Classes.TDSMAN.T_pMaxCompanyCount)
                if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) ==0)
                {
                    strSQL = "SELECT COUNT(COMPANY_NAME) AS MAX_COMPANY_COUNT FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY;
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
                    }
                }
                //--
                if(INSERT_COMPANY_DATA() == false) return;
                //--
                //
                //if (DROP_TEMP_TABLES() == false) return;
                DROP_TEMP_TABLES();
                //--
                for (int i = prgImportBar.Minimum; i <= prgImportBar.Maximum; i++)
                {
                    prgImportBar.PerformStep();
                }
                this.Cursor = Cursors.Default;
                //--
                BtnSave.Enabled = false;
                //
                cmnService.J_UserMessage("Import from Excel File completed", MessageBoxIcon.Information);
                prgImportBar.Value = 0;
                //--
                GC.Collect();
                //
                dmlService.Dispose();
                this.Close();
                this.Dispose();
            }
            //------------------------
        }
        #endregion

        //Added by Indrajit on 26-02-2013
        #region TrnDeducteeMasterExcelImport_FormClosing
        private void TrnDeducteeMasterExcelImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TdsMan.FreeBasicInfoEntry() == false)
                return;
        }
        #endregion

        #region tmrLoginRefresh_Tick
        private void tmrLoginRefresh_Tick(object sender, EventArgs e)
        {
            ////if (TDSMAN.Classes.TDSMAN.T_pBasicInfoId > 0)
            ////{
            //    strSQL = "UPDATE TEMP_STACK_BASIC_INFO " +
            //             "SET LAST_UPDATED_TIME   = " + TdsMan.GetServerDateTime() + " " +
            //             "WHERE BASIC_INFO_ID     = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + " " +
            //             "AND   PRINT_USER_SERIAL = '" + TDSMAN.Classes.TDSMAN.T_pProductSerial + "'";
            //    dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            ////}
        }
        #endregion


        #region tbcExcelImport_Selecting
        private void tbcExcelImport_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.Action == TabControlAction.Selecting)
            {
                if (blnOpenTabPage == false)
                {
                    e.Cancel = true;
                }
                else
                    blnOpenTabPage = false;
            }
        }
        #endregion


        #region dgcViewChallan_Click
        private void dgcViewChallan_Click(object sender, EventArgs e)
        {
            dgcViewChallan.Select(dgcViewChallan.CurrentRowIndex);
            dgcViewChallan.Select();
            dgcViewChallan.Focus();
        }
        #endregion

        #region dgcViewChallan_MouseClick
        private void dgcViewChallan_MouseClick(object sender, MouseEventArgs e)
        {
            dgcViewChallan_Click(sender, e);
        }
        #endregion



        #endregion

        #region User Defined Functions

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (lblSearchMode.Text == J_Mode.Sorting)
                {
                   return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    return true;
                }
                else
                {
                    //-----------------------------------------------------------------------
                    //-- EXCEL FILE SELECTED
                    //-----------------------------------------------------------------------
                    if (txtExcelPath.Text.Trim()== "")
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
                    //--
                    ////strSQL = "SELECT COUNT(COMPANY_ID) + 1 AS MAX_COMPANY_COUNT FROM MST_COMPANY";
                    ////if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > TDSMAN.Classes.TDSMAN.T_pMaxCompanyCount)
                    ////{
                    ////    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                    ////    {
                    ////        TrnTrialMessageBox TrialMessageBox = new TrnTrialMessageBox();
                    ////        TrialMessageBox.StartPosition = FormStartPosition.CenterScreen;
                    ////        TrialMessageBox.lblMessage2.Text = "Trial version supports only One Company.";
                    ////        TrialMessageBox.Show();
                    ////        return false;
                    ////    }
                    ////}
                    //--
                    return true;
                }
                //return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region CREATE_TEMP_TABLES
        private bool CREATE_TEMP_TABLES_SQL()
        {
            try
            {
                //Blocked by INDRAJIT on 16-03-2012
                //if (dmlService.J_IsDatabaseObjectExist("TEMP_MST_DEDUCTEE") == false)
                //{
                //    strSQL = "CREATE TABLE TEMP_MST_DEDUCTEE (" +
                //         "                  DEDUCTEE_ID        COUNTER," +
                //         "                  MODE               TEXT(10) DEFAULT \"\"," +
                //         "                  DEDUCTEE_NAME      TEXT(255) DEFAULT \"\"," +
                //         "                  DEDUCTEE_NAME_CELL TEXT(10)  DEFAULT \"\"," +
                //         "                  DEDUCTEE_PAN       TEXT(255) DEFAULT \"\"," +
                //         "                  DEDUCTEE_PAN_CELL  TEXT(10)  DEFAULT \"\"," +
                //         "                  DEDUCTEE_CODE      TEXT(255) DEFAULT \"\"," +
                //         "                  DEDUCTEE_CODE_CELL TEXT(10)  DEFAULT \"\"," +
                //         "                  ADDRESS1           TEXT(255) DEFAULT \"\"," +
                //         "                  ADDRESS1_CELL      TEXT(10)  DEFAULT \"\"," +
                //         "                  ADDRESS2           TEXT(255) DEFAULT \"\"," +
                //         "                  ADDRESS2_CELL      TEXT(10)  DEFAULT \"\"," +
                //         "                  ADDRESS3           TEXT(255) DEFAULT \"\"," +
                //         "                  ADDRESS3_CELL      TEXT(10)  DEFAULT \"\"," +
                //         "                  ADDRESS4           TEXT(255) DEFAULT \"\"," +
                //         "                  ADDRESS4_CELL      TEXT(10)  DEFAULT \"\"," +
                //         "                  ADDRESS5           TEXT(255) DEFAULT \"\"," +
                //         "                  ADDRESS5_CELL      TEXT(10)  DEFAULT \"\"," +
                //         "                  STATE              TEXT(255) DEFAULT \"\"," +
                //         "                  STATE_CELL         TEXT(10)  DEFAULT \"\"," +
                //         "                  PIN                TEXT(255) DEFAULT \"\"," +
                //         "                  PIN_CELL           TEXT(10)  DEFAULT \"\"," +
                //         "                  MOBILE             TEXT(255) DEFAULT \"\"," +
                //         "                  MOBILE_CELL        TEXT(10)  DEFAULT \"\"," +
                //         "                  EMAIL              TEXT(255) DEFAULT \"\"," +
                //         "                  EMAIL_CELL         TEXT(10)  DEFAULT \"\"," +
                //         "                  STATE_ID           NUMBER    DEFAULT 0)";
                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}
                //
                //strSQL = "DELETE FROM TEMP_MST_DEDUCTEE";
                //if (dmlService.J_ExecSql(strSQL) == false)
                //    return false;
                //
                #region T_tblTEMP_CHALLAN_DETAILS
                dmlService.J_BeginTransaction();
                //MessageBox.Show("2");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "") == true)
                {
                    //MessageBox.Show("2.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //MessageBox.Show("3");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "") == false)
                {
                    //MessageBox.Show("3.1");
                    strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + @" (
                                            " + cmnService.J_GetDataType("COMPANY_ID", J_Identity.YES) + @",
                                            " + cmnService.J_GetDataType("MODE", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("COMPANY_NAME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("COMPANY_NAME_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("COMPANY_TAN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("COMPANY_TAN_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("COMPANY_PAN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("COMPANY_PAN_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("BRANCH", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("BRANCH_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("DED_TYPE", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("DED_TYPE_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("TAN_REG", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("TAN_REG_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("FLT_DOOR", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("FLT_DOOR_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("BLDG", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("BLDG_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RD_STRT", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("RD_STRT_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("AREA", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("AREA_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("TOWN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("TOWN_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("PIN", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("PIN_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("STATE", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("STATE_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("STD", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("STD_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("PHONE", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("PHONE_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("EMAIL", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("EMAIL_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RP_NAME", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("RP_NAME_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("DESIG", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("DESIG_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("FATHER_NAME", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("FATHER_NAME_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("MOBILE", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("MOBILE_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RP_FLT_DOOR", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("RP_FLT_DOOR_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RP_BLDG", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("RP_BLDG_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RP_RD_STRT", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("RP_RD_STRT_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RP_AREA", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("RP_AREA_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RP_TOWN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("RP_TOWN_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RP_PIN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("RP_PIN_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RP_STATE", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("RP_STATE_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RP_STD", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("RP_STD_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RP_PHONE", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("RP_PHONE_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("RP_EMAIL", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("RP_EMAIL_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("PAO", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("PAO_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("PAO_REG", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("PAO_REG_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("DDO", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("DDO_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("DDO_REG", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("DDO_REG_CELL", J_ColumnType.Char) + @",                                            
                                            " + cmnService.J_GetDataType("MIN_STATE", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("MIN_STATE_CELL", J_ColumnType.Char) + @",                                           
                                            " + cmnService.J_GetDataType("MINISTRY", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("MINISTRY_CELL", J_ColumnType.Char) + @",                                           
                                            " + cmnService.J_GetDataType("OTH_MIN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("OTH_MIN_CELL", J_ColumnType.Char) + @",                                           
                                            " + cmnService.J_GetDataType("AOI", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("AOI_CELL", J_ColumnType.Char) + @",                                           
                                            " + cmnService.J_GetDataType("DED_TYPE_ID", J_ColumnType.Integer) + @",                                    
                                            " + cmnService.J_GetDataType("STATE_ID", J_ColumnType.Integer) + @",                                    
                                            " + cmnService.J_GetDataType("RP_STATE_ID", J_ColumnType.Integer) + @",                                    
                                            " + cmnService.J_GetDataType("MIN_STATE_ID", J_ColumnType.Integer) + @",                                
                                            " + cmnService.J_GetDataType("MINISTRY_ID", J_ColumnType.Integer) + @",
                                            " + cmnService.J_GetDataType("RP_PAN", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("RP_PAN_CELL", J_ColumnType.Char) + @")";
                    //MessageBox.Show(strSQL);
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                #endregion

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
                //if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == false)
                //{
                //    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                //         "                  ERR_VALIDATION_ID COUNTER," +
                //         "                  ERR_TYPE          TEXT(25) DEFAULT \"\"," +
                //         "                  ERR_CELL          TEXT(10) DEFAULT \"\"," +
                //         "                  ERR_COLUMN        TEXT(50) DEFAULT \"\"," +
                //         "                  ERR_SHEET         TEXT(25) DEFAULT \"\"," +
                //         "                  ERR_COLOR         TEXT(25) DEFAULT \"\"," +
                //         "                  ERR_DESC          TEXT(255) DEFAULT \"\"," +
                //         "                  ERR_FORM_NO       TEXT(5) DEFAULT \"\")";
                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}
                ////
                //strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                //if (dmlService.J_ExecSql(strSQL) == false)
                //    return false;

                //return true;
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
                         "                  " + cmnService.J_GetDataType("ERR_VALIDATION_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_TYPE", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_CELL", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLUMN", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_SHEET", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLOR", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_DESC", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_FORM_NO", J_ColumnType.String, 255) + ")";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                dmlService.J_ExecSql(strSQL);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DROP_TEMP_TABLES
        private bool DROP_TEMP_TABLES()
        {
            try
            {
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "";
                    if(dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //

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
                    string strCompanyName = "";
                    string strTAN = "";
                    string strPAN = "";
                    string strDeductorType = "";
                    string strBranch = "";
                    string strTANRegNo = "";
                    string strFltDoor = "";
                    string strBldg = "";
                    string strRdStrt = "";
                    string strRdArea = "";
                    string strTown = "";
                    string strPIN = "";
                    string strState = "";
                    string strSTD = "";
                    string strPhone = "";
                    string strEmail = "";
                    string strRPName = "";
                    string strDesig = "";
                    string strFatherName = "";
                    string strMobile = "";
                    string strRPFltDoor = "";
                    string strRPBldg = "";
                    string strRPRdStrt = "";
                    string strRPRdArea = "";
                    string strRPTown = "";
                    string strRPPIN = "";
                    string strRPState = "";
                    string strRPSTD = "";
                    string strRPPhone = "";
                    string strRPEmail = "";
                    string strPAOCode = "";
                    string strPAORegNo = "";
                    string strDDOCode = "";
                    string strDDORegNo = "";
                    string strMinState = "";
                    string strMinistry = "";
                    string strOtherMinistry = "";
                    string strAOINumber = "";
                    string strRPPAN = ""; // 2015/04/21
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                {
                    strTempCompanyMasterPath = Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_pProductSerial + "_CompanyMaster.txt");
                    if (File.Exists(strTempCompanyMasterPath) == true)
                        File.Delete(strTempCompanyMasterPath);

                    //strTempCompanyMasterPath = Path.Combine(Application.StartupPath, "CompanyMaster.txt");
                }
                else
                    strTempCompanyMasterPath = Path.Combine(Application.StartupPath, "CompanyMaster.txt");

                    string tableName = "";
                    string textfileName = "";
                    
                #endregion
            
                #region INSERTING DATA TO TEXT FILE
                
                if (KILL_EXCEL() == false)
                    return false;
                //// READ EXCEL FILE
                DataSet myDataSet;
                OleDbDataAdapter myCommand;
                string strConnectionString = "";
                //
                if(Path.GetExtension(txtExcelPath.Text.Trim()) == ".xls")
                    strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                else if(Path.GetExtension(txtExcelPath.Text.Trim()) == ".xlsx")
                    strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\"";
                //
                OleDbConnection con = new OleDbConnection(strConnectionString);
                //  
                
                con.Open();
                //
                // INSERT DEDUCTEE MASTER
                //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                myDataSet = new DataSet();
                myCommand = new OleDbDataAdapter("SELECT * FROM [" + T_Company_Master_Sheet_Name.COMPANY_MASTER + "$]", con);
                myCommand.Fill(myDataSet);
                //con.Close();

                long lngRow = 2;
                long lngColumn = 0;

                int intLineNumber = 0;

                StreamWriter StreamWriterDeductee = cmnService.J_ReturnStreamWriter(strTempCompanyMasterPath);

                //Travers through each row in the dataset
                foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
                {
                    lblProgressDisplayMessage.Visible = true;
                    //lblProgressDisplayMessage.Text = "Transferring Data [" + T_Company_Master_Sheet_Name.DEDUCTEE_MASTER + " : " + lngRow + " ]";
                    //this.Refresh();
                    //////
                    //lngColumn =65;
                    ////Stores info in Datarow into an array
                    Object[] cells = myDataRow.ItemArray;

                    intLineNumber = intLineNumber + 1;
                    int intColumnValue = 64;
                    int intColumnValue1 = 64; // for AA after Z
                    //
                    strCompanyName = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.COMPANY_NAME]);
                    strTAN = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.COMPANY_TAN]);
                    strPAN = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.COMPANY_PAN]);
                    strBranch = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.BRANCH]);
                    strDeductorType = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.DED_TYPE]);
                    strTANRegNo = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.TAN_REG]);
                    strFltDoor = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.FLT_DOOR]);
                    strBldg = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.BLDG]);
                    strRdStrt = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RD_STRT]);
                    strRdArea = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.AREA]);
                    strTown = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.TOWN]);
                    strPIN = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.PIN]);
                    strState = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.STATE]);
                    strSTD = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.STD]);
                    strPhone = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.PHONE]);
                    strEmail = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.EMAIL]);
                    strRPName = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_NAME]);
                    strDesig = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.DESIG]);
                    strFatherName = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.FATHER_NAME]);
                    strMobile = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.MOBILE]);
                    strRPFltDoor = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_FLT_DOOR]);
                    strRPBldg = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_BLDG]);
                    strRPRdStrt = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_RD_STRT]);
                    strRPRdArea = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_AREA]);
                    strRPTown = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_TOWN]);
                    strRPPIN = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_PIN]);
                    strRPState = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_STATE]);
                    strRPSTD = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_STD]);
                    strRPPhone = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_PHONE]);
                    strRPEmail = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_EMAIL]);
                    strPAOCode = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.PAO]);
                    strPAORegNo = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.PAO_REG]);
                    strDDOCode = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.DDO]);
                    strDDORegNo = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.DDO_REG]);
                    strMinState = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.MIN_STATE]);
                    strMinistry = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.MIN]);
                    strOtherMinistry = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.OTH_MIN]);
                    strAOINumber = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.AOI]);
                    strRPPAN = Convert.ToString(cells[(int)T_GET_COMPANY_MASTER_DATA_FROM_EXCEL.RP_PAN]);
                    // CHECK BLANK ROW TO EXIT
                    if (strCompanyName == "" &&
                            strTAN == "" &&
                                strPAN == "" &&
                                    strDeductorType == "" &&
                                        strBranch == "" &&
                                            strTANRegNo == "" &&
                                                strFltDoor == "" &&
                                                    strBldg == "" &&
                                                        strRdStrt == "" &&
                                                            strRdArea == "" &&
                                                                strTown == "" &&
                                                                    strPIN == "" &&
                                                                        strState == "" &&
                                                                            strSTD == "" &&
                                                                                strPhone == "" &&
                                                                                    strEmail == "" &&
                                                                                        strRPName == "" &&
                                                                                            strDesig == "" &&
                                                                                                strFatherName == "" &&
                                                                                                    strMobile == "" &&
                                                                                                        strRPFltDoor == "" &&
                                                                                                            strRPBldg == "" &&
                                                                                                                strRPRdStrt == "" &&
                                                                                                                    strRPRdArea == "" &&
                                                                                                                        strRPTown == "" &&
                                                                                                                            strRPPIN == "" &&
                                                                                                                                strRPState == "" &&
                                                                                                                                    strRPSTD == "" &&
                                                                                                                                        strRPPhone == "" &&
                                                                                                                                            strRPEmail == "" &&
                                                                                                                                                strPAOCode == "" &&
                                                                                                                                                    strPAORegNo == "" &&
                                                                                                                                                        strDDOCode == "" &&
                                                                                                                                                            strDDORegNo == "" &&
                                                                                                                                                                strMinState == "" &&
                                                                                                                                                                    strMinistry == "" &&
                                                                                                                                                                        strOtherMinistry == "" &&
                                                                                                                                                                            strAOINumber == "" &&
                                                                                                                                                                               strRPPAN == "")
                        break;

                    cmnService.J_WriteLine(ref StreamWriterDeductee, TdsMan.T_WriteField(intLineNumber.ToString()) + TdsMan.T_WriteField("") +
                                                                     TdsMan.T_WriteField(strCompanyName) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 1)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strTAN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 2)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strPAN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 3)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strBranch) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 4)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strDeductorType) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 5)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strTANRegNo) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 6)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strFltDoor) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 7)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strBldg) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 8)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRdStrt) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 9)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRdArea) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 10)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strTown) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 11)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strPIN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 12)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strState) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 13)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strSTD) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 14)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strPhone) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 15)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strEmail) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 16)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRPName) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 17)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strDesig) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 18)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strFatherName) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 19)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strMobile) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 20)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRPFltDoor) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 21)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRPBldg) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 22)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRPRdStrt) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 23)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRPRdArea) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 24)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRPTown) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 25)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRPPIN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 26)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRPState) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 1)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRPSTD) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 2)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRPPhone) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 3)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strRPEmail) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 4)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strPAOCode) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 5)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strPAORegNo) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 6)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strDDOCode) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 7)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strDDORegNo) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 8)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strMinState) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 9)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strMinistry) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 10)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strOtherMinistry) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 11)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strAOINumber) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 12)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField("0") +
                                                                     TdsMan.T_WriteField("0") +
                                                                     TdsMan.T_WriteField("0") +
                                                                     TdsMan.T_WriteField("0") +
                                                                     TdsMan.T_WriteField("0") +
                                                                     TdsMan.T_WriteField(strRPPAN) + TdsMan.T_WriteField((Convert.ToChar(65).ToString() + Convert.ToString((char)(intColumnValue1 + 13)) + (Convert.ToString(intLineNumber + 1)))));
                }

                myDataSet.Dispose();
                myCommand.Dispose();

                StreamWriterDeductee.Flush();
                StreamWriterDeductee.Close();

                #endregion

                #region TRANSFERRING DATA FROM TEXT FILE TO ACCESS

                //Added by INDRAJIT on 16-03-2012
                
                //Array to hold data segment name
                tableName =  TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY ;
                textfileName = "CompanyMaster";

                ImportTextToTables(tableName, textfileName, strTempCompanyMasterPath, false);

                // DELETE THE TXT FILE
                if (File.Exists(strTempCompanyMasterPath) == true)
                    File.Delete(strTempCompanyMasterPath);

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
            string strFolderPath = cmnService.J_GetDirectoryName(strTempCompanyMasterPath);

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
            if (txtfile == "CompanyMaster")
            {
                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "
                StreamWriter.WriteLine(@"Col1=COMPANY_ID integer
                                        Col2=MODE char
                                        Col3=COMPANY_NAME char
                                        Col4=COMPANY_NAME_CELL char
                                        Col5=COMPANY_TAN char
                                        Col6=COMPANY_TAN_CELL char
                                        Col7=COMPANY_PAN char
                                        Col8=COMPANY_PAN_CELL char
                                        Col9=BRANCH char
                                        Col10=BRANCH_CELL char
                                        Col11=DED_TYPE char
                                        Col12=DED_TYPE_CELL char
                                        Col13=TAN_REG char
                                        Col14=TAN_REG_CELL char
                                        Col15=FLT_DOOR char
                                        Col16=FLT_DOOR_CELL char
                                        Col17=BLDG char
                                        Col18=BLDG_CELL char
                                        Col19=RD_STRT char
                                        Col20=RD_STRT_CELL char
                                        Col21=AREA char
                                        Col22=AREA_CELL char
                                        Col23=TOWN char
                                        Col24=TOWN_CELL char
                                        Col25=PIN char
                                        Col26=PIN_CELL char
                                        Col27=STATE char
                                        Col28=STATE_CELL char
                                        Col29=STD char
                                        Col30=STD_CELL char
                                        Col31=PHONE char
                                        Col32=PHONE_CELL char
                                        Col33=EMAIL char
                                        Col34=EMAIL_CELL char
                                        Col35=RP_NAME char
                                        Col36=RP_NAME_CELL char
                                        Col37=DESIG char
                                        Col38=DESIG_CELL char
                                        Col39=FATHER_NAME char
                                        Col40=FATHER_NAME_CELL char
                                        Col41=MOBILE char
                                        Col42=MOBILE_CELL char
                                        Col43=RP_FLT_DOOR char
                                        Col44=RP_FLT_DOOR_CELL char
                                        Col45=RP_BLDG char
                                        Col46=RP_BLDG_CELL char
                                        Col47=RP_RD_STRT char
                                        Col48=RP_RD_STRT_CELL char
                                        Col49=RP_AREA char
                                        Col50=RP_AREA_CELL char
                                        Col51=RP_TOWN char
                                        Col52=RP_TOWN_CELL char
                                        Col53=RP_PIN char
                                        Col54=RP_PIN_CELL char
                                        Col55=RP_STATE char
                                        Col56=RP_STATE_CELL char
                                        Col57=RP_STD char
                                        Col58=RP_STD_CELL char
                                        Col59=RP_PHONE char
                                        Col60=RP_PHONE_CELL char
                                        Col61=RP_EMAIL char
                                        Col62=RP_EMAIL_CELL char
                                        Col63=PAO char
                                        Col64=PAO_CELL char
                                        Col65=PAO_REG char
                                        Col66=PAO_REG_CELL char
                                        Col67=DDO char
                                        Col68=DDO_CELL char
                                        Col69=DDO_REG char
                                        Col70=DDO_REG_CELL char
                                        Col71=MIN_STATE char
                                        Col72=MIN_STATE_CELL char
                                        Col73=MINISTRY char
                                        Col74=MINISTRY_CELL char
                                        Col75=OTH_MIN char
                                        Col76=OTH_MIN_CELL char
                                        Col77=AOI char
                                        Col78=AOI_CELL char
                                        Col79=DED_TYPE_ID integer
                                        Col80=STATE_ID integer
                                        Col81=RP_STATE_ID integer
                                        Col82=MIN_STATE_ID integer
                                        Col83=MINISTRY_ID integer
                                        Col84=RP_PAN char
                                        Col85=RP_PAN_CELL char
                                        ");
                #endregion
            }

            StreamWriter.Close();
            //
            if(J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
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
                strSheetName = T_Company_Master_Sheet_Name.COMPANY_MASTER;

                #region COMPANY NAME

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "  SET COMPANY_NAME = '' WHERE COMPANY_NAME IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "  " +
                    "     WHERE  COMPANY_NAME = ''" +
                    "     AND    COMPANY_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "            COMPANY_NAME_CELL AS ERROR_CELL," +
                        "            'COMPANY_NAME_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                        "     WHERE  COMPANY_NAME = ''" +
                        "     AND    COMPANY_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                    "     WHERE  LEN(COMPANY_NAME) > 75" +
                    "     AND    COMPANY_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            COMPANY_NAME_CELL AS ERROR_CELL," +
                        "            'COMPANY_NAME_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                        "     WHERE  LEN(COMPANY_NAME) > 75" +
                        "     AND    COMPANY_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region COMPANY TAN
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET COMPANY_TAN = '' WHERE COMPANY_TAN IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                    "     WHERE  COMPANY_TAN = ''" +
                    "     AND    COMPANY_TAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             COMPANY_TAN_CELL AS ERROR_CELL," +
                        "             'COMPANY_TAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                        "     WHERE  COMPANY_TAN = ''" +
                        "     AND    COMPANY_TAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                    "     WHERE  LEN(COMPANY_TAN) <> 10" +
                    "     AND    COMPANY_TAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "             COMPANY_TAN_CELL AS ERROR_CELL," +
                        "             'COMPANY_TAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                        "     WHERE  LEN(COMPANY_TAN) <> 10" +
                        "     AND    COMPANY_TAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //PAN STRUCTURE CHECK
                strSQL = "SELECT COUNT(*)" +
                 "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                 "     WHERE  (ISNUMERIC(LEFT(COMPANY_TAN,1)) <> 0" +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,2,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,3,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,4,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,5,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,6,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,7,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,8,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,9,1)) = 0 " +
                 "     OR     ISNUMERIC(RIGHT(COMPANY_TAN,1)) = -1)" +
                 "     AND    COMPANY_TAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             COMPANY_TAN_CELL AS ERROR_CELL," +
                        "             'COMPANY_TAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                        "     WHERE  (ISNUMERIC(LEFT(COMPANY_TAN,1)) <> 0" +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,2,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,3,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,4,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,5,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,6,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,7,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,8,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_TAN,9,1)) = 0 " +
                        "     OR     ISNUMERIC(RIGHT(COMPANY_TAN,1)) = -1)" +
                        "     AND    COMPANY_TAN <> 'TANNOTAVBL'" +
                        "     AND    COMPANY_TAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // DUPLICATE CHECK
                // WHEN COMPANY_PAN <> 'PANNOTAVBL'
                strSQL = "SELECT SUM(COMPANY.NO_OF_ROWS) AS NO_OF_ROWS " +
                         "FROM  (SELECT COMPANY_TAN," +
                         "              COUNT(COMPANY_ID) AS NO_OF_ROWS " +
                         "       FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                         "       WHERE  COMPANY_TAN     <> 'TANNOTAVBL' " +
                         "       AND    COMPANY_TAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                         "       GROUP BY COMPANY_TAN " +
                         "       HAVING COUNT(COMPANY_TAN) > 1) AS COMPANY";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //
                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                        "             COMPANY_TAN_CELL AS ERROR_CELL," +
                        "             'COMPANY_TAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.DUPLICATE_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  COMPANY_TAN IN (SELECT COMPANY_TAN " +
                        "                             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                        "                             WHERE  COMPANY_TAN     <> 'TANNOTAVBL' " +
                        "                             AND    COMPANY_TAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                        "                             GROUP BY COMPANY_TAN " +
                        "                             HAVING COUNT(COMPANY_TAN) > 1)";
                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //###########################
                strSQL = @"SELECT COUNT (*) AS NO_OF_ROWS 
                           FROM   MST_COMPANY,
                                  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + @"
                           WHERE  MST_COMPANY.TAN_NO = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".COMPANY_TAN ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //
                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.DUPLICATE_EXCEL_DATABASE + "'," +
                        "             COMPANY_TAN_CELL AS ERROR_CELL," +
                        "             'COMPANY_TAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISC + "'" +
                        "     FROM   MST_COMPANY, " +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                        "     WHERE  MST_COMPANY.TAN_NO = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".COMPANY_TAN ";
                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //###########################
                #endregion

                #region COMPANY PAN

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET COMPANY_PAN = '' WHERE COMPANY_PAN IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  COMPANY_PAN = ''" +
                    "     AND    COMPANY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             COMPANY_PAN_CELL AS ERROR_CELL," +
                        "             'COMPANY_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  COMPANY_PAN = ''" +
                        "     AND    COMPANY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(COMPANY_PAN) <> 10" +
                    "     AND    COMPANY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "             COMPANY_PAN_CELL AS ERROR_CELL," +
                        "             'COMPANY_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(COMPANY_PAN) <> 10" +
                        "     AND    COMPANY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //PAN STRUCTURE CHECK
                strSQL = "SELECT COUNT(*)" +
                 "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                 "     WHERE  (ISNUMERIC(LEFT(COMPANY_PAN,1)) <> 0" +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,2,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,3,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,4,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,5,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,6,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,7,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,8,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,9,1)) = 0 " +
                 "     OR     ISNUMERIC(RIGHT(COMPANY_PAN,1)) = -1)" +
                 "     AND    COMPANY_PAN <> 'PANNOTREQD'" +
                 "     AND    COMPANY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             COMPANY_PAN_CELL AS ERROR_CELL," +
                        "             'COMPANY_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  (ISNUMERIC(LEFT(COMPANY_PAN,1)) <> 0" +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,2,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,3,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,4,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,5,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,6,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,7,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,8,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(COMPANY_PAN,9,1)) = 0 " +
                        "     OR     ISNUMERIC(RIGHT(COMPANY_PAN,1)) = -1)" +
                        "     AND    COMPANY_PAN <> 'PANNOTREQD'" +
                        "     AND    COMPANY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region BRANCH

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET BRANCH = '' WHERE BRANCH IS NULL";
                dmlService.J_ExecSql(strSQL);

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(BRANCH) > 12" +
                    "     AND    BRANCH <> '' " +
                    "     AND    BRANCH_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            BRANCH_CELL AS ERROR_CELL," +
                        "            'BRANCH_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(BRANCH) > 12" +
                        "     AND    BRANCH <> '' " +
                        "     AND    BRANCH_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region DEDUCTOR TYPE

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET DED_TYPE = '' WHERE DED_TYPE IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK

                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  DED_TYPE = '' " +
                    "     AND    DED_TYPE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             DED_TYPE_CELL AS ERROR_CELL," +
                        "             'DED_TYPE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  DED_TYPE = ''" +
                        "     AND    DED_TYPE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                // VALIDITY CHECK

                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  DED_TYPE <> 'A'" +
                    "     AND    DED_TYPE <> 'B'" +
                    "     AND    DED_TYPE <> 'D'" +
                    "     AND    DED_TYPE <> 'E'" +
                    "     AND    DED_TYPE <> 'F'" +
                    "     AND    DED_TYPE <> 'G'" +
                    "     AND    DED_TYPE <> 'H'" +
                    "     AND    DED_TYPE <> 'J'" +
                    "     AND    DED_TYPE <> 'K'" +
                    "     AND    DED_TYPE <> 'L'" +
                    "     AND    DED_TYPE <> 'M'" +
                    "     AND    DED_TYPE <> 'N'" +
                    "     AND    DED_TYPE <> 'P'" +
                    "     AND    DED_TYPE <> 'Q'" +
                    "     AND    DED_TYPE <> 'S'" +
                    "     AND    DED_TYPE <> 'T'" +
                    "     AND    DED_TYPE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             DED_TYPE_CELL AS ERROR_CELL," +
                        "             'DED_TYPE_CODE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  DED_TYPE <> 'A'" +
                        "     AND    DED_TYPE <> 'B'" +
                        "     AND    DED_TYPE <> 'D'" +
                        "     AND    DED_TYPE <> 'E'" +
                        "     AND    DED_TYPE <> 'F'" +
                        "     AND    DED_TYPE <> 'G'" +
                        "     AND    DED_TYPE <> 'H'" +
                        "     AND    DED_TYPE <> 'J'" +
                        "     AND    DED_TYPE <> 'K'" +
                        "     AND    DED_TYPE <> 'L'" +
                        "     AND    DED_TYPE <> 'M'" +
                        "     AND    DED_TYPE <> 'N'" +
                        "     AND    DED_TYPE <> 'P'" +
                        "     AND    DED_TYPE <> 'Q'" +
                        "     AND    DED_TYPE <> 'S'" +
                        "     AND    DED_TYPE <> 'T'" +
                        "     AND    DED_TYPE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region TAN REG NO

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET TAN_REG = '' WHERE TAN_REG IS NULL";
                dmlService.J_ExecSql(strSQL);

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(TAN_REG) > 12" +
                    "     AND    TAN_REG <> '' " +
                    "     AND    TAN_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            TAN_REG_CELL AS ERROR_CELL," +
                        "            'TAN_REG_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(TAN_REG) > 12" +
                        "     AND    TAN_REG <> '' " +
                        "     AND    TAN_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region FLT/DR/BLCK NO.

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET FLT_DOOR = '' WHERE FLT_DOOR IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  FLT_DOOR = '' " +
                    "     AND    FLT_DOOR_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             FLT_DOOR_CELL AS ERROR_CELL," +
                        "             'FLT_DOOR_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  FLT_DOOR = ''" +
                        "     AND    FLT_DOOR_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(FLT_DOOR) > 25" +
                    "     AND    FLT_DOOR <> '' " +
                    "     AND    FLT_DOOR_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            FLT_DOOR_CELL AS ERROR_CELL," +
                        "            'FLT_DOOR_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(FLT_DOOR) > 25" +
                        "     AND    FLT_DOOR <> '' " +
                        "     AND    FLT_DOOR_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region BLDG

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET BLDG = '' WHERE BLDG IS NULL";
                dmlService.J_ExecSql(strSQL);

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(BLDG) > 25" +
                    "     AND    BLDG <> '' " +
                    "     AND    BLDG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            BLDG_CELL AS ERROR_CELL," +
                        "            'BLDG_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(BLDG) > 25" +
                        "     AND    BLDG <> '' " +
                        "     AND    BLDG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region RD_STRT

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RD_STRT = '' WHERE RD_STRT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RD_STRT) > 25" +
                    "     AND    RD_STRT <> '' " +
                    "     AND    RD_STRT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            RD_STRT_CELL AS ERROR_CELL," +
                        "            'RD_STRT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RD_STRT) > 25" +
                        "     AND    RD_STRT <> '' " +
                        "     AND    RD_STRT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region AREA

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET AREA = '' WHERE AREA IS NULL";
                dmlService.J_ExecSql(strSQL);

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(AREA) > 25" +
                    "     AND    AREA <> '' " +
                    "     AND    AREA_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            AREA_CELL AS ERROR_CELL," +
                        "            'AREA_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(AREA) > 25" +
                        "     AND    AREA <> '' " +
                        "     AND    AREA_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region TOWN

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET FLT_DOOR = '' WHERE FLT_DOOR IS NULL";
                dmlService.J_ExecSql(strSQL);

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(TOWN) > 25" +
                    "     AND    TOWN <> '' " +
                    "     AND    TOWN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            TOWN_CELL AS ERROR_CELL," +
                        "            'TOWN_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(TOWN) > 25" +
                        "     AND    TOWN <> '' " +
                        "     AND    TOWN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region PIN

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET PIN = '' WHERE PIN IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  PIN = '' " +
                    "     AND    PIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             PIN_CELL AS ERROR_CELL," +
                        "             'PIN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  PIN = ''" +
                        "     AND    PIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(PIN) > 6" +
                    "     AND    PIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            PIN_CELL AS ERROR_CELL," +
                        "            'PIN_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(PIN) > 6" +
                        "     AND    PIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region STATE

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET STATE = '' WHERE STATE IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  STATE = '' " +
                    "     AND    STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             STATE_CELL AS ERROR_CELL," +
                        "             'STATE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  STATE = ''" +
                        "     AND    STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  STATE NOT IN (SELECT STATE_NAME FROM MST_STATE)" +
                    "     AND    STATE <> ''" +
                    "     AND    STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            STATE_CELL AS ERROR_CELL," +
                        "            'STATE_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  STATE NOT IN (SELECT STATE_NAME FROM MST_STATE)" +
                        "     AND    STATE <> ''" +
                        "     AND    STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region STD

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET STD = '' WHERE STD IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  STD = '' " +
                    "     AND    MOBILE = '' " +
                    "     AND    STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             STD_CELL AS ERROR_CELL," +
                        "             'STD_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  STD = ''" +
                        "     AND    MOBILE = '' " +
                        "     AND    STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  STD = '' " +
                    "     AND    PHONE <> '' " +
                    "     AND    STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             STD_CELL AS ERROR_CELL," +
                        "             'STD_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  STD = ''" +
                        "     AND    PHONE <> '' " +
                        "     AND    STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(STD) > 5" +
                    "     AND    STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            STD_CELL AS ERROR_CELL," +
                        "            'STD_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(STD) > 5" +
                        "     AND    STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region PHONE

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET PHONE = '' WHERE PHONE IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  PHONE = '' " +
                    "     AND    MOBILE = '' " +
                    "     AND    PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             PHONE_CELL AS ERROR_CELL," +
                        "             'PHONE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  PHONE = ''" +
                        "     AND    MOBILE = '' " +
                        "     AND    PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  PHONE = '' " +
                    "     AND    STD <> '' " +
                    "     AND    PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             PHONE_CELL AS ERROR_CELL," +
                        "             'PHONE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  PHONE = ''" +
                        "     AND    STD <> '' " +
                        "     AND    PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(PHONE) > 10" +
                    "     AND    PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            PHONE_CELL AS ERROR_CELL," +
                        "            'PHONE_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(PHONE) > 10" +
                        "     AND    PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region EMAIL

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET EMAIL = '' WHERE EMAIL IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  EMAIL = '' " +
                    "     AND    RP_EMAIL = '' " +
                    "     AND    EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             EMAIL_CELL AS ERROR_CELL," +
                        "             'EMAIL_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  EMAIL = ''" +
                        "     AND    RP_EMAIL = '' " +
                        "     AND    EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(EMAIL) > 75" +
                    "     AND    EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            EMAIL_CELL AS ERROR_CELL," +
                        "            'EMAIL_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(EMAIL) > 75" +
                        "     AND    EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region RP_NAME

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_NAME = '' WHERE RP_NAME IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  RP_NAME = '' " +
                    "     AND    RP_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             RP_NAME_CELL AS ERROR_CELL," +
                        "             'RP_NAME_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  RP_NAME = ''" +
                        "     AND    RP_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RP_NAME) > 75" +
                    "     AND    RP_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            RP_NAME_CELL AS ERROR_CELL," +
                        "            'RP_NAME_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RP_NAME) > 25" +
                        "     AND    RP_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region DESIG

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET DESIG = '' WHERE DESIG IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  DESIG = '' " +
                    "     AND    DESIG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             DESIG_CELL AS ERROR_CELL," +
                        "             'DESIG_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  DESIG = ''" +
                        "     AND    DESIG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(DESIG) > 20" +
                    "     AND    DESIG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            DESIG_CELL AS ERROR_CELL," +
                        "            'DESIG_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(DESIG) > 20" +
                        "     AND    DESIG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region FATHER_NAME

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET FATHER_NAME = '' WHERE FATHER_NAME IS NULL";
                dmlService.J_ExecSql(strSQL);

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(FATHER_NAME) > 25" +
                    "     AND    FATHER_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            FATHER_NAME_CELL AS ERROR_CELL," +
                        "            'FATHER_NAME_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(FATHER_NAME) > 25" +
                        "     AND    FATHER_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region MOBILE

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET MOBILE = '' WHERE MOBILE IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  MOBILE = '' " +
                    "     AND    MOBILE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             MOBILE_CELL AS ERROR_CELL," +
                        "             'MOBILE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  MOBILE = ''" +
                        "     AND    MOBILE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(MOBILE) <> 10" +
                    "     AND    MOBILE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            MOBILE_CELL AS ERROR_CELL," +
                        "            'MOBILE_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(MOBILE) <> 10" +
                        "     AND    MOBILE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region FLT/DR/BLCK NO.

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_FLT_DOOR = '' WHERE RP_FLT_DOOR IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  RP_FLT_DOOR = '' " +
                    "     AND    RP_FLT_DOOR_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             RP_FLT_DOOR_CELL AS ERROR_CELL," +
                        "             'RP_FLT_DOOR_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  RP_FLT_DOOR = ''" +
                        "     AND    RP_FLT_DOOR_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RP_FLT_DOOR) > 25" +
                    "     AND    RP_FLT_DOOR <> '' " +
                    "     AND    RP_FLT_DOOR_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            RP_FLT_DOOR_CELL AS ERROR_CELL," +
                        "            'RP_FLT_DOOR_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RP_FLT_DOOR) > 25" +
                        "     AND    RP_FLT_DOOR <> '' " +
                        "     AND    RP_FLT_DOOR_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region BLDG

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_BLDG = '' WHERE RP_BLDG IS NULL";
                dmlService.J_ExecSql(strSQL);

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RP_BLDG) > 25" +
                    "     AND    RP_BLDG <> '' " +
                    "     AND    RP_BLDG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            RP_BLDG_CELL AS ERROR_CELL," +
                        "            'RP_BLDG_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RP_BLDG) > 25" +
                        "     AND    RP_BLDG <> '' " +
                        "     AND    RP_BLDG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region RD_STRT

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_RD_STRT = '' WHERE RP_RD_STRT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RP_RD_STRT) > 25" +
                    "     AND    RP_RD_STRT <> '' " +
                    "     AND    RP_RD_STRT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            RP_RD_STRT_CELL AS ERROR_CELL," +
                        "            'RP_RD_STRT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RP_RD_STRT) > 25" +
                        "     AND    RP_RD_STRT <> '' " +
                        "     AND    RP_RD_STRT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region AREA

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_AREA = '' WHERE RP_AREA IS NULL";
                dmlService.J_ExecSql(strSQL);

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RP_AREA) > 25" +
                    "     AND    RP_AREA <> '' " +
                    "     AND    RP_AREA_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            RP_AREA_CELL AS ERROR_CELL," +
                        "            'RP_AREA_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RP_AREA) > 25" +
                        "     AND    RP_AREA <> '' " +
                        "     AND    RP_AREA_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region TOWN

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_TOWN = '' WHERE RP_TOWN IS NULL";
                dmlService.J_ExecSql(strSQL);

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RP_TOWN) > 25" +
                    "     AND    RP_TOWN <> '' " +
                    "     AND    RP_TOWN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            RP_TOWN_CELL AS ERROR_CELL," +
                        "            'RP_TOWN_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RP_TOWN) > 25" +
                        "     AND    RP_TOWN <> '' " +
                        "     AND    RP_TOWN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region PIN

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_PIN = '' WHERE RP_PIN IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  RP_PIN = '' " +
                    "     AND    RP_PIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             RP_PIN_CELL AS ERROR_CELL," +
                        "             'RP_PIN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  RP_PIN = ''" +
                        "     AND    RP_PIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RP_PIN) > 6" +
                    "     AND    RP_PIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            RP_PIN_CELL AS ERROR_CELL," +
                        "            'RP_PIN_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RP_PIN) > 6" +
                        "     AND    RP_PIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region STATE

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_STATE = '' WHERE RP_STATE IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  RP_STATE = '' " +
                    "     AND    RP_STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             RP_STATE_CELL AS ERROR_CELL," +
                        "             'RP_STATE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  RP_STATE = ''" +
                        "     AND    RP_STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  RP_STATE NOT IN (SELECT STATE_NAME FROM MST_STATE)" +
                    "     AND    RP_STATE <> ''" +
                    "     AND    RP_STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            RP_STATE_CELL AS ERROR_CELL," +
                        "            'RP_STATE_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  RP_STATE NOT IN (SELECT STATE_NAME FROM MST_STATE)" +
                        "     AND    RP_STATE <> ''" +
                        "     AND    RP_STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region STD

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_STD = '' WHERE RP_STD IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  RP_STD = '' " +
                    "     AND    MOBILE = '' " +
                    "     AND    RP_STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             RP_STD_CELL AS ERROR_CELL," +
                        "             'RP_STD_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  RP_STD = ''" +
                        "     AND    MOBILE = '' " +
                        "     AND    RP_STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  RP_STD = '' " +
                    "     AND    RP_PHONE <> '' " +
                    "     AND    RP_STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             RP_STD_CELL AS ERROR_CELL," +
                        "             'RP_STD_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  RP_STD = ''" +
                        "     AND    RP_PHONE <> '' " +
                        "     AND    RP_STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RP_STD) > 5" +
                    "     AND    RP_STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            RP_STD_CELL AS ERROR_CELL," +
                        "            'STD_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RP_STD) > 5" +
                        "     AND    RP_STD_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region PHONE

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_PHONE = '' WHERE RP_PHONE IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  RP_PHONE = '' " +
                    "     AND    MOBILE = '' " +
                    "     AND    RP_PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             RP_PHONE_CELL AS ERROR_CELL," +
                        "             'RP_PHONE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  RP_PHONE = ''" +
                        "     AND    MOBILE = '' " +
                        "     AND    RP_PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  RP_PHONE = '' " +
                    "     AND    RP_STD <> '' " +
                    "     AND    RP_PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             RP_PHONE_CELL AS ERROR_CELL," +
                        "             'RP_PHONE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  RP_PHONE = ''" +
                        "     AND    RP_STD <> '' " +
                        "     AND    RP_PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RP_PHONE) > 10" +
                    "     AND    RP_PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            RP_PHONE_CELL AS ERROR_CELL," +
                        "            'RP_PHONE_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RP_PHONE) > 10" +
                        "     AND    RP_PHONE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region EMAIL

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_EMAIL = '' WHERE RP_EMAIL IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  RP_EMAIL = '' " +
                    "     AND    EMAIL = '' " +
                    "     AND    RP_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             RP_EMAIL_CELL AS ERROR_CELL," +
                        "             'RP_EMAIL_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  RP_EMAIL = ''" +
                        "     AND    EMAIL = '' " +
                        "     AND    RP_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RP_EMAIL) > 75" +
                    "     AND    RP_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "             RP_EMAIL_CELL AS ERROR_CELL," +
                        "            'RP_EMAIL_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RP_EMAIL) > 75" +
                        "     AND    RP_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region PAO
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET PAO = '' WHERE PAO IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  PAO = '' " +
                    //"     AND    DED_TYPE IN ('A','D','E','G','H','L','N','S') " +
                    "     AND    DED_TYPE IN ('A') " +
                    "     AND    PAO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             PAO_CELL AS ERROR_CELL," +
                        "             'PAO_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  PAO = '' " +
                        //"     AND    DED_TYPE IN ('A','D','E','G','H','L','N','S') " +
                        "     AND    DED_TYPE IN ('A') " +
                        "     AND    PAO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //VALUE NOT REQD
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  PAO <> '' " +
                    "     AND    DED_TYPE IN ('B','F','J','K','M','P','Q','T') " +
                    "     AND    PAO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                        "             PAO_CELL AS ERROR_CELL," +
                        "             'PAO_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISC + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  PAO <> ''" +
                        "     AND    DED_TYPE IN ('B','F','J','K','M','P','Q','T') " +
                        "     AND    PAO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(PAO) > 20" +
                    "     AND    PAO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            PAO_CELL AS ERROR_CELL," +
                        "            'PAO_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(PAO) > 20" +
                        "     AND    PAO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region PAO_REG
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET PAO_REG = '' WHERE PAO_REG IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  PAO_REG = '' " +
                    "     AND    DED_TYPE IN ('A') " +
                    "     AND    PAO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             PAO_REG_CELL AS ERROR_CELL," +
                        "             'PAO_REG_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  PAO_REG = ''" +
                        "     AND    DED_TYPE IN ('A') " +
                        "     AND    PAO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //VALUE NOT REQD
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  PAO_REG <> '' " +
                    "     AND    DED_TYPE IN ('B','F','J','K','M','P','Q','T') " +
                    "     AND    PAO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                        "             PAO_REG_CELL AS ERROR_CELL," +
                        "             'PAO_REG_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISC + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  PAO_REG <> ''" +
                        "     AND    DED_TYPE IN ('B','F','J','K','M','P','Q','T') " +
                        "     AND    PAO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(PAO_REG) > 7" +
                    "     AND    PAO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            PAO_REG_CELL AS ERROR_CELL," +
                        "            'PAO_REG_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(PAO_REG) >7" +
                        "     AND    PAO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region DDO
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET DDO = '' WHERE DDO IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  DDO = '' " +
                    "     AND    DED_TYPE IN ('A') " +
                    "     AND    DDO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             DDO_CELL AS ERROR_CELL," +
                        "             'DDO_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  DDO = ''" +
                        "     AND    DED_TYPE IN ('A') " +
                        "     AND    DDO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //VALUE NOT REQD
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  DDO <> '' " +
                    "     AND    DED_TYPE IN ('B','F','J','K','M','P','Q','T') " +
                    "     AND    DDO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                        "             DDO_CELL AS ERROR_CELL," +
                        "             'DDO_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISC + "'" +
                        "      FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "      WHERE  DDO <> ''" +
                        "      AND    DED_TYPE IN ('B','F','J','K','M','P','Q','T') " +
                        "      AND    DDO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(DDO) > 20" +
                    "     AND    DDO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            DDO_CELL AS ERROR_CELL," +
                        "            'DDO_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(DDO) > 20" +
                        "     AND    DDO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region DDO_REG
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET DDO_REG = '' WHERE DDO_REG IS NULL";
                dmlService.J_ExecSql(strSQL);

                ////BLANK OR 0 CHECK
                //strSQL = "SELECT COUNT(*)" +
                //    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                //    "     WHERE  DDO_REG = '' " +
                //    "     AND    DED_TYPE IN (A,D,E,G,H,L,N,S) " +
                //    "     AND    DDO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                ////
                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount > 0)
                //{
                //    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                //        "             DDO_REG_CELL AS ERROR_CELL," +
                //        "             'DDO_REG_CELL'," +
                //        "             '" + strSheetName + "'," +
                //        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                //        "     WHERE  DDO_REG = ''" +
                //        "     AND    DED_TYPE IN (A,D,E,G,H,L,N,S) " +
                //        "     AND    DDO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //    //
                //    //                
                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}

                //VALUE NOT REQD
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  DDO_REG <> '' " +
                    "     AND    DED_TYPE IN ('B','F','J','K','M','P','Q','T') " +
                    "     AND    DDO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                        "             DDO_REG_CELL AS ERROR_CELL," +
                        "             'DDO_REG_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISC + "'" +
                        "      FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "      WHERE  DDO_REG <> ''" +
                        "      AND    DED_TYPE IN ('B','F','J','K','M','P','Q','T') " +
                        "      AND    DDO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(DDO_REG) > 10" +
                    "     AND    DDO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            DDO_REG_CELL AS ERROR_CELL," +
                        "            'DDO_REG_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(DDO_REG) > 10" +
                        "     AND    DDO_REG_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region MIN_STATE
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET MIN_STATE = '' WHERE MIN_STATE IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  MIN_STATE = '' " +
                    "     AND    DED_TYPE IN ('E','H','N','S') " +
                    "     AND    MIN_STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             MIN_STATE_CELL AS ERROR_CELL," +
                        "             'MIN_STATE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  MIN_STATE = ''" +
                        "     AND    DED_TYPE IN ('E','H','N','S') " +
                        "     AND    MIN_STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //VALUE NOT REQD
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  MIN_STATE <> '' " +
                    "     AND    DED_TYPE IN ('A','B','D','F','G','J','K','L','M','P','Q','T') " +
                    "     AND    MIN_STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                        "             MIN_STATE_CELL AS ERROR_CELL," +
                        "             'MIN_STATE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISC + "'" +
                        "      FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "      WHERE  MIN_STATE <> ''" +
                        "      AND    DED_TYPE IN ('A','B','D','F','G','J','K','L','M','P','Q','T') " +
                        "      AND    MIN_STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  MIN_STATE NOT IN (SELECT STATE_NAME FROM MST_STATE)" +
                    "     AND    MIN_STATE <> ''" +
                    "     AND    MIN_STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            MIN_STATE_CELL AS ERROR_CELL," +
                        "            'MIN_STATE_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  MIN_STATE NOT IN (SELECT STATE_NAME FROM MST_STATE)" +
                        "     AND    MIN_STATE <> ''" +
                        "     AND    MIN_STATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region MINISTRY
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET MINISTRY = '' WHERE MINISTRY IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  MINISTRY = '' " +
                    "     AND    DED_TYPE IN ('A') " +
                    "     AND    MINISTRY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             MINISTRY_CELL AS ERROR_CELL," +
                        "             'MINISTRY_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  MINISTRY = ''" +
                        "     AND    DED_TYPE IN ('A') " +
                        "     AND    MINISTRY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //VALUE NOT REQD
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  MINISTRY <> '' " +
                    "     AND    DED_TYPE IN ('B','F','J','K','M','P','Q','S','T') " +
                    "     AND    MINISTRY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                        "             MINISTRY_CELL AS ERROR_CELL," +
                        "             'MINISTRY_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISC + "'" +
                        "      FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "      WHERE  MINISTRY <> ''" +
                        "      AND    DED_TYPE IN ('B','F','J','K','M','P','Q','S','T') " +
                        "      AND    MINISTRY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  MINISTRY NOT IN (SELECT MINISTRY_NAME FROM MST_MINISTRY)" +
                    "     AND    MINISTRY <> ''" +
                    "     AND    MINISTRY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            MINISTRY_CELL AS ERROR_CELL," +
                        "            'MINISTRY_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  MINISTRY NOT IN (SELECT MINISTRY_NAME FROM MST_MINISTRY)" +
                        "     AND    MINISTRY <> ''" +
                        "     AND    MINISTRY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region OTH_MIN
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET OTH_MIN = '' WHERE OTH_MIN IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  OTH_MIN = '' " +
                    "     AND    MINISTRY IN ('OTHERS') " +
                    "     AND    OTH_MIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             OTH_MIN_CELL AS ERROR_CELL," +
                        "             'OTH_MIN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  OTH_MIN = ''" +
                        "     AND    MINISTRY IN ('OTHERS') " +
                        "     AND    OTH_MIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //VALUE NOT REQD
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  OTH_MIN <> '' " +
                    "     AND    MINISTRY <> 'OTHERS' " +
                    "     AND    OTH_MIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                        "             OTH_MIN_CELL AS ERROR_CELL," +
                        "             'OTH_MIN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISC + "'" +
                        "      FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "      WHERE  OTH_MIN <> '' " +
                        "      AND    MINISTRY <> 'OTHERS' " +
                        "      AND    OTH_MIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(OTH_MIN) > 25" +
                    "     AND    OTH_MIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            OTH_MIN_CELL AS ERROR_CELL," +
                        "            'OTH_MIN_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(OTH_MIN) > 25" +
                        "     AND    OTH_MIN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                #endregion

                #region AOI
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET AOI = '' WHERE AOI IS NULL";
                dmlService.J_ExecSql(strSQL);

                ////BLANK OR 0 CHECK
                //strSQL = "SELECT COUNT(*)" +
                //    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                //    "     WHERE  AOI = '' " +
                //    "     AND    DED_TYPE IN ('A','S') " +
                //    "     AND    AOI_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                ////
                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount > 0)
                //{
                //    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                //        "             AOI_CELL AS ERROR_CELL," +
                //        "             'AOI_CELL'," +
                //        "             '" + strSheetName + "'," +
                //        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                //        "     WHERE  AOI = ''" +
                //        "     AND    DED_TYPE IN ('A','S') " +
                //        "     AND    AOI_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //    //
                //    //                
                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}

                //VALUE NOT REQD
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  AOI <> '' " +
                    "     AND    DED_TYPE IN ('B','D','E','F','G','H','J','K','L','M','N','P','Q','T') " +
                    "     AND    AOI_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                        "             AOI_CELL AS ERROR_CELL," +
                        "             'AOI_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISC + "'" +
                        "      FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "      WHERE  AOI <> ''" +
                        "      AND    DED_TYPE IN ('B','D','E','F','G','H','J','K','L','M','N','P','Q','T') " +
                        "      AND    AOI_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(AOI) > 10" +
                    "     AND    AOI_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            AOI_CELL AS ERROR_CELL," +
                        "            'AOI_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(AOI) > 10" +
                        "     AND    AOI_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                #endregion

                #region RP PAN

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " SET RP_PAN = '' WHERE RP_PAN IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  RP_PAN = ''" +
                    "     AND    RP_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             RP_PAN_CELL AS ERROR_CELL," +
                        "             'RP_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  RP_PAN = ''" +
                        "     AND    RP_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                    "     WHERE  LEN(RP_PAN) <> 10" +
                    "     AND    RP_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "             RP_PAN_CELL AS ERROR_CELL," +
                        "             'RP_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  LEN(RP_PAN) <> 10" +
                        "     AND    RP_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //PAN STRUCTURE CHECK
                strSQL = "SELECT COUNT(*)" +
                 "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                 "     WHERE  (ISNUMERIC(LEFT(RP_PAN,1)) <> 0" +
                 "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,2,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,3,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,4,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,5,1)) <> 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,6,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,7,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,8,1)) = 0 " +
                 "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,9,1)) = 0 " +
                 "     OR     ISNUMERIC(RIGHT(RP_PAN,1)) = -1" +
                 "     OR     RP_PAN = 'PANNOTREQD')" +
                 "     AND    RP_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             RP_PAN_CELL AS ERROR_CELL," +
                        "             'RP_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "" +
                        "     WHERE  (ISNUMERIC(LEFT(RP_PAN,1)) <> 0" +
                        "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,2,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,3,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,4,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,5,1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,6,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,7,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,8,1)) = 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(RP_PAN,9,1)) = 0 " +
                        "     OR     ISNUMERIC(RIGHT(RP_PAN,1)) = -1" +
                        "     AND    RP_PAN  = 'PANNOTREQD')" +
                        "     AND    RP_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
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

        #region KILL EXCEL
        private bool KILL_EXCEL()
        {
            try
            {
                //--
                //foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                //{
                //    if (process.MainModule.ModuleName.ToUpper().Equals("EXCEL.EXE"))
                //    {
                //        process.Kill();
                //        //process.Close();
                //        //process.Dispose();
                //        break;
                //    }
                //}
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DELETE WORKSHEET
        private bool DELETE_WORKSHEET(string ExcelFilePath)
        {
            try
            {
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

        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
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
                Microsoft.Office.Interop.Excel.Worksheet wsnew = sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet ;
                
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

        #region WRITE ERROR WORKSHEET
        private bool WRITE_ERROR_WORKSHEET(string ExcelFilePath)
        {

            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            string strMatchSheetName = T_Company_Master_Sheet_Name.COMPANY_MASTER;
            int intSkipIF = 0;
            try
            {
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
                strSQL = "SELECT ERR_TYPE," +
                    "            ERR_CELL," +
                    "            ERR_COLUMN," +
                    "            ERR_SHEET," +
                    "            ERR_DESC " +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "     ORDER BY ERR_SHEET," +
                    "            ERR_VALIDATION_ID";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    drdGetErrorSheetRecord.Close();
                    drdGetErrorSheetRecord.Dispose();
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
                            wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = T_Company_Master_Sheet_Name.COMPANY_MASTER;
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

        #region COLOR ERROR CELLS
        private bool COLOR_ERROR_CELLS(string ExcelFilePath)
        {

            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            //--
            try
            {
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
                //
                strSQL = "SELECT ERR_TYPE," +
                    "            ERR_CELL," +
                    "            ERR_COLUMN," +
                    "            ERR_COLOR," +
                    "            ERR_SHEET," +
                    "            ERR_DESC " +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "     ORDER BY ERR_SHEET";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    drdGetErrorSheetRecord.Close();
                    drdGetErrorSheetRecord.Dispose();
                    return false;
                }
                while (drdGetErrorSheetRecord.Read())
                {

                    Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[drdGetErrorSheetRecord["ERR_SHEET"].ToString()];
                    //
                    wsnew.get_Range(drdGetErrorSheetRecord["ERR_CELL"].ToString(), m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromName(drdGetErrorSheetRecord["ERR_COLOR"].ToString()));
                    //
                    wb.Save();
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
            catch(Exception e)
            {
                return false;
            }
        }
        #endregion

        #region INITIALIZE COLOR ERROR CELLS
        private bool INITIALIZE_COLOR_ERROR_CELLS(string ExcelFilePath)
        {

            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            //--
            try
            {
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
                //
                strSQL = "SELECT ERR_TYPE," +
                    "            ERR_CELL," +
                    "            ERR_COLUMN," +
                    "            ERR_COLOR," +
                    "            ERR_SHEET," +
                    "            ERR_DESC " +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "     ORDER BY ERR_SHEET";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    //drdGetErrorSheetRecord.Close();
                    //drdGetErrorSheetRecord.Dispose();
                    return false;
                }
                while (drdGetErrorSheetRecord.Read())
                {
                    //if (FormNo == drdGetErrorSheetRecord["ERR_FORM_NO"].ToString())
                    //{
                        Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[drdGetErrorSheetRecord["ERR_SHEET"].ToString()];
                        //
                        wsnew.get_Range(drdGetErrorSheetRecord["ERR_CELL"].ToString(), m).Interior.ColorIndex = -4142;
                        //
                        wb.Save();
                    //}
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
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region LOAD IMPORT INTERFACE
        private bool LOAD_IMPORT_INTERFACE()
        {
            try
            {
                //if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""))) == 0)
                //{
                //    //--
                //    blnOpenTabPage = true;
                //    tbcExcelImport.TabPages.Remove(tbpValidateExcelFile);
                //    //-- EXISTING RECORDS NOT TO BE UPDATED
                //    long lngTotalRecords = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_ID) AS EXISTING_DEDUCTEES FROM MST_DEDUCTEE")));
                //    long lngExistingRecords = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_ID) AS UPDATE_DEDUCTEE FROM TEMP_MST_DEDUCTEE WHERE MODE ='" + T_Deductee_Master_Updt_Tag.UPDATE + "'")));
                //    //-- UPDATED RECORDS
                //    if (chkUpdate.Checked == true)
                //    {
                //        lblExistingDeductee.Text = "Existing Deductee Record (to be updated)";
                //        //
                //        lblExistingDeducteeNoChange.Text = Convert.ToString(lngTotalRecords - lngExistingRecords);
                //    }
                //    else
                //    {
                //        lblExistingDeductee.Text = "Existing Deductee Record (not to be updated)";
                //        //
                //        lblExistingDeducteeNoChange.Text = Convert.ToString(lngTotalRecords);
                //    }
                //    //
                //    lblDeducteesUpdated.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_ID) AS UPDATE_DEDUCTEE FROM TEMP_MST_DEDUCTEE WHERE MODE ='" + T_Deductee_Master_Updt_Tag.UPDATE + "'"));
                //    //-- NEW RECORDS
                //    lblNewRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_ID) AS NEW_DEDUCTEE FROM TEMP_MST_DEDUCTEE WHERE MODE ='" + T_Deductee_Master_Updt_Tag.NEW + "'"));
                //    //--
                //    return true;
                //}
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region LoadCompanyMasterGrid
        private void LoadCompanyMasterGrid()
        {
            string strMidSubString = "";
            string strUpper = "";
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

            //UPDATE MINISTRY_ID ON TEMP MASTER TABLE
            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                "     INNER  JOIN MST_CATEGORY " +
                "            ON  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".DED_TYPE = MST_CATEGORY.CATEGORY_CODE " +
                "     SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".DED_TYPE_ID  = MST_CATEGORY.CATEGORY_ID " +
                "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".DED_TYPE <> ''";

            if (dmlService.J_ExecSql(strSQL) == false)
                return;

            //UPDATE STATE_ID ON TEMP MASTER TABLE
            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                "     INNER  JOIN MST_STATE " +
                "            ON  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".STATE = MST_STATE.STATE_NAME " +
                "     SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".STATE_ID   = MST_STATE.STATE_ID " +
                "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".STATE <> ''";

            if (dmlService.J_ExecSql(strSQL) == false)
                return;

            //UPDATE STATE_ID ON TEMP MASTER TABLE
            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                "     INNER  JOIN MST_STATE " +
                "            ON  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".RP_STATE = MST_STATE.STATE_NAME " +
                "     SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".RP_STATE_ID  = MST_STATE.STATE_ID " +
                "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".RP_STATE <> ''";

            if (dmlService.J_ExecSql(strSQL) == false)
                return;

            //UPDATE MIN_STATE_ID ON TEMP MASTER TABLE
            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                "     SET    MIN_STATE_ID = 0 " +
                "     WHERE  MIN_STATE_ID IS NULL";

            if (dmlService.J_ExecSql(strSQL) == false)
                return;

            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                "     INNER  JOIN MST_STATE " +
                "            ON  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".MIN_STATE = MST_STATE.STATE_NAME " +
                "     SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".MIN_STATE_ID  = MST_STATE.STATE_ID " +
                "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".MIN_STATE <> ''";

            if (dmlService.J_ExecSql(strSQL) == false)
                return;

            //UPDATE MINISTRY_ID ON TEMP MASTER TABLE
            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                "     SET    MINISTRY_ID = 0 " +
                "     WHERE  MINISTRY_ID IS NULL";

            if (dmlService.J_ExecSql(strSQL) == false)
                return;

            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + " " +
                "     INNER  JOIN MST_MINISTRY " +
                "            ON  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".MINISTRY = MST_MINISTRY.MINISTRY_NAME " +
                "     SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".MINISTRY_ID  = MST_MINISTRY.MINISTRY_ID " +
                "     WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".MINISTRY <> ''";

            if (dmlService.J_ExecSql(strSQL) == false)
                return;
            
            //-----------------------------------------------------------
            string[,] strMatrixChallanDetails = {{"COMPANY_ID", "0", "", "", "", "F", ""},
                                        {"COMPANY NAME", "300", "S", "", "", "", "T"},
                                        {"TAN", "100", "S", "", "", "", "T"},
                                        {"PAN", "100", "S", "", "", "", "T"},
                                        {"DEDUCTOR TYPE", "100", "S", "", "", "", "T"},
                                        {"RESPONSIBLE PERSON", "300", "0.00", "", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            //string[,] strError = {{"(CDBL(TEMP_CHALLAN_DETAILS.TOTAL_TAX_DEPOSITED) - TEMP_CHALLAN_DETAILS.CTRL_TOT_TAX) < 0 ", "F", "Error!!", "T"},
            //                                    {"JAYA", "F", "", "T"}};

            strOrderBy = "COMPANY_NAME,COMPANY_TAN";
            strQuery = @"SELECT COMPANY_ID,
                                " + strUpper + @"(COMPANY_NAME),
                                " + strUpper + @"(COMPANY_TAN),
                                " + strUpper + @"(COMPANY_PAN),
                                MST_CATEGORY.CATEGORY_CODE + ' - ' + MST_CATEGORY.CATEGORY_DESCRIPTION AS D_STATUS,
                                " + strUpper + @"(RP_NAME) 
                         FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + @", 
                                MST_CATEGORY 
                         WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + ".DED_TYPE_ID  = MST_CATEGORY.CATEGORY_ID ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQL, strMatrixChallanDetails);       //Show Data into the Grid
        }
        #endregion
       
        #region INSERT_COMPANY_DATA
        private bool INSERT_COMPANY_DATA()
        {
            strSQL = @"INSERT INTO MST_COMPANY (
                                 COMPANY_NAME,
                                 TAN_NO,
                                 PAN_NO,
                                 BRANCH_DIV,
                                 D_CATEGORY_ID,
                                 TAN_REG_NO,
                                 MINISTRY_ID,
                                 MINISTRY_OTHER,
                                 ADDRESS1,
                                 ADDRESS2,
                                 ADDRESS3,
                                 ADDRESS4,
                                 ADDRESS5,
                                 STATE_ID,
                                 PIN_CODE,
                                 STD,
                                 PHONE,
                                 EMAIL,
                                 PERSON_NAME,
                                 DESIGNATION,
                                 FATHER_NAME,
                                 P_ADDRESS1,
                                 P_ADDRESS2,
                                 P_ADDRESS3,
                                 P_ADDRESS4,
                                 P_ADDRESS5,
                                 P_STATE_ID,
                                 P_PIN_CODE,
                                 P_STD,
                                 P_PHONE,
                                 P_EMAIL,
                                 P_MOBILE,
                                 PAO_CODE,
                                 PAO_REG_NO,
                                 DDO_CODE,
                                 DDO_REG_NO,
                                 D_STATE_ID,
                                 AIN_NO,
                                 P_PAN)
                          SELECT COMPANY_NAME, 
                                COMPANY_TAN, 
                                COMPANY_PAN, 
                                BRANCH, 
                                DED_TYPE_ID,
                                TAN_REG, 
                                MINISTRY_ID,
                                OTH_MIN, 
                                FLT_DOOR, 
                                BLDG, 
                                RD_STRT, 
                                AREA, 
                                TOWN, 
                                STATE_ID,
                                PIN,  
                                STD, 
                                PHONE, 
                                EMAIL, 
                                RP_NAME,
                                DESIG, 
                                FATHER_NAME,
                                RP_FLT_DOOR, 
                                RP_BLDG, 
                                RP_RD_STRT, 
                                RP_AREA, 
                                RP_TOWN, 
                                RP_STATE_ID, 
                                RP_PIN,  
                                RP_STD, 
                                RP_PHONE,
                                RP_EMAIL,  
                                MOBILE,   
                                PAO, 
                                PAO_REG, 
                                DDO, 
                                DDO_REG,  
                                MIN_STATE_ID,
                                AOI,
                                RP_PAN  
                        FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_COMPANY + "";
            //}
            if (dmlService.J_ExecSql(strSQL) == false)
                return false;

            return true;
        }
        #endregion

        #region CheckExcelStructure
        private bool CheckExcelStructure(string ExcelFilePath)
        {
            try
            {
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
                //--------------------
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Company name", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "TAN", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "PAN", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Branch/Division", con) == false)
                {
                    return false;
                    con.Close();
                    con.Dispose();
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Deductor type", con) == false)
                {
                    return false;
                    con.Close();
                    con.Dispose();
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "TAN Reg No", con) == false)
                {
                    return false;
                    con.Close();
                    con.Dispose();
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Flt/Dr/Blck No", con) == false)
                {
                    return false;
                    con.Close();
                    con.Dispose();
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Building", con) == false)
                {
                    return false;
                    con.Close();
                    con.Dispose();
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Rd/Strt/Lane", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Area/Locality", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Town/District", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "PIN", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "State", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "STD", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Phone", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Email", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's Name", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Designation", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Father's Name", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Mobile No", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's Flt/Dr/Blck No", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's Building", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's Rd/Strt/Lane", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's Area/Locality", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's Town/District", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's PIN", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's State", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's STD", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's Phone", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's Email", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "PAO Code", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "PAO Reg No", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "DDO Code", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "DDO Reg No", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "State", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Ministry", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Other Ministry", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Account Office Identification Number", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //-- 2015/04/21
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Company_Master_Sheet_Name.COMPANY_MASTER, "Responsible Person's PAN", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                //--------------------
                con.Close();
                con.Dispose();

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

        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0078", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}

