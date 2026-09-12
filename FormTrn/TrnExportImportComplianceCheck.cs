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
using TDSMAN.FormMst;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
//--
using Excel = Microsoft.Office.Interop.Excel.Worksheet;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion



namespace TDSMAN.FormTrn
{
    public partial class TrnExportImportComplianceCheck : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region Objects & Variables declaration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        ExcelService ExcelService = new ExcelService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //-----------------------------------------------------------------------
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        //string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        //string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //Microsoft.Office.Interop.Access.Application Access = new Microsoft.Office.Interop.Access.Application();
        //-----------------------------------------------------------------------
        int intCaratPosition = 0;
        string strFVUPath = "";
        long lngBasicInfoID = 0;
        string strErrorWorksheetName = "Validation Error";
        //-----------------------------------------------------------------------
        string strNewDeducteeWorkSheetName = "New Deductees Found";
        string strDeductionsChVIAWorkSheetName = "Deductions ChVI-A"; //-- 2020/05/13
        long lngRowCount;
        long lngNewDeducteesCreated;
        //
        string strErrorMessage = "";

        //string strTemporaryfileChallanPath = "";
        //string strTemporaryfileDeducteePath = "";
        //string strTemporaryfileSalaryPath = "";
        //string strTemporaryfileForm16SalaryPath = "";
        //string strTemporaryfileSalaryCHVIAPath = "";
        string strTemporaryfileCompliancePath = "";

        OleDbDataAdapter myCommand;
        string strConnectionString = "";
        OleDbConnection con;
        //
        bool blnOpenTabPage = false;

        //ADDED BY DHRUB FOR ADDING NEW FIELDS INTO TEXT
        //      FOR SALARY DETAILS 2013-14  AND ONWARDS 
        int intInitialColumnAfterZ = 0;
        //int intInitialColumnNumbering = 1;
        string strExcelFieldAsciiAfterZ = "";
        string strAAsciiAfterZ = "";
        string strBAsciiAfterZ = "";
        string strCAsciiAfterZ = "";
        string strDAsciiAfterZ = "";
        string strEAsciiAfterZ = "";
        //
        string strExcelComplianceDate = "";

        //ADDED BY DHRUB ON 30/12/2013 FOR ADDING THE EXCEL IMPORT VALUES 
        //     FOR INTEREST ALLOCATED AND OTHER INTEREST ALLOCATED 
        string strImportValuesToAllocated = "";

        //Added by Dhrub Mukherjee On 07/01/2014
        //---------------------------------------
        //---------------------------------------
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        ToolTip tllTip = new ToolTip();

        double dblTotalExcelRecords = 0; //-- 2015/07/31
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        bool blnEmpReferenceNo = false; //-- 2020/06/04
        //--
        string strCheckExcelStructureMessage = "";
        //
        bool blnSALARY_DETAILS_CHVIA = false;
        bool blnSection194NF = false, blnSection194N = false;


        #endregion

