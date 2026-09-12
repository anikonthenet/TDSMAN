
#region Refered Namespaces & Classes

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
//using System.Collections.Generic;
//using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;
using Microsoft.Office.Interop.Excel;
//
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;
//--
//using System.Data;
//using System.Data.SqlClient;
//using System.Text;
//using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
//--

#endregion

namespace TDSMAN.FormTrnCalcMonthlyTDS
{
    public partial class TrnProcessMonthlyDataExport : Form
    {
        #region System Generated Code
        public TrnProcessMonthlyDataExport()
        {
            InitializeComponent();
        }

        public TrnProcessMonthlyDataExport(string strFAYear, string strCompany, string strQuarter, string strForm, string strMonth, long lngHeader)
        {
            strFAYearText = strFAYear;
            strCompanyText = strCompany;
            strQuarterText = strQuarter;
            strFormText = strForm;
            strMonthText = strMonth;
            lngHeaderId = lngHeader;

            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        TracesConnect objTracesConnect = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;        //For Storing the Local SQL Query        					
        string strQueryCD;			        //For Storing the general SQL Query
        string strQueryDD;                   //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strInvalidPAN = "";
        string strUnmatchedPAN = "";
        long lngBasicInfoID = 0;
        //--            
        ToolTip tllTip = new ToolTip();
        string strEmpDed = "";
        string strbtnVerification = "&Start Validation";
        //string strLogOff = "Log off";
        //string strLogOn = "Log on";
        //
        bool blnStatus = false;
        bool blnVerificationComplete = false;
        long lngDeducteeID = 0;
        bool blRegular = true;
        bool blnSelectComboExit = false;
        //
        int intPANId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0;
        int intVerifyId = 0;
        //
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNoBkmark = "";
        string strFAYear = "";
        //
        int j = 0;
        //
        string strTan = "";
        //
        string strQueryForm16 = "";
        //
        string strFAYearText = "";
        string strCompanyText = "";
        string strQuarterText = "";
        string strFormText = "";
        string strMonthText = "";
        long lngHeaderId = 0;
        //
        BindingSource objBindingSource = new BindingSource();
        private BindingList<PANDetails> _results = new BindingList<PANDetails>();
        //
        enum enmRequestType
        {
            Login,
            PanValidation,
            LogOff
        }
        //
        string strNOTAVAILABLE = "NOT AVAILABLE";
        //
        string strExcelName = string.Empty;
        //
        System.Data.OleDb.OleDbConnection con;
        DataSet myDataSet;
        OleDbDataAdapter myCommand;
        string strExcelFilePath = string.Empty;
        //
        //-- Added By Abhishek Dey On 01/05/2020 --
        double dblFAYearId = 0, dblCompanyId = 0;
        //-----------------------------------------
        //--
        #endregion

        #region User Defined Events

        #region TrnProcessMonthlyDataExport_Load
        private void TrnProcessMonthlyDataExport_Load(object sender, EventArgs e)
        {
            try
            {
                txtCompany.Text = strCompanyText;
                txtFinancialYear.Text = strFAYearText;
                txtFormNo.Text = strFormText;
                txtMonth.Text = strMonthText;
                txtQuarter.Text = strQuarterText;

                ClearControls();
                //--
                //txtFinancialYear.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FA_YEAR FROM MST_ASSESSMENT WHERE ASST_ID = " + dblFAYearId));
                //txtCompany.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_NAME + ' [' + TAN_NO + ']' FROM MST_COMPANY WHERE COMPANY_ID = " + dblCompanyId));
                //
                string[] word = txtCompany.Text.Trim().Split('[');
                strTan = word[1].Remove(word[1].Length - 1, 1);
                 
                //
                rbnCSVOption.Checked = true;
                                
                //
                this.Cursor = Cursors.Default;
                //-----------
            }
            catch (Exception err_handler)
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        

        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            //
            GC.Collect();
            //
            blnVerificationComplete = false;
            //--
            this.Dispose();
            this.Close();
        }
        #endregion

       
        #region btnBack_Click
        private void btnBack_Click(object sender, EventArgs e)
        {
            btnExportToExcel.Text = "&Next";
            //tbcDeleteReturn.SelectTab(tbpWarning);
            //lblSteps.Text = "Step 1 of 2";
            //btnBack.Enabled = false;
            //btnBack.BackColor = System.Drawing.Color.LightGray;
            btnExportToExcel.Enabled = true;
            btnExportToExcel.BackColor = System.Drawing.Color.Lavender;
        }
        #endregion        


        #region btnSelectExcelPath_Click

        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
            // Create a new instance of FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
            // A new folder button will display in FolderBrowserDialog.
            folderBrowserDlg.ShowNewFolderButton = true;
            //Show FolderBrowserDialog
            DialogResult dlgResult = folderBrowserDlg.ShowDialog();
            if (dlgResult.Equals(DialogResult.OK))
            {
                //Show selected folder path in textbox1.
                txtExcelPath.Text = folderBrowserDlg.SelectedPath;
                //Browsing start from root folder.
                Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
            }
        }

