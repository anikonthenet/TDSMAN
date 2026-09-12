#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnTagDeductees
Version			: 1.0
Start Date		: 08-12-2014
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
using TDSMAN.FormMst;
using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormSys;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

#region T_TAG_MODE
public struct T_TAG_MODE
{
    public const string UNTAG_DEDUCTEE = "UNTAG_DEDUCTEE";
    public const string TAG_DEDUCTEE = "TAG_DEDUCTEE";
}
#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnTagDeductees : Form
    {

        ResizeForm _form_resize;

        #region System Generated Code        

        //public TrnTagDeductees(long ChallanID)
        //{
        //    lngChallanID = ChallanID;
        //    InitializeComponent();
        //}
        public TrnTagDeductees()
        {
            //lngChallanID = ChallanID;
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
        //-----------------------------------------------------------------------
        long lngSearchId;					//For Storing the Id
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        DataSet dsetChallanGridClone = new DataSet();
        DataSet dsetChallanDetailsGridClone = new DataSet();
        RptDialog rptDialog = new RptDialog();
        //-----------------------------------------------------------------------
        string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        //-----------------------------------------------------------------------
        string strSQLGridViewTabPages;
        long lngBasicInfoID;
        long lngChallanID;
        //long lngChallanDetailID;
        long lngEmployeeDetailID;
        long lngEmployeeID;
        string strSQLShowHelpEmployee;
        string strSQLShowHelpPAN;

        string strStateCode;
        string strRPStateCode;
        string strDStateCode;
        string strMinistryCode;

        string strBatFile = "";
        string strFVUFile = "";
        string strCSIDownloadFilePath = "";

        string newOutputFileName = "";

        string[,] strArray;

        bool blnShowHelp = true;
        bool blnShowPANHelp = true;
        bool blnChkChanged = true;
        bool blnSectionDisplay = true;
        bool blnTagDeductee = true;
        //--            
        IDataReader drdShowEmployeePAN = null;
        ToolTip tllTip = new ToolTip();

        bool blnRestrictIncomeTaxCalculation = false;
        //-- 
        string strEmpDed = "";
        bool blnProceed = false;
        double dblTotalTaxDeposited = 0;
        long lngSelectedRows = 0;
        //

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

        #region TrnTagDeductees_Load
        private void TrnTagDeductees_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            //--
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //----
            GC.Collect();
            //
            //-----------------------------------------------------------
            lblTitle.Text = "Tag Deductee records";
            lblMode.Text = J_Mode.View;
            //-----------------------------------------------------------         
            txtFormNo.Text = TDSMAN.Classes.TDSMAN.T_pFormNo;
            txtFormNoDisplay.Text = TdsMan.GetFormNoIT2025(TDSMAN.Classes.TDSMAN.T_pFormNo) + " (" + TDSMAN.Classes.TDSMAN.T_pFormNo + ")";
            txtFinancialYear.Text = TDSMAN.Classes.TDSMAN.T_pFinancialYear;
            txtFinancialYearID.Text= Convert.ToString(TDSMAN.Classes.TDSMAN.T_pFinancialYearId);
            txtQuarter.Text = TDSMAN.Classes.TDSMAN.T_pQuarter;
            txtCompany.Text = TDSMAN.Classes.TDSMAN.T_pCompanyName;
            //--
            if(txtFormNo.Text == T_FormNo.F24Q)
                strEmpDed = "EMPLOYEE";
            else
                strEmpDed = "DEDUCTEE";
            //
            LoadDeducteeGrid(0, 0);
            //
            txtViewChallanDetailsDifference.Text = "0.00";
            txtViewChallanDetailsTotalTax.Text = "0.00";
            txtViewDeducteeTotalTaxDeposited.Text = "0.00";
            ClearTaggedControls();
            //
            BtnUntagDeductees.Enabled = false;
            BtnUntagDeductees.BackColor = Color.LightGray;
            //
            btnSave.Enabled = false;
            btnSave.BackColor = Color.LightGray;
            //-- LOAD CHALLAN DETAILS
            //ShowChallanDetailsRecord(lngChallanID); 
            //LoadChallanGrid(lngChallanID); 
            ////-- LOAD CHALLAN DESCRIPTOR
            LoadChallanComboBox(ref cmbChallanDescriptor);
            ////-- LOAD ORPHAN DEDUCTEES
            LoadTagDeducteeGrid(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, 0, 0, 0);
            ////-- LOAD SEARCH MONTH
            LoadSearchMonth(ref cmbSearchMonthWise);
            ////-- LOAD SEARCH SECTION
            if (cmnService.J_ReturnInt32Value(txtFinancialYearID.Text) >= T_FinancialYearID.F2013_14ID)
            {
                LoadSearchSection(ref cmbSearchSectionWise);
            }
            else
            {
                cmbSearchSectionWise.Visible = false;
                lblSearchSectionWiseCaption.Visible = false;
            }
            ////-- LOAD DEDUCTEE CODE
            //if (txtFormNo.Text != T_FormNo.F24Q)
            //{
            //    LoadSearchDeducteeCode(ref cmbSearchDeducteeCodeWise);
            //}
            //else
            //{
            //    lblSearchDeducteeCodeWiseCaption.Visible = false;
            //    cmbSearchDeducteeCodeWise.Visible = false;
            //}
            //
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            Insert_Update_Delete_Data(T_TAG_MODE.TAG_DEDUCTEE); GC.Collect();
            //            
        }
        #endregion

        #region BtnUntagDeductees_Click
        private void BtnUntagDeductees_Click(object sender, EventArgs e)
        {
            Insert_Update_Delete_Data(T_TAG_MODE.UNTAG_DEDUCTEE); GC.Collect();
            //            
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            //--
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion


        #region cmbChallanDescriptor_SelectedIndexChanged
        private void cmbChallanDescriptor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChallanDescriptor.SelectedIndex <= 0)
            {
                LoadDeducteeGrid(0, 0);
                txtViewChallanDetailsDifference.Text = "0.00";
                txtViewChallanDetailsTotalTax.Text = "0.00";
                txtViewDeducteeTotalTaxDeposited.Text = "0.00";
                //
                BtnUntagDeductees.Enabled = false;
                BtnUntagDeductees.BackColor = Color.LightGray;
                //
                btnSave.Enabled = false;
                btnSave.BackColor = Color.LightGray;
                return;
            }
            //
            BtnUntagDeductees.Enabled = true;
            BtnUntagDeductees.BackColor = Color.Lavender;
            //
            btnSave.Enabled = true;
            btnSave.BackColor = Color.Lavender;
            //
            LoadDeducteeGrid(Convert.ToInt32(Support.GetItemData(cmbChallanDescriptor, cmbChallanDescriptor.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_pBasicInfoId);
            LoadSummaryInformation(Convert.ToInt32(Support.GetItemData(cmbChallanDescriptor, cmbChallanDescriptor.SelectedIndex)));
            //
            //pnlChallanDeductee.Enabled = false;
            //pnlOrphanDeductee.Enabled = false;
            //BtnUntagDeductees.Enabled = false;
            //BtnUntagDeductees.BackColor = Color.LightGray;
            //btnSave.Enabled = false;
            //btnSave.BackColor = Color.LightGray;
            cmbChallanDescriptor.Enabled = false;
            //
        }
        #endregion

        #region cmbSearchWise_SelectedIndexChanged
        private void cmbSearchWise_SelectedIndexChanged(object sender, EventArgs e)
        {
            long MonthYear = 0; long SectionID = 0; int DeducteeCode = 0;
            try
            {
                if (cmbSearchMonthWise.SelectedIndex > 0)
                    MonthYear = Convert.ToInt32(Support.GetItemData(cmbSearchMonthWise, cmbSearchMonthWise.SelectedIndex));
                //
                if (cmbSearchSectionWise.SelectedIndex > 0)
                    SectionID = Convert.ToInt32(Support.GetItemData(cmbSearchSectionWise, cmbSearchSectionWise.SelectedIndex));
                //
                if (cmbSearchDeducteeCodeWise.SelectedIndex > 0)
                    DeducteeCode = Convert.ToInt32(Support.GetItemData(cmbSearchDeducteeCodeWise, cmbSearchDeducteeCodeWise.SelectedIndex));
                //
                ClearTaggedControls();
                //-- LOAD ORPHAN DEDUCTEES BASED ON SEARCH
                LoadTagDeducteeGrid(TDSMAN.Classes.TDSMAN.T_pBasicInfoId,
                                    MonthYear,
                                    SectionID,
                                    DeducteeCode);
            }
            catch(Exception err)
            {
            }
        }
        #endregion

        #region grdvTagDeductee_CurrentCellDirtyStateChanged
        private void grdvTagDeductee_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grdvTagDeductee.IsCurrentCellDirty)
            {
                grdvTagDeductee.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        #endregion

        #region grdvTagDeductee_CellValueChanged
        private void grdvTagDeductee_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (blnTagDeductee == false) return;
            //
                long lngSelectedRecords = 0;
                double dblSelectedTotal = 0;
                double dblSelectedTaxDeposited = 0;
                //
                foreach (DataGridViewRow row in grdvTagDeductee.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if ((bool)row.Cells[0].Value == true)
                        {
                            lngSelectedRecords = lngSelectedRecords + 1;
                            //
                            dblSelectedTotal = dblSelectedTotal + cmnService.J_ReturnDoubleValue(row.Cells[8].Value);
                            dblSelectedTaxDeposited = dblSelectedTaxDeposited + cmnService.J_ReturnDoubleValue(row.Cells[9].Value);
                        }
                    }
                    //else if ((bool)row.Cells[0].Selected == false)
                    //{
                    //}
                }
                //
                if(lngSelectedRecords == 1)
                {
                    lblSelectedCount.Visible = true;
                    lblSelectedCount.Text = "Tagged Summary : " + lngSelectedRecords.ToString() + " record selected";
                    lblSelectedTotal.Text = string.Format("{0:0.00}", dblSelectedTotal);
                    lblSelectedTaxDeposited.Text = string.Format("{0:0.00}", dblSelectedTaxDeposited);
                }
                else if(lngSelectedRecords > 1)
                {
                    lblSelectedCount.Visible = true;
                    lblSelectedCount.Text = "Tagged Summary : " + lngSelectedRecords.ToString() + " records selected";
                    lblSelectedTotal.Text = string.Format("{0:0.00}", dblSelectedTotal);
                    lblSelectedTaxDeposited.Text = string.Format("{0:0.00}", dblSelectedTaxDeposited);
                }
                else if(lngSelectedRecords == 0)
                {
                    lblSelectedCount.Visible = false;
                    lblSelectedTotal.Text = "0.00";
                    lblSelectedTaxDeposited.Text = "0.00";
                }                
                //
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion


        #region chkSelectDeselect_CheckedChanged
        private void chkSelectDeselect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                foreach (DataGridViewRow row in grdvTagDeductee.Rows)
                {
                    if (chkSelectDeselect.Checked == true)//checked all checkbox
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                        {
                            row.Cells[0].Value = true;
                        }
                    }
                    else//Unchecked all checkbox
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                        {
                            row.Cells[0].Value = false;
                        }
                    }
                } 
                this.Cursor = Cursors.Default;                            
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;                            
            }
        }
        #endregion


        #region btnLoadData_Click
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnLoadData.Text == "&Load")
                {
                    if (cmbChallanDescriptor.SelectedIndex <= 0)
                    {
                        LoadDeducteeGrid(0, 0);
                        txtViewChallanDetailsDifference.Text = "0.00";
                        txtViewChallanDetailsTotalTax.Text = "0.00";
                        txtViewDeducteeTotalTaxDeposited.Text = "0.00";
                        //
                        BtnUntagDeductees.Enabled = false;
                        BtnUntagDeductees.BackColor = Color.LightGray;
                        //
                        btnSave.Enabled = false;
                        btnSave.BackColor = Color.LightGray;
                        return;
                    }
                    //
                    BtnUntagDeductees.Enabled = true;
                    BtnUntagDeductees.BackColor = Color.Lavender;
                    //
                    btnSave.Enabled = true;
                    btnSave.BackColor = Color.Lavender;
                    //
                    lngChallanID = Convert.ToInt32(Support.GetItemData(cmbChallanDescriptor, cmbChallanDescriptor.SelectedIndex));
                    //
                    LoadDeducteeGrid(lngChallanID, TDSMAN.Classes.TDSMAN.T_pBasicInfoId);
                    LoadSummaryInformation(lngChallanID);
                    //
                    //pnlChallanDeductee.Enabled = false;
                    //pnlOrphanDeductee.Enabled = false;
                    //BtnUntagDeductees.Enabled = false;
                    //BtnUntagDeductees.BackColor = Color.LightGray;
                    //btnSave.Enabled = false;
                    //btnSave.BackColor = Color.LightGray;
                    cmbChallanDescriptor.Enabled = false;
                    btnLoadData.Text = "&Reset";
                    btnLoadData.ForeColor = Color.Red;
                    //
                }
                else if (btnLoadData.Text == "&Reset")
                {
                    cmbChallanDescriptor.Enabled = true;
                    btnLoadData.Text = "&Load";
                    btnLoadData.ForeColor = Color.Black;
                    //
                    cmbChallanDescriptor.SelectedIndex = 0;
                    //
                    LoadDeducteeGrid(0, 0);
                    txtViewChallanDetailsDifference.Text = "0.00";
                    txtViewChallanDetailsTotalTax.Text = "0.00";
                    txtViewDeducteeTotalTaxDeposited.Text = "0.00";
                    //
                    BtnUntagDeductees.Enabled = false;
                    BtnUntagDeductees.BackColor = Color.LightGray;
                    //
                    btnSave.Enabled = false;
                    btnSave.BackColor = Color.LightGray;
                    //
                    
                }
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        

        #endregion

        #region User Define Functions


        #region LoadChallanComboBox
        private void LoadChallanComboBox(ref ComboBox combobox)
        {
            //cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.SL_NO", J_SQLColFormat.ConvertToString)
            //----------
            strSQL = " SELECT CHALLAN_ID," +
                //"             'Sl No. - ' + CSTR(SL_NO) + ' Challan No. - ' + CSTR(CHALLAN_NO) + ' Challan Date - ' + CSTR(DEPOSIT_DATE) + ' BSR Code - ' + CSTR(BSR_CODE) AS CHALLAN " +
                //-- 2018/09/20
                "             'Sl No. - ' + " + cmnService.J_SQLDBFormat("SL_NO", J_SQLColFormat.ConvertToString) + " + ' Challan No. - ' + " + cmnService.J_SQLDBFormat("CHALLAN_NO", J_SQLColFormat.ConvertToString) + " + ' Challan Date - ' + " + cmnService.J_SQLDBFormat("DEPOSIT_DATE", J_SQLColFormat.ConvertToString) + " + ' BSR Code - ' + " + cmnService.J_SQLDBFormat("BSR_CODE", J_SQLColFormat.ConvertToString) + " AS CHALLAN " +
                "      FROM   TRN_CHALLAN " +
                "      WHERE  TRN_CHALLAN.BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + " " +
                "      ORDER BY CHALLAN_ID";
            if (dmlService.J_PopulateComboBox(strSQL, ref  combobox) == false) return;
            //-----------
        }
        #endregion

        #region LoadDeducteeGrid
        private void LoadDeducteeGrid(long ChallanID, long BasicInfoID)
        {
            //-----------------------------------------------------------
            string[,] strMatrixViewDeductee = {{"DEDUCTEE_DETAIL_ID", "0", "", "R", "", "F", ""},
                                        {"Sl ", "40", "", "R", "", "T", ""},
                                        {"Deductee_ID", "0", "S", "", "", "F", ""},
                                        {"PAN No.", "85", "S", "", "", "", "fill"},
                                        {"Party Name", "200", "S", "", "", "", "fill"},
                                        {"Section", "100", "S", "", "", "", ""},
                                        {"Amount", "75", "0.00", "R", "", "T", "fill"},
                                        {"Date", "68", "dd/MM/yyyy", "", "", "T", "fill"},
                                        {"Total", "100", "0.00", "R", "", "", ""},
                                        {"Tax Deposited", "100", "0.00", "R", "", "", "T"},
                                        {"TAX_AMOUNT", "0", "", "", "", "F", ""},
                                        {"SURCHARGE_AMOUNT", "0", "", "", "", "F", ""},
                                        {"CESS_AMOUNT", "0", "", "", "", "F", ""}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            strOrderBy = "TRN_DEDUCTEE_DETAILS.SL_NO";
            strQuery = "SELECT TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID   AS DEDUCTEE_DETAIL_ID," +
                     "         TRN_DEDUCTEE_DETAILS.SL_NO                AS SL_NO," +
                     "         MST_" + strEmpDed + "." + strEmpDed + "_ID   AS DEDUCTEE_ID," +
                     "         MST_" + strEmpDed + "." + strEmpDed + "_PAN  AS DEDUCTEE_PAN," +
                     "         MST_" + strEmpDed + "." + strEmpDed + "_NAME AS DEDUCTEE_NAME," +
                     "         MST_SECTION.SECTION_NO                    AS SECTION_NO," +
                     "         TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT       AS PAYMENT_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.PAYMENT_DATE         AS PAYMENT_DATE," +
                     "         TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT         AS TOTAL_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS TAX_DEPOSITED_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.TAX_AMOUNT           AS TAX_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT     AS SURCHARGE_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.CESS_AMOUNT          AS CESS_AMOUNT " +
                     "  FROM   TRN_DEDUCTEE_DETAILS," +
                     "         MST_" + strEmpDed + "," +
                     "         MST_SECTION " +
                     "  WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_" + strEmpDed + "." + strEmpDed + "_ID " +
                     "  AND    TRN_DEDUCTEE_DETAILS.SECTION_ID    = MST_SECTION.SECTION_ID " +
                     "  AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID    = " + ChallanID + " " +
                     "  AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " ";
            //-----------------------------------------------------------
            strSQL = strQuery + " ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvDeductee, strSQL, strMatrixViewDeductee);       //Show Data into the Grid                
        }
        #endregion       
        
        #region LoadTagDeducteeGrid
        private void LoadTagDeducteeGrid(long BasicInfoID, long MonthYearWise, long SectionWise, int DeducteeCode)
        {
            //string[,] strGridPayee ={{"Payee ID", "0", "", "R", "", "F", ""},
            //                      {"Group ID", "0", "", "R", "", "F", ""},
            //                      {"Payee", "600", "S", "", "", "T", "fill"}};

            //-----------------------------------------------------------
            string[,] strMatrixViewDeductee = {{"DEDUCTEE_DETAIL_ID", "0", "", "R", "", "F", ""},
                                        {"Sl No.", "40", "", "R", "", "T", ""},
                                        {"PAN No.", "85", "S", "", "", "T", "fill"},
                                        {"Party Name", "200", "S", "", "", "T", "fill"},
                                        {"Section", "80", "S", "", "", "T", ""},
                                        {"Amount", "75", "0.00", "R", "", "T", "fill"},
                                        {"Date", "68", "dd/MM/yyyy", "", "", "T", "fill"},
                                        {"Total", "75", "0.00", "R", "", "T", "fill"},
                                        {"Tax Deposited", "75", "0.00", "R", "", "T", "fill"},
                                        {"CHALLAN_ID", "0", "", "", "", "F", ""},
                                        {"TAX_AMOUNT", "0", "", "", "", "F", ""},
                                        {"SURCHARGE_AMOUNT", "0", "", "", "", "F", ""},
                                        {"CESS_AMOUNT", "0", "", "", "", "F", ""}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            strOrderBy = "TRN_DEDUCTEE_DETAILS.CHALLAN_ID DESC,  TRN_DEDUCTEE_DETAILS.SL_NO";
            strQuery = "SELECT TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID   AS DEDUCTEE_DETAIL_ID," +
                     "         TRN_DEDUCTEE_DETAILS.SL_NO                AS SL_NO," +
                     "         MST_" + strEmpDed + "." + strEmpDed + "_PAN  AS DEDUCTEE_PAN," +
                     "         MST_" + strEmpDed + "." + strEmpDed + "_NAME AS DEDUCTEE_NAME," +
                     "         MST_SECTION.SECTION_NO                    AS SECTION_NO," +
                     "         TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT       AS PAYMENT_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.PAYMENT_DATE         AS PAYMENT_DATE," +
                     "         TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT         AS TOTAL_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS TAX_DEPOSITED_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.CHALLAN_ID           AS CHALLAN_ID," +
                     "         TRN_DEDUCTEE_DETAILS.TAX_AMOUNT           AS TAX_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT     AS SURCHARGE_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.CESS_AMOUNT          AS CESS_AMOUNT " +
                     "  FROM   TRN_DEDUCTEE_DETAILS," +
                     "         MST_" + strEmpDed + "," +
                     "         MST_SECTION " +
                     "  WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_" + strEmpDed + "." + strEmpDed + "_ID " +
                     "  AND    TRN_DEDUCTEE_DETAILS.SECTION_ID    = MST_SECTION.SECTION_ID " +
                     "  AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID    = 0 " +
                     "  AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " ";
            //
            if (MonthYearWise > 0)
                strQuery = strQuery + " AND FORMAT(PAYMENT_DATE,'MMYYYY') =" + MonthYearWise + " ";
            //
            if (SectionWise > 0)
                strQuery = strQuery + " AND TRN_DEDUCTEE_DETAILS.SECTION_ID =" + SectionWise + " ";
            //
            //-----------------------------------------------------------
            strSQL = strQuery + " ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            blnTagDeductee = false;
            //
            TdsMan.PopulateGridView(grdvTagDeductee, dmlService.J_pCommand, strSQL, strMatrixViewDeductee);
            //-- LOAD SELECTION ON THE GRID            
            foreach (DataGridViewRow row in grdvTagDeductee.Rows)
            {
                if (row.Cells[10].Value != null && cmnService.J_ReturnInt16Value(Convert.ToString(row.Cells[10].Value)) > 0)
                {
                    row.Cells[0].Value = true;
                }
            }
            //
            if (grdvTagDeductee.RowCount > 0)
            {
                lblNoOfDeductees.Visible = true;
                if (grdvTagDeductee.RowCount == 1)
                    lblNoOfDeductees.Text = "Untagged deductee = " + grdvTagDeductee.RowCount.ToString();
                else
                    lblNoOfDeductees.Text = "Untagged deductee = " + grdvTagDeductee.RowCount.ToString() + " Nos.";
            }
            else
                lblNoOfDeductees.Visible = false;
            //--
            blnTagDeductee = true;
            //
        }
        #endregion       

        #region LoadSearchMonth
        private void LoadSearchMonth(ref ComboBox combobox)
        {
            //" + cmnService.J_SQLDBFormat("COR_HDR_BATCH.IMPORTED_DATE", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSS) + "
            //strSQL = " SELECT DISTINCT FORMAT(PAYMENT_DATE,'MMYYYY') AS MMYYYY, " +
            //        "         FORMAT(PAYMENT_DATE,'mmm')&' ' & FORMAT(PAYMENT_DATE,'YYYY') AS MON_YYYY " +
            //        " FROM    TRN_DEDUCTEE_DETAILS " +
            //        " WHERE   BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + " " +
            //        " AND     CHALLAN_ID = 0 " +
            //        " ORDER BY FORMAT(PAYMENT_DATE,'MMYYYY') ";
            //---------------------------------
            //-- 2018/09/20
            strSQL = " SELECT DISTINCT " + cmnService.J_SQLDBFormat("PAYMENT_DATE", J_SQLColFormat.DateFormatMMYYYY ) + " AS MMYYYY, " +
                    "         " + cmnService.J_SQLDBFormat("PAYMENT_DATE", J_SQLColFormat.DateFormatMMM) + " + ' ' +  " + cmnService.J_SQLDBFormat("PAYMENT_DATE", J_SQLColFormat.DateFormatYYYY) + " AS MON_YYYY " +
                    " FROM    TRN_DEDUCTEE_DETAILS " +
                    " WHERE   BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + " " +
                    " AND     CHALLAN_ID = 0 " +
                    " ORDER BY " + cmnService.J_SQLDBFormat("PAYMENT_DATE", J_SQLColFormat.DateFormatMMYYYY) + " ";
            if (dmlService.J_PopulateComboBox(strSQL, ref  combobox) == false) return;            
        }
        #endregion

        #region LoadSearchSection
        private void LoadSearchSection(ref ComboBox combobox)
        {
            strSQL = " SELECT DISTINCT MST_SECTION.SECTION_ID AS SECTION_ID, " +
                   "           MST_SECTION.SECTION_NO           AS SECTION_NO " +
                   "   FROM    TRN_DEDUCTEE_DETAILS," +
                   "           MST_SECTION " +
                   "   WHERE   TRN_DEDUCTEE_DETAILS.SECTION_ID =  MST_SECTION.SECTION_ID " +
                   "   AND     TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + " " +
                   "   AND     TRN_DEDUCTEE_DETAILS.CHALLAN_ID = 0 " +
                   "   ORDER BY MST_SECTION.SECTION_NO";
            if (dmlService.J_PopulateComboBox(strSQL, ref  combobox, -1  ) == false) return;        
        }
        #endregion

        #region LoadSearchDeducteeCode
        private void LoadSearchDeducteeCode(ref ComboBox combobox)
        {
            strSQL = " SELECT DISTINCT MST_DEDUCTEE.DEDUCTEE_CODE AS SECTION_ID, " +
                   "           MST_DEDUCTEE.DEDUCTEE_CODE           AS SECTION_NO " +
                   "   FROM    TRN_DEDUCTEE_DETAILS," +
                   "           MST_DEDUCTEE " +
                   "   WHERE   TRN_DEDUCTEE_DETAILS.PARTY_ID =  MST_DEDUCTEE.DEDUCTEE_ID " +
                   "   AND     TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + " " +
                   "   AND     TRN_DEDUCTEE_DETAILS.CHALLAN_ID = 0 " +
                   "   ORDER BY MST_DEDUCTEE.DEDUCTEE_CODE";
            if (dmlService.J_PopulateComboBox(strSQL, ref  combobox, -1) == false) return;   
        }
        #endregion

        #region LoadSummaryInformation
        private void LoadSummaryInformation(long ChallanId)
        {
            IDataReader drdShowChallanDetailsRecord = null;
            try
            {
                strSQL = "SELECT TOT_TAX, CTRL_TOT_TAX FROM TRN_CHALLAN WHERE CHALLAN_ID = " + ChallanId + " ";
                drdShowChallanDetailsRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowChallanDetailsRecord == null)
                {
                    txtViewChallanDetailsDifference.Text = "0.00";
                    txtViewChallanDetailsTotalTax.Text = "0.00";
                    txtViewDeducteeTotalTaxDeposited.Text = "0.00";
                    return;
                }
                //
                while (drdShowChallanDetailsRecord.Read())
                {
                    txtViewChallanDetailsTotalTax.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanDetailsRecord["TOT_TAX"]) == "" ? "0" : Convert.ToString(drdShowChallanDetailsRecord["TOT_TAX"])));
                    txtViewDeducteeTotalTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanDetailsRecord["CTRL_TOT_TAX"]) == "" ? "0" : Convert.ToString(drdShowChallanDetailsRecord["CTRL_TOT_TAX"])));
                }
                drdShowChallanDetailsRecord.Close();
                drdShowChallanDetailsRecord.Dispose();
                //--
                txtViewChallanDetailsDifference.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToDouble(txtViewChallanDetailsTotalTax.Text) - Convert.ToDouble(txtViewDeducteeTotalTaxDeposited.Text)));
                //
                if (Convert.ToDouble(txtViewChallanDetailsDifference.Text) != 0)
                    txtViewChallanDetailsDifference.ForeColor = Color.Red;
                else if (Convert.ToDouble(txtViewChallanDetailsDifference.Text) == 0)
                    txtViewChallanDetailsDifference.ForeColor = Color.Black;
            }
            catch (Exception err)
            {
            }
        }
        #endregion

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data(string Mode)
        {
            long lngDeducteeID = 0; 
            //long lngChallanID = 0; 
            long lngMaxDeducteeSrlNo = 0;
            string strIDs = ""; double dblCTRL_TDS = 0; double dblCTRL_SURCHARGE = 0; double dblCTRL_EDU_CESS = 0; double dblCTRL_TOT_TAX = 0; double dblCTRL_TOT = 0;
            long lngMinSrlNo = 0;
            //
            try
            {
                //--------------------------------------------
                switch (lblMode.Text)
                {
                    #region View
                    case J_Mode.View:
                        //*****
                        #region UNTAG_DEDUCTEE
                        if (Mode == T_TAG_MODE.UNTAG_DEDUCTEE)
                        {
                            if (dgvDeductee.CurrentRow == null)
                            {
                                cmnService.J_UserMessage("No deductee selected!!");
                                return;
                            }
                            //foreach (DGVControl.DGVControl row in dgvDeductee.RowCount )
                            //{
                            //lngChallanID = Convert.ToInt32(Support.GetItemData(cmbChallanDescriptor, cmbChallanDescriptor.SelectedIndex));
                            lngDeducteeID = Convert.ToInt64(dgvDeductee.Rows[dgvDeductee.CurrentRow.Index].Cells[0].Value);
                            lngMinSrlNo= Convert.ToInt64(dgvDeductee.Rows[dgvDeductee.CurrentRow.Index].Cells[1].Value);
                            //
                            if (lngDeducteeID > 0)
                            {
                                if (cmnService.J_UserMessage("Untagging Deductee.\nProceed??", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    dmlService.J_BeginTransaction();
                                    //--
                                    lngMaxDeducteeSrlNo = 0;
                                    lngMaxDeducteeSrlNo = cmnService.J_ReturnInt64Value(dmlService.J_ReturnMaxValue("TRN_DEDUCTEE_DETAILS", "SL_NO", "CHALLAN_ID = 0 AND BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId));
                                    //--
                                    strSQL = "UPDATE TRN_DEDUCTEE_DETAILS SET CHALLAN_ID = 0, SL_NO = " + (lngMaxDeducteeSrlNo + 1) + " WHERE DEDUCTEE_DETAIL_ID = " + lngDeducteeID;
                                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                    {
                                        dmlService.J_Rollback();
                                        return;
                                    }
                                    //
                                    strSQL = "UPDATE TRN_CHALLAN SET " +
                                        "           CTRL_TDS       = CTRL_TDS       - " + cmnService.J_ReturnDoubleValue(dgvDeductee.Rows[dgvDeductee.CurrentRow.Index].Cells[10].Value) + ", " +
                                        "           CTRL_SURCHARGE = CTRL_SURCHARGE - " + cmnService.J_ReturnDoubleValue(dgvDeductee.Rows[dgvDeductee.CurrentRow.Index].Cells[11].Value) + ", " +
                                        "           CTRL_EDU_CESS  = CTRL_EDU_CESS  - " + cmnService.J_ReturnDoubleValue(dgvDeductee.Rows[dgvDeductee.CurrentRow.Index].Cells[12].Value) + ", " +
                                        "           CTRL_TOT       = CTRL_TOT       - " + cmnService.J_ReturnDoubleValue(dgvDeductee.Rows[dgvDeductee.CurrentRow.Index].Cells[8].Value) + ", " +
                                        "           CTRL_TOT_TAX   = CTRL_TOT_TAX   - " + cmnService.J_ReturnDoubleValue(dgvDeductee.Rows[dgvDeductee.CurrentRow.Index].Cells[9].Value) + " " +
                                        "    WHERE  CHALLAN_ID     = " + lngChallanID;
                                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                    {
                                        dmlService.J_Rollback();
                                        return;
                                    }
                                    //
                                    // RECALCALUTING SRL NO.
                                    if (TdsMan.T_UpdateSrlNo(dmlService.J_pCommand, lngMinSrlNo, lngChallanID, Updt_SrlNo.DeducteeSrlNo) == false)
                                    {
                                        dmlService.J_Rollback();
                                        return;
                                    }
                                    //
                                    dmlService.J_Commit();
                                    // UPDATING ORPHAN SRL NO.
                                    //lngMaxDeducteeSrlNo = 0;
                                    //lngMaxDeducteeSrlNo = cmnService.J_ReturnInt64Value(dmlService.J_ReturnMaxValue("TRN_DEDUCTEE_DETAILS", "SL_NO", "CHALLAN_ID = 0 AND BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId));
                                    //strSQL = "UPDATE TRN_DEDUCTEE_DETAILS SET SL_NO = " + (lngMaxDeducteeSrlNo + 1) + " WHERE CHALLAN_ID = 0 AND DEDUCTEE_DETAIL_ID = " + lngDeducteeID;
                                    //if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                    //{
                                    //    dmlService.J_Rollback();
                                    //    return;
                                    //}
                                    //
                                    #region LOAD MODULE
                                    //-- LOAD ORPHAN DEDUCTEES
                                    LoadTagDeducteeGrid(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, 0, 0, 0);
                                    //-- LOAD SEARCH MONTH
                                    LoadSearchMonth(ref cmbSearchMonthWise);
                                    //-- LOAD SEARCH SECTION
                                    if (cmnService.J_ReturnInt32Value(txtFinancialYearID.Text) >= T_FinancialYearID.F2013_14ID)
                                    {
                                        LoadSearchSection(ref cmbSearchSectionWise);
                                    }
                                    else
                                    {
                                        cmbSearchSectionWise.Visible = false;
                                        lblSearchSectionWiseCaption.Visible = false;
                                    }
                                    //-- LOAD DEDUCTEE CODE
                                    if (txtFormNo.Text != T_FormNo.F24Q)
                                    {
                                        LoadSearchDeducteeCode(ref cmbSearchDeducteeCodeWise);
                                    }
                                    else
                                    {
                                        lblSearchDeducteeCodeWiseCaption.Visible = false;
                                        cmbSearchDeducteeCodeWise.Visible = false;
                                    }
                                    //
                                    //LoadDeducteeGrid(Convert.ToInt32(Support.GetItemData(cmbChallanDescriptor, cmbChallanDescriptor.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_pBasicInfoId);
                                    LoadDeducteeGrid(lngChallanID, TDSMAN.Classes.TDSMAN.T_pBasicInfoId);
                                    //
                                    //LoadSummaryInformation(Convert.ToInt32(Support.GetItemData(cmbChallanDescriptor, cmbChallanDescriptor.SelectedIndex)));
                                    LoadSummaryInformation(lngChallanID);
                                    //--
                                    #endregion
                                }
                                else
                                    return;
                            }
                            else
                            {
                                cmnService.J_UserMessage("No row selected");
                                return;
                            }

                            //}
                            //-----------------------------------------------------------
                        }
                        #endregion
                        //*****
                        #region TAG_DEDUCTEE
                        else if (Mode == T_TAG_MODE.TAG_DEDUCTEE)
                        {
                            if (ValidateFields(T_TAG_MODE.TAG_DEDUCTEE) == false) return;
                            //
                            if (blnProceed == true)
                            {
                                if (cmnService.J_UserMessage("Records selected : " + lngSelectedRows.ToString() + "\nTotal Tax Deposited : " + string.Format("{0:0.00}", dblTotalTaxDeposited) + "\nProceed tagging with the selected Challan??", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    return;
                            }
                            //--
                            //lngChallanID = Convert.ToInt32(Support.GetItemData(cmbChallanDescriptor, cmbChallanDescriptor.SelectedIndex));
                            //
                            this.Cursor = Cursors.WaitCursor;
                            // UPDATING ORPHAN SRL NO.
                            lngMaxDeducteeSrlNo = 0;
                            lngMaxDeducteeSrlNo = cmnService.J_ReturnInt64Value(dmlService.J_ReturnMaxValue("TRN_DEDUCTEE_DETAILS", "SL_NO", "CHALLAN_ID = " + lngChallanID));
                            //--
                            lngMinSrlNo = 1;
                            foreach (DataGridViewRow row in grdvTagDeductee.Rows)
                            {
                                strIDs = Convert.ToString(row.Cells[1].Value);
                                //lngDeducteeID = cmnService.J_ReturnInt64Value(Convert.ToString(row.Cells[1].Value));
                                //
                                if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                                {
                                    //strIDs = strIDs + "," + Convert.ToString(row.Cells[1].Value);
                                    //strIDs = Convert.ToString(row.Cells[1].Value);
                                    //
                                    dblCTRL_TDS = dblCTRL_TDS + cmnService.J_ReturnDoubleValue(row.Cells[10].Value);
                                    //
                                    dblCTRL_SURCHARGE = dblCTRL_SURCHARGE + cmnService.J_ReturnDoubleValue(row.Cells[11].Value);
                                    //
                                    dblCTRL_EDU_CESS = dblCTRL_EDU_CESS + cmnService.J_ReturnDoubleValue(row.Cells[12].Value);
                                    //
                                    dblCTRL_TOT = dblCTRL_TOT + cmnService.J_ReturnDoubleValue(row.Cells[8].Value);
                                    //
                                    dblCTRL_TOT_TAX = dblCTRL_TOT_TAX + cmnService.J_ReturnDoubleValue(row.Cells[9].Value);
                                    //
                                    //if(lngMinSrlNo == 0)
                                    //lngMinSrlNo = Convert.ToInt32(row.Cells[2].Value);
                                    //--
                                    dmlService.J_BeginTransaction(); 
                                    //--
                                    //if (TdsMan.T_UpdateSrlNo(dmlService.J_pCommand, lngMinSrlNo, TDSMAN.Classes.TDSMAN.T_pBasicInfoId, Updt_SrlNo.OrphanDeducteeSrlNo) == false)
                                    //{
                                    //    dmlService.J_Rollback();
                                    //    return;
                                    //}
                                    //--
                                    strSQL = @"UPDATE TRN_DEDUCTEE_DETAILS 
                                               SET    SL_NO      = " + (lngMaxDeducteeSrlNo + 1) + @",
                                                      CHALLAN_ID = " + lngChallanID + @" 
                                               WHERE  DEDUCTEE_DETAIL_ID IN (" + strIDs + @")";
                                    //--
                                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                    {
                                        dmlService.J_Rollback();
                                        return;
                                    }
                                    //--
                                    dmlService.J_Commit();
                                    //
                                    lngMaxDeducteeSrlNo = lngMaxDeducteeSrlNo + 1;
                                    //
                                    lngMinSrlNo = lngMinSrlNo - 1;  
                                    //if (lngMinID == 0)
                                    //    lngMinID = Convert.ToInt32(strIDs);
                                    //else
                                    //{

                                    //}
                                }
                                //--
                                strSQL = "UPDATE TRN_DEDUCTEE_DETAILS SET SL_NO = " + lngMinSrlNo + " WHERE CHALLAN_ID = 0 AND DEDUCTEE_DETAIL_ID = " + strIDs;
                                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                {
                                    dmlService.J_Rollback();
                                    return;
                                }
                                //--
                                lngMinSrlNo = lngMinSrlNo + 1;                                    
                                //if (TdsMan.T_UpdateSrlNo(dmlService.J_pCommand, lngMinSrlNo, TDSMAN.Classes.TDSMAN.T_pBasicInfoId, Updt_SrlNo.OrphanDeducteeSrlNo) == false)
                                //{
                                //    dmlService.J_Rollback();
                                //    return;
                                //}
                                    
                            }
                            //
                            dmlService.J_BeginTransaction();
                            //
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "           CTRL_TDS       = CTRL_TDS + " + dblCTRL_TDS + "," +
                                "           CTRL_SURCHARGE = CTRL_SURCHARGE + " + dblCTRL_SURCHARGE + "," +
                                "           CTRL_EDU_CESS  = CTRL_EDU_CESS + " + dblCTRL_EDU_CESS + "," +
                                "           CTRL_TOT       = CTRL_TOT + " + dblCTRL_TOT + "," +
                                "           CTRL_TOT_TAX   = CTRL_TOT_TAX + " + dblCTRL_TOT_TAX + " " +
                                "    WHERE  CHALLAN_ID     = " + lngChallanID;
                            //
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                            //
                            // RECALCULATING SRL NO.
                            //if (TdsMan.T_UpdateSrlNo(dmlService.J_pCommand, lngMinSrlNo, TDSMAN.Classes.TDSMAN.T_pBasicInfoId, Updt_SrlNo.OrphanDeducteeSrlNo) == false)
                            //{
                            //    dmlService.J_Rollback();
                            //    return;
                            //}
                            ////
                            dmlService.J_Commit();
                            //
                            this.Cursor = Cursors.Default;
                            //
                            #region LOAD MODULE
                            //-- LOAD ORPHAN DEDUCTEES
                            LoadTagDeducteeGrid(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, 0, 0, 0);
                            //
                            chkSelectDeselect.Checked = false;
                            //-- LOAD SEARCH MONTH
                            LoadSearchMonth(ref cmbSearchMonthWise);
                            //-- LOAD SEARCH SECTION
                            if (cmnService.J_ReturnInt32Value(txtFinancialYearID.Text) >= T_FinancialYearID.F2013_14ID)
                            {
                                LoadSearchSection(ref cmbSearchSectionWise);
                            }
                            else
                            {
                                cmbSearchSectionWise.Visible = false;
                                lblSearchSectionWiseCaption.Visible = false;
                            }
                            //-- LOAD DEDUCTEE CODE
                            if (txtFormNo.Text != T_FormNo.F24Q)
                            {
                                LoadSearchDeducteeCode(ref cmbSearchDeducteeCodeWise);
                            }
                            else
                            {
                                lblSearchDeducteeCodeWiseCaption.Visible = false;
                                cmbSearchDeducteeCodeWise.Visible = false;
                            }
                            //
                            //LoadDeducteeGrid(Convert.ToInt32(Support.GetItemData(cmbChallanDescriptor, cmbChallanDescriptor.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_pBasicInfoId);
                            LoadDeducteeGrid(lngChallanID, TDSMAN.Classes.TDSMAN.T_pBasicInfoId);
                            //
                            //LoadSummaryInformation(Convert.ToInt32(Support.GetItemData(cmbChallanDescriptor, cmbChallanDescriptor.SelectedIndex)));
                            LoadSummaryInformation(lngChallanID);
                            //--
                            #endregion
                        }
                        #endregion
                        //*****
                        break;
                    #endregion
                }
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields(string Mode)
        {
            try
            {
                if (Mode == T_TAG_MODE.TAG_DEDUCTEE)
                {
                    if (grdvTagDeductee.CurrentRow == null)
                    {
                        cmnService.J_UserMessage("No deductee selected!!");
                        return false;
                    }
                    //--
                    dblTotalTaxDeposited = 0; blnProceed = false; lngSelectedRows = 0;
                    //
                    foreach (DataGridViewRow row in grdvTagDeductee.Rows)
                    {
                        //
                        if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                        {
                            blnProceed = true;
                            dblTotalTaxDeposited = dblTotalTaxDeposited + cmnService.J_ReturnDoubleValue(row.Cells[8].Value);
                            lngSelectedRows = lngSelectedRows + 1;
                        }
                    }
                    //
                    if (lngSelectedRows == 0)
                    {
                        cmnService.J_UserMessage("No Deductee selected for Tagging");
                        grdvTagDeductee.Select();
                        return false;
                    }
                    //
                    if (cmnService.J_ReturnDoubleValue(txtViewDeducteeTotalTaxDeposited.Text) + dblTotalTaxDeposited > cmnService.J_ReturnDoubleValue(txtViewChallanDetailsTotalTax.Text))
                    {
                        cmnService.J_UserMessage("Total Deductee deposit can never be more than the Challan deposit");
                        grdvTagDeductee.Select();
                        return false;
                    }
                    //

                }
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion

        #region ShowChallanDetailsRecord
        private bool ShowChallanDetailsRecord(long Id)
        {
            IDataReader drdShowChallanDetailsRecord = null;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            try
            {
                //lblViewChallanDetailsSectionId.Text = "";
                //--
                string[,] strShowChallanDetailsRecordMatrix = {{"TRN_CHALLAN.BOOK_ENTRY = 1 ", "F", "TRN_CHALLAN.TRANSFER_VOUCHER_NO", "F"},
                                                               {"TRN_CHALLAN.BOOK_ENTRY = 0 ", "F", "TRN_CHALLAN.CHALLAN_NO", "F"}};
                //
                string[,] strLoadChallanTotTaxMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
                                                        {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED- TRN_CHALLAN.LATE_FEE", "F"}};
                // 
                strSQL = "SELECT TRN_CHALLAN.CHALLAN_ID          AS CHALLAN_ID," +
                    "            TRN_CHALLAN.SL_NO               AS SL_NO," +
                    "            TRN_CHALLAN.SECTION_ID          AS SECTION_ID," +
                    "            MST_SECTION.SECTION_NO          AS SECTION_NO," +
                    "            " + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEPOSIT_DATE," +
                    "            TRN_CHALLAN.BSR_CODE            AS BSR_CODE," +
                    "            " + cmnService.J_SQLDBFormat(strShowChallanDetailsRecordMatrix, J_SQLColFormat.Case_End) + " AS CHALLAN_NO," +
                    "            TRN_CHALLAN.CHEQUE_NO           AS CHEQUE_NO," +
                    "            TRN_CHALLAN.TDS                 AS TDS," +
                    "            TRN_CHALLAN.SURCHARGE           AS SURCHARGE," +
                    "            TRN_CHALLAN.EDUCATION_CESS      AS EDUCATION_CESS," +
                    "            TRN_CHALLAN.INTEREST            AS INTEREST," +
                    "            TRN_CHALLAN.OTHERS              AS OTHERS," +
                    "            " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " AS TOT_TAX," +
                    "            TRN_CHALLAN.CTRL_TOT_TAX        AS CTRL_TOT_TAX," +
                    "            TRN_CHALLAN.INTEREST_ALLOCATED  AS INTEREST_ALLOCATED," +
                    "            TRN_CHALLAN.OTHERS_ALLOCATED    AS OTHERS_ALLOCATED," +
                    "            TRN_CHALLAN.REMARKS             AS REMARKS," +
                    "            TRN_CHALLAN.BOOK_ENTRY          AS BOOK_ENTRY " +
                    "     FROM  (TRN_CHALLAN LEFT JOIN MST_SECTION " +
                    "            ON  TRN_CHALLAN.SECTION_ID    = MST_SECTION.SECTION_ID)" +
                    "     WHERE  TRN_CHALLAN.CHALLAN_ID = " + Id + " ";
                drdShowChallanDetailsRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowChallanDetailsRecord == null)
                    return false;
                //
                while (drdShowChallanDetailsRecord.Read())
                {
                    lngChallanID = Id;
                    //
                    //txtViewChallanDetailsSrlNo.Text = Convert.ToString(drdShowChallanDetailsRecord["SL_NO"]);
                    //txtViewChallanDetailsSection.Text = Convert.ToString(drdShowChallanDetailsRecord["SECTION_NO"]);
                    //lblViewChallanDetailsSectionId.Text = Convert.ToString(drdShowChallanDetailsRecord["SECTION_ID"]);
                    //mskViewChallanDetailsDate.Text = Convert.ToString(drdShowChallanDetailsRecord["DEPOSIT_DATE"]);
                    //txtViewChallanDetailsChallanNo.Text = Convert.ToString(drdShowChallanDetailsRecord["CHALLAN_NO"]);
                    //
                    //if (Convert.ToString(drdShowChallanDetailsRecord["BOOK_ENTRY"]) == "1")
                    //    lblViewChallanDetailsChallanNo.Text = "Trf Vchr(DDO Sl.)";
                    //else if (Convert.ToString(drdShowChallanDetailsRecord["BOOK_ENTRY"]) == "0")
                    //    lblViewChallanDetailsChallanNo.Text = "Challan No.";

                    //txtViewChallanDetailsTotalTax.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanDetailsRecord["TOT_TAX"]) == "" ? "0" : Convert.ToString(drdShowChallanDetailsRecord["TOT_TAX"])));
                    //txtViewDeducteeTotalTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanDetailsRecord["CTRL_TOT_TAX"]) == "" ? "0" : Convert.ToString(drdShowChallanDetailsRecord["CTRL_TOT_TAX"])));
                    //--
                    drdShowChallanDetailsRecord.Close();
                    drdShowChallanDetailsRecord.Dispose();
                    //
                    if (Convert.ToInt32(txtFinancialYearID.Text) >= T_FinancialYearID.F2013_14ID)
                    {
                        //lblViewChallanDetailsSection.Visible = false;
                        //txtViewChallanDetailsSection.Visible = false;
                    }
                    else
                    {
                        //lblViewChallanDetailsSection.Visible = true;
                        //txtViewChallanDetailsSection.Visible = true;
                    }
                    //
                    //txtDeducteeName.Select();
                    return true;
                }
                //-----------------------------------------------------------
                drdShowChallanDetailsRecord.Close();
                drdShowChallanDetailsRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                //lngChallanID = 0;
                ////-----------------------------------------------------------
                //if (strCheckFields == "")
                //    strSQL = strQuery + "order by " + strOrderBy;
                //else
                //    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                ////-----------------------------------------------------------
                //if (dsetChallanDetailsGridClone != null) dsetChallanDetailsGridClone.Clear();
                //dsetChallanDetailsGridClone = dmlService.J_ShowDataInGrid(ref dgcViewChallan, strSQL, strMatrix);       //Show Data into the Grid
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowChallanDetailsRecord.Close();
                drdShowChallanDetailsRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region LoadChallanGrid
        private void LoadChallanGrid(long lngChallanID)
        {
            if (lngChallanID <= 0)
            {
                LoadDeducteeGrid(0, 0);
                txtViewChallanDetailsDifference.Text = "0.00";
                txtViewChallanDetailsTotalTax.Text = "0.00";
                txtViewDeducteeTotalTaxDeposited.Text = "0.00";
                //
                BtnUntagDeductees.Enabled = false;
                BtnUntagDeductees.BackColor = Color.LightGray;
                //
                btnSave.Enabled = false;
                btnSave.BackColor = Color.LightGray;
                return;
            }
            //
            BtnUntagDeductees.Enabled = true;
            BtnUntagDeductees.BackColor = Color.Lavender;
            //
            btnSave.Enabled = true;
            btnSave.BackColor = Color.Lavender;
            //
            LoadDeducteeGrid(lngChallanID, TDSMAN.Classes.TDSMAN.T_pBasicInfoId);
            LoadSummaryInformation(lngChallanID);
        }
        #endregion

        #region ClearTaggedControls
        private void ClearTaggedControls()
        {            
            lblSelectedCount.Text = "";
            lblSelectedTotal.Text = "0.00";
            lblSelectedTaxDeposited.Text = "0.00";
            chkSelectDeselect.Checked = false;
        }
        #endregion





        #endregion


    }
}