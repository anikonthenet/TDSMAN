
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
    public partial class TrnFVUImport : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnFVUImport()
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
        long lngBasicInfoID = 0;
        int intLoop = 0;
        double dblFVUVersion = 0;
        //-----------------------------------------------------------------------

        #endregion

        #region User Defined Events

        #region TrnFVUImport_Load

        private void TrnFVUImport_Load(object sender, EventArgs e)
        {
            //-----------
            lblTitle.Text = "FVU Import";
            //-- FINANCIAL YEAR
            //-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //-- QUARTER
            //-----------
            string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter, 1);
            //-- FORM NO
            string[] strQuarterWiseDeducteeReportForm ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            //-----------
            //-- COMPANY
            //-----------
            //Modified by Shrey Kejriwal on 17/08/2011
            //strSQL = " SELECT COMPANY_ID," +
            //    "             COMPANY_NAME " +
            //    "      FROM   MST_COMPANY " +
            //    "      ORDER BY COMPANY_NAME";

            //--
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME & ' [' & TAN_NO & ']'" +
                "      FROM   MST_COMPANY " +
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
        }

        #endregion

        #region btnSelectFVUPath_Click
        private void btnSelectFVUPath_Click(object sender, EventArgs e)
        {
            strFVUPath = cmnService.J_OpenFileDialog("FVU File | *.fvu", "FVU File | *.fvu", "Choose the FVU File to import");
            if (strFVUPath != "")
                txtFVUPath.Text = strFVUPath; 
            
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
            // AUTO UPDATE
            //ToolStripDropDown popup = new ToolStripDropDown();
            //popup.Margin = Padding.Empty;
            //popup.Padding = Padding.Empty;
            //ToolStripControlHost host = new ToolStripControlHost(BtnSave);
            //host.Margin = Padding.Empty;
            //host.Padding = Padding.Empty;
            //popup.Items.Add(host);
            //popup.Show(280,57);//             
            //prgBar.Value = 0;
            //
            if (ValidateFields() == false) return;
            //
            if (CheckFVUCompatibility(cmbFinancialYear.Text, cmbQuarter.Text, cmbFormNo.Text, txtTAN.Text, txtFVUPath.Text) == false)
            {
                cmnService.J_UserMessage(strCheckCompatibilityMessage);
                btnSelectFVUPath.Select();
                return;
            }
            //
            if (chkTransactionImport.Checked == true)
            {
                //
                lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    cmbQuarter.Text,
                    Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                    cmbFormNo.Text);
                //
                if (lngBasicInfoID == 0)
                {
                    if (cmnService.J_UserMessage("Proceed to Import?", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        BtnExit.Select();
                        return;
                    }
                }
                else
                {
                    if (cmnService.J_UserMessage("All existing transaction of this return will be Deleted - Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        BtnExit.Select();
                        return;
                    }
                }
                //-----------------------------------------------
            }
            else
            {
                if (cmnService.J_UserMessage("Only Master data will be Imported - Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    BtnExit.Select();
                    return;
                }                
            }
            // GET BASIC_INFO_ID
            //-----------------------------------------------
            this.Cursor = Cursors.WaitCursor;
            // IMPORT MASTER
            if (ImportMastersFromFVU(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), txtFVUPath.Text) == false) return;
            //
            prgBar.Value = prgBar.Value + 5;
            this.Refresh();
            //
            // IMPORT TRANSACTIONS
            if (chkTransactionImport.Checked == true)
            {
                //
                lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    cmbQuarter.Text,
                    Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                    cmbFormNo.Text);
                //
                if (lngBasicInfoID == 0)
                {
                    if (TdsMan.T_GenerateBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                    cmbQuarter.Text,
                                                    Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                    cmbFormNo.Text) == true)
                        lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                            cmbQuarter.Text,
                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                            cmbFormNo.Text);
                }
                //            
                if (TdsMan.T_DeleteSalaryDetails(lngBasicInfoID) == false) return;
                //
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //
                if (TdsMan.T_DeleteDeducteeDetails(lngBasicInfoID) == false) return;
                //
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //
                if (TdsMan.T_DeleteChallanDetails(lngBasicInfoID) == false) return;
                //
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //
                if (TdsMan.T_DeleteReceiptNo(lngBasicInfoID) == false) return;
                //
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();                
                //
                if (ImportTransactionsFromFVU(lngBasicInfoID, txtFVUPath.Text, cmbFormNo.Text) == false) return;
                //
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //
            }
            //
            for (int i = prgBar.Minimum; i <= prgBar.Maximum; i++)
            {
                prgBar.PerformStep();
            }
            this.Cursor = Cursors.Default;
            //
            //-----------
            //-- COMPANY
            //-----------

            //COMMENTED BY SHREY KEJRIWAL ON 23/09/2011
            //strSQL = " SELECT COMPANY_ID," +
            //    "             COMPANY_NAME " +
            //    "      FROM   MST_COMPANY " +
            //    "      ORDER BY COMPANY_NAME";

            //Added By Shrey Kejriwal on 23/09/2011
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME & ' [' & TAN_NO & ']'" +
                "      FROM   MST_COMPANY " +
                "      ORDER BY COMPANY_NAME";

            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //
            txtFVUPath.Text = "";
            //chkTransactionImport.Checked = false;
            //---------------
            cmnService.J_UserMessage("Import Completed");
            //
            prgBar.Value = 0;
            //            
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex <= 0)
            {
                txtTAN.Text = "";
                txtCITAddress.Text = "";
                txtCITCity.Text = "";
                txtCITPIN.Text = "";
                return;
            }
            //--
            txtTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_COMPANY WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))));
            txtCITAddress.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT CIT_TDS_ADDRESS FROM MST_COMPANY WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))));
            txtCITCity.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT CIT_TDS_CITY FROM MST_COMPANY WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))));
            txtCITPIN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT CIT_TDS_PINCODE FROM MST_COMPANY WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))));
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
                    if (cmbFinancialYear.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Financial Year - Cannot be Blank");
                        cmbFinancialYear.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- QTR
                    //-----------------------------------------------------------------------
                    if (cmbQuarter.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Quarter - Cannot be Blank");
                        cmbQuarter.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- FORM NO
                    //-----------------------------------------------------------------------
                    if (cmbFormNo.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Form No. - Cannot be Blank");
                        cmbFormNo.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- COMPANY NAME
                    //-----------------------------------------------------------------------
                    if (cmbCompany.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Company - Cannot be Blank");
                        cmbCompany.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- FVU FILE SELECTED
                    //-----------------------------------------------------------------------
                    if (txtFVUPath.Text.Trim()== "")
                    {
                        cmnService.J_UserMessage("FVU not selected");
                        btnSelectFVUPath.Select();
                        return false;
                    }
                    // FILE SHOULD BE FVU
                    if (Path.GetExtension(txtFVUPath.Text).ToUpper() != ".FVU")
                    {
                        cmnService.J_UserMessage("Selected file should be a FVU file");
                        btnSelectFVUPath.Select();
                        return false;
                    }
                    // FILE EXIST
                    if (cmnService.J_IsFileExist(txtFVUPath.Text) == false)
                    {
                        cmnService.J_UserMessage("Selected FVU file not found");
                        btnSelectFVUPath.Select();
                        return false;
                    }
                    // FILE OPEN
                    if (cmnService.J_IsProcessOpen(txtFVUPath.Text) == true)
                    {
                        cmnService.J_UserMessage("Selected FVU open");
                        btnSelectFVUPath.Select();
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
        private bool CheckFVUCompatibility(string FinancialYear, string Qtr, string FormNo, string TAN, string FVUPath)
        {
            string strUploadType = "";
            string strFVUVersion = "";
            string strFormNo = "";
            string strTAN = "";
            string strFinancialYear = "";
            string strQTR = "";

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

                    dblFVUVersion = cmnService.J_ReturnDoubleValue(strFVUVersion.Substring(3).Trim());

                    // COMMENTED ON 2011-05-26 BY ANIK
                    //// FVU Version [2.126]
                    //for (int a = 1; a < 8; a++)
                    //{
                    //    intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    //    //
                    //}
                    //strFVUVersion = ListLines[i].Substring(intCaratPosition + 1);
                    //strFVUVersion = ListLines[i].Substring(intCaratPosition + 1, strFVUVersion.IndexOf("^"));
                    ////                    
                    //dblFVUVersion = cmnService.J_ReturnDoubleValue(strFVUVersion.Substring(3).Trim());
                    //if (dblFVUVersion == 2.126)
                    //{
                    //    strCheckCompatibilityMessage = "FVU Version before 2.128 is not supported";
                    //    txtRdr.Close();
                    //    txtRdr.Dispose();
                    //    return false;
                    //}
                    //// FVU Version [2.128, 2.129, 3.0]
                    //for (int a = 1; a < 1; a++)
                    //{
                    //    intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    //    //
                    //}
                    //strFVUVersion = ListLines[i].Substring(intCaratPosition + 1);
                    //strFVUVersion = ListLines[i].Substring(intCaratPosition + 1, strFVUVersion.IndexOf("^"));
                    ////                    
                    //dblFVUVersion = cmnService.J_ReturnDoubleValue(strFVUVersion.Substring(3).Trim());
                    //if (dblFVUVersion < 2.128)
                    //{
                    //    strCheckCompatibilityMessage = "FVU Version before 2.128 is not supported";
                    //    txtRdr.Close();
                    //    txtRdr.Dispose();
                    //    return false;
                    //}
                    //
                    continue;
                }

                if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "BH")
                {
                    // FORM NO.
                    for (int a = 1; a < 4; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }

                    strFormNo = ListLines[i].Substring(intCaratPosition + 1);
                    strFormNo = ListLines[i].Substring(intCaratPosition + 1, strFormNo.IndexOf("^"));
                    // FORM NO. CHECK
                    if (FormNo != strFormNo)
                    {
                        strCheckCompatibilityMessage = "Form No. mismatched";
                        txtRdr.Close();
                        txtRdr.Dispose();
                        return false;
                    }

                    // TAN
                    for (int a = 1; a < 9; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strTAN = ListLines[i].Substring(intCaratPosition + 1, 10);
                    // TAN CHECK
                    if (TAN != strTAN)
                    {
                        strCheckCompatibilityMessage = "TAN mismatched";
                        txtRdr.Close();
                        txtRdr.Dispose();
                        return false;
                    }

                    // FINANCIAL YEAR
                    for (int a = 1; a < 5; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strFinancialYear = ListLines[i].Substring(intCaratPosition + 1, 6);
                    // FINANCIAL YEAR CHECK
                    FinancialYear = cmnService.J_Left(FinancialYear, 4) + cmnService.J_Right(FinancialYear, 2);
                    if (FinancialYear != strFinancialYear)
                    {
                        strCheckCompatibilityMessage = "Financial Year mismatched";
                        txtRdr.Close();
                        txtRdr.Dispose();
                        return false;
                    }

                    // QTR
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strQTR = ListLines[i].Substring(intCaratPosition + 1, 2);
                    // QTR CHECK
                    if (Qtr != strQTR)
                    {
                        strCheckCompatibilityMessage = "Qtr mismatched";
                        txtRdr.Close();
                        txtRdr.Dispose();
                        return false;
                    }
                    //
                    continue;
                }
            }
            txtRdr.Close();
            txtRdr.Dispose();
            return true;
        }
        #endregion

        #region ImportMastersFromFVU

        private bool ImportMastersFromFVU(long CompanyId, string FVUPath)
        {
            #region DECLARATION

            int NumberOfLines = 0;

            string strFVUVersion = "";
            double dblFVUVersion = 0;

            string strFormNo = "";
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

            string strDDRecordNumber = "";
            string strDeducteeCode = "";
            string strDeducteePAN = "";
            string strDeducteeName = "";

            string strSDRecordNumber = "";
            string strEmployeePAN = "";
            string strEmployeeName = "";
            string strEmployeeCategory = "";
                        
            #endregion
            
            //
            TextReader txtRdrGetNoLines = new StreamReader(FVUPath);
            while (txtRdrGetNoLines.ReadLine() != null)
            {
                NumberOfLines++;
            }
            txtRdrGetNoLines.Close();
            txtRdrGetNoLines.Dispose();

            string[] ListLines = new string[NumberOfLines];

            //cmnService.J_UserMessage(NumberOfLines.ToString());
            //
            //
            TextReader txtRdr = new StreamReader(FVUPath);
            intCaratPosition = 0;
            //
            for (int i = 0; i < NumberOfLines; i++)
            {
                ListLines[i] = txtRdr.ReadLine();

                intCaratPosition = ListLines[i].IndexOf("^");

                #region FILE HEADER [FH]

                if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "FH")
                {
                    // FVU Version
                    for (int a = 1; a < 11; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        //
                    }
                    strFVUVersion = ListLines[i].Substring(intCaratPosition + 1);
                    strFVUVersion = ListLines[i].Substring(intCaratPosition + 1, strFVUVersion.IndexOf("^"));
                    //
                    //dblFVUVersion = cmnService.J_ReturnDoubleValue(strFVUVersion.Substring(3));
                    //
                    continue;
                }
                #endregion

                #region BATCH HEADER [BH]

                //if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "BH")
                //{
                //    // 1 LINE NUMBER
                //    // 2 RECORD TYPE
                //    // 3 BATCH NUMBER
                //    // 4 COUNT OF CHALLAN/TRANSFER VOUCHER RECORDS
                //    // 5 FORM NO.
                //    for (int a = 1; a < 4; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }

                //    strFormNo = ListLines[i].Substring(intCaratPosition + 1);
                //    strFormNo = ListLines[i].Substring(intCaratPosition + 1, strFormNo.IndexOf("^"));

                //    // 6 TRANSACTION TYPE
                //    // 7 BATCH UPDATION INDICATOR
                //    // 8 ORIGINAL TOKEN NUMBER
                //    // 9 PREVIOUS TOKEN NUMBER
                //    // 10 TOKEN NUMBER OF THE STATEMENT SUBMITTED
                //    // 11 TOKEN NUMBER DATE
                //    // 12 LAST TAN
                //    // 13 TAN
                //    for (int a = 1; a < 9; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strTAN = ListLines[i].Substring(intCaratPosition + 1, 10);

                //    // 14 RECEIPT NO.
                //    // 15 PAN
                //    for (int a = 1; a < 3; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strPAN = ListLines[i].Substring(intCaratPosition + 1, 10);

                //    // 16 ASSESSMENT YEAR
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strAssessmentYear = ListLines[i].Substring(intCaratPosition + 1, 6);

                //    // 17 FINANCIAL YEAR
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strFinancialYear = ListLines[i].Substring(intCaratPosition + 1, 6);

                //    // 18 PERIOD/QTR
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strQTR = ListLines[i].Substring(intCaratPosition + 1, 2);

                //    // 19 DEDUCTOR NAME
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorName = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorName = ListLines[i].Substring(intCaratPosition + 1, strDeductorName.IndexOf("^"));

                //    // 20 DEDUCTOR BRANCH
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorBranch = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorBranch = ListLines[i].Substring(intCaratPosition + 1, strDeductorBranch.IndexOf("^"));

                //    // 21 DEDUCTOR ADDRESS1
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorAddress1 = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorAddress1 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress1.IndexOf("^"));

                //    // 22 DEDUCTOR ADDRESS2
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorAddress2 = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorAddress2 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress2.IndexOf("^"));

                //    // 23 DEDUCTOR ADDRESS3
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorAddress3 = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorAddress3 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress3.IndexOf("^"));

                //    // 24 DEDUCTOR ADDRESS4
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorAddress4 = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorAddress4 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress4.IndexOf("^"));

                //    // 25 DEDUCTOR ADDRESS5
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorAddress5 = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorAddress5 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress5.IndexOf("^"));

                //    // 26 DEDUCTOR STATE
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorStateCode = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorStateCode = ListLines[i].Substring(intCaratPosition + 1, strDeductorStateCode.IndexOf("^"));
                //    //
                //    lngDeductorStateID = dmlService.J_ReturnId(dmlService.J_pCommand,"SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDeductorStateCode) + "'");

                //    // 27 DEDUCTOR PIN
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorPIN = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorPIN = ListLines[i].Substring(intCaratPosition + 1, strDeductorPIN.IndexOf("^"));

                //    // 28 DEDUCTOR EMAIL
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorEmail = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorEmail = ListLines[i].Substring(intCaratPosition + 1, strDeductorEmail.IndexOf("^"));

                //    // 29 DEDUCTOR STD
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorSTD = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorSTD = ListLines[i].Substring(intCaratPosition + 1, strDeductorSTD.IndexOf("^"));

                //    // 30 DEDUCTOR TELEPHONE
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorTelePhone = ListLines[i].Substring(intCaratPosition + 1, strDeductorTelePhone.IndexOf("^"));

                //    // 31 DEDUCTOR CHANGE OF ADDRESS
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorChangeofAddress = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorChangeofAddress = ListLines[i].Substring(intCaratPosition + 1, strDeductorChangeofAddress.IndexOf("^"));

                //    // 32 DEDUCTOR TYPE
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDeductorType = ListLines[i].Substring(intCaratPosition + 1);
                //    strDeductorType = ListLines[i].Substring(intCaratPosition + 1, strDeductorType.IndexOf("^"));
                //    //
                //    lngDeductorCategoryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT CATEGORY_ID FROM MST_CATEGORY WHERE CATEGORY_CODE = '" + cmnService.J_ReplaceQuote(strDeductorType) + "'");

                //    // 33 RP NAME
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPName = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPName = ListLines[i].Substring(intCaratPosition + 1, strRPName.IndexOf("^"));

                //    // 34 RP DESIGNATION
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPDesignation = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPDesignation = ListLines[i].Substring(intCaratPosition + 1, strRPDesignation.IndexOf("^"));

                //    // 35 RP ADDRESS1
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPAddress1 = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPAddress1 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress1.IndexOf("^"));

                //    // 36 RP ADDRESS2
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPAddress2 = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPAddress2 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress2.IndexOf("^"));

                //    // 37 RP ADDRESS3
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPAddress3 = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPAddress3 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress3.IndexOf("^"));

                //    // 38 RP ADDRESS4
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPAddress4 = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPAddress4 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress4.IndexOf("^"));

                //    // 39 RP ADDRESS5
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPAddress5 = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPAddress5 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress5.IndexOf("^"));

                //    // 40 RP STATE
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPStateCode = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPStateCode = ListLines[i].Substring(intCaratPosition + 1, strRPStateCode.IndexOf("^"));
                //    //
                //    lngRPStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strRPStateCode) + "'");

                //    // 41 RP PIN
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPPIN = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPPIN = ListLines[i].Substring(intCaratPosition + 1, strRPPIN.IndexOf("^"));

                //    // 42 RP EMAIL
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPEmail = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPEmail = ListLines[i].Substring(intCaratPosition + 1, strRPEmail.IndexOf("^"));

                //    // 43 RP MOBILE
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPMobile = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPMobile = ListLines[i].Substring(intCaratPosition + 1, strRPMobile.IndexOf("^"));

                //    if (dblFVUVersion < 3.0)
                //        strRPMobile = "";

                //    // 44 RP STD
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPSTD = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPSTD = ListLines[i].Substring(intCaratPosition + 1, strRPSTD.IndexOf("^"));

                //    // 45 RP TELEPHONE
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPTelePhone = ListLines[i].Substring(intCaratPosition + 1, strRPTelePhone.IndexOf("^"));

                //    // 46 RP CHANGE OF ADDRESS
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strRPChangeofAddress = ListLines[i].Substring(intCaratPosition + 1);
                //    strRPChangeofAddress = ListLines[i].Substring(intCaratPosition + 1, strRPChangeofAddress.IndexOf("^"));

                //    // 47 BATCH TOTAL
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strBatchTotal = ListLines[i].Substring(intCaratPosition + 1);
                //    strBatchTotal = ListLines[i].Substring(intCaratPosition + 1, strBatchTotal.IndexOf("^"));

                //    // FOR FORM NO. 24Q
                //    if (strFormNo == T_FormNo.F24Q)
                //    {
                //        // 48 UNMATCHED CHALLAN COUNT
                //        // 49 COUNT SALARY DETAIL RECORDS
                //        for (int a = 1; a < 3; a++)
                //        {
                //            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //        }
                //        strCountSalaryDetailRecords = ListLines[i].Substring(intCaratPosition + 1);
                //        strCountSalaryDetailRecords = ListLines[i].Substring(intCaratPosition + 1, strCountSalaryDetailRecords.IndexOf("^"));
                       
                //        // 50 COUNT SALARY DETAIL RECORDS
                //        for (int a = 1; a < 2; a++)
                //        {
                //            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //        }
                //        strBatchTotalSalary = ListLines[i].Substring(intCaratPosition + 1);
                //        strBatchTotalSalary = ListLines[i].Substring(intCaratPosition + 1, strBatchTotalSalary.IndexOf("^"));
                //        //
                //        intLoop = 2;
                //    }
                //    else
                //        intLoop = 5;
                //    //
                //    // 51 AO APPROVAL
                //    for (int a = 1; a < intLoop; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strAOApproval = ListLines[i].Substring(intCaratPosition + 1);
                //    strAOApproval = ListLines[i].Substring(intCaratPosition + 1, strAOApproval.IndexOf("^"));

                //    // 52 AO APPROVAL NUMBER
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strAOApprovalNumber = ListLines[i].Substring(intCaratPosition + 1);
                //    strAOApprovalNumber = ListLines[i].Substring(intCaratPosition + 1, strAOApprovalNumber.IndexOf("^"));

                //    // 53 LAST DEDUCTOR TYPE
                //    // 54 DEDUCTOR STATE NAME
                //    for (int a = 1; a < 3; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDStateCode = ListLines[i].Substring(intCaratPosition + 1);
                //    strDStateCode = ListLines[i].Substring(intCaratPosition + 1, strDStateCode.IndexOf("^"));
                //    //
                //    lngDStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDStateCode) + "'");

                //    // 55 PAO CODE
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strPAO = ListLines[i].Substring(intCaratPosition + 1);
                //    strPAO = ListLines[i].Substring(intCaratPosition + 1, strPAO.IndexOf("^"));

                //    // 56 DDO CODE
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDDO = ListLines[i].Substring(intCaratPosition + 1);
                //    strDDO = ListLines[i].Substring(intCaratPosition + 1, strDDO.IndexOf("^"));

                //    // 57 MINISTRY
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strMinistryName = ListLines[i].Substring(intCaratPosition + 1);
                //    strMinistryName = ListLines[i].Substring(intCaratPosition + 1, strMinistryName.IndexOf("^"));
                //    //
                //    lngMinistryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT MINISTRY_ID FROM MST_MINISTRY WHERE MINISTRY_CODE = '" + cmnService.J_ReplaceQuote(strMinistryName) + "'");

                //    // 58 OTHER MINISTRY
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strOtherMinistryName = ListLines[i].Substring(intCaratPosition + 1);
                //    strOtherMinistryName = ListLines[i].Substring(intCaratPosition + 1, strOtherMinistryName.IndexOf("^"));

                //    // 59 FILLER 2
                //    // 60 PAO REGISTRATION NUMBER
                //    for (int a = 1; a < 3; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strPAORegNo = ListLines[i].Substring(intCaratPosition + 1);
                //    strPAORegNo = ListLines[i].Substring(intCaratPosition + 1, strPAORegNo.IndexOf("^"));

                //    // 61 DDO REGISTRATION NUMBER
                //    for (int a = 1; a < 2; a++)
                //    {
                //        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                //    }
                //    strDDORegNo = ListLines[i].Substring(intCaratPosition + 1);
                //    strDDORegNo = ListLines[i].Substring(intCaratPosition + 1, strDDORegNo.IndexOf("^"));

                //    // 62 RECORD HASH

                //    // UPDATE COMPANY

                //    strSQL = "UPDATE MST_COMPANY " +
                //             "SET    PAN_NO          = '" + cmnService.J_ReplaceQuote(strPAN.ToUpper()) + "'," +
                //             "       COMPANY_NAME    = '" + cmnService.J_ReplaceQuote(strDeductorName.ToUpper()) + "'," +
                //             "       BRANCH_DIV      = '" + cmnService.J_ReplaceQuote(strDeductorBranch) + "'," +
                //             "       D_CATEGORY_ID   =  " + lngDeductorCategoryId + "," +
                //             "       MINISTRY_ID     =  " + lngMinistryId + "," +
                //             "       MINISTRY_OTHER  = '" + cmnService.J_ReplaceQuote(strOtherMinistryName.ToUpper()) + "'," +
                //             "       ADDRESS1        = '" + cmnService.J_ReplaceQuote(strDeductorAddress1.ToUpper()) + "'," +
                //             "       ADDRESS2        = '" + cmnService.J_ReplaceQuote(strDeductorAddress2.ToUpper()) + "'," +
                //             "       ADDRESS3        = '" + cmnService.J_ReplaceQuote(strDeductorAddress3.ToUpper()) + "'," +
                //             "       ADDRESS4        = '" + cmnService.J_ReplaceQuote(strDeductorAddress4.ToUpper()) + "'," +
                //             "       ADDRESS5        = '" + cmnService.J_ReplaceQuote(strDeductorAddress5.ToUpper()) + "'," +
                //             "       STATE_ID        =  " + lngDeductorStateID + "," +
                //             "       PIN_CODE        = '" + cmnService.J_ReplaceQuote(strDeductorPIN) + "'," +
                //             "       STD             = '" + cmnService.J_ReplaceQuote(strDeductorSTD) + "'," +
                //             "       PHONE           = '" + cmnService.J_ReplaceQuote(strDeductorTelePhone) + "'," +
                //             "       EMAIL           = '" + cmnService.J_ReplaceQuote(strDeductorEmail) + "'," +
                //             "       PERSON_NAME     = '" + cmnService.J_ReplaceQuote(strRPName.ToUpper()) + "'," +
                //             "       DESIGNATION     = '" + cmnService.J_ReplaceQuote(strRPDesignation.ToUpper()) + "'," +
                //             "       P_ADDRESS1      = '" + cmnService.J_ReplaceQuote(strRPAddress1.ToUpper()) + "'," +
                //             "       P_ADDRESS2      = '" + cmnService.J_ReplaceQuote(strRPAddress2.ToUpper()) + "'," +
                //             "       P_ADDRESS3      = '" + cmnService.J_ReplaceQuote(strRPAddress3.ToUpper()) + "'," +
                //             "       P_ADDRESS4      = '" + cmnService.J_ReplaceQuote(strRPAddress4.ToUpper()) + "'," +
                //             "       P_ADDRESS5      = '" + cmnService.J_ReplaceQuote(strRPAddress5.ToUpper()) + "'," +
                //             "       P_STATE_ID      =  " + lngRPStateID + "," +
                //             "       P_PIN_CODE      = '" + cmnService.J_ReplaceQuote(strRPPIN) + "'," +
                //             "       P_PHONE         = '" + cmnService.J_ReplaceQuote(strRPTelePhone) + "'," +
                //             "       P_STD           = '" + cmnService.J_ReplaceQuote(strRPSTD) + "'," +
                //             "       P_EMAIL         = '" + cmnService.J_ReplaceQuote(strRPEmail) + "'," +
                //             "       P_MOBILE        = '" + cmnService.J_ReplaceQuote(strRPMobile) + "'," +
                //             "       PAO_CODE        = '" + cmnService.J_ReplaceQuote(strPAO.ToUpper()) + "'," +
                //             "       PAO_REG_NO      = '" + cmnService.J_ReplaceQuote(strPAORegNo.ToUpper()) + "'," +
                //             "       DDO_CODE        = '" + cmnService.J_ReplaceQuote(strDDO.ToUpper()) + "'," +
                //             "       DDO_REG_NO      = '" + cmnService.J_ReplaceQuote(strDDORegNo.ToUpper()) + "'," +
                //             "       D_STATE_ID      =  " + lngDStateID + " " +
                //             "WHERE  COMPANY_ID      = " + CompanyId;
                //    //
                //    if (dmlService.J_ExecSql(strSQL) == false)
                //    {
                //        return false;
                //    }
                //    //-- UPDATE LAST WORKED
                //    TdsMan.T_UpdateLastWorked(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), cmbQuarter.Text, cmbFormNo.Text, Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
                //    //---------------------        
                //    //
                //    continue;
                //}
                #endregion

                #region DEDUCTEE DETAIL [DD]

                if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "DD")
                {
                    //intCaratPosition = ListLines[i].IndexOf("^");

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
                    strDDRecordNumber = ListLines[i].Substring(intCaratPosition + 1, strDDRecordNumber.IndexOf("^"));

                    // 6 MODE
                    // 7 EMPLOYEE SERIAL NO
                    // 8 DEDUCTEE CODE
                    for (int a = 1; a < 4; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeducteeCode = ListLines[i].Substring(intCaratPosition + 1);
                    strDeducteeCode = ListLines[i].Substring(intCaratPosition + 1, strDeducteeCode.IndexOf("^"));

                    // 9 LAST EMPLOYEE PAN
                    // 10 DEDUCTEE PAN
                    for (int a = 1; a < 3; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeducteePAN = ListLines[i].Substring(intCaratPosition + 1);
                    strDeducteePAN = ListLines[i].Substring(intCaratPosition + 1, strDeducteePAN.IndexOf("^"));

                    // 11 LAST PAN REF NO
                    // 12 PAN REF NO
                    // 13 NAME OF DEDUCTEE
                    for (int a = 1; a < 4; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeducteeName = ListLines[i].Substring(intCaratPosition + 1);
                    strDeducteeName = ListLines[i].Substring(intCaratPosition + 1, strDeducteeName.IndexOf("^"));

                    // INSERT MASTER RECORD TO DEDUCTEE / EMPLOYEE MASTER
                    if (cmbFormNo.Text == T_FormNo.F24Q)
                    {
                        if (strDeducteePAN != "PANNOTAVBL")
                        {
                            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "MST_EMPLOYEE", " EMPLOYEE_PAN = '" + strDeducteePAN + "' " +
                                                                                " AND COMPANY_ID   = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))) == false)
                            {
                                strSQL = "INSERT INTO MST_EMPLOYEE(EMPLOYEE_NAME," +
                                         "                         EMPLOYEE_PAN," +
                                         "                         COMPANY_ID," +
                                         "                         GROUP_ID) " +
                                         "VALUES                  ('" + cmnService.J_ReplaceQuote(strDeducteeName.Trim().ToUpper()) + "'," +
                                         "                         '" + cmnService.J_ReplaceQuote(strDeducteePAN.Trim().ToUpper()) + "'," +
                                         "                          " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + "," +
                                         "                          " + TDSMAN.Classes.TDSMAN.T_pGroupId + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "MST_EMPLOYEE", " EMPLOYEE_NAME = '" + cmnService.J_ReplaceQuote(strDeducteeName) + "' " +
                                                                            " AND EMPLOYEE_PAN = '" + strDeducteePAN + "' " +
                                                                            " AND COMPANY_ID   = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))) == false)
                            {
                                strSQL = "INSERT INTO MST_EMPLOYEE(EMPLOYEE_NAME," +
                                         "                         EMPLOYEE_PAN," +
                                         "                         COMPANY_ID," +
                                         "                         GROUP_ID) " +
                                         "VALUES                  ('" + cmnService.J_ReplaceQuote(strDeducteeName.Trim().ToUpper()) + "'," +
                                         "                         '" + cmnService.J_ReplaceQuote(strDeducteePAN.Trim().ToUpper()) + "'," +
                                         "                          " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + "," +
                                         "                          " + TDSMAN.Classes.TDSMAN.T_pGroupId + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (strDeducteePAN != "PANNOTAVBL")
                        {
                            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "MST_DEDUCTEE", " DEDUCTEE_PAN = '" + strDeducteePAN + "'") == false)
                            {
                                strSQL = "INSERT INTO MST_DEDUCTEE(DEDUCTEE_CODE," +
                                         "                         DEDUCTEE_NAME," +
                                         "                         DEDUCTEE_PAN," +
                                         "                         GROUP_ID) " +
                                         "VALUES                  ('0" + cmnService.J_ReplaceQuote(strDeducteeCode.Trim()) + "'," +
                                         "                         '" + cmnService.J_ReplaceQuote(strDeducteeName.Trim().ToUpper()) + "'," +
                                         "                         '" + cmnService.J_ReplaceQuote(strDeducteePAN.Trim().ToUpper()) + "'," +
                                         "                          " + TDSMAN.Classes.TDSMAN.T_pGroupId + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "MST_DEDUCTEE", " DEDUCTEE_NAME = '" + cmnService.J_ReplaceQuote(strDeducteeName) + "' " +
                                                                            " AND DEDUCTEE_PAN = '" + strDeducteePAN + "'") == false)
                            {
                                strSQL = "INSERT INTO MST_DEDUCTEE(DEDUCTEE_CODE," +
                                         "                         DEDUCTEE_NAME," +
                                         "                         DEDUCTEE_PAN," +
                                         "                         GROUP_ID) " +
                                         "VALUES                  ('0" + cmnService.J_ReplaceQuote(strDeducteeCode.Trim()) + "'," +
                                         "                         '" + cmnService.J_ReplaceQuote(strDeducteeName.Trim().ToUpper()) + "'," +
                                         "                         '" + cmnService.J_ReplaceQuote(strDeducteePAN.Trim().ToUpper()) + "'," +
                                         "                          " + TDSMAN.Classes.TDSMAN.T_pGroupId + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    //
                    continue;
 

                    // 14 TDS INCOME TAX
                    // 15 TDS SURCHARGE
                    // 16 TDS CESS
                    // 17 TOTAL INCOME TAX DEDUCTED
                    // 18 LAST TOTAL INCOME TAX DEDUCTED
                    // 19 TOTAL TAX DEPOSITED
                    // 20 LAST TOTAL TAX DEPOSITED
                    // 21 TOTAL VALUE OF PURCHASE
                    // 22 AMOUNT OF PAYMENT
                    // 23 DATE ON WHICH AMOUNT PAID
                    // 24 DATE ON WHICH TAX DEDUCTED
                    // 25 DATE OF DEPOSIT
                    // 26 RATE
                    // 27 GROSSING UP INDICATOR
                    // 28 BOOK ENTRY/CASH INDICATOR
                    // 29 DATE OF FURNISHING TAX DEDUCTION CERTIFICATE
                    // 30 RAMARKS 1
                    // 31 RAMARKS 2
                    // 32 RAMARKS 3
                    // 33 RECORD HASH
                }

                #endregion
                
                #region SALARY DETAIL [SD]

                if (cmbFormNo.Text == T_FormNo.F24Q && cmbQuarter.Text == T_Qtr.Q4)
                {
                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "SD")
                    {
                        //intCaratPosition = ListLines[i].IndexOf("^");
                        
                        // 1 LINE NUMBER
                        // 2 RECORD TYPE
                        // 3 BATCH NUMBER
                        // 4 SALARY DETAIL RECORD NUMBER
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strSDRecordNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strSDRecordNumber = ListLines[i].Substring(intCaratPosition + 1, strSDRecordNumber.IndexOf("^"));

                        // 5 MODE
                        // 6 FILLER
                        // 7 EMPLOYEE PAN
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strEmployeePAN = ListLines[i].Substring(intCaratPosition + 1);
                        strEmployeePAN = ListLines[i].Substring(intCaratPosition + 1, strEmployeePAN.IndexOf("^"));

                        // 8 PAN REF NO
                        // 9 EMPLOYEE NAME
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strEmployeeName = ListLines[i].Substring(intCaratPosition + 1);
                        strEmployeeName = ListLines[i].Substring(intCaratPosition + 1, strEmployeeName.IndexOf("^"));

                        // 10 CATEGORY OF EMPLOYEE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strEmployeeCategory = ListLines[i].Substring(intCaratPosition + 1);
                        strEmployeeCategory = ListLines[i].Substring(intCaratPosition + 1, strEmployeeCategory.IndexOf("^"));

                        // INSERT MASTER RECORD TO EMPLOYEE MASTER
                        if (strEmployeePAN != "PANNOTAVBL")
                        {
                            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "MST_EMPLOYEE", " EMPLOYEE_PAN = '" + strEmployeePAN + "' " +
                                                                                " AND COMPANY_ID   = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))) == false)
                            {
                                strSQL = "INSERT INTO MST_EMPLOYEE(EMPLOYEE_NAME," +
                                         "                         EMPLOYEE_PAN," +
                                         "                         CATEGORY," +
                                         "                         COMPANY_ID," +
                                         "                         GROUP_ID) " +
                                         "VALUES                  ('" + cmnService.J_ReplaceQuote(strEmployeeName.Trim().ToUpper()) + "'," +
                                         "                         '" + cmnService.J_ReplaceQuote(strEmployeePAN.Trim().ToUpper()) + "'," +
                                         "                         '" + cmnService.J_ReplaceQuote(strEmployeeCategory.Trim()) + "'," +
                                         "                          " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + "," +
                                         "                          " + TDSMAN.Classes.TDSMAN.T_pGroupId + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                //// UPDATE EMPLOYEE MASTER
                                //strSQL = "UPDATE MST_EMPLOYEE SET" +
                                //    "            CATEGORY ='" + cmnService.J_ReplaceQuote(strEmployeeCategory.Trim()) + "' " +
                                //    "     WHERE  EMPLOYEE_NAME ='" + cmnService.J_ReplaceQuote(strEmployeeName.Trim()) + "' " +
                                //    "     AND    EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(strEmployeePAN.Trim()) + "' " +
                                //    "     AND    COMPANY_ID    = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                                //if (dmlService.J_ExecSql(strSQL) == false)
                                //{
                                //    return false;
                                //}
                            }
                        }
                        else
                        {
                            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "MST_EMPLOYEE", " EMPLOYEE_NAME = '" + cmnService.J_ReplaceQuote(strEmployeeName) + "' " +
                                                                            " AND EMPLOYEE_PAN = '" + strEmployeePAN + "' " +
                                                                            " AND COMPANY_ID   = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))) == false)
                            {
                                strSQL = "INSERT INTO MST_EMPLOYEE(EMPLOYEE_NAME," +
                                         "                         EMPLOYEE_PAN," +
                                         "                         CATEGORY," +
                                         "                         COMPANY_ID," +
                                         "                         GROUP_ID) " +
                                         "VALUES                  ('" + cmnService.J_ReplaceQuote(strEmployeeName.Trim().ToUpper()) + "'," +
                                         "                         '" + cmnService.J_ReplaceQuote(strEmployeePAN.Trim().ToUpper()) + "'," +
                                         "                         '" + cmnService.J_ReplaceQuote(strEmployeeCategory.Trim()) + "'," +
                                         "                          " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + "," +
                                         "                          " + TDSMAN.Classes.TDSMAN.T_pGroupId + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                //// UPDATE EMPLOYEE MASTER
                                //strSQL = "UPDATE MST_EMPLOYEE SET" +
                                //    "            CATEGORY ='" + cmnService.J_ReplaceQuote(strEmployeeCategory.Trim()) + "' " +
                                //    "     WHERE  EMPLOYEE_NAME ='" + cmnService.J_ReplaceQuote(strEmployeeName.Trim()) + "' " +
                                //    "     AND    EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(strEmployeePAN.Trim()) + "' " +
                                //    "     AND    COMPANY_ID    = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                                //if (dmlService.J_ExecSql(strSQL) == false)
                                //{
                                //    return false;
                                //}
                            }
                        }
                           
                        //
                        continue;

                        // 11 PERIOD OF EMPLOYMENT FROM DATE 
                        // 12 PERIOD OF EMPLOYMENT TO DATE
                        // 13 TOTAL AMOUNT OF SALARY
                        // 14 FILLER
                        // 15 COUNT OF SECTION 16 DETAIL RECORDS
                        // 16 GROSS TOTAL OF SECTION 16
                        // 17 INCOME CHARGEABLE UNDER HEAD SALARIES
                        // 18 INCOME OTHER THA SALARIES
                        // 19 GROSS TOTAL INCOME
                        // 20 LAST GROSS TOTAL INCOME
                        // 21 COUNT OF CHAPTER VI-A DETAIL RECORDS
                        // 22 GROSS TOTAL OF CHAPTER VI-A DETAIL RECORDS
                        // 23 TOTAL TAXABLE INCOME
                        // 24 INCOME TAX ON TOTAL INCOME
                        // 25 SURCHARGE
                        // 26 EDUCATION CESS
                        // 27 INCOME TAX RELIEF U/S 89
                        // 28 NET INCOME TAX PAYABLE
                        // 29 TOTAL AMOUNT OF TAX DEDUCTED
                        // 30 SHORTFALL/EXCESS
                        // 31 RAMARKS 1
                        // 32 RAMARKS 2
                        // 33 RAMARKS 3
                        // 34 RECORD HASH
                        
                        
                    }

                }

                #endregion

            }

            return true;
        }

        #endregion

        #region ImportTransactionsFromFVU

        private bool ImportTransactionsFromFVU(long BasicInfoID, string FVUPath, string FormNo)
        {
            #region DECLARATION

            int NumberOfLines = 0;

            // BH
            string strFormNo = "";
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

            long lngChallanID = 0;

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



            #endregion

            //
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

                intCaratPosition = ListLines[i].IndexOf("^");

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
                    strFormNo = ListLines[i].Substring(intCaratPosition + 1, strFormNo.IndexOf("^"));

                    // 6 TRANSACTION TYPE
                    // 7 BATCH UPDATION INDICATOR
                    // 8 ORIGINAL TOKEN NUMBER
                    // 9 PREVIOUS TOKEN NUMBER
                    // 10 TOKEN NUMBER OF THE STATEMENT SUBMITTED
                    // 11 TOKEN NUMBER DATE
                    // 12 LAST TAN
                    // 13 TAN
                    for (int a = 1; a < 9; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strTAN = ListLines[i].Substring(intCaratPosition + 1, 10);

                    // 14 RECEIPT NO.
                    // 15 PAN
                    for (int a = 1; a < 3; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strPAN = ListLines[i].Substring(intCaratPosition + 1, 10);

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
                    strDeductorName = ListLines[i].Substring(intCaratPosition + 1, strDeductorName.IndexOf("^"));

                    // 20 DEDUCTOR BRANCH
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorBranch = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorBranch = ListLines[i].Substring(intCaratPosition + 1, strDeductorBranch.IndexOf("^"));

                    // 21 DEDUCTOR ADDRESS1
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorAddress1 = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorAddress1 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress1.IndexOf("^"));

                    // 22 DEDUCTOR ADDRESS2
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorAddress2 = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorAddress2 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress2.IndexOf("^"));

                    // 23 DEDUCTOR ADDRESS3
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorAddress3 = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorAddress3 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress3.IndexOf("^"));

                    // 24 DEDUCTOR ADDRESS4
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorAddress4 = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorAddress4 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress4.IndexOf("^"));

                    // 25 DEDUCTOR ADDRESS5
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorAddress5 = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorAddress5 = ListLines[i].Substring(intCaratPosition + 1, strDeductorAddress5.IndexOf("^"));

                    // 26 DEDUCTOR STATE
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorStateCode = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorStateCode = ListLines[i].Substring(intCaratPosition + 1, strDeductorStateCode.IndexOf("^"));
                    //
                    if (strDeductorStateCode.Length > 0)
                        if (cmnService.J_Left(strDeductorStateCode, 1) == "0")
                            strDeductorStateCode = cmnService.J_Right(strDeductorStateCode, 1);
                    //
                    lngDeductorStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDeductorStateCode) + "'");

                    // 27 DEDUCTOR PIN
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorPIN = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorPIN = ListLines[i].Substring(intCaratPosition + 1, strDeductorPIN.IndexOf("^"));

                    // 28 DEDUCTOR EMAIL
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorEmail = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorEmail = ListLines[i].Substring(intCaratPosition + 1, strDeductorEmail.IndexOf("^"));

                    // 29 DEDUCTOR STD
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorSTD = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorSTD = ListLines[i].Substring(intCaratPosition + 1, strDeductorSTD.IndexOf("^"));

                    // 30 DEDUCTOR TELEPHONE
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorTelePhone = ListLines[i].Substring(intCaratPosition + 1, strDeductorTelePhone.IndexOf("^"));

                    // 31 DEDUCTOR CHANGE OF ADDRESS
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorChangeofAddress = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorChangeofAddress = ListLines[i].Substring(intCaratPosition + 1, strDeductorChangeofAddress.IndexOf("^"));

                    // 32 DEDUCTOR TYPE
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeductorType = ListLines[i].Substring(intCaratPosition + 1);
                    strDeductorType = ListLines[i].Substring(intCaratPosition + 1, strDeductorType.IndexOf("^"));
                    //
                    if(dblFVUVersion <= 2.126)
                        lngDeductorCategoryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT D_CATEGORY_ID FROM MST_COMPANY WHERE COMPANY_ID =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
                    else
                        lngDeductorCategoryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT CATEGORY_ID FROM MST_CATEGORY WHERE CATEGORY_CODE = '" + cmnService.J_ReplaceQuote(strDeductorType) + "'");

                    // 33 RP NAME
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPName = ListLines[i].Substring(intCaratPosition + 1);
                    strRPName = ListLines[i].Substring(intCaratPosition + 1, strRPName.IndexOf("^"));

                    // 34 RP DESIGNATION
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPDesignation = ListLines[i].Substring(intCaratPosition + 1);
                    strRPDesignation = ListLines[i].Substring(intCaratPosition + 1, strRPDesignation.IndexOf("^"));

                    // 35 RP ADDRESS1
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPAddress1 = ListLines[i].Substring(intCaratPosition + 1);
                    strRPAddress1 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress1.IndexOf("^"));

                    // 36 RP ADDRESS2
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPAddress2 = ListLines[i].Substring(intCaratPosition + 1);
                    strRPAddress2 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress2.IndexOf("^"));

                    // 37 RP ADDRESS3
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPAddress3 = ListLines[i].Substring(intCaratPosition + 1);
                    strRPAddress3 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress3.IndexOf("^"));

                    // 38 RP ADDRESS4
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPAddress4 = ListLines[i].Substring(intCaratPosition + 1);
                    strRPAddress4 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress4.IndexOf("^"));

                    // 39 RP ADDRESS5
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPAddress5 = ListLines[i].Substring(intCaratPosition + 1);
                    strRPAddress5 = ListLines[i].Substring(intCaratPosition + 1, strRPAddress5.IndexOf("^"));

                    // 40 RP STATE
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPStateCode = ListLines[i].Substring(intCaratPosition + 1);
                    strRPStateCode = ListLines[i].Substring(intCaratPosition + 1, strRPStateCode.IndexOf("^"));
                    //
                    if (strRPStateCode.Length > 0)
                        if (cmnService.J_Left(strRPStateCode, 1) == "0")
                            strRPStateCode = cmnService.J_Right(strRPStateCode, 1);
                    //
                    lngRPStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strRPStateCode) + "'");

                    // 41 RP PIN
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPPIN = ListLines[i].Substring(intCaratPosition + 1);
                    strRPPIN = ListLines[i].Substring(intCaratPosition + 1, strRPPIN.IndexOf("^"));

                    // 42 RP EMAIL
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPEmail = ListLines[i].Substring(intCaratPosition + 1);
                    strRPEmail = ListLines[i].Substring(intCaratPosition + 1, strRPEmail.IndexOf("^"));

                    // 43 RP MOBILE
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPMobile = ListLines[i].Substring(intCaratPosition + 1);
                    strRPMobile = ListLines[i].Substring(intCaratPosition + 1, strRPMobile.IndexOf("^"));

                    //if (dblFVUVersion < 3.0)
                    //    strRPMobile = "";

                    // 44 RP STD
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPSTD = ListLines[i].Substring(intCaratPosition + 1);
                    strRPSTD = ListLines[i].Substring(intCaratPosition + 1, strRPSTD.IndexOf("^"));

                    // 45 RP TELEPHONE
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPTelePhone = ListLines[i].Substring(intCaratPosition + 1);
                    strRPTelePhone = ListLines[i].Substring(intCaratPosition + 1, strRPTelePhone.IndexOf("^"));

                    // 46 RP CHANGE OF ADDRESS
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRPChangeofAddress = ListLines[i].Substring(intCaratPosition + 1);
                    strRPChangeofAddress = ListLines[i].Substring(intCaratPosition + 1, strRPChangeofAddress.IndexOf("^"));

                    // 47 BATCH TOTAL
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strBatchTotal = ListLines[i].Substring(intCaratPosition + 1);
                    strBatchTotal = ListLines[i].Substring(intCaratPosition + 1, strBatchTotal.IndexOf("^"));

                    // FOR FORM NO. 24Q
                    if (strFormNo == T_FormNo.F24Q)
                    {
                        // 48 UNMATCHED CHALLAN COUNT
                        // 49 COUNT SALARY DETAIL RECORDS
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strCountSalaryDetailRecords = ListLines[i].Substring(intCaratPosition + 1);
                        strCountSalaryDetailRecords = ListLines[i].Substring(intCaratPosition + 1, strCountSalaryDetailRecords.IndexOf("^"));

                        // 50 COUNT SALARY DETAIL RECORDS
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strBatchTotalSalary = ListLines[i].Substring(intCaratPosition + 1);
                        strBatchTotalSalary = ListLines[i].Substring(intCaratPosition + 1, strBatchTotalSalary.IndexOf("^"));
                        //
                        intLoop = 2;
                    }
                    else
                        intLoop = 5;
                    //
                    // 51 AO APPROVAL
                    for (int a = 1; a < intLoop; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strAOApproval = ListLines[i].Substring(intCaratPosition + 1);
                    strAOApproval = ListLines[i].Substring(intCaratPosition + 1, strAOApproval.IndexOf("^"));

                    // 52 AO APPROVAL NUMBER
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strAOApprovalNumber = ListLines[i].Substring(intCaratPosition + 1);
                    strAOApprovalNumber = ListLines[i].Substring(intCaratPosition + 1, strAOApprovalNumber.IndexOf("^"));

                    // 53 LAST DEDUCTOR TYPE
                    if (dblFVUVersion > 2.126)
                    {
                        // 54 DEDUCTOR STATE NAME
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDStateCode = ListLines[i].Substring(intCaratPosition + 1);
                        strDStateCode = ListLines[i].Substring(intCaratPosition + 1, strDStateCode.IndexOf("^"));
                        //
                        if (strDStateCode.Length > 0)
                            if (cmnService.J_Left(strDStateCode, 1) == "0")
                                strDStateCode = cmnService.J_Right(strDStateCode, 1);
                        //
                        lngDStateID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT STATE_ID FROM MST_STATE WHERE STATE_CODE = '" + cmnService.J_ReplaceQuote(strDStateCode) + "'");

                        // 55 PAO CODE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strPAO = ListLines[i].Substring(intCaratPosition + 1);
                        strPAO = ListLines[i].Substring(intCaratPosition + 1, strPAO.IndexOf("^"));

                        // 56 DDO CODE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDDO = ListLines[i].Substring(intCaratPosition + 1);
                        strDDO = ListLines[i].Substring(intCaratPosition + 1, strDDO.IndexOf("^"));

                        // 57 MINISTRY
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strMinistryName = ListLines[i].Substring(intCaratPosition + 1);
                        strMinistryName = ListLines[i].Substring(intCaratPosition + 1, strMinistryName.IndexOf("^"));
                        //
                        lngMinistryId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT MINISTRY_ID FROM MST_MINISTRY WHERE MINISTRY_CODE = '" + cmnService.J_ReplaceQuote(strMinistryName) + "'");

                        // 58 OTHER MINISTRY
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strOtherMinistryName = ListLines[i].Substring(intCaratPosition + 1);
                        strOtherMinistryName = ListLines[i].Substring(intCaratPosition + 1, strOtherMinistryName.IndexOf("^"));

                        // 59 FILLER 2
                        // 60 PAO REGISTRATION NUMBER
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strPAORegNo = ListLines[i].Substring(intCaratPosition + 1);
                        strPAORegNo = ListLines[i].Substring(intCaratPosition + 1, strPAORegNo.IndexOf("^"));

                        // 61 DDO REGISTRATION NUMBER
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strDDORegNo = ListLines[i].Substring(intCaratPosition + 1);
                        strDDORegNo = ListLines[i].Substring(intCaratPosition + 1, strDDORegNo.IndexOf("^"));
                    }
                    // 62 RECORD HASH

                    // DELETE TRN_COMPANY_INFO BASED ON BASIC_INFO_ID
                    strSQL = "DELETE FROM TRN_COMPANY_INFO WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        return false;
                    }
                    // INSERT TRN_COMPANY_INFO
                    strSQL = "INSERT INTO TRN_COMPANY_INFO (" +
                     "            BASIC_INFO_ID," +
                     "            COMPANY_ID," +
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
                     "            CIT_TDS_ADDRESS," +
                     "            CIT_TDS_CITY," +
                     "            CIT_TDS_PINCODE) " +
                     "     VALUES( " + lngBasicInfoID + "," +
                     "             " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + "," +
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
                     "            '" + cmnService.J_ReplaceQuote(txtCITAddress.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtCITCity.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtCITPIN.Text.Trim()) + "')";
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
                    strCDRecordNumber = ListLines[i].Substring(intCaratPosition + 1, strCDRecordNumber.IndexOf("^"));

                    // 5 COUNT OF DEDUCTEE RECORDS
                    // 6 NIL CHALLAN INDICATOR
                    for (int a = 1; a < 3; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strNILChallanIndicator = ListLines[i].Substring(intCaratPosition + 1);
                    strNILChallanIndicator = ListLines[i].Substring(intCaratPosition + 1, strNILChallanIndicator.IndexOf("^"));
                    //
                    if (strNILChallanIndicator == "Y")
                        intNILChallanIndicator = 1;
                    else if (strNILChallanIndicator == "N")
                        intNILChallanIndicator = 0;

                    // 7 CHALLAN UPDATION INDICATOR
                    // 8 FILLER 3
                    // 9 FILLER 4
                    // 10 FILLER 5
                    // 11 LAST BANK CHALLAN NUMBER
                    // 12 CHALLAN NUMBER
                    for (int a = 1; a < 7; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strChallanNumber = ListLines[i].Substring(intCaratPosition + 1);
                    strChallanNumber = ListLines[i].Substring(intCaratPosition + 1, strChallanNumber.IndexOf("^"));

                    // 13 LAST TRANSFER VOUCHER NUMBER
                    // 14 TRANSFER VOUCHER NUMBER
                    for (int a = 1; a < 3; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strTransferVoucherNumber = ListLines[i].Substring(intCaratPosition + 1);
                    strTransferVoucherNumber = ListLines[i].Substring(intCaratPosition + 1, strTransferVoucherNumber.IndexOf("^"));

                    // 15 LAST BANK BRANCH CODE
                    // 16 BANK BRANCH CODE
                    for (int a = 1; a < 3; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strBSRCode = ListLines[i].Substring(intCaratPosition + 1);
                    strBSRCode = ListLines[i].Substring(intCaratPosition + 1, strBSRCode.IndexOf("^"));

                    // 17 LAST DATE DATE BANK CHALLAN
                    // 18 DATE BANK CHALLAN
                    for (int a = 1; a < 3; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDateofBankChallan = ListLines[i].Substring(intCaratPosition + 1);
                    strDateofBankChallan = ListLines[i].Substring(intCaratPosition + 1, strDateofBankChallan.IndexOf("^"));
                    strDateofBankChallan = cmnService.J_Left(strDateofBankChallan, 2) + "/" + cmnService.J_Mid(strDateofBankChallan,2,2) + "/" + cmnService.J_Right(strDateofBankChallan, 4);

                    // 19 FILLER 6
                    // 20 FILLER 7
                    // 21 SECTION
                    for (int a = 1; a < 4; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strSection = ListLines[i].Substring(intCaratPosition + 1);
                    strSection = ListLines[i].Substring(intCaratPosition + 1, strSection.IndexOf("^"));
                    //
                    lngSectionID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT SECTION_ID FROM MST_SECTION WHERE SECTION_NAME = '" + cmnService.J_ReplaceQuote(strSection) + "'");

                    // 22 OLTAS - INCOME TAX 
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strOLTASIncomeTax = ListLines[i].Substring(intCaratPosition + 1);
                    strOLTASIncomeTax = ListLines[i].Substring(intCaratPosition + 1, strOLTASIncomeTax.IndexOf("^"));

                    // 23 OLTAS - SURCHARGE
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strOLTASSurcharge = ListLines[i].Substring(intCaratPosition + 1);
                    strOLTASSurcharge = ListLines[i].Substring(intCaratPosition + 1, strOLTASSurcharge.IndexOf("^"));

                    // 24 OLTAS - CESS
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strOLTASCess = ListLines[i].Substring(intCaratPosition + 1);
                    strOLTASCess = ListLines[i].Substring(intCaratPosition + 1, strOLTASCess.IndexOf("^"));

                    // 25 OLTAS - INTEREST AMOUNT
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strOLTASInterestAmount = ListLines[i].Substring(intCaratPosition + 1);
                    strOLTASInterestAmount = ListLines[i].Substring(intCaratPosition + 1, strOLTASInterestAmount.IndexOf("^"));

                    // 26 OLTAS - OTHERS
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strOLTASOthers = ListLines[i].Substring(intCaratPosition + 1);
                    strOLTASOthers = ListLines[i].Substring(intCaratPosition + 1, strOLTASOthers.IndexOf("^"));

                    // 27 TOTAL DEPOSITED AMOUNT
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strTotalDepositedAmount = ListLines[i].Substring(intCaratPosition + 1);
                    strTotalDepositedAmount = ListLines[i].Substring(intCaratPosition + 1, strTotalDepositedAmount.IndexOf("^"));
                    
                    // 28 LAST TOTAL DEPOSITED AMOUNT +
                    // 29 TOTAL TAX DEPOSIT AMOUNT
                    for (int a = 1; a < 3; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strTotalTaxDepositedAmount = ListLines[i].Substring(intCaratPosition + 1);
                    strTotalTaxDepositedAmount = ListLines[i].Substring(intCaratPosition + 1, strTotalTaxDepositedAmount.IndexOf("^"));

                    // 30 INCOME TAX
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strIncomeTax = ListLines[i].Substring(intCaratPosition + 1);
                    strIncomeTax = ListLines[i].Substring(intCaratPosition + 1, strIncomeTax.IndexOf("^"));

                    // 31 SURCHARGE
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strSurcharge = ListLines[i].Substring(intCaratPosition + 1);
                    strSurcharge = ListLines[i].Substring(intCaratPosition + 1, strSurcharge.IndexOf("^"));

                    // 32 CESS
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strCess = ListLines[i].Substring(intCaratPosition + 1);
                    strCess = ListLines[i].Substring(intCaratPosition + 1, strCess.IndexOf("^"));

                    // 33 SUM OF TOTAL INCOME TAX
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strSumTotalIncomeTax = ListLines[i].Substring(intCaratPosition + 1);
                    strSumTotalIncomeTax = ListLines[i].Substring(intCaratPosition + 1, strSumTotalIncomeTax.IndexOf("^"));

                    // 34 INTERST AMOUNT
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strInterestAmount = ListLines[i].Substring(intCaratPosition + 1);
                    strInterestAmount = ListLines[i].Substring(intCaratPosition + 1, strInterestAmount.IndexOf("^"));

                    // 35 OTHERS
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strOthersAmount = ListLines[i].Substring(intCaratPosition + 1);
                    strOthersAmount = ListLines[i].Substring(intCaratPosition + 1, strOthersAmount.IndexOf("^"));

                    // 36 CHEQUE/DD NUMBER
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strChequeDDNumber = ListLines[i].Substring(intCaratPosition + 1);
                    strChequeDDNumber = ListLines[i].Substring(intCaratPosition + 1, strChequeDDNumber.IndexOf("^"));
                    //
                    if (strChequeDDNumber == "0")
                        strChequeDDNumber = "";
                    // 37 BOOK ENTRY
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strBookEntry = ListLines[i].Substring(intCaratPosition + 1);
                    strBookEntry = ListLines[i].Substring(intCaratPosition + 1, strBookEntry.IndexOf("^"));
                    //
                    if (strBookEntry.ToUpper() == "Y")
                        intBookEntry = 1;
                    else
                        intBookEntry = 0;
                    // 38 REMARKS
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strCDRemarks = ListLines[i].Substring(intCaratPosition + 1);
                    strCDRemarks = ListLines[i].Substring(intCaratPosition + 1, strCDRemarks.IndexOf("^"));

                    // 39 RECORD HASH +
                    //######################################
                    // DATA UPDATE
                    strSQL = "UPDATE TRN_BASIC_INFO SET NIL_RETURN = "  + intNILChallanIndicator + " WHERE BASIC_INFO_ID = " + BasicInfoID;
                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        return false;
                    }
                    // DATA INSERT

                    strSQL = "INSERT INTO TRN_CHALLAN (" +
                             "            BASIC_INFO_ID," +
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
                             "            CTRL_TOT) " +
                             "     VALUES(" + BasicInfoID + "," +
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
                             "            " + cmnService.J_ReturnDoubleValue(strSumTotalIncomeTax) + ")";
                    //-----------------------------------------------------------
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        return false;
                    }
                    intCDSerialNo = intCDSerialNo + 1;
                    //
                    continue;
                }

                #endregion

                #region DEDUCTEE DETAIL [DD]

                if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "DD")
                {
                    //intCaratPosition = ListLines[i].IndexOf("^");

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
                    strDDRecordNumber = ListLines[i].Substring(intCaratPosition + 1, strDDRecordNumber.IndexOf("^"));

                    // 6 MODE
                    // 7 EMPLOYEE SERIAL NO
                    // 8 DEDUCTEE CODE
                    for (int a = 1; a < 4; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeducteeCode = ListLines[i].Substring(intCaratPosition + 1);
                    strDeducteeCode = ListLines[i].Substring(intCaratPosition + 1, strDeducteeCode.IndexOf("^"));

                    // 9 LAST EMPLOYEE PAN
                    // 10 DEDUCTEE PAN
                    for (int a = 1; a < 3; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeducteePAN = ListLines[i].Substring(intCaratPosition + 1);
                    strDeducteePAN = ListLines[i].Substring(intCaratPosition + 1, strDeducteePAN.IndexOf("^"));

                    // 11 LAST PAN REF NO
                    // 12 PAN REF NO
                    // 13 NAME OF DEDUCTEE
                    for (int a = 1; a < 4; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDeducteeName = ListLines[i].Substring(intCaratPosition + 1);
                    strDeducteeName = ListLines[i].Substring(intCaratPosition + 1, strDeducteeName.IndexOf("^"));

                    // 
                    if (cmbFormNo.Text == T_FormNo.F24Q)
                        if(strDeducteePAN == "PANNOTAVBL")
                            lngPartyId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT EMPLOYEE_ID FROM MST_EMPLOYEE WHERE " +
                                              "EMPLOYEE_NAME = '" + cmnService.J_ReplaceQuote(strDeducteeName) + "' AND " +
                                              "EMPLOYEE_PAN  = '" + cmnService.J_ReplaceQuote(strDeducteePAN) + "'");
                        else
                            lngPartyId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT EMPLOYEE_ID FROM MST_EMPLOYEE WHERE " +
                                              "EMPLOYEE_PAN  = '" + cmnService.J_ReplaceQuote(strDeducteePAN) + "'");
                    else
                        if (strDeducteePAN == "PANNOTAVBL")
                            lngPartyId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT DEDUCTEE_ID FROM MST_DEDUCTEE WHERE " +
                                          "DEDUCTEE_NAME = '" + cmnService.J_ReplaceQuote(strDeducteeName) + "' AND " +
                                          "DEDUCTEE_PAN  = '" + cmnService.J_ReplaceQuote(strDeducteePAN) + "'");
                        else
                            lngPartyId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT DEDUCTEE_ID FROM MST_DEDUCTEE WHERE " +
                                          "DEDUCTEE_PAN  = '" + cmnService.J_ReplaceQuote(strDeducteePAN) + "'");
                    //--
                    // 14 TDS INCOME TAX
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strTDSIncomeTax = ListLines[i].Substring(intCaratPosition + 1);
                    strTDSIncomeTax = ListLines[i].Substring(intCaratPosition + 1, strTDSIncomeTax.IndexOf("^"));

                    // 15 TDS SURCHARGE
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strTDSSurcharge = ListLines[i].Substring(intCaratPosition + 1);
                    strTDSSurcharge = ListLines[i].Substring(intCaratPosition + 1, strTDSSurcharge.IndexOf("^"));

                    // 16 TDS CESS
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strTDSCess = ListLines[i].Substring(intCaratPosition + 1);
                    strTDSCess = ListLines[i].Substring(intCaratPosition + 1, strTDSCess.IndexOf("^"));

                    // 17 TOTAL INCOME TAX DEDUCTED
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strTotalIncomeTaxDeducted = ListLines[i].Substring(intCaratPosition + 1);
                    strTotalIncomeTaxDeducted = ListLines[i].Substring(intCaratPosition + 1, strTotalIncomeTaxDeducted.IndexOf("^"));

                    // 18 LAST TOTAL INCOME TAX DEDUCTED
                    // 19 TOTAL TAX DEPOSITED
                    for (int a = 1; a < 3; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strTotalTaxDeposited = ListLines[i].Substring(intCaratPosition + 1);
                    strTotalTaxDeposited = ListLines[i].Substring(intCaratPosition + 1, strTotalTaxDeposited.IndexOf("^"));

                    // 20 LAST TOTAL TAX DEPOSITED
                    // 21 TOTAL VALUE OF PURCHASE
                    if (FormNo == T_FormNo.F27EQ)
                    {
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTotalValueOfPurchase = ListLines[i].Substring(intCaratPosition + 1);
                        strTotalValueOfPurchase = ListLines[i].Substring(intCaratPosition + 1, strTotalValueOfPurchase.IndexOf("^"));

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
                    strAmountOfPayment = ListLines[i].Substring(intCaratPosition + 1, strAmountOfPayment.IndexOf("^"));

                    // 23 DATE ON WHICH AMOUNT PAID
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDateAmountPaid = ListLines[i].Substring(intCaratPosition + 1);
                    strDateAmountPaid = ListLines[i].Substring(intCaratPosition + 1, strDateAmountPaid.IndexOf("^"));
                    strDateAmountPaid = cmnService.J_Left(strDateAmountPaid, 2) + "/" + cmnService.J_Mid(strDateAmountPaid, 2, 2) + "/" + cmnService.J_Right(strDateAmountPaid, 4);

                    // 24 DATE ON WHICH TAX DEDUCTED
                    for (int a = 1; a < 2; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strDateTaxDeducted = ListLines[i].Substring(intCaratPosition + 1);
                    strDateTaxDeducted = ListLines[i].Substring(intCaratPosition + 1, strDateTaxDeducted.IndexOf("^"));
                    if (strDateTaxDeducted == "")
                        strDateTaxDeducted = "NULL";
                    else
                        strDateTaxDeducted = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(cmnService.J_Left(strDateTaxDeducted, 2) + "/" + cmnService.J_Mid(strDateTaxDeducted, 2, 2) + "/" + cmnService.J_Right(strDateTaxDeducted, 4))  + cmnService.J_DateOperator();

                    // 25 DATE OF DEPOSIT
                    // 26 RATE
                    for (int a = 1; a < 3; a++)
                    {
                        intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                    }
                    strRate = ListLines[i].Substring(intCaratPosition + 1);
                    strRate = ListLines[i].Substring(intCaratPosition + 1, strRate.IndexOf("^"));

                    // 27 GROSSING UP INDICATOR
                    if (FormNo == T_FormNo.F27Q)
                    {
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strGrossingUpIndicator = ListLines[i].Substring(intCaratPosition + 1);
                        strGrossingUpIndicator = ListLines[i].Substring(intCaratPosition + 1, strGrossingUpIndicator.IndexOf("^"));

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
                    strBookEntry = ListLines[i].Substring(intCaratPosition + 1, strBookEntry.IndexOf("^"));
                    //--
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
                    strRemarks1 = ListLines[i].Substring(intCaratPosition + 1, strRemarks1.IndexOf("^"));

                    // 31 REMARKS 2
                    // 32 REMARKS 3
                    // 33 RECORD HASH

                    // GET CHALLAN ID
                    lngChallanID = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT MAX(CHALLAN_ID) FROM TRN_CHALLAN");

                    // DATA INSERT
                    strSQL = "INSERT INTO TRN_DEDUCTEE_DETAILS " +
                             "           (CHALLAN_ID," +
                             "            BASIC_INFO_ID," +
                             "            SL_NO," +
                             "            PARTY_ID," +
                             "            PAYMENT_DATE," +
                             "            DEDUCTED_DATE," +
                             "            PAYMENT_AMOUNT," +
                             "            RATE," +
                             "            TAX_AMOUNT," +
                             "            SURCHARGE_AMOUNT," +
                             "            CESS_AMOUNT," +
                             "            TOTAL_AMOUNT," +
                             "            TAX_DEPOSITED_AMOUNT,";
                    if (FormNo == T_FormNo.F27Q)
                        strSQL = strSQL + "GROSSING_UP_INDICATOR,";
                    else if (FormNo == T_FormNo.F27EQ)
                        strSQL = strSQL + "TOT_VALUE_PURCHASE,";

                    strSQL = strSQL + "   NON_DEDUCTION_FLAG," +
                             "            CASH_BOOK_ENTRY) " +
                             "     VALUES(" + lngChallanID + "," +
                             "            " + BasicInfoID + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strDDRecordNumber) + "," +
                             "            " + lngPartyId + "," +
                             "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strDateAmountPaid) + cmnService.J_DateOperator() + "," +
                             "            " + strDateTaxDeducted + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strAmountOfPayment) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strRate) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strTDSIncomeTax) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strTDSSurcharge) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strTDSCess) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strTotalIncomeTaxDeducted) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strTotalTaxDeposited) + ",";
                    if (FormNo == T_FormNo.F27Q)
                        strSQL = strSQL + "'" + cmnService.J_ReplaceQuote(strGrossingUpIndicator) + "',";
                    else if (FormNo == T_FormNo.F27EQ)
                        strSQL = strSQL + " " + cmnService.J_ReturnDoubleValue(strTotalValueOfPurchase) + ",";

                    strSQL = strSQL + "  '" + cmnService.J_ReplaceQuote(strRemarks1) + "'," +
                             "            " + intBookEntry + ")";
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

                if (cmbFormNo.Text == T_FormNo.F24Q && cmbQuarter.Text == T_Qtr.Q4)
                {
                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "SD")
                    {
                        //intCaratPosition = ListLines[i].IndexOf("^");

                        // 1 LINE NUMBER
                        // 2 RECORD TYPE
                        // 3 BATCH NUMBER
                        // 4 SALARY DETAIL RECORD NUMBER
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strSDRecordNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strSDRecordNumber = ListLines[i].Substring(intCaratPosition + 1, strSDRecordNumber.IndexOf("^"));

                        // 5 MODE
                        // 6 FILLER
                        // 7 EMPLOYEE PAN
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strEmployeePAN = ListLines[i].Substring(intCaratPosition + 1);
                        strEmployeePAN = ListLines[i].Substring(intCaratPosition + 1, strEmployeePAN.IndexOf("^"));

                        // 8 PAN REF NO
                        // 9 EMPLOYEE NAME
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strEmployeeName = ListLines[i].Substring(intCaratPosition + 1);
                        strEmployeeName = ListLines[i].Substring(intCaratPosition + 1, strEmployeeName.IndexOf("^"));

                        // 10 CATEGORY OF EMPLOYEE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strEmployeeCategory = ListLines[i].Substring(intCaratPosition + 1);
                        strEmployeeCategory = ListLines[i].Substring(intCaratPosition + 1, strEmployeeCategory.IndexOf("^"));

                        // GET PARTY ID
                        lngPartyId = 0;
                        if (strEmployeePAN == "PANNOTAVBL")
                            lngPartyId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT EMPLOYEE_ID FROM MST_EMPLOYEE WHERE " +
                                              "EMPLOYEE_NAME ='" + cmnService.J_ReplaceQuote(strEmployeeName) + "' AND " +
                                              "EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(strEmployeePAN) + "' AND " +
                                              "COMPANY_ID    =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
                        else
                            lngPartyId = dmlService.J_ReturnId(dmlService.J_pCommand, "SELECT EMPLOYEE_ID FROM MST_EMPLOYEE WHERE " +
                                              "EMPLOYEE_PAN ='" + cmnService.J_ReplaceQuote(strEmployeePAN) + "' AND " +
                                              "COMPANY_ID   =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
                        
                        // UPDATE CATEGORY
                        strSQL = "UPDATE MST_EMPLOYEE SET CATEGORY = '" + strEmployeeCategory + "' WHERE EMPLOYEE_ID = " + lngPartyId;
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                        // 11 PERIOD OF EMPLOYMENT FROM DATE 
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strPeriodFromDate = ListLines[i].Substring(intCaratPosition + 1);
                        strPeriodFromDate = ListLines[i].Substring(intCaratPosition + 1, strPeriodFromDate.IndexOf("^"));
                        strPeriodFromDate = cmnService.J_Left(strPeriodFromDate, 2) + "/" + cmnService.J_Mid(strPeriodFromDate, 2, 2) + "/" + cmnService.J_Right(strPeriodFromDate, 4);

                        // 12 PERIOD OF EMPLOYMENT TO DATE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strPeriodToDate = ListLines[i].Substring(intCaratPosition + 1);
                        strPeriodToDate = ListLines[i].Substring(intCaratPosition + 1, strPeriodToDate.IndexOf("^"));
                        strPeriodToDate = cmnService.J_Left(strPeriodToDate, 2) + "/" + cmnService.J_Mid(strPeriodToDate, 2, 2) + "/" + cmnService.J_Right(strPeriodToDate, 4);

                        // 13 TOTAL AMOUNT OF SALARY
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTotalAmountSalary = ListLines[i].Substring(intCaratPosition + 1);
                        strTotalAmountSalary = ListLines[i].Substring(intCaratPosition + 1, strTotalAmountSalary.IndexOf("^"));

                        // 14 FILLER
                        // 15 COUNT OF SECTION 16 DETAIL RECORDS
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strCountSec16Records = ListLines[i].Substring(intCaratPosition + 1);
                        strCountSec16Records = ListLines[i].Substring(intCaratPosition + 1, strCountSec16Records.IndexOf("^"));

                        // 16 GROSS TOTAL OF SECTION 16
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strGrossTotalSec16 = ListLines[i].Substring(intCaratPosition + 1);
                        strGrossTotalSec16 = ListLines[i].Substring(intCaratPosition + 1, strGrossTotalSec16.IndexOf("^"));
                        
                        // 17 INCOME CHARGEABLE UNDER HEAD SALARIES
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strIncomeChargeableUnderHeadSalaries = ListLines[i].Substring(intCaratPosition + 1);
                        strIncomeChargeableUnderHeadSalaries = ListLines[i].Substring(intCaratPosition + 1, strIncomeChargeableUnderHeadSalaries.IndexOf("^"));

                        // 18 INCOME OTHER THAN SALARIES
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strIncomeOtherThanSalaries = ListLines[i].Substring(intCaratPosition + 1);
                        strIncomeOtherThanSalaries = ListLines[i].Substring(intCaratPosition + 1, strIncomeOtherThanSalaries.IndexOf("^"));

                        // 19 GROSS TOTAL INCOME
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strGrossTotalIncome = ListLines[i].Substring(intCaratPosition + 1);
                        strGrossTotalIncome = ListLines[i].Substring(intCaratPosition + 1, strGrossTotalIncome.IndexOf("^"));

                        // 20 LAST GROSS TOTAL INCOME
                        // 21 COUNT OF CHAPTER VI-A DETAIL RECORDS
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strCountChVIARecords = ListLines[i].Substring(intCaratPosition + 1);
                        strCountChVIARecords = ListLines[i].Substring(intCaratPosition + 1, strCountChVIARecords.IndexOf("^"));

                        // 22 GROSS TOTAL OF CHAPTER VI-A DETAIL RECORDS
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strGrossTotalChVIA = ListLines[i].Substring(intCaratPosition + 1);
                        strGrossTotalChVIA = ListLines[i].Substring(intCaratPosition + 1, strGrossTotalChVIA.IndexOf("^"));

                        // 23 TOTAL TAXABLE INCOME
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTotalTaxableIncome = ListLines[i].Substring(intCaratPosition + 1);
                        strTotalTaxableIncome = ListLines[i].Substring(intCaratPosition + 1, strTotalTaxableIncome.IndexOf("^"));

                        // 24 INCOME TAX ON TOTAL INCOME
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strIncomeTaxOnTotalIncome = ListLines[i].Substring(intCaratPosition + 1);
                        strIncomeTaxOnTotalIncome = ListLines[i].Substring(intCaratPosition + 1, strIncomeTaxOnTotalIncome.IndexOf("^"));

                        // 25 SURCHARGE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strSurchargeSD = ListLines[i].Substring(intCaratPosition + 1);
                        strSurchargeSD = ListLines[i].Substring(intCaratPosition + 1, strSurchargeSD.IndexOf("^"));

                        // 26 EDUCATION CESS
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strEducationCess = ListLines[i].Substring(intCaratPosition + 1);
                        strEducationCess = ListLines[i].Substring(intCaratPosition + 1, strEducationCess.IndexOf("^"));

                        // 27 INCOME TAX RELIEF U/S 89
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strIncomeTaxRelief = ListLines[i].Substring(intCaratPosition + 1);
                        strIncomeTaxRelief = ListLines[i].Substring(intCaratPosition + 1, strIncomeTaxRelief.IndexOf("^"));

                        // 28 NET INCOME TAX PAYABLE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strNetIncomeTaxPayable = ListLines[i].Substring(intCaratPosition + 1);
                        strNetIncomeTaxPayable = ListLines[i].Substring(intCaratPosition + 1, strNetIncomeTaxPayable.IndexOf("^"));

                        // 29 TOTAL AMOUNT OF TAX DEDUCTED
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTotalAmountOfTaxDeducted = ListLines[i].Substring(intCaratPosition + 1);
                        strTotalAmountOfTaxDeducted = ListLines[i].Substring(intCaratPosition + 1, strTotalAmountOfTaxDeducted.IndexOf("^"));

                        // 30 SHORTFALL/EXCESS
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strShortfallExcess = ListLines[i].Substring(intCaratPosition + 1);
                        strShortfallExcess = ListLines[i].Substring(intCaratPosition + 1, strShortfallExcess.IndexOf("^"));

                        // 31 RAMARKS 1
                        // 32 RAMARKS 2
                        // 33 RAMARKS 3
                        // 34 RECORD HASH
                        // DATA INSERT
                        //-----------------------------------------------------------
                        strSQL = "INSERT INTO TRN_SALARY_DETAILS (" +
                                "             BASIC_INFO_ID," +
                                "             EMPLOYEE_ID," +
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
                                "             SHORTFALL_TAX) " +
                                "     VALUES(" + lngBasicInfoID + "," +
                                "            " + lngPartyId + "," +
                                "            " + cmnService.J_ReturnInt32Value(strSDRecordNumber) + "," +
                                "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strPeriodFromDate) + cmnService.J_DateOperator() + "," +
                                "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strPeriodToDate) + cmnService.J_DateOperator() + "," +
                                "            " + cmnService.J_ReturnDoubleValue(strTotalAmountSalary) + "," +
                                "            " + cmnService.J_ReturnDoubleValue(strGrossTotalSec16) + "," +
                                "            " + cmnService.J_ReturnDoubleValue(strIncomeChargeableUnderHeadSalaries) + "," +
                                "            " + cmnService.J_ReturnDoubleValue(strIncomeOtherThanSalaries) + "," +
                                "            " + cmnService.J_ReturnDoubleValue(strGrossTotalIncome) + "," +
                                "           '" + cmnService.J_ReturnDoubleValue(strGrossTotalChVIA) + "'," +
                                "            " + cmnService.J_ReturnDoubleValue(strTotalTaxableIncome) + "," +
                                "           '" + cmnService.J_ReturnDoubleValue(strIncomeTaxOnTotalIncome) + "'," +
                                "            " + cmnService.J_ReturnDoubleValue(strSurchargeSD) + "," +
                                "            " + cmnService.J_ReturnDoubleValue(strEducationCess) + "," +
                                "            " + (cmnService.J_ReturnDoubleValue(strIncomeTaxOnTotalIncome) + cmnService.J_ReturnDoubleValue(strSurchargeSD) + cmnService.J_ReturnDoubleValue(strEducationCess)) + "," +
                                "            " + cmnService.J_ReturnDoubleValue(strIncomeTaxRelief) + "," +
                                "            " + cmnService.J_ReturnDoubleValue(strNetIncomeTaxPayable) + "," +
                                "            " + cmnService.J_ReturnDoubleValue(strTotalAmountOfTaxDeducted) + "," +
                                "            " + cmnService.J_ReturnDoubleValue(strShortfallExcess) + ")";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                    }
                    else if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 3) == "S16")
                    {
                        #region SALARY DETAIL [SD-S16]
                        //intCaratPosition = ListLines[i].IndexOf("^");

                        // 1 LINE NUMBER
                        // 2 RECORD TYPE
                        // 3 BATCH NUMBER
                        // 4 SALARY DETAIL RECORD NUMBER
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strSDS16RecordNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strSDS16RecordNumber = ListLines[i].Substring(intCaratPosition + 1, strSDS16RecordNumber.IndexOf("^"));

                        // 5 SALARY DETAIL - SECTION 16 RECORD NUMBER
                        // 6 SECTION 16 ID
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strS16SectionID = ListLines[i].Substring(intCaratPosition + 1);
                        strS16SectionID = ListLines[i].Substring(intCaratPosition + 1, strS16SectionID.IndexOf("^"));

                        // 7 TOTAL DEDUCTION
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strS16TotalDeduction = ListLines[i].Substring(intCaratPosition + 1);
                        strS16TotalDeduction = ListLines[i].Substring(intCaratPosition + 1, strS16TotalDeduction.IndexOf("^"));

                        // 8 RECORD HASH
                        string strUS_16Field = "";
                        //
                        if (strS16SectionID == "16(ii)")
                            strUS_16Field = "US_16_EA";
                        else
                            strUS_16Field = "US_16_TE";
                        //
                        strSQL = "UPDATE TRN_SALARY_DETAILS SET" +
                            "            " + strUS_16Field + " = " + cmnService.J_ReturnDoubleValue(strS16TotalDeduction) + " " +
                            "     WHERE  SALARY_DETAILS_ID = (SELECT MAX(SALARY_DETAILS_ID) FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID + ")";
                        //-----------------------------------------------
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                        //
                        continue;
                        //
                        #endregion
                    }
                    else if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 3) == "C6A")
                    {
                        #region SALARY DETAIL [SD-C6A]
                        
                        //intCaratPosition = ListLines[i].IndexOf("^");

                        // 1 LINE NUMBER
                        // 2 RECORD TYPE
                        // 3 BATCH NUMBER
                        // 4 SALARY DETAIL RECORD NUMBER
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strSDC6ARecordNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strSDC6ARecordNumber = ListLines[i].Substring(intCaratPosition + 1, strSDC6ARecordNumber.IndexOf("^"));

                        // 5 SALARY DETAIL - CHAPTER VI A RECORD NUMBER
                        // 6 CHAPTER VI A ID
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strC6ASectionID = ListLines[i].Substring(intCaratPosition + 1);
                        strC6ASectionID = ListLines[i].Substring(intCaratPosition + 1, strC6ASectionID.IndexOf("^"));

                        // 7 TOTAL AMOUNT UNDER CHAPTER VI A
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strC6ATotalAmount = ListLines[i].Substring(intCaratPosition + 1);
                        strC6ATotalAmount = ListLines[i].Substring(intCaratPosition + 1, strC6ATotalAmount.IndexOf("^"));

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
                        strSQL = "UPDATE TRN_SALARY_DETAILS SET" +
                            "            " + strC6AField + " = " + cmnService.J_ReturnDoubleValue(strC6ATotalAmount) + " " +
                            "     WHERE  SALARY_DETAILS_ID = (SELECT MAX(SALARY_DETAILS_ID) FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID + ")";
                        //-----------------------------------------------
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                        //
                        continue;

                        #endregion
                    }
                    //
                    continue;
                }              
                    

                #endregion

                #region COMMENTED
                /*
                #region SALARY DETAIL [SD-S16]

                if (cmbFormNo.Text == T_FormNo.F24Q && cmbQuarter.Text == T_Qtr.Q4)
                {
                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 3) == "S16")
                    {
                        //intCaratPosition = ListLines[i].IndexOf("^");

                        // 1 LINE NUMBER
                        // 2 RECORD TYPE
                        // 3 BATCH NUMBER
                        // 4 SALARY DETAIL RECORD NUMBER
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strSDS16RecordNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strSDS16RecordNumber = ListLines[i].Substring(intCaratPosition + 1, strSDS16RecordNumber.IndexOf("^"));

                        // 5 SALARY DETAIL - SECTION 16 RECORD NUMBER
                        // 6 SECTION 16 ID
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strS16SectionID = ListLines[i].Substring(intCaratPosition + 1);
                        strS16SectionID = ListLines[i].Substring(intCaratPosition + 1, strS16SectionID.IndexOf("^"));

                        // 7 TOTAL DEDUCTION
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strS16TotalDeduction = ListLines[i].Substring(intCaratPosition + 1);
                        strS16TotalDeduction = ListLines[i].Substring(intCaratPosition + 1, strS16TotalDeduction.IndexOf("^"));

                        // 8 RECORD HASH
                        string strUS_16Field = "";
                        //
                        if (strS16SectionID == "16(ii)")
                            strUS_16Field = "US_16_EA";
                        else
                            strUS_16Field = "US_16_TE";
                        //
                        strSQL = "UPDATE TRN_SALARY_DETAILS SET" +
                            "            " + strUS_16Field + " = " + cmnService.J_ReturnDoubleValue(strS16TotalDeduction) + " " +
                            "     WHERE  SALARY_DETAILS_ID = (SELECT MAX(SALARY_DETAILS_ID) FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID + ")";
                        //-----------------------------------------------
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                        //
                        continue;
                    }
                }
                #endregion

                #region SALARY DETAIL [SD-C6A]

                if (cmbFormNo.Text == T_FormNo.F24Q && cmbQuarter.Text == T_Qtr.Q4)
                {
                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 3) == "C6A")
                    {
                        //intCaratPosition = ListLines[i].IndexOf("^");

                        // 1 LINE NUMBER
                        // 2 RECORD TYPE
                        // 3 BATCH NUMBER
                        // 4 SALARY DETAIL RECORD NUMBER
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strSDC6ARecordNumber = ListLines[i].Substring(intCaratPosition + 1);
                        strSDC6ARecordNumber = ListLines[i].Substring(intCaratPosition + 1, strSDC6ARecordNumber.IndexOf("^"));

                        // 5 SALARY DETAIL - CHAPTER VI A RECORD NUMBER
                        // 6 CHAPTER VI A ID
                        for (int a = 1; a < 3; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strC6ASectionID = ListLines[i].Substring(intCaratPosition + 1);
                        strC6ASectionID = ListLines[i].Substring(intCaratPosition + 1, strC6ASectionID.IndexOf("^"));

                        // 7 TOTAL AMOUNT UNDER CHAPTER VI A
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strC6ATotalAmount = ListLines[i].Substring(intCaratPosition + 1);
                        strC6ATotalAmount = ListLines[i].Substring(intCaratPosition + 1, strC6ATotalAmount.IndexOf("^"));

                        // 8 RECORD HASH
                        string strC6AField = "";
                        //
                        if (strC6ASectionID == "80CCE")
                            strC6AField = "CVIA_SEC80CCE_TOTAL_DED_AMOUNT";
                        else
                            strC6AField = "CVIA_OTH_DED_TOTAL";
                        //
                        strSQL = "UPDATE TRN_SALARY_DETAILS SET" +
                            "            " + strC6AField + " = " + cmnService.J_ReturnDoubleValue(strC6ATotalAmount) + " " +
                            "     WHERE  SALARY_DETAILS_ID = (SELECT MAX(SALARY_DETAILS_ID) FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID + ")";
                        //-----------------------------------------------
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                        //
                        continue;
                    }
                }
                #endregion
                */
                #endregion
            }

            return true;
        }

        #endregion

        #endregion

    }
}

