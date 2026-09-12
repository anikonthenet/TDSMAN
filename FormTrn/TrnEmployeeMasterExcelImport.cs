
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
    using ICSharpCode.SharpZipLib.Zip;
    using Excel = Microsoft.Office.Interop.Excel.Worksheet;
    //~~~~ This namespace are using for using VB6 component
    using Microsoft.VisualBasic.Compatibility.VB6;

#endregion



namespace TDSMAN.FormTrn
{
    #region STRUCTURE

    #region T_Employee_Master_Sheet_Name
    public struct T_Employee_Master_Sheet_Name
    {
        public const string VALIDATION_ERROR_DETAILS = "Validation Error Details";
        public const string EMPLOYEE_MASTER = "Employee Master";
    }
    #endregion

    //#region T_Error_Type
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
    //#endregion

    //#region T_Error_Type_Color
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
    //#endregion

    //#region T_Deductee_Master_Sheet_Name
    //public struct T_Deductee_Master_Sheet_Name
    //{
    //    public const string VALIDATION_ERROR_DETAILS = "Validation Error Details";
    //    public const string DEDUCTEE_MASTER = "Deductee Master";
    //}
    //#endregion

    //#region T_Deductee_Master_SaveTag
    //public struct T_Deductee_Master_SaveTag
    //{
    //    public const string IMPORT = "IMPORT";
    //    public const string VALIDATE = "VALIDATE";
    //}
    //#endregion

    //#region T_Deductee_Master_Updt_Tag
    //public struct T_Deductee_Master_Updt_Tag
    //{
    //    public const string NEW = "New";
    //    public const string UPDATE = "Existing";
    //}
    //#endregion

    #endregion      

    public partial class TrnEmployeeMasterExcelImport : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnEmployeeMasterExcelImport()
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
        string strTempEmployeeMasterPath = "";
        //
        bool blnOpenTabPage = false;

        #endregion

        #region set ENUM

        #region T_DEDUCTEE_MASTER_COLUMN

        public enum T_DEDUCTEE_MASTER_COLUMN
        {
            DEDUCTEE_ID = 0,
            DEDUCTEE_NAME = 1,
            DEDUCTEE_NAME_CELL = 2,
            DEDUCTEE_PAN = 3,
            DEDUCTEE_PAN_CELL =4,
            DEDUCTEE_CODE = 5,
            DEDUCTEE_CODE_CELL = 6,
            ADDRESS1 = 7,
            ADDRESS1_CELL = 8,
            ADDRESS2 = 9,
            ADDRESS2_CELL =10,
            ADDRESS3 =11,
            ADDRESS3_CELL =12,
            ADDRESS4 =13,
            ADDRESS4_CELL =14,
            ADDRESS5 =15,
            ADDRESS5_CELL =16,
            STATE =17,
            STATE_CELL =18,
            PIN =19,
            PIN_CELL =20,
            MOBILE =21,
            MOBILE_CELL =22,
            EMAIL =23,
            EMAIL_CELL =24
        }
        #endregion

        #region T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL

        public enum T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL
        {
            EMPLOYEE_NAME = 0,
            EMPLOYEE_PAN = 1,
            EMPLOYEE_CATEGORY = 2,
            REFERENCE_NO = 3,
            DESIGNATION = 4
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
            lblTitle.Text = "Employee Master Import";
            //
            lblMessage.Text = "Please Note : - ";
            lblMessage1.Text = "1. Only unique PAN should be provided for import.";
            lblMessage2.Text = "2. For employee with PANNOTAVBL unique name should be provided.";
            //
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME + ' [' + TAN_NO + ']'" +
                "      FROM   MST_COMPANY " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //
            BtnSave.Tag = T_SaveTag.VALIDATE;
            //    
            cmbCompany.Select();
        }

        #endregion

