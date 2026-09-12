
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
    public partial class TrnConsolidatedStatement_FVUImport_New : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnConsolidatedStatement_FVUImport_New()
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
            
        #endregion

        #region enum for data structure

        #region enum T_FileHeader
        public enum T_FileHeader
        {
            //FH Structure
            LineNumber = 0,
            RecordType = 1,
            FileType = 2,
            UploadType = 3,
            FileCreationDate = 4,
            FileSequenceNumber = 5,
            UploaderType = 6,
            TFCIDOrganizationID = 7,
            TotalNumberOfBatches = 8,
            CountOflineInfile = 9,
            DataAsOn = 10,
            Filler = 11,
            Filler1 = 12,
            Filler2 = 13,
            FileHash = 14,
            Filler3 = 15,
            Filler4 = 16,
            Filler5 = 17
        }
        #endregion

        #region enum T_BatchHeader
        public enum T_BatchHeader
        {
            //BH Structure
            Linenumber = 0,
            RecordType = 1,
            BatchNumber = 2,
            CountofChallanRecords = 3,
            FormNumber = 4,
            TransactionType = 5,
            BatchUpdationIndicator = 6,
            OriginalRRRnumber = 7,
            PreviousRRRnumber = 8,
            RRRnumber = 9,
            RRRDate = 10,
            LastTAN = 11,
            TAN = 12,
            ExpectedChallanRecordNumber = 13,
            PAN = 14,
            AssessmentYr = 15,
            FinancialYr = 16,
            Period = 17,
            NameofDeductor = 18,
            BranchDivision = 19,
            Address1 = 20,
            Address2 = 21,
            Address3 = 22,
            Address4 = 23,
            Address5 = 24,
            State = 25,
            PIN = 26,
            EmailID = 27,
            STD = 28,
            Telephonenumber = 29,
            ChangeofAddressofDeductor = 30,
            DeductorType = 31,
            NameofPersonresponsible = 32,
            Designation = 33,
            ResponsiblePersonsAddress1 = 34,
            ResponsiblePersonsAddress2 = 35,
            ResponsiblePersonsAddress3 = 36,
            ResponsiblePersonsAddress4 = 37,
            ResponsiblePersonsAddress5 = 38,
            ResponsiblePersonsState = 39,
            ResponsiblePersonsPIN = 40,
            ResponsiblePersonsEmailID = 41,
            Remark = 42,
            ResponsiblePersonsSTDCODE = 43,
            ResponsiblePersonsTelephonenumber = 44,
            ChangeofAddressofResponsibleperson = 45,
            BatchTotalofTotalofDepositAmountasperChallan = 46,
            Mobileno = 47,
            CountofSalaryDetailsRecords = 48,
            BatchTotalofGrossTotalIncomeasperSalaryDetail = 49,
            AOApproval = 50,
            AOApprovalnumber = 51,
            LastDeductorCollectorType = 52,
            StateName = 53,
            PAOCode = 54,
            DDOCode = 55,
            MinistryName = 56,
            MinistryNameOther = 57,
            RegistrationID = 58,
            PAORegistrationnumber = 59,
            DDORegistrationnumber = 60,
            RecordHash = 61

        }
        #endregion

        #region enum T_Challan_detail
        public enum T_Challan_detail
        {
            Linenumber = 0,
            RecordType = 1,
            Batchnumber = 2,
            ChallanDetailRecordnumber = 3,
            CountofDeducteePartyRecords = 4,
            NILChallanIndicator = 5,
            ChallanUpdationIndicator = 6,
            ExpectedDeducteeserialnumber = 7,
            ChallanMatchingIndicator = 8,
            Filler = 9,
            LastBankChallanNo = 10,
            BankChallanNo = 11,
            LastTransferVoucher= 12,
            TransferVoucher = 13,
            LastBankBranchCode = 14,
            BankBranchCode = 15,
            LastDateofBankChallanNo= 16,
            DateofBankChallanNo = 17,
            Filler5 = 18,
            Filler6 = 19,
            SectionCollectionCode = 20,
            OltasTDSTCSIncomeTax = 21,
            OltasTDSTCSSurcharge = 22,
            OltasTDSTCSCess = 23,
            OltasTDSTCSInterestAmount = 24,
            OltasTDSTCSOthersAmount = 25,
            TotalDepositAmountasperChallan = 26,
            LastTotalofDepositAmountasperChallan = 27,
            TotalTaxDepositAmountasperdeducteeannexure = 28,
            TDSTCSIncomeTax = 29,
            TDSTCSSurcharge = 30,
            TDSTCSCess = 31,
            SumofTotalIncomeTaxDeductedatSource = 32,
            TDSTCSInterestAmount = 33,
            TDSTCSOthersAmount = 34,
            ChequeDDnumber = 35,
            ByBookentryCash = 36,
            PendingAmount = 37,
            RecordHash = 38
        }
        #endregion

        #region enum T_deductee_detail
        public enum T_deductee_detail
        {
            Linenumber = 0,
            RecordType = 1,
            Batchnumber = 2,
            ChallanDetailRecordnumber = 3,
            DeducteeDetailRecordNo = 4,
            Mode = 5,
            EmployeeSerialNo = 6,
            DeducteeCode = 7,
            LastEmployeePAN = 8,
            EmployeePAN = 9,
            LastEmployeePartyPANRef = 10,
            PANRefnumber = 11,
            NameofDeductee = 12,
            TDSIncomeTax = 13,
            TDSSurcharge = 14,
            TDSCess = 15,
            TotalIncomeTaxDeducted = 16,
            LastTotalIncomeTaxDeducted = 17,
            TotalTaxDeposited = 18,
            LastTotalTaxDeposited = 19,
            TotalValueofPurchase = 20,
            AmountofPayment = 21,
            DateonwhichAmountpaid = 22,
            DateonwhichtaxDeducted = 23,
            DateofDeposit = 24,
            Rate = 25,
            GrossingupIndicator = 26,
            BookEntryCashIndicator = 27,
            DateoffurnishingTaxDeductionCertificate = 28,
            Remarks1 = 29,
            PANFlag = 30,
            PANcounter = 31,
            RecordHash = 32

        }
        #endregion

        #region enum T_SalaryDetail
        public enum T_SalaryDetail
        {
            Linenumber = 0,
            RecordType = 1,
            Batchnumber = 2,
            SalaryDetailsRecordnumber = 3,
            Mode = 4,
            Filler7 = 5,
            EmployeePAN = 6,
            PANRefnumber = 7,
            Name = 8,
            Category = 9,
            FromDate = 10,
            ToDate = 11,
            Totalamountofsalary = 12,
            Filler8 = 13,
            CountofSection16DetailRecords = 14,
            GrossTotalofTotalDeductionundersection16 = 15,
            IncomechargeableundertheheadSalaries = 16,
            OtherIncome = 17,
            GrossTotalIncome = 18,
            LastGrossTotalIncome = 19,
            CountofChapterVIADetailRecord = 20,
            GrossTotalofAmountdeductible = 21,
            TotalTaxableIncome = 22,
            IncomeTaxonTotalIncome = 23,
            Surcharge = 24,
            EducationCess = 25,
            IncomeTaxReliefus89 = 26,
            NetIncomeTaxpayable = 27,
            TotalAmountofTaxDeducted = 28,
            Shortfallintaxdeduction = 29,
            PANFlag = 30,
            PANcounter = 31,
            Remarks3 = 32

        }
        #endregion

        #region enum T_S16Details
        public enum T_S16Details
        {
            Linenumber = 0,
            RecordType = 1,
            Batchnumber = 2,
            SalaryDetailRecordnumber = 3,
            Section16deductionDetail = 4,
            Section16deductionID = 5,
            TotalAmount = 6,
            RecordHash = 7
        }
        #endregion

        #region enum T_CVIADetails
        public enum T_CVIADetails
        {
            Linenumber = 0,
            RecordType = 1,
            Batchnumber = 2,
            SalaryDetailRecordnumber = 3,
            ChapterVIADetailsRecordnumber = 4,
            ChapterVIASectionID = 5,
            TotalAmount = 6,
            RecordHash = 7

        }
        #endregion

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
                txtFVUPath.Text = strFVUPath;

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
            prgBar.Value = 0;
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
            if (cmnService.J_UserMessage("Proceed to Import?", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                BtnExit.Select();
                return;
            }
            //-----------------------------------------------

            // GET BASIC_INFO_ID
            //-----------------------------------------------
            this.Cursor = Cursors.WaitCursor;
            //
            prgBar.Value = prgBar.Value + 5;
            this.Refresh();
            //

            prgBar.Value = prgBar.Value + 5;
            this.Refresh();
            //
            if (ImportTransactionsFromFVU(txtFVUPath.Text) == false) return;
            //
            prgBar.Value = prgBar.Value + 5;
            this.Refresh();
            //
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
                        strCheckCompatibilityMessage = "Other than Regular Return FVU is supported";
                        txtRdr.Close();
                        txtRdr.Dispose();
                        return false;
                    }
                    //
                    if (Path.GetExtension(txtFVUPath.Text).ToUpper() == ".TDS")
                        dblFVUVersion = cmnService.J_ReturnDoubleValue(T_FVU_Version.FVU_3_2);
                    else
                    {
                        // FVU Version
                        for (int a = 1; a < 7; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        //
                        do
                        {
                            for (int a = 1; a < 2; a++)
                            {
                                intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            }
                            //
                            strFVUVersion = ListLines[i].Substring(intCaratPosition + 1);
                            strFVUVersion = ListLines[i].Substring(intCaratPosition + 1, strFVUVersion.IndexOf("^"));
                        } while (strFVUVersion.Substring(0, 3) != "FVU");
                        //
                        dblFVUVersion = cmnService.J_ReturnDoubleValue(strFVUVersion.Substring(3).Trim());
                    }

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
                    // FORM NO.
                    // TAN
                    // FINANCIAL YEAR
                    for (int a = 1; a < 16; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strFinancialYear = ListLines[i].Substring(intCaratPosition + 1, 6);
                    // MAX FINANCIAL YEAR CHECK
                    FinancialYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FA_YEAR FROM MST_ASSESSMENT ORDER BY ASST_ID DESC"));
                    FinancialYear = cmnService.J_Left(FinancialYear, 4) + cmnService.J_Right(FinancialYear, 2);
                    if (cmnService.J_ReturnInt64Value(strFinancialYear) > cmnService.J_ReturnInt64Value(FinancialYear))
                    {
                        strCheckCompatibilityMessage = "FVU's F.Y. Year [" + strFinancialYear + "] is greater than Package's maximum F.Y. Year [" + FinancialYear + "]\nNot Supported.";
                        txtRdr.Close();
                        txtRdr.Dispose();
                        return false;
                    }
                    // MIN FINANCIAL YEAR CHECK
                    FinancialYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FA_YEAR FROM MST_ASSESSMENT ORDER BY ASST_ID"));
                    FinancialYear = cmnService.J_Left(FinancialYear, 4) + cmnService.J_Right(FinancialYear, 2);
                    if (cmnService.J_ReturnInt64Value(strFinancialYear) < cmnService.J_ReturnInt64Value(FinancialYear))
                    {
                        strCheckCompatibilityMessage = "FVU's F.Y. Year [" + strFinancialYear + "] is less than Package's minimum F.Y. Year [" + FinancialYear + "]\nNot Supported.";
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
        #endregion

        #region ImportTransactionsFromFVU

        private bool ImportTransactionsFromFVU(string FVUPath)
        {
            try
            {
                #region DECLARATION

                int NumberOfLines = 0;

                // FH
                string strFileCreationDate = "";
                string strRPUName = "";

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

                //dmlService.J_BeginTransaction();
                //
                TextReader txtRdrGetNoLines = new StreamReader(FVUPath);
                while (txtRdrGetNoLines.ReadLine() != null)
                {
                    NumberOfLines++;
                }
                txtRdrGetNoLines.Close();
                txtRdrGetNoLines.Dispose();

                //string[] ListLines = new string[NumberOfLines];
                //
                TextReader txtRdr = new StreamReader(FVUPath);
                string[] strTextReader;
                intCaratPosition = 0;
                int intCurrentLine = 0;
                //
                for (int i = 0; i < NumberOfLines; i++)
                {
                    strTextReader = txtRdr.ReadLine().Split('^');

                    intCurrentLine = Convert.ToInt32(strTextReader[0]);

                    #region FILE HEADER [FH]
                    if (strTextReader[1] == "FH")
                    {
                        strFileCreationDate = strTextReader[(int)T_FileHeader.FileCreationDate];
                        continue;
                    }
                    #endregion

                    #region BATCH HEADER [BH]

                    if (strTextReader[1] == "BH")
                    {
                        strFormNo = strTextReader[(int)T_BatchHeader.FormNumber];
                        strOriginalTokenNo = strTextReader[(int)T_BatchHeader.OriginalRRRnumber];
                        strPreviousTokenNo = strTextReader[(int)T_BatchHeader.PreviousRRRnumber];
                        strTAN = strTextReader[(int)T_BatchHeader.TAN];
                        strPAN = strTextReader[(int)T_BatchHeader.PAN];
                        strAssessmentYear = strTextReader[(int)T_BatchHeader.AssessmentYr];

                        strFinancialYear = strTextReader[(int)T_BatchHeader.FinancialYr];
                        strFinancialYear = cmnService.J_Left(strFinancialYear, 4) + "-" + cmnService.J_Right(strFinancialYear, 2);
                        long lngFinancialYear = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT ASST_ID FROM MST_ASSESSMENT WHERE FA_YEAR = '" + cmnService.J_ReplaceQuote(strFinancialYear) + "'");

                        strQTR = strTextReader[(int)T_BatchHeader.Period];
                        strDeductorName = strTextReader[(int)T_BatchHeader.NameofDeductor];
                        strDeductorBranch = strTextReader[(int)T_BatchHeader.BranchDivision];
                        strDeductorAddress1 = strTextReader[(int)T_BatchHeader.Address1];
                        strDeductorAddress2 = strTextReader[(int)T_BatchHeader.Address2];
                        strDeductorAddress3 = strTextReader[(int)T_BatchHeader.Address3];
                        strDeductorAddress4 = strTextReader[(int)T_BatchHeader.Address4];
                        strDeductorAddress5 = strTextReader[(int)T_BatchHeader.Address5];

                        strDeductorStateCode = strTextReader[(int)T_BatchHeader.State];
                        strDeductorStateCode = Convert.ToString(cmnService.J_ReturnInt64Value(strDeductorStateCode));
                        lngDeductorStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDeductorStateCode) + "'");

                        strDeductorPIN = strTextReader[(int)T_BatchHeader.PIN];
                        strDeductorEmail = strTextReader[(int)T_BatchHeader.EmailID];
                        strDeductorSTD = strTextReader[(int)T_BatchHeader.STD];
                        strDeductorTelePhone = strTextReader[(int)T_BatchHeader.Telephonenumber];

                        strDeductorChangeofAddress = strTextReader[(int)T_BatchHeader.ChangeofAddressofDeductor];
                        if (strDeductorChangeofAddress.ToUpper() == "Y")
                            intDeductorChangeofAddress = 1;
                        else
                            intDeductorChangeofAddress = 0;

                        strDeductorType = strTextReader[(int)T_BatchHeader.DeductorType];
                        lngDeductorCategoryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT CATEGORY_ID FROM MST_CATEGORY WHERE CATEGORY_CODE = '" + cmnService.J_ReplaceQuote(strDeductorType) + "'");

                        strRPName = strTextReader[(int)T_BatchHeader.NameofPersonresponsible];

                        strRPDesignation = strTextReader[(int)T_BatchHeader.Designation];

                        strRPAddress1 = strTextReader[(int)T_BatchHeader.ResponsiblePersonsAddress1];

                        strRPAddress2 = strTextReader[(int)T_BatchHeader.ResponsiblePersonsAddress2];

                        strRPAddress3 = strTextReader[(int)T_BatchHeader.ResponsiblePersonsAddress3];

                        strRPAddress4 = strTextReader[(int)T_BatchHeader.ResponsiblePersonsAddress4];

                        strRPAddress5 = strTextReader[(int)T_BatchHeader.ResponsiblePersonsAddress5];

                        strRPStateCode = strTextReader[(int)T_BatchHeader.ResponsiblePersonsState];
                        strRPStateCode = Convert.ToString(cmnService.J_ReturnInt64Value(strRPStateCode));
                        lngRPStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strRPStateCode) + "'");

                        strRPPIN = strTextReader[(int)T_BatchHeader.ResponsiblePersonsPIN];

                        strRPEmail = strTextReader[(int)T_BatchHeader.ResponsiblePersonsEmailID];

                        strRPSTD = strTextReader[(int)T_BatchHeader.ResponsiblePersonsSTDCODE];

                        strRPTelePhone = strTextReader[(int)T_BatchHeader.ResponsiblePersonsTelephonenumber];

                        strRPChangeofAddress = strTextReader[(int)T_BatchHeader.ChangeofAddressofResponsibleperson];
                        //
                        if (strRPChangeofAddress.ToUpper() == "Y")
                            intRPChangeofAddress = 1;
                        else
                            intRPChangeofAddress = 0;

                        strRPMobile = strTextReader[(int)T_BatchHeader.Mobileno];

                        strDStateCode = strTextReader[(int)T_BatchHeader.StateName];
                        //
                        strDStateCode = Convert.ToString(cmnService.J_ReturnInt64Value(strDStateCode));
                        lngDStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDStateCode) + "'");

                        strPAO = strTextReader[(int)T_BatchHeader.PAOCode];
                        strDDO = strTextReader[(int)T_BatchHeader.DDOCode];

                        strMinistryName = strTextReader[(int)T_BatchHeader.MinistryName];
                        //
                        lngMinistryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT MINISTRY_ID FROM MST_MINISTRY WHERE MINISTRY_CODE = '" + cmnService.J_ReplaceQuote(strMinistryName) + "'");

                        strOtherMinistryName = strTextReader[(int)T_BatchHeader.MinistryNameOther];

                        strPAORegNo = strTextReader[(int)T_BatchHeader.PAORegistrationnumber];

                        strDDORegNo = strTextReader[(int)T_BatchHeader.DDORegistrationnumber];

                        // 62 RECORD HASH
                        T_GenerateBatchId(dmlService.J_pCommand, lngFinancialYear, strQTR,
                                          strFormNo, System.DateTime.Now.ToString(), strRPUName,
                                          cmnService.J_Left(strFileCreationDate, 2) + "/" + cmnService.J_Mid(strFileCreationDate, 2, 2) + "/" + cmnService.J_Right(strFileCreationDate, 4));
                        //
                        lngBatchID = T_ReturnBatchId();
                        //
                        strSQL = "UPDATE COR_HDR_BATCH SET " +
                            "            ORIGINAL_RRR_NO ='" + cmnService.J_ReplaceQuote(strOriginalTokenNo) + "'," +
                            "            PREVIOUS_RRR_NO ='" + cmnService.J_ReplaceQuote(strPreviousTokenNo) + "' " +
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
                         "            P_ADDRESS_CHANGE) " +
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
                         "             " + intRPChangeofAddress + ")";
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
                         "            P_ADDRESS_CHANGE) " +
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
                         "            P_ADDRESS_CHANGE " +
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

                    if (strTextReader[1] == "CD")
                    {
                        strCDRecordNumber = strTextReader[(int)T_Challan_detail.ChallanDetailRecordnumber];

                        strNILChallanIndicator = strTextReader[(int)T_Challan_detail.NILChallanIndicator];
                        //
                        if (strNILChallanIndicator == "Y")
                            intNILChallanIndicator = 1;
                        else if (strNILChallanIndicator == "N")
                            intNILChallanIndicator = 0;

                        strChallanStatus = strTextReader[(int)T_Challan_detail.ChallanMatchingIndicator];

                        strChallanNumber = strTextReader[(int)T_Challan_detail.BankChallanNo];

                        strTransferVoucherNumber = strTextReader[(int)T_Challan_detail.TransferVoucher];

                        strBSRCode = strTextReader[(int)T_Challan_detail.BankBranchCode];

                        strDateofBankChallan = strTextReader[(int)T_Challan_detail.DateofBankChallanNo];
                        strDateofBankChallan = cmnService.J_Left(strDateofBankChallan, 2) + "/" + cmnService.J_Mid(strDateofBankChallan, 2, 2) + "/" + cmnService.J_Right(strDateofBankChallan, 4);

                        strSection = strTextReader[(int)T_Challan_detail.SectionCollectionCode];
                        //
                        lngSectionID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT SECTION_ID FROM MST_SECTION WHERE SECTION_NAME = '" + cmnService.J_ReplaceQuote(strSection) + "'");

                        strOLTASIncomeTax = strTextReader[(int)T_Challan_detail.OltasTDSTCSIncomeTax];

                        strOLTASSurcharge = strTextReader[(int)T_Challan_detail.OltasTDSTCSSurcharge];

                        strOLTASCess = strTextReader[(int)T_Challan_detail.OltasTDSTCSCess];

                        strOLTASInterestAmount = strTextReader[(int)T_Challan_detail.OltasTDSTCSInterestAmount];

                        strOLTASOthers = strTextReader[(int)T_Challan_detail.OltasTDSTCSOthersAmount];

                        strTotalDepositedAmount = strTextReader[(int)T_Challan_detail.TotalDepositAmountasperChallan];

                        strTotalTaxDepositedAmount = strTextReader[(int)T_Challan_detail.TotalTaxDepositAmountasperdeducteeannexure];

                        strIncomeTax = strTextReader[(int)T_Challan_detail.TDSTCSIncomeTax];

                        strSurcharge = strTextReader[(int)T_Challan_detail.TDSTCSSurcharge];

                        strCess = strTextReader[(int)T_Challan_detail.TDSTCSCess];

                        strSumTotalIncomeTax = strTextReader[(int)T_Challan_detail.SumofTotalIncomeTaxDeductedatSource];

                        strInterestAmount = strTextReader[(int)T_Challan_detail.TDSTCSInterestAmount];

                        strOthersAmount = strTextReader[(int)T_Challan_detail.TDSTCSOthersAmount];

                        strChequeDDNumber = strTextReader[(int)T_Challan_detail.ChequeDDnumber];
                        //
                        if (strChequeDDNumber == "0")
                            strChequeDDNumber = "";

                        strBookEntry = strTextReader[(int)T_Challan_detail.ByBookentryCash];
                        //

                        //Modifed by Shrey Kejriwal on 20/09/2011
                        if (strBookEntry.ToUpper() == "Y")
                            intBookEntry = 1;
                        else if (strBookEntry.ToUpper() == "")
                            intBookEntry = 2;
                        else
                            intBookEntry = 0;

                        // 38 REMARKS
                        strPendingAmount = strTextReader[(int)T_Challan_detail.PendingAmount];

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
                                 "            CHALLAN_STATUS) " +
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
                                 "           '" + cmnService.J_ReplaceQuote(strChallanStatus) + "')";
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
                                  "            CHALLAN_STATUS) " +
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
                                  "            CHALLAN_STATUS " +
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

                    if (strTextReader[1] == "DD")
                    {
                        strDDRecordNumber = strTextReader[(int)T_deductee_detail.DeducteeDetailRecordNo];

                        strDeducteeCode = strTextReader[(int)T_deductee_detail.DeducteeCode];

                        if (strFormNo != T_FormNo.F24Q)
                            strDeducteeCode = "0" + strDeducteeCode;

                        strDeducteePAN = strTextReader[(int)T_deductee_detail.EmployeePAN];

                        strDeducteeName = strTextReader[(int)T_deductee_detail.NameofDeductee];

                        strTDSIncomeTax = strTextReader[(int)T_deductee_detail.TDSIncomeTax];

                        strTDSSurcharge = strTextReader[(int)T_deductee_detail.TDSSurcharge];

                        strTDSCess = strTextReader[(int)T_deductee_detail.TDSCess];

                        strTotalIncomeTaxDeducted = strTextReader[(int)T_deductee_detail.TotalIncomeTaxDeducted];

                        strTotalTaxDeposited = strTextReader[(int)T_deductee_detail.TotalTaxDeposited];

                        strTotalValueOfPurchase = strTextReader[(int)T_deductee_detail.TotalValueofPurchase];

                        strAmountOfPayment = strTextReader[(int)T_deductee_detail.AmountofPayment];

                        strDateAmountPaid = strTextReader[(int)T_deductee_detail.DateonwhichAmountpaid];
                        strDateAmountPaid = cmnService.J_Left(strDateAmountPaid, 2) + "/" + cmnService.J_Mid(strDateAmountPaid, 2, 2) + "/" + cmnService.J_Right(strDateAmountPaid, 4);

                        strDateTaxDeducted = strTextReader[(int)T_deductee_detail.DateonwhichtaxDeducted];
                        
                        if (strDateTaxDeducted.Trim() == "")
                            strDateTaxDeducted = "NULL";
                        else
                            strDateTaxDeducted = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(cmnService.J_Left(strDateTaxDeducted, 2) + "/" + cmnService.J_Mid(strDateTaxDeducted, 2, 2) + "/" + cmnService.J_Right(strDateTaxDeducted, 4)) + cmnService.J_DateOperator();

                        strRate = strTextReader[(int)T_deductee_detail.Rate];

                        strGrossingUpIndicator = strTextReader[(int)T_deductee_detail.GrossingupIndicator];


                        strBookEntry = strTextReader[(int)T_deductee_detail.BookEntryCashIndicator];

                        if (strBookEntry.ToUpper() == "Y")
                            intBookEntry = 1;
                        else
                            intBookEntry = 0;

                        strRemarks1 = strTextReader[(int)T_deductee_detail.Remarks1];

                        strPanFlag = strTextReader[(int)T_deductee_detail.PANFlag];

                        if (strPanFlag.ToUpper() == "N")
                            intPanFlag = 1; //invalid pan
                        else
                            intPanFlag = 0;

                        strPanCounter = strTextReader[(int)T_deductee_detail.PANcounter];

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
                        if (strTextReader[1] == "SD")
                        {
                            strSDRecordNumber = strTextReader[(int)T_SalaryDetail.SalaryDetailsRecordnumber];

                            strEmployeePAN = strTextReader[(int)T_SalaryDetail.EmployeePAN];

                            strEmployeeName = strTextReader[(int)T_SalaryDetail.Name];

                            strEmployeeCategory = strTextReader[(int)T_SalaryDetail.Category];

                            strPeriodFromDate = strTextReader[(int)T_SalaryDetail.FromDate];
                            strPeriodFromDate = cmnService.J_Left(strPeriodFromDate, 2) + "/" + cmnService.J_Mid(strPeriodFromDate, 2, 2) + "/" + cmnService.J_Right(strPeriodFromDate, 4);

                            strPeriodToDate = strTextReader[(int)T_SalaryDetail.ToDate];
                            strPeriodToDate = cmnService.J_Left(strPeriodToDate, 2) + "/" + cmnService.J_Mid(strPeriodToDate, 2, 2) + "/" + cmnService.J_Right(strPeriodToDate, 4);

                            strTotalAmountSalary = strTextReader[(int)T_SalaryDetail.Totalamountofsalary];

                            strCountSec16Records = strTextReader[(int)T_SalaryDetail.CountofSection16DetailRecords];

                            strGrossTotalSec16 = strTextReader[(int)T_SalaryDetail.GrossTotalofTotalDeductionundersection16];

                            strIncomeChargeableUnderHeadSalaries = strTextReader[(int)T_SalaryDetail.IncomechargeableundertheheadSalaries];

                            strIncomeOtherThanSalaries = strTextReader[(int)T_SalaryDetail.OtherIncome];

                            strGrossTotalIncome = strTextReader[(int)T_SalaryDetail.GrossTotalIncome];

                            strCountChVIARecords = strTextReader[(int)T_SalaryDetail.CountofChapterVIADetailRecord];

                            strGrossTotalChVIA = strTextReader[(int)T_SalaryDetail.GrossTotalofAmountdeductible];

                            strTotalTaxableIncome = strTextReader[(int)T_SalaryDetail.TotalTaxableIncome];

                            strIncomeTaxOnTotalIncome = strTextReader[(int)T_SalaryDetail.IncomeTaxonTotalIncome];

                            strSurchargeSD = strTextReader[(int)T_SalaryDetail.Surcharge];

                            strEducationCess = strTextReader[(int)T_SalaryDetail.EducationCess];

                            strIncomeTaxRelief = strTextReader[(int)T_SalaryDetail.IncomeTaxReliefus89];

                            strNetIncomeTaxPayable = strTextReader[(int)T_SalaryDetail.NetIncomeTaxpayable];

                            strTotalAmountOfTaxDeducted = strTextReader[(int)T_SalaryDetail.TotalAmountofTaxDeducted];

                            strShortfallExcess = strTextReader[(int)T_SalaryDetail.Shortfallintaxdeduction];

                            strPanFlag = strTextReader[(int)T_SalaryDetail.PANFlag];

                            if (strPanFlag.ToUpper() == "N")
                                intPanFlag = 1; //invalid pan
                            else
                                intPanFlag = 0;

                            strPanCounter = strTextReader[(int)T_SalaryDetail.PANcounter];

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
                        if (strTextReader[1] == "S16")
                        {
                            strSDS16RecordNumber = strTextReader[(int)T_S16Details.SalaryDetailRecordnumber];

                            strS16SectionID = strTextReader[(int)T_S16Details.Section16deductionID];

                            strS16TotalDeduction = strTextReader[(int)T_S16Details.TotalAmount];

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
                        if (strTextReader[1] == "C6A")
                        {
                            strSDC6ARecordNumber = strTextReader[(int)T_CVIADetails.SalaryDetailRecordnumber];

                            strC6ASectionID = strTextReader[(int)T_CVIADetails.ChapterVIASectionID];

                            strC6ATotalAmount = strTextReader[(int)T_CVIADetails.TotalAmount];

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

                //dmlService.J_Commit();
                return true;
            }
            catch (Exception err_handler)
            {
                //dmlService.J_Rollback();
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

