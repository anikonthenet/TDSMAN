
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



namespace TDSMAN.FormTrn
{
    public partial class TrnConsolidatedStatement_FVUImport : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnConsolidatedStatement_FVUImport()
        {
            InitializeComponent();
        }
        #endregion

        #region enum Save_button_tag
        public struct Save_button_tag
        {
            public const string Import = "Import";
            public const string Cancel = "Cancel";
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
        //
        int intCaratPosition = 0;
        string strFVUPath = "";
        string strCheckCompatibilityMessage = "";
        long lngBatchID = 0;
        int intLoop = 0;
        //-----------------------------------------------------------------------
        string strUploadType = "";
        string strFVUVersion = "";
        double dblFVUVersion = 0;

        string OutputFolder;
        string Filename;
        string OutputFilePath;

        string strFileCreationDate = "";

        bool blnImport;
        bool blnCancel;

            
        #endregion

        #region User Defined Events

        #region TrnFVUImport_Load
        private void TrnFVUImport_Load(object sender, EventArgs e)
        {
            GC.Collect();
            //
            //-----------
            lblTitle.Text = "Import Data for Correction";
            lblNotes1.Text = "As per the new notification released by the TIN-NSDL it is mandatory to make corrections using consolidated TDS/TCS\nstatement only downloaded from TIN-website.";

            //lnkLabel2.Text = "Click here to download the tutorial on how to get the TDS/TCS file from TIN-NSDL."
            //-- ANIK 2011/09/03
            lnkLabel2.Text = "Click here to download the tutorial on how to get the TDS/TCS.";

            //Added by Shrey Kejriwal on 15/02/2012
            lblPrcnt.Text = "";
            //
            BtnSave.Text = Save_button_tag.Import;
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

            //Modified by Shrey Kejriwal on 24/02/2012
            if (strFVUPath != "")
                txtFVUPath.Text = strFVUPath;
            else
                txtFVUPath.Text = "";

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
            if (BtnSave.Text == Save_button_tag.Import)
            {
                prgBar.Value = 0;
                blnCancel = false;
                //
                if (ValidateFields() == false) return;
                //
                if (CheckFVUCompatibility(txtFVUPath.Text) == false)
                {
                    cmnService.J_UserMessage(strCheckCompatibilityMessage);
                    btnSelectTDSPath.Select();
                    return;
                }
                //
                if (cmnService.J_UserMessage("TDS File Creation Date : " + strFileCreationDate + "\n Please ensure that you are using the latest file for preparing correction statement.\n Proceed to Import?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    BtnExit.Select();
                    return;
                }

                ////prgBar.Value = prgBar.Value + 5;
                //this.Refresh();

                OutputFolder = Path.Combine(Application.StartupPath, "Correction Input Files");
                Filename = cmnService.J_GetFileName(strFVUPath);
                OutputFilePath = OutputFolder + "\\" + Filename;

                cmnService.J_CreateDirectory(OutputFolder);


                File.Copy(strFVUPath, OutputFilePath, true);

                //Included by Indrajit on 09.02.2012
                this.Cursor = Cursors.WaitCursor;
                bgwWorker.RunWorkerAsync();


                BtnSave.Text = Save_button_tag.Cancel;
                BtnExit.Enabled = false;
                BtnExit.BackColor = Color.Silver;

                // Commented by Shrey Kejriwal on 15/02/2012

                //if (ImportTransactionsFromFVU(txtFVUPath.Text) == false)
                //{
                //    this.Cursor = Cursors.Default;
                //    return;
                //}
                ////
                ////prgBar.Value = prgBar.Value + 5;
                //this.Refresh();
                ////
                ////
                //for (int i = prgBar.Minimum; i <= prgBar.Maximum; i++)
                //{
                //    prgBar.PerformStep();
                //}
                //this.Cursor = Cursors.Default;
                ////

                //txtFVUPath.Text = "";
                ////---------------
                //cmnService.J_UserMessage("Import Completed");
                ////
                //prgBar.Value = 0;
                //    
            }
            else
            {
                blnCancel = true;
                bgwWorker.CancelAsync();
                dmlService.J_Rollback();
            }

        }
        #endregion

        #region bgwWorker_DoWork
        //Included by Indrajit on 09.02.2012

        private void bgwWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            blnImport = ImportTransactionsFromFVU(txtFVUPath.Text, bgwWorker);

            if (bgwWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }
        #endregion

        #region bgwWorker_ProgressChanged
        //Included by Indrajit on 09.02.2012

        private void bgwWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblPrcnt.Text = e.ProgressPercentage.ToString() + "% Completed";
            prgBar.Value = e.ProgressPercentage;
        }
        #endregion

        #region bgwWorker_RunWorkerCompleted
        //Included by Indrajit on 09.02.2012

        private void bgwWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            txtFVUPath.Text = "";
            prgBar.Value = 0;
            lblPrcnt.Text = "";

            BtnSave.Text = Save_button_tag.Import;
            BtnExit.Enabled = true;
            BtnExit.BackColor = Color.Lavender;

            if (e.Cancelled)
            {
                cmnService.J_UserMessage("Import cancelled.");
                return;
            }

            cmnService.J_UserMessage("Import Completed");
            
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
                    //if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
                    //    return false;
                    //}
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    //if (grpSearch.Visible == false)
                    //{
                    //    if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    //    {
                    //        cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //        if (dsetGridClone == null) return false;
                    //        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
                    //        return false;
                    //    }
                    //}
                    //else if (grpSearch.Visible == true)
                    //{
                    //    if (txtPANSearch.Text.Trim() == "" &&
                    //        txtEmployeeNameSearch.Text.Trim() == "" &&
                    //        txtCompanyNameSearch.Text.Trim() == "")
                    //    {
                    //        cmnService.J_UserMessage(J_Msg.SearchingValues);
                    //        txtPANSearch.Select();
                    //        return false;
                    //    }
                    //}
                    //return true;
                }
                else
                {
                    //-----------------------------------------------------------------------
                    //-- FINANCILAL YEAR
                    //-----------------------------------------------------------------------
                    //if (cmbFinancialYear.SelectedIndex <= 0)
                    //{
                    //    cmnService.J_UserMessage("Financial Year - Cannot be Blank");
                    //    cmbFinancialYear.Select();
                    //    return false;
                    //}
                    ////-----------------------------------------------------------------------
                    ////-- QTR
                    ////-----------------------------------------------------------------------
                    //if (cmbQuarter.SelectedIndex <= 0)
                    //{
                    //    cmnService.J_UserMessage("Quarter - Cannot be Blank");
                    //    cmbQuarter.Select();
                    //    return false;
                    //}
                    ////-----------------------------------------------------------------------
                    ////-- FORM NO
                    ////-----------------------------------------------------------------------
                    //if (cmbFormNo.SelectedIndex <= 0)
                    //{
                    //    cmnService.J_UserMessage("Form No. - Cannot be Blank");
                    //    cmbFormNo.Select();
                    //    return false;
                    //}
                    ////-----------------------------------------------------------------------
                    ////-- COMPANY NAME
                    ////-----------------------------------------------------------------------
                    //if (cmbCompany.SelectedIndex <= 0)
                    //{
                    //    cmnService.J_UserMessage("Company - Cannot be Blank");
                    //    cmbCompany.Select();
                    //    return false;
                    //}
                    //-----------------------------------------------------------------------
                    //-- FVU FILE SELECTED
                    //-----------------------------------------------------------------------
                    if (txtFVUPath.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("File not selected");
                        btnSelectTDSPath.Select();
                        return false;
                    }
                    // FILE SHOULD BE FVU
                    //if (Path.GetExtension(txtFVUPath.Text).ToUpper() != ".TDS")
                    //{
                    //    cmnService.J_UserMessage("Selected file Invalid");
                    //    btnSelectTDSPath.Select();
                    //    return false;
                    //}
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
            string strFinancialYear = "";
            string FinancialYear = "";

            TextReader txtRdr = new StreamReader(FVUPath);
            //
            int NumberOfLines = 3;
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
                        strCheckCompatibilityMessage = "Other than Regular Return FVU is supported";
                        txtRdr.Close();
                        txtRdr.Dispose();
                        return false;
                    }
                    // ***********************************************************************************
                    //Added by Shrey Kejriwal on 30/12/2011
                    //--- Checking Date of Creation of of tds file
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strFileCreationDate = ListLines[i].Substring(intCaratPosition + 1);
                    strFileCreationDate = ListLines[i].Substring(intCaratPosition + 1, strFileCreationDate.IndexOf("^"));

                    strFileCreationDate = cmnService.J_Left(strFileCreationDate, 2) + "/" + cmnService.J_Mid(strFileCreationDate, 2, 2) + "/" + cmnService.J_Right(strFileCreationDate, 4);
                    //Formatting the date for display in the confirmation message
                    strFileCreationDate = String.Format("{0:dd MMMM yyyy}", dtService.J_ConvertddMMyyyy(strFileCreationDate));

                    // ***********************************************************************************

                    //
                    if (Path.GetExtension(txtFVUPath.Text).ToUpper() == ".TDS")
                        dblFVUVersion = cmnService.J_ReturnDoubleValue(T_FVU_Version.FVU_3_2);

                    //Commented by Shrey Kejriwal on 30/12/2011
                    // FVU file no longer used
                    //else
                    //{
                    //    // FVU Version
                    //    for (int a = 1; a < 7; a++)
                    //    {
                    //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    //    }
                    //    //
                    //    do
                    //    {
                    //        for (int a = 1; a < 2; a++)
                    //        {
                    //            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    //        }
                    //        //
                    //        strFVUVersion = ListLines[i].Substring(intCaratPosition + 1);
                    //        strFVUVersion = ListLines[i].Substring(intCaratPosition + 1, strFVUVersion.IndexOf("^"));
                    //    } while (strFVUVersion.Substring(0, 3) != "FVU");
                    //    //
                    //    dblFVUVersion = cmnService.J_ReturnDoubleValue(strFVUVersion.Substring(3).Trim());
                    //}

                    // -- COMMENTED 2011-05-26 - ANIK
                    //// FVU Version [2.126]
                    //for (int a = 1; a < 8; a++)
                    //{
                    //    intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    //    //
                    //}
                    //strFVUVersion = ListLines[i].Substring(intCaratPosition + 1);
                    //strFVUVersion = ListLines[i].Substring(intCaratPosition + 1, strFVUVersion.IndexOf("^"));
                    ////    
                    //if (strFVUVersion != "")
                    //    dblFVUVersion = cmnService.J_ReturnDoubleValue(strFVUVersion.Substring(3).Trim());
                    ////
                    //if (dblFVUVersion != 2.126)
                    //{
                    //    // FVU Version [2.128, 2.129, 3.0]
                    //    for (int a = 1; a < 1; a++)
                    //    {
                    //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    //        //
                    //    }
                    //    strFVUVersion = ListLines[i].Substring(intCaratPosition + 1);
                    //    strFVUVersion = ListLines[i].Substring(intCaratPosition + 1, strFVUVersion.IndexOf("^"));
                    //    //
                    //    dblFVUVersion = cmnService.J_ReturnDoubleValue(strFVUVersion.Substring(3).Trim());
                    //}
                    
