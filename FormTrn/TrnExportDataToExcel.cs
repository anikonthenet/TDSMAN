
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

namespace TDSMAN.FormTrn
{
    public partial class TrnExportDataToExcel : Form
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnExportDataToExcel()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
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
        //--
        double dblMaxExcelXLSXMaxRows = 1048575;
        //--
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

        #region TrnExportData_Load
        private void TrnExportData_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            try
            {
                ClearControls(); 
                //--
                LoadControls();
                //--
                //btnBack.Enabled = false;
                //btnBack.BackColor = Color.LightGray;
                //--
                grpControlSummary.Visible = false;
                pnlLine1.Visible = false;
                //pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                //
                //-- 01/01/2017 --
                btnNext.Enabled = false;
                btnNext.BackColor = System.Drawing.Color.LightGray;
                //--
                cmbFinancialYear.Select();
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

        #region rbnRegularCorrectionReturn_CheckedChanged
        private void rbnRegularCorrectionReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnRegularReturn.Checked == true)
            {
                grpRegularReturn.Visible = true;
                grpCorrectionReturn.Visible = false;
                //--
                chkSalaryDetails.Visible = false;
                //--
                ClearControls();
                //--
            }
            else if (rbnCorrectionReturn.Checked == true)
            {
                grpRegularReturn.Visible = false;
                grpCorrectionReturn.Visible = true;
                //grpCorrectionReturn.Location = new System.Drawing.Point(2, 113);
                //grpCorrectionReturn.Width = 696;
                //grpCorrectionReturn.Height = 121;
                //
                this.Cursor = Cursors.WaitCursor;
                //
                #region CLEAR CONTROLS
                //////--
                LoadCorrectionBatch();
                //////
                //////LoadDeducteeGridCorr(lngBasicInfoID);
                dgcViewBatch_Click(sender, e);
                //////
                #endregion
                //--
                this.Cursor = Cursors.Default;
            }
            //
            //LoadDeducteeGrid();
        }
        #endregion

        #region cmbLoadGrid_SelectedIndexChanged
        private void cmbLoadGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                ClearControls();
                return;
            }
            if (cmbCompany.SelectedIndex <= 0)
            {
                ClearControls();
                return;
            }
            if (cmbQuarter.Text == "")
            {
                ClearControls();
                return;
            }
            if (cmbFormNo.Text == "")
            {
                ClearControls();
                return;
            }
            //-----------------------------------------------
            ClearControls();
            //
            string strFormNo = cmbFormNo.Text.Substring( cmbFormNo.Text.IndexOf('(') + 1, cmbFormNo.Text.IndexOf(')') - cmbFormNo.Text.IndexOf('(') - 1);
            //-----------------------------------------------
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        cmbQuarter.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        strFormNo);
            //
            if (strFormNo == T_FormNo.F24Q)
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
            if (lngBasicInfoID == 0)
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

            if (lngBasicInfoID == 0)
            {
                btnNext.BackColor = System.Drawing.Color.LightGray;
                btnNext.Enabled = false;
                //
                cmnService.J_UserMessage("No Record found !!!");
                cmbFinancialYear.Select();
                return;
            }
            else
            {
                //-- Added By Abhishek Dey On 02/10/2019 --
                #region IS COMPANY ACCESSABLE FOR CLIENT
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                {
                    if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                    {
                        if (TdsMan.IsCompanyAccessible(Convert.ToInt64(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                        {
                            cmnService.J_UserMessage("You are not authorised to proceed");
                            //BtnCancel.Select();
                            return;
                        }

                        //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

                    }
                }
                #endregion
                //-----------------------------------------
                //--
                if (rbnCSVOption.Checked == false && rbnExcelOption.Checked == false)
                {
                    cmnService.J_UserMessage("Select the Export option...");
                    //rbnCSVOption.Focus();
                    return;
                }
                //--
                grpExport.Visible = true;
                grpExport.Location = new System.Drawing.Point(148, 266);
                //
                tbcExportData.Enabled = false;
                grpButtons.Enabled = false;
                //
                string tan = "";
                if (rbnRegularReturn.Checked == true)
                {
                    string[] word = cmbCompany.Text.Trim().Split('[');
                    string strTAN = word[1].Remove(word[1].Length - 1, 1);
                    tan = strTAN;
                }
                else if (rbnCorrectionReturn.Checked == true)
                {
                    //string[] word = dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[4].Value.ToString();
                    string strTAN = dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[5].Value.ToString();
                    tan = strTAN;
                }
                //
                //txtDestinationFileName.Text = "XLS_EXPORT_" + cmbFormNo.Text.Trim() + "_" + cmbQuarter.Text.Trim() + "_" + cmbFinancialYear.Text.Trim().Substring(2).Replace("-", "") + "_" + tan + ".XLS";
                if (rbnRegularReturn.Checked == true)
                {
                    if (rbnCSVOption.Checked == true)
                    {
                        lblFileFolderName.Text = "Folder Name";
                        if(chkSalaryDetails.Checked == true)
                            txtDestinationFileName.Text = "CSV_EXPORT_" + cmbFormNo.Text.Trim() + "_" + cmbQuarter.Text.Trim() + "_" + cmbFinancialYear.Text.Trim().Substring(2).Replace("-", "") + "_SD_" + tan;  //-- 22/01/2019 --
                        else
                            txtDestinationFileName.Text = "CSV_EXPORT_" + cmbFormNo.Text.Trim() + "_" + cmbQuarter.Text.Trim() + "_" + cmbFinancialYear.Text.Trim().Substring(2).Replace("-", "") + "_" + tan;  //-- 22/01/2019 --
                        btnExportToExcel.Text = "&Export to CSV";
                    }
                    else if (rbnExcelOption.Checked == true)
                    {
                        lblFileFolderName.Text = "File Name";
                        if (chkSalaryDetails.Checked == true)
                            txtDestinationFileName.Text = "XLS_EXPORT_" + cmbFormNo.Text.Trim() + "_" + cmbQuarter.Text.Trim() + "_" + cmbFinancialYear.Text.Trim().Substring(2).Replace("-", "") + "_SD_" + tan + ".XLSX";  //-- 21/08/2018 --
                        else
                            txtDestinationFileName.Text = "XLS_EXPORT_" + cmbFormNo.Text.Trim() + "_" + cmbQuarter.Text.Trim() + "_" + cmbFinancialYear.Text.Trim().Substring(2).Replace("-", "") + "_" + tan + ".XLSX";  //-- 21/08/2018 --
                        btnExportToExcel.Text = "&Export to Excel";
                    }
                }
                else if(rbnCorrectionReturn.Checked==true)
                {
                    if (rbnCSVOption.Checked == true)
                    {
                        lblFileFolderName.Text = "Folder Name";
                        if (chkSalaryDetails.Checked == true)
                            txtDestinationFileName.Text = "CSV_CORR_EXPORT_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[2].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[3].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[1].Value.ToString().Substring(2).Replace("-", "") + "_SD_" + tan;  //-- 22/01/2019 --
                        else
                            txtDestinationFileName.Text = "CSV_CORR_EXPORT_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[2].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[3].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[1].Value.ToString().Substring(2).Replace("-", "") + "_" + tan;  //-- 22/01/2019 --
                        btnExportToExcel.Text = "&Export to CSV";
                    }
                    else if (rbnExcelOption.Checked == true)
                    {
                        lblFileFolderName.Text = "File Name";
                        if (chkSalaryDetails.Checked == true)
                            txtDestinationFileName.Text = "XLS_CORR_EXPORT_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[2].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[3].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[1].Value.ToString().Substring(2).Replace("-", "") + "_SD_" + tan + ".XLSX";  //-- 21/08/2018 --
                        else
                            txtDestinationFileName.Text = "XLS_CORR_EXPORT_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[2].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[3].Value.ToString() + "_" + dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[1].Value.ToString().Substring(2).Replace("-", "") + "_" + tan + ".XLSX";  //-- 21/08/2018 --
                        btnExportToExcel.Text = "&Export to Excel";
                    }
                }
            }
            //       xlsx      
        }
        #endregion

        #region btnBack_Click
        private void btnBack_Click(object sender, EventArgs e)
        {
            btnNext.Text = "&Next";
            //tbcDeleteReturn.SelectTab(tbpWarning);
            //lblSteps.Text = "Step 1 of 2";
            //btnBack.Enabled = false;
            //btnBack.BackColor = System.Drawing.Color.LightGray;
            btnNext.Enabled = true;
            btnNext.BackColor = System.Drawing.Color.Lavender;
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
            //string strFormNo = cmbFormNo.Text.Trim();  //-- 30/12/2017 --
            //-- 2026/09/03
            string strFormNo = "";
            if (cmbFormNo.SelectedIndex > 0)
            {
                if(cmbFormNo.Text == T_FormNo.F24QSalaryDetails)
                    strFormNo = cmbFormNo.Text.Substring(0,3);            //
                else
                    strFormNo = cmbFormNo.Text.Substring(cmbFormNo.Text.IndexOf('(') + 1, cmbFormNo.Text.IndexOf(')') - cmbFormNo.Text.IndexOf('(') - 1);            //
            }
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                grpControlSummary.Visible = false;
                grpExportOption.Visible = false; //-- 2019/01/22
                pnlLine1.Visible = false;
                //pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false;
                //lblTotalRecords.Visible = false;
                btnNext.Enabled = false;
                btnNext.BackColor = System.Drawing.Color.LightGray;
                return;
            }
            else if (cmbFinancialYear.SelectedIndex > 0)
            {
                //
                long lngTotalRecords = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS, TRN_BASIC_INFO WHERE TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID AND TRN_BASIC_INFO.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), J_QueryType.DirectQuery);
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
            //-- 2021/02/19
            //if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2018_19ID)
            //{
            //    string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails};
            //    dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            //}
            //else if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2018_19ID)
            //{
            //    string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QForm16SalaryDetails};
            //    dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            //}
            //--
            if (cmbCompany.SelectedIndex <= 0)
            {
                grpControlSummary.Visible = false;
                grpExportOption.Visible = false; //-- 2019/01/22
                pnlLine1.Visible = false;
                //pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.Enabled = false;
                btnNext.BackColor = System.Drawing.Color.LightGray;   
                //
                if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails || cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
                {
                    cmbQuarter.Text = T_Qtr.Q4;
                    cmbQuarter.Enabled = false;
                }

                return;
            }
            if (cmbFormNo.Text == "")
            {
                grpControlSummary.Visible = false;
                grpExportOption.Visible = false; //-- 2019/01/22
                pnlLine1.Visible = false;
                //pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.Enabled = false;
                btnNext.BackColor = System.Drawing.Color.LightGray;
                return;
            }
            else if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails || cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
            {
                cmbQuarter.Text = T_Qtr.Q4;
                cmbQuarter.Enabled = false; //-- 29/12/2017 --
                //
                strFormNo = cmnService.J_Left(cmbFormNo.Text, 4).Trim();
            }
            ////-- Added On 27/12/2017 --
            ////
            //if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails || cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
            //{
            //    string[] strQtr = { T_Qtr.Q4 };
            //    dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter);
            //}
            ////--
            //
            if (cmbQuarter.Text == "")
            {
                grpControlSummary.Visible = false;
                grpExportOption.Visible = false; //-- 2019/01/22
                pnlLine1.Visible = false;
                //pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.Enabled = false;
                btnNext.BackColor = System.Drawing.Color.LightGray;
                return;
            }
            //      
            //if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2018_19ID)
            //{
            //    string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails };
            //    dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            //}
            //else if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2018_19ID)
            //{
            //    string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QForm16SalaryDetails };
            //    dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            //}
            //--
            //string strFormNo = cmnService.J_Left(cmbFormNo.Text, 4).Trim();
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        cmbQuarter.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        strFormNo);
            //if (lngBasicInfoID == 0)
            //{
            //    lblHideTabs.Text = "No records found for the selected return";
            //    return;
            //}
            //--
            //
            //-- 29/12/2017 --
            if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails || cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
            {
                if (lngBasicInfoID == 0)
                {
                    txtNoOfRecords.Text = string.Empty;
                    txtNetTaxableIncome.Text = string.Empty;
                    grpControlSalary.Visible = false;
                    grpControlSummary.Visible = false;
                    grpExportOption.Visible = false; //-- 2019/01/22
                    pnlLine1.Visible = false;
                    btnNext.Enabled = false;
                    btnNext.BackColor = System.Drawing.Color.LightGray;
                    return;
                }
                grpControlSalary.Visible = true;
                grpExportOption.Visible = true; //-- 2019/01/22
                //grpControlSalary.Location = new System.Drawing.Point(168, 350);
                grpControlSalary.Size = new System.Drawing.Size(698, 93);
                btnNext.Enabled = true;
                btnNext.BackColor = System.Drawing.Color.Lavender;
                txtNoOfRecords.Text = Convert.ToString(dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID, J_QueryType.DirectQuery));
                txtNetTaxableIncome.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID)) == "" ? "0.00" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID))));
                //
                grpControlSummary.Visible = false;
                pnlLine1.Visible = true;
                grpControlSalary.Visible = true; 
                //-- 2023/09/13
                if(Convert.ToDouble(txtNoOfRecords.Text) > dblMaxExcelXLSXMaxRows)
                {
                    rbnExcelOption.Enabled = false;
                    rbnCSVOption.Checked = true;
                    lblOnlyCSVMessage.Visible = true;
                }
                else
                {
                    rbnExcelOption.Enabled = true;
                    rbnCSVOption.Checked = false;
                    rbnExcelOption.Checked = false;
                    lblOnlyCSVMessage.Visible = false;
                }
            }
            else
            {
                ControlSummaryBasicInfo(lngBasicInfoID,false);
            }
            //ReturnFilingStatus(lngBasicInfoID);
            //-- 
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
            grpExport.Visible = false;
            //
            grpButtons.Enabled = true;
            tbcExportData.Enabled = true;
        }

        #endregion


        #region btnExportToExcel_Click

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            #region Variable Declaration
            strExcelFilePath = string.Empty;
            //
            // CHALLAN
            string strSerialNo = string.Empty;
            string strSection = string.Empty;
            string strTDS = string.Empty;
            string strSurcharge = string.Empty;
            string strEducationCess = string.Empty;
            string strInterest = string.Empty;
            string strFee = string.Empty;
            string strOther = string.Empty;
            string strTotalTaxDeposited = string.Empty;
            string strBSRCode = string.Empty;
            string strDateOnTaxDeposited = string.Empty;
            string strTransferVoucherChallanSerialNo = string.Empty;
            string strTdsDeposited = string.Empty;
            string strMinorHead = string.Empty;
            string strInterestAllocated = string.Empty;
            string strOtherAllocated = string.Empty;            
            string strChequeNo = string.Empty;
            //--
            //DEDUCTEE
            string strDeducteeSerialNo = string.Empty;
            string strChallanSerialRef = string.Empty;
            string strPanOfEmployee = string.Empty;
            string strNameOfEmployee = string.Empty;
            string strSectionCode = string.Empty;
            string strPaymentDate = string.Empty;
            string strDeductedDate = string.Empty;
            string strAmountPaid = string.Empty;
            string strDeducteeTds = string.Empty;
            string strDeducteeSurcharge = string.Empty;
            string strDeducteeEducationCess = string.Empty;
            string strTotalTaxDeducted = string.Empty;
            string strDeducteeTotalTaxDeposited = string.Empty;
            string strLowerDeduction = string.Empty;
            string strCertificateNonDeduction = string.Empty;
            string strEmployeeSerialNo = string.Empty;
            //--2023/09/11
            string strSec194NAmountExcess1Cr = string.Empty, strSec194NFAmount20LExcess1Cr = string.Empty, strSec194NCAmountExcess3Cr = string.Empty, strSec194N_FTAmount20LExcess3Cr = string.Empty;
            string strOptingOutOf115BAC = string.Empty;
            string strQueryDDAnnexReg;                   //For Storing the general SQL Query
            string strQueryDDAnnexCorr;                   //For Storing the general SQL Query
            //
            string strDeducteeCode = string.Empty;
            string strRateAtDeducted = string.Empty;
            //
            string strTDSActCode = string.Empty;
            string strRemitanceCode = string.Empty;
            string strAcknowledgmentNo = string.Empty;
            string strCountryCode = string.Empty;
            string strContactNo = string.Empty;
            string strEmail = string.Empty;
            string strTaxIdentificationNo = string.Empty;
            string strAddress = string.Empty;
            string strGrossingUp = string.Empty;
            //
            string strValueOfPurchase = string.Empty;
            //
            string strNonResident = string.Empty;
            string strPermanentlyEstablished = string.Empty;
            //
            // SALARY DETAILS
            string strSalEmpSerialNo = string.Empty;
            string strSalPanOfEmp = string.Empty;
            string strNameOfEmp = string.Empty;
            string strMode = string.Empty; //-- 2020/09/01            
            string strCategoryEmp = string.Empty;
            string strPeriodOfEmploymentFromDate = string.Empty;
            string strPeriodOfEmploymentTODate = string.Empty;
            string strNewRegime = string.Empty;
            string strTotalSalary = string.Empty;
            //string strGrossEducationUnderSection = string.Empty;
            //string strGrossDeductionUnderSection = string.Empty;
            string strGrossDeductionUnderSection16ii = string.Empty;
            string strGrossDeductionUnderSection16iii = string.Empty;
            string strGrossDeductionUnderSection16ia = string.Empty;
            string strGrossTotalDeductionUnderSection = string.Empty;
            string strIncomeChargeableUnderHeadSalaries = string.Empty;
            string strIncomeOtherThanSalary = string.Empty;
            string strGrossTotalIncome = string.Empty;
            string strDeductionunderChapterVIAUnderSection80CCE = string.Empty;
            string strDeductionUnderChapterVIAUnderSection80CCF = string.Empty;
            string strDeductionUnderChapterVIAUnderOtherSections = string.Empty;
            string strGrossTotalDeductionUnderChapterVIA = string.Empty;
            string strTotalTaxableIncome = string.Empty;
            string strIncomeTaxOnTotalIncome = string.Empty;
            string strSalSurcharge = string.Empty;
            string strSalEducationCess = string.Empty;
            string strIncomeTaxReleifUnderSection89 = string.Empty;
            string strNetTaxPayable = string.Empty;
            string strTotalTDSDeducted = string.Empty;
            string strShortfallExcessDeductionOfTax = string.Empty;
            string strCurrentEmployerSalary = string.Empty;
            string strPreviousEmployerSalary = string.Empty;
            string strCurrentEmployerTDS = string.Empty;
            string strPreviousEmployerTDS = string.Empty;
            string strWhetherTaxDeductedAtHigherRate = string.Empty;
            string strTDSIncludingSuperannuation = string.Empty;
            string strWhetherContributionsPaidByTrustees = string.Empty;
            string strNameOfSuperannuationFund = string.Empty;
            string strFromDate = string.Empty;
            string strToDate = string.Empty;
            string strAmountOfContributionRepaid = string.Empty;
            string strAverageRateOfDeduction = string.Empty;
            string strAmountOfTaxDeducted = string.Empty;
            string strSalGrossTotalIncome = string.Empty;
            string strWhetherRentPaymentExceeds1lakh = string.Empty;
            string strPANOfLandlord1 = string.Empty;
            string strNameOfLandlord1 = string.Empty;
            string strPANOfLandlord2 = string.Empty;
            string strNameOfLandlord2 = string.Empty;
            string strPANOfLandlord3 = string.Empty;
            string strNameOfLandlord3 = string.Empty;
            string strPANOfLandlord4 = string.Empty;
            string strNameOfLandlord4 = string.Empty;
            string strWhetherInterestpaidExceeds1lakh = string.Empty;
            string strPANOfLender1 = string.Empty;
            string strNameOfLender1 = string.Empty;
            string strPANOfLender2 = string.Empty;
            string strNameOfLender2 = string.Empty;
            string strPANOfLender3 = string.Empty;
            string strNameOfLender3 = string.Empty;
            string strPANOfLender4 = string.Empty;
            string strNameOfLender4 = string.Empty;
            //-- Added By Abhishek dey on 10/05/2019 --
            // For New Format
            string strSec17_1 = string.Empty;
            string strSec17_2 = string.Empty;
            string strSec17_3 = string.Empty;
            string strOtherExemptionSec10 = string.Empty;
            string strBalance = string.Empty;
            string strIncomeLossHousePropertyOfferedTDS = string.Empty;
            string strUnderSec80C = string.Empty;
            string strUnderSec80CCC = string.Empty;
            string strUnderSec80CCD_1 = string.Empty;
            string strTotalAggregate = string.Empty;
            string strUnderSec80CCD_1B = string.Empty;
            string strUnderSec80CCD_2 = string.Empty;
            string strUnderSec80D = string.Empty;
            string strUnderSec80E = string.Empty, strUnderSec80CCH = string.Empty, strUnderSec80CCH_1 = string.Empty;
            string strUnderSec80G = string.Empty;
            string strUnderSec80TTA = string.Empty;
            //-------------------------------------------------
            //
            // SALARY DETAILS FORM-16
            string strForm16EmployeeSerialNo = string.Empty;
            string strForm16PANOfTheEmployee = string.Empty;
            string strForm16NameOfTheEmployee = string.Empty;
            string strForm16CategoryOfTheEmployee = string.Empty;
            string strForm16PeriodOfEmploymentFormDate = string.Empty;
            string strForm16PeriodofemploymentToDate = string.Empty;
            string strForm16GrossSalarySec171 = string.Empty;
            string strForm16GrossSalarySec172 = string.Empty;
            string strForm16GrossSalarySec173 = string.Empty;
            
            string strForm16TotalSalary335 = string.Empty;    //-- 16/02/2018 --
            string strForm16LessAllowanceUnderSection10Description1 = string.Empty;
            string strForm16LessAllowanceUnderSection10Amount1 = string.Empty;
            string strForm16LessAllowanceUnderSection10Description2 = string.Empty;
            string strForm16LessAllowanceUnderSection10Amount2 = string.Empty;
            string strForm16LessAllowanceUnderSection10Description3 = string.Empty;
            string strForm16LessAllowanceUnderSection10Amount3 = string.Empty;
            string strForm16LessAllowanceUnderSection10Description4 = string.Empty;
            string strForm16LessAllowanceUnderSection10Amount4 = string.Empty;
            string strForm16LessAllowanceUnderSection10Description5 = string.Empty;
            string strForm16LessAllowanceUnderSection10Amount5 = string.Empty;
            string strForm16AllowanceTotal = string.Empty;
            string strForm16Balance = string.Empty;
            string strForm16CurrentEmployerSalary333 = string.Empty;    //-- 16/02/2018 --
            string strForm16PreviousEmployerSalary334 = string.Empty;   //-- 16/02/2018 --
            string strForm16DeductionsEntertainmentAllowance16ii = string.Empty; //-- 16/02/2018 --
            string strForm16DeductionsTaxOnEmployment16iii = string.Empty;   //-- 16/02/2018 --
            string strForm16Deductions16ia = string.Empty;   //-- 27/12/2018 --
            string strForm16AggregateOf4ab = string.Empty;   //-- 16/02/2018 --
            string strForm16IncomeChargeableUnderTheHead3_5 = string.Empty;  //-- 16/02/2018 --
            string strForm16AddAnyOtherIncomeOtherThanSalaryDescription1 = string.Empty;
            string strForm16AddAnyOtherIncomeOtherThanSalaryAmount1 = string.Empty;
            string strForm16AddAnyOtherIncomeOtherThanSalaryDescription2 = string.Empty;
            string strForm16AddAnyOtherIncomeOtherThanSalaryAmount2 = string.Empty;
            string strForm16AddAnyOtherIncomeOtherThanSalaryDescription3 = string.Empty;
            string strForm16AddAnyOtherIncomeOtherThanSalaryAmount3 = string.Empty;
            string strForm16AddAnyOtherIncomeOtherThanSalaryDescription4 = string.Empty;
            string strForm16AddAnyOtherIncomeOtherThanSalaryAmount4 = string.Empty;
            string strForm16Total = string.Empty;
            string strForm16GrossTotalIncome6_7 = string.Empty;  //-- 16/02/2018 --
            string strForm16DeductionsUnderChapterSection80CDescription1 = string.Empty;
            string strForm16DeductionsUnderChapterAmount1 = string.Empty;
            string strForm16DeductionsUnderChapterSection80CDescription2 = string.Empty;
            string strForm16DeductionsUnderChapterAmount2 = string.Empty;
            string strForm16DeductionsUnderChapterSection80CDescription3 = string.Empty;
            string strForm16DeductionsUnderChapterAmount3 = string.Empty;
            string strForm16DeductionsUnderChapterSection80CDescription4 = string.Empty;
            string strForm16DeductionsUnderChapterAmount4 = string.Empty;
            string strForm16DeductionsUnderChapterSection80CDescription5 = string.Empty;
            string strForm16DeductionsUnderChapterAmount5 = string.Empty;
            string strForm16DeductionsUnderChapterSection80CDescription6 = string.Empty;
            string strForm16DeductionsUnderChapterAmount6 = string.Empty;
            string strForm16GrossTotal80C = string.Empty;
            string strForm16DeductibleTotal80C = string.Empty;
            string strForm16Section80CCCGrossAmount = string.Empty;
            string strForm16Section80CCCDeductibleAmount = string.Empty;
            string strForm16Section80CCDGrossAmount = string.Empty;
            string strForm16Section80CCDDeductibleAmount = string.Empty;
            string strForm16TotalDeductibleAmount80CCE = string.Empty;
            string strForm16Section80CCGGrossAmount = string.Empty;
            string strForm16Section80CCGDeductibleAmount = string.Empty;
            string strForm16OtherSection = string.Empty;  // BH
            string strForm16GrossAmount = string.Empty;
            string strForm16QualifyingAmount = string.Empty;
            string strForm16DeductibleAmount = string.Empty;
            string strForm16OtherSection2 = string.Empty;
            string strForm16GrossAmountBM = string.Empty;
            string strForm16QualifyingAmountBN = string.Empty;
            string strForm16DeductibleAmountBO = string.Empty;
            string strForm16OtherSection3 = string.Empty;
            string strForm16GrossAmountBQ = string.Empty;
            string strForm16QualifyingAmountBR = string.Empty;
            string strForm16DeductibleAmountBS = string.Empty;
            string strForm16OtherSection4 = string.Empty;
            string strForm16GrossAmountBU = string.Empty;
            string strForm16QualifyingAmountBV = string.Empty;
            string strForm16DeductibleAmountBW = string.Empty;
            string strForm16OtherSection5 = string.Empty;
            string strForm16GrossAmountBY = string.Empty;
            string strForm16QualifyingAmountBZ = string.Empty;
            string strForm16DeductibleAmountCA = string.Empty;
            string strForm16TotalDeductibleAmountOtherSections = string.Empty;
            string strForm16AggregateOfDeductibleAmountUnderChapter = string.Empty;
            string strForm16TotalTaxableIncome344 = string.Empty;
            string strForm16IncomeTaxOnTotalIncome345 = string.Empty;
            string strForm16SurchargeOnTaxComputedOnSrl12 = string.Empty;
            string strForm16EducationCessOnTaxComputedOnSrl12346 = string.Empty;
            string strForm16TaxPayable = string.Empty;
            string strForm16LessReliefUnderSection89 = string.Empty;
            string strForm16NetTaxPayable = string.Empty;
            string strForm16TotalTDSDeducted = string.Empty;
            string strForm16CurrentEmployerTDS349 = string.Empty;
            string strForm16PreviousEmployerTDS350 = string.Empty;
            string strForm16ShortfallExcessDeductionOfTax = string.Empty;
            string strForm16WhetherTaxDeductedAtHigherRate = string.Empty;
            string strForm16TDSIncludingSuperannuation = string.Empty;
            string strForm16WhetherContributionsPaidOfSuperannuationFund = string.Empty;
            string strForm16NameOfSuperannuationFund = string.Empty;
            string strForm16FromDate = string.Empty;
            string strForm16ToDate = string.Empty;
            string strForm16AmountOfContributionRepaid = string.Empty;
            string strForm16AverageRateOfDeduction = string.Empty;
            string strForm16AmountOfTaxDeducted = string.Empty;
            string strForm16GrossTotalIncomeCX = string.Empty;
            string strForm16WhetherRentPaymentExceeds1lakhDuringPreviousYear = string.Empty;
            string strForm16PANOfLandlord1 = string.Empty;
            string strForm16NameOfLandlord1 = string.Empty;
            string strForm16PANOfLandlord2 = string.Empty;
            string strForm16NameOfLandlord2 = string.Empty;
            string strForm16PANOfLandlord3 = string.Empty;
            string strForm16NameOfLandlord3 = string.Empty;
            string strForm16PANOfLandlord4 = string.Empty;
            string strForm16NameOfLandlord4 = string.Empty;
            string strForm16WhetherInterestPaidExceeds1lakh = string.Empty;
            string strForm16PANOfLender1 = string.Empty;
            string strForm16NameOfLender1 = string.Empty;
            string strForm16PANOfLender2 = string.Empty;
            string strForm16NameOfLender2 = string.Empty;
            string strForm16PANOfLender3 = string.Empty;
            string strForm16NameOflender3 = string.Empty;
            string strForm16PANOfLender4 = string.Empty;
            string strForm16NameOfLender4 = string.Empty;
            //string 
            //-- Added By Abhishek DEy On 09/05/2019 --
            // FOR NEW FORMAT
            string strSec10_5 = string.Empty;
            string strSec10_10 = string.Empty;
            string strSec10_10A = string.Empty;
            string strSec10_10AA = string.Empty;
            string strSec10_13A = string.Empty; 
            string strSec10_14 = string.Empty; 
            string strSec_10_Description_1 = string.Empty;
            string strSec_10_Amount_1 = string.Empty;
            string strSec_10_Description_2 = string.Empty;
            string strSec_10_Amount_2 = string.Empty;
            string strSec_10_Description_3 = string.Empty;
            string strSec_10_Amount_3 = string.Empty;
            string strSec_10_Description_4 = string.Empty;
            string strSec_10_Amount_4 = string.Empty;
            string strSec_10_Description_5 = string.Empty;
            string strSec_10_Amount_5 = string.Empty;
            string strAllowance_Total = string.Empty;
            string str3_Balance = string.Empty;
            string strCurrent_Employer_Salary = string.Empty;
            string strPrevious_Employer_Salary = string.Empty;
            string strEntertainment_Allowance_16ii = string.Empty;
            string strTax_On_Employment_16iii = string.Empty;
            string strDeductions_16ia = string.Empty;
            string strAggregateOf_4 = string.Empty;
            string strHeadSalaries3 = string.Empty;
            string strHouseProperty = string.Empty;
            string strIncome_Other_Sources = string.Empty;
            string strGross_Total_Income6_7 = string.Empty;
            string strUnder_Sec_80C_Gross = string.Empty;
            string strUnder_Sec_80C_Amount = string.Empty;
            string strUnder_Sec_80CCC_Gross = string.Empty;
            string strUnder_Sec_80CCC_Amount = string.Empty;
            string strUnder_Sec_80CCD1_Gross = string.Empty;
            string strUnder_Sec_80CCD1_Amount = string.Empty;
            string str80C_80CCC_80CCD_Gross = string.Empty;
            string str80C_80CCC_80CCD_Amount = string.Empty;
            string strUnder_Sec_80CCD1B_Gross = string.Empty;
            string strUnder_Sec_80CCD1B_Amount = string.Empty;
            string strUnder_Sec_80CCD2_gross = string.Empty;
            string strUnder_Sec_80CCD2_Amount = string.Empty;
            string strUnder_Sec_80D_Gross = string.Empty;
            string strUnder_Sec_80D_Amount = string.Empty;
            string strUnder_Sec_80E_Gross = string.Empty;
            string strUnder_Sec_80E_Amount = string.Empty;
            string strUnder_Sec_80G_Gross = string.Empty;
            string strUnder_Sec_80G_Qualifying_Amount = string.Empty;
            string strUnder_Sec_80G_Amount = string.Empty;
            string strUnder_Sec_80TTA_Gross = string.Empty;
            string strUnder_Sec_80TTA_Qualifying_Amount = string.Empty;
            string strUnder_Sec_80TTA_Amount = string.Empty;
            string strOther_Provisions_Section = string.Empty;
            string strOther_Provisions_gross = string.Empty;
            string strOther_Provisions_Qualifying_Amount = string.Empty;
            string strOther_Provisions_Amount = string.Empty;
            string strOther_Provisions_Section_2 = string.Empty;
            string strOther_Provisions_gross_2 = string.Empty;
            string strOther_Provisions_Qualifying_Amount_2 = string.Empty;
            string strOther_Provisions_Amount_2 = string.Empty;
            string strOther_Provisions_Section_3 = string.Empty;
            string strOther_Provisions_gross_3 = string.Empty;
            string strOther_Provisions_Qualifying_Amount_3 = string.Empty;
            string strOther_Provisions_Amount_3 = string.Empty;
            string strOther_Provisions_Section_4 = string.Empty;
            string strOther_Provisions_gross_4 = string.Empty;
            string strOther_Provisions_Qualifying_Amount_4 = string.Empty;
            string strOther_Provisions_Amount_4 = string.Empty;
            string strOther_Provisions_Section_5 = string.Empty;
            string strOther_Provisions_gross_5 = string.Empty;
            string strOther_Provisions_Qualifying_Amount_5 = string.Empty;
            string strOther_Provisions_Amount_5 = string.Empty;
            string strOther_Provisions_Section_6 = string.Empty;
            string strOther_Provisions_gross_6 = string.Empty;
            string strOther_Provisions_Qualifying_Amount_6 = string.Empty;
            string strOther_Provisions_Amount_6 = string.Empty;
            string strTotal_Amount_Under_Sec_Gross = string.Empty;
            string strTotal_Amount_Under_Sec_Total_Qualifying_Amount = string.Empty;
            string strTotal_Amount_Under_Sec_Amount = string.Empty;
            string strGross_Total_Deduction = string.Empty;
            string strTotal_Taxable_Income = string.Empty;
            string strIncome_Tax_Total = string.Empty;
            string strRebate = string.Empty;
            //string strSurcharge = string.Empty;
            string str_Education_Cess = string.Empty;
            string strTax_Payable = string.Empty;
            string strRelief_Under_Section_89 = string.Empty;
            string strNet_Tax_Payable = string.Empty;
            string strTotal_TDS_Deducted = string.Empty;
            string strCurrent_Employer_TDS = string.Empty;
            string strPreviousEmployer_TDS = string.Empty;
            string strShortfall = string.Empty;
            string strTax_Deducted_Higher_Rate = string.Empty;
            string strSuperannuation = string.Empty;
            string strSuperannuation_Fund = string.Empty;
            string strName_Superannuation = string.Empty;
            string strFrom_Date = string.Empty;
            string strTo_Date = string.Empty;
            string strcontribution_Repaid = string.Empty;
            string strAverage_Rate_Deduction = string.Empty;
            string strAmount_Tax_Deducted = string.Empty;
            string strGross_Total_Income = string.Empty;
            string str_Rent_Payment = string.Empty;
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
            //--
            string strFinancialYear = "", strQuarter = "", strFormNo = "", strCompany = "";
            int intFinancialYearID = 0;
            if (rbnRegularReturn.Checked ==true)
            {
                strFinancialYear = cmbFinancialYear.Text;
                strQuarter = cmbQuarter.Text;
                strFormNo = cmbFormNo.Text;
                strCompany = cmbCompany.Text;
                intFinancialYearID = Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
            }
            else if (rbnCorrectionReturn.Checked == true)
            {
                strFinancialYear = dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[1].Value.ToString();
                strQuarter = dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[3].Value.ToString();
                strFormNo = dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[2].Value.ToString();
                strCompany = dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[4].Value.ToString();
                intFinancialYearID = cmnService.J_ReturnInt32Value(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[8].Value.ToString());
            }
            //--
            if (cmnService.J_UserMessage("FINANCIAL YEAR \t: " + strFinancialYear + " \n " +
                        "QUARTER \t: " + strQuarter + " \n " +
                        "FORM NO. \t: " + strFormNo + " \n " +
                        "COMPANY  \t: " + strCompany + " \n " +
                        "Are you sure you want to Export data ??", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;
            else
            {
                try
                {
                    //strFormNo = TdsMan.GetFormNoIT1961(strFormNo);
                    if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails)
                        strFormNo = strFormNo.Substring(0, 3);
                    else
                        strFormNo = strFormNo.Split('(', ')')[1];
                    //---   
                    prgBar.Value = prgBar.Value + 5;  //-- 01/01/2018
                    //
                    if (CREATE_EXCEL_FILE(strExcelFilePath) == false)
                    {
                        cmnService.J_UserMessage("Some error occurred");
                        return;
                    }
                    else
                    {
                        strFormNo = strFormNo;
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
                            //-----------------------------------------------------------
                            string[,] strLoadChallanGridMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TRANSFER_VOUCHER_NO", "F"},
                                            {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.CHALLAN_NO", "F"}};

                        //-------------------------------------------------------------
                        //string[,] strLoadChallanTotTaxMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
                        //                            {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED- TRN_CHALLAN.LATE_FEE", "F"}};
                        string[,] strLoadChallanTotTaxMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TOT_TAX", "F"},
                                                    {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.TOT_TAX", "F"}};
                        //-------------------------------------------------------------
                        //
                        string[,] strLoadChallanBookEntryMatrix = {{"TRN_CHALLAN.BOOK_ENTRY = 1", "F", "Y", "T"},
                                            {"TRN_CHALLAN.BOOK_ENTRY = 0", "F", " ", "T"}};
                            //--------------------------------------------------------------
                            string[,] strValueOfPurchaseMatrix = {{"TRN_DEDUCTEE_DETAILS.TOT_VALUE_PURCHASE = 1", "F", "Y", "T"},
                                            {"TRN_DEDUCTEE_DETAILS.TOT_VALUE_PURCHASE = 0", "F", " ", "T"}};
                            //--------------------------------------------------------------  
                            //string[,] strGrossingUpIndicatorMatrix = {{"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = '0' OR TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = ''", "F", " ", "T"},
                            //                    //{"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = ''", "F", " ", "T"},
                            //                    {"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = 'Y'", "F", "Y", "T"},
                            //                    {"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = 'N'", "F", "N", "T"}};
                            //   cmnService.J_SQLDBFormat("DED.COUNT_REC", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 0 " +
                            //--------------------------------------------------------------
                            string[,] strGrossingUpIndicatorMatrix = {{"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = 'Y' ", "F", "Yes", "T"},
                                                {"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = 'N'", "F", "No", "T"},
                                                {"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = ''", "F", "", "T"}};

                        //string[,] strGrossingUpIndicatorMatrix = {{"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = ''", "F", "", "T"},
                        //                    {cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR", J_ColumnType.String, J_SQLColFormat.NullCheck) , "F", "", "T"},   //-- 22/02/2018 --
                        //                    {"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = 'Y'", "F", "Y", "T"},
                        //                    {"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = 'N'", "F", "N", "T"}};
                        //--------------------------------------------------------------
                        //
                        //-- 16/02/2018 --
                        //string[,] strTaxDeductedAtHigherRateMatrix = {{"TRN_SALARY_DETAILS.TAX_DEDUCTED_HIGHER_RATE = 1", "F", "Y", "T"},
                        //                    {cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.TAX_DEDUCTED_HIGHER_RATE", J_ColumnType.String, J_SQLColFormat.NullCheck) , "F", " ", "T"},   //-- 26/02/2018 --
                        //                    {"TRN_SALARY_DETAILS.TAX_DEDUCTED_HIGHER_RATE = 0", "F", " ", "T"}};
                        //-- 2019/05/07
                        string[,] strTaxDeductedAtHigherRateMatrix = {{"TRN_SALARY_DETAILS.TAX_DEDUCTED_HIGHER_RATE = 1", "F", "Y", "T"},
                                            {"TRN_SALARY_DETAILS.TAX_DEDUCTED_HIGHER_RATE = 0", "F", " ", "T"}};

                        //-- 2021/08/06
                        string[,] strSECTION_115BAC_FLAGMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "Y", "T"},
                                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", " ", "T"}};
                        //
                        string[,] strSECTION_115BAC_FLAGMatrixCorr = {{"COR_TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "Y", "T"},
                                            {"COR_TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", " ", "T"}};

                        //Iif(Isnull(trn_salary_details.super_ann_yn) = true, '',  Iif(trn_salary_details.super_ann_yn = '', '',
                        //    Iif(trn_salary_details.super_ann_yn = 'N', '', super_ann_amount))) AS [Amount of contribution repaid1],   {cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.SUPER_ANN_YN", J_ColumnType.String, J_SQLColFormat.NullCheck), "F", "", "T"},

                        //string[,] strAmountOfContributionRepaidMatrix = {{"TRN_SALARY_DETAILS.SUPER_ANN_YN = '' ", "F", "", "T"},
                        //                    {"TRN_SALARY_DETAILS.SUPER_ANN_YN = 'N'", "F", "", "T"},  
                        //                    {"TRN_SALARY_DETAILS.SUPER_ANN_YN = 'Y'", "F", "TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT", "F"}};
                        //--------------------------------------------------------------
                        //
                        //-- 17/02/2018 --
                        string[,] strContributionsPaidOfSuperannuationFundMatrix = {{"TRN_SALARY_DETAILS.SUPER_ANN_YN = 1", "F", "Y", "T"},
                                            {"TRN_SALARY_DETAILS.SUPER_ANN_YN = 0", "F", " ", "T"}};
                        //--------------------------------------------------------------
                        string[,] strWhetherRentExceeds1lakhDuringPreviousYearMatrix = {{"TRN_SALARY_DETAILS.RENT_EXCEEDING_YN = 1", "F", "Y", "T"},
                                        {"TRN_SALARY_DETAILS.RENT_EXCEEDING_YN = 0", "F", " ", "T"}};
                        //--------------------------------------------------------------
                        string[,] strWhetherInterestPaidExceeds1LakhMatrix = {{"TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER = 1", "F", "Y", "T"},
                                        {"TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER = 0", "F", " ", "T"}};
                        //--------------------------------------------------------------
                        //-- 24/02/2018 --
                        //string[,] strAmountOfContributionRepaidMatrix = {{"TRN_SALARY_DETAILS.SUPER_ANN_YN = ''", "F", " ", "SUPER_ANN_AMOUNT"},
                        //                    {cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.SUPER_ANN_YN", J_ColumnType.String, J_SQLColFormat.NullCheck) , "F", " ", "SUPER_ANN_AMOUNT"},   //-- 22/02/2018 --
                        //                    {"TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT = 'Y'", "F", "Y", "T"},
                        //                    {"TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT = 'N'", "F", "N", "T"}};
                        //--------------------------------------------------------------
                        //-- 26/02/2018 --
                        string[,] strAmountOfContributionRepaidMatrix = {{"TRN_SALARY_DETAILS.SUPER_ANN_YN = '' ", "F", "", "T"},
                                        {"TRN_SALARY_DETAILS.SUPER_ANN_YN = 'N'", "F", "", "T"},
                                        {"TRN_SALARY_DETAILS.SUPER_ANN_YN = 'Y'", "F", "TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT", "F"}};
                        //--------------------------------------------------------------
                        string[,] strAverageRateOfDeductionMatrix = {{"TRN_SALARY_DETAILS.SUPER_ANN_YN = ''", "F", "", "T"},
                                        {"TRN_SALARY_DETAILS.SUPER_ANN_YN = 'N'", "F", "", "T"},
                                        {"TRN_SALARY_DETAILS.SUPER_ANN_YN = 'Y'", "F", "TRN_SALARY_DETAILS.SUPER_ANN_RATE", "F"}};
                        //--------------------------------------------------------------
                        string[,] strAmountOfTaxDeductedMatrix = {{"TRN_SALARY_DETAILS.SUPER_ANN_YN = ''", "F", "", "T"},
                                        {"TRN_SALARY_DETAILS.SUPER_ANN_YN = 'N'", "F", "", "T"},
                                        {"TRN_SALARY_DETAILS.SUPER_ANN_YN = 'Y'", "F", "TRN_SALARY_DETAILS.SUPER_ANN_TAX", "F"}};

                        //--------------------------------------------------------------
                        //}
                        //else if(rbnCorrectionReturn.Checked==true)
                        //{
                        //-----------------------------------------------------------
                        string[,] strLoadChallanGridMatrixCorr = {{"COR_TRN_CHALLAN.CHALLAN_NO = ''", "F", "COR_TRN_CHALLAN.TRANSFER_VOUCHER_NO", "F"},
                                        {"COR_TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "COR_TRN_CHALLAN.CHALLAN_NO", "F"}};

                        //-------------------------------------------------------------
                        //string[,] strLoadChallanTotTaxMatrixCorr = {{"COR_TRN_CHALLAN.CHALLAN_NO = ''", "F", "COR_TRN_CHALLAN.TOT_TAX - COR_TRN_CHALLAN.INTEREST_ALLOCATED - COR_TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
                        //                            {"COR_TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "COR_TRN_CHALLAN.TOT_TAX - COR_TRN_CHALLAN.INTEREST_ALLOCATED - COR_TRN_CHALLAN.OTHERS_ALLOCATED - COR_TRN_CHALLAN.LATE_FEE", "F"}};
                        string[,] strLoadChallanTotTaxMatrixCorr = {{"COR_TRN_CHALLAN.CHALLAN_NO = ''", "F", "COR_TRN_CHALLAN.TOT_TAX", "F"},
                                                    {"COR_TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "COR_TRN_CHALLAN.TOT_TAX", "F"}};
                        //-------------------------------------------------------------
                        //
                        string[,] strLoadChallanBookEntryMatrixCorr = {{"COR_TRN_CHALLAN.BOOK_ENTRY = 1", "F", "Y", "T"},
                                            {"COR_TRN_CHALLAN.BOOK_ENTRY = 0", "F", " ", "T"}};
                        //--------------------------------------------------------------
                        string[,] strValueOfPurchaseMatrixCorr = {{"COR_TRN_DEDUCTEE_DETAILS.TOT_VALUE_PURCHASE = 1", "F", "Y", "T"},
                                        {"COR_TRN_DEDUCTEE_DETAILS.TOT_VALUE_PURCHASE = 0", "F", " ", "T"}};
                        //--------------------------------------------------------------  
                        //--------------------------------------------------------------
                        string[,] strGrossingUpIndicatorMatrixCorr = {{"COR_TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = 'Y' ", "F", "Yes", "T"},
                                            {"COR_TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = 'N'", "F", "No", "T"},
                                            {"COR_TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = ''", "F", "", "T"}};                            
                        //--------------------------------------------------------------
                        //
                        //-- 2019/05/07
                        string[,] strTaxDeductedAtHigherRateMatrixCorr = {{"COR_TRN_SALARY_DETAILS.TAX_DEDUCTED_HIGHER_RATE = 1", "F", "Y", "T"},
                                        {"COR_TRN_SALARY_DETAILS.TAX_DEDUCTED_HIGHER_RATE = 0", "F", " ", "T"}};
                        //--------------------------------------------------------------
                        //
                        //-- 17/02/2018 --
                        string[,] strContributionsPaidOfSuperannuationFundMatrixCorr = {{"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = 1", "F", "Y", "T"},
                                        {"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = 0", "F", " ", "T"}};
                        //--------------------------------------------------------------
                        string[,] strWhetherRentExceeds1lakhDuringPreviousYearMatrixCorr = {{"COR_TRN_SALARY_DETAILS.RENT_EXCEEDING_YN = 1", "F", "Y", "T"},
                                        {"COR_TRN_SALARY_DETAILS.RENT_EXCEEDING_YN = 0", "F", " ", "T"}};
                        //--------------------------------------------------------------
                        string[,] strWhetherInterestPaidExceeds1LakhMatrixCorr = {{"COR_TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER = 1", "F", "Y", "T"},
                                        {"COR_TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER = 0", "F", " ", "T"}};
                        //--------------------------------------------------------------
                        //--------------------------------------------------------------
                        //-- 26/02/2018 --
                        string[,] strAmountOfContributionRepaidMatrixCorr = {{"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = '' ", "F", "", "T"},
                                        {"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = 'N'", "F", "", "T"},
                                        {"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = 'Y'", "F", "COR_TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT", "F"}};
                        //--------------------------------------------------------------
                        string[,] strAverageRateOfDeductionMatrixCorr = {{"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = ''", "F", "", "T"},
                                        {"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = 'N'", "F", "", "T"},
                                        {"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = 'Y'", "F", "COR_TRN_SALARY_DETAILS.SUPER_ANN_RATE", "F"}};
                        //--------------------------------------------------------------
                        string[,] strAmountOfTaxDeductedMatrixCorr = {{"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = ''", "F", "", "T"},
                                            {"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = 'N'", "F", "", "T"},
                                            {"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = 'Y'", "F", "COR_TRN_SALARY_DETAILS.SUPER_ANN_TAX", "F"}};
                        //-------------------------------------------------------------- //-- 2020/09/01
                        string[,] strSUPER_ANN_YNMatrixCorr = {{"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = '0'", "F", "N", "T"},
                                                {"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = 'N'", "F", "N", "T"},
                                                {"COR_TRN_SALARY_DETAILS.SUPER_ANN_YN = 'Y'", "F", "Y", "T"}};
                        //-------------------------------------------------------------- //-- 2020/09/01
                        string[,] strModeMatrixCorr = {{"COR_TRN_SALARY_DETAILS.MODE = 'D'", "F", "DELETE", "T"},
                                                {"COR_TRN_SALARY_DETAILS.MODE = 'A'", "F", "ADD", "T"},
                                                {"COR_TRN_SALARY_DETAILS.MODE = 'U'", "F", "MODIFY", "T"},
                                                {"COR_TRN_SALARY_DETAILS.PAN_UPDATION_INDICATOR = 1", "F", "PAN UPDT", "T"},
                                                {"COR_TRN_SALARY_DETAILS.MODE = ''", "F", "", "T"}};
                        //-- 2023/09/13
                        string[,] strSECTION_194NFMatrix = {{"TRN_DEDUCTEE_DETAILS.SEC194N_20LAKH_1CRORE_PAYMENT_AMOUNT > 0", "F", "1", "F"},
                                                {"TRN_DEDUCTEE_DETAILS.SEC194N_GREATER_1CRORE_PAYMENT_AMOUNT > 0", "F", "2", "T"},
                                                {"TRN_DEDUCTEE_DETAILS.SEC194N_20LAKH_1CRORE_PAYMENT_AMOUNT = 0 AND TRN_DEDUCTEE_DETAILS.SEC194N_GREATER_1CRORE_PAYMENT_AMOUNT = 0", "F", "", "T"}};
                        string[,] strSEC194NFTMatrix = {{"TRN_DEDUCTEE_DETAILS.SEC194NFT_20L_3CRORE > 0", "F", "1", "F"},
                                                {"TRN_DEDUCTEE_DETAILS.SEC194NFT_EXCESS_3CRORE > 0", "F", "2", "T"},
                                                {"TRN_DEDUCTEE_DETAILS.SEC194NFT_20L_3CRORE = 0 AND TRN_DEDUCTEE_DETAILS.SEC194NFT_EXCESS_3CRORE = 0", "F", "", "T"}};
                        string[,] strSECTION_115BAC_FLAGAnnexIMatrix = {{"TRN_DEDUCTEE_DETAILS.SECTION_115BAC_FLAG = 1", "F", "Y", "T"},
                                                {"TRN_DEDUCTEE_DETAILS.SECTION_115BAC_FLAG = 2", "F", "N", "T"},
                                                {"TRN_DEDUCTEE_DETAILS.SECTION_115BAC_FLAG = 0", "F", "", "T"}};

                        //}

                        //--------------------------------------------------------------
                        // FORM 24Q
                        #region FORM 24Q
                        if (strFormNo == T_FormNo.F24Q)
                        {
                            if (rbnCorrectionReturn.Checked == true && chkSalaryDetails.Checked == true)
                            {
                                #region 24Q SD CORRECTION
                                //-- 20/02/2018 --
                                if (CREATE_NEW_WORKSHEET(strExcelFilePath, "SALARY DETAILS") == false) return;
                                //
                                prgBar.Value = prgBar.Value + 5;
                                //--------------------------------------------------------------------------------------
                                //
                                strSQL = "UPDATE COR_TRN_SALARY_DETAILS SET SUPER_ANN_YN = '' WHERE SUPER_ANN_YN IS NULL AND BATCH_HEADER_ID = " + lngBasicInfoID;
                                dmlService.J_ExecSql(strSQL);
                                //--
                                //-- Added By Abhishek Dey On 10/05/2019 --
                                if (intFinancialYearID >= T_FinancialYearID.F2018_19ID)
                                {
                                    #region Variable Declaration 
                                    // For New Format
                                    strSalEmpSerialNo = "[Serial No]";
                                    strSalPanOfEmp = "[PAN of the Employee]";
                                    strNameOfEmp = "[Name of the Employee]";
                                    strMode = "[Mode]";
                                    strCategoryEmp = "[Category of the Employee]";
                                    strPeriodOfEmploymentFromDate = "[Period of employment : From Date (dd/mm/yyyy)]";
                                    strPeriodOfEmploymentTODate = "[Period of employment : To Date (dd/mm/yyyy)]";
                                    strNewRegime = "[New Regime (Taxation u / s 115BAC) (Y / N)]";
                                    //-- Added By Abhishek Dey On 10/05/2019 --
                                    strSec17_1 = "[Sec 17(1)]";
                                    strSec17_2 = "[Sec 17(2)]";
                                    strSec17_3 = "[Sec 17(3)]";
                                    //--------------------------------------------------
                                    strTotalSalary = "[Total Salary]";
                                    //-- Added By Abhishek Dey On 10/05/2019 --
                                    strSec10_5 = "[Sec10(5)]";
                                    strSec10_10 = "[Sec10(10)]";
                                    strSec10_10A = "[Sec10(10A)]";
                                    strSec10_10AA = "[Sec10(10AA)]";
                                    strSec10_13A = "[Sec10(13A)]";
                                    strOtherExemptionSec10 = "[Any Other exemption u/s Sec10]";
                                    strBalance = "[Balance]";
                                    //--------------------------------------------------
                                    strGrossDeductionUnderSection16ii = "[Gross Deduction under section 16(ii)]";
                                    strGrossDeductionUnderSection16iii = "[Gross Deduction under section 16(iii)]";
                                    strGrossDeductionUnderSection16ia = "[Gross Deduction under section 16(ia)]";
                                    strGrossTotalDeductionUnderSection = "[Gross Total Deduction under section 16(ii) & 16(iii) & 16(ia)]";
                                    strIncomeChargeableUnderHeadSalaries = "[Income Chargeable under head Salaries]";
                                    //-- Added By Abhishek Dey On 10/05/2019 --
                                    strIncomeLossHousePropertyOfferedTDS = "[Income or loss - House Property offered for TDS]";
                                    //--------------------------------------------------
                                    strIncomeOtherThanSalary = "[Income-Other Sources offered for TDS]";
                                    strGrossTotalIncome = "[Gross Total Income(340)]";
                                    //-- Added By Abhishek Dey On 10/05/2019 --
                                    strUnderSec80C = "[Deduction under Chapter VIA under sec 80C]";
                                    strUnderSec80CCC = "[Deduction under Chapter VIA under sec 80CCC]";
                                    strUnderSec80CCD_1 = "[Deduction under Chapter VIA under sec 80CCD(1)]";
                                    strTotalAggregate = "[Total deduction u / s 80C 80CCC & 80CCD(1)]";
                                    strUnderSec80CCD_1B = "[Deduction under Chapter VIA under sec 80CCD(1B)]";
                                    strUnderSec80CCD_2 = "[Deduction under Chapter VIA under sec 80CCD(2)]";
                                    strUnderSec80D = "[Deduction under Chapter VIA under sec 80D]";
                                    strUnderSec80E = "[Deduction under Chapter VIA under sec 80E]";
                                    strUnderSec80G = "[Deduction under Chapter VIA under sec 80G]";
                                    strUnderSec80TTA = "[Deduction under Chapter VIA under sec 80TTA]";
                                    strDeductionUnderChapterVIAUnderOtherSections = "[Deduction under Chapter VIA under Other sections]";
                                    strGrossTotalDeductionUnderChapterVIA = "[Gross Total Deduction under chapter VIA]";
                                    strTotalTaxableIncome = "[Total Taxable Income]";
                                    strIncomeTaxOnTotalIncome = "[Income Tax on Total Income]";
                                    strRebate = "[Rebate under section 87A]";
                                    strSalSurcharge = "[Surcharge]";
                                    strSalEducationCess = "[Education Cess]";
                                    strIncomeTaxReleifUnderSection89 = "[Income Tax Relief under section 89]";
                                    strNetTaxPayable = "[Net Tax Payable]";
                                    strTotalTDSDeducted = "[Total TDS Deducted]";
                                    strShortfallExcessDeductionOfTax = "[Shortfall/Excess Deduction of Tax]";
                                    strCurrentEmployerSalary = "[Current employer salary]";
                                    strPreviousEmployerSalary = "[Previous employer salary]";
                                    strCurrentEmployerTDS = "[Current employer TDS]";
                                    strPreviousEmployerTDS = "[Previous employer TDS]";
                                    strWhetherTaxDeductedAtHigherRate = "[Tax deducted at Higher rate due to non furnishing]";
                                    strTDSIncludingSuperannuation = "[TDS including Superannuation]";
                                    strWhetherContributionsPaidByTrustees = "[Contributions paid by trustees approved Superan fund (Y/N)]";
                                    strNameOfSuperannuationFund = "[Name of Superannuation Fund]";
                                    strFromDate = "[From Date]";
                                    strToDate = "[To Date]";
                                    strAmountOfContributionRepaid = "[Amount of contribution repaid]";
                                    strAverageRateOfDeduction = "[Average rate of deduction]";
                                    strAmountOfTaxDeducted = "[Amount of Tax deducted]";
                                    strSalGrossTotalIncome = "[Gross Total Income]";
                                    strWhetherRentPaymentExceeds1lakh = "[Whether rent payment exceeds 1lakh during previous year(Y/N)]";
                                    strPANOfLandlord1 = "[PAN of landlord 1]";
                                    strNameOfLandlord1 = "[Name of landlord 1]";
                                    strPANOfLandlord2 = "[PAN of landlord 2]";
                                    strNameOfLandlord2 = "[Name of landlord 2]";
                                    strPANOfLandlord3 = "[PAN of landlord 3]";
                                    strNameOfLandlord3 = "[Name of landlord 3]";
                                    strPANOfLandlord4 = "[PAN of landlord 4]";
                                    strNameOfLandlord4 = "[Name of landlord 4]";
                                    strWhetherInterestpaidExceeds1lakh = "[Whether interest paid exceeds 1lakh (Y/N)]";
                                    strPANOfLender1 = "[PAN of lender 1]";
                                    strNameOfLender1 = "[Name of lender 1]";
                                    strPANOfLender2 = "[PAN of lender 2]";
                                    strNameOfLender2 = "[Name of lender 2]";
                                    strPANOfLender3 = "[PAN of lender 3]";
                                    strNameOfLender3 = "[Name of lender 3]";
                                    strPANOfLender4 = "[PAN of lender 4]";
                                    strNameOfLender4 = "[Name of lender 4]";
                                    //--------------------------------------------------      
                                    //-- 2020/08/14
                                    strEmployeeSerialNo = "";
                                    strQueryDDAnnexReg = "";
                                    strQueryDDAnnexCorr = "";
                                    if (TDSMAN.Classes.TDSMAN.T_ShareReferenceNoF24Q == true)
                                    {
                                        strEmployeeSerialNo = "[Employee Serial No]";
                                        strQueryDDAnnexReg = ",TRN_SALARY_DETAILS.EMPLOYEE_SERIAL_NO AS " + strEmployeeSerialNo;
                                        strQueryDDAnnexCorr = ",COR_TRN_SALARY_DETAILS.EMPLOYEE_SERIAL_NO AS " + strEmployeeSerialNo;
                                    }
                                    //--
                                    #endregion
                                    //-- Salary Details
                                    #region Query New Format (24Q-SALARY DETAILS)
                                    strQueryDD = " SELECT COR_TRN_SALARY_DETAILS.SL_NO                      AS " + strSalEmpSerialNo + "," +
                                                "        COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN                                     AS " + strSalPanOfEmp + "," +
                                                "        COR_TRN_SALARY_DETAILS.EMPLOYEE_NAME                                  AS " + strNameOfEmp + "," +
                                                "        COR_TRN_SALARY_DETAILS.EMPLOYEE_CATEGORY                             AS " + strCategoryEmp + "," +
                                                //"        COR_TRN_SALARY_DETAILS.FROM_DATE                                  AS " + strPeriodOfEmploymentFromDate + "," +
                                                //"        COR_TRN_SALARY_DETAILS.TO_DATE                                    AS " + strPeriodOfEmploymentTODate + "," +
                                                "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPeriodOfEmploymentFromDate + "," +
                                                "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "     AS " + strPeriodOfEmploymentTODate + "," +
                                                "        " + cmnService.J_SQLDBFormat(strModeMatrixCorr, J_SQLColFormat.Case_End)  + " AS " + strMode + "," +
                                                "        " + cmnService.J_SQLDBFormat(strSECTION_115BAC_FLAGMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strNewRegime + ", " +
                                                "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_1                        AS " + strSec17_1 + "," +
                                                "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_2                        AS " + strSec17_2 + "," +
                                                "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_3                        AS " + strSec17_3 + "," +
                                                //"        COR_TRN_SALARY_DETAILS.TS_GS_TOTAL                              AS " + strTotalSalary + "," + //-- 2026/03/26
                                                "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_1 + COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_2 + COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_3 AS " + strTotalSalary + "," +
                                                "        COR_TRN_SALARY_DETAILS.SEC10_5_AMOUNT                       AS " + strSec10_5 + "," +
                                                "        COR_TRN_SALARY_DETAILS.SEC10_10_AMOUNT                     AS " + strSec10_10 + "," +
                                                "        COR_TRN_SALARY_DETAILS.SEC10_10A_AMOUNT                   AS " + strSec10_10A + "," +
                                                "        COR_TRN_SALARY_DETAILS.SEC10_10AA_AMOUNT                 AS " + strSec10_10AA + "," +
                                                "        COR_TRN_SALARY_DETAILS.SEC10_13A_AMOUNT                   AS " + strSec10_13A + "," +
                                                //"        COR_TRN_SALARY_DETAILS.SEC10_TOTAL_AMOUNT                AS " + strOtherExemptionSec10 + "," +
                                                "        COR_TRN_SALARY_DETAILS.TS_LA_TOTAL                AS " + strOtherExemptionSec10 + "," +
                                                //"        COR_TRN_SALARY_DETAILS.TS_BALANCE                                AS " + strBalance + "," + //-- 2026/03/26
                                                "        ( TS_GS_SEC_17_1 + TS_GS_SEC_17_2 + TS_GS_SEC_17_3) - (SEC10_5_AMOUNT + SEC10_10_AMOUNT + SEC10_10A_AMOUNT + SEC10_10AA_AMOUNT + SEC10_13A_AMOUNT + TS_LA_TOTAL) AS " + strBalance + "," +
                                                "        COR_TRN_SALARY_DETAILS.US_16_EA                                    AS " + strGrossDeductionUnderSection16ii + "," +
                                                "        COR_TRN_SALARY_DETAILS.US_16_TE                                     AS " + strGrossDeductionUnderSection16iii + "," +
                                                "        COR_TRN_SALARY_DETAILS.US_16_IA                                     AS " + strGrossDeductionUnderSection16ia + "," +
                                                "        COR_TRN_SALARY_DETAILS.US_16_AGGREGATE                      AS " + strGrossTotalDeductionUnderSection + "," +
                                                "        COR_TRN_SALARY_DETAILS.INCOME_CHARGEABLE                  AS " + strIncomeChargeableUnderHeadSalaries + "," +
                                                "        COR_TRN_SALARY_DETAILS.AIS_ITEM_1                                 AS " + strIncomeLossHousePropertyOfferedTDS + "," +
                                                "        COR_TRN_SALARY_DETAILS.AIS_ITEM_2                                 AS " + strIncomeOtherThanSalary + "," +
                                                "        COR_TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                AS " + strGrossTotalIncome + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_DED_TOTAL            AS " + strUnderSec80C + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCC_DED_AMOUNT     AS " + strUnderSec80CCC + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_DED_AMOUNT     AS " + strUnderSec80CCD_1 + "," +
                                                //"        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT    AS " + strTotalAggregate + "," + //--2026/03/26
                                                "        COR_TRN_SALARY_DETAILS.CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT    AS " + strTotalAggregate + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_1B_DED_AMOUNT   AS " + strUnderSec80CCD_1B + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_2_DED_AMOUNT   AS " + strUnderSec80CCD_2 + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_SEC80D_DED_AMOUNT          AS " + strUnderSec80D + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_SEC80E_DED_AMOUNT           AS " + strUnderSec80E + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_SEC80G_DED_AMOUNT          AS " + strUnderSec80G + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_SEC80TTA_DED_AMOUNT       AS " + strUnderSec80TTA + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL                    AS " + strDeductionUnderChapterVIAUnderOtherSections + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_DED_TOTAL                            AS " + strGrossTotalDeductionUnderChapterVIA + "," +
                                                "        COR_TRN_SALARY_DETAILS.TOTAL_INCOME                               AS " + strTotalTaxableIncome + "," +
                                                //"        COR_TRN_SALARY_DETAILS.TAX_TOTAL_INCOME                        AS " + strIncomeTaxOnTotalIncome + "," +
                                                "        COR_TRN_SALARY_DETAILS.TAX_TOTAL_INCOME_B4_REBATE                        AS " + strIncomeTaxOnTotalIncome + "," +
                                                "        COR_TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT               AS " + strRebate + "," +
                                                "        COR_TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME                     AS " + strSalSurcharge + "," +
                                                "        COR_TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME                    AS " + strSalEducationCess + "," +
                                                "        COR_TRN_SALARY_DETAILS.US_89_LESS                                   AS " + strIncomeTaxReleifUnderSection89 + "," +
                                                "        COR_TRN_SALARY_DETAILS.TAX_PAYABLE                                  AS " + strNetTaxPayable + "," +
                                                "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                   AS " + strTotalTDSDeducted + "," +
                                                "        COR_TRN_SALARY_DETAILS.SHORTFALL_TAX                              AS " + strShortfallExcessDeductionOfTax + "," +
                                                "        COR_TRN_SALARY_DETAILS.TAXABLE_AMOUNT                          AS " + strCurrentEmployerSalary + "," +
                                                "        COR_TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT        AS " + strPreviousEmployerSalary + "," +
                                                "        COR_TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT    AS " + strCurrentEmployerTDS + "," +
                                                "        COR_TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL  AS " + strPreviousEmployerTDS + "," +
                                                "        " + cmnService.J_SQLDBFormat(strTaxDeductedAtHigherRateMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strWhetherTaxDeductedAtHigherRate + ", " +
                                                "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED + COR_TRN_SALARY_DETAILS.SUPER_ANN_TAX   AS " + strTDSIncludingSuperannuation + "," +
                                                "        " + cmnService.J_SQLDBFormat(strSUPER_ANN_YNMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strWhetherContributionsPaidByTrustees + ", " +
                                                "        COR_TRN_SALARY_DETAILS.SUPER_ANN_NAME                           AS " + strNameOfSuperannuationFund + "," +
                                                "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strFromDate + "," +
                                                "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strToDate + "," +
                                                "        " + cmnService.J_SQLDBFormat(strAmountOfContributionRepaidMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAmountOfContributionRepaid + ", " +
                                                "        " + cmnService.J_SQLDBFormat(strAverageRateOfDeductionMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAverageRateOfDeduction + ", " +
                                                "        " + cmnService.J_SQLDBFormat(strAmountOfTaxDeductedMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAmountOfTaxDeducted + ", " +
                                                "        COR_TRN_SALARY_DETAILS.SUPER_ANN_INCOME                           AS " + strSalGrossTotalIncome + "," +
                                                "        COR_TRN_SALARY_DETAILS.RENT_EXCEEDING_YN                          AS " + strWhetherRentPaymentExceeds1lakh + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_1_PAN                               AS " + strPANOfLandlord1 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_1_NAME                            AS " + strNameOfLandlord1 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_2_PAN                               AS " + strPANOfLandlord2 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_2_NAME                            AS " + strNameOfLandlord2 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_3_PAN                               AS " + strPANOfLandlord3 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_3_NAME                            AS " + strNameOfLandlord3 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_4_PAN                               AS " + strPANOfLandlord4 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_4_NAME                            AS " + strNameOfLandlord4 + "," +
                                                "        COR_TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER                AS " + strWhetherInterestpaidExceeds1lakh + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_1_PAN                                    AS " + strPANOfLender1 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_1_NAME                                 AS " + strNameOfLender1 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_2_PAN                                   AS " + strPANOfLender2 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_2_NAME                                AS " + strNameOfLender2 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_3_PAN                                  AS " + strPANOfLender3 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_3_NAME                               AS " + strNameOfLender3 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_4_PAN                                  AS " + strPANOfLender4 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_4_NAME                              AS " + strNameOfLender4 + " " + strQueryDDAnnexCorr +
                                                " FROM   COR_TRN_SALARY_DETAILS             " +
                                                " WHERE  COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                                                " ORDER  BY COR_TRN_SALARY_DETAILS.SL_NO    ";
                                    #endregion

                                }
                                //--------------------------------------------------
                                else
                                {
                                    // OLD Format (24Q-SALARY DETAILS)

                                    #region Variable Declaration
                                    strSalEmpSerialNo = "[Serial No (328)]";
                                    strSalPanOfEmp = "[PAN of the Employee (329)]";
                                    strNameOfEmp = "[Name of the Employee (330)]";
                                    strCategoryEmp = "[Category of the Employee (331)]";
                                    strMode = "[Mode]";
                                    strPeriodOfEmploymentFromDate = "[Period of employment : From Date (dd/mm/yyyy) (332)]";
                                    strPeriodOfEmploymentTODate = "[Period of employment : To Date (dd/mm/yyyy) (332)]";
                                    strTotalSalary = "[Total Salary(335)]";
                                    strGrossDeductionUnderSection16ii = "[Gross Deduction under section 16(ii)]";
                                    strGrossDeductionUnderSection16iii = "[Gross Deduction under section 16(iii)]";
                                    strGrossDeductionUnderSection16ia = "[Gross Deduction under section 16(ia)]";
                                    strGrossTotalDeductionUnderSection = "[Gross Total Deduction under section 16(ii) & 16(iii) & 16(ia)]";
                                    strIncomeChargeableUnderHeadSalaries = "[Income Chargeable under head Salaries(338)]";
                                    strIncomeOtherThanSalary = "[Income other than Salary (339)]";
                                    strGrossTotalIncome = "[Gross Total Income(340)]";
                                    strDeductionunderChapterVIAUnderSection80CCE = "[Deduction under Chapter VIA under section 80CCE]";
                                    strDeductionUnderChapterVIAUnderSection80CCF = "[Deduction under Chapter VIA under section 80CCF]";
                                    strDeductionUnderChapterVIAUnderOtherSections = "[Deduction under Chapter VIA under Other sections]";
                                    strGrossTotalDeductionUnderChapterVIA = "[Gross Total Deduction under chapter VIA (343)]";
                                    strTotalTaxableIncome = "[Total Taxable Income (344)]";
                                    strIncomeTaxOnTotalIncome = "[Income Tax on Total Income (345)]";
                                    strSalSurcharge = "[Surcharge]";
                                    strSalEducationCess = "[Education Cess (346)]";
                                    strIncomeTaxReleifUnderSection89 = "[Income Tax Releif under section 89 (347)]";
                                    strNetTaxPayable = "[Net Tax Payable (348)]";
                                    strTotalTDSDeducted = "[Total TDS Deducted (351)]";
                                    strShortfallExcessDeductionOfTax = "[Shortfall/Excess Deduction of Tax(352)]";
                                    strCurrentEmployerSalary = "[Current employer salary(333)]";
                                    strPreviousEmployerSalary = "[Previous employer salary(334)]";
                                    strCurrentEmployerTDS = "[Current employer TDS(349)]";
                                    strPreviousEmployerTDS = "[Previous employer TDS(350)]";
                                    strWhetherTaxDeductedAtHigherRate = "[Tax deducted at Higher rate due to non furnishing(353)]";
                                    strTDSIncludingSuperannuation = "[TDS including Superannuation]";
                                    strWhetherContributionsPaidByTrustees = "[Contributions paid by trustees approved Superan fund (Y/N)]";
                                    strNameOfSuperannuationFund = "[Name of Superannuation Fund]";
                                    strFromDate = "[From Date]";
                                    strToDate = "[To Date]";
                                    strAmountOfContributionRepaid = "[Amount of contribution repaid]";
                                    strAverageRateOfDeduction = "[Average rate of deduction]";
                                    strAmountOfTaxDeducted = "[Amount of Tax deducted]";
                                    strSalGrossTotalIncome = "[Gross Total Income]";
                                    strWhetherRentPaymentExceeds1lakh = "[Whether rent payment exceeds 1lakh during previous year(Y/N)]";
                                    strPANOfLandlord1 = "[PAN of landlord 1]";
                                    strNameOfLandlord1 = "[Name of landlord 1]";
                                    strPANOfLandlord2 = "[PAN of landlord 2]";
                                    strNameOfLandlord2 = "[Name of landlord 2]";
                                    strPANOfLandlord3 = "[PAN of landlord 3]";
                                    strNameOfLandlord3 = "[Name of landlord 3]";
                                    strPANOfLandlord4 = "[PAN of landlord 4]";
                                    strNameOfLandlord4 = "[Name of landlord 4]";
                                    strWhetherInterestpaidExceeds1lakh = "[Whether interest paid exceeds 1lakh (Y/N)]";
                                    strPANOfLender1 = "[PAN of lender 1]";
                                    strNameOfLender1 = "[Name of lender 1]";
                                    strPANOfLender2 = "[PAN of lender 2]";
                                    strNameOfLender2 = "[Name of lender 2]";
                                    strPANOfLender3 = "[PAN of lender 3]";
                                    strNameOfLender3 = "[Name of lender 3]";
                                    strPANOfLender4 = "[PAN of lender 4]";
                                    strNameOfLender4 = "[Name of lender 4]";
                                    #endregion

                                    #region Query                                    
                                        strQueryDD = " SELECT COR_TRN_SALARY_DETAILS.SL_NO                                     AS " + strSalEmpSerialNo + "," +
                                                "        COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN                                     AS " + strSalPanOfEmp + "," +
                                                "        COR_TRN_SALARY_DETAILS.EMPLOYEE_NAME                                    AS " + strNameOfEmp + "," +
                                                "        COR_TRN_SALARY_DETAILS.EMPLOYEE_CATEGORY                                AS " + strCategoryEmp + "," +
                                                //"        COR_TRN_SALARY_DETAILS.FROM_DATE                                  AS " + strPeriodOfEmploymentFromDate + "," +
                                                //"        COR_TRN_SALARY_DETAILS.TO_DATE                                    AS " + strPeriodOfEmploymentTODate + "," +
                                                "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPeriodOfEmploymentFromDate + "," +
                                                "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPeriodOfEmploymentTODate + "," +
                                                "        " + cmnService.J_SQLDBFormat(strModeMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strMode + "," +
                                                "        COR_TRN_SALARY_DETAILS.TS_BALANCE                                 AS " + strTotalSalary + "," +
                                                "        COR_TRN_SALARY_DETAILS.US_16_EA                                   AS " + strGrossDeductionUnderSection16ii + "," +
                                                "        COR_TRN_SALARY_DETAILS.US_16_TE                                   AS " + strGrossDeductionUnderSection16iii + "," +
                                                "        COR_TRN_SALARY_DETAILS.US_16_IA                                   AS " + strGrossDeductionUnderSection16ia + "," +
                                                "        COR_TRN_SALARY_DETAILS.US_16_AGGREGATE                            AS " + strGrossTotalDeductionUnderSection + "," +
                                                "        COR_TRN_SALARY_DETAILS.INCOME_CHARGEABLE                          AS " + strIncomeChargeableUnderHeadSalaries + "," +
                                                "        COR_TRN_SALARY_DETAILS.AIS_TOTAL                                  AS " + strIncomeOtherThanSalary + "," +
                                                "        COR_TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                         AS " + strGrossTotalIncome + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT             AS " + strDeductionunderChapterVIAUnderSection80CCE + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCG_DED_AMOUNT                   AS " + strDeductionUnderChapterVIAUnderSection80CCF + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL                         AS " + strDeductionUnderChapterVIAUnderOtherSections + "," +
                                                "        COR_TRN_SALARY_DETAILS.CVIA_DED_TOTAL                             AS " + strGrossTotalDeductionUnderChapterVIA + "," +
                                                "        COR_TRN_SALARY_DETAILS.TOTAL_INCOME                               AS " + strTotalTaxableIncome + "," +
                                                "        COR_TRN_SALARY_DETAILS.TAX_TOTAL_INCOME                           AS " + strIncomeTaxOnTotalIncome + "," +
                                                "        COR_TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME                          AS " + strSalSurcharge + "," +
                                                "        COR_TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME                         AS " + strSalEducationCess + "," +
                                                "        COR_TRN_SALARY_DETAILS.US_89_LESS                                 AS " + strIncomeTaxReleifUnderSection89 + "," +
                                                "        COR_TRN_SALARY_DETAILS.TAX_PAYABLE                                AS " + strNetTaxPayable + "," +
                                                "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                         AS " + strTotalTDSDeducted + "," +
                                                "        COR_TRN_SALARY_DETAILS.SHORTFALL_TAX                              AS " + strShortfallExcessDeductionOfTax + "," +
                                                "        COR_TRN_SALARY_DETAILS.TAXABLE_AMOUNT                             AS " + strCurrentEmployerSalary + "," +
                                                "        COR_TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT                    AS " + strPreviousEmployerSalary + "," +
                                                "        COR_TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT                  AS " + strCurrentEmployerTDS + "," +
                                                "        COR_TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL                AS " + strPreviousEmployerTDS + "," +
                                                "        " + cmnService.J_SQLDBFormat(strTaxDeductedAtHigherRateMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strWhetherTaxDeductedAtHigherRate + ", " +   //-- 26/02/2018 --
                                                                                                                                                                                                             //"        COR_TRN_SALARY_DETAILS.TAX_DEDUCTED_HIGHER_RATE                   AS " + strWhetherTaxDeductedAtHigherRate + "," +    //-- 26/02/2018 --
                                                "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED + COR_TRN_SALARY_DETAILS.SUPER_ANN_TAX   AS " + strTDSIncludingSuperannuation + "," +
                                                //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_YN                               AS " + strWhetherContributionsPaidByTrustees + "," +    //-- 24/02/2018 --                                      
                                                "        " + cmnService.J_SQLDBFormat(strSUPER_ANN_YNMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strWhetherContributionsPaidByTrustees + ", " +
                                                "        COR_TRN_SALARY_DETAILS.SUPER_ANN_NAME                             AS " + strNameOfSuperannuationFund + "," +
                                                //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE                        AS " + strFromDate + "," +
                                                //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE                          AS " + strToDate + "," +
                                                "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strFromDate + "," +
                                                "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strToDate + "," +
                                                "        " + cmnService.J_SQLDBFormat(strAmountOfContributionRepaidMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAmountOfContributionRepaid + ", " +   //-- 26/02/2018 --
                                                "        " + cmnService.J_SQLDBFormat(strAverageRateOfDeductionMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAverageRateOfDeduction + ", " +   //-- 26/02/2018 --
                                                "        " + cmnService.J_SQLDBFormat(strAmountOfTaxDeductedMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAmountOfTaxDeducted + ", " +   //-- 26/02/2018 --                                                                                                                                                                                     //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT                           AS " + strAmountOfContributionRepaid + "," +   //-- 24/02/2018 --                                      
                                                                                                                                                                                              //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_RATE                             AS " + strAverageRateOfDeduction + "," +
                                                                                                                                                                                              //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_TAX                              AS " + strAmountOfTaxDeducted + "," +
                                                "        COR_TRN_SALARY_DETAILS.SUPER_ANN_INCOME                           AS " + strSalGrossTotalIncome + "," +
                                                "        COR_TRN_SALARY_DETAILS.RENT_EXCEEDING_YN                          AS " + strWhetherRentPaymentExceeds1lakh + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_1_PAN                             AS " + strPANOfLandlord1 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_1_NAME                            AS " + strNameOfLandlord1 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_2_PAN                             AS " + strPANOfLandlord2 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_2_NAME                            AS " + strNameOfLandlord2 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_3_PAN                             AS " + strPANOfLandlord3 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_3_NAME                            AS " + strNameOfLandlord3 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_4_PAN                             AS " + strPANOfLandlord4 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LANDLORD_4_NAME                            AS " + strNameOfLandlord4 + "," +
                                                "        COR_TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER                    AS " + strWhetherInterestpaidExceeds1lakh + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_1_PAN                               AS " + strPANOfLender1 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_1_NAME                              AS " + strNameOfLender1 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_2_PAN                               AS " + strPANOfLender2 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_2_NAME                              AS " + strNameOfLender2 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_3_PAN                               AS " + strPANOfLender3 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_3_NAME                              AS " + strNameOfLender3 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_4_PAN                               AS " + strPANOfLender4 + "," +
                                                "        COR_TRN_SALARY_DETAILS.LENDER_4_NAME                              AS " + strNameOfLender4 + " " +
                                                " FROM   COR_TRN_SALARY_DETAILS             " +
                                                " WHERE  COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                                                " ORDER  BY COR_TRN_SALARY_DETAILS.SL_NO    ";
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                #region 24Q REGULAR/CORRECTION
                                //-- 20/02/2018 --
                                // REMARKS
                                //--------------------------------------------------------------------------------------------------
                                if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Remarks") == false) return;
                                //
                                if (WRITE_REMARKS_WORKSHEET(strExcelFilePath, "Remarks", strFormNo) == false) return;
                                //
                                prgBar.Value = prgBar.Value + 5;
                                //--------------------------------------------------------------------------------------------------
                                // SECTION
                                if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Section") == false) return;
                                //
                                if (WRITE_SECTION_WORKSHEET(strExcelFilePath, "Section", strFormNo) == false) return;
                                //
                                prgBar.Value = prgBar.Value + 5;
                                //--------------------------------------------------------------------------------------------------
                                //
                                // RECEIPT NO
                                if (CREATE_NEW_WORKSHEET(strExcelFilePath, "RECEIPT NO") == false) return;
                                //
                                if (rbnCorrectionReturn.Checked == true)
                                {
                                    if (WRITE_RECEIPT_NO_CORR_WORKSHEET(strExcelFilePath, "RECEIPT NO", lngBasicInfoID) == false) return;
                                }
                                else
                                {
                                    if (WRITE_RECEIPT_NO_REG_WORKSHEET(strExcelFilePath, "RECEIPT NO", lngBasicInfoID) == false) return;
                                }
                                //
                                prgBar.Value = prgBar.Value + 5;
                                //--------------------------------------------------------------------------------------------------
                                // EMPLOYEE DETAILS
                                if (CREATE_NEW_WORKSHEET(strExcelFilePath, "EMPLOYEE DETAILS") == false) return;
                                //
                                prgBar.Value = prgBar.Value + 5;
                                //
                                //--------------------------------------------------------------------------------------------------
                                //
                                // CHALLAN VARIABLE
                                strSerialNo = "[Running Serial No (301)]";
                                strSection = "[Section Code]";
                                strTDS = "[TDS (302)]";
                                strSurcharge = "[Surcharge]";
                                strEducationCess = "[Education Cess (303)]";
                                strInterest = "[Interest (304)]";
                                strFee = "[Fee (305)]";
                                strOther = "[Others (306)]";
                                strTotalTaxDeposited = "[Total Tax Deposited (307)]";
                                strBSRCode = "[BSR Code/24G Receipt No (309)]";
                                strDateOnTaxDeposited = "[Date on Tax Deposited (dd/mm/yyyy) (311)]";
                                strTransferVoucherChallanSerialNo = "[Transfer Voucher/Challan Serial No (310)]";
                                strTdsDeposited = "[Whether TDS Deposited by Book Entry (308)]";
                                strMinorHead = "[Minor head (312)]";
                                strChequeNo = "[Cheque No]";
                                strInterestAllocated = "[Interest Allocated]";
                                strOtherAllocated = "[Other Allocated]";
                                //
                                // DEDUCTEE
                                strDeducteeSerialNo = "[Serial No (313)]";
                                strChallanSerialRef = "[Challan Serial Reference (301)]";
                                strPanOfEmployee = "[PAN of the Employee (315)]";
                                strNameOfEmployee = "[Name of the Employee (316)]";
                                strSectionCode = "[Section Code (317)]";
                                strPaymentDate = "[Payment/Credit Date (dd/mm/yyyy) (318)]";
                                strDeductedDate = "[Deducted Date]";
                                strAmountPaid = "[Amount Paid/Credited (320)]";
                                strDeducteeTds = "[TDS (321)]";
                                strDeducteeSurcharge = "[Surcharge]";
                                strDeducteeEducationCess = "[Education Cess (322)]";
                                strTotalTaxDeducted = "[Total Tax Deducted (323)]";
                                strDeducteeTotalTaxDeposited = "[Total Tax Deposited (324)]";
                                strLowerDeduction = "[Reason for Non-deduction/Lower Deduction (326)]";
                                strCertificateNonDeduction = "[Certificate number for Lower/non deduction (327)]";
                                //--
                                //-- 2020/08/14
                                strEmployeeSerialNo = "";
                                strQueryDDAnnexReg = "";
                                strQueryDDAnnexCorr = "";
                                if (TDSMAN.Classes.TDSMAN.T_ShareReferenceNoF24Q == true)
                                {
                                    strEmployeeSerialNo = "[Employee Serial No]";
                                    strQueryDDAnnexReg = ",TRN_DEDUCTEE_DETAILS.EMPLOYEE_SERIAL_NO AS " + strEmployeeSerialNo;
                                    strQueryDDAnnexCorr = ",COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_REF AS " + strEmployeeSerialNo;
                                }
                                //--
                                //
                                if (intFinancialYearID >= T_FinancialYearID.F2013_14ID)
                                {
                                    // 21/12/2017
                                    // DEDUCTEE
                                    if (rbnRegularReturn.Checked == true)
                                        strQueryDD = " Select  TRN_DEDUCTEE_DETAILS.SL_NO                    AS " + strDeducteeSerialNo + "," +
                                                    "         TRN_CHALLAN.SL_NO                             AS " + strChallanSerialRef + "," +
                                                    "         MST_EMPLOYEE.EMPLOYEE_PAN                     AS " + strPanOfEmployee + "," +
                                                    "         MST_EMPLOYEE.EMPLOYEE_NAME                    AS " + strNameOfEmployee + "," +
                                                    "         MST_SECTION.SECTION_NO                        AS " + strSectionCode + "," +
                                                    //"         TRN_DEDUCTEE_DETAILS.PAYMENT_DATE             AS " + strPaymentDate + "," +
                                                    "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT           AS " + strAmountPaid + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.TAX_AMOUNT               AS " + strDeducteeTds + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT         AS " + strDeducteeSurcharge + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.CESS_AMOUNT              AS " + strDeducteeEducationCess + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT             AS " + strTotalTaxDeducted + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT     AS " + strDeducteeTotalTaxDeposited + "," +
                                                    "         MST_REASON.REASON                             AS " + strLowerDeduction + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO           AS " + strCertificateNonDeduction + "," +
                                                    "         " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + " " +
                                                     strQueryDDAnnexReg +
                                                    " FROM (((( TRN_DEDUCTEE_DETAILS        " +
                                                    "           LEFT JOIN TRN_CHALLAN       " +
                                                    "           ON  TRN_DEDUCTEE_DETAILS.CHALLAN_ID  = TRN_CHALLAN.CHALLAN_ID)       " +
                                                    "           LEFT JOIN   MST_EMPLOYEE    " +
                                                    "           ON   TRN_DEDUCTEE_DETAILS.PARTY_ID  =  MST_EMPLOYEE.EMPLOYEE_ID )    " +
                                                    "           LEFT JOIN      MST_SECTION  " +
                                                    "           ON   TRN_DEDUCTEE_DETAILS.SECTION_ID   =    MST_SECTION.SECTION_ID)  " +
                                                    "           LEFT JOIN      MST_REASON   " +
                                                    "           ON    TRN_DEDUCTEE_DETAILS.REASON_ID   =   MST_REASON.REASON_ID)     " +
                                                    " WHERE     TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID      =   " + lngBasicInfoID + " " +
                                                    " ORDER BY  TRN_CHALLAN.SL_NO,          " +
                                                    "           TRN_DEDUCTEE_DETAILS.SL_NO  ";
                                    else if (rbnCorrectionReturn.Checked == true)
                                        strQueryDD = " Select  COR_TRN_DEDUCTEE_DETAILS.SL_NO               AS " + strDeducteeSerialNo + "," +
                                                    "         COR_TRN_CHALLAN.SL_NO                         AS " + strChallanSerialRef + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN         AS " + strPanOfEmployee + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME        AS " + strNameOfEmployee + "," +
                                                    "         MST_SECTION.SECTION_NO                        AS " + strSectionCode + "," +
                                                    //"         TRN_DEDUCTEE_DETAILS.PAYMENT_DATE             AS " + strPaymentDate + "," +
                                                    "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT           AS " + strAmountPaid + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.TAX_AMOUNT               AS " + strDeducteeTds + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT         AS " + strDeducteeSurcharge + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.CESS_AMOUNT              AS " + strDeducteeEducationCess + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT             AS " + strTotalTaxDeducted + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT     AS " + strDeducteeTotalTaxDeposited + "," +
                                                    "         MST_REASON.REASON                                 AS " + strLowerDeduction + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO           AS " + strCertificateNonDeduction + "," +
                                                    "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + "  " + strQueryDDAnnexCorr +
                                                    " FROM  (((COR_TRN_DEDUCTEE_DETAILS        " +
                                                    "           LEFT JOIN COR_TRN_CHALLAN       " +
                                                    "           ON  COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID  = COR_TRN_CHALLAN.TRN_CHALLAN_ID)       " +
                                                    "           LEFT JOIN      MST_SECTION  " +
                                                    "           ON   COR_TRN_DEDUCTEE_DETAILS.SECTION_ID   =    MST_SECTION.SECTION_ID)  " +
                                                    "           LEFT JOIN      MST_REASON   " +
                                                    "           ON    COR_TRN_DEDUCTEE_DETAILS.REASON_ID   =   MST_REASON.REASON_ID)     " +
                                                    " WHERE     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID    =   " + lngBasicInfoID + " " +
                                                    " ORDER BY  COR_TRN_CHALLAN.SL_NO,          " +
                                                    "           COR_TRN_DEDUCTEE_DETAILS.SL_NO  ";
                                    //--------------------------------------------------------------
                                }
                                else
                                {
                                    // 21/12/2017
                                    // DEDUCTEE
                                    if (rbnRegularReturn.Checked == true)
                                        strQueryDD = " Select  TRN_DEDUCTEE_DETAILS.SL_NO                    AS " + strDeducteeSerialNo + "," +
                                                    "         TRN_CHALLAN.SL_NO                             AS " + strChallanSerialRef + "," +
                                                    "         MST_EMPLOYEE.EMPLOYEE_PAN                     AS " + strPanOfEmployee + "," +
                                                    "         MST_EMPLOYEE.EMPLOYEE_NAME                    AS " + strNameOfEmployee + "," +
                                                    "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT           AS " + strAmountPaid + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.TAX_AMOUNT               AS " + strDeducteeTds + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT         AS " + strDeducteeSurcharge + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.CESS_AMOUNT              AS " + strDeducteeEducationCess + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT             AS " + strTotalTaxDeducted + "," +
                                                    "         TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT     AS " + strDeducteeTotalTaxDeposited + "," +
                                                    "         MST_REASON.REASON                             AS " + strLowerDeduction + ", " +
                                                    "         TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO           AS " + strCertificateNonDeduction + "," +
                                                    "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + " " +
                                                    " FROM  ((( TRN_DEDUCTEE_DETAILS        " +
                                                    "           LEFT JOIN TRN_CHALLAN       " +
                                                    "           ON  TRN_DEDUCTEE_DETAILS.CHALLAN_ID  = TRN_CHALLAN.CHALLAN_ID)       " +
                                                    "           LEFT JOIN   MST_EMPLOYEE    " +
                                                    "           ON   TRN_DEDUCTEE_DETAILS.PARTY_ID  =  MST_EMPLOYEE.EMPLOYEE_ID )    " +
                                                    "           LEFT JOIN      MST_REASON   " +
                                                    "           ON    TRN_DEDUCTEE_DETAILS.REASON_ID   =   MST_REASON.REASON_ID)     " +
                                                    " WHERE     TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID      =   " + lngBasicInfoID + " " +
                                                    " ORDER BY  TRN_CHALLAN.SL_NO,          " +
                                                    "           TRN_DEDUCTEE_DETAILS.SL_NO  ";
                                    else if (rbnCorrectionReturn.Checked == true)
                                        strQueryDD = " Select  COR_TRN_DEDUCTEE_DETAILS.SL_NO                   AS " + strDeducteeSerialNo + "," +
                                                    "         COR_TRN_CHALLAN.SL_NO                                 AS " + strChallanSerialRef + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN             AS " + strPanOfEmployee + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME            AS " + strNameOfEmployee + "," +
                                                    "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT           AS " + strAmountPaid + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.TAX_AMOUNT               AS " + strDeducteeTds + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT         AS " + strDeducteeSurcharge + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.CESS_AMOUNT              AS " + strDeducteeEducationCess + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT             AS " + strTotalTaxDeducted + "," +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT     AS " + strDeducteeTotalTaxDeposited + "," +
                                                    "         MST_REASON.REASON                                 AS " + strLowerDeduction + ", " +
                                                    "         COR_TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO           AS " + strCertificateNonDeduction + "," +
                                                    "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + "  " +
                                                    " FROM  (( COR_TRN_DEDUCTEE_DETAILS        " +
                                                    "           LEFT JOIN COR_TRN_CHALLAN       " +
                                                    "           ON  COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID  = COR_TRN_CHALLAN.TRN_CHALLAN_ID)       " +
                                                    "           LEFT JOIN      MST_REASON   " +
                                                    "           ON    COR_TRN_DEDUCTEE_DETAILS.REASON_ID   =   MST_REASON.REASON_ID)     " +
                                                    " WHERE     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID      =   " + lngBasicInfoID + " " +
                                                    " ORDER BY  COR_TRN_CHALLAN.SL_NO,          " +
                                                    "           COR_TRN_DEDUCTEE_DETAILS.SL_NO  ";
                                    //--------------------------------------------------------------
                                }
                                #endregion
                            }
                        }
                        #endregion
                        //
                        // FORM 26Q
                        #region FORM 26Q
                        else if (strFormNo == T_FormNo.F26Q)
                        {
                            //-- 20/02/2018 --
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Remarks") == false) return;
                            //
                            if (WRITE_REMARKS_WORKSHEET(strExcelFilePath,"Remarks", strFormNo) == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5; 
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Deductee Code") == false) return;
                            //
                            if (WRITE_DEDUCTEE_CODE_WORKSHEET(strExcelFilePath, "Deductee Code") == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5; 
                            //-----------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Section") == false) return;
                            //
                            if (WRITE_SECTION_WORKSHEET(strExcelFilePath,"Section", strFormNo) == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5;
                            // RECEIPT NO
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "RECEIPT NO") == false) return;
                            //
                            if (rbnRegularReturn.Checked == true)
                            {
                                if (WRITE_RECEIPT_NO_REG_WORKSHEET(strExcelFilePath, "RECEIPT NO", lngBasicInfoID) == false) return;
                            }
                            else
                            {
                                if (WRITE_RECEIPT_NO_CORR_WORKSHEET(strExcelFilePath, "RECEIPT NO", lngBasicInfoID) == false) return;
                            }
                            //
                            //-----------------------------------------------------------------------------------------------------------
                            //
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "DEDUCTEE DETAILS") == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5; 
                            //-----------------------------------------------------------------------------------------------------------
                            //
                            // CHALLAN VARIABLE
                            strSerialNo = "[Running Serial No (401)]";
                            strSection = "[Section-Code]";
                            strTDS = "[TDS (402)]";
                            strSurcharge = "[Surcharge]";
                            strEducationCess = "[Education Cess]";
                            strInterest = "[Interest (403)]";
                            strFee = "[Fee (404)]";
                            strOther = "[Others (405)]";
                            strTotalTaxDeposited = "[Total Tax Deposited (406)]";
                            strBSRCode = "[BSR Code/24G Receipt No (408)]";
                            strDateOnTaxDeposited = "[Date on Tax Deposited (dd/mm/yyyy) (410)]";
                            strTransferVoucherChallanSerialNo = "[Transfer Voucher/Challan Serial No (409)]";
                            strTdsDeposited = "[Whether TDS Deposited by Book Entry (407)]";
                            strMinorHead = "[Minor head (411)]";
                            strChequeNo = "[Cheque No]";
                            strInterestAllocated = "[Interest Allocated]";
                            strOtherAllocated = "[Other Allocated]";
                             //
                             // DEDUCTEE
                             strDeducteeSerialNo = "[Deductee Serial No (414)]";
                            strChallanSerialRef = "[Challan Serial Reference (401)]";
                            strDeducteeCode = "[Deductee Code (414)]";
                            strPanOfEmployee = "[PAN of the Deductee (415)]";
                            strNameOfEmployee = "[Name of the Deductee (416)]";
                            strSectionCode = "[Section Code (417)]";
                            strPaymentDate = "[Payment/Credit Date (dd/mm/yyyy) (418)]";
                            strDeductedDate = "[Deducted Date]";
                            strAmountPaid = "[Amount Paid/Credited (419)]";
                            strDeducteeTds = "[TDS]";
                            strDeducteeSurcharge = "[Surcharge]";
                            strDeducteeEducationCess = "[Education Cess]";
                            strTotalTaxDeducted = "[Total Tax Deducted (420)]";
                            strDeducteeTotalTaxDeposited = "[Total Tax Deposited (421)]";
                            strRateAtDeducted = "[Rate at which deducted (423)]";
                            strLowerDeduction = "[Reason for Non-deduction/Lower Deduction (424)]";
                            strCertificateNonDeduction = "[Certificate number for Lower/non deduction (425)]";
                            //--2023/09/11
                            strSec194NAmountExcess1Cr = "[Amount Excess 1Cr - Sec194N]";
                            strSec194NFAmount20LExcess1Cr = "[Section 194NF; ITR not Filed (1 : 20L-1Cr 2 : excess of 1Cr)]";
                            strSec194NCAmountExcess3Cr = "[Amount Excess 3Cr - Sec194NC]";
                            strSec194N_FTAmount20LExcess3Cr = "[Section 194N-FT: ITR not Filed (1 : 20L-3Cr; 2 : excess of 3Cr)]";
                            //-- 2020/08/14
                            strEmployeeSerialNo = "";
                            strQueryDDAnnexReg = "";
                            strQueryDDAnnexCorr = "";
                            if (TDSMAN.Classes.TDSMAN.T_ShareReferenceNoF26Q == true)
                            {
                                strEmployeeSerialNo = "[Deductee Serial No]";
                                strQueryDDAnnexReg = ",TRN_DEDUCTEE_DETAILS.EMPLOYEE_SERIAL_NO AS " + strEmployeeSerialNo;
                                strQueryDDAnnexCorr = ",COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_REF AS " + strEmployeeSerialNo;
                            }
                            //--
                            if (intFinancialYearID >= T_FinancialYearID.F2013_14ID)
                            {
                                // 21/12/2017
                                // DEDUCTEE
                                if (rbnRegularReturn.Checked == true)
                                    strQueryDD = " Select    TRN_DEDUCTEE_DETAILS.SL_NO                   AS " + strDeducteeSerialNo + "," +
                                            "           TRN_CHALLAN.SL_NO                            AS " + strChallanSerialRef + "," +
                                            "           MST_DEDUCTEE.DEDUCTEE_CODE                   AS " + strDeducteeCode + "," +
                                            "           MST_DEDUCTEE.DEDUCTEE_PAN                    AS " + strPanOfEmployee + "," +
                                            "           MST_DEDUCTEE.DEDUCTEE_NAME                   AS " + strNameOfEmployee + "," +
                                            "           MST_SECTION.SECTION_NO                       AS " + strSectionCode + "," +
                                            //"           TRN_DEDUCTEE_DETAILS.PAYMENT_DATE            AS " + strPaymentDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                            "           TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT          AS " + strAmountPaid + "," +
                                            "           TRN_DEDUCTEE_DETAILS.TAX_AMOUNT              AS " + strDeducteeTds + "," +
                                            "           TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT        AS " + strDeducteeSurcharge + "," +
                                            "           TRN_DEDUCTEE_DETAILS.CESS_AMOUNT             AS " + strDeducteeEducationCess + "," +
                                            "           TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT            AS " + strTotalTaxDeducted + "," +
                                            "           TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT    AS " + strDeducteeTotalTaxDeposited + "," +
                                            "           TRN_DEDUCTEE_DETAILS.RATE                    AS " + strRateAtDeducted + "," +
                                            "           MST_REASON.REASON                            AS " + strLowerDeduction + "," +
                                            "           TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO          AS " + strCertificateNonDeduction + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + "," +
                                            "           TRN_DEDUCTEE_DETAILS.SEC194N_EXCESS_1CRORE_AMOUNT                              AS " + strSec194NAmountExcess1Cr + "," +
                                            "        " + cmnService.J_SQLDBFormat(strSECTION_194NFMatrix, J_SQLColFormat.Case_End) + " AS " + strSec194NFAmount20LExcess1Cr + ", " +
                                            "           TRN_DEDUCTEE_DETAILS.SEC194NC_EXCESS_3CRORE                                    AS " + strSec194NCAmountExcess3Cr + "," +
                                            "        " + cmnService.J_SQLDBFormat(strSEC194NFTMatrix, J_SQLColFormat.Case_End) + "     AS " + strSec194N_FTAmount20LExcess3Cr + " " +
                                            "           " + strQueryDDAnnexReg + 
                                            " FROM (((( TRN_DEDUCTEE_DETAILS       " +
                                            "           LEFT JOIN TRN_CHALLAN      " +
                                            "           ON  TRN_DEDUCTEE_DETAILS.CHALLAN_ID  = TRN_CHALLAN.CHALLAN_ID)        " +
                                            "           LEFT JOIN    MST_DEDUCTEE  " +
                                            "           ON   TRN_DEDUCTEE_DETAILS.PARTY_ID  =   MST_DEDUCTEE.DEDUCTEE_ID )    " +
                                            "           LEFT JOIN      MST_SECTION " +
                                            "           ON   TRN_DEDUCTEE_DETAILS.SECTION_ID   =    MST_SECTION.SECTION_ID)   " +
                                            "           LEFT JOIN      MST_REASON  " +
                                            "           ON    TRN_DEDUCTEE_DETAILS.REASON_ID   =   MST_REASON.REASON_ID)      " +
                                            " WHERE     TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID      =  " + lngBasicInfoID + " " +
                                            " ORDER  BY TRN_CHALLAN.SL_NO,             " +
                                            "           TRN_DEDUCTEE_DETAILS.SL_NO     ";
                                else if (rbnCorrectionReturn.Checked == true)
                                    strQueryDD = " Select    COR_TRN_DEDUCTEE_DETAILS.SL_NO              AS " + strDeducteeSerialNo + "," +
                                            "           COR_TRN_CHALLAN.SL_NO                            AS " + strChallanSerialRef + "," +
                                            "           COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE           AS " + strDeducteeCode + "," +
                                            "           COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN            AS " + strPanOfEmployee + "," +
                                            "           COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME           AS " + strNameOfEmployee + "," +
                                            "           MST_SECTION.SECTION_NO                           AS " + strSectionCode + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                            "           COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT          AS " + strAmountPaid + "," +
                                            "           COR_TRN_DEDUCTEE_DETAILS.TAX_AMOUNT              AS " + strDeducteeTds + "," +
                                            "           COR_TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT        AS " + strDeducteeSurcharge + "," +
                                            "           COR_TRN_DEDUCTEE_DETAILS.CESS_AMOUNT             AS " + strDeducteeEducationCess + "," +
                                            "           COR_TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT            AS " + strTotalTaxDeducted + "," +
                                            "           COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT    AS " + strDeducteeTotalTaxDeposited + "," +
                                            "           COR_TRN_DEDUCTEE_DETAILS.RATE                    AS " + strRateAtDeducted + "," +
                                            "           MST_REASON.REASON                            AS " + strLowerDeduction + "," +
                                            "           COR_TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO          AS " + strCertificateNonDeduction + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + " " +
                                            strQueryDDAnnexCorr +
                                            " FROM ((( COR_TRN_DEDUCTEE_DETAILS       " +
                                            "           LEFT JOIN COR_TRN_CHALLAN      " +
                                            "           ON  COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID  = COR_TRN_CHALLAN.TRN_CHALLAN_ID)        " +
                                            "           LEFT JOIN      MST_SECTION " +
                                            "           ON   COR_TRN_DEDUCTEE_DETAILS.SECTION_ID   =    MST_SECTION.SECTION_ID)   " +
                                            "           LEFT JOIN      MST_REASON  " +
                                            "           ON    COR_TRN_DEDUCTEE_DETAILS.REASON_ID   =   MST_REASON.REASON_ID)      " +
                                            " WHERE     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID      =  " + lngBasicInfoID + " " +
                                            " ORDER  BY COR_TRN_CHALLAN.SL_NO,             " +
                                            "           COR_TRN_DEDUCTEE_DETAILS.SL_NO     ";
                            }
                            else
                            {
                                //-----------------------------------------------------------  
                                // 21/12/2017
                                // DEDUCTEE
                                if (rbnRegularReturn.Checked == true)
                                    strQueryDD = " Select      TRN_DEDUCTEE_DETAILS.SL_NO                   AS " + strDeducteeSerialNo + "," +
                                              "           TRN_CHALLAN.SL_NO                            AS " + strChallanSerialRef + "," +
                                              "           MST_DEDUCTEE.DEDUCTEE_CODE                   AS " + strDeducteeCode + "," +
                                              "           MST_DEDUCTEE.DEDUCTEE_PAN                    AS " + strPanOfEmployee + "," +
                                              "           MST_DEDUCTEE.DEDUCTEE_NAME                   AS " + strNameOfEmployee + "," +
                                    //"           MST_SECTION.SECTION_NO                       AS " + strSectionCode + "," +
                                              //"           TRN_DEDUCTEE_DETAILS.PAYMENT_DATE            AS " + strPaymentDate + "," +
                                              "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                              "           TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT          AS " + strAmountPaid + "," +
                                              "           TRN_DEDUCTEE_DETAILS.TAX_AMOUNT              AS " + strDeducteeTds + "," +
                                              "           TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT        AS " + strDeducteeSurcharge + "," +
                                              "           TRN_DEDUCTEE_DETAILS.CESS_AMOUNT             AS " + strDeducteeEducationCess + "," +
                                              "           TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT            AS " + strTotalTaxDeducted + "," +
                                              "           TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT    AS " + strDeducteeTotalTaxDeposited + "," +
                                              "           TRN_DEDUCTEE_DETAILS.RATE                    AS " + strRateAtDeducted + "," +
                                              "           MST_REASON.REASON                            AS " + strLowerDeduction + "," +
                                              "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + " " +
                                              //"           TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO          AS " + strCertificateNonDeduction + " " +
                                              " FROM  ((( TRN_DEDUCTEE_DETAILS       " +
                                              "           LEFT JOIN TRN_CHALLAN      " +
                                              "           ON  TRN_DEDUCTEE_DETAILS.CHALLAN_ID  = TRN_CHALLAN.CHALLAN_ID)        " +
                                              "           LEFT JOIN    MST_DEDUCTEE  " +
                                              "           ON   TRN_DEDUCTEE_DETAILS.PARTY_ID  =   MST_DEDUCTEE.DEDUCTEE_ID )    " +
                                    //"           LEFT JOIN      MST_SECTION " +
                                    //"           ON   TRN_DEDUCTEE_DETAILS.SECTION_ID   =    MST_SECTION.SECTION_ID)   " +
                                              "           LEFT JOIN      MST_REASON  " +
                                              "           ON    TRN_DEDUCTEE_DETAILS.REASON_ID   =   MST_REASON.REASON_ID)      " +
                                              " WHERE     TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID      =  " + lngBasicInfoID + " " +
                                              " ORDER  BY TRN_CHALLAN.SL_NO,             " +
                                              "           TRN_DEDUCTEE_DETAILS.SL_NO     ";
                                else if (rbnCorrectionReturn.Checked == true)
                                    strQueryDD = " Select      COR_TRN_DEDUCTEE_DETAILS.SL_NO              AS " + strDeducteeSerialNo + "," +
                                              "           COR_TRN_CHALLAN.SL_NO                            AS " + strChallanSerialRef + "," +
                                              "           COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE           AS " + strDeducteeCode + "," +
                                              "           COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN            AS " + strPanOfEmployee + "," +
                                              "           COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME           AS " + strNameOfEmployee + "," +
                                              "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                              "           COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT          AS " + strAmountPaid + "," +
                                              "           COR_TRN_DEDUCTEE_DETAILS.TAX_AMOUNT              AS " + strDeducteeTds + "," +
                                              "           COR_TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT        AS " + strDeducteeSurcharge + "," +
                                              "           COR_TRN_DEDUCTEE_DETAILS.CESS_AMOUNT             AS " + strDeducteeEducationCess + "," +
                                              "           COR_TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT            AS " + strTotalTaxDeducted + "," +
                                              "           COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT    AS " + strDeducteeTotalTaxDeposited + "," +
                                              "           COR_TRN_DEDUCTEE_DETAILS.RATE                    AS " + strRateAtDeducted + "," +
                                              "           MST_REASON.REASON                            AS " + strLowerDeduction + "," +
                                              "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + " " +
                                              " FROM  (( COR_TRN_DEDUCTEE_DETAILS       " +
                                              "           LEFT JOIN COR_TRN_CHALLAN      " +
                                              "           ON  COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID  = COR_TRN_CHALLAN.TRN_CHALLAN_ID)        " +
                                              "           LEFT JOIN      MST_REASON  " +
                                              "           ON    COR_TRN_DEDUCTEE_DETAILS.REASON_ID   =   MST_REASON.REASON_ID)      " +
                                              " WHERE     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID   =  " + lngBasicInfoID + " " +
                                              " ORDER  BY COR_TRN_CHALLAN.SL_NO,             " +
                                              "           COR_TRN_DEDUCTEE_DETAILS.SL_NO     ";
                                //-------------------------------------------------------------
                            }
                        }
                        #endregion
                        //
                        // FORM 27Q
                        #region FORM 27Q
                        else if (strFormNo == T_FormNo.F27Q)
                        {
                            //-- 20/02/2018 --
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Remarks") == false) return;
                            //
                            if (WRITE_REMARKS_WORKSHEET(strExcelFilePath,"Remarks", strFormNo) == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5; 
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Deductee Code") == false) return;
                            //
                            if (WRITE_DEDUCTEE_CODE_WORKSHEET(strExcelFilePath, "Deductee Code") == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5; 
                            //-----------------------------------------------------------------------------------------------------------
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (CREATE_NEW_WORKSHEET(strExcelFilePath, "TDS Rate Act Codes") == false) return;
                                //
                                if (WRITE_TDS_RATE_ACT_WORKSHEET(strExcelFilePath, "TDS Rate Act Codes") == false) return;
                                //
                                prgBar.Value = prgBar.Value + 5; 
                                //-----------------------------------------------------------------------------------------------------------
                                if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Country Codes") == false) return;
                                //
                                if (WRITE_COUNTRY_WORKSHEET(strExcelFilePath, "Country Codes") == false) return;
                                //
                                prgBar.Value = prgBar.Value + 5; 
                                //-----------------------------------------------------------------------------------------------------------
                                if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Remittance Codes") == false) return;
                                //
                                if (WRITE_REMITTANCE_WORKSHEET(strExcelFilePath, "Remittance Codes") == false) return;
                                //
                                prgBar.Value = prgBar.Value + 5; 
                            }
                            //---------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Section") == false) return;
                            //
                            if (WRITE_SECTION_WORKSHEET(strExcelFilePath,"Section", strFormNo) == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5;
                            // RECEIPT NO
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "RECEIPT NO") == false) return;
                            //
                            if (rbnRegularReturn.Checked == true)
                            {
                                if (WRITE_RECEIPT_NO_REG_WORKSHEET(strExcelFilePath, "RECEIPT NO", lngBasicInfoID) == false) return;
                            }
                            else
                            {
                                if (WRITE_RECEIPT_NO_CORR_WORKSHEET(strExcelFilePath, "RECEIPT NO", lngBasicInfoID) == false) return;
                            }
                            //
                            //---------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "DEDUCTEE DETAILS") == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5; 
                            //---------------------------------------------------------------------------------------------------------
                            //
                            // CHALLAN VARIABLE
                            strSerialNo = "[Running Serial No (701)]";
                            strSection = "[Section-Code]";
                            strTDS = "[TDS (702)]";
                            strSurcharge = "[Surcharge (703)]";
                            strEducationCess = "[Education Cess (704)]";
                            strInterest = "[Interest (705)]";
                            strFee = "[Fee (706)]";
                            strOther = "[Others (707)]";
                            strTotalTaxDeposited = "[Total Tax Deposited (708)]";
                            strBSRCode = "[BSR Code / 24G Receipt No (710)]";
                            strDateOnTaxDeposited = "[Date on Tax Deposited (dd/mm/yyyy) (712)]";
                            strTransferVoucherChallanSerialNo = "[Transfer Voucher/Challan Serial No (711)]";
                            strTdsDeposited = "[Whether TDS Deposited by Book Entry (709)]";
                            strMinorHead = "[Minor head (713)]";
                            strChequeNo = "[Cheque No]";
                            strInterestAllocated = "[Interest Allocated]";
                            strOtherAllocated = "[Other Allocated]";
                            //
                            // DEDUCTEE DETAILS
                            strDeducteeSerialNo = "[Deductee Serial No (714)]";
                            strChallanSerialRef = "[Challan Serial Reference (701)]";
                            strDeducteeCode = "[Deductee Code (716)]";
                            strPanOfEmployee = "[PAN of the Deductee (717)]";
                            strNameOfEmployee = "[Name of the Deductee (718)]";
                            strSectionCode = "[Section Code (719)]";
                            strPaymentDate = "[Payment/Credit Date (dd/mm/yyyy) (727)]";
                            strDeductedDate = "[Deducted Date]";
                            strAmountPaid = "[Amount Paid/Credited (721)]";
                            strDeducteeTds = "[TDS (722)]";
                            strDeducteeSurcharge = "[Surcharge (723)]";
                            strDeducteeEducationCess = "[Education Cess (724)]";
                            strTotalTaxDeducted = "[Total Tax Deducted (725)]";
                            strDeducteeTotalTaxDeposited = "[Total Tax Deposited (726)]";
                            strRateAtDeducted = "[Rate at which deducted (728)]";
                            strLowerDeduction = "[Reason for Non-deduction/Lower Deduction (729)]";
                            strCertificateNonDeduction = "[Certificate number for Lower/non deduction (730)]";
                            strTDSActCode = "[TDS Rate Act Code (731)]";
                            strRemitanceCode = "[Remittance code (732)]";
                            strAcknowledgmentNo = "[Acknowledgment No Form 15CA (733)]";
                            strCountryCode = "[Country code (734)]";
                            strContactNo = "[Contact No (735)]";
                            strEmail = "[Email (736)]";
                            strTaxIdentificationNo = "[Tax Identification No (737)]";
                            strAddress = "[Address (738)]";
                            strGrossingUp = "[Grossing Up Indicator]";
                            //-- 2020/08/14
                            strEmployeeSerialNo = "";
                            strQueryDDAnnexReg = "";
                            strQueryDDAnnexCorr = "";
                            if (TDSMAN.Classes.TDSMAN.T_ShareReferenceNoF27Q == true)
                            {
                                strEmployeeSerialNo = "[Deductee Serial No]";
                                strQueryDDAnnexReg = ",TRN_DEDUCTEE_DETAILS.EMPLOYEE_SERIAL_NO AS " + strEmployeeSerialNo;
                                strQueryDDAnnexCorr = ",COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_REF AS " + strEmployeeSerialNo;
                            }
                            //--
                            //--2023/09/13
                            strSec194NAmountExcess1Cr = "[Amount Excess 1Cr - Sec194N]";
                            strSec194NFAmount20LExcess1Cr = "[Section 194NF; ITR not Filed (1 : 20L-1Cr 2 : excess of 1Cr)]";
                            strSec194NCAmountExcess3Cr = "[Amount Excess 3Cr - Sec194NC]";
                            strSec194N_FTAmount20LExcess3Cr = "[Section 194N-FT: ITR not Filed (1 : 20L-3Cr; 2 : excess of 3Cr)]";
                            strOptingOutOf115BAC = "[Opting out of taxation regime u/s 115BAC(1A)-(Y/N)]";
                            //----------------------------------------------------------
                            //
                            if (intFinancialYearID >= T_FinancialYearID.F2013_14ID)
                            {
                                // DEDUCTEE
                                if (rbnRegularReturn.Checked == true)
                                    strQueryDD = " SELECT TRN_DEDUCTEE_DETAILS.SL_NO                        AS " + strDeducteeSerialNo + "," +
                                            "        TRN_CHALLAN.SL_NO                                 AS " + strChallanSerialRef + "," +
                                            "        MST_DEDUCTEE.DEDUCTEE_CODE                        AS " + strDeducteeCode + "," +
                                            "        MST_DEDUCTEE.DEDUCTEE_PAN                         AS " + strPanOfEmployee + "," +
                                            "        MST_DEDUCTEE.DEDUCTEE_NAME                        AS " + strNameOfEmployee + "," +
                                            "        MST_SECTION.SECTION_NO                            AS " + strSectionCode + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                            "        TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT               AS " + strAmountPaid + "," +
                                            "        TRN_DEDUCTEE_DETAILS.TAX_AMOUNT                   AS " + strDeducteeTds + "," +
                                            "        TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT             AS " + strDeducteeSurcharge + "," +
                                            "        TRN_DEDUCTEE_DETAILS.CESS_AMOUNT                  AS " + strDeducteeEducationCess + "," +
                                            "        TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT                 AS " + strTotalTaxDeducted + "," +
                                            "        TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT         AS " + strDeducteeTotalTaxDeposited + "," +
                                            "        TRN_DEDUCTEE_DETAILS.RATE                         AS " + strRateAtDeducted + "," +
                                            "        MST_REASON.REASON                                 AS " + strLowerDeduction + "," +
                                            "        TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO               AS " + strCertificateNonDeduction + "," +
                                            "        MST_TDS_APPLICABILITY.TDS_APPLICABILITY_CODE      AS " + strTDSActCode + "," +
                                            "        MST_REMITTANCE.REMITTANCE_CODE                    AS " + strRemitanceCode + "," +
                                            //"        \"'\" " + " + TRN_DEDUCTEE_DETAILS.UNIQUE_ACKN    AS " + strAcknowledgmentNo + "," +
                                            "        TRN_DEDUCTEE_DETAILS.UNIQUE_ACKN                  AS " + strAcknowledgmentNo + "," +
                                            "        MST_COUNTRY.COUNTRY_CODE                          AS " + strCountryCode + "," +
                                            "        TRN_DEDUCTEE_DETAILS.MOBILE_NO                    AS " + strContactNo + "," +
                                            "        TRN_DEDUCTEE_DETAILS.EMAIL                        AS " + strEmail + "," +
                                            "        TRN_DEDUCTEE_DETAILS.DEDUCTEE_TAX_ID              AS " + strTaxIdentificationNo + "," +
                                            "        TRN_DEDUCTEE_DETAILS.DEDUCTEE_ADDRESS             AS " + strAddress + "," +
                                            "        " + cmnService.J_SQLDBFormat(strGrossingUpIndicatorMatrix, J_SQLColFormat.Case_End) + " AS " + strGrossingUp + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + "," +
                                            "        TRN_DEDUCTEE_DETAILS.SEC194N_EXCESS_1CRORE_AMOUNT                                 AS " + strSec194NAmountExcess1Cr + "," +
                                            "        " + cmnService.J_SQLDBFormat(strSECTION_194NFMatrix, J_SQLColFormat.Case_End) + " AS " + strSec194NFAmount20LExcess1Cr + ", " +
                                            "        TRN_DEDUCTEE_DETAILS.SEC194NC_EXCESS_3CRORE                                       AS " + strSec194NCAmountExcess3Cr + "," +
                                            "        " + cmnService.J_SQLDBFormat(strSEC194NFTMatrix, J_SQLColFormat.Case_End) + "     AS " + strSec194N_FTAmount20LExcess3Cr + "," +
                                            "        " + cmnService.J_SQLDBFormat(strSECTION_115BAC_FLAGAnnexIMatrix, J_SQLColFormat.Case_End) + "     AS " + strOptingOutOf115BAC + " " +
                                            "        " + strQueryDDAnnexReg + 
                                            " FROM   ((((((( TRN_DEDUCTEE_DETAILS  " +
                                            "      LEFT JOIN TRN_CHALLAN           " +
                                            "             ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)                                 " +
                                            "      LEFT JOIN MST_DEDUCTEE          " +
                                            "             ON TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_DEDUCTEE.DEDUCTEE_ID )                                " +
                                            "      LEFT JOIN MST_SECTION            " +
                                            "             ON TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID)                                 " +
                                            "      LEFT JOIN MST_REASON              " +
                                            "             ON TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)                                    " +
                                            "      LEFT JOIN  MST_TDS_APPLICABILITY  " +
                                            "             ON TRN_DEDUCTEE_DETAILS.TDS_APPLICABILITY_ID  =  MST_TDS_APPLICABILITY.TDS_APPLICABILITY_ID) " +
                                            "      LEFT JOIN MST_REMITTANCE        " +
                                            "             ON TRN_DEDUCTEE_DETAILS.REMITTANCE_ID  =  MST_REMITTANCE.REMITTANCE_ID)                      " +
                                            "      LEFT JOIN MST_COUNTRY          " +
                                            "             ON TRN_DEDUCTEE_DETAILS.COUNTRY_ID   =   MST_COUNTRY.COUNTRY_ID)                             " +
                                            " WHERE    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                                            " ORDER BY TRN_CHALLAN.SL_NO,          " +
                                            "          TRN_DEDUCTEE_DETAILS.SL_NO  ";
                                else  if (rbnCorrectionReturn.Checked == true)
                                    strQueryDD = " SELECT COR_TRN_DEDUCTEE_DETAILS.SL_NO                   AS " + strDeducteeSerialNo + "," +
                                            "        COR_TRN_CHALLAN.SL_NO                                 AS " + strChallanSerialRef + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE                AS " + strDeducteeCode + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN                 AS " + strPanOfEmployee + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME                AS " + strNameOfEmployee + "," +
                                            "        MST_SECTION.SECTION_NO                                AS " + strSectionCode + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT               AS " + strAmountPaid + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.TAX_AMOUNT                   AS " + strDeducteeTds + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT             AS " + strDeducteeSurcharge + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.CESS_AMOUNT                  AS " + strDeducteeEducationCess + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT                 AS " + strTotalTaxDeducted + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT         AS " + strDeducteeTotalTaxDeposited + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.RATE                         AS " + strRateAtDeducted + "," +
                                            "        MST_REASON.REASON                                 AS " + strLowerDeduction + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO               AS " + strCertificateNonDeduction + "," +
                                            "        MST_TDS_APPLICABILITY.TDS_APPLICABILITY_CODE      AS " + strTDSActCode + "," +
                                            "        MST_REMITTANCE.REMITTANCE_CODE                    AS " + strRemitanceCode + "," +
                                            //"        \"'\" " + " + COR_TRN_DEDUCTEE_DETAILS.UNIQUE_ACKN    AS " + strAcknowledgmentNo + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.UNIQUE_ACKN                  AS " + strAcknowledgmentNo + "," +
                                            "        MST_COUNTRY.COUNTRY_CODE                          AS " + strCountryCode + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.MOBILE_NO                    AS " + strContactNo + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.EMAIL                        AS " + strEmail + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_TAX_ID              AS " + strTaxIdentificationNo + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_ADDRESS             AS " + strAddress + "," +
                                            "        " + cmnService.J_SQLDBFormat(strGrossingUpIndicatorMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strGrossingUp + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + " " + strQueryDDAnnexCorr +
                                            " FROM   (((((( COR_TRN_DEDUCTEE_DETAILS  " +
                                            "      LEFT JOIN COR_TRN_CHALLAN           " +
                                            "             ON COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID)                                 " +
                                           "      LEFT JOIN MST_SECTION            " +
                                            "             ON COR_TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID)                                 " +
                                            "      LEFT JOIN MST_REASON              " +
                                            "             ON COR_TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)                                    " +
                                            "      LEFT JOIN  MST_TDS_APPLICABILITY  " +
                                            "             ON COR_TRN_DEDUCTEE_DETAILS.TDS_APPLICABILITY_ID  =  MST_TDS_APPLICABILITY.TDS_APPLICABILITY_ID) " +
                                            "      LEFT JOIN MST_REMITTANCE        " +
                                            "             ON COR_TRN_DEDUCTEE_DETAILS.REMITTANCE_ID  =  MST_REMITTANCE.REMITTANCE_ID)                      " +
                                            "      LEFT JOIN MST_COUNTRY          " +
                                            "             ON COR_TRN_DEDUCTEE_DETAILS.COUNTRY_ID   =   MST_COUNTRY.COUNTRY_ID)                             " +
                                            " WHERE    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                                            " ORDER BY COR_TRN_CHALLAN.SL_NO,          " +
                                            "          COR_TRN_DEDUCTEE_DETAILS.SL_NO  ";

                            }
                            else
                            {
                                //-----------------------------------------------------------                             
                                //
                                // DEDUCTEE
                                if (rbnRegularReturn.Checked == true)
                                    strQueryDD = " SELECT TRN_DEDUCTEE_DETAILS.SL_NO                        AS " + strDeducteeSerialNo + "," +
                                            "        TRN_CHALLAN.SL_NO                                 AS " + strChallanSerialRef + "," +
                                            "        MST_DEDUCTEE.DEDUCTEE_CODE                        AS " + strDeducteeCode + "," +
                                            "        MST_DEDUCTEE.DEDUCTEE_PAN                         AS " + strPanOfEmployee + "," +
                                            "        MST_DEDUCTEE.DEDUCTEE_NAME                        AS " + strNameOfEmployee + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                            "        TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT               AS " + strAmountPaid + "," +
                                            "        TRN_DEDUCTEE_DETAILS.TAX_AMOUNT                   AS " + strDeducteeTds + "," +
                                            "        TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT             AS " + strDeducteeSurcharge + "," +
                                            "        TRN_DEDUCTEE_DETAILS.CESS_AMOUNT                  AS " + strDeducteeEducationCess + "," +
                                            "        TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT                 AS " + strTotalTaxDeducted + "," +
                                            "        TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT         AS " + strDeducteeTotalTaxDeposited + "," +
                                            "        TRN_DEDUCTEE_DETAILS.RATE                         AS " + strRateAtDeducted + "," +
                                            "        MST_REASON.REASON                                 AS " + strLowerDeduction + "," +
                                            "        " + cmnService.J_SQLDBFormat(strGrossingUpIndicatorMatrix, J_SQLColFormat.Case_End) + " AS " + strGrossingUp + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + " " +
                                            " FROM      (((  TRN_DEDUCTEE_DETAILS  " +
                                            "      LEFT JOIN TRN_CHALLAN           " +
                                            "             ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)                                 " +
                                            "      LEFT JOIN MST_DEDUCTEE          " +
                                            "             ON TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_DEDUCTEE.DEDUCTEE_ID )                                " +
                                            "      LEFT JOIN MST_REASON              " +
                                            "             ON TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)                                    " +
                                            " WHERE    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                                            " ORDER BY TRN_CHALLAN.SL_NO,          " +
                                            "          TRN_DEDUCTEE_DETAILS.SL_NO  ";
                                else if (rbnCorrectionReturn.Checked == true)
                                    strQueryDD = " SELECT COR_TRN_DEDUCTEE_DETAILS.SL_NO                   AS " + strDeducteeSerialNo + "," +
                                            "        COR_TRN_CHALLAN.SL_NO                                 AS " + strChallanSerialRef + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE                AS " + strDeducteeCode + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN                 AS " + strPanOfEmployee + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME                AS " + strNameOfEmployee + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT               AS " + strAmountPaid + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.TAX_AMOUNT                   AS " + strDeducteeTds + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT             AS " + strDeducteeSurcharge + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.CESS_AMOUNT                  AS " + strDeducteeEducationCess + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT                 AS " + strTotalTaxDeducted + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT         AS " + strDeducteeTotalTaxDeposited + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.RATE                         AS " + strRateAtDeducted + "," +
                                            "        MST_REASON.REASON                                 AS " + strLowerDeduction + "," +
                                            "        " + cmnService.J_SQLDBFormat(strGrossingUpIndicatorMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strGrossingUp + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + " " +
                                            " FROM      ((  COR_TRN_DEDUCTEE_DETAILS  " +
                                            "      LEFT JOIN COR_TRN_CHALLAN           " +
                                            "             ON COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID)                                 " +
                                            "      LEFT JOIN MST_REASON              " +
                                            "             ON COR_TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)                                    " +
                                            " WHERE    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                                            " ORDER BY COR_TRN_CHALLAN.SL_NO,          " +
                                            "          COR_TRN_DEDUCTEE_DETAILS.SL_NO  ";
                            }
                        }
                        #endregion
                        //
                        // FORM 27EQ
                        #region FORM 27EQ
                        else if (strFormNo == T_FormNo.F27EQ)
                        {
                            //-- 20/02/2018 --
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Remarks") == false) return;
                            //
                            if (WRITE_REMARKS_WORKSHEET(strExcelFilePath,"Remarks", strFormNo) == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5; 
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Deductee Code") == false) return;
                            //
                            if (WRITE_DEDUCTEE_CODE_WORKSHEET(strExcelFilePath, "Deductee Code") == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5; 
                            //-----------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Collection Code") == false) return;
                            //
                            if (WRITE_SECTION_WORKSHEET(strExcelFilePath,"Collection Code", strFormNo) == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5;
                            //------------------------------------------------------------------------------------------------------  
                            // RECEIPT NO
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "RECEIPT NO") == false) return;
                            //
                            if (rbnRegularReturn.Checked == true)
                            {
                                if (WRITE_RECEIPT_NO_REG_WORKSHEET(strExcelFilePath, "RECEIPT NO", lngBasicInfoID) == false) return;
                            }
                            else
                            {
                                if (WRITE_RECEIPT_NO_CORR_WORKSHEET(strExcelFilePath, "RECEIPT NO", lngBasicInfoID) == false) return;
                            }
                            //    
                            //
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "DEDUCTEE DETAILS") == false) return;
                            //
                            //prgBar.Value = prgBar.Value + 5; 
                            //------------------------------------------------------------------------------------------------------
                            //
                            // CHALLAN VARIABLE
                            strSerialNo = "[Running Serial No (651)]";
                            strSection = "[Section-Code]";
                            strTDS = "[TCS (652)]";
                            strSurcharge = "[Surcharge (653)]";
                            strEducationCess = "[Education Cess (654)]";
                            strInterest = "[Interest (655)]";
                            strFee = "[Fee (656)]";
                            strOther = "[Others (657)]";
                            strTotalTaxDeposited = "[Total Tax Deposited (658)]";
                            strBSRCode = "[BSR Code / 24G Receipt No (660)]";
                            strDateOnTaxDeposited = "[Date on Tax Deposited (dd/mm/yyyy) (662)]";
                            strTransferVoucherChallanSerialNo = "[Transfer Voucher/Challan Serial No (661)]";
                            strTdsDeposited = "[Whether TDS Deposited by Book Entry (659)]";
                            strMinorHead = "[Minor head (663)]";
                            strChequeNo = "[Cheque No]";
                            strInterestAllocated = "[Interest Allocated]";
                            strOtherAllocated = "[Other Allocated]";
                            //

                            // DEDUCTEE DETAILS
                            strDeducteeSerialNo = "[Party Serial No (664)]";
                            strChallanSerialRef = "[Challan Serial Reference (651)]";
                            strDeducteeCode = "[Party Code (666)]";
                            strPanOfEmployee = "[PAN of the Deductee (667)]";
                            strNameOfEmployee = "[Name of the Party (668)]";
                            strSectionCode = "[Section Code (672)]";
                            strPaymentDate = "[Payment/Debit Date (dd/mm/yyyy) (671)]";
                            strDeductedDate = "[Deducted Date]";
                            strAmountPaid = "[Amount Paid/Debited (670)]";
                            strDeducteeTds = "[TCS (673)]";
                            strDeducteeSurcharge = "[Surcharge (674)]";
                            strDeducteeEducationCess = "[Education Cess (675)]";
                            strTotalTaxDeducted = "[Total Tax Collected (676)]";
                            strDeducteeTotalTaxDeposited = "[Total Tax Deposited (677)]";
                            strRateAtDeducted = "[Rate at which Collected (679)]";
                            strLowerDeduction = "[Reason for Non-Collection/Lower Collection (680)]";
                            strCertificateNonDeduction = "[Certificate number for Lower/non deduction (681)]";
                            strValueOfPurchase = "[Value of Purchase (669)]";
                            strNonResident = "[Non-Resident]";
                            strPermanentlyEstablished = "[Permanently Established]";
                            //--2023/09/13
                            strOptingOutOf115BAC = "[Opting out of taxation regime u/s 115BAC(1A)-(Y/N)]";
                            //
                            //----------------------------------------
                            //
                            if (intFinancialYearID >= T_FinancialYearID.F2013_14ID)
                            {
                                // DEDUCTEE
                                if (rbnRegularReturn.Checked == true)
                                    strQueryDD = " SELECT TRN_DEDUCTEE_DETAILS.SL_NO                       AS " + strDeducteeSerialNo + "," +
                                                "        TRN_CHALLAN.SL_NO                                 AS " + strChallanSerialRef + "," +
                                                "        MST_DEDUCTEE.DEDUCTEE_CODE                        AS " + strDeducteeCode + "," +
                                                "        MST_DEDUCTEE.DEDUCTEE_PAN                         AS " + strPanOfEmployee + "," +
                                                "        MST_DEDUCTEE.DEDUCTEE_NAME                        AS " + strNameOfEmployee + "," +
                                                "        MST_SECTION.SECTION_NO                            AS " + strSectionCode + "," +
                                                //"        TRN_DEDUCTEE_DETAILS.PAYMENT_DATE                 AS " + strPaymentDate + "," +
                                                "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                                "        TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT               AS " + strAmountPaid + "," +
                                                "        TRN_DEDUCTEE_DETAILS.TAX_AMOUNT                   AS " + strDeducteeTds + "," +
                                                "        TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT             AS " + strDeducteeSurcharge + "," +
                                                "        TRN_DEDUCTEE_DETAILS.CESS_AMOUNT                  AS " + strDeducteeEducationCess + "," +
                                                "        TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT                 AS " + strTotalTaxDeducted + "," +
                                                "        TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT         AS " + strDeducteeTotalTaxDeposited + "," +
                                                "        TRN_DEDUCTEE_DETAILS.RATE                         AS " + strRateAtDeducted + "," +
                                                "        MST_REASON.REASON                                 AS " + strLowerDeduction + "," +
                                                "        TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO               AS " + strCertificateNonDeduction + "," +
                                                //"        " + cmnService.J_SQLDBFormat(strValueOfPurchaseMatrix, J_SQLColFormat.Case_End) + " AS " + strValueOfPurchase + " " +
                                                "        TRN_DEDUCTEE_DETAILS.TOT_VALUE_PURCHASE           AS " + strValueOfPurchase + ", " +
                                                "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + "," +
                                                //-- 26/02/2018 --
                                                "        TRN_DEDUCTEE_DETAILS.NON_RESIDENT                 AS " + strNonResident + ", " +
                                                "        TRN_DEDUCTEE_DETAILS.PERMANENT_ESTABLISHMENT      AS " + strPermanentlyEstablished + "," +
                                                "        " + cmnService.J_SQLDBFormat(strSECTION_115BAC_FLAGAnnexIMatrix, J_SQLColFormat.Case_End) + "     AS " + strOptingOutOf115BAC + " " +
                                                " FROM      (((( TRN_DEDUCTEE_DETAILS  " +
                                                "      LEFT JOIN TRN_CHALLAN           " +
                                                "             ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)                                 " +
                                                "      LEFT JOIN MST_DEDUCTEE          " +
                                                "             ON TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_DEDUCTEE.DEDUCTEE_ID )                                " +
                                                "      LEFT JOIN MST_SECTION            " +
                                                "             ON TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID)                                 " +
                                                "      LEFT JOIN MST_REASON              " +
                                                "             ON TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)                                    " +
                                                " WHERE    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                                                " ORDER BY TRN_CHALLAN.SL_NO,          " +
                                                "          TRN_DEDUCTEE_DETAILS.SL_NO  ";
                                else if (rbnCorrectionReturn.Checked == true)
                                    strQueryDD = " SELECT COR_TRN_DEDUCTEE_DETAILS.SL_NO                      AS " + strDeducteeSerialNo + "," +
                                                "        COR_TRN_CHALLAN.SL_NO                                AS " + strChallanSerialRef + "," +
                                                "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE               AS " + strDeducteeCode + "," +
                                                "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN                AS " + strPanOfEmployee + "," +
                                                "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME               AS " + strNameOfEmployee + "," +
                                                "        MST_SECTION.SECTION_NO                               AS " + strSectionCode + "," +
                                                //"        COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE                 AS " + strPaymentDate + "," +
                                                "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                                "        COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT               AS " + strAmountPaid + "," +
                                                "        COR_TRN_DEDUCTEE_DETAILS.TAX_AMOUNT                   AS " + strDeducteeTds + "," +
                                                "        COR_TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT             AS " + strDeducteeSurcharge + "," +
                                                "        COR_TRN_DEDUCTEE_DETAILS.CESS_AMOUNT                  AS " + strDeducteeEducationCess + "," +
                                                "        COR_TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT                 AS " + strTotalTaxDeducted + "," +
                                                "        COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT         AS " + strDeducteeTotalTaxDeposited + "," +
                                                "        COR_TRN_DEDUCTEE_DETAILS.RATE                         AS " + strRateAtDeducted + "," +
                                                "        MST_REASON.REASON                                     AS " + strLowerDeduction + "," +
                                                "        COR_TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO               AS " + strCertificateNonDeduction + "," +
                                                //"        " + cmnService.J_SQLDBFormat(strValueOfPurchaseMatrix, J_SQLColFormat.Case_End) + " AS " + strValueOfPurchase + " " +
                                                "        COR_TRN_DEDUCTEE_DETAILS.TOT_VALUE_PURCHASE           AS " + strValueOfPurchase + "," +
                                                "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + ", " +
                                                //-- 26/02/2018 --
                                                "        COR_TRN_DEDUCTEE_DETAILS.NON_RESIDENT                 AS " + strNonResident + ", " +
                                                "        COR_TRN_DEDUCTEE_DETAILS.PERMANENT_ESTABLISHMENT      AS " + strPermanentlyEstablished + " " +
                                                " FROM      ((( COR_TRN_DEDUCTEE_DETAILS  " +
                                                "      LEFT JOIN COR_TRN_CHALLAN           " +
                                                "             ON COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID)                                 " +
                                                "      LEFT JOIN MST_SECTION            " +
                                                "             ON COR_TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID)                                 " +
                                                "      LEFT JOIN MST_REASON              " +
                                                "             ON COR_TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)                                  " +
                                                " WHERE    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                                                " ORDER BY COR_TRN_CHALLAN.SL_NO,          " +
                                                "          COR_TRN_DEDUCTEE_DETAILS.SL_NO  ";

                            }
                            else
                            {
                                // DEDUCTEE
                                if (rbnRegularReturn.Checked == true)
                                    strQueryDD = " SELECT TRN_DEDUCTEE_DETAILS.SL_NO                        AS " + strDeducteeSerialNo + "," +
                                            "        TRN_CHALLAN.SL_NO                                 AS " + strChallanSerialRef + "," +
                                            "        MST_DEDUCTEE.DEDUCTEE_CODE                        AS " + strDeducteeCode + "," +
                                            "        MST_DEDUCTEE.DEDUCTEE_PAN                         AS " + strPanOfEmployee + "," +
                                            "        MST_DEDUCTEE.DEDUCTEE_NAME                        AS " + strNameOfEmployee + "," +
                                    //"        MST_SECTION.SECTION_NO                            AS " + strSectionCode + "," +
                                            //"        TRN_DEDUCTEE_DETAILS.PAYMENT_DATE                 AS " + strPaymentDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                            "        TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT               AS " + strAmountPaid + "," +
                                            "        TRN_DEDUCTEE_DETAILS.TAX_AMOUNT                   AS " + strDeducteeTds + "," +
                                            "        TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT             AS " + strDeducteeSurcharge + "," +
                                            "        TRN_DEDUCTEE_DETAILS.CESS_AMOUNT                  AS " + strDeducteeEducationCess + "," +
                                            "        TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT                 AS " + strTotalTaxDeducted + "," +
                                            "        TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT         AS " + strDeducteeTotalTaxDeposited + "," +
                                            "        TRN_DEDUCTEE_DETAILS.RATE                         AS " + strRateAtDeducted + "," +
                                            "        MST_REASON.REASON                                 AS " + strLowerDeduction + "," +
                                    //"        TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO               AS " + strCertificateNonDeduction + "," +
                                            "        " + cmnService.J_SQLDBFormat(strValueOfPurchaseMatrix, J_SQLColFormat.Case_End) + " AS " + strValueOfPurchase + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + " " +

                                            " FROM       ((( TRN_DEDUCTEE_DETAILS  " +
                                            "      LEFT JOIN TRN_CHALLAN           " +
                                            "             ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)                                 " +
                                            "      LEFT JOIN MST_DEDUCTEE          " +
                                            "             ON TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_DEDUCTEE.DEDUCTEE_ID )                                " +
                                    //"      LEFT JOIN MST_SECTION            " +
                                    //"             ON TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID)                                 " +
                                            "      LEFT JOIN MST_REASON              " +
                                            "             ON TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)                                    " +

                                            " WHERE    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                                            " ORDER BY TRN_CHALLAN.SL_NO,          " +
                                            "          TRN_DEDUCTEE_DETAILS.SL_NO  ";
                                else if(rbnCorrectionReturn.Checked==true)
                                    strQueryDD = " SELECT COR_TRN_DEDUCTEE_DETAILS.SL_NO                        AS " + strDeducteeSerialNo + "," +
                                            "        COR_TRN_CHALLAN.SL_NO                                 AS " + strChallanSerialRef + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE                        AS " + strDeducteeCode + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN                         AS " + strPanOfEmployee + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME                        AS " + strNameOfEmployee + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPaymentDate + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT               AS " + strAmountPaid + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.TAX_AMOUNT                   AS " + strDeducteeTds + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT             AS " + strDeducteeSurcharge + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.CESS_AMOUNT                  AS " + strDeducteeEducationCess + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT                 AS " + strTotalTaxDeducted + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT         AS " + strDeducteeTotalTaxDeposited + "," +
                                            "        COR_TRN_DEDUCTEE_DETAILS.RATE                         AS " + strRateAtDeducted + "," +
                                            "        MST_REASON.REASON                                 AS " + strLowerDeduction + "," +
                                            "        " + cmnService.J_SQLDBFormat(strValueOfPurchaseMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strValueOfPurchase + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDeductedDate + " " +
                                            " FROM       (( COR_TRN_DEDUCTEE_DETAILS  " +
                                            "      LEFT JOIN COR_TRN_CHALLAN           " +
                                            "             ON COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID)                                 " +
                                            "      LEFT JOIN MST_REASON              " +
                                            "             ON COR_TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)                                    " +
                                            " WHERE    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                                            " ORDER BY COR_TRN_CHALLAN.SL_NO,          " +
                                            "          COR_TRN_DEDUCTEE_DETAILS.SL_NO  ";

                            }
                        }
                        #endregion
                        //
                        // FORM SALARY DETAILS
                        #region 24Q-SALARY DETAILS
                        else if (strFormNo == T_FormNo.F24QSalaryDetails)
                        {
                            //-- 20/02/2018 --
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "SALARY DETAILS") == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5;
                            //--------------------------------------------------------------------------------------
                            //
                            strSQL = "UPDATE TRN_SALARY_DETAILS SET SUPER_ANN_YN = '' WHERE SUPER_ANN_YN IS NULL AND BASIC_INFO_ID = " + lngBasicInfoID;
                            dmlService.J_ExecSql(strSQL);
                            //--
                            //-- Added By Abhishek Dey On 10/05/2019 --
                            if (intFinancialYearID >= T_FinancialYearID.F2018_19ID)
                            {
                                #region Variable Declaration 
                                // For New Format
                                strSalEmpSerialNo = "[Serial No]";
                                strSalPanOfEmp = "[PAN of the Employee]";
                                strNameOfEmp = "[Name of the Employee]";
                                strCategoryEmp = "[Category of the Employee]";
                                strPeriodOfEmploymentFromDate = "[Period of employment : From Date (dd/mm/yyyy)]";
                                strPeriodOfEmploymentTODate = "[Period of employment : To Date (dd/mm/yyyy)]";
                                strNewRegime = "[New Regime (Taxation u / s 115BAC) (Y / N)]";
                                //-- Added By Abhishek Dey On 10/05/2019 --
                                strSec17_1 = "[Sec 17(1)]";
                                strSec17_2 = "[Sec 17(2)]";
                                strSec17_3 = "[Sec 17(3)]";
                                //--------------------------------------------------
                                strTotalSalary = "[Total Salary]";
                                //-- Added By Abhishek Dey On 10/05/2019 --
                                strSec10_5 = "[Sec10(5)]";
                                strSec10_10 = "[Sec10(10)]";
                                strSec10_10A = "[Sec10(10A)]";
                                strSec10_10AA = "[Sec10(10AA)]";
                                strSec10_13A = "[Sec10(13A)]";
                                strSec10_14 = "[Sec10(14)]";
                                strOtherExemptionSec10 = "[Any Other exemption u/s Sec10]";
                                strBalance = "[Balance]";
                                //--------------------------------------------------
                                strGrossDeductionUnderSection16ii = "[Gross Deduction under section 16(ii)]";
                                strGrossDeductionUnderSection16iii = "[Gross Deduction under section 16(iii)]";
                                strGrossDeductionUnderSection16ia = "[Gross Deduction under section 16(ia)]";
                                strGrossTotalDeductionUnderSection = "[Gross Total Deduction under section 16(ii) & 16(iii) & 16(ia)]";
                                strIncomeChargeableUnderHeadSalaries = "[Income Chargeable under head Salaries]";
                                //-- Added By Abhishek Dey On 10/05/2019 --
                                strIncomeLossHousePropertyOfferedTDS = "[Income or loss - House Property offered for TDS]";
                                //--------------------------------------------------
                                strIncomeOtherThanSalary = "[Income-Other Sources offered for TDS]";
                                strGrossTotalIncome = "[Gross Total Income(340)]";
                                //-- Added By Abhishek Dey On 10/05/2019 --
                                strUnderSec80C = "[Deduction under Chapter VIA under sec 80C]";
                                strUnderSec80CCC = "[Deduction under Chapter VIA under sec 80CCC]";
                                strUnderSec80CCD_1 = "[Deduction under Chapter VIA under sec 80CCD(1)]";
                                strTotalAggregate = "[Total deduction u / s 80C 80CCC & 80CCD(1)]";
                                strUnderSec80CCD_1B = "[Deduction under Chapter VIA under sec 80CCD(1B)]";
                                strUnderSec80CCD_2 = "[Deduction under Chapter VIA under sec 80CCD(2)]";
                                strUnderSec80D = "[Deduction under Chapter VIA under sec 80D]";
                                strUnderSec80E = "[Deduction under Chapter VIA under sec 80E]";
                                strUnderSec80CCH = "[Deduction under Chapter VIA under sec 80CCH]";
                                strUnderSec80CCH_1 = "[Deduction under Chapter VIA under sec 80CCH(1)]";
                                strUnderSec80G = "[Deduction under Chapter VIA under sec 80G]";
                                strUnderSec80TTA = "[Deduction under Chapter VIA under sec 80TTA]";                                
                                strDeductionUnderChapterVIAUnderOtherSections = "[Deduction under Chapter VIA under Other sections]";
                                strGrossTotalDeductionUnderChapterVIA = "[Gross Total Deduction under chapter VIA]";
                                strTotalTaxableIncome = "[Total Taxable Income]";
                                strIncomeTaxOnTotalIncome = "[Income Tax on Total Income]";
                                strRebate = "[Rebate under section 87A]";
                                strSalSurcharge = "[Surcharge]";
                                strSalEducationCess = "[Education Cess]";
                                strIncomeTaxReleifUnderSection89 = "[Income Tax Releif under section 89]";
                                strNetTaxPayable = "[Net Tax Payable]";
                                strTotalTDSDeducted = "[Total TDS Deducted]";
                                strShortfallExcessDeductionOfTax = "[Shortfall/Excess Deduction of Tax]";
                                strCurrentEmployerSalary = "[Current employer salary]";
                                strPreviousEmployerSalary = "[Previous employer salary]";
                                strCurrentEmployerTDS = "[Current employer TDS]";
                                strPreviousEmployerTDS = "[Previous employer TDS]";
                                strWhetherTaxDeductedAtHigherRate = "[Tax deducted at Higher rate due to non furnishing]";
                                strTDSIncludingSuperannuation = "[TDS including Superannuation]";
                                strWhetherContributionsPaidByTrustees = "[Contributions paid by trustees approved Superan fund (Y/N)]";
                                strNameOfSuperannuationFund = "[Name of Superannuation Fund]";
                                strFromDate = "[From Date]";
                                strToDate = "[To Date]";
                                strAmountOfContributionRepaid = "[Amount of contribution repaid]";
                                strAverageRateOfDeduction = "[Average rate of deduction]";
                                strAmountOfTaxDeducted = "[Amount of Tax deducted]";
                                strSalGrossTotalIncome = "[Gross Total Income]";
                                strWhetherRentPaymentExceeds1lakh = "[Whether rent payment exceeds 1lakh during previous year(Y/N)]";
                                strPANOfLandlord1 = "[PAN of landlord 1]";
                                strNameOfLandlord1 = "[Name of landlord 1]";
                                strPANOfLandlord2 = "[PAN of landlord 2]";
                                strNameOfLandlord2 = "[Name of landlord 2]";
                                strPANOfLandlord3 = "[PAN of landlord 3]";
                                strNameOfLandlord3 = "[Name of landlord 3]";
                                strPANOfLandlord4 = "[PAN of landlord 4]";
                                strNameOfLandlord4 = "[Name of landlord 4]";
                                strWhetherInterestpaidExceeds1lakh = "[Whether interest paid exceeds 1lakh (Y/N)]";
                                strPANOfLender1 = "[PAN of lender 1]";
                                strNameOfLender1 = "[Name of lender 1]";
                                strPANOfLender2 = "[PAN of lender 2]";
                                strNameOfLender2 = "[Name of lender 2]";
                                strPANOfLender3 = "[PAN of lender 3]";
                                strNameOfLender3 = "[Name of lender 3]";
                                strPANOfLender4 = "[PAN of lender 4]";
                                strNameOfLender4 = "[Name of lender 4]";
                                //--------------------------------------------------      
                                //-- 2020/08/14
                                strEmployeeSerialNo = "";
                                strQueryDDAnnexReg = "";
                                strQueryDDAnnexCorr = "";
                                if (TDSMAN.Classes.TDSMAN.T_ShareReferenceNoF24Q == true)
                                {
                                    strEmployeeSerialNo = "[Employee Serial No]";
                                    strQueryDDAnnexReg = ",TRN_SALARY_DETAILS.EMPLOYEE_SERIAL_NO AS " + strEmployeeSerialNo;
                                    strQueryDDAnnexCorr = ",COR_TRN_SALARY_DETAILS.EMPLOYEE_SERIAL_NO AS " + strEmployeeSerialNo;
                                }
                                //--
                                #endregion
                                //-- Salary Details
                                #region Query New Format (24Q-SALARY DETAILS)
                                if (rbnRegularReturn.Checked==true)
                                    strQueryDD = " SELECT TRN_SALARY_DETAILS.SL_NO                             AS " + strSalEmpSerialNo + "," +
                                            "        MST_EMPLOYEE.EMPLOYEE_PAN                                 AS " + strSalPanOfEmp + "," +
                                            "        MST_EMPLOYEE.EMPLOYEE_NAME                                AS " + strNameOfEmp + "," +
                                            "        MST_EMPLOYEE.CATEGORY                                     AS " + strCategoryEmp + "," +
                                            //"        TRN_SALARY_DETAILS.FROM_DATE                                  AS " + strPeriodOfEmploymentFromDate + "," +
                                            //"        TRN_SALARY_DETAILS.TO_DATE                                    AS " + strPeriodOfEmploymentTODate + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPeriodOfEmploymentFromDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "     AS " + strPeriodOfEmploymentTODate + "," +
                                            "        " + cmnService.J_SQLDBFormat(strSECTION_115BAC_FLAGMatrix, J_SQLColFormat.Case_End) + " AS " + strNewRegime + ", " +
                                            "        TRN_SALARY_DETAILS.TS_GS_SEC_17_1                         AS " + strSec17_1 + "," +
                                            "        TRN_SALARY_DETAILS.TS_GS_SEC_17_2                         AS " + strSec17_2 + "," +
                                            "        TRN_SALARY_DETAILS.TS_GS_SEC_17_3                         AS " + strSec17_3 + "," +
                                            "        TRN_SALARY_DETAILS.TS_GS_TOTAL                            AS " + strTotalSalary + "," +
                                            "        TRN_SALARY_DETAILS.SEC10_5_AMOUNT                         AS " + strSec10_5 + "," +
                                            "        TRN_SALARY_DETAILS.SEC10_10_AMOUNT                        AS " + strSec10_10 + "," +
                                            "        TRN_SALARY_DETAILS.SEC10_10A_AMOUNT                       AS " + strSec10_10A + "," +
                                            "        TRN_SALARY_DETAILS.SEC10_10AA_AMOUNT                      AS " + strSec10_10AA + "," +
                                            "        TRN_SALARY_DETAILS.SEC10_13A_AMOUNT                       AS " + strSec10_13A + "," +
                                            "        TRN_SALARY_DETAILS.SEC10_14_AMOUNT                        AS " + strSec10_14 + "," +
                                            "        TRN_SALARY_DETAILS.TS_LA_TOTAL                            AS " + strOtherExemptionSec10 + "," + 
                                            "        TRN_SALARY_DETAILS.TS_BALANCE                             AS " + strBalance + "," +
                                            "        TRN_SALARY_DETAILS.US_16_EA                               AS " + strGrossDeductionUnderSection16ii + "," +
                                            "        TRN_SALARY_DETAILS.US_16_TE                               AS " + strGrossDeductionUnderSection16iii + "," +
                                            "        TRN_SALARY_DETAILS.US_16_IA                               AS " + strGrossDeductionUnderSection16ia + "," +
                                            "        TRN_SALARY_DETAILS.US_16_AGGREGATE                        AS " + strGrossTotalDeductionUnderSection + "," +
                                            "        TRN_SALARY_DETAILS.INCOME_CHARGEABLE                      AS " + strIncomeChargeableUnderHeadSalaries + "," +
                                            "        TRN_SALARY_DETAILS.AIS_ITEM_1                             AS " + strIncomeLossHousePropertyOfferedTDS + "," +
                                            "        TRN_SALARY_DETAILS.AIS_ITEM_2                             AS " + strIncomeOtherThanSalary + "," +
                                            "        TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                     AS " + strGrossTotalIncome + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80C_DED_TOTAL                  AS " + strUnderSec80C + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCC_DED_AMOUNT               AS " + strUnderSec80CCC + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCD_DED_AMOUNT               AS " + strUnderSec80CCD_1 + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT         AS " + strTotalAggregate + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCD_1B_DED_AMOUNT            AS " + strUnderSec80CCD_1B + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCD_2_DED_AMOUNT             AS " + strUnderSec80CCD_2 + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80D_DED_AMOUNT                 AS " + strUnderSec80D + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80E_DED_AMOUNT                 AS " + strUnderSec80E + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCH_DED_AMOUNT               AS " + strUnderSec80CCH + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCH_1_DED_AMOUNT             AS " + strUnderSec80CCH_1 + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80G_DED_AMOUNT                 AS " + strUnderSec80G + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80TTA_DED_AMOUNT               AS " + strUnderSec80TTA + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL                     AS " + strDeductionUnderChapterVIAUnderOtherSections + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_DED_TOTAL                         AS " + strGrossTotalDeductionUnderChapterVIA + "," +
                                            "        TRN_SALARY_DETAILS.TOTAL_INCOME                           AS " + strTotalTaxableIncome + "," +
                                            "        TRN_SALARY_DETAILS.TAX_TOTAL_INCOME_B4_REBATE             AS " + strIncomeTaxOnTotalIncome + "," +
                                            "        TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT                   AS " + strRebate + "," +
                                            "        TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME                      AS " + strSalSurcharge + "," +
                                            "        TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME                     AS " + strSalEducationCess + "," +
                                            "        TRN_SALARY_DETAILS.US_89_LESS                             AS " + strIncomeTaxReleifUnderSection89 + "," +
                                            "        TRN_SALARY_DETAILS.TAX_PAYABLE                            AS " + strNetTaxPayable + "," +
                                            "        TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                     AS " + strTotalTDSDeducted + "," +
                                            "        TRN_SALARY_DETAILS.SHORTFALL_TAX                          AS " + strShortfallExcessDeductionOfTax + "," +
                                            "        TRN_SALARY_DETAILS.TAXABLE_AMOUNT                         AS " + strCurrentEmployerSalary + "," +
                                            "        TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT                AS " + strPreviousEmployerSalary + "," +
                                            "        TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT              AS " + strCurrentEmployerTDS + "," +
                                            "        TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL            AS " + strPreviousEmployerTDS + "," +
                                            "        " + cmnService.J_SQLDBFormat(strTaxDeductedAtHigherRateMatrix, J_SQLColFormat.Case_End) + " AS " + strWhetherTaxDeductedAtHigherRate + ", " +                                                                                                                                                                                                  
                                            "        TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED + TRN_SALARY_DETAILS.SUPER_ANN_TAX   AS " + strTDSIncludingSuperannuation + "," +
                                            "        TRN_SALARY_DETAILS.SUPER_ANN_YN                           AS " + strWhetherContributionsPaidByTrustees + "," +                                     
                                            "        TRN_SALARY_DETAILS.SUPER_ANN_NAME                         AS " + strNameOfSuperannuationFund + "," +                                            
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strFromDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strToDate + "," +
                                            "        " + cmnService.J_SQLDBFormat(strAmountOfContributionRepaidMatrix, J_SQLColFormat.Case_End) + " AS " + strAmountOfContributionRepaid + ", " +   
                                            "        " + cmnService.J_SQLDBFormat(strAverageRateOfDeductionMatrix, J_SQLColFormat.Case_End) + " AS " + strAverageRateOfDeduction + ", " +   
                                            "        " + cmnService.J_SQLDBFormat(strAmountOfTaxDeductedMatrix, J_SQLColFormat.Case_End) + " AS " + strAmountOfTaxDeducted + ", " +                                                                                                                                                                                         
                                            "        TRN_SALARY_DETAILS.SUPER_ANN_INCOME                       AS " + strSalGrossTotalIncome + "," +
                                            "        TRN_SALARY_DETAILS.RENT_EXCEEDING_YN                      AS " + strWhetherRentPaymentExceeds1lakh + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_1_PAN                         AS " + strPANOfLandlord1 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_1_NAME                        AS " + strNameOfLandlord1 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_2_PAN                         AS " + strPANOfLandlord2 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_2_NAME                        AS " + strNameOfLandlord2 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_3_PAN                         AS " + strPANOfLandlord3 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_3_NAME                        AS " + strNameOfLandlord3 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_4_PAN                         AS " + strPANOfLandlord4 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_4_NAME                        AS " + strNameOfLandlord4 + "," +
                                            "        TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER                AS " + strWhetherInterestpaidExceeds1lakh + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_1_PAN                           AS " + strPANOfLender1 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_1_NAME                          AS " + strNameOfLender1 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_2_PAN                           AS " + strPANOfLender2 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_2_NAME                          AS " + strNameOfLender2 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_3_PAN                           AS " + strPANOfLender3 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_3_NAME                          AS " + strNameOfLender3 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_4_PAN                           AS " + strPANOfLender4 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_4_NAME                          AS " + strNameOfLender4 + " " + strQueryDDAnnexReg + 
                                            " FROM   TRN_SALARY_DETAILS             " +
                                            "        LEFT JOIN MST_EMPLOYEE         " +
                                            "        ON TRN_SALARY_DETAILS.EMPLOYEE_ID = MST_EMPLOYEE.EMPLOYEE_ID       " +
                                            " WHERE  TRN_SALARY_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                                            " ORDER  BY TRN_SALARY_DETAILS.SL_NO    ";
                                else if (rbnCorrectionReturn.Checked == true)
                                    strQueryDD = " SELECT COR_TRN_SALARY_DETAILS.SL_NO                      AS " + strSalEmpSerialNo + "," +
                                            "        COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN                                     AS " + strSalPanOfEmp + "," +
                                            "        COR_TRN_SALARY_DETAILS.EMPLOYEE_NAME                                  AS " + strNameOfEmp + "," +
                                            "        COR_TRN_SALARY_DETAILS.CATEGORY                                            AS " + strCategoryEmp + "," +
                                            //"        COR_TRN_SALARY_DETAILS.FROM_DATE                                  AS " + strPeriodOfEmploymentFromDate + "," +
                                            //"        COR_TRN_SALARY_DETAILS.TO_DATE                                    AS " + strPeriodOfEmploymentTODate + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPeriodOfEmploymentFromDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "     AS " + strPeriodOfEmploymentTODate + "," +
                                            "        " + cmnService.J_SQLDBFormat(strSECTION_115BAC_FLAGMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strNewRegime + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_1                        AS " + strSec17_1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_2                        AS " + strSec17_2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_3                        AS " + strSec17_3 + "," +
                                            //"        COR_TRN_SALARY_DETAILS.TS_GS_TOTAL                              AS " + strTotalSalary + "," +
                                            "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_1 + TS_GS_SEC_17_2 + TS_GS_SEC_17_3 AS " + strTotalSalary + "," +
                                            "        COR_TRN_SALARY_DETAILS.SEC10_5_AMOUNT                       AS " + strSec10_5 + "," +
                                            "        COR_TRN_SALARY_DETAILS.SEC10_10_AMOUNT                     AS " + strSec10_10 + "," +
                                            "        COR_TRN_SALARY_DETAILS.SEC10_10A_AMOUNT                   AS " + strSec10_10A + "," +
                                            "        COR_TRN_SALARY_DETAILS.SEC10_10AA_AMOUNT                 AS " + strSec10_10AA + "," +
                                            "        COR_TRN_SALARY_DETAILS.SEC10_13A_AMOUNT                   AS " + strSec10_13A + "," +
                                            "        COR_TRN_SALARY_DETAILS.SEC10_14_AMOUNT                   AS " + strSec10_14 + "," +
                                            //"        COR_TRN_SALARY_DETAILS.SEC10_TOTAL_AMOUNT                AS " + strOtherExemptionSec10 + "," +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_TOTAL                AS " + strOtherExemptionSec10 + "," + 
                                            "        COR_TRN_SALARY_DETAILS.TS_BALANCE                                AS " + strBalance + "," +
                                            "        COR_TRN_SALARY_DETAILS.US_16_EA                                    AS " + strGrossDeductionUnderSection16ii + "," +
                                            "        COR_TRN_SALARY_DETAILS.US_16_TE                                     AS " + strGrossDeductionUnderSection16iii + "," +
                                            "        COR_TRN_SALARY_DETAILS.US_16_IA                                     AS " + strGrossDeductionUnderSection16ia + "," +
                                            "        COR_TRN_SALARY_DETAILS.US_16_AGGREGATE                      AS " + strGrossTotalDeductionUnderSection + "," +
                                            "        COR_TRN_SALARY_DETAILS.INCOME_CHARGEABLE                  AS " + strIncomeChargeableUnderHeadSalaries + "," +
                                            "        COR_TRN_SALARY_DETAILS.AIS_ITEM_1                                 AS " + strIncomeLossHousePropertyOfferedTDS + "," +
                                            "        COR_TRN_SALARY_DETAILS.AIS_ITEM_2                                 AS " + strIncomeOtherThanSalary + "," +
                                            "        COR_TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                AS " + strGrossTotalIncome + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_DED_TOTAL            AS " + strUnderSec80C + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCC_DED_AMOUNT     AS " + strUnderSec80CCC + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_DED_AMOUNT     AS " + strUnderSec80CCD_1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT    AS " + strTotalAggregate + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_1B_DED_AMOUNT   AS " + strUnderSec80CCD_1B + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_2_DED_AMOUNT   AS " + strUnderSec80CCD_2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80D_DED_AMOUNT          AS " + strUnderSec80D + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80E_DED_AMOUNT           AS " + strUnderSec80E + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCH_DED_AMOUNT           AS " + strUnderSec80CCH + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCH_1_DED_AMOUNT           AS " + strUnderSec80CCH_1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80G_DED_AMOUNT          AS " + strUnderSec80G + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80TTA_DED_AMOUNT       AS " + strUnderSec80TTA + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL                    AS " + strDeductionUnderChapterVIAUnderOtherSections + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_DED_TOTAL                            AS " + strGrossTotalDeductionUnderChapterVIA + "," +
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_INCOME                               AS " + strTotalTaxableIncome + "," +
                                            "        COR_TRN_SALARY_DETAILS.TAX_TOTAL_INCOME                        AS " + strIncomeTaxOnTotalIncome + "," +
                                            "        COR_TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT               AS " + strRebate + "," +
                                            "        COR_TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME                     AS " + strSalSurcharge + "," +
                                            "        COR_TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME                    AS " + strSalEducationCess + "," +
                                            "        COR_TRN_SALARY_DETAILS.US_89_LESS                                   AS " + strIncomeTaxReleifUnderSection89 + "," +
                                            "        COR_TRN_SALARY_DETAILS.TAX_PAYABLE                                  AS " + strNetTaxPayable + "," +
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                   AS " + strTotalTDSDeducted + "," +
                                            "        COR_TRN_SALARY_DETAILS.SHORTFALL_TAX                              AS " + strShortfallExcessDeductionOfTax + "," +
                                            "        COR_TRN_SALARY_DETAILS.TAXABLE_AMOUNT                          AS " + strCurrentEmployerSalary + "," +
                                            "        COR_TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT        AS " + strPreviousEmployerSalary + "," +
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT    AS " + strCurrentEmployerTDS + "," +
                                            "        COR_TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL  AS " + strPreviousEmployerTDS + "," +
                                            "        " + cmnService.J_SQLDBFormat(strTaxDeductedAtHigherRateMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strWhetherTaxDeductedAtHigherRate + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED + COR_TRN_SALARY_DETAILS.SUPER_ANN_TAX   AS " + strTDSIncludingSuperannuation + "," +
                                            "        COR_TRN_SALARY_DETAILS.SUPER_ANN_YN                               AS " + strWhetherContributionsPaidByTrustees + "," +
                                            "        COR_TRN_SALARY_DETAILS.SUPER_ANN_NAME                           AS " + strNameOfSuperannuationFund + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strFromDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strToDate + "," +
                                            "        " + cmnService.J_SQLDBFormat(strAmountOfContributionRepaidMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAmountOfContributionRepaid + ", " +
                                            "        " + cmnService.J_SQLDBFormat(strAverageRateOfDeductionMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAverageRateOfDeduction + ", " +
                                            "        " + cmnService.J_SQLDBFormat(strAmountOfTaxDeductedMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAmountOfTaxDeducted + ", " +
                                            "        COR_TRN_SALARY_DETAILS.SUPER_ANN_INCOME                           AS " + strSalGrossTotalIncome + "," +
                                            "        COR_TRN_SALARY_DETAILS.RENT_EXCEEDING_YN                          AS " + strWhetherRentPaymentExceeds1lakh + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_1_PAN                               AS " + strPANOfLandlord1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_1_NAME                            AS " + strNameOfLandlord1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_2_PAN                               AS " + strPANOfLandlord2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_2_NAME                            AS " + strNameOfLandlord2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_3_PAN                               AS " + strPANOfLandlord3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_3_NAME                            AS " + strNameOfLandlord3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_4_PAN                               AS " + strPANOfLandlord4 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_4_NAME                            AS " + strNameOfLandlord4 + "," +
                                            "        COR_TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER                AS " + strWhetherInterestpaidExceeds1lakh + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_1_PAN                                    AS " + strPANOfLender1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_1_NAME                                 AS " + strNameOfLender1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_2_PAN                                   AS " + strPANOfLender2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_2_NAME                                AS " + strNameOfLender2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_3_PAN                                  AS " + strPANOfLender3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_3_NAME                               AS " + strNameOfLender3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_4_PAN                                  AS " + strPANOfLender4 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_4_NAME                              AS " + strNameOfLender4 + " " + strQueryDDAnnexCorr +
                                            " FROM   COR_TRN_SALARY_DETAILS             " +
                                            " WHERE  COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                                            " ORDER  BY COR_TRN_SALARY_DETAILS.SL_NO    ";
                                #endregion

                            }
                            //--------------------------------------------------
                            else
                            {
                                // OLD Format (24Q-SALARY DETAILS)
                                #region Variable Declaration
                                strSalEmpSerialNo = "[Serial No (328)]";
                                strSalPanOfEmp = "[PAN of the Employee (329)]";
                                strNameOfEmp = "[Name of the Employee (330)]";
                                strCategoryEmp = "[Category of the Employee (331)]";
                                strPeriodOfEmploymentFromDate = "[Period of employment : From Date (dd/mm/yyyy) (332)]";
                                strPeriodOfEmploymentTODate = "[Period of employment : To Date (dd/mm/yyyy) (332)]";                                
                                strTotalSalary = "[Total Salary(335)]";                               
                                strGrossDeductionUnderSection16ii = "[Gross Deduction under section 16(ii)]";
                                strGrossDeductionUnderSection16iii = "[Gross Deduction under section 16(iii)]";
                                strGrossDeductionUnderSection16ia = "[Gross Deduction under section 16(ia)]";
                                strGrossTotalDeductionUnderSection = "[Gross Total Deduction under section 16(ii) & 16(iii) & 16(ia)]";
                                strIncomeChargeableUnderHeadSalaries = "[Income Chargeable under head Salaries(338)]";                                
                                strIncomeOtherThanSalary = "[Income other than Salary (339)]";
                                strGrossTotalIncome = "[Gross Total Income(340)]";
                                strDeductionunderChapterVIAUnderSection80CCE = "[Deduction under Chapter VIA under section 80CCE]";
                                strDeductionUnderChapterVIAUnderSection80CCF = "[Deduction under Chapter VIA under section 80CCF]";
                                strDeductionUnderChapterVIAUnderOtherSections = "[Deduction under Chapter VIA under Other sections]";
                                strGrossTotalDeductionUnderChapterVIA = "[Gross Total Deduction under chapter VIA (343)]";
                                strTotalTaxableIncome = "[Total Taxable Income (344)]";
                                strIncomeTaxOnTotalIncome = "[Income Tax on Total Income (345)]";
                                strSalSurcharge = "[Surcharge]";
                                strSalEducationCess = "[Education Cess (346)]";
                                strIncomeTaxReleifUnderSection89 = "[Income Tax Releif under section 89 (347)]";
                                strNetTaxPayable = "[Net Tax Payable (348)]";
                                strTotalTDSDeducted = "[Total TDS Deducted (351)]";
                                strShortfallExcessDeductionOfTax = "[Shortfall/Excess Deduction of Tax(352)]";
                                strCurrentEmployerSalary = "[Current employer salary(333)]";
                                strPreviousEmployerSalary = "[Previous employer salary(334)]";
                                strCurrentEmployerTDS = "[Current employer TDS(349)]";
                                strPreviousEmployerTDS = "[Previous employer TDS(350)]";
                                strWhetherTaxDeductedAtHigherRate = "[Tax deducted at Higher rate due to non furnishing(353)]";
                                strTDSIncludingSuperannuation = "[TDS including Superannuation]";
                                strWhetherContributionsPaidByTrustees = "[Contributions paid by trustees approved Superan fund (Y/N)]";
                                strNameOfSuperannuationFund = "[Name of Superannuation Fund]";
                                strFromDate = "[From Date]";
                                strToDate = "[To Date]";
                                strAmountOfContributionRepaid = "[Amount of contribution repaid]";
                                strAverageRateOfDeduction = "[Average rate of deduction]";
                                strAmountOfTaxDeducted = "[Amount of Tax deducted]";
                                strSalGrossTotalIncome = "[Gross Total Income]";
                                strWhetherRentPaymentExceeds1lakh = "[Whether rent payment exceeds 1lakh during previous year(Y/N)]";
                                strPANOfLandlord1 = "[PAN of landlord 1]";
                                strNameOfLandlord1 = "[Name of landlord 1]";
                                strPANOfLandlord2 = "[PAN of landlord 2]";
                                strNameOfLandlord2 = "[Name of landlord 2]";
                                strPANOfLandlord3 = "[PAN of landlord 3]";
                                strNameOfLandlord3 = "[Name of landlord 3]";
                                strPANOfLandlord4 = "[PAN of landlord 4]";
                                strNameOfLandlord4 = "[Name of landlord 4]";
                                strWhetherInterestpaidExceeds1lakh = "[Whether interest paid exceeds 1lakh (Y/N)]";
                                strPANOfLender1 = "[PAN of lender 1]";
                                strNameOfLender1 = "[Name of lender 1]";
                                strPANOfLender2 = "[PAN of lender 2]";
                                strNameOfLender2 = "[Name of lender 2]";
                                strPANOfLender3 = "[PAN of lender 3]";
                                strNameOfLender3 = "[Name of lender 3]";
                                strPANOfLender4 = "[PAN of lender 4]";
                                strNameOfLender4 = "[Name of lender 4]";
                                #endregion

                                #region Query
                                if (rbnRegularReturn.Checked == true)
                                    strQueryDD = " SELECT TRN_SALARY_DETAILS.SL_NO                                     AS " + strSalEmpSerialNo + "," +
                                            "        MST_EMPLOYEE.EMPLOYEE_PAN                                     AS " + strSalPanOfEmp + "," +
                                            "        MST_EMPLOYEE.EMPLOYEE_NAME                                    AS " + strNameOfEmp + "," +
                                            "        MST_EMPLOYEE.CATEGORY                                         AS " + strCategoryEmp + "," +
                                            //"        TRN_SALARY_DETAILS.FROM_DATE                                  AS " + strPeriodOfEmploymentFromDate + "," +
                                            //"        TRN_SALARY_DETAILS.TO_DATE                                    AS " + strPeriodOfEmploymentTODate + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPeriodOfEmploymentFromDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPeriodOfEmploymentTODate + "," +
                                            "        TRN_SALARY_DETAILS.TS_BALANCE                                 AS " + strTotalSalary + "," +
                                            "        TRN_SALARY_DETAILS.US_16_EA                                   AS " + strGrossDeductionUnderSection16ii + "," +
                                            "        TRN_SALARY_DETAILS.US_16_TE                                   AS " + strGrossDeductionUnderSection16iii + "," +
                                            "        TRN_SALARY_DETAILS.US_16_IA                                   AS " + strGrossDeductionUnderSection16ia + "," +
                                            "        TRN_SALARY_DETAILS.US_16_AGGREGATE                            AS " + strGrossTotalDeductionUnderSection + "," +
                                            "        TRN_SALARY_DETAILS.INCOME_CHARGEABLE                          AS " + strIncomeChargeableUnderHeadSalaries + "," +
                                            "        TRN_SALARY_DETAILS.AIS_TOTAL                                  AS " + strIncomeOtherThanSalary + "," +
                                            "        TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                         AS " + strGrossTotalIncome + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT             AS " + strDeductionunderChapterVIAUnderSection80CCE + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCG_DED_AMOUNT                   AS " + strDeductionUnderChapterVIAUnderSection80CCF + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL                         AS " + strDeductionUnderChapterVIAUnderOtherSections + "," +
                                            "        TRN_SALARY_DETAILS.CVIA_DED_TOTAL                             AS " + strGrossTotalDeductionUnderChapterVIA + "," +
                                            "        TRN_SALARY_DETAILS.TOTAL_INCOME                               AS " + strTotalTaxableIncome + "," +
                                            "        TRN_SALARY_DETAILS.TAX_TOTAL_INCOME                           AS " + strIncomeTaxOnTotalIncome + "," +
                                            "        TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME                          AS " + strSalSurcharge + "," +
                                            "        TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME                         AS " + strSalEducationCess + "," +
                                            "        TRN_SALARY_DETAILS.US_89_LESS                                 AS " + strIncomeTaxReleifUnderSection89 + "," +
                                            "        TRN_SALARY_DETAILS.TAX_PAYABLE                                AS " + strNetTaxPayable + "," +
                                            "        TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                         AS " + strTotalTDSDeducted + "," +
                                            "        TRN_SALARY_DETAILS.SHORTFALL_TAX                              AS " + strShortfallExcessDeductionOfTax + "," +
                                            "        TRN_SALARY_DETAILS.TAXABLE_AMOUNT                             AS " + strCurrentEmployerSalary + "," +
                                            "        TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT                    AS " + strPreviousEmployerSalary + "," +
                                            "        TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT                  AS " + strCurrentEmployerTDS + "," +
                                            "        TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL                AS " + strPreviousEmployerTDS + "," +
                                            "        " + cmnService.J_SQLDBFormat(strTaxDeductedAtHigherRateMatrix, J_SQLColFormat.Case_End) + " AS " + strWhetherTaxDeductedAtHigherRate + ", " +   //-- 26/02/2018 --
                                                                                                                                                                                                     //"        TRN_SALARY_DETAILS.TAX_DEDUCTED_HIGHER_RATE                   AS " + strWhetherTaxDeductedAtHigherRate + "," +    //-- 26/02/2018 --
                                            "        TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED + TRN_SALARY_DETAILS.SUPER_ANN_TAX   AS " + strTDSIncludingSuperannuation + "," +
                                            "        TRN_SALARY_DETAILS.SUPER_ANN_YN                               AS " + strWhetherContributionsPaidByTrustees + "," +    //-- 24/02/2018 --                                      
                                            "        TRN_SALARY_DETAILS.SUPER_ANN_NAME                             AS " + strNameOfSuperannuationFund + "," +
                                            //"        TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE                        AS " + strFromDate + "," +
                                            //"        TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE                          AS " + strToDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strFromDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strToDate + "," +
                                            "        " + cmnService.J_SQLDBFormat(strAmountOfContributionRepaidMatrix, J_SQLColFormat.Case_End) + " AS " + strAmountOfContributionRepaid + ", " +   //-- 26/02/2018 --
                                            "        " + cmnService.J_SQLDBFormat(strAverageRateOfDeductionMatrix, J_SQLColFormat.Case_End) + " AS " + strAverageRateOfDeduction + ", " +   //-- 26/02/2018 --
                                            "        " + cmnService.J_SQLDBFormat(strAmountOfTaxDeductedMatrix, J_SQLColFormat.Case_End) + " AS " + strAmountOfTaxDeducted + ", " +   //-- 26/02/2018 --
                                                                                                                                                                                      //"        TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT                           AS " + strAmountOfContributionRepaid + "," +   //-- 24/02/2018 --                                      
                                                                                                                                                                                      //"        TRN_SALARY_DETAILS.SUPER_ANN_RATE                             AS " + strAverageRateOfDeduction + "," +
                                                                                                                                                                                      //"        TRN_SALARY_DETAILS.SUPER_ANN_TAX                              AS " + strAmountOfTaxDeducted + "," +
                                            "        TRN_SALARY_DETAILS.SUPER_ANN_INCOME                           AS " + strSalGrossTotalIncome + "," +
                                            "        TRN_SALARY_DETAILS.RENT_EXCEEDING_YN                          AS " + strWhetherRentPaymentExceeds1lakh + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_1_PAN                             AS " + strPANOfLandlord1 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_1_NAME                            AS " + strNameOfLandlord1 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_2_PAN                             AS " + strPANOfLandlord2 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_2_NAME                            AS " + strNameOfLandlord2 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_3_PAN                             AS " + strPANOfLandlord3 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_3_NAME                            AS " + strNameOfLandlord3 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_4_PAN                             AS " + strPANOfLandlord4 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_4_NAME                            AS " + strNameOfLandlord4 + "," +
                                            "        TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER                    AS " + strWhetherInterestpaidExceeds1lakh + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_1_PAN                               AS " + strPANOfLender1 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_1_NAME                              AS " + strNameOfLender1 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_2_PAN                               AS " + strPANOfLender2 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_2_NAME                              AS " + strNameOfLender2 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_3_PAN                               AS " + strPANOfLender3 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_3_NAME                              AS " + strNameOfLender3 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_4_PAN                               AS " + strPANOfLender4 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_4_NAME                              AS " + strNameOfLender4 + " " +
                                            " FROM   TRN_SALARY_DETAILS             " +
                                            "        LEFT JOIN MST_EMPLOYEE         " +
                                            "        ON TRN_SALARY_DETAILS.EMPLOYEE_ID = MST_EMPLOYEE.EMPLOYEE_ID       " +
                                            " WHERE  TRN_SALARY_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                                            " ORDER  BY TRN_SALARY_DETAILS.SL_NO    ";
                                else if (rbnCorrectionReturn.Checked == true)
                                    strQueryDD = " SELECT COR_TRN_SALARY_DETAILS.SL_NO                                     AS " + strSalEmpSerialNo + "," +
                                            "        COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN                                     AS " + strSalPanOfEmp + "," +
                                            "        COR_TRN_SALARY_DETAILS.EMPLOYEE_NAME                                    AS " + strNameOfEmp + "," +
                                            "        COR_TRN_SALARY_DETAILS.CATEGORY                                         AS " + strCategoryEmp + "," +
                                            //"        COR_TRN_SALARY_DETAILS.FROM_DATE                                  AS " + strPeriodOfEmploymentFromDate + "," +
                                            //"        COR_TRN_SALARY_DETAILS.TO_DATE                                    AS " + strPeriodOfEmploymentTODate + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPeriodOfEmploymentFromDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strPeriodOfEmploymentTODate + "," +
                                            "        COR_TRN_SALARY_DETAILS.TS_BALANCE                                 AS " + strTotalSalary + "," +
                                            "        COR_TRN_SALARY_DETAILS.US_16_EA                                   AS " + strGrossDeductionUnderSection16ii + "," +
                                            "        COR_TRN_SALARY_DETAILS.US_16_TE                                   AS " + strGrossDeductionUnderSection16iii + "," +
                                            "        COR_TRN_SALARY_DETAILS.US_16_IA                                   AS " + strGrossDeductionUnderSection16ia + "," +
                                            "        COR_TRN_SALARY_DETAILS.US_16_AGGREGATE                            AS " + strGrossTotalDeductionUnderSection + "," +
                                            "        COR_TRN_SALARY_DETAILS.INCOME_CHARGEABLE                          AS " + strIncomeChargeableUnderHeadSalaries + "," +
                                            "        COR_TRN_SALARY_DETAILS.AIS_TOTAL                                  AS " + strIncomeOtherThanSalary + "," +
                                            "        COR_TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                         AS " + strGrossTotalIncome + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT             AS " + strDeductionunderChapterVIAUnderSection80CCE + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCG_DED_AMOUNT                   AS " + strDeductionUnderChapterVIAUnderSection80CCF + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL                         AS " + strDeductionUnderChapterVIAUnderOtherSections + "," +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_DED_TOTAL                             AS " + strGrossTotalDeductionUnderChapterVIA + "," +
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_INCOME                               AS " + strTotalTaxableIncome + "," +
                                            "        COR_TRN_SALARY_DETAILS.TAX_TOTAL_INCOME                           AS " + strIncomeTaxOnTotalIncome + "," +
                                            "        COR_TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME                          AS " + strSalSurcharge + "," +
                                            "        COR_TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME                         AS " + strSalEducationCess + "," +
                                            "        COR_TRN_SALARY_DETAILS.US_89_LESS                                 AS " + strIncomeTaxReleifUnderSection89 + "," +
                                            "        COR_TRN_SALARY_DETAILS.TAX_PAYABLE                                AS " + strNetTaxPayable + "," +
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                         AS " + strTotalTDSDeducted + "," +
                                            "        COR_TRN_SALARY_DETAILS.SHORTFALL_TAX                              AS " + strShortfallExcessDeductionOfTax + "," +
                                            "        COR_TRN_SALARY_DETAILS.TAXABLE_AMOUNT                             AS " + strCurrentEmployerSalary + "," +
                                            "        COR_TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT                    AS " + strPreviousEmployerSalary + "," +
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT                  AS " + strCurrentEmployerTDS + "," +
                                            "        COR_TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL                AS " + strPreviousEmployerTDS + "," +
                                            "        " + cmnService.J_SQLDBFormat(strTaxDeductedAtHigherRateMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strWhetherTaxDeductedAtHigherRate + ", " +   //-- 26/02/2018 --
                                                                                                                                                                                                     //"        COR_TRN_SALARY_DETAILS.TAX_DEDUCTED_HIGHER_RATE                   AS " + strWhetherTaxDeductedAtHigherRate + "," +    //-- 26/02/2018 --
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED + COR_TRN_SALARY_DETAILS.SUPER_ANN_TAX   AS " + strTDSIncludingSuperannuation + "," +
                                            "        COR_TRN_SALARY_DETAILS.SUPER_ANN_YN                               AS " + strWhetherContributionsPaidByTrustees + "," +    //-- 24/02/2018 --                                      
                                            "        COR_TRN_SALARY_DETAILS.SUPER_ANN_NAME                             AS " + strNameOfSuperannuationFund + "," +
                                            //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE                        AS " + strFromDate + "," +
                                            //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE                          AS " + strToDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strFromDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strToDate + "," +
                                            "        " + cmnService.J_SQLDBFormat(strAmountOfContributionRepaidMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAmountOfContributionRepaid + ", " +   //-- 26/02/2018 --
                                            "        " + cmnService.J_SQLDBFormat(strAverageRateOfDeductionMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAverageRateOfDeduction + ", " +   //-- 26/02/2018 --
                                            "        " + cmnService.J_SQLDBFormat(strAmountOfTaxDeductedMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strAmountOfTaxDeducted + ", " +   //-- 26/02/2018 --                                                                                                                                                                                     //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT                           AS " + strAmountOfContributionRepaid + "," +   //-- 24/02/2018 --                                      
                                            //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_RATE                             AS " + strAverageRateOfDeduction + "," +
                                            //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_TAX                              AS " + strAmountOfTaxDeducted + "," +
                                            "        COR_TRN_SALARY_DETAILS.SUPER_ANN_INCOME                           AS " + strSalGrossTotalIncome + "," +
                                            "        COR_TRN_SALARY_DETAILS.RENT_EXCEEDING_YN                          AS " + strWhetherRentPaymentExceeds1lakh + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_1_PAN                             AS " + strPANOfLandlord1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_1_NAME                            AS " + strNameOfLandlord1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_2_PAN                             AS " + strPANOfLandlord2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_2_NAME                            AS " + strNameOfLandlord2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_3_PAN                             AS " + strPANOfLandlord3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_3_NAME                            AS " + strNameOfLandlord3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_4_PAN                             AS " + strPANOfLandlord4 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_4_NAME                            AS " + strNameOfLandlord4 + "," +
                                            "        COR_TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER                    AS " + strWhetherInterestpaidExceeds1lakh + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_1_PAN                               AS " + strPANOfLender1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_1_NAME                              AS " + strNameOfLender1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_2_PAN                               AS " + strPANOfLender2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_2_NAME                              AS " + strNameOfLender2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_3_PAN                               AS " + strPANOfLender3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_3_NAME                              AS " + strNameOfLender3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_4_PAN                               AS " + strPANOfLender4 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_4_NAME                              AS " + strNameOfLender4 + " " +
                                            " FROM   COR_TRN_SALARY_DETAILS             " +
                                            " WHERE  COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                                            " ORDER  BY COR_TRN_SALARY_DETAILS.SL_NO    ";
                                #endregion
                            }
                        }
                        #endregion
                        //
                        // FORM-16 SALARY DETAILS
                        #region FORM-16-SALARY DETAILS
                        else if (strFormNo == T_FormNo.F24QForm16SalaryDetails)
                        {
                            //-- 20/02/2018 --
                            //-- 19/02/2018 --
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "Read me") == false) return;
                            prgBar.Value = prgBar.Value + 5; 
                            //
                            this.Cursor = Cursors.WaitCursor;
                            //
                            if (WRITE_READ_ME_WORKSHEET(strExcelFilePath, "Read me", cmbFormNo.Text) == false) return;
                            prgBar.Value = prgBar.Value + 5; 
                            //-----------------------------------------------------------------------------------------------------

                            //
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "FORM16-DETAILS") == false) return;
                            //
                            prgBar.Value = prgBar.Value + 5;
                            //-----------------------------------------------------------------------------------------------------
                            //-- Added By Abhishek Dey On 09/05/2019 --
                            if (intFinancialYearID >= T_FinancialYearID.F2018_19ID)
                            {
                                #region variable declaretion for new format
                                strForm16EmployeeSerialNo = "[Serial No]";
                                strForm16PANOfTheEmployee = "[PAN of the Employee]";
                                strForm16NameOfTheEmployee = "[Name of the Employee]";
                                strForm16CategoryOfTheEmployee = "[Category of the Employee]";
                                strForm16PeriodOfEmploymentFormDate = "[Period of employment : From Date (dd/mm/yyyy)]";
                                strForm16PeriodofemploymentToDate = "[Period of employment : To Date (dd/mm/yyyy)]";

                                strForm16GrossSalarySec171 = "[Gross Salary a) Sec 17(1)]";   //-- 16/02/2018 --
                                strForm16GrossSalarySec172 = "[Gross Salary b) Sec 17(2)]";   //-- 16/02/2018 --
                                strForm16GrossSalarySec173 = "[Gross Salary c) Sec 17(3)]";   //-- 16/02/2018 --
                                strForm16TotalSalary335 = "[Total Salary]";

                                strSec10_5 = "[Sec10(5)]";
                                strSec10_10 = "[Sec10(10)]";
                                strSec10_10A = "[Sec10(10A)]";
                                strSec10_10AA = "[Sec10(10AA)]";
                                strSec10_13A = "[Sec10(13A)]";
                                strSec_10_Description_1 = "[Amount of any other exemption under section 10 description 1]";
                                strSec_10_Amount_1 = "[Amount of any other exemption under section 10 amount 1]";
                                strSec_10_Description_2 = "[Amount of any other exemption under section 10 description 2]";
                                strSec_10_Amount_2 = "[Amount of any other exemption under section 10 amount 2]";
                                strSec_10_Description_3 = "[Amount of any other exemption under section 10 description 3]";
                                strSec_10_Amount_3 = "[Amount of any other exemption under section 10 amount 3]";
                                strSec_10_Description_4 = "[Amount of any other exemption under section 10 description 4]";
                                strSec_10_Amount_4 = "[Amount of any other exemption under section 10 amount 4]";
                                strSec_10_Description_5 = "[Amount of any other exemption under section 10 description 5]";
                                strSec_10_Amount_5 = "[Amount of any other exemption under section 10 amount 5]";
                                strAllowance_Total = "[Allowance Total]";
                                str3_Balance = "[3) Balance]";
                                strCurrent_Employer_Salary = "[Current employer salary]";
                                strPrevious_Employer_Salary = "[Previous employer salary]";
                                strEntertainment_Allowance_16ii = "[4) Deductions u/s 16 : (a)Entertainment Allowance (16ii)]";
                                strTax_On_Employment_16iii = "[4) Deductions u/s 16 : (b)Tax on Employment(16iii)]";
                                strDeductions_16ia = "[4) (c)Deductions u/s 16(ia)]";
                                strAggregateOf_4 = "[5) Aggregate of 4(a)&(b)&(c) ]";
                                strHeadSalaries3 = "[6) Income chargeable under the head 'Salaries'(3 - 5)]";
                                strHouseProperty = "[Income or loss - House Property offered for TDS]";
                                strIncome_Other_Sources = "[Income - Other Sources offered for TDS]";
                                strGross_Total_Income6_7 = "[8) Gross Total Income(6 + 7)]";
                                strUnder_Sec_80C_Gross = "[Chapter VIA under sec 80C - Gross Amount]";
                                strUnder_Sec_80C_Amount = "[Chapter VIA under sec 80C - Deductible Amount]";
                                strUnder_Sec_80CCC_Gross = "[Chapter VIA under sec 80CCC - Gross Amount]";
                                strUnder_Sec_80CCC_Amount = "[Chapter VIA under sec 80CCC - Deductible Amount]";
                                strUnder_Sec_80CCD1_Gross = "[Chapter VIA under sec 80CCD(1) - Gross Amount]";
                                strUnder_Sec_80CCD1_Amount = "[Chapter VIA under sec 80CCD(1) - Deductible Amount]";
                                str80C_80CCC_80CCD_Gross = "[Total u/ s 80C, 80CCC & 80CCD(1) - Gross Amount]";
                                str80C_80CCC_80CCD_Amount = "[Total u/s 80C, 80CCC & 80CCD(1) - Deductible Amount]";
                                strUnder_Sec_80CCD1B_Gross = "[Chapter VIA under sec 80CCD(1B) - Gross Amount]";
                                strUnder_Sec_80CCD1B_Amount = "[Chapter VIA under sec 80CCD(1B) - Deductible Amount]";
                                strUnder_Sec_80CCD2_gross = "[Chapter VIA under sec 80CCD(2) - Gross Amount]";
                                strUnder_Sec_80CCD2_Amount = "[Chapter VIA under sec 80CCD(2) - Deductible Amount]";
                                strUnder_Sec_80D_Gross = "[Chapter VIA under sec 80D - Gross Amount]";
                                strUnder_Sec_80D_Amount = "[Chapter VIA under sec 80D - Deductible Amount]";
                                strUnder_Sec_80E_Gross = "[Chapter VIA under sec 80E - Gross Amount]";
                                strUnder_Sec_80E_Amount = "[Chapter VIA under sec 80E - Deductible Amount]";
                                strUnder_Sec_80G_Gross = "[Chapter VIA under sec 80G - Gross Amount]";
                                strUnder_Sec_80G_Qualifying_Amount = "[Chapter VIA under sec 80G - Qualifying Amount]";
                                strUnder_Sec_80G_Amount = "[Chapter VIA under sec 80G - Deductible Amount]";
                                strUnder_Sec_80TTA_Gross = "[Chapter VIA under sec 80TTA - Gross Amount]";
                                strUnder_Sec_80TTA_Qualifying_Amount = "[Chapter VIA under sec 80TTA - Qualifying Amount]";
                                strUnder_Sec_80TTA_Amount = "[Chapter VIA under sec 80TTA - Deductible Amount]";
                                strOther_Provisions_Section = "[1) Chapter VIA Other Provisions -Section]";
                                strOther_Provisions_gross = "[1) Chapter VIA Other Provisions -Gross Amount]";
                                strOther_Provisions_Qualifying_Amount = "[1) Chapter VIA Other Provisions -Qualifying Amount]";
                                strOther_Provisions_Amount = "[1) Chapter VIA Other Provisions -Deductible Amount]";
                                strOther_Provisions_Section_2 = "[2) Chapter VIA Other Provisions -Section]";
                                strOther_Provisions_gross_2 = "[2) Chapter VIA Other Provisions -Gross Amount]";
                                strOther_Provisions_Qualifying_Amount_2 = "[2) Chapter VIA Other Provisions -Qualifying Amount]";
                                strOther_Provisions_Amount_2 = "[2) Chapter VIA Other Provisions -Deductible Amount]";
                                strOther_Provisions_Section_3 = "[3) Chapter VIA Other Provisions -Section]";
                                strOther_Provisions_gross_3 = "[3) Chapter VIA Other Provisions - Gross Amount]";
                                strOther_Provisions_Qualifying_Amount_3 = "[3) Chapter VIA Other Provisions - Qualifying Amount]";
                                strOther_Provisions_Amount_3 = "[3) Chapter VIA Other Provisions - Deductible Amount]";
                                strOther_Provisions_Section_4 = "[4) Chapter VIA Other Provisions -Section]";
                                strOther_Provisions_gross_4 = "[4) Chapter VIA Other Provisions - Gross Amount]";
                                strOther_Provisions_Qualifying_Amount_4 = "[4) Chapter VIA Other Provisions - Qualifying Amount]";
                                strOther_Provisions_Amount_4 = "[4) Chapter VIA Other Provisions - Deductible Amount]";
                                strOther_Provisions_Section_5 = "[5) Chapter VIA Other Provisions - Section]";
                                strOther_Provisions_gross_5 = "[5) Chapter VIA Other Provisions - Gross Amount]";
                                strOther_Provisions_Qualifying_Amount_5 = "[5) Chapter VIA Other Provisions - Qualifying Amount]";
                                strOther_Provisions_Amount_5 = "[5) Chapter VIA Other Provisions - Deductible Amount]";
                                strOther_Provisions_Section_6 = "[6) Chapter VIA Other Provisions - Section]";
                                strOther_Provisions_gross_6 = "[6) Chapter VIA Other Provisions - Gross Amount]";
                                strOther_Provisions_Qualifying_Amount_6 = "[6) Chapter VIA Other Provisions - Qualifying Amount]";
                                strOther_Provisions_Amount_6 = "[6) Chapter VIA Other Provisions - Deductible Amount]";
                                strTotal_Amount_Under_Sec_Gross = "[Total under Chapter VIA Other sections - Gross Amount]";
                                strTotal_Amount_Under_Sec_Total_Qualifying_Amount = "[Total under Chapter VIA under Other sections - Qualifying Amount]";
                                strTotal_Amount_Under_Sec_Amount = "[Total under Chapter VIA under Other sections - Deductible Amount]";
                                strGross_Total_Deduction = "[Gross Total Deduction under chapter VIA]";
                                strTotal_Taxable_Income = "[11) Total Taxable Income]";
                                strIncome_Tax_Total = "[12) Income Tax on Total Income]";
                                strRebate = "[Rebate under section 87A]";
                                strSurcharge = "[13) Surcharge(on Tax computed on Srl) 12)]";
                                str_Education_Cess = "[14) Education Cess (on Tax computed on Srl 12)]";
                                strTax_Payable = "[15) Tax Payable (12+13+14)]";
                                strRelief_Under_Section_89 = "[16) Less: Relief under Section 89]";
                                strNet_Tax_Payable = "[17) Net Tax Payable]";
                                strTotal_TDS_Deducted = "[Total TDS Deducted]";
                                strCurrent_Employer_TDS = "[Current employer TDS]";
                                strPreviousEmployer_TDS = "[Previous employer TDS]";
                                strShortfall = "[Shortfall Excess / Deduction of Tax]";
                                strTax_Deducted_Higher_Rate = "[Whether Tax deducted at Higher Rate (Y/N)]";
                                strSuperannuation = "[18) TDS including Superannuation]";
                                strSuperannuation_Fund = "[19) Whether contributions paid of Superannuation fund (Y/N)]";
                                strName_Superannuation = "[Name of Superannuation Fund]";
                                strFrom_Date = "[From Date]";
                                strTo_Date = "[To Date]";
                                strcontribution_Repaid = "[Amount of contribution repaid]";
                                strAverage_Rate_Deduction = "[Average rate of deduction]";
                                strAmount_Tax_Deducted = "[Amount of Tax deducted]";
                                strGross_Total_Income = "[Gross Total Income]";
                                str_Rent_Payment = "[20) Whether rent payment exceeds 1L during previous year(Y/N)]";
                                strForm16PANOfLandlord1 = "[PAN of landlord 1]";
                                strForm16NameOfLandlord1 = "[Name of landlord 1]";
                                strForm16PANOfLandlord2 = "[PAN of landlord 2]";
                                strForm16NameOfLandlord2 = "[Name of landlord 2]";
                                strForm16PANOfLandlord3 = "[PAN of landlord 3]";
                                strForm16NameOfLandlord3 = "[Name of landlord 3]";
                                strForm16PANOfLandlord4 = "[PAN of landlord 4]";
                                strForm16NameOfLandlord4 = "[Name of landlord 4]";
                                strForm16WhetherInterestPaidExceeds1lakh = "[21) Whether interest paid exceeds 1L (Y/N)]";
                                strForm16PANOfLender1 = "[PAN of lender 1]";
                                strForm16NameOfLender1 = "[Name of lender 1]";
                                strForm16PANOfLender2 = "[PAN of lender 2]";
                                strForm16NameOfLender2 = "[Name of lender 2]";
                                strForm16PANOfLender3 = "[PAN of lender 3]";
                                strForm16NameOflender3 = "[Name of lender 3]";
                                strForm16PANOfLender4 = "[PAN of lender 4]";
                                strForm16NameOfLender4 = "[Name of lender 4]";
                                #endregion
                                //
                                //-----------------------
                                // FORM-16-SALARY DETAILS FOR NEW FORMAT (2019-20)
                                //-----------------------
                                #region Query
                                if (rbnRegularReturn.Checked == true)
                                    strQueryDD = " SELECT TRN_SALARY_DETAILS.SL_NO                                                    AS " + strForm16EmployeeSerialNo + "," +
                                            "        MST_EMPLOYEE.EMPLOYEE_PAN                                                               AS " + strForm16PANOfTheEmployee + "," +
                                            "        MST_EMPLOYEE.EMPLOYEE_NAME                                                            AS " + strForm16NameOfTheEmployee + "," +
                                            "        MST_EMPLOYEE.CATEGORY                                                                      AS " + strForm16CategoryOfTheEmployee + "," +
                                            //"        TRN_SALARY_DETAILS.FROM_DATE                                  AS " + strForm16PeriodOfEmploymentFormDate + "," +
                                            //"        TRN_SALARY_DETAILS.TO_DATE                                    AS " + strForm16PeriodofemploymentToDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16PeriodOfEmploymentFormDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16PeriodofemploymentToDate + "," +
                                            "        TRN_SALARY_DETAILS.TS_GS_SEC_17_1                                                 AS " + strForm16GrossSalarySec171 + "," +
                                            "        TRN_SALARY_DETAILS.TS_GS_SEC_17_2                                                 AS " + strForm16GrossSalarySec172 + "," +
                                            "        TRN_SALARY_DETAILS.TS_GS_SEC_17_3                                                 AS " + strForm16GrossSalarySec173 + "," +
                                            "        TRN_SALARY_DETAILS.TS_GS_TOTAL                                                       AS " + strForm16TotalSalary335 + "," +
                                            "        TRN_SALARY_DETAILS.SEC10_5_AMOUNT                                                AS " + strSec10_5 + "," +
                                            "        TRN_SALARY_DETAILS.SEC10_10_AMOUNT                                              AS " + strSec10_10 + "," +
                                            "        TRN_SALARY_DETAILS.SEC10_10A_AMOUNT                                            AS " + strSec10_10A + ", " +
                                            "        TRN_SALARY_DETAILS.SEC10_10AA_AMOUNT                                          AS " + strSec10_10AA  + ", " +
                                            "        TRN_SALARY_DETAILS.SEC10_13A_AMOUNT                                            AS " + strSec10_13A + ", " +
                                            "        TRN_SALARY_DETAILS.TS_LA_ITEM_1_DESC                                            AS " + strSec_10_Description_1 + ", " +
                                            "        TRN_SALARY_DETAILS.TS_LA_ITEM_1                                                      AS " + strSec_10_Amount_1 + ", " +
                                            "        TRN_SALARY_DETAILS.TS_LA_ITEM_2_DESC                                            AS " + strSec_10_Description_2 + ", " +
                                            "        TRN_SALARY_DETAILS.TS_LA_ITEM_2                                                      AS " + strSec_10_Amount_2  + ", " +
                                            "        TRN_SALARY_DETAILS.TS_LA_ITEM_3_DESC                                            AS " + strSec_10_Description_3  + ", " +
                                            "        TRN_SALARY_DETAILS.TS_LA_ITEM_3                                                      AS " + strSec_10_Amount_3  + ", " +
                                            "        TRN_SALARY_DETAILS.TS_LA_ITEM_4_DESC                                            AS " + strSec_10_Description_4 + ", " +
                                            "        TRN_SALARY_DETAILS.TS_LA_ITEM_4                                                      AS " + strSec_10_Amount_4 + ", " +
                                            "        TRN_SALARY_DETAILS.TS_LA_ITEM_5_DESC                                            AS " + strSec_10_Description_5 + ", " +
                                            "        TRN_SALARY_DETAILS.TS_LA_ITEM_5                                                      AS " + strSec_10_Amount_5 + ", " +
                                            "        TRN_SALARY_DETAILS.TS_LA_TOTAL                                                       AS " + strAllowance_Total + ", " +
                                            "        TRN_SALARY_DETAILS.TS_BALANCE                                                        AS " + str3_Balance + ", " +
                                            "        TRN_SALARY_DETAILS.TAXABLE_AMOUNT                                                AS " + strCurrent_Employer_Salary  + ", " +
                                            "        TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT                              AS " + strPrevious_Employer_Salary  + ", " +
                                            "        TRN_SALARY_DETAILS.US_16_EA                                                            AS " + strEntertainment_Allowance_16ii + ", " +
                                            "        TRN_SALARY_DETAILS.US_16_TE                                                            AS " + strTax_On_Employment_16iii + ", " +
                                            "        TRN_SALARY_DETAILS.US_16_IA                                                             AS " + strDeductions_16ia + ", " +
                                            "        TRN_SALARY_DETAILS.US_16_AGGREGATE                                                AS " + strAggregateOf_4 + ", " +
                                            "        TRN_SALARY_DETAILS.INCOME_CHARGEABLE                                           AS " + strHeadSalaries3 + ", " +
                                            "        TRN_SALARY_DETAILS.AIS_ITEM_1                                                          AS " + strHouseProperty + ", " +
                                            "        TRN_SALARY_DETAILS.AIS_ITEM_2                                                          AS " + strIncome_Other_Sources + ", " +
                                            "        TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                                         AS " + strGross_Total_Income6_7 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80C_GROSS_TOTAL                                AS " + strUnder_Sec_80C_Gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80C_DED_TOTAL                                    AS " + strUnder_Sec_80C_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCC_GROSS_AMOUNT                         AS " + strUnder_Sec_80CCC_Gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCC_DED_AMOUNT                             AS " + strUnder_Sec_80CCC_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCD_GROSS_AMOUNT                        AS " + strUnder_Sec_80CCD1_Gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCD_DED_AMOUNT                            AS " + strUnder_Sec_80CCD1_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_GROSS_AMOUNT             AS " + str80C_80CCC_80CCD_Gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT                 AS " + str80C_80CCC_80CCD_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCD_1B_GROSS_AMOUNT                  AS " + strUnder_Sec_80CCD1B_Gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCD_1B_DED_AMOUNT                      AS " + strUnder_Sec_80CCD1B_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCD_2_GROSS_AMOUNT                    AS " + strUnder_Sec_80CCD2_gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80CCD_2_DED_AMOUNT                        AS " + strUnder_Sec_80CCD2_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80D_GROSS_AMOUNT                             AS " + strUnder_Sec_80D_Gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80D_DED_AMOUNT                                 AS " + strUnder_Sec_80D_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80E_GROSS_AMOUNT                            AS " + strUnder_Sec_80E_Gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80E_DED_AMOUNT                                AS " + strUnder_Sec_80E_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80G_GROSS_AMOUNT                           AS " + strUnder_Sec_80G_Gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80G_QUAL_AMOUNT                             AS " + strUnder_Sec_80G_Qualifying_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80G_DED_AMOUNT                               AS " + strUnder_Sec_80G_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80TTA_GROSS_AMOUNT                        AS " + strUnder_Sec_80TTA_Gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80TTA_QUAL_AMOUNT                           AS " + strUnder_Sec_80TTA_Qualifying_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_SEC80TTA_DED_AMOUNT                              AS " + strUnder_Sec_80TTA_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DESC                                      AS " + strOther_Provisions_Section + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_GROSS_AMOUNT                     AS " + strOther_Provisions_gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_QUAL_AMOUNT                        AS " + strOther_Provisions_Qualifying_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DED_AMOUNT                          AS " + strOther_Provisions_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DESC                                       AS " + strOther_Provisions_Section_2 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_GROSS_AMOUNT                      AS " + strOther_Provisions_gross_2 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_QUAL_AMOUNT                         AS " + strOther_Provisions_Qualifying_Amount_2 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DED_AMOUNT                           AS " + strOther_Provisions_Amount_2 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DESC                                        AS " + strOther_Provisions_Section_3 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_GROSS_AMOUNT                       AS " + strOther_Provisions_gross_3 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_QUAL_AMOUNT                          AS " + strOther_Provisions_Qualifying_Amount_3 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DED_AMOUNT                           AS " + strOther_Provisions_Amount_3 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DESC                                         AS " + strOther_Provisions_Section_4 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_GROSS_AMOUNT                        AS " + strOther_Provisions_gross_4 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_QUAL_AMOUNT                           AS " + strOther_Provisions_Qualifying_Amount_4 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DED_AMOUNT                             AS " + strOther_Provisions_Amount_4 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DESC                                          AS " + strOther_Provisions_Section_5 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_GROSS_AMOUNT                         AS " + strOther_Provisions_gross_5 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_QUAL_AMOUNT                            AS " + strOther_Provisions_Qualifying_Amount_5 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DED_AMOUNT                               AS " + strOther_Provisions_Amount_5 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_DESC                                            AS " + strOther_Provisions_Section_6 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_GROSS_AMOUNT                           AS " + strOther_Provisions_gross_6 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_QUAL_AMOUNT                               AS " + strOther_Provisions_Qualifying_Amount_6 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_DED_AMOUNT                                 AS " + strOther_Provisions_Amount_6 + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_GROSS_TOTAL                                             AS " + strTotal_Amount_Under_Sec_Gross + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_QUAL_TOTAL                                                AS " + strTotal_Amount_Under_Sec_Total_Qualifying_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL                                                 AS " + strTotal_Amount_Under_Sec_Amount + ", " +
                                            "        TRN_SALARY_DETAILS.CVIA_DED_TOTAL                                                         AS " + strGross_Total_Deduction + ", " +
                                            "        TRN_SALARY_DETAILS.TOTAL_INCOME                                                             AS " + strTotal_Taxable_Income + ", " +
                                            "        TRN_SALARY_DETAILS.TAX_TOTAL_INCOME_B4_REBATE                                                AS " + strIncome_Tax_Total + ", " +
                                            "        TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT                                              AS " + strRebate + ", " +
                                            "        TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME                                                   AS " + strSurcharge + ", " +
                                            "        TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME                                                   AS " + str_Education_Cess + ", " +
                                            "        TRN_SALARY_DETAILS.TAX_PAYABLE_AGGREGATE                                             AS " + strTax_Payable + ", " +
                                            "        TRN_SALARY_DETAILS.US_89_LESS                                                                  AS " + strRelief_Under_Section_89 + ", " +
                                            "        TRN_SALARY_DETAILS.TAX_PAYABLE                                                                 AS " + strNet_Tax_Payable + ", " +
                                            "        TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                                                  AS " + strTotal_TDS_Deducted + ", " +
                                            "        TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT                                    AS " + strCurrent_Employer_TDS + ", " +
                                            "        TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL                                  AS " + strPreviousEmployer_TDS + ", " +
                                            "        TRN_SALARY_DETAILS.SHORTFALL_TAX                                                            AS " + strShortfall + ", " +
                                            "        " + cmnService.J_SQLDBFormat(strTaxDeductedAtHigherRateMatrix, J_SQLColFormat.Case_End) + " AS " + strTax_Deducted_Higher_Rate + ", " +
                                            "        TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED + TRN_SALARY_DETAILS.SUPER_ANN_TAX                    AS " + strSuperannuation + "," +  
                                            "        TRN_SALARY_DETAILS.SUPER_ANN_YN                                  AS " + strSuperannuation_Fund + ", " +
                                            "        TRN_SALARY_DETAILS.SUPER_ANN_NAME                             AS " + strName_Superannuation + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strFrom_Date + "," +
                                            "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strTo_Date + "," +
                                            "        TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT                         AS " + strcontribution_Repaid + "," +
                                            "        TRN_SALARY_DETAILS.SUPER_ANN_RATE                              AS " + strAverage_Rate_Deduction + "," +
                                            "        TRN_SALARY_DETAILS.SUPER_ANN_TAX                                AS " + strAmount_Tax_Deducted + "," +
                                            "        TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                      AS " + strGross_Total_Income + ", " +
                                            "        TRN_SALARY_DETAILS.RENT_EXCEEDING_YN                         AS " + str_Rent_Payment + ", " +
                                            "        TRN_SALARY_DETAILS.LANDLORD_1_PAN                              AS " + strForm16PANOfLandlord1 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_1_NAME                           AS " + strForm16NameOfLandlord1 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_2_PAN                              AS " + strForm16PANOfLandlord2 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_2_NAME                           AS " + strForm16NameOfLandlord2 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_3_PAN                              AS " + strForm16PANOfLandlord3 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_3_NAME                           AS " + strForm16NameOfLandlord3 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_4_PAN                              AS " + strForm16PANOfLandlord4 + "," +
                                            "        TRN_SALARY_DETAILS.LANDLORD_4_NAME                           AS " + strForm16NameOfLandlord4 + "," +
                                            "        TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER              AS " + strForm16WhetherInterestPaidExceeds1lakh + ", " +
                                            "        TRN_SALARY_DETAILS.LENDER_1_PAN                                  AS " + strForm16PANOfLender1 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_1_NAME                               AS " + strForm16NameOfLender1 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_2_PAN                                  AS " + strForm16PANOfLender2 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_2_NAME                               AS " + strForm16NameOfLender2 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_3_PAN                                 AS " + strForm16PANOfLender3 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_3_NAME                               AS " + strForm16NameOflender3 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_4_PAN                                 AS " + strForm16PANOfLender4 + "," +
                                            "        TRN_SALARY_DETAILS.LENDER_4_NAME                              AS " + strForm16NameOfLender4 + " " +
                                            "          FROM  TRN_SALARY_DETAILS  " +
                                            "     LEFT JOIN  MST_EMPLOYEE        " +
                                            "            ON  TRN_SALARY_DETAILS.EMPLOYEE_ID = MST_EMPLOYEE.EMPLOYEE_ID " +
                                            "         WHERE  TRN_SALARY_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                                            "     ORDER  BY  TRN_SALARY_DETAILS.SL_NO    ";
                                else if (rbnCorrectionReturn.Checked == true)
                                    strQueryDD = " SELECT COR_TRN_SALARY_DETAILS.SL_NO                                                    AS " + strForm16EmployeeSerialNo + "," +
                                            "        COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN                                                               AS " + strForm16PANOfTheEmployee + "," +
                                            "        COR_TRN_SALARY_DETAILS.EMPLOYEE_NAME                                                            AS " + strForm16NameOfTheEmployee + "," +
                                            "        COR_TRN_SALARY_DETAILS.CATEGORY                                                                      AS " + strForm16CategoryOfTheEmployee + "," +
                                            //"        COR_TRN_SALARY_DETAILS.FROM_DATE                                  AS " + strForm16PeriodOfEmploymentFormDate + "," +
                                            //"        COR_TRN_SALARY_DETAILS.TO_DATE                                    AS " + strForm16PeriodofemploymentToDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16PeriodOfEmploymentFormDate + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16PeriodofemploymentToDate + "," +
                                            "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_1                                                 AS " + strForm16GrossSalarySec171 + "," +
                                            "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_2                                                 AS " + strForm16GrossSalarySec172 + "," +
                                            "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_3                                                 AS " + strForm16GrossSalarySec173 + "," +
                                            "        COR_TRN_SALARY_DETAILS.TS_GS_TOTAL                                                       AS " + strForm16TotalSalary335 + "," +
                                            "        COR_TRN_SALARY_DETAILS.SEC10_5_AMOUNT                                                AS " + strSec10_5 + "," +
                                            "        COR_TRN_SALARY_DETAILS.SEC10_10_AMOUNT                                              AS " + strSec10_10 + "," +
                                            "        COR_TRN_SALARY_DETAILS.SEC10_10A_AMOUNT                                            AS " + strSec10_10A + ", " +
                                            "        COR_TRN_SALARY_DETAILS.SEC10_10AA_AMOUNT                                          AS " + strSec10_10AA + ", " +
                                            "        COR_TRN_SALARY_DETAILS.SEC10_13A_AMOUNT                                            AS " + strSec10_13A + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_1_DESC                                            AS " + strSec_10_Description_1 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_1                                                      AS " + strSec_10_Amount_1 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_2_DESC                                            AS " + strSec_10_Description_2 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_2                                                      AS " + strSec_10_Amount_2 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_3_DESC                                            AS " + strSec_10_Description_3 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_3                                                      AS " + strSec_10_Amount_3 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_4_DESC                                            AS " + strSec_10_Description_4 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_4                                                      AS " + strSec_10_Amount_4 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_5_DESC                                            AS " + strSec_10_Description_5 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_5                                                      AS " + strSec_10_Amount_5 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_LA_TOTAL                                                       AS " + strAllowance_Total + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TS_BALANCE                                                        AS " + str3_Balance + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TAXABLE_AMOUNT                                                AS " + strCurrent_Employer_Salary + ", " +
                                            "        COR_TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT                              AS " + strPrevious_Employer_Salary + ", " +
                                            "        COR_TRN_SALARY_DETAILS.US_16_EA                                                            AS " + strEntertainment_Allowance_16ii + ", " +
                                            "        COR_TRN_SALARY_DETAILS.US_16_TE                                                            AS " + strTax_On_Employment_16iii + ", " +
                                            "        COR_TRN_SALARY_DETAILS.US_16_IA                                                             AS " + strDeductions_16ia + ", " +
                                            "        COR_TRN_SALARY_DETAILS.US_16_AGGREGATE                                                AS " + strAggregateOf_4 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.INCOME_CHARGEABLE                                           AS " + strHeadSalaries3 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.AIS_ITEM_1                                                          AS " + strHouseProperty + ", " +
                                            "        COR_TRN_SALARY_DETAILS.AIS_ITEM_2                                                          AS " + strIncome_Other_Sources + ", " +
                                            "        COR_TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                                         AS " + strGross_Total_Income6_7 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_GROSS_TOTAL                                AS " + strUnder_Sec_80C_Gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_DED_TOTAL                                    AS " + strUnder_Sec_80C_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCC_GROSS_AMOUNT                         AS " + strUnder_Sec_80CCC_Gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCC_DED_AMOUNT                             AS " + strUnder_Sec_80CCC_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_GROSS_AMOUNT                        AS " + strUnder_Sec_80CCD1_Gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_DED_AMOUNT                            AS " + strUnder_Sec_80CCD1_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_GROSS_AMOUNT             AS " + str80C_80CCC_80CCD_Gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT                 AS " + str80C_80CCC_80CCD_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_1B_GROSS_AMOUNT                  AS " + strUnder_Sec_80CCD1B_Gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_1B_DED_AMOUNT                      AS " + strUnder_Sec_80CCD1B_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_2_GROSS_AMOUNT                    AS " + strUnder_Sec_80CCD2_gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_2_DED_AMOUNT                        AS " + strUnder_Sec_80CCD2_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80D_GROSS_AMOUNT                             AS " + strUnder_Sec_80D_Gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80D_DED_AMOUNT                                 AS " + strUnder_Sec_80D_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80E_GROSS_AMOUNT                            AS " + strUnder_Sec_80E_Gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80E_DED_AMOUNT                                AS " + strUnder_Sec_80E_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80G_GROSS_AMOUNT                           AS " + strUnder_Sec_80G_Gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80G_QUAL_AMOUNT                             AS " + strUnder_Sec_80G_Qualifying_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80G_DED_AMOUNT                               AS " + strUnder_Sec_80G_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80TTA_GROSS_AMOUNT                        AS " + strUnder_Sec_80TTA_Gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80TTA_QUAL_AMOUNT                           AS " + strUnder_Sec_80TTA_Qualifying_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_SEC80TTA_DED_AMOUNT                              AS " + strUnder_Sec_80TTA_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DESC                                      AS " + strOther_Provisions_Section + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_GROSS_AMOUNT                     AS " + strOther_Provisions_gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_QUAL_AMOUNT                        AS " + strOther_Provisions_Qualifying_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DED_AMOUNT                          AS " + strOther_Provisions_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DESC                                       AS " + strOther_Provisions_Section_2 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_GROSS_AMOUNT                      AS " + strOther_Provisions_gross_2 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_QUAL_AMOUNT                         AS " + strOther_Provisions_Qualifying_Amount_2 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DED_AMOUNT                           AS " + strOther_Provisions_Amount_2 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DESC                                        AS " + strOther_Provisions_Section_3 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_GROSS_AMOUNT                       AS " + strOther_Provisions_gross_3 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_QUAL_AMOUNT                          AS " + strOther_Provisions_Qualifying_Amount_3 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DED_AMOUNT                           AS " + strOther_Provisions_Amount_3 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DESC                                         AS " + strOther_Provisions_Section_4 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_GROSS_AMOUNT                        AS " + strOther_Provisions_gross_4 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_QUAL_AMOUNT                           AS " + strOther_Provisions_Qualifying_Amount_4 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DED_AMOUNT                             AS " + strOther_Provisions_Amount_4 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DESC                                          AS " + strOther_Provisions_Section_5 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_GROSS_AMOUNT                         AS " + strOther_Provisions_gross_5 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_QUAL_AMOUNT                            AS " + strOther_Provisions_Qualifying_Amount_5 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DED_AMOUNT                               AS " + strOther_Provisions_Amount_5 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_DESC                                            AS " + strOther_Provisions_Section_6 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_GROSS_AMOUNT                           AS " + strOther_Provisions_gross_6 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_QUAL_AMOUNT                               AS " + strOther_Provisions_Qualifying_Amount_6 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_DED_AMOUNT                                 AS " + strOther_Provisions_Amount_6 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_GROSS_TOTAL                                             AS " + strTotal_Amount_Under_Sec_Gross + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_QUAL_TOTAL                                                AS " + strTotal_Amount_Under_Sec_Total_Qualifying_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL                                                 AS " + strTotal_Amount_Under_Sec_Amount + ", " +
                                            "        COR_TRN_SALARY_DETAILS.CVIA_DED_TOTAL                                                         AS " + strGross_Total_Deduction + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_INCOME                                                             AS " + strTotal_Taxable_Income + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TAX_TOTAL_INCOME_B4_REBATE                                                      AS " + strIncome_Tax_Total + ", " +
                                            "        COR_TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT                                              AS " + strRebate + ", " +
                                            "        COR_TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME                                                   AS " + strSurcharge + ", " +
                                            "        COR_TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME                                                   AS " + str_Education_Cess + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TAX_PAYABLE_AGGREGATE                                             AS " + strTax_Payable + ", " +
                                            "        COR_TRN_SALARY_DETAILS.US_89_LESS                                                                  AS " + strRelief_Under_Section_89 + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TAX_PAYABLE                                                                 AS " + strNet_Tax_Payable + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                                                  AS " + strTotal_TDS_Deducted + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT                                    AS " + strCurrent_Employer_TDS + ", " +
                                            "        COR_TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL                                  AS " + strPreviousEmployer_TDS + ", " +
                                            "        COR_TRN_SALARY_DETAILS.SHORTFALL_TAX                                                            AS " + strShortfall + ", " +
                                            "        " + cmnService.J_SQLDBFormat(strTaxDeductedAtHigherRateMatrix, J_SQLColFormat.Case_End) + " AS " + strTax_Deducted_Higher_Rate + ", " +
                                            "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED + COR_TRN_SALARY_DETAILS.SUPER_ANN_TAX                    AS " + strSuperannuation + "," +
                                            "        COR_TRN_SALARY_DETAILS.SUPER_ANN_YN                                  AS " + strSuperannuation_Fund + ", " +
                                            "        COR_TRN_SALARY_DETAILS.SUPER_ANN_NAME                             AS " + strName_Superannuation + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strFrom_Date + "," +
                                            "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strTo_Date + "," +
                                            "        COR_TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT                         AS " + strcontribution_Repaid + "," +
                                            "        COR_TRN_SALARY_DETAILS.SUPER_ANN_RATE                              AS " + strAverage_Rate_Deduction + "," +
                                            "        COR_TRN_SALARY_DETAILS.SUPER_ANN_TAX                                AS " + strAmount_Tax_Deducted + "," +
                                            "        COR_TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                      AS " + strGross_Total_Income + ", " +
                                            "        COR_TRN_SALARY_DETAILS.RENT_EXCEEDING_YN                         AS " + str_Rent_Payment + ", " +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_1_PAN                              AS " + strForm16PANOfLandlord1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_1_NAME                           AS " + strForm16NameOfLandlord1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_2_PAN                              AS " + strForm16PANOfLandlord2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_2_NAME                           AS " + strForm16NameOfLandlord2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_3_PAN                              AS " + strForm16PANOfLandlord3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_3_NAME                           AS " + strForm16NameOfLandlord3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_4_PAN                              AS " + strForm16PANOfLandlord4 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LANDLORD_4_NAME                           AS " + strForm16NameOfLandlord4 + "," +
                                            "        COR_TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER              AS " + strForm16WhetherInterestPaidExceeds1lakh + ", " +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_1_PAN                                  AS " + strForm16PANOfLender1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_1_NAME                               AS " + strForm16NameOfLender1 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_2_PAN                                  AS " + strForm16PANOfLender2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_2_NAME                               AS " + strForm16NameOfLender2 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_3_PAN                                 AS " + strForm16PANOfLender3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_3_NAME                               AS " + strForm16NameOflender3 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_4_PAN                                 AS " + strForm16PANOfLender4 + "," +
                                            "        COR_TRN_SALARY_DETAILS.LENDER_4_NAME                              AS " + strForm16NameOfLender4 + " " +
                                            " FROM  COR_TRN_SALARY_DETAILS  " +
                                            " WHERE  COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                                            " ORDER  BY  COR_TRN_SALARY_DETAILS.SL_NO    ";
                                #endregion

                            }
                            //--------------------------------------------------
                            else
                            {
                                #region variable declaretion
                                strForm16EmployeeSerialNo = "[Serial No (328)]";
                                strForm16PANOfTheEmployee = "[PAN of the Employee (329)]";
                                strForm16NameOfTheEmployee = "[Name of the Employee (330)]";
                                strForm16CategoryOfTheEmployee = "[Category of the Employee (331)]";
                                strForm16PeriodOfEmploymentFormDate = "[Period of employment : From Date (dd/mm/yyyy) (332)]";
                                strForm16PeriodofemploymentToDate = "[Period of employment : To Date (dd/mm/yyyy) (332)]";
                                strForm16GrossSalarySec171 = "[Gross Salary a) Sec 17(1)]";   //-- 16/02/2018 --
                                strForm16GrossSalarySec172 = "[Gross Salary b) Sec 17(2)]";   //-- 16/02/2018 --
                                strForm16GrossSalarySec173 = "[Gross Salary c) Sec 17(3)]";   //-- 16/02/2018 --

                                strForm16TotalSalary335 = "[Total Salary(335)]";
                                strForm16LessAllowanceUnderSection10Description1 = "[2) Less : Allowance under section 10 description 1]";
                                strForm16LessAllowanceUnderSection10Amount1 = "[2) Less : Allowance under section 10 amount 1]";
                                strForm16LessAllowanceUnderSection10Description2 = "[2) Less : Allowance under section 10 description 2]";
                                strForm16LessAllowanceUnderSection10Amount2 = "[2) Less : Allowance under section 10 amount 2]";
                                strForm16LessAllowanceUnderSection10Description3 = "[2) Less : Allowance under section 10 description 3]";
                                strForm16LessAllowanceUnderSection10Amount3 = "[2) Less : Allowance under section 10 amount 3]";
                                strForm16LessAllowanceUnderSection10Description4 = "[2) Less : Allowance under section 10 description 4]";
                                strForm16LessAllowanceUnderSection10Amount4 = "[2) Less : Allowance under section 10 amount 4]";
                                strForm16LessAllowanceUnderSection10Description5 = "[2) Less : Allowance under section 10 description 5]";
                                strForm16LessAllowanceUnderSection10Amount5 = "[2) Less : Allowance under section 10 amount 5]";
                                strForm16AllowanceTotal = "[Allowance Total]";
                                strForm16Balance = "[3) Balance]";
                                strForm16CurrentEmployerSalary333 = "[Current employer salary(333)]";
                                strForm16PreviousEmployerSalary334 = "[Previous employer salary(334)]";
                                strForm16DeductionsEntertainmentAllowance16ii = "[4) Deductions u/s 16 : (a)Entertainment Allowance (16ii)]";
                                strForm16DeductionsTaxOnEmployment16iii = "[4) Deductions u/s 16 : (b)Tax on Employment(16iii)]";
                                strForm16Deductions16ia = "[4) Deductions u/s 16 : (c) 16(ia)]";
                                strForm16AggregateOf4ab = "[5) Aggregate of 4(a)&(b)&(c)]";
                                strForm16IncomeChargeableUnderTheHead3_5 = "[6) Income chargeable under the head 'Salaries' (3-5)]";
                                strForm16AddAnyOtherIncomeOtherThanSalaryDescription1 = "[7) Add : Any other income other than salary description 1]";
                                strForm16AddAnyOtherIncomeOtherThanSalaryAmount1 = "[Add : Any other income other than salary amount 1]";
                                strForm16AddAnyOtherIncomeOtherThanSalaryDescription2 = "[Add : Any other income other than salary description 2]";
                                strForm16AddAnyOtherIncomeOtherThanSalaryAmount2 = "[Add : Any other income other than salary amount 2]";
                                strForm16AddAnyOtherIncomeOtherThanSalaryDescription3 = "[Add : Any other income other than salary description 3]";
                                strForm16AddAnyOtherIncomeOtherThanSalaryAmount3 = "[Add : Any other income other than salary amount 3]";
                                strForm16AddAnyOtherIncomeOtherThanSalaryDescription4 = "[Add : Any other income other than salary description 4]";
                                strForm16AddAnyOtherIncomeOtherThanSalaryAmount4 = "[Add : Any other income other than salary amount 4]";
                                strForm16Total = "[Total]";
                                strForm16GrossTotalIncome6_7 = "[8) Gross Total Income (6+7)]";
                                strForm16DeductionsUnderChapterSection80CDescription1 = "[9) Deductions under Chapter VI-A : Section 80C - Description 1]";
                                strForm16DeductionsUnderChapterAmount1 = "[Deductions under Chapter VI-A : Amount 1]";
                                strForm16DeductionsUnderChapterSection80CDescription2 = "[Deductions under Chapter VI-A : Section 80C - Description 2]";
                                strForm16DeductionsUnderChapterAmount2 = "[Deductions under Chapter VI-A : Amount 2]";
                                strForm16DeductionsUnderChapterSection80CDescription3 = "[Deductions under Chapter VI-A : Section 80C - Description 3]";
                                strForm16DeductionsUnderChapterAmount3 = "[Deductions under Chapter VI-A : Amount 3]";
                                strForm16DeductionsUnderChapterSection80CDescription4 = "[Deductions under Chapter VI-A : Section 80C - Description 4]";
                                strForm16DeductionsUnderChapterAmount4 = "[Deductions under Chapter VI-A : Amount 4]";
                                strForm16DeductionsUnderChapterSection80CDescription5 = "[Deductions under Chapter VI-A : Section 80C - Description 5]";
                                strForm16DeductionsUnderChapterAmount5 = "[Deductions under Chapter VI-A : Amount 5]";
                                strForm16DeductionsUnderChapterSection80CDescription6 = "[Deductions under Chapter VI-A : Section 80C - Description 6]";
                                strForm16DeductionsUnderChapterAmount6 = "[Deductions under Chapter VI-A : Amount 6]";
                                strForm16GrossTotal80C = "[Gross Total (80C)]";
                                strForm16DeductibleTotal80C = "[Deductible Total (80C)]";
                                strForm16Section80CCCGrossAmount = "[Section 80CCC - Gross Amount]";
                                strForm16Section80CCCDeductibleAmount = "[Section 80CCC - Deductible Amount]";
                                strForm16Section80CCDGrossAmount = "[Section 80CCD - Gross Amount]";
                                strForm16Section80CCDDeductibleAmount = "[Section 80CCD - Deductible  Amount]";
                                strForm16TotalDeductibleAmount80CCE = "[Total Deductible Amount (80CCE)]";
                                strForm16Section80CCGGrossAmount = "[Section 80CCG - Gross Amount]";
                                strForm16Section80CCGDeductibleAmount = "[Section 80CCG - Deductible Amount]";
                                strForm16OtherSection = "[(i) Other Section]";  // BH
                                strForm16GrossAmount = "[Gross Amount]";
                                strForm16QualifyingAmount = "[Qualifying Amount]";
                                strForm16DeductibleAmount = "[Deductible   Amount]";
                                strForm16OtherSection2 = "[(ii) Other Section]";
                                strForm16GrossAmountBM = "[Gross  Amount]";
                                strForm16QualifyingAmountBN = "[Qualifying  Amount]";
                                strForm16DeductibleAmountBO = "[Deductible    Amount]";
                                strForm16OtherSection3 = "[(iii) Other Section]";
                                strForm16GrossAmountBQ = "[Gross   Amount]";
                                strForm16QualifyingAmountBR = "[Qualifying   Amount]";
                                strForm16DeductibleAmountBS = "[Deductible      Amount]";
                                strForm16OtherSection4 = "[(iv) Other Section]";
                                strForm16GrossAmountBU = "[Gross    Amount]";
                                strForm16QualifyingAmountBV = "[Qualifying       Amount]";
                                strForm16DeductibleAmountBW = "[Deductible        Amount]";
                                strForm16OtherSection5 = "[(v) Other Section]";
                                strForm16GrossAmountBY = "[Gross      Amount]";
                                strForm16QualifyingAmountBZ = "[Qualifying        Amount]";
                                strForm16DeductibleAmountCA = "[Deductible         Amount]";
                                strForm16TotalDeductibleAmountOtherSections = "[Total Deductible Amount (Other Sections)]";
                                strForm16AggregateOfDeductibleAmountUnderChapter = "[10) Aggregate of deductible amount under Chapter VI-A]";
                                strForm16TotalTaxableIncome344 = "[11) Total Taxable Income (344)]";
                                strForm16IncomeTaxOnTotalIncome345 = "[12) Income Tax on Total Income (345)]";
                                strForm16SurchargeOnTaxComputedOnSrl12 = "[13) Surcharge (on Tax computed on Srl) 12)]";
                                strForm16EducationCessOnTaxComputedOnSrl12346 = "[14) Education Cess (on Tax computed on Srl 12) (346)]";
                                strForm16TaxPayable = "[15) Tax Payable (12+13+14)]";
                                strForm16LessReliefUnderSection89 = "[16) Less: Relief under Section 89]";
                                strForm16NetTaxPayable = "[17) Net Tax Payable]";
                                strForm16TotalTDSDeducted = "[Total TDS Deducted]";
                                strForm16CurrentEmployerTDS349 = "[Current employer TDS(349)]";
                                strForm16PreviousEmployerTDS350 = "[Previous employer TDS(350)]";
                                strForm16ShortfallExcessDeductionOfTax = "[Shortfall Excess / Deduction of Tax]";
                                strForm16WhetherTaxDeductedAtHigherRate = "[Whether Tax deducted at Higher Rate (Y/N)]";
                                strForm16TDSIncludingSuperannuation = "[18) TDS including Superannuation]";
                                strForm16WhetherContributionsPaidOfSuperannuationFund = "[19) Whether contributions paid of Superannuation fund]";
                                strForm16NameOfSuperannuationFund = "[Name of Superannuation Fund]";
                                strForm16FromDate = "[From Date]";
                                strForm16ToDate = "[To Date]";
                                strForm16AmountOfContributionRepaid = "[Amount of contribution repaid]";
                                strForm16AverageRateOfDeduction = "[Average rate of deduction]";
                                strForm16AmountOfTaxDeducted = "[Amount of Tax deducted]";
                                strForm16GrossTotalIncomeCX = "[Gross Total Income]";
                                strForm16WhetherRentPaymentExceeds1lakhDuringPreviousYear = "[20) Whether rent payment exceeds 1lakh during previous year(Y/N)]";
                                strForm16PANOfLandlord1 = "[PAN of landlord 1]";
                                strForm16NameOfLandlord1 = "[Name of landlord 1]";
                                strForm16PANOfLandlord2 = "[PAN of landlord 2]";
                                strForm16NameOfLandlord2 = "[Name of landlord 2]";
                                strForm16PANOfLandlord3 = "[PAN of landlord 3]";
                                strForm16NameOfLandlord3 = "[Name of landlord 3]";
                                strForm16PANOfLandlord4 = "[PAN of landlord 4]";
                                strForm16NameOfLandlord4 = "[Name of landlord 4]";
                                strForm16WhetherInterestPaidExceeds1lakh = "[21) Whether interest paid exceeds 1lakh (Y/N)]";
                                strForm16PANOfLender1 = "[PAN of lender 1]";
                                strForm16NameOfLender1 = "[Name of lender 1]";
                                strForm16PANOfLender2 = "[PAN of lender 2]";
                                strForm16NameOfLender2 = "[Name of lender 2]";
                                strForm16PANOfLender3 = "[PAN of lender 3]";
                                strForm16NameOflender3 = "[Name of lender 3]";
                                strForm16PANOfLender4 = "[PAN of lender 4]";
                                strForm16NameOfLender4 = "[Name of lender 4]";
                                #endregion
                                //
                                //-----------------------
                                // FORM-16-SALARY DETAILS
                                //-----------------------
                                #region Query
                                if (rbnRegularReturn.Checked == true)
                                {
                                    strQueryDD = " SELECT TRN_SALARY_DETAILS.SL_NO                                      AS " + strForm16EmployeeSerialNo + "," +
                                             "        MST_EMPLOYEE.EMPLOYEE_PAN                                     AS " + strForm16PANOfTheEmployee + "," +
                                             "        MST_EMPLOYEE.EMPLOYEE_NAME                                    AS " + strForm16NameOfTheEmployee + "," +
                                             "        MST_EMPLOYEE.CATEGORY                                         AS " + strForm16CategoryOfTheEmployee + "," +
                                             //"        TRN_SALARY_DETAILS.FROM_DATE                                  AS " + strForm16PeriodOfEmploymentFormDate + "," +
                                             //"        TRN_SALARY_DETAILS.TO_DATE                                    AS " + strForm16PeriodofemploymentToDate + "," +
                                             "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16PeriodOfEmploymentFormDate + "," +
                                             "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16PeriodofemploymentToDate + "," +
                                             "        TRN_SALARY_DETAILS.TS_GS_SEC_17_1                             AS " + strForm16GrossSalarySec171 + "," +
                                             "        TRN_SALARY_DETAILS.TS_GS_SEC_17_2                             AS " + strForm16GrossSalarySec172 + "," +
                                             "        TRN_SALARY_DETAILS.TS_GS_SEC_17_3                             AS " + strForm16GrossSalarySec173 + "," +
                                             "        TRN_SALARY_DETAILS.TS_GS_TOTAL                                AS " + strForm16TotalSalary335 + "," +
                                             "        TRN_SALARY_DETAILS.TS_LA_ITEM_1_DESC                          AS " + strForm16LessAllowanceUnderSection10Description1 + "," +
                                             "        TRN_SALARY_DETAILS.TS_LA_ITEM_1                               AS " + strForm16LessAllowanceUnderSection10Amount1 + "," +
                                             "        TRN_SALARY_DETAILS.TS_LA_ITEM_2_DESC                          AS " + strForm16LessAllowanceUnderSection10Description2 + "," +
                                             "        TRN_SALARY_DETAILS.TS_LA_ITEM_2                               AS " + strForm16LessAllowanceUnderSection10Amount2 + "," +
                                             "        TRN_SALARY_DETAILS.TS_LA_ITEM_3_DESC                          AS " + strForm16LessAllowanceUnderSection10Description3 + "," +
                                             "        TRN_SALARY_DETAILS.TS_LA_ITEM_3                               AS " + strForm16LessAllowanceUnderSection10Amount3 + "," +
                                             "        TRN_SALARY_DETAILS.TS_LA_ITEM_4_DESC                          AS " + strForm16LessAllowanceUnderSection10Description4 + "," +
                                             "        TRN_SALARY_DETAILS.TS_LA_ITEM_4                               AS " + strForm16LessAllowanceUnderSection10Amount4 + "," +
                                             "        TRN_SALARY_DETAILS.TS_LA_ITEM_5_DESC                          AS " + strForm16LessAllowanceUnderSection10Description5 + "," +
                                             "        TRN_SALARY_DETAILS.TS_LA_ITEM_5                               AS " + strForm16LessAllowanceUnderSection10Amount5 + "," +
                                             "        TRN_SALARY_DETAILS.TS_LA_TOTAL                                AS " + strForm16AllowanceTotal + "," +
                                             "        TRN_SALARY_DETAILS.TS_BALANCE                                 AS " + strForm16Balance + ",";
                                    //-- 17/02/2018 --
                                    // if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID) //-- Commented By Abhishek Dey on 09/05/2019 
                                    if (intFinancialYearID >= T_FinancialYearID.F2013_14ID)  //-- 09/05/2019
                                    {
                                        strQueryDD = strQueryDD + "        TRN_SALARY_DETAILS.TAXABLE_AMOUNT                AS " + strForm16CurrentEmployerSalary333 + "," +
                                                                    "        TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT       AS " + strForm16PreviousEmployerSalary334 + ",";
                                    }
                                    else
                                    {
                                        strQueryDD = strQueryDD + " ' '                                                     AS " + strForm16CurrentEmployerSalary333 + "," +
                                                                    " ' '                                                     AS " + strForm16PreviousEmployerSalary334 + ",";
                                    }
                                    //-----------------
                                    strQueryDD = strQueryDD + "        TRN_SALARY_DETAILS.US_16_EA                      AS " + strForm16DeductionsEntertainmentAllowance16ii + "," +
                                    "        TRN_SALARY_DETAILS.US_16_TE                                   AS " + strForm16DeductionsTaxOnEmployment16iii + "," +
                                    "        TRN_SALARY_DETAILS.US_16_IA                                   AS " + strForm16Deductions16ia + "," +
                                    "        TRN_SALARY_DETAILS.US_16_AGGREGATE                            AS " + strForm16AggregateOf4ab + "," +
                                    "        TRN_SALARY_DETAILS.INCOME_CHARGEABLE                          AS " + strForm16IncomeChargeableUnderTheHead3_5 + "," +
                                    "        TRN_SALARY_DETAILS.AIS_ITEM_1_DESC                            AS " + strForm16AddAnyOtherIncomeOtherThanSalaryDescription1 + "," +
                                    "        TRN_SALARY_DETAILS.AIS_ITEM_1                                 AS " + strForm16AddAnyOtherIncomeOtherThanSalaryAmount1 + "," +
                                    "        TRN_SALARY_DETAILS.AIS_ITEM_2_DESC                            AS " + strForm16AddAnyOtherIncomeOtherThanSalaryDescription2 + "," +
                                    "        TRN_SALARY_DETAILS.AIS_ITEM_2                                 AS " + strForm16AddAnyOtherIncomeOtherThanSalaryAmount2 + "," +
                                    "        TRN_SALARY_DETAILS.AIS_ITEM_3_DESC                            AS " + strForm16AddAnyOtherIncomeOtherThanSalaryDescription3 + "," +
                                    "        TRN_SALARY_DETAILS.AIS_ITEM_3                                 AS " + strForm16AddAnyOtherIncomeOtherThanSalaryAmount3 + "," +
                                    "        TRN_SALARY_DETAILS.AIS_ITEM_4_DESC                            AS " + strForm16AddAnyOtherIncomeOtherThanSalaryDescription4 + "," +
                                    "        TRN_SALARY_DETAILS.AIS_ITEM_4                                 AS " + strForm16AddAnyOtherIncomeOtherThanSalaryAmount4 + "," +
                                    "        TRN_SALARY_DETAILS.AIS_Total                                  AS " + strForm16Total + "," +
                                    "        TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                         AS " + strForm16GrossTotalIncome6_7 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_1_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription1 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_1                         AS " + strForm16DeductionsUnderChapterAmount1 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_2_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription2 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_2                         AS " + strForm16DeductionsUnderChapterAmount2 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_3_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription3 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_3                         AS " + strForm16DeductionsUnderChapterAmount3 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_4_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription4 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_4                         AS " + strForm16DeductionsUnderChapterAmount4 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_5_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription5 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_5                         AS " + strForm16DeductionsUnderChapterAmount5 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_6_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription6 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_6                         AS " + strForm16DeductionsUnderChapterAmount6 + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_GROSS_TOTAL                    AS " + strForm16GrossTotal80C + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80C_DED_TOTAL                      AS " + strForm16DeductibleTotal80C + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80CCC_GROSS_AMOUNT                 AS " + strForm16Section80CCCGrossAmount + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80CCC_DED_AMOUNT                    AS " + strForm16Section80CCCDeductibleAmount + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80CCD_GROSS_AMOUNT                 AS " + strForm16Section80CCDGrossAmount + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80CCD_DED_AMOUNT                   AS " + strForm16Section80CCDDeductibleAmount + "," +
                                    "        TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT             AS " + strForm16TotalDeductibleAmount80CCE + ",";
                                    //if(TDSMAN.Classes.TDSMAN.T_pFinancialYearId == T_FinancialYearID.F2011_12ID)  //-- Commented By Abhishek Dey On 09/05/2019 --
                                    if (intFinancialYearID == T_FinancialYearID.F2011_12ID) //-- 09/05/2019 --
                                    {
                                        strQueryDD = strQueryDD + "        TRN_SALARY_DETAILS.CVIA_SEC80CCF_GROSS_AMOUNT         AS " + strForm16Section80CCGGrossAmount + "," +
                                                                          "        TRN_SALARY_DETAILS.CVIA_SEC80CCF_DED_AMOUNT           AS " + strForm16Section80CCGDeductibleAmount + ",";
                                    }
                                    else
                                    {
                                        strQueryDD = strQueryDD + "        TRN_SALARY_DETAILS.CVIA_SEC80CCG_GROSS_AMOUNT         AS " + strForm16Section80CCGGrossAmount + "," +
                                                                          "        TRN_SALARY_DETAILS.CVIA_SEC80CCG_DED_AMOUNT           AS " + strForm16Section80CCGDeductibleAmount + ",";
                                    }

                                    strQueryDD = strQueryDD + "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DESC          AS " + strForm16OtherSection + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_GROSS_AMOUNT                AS " + strForm16GrossAmount + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_QUAL_AMOUNT                 AS " + strForm16QualifyingAmount + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DED_AMOUNT                  AS " + strForm16DeductibleAmount + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DESC                        AS " + strForm16OtherSection2 + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_GROSS_AMOUNT                AS " + strForm16GrossAmountBM + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_QUAL_AMOUNT                 AS " + strForm16QualifyingAmountBN + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DED_AMOUNT                  AS " + strForm16DeductibleAmountBO + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DESC                        AS " + strForm16OtherSection3 + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_GROSS_AMOUNT                AS " + strForm16GrossAmountBQ + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_QUAL_AMOUNT                 AS " + strForm16QualifyingAmountBR + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DED_AMOUNT                  AS " + strForm16DeductibleAmountBS + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DESC                        AS " + strForm16OtherSection4 + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_GROSS_AMOUNT                AS " + strForm16GrossAmountBU + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_QUAL_AMOUNT                 AS " + strForm16QualifyingAmountBV + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DED_AMOUNT                  AS " + strForm16DeductibleAmountBW + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DESC                        AS " + strForm16OtherSection5 + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_GROSS_AMOUNT                AS " + strForm16GrossAmountBY + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_QUAL_AMOUNT                 AS " + strForm16QualifyingAmountBZ + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DED_AMOUNT                  AS " + strForm16DeductibleAmountCA + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL                          AS " + strForm16TotalDeductibleAmountOtherSections + "," +
                                                 "        TRN_SALARY_DETAILS.CVIA_DED_TOTAL                              AS " + strForm16AggregateOfDeductibleAmountUnderChapter + "," +
                                                 "        TRN_SALARY_DETAILS.TOTAL_INCOME                                AS " + strForm16TotalTaxableIncome344 + "," +
                                                 "        TRN_SALARY_DETAILS.TAX_TOTAL_INCOME                            AS " + strForm16IncomeTaxOnTotalIncome345 + "," +
                                                 "        TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME                           AS " + strForm16SurchargeOnTaxComputedOnSrl12 + "," +
                                                 "        TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME                          AS " + strForm16EducationCessOnTaxComputedOnSrl12346 + "," +
                                                 "        TRN_SALARY_DETAILS.TAX_PAYABLE_AGGREGATE                       AS " + strForm16TaxPayable + "," +
                                                 "        TRN_SALARY_DETAILS.US_89_LESS                                  AS " + strForm16LessReliefUnderSection89 + "," +
                                                 "        TRN_SALARY_DETAILS.TAX_PAYABLE                                 AS " + strForm16NetTaxPayable + "," +
                                                 "        TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                          AS " + strForm16TotalTDSDeducted + ",";
                                    // if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)  //-- Commented By Abhishek Dey On 09/05/2019 --
                                    if (intFinancialYearID >= T_FinancialYearID.F2013_14ID) //-- 09/05/2019 --
                                    {
                                        strQueryDD = strQueryDD + "        TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT                 AS " + strForm16CurrentEmployerTDS349 + "," +
                                                                   "        TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL               AS " + strForm16PreviousEmployerTDS350 + ",";
                                    }
                                    else
                                    {
                                        strQueryDD = strQueryDD + " ' '                                                AS " + strForm16CurrentEmployerTDS349 + "," +
                                                                   " ' '                                                AS " + strForm16PreviousEmployerTDS350 + ",";
                                    }
                                    //"        TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT                        AS " + strForm16CurrentEmployerTDS349 + "," +
                                    //"        TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL                        AS " + strForm16PreviousEmployerTDS350 + "," +
                                    strQueryDD = strQueryDD + "        TRN_SALARY_DETAILS.SHORTFALL_TAX             AS " + strForm16ShortfallExcessDeductionOfTax + "," +
                                                 "        " + cmnService.J_SQLDBFormat(strTaxDeductedAtHigherRateMatrix, J_SQLColFormat.Case_End) + " AS " + strForm16WhetherTaxDeductedAtHigherRate + ", " +
                                                 //"        TRN_SALARY_DETAILS.                        AS " + strForm16WhetherTaxDeductedAtHigherRate + "," +
                                                 //"        TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                       AS " + strForm16TDSIncludingSuperannuation + "," +
                                                 "        TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED + TRN_SALARY_DETAILS.SUPER_ANN_TAX                    AS " + strForm16TDSIncludingSuperannuation + "," +  //-- 21/02/2018 --
                                                                                                                                                                                                          //-- 17/02/2018 --
                                                 "        TRN_SALARY_DETAILS.SUPER_ANN_YN                               AS " + strForm16WhetherContributionsPaidOfSuperannuationFund + ", " +
                                                 //----------------
                                                 //"        TRN_SALARY_DETAILS.SUPER_ANN_YN                        AS " + strForm16WhetherContributionsPaidOfSuperannuationFund + "," +
                                                 "        TRN_SALARY_DETAILS.SUPER_ANN_NAME                             AS " + strForm16NameOfSuperannuationFund + "," +
                                                 //"        TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE                        AS " + strForm16FromDate + "," +
                                                 //"        TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE                          AS " + strForm16ToDate + "," +
                                                 "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16FromDate + "," +
                                                 "        " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16ToDate + "," +
                                                 "        TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT                           AS " + strForm16AmountOfContributionRepaid + "," +
                                                 "        TRN_SALARY_DETAILS.SUPER_ANN_RATE                             AS " + strForm16AverageRateOfDeduction + "," +
                                                 "        TRN_SALARY_DETAILS.SUPER_ANN_TAX                              AS " + strForm16AmountOfTaxDeducted + "," +
                                                 "        TRN_SALARY_DETAILS.SUPER_ANN_INCOME                           AS " + strForm16GrossTotalIncomeCX + "," +
                                                 //-- 17/02/2018 --
                                                 //"        TRN_SALARY_DETAILS.                        AS " + strForm16WhetherRentPaymentExceeds1lakhDuringPreviousYear + "," +
                                                 "        TRN_SALARY_DETAILS.RENT_EXCEEDING_YN                          AS " + strForm16WhetherRentPaymentExceeds1lakhDuringPreviousYear + ", " +     //-- 19/02/2018 --
                                                                                                                                                                                                      //
                                                 "        TRN_SALARY_DETAILS.LANDLORD_1_PAN                             AS " + strForm16PANOfLandlord1 + "," +
                                                 "        TRN_SALARY_DETAILS.LANDLORD_1_NAME                            AS " + strForm16NameOfLandlord1 + "," +
                                                 "        TRN_SALARY_DETAILS.LANDLORD_2_PAN                             AS " + strForm16PANOfLandlord2 + "," +
                                                 "        TRN_SALARY_DETAILS.LANDLORD_2_NAME                            AS " + strForm16NameOfLandlord2 + "," +
                                                 "        TRN_SALARY_DETAILS.LANDLORD_3_PAN                             AS " + strForm16PANOfLandlord3 + "," +
                                                 "        TRN_SALARY_DETAILS.LANDLORD_3_NAME                            AS " + strForm16NameOfLandlord3 + "," +
                                                 "        TRN_SALARY_DETAILS.LANDLORD_4_PAN                             AS " + strForm16PANOfLandlord4 + "," +
                                                 "        TRN_SALARY_DETAILS.LANDLORD_4_NAME                            AS " + strForm16NameOfLandlord4 + "," +
                                                 //-- 17/02/2018 --
                                                 //"        TRN_SALARY_DETAILS.                                           AS " + strForm16WhetherInterestPaidExceeds1lakh + "," +
                                                 "        TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER                    AS " + strForm16WhetherInterestPaidExceeds1lakh + ", " +   //-- 19/02/2018 --
                                                                                                                                                                                   //
                                                 "        TRN_SALARY_DETAILS.LENDER_1_PAN                               AS " + strForm16PANOfLender1 + "," +
                                                 "        TRN_SALARY_DETAILS.LENDER_1_NAME                              AS " + strForm16NameOfLender1 + "," +
                                                 "        TRN_SALARY_DETAILS.LENDER_2_PAN                               AS " + strForm16PANOfLender2 + "," +
                                                 "        TRN_SALARY_DETAILS.LENDER_2_NAME                              AS " + strForm16NameOfLender2 + "," +
                                                 "        TRN_SALARY_DETAILS.LENDER_3_PAN                               AS " + strForm16PANOfLender3 + "," +
                                                 "        TRN_SALARY_DETAILS.LENDER_3_NAME                              AS " + strForm16NameOflender3 + "," +
                                                 "        TRN_SALARY_DETAILS.LENDER_4_PAN                               AS " + strForm16PANOfLender4 + "," +
                                                 "        TRN_SALARY_DETAILS.LENDER_4_NAME                              AS " + strForm16NameOfLender4 + " " +
                                                 //-- 19/02/2018 --
                                                 "          FROM  TRN_SALARY_DETAILS  " +
                                                 "     LEFT JOIN  MST_EMPLOYEE        " +
                                                 "            ON  TRN_SALARY_DETAILS.EMPLOYEE_ID = MST_EMPLOYEE.EMPLOYEE_ID " +
                                                 "         WHERE  TRN_SALARY_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                                                 "     ORDER  BY  TRN_SALARY_DETAILS.SL_NO    ";
                                }
                                else if (rbnCorrectionReturn.Checked == true)
                                {
                                    strQueryDD = " SELECT COR_TRN_SALARY_DETAILS.SL_NO                                      AS " + strForm16EmployeeSerialNo + "," +
                                             "        COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN                                     AS " + strForm16PANOfTheEmployee + "," +
                                             "        COR_TRN_SALARY_DETAILS.EMPLOYEE_NAME                                    AS " + strForm16NameOfTheEmployee + "," +
                                             "        COR_TRN_SALARY_DETAILS.CATEGORY                                         AS " + strForm16CategoryOfTheEmployee + "," +
                                             //"        COR_TRN_SALARY_DETAILS.FROM_DATE                                  AS " + strForm16PeriodOfEmploymentFormDate + "," +
                                             //"        COR_TRN_SALARY_DETAILS.TO_DATE                                    AS " + strForm16PeriodofemploymentToDate + "," +
                                             "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16PeriodOfEmploymentFormDate + "," +
                                             "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16PeriodofemploymentToDate + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_1                             AS " + strForm16GrossSalarySec171 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_2                             AS " + strForm16GrossSalarySec172 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_GS_SEC_17_3                             AS " + strForm16GrossSalarySec173 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_GS_TOTAL                                AS " + strForm16TotalSalary335 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_1_DESC                          AS " + strForm16LessAllowanceUnderSection10Description1 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_1                               AS " + strForm16LessAllowanceUnderSection10Amount1 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_2_DESC                          AS " + strForm16LessAllowanceUnderSection10Description2 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_2                               AS " + strForm16LessAllowanceUnderSection10Amount2 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_3_DESC                          AS " + strForm16LessAllowanceUnderSection10Description3 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_3                               AS " + strForm16LessAllowanceUnderSection10Amount3 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_4_DESC                          AS " + strForm16LessAllowanceUnderSection10Description4 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_4                               AS " + strForm16LessAllowanceUnderSection10Amount4 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_5_DESC                          AS " + strForm16LessAllowanceUnderSection10Description5 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_LA_ITEM_5                               AS " + strForm16LessAllowanceUnderSection10Amount5 + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_LA_TOTAL                                AS " + strForm16AllowanceTotal + "," +
                                             "        COR_TRN_SALARY_DETAILS.TS_BALANCE                                 AS " + strForm16Balance + ",";
                                    //-- 17/02/2018 --
                                    // if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID) //-- Commented By Abhishek Dey on 09/05/2019 
                                    if (intFinancialYearID >= T_FinancialYearID.F2013_14ID)  //-- 09/05/2019
                                    {
                                        strQueryDD = strQueryDD + "        COR_TRN_SALARY_DETAILS.TAXABLE_AMOUNT                AS " + strForm16CurrentEmployerSalary333 + "," +
                                                                    "        COR_TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT       AS " + strForm16PreviousEmployerSalary334 + ",";
                                    }
                                    else
                                    {
                                        strQueryDD = strQueryDD + " ' '                                                     AS " + strForm16CurrentEmployerSalary333 + "," +
                                                                    " ' '                                                     AS " + strForm16PreviousEmployerSalary334 + ",";
                                    }
                                    //-----------------
                                    strQueryDD = strQueryDD + "        COR_TRN_SALARY_DETAILS.US_16_EA                      AS " + strForm16DeductionsEntertainmentAllowance16ii + "," +
                                    "        COR_TRN_SALARY_DETAILS.US_16_TE                                   AS " + strForm16DeductionsTaxOnEmployment16iii + "," +
                                    "        COR_TRN_SALARY_DETAILS.US_16_IA                                   AS " + strForm16Deductions16ia + "," +
                                    "        COR_TRN_SALARY_DETAILS.US_16_AGGREGATE                            AS " + strForm16AggregateOf4ab + "," +
                                    "        COR_TRN_SALARY_DETAILS.INCOME_CHARGEABLE                          AS " + strForm16IncomeChargeableUnderTheHead3_5 + "," +
                                    "        COR_TRN_SALARY_DETAILS.AIS_ITEM_1_DESC                            AS " + strForm16AddAnyOtherIncomeOtherThanSalaryDescription1 + "," +
                                    "        COR_TRN_SALARY_DETAILS.AIS_ITEM_1                                 AS " + strForm16AddAnyOtherIncomeOtherThanSalaryAmount1 + "," +
                                    "        COR_TRN_SALARY_DETAILS.AIS_ITEM_2_DESC                            AS " + strForm16AddAnyOtherIncomeOtherThanSalaryDescription2 + "," +
                                    "        COR_TRN_SALARY_DETAILS.AIS_ITEM_2                                 AS " + strForm16AddAnyOtherIncomeOtherThanSalaryAmount2 + "," +
                                    "        COR_TRN_SALARY_DETAILS.AIS_ITEM_3_DESC                            AS " + strForm16AddAnyOtherIncomeOtherThanSalaryDescription3 + "," +
                                    "        COR_TRN_SALARY_DETAILS.AIS_ITEM_3                                 AS " + strForm16AddAnyOtherIncomeOtherThanSalaryAmount3 + "," +
                                    "        COR_TRN_SALARY_DETAILS.AIS_ITEM_4_DESC                            AS " + strForm16AddAnyOtherIncomeOtherThanSalaryDescription4 + "," +
                                    "        COR_TRN_SALARY_DETAILS.AIS_ITEM_4                                 AS " + strForm16AddAnyOtherIncomeOtherThanSalaryAmount4 + "," +
                                    "        COR_TRN_SALARY_DETAILS.AIS_Total                                  AS " + strForm16Total + "," +
                                    "        COR_TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME                         AS " + strForm16GrossTotalIncome6_7 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_1_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription1 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_1                         AS " + strForm16DeductionsUnderChapterAmount1 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_2_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription2 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_2                         AS " + strForm16DeductionsUnderChapterAmount2 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_3_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription3 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_3                         AS " + strForm16DeductionsUnderChapterAmount3 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_4_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription4 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_4                         AS " + strForm16DeductionsUnderChapterAmount4 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_5_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription5 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_5                         AS " + strForm16DeductionsUnderChapterAmount5 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_6_DESC                    AS " + strForm16DeductionsUnderChapterSection80CDescription6 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_6                         AS " + strForm16DeductionsUnderChapterAmount6 + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_GROSS_TOTAL                    AS " + strForm16GrossTotal80C + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80C_DED_TOTAL                      AS " + strForm16DeductibleTotal80C + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCC_GROSS_AMOUNT                 AS " + strForm16Section80CCCGrossAmount + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCC_DED_AMOUNT                    AS " + strForm16Section80CCCDeductibleAmount + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_GROSS_AMOUNT                 AS " + strForm16Section80CCDGrossAmount + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCD_DED_AMOUNT                   AS " + strForm16Section80CCDDeductibleAmount + "," +
                                    "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT             AS " + strForm16TotalDeductibleAmount80CCE + ",";
                                    //if(TDSMAN.Classes.TDSMAN.T_pFinancialYearId == T_FinancialYearID.F2011_12ID)  //-- Commented By Abhishek Dey On 09/05/2019 --
                                    if (intFinancialYearID == T_FinancialYearID.F2011_12ID) //-- 09/05/2019 --
                                    {
                                        strQueryDD = strQueryDD + "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCF_GROSS_AMOUNT         AS " + strForm16Section80CCGGrossAmount + "," +
                                                                          "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCF_DED_AMOUNT           AS " + strForm16Section80CCGDeductibleAmount + ",";
                                    }
                                    else
                                    {
                                        strQueryDD = strQueryDD + "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCG_GROSS_AMOUNT         AS " + strForm16Section80CCGGrossAmount + "," +
                                                                          "        COR_TRN_SALARY_DETAILS.CVIA_SEC80CCG_DED_AMOUNT           AS " + strForm16Section80CCGDeductibleAmount + ",";
                                    }

                                    strQueryDD = strQueryDD + "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DESC          AS " + strForm16OtherSection + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_GROSS_AMOUNT                AS " + strForm16GrossAmount + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_QUAL_AMOUNT                 AS " + strForm16QualifyingAmount + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DED_AMOUNT                  AS " + strForm16DeductibleAmount + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DESC                        AS " + strForm16OtherSection2 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_GROSS_AMOUNT                AS " + strForm16GrossAmountBM + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_QUAL_AMOUNT                 AS " + strForm16QualifyingAmountBN + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DED_AMOUNT                  AS " + strForm16DeductibleAmountBO + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DESC                        AS " + strForm16OtherSection3 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_GROSS_AMOUNT                AS " + strForm16GrossAmountBQ + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_QUAL_AMOUNT                 AS " + strForm16QualifyingAmountBR + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DED_AMOUNT                  AS " + strForm16DeductibleAmountBS + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DESC                        AS " + strForm16OtherSection4 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_GROSS_AMOUNT                AS " + strForm16GrossAmountBU + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_QUAL_AMOUNT                 AS " + strForm16QualifyingAmountBV + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DED_AMOUNT                  AS " + strForm16DeductibleAmountBW + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DESC                        AS " + strForm16OtherSection5 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_GROSS_AMOUNT                AS " + strForm16GrossAmountBY + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_QUAL_AMOUNT                 AS " + strForm16QualifyingAmountBZ + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DED_AMOUNT                  AS " + strForm16DeductibleAmountCA + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL                          AS " + strForm16TotalDeductibleAmountOtherSections + "," +
                                                 "        COR_TRN_SALARY_DETAILS.CVIA_DED_TOTAL                              AS " + strForm16AggregateOfDeductibleAmountUnderChapter + "," +
                                                 "        COR_TRN_SALARY_DETAILS.TOTAL_INCOME                                AS " + strForm16TotalTaxableIncome344 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.TAX_TOTAL_INCOME                            AS " + strForm16IncomeTaxOnTotalIncome345 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME                           AS " + strForm16SurchargeOnTaxComputedOnSrl12 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME                          AS " + strForm16EducationCessOnTaxComputedOnSrl12346 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.TAX_PAYABLE_AGGREGATE                       AS " + strForm16TaxPayable + "," +
                                                 "        COR_TRN_SALARY_DETAILS.US_89_LESS                                  AS " + strForm16LessReliefUnderSection89 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.TAX_PAYABLE                                 AS " + strForm16NetTaxPayable + "," +
                                                 "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                          AS " + strForm16TotalTDSDeducted + ",";
                                    // if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)  //-- Commented By Abhishek Dey On 09/05/2019 --
                                    if (intFinancialYearID >= T_FinancialYearID.F2013_14ID) //-- 09/05/2019 --
                                    {
                                        strQueryDD = strQueryDD + "        COR_TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT                 AS " + strForm16CurrentEmployerTDS349 + "," +
                                                                   "        COR_TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL               AS " + strForm16PreviousEmployerTDS350 + ",";
                                    }
                                    else
                                    {
                                        strQueryDD = strQueryDD + " ' '                                                AS " + strForm16CurrentEmployerTDS349 + "," +
                                                                   " ' '                                                AS " + strForm16PreviousEmployerTDS350 + ",";
                                    }
                                    //"        COR_TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT                        AS " + strForm16CurrentEmployerTDS349 + "," +
                                    //"        COR_TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL                        AS " + strForm16PreviousEmployerTDS350 + "," +
                                    strQueryDD = strQueryDD + "        COR_TRN_SALARY_DETAILS.SHORTFALL_TAX             AS " + strForm16ShortfallExcessDeductionOfTax + "," +
                                                 "        " + cmnService.J_SQLDBFormat(strTaxDeductedAtHigherRateMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strForm16WhetherTaxDeductedAtHigherRate + ", " +
                                                 //"        COR_TRN_SALARY_DETAILS.                        AS " + strForm16WhetherTaxDeductedAtHigherRate + "," +
                                                 //"        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED                       AS " + strForm16TDSIncludingSuperannuation + "," +
                                                 "        COR_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED + COR_TRN_SALARY_DETAILS.SUPER_ANN_TAX                    AS " + strForm16TDSIncludingSuperannuation + "," +  //-- 21/02/2018 --
                                                                                                                                                                                                          //-- 17/02/2018 --
                                                 "        COR_TRN_SALARY_DETAILS.SUPER_ANN_YN                               AS " + strForm16WhetherContributionsPaidOfSuperannuationFund + ", " +
                                                 //----------------
                                                 //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_YN                        AS " + strForm16WhetherContributionsPaidOfSuperannuationFund + "," +
                                                 "        COR_TRN_SALARY_DETAILS.SUPER_ANN_NAME                             AS " + strForm16NameOfSuperannuationFund + "," +
                                                 //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE                        AS " + strForm16FromDate + "," +
                                                 //"        COR_TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE                          AS " + strForm16ToDate + "," +
                                                 "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16FromDate + "," +
                                                 "        " + cmnService.J_SQLDBFormat("COR_TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strForm16ToDate + "," +
                                                 "        COR_TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT                           AS " + strForm16AmountOfContributionRepaid + "," +
                                                 "        COR_TRN_SALARY_DETAILS.SUPER_ANN_RATE                             AS " + strForm16AverageRateOfDeduction + "," +
                                                 "        COR_TRN_SALARY_DETAILS.SUPER_ANN_TAX                              AS " + strForm16AmountOfTaxDeducted + "," +
                                                 "        COR_TRN_SALARY_DETAILS.SUPER_ANN_INCOME                           AS " + strForm16GrossTotalIncomeCX + "," +
                                                 //-- 17/02/2018 --
                                                 //"        COR_TRN_SALARY_DETAILS.                        AS " + strForm16WhetherRentPaymentExceeds1lakhDuringPreviousYear + "," +
                                                 "        COR_TRN_SALARY_DETAILS.RENT_EXCEEDING_YN                          AS " + strForm16WhetherRentPaymentExceeds1lakhDuringPreviousYear + ", " +     //-- 19/02/2018 --
                                                                                                                                                                                                      //
                                                 "        COR_TRN_SALARY_DETAILS.LANDLORD_1_PAN                             AS " + strForm16PANOfLandlord1 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LANDLORD_1_NAME                            AS " + strForm16NameOfLandlord1 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LANDLORD_2_PAN                             AS " + strForm16PANOfLandlord2 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LANDLORD_2_NAME                            AS " + strForm16NameOfLandlord2 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LANDLORD_3_PAN                             AS " + strForm16PANOfLandlord3 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LANDLORD_3_NAME                            AS " + strForm16NameOfLandlord3 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LANDLORD_4_PAN                             AS " + strForm16PANOfLandlord4 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LANDLORD_4_NAME                            AS " + strForm16NameOfLandlord4 + "," +
                                                 //-- 17/02/2018 --
                                                 //"        COR_TRN_SALARY_DETAILS.                                           AS " + strForm16WhetherInterestPaidExceeds1lakh + "," +
                                                 "        COR_TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER                    AS " + strForm16WhetherInterestPaidExceeds1lakh + ", " +   //-- 19/02/2018 --
                                                                                                                                                                                   //
                                                 "        COR_TRN_SALARY_DETAILS.LENDER_1_PAN                               AS " + strForm16PANOfLender1 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LENDER_1_NAME                              AS " + strForm16NameOfLender1 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LENDER_2_PAN                               AS " + strForm16PANOfLender2 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LENDER_2_NAME                              AS " + strForm16NameOfLender2 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LENDER_3_PAN                               AS " + strForm16PANOfLender3 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LENDER_3_NAME                              AS " + strForm16NameOflender3 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LENDER_4_PAN                               AS " + strForm16PANOfLender4 + "," +
                                                 "        COR_TRN_SALARY_DETAILS.LENDER_4_NAME                              AS " + strForm16NameOfLender4 + " " +
                                                 //-- 19/02/2018 --
                                                 "     FROM  COR_TRN_SALARY_DETAILS  " +
                                                 "     WHERE  COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                                                 "     ORDER  BY  COR_TRN_SALARY_DETAILS.SL_NO    ";
                                }
                                //-----------------
                                #endregion

                            }
                        }
                        #endregion
                        //-- END OF FORM-16 SALARY DETAILS
                        prgBar.Value = prgBar.Value + 5;  //-- 01/01/2018 -- //-- 16/02/2018 --
                        // CHALLAN DETAILS FOR ALL FORM EXCEPT SALARY DETAILS
                        //
                        if (chkSalaryDetails.Checked == false)
                        {
                            //-- Added By Abhishek Dey On 16/02/2018 --
                            if (strFormNo != T_FormNo.F24QForm16SalaryDetails && strFormNo != T_FormNo.F24QSalaryDetails)
                            {
                                //
                                if (intFinancialYearID >= T_FinancialYearID.F2013_14ID)
                                {
                                    string strSubQueryCD = "";
                                    if (rbnRegularReturn.Checked == true)
                                    {
                                        if (dmlService.J_ReturnNoOfRows("SELECT CHALLAN_ID FROM TRN_CHALLAN WHERE INTEREST_ALLOCATED + OTHERS_ALLOCATED > 0 AND BASIC_INFO_ID  = " + lngBasicInfoID , J_QueryType.DirectQuery )>0)
                                        {
                                            strSubQueryCD = ",TRN_CHALLAN.INTEREST_ALLOCATED  AS " + strInterestAllocated + ", " +
                                                            "TRN_CHALLAN.OTHERS_ALLOCATED    AS " + strOtherAllocated ;
                                        }
                                        //--
                                        strQueryCD = " SELECT  TRN_CHALLAN.SL_NO               AS " + strSerialNo + "," +
                                                      "        TRN_CHALLAN.TDS                 AS " + strTDS + "," +
                                                      "        TRN_CHALLAN.SURCHARGE           AS " + strSurcharge + "," +
                                                      "        TRN_CHALLAN.EDUCATION_CESS      AS " + strEducationCess + "," +
                                                      "        TRN_CHALLAN.INTEREST            AS " + strInterest + "," +
                                                      "        TRN_CHALLAN.LATE_FEE            AS " + strFee + "," +
                                                      "        TRN_CHALLAN.OTHERS              AS " + strOther + "," +
                                                      "        " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " AS " + strTotalTaxDeposited + "," +
                                                      //"        \"'\" " + " + TRN_CHALLAN.BSR_CODE  AS " + strBSRCode + "," +
                                                      "        TRN_CHALLAN.BSR_CODE  AS " + strBSRCode + "," +
                                                      //"        TRN_CHALLAN.DEPOSIT_DATE        AS " + strDateOnTaxDeposited + "," +
                                                      "        " + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDateOnTaxDeposited + "," +
                                                      //"        \"'\"+ " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrix, J_SQLColFormat.Case_End) + " AS " + strTransferVoucherChallanSerialNo + "," +
                                                      "        " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrix, J_SQLColFormat.Case_End) + " AS " + strTransferVoucherChallanSerialNo + "," +
                                                      "        " + cmnService.J_SQLDBFormat(strLoadChallanBookEntryMatrix, J_SQLColFormat.Case_End) + " AS " + strTdsDeposited + "," +
                                                      "        MST_MINOR_HEAD.MINOR_HEAD_CODE  AS " + strMinorHead + " " + strSubQueryCD +
                                                      " FROM   (TRN_CHALLAN LEFT JOIN MST_MINOR_HEAD " +
                                                      " ON     TRN_CHALLAN.MINOR_HEAD_ID  = MST_MINOR_HEAD.MINOR_HEAD_ID)" +
                                                      " WHERE  TRN_CHALLAN.BASIC_INFO_ID  = " + lngBasicInfoID + " " +
                                                      " ORDER BY TRN_CHALLAN.CHALLAN_ID ";
                                    }
                                    else if (rbnCorrectionReturn.Checked == true)
                                    {
                                        if (dmlService.J_ReturnNoOfRows("SELECT TRN_CHALLAN_ID FROM COR_TRN_CHALLAN WHERE INTEREST_ALLOCATED + OTHERS_ALLOCATED > 0 AND BATCH_HEADER_ID  = " + lngBasicInfoID, J_QueryType.DirectQuery) > 0)
                                        {
                                            strSubQueryCD = ",COR_TRN_CHALLAN.INTEREST_ALLOCATED  AS " + strInterestAllocated + ", " +
                                                            "COR_TRN_CHALLAN.OTHERS_ALLOCATED    AS " + strOtherAllocated;
                                        }
                                        //--
                                        strQueryCD = " SELECT  COR_TRN_CHALLAN.SL_NO               AS " + strSerialNo + "," +
                                                      "        COR_TRN_CHALLAN.TDS                 AS " + strTDS + "," +
                                                      "        COR_TRN_CHALLAN.SURCHARGE           AS " + strSurcharge + "," +
                                                      "        COR_TRN_CHALLAN.EDUCATION_CESS      AS " + strEducationCess + "," +
                                                      "        COR_TRN_CHALLAN.INTEREST            AS " + strInterest + "," +
                                                      "        COR_TRN_CHALLAN.LATE_FEE            AS " + strFee + "," +
                                                      "        COR_TRN_CHALLAN.OTHERS              AS " + strOther + "," +
                                                      "        " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strTotalTaxDeposited + "," +
                                                      //"        \"'\" " + " + COR_TRN_CHALLAN.BSR_CODE  AS " + strBSRCode + "," +
                                                      "        COR_TRN_CHALLAN.BSR_CODE  AS " + strBSRCode + "," +
                                                      //"        COR_TRN_CHALLAN.DEPOSIT_DATE        AS " + strDateOnTaxDeposited + "," +
                                                      "        " + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDateOnTaxDeposited + "," +
                                                      //"        \"'\"+ " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrix, J_SQLColFormat.Case_End) + " AS " + strTransferVoucherChallanSerialNo + "," +
                                                      "        " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strTransferVoucherChallanSerialNo + "," +
                                                      "        " + cmnService.J_SQLDBFormat(strLoadChallanBookEntryMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strTdsDeposited + "," +
                                                      "        MST_MINOR_HEAD.MINOR_HEAD_CODE      AS " + strMinorHead + " " + strSubQueryCD + 
                                                      " FROM   (COR_TRN_CHALLAN LEFT JOIN MST_MINOR_HEAD " +
                                                      " ON     COR_TRN_CHALLAN.MINOR_HEAD_ID  = MST_MINOR_HEAD.MINOR_HEAD_ID)" +
                                                      " WHERE  COR_TRN_CHALLAN.BATCH_HEADER_ID= " + lngBasicInfoID + " " +
                                                      " ORDER BY COR_TRN_CHALLAN.TRN_CHALLAN_ID ";
                                    }
                                    //-----------------------------------------------------------
                                    //strSQL = strQuery + "ORDER BY " + strOrderBy;
                                    //-----------------------------------------------------------  
                                }
                                else
                                {
                                    if (rbnRegularReturn.Checked == true)
                                        strQueryCD = " SELECT TRN_CHALLAN.SL_NO               AS " + strSerialNo + "," +
                                                 "        MST_SECTION.SECTION_NO          AS " + strSection + "," +
                                                 "        TRN_CHALLAN.TDS                 AS " + strTDS + "," +
                                                 "        TRN_CHALLAN.SURCHARGE           AS " + strSurcharge + "," +
                                                 "        TRN_CHALLAN.EDUCATION_CESS      AS " + strEducationCess + "," +
                                                 "        TRN_CHALLAN.INTEREST            AS " + strInterest + "," +
                                                 "        TRN_CHALLAN.LATE_FEE            AS " + strFee + "," +
                                                 "        TRN_CHALLAN.OTHERS              AS " + strOther + "," +
                                                 "        " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " AS " + strTotalTaxDeposited + "," +
                                                 "        TRN_CHALLAN.CHEQUE_NO           AS " + strChequeNo + "," +
                                                 //"        \"'\" " + " + TRN_CHALLAN.BSR_CODE            AS " + strBSRCode + "," +
                                                 "        TRN_CHALLAN.BSR_CODE            AS " + strBSRCode + "," +
                                                  //"        TRN_CHALLAN.DEPOSIT_DATE        AS " + strDateOnTaxDeposited + "," +
                                                  "        " + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDateOnTaxDeposited + "," +
                                                 //"        \"'\"+ " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrix, J_SQLColFormat.Case_End) + " AS " + strTransferVoucherChallanSerialNo + "," +
                                                 "        " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrix, J_SQLColFormat.Case_End) + " AS " + strTransferVoucherChallanSerialNo + "," +
                                                 "        " + cmnService.J_SQLDBFormat(strLoadChallanBookEntryMatrix, J_SQLColFormat.Case_End) + " AS " + strTdsDeposited + " " +
                                                 " FROM  (TRN_CHALLAN LEFT JOIN MST_SECTION " +
                                                 "       ON  TRN_CHALLAN.SECTION_ID    = MST_SECTION.SECTION_ID)" +
                                                 " WHERE  TRN_CHALLAN.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                                                 " ORDER BY TRN_CHALLAN.CHALLAN_ID ";
                                    else if (rbnCorrectionReturn.Checked == true)
                                        strQueryCD = " SELECT COR_TRN_CHALLAN.SL_NO               AS " + strSerialNo + "," +
                                                 "        MST_SECTION.SECTION_NO          AS " + strSection + "," +
                                                 "        COR_TRN_CHALLAN.TDS                 AS " + strTDS + "," +
                                                 "        COR_TRN_CHALLAN.SURCHARGE           AS " + strSurcharge + "," +
                                                 "        COR_TRN_CHALLAN.EDUCATION_CESS      AS " + strEducationCess + "," +
                                                 "        COR_TRN_CHALLAN.INTEREST            AS " + strInterest + "," +
                                                 "        COR_TRN_CHALLAN.LATE_FEE            AS " + strFee + "," +
                                                 "        COR_TRN_CHALLAN.OTHERS              AS " + strOther + "," +
                                                 "        " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strTotalTaxDeposited + "," +
                                                 "        COR_TRN_CHALLAN.CHEQUE_NO           AS " + strChequeNo + "," +
                                                 //"        \"'\" " + " + COR_TRN_CHALLAN.BSR_CODE            AS " + strBSRCode + "," +
                                                 "        COR_TRN_CHALLAN.BSR_CODE            AS " + strBSRCode + "," +
                                                  //"        COR_TRN_CHALLAN.DEPOSIT_DATE        AS " + strDateOnTaxDeposited + "," +
                                                  "        " + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS " + strDateOnTaxDeposited + "," +
                                                 //"        \"'\"+ " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrix, J_SQLColFormat.Case_End) + " AS " + strTransferVoucherChallanSerialNo + "," +
                                                 "        " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strTransferVoucherChallanSerialNo + "," +
                                                 "        " + cmnService.J_SQLDBFormat(strLoadChallanBookEntryMatrixCorr, J_SQLColFormat.Case_End) + " AS " + strTdsDeposited + " " +
                                                 " FROM  (COR_TRN_CHALLAN LEFT JOIN MST_SECTION " +
                                                 "       ON  COR_TRN_CHALLAN.SECTION_ID    = MST_SECTION.SECTION_ID)" +
                                                 " WHERE  COR_TRN_CHALLAN.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                                                 " ORDER BY COR_TRN_CHALLAN.TRN_CHALLAN_ID ";

                                    //-----------------------------------------------------------
                                    //strSQL = strQuery + "ORDER BY " + strOrderBy;
                                    //----------------------------------------------------------- 
                                }
                                //
                            }
                        }
                        //
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
                        prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                        //--
                        if (strFormNo == T_FormNo.F24Q)
                        {
                            if (chkSalaryDetails.Checked == true)
                            {
                                if (ExportToExcelFromSQL(strQueryDD, "SALARY DETAILS") == false)
                                {
                                    cmnService.J_UserMessage("Export data failed ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    prgBar.Value = 0;  //-- 01/01/2018  --
                                    return;
                                }
                            }
                            else if (ExportToExcelFromSQL(strQueryDD, "EMPLOYEE DETAILS") == false)
                            {
                                //cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cmnService.J_UserMessage("Export data failed ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                prgBar.Value = 0;  //-- 01/01/2018  --
                                return;
                            }
                        }
                        else if (strFormNo == T_FormNo.F24QSalaryDetails)
                        {
                            if (ExportToExcelFromSQL(strQueryDD, "SALARY DETAILS") == false)
                            {
                                //cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cmnService.J_UserMessage("Export data failed ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                prgBar.Value = 0;  //-- 01/01/2018  --
                                return;
                            }
                        }
                        else if (strFormNo == T_FormNo.F24QForm16SalaryDetails)
                        {
                            if (ExportToExcelFromSQL(strQueryDD, "FORM16-DETAILS") == false)
                            {
                                //cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cmnService.J_UserMessage("Export data failed ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                prgBar.Value = 0;  //-- 01/01/2018  --
                                return;
                            }
                        }
                        else
                        {
                            if (ExportToExcelFromSQL(strQueryDD, "DEDUCTEE DETAILS") == false)
                            {
                                //cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cmnService.J_UserMessage("Export data failed ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                prgBar.Value = 0;  //-- 01/01/2018  --
                                return;
                            }
                        }
                        //                       
                        //--
                        if (chkSalaryDetails.Checked == false)
                        {
                            if (strFormNo != T_FormNo.F24QSalaryDetails && strFormNo != T_FormNo.F24QForm16SalaryDetails)  //-- 29/12/2017 --
                            {
                                if (CREATE_NEW_WORKSHEET(strExcelFilePath, "CHALLAN DETAILS") == false) return;
                                //
                                prgBar.Value = prgBar.Value + 5;
                                //
                                if (ExportToExcelFromSQL(strQueryCD, "CHALLAN DETAILS") == false)
                                {
                                    //cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmnService.J_UserMessage("Export data failed ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    prgBar.Value = 0;  //-- 01/01/2018  --
                                    return;
                                }
                            }
                        }
                        //--
                        prgBar.Value = prgBar.Value + 5; 
                        //---------------------------
                        this.Cursor = Cursors.Default;
                        grpExport.Visible = false;
                        //--
                        cmnService.J_UserMessage("Data Exported Successfully...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //
                        //-- 01/01/2017 --
                        ClearControls();  
                        //--
                        LoadControls();
                        //
                        tbcExportData.Enabled = true;
                        grpButtons.Enabled = true;
                        //
                        btnNext.Enabled = false;
                        btnNext.BackColor = System.Drawing.Color.LightGray;
                        //--
                        grpControlSummary.Visible = false;
                        pnlLine1.Visible = false;
                        prgBar.Value = 0;
                        //--
                        cmbFinancialYear.Select();
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
            //--------------------------------------------
        }

        #endregion

        #region Batch

        #region dgcViewBatch_Click
        private void dgcViewBatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgcViewBatch.CurrentRow == null)
                {
                    lngBasicInfoID = 0;
                    //ClearFields();
                    //LoadDeducteeGrid();
                    return;
                }

                if (dgcViewBatch.CurrentRow.Index < 0)
                {
                    lngBasicInfoID = 0;
                    //ClearFields();
                    return;
                }
                //
                //ClearFields();
                //
                lngBasicInfoID = Convert.ToInt64(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[0].Value);
                //
                ControlSummaryBasicInfo(lngBasicInfoID, true);
                //
            }
            catch(Exception err)
            {
            }
        }
        #endregion

        #region dgcViewBatch_KeyDown
        private void dgcViewBatch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgcViewBatch.CurrentRow.Index < 0) return;
                //
                //ClearFields();
                //
                lngBasicInfoID = Convert.ToInt64(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[0].Value);
                //
                //LoadDeducteeGridCorr(lngBasicInfoID, chkBoxNewEntriesOnly.Checked);
                //strTempMode = lblMode.Text;
            }
            catch
            {
                //cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region dgcViewBatch_CurrentCellChanged
        private void dgcViewBatch_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgcViewBatch.CurrentRow == null) return;
            dgcViewBatch_Click(sender, e);
        }
        #endregion


        #region chkSalaryDetails_CheckedChanged
        private void chkSalaryDetails_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgcViewBatch.CurrentRow == null)
                {
                    lngBasicInfoID = 0;
                    //ClearFields();
                    //LoadDeducteeGrid();
                    return;
                }

                if (dgcViewBatch.CurrentRow.Index < 0)
                {
                    lngBasicInfoID = 0;
                    //ClearFields();
                    return;
                }
                //
                //ClearFields();
                //
                lngBasicInfoID = Convert.ToInt64(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[0].Value);
                //
                if (chkSalaryDetails.Checked == true)
                {
                    grpControlSalary.Visible = true;
                    grpControlSummary.Visible = false;
                    //
                    txtNoOfRecords.Text = Convert.ToString(dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM COR_TRN_SALARY_DETAILS WHERE BATCH_HEADER_ID = " + lngBasicInfoID, J_QueryType.DirectQuery));
                    txtNetTaxableIncome.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM COR_TRN_SALARY_DETAILS WHERE BATCH_HEADER_ID = " + lngBasicInfoID)) == "" ? "0.00" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM COR_TRN_SALARY_DETAILS WHERE BATCH_HEADER_ID = " + lngBasicInfoID))));

                    //lblNetTaxableIncome.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM COR_TRN_SALARY_DETAILS WHERE BATCH_HEADER_ID = " + lngBasicInfoID)) == "" ? "0.00" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM COR_TRN_SALARY_DETAILS WHERE BATCH_HEADER_ID = " + lngBasicInfoID))));
                    //lblNetTaxableIncome.Visible = true;
                    //lblNetTaxableIncomeCaption.Visible = true;
                }
                else
                {
                    //lblNetTaxableIncome.Visible = false;
                    //lblNetTaxableIncomeCaption.Visible = false;
                    grpControlSalary.Visible = false;
                    grpControlSummary.Visible = true;
                }
                //
            }
            catch (Exception err)
            {
            }
        }
        #endregion

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
            //grpButtons.Enabled = true;
            //
            txtTotalChallanRecords.Text = "";
            txtTotalDeducteeRecords.Text = "";
            txtTotalChallanAmount.Text = "";
            txtTotalDeducteeTDS.Text = "";
            txtAmountPaid.Text = "";
            txtExcelPath.Text = string.Empty;
            txtDestinationFileName.Text = string.Empty;
            //
            //txtReceiptNo.Text = "";
            //txtDateofFiling.Text = "";
            //txtTokenNo.Text = "";
            //
            cmbQuarter.Enabled = true;  //-- 29/12/2017 --
            //-- 15/02/2018 --
            txtNoOfRecords.Text = string.Empty;
            txtNetTaxableIncome.Text = string.Empty;
            txtTotalChallanAmount.Text = string.Empty;
            lblNetTaxableIncome.Text = string.Empty;
            grpControlSummary.Visible = false;
            grpControlSalary.Visible = false;
            grpExportOption.Visible = false;
            //--
            rbnCSVOption.Checked = false;
            rbnExcelOption.Checked = false;
            //--
            lblOnlyCSVMessage.Text = "Only CSV export for records > " + dblMaxExcelXLSXMaxRows.ToString() + ".\nOpen the CSV file in Text Editor only.";
            lblOnlyCSVMessage.Visible = false;
            //----------------
        }
        #endregion

        #region LoadControls
        private void LoadControls()
        {
            //
            blnSelectComboExit = true;
            //-----------
            //-- FINANCIAL YEAR
            //-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //-- QUARTER
            //-----------
            string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter);
            //-----------
            //-- FORM NO
            //-----------
            //string[] strFormNo1 ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QForm16SalaryDetails };
            string[] strFormNo1 = { T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")", T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")", T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")", T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")", T_FormNo.F24QSalaryDetails };
            dmlService.J_PopulateComboBox(strFormNo1, ref cmbFormNo);
            //-----------
            //-- COMPANY
            //-----------
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME + ' [' + TAN_NO + ']' " +
                "      FROM   MST_COMPANY " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //
            blnSelectComboExit = false;
            //-----------
        }
        #endregion

        #region ValidateFields

        private bool ValidateFields()
        {
            if (string.IsNullOrEmpty(cmbFinancialYear.Text.Trim()))
            {

                return false;
            }

            if (string.IsNullOrEmpty(cmbFormNo.Text.Trim()))
            {

                return false;
            }

            if (string.IsNullOrEmpty(cmbQuarter.Text.Trim()))
            {

                return false;
            }

            if (string.IsNullOrEmpty(cmbCompany.Text.Trim()))
            {

                return false;
            }
            return true;
        }

        #endregion

        #region ControlSummaryBasicInfo
        private void ControlSummaryBasicInfo(long BasicInfoID, bool CorrectionData)
        {
            if (BasicInfoID == 0)
            {
                txtTotalChallanRecords.Text = "";
                txtTotalDeducteeRecords.Text = "";
                txtTotalChallanAmount.Text = "";
                txtTotalDeducteeTDS.Text = "";
                txtAmountPaid.Text = "";
                grpControlSummary.Visible = false;
                grpControlSalary.Visible = false;  //-- 01/01/2017 --
                pnlLine1.Visible = false;
                //pnlLine2.Visible = false;
                btnNext.Enabled = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.BackColor = System.Drawing.Color.LightGray; 
                return;
            }
            grpControlSummary.Visible = true;
            grpExportOption.Visible = true; //-- 2019/01/22
            grpControlSalary.Visible = false;  //-- 01/01/2018 --
            pnlLine1.Visible = true;
            //pnlLine2.Visible = true;
            //lblTotalRecordsReturn.Visible = true;
            btnNext.Enabled = true;
            btnNext.BackColor = System.Drawing.Color.Lavender; 
            txtTotalChallanRecords.Text = "0";
            txtTotalDeducteeRecords.Text = "0";
            txtTotalChallanAmount.Text = "0.00";
            txtTotalDeducteeTDS.Text = "0.00";
            txtAmountPaid.Text = "0.00";

            if (CorrectionData == false)
            {
                chkSalaryDetails.Visible = false;
                // CONTROL SUMMARY VALUES
                txtTotalChallanRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(CHALLAN_ID) AS COUNT_CHALLAN_ID FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID));
                txtTotalDeducteeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID));
                txtTotalChallanAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID))));
                txtTotalDeducteeTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
                txtAmountPaid.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
                //
                long lngTotalRecordsReturn = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID, J_QueryType.DirectQuery);
                lblTotalRecordsReturn.Text = "Total Records in this return : " + Convert.ToString(lngTotalRecordsReturn);
                //--
                string strFormNo = cmbFormNo.Text.Substring(cmbFormNo.Text.IndexOf('(') + 1, cmbFormNo.Text.IndexOf(')') - cmbFormNo.Text.IndexOf('(') - 1);
                //--
                if (strFormNo == T_FormNo.F24Q && cmbQuarter.Text == T_Qtr.Q4)
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
            }
            else if(CorrectionData == true)
            {
                chkSalaryDetails.Visible = false;
                // CONTROL SUMMARY VALUES
                txtTotalChallanRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(TRN_CHALLAN_ID) AS COUNT_CHALLAN_ID FROM COR_TRN_CHALLAN WHERE BATCH_HEADER_ID = " + BasicInfoID));
                txtTotalDeducteeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(TRN_DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BasicInfoID));
                txtTotalChallanAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM COR_TRN_CHALLAN WHERE BATCH_HEADER_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM COR_TRN_CHALLAN WHERE BATCH_HEADER_ID = " + BasicInfoID))));
                txtTotalDeducteeTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BasicInfoID))));
                txtAmountPaid.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BasicInfoID))));
                //
                long lngTotalRecordsReturn = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BasicInfoID, J_QueryType.DirectQuery);
                lblTotalRecordsReturn.Text = "Total Records in this return : " + Convert.ToString(lngTotalRecordsReturn);
                //
                //if (chkSalaryDetails.Checked==true)
                //{
                //    grpControlSalary.Visible = true;
                //    grpControlSummary.Visible = false;
                //    //
                //    lblNetTaxableIncome.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM COR_TRN_SALARY_DETAILS WHERE BATCH_HEADER_ID = " + lngBasicInfoID)) == "" ? "0.00" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM COR_TRN_SALARY_DETAILS WHERE BATCH_HEADER_ID = " + lngBasicInfoID))));
                //    lblNetTaxableIncome.Visible = true;
                //    lblNetTaxableIncomeCaption.Visible = true;
                //}
                //else
                //{
                //    //lblNetTaxableIncome.Visible = false;
                //    //lblNetTaxableIncomeCaption.Visible = false;
                //    grpControlSalary.Visible = false ;
                //    grpControlSummary.Visible = true;
                //}
                //--
                if (dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[2].Value.ToString() == T_FormNo.F24Q
                   && dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[3].Value.ToString() == T_Qtr.Q4)
                {
                    chkSalaryDetails.Visible = true;
                    chkSalaryDetails.Checked = false;
                }
                else
                    chkSalaryDetails.Visible = false;
            }

            //-- 2023/09/13
            if (Convert.ToDouble(txtTotalDeducteeRecords.Text) > dblMaxExcelXLSXMaxRows)
            {
                rbnExcelOption.Enabled = false;
                rbnCSVOption.Checked = true; 
                lblOnlyCSVMessage.Visible = true;
            }
            else
            {
                rbnExcelOption.Enabled = true;
                rbnCSVOption.Checked = false;
                rbnExcelOption.Checked = false;
                lblOnlyCSVMessage.Visible = false;
            }
            //
        }
        #endregion

        #region ReturnFilingStatus
        private void ReturnFilingStatus(long BasicInfoId)
        {
            IDataReader drdShowRecord = null;
            //
            try
            {
                //txtReceiptNo.Text = "";
                //txtDateofFiling.Text = "";
                //txtTokenNo.Text = "";                    
                //  
                if (BasicInfoId == 0)
                {
                    //grpReturnFilingStatus.Visible = false;
                    //pnlLine2.Visible = false;               
                    //
                    return;
                }
                //grpReturnFilingStatus.Visible = true;
                //pnlLine2.Visible = true; 
                // CHECK IF CHALLAN EXISTS
                strSQL = "SELECT RECEIPT_NO," +
                    "            " + cmnService.J_SQLDBFormat("DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DATE_OF_FILING," +
                    "            PRN_NO," +
                    "            PREV_FILED," +
                    "            PREV_PRN_NO " +
                    "     FROM   TRN_BASIC_INFO " +
                    "     WHERE  BASIC_INFO_ID = " + BasicInfoId;
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                    return;
                //
                while (drdShowRecord.Read())
                {
                    //
                    //txtReceiptNo.Text = Convert.ToString(drdShowRecord["RECEIPT_NO"]);
                    //txtDateofFiling.Text = Convert.ToString(drdShowRecord["DATE_OF_FILING"]);
                    //txtTokenNo.Text = Convert.ToString(drdShowRecord["PRN_NO"]);                    
                    //--
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    return;
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
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
                //--
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                //
                object misValue = System.Reflection.Missing.Value;
                //--
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

        //-- 19/02/2018 --
        #region WRITE READ ME WORKSHEET
        private bool WRITE_READ_ME_WORKSHEET(string ExcelFilePath, string SheetName, string FormName)
        {
            try
            {
                if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
                //--
                //if (KILL_EXCEL() == false)
                //    return false;
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
                #region F24QForm16SalaryDetails
                if (FormName == T_FormNo.F24QForm16SalaryDetails)
                {
                    wsnew.get_Range("A1", m).Value2 = "Category of the Employee (331)";
                    wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("A1", m).Borders.Value = true;
                    wsnew.get_Range("A1", m).ColumnWidth = 50;
                    wsnew.get_Range("A1", m).RowHeight = 15;
                    wsnew.get_Range("A1", m).WrapText = true;
                    wsnew.get_Range("A1", m).Font.Name = "Arial";
                    wsnew.get_Range("A1", m).Font.Bold = true;
                    wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);  //-- 21/02/2018 --
                    //                    
                    wsnew.get_Range("A2", m).Value2 = "G - GENERAL";
                    wsnew.get_Range("A2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("A2", m).ColumnWidth = 50;
                    wsnew.get_Range("A2", m).RowHeight = 15;
                    wsnew.get_Range("A2", m).WrapText = true;
                    wsnew.get_Range("A2", m).Font.Name = "Arial";
                    wsnew.get_Range("A2", m).Font.Size = 10;
                    //                    
                    wsnew.get_Range("A3", m).Value2 = "W - WOMAN";
                    wsnew.get_Range("A3", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("A3", m).ColumnWidth = 50;
                    wsnew.get_Range("A3", m).RowHeight = 15;
                    wsnew.get_Range("A3", m).WrapText = true;
                    wsnew.get_Range("A3", m).Font.Name = "Arial";
                    wsnew.get_Range("A3", m).Font.Size = 10;
                    //                    
                    wsnew.get_Range("A4", m).Value2 = "S - SENIOR CITIZEN";
                    wsnew.get_Range("A4", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("A4", m).ColumnWidth = 50;
                    wsnew.get_Range("A4", m).RowHeight = 15;
                    wsnew.get_Range("A4", m).WrapText = true;
                    wsnew.get_Range("A4", m).Font.Name = "Arial";
                    wsnew.get_Range("A4", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A5", m).Value2 = "O - VERY SENIOR CITIZEN";
                    wsnew.get_Range("A5", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("A5", m).ColumnWidth = 50;
                    wsnew.get_Range("A5", m).RowHeight = 15;
                    wsnew.get_Range("A5", m).WrapText = true;
                    wsnew.get_Range("A5", m).Font.Name = "Arial";
                    wsnew.get_Range("A5", m).Font.Size = 10;
                    //
                    //wsnew.get_Range("A7", m).Borders.Value = true;
                    //wsnew.get_Range("A7", m).ColumnWidth = 50;
                    //wsnew.get_Range("A7", m).RowHeight = 15;
                    //wsnew.get_Range("A7", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 153));
                    //
                    //wsnew.get_Range("B7", m).Value2 = "MANDATORY FIELD";
                    //wsnew.get_Range("B7", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    //wsnew.get_Range("B7", m).ColumnWidth = 50;
                    //wsnew.get_Range("B7", m).RowHeight = 15;
                    //wsnew.get_Range("B7", m).WrapText = true;
                    //wsnew.get_Range("B7", m).Font.Name = "Arial";
                    //wsnew.get_Range("B7", m).Font.Size = 10;
                    ////
                    //wsnew.get_Range("A8", m).Borders.Value = true;
                    //wsnew.get_Range("A8", m).ColumnWidth = 50;
                    //wsnew.get_Range("A8", m).RowHeight = 15;
                    //wsnew.get_Range("A8", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    ////
                    //wsnew.get_Range("B8", m).Value2 = "CALCULATED FIELD";
                    //wsnew.get_Range("B8", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    //wsnew.get_Range("B8", m).ColumnWidth = 50;
                    //wsnew.get_Range("B8", m).RowHeight = 15;
                    //wsnew.get_Range("B8", m).WrapText = true;
                    //wsnew.get_Range("B8", m).Font.Name = "Arial";
                    //wsnew.get_Range("B8", m).Font.Size = 10;
                    ////
                    //wsnew.get_Range("A9", m).Borders.Value = true;
                    //wsnew.get_Range("A9", m).ColumnWidth = 50;
                    //wsnew.get_Range("A9", m).RowHeight = 15;
                    //wsnew.get_Range("A9", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(204, 255, 204));
                    ////
                    //wsnew.get_Range("B9", m).Value2 = "OPTIONAL FIELD";
                    //wsnew.get_Range("B9", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    //wsnew.get_Range("B9", m).ColumnWidth = 50;
                    //wsnew.get_Range("B9", m).RowHeight = 15;
                    //wsnew.get_Range("B9", m).WrapText = true;
                    //wsnew.get_Range("B9", m).Font.Name = "Arial";
                    //wsnew.get_Range("B9", m).Font.Size = 10;
                }
                #endregion              
                //                        
                wb.Save();
                wb.Close(m, m, m);
                wbs.Close();                

                wb = null;
                wbs = null;
                wsnew = null;
               
                //
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
        //----------------

        //-- 20/02/2018 --
        #region WRITE_REMARKS_WORKSHEET
        private bool WRITE_REMARKS_WORKSHEET(string ExcelFilePath, string SheetName, string FormNo)
        {
            try
            {
                if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
                //
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

        #region WRITE SECTION WORKSHEET
        private bool WRITE_SECTION_WORKSHEET(string ExcelFilePath, string SheetName, string FormNo)
        {
            try
            {
                if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
                //--
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

        #region WRITE RECEIPT NO. WORKSHEET
        private bool WRITE_RECEIPT_NO_REG_WORKSHEET(string ExcelFilePath, string SheetName, long BasicInfo)
        {
            try
            {
                if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
                //--
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
                wsnew.get_Range("A1", m).Value2 = "Receipt No.";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 65;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "Date of Filing";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 40;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("C1", m).Value2 = "Token No.";
                wsnew.get_Range("C1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C1", m).Borders.Value = true;
                wsnew.get_Range("C1", m).ColumnWidth = 65;
                wsnew.get_Range("C1", m).WrapText = true;
                wsnew.get_Range("C1", m).Font.Name = "Arial";
                wsnew.get_Range("C1", m).Font.Bold = true;
                wsnew.get_Range("C1", m).Font.Size = 10;
                wsnew.get_Range("C1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("C1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT TRN_BASIC_INFO.BASIC_INFO_ID," +
                    "            " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.RECEIPT_NO", J_SQLColFormat.ConvertToString) + "        AS RECEIPT_NO," +
                    "            " + cmnService.J_SQLDBFormat(cmnService.J_SQLDBFormat("TRN_BASIC_INFO.DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY), J_SQLColFormat.ConvertToString) + " AS DATE_OF_FILING," +
                    "            " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.PRN_NO", J_SQLColFormat.ConvertToString) + "            AS TOKEN_NO " +
                    "     FROM   TRN_BASIC_INFO " +
                    "     WHERE  TRN_BASIC_INFO.BASIC_INFO_ID = " + BasicInfo + " ";
                //if (FormNo == T_FormNo.F24Q)
                //    strSQL = strSQL + "AND SECTION_NO <> '' ";
                //strSQL = strSQL + "ORDER BY SECTION_ID";
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
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["RECEIPT_NO"].ToString();
                    //wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["DATE_OF_FILING"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("C" + lngSheetRow, m).Value2 = drdGetSheetRecord["TOKEN_NO"].ToString();
                    wsnew.get_Range("C" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //

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

        #region WRITE RECEIPT NO. WORKSHEET
        private bool WRITE_RECEIPT_NO_CORR_WORKSHEET(string ExcelFilePath, string SheetName, long BasicInfo)
        {
            try
            {
                if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
                //--
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
                wsnew.get_Range("A1", m).Value2 = "Receipt No.";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 65;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "Date of Filing";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 40;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("C1", m).Value2 = "Token No.";
                wsnew.get_Range("C1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C1", m).Borders.Value = true;
                wsnew.get_Range("C1", m).ColumnWidth = 65;
                wsnew.get_Range("C1", m).WrapText = true;
                wsnew.get_Range("C1", m).Font.Name = "Arial";
                wsnew.get_Range("C1", m).Font.Bold = true;
                wsnew.get_Range("C1", m).Font.Size = 10;
                wsnew.get_Range("C1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("C1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT COR_HDR_BATCH.BATCH_HEADER_ID," +
                    "            " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.RECEIPT_NO", J_SQLColFormat.ConvertToString) + "        AS RECEIPT_NO," +
                    "            " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DATE_OF_FILING," +
                    "            " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.PRN_NO", J_SQLColFormat.ConvertToString) + "            AS TOKEN_NO " +
                    "     FROM   COR_HDR_BATCH " +
                    "     WHERE  COR_HDR_BATCH.BATCH_HEADER_ID = " + BasicInfo + " ";
                //if (FormNo == T_FormNo.F24Q)
                //    strSQL = strSQL + "AND SECTION_NO <> '' ";
                //strSQL = strSQL + "ORDER BY SECTION_ID";
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
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["RECEIPT_NO"].ToString();
                    //wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["DATE_OF_FILING"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("C" + lngSheetRow, m).Value2 = drdGetSheetRecord["TOKEN_NO"].ToString();
                    wsnew.get_Range("C" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //

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
                if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
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

        #region WRITE COUNTRY WORKSHEET
        private bool WRITE_COUNTRY_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
                //--
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
                wsnew.get_Range("A1", m).Value2 = "COUNTRY";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 55;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "COUNTRY CODE";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 25;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = "SELECT COUNTRY_ID," +
                        "            COUNTRY_DESC," +
                        "            CAST(COUNTRY_CODE AS VARCHAR) AS COUNTRY_CODE " +
                        "     FROM   MST_COUNTRY ";
                else
                    strSQL = "SELECT COUNTRY_ID," +
                        "            COUNTRY_DESC," +
                        "            CSTR(COUNTRY_CODE) AS COUNTRY_CODE " +
                        "     FROM   MST_COUNTRY ";
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
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["COUNTRY_DESC"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = "'" + Convert.ToString(drdGetSheetRecord["COUNTRY_CODE"]);
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

        #region WRITE REMITTANCE WORKSHEET
        private bool WRITE_REMITTANCE_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
                //
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
                wsnew.get_Range("A1", m).Value2 = "REMITTANCE";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 55;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "REMITTANCE CODE";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 25;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT REMITTANCE_ID," +
                    "            REMITTANCE_DESC," +
                    "            REMITTANCE_CODE " +
                    "     FROM   MST_REMITTANCE " +
                    "     WHERE  INACTIVE_FLAG = 0";
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
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["REMITTANCE_DESC"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["REMITTANCE_CODE"].ToString();
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

        #region WRITE TDS_RATE_ACT WORKSHEET
        private bool WRITE_TDS_RATE_ACT_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
                //
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
                wsnew.get_Range("A1", m).Value2 = "TDS RATE ACT";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 50;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "TDS RATE ACT CODE";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 25;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT TDS_APPLICABILITY_ID," +
                    "            TDS_APPLICABILITY_DESC," +
                    "            TDS_APPLICABILITY_CODE " +
                    "     FROM   MST_TDS_APPLICABILITY ";
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
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["TDS_APPLICABILITY_DESC"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["TDS_APPLICABILITY_CODE"].ToString();
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

        //-- Added By Abhishek Dey On 21/05/2018 --
        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnExportDataToExcel");
            Tan.ShowDialog();
            //--------------
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=EztMuIUEnAo");
        }
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
                    if(dt.Rows[i][k].ToString().Contains(",") == true)
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
        #region LoadCorrectionBatch
        private void LoadCorrectionBatch()
        {
            string[,] strMatrixBatchGrid = null; string strOrderBy = ""; string strQuery = ""; DataSet dsetGridClone = new DataSet();

            //-----------------------------------------------------------
            string[,] strMatrixBatch = {{"BATCH_HEADER_ID", "0", "", "Right", "", "F", ""},
                                        {"FA Year", "70", "S", "", "", "", ""},
                                        {"Form No", "75", "S", "", "", "", ""},
                                        {"Qtr", "35", "", "", "", "", ""},
                                        {"Company Name", "250", "", "", "", "", "T"},
                                        {"TAN No.", "90", "", "", "", "", ""},
                                        {"Imported date & Time", "140", "dd/MM/yyyy", "", "", "", ""},
                                        {"Total Corrections", "100", "", "Right", "", "", ""},
                                        {"FA Year Id", "0", "", "", "", "F", ""}};
            //-----------------------------------------------------------
            strMatrixBatchGrid = strMatrixBatch;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------

            string[,] strLoadCorrectionMatrix = {{"COR_TRN_COMPANY.MODE = '" + T_CorrectionMode.TANUpdation + "'", "F", "Cancelled", "T"},
                                                 {"JAYA", "F", "COR_HDR_BATCH.TOTAL_CORRECTION", "F"}};

            strOrderBy = "COR_HDR_BATCH.BATCH_HEADER_ID DESC";
            strQuery = "SELECT COR_HDR_BATCH.BATCH_HEADER_ID  AS BATCH_HEADER_ID," +
                      "        MST_ASSESSMENT.FA_YEAR         AS FA_YEAR," +
                      "        COR_HDR_BATCH.FORM_NO          AS FORM_NO," +
                      "        COR_HDR_BATCH.QTR              AS QTR," +
                      "        COR_TRN_COMPANY.COMPANY_NAME   AS COMPANY_NAME," +
                      "        COR_TRN_COMPANY.TAN_NO         AS TAN_NO," +
                      //"        FORMAT(COR_HDR_BATCH.IMPORTED_DATE, \"dd/MM/yyyy h:m AMPM\")   AS IMPORTED_DATE," +
                      "        " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.IMPORTED_DATE", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSS) + " AS IMPORTED_DATE, " +
                      "        " + cmnService.J_SQLDBFormat(strLoadCorrectionMatrix, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS TOTAL_CORRECTION," +
                      "        MST_ASSESSMENT.ASST_ID         AS ASST_ID " +
                      "FROM    COR_HDR_BATCH," +
                      "        MST_ASSESSMENT," +
                      "        COR_TRN_COMPANY " +
                      "WHERE   COR_HDR_BATCH.ASST_ID            = MST_ASSESSMENT.ASST_ID " +
                      "AND     COR_HDR_BATCH.BATCH_HEADER_ID    = COR_TRN_COMPANY.BATCH_HEADER_ID ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewBatch, strSQL, strMatrixBatchGrid);       //Show Data into the Grid
        }


        #endregion

        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0104", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }

}