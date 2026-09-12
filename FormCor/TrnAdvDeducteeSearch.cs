#region Refered Namespaces & Classes

//~~~~ System Namespaces ~~~~
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnAdvDeducteeSearch : Form
    {
        #region Constructor
        public TrnAdvDeducteeSearch(long BatchId)
        {
            lngBatchId = BatchId;

            InitializeComponent();
        }
        #endregion

        #region Private Variables and Class Objects
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

        int intCountRecords = 0;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;

        long lngBatchId = 0;

        #endregion 

        #region User Defined Events

        #region TrnAdvDeducteeSearch_Load
        private void TrnAdvDeducteeSearch_Load(object sender, EventArgs e)
        {
            GC.Collect();
            //
            IDataReader reader = null;

            //Populating return information i.e
            //company name
            //fa year
            //form no
            //quarter

            strSQL = "SELECT COR_TRN_COMPANY.COMPANY_NAME AS COMPANY_NAME, " +
                     "       COR_TRN_COMPANY.TAN_NO       AS TAN_NO," +
                     "       COR_HDR_BATCH.FORM_NO        AS FORM_NO, " +
                     "       COR_HDR_BATCH.QTR            AS QTR, " +
                     "       MST_ASSESSMENT.FA_YEAR       AS FA_YEAR " +
                     "FROM   COR_HDR_BATCH, " +
                     "       COR_TRN_COMPANY, " +
                     "       MST_ASSESSMENT " +
                     "WHERE  COR_HDR_BATCH.BATCH_HEADER_ID = COR_TRN_COMPANY.BATCH_HEADER_ID " +
                     "AND    COR_HDR_BATCH.ASST_ID         = MST_ASSESSMENT.ASST_ID " +
                     "AND    COR_HDR_BATCH.BATCH_HEADER_ID = " + lngBatchId;

            reader = dmlService.J_ExecSqlReturnReader(strSQL);

            while (reader.Read())
            {
                lblCompanyName.Text = Convert.ToString(reader["COMPANY_NAME"]);
                lblTAN.Text = Convert.ToString(reader["TAN_NO"]);
                lblFAYear.Text = Convert.ToString(reader["FA_YEAR"]);
                lblFormNo.Text = Convert.ToString(reader["FORM_NO"]);
                lblQuarter.Text = Convert.ToString(reader["QTR"]);
            }

            reader.Close();
            reader.Dispose();

            //Now Populating Grid
            PopulateGrid();

            //Now populating section combo
            //Only those section populated which is used in this return

            //strSQL = "SELECT DISTINCT MST_SECTION.SECTION_ID AS SECTION_ID, " +
            //         "       MST_SECTION.SECTION_NO AS SECTION_NO " +
            //         "FROM  MST_SECTION, " +
            //         "      COR_TRN_CHALLAN " +
            //         "WHERE COR_TRN_CHALLAN.SECTION_ID = MST_SECTION.SECTION_ID " +
            //         "AND   COR_TRN_CHALLAN.BATCH_HEADER_ID = " + lngBatchId;

            strSQL = "SELECT DISTINCT MST_SECTION.SECTION_ID AS SECTION_ID, " +
                     "       MST_SECTION.SECTION_NO AS SECTION_NO " +
                     "FROM  MST_SECTION LEFT JOIN COR_TRN_DEDUCTEE_DETAILS " +
                     "      ON COR_TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID " +
                     "WHERE COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBatchId;

            dmlService.J_PopulateComboBox(strSQL, ref cmbSection);

            //Checking if Combo box only has one item and one blank
            if (cmbSection.Items.Count == 2)
                cmbSection.SelectedIndex = 1;

        }
        #endregion

        #region cmbSection_SelectedIndexChanged
        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateFilters();
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            GC.Collect();
            //
            this.Dispose();
            this.Close();
        }
        #endregion

        #region dgcViewRecords_CellFormatting
        private void dgcViewRecords_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            #region CellFormatting(DEDUCTEE_PAN)

            if (dgcViewRecords.Columns[e.ColumnIndex].DataPropertyName == "PARTY_PAN")
            {
                //CHECKING THE INVALID PAN FLAG FOR THIS PAN
                if (dgcViewRecords.Rows[e.RowIndex].Cells["INVALID_PAN"].Value.ToString() == "1")
                {
                    //PAN IS INVALID

                    //NOW CHECKING IF THE PAN HAS BEEN MODIFIED OR THE RECORD HAS BEEN DELETED

                    if (dgcViewRecords.Rows[e.RowIndex].Cells["PAN_UPDATION_INDICATOR"].Value.ToString() != "1" &&
                        dgcViewRecords.Rows[e.RowIndex].Cells["MODE"].Value.ToString() != "Del")
                    {
                        //PAN HAS NOT BEEN MODIFIED AND HAS NOT BEEN DELETED

                        //NOW HIGHLIGHLING THE CELL 
                        //highlighting the cell
                        //e.CellStyle.BackColor = Color.FromArgb(231, 206, 248); //PURPLE COLOR
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;

                        if (e.Value.ToString() == "PANNOTAVBL")
                            dgcViewRecords.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Enter the Valid PAN if now available";
                        else if (e.Value.ToString() == "PANINVALID")
                            dgcViewRecords.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Enter the Valid PAN if now available";
                        else if (e.Value.ToString() == "PANAPPLIED")
                            dgcViewRecords.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Enter the Valid PAN if now available";
                        else
                            dgcViewRecords.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "This PAN (" + e.Value.ToString() + ") has been marked \nInvalid by the Income Tax Department";
                    }
                }
            }
            #endregion

            #region CellFormatting(MODE)
            if (dgcViewRecords.Columns[e.ColumnIndex].DataPropertyName == "MODE")
            {
                if (e.Value.ToString() == "Upd")
                {

                    #region Highlighting cell for Update record

                    // ----------------------------------------------
                    // -- HIGHLIGHTING THE UPDATE MODE CELL
                    // ----------------------------------------------

                    e.CellStyle.BackColor = Color.Yellow;
                    dgcViewRecords.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "This record is updated";

                    string strPreviousValue = "";
                    string strNewValue = "";

                    // -- FOR HIGHLIGHTING DEDUCTEE PAN
                    //CHECKING IF THE PAN IS UPDATED

                    strPreviousValue = Convert.ToString(dgcViewRecords.Rows[e.RowIndex].Cells["HDR_DEDUCTEE_PAN"].Value);
                    strNewValue = Convert.ToString(dgcViewRecords.Rows[e.RowIndex].Cells["PARTY_PAN"].Value);

                    if (strPreviousValue != strNewValue)
                    {
                        dgcViewRecords.Rows[e.RowIndex].Cells["PARTY_PAN"].Style.BackColor = Color.Yellow;
                        dgcViewRecords.Rows[e.RowIndex].Cells["PARTY_PAN"].ToolTipText = "Old Value\t: " + strPreviousValue +
                                                                                                               "\nNew Value\t: " + strNewValue;
                    }

                    // -- FOR HIGHLIGHTING DEDUCTEE NAME
                    //CHECKING IF THE NAME IS UPDATED
                    strPreviousValue = Convert.ToString(dgcViewRecords.Rows[e.RowIndex].Cells["HDR_DEDUCTEE_NAME"].Value);
                    strNewValue = Convert.ToString(dgcViewRecords.Rows[e.RowIndex].Cells["PARTY_NAME"].Value);

                    if (strPreviousValue != strNewValue)
                    {
                        dgcViewRecords.Rows[e.RowIndex].Cells["PARTY_NAME"].Style.BackColor = Color.Yellow;
                        dgcViewRecords.Rows[e.RowIndex].Cells["PARTY_NAME"].ToolTipText = "Old Value\t: " + strPreviousValue +
                                                                                          "\nNew Value\t: " + strNewValue;
                    }

                    // -- FOR HIGHLIGHTING PAYMENT_AMOUNT
                    strPreviousValue = string.Format("{0:0.00}", Convert.ToDouble(dgcViewRecords.Rows[e.RowIndex].Cells["HDR_PAYMENT_AMOUNT"].Value));
                    strNewValue = string.Format("{0:0.00}", Convert.ToDouble(dgcViewRecords.Rows[e.RowIndex].Cells["AMOUNT"].Value));

                    if (strPreviousValue != strNewValue)
                    {
                        dgcViewRecords.Rows[e.RowIndex].Cells["AMOUNT"].Style.BackColor = Color.Yellow;
                        dgcViewRecords.Rows[e.RowIndex].Cells["AMOUNT"].ToolTipText = "Old Value\t: " + strPreviousValue +
                                                                                                            "\nNew Value\t: " + strNewValue;
                    }

                    // -- FOR HIGHLIGHTING PAYMENT_DATE
                    strPreviousValue = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dgcViewRecords.Rows[e.RowIndex].Cells["HDR_PAYMENT_DATE"].Value));
                    strNewValue = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dgcViewRecords.Rows[e.RowIndex].Cells["PAYMENT_DATE"].Value));

                    if (strPreviousValue != strNewValue)
                    {
                        dgcViewRecords.Rows[e.RowIndex].Cells["PAYMENT_DATE"].Style.BackColor = Color.Yellow;
                        dgcViewRecords.Rows[e.RowIndex].Cells["PAYMENT_DATE"].ToolTipText = "Old Value\t: " + strPreviousValue +
                                                                                                            "\nNew Value\t: " + strNewValue;
                    }

                    // -- FOR HIGHLIGHTING TOTAL DEPOSITED
                    strPreviousValue = string.Format("{0:0.00}", Convert.ToDouble(dgcViewRecords.Rows[e.RowIndex].Cells["HDR_TAX_DEPOSITED_AMOUNT"].Value));
                    strNewValue = string.Format("{0:0.00}", Convert.ToDouble(dgcViewRecords.Rows[e.RowIndex].Cells["TAX_DEPOSITED"].Value));

                    if (strPreviousValue != strNewValue)
                    {
                        dgcViewRecords.Rows[e.RowIndex].Cells["TAX_DEPOSITED"].Style.BackColor = Color.Yellow;
                        dgcViewRecords.Rows[e.RowIndex].Cells["TAX_DEPOSITED"].ToolTipText = "Old Value\t: " + strPreviousValue +
                                                                                             "\nNew Value\t: " + strNewValue;
                    }

                    #endregion

                }

                else if (e.Value.ToString() == "Add" || e.Value.ToString() == "New")
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    dgcViewRecords.Rows[e.RowIndex].Cells["MODE"].ToolTipText = "New Record Added";
                }

                else if (e.Value.ToString() == "Del")
                {
                    e.CellStyle.BackColor = Color.FromArgb(231, 206, 248);
                    dgcViewRecords.Rows[e.RowIndex].Cells["MODE"].ToolTipText = "This record is marked for deletion.\n";
                }
            }
            #endregion
        }

        #endregion

        #region btnPrintAdvDeducteeSearch_Click
        private void btnPrintAdvDeducteeSearch_Click(object sender, EventArgs e)
        {
            //
            string SearchFilters = "";
            if (cmbSection.SelectedIndex > 0)
                SearchFilters = " SECTION : " + cmbSection.Text + " ,";
            if (txtDeducteeName.Text.Trim() != "")
                SearchFilters = SearchFilters + " DEDUCTEE NAME : " + txtDeducteeName.Text + " ,";
            if (txtDeducteePAN.Text.Trim() != "")
                SearchFilters = SearchFilters + " PAN : " + txtDeducteePAN.Text + " ,";
            if (chkInvalidPAN.Checked == true)
                SearchFilters = SearchFilters + " INVALID PAN ";
            //

            TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;

            RptDialog rptDialog = new RptDialog();
            rptDialog.PrintAdvDeducteeSearch(SearchFilters,strSQL,lblCompanyName.Text,lblTAN.Text, lblFAYear.Text,lblQuarter.Text,lblFormNo.Text);
        }
        #endregion


        #endregion

        #region User Defined Functions

        #region PopulateGrid
        public void PopulateGrid()
        {
            string[,] strMatrixViewDeductee = {{"Challan Sl.", "40", "", "", "", "", ""},
                                               {"Party Sl.", "45", "", "", "", "", ""},
                                               {"Mode", "45", "", "", "", "", ""},
                                               {"Section", "45", "", "", "", "", ""},
                                               {"Deductee Name", "120", "", "", "", "", "T"},
                                               {"PAN No.", "85", "", "", "", "", ""},
                                               {"Date", "70", "d", "", "", "", ""},
                                               {"Amount", "80", "0.00", "R", "", "", ""},
                                               {"Tax Deposited", "70", "0.00", "R", "", "", ""},
                                               {"INVALID_PAN", "0", "0", "R", "", "F", ""},
                                               {"HDR_DEDUCTEE_NAME", "0", "", "R", "", "F", ""},
                                               {"HDR_DEDUCTEE_PAN", "0", "", "R", "", "F", ""},
                                               {"HDR_PAYMENT_DATE", "0", "d", "R", "", "F", ""},
                                               {"HDR_PAYMENT_AMOUNT", "0", "0.00", "R", "", "F", ""},
                                               {"HDR_TAX_DEPOSITED_AMOUNT", "0", "0.00", "R", "", "F", ""},
                                               {"PAN_UPDATION_INDICATOR", "0", "0", "R", "", "F", ""}};

            strOrderBy = "MST_ASSESSMENT.FA_YEAR, " +
                         "COR_HDR_BATCH.QTR, " +
                         "COR_TRN_CHALLAN.SL_NO, " +
                         "COR_TRN_DEDUCTEE_DETAILS.SL_NO ";

            string[,] strLoadDeducteeMode = {{"COR_TRN_DEDUCTEE_DETAILS.MODE = 'U'", "F", "Upd", "T"},
                                             {"COR_TRN_DEDUCTEE_DETAILS.MODE = 'A'", "F", "Add", "T"},
                                             {"COR_TRN_DEDUCTEE_DETAILS.MODE = 'D'", "F", "Del", "T"},
                                             {"COR_TRN_DEDUCTEE_DETAILS.MODE = 'O'", "F", "New", "T"},
                                             {"COR_TRN_DEDUCTEE_DETAILS.PAN_UPDATION_INDICATOR = 1", "F", "Upd", "T"},
                                             {"COR_TRN_DEDUCTEE_DETAILS.MODE = ''", "F", "", "T"}};


            strQuery = "SELECT COR_TRN_CHALLAN.SL_NO                           AS CHALLAN_SL_NO, " +
                       "       COR_TRN_DEDUCTEE_DETAILS.SL_NO                  AS PARTY_SL_NO, " +
                       "       " + cmnService.J_SQLDBFormat(strLoadDeducteeMode, J_SQLColFormat.Case_End) + " AS MODE," +
                       "       MST_SECTION.SECTION_NO                          AS SECTION_NO, " +
                       "       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME          AS PARTY_NAME, " +
                       "       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN           AS PARTY_PAN, " +
                       "       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE           AS PAYMENT_DATE, " +
                       "       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT         AS AMOUNT, " +
                       "       COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT   AS TAX_DEPOSITED," +
                       "       " + cmnService.J_SQLDBFormat("COR_HDR_DEDUCTEE_DETAILS.INVALID_PAN", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "         AS INVALID_PAN," +
                       "       " + cmnService.J_SQLDBFormat("COR_HDR_DEDUCTEE_DETAILS.DEDUCTEE_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + "        AS HDR_DEDUCTEE_NAME," +
                       "       " + cmnService.J_SQLDBFormat("COR_HDR_DEDUCTEE_DETAILS.DEDUCTEE_PAN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "         AS HDR_DEDUCTEE_PAN," +
                       "       COR_HDR_DEDUCTEE_DETAILS.PAYMENT_DATE           AS HDR_PAYMENT_DATE," +
                       "       " + cmnService.J_SQLDBFormat("COR_HDR_DEDUCTEE_DETAILS.PAYMENT_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "       AS HDR_PAYMENT_AMOUNT," +
                       "       " + cmnService.J_SQLDBFormat("COR_HDR_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS HDR_TAX_DEPOSITED_AMOUNT," +
                       "       COR_TRN_DEDUCTEE_DETAILS.PAN_UPDATION_INDICATOR AS PAN_UPDATION_INDICATOR " +
                       "FROM (((((COR_HDR_BATCH " +
                       "INNER JOIN MST_ASSESSMENT " +
                       "ON COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID) " +
                       "INNER JOIN COR_TRN_CHALLAN " +
                       "ON COR_HDR_BATCH.BATCH_HEADER_ID = COR_TRN_CHALLAN.BATCH_HEADER_ID) " +
                       "LEFT JOIN COR_TRN_DEDUCTEE_DETAILS " +
                       "ON COR_TRN_CHALLAN.TRN_CHALLAN_ID = COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID) " +
                       "LEFT JOIN MST_SECTION " +
                       "ON COR_TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID) " +
                       "LEFT JOIN COR_HDR_DEDUCTEE_DETAILS " +
                       "ON COR_HDR_DEDUCTEE_DETAILS.HDR_DEDUCTEE_DETAIL_ID = COR_TRN_DEDUCTEE_DETAILS.HDR_DEDUCTEE_DETAIL_ID) " +
                       "WHERE COR_HDR_BATCH.BATCH_HEADER_ID = " + lngBatchId;

            //-----------------------------------------------------------
            strSQL = strQuery + " ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();

            dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgcViewRecords, strSQL, strMatrixViewDeductee);

            dgcViewRecords.ClearSelection();

        }

        #endregion

        #region CreateFilters
        public void CreateFilters()
        {
            try
            {
                strCheckFields = "";

                //----------------------------------------------------------------------
                //-- SECTION NO. SEARCH
                //----------------------------------------------------------------------
                if (cmbSection.SelectedIndex > 0)
                    strCheckFields = strCheckFields + " AND MST_SECTION.SECTION_ID = " + Convert.ToInt32(Support.GetItemData(cmbSection, cmbSection.SelectedIndex)) + " ";
                //----------------------------------------------------------------------

                //----------------------------------------------------------------------
                //-- DEDUCTEE PAN
                //----------------------------------------------------------------------
                if (txtDeducteePAN.Text.Trim() != "")
                    strCheckFields = strCheckFields + " AND COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN like '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text.Trim().ToUpper()) + "%' ";
                //----------------------------------------------------------------------

                //----------------------------------------------------------------------
                //-- DEDUCTEE NAME
                //----------------------------------------------------------------------
                if (txtDeducteeName.Text.Trim() != "")
                    strCheckFields = strCheckFields + " AND COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME like '%" + cmnService.J_ReplaceQuote(txtDeducteeName.Text.Trim().ToUpper()) + "%' ";
                //----------------------------------------------------------------------

                //----------------------------------------------------------------------
                //-- INVALID PAN
                //----------------------------------------------------------------------
                if (chkInvalidPAN.Checked == true)
                    strCheckFields = strCheckFields + " AND COR_HDR_DEDUCTEE_DETAILS.INVALID_PAN > 0 AND COR_TRN_DEDUCTEE_DETAILS.PAN_UPDATION_INDICATOR = 0 ";
                //else if (chkInvalidPAN.Checked == false)
                //    strCheckFields = strCheckFields + "AND COR_HDR_DEDUCTEE_DETAILS.INVALID_PAN = 0";
                //----------------------------------------------------------------------

                strSQL = strQuery + strCheckFields + " ORDER BY " + strOrderBy;
                //----------------------------------------------------------------------

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgcViewRecords, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //
                lblRecords.Text = "Deductee Records : " + Convert.ToString(dgcViewRecords.RowCount);                    
            }
            catch
            {

            }
        }

        #endregion

        

        #endregion

    }
}