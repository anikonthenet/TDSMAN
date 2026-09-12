#region Referred Namespaces
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
using System.Text;
//~~~~ User Namespaces ~~~~
using TDSMAN;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormTrn;
using TDSMAN.FormSys;
using TDSMAN.FormMst;
using TDSMAN.FormPar;
using TDSMAN.FormUtl;
//------------
using System.Reflection;

using Microsoft.VisualBasic.Compatibility.VB6;
#endregion

namespace TDSMAN.FormTrn
{
    #region T_SortOrderReturnsUnderProcess
    public struct T_SortOrderReturnsUnderProcess
    {
        public const string Last_Worked_On = "Last Worked On";
        public const string Latest_Return = "Latest Return";
        public const string Filing_Due_Date = "Filing Due Date";
    }
    #endregion

    public partial class TrnTabbedDashboard : Form
    {

        ResizeForm _form_resize;

        #region TrnTabbedDashboard
        public TrnTabbedDashboard()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        #region Objects & Variables declaration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();

        ToolTip tllTip = new ToolTip();

        mdiTDSMAN mdiTDSMAN = new mdiTDSMAN();

        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        string strSearchText = "-- Search here (min 3 chars) --";
        string strFilingStatusSearchText = "-- Search TAN - Name here (min 3 chars) --";
        //--
        string strSQL = "";
        //
        //long lngNoOfRecordsSQL = 5, lngNoOfReturnDays = 120;
        string strQuery = "", strQuarter = "", strCompanyName = ""; int intAsstId = 0;
        string strSortReturnsUnderProcess = "";
        bool blExit = true;
        //
        string strSQLReturnUnderProcess = "", strSQLReturnsReadyForFiling = "", strSQLFiledReturns = "", strSQLFilingStatus = "";
        bool blResize = true;
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

        #region TrnTabbedDashboard_Load
        private void TrnTabbedDashboard_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            //
            blExit = true;
            txtSearchHereReturnsUnderProcess.Text = strSearchText;
            txtSearchReturnsReadyForFiling.Text = strSearchText;
            txtSearchFiledReturns.Text = strSearchText;
            txtSearchFilingStatus.Text = strFilingStatusSearchText;
            blExit = false;
            //
            lblReturnsAccessedFooterMessage.Text = "[based on Returns accessed in the last " + TDSMAN.Classes.TDSMAN.T_RETURNS_UNDER_PROCESS_DAYS + " days]";
            lblReturnsGeneratedFooterMessage.Text = "[based on Returns generated in the last " + TDSMAN.Classes.TDSMAN.T_RETURNS_READY_FOR_FILING_DAYS+ " days]";
            lblReturnsFiledFooterMessage.Text = "[based on Returns filed in the last " + TDSMAN.Classes.TDSMAN.T_FILED_RETURNS_DAYS + " days]";
            //--
            dgvReturnsReadyForFiling.Select();
            //--
            lblReturnsUnderProcess_Click(sender, e);
            //--
            if (TDSMAN.Classes.TDSMAN.T_DEFAULT_HOME_DASHBOARD_FLAG == true)
                chkMakeDashboardDefaultHomeScreen.Checked = true;
            else
                chkMakeDashboardDefaultHomeScreen.Checked = false;
            //--
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      ORDER BY ASST_ID DESC";
            blExit = true;
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            blExit = false;
            //--
        }
        #endregion


        #region TrnTabbedDashboard_Activated
        private void TrnTabbedDashboard_Activated(object sender, EventArgs e)
        {
            //--
            //if (TdsMan.UpdateReturnLastDate() == false)
            //{ }
            if(bgBackGroundWorker.IsBusy==false)
                bgBackGroundWorker.RunWorkerAsync();
            //LoadReturnsUnderProcessFirstScreen("", Convert.ToInt32(txtNoOfRecords.Text), Convert.ToInt32(txtNoOfDays.Text));
            if (strSortReturnsUnderProcess == "")
            {
                strSortReturnsUnderProcess = T_SortOrderReturnsUnderProcess.Last_Worked_On;
                LoadReturnsUnderProcessFirstScreen("", strSortReturnsUnderProcess);
            }
            else
                LoadReturnsUnderProcessFirstScreen("", strSortReturnsUnderProcess);
            //--
            LoadReturnsReadyforFiling("");
            //--
            LoadFiledReturns("");
            //--
            LoadFilingStatus("");
            //btnFirstScreen.ForeColor = Color.Black;
            //btnSecondScreen.ForeColor = Color.LightGray;
            //--
        }
        #endregion


