
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
using System.Collections.Generic;

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;


#endregion



namespace TDSMAN.FormTrn
{
    public partial class TrnTDSFile_BulkImport : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnTDSFile_BulkImport()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            ////--
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
        string strFileCreationDateFVU5_2 = "";
        string strImportErrorMessage = "Import Failed.";
        string strImportVerificationMessage = "Import Completed but verification failed.";

        int intDefaultInvalidReturnValue = 1;

        bool blnNullSectionFound = false;
        //-- 30/11/2017 --
        string strLogFileCreationDate = "";
        string strLogTanNo = "";
        string strLogForm = "";
        string strLogFaYear = "";
        string strLogFaYearId = "";
        string strLogQuarter = "";
        string strFileCreationDateDDMMYYY = "";
        //----------------
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

        #region TrnFVUImport_Load

        private void TrnFVUImport_Load(object sender, EventArgs e)
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
            lblTitle.Text = "Import Data for Correction";
            lblNotes1.Text = "As per the new notification released by the TRACES it is mandatory to make corrections using consolidated TDS/TCS\nstatement only downloaded from TRACES-website.";

            //lnkLabel2.Text = "Click here to download the tutorial on how to get the TDS/TCS file from TIN-NSDL."
            //-- ANIK 2011/09/03
            //lnkLabel2.Text = "Click here to download the tutorial on how to get the TDS/TCS.";
            lnkLabel2.Text = "Click here to view the tutorial on how to get the TDS/TCS.";

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
            //System.Diagnostics.Process.Start("http://www.tdsman.com/Downloads/eTutorial-TAN-Registration.zip");
            //-- ANIK 2013/03/16
            System.Diagnostics.Process.Start("https://www.tdscpc.gov.in/en/download-nsdl-conso-file-etutorial.html");
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
            //
            if (ValidateFields() == false) return;
            //
            OutputFolder = Path.Combine(Application.StartupPath, "Correction Input Files");
            strFileName = cmnService.J_GetFileName(strFVUPath);

            //Getting filename without extension
            strFileName = cmnService.J_Left(cmnService.J_GetFileName(strFVUPath), strFileName.Length - 4);
            //
            string strTempFileName = strFileName;
            //
            int intIncrement = 0;
            // --------------------------------------------
            // -- Assigning a different filename if the file with same name exists
            // --------------------------------------------
            do
            {
                if (cmnService.J_IsFileExist(Path.Combine(OutputFolder, strFileName + ".tds")) == true)
                {
                    intIncrement++;
                    strFileName = strTempFileName + " (" + intIncrement + ")";
                }

            } while (cmnService.J_IsFileExist(Path.Combine(OutputFolder, strFileName + ".tds")) == true);
            //
            OutputFilePath = OutputFolder + "\\" + strFileName + ".tds";

            //SAVING THE FILE SELECTED IN APPLICATION FOLDER
            cmnService.J_CreateDirectory(OutputFolder);

            File.Copy(strFVUPath, OutputFilePath, true);

