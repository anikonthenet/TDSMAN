#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnTaxRegimeCalculation 
Version			: 1.0
Start Date		: 10/01/2025
End Date		: 
Last Updated    : 
Tables Used     : 
Module Desc		: 
________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

//~~~~ User Namespaces ~~~~
using TDSMAN.FormMst;
using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

#endregion

#region set ENUM
public enum J_TaxRegimeCalculation
{
    //==============================================================
    SALARY_DETAILS_ID         = 0,
    EMP_ID                    = 1,
    EMP_SERIAL_NO             = 2,
    EMP_PAN                   = 3,
    EMP_NAME                  = 4,
    EMP_CATEGORY              = 5,
    EMP_REGIME                = 6,
    EMP_TAXABLE_INCOME_OLD_REGIME = 7,
    EMP_TAX_AMOUNT_OLD_REGIME     = 8,
    EMP_TAXABLE_INCOME_NEW_REGIME = 9,
    EMP_TAX_AMOUNT_NEW_REGIME = 10,
    EMP_GROSS_SALARY          = 11,
    EMP_SEC10_5_AMOUNT        = 12,
    EMP_SEC10_10_AMOUNT       = 13,
    EMP_SEC10_10A_AMOUNT      = 14,
    EMP_SEC10_10AA_AMOUNT     = 15,
    EMP_SEC10_13A_AMOUNT      = 16,
    EMP_SEC10_14_AMOUNT       = 17,
    EMP_TS_LA_TOTAL           = 18,
    EMP_CURRENT_SALARY        = 19,
    EMP_PREVIOUS_SALARY       = 20,
    EMP_AIS_ITEM_1            = 21,
    EMP_AIS_ITEM_2            = 22,
    EMP_US_16_EA_AMOUNT       = 23,
    EMP_US_16_TE_AMOUNT       = 24,
    EMP_US_16_IA_AMOUNT       = 25,
    EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT = 26,
    EMP_CVIA_SEC80CCD_1B_DED_AMOUNT    = 27,
    EMP_CVIA_SEC80CCD_2_DED_AMOUNT     = 28,
    EMP_CVIA_SEC80D_DED_AMOUNT         = 29,
    EMP_CVIA_SEC80E_DED_AMOUNT         = 30,
    EMP_CVIA_SEC80CCH_DED_AMOUNT       = 31,
    EMP_CVIA_SEC80CCH_1_DED_AMOUNT     = 32,
    EMP_CVIA_SEC80G_DED_AMOUNT         = 33,
    EMP_CVIA_SEC80TTA_DED_AMOUNT       = 34,
    EMP_CVIA_OTH_DED_TOTAL             = 35,
    EMP_REBATE_US87_OLD_REGIME         = 36,
    EMP_REBATE_US87_NEW_REGIME         = 37,
    EMP_EDU_CESS_OLD_REGIME            = 38,
    EMP_EDU_CESS_NEW_REGIME            = 39,
    EMP_SURCHARGE_OLD_REGIME           = 40,
    EMP_SURCHARGE_NEW_REGIME           = 41,
    EMP_GROSS_TAX_OLD_REGIME           = 42,
    EMP_GROSS_TAX_NEW_REGIME           = 43,
    BENEFICIAL_REGIME                  = 44,
    EMP_REGIME_STATUS                  = 45
    //==============================================================
}

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnTaxRegimeCalculation : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region Constructor
        public TrnTaxRegimeCalculation(long BASIC_INFO_ID, long FA_YEAR_ID, string FA_YEAR, string COMPANY_NAME, string COMPANY_TAN)
        {
            lngBASIC_INFO_ID = BASIC_INFO_ID;
            lngFA_YEAR_ID = FA_YEAR_ID;
            strFA_YEAR = FA_YEAR;
            strCOMPANY_NAME = COMPANY_NAME;
            strCOMPANY_TAN = COMPANY_TAN;
            //
            InitializeComponent();
            ////--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            ////--
        }

        public TrnTaxRegimeCalculation()//long BASIC_INFO_ID, long FA_YEAR_ID, string FA_YEAR, string COMPANY_NAME, string COMPANY_TAN)
        {
            //lngBASIC_INFO_ID = BASIC_INFO_ID;
            //lngFA_YEAR_ID = FA_YEAR_ID;
            //strFA_YEAR = FA_YEAR;
            //strCOMPANY_NAME = COMPANY_NAME;
            //strCOMPANY_TAN = COMPANY_TAN;
            //
            InitializeComponent();
            ////--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            ////--
        }
        #endregion

        #region Objects & Variables declaration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //
        long lngBASIC_INFO_ID = 0, lngFA_YEAR_ID = 0;
        string strFA_YEAR = "", strCOMPANY_NAME = "", strCOMPANY_TAN = "";
        bool blResize = true;
        //
        string strSQL = ""; int intGridRowIteration = 0;

        bool blnSelectComboExit = false;
        #endregion

        #region User Defined Events

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

        #region TrnTaxRegimeCalculation_Load
        private void TrnTaxRegimeCalculation_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            //--
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            lblTitle.Text = "Tax Calculator – Old / New Regime";
            //
            lblCompanyWithTAN.Text = strCOMPANY_NAME + " - " + strCOMPANY_TAN;
            lblFAYear.Text = strFA_YEAR;
            //
            BtnSave.Enabled = false;
            BtnSave.BackColor = Color.LightGray;
            BtnCancel.BackColor = Color.Lavender;
            //
            //--
            #region CREATE TEMP TABLE
            dmlService.J_BeginTransaction();
            //MessageBox.Show("2");
            if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION + "") == true)
            {
                //MessageBox.Show("2.1");
                strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION + "";
                dmlService.J_ExecSql(strSQL);
            }
            dmlService.J_Commit();
            //MessageBox.Show("3");
            dmlService.J_BeginTransaction();
            if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION) == false)
            {
                //MessageBox.Show("3.1");
                strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION + @" (
                                        " + cmnService.J_GetDataType("TAX_REGIME_COMPARISION_ID", J_Identity.YES) + @", 
                                        " + cmnService.J_GetDataType("BASIC_INFO_ID", J_ColumnType.Long) + @",
                                        " + cmnService.J_GetDataType("SALARY_DETAILS_ID", J_ColumnType.Long) + @",
                                        " + cmnService.J_GetDataType("EMP_ID", J_ColumnType.Long) + @",
                                        " + cmnService.J_GetDataType("EMP_SERIAL_NO", J_ColumnType.Integer) + @",
                                        " + cmnService.J_GetDataType("EMP_PAN", J_ColumnType.String) + @",
                                        " + cmnService.J_GetDataType("EMP_NAME", J_ColumnType.String) + @",
                                        " + cmnService.J_GetDataType("EMP_CATEGORY", J_ColumnType.String) + @",
                                        " + cmnService.J_GetDataType("EMP_REGIME", J_ColumnType.Integer) + @",
                                        " + cmnService.J_GetDataType("EMP_GROSS_SALARY", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_5_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_10_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_10A_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_10AA_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_13A_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_TOTAL_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_14_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_TS_LA_TOTAL", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CURRENT_SALARY", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_PREVIOUS_SALARY", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_AIS_ITEM_1", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_AIS_ITEM_2", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_NET_INCOME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_US_16_EA_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_US_16_TE_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_US_16_IA_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CH_VI_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80CCD_1B_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80CCD_2_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80D_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80E_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80CCH_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80CCH_1_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80G_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80TTA_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_OTH_DED_TOTAL", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_TAXABLE_INCOME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_TAX_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_OLD_TAXABLE_INCOME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_OLD_TAX_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SURCHARGE_OLD_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_EDU_CESS_OLD_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_OLD_TOTAL_TAX", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_NEW_TAXABLE_INCOME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_NEW_TAX_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SURCHARGE_NEW_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_EDU_CESS_NEW_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_NEW_TOTAL_TAX", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_REBATE_US87_OLD_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_REBATE_US87_NEW_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("REGIME_STATUS", J_ColumnType.String) + @")";
                //cmnService.J_UserMessage(strSQL);
                dmlService.J_ExecSql(strSQL);
            }
            dmlService.J_Commit();
            #endregion
            //--
            ////LoadEmployeeGrid();
            ////
            //if (!bgWorker.IsBusy)
            //    bgWorker.RunWorkerAsync();
            ////
            LoadControls();
        }
        #endregion

        #region TrnTaxRegimeCalculation_Activated
        private void TrnTaxRegimeCalculation_Activated(object sender, EventArgs e)
        {
            //
            blResize = false;
            //
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //--
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion


        #region DgvEmployees_CellClick
        private void DgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string strEmployeeDetails = "";
            try
            {
                if (e.ColumnIndex == 45) // J_TaxRegimeCalculation.EMP_REGIME_STATUS.ToString())
                {
                    if(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_REGIME].Value) == "")
                        strEmployeeDetails = Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_PAN].Value) + "  |  " +
                                             Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_NAME].Value) + "  |  " +
                                             "OLD";
                    else
                        strEmployeeDetails = Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_PAN].Value) + "  |  " +
                                             Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_NAME].Value) + "  |  " +
                                             Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_REGIME].Value);
                    //--
                    TrnTaxRegimeComparisonView objTrnTaxRegimeComparisonView = new TrnTaxRegimeComparisonView(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                                                                          cmbFinancialYear.Text,
                                                                                                          strEmployeeDetails,
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_GROSS_SALARY].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_PREVIOUS_SALARY].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_5_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10A_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10AA_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_13A_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_14_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_TS_LA_TOTAL].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_US_16_EA_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_US_16_TE_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_US_16_IA_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_AIS_ITEM_1].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_AIS_ITEM_2].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80CCD_1B_DED_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80CCD_2_DED_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80D_DED_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80E_DED_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80CCH_DED_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80CCH_1_DED_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80G_DED_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80TTA_DED_AMOUNT].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_OTH_DED_TOTAL].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_TAXABLE_INCOME_OLD_REGIME].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_TAXABLE_INCOME_NEW_REGIME].Value)),
                                                                                                          //Convert.ToDouble(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_OLD_REGIME].Value)),
                                                                                                          //Convert.ToDouble(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_NEW_REGIME].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_GROSS_TAX_OLD_REGIME].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_GROSS_TAX_NEW_REGIME].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_REBATE_US87_OLD_REGIME].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_REBATE_US87_NEW_REGIME].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_EDU_CESS_OLD_REGIME].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_EDU_CESS_NEW_REGIME].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_SURCHARGE_OLD_REGIME].Value)),
                                                                                                          cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[dgvEmployees.CurrentRow.Index].Cells[(int)J_TaxRegimeCalculation.EMP_SURCHARGE_NEW_REGIME].Value))                                                                                                     
                                                                                                         );
                    objTrnTaxRegimeComparisonView.ShowDialog();
                }
            }
            catch(Exception err)
            {

            }
        }
        #endregion


        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strExportFilePath = "", strExportFileName = strCOMPANY_TAN + "_" + string.Format("{0:ddMMyy}", System.DateTime.Now.Date) + "_" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".csv";
                // Create a new instance of FolderBrowserDialog.
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                // A new folder button will display in FolderBrowserDialog.
                folderBrowserDlg.ShowNewFolderButton = true;
                //Show FolderBrowserDialog
                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                if (dlgResult.Equals(DialogResult.OK))
                {
                    //Show selected folder path in textbox1.
                    strExportFilePath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                //--
                if (strExportFilePath == "") return;
                //-----------------------------------------------------------
                string[,] strEMP_REGIMEMatrix = {{"EMP_REGIME <> 0", "F", "NEW", "T"},
                                            {"EMP_REGIME = 0", "F", "", "T"}};
                //
                //string[,] strEMP_OLD_TAXABLE_INCOMEMatrix = {{"EMP_REGIME = 0", "F", "NEW", "T"},
                //                            {"EMP_REGIME = 0", "F", "", "T"}};
                //            
                strSQL = "SELECT   EMP_SERIAL_NO       AS [Serial No]," +
                         "         EMP_PAN             AS [PAN]," +
                         "         EMP_NAME            AS [Name]," +
                         "         EMP_CATEGORY        AS [Category]," +
                         "" + cmnService.J_SQLDBFormat(strEMP_REGIMEMatrix, J_SQLColFormat.Case_End) + @" AS [Regime]," +
                         "         EMP_OLD_TAXABLE_INCOME AS [Taxable Income (Old Regime)]," +
                         "         EMP_OLD_TAX_AMOUNT     AS [Tax (Old Regime)]," +
                         "         EMP_NEW_TAXABLE_INCOME AS [Taxable Income (New Regime)]," +
                         "         EMP_NEW_TAX_AMOUNT     AS [Tax (New Regime)] " +
                         "FROM     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION + " " +
                         "WHERE    BASIC_INFO_ID = " + lngBASIC_INFO_ID + " ORDER BY EMP_SERIAL_NO";
                //
                //ExportToCSV(strSQL, Path.Combine(strExportFilePath, strExportFileName));
                ExportGridToCSV(Path.Combine(strExportFilePath, strExportFileName));
                //--
                if (cmnService.J_UserMessage("Export completed...\nDo you want to open the file?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(Path.Combine(strExportFilePath, strExportFileName));
                    return;
                }
            }
            catch
            {

            }

        }
        #endregion

        #region BgWorker_DoWork
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int j = 0;
            double dblStdDeduction = 0, dblGrossSal = 0, dblCurrentEmpSal = 0, dblPreviousEmpSal = 0, dblSection10Amt = 0;
            double dblOtherIncomeAmount1 = 0, dblOtherIncome = 0, dblSection16Amt = 0, dblChapterVIA = 0, dblTotalTaxableIncome = 0, dblTax = 0;
            double dblCalculatedECess = 0, dblCalculatedSurcharge = 0, dblCalculatedTaxCredit = 0, dblTaxonTotalIncomeB4TaxCredit = 0, dblTaxonTotalIncomeB4TaxCreditMarginalRelief = 0;
            string strRegime = "";
            try
            {
                //--
                //this.Cursor = Cursors.WaitCursor;
                //--
                for (int i = j; i <= dgvEmployees.RowCount - 1; i++)
                {
                    dblTax = 0; dblCalculatedECess = 0; dblCalculatedSurcharge = 0; dblCalculatedTaxCredit = 0; dblTaxonTotalIncomeB4TaxCredit = 0; dblTaxonTotalIncomeB4TaxCreditMarginalRelief = 0;
                    //lblStatus.Text = "Processing : " + (i + 1).ToString() + " records out of " + dgvEmployees.RowCount.ToString();
                    intGridRowIteration = i;
                    //###
                    //###
                    if (Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_REGIME].Value).ToString() == "") //-- OLD REGIME
                    {
                        dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_NEW_REGIME].Value = "0.00";
                        dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_GROSS_TAX_OLD_REGIME].Value = Convert.ToDouble(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_OLD_REGIME].Value);
                        //-- CALCULATE AS PER NEW REGIME --
                        dblGrossSal = Convert.ToDouble(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_GROSS_SALARY].Value));
                        dblPreviousEmpSal = Convert.ToDouble(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_PREVIOUS_SALARY].Value));
                        //-- SECTION 10
                        if (lngFA_YEAR_ID >= T_FinancialYearID.F2023_24ID)
                            dblSection10Amt = cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10A_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10AA_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_14_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TS_LA_TOTAL].Value));
                        else
                            dblSection10Amt = cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10A_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10AA_AMOUNT].Value));
                        //-- GETTING THE STANDARD DEDUCTION
                        if (lngFA_YEAR_ID >= T_FinancialYearID.F2024_25ID)
                            strSQL = "SELECT SD_US_16_IA_LIMIT_NEW_REGIME FROM MST_ASSESSMENT WHERE ASST_ID = " + lngFA_YEAR_ID;
                        else
                            strSQL = "SELECT SD_US_16_IA_LIMIT FROM MST_ASSESSMENT WHERE ASST_ID = " + lngFA_YEAR_ID;
                        dblStdDeduction = Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                        dblSection16Amt = dblStdDeduction;
                        //--
                        //-- OTHER INCOME
                        if (cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_AIS_ITEM_1].Value)) < 0)
                            dblOtherIncomeAmount1 = 0;
                        else
                            dblOtherIncomeAmount1 = cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_AIS_ITEM_1].Value));
                        dblOtherIncome = dblOtherIncomeAmount1 + cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_AIS_ITEM_2].Value));
                        //-- CHAPTER VI-A
                        dblChapterVIA = cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80CCD_2_DED_AMOUNT].Value));
                        //-- TOTAL TAXABLE INCOME
                        dblCurrentEmpSal = dblGrossSal - dblSection10Amt;
                        dblTotalTaxableIncome = (dblCurrentEmpSal + dblPreviousEmpSal) + dblOtherIncome - dblStdDeduction - dblChapterVIA;
                        //-- TAX CALCULATION
                        dblTax = TdsMan.CalculateIncomeTaxAmount(Convert.ToInt32(lngFA_YEAR_ID),
                                                                Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CATEGORY].Value),
                                                                dblTotalTaxableIncome,
                                                                "true",
                                                                out dblCalculatedECess,
                                                                out dblCalculatedSurcharge,
                                                                out dblCalculatedTaxCredit,
                                                                out dblTaxonTotalIncomeB4TaxCredit,
                                                                out dblTaxonTotalIncomeB4TaxCreditMarginalRelief);
                        //
                        dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAXABLE_INCOME_NEW_REGIME].Value = string.Format("{0:0.00}", dblTotalTaxableIncome);
                        if (dblTax > 0)
                        {
                            //--  
                            dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_EDU_CESS_NEW_REGIME].Value = string.Format("{0:0.00}", dblCalculatedECess);
                            dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SURCHARGE_NEW_REGIME].Value = string.Format("{0:0.00}", dblCalculatedSurcharge);
                            //--
                            if (dblTaxonTotalIncomeB4TaxCreditMarginalRelief > 0)
                            {
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_REBATE_US87_NEW_REGIME].Value = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCreditMarginalRelief - dblTaxonTotalIncomeB4TaxCredit)));
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_NEW_REGIME].Value = string.Format("{0:0.00}", dblTax + dblTaxonTotalIncomeB4TaxCreditMarginalRelief - dblTaxonTotalIncomeB4TaxCredit + dblCalculatedECess + dblCalculatedSurcharge);// Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCreditMarginalRelief)));
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_GROSS_TAX_NEW_REGIME].Value = string.Format("{0:0.00}", dblTax);
                            }
                            else
                            {
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_GROSS_TAX_NEW_REGIME].Value = string.Format("{0:0.00}", dblTax);
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_NEW_REGIME].Value = string.Format("{0:0.00}", dblTax + dblCalculatedECess + dblCalculatedSurcharge);// dblTax);// dblTaxonTotalIncomeB4TaxCredit);
                            }
                            //--
                        }
                    }
                    else if (Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_REGIME].Value).ToString() == "NEW") //-- NEW REGIME
                    {
                        dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_OLD_REGIME].Value = "0.00";
                        dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_GROSS_TAX_NEW_REGIME].Value = Convert.ToDouble(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_NEW_REGIME].Value);
                        //-- CALCULATE AS PER OLD REGIME --
                        dblGrossSal = Convert.ToDouble(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_GROSS_SALARY].Value));
                        dblPreviousEmpSal = Convert.ToDouble(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_PREVIOUS_SALARY].Value));
                        //-- SECTION 10
                        if (lngFA_YEAR_ID >= T_FinancialYearID.F2023_24ID)
                            dblSection10Amt = cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_5_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10A_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10AA_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_13A_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_14_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TS_LA_TOTAL].Value));
                        else
                            dblSection10Amt = cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_5_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10A_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_10AA_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SEC10_13A_AMOUNT].Value)) +
                                             cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TS_LA_TOTAL].Value));
                        //-- GETTING THE STANDARD DEDUCTION
                        if (lngFA_YEAR_ID >= T_FinancialYearID.F2024_25ID)
                            strSQL = "SELECT SD_US_16_IA_LIMIT FROM MST_ASSESSMENT WHERE ASST_ID = " + lngFA_YEAR_ID;
                        else
                            strSQL = "SELECT SD_US_16_IA_LIMIT FROM MST_ASSESSMENT WHERE ASST_ID = " + lngFA_YEAR_ID;
                        dblStdDeduction = Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                        dblSection16Amt = dblStdDeduction + cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_US_16_EA_AMOUNT].Value)) + cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_US_16_TE_AMOUNT].Value));
                        //-- OTHER INCOME
                        dblOtherIncome = cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_AIS_ITEM_1].Value)) + cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_AIS_ITEM_1].Value));
                        //-- CHAPTER VI-A
                        dblChapterVIA = cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT].Value)) +
                                        cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80CCD_1B_DED_AMOUNT].Value)) +
                                        cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80CCD_2_DED_AMOUNT].Value)) +
                                        cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80D_DED_AMOUNT].Value)) +
                                        cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80E_DED_AMOUNT].Value)) +
                                        cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80CCH_DED_AMOUNT].Value)) +
                                        cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80G_DED_AMOUNT].Value)) +
                                        cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_SEC80TTA_DED_AMOUNT].Value)) +
                                        cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CVIA_OTH_DED_TOTAL].Value));
                        //-- TOTAL TAXABLE INCOME
                        dblCurrentEmpSal = dblGrossSal - dblSection10Amt;
                        dblTotalTaxableIncome = (dblCurrentEmpSal + dblPreviousEmpSal) + dblOtherIncome - dblStdDeduction - dblChapterVIA;
                        //-- TAX CALCULATION
                        dblTax = TdsMan.CalculateIncomeTaxAmount(Convert.ToInt32(lngFA_YEAR_ID),
                                                                Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_CATEGORY].Value),
                                                                dblTotalTaxableIncome,
                                                                "false",
                                                                out dblCalculatedECess,
                                                                out dblCalculatedSurcharge,
                                                                out dblCalculatedTaxCredit,
                                                                out dblTaxonTotalIncomeB4TaxCredit,
                                                                out dblTaxonTotalIncomeB4TaxCreditMarginalRelief);
                        //
                        ////dgvDeductees.Rows[i].Cells[intNameVerified].Value
                        dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAXABLE_INCOME_OLD_REGIME].Value = string.Format("{0:0.00}", dblTotalTaxableIncome);
                        if (dblTax > 0)
                        {
                            //dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_OLD_REGIME].Value = string.Format("{0:0.00}", dblTax + dblCalculatedSurcharge + dblCalculatedECess);
                            //--  
                            dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_EDU_CESS_OLD_REGIME].Value = string.Format("{0:0.00}", dblCalculatedECess);
                            dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_SURCHARGE_OLD_REGIME].Value = string.Format("{0:0.00}", dblCalculatedSurcharge);
                            //--
                            if (dblTaxonTotalIncomeB4TaxCreditMarginalRelief > 0)
                            {
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_REBATE_US87_OLD_REGIME].Value = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCreditMarginalRelief - dblTaxonTotalIncomeB4TaxCredit)));
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_OLD_REGIME].Value = string.Format("{0:0.00}", dblTax + dblTaxonTotalIncomeB4TaxCreditMarginalRelief - dblTaxonTotalIncomeB4TaxCredit + dblCalculatedECess + dblCalculatedSurcharge);// Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCreditMarginalRelief)));
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_GROSS_TAX_OLD_REGIME].Value = string.Format("{0:0.00}", dblTax);
                            }
                            else
                            {
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_GROSS_TAX_OLD_REGIME].Value = string.Format("{0:0.00}", dblTax);
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_OLD_REGIME].Value = string.Format("{0:0.00}", dblTax + dblCalculatedECess + dblCalculatedSurcharge);// dblTax);// dblTaxonTotalIncomeB4TaxCredit);
                            }
                            //--
                        }
                    }
                    //--
                    if ((cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_OLD_REGIME].Value)) +
                        cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_NEW_REGIME].Value))) > 0)
                    {
                        if (cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_OLD_REGIME].Value)) >
                            cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_NEW_REGIME].Value)))
                        {
                            if (Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_REGIME].Value) != "NEW")
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.BENEFICIAL_REGIME].Value = "- New Regime -";
                        }
                        else
                        {
                            if (Convert.ToString(dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.EMP_REGIME].Value) != "")
                                dgvEmployees.Rows[i].Cells[(int)J_TaxRegimeCalculation.BENEFICIAL_REGIME].Value = "- Old Regime -";
                        }
                    }
                    //###
                    //lblStatus.Text = "Processing : " + (i + 1).ToString() + " records out of " + dgvEmployees.RowCount.ToString();
                }
                //--
                //lblStatus.Text = "Total Record(s) : " + dgvEmployees.RowCount.ToString();
            }
            catch (Exception err)
            {
                //this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message);
            }

        }
        #endregion

        #region BgWorker_ProgressChanged
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblStatus.Text = "Processing : " + (intGridRowIteration + 1).ToString() + " records out of " + dgvEmployees.RowCount.ToString();
        }
        #endregion
               
        #region BgWorker_RunWorkerCompleted
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblStatus.Text = "Total Record(s) : " + dgvEmployees.RowCount.ToString();
            BtnSave.Enabled = true;
            BtnSave.BackColor = Color.Lavender;
            this.Cursor = Cursors.Default;
            //
            dgvEmployees.AutoGenerateColumns = false;
            //
            DataGridViewButtonColumn BtnCheckDetails = new DataGridViewButtonColumn();
            {
                //dgvUpdates.Columns
                //BtnShow.Visible = true;
                BtnCheckDetails.HeaderText = "";
                BtnCheckDetails.Width = 110;
                //BtnReturnHealth.Text = " - Check Health - ";
                BtnCheckDetails.Text = "View";
                BtnCheckDetails.ToolTipText = "Check Details";
                BtnCheckDetails.Name = "BtnCheckDetails";
                BtnCheckDetails.FlatStyle = FlatStyle.Popup;
                BtnCheckDetails.UseColumnTextForButtonValue = true;
                dgvEmployees.Columns.Add(BtnCheckDetails);
                BtnCheckDetails.Dispose();
                dgvEmployees.Refresh();
            }
            this.Cursor = Cursors.Default;
        }
        #endregion

        #region BtnLoad_Click
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (blnSelectComboExit == true)
                return;
            //
            lngBASIC_INFO_ID = 0;
            //
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                LoadEmployeeGrid();
                return;
            }
            //--
            if (cmbCompany.SelectedIndex <= 0)
            {
                LoadEmployeeGrid();
                return;
            }
            //--
            lngBASIC_INFO_ID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        T_Qtr.Q4,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        T_FormNo.F24Q);
            if (lngBASIC_INFO_ID == 0)
            {
                LoadEmployeeGrid();
                return;
            }
            //--
            strSQL = @"SELECT COUNT(*) FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBASIC_INFO_ID;
            if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) == 0)
            {
                LoadEmployeeGrid();
                return;
            }
            //--
            #region CREATE TEMP TABLE
            dmlService.J_BeginTransaction();
            //MessageBox.Show("2");
            if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION + "") == true)
            {
                //MessageBox.Show("2.1");
                //strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION + "";
                strSQL = "DELETE * FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION;
                dmlService.J_ExecSql(strSQL);
            }
            dmlService.J_Commit();
            //MessageBox.Show("3");
            dmlService.J_BeginTransaction();
            if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION) == false)
            {
                //MessageBox.Show("3.1");
                strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION + @" (
                                        " + cmnService.J_GetDataType("TAX_REGIME_COMPARISION_ID", J_Identity.YES) + @", 
                                        " + cmnService.J_GetDataType("BASIC_INFO_ID", J_ColumnType.Long) + @",
                                        " + cmnService.J_GetDataType("SALARY_DETAILS_ID", J_ColumnType.Long) + @",
                                        " + cmnService.J_GetDataType("EMP_ID", J_ColumnType.Long) + @",
                                        " + cmnService.J_GetDataType("EMP_SERIAL_NO", J_ColumnType.Integer) + @",
                                        " + cmnService.J_GetDataType("EMP_PAN", J_ColumnType.String) + @",
                                        " + cmnService.J_GetDataType("EMP_NAME", J_ColumnType.String) + @",
                                        " + cmnService.J_GetDataType("EMP_CATEGORY", J_ColumnType.String) + @",
                                        " + cmnService.J_GetDataType("EMP_REGIME", J_ColumnType.Integer) + @",
                                        " + cmnService.J_GetDataType("EMP_GROSS_SALARY", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_5_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_10_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_10A_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_10AA_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_13A_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_TOTAL_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SEC10_14_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_TS_LA_TOTAL", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CURRENT_SALARY", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_PREVIOUS_SALARY", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_AIS_ITEM_1", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_AIS_ITEM_2", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_NET_INCOME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_US_16_EA_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_US_16_TE_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_US_16_IA_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CH_VI_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80CCD_1B_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80CCD_2_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80D_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80E_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80CCH_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80CCH_1_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80G_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_SEC80TTA_DED_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_CVIA_OTH_DED_TOTAL", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_TAXABLE_INCOME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_TAX_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_OLD_TAXABLE_INCOME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_OLD_TAX_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SURCHARGE_OLD_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_EDU_CESS_OLD_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_OLD_TOTAL_TAX", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_NEW_TAXABLE_INCOME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_NEW_TAX_AMOUNT", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_SURCHARGE_NEW_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_EDU_CESS_NEW_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_NEW_TOTAL_TAX", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_REBATE_US87_OLD_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("EMP_REBATE_US87_NEW_REGIME", J_ColumnType.Double) + @",
                                        " + cmnService.J_GetDataType("REGIME_STATUS", J_ColumnType.String) + @")";
                //cmnService.J_UserMessage(strSQL);
                dmlService.J_ExecSql(strSQL);
            }
            dmlService.J_Commit();
            #endregion
            //--
            #region INSERT
            strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION +
                       "(SALARY_DETAILS_ID," +
                       " BASIC_INFO_ID," +
                       " EMP_ID, " +
                       " EMP_SERIAL_NO, " +
                       " EMP_PAN, " +
                       " EMP_NAME, " +
                       " EMP_CATEGORY, " +
                       " EMP_REGIME, " +
                       " EMP_GROSS_SALARY, " +
                       " EMP_SEC10_5_AMOUNT, " +
                       " EMP_SEC10_10_AMOUNT, " +
                       " EMP_SEC10_10A_AMOUNT, " +
                       " EMP_SEC10_10AA_AMOUNT, " +
                       " EMP_SEC10_13A_AMOUNT, " +
                       " EMP_SEC10_14_AMOUNT, " +
                       " EMP_TS_LA_TOTAL, " +
                       " EMP_CURRENT_SALARY, " +
                       " EMP_PREVIOUS_SALARY, " +
                       " EMP_AIS_ITEM_1, " +
                       " EMP_AIS_ITEM_2, " +
                       " EMP_US_16_EA_AMOUNT, " +
                       " EMP_US_16_TE_AMOUNT, " +
                       " EMP_US_16_IA_AMOUNT, " +
                       " EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT, " +
                       " EMP_CVIA_SEC80CCD_1B_DED_AMOUNT, " +
                       " EMP_CVIA_SEC80CCD_2_DED_AMOUNT, " +
                       " EMP_CVIA_SEC80D_DED_AMOUNT, " +
                       " EMP_CVIA_SEC80E_DED_AMOUNT, " +
                       " EMP_CVIA_SEC80CCH_DED_AMOUNT, " +
                       " EMP_CVIA_SEC80CCH_1_DED_AMOUNT, " +
                       " EMP_CVIA_SEC80G_DED_AMOUNT, " +
                       " EMP_CVIA_SEC80TTA_DED_AMOUNT, " +
                       " EMP_CVIA_OTH_DED_TOTAL, " +
                       " EMP_TAXABLE_INCOME, " +
                       " EMP_TAX_AMOUNT, " +
                       " EMP_OLD_TAXABLE_INCOME, " +
                       " EMP_OLD_TAX_AMOUNT, " +
                       " EMP_NEW_TAXABLE_INCOME, " +
                       " EMP_NEW_TAX_AMOUNT," +
                       " EMP_SURCHARGE_OLD_REGIME," +
                       " EMP_EDU_CESS_OLD_REGIME," +
                       " EMP_SURCHARGE_NEW_REGIME," +
                       " EMP_EDU_CESS_NEW_REGIME)";
            //
            string[,] strOLD_TOTAL_INCOMEMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "TRN_SALARY_DETAILS.TOTAL_INCOME", "F"},
                                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "0.00", "T"}};

            string[,] strOLD_TAX_AMOUNTMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "TRN_SALARY_DETAILS.TAX_PAYABLE", "F"},
                                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "0.00", "T"}};

            string[,] strNEW_TOTAL_INCOMEMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "TRN_SALARY_DETAILS.TOTAL_INCOME", "F"},
                                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "0.00", "T"}};

            string[,] strNEW_TAX_AMOUNTMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "TRN_SALARY_DETAILS.TAX_PAYABLE", "F"},
                                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "0.00", "T"}};

            string[,] strEMP_SURCHARGE_OLD_REGIMEMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME", "F"},
                                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "0.00", "T"}};

            string[,] strEMP_EDU_CESS_OLD_REGIMEMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME", "F"},
                                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "0.00", "T"}};

            string[,] strEMP_SURCHARGE_NEW_REGIMEMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME", "F"},
                                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "0.00", "T"}};

            string[,] strEMP_EDU_CESS_NEW_REGIMEMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME", "F"},
                                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "0.00", "T"}};

            strSQL = strSQL + " " +
               "SELECT TRN_SALARY_DETAILS.SALARY_DETAILS_ID  AS SALARY_DETAILS_ID," +
               "       TRN_SALARY_DETAILS.BASIC_INFO_ID      AS BASIC_INFO_ID," +
               "       MST_EMPLOYEE.EMPLOYEE_ID              AS EMP_ID," +
               "       TRN_SALARY_DETAILS.SL_NO              AS EMP_SERIAL_NO," +
               "       MST_EMPLOYEE.EMPLOYEE_PAN             AS EMP_PAN," +
               "       MST_EMPLOYEE.EMPLOYEE_NAME            AS EMP_NAME," +
               "       MST_EMPLOYEE.CATEGORY                 AS EMP_CATEGORY," +
               "       TRN_SALARY_DETAILS.SECTION_115BAC_FLAG AS SECTION_115BAC_FLAG," +
               "       TRN_SALARY_DETAILS.TS_GS_TOTAL        AS EMP_GROSS_SALARY," +
               "       TRN_SALARY_DETAILS.SEC10_5_AMOUNT     AS EMP_SEC10_5_AMOUNT," +
               "       TRN_SALARY_DETAILS.SEC10_10_AMOUNT    AS EMP_SEC10_10_AMOUNT," +
               "       TRN_SALARY_DETAILS.SEC10_10A_AMOUNT   AS EMP_SEC10_10A_AMOUNT," +
               "       TRN_SALARY_DETAILS.SEC10_10AA_AMOUNT  AS EMP_SEC10_10AA_AMOUNT," +
               "       TRN_SALARY_DETAILS.SEC10_13A_AMOUNT   AS EMP_SEC10_13A_AMOUNT," +
               "       TRN_SALARY_DETAILS.SEC10_14_AMOUNT    AS EMP_SEC10_14_AMOUNT," +
               "       TRN_SALARY_DETAILS.TS_LA_TOTAL        AS EMP_TS_LA_TOTAL," +
               "       TRN_SALARY_DETAILS.TAXABLE_AMOUNT     AS EMP_CURRENT_SALARY," +
               "       TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT AS EMP_PREVIOUS_SALARY," +
               "       TRN_SALARY_DETAILS.AIS_ITEM_1         AS EMP_AIS_ITEM_1," +
               "       TRN_SALARY_DETAILS.AIS_ITEM_2         AS EMP_AIS_ITEM_2," +
               "       TRN_SALARY_DETAILS.US_16_EA           AS EMP_US_16_EA_AMOUNT," +
               "       TRN_SALARY_DETAILS.US_16_TE           AS EMP_US_16_TE_AMOUNT," +
               "       TRN_SALARY_DETAILS.US_16_IA           AS EMP_US_16_IA_AMOUNT," +
               "       TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT AS EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
               "       TRN_SALARY_DETAILS.CVIA_SEC80CCD_1B_DED_AMOUNT    AS EMP_CVIA_SEC80CCD_1B_DED_AMOUNT," +
               "       TRN_SALARY_DETAILS.CVIA_SEC80CCD_2_DED_AMOUNT     AS EMP_CVIA_SEC80CCD_2_DED_AMOUNT," +
               "       TRN_SALARY_DETAILS.CVIA_SEC80D_DED_AMOUNT         AS EMP_CVIA_SEC80D_DED_AMOUNT," +
               "       TRN_SALARY_DETAILS.CVIA_SEC80E_DED_AMOUNT         AS EMP_CVIA_SEC80E_DED_AMOUNT," +
               "       TRN_SALARY_DETAILS.CVIA_SEC80CCH_DED_AMOUNT       AS EMP_CVIA_SEC80CCH_DED_AMOUNT," +
               "       TRN_SALARY_DETAILS.CVIA_SEC80CCH_1_DED_AMOUNT     AS EMP_CVIA_SEC80CCH_1_DED_AMOUNT," +
               "       TRN_SALARY_DETAILS.CVIA_SEC80G_DED_AMOUNT         AS EMP_CVIA_SEC80G_DED_AMOUNT," +
               "       TRN_SALARY_DETAILS.CVIA_SEC80TTA_DED_AMOUNT       AS EMP_CVIA_SEC80TTA_DED_AMOUNT," +
               "       TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL             AS EMP_CVIA_OTH_DED_TOTAL," +
               "       TRN_SALARY_DETAILS.TOTAL_INCOME                   AS EMP_TAXABLE_INCOME," +
               "       TRN_SALARY_DETAILS.TAX_PAYABLE                    AS EMP_TAX_AMOUNT," +
               "" + cmnService.J_SQLDBFormat(strOLD_TOTAL_INCOMEMatrix, J_SQLColFormat.Case_End) + @" AS EMP_OLD_TAXABLE_INCOME," +
               "" + cmnService.J_SQLDBFormat(strOLD_TAX_AMOUNTMatrix, J_SQLColFormat.Case_End) + @" AS EMP_OLD_TAX_AMOUNT," +
               "" + cmnService.J_SQLDBFormat(strNEW_TOTAL_INCOMEMatrix, J_SQLColFormat.Case_End) + @" AS EMP_NEW_TAXABLE_INCOME," +
               "" + cmnService.J_SQLDBFormat(strNEW_TAX_AMOUNTMatrix, J_SQLColFormat.Case_End) + @" AS EMP_NEW_TAX_AMOUNT," +
               "" + cmnService.J_SQLDBFormat(strEMP_SURCHARGE_OLD_REGIMEMatrix, J_SQLColFormat.Case_End) + @" AS EMP_SURCHARGE_OLD_REGIME," +
               "" + cmnService.J_SQLDBFormat(strEMP_EDU_CESS_OLD_REGIMEMatrix, J_SQLColFormat.Case_End) + @" AS EMP_EDU_CESS_OLD_REGIME," +
               "" + cmnService.J_SQLDBFormat(strEMP_SURCHARGE_NEW_REGIMEMatrix, J_SQLColFormat.Case_End) + @" AS EMP_SURCHARGE_NEW_REGIME," +
               "" + cmnService.J_SQLDBFormat(strEMP_EDU_CESS_NEW_REGIMEMatrix, J_SQLColFormat.Case_End) + @" AS EMP_EDU_CESS_NEW_REGIME " +
               "FROM   TRN_SALARY_DETAILS," +
               "       MST_EMPLOYEE " +
               "WHERE  TRN_SALARY_DETAILS.EMPLOYEE_ID    = MST_EMPLOYEE.EMPLOYEE_ID " +
               "AND    TRN_SALARY_DETAILS.BASIC_INFO_ID  = " + lngBASIC_INFO_ID + " " +
               "ORDER BY TRN_SALARY_DETAILS.SL_NO";
            //--
            dmlService.J_BeginTransaction();
            dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            dmlService.J_Commit();
            //--
            #endregion
            //--
            lngFA_YEAR_ID = Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
            LoadEmployeeGrid(lngBASIC_INFO_ID);
            //-- 
            btnLoad.Enabled = false; btnLoad.BackColor = Color.LightGray;
        }
        #endregion

        #endregion

        #region User Define Functions

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
                "      AND    ASST_ID >= 20 " +
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
            //
            blnSelectComboExit = false;
            //-----------
        }
        #endregion

        #region LoadEmployeeGrid()
        private void LoadEmployeeGrid()
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //dgvEmployees.Rows.Clear();
                //dgvEmployees.Columns.Clear();
                dgvEmployees.DataSource = null;
                dgvEmployees.Refresh();
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"SALARY_DETAILS_ID", "0", "", "", "", "F", ""},
                                        {"EMP_ID", "0", "", "", "", "F", ""},
                                        {"Serial No.", "85", "", "", "", "", ""},
                                        {"PAN", "85", "S", "", "", "", "T"},
                                        {"Name", "80", "S", "", "", "", "T"},
                                        {"Category", "35", "S", "", "", "", "T"},
                                        {"Regime", "35", "S", "", "", "", "T"},
                                        {"Taxable Income (Old Regime)", "95", "0.00", "R", "", "", "T"},
                                        {"Tax Amount (Old Regime)", "95", "0.00", "R", "", "", "T"},
                                        {"Taxable Income (New Regime)", "95", "0.00", "R", "", "", "T"},
                                        {"Tax Amount (New Regime)", "95", "0.00", "R", "", "", "T"},
                                        {"EMP_GROSS_SALARY", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_5_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_10_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_10A_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_10AA_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_13A_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_14_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_TS_LA_TOTAL", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CURRENT_SALARY", "0", "0.00", "", "", "F", ""},
                                        {"EMP_PREVIOUS_SALARY", "0", "0.00", "", "", "F", ""},
                                        {"EMP_AIS_ITEM_1", "0", "0.00", "", "", "F", ""},
                                        {"EMP_AIS_ITEM_2", "0", "0.00", "", "", "F", ""},
                                        {"EMP_US_16_EA_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_US_16_TE_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_US_16_IA_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80CCD_1B_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80CCD_2_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80D_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80E_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80CCH_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80CCH_1_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80G_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80TTA_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_OTH_DED_TOTAL", "0", "0.00", "", "", "F", ""},
                                        {"EMP_REBATE_US87_OLD_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_REBATE_US87_NEW_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_EDU_CESS_OLD_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_EDU_CESS_NEW_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SURCHARGE_OLD_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SURCHARGE_NEW_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_GROSS_TAX_OLD_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_GROSS_TAX_NEW_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"Beneficial", "35", "S", "", "", "", "T"}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                string[,] strEMP_REGIMEMatrix = {{"EMP_REGIME <> 0", "F", "NEW", "T"},
                                            {"EMP_REGIME = 0", "F", "", "T"}};
                //
                //string[,] strEMP_OLD_TAXABLE_INCOMEMatrix = {{"EMP_REGIME = 0", "F", "NEW", "T"},
                //                            {"EMP_REGIME = 0", "F", "", "T"}};
                //            
                strSQL = "SELECT   SALARY_DETAILS_ID   AS SALARY_DETAILS_ID," +
                         "         EMP_ID              AS EMP_ID," +
                         "         EMP_SERIAL_NO       AS EMP_SERIAL_NO," +
                         "         EMP_PAN             AS EMP_PAN," +
                         "         EMP_NAME            AS EMP_NAME," +
                         "         EMP_CATEGORY        AS EMP_CATEGORY," +
                         "" + cmnService.J_SQLDBFormat(strEMP_REGIMEMatrix, J_SQLColFormat.Case_End) + @" AS EMP_REGIME," +
                         "         EMP_OLD_TAXABLE_INCOME AS EMP_OLD_TAXABLE_INCOME," +
                         "         EMP_OLD_TAX_AMOUNT     AS EMP_OLD_TAX_AMOUNT," +
                         "         EMP_NEW_TAXABLE_INCOME AS EMP_NEW_TAXABLE_INCOME," +
                         "         EMP_NEW_TAX_AMOUNT     AS EMP_NEW_TAX_AMOUNT," +
                         "         EMP_GROSS_SALARY       AS EMP_GROSS_SALARY ," +
                         "         EMP_SEC10_5_AMOUNT     AS EMP_SEC10_5_AMOUNT ," +
                         "         EMP_SEC10_10_AMOUNT    AS EMP_SEC10_10_AMOUNT ," +
                         "         EMP_SEC10_10A_AMOUNT   AS EMP_SEC10_10A_AMOUNT ," +
                         "         EMP_SEC10_10AA_AMOUNT  AS EMP_SEC10_10AA_AMOUNT ," +
                         "         EMP_SEC10_13A_AMOUNT   AS EMP_SEC10_13A_AMOUNT ," +
                         "         EMP_SEC10_14_AMOUNT    AS EMP_SEC10_14_AMOUNT ," +
                         "         EMP_TS_LA_TOTAL        AS EMP_TS_LA_TOTAL ," +
                         "         EMP_CURRENT_SALARY     AS EMP_CURRENT_SALARY ," +
                         "         EMP_PREVIOUS_SALARY    AS EMP_PREVIOUS_SALARY ," +
                         "         EMP_AIS_ITEM_1         AS EMP_AIS_ITEM_1," +
                         "         EMP_AIS_ITEM_2         AS EMP_AIS_ITEM_2," +
                         "         EMP_US_16_EA_AMOUNT    AS EMP_US_16_EA_AMOUNT ," +
                         "         EMP_US_16_TE_AMOUNT    AS EMP_US_16_TE_AMOUNT ," +
                         "         EMP_US_16_IA_AMOUNT    AS EMP_US_16_IA_AMOUNT ," +
                         "         EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT AS EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80CCD_1B_DED_AMOUNT    AS EMP_CVIA_SEC80CCD_1B_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80CCD_2_DED_AMOUNT     AS EMP_CVIA_SEC80CCD_2_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80D_DED_AMOUNT         AS EMP_CVIA_SEC80D_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80E_DED_AMOUNT         AS EMP_CVIA_SEC80E_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80CCH_DED_AMOUNT       AS EMP_CVIA_SEC80CCH_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80CCH_1_DED_AMOUNT     AS EMP_CVIA_SEC80CCH_1_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80G_DED_AMOUNT         AS EMP_CVIA_SEC80G_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80TTA_DED_AMOUNT       AS EMP_CVIA_SEC80TTA_DED_AMOUNT ," +
                         "         EMP_CVIA_OTH_DED_TOTAL             AS EMP_CVIA_OTH_DED_TOTAL ," +
                         "         EMP_REBATE_US87_OLD_REGIME         AS EMP_REBATE_US87_OLD_REGIME ," +
                         "         EMP_REBATE_US87_NEW_REGIME         AS EMP_REBATE_US87_NEW_REGIME ," +
                         "         EMP_EDU_CESS_OLD_REGIME            AS EMP_EDU_CESS_OLD_REGIME ," +
                         "         EMP_EDU_CESS_NEW_REGIME            AS EMP_EDU_CESS_NEW_REGIME ," +
                         "         EMP_SURCHARGE_OLD_REGIME           AS EMP_SURCHARGE_OLD_REGIME ," +
                         "         EMP_SURCHARGE_NEW_REGIME           AS EMP_SURCHARGE_NEW_REGIME  ," +
                         "         ''                                 AS EMP_GROSS_TAX_OLD_REGIME  ," +
                         "         ''                                 AS EMP_GROSS_TAX_NEW_REGIME  ," +
                         "         ''                                 AS BENEFICIAL_REGIME " +
                         "FROM     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION + " " +
                         "WHERE    1=2 ORDER BY EMP_SERIAL_NO";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvEmployees, strSQL, strMatrixViewDeductee);
                dgvEmployees.ClearSelection();
                //
                //dgvEmployees.AutoGenerateColumns = false;
                //DataGridViewButtonColumn BtnCheckDetails = new DataGridViewButtonColumn();
                //{
                //    //dgvUpdates.Columns
                //    //BtnShow.Visible = true;
                //    BtnCheckDetails.HeaderText = "";
                //    BtnCheckDetails.Width = 110;
                //    //BtnReturnHealth.Text = " - Check Health - ";
                //    BtnCheckDetails.Text = "View";
                //    BtnCheckDetails.ToolTipText = "Check Details";
                //    BtnCheckDetails.Name = "BtnCheckDetails";
                //    BtnCheckDetails.FlatStyle = FlatStyle.Popup;
                //    BtnCheckDetails.UseColumnTextForButtonValue = true;
                //    dgvEmployees.Columns.Add(BtnCheckDetails);
                //    //BtnReturnHealth.Dispose();
                //}
                ////
                //if (!bgWorker.IsBusy)
                //    bgWorker.RunWorkerAsync();
                //
            }
            catch(Exception ERR)
            {
                cmnService.J_UserMessage(ERR.Message);
            }
        }
        #endregion

        #region CmbFinancialYear_SelectedIndexChanged
        private void CmbFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dgvEmployees.DataSource = null;
            //dgvEmployees.Refresh();
            LoadEmployeeGrid();

            btnLoad.Enabled = true;
            btnLoad.BackColor = Color.Lavender;
            lblStatus.Text = "";
        }
        #endregion

        #region CmbCompany_SelectedIndexChanged
        private void CmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dgvEmployees.DataSource = null;
            //dgvEmployees.Refresh();
            LoadEmployeeGrid();

            btnLoad.Enabled = true;
            btnLoad.BackColor = Color.Lavender;
            lblStatus.Text = "";
        }
        #endregion

        #region LoadEmployeeGrid(BasicInfoID)
        private void LoadEmployeeGrid(long BasicInfoID)
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //if(BasicInfoID == 0)
                //{
                    //dgvEmployees.Rows.Clear();
                    //dgvEmployees.Columns.Clear();
                dgvEmployees.DataSource = null;
                dgvEmployees.Refresh();
                //}
                //--
                #region INSERT DATA INTO T_tblTEMP_TAX_REGIME_COMPARISION
                //strSQL = "DELETE * FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION;
                //dmlService.J_BeginTransaction();
                //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                //dmlService.J_Commit();
                ////--
                //strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION +
                //           "(SALARY_DETAILS_ID," +
                //           " BASIC_INFO_ID," +
                //           " EMP_ID, " +
                //           " EMP_SERIAL_NO, " +
                //           " EMP_PAN, " +
                //           " EMP_NAME, " +
                //           " EMP_CATEGORY, " +
                //           " EMP_REGIME, " +
                //           " EMP_GROSS_SALARY, " +
                //           " EMP_SEC10_5_AMOUNT, " +
                //           " EMP_SEC10_10_AMOUNT, " +
                //           " EMP_SEC10_10A_AMOUNT, " +
                //           " EMP_SEC10_10AA_AMOUNT, " +
                //           " EMP_SEC10_13A_AMOUNT, " +
                //           " EMP_SEC10_14_AMOUNT, " +
                //           " EMP_TS_LA_TOTAL, " +
                //           " EMP_CURRENT_SALARY, " +
                //           " EMP_PREVIOUS_SALARY, " +
                //           " EMP_AIS_ITEM_1, " +
                //           " EMP_AIS_ITEM_2, " +
                //           " EMP_US_16_EA_AMOUNT, " +
                //           " EMP_US_16_TE_AMOUNT, " +
                //           " EMP_US_16_IA_AMOUNT, " +
                //           " EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT, " +
                //           " EMP_CVIA_SEC80CCD_1B_DED_AMOUNT, " +
                //           " EMP_CVIA_SEC80CCD_2_DED_AMOUNT, " +
                //           " EMP_CVIA_SEC80D_DED_AMOUNT, " +
                //           " EMP_CVIA_SEC80E_DED_AMOUNT, " +
                //           " EMP_CVIA_SEC80CCH_DED_AMOUNT, " +
                //           " EMP_CVIA_SEC80CCH_1_DED_AMOUNT, " +
                //           " EMP_CVIA_SEC80G_DED_AMOUNT, " +
                //           " EMP_CVIA_SEC80TTA_DED_AMOUNT, " +
                //           " EMP_CVIA_OTH_DED_TOTAL, " +
                //           " EMP_TAXABLE_INCOME, " +
                //           " EMP_TAX_AMOUNT, " +
                //           " EMP_OLD_TAXABLE_INCOME, " +
                //           " EMP_OLD_TAX_AMOUNT, " +
                //           " EMP_NEW_TAXABLE_INCOME, " +
                //           " EMP_NEW_TAX_AMOUNT)";
                ////
                //string[,] strOLD_TOTAL_INCOMEMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "TRN_SALARY_DETAILS.TOTAL_INCOME", "F"},
                //                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "0.00", "T"}};

                //string[,] strOLD_TAX_AMOUNTMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "TRN_SALARY_DETAILS.TAX_PAYABLE", "F"},
                //                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "0.00", "T"}};

                //string[,] strNEW_TOTAL_INCOMEMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "TRN_SALARY_DETAILS.TOTAL_INCOME", "F"},
                //                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "0.00", "T"}};

                //string[,] strNEW_TAX_AMOUNTMatrix = {{"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 1", "F", "TRN_SALARY_DETAILS.TAX_PAYABLE", "F"},
                //                            {"TRN_SALARY_DETAILS.SECTION_115BAC_FLAG = 0", "F", "0.00", "T"}};

                //strSQL = strSQL + " " +
                //   "SELECT TRN_SALARY_DETAILS.SALARY_DETAILS_ID  AS SALARY_DETAILS_ID," +
                //   "       TRN_SALARY_DETAILS.BASIC_INFO_ID      AS BASIC_INFO_ID," +
                //   "       MST_EMPLOYEE.EMPLOYEE_ID              AS EMP_ID," +
                //   "       TRN_SALARY_DETAILS.SL_NO              AS EMP_SERIAL_NO," +
                //   "       MST_EMPLOYEE.EMPLOYEE_PAN             AS EMP_PAN," +
                //   "       MST_EMPLOYEE.EMPLOYEE_NAME            AS EMP_NAME," +
                //   "       MST_EMPLOYEE.CATEGORY                 AS EMP_CATEGORY," +
                //   "       TRN_SALARY_DETAILS.SECTION_115BAC_FLAG AS SECTION_115BAC_FLAG," +
                //   "       TRN_SALARY_DETAILS.TS_GS_TOTAL        AS EMP_GROSS_SALARY," +
                //   "       TRN_SALARY_DETAILS.SEC10_5_AMOUNT     AS EMP_SEC10_5_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.SEC10_10_AMOUNT    AS EMP_SEC10_10_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.SEC10_10A_AMOUNT   AS EMP_SEC10_10A_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.SEC10_10AA_AMOUNT  AS EMP_SEC10_10AA_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.SEC10_13A_AMOUNT   AS EMP_SEC10_13A_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.SEC10_14_AMOUNT    AS EMP_SEC10_14_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.TS_LA_TOTAL        AS EMP_TS_LA_TOTAL," +
                //   "       TRN_SALARY_DETAILS.TAXABLE_AMOUNT     AS EMP_CURRENT_SALARY," +
                //   "       TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT AS EMP_PREVIOUS_SALARY," +
                //   "       TRN_SALARY_DETAILS.AIS_ITEM_1         AS EMP_AIS_ITEM_1," +
                //   "       TRN_SALARY_DETAILS.AIS_ITEM_2         AS EMP_AIS_ITEM_2," +
                //   "       TRN_SALARY_DETAILS.US_16_EA           AS EMP_US_16_EA_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.US_16_TE           AS EMP_US_16_TE_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.US_16_IA           AS EMP_US_16_IA_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT AS EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.CVIA_SEC80CCD_1B_DED_AMOUNT    AS EMP_CVIA_SEC80CCD_1B_DED_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.CVIA_SEC80CCD_2_DED_AMOUNT     AS EMP_CVIA_SEC80CCD_2_DED_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.CVIA_SEC80D_DED_AMOUNT         AS EMP_CVIA_SEC80D_DED_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.CVIA_SEC80E_DED_AMOUNT         AS EMP_CVIA_SEC80E_DED_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.CVIA_SEC80CCH_DED_AMOUNT       AS EMP_CVIA_SEC80CCH_DED_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.CVIA_SEC80CCH_1_DED_AMOUNT     AS EMP_CVIA_SEC80CCH_1_DED_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.CVIA_SEC80G_DED_AMOUNT         AS EMP_CVIA_SEC80G_DED_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.CVIA_SEC80TTA_DED_AMOUNT       AS EMP_CVIA_SEC80TTA_DED_AMOUNT," +
                //   "       TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL             AS EMP_CVIA_OTH_DED_TOTAL," +
                //   "       TRN_SALARY_DETAILS.TOTAL_INCOME                   AS EMP_TAXABLE_INCOME," +
                //   "       TRN_SALARY_DETAILS.TAX_PAYABLE                    AS EMP_TAX_AMOUNT," +
                //   "" + cmnService.J_SQLDBFormat(strOLD_TOTAL_INCOMEMatrix, J_SQLColFormat.Case_End) + @" AS EMP_OLD_TAXABLE_INCOME," +
                //   "" + cmnService.J_SQLDBFormat(strOLD_TAX_AMOUNTMatrix, J_SQLColFormat.Case_End) + @" AS EMP_OLD_TAX_AMOUNT," +
                //   "" + cmnService.J_SQLDBFormat(strNEW_TOTAL_INCOMEMatrix, J_SQLColFormat.Case_End) + @" AS EMP_NEW_TAXABLE_INCOME," +
                //   "" + cmnService.J_SQLDBFormat(strNEW_TAX_AMOUNTMatrix, J_SQLColFormat.Case_End) + @" AS EMP_NEW_TAX_AMOUNT " +
                //   "FROM   TRN_SALARY_DETAILS," +
                //   "       MST_EMPLOYEE " +
                //   "WHERE  TRN_SALARY_DETAILS.EMPLOYEE_ID    = MST_EMPLOYEE.EMPLOYEE_ID " +
                //   "AND    TRN_SALARY_DETAILS.BASIC_INFO_ID  = " + BasicInfoID + " " +
                //   "ORDER BY TRN_SALARY_DETAILS.SL_NO";
                ////--
                //dmlService.J_BeginTransaction();
                //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                //dmlService.J_Commit();
                #endregion
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"SALARY_DETAILS_ID", "0", "", "", "", "F", ""},
                                        {"EMP_ID", "0", "", "", "", "F", ""},
                                        {"Serial No.", "85", "", "", "", "", ""},
                                        {"PAN", "85", "S", "", "", "", "T"},
                                        {"Name", "80", "S", "", "", "", "T"},
                                        {"Category", "35", "S", "", "", "", "T"},
                                        {"Regime", "35", "S", "", "", "", "T"},
                                        {"Taxable Income (Old Regime)", "95", "0.00", "R", "", "", "T"},
                                        {"Tax Amount (Old Regime)", "95", "0.00", "R", "", "", "T"},
                                        {"Taxable Income (New Regime)", "95", "0.00", "R", "", "", "T"},
                                        {"Tax Amount (New Regime)", "95", "0.00", "R", "", "", "T"},
                                        {"EMP_GROSS_SALARY", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_5_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_10_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_10A_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_10AA_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_13A_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SEC10_14_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_TS_LA_TOTAL", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CURRENT_SALARY", "0", "0.00", "", "", "F", ""},
                                        {"EMP_PREVIOUS_SALARY", "0", "0.00", "", "", "F", ""},
                                        {"EMP_AIS_ITEM_1", "0", "0.00", "", "", "F", ""},
                                        {"EMP_AIS_ITEM_2", "0", "0.00", "", "", "F", ""},
                                        {"EMP_US_16_EA_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_US_16_TE_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_US_16_IA_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80CCD_1B_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80CCD_2_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80D_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80E_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80CCH_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80CCH_1_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80G_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_SEC80TTA_DED_AMOUNT", "0", "0.00", "", "", "F", ""},
                                        {"EMP_CVIA_OTH_DED_TOTAL", "0", "0.00", "", "", "F", ""},
                                        {"EMP_REBATE_US87_OLD_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_REBATE_US87_NEW_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_EDU_CESS_OLD_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_EDU_CESS_NEW_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SURCHARGE_OLD_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_SURCHARGE_NEW_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_GROSS_TAX_OLD_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"EMP_GROSS_TAX_NEW_REGIME", "0", "0.00", "", "", "F", ""},
                                        {"Beneficial", "35", "S", "", "", "", "T"}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                string[,] strEMP_REGIMEMatrix = {{"EMP_REGIME <> 0", "F", "NEW", "T"},
                                            {"EMP_REGIME = 0", "F", "", "T"}};
                //
                //            
                strSQL = "SELECT   SALARY_DETAILS_ID   AS SALARY_DETAILS_ID," +
                         "         EMP_ID              AS EMP_ID," +
                         "         EMP_SERIAL_NO       AS EMP_SERIAL_NO," +
                         "         EMP_PAN             AS EMP_PAN," +
                         "         EMP_NAME            AS EMP_NAME," +
                         "         EMP_CATEGORY        AS EMP_CATEGORY," +
                         "" + cmnService.J_SQLDBFormat(strEMP_REGIMEMatrix, J_SQLColFormat.Case_End) + @" AS EMP_REGIME," +
                         "         EMP_OLD_TAXABLE_INCOME AS EMP_OLD_TAXABLE_INCOME," +
                         "         EMP_OLD_TAX_AMOUNT     AS EMP_OLD_TAX_AMOUNT," +
                         "         EMP_NEW_TAXABLE_INCOME AS EMP_NEW_TAXABLE_INCOME," +
                         "         EMP_NEW_TAX_AMOUNT     AS EMP_NEW_TAX_AMOUNT," +
                         "         EMP_GROSS_SALARY       AS EMP_GROSS_SALARY ," +
                         "         EMP_SEC10_5_AMOUNT     AS EMP_SEC10_5_AMOUNT ," +
                         "         EMP_SEC10_10_AMOUNT    AS EMP_SEC10_10_AMOUNT ," +
                         "         EMP_SEC10_10A_AMOUNT   AS EMP_SEC10_10A_AMOUNT ," +
                         "         EMP_SEC10_10AA_AMOUNT  AS EMP_SEC10_10AA_AMOUNT ," +
                         "         EMP_SEC10_13A_AMOUNT   AS EMP_SEC10_13A_AMOUNT ," +
                         "         EMP_SEC10_14_AMOUNT    AS EMP_SEC10_14_AMOUNT ," +
                         "         EMP_TS_LA_TOTAL        AS EMP_TS_LA_TOTAL ," +
                         "         EMP_CURRENT_SALARY     AS EMP_CURRENT_SALARY ," +
                         "         EMP_PREVIOUS_SALARY    AS EMP_PREVIOUS_SALARY ," +
                         "         EMP_AIS_ITEM_1         AS EMP_AIS_ITEM_1," +
                         "         EMP_AIS_ITEM_2         AS EMP_AIS_ITEM_2," +
                         "         EMP_US_16_EA_AMOUNT    AS EMP_US_16_EA_AMOUNT ," +
                         "         EMP_US_16_TE_AMOUNT    AS EMP_US_16_TE_AMOUNT ," +
                         "         EMP_US_16_IA_AMOUNT    AS EMP_US_16_IA_AMOUNT ," +
                         "         EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT AS EMP_CVIA_SEC80CCE_TOTAL_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80CCD_1B_DED_AMOUNT    AS EMP_CVIA_SEC80CCD_1B_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80CCD_2_DED_AMOUNT     AS EMP_CVIA_SEC80CCD_2_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80D_DED_AMOUNT         AS EMP_CVIA_SEC80D_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80E_DED_AMOUNT         AS EMP_CVIA_SEC80E_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80CCH_DED_AMOUNT       AS EMP_CVIA_SEC80CCH_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80CCH_1_DED_AMOUNT     AS EMP_CVIA_SEC80CCH_1_DED_AMOUNT ," +
                         "         EMP_CVIA_SEC80G_DED_AMOUNT         AS EMP_CVIA_SEC80G_DED_AMOUNT ," + 
                         "         EMP_CVIA_SEC80TTA_DED_AMOUNT       AS EMP_CVIA_SEC80TTA_DED_AMOUNT ," +
                         "         EMP_CVIA_OTH_DED_TOTAL             AS EMP_CVIA_OTH_DED_TOTAL ," +
                         "         EMP_REBATE_US87_OLD_REGIME         AS EMP_REBATE_US87_OLD_REGIME ," +
                         "         EMP_REBATE_US87_NEW_REGIME         AS EMP_REBATE_US87_NEW_REGIME ," +
                         "         EMP_EDU_CESS_OLD_REGIME            AS EMP_EDU_CESS_OLD_REGIME ," +
                         "         EMP_EDU_CESS_NEW_REGIME            AS EMP_EDU_CESS_NEW_REGIME ," +
                         "         EMP_SURCHARGE_OLD_REGIME           AS EMP_SURCHARGE_OLD_REGIME ," +
                         "         EMP_SURCHARGE_NEW_REGIME           AS EMP_SURCHARGE_NEW_REGIME  ," +
                         "         ''                                 AS EMP_GROSS_TAX_OLD_REGIME  ," +
                         "         ''                                 AS EMP_GROSS_TAX_NEW_REGIME  ," +
                         "         ''                                 AS BENEFICIAL_REGIME " +
                         "FROM     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_REGIME_COMPARISION + " " +
                         "WHERE    BASIC_INFO_ID = " + BasicInfoID + " ORDER BY EMP_SERIAL_NO";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvEmployees, strSQL, strMatrixViewDeductee);
                dgvEmployees.ClearSelection();
                //
                ////////dgvEmployees.AutoGenerateColumns = false;
                //////////
                ////////DataGridViewButtonColumn BtnCheckDetails = new DataGridViewButtonColumn();
                ////////{
                ////////    //dgvUpdates.Columns
                ////////    //BtnShow.Visible = true;
                ////////    BtnCheckDetails.HeaderText = "";
                ////////    BtnCheckDetails.Width = 110;
                ////////    //BtnReturnHealth.Text = " - Check Health - ";
                ////////    BtnCheckDetails.Text = "View";
                ////////    BtnCheckDetails.ToolTipText = "Check Details";
                ////////    BtnCheckDetails.Name = "BtnCheckDetails";
                ////////    BtnCheckDetails.FlatStyle = FlatStyle.Popup;
                ////////    BtnCheckDetails.UseColumnTextForButtonValue = true; 
                ////////    dgvEmployees.Columns.Add(BtnCheckDetails);
                ////////    BtnCheckDetails.Dispose();
                ////////    dgvEmployees.Refresh();
                ////////}
                //
                if (!bgWorker.IsBusy)
                    bgWorker.RunWorkerAsync();
                //
            }
            catch (Exception ERR)
            {
                cmnService.J_UserMessage(ERR.Message);
            }
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

        #region ExportGridToCSV
        private void ExportGridToCSV(string CSVFile)
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=Employee.csv");
            //Response.Charset = "";
            //Response.ContentType = "application/text";
            //dgvEmployees.AllowPaging = false;
            //dgvEmployees.DataBind();

            StreamWriter StreamWriter = cmnService.J_ReturnStreamWriter(CSVFile);

            StringBuilder columnbind = new StringBuilder();
            for (int k = 0; k < dgvEmployees.Columns.Count; k++)
            {
                if(k == (int)J_TaxRegimeCalculation.EMP_SERIAL_NO
                    || k == (int)J_TaxRegimeCalculation.EMP_PAN
                    || k == (int)J_TaxRegimeCalculation.EMP_NAME
                    || k == (int)J_TaxRegimeCalculation.EMP_CATEGORY
                    || k == (int)J_TaxRegimeCalculation.EMP_REGIME
                    || k == (int)J_TaxRegimeCalculation.EMP_TAXABLE_INCOME_OLD_REGIME
                    || k == (int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_OLD_REGIME
                    || k == (int)J_TaxRegimeCalculation.EMP_TAXABLE_INCOME_NEW_REGIME
                    || k == (int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_NEW_REGIME
                    || k == (int)J_TaxRegimeCalculation.BENEFICIAL_REGIME)
                    columnbind.Append(dgvEmployees.Columns[k].HeaderText + ',');
            }

            columnbind.Append("\r\n");
            for (int i = 0; i < dgvEmployees.Rows.Count; i++)
            {
                for (int k = 0; k < dgvEmployees.Columns.Count; k++)
                {
                    if (k == (int)J_TaxRegimeCalculation.EMP_SERIAL_NO
                    || k == (int)J_TaxRegimeCalculation.EMP_PAN
                    || k == (int)J_TaxRegimeCalculation.EMP_NAME
                    || k == (int)J_TaxRegimeCalculation.EMP_CATEGORY
                    || k == (int)J_TaxRegimeCalculation.EMP_REGIME
                    || k == (int)J_TaxRegimeCalculation.EMP_TAXABLE_INCOME_OLD_REGIME
                    || k == (int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_OLD_REGIME
                    || k == (int)J_TaxRegimeCalculation.EMP_TAXABLE_INCOME_NEW_REGIME
                    || k == (int)J_TaxRegimeCalculation.EMP_TAX_AMOUNT_NEW_REGIME
                    || k == (int)J_TaxRegimeCalculation.BENEFICIAL_REGIME)
                        columnbind.Append(dgvEmployees.Rows[i].Cells[k].Value.ToString() + ',');
                }

                columnbind.Append("\r\n");
            }
            cmnService.J_WriteLine(ref StreamWriter, columnbind.ToString());
            StreamWriter.Flush();
            StreamWriter.Close();
            //Response.Output.Write(columnbind.ToString());
            //Response.Flush();
            //Response.End();

        }
        #endregion

        #endregion

    }
}