                    //if (dblFVUVersion < 2.126)
                    //{
                    //    strCheckCompatibilityMessage = "FVU Version before 2.126 is not supported";
                    //    txtRdr.Close();
                    //    txtRdr.Dispose();
                    //    return false;
                    //}
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
                        cmnService.J_UserMessage("Incorrect Format of the File. Please check.");
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
                        cmnService.J_UserMessage("Incorrect Format of the File. Please check.");
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

                if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "CD")
                {
                    string strPendingAmount;
                    
                    for (int a = 1; a < 37; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strPendingAmount = ListLines[i].Substring(intCaratPosition + 1);
                    strPendingAmount = ListLines[i].Substring(intCaratPosition + 1, strPendingAmount.Trim().IndexOf("^"));

                    if (strPendingAmount == "")
                    {
                        //Commented by Shrey Kejriwal on 05/03/2012
                        //As for nil challans Pending Amount is not given

                        //cmnService.J_UserMessage("File format is obsolete. Please request for the .TDS file again and import the new file.");
                        //txtRdr.Close();
                        //txtRdr.Dispose();
                        //return false;
                    }
                }
            }
            txtRdr.Close();
            txtRdr.Dispose();
            return true;
        }
        #endregion

        #region ImportTransactionsFromFVU

        private bool ImportTransactionsFromFVU(string FVUPath, BackgroundWorker bgwWorker)
        {
            try
            {
                #region DECLARATION

                int NumberOfLines = 0;

                // FH
                string strFileCreationDate = "";
                string strRPUName = "";
                string strFileHash = "";

                // BH
                string strFormNo = "";
                string strOriginalTokenNo = "";
                string strPreviousTokenNo = "";
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
                int intDeductorChangeofAddress = 0;
                string strDeductorType = "";
                long lngDeductorCategoryId = 0;
                

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
                int intRPChangeofAddress = 0;
                string strRPType = "";

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
                string strPAORegNo = "";
                string strDDORegNo = "";

                string strExpectedChallanRecNo = "";
                string strExpectedSDRecNo = "";
                int intExpectedSDRecNo = 0;

                // CD
                string strCDRecordNumber = "";
                string strNILChallanIndicator = "";
                int intNILChallanIndicator = 0;
                string strChallanNumber = "";
                string strTransferVoucherNumber = "";
                string strBSRCode = "";
                string strDateofBankChallan = "";
                string strSection = "";
                long lngSectionID = 0;
                string strOLTASIncomeTax = "";
                string strOLTASSurcharge = "";
                string strOLTASCess = "";
                string strOLTASInterestAmount = "";
                string strOLTASOthers = "";
                string strTotalDepositedAmount = "";
                string strTotalTaxDepositedAmount = "";
                string strIncomeTax = "";
                string strSurcharge = "";
                string strCess = "";
                string strSumTotalIncomeTax = "";
                string strInterestAmount = "";
                string strOthersAmount = "";
                string strChequeDDNumber = "";
                string strBookEntry = "";
                string strCDRemarks = "";
                int intCDSerialNo = 1;
                int intBookEntry = 0;
                string strPendingAmount = "";

                long lngChallanID = 0;

                //Added by Shrey on 09/08/2011
                string strChallanStatus;
                string strExpectedDeducteeSerial;

                // DD
                string strDDRecordNumber = "";
                string strDeducteeCode = "";
                string strDeducteePAN = "";
                string strDeducteeName = "";
                long lngPartyId = 0;
                string strTDSIncomeTax = "";
                string strTDSSurcharge = "";
                string strTDSCess = "";
                string strTotalIncomeTaxDeducted = "";
                string strTotalTaxDeposited = "";
                string strTotalValueOfPurchase = "";
                string strAmountOfPayment = "";
                string strDateAmountPaid = "";
                string strDateTaxDeducted = "";
                string strRate = "";
                string strGrossingUpIndicator = "";
                string strRemarks1 = "";
                string strPanFlag = "";
                int intPanFlag = 0;

                string strPanCounter = "";
                int intPanCounter = 0;


                // SD
                string strSDRecordNumber = "";
                string strEmployeePAN = "";
                string strEmployeeName = "";
                string strEmployeeCategory = "";
                string strPeriodFromDate = "";
                string strPeriodToDate = "";
                string strTotalAmountSalary = "";

                string strCountSec16Records = "";
                string strGrossTotalSec16 = "";
                string strIncomeChargeableUnderHeadSalaries = "";
                string strIncomeOtherThanSalaries = "";
                string strGrossTotalIncome = "";
                string strCountChVIARecords = "";
                string strGrossTotalChVIA = "";

                string strTotalTaxableIncome = "";
                string strIncomeTaxOnTotalIncome = "";
                string strSurchargeSD = "";
                string strEducationCess = "";
                string strIncomeTaxRelief = "";
                string strNetIncomeTaxPayable = "";
                string strTotalAmountOfTaxDeducted = "";
                string strShortfallExcess = "";

                // SD - S16
                string strSDS16RecordNumber = "";
                string strS16SectionID = "";
                string strS16TotalDeduction = "";

                // SD - C6A
                string strSDC6ARecordNumber = "";
                string strC6ASectionID = "";
                string strC6ATotalAmount = "";

                long lngCOR_HDR_SALARY_DETAILS = 0;

                #endregion

                dmlService.J_BeginTransaction();
                
                TextReader txtRdrGetNoLines = new StreamReader(FVUPath);
                while (txtRdrGetNoLines.ReadLine() != null)
                {
                    NumberOfLines++;
                }
                txtRdrGetNoLines.Close();
                txtRdrGetNoLines.Dispose();

                string[] ListLines = new string[NumberOfLines];
                //
                TextReader txtRdr = new StreamReader(FVUPath);
                intCaratPosition = 0;
                //
                for (int i = 0; i < NumberOfLines; i++)
                {
                    ListLines[i] = txtRdr.ReadLine();

                    //Added by Shrey Kejriwal on 15/02/2012
                    //=============================================================
                    //Checking for the Cancellation Request
                    if (bgwWorker.CancellationPending)
                        break;

                    //Calculating percentage finished
                    double fltPercent = Math.Round((Convert.ToDouble(i + 1) / NumberOfLines) * 100, 1);
                    
                    // Reporting progress only if the percentage change is in whole number
                    // and also checking if the progress bar has not reached the maximum
                    if (fltPercent % 1 == 0 && prgBar.Value != 100 && fltPercent != 0)
                        bgwWorker.ReportProgress(Convert.ToInt32(fltPercent));
                    //=============================================================

                    intCaratPosition = ListLines[i].Trim().IndexOf("^");

                    #region FILE HEADER [FH]

                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "FH")
                    {
                        //1	Line Number
                        //2	Record Type
                        //3	File Type
                        //4	Upload Type
                        //5	File Creation Date
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }

                        strFileCreationDate = ListLines[i].Substring(intCaratPosition + 1);
                        strFileCreationDate = ListLines[i].Substring(intCaratPosition + 1, strFileCreationDate.Trim().IndexOf("^"));

                        //6	File Sequence No.
                        //7	Uploader Type
                        //8	TAN of Deductor
                        //9	Total No. of Batches 
                        //10 Name of Return Preparation Utility
                        for (int a = 1; a < 6; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }

                        strRPUName = ListLines[i].Substring(intCaratPosition + 1);
                        strRPUName = ListLines[i].Substring(intCaratPosition + 1, strRPUName.Trim().IndexOf("^"));

                        if (dblFVUVersion < 2.128)
                            strRPUName = "";

                        //11	Record Hash (Not applicable)
                        //12	FVU Version (Not applicable)
                        for (int a = 1; a < 6; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }

                        strFileHash = ListLines[i].Substring(intCaratPosition + 1);
                        strFileHash = ListLines[i].Substring(intCaratPosition + 1, strFileHash.Trim().IndexOf("^"));

                        //13	File Hash (Not applicable)
                        //14	Sam Version (Not applicable)
                        //15	SAM Hash (Not applicable)
                        //16	SCM Version (Not applicable)
                        //17	SCM Hash (Not applicable)

                        //
                        continue;
                    }
                    #endregion

                    #region BATCH HEADER [BH]

                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "BH")
                    {
                        // 1 LINE NUMBER
                        // 2 RECORD TYPE
                        // 3 BATCH NUMBER
                        // 4 COUNT OF CHALLAN/TRANSFER VOUCHER RECORDS
                        // 5 FORM NO.
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }

                        strFormNo = ListLines[i].Substring(intCaratPosition + 1);
                        strFormNo = ListLines[i].Substring(intCaratPosition + 1, strFormNo.Trim().IndexOf("^"));

                        // 6 TRANSACTION TYPE
                        // 7 BATCH UPDATION INDICATOR
                        // 8 ORIGINAL TOKEN NUMBER
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }

                        strOriginalTokenNo = ListLines[i].Substring(intCaratPosition + 1);
                        strOriginalTokenNo = ListLines[i].Substring(intCaratPosition + 1, strOriginalTokenNo.Trim().IndexOf("^"));

                        // 9 PREVIOUS TOKEN NUMBER
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }

                        strPreviousTokenNo = ListLines[i].Substring(intCaratPosition + 1);
                        strPreviousTokenNo = ListLines[i].Substring(intCaratPosition + 1, strPreviousTokenNo.Trim().IndexOf("^"));

                        // 10 TOKEN NUMBER OF THE STATEMENT SUBMITTED
                        // 11 TOKEN NUMBER DATE
                        // 12 LAST TAN
                        // 13 TAN
                        for (int a = 1; a < 5; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTAN = ListLines[i].Substring(intCaratPosition + 1);
                        strTAN = ListLines[i].Substring(intCaratPosition + 1, strTAN.Trim().IndexOf("^"));

                        // 14 EXPECTED CHALLAN RECORD NUMBER
                        // Added by Shrey Kejriwal on 24/02/2012

                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strExpectedChallanRecNo = ListLines[i].Substring(intCaratPosition + 1);
                        strExpectedChallanRecNo = ListLines[i].Substring(intCaratPosition + 1, strExpectedChallanRecNo.Trim().IndexOf("^"));

                        // 15 PAN
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        //-- ANIK GHOSH 2011-07-12
                        //strPAN = ListLines[i].Substring(intCaratPosition + 1, 10);
                        strPAN = ListLines[i].Substring(intCaratPosition + 1);
                        strPAN = ListLines[i].Substring(intCaratPosition + 1, strPAN.Trim().IndexOf("^"));
                        //--


                        // 16 ASSESSMENT YEAR
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strAssessmentYear = ListLines[i].Substring(intCaratPosition + 1, 6);

                        // 17 FINANCIAL YEAR
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strFinancialYear = ListLines[i].Substring(intCaratPosition + 1, 6);
                        strFinancialYear = cmnService.J_Left(strFinancialYear, 4) + "-" + cmnService.J_Right(strFinancialYear, 2);
                        long lngFinancialYear = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT ASST_ID FROM MST_ASSESSMENT WHERE FA_YEAR = '" + cmnService.J_ReplaceQuote(strFinancialYear) + "'");
                        // 18 PERIOD/QTR
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strQTR = ListLines[i].Substring(intCaratPosition + 1, 2);

                        // 19 DEDUCTOR NAME
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorName = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorName = ListLines[i].Substring(intCaratPosition + 1, strDeductorName.Trim().IndexOf("^"));

                        // 20 DEDUCTOR BRANCH
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorBranch = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorBranch = ListLines[i].Substring(intCaratPosition + 1, strDeductorBranch.Trim().IndexOf("^"));

                        // 21 DEDUCTOR ADDRESS1
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorAddress1 = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorAddress1 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress1.Trim().IndexOf("^"));

                        // 22 DEDUCTOR ADDRESS2
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorAddress2 = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorAddress2 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress2.Trim().IndexOf("^"));

                        // 23 DEDUCTOR ADDRESS3
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorAddress3 = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorAddress3 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress3.Trim().IndexOf("^"));

                        // 24 DEDUCTOR ADDRESS4
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorAddress4 = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorAddress4 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress4.Trim().IndexOf("^"));

                        // 25 DEDUCTOR ADDRESS5
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorAddress5 = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorAddress5 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress5.Trim().IndexOf("^"));

                        // 26 DEDUCTOR STATE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorStateCode = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorStateCode = ListLines[i].Substring(intCaratPosition + 1, strDeductorStateCode.Trim().IndexOf("^"));
                        //
                        strDeductorStateCode = Convert.ToString(cmnService.J_ReturnInt64Value(strDeductorStateCode));
                        lngDeductorStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDeductorStateCode) + "'");

                        // 27 DEDUCTOR PIN
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorPIN = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorPIN = ListLines[i].Substring(intCaratPosition + 1, strDeductorPIN.Trim().IndexOf("^"));

                        // 28 DEDUCTOR EMAIL
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorEmail = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorEmail = ListLines[i].Substring(intCaratPosition + 1, strDeductorEmail.Trim().IndexOf("^"));

                        // 29 DEDUCTOR STD
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorSTD = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorSTD = ListLines[i].Substring(intCaratPosition + 1, strDeductorSTD.Trim().IndexOf("^"));

                        // 30 DEDUCTOR TELEPHONE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorTelePhone = ListLines[i].Substring(intCaratPosition + 1, strDeductorTelePhone.Trim().IndexOf("^"));

                        // 31 DEDUCTOR CHANGE OF ADDRESS
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorChangeofAddress = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorChangeofAddress = ListLines[i].Substring(intCaratPosition + 1, strDeductorChangeofAddress.Trim().IndexOf("^"));
                        //
                        if (strDeductorChangeofAddress.ToUpper() == "Y")
                            intDeductorChangeofAddress = 1;
                        else
                            intDeductorChangeofAddress = 0;

                        // 32 DEDUCTOR TYPE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeductorType = ListLines[i].Substring(intCaratPosition + 1);
                        strDeductorType = ListLines[i].Substring(intCaratPosition + 1, strDeductorType.Trim().IndexOf("^"));
                        //
                        lngDeductorCategoryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT CATEGORY_ID FROM MST_CATEGORY WHERE CATEGORY_CODE = '" + cmnService.J_ReplaceQuote(strDeductorType) + "'");

                        // 33 RP NAME
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPName = ListLines[i].Substring(intCaratPosition + 1);
                        strRPName = ListLines[i].Substring(intCaratPosition + 1, strRPName.Trim().IndexOf("^"));

                        // 34 RP DESIGNATION
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPDesignation = ListLines[i].Substring(intCaratPosition + 1);
                        strRPDesignation = ListLines[i].Substring(intCaratPosition + 1, strRPDesignation.Trim().IndexOf("^"));

                        // 35 RP ADDRESS1
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPAddress1 = ListLines[i].Substring(intCaratPosition + 1);
                        strRPAddress1 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress1.Trim().IndexOf("^"));

                        // 36 RP ADDRESS2
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPAddress2 = ListLines[i].Substring(intCaratPosition + 1);
                        strRPAddress2 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress2.Trim().IndexOf("^"));

                        // 37 RP ADDRESS3
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPAddress3 = ListLines[i].Substring(intCaratPosition + 1);
                        strRPAddress3 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress3.Trim().IndexOf("^"));

                        // 38 RP ADDRESS4
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPAddress4 = ListLines[i].Substring(intCaratPosition + 1);
                        strRPAddress4 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress4.Trim().IndexOf("^"));

                        // 39 RP ADDRESS5
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPAddress5 = ListLines[i].Substring(intCaratPosition + 1);
                        strRPAddress5 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress5.Trim().IndexOf("^"));

                        // 40 RP STATE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPStateCode = ListLines[i].Substring(intCaratPosition + 1);
                        strRPStateCode = ListLines[i].Substring(intCaratPosition + 1, strRPStateCode.Trim().IndexOf("^"));
                        //
                        strRPStateCode = Convert.ToString(cmnService.J_ReturnInt64Value(strRPStateCode));
                        lngRPStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strRPStateCode) + "'");

                        // 41 RP PIN
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPPIN = ListLines[i].Substring(intCaratPosition + 1);
                        strRPPIN = ListLines[i].Substring(intCaratPosition + 1, strRPPIN.Trim().IndexOf("^"));

                        // 42 RP EMAIL
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPEmail = ListLines[i].Substring(intCaratPosition + 1);
                        strRPEmail = ListLines[i].Substring(intCaratPosition + 1, strRPEmail.Trim().IndexOf("^"));

                        //43 Expected Salry detail Record Number
                        //Added by Shrey Kejriwal on 24/02/2012

                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strExpectedSDRecNo = ListLines[i].Substring(intCaratPosition + 1);
                        strExpectedSDRecNo = ListLines[i].Substring(intCaratPosition + 1, strExpectedSDRecNo.Trim().IndexOf("^"));

                        if (strFormNo == T_FormNo.F24Q && strQTR == T_Qtr.Q4)
                            intExpectedSDRecNo = Convert.ToInt32(strExpectedSDRecNo);

                        ////if (dblFVUVersion < 3.0)
                        ////    strRPMobile = "";

                        // 44 RP STD
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPSTD = ListLines[i].Substring(intCaratPosition + 1);
                        strRPSTD = ListLines[i].Substring(intCaratPosition + 1, strRPSTD.Trim().IndexOf("^"));

                        // 45 RP TELEPHONE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                        strRPTelePhone = ListLines[i].Substring(intCaratPosition + 1, strRPTelePhone.Trim().IndexOf("^"));

                        // 46 RP CHANGE OF ADDRESS
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPChangeofAddress = ListLines[i].Substring(intCaratPosition + 1);
                        strRPChangeofAddress = ListLines[i].Substring(intCaratPosition + 1, strRPChangeofAddress.Trim().IndexOf("^"));
                        //
                        if (strRPChangeofAddress.ToUpper() == "Y")
                            intRPChangeofAddress = 1;
                        else
                            intRPChangeofAddress = 0;

                        // 47 BATCH TOTAL
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strBatchTotal = ListLines[i].Substring(intCaratPosition + 1);
                        strBatchTotal = ListLines[i].Substring(intCaratPosition + 1, strBatchTotal.Trim().IndexOf("^"));


                        // 48 MOBILE NO.

                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRPMobile = ListLines[i].Substring(intCaratPosition + 1);
                        strRPMobile = ListLines[i].Substring(intCaratPosition + 1, strRPMobile.Trim().IndexOf("^"));

                        if (dblFVUVersion < 3.0)
                            strRPMobile = "";

                        // FOR FORM NO. 24Q
                        if (strFormNo == T_FormNo.F24Q)
                        {
                            //////// 48 UNMATCHED CHALLAN COUNT
                            // 49 COUNT SALARY DETAIL RECORDS
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strCountSalaryDetailRecords = ListLines[i].Substring(intCaratPosition + 1);
                            strCountSalaryDetailRecords = ListLines[i].Substring(intCaratPosition + 1, strCountSalaryDetailRecords.Trim().IndexOf("^"));

                            // 50 COUNT SALARY DETAIL RECORDS
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strBatchTotalSalary = ListLines[i].Substring(intCaratPosition + 1);
                            strBatchTotalSalary = ListLines[i].Substring(intCaratPosition + 1, strBatchTotalSalary.Trim().IndexOf("^"));
                            //
                            intLoop = 2;
                        }
                        else
                            intLoop = 4;
                        //
                        // 51 AO APPROVAL
                        for (int a = 1; a < intLoop; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strAOApproval = ListLines[i].Substring(intCaratPosition + 1);
                        strAOApproval = ListLines[i].Substring(intCaratPosition + 1, strAOApproval.Trim().IndexOf("^"));

                        // 52 AO APPROVAL NUMBER
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strAOApprovalNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strAOApprovalNumber = ListLines[i].Substring(intCaratPosition + 1, strAOApprovalNumber.Trim().IndexOf("^"));

                        if (dblFVUVersion >= 2.126)
                        {
                            // 53 LAST DEDUCTOR TYPE
                            // 54 DEDUCTOR STATE NAME
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strDStateCode = ListLines[i].Substring(intCaratPosition + 1);
                            strDStateCode = ListLines[i].Substring(intCaratPosition + 1, strDStateCode.Trim().IndexOf("^"));
                            //
                            strDStateCode = Convert.ToString(cmnService.J_ReturnInt64Value(strDStateCode));
                            lngDStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDStateCode) + "'");

                            // 55 PAO CODE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strPAO = ListLines[i].Substring(intCaratPosition + 1);
                            strPAO = ListLines[i].Substring(intCaratPosition + 1, strPAO.Trim().IndexOf("^"));

                            // 56 DDO CODE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strDDO = ListLines[i].Substring(intCaratPosition + 1);
                            strDDO = ListLines[i].Substring(intCaratPosition + 1, strDDO.Trim().IndexOf("^"));

                            // 57 MINISTRY
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strMinistryName = ListLines[i].Substring(intCaratPosition + 1);
                            strMinistryName = ListLines[i].Substring(intCaratPosition + 1, strMinistryName.Trim().IndexOf("^"));
                            //
                            lngMinistryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT MINISTRY_ID FROM MST_MINISTRY WHERE MINISTRY_CODE = '" + cmnService.J_ReplaceQuote(strMinistryName) + "'");

                            // 58 OTHER MINISTRY
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strOtherMinistryName = ListLines[i].Substring(intCaratPosition + 1);
                            strOtherMinistryName = ListLines[i].Substring(intCaratPosition + 1, strOtherMinistryName.Trim().IndexOf("^"));

                            // 59 FILLER 2
                            // 60 PAO REGISTRATION NUMBER
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strPAORegNo = ListLines[i].Substring(intCaratPosition + 1);
                            strPAORegNo = ListLines[i].Substring(intCaratPosition + 1, strPAORegNo.Trim().IndexOf("^"));

                            // 61 DDO REGISTRATION NUMBER
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strDDORegNo = ListLines[i].Substring(intCaratPosition + 1);
                            strDDORegNo = ListLines[i].Substring(intCaratPosition + 1, strDDORegNo.Trim().IndexOf("^"));

                            // 62 RECORD HASH
                        }
                        T_GenerateBatchId(dmlService.J_pCommand, lngFinancialYear, strQTR,
                                          strFormNo, System.DateTime.Now.ToString(), strRPUName,
                                          cmnService.J_Left(strFileCreationDate, 2) + "/" + cmnService.J_Mid(strFileCreationDate, 2, 2) + "/" + cmnService.J_Right(strFileCreationDate, 4));
                        //
                        lngBatchID = T_ReturnBatchId();
                        //
                        strSQL = "UPDATE COR_HDR_BATCH SET " +
                            "            ORIGINAL_RRR_NO ='" + cmnService.J_ReplaceQuote(strOriginalTokenNo) + "'," +
                            "            PREVIOUS_RRR_NO ='" + cmnService.J_ReplaceQuote(strPreviousTokenNo) + "', " +
                            "            HASH_VALUE ='" + strFileHash + "', " +
                            "            TDS_FILE_PATH ='" + OutputFilePath + "' " +
                            "     WHERE  BATCH_HEADER_ID = " + lngBatchID;
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                        // INSERT TRN_COMPANY_INFO
                        strSQL = "INSERT INTO COR_HDR_COMPANY (" +
                         "            BATCH_HEADER_ID," +
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
                         "            ADDRESS_CHANGE," +
                         "            P_ADDRESS_CHANGE, " +
                         "            EXPECTED_CHALLAN_RECORD_NO, " +
                         "            EXPECTED_SD_RECORD_NO) " +
                         "     VALUES( " + lngBatchID + "," +
                         "             " + TDSMAN.Classes.TDSMAN.T_pGroupId + "," +
                         "            '" + cmnService.J_ReplaceQuote(strTAN.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strPAN.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDeductorName.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDeductorBranch.ToUpper()) + "'," +
                         "             " + lngDeductorCategoryId + "," +
                         "             " + lngMinistryId + "," +
                         "            '" + cmnService.J_ReplaceQuote(strOtherMinistryName.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDeductorAddress1.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDeductorAddress2.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDeductorAddress3.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDeductorAddress4.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDeductorAddress5.ToUpper()) + "'," +
                         "             " + lngDeductorStateID + "," +
                         "            '" + cmnService.J_ReplaceQuote(strDeductorPIN) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDeductorSTD) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDeductorTelePhone) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDeductorEmail) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strRPName.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strRPDesignation.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strRPAddress1.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strRPAddress2.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strRPAddress3.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strRPAddress4.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strRPAddress5.ToUpper()) + "'," +
                         "             " + lngRPStateID + "," +
                         "            '" + cmnService.J_ReplaceQuote(strRPPIN) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strRPSTD) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strRPTelePhone) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strRPEmail) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strRPMobile) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strPAO.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strPAORegNo.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDDO.ToUpper()) + "'," +
                         "            '" + cmnService.J_ReplaceQuote(strDDORegNo.ToUpper()) + "'," +
                         "             " + lngDStateID + "," +
                         "             " + intDeductorChangeofAddress + "," +
                         "             " + intRPChangeofAddress + "," +
                         "             " + strExpectedChallanRecNo + "," +
                         "             " + intExpectedSDRecNo + ")";
                        //
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                        //
                        strSQL = "SELECT MAX(HDR_COMPANY_ID) AS COMPANY_ID FROM COR_HDR_COMPANY";
                        long lngCOR_HDR_COMPANY = dmlService.J_ReturnId(strSQL);
                        //
                        strSQL = "INSERT INTO COR_TRN_COMPANY(" +
                         "            HDR_COMPANY_ID," +
                         "            BATCH_HEADER_ID," +
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
                         "            ADDRESS_CHANGE," +
                         "            P_ADDRESS_CHANGE, " +
                         "            EXPECTED_CHALLAN_RECORD_NO, " +
                         "            EXPECTED_SD_RECORD_NO) " +
                         "     SELECT HDR_COMPANY_ID," +
                         "            BATCH_HEADER_ID," +
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
                         "            ADDRESS_CHANGE," +
                         "            P_ADDRESS_CHANGE, " +
                         "            EXPECTED_CHALLAN_RECORD_NO, " +
                         "            EXPECTED_SD_RECORD_NO " + 
                         "     FROM   COR_HDR_COMPANY " +
                         "     WHERE  HDR_COMPANY_ID = " + lngCOR_HDR_COMPANY;
                        //
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                        //
                        continue;
                    }
                    #endregion

                    #region CHALLAN DETAIL [CD]

                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "CD")
                    {
                        // 1 LINE NUMBER
                        // 2 RECORD TYPE
                        // 3 BATCH NUMBER
                        // 4 CHALLAN DETAIL RECORD NUMBER
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strCDRecordNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strCDRecordNumber = ListLines[i].Substring(intCaratPosition + 1, strCDRecordNumber.Trim().IndexOf("^"));

                        // 5 COUNT OF DEDUCTEE RECORDS
                        // 6 NIL CHALLAN INDICATOR
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strNILChallanIndicator = ListLines[i].Substring(intCaratPosition + 1);
                        strNILChallanIndicator = ListLines[i].Substring(intCaratPosition + 1, strNILChallanIndicator.Trim().IndexOf("^"));
                        //
                        if (strNILChallanIndicator == "Y")
                            intNILChallanIndicator = 1;
                        else if (strNILChallanIndicator == "N")
                            intNILChallanIndicator = 0;

                        // 7 CHALLAN UPDATION INDICATOR

                        // 8 EXPECTED DEDUCTEE RECORD NO.
                        
                        //Added by Shrey Kejriwal on 23/02/2012
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strExpectedDeducteeSerial = ListLines[i].Substring(intCaratPosition + 1);
                        strExpectedDeducteeSerial = ListLines[i].Substring(intCaratPosition + 1, strExpectedDeducteeSerial.Trim().IndexOf("^"));
                        

                        // MODIFIED BY SHREY KEJRIWAL ON 09/08/2011 TO SHOW CHALLAN STATUS
                        // 9 CHALLAN MATCHING INDICATOR 
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strChallanStatus = ListLines[i].Substring(intCaratPosition + 1);
                        strChallanStatus = ListLines[i].Substring(intCaratPosition + 1, strChallanStatus.Trim().IndexOf("^"));

                        // 10 FILLER 5
                        // 11 LAST BANK CHALLAN NUMBER
                        // 12 CHALLAN NUMBER
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strChallanNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strChallanNumber = ListLines[i].Substring(intCaratPosition + 1, strChallanNumber.Trim().IndexOf("^"));

                        // 13 LAST TRANSFER VOUCHER NUMBER
                        // 14 TRANSFER VOUCHER NUMBER
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTransferVoucherNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strTransferVoucherNumber = ListLines[i].Substring(intCaratPosition + 1, strTransferVoucherNumber.Trim().IndexOf("^"));

                        // 15 LAST BANK BRANCH CODE
                        // 16 BANK BRANCH CODE
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strBSRCode = ListLines[i].Substring(intCaratPosition + 1);
                        strBSRCode = ListLines[i].Substring(intCaratPosition + 1, strBSRCode.Trim().IndexOf("^"));

                        // 17 LAST DATE DATE BANK CHALLAN
                        // 18 DATE BANK CHALLAN
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDateofBankChallan = ListLines[i].Substring(intCaratPosition + 1);
                        strDateofBankChallan = ListLines[i].Substring(intCaratPosition + 1, strDateofBankChallan.Trim().IndexOf("^"));
                        strDateofBankChallan = cmnService.J_Left(strDateofBankChallan, 2) + "/" + cmnService.J_Mid(strDateofBankChallan, 2, 2) + "/" + cmnService.J_Right(strDateofBankChallan, 4);

                        // 19 FILLER 6
                        // 20 FILLER 7
                        // 21 SECTION
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strSection = ListLines[i].Substring(intCaratPosition + 1);
                        strSection = ListLines[i].Substring(intCaratPosition + 1, strSection.Trim().IndexOf("^"));
                        //
                        lngSectionID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT SECTION_ID FROM MST_SECTION WHERE SECTION_NAME = '" + cmnService.J_ReplaceQuote(strSection) + "'");

                        // 22 OLTAS - INCOME TAX 
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strOLTASIncomeTax = ListLines[i].Substring(intCaratPosition + 1);
                        strOLTASIncomeTax = ListLines[i].Substring(intCaratPosition + 1, strOLTASIncomeTax.Trim().IndexOf("^"));

                        // 23 OLTAS - SURCHARGE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strOLTASSurcharge = ListLines[i].Substring(intCaratPosition + 1);
                        strOLTASSurcharge = ListLines[i].Substring(intCaratPosition + 1, strOLTASSurcharge.Trim().IndexOf("^"));

                        // 24 OLTAS - CESS
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strOLTASCess = ListLines[i].Substring(intCaratPosition + 1);
                        strOLTASCess = ListLines[i].Substring(intCaratPosition + 1, strOLTASCess.Trim().IndexOf("^"));

                        // 25 OLTAS - INTEREST AMOUNT
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strOLTASInterestAmount = ListLines[i].Substring(intCaratPosition + 1);
                        strOLTASInterestAmount = ListLines[i].Substring(intCaratPosition + 1, strOLTASInterestAmount.Trim().IndexOf("^"));

                        // 26 OLTAS - OTHERS
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strOLTASOthers = ListLines[i].Substring(intCaratPosition + 1);
                        strOLTASOthers = ListLines[i].Substring(intCaratPosition + 1, strOLTASOthers.Trim().IndexOf("^"));

                        // 27 TOTAL DEPOSITED AMOUNT
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTotalDepositedAmount = ListLines[i].Substring(intCaratPosition + 1);
                        strTotalDepositedAmount = ListLines[i].Substring(intCaratPosition + 1, strTotalDepositedAmount.Trim().IndexOf("^"));

                        // 28 LAST TOTAL DEPOSITED AMOUNT +
                        // 29 TOTAL TAX DEPOSIT AMOUNT
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTotalTaxDepositedAmount = ListLines[i].Substring(intCaratPosition + 1);
                        strTotalTaxDepositedAmount = ListLines[i].Substring(intCaratPosition + 1, strTotalTaxDepositedAmount.Trim().IndexOf("^"));

                        // 30 INCOME TAX
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strIncomeTax = ListLines[i].Substring(intCaratPosition + 1);
                        strIncomeTax = ListLines[i].Substring(intCaratPosition + 1, strIncomeTax.Trim().IndexOf("^"));

                        // 31 SURCHARGE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strSurcharge = ListLines[i].Substring(intCaratPosition + 1);
                        strSurcharge = ListLines[i].Substring(intCaratPosition + 1, strSurcharge.Trim().IndexOf("^"));

                        // 32 CESS
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strCess = ListLines[i].Substring(intCaratPosition + 1);
                        strCess = ListLines[i].Substring(intCaratPosition + 1, strCess.Trim().IndexOf("^"));

                        // 33 SUM OF TOTAL INCOME TAX
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strSumTotalIncomeTax = ListLines[i].Substring(intCaratPosition + 1);
                        strSumTotalIncomeTax = ListLines[i].Substring(intCaratPosition + 1, strSumTotalIncomeTax.Trim().IndexOf("^"));

                        // 34 INTERST AMOUNT
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strInterestAmount = ListLines[i].Substring(intCaratPosition + 1);
                        strInterestAmount = ListLines[i].Substring(intCaratPosition + 1, strInterestAmount.Trim().IndexOf("^"));

                        // 35 OTHERS
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strOthersAmount = ListLines[i].Substring(intCaratPosition + 1);
                        strOthersAmount = ListLines[i].Substring(intCaratPosition + 1, strOthersAmount.Trim().IndexOf("^"));

                        // 36 CHEQUE/DD NUMBER
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strChequeDDNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strChequeDDNumber = ListLines[i].Substring(intCaratPosition + 1, strChequeDDNumber.Trim().IndexOf("^"));
                        //
                        if (strChequeDDNumber == "0")
                            strChequeDDNumber = "";
                        // 37 BOOK ENTRY
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strBookEntry = ListLines[i].Substring(intCaratPosition + 1);
                        strBookEntry = ListLines[i].Substring(intCaratPosition + 1, strBookEntry.Trim().IndexOf("^"));
                        //

                        //Modifed by Shrey Kejriwal on 20/09/2011
                        if (strBookEntry.ToUpper() == "Y")
                            intBookEntry = 1;
                        else if (strBookEntry.ToUpper() == "")
                            intBookEntry = 2;
                        else
                            intBookEntry = 0;

                        // 38 PENDING AMOUNT
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strPendingAmount = ListLines[i].Substring(intCaratPosition + 1);
                        strPendingAmount = ListLines[i].Substring(intCaratPosition + 1, strPendingAmount.Trim().IndexOf("^"));

                        if (strPendingAmount == "")
                            strPendingAmount = "0.00";

                        // 39 RECORD HASH +
                        //######################################
                        // DATA UPDATE
                        //strSQL = "UPDATE TRN_BASIC_INFO SET NIL_RETURN = "  + intNILChallanIndicator + " WHERE BASIC_INFO_ID = " + BasicInfoID;
                        ////
                        //if (dmlService.J_ExecSql(strSQL) == false)
                        //{
                        //    return false;
                        //}
                        // DATA INSERT

                        strSQL = "INSERT INTO COR_HDR_CHALLAN (" +
                                 "            BATCH_HEADER_ID," +
                                 "            SL_NO," +
                                 "            SECTION_ID," +
                                 "            DEPOSIT_DATE," +
                                 "            BSR_CODE," +
                                 "            CHALLAN_NO," +
                                 "            TRANSFER_VOUCHER_NO," +
                                 "            CHEQUE_NO," +
                                 "            TDS," +
                                 "            SURCHARGE," +
                                 "            EDUCATION_CESS," +
                                 "            INTEREST," +
                                 "            OTHERS," +
                                 "            TOT_TAX," +
                                 "            CTRL_TOT_TAX," +
                                 "            INTEREST_ALLOCATED," +
                                 "            OTHERS_ALLOCATED," +
                                 "            REMARKS," +
                                 "            BOOK_ENTRY," +
                                 "            CTRL_TDS," +
                                 "            CTRL_SURCHARGE," +
                                 "            CTRL_EDU_CESS," +
                                 "            CTRL_TOT," +
                                 "            IMPORT_FLAG," +
                                 "            CHALLAN_STATUS, " +
                                 "            EXPECTED_DEDUCTEE_RECORD_NO, " +
                                 "            PENDING_AMOUNT) " +
                                 "     VALUES(" + lngBatchID + "," +
                                 "            " + intCDSerialNo + "," +
                                 "            " + lngSectionID + "," +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strDateofBankChallan) + cmnService.J_DateOperator() + "," +
                                 "           '" + cmnService.J_ReplaceQuote(strBSRCode) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(strChallanNumber) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(strTransferVoucherNumber) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(strChequeDDNumber) + "'," +
                                 "            " + cmnService.J_ReturnDoubleValue(strOLTASIncomeTax) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strOLTASSurcharge) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strOLTASCess) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strOLTASInterestAmount) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strOLTASOthers) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strTotalDepositedAmount) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strTotalTaxDepositedAmount) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strInterestAmount) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strOthersAmount) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(strCDRemarks) + "', " +
                                 "            " + intBookEntry + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strIncomeTax) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strSurcharge) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strCess) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strSumTotalIncomeTax) + "," +
                                 "            1, " +
                                 "           '" + cmnService.J_ReplaceQuote(strChallanStatus) + "'," +
                                 "            " + strExpectedDeducteeSerial + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strPendingAmount) + ")";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                        //
                        strSQL = "SELECT MAX(HDR_CHALLAN_ID) AS CHALLAN_ID FROM COR_HDR_CHALLAN";
                        long lngCOR_HDR_CHALLAN = dmlService.J_ReturnId(strSQL);
                        //
                        strSQL = "INSERT INTO COR_TRN_CHALLAN (" +
                                  "            HDR_CHALLAN_ID," +
                                  "            BATCH_HEADER_ID," +
                                  "            SL_NO," +
                                  "            SECTION_ID," +
                                  "            DEPOSIT_DATE," +
                                  "            BSR_CODE," +
                                  "            CHALLAN_NO," +
                                  "            TRANSFER_VOUCHER_NO," +
                                  "            CHEQUE_NO," +
                                  "            TDS," +
                                  "            SURCHARGE," +
                                  "            EDUCATION_CESS," +
                                  "            INTEREST," +
                                  "            OTHERS," +
                                  "            TOT_TAX," +
                                  "            CTRL_TOT_TAX," +
                                  "            INTEREST_ALLOCATED," +
                                  "            OTHERS_ALLOCATED," +
                                  "            REMARKS," +
                                  "            BOOK_ENTRY," +
                                  "            CTRL_TDS," +
                                  "            CTRL_SURCHARGE," +
                                  "            CTRL_EDU_CESS," +
                                  "            CTRL_TOT," +
                                  "            IMPORT_FLAG," +
                                  "            CHALLAN_STATUS, " +
                                  "            EXPECTED_DEDUCTEE_RECORD_NO) " +
                                  "     SELECT HDR_CHALLAN_ID," +
                                  "            BATCH_HEADER_ID," +
                                  "            SL_NO," +
                                  "            SECTION_ID," +
                                  "            DEPOSIT_DATE," +
                                  "            BSR_CODE," +
                                  "            CHALLAN_NO," +
                                  "            TRANSFER_VOUCHER_NO," +
                                  "            CHEQUE_NO," +
                                  "            TDS," +
                                  "            SURCHARGE," +
                                  "            EDUCATION_CESS," +
                                  "            INTEREST," +
                                  "            OTHERS," +
                                  "            TOT_TAX," +
                                  "            CTRL_TOT_TAX," +
                                  "            INTEREST_ALLOCATED," +
                                  "            OTHERS_ALLOCATED," +
                                  "            REMARKS," +
                                  "            BOOK_ENTRY," +
                                  "            CTRL_TDS," +
                                  "            CTRL_SURCHARGE," +
                                  "            CTRL_EDU_CESS," +
                                  "            CTRL_TOT," +
                                  "            IMPORT_FLAG," +
                                  "            CHALLAN_STATUS, " +
                                  "            EXPECTED_DEDUCTEE_RECORD_NO " +
                                  "     FROM   COR_HDR_CHALLAN " +
                                  "     WHERE  HDR_CHALLAN_ID = " + lngCOR_HDR_CHALLAN;
                        //
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                        //
                        intCDSerialNo = intCDSerialNo + 1;
                        //
                        continue;
                    }

                    #endregion

                    #region DEDUCTEE DETAIL [DD]

                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "DD")
                    {
                        //intCaratPosition = ListLines[i].Trim().IndexOf("^");

                        // 1 LINE NUMBER
                        // 2 RECORD TYPE
                        // 3 BATCH NUMBER
                        // 4 CHALLAN DETAIL RECORD NUMBER
                        // 5 DEDUCTEE DETAIL RECORD NUMBER
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDDRecordNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strDDRecordNumber = ListLines[i].Substring(intCaratPosition + 1, strDDRecordNumber.Trim().IndexOf("^"));

                        // 6 MODE
                        // 7 EMPLOYEE SERIAL NO
                        // 8 DEDUCTEE CODE
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeducteeCode = ListLines[i].Substring(intCaratPosition + 1);
                        strDeducteeCode = ListLines[i].Substring(intCaratPosition + 1, strDeducteeCode.Trim().IndexOf("^"));
                        if (strFormNo != T_FormNo.F24Q)
                            strDeducteeCode = "0" + strDeducteeCode;

                        // 9 LAST EMPLOYEE PAN
                        // 10 DEDUCTEE PAN
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeducteePAN = ListLines[i].Substring(intCaratPosition + 1);
                        strDeducteePAN = ListLines[i].Substring(intCaratPosition + 1, strDeducteePAN.Trim().IndexOf("^"));

                        // 11 LAST PAN REF NO
                        // 12 PAN REF NO
                        // 13 NAME OF DEDUCTEE
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDeducteeName = ListLines[i].Substring(intCaratPosition + 1);
                        strDeducteeName = ListLines[i].Substring(intCaratPosition + 1, strDeducteeName.Trim().IndexOf("^"));

                        // 
                        //if (cmbFormNo.Text == T_FormNo.F24Q)
                        //    lngPartyId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT EMPLOYEE_ID FROM MST_EMPLOYEE WHERE " +
                        //                      "EMPLOYEE_NAME = '" + cmnService.J_ReplaceQuote(strDeducteeName) + "' AND " +
                        //                      "EMPLOYEE_PAN  = '" + cmnService.J_ReplaceQuote(strDeducteePAN) + "'");
                        //else
                        //    lngPartyId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT DEDUCTEE_ID FROM MST_DEDUCTEE WHERE " +
                        //                      "DEDUCTEE_NAME = '" + cmnService.J_ReplaceQuote(strDeducteeName) + "' AND " +
                        //                      "DEDUCTEE_PAN  = '" + cmnService.J_ReplaceQuote(strDeducteePAN) + "'");



                        // 14 TDS INCOME TAX
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTDSIncomeTax = ListLines[i].Substring(intCaratPosition + 1);
                        strTDSIncomeTax = ListLines[i].Substring(intCaratPosition + 1, strTDSIncomeTax.Trim().IndexOf("^"));

                        // 15 TDS SURCHARGE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTDSSurcharge = ListLines[i].Substring(intCaratPosition + 1);
                        strTDSSurcharge = ListLines[i].Substring(intCaratPosition + 1, strTDSSurcharge.Trim().IndexOf("^"));

                        // 16 TDS CESS
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTDSCess = ListLines[i].Substring(intCaratPosition + 1);
                        strTDSCess = ListLines[i].Substring(intCaratPosition + 1, strTDSCess.Trim().IndexOf("^"));

                        // 17 TOTAL INCOME TAX DEDUCTED
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTotalIncomeTaxDeducted = ListLines[i].Substring(intCaratPosition + 1);
                        strTotalIncomeTaxDeducted = ListLines[i].Substring(intCaratPosition + 1, strTotalIncomeTaxDeducted.Trim().IndexOf("^"));

                        // 18 LAST TOTAL INCOME TAX DEDUCTED
                        // 19 TOTAL TAX DEPOSITED
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTotalTaxDeposited = ListLines[i].Substring(intCaratPosition + 1);
                        strTotalTaxDeposited = ListLines[i].Substring(intCaratPosition + 1, strTotalTaxDeposited.Trim().IndexOf("^"));

                        // 20 LAST TOTAL TAX DEPOSITED
                        // 21 TOTAL VALUE OF PURCHASE
                        if (strFormNo == T_FormNo.F27EQ)
                        {
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strTotalValueOfPurchase = ListLines[i].Substring(intCaratPosition + 1);
                            strTotalValueOfPurchase = ListLines[i].Substring(intCaratPosition + 1, strTotalValueOfPurchase.Trim().IndexOf("^"));

                            intLoop = 2;
                        }
                        else
                            intLoop = 4;

                        // 22 AMOUNT OF PAYMENT
                        for (int a = 1; a < intLoop; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strAmountOfPayment = ListLines[i].Substring(intCaratPosition + 1);
                        strAmountOfPayment = ListLines[i].Substring(intCaratPosition + 1, strAmountOfPayment.Trim().IndexOf("^"));

                        // 23 DATE ON WHICH AMOUNT PAID
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDateAmountPaid = ListLines[i].Substring(intCaratPosition + 1);
                        strDateAmountPaid = ListLines[i].Substring(intCaratPosition + 1, strDateAmountPaid.Trim().IndexOf("^"));
                        strDateAmountPaid = cmnService.J_Left(strDateAmountPaid, 2) + "/" + cmnService.J_Mid(strDateAmountPaid, 2, 2) + "/" + cmnService.J_Right(strDateAmountPaid, 4);

                        // 24 DATE ON WHICH TAX DEDUCTED
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDateTaxDeducted = ListLines[i].Substring(intCaratPosition + 1);
                        strDateTaxDeducted = ListLines[i].Substring(intCaratPosition + 1, strDateTaxDeducted.Trim().Trim().IndexOf("^"));
                        if (strDateTaxDeducted.Trim() == "")
                            strDateTaxDeducted = "NULL";
                        else
                            strDateTaxDeducted = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(cmnService.J_Left(strDateTaxDeducted, 2) + "/" + cmnService.J_Mid(strDateTaxDeducted, 2, 2) + "/" + cmnService.J_Right(strDateTaxDeducted, 4)) + cmnService.J_DateOperator();

                        // 25 DATE OF DEPOSIT
                        // 26 RATE
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRate = ListLines[i].Substring(intCaratPosition + 1);
                        strRate = ListLines[i].Substring(intCaratPosition + 1, strRate.Trim().IndexOf("^"));

                        // 27 GROSSING UP INDICATOR
                        if (strFormNo == T_FormNo.F27Q)
                        {
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strGrossingUpIndicator = ListLines[i].Substring(intCaratPosition + 1);
                            strGrossingUpIndicator = ListLines[i].Substring(intCaratPosition + 1, strGrossingUpIndicator.Trim().IndexOf("^"));

                            intLoop = 2;
                        }
                        else
                            intLoop = 3;

                        // 28 BOOK ENTRY/CASH INDICATOR
                        for (int a = 1; a < intLoop; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strBookEntry = ListLines[i].Substring(intCaratPosition + 1);
                        strBookEntry = ListLines[i].Substring(intCaratPosition + 1, strBookEntry.Trim().IndexOf("^"));

                        if (strBookEntry.ToUpper() == "Y")
                            intBookEntry = 1;
                        else
                            intBookEntry = 0;

                        // 29 DATE OF FURNISHING TAX DEDUCTION CERTIFICATE
                        // 30 REMARKS 1
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strRemarks1 = ListLines[i].Substring(intCaratPosition + 1);
                        strRemarks1 = ListLines[i].Substring(intCaratPosition + 1, strRemarks1.Trim().IndexOf("^"));

                        //Added by Shrey Kejriwal on 24/11/2011
                        // 31 PAN FLAG

                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strPanFlag = ListLines[i].Substring(intCaratPosition + 1);
                        strPanFlag = ListLines[i].Substring(intCaratPosition + 1, strPanFlag.Trim().IndexOf("^"));

                        if (strPanFlag.ToUpper() == "N")
                            intPanFlag = 1; //invalid pan
                        else
                            intPanFlag = 0;

                        //Added by Shrey Kejriwal on 24/11/2011
                        // 32 Pan Counter
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strPanCounter = ListLines[i].Substring(intCaratPosition + 1);
                        strPanCounter = ListLines[i].Substring(intCaratPosition + 1, strPanCounter.Trim().IndexOf("^"));

                        if (strPanCounter == "")
                            intPanCounter = 0;
                        else
                            intPanCounter = Convert.ToInt32(strPanCounter);


                        // 33 RECORD HASH

                        // GET CHALLAN ID
                        lngChallanID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT MAX(HDR_CHALLAN_ID) FROM COR_HDR_CHALLAN");

                        // DATA INSERT
                        strSQL = "INSERT INTO COR_HDR_DEDUCTEE_DETAILS " +
                                 "           (HDR_CHALLAN_ID," +
                                 "            BATCH_HEADER_ID," +
                                 "            SL_NO," +
                                 "            DEDUCTEE_NAME," +
                                 "            DEDUCTEE_PAN," +
                                 "            DEDUCTEE_CODE," +
                                 "            PAYMENT_DATE," +
                                 "            DEDUCTED_DATE," +
                                 "            PAYMENT_AMOUNT," +
                                 "            RATE," +
                                 "            TAX_AMOUNT," +
                                 "            SURCHARGE_AMOUNT," +
                                 "            CESS_AMOUNT," +
                                 "            TOTAL_AMOUNT," +
                                 "            TAX_DEPOSITED_AMOUNT,";
                        if (strFormNo == T_FormNo.F27Q)
                            strSQL = strSQL + "GROSSING_UP_INDICATOR,";
                        else if (strFormNo == T_FormNo.F27EQ)
                            strSQL = strSQL + "TOT_VALUE_PURCHASE,";

                        strSQL = strSQL + "   NON_DEDUCTION_FLAG," +
                                 "            IMPORT_FLAG," +
                                 "            CASH_BOOK_ENTRY," +
                                 "            INVALID_PAN," +               //Added by Shrey on 24/11/2011
                                 "            PAN_COUNTER) " +              //Added by Shrey on 24/11/2011
                                 "     VALUES(" + lngChallanID + "," +
                                 "            " + lngBatchID + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strDDRecordNumber) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(strDeducteeName.ToUpper()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(strDeducteePAN) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(strDeducteeCode) + "'," +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strDateAmountPaid) + cmnService.J_DateOperator() + "," +
                                 "            " + strDateTaxDeducted + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strAmountOfPayment) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strRate) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strTDSIncomeTax) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strTDSSurcharge) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strTDSCess) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strTotalIncomeTaxDeducted) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(strTotalTaxDeposited) + ",";
                        if (strFormNo == T_FormNo.F27Q)
                            strSQL = strSQL + "'" + cmnService.J_ReplaceQuote(strGrossingUpIndicator) + "',";
                        else if (strFormNo == T_FormNo.F27EQ)
                            strSQL = strSQL + " " + cmnService.J_ReturnDoubleValue(strTotalValueOfPurchase) + ",";

                        strSQL = strSQL + "'" + cmnService.J_ReplaceQuote(strRemarks1) + "'," +
                                          "1," + intBookEntry + ", " + intPanFlag + ", " + intPanCounter + ")";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                        //
                        strSQL = "SELECT MAX(HDR_DEDUCTEE_DETAIL_ID) AS DEDUCTEE_DETAIL_ID FROM COR_HDR_DEDUCTEE_DETAILS";
                        long lngCOR_HDR_DEDUCTEE_DETAILS = dmlService.J_ReturnId(strSQL);
                        //
                        strSQL = "SELECT MAX(TRN_CHALLAN_ID) AS TRN_CHALLAN_ID FROM COR_TRN_CHALLAN";
                        long lngCOR_TRN_CHALLAN = dmlService.J_ReturnId(strSQL);
                        //
                        strSQL = "INSERT INTO COR_TRN_DEDUCTEE_DETAILS " +
                                 "           (TRN_CHALLAN_ID," +
                                 "            HDR_DEDUCTEE_DETAIL_ID," +
                                 "            BATCH_HEADER_ID," +
                                 "            SL_NO," +
                                 "            DEDUCTEE_NAME," +
                                 "            DEDUCTEE_PAN," +
                                 "            DEDUCTEE_CODE," +
                                 "            PAYMENT_DATE," +
                                 "            DEDUCTED_DATE," +
                                 "            PAYMENT_AMOUNT," +
                                 "            RATE," +
                                 "            TAX_AMOUNT," +
                                 "            SURCHARGE_AMOUNT," +
                                 "            CESS_AMOUNT," +
                                 "            TOTAL_AMOUNT," +
                                 "            TAX_DEPOSITED_AMOUNT,";

                        if (strFormNo == T_FormNo.F27Q)
                            strSQL = strSQL + "GROSSING_UP_INDICATOR,";
                        else if (strFormNo == T_FormNo.F27EQ)
                            strSQL = strSQL + "TOT_VALUE_PURCHASE,";

                        strSQL = strSQL + "NON_DEDUCTION_FLAG," +
                                 "            IMPORT_FLAG," +
                                 "            CASH_BOOK_ENTRY) " +
                                 "     SELECT " + lngCOR_TRN_CHALLAN + "," +
                                 "            HDR_DEDUCTEE_DETAIL_ID," +
                                 "            BATCH_HEADER_ID," +
                                 "            SL_NO," +
                                 "            DEDUCTEE_NAME," +
                                 "            DEDUCTEE_PAN," +
                                 "            DEDUCTEE_CODE," +
                                 "            PAYMENT_DATE," +
                                 "            DEDUCTED_DATE," +
                                 "            PAYMENT_AMOUNT," +
                                 "            RATE," +
                                 "            TAX_AMOUNT," +
                                 "            SURCHARGE_AMOUNT," +
                                 "            CESS_AMOUNT," +
                                 "            TOTAL_AMOUNT," +
                                 "            TAX_DEPOSITED_AMOUNT,";

                        if (strFormNo == T_FormNo.F27Q)
                            strSQL = strSQL + "GROSSING_UP_INDICATOR,";
                        else if (strFormNo == T_FormNo.F27EQ)
                            strSQL = strSQL + "TOT_VALUE_PURCHASE,";

                        strSQL = strSQL + "   NON_DEDUCTION_FLAG," +
                                 "            IMPORT_FLAG," +
                                 "            CASH_BOOK_ENTRY " +
                                 "      FROM  COR_HDR_DEDUCTEE_DETAILS " +
                                 "      WHERE HDR_DEDUCTEE_DETAIL_ID  = " + lngCOR_HDR_DEDUCTEE_DETAILS;
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }

                        //
                        continue;
                    }

                    #endregion

                    #region SALARY DETAIL [SD]

                    if (strFormNo == T_FormNo.F24Q && strQTR == T_Qtr.Q4)
                    {
                        if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "SD")
                        {
                            //intCaratPosition = ListLines[i].Trim().IndexOf("^");

                            // 1 LINE NUMBER
                            // 2 RECORD TYPE
                            // 3 BATCH NUMBER
                            // 4 SALARY DETAIL RECORD NUMBER
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strSDRecordNumber = ListLines[i].Substring(intCaratPosition + 1);
                            strSDRecordNumber = ListLines[i].Substring(intCaratPosition + 1, strSDRecordNumber.Trim().IndexOf("^"));

                            // 5 MODE
                            // 6 FILLER
                            // 7 EMPLOYEE PAN
                            for (int a = 1; a < 4; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strEmployeePAN = ListLines[i].Substring(intCaratPosition + 1);
                            strEmployeePAN = ListLines[i].Substring(intCaratPosition + 1, strEmployeePAN.Trim().IndexOf("^"));

                            // 8 PAN REF NO
                            // 9 EMPLOYEE NAME
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strEmployeeName = ListLines[i].Substring(intCaratPosition + 1);
                            strEmployeeName = ListLines[i].Substring(intCaratPosition + 1, strEmployeeName.Trim().IndexOf("^"));

                            // 10 CATEGORY OF EMPLOYEE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strEmployeeCategory = ListLines[i].Substring(intCaratPosition + 1);
                            strEmployeeCategory = ListLines[i].Substring(intCaratPosition + 1, strEmployeeCategory.Trim().IndexOf("^"));

                            // GET PARTY ID
                            //lngPartyId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT EMPLOYEE_ID FROM MST_EMPLOYEE WHERE " +
                            //                  "EMPLOYEE_NAME ='" + cmnService.J_ReplaceQuote(strEmployeeName) + "' AND " +
                            //                  "EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(strEmployeePAN) + "'");

                            // 11 PERIOD OF EMPLOYMENT FROM DATE 
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strPeriodFromDate = ListLines[i].Substring(intCaratPosition + 1);
                            strPeriodFromDate = ListLines[i].Substring(intCaratPosition + 1, strPeriodFromDate.Trim().IndexOf("^"));
                            strPeriodFromDate = cmnService.J_Left(strPeriodFromDate, 2) + "/" + cmnService.J_Mid(strPeriodFromDate, 2, 2) + "/" + cmnService.J_Right(strPeriodFromDate, 4);

                            // 12 PERIOD OF EMPLOYMENT TO DATE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strPeriodToDate = ListLines[i].Substring(intCaratPosition + 1);
                            strPeriodToDate = ListLines[i].Substring(intCaratPosition + 1, strPeriodToDate.Trim().IndexOf("^"));
                            strPeriodToDate = cmnService.J_Left(strPeriodToDate, 2) + "/" + cmnService.J_Mid(strPeriodToDate, 2, 2) + "/" + cmnService.J_Right(strPeriodToDate, 4);

                            // 13 TOTAL AMOUNT OF SALARY
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strTotalAmountSalary = ListLines[i].Substring(intCaratPosition + 1);
                            strTotalAmountSalary = ListLines[i].Substring(intCaratPosition + 1, strTotalAmountSalary.Trim().IndexOf("^"));

                            // 14 FILLER
                            // 15 COUNT OF SECTION 16 DETAIL RECORDS
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strCountSec16Records = ListLines[i].Substring(intCaratPosition + 1);
                            strCountSec16Records = ListLines[i].Substring(intCaratPosition + 1, strCountSec16Records.Trim().IndexOf("^"));

                            // 16 GROSS TOTAL OF SECTION 16
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strGrossTotalSec16 = ListLines[i].Substring(intCaratPosition + 1);
                            strGrossTotalSec16 = ListLines[i].Substring(intCaratPosition + 1, strGrossTotalSec16.Trim().IndexOf("^"));

                            // 17 INCOME CHARGEABLE UNDER HEAD SALARIES
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strIncomeChargeableUnderHeadSalaries = ListLines[i].Substring(intCaratPosition + 1);
                            strIncomeChargeableUnderHeadSalaries = ListLines[i].Substring(intCaratPosition + 1, strIncomeChargeableUnderHeadSalaries.Trim().IndexOf("^"));

                            // 18 INCOME OTHER THAN SALARIES
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strIncomeOtherThanSalaries = ListLines[i].Substring(intCaratPosition + 1);
                            strIncomeOtherThanSalaries = ListLines[i].Substring(intCaratPosition + 1, strIncomeOtherThanSalaries.Trim().IndexOf("^"));

                            // 19 GROSS TOTAL INCOME
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strGrossTotalIncome = ListLines[i].Substring(intCaratPosition + 1);
                            strGrossTotalIncome = ListLines[i].Substring(intCaratPosition + 1, strGrossTotalIncome.Trim().IndexOf("^"));

                            // 20 LAST GROSS TOTAL INCOME
                            // 21 COUNT OF CHAPTER VI-A DETAIL RECORDS
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strCountChVIARecords = ListLines[i].Substring(intCaratPosition + 1);
                            strCountChVIARecords = ListLines[i].Substring(intCaratPosition + 1, strCountChVIARecords.Trim().IndexOf("^"));

                            // 22 GROSS TOTAL OF CHAPTER VI-A DETAIL RECORDS
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strGrossTotalChVIA = ListLines[i].Substring(intCaratPosition + 1);
                            strGrossTotalChVIA = ListLines[i].Substring(intCaratPosition + 1, strGrossTotalChVIA.Trim().IndexOf("^"));

                            // 23 TOTAL TAXABLE INCOME
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strTotalTaxableIncome = ListLines[i].Substring(intCaratPosition + 1);
                            strTotalTaxableIncome = ListLines[i].Substring(intCaratPosition + 1, strTotalTaxableIncome.Trim().IndexOf("^"));

                            // 24 INCOME TAX ON TOTAL INCOME
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strIncomeTaxOnTotalIncome = ListLines[i].Substring(intCaratPosition + 1);
                            strIncomeTaxOnTotalIncome = ListLines[i].Substring(intCaratPosition + 1, strIncomeTaxOnTotalIncome.Trim().IndexOf("^"));

                            // 25 SURCHARGE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strSurchargeSD = ListLines[i].Substring(intCaratPosition + 1);
                            strSurchargeSD = ListLines[i].Substring(intCaratPosition + 1, strSurchargeSD.Trim().IndexOf("^"));

                            // 26 EDUCATION CESS
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strEducationCess = ListLines[i].Substring(intCaratPosition + 1);
                            strEducationCess = ListLines[i].Substring(intCaratPosition + 1, strEducationCess.Trim().IndexOf("^"));

                            // 27 INCOME TAX RELIEF U/S 89
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strIncomeTaxRelief = ListLines[i].Substring(intCaratPosition + 1);
                            strIncomeTaxRelief = ListLines[i].Substring(intCaratPosition + 1, strIncomeTaxRelief.Trim().IndexOf("^"));

                            // 28 NET INCOME TAX PAYABLE
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strNetIncomeTaxPayable = ListLines[i].Substring(intCaratPosition + 1);
                            strNetIncomeTaxPayable = ListLines[i].Substring(intCaratPosition + 1, strNetIncomeTaxPayable.Trim().IndexOf("^"));

                            // 29 TOTAL AMOUNT OF TAX DEDUCTED
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strTotalAmountOfTaxDeducted = ListLines[i].Substring(intCaratPosition + 1);
                            strTotalAmountOfTaxDeducted = ListLines[i].Substring(intCaratPosition + 1, strTotalAmountOfTaxDeducted.Trim().IndexOf("^"));

                            // 30 SHORTFALL/EXCESS
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strShortfallExcess = ListLines[i].Substring(intCaratPosition + 1);
                            strShortfallExcess = ListLines[i].Substring(intCaratPosition + 1, strShortfallExcess.Trim().IndexOf("^"));

                            //Added by Shrey Kejriwal on 24/11/2011
                            // 31 PAN FLAG
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strPanFlag = ListLines[i].Substring(intCaratPosition + 1);
                            strPanFlag = ListLines[i].Substring(intCaratPosition + 1, strPanFlag.Trim().IndexOf("^"));

                            if (strPanFlag.ToUpper() == "N")
                                intPanFlag = 1; //invalid pan
                            else
                                intPanFlag = 0;

                            // 32 Pan Counter
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strPanCounter = ListLines[i].Substring(intCaratPosition + 1);
                            strPanCounter = ListLines[i].Substring(intCaratPosition + 1, strPanCounter.Trim().IndexOf("^"));

                            if (strPanCounter == "")
                                intPanCounter = 0;
                            else
                                intPanCounter = Convert.ToInt32(strPanCounter);
                            
                            
                            // 33 RAMARKS 3
                            // 34 RECORD HASH
                            // DATA INSERT
                            //-----------------------------------------------------------
                            strSQL = "INSERT INTO COR_HDR_SALARY_DETAILS (" +
                                    "             BATCH_HEADER_ID," +
                                    "             EMPLOYEE_NAME," +
                                    "             EMPLOYEE_PAN," +
                                    "             EMPLOYEE_CATEGORY," +
                                    "             SL_NO," +
                                    "             FROM_DATE," +
                                    "             TO_DATE," +
                                    "             TS_BALANCE," +
                                    "             US_16_AGGREGATE," +
                                    "             INCOME_CHARGEABLE," +
                                    "             AIS_TOTAL," +
                                    "             GROSS_TOTAL_INCOME," +
                                    "             CVIA_DED_TOTAL," +
                                    "             TOTAL_INCOME," +
                                    "             TAX_TOTAL_INCOME," +
                                    "             SCHG_TOTAL_INCOME," +
                                    "             ECESS_TOTAL_INCOME," +
                                    "             TAX_PAYABLE_AGGREGATE," +
                                    "             US_89_LESS," +
                                    "             TAX_PAYABLE," +
                                    "             TOTAL_TDS_DEDUCTED," +
                                    "             SHORTFALL_TAX," +
                                    "             IMPORT_FLAG," +
                                    "             INVALID_PAN," +               //Added by Shrey on 24/11/2011
                                    "             PAN_COUNTER) " +              //Added by Shrey on 24/11/2011 " +
                                    "     VALUES(" + lngBatchID + "," +
                                    "           '" + cmnService.J_ReplaceQuote(strEmployeeName) + "'," +
                                    "           '" + cmnService.J_ReplaceQuote(strEmployeePAN) + "'," +
                                    "           '" + cmnService.J_ReplaceQuote(strEmployeeCategory) + "'," +
                                    "            " + cmnService.J_ReturnInt32Value(strSDRecordNumber) + "," +
                                    "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strPeriodFromDate) + cmnService.J_DateOperator() + "," +
                                    "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strPeriodToDate) + cmnService.J_DateOperator() + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strTotalAmountSalary) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strGrossTotalSec16) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strIncomeChargeableUnderHeadSalaries) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strIncomeOtherThanSalaries) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strGrossTotalIncome) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strGrossTotalChVIA) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strTotalTaxableIncome) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strIncomeTaxOnTotalIncome) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strSurchargeSD) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strEducationCess) + "," +
                                    "            " + (cmnService.J_ReturnDoubleValue(strIncomeTaxOnTotalIncome) + cmnService.J_ReturnDoubleValue(strSurchargeSD) + cmnService.J_ReturnDoubleValue(strEducationCess)) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strIncomeTaxRelief) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strNetIncomeTaxPayable) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strTotalAmountOfTaxDeducted) + "," +
                                    "            " + cmnService.J_ReturnDoubleValue(strShortfallExcess) + "," +
                                    "            1," + intPanFlag + ", " + intPanCounter + ")";
                            //-----------------------------------------------------------
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                return false;
                            }
                            //
                            strSQL = "SELECT MAX(HDR_SALARY_DETAILS_ID) AS SALARY_DETAIL_ID FROM COR_HDR_SALARY_DETAILS";
                            lngCOR_HDR_SALARY_DETAILS = dmlService.J_ReturnId(strSQL);
                            //
                            strSQL = "INSERT INTO COR_TRN_SALARY_DETAILS (" +
                                    "             BATCH_HEADER_ID," +
                                    "             HDR_SALARY_DETAILS_ID," +
                                    "             EMPLOYEE_NAME," +
                                    "             EMPLOYEE_PAN," +
                                    "             EMPLOYEE_CATEGORY," +
                                    "             SL_NO," +
                                    "             FROM_DATE," +
                                    "             TO_DATE," +
                                    "             TS_BALANCE," +
                                    "             US_16_AGGREGATE," +
                                    "             INCOME_CHARGEABLE," +
                                    "             AIS_TOTAL," +
                                    "             GROSS_TOTAL_INCOME," +
                                    "             CVIA_DED_TOTAL," +
                                    "             TOTAL_INCOME," +
                                    "             TAX_TOTAL_INCOME," +
                                    "             SCHG_TOTAL_INCOME," +
                                    "             ECESS_TOTAL_INCOME," +
                                    "             TAX_PAYABLE_AGGREGATE," +
                                    "             US_89_LESS," +
                                    "             TAX_PAYABLE," +
                                    "             TOTAL_TDS_DEDUCTED," +
                                    "             SHORTFALL_TAX," +
                                    "             IMPORT_FLAG) " +
                                    "     SELECT  BATCH_HEADER_ID," +
                                    "         " + lngCOR_HDR_SALARY_DETAILS + "," +
                                    "             EMPLOYEE_NAME," +
                                    "             EMPLOYEE_PAN," +
                                    "             EMPLOYEE_CATEGORY," +
                                    "             SL_NO," +
                                    "             FROM_DATE," +
                                    "             TO_DATE," +
                                    "             TS_BALANCE," +
                                    "             US_16_AGGREGATE," +
                                    "             INCOME_CHARGEABLE," +
                                    "             AIS_TOTAL," +
                                    "             GROSS_TOTAL_INCOME," +
                                    "             CVIA_DED_TOTAL," +
                                    "             TOTAL_INCOME," +
                                    "             TAX_TOTAL_INCOME," +
                                    "             SCHG_TOTAL_INCOME," +
                                    "             ECESS_TOTAL_INCOME," +
                                    "             TAX_PAYABLE_AGGREGATE," +
                                    "             US_89_LESS," +
                                    "             TAX_PAYABLE," +
                                    "             TOTAL_TDS_DEDUCTED," +
                                    "             SHORTFALL_TAX," +
                                    "             IMPORT_FLAG " +
                                    "     FROM    COR_HDR_SALARY_DETAILS " +
                                    "     WHERE   HDR_SALARY_DETAILS_ID = " + lngCOR_HDR_SALARY_DETAILS;
                            //-----------------------------------------------------------
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                return false;
                            }

                            continue;
                        }
                        //
                        //continue;
                        //}              


                    #endregion

                        #region SALARY DETAIL [SD-S16]

                        //if (strFormNo == T_FormNo.F24Q && strQTR == T_Qtr.Q4)
                        //{
                        if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 3) == "S16")
                        {
                            //intCaratPosition = ListLines[i].Trim().IndexOf("^");

                            // 1 LINE NUMBER
                            // 2 RECORD TYPE
                            // 3 BATCH NUMBER
                            // 4 SALARY DETAIL RECORD NUMBER
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strSDS16RecordNumber = ListLines[i].Substring(intCaratPosition + 1);
                            strSDS16RecordNumber = ListLines[i].Substring(intCaratPosition + 1, strSDS16RecordNumber.Trim().IndexOf("^"));

                            // 5 SALARY DETAIL - SECTION 16 RECORD NUMBER
                            // 6 SECTION 16 ID
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strS16SectionID = ListLines[i].Substring(intCaratPosition + 1);
                            strS16SectionID = ListLines[i].Substring(intCaratPosition + 1, strS16SectionID.Trim().IndexOf("^"));

                            // 7 TOTAL DEDUCTION
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strS16TotalDeduction = ListLines[i].Substring(intCaratPosition + 1);
                            strS16TotalDeduction = ListLines[i].Substring(intCaratPosition + 1, strS16TotalDeduction.Trim().IndexOf("^"));

                            // 8 RECORD HASH
                            string strUS_16Field = "";
                            //
                            if (strS16SectionID == "16(ii)")
                                strUS_16Field = "US_16_EA";
                            else
                                strUS_16Field = "US_16_TE";
                            //
                            strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET" +
                                "            " + strUS_16Field + " = " + cmnService.J_ReturnDoubleValue(strS16TotalDeduction) + " " +
                                "     WHERE  HDR_SALARY_DETAILS_ID =" + lngCOR_HDR_SALARY_DETAILS + " " +
                                "     AND    BATCH_HEADER_ID       = " + lngBatchID;
                            //(SELECT MAX(SALARY_DETAILS_ID) FROM COR_TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID + ")";
                            //-----------------------------------------------
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                return false;
                            }
                            //
                            strSQL = "UPDATE COR_TRN_SALARY_DETAILS SET" +
                               "            " + strUS_16Field + " = " + cmnService.J_ReturnDoubleValue(strS16TotalDeduction) + " " +
                               "     WHERE  HDR_SALARY_DETAILS_ID =" + lngCOR_HDR_SALARY_DETAILS + " " +
                               "     AND    BATCH_HEADER_ID       = " + lngBatchID;
                            //(SELECT MAX(SALARY_DETAILS_ID) FROM COR_TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID + ")";
                            //-----------------------------------------------
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                return false;
                            }
                            //
                            continue;
                            //}
                        }
                        #endregion

                        #region SALARY DETAIL [SD-C6A]

                        //if (strFormNo == T_FormNo.F24Q && strQTR == T_Qtr.Q4)
                        //{
                        if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 3) == "C6A")
                        {
                            //intCaratPosition = ListLines[i].Trim().IndexOf("^");

                            // 1 LINE NUMBER
                            // 2 RECORD TYPE
                            // 3 BATCH NUMBER
                            // 4 SALARY DETAIL RECORD NUMBER
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strSDC6ARecordNumber = ListLines[i].Substring(intCaratPosition + 1);
                            strSDC6ARecordNumber = ListLines[i].Substring(intCaratPosition + 1, strSDC6ARecordNumber.Trim().IndexOf("^"));

                            // 5 SALARY DETAIL - CHAPTER VI A RECORD NUMBER
                            // 6 CHAPTER VI A ID
                            for (int a = 1; a < 3; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strC6ASectionID = ListLines[i].Substring(intCaratPosition + 1);
                            strC6ASectionID = ListLines[i].Substring(intCaratPosition + 1, strC6ASectionID.Trim().IndexOf("^"));

                            // 7 TOTAL AMOUNT UNDER CHAPTER VI A
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            strC6ATotalAmount = ListLines[i].Substring(intCaratPosition + 1);
                            strC6ATotalAmount = ListLines[i].Substring(intCaratPosition + 1, strC6ATotalAmount.Trim().IndexOf("^"));

                            // 8 RECORD HASH
                            string strC6AField = "";
                            //
                            if (strC6ASectionID == "80CCE")
                                strC6AField = "CVIA_SEC80CCE_TOTAL_DED_AMOUNT";
                            else if (strC6ASectionID == "80CCF")
                                strC6AField = "CVIA_SEC80CCF_DED_AMOUNT";
                            else
                                strC6AField = "CVIA_OTH_DED_TOTAL";
                            //
                            strSQL = "UPDATE COR_HDR_SALARY_DETAILS SET" +
                                "            " + strC6AField + " = " + cmnService.J_ReturnDoubleValue(strC6ATotalAmount) + " " +
                                "     WHERE  HDR_SALARY_DETAILS_ID =" + lngCOR_HDR_SALARY_DETAILS + " " +
                                "     AND    BATCH_HEADER_ID       = " + lngBatchID;
                            //SALARY_DETAILS_ID = (SELECT MAX(SALARY_DETAILS_ID) FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID + ")";
                            //-----------------------------------------------
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                return false;
                            }
                            //
                            strSQL = "UPDATE COR_TRN_SALARY_DETAILS SET" +
                                 "            " + strC6AField + " = " + cmnService.J_ReturnDoubleValue(strC6ATotalAmount) + " " +
                                 "     WHERE  HDR_SALARY_DETAILS_ID =" + lngCOR_HDR_SALARY_DETAILS + " " +
                                 "     AND    BATCH_HEADER_ID       = " + lngBatchID;
                            //SALARY_DETAILS_ID = (SELECT MAX(SALARY_DETAILS_ID) FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID + ")";
                            //-----------------------------------------------
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                return false;
                            }
                            //
                            //continue;
                            continue;
                        }
                        //}
                    }
                        #endregion

                }
                if(blnCancel == false) dmlService.J_Commit();

                return true;
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage("Importing Failed");
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

        #endregion

    }
}

