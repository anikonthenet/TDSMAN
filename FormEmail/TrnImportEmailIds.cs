
#region Refered Namespaces & Classes
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

using System.Data.OleDb;
//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
//--
//using Excel = Microsoft.Office.Interop.Excel.Worksheet;
using Excel = Microsoft.Office.Interop.Excel;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;
#endregion

namespace TDSMAN.FormTrn
{
    #region T_Party_Sheet_Name
    public struct T_Party_Sheet_Name
    {
        public const string VALIDATION_ERROR_DETAILS = "Validation Error Details";
        public const string PARTY_MASTER = "Party Master";
    }
    #endregion

    public partial class TrnImportEmailIds : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnImportEmailIds()
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
        ExcelService ExcelService = new ExcelService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        string strExcelFolder = "";
        string ExcelFileName = "";

        //string strSourcePath = Path.Combine(Application.StartupPath, "BLANK EXCEL FILE FORMAT");

        string strSourceFile = "";
        string strDestFile = "";
        //--
        string strSQL = "";
        string strOrderBy = "";
        string strQuery = "";
        //--
        string strCompanyMaster = "Company Master";
        //string strDeducteeMaster = "Deductee Master";
        string strEmployeeMaster = "Party Master";
        string strSalaryDetail = "Salary Details";
        //
        string strTempPartyMasterPath = "";
        bool blnUpdateTextfileName = true;
        //
        string strExcelPathEmployee = "";
        string strExcelPathDeductee = "";

        int intTotalErrors = 0;
        bool blnOpenTabPage = false;
        //
        string strMidSubString = "";
        string strUpper = "";
        string strLower = "";
        DataSet dsetGridClone = new DataSet();
        bool blnExcel = false; string strCSVFilePath = "";
        #endregion

        #region T_GET_PARTY_MASTER_DATA_FROM_EXCEL

        public enum T_GET_PARTY_MASTER_DATA_FROM_EXCEL
        {
            PARTY_PAN = 0,
            PARTY_NAME = 1,
            PARTY_EMAIL = 2
        }

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

        #region TrnImportEmailIds_Load

