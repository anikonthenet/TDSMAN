
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
    public partial class UtlImportfromAccess : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public UtlImportfromAccess()
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
        //
        int intCaratPosition = 0;

        string strFVUPath = "";
        string strCheckCompatibilityMessage = "";
        long lngBatchID = 0;
        //-----------------------------------------------------------------------
        string strUploadType = "";

        string OutputFolder;
        //string Filename;
        string OutputFilePath;

        //Added by INDRAJIT on 22-02-2012 to BULK IMPORT
        //----------------------------------------------
        string SourceFilePath;
        string strFileName;
        string strFolderPath;
        //----------------------------------------------
        string strImporttableName = "";

        string strFileCreationDate = "";
        string strImportErrorMessage = "Import Failed.";
    
        #endregion

        #region User Defined Events

        #region TrnFVUImport_Load

        private void TrnFVUImport_Load(object sender, EventArgs e)
        {
            //-----------
            lblTitle.Text = "Import Data for Correction";
            lblNotes1.Text = "As per the new notification released by the TIN-NSDL it is mandatory to make corrections using consolidated TDS/TCS\nstatement only downloaded from TIN-website.";

            //lnkLabel2.Text = "Click here to download the tutorial on how to get the TDS/TCS file from TIN-NSDL."
            //-- ANIK 2011/09/03
            lnkLabel2.Text = "Click here to download the tutorial on how to get the TDS/TCS.";

            //Added by Shrey Kejriwal on 18/03/2012
            BtnSave.Enabled = false;
            BtnSave.BackColor = Color.Silver;

            //
        }

        #endregion

        #region lnkLabel2_LinkClicked
        private void lnkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://tin-nsdl.com/downloads/eTutorial-TAN%20Registration.zip");
            //-- ANIK 2011/09/03
            System.Diagnostics.Process.Start("http://www.tdsman.com/Downloads/eTutorial-TAN-Registration.zip");
        }
        #endregion

        #region btnSelectTDSPath_Click
        private void btnSelectTDSPath_Click(object sender, EventArgs e)
        {
            //strFVUPath = cmnService.J_OpenFileDialog("Import File | *.tds; *.fvu", "Import File | *.tds; *.fvu", "Choose the File to import");
            strFVUPath = cmnService.J_OpenFileDialog("Import File | *.tds", "Import File | *.tds", "Choose the File to import");

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
            //
            if (ValidateFields() == false) return;

            OutputFolder = Path.Combine(Application.StartupPath, "Correction Input Files");
            strFileName = cmnService.J_GetFileName(strFVUPath);
            OutputFilePath = OutputFolder + "\\" + strFileName;

            //SAVING THE FILE SELECTED IN APPLICATION FOLDER
            cmnService.J_CreateDirectory(OutputFolder);

            File.Copy(strFVUPath, OutputFilePath, true);
            
            // TO CHECK IF THE FILE SELECTED IS CONSOLIDATED FILE ONLY
            if (CheckFVUCompatibility(txtFVUPath.Text) == false)
            {
                cmnService.J_UserMessage(strCheckCompatibilityMessage);
                this.Cursor = Cursors.Default;
                btnSelectTDSPath.Select();
                return;
            }

            //DISPLYING THE CONFIRMATION MESSAGE BEFORE IMPORTING
            if (cmnService.J_UserMessage("TDS File Creation Date : " + strFileCreationDate + "\n Please ensure that you are using the latest file for preparing correction statement.\n Proceed to Import?", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                BtnExit.Select();
                return;
            }

            this.Refresh();
            
            //IMPORTING ALL THE DATA FROM THE TEXT FILE
            if (ImportTransactionsFromFVU(txtFVUPath.Text) == false)
            {
                prgBar.Value = 0;
                this.Cursor = Cursors.Default;
                return;
            }

            //
            prgBar.Value = prgBar.Value + 5;
            this.Refresh();

            //
            for (int i = prgBar.Minimum; i <= prgBar.Maximum; i++)
            {
                prgBar.PerformStep();
            }
            this.Cursor = Cursors.Default;
            //
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

        #region CheckFVUCompatibility
        private bool CheckFVUCompatibility(string FVUPath)
        {
            try
            {
                string strFinancialYear = "";
                string FinancialYear = "";

                TextReader txtRdr = new StreamReader(FVUPath);
                //
                int NumberOfLines = 2;
                string[] ListLines = new string[NumberOfLines];
                //
                intCaratPosition = 0;
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
                            strCheckCompatibilityMessage = "File format is incorrect. Please choose the Consolidated file only.";
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }
                        // ***********************************************************************************
                        
                        //ADDED BY SHREY KEJRIWAL ON 30/12/2011
                        //CHECKING DATE OF CREATION OF OF TDS FILE FOR DISPLAY PURPOSE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strFileCreationDate = ListLines[i].Substring(intCaratPosition + 1);
                        strFileCreationDate = ListLines[i].Substring(intCaratPosition + 1, strFileCreationDate.IndexOf("^"));

                        strFileCreationDate = cmnService.J_Left(strFileCreationDate, 2) + "/" + cmnService.J_Mid(strFileCreationDate, 2, 2) + "/" + cmnService.J_Right(strFileCreationDate, 4);
                        //Formatting the date for display in the confirmation message
                        strFileCreationDate = String.Format("{0:dd MMMM yyyy}", dtService.J_ConvertddMMyyyy(strFileCreationDate));
                    }

                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "BH")
                    {
                        string strOriginalPRN = "";
                        string strPreviousPRN = "";
                        // FORM NO.
                        // TAN

                        //Modified by Shrey Kejriwal on 16/12/2011

                        for (int a = 1; a < 7; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strOriginalPRN = ListLines[i].Substring(intCaratPosition + 1);
                        strOriginalPRN = ListLines[i].Substring(intCaratPosition + 1, strOriginalPRN.Trim().IndexOf("^"));

                        if (strOriginalPRN == "")
                        {
                            strCheckCompatibilityMessage = "Incorrect Format of the File. Please check.";
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }

                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strPreviousPRN = ListLines[i].Substring(intCaratPosition + 1);
                        strPreviousPRN = ListLines[i].Substring(intCaratPosition + 1, strPreviousPRN.Trim().IndexOf("^"));

                        if (strPreviousPRN == "")
                        {
                            strCheckCompatibilityMessage = "Incorrect Format of the File. Please check.";
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }

                        // FINANCIAL YEAR
                        for (int a = 1; a < 9; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strFinancialYear = ListLines[i].Substring(intCaratPosition + 1);
                        strFinancialYear = ListLines[i].Substring(intCaratPosition + 1, strFinancialYear.Trim().IndexOf("^"));

                        // MAX FINANCIAL YEAR CHECK
                        FinancialYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FA_YEAR FROM MST_ASSESSMENT ORDER BY ASST_ID DESC"));
                        FinancialYear = cmnService.J_Left(FinancialYear, 4) + cmnService.J_Right(FinancialYear, 2);
                        if (cmnService.J_ReturnInt64Value(strFinancialYear) > cmnService.J_ReturnInt64Value(FinancialYear))
                        {
                            strCheckCompatibilityMessage = "F.Y. Year [" + strFinancialYear + "] of the imported file is greater than Package's maximum F.Y. Year [" + FinancialYear + "]\nNot Supported.";
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }
                        // MIN FINANCIAL YEAR CHECK
                        FinancialYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FA_YEAR FROM MST_ASSESSMENT ORDER BY ASST_ID"));
                        FinancialYear = cmnService.J_Left(FinancialYear, 4) + cmnService.J_Right(FinancialYear, 2);
                        if (cmnService.J_ReturnInt64Value(strFinancialYear) < cmnService.J_ReturnInt64Value(FinancialYear))
                        {
                            strCheckCompatibilityMessage = "F.Y. Year [" + strFinancialYear + "] of the imported file is less than Package's minimum F.Y. Year [" + FinancialYear + "]\nNot Supported.";
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }

                    }
                }
                txtRdr.Close();
                txtRdr.Dispose();
                return true;
            }
            catch
            {
                strCheckCompatibilityMessage = "Incorrect Format of the File. Please check.";
                return false;
            }
        }
        #endregion

        #region ImportTransactionsFromFVU
        private bool ImportTransactionsFromFVU(string FVUPath)
        {
            try
            {
                #region DECLARATION

                prgBar.Value = 0;
                
                // FH
                //string strFileCreationDate = "";
                string strFileHash = "";

                // BH
                string strFormNo = "";
                string strQTR = "";

                #endregion

                dmlService.J_BeginTransaction();

                #region TDS file Import
                
                //TABLE NAME FOR DATA IMPORT
                strImporttableName = "TEMP_TDS_FILE_DATA";

                //COPY .TDS FILE AS .TXT FILE TO IMPORT DATA TO ACCESS
                File.Copy(strFVUPath, Application.StartupPath + "\\" + strImporttableName + ".txt", true);

                
                //Creation of dump table from .tds file
                if (T_BulkImportFromTextFile(strImporttableName, false) == false) return false;
                
                
                prgBar.Value = prgBar.Value + 10;
            
                //Deleting .TXT file
                File.Delete(Application.StartupPath + "\\" + strImporttableName + ".txt");

                #endregion

                #region Creating Temporary Tables
                if (T_CreateTempTables() == false)
                {
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                #endregion

                #region Insert Data To Temporary Tables

                #region Insert TEMP_FH

                //Insert data into Temp Tables
                strSQL = "INSERT INTO TEMP_FH   " +
                         "SELECT DateSerial(RIGHT(F5,4),MID(F5,3,2),LEFT(F5,2)) AS FILE_CREATION_DATE," +
                         "       F10 AS FILE_LINES_COUNT," +
                         "       F15 AS HASH_VALUE   " +
                         "FROM [" + strImporttableName + "] WHERE F2 = 'FH'";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #region Insert TEMP_BH

                strSQL = "INSERT INTO TEMP_BH   " + 
                         "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Integer , J_SQLColFormat.NullCheck) + "    AS CHALLAN_RECORDS_COUNT, " + 
                         "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.String , J_SQLColFormat.NullCheck) + "     AS FORM_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F8", J_ColumnType.String, J_SQLColFormat.NullCheck) + "      AS ORIGINAL_RRR_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F9", J_ColumnType.String, J_SQLColFormat.NullCheck) + "      AS PREVIOUS_RRR_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F13", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS TAN_NO, " + 
                         "       " + cmnService.J_SQLDBFormat("F14", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS EXPECTED_CHALLAN_RECORD_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F15", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PAN_NO, " +
                         "       " + cmnService.J_SQLDBFormat("LEFT(F16,4) + '-' + RIGHT(F16,2)", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS ASST_YEAR, " +
                         "       " + cmnService.J_SQLDBFormat("LEFT(F17,4) + '-' + RIGHT(F17,2)", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS FA_YEAR, " +
                         "       " + cmnService.J_SQLDBFormat("F18", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS QTR, " +
                         "       " + cmnService.J_SQLDBFormat("F19", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS COMPANY_NAME, " +
                         "       " + cmnService.J_SQLDBFormat("F20", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS BRANCH_DIV, " +
                         "       " + cmnService.J_SQLDBFormat("F21", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ADDRESS1, " +
                         "       " + cmnService.J_SQLDBFormat("F22", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ADDRESS2, " +
                         "       " + cmnService.J_SQLDBFormat("F23", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ADDRESS3, " +
                         "       " + cmnService.J_SQLDBFormat("F24", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ADDRESS4, " +
                         "       " + cmnService.J_SQLDBFormat("F25", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ADDRESS5, " +
                         "       " + cmnService.J_SQLDBFormat("F26", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS STATE_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F27", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PIN_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F28", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMAIL, " +
                         "       " + cmnService.J_SQLDBFormat("F29", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS STD, " +
                         "       " + cmnService.J_SQLDBFormat("F30", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PHONE, " +
                         "       " + cmnService.J_SQLDBFormat("F31", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ADDRESS_CHANGE, " +
                         "       " + cmnService.J_SQLDBFormat("F32", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS CATEGORY_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F33", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PERSON_NAME, " +
                         "       " + cmnService.J_SQLDBFormat("F34", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DESIGNATION, " +
                         "       " + cmnService.J_SQLDBFormat("F35", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS1, " +
                         "       " + cmnService.J_SQLDBFormat("F36", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS2, " +
                         "       " + cmnService.J_SQLDBFormat("F37", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS3, " +
                         "       " + cmnService.J_SQLDBFormat("F38", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS4, " +
                         "       " + cmnService.J_SQLDBFormat("F39", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS5, " +
                         "       " + cmnService.J_SQLDBFormat("F40", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_STATE_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F41", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_PIN_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F42", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_EMAIL, " + 
                         "       " + cmnService.J_SQLDBFormat("F43", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS EXPECTED_SD_RECORD_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F44", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_STD, " +
                         "       " + cmnService.J_SQLDBFormat("F45", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_PHONE, " +
                         "       " + cmnService.J_SQLDBFormat("F46", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS_CHANGE, " + 
                         "       " + cmnService.J_SQLDBFormat("F47", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TOT_CHALLAN_DEPOSIT, " + 
                         "       " + cmnService.J_SQLDBFormat("F48", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_MOBILE, " + 
                         "       " + cmnService.J_SQLDBFormat("F49", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS COUNT_SD_RECORDS, " + 
                         "       " + cmnService.J_SQLDBFormat("F50", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TOT_GROSS_TOT_INCOME, " +
                         "       " + cmnService.J_SQLDBFormat("F54", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS D_STATE_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F55", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PAO_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F56", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DDO_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F57", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS MINISTRY_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F58", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS MINISTRY_OTHER, " +
                         "       " + cmnService.J_SQLDBFormat("F60", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PAO_REG_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F61", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DDO_REG_NO   " +
                         "FROM [" + strImporttableName + "] WHERE F2 = 'BH'";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                IDataReader reader;
                reader = dmlService.J_ExecSqlReturnReader("SELECT FORM_NO,QTR FROM TEMP_BH");
                if (reader == null)
                {
                    reader.Close();
                    reader.Dispose();
                    return false;
                }
                while (reader.Read())
                {
                    strFormNo = Convert.ToString(reader["FORM_NO"]);
                    strQTR = Convert.ToString(reader["QTR"]);
                }
                reader.Close();
                reader.Dispose();

                #endregion

                #region Insert TEMP_CD

                strSQL = "INSERT INTO TEMP_CD   " +
                         "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "     AS SL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.Integer , J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_COUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F8", J_ColumnType.Integer , J_SQLColFormat.NullCheck) + "    AS EXPECTED_DEDUCTEE_RECORD_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F9", J_ColumnType.String , J_SQLColFormat.NullCheck) + "     AS CHALLAN_STATUS, " +
                         "       " + cmnService.J_SQLDBFormat("F12", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS CHALLAN_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F14", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS TRANSFER_VOUCHER_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F16", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS BSR_CODE, " +
                         "           DateSerial(RIGHT(F18,4), MID(F18,3,2),LEFT(F18,2))                                     AS DEPOSIT_DATE," +
                         "       " + cmnService.J_SQLDBFormat("F21", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS SECTION_NAME, " +
                         "       " + cmnService.J_SQLDBFormat("F22", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS TDS, " +
                         "       " + cmnService.J_SQLDBFormat("F23", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS SURCHARGE, " +
                         "       " + cmnService.J_SQLDBFormat("F24", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS EDUCATION_CESS, " +
                         "       " + cmnService.J_SQLDBFormat("F25", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS INTEREST, " +
                         "       " + cmnService.J_SQLDBFormat("F26", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS OTHERS, " +
                         "       " + cmnService.J_SQLDBFormat("F27", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS TOT_TAX, " +
                         "       " + cmnService.J_SQLDBFormat("F29", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS CTRL_TOT_TAX, " +
                         "       " + cmnService.J_SQLDBFormat("F30", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS CTRL_TDS, " +
                         "       " + cmnService.J_SQLDBFormat("F31", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS CTRL_SURCHARGE, " +
                         "       " + cmnService.J_SQLDBFormat("F32", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS CTRL_EDU_CESS, " +
                         "       " + cmnService.J_SQLDBFormat("F33", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS CTRL_TOT, " +
                         "       " + cmnService.J_SQLDBFormat("F34", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS INTEREST_ALLOCATED, " +
                         "       " + cmnService.J_SQLDBFormat("F35", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS OTHERS_ALLOCATED, " +
                         "       " + cmnService.J_SQLDBFormat("F36", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS CHEQUE_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F37", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS BOOK_ENTRY, " +
                         "       " + cmnService.J_SQLDBFormat("F38", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS PENDING_AMOUNT   " +
                         "FROM [" + strImporttableName + "] WHERE F2 = 'CD'";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #region Insert TEMP_DD

                string[,] strDeductedDate = {{"F24 IS NULL" , "F", "F24", "F"},
                                             {"F24 IS NOT NULL " , "F", "DATESERIAL(RIGHT(F24,4),MID(F24,3,2),LEFT(F24,2))", "F"}};

                string[,] strPaymentDate = {{"F23 IS NULL" , "F", "F23", "F"},
                                             {"F23 IS NOT NULL " , "F", "DATESERIAL(RIGHT(F23,4),MID(F23,3,2),LEFT(F23,2))", "F"}};
                
                strSQL = "INSERT INTO TEMP_DD   " +
                         "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "     AS CHALLAN_SL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "     AS SL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F8", J_ColumnType.String , J_SQLColFormat.NullCheck) + "     AS DEDUCTEE_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F10", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_PAN, " +
                         "       " + cmnService.J_SQLDBFormat("F12", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_PAN_REF, " +
                         "       " + cmnService.J_SQLDBFormat("F13", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_NAME, " +
                         "       " + cmnService.J_SQLDBFormat("F14", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS TAX_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F15", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS SURCHARGE_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F16", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS CESS_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F17", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS TOTAL_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F19", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS TAX_DEPOSITED_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F21", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS TOT_VALUE_PURCHASE, " +
                         "       " + cmnService.J_SQLDBFormat("F22", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS PAYMENT_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat(strPaymentDate, J_SQLColFormat.Case_End, J_ElsePart.YES) + "  AS PAYMENT_DATE, " +
                         "       " + cmnService.J_SQLDBFormat(strDeductedDate, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS DEDUCTED_DATE, " +
                         "       " + cmnService.J_SQLDBFormat("F26", J_ColumnType.Double , J_SQLColFormat.NullCheck) + "    AS RATE, " +
                         "       " + cmnService.J_SQLDBFormat("F27", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS GROSSING_UP_INDICATOR, " +
                         "       " + cmnService.J_SQLDBFormat("F28", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS CASH_BOOK_ENTRY, " +
                         "       " + cmnService.J_SQLDBFormat("F30", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS NON_DEDUCTION_FLAG, " +
                         "       " + cmnService.J_SQLDBFormat("F31", J_ColumnType.String , J_SQLColFormat.NullCheck) + "    AS INVALID_PAN, " +
                         "       " + cmnService.J_SQLDBFormat("F32", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS PAN_COUNTER   " +
                         "FROM [" + strImporttableName + "] WHERE F2 = 'DD'";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #endregion

                #region Insert Data To Correction Tables

                #region Insert data to COR_HDR_BATCH

                IDataReader dtrFH;
                dtrFH = dmlService.J_ExecSqlReturnReader("SELECT FILE_CREATION_DATE,HASH_VALUE FROM TEMP_FH");
                if (dtrFH == null)
                {
                    dtrFH.Close();
                    dtrFH.Dispose();
                    return false;
                }
                while (dtrFH.Read())
                {
                    strFileCreationDate = Convert.ToString(dtrFH["FILE_CREATION_DATE"]);
                    strFileHash = Convert.ToString(dtrFH["HASH_VALUE"]);
                }
                dtrFH.Close();
                dtrFH.Dispose();

                
                strSQL = "INSERT INTO COR_HDR_BATCH (ASST_ID," +
                         "                           FORM_NO," +
                         "                           QTR," +
                         "                           IMPORTED_DATE," +
                         "                           PREVIOUS_RRR_NO," +
                         "                           ORIGINAL_RRR_NO," +
                         "                           FILE_DATE," +
                         "                           HASH_VALUE," +
                         "                           TDS_FILE_PATH) " +
                         "SELECT ASST_ID                             AS ASST_ID," +
                         "       FORM_NO                             AS FORM_NO," +
                         "       QTR                                 AS QTR," +
                         "  '" + System.DateTime.Now.ToString() + "' AS IMPORTED_DATE," +
                         "       PREVIOUS_RRR_NO                     AS PREVIOUS_RRR_NO," +
                         "       ORIGINAL_RRR_NO                     AS ORIGINAL_RRR_NO," +
                         "   " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strFileCreationDate) + cmnService.J_DateOperator() + " AS FILE_DATE," +
                         "  '" + strFileHash + "'                    AS HASH_VALUE," +
                         "  '" + OutputFilePath + "'                 AS TDS_FILE_PATH " +
                         "FROM  TEMP_BH,MST_ASSESSMENT " +
                         "WHERE TEMP_BH.ASST_YEAR = MST_ASSESSMENT.ASST_YEAR";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                strSQL = "SELECT MAX(BATCH_HEADER_ID) FROM COR_HDR_BATCH";
                lngBatchID = Convert.ToInt64(dmlService.J_ExecSqlReturnScalar(strSQL));

                #endregion


                #region Insert data to COR_HDR_COMPANY

                string[,] strADDRESS_CHANGE = {{"TEMP_BH.ADDRESS_CHANGE = 'Y'" , "F", "1", "F"},
                                               {"TEMP_BH.ADDRESS_CHANGE = 'N'" , "F", "0", "F"}};
                string[,] strP_ADDRESS_CHANGE = {{"TEMP_BH.P_ADDRESS_CHANGE = 'Y'" , "F", "1", "F"},
                                                 {"TEMP_BH.P_ADDRESS_CHANGE = 'N'" , "F", "0", "F"}};

                strSQL = "INSERT INTO COR_HDR_COMPANY (BATCH_HEADER_ID," +
                         "                             TAN_NO," +
                         "                             PAN_NO," +
                         "                             COMPANY_NAME," +
                         "                             D_CATEGORY_ID," +
                         "                             D_STATE_ID," +
                         "                             BRANCH_DIV," +
                         "                             MINISTRY_ID," +
                         "                             MINISTRY_OTHER," +
                         "                             PAO_CODE," +
                         "                             PAO_REG_NO," +
                         "                             DDO_CODE," +
                         "                             DDO_REG_NO," +
                         "                             ADDRESS1," +
                         "                             ADDRESS2," +
                         "                             ADDRESS3," +
                         "                             ADDRESS4," +
                         "                             ADDRESS5," +
                         "                             STATE_ID," +
                         "                             PIN_CODE," +
                         "                             PHONE," +
                         "                             STD," +
                         "                             EMAIL," +
                         "                             PERSON_NAME," +
                         "                             DESIGNATION," +
                         "                             P_ADDRESS1," +
                         "                             P_ADDRESS2," +
                         "                             P_ADDRESS3," +
                         "                             P_ADDRESS4," +
                         "                             P_ADDRESS5," +
                         "                             P_STATE_ID," +
                         "                             P_PIN_CODE," +
                         "                             P_PHONE," +
                         "                             P_STD," +
                         "                             P_EMAIL," +
                         "                             P_MOBILE," +
                         "                             ADDRESS_CHANGE," +
                         "                             P_ADDRESS_CHANGE," +
                         "                             EXPECTED_CHALLAN_RECORD_NO," +
                         "                             EXPECTED_SD_RECORD_NO) " +
                         "SELECT " + lngBatchID + "         AS BATCH_HEADER_ID," +
                         "         TEMP_BH.TAN_NO           AS TAN_NO," +
                         "         TEMP_BH.PAN_NO           AS PAN_NO," +
                         "         TEMP_BH.COMPANY_NAME     AS COMPANY_NAME," +
                         "     " + cmnService.J_SQLDBFormat("MST_CATEGORY.CATEGORY_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS D_CATEGORY_ID," +
                         "     " + cmnService.J_SQLDBFormat("MST_STATE_D.STATE_ID",J_ColumnType.Integer,J_SQLColFormat.NullCheck) + " AS D_STATE_ID," +
                         "         TEMP_BH.BRANCH_DIV       AS BRANCH_DIV," +
                         "     " + cmnService.J_SQLDBFormat("MST_MINISTRY.MINISTRY_ID",J_ColumnType.Integer,J_SQLColFormat.NullCheck) + " AS MINISTRY_ID," +
                         "         TEMP_BH.MINISTRY_OTHER   AS MINISTRY_OTHER," +
                         "         TEMP_BH.PAO_CODE         AS PAO_CODE," +
                         "         TEMP_BH.PAO_REG_NO       AS PAO_REG_NO," +
                         "         TEMP_BH.DDO_CODE         AS DDO_CODE," +
                         "         TEMP_BH.DDO_REG_NO       AS DDO_REG_NO," +
                         "         TEMP_BH.ADDRESS1         AS ADDRESS1," +
                         "         TEMP_BH.ADDRESS2         AS ADDRESS2," +
                         "         TEMP_BH.ADDRESS3         AS ADDRESS3," +
                         "         TEMP_BH.ADDRESS4         AS ADDRESS4," +
                         "         TEMP_BH.ADDRESS5         AS ADDRESS5," +
                         "     " + cmnService.J_SQLDBFormat("MST_STATE.STATE_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS STATE_ID," +
                         "         TEMP_BH.PIN_CODE         AS PIN_CODE," +
                         "         TEMP_BH.PHONE            AS PHONE," +
                         "         TEMP_BH.STD              AS STD," +
                         "         TEMP_BH.EMAIL            AS EMAIL," +
                         "         TEMP_BH.PERSON_NAME      AS PERSON_NAME," +
                         "         TEMP_BH.DESIGNATION      AS DESIGNATION," +
                         "         TEMP_BH.P_ADDRESS1       AS P_ADDRESS1," +
                         "         TEMP_BH.P_ADDRESS2       AS P_ADDRESS2," +
                         "         TEMP_BH.P_ADDRESS3       AS P_ADDRESS3," +
                         "         TEMP_BH.P_ADDRESS4       AS P_ADDRESS4," +
                         "         TEMP_BH.P_ADDRESS5       AS P_ADDRESS5," +
                         "     " + cmnService.J_SQLDBFormat("MST_STATE_P.STATE_ID",J_ColumnType.Integer,J_SQLColFormat.NullCheck) + " AS P_STATE_ID," +
                         "         TEMP_BH.P_PIN_CODE       AS P_PIN_CODE," +
                         "         TEMP_BH.P_PHONE          AS P_PHONE," +
                         "         TEMP_BH.P_STD            AS P_STD," +
                         "         TEMP_BH.P_EMAIL          AS P_EMAIL," +
                         "         TEMP_BH.P_MOBILE         AS P_MOBILE," +
                         "     " + cmnService.J_SQLDBFormat(strADDRESS_CHANGE, J_SQLColFormat.Case_End) + " AS ADDRESS_CHANGE," +
                         "     " + cmnService.J_SQLDBFormat(strP_ADDRESS_CHANGE, J_SQLColFormat.Case_End) + " AS P_ADDRESS_CHANGE," +
                         "         TEMP_BH.EXPECTED_CHALLAN_RECORD_NO AS EXPECTED_CHALLAN_RECORD_NO," +
                         "         TEMP_BH.EXPECTED_SD_RECORD_NO      AS EXPECTED_SD_RECORD_NO " +
                         "FROM  ((((TEMP_BH LEFT JOIN MST_CATEGORY ON RTRIM(TEMP_BH.CATEGORY_CODE) = MST_CATEGORY.CATEGORY_CODE) " +
                         "LEFT JOIN MST_STATE AS MST_STATE_D ON TEMP_BH.D_STATE_CODE = MST_STATE_D.STATE_CODE) " +
                         "LEFT JOIN MST_STATE AS MST_STATE_P ON TEMP_BH.P_STATE_CODE = MST_STATE_P.STATE_CODE) " +
                         "LEFT JOIN MST_STATE AS MST_STATE   ON TEMP_BH.STATE_CODE   = MST_STATE.STATE_CODE) " +
                         "LEFT JOIN MST_MINISTRY ON TEMP_BH.MINISTRY_CODE = MST_MINISTRY.MINISTRY_CODE";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #region Insert data to COR_TRN_COMPANY
                strSQL = "INSERT INTO COR_TRN_COMPANY (BATCH_HEADER_ID," +
                         "                             HDR_COMPANY_ID," +
                         "                             TAN_NO," +
                         "                             PAN_NO," +
                         "                             COMPANY_NAME," +
                         "                             D_CATEGORY_ID," +
                         "                             D_STATE_ID," +
                         "                             BRANCH_DIV," +
                         "                             MINISTRY_ID," +
                         "                             MINISTRY_OTHER," +
                         "                             PAO_CODE," +
                         "                             PAO_REG_NO," +
                         "                             DDO_CODE," +
                         "                             DDO_REG_NO," +
                         "                             ADDRESS1," +
                         "                             ADDRESS2," +
                         "                             ADDRESS3," +
                         "                             ADDRESS4," +
                         "                             ADDRESS5," +
                         "                             STATE_ID," +
                         "                             PIN_CODE," +
                         "                             PHONE," +
                         "                             STD," +
                         "                             EMAIL," +
                         "                             PERSON_NAME," +
                         "                             DESIGNATION," +
                         "                             P_ADDRESS1," +
                         "                             P_ADDRESS2," +
                         "                             P_ADDRESS3," +
                         "                             P_ADDRESS4," +
                         "                             P_ADDRESS5," +
                         "                             P_STATE_ID," +
                         "                             P_PIN_CODE," +
                         "                             P_PHONE," +
                         "                             P_STD," +
                         "                             P_EMAIL," +
                         "                             P_MOBILE," +
                         "                             ADDRESS_CHANGE," +
                         "                             P_ADDRESS_CHANGE," +
                         "                             EXPECTED_CHALLAN_RECORD_NO," +
                         "                             EXPECTED_SD_RECORD_NO) " +
                         "SELECT  BATCH_HEADER_ID            AS BATCH_HEADER_ID," +
                         "        HDR_COMPANY_ID             AS HDR_COMPANY_ID," +
                         "        TAN_NO                     AS TAN_NO," +
                         "        PAN_NO                     AS PAN_NO," +
                         "        COMPANY_NAME               AS COMPANY_NAME," +
                         "        D_CATEGORY_ID              AS D_CATEGORY_ID," +
                         "        D_STATE_ID                 AS D_STATE_ID," +
                         "        BRANCH_DIV                 AS BRANCH_DIV," +
                         "        MINISTRY_ID                AS MINISTRY_ID," +
                         "        MINISTRY_OTHER             AS MINISTRY_OTHER," +
                         "        PAO_CODE                   AS PAO_CODE," +
                         "        PAO_REG_NO                 AS PAO_REG_NO," +
                         "        DDO_CODE                   AS DDO_CODE," +
                         "        DDO_REG_NO                 AS DDO_REG_NO," +
                         "        ADDRESS1                   AS ADDRESS1," +
                         "        ADDRESS2                   AS ADDRESS2," +
                         "        ADDRESS3                   AS ADDRESS3," +
                         "        ADDRESS4                   AS ADDRESS4," +
                         "        ADDRESS5                   AS ADDRESS5," +
                         "        STATE_ID                   AS STATE_ID," +
                         "        PIN_CODE                   AS PIN_CODE," +
                         "        PHONE                      AS PHONE," +
                         "        STD                        AS STD," +
                         "        EMAIL                      AS EMAIL," +
                         "        PERSON_NAME                AS PERSON_NAME," +
                         "        DESIGNATION                AS DESIGNATION," +
                         "        P_ADDRESS1                 AS P_ADDRESS1," +
                         "        P_ADDRESS2                 AS P_ADDRESS2," +
                         "        P_ADDRESS3                 AS P_ADDRESS3," +
                         "        P_ADDRESS4                 AS P_ADDRESS4," +
                         "        P_ADDRESS5                 AS P_ADDRESS5," +
                         "        P_STATE_ID                 AS P_STATE_ID," +
                         "        P_PIN_CODE                 AS P_PIN_CODE," +
                         "        P_PHONE                    AS P_PHONE," +
                         "        P_STD                      AS P_STD," +
                         "        P_EMAIL                    AS P_EMAIL," +
                         "        P_MOBILE                   AS P_MOBILE," +
                         "        ADDRESS_CHANGE             AS ADDRESS_CHANGE," +
                         "        P_ADDRESS_CHANGE           AS P_ADDRESS_CHANGE," +
                         "        EXPECTED_CHALLAN_RECORD_NO AS EXPECTED_CHALLAN_RECORD_NO," +
                         "        EXPECTED_SD_RECORD_NO      AS EXPECTED_SD_RECORD_NO " +
                         "FROM  COR_HDR_COMPANY " +
                         "WHERE BATCH_HEADER_ID = " + lngBatchID ;

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;
                #endregion


                #region Insert data to COR_HDR_CHALLAN

                string[,] strBOOK_ENTRY = {{"TEMP_CD.BOOK_ENTRY = 'Y'" , "F", "1", "F"},
                                           {"TEMP_CD.BOOK_ENTRY = 'N'" , "F", "0", "F"}};

                strSQL = "INSERT INTO COR_HDR_CHALLAN (BATCH_HEADER_ID," +
                         "                             SL_NO," +
                         "                             EXPECTED_DEDUCTEE_RECORD_NO," +
                         "                             CHALLAN_STATUS," +
                         "                             CHALLAN_NO," +
                         "                             TRANSFER_VOUCHER_NO," +
                         "                             BSR_CODE," +
                         "                             DEPOSIT_DATE," +
                         "                             SECTION_ID," +
                         "                             TDS," +
                         "                             SURCHARGE," +
                         "                             EDUCATION_CESS," +
                         "                             INTEREST," +
                         "                             OTHERS," +
                         "                             TOT_TAX," +
                         "                             CTRL_TOT_TAX," +
                         "                             CTRL_TDS," +
                         "                             CTRL_SURCHARGE," +
                         "                             CTRL_EDU_CESS," +
                         "                             CTRL_TOT," +
                         "                             INTEREST_ALLOCATED," +
                         "                             OTHERS_ALLOCATED," +
                         "                             CHEQUE_NO," +
                         "                             BOOK_ENTRY," +
                         "                             IMPORT_FLAG," +
                         "                             PENDING_AMOUNT) " +
                         "SELECT " + lngBatchID + "                    AS BATCH_HEADER_ID," +
                         "         TEMP_CD.SL_NO                       AS SL_NO," +
                         "         TEMP_CD.EXPECTED_DEDUCTEE_RECORD_NO AS EXPECTED_DEDUCTEE_RECORD_NO," +
                         "         TEMP_CD.CHALLAN_STATUS              AS CHALLAN_STATUS," +
                         "         TEMP_CD.CHALLAN_NO                  AS CHALLAN_NO," +
                         "         TEMP_CD.TRANSFER_VOUCHER_NO         AS TRANSFER_VOUCHER_NO," +
                         "         TEMP_CD.BSR_CODE                    AS BSR_CODE," +
                         "         TEMP_CD.DEPOSIT_DATE                AS DEPOSIT_DATE," +
                         "         MST_SECTION.SECTION_ID              AS SECTION_ID," +
                         "         TEMP_CD.TDS                         AS TDS," +
                         "         TEMP_CD.SURCHARGE                   AS SURCHARGE," +
                         "         TEMP_CD.EDUCATION_CESS              AS EDUCATION_CESS," +
                         "         TEMP_CD.INTEREST                    AS INTEREST," +
                         "         TEMP_CD.OTHERS                      AS OTHERS," +
                         "         TEMP_CD.TOT_TAX                     AS TOT_TAX," +
                         "         TEMP_CD.CTRL_TOT_TAX                AS CTRL_TOT_TAX," +
                         "         TEMP_CD.CTRL_TDS                    AS CTRL_TDS," +
                         "         TEMP_CD.CTRL_SURCHARGE              AS CTRL_SURCHARGE," +
                         "         TEMP_CD.CTRL_EDU_CESS               AS CTRL_EDU_CESS," +
                         "         TEMP_CD.CTRL_TOT                    AS CTRL_TOT," +
                         "         TEMP_CD.INTEREST_ALLOCATED          AS INTEREST_ALLOCATED," +
                         "         TEMP_CD.OTHERS_ALLOCATED            AS OTHERS_ALLOCATED," +
                         "         TEMP_CD.CHEQUE_NO                   AS CHEQUE_NO," +
                         "     " + cmnService.J_SQLDBFormat(strBOOK_ENTRY, J_SQLColFormat.Case_End) + " AS BOOK_ENTRY," +
                         "         1                                   AS IMPORT_FLAG," +
                         "         TEMP_CD.PENDING_AMOUNT              AS PENDING_AMOUNT " +
                         "FROM TEMP_CD " +
                         "LEFT JOIN MST_SECTION " +
                         "ON RTRIM(TEMP_CD.SECTION_NAME) = MST_SECTION.SECTION_NAME " +
                         "ORDER BY SL_NO";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;
                #endregion

                #region Insert data to COR_TRN_CHALLAN

                strSQL = "INSERT INTO COR_TRN_CHALLAN (BATCH_HEADER_ID," +
                         "                             HDR_CHALLAN_ID," +
                         "                             SL_NO," +
                         "                             EXPECTED_DEDUCTEE_RECORD_NO," +
                         "                             CHALLAN_STATUS," +
                         "                             CHALLAN_NO," +
                         "                             TRANSFER_VOUCHER_NO," +
                         "                             BSR_CODE," +
                         "                             DEPOSIT_DATE," +
                         "                             SECTION_ID," +
                         "                             TDS," +
                         "                             SURCHARGE," +
                         "                             EDUCATION_CESS," +
                         "                             INTEREST," +
                         "                             OTHERS," +
                         "                             TOT_TAX," +
                         "                             CTRL_TOT_TAX," +
                         "                             CTRL_TDS," +
                         "                             CTRL_SURCHARGE," +
                         "                             CTRL_EDU_CESS," +
                         "                             CTRL_TOT," +
                         "                             INTEREST_ALLOCATED," +
                         "                             OTHERS_ALLOCATED," +
                         "                             CHEQUE_NO," +
                         "                             BOOK_ENTRY," +
                         "                             IMPORT_FLAG," +
                         "                             PENDING_AMOUNT) " +
                         "SELECT  BATCH_HEADER_ID             AS BATCH_HEADER_ID," +
                         "        HDR_CHALLAN_ID              AS HDR_CHALLAN_ID," +
                         "        SL_NO                       AS SL_NO," +
                         "        EXPECTED_DEDUCTEE_RECORD_NO AS EXPECTED_DEDUCTEE_RECORD_NO," +
                         "        CHALLAN_STATUS              AS CHALLAN_STATUS," +
                         "        CHALLAN_NO                  AS CHALLAN_NO," +
                         "        TRANSFER_VOUCHER_NO         AS TRANSFER_VOUCHER_NO," +
                         "        BSR_CODE                    AS BSR_CODE," +
                         "        DEPOSIT_DATE                AS DEPOSIT_DATE," +
                         "        SECTION_ID                  AS SECTION_ID," +
                         "        TDS                         AS TDS," +
                         "        SURCHARGE                   AS SURCHARGE," +
                         "        EDUCATION_CESS              AS EDUCATION_CESS," +
                         "        INTEREST                    AS INTEREST," +
                         "        OTHERS                      AS OTHERS," +
                         "        TOT_TAX                     AS TOT_TAX," +
                         "        CTRL_TOT_TAX                AS CTRL_TOT_TAX," +
                         "        CTRL_TDS                    AS CTRL_TDS," +
                         "        CTRL_SURCHARGE              AS CTRL_SURCHARGE," +
                         "        CTRL_EDU_CESS               AS CTRL_EDU_CESS," +
                         "        CTRL_TOT                    AS CTRL_TOT," +
                         "        INTEREST_ALLOCATED          AS INTEREST_ALLOCATED," +
                         "        OTHERS_ALLOCATED            AS OTHERS_ALLOCATED," +
                         "        CHEQUE_NO                   AS CHEQUE_NO," +
                         "        BOOK_ENTRY                  AS BOOK_ENTRY," +
                         "        IMPORT_FLAG                 AS IMPORT_FLAG," +
                         "        PENDING_AMOUNT              AS PENDING_AMOUNT " +
                         "FROM COR_HDR_CHALLAN " +
                         "WHERE BATCH_HEADER_ID = " + lngBatchID + " " +
                         "ORDER BY SL_NO";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;
                #endregion


                #region Insert data to COR_HDR_DEDUCTEE_DETAILS

                string[,] strGROSSING_UP_INDICATOR = {{"TEMP_DD.GROSSING_UP_INDICATOR = 'Y'" , "F", "1", "F"},
                                                      {"TEMP_DD.GROSSING_UP_INDICATOR = 'N'" , "F", "0", "F"}};
                string[,] strCASH_BOOK_ENTRY       = {{"TEMP_DD.CASH_BOOK_ENTRY = 'Y'" , "F", "1", "F"},
                                                      {"TEMP_DD.CASH_BOOK_ENTRY = 'N'" , "F", "0", "F"}};
                string[,] strINVALID_PAN           = {{"TEMP_DD.INVALID_PAN = 'N'" , "F", "1", "F"},
                                                      {"TEMP_DD.INVALID_PAN = 'Y'" , "F", "0", "F"}};

                strSQL = "INSERT INTO COR_HDR_DEDUCTEE_DETAILS (HDR_CHALLAN_ID," +
                         "                                      BATCH_HEADER_ID," +
                         "                                      SL_NO," +
                         "                                      DEDUCTEE_CODE," +
                         "                                      DEDUCTEE_PAN," +
                         "                                      DEDUCTEE_PAN_REF," +
                         "                                      DEDUCTEE_NAME," +
                         "                                      TAX_AMOUNT," +
                         "                                      SURCHARGE_AMOUNT," +
                         "                                      CESS_AMOUNT," +
                         "                                      TOTAL_AMOUNT," +
                         "                                      TAX_DEPOSITED_AMOUNT," +
                         "                                      TOT_VALUE_PURCHASE," +
                         "                                      PAYMENT_AMOUNT," +
                         "                                      PAYMENT_DATE," +
                         "                                      DEDUCTED_DATE," +
                         "                                      RATE," +
                         "                                      GROSSING_UP_INDICATOR," +
                         "                                      CASH_BOOK_ENTRY," +
                         "                                      NON_DEDUCTION_FLAG," +
                         "                                      INVALID_PAN," +
                         "                                      IMPORT_FLAG," +
                         "                                      PAN_COUNTER) " +
                         "SELECT CHALLAN_SL_NO          AS HDR_CHALLAN_ID," +
                         "    " + lngBatchID + "        AS BATCH_HEADER_ID," +
                         "        SL_NO                 AS SL_NO," +
                         "        DEDUCTEE_CODE         AS DEDUCTEE_CODE," +
                         "        DEDUCTEE_PAN          AS DEDUCTEE_PAN," +
                         "        DEDUCTEE_PAN_REF      AS DEDUCTEE_PAN_REF," +
                         "        DEDUCTEE_NAME         AS DEDUCTEE_NAME," +
                         "        TAX_AMOUNT            AS TAX_AMOUNT," +
                         "        SURCHARGE_AMOUNT      AS SURCHARGE_AMOUNT," +
                         "        CESS_AMOUNT           AS CESS_AMOUNT," +
                         "        TOTAL_AMOUNT          AS TOTAL_AMOUNT," +
                         "        TAX_DEPOSITED_AMOUNT  AS TAX_DEPOSITED_AMOUNT," +
                         "        TOT_VALUE_PURCHASE    AS TOT_VALUE_PURCHASE," +
                         "        PAYMENT_AMOUNT        AS PAYMENT_AMOUNT," +
                         "        PAYMENT_DATE          AS PAYMENT_DATE," +
                         "        DEDUCTED_DATE         AS DEDUCTED_DATE," +
                         "        RATE                  AS RATE," +
                         "    " + cmnService.J_SQLDBFormat(strGROSSING_UP_INDICATOR, J_SQLColFormat.Case_End) + " AS GROSSING_UP_INDICATOR," +
                         "    " + cmnService.J_SQLDBFormat(strCASH_BOOK_ENTRY, J_SQLColFormat.Case_End) + " AS CASH_BOOK_ENTRY," +
                         "        NON_DEDUCTION_FLAG    AS NON_DEDUCTION_FLAG," +
                         "    " + cmnService.J_SQLDBFormat(strINVALID_PAN, J_SQLColFormat.Case_End) + " AS INVALID_PAN," +
                         "        1                     AS IMPORT_FLAG," +
                         "        PAN_COUNTER           AS PAN_COUNTER " +
                         "FROM TEMP_DD " +
                         "ORDER BY SL_NO";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }

                strSQL = "UPDATE COR_HDR_DEDUCTEE_DETAILS " +
                         "INNER JOIN COR_HDR_CHALLAN " +
                         "ON COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID   = COR_HDR_CHALLAN.BATCH_HEADER_ID " +
                         "SET COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID   = COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                         "WHERE COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID = COR_HDR_CHALLAN.SL_NO " +
                         "AND COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = " + lngBatchID + "";


                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #region Insert data to COR_TRN_DEDUCTEE_DETAILS

                strSQL = "INSERT INTO COR_TRN_DEDUCTEE_DETAILS (TRN_CHALLAN_ID," +
                         "                                      HDR_DEDUCTEE_DETAIL_ID," +
                         "                                      BATCH_HEADER_ID," +
                         "                                      SL_NO," +
                         "                                      DEDUCTEE_CODE," +
                         "                                      DEDUCTEE_PAN," +
                         "                                      DEDUCTEE_NAME," +
                         "                                      TAX_AMOUNT," +
                         "                                      SURCHARGE_AMOUNT," +
                         "                                      CESS_AMOUNT," +
                         "                                      TOTAL_AMOUNT," +
                         "                                      TAX_DEPOSITED_AMOUNT," +
                         "                                      TOT_VALUE_PURCHASE," +
                         "                                      PAYMENT_AMOUNT," +
                         "                                      PAYMENT_DATE," +
                         "                                      DEDUCTED_DATE," +
                         "                                      RATE," +
                         "                                      GROSSING_UP_INDICATOR," +
                         "                                      CASH_BOOK_ENTRY," +
                         "                                      IMPORT_FLAG," +
                         "                                      NON_DEDUCTION_FLAG) " +
                         "SELECT  HDR_CHALLAN_ID         AS TRN_CHALLAN_ID," +
                         "        HDR_DEDUCTEE_DETAIL_ID AS HDR_DEDUCTEE_DETAIL_ID," +
                         "        BATCH_HEADER_ID        AS BATCH_HEADER_ID," +
                         "        SL_NO                  AS SL_NO," +
                         "        DEDUCTEE_CODE          AS DEDUCTEE_CODE," +
                         "        DEDUCTEE_PAN           AS DEDUCTEE_PAN," +
                         "        DEDUCTEE_NAME          AS DEDUCTEE_NAME," +
                         "        TAX_AMOUNT             AS TAX_AMOUNT," +
                         "        SURCHARGE_AMOUNT       AS SURCHARGE_AMOUNT," +
                         "        CESS_AMOUNT            AS CESS_AMOUNT," +
                         "        TOTAL_AMOUNT           AS TOTAL_AMOUNT," +
                         "        TAX_DEPOSITED_AMOUNT   AS TAX_DEPOSITED_AMOUNT," +
                         "        TOT_VALUE_PURCHASE     AS TOT_VALUE_PURCHASE," +
                         "        PAYMENT_AMOUNT         AS PAYMENT_AMOUNT," +
                         "        PAYMENT_DATE           AS PAYMENT_DATE," +
                         "        DEDUCTED_DATE          AS DEDUCTED_DATE," +
                         "        RATE                   AS RATE," +
                         "        GROSSING_UP_INDICATOR  AS GROSSING_UP_INDICATOR," +
                         "        CASH_BOOK_ENTRY        AS CASH_BOOK_ENTRY," +
                         "        IMPORT_FLAG            AS IMPORT_FLAG," +
                         "        NON_DEDUCTION_FLAG     AS NON_DEDUCTION_FLAG " +
                         "FROM COR_HDR_DEDUCTEE_DETAILS " +
                         "WHERE BATCH_HEADER_ID = " + lngBatchID + " " +
                         "ORDER BY SL_NO";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }

                strSQL = "UPDATE COR_TRN_DEDUCTEE_DETAILS " +
                         "INNER JOIN COR_TRN_CHALLAN " +
                         "ON COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID     = COR_TRN_CHALLAN.HDR_CHALLAN_ID " +
                         "SET COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID    = COR_TRN_CHALLAN.TRN_CHALLAN_ID " +
                         "WHERE COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBatchID + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #endregion

                #region For Salary Details

                if (strFormNo == T_FormNo.F24Q && strQTR == T_Qtr.Q4)
                {
                    #region Insert TEMP_SD
                    strSQL = "INSERT INTO TEMP_SD " +
                             "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SL_NO, " +
                             "       " + cmnService.J_SQLDBFormat("F7", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMPLOYEE_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F9", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMPLOYEE_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F10", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS EMPLOYEE_CATEGORY, " +
                             "           DateSerial(RIGHT(F11,4),MID(F11,3,2),LEFT(F11,2))                                     AS FROM_DATE, " +
                             "           DateSerial(RIGHT(F12,4),MID(F12,3,2),LEFT(F12,2))                                     AS TO_DATE, " +
                             "       " + cmnService.J_SQLDBFormat("F13", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TS_BALANCE, " +
                             "       " + cmnService.J_SQLDBFormat("F16", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS US_16_AGGREGATE, " +
                             "       " + cmnService.J_SQLDBFormat("F17", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS INCOME_CHARGEABLE, " +
                             "       " + cmnService.J_SQLDBFormat("F18", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS AIS_TOTAL, " +
                             "       " + cmnService.J_SQLDBFormat("F19", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS GROSS_TOTAL_INCOME, " +
                             "       " + cmnService.J_SQLDBFormat("F22", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CVIA_DED_TOTAL, " +
                             "       " + cmnService.J_SQLDBFormat("F23", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TOTAL_INCOME, " +
                             "       " + cmnService.J_SQLDBFormat("F24", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TAX_TOTAL_INCOME, " +
                             "       " + cmnService.J_SQLDBFormat("F25", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SCHG_TOTAL_INCOME, " +
                             "       " + cmnService.J_SQLDBFormat("F26", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS ECESS_TOTAL_INCOME, " +
                             "       " + cmnService.J_SQLDBFormat("F27", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS US_89_LESS, " +
                             "       " + cmnService.J_SQLDBFormat("F28", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TAX_PAYABLE, " +
                             "       " + cmnService.J_SQLDBFormat("F29", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TOTAL_TDS_DEDUCTED, " +
                             "       " + cmnService.J_SQLDBFormat("F30", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SHORTFALL_TAX, " +
                             "       " + cmnService.J_SQLDBFormat("F31", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS INVALID_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F32", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS PAN_COUNTER " +
                             "FROM [" + strImporttableName + "] WHERE F2 = 'SD'";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    #endregion

                    #region Insert TEMP_S16
                    strSQL = "INSERT INTO TEMP_S16 " +
                             "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SD_SL_NO, " +
                             "       " + cmnService.J_SQLDBFormat("F6", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS SECTION_ID, " +
                             "       " + cmnService.J_SQLDBFormat("F7", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TOTAL_AMOUNT " +
                             "FROM [" + strImporttableName + "] WHERE F2 = 'S16'";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    #endregion

                    #region Insert TEMP_C6A
                    strSQL = "INSERT INTO TEMP_C6A " +
                             "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SD_SL_NO, " +
                             "       " + cmnService.J_SQLDBFormat("F6", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS SECTION_ID, " +
                             "       " + cmnService.J_SQLDBFormat("F7", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TOTAL_AMOUNT " +
                             "FROM [" + strImporttableName + "] WHERE F2 = 'C6A'";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    #endregion

                    #region Insert data to COR_HDR_SALARY_DETAILS

                    string[,] strINVALID_PAN_1 = {{"TEMP_SD.INVALID_PAN = 'N'" , "F", "1", "F"},
                                                  {"TEMP_SD.INVALID_PAN = 'Y'" , "F", "0", "F"}};

                    strSQL = "INSERT INTO COR_HDR_SALARY_DETAILS (BATCH_HEADER_ID," +
                             "                                    SL_NO," +
                             "                                    EMPLOYEE_NAME," +
                             "                                    EMPLOYEE_PAN," +
                             "                                    EMPLOYEE_CATEGORY," +
                             "                                    FROM_DATE," +
                             "                                    TO_DATE," +
                             "                                    TS_BALANCE," +
                             "                                    US_16_AGGREGATE," +
                             "                                    INCOME_CHARGEABLE," +
                             "                                    AIS_Total," +
                             "                                    GROSS_TOTAL_INCOME," +
                             "                                    CVIA_DED_TOTAL," +
                             "                                    TOTAL_INCOME," +
                             "                                    TAX_TOTAL_INCOME," +
                             "                                    SCHG_TOTAL_INCOME," +
                             "                                    ECESS_TOTAL_INCOME," +
                             "                                    TAX_PAYABLE_AGGREGATE," +
                             "                                    US_89_LESS," +
                             "                                    TAX_PAYABLE," +
                             "                                    TOTAL_TDS_DEDUCTED," +
                             "                                    SHORTFALL_TAX," +
                             "                                    IMPORT_FLAG," +
                             "                                    INVALID_PAN," +
                             "                                    PAN_COUNTER," +
                             "                                    CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCF_DED_AMOUNT," +
                             "                                    CVIA_OTH_DED_TOTAL," +
                             "                                    US_16_EA," +
                             "                                    US_16_TE) " +
                             "SELECT " + lngBatchID +  "     AS BATCH_HEADER_ID," +
                             "         TEMP_SD.SL_NO         AS SL_NO," +
                             "         EMPLOYEE_NAME         AS EMPLOYEE_NAME," +
                             "         EMPLOYEE_PAN          AS EMPLOYEE_PAN," +
                             "         EMPLOYEE_CATEGORY     AS EMPLOYEE_CATEGORY," +
                             "         FROM_DATE             AS FROM_DATE," +
                             "         TO_DATE               AS TO_DATE," +
                             "         TS_BALANCE            AS TS_BALANCE," +
                             "         US_16_AGGREGATE       AS US_16_AGGREGATE," +
                             "         INCOME_CHARGEABLE     AS INCOME_CHARGEABLE," +
                             "         AIS_Total             AS AIS_Total," +
                             "         GROSS_TOTAL_INCOME    AS GROSS_TOTAL_INCOME," +
                             "         CVIA_DED_TOTAL        AS CVIA_DED_TOTAL," +
                             "         TOTAL_INCOME          AS TOTAL_INCOME," +
                             "         TAX_TOTAL_INCOME      AS TAX_TOTAL_INCOME," +
                             "         SCHG_TOTAL_INCOME     AS SCHG_TOTAL_INCOME," +
                             "         ECESS_TOTAL_INCOME    AS ECESS_TOTAL_INCOME," +
                             "        (TAX_TOTAL_INCOME + SCHG_TOTAL_INCOME + ECESS_TOTAL_INCOME) AS TAX_PAYABLE_AGGREGATE," +
                             "         US_89_LESS            AS US_89_LESS," +
                             "         TAX_PAYABLE           AS TAX_PAYABLE," +
                             "         TOTAL_TDS_DEDUCTED    AS TOTAL_TDS_DEDUCTED," +
                             "         SHORTFALL_TAX         AS SHORTFALL_TAX," +
                             "         1                     AS IMPORT_FLAG," +
                             "       " + cmnService.J_SQLDBFormat(strINVALID_PAN_1, J_SQLColFormat.Case_End) + " AS INVALID_PAN," +
                             "         PAN_COUNTER           AS PAN_COUNTER," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCE.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCF.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS CVIA_SEC80CCF_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_OTHERS.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CVIA_OTH_DED_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_16ii.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS US_16_EA," +
                             "       " + cmnService.J_SQLDBFormat("F_16iii.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS US_16_TE " +
                             "FROM (((((TEMP_SD " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM TEMP_C6A " +
                             "WHERE SECTION_ID = '80CCE') AS F_80CCE " +
                             "ON TEMP_SD.SL_NO = F_80CCE.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM TEMP_C6A " +
                             "WHERE SECTION_ID = '80CCF') AS F_80CCF " +
                             "ON TEMP_SD.SL_NO = F_80CCF.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM TEMP_C6A " +
                             "WHERE SECTION_ID = 'OTHERS') AS F_OTHERS " +
                             "ON TEMP_SD.SL_NO = F_OTHERS.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM TEMP_S16 " +
                             "WHERE SECTION_ID = '16(ii)') AS F_16ii " +
                             "ON TEMP_SD.SL_NO = F_16ii.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM TEMP_S16 " +
                             "WHERE SECTION_ID = '16(iii)') AS F_16iii " +
                             "ON TEMP_SD.SL_NO = F_16iii.SD_SL_NO)";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    #endregion

                    #region Insert data to COR_TRN_SALARY_DETAILS

                    strSQL = "INSERT INTO COR_TRN_SALARY_DETAILS (BATCH_HEADER_ID," +
                             "                                    HDR_SALARY_DETAILS_ID," +
                             "                                    SL_NO," +
                             "                                    EMPLOYEE_NAME," +
                             "                                    EMPLOYEE_PAN," +
                             "                                    EMPLOYEE_CATEGORY," +
                             "                                    FROM_DATE," +
                             "                                    TO_DATE," +
                             "                                    TS_BALANCE," +
                             "                                    US_16_EA," +
                             "                                    US_16_TE," +
                             "                                    US_16_AGGREGATE," +
                             "                                    INCOME_CHARGEABLE," +
                             "                                    AIS_Total," +
                             "                                    GROSS_TOTAL_INCOME," +
                             "                                    CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                             "                                    CVIA_OTH_DED_TOTAL," +
                             "                                    CVIA_DED_TOTAL," +
                             "                                    TOTAL_INCOME," +
                             "                                    TAX_TOTAL_INCOME," +
                             "                                    SCHG_TOTAL_INCOME," +
                             "                                    ECESS_TOTAL_INCOME," +
                             "                                    TAX_PAYABLE_AGGREGATE," +
                             "                                    US_89_LESS," +
                             "                                    TAX_PAYABLE," +
                             "                                    TOTAL_TDS_DEDUCTED," +
                             "                                    SHORTFALL_TAX," +
                             "                                    IMPORT_FLAG," +
                             "                                    CVIA_SEC80CCF_DED_AMOUNT) " +
                             "SELECT  BATCH_HEADER_ID                AS BATCH_HEADER_ID," +
                             "        HDR_SALARY_DETAILS_ID          AS HDR_SALARY_DETAILS_ID," +
                             "        SL_NO                          AS SL_NO," +
                             "        EMPLOYEE_NAME                  AS EMPLOYEE_NAME," +
                             "        EMPLOYEE_PAN                   AS EMPLOYEE_PAN," +
                             "        EMPLOYEE_CATEGORY              AS EMPLOYEE_CATEGORY," +
                             "        FROM_DATE                      AS FROM_DATE," +
                             "        TO_DATE                        AS TO_DATE," +
                             "        TS_BALANCE                     AS TS_BALANCE," +
                             "        US_16_EA                       AS US_16_EA," +
                             "        US_16_TE                       AS US_16_TE," +
                             "        US_16_AGGREGATE                AS US_16_AGGREGATE," +
                             "        INCOME_CHARGEABLE              AS INCOME_CHARGEABLE," +
                             "        AIS_Total                      AS AIS_Total," +
                             "        GROSS_TOTAL_INCOME             AS GROSS_TOTAL_INCOME," +
                             "        CVIA_SEC80CCE_TOTAL_DED_AMOUNT AS CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                             "        CVIA_OTH_DED_TOTAL             AS CVIA_OTH_DED_TOTAL," +
                             "        CVIA_DED_TOTAL                 AS CVIA_DED_TOTAL," +
                             "        TOTAL_INCOME                   AS TOTAL_INCOME," +
                             "        TAX_TOTAL_INCOME               AS TAX_TOTAL_INCOME," +
                             "        SCHG_TOTAL_INCOME              AS SCHG_TOTAL_INCOME," +
                             "        ECESS_TOTAL_INCOME             AS ECESS_TOTAL_INCOME," +
                             "        TAX_PAYABLE_AGGREGATE          AS TAX_PAYABLE_AGGREGATE," +
                             "        US_89_LESS                     AS US_89_LESS," +
                             "        TAX_PAYABLE                    AS TAX_PAYABLE," +
                             "        TOTAL_TDS_DEDUCTED             AS TOTAL_TDS_DEDUCTED," +
                             "        SHORTFALL_TAX                  AS SHORTFALL_TAX," +
                             "        IMPORT_FLAG                    AS IMPORT_FLAG," +
                             "        CVIA_SEC80CCF_DED_AMOUNT       AS CVIA_SEC80CCF_DED_AMOUNT " +
                             "FROM COR_HDR_SALARY_DETAILS " +
                             "WHERE BATCH_HEADER_ID = " + lngBatchID + " " +
                             "ORDER BY SL_NO";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    #endregion
                }

                #endregion

                #region VERIFYING RECORD COUNTS

                strSQL = "SELECT COUNT(*) AS CHALLAN_MATCHED " +
                         "FROM TEMP_BH, " +
                         "    (SELECT COUNT(COR_HDR_CHALLAN.HDR_CHALLAN_ID) AS CHALLAN_IMPORTED " +
                         "     FROM COR_HDR_CHALLAN " +
                         "     WHERE BATCH_HEADER_ID = " + lngBatchID + " ) AS CHALLAN_CNT " +
                         "WHERE TEMP_BH.CHALLAN_RECORDS_COUNT = CHALLAN_CNT.CHALLAN_IMPORTED";

                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) == 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }

                strSQL = "SELECT COUNT(*) AS DEDUCTEE_NOT_MATCHED " +
                         "FROM ((TEMP_CD " +
                         "INNER JOIN COR_HDR_CHALLAN " +
                         "ON TEMP_CD.SL_NO = COR_HDR_CHALLAN.SL_NO) " +
                         "LEFT JOIN " +
                         "      (SELECT HDR_CHALLAN_ID, " +
                         "              COUNT(HDR_DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_IMPORTED " +
                         "       FROM   COR_HDR_DEDUCTEE_DETAILS " +
                         "       GROUP BY HDR_CHALLAN_ID) AS DEDUCTEE " +
                         "ON COR_HDR_CHALLAN.HDR_CHALLAN_ID = DEDUCTEE.HDR_CHALLAN_ID) " +
                         "WHERE  TEMP_CD.DEDUCTEE_COUNT <> " + cmnService.J_SQLDBFormat("DEDUCTEE.COUNT_DEDUCTEE_IMPORTED", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " " +
                         "AND COR_HDR_CHALLAN.BATCH_HEADER_ID = " + lngBatchID;

                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #region DROPING ALL THE TEMPORARY TABLES CREATED

                if (dmlService.J_IsDatabaseObjectExist(strImporttableName) == true)
                {
                    strSQL = "DROP TABLE " + strImporttableName;

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }

                if (dmlService.J_IsDatabaseObjectExist("TEMP_BH") == true)
                {
                    strSQL = "DROP TABLE TEMP_BH";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("TEMP_FH") == true)
                {
                    strSQL = "DROP TABLE TEMP_FH";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("TEMP_CD") == true)
                {
                    strSQL = "DROP TABLE TEMP_CD";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("TEMP_DD") == true)
                {
                    strSQL = "DROP TABLE TEMP_DD";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("TEMP_SD") == true)
                {
                    strSQL = "DROP TABLE TEMP_SD";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("TEMP_S16") == true)
                {
                    strSQL = "DROP TABLE TEMP_S16";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }

                if (dmlService.J_IsDatabaseObjectExist("TEMP_C6A") == true)
                {
                    strSQL = "DROP TABLE TEMP_C6A";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                #endregion


                dmlService.J_Commit();


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

        #region T_ReturnBatchId
        public long T_ReturnBatchId()
        {
            strSQL = "SELECT  BATCH_HEADER_ID " +
                    " FROM    COR_HDR_BATCH " +
                    " ORDER BY BATCH_HEADER_ID DESC";
            //" WHERE   ASST_ID    =  " + FYID + " " +
            //" AND     QTR        = '" + Qtr + "'" +
            //" AND     COMPANY_ID =  " + CompanyID + " " +
            //" AND     FORM_NO    = '" + cmnService.J_ReplaceQuote(FormNo) + "'";

            return cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
        }
        #endregion

        #region T_GenerateBatchId
        public bool T_GenerateBatchId(IDbCommand command, long FYID, string Qtr, string FormNo,
                                      string ImportDate, string RPUName, string FileCreationDate)
        {
            strSQL = "INSERT INTO COR_HDR_BATCH (ASST_ID," +
                "                  QTR," +
                "                  FORM_NO," +
                "                  IMPORTED_DATE," +
                "                  RPU_NAME," +
                "                  FILE_DATE) " +
                "     VALUES      (" + FYID + ", " +
                "                 '" + Qtr + "'," +
                "                 '" + cmnService.J_ReplaceQuote(FormNo) + "'," +
                "                 '" + cmnService.J_ReplaceQuote(ImportDate) + "'," +
                "                 '" + cmnService.J_ReplaceQuote(RPUName) + "'," +
                "                 '" + cmnService.J_ReplaceQuote(FileCreationDate) + "')";
            if (dmlService.J_ExecSql(command, strSQL) == false)
                return false;
            else
                return true;
        }
        #endregion

        #region T_BulkImportFromTextFile
        private bool T_BulkImportFromTextFile(string ImportTableName, bool FirstRowAsColumnHeader)
        {
            //Recreating schema file
            if (File.Exists(Application.StartupPath + "\\schema.ini") == true)
                File.Delete(Application.StartupPath + "\\schema.ini");

            StreamWriter StreamWriter = new StreamWriter(Application.StartupPath + "\\schema.ini");
            StreamWriter.WriteLine("[" + ImportTableName + ".txt]");
            StreamWriter.WriteLine("ColNameHeader=" + (FirstRowAsColumnHeader == true ? "true" : "false") + "");
            StreamWriter.WriteLine("Format=Delimited(^)");
            StreamWriter.WriteLine("MaxScanRows=0");
            StreamWriter.WriteLine("CharacterSet=ANSI");

            #region DEFINING THE COLUMN NAMES

            StreamWriter.WriteLine(@"Col1=F1 Integer
                                     Col2=F2 Char
                                     Col3=F3 Char
                                     Col4=F4 Char
                                     Col5=F5 Char
                                     Col6=F6 Char
                                     Col7=F7 Char
                                     Col8=F8 Char
                                     Col9=F9 Char
                                     Col10=F10 Char
                                     Col11=F11 Char
                                     Col12=F12 Char
                                     Col13=F13 Char
                                     Col14=F14 Char
                                     Col15=F15 Char
                                     Col16=F16 Char
                                     Col17=F17 Char
                                     Col18=F18 Char
                                     Col19=F19 Char
                                     Col20=F20 Char
                                     Col21=F21 Char
                                     Col22=F22 Char
                                     Col23=F23 Char
                                     Col24=F24 Char
                                     Col25=F25 Char
                                     Col26=F26 Char
                                     Col27=F27 Char
                                     Col28=F28 Char
                                     Col29=F29 Char
                                     Col30=F30 Char
                                     Col31=F31 Char
                                     Col32=F32 Char
                                     Col33=F33 Char
                                     Col34=F34 Char
                                     Col35=F35 Char
                                     Col36=F36 Char
                                     Col37=F37 Char
                                     Col38=F38 Char
                                     Col39=F39 Char
                                     Col40=F40 Char
                                     Col41=F41 Char
                                     Col42=F42 Char
                                     Col43=F43 Char
                                     Col44=F44 Char
                                     Col45=F45 Char
                                     Col46=F46 Char
                                     Col47=F47 Char
                                     Col48=F48 Char
                                     Col49=F49 Char
                                     Col50=F50 Char
                                     Col51=F51 Char
                                     Col52=F52 Char
                                     Col53=F53 Char
                                     Col54=F54 Char
                                     Col55=F55 Char
                                     Col56=F56 Char
                                     Col57=F57 Char
                                     Col58=F58 Char
                                     Col59=F59 Char
                                     Col60=F60 Char
                                     Col61=F61 Char
                                     Col62=F62 Char");

            #endregion

            StreamWriter.Close();

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

            //QUERY TO IMPORT THE DATA FROM TEXT FILE
            strSQL = "SELECT * INTO [" + ImportTableName + "] FROM " +
                     "[Text; DATABASE=" + Application.StartupPath + "].[" + ImportTableName + ".txt" + "]";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage("Import Failed!");
                return false;
            }

            return true;
        }

        #endregion

        #region T_CreateTempTables
        public bool T_CreateTempTables()
        {
            try
            {
                if (dmlService.J_IsDatabaseObjectExist("TEMP_FH") == true)
                {
                    strSQL = "DROP TABLE TEMP_FH";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE TEMP_FH (" +
                         "             FILE_CREATION_DATE DATETIME," +
                         "             FILE_LINES_COUNT   TEXT(20)  DEFAULT \"\"," +
                         "             HASH_VALUE         TEXT(20)  DEFAULT \"\" " +
                         "             )";

                dmlService.J_ExecSql(strSQL);


                if (dmlService.J_IsDatabaseObjectExist("TEMP_BH") == true)
                {
                    strSQL = "DROP TABLE TEMP_BH";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE TEMP_BH (" +
                         "             CHALLAN_RECORDS_COUNT      NUMBER    DEFAULT 0," +
                         "             FORM_NO                    TEXT(4)   DEFAULT \"\"," +
                         "             ORIGINAL_RRR_NO            TEXT(15)  DEFAULT \"\"," +
                         "             PREVIOUS_RRR_NO            TEXT(15)  DEFAULT \"\"," +
                         "             TAN_NO                     TEXT(10)  DEFAULT \"\"," +
                         "             EXPECTED_CHALLAN_RECORD_NO NUMBER    DEFAULT 0," +
                         "             PAN_NO                     TEXT(10)  DEFAULT \"\"," +
                         "             ASST_YEAR                  TEXT(7)   DEFAULT \"\"," +
                         "             FA_YEAR                    TEXT(7)   DEFAULT \"\"," +
                         "             QTR                        TEXT(2)   DEFAULT \"\"," +
                         "             COMPANY_NAME               TEXT(75)  DEFAULT \"\"," +
                         "             BRANCH_DIV                 TEXT(75)  DEFAULT \"\"," +
                         "             ADDRESS1                   TEXT(25)  DEFAULT \"\"," +
                         "             ADDRESS2                   TEXT(25)  DEFAULT \"\"," +
                         "             ADDRESS3                   TEXT(25)  DEFAULT \"\"," +
                         "             ADDRESS4                   TEXT(25)  DEFAULT \"\"," +
                         "             ADDRESS5                   TEXT(25)  DEFAULT \"\"," +
                         "             STATE_CODE                 TEXT(2)   DEFAULT \"\"," +
                         "             PIN_CODE                   TEXT(6)   DEFAULT \"\"," +
                         "             EMAIL                      TEXT(75)  DEFAULT \"\"," +
                         "             STD                        TEXT(5)   DEFAULT \"\"," +
                         "             PHONE                      TEXT(30)  DEFAULT \"\"," +
                         "             ADDRESS_CHANGE             TEXT(1)   DEFAULT \"\"," +
                         "             CATEGORY_CODE              TEXT(5)   DEFAULT \"\"," +
                         "             PERSON_NAME                TEXT(75)  DEFAULT \"\"," +
                         "             DESIGNATION                TEXT(20)  DEFAULT \"\"," +
                         "             P_ADDRESS1                 TEXT(25)  DEFAULT \"\"," +
                         "             P_ADDRESS2                 TEXT(25)  DEFAULT \"\"," +
                         "             P_ADDRESS3                 TEXT(25)  DEFAULT \"\"," +
                         "             P_ADDRESS4                 TEXT(25)  DEFAULT \"\"," +
                         "             P_ADDRESS5                 TEXT(25)  DEFAULT \"\"," +
                         "             P_STATE_CODE               TEXT(2)   DEFAULT \"\"," +
                         "             P_PIN_CODE                 TEXT(6)   DEFAULT \"\"," +
                         "             P_EMAIL                    TEXT(75)  DEFAULT \"\"," +
                         "             EXPECTED_SD_RECORD_NO      NUMBER    DEFAULT 0," +
                         "             P_STD                      TEXT(5)   DEFAULT \"\"," +
                         "             P_PHONE                    TEXT(10)  DEFAULT \"\"," +
                         "             P_ADDRESS_CHANGE           TEXT(1)   DEFAULT \"\"," +
                         "             TOT_CHALLAN_DEPOSIT        MONEY     DEFAULT 0," +
                         "             P_MOBILE                   TEXT(10)  DEFAULT \"\"," +
                         "             COUNT_SD_RECORDS           NUMBER    DEFAULT 0," +
                         "             TOT_GROSS_TOT_INCOME       MONEY     DEFAULT 0," +
                         "             D_STATE_CODE               TEXT(2)   DEFAULT \"\"," +
                         "             PAO_CODE                   TEXT(20)  DEFAULT \"\"," +
                         "             DDO_CODE                   TEXT(20)  DEFAULT \"\"," +
                         "             MINISTRY_CODE              TEXT(3)   DEFAULT \"\"," +
                         "             MINISTRY_OTHER             TEXT(150) DEFAULT \"\"," +
                         "             PAO_REG_NO                 TEXT(7)   DEFAULT \"\"," +
                         "             DDO_REG_NO                 TEXT(10)  DEFAULT \"\" " +
                         "             )";

                dmlService.J_ExecSql(strSQL);


                if (dmlService.J_IsDatabaseObjectExist("TEMP_CD") == true)
                {
                    strSQL = "DROP TABLE TEMP_CD";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE TEMP_CD (" +
                         "             SL_NO                       NUMBER   DEFAULT 0," +
                         "             DEDUCTEE_COUNT              NUMBER   DEFAULT 0," +
                         "             EXPECTED_DEDUCTEE_RECORD_NO NUMBER   DEFAULT 0," +
                         "             CHALLAN_STATUS              TEXT(1)  DEFAULT \"\"," +
                         "             CHALLAN_NO                  TEXT(50) DEFAULT \"\"," +
                         "             TRANSFER_VOUCHER_NO         TEXT(9)  DEFAULT \"\"," +
                         "             BSR_CODE                    TEXT(50) DEFAULT \"\"," +
                         "             DEPOSIT_DATE                DATETIME," +
                         "             SECTION_NAME                TEXT(15) DEFAULT 0," +
                         "             TDS                         MONEY    DEFAULT 0," +
                         "             SURCHARGE                   MONEY    DEFAULT 0," +
                         "             EDUCATION_CESS              MONEY    DEFAULT 0," +
                         "             INTEREST                    MONEY    DEFAULT 0," +
                         "             OTHERS                      MONEY    DEFAULT 0," +
                         "             TOT_TAX                     MONEY    DEFAULT 0," +
                         "             CTRL_TOT_TAX                MONEY    DEFAULT 0," +
                         "             CTRL_TDS                    MONEY    DEFAULT 0," +
                         "             CTRL_SURCHARGE              MONEY    DEFAULT 0," +
                         "             CTRL_EDU_CESS               MONEY    DEFAULT 0," +
                         "             CTRL_TOT                    MONEY    DEFAULT 0," +
                         "             INTEREST_ALLOCATED          MONEY    DEFAULT 0," +
                         "             OTHERS_ALLOCATED            MONEY    DEFAULT 0," +
                         "             CHEQUE_NO                   TEXT(15) DEFAULT \"\"," +
                         "             BOOK_ENTRY                  TEXT(1)  DEFAULT \"\"," +
                         "             PENDING_AMOUNT              MONEY    DEFAULT 0" +
                         "             )";

                dmlService.J_ExecSql(strSQL);


                if (dmlService.J_IsDatabaseObjectExist("TEMP_DD") == true)
                {
                    strSQL = "DROP TABLE TEMP_DD";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE TEMP_DD (" +
                         "             CHALLAN_SL_NO         NUMBER    DEFAULT 0," +
                         "             SL_NO                 NUMBER    DEFAULT 0," +
                         "             DEDUCTEE_CODE         TEXT(2)   DEFAULT \"\"," +
                         "             DEDUCTEE_PAN          TEXT(10)  DEFAULT \"\"," +
                         "             DEDUCTEE_PAN_REF      TEXT(10)  DEFAULT \"\"," +
                         "             DEDUCTEE_NAME         TEXT(75)  DEFAULT \"\"," +
                         "             TAX_AMOUNT            MONEY     DEFAULT 0," +
                         "             SURCHARGE_AMOUNT      MONEY     DEFAULT 0," +
                         "             CESS_AMOUNT           MONEY     DEFAULT 0," +
                         "             TOTAL_AMOUNT          MONEY     DEFAULT 0," +
                         "             TAX_DEPOSITED_AMOUNT  MONEY     DEFAULT 0," +
                         "             TOT_VALUE_PURCHASE    MONEY     DEFAULT 0," +
                         "             PAYMENT_AMOUNT        MONEY     DEFAULT 0," +
                         "             PAYMENT_DATE          DATETIME," +
                         "             DEDUCTED_DATE         DATETIME," +
                         "             RATE                  MONEY     DEFAULT 0," +
                         "             GROSSING_UP_INDICATOR TEXT(1)   DEFAULT \"\"," +
                         "             CASH_BOOK_ENTRY       TEXT(1)   DEFAULT \"\"," +
                         "             NON_DEDUCTION_FLAG    TEXT(1)   DEFAULT \"\"," +
                         "             INVALID_PAN           TEXT(1)   DEFAULT \"\"," +
                         "             PAN_COUNTER           NUMBER    DEFAULT 0" +
                         "             )";

                dmlService.J_ExecSql(strSQL);


                if (dmlService.J_IsDatabaseObjectExist("TEMP_SD") == true)
                {
                    strSQL = "DROP TABLE TEMP_SD";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE TEMP_SD (" +
                         "             SL_NO                 NUMBER    DEFAULT 0," +
                         "             EMPLOYEE_PAN          TEXT(10)  DEFAULT \"\"," +
                         "             EMPLOYEE_NAME         TEXT(75)  DEFAULT \"\"," +
                         "             EMPLOYEE_CATEGORY     TEXT(10)  DEFAULT \"\"," +
                         "             FROM_DATE             DATETIME," +
                         "             TO_DATE               DATETIME," +
                         "             TS_BALANCE            MONEY     DEFAULT 0," +
                         "             US_16_AGGREGATE       MONEY     DEFAULT 0," +
                         "             INCOME_CHARGEABLE     MONEY     DEFAULT 0," +
                         "             AIS_TOTAL             MONEY     DEFAULT 0," +
                         "             GROSS_TOTAL_INCOME    MONEY     DEFAULT 0," +
                         "             CVIA_DED_TOTAL        MONEY     DEFAULT 0," +
                         "             TOTAL_INCOME          MONEY     DEFAULT 0," +
                         "             TAX_TOTAL_INCOME      MONEY     DEFAULT 0," +
                         "             SCHG_TOTAL_INCOME     MONEY     DEFAULT 0," +
                         "             ECESS_TOTAL_INCOME    MONEY     DEFAULT 0," +
                         "             US_89_LESS            MONEY     DEFAULT 0," +
                         "             TAX_PAYABLE           MONEY     DEFAULT 0," +
                         "             TOTAL_TDS_DEDUCTED    MONEY     DEFAULT 0," +
                         "             SHORTFALL_TAX         MONEY     DEFAULT 0," +
                         "             INVALID_PAN           TEXT(1)   DEFAULT \"\"," +
                         "             PAN_COUNTER           NUMBER    DEFAULT 0" +
                         "             )";

                dmlService.J_ExecSql(strSQL);

                if (dmlService.J_IsDatabaseObjectExist("TEMP_S16") == true)
                {
                    strSQL = "DROP TABLE TEMP_S16";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE TEMP_S16 (" +
                         "             SD_SL_NO              NUMBER    DEFAULT 0," +
                         "             SECTION_ID            TEXT(15)  DEFAULT \"\"," +
                         "             TOTAL_AMOUNT          MONEY     DEFAULT 0" +
                         "             )";

                dmlService.J_ExecSql(strSQL);


                if (dmlService.J_IsDatabaseObjectExist("TEMP_C6A") == true)
                {
                    strSQL = "DROP TABLE TEMP_C6A";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE TEMP_C6A (" +
                         "             SD_SL_NO              NUMBER    DEFAULT 0," +
                         "             SECTION_ID            TEXT(15)  DEFAULT \"\"," +
                         "             TOTAL_AMOUNT          MONEY     DEFAULT 0" +
                         "             )";

                dmlService.J_ExecSql(strSQL);
                
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion

        #endregion

    }
}