        #region dgvReturnsReadyForFiling_CellFormatting
        private void dgvReturnsReadyForFiling_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvReturnsReadyForFiling.Columns[e.ColumnIndex].DataPropertyName == "RETURN_TYPE")
            {
                if (dgvReturnsReadyForFiling.Rows[e.RowIndex].Cells["RETURN_TYPE"].Value.ToString().ToUpper() == "C")
                {
                    //e.CellStyle.BackColor = Color.Khaki;
                    //e.CellStyle.ForeColor = Color.Black;
                    //
                    DataGridViewCell cell = this.dgvReturnsReadyForFiling.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "C-Correction Return";
                }
                else
                {
                    //
                    DataGridViewCell cell = this.dgvReturnsReadyForFiling.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "R-Regular Return";
                }
            }
            else if (dgvReturnsReadyForFiling.Columns[e.ColumnIndex].DataPropertyName == "UPDATE_RECEIPT_NO")
            {
                e.CellStyle.ForeColor = Color.Blue;
            }
            else if (dgvReturnsReadyForFiling.Columns[e.ColumnIndex].DataPropertyName == "")
            {
                DataGridViewCell cell = this.dgvReturnsReadyForFiling.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Download Filed Return";
            }
            //--
            if ((e.ColumnIndex == this.dgvReturnsReadyForFiling.Columns["UPDATE_RECEIPT_NO"].Index) && e.Value != null)
            {
                DataGridViewCell cell = this.dgvReturnsReadyForFiling.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Click to update Receipt No.";
                #region COMMENT
                //if (e.Value.Equals("*"))
                //{
                //    cell.ToolTipText = "very bad";
                //}
                //else if (e.Value.Equals("**"))
                //{
                //    cell.ToolTipText = "bad";
                //}
                //else if (e.Value.Equals("***"))
                //{
                //    cell.ToolTipText = "good";
                //}
                //else if (e.Value.Equals("****"))
                //{
                //    cell.ToolTipText = "very good";
                //}
                #endregion
            }
            //else if ((e.ColumnIndex == this.dgvReturnsReadyForFiling.Columns["BtnDownloadReturn"].Index) && e.Value != null)
            //{
            //    DataGridViewCell cell = this.dgvReturnsReadyForFiling.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //    cell.ToolTipText = "Download Filed Return";
            //}
            //--

            //--
            if (dgvReturnsReadyForFiling.Columns[e.ColumnIndex].DataPropertyName == "TOTAL_DEDUCTEES")
            {
                if (dgvReturnsReadyForFiling.Rows[e.RowIndex].Cells["TOTAL_DEDUCTEES"].Value.ToString().Contains("/") == true)
                {
                    DataGridViewCell cell = this.dgvReturnsReadyForFiling.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //cell.ToolTipText = "Employee(s) in Quarterly Return || Employee(s) in Salary Detail";
                    cell.ToolTipText = "Q4-Salary Details";
                }
            }
        }
        #endregion

        #region dgvReturnsUnderProcessFirstScreen_CellMouseEnter
        private void dgvReturnsUnderProcessFirstScreen_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                string colname = dgvReturnsUnderProcessFirstScreen.Columns[e.ColumnIndex].Name;
                if (colname != "BtnReturnHealth")
                {
                    dgvReturnsUnderProcessFirstScreen.Cursor = Cursors.Default;
                }
                else
                {
                    dgvReturnsUnderProcessFirstScreen.Cursor = Cursors.Hand;
                }
                //--
                //if (dgvReturnsUnderProcessFirstScreen.Columns[e.ColumnIndex].DataPropertyName == "RETURN_LAST_DATE")
                //{
                //    if (dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["RETURN_LAST_DATE"].Value.ToString() != "N.A.")
                //    {
                //        if (dtService.J_ConvertToIntYYYYMMDD(dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["RETURN_LAST_DATE"].Value.ToString()) < dtService.J_ConvertToIntYYYYMMDD(J_ReturnServerDate()))
                //        {
                //            DataGridViewCell cell = this.dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //            cell.ToolTipText = "Filing due date crossed !!";
                //        }
                //    }
                //}
            }
        }
        #endregion

        #region dgvReturnsReadyForFiling_DoubleClick
        private void dgvReturnsReadyForFiling_DoubleClick(object sender, EventArgs e)
        {
            if (dgvReturnsReadyForFiling.CurrentRow != null)
            {
                if (Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[1].Value) == "C")
                {
                    //-- 2021/12/03
                    TDSMAN.Classes.TDSMAN.T_pBatchId = Convert.ToInt64(Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[0].Value));
                    //
                    TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard = true;
                    //
                    cmnService.J_ShowChildForm(new Trn26QCorrectionReturn(), J_Var.frmMain, "Make Corrections");
                }
                else
                {
                    TDSMAN.Classes.TDSMAN.T_pTabFormCaption = Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[6].Value);
                    //--
                    #region ADD/GET BOOKMARK
                    if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
                    {
                        strQuery = @"FORM_NAME= '" + TDSMAN.Classes.TDSMAN.T_pTabFormCaption + "' ";
                        if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL", strQuery) == true)
                        {
                            //11 12
                            //Inserting the Form BookMarkDetail [T_TransactionMode.UPDATE]
                            intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.UPDATE.ToString(),
                                                                Convert.ToInt32(Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[15].Value)),
                                                                Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[5].Value),
                                                                TDSMAN.Classes.TDSMAN.T_pTabFormCaption,
                                                                Convert.ToInt32(Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[14].Value)),
                                                                TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                                                                out strQuarter,
                                                                out strCompanyName
                                                               );
                        }
                        else
                        {
                            //Inserting the Form BookMarkDetail [T_TransactionMode.INSERT]
                            intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.INSERT.ToString(),
                                                                Convert.ToInt32(Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[15].Value)),
                                                                Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[5].Value),
                                                                TDSMAN.Classes.TDSMAN.T_pTabFormCaption,
                                                                Convert.ToInt32(Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[14].Value)),
                                                                TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                                                                out strQuarter,
                                                                out strCompanyName
                                                               );
                        }
                    }
                    #endregion
                    //--
                    //--
                    if (Convert.ToInt32(Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[15].Value)) >= T_FinancialYearID.F2026_27ID)
                    {
                        TdsMan.CloseChildForm(new TrnRegularReturn_26_27(0), this);
                        //--
                        cmnService.J_ShowChildForm(new TrnRegularReturn_26_27(0), J_Var.frmMain, "Form " + TDSMAN.Classes.TDSMAN.T_pTabFormCaption);
                    }
                    else
                    {
                        TdsMan.CloseChildForm(new TrnRegularReturn(0), this);
                        //--
                        cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form " + TDSMAN.Classes.TDSMAN.T_pTabFormCaption);
                    }
                }
            }

        }
        #endregion

        #region dgvReturnsReadyForFiling_CellMouseEnter
        private void dgvReturnsReadyForFiling_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                string colname = dgvReturnsReadyForFiling.Columns[e.ColumnIndex].Name;
                if (colname != "BtnDownloadReturn" && colname != "UPDATE_RECEIPT_NO")
                {
                    dgvReturnsReadyForFiling.Cursor = Cursors.Default;
                }
                else
                {
                    dgvReturnsReadyForFiling.Cursor = Cursors.Hand;
                }
                //
                //if(colname == "UPDATE_RECEIPT_NO")
                //{
                //    ToolTip info = new ToolTip();
                //    info.SetToolTip(dgvReturnsReadyForFiling.Columns[e.ColumnIndex], "Update Receipt No.");
                //}
            }
        }
        #endregion

        #region dgvReturnsUnderProcessFirstScreen_CellClick
        private void dgvReturnsUnderProcessFirstScreen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //  dgvReturnsUnderProcessFirstScreen.Columns[e.ColumnIndex].DisplayIndex
            //if (dgvReturnsUnderProcessFirstScreen.Columns[e.ColumnIndex].Name.ToString() == "BtnReturnHealth")// "0")// || e.ColumnIndex.ToString() == "11") //-- PREDICT DEFAULTS
            if (e.ColumnIndex.ToString() == "19")
            {
                if (Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[1].Value) == "C")
                {
                    this.Cursor = Cursors.WaitCursor;
                    //--
                    TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId = 0;
                    //
                    //TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId = cmnService.J_ReturnInt64Value(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[2].Value));
                    TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId = cmnService.J_ReturnInt64Value(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[0].Value));
                    TDSMAN.Classes.TDSMAN.T_FromModule = T_OTHERMODULENAME.MAKE_CORRECTION;
                    //--
                    #region ADD/GET BOOKMARK
                    //if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
                    //{
                    //    strQuery = @"FORM_NAME= '" + strFormNo + "' ";
                    //    if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL", strQuery) == true)
                    //    {
                    //        //Inserting the Form BookMarkDetail [T_TransactionMode.UPDATE]
                    //        intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.UPDATE.ToString(),
                    //                                            Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    //                                            cmbQuarter.Text,
                    //                                            strFormNo,
                    //                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                    //                                            TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                    //                                            out strQuarter,
                    //                                            out strCompanyName
                    //                                           );
                    //    }
                    //    else
                    //    {
                    //        //Inserting the Form BookMarkDetail [T_TransactionMode.INSERT]
                    //        intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.INSERT.ToString(),
                    //                                            Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    //                                            cmbQuarter.Text,
                    //                                            strFormNo,
                    //                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                    //                                            TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                    //                                            out strQuarter,
                    //                                            out strCompanyName
                    //                                           );
                    //    }
                    //}
                    #endregion
                    //TrnHealthCheckupRegCorr TrnHealthCheckupRegCorr = new TrnHealthCheckupRegCorr();
                    //TrnHealthCheckupRegCorr.Show();
                    //-- 2021/11/26
                    TdsMan.T_Insert_Update_Access_Datetime(dmlService.J_pCommand, TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId, false);
                    //
                    cmnService.J_ShowChildForm(new TrnPredictDefaultsCorr(), J_Var.frmMain, "Predict Defaults");
                    //--
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;
                    //--
                    TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId = 0;
                    //
                    TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId = cmnService.J_ReturnInt64Value(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[0].Value));
                    TDSMAN.Classes.TDSMAN.T_FromModule = T_OTHERMODULENAME.REGULAR_FORM;
                    //--
                    #region ADD/GET BOOKMARK
                    //if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
                    //{
                    //    strQuery = @"FORM_NAME= '" + strFormNo + "' ";
                    //    if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL", strQuery) == true)
                    //    {
                    //        //Inserting the Form BookMarkDetail [T_TransactionMode.UPDATE]
                    //        intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.UPDATE.ToString(),
                    //                                            Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    //                                            cmbQuarter.Text,
                    //                                            strFormNo,
                    //                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                    //                                            TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                    //                                            out strQuarter,
                    //                                            out strCompanyName
                    //                                           );
                    //    }
                    //    else
                    //    {
                    //        //Inserting the Form BookMarkDetail [T_TransactionMode.INSERT]
                    //        intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.INSERT.ToString(),
                    //                                            Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    //                                            cmbQuarter.Text,
                    //                                            strFormNo,
                    //                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                    //                                            TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                    //                                            out strQuarter,
                    //                                            out strCompanyName
                    //                                           );
                    //    }
                    //}
                    #endregion
                    //TrnHealthCheckupRegCorr TrnHealthCheckupRegCorr = new TrnHealthCheckupRegCorr();
                    //TrnHealthCheckupRegCorr.Show();
                    //-- 2021/11/26
                    TdsMan.T_Insert_Update_Access_Datetime(dmlService.J_pCommand, TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId, false);
                    //
                    cmnService.J_ShowChildForm(new TrnPredictDefaultsCorr(), J_Var.frmMain, "Predict Defaults");
                    //--
                    this.Cursor = Cursors.Default;
                }
            }
            //else if (e.ColumnIndex.ToString() == "12" )//|| e.ColumnIndex.ToString() == "12") //-- GOTO RETURN
            //{
            //    if (Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[1].Value) == "CORR")
            //    {
            //        cmnService.J_ShowChildForm(new Trn26QCorrectionReturn(), J_Var.frmMain, "Make Corrections");
            //    }
            //    else
            //    {
            //        TDSMAN.Classes.TDSMAN.T_pTabFormCaption = Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[6].Value);
            //        //--
            //        #region ADD/GET BOOKMARK
            //        if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
            //        {
            //            strQuery = @"FORM_NAME= '" + TDSMAN.Classes.TDSMAN.T_pTabFormCaption + "' ";
            //            if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL", strQuery) == true)
            //            {
            //                //11 12
            //                //Inserting the Form BookMarkDetail [T_TransactionMode.UPDATE]
            //                intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.UPDATE.ToString(),
            //                                                    Convert.ToInt32(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[9].Value)),
            //                                                    Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[5].Value),
            //                                                    TDSMAN.Classes.TDSMAN.T_pTabFormCaption,
            //                                                    Convert.ToInt32(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[10].Value)),
            //                                                    TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
            //                                                    out strQuarter,
            //                                                    out strCompanyName
            //                                                   );
            //            }
            //            else
            //            {
            //                //Inserting the Form BookMarkDetail [T_TransactionMode.INSERT]
            //                intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.INSERT.ToString(),
            //                                                    Convert.ToInt32(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[9].Value)),
            //                                                    Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[5].Value),
            //                                                    TDSMAN.Classes.TDSMAN.T_pTabFormCaption,
            //                                                    Convert.ToInt32(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[10].Value)),
            //                                                    TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
            //                                                    out strQuarter,
            //                                                    out strCompanyName
            //                                                   );
            //            }
            //        }
            //        #endregion
            //        //--
            //        TdsMan.CloseChildForm(new TrnRegularReturn(0), this);
            //        //--
            //        cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form " + TDSMAN.Classes.TDSMAN.T_pTabFormCaption);
            //    }
            //}
        }
        #endregion

        #region dgvReturnsUnderProcessFirstScreen_CellFormatting
        private void dgvReturnsUnderProcessFirstScreen_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvReturnsUnderProcessFirstScreen.Columns[e.ColumnIndex].DataPropertyName == "RETURN_TYPE")
            {
                if (dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["RETURN_TYPE"].Value.ToString().ToUpper() == "C")
                {
                    //e.CellStyle.BackColor = Color.Khaki;
                    //e.CellStyle.ForeColor = Color.Black;
                    DataGridViewCell cell = this.dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "C-Correction Return";
                }
                else
                {
                    DataGridViewCell cell = this.dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "R-Regular Return";
                }
            }
            //--
            if (dgvReturnsUnderProcessFirstScreen.Columns[e.ColumnIndex].DataPropertyName == "RETURN_LAST_DATE")
            {
                if (dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["RETURN_LAST_DATE"].Value.ToString() != "N.A.")
                {
                    if (dtService.J_ConvertToIntYYYYMMDD(dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["RETURN_LAST_DATE"].Value.ToString()) < dtService.J_ConvertToIntYYYYMMDD(J_ReturnServerDate()))
                    {
                        //e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.Red;
                        //
                        //DataGridViewCell cell = this.dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        //cell.ToolTipText = "Filing due date crossed !!";
                    }
                }
                else if (dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["RETURN_LAST_DATE"].Value.ToString() == "N.A.")
                {
                    DataGridViewCell cell = this.dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //cell.ToolTipText = "Correction Return has no due date";
                    cell.ToolTipText = "Not Applicable";
                }
            }
            //--
            if (dgvReturnsUnderProcessFirstScreen.Columns[e.ColumnIndex].DataPropertyName == "TOTAL_DEDUCTEES")
            {
                if (dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["TOTAL_DEDUCTEES"].Value.ToString().Contains("/") == true)
                {
                    DataGridViewCell cell = this.dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //cell.ToolTipText = "Employee(s) in Quarterly Return / Employee(s) in Salary Detail";
                    cell.ToolTipText = "Q4-Salary Details";
                }
            }
            //--
            if (dgvReturnsUnderProcessFirstScreen.Columns[e.ColumnIndex].DataPropertyName == "")
            {
                if (cmnService.J_ReturnDoubleValue(dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["LAST_UPDATE_DATETIME_COMPARE"].Value.ToString()) > cmnService.J_ReturnDoubleValue(dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["PREDICT_DEFAULTS_ACCESS_DATETIME"].Value.ToString()))
                {
                    e.CellStyle.BackColor = Color.LightGray;
                    //e.Value = "It is recommended to check for defaults. Click to proceed.";// "Check";
                    e.Value =  "Re-Check";
                }
                else if (dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["PREDICT_DEFAULTS_STATUS"].Value.ToString().ToUpper() == T_PREDICT_DEFAULTS_STATUS.HAS_DEFAULT_STATUS.ToString())
                {
                    e.CellStyle.BackColor = Color.DarkOrange;
                    //e.Value = "Last check showed default. Click to check current status.";// "Default";
                    e.Value =  "Default";
                    //e.g
                }
                else if (dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["PREDICT_DEFAULTS_STATUS"].Value.ToString().ToUpper() == T_PREDICT_DEFAULTS_STATUS.NO_DEFAULT_STATUS.ToString())
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    //e.Value = "As per last check, no defaults were detected.";// "No Defaults"
                    e.Value = "No Defaults";
                }
                else //if (dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["PREDICT_DEFAULTS_STATUS"].Value.ToString().ToUpper() == T_PREDICT_DEFAULTS_STATUS.RE_CHECK_STATUS.ToString())
                {
                    e.CellStyle.BackColor = Color.LightGray;
                    //e.Value = "Changes done after last status check. Click to re-check.";// "Check";
                    e.Value =  "Check";
                }
                //--
                #region TOOL-TIP
                if (cmnService.J_ReturnDoubleValue(dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["LAST_UPDATE_DATETIME_COMPARE"].Value.ToString()) > cmnService.J_ReturnDoubleValue(dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["PREDICT_DEFAULTS_ACCESS_DATETIME"].Value.ToString()))
                {
                    DataGridViewCell cell = this.dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Changes done after last status check. Click to re-check.";
                    //e.Value = "It is recommended to check for defaults. Click to proceed.";// "Check";
                }
                else if (dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["PREDICT_DEFAULTS_STATUS"].Value.ToString().ToUpper() == T_PREDICT_DEFAULTS_STATUS.HAS_DEFAULT_STATUS.ToString())
                {
                    DataGridViewCell cell = this.dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Last check showed default. Click to check current status.";
                    //e.Value = "Last check showed default. Click to check current status.";// "Default";
                    //e.g
                }
                else if (dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["PREDICT_DEFAULTS_STATUS"].Value.ToString().ToUpper() == T_PREDICT_DEFAULTS_STATUS.NO_DEFAULT_STATUS.ToString())
                {
                    DataGridViewCell cell = this.dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "As per last check, no defaults were detected.";
                }
                else// if (dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells["PREDICT_DEFAULTS_STATUS"].Value.ToString().ToUpper() == T_PREDICT_DEFAULTS_STATUS.RE_CHECK_STATUS.ToString())
                {
                    DataGridViewCell cell = this.dgvReturnsUnderProcessFirstScreen.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "It is recommended to check for defaults. Click to proceed.";
                }
                //--
                #endregion
            }
        }
        #endregion

        #region dgvReturnsReadyForFiling_CellClick
        private void dgvReturnsReadyForFiling_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.ToString() == "17")
            {
                if (Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[13].Value) != "")
                {
                    if (Directory.Exists(Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[13].Value)) == true)
                    {
                        Process.Start(Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[13].Value));
                    }
                    else
                    {
                        //cmnService.J_UserMessage("File Path not Found !!");
                        cmnService.J_UserMessage("The FVU file is missing in the specified path.\n For assistance, call the TDSMAN helpdesk.");
                        return;
                    }

                }
                else
                {
                    //cmnService.J_UserMessage("File Path not Found !!");
                    cmnService.J_UserMessage("The FVU file is missing in the specified path.\n For assistance, call the TDSMAN helpdesk.");
                    return;
                }
            }
            else if (e.ColumnIndex.ToString() == "12")
            {
                if (dgvReturnsReadyForFiling.Rows[e.RowIndex].Cells["RETURN_TYPE"].Value.ToString().ToUpper() == "C")
                {
                    TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard = true;
                    TDSMAN.Classes.TDSMAN.T_pBatchId = Convert.ToInt64(Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[0].Value));
                    //
                    cmnService.J_ShowChildForm(new MstCorrReceiptNo(), J_Var.frmMain, "Correction Receipt No.");
                }
                else
                {
                    TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard = true;
                    TDSMAN.Classes.TDSMAN.T_pBasicInfoId = Convert.ToInt64(Convert.ToString(dgvReturnsReadyForFiling.Rows[dgvReturnsReadyForFiling.CurrentRow.Index].Cells[0].Value));
                    //
                    cmnService.J_ShowChildForm(new MstReceiptNo(), J_Var.frmMain, "Receipt No. Master");
                }
            }
        }
        #endregion

        #region dgvReturnsUnderProcessFirstScreen_DoubleClick
        private void dgvReturnsUnderProcessFirstScreen_DoubleClick(object sender, EventArgs e)
        {
            //-- Convert.ToInt64(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[0].Value)
            //if (e.ColumnIndex.ToString() == "12")//|| e.ColumnIndex.ToString() == "12") //-- GOTO RETURN
            //{
            if (dgvReturnsUnderProcessFirstScreen.CurrentRow != null)
            {
                if (Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[1].Value) == "C")
                {
                    //-- 2021/12/03
                    TDSMAN.Classes.TDSMAN.T_pBatchId = Convert.ToInt64(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[0].Value));
                    //
                    TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard = true;
                    //
                    cmnService.J_ShowChildForm(new Trn26QCorrectionReturn(), J_Var.frmMain, "Make Corrections");
                }
                else
                {
                    TDSMAN.Classes.TDSMAN.T_pTabFormCaption = Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[6].Value);
                    //--
                    #region ADD/GET BOOKMARK
                    if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
                    {
                        strQuery = @"FORM_NAME= '" + TDSMAN.Classes.TDSMAN.T_pTabFormCaption + "' ";
                        if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL", strQuery) == true)
                        {
                            //11 12
                            //Inserting the Form BookMarkDetail [T_TransactionMode.UPDATE]
                            intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.UPDATE.ToString(),
                                                                Convert.ToInt32(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[13].Value)),
                                                                Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[5].Value),
                                                                TDSMAN.Classes.TDSMAN.T_pTabFormCaption,
                                                                Convert.ToInt32(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[14].Value)),
                                                                TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                                                                out strQuarter,
                                                                out strCompanyName
                                                               );
                        }
                        else
                        {
                            //Inserting the Form BookMarkDetail [T_TransactionMode.INSERT]
                            intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.INSERT.ToString(),
                                                                Convert.ToInt32(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[13].Value)),
                                                                Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[5].Value),
                                                                TDSMAN.Classes.TDSMAN.T_pTabFormCaption,
                                                                Convert.ToInt32(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[14].Value)),
                                                                TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                                                                out strQuarter,
                                                                out strCompanyName
                                                               );
                        }
                    }
                    #endregion
                    //--
                    if (Convert.ToInt32(Convert.ToString(dgvReturnsUnderProcessFirstScreen.Rows[dgvReturnsUnderProcessFirstScreen.CurrentRow.Index].Cells[13].Value)) >= T_FinancialYearID.F2026_27ID)
                    {
                        TdsMan.CloseChildForm(new TrnRegularReturn_26_27(0), this);
                        //--
                        cmnService.J_ShowChildForm(new TrnRegularReturn_26_27(0), J_Var.frmMain, "Form " + TDSMAN.Classes.TDSMAN.T_pTabFormCaption);
                    }
                    else
                    {
                        TdsMan.CloseChildForm(new TrnRegularReturn(0), this);
                        //--
                        cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form " + TDSMAN.Classes.TDSMAN.T_pTabFormCaption);
                    }
                }
                //}
            }
        }
        #endregion        

        #region dgvReturnsUnderProcessFirstScreen_KeyPress
        private void dgvReturnsUnderProcessFirstScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                dgvReturnsUnderProcessFirstScreen_DoubleClick(sender, e);
            }
        }
        #endregion



        #region txtSearchFiledReturns_TextChanged
        private void txtSearchFiledReturns_TextChanged(object sender, EventArgs e)
        {
            if (blExit == true) return;
            //
            if (dgvFiledReturns.Visible == true)
            {
                if (txtSearchFiledReturns.Text.Trim() == "" || txtSearchFiledReturns.Text.Trim() == strSearchText)
                    LoadFiledReturns("");
                else if (txtSearchFiledReturns.Text.Length >= 3)
                    LoadFiledReturns(txtSearchFiledReturns.Text.Trim());
            }
        }
        #endregion

        #region txtSearchHereReturnsUnderProcess_TextChanged
        private void txtSearchHereReturnsUnderProcess_TextChanged(object sender, EventArgs e)
        {
            if (dgvReturnsUnderProcessFirstScreen.Visible == true)
            {
                if (txtSearchHereReturnsUnderProcess.Text.Trim() == "" || txtSearchHereReturnsUnderProcess.Text.Trim() == strSearchText)
                    LoadReturnsUnderProcessFirstScreen("", strSortReturnsUnderProcess);//, Convert.ToInt32(txtNoOfRecords.Text), Convert.ToInt32(txtNoOfDays.Text));
                else if(txtSearchHereReturnsUnderProcess.Text.Length >=3)
                    LoadReturnsUnderProcessFirstScreen(txtSearchHereReturnsUnderProcess.Text.Trim(), strSortReturnsUnderProcess);//, Convert.ToInt32(txtNoOfRecords.Text), Convert.ToInt32(txtNoOfDays.Text));
            }
            //else if (dgvReturnsUnderProcessSecondScreen.Visible == true)
            //{
            //    if (txtSearchHereReturnsUnderProcess.Text.Trim() == "" || txtSearchHereReturnsUnderProcess.Text.Trim() == strSearchText)
            //        LoadReturnsUnderProcessSecondScreen("", Convert.ToInt32(txtNoOfRecords.Text), Convert.ToInt32(txtNoOfDays.Text));
            //    else
            //        LoadReturnsUnderProcessSecondScreen(txtSearchHereReturnsUnderProcess.Text.Trim(), Convert.ToInt32(txtNoOfRecords.Text), Convert.ToInt32(txtNoOfDays.Text));
            //}
        }
        #endregion


        #region txtSearchHereReturnsUnderProcess_TextChanged
        private void txtSearchReturnsReadyForFiling_TextChanged(object sender, EventArgs e)
        {
            if (blExit == true) return;
            //
            if (txtSearchReturnsReadyForFiling.Text.Trim() == "" || txtSearchReturnsReadyForFiling.Text.Trim() == strSearchText)
                LoadReturnsReadyforFiling("");
            else if (txtSearchReturnsReadyForFiling.Text.Length >= 3)
                LoadReturnsReadyforFiling(txtSearchReturnsReadyForFiling.Text.Trim());
        }
        #endregion



        #region txtSearchHereReturnsUnderProcess_Enter
        private void txtSearchHereReturnsUnderProcess_Enter(object sender, EventArgs e)
        {
            txtSearchHereReturnsUnderProcess.Text = "";
            txtSearchHereReturnsUnderProcess.ForeColor = Color.Black;
            txtSearchHereReturnsUnderProcess.BackColor = Color.White;
        }
        #endregion

        #region txtSearchHereReturnsUnderProcess_Leave
        private void txtSearchHereReturnsUnderProcess_Leave(object sender, EventArgs e)
        {
            if (txtSearchHereReturnsUnderProcess.Text.Trim() == "")
            {
                txtSearchHereReturnsUnderProcess.Text = strSearchText;
                txtSearchHereReturnsUnderProcess.BackColor = Color.Azure;
                txtSearchHereReturnsUnderProcess.ForeColor = Color.SlateGray;
            }
        }
        #endregion

        #region txtSearchReturnsReadyForFiling_Enter
        private void txtSearchReturnsReadyForFiling_Enter(object sender, EventArgs e)
        {
            txtSearchReturnsReadyForFiling.Text = "";
            txtSearchReturnsReadyForFiling.BackColor = Color.White;
            txtSearchReturnsReadyForFiling.ForeColor = Color.Black;
        }
        #endregion

        #region txtSearchReturnsReadyForFiling_Leave
        private void txtSearchReturnsReadyForFiling_Leave(object sender, EventArgs e)
        {
            if (txtSearchReturnsReadyForFiling.Text.Trim() == "")
            {
                txtSearchReturnsReadyForFiling.Text = strSearchText;
                txtSearchReturnsReadyForFiling.BackColor = Color.Azure;
                txtSearchReturnsReadyForFiling.ForeColor = Color.SlateGray;
            }
        }
        #endregion

        #region txtSearchFiledReturns_Enter
        private void txtSearchFiledReturns_Enter(object sender, EventArgs e)
        {
            txtSearchFiledReturns.Text = "";
            txtSearchFiledReturns.BackColor = Color.White;
            txtSearchFiledReturns.ForeColor = Color.Black;
        }
        #endregion

        #region txtSearchFiledReturns_Leave
        private void txtSearchFiledReturns_Leave(object sender, EventArgs e)
        {
            if (txtSearchFiledReturns.Text.Trim() == "")
            {
                txtSearchFiledReturns.Text = strSearchText;
                txtSearchFiledReturns.BackColor = Color.Azure;
                txtSearchFiledReturns.ForeColor = Color.SlateGray;
            }
        }
        #endregion



        #region txtSearchFilingStatus_TextChanged
        private void txtSearchFilingStatus_TextChanged(object sender, EventArgs e)
        {
            if (blExit == true) return;
            //
            if (dgvFilingStatus.Visible == true)
            {
                if (txtSearchFilingStatus.Text.Trim() == "" || txtSearchFilingStatus.Text.Trim() == strFilingStatusSearchText)
                    LoadFilingStatus("");
                else if (txtSearchFilingStatus.Text.Length >= 3)
                    LoadFilingStatus(txtSearchFilingStatus.Text.Trim());
            }
        }
        #endregion


        #region txtSearchFilingStatus_Leave
        private void txtSearchFilingStatus_Leave(object sender, EventArgs e)
        {
            if (txtSearchFilingStatus.Text.Trim() == "")
            {
                txtSearchFilingStatus.Text = strFilingStatusSearchText;
                txtSearchFilingStatus.BackColor = Color.Azure;
                txtSearchFilingStatus.ForeColor = Color.SlateGray;
            }
        }
        #endregion


        #region txtSearchFilingStatus_Enter
        private void txtSearchFilingStatus_Enter(object sender, EventArgs e)
        {
            txtSearchFilingStatus.Text = "";
            txtSearchFilingStatus.BackColor = Color.White;
            txtSearchFilingStatus.ForeColor = Color.Black;
        }
        #endregion

        #region chkMakeDashboardDefaultHomeScreen_CheckedChanged
        private void chkMakeDashboardDefaultHomeScreen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMakeDashboardDefaultHomeScreen.Checked == true)
            {
                strSQL = "UPDATE MST_SETUP SET DEFAULT_HOME_DASHBOARD_FLAG = 1 ";
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                {
                    if (TDSMAN.Classes.TDSMAN.T_MACHINE_ID > 0)
                        strSQL = strSQL + " WHERE SETUPID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " ";
                }
                dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            }
            else
            {
                strSQL = "UPDATE MST_SETUP SET DEFAULT_HOME_DASHBOARD_FLAG = 0 ";
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                {
                    if (TDSMAN.Classes.TDSMAN.T_MACHINE_ID > 0)
                        strSQL = strSQL + " WHERE SETUPID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " ";
                }
                dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            }
        }
        #endregion



        #region User Define Functions

        #region LoadGrid

        #region LoadReturnsUnderProcessFirstScreen

        #region COMMENTED
        //private void LoadReturnsUnderProcessFirstScreen(string SearchSQL, long NoOfRecordsSQL, long NoOfDaysSQL)
        //{
        //    DataSet dsetGridClone = new DataSet();
        //    try
        //    {
        //        dgvReturnsUnderProcessSecondScreen.Visible = false;
        //        dgvReturnsUnderProcessFirstScreen.Visible = true;
        //        // 1 :  SUCCESSFULLY GENERATED
        //        // 2 :  SUCCESSFULLY GENERATED WITH MISMATCH REPORT
        //        // 3 :  ERROR HAS OCCURRED
        //        //-----------------------------------------------------------
        //        //dgvReturnsUnderProcessFirstScreen.AutoGenerateColumns = true;
        //        //--
        //        string[,] strMatrix = {{"BASIC_INFO_ID", "0", "", "", "", "F", ""},
        //                            {"Type", "45", "", "", "", "", ""},
        //                            {"TAN", "100", "", "", "", "", ""},
        //                            {"Company", "300", "", "", "", "", ""},
        //                            {"FA Year", "80", "", "", "", "", ""},
        //                            {"Qtr", "60", "", "", "", "", ""},
        //                            {"Form", "60", "", "", "", "", ""},
        //                            {"Filing Due Date", "125", "", "", "", "", ""},
        //                            {"LAST_UPDATE_DATETIME_COMPARE", "0", "", "", "", "F", ""},
        //                            {"ASST_ID", "0", "", "", "", "F", ""},
        //                            {"COMPANY_ID", "0", "", "", "", "F", ""},
        //                            {"PREDICT_DEFAULTS_ACCESS_DATETIME", "0", "", "", "", "F", ""},
        //                            {"PREDICT_DEFAULTS_STATUS", "0", "", "", "", "F", ""},
        //                            {"LAST_UPDATE_DATETIME", "0", "", "", "", "F", ""}};
        //        //{"Status", "80", "", "", "", "", ""},
        //        //{"Receipt No.", "90", "", "", "", "", ""},
        //        //{"Date of Filing", "80", "", "", "", "", ""}};
        //        //--
        //        //string[,] strValidationStatusMatrix = {{cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 1", "F", "Successfully Generated", "T"},
        //        //                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 2", "F", "Warning while Generated", "T"},
        //        //                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 3", "F", "Error while Generated", "T"},
        //        //                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 0", "F", "", "T"}};
        //        //"" + cmnService.J_SQLDBFormat(strValidationStatusMatrix, J_SQLColFormat.Case_End) + @" AS VALIDATION_STATUS,
        //        //
        //        NoOfRecordsSQL = Convert.ToInt32(txtNoOfRecords.Text);
        //        NoOfDaysSQL = Convert.ToInt32(txtNoOfDays.Text);
        //        //-----------------------------------------------------------
        //        strSQL = @"SELECT TOP " + NoOfRecordsSQL + @" * FROM(
        //                SELECT COR_HDR_BATCH.BATCH_HEADER_ID AS BASIC_INFO_ID,
        //                          'CORR'                        AS RETURN_TYPE,
        //                          COR_HDR_COMPANY.TAN_NO,
        //                          COR_HDR_COMPANY.COMPANY_NAME,
        //                          MST_ASSESSMENT.FA_YEAR,
        //                          COR_HDR_BATCH.QTR,
        //                          COR_HDR_BATCH.FORM_NO,
        //                          'N.A.'         AS RETURN_LAST_DATE,
        //                          " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.LAST_UPDATE_DATETIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSSNumeric) + @" AS LAST_UPDATE_DATETIME_COMPARE,
        //                          MST_ASSESSMENT.ASST_ID,
        //                          COR_HDR_COMPANY.HDR_COMPANY_ID AS COMPANY_ID,
        //                          " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.PREDICT_DEFAULTS_ACCESS_DATETIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSSNumeric) + @" AS PREDICT_DEFAULTS_ACCESS_DATETIME,
        //                          COR_HDR_BATCH.PREDICT_DEFAULTS_STATUS,
        //                          COR_HDR_BATCH.LAST_UPDATE_DATETIME
        //                   FROM   (((COR_HDR_BATCH
        //                             INNER JOIN COR_HDR_COMPANY
        //                                     ON COR_HDR_COMPANY.BATCH_HEADER_ID =
        //                                        COR_HDR_BATCH.BATCH_HEADER_ID )
        //                            INNER JOIN MST_ASSESSMENT
        //                                    ON MST_ASSESSMENT.ASST_ID = COR_HDR_BATCH.ASST_ID)
        //                           LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
        //                                             TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
        //                                      FROM   TRN_FILE_GENERATION_LOG
        //                                             INNER JOIN (SELECT BASIC_INFO_ID,
        //                                                                MAX(FG_LOG_ID) AS FG_LOG_ID
        //                                                         FROM   TRN_FILE_GENERATION_LOG
        //                                                         WHERE  CORR = 1
        //                                                         GROUP  BY BASIC_INFO_ID) AS
        //                                                        LAST_FG_LOG
        //                                                     ON LAST_FG_LOG.FG_LOG_ID =
        //                                                        TRN_FILE_GENERATION_LOG.FG_LOG_ID)
        //                                     AS GENERATION_STATUS
        //                                  ON GENERATION_STATUS.BASIC_INFO_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
        //                WHERE " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (0,3) ";
        //        //if (SearchSQL != "")
        //        //    strSQL = strSQL + "AND COR_HDR_COMPANY.TAN_NO + COR_HDR_COMPANY.COMPANY_NAME + MST_ASSESSMENT.FA_YEAR + COR_HDR_BATCH.QTR + COR_HDR_BATCH.FORM_NO LIKE '%" + SearchSQL + @"%' ";
        //        strSQL = strSQL + @" UNION
        //                SELECT TRN_BASIC_INFO.BASIC_INFO_ID,
        //                      'REG' AS RETURN_TYPE,
        //                      MST_COMPANY.TAN_NO,
        //                      MST_COMPANY.COMPANY_NAME,
        //                      MST_ASSESSMENT.FA_YEAR,
        //                      TRN_BASIC_INFO.QTR,
        //                      TRN_BASIC_INFO.FORM_NO," +
        //                      cmnService.J_SQLDBFormat("TRN_BASIC_INFO.RETURN_LAST_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS RETURN_LAST_DATE,
        //                      " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.LAST_UPDATE_DATETIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSSNumeric) + @" AS LAST_UPDATE_DATETIME_COMPARE,
        //                      MST_ASSESSMENT.ASST_ID,
        //                      TRN_BASIC_INFO.COMPANY_ID,
        //                      " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.PREDICT_DEFAULTS_ACCESS_DATETIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSSNumeric) + @" AS PREDICT_DEFAULTS_ACCESS_DATETIME,
        //                      TRN_BASIC_INFO.PREDICT_DEFAULTS_STATUS,
        //                      TRN_BASIC_INFO.LAST_UPDATE_DATETIME
        //               FROM   ((((TRN_BASIC_INFO
        //                          INNER JOIN MST_COMPANY
        //                                  ON MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID)
        //                         INNER JOIN MST_ASSESSMENT
        //                                 ON MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID)
        //                        LEFT JOIN (SELECT BASIC_INFO_ID,
        //                                          COUNT(*) AS TOTAL_CHALLANS,
        //                                          SUM(TOT_TAX) AS TOT_TAX
        //                                   FROM   TRN_CHALLAN
        //                                   GROUP  BY BASIC_INFO_ID) AS CHALLANS
        //                               ON CHALLANS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
        //                       LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
        //                                         TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
        //                                  FROM   TRN_FILE_GENERATION_LOG
        //                                         INNER JOIN (SELECT BASIC_INFO_ID,
        //                                                            MAX(FG_LOG_ID) AS FG_LOG_ID
        //                                                     FROM   TRN_FILE_GENERATION_LOG
        //                                                     WHERE  CORR = 0
        //                                                     GROUP  BY BASIC_INFO_ID) AS LAST_FG_LOG
        //                                                 ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID)
        //                                 AS  GENERATION_STATUS
        //                              ON GENERATION_STATUS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
        //                WHERE " + cmnService.J_SQLDBFormat("CHALLANS.TOTAL_CHALLANS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" > 0 
        //                AND   " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (0,3) ) REG_CORR_RETURN ";
        //        //-- 2021/11/23
        //        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
        //            strSQL = strSQL  + "WHERE   (REG_CORR_RETURN.LAST_UPDATE_DATETIME IS NULL OR DATEDIFF(day, REG_CORR_RETURN.LAST_UPDATE_DATETIME, getdate()) <= " + NoOfDaysSQL + ") ";
        //        else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
        //            strSQL = strSQL + "WHERE   (REG_CORR_RETURN.LAST_UPDATE_DATETIME IS NULL OR DATEDIFF('d', REG_CORR_RETURN.LAST_UPDATE_DATETIME," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
        //        //--
        //        if (SearchSQL!="")
        //                strSQL = strSQL  + " AND REG_CORR_RETURN.RETURN_TYPE + REG_CORR_RETURN.TAN_NO + REG_CORR_RETURN.COMPANY_NAME + REG_CORR_RETURN.FA_YEAR + REG_CORR_RETURN.QTR + REG_CORR_RETURN.FORM_NO + REG_CORR_RETURN.RETURN_LAST_DATE LIKE '%" + SearchSQL + @"%' ";
        //        //
        //        strSQL = strSQL + " ORDER BY REG_CORR_RETURN.LAST_UPDATE_DATETIME DESC, REG_CORR_RETURN.FA_YEAR DESC, REG_CORR_RETURN.QTR DESC, REG_CORR_RETURN.BASIC_INFO_ID DESC";
        //        //-----------
        //        dgvReturnsUnderProcessFirstScreen.AutoGenerateColumns = true;
        //        if (dsetGridClone != null) dsetGridClone.Clear();
        //        dgvReturnsUnderProcessFirstScreen.DataSource = null;
        //        dgvReturnsUnderProcessFirstScreen.Columns.Clear();
        //        dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvReturnsUnderProcessFirstScreen, strSQL, strMatrix);
        //        //dgvReturnsUnderProcessFirstScreen.ClearSelection();
        //        dgvReturnsUnderProcessFirstScreen.Refresh();
        //        //--
        //        //{
        //        dgvReturnsUnderProcessFirstScreen.AutoGenerateColumns = false;
        //        DataGridViewButtonColumn BtnReturnHealth = new DataGridViewButtonColumn();
        //        {
        //            //dgvUpdates.Columns
        //            //BtnShow.Visible = true;
        //            BtnReturnHealth.HeaderText = "- Check Defaults -";
        //            BtnReturnHealth.Width = 120;
        //            //BtnReturnHealth.Text = " - Check Health - ";
        //            BtnReturnHealth.Text = "Check";
        //            BtnReturnHealth.ToolTipText = "Check Predict Defaults";
        //            BtnReturnHealth.Name = "BtnReturnHealth";
        //            BtnReturnHealth.FlatStyle = FlatStyle.Popup;
        //            BtnReturnHealth.UseColumnTextForButtonValue = true;
        //            dgvReturnsUnderProcessFirstScreen.Columns.Add(BtnReturnHealth);
        //            //BtnReturnHealth.Dispose();
        //        }
        //        //
        //        //for (int i = 0; i <= dgvReturnsUnderProcessFirstScreen.RowCount - 1; i++)
        //        //{
        //        //    // 8  -- LAST_UPDATE_DATETIME 
        //        //    // 11 -- PREDICT_DEFAULTS_ACCESS_DATETIME
        //        //    // 12 -- PREDICT_DEFAULTS_STATUS
        //        //    if (dgvReturnsUnderProcessFirstScreen.Rows[i].Cells[12].Value.ToString() != "")
        //        //    {
        //        //        if(dgvReturnsUnderProcessFirstScreen.Rows[i].Cells[12].Value.ToString() == T_PREDICT_DEFAULTS_STATUS.HAS_DEFAULT_STATUS)

        //        //        //TotalShortPayments += cmnService.J_ReturnDoubleValue(dgvReturnsUnderProcessFirstScreen.Rows[i].Cells[10].Value);
        //        //        //if(dgvReturnsUnderProcessFirstScreen.Rows[i].Cells[8].Value.ToString() )
        //        //    }
        //        //}
        //        //DataGridViewButtonColumn BtnShowReturn = new DataGridViewButtonColumn();
        //        //{
        //        //    //dgvUpdates.Columns
        //        //    //BtnShow.Visible = true;
        //        //    BtnShowReturn.HeaderText = "";
        //        //    BtnShowReturn.Text = "Return";
        //        //    BtnShowReturn.ToolTipText = "Go to the Return";
        //        //    BtnShowReturn.Name = "BtnShowReturn";
        //        //    BtnShowReturn.UseColumnTextForButtonValue = true;
        //        //    dgvReturnsUnderProcessFirstScreen.Columns.Add(BtnShowReturn);
        //        //    //BtnShowReturn.Dispose();
        //        //}
        //    }
        //    catch (Exception err)
        //    {
        //        cmnService.J_UserMessage(err.Message);
        //    }
        //}

        #endregion

        private void LoadReturnsUnderProcessFirstScreen(string SearchSQL, string SortSQL)// long NoOfRecordsSQL, long NoOfDaysSQL)
        {
            DataSet dsetGridClone = new DataSet(); long NoOfDaysSQL = 0;
            try
            {
                //dgvReturnsUnderProcessSecondScreen.Visible = false;
                dgvReturnsUnderProcessFirstScreen.Visible = true;
                // 1 :  SUCCESSFULLY GENERATED
                // 2 :  SUCCESSFULLY GENERATED WITH MISMATCH REPORT
                // 3 :  ERROR HAS OCCURRED
                //-----------------------------------------------------------
                //dgvReturnsUnderProcessFirstScreen.AutoGenerateColumns = true;
                //--
                string[,] strMatrix = {{"BASIC_INFO_ID", "0", "", "", "", "F", ""},
                                    {"*", "25", "", "C", "", "", "T"},
                                    {"TAN", "95", "", "", "", "", "T"},
                                    {"Company", "240", "", "", "", "", "T"},
                                    {"FA_YEAR", "0", "", "", "", "F", ""},
                                    {"QTR", "0", "", "", "", "F", ""},
                                    {"FORM_NO", "0", "", "", "", "F", ""},
                                    {"FA Year [Qtr-Form]", "100", "", "", "", "", "T"},
                                    {"No. of Challans", "85", "", "R", "", "", "T"},
                                    {"Line Records", "85", "", "R", "", "", "T"},
                                    {"Challans Balance", "100", "0.00", "R", "", "", "T"},
                                    {"Filing Due Date", "100", "", "", "", "", "T"},
                                    {"LAST_UPDATE_DATETIME_COMPARE", "0", "", "", "", "F", ""},
                                    {"ASST_ID", "0", "", "", "", "F", ""},
                                    {"COMPANY_ID", "0", "", "", "", "F", ""},
                                    {"PREDICT_DEFAULTS_ACCESS_DATETIME", "0", "", "", "", "F", ""},
                                    {"PREDICT_DEFAULTS_STATUS", "0", "", "", "", "F", ""},
                                    {"LAST_UPDATE_DATETIME", "0", "", "", "", "F", ""},
                                    {"FilingDueDate_YYYYMMDD", "0", "", "", "", "F", ""}};
                //{"Status", "80", "", "", "", "", ""},
                //{"Receipt No.", "90", "", "", "", "", ""},
                //{"Date of Filing", "80", "", "", "", "", ""}};
                //--
                //string[,] strValidationStatusMatrix = {{cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 1", "F", "Successfully Generated", "T"},
                //                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 2", "F", "Warning while Generated", "T"},
                //                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 3", "F", "Error while Generated", "T"},
                //                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 0", "F", "", "T"}};
                //"" + cmnService.J_SQLDBFormat(strValidationStatusMatrix, J_SQLColFormat.Case_End) + @" AS VALIDATION_STATUS,
                //
                string[,] strCORDEDSALMatrix = {{"cor_hdr_batch.form_no = '24Q' AND cor_hdr_batch.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString)  + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"cor_hdr_batch.form_no <> '24Q' AND cor_hdr_batch.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                //
                string[,] strREGDEDSALMatrix = {{"TRN_BASIC_INFO.form_no = '24Q' AND TRN_BASIC_INFO.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString) + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"TRN_BASIC_INFO.form_no <> '24Q' AND TRN_BASIC_INFO.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                //
                //NoOfRecordsSQL = Convert.ToInt32(txtNoOfRecords.Text);
                NoOfDaysSQL = TDSMAN.Classes.TDSMAN.T_RETURNS_UNDER_PROCESS_DAYS; //Convert.ToInt32(txtNoOfDays.Text);
                //-----------------------------------------------------------
                //strSQL = @"SELECT TOP " + NoOfRecordsSQL + @" * FROM(
                strSQL = @"SELECT  * FROM(
                        SELECT COR_HDR_BATCH.BATCH_HEADER_ID   AS BASIC_INFO_ID,
                                  'C'                          AS RETURN_TYPE,
                                  COR_HDR_COMPANY.TAN_NO       AS COMPANY_TAN,
                                  COR_HDR_COMPANY.COMPANY_NAME AS COMPANY_NAME,
                                  MST_ASSESSMENT.FA_YEAR,
                                  COR_HDR_BATCH.QTR,
                                  COR_HDR_BATCH.FORM_NO," +
                                 //RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + COR_HDR_BATCH.FORM_NO + ']' AS YEAR_QTR_FORM_RETURN,
                                 "IIF(COR_HDR_BATCH.FORM_NO = '24Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '138 (24Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '26Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '140 (26Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '27Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '144 (27Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '27EQ', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '143 (27EQ)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '138', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '138 (24Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '140', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '140 (26Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '144', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '144 (27Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '143', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '143 (27EQ)' + ']', " +
                                 "'')))))))) AS YEAR_QTR_FORM_RETURN," +

                                 @"CHALLANS.TOTAL_CHALLANS,
                                  " + cmnService.J_SQLDBFormat(strCORDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS TOTAL_DEDUCTEES,
                                  TOT_TAX - TAX_DEPOSITED_AMOUNT AS CHALLAN_BALANCE,
                                  'N.A.'         AS RETURN_LAST_DATE,  
                                  " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.LAST_UPDATE_DATETIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSSNumeric) + @" AS LAST_UPDATE_DATETIME_COMPARE,
                                  MST_ASSESSMENT.ASST_ID,
                                  COR_HDR_COMPANY.HDR_COMPANY_ID AS COMPANY_ID,
                                  " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.PREDICT_DEFAULTS_ACCESS_DATETIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSSNumeric) + @" AS PREDICT_DEFAULTS_ACCESS_DATETIME,
                                  COR_HDR_BATCH.PREDICT_DEFAULTS_STATUS,
                                  COR_HDR_BATCH.LAST_UPDATE_DATETIME,
                                  '00000000' AS RETURN_LAST_DATE_YYYYMMDD
                           FROM   ((((((COR_HDR_BATCH
                                     INNER JOIN COR_HDR_COMPANY
                                             ON COR_HDR_COMPANY.BATCH_HEADER_ID =
                                                COR_HDR_BATCH.BATCH_HEADER_ID )
                                    INNER JOIN MST_ASSESSMENT
                                            ON MST_ASSESSMENT.ASST_ID = COR_HDR_BATCH.ASST_ID)
                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID,
                                                         COUNT(*)     AS TOTAL_CHALLANS,
                                                         SUM(TOT_TAX) AS TOT_TAX
                                                FROM     COR_TRN_CHALLAN
                                                GROUP BY BATCH_HEADER_ID) AS CHALLANS
                                    ON         CHALLANS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                         cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                         cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                                FROM     COR_TRN_DEDUCTEE_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS DEDUCTEES
                                    ON         DEDUCTEES.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)

                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                          cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                                FROM     COR_TRN_SALARY_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS SALARY
                                    ON         SALARY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)

                                   LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                                     TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
                                              FROM   TRN_FILE_GENERATION_LOG
                                                     INNER JOIN (SELECT BASIC_INFO_ID,
                                                                        MAX(FG_LOG_ID) AS FG_LOG_ID
                                                                 FROM   TRN_FILE_GENERATION_LOG
                                                                 WHERE  CORR = 1
                                                                 GROUP  BY BASIC_INFO_ID) AS
                                                                LAST_FG_LOG
                                                             ON LAST_FG_LOG.FG_LOG_ID =
                                                                TRN_FILE_GENERATION_LOG.FG_LOG_ID)
                                             AS GENERATION_STATUS
                                          ON GENERATION_STATUS.BASIC_INFO_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                        WHERE " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (0,3) ";
                //if (SearchSQL != "")
                //    strSQL = strSQL + "AND COR_HDR_COMPANY.TAN_NO + COR_HDR_COMPANY.COMPANY_NAME + MST_ASSESSMENT.FA_YEAR + COR_HDR_BATCH.QTR + COR_HDR_BATCH.FORM_NO LIKE '%" + SearchSQL + @"%' ";
                strSQL = strSQL + @" UNION
                        SELECT TRN_BASIC_INFO.BASIC_INFO_ID,
                              'R' AS RETURN_TYPE,
                              MST_COMPANY.TAN_NO       AS COMPANY_TAN,
                              MST_COMPANY.COMPANY_NAME AS COMPANY_NAME,
                              MST_ASSESSMENT.FA_YEAR,
                              TRN_BASIC_INFO.QTR,
                              TRN_BASIC_INFO.FORM_NO," +
                              //RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + TRN_BASIC_INFO.FORM_NO + ']' AS YEAR_QTR_FORM_RETURN,
                              "IIF(TRN_BASIC_INFO.FORM_NO = '24Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '138 (24Q)' + ']', " +
                                 "IIF(TRN_BASIC_INFO.FORM_NO = '26Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '140 (26Q)' + ']', " +
                                 "IIF(TRN_BASIC_INFO.FORM_NO = '27Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '144 (27Q)' + ']', " +
                                 "IIF(TRN_BASIC_INFO.FORM_NO = '27EQ', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '143 (27EQ)' + ']', '')))) AS YEAR_QTR_FORM_RETURN," +
                             @"CHALLANS.TOTAL_CHALLANS,
                              " + cmnService.J_SQLDBFormat(strREGDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS TOTAL_DEDUCTEES,
                              TOT_TAX - TAX_DEPOSITED_AMOUNT AS CHALLAN_BALANCE,
                              " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.RETURN_LAST_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS RETURN_LAST_DATE,
                              " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.LAST_UPDATE_DATETIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSSNumeric) + @" AS LAST_UPDATE_DATETIME_COMPARE,
                              MST_ASSESSMENT.ASST_ID,
                              TRN_BASIC_INFO.COMPANY_ID,
                              " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.PREDICT_DEFAULTS_ACCESS_DATETIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSSNumeric) + @" AS PREDICT_DEFAULTS_ACCESS_DATETIME,
                              TRN_BASIC_INFO.PREDICT_DEFAULTS_STATUS,
                              TRN_BASIC_INFO.LAST_UPDATE_DATETIME,
                              " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.RETURN_LAST_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + @" AS RETURN_LAST_DATE_YYYYMMDD
                       FROM   ((((((TRN_BASIC_INFO
                                  INNER JOIN MST_COMPANY
                                          ON MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID)
                                 INNER JOIN MST_ASSESSMENT
                                         ON MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID)
                                LEFT JOIN (SELECT BASIC_INFO_ID,
                                                  COUNT(*) AS TOTAL_CHALLANS,
                                                  SUM(TOT_TAX) AS TOT_TAX
                                           FROM   TRN_CHALLAN
                                           GROUP  BY BASIC_INFO_ID) AS CHALLANS
                                       ON CHALLANS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                                LEFT JOIN
                                           (SELECT   BASIC_INFO_ID," +
                                                     cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                     cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                            FROM     TRN_DEDUCTEE_DETAILS
                                            GROUP BY BASIC_INFO_ID) AS DEDUCTEES
                                ON         DEDUCTEES.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)

                                LEFT JOIN
                                            (SELECT   BASIC_INFO_ID," +
                                                    cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                            FROM      TRN_SALARY_DETAILS
                                            GROUP BY BASIC_INFO_ID) AS SALARY
                                ON         SALARY.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)

                               LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                                 TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
                                          FROM   TRN_FILE_GENERATION_LOG
                                                 INNER JOIN (SELECT BASIC_INFO_ID,
                                                                    MAX(FG_LOG_ID) AS FG_LOG_ID
                                                             FROM   TRN_FILE_GENERATION_LOG
                                                             WHERE  CORR = 0
                                                             GROUP  BY BASIC_INFO_ID) AS LAST_FG_LOG
                                                         ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID)
                                         AS  GENERATION_STATUS
                                      ON GENERATION_STATUS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        WHERE " + cmnService.J_SQLDBFormat("CHALLANS.TOTAL_CHALLANS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" > 0 
                        AND   " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (0,3) ) REG_CORR_RETURN ";
                //-- 2021/11/23
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = strSQL + "WHERE   (REG_CORR_RETURN.LAST_UPDATE_DATETIME IS NULL OR DATEDIFF(day, REG_CORR_RETURN.LAST_UPDATE_DATETIME, getdate()) <= " + NoOfDaysSQL + ") ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = strSQL + "WHERE   (REG_CORR_RETURN.LAST_UPDATE_DATETIME IS NULL OR DATEDIFF('d', REG_CORR_RETURN.LAST_UPDATE_DATETIME," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
                //--
                if (SearchSQL != "")
                {
                    if (SearchSQL.ToUpper() == "COR")
                        strSQL = strSQL + @" AND REG_CORR_RETURN.RETURN_TYPE = 'C' ";
                    else if (SearchSQL.ToUpper() == "REG")
                        strSQL = strSQL + @" AND REG_CORR_RETURN.RETURN_TYPE = 'R' ";
                    else
                        strSQL = strSQL + @" AND REG_CORR_RETURN.RETURN_TYPE + 
                                                 REG_CORR_RETURN.COMPANY_TAN + 
                                                 REG_CORR_RETURN.COMPANY_NAME + 
                                                 REG_CORR_RETURN.YEAR_QTR_FORM_RETURN + " +
                                                 cmnService.J_SQLDBFormat("REG_CORR_RETURN.TOTAL_CHALLANS", J_SQLColFormat.ConvertToString) + " + " +
                                                 cmnService.J_SQLDBFormat("REG_CORR_RETURN.TOTAL_DEDUCTEES ", J_SQLColFormat.ConvertToString) + " + " +
                                                 cmnService.J_SQLDBFormat("REG_CORR_RETURN.CHALLAN_BALANCE ", J_SQLColFormat.ConvertToString) + @" +
                                                 REG_CORR_RETURN.RETURN_LAST_DATE LIKE '%" + SearchSQL.Replace("[", "[[]") + @"%' ";
                }
                //
                if(SortSQL == T_SortOrderReturnsUnderProcess.Last_Worked_On)
                    strSQL = strSQL + @" ORDER BY REG_CORR_RETURN.LAST_UPDATE_DATETIME DESC, 
                                                  REG_CORR_RETURN.FA_YEAR DESC, 
                                                  REG_CORR_RETURN.QTR DESC, 
                                                  REG_CORR_RETURN.BASIC_INFO_ID DESC";
                else if (SortSQL == T_SortOrderReturnsUnderProcess.Latest_Return)
                    strSQL = strSQL + @" ORDER BY REG_CORR_RETURN.ASST_ID DESC, 
                                                  REG_CORR_RETURN.QTR DESC, 
                                                  REG_CORR_RETURN.BASIC_INFO_ID DESC, REG_CORR_RETURN.COMPANY_NAME";
                else if (SortSQL == T_SortOrderReturnsUnderProcess.Filing_Due_Date)
                    strSQL = strSQL + @" ORDER BY REG_CORR_RETURN.RETURN_LAST_DATE_YYYYMMDD DESC, 
                                                  REG_CORR_RETURN.FA_YEAR DESC, 
                                                  REG_CORR_RETURN.QTR DESC, 
                                                  REG_CORR_RETURN.BASIC_INFO_ID DESC";
                //-----------
                txtSortOnReturnsUnderProcess.Text = "Sort - " + SortSQL;
                //--
                dgvReturnsUnderProcessFirstScreen.AutoGenerateColumns = true;
                if (dsetGridClone != null) dsetGridClone.Clear();
                dgvReturnsUnderProcessFirstScreen.DataSource = null;
                dgvReturnsUnderProcessFirstScreen.Columns.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvReturnsUnderProcessFirstScreen, strSQL, strMatrix);
                strSQLReturnUnderProcess = strSQL; //-- for export to csv
                //dgvReturnsUnderProcessFirstScreen.ClearSelection();
                dgvReturnsUnderProcessFirstScreen.Refresh();
                //--
                //{
                dgvReturnsUnderProcessFirstScreen.AutoGenerateColumns = false;
                DataGridViewButtonColumn BtnReturnHealth = new DataGridViewButtonColumn();
                {
                    //dgvUpdates.Columns
                    //BtnShow.Visible = true;
                    BtnReturnHealth.HeaderText = "     - Status -";
                    BtnReturnHealth.Width = 110;
                    //BtnReturnHealth.Text = " - Check Health - ";
                    BtnReturnHealth.Text = "Check";
                    BtnReturnHealth.ToolTipText = "Check Predict Defaults";
                    BtnReturnHealth.Name = "BtnReturnHealth";
                    BtnReturnHealth.FlatStyle = FlatStyle.Popup;
                    BtnReturnHealth.UseColumnTextForButtonValue = true;
                    dgvReturnsUnderProcessFirstScreen.Columns.Add(BtnReturnHealth);
                    //BtnReturnHealth.Dispose();
                }
                //
                lblTotalCountUP.Text = "Total Record Count : " + dgvReturnsUnderProcessFirstScreen.RowCount.ToString();
                //for (int i = 0; i <= dgvReturnsUnderProcessFirstScreen.RowCount - 1; i++)
                //{
                //    // 8  -- LAST_UPDATE_DATETIME 
                //    // 11 -- PREDICT_DEFAULTS_ACCESS_DATETIME
                //    // 12 -- PREDICT_DEFAULTS_STATUS
                //    if (dgvReturnsUnderProcessFirstScreen.Rows[i].Cells[12].Value.ToString() != "")
                //    {
                //        if(dgvReturnsUnderProcessFirstScreen.Rows[i].Cells[12].Value.ToString() == T_PREDICT_DEFAULTS_STATUS.HAS_DEFAULT_STATUS)

                //        //TotalShortPayments += cmnService.J_ReturnDoubleValue(dgvReturnsUnderProcessFirstScreen.Rows[i].Cells[10].Value);
                //        //if(dgvReturnsUnderProcessFirstScreen.Rows[i].Cells[8].Value.ToString() )
                //    }
                //}
                //DataGridViewButtonColumn BtnShowReturn = new DataGridViewButtonColumn();
                //{
                //    //dgvUpdates.Columns
                //    //BtnShow.Visible = true;
                //    BtnShowReturn.HeaderText = "";
                //    BtnShowReturn.Text = "Return";
                //    BtnShowReturn.ToolTipText = "Go to the Return";
                //    BtnShowReturn.Name = "BtnShowReturn";
                //    BtnShowReturn.UseColumnTextForButtonValue = true;
                //    dgvReturnsUnderProcessFirstScreen.Columns.Add(BtnShowReturn);
                //    //BtnShowReturn.Dispose();
                //}
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion


        #region LoadReturnsUnderProcessSecondScreen
        //private void LoadReturnsUnderProcessSecondScreen(string SearchSQL, long NoOfRecordsSQL, long NoOfDaysSQL)
        //{
        //    DataSet dsetGridClone = new DataSet(); String strSQLNullCheckCorr = "", strSQLNullCheckReg = "";
        //    try
        //    {
        //        dgvReturnsUnderProcessSecondScreen.Visible = true;
        //        dgvReturnsUnderProcessFirstScreen.Visible = false;
        //        // 1 :  SUCCESSFULLY GENERATED
        //        // 2 :  SUCCESSFULLY GENERATED WITH MISMATCH REPORT
        //        // 3 :  ERROR HAS OCCURRED
        //        //-----------------------------------------------------------
        //        string[,] strMatrix = {{"BASIC_INFO_ID", "0", "", "", "", "F", ""},
        //                            {"Type", "45", "", "", "", "", ""},
        //                            {"TAN", "95", "", "", "", "", ""},
        //                            {"Company", "100", "", "", "", "", ""},
        //                            {"FA Yr", "70", "", "", "", "", ""},
        //                            {"Qtr", "35", "", "", "", "", ""},
        //                            {"Form", "40", "", "", "", "", ""},
        //                            {"Challan", "55", "", "R", "", "", ""},
        //                            {"Chl-Value", "100",  "0.00", "R", "", "", ""},
        //                            {"Deductee", "80", "", "R", "", "", ""},
        //                            {"Ded-Value", "100",  "0.00", "R", "", "", ""},
        //                            {"Difference", "95", "0.00", "R", "", "", ""},
        //                            {"Salary", "73", "", "R", "", "", ""},
        //                            {"LAST_UPDATE_DATETIME", "0", "", "", "", "F", ""},
        //                            {"ASST_ID", "0", "", "", "", "F", ""},
        //                            {"COMPANY_ID", "0", "", "", "", "F", ""},
        //                            {"PREDICT_DEFAULTS_ACCESS_DATETIME", "0", "", "", "", "F", ""}};
        //        //{"Status", "80", "", "", "", "", ""},
        //        //{"Receipt No.", "90", "", "", "", "", ""},
        //        //{"Date of Filing", "80", "", "", "", "", ""}};
        //        //--
        //        //string[,] strValidationStatusMatrix = {{cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 1", "F", "Successfully Generated", "T"},
        //        //                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 2", "F", "Warning while Generated", "T"},
        //        //                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 3", "F", "Error while Generated", "T"},
        //        //                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 0", "F", "", "T"}};
        //        //"" + cmnService.J_SQLDBFormat(strValidationStatusMatrix, J_SQLColFormat.Case_End) + @" AS VALIDATION_STATUS,
        //        string[,] strLoadChallanTotTaxMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
        //                                            {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED- TRN_CHALLAN.LATE_FEE", "F"}};
        //        //
        //        string[,] strLoadChallanCorrTotTaxMatrix = {{"COR_TRN_CHALLAN.BOOK_ENTRY =  1", "F", "COR_TRN_CHALLAN.TOT_TAX - COR_TRN_CHALLAN.INTEREST_ALLOCATED - COR_TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
        //                                            {"COR_TRN_CHALLAN.BOOK_ENTRY <> 1", "F", "COR_TRN_CHALLAN.TOT_TAX - COR_TRN_CHALLAN.INTEREST_ALLOCATED - COR_TRN_CHALLAN.OTHERS_ALLOCATED - COR_TRN_CHALLAN.LATE_FEE", "F"}};
        //        //
        //        if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
        //        {
        //            strSQLNullCheckReg = "IIF(SALARY_DETAILS.TOTAL_SALARY_DETAILS IS NULL, 'N.A.', SALARY_DETAILS.TOTAL_SALARY_DETAILS) AS TOTAL_SALARY_DETAILS,";
        //            strSQLNullCheckCorr = "IIF(SALARY_DETAILS.TOTAL_SALARY_DETAILS IS NULL, 'N.A.', '-') AS TOTAL_SALARY_DETAILS,";
        //        }
        //        else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
        //        {
        //            strSQLNullCheckReg = "CASE WHEN SALARY_DETAILS.TOTAL_SALARY_DETAILS IS NULL THEN 'N.A.' ELSE CAST(SALARY_DETAILS.TOTAL_SALARY_DETAILS AS VARCHAR) END AS TOTAL_SALARY_DETAILS,";
        //            strSQLNullCheckCorr = "CASE WHEN SALARY_DETAILS.TOTAL_SALARY_DETAILS IS NULL THEN 'N.A.' ELSE '-' END AS TOTAL_SALARY_DETAILS,";
        //        }
        //        //
        //        NoOfRecordsSQL = Convert.ToInt32(txtNoOfRecords.Text);
        //        NoOfDaysSQL = Convert.ToInt32(txtNoOfDays.Text);
        //        //-----------------------------------------------------------
        //        #region COMMENT
        //        //strSQL = @"SELECT TOP " + NoOfRecordsSQL + @" * FROM(
        //        //        SELECT COR_HDR_BATCH.BATCH_HEADER_ID AS BASIC_INFO_ID,
        //        //                  'CORR'                        AS RETURN_TYPE,
        //        //                  COR_HDR_COMPANY.TAN_NO,
        //        //                  COR_HDR_COMPANY.COMPANY_NAME,
        //        //                  MST_ASSESSMENT.FA_YEAR,
        //        //                  COR_HDR_BATCH.QTR,
        //        //                  COR_HDR_BATCH.FORM_NO,
        //        //                  '-'               AS TOTAL_CHALLANS,
        //        //                  '-'               AS TOT_TAX,
        //        //                  '-'               AS TOTAL_DEDUCTEES,
        //        //                  '-'               AS TOT_TAX_DEPOSITED,
        //        //                  CHALLAN_DIFF.DIFF AS DIFF,   
        //        //                  '-'               AS TOTAL_SALARY_DETAILS,
        //        //                  COR_HDR_BATCH.LAST_UPDATE_DATETIME,
        //        //                  MST_ASSESSMENT.ASST_ID,
        //        //                  COR_HDR_COMPANY.HDR_COMPANY_ID AS COMPANY_ID,
        //        //                  COR_HDR_BATCH.PREDICT_DEFAULTS_ACCESS_DATETIME
        //        //           FROM   ((((COR_HDR_BATCH
        //        //                     INNER JOIN COR_HDR_COMPANY
        //        //                             ON COR_HDR_COMPANY.BATCH_HEADER_ID =
        //        //                                COR_HDR_BATCH.BATCH_HEADER_ID )
        //        //                    INNER JOIN MST_ASSESSMENT
        //        //                            ON MST_ASSESSMENT.ASST_ID = COR_HDR_BATCH.ASST_ID)                
        //        //                    INNER JOIN (SELECT COR_TRN_CHALLAN.BATCH_HEADER_ID,
        //        //                                      SUM(" + cmnService.J_SQLDBFormat(strLoadChallanCorrTotTaxMatrix, J_SQLColFormat.Case_End) + @" - COR_TRN_CHALLAN.CTRL_TOT_TAX ) AS DIFF
        //        //                 FROM (COR_HDR_BATCH  INNER JOIN COR_TRN_CHALLAN 
        //        //             ON   COR_HDR_BATCH.BATCH_HEADER_ID = COR_TRN_CHALLAN.BATCH_HEADER_ID)
        //        //             GROUP BY COR_TRN_CHALLAN.BATCH_HEADER_ID) AS CHALLAN_DIFF
        //        //                   ON  CHALLAN_DIFF.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)  
        //        //                   LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
        //        //                                     TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
        //        //                              FROM   TRN_FILE_GENERATION_LOG
        //        //                                     INNER JOIN (SELECT BASIC_INFO_ID,
        //        //                                                        MAX(FG_LOG_ID) AS FG_LOG_ID
        //        //                                                 FROM   TRN_FILE_GENERATION_LOG
        //        //                                                 WHERE  CORR = 1
        //        //                                                 GROUP  BY BASIC_INFO_ID) AS
        //        //                                                LAST_FG_LOG
        //        //                                             ON LAST_FG_LOG.FG_LOG_ID =
        //        //                                                TRN_FILE_GENERATION_LOG.FG_LOG_ID)
        //        //                             AS GENERATION_STATUS
        //        //                          ON GENERATION_STATUS.BASIC_INFO_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
        //        //        WHERE " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (0,3)";
        //        //--
        //        #endregion
        //        strSQL = @"SELECT TOP " + NoOfRecordsSQL + @" * FROM(
        //                SELECT COR_HDR_BATCH.BATCH_HEADER_ID AS BASIC_INFO_ID,
        //                          'CORR'                        AS RETURN_TYPE,
        //                          COR_HDR_COMPANY.TAN_NO,
        //                          COR_HDR_COMPANY.COMPANY_NAME,
        //                          MST_ASSESSMENT.FA_YEAR,
        //                          COR_HDR_BATCH.QTR,
        //                          COR_HDR_BATCH.FORM_NO,
        //                          '-'               AS TOTAL_CHALLANS,
        //                          '-'               AS TOT_TAX,
        //                          '-'               AS TOTAL_DEDUCTEES,
        //                          '-'               AS TOT_TAX_DEPOSITED,
        //                          '-'               AS DIFF,"
        //                          + strSQLNullCheckCorr + @"
        //                          COR_HDR_BATCH.LAST_UPDATE_DATETIME,
        //                          MST_ASSESSMENT.ASST_ID,
        //                          COR_HDR_COMPANY.HDR_COMPANY_ID AS COMPANY_ID,
        //                          COR_HDR_BATCH.PREDICT_DEFAULTS_ACCESS_DATETIME
        //                   FROM   ((((COR_HDR_BATCH
        //                             INNER JOIN COR_HDR_COMPANY
        //                                     ON COR_HDR_COMPANY.BATCH_HEADER_ID =
        //                                        COR_HDR_BATCH.BATCH_HEADER_ID )
        //                            INNER JOIN MST_ASSESSMENT
        //                                    ON MST_ASSESSMENT.ASST_ID = COR_HDR_BATCH.ASST_ID)  
        //                            LEFT JOIN (SELECT BATCH_HEADER_ID,
        //                                              COUNT(*) AS TOTAL_SALARY_DETAILS
        //                                        FROM  COR_TRN_SALARY_DETAILS
        //                                        GROUP BY BATCH_HEADER_ID) AS SALARY_DETAILS
        //                                    ON SALARY_DETAILS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
        //                           LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
        //                                             TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
        //                                      FROM   TRN_FILE_GENERATION_LOG
        //                                             INNER JOIN (SELECT BASIC_INFO_ID,
        //                                                                MAX(FG_LOG_ID) AS FG_LOG_ID
        //                                                         FROM   TRN_FILE_GENERATION_LOG
        //                                                         WHERE  CORR = 1
        //                                                         GROUP  BY BASIC_INFO_ID) AS
        //                                                        LAST_FG_LOG
        //                                                     ON LAST_FG_LOG.FG_LOG_ID =
        //                                                        TRN_FILE_GENERATION_LOG.FG_LOG_ID)
        //                                     AS GENERATION_STATUS
        //                                  ON GENERATION_STATUS.BASIC_INFO_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
        //                WHERE " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (0,3)";
        //        //if (SearchSQL != "")
        //        //    strSQL = strSQL + "AND COR_HDR_COMPANY.TAN_NO + COR_HDR_COMPANY.COMPANY_NAME + MST_ASSESSMENT.FA_YEAR + COR_HDR_BATCH.QTR + COR_HDR_BATCH.FORM_NO LIKE '%" + SearchSQL + @"%' ";
        //        strSQL = strSQL + @" UNION
        //                SELECT TRN_BASIC_INFO.BASIC_INFO_ID,
        //                      'REG' AS RETURN_TYPE,
        //                      MST_COMPANY.TAN_NO,
        //                      MST_COMPANY.COMPANY_NAME,
        //                      MST_ASSESSMENT.FA_YEAR,
        //                      TRN_BASIC_INFO.QTR,
        //                      TRN_BASIC_INFO.FORM_NO," +
        //                      cmnService.J_SQLDBFormat("CHALLANS.TOTAL_CHALLANS", J_SQLColFormat.ConvertToString) + @"    AS TOTAL_CHALLANS," +
        //                      cmnService.J_SQLDBFormat("CHALLANS.TOT_TAX", J_SQLColFormat.ConvertToString) + @"            AS TOT_TAX," +
        //                      cmnService.J_SQLDBFormat(cmnService.J_SQLDBFormat("DEDUCTEES.TOTAL_DEDUCTEES", J_ColumnType.Integer, J_SQLColFormat.NullCheck), J_SQLColFormat.ConvertToString) + @"   AS TOTAL_DEDUCTEES," +
        //                      cmnService.J_SQLDBFormat(cmnService.J_SQLDBFormat("DEDUCTEES.TOT_TAX_DEPOSITED", J_ColumnType.Double, J_SQLColFormat.NullCheck), J_SQLColFormat.ConvertToString) + @" AS TOT_TAX_DEPOSITED, " +
        //                      cmnService.J_SQLDBFormat(cmnService.J_SQLDBFormat("CHALLAN_DIFF.DIFF", J_ColumnType.Double, J_SQLColFormat.NullCheck), J_SQLColFormat.ConvertToString) + @" AS DIFF, ";
        //        strSQL = strSQL + strSQLNullCheckReg + @"
        //                      TRN_BASIC_INFO.LAST_UPDATE_DATETIME,
        //                      MST_ASSESSMENT.ASST_ID,
        //                      TRN_BASIC_INFO.COMPANY_ID,
        //                      TRN_BASIC_INFO.PREDICT_DEFAULTS_ACCESS_DATETIME
        //               FROM   (((((((TRN_BASIC_INFO
        //                          INNER JOIN MST_COMPANY
        //                                  ON MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID)
        //                         INNER JOIN MST_ASSESSMENT
        //                                 ON MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID)
        //                        LEFT JOIN (SELECT BASIC_INFO_ID,
        //                                          COUNT(*) AS TOTAL_CHALLANS,
        //                                          SUM(TOT_TAX) AS TOT_TAX
        //                                   FROM   TRN_CHALLAN
        //                                   GROUP  BY BASIC_INFO_ID) AS CHALLANS
        //                               ON CHALLANS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
        //                        LEFT JOIN (SELECT BASIC_INFO_ID,
        //                                          COUNT(*) AS TOTAL_DEDUCTEES,
        //                                          SUM(TAX_DEPOSITED_AMOUNT) AS TOT_TAX_DEPOSITED
        //                                   FROM   TRN_DEDUCTEE_DETAILS
        //                                   GROUP  BY BASIC_INFO_ID) AS DEDUCTEES
        //                               ON DEDUCTEES.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
        //                        INNER JOIN (SELECT TRN_CHALLAN.BASIC_INFO_ID,
        //                                           SUM(" + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + @" - TRN_CHALLAN.CTRL_TOT_TAX) AS DIFF
        //                                    FROM (TRN_BASIC_INFO 
								//                  INNER JOIN TRN_CHALLAN 
								//                  ON   TRN_BASIC_INFO.BASIC_INFO_ID = TRN_CHALLAN.BASIC_INFO_ID)
								//            GROUP BY TRN_CHALLAN.BASIC_INFO_ID) AS CHALLAN_DIFF
						  //                       ON  CHALLAN_DIFF.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
        //                       LEFT JOIN (SELECT BASIC_INFO_ID,
        //                                              COUNT(*) AS TOTAL_SALARY_DETAILS
        //                                       FROM   TRN_SALARY_DETAILS
        //                                       GROUP  BY BASIC_INFO_ID) AS SALARY_DETAILS
        //                                   ON SALARY_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
        //                       LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
        //                                         TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
        //                                  FROM   TRN_FILE_GENERATION_LOG
        //                                         INNER JOIN (SELECT BASIC_INFO_ID,
        //                                                            MAX(FG_LOG_ID) AS FG_LOG_ID
        //                                                     FROM   TRN_FILE_GENERATION_LOG
        //                                                     WHERE  CORR = 0
        //                                                     GROUP  BY BASIC_INFO_ID) AS LAST_FG_LOG
        //                                                 ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID)
        //                                 AS  GENERATION_STATUS
        //                              ON GENERATION_STATUS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
        //                WHERE " + cmnService.J_SQLDBFormat("CHALLANS.TOTAL_CHALLANS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" > 0 
        //                AND   " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (0,3) ) REG_CORR_RETURN ";
        //        //-- 2021/11/23
        //        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
        //            strSQL = strSQL + "WHERE   (REG_CORR_RETURN.LAST_UPDATE_DATETIME IS NULL OR DATEDIFF(day, REG_CORR_RETURN.LAST_UPDATE_DATETIME, getdate()) <= " + NoOfDaysSQL + ") ";
        //        else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
        //            strSQL = strSQL + "WHERE   (REG_CORR_RETURN.LAST_UPDATE_DATETIME IS NULL OR DATEDIFF('d', REG_CORR_RETURN.LAST_UPDATE_DATETIME," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
        //        //--
        //        if (SearchSQL != "")
        //            strSQL = strSQL + @" AND REG_CORR_RETURN.RETURN_TYPE + REG_CORR_RETURN.TAN_NO + REG_CORR_RETURN.COMPANY_NAME + REG_CORR_RETURN.FA_YEAR + REG_CORR_RETURN.QTR + REG_CORR_RETURN.FORM_NO + 
        //                                     REG_CORR_RETURN.TOTAL_CHALLANS + REG_CORR_RETURN.TOT_TAX + REG_CORR_RETURN.TOTAL_DEDUCTEES + REG_CORR_RETURN.TOT_TAX_DEPOSITED + 
        //                                     REG_CORR_RETURN.TOTAL_SALARY_DETAILS LIKE '%" + SearchSQL + @"%' ";
        //        //
        //        strSQL = strSQL + " ORDER BY REG_CORR_RETURN.LAST_UPDATE_DATETIME DESC, REG_CORR_RETURN.FA_YEAR DESC, REG_CORR_RETURN.QTR DESC, REG_CORR_RETURN.BASIC_INFO_ID DESC";
        //        //-----------
        //        //dgvReturnsUnderProcessSecondScreen.AllowUserToOrderColumns = false;
        //        dgvReturnsUnderProcessSecondScreen.AutoGenerateColumns = true;
        //        if (dsetGridClone != null) dsetGridClone.Clear();
        //        dgvReturnsUnderProcessSecondScreen.DataSource = null;
        //        dgvReturnsUnderProcessSecondScreen.Columns.Clear();
        //        dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvReturnsUnderProcessSecondScreen, strSQL, strMatrix);
        //        //dgvReturnsUnderProcessSecondScreen.ClearSelection();
        //        dgvReturnsUnderProcessSecondScreen.Refresh();
        //        //--
        //        dgvReturnsUnderProcessSecondScreen.AutoGenerateColumns = false;
        //        #region CellFormatting(SHOW BUTTON)
        //        //DataGridViewButtonColumn BtnShowReturn2 = new DataGridViewButtonColumn();
        //        //{
        //        //    //dgvUpdates.Columns
        //        //    //BtnShow.Visible = true;
        //        //    BtnShowReturn2.HeaderText = "";
        //        //    BtnShowReturn2.Text = "Return";
        //        //    BtnShowReturn2.ToolTipText = "Go to the Return";
        //        //    BtnShowReturn2.Name = "BtnShowReturn";
        //        //    BtnShowReturn2.UseColumnTextForButtonValue = true;
        //        //    //BtnShowReturn2.Dispose();
        //        //    dgvReturnsUnderProcessSecondScreen.Columns.Add(BtnShowReturn2);
        //        //}
        //        #endregion
        //        //-
        //    }
        //    catch (Exception err)
        //    {
        //        cmnService.J_UserMessage(err.Message);
        //    }
        //}


        #endregion

        #region LoadReturnsReadyforFiling
        private void LoadReturnsReadyforFiling(string SearchSQL)
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------------------------------------------------------
                string[,] strMatrix = {{"BASIC_INFO_ID", "0", "", "", "", "F", ""},
                                    {"*", "25", "", "C", "", "", "T"},
                                    {"TAN", "95", "", "", "", "", "T"},
                                    {"Company", "240", "", "", "", "", "T"},
                                    {"FA Year", "0", "", "", "", "F", ""},
                                    {"Qtr", "0", "", "", "", "F", ""},
                                    {"FORM_NO", "0", "", "", "", "F", ""},
                                    {"FA Year [Qtr-Form]", "100", "", "", "", "", "T"},
                                    {"No. of Challans", "85", "", "R", "", "", "T"},
                                    {"Line Records", "85", "", "R", "", "", "T"},
                                    {"Challans Balance", "100", "0.00", "R", "", "", "T"},
                                    {"Return Generated", "90", "dd/MM/yyyy", "", "", "", "T"},
                                    {"Receipt No.", "70", "", "", "", "", "T"},
                                    {"OUTPUT_FILE_PATH", "0", "", "", "", "F", ""},
                                    {"COMPANY_ID", "0", "", "", "", "F", ""},
                                    {"ASST_ID", "0", "", "", "", "F", ""},
                                    {"DATE_TIME_OF_CREATION_ACTUAL", "0", "", "", "", "F", ""}};//,
                                                                                                //{"Status", "80", "", "", "", "", ""},
                                                                                                //{"Receipt No.", "90", "", "", "", "", ""},
                                                                                                //{"Date of Filing", "80", "", "", "", "", ""}};
                                                                                                //--
                string[,] strValidationStatusMatrix = {{cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 1", "F", "Successfully Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 2", "F", "Warning while Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 3", "F", "Error while Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 0", "F", "", "T"}};
                //
                string[,] strCORDEDSALMatrix = {{"cor_hdr_batch.form_no = '24Q' AND cor_hdr_batch.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString)  + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"cor_hdr_batch.form_no <> '24Q' AND cor_hdr_batch.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                //
                string[,] strREGDEDSALMatrix = {{"TRN_BASIC_INFO.form_no = '24Q' AND TRN_BASIC_INFO.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString) + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"TRN_BASIC_INFO.form_no <> '24Q' AND TRN_BASIC_INFO.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                //
                long NoOfDaysSQL = TDSMAN.Classes.TDSMAN.T_RETURNS_READY_FOR_FILING_DAYS;
                //-----------------------------------------------------------
                strSQL = @"SELECT  * FROM(
                         SELECT COR_HDR_BATCH.BATCH_HEADER_ID AS BASIC_INFO_ID,
                               'C' AS RETURN_TYPE,
                               COR_HDR_COMPANY.TAN_NO       AS COMPANY_TAN,
                               COR_HDR_COMPANY.COMPANY_NAME AS COMPANY_NAME,
                               MST_ASSESSMENT.FA_YEAR,
                               COR_HDR_BATCH.QTR,
                               COR_HDR_BATCH.FORM_NO," +
                                //RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + COR_HDR_BATCH.FORM_NO + ']' AS YEAR_QTR_FORM_RETURN,
                                "IIF(COR_HDR_BATCH.FORM_NO = '24Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '138 (24Q)' + ']', " +
                                "IIF(COR_HDR_BATCH.FORM_NO = '26Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '140 (26Q)' + ']', " +
                                "IIF(COR_HDR_BATCH.FORM_NO = '27Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '144 (27Q)' + ']', " +
                                "IIF(COR_HDR_BATCH.FORM_NO = '27EQ', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '143 (27EQ)' + ']', " +
                                "IIF(COR_HDR_BATCH.FORM_NO = '138', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '138 (24Q)' + ']', " +
                                "IIF(COR_HDR_BATCH.FORM_NO = '140', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '140 (26Q)' + ']', " +
                                "IIF(COR_HDR_BATCH.FORM_NO = '144', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '144 (27Q)' + ']', " +
                                "IIF(COR_HDR_BATCH.FORM_NO = '143', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '143 (27EQ)' + ']', " +
                                "'')))))))) AS YEAR_QTR_FORM_RETURN," +
                              @"CHALLANS.TOTAL_CHALLANS,
                               " + cmnService.J_SQLDBFormat(strCORDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS TOTAL_DEDUCTEES,
                               TOT_TAX - TAX_DEPOSITED_AMOUNT AS CHALLAN_BALANCE,
                               GENERATION_STATUS.DATE_TIME_OF_CREATION,
                               'Update' AS UPDATE_RECEIPT_NO,
                               GENERATION_STATUS.OUTPUT_FILE_PATH,
                               COR_HDR_COMPANY.HDR_COMPANY_ID AS COMPANY_ID,
                               COR_HDR_BATCH.ASST_ID,
                               GENERATION_STATUS.DATE_TIME_OF_CREATION_ACTUAL 
                        FROM ((((((COR_HDR_BATCH
                        INNER JOIN COR_HDR_COMPANY 
                              ON COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID )
                        INNER JOIN MST_ASSESSMENT 
                              ON MST_ASSESSMENT.ASST_ID = COR_HDR_BATCH.ASST_ID)
                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID,
                                                         COUNT(*)     AS TOTAL_CHALLANS,
                                                         SUM(TOT_TAX) AS TOT_TAX
                                                FROM     COR_TRN_CHALLAN
                                                GROUP BY BATCH_HEADER_ID) AS CHALLANS
                                    ON         CHALLANS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                         cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                     cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                                FROM     COR_TRN_DEDUCTEE_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS DEDUCTEES
                                    ON         DEDUCTEES.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)

                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                          cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                                FROM     COR_TRN_SALARY_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS SALARY
                                    ON         SALARY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                                   LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                          TRN_FILE_GENERATION_LOG.VALIDATION_STATUS, 
                                         " + cmnService.J_SQLDBFormat("TRN_FILE_GENERATION_LOG.DATE_TIME_OF_CREATION", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS DATE_TIME_OF_CREATION,
                                          TRN_FILE_GENERATION_LOG.OUTPUT_FILE_PATH,
                                          TRN_FILE_GENERATION_LOG.DATE_TIME_OF_CREATION AS DATE_TIME_OF_CREATION_ACTUAL
                                   FROM TRN_FILE_GENERATION_LOG
                                   INNER JOIN (SELECT BASIC_INFO_ID,
                                                      MAX(FG_LOG_ID) AS FG_LOG_ID
                                               FROM TRN_FILE_GENERATION_LOG
                                               WHERE CORR = 1
                                               GROUP BY BASIC_INFO_ID) AS LAST_FG_LOG
                                          ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID ) AS GENERATION_STATUS
                             ON GENERATION_STATUS.BASIC_INFO_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                        WHERE " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (1,2)
                        AND   COR_HDR_BATCH.PRN_NO = '' AND COR_HDR_BATCH.DATE_OF_FILING IS NULL ";
                //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                //    strSQL = strSQL + "AND   (DATEDIFF(day, CONVERT(DATETIME, date_time_of_creation, 103), getdate()) <= " + NoOfDaysSQL + ") ";
                //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                //    strSQL = strSQL + "AND   (DATEDIFF('d', DATE_TIME_OF_CREATION," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
                //--
                //if (SearchSQL != "")
                //    strSQL = strSQL + " AND COR_HDR_COMPANY.TAN_NO + COR_HDR_COMPANY.COMPANY_NAME + MST_ASSESSMENT.FA_YEAR + COR_HDR_BATCH.QTR + COR_HDR_BATCH.FORM_NO LIKE '%" + SearchSQL + @"%' ";
                strSQL = strSQL + @"
                        UNION
                        SELECT TRN_BASIC_INFO.BASIC_INFO_ID,
                               'R' AS RETURN_TYPE,    
                               MST_COMPANY.TAN_NO       AS COMPANY_TAN,
                               MST_COMPANY.COMPANY_NAME AS COMPANY_NAME,
                               MST_ASSESSMENT.FA_YEAR,
                               TRN_BASIC_INFO.QTR,
                               TRN_BASIC_INFO.FORM_NO," +
                              //RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + TRN_BASIC_INFO.FORM_NO + ']' AS YEAR_QTR_FORM_RETURN,
                            "IIF(TRN_BASIC_INFO.FORM_NO = '24Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '138 (24Q)' + ']', " +
                            "IIF(TRN_BASIC_INFO.FORM_NO = '26Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '140 (26Q)' + ']', " +
                            "IIF(TRN_BASIC_INFO.FORM_NO = '27Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '144 (27Q)' + ']', " +
                            "IIF(TRN_BASIC_INFO.FORM_NO = '27EQ', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '143 (27EQ)' + ']', '')))) AS YEAR_QTR_FORM_RETURN," +
                             @"CHALLANS.TOTAL_CHALLANS,
                              " + cmnService.J_SQLDBFormat(strREGDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS TOTAL_DEDUCTEES,
                               TOT_TAX - TAX_DEPOSITED_AMOUNT AS CHALLAN_BALANCE,
                               GENERATION_STATUS.DATE_TIME_OF_CREATION,
                               'Update' AS UPDATE_RECEIPT_NO,
                               GENERATION_STATUS.OUTPUT_FILE_PATH,
                               MST_COMPANY.COMPANY_ID AS COMPANY_ID,
                               TRN_BASIC_INFO.ASST_ID,
                               GENERATION_STATUS.DATE_TIME_OF_CREATION_ACTUAL 
                        FROM ((((((TRN_BASIC_INFO
                        INNER JOIN MST_COMPANY 
                              ON MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID)
                        INNER JOIN MST_ASSESSMENT 
                              ON MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID)
                                LEFT JOIN (SELECT BASIC_INFO_ID,
                                                  COUNT(*) AS TOTAL_CHALLANS,
                                                  SUM(TOT_TAX) AS TOT_TAX
                                           FROM   TRN_CHALLAN
                                           GROUP  BY BASIC_INFO_ID) AS CHALLANS
                                       ON CHALLANS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                                LEFT JOIN
                                           (SELECT   BASIC_INFO_ID," +
                                                     cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                     cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                            FROM     TRN_DEDUCTEE_DETAILS
                                            GROUP BY BASIC_INFO_ID) AS DEDUCTEES
                                ON         DEDUCTEES.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)

                                LEFT JOIN
                                            (SELECT   BASIC_INFO_ID," +
                                                    cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                            FROM      TRN_SALARY_DETAILS
                                            GROUP BY BASIC_INFO_ID) AS SALARY
                                ON         SALARY.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                          TRN_FILE_GENERATION_LOG.VALIDATION_STATUS, 
                                        " + cmnService.J_SQLDBFormat("TRN_FILE_GENERATION_LOG.DATE_TIME_OF_CREATION", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS DATE_TIME_OF_CREATION,                                         
                                          TRN_FILE_GENERATION_LOG.OUTPUT_FILE_PATH,
                                          TRN_FILE_GENERATION_LOG.DATE_TIME_OF_CREATION AS DATE_TIME_OF_CREATION_ACTUAL
                                   FROM TRN_FILE_GENERATION_LOG
                                   INNER JOIN (SELECT BASIC_INFO_ID,
                                                      MAX(FG_LOG_ID) AS FG_LOG_ID
                                               FROM TRN_FILE_GENERATION_LOG
                                               WHERE CORR = 0
                                               GROUP BY BASIC_INFO_ID) AS LAST_FG_LOG
                                          ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID ) AS GENERATION_STATUS
                             ON GENERATION_STATUS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        WHERE " + cmnService.J_SQLDBFormat("CHALLANS.TOTAL_CHALLANS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" > 0 
                        AND   " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (1,2) 
                        AND   TRN_BASIC_INFO.PRN_NO = '' AND TRN_BASIC_INFO.DATE_OF_FILING IS NULL  ) REG_CORR_RETURN ";
                //-- 2022/03/02
                //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                //    strSQL = strSQL + "AND   (DATE_TIME_OF_CREATION IS NULL OR DATEDIFF(day, DATE_TIME_OF_CREATION, getdate()) <= " + NoOfDaysSQL + ") ";
                //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                //    strSQL = strSQL + "AND   (DATE_TIME_OF_CREATION IS NULL OR DATEDIFF('d', DATE_TIME_OF_CREATION," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = strSQL + "WHERE (REG_CORR_RETURN.DATE_TIME_OF_CREATION_ACTUAL IS NULL OR DATEDIFF(day, CONVERT(DATETIME, REG_CORR_RETURN.DATE_TIME_OF_CREATION_ACTUAL, 103), getdate()) <= " + NoOfDaysSQL + ") ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = strSQL + "WHERE (REG_CORR_RETURN.DATE_TIME_OF_CREATION_ACTUAL IS NULL OR DATEDIFF('d', LEFT(" + cmnService.J_SQLDBFormat("REG_CORR_RETURN.DATE_TIME_OF_CREATION_ACTUAL", J_SQLColFormat.DateFormatDDMMYYYY) + @", 10)," + TdsMan.GetServerDate() + @") <= " + NoOfDaysSQL + ") ";
                //--
                if (SearchSQL != "")
                {
                    if (SearchSQL.ToUpper() == "COR")
                        strSQL = strSQL + @" AND REG_CORR_RETURN.RETURN_TYPE = 'C' ";
                    else if (SearchSQL.ToUpper() == "REG")
                        strSQL = strSQL + @" AND REG_CORR_RETURN.RETURN_TYPE = 'R' ";
                    else
                        strSQL = strSQL + " AND REG_CORR_RETURN.COMPANY_TAN + REG_CORR_RETURN.COMPANY_NAME + REG_CORR_RETURN.FA_YEAR + REG_CORR_RETURN.QTR + REG_CORR_RETURN.FORM_NO  LIKE '%" + SearchSQL + @"%'";
                }
                //
                strSQL = strSQL + @" ORDER BY REG_CORR_RETURN.DATE_TIME_OF_CREATION_ACTUAL DESC, 
                                                  REG_CORR_RETURN.FA_YEAR DESC,
                                                  REG_CORR_RETURN.QTR DESC,
                                                  REG_CORR_RETURN.BASIC_INFO_ID DESC ";
                //-----------
                dgvReturnsReadyForFiling.AutoGenerateColumns = true;
                if (dsetGridClone != null) dsetGridClone.Clear();
                dgvReturnsReadyForFiling.DataSource = null;
                dgvReturnsReadyForFiling.Columns.Clear();
                if(dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0 )//--2024/05/21
                    dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvReturnsReadyForFiling, strSQL, strMatrix);
                strSQLReturnsReadyForFiling = strSQL; //-- for export to csv
                dgvReturnsReadyForFiling.ClearSelection();
                //--
                #region CellFormatting()
                dgvReturnsReadyForFiling.AutoGenerateColumns = false;
                DataGridViewButtonColumn BtnDownloadReturn = new DataGridViewButtonColumn();
                {
                    BtnDownloadReturn.HeaderText = "";
                    BtnDownloadReturn.Width = 50;
                    BtnDownloadReturn.Text = "▼";// - Download Return - ";
                    BtnDownloadReturn.Name = "BtnDownloadReturn";
                    BtnDownloadReturn.UseColumnTextForButtonValue = true;
                    //BtnDownloadReturn.ToolTipText = "Download Filed Return";
                    dgvReturnsReadyForFiling.Columns.Add(BtnDownloadReturn);
                }
                #endregion
                //
                lblTotalCountRF.Text = "Total Record Count : " + dgvReturnsReadyForFiling.RowCount.ToString();
                //--
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }

        private void txtSortOnReturnsUnderProcess_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ctMenuReturnsUnderProcess.Show(txtSortOnReturnsUnderProcess, new Point(e.X, e.Y));
                if (strSortReturnsUnderProcess == T_SortOrderReturnsUnderProcess.Last_Worked_On)
                {
                    mnuLastWorkedOnUP.Checked = true;
                    mnuLatestReturnUP.Checked = false;
                    mnuFilingDueDateUP.Checked = false;
                }
                else if (strSortReturnsUnderProcess == T_SortOrderReturnsUnderProcess.Latest_Return)
                {
                    mnuLatestReturnUP.Checked = true;
                    mnuLastWorkedOnUP.Checked = false;
                    mnuFilingDueDateUP.Checked = false;
                }
                else if (strSortReturnsUnderProcess == T_SortOrderReturnsUnderProcess.Filing_Due_Date)
                {
                    mnuFilingDueDateUP.Checked = true;
                    mnuLatestReturnUP.Checked = false;
                    mnuLastWorkedOnUP.Checked = false;
                }
            }
        }
        #endregion


        #region mnuLastWorkedOnUP_Click
        private void mnuLastWorkedOnUP_Click(object sender, EventArgs e)
        {
            strSortReturnsUnderProcess = T_SortOrderReturnsUnderProcess.Last_Worked_On;
            LoadReturnsUnderProcessFirstScreen("", strSortReturnsUnderProcess);
        }
        #endregion


        #region mnuLatestReturnUP_Click
        private void mnuLatestReturnUP_Click(object sender, EventArgs e)
        {
            strSortReturnsUnderProcess = T_SortOrderReturnsUnderProcess.Latest_Return;
            LoadReturnsUnderProcessFirstScreen("", strSortReturnsUnderProcess);
        }
        #endregion


        #region mnuFilingDueDateUP_Click
        private void mnuFilingDueDateUP_Click(object sender, EventArgs e)
        {
            strSortReturnsUnderProcess = T_SortOrderReturnsUnderProcess.Filing_Due_Date;
            LoadReturnsUnderProcessFirstScreen("", strSortReturnsUnderProcess);
        }

        private void lblReturnsReadyForFiling_Click(object sender, EventArgs e)
        {
            tbcTabControl.SelectTab(tbpReturnsReadyForFiling);
            //
            lblReturnsReadyForFiling.Font = new Font(lblReturnsReadyForFiling.Font.Name, lblReturnsReadyForFiling.Font.Size, FontStyle.Bold);// | FontStyle.Underline);
            lblReturnsReadyForFiling.BackColor = Color.FromArgb(32, 94, 187);// Color.Blue;
            lblReturnsReadyForFiling.ForeColor = Color.White;
            //
            lblReturnsUnderProcess.Font = new Font(lblReturnsUnderProcess.Font.Name, lblReturnsUnderProcess.Font.Size, FontStyle.Regular);
            lblReturnsUnderProcess.BackColor = Color.AliceBlue;
            lblReturnsUnderProcess.ForeColor = Color.Black;
            lblFiledReturns.Font = new Font(lblFiledReturns.Font.Name, lblFiledReturns.Font.Size, FontStyle.Regular);
            lblFiledReturns.BackColor = Color.AliceBlue;
            lblFiledReturns.ForeColor = Color.Black;
            lblFilingStatus.Font = new Font(lblFilingStatus.Font.Name, lblFilingStatus.Font.Size, FontStyle.Regular);
            lblFilingStatus.BackColor = Color.AliceBlue;
            lblFilingStatus.ForeColor = Color.Black;
            //
            lnkExport.Visible = true;
        }

        private void lblReturnsUnderProcess_Click(object sender, EventArgs e)
        {
            tbcTabControl.SelectTab(tbpReturnUnderProcess);
            //
            lblReturnsUnderProcess.Font = new Font(lblReturnsUnderProcess.Font.Name, lblReturnsUnderProcess.Font.Size, FontStyle.Bold);//| FontStyle.Underline);
            //lblReturnsUnderProcess.BackColor = Color.AliceBlue;
            //lblReturnsUnderProcess.ForeColor = Color.Black;
            lblReturnsUnderProcess.BackColor = Color.FromArgb(32, 94, 187);// Color.Blue;
            lblReturnsUnderProcess.ForeColor = Color.White;
            //
            lblReturnsReadyForFiling.Font = new Font(lblReturnsReadyForFiling.Font.Name, lblReturnsReadyForFiling.Font.Size, FontStyle.Regular);
            lblReturnsReadyForFiling.BackColor = Color.AliceBlue;
            lblReturnsReadyForFiling.ForeColor = Color.Black;
            lblFiledReturns.Font = new Font(lblFiledReturns.Font.Name, lblFiledReturns.Font.Size, FontStyle.Regular);
            lblFiledReturns.BackColor = Color.AliceBlue;
            lblFiledReturns.ForeColor = Color.Black;
            lblFilingStatus.Font = new Font(lblFilingStatus.Font.Name, lblFilingStatus.Font.Size, FontStyle.Regular);
            lblFilingStatus.BackColor = Color.AliceBlue;
            lblFilingStatus.ForeColor = Color.Black;
            //
            lnkExport.Visible = true;
        }

        private void lblFiledReturns_Click(object sender, EventArgs e)
        {
            tbcTabControl.SelectTab(tbpFiledReturns);
            //
            lblFiledReturns.Font = new Font(lblFiledReturns.Font.Name, lblFiledReturns.Font.Size, FontStyle.Bold);// | FontStyle.Underline);
            lblFiledReturns.BackColor = Color.FromArgb(32, 94, 187);// Color.Blue;
            lblFiledReturns.ForeColor = Color.White;
            //
            lblReturnsUnderProcess.Font = new Font(lblReturnsUnderProcess.Font.Name, lblReturnsUnderProcess.Font.Size, FontStyle.Regular);
            lblReturnsUnderProcess.BackColor = Color.AliceBlue;
            lblReturnsUnderProcess.ForeColor = Color.Black;
            lblReturnsReadyForFiling.Font = new Font(lblReturnsReadyForFiling.Font.Name, lblReturnsReadyForFiling.Font.Size, FontStyle.Regular);
            lblReturnsReadyForFiling.BackColor = Color.AliceBlue;
            lblReturnsReadyForFiling.ForeColor = Color.Black;
            lblFilingStatus.Font = new Font(lblFilingStatus.Font.Name, lblFilingStatus.Font.Size, FontStyle.Regular);
            lblFilingStatus.BackColor = Color.AliceBlue;
            lblFilingStatus.ForeColor = Color.Black;
            //
            lnkExport.Visible = true;
        }
        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }


        #region dgvFiledReturns_DoubleClick
        private void dgvFiledReturns_DoubleClick(object sender, EventArgs e)
        {
            if (dgvFiledReturns.CurrentRow != null)
            {
                if (Convert.ToString(dgvFiledReturns.Rows[dgvFiledReturns.CurrentRow.Index].Cells[1].Value) == "C")
                {
                    //-- 2021/12/03
                    TDSMAN.Classes.TDSMAN.T_pBatchId = Convert.ToInt64(Convert.ToString(dgvFiledReturns.Rows[dgvFiledReturns.CurrentRow.Index].Cells[0].Value));
                    //
                    TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard = true;
                    //
                    cmnService.J_ShowChildForm(new Trn26QCorrectionReturn(), J_Var.frmMain, "Make Corrections");
                }
                else
                {
                    TDSMAN.Classes.TDSMAN.T_pTabFormCaption = Convert.ToString(dgvFiledReturns.Rows[dgvFiledReturns.CurrentRow.Index].Cells[6].Value);
                    //--
                    #region ADD/GET BOOKMARK
                    if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
                    {
                        strQuery = @"FORM_NAME= '" + TDSMAN.Classes.TDSMAN.T_pTabFormCaption + "' ";
                        if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL", strQuery) == true)
                        {
                            //11 12
                            //Inserting the Form BookMarkDetail [T_TransactionMode.UPDATE]
                            intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.UPDATE.ToString(),
                                                                Convert.ToInt32(Convert.ToString(dgvFiledReturns.Rows[dgvFiledReturns.CurrentRow.Index].Cells[16].Value)),
                                                                Convert.ToString(dgvFiledReturns.Rows[dgvFiledReturns.CurrentRow.Index].Cells[5].Value),
                                                                TDSMAN.Classes.TDSMAN.T_pTabFormCaption,
                                                                Convert.ToInt32(Convert.ToString(dgvFiledReturns.Rows[dgvFiledReturns.CurrentRow.Index].Cells[15].Value)),
                                                                TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                                                                out strQuarter,
                                                                out strCompanyName
                                                               );
                        }
                        else
                        {
                            //Inserting the Form BookMarkDetail [T_TransactionMode.INSERT]
                            intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.INSERT.ToString(),
                                                                Convert.ToInt32(Convert.ToString(dgvFiledReturns.Rows[dgvFiledReturns.CurrentRow.Index].Cells[16].Value)),
                                                                Convert.ToString(dgvFiledReturns.Rows[dgvFiledReturns.CurrentRow.Index].Cells[5].Value),
                                                                TDSMAN.Classes.TDSMAN.T_pTabFormCaption,
                                                                Convert.ToInt32(Convert.ToString(dgvFiledReturns.Rows[dgvFiledReturns.CurrentRow.Index].Cells[15].Value)),
                                                                TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                                                                out strQuarter,
                                                                out strCompanyName
                                                               );
                        }
                    }
                    #endregion
                    //--
                    //--
                    if (Convert.ToInt32(Convert.ToString(dgvFiledReturns.Rows[dgvFiledReturns.CurrentRow.Index].Cells[16].Value)) >= T_FinancialYearID.F2026_27ID)
                    {
                        TdsMan.CloseChildForm(new TrnRegularReturn_26_27(0), this);
                        //--
                        cmnService.J_ShowChildForm(new TrnRegularReturn_26_27(0), J_Var.frmMain, "Form " + TDSMAN.Classes.TDSMAN.T_pTabFormCaption);
                    }
                    else
                    {
                        TdsMan.CloseChildForm(new TrnRegularReturn(0), this);
                        //--
                        cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form " + TDSMAN.Classes.TDSMAN.T_pTabFormCaption);
                    }
                }
            }
        }
        #endregion


        #region dgvFiledReturns_CellFormatting
        private void dgvFiledReturns_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvFiledReturns.Columns[e.ColumnIndex].DataPropertyName == "TOTAL_DEDUCTEES")
            {
                if (dgvFiledReturns.Rows[e.RowIndex].Cells["TOTAL_DEDUCTEES"].Value.ToString().Contains("/") == true)
                {
                    DataGridViewCell cell = this.dgvFiledReturns.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //cell.ToolTipText = "Employee(s) in Quarterly Return || Employee(s) in Salary Detail";
                    cell.ToolTipText = "Q4-Salary Details";
                }
            }
        }
        #endregion

        #region lblFilingStatus_Click
        private void lblFilingStatus_Click(object sender, EventArgs e)
        {
            tbcTabControl.SelectTab(tbpFilingStatus);
            //
            lblFilingStatus.Font = new Font(lblFilingStatus.Font.Name, lblFilingStatus.Font.Size, FontStyle.Bold);// | FontStyle.Underline);
            lblFilingStatus.BackColor = Color.FromArgb(32, 94, 187);// Color.Blue;
            lblFilingStatus.ForeColor = Color.White;
            //
            lblReturnsUnderProcess.Font = new Font(lblReturnsUnderProcess.Font.Name, lblReturnsUnderProcess.Font.Size, FontStyle.Regular);
            lblReturnsUnderProcess.BackColor = Color.AliceBlue;
            lblReturnsUnderProcess.ForeColor = Color.Black;
            lblReturnsReadyForFiling.Font = new Font(lblReturnsReadyForFiling.Font.Name, lblReturnsReadyForFiling.Font.Size, FontStyle.Regular);
            lblReturnsReadyForFiling.BackColor = Color.AliceBlue;
            lblReturnsReadyForFiling.ForeColor = Color.Black;
            lblFiledReturns.Font = new Font(lblFiledReturns.Font.Name, lblFiledReturns.Font.Size, FontStyle.Regular);
            lblFiledReturns.BackColor = Color.AliceBlue;
            lblFiledReturns.ForeColor = Color.Black;
            //
            lnkExport.Visible = false;

        }
        #endregion

        #region LoadFiledReturns
        private void LoadFiledReturns(string SearchSQL)
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------------------------------------------------------
                string[,] strMatrix = {{"BASIC_INFO_ID", "0", "", "", "", "F", ""},
                                    {"*", "25", "", "C", "", "", ""},
                                    {"TAN", "95", "", "", "", "", "T"},
                                    {"Company", "240", "", "", "", "", "T"},
                                    {"FA Year", "0", "", "", "", "F", ""},
                                    {"Qtr", "0", "", "", "", "F", ""},
                                    {"Form", "0", "", "", "", "F", ""},
                                    {"FA Year [Qtr-Form]", "100", "", "", "", "", "T"},
                                    {"No. of Challans", "50", "", "R", "", "", "T"},
                                    {"Line Records", "50", "", "R", "", "", "T"},
                                    {"Challans Balance", "100", "0.00", "R", "", "", "T"},
                                    {"Status", "0", "", "", "", "F", ""},
                                    {"Token No.", "100", "", "", "", "", "T"},
                                    {"Receipt No.", "120", "", "", "", "", "T"},
                                    {"Date of Filing", "89", "", "", "", "", "T"},
                                    {"COMPANY_ID", "0", "", "", "", "F", ""},
                                    {"ASST_ID", "0", "", "", "", "F", ""}};
                //--
                string[,] strValidationStatusMatrix = {{cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 1", "F", "Successfully Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 2", "F", "Warning while Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 3", "F", "Error while Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 0", "F", "", "T"}};
                //
                string[,] strCORDEDSALMatrix = {{"cor_hdr_batch.form_no = '24Q' AND cor_hdr_batch.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString)  + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"cor_hdr_batch.form_no <> '24Q' AND cor_hdr_batch.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                //
                string[,] strREGDEDSALMatrix = {{"TRN_BASIC_INFO.form_no = '24Q' AND TRN_BASIC_INFO.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString) + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"TRN_BASIC_INFO.form_no <> '24Q' AND TRN_BASIC_INFO.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                //
                long NoOfDaysSQL = TDSMAN.Classes.TDSMAN.T_FILED_RETURNS_DAYS;
                //-----------------------------------------------------------
                strSQL = @"SELECT * FROM (
                           SELECT COR_HDR_BATCH.BATCH_HEADER_ID AS BASIC_INFO_ID,
                                  'C' AS RETURN_TYPE,
                                  COR_HDR_COMPANY.TAN_NO       AS COMPANY_TAN,
                                  COR_HDR_COMPANY.COMPANY_NAME AS COMPANY_NAME,
                                  MST_ASSESSMENT.FA_YEAR,
                                  COR_HDR_BATCH.QTR,
                                  COR_HDR_BATCH.FORM_NO," +
                                 //RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + COR_HDR_BATCH.FORM_NO + ']' AS YEAR_QTR_FORM_RETURN,
                                 "IIF(COR_HDR_BATCH.FORM_NO = '24Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '138 (24Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '26Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '140 (26Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '27Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '144 (27Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '27EQ', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '143 (27EQ)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '138', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '138 (24Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '140', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '140 (26Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '144', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '144 (27Q)' + ']', " +
                                 "IIF(COR_HDR_BATCH.FORM_NO = '143', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + COR_HDR_BATCH.QTR + '-' + '143 (27EQ)' + ']', " +
                                 "'')))))))) AS YEAR_QTR_FORM_RETURN," +
                                 @"CHALLANS.TOTAL_CHALLANS, " +
                                  cmnService.J_SQLDBFormat(strCORDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS TOTAL_DEDUCTEES,
                                  TOT_TAX - TAX_DEPOSITED_AMOUNT AS CHALLAN_BALANCE,
                                  " + cmnService.J_SQLDBFormat(strValidationStatusMatrix, J_SQLColFormat.Case_End) + @" AS VALIDATION_STATUS,
                                  COR_HDR_BATCH.PRN_NO     AS PRN_NO,
                                  COR_HDR_BATCH.RECIEPT_NO AS RECEIPT_NO,
                                  COR_HDR_BATCH.DATE_OF_FILING,
                                  COR_HDR_COMPANY.HDR_COMPANY_ID AS COMPANY_ID,
                                  COR_HDR_BATCH.ASST_ID
                        FROM ((((((COR_HDR_BATCH
                        INNER JOIN COR_HDR_COMPANY 
                              ON COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID )
                        INNER JOIN MST_ASSESSMENT 
                              ON MST_ASSESSMENT.ASST_ID = COR_HDR_BATCH.ASST_ID)
                        LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                          TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
                                   FROM TRN_FILE_GENERATION_LOG
                                   INNER JOIN (SELECT BASIC_INFO_ID,
                                                      MAX(FG_LOG_ID) AS FG_LOG_ID
                                               FROM TRN_FILE_GENERATION_LOG
                                               WHERE CORR = 1
                                               GROUP BY BASIC_INFO_ID) AS LAST_FG_LOG
                                          ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID ) AS GENERATION_STATUS
                             ON GENERATION_STATUS.BASIC_INFO_ID = COR_HDR_BATCH.BATCH_HEADER_ID)

                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID,
                                                         COUNT(*)     AS TOTAL_CHALLANS,
                                                         SUM(TOT_TAX) - SUM(INTEREST) AS TOT_TAX
                                                FROM     COR_TRN_CHALLAN
                                                GROUP BY BATCH_HEADER_ID) AS CHALLANS
                                    ON         CHALLANS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                         cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                     cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                                FROM     COR_TRN_DEDUCTEE_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS DEDUCTEES
                                    ON         DEDUCTEES.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)

                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                          cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                                FROM     COR_TRN_SALARY_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS SALARY
                                    ON         SALARY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                        WHERE " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (1,2)
                        AND   (COR_HDR_BATCH.RECIEPT_NO <> '' OR COR_HDR_BATCH.PRN_NO <> '')";// AND COR_HDR_BATCH.DATE_OF_FILING  IS NOT NULL ";
                //-- 2022/03/02
                //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                //    strSQL = strSQL + " AND   (DATE_OF_FILING IS NULL OR DATEDIFF(day, CONVERT(DATETIME, DATE_OF_FILING, 103), getdate()) <= " + NoOfDaysSQL + ") ";
                //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                //    strSQL = strSQL + " AND   (DATE_OF_FILING IS NULL OR DATEDIFF('d', DATE_OF_FILING," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
                //--
                //if (SearchSQL != "")
                //    strSQL = strSQL + " AND COR_HDR_COMPANY.TAN_NO + COR_HDR_COMPANY.COMPANY_NAME + MST_ASSESSMENT.FA_YEAR + COR_HDR_BATCH.QTR + COR_HDR_BATCH.FORM_NO LIKE '%" + SearchSQL + @"%' ";
                strSQL = strSQL + @"
                        UNION
                        SELECT TRN_BASIC_INFO.BASIC_INFO_ID,
                               'R' AS RETURN_TYPE,     
                               MST_COMPANY.TAN_NO       AS COMPANY_TAN,
                               MST_COMPANY.COMPANY_NAME AS COMPANY_NAME,
                               MST_ASSESSMENT.FA_YEAR,
                               TRN_BASIC_INFO.QTR,
                               TRN_BASIC_INFO.FORM_NO," +
                              //RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + TRN_BASIC_INFO.FORM_NO + ']' AS YEAR_QTR_FORM_RETURN,
                              "IIF(TRN_BASIC_INFO.FORM_NO = '24Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '138 (24Q)' + ']', " +
                                 "IIF(TRN_BASIC_INFO.FORM_NO = '26Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '140 (26Q)' + ']', " +
                                 "IIF(TRN_BASIC_INFO.FORM_NO = '27Q', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '144 (27Q)' + ']', " +
                                 "IIF(TRN_BASIC_INFO.FORM_NO = '27EQ', RIGHT(MST_ASSESSMENT.FA_YEAR,5) + ' [' + TRN_BASIC_INFO.QTR + '-' + '143 (27EQ)' + ']', '')))) AS YEAR_QTR_FORM_RETURN," +
                              @"CHALLANS.TOTAL_CHALLANS,
                              " + cmnService.J_SQLDBFormat(strREGDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS TOTAL_DEDUCTEES,
                              TOT_TAX - TAX_DEPOSITED_AMOUNT AS CHALLAN_BALANCE," +
                               "" + cmnService.J_SQLDBFormat(strValidationStatusMatrix, J_SQLColFormat.Case_End) + @" AS VALIDATION_STATUS,
                               TRN_BASIC_INFO.PRN_NO     AS PRN_NO,
                               TRN_BASIC_INFO.RECEIPT_NO AS RECEIPT_NO,
                               TRN_BASIC_INFO.DATE_OF_FILING,
                               MST_COMPANY.COMPANY_ID AS COMPANY_ID,
                               TRN_BASIC_INFO.ASST_ID
                        FROM ((((((TRN_BASIC_INFO
                        INNER JOIN MST_COMPANY 
                              ON MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID)
                        INNER JOIN MST_ASSESSMENT 
                              ON MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID)
                        LEFT JOIN (SELECT BASIC_INFO_ID,
                                          COUNT(*) AS TOTAL_CHALLANS,
                                          SUM(TOT_TAX) - SUM(INTEREST) AS TOT_TAX
                                   FROM TRN_CHALLAN
                                   GROUP BY BASIC_INFO_ID) AS CHALLANS
                             ON CHALLANS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        LEFT JOIN
                                    (SELECT   BASIC_INFO_ID," +
                                                cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                    FROM     TRN_DEDUCTEE_DETAILS
                                    GROUP BY BASIC_INFO_ID) AS DEDUCTEES
                        ON         DEDUCTEES.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)

                        LEFT JOIN
                                    (SELECT   BASIC_INFO_ID," +
                                            cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                    FROM      TRN_SALARY_DETAILS
                                    GROUP BY BASIC_INFO_ID) AS SALARY
                        ON         SALARY.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                          TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
                                   FROM TRN_FILE_GENERATION_LOG
                                   INNER JOIN (SELECT BASIC_INFO_ID,
                                                      MAX(FG_LOG_ID) AS FG_LOG_ID
                                               FROM TRN_FILE_GENERATION_LOG
                                               WHERE CORR = 0
                                               GROUP BY BASIC_INFO_ID) AS LAST_FG_LOG
                                          ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID ) AS GENERATION_STATUS
                             ON GENERATION_STATUS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        WHERE " + cmnService.J_SQLDBFormat("CHALLANS.TOTAL_CHALLANS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" > 0 
                        AND   " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (1,2)
                        AND   (TRN_BASIC_INFO.RECEIPT_NO <> '' OR TRN_BASIC_INFO.PRN_NO <> ''  ) ) REG_CORR_RETURN ";// AND TRN_BASIC_INFO.DATE_OF_FILING  IS NOT NULL ";
                //-- 2022/03/02
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = strSQL + " WHERE   (REG_CORR_RETURN.DATE_OF_FILING IS NULL OR DATEDIFF(day, CONVERT(DATETIME, REG_CORR_RETURN.DATE_OF_FILING, 103), getdate()) <= " + NoOfDaysSQL + ") ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = strSQL + " WHERE   (REG_CORR_RETURN.DATE_OF_FILING IS NULL OR DATEDIFF('d', REG_CORR_RETURN.DATE_OF_FILING," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
                //
                if (SearchSQL != "")
                {
                    if (SearchSQL.ToUpper() == "COR")
                        strSQL = strSQL + @" AND REG_CORR_RETURN.RETURN_TYPE = 'C' ";
                    else if (SearchSQL.ToUpper() == "REG")
                        strSQL = strSQL + @" AND REG_CORR_RETURN.RETURN_TYPE = 'R' ";
                    else
                        strSQL = strSQL + " AND REG_CORR_RETURN.COMPANY_TAN + REG_CORR_RETURN.COMPANY_NAME + REG_CORR_RETURN.FA_YEAR + REG_CORR_RETURN.QTR + REG_CORR_RETURN.FORM_NO  LIKE '%" + SearchSQL + @"%'";
                }
                //-----------
                if (dsetGridClone != null) dsetGridClone.Clear();
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)//--2024/05/21
                    dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvFiledReturns, strSQL, strMatrix);
                strSQLFiledReturns = strSQL; //-- for export to csv
                dgvFiledReturns.ClearSelection();
                //
                lblTotalCountFR.Text = "Total Record Count : " + dgvFiledReturns.RowCount.ToString();
                //--
                #region CellFormatting(SHOW_BUTTON)

                ////////if (dgvUpdates.Columns[e.ColumnIndex].DataPropertyName == "SHOW_BUTTON")
                ////////{
                //////DataGridViewButtonColumn BtnShow = new DataGridViewButtonColumn();
                //////dgvFiledReturns.Columns.Add(BtnShow);
                ////////dgvUpdates.Columns
                //////BtnShow.Visible = true;
                //////BtnShow.HeaderText = "";
                //////BtnShow.Text = "Show";
                //////BtnShow.ToolTipText = "Go to the Return";
                //////BtnShow.Name = "BtnShow";
                //////BtnShow.UseColumnTextForButtonValue = true;
                ////////}
                #endregion
                //-
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region cmbFinancialYear_SelectedIndexChanged
        private void cmbFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blExit == true) return;
            LoadFilingStatus("");
        }
        #endregion


        #region BgBackGroundWorker_DoWork
        private void BgBackGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (TdsMan.UpdateReturnLastDate() == false)
            //{ }
        }

        private void BgBackGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (TdsMan.UpdateReturnLastDate() == false)
            //{ }
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0077", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region dgvFilingStatus_CellFormatting
        private void dgvFilingStatus_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvFilingStatus.Columns[e.ColumnIndex].DataPropertyName == "Q1_STATUS_DISPLAY")
            {
                if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q1_STATUS_VALUE"].Value.ToString().ToUpper() == "1")
                {
                    e.CellStyle.BackColor = Color.Green;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Generated Successfully";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q1_STATUS_VALUE"].Value.ToString().ToUpper() == "2")
                {
                    e.CellStyle.BackColor = Color.Green;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Generated with Warning";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q1_STATUS_VALUE"].Value.ToString().ToUpper() == "3")
                {
                    //e.CellStyle.BackColor = Color.Red;
                    ////
                    //DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //cell.ToolTipText = "Error during Generation";
                    e.CellStyle.BackColor = Color.Gray;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Not Generated";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q1_STATUS_VALUE"].Value.ToString().ToUpper() == "4")
                {
                    e.CellStyle.BackColor = Color.Gray;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Not Generated";
                }
            }
            else if (dgvFilingStatus.Columns[e.ColumnIndex].DataPropertyName == "Q2_STATUS_DISPLAY")
            {
                if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q2_STATUS_VALUE"].Value.ToString().ToUpper() == "1")
                {
                    e.CellStyle.BackColor = Color.Green;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Generated Successfully";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q2_STATUS_VALUE"].Value.ToString().ToUpper() == "2")
                {
                    e.CellStyle.BackColor = Color.Green;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Generated with Warning";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q2_STATUS_VALUE"].Value.ToString().ToUpper() == "3")
                {
                    //e.CellStyle.BackColor = Color.Red;
                    ////
                    //DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //cell.ToolTipText = "Error during Generation";
                    e.CellStyle.BackColor = Color.Gray;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Not Generated";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q2_STATUS_VALUE"].Value.ToString().ToUpper() == "4")
                {
                    e.CellStyle.BackColor = Color.Gray;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Not Generated";
                }
            }
            else if (dgvFilingStatus.Columns[e.ColumnIndex].DataPropertyName == "Q3_STATUS_DISPLAY")
            {
                if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q3_STATUS_VALUE"].Value.ToString().ToUpper() == "1")
                {
                    e.CellStyle.BackColor = Color.Green;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Generated Successfully";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q3_STATUS_VALUE"].Value.ToString().ToUpper() == "2")
                {
                    e.CellStyle.BackColor = Color.Green;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Generated with Warning";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q3_STATUS_VALUE"].Value.ToString().ToUpper() == "3")
                {
                    //e.CellStyle.BackColor = Color.Red;
                    ////
                    //DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //cell.ToolTipText = "Error during Generation";
                    e.CellStyle.BackColor = Color.Gray;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Not Generated";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q3_STATUS_VALUE"].Value.ToString().ToUpper() == "4")
                {
                    e.CellStyle.BackColor = Color.Gray;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Not Generated";
                }
            }
            else if (dgvFilingStatus.Columns[e.ColumnIndex].DataPropertyName == "Q4_STATUS_DISPLAY")
            {
                if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q4_STATUS_VALUE"].Value.ToString().ToUpper() == "1")
                {
                    e.CellStyle.BackColor = Color.Green;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Generated Successfully";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q4_STATUS_VALUE"].Value.ToString().ToUpper() == "2")
                {
                    e.CellStyle.BackColor = Color.Green;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Generated with Warning";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q4_STATUS_VALUE"].Value.ToString().ToUpper() == "3")
                {
                    //e.CellStyle.BackColor = Color.Red;
                    ////
                    //DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //cell.ToolTipText = "Error during Generation";
                    e.CellStyle.BackColor = Color.Gray;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Not Generated";
                }
                else if (dgvFilingStatus.Rows[e.RowIndex].Cells["Q4_STATUS_VALUE"].Value.ToString().ToUpper() == "4")
                {
                    e.CellStyle.BackColor = Color.Gray;
                    //
                    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Return Not Generated";
                }
            }
            //else if (dgvFilingStatus.Columns[e.ColumnIndex].DataPropertyName == "UPDATE_RECEIPT_NO")
            //{
            //    e.CellStyle.ForeColor = Color.Blue;
            //}
            //else if (dgvFilingStatus.Columns[e.ColumnIndex].DataPropertyName == "")
            //{
            //    DataGridViewCell cell = this.dgvFilingStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //    cell.ToolTipText = "Download Filed Return";
            //}
        }
        #endregion


        #region lnkExport_LinkClicked
        private void lnkExport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try {
                string strPath = "", strFileName = "", SortSQL="";
                //--
                #region ReturnUnderProcess
                if (tbcTabControl.SelectedTab == tbpReturnUnderProcess)
                {
                    if (dgvReturnsUnderProcessFirstScreen.RowCount <= 0)
                    {
                        cmnService.J_UserMessage("No record exists for Export !!");
                        return;
                    }
                    //
                    #region QUERY
                    string[,] strCORDEDSALMatrix = {{"cor_hdr_batch.form_no = '24Q' AND cor_hdr_batch.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString)  + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"cor_hdr_batch.form_no <> '24Q' AND cor_hdr_batch.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                    //
                    string[,] strREGDEDSALMatrix = {{"TRN_BASIC_INFO.form_no = '24Q' AND TRN_BASIC_INFO.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString) + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"TRN_BASIC_INFO.form_no <> '24Q' AND TRN_BASIC_INFO.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                    //
                    //NoOfRecordsSQL = Convert.ToInt32(txtNoOfRecords.Text);
                    long NoOfDaysSQL = TDSMAN.Classes.TDSMAN.T_RETURNS_UNDER_PROCESS_DAYS; //Convert.ToInt32(txtNoOfDays.Text);
                                                                                           //-----------------------------------------------------------
                                                                                           //strSQLReturnsReadyForFiling = @"SELECT TOP " + NoOfRecordsSQL + @" * FROM(
                    strSQLReturnUnderProcess = @"SELECT  * FROM(
                        SELECT    'C'                          AS [RETURN TYPE],
                                  COR_HDR_COMPANY.TAN_NO       AS [COMPANY TAN],
                                  COR_HDR_COMPANY.COMPANY_NAME AS [COMPANY NAME],
                                  MST_ASSESSMENT.FA_YEAR       AS [FA YEAR],
                                  COR_HDR_BATCH.QTR            AS [QTR],
                                  COR_HDR_BATCH.FORM_NO        AS [FORM],
                                  CHALLANS.TOTAL_CHALLANS      AS [TOTAL CHALLAN],
                                  " + cmnService.J_SQLDBFormat(strCORDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS [TOTAL DEDUCTEES],
                                  TOT_TAX - TAX_DEPOSITED_AMOUNT AS [CHALLAN BALANCE],
                                  'N.A.'         AS [RETURN LAST DATE]
                           FROM   ((((((COR_HDR_BATCH
                                     INNER JOIN COR_HDR_COMPANY
                                             ON COR_HDR_COMPANY.BATCH_HEADER_ID =
                                                COR_HDR_BATCH.BATCH_HEADER_ID )
                                    INNER JOIN MST_ASSESSMENT
                                            ON MST_ASSESSMENT.ASST_ID = COR_HDR_BATCH.ASST_ID)
                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID,
                                                         COUNT(*)     AS TOTAL_CHALLANS,
                                                         SUM(TOT_TAX) AS TOT_TAX
                                                FROM     COR_TRN_CHALLAN
                                                GROUP BY BATCH_HEADER_ID) AS CHALLANS
                                    ON         CHALLANS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                             cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                             cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                                FROM     COR_TRN_DEDUCTEE_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS DEDUCTEES
                                    ON         DEDUCTEES.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)

                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                              cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                                FROM     COR_TRN_SALARY_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS SALARY
                                    ON         SALARY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)

                                   LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                                     TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
                                              FROM   TRN_FILE_GENERATION_LOG
                                                     INNER JOIN (SELECT BASIC_INFO_ID,
                                                                        MAX(FG_LOG_ID) AS FG_LOG_ID
                                                                 FROM   TRN_FILE_GENERATION_LOG
                                                                 WHERE  CORR = 1
                                                                 GROUP  BY BASIC_INFO_ID) AS
                                                                LAST_FG_LOG
                                                             ON LAST_FG_LOG.FG_LOG_ID =
                                                                TRN_FILE_GENERATION_LOG.FG_LOG_ID)
                                             AS GENERATION_STATUS
                                          ON GENERATION_STATUS.BASIC_INFO_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                        WHERE " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (0,3) ";
                    //if (SearchSQL != "")
                    //    strSQLReturnsReadyForFiling = strSQLReturnsReadyForFiling + "AND COR_HDR_COMPANY.TAN_NO + COR_HDR_COMPANY.COMPANY_NAME + MST_ASSESSMENT.FA_YEAR + COR_HDR_BATCH.QTR + COR_HDR_BATCH.FORM_NO LIKE '%" + SearchSQL + @"%' ";
                    strSQLReturnUnderProcess = strSQLReturnUnderProcess + @" UNION
                        SELECT 'R'                     AS [RETURN TYPE],
                              MST_COMPANY.TAN_NO       AS [COMPANY TAN],
                              MST_COMPANY.COMPANY_NAME AS [COMPANY NAME],
                              MST_ASSESSMENT.FA_YEAR   AS [FA YEAR],
                              TRN_BASIC_INFO.QTR       AS [QTR],
                              TRN_BASIC_INFO.FORM_NO   AS [FORM],
                              CHALLANS.TOTAL_CHALLANS  AS [TOTAL CHALLAN],
                              " + cmnService.J_SQLDBFormat(strREGDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS [TOTAL DEDUCTEES],
                              TOT_TAX - TAX_DEPOSITED_AMOUNT AS [CHALLAN BALANCE],
                              " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.RETURN_LAST_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS [RETURN LAST DATE]
                       FROM   ((((((TRN_BASIC_INFO
                                  INNER JOIN MST_COMPANY
                                          ON MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID)
                                 INNER JOIN MST_ASSESSMENT
                                         ON MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID)
                                LEFT JOIN (SELECT BASIC_INFO_ID,
                                                  COUNT(*) AS TOTAL_CHALLANS,
                                                  SUM(TOT_TAX) AS TOT_TAX
                                           FROM   TRN_CHALLAN
                                           GROUP  BY BASIC_INFO_ID) AS CHALLANS
                                       ON CHALLANS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                                LEFT JOIN
                                           (SELECT   BASIC_INFO_ID," +
                                                         cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                         cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                            FROM     TRN_DEDUCTEE_DETAILS
                                            GROUP BY BASIC_INFO_ID) AS DEDUCTEES
                                ON         DEDUCTEES.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)

                                LEFT JOIN
                                            (SELECT   BASIC_INFO_ID," +
                                                        cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                            FROM      TRN_SALARY_DETAILS
                                            GROUP BY BASIC_INFO_ID) AS SALARY
                                ON         SALARY.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)

                               LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                                 TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
                                          FROM   TRN_FILE_GENERATION_LOG
                                                 INNER JOIN (SELECT BASIC_INFO_ID,
                                                                    MAX(FG_LOG_ID) AS FG_LOG_ID
                                                             FROM   TRN_FILE_GENERATION_LOG
                                                             WHERE  CORR = 0
                                                             GROUP  BY BASIC_INFO_ID) AS LAST_FG_LOG
                                                         ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID)
                                         AS  GENERATION_STATUS
                                      ON GENERATION_STATUS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        WHERE " + cmnService.J_SQLDBFormat("CHALLANS.TOTAL_CHALLANS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" > 0 
                        AND   " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (0,3) ) REG_CORR_RETURN ";
                    //-- 2021/11/23
                    //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    //    strSQLReturnUnderProcess = strSQLReturnUnderProcess + "WHERE   (REG_CORR_RETURN.LAST_UPDATE_DATETIME IS NULL OR DATEDIFF(day, REG_CORR_RETURN.LAST_UPDATE_DATETIME, getdate()) <= " + NoOfDaysSQL + ") ";
                    //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    //    strSQLReturnUnderProcess = strSQLReturnUnderProcess + "WHERE   (REG_CORR_RETURN.LAST_UPDATE_DATETIME IS NULL OR DATEDIFF('d', REG_CORR_RETURN.LAST_UPDATE_DATETIME," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
                    //--
                    //if (SearchSQL != "")
                    //{
                    //    if (SearchSQL.ToUpper() == "COR")
                    //        strSQLReturnUnderProcess = strSQLReturnUnderProcess + @" AND REG_CORR_RETURN.RETURN_TYPE = 'C' ";
                    //    else if (SearchSQL.ToUpper() == "REG")
                    //        strSQLReturnUnderProcess = strSQLReturnUnderProcess + @" AND REG_CORR_RETURN.RETURN_TYPE = 'R' ";
                    //    else
                    //        strSQLReturnUnderProcess = strSQLReturnUnderProcess + @" AND REG_CORR_RETURN.RETURN_TYPE + 
                    //                             REG_CORR_RETURN.COMPANY_TAN + 
                    //                             REG_CORR_RETURN.COMPANY_NAME + 
                    //                             REG_CORR_RETURN.YEAR_QTR_FORM_RETURN + " +
                    //                                 cmnService.J_SQLDBFormat("REG_CORR_RETURN.TOTAL_CHALLANS", J_SQLColFormat.ConvertToString) + " + " +
                    //                                 cmnService.J_SQLDBFormat("REG_CORR_RETURN.TOTAL_DEDUCTEES ", J_SQLColFormat.ConvertToString) + " + " +
                    //                                 cmnService.J_SQLDBFormat("REG_CORR_RETURN.CHALLAN_BALANCE ", J_SQLColFormat.ConvertToString) + @" +
                    //                             REG_CORR_RETURN.RETURN_LAST_DATE LIKE '%" + SearchSQL.Replace("[", "[[]") + @"%' ";
                    //}
                    //
                    if (SortSQL == T_SortOrderReturnsUnderProcess.Last_Worked_On)
                        strSQLReturnUnderProcess = strSQLReturnUnderProcess + @" ORDER BY 
                                                  REG_CORR_RETURN.FA_YEAR DESC, 
                                                  REG_CORR_RETURN.QTR DESC, 
                                                  REG_CORR_RETURN.BASIC_INFO_ID DESC";
                    else if (SortSQL == T_SortOrderReturnsUnderProcess.Latest_Return)
                        strSQLReturnUnderProcess = strSQLReturnUnderProcess + @" ORDER BY REG_CORR_RETURN.ASST_ID DESC, 
                                                  REG_CORR_RETURN.QTR DESC, 
                                                  REG_CORR_RETURN.BASIC_INFO_ID DESC, REG_CORR_RETURN.COMPANY_NAME";
                    else if (SortSQL == T_SortOrderReturnsUnderProcess.Filing_Due_Date)
                        strSQLReturnUnderProcess = strSQLReturnUnderProcess + @" ORDER BY REG_CORR_RETURN.FA_YEAR DESC, 
                                                  REG_CORR_RETURN.QTR DESC, 
                                                  REG_CORR_RETURN.BASIC_INFO_ID DESC";
                    //-----------

                    #endregion
                    // Create a new instance of FolderBrowserDialog.
                    FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                    // A new folder button will display in FolderBrowserDialog.
                    folderBrowserDlg.ShowNewFolderButton = true;
                    //Show FolderBrowserDialog
                    DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                    if (dlgResult.Equals(DialogResult.OK))
                    {
                        //Show selected folder path in textbox1.
                        strPath = folderBrowserDlg.SelectedPath;
                        //Browsing start from root folder.
                        Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                    }
                    else
                        return;
                    //--
                    strFileName = "ReturnUnderProcess_" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".csv";
                    ExportToCSV(strSQLReturnUnderProcess, Path.Combine(strPath, strFileName));
                }
                #endregion

                #region ReturnsReadyForFiling
                else if (tbcTabControl.SelectedTab == tbpReturnsReadyForFiling)
                {
                    if (dgvReturnsReadyForFiling.RowCount <= 0)
                    {
                        cmnService.J_UserMessage("No record exists for Export !!");
                        return;
                    }
                    #region QUERY
                    string[,] strValidationStatusMatrix = {{cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 1", "F", "Successfully Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 2", "F", "Warning while Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 3", "F", "Error while Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 0", "F", "", "T"}};
                    //
                    string[,] strCORDEDSALMatrix = {{"cor_hdr_batch.form_no = '24Q' AND cor_hdr_batch.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString)  + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"cor_hdr_batch.form_no <> '24Q' AND cor_hdr_batch.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                    //
                    string[,] strREGDEDSALMatrix = {{"TRN_BASIC_INFO.form_no = '24Q' AND TRN_BASIC_INFO.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString) + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"TRN_BASIC_INFO.form_no <> '24Q' AND TRN_BASIC_INFO.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                    //
                    long NoOfDaysSQL = TDSMAN.Classes.TDSMAN.T_RETURNS_READY_FOR_FILING_DAYS;
                    //-----------------------------------------------------------
                    strSQLReturnsReadyForFiling = @"SELECT  * FROM(
                         SELECT 'C' AS [RETURN TYPE],
                               COR_HDR_COMPANY.TAN_NO       AS [COMPANY TAN],
                               COR_HDR_COMPANY.COMPANY_NAME AS [COMPANY NAME],
                               MST_ASSESSMENT.FA_YEAR       AS [FA YEAR],
                               COR_HDR_BATCH.QTR            AS [QTR],
                               COR_HDR_BATCH.FORM_NO        AS [FORM],
                               CHALLANS.TOTAL_CHALLANS      AS [TOTAL CHALLAN],
                               " + cmnService.J_SQLDBFormat(strCORDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS [TOTAL DEDUCTEES],
                               TOT_TAX - TAX_DEPOSITED_AMOUNT AS [CHALLAN BALANCE],
                               GENERATION_STATUS.DATE_TIME_OF_CREATION AS [DATE TIME OF CREATION],
                               GENERATION_STATUS.OUTPUT_FILE_PATH      AS [OUTPUT FILE PATH]
                        FROM ((((((COR_HDR_BATCH
                        INNER JOIN COR_HDR_COMPANY 
                              ON COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID )
                        INNER JOIN MST_ASSESSMENT 
                              ON MST_ASSESSMENT.ASST_ID = COR_HDR_BATCH.ASST_ID)
                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID,
                                                         COUNT(*)     AS TOTAL_CHALLANS,
                                                         SUM(TOT_TAX) AS TOT_TAX
                                                FROM     COR_TRN_CHALLAN
                                                GROUP BY BATCH_HEADER_ID) AS CHALLANS
                                    ON         CHALLANS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                             cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                         cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                                FROM     COR_TRN_DEDUCTEE_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS DEDUCTEES
                                    ON         DEDUCTEES.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)

                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                              cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                                FROM     COR_TRN_SALARY_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS SALARY
                                    ON         SALARY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                                   LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                          TRN_FILE_GENERATION_LOG.VALIDATION_STATUS, 
                                         " + cmnService.J_SQLDBFormat("TRN_FILE_GENERATION_LOG.DATE_TIME_OF_CREATION", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS DATE_TIME_OF_CREATION,
                                          TRN_FILE_GENERATION_LOG.OUTPUT_FILE_PATH,
                                          TRN_FILE_GENERATION_LOG.DATE_TIME_OF_CREATION AS DATE_TIME_OF_CREATION_ACTUAL
                                   FROM TRN_FILE_GENERATION_LOG
                                   INNER JOIN (SELECT BASIC_INFO_ID,
                                                      MAX(FG_LOG_ID) AS FG_LOG_ID
                                               FROM TRN_FILE_GENERATION_LOG
                                               WHERE CORR = 1
                                               GROUP BY BASIC_INFO_ID) AS LAST_FG_LOG
                                          ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID ) AS GENERATION_STATUS
                             ON GENERATION_STATUS.BASIC_INFO_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                        WHERE " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (1,2)
                        AND   COR_HDR_BATCH.PRN_NO = '' AND COR_HDR_BATCH.DATE_OF_FILING IS NULL ";
                    //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    //    strSQLReturnsReadyForFiling = strSQLReturnsReadyForFiling + "AND   (DATEDIFF(day, CONVERT(DATETIME, date_time_of_creation, 103), getdate()) <= " + NoOfDaysSQL + ") ";
                    //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    //    strSQLReturnsReadyForFiling = strSQLReturnsReadyForFiling + "AND   (DATEDIFF('d', DATE_TIME_OF_CREATION," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
                    //--
                    //if (SearchSQL != "")
                    //    strSQLReturnsReadyForFiling = strSQLReturnsReadyForFiling + " AND COR_HDR_COMPANY.TAN_NO + COR_HDR_COMPANY.COMPANY_NAME + MST_ASSESSMENT.FA_YEAR + COR_HDR_BATCH.QTR + COR_HDR_BATCH.FORM_NO LIKE '%" + SearchSQL + @"%' ";
                    strSQLReturnsReadyForFiling = strSQLReturnsReadyForFiling + @"
                        UNION
                        SELECT 'R' AS [RETURN TYPE],    
                               MST_COMPANY.TAN_NO       AS [COMPANY TAN],
                               MST_COMPANY.COMPANY_NAME AS [COMPANY NAME],
                               MST_ASSESSMENT.FA_YEAR   AS [FA YEAR],
                               TRN_BASIC_INFO.QTR       AS [QTR],
                               TRN_BASIC_INFO.FORM_NO   AS [FORM],
                              CHALLANS.TOTAL_CHALLANS   AS [TOTAL CHALLAN],
                              " + cmnService.J_SQLDBFormat(strREGDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS [TOTAL DEDUCTEES],
                               TOT_TAX - TAX_DEPOSITED_AMOUNT AS [CHALLAN BALANCE],
                               GENERATION_STATUS.DATE_TIME_OF_CREATION AS [DATE TIME OF CREATION],
                               GENERATION_STATUS.OUTPUT_FILE_PATH AS [OUTPUT FILE PATH]
                        FROM ((((((TRN_BASIC_INFO
                        INNER JOIN MST_COMPANY 
                              ON MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID)
                        INNER JOIN MST_ASSESSMENT 
                              ON MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID)
                                LEFT JOIN (SELECT BASIC_INFO_ID,
                                                  COUNT(*) AS TOTAL_CHALLANS,
                                                  SUM(TOT_TAX) AS TOT_TAX
                                           FROM   TRN_CHALLAN
                                           GROUP  BY BASIC_INFO_ID) AS CHALLANS
                                       ON CHALLANS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                                LEFT JOIN
                                           (SELECT   BASIC_INFO_ID," +
                                                         cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                         cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                            FROM     TRN_DEDUCTEE_DETAILS
                                            GROUP BY BASIC_INFO_ID) AS DEDUCTEES
                                ON         DEDUCTEES.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)

                                LEFT JOIN
                                            (SELECT   BASIC_INFO_ID," +
                                                        cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                            FROM      TRN_SALARY_DETAILS
                                            GROUP BY BASIC_INFO_ID) AS SALARY
                                ON         SALARY.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                          TRN_FILE_GENERATION_LOG.VALIDATION_STATUS, 
                                        " + cmnService.J_SQLDBFormat("TRN_FILE_GENERATION_LOG.DATE_TIME_OF_CREATION", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS DATE_TIME_OF_CREATION,                                         
                                          TRN_FILE_GENERATION_LOG.OUTPUT_FILE_PATH,
                                          TRN_FILE_GENERATION_LOG.DATE_TIME_OF_CREATION AS DATE_TIME_OF_CREATION_ACTUAL
                                   FROM TRN_FILE_GENERATION_LOG
                                   INNER JOIN (SELECT BASIC_INFO_ID,
                                                      MAX(FG_LOG_ID) AS FG_LOG_ID
                                               FROM TRN_FILE_GENERATION_LOG
                                               WHERE CORR = 0
                                               GROUP BY BASIC_INFO_ID) AS LAST_FG_LOG
                                          ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID ) AS GENERATION_STATUS
                             ON GENERATION_STATUS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        WHERE " + cmnService.J_SQLDBFormat("CHALLANS.TOTAL_CHALLANS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" > 0 
                        AND   " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (1,2) 
                        AND   TRN_BASIC_INFO.PRN_NO = '' AND TRN_BASIC_INFO.DATE_OF_FILING IS NULL  ) REG_CORR_RETURN ";
                    //-- 2022/03/02
                    //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    //    strSQLReturnsReadyForFiling = strSQLReturnsReadyForFiling + "AND   (DATE_TIME_OF_CREATION IS NULL OR DATEDIFF(day, DATE_TIME_OF_CREATION, getdate()) <= " + NoOfDaysSQL + ") ";
                    //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    //    strSQLReturnsReadyForFiling = strSQLReturnsReadyForFiling + "AND   (DATE_TIME_OF_CREATION IS NULL OR DATEDIFF('d', DATE_TIME_OF_CREATION," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
                    //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    //    strSQLReturnsReadyForFiling = strSQLReturnsReadyForFiling + "WHERE (REG_CORR_RETURN.DATE_TIME_OF_CREATION_ACTUAL IS NULL OR DATEDIFF(day, CONVERT(DATETIME, REG_CORR_RETURN.DATE_TIME_OF_CREATION_ACTUAL, 103), getdate()) <= " + NoOfDaysSQL + ") ";
                    //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    //    strSQLReturnsReadyForFiling = strSQLReturnsReadyForFiling + "WHERE (REG_CORR_RETURN.DATE_TIME_OF_CREATION_ACTUAL IS NULL OR DATEDIFF('d', REG_CORR_RETURN.DATE_TIME_OF_CREATION_ACTUAL," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
                    //--
                    //
                    strSQLReturnsReadyForFiling = strSQLReturnsReadyForFiling + @" ORDER BY  
                                                  REG_CORR_RETURN.[FA YEAR] DESC,
                                                  REG_CORR_RETURN.QTR DESC";//,
                                                  //REG_CORR_RETURN.BASIC_INFO_ID DESC ";
                    //-----------
                    #endregion
                    // Create a new instance of FolderBrowserDialog.
                    FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                    // A new folder button will display in FolderBrowserDialog.
                    folderBrowserDlg.ShowNewFolderButton = true;
                    //Show FolderBrowserDialog
                    DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                    if (dlgResult.Equals(DialogResult.OK))
                    {
                        //Show selected folder path in textbox1.
                        strPath = folderBrowserDlg.SelectedPath;
                        //Browsing start from root folder.
                        Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                    }
                    else
                        return;
                    //--
                    strFileName = "ReturnsReadyForFiling_" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".csv";
                    ExportToCSV(strSQLReturnsReadyForFiling, Path.Combine(strPath, strFileName));
                }
                #endregion

                #region FiledReturns
                else if (tbcTabControl.SelectedTab == tbpFiledReturns)
                {
                    if (dgvFiledReturns.RowCount <= 0)
                    {
                        cmnService.J_UserMessage("No record exists for Export !!");
                        return;
                    }
                    #region QUERY
                    //--
                    string[,] strValidationStatusMatrix = {{cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 1", "F", "Successfully Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 2", "F", "Warning while Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 3", "F", "Error while Generated", "T"},
                                                        {cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 0", "F", "", "T"}};
                    //
                    string[,] strCORDEDSALMatrix = {{"cor_hdr_batch.form_no = '24Q' AND cor_hdr_batch.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString)  + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"cor_hdr_batch.form_no <> '24Q' AND cor_hdr_batch.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                    //
                    string[,] strREGDEDSALMatrix = {{"TRN_BASIC_INFO.form_no = '24Q' AND TRN_BASIC_INFO.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString) + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                                            {"TRN_BASIC_INFO.form_no <> '24Q' AND TRN_BASIC_INFO.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                    //
                    long NoOfDaysSQL = TDSMAN.Classes.TDSMAN.T_FILED_RETURNS_DAYS;
                    //-----------------------------------------------------------
                    strSQLFiledReturns = @"SELECT * FROM (
                           SELECT 'C' AS [RETURN TYPE],
                                  COR_HDR_COMPANY.TAN_NO       AS [COMPANY TAN],
                                  COR_HDR_COMPANY.COMPANY_NAME AS [COMPANY NAME],
                                  MST_ASSESSMENT.FA_YEAR       AS [FA YEAR],
                                  COR_HDR_BATCH.QTR            AS [QTR],
                                  COR_HDR_BATCH.FORM_NO        AS [FORM],
                                  CHALLANS.TOTAL_CHALLANS      AS [TOTAL CHALLAN], " +
                                      cmnService.J_SQLDBFormat(strCORDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS [TOTAL DEDUCTEES],
                                  TOT_TAX - TAX_DEPOSITED_AMOUNT AS [CHALLAN BALANCE],
                                  " + cmnService.J_SQLDBFormat(strValidationStatusMatrix, J_SQLColFormat.Case_End) + @" AS [VALIDATION STATUS],
                                  COR_HDR_BATCH.RECIEPT_NO AS [RECEIPT NO],
                                  " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS [DATE OF FILING]
                        FROM ((((((COR_HDR_BATCH
                        INNER JOIN COR_HDR_COMPANY 
                              ON COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID )
                        INNER JOIN MST_ASSESSMENT 
                              ON MST_ASSESSMENT.ASST_ID = COR_HDR_BATCH.ASST_ID)
                        LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                          TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
                                   FROM TRN_FILE_GENERATION_LOG
                                   INNER JOIN (SELECT BASIC_INFO_ID,
                                                      MAX(FG_LOG_ID) AS FG_LOG_ID
                                               FROM TRN_FILE_GENERATION_LOG
                                               WHERE CORR = 1
                                               GROUP BY BASIC_INFO_ID) AS LAST_FG_LOG
                                          ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID ) AS GENERATION_STATUS
                             ON GENERATION_STATUS.BASIC_INFO_ID = COR_HDR_BATCH.BATCH_HEADER_ID)

                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID,
                                                         COUNT(*)     AS TOTAL_CHALLANS,
                                                         SUM(TOT_TAX) - SUM(INTEREST) AS TOT_TAX
                                                FROM     COR_TRN_CHALLAN
                                                GROUP BY BATCH_HEADER_ID) AS CHALLANS
                                    ON         CHALLANS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                             cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                         cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                                FROM     COR_TRN_DEDUCTEE_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS DEDUCTEES
                                    ON         DEDUCTEES.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)

                                    LEFT JOIN
                                               (SELECT   BATCH_HEADER_ID," +
                                                              cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                                FROM     COR_TRN_SALARY_DETAILS
                                                GROUP BY BATCH_HEADER_ID) AS SALARY
                                    ON         SALARY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                        WHERE " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (1,2)
                        AND   COR_HDR_BATCH.RECIEPT_NO <> ''";
                    strSQLFiledReturns = strSQLFiledReturns + @"
                        UNION
                        SELECT 'R' AS [RETURN TYPE],     
                               MST_COMPANY.TAN_NO       AS [COMPANY TAN],
                               MST_COMPANY.COMPANY_NAME AS [COMPANY NAME],
                               MST_ASSESSMENT.FA_YEAR   AS [FA YEAR],
                               TRN_BASIC_INFO.QTR       AS [QTR],
                               TRN_BASIC_INFO.FORM_NO   AS [FORM],
                               CHALLANS.TOTAL_CHALLANS  AS [TOTAL CHALLAN],
                              " + cmnService.J_SQLDBFormat(strREGDEDSALMatrix, J_SQLColFormat.Case_End) + @" AS [TOTAL DEDUCTEES],
                              TOT_TAX - TAX_DEPOSITED_AMOUNT AS [CHALLAN BALANCE]," +
                                   "" + cmnService.J_SQLDBFormat(strValidationStatusMatrix, J_SQLColFormat.Case_End) + @" AS [VALIDATION STATUS],
                               TRN_BASIC_INFO.RECEIPT_NO    AS [RECEIPT NO],
                               " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS [DATE OF FILING]
                        FROM ((((((TRN_BASIC_INFO
                        INNER JOIN MST_COMPANY 
                              ON MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID)
                        INNER JOIN MST_ASSESSMENT 
                              ON MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID)
                        LEFT JOIN (SELECT BASIC_INFO_ID,
                                          COUNT(*) AS TOTAL_CHALLANS,
                                          SUM(TOT_TAX) - SUM(INTEREST) AS TOT_TAX
                                   FROM TRN_CHALLAN
                                   GROUP BY BASIC_INFO_ID) AS CHALLANS
                             ON CHALLANS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        LEFT JOIN
                                    (SELECT   BASIC_INFO_ID," +
                                                    cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS TOTAL_DEDUCTEES," +
                                                    cmnService.J_SQLDBFormat("SUM(TAX_DEPOSITED_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + @"   AS TAX_DEPOSITED_AMOUNT
                                    FROM     TRN_DEDUCTEE_DETAILS
                                    GROUP BY BASIC_INFO_ID) AS DEDUCTEES
                        ON         DEDUCTEES.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)

                        LEFT JOIN
                                    (SELECT   BASIC_INFO_ID," +
                                                cmnService.J_SQLDBFormat("COUNT(*)", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"  AS TOTAL_SALARY
                                    FROM      TRN_SALARY_DETAILS
                                    GROUP BY BASIC_INFO_ID) AS SALARY
                        ON         SALARY.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        LEFT JOIN (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                          TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
                                   FROM TRN_FILE_GENERATION_LOG
                                   INNER JOIN (SELECT BASIC_INFO_ID,
                                                      MAX(FG_LOG_ID) AS FG_LOG_ID
                                               FROM TRN_FILE_GENERATION_LOG
                                               WHERE CORR = 0
                                               GROUP BY BASIC_INFO_ID) AS LAST_FG_LOG
                                          ON LAST_FG_LOG.FG_LOG_ID = TRN_FILE_GENERATION_LOG.FG_LOG_ID ) AS GENERATION_STATUS
                             ON GENERATION_STATUS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID)
                        WHERE " + cmnService.J_SQLDBFormat("CHALLANS.TOTAL_CHALLANS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" > 0 
                        AND   " + cmnService.J_SQLDBFormat("GENERATION_STATUS.VALIDATION_STATUS", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @" IN  (1,2)
                        AND   TRN_BASIC_INFO.RECEIPT_NO <> '' ) REG_CORR_RETURN ";// AND TRN_BASIC_INFO.DATE_OF_FILING  IS NOT NULL ";
                                                                                  //-- 2022/03/02
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQLFiledReturns = strSQLFiledReturns + " WHERE   (REG_CORR_RETURN.[DATE OF FILING] IS NULL OR DATEDIFF(day, CONVERT(DATETIME, REG_CORR_RETURN.[DATE OF FILING], 103), getdate()) <= " + NoOfDaysSQL + ") ";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQLFiledReturns = strSQLFiledReturns + " WHERE   (REG_CORR_RETURN.[DATE OF FILING] IS NULL OR DATEDIFF('d', REG_CORR_RETURN.[DATE OF FILING]," + TdsMan.GetServerDateTime() + @") <= " + NoOfDaysSQL + ") ";
                    //
                    #endregion
                    // Create a new instance of FolderBrowserDialog.
                    FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                    // A new folder button will display in FolderBrowserDialog.
                    folderBrowserDlg.ShowNewFolderButton = true;
                    //Show FolderBrowserDialog
                    DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                    if (dlgResult.Equals(DialogResult.OK))
                    {
                        //Show selected folder path in textbox1.
                        strPath = folderBrowserDlg.SelectedPath;
                        //Browsing start from root folder.
                        Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                    }
                    else
                        return;
                    //--
                    strFileName = "FiledReturns_" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".csv";
                    ExportToCSV(strSQLFiledReturns, Path.Combine(strPath, strFileName));
                }
                #endregion

                #region FilingStatus
                else if (tbcTabControl.SelectedTab == tbpFilingStatus)
                {
                    if (dgvFilingStatus.RowCount <= 0)
                    {
                        cmnService.J_UserMessage("No record exists for Export !!");
                        return;
                    }
                    #region QUERY
                    //--
                    string[,] strQ1StatusMatrix = {{"QTR = 'Q1'", "F", "Q1_STATUS", "F"},
                                                {"", "F", "0", "T"}};
                    string[,] strQ2StatusMatrix = {{"QTR = 'Q2'", "F", "Q2_STATUS", "F"},
                                                {"", "F", "0", "T"}};
                    string[,] strQ3StatusMatrix = {{"QTR = 'Q3'", "F", "Q3_STATUS", "F"},
                                                {"", "F", "0", "T"}};
                    string[,] strQ4StatusMatrix = {{"QTR = 'Q4'", "F", "Q4_STATUS", "F"},
                                                {"", "F", "0", "T"}};
                    //
                    //string[,] strCORDEDSALMatrix = {{"cor_hdr_batch.form_no = '24Q' AND cor_hdr_batch.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString)  + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                    //                            {"cor_hdr_batch.form_no <> '24Q' AND cor_hdr_batch.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                    ////
                    //string[,] strREGDEDSALMatrix = {{"TRN_BASIC_INFO.form_no = '24Q' AND TRN_BASIC_INFO.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString) + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                    //                            {"TRN_BASIC_INFO.form_no <> '24Q' AND TRN_BASIC_INFO.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                    //
                    long NoOfDaysSQL = TDSMAN.Classes.TDSMAN.T_FILED_RETURNS_DAYS;
                    //-----------------------------------------------------------
                    strSQLFilingStatus = @"SELECT  
                                   TAN_NO AS [COMPANY TAN],	
	                               COMPANY_NAME AS [COMPANY NAME],	
	                               FORM_NO      AS [FORM],
	                               SUM(Q1_STATUS) AS [Q1 STATUS VALUE],
	                               SUM(Q2_STATUS) AS [Q2 STATUS VALUE],
	                               SUM(Q3_STATUS) AS [Q3 STATUS VALUE],
	                               SUM(Q4_STATUS) AS [Q4 STATUS VALUE]
                            FROM ( SELECT ASST_ID,
                                          TAN_NO,	
	                                      COMPANY_NAME,	
	                               FORM_NO,";
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQLFilingStatus = strSQLFilingStatus + @"CASE WHEN QTR = 'Q1' THEN RETURN_STATUS ELSE 0 END AS Q1_STATUS,
                                        CASE WHEN QTR = 'Q2' THEN RETURN_STATUS ELSE 0 END AS Q2_STATUS,
                                        CASE WHEN QTR = 'Q3' THEN RETURN_STATUS ELSE 0 END AS Q3_STATUS,
                                        CASE WHEN QTR = 'Q4' THEN RETURN_STATUS ELSE 0 END AS Q4_STATUS ";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQLFilingStatus = strSQLFilingStatus + @"IIF(QTR = 'Q1', RETURN_STATUS, 0 ) AS Q1_STATUS,
                                        IIF(QTR = 'Q2', RETURN_STATUS, 0 ) AS Q2_STATUS,
                                        IIF(QTR = 'Q3', RETURN_STATUS, 0 ) AS Q3_STATUS,
                                        IIF(QTR = 'Q4', RETURN_STATUS, 0 ) AS Q4_STATUS ";
                    strSQLFilingStatus = strSQLFilingStatus + @"FROM(
                            SELECT ASST_ID,
                                   TAN_NO,
	                               COMPANY_NAME,	
	                               QTR,	
	                               FORM_NO,	
	                               TOTAL_CHALLANS,	
	                               VALIDATION_STATUS,";
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQLFilingStatus = strSQLFilingStatus + @"CASE WHEN TOTAL_CHALLANS = 0 THEN 0  
			                            ELSE CASE WHEN VALIDATION_STATUS = 1 THEN 1 
			                                      WHEN VALIDATION_STATUS = 2 THEN 2 
					                              WHEN VALIDATION_STATUS = 3 THEN 3 
					                              WHEN VALIDATION_STATUS = 0 THEN 4 
                                             END
                                       END AS RETURN_STATUS ";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQLFilingStatus = strSQLFilingStatus + @"IIF(TOTAL_CHALLANS = 0, 0, IIF(VALIDATION_STATUS = 1, 1, IIF(VALIDATION_STATUS = 2, 2, IIF(VALIDATION_STATUS = 3, 3, IIF(VALIDATION_STATUS = 0, 4,0))))) AS RETURN_STATUS ";
                    strSQLFilingStatus = strSQLFilingStatus + @"FROM (
                            SELECT TRN_BASIC_INFO.BASIC_INFO_ID,
                                   TRN_BASIC_INFO.COMPANY_ID,
	                               TRN_BASIC_INFO.ASST_ID,
	                               MST_ASSESSMENT.FA_YEAR,
	                               MST_COMPANY.TAN_NO,
	                               MST_COMPANY.COMPANY_NAME,
	                               TRN_BASIC_INFO.QTR,
	                               TRN_BASIC_INFO.FORM_NO," +
                                       cmnService.J_SQLDBFormat("CHALLANS.TOTAL_CHALLANS", J_ColumnType.Long, J_SQLColFormat.NullCheck) + " AS TOTAL_CHALLANS," +
                                       cmnService.J_SQLDBFormat("RETURN_SUMMARY.VALIDATION_STATUS", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @" AS VALIDATION_STATUS 
                            FROM ((((TRN_BASIC_INFO
                            INNER JOIN MST_COMPANY ON MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID)
                            INNER JOIN MST_ASSESSMENT ON MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID)
                            LEFT JOIN (SELECT BASIC_INFO_ID,
                                              COUNT(*) AS TOTAL_CHALLANS
                                       FROM TRN_CHALLAN
		                               GROUP BY BASIC_INFO_ID) AS CHALLANS
                                 ON CHALLANS.BASIC_INFO_ID =  TRN_BASIC_INFO.BASIC_INFO_ID)
                            LEFT JOIN  (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                               TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
                                        FROM TRN_FILE_GENERATION_LOG
                                        INNER JOIN (SELECT BASIC_INFO_ID, 
                                                           MAX(FG_LOG_ID) AS FG_LOG_ID1
                                                    FROM TRN_FILE_GENERATION_LOG
                                                    GROUP BY BASIC_INFO_ID) AS LAST_STATUS 
                                        ON LAST_STATUS.FG_LOG_ID1 = TRN_FILE_GENERATION_LOG.FG_LOG_ID
                                        WHERE TRN_FILE_GENERATION_LOG.CORR = 0 ) AS RETURN_SUMMARY
                                        ON RETURN_SUMMARY.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID) 
                            WHERE CHALLANS.TOTAL_CHALLANS > 0
                            ) AS SUMMARY
                            ) AS SUMMARY1
                            ) AS SUMMARY2";
                    strSQLFilingStatus = strSQLFilingStatus + @" WHERE ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));

                    strSQLFilingStatus = strSQLFilingStatus + @" GROUP BY ASST_ID,
                                            TAN_NO,	
	                                        COMPANY_NAME,	
	                                        FORM_NO
                            ORDER BY ASST_ID,
                                     TAN_NO,	
	                                 COMPANY_NAME,	
	                                 FORM_NO";
                    //----
                    #endregion
                    // Create a new instance of FolderBrowserDialog.
                    FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                    // A new folder button will display in FolderBrowserDialog.
                    folderBrowserDlg.ShowNewFolderButton = true;
                    //Show FolderBrowserDialog
                    DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                    if (dlgResult.Equals(DialogResult.OK))
                    {
                        //Show selected folder path in textbox1.
                        strPath = folderBrowserDlg.SelectedPath;
                        //Browsing start from root folder.
                        Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                    }
                    else
                        return;
                    //--
                    strFileName = "FilingStatus_" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".csv";
                    ExportToCSV(strSQLFilingStatus, Path.Combine(strPath, strFileName));
                }
                //
                cmnService.J_UserMessage("Export Completed.\nNow it will open the file.");
                System.Diagnostics.Process.Start(Path.Combine(strPath, strFileName));
            }
            #endregion
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region LoadFilingStatus
        private void LoadFilingStatus(string SearchSQL)
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------------------------------------------------------
                string[,] strMatrix = {{"ASST_ID", "0", "", "", "", "F", ""},
                                    {"TAN", "120", "", "", "", "", "T"},
                                    {"Company", "290", "", "", "", "", "T"},
                                    {"Form", "100", "", "", "", "", "T"},
                                    {"Qtr 1", "105", "", "", "", "", "T"},
                                    {"Qtr 2", "105", "", "", "", "", "T"},
                                    {"Qtr 3", "105", "", "", "", "", "T"},
                                    {"Qtr 4", "105", "", "", "", "", "T"},
                                    {"Qtr1Val", "50", "", "", "", "F", "T"},
                                    {"Qtr2Val", "50", "", "", "", "F", "T"},
                                    {"Qtr3Val", "50", "", "", "", "F", "T"},
                                    {"Qtr4Val", "50", "", "", "", "F", "T"}};
                //--
                string[,] strQ1StatusMatrix = {{"QTR = 'Q1'", "F", "Q1_STATUS", "F"},
                                                {"", "F", "0", "T"}};
                string[,] strQ2StatusMatrix = {{"QTR = 'Q2'", "F", "Q2_STATUS", "F"},
                                                {"", "F", "0", "T"}};
                string[,] strQ3StatusMatrix = {{"QTR = 'Q3'", "F", "Q3_STATUS", "F"},
                                                {"", "F", "0", "T"}};
                string[,] strQ4StatusMatrix = {{"QTR = 'Q4'", "F", "Q4_STATUS", "F"},
                                                {"", "F", "0", "T"}};
                //
                //string[,] strCORDEDSALMatrix = {{"cor_hdr_batch.form_no = '24Q' AND cor_hdr_batch.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString)  + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                //                            {"cor_hdr_batch.form_no <> '24Q' AND cor_hdr_batch.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                ////
                //string[,] strREGDEDSALMatrix = {{"TRN_BASIC_INFO.form_no = '24Q' AND TRN_BASIC_INFO.qtr = 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString) + " + ' / ' + " + cmnService.J_SQLDBFormat("SALARY.total_SALARY", J_SQLColFormat.ConvertToString), "F"},
                //                            {"TRN_BASIC_INFO.form_no <> '24Q' AND TRN_BASIC_INFO.qtr <> 'Q4'", "F", cmnService.J_SQLDBFormat("DEDUCTEES.total_DEDUCTEES", J_SQLColFormat.ConvertToString), "F"}};
                //
                long NoOfDaysSQL = TDSMAN.Classes.TDSMAN.T_FILED_RETURNS_DAYS;
                //-----------------------------------------------------------
                strSQL = @"SELECT  ASST_ID,
                                   TAN_NO,	
	                               COMPANY_NAME,	
	                               FORM_NO,
                                   ''             AS Q1_STATUS_DISPLAY,
                                   ''             AS Q2_STATUS_DISPLAY,
                                   ''             AS Q3_STATUS_DISPLAY,
                                   ''             AS Q4_STATUS_DISPLAY,
	                               SUM(Q1_STATUS) AS Q1_STATUS_VALUE,
	                               SUM(Q2_STATUS) AS Q2_STATUS_VALUE,
	                               SUM(Q3_STATUS) AS Q3_STATUS_VALUE,
	                               SUM(Q4_STATUS) AS Q4_STATUS_VALUE
                            FROM ( SELECT ASST_ID,
                                          TAN_NO,	
	                                      COMPANY_NAME,
	                               FORM_NO,";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = strSQL + @"CASE WHEN QTR = 'Q1' THEN RETURN_STATUS ELSE 0 END AS Q1_STATUS,
                                        CASE WHEN QTR = 'Q2' THEN RETURN_STATUS ELSE 0 END AS Q2_STATUS,
                                        CASE WHEN QTR = 'Q3' THEN RETURN_STATUS ELSE 0 END AS Q3_STATUS,
                                        CASE WHEN QTR = 'Q4' THEN RETURN_STATUS ELSE 0 END AS Q4_STATUS ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = strSQL + @"IIF(QTR = 'Q1', RETURN_STATUS, 0 ) AS Q1_STATUS,
                                        IIF(QTR = 'Q2', RETURN_STATUS, 0 ) AS Q2_STATUS,
                                        IIF(QTR = 'Q3', RETURN_STATUS, 0 ) AS Q3_STATUS,
                                        IIF(QTR = 'Q4', RETURN_STATUS, 0 ) AS Q4_STATUS ";
                strSQL = strSQL + @"FROM(
                            SELECT ASST_ID,
                                   TAN_NO,
	                               COMPANY_NAME,	
	                               QTR,	
                                   FORM_NO,	
                                   TOTAL_CHALLANS,	
	                               VALIDATION_STATUS,";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = strSQL + @"CASE WHEN TOTAL_CHALLANS = 0 THEN 0  
			                            ELSE CASE WHEN VALIDATION_STATUS = 1 THEN 1 
			                                      WHEN VALIDATION_STATUS = 2 THEN 2 
					                              WHEN VALIDATION_STATUS = 3 THEN 3 
					                              WHEN VALIDATION_STATUS = 0 THEN 4 
                                             END
                                       END AS RETURN_STATUS ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = strSQL + @"IIF(TOTAL_CHALLANS = 0, 0, IIF(VALIDATION_STATUS = 1, 1, IIF(VALIDATION_STATUS = 2, 2, IIF(VALIDATION_STATUS = 3, 3, IIF(VALIDATION_STATUS = 0, 4,0))))) AS RETURN_STATUS ";
                strSQL = strSQL + @"FROM (
                            SELECT TRN_BASIC_INFO.BASIC_INFO_ID,
                                   TRN_BASIC_INFO.COMPANY_ID,
	                               TRN_BASIC_INFO.ASST_ID,
	                               MST_ASSESSMENT.FA_YEAR,
	                               MST_COMPANY.TAN_NO,
	                               MST_COMPANY.COMPANY_NAME,
	                               TRN_BASIC_INFO.QTR," +
                                  //TRN_BASIC_INFO.FORM_NO,"
                                  "IIF(TRN_BASIC_INFO.FORM_NO = '24Q', '138 (24Q)', " +
                                  "IIF(TRN_BASIC_INFO.FORM_NO = '26Q', '140 (26Q)', " +
                                  "IIF(TRN_BASIC_INFO.FORM_NO = '27Q', '144 (27Q)', " +
                                  "IIF(TRN_BASIC_INFO.FORM_NO = '27EQ', '143 (27EQ)', '')))) AS FORM_NO, " +
                                 @" " + cmnService.J_SQLDBFormat("CHALLANS.TOTAL_CHALLANS", J_ColumnType.Long, J_SQLColFormat.NullCheck) + " AS TOTAL_CHALLANS," +
                                   cmnService.J_SQLDBFormat("RETURN_SUMMARY.VALIDATION_STATUS", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @" AS VALIDATION_STATUS 
                            FROM ((((TRN_BASIC_INFO
                            INNER JOIN MST_COMPANY ON MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID)
                            INNER JOIN MST_ASSESSMENT ON MST_ASSESSMENT.ASST_ID = TRN_BASIC_INFO.ASST_ID)
                            LEFT JOIN (SELECT BASIC_INFO_ID,
                                              COUNT(*) AS TOTAL_CHALLANS
                                       FROM TRN_CHALLAN
		                               GROUP BY BASIC_INFO_ID) AS CHALLANS
                                 ON CHALLANS.BASIC_INFO_ID =  TRN_BASIC_INFO.BASIC_INFO_ID)
                            LEFT JOIN  (SELECT TRN_FILE_GENERATION_LOG.BASIC_INFO_ID,
                                               TRN_FILE_GENERATION_LOG.VALIDATION_STATUS
                                        FROM TRN_FILE_GENERATION_LOG
                                        INNER JOIN (SELECT BASIC_INFO_ID, 
                                                           MAX(FG_LOG_ID) AS FG_LOG_ID1
                                                    FROM TRN_FILE_GENERATION_LOG
                                                    GROUP BY BASIC_INFO_ID) AS LAST_STATUS 
                                        ON LAST_STATUS.FG_LOG_ID1 = TRN_FILE_GENERATION_LOG.FG_LOG_ID
                                        WHERE TRN_FILE_GENERATION_LOG.CORR = 0 ) AS RETURN_SUMMARY
                                        ON RETURN_SUMMARY.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID) 
                            WHERE CHALLANS.TOTAL_CHALLANS > 0
                            ) AS SUMMARY
                            ) AS SUMMARY1
                            ) AS SUMMARY2";
                strSQL = strSQL + @" WHERE ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
                if (SearchSQL != "")
                {
                    strSQL = strSQL + " AND TAN_NO + COMPANY_NAME + FORM_NO  LIKE '%" + SearchSQL + @"%'";
                }
                strSQL = strSQL + @" GROUP BY ASST_ID,
                                            TAN_NO,	
	                                        COMPANY_NAME,	
	                                        FORM_NO
                            ORDER BY ASST_ID,
                                     TAN_NO,	
	                                 COMPANY_NAME,	
	                                 FORM_NO";
                //----
                if (dsetGridClone != null) dsetGridClone.Clear();
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)//--2024/05/21
                    dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvFilingStatus, strSQL, strMatrix);
                strSQLFilingStatus = strSQL; //-- for export to csv
                dgvFilingStatus.ClearSelection();
                //
                lblReturnsStatusFooterMessage.Text = "Total Record Count : " + dgvFilingStatus.RowCount.ToString();
                //--
                #region CellFormatting(SHOW_BUTTON)

                ////////if (dgvUpdates.Columns[e.ColumnIndex].DataPropertyName == "SHOW_BUTTON")
                ////////{
                //////DataGridViewButtonColumn BtnShow = new DataGridViewButtonColumn();
                //////dgvFiledReturns.Columns.Add(BtnShow);
                ////////dgvUpdates.Columns
                //////BtnShow.Visible = true;
                //////BtnShow.HeaderText = "";
                //////BtnShow.Text = "Show";
                //////BtnShow.ToolTipText = "Go to the Return";
                //////BtnShow.Name = "BtnShow";
                //////BtnShow.UseColumnTextForButtonValue = true;
                ////////}
                #endregion
                //-
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }




        #endregion

        #endregion


        #region J_ReturnServerDate
        public string J_ReturnServerDate()
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strSQL = "SELECT FORMAT(DATE(),'dd/MM/yyyy')";
            else
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            //--
            return cmnService.J_NullToText(dmlService.J_ExecSqlReturnScalar(strSQL));
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
