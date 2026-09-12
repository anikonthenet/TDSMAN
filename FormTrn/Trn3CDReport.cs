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




//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;
using TDSMAN.Reports.Transaction;
using CrystalDecisions.CrystalReports.Engine;

#endregion

namespace TDSMAN.FormTrn
{

    #region T_ReturnGrid
    public struct T_ReturnGrid
    {
        public const int GridId = 0;
        public const int FormNo = 1;
        public const int FormDisplay = 2;
        public const int Qtr = 3;
        public const int Desc = 4;
        public const int BasicInfoId = 5;
        public const int REGULAR_CORRECTION = 6;
        public const int Status = 7;
    }
    #endregion

    public partial class Trn3CDReport : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public Trn3CDReport()
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
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        ReportClass rptcls;
        //RptDialog rptDialog = new RptDialog();
        DataSet myDataSet;

        string strSQL = string.Empty;
        string strInvalidPAN = "";
        string strUnmatchedPAN = "";
        long lngBatchBasicInfoID = 0;
        //--            
        ToolTip tllTip = new ToolTip();
        ToolTip tllTipVideoDemo = new ToolTip();
        string strEmpDed = "";
        string strbtnVerification = "&Start Validation";
        string strImportErrorMessage = "Import Failed.";

        int intDefaultInvalidReturnValue = 1;
        //string strLogOff = "Log off";
        //string strLogOn = "Log on";
        string strFVUPath = "";
        string strCheckCompatibilityMessage = "";
        long lngBatchID = 0;
        //
        bool blnStatus = false;
        bool blnVerificationComplete = false;
        long lngDeducteeID = 0;
        bool blRegular = true;
        bool blnSelectComboExit = false;
        bool blnLoadGridExit = false;
        //
        int intPANId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0;
        int intVerifyId = 0;

        bool blnNullSectionFound = false;
        //
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNo = "";
        string strQtr = "";
        string strFormNoBkmark = "";
        string strFAYear = "";
        string OutputFilePath;

        //----------------------------------------------
        string SourceFilePath;
        string strFileName;
        string strFolderPath;
        string strImporttableName = "";
        string strLogFileCreationDate = "";
        string strLogTanNo = "";

        string strFileCreationDate = "";
        string strFileCreationDateFVU5_2 = "";
        string strLogForm = "";
        string strLogFaYear = "";
        string strLogFaYearId = "";
        string strLogQuarter = "";
        //-- 23/02/2018 --
        string strExcelFilePath = string.Empty;
        string strExcelFileName = string.Empty;
        //
        int j = 0;
        //
        BindingSource objBindingSource = new BindingSource();
        private BindingList<PANDetails> _results = new BindingList<PANDetails>();
        //
        bool blResize = true;
        enum enmRequestType
        {
            Login,
            PanValidation,
            LogOff
        }
        //
        string strNOTAVAILABLE = "NOT AVAILABLE";
        //--
        #endregion

        #region RESIZING WINDOW
        private void _Load(object sender, EventArgs e)
        {
            _form_resize._get_initial_size();
        }

        private void _Resize(object sender, EventArgs e)
        {
            if (blResize == true)
                _form_resize._resize();
            //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        }
        #endregion

        #region User Defined Events

        #region Trn3CDReport_Load
        private void Trn3CDReport_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            lblTitle.Text = "Form 3CD [TDS Section]";
            ClearControls();
            tbcData.Enabled = false;
            BtnRefresh.Enabled = false;
            BtnRefresh.BackColor = Color.LightGray;
            //LoadGrid();
        }
        #endregion