        #endregion


        #region btnClose_Click

        private void btnClose_Click(object sender, EventArgs e)
        {
            //grpExport.Visible = false;
            //
            grpButtons.Enabled = true;
            //tbcExportData.Enabled = true;
        }

        #endregion


        #region btnExportToExcel_Click

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtExcelPath.Text.Trim()))
            {
                cmnService.J_UserMessage("Please select the folder path", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSelectExcelPath.Select();
                return;
            }
            //
            if (string.IsNullOrEmpty(txtDestinationFileName.Text.Trim()))
            {
                cmnService.J_UserMessage("Please enter the file name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDestinationFileName.Select();
                return;
            }
            //            

            #region Variable Declaration
            strExcelFilePath = string.Empty;
            //
            //-- Added By Abhishek Dey On 02/05/2020 --
            // FORM 16
            string strSL_NO = string.Empty;
            string strPAN_No = string.Empty;
            string strEmployee_Name = string.Empty;
            string strReference_No = string.Empty;
            string strPayment_Amount = string.Empty;
            string strPayment_Date = string.Empty;
            string strDeducted_Date = string.Empty;
            string strSection = string.Empty;
            string strTDS_Paid_Till_Date = string.Empty;
            string strTotal_Taxable_Income = string.Empty;
            string strMonthly_TDS = string.Empty;                        
            //--------------------------------------------------
            #endregion
            //--
            prgBar.Value = 0;
            //            
            strExcelFilePath = txtExcelPath.Text + "\\" + txtDestinationFileName.Text.Trim();
            //-- 2018/09/27
            if (rbnCSVOption.Checked == false)// return true; //-- 2019/01/22
            {
                if (cmnService.J_Right(strExcelFilePath, 5).ToUpper() != ".XLSX")
                {
                    strExcelFilePath = strExcelFilePath + ".XLSX";
                }
            }
            ////-- 
            if (rbnCSVOption.Checked == true)
            {
                if (Directory.Exists(strExcelFilePath) == true)
                {
                    cmnService.J_UserMessage("Folder exists with same name.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (rbnExcelOption.Checked == true)
            {
                // CHECK IF SAME NAME FILE EXIST
                if (File.Exists(strExcelFilePath))  //-- 01/01/2017 --
                {
                    cmnService.J_UserMessage("File exists with same name.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //--
                if (string.IsNullOrEmpty(txtExcelPath.Text.Trim()))
                {
                    cmnService.J_UserMessage("Please select specific folder for import");
                    return;
                }
                //
                if (string.IsNullOrEmpty(txtDestinationFileName.Text.Trim()))
                {
                    cmnService.J_UserMessage("Please specific the File Name");
                    return;
                }
            }
            //--
            if (cmnService.J_UserMessage("FINANCIAL YEAR \t: " + 
                 txtFinancialYear.Text + " \n " +
                        "COMPANY  \t: " + txtCompany.Text + " \n " +
                        "Form  \t: " + txtFormNo.Text + " \n " +
                        "Quarter  \t: " + txtQuarter.Text + " \n " +
                        "Month  \t: " + txtMonth.Text + " \n " +
                        "Are you sure you want to Export data ??", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;
            else
            {
                try
                {
                    //---                     
                    //
                    prgBar.Value = prgBar.Value + 5;  //-- 01/01/2018
                    //
                    if (CREATE_EXCEL_FILE(strExcelFilePath) == false)
                    {
                        cmnService.J_UserMessage("Some error occurred");
                        return;
                    }
                    else
                    {
                        //--------------------------------------------------------------------------------------------------
                        ////-- 19/02/2018 --
                        //if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Read me") == false) return;
                        ////
                        //this.Cursor = Cursors.WaitCursor;
                        ////
                        //if (WRITE_READ_ME_WORKSHEET(strExcelFilePath, "Read me", cmbFormNo.Text) == false) return;
                        //--------------------------------------------------------------------------------------------------
                        //--------------------------------------------------------------------------------------------------
                        //GC.Collect();
                        //
                        prgBar.Value = prgBar.Value + 5;  //-- 01/01/2018
                        //               
                        //
                        prgBar.Value = prgBar.Value + 5;   //-- 01/01/2018 --
                        //
                        this.Cursor = Cursors.WaitCursor;
                        //
                        //strExcelFilePath = txtExcelPath.Text + "\\" + strExcelName;
                        //-- 
                        // 02/05/2020                          

                        if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Monthly Data") == false) return;
                        //
                        prgBar.Value = prgBar.Value + 5;
                        //
                        // ASSIGN VALUE TO THE VARIABLE
                        #region ASSIGN VALUE TO THE VARIABLE
                        strSL_NO = "[Sl No]";
                        strPAN_No = "[PAN No]";
                        strEmployee_Name = "[Employee Name]";
                        strReference_No = "[Reference No]";
                        strPayment_Amount = "[Payment Amount]";
                        strPayment_Date = "[Payment Date]";
                        strDeducted_Date = "[Deducted Date]";
                        strSection = "[Section]";
                        strTDS_Paid_Till_Date = "[TDS Paid Till Date]";
                        strTotal_Taxable_Income = "[Total Taxable Income]";
                        strMonthly_TDS = "[Monthly TDS]";
                        #endregion
                        //
                        // QUERY OF FETCHING DATA
                        strQueryForm16 = @"SELECT DETAIL.DETAIL_SL_NO                                      AS " + strSL_NO + "," +
                                          "        DETAIL.EMPLOYEE_PAN                                     AS " + strPAN_No + "," +
                                          "        DETAIL.EMPLOYEE_NAME                                    AS " + strEmployee_Name + "," +
                                          "        DETAIL.EMPLOYEE_REF_NO                                  AS " + strReference_No + "," +
                                          "        DETAIL.PAYMENT_AMOUNT                                   AS " + strPayment_Amount + "," +
                                          "   " + cmnService.J_SQLDBFormat(" DETAIL.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "  AS " + strPayment_Date + "," +
                                          "   " + cmnService.J_SQLDBFormat("DETAIL.DEDUCTION_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeducted_Date + "," +
                                          "        MST_SECTION.SECTION_NO                                  AS " + strSection + "," +
                                          "        DETAIL.TDS_PAID_TILL_DATE                               AS " + strTDS_Paid_Till_Date + "," +
                                          "        DETAIL.TOT_TAXABLE_INCOME                               AS " + strTotal_Taxable_Income + "," +
                                          "        DETAIL.MONTHLY_TDS                                      AS " + strMonthly_TDS + " " +

                                          " FROM   ((TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL AS DETAIL INNER JOIN TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER AS HEADER " +
                                          "       ON DETAIL.SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = HEADER.SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID) " +
                                          "       INNER JOIN MST_SECTION " +
                                          "       ON DETAIL.SECTION_ID             = MST_SECTION.SECTION_ID) " +
                                          " WHERE  DETAIL.SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID            = " + lngHeaderId + " " +
                                          " ORDER BY DETAIL.DETAIL_SL_NO ";



                        //--- DELETE WORKSHEET
                        if (DELETE_WORKSHEET(strExcelFilePath, "Sheet1") == false)
                        {
                            //return;
                        }
                        prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                        if (DELETE_WORKSHEET(strExcelFilePath, "Sheet2") == false) //return;
                        {
                        }
                        prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                        if (DELETE_WORKSHEET(strExcelFilePath, "Sheet3") == false) //return;
                        {
                        }
                        //--
                        //ExportToCSV(strQueryDD, "C://test.csv");
                        //
                        prgBar.Value = prgBar.Value + 5;
                        //
                        if (ExportToExcelFromSQL(strQueryForm16, "Monthly Data") == false)
                        {
                            //cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmnService.J_UserMessage("Export data failed ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            prgBar.Value = 0;  //-- 01/01/2018  --
                            return;
                        }                        

                        //--------------------------------------------------------------


                        
                        prgBar.Value = prgBar.Value + 5;  //-- 01/01/2018 -- //-- 16/02/2018 --
                        
                        //--
                        prgBar.Value = prgBar.Value + 5;
                        //---------------------------
                        this.Cursor = Cursors.Default;
                        //grpExport.Visible = false;
                        //--
                        cmnService.J_UserMessage("Data Exported Successfully...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //
                        //-- 01/01/2017 --
                        ClearControls();
                        //
                        //-- 04/05/2020 --
                        if (rbnCSVOption.Checked == true)
                            txtDestinationFileName.Text = "CSV_EXPORT_PROCESS_MONTHLY_DATA_" + txtFinancialYear.Text.Trim().Substring(2).Replace(" - ", "") + "_" + strTan;
                        else
                            rbnCSVOption.Checked = true;
                        //--
                        //LoadControls();
                        //
                        tbcExportData.Enabled = true;
                        grpButtons.Enabled = true;
                        //
                        //btnExportToExcel.Enabled = false;
                        //btnExportToExcel.BackColor = System.Drawing.Color.LightGray;
                        //--
                        //grpControlSummary.Visible = false;
                        pnlLine1.Visible = false;
                        prgBar.Value = 0;
                        //--
                        //cmbFinancialYear.Select();
                        //
                        this.Cursor = Cursors.Default;
                        //
                        //btnXit_Click(sender, e);
                        //-----------------
                    }
                }
                //--
                catch (Exception err_handler)
                {
                    cmnService.J_UserMessage(err_handler.Message);
                }
            }
            //--------------------------------------------
        }

        #endregion

        #endregion

        #region User Define Functions

        //--
        #region ExportToExcelFromSQL
        private bool  ExportToExcelFromSQL(string strSQL, string SheetName)
        {
            //-- 20/02/2018 --
            //GC.Collect();
            GC.WaitForPendingFinalizers();
            //----------------
            //
            try
            {
                if (rbnCSVOption.Checked == true)  //-- 2019/01/22
                {
                    ExportToCSV(strSQL,Path.Combine(Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text), SheetName + ".csv"));
                    return true;
                }
                else
                {
                    myDataSet = new DataSet();
                    myDataSet = dmlService.J_ExecSqlReturnDataSet(strSQL);
                    //
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                    Microsoft.Office.Interop.Excel.Workbooks workbook = app.Workbooks;
                    prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 -
                    //
                    object m = Type.Missing;
                    Microsoft.Office.Interop.Excel.Workbook wb = workbook.Open(strExcelFilePath,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                    Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                    wsnew.Name = SheetName;
                    //

                    int colIndex = 0;
                    int rowIndex = 1;

                    foreach (DataColumn dc in myDataSet.Tables[0].Columns)
                    {
                        colIndex++;
                        wsnew.Cells[1, colIndex] = dc.ColumnName;
                    }
                    //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 -
                    foreach (DataRow dr in myDataSet.Tables[0].Rows)
                    {
                        rowIndex++;
                        colIndex = 0;

                        foreach (DataColumn dc in myDataSet.Tables[0].Columns)
                        {
                            colIndex++;
                            //wsnew.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                            //if (dr[dc.ColumnName].ToString().Length == 10 && dr[dc.ColumnName].ToString().Contains("/") == true)
                            if (dr[dc.ColumnName].ToString().Length == 10 
                                && (cmnService.J_Mid(dr[dc.ColumnName].ToString(), 2, 1) == "/" || cmnService.J_Mid(dr[dc.ColumnName].ToString(), 2, 1) == "-" ) 
                                && (cmnService.J_Mid(dr[dc.ColumnName].ToString(), 5, 1) == "/" || cmnService.J_Mid(dr[dc.ColumnName].ToString(), 5, 1) == "-"))
                                wsnew.Cells[rowIndex, colIndex] = "'" + dr[dc.ColumnName];
                            else
                                wsnew.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                        }
                    }

                    wsnew.Columns.AutoFit();

                    wb.Save();
                    wb.Close(m, m, m);

                    wb = null;
                    workbook = null;
                    //
                    wsnew = null;

                    //Marshal.ReleaseComObject(wsnew);
                    //Marshal.ReleaseComObject(wsnew);

                    //wsnew.Delete(); //-- 20/02/2018 --

                    app.Quit();
                    app = null;
                }
                //--
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
            return true;
        }
        #endregion

        //-- 20/02/2018 --
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
        //----------------

        #region ClearControls
        private void ClearControls()
        {           
            
            txtExcelPath.Text = string.Empty;
            txtDestinationFileName.Text = string.Empty;
            //
            //--
            //rbnCSVOption.Checked = true;
            //----------------
        }
        #endregion

              
        #region CREATE EXCEL FILE
        // SOURCE PATH : http://csharp.net-informations.com/excel/csharp-create-excel.htm
        private bool CREATE_EXCEL_FILE(string ExcelFilePath)
        {
            try
            {                
                //--
                if (rbnCSVOption.Checked == true)
                {
                    if (Directory.Exists(ExcelFilePath) == false)
                        Directory.CreateDirectory(ExcelFilePath);
                    return true; //-- 2019/01/22
                }
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


        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath, string WorksheetName)
        {
            try
            {                
                //
                if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
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

        #region DELETE WORKSHEET
        private bool DELETE_WORKSHEET(string ExcelFilePath, string ExcelSheet)
        {
            try
            {
                if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22        
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
               

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=EztMuIUEnAo");
        }
        #region rbnCSVOption_CheckedChanged
        private void rbnCSVOption_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnCSVOption.Checked == true)
            {
                txtDestinationFileName.Text = "CSV_EXPORT_PROCESS_MONTHLY_DATA_" + txtFinancialYear.Text.Trim().Substring(2).Replace(" - ", "") + "_" + strTan;
            }
            else
            {
                txtDestinationFileName.Text = "XLS_EXPORT_PROCESS_MONTHLY_DATA_" + txtFinancialYear.Text.Trim().Substring(2).Replace(" - ", "") + "_" + strTan;
            }
        }
        #endregion

        #region rbnExcelOption_CheckedChanged
        private void rbnExcelOption_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnExcelOption.Checked == true)
            {
                txtDestinationFileName.Text = "XLS_EXPORT_PROCESS_MONTHLY_DATA_" + txtFinancialYear.Text.Trim().Substring(2).Replace(" - ", "") + "_" + strTan;
            }
            else
            {
                txtDestinationFileName.Text = "CSV_EXPORT_PROCESS_MONTHLY_DATA_" + txtFinancialYear.Text.Trim().Substring(2).Replace(" - ", "") + "_" + strTan;
            }
        }
        #endregion

        #endregion
        //----------------------------------------

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
                    // 03/05/2020
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
        //----------------
        #endregion

       
    }

}