        private void TrnImportEmailIds_Load(object sender, EventArgs e)
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
                //cmnService.J_StatusButton(this, lblMode.Text);
                //-----------------------------------------------------------
                //DisableControls();
                //-----------------------------------------------------------
                //ViewGrid.Height = 518;
                //
                //ControlVisible(false);
                //ClearControls();
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
                //-----------------------------------------------------------
                lblTitle.Text = "Import email id";
                //-----------------------------------------------------------
                //ViewGrid_Click(sender, e);
                //Added by Indrajit on 12-02-2013
                //Starting the timer
                //tmrGridRefresh.Start();
                BtnSave.Tag = T_SaveTag.VALIDATE;
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    strMidSubString = "SUBSTRING";
                    strUpper = "UPPER";
                    strLower = "LOWER";
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    strMidSubString = "MID";
                    strUpper = "UCASE";
                    strLower = "LCASE";
                }
                //--------------------------------------------                
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }

        #endregion



        #region btnGetExcelFileEmployee_Click
        private void btnGetExcelFileEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbnEmployeeExcel.Checked == true)
                {
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    {
                        string strExcelFolder = cmnService.J_OpenFolderDialog("Select Destination Folder");

                        if (strExcelFolder == "") return;

                        string strSourcePath = strExcelFolder;
                        string strExcelFileName = "eMailId_Employee.xlsx";
                        //--
                        //--SOURCE FILE 
                        strSourceFile = Path.Combine(strSourcePath, strExcelFileName);
                        //-- MessageBox.Show("2");
                        //--DESTINATION FILE
                        //strDestFile = Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text);
                        //-- MessageBox.Show("3");
                        //
                        CreateEmployeeMaster(strSourceFile);
                    }
                }
                else if (rbnEmployeeCSV.Checked == true)
                {
                    string strCSVPath = cmnService.J_OpenFolderDialog("Select Destination Folder");
                    //
                    if (strCSVPath == "") return;
                    //--
                    int intIncrement = 0;
                    //--
                    this.Cursor = Cursors.WaitCursor;
                    //--
                    string CSVFileName = "CSV_eMailId_Employee.csv";
                    string CSVFolderName = "CSV_eMailId_Employee";
                    do
                    {
                        if (cmnService.J_IsFileExist(Path.Combine(strCSVPath, CSVFileName)) == true)
                        {
                            intIncrement++;
                            CSVFileName = cmnService.J_Mid(CSVFileName, 0, CSVFileName.Length - 4) + " (" + intIncrement + ").csv";  //-- 12/11/2018 --
                        }
                    } while (cmnService.J_IsFileExist(Path.Combine(strCSVPath, CSVFileName)) == true);
                    //--
                    using (WebClient wc = new WebClient())
                        wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_eMailId_Employee.csv", Path.Combine(strCSVPath, CSVFileName));
                    //--
                    System.Threading.Thread.Sleep(300);
                    //--
                    if (File.Exists(Path.Combine(strCSVPath, CSVFileName)) == true)
                    {
                        System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFileName));
                    }
                    else
                    {
                        cmnService.J_UserMessage("File not found");
                    }
                    //--
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception err)
            {

                cmnService.J_UserMessage("File creation failed");
                //cmnService.J_UserMessage(err.Message);
                //--
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region btnGetExcelFileEmployee_MouseClick
        private void btnGetExcelFileEmployee_MouseClick(object sender, MouseEventArgs e)
        {
            if (J_Var.J_pDatabaseType != J_DatabaseType.SqlServer)
            {
                if (e.Button == MouseButtons.Left)
                    cntxtMnuStrpSelectExcel.Show(btnGetExcelFileEmployee, new Point(e.X, e.Y));
            }
        }
        #endregion

        #region tlStrpXls_Click
        private void tlStrpXls_Click(object sender, EventArgs e)
        {
            string strExcelFolder = cmnService.J_OpenFolderDialog("Select Destination Folder to create .xls file.");

            if (strExcelFolder == "") return;

            string strSourcePath = strExcelFolder;
            //
            if (rbnImportEmployee.Checked == true)
            {
                string strExcelFileName = "eMailId_Employee.xls";
                //--
                //--SOURCE FILE 
                strSourceFile = Path.Combine(strSourcePath, strExcelFileName);
                //-- MessageBox.Show("2");
                //--DESTINATION FILE
                //strDestFile = Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text);
                //-- MessageBox.Show("3");
                //
                CreateEmployeeMaster(strSourceFile);
            }
            else if (rbnImportDeductee.Checked == true)
            {
                string strExcelFileName = "eMailId_Deductee.xls";
                //--
                //--SOURCE FILE 
                strSourceFile = Path.Combine(strSourcePath, strExcelFileName);
                //-- MessageBox.Show("2");
                //--DESTINATION FILE
                //strDestFile = Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text);
                //-- MessageBox.Show("3");
                //
                CreateDeducteeMaster(strSourceFile);
            }
        }
        #endregion

        #region tlStrpXlsx_Click
        private void tlStrpXlsx_Click(object sender, EventArgs e)
        {
            string strExcelFolder = cmnService.J_OpenFolderDialog("Select Destination Folder to create .xlsx file.");

            if (strExcelFolder == "") return;

            string strSourcePath = strExcelFolder;
            if (rbnImportEmployee.Checked == true)
            {
                string strExcelFileName = "eMailId_Employee.xlsx";
                //--
                //--SOURCE FILE 
                strSourceFile = Path.Combine(strSourcePath, strExcelFileName);
                //-- MessageBox.Show("2");
                //--DESTINATION FILE
                //strDestFile = Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text);
                //-- MessageBox.Show("3");
                //
                CreateEmployeeMaster(strSourceFile);
            }
            else if (rbnImportDeductee.Checked == true)
            {
                string strExcelFileName = "eMailId_Deductee.xlsx";
                //--
                //--SOURCE FILE 
                strSourceFile = Path.Combine(strSourcePath, strExcelFileName);
                //-- MessageBox.Show("2");
                //--DESTINATION FILE
                //strDestFile = Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text);
                //-- MessageBox.Show("3");
                //
                CreateDeducteeMaster(strSourceFile);
                //
            }
        }
        #endregion



        #region btnGetExcelFileDeductee_Click
        private void btnGetExcelFileDeductee_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbnDeducteeExcel.Checked == true)
                {
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    {
                        string strExcelFolder = cmnService.J_OpenFolderDialog("Select Destination Folder");

                        if (strExcelFolder == "") return;

                        string strSourcePath = strExcelFolder;
                        string strExcelFileName = "eMailId_Deductee.xlsx";
                        //
                        //--SOURCE FILE 
                        strSourceFile = Path.Combine(strSourcePath, strExcelFileName);
                        //
                        CreateDeducteeMaster(strSourceFile);
                    }
                }
                else if (rbnDeducteeCSV.Checked == true)
                {
                    string strCSVPath = cmnService.J_OpenFolderDialog("Select Destination Folder");
                    //
                    if (strCSVPath == "") return;
                    //--
                    int intIncrement = 0;
                    //--
                    this.Cursor = Cursors.WaitCursor;
                    //--
                    string CSVFileName = "CSV_eMailId_Deductee.csv";
                    string CSVFolderName = "CSV_eMailId_Deductee";
                    do
                    {
                        if (cmnService.J_IsFileExist(Path.Combine(strCSVPath, CSVFileName)) == true)
                        {
                            intIncrement++;
                            CSVFileName = cmnService.J_Mid(CSVFileName, 0, CSVFileName.Length - 4) + " (" + intIncrement + ").csv";  //-- 12/11/2018 --
                        }
                    } while (cmnService.J_IsFileExist(Path.Combine(strCSVPath, CSVFileName)) == true);
                    //--
                    using (WebClient wc = new WebClient())
                        wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_eMailId_Deductee.csv", Path.Combine(strCSVPath, CSVFileName));
                    //--
                    System.Threading.Thread.Sleep(300);
                    //--
                    if (File.Exists(Path.Combine(strCSVPath, CSVFileName)) == true)
                    {
                        System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFileName));
                    }
                    else
                    {
                        cmnService.J_UserMessage("File not found");
                    }
                    //--
                    this.Cursor = Cursors.Default;
                }

            }
            catch (Exception err)
            {

                cmnService.J_UserMessage("Excel file creation failed");
                //cmnService.J_UserMessage(err.Message);
                //--
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region btnSelectExcelPathEmployee_Click
        private void btnSelectExcelPathEmployee_Click(object sender, EventArgs e)
        {
            if (rbnEmployeeExcel.Checked == true)
            {
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                    strExcelPathEmployee = cmnService.J_OpenFileDialog("Excel File | *.xlsx", "Excel File | *.xlsx", "Choose the Excel File to import");
                else
                    strExcelPathEmployee = cmnService.J_OpenFileDialog("Excel File | *.xls; *.xlsx", "Excel File | *.xls; *.xlsx", "Choose the Excel File to import");
            }
            else if (rbnEmployeeCSV.Checked==true)
            {
                strExcelPathEmployee = cmnService.J_OpenFileDialog("CSV File | *.csv", "CSV File | *.csv", "Choose the CSV File to import");
            }
            //--
            if (strExcelPathEmployee != "")
                txtExcelPathEmployee.Text = strExcelPathEmployee;
        }
        #endregion

        #region btnSelectExcelPathDeductee_Click
        private void btnSelectExcelPathDeductee_Click(object sender, EventArgs e)
        {
            if (rbnDeducteeExcel.Checked == true)
            {
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                    strExcelPathDeductee = cmnService.J_OpenFileDialog("Excel File | *.xlsx", "Excel File | *.xlsx", "Choose the Excel File to import");
                else
                    strExcelPathDeductee = cmnService.J_OpenFileDialog("Excel File | *.xls; *.xlsx", "Excel File | *.xls; *.xlsx", "Choose the Excel File to import");
            }
            else if (rbnDeducteeCSV.Checked == true)
            {
                strExcelPathDeductee = cmnService.J_OpenFileDialog("CSV File | *.csv", "CSV File | *.csv", "Choose the CSV File to import");
            }
            //--
            if (strExcelPathDeductee != "")
                txtExcelPathDeductee.Text = strExcelPathDeductee;
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            blnExcel = false;
            try
            {
                if (tbcEmailImport.SelectedIndex == 0 || tbcEmailImport.SelectedIndex == 2) //-- EMPLOYEE
                {
                    #region VALIDATE - EMPLOYEE
                    if (Convert.ToString(BtnSave.Tag) == T_SaveTag.VALIDATE)
                    {
                        //--
                        if (rbnEmployeeExcel.Checked == true)
                        {
                            blnExcel = true;
                            strCSVFilePath = "";
                        }
                        else if (rbnEmployeeCSV.Checked == true)
                        {
                            blnExcel = false;
                            strCSVFilePath = txtExcelPathEmployee.Text;
                        }
                        //--
                        if (ValidateFields() == false) return;
                        //--
                        if (cmnService.J_UserMessage("Proceed Email id Import??", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                        //--                
                        this.Cursor = Cursors.WaitCursor;
                        //--
                        if (rbnEmployeeExcel.Checked == true)
                        {
                            blnExcel = true;
                            if (CheckExcelStructure(txtExcelPathEmployee.Text) == false)
                            {
                                this.Cursor = Cursors.Default;
                                cmnService.J_UserMessage("Selected Excel file is invalid");
                                prgBarEmployee.Value = 0;
                                btnSelectExcelPathEmployee.Select();
                                return;
                            }

                            prgBarEmployee.Value = prgBarEmployee.Value + 5;
                            this.Refresh();
                            //
                            lblProgressDisplayMessageEmployee.Visible = true;
                            lblProgressDisplayMessageEmployee.Text = "Process Started";
                            //--
                            if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
                            {
                                //
                                lblProgressDisplayMessageEmployee.Visible = true;
                                lblProgressDisplayMessageEmployee.Text = "EXCEL file initialized";
                                //
                            }
                            //
                        }
                        else
                            blnExcel = false;
                        //--
                        prgBarEmployee.Value = prgBarEmployee.Value + 5;
                        this.Refresh();
                        // CREATE TEMP TABLES  
                        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        {
                            //MessageBox.Show("1");
                            if (CREATE_TEMP_TABLES_SQL() == false)
                            {
                                cmnService.J_UserMessage("Temporary Tables Not created", MessageBoxIcon.Exclamation);
                                this.Cursor = Cursors.Default;
                                prgBarEmployee.Value = 0;
                                return;
                            }
                        }
                        else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        {
                            if (CREATE_TEMP_TABLES() == false)
                            {
                                cmnService.J_UserMessage("Temporary Tables Not created");
                                this.Cursor = Cursors.Default;
                                prgBarEmployee.Value = 0;
                                return;
                            }
                        }
                        //--
                        prgBarEmployee.Value = prgBarEmployee.Value + 5;
                        this.Refresh();
                        // GET DATA FROM EXCEL ACCESS       
                        //
                        lblProgressDisplayMessageEmployee.Visible = true;
                        lblProgressDisplayMessageEmployee.Text = "Transferring Data";
                        //
                        if (blnExcel==true)
                        {
                            if (GET_DATA_FROM_EXCEL_ACCESS(txtExcelPathEmployee.Text.Trim()) == false)
                            {
                                cmnService.J_UserMessage("Excel data import failed");
                                this.Cursor = Cursors.Default;
                                prgBarEmployee.Value = 0;
                                return;
                            }
                        }
                        else if (blnExcel == false)
                        {
                            if (GET_DATA_FROM_CSV_ACCESS(txtExcelPathEmployee.Text.Trim()) == false)
                            {
                                cmnService.J_UserMessage("CSV data import failed");
                                this.Cursor = Cursors.Default;
                                prgBarEmployee.Value = 0;
                                return;
                            }
                        }
                        prgBarEmployee.Value = prgBarEmployee.Value + 5;
                        this.Refresh();
                        // VALIDATE DATA       
                        //
                        lblProgressDisplayMessageEmployee.Visible = true;
                        lblProgressDisplayMessageEmployee.Text = "Validation Started";
                        //
                        if (VALIDATE_DATA() == false)
                        {
                            cmnService.J_UserMessage("Data Validation failed");
                            this.Cursor = Cursors.Default;
                            prgBarEmployee.Value = 0;
                            return;
                        }
                        prgBarEmployee.Value = prgBarEmployee.Value + 5;
                        this.Refresh();
                        //--
                        if (rbnEmployeeExcel.Checked == true)
                        {
                            if (DELETE_WORKSHEET(txtExcelPathEmployee.Text) == false)
                            {
                                cmnService.J_UserMessage("Error Sheet Deletion failed");
                                this.Cursor = Cursors.Default;
                                prgBarEmployee.Value = 0;
                                return;
                            }
                            //
                            if (CREATE_NEW_WORKSHEET(txtExcelPathEmployee.Text) == false)
                            {
                                cmnService.J_UserMessage("Error Sheet Creation failed");
                                this.Cursor = Cursors.Default;
                                prgBarEmployee.Value = 0;
                                return;
                            }
                        }
                        //
                        prgBarEmployee.Value = prgBarEmployee.Value + 5;
                        this.Refresh();
                        //
                        lblProgressDisplayMessageEmployee.Visible = true;
                        lblProgressDisplayMessageEmployee.Text = "New Worksheet created";
                        //
                        //--
                        prgBarEmployee.Value = prgBarEmployee.Value + 5;
                        this.Refresh();
                        //
                        intTotalErrors = 0;
                        intTotalErrors = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""));
                        //        
                        if (intTotalErrors > 0)
                        {
                            if (blnExcel == true)
                            {
                                if (WRITE_ERROR_WORKSHEET(txtExcelPathEmployee.Text) == false)
                                {
                                    cmnService.J_UserMessage("Writing Error Sheet failed");
                                    this.Cursor = Cursors.Default;
                                    prgBarEmployee.Value = 0;
                                    return;
                                }
                            }
                            else if (blnExcel == false)
                            {
                                if (WRITE_ERROR_HTML(strCSVFilePath) == false)
                                {
                                    cmnService.J_UserMessage("Writing Error File failed", MessageBoxIcon.Error);
                                    this.Cursor = Cursors.Default;
                                    prgBarEmployee.Value = 0;
                                    return;
                                }
                            }
                            prgBarEmployee.Value = prgBarEmployee.Value + 5;
                            this.Refresh();

                            //--
                            //if (chkColorCodingExcelsheet.Checked == true)
                            //{
                            //    //
                            //    lblProgressDisplayMessageEmployee.Visible = true;
                            //    lblProgressDisplayMessageEmployee.Text = "EXCEL file coloring";
                            //    //
                            //    //if (COLOR_ERROR_CELLS(txtExcelPathEmployee.Text) == false)
                            //    //{
                            //    //    cmnService.J_UserMessage("Coloring Error Cells failed");
                            //    //    this.Cursor = Cursors.Default;
                            //    //    prgBarEmployee.Value = 0;
                            //    //    return;
                            //    //}
                            //}
                            //-- DELETE ALL .tmp FILES
                            Delete_TMP_Files(txtExcelPathEmployee.Text);
                            //
                            lblProgressDisplayMessageEmployee.Visible = false;
                            //
                            for (int i = prgBarEmployee.Minimum; i <= prgBarEmployee.Maximum; i++)
                            {
                                prgBarEmployee.PerformStep();
                            }
                            this.Cursor = Cursors.Default;
                            if (blnExcel == true)
                            {
                                //cmnService.J_UserMessage("Excel File Validation failed \n Check the <Validation Error> Sheet of the Excel file ", MessageBoxIcon.Exclamation);
                                if (cmnService.J_UserMessage("Excel File Validation failed \n Do you want to open the Excel file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                                {
                                    if (File.Exists(txtExcelPathEmployee.Text) == true)
                                    {
                                        System.Diagnostics.Process.Start(txtExcelPathEmployee.Text);
                                    }
                                }
                            }
                            else if (blnExcel == false)
                            {
                                cmnService.J_UserMessage("CSV File Validation failed \n Now it will open the error (HTML) file.", MessageBoxIcon.Exclamation);
                                //--
                                prgBarEmployee.Value = 0;
                                if (File.Exists(Path.Combine(Path.GetDirectoryName(txtExcelPathEmployee.Text), Path.GetFileNameWithoutExtension(txtExcelPathEmployee.Text) + ".htm")) == true)
                                {
                                    System.Diagnostics.Process.Start(Path.Combine(Path.GetDirectoryName(txtExcelPathEmployee.Text), Path.GetFileNameWithoutExtension(txtExcelPathEmployee.Text) + ".htm"));
                                }
                            }
                            //
                            prgBarEmployee.Value = 0;
                            return;
                        }
                        else
                        {
                            //if (UPDT_MODE(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))) == false)
                            //{
                            //    cmnService.J_UserMessage("Updating failed");
                            //    this.Cursor = Cursors.Default;
                            //    prgBarEmployee.Value = 0;
                            //    return;
                            //}

                            for (int i = prgBarEmployee.Minimum; i <= prgBarEmployee.Maximum; i++)
                            {
                                prgBarEmployee.PerformStep();
                            }
                            this.Cursor = Cursors.Default;

                            if (DELETE_WORKSHEET(txtExcelPathEmployee.Text) == false) this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage("Excel File Validation is completed \n     Proceed to Import ", MessageBoxIcon.Information);
                        }
                        //-- DELETE ALL .tmp FILES
                        Delete_TMP_Files(txtExcelPathEmployee.Text);
                        lblProgressDisplayMessageEmployee.Visible = false;
                        //--
                        if (LOAD_IMPORT_INTERFACE_EMPLOYEE() == false)
                        {
                            cmnService.J_UserMessage("Loading Import Interface failed");
                            this.Cursor = Cursors.Default;
                            prgBarEmployee.Value = 0;
                            return;
                        }
                        //- UPDATE MODE
                        LoadEmployeeMasterGrid();
                        //--

                        //--
                        BtnSave.Text = "Import Data";
                        BtnSave.Tag = T_SaveTag.IMPORT;
                        lblTitle.Text = "Excel Import";
                    }
                    #endregion
                    //--
                    #region IMPORT - EMPLOYEE
                    else if (Convert.ToString(BtnSave.Tag) == T_SaveTag.IMPORT)
                    {
                        //--
                        if (cmnService.J_UserMessage("Proceed Import??", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                        //--
                        prgEmployeeImportBar.Value = 0;
                        //--
                        this.Cursor = Cursors.WaitCursor;
                        //--                
                        prgEmployeeImportBar.Value = prgEmployeeImportBar.Value + 5;
                        this.Refresh();
                        //--
                        //--
                        if (UPDATE_EMPLOYEE_EMAIL() == false) return;
                        //--
                        //
                        if (DROP_TEMP_TABLES() == false) return;
                        //--
                        for (int i = prgEmployeeImportBar.Minimum; i <= prgEmployeeImportBar.Maximum; i++)
                        {
                            prgEmployeeImportBar.PerformStep();
                        }
                        this.Cursor = Cursors.Default;
                        //--
                        BtnSave.Enabled = false;
                        //
                        cmnService.J_UserMessage("Import from Excel File completed", MessageBoxIcon.Information);
                        prgEmployeeImportBar.Value = 0;
                        //--
                        dmlService.Dispose();
                        this.Close();
                        this.Dispose();
                    }
                    #endregion
                }
                else if (tbcEmailImport.SelectedIndex == 1|| tbcEmailImport.SelectedIndex == 3) //-- DEDUCTEE
                {
                    #region VALIDATE - DEDUCTEE
                    if (Convert.ToString(BtnSave.Tag) == T_SaveTag.VALIDATE)
                    {
                        //--
                        if (rbnDeducteeExcel.Checked == true)
                        {
                            blnExcel = true;
                            strCSVFilePath = "";
                        }
                        else if (rbnDeducteeCSV.Checked == true)
                        {
                            blnExcel = false;
                            strCSVFilePath = txtExcelPathDeductee.Text;
                        }
                        //--
                        if (ValidateFields() == false) return;
                        //--
                        if (cmnService.J_UserMessage("Proceed Email id Import??", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                        //--                
                        this.Cursor = Cursors.WaitCursor;
                        //--
                        if (rbnDeducteeExcel.Checked == true)
                        {
                            blnExcel = true;
                            if (CheckExcelStructure(txtExcelPathDeductee.Text) == false)
                            {
                                this.Cursor = Cursors.Default;
                                cmnService.J_UserMessage("Selected Excel file is invalid");
                                prgBarDeductee.Value = 0;
                                btnSelectExcelPathEmployee.Select();
                                return;
                            }
                            prgBarDeductee.Value = prgBarDeductee.Value + 5;
                            this.Refresh();
                            //
                            lblProgressDisplayMessageEmployee.Visible = true;
                            lblProgressDisplayMessageEmployee.Text = "Process Started";
                            //--
                            if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
                            {
                                //
                                lblProgressDisplayMessageEmployee.Visible = true;
                                lblProgressDisplayMessageEmployee.Text = "EXCEL file initialized";
                                //
                                //if (INITIALIZE_COLOR_ERROR_CELLS(txtExcelPath.Text) == false)
                                //{
                                //    cmnService.J_UserMessage("Initialize Coloring Error Cells failed");
                                //    this.Cursor = Cursors.Default;
                                //    prgBarDeductee.Value = 0;
                                //    return;
                                //}
                            }
                        }
                        else
                            blnExcel = false;
                        //--
                        prgBarDeductee.Value = prgBarDeductee.Value + 5;
                        this.Refresh();
                        // CREATE TEMP TABLES  
                        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        {
                            //MessageBox.Show("1");
                            if (CREATE_TEMP_TABLES_SQL() == false)
                            {
                                cmnService.J_UserMessage("Temporary Tables Not created", MessageBoxIcon.Exclamation);
                                this.Cursor = Cursors.Default;
                                prgBarDeductee.Value = 0;
                                return;
                            }
                        }
                        else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        {
                            if (CREATE_TEMP_TABLES() == false)
                            {
                                cmnService.J_UserMessage("Temporary Tables Not created");
                                this.Cursor = Cursors.Default;
                                prgBarDeductee.Value = 0;
                                return;
                            }
                        }
                        prgBarDeductee.Value = prgBarDeductee.Value + 5;
                        this.Refresh();
                        // GET DATA FROM EXCEL ACCESS       
                        //
                        lblProgressDisplayMessageEmployee.Visible = true;
                        lblProgressDisplayMessageEmployee.Text = "Transferring Data";
                        //
                        if (rbnDeducteeExcel.Checked == true)
                        {
                            if (GET_DATA_FROM_EXCEL_ACCESS(txtExcelPathDeductee.Text.Trim()) == false)
                            {
                                cmnService.J_UserMessage("Excel data import failed");
                                this.Cursor = Cursors.Default;
                                prgBarDeductee.Value = 0;
                                return;
                            }
                        }
                        else if (rbnDeducteeCSV.Checked == true)
                        {
                            if (GET_DATA_FROM_CSV_ACCESS(txtExcelPathDeductee.Text.Trim()) == false)
                            {
                                cmnService.J_UserMessage("CSV data import failed");
                                this.Cursor = Cursors.Default;
                                prgBarDeductee.Value = 0;
                                return;
                            }
                        }
                        prgBarDeductee.Value = prgBarDeductee.Value + 5;
                        this.Refresh();
                        // VALIDATE DATA       
                        //
                        lblProgressDisplayMessageEmployee.Visible = true;
                        lblProgressDisplayMessageEmployee.Text = "Validation Started";
                        //
                        if (VALIDATE_DATA() == false)
                        {
                            cmnService.J_UserMessage("Data Validation failed");
                            this.Cursor = Cursors.Default;
                            prgBarDeductee.Value = 0;
                            return;
                        }
                        prgBarDeductee.Value = prgBarDeductee.Value + 5;
                        this.Refresh();
                        //--
                        if (rbnDeducteeExcel.Checked == true)
                        {
                            if (DELETE_WORKSHEET(txtExcelPathDeductee.Text) == false)
                            {
                                cmnService.J_UserMessage("Error Sheet Deletion failed");
                                this.Cursor = Cursors.Default;
                                prgBarDeductee.Value = 0;
                                return;
                            }
                            //
                            if (CREATE_NEW_WORKSHEET(txtExcelPathDeductee.Text) == false)
                            {
                                cmnService.J_UserMessage("Error Sheet Creation failed");
                                this.Cursor = Cursors.Default;
                                prgBarDeductee.Value = 0;
                                return;
                            }
                        }
                        prgBarDeductee.Value = prgBarDeductee.Value + 5;
                        this.Refresh();
                        //
                        lblProgressDisplayMessageEmployee.Visible = true;
                        lblProgressDisplayMessageEmployee.Text = "New Worksheet created";
                        //
                        //--
                        prgBarDeductee.Value = prgBarDeductee.Value + 5;
                        this.Refresh();
                        //
                        intTotalErrors = 0;
                        intTotalErrors = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""));
                        //        
                        if (intTotalErrors > 0)
                        {
                            if (blnExcel == true)
                            {
                                if (WRITE_ERROR_WORKSHEET(txtExcelPathDeductee.Text) == false)
                                {
                                    cmnService.J_UserMessage("Writing Error Sheet failed");
                                    this.Cursor = Cursors.Default;
                                    prgBarDeductee.Value = 0;
                                    return;
                                }
                            }
                            else if (blnExcel == false)
                            {
                                if (WRITE_ERROR_HTML(strCSVFilePath) == false)
                                {
                                    cmnService.J_UserMessage("Writing Error File failed", MessageBoxIcon.Error);
                                    this.Cursor = Cursors.Default;
                                    prgBarDeductee.Value = 0;
                                    return;
                                }
                            }

                            prgBarDeductee.Value = prgBarDeductee.Value + 5;
                            this.Refresh();
                            //-- DELETE ALL .tmp FILES
                            Delete_TMP_Files(txtExcelPathDeductee.Text);
                            //
                            lblProgressDisplayMessageEmployee.Visible = false;
                            //
                            for (int i = prgBarDeductee.Minimum; i <= prgBarDeductee.Maximum; i++)
                            {
                                prgBarDeductee.PerformStep();
                            }
                            this.Cursor = Cursors.Default;

                            if (rbnDeducteeExcel.Checked == true)
                            {
                                //cmnService.J_UserMessage("Excel File Validation failed \n Check the <Validation Error> Sheet of the Excel file ", MessageBoxIcon.Exclamation);
                                if (cmnService.J_UserMessage("Excel File Validation failed \n Do you want to open the Excel file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                                {
                                    if (File.Exists(txtExcelPathDeductee.Text) == true)
                                    {
                                        System.Diagnostics.Process.Start(txtExcelPathDeductee.Text);
                                    }
                                }
                            }
                            else if (rbnDeducteeCSV.Checked == true)
                            {
                                cmnService.J_UserMessage("CSV File Validation failed \n Now it will open the error (HTML) file.", MessageBoxIcon.Exclamation);
                                //--
                                prgBarDeductee.Value = 0;
                                if (File.Exists(Path.Combine(Path.GetDirectoryName(txtExcelPathDeductee.Text), Path.GetFileNameWithoutExtension(txtExcelPathDeductee.Text) + ".htm")) == true)
                                {
                                    System.Diagnostics.Process.Start(Path.Combine(Path.GetDirectoryName(txtExcelPathDeductee.Text), Path.GetFileNameWithoutExtension(txtExcelPathDeductee.Text) + ".htm"));
                                }
                            }
                            //
                            prgBarDeductee.Value = 0;
                            return;
                        }
                        else
                        {
                            
                            for (int i = prgBarDeductee.Minimum; i <= prgBarDeductee.Maximum; i++)
                            {
                                prgBarDeductee.PerformStep();
                            }
                            this.Cursor = Cursors.Default;

                            if (DELETE_WORKSHEET(txtExcelPathDeductee.Text) == false) this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage("Excel File Validation is completed \n     Proceed to Import ", MessageBoxIcon.Information);
                        }
                        //-- DELETE ALL .tmp FILES
                        Delete_TMP_Files(txtExcelPathDeductee.Text);
                        lblProgressDisplayMessageEmployee.Visible = false;
                        //--
                        if (LOAD_IMPORT_INTERFACE_DEDUCTEE() == false)
                        {
                            cmnService.J_UserMessage("Loading Import Interface failed");
                            this.Cursor = Cursors.Default;
                            prgBarDeductee.Value = 0;
                            return;
                        }
                        //- UPDATE MODE
                        LoadDeducteeMasterGrid();
                        //--

                        //--
                        BtnSave.Text = "Import Data";
                        BtnSave.Tag = T_SaveTag.IMPORT;
                        lblTitle.Text = "Excel Import";
                    }
                    #endregion
                    //--
                    #region IMPORT - DEDUCTEE
                    else if (Convert.ToString(BtnSave.Tag) == T_SaveTag.IMPORT)
                    {
                        //--
                        if (cmnService.J_UserMessage("Proceed Import??", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                        //--
                        prgEmployeeImportBar.Value = 0;
                        //--
                        this.Cursor = Cursors.WaitCursor;
                        //--                
                        prgEmployeeImportBar.Value = prgEmployeeImportBar.Value + 5;
                        this.Refresh();
                        //--
                        //--
                        if (UPDATE_DEDUCTEE_EMAIL() == false) return;
                        //--
                        //
                        if (DROP_TEMP_TABLES() == false) return;
                        //--
                        for (int i = prgEmployeeImportBar.Minimum; i <= prgEmployeeImportBar.Maximum; i++)
                        {
                            prgEmployeeImportBar.PerformStep();
                        }
                        this.Cursor = Cursors.Default;
                        //--
                        BtnSave.Enabled = false;
                        //
                        cmnService.J_UserMessage("Import from Excel File completed", MessageBoxIcon.Information);
                        prgEmployeeImportBar.Value = 0;
                        //--
                        dmlService.Dispose();
                        this.Close();
                        this.Dispose();
                    }
                    #endregion
                }
            }
            catch (Exception err)
            {

                cmnService.J_UserMessage("Excel file creation failed");
                //cmnService.J_UserMessage(err.Message);
                //--
                this.Cursor = Cursors.Default;
            }

        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region rbnImportEmployee_CheckedChanged
        private void rbnImportEmployee_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnImportEmployee.Checked == true)
            {
                //
                rbnDeducteeCSV.Checked = false;
                rbnDeducteeExcel.Checked = false;
                txtExcelPathDeductee.Text = "";
                //
                tbcEmailImport.SelectTab(tbpValidateEmployee);
            }
            else if (rbnImportDeductee.Checked == true)
            {
                //
                rbnEmployeeCSV.Checked = false;
                rbnEmployeeExcel.Checked = false;
                txtExcelPathEmployee.Text = "";
                //
                tbcEmailImport.SelectTab(tbpValidateDeductee);
            }
        }
        #endregion

        #region chkSelectAllDeductee_CheckedChanged
        private void chkSelectAllDeductee_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                //if (grdvCertificateDescription.Visible == false) { chkSelectDeselect.Checked = false; return; }
                //if (blnSelectDeselect == false) return;
                //--
                this.Cursor = Cursors.WaitCursor;
                //blnDeleteTempGridRecord = true;
                foreach (DataGridViewRow row in grdvDeductee.Rows)
                {
                    if (chkSelectAllDeductee.Checked == true)//checked all checkbox
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                        {
                            row.Cells[0].Value = true;
                        }
                    }
                    else//Unchecked all checkbox
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                        {
                            //blnExitGrid = false;
                            row.Cells[0].Value = false;
                            //blnExitGrid = false;
                        }
                        //strSelectedLabel = "";
                    }
                }
                //
                #region COMMENT
                //blnDeleteTempGridRecord = false;

                ////ADDED BY DHRUB ON 07/02/2014
                //if ((blnDeleteTempGridRecord == false) && (grdvCertificateDescription.Rows.Count > 0))
                //    SelectedGridItemsWithSearchData(cmnService.J_GenerateDataGridViewSelectedId(grdvCertificateDescription));
                ////
                //if (chkCertificateSelectDeselect.Checked == true)
                //{
                //    //dmlService.J_ExecSql("DELETE FROM TEMP_REPORT_GRID_ITEMS_SELECTED");
                //    //--
                //    if (grdvCertificateDescription.RowCount > 0)
                //    {
                //        foreach (DataGridViewRow row in grdvCertificateDescription.Rows)
                //        {
                //            if (row.Cells[1].Value != null)
                //            {

                //                if (blnTextGrid == false)
                //                {
                //                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED, "ITEM_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value))) == false)
                //                    {
                //                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED + "(ITEM_ID,ITEM_NAME) VALUES(" +
                //                                                        cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + ",'" +
                //                                                        cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[2].Value)) + "')";
                //                        dmlService.J_ExecSql(strSQL);
                //                    }
                //                }
                //                else
                //                {
                //                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED, "ITEM_NAME ='" + Convert.ToString(row.Cells[1].Value) + " - " + Convert.ToString(row.Cells[2].Value) + "'") == false)
                //                    {
                //                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED + "(ITEM_NAME) VALUES('" +
                //                                                        Convert.ToString(row.Cells[1].Value) + " - " + Convert.ToString(row.Cells[2].Value) + "')";
                //                        dmlService.J_ExecSql(strSQL);
                //                    }
                //                }
                //                //
                //                //intCount = intCount + 1;
                //            }
                //        }
                //        //--
                //        intCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED, J_QueryType.DirectQuery);
                //        if (intCount > 0)
                //        {
                //            lblCertificateShowSelected.Visible = true;
                //            lblCertificateShowSelected.Text = intCount + " record(s) selected";
                //        }
                //        else
                //            lblCertificateShowSelected.Visible = false;
                //        //--
                //    }

                //}
                //else
                //{
                //    if (txtCertificateSearch.Text.Trim() == "")
                //    {
                //        dmlService.J_ExecSql("DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED);
                //        lblCertificateShowSelected.Visible = false;
                //        intCount = 0;
                //    }
                //    else
                //    {
                //        foreach (DataGridViewRow row in grdvCertificateDescription.Rows)
                //        {
                //            if (row.Cells[1].Value != null)
                //            {
                //                //if (dmlService.J_IsRecordExist("TEMP_REPORT_GRID_ITEMS_SELECTED", "ITEM_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value))) == false)
                //                //{
                //                //strSQL = "INSERT INTO TEMP_REPORT_GRID_ITEMS_SELECTED(ITEM_ID,ITEM_NAME) VALUES(" +
                //                //                                cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + ",'" +
                //                //                                cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[2].Value)) + "')";
                //                //dmlService.J_ExecSql(strSQL);
                //                //}
                //                //
                //                //intCount = intCount + 1;
                //                dmlService.J_ExecSql("DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED + " WHERE ITEM_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)));
                //            }
                //        }
                //        //--
                //        intCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED, J_QueryType.DirectQuery);
                //        if (intCount > 0)
                //        {
                //            lblCertificateShowSelected.Visible = true;
                //            lblCertificateShowSelected.Text = intCount + " record(s) selected";
                //        }
                //        else
                //            lblCertificateShowSelected.Visible = false;
                //        //--

                //    }
                //}
                //
                #endregion
                //
                this.Cursor = Cursors.Default;
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region chkSelectAllEmployee_CheckedChanged
        private void chkSelectAllEmployee_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                //if (grdvCertificateDescription.Visible == false) { chkSelectDeselect.Checked = false; return; }
                //if (blnSelectDeselect == false) return;
                //--
                this.Cursor = Cursors.WaitCursor;
                //blnDeleteTempGridRecord = true;
                foreach (DataGridViewRow row in grdvEmployee.Rows)
                {
                    if (chkSelectAllEmployee.Checked == true)//checked all checkbox
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                        {
                            row.Cells[0].Value = true;
                        }
                    }
                    else//Unchecked all checkbox
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                        {
                            //blnExitGrid = false;
                            row.Cells[0].Value = false;
                            //blnExitGrid = false;
                        }
                        //strSelectedLabel = "";
                    }
                }
                //
                #region COMMENT
                //blnDeleteTempGridRecord = false;

                ////ADDED BY DHRUB ON 07/02/2014
                //if ((blnDeleteTempGridRecord == false) && (grdvCertificateDescription.Rows.Count > 0))
                //    SelectedGridItemsWithSearchData(cmnService.J_GenerateDataGridViewSelectedId(grdvCertificateDescription));
                ////
                //if (chkCertificateSelectDeselect.Checked == true)
                //{
                //    //dmlService.J_ExecSql("DELETE FROM TEMP_REPORT_GRID_ITEMS_SELECTED");
                //    //--
                //    if (grdvCertificateDescription.RowCount > 0)
                //    {
                //        foreach (DataGridViewRow row in grdvCertificateDescription.Rows)
                //        {
                //            if (row.Cells[1].Value != null)
                //            {

                //                if (blnTextGrid == false)
                //                {
                //                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED, "ITEM_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value))) == false)
                //                    {
                //                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED + "(ITEM_ID,ITEM_NAME) VALUES(" +
                //                                                        cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + ",'" +
                //                                                        cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[2].Value)) + "')";
                //                        dmlService.J_ExecSql(strSQL);
                //                    }
                //                }
                //                else
                //                {
                //                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED, "ITEM_NAME ='" + Convert.ToString(row.Cells[1].Value) + " - " + Convert.ToString(row.Cells[2].Value) + "'") == false)
                //                    {
                //                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED + "(ITEM_NAME) VALUES('" +
                //                                                        Convert.ToString(row.Cells[1].Value) + " - " + Convert.ToString(row.Cells[2].Value) + "')";
                //                        dmlService.J_ExecSql(strSQL);
                //                    }
                //                }
                //                //
                //                //intCount = intCount + 1;
                //            }
                //        }
                //        //--
                //        intCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED, J_QueryType.DirectQuery);
                //        if (intCount > 0)
                //        {
                //            lblCertificateShowSelected.Visible = true;
                //            lblCertificateShowSelected.Text = intCount + " record(s) selected";
                //        }
                //        else
                //            lblCertificateShowSelected.Visible = false;
                //        //--
                //    }

                //}
                //else
                //{
                //    if (txtCertificateSearch.Text.Trim() == "")
                //    {
                //        dmlService.J_ExecSql("DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED);
                //        lblCertificateShowSelected.Visible = false;
                //        intCount = 0;
                //    }
                //    else
                //    {
                //        foreach (DataGridViewRow row in grdvCertificateDescription.Rows)
                //        {
                //            if (row.Cells[1].Value != null)
                //            {
                //                //if (dmlService.J_IsRecordExist("TEMP_REPORT_GRID_ITEMS_SELECTED", "ITEM_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value))) == false)
                //                //{
                //                //strSQL = "INSERT INTO TEMP_REPORT_GRID_ITEMS_SELECTED(ITEM_ID,ITEM_NAME) VALUES(" +
                //                //                                cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + ",'" +
                //                //                                cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[2].Value)) + "')";
                //                //dmlService.J_ExecSql(strSQL);
                //                //}
                //                //
                //                //intCount = intCount + 1;
                //                dmlService.J_ExecSql("DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED + " WHERE ITEM_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)));
                //            }
                //        }
                //        //--
                //        intCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED, J_QueryType.DirectQuery);
                //        if (intCount > 0)
                //        {
                //            lblCertificateShowSelected.Visible = true;
                //            lblCertificateShowSelected.Text = intCount + " record(s) selected";
                //        }
                //        else
                //            lblCertificateShowSelected.Visible = false;
                //        //--

                //    }
                //}
                //
                #endregion
                //
                this.Cursor = Cursors.Default;
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region CREATING BLANK EXCEL FILE

        #region CREATE EXCEL FILE
        // SOURCE PATH : http://csharp.net-informations.com/excel/csharp-create-excel.htm
        private bool CREATE_EXCEL_FILE(string ExcelFilePath)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                //-- MessageBox.Show("5.0.1.1");
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                //Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                //--
                //-- MessageBox.Show("5.0.1.2");
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
                //-- MessageBox.Show("5.0.1.3");
                xlWorkBook.Close(true, misValue, misValue);
                //-- MessageBox.Show("5.0.1.4");
                xlApp.Quit();
                //-- MessageBox.Show("5.0.1.5");

                //ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                //-- MessageBox.Show("5.0.1.6");
                ReleaseObject(xlApp);
                //-- MessageBox.Show("5.0.1.7");
                //--
                return true;
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
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
                Microsoft.Office.Interop.Excel.Worksheet wsnew = sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                wsnew.Name = T_Party_Sheet_Name.VALIDATION_ERROR_DETAILS;

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

        #region DELETE WORKSHEET
        private bool DELETE_WORKSHEET(string ExcelFilePath, string ExcelSheet)
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
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
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
                    if (ws.Name.ToString().Trim() ==  T_Party_Sheet_Name.VALIDATION_ERROR_DETAILS)
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

        #region WRITE WORKSHEET

        #region WRITE WORKSHEET
        private bool WRITE_WORKSHEET(string ExcelFilePath, string SheetName, string Cell, string CellValue, bool Mandatory)
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range(Cell, m).Value2 = CellValue;
                wsnew.get_Range(Cell, m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range(Cell, m).Borders.Value = true;
                wsnew.get_Range(Cell, m).RowHeight = 48.75;
                wsnew.get_Range(Cell, m).ColumnWidth = 12;
                wsnew.get_Range(Cell, m).WrapText = true;
                wsnew.get_Range(Cell, m).Font.Name = "Arial";
                wsnew.get_Range(Cell, m).Font.Bold = true;
                wsnew.get_Range(Cell, m).Font.Size = 9;
                //@@@@@@@@@@@@@@@
                if (Mandatory == true)
                    //                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow);
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 153));
                else if (Mandatory == false)
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(204, 255, 204));
                //                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
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
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region WRITE WORKSHEET
        private bool WRITE_WORKSHEET(string ExcelFilePath, string SheetName, string Cell, string CellValue, bool Mandatory, bool Calculated)
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range(Cell, m).Value2 = CellValue;
                wsnew.get_Range(Cell, m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range(Cell, m).Borders.Value = true;
                wsnew.get_Range(Cell, m).RowHeight = 48.75;
                wsnew.get_Range(Cell, m).ColumnWidth = 12;
                wsnew.get_Range(Cell, m).WrapText = true;
                wsnew.get_Range(Cell, m).Font.Name = "Arial";
                wsnew.get_Range(Cell, m).Font.Bold = true;
                wsnew.get_Range(Cell, m).Font.Size = 9;
                //@@@@@@@@@@@@@@@
                if (Mandatory == true)
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 153));
                else if (Mandatory == false)
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(204, 255, 204));
                //--
                if (Calculated == true)
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(191, 187, 195));
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
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region WRITE SECTION WORKSHEET
        private bool WRITE_SECTION_WORKSHEET(string ExcelFilePath, string SheetName, string FormNo)
        {
            try
            {
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
                wsnew.get_Range("A1", m).Value2 = "Section";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 12;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "Section Description";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT SECTION_ID," +
                    "            SECTION_NO," +
                    "            SECTION_DESCRIPTION " +
                    "     FROM   MST_SECTION " +
                    "     WHERE  FORM_NAME ='" + FormNo + "' ";
                if (FormNo == T_FormNo.F24Q)
                    strSQL = strSQL + "AND SECTION_NO <> '' ";
                strSQL = strSQL + "ORDER BY SECTION_ID";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["SECTION_NO"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["SECTION_DESCRIPTION"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                //
                //wsnew.get_Range("A1", "M18").Locked = false;
                //wsnew.get_Range("A1", "M18").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
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

        #region WRITE DEDUCTEE CODE WORKSHEET
        private bool WRITE_DEDUCTEE_CODE_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                //--
                if (KILL_EXCEL() == false)
                    return false;
                //--
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
                wsnew.get_Range("A1", m).Value2 = "Deductee Code";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 25;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("A2", m).Value2 = "01";
                wsnew.get_Range("A2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A2", m).Borders.Value = true;
                wsnew.get_Range("A2", m).ColumnWidth = 25;
                wsnew.get_Range("A2", m).WrapText = true;
                wsnew.get_Range("A2", m).Font.Name = "Arial";
                wsnew.get_Range("A2", m).Font.Bold = true;
                wsnew.get_Range("A2", m).Font.Size = 10;
                //
                wsnew.get_Range("A3", m).Value2 = "02";
                wsnew.get_Range("A3", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A3", m).Borders.Value = true;
                wsnew.get_Range("A3", m).ColumnWidth = 25;
                wsnew.get_Range("A3", m).WrapText = true;
                wsnew.get_Range("A3", m).Font.Name = "Arial";
                wsnew.get_Range("A3", m).Font.Bold = true;
                wsnew.get_Range("A3", m).Font.Size = 10;
                //
                wsnew.get_Range("B1", m).Value2 = "Deductee Type";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B2", m).Value2 = "Company";
                wsnew.get_Range("B2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B2", m).Borders.Value = true;
                wsnew.get_Range("B2", m).ColumnWidth = 25;
                wsnew.get_Range("B2", m).WrapText = true;
                wsnew.get_Range("B2", m).Font.Name = "Arial";
                wsnew.get_Range("B2", m).Font.Bold = true;
                wsnew.get_Range("B2", m).Font.Size = 10;
                //
                wsnew.get_Range("B3", m).Value2 = "Non-Company";
                wsnew.get_Range("B3", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B3", m).Borders.Value = true;
                wsnew.get_Range("B3", m).ColumnWidth = 25;
                wsnew.get_Range("B3", m).WrapText = true;
                wsnew.get_Range("B3", m).Font.Name = "Arial";
                wsnew.get_Range("B3", m).Font.Bold = true;
                wsnew.get_Range("B3", m).Font.Size = 10;
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                ////
                //wsnew.get_Range("A1", "B10").Locked = false;
                //wsnew.get_Range("A1", "B10").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
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

        #region WRITE EMPLOYEE CATEGORY WORKSHEET
        private bool WRITE_EMPLOYEE_CATEGORY_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                //--
                if (KILL_EXCEL() == false)
                    return false;
                //--
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
                wsnew.get_Range("A1", m).Value2 = "Employee Category";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 25;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("A2", m).Value2 = "G";
                wsnew.get_Range("A2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A2", m).Borders.Value = true;
                wsnew.get_Range("A2", m).ColumnWidth = 25;
                wsnew.get_Range("A2", m).WrapText = true;
                wsnew.get_Range("A2", m).Font.Name = "Arial";
                wsnew.get_Range("A2", m).Font.Bold = true;
                wsnew.get_Range("A2", m).Font.Size = 10;
                //
                wsnew.get_Range("A3", m).Value2 = "W";
                wsnew.get_Range("A3", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A3", m).Borders.Value = true;
                wsnew.get_Range("A3", m).ColumnWidth = 25;
                wsnew.get_Range("A3", m).WrapText = true;
                wsnew.get_Range("A3", m).Font.Name = "Arial";
                wsnew.get_Range("A3", m).Font.Bold = true;
                wsnew.get_Range("A3", m).Font.Size = 10;
                //
                wsnew.get_Range("A4", m).Value2 = "S";
                wsnew.get_Range("A4", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A4", m).Borders.Value = true;
                wsnew.get_Range("A4", m).ColumnWidth = 25;
                wsnew.get_Range("A4", m).WrapText = true;
                wsnew.get_Range("A4", m).Font.Name = "Arial";
                wsnew.get_Range("A4", m).Font.Bold = true;
                wsnew.get_Range("A4", m).Font.Size = 10;
                //
                wsnew.get_Range("A5", m).Value2 = "O";
                wsnew.get_Range("A5", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A5", m).Borders.Value = true;
                wsnew.get_Range("A5", m).ColumnWidth = 25;
                wsnew.get_Range("A5", m).WrapText = true;
                wsnew.get_Range("A5", m).Font.Name = "Arial";
                wsnew.get_Range("A5", m).Font.Bold = true;
                wsnew.get_Range("A5", m).Font.Size = 10;
                //
                wsnew.get_Range("B1", m).Value2 = "Description";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B2", m).Value2 = "General";
                wsnew.get_Range("B2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B2", m).Borders.Value = true;
                wsnew.get_Range("B2", m).ColumnWidth = 25;
                wsnew.get_Range("B2", m).WrapText = true;
                wsnew.get_Range("B2", m).Font.Name = "Arial";
                wsnew.get_Range("B2", m).Font.Bold = true;
                wsnew.get_Range("B2", m).Font.Size = 10;
                //
                wsnew.get_Range("B3", m).Value2 = "Female";
                wsnew.get_Range("B3", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B3", m).Borders.Value = true;
                wsnew.get_Range("B3", m).ColumnWidth = 25;
                wsnew.get_Range("B3", m).WrapText = true;
                wsnew.get_Range("B3", m).Font.Name = "Arial";
                wsnew.get_Range("B3", m).Font.Bold = true;
                wsnew.get_Range("B3", m).Font.Size = 10;
                //
                wsnew.get_Range("B4", m).Value2 = "Senior Citizen";
                wsnew.get_Range("B4", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B4", m).Borders.Value = true;
                wsnew.get_Range("B4", m).ColumnWidth = 25;
                wsnew.get_Range("B4", m).WrapText = true;
                wsnew.get_Range("B4", m).Font.Name = "Arial";
                wsnew.get_Range("B4", m).Font.Bold = true;
                wsnew.get_Range("B4", m).Font.Size = 10;
                //
                wsnew.get_Range("B5", m).Value2 = "Very Senior Citizen";
                wsnew.get_Range("B5", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B5", m).Borders.Value = true;
                wsnew.get_Range("B5", m).ColumnWidth = 25;
                wsnew.get_Range("B5", m).WrapText = true;
                wsnew.get_Range("B5", m).Font.Name = "Arial";
                wsnew.get_Range("B5", m).Font.Bold = true;
                wsnew.get_Range("B5", m).Font.Size = 10;
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                ////
                //wsnew.get_Range("A1", "B10").Locked = false;
                //wsnew.get_Range("A1", "B10").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
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

        #region WRITE_REMARKS_WORKSHEET
        private bool WRITE_REMARKS_WORKSHEET(string ExcelFilePath, string SheetName, string FormNo)
        {
            try
            {
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
                wsnew.get_Range("A1", m).Value2 = "Reason";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 12;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "Reason Description";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT REASON_ID," +
                    "            REASON," +
                    "            DESCRIPTION " +
                    "     FROM   MST_REASON " +
                    "     WHERE  FORM_NO ='" + FormNo + "' ";
                strSQL = strSQL + "ORDER BY REASON";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["REASON"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["DESCRIPTION"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                //
                //wsnew.get_Range("A1", "M18").Locked = false;
                //wsnew.get_Range("A1", "M18").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
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

        #region WRITE READ ME WORKSHEET
        private bool WRITE_READ_ME_WORKSHEET(string ExcelFilePath, string SheetName, string FormName)
        {
            try
            {
                //--
                if (KILL_EXCEL() == false)
                    return false;
                //--
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
                wsnew.get_Range("B4", m).Borders.Value = true;
                wsnew.get_Range("B4", m).ColumnWidth = 25;
                wsnew.get_Range("B4", m).RowHeight = 15;
                wsnew.get_Range("B4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 153));
                //
                wsnew.get_Range("B5", m).Borders.Value = true;
                wsnew.get_Range("B5", m).ColumnWidth = 25;
                wsnew.get_Range("B5", m).RowHeight = 15;
                wsnew.get_Range("B5", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(204, 255, 204));
                //
                wsnew.get_Range("C4", m).Value2 = "Light yellow Color in header, should be Mandatory";
                wsnew.get_Range("C4", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C4", m).Borders.Value = true;
                wsnew.get_Range("C4", m).ColumnWidth = 55;
                wsnew.get_Range("C4", m).WrapText = true;
                wsnew.get_Range("C4", m).Font.Name = "Arial";
                wsnew.get_Range("C4", m).Font.Bold = true;
                wsnew.get_Range("C4", m).Font.Size = 10;
                //
                wsnew.get_Range("C5", m).Value2 = "Light Green Color in header, Optional";
                wsnew.get_Range("C5", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C5", m).Borders.Value = true;
                wsnew.get_Range("C5", m).ColumnWidth = 55;
                wsnew.get_Range("C5", m).WrapText = true;
                wsnew.get_Range("C5", m).Font.Name = "Arial";
                wsnew.get_Range("C5", m).Font.Bold = true;
                wsnew.get_Range("C5", m).Font.Size = 10;
                //
                //wsnew.get_Range("B3", "C6").BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black);
                wsnew.get_Range("B3", "C6").Borders.Value = true;
                //
                wsnew.get_Range("C8", m).Value2 = "ERROR Color in Excel format";
                wsnew.get_Range("C8", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C8", m).Borders.Value = true;
                wsnew.get_Range("C8", m).ColumnWidth = 55;
                wsnew.get_Range("C8", m).WrapText = true;
                wsnew.get_Range("C8", m).Font.Name = "Arial";
                wsnew.get_Range("C8", m).Font.Bold = true;
                wsnew.get_Range("C8", m).Font.Size = 12;
                wsnew.get_Range("C8", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                wsnew.get_Range("C8", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                //
                wsnew.get_Range("B10", m).Value2 = "Color";
                wsnew.get_Range("B10", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B10", m).Borders.Value = true;
                wsnew.get_Range("B10", m).ColumnWidth = 55;
                wsnew.get_Range("B10", m).WrapText = true;
                wsnew.get_Range("B10", m).Font.Name = "Arial";
                wsnew.get_Range("B10", m).Font.Bold = true;
                wsnew.get_Range("B10", m).Font.Size = 10;
                wsnew.get_Range("B10", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                wsnew.get_Range("B10", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                //
                wsnew.get_Range("C10", m).Value2 = "Message";
                wsnew.get_Range("C10", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C10", m).Borders.Value = true;
                wsnew.get_Range("C10", m).ColumnWidth = 55;
                wsnew.get_Range("C10", m).WrapText = true;
                wsnew.get_Range("C10", m).Font.Name = "Arial";
                wsnew.get_Range("C10", m).Font.Bold = true;
                wsnew.get_Range("C10", m).Font.Size = 10;
                wsnew.get_Range("C10", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                wsnew.get_Range("C10", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                //
                wsnew.get_Range("B12", m).Borders.Value = true;
                wsnew.get_Range("B12", m).ColumnWidth = 55;
                wsnew.get_Range("B12", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SteelBlue);
                //
                wsnew.get_Range("C12", m).Value2 = "Duplication (It should be unique)";
                wsnew.get_Range("C12", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C12", m).Borders.Value = true;
                wsnew.get_Range("C12", m).ColumnWidth = 55;
                wsnew.get_Range("C12", m).WrapText = true;
                wsnew.get_Range("C12", m).Font.Name = "Arial";
                wsnew.get_Range("C12", m).Font.Bold = true;
                wsnew.get_Range("C12", m).Font.Size = 10;
                //
                wsnew.get_Range("B14", m).Borders.Value = true;
                wsnew.get_Range("B14", m).ColumnWidth = 55;
                wsnew.get_Range("B14", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Tan);
                //
                wsnew.get_Range("C14", m).Value2 = "Error in data length";
                wsnew.get_Range("C14", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C14", m).Borders.Value = true;
                wsnew.get_Range("C14", m).ColumnWidth = 55;
                wsnew.get_Range("C14", m).WrapText = true;
                wsnew.get_Range("C14", m).Font.Name = "Arial";
                wsnew.get_Range("C14", m).Font.Bold = true;
                wsnew.get_Range("C14", m).Font.Size = 10;
                //
                wsnew.get_Range("B16", m).Borders.Value = true;
                wsnew.get_Range("B16", m).ColumnWidth = 55;
                wsnew.get_Range("B16", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SpringGreen);
                //
                wsnew.get_Range("C16", m).Value2 = "Only numeric data allowed";
                wsnew.get_Range("C16", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C16", m).Borders.Value = true;
                wsnew.get_Range("C16", m).ColumnWidth = 55;
                wsnew.get_Range("C16", m).WrapText = true;
                wsnew.get_Range("C16", m).Font.Name = "Arial";
                wsnew.get_Range("C16", m).Font.Bold = true;
                wsnew.get_Range("C16", m).Font.Size = 10;
                //
                wsnew.get_Range("B18", m).Borders.Value = true;
                wsnew.get_Range("B18", m).ColumnWidth = 55;
                wsnew.get_Range("B18", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Chocolate);
                //
                wsnew.get_Range("C18", m).Value2 = "Invalid Data";
                wsnew.get_Range("C18", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C18", m).Borders.Value = true;
                wsnew.get_Range("C18", m).ColumnWidth = 55;
                wsnew.get_Range("C18", m).WrapText = true;
                wsnew.get_Range("C18", m).Font.Name = "Arial";
                wsnew.get_Range("C18", m).Font.Bold = true;
                wsnew.get_Range("C18", m).Font.Size = 10;
                //
                wsnew.get_Range("B20", m).Borders.Value = true;
                wsnew.get_Range("B20", m).ColumnWidth = 55;
                wsnew.get_Range("B20", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                //
                wsnew.get_Range("C20", m).Value2 = "Missing Data";
                wsnew.get_Range("C20", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C20", m).Borders.Value = true;
                wsnew.get_Range("C20", m).ColumnWidth = 55;
                wsnew.get_Range("C20", m).WrapText = true;
                wsnew.get_Range("C20", m).Font.Name = "Arial";
                wsnew.get_Range("C20", m).Font.Bold = true;
                wsnew.get_Range("C20", m).Font.Size = 10;
                //
                wsnew.get_Range("B22", m).Borders.Value = true;
                wsnew.get_Range("B22", m).ColumnWidth = 55;
                wsnew.get_Range("B22", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Tomato);
                //
                wsnew.get_Range("C22", m).Value2 = "Invalid Format";
                wsnew.get_Range("C22", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C22", m).Borders.Value = true;
                wsnew.get_Range("C22", m).ColumnWidth = 55;
                wsnew.get_Range("C22", m).WrapText = true;
                wsnew.get_Range("C22", m).Font.Name = "Arial";
                wsnew.get_Range("C22", m).Font.Bold = true;
                wsnew.get_Range("C22", m).Font.Size = 10;
                //
                wsnew.get_Range("B8", "C27").Borders.Value = true;
                //
                #region SALARY DETAIL
                if (FormName == T_FormNo.F24QSalaryDetails)
                {
                    //
                    wsnew.get_Range("C30", m).Value2 = "Column Explaination of Salary Details";
                    wsnew.get_Range("C30", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("C30", m).Borders.Value = true;
                    wsnew.get_Range("C30", m).ColumnWidth = 55;
                    wsnew.get_Range("C30", m).WrapText = true;
                    wsnew.get_Range("C30", m).Font.Name = "Arial";
                    wsnew.get_Range("C30", m).Font.Bold = true;
                    wsnew.get_Range("C30", m).Font.Size = 10;
                    wsnew.get_Range("C30", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("C30", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("A32", m).Value2 = "Col Name";
                    wsnew.get_Range("A32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("A32", m).Borders.Value = true;
                    wsnew.get_Range("A32", m).ColumnWidth = 55;
                    wsnew.get_Range("A32", m).WrapText = true;
                    wsnew.get_Range("A32", m).Font.Name = "Arial";
                    wsnew.get_Range("A32", m).Font.Bold = true;
                    wsnew.get_Range("A32", m).Font.Size = 10;
                    wsnew.get_Range("A32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("A32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("A32", m).Value2 = "Col Name";
                    wsnew.get_Range("A32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("A32", m).Borders.Value = true;
                    wsnew.get_Range("A32", m).ColumnWidth = 55;
                    wsnew.get_Range("A32", m).WrapText = true;
                    wsnew.get_Range("A32", m).Font.Name = "Arial";
                    wsnew.get_Range("A32", m).Font.Bold = true;
                    wsnew.get_Range("A32", m).Font.Size = 10;
                    wsnew.get_Range("A32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("A32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("B32", m).Value2 = "Column Header";
                    wsnew.get_Range("B32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B32", m).Borders.Value = true;
                    wsnew.get_Range("B32", m).ColumnWidth = 55;
                    wsnew.get_Range("B32", m).WrapText = true;
                    wsnew.get_Range("B32", m).Font.Name = "Arial";
                    wsnew.get_Range("B32", m).Font.Bold = true;
                    wsnew.get_Range("B32", m).Font.Size = 10;
                    wsnew.get_Range("B32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("B32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("C32", m).Value2 = "Description";
                    wsnew.get_Range("C32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C32", m).Borders.Value = true;
                    wsnew.get_Range("C32", m).ColumnWidth = 55;
                    wsnew.get_Range("C32", m).WrapText = true;
                    wsnew.get_Range("C32", m).Font.Name = "Arial";
                    wsnew.get_Range("C32", m).Font.Bold = true;
                    wsnew.get_Range("C32", m).Font.Size = 10;
                    wsnew.get_Range("C32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("C32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("D32", m).Value2 = "Format";
                    wsnew.get_Range("D32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D32", m).Borders.Value = true;
                    wsnew.get_Range("D32", m).ColumnWidth = 55;
                    wsnew.get_Range("D32", m).WrapText = true;
                    wsnew.get_Range("D32", m).Font.Name = "Arial";
                    wsnew.get_Range("D32", m).Font.Bold = true;
                    wsnew.get_Range("D32", m).Font.Size = 10;
                    wsnew.get_Range("D32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("D32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("E32", m).Value2 = "M / O";
                    wsnew.get_Range("E32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E32", m).Borders.Value = true;
                    wsnew.get_Range("E32", m).ColumnWidth = 55;
                    wsnew.get_Range("E32", m).WrapText = true;
                    wsnew.get_Range("E32", m).Font.Name = "Arial";
                    wsnew.get_Range("E32", m).Font.Bold = true;
                    wsnew.get_Range("E32", m).Font.Size = 10;
                    wsnew.get_Range("E32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("E32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("F32", m).Value2 = "Max Length";
                    wsnew.get_Range("F32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F32", m).Borders.Value = true;
                    wsnew.get_Range("F32", m).ColumnWidth = 55;
                    wsnew.get_Range("F32", m).WrapText = true;
                    wsnew.get_Range("F32", m).Font.Name = "Arial";
                    wsnew.get_Range("F32", m).Font.Bold = true;
                    wsnew.get_Range("F32", m).Font.Size = 10;
                    wsnew.get_Range("F32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("F32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    #region Col Name
                    wsnew.get_Range("A33", m).Value2 = "A";
                    wsnew.get_Range("A33", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A33", m).Borders.Value = true;
                    wsnew.get_Range("A33", m).ColumnWidth = 9;
                    wsnew.get_Range("A33", m).Font.Name = "Arial";
                    wsnew.get_Range("A33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A34", m).Value2 = "B";
                    wsnew.get_Range("A34", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A34", m).Borders.Value = true;
                    wsnew.get_Range("A34", m).ColumnWidth = 9;
                    wsnew.get_Range("A34", m).Font.Name = "Arial";
                    wsnew.get_Range("A34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A35", m).Value2 = "C";
                    wsnew.get_Range("A35", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A35", m).Borders.Value = true;
                    wsnew.get_Range("A35", m).ColumnWidth = 9;
                    wsnew.get_Range("A35", m).Font.Name = "Arial";
                    wsnew.get_Range("A35", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A36", m).Value2 = "D";
                    wsnew.get_Range("A36", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A36", m).Borders.Value = true;
                    wsnew.get_Range("A36", m).ColumnWidth = 9;
                    wsnew.get_Range("A36", m).Font.Name = "Arial";
                    wsnew.get_Range("A36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A37", m).Value2 = "E";
                    wsnew.get_Range("A37", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A37", m).Borders.Value = true;
                    wsnew.get_Range("A37", m).ColumnWidth = 9;
                    wsnew.get_Range("A37", m).Font.Name = "Arial";
                    wsnew.get_Range("A37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A38", m).Value2 = "F";
                    wsnew.get_Range("A38", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A38", m).Borders.Value = true;
                    wsnew.get_Range("A38", m).ColumnWidth = 9;
                    wsnew.get_Range("A38", m).Font.Name = "Arial";
                    wsnew.get_Range("A38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A39", m).Value2 = "G";
                    wsnew.get_Range("A39", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A39", m).Borders.Value = true;
                    wsnew.get_Range("A39", m).ColumnWidth = 9;
                    wsnew.get_Range("A39", m).Font.Name = "Arial";
                    wsnew.get_Range("A39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A40", m).Value2 = "H";
                    wsnew.get_Range("A40", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A40", m).Borders.Value = true;
                    wsnew.get_Range("A40", m).ColumnWidth = 9;
                    wsnew.get_Range("A40", m).Font.Name = "Arial";
                    wsnew.get_Range("A40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A41", m).Value2 = "I";
                    wsnew.get_Range("A41", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A41", m).Borders.Value = true;
                    wsnew.get_Range("A41", m).ColumnWidth = 9;
                    wsnew.get_Range("A41", m).Font.Name = "Arial";
                    wsnew.get_Range("A41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A42", m).Value2 = "J";
                    wsnew.get_Range("A42", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A42", m).Borders.Value = true;
                    wsnew.get_Range("A42", m).ColumnWidth = 9;
                    wsnew.get_Range("A42", m).Font.Name = "Arial";
                    wsnew.get_Range("A42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A43", m).Value2 = "K";
                    wsnew.get_Range("A43", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A43", m).Borders.Value = true;
                    wsnew.get_Range("A43", m).ColumnWidth = 9;
                    wsnew.get_Range("A43", m).Font.Name = "Arial";
                    wsnew.get_Range("A43", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A44", m).Value2 = "L";
                    wsnew.get_Range("A44", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A44", m).Borders.Value = true;
                    wsnew.get_Range("A44", m).ColumnWidth = 9;
                    wsnew.get_Range("A44", m).Font.Name = "Arial";
                    wsnew.get_Range("A44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A45", m).Value2 = "M";
                    wsnew.get_Range("A45", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A45", m).Borders.Value = true;
                    wsnew.get_Range("A45", m).ColumnWidth = 9;
                    wsnew.get_Range("A45", m).Font.Name = "Arial";
                    wsnew.get_Range("A45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A46", m).Value2 = "N";
                    wsnew.get_Range("A46", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A46", m).Borders.Value = true;
                    wsnew.get_Range("A46", m).ColumnWidth = 9;
                    wsnew.get_Range("A46", m).Font.Name = "Arial";
                    wsnew.get_Range("A46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A47", m).Value2 = "O";
                    wsnew.get_Range("A47", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A47", m).Borders.Value = true;
                    wsnew.get_Range("A47", m).ColumnWidth = 9;
                    wsnew.get_Range("A47", m).Font.Name = "Arial";
                    wsnew.get_Range("A47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A48", m).Value2 = "P";
                    wsnew.get_Range("A48", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A48", m).Borders.Value = true;
                    wsnew.get_Range("A48", m).ColumnWidth = 9;
                    wsnew.get_Range("A48", m).Font.Name = "Arial";
                    wsnew.get_Range("A48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A49", m).Value2 = "Q";
                    wsnew.get_Range("A49", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A49", m).Borders.Value = true;
                    wsnew.get_Range("A49", m).ColumnWidth = 9;
                    wsnew.get_Range("A49", m).Font.Name = "Arial";
                    wsnew.get_Range("A49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A50", m).Value2 = "R";
                    wsnew.get_Range("A50", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A50", m).Borders.Value = true;
                    wsnew.get_Range("A50", m).ColumnWidth = 9;
                    wsnew.get_Range("A50", m).Font.Name = "Arial";
                    wsnew.get_Range("A50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A51", m).Value2 = "S";
                    wsnew.get_Range("A51", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A51", m).Borders.Value = true;
                    wsnew.get_Range("A51", m).ColumnWidth = 9;
                    wsnew.get_Range("A51", m).Font.Name = "Arial";
                    wsnew.get_Range("A51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A52", m).Value2 = "T";
                    wsnew.get_Range("A52", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A52", m).Borders.Value = true;
                    wsnew.get_Range("A52", m).ColumnWidth = 9;
                    wsnew.get_Range("A52", m).Font.Name = "Arial";
                    wsnew.get_Range("A52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A53", m).Value2 = "U";
                    wsnew.get_Range("A53", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A53", m).Borders.Value = true;
                    wsnew.get_Range("A53", m).ColumnWidth = 9;
                    wsnew.get_Range("A53", m).Font.Name = "Arial";
                    wsnew.get_Range("A53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A54", m).Value2 = "V";
                    wsnew.get_Range("A54", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A54", m).Borders.Value = true;
                    wsnew.get_Range("A54", m).ColumnWidth = 9;
                    wsnew.get_Range("A54", m).Font.Name = "Arial";
                    wsnew.get_Range("A54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A55", m).Value2 = "W";
                    wsnew.get_Range("A55", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A55", m).Borders.Value = true;
                    wsnew.get_Range("A55", m).ColumnWidth = 9;
                    wsnew.get_Range("A55", m).Font.Name = "Arial";
                    wsnew.get_Range("A55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A56", m).Value2 = "X";
                    wsnew.get_Range("A56", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A56", m).Borders.Value = true;
                    wsnew.get_Range("A56", m).ColumnWidth = 9;
                    wsnew.get_Range("A56", m).Font.Name = "Arial";
                    wsnew.get_Range("A56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A57", m).Value2 = "Y";
                    wsnew.get_Range("A57", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A57", m).Borders.Value = true;
                    wsnew.get_Range("A57", m).ColumnWidth = 9;
                    wsnew.get_Range("A57", m).Font.Name = "Arial";
                    wsnew.get_Range("A57", m).Font.Size = 10;
                    //
                    #endregion
                    //
                    #region Column Header
                    wsnew.get_Range("B33", m).Value2 = "Employee Serial No (327)";
                    wsnew.get_Range("B33", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B33", m).Borders.Value = true;
                    wsnew.get_Range("B33", m).ColumnWidth = 28;
                    wsnew.get_Range("B33", m).Font.Name = "Arial";
                    wsnew.get_Range("B33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B34", m).Value2 = "PAN of the Employee (328)";
                    wsnew.get_Range("B34", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B34", m).Borders.Value = true;
                    wsnew.get_Range("B34", m).ColumnWidth = 28;
                    wsnew.get_Range("B34", m).Font.Name = "Arial";
                    wsnew.get_Range("B34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B35", m).Value2 = "Name of the Employee (329)";
                    wsnew.get_Range("B35", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B35", m).Borders.Value = true;
                    wsnew.get_Range("B35", m).ColumnWidth = 28;
                    wsnew.get_Range("B35", m).Font.Name = "Arial";
                    wsnew.get_Range("B35", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B36", m).Value2 = "Category of the Employee (330)";
                    wsnew.get_Range("B36", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B36", m).Borders.Value = true;
                    wsnew.get_Range("B36", m).ColumnWidth = 28;
                    wsnew.get_Range("B36", m).Font.Name = "Arial";
                    wsnew.get_Range("B36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B37", m).Value2 = "Period of employment : From Date (dd/mm/yyyy) (331)";
                    wsnew.get_Range("B37", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B37", m).Borders.Value = true;
                    wsnew.get_Range("B37", m).ColumnWidth = 28;
                    wsnew.get_Range("B37", m).Font.Name = "Arial";
                    wsnew.get_Range("B37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B38", m).Value2 = "Period of employment : To Date (dd/mm/yyyy) (331)";
                    wsnew.get_Range("B38", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B38", m).Borders.Value = true;
                    wsnew.get_Range("B38", m).ColumnWidth = 28;
                    wsnew.get_Range("B38", m).Font.Name = "Arial";
                    wsnew.get_Range("B38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B39", m).Value2 = "Total Salary(332)";
                    wsnew.get_Range("B39", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B39", m).Borders.Value = true;
                    wsnew.get_Range("B39", m).ColumnWidth = 28;
                    wsnew.get_Range("B39", m).Font.Name = "Arial";
                    wsnew.get_Range("B39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B40", m).Value2 = "Gross Deduction under section 16(ii)";
                    wsnew.get_Range("B40", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B40", m).Borders.Value = true;
                    wsnew.get_Range("B40", m).ColumnWidth = 28;
                    wsnew.get_Range("B40", m).Font.Name = "Arial";
                    wsnew.get_Range("B40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B41", m).Value2 = "Gross Deduction under section 16(iii)";
                    wsnew.get_Range("B41", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B41", m).Borders.Value = true;
                    wsnew.get_Range("B41", m).ColumnWidth = 28;
                    wsnew.get_Range("B41", m).Font.Name = "Arial";
                    wsnew.get_Range("B41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B42", m).Value2 = "Gross Total Deduction under section 16(iii) (333)";
                    wsnew.get_Range("B42", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B42", m).Borders.Value = true;
                    wsnew.get_Range("B42", m).ColumnWidth = 28;
                    wsnew.get_Range("B42", m).Font.Name = "Arial";
                    wsnew.get_Range("B42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B43", m).Value2 = "Income Chargeable under head Salaries(334)";
                    wsnew.get_Range("B43", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B43", m).Borders.Value = true;
                    wsnew.get_Range("B43", m).ColumnWidth = 28;
                    wsnew.get_Range("B43", m).Font.Name = "Arial";
                    wsnew.get_Range("B33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B44", m).Value2 = "Income other than Salary (335)";
                    wsnew.get_Range("B44", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B44", m).Borders.Value = true;
                    wsnew.get_Range("B44", m).ColumnWidth = 28;
                    wsnew.get_Range("B44", m).Font.Name = "Arial";
                    wsnew.get_Range("B44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B45", m).Value2 = "Gross Total Income(336)";
                    wsnew.get_Range("B45", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B45", m).Borders.Value = true;
                    wsnew.get_Range("B45", m).ColumnWidth = 28;
                    wsnew.get_Range("B45", m).Font.Name = "Arial";
                    wsnew.get_Range("B45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B46", m).Value2 = "Deduction under Chapter VIA under section 80CCE";
                    wsnew.get_Range("B46", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B46", m).Borders.Value = true;
                    wsnew.get_Range("B46", m).ColumnWidth = 28;
                    wsnew.get_Range("B46", m).Font.Name = "Arial";
                    wsnew.get_Range("B46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B47", m).Value2 = "Deduction under Chapter VIA under section 80CCF";
                    wsnew.get_Range("B47", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B47", m).Borders.Value = true;
                    wsnew.get_Range("B47", m).ColumnWidth = 28;
                    wsnew.get_Range("B47", m).Font.Name = "Arial";
                    wsnew.get_Range("B47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B48", m).Value2 = "Deduction under Chapter VIA under Other sections";
                    wsnew.get_Range("B48", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B48", m).Borders.Value = true;
                    wsnew.get_Range("B48", m).ColumnWidth = 28;
                    wsnew.get_Range("B48", m).Font.Name = "Arial";
                    wsnew.get_Range("B48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B49", m).Value2 = "Gross Total Deduction under chapter VIA (339)";
                    wsnew.get_Range("B49", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B49", m).Borders.Value = true;
                    wsnew.get_Range("B49", m).ColumnWidth = 28;
                    wsnew.get_Range("B49", m).Font.Name = "Arial";
                    wsnew.get_Range("B49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B50", m).Value2 = "Total Taxable Income (340)";
                    wsnew.get_Range("B50", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B50", m).Borders.Value = true;
                    wsnew.get_Range("B50", m).ColumnWidth = 28;
                    wsnew.get_Range("B50", m).Font.Name = "Arial";
                    wsnew.get_Range("B50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B51", m).Value2 = "Income Tax on Total Income (341)";
                    wsnew.get_Range("B51", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B51", m).Borders.Value = true;
                    wsnew.get_Range("B51", m).ColumnWidth = 28;
                    wsnew.get_Range("B51", m).Font.Name = "Arial";
                    wsnew.get_Range("B51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B52", m).Value2 = "Surcharge (342)";
                    wsnew.get_Range("B52", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B52", m).Borders.Value = true;
                    wsnew.get_Range("B52", m).ColumnWidth = 28;
                    wsnew.get_Range("B52", m).Font.Name = "Arial";
                    wsnew.get_Range("B52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B53", m).Value2 = "Education Cess (343)";
                    wsnew.get_Range("B53", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B53", m).Borders.Value = true;
                    wsnew.get_Range("B53", m).ColumnWidth = 28;
                    wsnew.get_Range("B53", m).Font.Name = "Arial";
                    wsnew.get_Range("B53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B54", m).Value2 = "Income Tax Releif under section 89 (344)";
                    wsnew.get_Range("B54", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B54", m).Borders.Value = true;
                    wsnew.get_Range("B54", m).ColumnWidth = 28;
                    wsnew.get_Range("B54", m).Font.Name = "Arial";
                    wsnew.get_Range("B54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B55", m).Value2 = "Net Tax Payable (345)";
                    wsnew.get_Range("B55", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B55", m).Borders.Value = true;
                    wsnew.get_Range("B55", m).ColumnWidth = 28;
                    wsnew.get_Range("B55", m).Font.Name = "Arial";
                    wsnew.get_Range("B55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B56", m).Value2 = "Total TDS Deducted (346)";
                    wsnew.get_Range("B56", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B56", m).Borders.Value = true;
                    wsnew.get_Range("B56", m).ColumnWidth = 28;
                    wsnew.get_Range("B56", m).Font.Name = "Arial";
                    wsnew.get_Range("B56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B57", m).Value2 = "Shortfall/Excess Deduction of Tax(347)";
                    wsnew.get_Range("B57", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B57", m).Borders.Value = true;
                    wsnew.get_Range("B57", m).ColumnWidth = 28;
                    wsnew.get_Range("B57", m).Font.Name = "Arial";
                    wsnew.get_Range("B57", m).Font.Size = 10;
                    //
                    #endregion
                    //
                    #region Description
                    wsnew.get_Range("C33", m).Value2 = "Running serial no to indicate detail record no.";
                    wsnew.get_Range("C33", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C33", m).Borders.Value = true;
                    wsnew.get_Range("C33", m).ColumnWidth = 110;
                    wsnew.get_Range("C33", m).Font.Name = "Arial";
                    wsnew.get_Range("C33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C34", m).Value2 = "PAN of the employee.";
                    wsnew.get_Range("C34", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C34", m).Borders.Value = true;
                    wsnew.get_Range("C34", m).ColumnWidth = 110;
                    wsnew.get_Range("C34", m).Font.Name = "Arial";
                    wsnew.get_Range("C34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C35", m).Value2 = "Mention the Name of the employee.";
                    wsnew.get_Range("C35", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C35", m).Borders.Value = true;
                    wsnew.get_Range("C35", m).ColumnWidth = 110;
                    wsnew.get_Range("C35", m).Font.Name = "Arial";
                    wsnew.get_Range("C35", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C36", m).Value2 = "Mention the Category of the employee. G for General, W for Woman, S for Senior Citizen.";
                    wsnew.get_Range("C36", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C36", m).Borders.Value = true;
                    wsnew.get_Range("C36", m).ColumnWidth = 110;
                    wsnew.get_Range("C36", m).Font.Name = "Arial";
                    wsnew.get_Range("C36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C37", m).Value2 = "Mention the From date of period of employment.";
                    wsnew.get_Range("C37", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C37", m).Borders.Value = true;
                    wsnew.get_Range("C37", m).ColumnWidth = 110;
                    wsnew.get_Range("C37", m).Font.Name = "Arial";
                    wsnew.get_Range("C37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C38", m).Value2 = "Mention the From to of period of employment.";
                    wsnew.get_Range("C38", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C38", m).Borders.Value = true;
                    wsnew.get_Range("C38", m).ColumnWidth = 110;
                    wsnew.get_Range("C38", m).Font.Name = "Arial";
                    wsnew.get_Range("C38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C39", m).Value2 = "Mention the Total Salary of the Employee.";
                    wsnew.get_Range("C39", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C39", m).Borders.Value = true;
                    wsnew.get_Range("C39", m).ColumnWidth = 110;
                    wsnew.get_Range("C39", m).Font.Name = "Arial";
                    wsnew.get_Range("C39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C40", m).Value2 = "Mention the Gross Deduction under section 16(ii) of the Employee.";
                    wsnew.get_Range("C40", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C40", m).Borders.Value = true;
                    wsnew.get_Range("C40", m).ColumnWidth = 110;
                    wsnew.get_Range("C40", m).Font.Name = "Arial";
                    wsnew.get_Range("C40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C41", m).Value2 = "Mention the Total Salary of the Employee.";
                    wsnew.get_Range("C41", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C41", m).Borders.Value = true;
                    wsnew.get_Range("C41", m).ColumnWidth = 110;
                    wsnew.get_Range("C41", m).Font.Name = "Arial";
                    wsnew.get_Range("C41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C42", m).Value2 = "Mention the Gross Deduction under section 16(iii) of the Employee.";
                    wsnew.get_Range("C42", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C42", m).Borders.Value = true;
                    wsnew.get_Range("C42", m).ColumnWidth = 110;
                    wsnew.get_Range("C42", m).Font.Name = "Arial";
                    wsnew.get_Range("C42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C43", m).Value2 = "This field shows Income Chargeable under head Salaries.";
                    wsnew.get_Range("C43", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C43", m).Borders.Value = true;
                    wsnew.get_Range("C43", m).ColumnWidth = 110;
                    wsnew.get_Range("C43", m).Font.Name = "Arial";
                    wsnew.get_Range("C33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C44", m).Value2 = "Mention the Income other than Salary of the Employee.";
                    wsnew.get_Range("C44", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C44", m).Borders.Value = true;
                    wsnew.get_Range("C44", m).ColumnWidth = 110;
                    wsnew.get_Range("C44", m).Font.Name = "Arial";
                    wsnew.get_Range("C44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C45", m).Value2 = "This field shows Gross Total Income under head Salaries.";
                    wsnew.get_Range("C45", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C45", m).Borders.Value = true;
                    wsnew.get_Range("C45", m).ColumnWidth = 110;
                    wsnew.get_Range("C45", m).Font.Name = "Arial";
                    wsnew.get_Range("C45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C46", m).Value2 = "Mention the Deduction under Chapter VIA under section 80CCE of the Employee.";
                    wsnew.get_Range("C46", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C46", m).Borders.Value = true;
                    wsnew.get_Range("C46", m).ColumnWidth = 110;
                    wsnew.get_Range("C46", m).Font.Name = "Arial";
                    wsnew.get_Range("C46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C47", m).Value2 = "Mention the Deduction under Chapter VIA under section 80CCF of the Employee.";
                    wsnew.get_Range("C47", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C47", m).Borders.Value = true;
                    wsnew.get_Range("C47", m).ColumnWidth = 110;
                    wsnew.get_Range("C47", m).Font.Name = "Arial";
                    wsnew.get_Range("C47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C48", m).Value2 = "Mention the Deduction under Chapter VIA under Other sections of the Employee.";
                    wsnew.get_Range("C48", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C48", m).Borders.Value = true;
                    wsnew.get_Range("C48", m).ColumnWidth = 110;
                    wsnew.get_Range("C48", m).Font.Name = "Arial";
                    wsnew.get_Range("C48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C49", m).Value2 = "This field shows Gross Total Deduction under chapter VIA under head Salaries.";
                    wsnew.get_Range("C49", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C49", m).Borders.Value = true;
                    wsnew.get_Range("C49", m).ColumnWidth = 110;
                    wsnew.get_Range("C49", m).Font.Name = "Arial";
                    wsnew.get_Range("C49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C50", m).Value2 = "This field shows Gross Total Taxable Income under chapter VIA under head Salaries.";
                    wsnew.get_Range("C50", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C50", m).Borders.Value = true;
                    wsnew.get_Range("C50", m).ColumnWidth = 110;
                    wsnew.get_Range("C50", m).Font.Name = "Arial";
                    wsnew.get_Range("C50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C51", m).Value2 = "Mention the Income Tax on Total Income of the Employee.";
                    wsnew.get_Range("C51", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C51", m).Borders.Value = true;
                    wsnew.get_Range("C51", m).ColumnWidth = 110;
                    wsnew.get_Range("C51", m).Font.Name = "Arial";
                    wsnew.get_Range("C51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C52", m).Value2 = "Mention the Surcharge of the Employee.";
                    wsnew.get_Range("C52", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C52", m).Borders.Value = true;
                    wsnew.get_Range("C52", m).ColumnWidth = 110;
                    wsnew.get_Range("C52", m).Font.Name = "Arial";
                    wsnew.get_Range("C52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C53", m).Value2 = "Mention the Education Cess of the Employee.";
                    wsnew.get_Range("C53", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C53", m).Borders.Value = true;
                    wsnew.get_Range("C53", m).ColumnWidth = 110;
                    wsnew.get_Range("C53", m).Font.Name = "Arial";
                    wsnew.get_Range("C53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C54", m).Value2 = "Mention the Income Tax Releif under section 89 of the Employee.";
                    wsnew.get_Range("C54", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C54", m).Borders.Value = true;
                    wsnew.get_Range("C54", m).ColumnWidth = 110;
                    wsnew.get_Range("C54", m).Font.Name = "Arial";
                    wsnew.get_Range("C54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C55", m).Value2 = "Mention the Net Tax Payable of the Employee.";
                    wsnew.get_Range("C55", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C55", m).Borders.Value = true;
                    wsnew.get_Range("C55", m).ColumnWidth = 110;
                    wsnew.get_Range("C55", m).Font.Name = "Arial";
                    wsnew.get_Range("C55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C56", m).Value2 = "Mention the Total TDS Deducted  of the Employee.";
                    wsnew.get_Range("C56", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C56", m).Borders.Value = true;
                    wsnew.get_Range("C56", m).ColumnWidth = 110;
                    wsnew.get_Range("C56", m).Font.Name = "Arial";
                    wsnew.get_Range("C56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C57", m).Value2 = "This field shows Shortfall/Excess Deduction of Tax.";
                    wsnew.get_Range("C57", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C57", m).Borders.Value = true;
                    wsnew.get_Range("C57", m).ColumnWidth = 110;
                    wsnew.get_Range("C57", m).Font.Name = "Arial";
                    wsnew.get_Range("C57", m).Font.Size = 10;
                    //
                    #endregion
                    //
                    #region Format
                    wsnew.get_Range("D33", m).Value2 = "Numeric";
                    wsnew.get_Range("D33", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D33", m).Borders.Value = true;
                    wsnew.get_Range("D33", m).ColumnWidth = 9;
                    wsnew.get_Range("D33", m).Font.Name = "Drial";
                    wsnew.get_Range("D33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D34", m).Value2 = "Text";
                    wsnew.get_Range("D34", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D34", m).Borders.Value = true;
                    wsnew.get_Range("D34", m).ColumnWidth = 9;
                    wsnew.get_Range("D34", m).Font.Name = "Drial";
                    wsnew.get_Range("D34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D35", m).Value2 = "Text";
                    wsnew.get_Range("D35", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D35", m).Borders.Value = true;
                    wsnew.get_Range("D35", m).ColumnWidth = 9;
                    wsnew.get_Range("D35", m).Font.Name = "Drial";
                    wsnew.get_Range("D35", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D36", m).Value2 = "Numeric";
                    wsnew.get_Range("D36", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D36", m).Borders.Value = true;
                    wsnew.get_Range("D36", m).ColumnWidth = 9;
                    wsnew.get_Range("D36", m).Font.Name = "Drial";
                    wsnew.get_Range("D36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D37", m).Value2 = "Date";
                    wsnew.get_Range("D37", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D37", m).Borders.Value = true;
                    wsnew.get_Range("D37", m).ColumnWidth = 9;
                    wsnew.get_Range("D37", m).Font.Name = "Drial";
                    wsnew.get_Range("D37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D38", m).Value2 = "Date";
                    wsnew.get_Range("D38", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D38", m).Borders.Value = true;
                    wsnew.get_Range("D38", m).ColumnWidth = 9;
                    wsnew.get_Range("D38", m).Font.Name = "Drial";
                    wsnew.get_Range("D38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D39", m).Value2 = "Numeric";
                    wsnew.get_Range("D39", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D39", m).Borders.Value = true;
                    wsnew.get_Range("D39", m).ColumnWidth = 9;
                    wsnew.get_Range("D39", m).Font.Name = "Drial";
                    wsnew.get_Range("D39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D40", m).Value2 = "Numeric";
                    wsnew.get_Range("D40", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D40", m).Borders.Value = true;
                    wsnew.get_Range("D40", m).ColumnWidth = 9;
                    wsnew.get_Range("D40", m).Font.Name = "Drial";
                    wsnew.get_Range("D40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D41", m).Value2 = "Numeric";
                    wsnew.get_Range("D41", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D41", m).Borders.Value = true;
                    wsnew.get_Range("D41", m).ColumnWidth = 9;
                    wsnew.get_Range("D41", m).Font.Name = "Drial";
                    wsnew.get_Range("D41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D42", m).Value2 = "Numeric";
                    wsnew.get_Range("D42", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D42", m).Borders.Value = true;
                    wsnew.get_Range("D42", m).ColumnWidth = 9;
                    wsnew.get_Range("D42", m).Font.Name = "Drial";
                    wsnew.get_Range("D42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D43", m).Value2 = "Numeric";
                    wsnew.get_Range("D43", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D43", m).Borders.Value = true;
                    wsnew.get_Range("D43", m).ColumnWidth = 9;
                    wsnew.get_Range("D43", m).Font.Name = "Drial";
                    wsnew.get_Range("D43", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D44", m).Value2 = "Numeric";
                    wsnew.get_Range("D44", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D44", m).Borders.Value = true;
                    wsnew.get_Range("D44", m).ColumnWidth = 9;
                    wsnew.get_Range("D44", m).Font.Name = "Drial";
                    wsnew.get_Range("D44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D45", m).Value2 = "Numeric";
                    wsnew.get_Range("D45", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D45", m).Borders.Value = true;
                    wsnew.get_Range("D45", m).ColumnWidth = 9;
                    wsnew.get_Range("D45", m).Font.Name = "Drial";
                    wsnew.get_Range("D45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D46", m).Value2 = "Numeric";
                    wsnew.get_Range("D46", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D46", m).Borders.Value = true;
                    wsnew.get_Range("D46", m).ColumnWidth = 9;
                    wsnew.get_Range("D46", m).Font.Name = "Drial";
                    wsnew.get_Range("D46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D47", m).Value2 = "Numeric";
                    wsnew.get_Range("D47", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D47", m).Borders.Value = true;
                    wsnew.get_Range("D47", m).ColumnWidth = 9;
                    wsnew.get_Range("D47", m).Font.Name = "Drial";
                    wsnew.get_Range("D47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D48", m).Value2 = "Numeric";
                    wsnew.get_Range("D48", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D48", m).Borders.Value = true;
                    wsnew.get_Range("D48", m).ColumnWidth = 9;
                    wsnew.get_Range("D48", m).Font.Name = "Drial";
                    wsnew.get_Range("D48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D49", m).Value2 = "Numeric";
                    wsnew.get_Range("D49", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D49", m).Borders.Value = true;
                    wsnew.get_Range("D49", m).ColumnWidth = 9;
                    wsnew.get_Range("D49", m).Font.Name = "Drial";
                    wsnew.get_Range("D49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D50", m).Value2 = "Numeric";
                    wsnew.get_Range("D50", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D50", m).Borders.Value = true;
                    wsnew.get_Range("D50", m).ColumnWidth = 9;
                    wsnew.get_Range("D50", m).Font.Name = "Drial";
                    wsnew.get_Range("D50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D51", m).Value2 = "Numeric";
                    wsnew.get_Range("D51", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D51", m).Borders.Value = true;
                    wsnew.get_Range("D51", m).ColumnWidth = 9;
                    wsnew.get_Range("D51", m).Font.Name = "Drial";
                    wsnew.get_Range("D51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D52", m).Value2 = "Numeric";
                    wsnew.get_Range("D52", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D52", m).Borders.Value = true;
                    wsnew.get_Range("D52", m).ColumnWidth = 9;
                    wsnew.get_Range("D52", m).Font.Name = "Drial";
                    wsnew.get_Range("D52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D53", m).Value2 = "Numeric";
                    wsnew.get_Range("D53", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D53", m).Borders.Value = true;
                    wsnew.get_Range("D53", m).ColumnWidth = 9;
                    wsnew.get_Range("D53", m).Font.Name = "Drial";
                    wsnew.get_Range("D53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D54", m).Value2 = "Numeric";
                    wsnew.get_Range("D54", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D54", m).Borders.Value = true;
                    wsnew.get_Range("D54", m).ColumnWidth = 9;
                    wsnew.get_Range("D54", m).Font.Name = "Drial";
                    wsnew.get_Range("D54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D55", m).Value2 = "Numeric";
                    wsnew.get_Range("D55", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D55", m).Borders.Value = true;
                    wsnew.get_Range("D55", m).ColumnWidth = 9;
                    wsnew.get_Range("D55", m).Font.Name = "Drial";
                    wsnew.get_Range("D55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D56", m).Value2 = "Numeric";
                    wsnew.get_Range("D56", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D56", m).Borders.Value = true;
                    wsnew.get_Range("D56", m).ColumnWidth = 9;
                    wsnew.get_Range("D56", m).Font.Name = "Drial";
                    wsnew.get_Range("D56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D57", m).Value2 = "Numeric";
                    wsnew.get_Range("D57", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D57", m).Borders.Value = true;
                    wsnew.get_Range("D57", m).ColumnWidth = 9;
                    wsnew.get_Range("D57", m).Font.Name = "Drial";
                    wsnew.get_Range("D57", m).Font.Size = 10;
                    //
                    #endregion
                    //
                    #region M / O
                    wsnew.get_Range("E33", m).Value2 = "Mandatory";
                    wsnew.get_Range("E33", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E33", m).Borders.Value = true;
                    wsnew.get_Range("E33", m).ColumnWidth = 9;
                    wsnew.get_Range("E33", m).Font.Name = "Arial";
                    wsnew.get_Range("E33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E34", m).Value2 = "Mandatory";
                    wsnew.get_Range("E34", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E34", m).Borders.Value = true;
                    wsnew.get_Range("E34", m).ColumnWidth = 9;
                    wsnew.get_Range("E34", m).Font.Name = "Arial";
                    wsnew.get_Range("E34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E35", m).Value2 = "Mandatory";
                    wsnew.get_Range("E35", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E35", m).Borders.Value = true;
                    wsnew.get_Range("E35", m).ColumnWidth = 9;
                    wsnew.get_Range("E35", m).Font.Name = "Arial";
                    wsnew.get_Range("E35", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E36", m).Value2 = "Mandatory";
                    wsnew.get_Range("E36", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E36", m).Borders.Value = true;
                    wsnew.get_Range("E36", m).ColumnWidth = 9;
                    wsnew.get_Range("E36", m).Font.Name = "Arial";
                    wsnew.get_Range("E36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E37", m).Value2 = "Mandatory";
                    wsnew.get_Range("E37", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E37", m).Borders.Value = true;
                    wsnew.get_Range("E37", m).ColumnWidth = 9;
                    wsnew.get_Range("E37", m).Font.Name = "Arial";
                    wsnew.get_Range("E37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E38", m).Value2 = "Mandatory";
                    wsnew.get_Range("E38", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E38", m).Borders.Value = true;
                    wsnew.get_Range("E38", m).ColumnWidth = 9;
                    wsnew.get_Range("E38", m).Font.Name = "Arial";
                    wsnew.get_Range("E38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E39", m).Value2 = "Mandatory";
                    wsnew.get_Range("E39", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E39", m).Borders.Value = true;
                    wsnew.get_Range("E39", m).ColumnWidth = 9;
                    wsnew.get_Range("E39", m).Font.Name = "Arial";
                    wsnew.get_Range("E39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E40", m).Value2 = "Optional";
                    wsnew.get_Range("E40", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E40", m).Borders.Value = true;
                    wsnew.get_Range("E40", m).ColumnWidth = 9;
                    wsnew.get_Range("E40", m).Font.Name = "Arial";
                    wsnew.get_Range("E40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E41", m).Value2 = "Optional";
                    wsnew.get_Range("E41", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E41", m).Borders.Value = true;
                    wsnew.get_Range("E41", m).ColumnWidth = 9;
                    wsnew.get_Range("E41", m).Font.Name = "Arial";
                    wsnew.get_Range("E41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E42", m).Value2 = "Optional";
                    wsnew.get_Range("E42", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E42", m).Borders.Value = true;
                    wsnew.get_Range("E42", m).ColumnWidth = 9;
                    wsnew.get_Range("E42", m).Font.Name = "Arial";
                    wsnew.get_Range("E42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E43", m).Value2 = "Mandatory";
                    wsnew.get_Range("E43", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E43", m).Borders.Value = true;
                    wsnew.get_Range("E43", m).ColumnWidth = 9;
                    wsnew.get_Range("E43", m).Font.Name = "Arial";
                    wsnew.get_Range("E43", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E44", m).Value2 = "Optional";
                    wsnew.get_Range("E44", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E44", m).Borders.Value = true;
                    wsnew.get_Range("E44", m).ColumnWidth = 9;
                    wsnew.get_Range("E44", m).Font.Name = "Arial";
                    wsnew.get_Range("E44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E45", m).Value2 = "Mandatory";
                    wsnew.get_Range("E45", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E45", m).Borders.Value = true;
                    wsnew.get_Range("E45", m).ColumnWidth = 9;
                    wsnew.get_Range("E45", m).Font.Name = "Arial";
                    wsnew.get_Range("E45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E46", m).Value2 = "Optional";
                    wsnew.get_Range("E46", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E46", m).Borders.Value = true;
                    wsnew.get_Range("E46", m).ColumnWidth = 9;
                    wsnew.get_Range("E46", m).Font.Name = "Arial";
                    wsnew.get_Range("E46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E47", m).Value2 = "Optional";
                    wsnew.get_Range("E47", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E47", m).Borders.Value = true;
                    wsnew.get_Range("E47", m).ColumnWidth = 9;
                    wsnew.get_Range("E47", m).Font.Name = "Arial";
                    wsnew.get_Range("E47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E48", m).Value2 = "Optional";
                    wsnew.get_Range("E48", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E48", m).Borders.Value = true;
                    wsnew.get_Range("E48", m).ColumnWidth = 9;
                    wsnew.get_Range("E48", m).Font.Name = "Arial";
                    wsnew.get_Range("E48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E49", m).Value2 = "Optional";
                    wsnew.get_Range("E49", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E49", m).Borders.Value = true;
                    wsnew.get_Range("E49", m).ColumnWidth = 9;
                    wsnew.get_Range("E49", m).Font.Name = "Arial";
                    wsnew.get_Range("E49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E50", m).Value2 = "Mandatory";
                    wsnew.get_Range("E50", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E50", m).Borders.Value = true;
                    wsnew.get_Range("E50", m).ColumnWidth = 9;
                    wsnew.get_Range("E50", m).Font.Name = "Arial";
                    wsnew.get_Range("E50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E51", m).Value2 = "Optional";
                    wsnew.get_Range("E51", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E51", m).Borders.Value = true;
                    wsnew.get_Range("E51", m).ColumnWidth = 9;
                    wsnew.get_Range("E51", m).Font.Name = "Arial";
                    wsnew.get_Range("E51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E52", m).Value2 = "Optional";
                    wsnew.get_Range("E52", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E52", m).Borders.Value = true;
                    wsnew.get_Range("E52", m).ColumnWidth = 9;
                    wsnew.get_Range("E52", m).Font.Name = "Arial";
                    wsnew.get_Range("E52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E53", m).Value2 = "Optional";
                    wsnew.get_Range("E53", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E53", m).Borders.Value = true;
                    wsnew.get_Range("E53", m).ColumnWidth = 9;
                    wsnew.get_Range("E53", m).Font.Name = "Arial";
                    wsnew.get_Range("E53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E54", m).Value2 = "Optional";
                    wsnew.get_Range("E54", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E54", m).Borders.Value = true;
                    wsnew.get_Range("E54", m).ColumnWidth = 9;
                    wsnew.get_Range("E54", m).Font.Name = "Arial";
                    wsnew.get_Range("E54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E55", m).Value2 = "Optional";
                    wsnew.get_Range("E55", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E55", m).Borders.Value = true;
                    wsnew.get_Range("E55", m).ColumnWidth = 9;
                    wsnew.get_Range("E55", m).Font.Name = "Arial";
                    wsnew.get_Range("E55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E56", m).Value2 = "Optional";
                    wsnew.get_Range("E56", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E56", m).Borders.Value = true;
                    wsnew.get_Range("E56", m).ColumnWidth = 9;
                    wsnew.get_Range("E56", m).Font.Name = "Arial";
                    wsnew.get_Range("E56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E57", m).Value2 = "Optional";
                    wsnew.get_Range("E57", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E57", m).Borders.Value = true;
                    wsnew.get_Range("E57", m).ColumnWidth = 9;
                    wsnew.get_Range("E57", m).Font.Name = "Arial";
                    wsnew.get_Range("E57", m).Font.Size = 10;
                    //
                    #endregion
                    //
                    #region Max Length
                    wsnew.get_Range("F33", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F33", m).Borders.Value = true;
                    wsnew.get_Range("F33", m).ColumnWidth = 9;
                    wsnew.get_Range("F33", m).Font.Name = "Arial";
                    wsnew.get_Range("F33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F34", m).Value2 = "10";
                    wsnew.get_Range("F34", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F34", m).Borders.Value = true;
                    wsnew.get_Range("F34", m).ColumnWidth = 9;
                    wsnew.get_Range("F34", m).Font.Name = "Arial";
                    wsnew.get_Range("F34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F35", m).Value2 = "75";
                    wsnew.get_Range("F35", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F35", m).Borders.Value = true;
                    wsnew.get_Range("F35", m).ColumnWidth = 9;
                    wsnew.get_Range("F35", m).Font.Name = "Arial";
                    wsnew.get_Range("F35", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F36", m).Value2 = "1";
                    wsnew.get_Range("F36", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F36", m).Borders.Value = true;
                    wsnew.get_Range("F36", m).ColumnWidth = 9;
                    wsnew.get_Range("F36", m).Font.Name = "Arial";
                    wsnew.get_Range("F36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F37", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F37", m).Borders.Value = true;
                    wsnew.get_Range("F37", m).ColumnWidth = 9;
                    wsnew.get_Range("F37", m).Font.Name = "Arial";
                    wsnew.get_Range("F37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F38", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F38", m).Borders.Value = true;
                    wsnew.get_Range("F38", m).ColumnWidth = 9;
                    wsnew.get_Range("F38", m).Font.Name = "Arial";
                    wsnew.get_Range("F38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F39", m).Value2 = "15";
                    wsnew.get_Range("F39", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F39", m).Borders.Value = true;
                    wsnew.get_Range("F39", m).ColumnWidth = 9;
                    wsnew.get_Range("F39", m).Font.Name = "Arial";
                    wsnew.get_Range("F39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F40", m).Value2 = "15";
                    wsnew.get_Range("F40", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F40", m).Borders.Value = true;
                    wsnew.get_Range("F40", m).ColumnWidth = 9;
                    wsnew.get_Range("F40", m).Font.Name = "Arial";
                    wsnew.get_Range("F40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F41", m).Value2 = "15";
                    wsnew.get_Range("F41", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F41", m).Borders.Value = true;
                    wsnew.get_Range("F41", m).ColumnWidth = 9;
                    wsnew.get_Range("F41", m).Font.Name = "Arial";
                    wsnew.get_Range("F41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F42", m).Value2 = "15";
                    wsnew.get_Range("F42", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F42", m).Borders.Value = true;
                    wsnew.get_Range("F42", m).ColumnWidth = 9;
                    wsnew.get_Range("F42", m).Font.Name = "Arial";
                    wsnew.get_Range("F42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F43", m).Value2 = "15";
                    wsnew.get_Range("F43", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F43", m).Borders.Value = true;
                    wsnew.get_Range("F43", m).ColumnWidth = 9;
                    wsnew.get_Range("F43", m).Font.Name = "Arial";
                    wsnew.get_Range("F43", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F44", m).Value2 = "15";
                    wsnew.get_Range("F44", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F44", m).Borders.Value = true;
                    wsnew.get_Range("F44", m).ColumnWidth = 9;
                    wsnew.get_Range("F44", m).Font.Name = "Arial";
                    wsnew.get_Range("F44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F45", m).Value2 = "15";
                    wsnew.get_Range("F45", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F45", m).Borders.Value = true;
                    wsnew.get_Range("F45", m).ColumnWidth = 9;
                    wsnew.get_Range("F45", m).Font.Name = "Arial";
                    wsnew.get_Range("F45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F46", m).Value2 = "15";
                    wsnew.get_Range("F46", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F46", m).Borders.Value = true;
                    wsnew.get_Range("F46", m).ColumnWidth = 9;
                    wsnew.get_Range("F46", m).Font.Name = "Arial";
                    wsnew.get_Range("F46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F47", m).Value2 = "15";
                    wsnew.get_Range("F47", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F47", m).Borders.Value = true;
                    wsnew.get_Range("F47", m).ColumnWidth = 9;
                    wsnew.get_Range("F47", m).Font.Name = "Arial";
                    wsnew.get_Range("F47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F48", m).Value2 = "15";
                    wsnew.get_Range("F48", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F48", m).Borders.Value = true;
                    wsnew.get_Range("F48", m).ColumnWidth = 9;
                    wsnew.get_Range("F48", m).Font.Name = "Arial";
                    wsnew.get_Range("F48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F49", m).Value2 = "15";
                    wsnew.get_Range("F49", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F49", m).Borders.Value = true;
                    wsnew.get_Range("F49", m).ColumnWidth = 9;
                    wsnew.get_Range("F49", m).Font.Name = "Arial";
                    wsnew.get_Range("F49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F50", m).Value2 = "15";
                    wsnew.get_Range("F50", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F50", m).Borders.Value = true;
                    wsnew.get_Range("F50", m).ColumnWidth = 9;
                    wsnew.get_Range("F50", m).Font.Name = "Arial";
                    wsnew.get_Range("F50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F51", m).Value2 = "15";
                    wsnew.get_Range("F51", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F51", m).Borders.Value = true;
                    wsnew.get_Range("F51", m).ColumnWidth = 9;
                    wsnew.get_Range("F51", m).Font.Name = "Arial";
                    wsnew.get_Range("F51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F52", m).Value2 = "15";
                    wsnew.get_Range("F52", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F52", m).Borders.Value = true;
                    wsnew.get_Range("F52", m).ColumnWidth = 9;
                    wsnew.get_Range("F52", m).Font.Name = "Arial";
                    wsnew.get_Range("F52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F53", m).Value2 = "15";
                    wsnew.get_Range("F53", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F53", m).Borders.Value = true;
                    wsnew.get_Range("F53", m).ColumnWidth = 9;
                    wsnew.get_Range("F53", m).Font.Name = "Arial";
                    wsnew.get_Range("F53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F54", m).Value2 = "15";
                    wsnew.get_Range("F54", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F54", m).Borders.Value = true;
                    wsnew.get_Range("F54", m).ColumnWidth = 9;
                    wsnew.get_Range("F54", m).Font.Name = "Arial";
                    wsnew.get_Range("F54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F55", m).Value2 = "15";
                    wsnew.get_Range("F55", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F55", m).Borders.Value = true;
                    wsnew.get_Range("F55", m).ColumnWidth = 9;
                    wsnew.get_Range("F55", m).Font.Name = "Arial";
                    wsnew.get_Range("F55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F56", m).Value2 = "15";
                    wsnew.get_Range("F56", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F56", m).Borders.Value = true;
                    wsnew.get_Range("F56", m).ColumnWidth = 9;
                    wsnew.get_Range("F56", m).Font.Name = "Arial";
                    wsnew.get_Range("F56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F57", m).Value2 = "15";
                    wsnew.get_Range("F57", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F57", m).Borders.Value = true;
                    wsnew.get_Range("F57", m).ColumnWidth = 9;
                    wsnew.get_Range("F57", m).Font.Name = "Arial";
                    wsnew.get_Range("F57", m).Font.Size = 10;
                    //
                    #endregion
                }
                #endregion
                //
                //wsnew.get_Range("A1", "F61").Locked = false;
                //wsnew.get_Range("A1", "F61").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
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

        #endregion

        #region PROTECT WORKSHEET
        private bool PROTECT_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
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

                //wsnew.get_Range("A1", "M100").Locked = false;
                //wsnew.get_Range("A1", "M1").Locked = false;
                //wsnew.get_Range("A1", "M1").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
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

        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (rbnImportEmployee.Checked == true) //-- EMPLOYEE
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
                    if (txtExcelPathEmployee.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("File not selected");
                        btnSelectExcelPathEmployee.Select();
                        return false;
                    }
                    //
                    if (blnExcel == false)
                    {
                        // FILE SHOULD BE CSV
                        if (Path.GetExtension(txtExcelPathEmployee.Text).ToUpper() != ".CSV")
                        {
                            cmnService.J_UserMessage("Selected file should be a CSV file");
                            btnSelectExcelPathEmployee.Select();
                            return false;
                        }
                    }
                    else if(blnExcel == true)
                    {
                        // FILE SHOULD BE EXCEL
                        if (Path.GetExtension(txtExcelPathEmployee.Text).ToUpper() != ".XLS" && Path.GetExtension(txtExcelPathEmployee.Text).ToUpper() != ".XLSX")
                        {
                            cmnService.J_UserMessage("Selected file should be a Excel file");
                            btnSelectExcelPathEmployee.Select();
                            return false;
                        }
                    }
                    // FILE EXIST
                    if (cmnService.J_IsFileExist(txtExcelPathEmployee.Text) == false)
                    {
                        cmnService.J_UserMessage("Selected file not found");
                        btnSelectExcelPathEmployee.Select();
                        return false;
                    }
                    // FILE OPEN
                    //-- ANIK 2011-09-09
                    string strPath = txtExcelPathEmployee.Text.ToString();
                    //if (cmnService.J_IsProcessOpen(txtExcelPath.Text) == true)
                    if (TdsMan.T_isFileOpenOrReadOnly(ref strPath) == true)
                    {
                        cmnService.J_UserMessage("Selected file is open");
                        btnSelectExcelPathEmployee.Select();
                        return false;
                    }
                    return true;
                }
                else if (rbnImportDeductee.Checked == true) //-- DEDUCTEE
                {
                    //-----------------------------------------------------------------------
                    //-- EXCEL FILE SELECTED
                    //-----------------------------------------------------------------------
                    if (txtExcelPathDeductee.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("File not selected");
                        btnSelectExcelPathDeductee.Select();
                        return false;
                    }
                    if (blnExcel == false)
                    {
                        // FILE SHOULD BE CSV
                        if (Path.GetExtension(txtExcelPathDeductee.Text).ToUpper() != ".CSV")
                        {
                            cmnService.J_UserMessage("Selected file should be a CSV file");
                            btnSelectExcelPathDeductee.Select();
                            return false;
                        }
                    }
                    else if (blnExcel == true)
                    {
                        // FILE SHOULD BE EXCEL
                        if (Path.GetExtension(txtExcelPathDeductee.Text).ToUpper() != ".XLS" && Path.GetExtension(txtExcelPathDeductee.Text).ToUpper() != ".XLSX")
                        {
                            cmnService.J_UserMessage("Selected file should be a Excel file");
                            btnSelectExcelPathDeductee.Select();
                            return false;
                        }
                    }
                    // FILE EXIST
                    if (cmnService.J_IsFileExist(txtExcelPathDeductee.Text) == false)
                    {
                        cmnService.J_UserMessage("Selected file not found");
                        btnSelectExcelPathDeductee.Select();
                        return false;
                    }
                    // FILE OPEN
                    //-- ANIK 2011-09-09
                    string strPath = txtExcelPathDeductee.Text.ToString();
                    //if (cmnService.J_IsProcessOpen(txtExcelPath.Text) == true)
                    if (TdsMan.T_isFileOpenOrReadOnly(ref strPath) == true)
                    {
                        cmnService.J_UserMessage("Selected file is open");
                        btnSelectExcelPathDeductee.Select();
                        return false;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
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
                //
                if (Path.GetExtension(ExcelFilePath.Trim()) == ".xls")
                    strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                else if (Path.GetExtension(ExcelFilePath.Trim()) == ".xlsx")
                    strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\"";
                //
                OleDbConnection con = new OleDbConnection(strConnectionString);
                //  
                con.Open();
                //--------------------
                if (TdsMan.T_IsExcelDatabaseObjectExist(strEmployeeMaster, "PARTY PAN", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strEmployeeMaster, "PARTY NAME", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strEmployeeMaster, "PARTY EMAIL ID", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
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

        #region CREATE_TEMP_TABLES
        private bool CREATE_TEMP_TABLES()
        {
            try
            {
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "";
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //Blocked by INDRAJIT on 16-03-2012
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "") == false)
                {
                    if (blnExcel == false)
                    {
                        //MessageBox.Show("3.1");
                        strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + @" (
                                            " + cmnService.J_GetDataType("SERIAL_NO", J_ColumnType.String, 10) + @",
                                            " + cmnService.J_GetDataType("PARTY_PAN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("PARTY_NAME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("PARTY_EMAIL", J_ColumnType.String) + @")";
                    }
                    else if (blnExcel == true)
                    {
                        strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " (" +
                         "                  PARTY_ID         COUNTER," +
                         "                  PARTY_NAME       TEXT(255) DEFAULT \"\"," +
                         "                  PARTY_NAME_CELL  TEXT(10)  DEFAULT \"\"," +
                         "                  PARTY_PAN        TEXT(255) DEFAULT \"\"," +
                         "                  PARTY_PAN_CELL   TEXT(10)  DEFAULT \"\"," +
                         "                  PARTY_EMAIL      TEXT(255) DEFAULT \"\"," +
                         "                  PARTY_EMAIL_CELL TEXT(10)  DEFAULT \"\")";
                    }
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

        #region CREATE_TEMP_TABLES_SQL
        private bool CREATE_TEMP_TABLES_SQL()
        {
            try
            {
                #region T_tblTEMP_MST_PARTY
                dmlService.J_BeginTransaction();
                //MessageBox.Show("2");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "") == true)
                {
                    //MessageBox.Show("2.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //MessageBox.Show("3");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "") == false)
                {
                    if (blnExcel == false)
                    {
                        //MessageBox.Show("3.1");
                        strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + @" (
                                            " + cmnService.J_GetDataType("SERIAL_NO", J_ColumnType.String, 10) + @",
                                            " + cmnService.J_GetDataType("PARTY_PAN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("PARTY_NAME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("PARTY_EMAIL", J_ColumnType.String) + @")";
                    }
                    else if (blnExcel == true)
                    {
                        strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + @" (
                                            " + cmnService.J_GetDataType("PARTY_ID", J_Identity.YES) + @",
                                            " + cmnService.J_GetDataType("PARTY_PAN", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("PARTY_PAN_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("PARTY_NAME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("PARTY_NAME_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("PARTY_EMAIL", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("PARTY_EMAIL_CELL", J_ColumnType.Char) + @")";
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
                    if (blnExcel == false)
                        strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                             "                  " + cmnService.J_GetDataType("ERR_VALIDATION_ID", J_Identity.YES) + "," +
                             "                  " + cmnService.J_GetDataType("ERR_TYPE", J_ColumnType.String, 255) + "," +
                             "                  " + cmnService.J_GetDataType("ERR_ROW", J_ColumnType.String, 255) + "," +
                             "                  " + cmnService.J_GetDataType("ERR_COLUMN", J_ColumnType.String, 255) + "," +
                             "                  " + cmnService.J_GetDataType("ERR_SHEET", J_ColumnType.String, 255) + "," +
                             "                  " + cmnService.J_GetDataType("ERR_DESC", J_ColumnType.String, 255) + ")";
                    else if (blnExcel == true)
                        strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                         "                  " + cmnService.J_GetDataType("ERR_VALIDATION_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_TYPE", J_ColumnType.String, 25) + "," +
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

        #region CREATE_TEMP_ERR_TABLES
        private bool CREATE_TEMP_ERR_TABLES()
        {
            DMLService dmlService1 = new DMLService();
            try
            {
                //
                dmlService1.J_BeginTransaction();
                if (dmlService1.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION) == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                    dmlService1.J_ExecSql(strSQL);                        
                }
                dmlService1.J_Commit();
                //
                System.Threading.Thread.Sleep(3000);
                //
                dmlService1.J_BeginTransaction();
                if (dmlService1.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION) == false)
                {
                    if (blnExcel == false)
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
                        "                  " + cmnService.J_GetDataType("ERR_TYPE", J_ColumnType.String, 255) + "," +
                        "                  " + cmnService.J_GetDataType("ERR_CELL", J_ColumnType.String, 255) + "," +
                        "                  " + cmnService.J_GetDataType("ERR_COLUMN", J_ColumnType.String, 255) + "," +
                        "                  " + cmnService.J_GetDataType("ERR_SHEET", J_ColumnType.String, 255) + "," +
                        "                  " + cmnService.J_GetDataType("ERR_COLOR", J_ColumnType.String, 255) + "," +
                        "                  " + cmnService.J_GetDataType("ERR_DESC", J_ColumnType.String, 255) + "," +
                        "                  " + cmnService.J_GetDataType("ERR_FORM_NO", J_ColumnType.String, 255) + ")";
                    dmlService1.J_ExecSql(strSQL);
                }
                dmlService1.J_Commit();
                //
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                if (dmlService1.J_ExecSql(strSQL) == false)
                    return false;
                //
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
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "";
                    dmlService.J_ExecSql(strSQL);
                        //return false;
                }
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                    dmlService.J_ExecSql(strSQL);
                        //return false;
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
        private bool GET_DATA_FROM_EXCEL_ACCESS(string ExcelPath)
        {
            try
            {
                #region VARIABLE_DECLARATION
                // DEDUCTEE MASTER
                string strPartyName = "";
                string strPartyPAN = "";
                string strPartyEmail = "";

                ////strTempPartyMasterPath = Path.Combine(Application.StartupPath, "PartyMaster.txt");
                string strStartupPath = "";
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                    strStartupPath = Path.Combine(J_Var.J_pEnterpriseServerPath, "TMP FOLDER");
                else
                    strStartupPath = Path.Combine(Application.StartupPath, "TMP FOLDER");
                //-- DELETE IF THE FILE EXISTS.
                if (Directory.Exists(strStartupPath) == false)
                    Directory.CreateDirectory(strStartupPath);
                //--
                strTempPartyMasterPath = Path.Combine(Application.StartupPath, "PartyMaster.txt");
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                {
                    strTempPartyMasterPath = Path.Combine(strStartupPath, TDSMAN.Classes.TDSMAN.T_pProductSerial + "_PartyMaster.txt");
                    if (File.Exists(strTempPartyMasterPath) == true)
                        File.Delete(strTempPartyMasterPath);
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
                if (Path.GetExtension(ExcelPath.Trim()) == ".xls")
                    strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                else if (Path.GetExtension(ExcelPath.Trim()) == ".xlsx")
                    strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelPath + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\"";
                //
                OleDbConnection con = new OleDbConnection(strConnectionString);
                //                  
                con.Open();
                //
                // INSERT DEDUCTEE MASTER
                //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                myDataSet = new DataSet();
                myCommand = new OleDbDataAdapter("SELECT * FROM [" + T_Party_Sheet_Name.PARTY_MASTER + "$]", con);
                myCommand.Fill(myDataSet);
                //con.Close();

                long lngRow = 2;
                long lngColumn = 0;

                int intLineNumber = 0;

                StreamWriter StreamWriterDeductee = cmnService.J_ReturnStreamWriter(strTempPartyMasterPath);

                //Travers through each row in the dataset
                foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
                {
                    lblProgressDisplayMessageEmployee.Visible = true;
                    //lblProgressDisplayMessageEmployee.Text = "Transferring Data [" + T_Employee_Master_Sheet_Name.EMPLOYEE_MASTER + " : " + lngRow + " ]";
                    //this.Refresh();
                    //////
                    //lngColumn =65;
                    ////Stores info in Datarow into an array
                    Object[] cells = myDataRow.ItemArray;

                    intLineNumber = intLineNumber + 1;
                    int intColumnValue = 64;
                    //
                    strPartyPAN = Convert.ToString(cells[(int)T_GET_PARTY_MASTER_DATA_FROM_EXCEL.PARTY_PAN ]);
                    strPartyName = Convert.ToString(cells[(int)T_GET_PARTY_MASTER_DATA_FROM_EXCEL.PARTY_NAME]);
                    strPartyEmail = Convert.ToString(cells[(int)T_GET_PARTY_MASTER_DATA_FROM_EXCEL.PARTY_EMAIL]);
                    // CHECK BLANK ROW TO EXIT
                    if (strPartyPAN == "" &&
                            strPartyName == "" &&
                                strPartyEmail == "" )
                        break;

                    cmnService.J_WriteLine(ref StreamWriterDeductee, TdsMan.T_WriteField(intLineNumber.ToString()) + 
                                                                     TdsMan.T_WriteField(strPartyPAN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 1)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strPartyName) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 2)) + (Convert.ToString(intLineNumber + 1)))) +
                                                                     TdsMan.T_WriteField(strPartyEmail) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 3)) + (Convert.ToString(intLineNumber + 1)))) ); 
                }

                myDataSet.Dispose();
                myCommand.Dispose();

                StreamWriterDeductee.Flush();
                StreamWriterDeductee.Close();

                #endregion

                #region TRANSFERRING DATA FROM TEXT FILE TO ACCESS
                //Array to hold data segment name
                tableName = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "";
                textfileName = "PartyMaster";

                TdsMan.T_ReplaceDoubleQuotesinFile(strTempPartyMasterPath, true);

                ImportTextToTables(tableName, textfileName, strTempPartyMasterPath, false);

                // DELETE THE TXT FILE
                if (File.Exists(strTempPartyMasterPath) == true)
                    File.Delete(strTempPartyMasterPath);

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



        #region GET_DATA_FROM_CSV_ACCESS
        private bool GET_DATA_FROM_CSV_ACCESS(string ExcelPath)
        {
            try
            {
                #region VARIABLE_DECLARATION
                // DEDUCTEE MASTER
                string strPartyName = "";
                string strPartyPAN = "";
                string strPartyEmail = "";

                strTempPartyMasterPath = Path.Combine(Application.StartupPath, "PartyMaster.txt");

                string tableName = "";
                string textfileName = "";

                string strStartupPath = "";
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                    strStartupPath = Path.Combine(J_Var.J_pEnterpriseServerPath, "TMP FOLDER");
                else
                    strStartupPath = Path.Combine(Application.StartupPath, "TMP FOLDER");
                //-- DELETE IF THE FILE EXISTS.
                if (Directory.Exists(strStartupPath) == false)
                    Directory.CreateDirectory(strStartupPath);
                //--

                #endregion

                tableName = TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY;

                #region CSV 
                if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    //COPY .TDS FILE AS .TXT FILE TO IMPORT DATA TO ACCESS
                    if (File.Exists(Application.StartupPath + "\\" + tableName + ".txt") == true)
                        File.Delete(Application.StartupPath + "\\" + tableName + ".txt");
                    //
                    File.Copy(strCSVFilePath, Application.StartupPath + "\\" + tableName + ".txt", true);
                    //
                    //-- Creation of dump table from .CSV file
                    if (T_BulkImportFromCSVFile(tableName, false) == false) return false;

                    //-- DELETE 1ST ROW-COLUMN NAME
                    strSQL = @"SELECT COUNT(*) FROM " + tableName + @"
                                WHERE  SERIAL_NO = 'SERIAL NO'
                                AND    PARTY_NAME = 'PARTY NAME'
                                AND    PARTY_PAN  = 'PARTY PAN'
                                AND    PARTY_EMAIL = 'PARTY EMAIL ID'";
                    if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) != 1)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Selected file is invalid.\n" +
                                                    "Please get the latest Csv file.", MessageBoxIcon.Exclamation);
                        //--
                        prgBarEmployee.Value = 0;
                        btnSelectExcelPathEmployee.Select();
                        return false;
                    }
                    strSQL = @"DELETE FROM " + tableName + " WHERE PARTY_NAME = 'PARTY NAME'";
                    dmlService.J_ExecSql(strSQL);
                    //-- DELETE BLANK ROW
                    strSQL = @"DELETE FROM " + tableName + " WHERE PARTY_NAME IS NULL";
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
                    File.Copy(strCSVFilePath, strStartupPath + "\\" + tableName + ".txt", true);
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
                                WHERE  SERIAL_NO = 'SERIAL NO'
                                AND    PARTY_NAME = 'PARTY NAME'
                                AND    PARTY_PAN  = 'PARTY PAN'
                                AND    PARTY_EMAIL = 'PARTY EMAIL ID'";
                        if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) != 1)
                        {
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage("Selected Csv file is invalid.\n" +
                                                     "Please get the latest Csv file.", MessageBoxIcon.Exclamation);
                            //--
                            prgBarEmployee.Value = 0;
                            btnSelectExcelPathEmployee.Select();
                            return false;
                        }
                        //
                        strSQL = @"DELETE FROM " + tableName + " WHERE PARTY_NAME = 'PARTY NAME'";
                        dmlService.J_ExecSql(strSQL);
                        //-- DELETE BLANK ROW
                        strSQL = @"DELETE FROM " + tableName + " WHERE PARTY_NAME IS NULL";
                        dmlService.J_ExecSql(strSQL);
                        //--
                    }
                    //
                }
                return true;
                #endregion
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
                                    Col2 = PARTY_PAN Char
                                    Col3 = PARTY_NAME Char
                                    Col4 = PARTY_EMAIL Char");

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
        //
        private void ImportTextToTables(string tbl, string txtfile, string FilePath, bool hdr)
        {
            //Check 'n Create SCHEMA file for Temp Tables
            string strFolderPath = cmnService.J_GetDirectoryName(strTempPartyMasterPath);

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
            if (txtfile == "PartyMaster")
            {
                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "
                StreamWriter.WriteLine(@"Col1=PARTY_ID Integer
                                         Col2=PARTY_PAN Char
                                         Col3=PARTY_PAN_CELL Char
                                         Col4=PARTY_NAME Char
                                         Col5=PARTY_NAME_CELL Char
                                         Col6=PARTY_EMAIL Char
                                         Col7=PARTY_EMAIL_CELL Char");
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
            int lngRowCount;
            try
            {
                if (CREATE_TEMP_ERR_TABLES() == false)
                    return false;

                strSheetName = T_Party_Sheet_Name.PARTY_MASTER;

                System.Threading.Thread.Sleep(5000);
                //
                if (blnExcel == true)
                {

                    #region PARTY PAN

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " SET PARTY_PAN = '' WHERE PARTY_PAN IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //BLANK OR 0 CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  PARTY_PAN = ''" +
                        "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                            "             PARTY_PAN_CELL AS ERROR_CELL," +
                            "             'PARTY_PAN_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_PAN = ''" +
                            "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  LEN(PARTY_PAN) <> 10" +
                        "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "             PARTY_PAN_CELL AS ERROR_CELL," +
                            "             'PARTY_PAN_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  LEN(PARTY_PAN) <> 10" +
                            "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //PANNOTAVBL NOT ALLOWED
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  PARTY_PAN = 'PANNOTAVBL'" +
                        "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                            "             PARTY_PAN_CELL AS ERROR_CELL," +
                            "             'PARTY_PAN_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.VALUE_NOT_REQD + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_PAN = 'PANNOTAVBL'" +
                            "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //PAN STRUCTURE CHECK
                    strSQL = "SELECT COUNT(*)" +
                     "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                     "     WHERE  (ISNUMERIC(LEFT(PARTY_PAN,1)) <> 0" +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,2,1)) <> 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,3,1)) <> 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,4,1)) <> 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,5,1)) <> 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,6,1)) = 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,7,1)) = 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,8,1)) = 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,9,1)) = 0 " +
                     "     OR     ISNUMERIC(RIGHT(PARTY_PAN,1)) = -1)" +
                     "     AND    PARTY_PAN <> 'PANNOTAVBL'" +
                     "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "             PARTY_PAN_CELL AS ERROR_CELL," +
                            "             'PARTY_PAN_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  (ISNUMERIC(LEFT(PARTY_PAN,1)) <> 0" +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,2,1)) <> 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,3,1)) <> 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,4,1)) <> 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,5,1)) <> 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,6,1)) = 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,7,1)) = 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,8,1)) = 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,9,1)) = 0 " +
                            "     OR     ISNUMERIC(RIGHT(PARTY_PAN,1)) = -1)" +
                            "     AND    PARTY_PAN <> 'PANNOTAVBL'" +
                            "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    // DUPLICATE CHECK
                    // WHEN PARTY_PAN <> 'PANNOTAVBL'
                    strSQL = "SELECT SUM(PARTY.NO_OF_ROWS) AS NO_OF_ROWS " +
                             "FROM  (SELECT PARTY_PAN," +
                             "              COUNT(PARTY_ID) AS NO_OF_ROWS " +
                             "       FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " " +
                             "       WHERE  PARTY_PAN     <> 'PANNOTAVBL' " +
                             "       AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                             "       GROUP BY PARTY_PAN " +
                             "       HAVING COUNT(PARTY_PAN) > 1) AS PARTY";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                    //
                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                            "             PARTY_PAN_CELL AS ERROR_CELL," +
                            "             'PARTY_PAN_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.DUPLICATE_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_PAN IN (SELECT PARTY_PAN " +
                            "                             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " " +
                            "                             WHERE  PARTY_PAN     <> 'PANNOTAVBL' " +
                            "                             AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                            "                             GROUP BY PARTY_PAN " +
                            "                             HAVING COUNT(PARTY_PAN) > 1)";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    if (rbnImportEmployee.Checked == true)
                    {
                        #region PAN NOT FOUND UNDER THE COMPANY
                        strSQL = "SELECT COUNT(*)" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_PAN NOT IN (SELECT EMPLOYEE_PAN FROM MST_EMPLOYEE WHERE COMPANY_ID = " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex) + ") " +
                            "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                        if (lngRowCount > 0)
                        {
                            strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                                "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                                "             PARTY_PAN_CELL AS ERROR_CELL," +
                                "             'PARTY_PAN_CELL'," +
                                "             '" + strSheetName + "'," +
                                "             '" + T_Error_Type_Color.VALUE_NOT_REQD + "'" +
                                "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                                "     WHERE  PARTY_PAN NOT IN (SELECT EMPLOYEE_PAN FROM MST_EMPLOYEE WHERE COMPANY_ID = " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex) + ") " +
                                "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                            //
                            if (dmlService.J_ExecSql(strSQL) == false)
                                return false;
                        }
                        #endregion
                    }
                    else if (rbnImportDeductee.Checked == true)
                    {
                        #region PAN NOT FOUND 
                        strSQL = "SELECT COUNT(*)" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_PAN NOT IN (SELECT DEDUCTEE_PAN FROM MST_DEDUCTEE) " +
                            "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                        if (lngRowCount > 0)
                        {
                            strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                                "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                                "             PARTY_PAN_CELL AS ERROR_CELL," +
                                "             'PARTY_PAN_CELL'," +
                                "             '" + strSheetName + "'," +
                                "             '" + T_Error_Type_Color.VALUE_NOT_REQD + "'" +
                                "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                                "     WHERE  PARTY_PAN NOT IN (SELECT DEDUCTEE_PAN FROM MST_DEDUCTEE) " +
                                "     AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                            //
                            if (dmlService.J_ExecSql(strSQL) == false)
                                return false;
                        }
                        #endregion
                    }

                    #region PARTY NAME

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " SET PARTY_NAME = '' WHERE PARTY_NAME IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    ////BLANK OR 0 CHECK
                    //strSQL = "SELECT COUNT(*)" +
                    //    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                    //    "     WHERE  PARTY_NAME = ''" +
                    //    "     AND    PARTY_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    ////
                    //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    //if (lngRowCount > 0)
                    //{
                    //    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                    //        "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                    //        "            PARTY_NAME_CELL AS ERROR_CELL," +
                    //        "            'PARTY_NAME_CELL'," +
                    //        "            '" + strSheetName + "'," +
                    //        "            '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                    //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                    //        "     WHERE  PARTY_NAME = ''" +
                    //        "     AND    PARTY_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";

                    //    if (dmlService.J_ExecSql(strSQL) == false)
                    //        return false;
                    //}

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  LEN(PARTY_NAME) > 75" +
                        "     AND    PARTY_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            PARTY_NAME_CELL AS ERROR_CELL," +
                            "            'PARTY_NAME_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  LEN(PARTY_NAME) > 75" +
                            "     AND    PARTY_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //DUPLICATE CHECK
                    //WHEN PARTY_PAN = 'PANNOTAVBL'
                    strSQL = "SELECT COUNT(PARTY_PAN) " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " " +
                             "WHERE  PARTY_PAN     = 'PANNOTAVBL' " +
                             "AND    PARTY_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                             "GROUP BY PARTY_NAME " +
                             "HAVING COUNT(PARTY_NAME) > 1";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                            "             PARTY_NAME_CELL AS ERROR_CELL," +
                            "             'PARTY_NAME_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.DUPLICATE_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_NAME IN (SELECT PARTY_NAME " +
                            "                             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " " +
                            "                             WHERE  PARTY_PAN     = 'PANNOTAVBL' " +
                            "                             GROUP BY PARTY_NAME,PARTY_PAN  " +
                            "                             HAVING COUNT(PARTY_NAME) > 1) " +
                            "     AND    PARTY_PAN     = 'PANNOTAVBL' " +
                            "     AND    PARTY_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    #region PARTY EMAIL

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " SET PARTY_EMAIL = '' WHERE PARTY_EMAIL IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //BLANK OR 0 CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  PARTY_EMAIL= ''" +
                        "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                            "            PARTY_EMAIL_CELL AS ERROR_CELL," +
                            "            'PARTY_EMAIL_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_EMAIL = ''" +
                            "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";

                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  LEN(PARTY_EMAIL) > 75" +
                        "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            PARTY_EMAIL_CELL AS ERROR_CELL," +
                            "            'PARTY_EMAIL_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  LEN(PARTY_EMAIL) > 75" +
                            "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- EMAIL FORMAT CHECKING
                    //-- Email should have atleast one '@'
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE   PARTY_EMAIL LIKE '%@%' " +
                        "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount == 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            PARTY_EMAIL_CELL AS ERROR_CELL," +
                            "            'PARTY_EMAIL_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_EMAIL LIKE '%@%' " +
                            "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- Email should have atleast one '.'
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  PARTY_EMAIL LIKE '%.%' " +
                        "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount == 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            PARTY_EMAIL_CELL AS ERROR_CELL," +
                            "            'PARTY_EMAIL_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_EMAIL LIKE '%.%' " +
                            "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- '@' should be preceded by atleast one character.
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  RIGHT(PARTY_EMAIL, 1) = '@' " +
                        "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            PARTY_EMAIL_CELL AS ERROR_CELL," +
                            "            'PARTY_EMAIL_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  RIGHT(PARTY_EMAIL, 1) = '@' " +
                            "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- '@' should be succeeded by atleast one character.
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  LEFT(PARTY_EMAIL, 1) = '@' " +
                        "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            PARTY_EMAIL_CELL AS ERROR_CELL," +
                            "            'PARTY_EMAIL_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  LEFT(PARTY_EMAIL, 1) = '@' " +
                            "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- '.' should be preceded by atleast one character.
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  RIGHT(PARTY_EMAIL, 1) = '.' " +
                        "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            PARTY_EMAIL_CELL AS ERROR_CELL," +
                            "            'PARTY_EMAIL_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  RIGHT(PARTY_EMAIL, 1) = '.' " +
                            "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- '.' should be succeeded by atleast one character.
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  LEFT(PARTY_EMAIL, 1) = '.' " +
                        "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "            PARTY_EMAIL_CELL AS ERROR_CELL," +
                            "            'PARTY_EMAIL_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  LEFT(PARTY_EMAIL, 1) = '.' " +
                            "     AND    PARTY_EMAIL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- At least one '.' should come after '@'
                    //-- '@' can't be twice in a email.
                    //-- Invalid Email

                    #endregion
                }
                else if(blnExcel == false)
                {
                    #region PARTY PAN
                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " SET PARTY_PAN = '' WHERE PARTY_PAN IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //BLANK OR 0 CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  PARTY_PAN = ''" +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'PARTY PAN'," +
                            "             '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_PAN = ''" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  LEN(PARTY_PAN) <> 10" +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'PARTY PAN'," +
                            "             '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  LEN(PARTY_PAN) <> 10" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //PANNOTAVBL NOT ALLOWED
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  PARTY_PAN = 'PANNOTAVBL'" +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.VALUE_NOT_REQD + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'PARTY PAN'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.VALUE_NOT_REQD + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_PAN = 'PANNOTAVBL'" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //PAN STRUCTURE CHECK
                    strSQL = "SELECT COUNT(*)" +
                     "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                     "     WHERE  (ISNUMERIC(LEFT(PARTY_PAN,1)) <> 0" +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,2,1)) <> 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,3,1)) <> 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,4,1)) <> 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,5,1)) <> 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,6,1)) = 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,7,1)) = 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,8,1)) = 0 " +
                     "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,9,1)) = 0 " +
                     "     OR     ISNUMERIC(RIGHT(PARTY_PAN,1)) = -1)" +
                     "     AND    PARTY_PAN <> 'PANNOTAVBL'" +
                     "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'PARTY PAN'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  (ISNUMERIC(LEFT(PARTY_PAN,1)) <> 0" +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,2,1)) <> 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,3,1)) <> 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,4,1)) <> 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,5,1)) <> 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,6,1)) = 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,7,1)) = 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,8,1)) = 0 " +
                            "     OR     ISNUMERIC(" + strMidSubString + "(PARTY_PAN,9,1)) = 0 " +
                            "     OR     ISNUMERIC(RIGHT(PARTY_PAN,1)) = -1)" +
                            "     AND    PARTY_PAN <> 'PANNOTAVBL'" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    // DUPLICATE CHECK
                    // WHEN PARTY_PAN <> 'PANNOTAVBL'
                    strSQL = "SELECT SUM(PARTY.NO_OF_ROWS) AS NO_OF_ROWS " +
                             "FROM  (SELECT PARTY_PAN," +
                             "              COUNT(SERIAL_NO) AS NO_OF_ROWS " +
                             "       FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " " +
                             "       WHERE  PARTY_PAN     <> 'PANNOTAVBL' " +
                             "       AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                             "       GROUP BY PARTY_PAN " +
                             "       HAVING COUNT(PARTY_PAN) > 1) AS PARTY";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                    //
                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'PARTY PAN'," +
                            "             '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_PAN IN (SELECT PARTY_PAN " +
                            "                             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " " +
                            "                             WHERE  PARTY_PAN     <> 'PANNOTAVBL' " +
                            "                             AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                            "                             GROUP BY PARTY_PAN " +
                            "                             HAVING COUNT(PARTY_PAN) > 1)";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    if (rbnImportEmployee.Checked == true)
                    {
                        #region PAN NOT FOUND UNDER THE COMPANY
                        strSQL = "SELECT COUNT(*)" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_PAN NOT IN (SELECT EMPLOYEE_PAN FROM MST_EMPLOYEE WHERE COMPANY_ID = " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex) + ") " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                        if (lngRowCount > 0)
                        {
                            strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                                "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                                "             'PARTY PAN'," +
                                "             '" + strSheetName + "'" +
                                "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                                "     WHERE  PARTY_PAN NOT IN (SELECT EMPLOYEE_PAN FROM MST_EMPLOYEE WHERE COMPANY_ID = " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex) + ") " +
                                "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                            //
                            if (dmlService.J_ExecSql(strSQL) == false)
                                return false;
                        }
                        #endregion
                    }
                    else if (rbnImportDeductee.Checked == true)
                    {
                        #region PAN NOT FOUND 
                        strSQL = "SELECT COUNT(*)" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_PAN NOT IN (SELECT DEDUCTEE_PAN FROM MST_DEDUCTEE) " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                        if (lngRowCount > 0)
                        {
                            strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                                "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                                "             SERIAL_NO AS ERR_ROW," +
                                "             'PARTY PAN'," +
                                "             '" + strSheetName + "'" +
                                "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                                "     WHERE  PARTY_PAN NOT IN (SELECT DEDUCTEE_PAN FROM MST_DEDUCTEE) " +
                                "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                            //
                            if (dmlService.J_ExecSql(strSQL) == false)
                                return false;
                        }
                        #endregion
                    }

                    #region PARTY NAME

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " SET PARTY_NAME = '' WHERE PARTY_NAME IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    ////BLANK OR 0 CHECK
                    //strSQL = "SELECT COUNT(*)" +
                    //    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                    //    "     WHERE  PARTY_NAME = ''" +
                    //    "     AND    PARTY_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    ////
                    //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    //if (lngRowCount > 0)
                    //{
                    //    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                    //        "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                    //        "            PARTY_NAME_CELL AS ERROR_CELL," +
                    //        "            'PARTY_NAME_CELL'," +
                    //        "            '" + strSheetName + "'," +
                    //        "            '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                    //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                    //        "     WHERE  PARTY_NAME = ''" +
                    //        "     AND    PARTY_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";

                    //    if (dmlService.J_ExecSql(strSQL) == false)
                    //        return false;
                    //}

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  LEN(PARTY_NAME) > 75" +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "            'PARTY NAME'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  LEN(PARTY_NAME) > 75" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //DUPLICATE CHECK
                    //WHEN PARTY_PAN = 'PANNOTAVBL'
                    strSQL = "SELECT COUNT(PARTY_PAN) " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " " +
                             "WHERE  PARTY_PAN     = 'PANNOTAVBL' " +
                             "AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ") " +
                             "GROUP BY PARTY_NAME " +
                             "HAVING COUNT(PARTY_NAME) > 1";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "             'PARTY NAME'," +
                            "             '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_NAME IN (SELECT PARTY_NAME " +
                            "                             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " " +
                            "                             WHERE  PARTY_PAN     = 'PANNOTAVBL' " +
                            "                             GROUP BY PARTY_NAME,PARTY_PAN  " +
                            "                             HAVING COUNT(PARTY_NAME) > 1) " +
                            "     AND    PARTY_PAN     = 'PANNOTAVBL' " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //              
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    #endregion

                    #region PARTY EMAIL

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + " SET PARTY_EMAIL = '' WHERE PARTY_EMAIL IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //BLANK OR 0 CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  PARTY_EMAIL= ''" +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "            'PARTY EMAIL'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_EMAIL = ''" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";

                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    //LENGTH CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  LEN(PARTY_EMAIL) > 75" +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "            'PARTY EMAIL'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  LEN(PARTY_EMAIL) > 75" +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- EMAIL FORMAT CHECKING
                    //-- Email should have atleast one '@'
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE   PARTY_EMAIL LIKE '%@%' " +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount == 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "            'PARTY EMAIL'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_EMAIL LIKE '%@%' " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- Email should have atleast one '.'
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  PARTY_EMAIL LIKE '%.%' " +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount == 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "            'PARTY EMAIL'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  PARTY_EMAIL LIKE '%.%' " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- '@' should be preceded by atleast one character.
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  RIGHT(PARTY_EMAIL, 1) = '@' " +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "            'PARTY EMAIL'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  RIGHT(PARTY_EMAIL, 1) = '@' " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- '@' should be succeeded by atleast one character.
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  LEFT(PARTY_EMAIL, 1) = '@' " +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "            'PARTY EMAIL'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  LEFT(PARTY_EMAIL, 1) = '@' " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- '.' should be preceded by atleast one character.
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  RIGHT(PARTY_EMAIL, 1) = '.' " +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "            'PARTY EMAIL'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  RIGHT(PARTY_EMAIL, 1) = '.' " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- '.' should be succeeded by atleast one character.
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                        "     WHERE  LEFT(PARTY_EMAIL, 1) = '.' " +
                        "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_ROW, ERR_COLUMN, ERR_SHEET)" +
                            "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                            "             SERIAL_NO AS ERR_ROW," +
                            "            'PARTY EMAIL'," +
                            "            '" + strSheetName + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + "" +
                            "     WHERE  LEFT(PARTY_EMAIL, 1) = '.' " +
                            "     AND    SERIAL_NO NOT IN (SELECT ERR_ROW FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //-- At least one '.' should come after '@'
                    //-- '@' can't be twice in a email.
                    //-- Invalid Email

                    #endregion
                }

                return true;
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

        #region LOAD_IMPORT_INTERFACE_EMPLOYEE
        private bool LOAD_IMPORT_INTERFACE_EMPLOYEE()
        {
            try
            {
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""))) == 0)
                {
                    //--
                    blnOpenTabPage = true;
                    //
                    rbnImportDeductee.Enabled = false;
                    //--
                    tbcEmailImport.SelectTab(tbpImportEmployee);
                    //
                    lblExistingNoEmployee.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(EMPLOYEE_ID) AS UPDATE_EMPLOYEE FROM MST_EMPLOYEE WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))));
                    //-- NEW RECORDS
                    if(rbnEmployeeExcel.Checked==true)
                        lblEmployeeExcel.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(PARTY_ID) AS NEW_EMPLOYEE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY ));
                    else
                        lblEmployeeExcel.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(SERIAL_NO) AS NEW_EMPLOYEE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY));
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

        #region LOAD_IMPORT_INTERFACE_DEDUCTEE
        private bool LOAD_IMPORT_INTERFACE_DEDUCTEE()
        {
            try
            {
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""))) == 0)
                {
                    //--
                    blnOpenTabPage = true;
                    //
                    rbnImportEmployee.Enabled = false;
                    //--
                    tbcEmailImport.SelectTab(tbpImportDeductee);
                    //
                    //lblExistingNoEmployee.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(EMPLOYEE_ID) AS UPDATE_EMPLOYEE FROM MST_EMPLOYEE WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))));
                    //-- NEW RECORDS
                    if (rbnDeducteeExcel.Checked == true)
                        lblDeducteeExcel.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(PARTY_ID) AS NEW_EMPLOYEE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY));
                    else
                        lblDeducteeExcel.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(SERIAL_NO) AS NEW_EMPLOYEE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY));
                    //--
                    //txtCompany.Text = cmbCompany.Text;
                    //lblCompanyID.Text = Convert.ToString(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
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

        #region LoadEmployeeMasterGrid
        private void LoadEmployeeMasterGrid()
        {
            
            //-----------------------------------------------------------
            string[,] strMatrixEmployee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
                                        {"PAN", "100", "", "", "", "", "T"},
                                        {"EXISTING NAME", "100", "", "", "", "", "T"},
                                        {"EXISTING EMAIL ID", "200", "0", "", "", "", "T"},
                                        {"NAME IN EXCEL", "130", "", "", "", "", "T"},
                                        {"EMAIL ID IN EXCEL", "100", "0", "", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            strOrderBy = "EMPLOYEE_NAME, EMPLOYEE_PAN";
            strQuery = "SELECT EMPLOYEE_ID," +
                      "        " + strUpper + "(MST_EMPLOYEE.EMPLOYEE_PAN)," +
                      "        " + strUpper + "(MST_EMPLOYEE.EMPLOYEE_NAME)," +
                      "        " + strLower + "(MST_EMPLOYEE.EMAIL), " +
                      "        " + strUpper + "(MST_PARTY.PARTY_NAME)," +
                      "        " + strLower + "(MST_PARTY.PARTY_EMAIL) " +
                      " FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + @" AS MST_PARTY, 
                               MST_EMPLOYEE 
                      WHERE    MST_PARTY.PARTY_PAN = MST_EMPLOYEE.EMPLOYEE_PAN ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            TdsMan.PopulateGridView(grdvEmployee, dmlService.J_pCommand, strSQL, strMatrixEmployee);
            //if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgvEmployee, strSQL, strMatrixEmployee);       //Show Data into the Grid                

            //if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQLGridViewTabPages, strMatrixChallanDetails);       //Show Data into the Grid                
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQL, strMatrixChallanDetails);       //Show Data into the Grid
            //rptService.J_PopulateGridView(grdvDescription, strSQL, strCorrectionStatement, ref cmbSearch, true);

        }
        #endregion

        #region LoadDeducteeMasterGrid
        private void LoadDeducteeMasterGrid()
        {

            //-----------------------------------------------------------
            string[,] strMatrixDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
                                        {"PAN", "100", "", "", "", "", "T"},
                                        {"EXISTING NAME", "100", "", "", "", "", "T"},
                                        {"EXISTING EMAIL ID", "200", "0", "", "", "", "T"},
                                        {"NAME IN EXCEL", "130", "", "", "", "", "T"},
                                        {"EMAIL ID IN EXCEL", "100", "0", "", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            strOrderBy = "DEDUCTEE_NAME, DEDUCTEE_PAN";
            strQuery = "SELECT DEDUCTEE_ID," +
                      "        " + strUpper + "(MST_DEDUCTEE.DEDUCTEE_PAN)," +
                      "        " + strUpper + "(MST_DEDUCTEE.DEDUCTEE_NAME)," +
                      "        " + strLower + "(MST_DEDUCTEE.EMAIL), " +
                      "        " + strUpper + "(MST_PARTY.PARTY_NAME)," +
                      "        " + strLower + "(MST_PARTY.PARTY_EMAIL) " +
                      " FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY + @" AS MST_PARTY, 
                               MST_DEDUCTEE 
                      WHERE    MST_PARTY.PARTY_PAN = MST_DEDUCTEE.DEDUCTEE_PAN ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            TdsMan.PopulateGridView(grdvDeductee, dmlService.J_pCommand, strSQL, strMatrixDeductee);
            //if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvDeductee, strSQL, strMatrixDeductee);
            //if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQLGridViewTabPages, strMatrixChallanDetails);       //Show Data into the Grid                
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQL, strMatrixChallanDetails);       //Show Data into the Grid
            //rptService.J_PopulateGridView(grdvDescription, strSQL, strCorrectionStatement, ref cmbSearch, true);

        }
        #endregion

        #region UPDATE_EMPLOYEE_EMAIL
        private bool UPDATE_EMPLOYEE_EMAIL()
        {
            //COUNT THE NEW DEDUCTEES
            
            // UPDATE EXISTING
            //if (chkUpdate.Checked == true)
            //{
                int intExistingEmployees = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY ));
            //UPDATE EMPLOYEE RECORDS PRESENT IN MASTER
            if (intExistingEmployees > 0)
            {
                long lngEmployeeID = 0;
                string strPAN = "";
                string strEmail = "";
                foreach (DataGridViewRow row in grdvEmployee.Rows)
                {
                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                    {
                        lngEmployeeID = Convert.ToInt32(row.Cells[1].Value.ToString());
                        strPAN = row.Cells[2].Value.ToString();
                        strEmail = row.Cells[6].Value.ToString();
                        //--
                        strSQL = "UPDATE MST_EMPLOYEE" +
                                "     SET   EMAIL         ='" + strEmail + "' " +
                                "     WHERE EMPLOYEE_PAN  ='" + strPAN + "'" +
                                "     AND   COMPANY_ID    = " + cmnService.J_ReturnInt32Value(lblCompanyID.Text);

                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                        //intEmployeePANCounter = intEmployeePANCounter + 1;
                    }
                }
                //--
                //--
            }
            //}
            return true;
        }
        #endregion

        #region UPDATE_DEDUCTEE_EMAIL
        private bool UPDATE_DEDUCTEE_EMAIL()
        {
            //COUNT THE NEW DEDUCTEES

            // UPDATE EXISTING
            //if (chkUpdate.Checked == true)
            //{
            int intExistingEmployees = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_PARTY));
            //UPDATE EMPLOYEE RECORDS PRESENT IN MASTER
            if (intExistingEmployees > 0)
            {
                long lngDeducteeID = 0;
                string strPAN = "";
                string strEmail = "";
                foreach (DataGridViewRow row in grdvDeductee.Rows)
                {
                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                    {
                        lngDeducteeID = Convert.ToInt32(row.Cells[1].Value.ToString());
                        strPAN = row.Cells[2].Value.ToString();
                        strEmail = row.Cells[6].Value.ToString();
                        //--
                        strSQL = "UPDATE MST_DEDUCTEE " +
                            "     SET   EMAIL         ='" + strEmail + "' " +
                            "     WHERE DEDUCTEE_PAN  ='" + strPAN + "'";
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                        //intEmployeePANCounter = intEmployeePANCounter + 1;
                    }
                }


                //--
            }
            //}
            return true;
        }
        #endregion


        #region CreateEmployeeMaster
        private bool CreateEmployeeMaster(string strPath)
        {
            try
            {
                if (cmnService.J_IsFileExist(strSourceFile) == true)
                {
                    if (cmnService.J_UserMessage("File exists with same name.\nDo you want to replace the old file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return false;
                    else
                        File.Delete(strSourceFile);
                }
                else
                {
                    if (cmnService.J_UserMessage("Blank excel file will be created.\nProceed??", MessageBoxButtons.YesNo) == DialogResult.No)
                        return false;
                }
                //--
                #region Employee Master
                //--------------------------------------------------------------------------------------------------
                this.Cursor = Cursors.WaitCursor;
                //--
                if (CREATE_EXCEL_FILE(strPath) == false) return false;
                //--
                int intColumn = 64;
                int intRow = 1;
                //-----------------------------------------------------------------------------------------------------------
                if (CREATE_NEW_WORKSHEET(strPath, strEmployeeMaster) == false) return false;
                //
                if (WRITE_WORKSHEET(strPath,
                                            strEmployeeMaster,
                                            (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                            "PARTY PAN", false) == false) return false;
                //
                if (WRITE_WORKSHEET(strPath,
                                                                strEmployeeMaster,
                                                                (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                "PARTY NAME", false) == false) return false;
                //
                if (WRITE_WORKSHEET(strPath,
                                                                strEmployeeMaster,
                                                                (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                "PARTY EMAIL ID", true) == false) return false;
                //--
                if (DELETE_WORKSHEET(strPath, "Sheet1") == false) //return;
                    if (DELETE_WORKSHEET(strPath, "Sheet2") == false) //return;
                        if (DELETE_WORKSHEET(strPath, "Sheet3") == false) //return;
                                                                          //------------------------------------
                            if (KILL_EXCEL() == false)
                                return false;
                //--
                this.Cursor = Cursors.Default;
                //--
                //CHECK IF FILE IS CREATED OR NOT
                if (cmnService.J_IsFileExist(strPath) == true)
                {
                    cmnService.J_UserMessage("Blank Excel File Created");
                    //--
                    System.Diagnostics.Process.Start(strPath);
                }
                else
                    cmnService.J_UserMessage("Blank Excel File Creation Failed");
                //-----------------------------------------------------------------------------------------------------------                
                #endregion
                //--

                return true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage("Excel file creation failed");
                return false;
            }
        }
        #endregion

        #region CreateDeducteeMaster
        private bool CreateDeducteeMaster(string strPath)
        {
            try
            {
                //--
                if (cmnService.J_IsFileExist(strSourceFile) == true)
                {
                    if (cmnService.J_UserMessage("File exists with same name.\nDo you want to replace the old file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return false;
                }
                else
                {
                    if (cmnService.J_UserMessage("Blank excel file will be created.\nProceed??", MessageBoxButtons.YesNo) == DialogResult.No)
                        return false;
                }//--
                #region Deductee Master
                //--------------------------------------------------------------------------------------------------
                this.Cursor = Cursors.WaitCursor;
                //--
                if (CREATE_EXCEL_FILE(strPath) == false) return false;
                //--
                int intColumn = 64;
                int intRow = 1;
                //-----------------------------------------------------------------------------------------------------------
                if (CREATE_NEW_WORKSHEET(strPath, strEmployeeMaster) == false) return false;
                //
                if (WRITE_WORKSHEET(strPath,
                                            strEmployeeMaster,
                                            (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                            "PARTY PAN", false) == false) return false;
                //
                if (WRITE_WORKSHEET(strPath,
                                                                strEmployeeMaster,
                                                                (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                "PARTY NAME", false) == false) return false;
                //
                if (WRITE_WORKSHEET(strPath,
                                                                strEmployeeMaster,
                                                                (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                "PARTY EMAIL ID", true) == false) return false;
                //--
                if (DELETE_WORKSHEET(strPath, "Sheet1") == false) //return;
                    if (DELETE_WORKSHEET(strPath, "Sheet2") == false) //return;
                        if (DELETE_WORKSHEET(strPath, "Sheet3") == false) //return;
                                                                                                                //------------------------------------
                            if (KILL_EXCEL() == false)
                                return false;
                //--
                this.Cursor = Cursors.Default;
                //--
                //CHECK IF FILE IS CREATED OR NOT
                if (cmnService.J_IsFileExist(strPath) == true)
                {
                    cmnService.J_UserMessage("Blank Excel File Created");
                    //--
                    System.Diagnostics.Process.Start(strPath);
                }
                else
                    cmnService.J_UserMessage("Blank Excel File Creation Failed");
                //-----------------------------------------------------------------------------------------------------------                
                #endregion
                //--
                return true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage("Excel file creation failed");
                return false;
            }
        }
        #endregion

        #endregion

        #region btnGetExcelFileDeductee_MouseClick
        private void btnGetExcelFileDeductee_MouseClick(object sender, MouseEventArgs e)
        {
            if (J_Var.J_pDatabaseType != J_DatabaseType.SqlServer)
            {
                if (e.Button == MouseButtons.Left)
                    cntxtMnuStrpSelectExcel.Show(btnGetExcelFileEmployee, new Point(e.X, e.Y));
            }
        }
        #endregion

        #region rbnExcelCSV_CheckedChanged
        private void rbnExcelCSV_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnEmployeeExcel.Checked == true)
            {
                grpEmployeeGetFile.Text = "Excel file";
                lblEmployeeFileCaption.Text = "Excel file path";
            }
            else if (rbnEmployeeCSV.Checked == true)
            {
                grpEmployeeGetFile.Text = "CSV file";
                lblEmployeeFileCaption.Text = "CSV file path";
            }
        }


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

        #region rbnDeducteeExcel_CheckedChanged
        private void rbnDeducteeExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnDeducteeExcel.Checked == true)
            {
                grpDeducteeGetFile.Text = "Excel file";
                lblDeducteeFileCaption.Text = "Excel file path";
            }
            else if (rbnDeducteeCSV.Checked == true)
            {
                grpDeducteeGetFile.Text = "CSV file";
                lblDeducteeFileCaption.Text = "CSV file path";
            }
        }
        #endregion


    }
}