        #region cmbFinancialYearCompany_SelectedIndexChanged
        private void cmbFinancialYearCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strFormNo = "", strQtr = "";
            long dblFaYearId = 0, dblCompanyId = 0;
            try
            {
                lngBatchBasicInfoID = 0;
                //--                            
                BtnRefresh.Enabled = false;
                BtnRefresh.BackColor = Color.LightGray;
                //--
                if (blnSelectComboExit == true) return;
                //--                
                if (cmbFinancialYear.SelectedIndex > 0 && cmbCompany.SelectedIndex > 0)
                {
                    LoadGrid(false);
                    //--
                    lngBatchBasicInfoID = 0;
                    dblFaYearId = Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
                    dblCompanyId = Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
                    //string strDeductorType = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MST_CATEGORY.CATEGORY_CODE AS CATEGORY_CODE FROM MST_COMPANY, MST_CATEGORY WHERE MST_COMPANY.D_CATEGORY_ID = MST_CATEGORY.CATEGORY_ID AND MST_COMPANY.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))));
                    //
                    for (int i = j; i <= dgvReturnDetails.RowCount - 1; i++)
                    {
                        strFormNo = Convert.ToString(dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.FormNo].Value);
                        strQtr = Convert.ToString(dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Qtr].Value);
                        //--
                        #region CONSO FILE
                        strSQL = @"SELECT BATCH_HEADER_ID FROM COR_HDR_BATCH_3CD 
                                   WHERE  ASST_ID = " + dblFaYearId + @" 
                                   AND    FORM_NO ='" + strFormNo + @"' 
                                   AND    QTR ='" + strQtr + @"' 
                                   AND    COMPANY_ID =" + dblCompanyId;
                        if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                        {
                            lngBatchBasicInfoID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                            dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.BasicInfoId].Value = lngBatchBasicInfoID;
                            //
                            dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Status].Style.BackColor = Color.LightGreen;
                            dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Status].ToolTipText = "Data imported from Conso file";
                            dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Status].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Status].Value = "↓ - Conso file";
                            dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.REGULAR_CORRECTION].Value = "C";
                            //--                            
                            BtnRefresh.Enabled = true;
                            BtnRefresh.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
                            //-- RETURN LAST DATE
                            strSQL = "UPDATE COR_HDR_BATCH_3CD SET RETURN_LAST_DATE ='" + TdsMan.T_ReturnFilingLastDate(strQtr, TdsMan.ReturnFinancialYear(dblFaYearId), TdsMan.GetDeductorType(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))), dblFaYearId, strFormNo) + "' WHERE BATCH_HEADER_ID = " + lngBatchBasicInfoID;
                            dmlService.J_ExecSql(strSQL);
                            //--
                        }
                        
                        //--
                        #endregion
                        //--
                        if (Convert.ToString(dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.REGULAR_CORRECTION].Value).Trim() == "")
                        {
                            #region REGULAR
                            lngBatchBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, dblFaYearId,
                                                            strQtr.Trim(),
                                                            dblCompanyId,
                                                            strFormNo.Trim());
                            //--
                            if (lngBatchBasicInfoID > 0)
                            {
                                dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.BasicInfoId].Value = lngBatchBasicInfoID;
                                //--
                                if (dmlService.J_ReturnNoOfRows("SELECT DEDUCTEE_DETAIL_ID FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBatchBasicInfoID, J_QueryType.DirectQuery) > 0)
                                {
                                    dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Status].Style.BackColor = Color.Yellow;
                                    dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Status].ToolTipText = "Data available in Regular Return";
                                    dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Status].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Status].Value = "✔ - Return Data";
                                    dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.REGULAR_CORRECTION].Value = "R";
                                    //--                            
                                    BtnRefresh.Enabled = true;
                                    BtnRefresh.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
                                    //--
                                }
                                //-- RETURN LAST DATE
                                strSQL = "UPDATE TRN_BASIC_INFO SET RETURN_LAST_DATE ='" + TdsMan.T_ReturnFilingLastDate(strQtr, TdsMan.ReturnFinancialYear(dblFaYearId), TdsMan.GetDeductorType(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))), dblFaYearId, strFormNo) + "' WHERE BASIC_INFO_ID = " + lngBatchBasicInfoID;
                                dmlService.J_ExecSql(strSQL);
                                //--                        
                            }
                            //--
                            #endregion
                        }
                        //--
                    }
                    //
                    blnLoadGridExit = false;
                    //tbcData.Enabled = true;
                }
                else
                {
                    LoadGrid(true);
                    tbcData.Enabled = false;
                    //BtnRefresh.Enabled = false;
                    //BtnRefresh.BackColor = Color.LightGray;
                }
                //--
            }
            catch (Exception err)
            {
            }
        }
        #endregion

        #region dgvReturnDetails_ColumnAdded
        private void dgvReturnDetails_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgvReturnDetails.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        #endregion

        #region dgvReturnDetails_CurrentCellDirtyStateChanged
        private void dgvReturnDetails_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvReturnDetails.IsCurrentCellDirty)
            {
                dgvReturnDetails.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        #endregion

        #region dgvReturnDetails_RowEnter
        private void dgvReturnDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (blnLoadGridExit == true) return;
                //
                tbcData.Enabled = true;
                BtnRefresh.Enabled = true;
                BtnRefresh.BackColor = Color.Lavender;
                //
                //strFormNo = cmnService.J_Right(dgvReturnDetails.SelectedRows[0].Cells[T_ReturnGrid.FormNo].Value.ToString().Trim(), 4);
                strFormNo = dgvReturnDetails.SelectedRows[0].Cells[T_ReturnGrid.FormNo].Value.ToString().Trim();
                strQtr = dgvReturnDetails.SelectedRows[0].Cells[T_ReturnGrid.Qtr].Value.ToString();
                //
                lblHeadingFormQtr1.Text = "FY : " + cmbFinancialYear.Text + " | Form No. : " + strFormNo + " | Quarter : " + strQtr;
                //lblHeadingFormQtr2.Text = "Form No. " + strFormNo + " - " + strQtr;
                //--
                ControlSummaryBasicInfo(cmnService.J_ReturnInt32Value(dgvReturnDetails.SelectedRows[0].Cells[T_ReturnGrid.BasicInfoId].Value.ToString()), 
                                        strFormNo, 
                                        strQtr,
                                        dgvReturnDetails.SelectedRows[0].Cells[T_ReturnGrid.REGULAR_CORRECTION].Value.ToString());
                //
                ReturnFilingStatus(cmnService.J_ReturnInt32Value(dgvReturnDetails.SelectedRows[0].Cells[T_ReturnGrid.BasicInfoId].Value.ToString()),
                                   dgvReturnDetails.SelectedRows[0].Cells[T_ReturnGrid.REGULAR_CORRECTION].Value.ToString());
                //--
            }
            catch
            {
                //cmnService.J_UserMessage(dgvReturnDetails.SelectedRows[0].Cells[T_ReturnGrid.BasicInfoId].Value.ToString());
            }
        }
        #endregion

        #region dgvReturnDetails_CellClick
        private void dgvReturnDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvReturnDetails_RowEnter(sender, e);
        }
        #endregion        

        #region btnSelectTDSPath_Click
        private void btnSelectTDSPath_Click(object sender, EventArgs e)
        {
            //strFVUPath = cmnService.J_OpenFileDialog("TDS File | *.tds", "TDS File | *.tds", "Choose the TDS File to import");
            strFVUPath = cmnService.J_OpenFileDialog("TDS File | *.tds; ", "TDS File | *.tds;", "Choose the TDS File to import");
            if (strFVUPath != "")
                txtFVUPath.Text = strFVUPath.Trim();
            else
                txtFVUPath.Text = "";

        }
        #endregion

        #region btnImportTDSFile_Click
        private void btnImportTDSFile_Click(object sender, EventArgs e)
        {
            string OutputFolder = "", strFileName = "";
            //string Filename;
            string OutputFilePath;
            try
            {
                #region VALIDATE
                if (strFVUPath.Trim() == "")
                {
                    cmnService.J_UserMessage("Select the Conso/.tds file that you want to import.");
                    btnSelectTDSPath.Select();
                    return;
                }


                // FILE EXIST
                if (cmnService.J_IsFileExist(strFVUPath.Trim()) == false)
                {
                    cmnService.J_UserMessage("Selected File not found");
                    btnSelectTDSPath.Select();
                    return;
                }

                // FILE OPEN
                if (cmnService.J_IsProcessOpen(strFVUPath.Trim()) == true)
                {
                    cmnService.J_UserMessage("Selected File is open");
                    btnSelectTDSPath.Select();
                    return;
                }
                #endregion
                //--
                OutputFolder = Path.Combine(Application.StartupPath, "Correction Input Files");
                strFileName = cmnService.J_GetFileName(strFVUPath);

                //Getting filename without extension
                strFileName = cmnService.J_Left(cmnService.J_GetFileName(strFVUPath), strFileName.Length - 4);

                string strTempFileName = strFileName;


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

                OutputFilePath = OutputFolder + "\\" + strFileName + ".tds";


                //SAVING THE FILE SELECTED IN APPLICATION FOLDER
                cmnService.J_CreateDirectory(OutputFolder);

                File.Copy(strFVUPath, OutputFilePath, true);

                // TO CHECK IF THE FILE SELECTED IS CONSOLIDATED FILE ONLY
                if (CheckFVUCompatibility(strFVUPath.Trim(), strFormNo.Trim(), strQtr.Trim(), cmnService.J_Mid(cmbCompany.Text.Trim(), cmbCompany.Text.Trim().Length - 11, 10).Trim()) == false)
                {
                    cmnService.J_UserMessage(strCheckCompatibilityMessage);
                    this.Cursor = Cursors.Default;
                    btnSelectTDSPath.Select();
                    return;
                }
                //--
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
                //    //DISPLAYING THE CONFIRMATION MESSAGE BEFORE IMPORTING
                //    if (cmnService.J_UserMessage("TDS File Creation Date : " + strFileCreationDate + "\n Please ensure that you are using the latest file for preparing correction statement.\n Proceed to Import?", MessageBoxButtons.YesNo) == DialogResult.No)
                //    {
                //        BtnExit.Select();
                //        return;
                //    }
                //}
                if (cmnService.J_UserMessage("Proceed Conso file Import?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    BtnExit.Select();
                    return;
                }
                //--
                this.Refresh();
                //--
                #region DELETE PREV DATA IF PRESENT
                strSQL = @"SELECT BATCH_HEADER_ID FROM COR_HDR_BATCH_3CD 
                           WHERE  ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + 
                        @" AND    COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) +
                        @" AND    FORM_NO    ='" + strFormNo.Trim() + @"'
                           AND    QTR        ='" + strQtr.Trim() + @"'";
                long lngDeleteBatchID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                if (lngDeleteBatchID > 0)
                {
                    //
                    strSQL = "DELETE  FROM COR_HDR_BATCH_3CD WHERE BATCH_HEADER_ID = " + lngDeleteBatchID;
                    dmlService.J_ExecSql(strSQL);//
                    strSQL = "DELETE  FROM COR_HDR_CHALLAN_3CD WHERE BATCH_HEADER_ID = " + lngDeleteBatchID;
                    dmlService.J_ExecSql(strSQL);//
                    strSQL = "DELETE  FROM COR_HDR_COMPANY_3CD WHERE BATCH_HEADER_ID = " + lngDeleteBatchID;
                    dmlService.J_ExecSql(strSQL);//
                    strSQL = "DELETE  FROM COR_HDR_DEDUCTEE_DETAILS_3CD WHERE BATCH_HEADER_ID = " + lngDeleteBatchID;
                    dmlService.J_ExecSql(strSQL);//
                    strSQL = "DELETE  FROM COR_HDR_SALARY_DETAILS_3CD WHERE BATCH_HEADER_ID = " + lngDeleteBatchID;
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //IMPORTING ALL THE DATA FROM THE TEXT FILE
                //MessageBox.Show("1");
                if (ImportTransactionsFromFVU(strFVUPath.Trim(), Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))) == false)
                {
                    prgBar.Value = 0;
                    this.Cursor = Cursors.Default;
                    //MessageBox.Show("1-err");
                    return;
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
                //--
                grpConsoFileDetails.Visible = false;
                //grpConsoFileDetails1.Visible = false;
                cmnService.J_UserMessage("Import Completed");
                //--
                cmbFinancialYearCompany_SelectedIndexChanged(sender, e);
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
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

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            grpPrint.Visible = false;
            grpRegularReturn.Enabled = true;
            grpDetailsGrid.Enabled = true;
            grpButton.Enabled = true;
        }
        #endregion

        #region BtnRefresh_Click
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            grpPrint.Visible = true;
            grpRegularReturn.Enabled = false;
            grpDetailsGrid.Enabled = false;
            grpButton.Enabled = false;
        }
        #endregion

        #region rbnPrint_CheckedChanged
        private void rbnPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnExcel.Checked == true || rbnCSV.Checked==true)
            {
                //grpPrintType.Enabled = false;
                //grpExcel.Enabled = true;
                grpPrintType.Visible = false;
                grpExcel.Visible = true;
            }
            else if (rbnPrint.Checked == true)
            {
                //grpPrintType.Enabled = true;
                //grpExcel.Enabled = false;
                grpPrintType.Visible = true;
                grpExcel.Visible = false;
            }
        }
        #endregion

        #region btnGoPrint_Click
        private void btnGoPrint_Click(object sender, EventArgs e)
        {
            string strSQLSummary = "", strSQLDetails = "", strSQLReturnFilingSummary = "", strSQLRemarkMaster = "", strSQLSummaryOfInterests = "", strSQLSummaryOfInterestsSubRpt = "";
            try
            {
                #region Excel/CSV
                if (rbnExcel.Checked == true || rbnCSV.Checked == true)
                {
                    //-- FOR EXCEL EXPORT
                    //-- 
                    strExcelFilePath = txtExcelPath.Text.Trim();
                    //--
                    if (string.IsNullOrEmpty(txtExcelPath.Text.Trim()))
                    {
                        cmnService.J_UserMessage("Please select specific folder for import");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //--
                    string strTan = cmnService.J_Mid(cmbCompany.Text.Trim(), cmbCompany.Text.Length - 11, 10);
                    if (rbnExcel.Checked == true)
                    {
                        strExcelFileName = strTan + "_" + cmbFinancialYear.Text.Trim() + "_" + "3CD.XLSX";
                        strExcelFilePath = strExcelFilePath + "\\" + strExcelFileName;
                    }
                    else if (rbnCSV.Checked == true)
                    {
                        strExcelFileName = strTan + "_" + cmbFinancialYear.Text.Trim() + "_" + "3CD";
                        strExcelFilePath = strExcelFilePath + "\\" + strExcelFileName;
                    }
                    //--
                    //-- VALIDATION
                    // CHECK IF SAME NAME FILE EXIST
                    if (File.Exists(strExcelFilePath))  //-- 01/01/2017 --
                    {
                        if (cmnService.J_UserMessage("File exists with same name. Do you want to replace?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                        else
                        {
                            //if (cmnService.J_IsProcessOpen(strExcelFilePath) == true)
                            //{
                            //    cmnService.J_UserMessage("The file is already open");
                            //    return;
                            //}
                            //else
                            try
                            {
                                File.Delete(strExcelFilePath);
                            }
                            catch (Exception err)
                            {
                                this.Cursor = Cursors.Default;
                                cmnService.J_UserMessage("Could not replace the file!!!\nThe file may be in use...");
                                return;
                            }
                        }
                    }
                }
                #endregion
                //--
                if (cmnService.J_UserMessage("Proceed??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //
                this.Cursor = Cursors.WaitCursor;
                //-- CREATING TEMP TABLES
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD) == true)
                {
                    strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD;
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD) == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + " (" +
                         "                  " + cmnService.J_GetDataType("FORM3CD_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("DEDUCTEE_DETAIL_ID", J_ColumnType.Long, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("FORM_NO", J_ColumnType.String, 20, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("QTR", J_ColumnType.String, 4, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("ENTRY_TYPE", J_ColumnType.String, 20, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("SECTION_NO", J_ColumnType.String, 10, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("SECTION_DESCRIPTION", J_ColumnType.String, 100, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("DEDUCTEE_PAN", J_ColumnType.String, 10, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("DEDUCTEE_NAME", J_ColumnType.String, 75, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("PAYMENT_DATE", J_ColumnType.DateTime) + "," +
                         "                  " + cmnService.J_GetDataType("DEDUCTED_DATE", J_ColumnType.DateTime) + "," +
                         "                  " + cmnService.J_GetDataType("PAYMENT_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("RATE", J_ColumnType.Double, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("TAX_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("TOTAL_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("TAX_DEPOSITED_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("REASON", J_ColumnType.String, 1, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("DEPOSIT_DATE", J_ColumnType.DateTime) + "," +
                         "                  " + cmnService.J_GetDataType("RETURN_LAST_DATE", J_ColumnType.DateTime) + "," +
                         "                  " + cmnService.J_GetDataType("FILING_DATE", J_ColumnType.DateTime) + ")";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                dmlService.J_Commit();
                //
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD_INTEREST) == true)
                {
                    strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD_INTEREST;
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD_INTEREST) == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD_INTEREST + " (" +
                         "                  " + cmnService.J_GetDataType("FORM3CD_INTEREST_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("CHALLAN_ID", J_ColumnType.Long, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("FORM_NO", J_ColumnType.String, 20, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("QTR", J_ColumnType.String, 4, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("ENTRY_TYPE", J_ColumnType.String, 20, J_DefaultValue.YES) + "," +
                         "                  " + cmnService.J_GetDataType("DEPOSIT_DATE", J_ColumnType.DateTime) + "," +
                         "                  " + cmnService.J_GetDataType("INTEREST_ALLOCATED", J_ColumnType.Double, J_DefaultValue.YES) + ")";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                dmlService.J_Commit();
                //
                #region TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD (RETURN_LAST_DATE)
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD, "RETURN_LAST_DATE") == false)
                {
                    //strSQL = "ALTER TABLE MST_EMPLOYEE ADD COLUMN EMAIL TEXT(75) NOT NULL DEFAULT \"\"";

                    //strSQL = dmlService.ReturnALTERSyntaxSequelServer("TRN_BASIC_INFO", "RETURN_LAST_DATE", "DATETIME", "", "NULL", "");
                    strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD, "RETURN_LAST_DATE", "DATETIME", "", "NULL", "");
                    dmlService.J_ExecSql(strSQL);
                    //strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + " SET RETURN_LAST_DATE =''";
                    //dmlService.J_ExecSql(strSQL);
                }
                #endregion;
                //
                #region TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD (FILING_DATE)
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD, "FILING_DATE") == false)
                {
                    strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD, "FILING_DATE", "DATETIME", "", "NULL", "");
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //
                #region COMMENT
                //-- INSERTING DATA
//                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
//                {
//                    #region SQL SERVER
//                    strSQL = @"INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + @" (DEDUCTEE_DETAIL_ID, 
//			                            FORM_NO, 
//			                            QTR, 
//			                            ENTRY_TYPE,
//			                            SECTION_NO, 
//			                            SECTION_DESCRIPTION, 
//			                            DEDUCTEE_PAN, 
//			                            DEDUCTEE_NAME,
//			                            PAYMENT_DATE,
//			                            DEDUCTED_DATE,
//			                            PAYMENT_AMOUNT,
//			                            RATE,
//			                            TAX_AMOUNT,
//			                            TOTAL_AMOUNT,
//			                            TAX_DEPOSITED_AMOUNT,
//			                            REASON,
//			                            DEPOSIT_DATE)
//                            SELECT COR_HDR_DEDUCTEE_DETAILS_3CD.HDR_DEDUCTEE_DETAIL_ID,
//	                               COR_HDR_BATCH_3CD.FORM_NO,
//	                               COR_HDR_BATCH_3CD.QTR,
//	                               'CONSO' AS TYPE,
//                                   MST_SECTION.SECTION_NO,
//                                   MST_SECTION.SECTION_DESCRIPTION,
//	                               COR_HDR_DEDUCTEE_DETAILS_3CD.DEDUCTEE_PAN,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.DEDUCTEE_NAME,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.PAYMENT_DATE,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.DEDUCTED_DATE,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.PAYMENT_AMOUNT,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.RATE,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.TAX_AMOUNT,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.TOTAL_AMOUNT,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.TAX_DEPOSITED_AMOUNT
//                                   ,MST_REASON.REASON
//                                   ,COR_HDR_CHALLAN_3CD.DEPOSIT_DATE
//                            FROM   ((((COR_HDR_DEDUCTEE_DETAILS_3CD
//                            INNER JOIN COR_HDR_BATCH_3CD
//                            ON     COR_HDR_DEDUCTEE_DETAILS_3CD.BATCH_HEADER_ID = COR_HDR_BATCH_3CD.BATCH_HEADER_ID)
//                            INNER JOIN COR_HDR_CHALLAN_3CD
//                            ON     COR_HDR_CHALLAN_3CD.HDR_CHALLAN_ID = COR_HDR_DEDUCTEE_DETAILS_3CD.HDR_CHALLAN_ID)
//                            INNER JOIN MST_SECTION
//                            ON     COR_HDR_DEDUCTEE_DETAILS_3CD.SECTION_ID = MST_SECTION.SECTION_ID)
//                            LEFT JOIN MST_REASON
//                            ON     COR_HDR_DEDUCTEE_DETAILS_3CD.REASON_ID  = MST_REASON.REASON_ID)
//                            WHERE  COR_HDR_BATCH_3CD.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
//                            AND    COR_HDR_BATCH_3CD.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
//                            UNION ALL
//                            ((SELECT  TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID,
//	                               TRN_BASIC_INFO.FORM_NO,
//	                               TRN_BASIC_INFO.QTR,
//	                               'REGULAR' AS TYPE,
//                                   MST_SECTION.SECTION_NO,
//                                   MST_SECTION.SECTION_DESCRIPTION,
//                                   MST_DEDUCTEE.DEDUCTEE_PAN,
//                                   MST_DEDUCTEE.DEDUCTEE_NAME,
//                                   TRN_DEDUCTEE_DETAILS.PAYMENT_DATE,
//                                   TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE,
//                                   TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.RATE,
//                                   TRN_DEDUCTEE_DETAILS.TAX_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT
//                                   ,MST_REASON.REASON
//                                   ,TRN_CHALLAN.DEPOSIT_DATE
//                            FROM   (((((TRN_DEDUCTEE_DETAILS
//                            INNER JOIN TRN_BASIC_INFO
//                            ON TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
//                            INNER JOIN TRN_CHALLAN
//                            ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)
//                            INNER JOIN MST_DEDUCTEE
//                            ON TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_DEDUCTEE.DEDUCTEE_ID)
//                            INNER JOIN MST_SECTION
//                            ON     TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID)
//                            LEFT JOIN MST_REASON
//                            ON     TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)
//                            WHERE  TRN_BASIC_INFO.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
//                            AND    TRN_BASIC_INFO.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
//                            AND    TRN_BASIC_INFO.FORM_NO <> '24Q')  
//                            UNION ALL
//                            (SELECT  TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID,
//	                               TRN_BASIC_INFO.FORM_NO,
//	                               TRN_BASIC_INFO.QTR,
//	                               'REGULAR' AS TYPE,
//                                   MST_SECTION.SECTION_NO,
//                                   MST_SECTION.SECTION_DESCRIPTION,
//                                   MST_EMPLOYEE.EMPLOYEE_PAN,
//                                   MST_EMPLOYEE.EMPLOYEE_NAME,
//                                   TRN_DEDUCTEE_DETAILS.PAYMENT_DATE,
//                                   TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE,
//                                   TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.RATE,
//                                   TRN_DEDUCTEE_DETAILS.TAX_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT
//                                   ,MST_REASON.REASON
//                                   ,TRN_CHALLAN.DEPOSIT_DATE
//                            FROM   (((((TRN_DEDUCTEE_DETAILS
//                            INNER JOIN TRN_BASIC_INFO
//                            ON TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
//                            INNER JOIN TRN_CHALLAN
//                            ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)
//                            INNER JOIN MST_EMPLOYEE
//                            ON TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_EMPLOYEE.EMPLOYEE_ID)
//                            INNER JOIN MST_SECTION
//                            ON     TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID)
//                            LEFT JOIN MST_REASON
//                            ON     TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)
//                            WHERE  TRN_BASIC_INFO.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
//                            AND    TRN_BASIC_INFO.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
//                            AND    TRN_BASIC_INFO.FORM_NO = '24Q'))";
//                    //
//                    dmlService.J_ExecSql(strSQL);
//                    #endregion
//                    //--
//                }
//                else
                //{
                    #endregion
                //--
                #region COMMENTED
                    //
//                    #region CONSO FILE
//                    strSQL = @"INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + @" (DEDUCTEE_DETAIL_ID, 
//			                            FORM_NO, 
//			                            QTR, 
//			                            ENTRY_TYPE,
//			                            SECTION_NO, 
//			                            SECTION_DESCRIPTION, 
//			                            DEDUCTEE_PAN, 
//			                            DEDUCTEE_NAME,
//			                            PAYMENT_DATE,
//			                            DEDUCTED_DATE,
//			                            PAYMENT_AMOUNT,
//			                            RATE,
//			                            TAX_AMOUNT,
//			                            TOTAL_AMOUNT,
//			                            TAX_DEPOSITED_AMOUNT,
//			                            REASON,
//			                            DEPOSIT_DATE)
//                            SELECT COR_HDR_DEDUCTEE_DETAILS_3CD.HDR_DEDUCTEE_DETAIL_ID,
//	                               COR_HDR_BATCH_3CD.FORM_NO,
//	                               COR_HDR_BATCH_3CD.QTR,
//	                               'CONSO' AS TYPE,
//                                   MST_SECTION.SECTION_NO,
//                                   MST_SECTION.SECTION_DESCRIPTION,
//	                               COR_HDR_DEDUCTEE_DETAILS_3CD.DEDUCTEE_PAN,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.DEDUCTEE_NAME,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.PAYMENT_DATE,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.DEDUCTED_DATE,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.PAYMENT_AMOUNT,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.RATE,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.TAX_AMOUNT,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.TOTAL_AMOUNT,
//                                   COR_HDR_DEDUCTEE_DETAILS_3CD.TAX_DEPOSITED_AMOUNT
//                                   ,MST_REASON.REASON
//                                   ,COR_HDR_CHALLAN_3CD.DEPOSIT_DATE
//                            FROM   ((((COR_HDR_DEDUCTEE_DETAILS_3CD
//                            INNER JOIN COR_HDR_BATCH_3CD
//                            ON     COR_HDR_DEDUCTEE_DETAILS_3CD.BATCH_HEADER_ID = COR_HDR_BATCH_3CD.BATCH_HEADER_ID)
//                            INNER JOIN COR_HDR_CHALLAN_3CD
//                            ON     COR_HDR_CHALLAN_3CD.HDR_CHALLAN_ID = COR_HDR_DEDUCTEE_DETAILS_3CD.HDR_CHALLAN_ID)
//                            INNER JOIN MST_SECTION
//                            ON     COR_HDR_DEDUCTEE_DETAILS_3CD.SECTION_ID = MST_SECTION.SECTION_ID)
//                            LEFT JOIN MST_REASON
//                            ON     COR_HDR_DEDUCTEE_DETAILS_3CD.REASON_ID  = MST_REASON.REASON_ID)
//                            WHERE  COR_HDR_BATCH_3CD.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
//                            AND    COR_HDR_BATCH_3CD.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
//                    //
//                    dmlService.J_ExecSql(strSQL);
//                    #endregion
//                    //
//                    #region REGULAR NOT 24Q
//                    strSQL = @"INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + @" (DEDUCTEE_DETAIL_ID, 
//			                            FORM_NO, 
//			                            QTR, 
//			                            ENTRY_TYPE,
//			                            SECTION_NO, 
//			                            SECTION_DESCRIPTION, 
//			                            DEDUCTEE_PAN, 
//			                            DEDUCTEE_NAME,
//			                            PAYMENT_DATE,
//			                            DEDUCTED_DATE,
//			                            PAYMENT_AMOUNT,
//			                            RATE,
//			                            TAX_AMOUNT,
//			                            TOTAL_AMOUNT,
//			                            TAX_DEPOSITED_AMOUNT,
//			                            REASON,
//			                            DEPOSIT_DATE)
//                            SELECT TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID,
//	                               TRN_BASIC_INFO.FORM_NO,
//	                               TRN_BASIC_INFO.QTR,
//	                               'REGULAR' AS TYPE,
//                                   MST_SECTION.SECTION_NO,
//                                   MST_SECTION.SECTION_DESCRIPTION,
//                                   MST_DEDUCTEE.DEDUCTEE_PAN,
//                                   MST_DEDUCTEE.DEDUCTEE_NAME,
//                                   TRN_DEDUCTEE_DETAILS.PAYMENT_DATE,
//                                   TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE,
//                                   TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.RATE,
//                                   TRN_DEDUCTEE_DETAILS.TAX_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT
//                                   ,MST_REASON.REASON
//                                   ,TRN_CHALLAN.DEPOSIT_DATE
//                            FROM   (((((TRN_DEDUCTEE_DETAILS
//                            INNER JOIN TRN_BASIC_INFO
//                            ON TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
//                            INNER JOIN TRN_CHALLAN
//                            ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)
//                            INNER JOIN MST_DEDUCTEE
//                            ON TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_DEDUCTEE.DEDUCTEE_ID)
//                            INNER JOIN MST_SECTION
//                            ON     TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID)
//                            LEFT JOIN MST_REASON
//                            ON     TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)
//                            WHERE  TRN_BASIC_INFO.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
//                            AND    TRN_BASIC_INFO.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
//                            AND    TRN_BASIC_INFO.FORM_NO <> '24Q'";
//                    //
//                    dmlService.J_ExecSql(strSQL);
//                    #endregion
//                    //
//                    #region REGULAR 24Q
//                    strSQL = @"INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + @" (DEDUCTEE_DETAIL_ID, 
//			                            FORM_NO, 
//			                            QTR, 
//			                            ENTRY_TYPE,
//			                            SECTION_NO, 
//			                            SECTION_DESCRIPTION, 
//			                            DEDUCTEE_PAN, 
//			                            DEDUCTEE_NAME,
//			                            PAYMENT_DATE,
//			                            DEDUCTED_DATE,
//			                            PAYMENT_AMOUNT,
//			                            RATE,
//			                            TAX_AMOUNT,
//			                            TOTAL_AMOUNT,
//			                            TAX_DEPOSITED_AMOUNT,
//			                            REASON,
//			                            DEPOSIT_DATE)
//                            SELECT  TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID,
//	                               TRN_BASIC_INFO.FORM_NO,
//	                               TRN_BASIC_INFO.QTR,
//	                               'REGULAR' AS TYPE,
//                                   MST_SECTION.SECTION_NO,
//                                   MST_SECTION.SECTION_DESCRIPTION,
//                                   MST_EMPLOYEE.EMPLOYEE_PAN,
//                                   MST_EMPLOYEE.EMPLOYEE_NAME,
//                                   TRN_DEDUCTEE_DETAILS.PAYMENT_DATE,
//                                   TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE,
//                                   TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.RATE,
//                                   TRN_DEDUCTEE_DETAILS.TAX_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT,
//                                   TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT
//                                   ,MST_REASON.REASON
//                                   ,TRN_CHALLAN.DEPOSIT_DATE
//                            FROM   (((((TRN_DEDUCTEE_DETAILS
//                            INNER JOIN TRN_BASIC_INFO
//                            ON TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
//                            INNER JOIN TRN_CHALLAN
//                            ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)
//                            INNER JOIN MST_EMPLOYEE
//                            ON TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_EMPLOYEE.EMPLOYEE_ID)
//                            INNER JOIN MST_SECTION
//                            ON     TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID)
//                            LEFT JOIN MST_REASON
//                            ON     TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)
//                            WHERE  TRN_BASIC_INFO.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
//                            AND    TRN_BASIC_INFO.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
//                            AND    TRN_BASIC_INFO.FORM_NO = '24Q'";
//                    //
                    //                    dmlService.J_ExecSql(strSQL);
                    #endregion
                //
                #region INSERT DATA
                    for (int i = j; i <= dgvReturnDetails.RowCount - 1; i++)
                    {
                        if (dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.REGULAR_CORRECTION].Value == "C")
                        {
                            //-- DEDUCTEE DETAILS
                            strSQL = @"INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + @" (DEDUCTEE_DETAIL_ID, 
			                            FORM_NO, 
			                            QTR, 
			                            ENTRY_TYPE,
			                            SECTION_NO, 
			                            SECTION_DESCRIPTION, 
			                            DEDUCTEE_PAN, 
			                            DEDUCTEE_NAME,
			                            PAYMENT_DATE,
			                            DEDUCTED_DATE,
			                            PAYMENT_AMOUNT,
			                            RATE,
			                            TAX_AMOUNT,
			                            TOTAL_AMOUNT,
			                            TAX_DEPOSITED_AMOUNT,
			                            REASON,
			                            DEPOSIT_DATE,
                                        RETURN_LAST_DATE,
                                        FILING_DATE)
                            SELECT COR_HDR_DEDUCTEE_DETAILS_3CD.HDR_DEDUCTEE_DETAIL_ID,
	                               COR_HDR_BATCH_3CD.FORM_NO,
	                               COR_HDR_BATCH_3CD.QTR,
	                               'CONSO' AS TYPE,
                                   MST_SECTION.SECTION_NO,
                                   MST_SECTION.SECTION_DESCRIPTION,
	                               COR_HDR_DEDUCTEE_DETAILS_3CD.DEDUCTEE_PAN,
                                   COR_HDR_DEDUCTEE_DETAILS_3CD.DEDUCTEE_NAME,
                                   COR_HDR_DEDUCTEE_DETAILS_3CD.PAYMENT_DATE,
                                   COR_HDR_DEDUCTEE_DETAILS_3CD.DEDUCTED_DATE,
                                   COR_HDR_DEDUCTEE_DETAILS_3CD.PAYMENT_AMOUNT,
                                   COR_HDR_DEDUCTEE_DETAILS_3CD.RATE,
                                   COR_HDR_DEDUCTEE_DETAILS_3CD.TAX_AMOUNT,
                                   COR_HDR_DEDUCTEE_DETAILS_3CD.TOTAL_AMOUNT,
                                   COR_HDR_DEDUCTEE_DETAILS_3CD.TAX_DEPOSITED_AMOUNT
                                   ,MST_REASON.REASON
                                   ,COR_HDR_CHALLAN_3CD.DEPOSIT_DATE
	                               ,COR_HDR_BATCH_3CD.RETURN_LAST_DATE
	                               ,COR_HDR_BATCH_3CD.DATE_OF_FILING
                            FROM   ((((COR_HDR_DEDUCTEE_DETAILS_3CD
                            INNER JOIN COR_HDR_BATCH_3CD
                            ON     COR_HDR_DEDUCTEE_DETAILS_3CD.BATCH_HEADER_ID = COR_HDR_BATCH_3CD.BATCH_HEADER_ID)
                            INNER JOIN COR_HDR_CHALLAN_3CD
                            ON     COR_HDR_CHALLAN_3CD.HDR_CHALLAN_ID = COR_HDR_DEDUCTEE_DETAILS_3CD.HDR_CHALLAN_ID)
                            INNER JOIN MST_SECTION
                            ON     COR_HDR_DEDUCTEE_DETAILS_3CD.SECTION_ID = MST_SECTION.SECTION_ID)
                            LEFT JOIN MST_REASON
                            ON     COR_HDR_DEDUCTEE_DETAILS_3CD.REASON_ID  = MST_REASON.REASON_ID)
                            WHERE  COR_HDR_BATCH_3CD.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                            AND    COR_HDR_BATCH_3CD.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                            AND    COR_HDR_BATCH_3CD.FORM_NO    ='" + dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.FormNo].Value + @"'
                            AND    COR_HDR_BATCH_3CD.QTR        ='" + dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Qtr].Value + @"'";
                            //
                            dmlService.J_ExecSql(strSQL);
                            //-- CHALLAN DETAILS
                            strSQL = @"INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD_INTEREST + @" (CHALLAN_ID, 
			                            FORM_NO, 
			                            QTR, 
			                            ENTRY_TYPE,
			                            DEPOSIT_DATE, 
			                            INTEREST_ALLOCATED)
                            SELECT COR_HDR_CHALLAN_3CD.HDR_CHALLAN_ID,
	                               COR_HDR_BATCH_3CD.FORM_NO,
	                               COR_HDR_BATCH_3CD.QTR,
	                               'CONSO' AS TYPE,
                                   COR_HDR_CHALLAN_3CD.DEPOSIT_DATE,
	                               COR_HDR_CHALLAN_3CD.INTEREST_ALLOCATED
                            FROM   (COR_HDR_BATCH_3CD
                            INNER JOIN COR_HDR_CHALLAN_3CD
                            ON     COR_HDR_CHALLAN_3CD.BATCH_HEADER_ID = COR_HDR_BATCH_3CD.BATCH_HEADER_ID)
                            WHERE  COR_HDR_CHALLAN_3CD.INTEREST_ALLOCATED > 0 
                            AND    COR_HDR_BATCH_3CD.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                            AND    COR_HDR_BATCH_3CD.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                            AND    COR_HDR_BATCH_3CD.FORM_NO    ='" + dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.FormNo].Value + @"'
                            AND    COR_HDR_BATCH_3CD.QTR        ='" + dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Qtr].Value + @"'";
                            //
                            dmlService.J_ExecSql(strSQL);
                        }
                        else if (dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.REGULAR_CORRECTION].Value.ToString().Trim() == "R")
                        {
                            if (dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.FormNo].Value.ToString().Trim() == T_FormNo.F24Q.ToString().Trim())
                            {
                                strSQL = @"INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + @" (DEDUCTEE_DETAIL_ID, 
			                            FORM_NO, 
			                            QTR, 
			                            ENTRY_TYPE,
			                            SECTION_NO, 
			                            SECTION_DESCRIPTION, 
			                            DEDUCTEE_PAN, 
			                            DEDUCTEE_NAME,
			                            PAYMENT_DATE,
			                            DEDUCTED_DATE,
			                            PAYMENT_AMOUNT,
			                            RATE,
			                            TAX_AMOUNT,
			                            TOTAL_AMOUNT,
			                            TAX_DEPOSITED_AMOUNT,
			                            REASON,
			                            DEPOSIT_DATE,
                                        RETURN_LAST_DATE,
                                        FILING_DATE)
                                SELECT  TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID,
	                                   TRN_BASIC_INFO.FORM_NO,
	                                   TRN_BASIC_INFO.QTR,
	                                   'REGULAR' AS TYPE,
                                       MST_SECTION.SECTION_NO,
                                       MST_SECTION.SECTION_DESCRIPTION,
                                       MST_EMPLOYEE.EMPLOYEE_PAN,
                                       MST_EMPLOYEE.EMPLOYEE_NAME,
                                       TRN_DEDUCTEE_DETAILS.PAYMENT_DATE,
                                       TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE,
                                       TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                       TRN_DEDUCTEE_DETAILS.RATE,
                                       TRN_DEDUCTEE_DETAILS.TAX_AMOUNT,
                                       TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT,
                                       TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT
                                       ,MST_REASON.REASON
                                       ,TRN_CHALLAN.DEPOSIT_DATE
	                                   ,TRN_BASIC_INFO.RETURN_LAST_DATE
                                       ,TRN_BASIC_INFO.DATE_OF_FILING
                                FROM   (((((TRN_DEDUCTEE_DETAILS
                                INNER JOIN TRN_BASIC_INFO
                                ON TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                                INNER JOIN TRN_CHALLAN
                                ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)
                                INNER JOIN MST_EMPLOYEE
                                ON TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_EMPLOYEE.EMPLOYEE_ID)
                                INNER JOIN MST_SECTION
                                ON     TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID)
                                LEFT JOIN MST_REASON
                                ON     TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)
                                WHERE  TRN_BASIC_INFO.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                                AND    TRN_BASIC_INFO.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                                AND    TRN_BASIC_INFO.FORM_NO    = '24Q' 
                                AND    TRN_BASIC_INFO.QTR        ='" + dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Qtr].Value + @"'";
                                //
                                dmlService.J_ExecSql(strSQL);
                            }
                            else
                            {
                                strSQL = @"INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + @" (DEDUCTEE_DETAIL_ID, 
			                            FORM_NO, 
			                            QTR, 
			                            ENTRY_TYPE,
			                            SECTION_NO, 
			                            SECTION_DESCRIPTION, 
			                            DEDUCTEE_PAN, 
			                            DEDUCTEE_NAME,
			                            PAYMENT_DATE,
			                            DEDUCTED_DATE,
			                            PAYMENT_AMOUNT,
			                            RATE,
			                            TAX_AMOUNT,
			                            TOTAL_AMOUNT,
			                            TAX_DEPOSITED_AMOUNT,
			                            REASON,
			                            DEPOSIT_DATE,
                                        RETURN_LAST_DATE,
                                        FILING_DATE)
                                SELECT TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID,
	                                   TRN_BASIC_INFO.FORM_NO,
	                                   TRN_BASIC_INFO.QTR,
	                                   'REGULAR' AS TYPE,
                                       MST_SECTION.SECTION_NO,
                                       MST_SECTION.SECTION_DESCRIPTION,
                                       MST_DEDUCTEE.DEDUCTEE_PAN,
                                       MST_DEDUCTEE.DEDUCTEE_NAME,
                                       TRN_DEDUCTEE_DETAILS.PAYMENT_DATE,
                                       TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE,
                                       TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                       TRN_DEDUCTEE_DETAILS.RATE,
                                       TRN_DEDUCTEE_DETAILS.TAX_AMOUNT,
                                       TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT,
                                       TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT
                                       ,MST_REASON.REASON
                                       ,TRN_CHALLAN.DEPOSIT_DATE
	                                   ,TRN_BASIC_INFO.RETURN_LAST_DATE
                                       ,TRN_BASIC_INFO.DATE_OF_FILING
                                FROM   (((((TRN_DEDUCTEE_DETAILS
                                INNER JOIN TRN_BASIC_INFO
                                ON TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                                INNER JOIN TRN_CHALLAN
                                ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)
                                INNER JOIN MST_DEDUCTEE
                                ON TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_DEDUCTEE.DEDUCTEE_ID)
                                INNER JOIN MST_SECTION
                                ON     TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID)
                                LEFT JOIN MST_REASON
                                ON     TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID)
                                WHERE  TRN_BASIC_INFO.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                                AND    TRN_BASIC_INFO.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"                                
                                AND    TRN_BASIC_INFO.FORM_NO    ='" + dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.FormNo].Value.ToString().Trim() + @"'
                                AND    TRN_BASIC_INFO.QTR        ='" + dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Qtr].Value + @"'";
                                //
                                dmlService.J_ExecSql(strSQL);
                            }
                            //-- CHALLAN DETAILS
                            strSQL = @"INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD_INTEREST + @" (CHALLAN_ID, 
			                            FORM_NO, 
			                            QTR, 
			                            ENTRY_TYPE,
			                            DEPOSIT_DATE, 
			                            INTEREST_ALLOCATED)
                            SELECT TRN_CHALLAN.CHALLAN_ID,
	                               TRN_BASIC_INFO.FORM_NO,
	                               TRN_BASIC_INFO.QTR,
	                               'REGULAR' AS TYPE,
                                   TRN_CHALLAN.DEPOSIT_DATE,
	                               TRN_CHALLAN.INTEREST_ALLOCATED
                            FROM   (TRN_BASIC_INFO
                            INNER JOIN TRN_CHALLAN
                            ON     TRN_CHALLAN.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                            WHERE  TRN_CHALLAN.INTEREST_ALLOCATED > 0 
                            AND    TRN_BASIC_INFO.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                            AND    TRN_BASIC_INFO.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                            AND    TRN_BASIC_INFO.FORM_NO    ='" + dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.FormNo].Value + @"'
                            AND    TRN_BASIC_INFO.QTR        ='" + dgvReturnDetails.Rows[i].Cells[T_ReturnGrid.Qtr].Value + @"'";
                            //
                            dmlService.J_ExecSql(strSQL);
                        }
                    }
                    #endregion
                //}
                //--
                #region COMMENT
                //////string[] strForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
                //////string[] strQtr = { T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
                ////////
                //////foreach (string Form in strForm)
                //////{
                //////    foreach (string Qtr in strQtr)
                //////    {
                //////        strSQL = "SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + " WHERE FORM_NO ='" + Form + "' AND QTR ='" + Qtr + "'";
                //////        if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 1)
                //////        {
                //////            strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + " WHERE FORM_NO ='" + Form + "' AND QTR ='" + Qtr + "' AND ENTRY_TYPE ='REGULAR'";
                //////            dmlService.J_ExecSql(strSQL);
                //////        }
                //////    }
                //////}
                #endregion
                //--
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + " SET REASON = '' WHERE REASON IS NULL";
                dmlService.J_ExecSql(strSQL);
                //--
                #region REPORT SQLs
                //
                #region SUMMARY
                //-- SUMMARY
                string[,] strTOTAL_AMOUNT_WITHOUT_NO_DEDUCTION_MATRIX = {{"REASON <> 'B'", "F", "PAYMENT_AMOUNT", "F"},
                                                  {"REASON = 'B'", "F", "0.00", "T"}};
                string[,] strTOTAL_AMOUNT_DEDUCTED_AT_LOWER_RATE_MATRIX = {{"REASON = 'A'", "F", "PAYMENT_AMOUNT", "F"},
                                                  {"REASON <> 'A'", "F", "0.00", "T"}};
                string[,] strTDS_DEDUCTED_AT_LOWER_RATE_MATRIX = {{"REASON = 'A'", "F", "TAX_AMOUNT", "F"},
                                                  {"REASON <> 'A'", "F", "0.00", "T"}};
                // 
                strSQLSummary = @"SELECT SECTION_NO          AS SECTIO, 
                                         SECTION_DESCRIPTION AS DESCRIPTION, 
                                         SUM(PAYMENT_AMOUNT) AS TOTAL_AMOUNT,
                                         SUM(" + cmnService.J_SQLDBFormat(strTOTAL_AMOUNT_WITHOUT_NO_DEDUCTION_MATRIX, J_SQLColFormat.Case_End) + @")                   AS TOTAL_AMOUNT_WITHOUT_NO_DEDUCTION,
                                         SUM(TOTAL_AMOUNT)     AS TOTAL_TDS,
                                         SUM(" + cmnService.J_SQLDBFormat(strTOTAL_AMOUNT_DEDUCTED_AT_LOWER_RATE_MATRIX, J_SQLColFormat.Case_End) + @")                 AS TOTAL_AMOUNT_DEDUCTED_AT_LOWER_RATE,
                                         SUM(" + cmnService.J_SQLDBFormat(strTDS_DEDUCTED_AT_LOWER_RATE_MATRIX, J_SQLColFormat.Case_End) + @") AS TDS_DEDUCTED_AT_LOWER_RATE
                               FROM      " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + @"
                               GROUP BY SECTION_NO, 
                                        SECTION_DESCRIPTION
                               ORDER BY SECTION_NO";
                #endregion
                //
                #region DETAILS
                //-- DETAILS
                strSQLDetails = @"SELECT	FORM3CD_ID, 
		                                    FORM_NO,
		                                    QTR,
		                                    ENTRY_TYPE,
		                                    SECTION_NO,
		                                    SECTION_DESCRIPTION,
		                                    DEDUCTEE_PAN,
		                                    DEDUCTEE_NAME," +
                                            cmnService.J_SQLDBFormat("PAYMENT_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + @"  AS PAYMENT_DATE," +
                                            cmnService.J_SQLDBFormat("DEDUCTED_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + @"  AS DEDUCTED_DATE,
		                                    PAYMENT_AMOUNT,
		                                    RATE,
		                                    TAX_AMOUNT,
		                                    TOTAL_AMOUNT,
		                                    TAX_DEPOSITED_AMOUNT,
		                                    REASON," +
                                            cmnService.J_SQLDBFormat("DEPOSIT_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + @"  AS DEPOSIT_DATE," +
                                            cmnService.J_SQLDBFormat("RETURN_LAST_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + @"  AS RETURN_LAST_DATE                                            
                                  FROM	    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + @" 
                                  ORDER BY  SECTION_NO,
                                            QTR,
		                                    PAYMENT_DATE,
		                                    DEDUCTEE_NAME";
                //-- Added By Abhishek Dey On 04/04/2018 --
                strSQLRemarkMaster = @"SELECT FORM_NO, 
                                              REASON, 
                                              DESCRIPTION 
                                        FROM  MST_REASON 
                                    ORDER BY FORM_NO, 
                                             REASON ";
                #endregion
                //-----------------------------------------
                #region RETURN FILING OF SUMMARY
                //-- RETURN FILING OF SUMMARY
                strSQLReturnFilingSummary = @"SELECT	FORM_NO, 
		                                                QTR, " +
                                                        cmnService.J_SQLDBFormat("FILING_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + @"       AS FILING_DT," +
                                                        cmnService.J_SQLDBFormat("RETURN_LAST_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + @"  AS RETURN_DT,";
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQLReturnFilingSummary = strSQLReturnFilingSummary + @" CASE WHEN CONVERT(FLOAT, CONVERT(VARCHAR,FILING_DATE, 112)) > CONVERT(FLOAT, CONVERT(VARCHAR,RETURN_LAST_DATE, 112))
		                                        THEN 'Late Filing Fee applicable' ELSE '' END AS MARK ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQLReturnFilingSummary = strSQLReturnFilingSummary + @"IIF(FORMAT(FILING_DATE,'YYYYMMDD') > FORMAT(RETURN_LAST_DATE,'YYYYMMDD'), 'Late Filing Fee applicable','')  AS MARK ";
                //--
                strSQLReturnFilingSummary = strSQLReturnFilingSummary + @"FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD + @"  
                                                GROUP BY FORM_NO, 
		                                                QTR, 
		                                                FILING_DATE, 
		                                                RETURN_LAST_DATE
                                                ORDER BY FORM_NO, QTR";
                #endregion
                //-----------------------------------------
                #region SUMMARY OF INTERESTS

                ////-- SUMMARY OF INTERESTS u/s 201(1A) / 206C(7)
//                string[,] strTOTAL_INTEREST_TDS_MATRIX = {{"FORM_NO <> '27EQ'", "F", "INTEREST_ALLOCATED", "F"},
//                                                          {"FORM_NO = 'XXX'", "F", "0.00", "F"}};
//                string[,] strTOTAL_INTEREST_TCS_MATRIX = {{"FORM_NO = '27EQ'", "F", "INTEREST_ALLOCATED", "F"},
//                                                          {"FORM_NO = 'YYY'", "F", "0.00", "T"}};
//                strSQLSummaryOfInterests = @"SELECT SUM(" + cmnService.J_SQLDBFormat(cmnService.J_SQLDBFormat(strTOTAL_INTEREST_TDS_MATRIX, J_SQLColFormat.Case_End), J_ColumnType.Double, J_SQLColFormat.NullCheck) + @") AS TOTAL_INTEREST_TDS,
//                                                    SUM(" + cmnService.J_SQLDBFormat(cmnService.J_SQLDBFormat(strTOTAL_INTEREST_TCS_MATRIX, J_SQLColFormat.Case_End), J_ColumnType.Double, J_SQLColFormat.NullCheck) + @") AS TOTAL_INTEREST_TCS
//                                           FROM     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD_INTEREST + @"
////                                           GROUP BY FORM_NO"; 
//                string[,] strTOTAL_INTEREST201_MATRIX = {{"FORM_NO <>'27EQ'", "F", "SUM(INTEREST_ALLOCATED)", "F"},
//                                                      {"FORM_NO = 'XXX'", "F", "SUM(0)", "T"}};
//                strSQLSummaryOfInterests201 = @"SELECT '201 (1A)', " + cmnService.J_SQLDBFormat(strTOTAL_INTEREST201_MATRIX, J_SQLColFormat.Case_End) + @" AS TOTAL_INTEREST
//                                            FROM    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD_INTEREST + @"
//                                            GROUP BY FORM_NO";
//                //
//                string[,] strTOTAL_INTEREST206_MATRIX = {{"FORM_NO ='27EQ'", "F", "SUM(INTEREST_ALLOCATED)", "F"},
//                                                      {"FORM_NO = 'XXX'", "F", "SUM(0)", "T"}};
//                strSQLSummaryOfInterests206 = @"SELECT '206C (7)', " + cmnService.J_SQLDBFormat(strTOTAL_INTEREST206_MATRIX, J_SQLColFormat.Case_End) + @" AS TOTAL_INTEREST
//                                            FROM    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD_INTEREST + @"
//                                            GROUP BY FORM_NO";
                                        //SELECT FORM_NO,
                                        //SUM(INTEREST_ALLOCATED) AS INTEREST_ALLOCATED
                                        //FROM (SELECT CASE WHEN FORM_NO  = '27EQ' THEN '206C (7)' ELSE '201 (1A)' END  AS FORM_NO,
                                        //INTEREST_ALLOCATED 
                                        //FROM   TEMP_FORM_3CD_INTEREST_1) AS SUMMARY
                                        //GROUP BY FORM_NO
                                        //ORDER BY FORM_NO
                string[,] strTOTAL_INTEREST_TDS_MATRIX = {{"FORM_NO <> '27EQ'", "F", "201 (1A)", "T"},
                                                          {"FORM_NO = '27EQ'", "F", "206C (7)", "T"}};
//               
                strSQLSummaryOfInterests = @"SELECT FORMNO,
                                                    SUM(INTEREST_ALLOCATED) AS INTERESTALLOCATED
                                             FROM (SELECT " + cmnService.J_SQLDBFormat(strTOTAL_INTEREST_TDS_MATRIX, J_SQLColFormat.Case_End) + @"  AS FORMNO,
                                                          INTEREST_ALLOCATED 
                                                   FROM   TEMP_FORM_3CD_INTEREST_1) AS SUMMARY
                                             GROUP BY FORMNO
                                             ORDER BY FORMNO";
                //
                strSQLSummaryOfInterestsSubRpt = @"SELECT   FORM3CD_INTEREST_ID," + 
                                                            cmnService.J_SQLDBFormat("DEPOSIT_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + @"  AS DEPOSITDATE,
                                                            INTEREST_ALLOCATED
                                                   FROM     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_FORM_3CD_INTEREST + @"
                                                   ORDER BY DEPOSIT_DATE ";
                #endregion
                //
                #endregion
                //--
                if (rbnExcel.Checked == true)
                {
                    //-- FOR EXCEL EXPORT
                    #region EXCEL EXPORT
                    //-- 
                    strExcelFilePath = txtExcelPath.Text.Trim();
                    //DataTable dtExportSummary = new System.Data.DataTable();
                    //DataTable dtExportDetails = new System.Data.DataTable();
                    ////
                    //DataTable dtExportReturnFilingSummary = new System.Data.DataTable(); //-- Added By Abhishek Dey On 03/04/2018 --
                    ////
                    //DataTable dtRemarkMaster = new System.Data.DataTable();  //-- Added By Abhishek Dey On 04/04/2018 --
                    ////--
                    ////--
                    //DataTable dtSummaryInterest = new System.Data.DataTable();  //-- Added By Abhishek Dey On 12/04/2018 --
                    //DataTable dtSummaryInterestFinal = new System.Data.DataTable();  //-- Added By Abhishek Dey On 12/04/2018 --
                    //DataTable dtInterestPaid = new System.Data.DataTable();  //-- Added By Abhishek Dey On 12/04/2018 --
                    //DataTable dtInterestPaidFinal = new System.Data.DataTable();  //-- Added By Abhishek Dey On 12/04/2018 --
                    //--
                    //--
                    try
                    {
                        //
                        string strTan = cmnService.J_Mid(cmbCompany.Text.Trim(), cmbCompany.Text.Length - 11, 10);
                        strExcelFileName = strTan + "_" + cmbFinancialYear.Text.Trim() + "_" + "3CD.XLSX";
                        strExcelFilePath = strExcelFilePath + "\\" + strExcelFileName;
                        //
                        //dtExportSummary.Columns.AddRange(new DataColumn[7] { new DataColumn("[SECTION NO]"), new DataColumn("[DESCRIPTION]"), new DataColumn("[TOTAL AMOUNT]"), new DataColumn("[TOTAL AMOUNT WITHOUT 'NO DEDUCTION']"), new DataColumn("[TOTAL TDS]"), new DataColumn("[TOTAL AMOUNT DEDUCTED AT LOWER RATE]"), new DataColumn("[TDS DEDUCTED AT LOWER RATE]") });
                        //dtExportDetails.Columns.AddRange(new DataColumn[18] { new DataColumn("[FORM3CD ID]"), new DataColumn("[FORM NO]"), new DataColumn("[QTR]"), new DataColumn("[ENTRY TYPE]"), new DataColumn("[SECTION NO]"), new DataColumn("[SECTION DESCRIPTION]"), new DataColumn("[DEDUCTEE PAN]"), new DataColumn("[DEDUCTEE NAME]"), new DataColumn("[PAYMENT DATE]"), new DataColumn("[DEDUCTED DATE]"), new DataColumn("[PAYMENT AMOUNT]"), new DataColumn("[RATE]"), new DataColumn("[TAX AMOUNT]"), new DataColumn("[TOTAL AMOUNT]"), new DataColumn("[TAX DEPOSITED AMOUNT]"), new DataColumn("[REASON]"), new DataColumn("[DEPOSIT DATE]"), new DataColumn("[RETURN LAST DATE]") });
                        ////
                        ////dtExportReturnFilingSummary.Columns.AddRange(new DataColumn[4] { new DataColumn("[FORM NO]"), new DataColumn("[QTR]"), new DataColumn("[FILING DATE]"), new DataColumn("[RETURN LAST DATE]") });  //-- Added By Abhishek Dey On 03/04/2018 --
                        //dtExportReturnFilingSummary.Columns.AddRange(new DataColumn[5] { new DataColumn("[FORM NO]"), new DataColumn("[QTR]"), new DataColumn("[FILING DATE]"), new DataColumn("[RETURN LAST DATE]"), new DataColumn("[MARK]") });  //-- Added By Abhishek Dey On 03/04/2018 --//
                        //dtRemarkMaster.Columns.AddRange(new DataColumn[3] { new DataColumn("[FORM NO]"), new DataColumn("[REASON]"), new DataColumn("[DESCRIPTION]") });  //-- Added By Abhishek Dey On 04/04/2018 --
                        ////--
                        //DataSet dsSummary = new System.Data.DataSet();
                        //dsSummary = dmlService.J_ExecSqlReturnDataSet(strSQLSummary);
                        ////--
                        //DataSet dsDetails = new System.Data.DataSet();
                        //dsDetails = dmlService.J_ExecSqlReturnDataSet(strSQLDetails);
                        ////-- Added By Abhishek Dey On 03/04/2018 --
                        //DataSet dsExportReturnFilingSummary = new System.Data.DataSet();
                        //dsExportReturnFilingSummary = dmlService.J_ExecSqlReturnDataSet(strSQLReturnFilingSummary);
                        ////-- Added By Abhishek Dey On 04/04/2018 --
                        //DataSet dsRemarkMaster = new System.Data.DataSet();
                        //dsRemarkMaster = dmlService.J_ExecSqlReturnDataSet(strSQLRemarkMaster);
                        ////--
                        //dtSummaryInterest.Columns.AddRange(new DataColumn[2] { new DataColumn("[TDS]"), new DataColumn("[TCS]") });  //-- Added By Abhishek Dey On 12/04/2018 --
                        //dtInterestPaid.Columns.AddRange(new DataColumn[3] { new DataColumn("[FORM3CD INTEREST ID]"), new DataColumn("[DATE]"), new DataColumn("[AMOUNT PAID]") });                        
                        ////-----------------------------------------
                        ////-- Added By Abhishek Dey On 12/04/2018 --
                        //DataSet dsSummaryInterest = new System.Data.DataSet();
                        //dsSummaryInterest = dmlService.J_ExecSqlReturnDataSet(strSQLSummaryOfInterests);
                        ////--
                        //DataSet dsInterestPaid = new System.Data.DataSet();
                        //dsInterestPaid = dmlService.J_ExecSqlReturnDataSet(strSQLSummaryOfInterestsSubRpt);
                        ////-----------------------------------------
                        ////-----------------------------------------
                        //foreach (DataRow dr in dsSummary.Tables[0].Rows)
                        //{
                        //    dtExportSummary.Rows.Add(dr.ItemArray);
                        //}
                        ////--
                        //foreach (DataRow dr in dsDetails.Tables[0].Rows)
                        //{
                        //    dtExportDetails.Rows.Add(dr.ItemArray);
                        //}
                        ////--//-- Added By Abhishek Dey On 03/04/2018 --
                        //foreach (DataRow dr in dsExportReturnFilingSummary.Tables[0].Rows)
                        //{
                        //    dtExportReturnFilingSummary.Rows.Add(dr.ItemArray);
                        //}
                        ////-----------------------------------------
                        ////--
                        ////-- Added By Abhishek Dey On 04/04/2018 --
                        //foreach (DataRow dr in dsRemarkMaster.Tables[0].Rows)
                        //{
                        //    dtRemarkMaster.Rows.Add(dr.ItemArray);
                        //}
                        ////
                        ////-- Added By Abhishek Dey On 12/04/2018 --
                        //foreach (DataRow dr in dsSummaryInterest.Tables[0].Rows)
                        //{
                        //    dtSummaryInterest.Rows.Add(dr.ItemArray);
                        //}
                        ////----------------------------------------
                        //foreach (DataRow dr in dsInterestPaid.Tables[0].Rows)
                        //{
                        //    dtInterestPaid.Rows.Add(dr.ItemArray);
                        //}
                        //// MODIFICATION OF DATATABLE
                        //dtInterestPaid.Columns.Remove("[FORM3CD INTEREST ID]");
                        ////
                        //dtSummaryInterestFinal.Columns.Add("SECTION", typeof(System.String));
                        //dtSummaryInterestFinal.Columns.Add("INTEREST", typeof(System.String));
                        ////
                        //try
                        //{
                        //    if (dtSummaryInterest.Rows.Count > 0)  //-- Added By Abhishek Dey On 24/04/2018 --
                        //    {
                        //        dtSummaryInterestFinal.Rows.Add("201 (1A)", dtSummaryInterest.Rows[0]["[TCS]"]);
                        //        dtSummaryInterestFinal.Rows.Add("206C (7)", dtSummaryInterest.Rows[1]["[TCS]"]);
                        //    }
                        //}
                        //catch
                        //{
                        //}
                        //-----------------------------------------
                        //
                        //-----------------------------------------
                        //-- VALIDATION
                        // CHECK IF SAME NAME FILE EXIST
                        if (File.Exists(strExcelFilePath))  //-- 01/01/2017 --
                        {
                            if (cmnService.J_UserMessage("File exists with same name. Do you want to replace?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                return;
                            else
                            {
                                //if (cmnService.J_IsProcessOpen(strExcelFilePath) == true)
                                //{
                                //    cmnService.J_UserMessage("The file is already open");
                                //    return;
                                //}
                                //else
                                try
                                {
                                    File.Delete(strExcelFilePath);
                                }
                                catch (Exception err)
                                {
                                    this.Cursor = Cursors.Default;
                                    cmnService.J_UserMessage("Could not replace the file!!!\nThe file may be in use...");
                                    return;
                                }
                            }
                        }
                        //--
                        if (string.IsNullOrEmpty(txtExcelPath.Text.Trim()))
                        {
                            cmnService.J_UserMessage("Please select specific folder for import"); 
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        //
                        //if (txtExcelPath.Text.ToUpper().Contains(".XLS") || txtExcelPath.Text.ToUpper().Contains(".XLSX"))
                        //{ }
                        //else
                        //{
                        //    strExcelFilePath = strExcelFilePath + "\\" + strExcelFileName;
                        //}
                        //
                        this.Cursor = Cursors.WaitCursor;
                        // CREATE EXCEL FILE
                        if (CREATE_EXCEL_FILE(strExcelFilePath) == false)
                        {
                            cmnService.J_UserMessage("Some error occurred"); 
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        //
                        //-- Added By Abhishek Dey On 12/04/2018 --
                        //INTEREST PAID
                        //if (dtInterestPaid.Rows.Count > 0)   //-- Added By Abhishek Dey On 24/04/2018 --
                        //{
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "INTEREST PAID") == false) return;
                            //
                            //if (ExportToExcelFromDataTable(dtInterestPaid, "INTEREST PAID") == false)
                            if (ExportToExcelFromSQL(strSQLSummaryOfInterestsSubRpt, "INTEREST PAID") == false)
                            {
                                this.Cursor = Cursors.Default;
                                cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        //}
                        //-----------------------------------------
                        //if (dtSummaryInterestFinal.Rows.Count > 0)   //-- Added By Abhishek Dey On 24/04/2018 --
                        //{
                            //SUMMARY OF INTEREST UNDER SECTION 201(1A) AND 206C(7)
                            if (CREATE_NEW_WORKSHEET(strExcelFilePath, "SUMMARY OF INT 201-1A & 206C-7") == false) return;
                            //
                            //if (ExportToExcelFromDataTable(dtSummaryInterestFinal, "SUMMARY OF INT 201-1A & 206C-7") == false)
                            if (ExportToExcelFromSQL(strSQLSummaryOfInterests, "SUMMARY OF INT 201-1A & 206C-7") == false)
                            {
                                this.Cursor = Cursors.Default;
                                cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        //}
                        //-----------------------------------------
                        //-- Added By Abhishek Dey On 04/04/2018 --
                        //REMARKS
                        if (CREATE_NEW_WORKSHEET(strExcelFilePath, "REMARKS") == false) return;
                        //
                        //if (ExportToExcelFromDataTable(dtRemarkMaster, "REMARKS") == false)
                        if (ExportToExcelFromSQL(strSQLRemarkMaster, "REMARKS") == false)
                        {
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        //-----------------------------------------
                        // DETAILS
                        if (CREATE_NEW_WORKSHEET(strExcelFilePath, "DETAILS") == false) return;
                        //
                        //if (ExportToExcelFromDataTable(dtExportDetails, "DETAILS") == false)
                        if (ExportToExcelFromSQL(strSQLDetails, "DETAILS") == false)
                        {
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        // SUMMARY
                        if (CREATE_NEW_WORKSHEET(strExcelFilePath, "SUMMARY") == false) return;
                        //
                        //if (ExportToExcelFromDataTable(dtExportSummary, "SUMMARY") == false)
                        if (ExportToExcelFromSQL(strSQLSummary, "SUMMARY") == false)
                        {
                            cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Cursor = Cursors.Default; 
                            return;
                        }
                        //-- Added By Abhishek Dey On 03/04/2018 --
                        // RETURN FILING SUMMARY
                        if (CREATE_NEW_WORKSHEET(strExcelFilePath, "RETURN FILING SUMMARY") == false) return;
                        //
                        //if (ExportToExcelFromDataTable(dtExportReturnFilingSummary, "RETURN FILING SUMMARY") == false)
                        if (ExportToExcelFromSQL(strSQLReturnFilingSummary, "RETURN FILING SUMMARY") == false)
                        {
                            cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        //-----------------------------------------
                        // DELETE WORKSHEET
                        if (DELETE_WORKSHEET(strExcelFilePath, "Sheet1") == false) //return;
                        {
                        }
                        //
                        if (DELETE_WORKSHEET(strExcelFilePath, "Sheet2") == false) //return;
                        {
                        }
                        //
                        if (DELETE_WORKSHEET(strExcelFilePath, "Sheet3") == false) //return;
                        {
                        }
                        //--
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Exported Successfully..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //
                        System.Diagnostics.Process.Start(strExcelFilePath);
                        //
                        txtExcelPath.Text = string.Empty;
                    }
                    //---
                    catch (Exception err_handler)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage(err_handler.Message);
                    }
                    #endregion
                }
                else if (rbnPrint.Checked == true)
                {
                    #region PRINT - COMMENTED
                    //////if (rbnPrintSummary.Checked == true)
                    //////{
                    //////    //-- PRINT SUMMARY REPORT
                    //////    //
                    //////    //RptDialog rptDialog = new RptDialog();
                    //////    //rptDialog.PrintForm3CDSummary(strSQLSummary,cmbCompany.Text + " " + cmbFinancialYear.Text );
                    //////    //
                    //////    crForm3CDSummary rptForm3CDSummary = new crForm3CDSummary();
                    //////    rptcls = (ReportClass)rptForm3CDSummary;

                    //////    TextObject objtxtValue;

                    //////    //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                    //////    //objtxtValue.Text = "TDS / TCS - Section wise summary";
                    //////    ////SETTING REPORT TITLE
                    //////    //string[,] strArry = { { "txtReportTitle", cmbCompany.Text + " " + cmbFinancialYear.Text } };

                    //////    objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                    //////    objtxtValue.Text = cmnService.J_Left(cmbCompany.Text, cmbCompany.Text.Length - 12);
                    //////    //
                    //////    objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportTitle2"];
                    //////    objtxtValue.Text = "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text;
                    //////    //SETTING REPORT TITLE
                    //////    string[,] strArry = { { "txtReportTitle", "Form 3CD: TDS / TCS - Section wise summary" } };
                    //////    ReportService rptService = new ReportService();
                    //////    rptService.J_PreviewReport(ref rptcls, this, strSQLSummary, strArry);
                    //////}
                    //////else if (rbnPrintDetail.Checked == true)
                    //////{
                    //////    //-- PRINT DETAIL REPORT
                    //////    //
                    //////    //RptDialog rptDialog = new RptDialog();
                    //////    //rptDialog.PrintForm3CDDetails(strSQLDetails, cmbCompany.Text + " " + cmbFinancialYear.Text);
                    //////    crForm3CDDetails rptForm3CDDetails = new crForm3CDDetails();
                    //////    rptcls = (ReportClass)rptForm3CDDetails;
                    //////    //--
                    //////    string strRemarks = "SELECT FORM_NO, REASON, DESCRIPTION FROM MST_REASON ORDER BY FORM_NO, REASON";
                    //////    rptcls.OpenSubreport("crSubRptForm3CDDetails").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strRemarks).Tables[0]);
                    //////    //--
                    //////    TextObject objtxtValue;

                    //////    //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                    //////    //objtxtValue.Text = "TDS / TCS - Section wise details";
                    //////    ////SETTING REPORT TITLE
                    //////    //string[,] strArry = { { "txtReportTitle", cmbCompany.Text + " " + cmbFinancialYear.Text } };
                    //////    //--
                    //////    objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                    //////    objtxtValue.Text = cmnService.J_Left(cmbCompany.Text,cmbCompany.Text.Length -12) ;
                    //////    //
                    //////    objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportTitle2"];
                    //////    objtxtValue.Text = "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text ;                        
                    //////    //SETTING REPORT TITLE
                    //////    string[,] strArry = { { "txtReportTitle", "Form 3CD: TDS / TCS - Section wise details" } };
                    //////    //--
                    //////    ReportService rptService = new ReportService();
                    //////    rptService.J_PreviewReport(ref rptcls, this, strSQLDetails, strArry);
                    //////}
                    //////else if (rbnReturnFilingSummary.Checked == true)
                    //////{
                    //////    //-- PRINT DETAIL REPORT
                    //////    //
                    //////    //RptDialog rptDialog = new RptDialog();
                    //////    //rptDialog.PrintReturnFilingSummary(strSQLReturnFilingSummary, cmbCompany.Text + " " + cmbFinancialYear.Text);
                    //////    crForm3CDReturnFilingSummary rptForm3CDReturnFilingSummary = new crForm3CDReturnFilingSummary();
                    //////    rptcls = (ReportClass)rptForm3CDReturnFilingSummary;
                    //////    //
                    //////    TextObject objtxtValue;
                    //////    //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                    //////    //objtxtValue.Text = "Dates of Return Filing";
                    //////    ////SETTING REPORT TITLE
                    //////    //string[,] strArry = { { "txtReportTitle", cmbCompany.Text + " " + cmbFinancialYear.Text } };
                    //////    objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                    //////    objtxtValue.Text = cmnService.J_Left(cmbCompany.Text, cmbCompany.Text.Length - 12);
                    //////    //
                    //////    objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportTitle2"];
                    //////    objtxtValue.Text = "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text;
                    //////    //SETTING REPORT TITLE
                    //////    string[,] strArry = { { "txtReportTitle", "Form 3CD: Dates of Return Filing" } };
                    //////    ReportService rptService = new ReportService();
                    //////    rptService.J_PreviewReport(ref rptcls, this, strSQLReturnFilingSummary, strArry);
                    //////}
                    //////else if (rbnSummaryOfInterests.Checked == true)
                    //////{
                    //////    //-- PRINT DETAIL REPORT
                    //////    //
                    //////    //RptDialog rptDialog = new RptDialog();
                    //////    //rptDialog.PrintSummaryOfInterests(strSQLSummaryOfInterests, strSQLSummaryOfInterestsSubRpt, cmbCompany.Text + " " + cmbFinancialYear.Text);
                    //////    //
                    //////    crForm3CDSummaryOfInterest rptForm3CDSummaryOfInterest = new crForm3CDSummaryOfInterest();
                    //////    rptcls = (ReportClass)rptForm3CDSummaryOfInterest;
                    //////    //
                    //////    rptcls.OpenSubreport("crSubRptForm3CDSummaryOfInterest").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strSQLSummaryOfInterestsSubRpt).Tables[0]);
                    //////    //
                    //////    TextObject objtxtValue;
                    //////    //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                    //////    //objtxtValue.Text = "Interest u/s 201(1A)/206C(7)";
                    //////    ////SETTING REPORT TITLE
                    //////    //string[,] strArry = { { "txtReportTitle", cmbCompany.Text + " " + cmbFinancialYear.Text } };
                    //////    objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                    //////    objtxtValue.Text = cmnService.J_Left(cmbCompany.Text, cmbCompany.Text.Length - 12);
                    //////    //
                    //////    objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportTitle2"];
                    //////    objtxtValue.Text = "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text;
                    //////    //SETTING REPORT TITLE
                    //////    string[,] strArry = { { "txtReportTitle", "Form 3CD: Interest u/s 201(1A)/206C(7)" } };
                    //////    ReportService rptService = new ReportService();
                    //////    rptService.J_PreviewReport(ref rptcls, this, strSQLSummaryOfInterests, strArry);
                    //////}
                    //
                    #endregion
                    //
                    #region PRINT
                    if (rbnPrintSummary.Checked == true)
                    {
                        //-- PRINT SUMMARY REPORT
                        //
                        #region COMMENT
                        //crForm3CDSummary rptForm3CDSummary = new crForm3CDSummary();
                        //rptcls = (ReportClass)rptForm3CDSummary;
                        ////
                        //TextObject objtxtValue;
                        ////
                        //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                        //objtxtValue.Text = cmnService.J_Left(cmbCompany.Text, cmbCompany.Text.Length - 12);
                        ////
                        //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportTitle2"];
                        //objtxtValue.Text = "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text;
                        ////SETTING REPORT TITLE
                        //string[,] strArry = { { "txtReportTitle", "Form 3CD: TDS / TCS - Section wise summary" } 
                        //ReportService rptService = new ReportService();
                        //rptService.J_PreviewReport(ref rptcls, this, strSQLSummary, strArry);
                        #endregion
                        //RDLC Report
                        string[,] strArryForm3CDSummaryReport = { {"HeaderCompanyName", cmnService.J_Left(cmbCompany.Text, cmbCompany.Text.Length - 12) },
                                              {"HeaderTANFaYear", "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text },
                                              { "HeaderReportName", "Form 3CD: TDS / TCS - Section wise summary"}
                                                };
                        RptDialog rptDialog = new RptDialog();
                        rptDialog.J_PreviewReportRDLC("\\Reports\\crForm3CDSummary.rdlc", "DataSet1", strSQLSummary, strArryForm3CDSummaryReport);
                    }
                    else if (rbnPrintDetail.Checked == true)
                    {
                        //-- PRINT DETAIL REPORT
                        //
                        #region COMMENT
                        //RptDialog rptDialog = new RptDialog();
                        //rptDialog.PrintForm3CDDetails(strSQLDetails, cmbCompany.Text + " " + cmbFinancialYear.Text);
                        //crForm3CDDetails rptForm3CDDetails = new crForm3CDDetails();
                        //rptcls = (ReportClass)rptForm3CDDetails;
                        ////--
                        //string strSQLDetailsRemarks = "SELECT FORM_NO, REASON, DESCRIPTION FROM MST_REASON ORDER BY FORM_NO, REASON";
                        //rptcls.OpenSubreport("crSubRptForm3CDDetails").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strRemarks).Tables[0]);
                        ////--
                        //TextObject objtxtValue;

                        ////objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                        ////objtxtValue.Text = "TDS / TCS - Section wise details";
                        //////SETTING REPORT TITLE
                        ////string[,] strArry = { { "txtReportTitle", cmbCompany.Text + " " + cmbFinancialYear.Text } };
                        ////--
                        //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                        //objtxtValue.Text = cmnService.J_Left(cmbCompany.Text,cmbCompany.Text.Length -12) ;
                        ////
                        //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportTitle2"];
                        //objtxtValue.Text = "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text ;                        
                        ////SETTING REPORT TITLE
                        //string[,] strArry = { { "txtReportTitle", "Form 3CD: TDS / TCS - Section wise details" } };
                        ////--
                        //ReportService rptService = new ReportService();
                        //rptService.J_PreviewReport(ref rptcls, this, strSQLDetails, strArry
                        #endregion
                        //RDLC Report
                        string[,] strArryForm3CDDetailsReport = { {"HeaderCompanyName", cmnService.J_Left(cmbCompany.Text, cmbCompany.Text.Length - 12) },
                                              {"HeaderTANFaYear", "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text },
                                              { "HeaderReportName", "Form 3CD: TDS / TCS - Section wise details"}
                                                };
                        string strSQLDetailsRemarks = "SELECT FORM_NO, REASON, DESCRIPTION FROM MST_REASON ORDER BY FORM_NO, REASON";
                        RptDialog rptDialog = new RptDialog();
                        rptDialog.J_PreviewReportRDLC2DataSet("\\Reports\\crForm3CDDetails.rdlc", "DataSet1", strSQLDetails, "DataSet2", strSQLDetailsRemarks, strArryForm3CDDetailsReport);
                    }
                    else if (rbnReturnFilingSummary.Checked == true)
                    {
                        //-- PRINT DETAIL REPORT
                        //
                        #region COMMENT
                        //RptDialog rptDialog = new RptDialog();
                        //rptDialog.PrintReturnFilingSummary(strSQLReturnFilingSummary, cmbCompany.Text + " " + cmbFinancialYear.Text);
                        //crForm3CDReturnFilingSummary rptForm3CDReturnFilingSummary = new crForm3CDReturnFilingSummary();
                        //rptcls = (ReportClass)rptForm3CDReturnFilingSummary;
                        ////
                        //TextObject objtxtValue;
                        ////objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                        ////objtxtValue.Text = "Dates of Return Filing";
                        //////SETTING REPORT TITLE
                        ////string[,] strArry = { { "txtReportTitle", cmbCompany.Text + " " + cmbFinancialYear.Text } };
                        //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                        //objtxtValue.Text = cmnService.J_Left(cmbCompany.Text, cmbCompany.Text.Length - 12);
                        ////
                        //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportTitle2"];
                        //objtxtValue.Text = "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text;
                        ////SETTING REPORT TITLE
                        //string[,] strArry = { { "txtReportTitle", "Form 3CD: Dates of Return Filing" } };
                        //ReportService rptService = new ReportService();
                        //rptService.J_PreviewReport(ref rptcls, this, strSQLReturnFilingSummary, strArry);
                        #endregion
                        //RDLC Report
                        string[,] strArryForm3CDReturnFilingSummaryReport = { {"HeaderCompanyName", cmnService.J_Left(cmbCompany.Text, cmbCompany.Text.Length - 12) },
                                              {"HeaderTANFaYear", "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text },
                                              { "HeaderReportName", "Form 3CD: Dates of Return Filing"}
                                                };
                        RptDialog rptDialog = new RptDialog();
                        rptDialog.J_PreviewReportRDLC("\\Reports\\crForm3CDReturnFilingSummary.rdlc", "DataSet1", strSQLReturnFilingSummary, strArryForm3CDReturnFilingSummaryReport);
                    }
                    else if (rbnSummaryOfInterests.Checked == true)
                    {
                        //-- PRINT DETAIL REPORT
                        //
                        #region COMMENTED
                        //RptDialog rptDialog = new RptDialog();
                        //rptDialog.PrintSummaryOfInterests(strSQLSummaryOfInterests, strSQLSummaryOfInterestsSubRpt, cmbCompany.Text + " " + cmbFinancialYear.Text);
                        ////
                        //crForm3CDSummaryOfInterest rptForm3CDSummaryOfInterest = new crForm3CDSummaryOfInterest();
                        //rptcls = (ReportClass)rptForm3CDSummaryOfInterest;
                        ////
                        //rptcls.OpenSubreport("crSubRptForm3CDSummaryOfInterest").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strSQLSummaryOfInterestsSubRpt).Tables[0]);
                        ////
                        //TextObject objtxtValue;
                        ////objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                        ////objtxtValue.Text = "Interest u/s 201(1A)/206C(7)";
                        //////SETTING REPORT TITLE
                        ////string[,] strArry = { { "txtReportTitle", cmbCompany.Text + " " + cmbFinancialYear.Text } };
                        //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportHeader"];
                        //objtxtValue.Text = cmnService.J_Left(cmbCompany.Text, cmbCompany.Text.Length - 12);
                        ////
                        //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[1].ReportObjects["txtReportTitle2"];
                        //objtxtValue.Text = "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text;
                        ////SETTING REPORT TITLE
                        //string[,] strArry = { { "txtReportTitle", "Form 3CD: Interest u/s 201(1A)/206C(7)" } };
                        //ReportService rptService = new ReportService();
                        //rptService.J_PreviewReport(ref rptcls, this, strSQLSummaryOfInterests, strArry);
                        #endregion

                        //RDLC Report
                        string[,] strArryForm3CDDetailsReport = { {"HeaderCompanyName", cmnService.J_Left(cmbCompany.Text, cmbCompany.Text.Length - 12) },
                                              {"HeaderTANFaYear", "TAN : " + cmnService.J_Mid(cmbCompany.Text, cmbCompany.Text.Length - 11, 10) + "   FA Year : " + cmbFinancialYear.Text },
                                              { "HeaderReportName", "Form 3CD: Interest u/s 201(1A)/206C(7)"}
                                                };
                        RptDialog rptDialog = new RptDialog();
                        rptDialog.J_PreviewReportRDLC2DataSet("\\Reports\\crForm3CDSummaryOfInterests.rdlc", "DataSet1", strSQLSummaryOfInterests, "DataSet2", strSQLSummaryOfInterestsSubRpt, strArryForm3CDDetailsReport);
                    }
                    //
                    #endregion
                    //
                    this.Cursor = Cursors.Default;
                    //
                }
                else if(rbnCSV.Checked==true)
                {

                    if (Directory.Exists(strExcelFilePath) == false)
                        Directory.CreateDirectory(strExcelFilePath);
                    //
                    #region CSV
                    if (ExportToCSV(strSQLSummaryOfInterestsSubRpt, Path.Combine(strExcelFilePath, "INTEREST PAID.csv")) == false)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Failed export data to CSV...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //
                    if (ExportToCSV(strSQLSummaryOfInterests, Path.Combine(strExcelFilePath, "SUMMARY_OF_INT_201-1A_&_206C-7.csv")) == false)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Failed export data to CSV...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //
                    if (ExportToCSV(strSQLRemarkMaster, Path.Combine(strExcelFilePath, "REMARKS.csv")) == false)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Failed export data to CSV...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //
                    if (ExportToCSV(strSQLDetails, Path.Combine(strExcelFilePath, "DETAILS.csv")) == false)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Failed export data to CSV...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //
                    if (ExportToCSV(strSQLSummary, Path.Combine(strExcelFilePath, "SUMMARY.csv")) == false)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Failed export data to CSV...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //
                    if (ExportToCSV(strSQLReturnFilingSummary, Path.Combine(strExcelFilePath, "RETURN_FILING_SUMMARY.csv")) == false)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Failed export data to CSV...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //
                    this.Cursor = Cursors.Default;
                    //
                    cmnService.J_UserMessage("Exported Successfully..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //
                    System.Diagnostics.Process.Start(strExcelFilePath);
                    //
                    txtExcelPath.Text = string.Empty;
                    #endregion
                }
                //grpPrint.Visible = false;
                //grpRegularReturn.Enabled = true;
                //grpDetailsGrid.Enabled = true;
                //grpButton.Enabled = true;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        //-- Added By Abhishek Dey On 22/02/2018 --
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
        //-----------------------------------------


        #region lblForm3CDVideoDemo_MouseMove
        private void lblForm3CDVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(lblForm3CDVideoDemo, "Click here to view Video Demo of this module in the web");
        }
        #endregion

        #region lblForm3CDVideoDemo_Click
        private void lblForm3CDVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/embed/NDQC4ZkDVbY?autoplay=1");
        }
        #endregion

        //-- Added By Abhishek Dey On 21/05/2018 --
        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("Trn3CDReport");
            Tan.ShowDialog();
            //--------------
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        //-----------------------------------------
        #endregion
        //-----------------------------------------
        #endregion

        #region User Define Functions

        #region LoadGrid
        private void LoadGrid(bool blBlank)
        {
            //
            blnLoadGridExit = true;
            //-- GRID
            DataSet dsetGridClone = new DataSet();
            //
            string[,] strMatrix = {{"GRID_ID", "0", "", "Right", "", "F", ""},
                                    {"Form No.", "0", "S", "", "", "F", ""},
                                    {"", "100", "S", "", "", "", "T"},
                                    {"", "44", "S", "", "", "", "T"},
                                    {"", "250", "", "", "", "", "T"},
                                    {"BI", "0", "", "", "", "F", ""},
                                    {"REGULAR_CORRECTION", "0", "", "", "", "F", ""},
                                    {"", "150", "", "", "", "", "T"}};
            //
            strSQL = @"SELECT  GRID_ID,
                               FORM_NO      AS FORM_NO,
                               FORM_DISPLAY AS FORM_DISPLAY,
                               QTR          AS QTR,
                               FORM_DESC    AS FORM_DESC,
                               '0'          AS BASIC_INFO_ID,
                               ''           AS REGULAR_CORRECTION,
                               ''           AS STATUS
                      FROM     PAR_3CD_GRID ";
            if(blBlank==true)
              strSQL = strSQL + " WHERE 1=2 " ;        
            //
              strSQL = strSQL + " ORDER BY GRID_ID";
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvReturnDetails, strSQL, strMatrix);
            dgvReturnDetails.ClearSelection();
            //--
            ClearControlsTextBoxes();
            tbcData.Enabled = false;
            //BtnRefresh.Enabled = false;
            //BtnRefresh.BackColor = Color.LightGray;
            //
            blnLoadGridExit = true;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
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
                "      AND    VISIBILITY_AADHAAR_RETURN_FLAG = 1 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //-- COMPANY
            //-----------
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME + ' [' + TAN_NO + ']' " +
                "      FROM   MST_COMPANY " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            
            blnSelectComboExit = false;
            //-----------
        }
        #endregion

        #region ClearControlsTextBoxes
        private void ClearControlsTextBoxes()
        {
            //grpButtons.Enabled = true;
            //
            txtTotalChallanRecords.Text = "";
            txtTotalDeducteeRecords.Text = "";
            txtTotalChallanAmount.Text = "";
            txtTotalDeducteeTDS.Text = "";
            txtAmountPaid.Text = "";
            //
            //txtReceiptNo.Text = "";
            //txtDateofFiling.Text = "";
            //txtTokenNo.Text = "";
            //
            txtFVUPath.Text = "";
        }
        #endregion

        #region ControlSummaryBasicInfo
        private void ControlSummaryBasicInfo(long BasicInfoID, string FormNo, string Qtr, string RegConso)
        {
            if (BasicInfoID == 0)
            {
                txtTotalChallanRecords.Text = "";
                txtTotalDeducteeRecords.Text = "";
                txtTotalChallanAmount.Text = "";
                txtTotalDeducteeTDS.Text = "";
                txtAmountPaid.Text = "";
                //
                grpConsoFileDetails.Visible = false;
                //grpConsoFileDetails1.Visible = false;
                //grpControlSummary.Visible = false;
                //pnlLine1.Visible = false;
                //pnlLine2.Visible = false;
                //btnNext.Enabled = false;
                //lblTotalRecordsReturn.Visible = false;
                //btnNext.BackColor = Color.LightGray;
                return;
            }
            grpControlSummary.Visible = true;
            //pnlLine1.Visible = true;
            //pnlLine2.Visible = true;
            //lblTotalRecordsReturn.Visible = true;
            //btnNext.Enabled = true;
            //btnNext.BackColor = Color.Lavender;
            txtTotalChallanRecords.Text = "0";
            txtTotalDeducteeRecords.Text = "0";
            txtTotalChallanAmount.Text = "0.00";
            txtTotalDeducteeTDS.Text = "0.00";
            txtAmountPaid.Text = "0.00";
            grpConsoFileDetails.Visible = false;
            //grpConsoFileDetails1.Visible = false;

            if (RegConso == "R")
            {
                // CONTROL SUMMARY VALUES
                txtTotalChallanRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(CHALLAN_ID) AS COUNT_CHALLAN_ID FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID));
                txtTotalDeducteeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID));
                txtTotalChallanAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID))));
                txtTotalDeducteeTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
                txtAmountPaid.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
                //
                long lngTotalRecordsReturn = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID, J_QueryType.DirectQuery);
                //lblTotalRecordsReturn.Text = "Total Records in this return : " + Convert.ToString(lngTotalRecordsReturn);
                //
                if (FormNo == T_FormNo.F24Q && Qtr == T_Qtr.Q4)
                {
                    lblNetTaxableIncome.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBatchBasicInfoID)) == "" ? "0.00" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBatchBasicInfoID))));
                    lblNetTaxableIncome.Visible = true;
                    lblNetTaxableIncomeCaption.Visible = true;
                }
                else
                {
                    lblNetTaxableIncome.Visible = false;
                    lblNetTaxableIncomeCaption.Visible = false;
                }
            }
            else if (RegConso == "C")
            {
                // CONTROL SUMMARY VALUES
                txtTotalChallanRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(HDR_CHALLAN_ID) AS COUNT_CHALLAN_ID FROM COR_HDR_CHALLAN_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID));
                txtTotalDeducteeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(HDR_DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM COR_HDR_DEDUCTEE_DETAILS_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID));
                txtTotalChallanAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM COR_HDR_CHALLAN_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM COR_HDR_CHALLAN_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID))));
                txtTotalDeducteeTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM COR_HDR_DEDUCTEE_DETAILS_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM COR_HDR_DEDUCTEE_DETAILS_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID))));
                txtAmountPaid.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM COR_HDR_DEDUCTEE_DETAILS_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM COR_HDR_DEDUCTEE_DETAILS_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID))));
                //
                long lngTotalRecordsReturn = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM COR_HDR_DEDUCTEE_DETAILS_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID, J_QueryType.DirectQuery);
                //lblTotalRecordsReturn.Text = "Total Records in this return : " + Convert.ToString(lngTotalRecordsReturn);
                //
                if (FormNo == T_FormNo.F24Q && Qtr == T_Qtr.Q4)
                {
                    lblNetTaxableIncome.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM COR_HDR_SALARY_DETAILS_3CD WHERE BATCH_HEADER_ID = " + lngBatchBasicInfoID)) == "" ? "0.00" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM COR_HDR_SALARY_DETAILS_3CD WHERE BATCH_HEADER_ID = " + lngBatchBasicInfoID))));
                    lblNetTaxableIncome.Visible = true;
                    lblNetTaxableIncomeCaption.Visible = true;
                }
                else
                {
                    lblNetTaxableIncome.Visible = false;
                    lblNetTaxableIncomeCaption.Visible = false;
                }
                //
                grpConsoFileDetails.Visible = true;
                //grpConsoFileDetails1.Visible = true;
                //txtRPUDate.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FILE_DATE FROM COR_HDR_BATCH_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID));
                txtRPUDate.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT " + cmnService.J_SQLDBFormat("FILE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " FROM COR_HDR_BATCH_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID));
                txtImportDate.Text = string.Format("{0:dd/MM/yyyy hh:mm tt}", Convert.ToDateTime(dmlService.J_ExecSqlReturnScalar("SELECT IMPORTED_DATE FROM COR_HDR_BATCH_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID)));
                //
                //txtRPUDate1.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT " + cmnService.J_SQLDBFormat("FILE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " FROM COR_HDR_BATCH_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID));
                //txtImportDate1.Text = string.Format("{0:dd/MM/yyyy hh:mm tt}", Convert.ToDateTime(dmlService.J_ExecSqlReturnScalar("SELECT IMPORTED_DATE FROM COR_HDR_BATCH_3CD WHERE BATCH_HEADER_ID = " + BasicInfoID)));
            }
            //
        }
        #endregion
        
        #region ReturnFilingStatus
        private void ReturnFilingStatus(long BasicInfoId, string RegConso)
        {
            IDataReader drdShowRecord = null;
            //
            try
            {
                //txtReceiptNo.Text = "";
                //txtDateofFiling.Text = "";
                //txtTokenNo.Text = "";
                //  
                if (BasicInfoId == 0 || RegConso=="C")
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
                
        #region CheckFVUCompatibility

        private bool CheckFVUCompatibility(string FVUPath, string FormNo, string Qtr, string TAN)
        {
            try
            {
                //To Initialize Financial Year
                string FinancialYear = "";
                string strUploadType = "";
                string strFVUVersion = "";
                string strFormNo = "";
                string strTAN = "";
                string strFinancialYear = "";
                string strQTR = "";
                //
                int intCaratPosition = 0;
                //
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
                            //MODIFIED BY @@DHRUB--2015-08-10 
                            //strCheckCompatibilityMessage = "Other than Regular Return FVU is supported";
                            strCheckCompatibilityMessage = "Invalid file selected. Please select either Conso file or text file of Regular returns.";
                            //--
                            txtRdr.Close();
                            txtRdr.Dispose();
                            //
                            btnSelectTDSPath.Select();
                            return false;
                        }
                        //
                        //-- ADDED BY ABHISHEK ON 01/12/2017 --
                        //CHECKING DATE OF CREATION OF OF TDS FILE FOR DISPLAY PURPOSE
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strFileCreationDate = ListLines[i].Substring(intCaratPosition + 1);
                        strFileCreationDate = ListLines[i].Substring(intCaratPosition + 1, strFileCreationDate.IndexOf("^"));
                        //--
                        //DateTime dtFileCreationDate = dtService.J_ConvertddMMyyyy(strFileCreationDate);

                        string strFVUDate = cmnService.J_Left(strFileCreationDate, 2) + "/" + cmnService.J_Mid(strFileCreationDate, 2,2) +"/" + cmnService.J_Right(strFileCreationDate, 4);
                        if (dtService.J_ConvertToIntYYYYMMDD(strFVUDate) < dtService.J_ConvertToIntYYYYMMDD(txtRPUDate.Text))
                        {
                            //strCheckCompatibilityMessage = "Old file!!!";
                            strCheckCompatibilityMessage = "The Conso file which is imported on " + txtImportDate.Text + " was of later date [" + txtRPUDate.Text + "] than the selected Conso file [" + strFVUDate +"].\n";
                            strCheckCompatibilityMessage = strCheckCompatibilityMessage + "Only later/current date Conso file import is allowed.";
                            txtRdr.Close();
                            txtRdr.Dispose();

                            btnSelectTDSPath.Select();
                            return false;
                        }
                        //--
                        //                        
                        strLogFileCreationDate = cmnService.J_Right(strFileCreationDate, 4) + "-" + cmnService.J_Mid(strFileCreationDate, 2, 2) + "-" + cmnService.J_Left(strFileCreationDate, 2);
                        //-----------------------------------------
                        //
                        // FVU Version [2.126]
                        for (int a = 1; a < 10; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            //
                        }
                        strFVUVersion = ListLines[i].Substring(intCaratPosition + 1);
                        strFVUVersion = ListLines[i].Substring(intCaratPosition + 1, strFVUVersion.IndexOf("^"));
                        //                    
                        //dblFVUVersion = cmnService.J_ReturnDoubleValue(strFVUVersion.Substring(3).Trim());
                        //if (dblFVUVersion == 2.126)
                        //{
                        //    strCheckCompatibilityMessage = "FVU Version before 2.128 is not supported";
                        //    txtRdr.Close();
                        //    txtRdr.Dispose();
                        //    return false;
                        //}
                        // FVU Version [2.128, 2.129, 3.0]
                        for (int a = 1; a < 1; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                            //
                        }
                        strFVUVersion = ListLines[i].Substring(intCaratPosition + 1);
                        strFVUVersion = ListLines[i].Substring(intCaratPosition + 1, strFVUVersion.IndexOf("^"));
                        //                    
                        //dblFVUVersion = cmnService.J_ReturnDoubleValue(strFVUVersion.Substring(3).Trim());
                        //if (dblFVUVersion < 2.128)
                        //{
                        //    strCheckCompatibilityMessage = "FVU Version before 2.128 is not supported";
                        //    txtRdr.Close();
                        //    txtRdr.Dispose();
                        //    return false;
                        //}
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
                        //-- Added By Abhishek On 01/12/2017 --
                        strLogForm = strFormNo;
                        //-------------------------------------
                        // FORM NO. CHECK
                        if (FormNo != strFormNo)
                        {
                            strCheckCompatibilityMessage = "Form No. mismatched";
                            txtRdr.Close();
                            txtRdr.Dispose();
                            //
                            btnSelectTDSPath.Select();
                            return false;
                        }

                        // TAN
                        for (int a = 1; a < 9; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTAN = ListLines[i].Substring(intCaratPosition + 1, 10);
                        //-- Added By Abhishek On 01/12/2017 --
                        strLogTanNo = strTAN;
                        //-------------------------------------
                        // TAN CHECK
                        //#########################################################################
                        // MODIFIED BY DHRUB ON 2015-08-03 [FOR COMPANY DATA INSERT]
                        //if (cmbCompany.Text != strCreateNewCompanyText)
                        //{
                            if (TAN != strTAN)
                            {
                                strCheckCompatibilityMessage = "TAN mismatched";
                                txtRdr.Close();
                                txtRdr.Dispose();
                                //
                                cmbCompany.Select();
                                return false;
                            }
                        //}
                        //-- VALIDATE FOR [ IF TAN EXISTS INTO DATABASE ]
                        //else
                        //{
                        //    if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM MST_COMPANY WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(strTAN.ToUpper()) + "'")) > 0)
                        //    {
                        //        string strCompanyNameFromDatabase = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_NAME FROM MST_COMPANY WHERE TAN_NO ='" + cmnService.J_ReplaceQuote(strTAN.ToUpper()) + "'"));
                        //        //strCheckCompatibilityMessage = "Company already exists for " + strCompanyNameFromDatabase + " (" + cmnService.J_ReplaceQuote(strTAN.ToUpper()) + ").";
                        //        strCheckCompatibilityMessage = "Company " + strCompanyNameFromDatabase + " [ " + cmnService.J_ReplaceQuote(strTAN.ToUpper()) + " ] already exists, select this from Company list.";
                        //        txtRdr.Close();
                        //        txtRdr.Dispose();
                        //        //
                        //        cmbCompany.Select();
                        //        return false;
                        //    }

                        //}
                        //#########################################################################
                        // FINANCIAL YEAR
                        for (int a = 1; a < 5; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strFinancialYear = ListLines[i].Substring(intCaratPosition + 1, 6);
                        // FINANCIAL YEAR CHECK
                        FinancialYear = cmnService.J_Left(cmbFinancialYear.Text, 4) + cmnService.J_Right(cmbFinancialYear.Text, 2);
                        //-- Added By Abhishek On 01/12/2017 --
                        strLogFaYear = FinancialYear.Insert(4, "-");
                        //
                        if (FinancialYear != strFinancialYear)
                        {
                            strCheckCompatibilityMessage = "Financial Year mismatched";
                            txtRdr.Close();
                            txtRdr.Dispose();
                            //
                            cmbFinancialYear.Select();
                            return false;
                        }

                        // QTR
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strQTR = ListLines[i].Substring(intCaratPosition + 1, 2);
                        //-- Added By Abhishek On 01/12/2017 --
                        strLogQuarter = strQTR;
                        //-------------------------------------
                        // QTR CHECK
                        if (Qtr != strQTR)
                        {
                            strCheckCompatibilityMessage = "Qtr mismatched";
                            txtRdr.Close();
                            txtRdr.Dispose();
                            //
                            btnSelectTDSPath.Select();
                            return false;
                        }

                    }
                }
                txtRdr.Close();
                txtRdr.Dispose();
                return true;
            }
            catch (Exception err)
            {
                btnSelectTDSPath.Select();
                return false;
            }
        }

        #endregion

        #region ImportTransactionsFromFVU
        private bool ImportTransactionsFromFVU(string FVUPath, double CompanyId)
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
                                                " + cmnService.J_GetDataType("F69", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F70", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F71", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F72", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F73", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F74", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F75", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F76", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F77", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F78", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F79", J_ColumnType.String, 255) + @",
                                                " + cmnService.J_GetDataType("F80", J_ColumnType.String, 255) + @")";
                        dmlService.J_ExecSql(strSQL);
                    }
                    dmlService.J_Commit();
                    #endregion
                    //--
                    DataTable dt = new DataTable();
                    //
                    for (int col = 0; col < 80; col++)
                        dt.Columns.Add(new DataColumn("F" + (col + 1).ToString()));
                    //
                    string[] lines = System.IO.File.ReadAllLines(Application.StartupPath + "\\" + strImporttableName + ".txt");
                    //
                    foreach (string line in lines)
                    {
                        string[] columns = line.Split('^');
                        dt.Rows.Add(columns);
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
                strChequeValue = dmlService.J_ExecSqlReturnScalar(strSQL).ToString().Trim().ToUpper();
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
                reader = dmlService.J_ExecSqlReturnReader("SELECT FORM_NO,QTR,TAN_NO,FA_YEAR FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + "");
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


                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + " (" +
                         "       CHALLAN_SL_NO, " +
                         "       SL_NO, " +
                         "       MODE, " +
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
                         "       DEDUCTEE_TAX_ID) " +
                         "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CHALLAN_SL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SL_NO, " +
                         "       " + cmnService.J_SQLDBFormat("F6", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS MODE, " +
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
                         "       " + cmnService.J_SQLDBFormat("F43", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS DEDUCTEE_TAX_ID   " +
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
                //MessageBox.Show("1.4");
                #region Insert Data To Correction Tables

                #region Insert data to COR_HDR_BATCH_3CD

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


                strSQL = "INSERT INTO COR_HDR_BATCH_3CD (ASST_ID," +
                         "                           COMPANY_ID," +
                         "                           FORM_NO," +
                         "                           QTR," +
                         "                           IMPORTED_DATE," +
                         "                           PREVIOUS_RRR_NO," +
                         "                           ORIGINAL_RRR_NO," +
                         "                           FILE_DATE," +
                         "                           HASH_VALUE," +
                         "                           TDS_FILE_PATH, " +
                         "                           INVALID_RETURN) " +
                         "SELECT ASST_ID                             AS ASST_ID," +
                         "      " + CompanyId + "," +
                         "       FORM_NO                             AS FORM_NO," +
                         "       QTR                                 AS QTR," +
                         "  '" + System.DateTime.Now.ToString() + "' AS IMPORTED_DATE," +
                         "       PREVIOUS_RRR_NO                     AS PREVIOUS_RRR_NO," +
                         "       ORIGINAL_RRR_NO                     AS ORIGINAL_RRR_NO," +
                         "   " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strFileCreationDate) + cmnService.J_DateOperator() + " AS FILE_DATE," +
                         "  '" + strFileHash + "'                    AS HASH_VALUE," +
                         "  '" + OutputFilePath + "'                 AS TDS_FILE_PATH," +
                         "   " + intDefaultInvalidReturnValue + "    AS INVALID_RETURN " +
                         "FROM  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ",MST_ASSESSMENT " +
                         "WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".ASST_YEAR = MST_ASSESSMENT.ASST_YEAR";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;

                strSQL = "SELECT MAX(BATCH_HEADER_ID) FROM COR_HDR_BATCH_3CD";
                lngBatchID = Convert.ToInt64(dmlService.J_ExecSqlReturnScalar(strSQL));

                #endregion

                #region Insert data to COR_HDR_COMPANY_3CD
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
                strSQL = "INSERT INTO COR_HDR_COMPANY_3CD (BATCH_HEADER_ID," +
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

                #region Insert data to COR_HDR_CHALLAN_3CD

                string[,] strBOOK_ENTRY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".BOOK_ENTRY = 'Y'" , "F", "1", "F"},
                                           {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".BOOK_ENTRY = 'N'" , "F", "0", "F"},
                                           {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".BOOK_ENTRY = ''" , "F", "2", "F"}};

                strSQL = "INSERT INTO COR_HDR_CHALLAN_3CD (BATCH_HEADER_ID," +
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
                         "      ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".MINOR_CODE = MST_MINOR_HEAD.MINOR_HEAD_CODE) " +
                         "ORDER BY SL_NO";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                prgBar.Value = prgBar.Value + 5;
                #endregion

                #region Insert data to COR_HDR_DEDUCTEE_DETAILS_3CD
                //--
                #region GET FA YEAR
                strSQL = "SELECT ASST_ID FROM COR_HDR_BATCH_3CD WHERE BATCH_HEADER_ID = " + lngBatchID;
                long lngAsstID = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                #endregion
                //--
                string[,] strCASH_BOOK_ENTRY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".CASH_BOOK_ENTRY = 'Y'" , "F", "1", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".CASH_BOOK_ENTRY = 'N'" , "F", "0", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".CASH_BOOK_ENTRY = ''" , "F", "0", "F"}};

                string[,] strINVALID_PAN = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".INVALID_PAN = 'N'" , "F", "1", "F"},
                                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DD + ".INVALID_PAN = 'Y'" , "F", "0", "F"}};

                strSQL = "INSERT INTO COR_HDR_DEDUCTEE_DETAILS_3CD (HDR_CHALLAN_ID," +
                         "                                      BATCH_HEADER_ID," +
                         "                                      SL_NO," +
                         "                                      MODE," +
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
                         "                                      DEDUCTEE_TAX_ID) " +
                         "SELECT CHALLAN_SL_NO          AS HDR_CHALLAN_ID," +
                         "    " + lngBatchID + "        AS BATCH_HEADER_ID," +
                         "        SL_NO                 AS SL_NO," +
                         "        MODE                  AS MODE," +
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
                         "        DEDUCTEE_TAX_ID                            AS DEDUCTEE_TAX_ID " +
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
                    strSQL = strSQL + " AND   MST_SECTION.FORM_NAME = '" + strFormNo + "'  AND MST_SECTION.DIFF_RATES = 0 ";//-- 2016/05/18 ANIKWA...
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
                    strSQL = "UPDATE COR_HDR_DEDUCTEE_DETAILS_3CD " +
                             "SET    DEDUCTEE_CODE = '0' + DEDUCTEE_CODE " +
                             "WHERE  BATCH_HEADER_ID  = " + lngBatchID + " " +
                             "AND    LEN(DEDUCTEE_CODE) = 1";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage("Import Failed!");
                        return false;
                    }
                }

                strSQL = "UPDATE COR_HDR_DEDUCTEE_DETAILS_3CD " +
                         "INNER JOIN COR_HDR_CHALLAN_3CD " +
                         "ON    COR_HDR_DEDUCTEE_DETAILS_3CD.BATCH_HEADER_ID  = COR_HDR_CHALLAN_3CD.BATCH_HEADER_ID " +
                         "SET   COR_HDR_DEDUCTEE_DETAILS_3CD.HDR_CHALLAN_ID   = COR_HDR_CHALLAN_3CD.HDR_CHALLAN_ID " +
                         "WHERE COR_HDR_DEDUCTEE_DETAILS_3CD.HDR_CHALLAN_ID   = COR_HDR_CHALLAN_3CD.SL_NO " +
                         "AND   COR_HDR_DEDUCTEE_DETAILS_3CD.BATCH_HEADER_ID  = " + lngBatchID + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
                //-- UPDATE SECTION_ID for all

                //-- UPDATE ALL EXISTING DEDUCTEE WITH THE SECTION ID
                strSQL = "UPDATE ((COR_HDR_DEDUCTEE_DETAILS_3CD " +
                         "INNER JOIN COR_HDR_CHALLAN_3CD " +
                         "ON     COR_HDR_DEDUCTEE_DETAILS_3CD.HDR_CHALLAN_ID  = COR_HDR_CHALLAN_3CD.HDR_CHALLAN_ID) " +
                         "INNER JOIN COR_HDR_BATCH_3CD " +
                         "ON    COR_HDR_DEDUCTEE_DETAILS_3CD.BATCH_HEADER_ID  = COR_HDR_BATCH_3CD.BATCH_HEADER_ID) " +
                         "SET   COR_HDR_DEDUCTEE_DETAILS_3CD.SECTION_ID       = COR_HDR_CHALLAN_3CD.SECTION_ID " +
                         "WHERE COR_HDR_BATCH_3CD.ASST_ID                    <= " + T_FinancialYearID.F2012_13ID + " " +
                         "AND   COR_HDR_DEDUCTEE_DETAILS_3CD.BATCH_HEADER_ID  = " + lngBatchID;

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
                //--
                #region UPDATE COR_HDR_DEDUCTEE_DETAILS_3CD

                //Creation of temp table through SELECT ... INTO ... 
                strSQL = @"SELECT COR_HDR_DEDUCTEE_DETAILS_3CD.HDR_CHALLAN_ID, 
                                  SUM(COR_HDR_DEDUCTEE_DETAILS_3CD.TAX_DEPOSITED_AMOUNT) AS CTRL_TOT_TAX 
                           INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @"
                           FROM COR_HDR_DEDUCTEE_DETAILS_3CD
                           WHERE COR_HDR_DEDUCTEE_DETAILS_3CD.BATCH_HEADER_ID = " + lngBatchID + @"
                           GROUP BY COR_HDR_DEDUCTEE_DETAILS_3CD.HDR_CHALLAN_ID";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }

                strSQL = @"UPDATE  COR_HDR_CHALLAN_3CD
                           INNER JOIN 
                           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @"
                           ON    COR_HDR_CHALLAN_3CD.HDR_CHALLAN_ID = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @".HDR_CHALLAN_ID
                           SET   COR_HDR_CHALLAN_3CD.CTRL_TOT_TAX   = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + @".CTRL_TOT_TAX";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }

