
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

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;


#endregion



namespace TDSMAN.FormUtl
{
    public partial class UtlImportfromJSDB : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public UtlImportfromJSDB()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        //
        string strFVUPath = "";
        //-----------------------------------------------------------------------
        string OutputFolder;
        //string Filename;
        string OutputFilePath;
        //----------------------------------------------
        string strFileName;
        //----------------------------------------------

        string strImportErrorMessage = "Import Failed.";
    
        #endregion

        #region User Defined Events

        #region TrnFVUImport_Load

        private void TrnFVUImport_Load(object sender, EventArgs e)
        {
            //-----------
            lblTitle.Text = "Import From Previous Year Database";
            lblNotes1.Text = "This utility will import all the data from the source database to the existing database";

            lnkLabel2.Text = "--";

            //Added by Shrey Kejriwal on 18/03/2012
            BtnSave.Enabled = false;
            BtnSave.BackColor = Color.Silver;

            //
        }

        #endregion

        #region lnkLabel2_LinkClicked
        private void lnkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.tdsman.com/Downloads/eTutorial-TAN-Registration.zip");
        }
        #endregion

        #region btnSelectTDSPath_Click
        private void btnSelectTDSPath_Click(object sender, EventArgs e)
        {
            //strFVUPath = cmnService.J_OpenFileDialog("Import File | *.tds; *.fvu", "Import File | *.tds; *.fvu", "Choose the File to import");
            strFVUPath = cmnService.J_OpenFileDialog("Database File | *.mdb", "Choose the File to import");

            if (strFVUPath != "")
            {
                txtFVUPath.Text = strFVUPath;

                //Added by Shrey Kejriwal on 18/03/2012
                BtnSave.Enabled = true;
                BtnSave.BackColor = Color.Lavender;
            }
            else
            {
                txtFVUPath.Text = "";

                //Added by Shrey Kejriwal on 18/03/2012
                BtnSave.Enabled = false;
                BtnSave.BackColor = Color.Silver;
            }

        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            prgBar.Value = 0;
            //
            if (ValidateFields() == false) return;

            OutputFolder = Path.Combine(Application.StartupPath, "Prev Year Tables");
            strFileName = cmnService.J_GetFileName(strFVUPath);
            OutputFilePath = OutputFolder + "\\" + strFileName;

            //SAVING THE FILE SELECTED IN APPLICATION FOLDER
            cmnService.J_CreateDirectory(OutputFolder);

            #region VALIDATE SOURCE DATABASE

            //ADDED BY INDRAJIT ON 03-04-2012
            string strSourcePath = "";
            strSourcePath = strFVUPath;

            // -------------------------------------------------
            // -- NOW CHECKING IF THE SOURCE DB IS VALID TDSMAN DB
            // -------------------------------------------------

            DMLService dmlServiceSourceDB = new DMLService(J_DatabaseType.MsAccess, J_ConnectionProviderType.OleDb);

            //CHECKING IF THE CONNECTION CAN BE MADE TO THE DB
            if (dmlServiceSourceDB.J_ValidateConnection(strSourcePath, "mother") == false)
            {
                dmlServiceSourceDB.Dispose();
                cmnService.J_UserMessage("Invalid database file selected.");
                return;
            }

            if (dmlServiceSourceDB.J_IsDatabaseObjectExist("MST_ASSESSMENT") == false)
            {
                dmlServiceSourceDB.Dispose();
                cmnService.J_UserMessage("Invalid database file selected.");
                return;
            }
            ////if (cmnService.J_UserMessage("Please note : All your existing database will be over written with the selected database. \nProceed ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            ////    return;
            //--

            ////CHECKING IF THE DB IS THE TDSMAN DB ONLY
            //string strSQL = "SELECT GROUP_NAME FROM MST_GROUP WHERE GROUP_ID = 1";

            //if (Convert.ToString(cmnService.J_NullToText(dmlServiceSourceDB.J_ExecSqlReturnScalar(strSQL))) != "TDS-MAN")
            //{
            //    cmnService.J_UserMessage("Invalid database file selected.");
            //    return;
            //}

            string strDestinationPath = OutputFilePath;
            //END OF ADDITION BY INDRAJIT ON 03-04-2012
            
            //////DISPLYING THE CONFIRMATION MESSAGE BEFORE IMPORTING
            ////if (cmnService.J_UserMessage("TDS File Creation Date : " + strFileCreationDate + "\n Please ensure that you are using the latest file for preparing correction statement.\n Proceed to Import?", MessageBoxButtons.YesNo) == DialogResult.No)
            ////{
            ////    BtnExit.Select();
            ////    return;
            ////}

            #endregion

            prgBar.Value = prgBar.Value + 10;
            this.Refresh();

            dmlService.J_BeginTransaction();

            #region IMPORT TO TEMP TABLES

            //IMPORTING TABLES FROM SOURCE DATABASE
            if (ImportTransactionsFromFVU(txtFVUPath.Text) == false)
            {
                prgBar.Value = 0;
                this.Cursor = Cursors.Default;
                return;
            }

            #endregion

            #region CORRECTING THE ASSESSMENT YEAR ID OF THE DATA TO BE IMPORTED

            strSQL = "SELECT TOP 1 FA_YEAR FROM TEMP_MST_ASSESSMENT ORDER BY ASST_ID";

            //if (dmlService.J_ExecSqlReturnScalar(strSQL).ToString() != T_FinancialYear.F2005_06)
            //{
            //    strSQL = "UPDATE TEMP_MST_ASSESSMENT SET ASST_ID = ASST_ID + 2";
            //    dmlService.J_ExecSql(strSQL);

                strSQL = "UPDATE TEMP_TRN_BASIC_INFO SET ASST_ID = ASST_ID + 2";
                dmlService.J_ExecSql(strSQL);

                //strSQL = "UPDATE TEMP_COR_HDR_BATCH SET ASST_ID = ASST_ID + 2";
                //dmlService.J_ExecSql(strSQL);

            //}

            #endregion

                strSQL = "INSERT INTO MST_COMPANY SELECT * FROM TEMP_MST_COMPANY";
                dmlService.J_ExecSql(strSQL);
                strSQL = "DROP TABLE TEMP_MST_COMPANY";
                dmlService.J_ExecSql(strSQL);

                strSQL = "INSERT INTO MST_DEDUCTEE SELECT * FROM TEMP_MST_DEDUCTEE";
                dmlService.J_ExecSql(strSQL);
                strSQL = "DROP TABLE TEMP_MST_DEDUCTEE";
                dmlService.J_ExecSql(strSQL);

                strSQL = "INSERT INTO MST_EMPLOYEE SELECT * FROM TEMP_MST_EMPLOYEE";
                dmlService.J_ExecSql(strSQL);
                strSQL = "DROP TABLE TEMP_MST_EMPLOYEE";
                dmlService.J_ExecSql(strSQL);

                strSQL = "INSERT INTO TRN_BASIC_INFO SELECT * FROM TEMP_TRN_BASIC_INFO";
                dmlService.J_ExecSql(strSQL);
                strSQL = "DROP TABLE TEMP_TRN_BASIC_INFO";
                dmlService.J_ExecSql(strSQL);


                strSQL = "INSERT INTO TRN_CHALLAN SELECT * FROM TEMP_TRN_CHALLAN";
                dmlService.J_ExecSql(strSQL);
                strSQL = "DROP TABLE TEMP_TRN_CHALLAN";
                dmlService.J_ExecSql(strSQL);


                strSQL = "INSERT INTO TRN_COMPANY_INFO SELECT * FROM TEMP_TRN_COMPANY_INFO";
                dmlService.J_ExecSql(strSQL);
                strSQL = "DROP TABLE TEMP_TRN_COMPANY_INFO";
                dmlService.J_ExecSql(strSQL);

                strSQL = "INSERT INTO TRN_DEDUCTEE_DETAILS SELECT * FROM TEMP_TRN_DEDUCTEE_DETAILS";
                dmlService.J_ExecSql(strSQL);
                strSQL = "DROP TABLE TEMP_TRN_DEDUCTEE_DETAILS";
                dmlService.J_ExecSql(strSQL);

                strSQL = "INSERT INTO TRN_SALARY_DETAILS SELECT * FROM TEMP_TRN_SALARY_DETAILS";
                dmlService.J_ExecSql(strSQL);
                strSQL = "DROP TABLE TEMP_TRN_SALARY_DETAILS";
                dmlService.J_ExecSql(strSQL);

            
            txtFVUPath.Text = "";
            //---------------
            cmnService.J_UserMessage("Import Completed");
            //
            prgBar.Value = 0;
            //            
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                //ADDED BY SHREY KEJRIWAL ON 18/03/2012
                //FILE PATH BLANK CHECK
                if (txtFVUPath.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Select the TDS file that you want to import.");
                    btnSelectTDSPath.Select();
                    return false;
                }


                // FILE EXIST
                if (cmnService.J_IsFileExist(txtFVUPath.Text) == false)
                {
                    cmnService.J_UserMessage("Selected File not found");
                    btnSelectTDSPath.Select();
                    return false;
                }

                // FILE OPEN
                if (cmnService.J_IsProcessOpen(txtFVUPath.Text) == true)
                {
                    cmnService.J_UserMessage("Selected File is open");
                    btnSelectTDSPath.Select();
                    return false;
                }
                return true;
                
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ImportTransactionsFromFVU
        private bool ImportTransactionsFromFVU(string FVUPath)
        {
            try
            {
                //TABLE NAME FOR DATA IMPORT
                string[] strImporttableName = {"MST_COMPANY",
                                               "MST_DEDUCTEE",
                                               "MST_EMPLOYEE",
                                               "TRN_BASIC_INFO",
                                               "TRN_CHALLAN",
                                               "TRN_COMPANY_INFO",
                                               "TRN_DEDUCTEE_DETAILS",
                                               "TRN_SALARY_DETAILS"};

                string[] strDumptableName = {"TEMP_MST_COMPANY",
                                             "TEMP_MST_DEDUCTEE",
                                             "TEMP_MST_EMPLOYEE",
                                             "TEMP_TRN_BASIC_INFO",
                                             "TEMP_TRN_CHALLAN",
                                             "TEMP_TRN_COMPANY_INFO",
                                             "TEMP_TRN_DEDUCTEE_DETAILS",
                                             "TEMP_TRN_SALARY_DETAILS"};


                #region TABLE to TEXT

                cmnService.J_DeleteDirectory(Path.Combine(Application.StartupPath, "Prev Year Tables"));

                //Creation of TEXT file from REMOTE table
                if (T_TextFileFromTable(strImporttableName, true) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }

                #endregion


                prgBar.Value = prgBar.Value + 10;
                this.Refresh();

                for (int i = 0; i <= strDumptableName.Length - 1; i++)
                {
                    File.Copy(OutputFolder + "\\" + strImporttableName[i].ToString() + ".txt", OutputFolder + "\\" + strDumptableName[i].ToString() + ".txt");
                    File.Delete(OutputFolder + "\\" + strImporttableName[i].ToString() + ".txt");
                }

                #region TEXT file Import

                //Creation of TEMP table from .txt file
                if (T_BulkImportFromTextFile(strDumptableName, true) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }
                
                #endregion

                prgBar.Value = prgBar.Value + 10;
                this.Refresh();
            
                //Deleting .TXT file
                for (int i = 0; i <= strDumptableName.Length - 1; i++)
                {
                    File.Delete(OutputFolder + "\\" + strDumptableName[i].ToString() + ".txt");
                }

                return true;
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(strImportErrorMessage);
                return false;
            }
        }

        #endregion

        #region T_TextFileFromTable
        private bool T_TextFileFromTable(string[] tbl, bool FirstRowAsColumnHeader)
        {
            cmnService.J_CreateDirectory(Path.Combine(Application.StartupPath, "Prev Year Tables"));

            //Recreating schema file
            if (File.Exists(OutputFolder + "\\schema.ini") == true)
                File.Delete(OutputFolder + "\\schema.ini");

            StreamWriter StreamWriter = new StreamWriter(OutputFolder + "\\schema.ini");
            for (int i = 0; i <= tbl.Length - 1; i++)
            {
                StreamWriter.WriteLine("[" + tbl[i].ToString() + ".txt]");
                StreamWriter.WriteLine("ColNameHeader=" + (FirstRowAsColumnHeader == true ? "true" : "false") + "");
                StreamWriter.WriteLine("Format=Delimited(^)");
                StreamWriter.WriteLine("TextDelimeter=\"none\"");
                StreamWriter.WriteLine("MaxScanRows=0");
                StreamWriter.WriteLine("CharacterSet=1252");
                StreamWriter.WriteLine("DateTimeFormat=dd/MM/yyyy");
            }
            StreamWriter.Close();

            //OPENING SOURCE DATABASE
            DMLService dmlServiceSourceDB = new DMLService(J_DatabaseType.MsAccess, J_ConnectionProviderType.OleDb);

            //CHECKING IF THE CONNECTION CAN BE MADE TO THE DB
            if (dmlServiceSourceDB.J_ValidateConnection(strFVUPath, "mother") == false)
            {
                dmlServiceSourceDB.Dispose();
                cmnService.J_UserMessage("Invalid database file selected.");
                return false;
            }            
            //QUERY TO IMPORT THE DATA FROM TEXT FILE
            for (int i = 0; i <= tbl.Length - 1; i++)
            {
                strSQL = "SELECT * INTO [Text; DATABASE=" + OutputFolder + "].[" + tbl[i].ToString() + ".txt" + "] " +
                         "FROM [" + tbl[i].ToString() + "] ";

                if (dmlServiceSourceDB.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
            }
            dmlServiceSourceDB.Dispose();
            return true;
        }

        #endregion

        #region T_BulkImportFromTextFile
        private bool T_BulkImportFromTextFile(string[] tbl, bool FirstRowAsColumnHeader)
        {
            //Recreating schema file
            if (File.Exists(OutputFolder + "\\schema.ini") == true)
                File.Delete(OutputFolder + "\\schema.ini");

            StreamWriter StreamWriter = new StreamWriter(OutputFolder + "\\schema.ini");
            for (int i = 0; i <= tbl.Length - 1; i++)
            {
                StreamWriter.WriteLine("[" + tbl[i].ToString() + ".txt]");
                StreamWriter.WriteLine("ColNameHeader=" + (FirstRowAsColumnHeader == true ? "true" : "false") + "");
                StreamWriter.WriteLine("Format=Delimited(^)");
                StreamWriter.WriteLine("MaxScanRows=0");
                StreamWriter.WriteLine("CharacterSet=ANSI");

                #region DEFINING THE COLUMN NAMES

                strSQL = "SELECT TOP 1 * FROM " + cmnService.J_Mid(tbl[i].ToString(), 5, (tbl[i].ToString().Length)-5);
                DataTable datatable = dmlService.J_ExecSqlReturnDataTable(strSQL);

                //----WRITE COLUMN DETAILS 
                int intCounter = 1;
                string strDesc = "";

                foreach (DataColumn column in datatable.Columns)
                {
                    switch (column.DataType.Name)
                    {
                        case "Int16":
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Integer";
                            break;
                        case "Double":
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Double";
                            break;
                        case J_DataTableDataType.Integer:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Integer";
                            break;
                        case J_DataTableDataType.Long:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Integer";
                            break;
                        case J_DataTableDataType.Decimal:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Currency";
                            break;
                        case J_DataTableDataType.String:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Char";
                            break;
                        case J_DataTableDataType.DateTime:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Date";
                            break;
                        case J_DataTableDataType.Byte:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Byte";
                            break;
                        case J_DataTableDataType.Boolean:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Bit";
                            break;
                        case J_DataTableDataType.Currency:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Currency";
                            break;
                        case J_DataTableDataType.Binary:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " OLE";
                            break;
                    }
                    StreamWriter.WriteLine(strDesc);
                    intCounter++;
                }
                datatable.Dispose();
                
                #endregion
            }
            StreamWriter.Close();

            for (int i = 0; i <= tbl.Length - 1; i++)
            {
                //CREATING THE IMPORT TABLE
                if (dmlService.J_IsDatabaseObjectExist(tbl[i].ToString()) == true)
                {
                    strSQL = "DROP TABLE [" + tbl[i].ToString() + "]";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage("Import Failed!");
                        return false;
                    }
                }

                //QUERY TO IMPORT THE DATA FROM TEXT FILE
                strSQL = "SELECT * INTO [" + tbl[i].ToString() + "] FROM " +
                         "[Text; DATABASE=" + OutputFolder + "].[" + tbl[i].ToString() + ".txt" + "]";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
            }
            return true;
        }

        #endregion

        #endregion

    }
}