        #region btnSelectExcelPath_Click
        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
            if (rbnExcel.Checked == true)
            {
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                    strFVUPath = cmnService.J_OpenFileDialog("Excel File | *.xlsx", "Excel File | *.xlsx", "Choose the Excel File to import");
                else
                    strFVUPath = cmnService.J_OpenFileDialog("Excel File | *.xls; *.xlsx", "Excel File | *.xls; *.xlsx", "Choose the Excel File to import");
            }
            else if (rbnCSV.Checked == true)
            {
                strFVUPath = cmnService.J_OpenFileDialog("CSV File | *.csv", "CSV File | *.csv", "Choose the CSV File to import");
            }
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
            //-- Added By Abhishek Dey On 01/11/2019 --
            #region IS COMPANY ACCESSABLE FOR CLIENT
            if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
            {
                if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                {
                    if (TdsMan.IsCompanyAccessible(Convert.ToInt64(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                    {
                        cmnService.J_UserMessage("You are not authorised to proceed.");
                        BtnExit.Select();
                        return;
                    }

                    //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

                }
            }
            #endregion
            //-----------------------------------------
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            if (Convert.ToString(BtnSave.Tag) == T_SaveTag.VALIDATE)
            {
                //--
                if (ValidateFields() == false) return;
                //--
                if (rbnExcel.Checked == true)
                {

                    if (cmnService.J_UserMessage("Proceed Employee Master Excel Import??", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
                else if (rbnCSV.Checked == true)
                {

                    if (cmnService.J_UserMessage("Proceed Employee Master CSV Import??", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
                //--                
                this.Cursor = Cursors.WaitCursor;
                //--
                if (rbnExcel.Checked == true)
                {
                    if (CheckExcelStructure(txtExcelPath.Text) == false)
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
                }
                //
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
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
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    if (CREATE_TEMP_TABLES() == false)
                    {
                        cmnService.J_UserMessage("Temporary Tables Not created");
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                }
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                // GET DATA FROM EXCEL ACCESS       
                //
                lblProgressDisplayMessage.Visible = true;
                lblProgressDisplayMessage.Text = "Transferring Data";
                //
                if (GET_DATA_FROM_EXCEL_CSV_ACCESS() == false)
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
                if (rbnExcel.Checked == true)
                {
                    if (DELETE_WORKSHEET(txtExcelPath.Text) == false)
                    {
                        cmnService.J_UserMessage("Error Sheet Deletion failed");
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                    //
                    if (CREATE_NEW_WORKSHEET(txtExcelPath.Text) == false)
                    {
                        cmnService.J_UserMessage("Error Sheet Creation failed");
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                }
                //--
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

                    if (rbnExcel.Checked == true)
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
                    }
                    else
                    {
                        if (WRITE_ERROR_HTML(txtExcelPath.Text) == false)
                        {
                            cmnService.J_UserMessage("Writing Error File failed", MessageBoxIcon.Error);
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

                    if (rbnExcel.Checked == true)
                    {
                        //cmnService.J_UserMessage("Excel File Validation failed \n Check the <Validation Error> Sheet of the Excel file ", MessageBoxIcon.Exclamation);
                        if (cmnService.J_UserMessage("Excel File Validation failed \n Do you want to open the Excel file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            if (File.Exists(txtExcelPath.Text) == true)
                            {
                                System.Diagnostics.Process.Start(txtExcelPath.Text);
                            }
                        }
                    }
                    else if (rbnCSV.Checked == true)
                    {
                        //--
                        cmnService.J_UserMessage("CSV File Validation failed \n Now it will open the error (HTML) file.", MessageBoxIcon.Exclamation);
                        //--
                        prgBar.Value = 0;
                        if (File.Exists(Path.Combine(Path.GetDirectoryName(txtExcelPath.Text), Path.GetFileNameWithoutExtension(txtExcelPath.Text) + ".htm")) == true)
                        {
                            System.Diagnostics.Process.Start(Path.Combine(Path.GetDirectoryName(txtExcelPath.Text), Path.GetFileNameWithoutExtension(txtExcelPath.Text) + ".htm"));
                        }
                    }
                    //
                    prgBar.Value = 0;
                    return;
                }
                else
                {
                    if (UPDT_MODE(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))) == false)
                    {
                        cmnService.J_UserMessage("Updating failed");
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }

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
                if (LOAD_IMPORT_INTERFACE() == false)
                {
                    cmnService.J_UserMessage("Loading Import Interface failed");
                    this.Cursor = Cursors.Default;
                    prgBar.Value = 0;
                    return;
                }
                //- UPDATE MODE
                LoadDeducteeMasterGrid();
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
                if (INSERT_EMPLOYEE_DATA() == false) return;
                //--
                //
                if(DROP_TEMP_TABLES() == false) return;
                //--
                for (int i = prgImportBar.Minimum; i <= prgImportBar.Maximum; i++)
                {
                    prgImportBar.PerformStep();
                }
                this.Cursor = Cursors.Default;
                //--
                BtnSave.Enabled = false;
                //--
                cmnService.J_UserMessage("Import from Excel File completed", MessageBoxIcon.Information);
                prgImportBar.Value = 0;
                //--
                this.Close();
                this.Dispose();
                //--
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
            //----------------------------------------------------
            dgcViewChallan.Select(dgcViewChallan.CurrentRowIndex);
            dgcViewChallan.Select();
            dgcViewChallan.Focus();
        }
        #endregion

        #region dgcViewChallan_MouseClick
        private void dgcViewChallan_MouseClick(object sender, MouseEventArgs e)
        {
            dgcViewChallan_Click(sender, e);
            ////----------------------------------------------------
            //dgcViewChallan.Select(dgcViewChallan.CurrentRowIndex);
            //dgcViewChallan.Select();
            //dgcViewChallan.Focus();
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
                    if (cmbCompany.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Select Company");
                        cmbCompany.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- EXCEL FILE SELECTED
                    //-----------------------------------------------------------------------
                    if (txtExcelPath.Text.Trim()== "")
                    {
                        cmnService.J_UserMessage("File not selected");
                        btnSelectExcelPath.Select();
                        return false;
                    }
                    if (rbnExcel.Checked == true)
                    {
                        // FILE SHOULD BE EXCEL
                        if (Path.GetExtension(txtExcelPath.Text).ToUpper() != ".XLS" && Path.GetExtension(txtExcelPath.Text).ToUpper() != ".XLSX")
                        {
                            cmnService.J_UserMessage("Selected file should be a Excel file");
                            btnSelectExcelPath.Select();
                            return false;
                        }
                    }
                    else if(rbnCSV.Checked == true)
                    {
                        if (Path.GetExtension(txtExcelPath.Text).ToUpper() != ".CSV")
                        {
                            cmnService.J_UserMessage("Selected file should be a CSV file");
                            btnSelectExcelPath.Select();
                            return false;
                        }
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
        private bool CREATE_TEMP_TABLES()
        {
            try
            {
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ""; 
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //Blocked by INDRAJIT on 16-03-2012
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "") == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " (" +
                         "                  EMPLOYEE_ID        COUNTER," +
                         "                  MODE               TEXT(10) DEFAULT \"\"," +
                         "                  EMPLOYEE_NAME      TEXT(255) DEFAULT \"\"," +
                         "                  EMPLOYEE_NAME_CELL TEXT(10)  DEFAULT \"\"," +
                         "                  EMPLOYEE_PAN       TEXT(255) DEFAULT \"\"," +
                         "                  EMPLOYEE_PAN_CELL  TEXT(10)  DEFAULT \"\"," +
                         "                  EMPLOYEE_CATEGORY  TEXT(255) DEFAULT \"\"," +
                         "                  EMPLOYEE_CATEGORY_CELL TEXT(10)  DEFAULT \"\"," +
                         "                  REFERENCE_NO       TEXT(255) DEFAULT \"\"," +
                         "                  REFERENCE_NO_CELL  TEXT(10)  DEFAULT \"\"," +
                         "                  DESIGNATION        TEXT(255) DEFAULT \"\"," +
                         "                  DESIGNATION_CELL   TEXT(10)  DEFAULT \"\")";
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
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
                    //strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                    //     "                  ERR_VALIDATION_ID COUNTER," +
                    //     "                  ERR_TYPE          TEXT(25) DEFAULT \"\"," +
                    //     "                  ERR_CELL          TEXT(10) DEFAULT \"\"," +
                    //     "                  ERR_COLUMN        TEXT(50) DEFAULT \"\"," +
                    //     "                  ERR_SHEET         TEXT(25) DEFAULT \"\"," +
                    //     "                  ERR_COLOR         TEXT(25) DEFAULT \"\"," +
                    //     "                  ERR_DESC          TEXT(255) DEFAULT \"\"," +
                    //     "                  ERR_FORM_NO       TEXT(5) DEFAULT \"\")";
                    if (rbnCSV.Checked == true)
                        strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                             "                  " + cmnService.J_GetDataType("ERR_VALIDATION_ID", J_Identity.YES) + "," +
                             "                  " + cmnService.J_GetDataType("ERR_TYPE", J_ColumnType.String, 255) + "," +
                             "                  " + cmnService.J_GetDataType("ERR_ROW", J_ColumnType.String, 255) + "," +
                             "                  " + cmnService.J_GetDataType("ERR_COLUMN", J_ColumnType.String, 255) + "," +
                             "                  " + cmnService.J_GetDataType("ERR_SHEET", J_ColumnType.String, 255) + "," +
                             "                  " + cmnService.J_GetDataType("ERR_DESC", J_ColumnType.String, 255) + ")";
                    else
                        strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                         "                  " + cmnService.J_GetDataType("ERR_VALIDATION_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_TYPE", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_CELL", J_ColumnType.String, 10) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLUMN", J_ColumnType.String, 50) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_SHEET", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLOR", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_DESC", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_FORM_NO", J_ColumnType.String, 50) + ")";
                    dmlService.J_ExecSql(strSQL);
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
                #region T_tblTEMP_MST_EMPLOYEE
                dmlService.J_BeginTransaction();
                //MessageBox.Show("2");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "") == true)
                {
                    //MessageBox.Show("2.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //MessageBox.Show("3");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "") == false)
                {
                    //MessageBox.Show("3.1");
                    if (rbnCSV.Checked == true)
                    {
                        strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + @" (
                                            " + cmnService.J_GetDataType("SERIAL_NO", J_ColumnType.String, 10) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_PAN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_CATEGORY", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("REFERENCE_NO", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("DESIGNATION", J_ColumnType.String) + @")";
                    }
                    else if (rbnExcel.Checked == true)
                    {
                        strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + @" (
                                            " + cmnService.J_GetDataType("EMPLOYEE_ID", J_Identity.YES) + @",
                                            " + cmnService.J_GetDataType("MODE", J_ColumnType.String, 10) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_NAME_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_PAN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_PAN_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_CATEGORY", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_CATEGORY_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("REFERENCE_NO", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("REFERENCE_NO_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("DESIGNATION", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("DESIGNATION_CELL", J_ColumnType.Char) + @")";
                    }
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
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "";
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
                    string strEmployeeCategory = "";
                    string strReferenceNo = "";
                    string strDesignation = "";

                    string strStartupPath = "";
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                        strStartupPath = Path.Combine(J_Var.J_pEnterpriseServerPath, "TMP FOLDER");
                    else
                        strStartupPath = Path.Combine(Application.StartupPath, "TMP FOLDER");
                    if (Directory.Exists(strStartupPath) == false)
                        // DELETE IF THE FILE EXISTS.
                        Directory.CreateDirectory(strStartupPath);
                    //
                    strTempEmployeeMasterPath = Path.Combine(Application.StartupPath, "EmployeeMaster.txt");
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                    {
                        strTempEmployeeMasterPath = Path.Combine(strStartupPath, TDSMAN.Classes.TDSMAN.T_pProductSerial + "_EmployeeMaster.txt");
                        if (File.Exists(strTempEmployeeMasterPath) == true)
                            File.Delete(strTempEmployeeMasterPath);
                    }

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
                myCommand = new OleDbDataAdapter("SELECT * FROM [" + T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER + "$]", con);
                myCommand.Fill(myDataSet);
                //con.Close();

                long lngRow = 2;
                long lngColumn = 0;

                int intLineNumber = 0;

                StreamWriter StreamWriterDeductee = cmnService.J_ReturnStreamWriter(strTempEmployeeMasterPath);

                //Travers through each row in the dataset
                foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
                {
                    lblProgressDisplayMessage.Visible = true;
                    //lblProgressDisplayMessage.Text = "Transferring Data [" + T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER + " : " + lngRow + " ]";
                    //this.Refresh();
                    //////
                    //lngColumn =65;
                    ////Stores info in Datarow into an array
                    Object[] cells = myDataRow.ItemArray;

                    intLineNumber = intLineNumber + 1;
                    int intColumnValue = 64;
                    //
                    strEmployeeName = Convert.ToString(cells[(int)T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL.EMPLOYEE_NAME]);
                    strEmployeePAN = Convert.ToString(cells[(int)T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL.EMPLOYEE_PAN]);
                    strEmployeeCategory = Convert.ToString(cells[(int)T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL.EMPLOYEE_CATEGORY]);
                    strReferenceNo = Convert.ToString(cells[(int)T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL.REFERENCE_NO]);
                    strDesignation = Convert.ToString(cells[(int)T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL.DESIGNATION]);
                    // CHECK BLANK ROW TO EXIT
                    if (strEmployeeName == "" &&
                            strEmployeePAN == "" &&
                                strEmployeeCategory == "" &&
                                    strReferenceNo == "" &&
                                        strDesignation == "")
                        break;

                    cmnService.J_WriteLine(ref StreamWriterDeductee, TdsMan.T_WriteField(intLineNumber.ToString()) + TdsMan.T_WriteField("") +
                                                                     TdsMan.T_WriteField(strEmployeeName) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 1)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strEmployeePAN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 2)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strEmployeeCategory) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 3)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strReferenceNo) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 4)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strDesignation) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 5)) + (Convert.ToString(intLineNumber + 1))))); //+ (Convert.ToString(intLineNumber + 1)))));//);// + (Convert.ToString(intLineNumber + 1)))) +
                                                                     //TdsMan.T_WriteField(strAddress2) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 5)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     //TdsMan.T_WriteField(strAddress3) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 6)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     //TdsMan.T_WriteField(strAddress4) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 7)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     //TdsMan.T_WriteField(strAddress5) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 8)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     //TdsMan.T_WriteField(strState) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 9)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     //TdsMan.T_WriteField(strPIN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 10)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     //TdsMan.T_WriteField(strMobile) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 11)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     //TdsMan.T_WriteField(strEmail) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 12)) + (Convert.ToString(intLineNumber + 1)))) + TdsMan.T_WriteField("0"));
                }

                myDataSet.Dispose();
                myCommand.Dispose();

                StreamWriterDeductee.Flush();
                StreamWriterDeductee.Close();

                #endregion

                #region TRANSFERRING DATA FROM TEXT FILE TO ACCESS

                //Added by INDRAJIT on 16-03-2012
                
                //Array to hold data segment name
                tableName = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "";

                textfileName = "EmployeeMaster";

                TdsMan.T_ReplaceDoubleQuotesinFile(strTempEmployeeMasterPath , true);

                ImportTextToTables(tableName, textfileName, strTempEmployeeMasterPath ,false);

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

        #region GET_DATA_FROM_EXCEL_CSV_ACCESS
        private bool GET_DATA_FROM_EXCEL_CSV_ACCESS()
        {
            try
            {
                #region VARIABLE_DECLARATION
                // DEDUCTEE MASTER
                string strEmployeeName = "";
                string strEmployeePAN = "";
                string strEmployeeCategory = "";
                string strReferenceNo = "";
                string strDesignation = "";

                string strStartupPath = "";
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                    strStartupPath = Path.Combine(J_Var.J_pEnterpriseServerPath, "TMP FOLDER");
                else
                    strStartupPath = Path.Combine(Application.StartupPath, "TMP FOLDER");
                if (Directory.Exists(strStartupPath) == false)
                    // DELETE IF THE FILE EXISTS.
                    Directory.CreateDirectory(strStartupPath);
                //
                strTempEmployeeMasterPath = Path.Combine(Application.StartupPath, "EmployeeMaster.txt");
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                {
                    strTempEmployeeMasterPath = Path.Combine(strStartupPath, TDSMAN.Classes.TDSMAN.T_pProductSerial + "_EmployeeMaster.txt");
                    if (File.Exists(strTempEmployeeMasterPath) == true)
                        File.Delete(strTempEmployeeMasterPath);
                }

                string tableName = "";
                string textfileName = "";

                #endregion

                if (rbnExcel.Checked == true)
                {
                    #region EXCEL

                    #region INSERTING DATA TO TEXT FILE

                    if (KILL_EXCEL() == false)
                        return false;
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
                    myCommand = new OleDbDataAdapter("SELECT * FROM [" + T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER + "$]", con);
                    myCommand.Fill(myDataSet);
                    //con.Close();

                    long lngRow = 2;
                    long lngColumn = 0;

                    int intLineNumber = 0;

                    StreamWriter StreamWriterDeductee = cmnService.J_ReturnStreamWriter(strTempEmployeeMasterPath);

                    //Travers through each row in the dataset
                    foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
                    {
                        lblProgressDisplayMessage.Visible = true;
                        //lblProgressDisplayMessage.Text = "Transferring Data [" + T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER + " : " + lngRow + " ]";
                        //this.Refresh();
                        //////
                        //lngColumn =65;
                        ////Stores info in Datarow into an array
                        Object[] cells = myDataRow.ItemArray;

                        intLineNumber = intLineNumber + 1;
                        int intColumnValue = 64;
                        //
                        strEmployeeName = Convert.ToString(cells[(int)T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL.EMPLOYEE_NAME]);
                        strEmployeePAN = Convert.ToString(cells[(int)T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL.EMPLOYEE_PAN]);
                        strEmployeeCategory = Convert.ToString(cells[(int)T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL.EMPLOYEE_CATEGORY]);
                        strReferenceNo = Convert.ToString(cells[(int)T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL.REFERENCE_NO]);
                        strDesignation = Convert.ToString(cells[(int)T_GET_EMPLOYEE_MASTER_DATA_FROM_EXCEL.DESIGNATION]);
                        // CHECK BLANK ROW TO EXIT
                        if (strEmployeeName == "" &&
                                strEmployeePAN == "" &&
                                    strEmployeeCategory == "" &&
                                        strReferenceNo == "" &&
                                            strDesignation == "")
                            break;

                        cmnService.J_WriteLine(ref StreamWriterDeductee, TdsMan.T_WriteField(intLineNumber.ToString()) + TdsMan.T_WriteField("") +
                                                                         TdsMan.T_WriteField(strEmployeeName) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 1)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                         TdsMan.T_WriteField(strEmployeePAN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 2)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                         TdsMan.T_WriteField(strEmployeeCategory) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 3)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                         TdsMan.T_WriteField(strReferenceNo) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 4)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                         TdsMan.T_WriteField(strDesignation) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 5)) + (Convert.ToString(intLineNumber + 1))))); //+ (Convert.ToString(intLineNumber + 1)))));//);// + (Convert.ToString(intLineNumber + 1)))) +
                                                                                                                                                                                                                             //TdsMan.T_WriteField(strAddress2) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 5)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                                                                                                                                                                             //TdsMan.T_WriteField(strAddress3) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 6)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                                                                                                                                                                             //TdsMan.T_WriteField(strAddress4) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 7)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                                                                                                                                                                             //TdsMan.T_WriteField(strAddress5) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 8)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                                                                                                                                                                             //TdsMan.T_WriteField(strState) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 9)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                                                                                                                                                                             //TdsMan.T_WriteField(strPIN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 10)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                                                                                                                                                                             //TdsMan.T_WriteField(strMobile) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 11)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                                                                                                                                                                             //TdsMan.T_WriteField(strEmail) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 12)) + (Convert.ToString(intLineNumber + 1)))) + TdsMan.T_WriteField("0"));
                    }

                    myDataSet.Dispose();
                    myCommand.Dispose();

                    StreamWriterDeductee.Flush();
                    StreamWriterDeductee.Close();

                    #endregion

                    #region TRANSFERRING DATA FROM TEXT FILE TO ACCESS

                    //Added by INDRAJIT on 16-03-2012

                    //Array to hold data segment name
                    tableName = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "";

                    textfileName = "EmployeeMaster";

                    TdsMan.T_ReplaceDoubleQuotesinFile(strTempEmployeeMasterPath, true);

                    ImportTextToTables(tableName, textfileName, strTempEmployeeMasterPath, false);

                    // DELETE THE TXT FILE
                    if (File.Exists(strTempEmployeeMasterPath) == true)
                        File.Delete(strTempEmployeeMasterPath);

                    #endregion

                    myDataSet.Dispose();
                    myCommand.Dispose();

                    #endregion
                    //
                    con.Close();
                    con.Dispose();
                    //
                }
                else if (rbnCSV.Checked == true)
                {
                    tableName = TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE;

                    #region CSV 
                    if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    {
                        //COPY .TDS FILE AS .TXT FILE TO IMPORT DATA TO ACCESS
                        if (File.Exists(Application.StartupPath + "\\" + tableName + ".txt") == true)
                            File.Delete(Application.StartupPath + "\\" + tableName + ".txt");
                        //
                        File.Copy(txtExcelPath.Text, Application.StartupPath + "\\" + tableName + ".txt", true);
                        //
                        //-- Creation of dump table from .CSV file
                        if (T_BulkImportFromCSVFile(tableName, false) == false) return false;

                        //-- DELETE 1ST ROW-COLUMN NAME
                        strSQL = @"SELECT COUNT(*) FROM " + tableName + @"
                                WHERE  SERIAL_NO     = 'SERIAL NO'
                                AND    EMPLOYEE_NAME = 'EMPLOYEE NAME'
                                AND    EMPLOYEE_PAN  = 'EMPLOYEE PAN'
                                AND    EMPLOYEE_CATEGORY = 'EMPLOYEE CATEGORY'
                                AND    REFERENCE_NO      = 'REFERENCE NO'
                                AND    DESIGNATION       = 'DESIGNATION'";
                        if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) != 1)
                        {
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage("Selected file is invalid.\n" +
                                                        "Please get the latest Csv file.", MessageBoxIcon.Exclamation);
                            //--
                            prgBar.Value = 0;
                            btnSelectExcelPath.Select();
                            return false;
                        }
                        strSQL = @"DELETE FROM " + tableName + " WHERE SERIAL_NO = 'SERIAL NO'";
                        dmlService.J_ExecSql(strSQL);
                        //-- DELETE BLANK ROW
                        strSQL = @"DELETE FROM " + tableName + " WHERE SERIAL_NO IS NULL";
                        dmlService.J_ExecSql(strSQL);
                        //--
                    }
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    {
                        //tableName = TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS;
                        //COPY .TDS FILE AS .TXT FILE TO IMPORT DATA TO ACCESS
                        if (File.Exists(strStartupPath + "\\" + tableName + ".txt") == true)
                            File.Delete(strStartupPath + "\\" + tableName + ".txt");
                        //
                        File.Copy(txtExcelPath.Text, strStartupPath + "\\" + tableName + ".txt", true);
                        //
                        //Check 'n Create Temp Tables
                        if (dmlService.J_IsDatabaseObjectExist(tableName) == true)
                        {
                            strSQL = "DELETE FROM [" + tableName + "]";
                            dmlService.J_ExecSql(strSQL);
                        }
                        //--
                        if (Convert.ToString(File.ReadAllLines(tableName + ".txt")) != "")
                        {
                            //--
                            strSQL = @"BULK INSERT [" + tableName + "] FROM '" + strStartupPath + "\\" + tableName + ".txt" + "' WITH (fieldterminator = ',', rowterminator = '\\n')";
                            dmlService.J_ExecSql(strSQL);
                            //--
                            strSQL = @"SELECT COUNT(*) FROM " + tableName + @"
                                WHERE  SERIAL_NO     = 'SERIAL NO'
                                AND    EMPLOYEE_NAME = 'EMPLOYEE NAME'
                                AND    EMPLOYEE_PAN  = 'EMPLOYEE PAN'
                                AND    EMPLOYEE_CATEGORY = 'EMPLOYEE CATEGORY'
                                AND    REFERENCE_NO      = 'REFERENCE NO'
                                AND    DESIGNATION       = 'DESIGNATION'";
                            if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) != 1)
                            {
                                this.Cursor = Cursors.Default;
                                cmnService.J_UserMessage("Selected Csv file is invalid.\n" +
                                                         "Please get the latest Csv file.", MessageBoxIcon.Exclamation);
                                //--
                                prgBar.Value = 0;
                                btnSelectExcelPath.Select();
                                return false;
                            }
                            //
                            strSQL = @"DELETE FROM " + tableName + " WHERE SERIAL_NO = 'SERIAL NO'";
                            dmlService.J_ExecSql(strSQL);
                            //-- DELETE BLANK ROW
                            strSQL = @"DELETE FROM " + tableName + " WHERE SERIAL_NO IS NULL";
                            dmlService.J_ExecSql(strSQL);
                            //--
                        }
                        //
                    }
                    #endregion
                }
                return true;
                //############################################
            }
            catch
            {
                return false;
            }
        }
        #endregion


        #region T_BulkImportFromCSVFile
        private bool T_BulkImportFromCSVFile(string ImportTableName, bool FirstRowAsColumnHeader)
        {
            //Recreating schema file
            if (File.Exists(Application.StartupPath + "\\schema.ini") == true)
                File.Delete(Application.StartupPath + "\\schema.ini");

            StreamWriter StreamWriter = new StreamWriter(Application.StartupPath + "\\schema.ini");
            StreamWriter.WriteLine("[" + ImportTableName + ".txt]");
            StreamWriter.WriteLine("ColNameHeader=" + (FirstRowAsColumnHeader == true ? "True" : "False") + "");
            StreamWriter.WriteLine("Format=Delimited(,)");
            StreamWriter.WriteLine("MaxScanRows=0");
            StreamWriter.WriteLine("HDR = Yes");
            StreamWriter.WriteLine("CharacterSet=ANSI");
            //
            #region DEFINING THE COLUMN NAMES
            StreamWriter.WriteLine(@"Col1 = SERIAL_NO Char
                                    Col2 = EMPLOYEE_NAME Char
                                    Col3 = EMPLOYEE_PAN Char
                                    Col4 = EMPLOYEE_CATEGORY Char
                                    Col5 = REFERENCE_NO Char
                                    Col6 = DESIGNATION Char");

            #endregion
            //
            StreamWriter.Close();
            //
            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                //CREATING THE IMPORT TABLE
                if (dmlService.J_IsDatabaseObjectExist(ImportTableName) == true)
                {
                    strSQL = "DROP TABLE [" + ImportTableName + "]";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage("Import Failed!");
                        return false;
                    }
                }
            }

            //ADDED BY SHREY KEJRIWAL ON 24/05/2012
            //REPLACING DOUBLE QUOTES
            TdsMan.T_ReplaceDoubleQuotesinFile(Path.Combine(Application.StartupPath, ImportTableName + ".txt"), false);

            //QUERY TO IMPORT THE DATA FROM TEXT FILE
            strSQL = "SELECT * INTO [" + ImportTableName + "] FROM " +
                     "[Text; DATABASE=" + Application.StartupPath + "].[" + ImportTableName + ".txt" + "]";
            //
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage("Import Failed!");
                return false;
            }

            return true;
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
            if (txtfile == "EmployeeMaster")
            {
                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "
                StreamWriter.WriteLine(@"Col1=EMPLOYEE_ID Integer
                                         Col2=MODE Char
                                         Col3=EMPLOYEE_NAME Char
                                         Col4=EMPLOYEE_NAME_CELL Char
                                         Col5=EMPLOYEE_PAN Char
                                         Col6=EMPLOYEE_PAN_CELL Char
                                         Col7=EMPLOYEE_CATEGORY Char
                                         Col8=EMPLOYEE_CATEGORY_CELL Char
                                         Col9=REFERENCE_NO Char
                                         Col10=REFERENCE_NO_CELL Char
                                         Col11=DESIGNATION Char
                                         Col12=DESIGNATION_CELL Char");
                #endregion
            }

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
                if (rbnExcel.Checked == true)
                {
                    strSheetName = T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER;

                    #region EMPLOYEE PAN

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET EMPLOYEE_PAN = '' WHERE EMPLOYEE_PAN IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //BLANK OR 0 CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
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
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  EMPLOYEE_PAN = ''" +
                            "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
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
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  LEN(EMPLOYEE_PAN) <> 10" +
                            "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //PAN STRUCTURE CHECK
                    strSQL = "SELECT COUNT(*)" +
                     "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
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
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
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
                    // DUPLICATE CHECK
                    // WHEN EMPLOYEE_PAN <> 'PANNOTAVBL'
                    strSQL = "SELECT SUM(EMPLOYEE.NO_OF_ROWS) AS NO_OF_ROWS " +
                             "FROM  (SELECT EMPLOYEE_PAN," +
                             "              COUNT(EMPLOYEE_ID) AS NO_OF_ROWS " +
                             "       FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                             "       WHERE  EMPLOYEE_PAN     <> 'PANNOTAVBL' " +
                             "       AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                             "       GROUP BY EMPLOYEE_PAN " +
                             "       HAVING COUNT(EMPLOYEE_PAN) > 1) AS EMPLOYEE";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                    //
                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                            "             EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                            "             'EMPLOYEE_PAN_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.DUPLICATE_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  EMPLOYEE_PAN IN (SELECT EMPLOYEE_PAN " +
                            "                             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                            "                             WHERE  EMPLOYEE_PAN     <> 'PANNOTAVBL' " +
                            "                             AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                            "                             GROUP BY EMPLOYEE_PAN " +
                            "                             HAVING COUNT(EMPLOYEE_PAN) > 1)";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    #region EMPLOYEE NAME

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET EMPLOYEE_NAME = '' WHERE EMPLOYEE_NAME IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //BLANK OR 0 CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  EMPLOYEE_NAME = ''" +
                        "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                            "            EMPLOYEE_NAME_CELL AS ERROR_CELL," +
                            "            'EMPLOYEE_NAME_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  EMPLOYEE_NAME = ''" +
                            "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";

                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  LEN(EMPLOYEE_NAME) > 75" +
                        "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            EMPLOYEE_NAME_CELL AS ERROR_CELL," +
                            "            'EMPLOYEE_NAME_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  LEN(EMPLOYEE_NAME) > 75" +
                            "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //DUPLICATE CHECK
                    //WHEN EMPLOYEE_PAN = 'PANNOTAVBL'
                    strSQL = "SELECT COUNT(EMPLOYEE_PAN) " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                             "WHERE  EMPLOYEE_PAN     = 'PANNOTAVBL' " +
                             "AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                             "GROUP BY EMPLOYEE_NAME " +
                             "HAVING COUNT(EMPLOYEE_NAME) > 1";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                            "             EMPLOYEE_NAME_CELL AS ERROR_CELL," +
                            "             'EMPLOYEE_NAME_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.DUPLICATE_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  EMPLOYEE_NAME IN (SELECT EMPLOYEE_NAME " +
                            "                             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                            "                             WHERE  EMPLOYEE_PAN     = 'PANNOTAVBL' " +
                            "                             GROUP BY EMPLOYEE_NAME,EMPLOYEE_PAN  " +
                            "                             HAVING COUNT(EMPLOYEE_NAME) > 1) " +
                            "     AND    EMPLOYEE_PAN     = 'PANNOTAVBL' " +
                            "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    #region EMPLOYEE CATEGORY

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET EMPLOYEE_CATEGORY = '' WHERE EMPLOYEE_CATEGORY IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //BLANK OR 0 CHECK

                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  EMPLOYEE_CATEGORY = '' " +
                        "     AND    EMPLOYEE_CATEGORY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                            "             EMPLOYEE_CATEGORY_CELL AS ERROR_CELL," +
                            "             'EMPLOYEE_CATEGORY_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  EMPLOYEE_CATEGORY = ''" +
                            "     AND    EMPLOYEE_CATEGORY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    // VALIDITY CHECK

                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  " + strUpper + "(EMPLOYEE_CATEGORY) <> 'G'" +
                        "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'W'" +
                        "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'S'" +
                        "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'O'" +
                        "     AND    EMPLOYEE_CATEGORY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "             EMPLOYEE_CATEGORY_CELL AS ERROR_CELL," +
                            "             'EMPLOYEE_CATEGORY_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  " + strUpper + "(EMPLOYEE_CATEGORY) <> 'G'" +
                            "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'W'" +
                            "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'S'" +
                            "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'O'" +
                            "     AND    EMPLOYEE_CATEGORY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    #region REFERENCE

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET REFERENCE_NO = '' WHERE REFERENCE_NO IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  LEN(REFERENCE_NO) > 10" +
                        "     AND    REFERENCE_NO <> '' " +
                        "     AND    REFERENCE_NO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            REFERENCE_NO_CELL AS ERROR_CELL," +
                            "            'REFERENCE_NO_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  LEN(REFERENCE_NO) > 10" +
                            "     AND    REFERENCE_NO <> '' " +
                            "     AND    REFERENCE_NO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    #region DESIGNATION

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET DESIGNATION = '' WHERE DESIGNATION IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  LEN(DESIGNATION) > 75" +
                        "     AND    DESIGNATION <> '' " +
                        "     AND    DESIGNATION_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            DESIGNATION_CELL AS ERROR_CELL," +
                            "            'DESIGNATION_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  LEN(DESIGNATION) > 75" +
                            "     AND    DESIGNATION <> '' " +
                            "     AND    DESIGNATION_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion
                }
                else if (rbnCSV.Checked == true)
                {
                    strSheetName = T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER;

                    #region EMPLOYEE PAN

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET EMPLOYEE_PAN = '' WHERE EMPLOYEE_PAN IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //BLANK OR 0 CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  EMPLOYEE_PAN = ''" +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'EMPLOYEE_PAN'," +
                            "             '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  EMPLOYEE_PAN = ''" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  LEN(EMPLOYEE_PAN) <> 10" +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'EMPLOYEE_PAN'," +
                            "             '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  LEN(EMPLOYEE_PAN) <> 10" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //PAN STRUCTURE CHECK
                    strSQL = "SELECT COUNT(*)" +
                     "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
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
                     "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'EMPLOYEE_PAN'," +
                            "             '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
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
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    // DUPLICATE CHECK
                    // WHEN EMPLOYEE_PAN <> 'PANNOTAVBL'
                    strSQL = "SELECT SUM(EMPLOYEE.NO_OF_ROWS) AS NO_OF_ROWS " +
                             "FROM  (SELECT EMPLOYEE_PAN," +
                             "              COUNT(SERIAL_NO) AS NO_OF_ROWS " +
                             "       FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                             "       WHERE  EMPLOYEE_PAN     <> 'PANNOTAVBL' " +
                             "       AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                             "       GROUP BY EMPLOYEE_PAN " +
                             "       HAVING COUNT(EMPLOYEE_PAN) > 1) AS EMPLOYEE";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                    //
                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'EMPLOYEE_PAN'," +
                            "             '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  EMPLOYEE_PAN IN (SELECT EMPLOYEE_PAN " +
                            "                             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                            "                             WHERE  EMPLOYEE_PAN     <> 'PANNOTAVBL' " +
                            "                             AND    EMPLOYEE_PAN NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                            "                             GROUP BY EMPLOYEE_PAN " +
                            "                             HAVING COUNT(EMPLOYEE_PAN) > 1)";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    #region EMPLOYEE NAME

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET EMPLOYEE_NAME = '' WHERE EMPLOYEE_NAME IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //BLANK OR 0 CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  EMPLOYEE_NAME = ''" +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                            "            SERIAL_NO AS ERR_ROW," +
                            "            'EMPLOYEE_NAME'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  EMPLOYEE_NAME = ''" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";

                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  LEN(EMPLOYEE_NAME) > 75" +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            SERIAL_NO AS ERR_ROW," +
                            "            'EMPLOYEE_NAME'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  LEN(EMPLOYEE_NAME) > 75" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //DUPLICATE CHECK
                    //WHEN EMPLOYEE_PAN = 'PANNOTAVBL'
                    strSQL = "SELECT COUNT(EMPLOYEE_PAN) " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                             "WHERE  EMPLOYEE_PAN     = 'PANNOTAVBL' " +
                             "AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                             "GROUP BY EMPLOYEE_NAME " +
                             "HAVING COUNT(EMPLOYEE_NAME) > 1";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'EMPLOYEE_NAME'," +
                            "             '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  EMPLOYEE_NAME IN (SELECT EMPLOYEE_NAME " +
                            "                             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                            "                             WHERE  EMPLOYEE_PAN     = 'PANNOTAVBL' " +
                            "                             GROUP BY EMPLOYEE_NAME,EMPLOYEE_PAN  " +
                            "                             HAVING COUNT(EMPLOYEE_NAME) > 1) " +
                            "     AND    EMPLOYEE_PAN     = 'PANNOTAVBL' " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    #region EMPLOYEE CATEGORY

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET EMPLOYEE_CATEGORY = '' WHERE EMPLOYEE_CATEGORY IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //BLANK OR 0 CHECK

                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  EMPLOYEE_CATEGORY = '' " +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'EMPLOYEE_CATEGORY'," +
                            "             '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  EMPLOYEE_CATEGORY = ''" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    // VALIDITY CHECK

                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  " + strUpper + "(EMPLOYEE_CATEGORY) <> 'G'" +
                        "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'W'" +
                        "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'S'" +
                        "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'O'" +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'EMPLOYEE_CATEGORY'," +
                            "             '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  " + strUpper + "(EMPLOYEE_CATEGORY) <> 'G'" +
                            "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'W'" +
                            "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'S'" +
                            "     AND    " + strUpper + "(EMPLOYEE_CATEGORY) <> 'O'" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    #region REFERENCE

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET REFERENCE_NO = '' WHERE REFERENCE_NO IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  LEN(REFERENCE_NO) > 10" +
                        "     AND    REFERENCE_NO <> '' " +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            SERIAL_NO AS ERR_ROW," +
                            "            'REFERENCE_NO'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  LEN(REFERENCE_NO) > 10" +
                            "     AND    REFERENCE_NO <> '' " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    #region DESIGNATION

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET DESIGNATION = '' WHERE DESIGNATION IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "     WHERE  LEN(DESIGNATION) > 75" +
                        "     AND    DESIGNATION <> '' " +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            SERIAL_NO AS ERR_ROW," +
                            "            'DESIGNATION'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                            "     WHERE  LEN(DESIGNATION) > 75" +
                            "     AND    DESIGNATION <> '' " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion
                }
                return true;
            }
            catch(Exception e)
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
            string strMatchSheetName = T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER;
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
            long lngTotalRecords = 0, lngExistingRecords = 0;
            string strTableID = "";
            try
            {
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""))) == 0)
                {
                    //--
                    blnOpenTabPage = true;
                    tbcExcelImport.TabPages.Remove(tbpValidateExcelFile);
                    //
                    if (rbnExcel.Checked == true)
                    {
                        strTableID = "EMPLOYEE_ID";
                    }
                    else if (rbnCSV.Checked == true)
                    {
                        strTableID = "SERIAL_NO";
                    }
                    //-- EXISTING RECORDS NOT TO BE UPDATED
                    lngTotalRecords = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(EMPLOYEE_ID) AS EXISTING_DEDUCTEES FROM MST_EMPLOYEE WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)))));
                    lngExistingRecords = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(" + strTableID + ") AS UPDATE_DEDUCTEE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " WHERE MODE ='" + T_Deductee_Master_Updt_Tag.UPDATE + "'")));
                    //-- UPDATED RECORDS
                    if (chkUpdate.Checked == true)
                    {
                        lblExistingDeductee.Text = "Existing Employee Record (to be updated)";
                        //
                        lblExistingDeducteeNoChange.Text = Convert.ToString(lngTotalRecords - lngExistingRecords);
                    }
                    else
                    {
                        lblExistingDeductee.Text = "Existing Employee Record (not to be updated)";
                        //
                        lblExistingDeducteeNoChange.Text = Convert.ToString(lngTotalRecords);
                    }
                    //
                    lblDeducteesUpdated.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(" + strTableID + ") AS UPDATE_EMPLOYEE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " WHERE MODE ='" + T_Deductee_Master_Updt_Tag.UPDATE + "'"));
                    //-- NEW RECORDS
                    lblNewRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(" + strTableID + ") AS NEW_EMPLOYEE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " WHERE MODE ='" + T_Deductee_Master_Updt_Tag.NEW + "'"));
                    //--
                    txtCompany.Text = cmbCompany.Text;
                    lblCompanyID.Text = Convert.ToString(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
                    //--
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region LoadDeducteeMasterGrid
        private void LoadDeducteeMasterGrid()
        {
            string strUpper = "", strTableID = ""; ;
            //--
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                strUpper = "UPPER";
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                strUpper = "UCASE";
            }
            //
            if (rbnExcel.Checked == true)
            {
                tbcExcelImport.TabPages.Remove(tbpValidateExcelFile);
                strTableID = "EMPLOYEE_ID";
            }
            else if (rbnCSV.Checked == true)
            {
                strTableID = "SERIAL_NO";
            }
            //-----------------------------------------------------------
            string[,] strMatrixChallanDetails = {{"DEDUCTEE_ID", "0", "", "Right", "", "", ""},
                                        {"MODE", "80", "S", "", "", "", "T"},
                                        {"EMPLOYEE NAME", "280", "S", "", "", "", "T"},
                                        {"PAN", "100", "S", "", "", "", "T"},
                                        {"CATEGORY", "130", "S", "", "", "", "T"},
                                        {"DESIGNATION", "200", "0", "", "", "", "T"},
                                        {"REFERENCE NO.", "100", "0", "", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            string[,] strShowHelpEmployeeMatrix = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
                                                   {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
                                                   {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
                                                   {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
                                                   {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_CATEGORY = ''", "F", "", "T"}};
            
            strOrderBy = "EMPLOYEE_NAME, EMPLOYEE_PAN";
            strQuery = "SELECT " + strTableID + "," +
                      "        MODE," +
                      "        " + strUpper + "(EMPLOYEE_NAME)," +
                      "        " + strUpper + "(EMPLOYEE_PAN)," +
                      "        " + cmnService.J_SQLDBFormat(strShowHelpEmployeeMatrix, J_SQLColFormat.Case_End) + " AS CATEGORY," +
                      "        " + strUpper + "(DESIGNATION)," +
                      "        " + strUpper + "(REFERENCE_NO)" +
                      " FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQLGridViewTabPages, strMatrixChallanDetails);       //Show Data into the Grid                
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQL, strMatrixChallanDetails);       //Show Data into the Grid
        }
        #endregion
       
        #region INSERT_EMPLOYEE_DATA
        private bool INSERT_EMPLOYEE_DATA()
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
            //UPDATE STATE CODE ON TEMP MASTER TABLE
            //strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
            //    "     INNER JOIN MST_STATE " +
            //    "           ON  TEMP_MST_DEDUCTEE.STATE = MST_STATE.STATE_NAME " +
            //    "     SET  TEMP_MST_DEDUCTEE.STATE_ID   = MST_STATE.STATE_ID " +
            //    "     WHERE  TEMP_MST_DEDUCTEE.STATE <> ''";

            //if (dmlService.J_ExecSql(strSQL) == false)
            //    return false;
            //COUNT THE NEW DEDUCTEES
            int intNewDeductees = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " WHERE MODE = '" + T_Deductee_Master_Updt_Tag.NEW + "'"));
            //
            //INSERT NEW DEDUCTEES
            if (intNewDeductees > 0)
            {
                strSQL = "INSERT INTO MST_EMPLOYEE(EMPLOYEE_PAN," +
                "                                  EMPLOYEE_NAME," +
                "                                  COMPANY_ID," +
                "                                  CATEGORY," +
                "                                  DESIGNATION," +
                "                                  EMPLOYEE_REF)" +
                "        SELECT " + strUpper + "(EMPLOYEE_PAN)," +
                "               " + strUpper + "(EMPLOYEE_NAME)," +
                "               " + cmnService.J_ReturnInt32Value(lblCompanyID.Text) + "," +
                "               " + strUpper + "(EMPLOYEE_CATEGORY)," +
                "               " + strUpper + "(DESIGNATION)," +
                "               " + strUpper + "(REFERENCE_NO)" +
                "        FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                "        WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".MODE = '" + T_Deductee_Master_Updt_Tag.NEW + "'";
                //}
                if (dmlService.J_ExecSql(strSQL) == false)
                    return false;
            }

            // UPDATE EXISTING
            if (chkUpdate.Checked == true)
            {
                int intExistingEmployees = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " WHERE MODE = '" + T_Deductee_Master_Updt_Tag.UPDATE + "'"));
                //UPDATE EMPLOYEE RECORDS PRESENT IN MASTER
                if (intExistingEmployees > 0)
                {
                    strSQL = "UPDATE MST_EMPLOYEE" +
                        "     INNER JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "         ON MST_EMPLOYEE.EMPLOYEE_PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_PAN" +
                        "     SET   MST_EMPLOYEE.CATEGORY      = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_CATEGORY," +
                        "           MST_EMPLOYEE.DESIGNATION   = " + strUpper + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".DESIGNATION)," +
                        "           MST_EMPLOYEE.EMPLOYEE_REF  = " + strUpper + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".REFERENCE_NO)" +
                        "     WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".MODE     = '" + T_Deductee_Master_Updt_Tag.UPDATE + "'" +
                        "     AND   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_PAN <> 'PANNOTAVBL'" +
                        "     AND   MST_EMPLOYEE.COMPANY_ID        = " + cmnService.J_ReturnInt32Value(lblCompanyID.Text);

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                    //--
                    strSQL = "UPDATE MST_EMPLOYEE" +
                        "     INNER JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + "" +
                        "         ON MST_EMPLOYEE.EMPLOYEE_PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_PAN" +
                        "     SET   MST_EMPLOYEE.CATEGORY      = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_CATEGORY," +
                        "           MST_EMPLOYEE.DESIGNATION   = " + strUpper + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".DESIGNATION)," +
                        "           MST_EMPLOYEE.EMPLOYEE_REF  = " + strUpper + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".REFERENCE_NO)" +
                        "     WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".MODE     = '" + T_Deductee_Master_Updt_Tag.UPDATE + "'" +
                        "     AND   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_PAN = 'PANNOTAVBL'" +
                        "     AND   MST_EMPLOYEE.EMPLOYEE_PAN      = 'PANNOTAVBL'" +
                        "     AND   MST_EMPLOYEE.COMPANY_ID        = " + cmnService.J_ReturnInt32Value(lblCompanyID.Text);

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
            }
            return true;
        }
        #endregion

        #region UPDT_MODE
        private bool UPDT_MODE(long CompanyId)
        {
            #region UPDATE MODE = NEW

            if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE, "MODE") == false)
            {
                //strSQL = "ALTER TABLE MST_SETUP ADD COLUMN ENABLE_REFERENCE_NO NUMBER NOT NULL DEFAULT 0";
                strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE, "MODE", "TEXT", "10", "NOT NULL", "\"\"");
                dmlService.J_ExecSql(strSQL);
                strSQL = "UPDATE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET MODE = ''";
                dmlService.J_ExecSql(strSQL);
            }
            //
            dmlService.J_ExecSql("UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET MODE = '" + T_Deductee_Master_Updt_Tag.NEW + "' WHERE MODE IS NULL");
            dmlService.J_ExecSql("UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " SET MODE = '" + T_Deductee_Master_Updt_Tag.NEW + "' WHERE MODE = ''");
            //

            //FOR VALID PAN
            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                "     LEFT JOIN MST_EMPLOYEE " +
                "     ON    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_PAN = MST_EMPLOYEE.EMPLOYEE_PAN " +
                "     SET   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".MODE         = '" + T_Deductee_Master_Updt_Tag.NEW + "'" +
                "     WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_PAN <> 'PANNOTAVBL' " +
                "     AND   MST_EMPLOYEE.COMPANY_ID        = " + CompanyId + " " +
                "     AND   MST_EMPLOYEE.EMPLOYEE_PAN IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                return false;
            }
            //strSQL = "UPDATE TEMP_SALARY_DETAILS " +
            //          "         INNER JOIN MST_EMPLOYEE " +
            //          "         ON TEMP_SALARY_DETAILS.EMPLOYEE_PAN = MST_EMPLOYEE.EMPLOYEE_PAN  " +
            //          "  SET    TEMP_SALARY_DETAILS.EMPLOYEE_ID     = MST_EMPLOYEE.EMPLOYEE_ID " +
            //          "  WHERE  TEMP_SALARY_DETAILS.EMPLOYEE_PAN    <> 'PANNOTAVBL'" +
            //          "  AND    MST_EMPLOYEE.COMPANY_ID             = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

            //FOR PANNOTAVBL
            //strSQL = "UPDATE TEMP_MST_DEDUCTEE " +
                //"     SET MODE = '" + T_Deductee_Master_Updt_Tag.NEW + "'" +
                //"     WHERE DEDUCTEE_NAME NOT IN (SELECT DEDUCTEE_NAME FROM MST_DEDUCTEE WHERE DEDUCTEE_PAN = 'PANNOTAVBL')" +
                //"     AND   DEDUCTEE_PAN = 'PANNOTAVBL'";
            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                "     LEFT JOIN MST_EMPLOYEE " +
                "     ON    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_NAME = MST_EMPLOYEE.EMPLOYEE_NAME " +
                "     SET   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".MODE         ='" + T_Deductee_Master_Updt_Tag.NEW + "'" +
                "     WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_PAN ='PANNOTAVBL'" +
                "     AND   MST_EMPLOYEE.COMPANY_ID        = " + CompanyId + " " +
                "     AND   MST_EMPLOYEE.EMPLOYEE_NAME IS NULL";
            
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                return false;
            }
            #endregion

            #region UPDATE MODE = UPDATE

            //FOR VALID PAN
            //strSQL = "UPDATE TEMP_MST_DEDUCTEE " +
            //    "     SET MODE = '" + T_Deductee_Master_Updt_Tag.UPDATE + "'" +
            //    "     WHERE DEDUCTEE_PAN IN (SELECT DEDUCTEE_PAN FROM MST_DEDUCTEE WHERE DEDUCTEE_PAN <> 'PANNOTAVBL')" +
            //    "     AND   DEDUCTEE_PAN <> 'PANNOTAVBL'";
            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                "     LEFT JOIN MST_EMPLOYEE " +
                "     ON    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_PAN = MST_EMPLOYEE.EMPLOYEE_PAN " +
                "     SET   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".MODE         = '" + T_Deductee_Master_Updt_Tag.UPDATE + "'" +
                "     WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_PAN <> 'PANNOTAVBL'" +
                "     AND   MST_EMPLOYEE.COMPANY_ID        =" + CompanyId + " " +
                "     AND   MST_EMPLOYEE.EMPLOYEE_PAN IS NOT NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                return false;
            }

            //FOR PANNOTAVBL
            //strSQL = "UPDATE TEMP_MST_DEDUCTEE " +
            //    "     SET MODE = '" + T_Deductee_Master_Updt_Tag.UPDATE + "'" +
            //    "     WHERE DEDUCTEE_NAME IN (SELECT DEDUCTEE_NAME FROM MST_DEDUCTEE WHERE DEDUCTEE_PAN = 'PANNOTAVBL')" +
            //    "     AND   DEDUCTEE_PAN = 'PANNOTAVBL'";
            strSQL = "UPDATE    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " " +
                "     LEFT JOIN MST_EMPLOYEE " +
                "     ON    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_NAME = MST_EMPLOYEE.EMPLOYEE_NAME " +
                "     SET   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".MODE         = '" + T_Deductee_Master_Updt_Tag.UPDATE + "'" +
                "     WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + ".EMPLOYEE_PAN = 'PANNOTAVBL'" +
                "     AND   MST_EMPLOYEE.COMPANY_ID        =" + CompanyId + " " +
                "     AND MST_EMPLOYEE.EMPLOYEE_NAME IS NOT NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                return false;
            }
            #endregion
            //
            if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_EMPLOYEE + " WHERE MODE = ''")) == 0)
                return true;
            else
                return false;
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
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER, "EMPLOYEE NAME", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER, "EMPLOYEE PAN", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER, "EMPLOYEE CATEGORY", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER, "REFERENCE NO", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER, "DESIGNATION", con) == false)
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

        #region rbnExcelCSV_CheckedChanged
        private void rbnExcelCSV_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnExcel.Checked == true)
            {
                lnkCSVBlankFormat.Visible = false;
                chkColorCodingExcelsheet.Visible = true;
                lblCSVMessage.Visible = false;
            }
            else if (rbnCSV.Checked == true)
            {
                lnkCSVBlankFormat.Visible = true;
                chkColorCodingExcelsheet.Visible = false;
                lblCSVMessage.Visible = true ;
                chkColorCodingExcelsheet.Checked = false;
            }
        }


        #endregion
        
        #region lnkCSVBlankFormat_LinkClicked
        private void lnkCSVBlankFormat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string CSVFolderName = "", CSVFileName = "";
            try
            {

                string strCSVPath = cmnService.J_OpenFolderDialog("Select Destination Folder");
                //
                if (strCSVPath == "") return;
                //--
                int intIncrement = 0;
                //--
                this.Cursor = Cursors.WaitCursor;
                //--
                CSVFileName = "CSV_EMPLOYEE_MASTER.zip";
                CSVFolderName = "CSV_EMPLOYEE_MASTER";
                do
                {
                    if (cmnService.J_IsFileExist(Path.Combine(strCSVPath, CSVFileName)) == true)
                    {
                        intIncrement++;
                        CSVFileName = cmnService.J_Mid(CSVFileName, 0, CSVFileName.Length - 4) + " (" + intIncrement + ").zip";  //-- 12/11/2018 --
                    }
                } while (cmnService.J_IsFileExist(Path.Combine(strCSVPath, CSVFileName)) == true);
                //--
                using (WebClient wc = new WebClient())
                    wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_EMPLOYEE_MASTER.zip", Path.Combine(strCSVPath, CSVFileName));
                //--
                System.Threading.Thread.Sleep(300);
                //--
                if (Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)) == true)
                {
                    System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFileName));
                }
                else
                {
                    J_UnZipString(Path.Combine(strCSVPath, CSVFileName));
                    System.Threading.Thread.Sleep(300);
                    if (File.Exists(Path.Combine(strCSVPath, CSVFileName)) == true)
                    {
                        File.Delete(Path.Combine(strCSVPath, CSVFileName));
                    }
                    System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFolderName));
                }
                //--
                this.Cursor = Cursors.Default;
            }
            catch (Exception err)
            {

                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region UNZIP FILE AND RETURN STRING VALUE [ OVERLOADED METHOD ]

        #region J_UnZipString [1]
        public string J_UnZipString(string FilePath)
        {
            return this.J_UnZipString(FilePath, "");
        }
        #endregion

        #region J_UnZipString
        public string J_UnZipString(string FilePath, string password)
        {
            int iCounter = 0;
            string strFolder = string.Empty;

            try
            {
                string strFolderPath = cmnService.J_GetDirectoryName(FilePath);
                string strFileName = cmnService.J_GetFileName(FilePath);

                ICSharpCode.SharpZipLib.Zip.ZipEntry theEntry;
                //ZipEntry theEntry;
                string tmpEntry = String.Empty;
                ZipInputStream s = new ZipInputStream(File.OpenRead(FilePath));

                if (password != null && password != String.Empty)
                    s.Password = password;

                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory 
                    if (strFolderPath != "")
                        Directory.CreateDirectory(strFolderPath);

                    if (fileName != String.Empty)
                    {
                        if (theEntry.Name.IndexOf(".ini") < 0)
                        {
                            string fullPath = strFolderPath + "\\" + theEntry.Name;
                            fullPath = fullPath.Replace("\\ ", "\\");
                            string fullDirPath = Path.GetDirectoryName(fullPath);

                            if (iCounter == 0)
                            {
                                strFolder = fullDirPath;
                                iCounter = 1;
                            }

                            if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);

                            FileStream streamWriter = File.Create(fullPath);
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                    streamWriter.Write(data, 0, size);
                                else
                                    break;
                            }
                            streamWriter.Close();
                        }
                    }
                }
                s.Close();
                return strFolder;
            }
            catch (Exception exception)
            {
                cmnService.J_UserMessage(exception.Message);
                return strFolder;
            }
        }
        #endregion

        #endregion

        #region WRITE ERROR HTML
        private bool WRITE_ERROR_HTML(string ExcelFilePath)
        {
            IDataReader drdGetRecord = null;
            //--
            try
            {
                //Table start.
                string html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:arial'>";

                //Adding HeaderRow.
                html += "<tr>";
                html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Error Row</th>";
                //html += "</tr>";
                html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Error Column</th>";
                //html += "</tr>";
                html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Error Type</th>";
                html += "</tr>";

                strSQL = @"SELECT ERR_ROW, ERR_COLUMN, ERR_TYPE, ERR_SHEET FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " ORDER BY ERR_VALIDATION_ID ";
                drdGetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdGetRecord == null)
                {
                    return false;
                }
                //int i= 0;
                while (drdGetRecord.Read())
                {
                    //Adding DataRow.
                    html += "<tr>";
                    html += "<td style='width:120px;border: 1px solid #ccc'>" + Convert.ToString(drdGetRecord.GetValue(0)) + " - " + Convert.ToString(drdGetRecord.GetValue(3)) + "</td>";
                    //html += "</tr>";
                    //
                    //html += "<tr>";
                    html += "<td style='width:120px;border: 1px solid #ccc'>" + Convert.ToString(drdGetRecord.GetValue(1)) + "</td>";
                    //html += "</tr>";
                    //
                    //html += "<tr>";
                    html += "<td style='width:120px;border: 1px solid #ccc'>" + Convert.ToString(drdGetRecord.GetValue(2)) + "</td>";
                    html += "</tr>";
                    //i = i++;
                    //if (i == 2) i = 0;
                }
                drdGetRecord.Close();
                drdGetRecord.Dispose();
                //Table end.
                html += "</table>";
                //
                File.WriteAllText(Path.Combine(Path.GetDirectoryName(ExcelFilePath), Path.GetFileNameWithoutExtension(ExcelFilePath) + ".htm"), html);
                //--
                return true;
            }
            catch (Exception ERR)
            {
                return false;
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