//                strSQL = @"UPDATE  COR_TRN_CHALLAN_3CD
//                           INNER JOIN 
//                           COR_HDR_CHALLAN_3CD
//                           ON    COR_HDR_CHALLAN_3CD.HDR_CHALLAN_ID  = COR_TRN_CHALLAN_3CD.HDR_CHALLAN_ID
//                           SET   COR_TRN_CHALLAN_3CD.CTRL_TOT_TAX    = COR_HDR_CHALLAN_3CD.CTRL_TOT_TAX
//                           WHERE COR_TRN_CHALLAN_3CD.BATCH_HEADER_ID = " + lngBatchID;
//                if (dmlService.J_ExecSql(strSQL) == false)
//                {
//                    dmlService.J_Rollback();
//                    return false;
//                }
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

                #endregion
                //MessageBox.Show("1.5");
                #region For Salary Details

                if (strFormNo == T_FormNo.F24Q && strQTR == T_Qtr.Q4)
                {
                    string[,] strF62 = {{"F62 IS NULL" , "F", "NULL", "F"},
                                        {"F62 IS NOT NULL " , "F", ConvertSQLDate("F62"), "F"}};

                    string[,] strF63 = {{"F63 IS NULL" , "F", "NULL", "F"},
                                        {"F63 IS NOT NULL " , "F", ConvertSQLDate("F63"), "F"}};

                    #region Insert " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + "
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + " " +
                             "SELECT " + cmnService.J_SQLDBFormat("F4", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS SL_NO, " +
                             "       " + cmnService.J_SQLDBFormat("F5", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS MODE, " + //-- 2017/02/24
                             "       " + cmnService.J_SQLDBFormat("F7", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS EMPLOYEE_PAN, " +
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
                    strSQL = strSQL + "  " + cmnService.J_SQLDBFormat("F64", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_AMOUNT, " +
                             "       " + cmnService.J_SQLDBFormat("F65", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_RATE, " +
                             "       " + cmnService.J_SQLDBFormat("F66", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_TAX, " +
                             "       " + cmnService.J_SQLDBFormat("F67", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "    AS SUPER_ANN_INCOME " +
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
                             "       " + cmnService.J_SQLDBFormat("F7", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TOTAL_AMOUNT " +
                             "FROM [" + strImporttableName + "] WHERE F2 = 'C6A'";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage(strImportErrorMessage);
                        return false;
                    }

                    #endregion

                    #region Insert data to COR_HDR_SALARY_DETAILS_3CD

                    string[,] strINVALID_PAN_1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INVALID_PAN = 'N'" , "F", "1", "F"},
                                                  {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".INVALID_PAN = 'Y'" , "F", "0", "F"}};

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

                    strSQL = "INSERT INTO COR_HDR_SALARY_DETAILS_3CD (BATCH_HEADER_ID," +
                             "                                    SL_NO," +
                             "                                    MODE," +
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
                             "                                    CVIA_SEC80CCG_DED_AMOUNT," +
                             "                                    CVIA_OTH_DED_TOTAL," +
                             "                                    US_16_EA," +
                             "                                    US_16_TE," +
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
                             "                                    LENDER_4_NAME) " +
                             "SELECT " + lngBatchID + "     AS BATCH_HEADER_ID," +
                             "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO  AS SL_NO," +
                             "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".MODE   AS MODE," +
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
                             "       " + cmnService.J_SQLDBFormat("F_80CCG.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS CVIA_SEC80CCG_DED_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat("F_OTHERS.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CVIA_OTH_DED_TOTAL," +
                             "       " + cmnService.J_SQLDBFormat("F_16ii.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "   AS US_16_EA," +
                             "       " + cmnService.J_SQLDBFormat("F_16iii.TOTAL_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "  AS US_16_TE," +
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
                             "       " + cmnService.J_SQLDBFormat("LENDER_4_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS LENDER_4_NAME " +
                             "FROM ((((((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + " " +
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
                             "        TOTAL_AMOUNT " +
                             "FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_C6A + " " +
                             "WHERE SECTION_ID = 'OTHERS') AS F_OTHERS " +
                             "ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SD + ".SL_NO = F_OTHERS.SD_SL_NO) " +
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

                    #endregion
                }

                #endregion
                //-- 2017/10/30
                #region ENTER REGULAR DEDUCTEE/EMPLOYEE MASTER
                //--
                #region COMMENT
                //                if (strFormNo != T_FormNo.F24Q) //-- ELSE THAN 24Q
                //                {
                //                    strSQL = @"SELECT COUNT(*)
                //                               FROM   COR_TRN_COMPANY_3CD INNER JOIN MST_COMPANY
                //                               ON     COR_TRN_COMPANY_3CD.TAN_NO = MST_COMPANY.TAN_NO
                //                               WHERE  COR_TRN_COMPANY_3CD.BATCH_HEADER_ID = " + lngBatchID;
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
                //                               FROM   COR_TRN_COMPANY_3CD INNER JOIN MST_COMPANY
                //                               ON     COR_TRN_COMPANY_3CD.TAN_NO = MST_COMPANY.TAN_NO
                //                               WHERE  COR_TRN_COMPANY_3CD.BATCH_HEADER_ID = " + lngBatchID;
                //                    if (cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                //                    {
                //                        string strCompanyID = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_ID FROM COR_TRN_COMPANY_3CD INNER JOIN MST_COMPANY ON COR_TRN_COMPANY_3CD.TAN_NO = MST_COMPANY.TAN_NO WHERE COR_TRN_COMPANY_3CD.BATCH_HEADER_ID = " + lngBatchID)) ;
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
                //                                SELECT DISTINCT COR_TRN_SALARY_DETAILS_3CD.EMPLOYEE_PAN,
                //                                       COR_TRN_SALARY_DETAILS_3CD.EMPLOYEE_NAME," +
                //                                           @" " + strCompanyID + @"
                //                                FROM  COR_TRN_SALARY_DETAILS_3CD LEFT JOIN 
                //                                      MST_EMPLOYEE
                //                                ON    MST_EMPLOYEE.EMPLOYEE_PAN = COR_TRN_SALARY_DETAILS_3CD.EMPLOYEE_PAN
                //                                WHERE MST_EMPLOYEE.EMPLOYEE_PAN IS NULL
                //                                AND   COR_TRN_SALARY_DETAILS_3CD.BATCH_HEADER_ID = " + lngBatchID + @"           
                //                                ORDER BY COR_TRN_SALARY_DETAILS_3CD.EMPLOYEE_PAN,
                //                                      COR_TRN_SALARY_DETAILS_3CD.EMPLOYEE_NAME";
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
                #endregion
                //
                #region CHANGE MODE TO '' WHERE 'A' 2017/02/24
                //-- UPDATE MODE TO '' WHERE 'A'
                strSQL = "UPDATE COR_HDR_SALARY_DETAILS_3CD " +
                         "SET    MODE = '' " +
                         "WHERE  MODE = 'A'  " +
                         "AND BATCH_HEADER_ID = " + lngBatchID + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //-- UPDATE MODE TO '' WHERE 'A'
                //strSQL = "UPDATE COR_TRN_SALARY_DETAILS_3CD " +
                //         "SET    MODE = '' " +
                //         "WHERE  MODE = 'A'  " +
                //         "AND BATCH_HEADER_ID = " + lngBatchID + "";

                //if (dmlService.J_ExecSql(strSQL) == false)
                //{
                //    dmlService.J_Rollback();
                //    cmnService.J_UserMessage(strImportErrorMessage);
                //    return false;
                //}
                //--
                #endregion
                //MessageBox.Show("1.6");
                #region VERIFYING RECORD COUNTS

                strSQL = "SELECT COUNT(*) AS CHALLAN_MATCHED " +
                         "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ", " +
                         "    (SELECT COUNT(COR_HDR_CHALLAN_3CD.HDR_CHALLAN_ID) AS CHALLAN_IMPORTED " +
                         "     FROM   COR_HDR_CHALLAN_3CD " +
                         "     WHERE  BATCH_HEADER_ID          = " + lngBatchID + ") AS CHALLAN_CNT " +
                         "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_BH + ".CHALLAN_RECORDS_COUNT = CHALLAN_CNT.CHALLAN_IMPORTED";

                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) == 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }

                strSQL = "SELECT COUNT(*) AS DEDUCTEE_NOT_MATCHED " +
                         "FROM ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + " INNER JOIN COR_HDR_CHALLAN_3CD " +
                         "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".SL_NO = COR_HDR_CHALLAN_3CD.SL_NO) " +
                         "       LEFT JOIN " +
                         "      (SELECT HDR_CHALLAN_ID, " +
                         "              COUNT(HDR_DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_IMPORTED " +
                         "       FROM   COR_HDR_DEDUCTEE_DETAILS_3CD " +
                         "       GROUP BY HDR_CHALLAN_ID) AS DEDUCTEE " +
                         "       ON COR_HDR_CHALLAN_3CD.HDR_CHALLAN_ID = DEDUCTEE.HDR_CHALLAN_ID) " +
                         "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CD + ".DEDUCTEE_COUNT <> " + cmnService.J_SQLDBFormat("DEDUCTEE.COUNT_DEDUCTEE_IMPORTED", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " " +
                         "AND    COR_HDR_CHALLAN_3CD.BATCH_HEADER_ID   = " + lngBatchID;

                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //prgBar.Value = prgBar.Value + 5;

                #endregion
                //MessageBox.Show("1.7");
                #region VERIFYING NULL RECORDS

                strSQL = "SELECT COUNT(*) " +
                    "     FROM   COR_HDR_CHALLAN_3CD " +
                    "     WHERE  BATCH_HEADER_ID = " + lngBatchID + " " +
                    "     AND    TOT_TAX IS NULL";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //--
                strSQL = "SELECT COUNT(*) " +
                    "     FROM   COR_HDR_DEDUCTEE_DETAILS_3CD " +
                    "     WHERE  BATCH_HEADER_ID = " + lngBatchID + " " +
                    "     AND    TOTAL_AMOUNT IS NULL";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage(strImportErrorMessage);
                    return false;
                }
                //-- UPDATE
                strSQL = "SELECT COUNT(*) " +
                    "     FROM   COR_HDR_CHALLAN_3CD " +
                    "     WHERE  BATCH_HEADER_ID = " + lngBatchID + " " +
                    "     AND    SECTION_ID IS NULL";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    blnNullSectionFound = true;
                    //--
                    strSQL = "UPDATE COR_HDR_CHALLAN_3CD SET SECTION_ID = 0 WHERE BATCH_HEADER_ID = " + lngBatchID + " AND SECTION_ID IS NULL";
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        dmlService.J_Rollback();
                        return false;
                    }
                    //--
                    //strSQL = "UPDATE COR_TRN_CHALLAN_3CD SET SECTION_ID = 0 WHERE BATCH_HEADER_ID = " + lngBatchID + " AND SECTION_ID IS NULL";
                    //if (dmlService.J_ExecSql(strSQL) == false)
                    //{
                    //    dmlService.J_Rollback();
                    //    return false;
                    //}
                }
                prgBar.Value = prgBar.Value + 5;

                #endregion
                //MessageBox.Show("1.8");
                #region VERIFYING TRANSFER VOUCHER FOR BOOK ENTRY

                strSQL = "SELECT COUNT(*) AS COUNT_RECORDS " +
                    "     FROM   COR_HDR_CHALLAN_3CD " +
                    "     WHERE  BOOK_ENTRY          = 1 " +
                    "     AND    TRANSFER_VOUCHER_NO = '' " +
                    "     AND    BATCH_HEADER_ID     = " + lngBatchID + " ";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    strSQL = "UPDATE COR_HDR_CHALLAN_3CD SET TRANSFER_VOUCHER_NO = CHALLAN_NO WHERE BOOK_ENTRY = 1 AND TRANSFER_VOUCHER_NO = '' AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "UPDATE COR_HDR_CHALLAN_3CD SET CHALLAN_NO = '' WHERE BOOK_ENTRY = 1 AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    dmlService.J_ExecSql(strSQL);
                    //
                    //strSQL = "UPDATE COR_TRN_CHALLAN_3CD SET TRANSFER_VOUCHER_NO = CHALLAN_NO WHERE BOOK_ENTRY = 1 AND TRANSFER_VOUCHER_NO = '' AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    //dmlService.J_ExecSql(strSQL);
                    ////
                    //strSQL = "UPDATE COR_TRN_CHALLAN_3CD SET CHALLAN_NO = '' WHERE BOOK_ENTRY = 1 AND BATCH_HEADER_ID = " + lngBatchID + " ";
                    //dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //MessageBox.Show("1.9");
                #region UPDATING PAN_UPDATION_INDICATOR TO '0' //-- 2016/05/28
                //
                //strSQL = "UPDATE COR_TRN_SALARY_DETAILS_3CD SET PAN_UPDATION_INDICATOR = 0 WHERE BATCH_HEADER_ID = " + lngBatchID + " ";
                //dmlService.J_ExecSql(strSQL);
                //                    
                #endregion
                //MessageBox.Show("1.10");
                #region RE CALCULATING CTRL_TOT_TAX BASED ON PENDING AMOUNT

                // Added by Shrey Kejriwal on 25/09/2014

                // Checking whether there is difference in pending amount calculated on the basis of amount
                // and pending amount specified in file

                // And if found updating the ctrl_tot_tax field to show the pending amount difference


                //Difference calculation on the basis on book entry value
                //string[,] strLoadChallanTotTaxMatrix = {{"COR_TRN_CHALLAN_3CD.BOOK_ENTRY =  1", "F", "COR_TRN_CHALLAN_3CD.TOT_TAX - COR_TRN_CHALLAN_3CD.INTEREST_ALLOCATED - COR_TRN_CHALLAN_3CD.OTHERS_ALLOCATED", "F"},
                //                                    {"COR_TRN_CHALLAN_3CD.BOOK_ENTRY <> 1", "F", "COR_TRN_CHALLAN_3CD.TOT_TAX - COR_TRN_CHALLAN_3CD.INTEREST_ALLOCATED - COR_TRN_CHALLAN_3CD.OTHERS_ALLOCATED - COR_TRN_CHALLAN_3CD.LATE_FEE", "F"}};

                //strSQL = "SELECT COUNT(*) " + 
                //         "FROM COR_TRN_CHALLAN_3CD " +
                //         "WHERE  " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " - CTRL_TOT_TAX <> PENDING_AMOUNT " +
                //         "AND    CHALLAN_STATUS = 'M' " +
                //         "AND    PENDING_AMOUNT <> 0 " + //-- 2014/10/09
                //         "AND    BOOK_ENTRY = 0 " + //-- 2014/10/09
                //         "AND    BATCH_HEADER_ID = " + lngBatchID;

                //int intCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                //if (intCount > 0)
                //{

                //    strSQL = "UPDATE COR_TRN_CHALLAN_3CD " +
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
                int intChallan = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM COR_HDR_CHALLAN_3CD WHERE BATCH_HEADER_ID = " + lngBatchID));
                int intDeductee = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM COR_HDR_DEDUCTEE_DETAILS_3CD WHERE BATCH_HEADER_ID = " + lngBatchID));
                int intSalary = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM COR_HDR_SALARY_DETAILS_3CD WHERE BATCH_HEADER_ID = " + lngBatchID));
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
                                     Col69=F69 Char");

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
                //--
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
                            " + cmnService.J_GetDataType("AIN", J_ColumnType.String, 7) + @")";

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
                            " + cmnService.J_GetDataType("MINOR_CODE", J_ColumnType.String, 15) + @")";

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
                            "" + cmnService.J_GetDataType("DEDUCTEE_TAX_ID", J_ColumnType.String, 25) + ")"; //-- 2015/06/02 ANIK.G.

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
                            " + cmnService.J_GetDataType("SUPER_ANN_INCOME", J_ColumnType.Double) + @")";

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
                            " + cmnService.J_GetDataType("TOTAL_AMOUNT", J_ColumnType.Double) + @")";

                dmlService.J_ExecSql(strSQL);
                #endregion

                #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + " //-- 2016/03/30
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_COR_DEDUCTEE_TOT_TAX + "";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //--
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

        //-- Added By Abhishek Dey On 23/02/2018 --
        #region CREATE EXCEL FILE
        // SOURCE PATH : http://csharp.net-informations.com/excel/csharp-create-excel.htm
        private bool CREATE_EXCEL_FILE(string ExcelFilePath)
        {
            try
            {
                //--
                //-- MessageBox.Show("5.0.1.1");
                Microsoft.Office.Interop.Excel.Application xlApp;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                //Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                //--
                //-- MessageBox.Show("5.0.1.2");
                //string strExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "." + cmbFileType.Text;
                //--

                //xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);

                //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                //xlWorkSheet.Cells[1, 1] = "http://csharp.net-informations.com";
                //--
                //------------------------------------
                if (Path.GetExtension(ExcelFilePath).ToUpper() == ".XLS")
                    xlWorkBook.SaveAs(ExcelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                else if (Path.GetExtension(ExcelFilePath).ToUpper() == ".XLSX")
                    xlWorkBook.SaveAs(ExcelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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

        #region ExportToExcelFromDataTable
        private bool ExportToExcelFromDataTable(DataTable myDataTable, string SheetName)
        {
            //-- 20/02/2018 --
            //GC.Collect();
            GC.WaitForPendingFinalizers();
            //----------------
            //
            try
            {
                //myDataSet = new DataSet();
                //myDataSet = dmlService.J_ExecSqlReturnDataSet(strSQL);
                //
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbooks workbook = app.Workbooks;
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

                foreach (DataColumn dc in myDataTable.Columns)
                {
                    colIndex++;
                    wsnew.Cells[1, colIndex] = dc.ColumnName;
                }
                //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 -
                foreach (DataRow dr in myDataTable.Rows)
                {
                    rowIndex++;
                    colIndex = 0;

                    foreach (DataColumn dc in myDataTable.Columns)
                    {
                        colIndex++;
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
                //--
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
            return true;
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
                myDataSet = new DataSet();
                myDataSet = dmlService.J_ExecSqlReturnDataSet(strSQL);
                //
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                Microsoft.Office.Interop.Excel.Workbooks workbook = app.Workbooks;
                //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 -
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
                        //--
                        //if (dr[dc.ColumnName].ToString().Length == 10 && dr[dc.ColumnName].ToString().Contains("/") == true)
                        if (dr[dc.ColumnName].ToString().Length == 10
                            && (cmnService.J_Mid(dr[dc.ColumnName].ToString(), 2, 1) == "/" || cmnService.J_Mid(dr[dc.ColumnName].ToString(), 2, 1) == "-")
                            && (cmnService.J_Mid(dr[dc.ColumnName].ToString(), 5, 1) == "/" || cmnService.J_Mid(dr[dc.ColumnName].ToString(), 5, 1) == "-"))
                            wsnew.Cells[rowIndex, colIndex] = "'" + dr[dc.ColumnName];
                        else
                            wsnew.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                    }
                }
                //--
                //for (int i = 0; i < ds.table[1].Rows.Count; i++)
                //{
                //    ds.table[0].Row[i]["NewConverteddate"] = ds.table[0].Row[i]["Yourdatecoloumn"].ToString("dd/MM/yyyy");
                //} 
                //--
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
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
            return true;
        }


        #endregion

        #region ExportToCSV
        protected bool ExportToCSV(string strSQL, string CSVFile)
        {
            try
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
                return true;
            }
            catch(Exception err)
            {
                return false;
            }
        }
        #endregion

        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0070", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region Trn3CDReport_Activated
        private void Trn3CDReport_Activated(object sender, EventArgs e)
        {
            blResize = false;
        }
        #endregion

    }
}