        #region System Generated Code
        public TrnExportImportComplianceCheck()
        {
            InitializeComponent();
            ////--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
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


        #region TrnExportImportComplianceCheck_Load
        private void TrnExportImportComplianceCheck_Load(object sender, EventArgs e)
        {

            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
        }
        #endregion

        #region TrnExportImportComplianceCheck_Activated
        private void TrnExportImportComplianceCheck_Activated(object sender, EventArgs e)
        {
            lblTitle.Text = "Export / Import - Compliance Check";
            //
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
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbSelectCompanyName, "All Companies") == false) return;
            }
            //
        }
        #endregion

        #region btnSelectExcelFile_Click
        private void btnSelectExcelFile_Click(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)
            //    strFVUPath = cmnService.J_OpenFileDialog("Excel File | *.xlsx", "Excel File | *.xlsx", "Choose the Excel File to import");
            //else
            string strExcelFilePath = cmnService.J_OpenFileDialog("Excel File | *.xls; *.xlsx", "Excel File | *.xls; *.xlsx", "Choose the Excel File to import");
            //--
            if (strExcelFilePath != "")
                txtExcelFilePath.Text = strExcelFilePath;

        }

        #endregion

        #region btnSelectExcelPath_Click
        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
            string strExcelFolder = cmnService.J_OpenFolderDialog("Select Destination Folder");
            txtExcelFolderPath.Text = strExcelFolder;
        }
        #endregion

        #region btnExportToExcel_Click
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //
                if (txtExcelFolderPath.Text == "")
                {
                    cmnService.J_UserMessage("Select Folder to Save the CSV File");
                    btnSelectExcelPath.Select();
                    return;
                }
                string strCSVFileName = "List of PAN";
                prgExportBar.Value = 0;
                //-- csv file to export
                if (cmbSelectCompanyName.SelectedIndex > 0)
                {
                    string strPAN = cmnService.J_Left(cmnService.J_Right(cmbSelectCompanyName.Text.Trim(), 11), 10);
                    //
                    strSQL = @"SELECT COUNT(*) AS PAN
                        FROM(
                        SELECT  DEDUCTEE_PAN
                        FROM    MST_DEDUCTEE, TRN_DEDUCTEE_DETAILS, TRN_BASIC_INFO
                        WHERE   MST_DEDUCTEE.DEDUCTEE_ID = TRN_DEDUCTEE_DETAILS.PARTY_ID
                        AND     TRN_BASIC_INFO.BASIC_INFO_ID = TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID
                        AND     INACTIVE_FLAG = 0  AND DEDUCTEE_PAN <> 'PANNOTAVBL'
                        AND     TRN_BASIC_INFO.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbSelectCompanyName, cmbSelectCompanyName.SelectedIndex)) + @"
                        UNION 
                        SELECT  DEDUCTEE_PAN
                        FROM    COR_TRN_DEDUCTEE_DETAILS, COR_HDR_BATCH, COR_HDR_COMPANY
                        WHERE   COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID
                        AND     COR_HDR_BATCH.BATCH_HEADER_ID            = COR_HDR_COMPANY.BATCH_HEADER_ID
                        AND     COR_HDR_COMPANY.TAN_NO = '" + strPAN + @"'
                        AND     DEDUCTEE_PAN <> 'PANNOTAVBL'
                        GROUP BY DEDUCTEE_PAN)  AS REGULAR";
                    if (cmnService.J_UserMessage("Export " + Convert.ToInt64(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) + " no. of deductee(s) ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    //
                    strCSVFileName = strCSVFileName + " - " + strPAN;
                    //
                    prgExportBar.Value = prgExportBar.Value + 5;
                    //
                    strSQL = @"SELECT DEDUCTEE_PAN AS PAN
                        FROM(
                        SELECT  DEDUCTEE_PAN
                        FROM    MST_DEDUCTEE, TRN_DEDUCTEE_DETAILS, TRN_BASIC_INFO
                        WHERE   MST_DEDUCTEE.DEDUCTEE_ID = TRN_DEDUCTEE_DETAILS.PARTY_ID
                        AND     TRN_BASIC_INFO.BASIC_INFO_ID = TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID
                        AND     INACTIVE_FLAG = 0  AND DEDUCTEE_PAN <> 'PANNOTAVBL'
                        AND     TRN_BASIC_INFO.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbSelectCompanyName, cmbSelectCompanyName.SelectedIndex)) + @"
                        UNION 
                        SELECT  DEDUCTEE_PAN
                        FROM    COR_TRN_DEDUCTEE_DETAILS, COR_HDR_BATCH, COR_HDR_COMPANY
                        WHERE   COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID
                        AND     COR_HDR_BATCH.BATCH_HEADER_ID            = COR_HDR_COMPANY.BATCH_HEADER_ID
                        AND     COR_HDR_COMPANY.TAN_NO = '" + strPAN + @"'
                        AND     DEDUCTEE_PAN <> 'PANNOTAVBL'
                        GROUP BY DEDUCTEE_PAN)  AS REGULAR
                        ORDER BY REGULAR.DEDUCTEE_PAN";
                    prgExportBar.Value = prgExportBar.Value + 5;
                    //
                }
                else
                {
                    if (cmnService.J_UserMessage("Export all deductee(s) ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    prgExportBar.Value = prgExportBar.Value + 5;
                    strSQL = @"SELECT DEDUCTEE_PAN AS PAN
                        FROM(
                        SELECT  DEDUCTEE_PAN
                        FROM    MST_DEDUCTEE
                        WHERE   INACTIVE_FLAG = 0  AND DEDUCTEE_PAN <> 'PANNOTAVBL'
                        UNION
                        SELECT  DEDUCTEE_PAN
                        FROM    COR_TRN_DEDUCTEE_DETAILS WHERE DEDUCTEE_PAN <> 'PANNOTAVBL'
                        GROUP BY DEDUCTEE_PAN)  AS REGULAR
                        ORDER BY REGULAR.DEDUCTEE_PAN";
                    prgExportBar.Value = prgExportBar.Value + 5;
                }
                //ExportToCSV(strSQL, Path.Combine(Path.Combine(txtExcelFolderPath.Text, strCSVFileName), "Sheet1.csv"));
                ExportToCSV(strSQL, Path.Combine(txtExcelFolderPath.Text, strCSVFileName + ".csv"));
                //-- get PANs in a list
                //-- max 10,000 in a single file
                for (int i = prgExportBar.Minimum; i <= prgExportBar.Maximum; i++)
                {
                    prgExportBar.PerformStep();
                }
                this.Cursor = Cursors.Default;
                txtExcelFolderPath.Text = ""; cmbSelectCompanyName.SelectedIndex = 0;
                cmnService.J_UserMessage("Export Completed...");
            }
            catch (Exception err)
            {
                prgExportBar.Value = 0;
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region btnImport_Click
        private void btnImport_Click(object sender, EventArgs e)
        {
            //
            #region VALIDATE FIELDS
            //-----------------------------------------------------------------------
            //-- FVU FILE SELECTED
            //-----------------------------------------------------------------------
            if (txtExcelFilePath.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Excel file not selected");
                btnSelectExcelPath.Select();
                return;
            }
            // FILE SHOULD BE FVU
            if (Path.GetExtension(txtExcelFilePath.Text).ToUpper() != ".XLS" && Path.GetExtension(txtExcelFilePath.Text).ToUpper() != ".XLSX")
            {
                cmnService.J_UserMessage("Selected file should be a Excel file");
                btnSelectExcelPath.Select();
                return;
            }
            // FILE EXIST
            if (cmnService.J_IsFileExist(txtExcelFilePath.Text) == false)
            {
                cmnService.J_UserMessage("Selected Excel file not found");
                btnSelectExcelPath.Select();
                return;
            }
            // FILE OPEN
            //-- ANIK 2011-09-09
            string strPath = txtExcelFilePath.Text.ToString();
            //if (cmnService.J_IsProcessOpen(txtExcelPath.Text) == true)
            if (TdsMan.T_isFileOpenOrReadOnly(ref strPath) == true)
            {
                cmnService.J_UserMessage("Selected Excel file is open");
                btnSelectExcelPath.Select();
                return;
            }
            #endregion
            //
            prgImportBar.Value = 0;
            //
            if (cmnService.J_UserMessage("Proceed Excel Import??", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            //
            prgImportBar.Value = prgImportBar.Value + 5;
            this.Refresh();
            // CREATE TEMP TABLES 
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                //MessageBox.Show("1");
                if (CREATE_TEMP_TABLES_SQL() == false)
                {
                    cmnService.J_UserMessage("Temporary Tables Not created", MessageBoxIcon.Exclamation);
                    this.Cursor = Cursors.Default;
                    prgImportBar.Value = 0;
                    return;
                }
            }
            //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            //{
            //    if (CREATE_TEMP_TABLES() == false)
            //    {
            //        cmnService.J_UserMessage("Temporary Tables Not created", MessageBoxIcon.Exclamation);
            //        this.Cursor = Cursors.Default;
            //        prgImportBar.Value = 0;
            //        return;
            //    }
            //}
            prgImportBar.Value = prgImportBar.Value + 5;
            this.Refresh();
            // GET DATA FROM EXCEL ACCESS       
            //
            //lblProgressDisplayMessage.Visible = true;
            //lblProgressDisplayMessage.Text = "Transferring Data";
            //
            //start do work
            if (GET_DATA_FROM_EXCEL_ACCESS() == false)
            {
                cmnService.J_UserMessage(strErrorMessage);
                this.Cursor = Cursors.Default;
                prgImportBar.Value = 0;
                return;
            }
            prgImportBar.Value = prgImportBar.Value + 5;
            this.Refresh();
            //
            // VALIDATE DATA     
            //lblProgressDisplayMessage.Visible = true;
            //lblProgressDisplayMessage.Text = "Validation Started";
            if (VALIDATE_DATA() == false)
            {
                //--
                TdsMan.SHRINK_DATABASE();
                //--
                cmnService.J_UserMessage("Data Validation failed", MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                prgImportBar.Value = 0;
                return;
            }
            //

            //--
            if (DELETE_WORKSHEET(txtExcelFilePath.Text, strErrorWorksheetName) == false)
            {
                cmnService.J_UserMessage("Error Sheet Deletion failed", MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                prgImportBar.Value = 0;
                return;
            }
            //--
            if (DELETE_WORKSHEET(txtExcelFilePath.Text, strNewDeducteeWorkSheetName) == false)
            {
                cmnService.J_UserMessage("New Deductee Sheet Deletion failed", MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                prgImportBar.Value = 0;
                return;
            }
            prgImportBar.Value = prgImportBar.Value + 5;
            this.Refresh();
            //
            //lblProgressDisplayMessage.Visible = true;
            //lblProgressDisplayMessage.Text = "New Worksheet created";
            //
            if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""))) > 0)
            {
                //--
                if (CREATE_NEW_WORKSHEET(txtExcelFilePath.Text) == false)
                {
                    cmnService.J_UserMessage("Error Sheet Creation failed", MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    prgImportBar.Value = 0;
                    return;
                }
                prgImportBar.Value = prgImportBar.Value + 5;
                this.Refresh();
                //--
                //
                //if (Path.GetExtension(txtExcelPath.Text).ToUpper() == ".XLS")
                //{
                //    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""))) >= 65536)
                //    {
                //        cmnService.J_UserMessage("The Excel has more than '65536' nos of errors, which exceeds the max rows Excel file (.xls).\nPlease Save As the file to .xlsx and try Import again.", MessageBoxIcon.Error);
                //        this.Cursor = Cursors.Default;
                //        prgImportBar.Value = 0;
                //        return;
                //    }
                //}
                //--
                //lblProgressDisplayMessage.Visible = true;
                //lblProgressDisplayMessage.Text = "Error sheet writing started";
                //            
                if (WRITE_ERROR_WORKSHEET(txtExcelFilePath.Text) == false)
                {
                    cmnService.J_UserMessage("Writing Error Sheet failed", MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    prgImportBar.Value = 0;
                    return;
                }
                //
                TdsMan.SHRINK_DATABASE();
                //--
                for (int i = prgImportBar.Minimum; i <= prgImportBar.Maximum; i++)
                {
                    prgImportBar.PerformStep();
                }
                this.Cursor = Cursors.Default;

                con.Close();

                //cmnService.J_UserMessage("Excel File Validation failed \n Check the <Validation Error> Sheet of the Excel file ", MessageBoxIcon.Exclamation);
                if (cmnService.J_UserMessage("Some inconsistent data present in Excel File. So Import is not possible.\n Do you want to view those inconsistencies ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    if (File.Exists(txtExcelFilePath.Text) == true)
                    {
                        System.Diagnostics.Process.Start(txtExcelFilePath.Text);
                    }
                }
                prgImportBar.Value = 0;
                return;
            }
            //--
            if (UPDT_MODE() == false)
            {
                cmnService.J_UserMessage("Updating failed");
                this.Cursor = Cursors.Default;
                prgImportBar.Value = 0;
                return;
            }
            //--
            prgImportBar.Value = prgImportBar.Value + 5;
            this.Refresh();
            //--
            for (int i = prgImportBar.Minimum; i <= prgImportBar.Maximum; i++)
            {
                prgImportBar.PerformStep();
            }
            this.Cursor = Cursors.Default;
            //
            cmnService.J_UserMessage("PAN(s) marked for for Section 206AB & 206CCA sucessfully.", MessageBoxIcon.Information);
            //--
            if (cmnService.J_UserMessage("Do you want to view the updated Deductee(s)?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //cmnService.J_ShowChildForm(new MstDeducteeMasterComplianceCheck(), this, "Deductee Master - Compliance Check for Section 206AB & 206CCA");

                cmnService.J_ShowChildForm(new MstDeducteeMasterComplianceCheck(), J_Var.frmMain, "Deductee Master - Compliance Check for Section 206AB & 206CCA");
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

        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnExportImportComplianceCheck");
            Tan.ShowDialog();
            //--------------
            cmbSelectCompanyName.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #endregion

        #region User Defined Functions

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
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
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
                if (prgExportBar.Value < prgExportBar.Maximum)
                    prgExportBar.Value = prgExportBar.Value + 5;
                //--
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

        #region CREATE_TEMP_TABLES_SQL
        private bool CREATE_TEMP_TABLES_SQL()
        {
            try
            {
                #region T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK
                dmlService.J_BeginTransaction();
                //MessageBox.Show("2");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + "") == true)
                {
                    //MessageBox.Show("2.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //MessageBox.Show("3");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + "") == false)
                {
                    //MessageBox.Show("3.1");
                    strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + @" (
                                            " + cmnService.J_GetDataType("DEDUCTEE_COMPLIANCE_CHECK_ID", J_Identity.YES) + @",
                                            " + cmnService.J_GetDataType("PAN_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("PAN", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("STATUS_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("STATUS", J_ColumnType.String, 255) + @")";
                    //MessageBox.Show(strSQL);
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                #endregion
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

        #region GET_DATA_FROM_EXCEL_ACCESS
        private bool GET_DATA_FROM_EXCEL_ACCESS()
        {
            int intLN = 0;
            try
            {
                #region VARIABLE_DECLARATION
                // CHALLAN DETAILS
                int intLineNumberChallan = 0;
                string strCell0 = "", strCell1 = "", strCell2 = "", strCell3 = "", strCell4 = "", strCell5 = "";
                //
                // CREATE THE SUBFOLDER.
                string strStartupPath = "";
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                    strStartupPath = Path.Combine(J_Var.J_pEnterpriseServerPath, "TMP FOLDER");
                else
                    strStartupPath = Path.Combine(Application.StartupPath, "TMP FOLDER");
                if (Directory.Exists(strStartupPath) == false)
                    // DELETE IF THE FILE EXISTS.
                    Directory.CreateDirectory(strStartupPath);
                //
                //string strTemporaryfileChallanPath = Path.Combine(strStartupPath, "ChalllanDetails.txt");
                //string strTemporaryfileDeducteePath = Path.Combine(strStartupPath, "DeducteeDetails.txt");
                //string strTemporaryfileSalaryPath = Path.Combine(strStartupPath, "SalaryDetails.txt");
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                {
                    strTemporaryfileCompliancePath = Path.Combine(strStartupPath, TDSMAN.Classes.TDSMAN.T_pProductSerial + "_Compliance.txt");
                    if (File.Exists(strTemporaryfileCompliancePath) == true)
                        File.Delete(strTemporaryfileCompliancePath);
                    //
                }
                else
                {
                    strTemporaryfileCompliancePath = Path.Combine(strStartupPath, "Compliance.txt");
                }
                string tableName = "";
                string textfileName = "";

                #endregion
                //
                strExcelComplianceDate = "";
                //if (KILL_EXCEL() == false)
                //    return false;

                string strConnectionString = "";

                //MAKING CONNECTION TO THE EXCEL FILE
                if (Path.GetExtension(txtExcelFilePath.Text.Trim().ToLower()) == ".xls")
                    strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtExcelFilePath.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                else if (Path.GetExtension(txtExcelFilePath.Text.Trim().ToLower()) == ".xlsx")
                    strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtExcelFilePath.Text + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\"";

                con = new OleDbConnection(strConnectionString);
                //                
                con.Open();
                //--
                DataTable dt;

                dt = con.GetSchema("tables");
                string strSheetName = "";
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    //if ("'" + TableName.Trim().ToUpper() + "$'" == Convert.ToString(dt.Rows[i]["TABLE_NAME"]).Trim().ToUpper())
                    //{
                    //    dt.Dispose();
                    //    //cn.Dispose();
                    //    return true;
                    //}
                    strSheetName = Convert.ToString(dt.Rows[i]["TABLE_NAME"]).Trim().ToUpper();
                    break;
                }
                dt.Dispose();
                //con.Close();
                //con.Dispose();
                //strSheetName = strSheetName.Replace("#", ".");
                //// READ EXCEL FILE
                DataSet myDataSet;
                OleDbDataAdapter myCommand;
                //
                #region INSERT DETAILS

                #region TRANSFERRING DATA FROM EXCEL TO TEXT FILE

                // INSERT CHALLAN DETAILS

                //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                myDataSet = new DataSet();
                //
                myCommand = new OleDbDataAdapter("SELECT * FROM [" + strSheetName + "]", con);
                myCommand.Fill(myDataSet);
                StreamWriter StreamWriter = cmnService.J_ReturnStreamWriter(strTemporaryfileCompliancePath);
                //Travers through each row in the dataset
                foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
                {
                    //lblProgressDisplayMessage.Visible = true;
                    //
                    //Stores info in Datarow into an array
                    Object[] cells = myDataRow.ItemArray;
                    //
                    intLineNumberChallan = intLineNumberChallan + 1;
                    int intColumnValue = 64;
                    //
                    int intRowIndex = 0;

                    strCell0 = Convert.ToString(cells[0]).ToUpper();
                    //
                    strCell1 = Convert.ToString(cells[1]).ToUpper();
                    if (strCell1.Contains("SEARCH RESULT") == true)
                    {
                        strExcelComplianceDate = cmnService.J_Mid(strCell1, 17, 10);
                    }
                    //
                    strCell2 = Convert.ToString(cells[2]).ToUpper();
                    strCell3 = Convert.ToString(cells[3]).ToUpper();
                    strCell4 = Convert.ToString(cells[4]).ToUpper();
                    strCell5 = Convert.ToString(cells[5]).ToUpper();

                    // CHECK BLANK ROW TO EXIT
                    if (strCell0 != "" &&
                            strCell1 != "" &&
                                strCell2 != "" &&
                                    strCell3 != "" &&
                                        strCell4 != "" &&
                                            strCell5 != "")
                        cmnService.J_WriteLine(ref StreamWriter, TdsMan.T_WriteField(intLineNumberChallan.ToString()) +
                                            //TdsMan.T_WriteField(strCell0) 
                                            TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 2)) + (Convert.ToString(intLineNumberChallan + 1)))) +
                                            TdsMan.T_WriteField(strCell1) + //TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 2)) + (Convert.ToString(intLineNumberChallan + 1)))) +
                                                                            //TdsMan.T_WriteField(strCell2) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 3)) + (Convert.ToString(intLineNumberChallan + 1)))) +
                                                                            //TdsMan.T_WriteField(strCell3) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 4)) + (Convert.ToString(intLineNumberChallan + 1)))) +
                                                                            //TdsMan.T_WriteField(strCell4) + 
                                            TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 6)) + (Convert.ToString(intLineNumberChallan + 1)))) +
                                            TdsMan.T_WriteField(strCell5));
                }
                myDataSet.Dispose();
                myCommand.Dispose();

                StreamWriter.Flush();
                StreamWriter.Close();

                #endregion

                #region TRANSFERING DATA FROM TEXT TO ACCESS
                //Added by INDRAJIT on 14-03-2012

                //TABLE NAME TO BE CREATED
                //tableName = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + "";
                tableName = TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK;

                //TEXT FILE NAME
                textfileName = "Compliance";

                TdsMan.T_ReplaceDoubleQuotesinFile(strTemporaryfileCompliancePath, true);


                //IMPORTING THE DATA FROM THE TEXT FILE DIRECTLY TO THE 
                ImportTextToTables(tableName, textfileName, strTemporaryfileCompliancePath, false);

                // DELETE THE TXT FILE
                if (File.Exists(strTemporaryfileCompliancePath) == true)
                    File.Delete(strTemporaryfileCompliancePath);

                #endregion                    
                //--
                #endregion
                //
                myDataSet.Dispose();
                myCommand.Dispose();

                con.Close();
                con.Dispose();
                //
                intLN = 0;
                return true;
                //############################################
            }
            catch (Exception e)
            {
                //strErrorMessage = "Invalid data format in Excel file, please check. " + intLN;
                //-- 2015/10/14
                cmnService.J_UserMessage(e.Message);
                con.Close();
                con.Dispose();
                return false;
            }
        }
        #endregion        

        #region ImportTextToTables

        private void ImportTextToTables(string tbl, string txtfile, string FilePath, bool hdr)
        {
            //Added by INDRAJIT on 14-03-2012

            //Check 'n Create SCHEMA file for Temp Tables
            string strFolderPath = cmnService.J_GetDirectoryName(strTemporaryfileCompliancePath);

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

            #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + "
            StreamWriter.WriteLine(@"Col1=DEDUCTEE_COMPLIANCE_CHECK_ID Integer
                                         Col2=PAN_CELL Char
                                         Col3=PAN Char
                                         Col4=STATUS_CELL Char
                                         Col5=STATUS Char");
            #endregion

            //
            StreamWriter.Close();
            //
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

        #region VALIDATE_DATA
        private bool VALIDATE_DATA()
        {
            string strSheetName = "";
            string strMidSubString = "";
            string strUpper = "";
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
                //
                #region EMPLOYEE PAN

                //DELETE PAN = 'PAN'
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " WHERE PAN = 'PAN'";
                dmlService.J_ExecSql(strSQL);
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " SET PAN = '' WHERE PAN IS NULL";
                dmlService.J_ExecSql(strSQL);


                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " " +
                    "     WHERE  PAN = ''" +
                    "     AND    PAN NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                 WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN_CELL AS ERROR_CELL," +
                        "             'PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                       IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN      = '' ";


                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " " +
                    "     WHERE  LEN(PAN) <> 10" +
                    "     AND    PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                      WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN_CELL AS ERROR_CELL," +
                        "             'PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN_CELL  = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                        IS NULL " +
                        "     AND    LEN(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN) <> 10 ";

                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //PAN STRUCTURE CHECK
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = "SELECT COUNT(*)" +
                         "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " " +
                         "     WHERE  (ISNUMERIC(LEFT(PAN,1)) <> 0" +
                         "     OR     ISNUMERIC(SUBSTRING(PAN,2,1)) <> 0 " +
                         "     OR     ISNUMERIC(SUBSTRING(PAN,3,1)) <> 0 " +
                         "     OR     ISNUMERIC(SUBSTRING(PAN,4,1)) <> 0 " +
                         "     OR     ISNUMERIC(SUBSTRING(PAN,5,1)) <> 0 " +
                         "     OR     ISNUMERIC(SUBSTRING(PAN,6,4)) = 0 " +
                         "     OR     ISNUMERIC(RIGHT(PAN,1)) = -1)" +
                         "     AND    PAN <> 'PANNOTAVBL'" +
                         "     AND    PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                         "                                      WHERE ERR_SHEET = '" + strSheetName + "')";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = "SELECT COUNT(*)" +
                     "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " " +
                     "     WHERE  (ISNUMERIC(LEFT(PAN,1)) <> 0" +
                     "     OR     ISNUMERIC(MID(PAN,2,1)) <> 0 " +
                     "     OR     ISNUMERIC(MID(PAN,3,1)) <> 0 " +
                     "     OR     ISNUMERIC(MID(PAN,4,1)) <> 0 " +
                     "     OR     ISNUMERIC(MID(PAN,5,1)) <> 0 " +
                     "     OR     ISNUMERIC(MID(PAN,6,4)) = 0 " +
                     "     OR     ISNUMERIC(RIGHT(PAN,1)) = -1)" +
                     "     AND    PAN <> 'PANNOTAVBL'" +
                     "     AND    PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                     "                                      WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN_CELL AS ERROR_CELL," +
                            "             'PAN_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL                                         IS NULL " +
                            "     AND   (ISNUMERIC(LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 1))   <> 0 " +
                            "     OR     ISNUMERIC(SUBSTRING(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 2, 1)) <> 0 " +
                            "     OR     ISNUMERIC(SUBSTRING(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 3, 1)) <> 0 " +
                            "     OR     ISNUMERIC(SUBSTRING(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 4, 1)) <> 0 " +
                            "     OR     ISNUMERIC(SUBSTRING(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 5, 1)) <> 0 " +
                            "     OR     ISNUMERIC(SUBSTRING(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 6, 4))  = 0 " +
                            "     OR     ISNUMERIC(RIGHT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 1))  = -1) " +
                            "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN <> 'PANNOTAVBL' ";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN_CELL AS ERROR_CELL," +
                            "             'PAN_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL                                         IS NULL " +
                            "     AND   (ISNUMERIC(LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 1))   <> 0 " +
                            "     OR     ISNUMERIC(MID(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 2, 1)) <> 0 " +
                            "     OR     ISNUMERIC(MID(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 3, 1)) <> 0 " +
                            "     OR     ISNUMERIC(MID(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 4, 1)) <> 0 " +
                            "     OR     ISNUMERIC(MID(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 5, 1)) <> 0 " +
                            "     OR     ISNUMERIC(MID(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 6, 4))  = 0 " +
                            "     OR     ISNUMERIC(RIGHT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN, 1))  = -1) " +
                            "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN <> 'PANNOTAVBL' ";
                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                #endregion
                //
                #region STATUS
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " SET STATUS = '' WHERE STATUS IS NULL";
                dmlService.J_ExecSql(strSQL);
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    //strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " SET STATUS = RTRIM(REPLACE(STATUS,'^',''))";
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " SET STATUS = SUBSTRING(RTRIM(STATUS), 1, LEN(RTRIM(STATUS))-1)";
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " SET STATUS = SUBSTRING(RTRIM(STATUS), 1, LEN(RTRIM(STATUS))- 2)";
                    dmlService.J_ExecSql(strSQL);
                }

                //VALIDITY CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + "" +
                    "     WHERE  STATUS <> ''" +
                    "     AND    " + strUpper + "(STATUS) <> 'N'" +
                    "     AND    " + strUpper + "(STATUS) <> 'Y'" +
                    "     AND    " + strUpper + "(STATUS) <> 'NO'" +
                    "     AND    " + strUpper + "(STATUS) <> 'YES'" +
                    "     AND    " + strUpper + "(STATUS) <> 'YES*'" +
                    "     AND    " + strUpper + "(STATUS) <> '-'" +
                    "     AND    STATUS_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                        WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".STATUS_CELL AS ERROR_CELL," +
                        "             'STATUS_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".STATUS_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL        IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".STATUS        <> '' " +
                        "     AND    " + strUpper + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".STATUS) <> 'N' " +
                        "     AND    " + strUpper + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".STATUS) <> 'Y' " +
                        "     AND    " + strUpper + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".STATUS) <> 'NO' " +
                        "     AND    " + strUpper + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".STATUS) <> '-' " +
                        "     AND    " + strUpper + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".STATUS) <> 'YES' " +
                        "     AND    " + strUpper + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".STATUS) <> 'YES*' ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion
                //
                return true;
            }
            catch (Exception err)
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
                //if (KILL_EXCEL() == false)
                //    return false;
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
                //
                wb.Save();
                wb.Close(m, m, m);
                //
                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
        }
        #endregion

        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath)
        {
            try
            {
                //if (KILL_EXCEL() == false)
                //    return false;
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
            catch (Exception ERR)
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
            string strMatchSheetName = T_Sheet_Name.COMPLIANCE_CHECK;
            int intSkipIF = 0;
            try
            {
                //if (KILL_EXCEL() == false)
                //    return false;
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
                wsnew.get_Range("B4", m).Value2 = T_Sheet_Name.COMPLIANCE_CHECK;
                wsnew.get_Range("B4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
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
                    return false;
                }
                while (drdGetErrorSheetRecord.Read())
                {
                    if (intSkipIF == 0)
                    {
                        strMatchSheetName = drdGetErrorSheetRecord["ERR_SHEET"].ToString();
                        if (strMatchSheetName != T_Sheet_Name.COMPLIANCE_CHECK)
                        {
                            lngErrorSheetRow = lngErrorSheetRow + 2;
                            //
                            wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = T_Sheet_Name.COMPLIANCE_CHECK;
                            wsnew.get_Range("B" + lngErrorSheetRow, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                            //
                            lngErrorSheetRow = lngErrorSheetRow + 1;
                            intSkipIF = 1;
                        }
                    }
                    //
                    wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = "Cell : " + drdGetErrorSheetRecord["ERR_CELL"].ToString() + " - [" + drdGetErrorSheetRecord["ERR_COLUMN"].ToString().Replace("_", " ").Replace("CELL", "") + "] " + drdGetErrorSheetRecord["ERR_TYPE"].ToString();
                    //wsnew.get_Range("B" + lngErrorSheetRow, m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    //
                    lngErrorSheetRow = lngErrorSheetRow + 1;
                    //
                    //prgBar.Value = prgBar.Value + 1;
                    //this.Refresh();
                    //
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


        #region lnkReportingPortal_LinkClicked
        private void lnkReportingPortal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(lnkReportingPortal.Text.Trim());
        }
        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0108", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        #endregion

        #region UPDT_MODE
        private bool UPDT_MODE()
        {
            #region TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK (STATUS_ID)
            if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK, "STATUS_ID") == false)
            {
                strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK, "STATUS_ID", "NUMBER", "", "", "0");
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " SET STATUS_ID = 0 ";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " SET STATUS_ID = 1 WHERE STATUS IN ('YES', 'Y', 'Yes', 'Yes*', 'YES*')";
                dmlService.J_ExecSql(strSQL);
            }
            #endregion
            //
            #region TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK (MODE)
            if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK, "MODE") == false)
            {
                strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK, "MODE", "NUMBER", "", "", "0");
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " SET MODE = 0";
                dmlService.J_ExecSql(strSQL);
            }
            #endregion
            //-- UPDATE BLACK LISTED
            strSQL = "UPDATE MST_DEDUCTEE " +
                "     LEFT JOIN  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " " +
                "     ON    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN = MST_DEDUCTEE.DEDUCTEE_PAN " +
                "     SET   MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_FLAG              = 1, " +
                "           MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strExcelComplianceDate) + cmnService.J_DateOperator() + " " +
                "     WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".STATUS_ID = 1 ";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                return false;
            }
            //-- UPDATE WHITE LISTED
            strSQL = "UPDATE MST_DEDUCTEE " +
                "     LEFT JOIN  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " " +
                "     ON    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".PAN = MST_DEDUCTEE.DEDUCTEE_PAN " +
                "     SET   MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_FLAG              = 0, " +
                "           MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_WHITE_LISTED_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strExcelComplianceDate) + cmnService.J_DateOperator() + " " +
                "     WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + ".STATUS_ID = 0 " +
                "     AND   COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE IS NOT NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                return false;
            }

            //dmlService.J_ExecSql("UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " SET MODE = '" + T_Deductee_Master_Updt_Tag.NEW + "' WHERE MODE IS NULL");
            //dmlService.J_ExecSql("UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " SET MODE = '" + T_Deductee_Master_Updt_Tag.NEW + "' WHERE MODE = ''");
            //
            return true;
            //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_COMPLIANCE_CHECK + " WHERE MODE = ''")) == 0)
            //    return true;
            //else
            //    return false;
        }
        #endregion

        #endregion

    }
}
