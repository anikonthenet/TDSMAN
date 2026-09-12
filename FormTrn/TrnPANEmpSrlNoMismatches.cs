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
using System.Collections.Generic;

using System.Data;
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

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnPANEmpSrlNoMismatches : Form
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnPANEmpSrlNoMismatches()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            ////--
        }
        #endregion

        #region Objects & Variables declaration

        TracesConnect objTracesConnect = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;
        string strMultipleEmployeeCodeAgainstSamePanSQL = string.Empty;
        string strMultiplePanAgainstSameEmployeeCodeSQL = string.Empty;
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
        string strFAYear = "", strOrderBy = "", strQuery = "";
        bool blnRegular = false;
        //
        int j = 0;
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
        long lngBaseBasicInfoId = 0, lngBaseFAYearId = 0, lngBaseCompanyId = 0;
        string strBaseFromNo = "", strBaseQtr = "";
        long lngBasicInfoId1 = 0, lngBasicInfoId2 = 0, lngBasicInfoId3 = 0;
        //
        System.Data.OleDb.OleDbConnection con;
        DataSet myDataSet;
        OleDbDataAdapter myCommand;
        string strExcelFilePath = string.Empty;
        //--
        #endregion

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

        #region User Defined Events

        #region TrnDeleteReturn_Load
        private void TrnDeleteReturn_Load(object sender, EventArgs e)
        {
            try
            {
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //--
                ClearControls(); 
                //--
                //LoadControls();
                //--
                btnBack.Enabled = false;
                btnBack.BackColor = Color.LightGray;
                //--
                grpControlSummary.Visible = false;
                //pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                //--
                cmbBaseFinancialYear.Select();
                //
                this.Cursor = Cursors.Default;
                //-----------
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region cmbLoadGrid_SelectedIndexChanged
        private void cmbLoadGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBaseFinancialYear.SelectedIndex <= 0)
            {
                ClearControls();
                return;
            }
            if (cmbBaseCompany.SelectedIndex <= 0)
            {
                ClearControls();
                return;
            }
            if (cmbBaseQuarter.Text == "")
            {
                ClearControls();
                return;
            } 
            if (cmbBaseFormNo.Text == "")
            {
                ClearControls();
                return;
            }
            //-----------------------------------------------
            ClearControls();
            //-----------------------------------------------
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbBaseFinancialYear, cmbBaseFinancialYear.SelectedIndex)),
                                                        cmbBaseQuarter.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbBaseCompany, cmbBaseCompany.SelectedIndex)),
                                                        cmbBaseFormNo.Text);
            //
            if (cmbBaseFormNo.Text == T_FormNo.F24Q)
                strEmpDed = "Employee";
            else
                strEmpDed = "Deductee";
            //--
            intPANId = 1;
            intNameEntered = 2;
            intNameVerified = 3;
            intStatusId = 4;
            intVerifyId = 5;
            //
            blRegular = false;
            //
            if(lngBasicInfoID==0)
            {
                return;
            }
            //--
            //LoadDeducteeGrid(lngBasicInfoID);
            //--
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

        #region btnNext_Click
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (tbcPANEmpSrlNoMismatch.SelectedTab == tbpStartingPage)
            {
                #region tbpStartingPage
                tbcPANEmpSrlNoMismatch.SelectTab(tbpSelectBaseReturn);
                lblSteps.Text = "Step 2 of 4";
                rbnRegularReturn_CheckedChanged(sender, e);
                btnBack.Enabled = false;
                #endregion
            }
            else if (tbcPANEmpSrlNoMismatch.SelectedTab == tbpSelectBaseReturn)
            {
                #region tbpSelectBaseReturn
                //--
                if (rbnRegularReturn.Checked == true)
                {
                    strSQL = "SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                    if (cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) == 0)
                    {
                        cmnService.J_UserMessage("No Deductee records present !!!");
                        return;
                    }
                    else
                    {
                        lblBaseReturnDisplay.Text = "Financial Year : " + cmbBaseFinancialYear.Text + " / Form No. : " + cmbBaseFormNo.Text + " / Quarter : " + cmbBaseQuarter.Text + " / Company Name : " + cmbBaseCompany.Text;
                    }
                }
                else if (rbnCorrectionReturn.Checked == true)
                {

                }
                //-- CARRY OVER --//
                lngBaseBasicInfoId = lngBasicInfoID;
                lngBaseFAYearId = Convert.ToInt32(Support.GetItemData(cmbBaseFinancialYear, cmbBaseFinancialYear.SelectedIndex));
                lngBaseCompanyId = Convert.ToInt32(Support.GetItemData(cmbBaseCompany, cmbBaseCompany.SelectedIndex));
                strBaseFromNo = cmbBaseQuarter.Text;
                strBaseQtr = cmbBaseQuarter.Text;
                //--
                tbcPANEmpSrlNoMismatch.SelectTab(tbpComparingReturns);
                lblSteps.Text = "Step 3 of 4";
                btnBack.Enabled = true;
                btnBack.BackColor = Color.Lavender;
                //-- TYPE
                string[] strReturnType = { T_ReturnType.Regular, T_ReturnType.Correction };
                dmlService.J_PopulateComboBox(strReturnType, ref cmbReturnType1, 1);
                dmlService.J_PopulateComboBox(strReturnType, ref cmbReturnType2, 1);
                dmlService.J_PopulateComboBox(strReturnType, ref cmbReturnType3, 1);
                //--
                //LoadCompareDetails();
                //-- LOAD COMPARE DETAILS - Return Type, FA Year & Qtr
                if (cmbBaseQuarter.Text == T_Qtr.Q1)
                {
                    strSQL = @"SELECT DISTINCT MST_ASSESSMENT.ASST_ID,
                                      MST_ASSESSMENT.FA_YEAR
                               FROM   TRN_BASIC_INFO, MST_ASSESSMENT 
                               WHERE  TRN_BASIC_INFO.ASST_ID = MST_ASSESSMENT.ASST_ID
                                  AND FORM_NO        = '" + T_FormNo.F24Q +
                               "' AND COMPANY_ID     = " + lngBaseCompanyId +
                               "  AND BASIC_INFO_ID <> " + lngBaseBasicInfoId +
                               @" AND MST_ASSESSMENT.VISIBILITY_FLAG = 0
                                  ORDER BY MST_ASSESSMENT.ASST_ID DESC";
                    if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear1, J_ComboBoxSelectedIndex.YES) == false) return;
                }
                else if (cmbBaseQuarter.Text == T_Qtr.Q2)
                {

                }
                else if (cmbBaseQuarter.Text == T_Qtr.Q3)
                {

                }
                else if (cmbBaseQuarter.Text == T_Qtr.Q4)
                {

                }
                #endregion
            }
            else if (tbcPANEmpSrlNoMismatch.SelectedTab == tbpComparingReturns)
            {
                #region tbpComparingReturns
                tbcPANEmpSrlNoMismatch.SelectTab(tbpExport);
                btnNext.Text = "E&xport";
                lblSteps.Text = "Step 4 of 4";
                grpSelectFolder.Enabled = false;
                grpProgressBar.Enabled = false;
                rbnExcelOption.Checked = false;
                rbnCSVOption.Checked = false;
                //btnBack.Enabled = true;
                //btnBack.BackColor = Color.Lavender;
                lblBaseReturnDisplayExport.Text = lblBaseReturnDisplay.Text;
                lblCompanreReturnDisplayExport.Text = "";
                //
                if (cmbQuarter1.Text != "")
                {
                    lblCompanreReturnDisplayExport.Text = "1. Type : " + cmbReturnType1.Text + " ; Financial Year : " + cmbFinancialYear1.Text + " ; Quarter : " + cmbQuarter1.Text + " ; " + lblNoOfDeducteeRecords1.Text;
                }
                //
                if (cmbQuarter2.Text != "")
                {
                    lblCompanreReturnDisplayExport.Text = lblCompanreReturnDisplayExport.Text + "\n\n2. Type : " + cmbReturnType2.Text + " ; Financial Year : " + cmbFinancialYear2.Text + " ; Quarter : " + cmbQuarter2.Text + " ; " + lblNoOfDeducteeRecords2.Text;
                }
                //
                if (cmbQuarter3.Text != "")
                {
                    lblCompanreReturnDisplayExport.Text = lblCompanreReturnDisplayExport.Text + "\n\n3. Type : " + cmbReturnType3.Text + " ; Financial Year : " + cmbFinancialYear3.Text + " ; Quarter : " + cmbQuarter3.Text + " ; " + lblNoOfDeducteeRecords3.Text;
                }
                //
                if (lblCompanreReturnDisplayExport.Text.Trim() == "")
                    lblCompareWithNoSelection.Visible = true;
                else
                    lblCompareWithNoSelection.Visible = false;
                #endregion
            }
            else if (tbcPANEmpSrlNoMismatch.SelectedTab == tbpExport)
            {
                #region VALIDATION
                if(rbnCSVOption.Checked==false && rbnExcelOption.Checked==false)
                {
                    cmnService.J_UserMessage("Please select CSV or Excel option !!!");
                    rbnCSVOption.Select();
                    return;
                }
                strExcelFilePath = string.Empty;
                strExcelFilePath = txtExcelPath.Text + "\\" + txtDestinationFileName.Text.Trim();
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
                    if (string.IsNullOrEmpty(txtExcelPath.Text.Trim()))
                    {
                        cmnService.J_UserMessage("Please select specific folder for import");
                        btnSelectExcelPath.Select();
                        return;
                    }
                    //
                    //-- 2018/09/27
                    if (rbnCSVOption.Checked == false)// return true; //-- 2019/01/22
                    {
                        if (cmnService.J_Right(strExcelFilePath, 5).ToUpper() != ".XLSX")
                        {
                            strExcelFilePath = strExcelFilePath + ".XLSX";
                        }
                    }
                    if (string.IsNullOrEmpty(txtDestinationFileName.Text.Trim()))
                    {
                        cmnService.J_UserMessage("Please specific the File Name");
                        return;
                    }
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
                        btnSelectExcelPath.Select();
                        return;
                    }
                    //
                    if (string.IsNullOrEmpty(txtDestinationFileName.Text.Trim()))
                    {
                        cmnService.J_UserMessage("Please specific the File Name");
                        return;
                    }
                }
                //
                #endregion
                //--
                if (cmnService.J_UserMessage("Do you want to continue ??", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
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
                            //if (rbnRegularReturn.Checked == true)
                            //{
                            //}
                            #region VARIABLE DECLARATION
                            string strQtr = "", strSl = "",  strPAN = "", strEmpSrlNo = "", strEmpName = "", strChallanNo = "",  strDepositDate = "", strPaymentDate = "", strPaymentAmount = "", strTaxDepositedAmount = "";
                            string strBasicInfoIDs = "";
                            strBasicInfoIDs = "(" + lngBaseBasicInfoId ;
                            if (lngBasicInfoId1 > 0)
                                strBasicInfoIDs = strBasicInfoIDs + "," + lngBasicInfoId1.ToString();
                            if (lngBasicInfoId2 > 0)
                                strBasicInfoIDs = strBasicInfoIDs + "," + lngBasicInfoId2.ToString();
                            if (lngBasicInfoId3 > 0)
                                strBasicInfoIDs = strBasicInfoIDs + "," + lngBasicInfoId3.ToString();
                            strBasicInfoIDs = strBasicInfoIDs + ")";
                            #endregion
                            //--------------------------------------------------------------
                            #region Multiple Employee Code Against Same PAN SQL
                            prgBar.Value = prgBar.Value + 5;
                            //-----------------------------------------------------------------------------------------------------------
                            //
                            //
                            prgBar.Value = prgBar.Value + 5;
                            //-----------------------------------------------------------------------------------------------------------
                            //
                            strQtr                = "[Quarter]";
                            strSl                 = "[Srl No]";
                            strPAN                = "[PAN]";
                            strEmpSrlNo           = "[Employee Serial No/Code]";
                            strEmpName            = "[Employee Name]";
                            strChallanNo          = "[Challan No]";
                            strDepositDate        = "[Deposit Date]";
                            strPaymentDate        = "[Payment Date]";
                            strPaymentAmount      = "[Payment Amount]";
                            strTaxDepositedAmount = "[Tax Deposit Amount]";
                            //--
                            //-----------------------------------------------------------------------------------
                            //---MULTIPLE EMPLOYEE CODE AGAINST SAME PAN
                            //---------------------------------------------------------------------------------- -
                            //cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY)
                            strMultipleEmployeeCodeAgainstSamePanSQL = @"SELECT   TRN_BASIC_INFO.QTR               AS " + strQtr + @",
                                                                                  ROW_NUMBER() OVER(PARTITION BY PARTY_PAN ORDER BY PARTY_PAN, EMPLOYEE_SERIAL_NO, PAYMENT_DATE, DEDUCTEE_DETAIL_ID) AS " + strSl + @",
                                                                                  PARTY_PAN                         AS " + strPAN + @",
                                                                                  EMPLOYEE_SERIAL_NO                AS " + strEmpSrlNo + @",
                                                                                  PARTY_NAME                        AS " + strEmpName + @",
                                                                                  TRN_CHALLAN.SL_NO                 AS " + strChallanNo + @"," +
                                                                                  cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDepositDate + @"," +
                                                                                  cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE",J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + @"," +
                                                                                  cmnService.J_SQLDBFormat("PAYMENT_AMOUNT",J_SQLColFormat.ConvertToMoney) + "                    AS " + strPaymentAmount + @"," +
                                                                                  cmnService.J_SQLDBFormat("TAX_DEPOSITED_AMOUNT",J_SQLColFormat.ConvertToMoney) + "              AS " + strTaxDepositedAmount + @"
                                                                        FROM      TRN_DEDUCTEE_DETAILS, 
                                                                                  TRN_BASIC_INFO, 
                                                                                  TRN_CHALLAN
                                                                        WHERE     TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                                                                        AND       TRN_DEDUCTEE_DETAILS.CHALLAN_ID    = TRN_CHALLAN.CHALLAN_ID
                                                                        AND       TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID IN " + strBasicInfoIDs + @"
                                                                        AND       PARTY_PAN IN(SELECT PARTY_PAN
                                                                                                  FROM(SELECT PARTY_PAN,
                                                                                                              EMPLOYEE_SERIAL_NO
                                                                                                       FROM   TRN_DEDUCTEE_DETAILS
                                                                                                       WHERE  BASIC_INFO_ID IN " + strBasicInfoIDs + @"
                                                                                                       GROUP BY  PARTY_PAN,
                                                                                                                 EMPLOYEE_SERIAL_NO) AS SUMMARY
                                                                                                  GROUP BY PARTY_PAN
                                                                                                  HAVING COUNT(*) > 1)
                                                                        ORDER BY PARTY_PAN,
                                                                                 EMPLOYEE_SERIAL_NO,
                                                                                 PAYMENT_DATE,
                                                                                 DEDUCTEE_DETAIL_ID";
                            //-----------------------------------------------------------------------------------
                            //---MULTIPLE PAN AGAINST SAME EMPLOYEE CODE
                            //----------------------------------------------------------------------------------
                            strMultiplePanAgainstSameEmployeeCodeSQL = @"SELECT TRN_BASIC_INFO.QTR               AS " + strQtr + @",
                                                                               ROW_NUMBER() OVER (PARTITION BY EMPLOYEE_SERIAL_NO ORDER BY EMPLOYEE_SERIAL_NO, PARTY_PAN, PAYMENT_DATE, DEDUCTEE_DETAIL_ID)  AS " + strSl + @",
                                                                               EMPLOYEE_SERIAL_NO                AS " + strEmpSrlNo + @",
                                                                               PARTY_PAN                         AS " + strPAN + @",
                                                                               PARTY_NAME                        AS " + strEmpName + @",
                                                                               TRN_CHALLAN.SL_NO                 AS " + strChallanNo + @"," +
                                                                               cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDepositDate + @"," +
                                                                               cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + @"," +
                                                                               cmnService.J_SQLDBFormat("PAYMENT_AMOUNT",J_SQLColFormat.ConvertToMoney) + "                    AS " + strPaymentAmount + @"," +
                                                                               cmnService.J_SQLDBFormat("TAX_DEPOSITED_AMOUNT", J_SQLColFormat.ConvertToMoney) + "              AS " + strTaxDepositedAmount + @"
                                                                        FROM   TRN_DEDUCTEE_DETAILS, 
                                                                               TRN_BASIC_INFO, 
                                                                               TRN_CHALLAN
                                                                        WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                                                                        AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID    = TRN_CHALLAN.CHALLAN_ID
                                                                        AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID IN " + strBasicInfoIDs + @"
                                                                        AND    EMPLOYEE_SERIAL_NO IN (SELECT EMPLOYEE_SERIAL_NO
                                                                                                      FROM   (SELECT EMPLOYEE_SERIAL_NO,
                                                                                                                     PARTY_PAN
                                                                                                              FROM   TRN_DEDUCTEE_DETAILS
                                                                                                              WHERE  BASIC_INFO_ID  IN " + strBasicInfoIDs + @"
                                                                                                              GROUP BY  EMPLOYEE_SERIAL_NO,
							    			                                                                         PARTY_PAN) AS SUMMARY
                                                                                                      GROUP BY EMPLOYEE_SERIAL_NO
                                                                                                      HAVING COUNT(*) > 1)
                                                                        ORDER BY EMPLOYEE_SERIAL_NO,
                                                                                 PARTY_PAN,
                                                                                 PAYMENT_DATE,
                                                                                 DEDUCTEE_DETAIL_ID";
                            //}
                            #endregion
                            //
                            prgBar.Value = prgBar.Value + 5;  
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
                            //
                            prgBar.Value = prgBar.Value + 5; 
                            //--
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Multi Emp Code-PAN") == false) return;
                            if (ExportToExcelFromSQL(strMultipleEmployeeCodeAgainstSamePanSQL, "Multi Emp Code-PAN") == false)
                            {
                                cmnService.J_UserMessage("Export data failed ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                prgBar.Value = 0;  //-- 01/01/2018  --
                                return;
                            }
                            //--
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Multi PAN-Emp Code") == false) return;
                            if (ExportToExcelFromSQL(strMultiplePanAgainstSameEmployeeCodeSQL, "Multi PAN-Emp Code") == false)
                            {
                                cmnService.J_UserMessage("Export data failed ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                prgBar.Value = 0;  //-- 01/01/2018  --
                                return;
                            }
                            //--
                            prgBar.Value = prgBar.Value + 5;
                            //---------------------------
                            this.Cursor = Cursors.Default;
                            //--
                            cmnService.J_UserMessage("Data Exported Successfully..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //
                            //-- 01/01/2017 --
                            ClearControls();
                            //--
                            //LoadControls();
                            //
                            //tbcExportData.Enabled = true;
                            grpButtons.Enabled = true;
                            //
                            btnNext.Enabled = false;
                            btnNext.BackColor = System.Drawing.Color.LightGray;
                            //--
                            grpControlSummary.Visible = false;
                            pnlLine1.Visible = false;
                            prgBar.Value = 0;
                            //--
                            //cmbFinancialYear.Select();
                            //
                            this.Cursor = Cursors.Default;
                            //-----------------
                        }
                    }
                    //--
                    catch (Exception err_handler)
                    {
                        cmnService.J_UserMessage(err_handler.Message);
                    }
                }
            }
        }
        #endregion

        #region btnBack_Click
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (tbcPANEmpSrlNoMismatch.SelectedTab == tbpComparingReturns)
            {
                btnNext.Text = "&Next";
                tbcPANEmpSrlNoMismatch.SelectTab(tbpSelectBaseReturn);
                lblSteps.Text = "Step 2 of 4";
                btnBack.Enabled = false;
                btnBack.BackColor = Color.LightGray;
                btnNext.Enabled = true;
                btnNext.BackColor = Color.Lavender;
            }                   
            else if(tbcPANEmpSrlNoMismatch.SelectedTab == tbpExport)
            {
                btnNext.Text = "&Next";
                tbcPANEmpSrlNoMismatch.SelectTab(tbpComparingReturns);
                lblSteps.Text = "Step 3 of 4";
                //btnBack.Enabled = false;
                //btnBack.BackColor = Color.LightGray;
                btnNext.Enabled = true;
                btnNext.BackColor = Color.Lavender;
            }

        }
        #endregion

        #region cmbMain_SelectedIndexChanged
        private void cmbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnSelectComboExit == true)
                return;
            //
            ClearControls();
            //
            lngBasicInfoID = 0;
            //
            if (cmbBaseFinancialYear.SelectedIndex <= 0)
            {
                lngBasicInfoID = 0;
                grpControlSummary.Visible = false;
                //pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false; 
                //lblTotalRecords.Visible = false;
                btnNext.Enabled = false; 
                btnNext.BackColor = Color.LightGray;                    
                return;
            }
            else if (cmbBaseFinancialYear.SelectedIndex > 0)
            {
                lngBasicInfoID = 0;
                //
                long lngTotalRecords = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS, TRN_BASIC_INFO WHERE TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID AND TRN_BASIC_INFO.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbBaseFinancialYear, cmbBaseFinancialYear.SelectedIndex)), J_QueryType.DirectQuery);
                //
                //if (lngTotalRecords > 0)
                //{
                    //lblTotalRecords.Visible = true;
                    //lblTotalRecords.Text = "Total Records for FA year [" + cmbFinancialYear.Text + "] : " + Convert.ToString(lngTotalRecords);
                //}
                //else
                //    lblTotalRecords.Visible = false;
            }
            //--
            if (cmbBaseCompany.SelectedIndex <= 0)
            {
                lngBasicInfoID = 0;
                grpControlSummary.Visible = false;
                //pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.Enabled = false;
                btnNext.BackColor = Color.LightGray; 
                return;
            }
            if (cmbBaseFormNo.Text == "")
            {
                lngBasicInfoID = 0;
                grpControlSummary.Visible = false;
                //pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.Enabled = false;
                btnNext.BackColor = Color.LightGray; 
                return;
            }
            if (cmbBaseQuarter.Text == "")
            {
                lngBasicInfoID = 0;
                grpControlSummary.Visible = false;
                //pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.Enabled = false;
                btnNext.BackColor = Color.LightGray; 
                return;
            }
            //--
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbBaseFinancialYear, cmbBaseFinancialYear.SelectedIndex)),
                                                        cmbBaseQuarter.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbBaseCompany, cmbBaseCompany.SelectedIndex)),
                                                        cmbBaseFormNo.Text);
            //if (lngBasicInfoID == 0)
            //{
            //    lblHideTabs.Text = "No records found for the selected return";
            //    return;
            //}
            
            //--
            ControlSummaryBasicInfo(lngBasicInfoID);
            //ReturnFilingStatus(lngBasicInfoID);
            //-- 
        }
        #endregion
        
        #region cmbReturnType1_SelectedIndexChanged
        private void cmbReturnType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReturnType1.Text == T_ReturnType.Regular)
                blnRegular = true;
            else if (cmbReturnType1.Text == T_ReturnType.Correction)
                blnRegular = false;
            //-- LOAD COMPARE DETAILS - Return Type, FA Year & Qtr
            if (blnRegular == true) //-- WHEN 'REGULAR' SELECTED
            {
                if (cmbBaseQuarter.Text == T_Qtr.Q1)
                {
                    strSQL = @"SELECT DISTINCT MST_ASSESSMENT.ASST_ID,
                                      MST_ASSESSMENT.FA_YEAR
                               FROM   TRN_BASIC_INFO, MST_ASSESSMENT 
                               WHERE  TRN_BASIC_INFO.ASST_ID = MST_ASSESSMENT.ASST_ID
                                  AND FORM_NO        = '" + T_FormNo.F24Q +
                               "' AND COMPANY_ID     = " + lngBaseCompanyId +
                               "  AND BASIC_INFO_ID NOT IN (" + lngBaseBasicInfoId + "," + lngBasicInfoId2 + "," + lngBasicInfoId3 + ") " +
                               @" AND MST_ASSESSMENT.VISIBILITY_FLAG = 0
                                  ORDER BY MST_ASSESSMENT.ASST_ID DESC";
                    if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear1, J_ComboBoxSelectedIndex.YES) == false) return;
                }
                else if (cmbBaseQuarter.Text == T_Qtr.Q2)
                {

                }
                else if (cmbBaseQuarter.Text == T_Qtr.Q3)
                {

                }
                else if (cmbBaseQuarter.Text == T_Qtr.Q4)
                {

                }
            }
            else if (blnRegular == false) //-- WHEN 'CORRECTION' SELECTED
            {

            }

        }
        #endregion
        
        #region cmbReturnType2_SelectedIndexChanged
        private void cmbReturnType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReturnType2.Text == T_ReturnType.Regular)
                blnRegular = true;
            else if (cmbReturnType2.Text == T_ReturnType.Correction)
                blnRegular = false;
            //-- LOAD COMPARE DETAILS - Return Type, FA Year & Qtr
            if (blnRegular == true) //-- WHEN 'REGULAR' SELECTED
            {
                //if (cmbBaseQuarter.Text == T_Qtr.Q1)
                //{
                strSQL = @"SELECT DISTINCT MST_ASSESSMENT.ASST_ID,
                                      MST_ASSESSMENT.FA_YEAR
                               FROM   TRN_BASIC_INFO, MST_ASSESSMENT 
                               WHERE  TRN_BASIC_INFO.ASST_ID = MST_ASSESSMENT.ASST_ID
                                  AND FORM_NO        = '" + T_FormNo.F24Q +
                           "' AND COMPANY_ID     = " + lngBaseCompanyId +
                           "  AND BASIC_INFO_ID NOT IN (" + lngBaseBasicInfoId + "," + lngBasicInfoId1 + "," + lngBasicInfoId3 + ") " +
                           @" AND MST_ASSESSMENT.VISIBILITY_FLAG = 0
                                  ORDER BY MST_ASSESSMENT.ASST_ID DESC";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear2, J_ComboBoxSelectedIndex.YES) == false) return;
                //}
                //else if (cmbBaseQuarter.Text == T_Qtr.Q2)
                //{

                //}
                //else if (cmbBaseQuarter.Text == T_Qtr.Q3)
                //{

                //}
                //else if (cmbBaseQuarter.Text == T_Qtr.Q4)
                //{

                //}
            }
            else if (blnRegular == false) //-- WHEN 'CORRECTION' SELECTED
            {

            }
        }
        #endregion

        #region cmbReturnType3_SelectedIndexChanged
        private void cmbReturnType3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReturnType3.Text == T_ReturnType.Regular)
                blnRegular = true;
            else if (cmbReturnType3.Text == T_ReturnType.Correction)
                blnRegular = false;
            //-- LOAD COMPARE DETAILS - Return Type, FA Year & Qtr
            if (blnRegular == true) //-- WHEN 'REGULAR' SELECTED
            {
                //if (cmbBaseQuarter.Text == T_Qtr.Q1)
                //{
                strSQL = @"SELECT DISTINCT MST_ASSESSMENT.ASST_ID,
                                      MST_ASSESSMENT.FA_YEAR
                               FROM   TRN_BASIC_INFO, MST_ASSESSMENT 
                               WHERE  TRN_BASIC_INFO.ASST_ID = MST_ASSESSMENT.ASST_ID
                                  AND FORM_NO        = '" + T_FormNo.F24Q +
                           "' AND COMPANY_ID     = " + lngBaseCompanyId +
                           "  AND BASIC_INFO_ID NOT IN (" + lngBaseBasicInfoId + "," + lngBasicInfoId2 + "," + lngBasicInfoId1 + ") " +
                           @" AND MST_ASSESSMENT.VISIBILITY_FLAG = 0
                                  ORDER BY MST_ASSESSMENT.ASST_ID DESC";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear3, J_ComboBoxSelectedIndex.YES) == false) return;
                //}
                //else if (cmbBaseQuarter.Text == T_Qtr.Q2)
                //{

                //}
                //else if (cmbBaseQuarter.Text == T_Qtr.Q3)
                //{

                //}
                //else if (cmbBaseQuarter.Text == T_Qtr.Q4)
                //{

                //}
            }
            else if (blnRegular == false) //-- WHEN 'CORRECTION' SELECTED
            {

            }
        }

        #endregion


        #region cmbFinancialYear1_SelectedIndexChanged
        private void cmbFinancialYear1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lngBasicInfoId1 = 0;
            if (cmbFinancialYear1.SelectedIndex <= 0)
            {
                cmbQuarter1.Items.Clear();
            }
            //--
            if (blnRegular == true) //-- REGULAR
            {
                strSQL = @"SELECT BASIC_INFO_ID, QTR 
                           FROM   TRN_BASIC_INFO 
                           WHERE  FORM_NO = '" + T_FormNo.F24Q +
                          "' AND COMPANY_ID = " + lngBaseCompanyId +
                          "  AND ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear1, cmbFinancialYear1.SelectedIndex)) +
                          "  AND BASIC_INFO_ID NOT IN (" + lngBaseBasicInfoId + "," + lngBasicInfoId2 + "," + lngBasicInfoId3 + ") " +
                          "  ORDER BY QTR";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbQuarter1, J_ComboBoxSelectedIndex.YES) == false) return;
            }
            else if (blnRegular == false) //-- CORRECTION
            {

            }
        }
        #endregion

        #region cmbQuarter1_SelectedIndexChanged
        private void cmbQuarter1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNoOfDeducteeRecords1.Text = "";
            if (cmbFinancialYear1.SelectedIndex <= 0)
            {
                lngBasicInfoId1 = 0;
            }
            //
            if (cmbQuarter1.SelectedIndex <= 0)
            {
                lngBasicInfoId1 = 0;
            }
            //--
            if (cmbReturnType1.Text == T_ReturnType.Regular)
            {
                lngBasicInfoId1 = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear1, cmbFinancialYear1.SelectedIndex)),
                                                            cmbQuarter1.Text,
                                                            Convert.ToInt32(Support.GetItemData(cmbBaseCompany, cmbBaseCompany.SelectedIndex)),
                                                            cmbBaseFormNo.Text);
                if (lngBasicInfoId1 > 0)
                {
                    if (lngBasicInfoId1 == lngBasicInfoId2)
                    {
                        cmnService.J_UserMessage("Same record has been selected for Option 2\nPlease deselect this record!!");
                        cmbQuarter1.SelectedIndex = 0;
                        return;
                    }
                    else if (lngBasicInfoId1 == lngBasicInfoId3)
                    {
                        cmnService.J_UserMessage("Same record has been selected for Option 3\nPlease deselect this record!!");
                        cmbQuarter1.SelectedIndex = 0;
                        return;
                    }
                    else
                        lblNoOfDeducteeRecords1.Text = "There are " + Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoId1)) + " employee record(s).";
                }
                else
                    lblNoOfDeducteeRecords1.Text = "";
            }
        }
        #endregion

        #region cmbQuarter2_SelectedIndexChanged
        private void cmbQuarter2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNoOfDeducteeRecords2.Text = "";
            if (cmbFinancialYear2.SelectedIndex <= 0)
            {
                lngBasicInfoId2 = 0;
            }
            //
            if (cmbQuarter2.SelectedIndex <= 0)
            {
                lngBasicInfoId2 = 0;
            }
            //
            if (cmbReturnType2.Text == T_ReturnType.Regular)
            {
                lngBasicInfoId2 = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear2, cmbFinancialYear2.SelectedIndex)),
                                                        cmbQuarter2.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbBaseCompany, cmbBaseCompany.SelectedIndex)),
                                                        cmbBaseFormNo.Text);
                if (lngBasicInfoId2 > 0)
                {
                    if (lngBasicInfoId2 == lngBasicInfoId1)
                    {
                        cmnService.J_UserMessage("Same record has been selected for Option 1\nPlease deselect this record!!");
                        return;
                    }
                    else if (lngBasicInfoId2 == lngBasicInfoId3)
                    {
                        cmnService.J_UserMessage("Same record has been selected for Option 3\nPlease deselect this record!!");
                        return;
                    }
                    else
                        lblNoOfDeducteeRecords2.Text = "There are " + Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoId2)) + " employee record(s).";
                }
                else
                    lblNoOfDeducteeRecords2.Text = "";
            }
        }
        #endregion

        #region cmbQuarter3_SelectedIndexChanged
        private void cmbQuarter3_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNoOfDeducteeRecords3.Text = "";
            if (cmbFinancialYear3.SelectedIndex <= 0)
            {
                lngBasicInfoId3 = 0;
            }
            //
            if (cmbQuarter3.SelectedIndex <= 0)
            {
                lngBasicInfoId3 = 0;
            }
            //
            if (cmbReturnType3.Text == T_ReturnType.Regular)
            {
                lngBasicInfoId3 = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear3, cmbFinancialYear3.SelectedIndex)),
                                                        cmbQuarter3.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbBaseCompany, cmbBaseCompany.SelectedIndex)),
                                                        cmbBaseFormNo.Text);
                if (lngBasicInfoId3 > 0)
                {
                    if (lngBasicInfoId3 == lngBasicInfoId1)
                    {
                        cmnService.J_UserMessage("Same record has been selected for Option 1\nPlease deselect this record!!");
                        return;
                    }
                    else if (lngBasicInfoId3 == lngBasicInfoId2)
                    {
                        cmnService.J_UserMessage("Same record has been selected for Option 2\nPlease deselect this record!!");
                        return;
                    }
                    else
                        lblNoOfDeducteeRecords3.Text = "There are " + Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoId3)) + " employee record(s).";
                }
                else
                    lblNoOfDeducteeRecords3.Text = "";
            }
        }
        #endregion

        #region cmbFinancialYear2_SelectedIndexChanged
        private void cmbFinancialYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lngBasicInfoId2 = 0;
            if (cmbFinancialYear2.SelectedIndex <= 0)
            {
                cmbQuarter2.Items.Clear();
            }
            //--
            if (blnRegular == true) //-- REGULAR
            {
                strSQL = @"SELECT BASIC_INFO_ID, QTR 
                           FROM   TRN_BASIC_INFO 
                           WHERE  FORM_NO = '" + T_FormNo.F24Q +
                          "' AND COMPANY_ID = " + lngBaseCompanyId +
                          "  AND ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear2, cmbFinancialYear2.SelectedIndex)) +
                          "  AND BASIC_INFO_ID NOT IN (" + lngBaseBasicInfoId + "," + lngBasicInfoId1 + "," + lngBasicInfoId3 + ") " +
                          "  ORDER BY QTR";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbQuarter2, J_ComboBoxSelectedIndex.YES) == false) return;
            }
            else if (blnRegular == false) //-- CORRECTION
            {

            }
        }
        #endregion

        #region cmbFinancialYear3_SelectedIndexChanged
        private void cmbFinancialYear3_SelectedIndexChanged(object sender, EventArgs e)
        {
            lngBasicInfoId3 = 0;
            if (cmbFinancialYear3.SelectedIndex <= 0)
            {
                cmbQuarter3.Items.Clear();
            }
            //--
            if (blnRegular == true) //-- REGULAR
            {
                strSQL = @"SELECT BASIC_INFO_ID, QTR 
                           FROM   TRN_BASIC_INFO 
                           WHERE  FORM_NO = '" + T_FormNo.F24Q +
                          "' AND COMPANY_ID = " + lngBaseCompanyId +
                          "  AND ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear3, cmbFinancialYear3.SelectedIndex)) +
                          "  AND BASIC_INFO_ID NOT IN (" + lngBaseBasicInfoId + "," + lngBasicInfoId1 + "," + lngBasicInfoId2 + ") " +
                          "  ORDER BY QTR";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbQuarter3, J_ComboBoxSelectedIndex.YES) == false) return;
            }
            else if (blnRegular == false) //-- CORRECTION
            {

            }
        }
        #endregion

        #region rbnRegularReturn_CheckedChanged
        private void rbnRegularReturn_CheckedChanged(object sender, EventArgs e)
        {
            lngBasicInfoID = 0;
            if (rbnRegularReturn.Checked == true)
            {
                LoadControlsRegular();
            }
            else if (rbnCorrectionReturn.Checked == true)
            {
                LoadControlCorrection();
            }
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

        #endregion

        #region User Define Functions

        #region ClearControls
        private void ClearControls()
        {            
            //grpButtons.Enabled = true;
            //
            txtTotalChallanRecords.Text = "";
            txtTotalDeducteeRecords.Text = "";
            txtTotalChallanAmount.Text = "";
            txtTotalDeducteeTDS.Text = "";
            txtAmountPaid.Text = "";
            //
        }
        #endregion

        #region LoadControlsRegular
        private void LoadControlsRegular()
        {
            //
            blnSelectComboExit = true;
            //-----------
            //-- FINANCIAL YEAR
            //-----------
            strSQL = @" SELECT DISTINCT MST_ASSESSMENT.ASST_ID,
                               MST_ASSESSMENT.FA_YEAR
                        FROM   MST_ASSESSMENT,
	                           TRN_BASIC_INFO
                        WHERE  MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID
                        AND    VISIBILITY_FLAG = 0
                        ORDER BY MST_ASSESSMENT.ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBaseFinancialYear, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //-- QUARTER
            //-----------
            strSQL = @" SELECT DISTINCT 1,  QTR FROM TRN_BASIC_INFO";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBaseQuarter, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //-- FORM NO
            //-----------
            strSQL = @" SELECT DISTINCT 1,  FORM_NO  FROM TRN_BASIC_INFO WHERE FORM_NO = '24Q'";
            dmlService.J_PopulateComboBox(strSQL, ref cmbBaseFormNo,1);
            cmbBaseFormNo.Enabled = false;
            //-----------
            //-- COMPANY
            //-----------
            strSQL = @"SELECT DISTINCT MST_COMPANY.COMPANY_ID, 
                               MST_COMPANY.COMPANY_NAME + ' [' + MST_COMPANY.TAN_NO + ']'
                        FROM   MST_COMPANY,
                               TRN_BASIC_INFO
                        WHERE  MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID      
                        AND    MST_COMPANY.INACTIVE_FLAG = 0"; 
                        //ORDER BY MST_COMPANY.COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBaseCompany, J_ComboBoxSelectedIndex.YES) == false) return;
            //
            blnSelectComboExit = false;
            //-----------
        }
        #endregion

        #region LoadControlCorrection
        private void LoadControlCorrection()
        {
            //
            blnSelectComboExit = true;
            //-----------
            //-- FINANCIAL YEAR
            //-----------
            strSQL = @" SELECT DISTINCT MST_ASSESSMENT.ASST_ID,
                               MST_ASSESSMENT.FA_YEAR
                        FROM   MST_ASSESSMENT,
	                           COR_HDR_BATCH
                        WHERE  MST_ASSESSMENT.ASST_ID = COR_HDR_BATCH.ASST_ID
                        ORDER BY MST_ASSESSMENT.ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBaseFinancialYear, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //-- QUARTER
            //-----------
            strSQL = @" SELECT DISTINCT 1,  QTR FROM COR_HDR_BATCH";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBaseQuarter, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //-- FORM NO
            //-----------
            strSQL = @" SELECT DISTINCT 1,  FORM_NO  FROM COR_HDR_BATCH";
            dmlService.J_PopulateComboBox(strSQL, ref cmbBaseFormNo, J_ComboBoxSelectedIndex.YES);
            //-----------
            //-- COMPANY
            //-----------
            strSQL = @"SELECT DISTINCT 1
                               COR_TRN_COMPANY.COMPANY_NAME + ' [' + COR_TRN_COMPANY.TAN_NO + ']'
                        FROM   COR_TRN_COMPANY,
                               COR_HDR_BATCH
                        WHERE  COR_TRN_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID";
            //ORDER BY MST_COMPANY.COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBaseCompany, J_ComboBoxSelectedIndex.YES) == false) return;
            //
            blnSelectComboExit = false;
            //-----------
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            //if (cmbFinancialYear.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Financial Year - Cannot be blank");
            //    cmbFinancialYear.Select();
            //    return false;
            //}
            //if (cmbFormNo.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Form No. - Cannot be blank");
            //    cmbFormNo.Select();
            //    return false;
            //}
            //if (cmbQuarter.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Quarter - Cannot be blank");
            //    cmbQuarter.Select();
            //    return false;
            //}
            //if (cmbCompany.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Company - Cannot be blank");
            //    cmbCompany.Select();
            //    return false;
            //}
            //if (lngBasicInfoID == 0)
            //{
            //    cmnService.J_UserMessage("No deductee records found");
            //    cmbCompany.Select();
            //    return false;
            //}
            ////
            //if (dgvDeductees.RowCount <= 0)
            //{
            //    cmnService.J_UserMessage("No deductee records found");
            //    cmbCompany.Select();
            //    return false;
            //}
            //
            //if (TdsMan.T_CheckInternetConnectivty() == false)
            //{
            //    cmnService.J_UserMessage("Internet Connectivity not found");
            //    cmbCompany.Select();
            //    return false;
            //}
            //--
            return true;
        }

        #endregion

        #region ControlSummaryBasicInfo
        private void ControlSummaryBasicInfo(long BasicInfoID)
        {
            if (BasicInfoID == 0)
            {
                txtTotalChallanRecords.Text = "";
                txtTotalDeducteeRecords.Text = "";
                txtTotalChallanAmount.Text = "";
                txtTotalDeducteeTDS.Text = "";
                txtAmountPaid.Text = "";
                grpControlSummary.Visible = false;
                //pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                btnNext.Enabled = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.BackColor = Color.LightGray; 
                return;
            }
            grpControlSummary.Visible = true;
            //pnlLine1.Visible = true;
            pnlLine2.Visible = true;
            //lblTotalRecordsReturn.Visible = true;
            btnNext.Enabled = true;
            btnNext.BackColor = Color.Lavender; 
            txtTotalChallanRecords.Text = "0";
            txtTotalDeducteeRecords.Text = "0";
            txtTotalChallanAmount.Text = "0.00";
            txtTotalDeducteeTDS.Text = "0.00";
            txtAmountPaid.Text = "0.00";

            // CONTROL SUMMARY VALUES
            txtTotalChallanRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(CHALLAN_ID) AS COUNT_CHALLAN_ID FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID));
            txtTotalDeducteeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID));
            txtTotalChallanAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID))));
            txtTotalDeducteeTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
            txtAmountPaid.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
            //
            long lngTotalRecordsReturn = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID, J_QueryType.DirectQuery);
            lblTotalRecordsReturn.Text = "Total Records in this return : " + Convert.ToString(lngTotalRecordsReturn);
            //
            if (cmbBaseFormNo.Text == T_FormNo.F24Q && cmbBaseQuarter.Text == T_Qtr.Q4)
            {
                lblNetTaxableIncome.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID)) == "" ? "0.00" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID))));
                lblNetTaxableIncome.Visible = true;
                lblNetTaxableIncomeCaption.Visible = true;
            }
            else
            {
                lblNetTaxableIncome.Visible = false;
                lblNetTaxableIncomeCaption.Visible = false;
            }
            //if (Convert.ToDouble(txtTotalChallanRecords.Text) > 0 && txtTotalChallanRecords.Visible == true)
            //{
            //    BtnCancel.Enabled = true;
            //    BtnCancel.BackColor = Color.Lavender;
            //}
            //else
            //{
            //    BtnCancel.Enabled = false;
            //    BtnCancel.BackColor = Color.LightGray;
            //}
        }

        #region rbnCSVOption_CheckedChanged
        private void rbnCSVOption_CheckedChanged(object sender, EventArgs e)
        {
            if(rbnCSVOption.Checked==true || rbnExcelOption.Checked==true)
            {
                grpSelectFolder.Enabled = true;
                grpProgressBar.Enabled = true;
            }
            else
            {
                grpSelectFolder.Enabled = false;
                grpProgressBar.Enabled = false;
            }
            //--
            if (rbnRegularReturn.Checked == true)
            {
                if (rbnCSVOption.Checked == true)
                {
                    lblFileFolderName.Text = "Folder Name";
                    txtDestinationFileName.Text = "CSV_" + cmbBaseFormNo.Text.Trim() + "_" + cmbBaseQuarter.Text.Trim() + "_" + cmbBaseFinancialYear.Text.Trim().Substring(2).Replace("-", "") + "_" + cmnService.J_Right(cmbBaseCompany.Text,11).Replace("]","");  
                    //btnExportToExcel.Text = "&Export to CSV";
                }
                else if (rbnExcelOption.Checked == true)
                {
                    lblFileFolderName.Text = "File Name";
                    txtDestinationFileName.Text = "XLS_" + cmbBaseFormNo.Text.Trim() + "_" + cmbBaseQuarter.Text.Trim() + "_" + cmbBaseFinancialYear.Text.Trim().Substring(2).Replace("-", "") + "_" + cmnService.J_Right(cmbBaseCompany.Text, 11).Replace("]", "") + ".XLSX";  
                    //btnExportToExcel.Text = "&Export to Excel";
                }
            }
            else if (rbnCorrectionReturn.Checked == true)
            {
                //if (rbnCSVOption.Checked == true)
                //{
                //    lblFileFolderName.Text = "Folder Name";
                //    txtDestinationFileName.Text = "CSV_CORR_EXPORT_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[2].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[3].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[1].Value.ToString().Substring(2).Replace("-", "") + "_" + tan;  //-- 22/01/2019 --
                //    btnExportToExcel.Text = "&Export to CSV";
                //}
                //else if (rbnExcelOption.Checked == true)
                //{
                //    lblFileFolderName.Text = "File Name";
                //    txtDestinationFileName.Text = "XLS_CORR_EXPORT_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[2].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[3].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[1].Value.ToString().Substring(2).Replace("-", "") + "_" + tan + ".XLSX";  //-- 21/08/2018 --
                //    btnExportToExcel.Text = "&Export to Excel";
                //}
            }
            //--            
        }
        #endregion



        #endregion

        //-- Added By Abhishek Dey On 22/05/2018 --
        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnDeleteReturn");
            Tan.ShowDialog();
            //--------------
            cmbBaseCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion
        //-----------------------------------------

        #region LoadCompareDetails
        private void LoadCompareDetails(string ReturnType, string FinancialYear, string Qtr)
        {
            
        }
        #endregion


        #region CREATE EXCEL FILE
        // SOURCE PATH : http://csharp.net-informations.com/excel/csharp-create-excel.htm
        private bool CREATE_EXCEL_FILE(string ExcelFilePath)
        {
            try
            {
                //MessageBox.Show("5");
                //--
                if (rbnCSVOption.Checked == true)
                {
                    if (Directory.Exists(ExcelFilePath) == false)
                        Directory.CreateDirectory(ExcelFilePath);
                    return true; //-- 2019/01/22
                }
                //MessageBox.Show("5.0.1.1");
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                //Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                //--
                //MessageBox.Show("5.0.1.2");
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
                //MessageBox.Show("5.0.1.3");
                xlWorkBook.Close(true, misValue, misValue);
                //MessageBox.Show("5.0.1.4");
                xlApp.Quit();
                //MessageBox.Show("5.0.1.5");

                //ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                //MessageBox.Show("5.0.1.6");
                ReleaseObject(xlApp);
                //MessageBox.Show("5.0.1.7");
                //--
                return true;
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
                //cmnService.J_UserMessage(ERR.Message);
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

        #region ExportToExcelFromSQL
        private bool ExportToExcelFromSQL(string strSQL, string SheetName)
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
                    ExportToCSV(strSQL, Path.Combine(Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text), SheetName + ".csv"));
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
                                && (cmnService.J_Mid(dr[dc.ColumnName].ToString(), 2, 1) == "/" || cmnService.J_Mid(dr[dc.ColumnName].ToString(), 2, 1) == "-")
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

        #endregion

    }

}