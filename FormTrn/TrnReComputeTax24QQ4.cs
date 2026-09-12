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

using System.Xml;




//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;
using Excel = Microsoft.Office.Interop.Excel;
#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnReComputeTax24QQ4 : Form
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnReComputeTax24QQ4()
        {
            InitializeComponent();
            objBindingSource.DataSource = new PANDetails();
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

        string strSQL = string.Empty;
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
        bool blnExit = true;
        //
        int intID = 0;
        int intPANId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0;
        int intVerifyId = 0, intVerifiedName = 0,intClicked = 0;
        //;
        //
        string strQuarter = "", strOrderBy = "", strQuery = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNoBkmark = "";
        string strFAYear = "";
        //
        int intLoadGridPAN = 500;
        long lngSelectedGrid = 0;
        //
        int j = 0; int intI = 0;
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
        #region T_Grid
        public struct T_Grid
        {
            public const int SALARY_DETAILS_ID = 0;
            public const int EMPLOYEE_ID = 1;
            public const int SL_NO = 2;
            public const int EMPLOYEE_PAN = 3;
            public const int EMPLOYEE_NAME = 4;
            public const int CATEGORY = 5;
            public const int SECTION_115BAC_FLAG = 6;
            public const int TOTAL_INCOME = 7;
            public const int TAX_PAYABLE_ENTERED = 8;
            public const int TAX_REBATE_87_ENTERED = 9;
            public const int TAX_SURCH_ENTERED = 10;
            public const int TAX_EDU_CESS_ENTERED = 11;
            public const int TAX_PAYABLE_COMPUTED = 12;
            public const int TAX_REBATE_87_COMPUTED = 13;
            public const int TAX_SURCH_COMPUTED = 14;
            public const int TAX_EDU_CESS_COMPUTED = 15;
            public const int TAX_DIFF = 16;
        }
        #endregion
        //
        string strNOTAVAILABLE = "NOT AVAILABLE";
        //--
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        int lngAsstId = 0;// Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
        //Added by Indrajit on 21-02-2013 to get DateTime of a Remote Computer
        #region Decleration for Remote DateTime

        private const uint NERR_Success = 0;



        /// Free the buffer allocated by the NetRemoteTOD function
        /// <param name="Buffer">pointer to the buffer</param>
        /// <returns>NERR_Success on success, else the system error code.</returns>
        [DllImport("netapi32.dll", SetLastError = false)]
        private static extern uint NetApiBufferFree(IntPtr Buffer);

        [DllImport("netapi32.dll", CharSet = CharSet.Unicode,
        SetLastError = false)]
        private static extern uint NetRemoteTOD(string
        UncServerName, ref IntPtr BufferPtr);

        [StructLayout(LayoutKind.Sequential)]
        private struct TIME_OF_DAY_INFO
        {
            public uint tod_elapsedt;
            public uint tod_msecs;
            public uint tod_hours;
            public uint tod_mins;
            public uint tod_secs;
            public uint tod_hunds;
            public uint tod_timezone;
            public uint tod_tinterval;
            public uint tod_day;
            public uint tod_month;
            public uint tod_year;
            public uint tod_weekday;
        }
        #endregion
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

        //-- Added By Abhishek Dey On 22/08/2018 --
        #region TrnBulkPANNameValidation_Activated
        private void TrnBulkPANNameValidation_Activated(object sender, EventArgs e)
        {
            //--
            //int h = Screen.PrimaryScreen.WorkingArea.Height;
            //int w = Screen.PrimaryScreen.WorkingArea.Width;
            //this.ClientSize = new Size(w, h);
            //----
            //-----------------------------------------
        }
        #endregion
        //----------------------------------------------

        #region TrnBulkPANNameValidation_Load
        private void TrnBulkPANNameValidation_Load(object sender, EventArgs e)
        {
            try
            {
                //--
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //----
                GC.Collect();
                //
                ClearFields();
                //--
                //--
                #region COMMENT
                //                LoadDeducteeGrid();
                //                //--
                //                if (TDSMAN.Classes.TDSMAN.T_PANVerificationModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                //                {
                //                    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pBatchId;
                //                    //--
                //                    if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID =" + lngBasicInfoID)) == T_FormNo.F24Q)
                //                        strEmpDed = "Employee";
                //                    else
                //                        strEmpDed = "Deductee";
                //                    //--
                //                    intPANId = 0;
                //                    intNameEntered = 1;
                //                    intNameVerified = 2;
                //                    intStatusId = 3;
                //                    intVerifyId = 4;
                //                    //
                //                    blRegular = false;
                //                }
                //                else
                //                {
                //                    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pBasicInfoId;
                //                    //
                //                    if (TDSMAN.Classes.TDSMAN.T_pTabFormCaption == T_FormNo.F24Q)
                //                        strEmpDed = "Employee";
                //                    else
                //                        strEmpDed = "Deductee";
                //                    //--
                //                    intPANId = 1;
                //                    intNameEntered = 2;
                //                    intNameVerified = 3;
                //                    intStatusId = 4;
                //                    intVerifyId = 5;
                //                    //
                //                    blRegular = true;
                //                }
                //                //--
                //                if (lngBasicInfoID == 0)
                //                {
                //                    //if (dgvDeductees.Rows.Count > 0)
                //                    //    dgvDeductees.Rows.Clear();
                //                    return;
                //                }
                //                //--
                //                if (TDSMAN.Classes.TDSMAN.T_PANVerificationModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                //                    LoadDeducteeGridCorr(lngBasicInfoID);
                //                else
                //                    LoadDeducteeGrid(lngBasicInfoID);
                //                //
                //                //this.Cursor = Cursors.Default;
                //                //
                //                grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //                btnVerification.Text = strbtnVerification;
                //                btnVerification.ForeColor = Color.Black;
                //                //--
                //                TdsMan.GetSetup();
                //                //--
                //                #region COMMENT
                //                //-----------
                //                //-- FINANCIAL YEAR
                //                //-----------
                //                //strSQL = " SELECT ASST_ID," +
                //                //    "             FA_YEAR " +
                //                //    "      FROM   MST_ASSESSMENT " +
                //                //    "      WHERE  VISIBILITY_FLAG = 0 " +
                //                //    "      ORDER BY ASST_ID DESC";
                //                //if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
                //                ////-----------
                //                ////-- QUARTER
                //                ////-----------
                //                //string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
                //                //dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter, 1);
                //                ////-----------
                //                ////-- FORM NO
                //                ////-----------
                //                //string[] strFormNo ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
                //                //dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo, 1);
                //                ////-----------
                //                ////-- COMPANY
                //                ////-----------
                //                //strSQL = " SELECT COMPANY_ID," +
                //                //    "             COMPANY_NAME + ' [' + TAN_NO + ']' " +
                //                //    "      FROM   MST_COMPANY " +
                //                //    "      ORDER BY COMPANY_NAME";
                //                //if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
                //                ////-----------
                //                //cmbFinancialYear.Select();
                //                #endregion
                //                //--
                //                strSQL = "DELETE FROM TEMP_INVALID_PAN";
                //                dmlService.J_ExecSql(strSQL);
                //                //--
                //                #region INSERT INTO MST_VERIFIED_PAN

                //                strSQL = @"INSERT INTO MST_VERIFIED_PAN (PAN, VERIFIED_NAME)
                //                            SELECT DISTINCT DEDUCTEE_PAN, DEDUCTEE_NAME 
                //                            FROM   COR_HDR_DEDUCTEE_DETAILS LEFT JOIN MST_VERIFIED_PAN
                //                            ON     COR_HDR_DEDUCTEE_DETAILS.DEDUCTEE_PAN = MST_VERIFIED_PAN.PAN
                //                            WHERE  MST_VERIFIED_PAN.PAN IS NULL
                //                            AND    COR_HDR_DEDUCTEE_DETAILS.INVALID_PAN = 0
                //                            AND    COR_HDR_DEDUCTEE_DETAILS.DEDUCTEE_PAN NOT IN ('PANNOTAVBL', 'PANAPPLIED', 'PANINVALID')";
                //                dmlService.J_ExecSql(strSQL);
                //                //--
                //                strSQL = @"INSERT INTO MST_VERIFIED_PAN (PAN, VERIFIED_NAME)
                //                            SELECT DISTINCT EMPLOYEE_PAN, EMPLOYEE_NAME 
                //                            FROM   COR_HDR_SALARY_DETAILS LEFT JOIN MST_VERIFIED_PAN
                //                            ON     COR_HDR_SALARY_DETAILS.EMPLOYEE_PAN = MST_VERIFIED_PAN.PAN
                //                            WHERE  MST_VERIFIED_PAN.PAN IS NULL
                //                            AND    COR_HDR_SALARY_DETAILS.INVALID_PAN = 0
                //                            AND    COR_HDR_SALARY_DETAILS.EMPLOYEE_PAN NOT IN ('PANNOTAVBL', 'PANAPPLIED', 'PANINVALID')";
                //                dmlService.J_ExecSql(strSQL);

                //                #endregion
                #endregion
                //--
                //
                //chkSelectAll_CheckedChanged(sender, e);
                System.Threading.Thread.Sleep(300);
                //
                //btnDownload.Enabled = true;
                btnDownload.Enabled = false;
                btnUpdateSalaryDetails.Enabled = false;
                btnUpdateSalaryDetails.BackColor = Color.Lavender;
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


        #region btnLoadGrid_Click
        private void btnLoadGrid_Click(object sender, EventArgs e)
        {
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                LoadDeducteeGrid();
                //ClearFields();
                return;
            }
            if (cmbCompany.SelectedIndex <= 0)
            {
                LoadDeducteeGrid();
                //ClearFields();
                return;
            }
            lngAsstId = Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
            //-----------------------------------------------
            //ClearFields();
            //-----------------------------------------------
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        T_Qtr.Q4,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        T_FormNo.F24Q);
            //
            //
            if (lngBasicInfoID == 0)
            {
                LoadDeducteeGrid();
                return;
            }
            else
            {
                LoadDeducteeGrid(lngBasicInfoID);
                return;
            }
            //--
        }
        #endregion

        #region cmbLoadGrid_SelectedIndexChanged
        private void cmbLoadGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--
            LoadDeducteeGrid();
            //--
        }
        #endregion

               

        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            //bgwPANVerification.CancelAsync();
            //bgwPANVerification.Dispose();
            //
            TDSMAN.Classes.TDSMAN.T_pTabFormCaption = "";
            TDSMAN.Classes.TDSMAN.T_pQuarter = "";
            //
            GC.Collect();
            //
            blnVerificationComplete = false;
            //--
            this.Dispose();
            this.Close();
        }
        #endregion

        #region dgvDeductees_ColumnAdded
        private void dgvDeductees_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            //dgvDeductees.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        #endregion

        #region lblInvalidNo_TextChanged
        private void lblInvalidNo_TextChanged(object sender, EventArgs e)
        {
            //if (cmnService.J_ReturnInt32Value(lblInvalidNo.Text) > 0)
            //    btnPrintInvalidPAN.Enabled = true;
            //else
            //    btnPrintInvalidPAN.Enabled = false;
        }
        #endregion

        #region dgvDeductees_CellFormatting
        private void dgvDeductees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (dgvDeductees.Columns[e.ColumnIndex].DataPropertyName == "STATUS")
            //{
            //    if (dgvDeductees.Rows[e.RowIndex].Cells["VERIFIED_STATUS"].Value.ToString() == "0")
            //    {
            //        //highlighting the cell
            //        //e.CellStyle.BackColor = Color.Green;
            //        //e.CellStyle.ForeColor = Color.White;
            //        dgvDeductees.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Valid PAN";
            //    }
            //    else if (dgvDeductees.Rows[e.RowIndex].Cells["VERIFIED_STATUS"].Value.ToString() == "1")
            //    {
            //        //e.CellStyle.BackColor = Color.OrangeRed;
            //        //e.CellStyle.ForeColor = Color.Black;
            //        dgvDeductees.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Invalid PAN";
            //    }
            //    else if (dgvDeductees.Rows[e.RowIndex].Cells["VERIFIED_STATUS"].Value.ToString() == "")
            //    {
            //        //e.CellStyle.BackColor = Color.White;
            //    }
            //}
            ////--
            //if (dgvDeductees.Columns[e.ColumnIndex].DataPropertyName == "EMPLOYEE_NAME_V")
            //{
            //    if (dgvDeductees.Rows[e.RowIndex].Cells["EMPLOYEE_NAME_V"].Value.ToString() != "")
            //    {
            //        if (dgvDeductees.Rows[e.RowIndex].Cells["EMPLOYEE_NAME"].Value.ToString() != dgvDeductees.Rows[e.RowIndex].Cells["EMPLOYEE_NAME_V"].Value.ToString())
            //        {
            //            e.CellStyle.BackColor = Color.Tan;
            //        }
            //    }
            //}
        }
        #endregion


        #endregion

        #region User Define Functions

        #region PanVerify
        private void PanVerify(long BasicInfoID)
        {
            IDataReader drdPanVerify = null;
            try
            {
                strSQL = "SELECT   DISTINCT MST_" + strEmpDed + "." + strEmpDed + "_ID   AS DEDUCTEE_ID," +
                         "         MST_" + strEmpDed + "." + strEmpDed + "_NAME AS DEDUCTEE_NAME," +
                         "         MST_" + strEmpDed + "." + strEmpDed + "_PAN  AS DEDUCTEE_PAN," +
                         "         ''                                       AS STATUS " +
                         "FROM     TRN_DEDUCTEE_DETAILS," +
                         "         MST_" + strEmpDed + "  " +
                         "WHERE    TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_" + strEmpDed + "." + strEmpDed + "_ID " +
                         "AND      TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
                         "ORDER BY " + strEmpDed + "_NAME," +
                         "         " + strEmpDed + "_PAN";
                //--
                drdPanVerify = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdPanVerify == null)
                    return;
                //--
                DataGridViewCellFormattingEventArgs e;//new DataGridViewCellFormattingEventArgs(3, 1, "", null, "");
                //--
                while (drdPanVerify.Read())
                {
                    if (objTracesConnect.IsValidPAN(Convert.ToString(drdPanVerify["DEDUCTEE_PAN"])) == true)
                    {
                        //if (dgvDeductees.Columns[3].DataPropertyName == "STATUS")
                        //{
                        //    dgvDeductees CellStyle.BackColor = Color.Green;
                        //}
                    }
                    else
                    {
                        //if (dgvDeductees.Columns[e.ColumnIndex].DataPropertyName == "STATUS")
                        //{
                        //    e.CellStyle.BackColor = Color.Red;
                        //}
                    }
                }
                drdPanVerify.Close();
                drdPanVerify.Dispose();
                //
                #region COMMENT
                //EXECUTE QUERY STRING FOR INCOME TAX AMOUNT CALCULATION 
                //ds = dmlService.J_ExecSqlReturnDataSet(strSQL);

                //if (ds == null) return;
                ////------------
                //for (int i = 1; i <= ds.Tables[0].Rows.Count; i++)
                //{
                //    strPAN = strPAN + "," + ds.Tables[0].Rows[i - 1]["PAN_NO"];
                //}
                //string[] strPANVerification = strPAN.Split(',');

                //Debug.Print(Convert.ToString(DateTime.Now));
                //label2.Text = Convert.ToString(DateTime.Now);
                //for (int j = 0; j <= strPANVerification.Length - 1; j++)
                //{
                //    if (objAccount.IsValidPAN(strPANVerification[j]) == true)
                //        Debug.Print(strPANVerification[j] + " - TRUE");
                //    else
                //        Debug.Print(strPANVerification[j] + " - FALSE");

                //    this.Refresh();
                //    label1.Text = Convert.ToString(j + 1) + "/ " + Convert.ToString(strPANVerification.Length);
                //}
                //label2.Text = label2.Text + "      " + Convert.ToString(DateTime.Now);
                //MessageBox.Show("COMPLETED !");
                //Debug.Print(Convert.ToString(DateTime.Now));
                //Debug.Print("COMPLETED !");
                #endregion
            }
            catch (Exception err)
            {

            }
        }

        #endregion

        #region LoadDeducteeGrid

        #region LoadDeducteeGrid()
        private void LoadDeducteeGrid()
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"SALARY_DETAILS_ID", "0", "", "", "", "F", ""},
                                        {"EMPLOYEE_ID", "0", "", "", "", "F", ""},
                                        {"Sl. No.", "50", "", "", "", "", "T"},
                                        {"PAN No.", "110", "", "", "", "", "T"},
                                        {"Name", "250", "", "", "", "", "T"},
                                        {"Category", "60", "", "", "", "", "T"},
                                        {"Regime", "60", "", "", "", "", "T"},
                                        {"Taxable Income", "100", "0.00", "R", "", "", "T"},
                                        {"Tax Payable(as entered)", "100", "0.00", "R", "", "", "T"},
                                        {"Rebate us 87A(as entered)", "100", "0.00", "R", "", "", "T"},
                                        {"Surcharge", "0", "0.00", "", "", "F", ""},
                                        {"Edu Cess", "0", "0.00", "", "", "F", ""},
                                        {"Tax Payable(as computed)", "100", "0.00", "R", "", "", "T"},
                                        {"Rebate us 87A(as computed)", "100", "0.00", "R", "", "", "T"},
                                        {"Surcharge", "0", "0.00", "", "", "F", ""},
                                        {"Edu Cess", "0", "0.00", "", "", "F", ""},
                                        {"Diff(Tax Payable)", "200", "0.00", "R", "", "", "T"}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //            
                strSQL = "SELECT TRN_SALARY_DETAILS.SALARY_DETAILS_ID  AS SALARY_DETAILS_ID," +
                           "       MST_EMPLOYEE.EMPLOYEE_ID              AS EMPLOYEE_ID," +
                           "       TRN_SALARY_DETAILS.SL_NO              AS SL_NO," +
                           "       MST_EMPLOYEE.EMPLOYEE_PAN             AS EMPLOYEE_PAN," +
                           "       MST_EMPLOYEE.EMPLOYEE_NAME            AS EMPLOYEE_NAME," +
                           "       MST_EMPLOYEE.CATEGORY                 AS CATEGORY," +
                           "       TRN_SALARY_DETAILS.SECTION_115BAC_FLAG AS SECTION_115BAC_FLAG," +
                           "       TRN_SALARY_DETAILS.TOTAL_INCOME       AS TOTAL_INCOME," +
                           "       (TRN_SALARY_DETAILS.TAX_TOTAL_INCOME_B4_REBATE - TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT)   AS TAX_TOTAL_INCOME," +
                           "       TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT AS REBATE_US_87A_AMOUNT," +
                           "       TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME  AS SCHG_TOTAL_INCOME," +
                           "       TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME AS ECESS_TOTAL_INCOME," +
                           "       ''                                    AS TAX_PAYABLE_COMPUTED," +
                           "       ''                                    AS REBATE_US_87A_AMOUNT_COMPUTED," +
                           "       ''                                    AS SCHG_TOTAL_INCOME_COMPUTED," +
                           "       ''                                    AS ECESS_TOTAL_INCOME_COMPUTED," +
                           "       ''                                    AS DIFF " +
                           "FROM   TRN_SALARY_DETAILS," +
                           "       MST_EMPLOYEE " +
                           "WHERE  TRN_SALARY_DETAILS.EMPLOYEE_ID    = MST_EMPLOYEE.EMPLOYEE_ID " +
                           "AND      1=2 ";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
                //
                btnDownload.Enabled = false;
                btnUpdateSalaryDetails.Enabled = false;
                btnUpdateSalaryDetails.BackColor = Color.Lavender;
                txtTotalRecords.Text = dgvDeductees.Rows.Count.ToString();
                //
                txtDifferenceFound.Text = "0";
                txtShortDeductionRecords.Text = "0";
                txtShortDeductionValue.Text = "0.00";
                //grpStatus.Text = " Total Record";//(s): " + dgvDeductees.Rows.Count + " ";
                //
            }
            catch
            {

            }
        }
        #endregion

        #region LoadDeducteeGrid(long BasicInfoID)
        private void LoadDeducteeGrid(long BasicInfoID)
        {
            DataSet dsetGridClone = new DataSet();
            string strFilter = "";
            try
            {
                //if (rbnAllPANs.Checked == true)
                //    strFilter = "";
                //else if (rbnReVerifyValidPANs.Checked == true)
                //    strFilter = "0";
                //else if (rbnOnlyUnVerifiedPANs.Checked == true)
                //    strFilter = "1";
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"SALARY_DETAILS_ID", "0", "", "", "", "F", ""},
                                        {"EMPLOYEE_ID", "0", "", "", "", "F", ""},
                                        {"Sl. No.", "50", "", "", "", "", "T"},
                                        {"PAN No.", "110", "", "", "", "", "T"},
                                        {"Name", "250", "", "", "", "", "T"},
                                        {"Category", "60", "", "", "", "", "T"},
                                        {"Regime", "60", "", "", "", "", "T"},
                                        {"Taxable Income", "100", "0.00", "R", "", "", "T"},
                                        {"Tax Payable(as entered)", "100", "0.00", "R", "", "", "T"},
                                        {"Rebate us 87A(as entered)", "100", "0.00", "R", "", "", "T"},
                                        {"Surcharge", "0", "0.00", "", "", "F", ""},
                                        {"Edu Cess", "0", "0.00", "", "", "F", ""},
                                        {"Tax Payable(as computed)", "100", "0.00", "R", "", "", "T"},
                                        {"Rebate us 87A(as computed)", "100", "0.00", "R", "", "", "T"},
                                        {"Surcharge", "0", "0.00", "", "", "F", ""},
                                        {"Edu Cess", "0", "0.00", "", "", "F", ""},
                                        {"Diff(Tax Payable)", "200", "0.00", "R", "", "", "T"}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //
                string[,] strSECTION_115BAC_FLAG = { { "TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "", "T"},
                                             { "TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "New", "T"} };
                //
                strOrderBy = "TRN_SALARY_DETAILS.SL_NO";
                strQuery = "SELECT TRN_SALARY_DETAILS.SALARY_DETAILS_ID  AS SALARY_DETAILS_ID," +
                           "       MST_EMPLOYEE.EMPLOYEE_ID              AS EMPLOYEE_ID," +
                           "       TRN_SALARY_DETAILS.SL_NO              AS SL_NO," +
                           "       MST_EMPLOYEE.EMPLOYEE_PAN             AS EMPLOYEE_PAN," +
                           "       MST_EMPLOYEE.EMPLOYEE_NAME            AS EMPLOYEE_NAME," +
                           "       MST_EMPLOYEE.CATEGORY                 AS CATEGORY," +
                           " " + cmnService.J_SQLDBFormat(strSECTION_115BAC_FLAG, J_SQLColFormat.Case_End) + @" AS SECTION_115BAC_FLAG," +
                           "       TRN_SALARY_DETAILS.TOTAL_INCOME       AS TOTAL_INCOME," +
                           //"       (TRN_SALARY_DETAILS.TAX_TOTAL_INCOME_B4_REBATE - TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT)   AS TAX_TOTAL_INCOME," +
                           "       TRN_SALARY_DETAILS.TAX_TOTAL_INCOME_B4_REBATE   AS TAX_TOTAL_INCOME," +
                           "       TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT AS REBATE_US_87A_AMOUNT," +
                           "       TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME  AS SCHG_TOTAL_INCOME," +
                           "       TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME AS ECESS_TOTAL_INCOME," +
                           "       ''                                    AS TAX_PAYABLE_COMPUTED," +
                           "       ''                                    AS REBATE_US_87A_AMOUNT_COMPUTED," +
                           "       ''                                    AS SCHG_TOTAL_INCOME_COMPUTED," +
                           "       ''                                    AS ECESS_TOTAL_INCOME_COMPUTED," +
                           "       ''                                    AS DIFF  " +
                           "FROM   TRN_SALARY_DETAILS," +
                           "       MST_EMPLOYEE " +
                           "WHERE  TRN_SALARY_DETAILS.EMPLOYEE_ID    = MST_EMPLOYEE.EMPLOYEE_ID " +
                           "AND    TRN_SALARY_DETAILS.BASIC_INFO_ID  = " + BasicInfoID + " ";
                //-----------------------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                txtTotalRecords.Text = dgvDeductees.Rows.Count.ToString();
                //
                txtDifferenceFound.Text = "0";
                txtShortDeductionRecords.Text = "0";
                txtShortDeductionValue.Text = "0.00";
                //TdsMan.PopulateGridView(dgvDeductees, strSQL, strMatrixViewDeductee);
                //dgvDeductees.ClearSelection();
                //
                //grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //                
                //CheckAllGridRows();
                //foreach (DataGridViewRow row in dgvDeductees.Rows)
                //{
                //    //if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                //    //{
                //    //    row.Cells[0].Value = true;
                //    //}
                //    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                //    chk.Value = chk.TrueValue;
                //}
                //dgvDeductees.EndEdit();
                //foreach (DataGridViewRow row in dgvDeductees.Rows)
                //{
                //    dgvDeductees.Rows[0].SetValues(true);
                //}
                //
            }
            catch
            {

            }
        }
        #endregion
             

        

        #endregion

        #region ClearFields
        private void ClearFields()
        {
            btnStart.Enabled = true;
            btnStart.BackColor = Color.Lavender;
            grpButtons.Enabled = true;
            //
            //-----------
            //-- FINANCIAL YEAR
            //-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //-- COMPANY
            //-----------
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME + ' [' + TAN_NO + ']' " +
                "      FROM   MST_COMPANY " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //-----------
            txtTotalRecords.Text = "0";
            txtDifferenceFound.Text = "0";
            //
            txtShortDeductionValue.Text = "0.00";
            txtShortDeductionRecords.Text = "0";
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
               
               

        #region ShowHideLoginDetails
        void ShowHideLoginDetails(enmRequestType enmStatus)
        {
            switch (enmStatus)
            {
                case enmRequestType.Login:
                    //btnLogging.Text = strLogOn;
                    LoadDeducteeGrid();
                    //
                    break;
                case enmRequestType.PanValidation:
                    //
                    grpButtons.Enabled = true;
                    dgvDeductees.Enabled = true;
                    //
                    btnStart.Text = "Stop validating";
                    btnStart.ForeColor = Color.OrangeRed;
                    //
                    bgwTaxComputation.RunWorkerAsync();
                    //
                    //btnLogging.Text = strLogOff;
                    ////--
                    //LoadDeducteeGrid();
                    ////--
                    //if (TDSMAN.Classes.TDSMAN.T_PANVerificationModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                    //{
                    //    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pBatchId;
                    //    //--
                    //    if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID =" + lngBasicInfoID)) == T_FormNo.F24Q)
                    //        strEmpDed = "Employee";
                    //    else
                    //        strEmpDed = "Deductee";
                    //    //--
                    //    intPANId = 0;
                    //    intNameEntered = 1;
                    //    intNameVerified = 2;
                    //    intStatusId = 3;
                    //    intVerifyId = 4;
                    //    //
                    //    blRegular = false;
                    //}
                    //else
                    //{
                    //    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pBasicInfoId;
                    //    //
                    //    if (TDSMAN.Classes.TDSMAN.T_pTabFormCaption == T_FormNo.F24Q)
                    //        strEmpDed = "Employee";
                    //    else
                    //        strEmpDed = "Deductee";
                    //    //--
                    //    intPANId = 1; 
                    //    intNameEntered = 2;
                    //    intNameVerified = 3;
                    //    intStatusId = 4;
                    //    intVerifyId = 5;
                    //    //
                    //    blRegular = true;
                    //}
                    ////--
                    //if (lngBasicInfoID == 0)
                    //{
                    //    //if (dgvDeductees.Rows.Count > 0)
                    //    //    dgvDeductees.Rows.Clear();
                    //    return;
                    //}
                    ////--
                    //if (TDSMAN.Classes.TDSMAN.T_PANVerificationModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                    //    LoadDeducteeGridCorr(lngBasicInfoID);
                    //else
                    //    LoadDeducteeGrid(lngBasicInfoID);
                    ////
                    //this.Cursor = Cursors.Default;
                    ////
                    //grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                    ////
                    //--
                    break;

                case enmRequestType.LogOff:
                    ArrayList objList = new ArrayList();
                    objList.Add(enmRequestType.LogOff);
                    //-------------------------------------------
                    pgTimer.Start();
                    //-------------------------------------------
                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync(objList);

                    break;

            }
        }

        #endregion

        

        //-- Added By Abhishek Dey On 22/05/2018 --
        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnBulkPANNameValidationImprovedVersion");
            Tan.ShowDialog();
            //--------------
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        

        

        //-----------------------------------------

        #endregion

        

        

        
        #region dgvDeductees_CurrentCellDirtyStateChanged
        private void dgvDeductees_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDeductees.IsCurrentCellDirty)
            {
                dgvDeductees.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        #endregion


        #region bgwTaxComputation_DoWork
        private void bgwTaxComputation_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //
            string strTaxation115BAC = "";
            //int lngAsstId = Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
            double dblTotalTaxableIncome = 0; string strCategory = "";
            double dblOutCalculatedTax = 0, dblOutCalculatedECess = 0, dblOutCalculatedSurcharge = 0, dblOutCalculatedTaxCredit = 0, dblOutTaxonTotalIncomeB4TaxCredit = 0, dblOutTaxonTotalIncomeB4TaxCreditMarginalRelief = 0;
            //int intI = 0;
            int  intSDRecords = 0;
            double dblSDValue = 0;
            //
            for (int i = 0; i <= dgvDeductees.RowCount - 1; i++)
            {
                //
                if (dgvDeductees.Rows[i].Cells[T_Grid.SECTION_115BAC_FLAG].Value.ToString() == "")
                    strTaxation115BAC = T_TRUE_FALSE.FALSE.ToString();
                else
                    strTaxation115BAC = T_TRUE_FALSE.TRUE.ToString();
                //
                dblOutCalculatedTax = TdsMan.CalculateIncomeTaxAmount(lngAsstId,
                                                                dgvDeductees.Rows[i].Cells[T_Grid.CATEGORY].Value.ToString(),
                                                               Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TOTAL_INCOME].Value.ToString()),
                                                                strTaxation115BAC,
                                                               out dblOutCalculatedECess,
                                                               out dblOutCalculatedSurcharge,
                                                               out dblOutCalculatedTaxCredit,
                                                               out dblOutTaxonTotalIncomeB4TaxCredit,
                                                               out dblOutTaxonTotalIncomeB4TaxCreditMarginalRelief);
                dblOutCalculatedTax = Math.Round(dblOutCalculatedTax, 0);
                //
                 if (dblOutCalculatedTax >= 0)
                {
                    //lblTaxDeductingRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedTax)));
                    //lblTaxDeductingRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedTax)));
                    //dblOutCalculatedECess.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedECess)));
                    //dblOutCalculatedSurcharge.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedSurcharge)));
                    ////
                    //lblTaxOnTotalIncomeCalculated.Text = string.Format("{0:0.00}", dblTaxonTotalIncomeB4TaxCredit);
                }
                else
                {
                    //lblTaxDeductingRebateCalculated.Text = "0.00";
                    dblOutCalculatedECess = 0;
                    dblOutCalculatedSurcharge = 0;
                    //txtRebate.Text = "0.00";
                }
                if (Convert.ToInt32(lngAsstId) >= T_FinancialYearID.F2018_19ID)
                {
                    //lblRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedTaxCredit)));
                    //lblTaxOnTotalIncomeCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCredit)));

                    dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value = string.Format("{0:0.00}", dblOutCalculatedTaxCredit);
                    dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblOutTaxonTotalIncomeB4TaxCredit)));
                    //-- 2025/05/25
                    if (Convert.ToInt32(lngAsstId) >= T_FinancialYearID.F2023_24ID)
                    {
                        if (dblOutTaxonTotalIncomeB4TaxCreditMarginalRelief > 0)
                        {
                            dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblOutTaxonTotalIncomeB4TaxCreditMarginalRelief)));
                            dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblOutTaxonTotalIncomeB4TaxCreditMarginalRelief - dblOutTaxonTotalIncomeB4TaxCredit)));
                        }
                    }
                }
                else
                {
                    if (dblOutCalculatedTaxCredit <= dblOutTaxonTotalIncomeB4TaxCredit)
                    {
                        //lblTaxOnTotalIncomeCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCredit)) - Convert.ToDouble(Convert.ToString(dblCalculatedTaxCredit)));
                        dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value = string.Format("{0:0.00}", Convert.ToDouble(dblOutTaxonTotalIncomeB4TaxCredit - dblOutCalculatedTaxCredit));
                    }
                }
                //
                //
                if (dblOutCalculatedTaxCredit > dblOutTaxonTotalIncomeB4TaxCredit)
                {
                    //lblRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblOutTaxonTotalIncomeB4TaxCredit)));
                    dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value = string.Format("{0:0.00}", dblOutTaxonTotalIncomeB4TaxCredit);
                    if(dblOutCalculatedTax < 0)
                        dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value = string.Format("{0:0.00}", dblOutTaxonTotalIncomeB4TaxCredit); //-- 2025/05/25
                    else
                        dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value = "0.00";
                }
                //
                //dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value = string.Format("{0:0.00}", dblOutCalculatedTaxCredit);
                dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_COMPUTED].Value = string.Format("{0:0.00}", dblOutCalculatedSurcharge);
                dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_COMPUTED].Value = string.Format("{0:0.00}", dblOutCalculatedECess);
                //-- 2024/01/18
                dgvDeductees.Rows[i].Cells[T_Grid.TAX_DIFF].Value = string.Format("{0:0.00}", (Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value) - Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value)) - (Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value) - Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_ENTERED].Value)));
                //
                if (Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value) + Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_ENTERED].Value) 
                    != Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value) + Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value))
                {
                    //
                    //txtDifferenceFound.Text = (intI + 1).ToString();
                    intI = intI + 1;
                    //
                    //dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Style.BackColor = Color.Yellow;
                    dgvDeductees.Rows[i].Cells[T_Grid.TAX_DIFF].Style.BackColor = Color.Yellow;
                    //dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified";
                    //dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value = string.Format("{0:0.00}", dblOutCalculatedTaxCredit);
                    //dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_COMPUTED].Value = string.Format("{0:0.00}", dblOutCalculatedSurcharge);
                    //dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_COMPUTED].Value = string.Format("{0:0.00}", dblOutCalculatedECess);
                    //
                    dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].ToolTipText = "Rebate : " + string.Format("{0:0.00}", dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value) + "\nSurcharge : " + string.Format("{0:0.00}", dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_COMPUTED].Value) + "\nEdu Cess : " + string.Format("{0:0.00}", dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_COMPUTED].Value);
                    dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].ToolTipText = "Rebate : " + string.Format("{0:0.00}", dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_ENTERED].Value) + "\nSurcharge : " + string.Format("{0:0.00}", dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_ENTERED].Value) + "\nEdu Cess : " + string.Format("{0:0.00}", dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_ENTERED].Value);
                    //-- 2024/01/18
                    if (Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value) < Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value))
                    {
                        intSDRecords = intSDRecords + 1;
                        txtShortDeductionRecords.Text = intSDRecords.ToString();
                        //
                        dblSDValue = dblSDValue + (Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value) - Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value));
                        txtShortDeductionValue.Text = string.Format("{0:0.00}", dblSDValue);
                    }
                }
                //
            }
            txtDifferenceFound.Text = intI.ToString();
            //
            this.Cursor = Cursors.Default;
        }
        #endregion

        #region dgvDeductees_CellContentClick
        private void dgvDeductees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR)
                {
                    if (e.RowIndex != -1)
                    {
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvDeductees.Rows[e.RowIndex].Cells[0];
                        //if (cell.Value == null) return;
                        if (cell.Value == null || (bool)cell.Value == true)
                        {
                            cell.Value = false;
                        }
                        else if ((bool)cell.Value == false)
                        {
                            cell.Value = true;
                        }

                    }
                }
            }
            catch (Exception ERR)
            {

            }
        }

        #endregion

        #region bgwTaxComputation_RunWorkerCompleted
        private void bgwTaxComputation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //btnVerification.Text = strbtnVerification;
                //btnVerification.ForeColor = Color.Black;
            }
            //cmnService.J_UserMessage(intI.ToString());
            txtDifferenceFound.Text = intI.ToString();
            intI = 0;
            btnDownload.Enabled = true;
            //-- 2025/01/18
            if (cmnService.J_ReturnDoubleValue(txtDifferenceFound.Text) > 0)
            {
                btnUpdateSalaryDetails.Enabled = true;
                btnUpdateSalaryDetails.BackColor = Color.Yellow;
            }
            //------------------
            cmnService.J_UserMessage("Computation completed");
        }
        #endregion


        #region bgwTaxComputation_ProgressChanged
        private void bgwTaxComputation_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }
        #endregion


        #region btnCloseUpadateMaster_Click
        private void btnCloseUpadateMaster_Click(object sender, EventArgs e)
        {
            grpExportData.Visible = false;
            //
            grpRegularReturn.Enabled = true;
            dgvDeductees.Enabled = true;
            grpSummary.Enabled = true;
            grpButtons.Enabled = true;
        }
        #endregion


        #region btnGoExport_Click
        private void btnGoExport_Click(object sender, EventArgs e)
        {
            
        }
        #endregion


        #region btnDownload_Click
        private void btnDownload_Click(object sender, EventArgs e)
        {
            grpExportData.Visible = true;
            if (txtDifferenceFound.Text == "0")
                rbnAllData.Checked = true;
            else
                rbnDifference.Checked = true;
            //
            grpRegularReturn.Enabled = false;
            dgvDeductees.Enabled = false;
            grpSummary.Enabled = false;
            grpButtons.Enabled = false;

        }
        #endregion

        #region btnStart_Click
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDeductees.Rows.Count == 0)
                {
                    cmnService.J_UserMessage("No record found");
                    return;
                }
                //
                if (cmnService.J_UserMessage("Proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //
                this.Cursor = Cursors.WaitCursor;
                //
                btnUpdateSalaryDetails.Enabled = false;
                btnUpdateSalaryDetails.BackColor = Color.Lavender;
                //
                bgwTaxComputation.RunWorkerAsync();
            }
            catch(Exception err)
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region dgvDeductees_CellValueChanged
        private void dgvDeductees_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR)
            //{
            //    if (e.RowIndex != -1)
            //    {
            //        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvDeductees.Rows[e.RowIndex].Cells[0];
            //        //DataGridViewCell cellChildMenuID = (DataGridViewCell)dgvDeductees.Rows[e.RowIndex].Cells[1];
            //        //if (cell.Value == null || (bool)cell.Value == false)
            //        if ((bool)cell.Value == false)
            //        {
            //            //grdvNewDescription.MultiSelect = false;
            //            //grdvNewDescription.Rows[e.RowIndex].Selected = true;
            //            //cell.Value = true;
            //            //cmnService.J_UserMessage("UNCHECK"); 
            //            //strSQL = "DELETE FROM TRN_USER_MENU_ACCESS WHERE SETUP_ID =" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + " AND CHILD_MENU_ID = " + Convert.ToInt32(cellChildMenuID.Value.ToString());
            //            //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            //            lngSelectedGrid = lngSelectedGrid - 1;
            //        }
            //        else if ((bool)cell.Value == true)
            //        {
            //            //grdvNewDescription.Rows[e.RowIndex].Selected = false;
            //            //cell.Value = false;
            //            //cmnService.J_UserMessage("CHECK");
            //            //strSQL = "INSERT INTO TRN_USER_MENU_ACCESS (SETUP_ID, CHILD_MENU_ID) VALUES (" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + "," + Convert.ToInt32(cellChildMenuID.Value.ToString()) + ")";
            //            //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            //            lngSelectedGrid = lngSelectedGrid + 1;
            //        }
            //    }
            //}
        }

        #endregion


        #region CREATE EXCEL FILE
        // SOURCE PATH : http://csharp.net-informations.com/excel/csharp-create-excel.htm
        private bool CREATE_EXCEL_FILE(string ExcelFilePath)
        {
            try
            {
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


        #region btnGoExport_MouseClick
        private void btnGoExport_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                cntxtMnuExportData.Show(btnGoExport, new Point(e.X, e.Y));
        }
        #endregion

        #region mnuCntxtMenuExportToExcel_Click
        private void mnuCntxtMenuExportToExcel_Click(object sender, EventArgs e)
        {
            string strExcelPath = "", strExcelFile = "";
            try
            {
                //
                if (dgvDeductees.RowCount == 0)
                {
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("No records for Export.");
                    return;
                }
                //
                if (rbnDifference.Checked == true)
                {
                    if (txtDifferenceFound.Text == "0")
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("No 'Difference' records for Export.");
                        return;
                    }
                }
                // Create a new instance of FolderBrowserDialog.
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                // A new folder button will display in FolderBrowserDialog.
                folderBrowserDlg.ShowNewFolderButton = true;
                //Show FolderBrowserDialog
                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                if (dlgResult.Equals(DialogResult.OK))
                {
                    //Show selected folder path in textbox1.
                    strExcelPath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                else
                    return;
                //--
                if (rbnAllData.Checked == true)
                    strExcelFile = "TAX_COMPUTATION_" + cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10) + "-" + cmbFinancialYear.Text + "_ALL.XLS";
                else
                    strExcelFile = "TAX_COMPUTATION_" + cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10) + "-" + cmbFinancialYear.Text + "_DIFF.XLS";
                strExcelPath = Path.Combine(strExcelPath, strExcelFile);
                //--
                this.Cursor = Cursors.WaitCursor;
                //
                #region T_tblTEMP_EXPORT_TAX_COMPUTATION
                dmlService.J_BeginTransaction();
                //MessageBox.Show("2");
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION) == true)
                {
                    //MessageBox.Show("2.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION + "";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //MessageBox.Show("3");
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION) == false)
                {
                    //MessageBox.Show("3.1");
                    strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION + @" (
                                            " + cmnService.J_GetDataType("EXPORT_TAX_COMPUTATION_ID", J_Identity.YES) + @",
                                            " + cmnService.J_GetDataType("RUNNING_SERIAL_NO", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("PAN", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("CATEGORY", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("REGIME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("TAXABLE_INCOME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("TAX_PAYABLE", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("REBATE", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("SURCHARGE", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EDU_CESS", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("TAX_PAYABLE_COMPUTED", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("REBATE_COMPUTED", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("SURCHARGE_COMPUTED", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EDU_CESS_COMPUTED", J_ColumnType.String, 255) + @")";
                    //MessageBox.Show(strSQL);
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                #endregion
                //
                for (int i = 0; i < dgvDeductees.RowCount; i++)
                {
                    if (rbnAllData.Checked == true)
                    {
                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION + "(" +
                                                "            RUNNING_SERIAL_NO," +
                                                "            PAN," +
                                                "            EMPLOYEE_NAME," +
                                                "            CATEGORY," +
                                                "            REGIME," +
                                                "            TAXABLE_INCOME," +
                                                "            TAX_PAYABLE," +
                                                "            REBATE," +
                                                "            SURCHARGE," +
                                                "            EDU_CESS," +
                                                "            TAX_PAYABLE_COMPUTED," +
                                                "            REBATE_COMPUTED," +
                                                "            SURCHARGE_COMPUTED," +
                                                "            EDU_CESS_COMPUTED) " +
                                                "     VALUES('" + dgvDeductees.Rows[i].Cells[T_Grid.SL_NO].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.EMPLOYEE_PAN].Value.ToString() + "'," +
                                                "            '" + cmnService.J_ReplaceQuote(dgvDeductees.Rows[i].Cells[T_Grid.EMPLOYEE_NAME].Value.ToString()) + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.CATEGORY].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.SECTION_115BAC_FLAG].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TOTAL_INCOME].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_COMPUTED].Value.ToString() + "')";
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        dmlService.J_Commit();
                    }
                    else if (rbnDifference.Checked == true)
                    {
                        if (string.Format("{0:0.00}", Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value.ToString())) != dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value.ToString())
                        {
                            strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION + "(" +
                                                "            RUNNING_SERIAL_NO," +
                                                "            PAN," +
                                                "            EMPLOYEE_NAME," +
                                                "            CATEGORY," +
                                                "            REGIME," +
                                                "            TAXABLE_INCOME," +
                                                "            TAX_PAYABLE," +
                                                "            REBATE," +
                                                "            SURCHARGE," +
                                                "            EDU_CESS," +
                                                "            TAX_PAYABLE_COMPUTED," +
                                                "            REBATE_COMPUTED," +
                                                "            SURCHARGE_COMPUTED," +
                                                "            EDU_CESS_COMPUTED) " +
                                                "     VALUES('" + dgvDeductees.Rows[i].Cells[T_Grid.SL_NO].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.EMPLOYEE_PAN].Value.ToString() + "'," +
                                                "            '" + cmnService.J_ReplaceQuote(dgvDeductees.Rows[i].Cells[T_Grid.EMPLOYEE_NAME].Value.ToString()) + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.CATEGORY].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.SECTION_115BAC_FLAG].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TOTAL_INCOME].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_COMPUTED].Value.ToString() + "')";
                            //-----------------------------------------------------------
                            dmlService.J_BeginTransaction();
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                dmlService.J_Rollback();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            dmlService.J_Commit();
                            //MessageBox.Show(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value.ToString() + " = " +  dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value.ToString());
                        }

                    }
                }
                //
                //--
                if (CREATE_EXCEL_FILE(strExcelPath) == false)
                {
                    cmnService.J_UserMessage("Some error occurred while creating Excel file");
                    this.Cursor = Cursors.Default;
                    return;
                }
                //--
                if (CREATE_NEW_WORKSHEET(strExcelPath, "EMPLOYEE DETAILS") == false)
                {
                    cmnService.J_UserMessage("Some error occurred while creating Excel sheet");
                    this.Cursor = Cursors.Default;
                    return;
                }
                //--- DELETE WORKSHEET
                if (DELETE_WORKSHEET(strExcelPath, "Sheet1") == false)
                {
                    this.Cursor = Cursors.Default;
                    //return;
                }
                //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                if (DELETE_WORKSHEET(strExcelPath, "Sheet2") == false) //return;
                {
                    this.Cursor = Cursors.Default;
                }
                //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                if (DELETE_WORKSHEET(strExcelPath, "Sheet3") == false) //return;
                {
                    this.Cursor = Cursors.Default;
                }
                //
                string strSerialNo = "[Serial No]", strPAN = "PAN", strEmployeeName = "[Name]", strCategory = "Category", strRegime = "Regime", strTaxableIncome = "[Taxable Income]";
                string strTaxPayable = "[Tax Payable]", strRebate = "Rebate", strSurcharge = "Surcharge", strEduCess = "[Edu Cess]", strTaxPayableComputed = "[Tax Payable Computed]", strRebateComputed = "[Rebate Computed]", strSurchargeComputed = "[Surcharge Computed]", strEduCessComputed = "[Edu Cess Computed]";
                //
                strSQL = "SELECT RUNNING_SERIAL_NO       AS " + strSerialNo + "," +
                    "            PAN                     AS " + strPAN + "," +
                    "            EMPLOYEE_NAME           AS " + strEmployeeName + "," +
                    "            CATEGORY                AS " + strCategory + "," +
                    "            REGIME                  AS " + strRegime + " ," +
                    "            TAXABLE_INCOME          AS " + strTaxableIncome + " ," +
                    "            TAX_PAYABLE             AS " + strTaxPayable + " ," +
                    "            REBATE                  AS " + strRebate + " ," +
                    "            SURCHARGE               AS " + strSurcharge + " ," +
                    "            EDU_CESS                AS " + strEduCess + " ," +
                    "            TAX_PAYABLE_COMPUTED    AS " + strTaxPayableComputed + " ," +
                    "            REBATE_COMPUTED         AS " + strRebateComputed + " ," +
                    "            SURCHARGE_COMPUTED      AS " + strSurchargeComputed + " ," +
                    "            EDU_CESS_COMPUTED       AS " + strEduCessComputed + " " +
                    "     FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION + " ORDER BY EXPORT_TAX_COMPUTATION_ID";
                //
                if (ExportToExcelFromSQL(strSQL, "EMPLOYEE DETAILS", strExcelPath) == false)
                {
                    cmnService.J_UserMessage("Export data failed ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return;
                }
                //--;
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("Data Exported Successfully..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                //-
                dmlService.Dispose();
                //this.Close();
                //this.Dispose();
                //
                System.Diagnostics.Process.Start(strExcelPath);
                //
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region mnuCntxtMenuExportToCSV_Click
        private void mnuCntxtMenuExportToCSV_Click(object sender, EventArgs e)
        {
            string strExcelPath = "", strExcelFile = "";
            try
            {
                //
                if (dgvDeductees.RowCount == 0)
                {
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("No records for Export.");
                    return;
                }
                //
                if (rbnDifference.Checked == true)
                {
                    if (txtDifferenceFound.Text == "0")
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("No 'Difference' records for Export.");
                        return;
                    }
                }
                // Create a new instance of FolderBrowserDialog.
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                // A new folder button will display in FolderBrowserDialog.
                folderBrowserDlg.ShowNewFolderButton = true;
                //Show FolderBrowserDialog
                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                if (dlgResult.Equals(DialogResult.OK))
                {
                    //Show selected folder path in textbox1.
                    strExcelPath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                else
                    return;
                //--
                if (rbnAllData.Checked == true)
                    strExcelFile = "TAX_COMPUTATION_" + cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10) + "-" + cmbFinancialYear.Text + "_ALL.CSV";
                else
                    strExcelFile = "TAX_COMPUTATION_" + cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10) + "-" + cmbFinancialYear.Text + "_DIFF.CSV";
                strExcelPath = Path.Combine(strExcelPath, strExcelFile);
                //--
                this.Cursor = Cursors.WaitCursor;
                //
                #region T_tblTEMP_EXPORT_TAX_COMPUTATION
                dmlService.J_BeginTransaction();
                //MessageBox.Show("2");
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION) == true)
                {
                    //MessageBox.Show("2.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION + "";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //MessageBox.Show("3");
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION) == false)
                {
                    //MessageBox.Show("3.1");
                    strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION + @" (
                                            " + cmnService.J_GetDataType("EXPORT_TAX_COMPUTATION_ID", J_Identity.YES) + @",
                                            " + cmnService.J_GetDataType("RUNNING_SERIAL_NO", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("PAN", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("CATEGORY", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("REGIME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("TAXABLE_INCOME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("TAX_PAYABLE", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("REBATE", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("SURCHARGE", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EDU_CESS", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("TAX_PAYABLE_COMPUTED", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("REBATE_COMPUTED", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("SURCHARGE_COMPUTED", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EDU_CESS_COMPUTED", J_ColumnType.String, 255) + @")";
                    //MessageBox.Show(strSQL);
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                #endregion
                //
                for (int i = 0; i < dgvDeductees.RowCount; i++)
                {
                    if (rbnAllData.Checked == true)
                    {
                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION + "(" +
                                                "            RUNNING_SERIAL_NO," +
                                                "            PAN," +
                                                "            EMPLOYEE_NAME," +
                                                "            CATEGORY," +
                                                "            REGIME," +
                                                "            TAXABLE_INCOME," +
                                                "            TAX_PAYABLE," +
                                                "            REBATE," +
                                                "            SURCHARGE," +
                                                "            EDU_CESS," +
                                                "            TAX_PAYABLE_COMPUTED," +
                                                "            REBATE_COMPUTED," +
                                                "            SURCHARGE_COMPUTED," +
                                                "            EDU_CESS_COMPUTED) " +
                                                "     VALUES('" + dgvDeductees.Rows[i].Cells[T_Grid.SL_NO].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.EMPLOYEE_PAN].Value.ToString() + "'," +
                                                "            '" + cmnService.J_ReplaceQuote(dgvDeductees.Rows[i].Cells[T_Grid.EMPLOYEE_NAME].Value.ToString()) + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.CATEGORY].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.SECTION_115BAC_FLAG].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TOTAL_INCOME].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_COMPUTED].Value.ToString() + "')";
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        dmlService.J_Commit();
                    }
                    else if (rbnDifference.Checked == true)
                    {
                        if (string.Format("{0:0.00}", Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value.ToString())) != dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value.ToString())
                        {
                            strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION + "(" +
                                                "            RUNNING_SERIAL_NO," +
                                                "            PAN," +
                                                "            EMPLOYEE_NAME," +
                                                "            CATEGORY," +
                                                "            REGIME," +
                                                "            TAXABLE_INCOME," +
                                                "            TAX_PAYABLE," +
                                                "            REBATE," +
                                                "            SURCHARGE," +
                                                "            EDU_CESS," +
                                                "            TAX_PAYABLE_COMPUTED," +
                                                "            REBATE_COMPUTED," +
                                                "            SURCHARGE_COMPUTED," +
                                                "            EDU_CESS_COMPUTED) " +
                                                "     VALUES('" + dgvDeductees.Rows[i].Cells[T_Grid.SL_NO].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.EMPLOYEE_PAN].Value.ToString() + "'," +
                                                "            '" + cmnService.J_ReplaceQuote(dgvDeductees.Rows[i].Cells[T_Grid.EMPLOYEE_NAME].Value.ToString()) + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.CATEGORY].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.SECTION_115BAC_FLAG].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TOTAL_INCOME].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_ENTERED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_COMPUTED].Value.ToString() + "'," +
                                                "            '" + dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_COMPUTED].Value.ToString() + "')";
                            //-----------------------------------------------------------
                            dmlService.J_BeginTransaction();
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                dmlService.J_Rollback();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            dmlService.J_Commit();
                            //MessageBox.Show(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value.ToString() + " = " +  dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value.ToString());
                        }

                    }
                }
                //
                //--
                //if (CREATE_EXCEL_FILE(strExcelPath) == false)
                //{
                //    cmnService.J_UserMessage("Some error occurred while creating Excel file");
                //    this.Cursor = Cursors.Default;
                //    return;
                //}
                ////--
                //if (CREATE_NEW_WORKSHEET(strExcelPath, "EMPLOYEE DETAILS") == false)
                //{
                //    cmnService.J_UserMessage("Some error occurred while creating Excel sheet");
                //    this.Cursor = Cursors.Default;
                //    return;
                //}
                ////--- DELETE WORKSHEET
                //if (DELETE_WORKSHEET(strExcelPath, "Sheet1") == false)
                //{
                //    this.Cursor = Cursors.Default;
                //    //return;
                //}
                ////prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                //if (DELETE_WORKSHEET(strExcelPath, "Sheet2") == false) //return;
                //{
                //    this.Cursor = Cursors.Default;
                //}
                ////prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                //if (DELETE_WORKSHEET(strExcelPath, "Sheet3") == false) //return;
                //{
                //    this.Cursor = Cursors.Default;
                //}
                //
                string strSerialNo = "[Serial No]", strPAN = "PAN", strEmployeeName = "[Name]", strCategory = "Category", strRegime = "Regime", strTaxableIncome = "[Taxable Income]";
                string strTaxPayable = "[Tax Payable]", strRebate = "Rebate", strSurcharge = "Surcharge", strEduCess = "[Edu Cess]", strTaxPayableComputed = "[Tax Payable Computed]", strRebateComputed = "[Rebate Computed]", strSurchargeComputed = "[Surcharge Computed]", strEduCessComputed = "[Edu Cess Computed]";
                //
                strSQL = "SELECT RUNNING_SERIAL_NO       AS " + strSerialNo + "," +
                    "            PAN                     AS " + strPAN + "," +
                    "            EMPLOYEE_NAME           AS " + strEmployeeName + "," +
                    "            CATEGORY                AS " + strCategory + "," +
                    "            REGIME                  AS " + strRegime + " ," +
                    "            TAXABLE_INCOME          AS " + strTaxableIncome + " ," +
                    "            TAX_PAYABLE             AS " + strTaxPayable + " ," +
                    "            REBATE                  AS " + strRebate + " ," +
                    "            SURCHARGE               AS " + strSurcharge + " ," +
                    "            EDU_CESS                AS " + strEduCess + " ," +
                    "            TAX_PAYABLE_COMPUTED    AS " + strTaxPayableComputed + " ," +
                    "            REBATE_COMPUTED         AS " + strRebateComputed + " ," +
                    "            SURCHARGE_COMPUTED      AS " + strSurchargeComputed + " ," +
                    "            EDU_CESS_COMPUTED       AS " + strEduCessComputed + " " +
                    "     FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EXPORT_TAX_COMPUTATION + " ORDER BY EXPORT_TAX_COMPUTATION_ID";
                //
                if (ExportToCSV(strSQL, strExcelPath) == false)
                {
                    cmnService.J_UserMessage("Export data failed ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return;
                }
                //--;
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("Data Exported Successfully..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                //-
                dmlService.Dispose();
                //this.Close();
                //this.Dispose();
                //
                System.Diagnostics.Process.Start(strExcelPath);
                //
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0081", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion


        #region BtnUpdateSalaryDetails_Click
        private void BtnUpdateSalaryDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmnService.J_UserMessage(txtDifferenceFound.Text + " records will be updated...\nProceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
                //
                bgwTaxUpdation.RunWorkerAsync();
                
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region BgwTaxUpdation_DoWork
        private void BgwTaxUpdation_DoWork(object sender, DoWorkEventArgs e)
        {
            //--
            int iCount = 0;
            for (int i = 0; i <= dgvDeductees.RowCount - 1; i++)
            {
                this.Cursor = Cursors.WaitCursor;
                //
                if (Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_ENTERED].Value) + Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_ENTERED].Value) 
                    < Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value) + Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value))
                {
                    iCount = iCount + 1;
                    //btnUpdateSalaryDetails.Text = "Updating : " + txtDifferenceFound.Text + "/" + iCount.ToString();
                    //"       (TRN_SALARY_DETAILS.TAX_TOTAL_INCOME_B4_REBATE - TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT)   AS TAX_TOTAL_INCOME," +
                    //   "       TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT AS REBATE_US_87A_AMOUNT," +
                    //   "       TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME  AS SCHG_TOTAL_INCOME," +
                    //   "       TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME AS ECESS_TOTAL_INCOME," +
                    strSQL = @"UPDATE  TRN_SALARY_DETAILS SET 
                                           TAX_TOTAL_INCOME_B4_REBATE     = " + Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value) + @",
                                           TAX_TOTAL_INCOME = " + Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_PAYABLE_COMPUTED].Value) + @",
                                           SCHG_TOTAL_INCOME    = " + Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_SURCH_COMPUTED].Value) + @",
                                           ECESS_TOTAL_INCOME   = " + Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_EDU_CESS_COMPUTED].Value) + @", 
                                           REBATE_US_87A_AMOUNT = " + Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.TAX_REBATE_87_COMPUTED].Value) + @"
                                   WHERE   SALARY_DETAILS_ID    = " + Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.SALARY_DETAILS_ID].Value);
                    //MessageBox.Show(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //-- 
                    strSQL = @"UPDATE  TRN_SALARY_DETAILS SET 
                                           TAX_PAYABLE_AGGREGATE = (TAX_TOTAL_INCOME_B4_REBATE + SCHG_TOTAL_INCOME + ECESS_TOTAL_INCOME),
                                           TAX_PAYABLE           = (TAX_TOTAL_INCOME_B4_REBATE + SCHG_TOTAL_INCOME + ECESS_TOTAL_INCOME) - US_89_LESS,
                                           SHORTFALL_TAX         =  ((TAX_TOTAL_INCOME_B4_REBATE + SCHG_TOTAL_INCOME + ECESS_TOTAL_INCOME) - US_89_LESS) - TOTAL_TDS_DEDUCTED
                                   WHERE   SALARY_DETAILS_ID     = " + Convert.ToDouble(dgvDeductees.Rows[i].Cells[T_Grid.SALARY_DETAILS_ID].Value);
                    //MessageBox.Show(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
            }
            this.Cursor = Cursors.Default;
            ////--
            //btnUpdateSalaryDetails.Text = "&Update";
            //cmnService.J_UserMessage(txtDifferenceFound.Text + " records updated...");
        }
        #endregion

        #region BgwTaxUpdation_RunWorkerCompleted
        private void BgwTaxUpdation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //--
            btnUpdateSalaryDetails.Text = "&Update";
            cmnService.J_UserMessage(txtDifferenceFound.Text + " records has been updated...");
        }
        #endregion


        #region BgwTaxUpdation_ProgressChanged
        private void BgwTaxUpdation_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            btnUpdateSalaryDetails.Text = e.ProgressPercentage.ToString() + "% Completed";
        }
        #endregion

        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath, string WorksheetName)
        {
            try
            {
                //
                //if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
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
                //if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22        
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

        #region ExportToExcelFromSQL
        private bool ExportToExcelFromSQL(string strSQL, string SheetName, string ExcelPath)
        {
            //-- 20/02/2018 --
            //GC.Collect();
            GC.WaitForPendingFinalizers();

            DataSet myDataSet;
            //----------------
            //
            try
            {
                //if (rbnCSVOption.Checked == true)  //-- 2019/01/22
                //{
                //    ExportToCSV(strSQL, Path.Combine(Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text), SheetName + ".csv"));
                //    return true;
                //}
                //else
                //{
                myDataSet = new DataSet();
                myDataSet = dmlService.J_ExecSqlReturnDataSet(strSQL);
                //
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                Microsoft.Office.Interop.Excel.Workbooks workbook = app.Workbooks;
                //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 -
                //
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Workbook wb = workbook.Open(ExcelPath,
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
                //}
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
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
        }
        #endregion

    }
}