            // TO CHECK IF THE FILE SELECTED IS CONSOLIDATED FILE ONLY
            string strQtr = "", strFormNo = "", strTanNo = "", strFinancialYear = ""; strFileCreationDateDDMMYYY = "";
            if (CheckFVUCompatibility(txtFVUPath.Text, out strFinancialYear, out strFormNo ,out strQtr, out strTanNo, out strFileCreationDateDDMMYYY) == false)
            {
                cmnService.J_UserMessage(strCheckCompatibilityMessage);
                this.Cursor = Cursors.Default;
                btnSelectTDSPath.Select();
                return;
            }
            ////--
            //if (strFormNo == T_FormNo.F138_24Q)
            //    strFormNo = T_FormNo.F24Q;
            //else if (strFormNo == T_FormNo.F140_26Q)
            //    strFormNo = T_FormNo.F26Q;
            //else if (strFormNo == T_FormNo.F144_27Q)
            //    strFormNo = T_FormNo.F27Q;
            //else if (strFormNo == T_FormNo.F143_27EQ)
            //    strFormNo = T_FormNo.F27EQ;
            //--
            long lngFaYearID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ReturnId("SELECT ASST_ID FROM MST_ASSESSMENT WHERE FA_YEAR ='" + cmnService.J_Left(strFinancialYear, 4) + "-" + cmnService.J_Right(strFinancialYear, 2) + "'")));
            //if (Convert.ToInt32(strFileCreationDateFVU5_2) < 20160917)
            //{
            //    if (cmnService.J_UserMessage("TDS File Creation Date : " + strFileCreationDate + "\nIt is recommended to download the latest TDS file, you may find error while generating file on FVU 5.2 onwards.\n Proceed to Import?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            //    {
            //        BtnExit.Select();
            //        return;
            //    }
            //}
            //else
            //{
            //
            #region CHECK LINE RECORDS OF CONSO FILE IMPORT (4 times OF MAX ALLOWED LINE RECORDS) //-- 2025/07/17
            //
            #region GET LINE OF RECORDS OF CONSO FILE
            //-- DD
            //int intCountConsoDD = 0;
            //foreach (string line in File.ReadLines(txtFVUPath.Text))
            //{
            //    if (line.Contains("^DD^"))
            //        intCountConsoDD++;
            //}
            ////-- SD
            //int intCountConsoSD = 0;
            //foreach (string line in File.ReadLines(txtFVUPath.Text))
            //{
            //    if (line.Contains("^SD^"))
            //        intCountConsoSD++;
            //}
            ////-- 94P
            //int intCountConso94P = 0;
            //foreach (string line in File.ReadLines(txtFVUPath.Text))
            //{
            //    if (line.Contains("^94P^"))
            //        intCountConso94P++;
            //}
            //--
            #endregion
            //
            #region FETCHING EXISTING LINE OF RECORDS FROM EXISTING DATA BASED ON 'SoftwareVersion'
            //### GETTING THE BATCH ID(s) FOR THIS 'SoftwareVersion' ###
            //IDataReader drdGetBasicInfoIDs = null;
            //List<int> BatchHeaderIds = new List<int>();
            //string SoftwareVersion = TDSMAN.Classes.TDSMAN.T_pEditionType + "." + TDSMAN.Classes.TDSMAN.T_pPackageFAYear;
            ////
            //strSQL = "SELECT BATCH_HEADER_ID FROM COR_HDR_BATCH WHERE VERSION_NO = " + SoftwareVersion;
            //drdGetBasicInfoIDs = dmlService.J_ExecSqlReturnReader(strSQL);
            ////if (drdGetBasicInfoIDs == null)
            ////    return false;
            ////--
            //while (drdGetBasicInfoIDs.Read())
            //{
            //    BatchHeaderIds.Add(Convert.ToInt32(drdGetBasicInfoIDs["BATCH_HEADER_ID"]));
            //}
            //drdGetBasicInfoIDs.Close();
            //long lngCorrectionLineOfRecords = 0;
            //foreach (int BatchHeaderId in BatchHeaderIds) //-- DD
            //{
            //    lngCorrectionLineOfRecords += dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BatchHeaderId, J_QueryType.DirectQuery);
            //    //
            //}
            //foreach (int BatchHeaderId in BatchHeaderIds) //-- SD
            //{
            //    lngCorrectionLineOfRecords += dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM COR_TRN_SALARY_DETAILS WHERE BATCH_HEADER_ID = " + BatchHeaderId, J_QueryType.DirectQuery);
            //    //
            //}
            //foreach (int BatchHeaderId in BatchHeaderIds) //-- 94P
            //{
            //    lngCorrectionLineOfRecords += dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM COR_TRN_SALARY_DETAILS_194P WHERE BATCH_HEADER_ID = " + BatchHeaderId, J_QueryType.DirectQuery);
            //    //
            //}
            #endregion
            //
            //if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
            //{
            //    if (intCountConsoDD + intCountConsoSD + intCountConso94P + lngCorrectionLineOfRecords > TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions)
            //    {
            //        //cmnService.J_UserMessage("Exceeding the conso import limit (" + TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions + ") line records");
            //        TrnTrialMessageBox TrialMessageBox = new TrnTrialMessageBox();
            //        TrialMessageBox.StartPosition = FormStartPosition.CenterScreen;
            //        //
            //        if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
            //            TrialMessageBox.lblMessage1.Text = "You are using the Standard version.";
            //        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
            //            TrialMessageBox.lblMessage1.Text = "You are using the Professional version.";
            //        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION)
            //            TrialMessageBox.lblMessage1.Text = "You are using the Enterprise version.";
            //        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)
            //            TrialMessageBox.lblMessage1.Text = "You are using the Enterprise (Lite) version.";
            //        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
            //            TrialMessageBox.lblMessage1.Text = "You are using the Enterprise (Ultimate) version.";
            //        //
            //        TrialMessageBox.lblMessage2.Text = "It supports only upto " + TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions + " records in the conso file.";
            //        TrialMessageBox.Show();
            //        return;
            //    }
            //}
            //else
            //{
            //    if (intCountConsoDD + intCountConsoSD + intCountConso94P + lngCorrectionLineOfRecords > TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountTrialEditions)
            //    {
            //        //cmnService.J_UserMessage("Exceeding the conso import limit (" + TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountTrialEditions + ") line records");
            //        TrnTrialMessageBox TrialMessageBox = new TrnTrialMessageBox();
            //        TrialMessageBox.StartPosition = FormStartPosition.CenterScreen;
            //        TrialMessageBox.lblMessage2.Text = "It supports only upto " + TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountTrialEditions + " records in the conso file.";
            //        TrialMessageBox.Show();
            //        return;
            //    }
            //}
            //################################
            #endregion
            //
            strSQL = @"SELECT COR_HDR_BATCH.IMPORTED_DATE 
                       FROM   COR_HDR_BATCH, 
                              MST_ASSESSMENT,
                              COR_HDR_COMPANY
                       WHERE  COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID 
                       AND    COR_HDR_BATCH.BATCH_HEADER_ID = COR_HDR_COMPANY.BATCH_HEADER_ID 
                       AND    COR_HDR_BATCH.FORM_NO   ='" + strFormNo + @"' 
                       AND    COR_HDR_BATCH.QTR       ='" + strQtr + @"' 
                       AND    MST_ASSESSMENT.FA_YEAR  ='" + cmnService.J_Left(strFinancialYear,4) + "-" + cmnService.J_Right(strFinancialYear,2) + @"'
                       AND    COR_HDR_COMPANY.TAN_NO  ='" + strTanNo + @"'
                       AND    FILE_DATE ='" + strFileCreationDateDDMMYYY + "'";
            if (Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)) != "")
            {
                if(cmnService.J_UserMessage("TDS File Creation Date : " + strFileCreationDate + "\nThis File has been already imported on : " + Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)) + "\nOpen the Return?", MessageBoxButtons.OKCancel) == DialogResult.OK )
                { 
                
                    //
                    //if (cmnService.J_UserMessage("Do you want to view the Return?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                        strSQL = @"SELECT COR_HDR_BATCH.BATCH_HEADER_ID 
                                   FROM   COR_HDR_BATCH, 
                                          MST_ASSESSMENT,
                                          COR_HDR_COMPANY
                                   WHERE  COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID 
                                   AND    COR_HDR_BATCH.BATCH_HEADER_ID = COR_HDR_COMPANY.BATCH_HEADER_ID 
                                   AND    COR_HDR_BATCH.FORM_NO   ='" + strFormNo + @"' 
                                   AND    COR_HDR_BATCH.QTR       ='" + strQtr + @"' 
                                   AND    MST_ASSESSMENT.FA_YEAR  ='" + cmnService.J_Left(strFinancialYear, 4) + "-" + cmnService.J_Right(strFinancialYear, 2) + @"'
                                   AND    COR_HDR_COMPANY.TAN_NO  ='" + strTanNo + @"'
                                   AND    FILE_DATE ='" + strFileCreationDateDDMMYYY + "'";
                        lngBatchID = Convert.ToInt32(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                        GC.Collect();
                        //
                        dmlService.Dispose();
                        this.Close();
                        this.Dispose();
                        //-- 2019/04/01
                        TDSMAN.Classes.TDSMAN.T_pBatchId = lngBatchID;
                        //
                        TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard = true;
                    //--
                    if (lngFaYearID >= T_FinancialYearID.F2026_27ID)
                        cmnService.J_ShowChildForm(new Trn26QCorrectionReturn_2627(), J_Var.frmMain, "Make Corrections");
                    else
                        cmnService.J_ShowChildForm(new Trn26QCorrectionReturn(), J_Var.frmMain, "Make Corrections");
                    return;
                }
                else
                {
                    BtnExit.Select();
                    return;
                }
            }
            else
            {
                //DISPLAYING THE CONFIRMATION MESSAGE BEFORE IMPORTING
                if (cmnService.J_UserMessage("TDS File Creation Date : " + strFileCreationDate + "\nPlease ensure that you are using the latest file for preparing correction statement.\n Proceed to Import?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    BtnExit.Select();
                    return;
                }
            }
            //}
            if (strFormNo == T_FormNo.F138_24Q)
                strFormNo = T_FormNo.F24Q;
            else if (strFormNo == T_FormNo.F140_26Q)
                strFormNo = T_FormNo.F26Q;
            else if (strFormNo == T_FormNo.F144_27Q)
                strFormNo = T_FormNo.F27Q;
            else if (strFormNo == T_FormNo.F143_27EQ)
                strFormNo = T_FormNo.F27EQ;
            //--
            this.Refresh();
            //--
            if (lngFaYearID >= T_FinancialYearID.F2026_27ID)
            {
                if (ImportTransactionsFromConso2627(txtFVUPath.Text, strFinancialYear) == false)
                {
                    prgBar.Value = 0;
                    this.Cursor = Cursors.Default;
                    //MessageBox.Show("1-err");
                    return;
                }
            }
            else
            {
                //IMPORTING ALL THE DATA FROM THE TEXT FILE
                //MessageBox.Show("1");
                if (ImportTransactionsFromConso(txtFVUPath.Text, strFinancialYear) == false)
                {
                    prgBar.Value = 0;
                    this.Cursor = Cursors.Default;
                    //MessageBox.Show("1-err");
                    return;
                }
            }
            //MessageBox.Show("2");
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
            //if (blnNullSectionFound == true)
            //{
            //    cmnService.J_UserMessage("Import Completed\n\nPlease note Section Code was found missing in few of the challans present in the file downloaded from TRACES." +
            //        "\nEnter the valid sections in all the challans present for successful generation of the Correction return.");
            //}
            //else
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {                
                TdsMan.SHRINK_DATABASE();
            }
            //
            cmnService.J_UserMessage("Import Completed");
            //
            //
            prgBar.Value = 0;
            //
            if (cmnService.J_UserMessage("Do you want to view Correction Return form?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                GC.Collect();
                //
                dmlService.Dispose();
                this.Close();
                this.Dispose();
                //-- 2019/04/01
                TDSMAN.Classes.TDSMAN.T_pBatchId = lngBatchID;
                //
                TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard = true;
                //--
                if (lngFaYearID >= T_FinancialYearID.F2026_27ID)
                    cmnService.J_ShowChildForm(new Trn26QCorrectionReturn_2627(), J_Var.frmMain, "Make Corrections");
                else
                    cmnService.J_ShowChildForm(new Trn26QCorrectionReturn(), J_Var.frmMain, "Make Corrections");
            }
            else
                chkAddDeducteesToRegularMaster.Checked = false;  //-- Added By Abhishek Dey On 10/04/2018 --
            //
        }
        #endregion

        //Added by Indrajit on 26-02-2013
        #region TrnTDSFile_BulkImport_FormClosing
        private void TrnTDSFile_BulkImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (TdsMan.FreeBasicInfoEntry() == false)
            //    return;
            if (TdsMan.FreeBasicInfoEntry(true) == false)
                return;
        }
        #endregion

        #region tmrLoginRefresh_Tick
        private void tmrLoginRefresh_Tick(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pBasicInfoId > 0)
            //{
            //strSQL = "UPDATE TEMP_STACK_BASIC_INFO " +
            //         "SET LAST_UPDATED_TIME   = " + TdsMan.GetServerDateTime() + " " +
            //         "WHERE BASIC_INFO_ID     = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + " " +
            //         "AND   PRINT_USER_SERIAL = '" + TDSMAN.Classes.TDSMAN.T_pProductSerial + "'";
            //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            //}
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
        private bool CheckFVUCompatibility(string FVUPath, out string strFinancialYear, out string strFormNo, out string strQtr, out string strTanNo, out string strFileCreationDateDDMMYYY)
        { 
            strQtr = ""; strFormNo = ""; strTanNo = "";
            strFinancialYear = ""; strFileCreationDateDDMMYYY = ""; 
            try
            {
                //string strFinancialYear = "";
                string FinancialYear = "";
                //-- 

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
                            strCheckCompatibilityMessage = "File format is incorrect. Please choose the Consolidated file only. 1";
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
                        //-- Added By Abhishek Dey On 30/11/2017 --
                        strLogFileCreationDate = cmnService.J_Right(strFileCreationDate, 4) + "-" + cmnService.J_Mid(strFileCreationDate, 2, 2) + "-" + cmnService.J_Left(strFileCreationDate, 2);
                        //-----------------------------------------
                        //
                        strFileCreationDate = cmnService.J_Left(strFileCreationDate, 2) + "/" + cmnService.J_Mid(strFileCreationDate, 2, 2) + "/" + cmnService.J_Right(strFileCreationDate, 4);
                        strFileCreationDateDDMMYYY = strFileCreationDate;
                        //Formatting the date for display in the confirmation message
                        strFileCreationDateFVU5_2 = String.Format("{0:yyyyMMdd}", dtService.J_ConvertddMMyyyy(strFileCreationDate));
                        strFileCreationDate = String.Format("{0:dd MMMM yyyy}", dtService.J_ConvertddMMyyyy(strFileCreationDate));
                    }

                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "BH")
                    {
                        string strOriginalPRN = "";
                        string strPreviousPRN = "";
                        // FORM NO.
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strFormNo = ListLines[i].Substring(intCaratPosition + 1);
                        strFormNo = ListLines[i].Substring(intCaratPosition + 1, strFormNo.Trim().IndexOf("^"));

                        //Modified by Shrey Kejriwal on 16/12/2011

                        //for (int a = 1; a < 7; a++)
                        for (int a = 1; a < 4; a++)
                        {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strOriginalPRN = ListLines[i].Substring(intCaratPosition + 1);
                        strOriginalPRN = ListLines[i].Substring(intCaratPosition + 1, strOriginalPRN.Trim().IndexOf("^"));

                        if (strOriginalPRN == "")
                        {
                            strCheckCompatibilityMessage = "Incorrect Format of the File. Please check. 2";
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
                            strCheckCompatibilityMessage = "Incorrect Format of the File. Please check. 3";
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }
                        if (strPreviousPRN == "")
                        {
                            strCheckCompatibilityMessage = "Incorrect Format of the File. Please check. 4";
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }

                        // TAN
                        for (int a = 1; a < 5; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTanNo = ListLines[i].Substring(intCaratPosition + 1);
                        strTanNo = ListLines[i].Substring(intCaratPosition + 1, strTanNo.Trim().IndexOf("^"));

                        // FINANCIAL YEAR
                        //for (int a = 1; a < 9; a++)
                        for (int a = 1; a < 5; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strFinancialYear = ListLines[i].Substring(intCaratPosition + 1);
                        strFinancialYear = ListLines[i].Substring(intCaratPosition + 1, strFinancialYear.Trim().IndexOf("^"));

                        // QTR
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strQtr = ListLines[i].Substring(intCaratPosition + 1);
                        strQtr = ListLines[i].Substring(intCaratPosition + 1, strQtr.Trim().IndexOf("^"));


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
                        //FinancialYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FA_YEAR FROM MST_ASSESSMENT ORDER BY ASST_ID"));
                        //-- 2016/06/03
                        FinancialYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FA_YEAR FROM MST_ASSESSMENT WHERE VISIBILITY_FLAG = 0 ORDER BY ASST_ID"));
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
                strCheckCompatibilityMessage = "Incorrect Format of the File. Please check. ";
                return false;
            }
        }
        #endregion

        #region ImportTransactionsFromConso
        private bool ImportTransactionsFromConso(string FVUPath, string FinancialYear)
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
                //MessageBox.Show("1.1");
                //dmlService.J_BeginTransaction();

                #region TDS file Import

                #region COMMENTED
                ////TABLE NAME FOR DATA IMPORT
                //strImporttableName = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_TDS_FILE_DATA + "";

                ////COPY .TDS FILE AS .TXT FILE TO IMPORT DATA TO ACCESS
                //File.Copy(strFVUPath, Application.StartupPath + "\\" + strImporttableName + ".txt", true);


                ////-- Creation of dump table from .tds file
                //if (T_BulkImportFromTextFile(strImporttableName, false) == false) return false;
                ////
                ////-- DELETION OF DUPLICATE DATA
                //if (T_DeleteDuplicateData() == false) return false;

                //prgBar.Value = prgBar.Value + 10;

                ////Deleting .TXT file
                //File.Delete(Application.StartupPath + "\\" + strImporttableName + ".txt");
                #endregion
                //--
                strImporttableName = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_TDS_FILE_DATA + "";
                //COPY .TDS FILE AS .TXT FILE TO IMPORT DATA TO ACCESS
                if (File.Exists(Application.StartupPath + "\\" + strImporttableName + ".txt") == true)
                    File.Delete(Application.StartupPath + "\\" + strImporttableName + ".txt");
                File.Copy(strFVUPath, Application.StartupPath + "\\" + strImporttableName + ".txt", true);
                //}
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    #region ImportTableName
                    dmlService.J_BeginTransaction();
                    if (dmlService.J_IsDatabaseObjectExist(strImporttableName) == true)
                    {
                        strSQL = "DROP TABLE " + strImporttableName + "";
                        dmlService.J_ExecSql(strSQL);
                    }
                    if (dmlService.J_IsDatabaseObjectExist(strImporttableName) == false)
                    {
                        strSQL = @"CREATE TABLE " + strImporttableName + @" (
                                                " + cmnService.J_GetDataType("F1", J_Identity.YES) + @",
                                                " + cmnService.J_GetDataType("F2", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F3", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F4", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F5", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F6", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F7", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F8", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F9", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F10", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F11", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F12", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F13", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F14", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F15", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F16", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F17", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F18", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F19", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F20", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F21", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F22", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F23", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F24", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F25", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F26", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F27", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F28", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F29", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F30", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F31", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F32", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F33", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F34", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F35", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F36", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F37", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F38", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F39", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F40", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F41", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F42", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F43", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F44", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F45", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F46", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F47", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F48", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F49", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F50", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F51", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F52", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F53", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F54", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F55", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F56", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F57", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F58", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F59", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F60", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F61", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F62", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F63", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F64", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F65", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F66", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F67", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F68", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F69", J_ColumnType.String, 255) + @"," + //-- 2019/05/24
                                                "" + cmnService.J_GetDataType("F70", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F71", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F72", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F73", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F74", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F75", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F76", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F77", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F78", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F79", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F80", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F81", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F82", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F83", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F84", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F85", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F86", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F87", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F88", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F89", J_ColumnType.String, 255) + @")";
                        dmlService.J_ExecSql(strSQL);
                    }
                    dmlService.J_Commit();
                    #endregion
                    //--
                    DataTable dt = new DataTable();
                    //
                    //for (int col = 0; col < 69; col++)
                    //-- 2019/05/24
                    //for (int col = 0; col < 80; col++)
                    for (int col = 0; col < 89; col++)
                            dt.Columns.Add(new DataColumn("F" + (col + 1).ToString()));
                    //
                    GC.Collect();
                    //
                    string[] lines = System.IO.File.ReadAllLines(Application.StartupPath + "\\" + strImporttableName + ".txt");
                    //
                    GC.Collect();
                    //
                    foreach (string line in lines)
                    {
                        try
                        {
                            string[] columns = line.Split('^');
                            dt.Rows.Add(columns);
                        }
                        catch (Exception err)
                        {
                            GC.Collect();
                            continue;
                        }
                    }
                    //
                    dmlService.J_BulkCopyToSqlDatabase(dt, null, strImporttableName);
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    //-- Creation of dump table from .tds file
                    if (T_BulkImportFromTextFile(strImporttableName, false) == false) return false;
                    //--
                }
                //-- DELETION OF DUPLICATE DATA
                if (T_DeleteDuplicateData() == false) return false;
                //
                prgBar.Value = prgBar.Value + 10;
                //Deleting .TXT file
                File.Delete(Application.StartupPath + "\\" + strImporttableName + ".txt");
                // ----------------------------------------------------------
                // -- Added by Shrey Kejriwal on 01-04-2016
                // -- Checking for structure change in CD
                // ----------------------------------------------------------
                string strChequeValue = string.Empty;
                //--
                strSQL = "SELECT TOP 1 F36 " +
                         "FROM   " + strImporttableName + " " +
                         "WHERE F2 = 'CD'";
                //
                //strChequeValue = dmlService.J_ExecSqlReturnScalar(strSQL).ToString().Trim().ToUpper();
                strChequeValue = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)).Trim().ToUpper();
                //
                if (strChequeValue == "Y" || strChequeValue == "N")
                {
                    strSQL = "UPDATE " + strImporttableName + " " +
                             "SET   F36 = '', " +
                             "      F37 = F36, " +
                             "      F38 = F37, " +
                             "      F39 = F38, " +
                             "      F40 = F39, " +
                             "      F41 = F40 " +
                             "WHERE F2 = 'CD' " +
                             "AND   (F36 = 'Y' OR F36 = 'N')";

                    dmlService.J_ExecSql(strSQL);
                }
                // ----------------------------------------------------------
                #endregion
                //MessageBox.Show("1.2");
                #region Creating Temporary Tables
                if (T_CreateTempTables() == false)
                {
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                #endregion
                //MessageBox.Show("1.3");
                #region Insert Data To Temporary Tables

                #region Insert TEMP_FH

                //Insert data into Temp Tables
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "   " +
                             "SELECT CONVERT(DATETIME, SUBSTRING(F5, 3, 2) + '/' + LEFT(F5, 2) + '/' +  RIGHT(F5, 4), 101) AS FILE_CREATION_DATE," +
                             "       F10 AS FILE_LINES_COUNT," +
                             "       F15 AS HASH_VALUE   " +
                             "FROM [" + strImporttableName + "] WHERE F2 = 'FH'";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "   " +
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

                #region Insert " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "

                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + " (" +
                         "       CHALLAN_RECORDS_COUNT," +
                         "       FORM_NO," +
                         "       ORIGINAL_RRR_NO," +
                         "       PREVIOUS_RRR_NO," +
                         "       TAN_NO," +
                         "       EXPECTED_CHALLAN_RECORD_NO," +
                         "       PAN_NO," +
                         "       ASST_YEAR," +
                         "       FA_YEAR," +
                         "       QTR," +
                         "       COMPANY_NAME," +
                         "       BRANCH_DIV," +
                         "       ADDRESS1," +
                         "       ADDRESS2," +
                         "       ADDRESS3," +
                         "       ADDRESS4," +
                         "       ADDRESS5," +
                         "       STATE_CODE," +
                         "       PIN_CODE," +
                         "       EMAIL," +
                         "       STD," +
                         "       PHONE," +
                         "       ADDRESS_CHANGE," +
                         "       CATEGORY_CODE," +
                         "       PERSON_NAME," +
                         "       DESIGNATION," +
                         "       P_ADDRESS1," +
                         "       P_ADDRESS2," +
                         "       P_ADDRESS3," +
                         "       P_ADDRESS4," +
                         "       P_ADDRESS5," +
                         "       P_STATE_CODE," +
                         "       P_PIN_CODE," +
                         "       P_EMAIL," +
                         "       EXPECTED_SD_RECORD_NO," +
                         "       P_STD," +
                         "       P_PHONE," +
                         "       P_ADDRESS_CHANGE," +
                         "       TOT_CHALLAN_DEPOSIT," +
                         "       P_MOBILE," +
                         "       COUNT_SD_RECORDS," +
                         "       TOT_GROSS_TOT_INCOME," +
                         "       D_STATE_CODE," +
                         "       PAO_CODE," +
                         "       DDO_CODE," +
                         "       MINISTRY_CODE," +
                         "       MINISTRY_OTHER," +
                         "       P_PAN," +
                         "       PAO_REG_NO," +
                         "       DDO_REG_NO," +
                         "       ALT_STD," +
                         "       ALT_PHONE," +
                         "       ALT_EMAIL," +
                         "       ALT_P_STD," +
                         "       ALT_P_PHONE," +
                         "       ALT_P_EMAIL," +
                         "       AIN) " +
                         "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS CHALLAN_RECORDS_COUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS FORM_NO, " +
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
                         "       " + cmnService.J_SQLDBFormat("F59", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_PAN, " +
                         "       " + cmnService.J_SQLDBFormat("F60", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PAO_REG_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F61", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DDO_REG_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F63", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_STD, " +
                         "       " + cmnService.J_SQLDBFormat("F64", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_PHONE, " +
                         "       " + cmnService.J_SQLDBFormat("F65", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_EMAIL, " +
                         "       " + cmnService.J_SQLDBFormat("F66", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_P_STD, " +
                         "       " + cmnService.J_SQLDBFormat("F67", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_P_PHONE, " +
                         "       " + cmnService.J_SQLDBFormat("F68", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_P_EMAIL, " +
                         "       " + cmnService.J_SQLDBFormat("F69", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS AIN   " +
                         "FROM [" + strImporttableName + "] WHERE F2 = 'BH'";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                IDataReader reader;
                //reader = dmlService.J_ExecSqlReturnReader("SELECT FORM_NO,QTR FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ""); 
                //-- Modified On 30/11/2017 By Abhishek -- 
                reader = dmlService.J_ExecSqlReturnReader("SELECT FORM_NO, QTR, TAN_NO, FA_YEAR FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ""); 
                if (reader == null)
                {
                    reader.Close();
                    reader.Dispose();
                    return false;
                }
                while (reader.Read())
                {
                    //strFormNo = Convert.ToString(reader["FORM_NO"]);
                    //strQTR = Convert.ToString(reader["QTR"]);
                    //-- Modified By Abhishek On 30/11/2017 --
                    strLogForm = strFormNo = Convert.ToString(reader["FORM_NO"]);
                    strLogQuarter = strQTR = Convert.ToString(reader["QTR"]);
                    strLogTanNo = Convert.ToString(reader["TAN_NO"]);
                    strLogFaYear = Convert.ToString(reader["FA_YEAR"]);
                    //----------------------------------------
                }
                reader.Close();
                reader.Dispose();

                #endregion

                #region Insert " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "

                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "  ( " +
                         "       SL_NO, " +
                         "       DEDUCTEE_COUNT, " +
                         "       EXPECTED_DEDUCTEE_RECORD_NO, " +
                         "       CHALLAN_STATUS, " +
                         "       CHALLAN_NO, " +
                         "       TRANSFER_VOUCHER_NO, " +
                         "       BSR_CODE, " +
                         "       DEPOSIT_DATE," +
                         "       SECTION_NAME, " +
                         "       TDS, " +
                         "       SURCHARGE, " +
                         "       EDUCATION_CESS, " +
                         "       INTEREST, " +
                         "       OTHERS, " +
                         "       TOT_TAX, " +
                         "       CTRL_TOT_TAX, " +
                         "       CTRL_TDS, " +
                         "       CTRL_SURCHARGE, " +
                         "       CTRL_EDU_CESS, " +
                         "       CTRL_TOT, " +
                         "       INTEREST_ALLOCATED, " +
                         "       OTHERS_ALLOCATED, " +
                         "       CHEQUE_NO, " +
                         "       BOOK_ENTRY, " +
                         "       PENDING_AMOUNT," +
                         "       FEE," +
                         "       MINOR_CODE) " +
                         "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_COUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F8", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS EXPECTED_DEDUCTEE_RECORD_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F9", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS CHALLAN_STATUS, " +
                         "       " + cmnService.J_SQLDBFormat("F12", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS CHALLAN_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F14", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS TRANSFER_VOUCHER_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F16", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS BSR_CODE, " +
                    //"           DateSerial(RIGHT(F18,4), MID(F18,3,2),LEFT(F18,2))                                     AS DEPOSIT_DATE," +
                         "       " + ConvertSQLDate("F18") + "                                                              AS DEPOSIT_DATE," +
                         "       " + cmnService.J_SQLDBFormat("F21", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS SECTION_NAME, " +
                         "       " + cmnService.J_SQLDBFormat("F22", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TDS, " +
                         "       " + cmnService.J_SQLDBFormat("F23", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SURCHARGE, " +
                         "       " + cmnService.J_SQLDBFormat("F24", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS EDUCATION_CESS, " +
                         "       " + cmnService.J_SQLDBFormat("F25", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS INTEREST, " +
                         "       " + cmnService.J_SQLDBFormat("F26", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS OTHERS, " +
                         "       " + cmnService.J_SQLDBFormat("F27", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TOT_TAX, " +
                         "       " + cmnService.J_SQLDBFormat("F29", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CTRL_TOT_TAX, " +
                         "       " + cmnService.J_SQLDBFormat("F30", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CTRL_TDS, " +
                         "       " + cmnService.J_SQLDBFormat("F31", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CTRL_SURCHARGE, " +
                         "       " + cmnService.J_SQLDBFormat("F32", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CTRL_EDU_CESS, " +
                         "       " + cmnService.J_SQLDBFormat("F33", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CTRL_TOT, " +
                         "       " + cmnService.J_SQLDBFormat("F34", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS INTEREST_ALLOCATED, " +
                         "       " + cmnService.J_SQLDBFormat("F35", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS OTHERS_ALLOCATED, " +
                         "       " + cmnService.J_SQLDBFormat("F36", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS CHEQUE_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F37", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS BOOK_ENTRY, " +
                         "       " + cmnService.J_SQLDBFormat("F38", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS PENDING_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F40", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS FEE, " +
                         "       " + cmnService.J_SQLDBFormat("F41", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS MINOR_CODE " +
                         "FROM [" + strImporttableName + "] WHERE F2 = 'CD'";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #region UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "
                strSQL = "UPDATE " + strImporttableName + " SET F28 = '' WHERE F28 = 'NULL'";
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region Insert " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "

                //string[,] strDeductedDate = {{"F24 IS NULL" , "F", "F24", "F"},
                //                             {"F24 IS NOT NULL " , "F", "DATESERIAL(RIGHT(F24,4),MID(F24,3,2),LEFT(F24,2))", "F"}};

                //string[,] strPaymentDate = {{"F23 IS NULL" , "F", "F23", "F"},
                //                             {"F23 IS NOT NULL " , "F", "DATESERIAL(RIGHT(F23,4),MID(F23,3,2),LEFT(F23,2))", "F"}};
                string[,] strDeductedDate = {{"F24 IS NULL" , "F", "F24", "F"},
                                             {"F24 = ''" , "F", "NULL", "F"},
                                             {"F24 IS NOT NULL " , "F", ConvertSQLDate("F24"), "F"}};

                string[,] strPaymentDate = {{"F23 IS NULL" , "F", "F23", "F"},
                                             {"F23 = ''" , "F", "NULL", "F"},
                                             {"F23 IS NOT NULL " , "F", ConvertSQLDate("F23"), "F"}};

                string[,] strChallanPaymentDate = {{"F37 IS NULL" , "F", "F37", "F"},
                                             {"F37 = ''" , "F", "NULL", "F"},
                                             {"F37 IS NOT NULL " , "F", ConvertSQLDate("F37"), "F"}};


                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + " (" +
                         "       CHALLAN_SL_NO, " +
                         "       SL_NO, " +
                         "       MODE, " +
                         //"       EMPLOYEE_SERIAL_NO, " +
                         "       DEDUCTEE_CODE, " +
                         "       DEDUCTEE_PAN, " +
                         "       DEDUCTEE_PAN_REF, " +
                         "       DEDUCTEE_NAME, " +
                         "       TAX_AMOUNT, " +
                         "       SURCHARGE_AMOUNT, " +
                         "       CESS_AMOUNT, " +
                         "       TOTAL_AMOUNT, " +
                         "       TAX_DEPOSITED_AMOUNT, " +
                         "       TOT_VALUE_PURCHASE, " +
                         "       PAYMENT_AMOUNT, " +
                         "       PAYMENT_DATE, " +
                         "       DEDUCTED_DATE, " +
                         "       RATE, " +
                         "       GROSSING_UP_INDICATOR, " +
                         "       CASH_BOOK_ENTRY, " +
                         "       NON_DEDUCTION_FLAG, " +
                         "       INVALID_PAN, " +
                         "       PAN_COUNTER," +
                         "       SECTION_NO," +
                         "       CERTIFICATE_NO," +
                         "       TDS_APPLICABILITY_CODE," +
                         "       REMITTANCE_CODE," +
                         "       UNIQUE_ACKN," +
                         "       COUNTRY_CODE," +
                         "       EMAIL," +
                         "       MOBILE_NO," +
                         "       DEDUCTEE_ADDRESS," +
                         "       DEDUCTEE_TAX_ID," +
                         "       SECTION_115BAC_FLAG ";
                if (strFormNo != T_FormNo.F27Q)
                    strSQL = strSQL + ",TDS_CLAUSE_CHALLAN_NO," +
                         "       TDS_CLAUSE_PAYMENT_DATE ";
                strSQL = strSQL + ")SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CHALLAN_SL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F6", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS MODE, " +
                         //"       " + cmnService.J_SQLDBFormat("F7", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS EMPLOYEE_SERIAL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F8", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DEDUCTEE_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F10", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_PAN, " +
                         "       " + cmnService.J_SQLDBFormat("F12", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_PAN_REF, " +
                         "       " + cmnService.J_SQLDBFormat("F13", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_NAME, " +
                         "       " + cmnService.J_SQLDBFormat("F14", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TAX_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F15", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SURCHARGE_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F16", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CESS_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F17", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TOTAL_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F19", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TAX_DEPOSITED_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F21", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TOT_VALUE_PURCHASE, " +
                         "       " + cmnService.J_SQLDBFormat("F22", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS PAYMENT_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat(strPaymentDate, J_SQLColFormat.Case_End, J_ElsePart.YES) + "  AS PAYMENT_DATE, " +
                         "       " + cmnService.J_SQLDBFormat(strDeductedDate, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS DEDUCTED_DATE, " +
                         "       " + cmnService.J_SQLDBFormat("F26", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS RATE, " +
                         "       " + cmnService.J_SQLDBFormat("F27", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS GROSSING_UP_INDICATOR, " +
                         "       " + cmnService.J_SQLDBFormat("F28", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS CASH_BOOK_ENTRY, " +
                         "       " + cmnService.J_SQLDBFormat("F30", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS NON_DEDUCTION_FLAG, " +
                         "       " + cmnService.J_SQLDBFormat("F31", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS INVALID_PAN, " +
                         "       " + cmnService.J_SQLDBFormat("F32", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS PAN_COUNTER, " +
                         "       " + cmnService.J_SQLDBFormat("F34", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS SECTION_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F35", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS CERTIFICATE_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F36", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TDS_RATE, " +
                         "       " + cmnService.J_SQLDBFormat("F37", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS REMITTANCE_NATURE, " +
                         "       " + cmnService.J_SQLDBFormat("F38", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CA, " +
                         "       " + cmnService.J_SQLDBFormat("F39", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS COUNTRY, " +
                         "       " + cmnService.J_SQLDBFormat("F40", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMAIL, " +
                         "       " + cmnService.J_SQLDBFormat("F41", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS MOBILE_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F42", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DEDUCTEE_ADDRESS, " +
                         "       " + cmnService.J_SQLDBFormat("F43", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DEDUCTEE_TAX_ID, " +
                         "       " + cmnService.J_SQLDBFormat("F55", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS SECTION_115BAC_FLAG ";
                if (strFormNo != T_FormNo.F27Q)
                    strSQL = strSQL + " ," + cmnService.J_SQLDBFormat("F36", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS TDS_CLAUSE_CHALLAN_NO, " +
                     "       " + cmnService.J_SQLDBFormat(strChallanPaymentDate, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS TDS_CLAUSE_PAYMENT_DATE   ";
                strSQL = strSQL + "FROM [" + strImporttableName + "] WHERE F2 = 'DD'";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #endregion
                //MessageBox.Show("1.4");
                #region Insert Data To Correction Tables - BATCH, COMPANY, CHALLAN, DEDUCTEE & SALARY DETAILS

                #region Insert data to COR_HDR_BATCH

                IDataReader dtrFH;
                dtrFH = dmlService.J_ExecSqlReturnReader("SELECT FILE_CREATION_DATE,HASH_VALUE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "");
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

                string SoftwareVersion = TDSMAN.Classes.TDSMAN.T_pEditionType + "." + TDSMAN.Classes.TDSMAN.T_pPackageFAYear;
                strSQL = "INSERT INTO COR_HDR_BATCH (ASST_ID," +
                         "                           FORM_NO," +
                         "                           QTR," +
                         "                           IMPORTED_DATE," +
                         "                           PREVIOUS_RRR_NO," +
                         "                           ORIGINAL_RRR_NO," +
                         "                           FILE_DATE," +
                         "                           HASH_VALUE," +
                         "                           TDS_FILE_PATH, " +
                         "                           INVALID_RETURN, " +
                         "                           VERSION_NO) " +
                         "SELECT ASST_ID                             AS ASST_ID," +
                         "       FORM_NO                             AS FORM_NO," +
                         "       QTR                                 AS QTR," +
                         "  '" + System.DateTime.Now.ToString() + "' AS IMPORTED_DATE," +
                         "       PREVIOUS_RRR_NO                     AS PREVIOUS_RRR_NO," +
                         "       ORIGINAL_RRR_NO                     AS ORIGINAL_RRR_NO," +
                         //"   " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strFileCreationDate) + cmnService.J_DateOperator() + " AS FILE_DATE," +
                         "  '" + strFileCreationDateDDMMYYY + "' AS FILE_DATE," +
                         "  '" + strFileHash + "'                    AS HASH_VALUE," +
                         "  '" + OutputFilePath + "'                 AS TDS_FILE_PATH," +
                         "   " + intDefaultInvalidReturnValue + "    AS INVALID_RETURN," +
                         "   " + SoftwareVersion + "                 AS VERSION_NO " +
                         "FROM  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ",MST_ASSESSMENT " +
                         "WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ASST_YEAR = MST_ASSESSMENT.ASST_YEAR";

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
                //-- ANIK @ 2015/10/01... CONSO FILE > REGISTRATION ID IN PLACE OF P_PAN
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + " SET P_PAN = '' WHERE LEN(P_PAN) > 10";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //--
                string[,] strADDRESS_CHANGE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS_CHANGE = 'Y'" , "F", "1", "F"},
                                               {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS_CHANGE = 'N'" , "F", "0", "F"}};
                string[,] strP_ADDRESS_CHANGE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS_CHANGE = 'Y'" , "F", "1", "F"},
                                                 {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS_CHANGE = 'N'" , "F", "0", "F"}};
                //--
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
                         "                             EXPECTED_SD_RECORD_NO," +
                         "                             AIN_NO," +
                         "                             P_PAN) " + //-- 2015/08/08
                         "SELECT " + lngBatchID + "         AS BATCH_HEADER_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".TAN_NO           AS TAN_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PAN_NO           AS PAN_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".COMPANY_NAME     AS COMPANY_NAME," +
                         "     " + cmnService.J_SQLDBFormat("MST_CATEGORY.CATEGORY_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS D_CATEGORY_ID," +
                         "     " + cmnService.J_SQLDBFormat("MST_STATE_D.STATE_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS D_STATE_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".BRANCH_DIV       AS BRANCH_DIV," +
                         "     " + cmnService.J_SQLDBFormat("MST_MINISTRY.MINISTRY_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS MINISTRY_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".MINISTRY_OTHER   AS MINISTRY_OTHER," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PAO_CODE         AS PAO_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PAO_REG_NO       AS PAO_REG_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".DDO_CODE         AS DDO_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".DDO_REG_NO       AS DDO_REG_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS1         AS ADDRESS1," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS2         AS ADDRESS2," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS3         AS ADDRESS3," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS4         AS ADDRESS4," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS5         AS ADDRESS5," +
                         "     " + cmnService.J_SQLDBFormat("MST_STATE.STATE_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS STATE_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PIN_CODE         AS PIN_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PHONE            AS PHONE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".STD              AS STD," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".EMAIL            AS EMAIL," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PERSON_NAME      AS PERSON_NAME," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".DESIGNATION      AS DESIGNATION," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS1       AS P_ADDRESS1," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS2       AS P_ADDRESS2," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS3       AS P_ADDRESS3," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS4       AS P_ADDRESS4," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS5       AS P_ADDRESS5," +
                         "     " + cmnService.J_SQLDBFormat("MST_STATE_P.STATE_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS P_STATE_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_PIN_CODE       AS P_PIN_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_PHONE          AS P_PHONE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_STD            AS P_STD," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_EMAIL          AS P_EMAIL," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_MOBILE         AS P_MOBILE," +
                         "     " + cmnService.J_SQLDBFormat(strADDRESS_CHANGE, J_SQLColFormat.Case_End) + " AS ADDRESS_CHANGE," +
                         "     " + cmnService.J_SQLDBFormat(strP_ADDRESS_CHANGE, J_SQLColFormat.Case_End) + " AS P_ADDRESS_CHANGE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".EXPECTED_CHALLAN_RECORD_NO AS EXPECTED_CHALLAN_RECORD_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".EXPECTED_SD_RECORD_NO      AS EXPECTED_SD_RECORD_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".AIN                        AS AIN," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_PAN                      AS P_PAN " + //-- 2015/08/08
                         "FROM  ((((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + " LEFT JOIN MST_CATEGORY ON RTRIM(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".CATEGORY_CODE) = MST_CATEGORY.CATEGORY_CODE) " +
                         "LEFT JOIN MST_STATE AS MST_STATE_D ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".D_STATE_CODE = MST_STATE_D.STATE_CODE) " +
                         "LEFT JOIN MST_STATE AS MST_STATE_P ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_STATE_CODE = MST_STATE_P.STATE_CODE) " +
                         "LEFT JOIN MST_STATE AS MST_STATE   ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".STATE_CODE   = MST_STATE.STATE_CODE) " +
                         "LEFT JOIN MST_MINISTRY ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".MINISTRY_CODE = MST_MINISTRY.MINISTRY_CODE";
                //strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + " SET P_PAN = '' WHERE LEN(P_PAN) > 10";

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
                         "                             EXPECTED_SD_RECORD_NO," +
                         "                             AIN_NO," +
                         "                             P_PAN) " + //-- 2015/08/08
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
                         "        EXPECTED_SD_RECORD_NO      AS EXPECTED_SD_RECORD_NO," +
                         "        AIN_NO                     AS AIN_NO," +
                         "        P_PAN                      AS P_PAN " + //-- 2015/08/08
                         "FROM  COR_HDR_COMPANY " +
                         "WHERE BATCH_HEADER_ID = " + lngBatchID;

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;
                #endregion

                #region Insert data to COR_HDR_CHALLAN
                //--
                #region GET FA YEAR
                strSQL = "SELECT ASST_ID FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBatchID;
                long lngAsstID = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                #endregion
                //--

                string[,] strBOOK_ENTRY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".BOOK_ENTRY = 'Y'" , "F", "1", "F"},
                                           {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".BOOK_ENTRY = 'N'" , "F", "0", "F"},
                                           {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".BOOK_ENTRY = ''" , "F", "2", "F"}};

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
                         "                             PENDING_AMOUNT," +
                         "                             LATE_FEE," +
                         "                             MINOR_HEAD_ID) " +
                         "SELECT " + lngBatchID + "                    AS BATCH_HEADER_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".SL_NO                       AS SL_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".EXPECTED_DEDUCTEE_RECORD_NO AS EXPECTED_DEDUCTEE_RECORD_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CHALLAN_STATUS              AS CHALLAN_STATUS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CHALLAN_NO                  AS CHALLAN_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".TRANSFER_VOUCHER_NO         AS TRANSFER_VOUCHER_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".BSR_CODE                    AS BSR_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".DEPOSIT_DATE                AS DEPOSIT_DATE," +
                         "         " + cmnService.J_SQLDBFormat("MST_SECTION.SECTION_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS SECTION_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".TDS                         AS TDS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".SURCHARGE                   AS SURCHARGE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".EDUCATION_CESS              AS EDUCATION_CESS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".INTEREST                    AS INTEREST," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".OTHERS                      AS OTHERS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".TOT_TAX                     AS TOT_TAX," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CTRL_TOT_TAX                AS CTRL_TOT_TAX," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CTRL_TDS                    AS CTRL_TDS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CTRL_SURCHARGE              AS CTRL_SURCHARGE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CTRL_EDU_CESS               AS CTRL_EDU_CESS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CTRL_TOT                    AS CTRL_TOT," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".INTEREST_ALLOCATED          AS INTEREST_ALLOCATED," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".OTHERS_ALLOCATED            AS OTHERS_ALLOCATED," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CHEQUE_NO                   AS CHEQUE_NO," +
                         "     " + cmnService.J_SQLDBFormat(strBOOK_ENTRY, J_SQLColFormat.Case_End) + " AS BOOK_ENTRY," +
                         "         1                                   AS IMPORT_FLAG," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".PENDING_AMOUNT              AS PENDING_AMOUNT," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".FEE                         AS FEE," +
                         "       " + cmnService.J_SQLDBFormat("MST_MINOR_HEAD.MINOR_HEAD_ID", J_ColumnType.Long, J_SQLColFormat.NullCheck) + "    AS MINOR_HEAD_ID " +
                         "FROM ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + " LEFT JOIN MST_SECTION " +
                         "      ON RTRIM(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".SECTION_NAME) = MST_SECTION.SECTION_NAME) " +
                         "      LEFT JOIN MST_MINOR_HEAD " +
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".MINOR_CODE = MST_MINOR_HEAD.MINOR_HEAD_CODE) ";
                if (lngAsstID <= T_FinancialYearID.F2012_13ID)
                    strSQL = strSQL + " WHERE MST_SECTION.DIFF_RATES = 0 ";//-- 2020/07/02 ANIKWA...
                strSQL = strSQL + " ORDER BY SL_NO"; 

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
                         "                             PENDING_AMOUNT," +
                         "                             LATE_FEE," +
                         "                             MINOR_HEAD_ID) " +
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
                         "        PENDING_AMOUNT              AS PENDING_AMOUNT," +
                         "        LATE_FEE                    AS LATE_FEE," +
                         "        MINOR_HEAD_ID               AS MINOR_HEAD_ID " +
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
                string[,] strCASH_BOOK_ENTRY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".CASH_BOOK_ENTRY = 'Y'" , "F", "1", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".CASH_BOOK_ENTRY = 'N'" , "F", "0", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".CASH_BOOK_ENTRY = ''" , "F", "0", "F"}};

                string[,] strINVALID_PAN = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".INVALID_PAN = 'N'" , "F", "1", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".INVALID_PAN = 'Y'" , "F", "0", "F"}};

                string[,] strSECTION_115BAC_FLAGDD = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".SECTION_115BAC_FLAG = 'N'" , "F", "2", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".SECTION_115BAC_FLAG = 'Y'" , "F", "1", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".SECTION_115BAC_FLAG = ''" , "F", "0", "F"}};
                //
                int intAsstID = 0;
                strSQL = @"SELECT ASST_ID FROM MST_ASSESSMENT WHERE FA_YEAR = '" + FinancialYear + "'";
                if ( cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)))  < T_FinancialYearID.F2026_27ID)
                    intAsstID = T_FinancialYearID.F2025_26ID;
                //

                strSQL = "INSERT INTO COR_HDR_DEDUCTEE_DETAILS (HDR_CHALLAN_ID," +
                         "                                      BATCH_HEADER_ID," +
                         "                                      SL_NO," +
                         "                                      MODE," +
                         "                                      EMPLOYEE_SERIAL_NO," +
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
                    //"                                      NON_DEDUCTION_FLAG," +
                         "                                      REASON_ID," +
                         "                                      INVALID_PAN," +
                         "                                      IMPORT_FLAG," +
                         "                                      PAN_COUNTER," +
                         "                                      SECTION_ID," +
                         "                                      CERTIFICATE_NO," +
                         "                                      REMITTANCE_ID," +
                         "                                      UNIQUE_ACKN," +
                         "                                      TDS_APPLICABILITY_ID," +
                         "                                      COUNTRY_ID," +
                         "                                      EMAIL," +
                         "                                      MOBILE_NO," +
                         "                                      DEDUCTEE_ADDRESS," +
                         "                                      DEDUCTEE_TAX_ID," +
                         "                                      TDS_CLAUSE_CHALLAN_NO," +
                         "                                      TDS_CLAUSE_PAYMENT_DATE," +
                         "                                      SECTION_115BAC_FLAG) " +
                         "SELECT CHALLAN_SL_NO          AS HDR_CHALLAN_ID," +
                         "    " + lngBatchID + "        AS BATCH_HEADER_ID," +
                         "        SL_NO                 AS SL_NO," +
                         "        MODE                  AS MODE," +
                         "        EMPLOYEE_SERIAL_NO    AS EMPLOYEE_SERIAL_NO," +
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
                         "        GROSSING_UP_INDICATOR AS GROSSING_UP_INDICATOR," +
                         "    " + cmnService.J_SQLDBFormat(strCASH_BOOK_ENTRY, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CASH_BOOK_ENTRY," +
                    //"        NON_DEDUCTION_FLAG    AS NON_DEDUCTION_FLAG," +
                         "        MST_REASON.REASON_ID                       AS REASON_ID," +
                         "    " + cmnService.J_SQLDBFormat(strINVALID_PAN, J_SQLColFormat.Case_End) + " AS INVALID_PAN," +
                         "        1                                          AS IMPORT_FLAG," +
                         "        PAN_COUNTER                                AS PAN_COUNTER," +
                         "    " + cmnService.J_SQLDBFormat("MST_SECTION.SECTION_ID", J_ColumnType.Long, J_SQLColFormat.NullCheck) + " AS SECTION_ID," +
                         "        CERTIFICATE_NO                             AS CERTIFICATE_NO," +
                         "    " + cmnService.J_SQLDBFormat("MST_REMITTANCE.REMITTANCE_ID", J_ColumnType.Long, J_SQLColFormat.NullCheck) + " AS REMITTANCE_ID," +
                         "        UNIQUE_ACKN                                AS UNIQUE_ACKN," +
                         "    " + cmnService.J_SQLDBFormat("MST_TDS_APPLICABILITY.TDS_APPLICABILITY_ID", J_ColumnType.Long, J_SQLColFormat.NullCheck) + " AS TDS_APPLICABILITY_ID," +
                         "    " + cmnService.J_SQLDBFormat("MST_COUNTRY.COUNTRY_ID", J_ColumnType.Long, J_SQLColFormat.NullCheck) + " AS COUNTRY_ID," +
                         "        EMAIL                                      AS EMAIL," +
                         "        MOBILE_NO                                  AS MOBILE_NO," +
                         "        DEDUCTEE_ADDRESS                           AS DEDUCTEE_ADDRESS," +
                         "        DEDUCTEE_TAX_ID                            AS DEDUCTEE_TAX_ID," +
                         "        TDS_CLAUSE_CHALLAN_NO                      AS TDS_CLAUSE_CHALLAN_NO," +
                         "        TDS_CLAUSE_PAYMENT_DATE                    AS TDS_CLAUSE_PAYMENT_DATE," +
                         "        " + cmnService.J_SQLDBFormat(strSECTION_115BAC_FLAGDD, J_SQLColFormat.Case_End) + "  AS SECTION_115BAC_FLAG " +
                         "FROM  (((((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + " LEFT JOIN MST_SECTION " +
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".SECTION_NO = MST_SECTION.SECTION_NAME)" +
                         "      LEFT JOIN MST_REASON " +
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".NON_DEDUCTION_FLAG = MST_REASON.REASON)" +
                         "      LEFT JOIN MST_TDS_APPLICABILITY " +
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".TDS_APPLICABILITY_CODE = MST_TDS_APPLICABILITY.TDS_APPLICABILITY_CODE)" +
                         "      LEFT JOIN MST_REMITTANCE " +
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".REMITTANCE_CODE = MST_REMITTANCE.REMITTANCE_CODE)" +
                         "      LEFT JOIN MST_COUNTRY " +
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".COUNTRY_CODE = MST_COUNTRY.COUNTRY_CODE)" +
                         "WHERE MST_REASON.FORM_NO = '" + strFormNo + "' ";
                if (lngAsstID > T_FinancialYearID.F2012_13ID)
                    strSQL = strSQL + " AND   MST_SECTION.FORM_NAME = '" + strFormNo + "' AND MST_SECTION.DIFF_RATES = 0 ";//-- 2016/05/18 ANIKWA...
                if (strLogForm == T_FormNo.F27Q)
                    strSQL = strSQL + " AND MST_REMITTANCE.ASST_ID <= " + T_FinancialYearID.F2025_26ID;// intAsstID + " ";//-- 2026/07/03 ANIKWA...
                strSQL = strSQL + " ORDER BY SL_NO";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //
                if (strFormNo != T_FormNo.F24Q)
                {
                    //UPDATING DEDUCTEE CODE VALUE FROM '1' TO '01'
                    strSQL = "UPDATE COR_HDR_DEDUCTEE_DETAILS " +
                             "SET    DEDUCTEE_CODE = '0' + RIGHT(DEDUCTEE_CODE,1) " +
                             "WHERE  BATCH_HEADER_ID  = " + lngBatchID;

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage("Import Failed!");
                        return false;
                    }
                }

                strSQL = "UPDATE COR_HDR_DEDUCTEE_DETAILS " +
                         "INNER JOIN COR_HDR_CHALLAN " +
                         "ON    COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = COR_HDR_CHALLAN.BATCH_HEADER_ID " +
                         "SET   COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID   = COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                         "WHERE COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID   = COR_HDR_CHALLAN.SL_NO " +
                         "AND   COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = " + lngBatchID + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
                //-- UPDATE SECTION_ID for all

                //-- UPDATE ALL EXISTING DEDUCTEE WITH THE SECTION ID
                strSQL = "UPDATE ((COR_HDR_DEDUCTEE_DETAILS " +
                         "INNER JOIN COR_HDR_CHALLAN " +
                         "ON     COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID  = COR_HDR_CHALLAN.HDR_CHALLAN_ID) " +
                         "INNER JOIN COR_HDR_BATCH " +
                         "ON    COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = COR_HDR_BATCH.BATCH_HEADER_ID) " +
                         "SET   COR_HDR_DEDUCTEE_DETAILS.SECTION_ID       = COR_HDR_CHALLAN.SECTION_ID " +
                         "WHERE COR_HDR_BATCH.ASST_ID                    <= " + T_FinancialYearID.F2012_13ID + " " +
                         "AND   COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = " + lngBatchID;

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
                //--
                #region UPDATE COR_HDR_DEDUCTEE_DETAILS

                //Creation of temp table through SELECT ... INTO ... 
                strSQL = @"SELECT COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID, 
                                  SUM(COR_HDR_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT) AS CTRL_TOT_TAX 
                           INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @"
                           FROM COR_HDR_DEDUCTEE_DETAILS
                           WHERE COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBatchID + @"
                           GROUP BY COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }

                strSQL = @"UPDATE  COR_HDR_CHALLAN
                           INNER JOIN 
                           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @"
                           ON    COR_HDR_CHALLAN.HDR_CHALLAN_ID = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @".HDR_CHALLAN_ID
                           SET   COR_HDR_CHALLAN.CTRL_TOT_TAX   = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @".CTRL_TOT_TAX";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }

                strSQL = @"UPDATE  COR_TRN_CHALLAN
                           INNER JOIN 
                           COR_HDR_CHALLAN
                           ON    COR_HDR_CHALLAN.HDR_CHALLAN_ID  = COR_TRN_CHALLAN.HDR_CHALLAN_ID
                           SET   COR_TRN_CHALLAN.CTRL_TOT_TAX    = COR_HDR_CHALLAN.CTRL_TOT_TAX
                           WHERE COR_TRN_CHALLAN.BATCH_HEADER_ID = " + lngBatchID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }
                //--
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX) == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX;
                    dmlService.J_ExecSql(strSQL);
                    //if (dmlService.J_ExecSql(strSQL) == false)
                    //{
                    //    dmlService.J_Rollback();
                    //    return false;
                    //}
                }

                #endregion

                prgBar.Value = prgBar.Value + 5;

                #endregion

                #region Insert data to COR_TRN_DEDUCTEE_DETAILS

                strSQL = "INSERT INTO COR_TRN_DEDUCTEE_DETAILS (TRN_CHALLAN_ID," +
                         "                                      HDR_DEDUCTEE_DETAIL_ID," +
                         "                                      BATCH_HEADER_ID," +
                         "                                      SL_NO," +
                         "                                      MODE," +
                         "                                      EMPLOYEE_SERIAL_NO," +
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
                    //"                                      NON_DEDUCTION_FLAG " +
                         "                                      REASON_ID," +
                         "                                      SECTION_ID," +
                         "                                      CERTIFICATE_NO," +
                         "                                      REMITTANCE_ID," +
                         "                                      UNIQUE_ACKN," +
                         "                                      TDS_APPLICABILITY_ID, " +
                         "                                      COUNTRY_ID," +
                         "                                      DEDUCTEE_REF," +
                         "                                      EMAIL," +
                         "                                      MOBILE_NO," +
                         "                                      DEDUCTEE_ADDRESS," +
                         "                                      DEDUCTEE_TAX_ID," +
                         "                                      TDS_CLAUSE_CHALLAN_NO," +
                         "                                      TDS_CLAUSE_PAYMENT_DATE," +
                         "                                      SECTION_115BAC_FLAG " +
                         ") " +
                         "SELECT  HDR_CHALLAN_ID         AS TRN_CHALLAN_ID," +
                         "        HDR_DEDUCTEE_DETAIL_ID AS HDR_DEDUCTEE_DETAIL_ID," +
                         "        BATCH_HEADER_ID        AS BATCH_HEADER_ID," +
                         "        SL_NO                  AS SL_NO," +
                         "        MODE                   AS MODE," +
                         "        EMPLOYEE_SERIAL_NO     AS EMPLOYEE_SERIAL_NO," +
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
                    //"        NON_DEDUCTION_FLAG     AS NON_DEDUCTION_FLAG," +
                         "        REASON_ID              AS REASON_ID," +
                         "        SECTION_ID             AS SECTION_ID," +
                         "        CERTIFICATE_NO         AS CERTIFICATE_NO," +
                         "        REMITTANCE_ID          AS REMITTANCE_ID," +
                         "        UNIQUE_ACKN            AS UNIQUE_ACKN," +
                         "        TDS_APPLICABILITY_ID   AS TDS_APPLICABILITY_ID, " +
                         "        COUNTRY_ID             AS COUNTRY_ID, " +
                         "        DEDUCTEE_PAN_REF       AS DEDUCTEE_PAN_REF," +
                         "        EMAIL                  AS EMAIL," +
                         "        MOBILE_NO              AS MOBILE_NO," +
                         "        DEDUCTEE_ADDRESS       AS DEDUCTEE_ADDRESS," +
                         "        DEDUCTEE_TAX_ID        AS DEDUCTEE_TAX_ID," +
                         "        TDS_CLAUSE_CHALLAN_NO  AS TDS_CLAUSE_CHALLAN_NO," +
                         "        TDS_CLAUSE_PAYMENT_DATE AS TDS_CLAUSE_PAYMENT_DATE," +
                         "        SECTION_115BAC_FLAG    AS SECTION_115BAC_FLAG " +
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
                //-- UPDATE ALL EXISTING DEDUCTEE WITH THE SECTION ID
                strSQL = "UPDATE ((COR_TRN_DEDUCTEE_DETAILS " +
                         "INNER JOIN COR_TRN_CHALLAN " +
                         "ON    COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID   = COR_TRN_CHALLAN.TRN_CHALLAN_ID) " +
                         "INNER JOIN COR_HDR_BATCH " +
                         "ON    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = COR_HDR_BATCH.BATCH_HEADER_ID) " +
                         "SET   COR_TRN_DEDUCTEE_DETAILS.SECTION_ID       = COR_TRN_CHALLAN.SECTION_ID " +
                         "WHERE COR_HDR_BATCH.ASST_ID                    <= " + T_FinancialYearID.F2012_13ID + " " +
                         "AND   COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = " + lngBatchID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
                //-- UPDATE MODE TO '' WHERE 'O'
                strSQL = "UPDATE COR_TRN_DEDUCTEE_DETAILS " +
                         "SET    MODE = '' " +
                         "WHERE  MODE = 'O'  " +
                         "AND BATCH_HEADER_ID = " + lngBatchID + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //--
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #endregion
                //MessageBox.Show("1.5");
                #region For Salary Details

                if (strFormNo == T_FormNo.F24Q && strQTR == T_Qtr.Q4)
                {
                    string[,] strF62 = {{"F62 IS NULL" , "F", "NULL", "F"},
                                        {"F62 IS NOT NULL " , "F", ConvertSQLDate("F62"), "F"}};

                    string[,] strF63 = {{"F63 IS NULL" , "F", "NULL", "F"},
                                        {"F63 IS NOT NULL " , "F", ConvertSQLDate("F63"), "F"}};

                    #region Insert " + TDSMAN.Classes.TDSMAN. T_tblTEMP_SD + "
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + " " +
                             "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SL_NO, " +
                             "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS MODE, " + //-- 2017/02/24
                             "       " + cmnService.J_SQLDBFormat("F7", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMPLOYEE_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F8", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMPLOYEE_SERIAL_NO, " + //-- 2020/06/01
                             "       " + cmnService.J_SQLDBFormat("F9", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMPLOYEE_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F10", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS EMPLOYEE_CATEGORY, " +
                             //"           DateSerial(RIGHT(F11,4),MID(F11,3,2),LEFT(F11,2))                                     AS FROM_DATE, " +
                             //"           DateSerial(RIGHT(F12,4),MID(F12,3,2),LEFT(F12,2))                                     AS TO_DATE, " +
                             "           " + ConvertSQLDate("F11") + "                                     AS FROM_DATE, " +
                             "           " + ConvertSQLDate("F12") + "                                     AS TO_DATE, " +
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
                             "       " + cmnService.J_SQLDBFormat("F32", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS PAN_COUNTER, " +
                             "       " + cmnService.J_SQLDBFormat("F35", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TAXABLE_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F36", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS REPORTED_TAXABLE_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F37", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TOTAL_TAX_DEDUCTED_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F38", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS PREVIOUS_TAX_DEDUCTED_TOTAL, " +
                             "       " + cmnService.J_SQLDBFormat("F39", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS TAX_DEDUCTED_HIGHER_RATE, " +
                             "       " + cmnService.J_SQLDBFormat("F40", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS RENT_EXCEEDING_YN, " +
                             "       " + cmnService.J_SQLDBFormat("F41", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_PAN_COUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F42", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_1_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F43", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_1_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F44", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_2_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F45", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_2_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F46", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_3_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F47", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_3_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F48", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_4_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F49", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_4_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F50", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS INTEREST_PAID_TO_LENDER, " +
                             "       " + cmnService.J_SQLDBFormat("F51", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_PAN_COUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F52", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_1_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F53", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_1_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F54", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_2_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F55", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_2_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F56", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_3_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F57", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_3_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F58", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_4_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F59", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_4_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F60", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_YN, " +
                             "       " + cmnService.J_SQLDBFormat("F61", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_NAME, ";
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = strSQL + " CONVERT(DATETIME, F62, 103)    AS SUPER_ANN_FROM_DATE," +
                                          " CONVERT(DATETIME, F63, 103)    AS SUPER_ANN_FROM_DATE,";
                    else
                        strSQL = strSQL + " " + cmnService.J_SQLDBFormat(strF62, J_SQLColFormat.Case_End) + "    AS SUPER_ANN_FROM_DATE," +
                                          " " + cmnService.J_SQLDBFormat(strF63, J_SQLColFormat.Case_End) + "    AS SUPER_ANN_TO_DATE,";
                    //"       " + ConvertSQLDate("F62") + "                                                             AS SUPER_ANN_FROM_DATE, " +
                    //"       " + ConvertSQLDate("F63") + "                                                             AS SUPER_ANN_TO_DATE, " +
                    strSQL = strSQL + "  " + cmnService.J_SQLDBFormat("F64", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS SUPER_ANN_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F65", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_RATE, " +
                             "       " + cmnService.J_SQLDBFormat("F66", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_TAX, " +
                             "       " + cmnService.J_SQLDBFormat("F67", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_INCOME, " +
                             //-- 2019/05/24
                             "       " + cmnService.J_SQLDBFormat("F68", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TS_GS_SEC_17_1, " +
                             "       " + cmnService.J_SQLDBFormat("F69", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TS_GS_SEC_17_2, " +
                             "       " + cmnService.J_SQLDBFormat("F70", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TS_GS_SEC_17_3, " +
                             "       " + cmnService.J_SQLDBFormat("F71", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_5_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F72", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_10_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F73", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_10A_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F74", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_10AA_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F75", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_13A_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F76", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TS_LA_TOTAL, " +
                             "       " + cmnService.J_SQLDBFormat("F77", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_TOTAL_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F78", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS AIS_ITEM_2, " +
                             "       " + cmnService.J_SQLDBFormat("F79", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS REBATE_US_87A_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F80", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SECTION_115BAC_FLAG, " +
                             "       " + cmnService.J_SQLDBFormat("F81", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_14_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F82", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TDS_US_192_2B_AMOUNT " +
                             "FROM [" + strImporttableName + "] WHERE F2 = 'SD'";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    #endregion

                    #region Insert " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + " " +
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

                    #region Insert " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SD_SL_NO, " +
                             "       " + cmnService.J_SQLDBFormat("F6", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS SECTION_ID, " +
                             "       " + cmnService.J_SQLDBFormat("F7", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TOTAL_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F8", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS GROSS_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F9", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS QUALIFYING_AMOUNT " +
                             "FROM [" + strImporttableName + "] WHERE F2 = 'C6A'";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    #endregion

                    #region Insert data to COR_HDR_SALARY_DETAILS
                    
                    string[,] strINVALID_PAN_1 = {{TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INVALID_PAN = 'N'" , "F", "1", "F"},
                                                  {TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INVALID_PAN = ''" , "F", "0", "F"},
                                                  {TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INVALID_PAN = 'Y'" , "F", "0", "F"}};

                    string[,] strTAX_DEDUCTED_HIGHER_RATE_1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TAX_DEDUCTED_HIGHER_RATE = 'N'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TAX_DEDUCTED_HIGHER_RATE = 'Y'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TAX_DEDUCTED_HIGHER_RATE = ''" , "F", "0", "F"}};

                    string[,] strSUPER_ANN_YN = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SUPER_ANN_YN = 'N'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SUPER_ANN_YN = 'Y'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SUPER_ANN_YN = ''" , "F", "0", "F"}};

                    string[,] strRENT_EXCEEDING_YN = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".RENT_EXCEEDING_YN = 'N'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".RENT_EXCEEDING_YN = 'Y'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".RENT_EXCEEDING_YN = ''" , "F", "0", "F"}};

                    string[,] strINTEREST_PAID_TO_LENDER = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INTEREST_PAID_TO_LENDER = 'N'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INTEREST_PAID_TO_LENDER = 'Y'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INTEREST_PAID_TO_LENDER = ''" , "F", "0", "F"}};

                    //-- 2024/04/24
                    string[,] strSECTION_115BAC_FLAG_MATRIX = null;
                    if (Convert.ToInt32(lngAsstID) >= T_FinancialYearID.F2023_24ID)
                    {
                        string[,] strSECTION_115BAC_FLAG2324 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = 'Y'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = 'N'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = ''" , "F", "0", "F"}};
                        strSECTION_115BAC_FLAG_MATRIX = strSECTION_115BAC_FLAG2324;
                    }
                    else
                    {
                        string[,] strSECTION_115BAC_FLAG = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = 'N'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = 'Y'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = ''" , "F", "0", "F"}};
                        strSECTION_115BAC_FLAG_MATRIX = strSECTION_115BAC_FLAG;
                    }
                    //
                    strSQL = "INSERT INTO COR_HDR_SALARY_DETAILS (BATCH_HEADER_ID," +
                             "                                    SL_NO," +
                             "                                    MODE," +
                             "                                    EMPLOYEE_NAME," +
                             "                                    EMPLOYEE_PAN," +
                             "                                    EMPLOYEE_SERIAL_NO," + //-- 2020/06/01
                             "                                    EMPLOYEE_CATEGORY," +
                             "                                    FROM_DATE," +
                             "                                    TO_DATE," +
                             "                                    TS_BALANCE," +
                             "                                    US_16_AGGREGATE," +
                             "                                    INCOME_CHARGEABLE," +
                             //"                                    AIS_Total," +
                             "                                    AIS_ITEM_1," +
                             "                                    GROSS_TOTAL_INCOME," +
                             "                                    CVIA_DED_TOTAL," +
                             "                                    TOTAL_INCOME," +
                             //"                                    TAX_TOTAL_INCOME," +
                             "                                    TAX_TOTAL_INCOME_B4_REBATE," +
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
                             "                                    CVIA_SEC80CCG_DED_AMOUNT," +
                             "                                    CVIA_OTH_DED_TOTAL," +
                             "                                    US_16_EA," +
                             "                                    US_16_TE," +
                             "                                    US_16_IA," +
                             "                                    TAXABLE_AMOUNT," +
                             "                                    REPORTED_TAXABLE_AMOUNT," +
                             "                                    TOTAL_TAX_DEDUCTED_AMOUNT," +
                             "                                    PREVIOUS_TAX_DEDUCTED_TOTAL," +
                             "                                    TAX_DEDUCTED_HIGHER_RATE," +
                             "                                    SUPER_ANN_YN," +
                             "                                    SUPER_ANN_NAME," +
                             "                                    SUPER_ANN_FROM_DATE," +
                             "                                    SUPER_ANN_TO_DATE," +
                             "                                    SUPER_ANN_AMOUNT," +
                             "                                    SUPER_ANN_RATE," +
                             "                                    SUPER_ANN_TAX," +
                             "                                    SUPER_ANN_INCOME," +
                             "                                    RENT_EXCEEDING_YN," +
                             "                                    LANDLORD_PAN_COUNT," +
                             "                                    LANDLORD_1_PAN," +
                             "                                    LANDLORD_1_NAME," +
                             "                                    LANDLORD_2_PAN," +
                             "                                    LANDLORD_2_NAME," +
                             "                                    LANDLORD_3_PAN," +
                             "                                    LANDLORD_3_NAME," +
                             "                                    LANDLORD_4_PAN," +
                             "                                    LANDLORD_4_NAME," +
                             "                                    INTEREST_PAID_TO_LENDER," +
                             "                                    LENDER_PAN_COUNT," +
                             "                                    LENDER_1_PAN," +
                             "                                    LENDER_1_NAME," +
                             "                                    LENDER_2_PAN," +
                             "                                    LENDER_2_NAME," +
                             "                                    LENDER_3_PAN," +
                             "                                    LENDER_3_NAME," +
                             "                                    LENDER_4_PAN," +
                             "                                    LENDER_4_NAME," +
                             "                                    TS_GS_SEC_17_1," +
                             "                                    TS_GS_SEC_17_2," +
                             "                                    TS_GS_SEC_17_3," +
                             "                                    SEC10_5_AMOUNT," +
                             "                                    SEC10_10_AMOUNT," +
                             "                                    SEC10_10A_AMOUNT," +
                             "                                    SEC10_10AA_AMOUNT," +
                             "                                    SEC10_13A_AMOUNT," +
                             "                                    TS_LA_TOTAL," +
                             "                                    SEC10_TOTAL_AMOUNT," +
                             "                                    AIS_ITEM_2," +
                             "                                    REBATE_US_87A_AMOUNT," +
                             "                                    CVIA_SEC80C_DED_TOTAL," +
                             "                                    CVIA_SEC80CCC_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_1B_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_2_DED_AMOUNT," +
                             "                                    CVIA_SEC80D_DED_AMOUNT," +
                             "                                    CVIA_SEC80E_DED_AMOUNT," +
                             "                                    CVIA_SEC80G_DED_AMOUNT," +
                             "                                    CVIA_SEC80TTA_DED_AMOUNT," +
                             "                                    CVIA_SEC80C_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCC_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_1B_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_2_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80D_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80E_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80G_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80G_QUAL_AMOUNT," +
                             "                                    CVIA_SEC80TTA_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80TTA_QUAL_AMOUNT," +
                             "                                    CVIA_OTH_GROSS_AMOUNT," +
                             "                                    CVIA_OTH_QUAL_AMOUNT," +
                             "                                    SECTION_115BAC_FLAG," +
                             "                                    SEC10_14_AMOUNT," +
                             "                                    CVIA_SEC80CCH_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCH_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCH_1_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCH_1_GROSS_AMOUNT," +
                             "                                    TDS_US_192_2B_AMOUNT) " +
                             "SELECT " + lngBatchID + "     AS BATCH_HEADER_ID," +
                             "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO  AS SL_NO," +
                             "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".MODE   AS MODE," +
                             "         EMPLOYEE_NAME         AS EMPLOYEE_NAME," +
                             "         EMPLOYEE_PAN          AS EMPLOYEE_PAN," +
                             "         EMPLOYEE_SERIAL_NO         AS EMPLOYEE_SERIAL_NO," + //-- 2020/06/01
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
                             "       " + cmnService.J_SQLDBFormat("F_80CCG.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS CVIA_SEC80CCG_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_OTHERS.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CVIA_OTH_DED_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_16ii.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS US_16_EA," +
                             "       " + cmnService.J_SQLDBFormat("F_16iii.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS US_16_TE," +
                             "       " + cmnService.J_SQLDBFormat("F_16ia.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS US_16_IA," + //-- 2019/04/20
                             "       " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TAXABLE_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS TAXABLE_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".REPORTED_TAXABLE_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS REPORTED_TAXABLE_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TOTAL_TAX_DEDUCTED_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS TOTAL_TAX_DEDUCTED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".PREVIOUS_TAX_DEDUCTED_TOTAL", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS PREVIOUS_TAX_DEDUCTED_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat(strTAX_DEDUCTED_HIGHER_RATE_1, J_SQLColFormat.Case_End) + "           AS TAX_DEDUCTED_HIGHER_RATE," +
                             "       " + cmnService.J_SQLDBFormat(strSUPER_ANN_YN, J_SQLColFormat.Case_End) + "                         AS SUPER_ANN_YN," +
                             "       " + cmnService.J_SQLDBFormat("SUPER_ANN_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS SUPER_ANN_NAME," +
                             "         SUPER_ANN_FROM_DATE                                                                              AS SUPER_ANN_FROM_DATE," +
                             "         SUPER_ANN_TO_DATE                                                                                AS SUPER_ANN_TO_DATE," +
                             "       " + cmnService.J_SQLDBFormat("SUPER_ANN_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS SUPER_ANN_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("SUPER_ANN_RATE", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS SUPER_ANN_RATE," +
                             "       " + cmnService.J_SQLDBFormat("SUPER_ANN_TAX", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS SUPER_ANN_TAX," +
                             "       " + cmnService.J_SQLDBFormat("SUPER_ANN_INCOME", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS SUPER_ANN_INCOME," +
                             "       " + cmnService.J_SQLDBFormat("RENT_EXCEEDING_YN", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS RENT_EXCEEDING_YN," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_PAN_COUNT", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LANDLORD_PAN_COUNT," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_1_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LANDLORD_1_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_1_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LANDLORD_1_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_2_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LANDLORD_2_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_2_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LANDLORD_2_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_3_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LANDLORD_3_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_3_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LANDLORD_3_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_4_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LANDLORD_4_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_4_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LANDLORD_4_NAME," +
                             "       " + cmnService.J_SQLDBFormat("INTEREST_PAID_TO_LENDER", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS INTEREST_PAID_TO_LENDER," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_PAN_COUNT", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LENDER_PAN_COUNT," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_1_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LENDER_1_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_1_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LENDER_1_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_2_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LENDER_2_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_2_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LENDER_2_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_3_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LENDER_3_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_3_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LENDER_3_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_4_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LENDER_4_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_4_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LENDER_4_NAME," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TS_GS_SEC_17_1", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TS_GS_SEC_17_1," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TS_GS_SEC_17_2", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TS_GS_SEC_17_2," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TS_GS_SEC_17_3", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TS_GS_SEC_17_3," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_5_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SEC10_5_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_10_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_10_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_10A_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS SEC10_10A_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_10AA_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS SEC10_10AA_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_13A_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS SEC10_13A_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TS_LA_TOTAL", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS TS_LA_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS SEC10_TOTAL_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".AIS_ITEM_2", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "         AS AIS_ITEM_2," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".REBATE_US_87A_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS REBATE_US_87A_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F_80C.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80C_DED_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCC.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80CCC_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD1.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CVIA_SEC80CCD_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD_1B.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS CVIA_SEC80CCD_1B_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD2.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CVIA_SEC80CCD_2_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80D.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80D_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80E.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80E_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80G.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80G_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80TTA.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80TTA_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80C.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80C_GROSS_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCC.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80CCC_GROSS_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD1.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CVIA_SEC80CCD_GROSS_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD_1B.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS CVIA_SEC80CCD_1B_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD2.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CVIA_SEC80CCD_2_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80D.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80D_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80E.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80E_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80G.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80G_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80G.QUALIFYING_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS CVIA_SEC80G_QUALIFYING_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80TTA.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80TTA_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80TTA.QUALIFYING_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CVIA_SEC80TTA_QUALIFYING_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_OTHERS.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CVIA_OTH_GROSS_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_OTHERS.QUALIFYING_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CVIA_OTH_QUALIFYING_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat(strSECTION_115BAC_FLAG_MATRIX, J_SQLColFormat.Case_End) + "                      AS SECTION_115BAC_FLAG," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_14_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS SEC10_14_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCH.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80CCH_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCH.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80CCH_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCH_1.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CVIA_SEC80CCH_1_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCH_1.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CVIA_SEC80CCH_1_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TDS_US_192_2B_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS TDS_US_192_2B_AMOUNT " + 
                             "FROM ((((((((((((((((((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + " " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCE') AS F_80CCE " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCE.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCF') AS F_80CCF " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCF.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCG') AS F_80CCG " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCG.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = 'OTHERS') AS F_OTHERS " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_OTHERS.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80C') AS F_80C " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80C.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCC') AS F_80CCC " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCC.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCD(1)') AS F_80CCD1 " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCD1.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCD(2)') AS F_80CCD2 " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCD2.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCD(1B)') AS F_80CCD_1B " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCD_1B.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80D') AS F_80D " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80D.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80E') AS F_80E " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80E.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80G') AS F_80G " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80G.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80TTA') AS F_80TTA " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80TTA.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCH') AS F_80CCH " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCH.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCH(1)') AS F_80CCH_1 " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCH_1.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + " " +
                             "WHERE SECTION_ID = '16(ia)') AS F_16ia " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_16ia.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + " " +
                             "WHERE SECTION_ID = '16(ii)') AS F_16ii " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_16ii.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + " " +
                             "WHERE SECTION_ID = '16(iii)') AS F_16iii " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_16iii.SD_SL_NO)";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET US_16_AGGREGATE = US_16_EA + US_16_TE + US_16_IA WHERE BATCH_HEADER_ID = " + lngBatchID;
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                    //-- 2020/05/13
                    strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET AIS_Total = AIS_ITEM_1 + AIS_ITEM_2 WHERE BATCH_HEADER_ID = " + lngBatchID;
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                    if (lngAsstID >= T_FinancialYearID.F2018_19ID)
                    {
                        //-- 2019/05/25
                        strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT = CVIA_SEC80C_DED_TOTAL + CVIA_SEC80CCC_DED_AMOUNT + CVIA_SEC80CCD_DED_AMOUNT WHERE BATCH_HEADER_ID = " + lngBatchID;
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            cmnService.J_UserMessage(strImportErrorMessage);
                            return false;
                        }
                        //-- 2019/05/25
                        strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET CVIA_DED_TOTAL = CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT + CVIA_SEC80CCD_1B_DED_AMOUNT + CVIA_SEC80CCD_2_DED_AMOUNT + CVIA_SEC80D_DED_AMOUNT + CVIA_SEC80E_DED_AMOUNT + CVIA_SEC80G_DED_AMOUNT + CVIA_SEC80TTA_DED_AMOUNT  WHERE BATCH_HEADER_ID = " + lngBatchID;
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            cmnService.J_UserMessage(strImportErrorMessage);
                            return false;
                        }
                        //-- 2019/06/10
                        strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET AIS_Total = AIS_ITEM_1 + AIS_ITEM_2 WHERE BATCH_HEADER_ID = " + lngBatchID;
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            cmnService.J_UserMessage(strImportErrorMessage);
                            return false;
                        }
                    }

                    #endregion

                    #region Insert data to COR_TRN_SALARY_DETAILS

                    strSQL = "INSERT INTO COR_TRN_SALARY_DETAILS (BATCH_HEADER_ID," +
                             "                                    HDR_SALARY_DETAILS_ID," +
                             "                                    SL_NO," +
                             "                                    MODE," +
                             "                                    EMPLOYEE_NAME," +
                             "                                    EMPLOYEE_PAN," +
                             "                                    EMPLOYEE_SERIAL_NO," + //-- 2020/06/01
                             "                                    EMPLOYEE_CATEGORY," +
                             "                                    FROM_DATE," +
                             "                                    TO_DATE," +
                             "                                    TS_BALANCE," +
                             "                                    US_16_EA," +
                             "                                    US_16_TE," +
                             "                                    US_16_IA," +
                             "                                    US_16_AGGREGATE," +
                             "                                    INCOME_CHARGEABLE," +
                             "                                    AIS_Total," +
                             "                                    GROSS_TOTAL_INCOME," +
                             "                                    CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                             "                                    CVIA_OTH_DED_TOTAL," +
                             "                                    CVIA_DED_TOTAL," +
                             "                                    TOTAL_INCOME," +
                             //"                                    TAX_TOTAL_INCOME," +
                             "                                    TAX_TOTAL_INCOME_B4_REBATE," +
                             "                                    SCHG_TOTAL_INCOME," +
                             "                                    ECESS_TOTAL_INCOME," +
                             "                                    TAX_PAYABLE_AGGREGATE," +
                             "                                    US_89_LESS," +
                             "                                    TAX_PAYABLE," +
                             "                                    TOTAL_TDS_DEDUCTED," +
                             "                                    SHORTFALL_TAX," +
                             "                                    IMPORT_FLAG," +
                             "                                    CVIA_SEC80CCF_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCG_DED_AMOUNT," +
                             "                                    TAXABLE_AMOUNT," +
                             "                                    REPORTED_TAXABLE_AMOUNT," +
                             "                                    TOTAL_TAX_DEDUCTED_AMOUNT," +
                             "                                    PREVIOUS_TAX_DEDUCTED_TOTAL," +
                             "                                    TAX_DEDUCTED_HIGHER_RATE," +
                             "                                    SUPER_ANN_YN," +
                             "                                    SUPER_ANN_NAME," +
                             "                                    SUPER_ANN_FROM_DATE," +
                             "                                    SUPER_ANN_TO_DATE," +
                             "                                    SUPER_ANN_AMOUNT," +
                             "                                    SUPER_ANN_RATE," +
                             "                                    SUPER_ANN_TAX," +
                             "                                    SUPER_ANN_INCOME," +
                             "                                    RENT_EXCEEDING_YN," +
                             "                                    LANDLORD_PAN_COUNT," +
                             "                                    LANDLORD_1_PAN," +
                             "                                    LANDLORD_1_NAME," +
                             "                                    LANDLORD_2_PAN," +
                             "                                    LANDLORD_2_NAME," +
                             "                                    LANDLORD_3_PAN," +
                             "                                    LANDLORD_3_NAME," +
                             "                                    LANDLORD_4_PAN," +
                             "                                    LANDLORD_4_NAME," +
                             "                                    INTEREST_PAID_TO_LENDER," +
                             "                                    LENDER_PAN_COUNT," +
                             "                                    LENDER_1_PAN," +
                             "                                    LENDER_1_NAME," +
                             "                                    LENDER_2_PAN," +
                             "                                    LENDER_2_NAME," +
                             "                                    LENDER_3_PAN," +
                             "                                    LENDER_3_NAME," +
                             "                                    LENDER_4_PAN," +
                             "                                    LENDER_4_NAME," +
                             "                                    TS_GS_SEC_17_1," +
                             "                                    TS_GS_SEC_17_2," +
                             "                                    TS_GS_SEC_17_3," +
                             "                                    SEC10_5_AMOUNT," +
                             "                                    SEC10_10_AMOUNT," +
                             "                                    SEC10_10A_AMOUNT," +
                             "                                    SEC10_10AA_AMOUNT," +
                             "                                    SEC10_13A_AMOUNT," +
                             "                                    TS_LA_TOTAL," +
                             "                                    SEC10_TOTAL_AMOUNT," +
                             "                                    AIS_ITEM_1," +
                             "                                    AIS_ITEM_2," +
                             "                                    REBATE_US_87A_AMOUNT," +
                             "                                    CVIA_SEC80C_DED_TOTAL," +
                             "                                    CVIA_SEC80CCC_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_DED_AMOUNT," +
                             "                                    CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_1B_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_2_DED_AMOUNT," +
                             "                                    CVIA_SEC80D_DED_AMOUNT," +
                             "                                    CVIA_SEC80E_DED_AMOUNT," +
                             "                                    CVIA_SEC80G_DED_AMOUNT," +
                             "                                    CVIA_SEC80TTA_DED_AMOUNT," +
                             "                                    CVIA_SEC80C_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCC_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_1B_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_2_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80D_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80E_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80G_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80G_QUAL_AMOUNT," +
                             "                                    CVIA_SEC80TTA_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80TTA_QUAL_AMOUNT," +
                             "                                    CVIA_OTH_GROSS_AMOUNT," +
                             "                                    CVIA_OTH_QUAL_AMOUNT," +
                             "                                    SECTION_115BAC_FLAG," +
                             "                                    SEC10_14_AMOUNT," +
                             "                                    CVIA_SEC80CCH_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCH_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCH_1_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCH_1_GROSS_AMOUNT," +
                             "                                    TDS_US_192_2B_AMOUNT) " +
                             "SELECT  BATCH_HEADER_ID                AS BATCH_HEADER_ID," +
                             "        HDR_SALARY_DETAILS_ID          AS HDR_SALARY_DETAILS_ID," +
                             "        SL_NO                          AS SL_NO," +
                             "        MODE                           AS MODE," +
                             "        EMPLOYEE_NAME                  AS EMPLOYEE_NAME," +
                             "        EMPLOYEE_PAN                   AS EMPLOYEE_PAN," +
                             "        EMPLOYEE_SERIAL_NO             AS EMPLOYEE_SERIAL_NO," + //-- 2020/06/01
                             "        EMPLOYEE_CATEGORY              AS EMPLOYEE_CATEGORY," +
                             "        FROM_DATE                      AS FROM_DATE," +
                             "        TO_DATE                        AS TO_DATE," +
                             "        TS_BALANCE                     AS TS_BALANCE," +
                             "        US_16_EA                       AS US_16_EA," +
                             "        US_16_TE                       AS US_16_TE," +
                             "        US_16_IA                       AS US_16_IA," +
                             "        US_16_AGGREGATE                AS US_16_AGGREGATE," +
                             "        INCOME_CHARGEABLE              AS INCOME_CHARGEABLE," +
                             "        AIS_Total                      AS AIS_Total," +
                             "        GROSS_TOTAL_INCOME             AS GROSS_TOTAL_INCOME," +
                             "        CVIA_SEC80CCE_TOTAL_DED_AMOUNT AS CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                             "        CVIA_OTH_DED_TOTAL             AS CVIA_OTH_DED_TOTAL," +
                             "        CVIA_DED_TOTAL                 AS CVIA_DED_TOTAL," +
                             "        TOTAL_INCOME                   AS TOTAL_INCOME," +
                             //"        TAX_TOTAL_INCOME               AS TAX_TOTAL_INCOME," +
                             "        TAX_TOTAL_INCOME_B4_REBATE     AS TAX_TOTAL_INCOME," +
                             "        SCHG_TOTAL_INCOME              AS SCHG_TOTAL_INCOME," +
                             "        ECESS_TOTAL_INCOME             AS ECESS_TOTAL_INCOME," +
                             "        TAX_PAYABLE_AGGREGATE          AS TAX_PAYABLE_AGGREGATE," +
                             "        US_89_LESS                     AS US_89_LESS," +
                             "        TAX_PAYABLE                    AS TAX_PAYABLE," +
                             "        TOTAL_TDS_DEDUCTED             AS TOTAL_TDS_DEDUCTED," +
                             "        SHORTFALL_TAX                  AS SHORTFALL_TAX," +
                             "        IMPORT_FLAG                    AS IMPORT_FLAG," +
                             "        CVIA_SEC80CCF_DED_AMOUNT       AS CVIA_SEC80CCF_DED_AMOUNT," +
                             "        CVIA_SEC80CCG_DED_AMOUNT       AS CVIA_SEC80CCG_DED_AMOUNT," +
                             "        TAXABLE_AMOUNT                 AS TAXABLE_AMOUNT," +
                             "        REPORTED_TAXABLE_AMOUNT        AS REPORTED_TAXABLE_AMOUNT," +
                             "        TOTAL_TAX_DEDUCTED_AMOUNT      AS TOTAL_TAX_DEDUCTED_AMOUNT," +
                             "        PREVIOUS_TAX_DEDUCTED_TOTAL    AS PREVIOUS_TAX_DEDUCTED_TOTAL," +
                             "        TAX_DEDUCTED_HIGHER_RATE       AS TAX_DEDUCTED_HIGHER_RATE," +
                             "        SUPER_ANN_YN                   AS SUPER_ANN_YN," +
                             "        SUPER_ANN_NAME                 AS SUPER_ANN_NAME," +
                             "        SUPER_ANN_FROM_DATE            AS SUPER_ANN_FROM_DATE," +
                             "        SUPER_ANN_TO_DATE              AS SUPER_ANN_TO_DATE," +
                             "        SUPER_ANN_AMOUNT               AS SUPER_ANN_AMOUNT," +
                             "        SUPER_ANN_RATE                 AS SUPER_ANN_RATE," +
                             "        SUPER_ANN_TAX                  AS SUPER_ANN_TAX," +
                             "        SUPER_ANN_INCOME               AS SUPER_ANN_INCOME," +
                             "        RENT_EXCEEDING_YN              AS RENT_EXCEEDING_YN," +
                             "        LANDLORD_PAN_COUNT             AS LANDLORD_PAN_COUNT," +
                             "        LANDLORD_1_PAN                 AS LANDLORD_1_PAN," +
                             "        LANDLORD_1_NAME                AS LANDLORD_1_NAME," +
                             "        LANDLORD_2_PAN                 AS LANDLORD_2_PAN," +
                             "        LANDLORD_2_NAME                AS LANDLORD_2_NAME," +
                             "        LANDLORD_3_PAN                 AS LANDLORD_3_PAN," +
                             "        LANDLORD_3_NAME                AS LANDLORD_3_NAME," +
                             "        LANDLORD_4_PAN                 AS LANDLORD_4_PAN," +
                             "        LANDLORD_4_NAME                AS LANDLORD_4_NAME," +
                             "        INTEREST_PAID_TO_LENDER        AS INTEREST_PAID_TO_LENDER," +
                             "        LENDER_PAN_COUNT               AS LENDER_PAN_COUNT," +
                             "        LENDER_1_PAN                   AS LENDER_1_PAN," +
                             "        LENDER_1_NAME                  AS LENDER_1_NAME," +
                             "        LENDER_2_PAN                   AS LENDER_2_PAN," +
                             "        LENDER_2_NAME                  AS LENDER_2_NAME," +
                             "        LENDER_3_PAN                   AS LENDER_3_PAN," +
                             "        LENDER_3_NAME                  AS LENDER_3_NAME," +
                             "        LENDER_4_PAN                   AS LENDER_4_PAN," +
                             "        LENDER_4_NAME                  AS LENDER_4_NAME," +
                             "        TS_GS_SEC_17_1                 AS TS_GS_SEC_17_1," +
                             "        TS_GS_SEC_17_2                 AS TS_GS_SEC_17_2," +
                             "        TS_GS_SEC_17_3                 AS TS_GS_SEC_17_3," +
                             "        SEC10_5_AMOUNT                 AS SEC10_5_AMOUNT," +
                             "        SEC10_10_AMOUNT                AS SEC10_10_AMOUNT," +
                             "        SEC10_10A_AMOUNT               AS SEC10_10A_AMOUNT," +
                             "        SEC10_10AA_AMOUNT              AS SEC10_10AA_AMOUNT," +
                             "        SEC10_13A_AMOUNT               AS SEC10_13A_AMOUNT," +
                             "        TS_LA_TOTAL                    AS TS_LA_TOTAL," +
                             "        SEC10_TOTAL_AMOUNT             AS SEC10_TOTAL_AMOUNT," +
                             "        AIS_ITEM_1                     AS AIS_ITEM_1," +
                             "        AIS_ITEM_2                     AS AIS_ITEM_2," +
                             "        REBATE_US_87A_AMOUNT           AS REBATE_US_87A_AMOUNT," +
                             "        CVIA_SEC80C_DED_TOTAL          AS CVIA_SEC80C_DED_TOTAL," +
                             "        CVIA_SEC80CCC_DED_AMOUNT       AS CVIA_SEC80CCC_DED_AMOUNT," +
                             "        CVIA_SEC80CCD_DED_AMOUNT       AS CVIA_SEC80CCD_DED_AMOUNT," +
                             "        CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT AS CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT," +
                             "        CVIA_SEC80CCD_1B_DED_AMOUNT    AS CVIA_SEC80CCD_1B_DED_AMOUNT," +
                             "        CVIA_SEC80CCD_2_DED_AMOUNT     AS CVIA_SEC80CCD_2_DED_AMOUNT," +
                             "        CVIA_SEC80D_DED_AMOUNT         AS CVIA_SEC80D_DED_AMOUNT," +
                             "        CVIA_SEC80E_DED_AMOUNT         AS CVIA_SEC80E_DED_AMOUNT," +
                             "        CVIA_SEC80G_DED_AMOUNT         AS CVIA_SEC80G_DED_AMOUNT," +
                             "        CVIA_SEC80TTA_DED_AMOUNT       AS CVIA_SEC80TTA_DED_AMOUNT," +
                             "        CVIA_SEC80C_GROSS_AMOUNT       AS CVIA_SEC80C_GROSS_AMOUNT," +
                             "        CVIA_SEC80CCC_GROSS_AMOUNT     AS CVIA_SEC80CCC_GROSS_AMOUNT," +
                             "        CVIA_SEC80CCD_GROSS_AMOUNT     AS CVIA_SEC80CCD_GROSS_AMOUNT," +
                             "        CVIA_SEC80CCD_1B_GROSS_AMOUNT  AS CVIA_SEC80CCD_1B_GROSS_AMOUNT," +
                             "        CVIA_SEC80CCD_2_GROSS_AMOUNT   AS CVIA_SEC80CCD_2_GROSS_AMOUNT," +
                             "        CVIA_SEC80D_GROSS_AMOUNT       AS CVIA_SEC80D_GROSS_AMOUNT," +
                             "        CVIA_SEC80E_GROSS_AMOUNT       AS CVIA_SEC80E_GROSS_AMOUNT," +
                             "        CVIA_SEC80G_GROSS_AMOUNT       AS CVIA_SEC80G_GROSS_AMOUNT," +
                             "        CVIA_SEC80G_QUAL_AMOUNT        AS CVIA_SEC80G_QUAL_AMOUNT," +
                             "        CVIA_SEC80TTA_GROSS_AMOUNT     AS CVIA_SEC80TTA_GROSS_AMOUNT," +
                             "        CVIA_SEC80TTA_QUAL_AMOUNT      AS CVIA_SEC80TTA_QUAL_AMOUNT," +
                             "        CVIA_OTH_GROSS_AMOUNT          AS CVIA_OTH_GROSS_AMOUNT," +
                             "        CVIA_OTH_QUAL_AMOUNT           AS CVIA_OTH_QUAL_AMOUNT," +
                             "        SECTION_115BAC_FLAG            AS SECTION_115BAC_FLAG," +
                             "        SEC10_14_AMOUNT                AS SEC10_14_AMOUNT," +
                             "        CVIA_SEC80CCH_DED_AMOUNT       AS CVIA_SEC80CCH_DED_AMOUNT," +
                             "        CVIA_SEC80CCH_GROSS_AMOUNT     AS CVIA_SEC80CCH_GROSS_AMOUNT," +
                             "        CVIA_SEC80CCH_1_DED_AMOUNT     AS CVIA_SEC80CCH_1_DED_AMOUNT," +
                             "        CVIA_SEC80CCH_1_GROSS_AMOUNT   AS CVIA_SEC80CCH_1_GROSS_AMOUNT," +
                             "        TDS_US_192_2B_AMOUNT           AS TDS_US_192_2B_AMOUNT " +
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

                //-- 2024/11/19
                #region For 194P
                if (strFormNo == T_FormNo.F24Q && strQTR == T_Qtr.Q4)
                {

                }
                #endregion

                //-- 2017/10/30
                #region ENTER REGULAR DEDUCTEE/EMPLOYEE MASTER
                    //--
                    #region COMMENT
                    //                if (strFormNo != T_FormNo.F24Q) //-- ELSE THAN 24Q
                    //                {
                    //                    strSQL = @"SELECT COUNT(*)
                    //                               FROM   COR_TRN_COMPANY INNER JOIN MST_COMPANY
                    //                               ON     COR_TRN_COMPANY.TAN_NO = MST_COMPANY.TAN_NO
                    //                               WHERE  COR_TRN_COMPANY.BATCH_HEADER_ID = " + lngBatchID;
                    //                    if (cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    //                    {
                    //                        strSQL = @"INSERT INTO MST_DEDUCTEE (DEDUCTEE_CODE,
                    //                                                       DEDUCTEE_PAN,
                    //                                                       DEDUCTEE_NAME) 
                    //                                SELECT DISTINCT COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE,
                    //                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN,
                    //                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME
                    //                                FROM  COR_TRN_DEDUCTEE_DETAILS LEFT JOIN 
                    //                                      MST_DEDUCTEE
                    //                                ON    MST_DEDUCTEE.DEDUCTEE_PAN = COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN
                    //                                WHERE MST_DEDUCTEE.DEDUCTEE_PAN IS NULL
                    //                                AND   COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBatchID + @"            
                    //                                ORDER BY COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN,
                    //                                      COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME";
                    //                        if (dmlService.J_ExecSql(strSQL) == false)
                    //                        {
                    //                            dmlService.J_Rollback();
                    //                            cmnService.J_UserMessage(strImportErrorMessage);
                    //                            return false;
                    //                        }
                    //                    }
                    //                }
                    //                else //-- for 24Q
                    //                {
                    //                    strSQL = @"SELECT COUNT(*)
                    //                               FROM   COR_TRN_COMPANY INNER JOIN MST_COMPANY
                    //                               ON     COR_TRN_COMPANY.TAN_NO = MST_COMPANY.TAN_NO
                    //                               WHERE  COR_TRN_COMPANY.BATCH_HEADER_ID = " + lngBatchID;
                    //                    if (cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    //                    {
                    //                        string strCompanyID = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_ID FROM COR_TRN_COMPANY INNER JOIN MST_COMPANY ON COR_TRN_COMPANY.TAN_NO = MST_COMPANY.TAN_NO WHERE COR_TRN_COMPANY.BATCH_HEADER_ID = " + lngBatchID)) ;
                    //                        //--
                    //                        strSQL = @"INSERT INTO MST_EMPLOYEE (EMPLOYEE_PAN,
                    //                                                       EMPLOYEE_NAME,
                    //                                                       COMPANY_ID) 
                    //                                SELECT DISTINCT COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN,
                    //                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME," + 
                    //                                       @" " + strCompanyID + @"
                    //                                FROM  COR_TRN_DEDUCTEE_DETAILS LEFT JOIN 
                    //                                      MST_EMPLOYEE
                    //                                ON    MST_EMPLOYEE.EMPLOYEE_PAN = COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN
                    //                                WHERE MST_EMPLOYEE.EMPLOYEE_PAN IS NULL
                    //                                AND   COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBatchID + @"           
                    //                                ORDER BY COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN,
                    //                                      COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME";
                    //                        if (dmlService.J_ExecSql(strSQL) == false)
                    //                        {
                    //                            dmlService.J_Rollback();
                    //                            cmnService.J_UserMessage(strImportErrorMessage);
                    //                            return false;
                    //                        }
                    //                        //--
                    //                        if (strFormNo == T_FormNo.F24Q && strQTR == T_Qtr.Q4)
                    //                        {
                    //                            //--
                    //                            strSQL = @"INSERT INTO MST_EMPLOYEE (EMPLOYEE_PAN,
                    //                                                       EMPLOYEE_NAME,
                    //                                                       COMPANY_ID) 
                    //                                SELECT DISTINCT COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN,
                    //                                       COR_TRN_SALARY_DETAILS.EMPLOYEE_NAME," +
                    //                                           @" " + strCompanyID + @"
                    //                                FROM  COR_TRN_SALARY_DETAILS LEFT JOIN 
                    //                                      MST_EMPLOYEE
                    //                                ON    MST_EMPLOYEE.EMPLOYEE_PAN = COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN
                    //                                WHERE MST_EMPLOYEE.EMPLOYEE_PAN IS NULL
                    //                                AND   COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID = " + lngBatchID + @"           
                    //                                ORDER BY COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN,
                    //                                      COR_TRN_SALARY_DETAILS.EMPLOYEE_NAME";
                    //                            if (dmlService.J_ExecSql(strSQL) == false)
                    //                            {
                    //                                dmlService.J_Rollback();
                    //                                cmnService.J_UserMessage(strImportErrorMessage);
                    //                                return false;
                    //                            }
                    //                            //--
                    //                        }
                    //                        //--

                    //                    }
                    //                }
                    #endregion
                    //--
                    //TdsMan.CreateRegularDeducteeMasterFromCorrection(strFormNo, strQTR, lngBatchID);
                    //--
                    if (chkAddDeducteesToRegularMaster.Checked == true)  //-- Added By Abhishek Dey On 10/04/2018 --
                    TdsMan.CreateRegularDeducteeMasterFromCorrection(strFormNo, strQTR, lngBatchID);
                //--
                //--
                #endregion
                //
                #region CHANGE MODE TO '' WHERE 'A' 2017/02/24
                //-- UPDATE MODE TO '' WHERE 'A'
                strSQL = "UPDATE COR_HDR_SALARY_DETAILS " +
                         "SET    MODE = '' " +
                         "WHERE  MODE = 'A'  " +
                         "AND BATCH_HEADER_ID = " + lngBatchID + "";
                //-- UPDATE MODE TO '' WHERE 'A' OR 'F' //-- 2019/01/09
                //strSQL = "UPDATE COR_HDR_SALARY_DETAILS " +
                //         "SET    MODE = '' " +
                //         "WHERE  MODE = 'A' OR 'F' " +
                //         "AND BATCH_HEADER_ID = " + lngBatchID + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //-- UPDATE MODE TO '' WHERE 'A'
                strSQL = "UPDATE COR_TRN_SALARY_DETAILS " +
                         "SET    MODE = '' " +
                         "WHERE  MODE = 'A'  " +
                         "AND BATCH_HEADER_ID = " + lngBatchID + "";
                //-- UPDATE MODE TO '' WHERE 'A' //-- 2019/01/09
                //strSQL = "UPDATE COR_TRN_SALARY_DETAILS " +
                //         "SET    MODE = '' " +
                //         "WHERE  MODE = 'A' OR 'F' " +
                //         "AND BATCH_HEADER_ID = " + lngBatchID + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //--
                #endregion
                //MessageBox.Show("1.6");
                #region VERIFYING RECORD COUNTS

                strSQL = "SELECT COUNT(*) AS CHALLAN_MATCHED " +
                         "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ", " +
                         "    (SELECT COUNT(COR_HDR_CHALLAN.HDR_CHALLAN_ID) AS CHALLAN_IMPORTED " +
                         "     FROM   COR_HDR_CHALLAN " +
                         "     WHERE  BATCH_HEADER_ID          = " + lngBatchID + ") AS CHALLAN_CNT " +
                         "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".CHALLAN_RECORDS_COUNT = CHALLAN_CNT.CHALLAN_IMPORTED";

                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) == 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("1" + strImportVerificationMessage);
                    return false;
                }

                strSQL = "SELECT COUNT(*) AS DEDUCTEE_NOT_MATCHED " +
                         "FROM ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + " INNER JOIN COR_HDR_CHALLAN " +
                         "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".SL_NO = COR_HDR_CHALLAN.SL_NO) " +
                         "       LEFT JOIN " +
                         "      (SELECT HDR_CHALLAN_ID, " +
                         "              COUNT(HDR_DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_IMPORTED " +
                         "       FROM   COR_HDR_DEDUCTEE_DETAILS " +
                         "       GROUP BY HDR_CHALLAN_ID) AS DEDUCTEE " +
                         "       ON COR_HDR_CHALLAN.HDR_CHALLAN_ID = DEDUCTEE.HDR_CHALLAN_ID) " +
                         "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".DEDUCTEE_COUNT <> " + cmnService.J_SQLDBFormat("DEDUCTEE.COUNT_DEDUCTEE_IMPORTED", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " " +
                         "AND    COR_HDR_CHALLAN.BATCH_HEADER_ID   = " + lngBatchID;

                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("2" + strImportVerificationMessage);
                    return false;
                }
                //prgBar.Value = prgBar.Value + 5;

                #endregion
                //////MessageBox.Show("1.7");
                #region VERIFYING NULL RECORDS

                strSQL = "SELECT COUNT(*) " +
                    "     FROM   COR_HDR_CHALLAN " +
                    "     WHERE  BATCH_HEADER_ID = " + lngBatchID + " " +
                    "     AND    TOT_TAX IS NULL";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("3" + strImportVerificationMessage);
                    return false;
                }
                //--
                strSQL = "SELECT COUNT(*) " +
                    "     FROM   COR_HDR_DEDUCTEE_DETAILS " +
                    "     WHERE  BATCH_HEADER_ID = " + lngBatchID + " " +
                    "     AND    TOTAL_AMOUNT IS NULL";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("4" + strImportVerificationMessage);
                    return false;
                }
                //-- UPDATE
                strSQL = "SELECT COUNT(*) " +
                    "     FROM   COR_HDR_CHALLAN " +
                    "     WHERE  BATCH_HEADER_ID = " + lngBatchID + " " +
                    "     AND    SECTION_ID IS NULL";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    blnNullSectionFound = true;
                    //--
                    strSQL = "UPDATE COR_HDR_CHALLAN SET SECTION_ID = 0 WHERE BATCH_HEADER_ID = " + lngBatchID + " AND SECTION_ID IS NULL";
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        return false;
                    }
                    //--
                    strSQL = "UPDATE COR_TRN_CHALLAN SET SECTION_ID = 0 WHERE BATCH_HEADER_ID = " + lngBatchID + " AND SECTION_ID IS NULL";
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        return false;
                    }
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion
                ////MessageBox.Show("1.8");
                #region VERIFYING TRANSFER VOUCHER FOR BOOK ENTRY

                strSQL = "SELECT COUNT(*) AS COUNT_RECORDS " +
                    "     FROM   COR_HDR_CHALLAN " +
                    "     WHERE  BOOK_ENTRY          = 1 " +
                    "     AND    TRANSFER_VOUCHER_NO = '' " +
                    "     AND    BATCH_HEADER_ID     = " + lngBatchID + " ";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    strSQL = "UPDATE COR_HDR_CHALLAN SET TRANSFER_VOUCHER_NO = CHALLAN_NO WHERE BOOK_ENTRY = 1 AND TRANSFER_VOUCHER_NO = '' AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "UPDATE COR_HDR_CHALLAN SET CHALLAN_NO = '' WHERE BOOK_ENTRY = 1 AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "UPDATE COR_TRN_CHALLAN SET TRANSFER_VOUCHER_NO = CHALLAN_NO WHERE BOOK_ENTRY = 1 AND TRANSFER_VOUCHER_NO = '' AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "UPDATE COR_TRN_CHALLAN SET CHALLAN_NO = '' WHERE BOOK_ENTRY = 1 AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                ////MessageBox.Show("1.9");
                #region UPDATING PAN_UPDATION_INDICATOR TO '0' //-- 2016/05/28
                //
                strSQL = "UPDATE COR_TRN_SALARY_DETAILS SET PAN_UPDATION_INDICATOR = 0 WHERE BATCH_HEADER_ID = " + lngBatchID + " ";
                dmlService.J_ExecSql(strSQL);
                //                    
                #endregion
                //MessageBox.Show("1.10");
                #region RE CALCULATING CTRL_TOT_TAX BASED ON PENDING AMOUNT

                // Added by Shrey Kejriwal on 25/09/2014

                // Checking whether there is difference in pending amount calculated on the basis of amount
                // and pending amount specified in file

                // And if found updating the ctrl_tot_tax field to show the pending amount difference


                //Difference calculation on the basis on book entry value
                //string[,] strLoadChallanTotTaxMatrix = {{"COR_TRN_CHALLAN.BOOK_ENTRY =  1", "F", "COR_TRN_CHALLAN.TOT_TAX - COR_TRN_CHALLAN.INTEREST_ALLOCATED - COR_TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
                //                                    {"COR_TRN_CHALLAN.BOOK_ENTRY <> 1", "F", "COR_TRN_CHALLAN.TOT_TAX - COR_TRN_CHALLAN.INTEREST_ALLOCATED - COR_TRN_CHALLAN.OTHERS_ALLOCATED - COR_TRN_CHALLAN.LATE_FEE", "F"}};

                //strSQL = "SELECT COUNT(*) " + 
                //         "FROM COR_TRN_CHALLAN " +
                //         "WHERE  " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " - CTRL_TOT_TAX <> PENDING_AMOUNT " +
                //         "AND    CHALLAN_STATUS = 'M' " +
                //         "AND    PENDING_AMOUNT <> 0 " + //-- 2014/10/09
                //         "AND    BOOK_ENTRY = 0 " + //-- 2014/10/09
                //         "AND    BATCH_HEADER_ID = " + lngBatchID;

                //int intCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                //if (intCount > 0)
                //{

                //    strSQL = "UPDATE COR_TRN_CHALLAN " +
                //             "SET    CTRL_TOT_TAX = " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + @" - PENDING_AMOUNT " +
                //             "WHERE  " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + @" - CTRL_TOT_TAX <> PENDING_AMOUNT " +
                //             "AND   CHALLAN_STATUS = 'M' " +
                //             "AND   PENDING_AMOUNT <> 0 " + //-- 2014/10/09
                //             "AND   BOOK_ENTRY = 0 " + //-- 2014/10/09
                //             "AND   BATCH_HEADER_ID = " + lngBatchID;

                //    dmlService.J_ExecSql(strSQL);
                //}   

                #endregion
                //MessageBox.Show("1.11");
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

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                #endregion
                //--
                //-- Added By Abhishek Dey On 08/12/2017 --
                #region Count Of CHALAN Details/ DEDUCTEE Details/ SALARY Details
                //
                int intChallan = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM COR_TRN_CHALLAN WHERE BATCH_HEADER_ID = " + lngBatchID));
                int intDeductee = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + lngBatchID));
                int intSalary = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM COR_TRN_SALARY_DETAILS WHERE BATCH_HEADER_ID = " + lngBatchID));
                #endregion
                //-----------------------------------------
                //-- Added By Abhishek Dey On 30/11/2017 --
                #region Insert data to TRN_TDS_IMPORT_LOG
                strSQL = "SELECT ASST_ID FROM MST_ASSESSMENT WHERE FA_YEAR = '" + strLogFaYear + "' ";
                //
                strLogFaYearId = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //
                if (TdsMan.InsertDataToTrnTdsImportLogTable(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("h:mm:ss tt"), strLogFileCreationDate, strLogTanNo, strLogForm, strLogFaYearId, strLogQuarter, 1, Convert.ToInt32(lngBatchID), intChallan, intDeductee, intSalary) == false)  //-- Modified On 06/12/2017 --
                {
                    dmlService.J_Rollback();
                    return false;
                }
                //
                #endregion
                //-----------------------------------------
                //
                //dmlService.J_Commit();

                return true;
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(strImportErrorMessage);
                //MessageBox.Show(err_handler.Message);
                return false;
            }
        }

        #endregion

        #region ImportTransactionsFromConso2627
        private bool ImportTransactionsFromConso2627(string FVUPath, string FinancialYear)
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
                //
                string strSubStringFunction = "";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSubStringFunction = "SUBSTRING";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSubStringFunction = "MID";
                //
                #endregion
                //MessageBox.Show("1.1");
                //dmlService.J_BeginTransaction();

                #region TDS file Import
                //--
                strImporttableName = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_TDS_FILE_DATA + "";
                //COPY .TDS FILE AS .TXT FILE TO IMPORT DATA TO ACCESS
                if (File.Exists(Application.StartupPath + "\\" + strImporttableName + ".txt") == true)
                    File.Delete(Application.StartupPath + "\\" + strImporttableName + ".txt");
                File.Copy(strFVUPath, Application.StartupPath + "\\" + strImporttableName + ".txt", true);
                //}
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    #region ImportTableName
                    dmlService.J_BeginTransaction();
                    if (dmlService.J_IsDatabaseObjectExist(strImporttableName) == true)
                    {
                        strSQL = "DROP TABLE " + strImporttableName + "";
                        dmlService.J_ExecSql(strSQL);
                    }
                    if (dmlService.J_IsDatabaseObjectExist(strImporttableName) == false)
                    {
                        strSQL = @"CREATE TABLE " + strImporttableName + @" (
                                                " + cmnService.J_GetDataType("F1", J_Identity.YES) + @",
                                                " + cmnService.J_GetDataType("F2", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F3", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F4", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F5", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F6", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F7", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F8", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F9", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F10", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F11", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F12", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F13", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F14", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F15", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F16", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F17", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F18", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F19", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F20", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F21", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F22", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F23", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F24", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F25", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F26", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F27", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F28", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F29", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F30", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F31", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F32", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F33", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F34", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F35", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F36", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F37", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F38", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F39", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F40", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F41", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F42", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F43", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F44", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F45", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F46", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F47", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F48", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F49", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F50", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F51", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F52", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F53", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F54", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F55", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F56", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F57", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F58", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F59", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F60", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F61", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F62", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F63", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F64", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F65", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F66", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F67", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F68", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F69", J_ColumnType.String, 255) + @"," + //-- 2019/05/24
                                                "" + cmnService.J_GetDataType("F70", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F71", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F72", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F73", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F74", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F75", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F76", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F77", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F78", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F79", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F80", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F81", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F82", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F83", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F84", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F85", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F86", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F87", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F88", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F89", J_ColumnType.String, 255) + @")";
                        dmlService.J_ExecSql(strSQL);
                    }
                    dmlService.J_Commit();
                    #endregion
                    //--
                    DataTable dt = new DataTable();
                    //
                    //for (int col = 0; col < 69; col++)
                    //-- 2019/05/24
                    //for (int col = 0; col < 80; col++)
                    for (int col = 0; col < 89; col++)
                        dt.Columns.Add(new DataColumn("F" + (col + 1).ToString()));
                    //
                    GC.Collect();
                    //
                    string[] lines = System.IO.File.ReadAllLines(Application.StartupPath + "\\" + strImporttableName + ".txt");
                    //
                    GC.Collect();
                    //
                    foreach (string line in lines)
                    {
                        try
                        {
                            string[] columns = line.Split('^');
                            dt.Rows.Add(columns);
                        }
                        catch (Exception err)
                        {
                            GC.Collect();
                            continue;
                        }
                    }
                    //
                    dmlService.J_BulkCopyToSqlDatabase(dt, null, strImporttableName);
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    //-- Creation of dump table from .tds file
                    if (T_BulkImportFromTextFile(strImporttableName, false) == false) return false;
                    //--
                }
                //-- DELETION OF DUPLICATE DATA
                if (T_DeleteDuplicateData() == false) return false;
                //
                prgBar.Value = prgBar.Value + 10;
                //Deleting .TXT file
                File.Delete(Application.StartupPath + "\\" + strImporttableName + ".txt");
                // ----------------------------------------------------------
                // -- Added by Shrey Kejriwal on 01-04-2016
                // -- Checking for structure change in CD
                // ----------------------------------------------------------
                //string strChequeValue = string.Empty;
                ////--
                //strSQL = "SELECT TOP 1 F36 " +
                //         "FROM   " + strImporttableName + " " +
                //         "WHERE F2 = 'CD'";
                ////
                ////strChequeValue = dmlService.J_ExecSqlReturnScalar(strSQL).ToString().Trim().ToUpper();
                //strChequeValue = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)).Trim().ToUpper();
                ////
                //if (strChequeValue == "Y" || strChequeValue == "N")
                //{
                //    strSQL = "UPDATE " + strImporttableName + " " +
                //             "SET   F36 = '', " +
                //             "      F37 = F36, " +
                //             "      F38 = F37, " +
                //             "      F39 = F38, " +
                //             "      F40 = F39, " +
                //             "      F41 = F40 " +
                //             "WHERE F2 = 'CD' " +
                //             "AND   (F36 = 'Y' OR F36 = 'N')";

                //    dmlService.J_ExecSql(strSQL);
                //}
                // ----------------------------------------------------------
                #endregion
                //MessageBox.Show("1.2");
                #region Creating Temporary Tables
                if (T_CreateTempTables() == false)
                {
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                #endregion
                //MessageBox.Show("1.3");
                #region Insert Data To Temporary Tables

                #region Insert TEMP_FH

                //Insert data into Temp Tables
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "   " +
                             "SELECT CONVERT(DATETIME, SUBSTRING(F5, 3, 2) + '/' + LEFT(F5, 2) + '/' +  RIGHT(F5, 4), 101) AS FILE_CREATION_DATE," +
                             "       F10 AS FILE_LINES_COUNT," +
                             "       F15 AS HASH_VALUE   " +
                             "FROM [" + strImporttableName + "] WHERE F2 = 'FH'";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "   " +
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

                #region Insert " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "

                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + " (" +
                         "       CHALLAN_RECORDS_COUNT," +
                         "       FORM_NO," +
                         "       ORIGINAL_RRR_NO," +
                         "       PREVIOUS_RRR_NO," +
                         "       TAN_NO," +
                         "       EXPECTED_CHALLAN_RECORD_NO," +
                         "       PAN_NO," +
                         "       ASST_YEAR," +
                         "       FA_YEAR," +
                         "       QTR," +
                         "       COMPANY_NAME," +
                         "       BRANCH_DIV," +
                         "       ADDRESS1," +
                         "       ADDRESS2," +
                         "       ADDRESS3," +
                         "       ADDRESS4," +
                         "       ADDRESS5," +
                         "       STATE_CODE," +
                         "       PIN_CODE," +
                         "       EMAIL," +
                         "       ISD_CODE," +
                         "       PHONE," +
                         "       ADDRESS_CHANGE," +
                         "       CATEGORY_CODE," +
                         "       PERSON_NAME," +
                         "       DESIGNATION," +
                         "       P_ADDRESS1," +
                         "       P_ADDRESS2," +
                         "       P_ADDRESS3," +
                         "       P_ADDRESS4," +
                         "       P_ADDRESS5," +
                         "       P_STATE_CODE," +
                         "       P_PIN_CODE," +
                         "       P_EMAIL," +
                         "       EXPECTED_SD_RECORD_NO," +
                         "       P_ISD_CODE," +
                         "       P_PHONE," +
                         "       P_ADDRESS_CHANGE," +
                         "       TOT_CHALLAN_DEPOSIT," +
                         "       P_MOBILE," +
                         "       COUNT_SD_RECORDS," +
                         "       TOT_GROSS_TOT_INCOME," +
                         "       D_STATE_CODE," +
                         //"       PAO_CODE," +
                         //"       DDO_CODE," +
                         "       MINISTRY_CODE," +
                         "       MINISTRY_OTHER," +
                         "       P_PAN," +
                         //"       PAO_REG_NO," +
                         //"       DDO_REG_NO," +
                         //"       ALT_STD," +
                         //"       ALT_PHONE," +
                         //"       ALT_EMAIL," +
                         //"       ALT_P_STD," +
                         //"       ALT_P_PHONE," +
                         //"       ALT_P_EMAIL," +
                         "       AIN," +
                         "       COUNTRY," +
                         "       P_COUNTRY) " +
                         "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS CHALLAN_RECORDS_COUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS FORM_NO, " +
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
                         "       " + cmnService.J_SQLDBFormat("F29", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ISD_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F30", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PHONE, " +
                         "       " + cmnService.J_SQLDBFormat("F31", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ADDRESS_CHANGE, " +
                         "       " + cmnService.J_SQLDBFormat("F32", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS CATEGORY_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F33", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PERSON_NAME, " +
                         "       " + strSubStringFunction + "(" + cmnService.J_SQLDBFormat("F34", J_ColumnType.String, J_SQLColFormat.NullCheck) + ", 1, 20) AS DESIGNATION, " +
                         "       " + cmnService.J_SQLDBFormat("F35", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS1, " +
                         "       " + cmnService.J_SQLDBFormat("F36", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS2, " +
                         "       " + cmnService.J_SQLDBFormat("F37", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS3, " +
                         "       " + cmnService.J_SQLDBFormat("F38", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS4, " +
                         "       " + cmnService.J_SQLDBFormat("F39", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS5, " +
                         "       " + cmnService.J_SQLDBFormat("F40", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_STATE_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F41", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_PIN_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F42", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_EMAIL, " +
                         "       " + cmnService.J_SQLDBFormat("F43", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS EXPECTED_SD_RECORD_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F44", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ISD_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F45", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_PHONE, " +
                         "       " + cmnService.J_SQLDBFormat("F46", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_ADDRESS_CHANGE, " +
                         "       " + cmnService.J_SQLDBFormat("F47", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TOT_CHALLAN_DEPOSIT, " +
                         "       " + cmnService.J_SQLDBFormat("F48", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_MOBILE, " +
                         "       " + cmnService.J_SQLDBFormat("F49", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS COUNT_SD_RECORDS, " +
                         "       " + cmnService.J_SQLDBFormat("F50", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TOT_GROSS_TOT_INCOME, " +
                         "       " + cmnService.J_SQLDBFormat("F54", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS D_STATE_CODE, " +
                         //"       " + cmnService.J_SQLDBFormat("F55", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PAO_CODE, " +
                         //"       " + cmnService.J_SQLDBFormat("F56", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DDO_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F55", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS MINISTRY_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F56", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS MINISTRY_OTHER, " +
                         "       " + cmnService.J_SQLDBFormat("F59", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_PAN, " +
                         //"       " + cmnService.J_SQLDBFormat("F60", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PAO_REG_NO, " +
                         //"       " + cmnService.J_SQLDBFormat("F61", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DDO_REG_NO, " +
                         //"       " + cmnService.J_SQLDBFormat("F63", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_STD, " +
                         //"       " + cmnService.J_SQLDBFormat("F64", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_PHONE, " +
                         //"       " + cmnService.J_SQLDBFormat("F65", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_EMAIL, " +
                         //"       " + cmnService.J_SQLDBFormat("F66", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_P_STD, " +
                         //"       " + cmnService.J_SQLDBFormat("F67", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_P_PHONE, " +
                         //"       " + cmnService.J_SQLDBFormat("F68", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS ALT_P_EMAIL, " +
                         "       " + cmnService.J_SQLDBFormat("F69", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS AIN, " +
                         "       " + cmnService.J_SQLDBFormat("F70", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS COUNTRY, " +
                         "       " + cmnService.J_SQLDBFormat("F71", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS P_COUNTRY " +
                         "FROM [" + strImporttableName + "] WHERE F2 = 'BH'";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                IDataReader reader;
                //reader = dmlService.J_ExecSqlReturnReader("SELECT FORM_NO,QTR FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ""); 
                //-- Modified On 30/11/2017 By Abhishek -- 
                reader = dmlService.J_ExecSqlReturnReader("SELECT FORM_NO, QTR, TAN_NO, FA_YEAR FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "");
                if (reader == null)
                {
                    reader.Close();
                    reader.Dispose();
                    return false;
                }
                while (reader.Read())
                {
                    //strFormNo = Convert.ToString(reader["FORM_NO"]);
                    //strQTR = Convert.ToString(reader["QTR"]);
                    //-- Modified By Abhishek On 30/11/2017 --
                    strLogForm = strFormNo = Convert.ToString(reader["FORM_NO"]);
                    strLogQuarter = strQTR = Convert.ToString(reader["QTR"]);
                    strLogTanNo = Convert.ToString(reader["TAN_NO"]);
                    strLogFaYear = Convert.ToString(reader["FA_YEAR"]);
                    //----------------------------------------
                }
                reader.Close();
                reader.Dispose();
                #endregion
                //--
                if (strFormNo == T_FormNo.F138_24Q)
                    strFormNo = T_FormNo.F24Q;
                else if (strFormNo == T_FormNo.F140_26Q)
                    strFormNo = T_FormNo.F26Q;
                else if (strFormNo == T_FormNo.F144_27Q)
                    strFormNo = T_FormNo.F27Q;
                else if (strFormNo == T_FormNo.F143_27EQ)
                    strFormNo = T_FormNo.F27EQ;
                //--

                #region Insert " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "

                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "  ( " +
                         "       SL_NO, " +
                         "       DEDUCTEE_COUNT, " +
                         "       NIL_CHALLAN, " +
                         "       EXPECTED_DEDUCTEE_RECORD_NO, " +
                         "       CHALLAN_STATUS, " +
                         "       CHALLAN_NO, " +
                         "       TRANSFER_VOUCHER_NO, " +
                         "       BSR_CODE, " +
                         "       DEPOSIT_DATE," +
                         "       SECTION_NAME, " +
                         "       TDS, " +
                         //"       SURCHARGE, " +
                         //"       EDUCATION_CESS, " +
                         "       INTEREST, " +
                         "       OTHERS, " +
                         "       TOT_TAX, " +
                         "       CTRL_TOT_TAX, " +
                         //"       CTRL_TDS, " +
                         //"       CTRL_SURCHARGE, " +
                         //"       CTRL_EDU_CESS, " +
                         "       CTRL_TOT, " +
                         "       INTEREST_ALLOCATED, " +
                         "       OTHERS_ALLOCATED, " +
                         "       CHEQUE_NO, " +
                         "       BOOK_ENTRY, " +
                         "       PENDING_AMOUNT," +
                         "       FEE," +
                         "       MINOR_CODE) " +
                         "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_COUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F6", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS NIL_CHALLAN, " + //##
                         "       " + cmnService.J_SQLDBFormat("F8", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "    AS EXPECTED_DEDUCTEE_RECORD_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F9", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS CHALLAN_STATUS, " +
                         "       " + cmnService.J_SQLDBFormat("F12", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS CHALLAN_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F14", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS TRANSFER_VOUCHER_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F16", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS BSR_CODE, " +
                         //"           DateSerial(RIGHT(F18,4), MID(F18,3,2),LEFT(F18,2))                                     AS DEPOSIT_DATE," +
                         "       " + ConvertSQLDate("F18") + "                                                             AS DEPOSIT_DATE," +
                         "       " + cmnService.J_SQLDBFormat("F21", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS SECTION_NAME, " +
                         "       " + cmnService.J_SQLDBFormat("F22", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TDS, " +
                         //"       " + cmnService.J_SQLDBFormat("F23", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SURCHARGE, " + //##
                         //"       " + cmnService.J_SQLDBFormat("F24", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS EDUCATION_CESS, " + //##
                         "       " + cmnService.J_SQLDBFormat("F25", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS INTEREST, " +
                         "       " + cmnService.J_SQLDBFormat("F26", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS OTHERS, " +
                         "       " + cmnService.J_SQLDBFormat("F27", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TOT_TAX, " +
                         "       " + cmnService.J_SQLDBFormat("F29", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CTRL_TOT_TAX, " +
                         //"       " + cmnService.J_SQLDBFormat("F30", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CTRL_TDS, " +
                         //"       " + cmnService.J_SQLDBFormat("F31", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CTRL_SURCHARGE, " +
                         //"       " + cmnService.J_SQLDBFormat("F32", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CTRL_EDU_CESS, " +
                         "       " + cmnService.J_SQLDBFormat("F33", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CTRL_TOT, " +
                         "       " + cmnService.J_SQLDBFormat("F34", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS INTEREST_ALLOCATED, " +
                         "       " + cmnService.J_SQLDBFormat("F35", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS OTHERS_ALLOCATED, " +
                         "       " + cmnService.J_SQLDBFormat("F36", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS CHEQUE_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F37", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS BOOK_ENTRY, " +
                         "       " + cmnService.J_SQLDBFormat("F38", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS PENDING_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F40", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS FEE, " +
                         "       " + cmnService.J_SQLDBFormat("F41", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS MINOR_CODE " +
                         "FROM [" + strImporttableName + "] WHERE F2 = 'CD'";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #region UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "
                strSQL = "UPDATE " + strImporttableName + " SET F28 = '' WHERE F28 = 'NULL'";
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region Insert " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "

                //string[,] strDeductedDate = {{"F24 IS NULL" , "F", "F24", "F"},
                //                             {"F24 IS NOT NULL " , "F", "DATESERIAL(RIGHT(F24,4),MID(F24,3,2),LEFT(F24,2))", "F"}};

                //string[,] strPaymentDate = {{"F23 IS NULL" , "F", "F23", "F"},
                //                             {"F23 IS NOT NULL " , "F", "DATESERIAL(RIGHT(F23,4),MID(F23,3,2),LEFT(F23,2))", "F"}};

                string[,] strPaymentDate = {{"F23 IS NULL" , "F", "F23", "F"},
                                             {"F23 = ''" , "F", "NULL", "F"},
                                             {"F23 IS NOT NULL " , "F", ConvertSQLDate("F23"), "F"}};

                string[,] strDeductedDate = {{"F24 IS NULL" , "F", "F24", "F"},
                                             {"F24 = ''" , "F", "NULL", "F"},
                                             {"F24 IS NOT NULL " , "F", ConvertSQLDate("F24"), "F"}};

                string[,] strDepositDate = {{"F25 IS NULL" , "F", "F25", "F"},
                                             {"F25 = ''" , "F", "NULL", "F"},
                                             {"F25 IS NOT NULL " , "F", ConvertSQLDate("F25"), "F"}};

                string[,] strClausePaymentDate = {{"F48 IS NULL" , "F", "F48", "F"},
                                             {"F48 = ''" , "F", "NULL", "F"},
                                             {"F48 IS NOT NULL " , "F", ConvertSQLDate("F48"), "F"}};

                string[,] strChallanPaymentDate = {{"F37 IS NULL" , "F", "F37", "F"},
                                             {"F37 = ''" , "F", "NULL", "F"},
                                             {"F37 IS NOT NULL " , "F", ConvertSQLDate("F37"), "F"}};

                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + " (" +
                         "       CHALLAN_SL_NO, " +
                         "       SL_NO, " +
                         "       MODE, " +
                         //"       EMPLOYEE_SERIAL_NO, " +
                         "       DEDUCTEE_CODE, " +
                         "       DEDUCTEE_PAN, " +
                         "       DEDUCTEE_PAN_REF, " +
                         "       DEDUCTEE_NAME, " +
                         //"       TAX_AMOUNT, " +
                         //"       SURCHARGE_AMOUNT, " +
                         //"       CESS_AMOUNT, " +
                         "       TOTAL_AMOUNT, " +
                         "       TAX_DEPOSITED_AMOUNT, " +
                         "       TOT_VALUE_PURCHASE, " +
                         "       PAYMENT_AMOUNT, " +
                         "       PAYMENT_DATE, " +
                         "       DEDUCTED_DATE, " +
                         "       DATE_OF_DEPOSIT," +
                         "       RATE, " +
                         "       GROSSING_UP_INDICATOR, " +
                         "       CASH_BOOK_ENTRY, " +
                         "       NON_DEDUCTION_FLAG, " +
                         "       INVALID_PAN, " +
                         "       PAN_COUNTER," +
                         "       SECTION_NO," +
                         "       CERTIFICATE_NO," +
                         "       TDS_APPLICABILITY_CODE," +
                         "       REMITTANCE_CODE," +
                         "       UNIQUE_ACKN," +
                         "       COUNTRY_CODE," +
                         "       EMAIL," +
                         "       MOBILE_NO," +
                         "       DEDUCTEE_ADDRESS," +
                         "       DEDUCTEE_TAX_ID," +
                         "       NON_RESIDENT," +
                         "       PERMANENT_ESTABLISHMENT," +
                         "       TDS_CLAUSE_YN," +
                         "       TDS_CLAUSE_CHALLAN_NO," +
                         "       TDS_CLAUSE_PAYMENT_DATE," +
                         "       SEC194N_GREATER_1CRORE_PAYMENT_AMOUNT," +
                         "       SEC194NC_EXCESS_3CRORE," +
                         "       SECTION_115BAC_FLAG ";
                //if (strFormNo != T_FormNo.F27Q)
                    //strSQL = strSQL + ",TDS_CLAUSE_CHALLAN_NO," +
                    //     "       TDS_CLAUSE_PAYMENT_DATE ";
                strSQL = strSQL + ")SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CHALLAN_SL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F6", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS MODE, " +
                         //"       " + cmnService.J_SQLDBFormat("F7", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS EMPLOYEE_SERIAL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F8", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DEDUCTEE_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("F10", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_PAN, " +
                         "       " + cmnService.J_SQLDBFormat("F12", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_PAN_REF, " +
                         "       " + cmnService.J_SQLDBFormat("F13", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS DEDUCTEE_NAME, " +
                         //"       " + cmnService.J_SQLDBFormat("F14", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TAX_AMOUNT, " +
                         //"       " + cmnService.J_SQLDBFormat("F15", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SURCHARGE_AMOUNT, " +
                         //"       " + cmnService.J_SQLDBFormat("F16", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CESS_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F17", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TOTAL_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F19", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TAX_DEPOSITED_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F21", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TOT_VALUE_PURCHASE, " +
                         "       " + cmnService.J_SQLDBFormat("F22", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS PAYMENT_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat(strPaymentDate, J_SQLColFormat.Case_End, J_ElsePart.YES) + "  AS PAYMENT_DATE, " +
                         "       " + cmnService.J_SQLDBFormat(strDeductedDate, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS DEDUCTED_DATE, " +
                         "       " + cmnService.J_SQLDBFormat(strDepositDate, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS DEPOSIT_DATE, " +
                         "       " + cmnService.J_SQLDBFormat("F26", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS RATE, " +
                         "       " + cmnService.J_SQLDBFormat("F27", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS GROSSING_UP_INDICATOR, " +
                         "       " + cmnService.J_SQLDBFormat("F28", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS CASH_BOOK_ENTRY, " +
                         "       " + cmnService.J_SQLDBFormat("F30", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS NON_DEDUCTION_FLAG, " +
                         "       " + cmnService.J_SQLDBFormat("F31", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS INVALID_PAN, " +
                         "       " + cmnService.J_SQLDBFormat("F32", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS PAN_COUNTER, " +
                         "       " + cmnService.J_SQLDBFormat("F34", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS SECTION_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F35", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS CERTIFICATE_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F36", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TDS_RATE, " +
                         "       " + cmnService.J_SQLDBFormat("F37", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS REMITTANCE_NATURE, " +
                         "       " + cmnService.J_SQLDBFormat("F38", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS UNIQUE_ACKN, " +
                         "       " + cmnService.J_SQLDBFormat("F39", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS COUNTRY, " +
                         "       " + cmnService.J_SQLDBFormat("F40", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMAIL, " +
                         "       " + cmnService.J_SQLDBFormat("F41", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS MOBILE_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F42", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DEDUCTEE_ADDRESS, " +
                         "       " + cmnService.J_SQLDBFormat("F43", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DEDUCTEE_TAX_ID, " +
                         "       " + cmnService.J_SQLDBFormat("F44", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS NON_RESIDENT, " +
                         "       " + cmnService.J_SQLDBFormat("F45", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS PERMANENT_ESTABLISHMENT, " +
                         "       " + cmnService.J_SQLDBFormat("F46", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS TDS_CLAUSE_YN, " +
                         "       " + cmnService.J_SQLDBFormat("F47", J_ColumnType.Long, J_SQLColFormat.NullCheck) + "       AS TDS_CLAUSE_CHALLAN_NO, " +
                         "       " + cmnService.J_SQLDBFormat(strClausePaymentDate, J_SQLColFormat.Case_End, J_ElsePart.YES) + "     AS TDS_CLAUSE_PAYMENT_DATE, " +
                         "       " + cmnService.J_SQLDBFormat("F49", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SEC194N_GREATER_1CRORE_PAYMENT_AMOUNT, " +
                         "       " + cmnService.J_SQLDBFormat("F54", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SEC194NC_EXCESS_3CRORE, " +
                         "       " + cmnService.J_SQLDBFormat("F55", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS SECTION_115BAC_FLAG ";
                //if (strFormNo != T_FormNo.F27Q)
                    //strSQL = strSQL + " ," + cmnService.J_SQLDBFormat("F36", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS TDS_CLAUSE_CHALLAN_NO, " +
                    // "       " + cmnService.J_SQLDBFormat(strChallanPaymentDate, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS TDS_CLAUSE_PAYMENT_DATE   ";
                strSQL = strSQL + "FROM [" + strImporttableName + "] WHERE F2 = 'DD'";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #endregion
                //MessageBox.Show("1.4");
                #region Insert Data To Correction Tables - BATCH, COMPANY, CHALLAN, DEDUCTEE & SALARY DETAILS

                #region Insert data to COR_HDR_BATCH

                IDataReader dtrFH;
                dtrFH = dmlService.J_ExecSqlReturnReader("SELECT FILE_CREATION_DATE,HASH_VALUE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "");
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

                string SoftwareVersion = TDSMAN.Classes.TDSMAN.T_pEditionType + "." + TDSMAN.Classes.TDSMAN.T_pPackageFAYear;
                strSQL = "INSERT INTO COR_HDR_BATCH (ASST_ID," +
                         "                           FORM_NO," +
                         "                           QTR," +
                         "                           IMPORTED_DATE," +
                         "                           PREVIOUS_RRR_NO," +
                         "                           ORIGINAL_RRR_NO," +
                         "                           FILE_DATE," +
                         "                           HASH_VALUE," +
                         "                           TDS_FILE_PATH, " +
                         "                           INVALID_RETURN, " +
                         "                           VERSION_NO) " +
                         "SELECT ASST_ID                             AS ASST_ID," +
                         "       FORM_NO                             AS FORM_NO," +
                         "       QTR                                 AS QTR," +
                         "  '" + System.DateTime.Now.ToString() + "' AS IMPORTED_DATE," +
                         "       PREVIOUS_RRR_NO                     AS PREVIOUS_RRR_NO," +
                         "       ORIGINAL_RRR_NO                     AS ORIGINAL_RRR_NO," +
                         //"   " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strFileCreationDate) + cmnService.J_DateOperator() + " AS FILE_DATE," +
                         "  '" + strFileCreationDateDDMMYYY + "' AS FILE_DATE," +
                         "  '" + strFileHash + "'                    AS HASH_VALUE," +
                         "  '" + OutputFilePath + "'                 AS TDS_FILE_PATH," +
                         "   " + intDefaultInvalidReturnValue + "    AS INVALID_RETURN," +
                         "   " + SoftwareVersion + "                 AS VERSION_NO " +
                         "FROM  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ",MST_ASSESSMENT " +
                         "WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ASST_YEAR = MST_ASSESSMENT.ASST_YEAR";

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
                //-- ANIK @ 2015/10/01... CONSO FILE > REGISTRATION ID IN PLACE OF P_PAN
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + " SET P_PAN = '' WHERE LEN(P_PAN) > 10";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //--
                string[,] strADDRESS_CHANGE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS_CHANGE = 'Y'" , "F", "1", "F"},
                                               {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS_CHANGE = 'N'" , "F", "0", "F"}};
                string[,] strP_ADDRESS_CHANGE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS_CHANGE = 'Y'" , "F", "1", "F"},
                                                 {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS_CHANGE = 'N'" , "F", "0", "F"}};
                //--
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
                         "                             EXPECTED_SD_RECORD_NO," +
                         "                             AIN_NO," +
                         "                             P_PAN," +
                         "                             ISD_CODE," +
                         "                             P_ISD_CODE," +
                         "                             COUNTRY," +
                         "                             P_COUNTRY) " + 
                         "SELECT " + lngBatchID + "         AS BATCH_HEADER_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".TAN_NO           AS TAN_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PAN_NO           AS PAN_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".COMPANY_NAME     AS COMPANY_NAME," +
                         "     " + cmnService.J_SQLDBFormat("MST_CATEGORY.CATEGORY_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS D_CATEGORY_ID," +
                         "     " + cmnService.J_SQLDBFormat("MST_STATE_D.STATE_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS D_STATE_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".BRANCH_DIV       AS BRANCH_DIV," +
                         "     " + cmnService.J_SQLDBFormat("MST_MINISTRY.MINISTRY_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS MINISTRY_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".MINISTRY_OTHER   AS MINISTRY_OTHER," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PAO_CODE         AS PAO_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PAO_REG_NO       AS PAO_REG_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".DDO_CODE         AS DDO_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".DDO_REG_NO       AS DDO_REG_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS1         AS ADDRESS1," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS2         AS ADDRESS2," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS3         AS ADDRESS3," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS4         AS ADDRESS4," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ADDRESS5         AS ADDRESS5," +
                         "     " + cmnService.J_SQLDBFormat("MST_STATE.STATE_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS STATE_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PIN_CODE         AS PIN_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PHONE            AS PHONE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".STD              AS STD," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".EMAIL            AS EMAIL," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".PERSON_NAME      AS PERSON_NAME," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".DESIGNATION      AS DESIGNATION," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS1       AS P_ADDRESS1," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS2       AS P_ADDRESS2," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS3       AS P_ADDRESS3," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS4       AS P_ADDRESS4," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ADDRESS5       AS P_ADDRESS5," +
                         "     " + cmnService.J_SQLDBFormat("MST_STATE_P.STATE_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS P_STATE_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_PIN_CODE       AS P_PIN_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_PHONE          AS P_PHONE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_STD            AS P_STD," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_EMAIL          AS P_EMAIL," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_MOBILE         AS P_MOBILE," +
                         "     " + cmnService.J_SQLDBFormat(strADDRESS_CHANGE, J_SQLColFormat.Case_End) + " AS ADDRESS_CHANGE," +
                         "     " + cmnService.J_SQLDBFormat(strP_ADDRESS_CHANGE, J_SQLColFormat.Case_End) + " AS P_ADDRESS_CHANGE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".EXPECTED_CHALLAN_RECORD_NO AS EXPECTED_CHALLAN_RECORD_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".EXPECTED_SD_RECORD_NO      AS EXPECTED_SD_RECORD_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".AIN                        AS AIN," + //-- 2015/08/08
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_PAN                      AS P_PAN," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ISD_CODE                   AS ISD_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_ISD_CODE                 AS P_ISD_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".COUNTRY                    AS COUNTRY," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_COUNTRY                  AS P_COUNTRY " + 
                         "FROM  ((((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + " LEFT JOIN MST_CATEGORY ON RTRIM(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".CATEGORY_CODE) = MST_CATEGORY.CATEGORY_CODE) " +
                         "LEFT JOIN MST_STATE AS MST_STATE_D ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".D_STATE_CODE = MST_STATE_D.STATE_CODE) " +
                         "LEFT JOIN MST_STATE AS MST_STATE_P ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".P_STATE_CODE = MST_STATE_P.STATE_CODE) " +
                         "LEFT JOIN MST_STATE AS MST_STATE   ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".STATE_CODE   = MST_STATE.STATE_CODE) " +
                         "LEFT JOIN MST_MINISTRY ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".MINISTRY_CODE = MST_MINISTRY.MINISTRY_CODE";
                //strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + " SET P_PAN = '' WHERE LEN(P_PAN) > 10";

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
                         "                             EXPECTED_SD_RECORD_NO," +
                         "                             AIN_NO," +
                         "                             P_PAN," + //-- 2015/08/08
                         "                             GSTN," +
                         "                             COUNTRY," +
                         "                             P_COUNTRY," +
                         "                             ISD_CODE," +
                         "                             P_ISD_CODE) " + 
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
                         "        EXPECTED_SD_RECORD_NO      AS EXPECTED_SD_RECORD_NO," +
                         "        AIN_NO                     AS AIN_NO," +
                         "        P_PAN                      AS P_PAN," + //-- 2015/08/08
                         "        GSTN                       AS GSTN," +
                         "        COUNTRY                    AS COUNTRY," +
                         "        P_COUNTRY                  AS P_COUNTRY," +
                         "        ISD_CODE                   AS ISD_CODE," +
                         "        P_ISD_CODE                 AS P_ISD_CODE " + 
                         "FROM  COR_HDR_COMPANY " +
                         "WHERE BATCH_HEADER_ID = " + lngBatchID;

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;
                #endregion

                #region Insert data to COR_HDR_CHALLAN
                //--
                #region GET FA YEAR
                strSQL = "SELECT ASST_ID FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBatchID;
                long lngAsstID = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                #endregion
                //--

                string[,] strBOOK_ENTRY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".BOOK_ENTRY = 'B'" , "F", "1", "F"},
                                           {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".BOOK_ENTRY = 'C'" , "F", "0", "F"},
                                           {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".BOOK_ENTRY = ''" , "F", "0", "F"}};

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
                         "                             PENDING_AMOUNT," +
                         "                             LATE_FEE," +
                         "                             MINOR_HEAD_ID) " +
                         "SELECT " + lngBatchID + "                    AS BATCH_HEADER_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".SL_NO                       AS SL_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".EXPECTED_DEDUCTEE_RECORD_NO AS EXPECTED_DEDUCTEE_RECORD_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CHALLAN_STATUS              AS CHALLAN_STATUS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CHALLAN_NO                  AS CHALLAN_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".TRANSFER_VOUCHER_NO         AS TRANSFER_VOUCHER_NO," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".BSR_CODE                    AS BSR_CODE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".DEPOSIT_DATE                AS DEPOSIT_DATE," +
                         "         " + cmnService.J_SQLDBFormat("MST_SECTION.SECTION_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS SECTION_ID," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".TDS                         AS TDS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".SURCHARGE                   AS SURCHARGE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".EDUCATION_CESS              AS EDUCATION_CESS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".INTEREST                    AS INTEREST," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".OTHERS                      AS OTHERS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".TOT_TAX                     AS TOT_TAX," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CTRL_TOT_TAX                AS CTRL_TOT_TAX," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CTRL_TDS                    AS CTRL_TDS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CTRL_SURCHARGE              AS CTRL_SURCHARGE," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CTRL_EDU_CESS               AS CTRL_EDU_CESS," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CTRL_TOT                    AS CTRL_TOT," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".INTEREST_ALLOCATED          AS INTEREST_ALLOCATED," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".OTHERS_ALLOCATED            AS OTHERS_ALLOCATED," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".CHEQUE_NO                   AS CHEQUE_NO," +
                         "     " + cmnService.J_SQLDBFormat(strBOOK_ENTRY, J_SQLColFormat.Case_End) + " AS BOOK_ENTRY," +
                         "         1                                   AS IMPORT_FLAG," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".PENDING_AMOUNT              AS PENDING_AMOUNT," +
                         "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".FEE                         AS FEE," +
                         "       " + cmnService.J_SQLDBFormat("MST_MINOR_HEAD.MINOR_HEAD_ID", J_ColumnType.Long, J_SQLColFormat.NullCheck) + "    AS MINOR_HEAD_ID " +
                         "FROM ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + " LEFT JOIN MST_SECTION " +
                         "      ON RTRIM(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".SECTION_NAME) = MST_SECTION.SECTION_NAME) " +
                         "      LEFT JOIN MST_MINOR_HEAD " +
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".MINOR_CODE = MST_MINOR_HEAD.MINOR_HEAD_CODE) ";
                if (lngAsstID <= T_FinancialYearID.F2012_13ID)
                    strSQL = strSQL + " WHERE MST_SECTION.DIFF_RATES = 0 ";//-- 2020/07/02 ANIKWA...
                strSQL = strSQL + " ORDER BY SL_NO";

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
                         "                             PENDING_AMOUNT," +
                         "                             LATE_FEE," +
                         "                             MINOR_HEAD_ID) " +
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
                         "        PENDING_AMOUNT              AS PENDING_AMOUNT," +
                         "        LATE_FEE                    AS LATE_FEE," +
                         "        MINOR_HEAD_ID               AS MINOR_HEAD_ID " +
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
                string[,] strCASH_BOOK_ENTRY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".CASH_BOOK_ENTRY = 'Y'" , "F", "1", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".CASH_BOOK_ENTRY = 'N'" , "F", "0", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".CASH_BOOK_ENTRY = ''" , "F", "0", "F"}};

                string[,] strINVALID_PAN = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".INVALID_PAN = 'N'" , "F", "1", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".INVALID_PAN = 'Y'" , "F", "0", "F"}};

                string[,] strSECTION_115BAC_FLAGDD = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".SECTION_115BAC_FLAG = 'N'" , "F", "2", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".SECTION_115BAC_FLAG = 'Y'" , "F", "1", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".SECTION_115BAC_FLAG = ''" , "F", "0", "F"}};
                //
                int intAsstID = 0;
                strSQL = @"SELECT ASST_ID FROM MST_ASSESSMENT WHERE FA_YEAR = '" + FinancialYear + "'";
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) < T_FinancialYearID.F2026_27ID)
                    intAsstID = T_FinancialYearID.F2025_26ID;
                //
                strSQL = "INSERT INTO COR_HDR_DEDUCTEE_DETAILS (HDR_CHALLAN_ID," +
                         "                                      BATCH_HEADER_ID," +
                         "                                      SL_NO," +
                         "                                      MODE," +
                         "                                      EMPLOYEE_SERIAL_NO," +
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
                         "                                      DATE_OF_DEPOSIT," +
                         "                                      RATE," +
                         "                                      GROSSING_UP_INDICATOR," +
                         "                                      CASH_BOOK_ENTRY," +
                         //"                                      NON_DEDUCTION_FLAG," +
                         "                                      REASON_ID," +
                         "                                      INVALID_PAN," +
                         "                                      IMPORT_FLAG," +
                         "                                      PAN_COUNTER," +
                         "                                      SECTION_ID," +
                         "                                      CERTIFICATE_NO," +
                         "                                      REMITTANCE_ID," +
                         "                                      UNIQUE_ACKN," +
                         "                                      TDS_APPLICABILITY_ID," +
                         "                                      COUNTRY_ID," +
                         "                                      EMAIL," +
                         "                                      MOBILE_NO," +
                         "                                      DEDUCTEE_ADDRESS," +
                         "                                      DEDUCTEE_TAX_ID," +
                         "                                      TDS_CLAUSE_CHALLAN_NO," +
                         "                                      TDS_CLAUSE_PAYMENT_DATE," +
                         "                                      SECTION_115BAC_FLAG," +
                         "                                      NON_RESIDENT," +
                         "                                      PERMANENT_ESTABLISHMENT) " +
                         "SELECT CHALLAN_SL_NO          AS HDR_CHALLAN_ID," +
                         "    " + lngBatchID + "        AS BATCH_HEADER_ID," +
                         "        SL_NO                 AS SL_NO," +
                         "        MODE                  AS MODE," +
                         "        EMPLOYEE_SERIAL_NO    AS EMPLOYEE_SERIAL_NO," +
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
                         "        DATE_OF_DEPOSIT       AS DATE_OF_DEPOSIT," +
                         "        RATE                  AS RATE," +
                         "        GROSSING_UP_INDICATOR AS GROSSING_UP_INDICATOR," +
                         "    " + cmnService.J_SQLDBFormat(strCASH_BOOK_ENTRY, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CASH_BOOK_ENTRY," +
                         //"        NON_DEDUCTION_FLAG    AS NON_DEDUCTION_FLAG," +
                         "        MST_REASON.REASON_ID                       AS REASON_ID," +
                         "    " + cmnService.J_SQLDBFormat(strINVALID_PAN, J_SQLColFormat.Case_End) + " AS INVALID_PAN," +
                         "        1                                          AS IMPORT_FLAG," +
                         "        PAN_COUNTER                                AS PAN_COUNTER," +
                         "    " + cmnService.J_SQLDBFormat("MST_SECTION.SECTION_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS SECTION_ID," +
                         "        CERTIFICATE_NO                             AS CERTIFICATE_NO," +
                         "    " + cmnService.J_SQLDBFormat("MST_R.REMITTANCE_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS REMITTANCE_ID," +
                         "        UNIQUE_ACKN                                AS UNIQUE_ACKN," +
                         "    " + cmnService.J_SQLDBFormat("MST_TDS_APPLICABILITY.TDS_APPLICABILITY_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS TDS_APPLICABILITY_ID," +
                         "    " + cmnService.J_SQLDBFormat("MST_COUNTRY.COUNTRY_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS COUNTRY_ID," +
                         "        EMAIL                                      AS EMAIL," +
                         "        MOBILE_NO                                  AS MOBILE_NO," +
                         "        DEDUCTEE_ADDRESS                           AS DEDUCTEE_ADDRESS," +
                         "        DEDUCTEE_TAX_ID                            AS DEDUCTEE_TAX_ID," +
                         "        TDS_CLAUSE_CHALLAN_NO                      AS TDS_CLAUSE_CHALLAN_NO," +
                         "        TDS_CLAUSE_PAYMENT_DATE                    AS TDS_CLAUSE_PAYMENT_DATE," +
                         "        " + cmnService.J_SQLDBFormat(strSECTION_115BAC_FLAGDD, J_SQLColFormat.Case_End) + "  AS SECTION_115BAC_FLAG," +
                         "        NON_RESIDENT                               AS NON_RESIDENT," +
                         "        PERMANENT_ESTABLISHMENT                    AS PERMANENT_ESTABLISHMENT " +
                         "FROM  (((((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + " LEFT JOIN MST_SECTION " +
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".SECTION_NO = MST_SECTION.SECTION_NAME)" +
                         "      LEFT JOIN MST_REASON " +
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".NON_DEDUCTION_FLAG = MST_REASON.REASON)" +
                         "      LEFT JOIN MST_TDS_APPLICABILITY " +
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".TDS_APPLICABILITY_CODE = MST_TDS_APPLICABILITY.TDS_APPLICABILITY_CODE)" +
                         //"      LEFT JOIN MST_REMITTANCE " +
                         //"      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".REMITTANCE_CODE = MST_REMITTANCE.REMITTANCE_CODE)" +
                         "     LEFT JOIN " +
                         "       (" +
                         "           SELECT REMITTANCE_ID, " +
                         "                  REMITTANCE_CODE " +
                         "           FROM MST_REMITTANCE " +
                         "           WHERE VALID_UPTO_ASST_ID = 0 " +
                         "             AND INACTIVE_FLAG = 0 " +
                         "       ) AS MST_R " +
                         "       ON  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".REMITTANCE_CODE = MST_R.REMITTANCE_CODE) " +
                         "      LEFT JOIN MST_COUNTRY " +
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".COUNTRY_CODE = MST_COUNTRY.COUNTRY_CODE)" +
                         "WHERE MST_REASON.FORM_NO = '" + strFormNo + "' ";
                if (lngAsstID > T_FinancialYearID.F2012_13ID)
                    strSQL = strSQL + " AND   MST_SECTION.FORM_NAME = '" + strFormNo + "' AND MST_SECTION.DIFF_RATES = 0 ";//-- 2016/05/18 ANIKWA...
                if (strLogForm == T_FormNo.F144_27Q)
                {
                    //if (lngAsstID >= T_FinancialYearID.F2026_27ID)
                    //    strSQL = strSQL + @" AND (MST_REMITTANCE.REMITTANCE_ID IS NULL OR (MST_REMITTANCE.VALID_UPTO_ASST_ID = 0 AND MST_REMITTANCE.INACTIVE_FLAG = 0))";
                    //else
                    //    strSQL = strSQL + " AND MST_REMITTANCE.ASST_ID = " + T_FinancialYearID.F2025_26ID + " ";//-- 2026/07/03 ANIKWA...
                }
                strSQL = strSQL + " ORDER BY SL_NO";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //
                if (strFormNo != T_FormNo.F24Q)
                {
                    //UPDATING DEDUCTEE CODE VALUE FROM '1' TO '01'
                    strSQL = "UPDATE COR_HDR_DEDUCTEE_DETAILS " +
                             "SET    DEDUCTEE_CODE = '0' + RIGHT(DEDUCTEE_CODE,1) " +
                             "WHERE  BATCH_HEADER_ID  = " + lngBatchID;

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage("Import Failed!");
                        return false;
                    }
                }

                strSQL = "UPDATE COR_HDR_DEDUCTEE_DETAILS " +
                         "INNER JOIN COR_HDR_CHALLAN " +
                         "ON    COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = COR_HDR_CHALLAN.BATCH_HEADER_ID " +
                         "SET   COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID   = COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                         "WHERE COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID   = COR_HDR_CHALLAN.SL_NO " +
                         "AND   COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = " + lngBatchID + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
                //-- UPDATE SECTION_ID for all

                //-- UPDATE ALL EXISTING DEDUCTEE WITH THE SECTION ID
                strSQL = "UPDATE ((COR_HDR_DEDUCTEE_DETAILS " +
                         "INNER JOIN COR_HDR_CHALLAN " +
                         "ON     COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID  = COR_HDR_CHALLAN.HDR_CHALLAN_ID) " +
                         "INNER JOIN COR_HDR_BATCH " +
                         "ON    COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = COR_HDR_BATCH.BATCH_HEADER_ID) " +
                         "SET   COR_HDR_DEDUCTEE_DETAILS.SECTION_ID       = COR_HDR_CHALLAN.SECTION_ID " +
                         "WHERE COR_HDR_BATCH.ASST_ID                    <= " + T_FinancialYearID.F2012_13ID + " " +
                         "AND   COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = " + lngBatchID;

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
                //--
                #region UPDATE COR_HDR_DEDUCTEE_DETAILS

                //Creation of temp table through SELECT ... INTO ... 
                strSQL = @"SELECT COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID, 
                                  SUM(COR_HDR_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT) AS CTRL_TOT_TAX 
                           INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @"
                           FROM COR_HDR_DEDUCTEE_DETAILS
                           WHERE COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBatchID + @"
                           GROUP BY COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }

                strSQL = @"UPDATE  COR_HDR_CHALLAN
                           INNER JOIN 
                           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @"
                           ON    COR_HDR_CHALLAN.HDR_CHALLAN_ID = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @".HDR_CHALLAN_ID
                           SET   COR_HDR_CHALLAN.CTRL_TOT_TAX   = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @".CTRL_TOT_TAX";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }

                strSQL = @"UPDATE  COR_TRN_CHALLAN
                           INNER JOIN 
                           COR_HDR_CHALLAN
                           ON    COR_HDR_CHALLAN.HDR_CHALLAN_ID  = COR_TRN_CHALLAN.HDR_CHALLAN_ID
                           SET   COR_TRN_CHALLAN.CTRL_TOT_TAX    = COR_HDR_CHALLAN.CTRL_TOT_TAX
                           WHERE COR_TRN_CHALLAN.BATCH_HEADER_ID = " + lngBatchID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }
                //--
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX) == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX;
                    dmlService.J_ExecSql(strSQL);
                    //if (dmlService.J_ExecSql(strSQL) == false)
                    //{
                    //    dmlService.J_Rollback();
                    //    return false;
                    //}
                }

                #endregion

                prgBar.Value = prgBar.Value + 5;

                #endregion

                #region Insert data to COR_TRN_DEDUCTEE_DETAILS

                strSQL = "INSERT INTO COR_TRN_DEDUCTEE_DETAILS (TRN_CHALLAN_ID," +
                         "                                      HDR_DEDUCTEE_DETAIL_ID," +
                         "                                      BATCH_HEADER_ID," +
                         "                                      SL_NO," +
                         "                                      MODE," +
                         "                                      EMPLOYEE_SERIAL_NO," +
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
                         "                                      DATE_OF_DEPOSIT," +
                         "                                      RATE," +
                         "                                      GROSSING_UP_INDICATOR," +
                         "                                      CASH_BOOK_ENTRY," +
                         "                                      IMPORT_FLAG," +
                         //"                                      NON_DEDUCTION_FLAG " +
                         "                                      REASON_ID," +
                         "                                      SECTION_ID," +
                         "                                      CERTIFICATE_NO," +
                         "                                      REMITTANCE_ID," +
                         "                                      UNIQUE_ACKN," +
                         "                                      TDS_APPLICABILITY_ID, " +
                         "                                      COUNTRY_ID," +
                         "                                      DEDUCTEE_REF," +
                         "                                      EMAIL," +
                         "                                      MOBILE_NO," +
                         "                                      DEDUCTEE_ADDRESS," +
                         "                                      DEDUCTEE_TAX_ID," +
                         "                                      TDS_CLAUSE_CHALLAN_NO," +
                         "                                      TDS_CLAUSE_PAYMENT_DATE," +
                         "                                      SECTION_115BAC_FLAG," +
                         "                                      NON_RESIDENT," +
                         "                                      PERMANENT_ESTABLISHMENT " +
                         ") " +
                         "SELECT  HDR_CHALLAN_ID         AS TRN_CHALLAN_ID," +
                         "        HDR_DEDUCTEE_DETAIL_ID AS HDR_DEDUCTEE_DETAIL_ID," +
                         "        BATCH_HEADER_ID        AS BATCH_HEADER_ID," +
                         "        SL_NO                  AS SL_NO," +
                         "        MODE                   AS MODE," +
                         "        EMPLOYEE_SERIAL_NO     AS EMPLOYEE_SERIAL_NO," +
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
                         "        DATE_OF_DEPOSIT        AS DATE_OF_DEPOSIT," +
                         "        RATE                   AS RATE," +
                         "        GROSSING_UP_INDICATOR  AS GROSSING_UP_INDICATOR," +
                         "        CASH_BOOK_ENTRY        AS CASH_BOOK_ENTRY," +
                         "        IMPORT_FLAG            AS IMPORT_FLAG," +
                         //"        NON_DEDUCTION_FLAG     AS NON_DEDUCTION_FLAG," +
                         "        REASON_ID              AS REASON_ID," +
                         "        SECTION_ID             AS SECTION_ID," +
                         "        CERTIFICATE_NO         AS CERTIFICATE_NO," +
                         "        REMITTANCE_ID          AS REMITTANCE_ID," +
                         "        UNIQUE_ACKN            AS UNIQUE_ACKN," +
                         "        TDS_APPLICABILITY_ID   AS TDS_APPLICABILITY_ID, " +
                         "        COUNTRY_ID             AS COUNTRY_ID, " +
                         "        DEDUCTEE_PAN_REF       AS DEDUCTEE_PAN_REF," +
                         "        EMAIL                  AS EMAIL," +
                         "        MOBILE_NO              AS MOBILE_NO," +
                         "        DEDUCTEE_ADDRESS       AS DEDUCTEE_ADDRESS," +
                         "        DEDUCTEE_TAX_ID        AS DEDUCTEE_TAX_ID," +
                         "        TDS_CLAUSE_CHALLAN_NO  AS TDS_CLAUSE_CHALLAN_NO," +
                         "        TDS_CLAUSE_PAYMENT_DATE AS TDS_CLAUSE_PAYMENT_DATE," +
                         "        SECTION_115BAC_FLAG    AS SECTION_115BAC_FLAG," +
                         "        NON_RESIDENT           AS NON_RESIDENT," +
                         "        PERMANENT_ESTABLISHMENT AS PERMANENT_ESTABLISHMENT " +
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
                //-- UPDATE ALL EXISTING DEDUCTEE WITH THE SECTION ID
                strSQL = "UPDATE ((COR_TRN_DEDUCTEE_DETAILS " +
                         "INNER JOIN COR_TRN_CHALLAN " +
                         "ON    COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID   = COR_TRN_CHALLAN.TRN_CHALLAN_ID) " +
                         "INNER JOIN COR_HDR_BATCH " +
                         "ON    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = COR_HDR_BATCH.BATCH_HEADER_ID) " +
                         "SET   COR_TRN_DEDUCTEE_DETAILS.SECTION_ID       = COR_TRN_CHALLAN.SECTION_ID " +
                         "WHERE COR_HDR_BATCH.ASST_ID                    <= " + T_FinancialYearID.F2012_13ID + " " +
                         "AND   COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = " + lngBatchID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
                //-- UPDATE MODE TO '' WHERE 'O'
                strSQL = "UPDATE COR_TRN_DEDUCTEE_DETAILS " +
                         "SET    MODE = '' " +
                         "WHERE  MODE = 'O'  " +
                         "AND BATCH_HEADER_ID = " + lngBatchID + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //--
                prgBar.Value = prgBar.Value + 5;

                #endregion

                #endregion
                //MessageBox.Show("1.5");
                #region For Salary Details

                if (strFormNo == T_FormNo.F24Q && strQTR == T_Qtr.Q4)
                {
                    string[,] strF62 = {{"F62 IS NULL" , "F", "NULL", "F"},
                                        {"F62 IS NOT NULL " , "F", ConvertSQLDate("F62"), "F"}};

                    string[,] strF63 = {{"F63 IS NULL" , "F", "NULL", "F"},
                                        {"F63 IS NOT NULL " , "F", ConvertSQLDate("F63"), "F"}};

                    #region Insert " + TDSMAN.Classes.TDSMAN. T_tblTEMP_SD + "
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + " " +
                             "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SL_NO, " +
                             "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS MODE, " + //-- 2017/02/24
                             "       " + cmnService.J_SQLDBFormat("F7", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMPLOYEE_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F8", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMPLOYEE_SERIAL_NO, " + //-- 2020/06/01
                             "       " + cmnService.J_SQLDBFormat("F9", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMPLOYEE_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F10", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS EMPLOYEE_CATEGORY, " +
                             //"           DateSerial(RIGHT(F11,4),MID(F11,3,2),LEFT(F11,2))                                     AS FROM_DATE, " +
                             //"           DateSerial(RIGHT(F12,4),MID(F12,3,2),LEFT(F12,2))                                     AS TO_DATE, " +
                             "           " + ConvertSQLDate("F11") + "                                     AS FROM_DATE, " +
                             "           " + ConvertSQLDate("F12") + "                                     AS TO_DATE, " +
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
                             "       " + cmnService.J_SQLDBFormat("F32", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS PAN_COUNTER, " +
                             "       " + cmnService.J_SQLDBFormat("F35", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TAXABLE_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F36", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS REPORTED_TAXABLE_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F37", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TOTAL_TAX_DEDUCTED_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F38", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS PREVIOUS_TAX_DEDUCTED_TOTAL, " +
                             "       " + cmnService.J_SQLDBFormat("F39", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS TAX_DEDUCTED_HIGHER_RATE, " +
                             "       " + cmnService.J_SQLDBFormat("F40", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS RENT_EXCEEDING_YN, " +
                             "       " + cmnService.J_SQLDBFormat("F41", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_PAN_COUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F42", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_1_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F43", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_1_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F44", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_2_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F45", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_2_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F46", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_3_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F47", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_3_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F48", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_4_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F49", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LANDLORD_4_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F50", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS INTEREST_PAID_TO_LENDER, " +
                             "       " + cmnService.J_SQLDBFormat("F51", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_PAN_COUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F52", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_1_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F53", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_1_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F54", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_2_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F55", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_2_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F56", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_3_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F57", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_3_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F58", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_4_PAN, " +
                             "       " + cmnService.J_SQLDBFormat("F59", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS LENDER_4_NAME, " +
                             "       " + cmnService.J_SQLDBFormat("F60", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_YN, " +
                             "       " + cmnService.J_SQLDBFormat("F61", J_ColumnType.String, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_NAME, ";
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = strSQL + " CONVERT(DATETIME, F62, 103)    AS SUPER_ANN_FROM_DATE," +
                                          " CONVERT(DATETIME, F63, 103)    AS SUPER_ANN_FROM_DATE,";
                    else
                        strSQL = strSQL + " " + cmnService.J_SQLDBFormat(strF62, J_SQLColFormat.Case_End) + "    AS SUPER_ANN_FROM_DATE," +
                                          " " + cmnService.J_SQLDBFormat(strF63, J_SQLColFormat.Case_End) + "    AS SUPER_ANN_TO_DATE,";
                    //"       " + ConvertSQLDate("F62") + "                                                             AS SUPER_ANN_FROM_DATE, " +
                    //"       " + ConvertSQLDate("F63") + "                                                             AS SUPER_ANN_TO_DATE, " +
                    strSQL = strSQL + "  " + cmnService.J_SQLDBFormat("F64", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS SUPER_ANN_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F65", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_RATE, " +
                             "       " + cmnService.J_SQLDBFormat("F66", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_TAX, " +
                             "       " + cmnService.J_SQLDBFormat("F67", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_INCOME, " +
                             //-- 2019/05/24
                             "       " + cmnService.J_SQLDBFormat("F68", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TS_GS_SEC_17_1, " +
                             "       " + cmnService.J_SQLDBFormat("F69", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TS_GS_SEC_17_2, " +
                             "       " + cmnService.J_SQLDBFormat("F70", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TS_GS_SEC_17_3, " +
                             "       " + cmnService.J_SQLDBFormat("F71", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_5_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F72", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_10_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F73", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_10A_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F74", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_10AA_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F75", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_13A_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F76", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TS_LA_TOTAL, " +
                             "       " + cmnService.J_SQLDBFormat("F77", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_TOTAL_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F78", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS AIS_ITEM_2, " +
                             "       " + cmnService.J_SQLDBFormat("F79", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS REBATE_US_87A_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F80", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SECTION_115BAC_FLAG, " +
                             "       " + cmnService.J_SQLDBFormat("F81", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_14_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F82", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS TDS_US_192_2B_AMOUNT " +
                             "FROM [" + strImporttableName + "] WHERE F2 = 'SD'";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    #endregion

                    #region Insert " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + " " +
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

                    #region Insert " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SD_SL_NO, " +
                             "       " + cmnService.J_SQLDBFormat("F6", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS SECTION_ID, " +
                             "       " + cmnService.J_SQLDBFormat("F7", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TOTAL_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F8", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS GROSS_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F9", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS QUALIFYING_AMOUNT " +
                             "FROM [" + strImporttableName + "] WHERE F2 = 'C6A'";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    #endregion

                    #region Insert data to COR_HDR_SALARY_DETAILS

                    string[,] strINVALID_PAN_1 = {{TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INVALID_PAN = 'N'" , "F", "1", "F"},
                                                  {TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INVALID_PAN = ''" , "F", "0", "F"},
                                                  {TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INVALID_PAN = 'Y'" , "F", "0", "F"}};

                    string[,] strTAX_DEDUCTED_HIGHER_RATE_1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TAX_DEDUCTED_HIGHER_RATE = 'N'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TAX_DEDUCTED_HIGHER_RATE = 'Y'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TAX_DEDUCTED_HIGHER_RATE = ''" , "F", "0", "F"}};

                    string[,] strSUPER_ANN_YN = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SUPER_ANN_YN = 'N'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SUPER_ANN_YN = 'Y'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SUPER_ANN_YN = ''" , "F", "0", "F"}};

                    string[,] strRENT_EXCEEDING_YN = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".RENT_EXCEEDING_YN = 'N'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".RENT_EXCEEDING_YN = 'Y'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".RENT_EXCEEDING_YN = ''" , "F", "0", "F"}};

                    string[,] strINTEREST_PAID_TO_LENDER = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INTEREST_PAID_TO_LENDER = 'N'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INTEREST_PAID_TO_LENDER = 'Y'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INTEREST_PAID_TO_LENDER = ''" , "F", "0", "F"}};

                    //-- 2024/04/24
                    string[,] strSECTION_115BAC_FLAG_MATRIX = null;
                    if (Convert.ToInt32(lngAsstID) >= T_FinancialYearID.F2023_24ID)
                    {
                        string[,] strSECTION_115BAC_FLAG2324 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = 'Y'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = 'N'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = ''" , "F", "0", "F"}};
                        strSECTION_115BAC_FLAG_MATRIX = strSECTION_115BAC_FLAG2324;
                    }
                    else
                    {
                        string[,] strSECTION_115BAC_FLAG = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = 'N'" , "F", "0", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = 'Y'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SECTION_115BAC_FLAG = ''" , "F", "0", "F"}};
                        strSECTION_115BAC_FLAG_MATRIX = strSECTION_115BAC_FLAG;
                    }
                    //
                    strSQL = "INSERT INTO COR_HDR_SALARY_DETAILS (BATCH_HEADER_ID," +
                             "                                    SL_NO," +
                             "                                    MODE," +
                             "                                    EMPLOYEE_NAME," +
                             "                                    EMPLOYEE_PAN," +
                             "                                    EMPLOYEE_SERIAL_NO," + //-- 2020/06/01
                             "                                    EMPLOYEE_CATEGORY," +
                             "                                    FROM_DATE," +
                             "                                    TO_DATE," +
                             "                                    TS_BALANCE," +
                             "                                    US_16_AGGREGATE," +
                             "                                    INCOME_CHARGEABLE," +
                             //"                                    AIS_Total," +
                             "                                    AIS_ITEM_1," +
                             "                                    GROSS_TOTAL_INCOME," +
                             "                                    CVIA_DED_TOTAL," +
                             "                                    TOTAL_INCOME," +
                             //"                                    TAX_TOTAL_INCOME," +
                             "                                    TAX_TOTAL_INCOME_B4_REBATE," +
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
                             "                                    CVIA_SEC80CCG_DED_AMOUNT," +
                             "                                    CVIA_OTH_DED_TOTAL," +
                             "                                    US_16_EA," +
                             "                                    US_16_TE," +
                             "                                    US_16_IA," +
                             "                                    TAXABLE_AMOUNT," +
                             "                                    REPORTED_TAXABLE_AMOUNT," +
                             "                                    TOTAL_TAX_DEDUCTED_AMOUNT," +
                             "                                    PREVIOUS_TAX_DEDUCTED_TOTAL," +
                             "                                    TAX_DEDUCTED_HIGHER_RATE," +
                             "                                    SUPER_ANN_YN," +
                             "                                    SUPER_ANN_NAME," +
                             "                                    SUPER_ANN_FROM_DATE," +
                             "                                    SUPER_ANN_TO_DATE," +
                             "                                    SUPER_ANN_AMOUNT," +
                             "                                    SUPER_ANN_RATE," +
                             "                                    SUPER_ANN_TAX," +
                             "                                    SUPER_ANN_INCOME," +
                             "                                    RENT_EXCEEDING_YN," +
                             "                                    LANDLORD_PAN_COUNT," +
                             "                                    LANDLORD_1_PAN," +
                             "                                    LANDLORD_1_NAME," +
                             "                                    LANDLORD_2_PAN," +
                             "                                    LANDLORD_2_NAME," +
                             "                                    LANDLORD_3_PAN," +
                             "                                    LANDLORD_3_NAME," +
                             "                                    LANDLORD_4_PAN," +
                             "                                    LANDLORD_4_NAME," +
                             "                                    INTEREST_PAID_TO_LENDER," +
                             "                                    LENDER_PAN_COUNT," +
                             "                                    LENDER_1_PAN," +
                             "                                    LENDER_1_NAME," +
                             "                                    LENDER_2_PAN," +
                             "                                    LENDER_2_NAME," +
                             "                                    LENDER_3_PAN," +
                             "                                    LENDER_3_NAME," +
                             "                                    LENDER_4_PAN," +
                             "                                    LENDER_4_NAME," +
                             "                                    TS_GS_SEC_17_1," +
                             "                                    TS_GS_SEC_17_2," +
                             "                                    TS_GS_SEC_17_3," +
                             "                                    SEC10_5_AMOUNT," +
                             "                                    SEC10_10_AMOUNT," +
                             "                                    SEC10_10A_AMOUNT," +
                             "                                    SEC10_10AA_AMOUNT," +
                             "                                    SEC10_13A_AMOUNT," +
                             "                                    TS_LA_TOTAL," +
                             "                                    SEC10_TOTAL_AMOUNT," +
                             "                                    AIS_ITEM_2," +
                             "                                    REBATE_US_87A_AMOUNT," +
                             "                                    CVIA_SEC80C_DED_TOTAL," +
                             "                                    CVIA_SEC80CCC_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_1B_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_2_DED_AMOUNT," +
                             "                                    CVIA_SEC80D_DED_AMOUNT," +
                             "                                    CVIA_SEC80E_DED_AMOUNT," +
                             "                                    CVIA_SEC80G_DED_AMOUNT," +
                             "                                    CVIA_SEC80TTA_DED_AMOUNT," +
                             "                                    CVIA_SEC80C_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCC_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_1B_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_2_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80D_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80E_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80G_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80G_QUAL_AMOUNT," +
                             "                                    CVIA_SEC80TTA_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80TTA_QUAL_AMOUNT," +
                             "                                    CVIA_OTH_GROSS_AMOUNT," +
                             "                                    CVIA_OTH_QUAL_AMOUNT," +
                             "                                    SECTION_115BAC_FLAG," +
                             "                                    SEC10_14_AMOUNT," +
                             "                                    CVIA_SEC80CCH_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCH_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCH_1_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCH_1_GROSS_AMOUNT," +
                             "                                    TDS_US_192_2B_AMOUNT) " +
                             "SELECT " + lngBatchID + "     AS BATCH_HEADER_ID," +
                             "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO  AS SL_NO," +
                             "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".MODE   AS MODE," +
                             "         EMPLOYEE_NAME         AS EMPLOYEE_NAME," +
                             "         EMPLOYEE_PAN          AS EMPLOYEE_PAN," +
                             "         EMPLOYEE_SERIAL_NO         AS EMPLOYEE_SERIAL_NO," + //-- 2020/06/01
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
                             "       " + cmnService.J_SQLDBFormat("F_80CCG.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS CVIA_SEC80CCG_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_OTHERS.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CVIA_OTH_DED_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_16ii.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS US_16_EA," +
                             "       " + cmnService.J_SQLDBFormat("F_16iii.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS US_16_TE," +
                             "       " + cmnService.J_SQLDBFormat("F_16ia.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS US_16_IA," + //-- 2019/04/20
                             "       " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TAXABLE_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS TAXABLE_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".REPORTED_TAXABLE_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS REPORTED_TAXABLE_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TOTAL_TAX_DEDUCTED_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS TOTAL_TAX_DEDUCTED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".PREVIOUS_TAX_DEDUCTED_TOTAL", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS PREVIOUS_TAX_DEDUCTED_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat(strTAX_DEDUCTED_HIGHER_RATE_1, J_SQLColFormat.Case_End) + "           AS TAX_DEDUCTED_HIGHER_RATE," +
                             "       " + cmnService.J_SQLDBFormat(strSUPER_ANN_YN, J_SQLColFormat.Case_End) + "                         AS SUPER_ANN_YN," +
                             "       " + cmnService.J_SQLDBFormat("SUPER_ANN_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS SUPER_ANN_NAME," +
                             "         SUPER_ANN_FROM_DATE                                                                              AS SUPER_ANN_FROM_DATE," +
                             "         SUPER_ANN_TO_DATE                                                                                AS SUPER_ANN_TO_DATE," +
                             "       " + cmnService.J_SQLDBFormat("SUPER_ANN_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS SUPER_ANN_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("SUPER_ANN_RATE", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS SUPER_ANN_RATE," +
                             "       " + cmnService.J_SQLDBFormat("SUPER_ANN_TAX", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS SUPER_ANN_TAX," +
                             "       " + cmnService.J_SQLDBFormat("SUPER_ANN_INCOME", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS SUPER_ANN_INCOME," +
                             "       " + cmnService.J_SQLDBFormat("RENT_EXCEEDING_YN", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS RENT_EXCEEDING_YN," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_PAN_COUNT", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LANDLORD_PAN_COUNT," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_1_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LANDLORD_1_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_1_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LANDLORD_1_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_2_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LANDLORD_2_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_2_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LANDLORD_2_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_3_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LANDLORD_3_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_3_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LANDLORD_3_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_4_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LANDLORD_4_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LANDLORD_4_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LANDLORD_4_NAME," +
                             "       " + cmnService.J_SQLDBFormat("INTEREST_PAID_TO_LENDER", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS INTEREST_PAID_TO_LENDER," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_PAN_COUNT", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LENDER_PAN_COUNT," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_1_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LENDER_1_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_1_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LENDER_1_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_2_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LENDER_2_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_2_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LENDER_2_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_3_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LENDER_3_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_3_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LENDER_3_NAME," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_4_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "  AS LENDER_4_PAN," +
                             "       " + cmnService.J_SQLDBFormat("LENDER_4_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LENDER_4_NAME," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TS_GS_SEC_17_1", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TS_GS_SEC_17_1," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TS_GS_SEC_17_2", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TS_GS_SEC_17_2," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TS_GS_SEC_17_3", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TS_GS_SEC_17_3," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_5_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SEC10_5_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_10_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SEC10_10_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_10A_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS SEC10_10A_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_10AA_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS SEC10_10AA_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_13A_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS SEC10_13A_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TS_LA_TOTAL", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS TS_LA_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS SEC10_TOTAL_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".AIS_ITEM_2", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "         AS AIS_ITEM_2," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".REBATE_US_87A_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS REBATE_US_87A_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F_80C.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80C_DED_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCC.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80CCC_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD1.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CVIA_SEC80CCD_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD_1B.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS CVIA_SEC80CCD_1B_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD2.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CVIA_SEC80CCD_2_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80D.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80D_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80E.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80E_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80G.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80G_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80TTA.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80TTA_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80C.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80C_GROSS_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCC.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80CCC_GROSS_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD1.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CVIA_SEC80CCD_GROSS_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD_1B.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS CVIA_SEC80CCD_1B_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCD2.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CVIA_SEC80CCD_2_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80D.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80D_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80E.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80E_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80G.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "        AS CVIA_SEC80G_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80G.QUALIFYING_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS CVIA_SEC80G_QUALIFYING_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80TTA.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80TTA_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80TTA.QUALIFYING_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CVIA_SEC80TTA_QUALIFYING_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_OTHERS.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CVIA_OTH_GROSS_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_OTHERS.QUALIFYING_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CVIA_OTH_QUALIFYING_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat(strSECTION_115BAC_FLAG_MATRIX, J_SQLColFormat.Case_End) + "                      AS SECTION_115BAC_FLAG," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SEC10_14_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS SEC10_14_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCH.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80CCH_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCH.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "      AS CVIA_SEC80CCH_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCH_1.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CVIA_SEC80CCH_1_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_80CCH_1.GROSS_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS CVIA_SEC80CCH_1_GROSS_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".TDS_US_192_2B_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS TDS_US_192_2B_AMOUNT " +
                             "FROM ((((((((((((((((((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + " " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCE') AS F_80CCE " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCE.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCF') AS F_80CCF " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCF.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCG') AS F_80CCG " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCG.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = 'OTHERS') AS F_OTHERS " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_OTHERS.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80C') AS F_80C " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80C.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCC') AS F_80CCC " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCC.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCD(1)') AS F_80CCD1 " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCD1.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCD(2)') AS F_80CCD2 " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCD2.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCD(1B)') AS F_80CCD_1B " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCD_1B.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80D') AS F_80D " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80D.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80E') AS F_80E " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80E.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80G') AS F_80G " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80G.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80TTA') AS F_80TTA " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80TTA.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCH') AS F_80CCH " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCH.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT, " +
                             "        GROSS_AMOUNT, " +
                             "        QUALIFYING_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = '80CCH(1)') AS F_80CCH_1 " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_80CCH_1.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + " " +
                             "WHERE SECTION_ID = '16(ia)') AS F_16ia " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_16ia.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + " " +
                             "WHERE SECTION_ID = '16(ii)') AS F_16ii " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_16ii.SD_SL_NO) " +
                             "LEFT JOIN " +
                             "(SELECT SD_SL_NO, " +
                             "        SECTION_ID, " +
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + " " +
                             "WHERE SECTION_ID = '16(iii)') AS F_16iii " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_16iii.SD_SL_NO)";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET US_16_AGGREGATE = US_16_EA + US_16_TE + US_16_IA WHERE BATCH_HEADER_ID = " + lngBatchID;
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                    //-- 2020/05/13
                    strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET AIS_Total = AIS_ITEM_1 + AIS_ITEM_2 WHERE BATCH_HEADER_ID = " + lngBatchID;
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                    if (lngAsstID >= T_FinancialYearID.F2018_19ID)
                    {
                        //-- 2019/05/25
                        strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT = CVIA_SEC80C_DED_TOTAL + CVIA_SEC80CCC_DED_AMOUNT + CVIA_SEC80CCD_DED_AMOUNT WHERE BATCH_HEADER_ID = " + lngBatchID;
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            cmnService.J_UserMessage(strImportErrorMessage);
                            return false;
                        }
                        //-- 2019/05/25
                        strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET CVIA_DED_TOTAL = CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT + CVIA_SEC80CCD_1B_DED_AMOUNT + CVIA_SEC80CCD_2_DED_AMOUNT + CVIA_SEC80D_DED_AMOUNT + CVIA_SEC80E_DED_AMOUNT + CVIA_SEC80G_DED_AMOUNT + CVIA_SEC80TTA_DED_AMOUNT  WHERE BATCH_HEADER_ID = " + lngBatchID;
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            cmnService.J_UserMessage(strImportErrorMessage);
                            return false;
                        }
                        //-- 2019/06/10
                        strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET AIS_Total = AIS_ITEM_1 + AIS_ITEM_2 WHERE BATCH_HEADER_ID = " + lngBatchID;
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            cmnService.J_UserMessage(strImportErrorMessage);
                            return false;
                        }
                    }

                    #endregion

                    #region Insert data to COR_TRN_SALARY_DETAILS

                    strSQL = "INSERT INTO COR_TRN_SALARY_DETAILS (BATCH_HEADER_ID," +
                             "                                    HDR_SALARY_DETAILS_ID," +
                             "                                    SL_NO," +
                             "                                    MODE," +
                             "                                    EMPLOYEE_NAME," +
                             "                                    EMPLOYEE_PAN," +
                             "                                    EMPLOYEE_SERIAL_NO," + //-- 2020/06/01
                             "                                    EMPLOYEE_CATEGORY," +
                             "                                    FROM_DATE," +
                             "                                    TO_DATE," +
                             "                                    TS_BALANCE," +
                             "                                    US_16_EA," +
                             "                                    US_16_TE," +
                             "                                    US_16_IA," +
                             "                                    US_16_AGGREGATE," +
                             "                                    INCOME_CHARGEABLE," +
                             "                                    AIS_Total," +
                             "                                    GROSS_TOTAL_INCOME," +
                             "                                    CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                             "                                    CVIA_OTH_DED_TOTAL," +
                             "                                    CVIA_DED_TOTAL," +
                             "                                    TOTAL_INCOME," +
                             //"                                    TAX_TOTAL_INCOME," +
                             "                                    TAX_TOTAL_INCOME_B4_REBATE," +
                             "                                    SCHG_TOTAL_INCOME," +
                             "                                    ECESS_TOTAL_INCOME," +
                             "                                    TAX_PAYABLE_AGGREGATE," +
                             "                                    US_89_LESS," +
                             "                                    TAX_PAYABLE," +
                             "                                    TOTAL_TDS_DEDUCTED," +
                             "                                    SHORTFALL_TAX," +
                             "                                    IMPORT_FLAG," +
                             "                                    CVIA_SEC80CCF_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCG_DED_AMOUNT," +
                             "                                    TAXABLE_AMOUNT," +
                             "                                    REPORTED_TAXABLE_AMOUNT," +
                             "                                    TOTAL_TAX_DEDUCTED_AMOUNT," +
                             "                                    PREVIOUS_TAX_DEDUCTED_TOTAL," +
                             "                                    TAX_DEDUCTED_HIGHER_RATE," +
                             "                                    SUPER_ANN_YN," +
                             "                                    SUPER_ANN_NAME," +
                             "                                    SUPER_ANN_FROM_DATE," +
                             "                                    SUPER_ANN_TO_DATE," +
                             "                                    SUPER_ANN_AMOUNT," +
                             "                                    SUPER_ANN_RATE," +
                             "                                    SUPER_ANN_TAX," +
                             "                                    SUPER_ANN_INCOME," +
                             "                                    RENT_EXCEEDING_YN," +
                             "                                    LANDLORD_PAN_COUNT," +
                             "                                    LANDLORD_1_PAN," +
                             "                                    LANDLORD_1_NAME," +
                             "                                    LANDLORD_2_PAN," +
                             "                                    LANDLORD_2_NAME," +
                             "                                    LANDLORD_3_PAN," +
                             "                                    LANDLORD_3_NAME," +
                             "                                    LANDLORD_4_PAN," +
                             "                                    LANDLORD_4_NAME," +
                             "                                    INTEREST_PAID_TO_LENDER," +
                             "                                    LENDER_PAN_COUNT," +
                             "                                    LENDER_1_PAN," +
                             "                                    LENDER_1_NAME," +
                             "                                    LENDER_2_PAN," +
                             "                                    LENDER_2_NAME," +
                             "                                    LENDER_3_PAN," +
                             "                                    LENDER_3_NAME," +
                             "                                    LENDER_4_PAN," +
                             "                                    LENDER_4_NAME," +
                             "                                    TS_GS_SEC_17_1," +
                             "                                    TS_GS_SEC_17_2," +
                             "                                    TS_GS_SEC_17_3," +
                             "                                    SEC10_5_AMOUNT," +
                             "                                    SEC10_10_AMOUNT," +
                             "                                    SEC10_10A_AMOUNT," +
                             "                                    SEC10_10AA_AMOUNT," +
                             "                                    SEC10_13A_AMOUNT," +
                             "                                    TS_LA_TOTAL," +
                             "                                    SEC10_TOTAL_AMOUNT," +
                             "                                    AIS_ITEM_1," +
                             "                                    AIS_ITEM_2," +
                             "                                    REBATE_US_87A_AMOUNT," +
                             "                                    CVIA_SEC80C_DED_TOTAL," +
                             "                                    CVIA_SEC80CCC_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_DED_AMOUNT," +
                             "                                    CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_1B_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCD_2_DED_AMOUNT," +
                             "                                    CVIA_SEC80D_DED_AMOUNT," +
                             "                                    CVIA_SEC80E_DED_AMOUNT," +
                             "                                    CVIA_SEC80G_DED_AMOUNT," +
                             "                                    CVIA_SEC80TTA_DED_AMOUNT," +
                             "                                    CVIA_SEC80C_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCC_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_1B_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCD_2_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80D_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80E_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80G_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80G_QUAL_AMOUNT," +
                             "                                    CVIA_SEC80TTA_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80TTA_QUAL_AMOUNT," +
                             "                                    CVIA_OTH_GROSS_AMOUNT," +
                             "                                    CVIA_OTH_QUAL_AMOUNT," +
                             "                                    SECTION_115BAC_FLAG," +
                             "                                    SEC10_14_AMOUNT," +
                             "                                    CVIA_SEC80CCH_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCH_GROSS_AMOUNT," +
                             "                                    CVIA_SEC80CCH_1_DED_AMOUNT," +
                             "                                    CVIA_SEC80CCH_1_GROSS_AMOUNT," +
                             "                                    TDS_US_192_2B_AMOUNT) " +
                             "SELECT  BATCH_HEADER_ID                AS BATCH_HEADER_ID," +
                             "        HDR_SALARY_DETAILS_ID          AS HDR_SALARY_DETAILS_ID," +
                             "        SL_NO                          AS SL_NO," +
                             "        MODE                           AS MODE," +
                             "        EMPLOYEE_NAME                  AS EMPLOYEE_NAME," +
                             "        EMPLOYEE_PAN                   AS EMPLOYEE_PAN," +
                             "        EMPLOYEE_SERIAL_NO             AS EMPLOYEE_SERIAL_NO," + //-- 2020/06/01
                             "        EMPLOYEE_CATEGORY              AS EMPLOYEE_CATEGORY," +
                             "        FROM_DATE                      AS FROM_DATE," +
                             "        TO_DATE                        AS TO_DATE," +
                             "        TS_BALANCE                     AS TS_BALANCE," +
                             "        US_16_EA                       AS US_16_EA," +
                             "        US_16_TE                       AS US_16_TE," +
                             "        US_16_IA                       AS US_16_IA," +
                             "        US_16_AGGREGATE                AS US_16_AGGREGATE," +
                             "        INCOME_CHARGEABLE              AS INCOME_CHARGEABLE," +
                             "        AIS_Total                      AS AIS_Total," +
                             "        GROSS_TOTAL_INCOME             AS GROSS_TOTAL_INCOME," +
                             "        CVIA_SEC80CCE_TOTAL_DED_AMOUNT AS CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                             "        CVIA_OTH_DED_TOTAL             AS CVIA_OTH_DED_TOTAL," +
                             "        CVIA_DED_TOTAL                 AS CVIA_DED_TOTAL," +
                             "        TOTAL_INCOME                   AS TOTAL_INCOME," +
                             //"        TAX_TOTAL_INCOME               AS TAX_TOTAL_INCOME," +
                             "        TAX_TOTAL_INCOME_B4_REBATE     AS TAX_TOTAL_INCOME," +
                             "        SCHG_TOTAL_INCOME              AS SCHG_TOTAL_INCOME," +
                             "        ECESS_TOTAL_INCOME             AS ECESS_TOTAL_INCOME," +
                             "        TAX_PAYABLE_AGGREGATE          AS TAX_PAYABLE_AGGREGATE," +
                             "        US_89_LESS                     AS US_89_LESS," +
                             "        TAX_PAYABLE                    AS TAX_PAYABLE," +
                             "        TOTAL_TDS_DEDUCTED             AS TOTAL_TDS_DEDUCTED," +
                             "        SHORTFALL_TAX                  AS SHORTFALL_TAX," +
                             "        IMPORT_FLAG                    AS IMPORT_FLAG," +
                             "        CVIA_SEC80CCF_DED_AMOUNT       AS CVIA_SEC80CCF_DED_AMOUNT," +
                             "        CVIA_SEC80CCG_DED_AMOUNT       AS CVIA_SEC80CCG_DED_AMOUNT," +
                             "        TAXABLE_AMOUNT                 AS TAXABLE_AMOUNT," +
                             "        REPORTED_TAXABLE_AMOUNT        AS REPORTED_TAXABLE_AMOUNT," +
                             "        TOTAL_TAX_DEDUCTED_AMOUNT      AS TOTAL_TAX_DEDUCTED_AMOUNT," +
                             "        PREVIOUS_TAX_DEDUCTED_TOTAL    AS PREVIOUS_TAX_DEDUCTED_TOTAL," +
                             "        TAX_DEDUCTED_HIGHER_RATE       AS TAX_DEDUCTED_HIGHER_RATE," +
                             "        SUPER_ANN_YN                   AS SUPER_ANN_YN," +
                             "        SUPER_ANN_NAME                 AS SUPER_ANN_NAME," +
                             "        SUPER_ANN_FROM_DATE            AS SUPER_ANN_FROM_DATE," +
                             "        SUPER_ANN_TO_DATE              AS SUPER_ANN_TO_DATE," +
                             "        SUPER_ANN_AMOUNT               AS SUPER_ANN_AMOUNT," +
                             "        SUPER_ANN_RATE                 AS SUPER_ANN_RATE," +
                             "        SUPER_ANN_TAX                  AS SUPER_ANN_TAX," +
                             "        SUPER_ANN_INCOME               AS SUPER_ANN_INCOME," +
                             "        RENT_EXCEEDING_YN              AS RENT_EXCEEDING_YN," +
                             "        LANDLORD_PAN_COUNT             AS LANDLORD_PAN_COUNT," +
                             "        LANDLORD_1_PAN                 AS LANDLORD_1_PAN," +
                             "        LANDLORD_1_NAME                AS LANDLORD_1_NAME," +
                             "        LANDLORD_2_PAN                 AS LANDLORD_2_PAN," +
                             "        LANDLORD_2_NAME                AS LANDLORD_2_NAME," +
                             "        LANDLORD_3_PAN                 AS LANDLORD_3_PAN," +
                             "        LANDLORD_3_NAME                AS LANDLORD_3_NAME," +
                             "        LANDLORD_4_PAN                 AS LANDLORD_4_PAN," +
                             "        LANDLORD_4_NAME                AS LANDLORD_4_NAME," +
                             "        INTEREST_PAID_TO_LENDER        AS INTEREST_PAID_TO_LENDER," +
                             "        LENDER_PAN_COUNT               AS LENDER_PAN_COUNT," +
                             "        LENDER_1_PAN                   AS LENDER_1_PAN," +
                             "        LENDER_1_NAME                  AS LENDER_1_NAME," +
                             "        LENDER_2_PAN                   AS LENDER_2_PAN," +
                             "        LENDER_2_NAME                  AS LENDER_2_NAME," +
                             "        LENDER_3_PAN                   AS LENDER_3_PAN," +
                             "        LENDER_3_NAME                  AS LENDER_3_NAME," +
                             "        LENDER_4_PAN                   AS LENDER_4_PAN," +
                             "        LENDER_4_NAME                  AS LENDER_4_NAME," +
                             "        TS_GS_SEC_17_1                 AS TS_GS_SEC_17_1," +
                             "        TS_GS_SEC_17_2                 AS TS_GS_SEC_17_2," +
                             "        TS_GS_SEC_17_3                 AS TS_GS_SEC_17_3," +
                             "        SEC10_5_AMOUNT                 AS SEC10_5_AMOUNT," +
                             "        SEC10_10_AMOUNT                AS SEC10_10_AMOUNT," +
                             "        SEC10_10A_AMOUNT               AS SEC10_10A_AMOUNT," +
                             "        SEC10_10AA_AMOUNT              AS SEC10_10AA_AMOUNT," +
                             "        SEC10_13A_AMOUNT               AS SEC10_13A_AMOUNT," +
                             "        TS_LA_TOTAL                    AS TS_LA_TOTAL," +
                             "        SEC10_TOTAL_AMOUNT             AS SEC10_TOTAL_AMOUNT," +
                             "        AIS_ITEM_1                     AS AIS_ITEM_1," +
                             "        AIS_ITEM_2                     AS AIS_ITEM_2," +
                             "        REBATE_US_87A_AMOUNT           AS REBATE_US_87A_AMOUNT," +
                             "        CVIA_SEC80C_DED_TOTAL          AS CVIA_SEC80C_DED_TOTAL," +
                             "        CVIA_SEC80CCC_DED_AMOUNT       AS CVIA_SEC80CCC_DED_AMOUNT," +
                             "        CVIA_SEC80CCD_DED_AMOUNT       AS CVIA_SEC80CCD_DED_AMOUNT," +
                             "        CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT AS CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT," +
                             "        CVIA_SEC80CCD_1B_DED_AMOUNT    AS CVIA_SEC80CCD_1B_DED_AMOUNT," +
                             "        CVIA_SEC80CCD_2_DED_AMOUNT     AS CVIA_SEC80CCD_2_DED_AMOUNT," +
                             "        CVIA_SEC80D_DED_AMOUNT         AS CVIA_SEC80D_DED_AMOUNT," +
                             "        CVIA_SEC80E_DED_AMOUNT         AS CVIA_SEC80E_DED_AMOUNT," +
                             "        CVIA_SEC80G_DED_AMOUNT         AS CVIA_SEC80G_DED_AMOUNT," +
                             "        CVIA_SEC80TTA_DED_AMOUNT       AS CVIA_SEC80TTA_DED_AMOUNT," +
                             "        CVIA_SEC80C_GROSS_AMOUNT       AS CVIA_SEC80C_GROSS_AMOUNT," +
                             "        CVIA_SEC80CCC_GROSS_AMOUNT     AS CVIA_SEC80CCC_GROSS_AMOUNT," +
                             "        CVIA_SEC80CCD_GROSS_AMOUNT     AS CVIA_SEC80CCD_GROSS_AMOUNT," +
                             "        CVIA_SEC80CCD_1B_GROSS_AMOUNT  AS CVIA_SEC80CCD_1B_GROSS_AMOUNT," +
                             "        CVIA_SEC80CCD_2_GROSS_AMOUNT   AS CVIA_SEC80CCD_2_GROSS_AMOUNT," +
                             "        CVIA_SEC80D_GROSS_AMOUNT       AS CVIA_SEC80D_GROSS_AMOUNT," +
                             "        CVIA_SEC80E_GROSS_AMOUNT       AS CVIA_SEC80E_GROSS_AMOUNT," +
                             "        CVIA_SEC80G_GROSS_AMOUNT       AS CVIA_SEC80G_GROSS_AMOUNT," +
                             "        CVIA_SEC80G_QUAL_AMOUNT        AS CVIA_SEC80G_QUAL_AMOUNT," +
                             "        CVIA_SEC80TTA_GROSS_AMOUNT     AS CVIA_SEC80TTA_GROSS_AMOUNT," +
                             "        CVIA_SEC80TTA_QUAL_AMOUNT      AS CVIA_SEC80TTA_QUAL_AMOUNT," +
                             "        CVIA_OTH_GROSS_AMOUNT          AS CVIA_OTH_GROSS_AMOUNT," +
                             "        CVIA_OTH_QUAL_AMOUNT           AS CVIA_OTH_QUAL_AMOUNT," +
                             "        SECTION_115BAC_FLAG            AS SECTION_115BAC_FLAG," +
                             "        SEC10_14_AMOUNT                AS SEC10_14_AMOUNT," +
                             "        CVIA_SEC80CCH_DED_AMOUNT       AS CVIA_SEC80CCH_DED_AMOUNT," +
                             "        CVIA_SEC80CCH_GROSS_AMOUNT     AS CVIA_SEC80CCH_GROSS_AMOUNT," +
                             "        CVIA_SEC80CCH_1_DED_AMOUNT     AS CVIA_SEC80CCH_1_DED_AMOUNT," +
                             "        CVIA_SEC80CCH_1_GROSS_AMOUNT   AS CVIA_SEC80CCH_1_GROSS_AMOUNT," +
                             "        TDS_US_192_2B_AMOUNT           AS TDS_US_192_2B_AMOUNT " +
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

                //-- 2024/11/19
                #region For 194P
                if (strFormNo == T_FormNo.F24Q && strQTR == T_Qtr.Q4)
                {

                }
                #endregion

                //-- 2017/10/30
                #region ENTER REGULAR DEDUCTEE/EMPLOYEE MASTER
                //--
                //--
                if (chkAddDeducteesToRegularMaster.Checked == true)  //-- Added By Abhishek Dey On 10/04/2018 --
                    TdsMan.CreateRegularDeducteeMasterFromCorrection(strFormNo, strQTR, lngBatchID);
                //--
                //--
                #endregion
                //
                #region CHANGE MODE TO '' WHERE 'A' 2017/02/24
                //-- UPDATE MODE TO '' WHERE 'A'
                strSQL = "UPDATE COR_HDR_SALARY_DETAILS " +
                         "SET    MODE = '' " +
                         "WHERE  MODE = 'A'  " +
                         "AND BATCH_HEADER_ID = " + lngBatchID + "";
                //-- UPDATE MODE TO '' WHERE 'A' OR 'F' //-- 2019/01/09
                //strSQL = "UPDATE COR_HDR_SALARY_DETAILS " +
                //         "SET    MODE = '' " +
                //         "WHERE  MODE = 'A' OR 'F' " +
                //         "AND BATCH_HEADER_ID = " + lngBatchID + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //-- UPDATE MODE TO '' WHERE 'A'
                strSQL = "UPDATE COR_TRN_SALARY_DETAILS " +
                         "SET    MODE = '' " +
                         "WHERE  MODE = 'A'  " +
                         "AND BATCH_HEADER_ID = " + lngBatchID + "";
                //-- UPDATE MODE TO '' WHERE 'A' //-- 2019/01/09
                //strSQL = "UPDATE COR_TRN_SALARY_DETAILS " +
                //         "SET    MODE = '' " +
                //         "WHERE  MODE = 'A' OR 'F' " +
                //         "AND BATCH_HEADER_ID = " + lngBatchID + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //--
                #endregion
                //MessageBox.Show("1.6");
                #region VERIFYING RECORD COUNTS

                strSQL = "SELECT COUNT(*) AS CHALLAN_MATCHED " +
                         "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ", " +
                         "    (SELECT COUNT(COR_HDR_CHALLAN.HDR_CHALLAN_ID) AS CHALLAN_IMPORTED " +
                         "     FROM   COR_HDR_CHALLAN " +
                         "     WHERE  BATCH_HEADER_ID          = " + lngBatchID + ") AS CHALLAN_CNT " +
                         "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".CHALLAN_RECORDS_COUNT = CHALLAN_CNT.CHALLAN_IMPORTED";

                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) == 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("1" + strImportVerificationMessage);
                    return false;
                }

                strSQL = "SELECT COUNT(*) AS DEDUCTEE_NOT_MATCHED " +
                         "FROM ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + " INNER JOIN COR_HDR_CHALLAN " +
                         "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".SL_NO = COR_HDR_CHALLAN.SL_NO) " +
                         "       LEFT JOIN " +
                         "      (SELECT HDR_CHALLAN_ID, " +
                         "              COUNT(HDR_DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_IMPORTED " +
                         "       FROM   COR_HDR_DEDUCTEE_DETAILS " +
                         "       GROUP BY HDR_CHALLAN_ID) AS DEDUCTEE " +
                         "       ON COR_HDR_CHALLAN.HDR_CHALLAN_ID = DEDUCTEE.HDR_CHALLAN_ID) " +
                         "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".DEDUCTEE_COUNT <> " + cmnService.J_SQLDBFormat("DEDUCTEE.COUNT_DEDUCTEE_IMPORTED", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " " +
                         "AND    COR_HDR_CHALLAN.BATCH_HEADER_ID   = " + lngBatchID;

                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("2" + strImportVerificationMessage);
                    return false;
                }
                //prgBar.Value = prgBar.Value + 5;

                #endregion
                //////MessageBox.Show("1.7");
                #region VERIFYING NULL RECORDS

                strSQL = "SELECT COUNT(*) " +
                    "     FROM   COR_HDR_CHALLAN " +
                    "     WHERE  BATCH_HEADER_ID = " + lngBatchID + " " +
                    "     AND    TOT_TAX IS NULL";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("3" + strImportVerificationMessage);
                    return false;
                }
                //--
                strSQL = "SELECT COUNT(*) " +
                    "     FROM   COR_HDR_DEDUCTEE_DETAILS " +
                    "     WHERE  BATCH_HEADER_ID = " + lngBatchID + " " +
                    "     AND    TOTAL_AMOUNT IS NULL";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("4" + strImportVerificationMessage);
                    return false;
                }
                //-- UPDATE
                strSQL = "SELECT COUNT(*) " +
                    "     FROM   COR_HDR_CHALLAN " +
                    "     WHERE  BATCH_HEADER_ID = " + lngBatchID + " " +
                    "     AND    SECTION_ID IS NULL";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    blnNullSectionFound = true;
                    //--
                    strSQL = "UPDATE COR_HDR_CHALLAN SET SECTION_ID = 0 WHERE BATCH_HEADER_ID = " + lngBatchID + " AND SECTION_ID IS NULL";
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        return false;
                    }
                    //--
                    strSQL = "UPDATE COR_TRN_CHALLAN SET SECTION_ID = 0 WHERE BATCH_HEADER_ID = " + lngBatchID + " AND SECTION_ID IS NULL";
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        return false;
                    }
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion
                ////MessageBox.Show("1.8");
                #region VERIFYING TRANSFER VOUCHER FOR BOOK ENTRY

                strSQL = "SELECT COUNT(*) AS COUNT_RECORDS " +
                    "     FROM   COR_HDR_CHALLAN " +
                    "     WHERE  BOOK_ENTRY          = 1 " +
                    "     AND    TRANSFER_VOUCHER_NO = '' " +
                    "     AND    BATCH_HEADER_ID     = " + lngBatchID + " ";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    strSQL = "UPDATE COR_HDR_CHALLAN SET TRANSFER_VOUCHER_NO = CHALLAN_NO WHERE BOOK_ENTRY = 1 AND TRANSFER_VOUCHER_NO = '' AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "UPDATE COR_HDR_CHALLAN SET CHALLAN_NO = '' WHERE BOOK_ENTRY = 1 AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "UPDATE COR_TRN_CHALLAN SET TRANSFER_VOUCHER_NO = CHALLAN_NO WHERE BOOK_ENTRY = 1 AND TRANSFER_VOUCHER_NO = '' AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "UPDATE COR_TRN_CHALLAN SET CHALLAN_NO = '' WHERE BOOK_ENTRY = 1 AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                ////MessageBox.Show("1.9");
                #region UPDATING PAN_UPDATION_INDICATOR TO '0' //-- 2016/05/28
                //
                strSQL = "UPDATE COR_TRN_SALARY_DETAILS SET PAN_UPDATION_INDICATOR = 0 WHERE BATCH_HEADER_ID = " + lngBatchID + " ";
                dmlService.J_ExecSql(strSQL);
                //                    
                #endregion
                //MessageBox.Show("1.10");
                #region RE CALCULATING CTRL_TOT_TAX BASED ON PENDING AMOUNT

                // Added by Shrey Kejriwal on 25/09/2014

                // Checking whether there is difference in pending amount calculated on the basis of amount
                // and pending amount specified in file

                // And if found updating the ctrl_tot_tax field to show the pending amount difference


                //Difference calculation on the basis on book entry value
                //string[,] strLoadChallanTotTaxMatrix = {{"COR_TRN_CHALLAN.BOOK_ENTRY =  1", "F", "COR_TRN_CHALLAN.TOT_TAX - COR_TRN_CHALLAN.INTEREST_ALLOCATED - COR_TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
                //                                    {"COR_TRN_CHALLAN.BOOK_ENTRY <> 1", "F", "COR_TRN_CHALLAN.TOT_TAX - COR_TRN_CHALLAN.INTEREST_ALLOCATED - COR_TRN_CHALLAN.OTHERS_ALLOCATED - COR_TRN_CHALLAN.LATE_FEE", "F"}};

                //strSQL = "SELECT COUNT(*) " + 
                //         "FROM COR_TRN_CHALLAN " +
                //         "WHERE  " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " - CTRL_TOT_TAX <> PENDING_AMOUNT " +
                //         "AND    CHALLAN_STATUS = 'M' " +
                //         "AND    PENDING_AMOUNT <> 0 " + //-- 2014/10/09
                //         "AND    BOOK_ENTRY = 0 " + //-- 2014/10/09
                //         "AND    BATCH_HEADER_ID = " + lngBatchID;

                //int intCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                //if (intCount > 0)
                //{

                //    strSQL = "UPDATE COR_TRN_CHALLAN " +
                //             "SET    CTRL_TOT_TAX = " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + @" - PENDING_AMOUNT " +
                //             "WHERE  " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + @" - CTRL_TOT_TAX <> PENDING_AMOUNT " +
                //             "AND   CHALLAN_STATUS = 'M' " +
                //             "AND   PENDING_AMOUNT <> 0 " + //-- 2014/10/09
                //             "AND   BOOK_ENTRY = 0 " + //-- 2014/10/09
                //             "AND   BATCH_HEADER_ID = " + lngBatchID;

                //    dmlService.J_ExecSql(strSQL);
                //}   

                #endregion
                //MessageBox.Show("1.11");
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

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }
                }
                #endregion
                //--
                //-- Added By Abhishek Dey On 08/12/2017 --
                #region Count Of CHALAN Details/ DEDUCTEE Details/ SALARY Details
                //
                int intChallan = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM COR_TRN_CHALLAN WHERE BATCH_HEADER_ID = " + lngBatchID));
                int intDeductee = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + lngBatchID));
                int intSalary = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM COR_TRN_SALARY_DETAILS WHERE BATCH_HEADER_ID = " + lngBatchID));
                #endregion
                //-----------------------------------------
                //-- Added By Abhishek Dey On 30/11/2017 --
                #region Insert data to TRN_TDS_IMPORT_LOG
                strSQL = "SELECT ASST_ID FROM MST_ASSESSMENT WHERE FA_YEAR = '" + strLogFaYear + "' ";
                //
                strLogFaYearId = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //
                if (TdsMan.InsertDataToTrnTdsImportLogTable(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("h:mm:ss tt"), strLogFileCreationDate, strLogTanNo, strLogForm, strLogFaYearId, strLogQuarter, 1, Convert.ToInt32(lngBatchID), intChallan, intDeductee, intSalary) == false)  //-- Modified On 06/12/2017 --
                {
                    dmlService.J_Rollback();
                    return false;
                }
                //
                #endregion
                //-----------------------------------------
                //
                //dmlService.J_Commit();

                return true;
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(strImportErrorMessage);
                //MessageBox.Show(err_handler.Message);
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
                                     Col62=F62 Char
                                     Col63=F63 Char
                                     Col64=F64 Char
                                     Col65=F65 Char
                                     Col66=F66 Char
                                     Col67=F67 Char
                                     Col68=F68 Char
                                     Col69=F69 Char
                                     Col70=F70 Char
                                     Col71=F71 Char
                                     Col72=F72 Char
                                     Col73=F73 Char
                                     Col74=F74 Char
                                     Col75=F75 Char
                                     Col76=F76 Char
                                     Col77=F77 Char
                                     Col78=F78 Char
                                     Col79=F79 Char
                                     Col80=F80 Char
                                     Col81=F81 Char
                                     Col82=F82 Char");

            #endregion

            StreamWriter.Close();
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                #region ImportTableName
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist(ImportTableName) == true)
                {
                    strSQL = "DROP TABLE " + ImportTableName + "";
                    dmlService.J_ExecSql(strSQL);
                }
                if (dmlService.J_IsDatabaseObjectExist(ImportTableName) == false)
                {
                    strSQL = @"CREATE TABLE " + ImportTableName + @" (
                                            " + cmnService.J_GetDataType("F1", J_Identity.YES) + @",
                                            " + cmnService.J_GetDataType("F2", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F3", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F4", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F5", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F6", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F7", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F8", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F9", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F10", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F11", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F12", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F13", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F14", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F15", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F16", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F17", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F18", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F19", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F20", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F21", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F22", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F23", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F24", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F25", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F26", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F27", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F28", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F29", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F30", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F31", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F32", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F33", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F34", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F35", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F36", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F37", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F38", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F39", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F40", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F41", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F42", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F43", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F44", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F45", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F46", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F47", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F48", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F49", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F50", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F51", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F52", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F53", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F54", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F55", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F56", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F57", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F58", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F59", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F60", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F61", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F62", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F63", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F64", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F65", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F66", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F67", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F68", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("F69", J_ColumnType.String, 255) + @")";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                #endregion
                //Check 'n Create Temp Tables
                if (dmlService.J_IsDatabaseObjectExist(ImportTableName) == true)
                {
                    strSQL = "DELETE FROM [" + ImportTableName + "]";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = @"BULK INSERT [" + ImportTableName + "] FROM '" + Path.Combine(Application.StartupPath, ImportTableName) + ".txt' WITH (fieldterminator = '^', rowterminator = '\n')";
                dmlService.J_ExecSql(strSQL);
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
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
                #region COMMENTED
                /*
                #region TEMP_FH
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + " (" +
                         "             FILE_CREATION_DATE DATETIME," +
                         "             FILE_LINES_COUNT   TEXT(20)  DEFAULT \"\"," +
                         "             HASH_VALUE         TEXT(20)  DEFAULT \"\" " +
                         "             )";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + " (" +
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
                         "             P_PAN                      TEXT(12)  DEFAULT \"\"," +
                         "             PAO_REG_NO                 TEXT(7)   DEFAULT \"\"," +
                         "             DDO_REG_NO                 TEXT(10)  DEFAULT \"\"," +
                         "             ALT_STD                    TEXT(5)   DEFAULT \"\"," +
                         "             ALT_PHONE                  TEXT(30)  DEFAULT \"\"," +
                         "             ALT_EMAIL                  TEXT(75)  DEFAULT \"\"," +
                         "             ALT_P_STD                  TEXT(5)   DEFAULT \"\"," +
                         "             ALT_P_PHONE                TEXT(30)  DEFAULT \"\"," +
                         "             ALT_P_EMAIL                TEXT(75)  DEFAULT \"\"," +
                         "             AIN                        TEXT(7)   DEFAULT \"\" " +
                         "             )";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + " (" +
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
                         "             PENDING_AMOUNT              MONEY    DEFAULT 0," +
                         "             FEE                         MONEY    DEFAULT 0," +
                         "             MINOR_CODE                  TEXT(15) DEFAULT \"\"" +
                         "             )";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + " (" +
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
                         "             PAN_COUNTER           NUMBER    DEFAULT 0," +
                         "             SECTION_NO            TEXT(10)  DEFAULT \"\"," +
                         "             CERTIFICATE_NO        TEXT(20)  DEFAULT \"\"," +
                         "             TDS_APPLICABILITY_CODE TEXT(1)   DEFAULT \"\"," +
                         "             REMITTANCE_CODE       TEXT(3)   DEFAULT \"\"," +
                         //"             UNIQUE_ACKN           TEXT(12)  DEFAULT \"\"," +
                         //-- ANIK @ 16/07/2014
                         "             UNIQUE_ACKN           TEXT(100)  DEFAULT \"\"," +
                         "             COUNTRY_CODE          TEXT(3)   DEFAULT \"\"" +
                         "             )";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + "";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + " (" +
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
                         "             PAN_COUNTER           NUMBER    DEFAULT 0," +
                         "             TAXABLE_AMOUNT        MONEY     DEFAULT 0," +
                         "             REPORTED_TAXABLE_AMOUNT     MONEY     DEFAULT 0," +
                         "             TOTAL_TAX_DEDUCTED_AMOUNT   MONEY     DEFAULT 0," +
                         "             PREVIOUS_TAX_DEDUCTED_TOTAL MONEY     DEFAULT 0," +
                         "             TAX_DEDUCTED_HIGHER_RATE    TEXT(1)   DEFAULT \"\"" +
                         "             )";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + " (" +
                         "             SD_SL_NO              NUMBER    DEFAULT 0," +
                         "             SECTION_ID            TEXT(15)  DEFAULT \"\"," +
                         "             TOTAL_AMOUNT          MONEY     DEFAULT 0" +
                         "             )";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " (" +
                         "             SD_SL_NO              NUMBER    DEFAULT 0," +
                         "             SECTION_ID            TEXT(15)  DEFAULT \"\"," +
                         "             TOTAL_AMOUNT          MONEY     DEFAULT 0" +
                         "             )";
                dmlService.J_ExecSql(strSQL);                
                #endregion
                */
                #endregion
                //--
                #region TEMP_FH
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + "";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FH + @" (
                            " + cmnService.J_GetDataType("FILE_CREATION_DATE", J_ColumnType.DateTime) + @",
                            " + cmnService.J_GetDataType("FILE_LINES_COUNT", J_ColumnType.String, 20) + @",
                            " + cmnService.J_GetDataType("HASH_VALUE", J_ColumnType.String, 20) + @")";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + @" (
                            " + cmnService.J_GetDataType("CHALLAN_RECORDS_COUNT", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("FORM_NO", J_ColumnType.String, 4) + @",
                            " + cmnService.J_GetDataType("ORIGINAL_RRR_NO", J_ColumnType.String, 15) + @",
                            " + cmnService.J_GetDataType("PREVIOUS_RRR_NO", J_ColumnType.String, 15) + @",
                            " + cmnService.J_GetDataType("TAN_NO", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("EXPECTED_CHALLAN_RECORD_NO", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("PAN_NO", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("ASST_YEAR", J_ColumnType.String, 7) + @",
                            " + cmnService.J_GetDataType("FA_YEAR", J_ColumnType.String, 7) + @",
                            " + cmnService.J_GetDataType("QTR", J_ColumnType.String, 2) + @",
                            " + cmnService.J_GetDataType("COMPANY_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("BRANCH_DIV", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("ADDRESS1", J_ColumnType.String, 25) + @",
                            " + cmnService.J_GetDataType("ADDRESS2", J_ColumnType.String, 25) + @",
                            " + cmnService.J_GetDataType("ADDRESS3", J_ColumnType.String, 25) + @",
                            " + cmnService.J_GetDataType("ADDRESS4", J_ColumnType.String, 25) + @",
                            " + cmnService.J_GetDataType("ADDRESS5", J_ColumnType.String, 25) + @",
                            " + cmnService.J_GetDataType("STATE_CODE", J_ColumnType.String, 2) + @",
                            " + cmnService.J_GetDataType("PIN_CODE", J_ColumnType.String, 6) + @",
                            " + cmnService.J_GetDataType("EMAIL", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("STD", J_ColumnType.String, 5) + @",
                            " + cmnService.J_GetDataType("PHONE", J_ColumnType.String, 30) + @",
                            " + cmnService.J_GetDataType("ADDRESS_CHANGE", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("CATEGORY_CODE", J_ColumnType.String, 5) + @",
                            " + cmnService.J_GetDataType("PERSON_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("DESIGNATION", J_ColumnType.String, 20) + @",
                            " + cmnService.J_GetDataType("P_ADDRESS1", J_ColumnType.String, 25) + @",
                            " + cmnService.J_GetDataType("P_ADDRESS2", J_ColumnType.String, 25) + @",
                            " + cmnService.J_GetDataType("P_ADDRESS3", J_ColumnType.String, 25) + @",
                            " + cmnService.J_GetDataType("P_ADDRESS4", J_ColumnType.String, 25) + @",
                            " + cmnService.J_GetDataType("P_ADDRESS5", J_ColumnType.String, 25) + @",
                            " + cmnService.J_GetDataType("P_STATE_CODE", J_ColumnType.String, 2) + @",
                            " + cmnService.J_GetDataType("P_PIN_CODE", J_ColumnType.String, 6) + @",
                            " + cmnService.J_GetDataType("P_EMAIL", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("EXPECTED_SD_RECORD_NO", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("P_STD", J_ColumnType.String, 5) + @",
                            " + cmnService.J_GetDataType("P_PHONE", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("P_ADDRESS_CHANGE", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("TOT_CHALLAN_DEPOSIT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("P_MOBILE", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("COUNT_SD_RECORDS", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("TOT_GROSS_TOT_INCOME", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("D_STATE_CODE", J_ColumnType.String, 2) + @",
                            " + cmnService.J_GetDataType("PAO_CODE", J_ColumnType.String, 20) + @",
                            " + cmnService.J_GetDataType("DDO_CODE", J_ColumnType.String, 20) + @",
                            " + cmnService.J_GetDataType("MINISTRY_CODE", J_ColumnType.String, 3) + @",
                            " + cmnService.J_GetDataType("MINISTRY_OTHER", J_ColumnType.String, 150) + @",
                            " + cmnService.J_GetDataType("P_PAN", J_ColumnType.String, 12) + @",
                            " + cmnService.J_GetDataType("PAO_REG_NO", J_ColumnType.String, 7) + @",
                            " + cmnService.J_GetDataType("DDO_REG_NO", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("ALT_STD", J_ColumnType.String, 5) + @",
                            " + cmnService.J_GetDataType("ALT_PHONE", J_ColumnType.String, 30) + @",
                            " + cmnService.J_GetDataType("ALT_EMAIL", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("ALT_P_STD", J_ColumnType.String, 5) + @",
                            " + cmnService.J_GetDataType("ALT_P_PHONE", J_ColumnType.String, 30) + @",
                            " + cmnService.J_GetDataType("ALT_P_EMAIL", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("AIN", J_ColumnType.String, 7) + @",
                            " + cmnService.J_GetDataType("ISD_CODE", J_ColumnType.String, 7) + @",
                            " + cmnService.J_GetDataType("P_ISD_CODE", J_ColumnType.String, 7) + @",
                            " + cmnService.J_GetDataType("COUNTRY", J_ColumnType.String, 7) + @",
                            " + cmnService.J_GetDataType("P_COUNTRY", J_ColumnType.String, 7) + @")";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + @" ( 
                            " + cmnService.J_GetDataType("SL_NO", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("DEDUCTEE_COUNT", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("EXPECTED_DEDUCTEE_RECORD_NO", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("CHALLAN_STATUS", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("CHALLAN_NO", J_ColumnType.String, 50) + @",
                            " + cmnService.J_GetDataType("TRANSFER_VOUCHER_NO", J_ColumnType.String, 9) + @",
                            " + cmnService.J_GetDataType("BSR_CODE", J_ColumnType.String, 50) + @",
                            " + cmnService.J_GetDataType("DEPOSIT_DATE", J_ColumnType.DateTime) + @",
                            " + cmnService.J_GetDataType("SECTION_NAME", J_ColumnType.String, 15) + @",
                            " + cmnService.J_GetDataType("TDS", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SURCHARGE", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("EDUCATION_CESS", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("INTEREST", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("OTHERS", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TOT_TAX", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("CTRL_TOT_TAX", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("CTRL_TDS", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("CTRL_SURCHARGE", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("CTRL_EDU_CESS", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("CTRL_TOT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("INTEREST_ALLOCATED", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("OTHERS_ALLOCATED", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("CHEQUE_NO", J_ColumnType.String, 15) + @",
                            " + cmnService.J_GetDataType("BOOK_ENTRY", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("PENDING_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("FEE", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("MINOR_CODE", J_ColumnType.String, 15) + @",
                            " + cmnService.J_GetDataType("NIL_CHALLAN", J_ColumnType.String, 15) + @")";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + @" ( 
                            " + cmnService.J_GetDataType("CHALLAN_SL_NO", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("SL_NO", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("MODE", J_ColumnType.String, 2) + @",
                            " + cmnService.J_GetDataType("EMPLOYEE_SERIAL_NO", J_ColumnType.String, 9) + @", 
                            " + cmnService.J_GetDataType("DEDUCTEE_CODE", J_ColumnType.String, 2) + @",
                            " + cmnService.J_GetDataType("DEDUCTEE_PAN", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("DEDUCTEE_PAN_REF", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("DEDUCTEE_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("TAX_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SURCHARGE_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("CESS_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TOTAL_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TAX_DEPOSITED_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TOT_VALUE_PURCHASE", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("PAYMENT_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("PAYMENT_DATE", J_ColumnType.DateTime) + @",
                            " + cmnService.J_GetDataType("DEDUCTED_DATE", J_ColumnType.DateTime) + @",
                            " + cmnService.J_GetDataType("DATE_OF_DEPOSIT", J_ColumnType.DateTime) + @",
                            " + cmnService.J_GetDataType("RATE", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("GROSSING_UP_INDICATOR", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("CASH_BOOK_ENTRY", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("NON_DEDUCTION_FLAG", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("INVALID_PAN", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("PAN_COUNTER", J_ColumnType.Integer) + @",
                            " + cmnService.J_GetDataType("SECTION_NO", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("CERTIFICATE_NO", J_ColumnType.String, 20) + @",
                            " + cmnService.J_GetDataType("TDS_APPLICABILITY_CODE", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("REMITTANCE_CODE", J_ColumnType.String, 3) + @",
                            " + cmnService.J_GetDataType("UNIQUE_ACKN", J_ColumnType.String, 100) + @",
                            " + cmnService.J_GetDataType("COUNTRY_CODE", J_ColumnType.String, 4) + @"," +      //-- 2015/06/02 ANIK.G.
                            "" + cmnService.J_GetDataType("EMAIL", J_ColumnType.String, 75) + "," +            //-- 2016/11/12 ANIK.G.
                            "" + cmnService.J_GetDataType("MOBILE_NO", J_ColumnType.String, 15) + "," +        //-- 2016/11/12 ANIK.G.
                            "" + cmnService.J_GetDataType("DEDUCTEE_ADDRESS", J_ColumnType.String, 150) + "," + //-- 2016/11/12 ANIK.G.
                            "" + cmnService.J_GetDataType("DEDUCTEE_TAX_ID", J_ColumnType.String, 25) + "," + //-- 2015/06/02 ANIK.G. 
                            "" + cmnService.J_GetDataType("NON_RESIDENT", J_ColumnType.String, 25) + "," + //-- 2015/06/02 ANIK.G. 
                            "" + cmnService.J_GetDataType("PERMANENT_ESTABLISHMENT", J_ColumnType.String, 25) + "," + //-- 2015/06/02 ANIK.G. 
                            "" + cmnService.J_GetDataType("TDS_CLAUSE_YN", J_ColumnType.String, 25) + "," + //-- 2015/06/02 ANIK.G. 
                            "" + cmnService.J_GetDataType("TDS_CLAUSE_CHALLAN_NO", J_ColumnType.Long) + "," + //-- 2021/01/04
                            "" + cmnService.J_GetDataType("TDS_CLAUSE_PAYMENT_DATE", J_ColumnType.DateTime) + "," + //-- 2021/01/04
                            "" + cmnService.J_GetDataType("SEC194N_GREATER_1CRORE_PAYMENT_AMOUNT", J_ColumnType.Double) + "," +
                            "" + cmnService.J_GetDataType("SEC194NC_EXCESS_3CRORE", J_ColumnType.Double) + "," + 
                            "" + cmnService.J_GetDataType("SECTION_115BAC_FLAG", J_ColumnType.String, 2) + ")"; 

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + @" ( 
                            " + cmnService.J_GetDataType("SL_NO", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("MODE", J_ColumnType.String, 2) + @",
                            " + cmnService.J_GetDataType("EMPLOYEE_PAN", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("EMPLOYEE_SERIAL_NO", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("EMPLOYEE_CATEGORY", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("FROM_DATE", J_ColumnType.DateTime) + @",
                            " + cmnService.J_GetDataType("TO_DATE", J_ColumnType.DateTime) + @",
                            " + cmnService.J_GetDataType("TS_BALANCE", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("US_16_AGGREGATE", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("INCOME_CHARGEABLE", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("AIS_TOTAL", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("GROSS_TOTAL_INCOME", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("CVIA_DED_TOTAL", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TOTAL_INCOME", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TAX_TOTAL_INCOME", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SCHG_TOTAL_INCOME", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("ECESS_TOTAL_INCOME", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("US_89_LESS", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TAX_PAYABLE", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TOTAL_TDS_DEDUCTED", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SHORTFALL_TAX", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("INVALID_PAN", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("PAN_COUNTER", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("TAXABLE_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("REPORTED_TAXABLE_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TOTAL_TAX_DEDUCTED_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("PREVIOUS_TAX_DEDUCTED_TOTAL", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TAX_DEDUCTED_HIGHER_RATE", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("RENT_EXCEEDING_YN", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("LANDLORD_PAN_COUNT", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("LANDLORD_1_PAN", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("LANDLORD_1_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("LANDLORD_2_PAN", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("LANDLORD_2_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("LANDLORD_3_PAN", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("LANDLORD_3_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("LANDLORD_4_PAN", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("LANDLORD_4_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("INTEREST_PAID_TO_LENDER", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("LENDER_PAN_COUNT", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("LENDER_1_PAN", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("LENDER_1_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("LENDER_2_PAN", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("LENDER_2_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("LENDER_3_PAN", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("LENDER_3_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("LENDER_4_PAN", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("LENDER_4_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("SUPER_ANN_YN", J_ColumnType.String, 1) + @",
                            " + cmnService.J_GetDataType("SUPER_ANN_NAME", J_ColumnType.String, 75) + @",
                            " + cmnService.J_GetDataType("SUPER_ANN_FROM_DATE", J_ColumnType.DateTime) + @",
                            " + cmnService.J_GetDataType("SUPER_ANN_TO_DATE", J_ColumnType.DateTime) + @",
                            " + cmnService.J_GetDataType("SUPER_ANN_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SUPER_ANN_RATE", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SUPER_ANN_TAX", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SUPER_ANN_INCOME", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TS_GS_SEC_17_1", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TS_GS_SEC_17_2", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TS_GS_SEC_17_3", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SEC10_5_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SEC10_10_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SEC10_10A_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SEC10_10AA_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SEC10_13A_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TS_LA_TOTAL", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SEC10_TOTAL_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("AIS_ITEM_2", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("REBATE_US_87A_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("SECTION_115BAC_FLAG", J_ColumnType.String, 10) + @",
                            " + cmnService.J_GetDataType("SEC10_14_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("TDS_US_192_2B_AMOUNT", J_ColumnType.Double) + @")";

                
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_S16 + @" ( 
                            " + cmnService.J_GetDataType("SD_SL_NO", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("SECTION_ID", J_ColumnType.String, 15) + @",
                            " + cmnService.J_GetDataType("TOTAL_AMOUNT", J_ColumnType.Double) + @")";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + @" ( 
                            " + cmnService.J_GetDataType("SD_SL_NO", J_ColumnType.Long) + @",
                            " + cmnService.J_GetDataType("SECTION_ID", J_ColumnType.String, 15) + @",
                            " + cmnService.J_GetDataType("TOTAL_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("GROSS_AMOUNT", J_ColumnType.Double) + @",
                            " + cmnService.J_GetDataType("QUALIFYING_AMOUNT", J_ColumnType.Double) + @")";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + " //-- 2016/03/30
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + "";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion

        #region T_DeleteDuplicateData
        private bool T_DeleteDuplicateData()
        {
            strSQL = "SELECT F1 FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TDS_FILE_DATA + " WHERE F2 = 'BH' AND F1 > 2";
            long lngF1 = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
            //
            if (lngF1 > 0)
            {
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TDS_FILE_DATA + " WHERE F1 >= " + lngF1;
                if (dmlService.J_ExecSql(strSQL) == false)
                    return false;
            }
            //
            return true;
        }
        #endregion

        #region ConvertSQLDate
        private string ConvertSQLDate(string Field)
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                return "CONVERT(DATETIME, SUBSTRING(" + Field + ", 3, 2) + '/' + LEFT(" + Field + ", 2) + '/' +  RIGHT(" + Field + ", 4), 101)";
                //return "CONVERT(DATETIME," + Field + " , 103)";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                return "DateSerial(RIGHT(" + Field + ",4),MID(" + Field + ",3,2),LEFT(" + Field + ",2))";
            else
                return "";

        }
        #endregion

        private void grpNotes_Enter(object sender, EventArgs e)
        {

        }

        #endregion


        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=A3KsPWdluBA");
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0018", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));            
        }
        #endregion


        #region pctVideoDemo_MouseMove
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0041", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion


        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion
    }
}

