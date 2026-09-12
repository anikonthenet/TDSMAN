#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Dhrub Mukherjee
Module Name		: TrnMonthlyTDSCalculatorSummary
Version			: 1.0
Start Date		: 20/01/2014
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
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnHRACalculator : Form
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnHRACalculator()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        //
        public TrnHRACalculator(string EmployeePAN, string EmployeeName, double Salary)
        {            
            strEmployeePAN = EmployeePAN;
            strEmployeeName = EmployeeName;
            dblSalary = Salary;
            //
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
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
        DataSet dsetGridHRACalculation = new DataSet();
        //RptDialog rptDialog = new RptDialog();
        //-----------------------------------------------------------------------
        string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        //-----------------------------------------------------------------------
        string strMetro = "N";
        //
        bool blExit = true;
        //
        string strEmployeePAN = "", strEmployeeName = "";
        double dblSalary = 0;
        #endregion

        #region set ENUM

        #region T_HRA_GRID_COLUMN

        public enum T_HRA_GRID_COLUMN
        {
            MONTH_ID = 0,
            MONTH_NAME = 1,
            BASIC_AMOUNT = 2,
            DA_AMOUNT = 3,
            COMMISSION_AMOUNT = 4,
            TOTAL_SALARY = 5,
            METRO_YN = 6,
            HRA_RECEIVED = 7,
            RENT_PAID = 8,
            ACTUAL_RENT = 9,
            HRA_METRO = 10,
            HRA_EXEMPTED = 11,
            HRA_TAXABLE = 12
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

        #region TrnMonthlyTDSCalculatorSummary_Load
        private void TrnMonthlyTDSCalculatorSummary_Load(object sender, EventArgs e)
        {
            //--
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //--
            GC.Collect();
            //--
            lblEmployeePANName.Text = strEmployeePAN + " - " + strEmployeeName;
            if (lblEmployeePANName.Text.Trim() != "")
                lblEmployeePANName.Visible = true;
            else
                lblEmployeePANName.Visible = false;
            txtBasicSalary.Text = string.Format("{0:0.00}", dblSalary);
            //Load and clear controls
            ClearControls();
            //Selecting Income textbox
            txtBasicSalary.Select();
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            //
            GC.Collect();
            this.Close();
            this.Dispose();
            //
        }
        #endregion


        #region txtBasicSalary_KeyPress
        private void txtBasicSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtBasicSalary, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDAFormingPartOfSalary_KeyPress
        private void txtDAFormingPartOfSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtDAFormingPartOfSalary, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtCommissionAmount_KeyPress
        private void txtCommissionAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtCommissionAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtHRAReceivedAmount_KeyPress
        private void txtHRAReceivedAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtHRAReceivedAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtRentPaidAmount_KeyPress
        private void txtRentPaidAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtRentPaidAmount, "") == false)
                e.Handled = true;
        }
        #endregion


        #region chkMetroCity_CheckedChanged
        private void chkMetroCity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMetroCity.Checked == true)
                strMetro = "Y";
            else
                strMetro = "N";

        }
        #endregion

        #region lnkTaxCalculationVisitSite_LinkClicked
        private void lnkTaxCalculationVisitSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connectivity not found");
                return;
            }
            //System.Diagnostics.Process.Start("http://law.incometaxindia.gov.in/DIT/Xtras/taxcalc.aspx");
            System.Diagnostics.Process.Start("https://www.incometaxindia.gov.in/Pages/tools/house-rent-allowance-calculator.aspx");
        }
        #endregion

        #region btnReset_Click
        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
            LoadHRAGrid();
        }
        #endregion

        #region btnCalculation_Click
        private void btnCalculation_Click(object sender, EventArgs e)
        {
            double dblActualRent = 0, dblHRAMetro = 0, dblHRAExempted = 0, dblHRATaxable = 0;
            //--
            CalculateHRA(Convert.ToDouble(txtBasicSalary.Text), 
                         Convert.ToDouble(txtDAFormingPartOfSalary.Text), 
                         Convert.ToDouble(txtCommissionAmount.Text), 
                         Convert.ToDouble(lblTotalSalary.Text), 
                         strMetro, 
                         Convert.ToDouble(txtHRAReceivedAmount.Text), 
                         Convert.ToDouble(txtRentPaidAmount.Text),
                         out dblActualRent,
                         out dblHRAMetro,
                         out dblHRAExempted,
                         out dblHRATaxable);
            lblActualRent.Text = string.Format("{0:0.00}", dblActualRent);
            lblMetroHRA.Text = string.Format("{0:0.00}", dblHRAMetro);
            lblExemptedHRA.Text = string.Format("{0:0.00}", dblHRAExempted);
            lblTaxableHRA.Text = string.Format("{0:0.00}", dblHRATaxable);
            //--
            LoadHRAGrid();
            //
            CalculateGridTotal();
        }
        #endregion

        #region dgvViewCalculation_CellClick
        private void dgvViewCalculation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //--
            #region COMMENT
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[0].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[1].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[2].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[3].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[4].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[5].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[6].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[7].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[8].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[9].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[10].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[11].Value.ToString());
            //cmnService.J_UserMessage(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[12].Value.ToString());
            #endregion
            //--
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)])
                    || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.DA_AMOUNT)])
                    || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.COMMISSION_AMOUNT)])
                    || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.HRA_RECEIVED)])
                    || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.RENT_PAID)])
                    || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.METRO_YN)]))
                {
                    dgvViewCalculation.Columns[Convert.ToInt32(e.ColumnIndex)].ReadOnly = false;
                    //--
                    DataGridViewCell cell = dgvViewCalculation[e.ColumnIndex, e.RowIndex];
                    dgvViewCalculation.CurrentCell = cell;
                    dgvViewCalculation.BeginEdit(true);
                }
                else if (dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.METRO_YN)]))
                {
                    //dgvViewCalculation.Columns[Convert.ToInt32(e.ColumnIndex)].ReadOnly = false;
                    ////--
                    ////DataGridViewComboBoxColumn ComboBox = new DataGridViewComboBoxColumn();// dgvViewCalculation[e.ColumnIndex, e.RowIndex];

                    ////ComboBox.HeaderText = "Select Data";
                    ////ComboBox.Name = "ComboBox";
                    ////ComboBox.MaxDropDownItems = 4;
                    ////ComboBox.Items.Add("Y");
                    ////ComboBox.Items.Add("N");
                    ////dgvViewCalculation.Columns.Add(ComboBox);

                    ////dgvViewCalculation.CurrentCell = ComboBox;
                    ////dgvViewCalculation.BeginEdit(true);
                    //DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
                    //dgvCmb.HeaderText = "Name";
                    //dgvCmb.Items.Add("Ghanashyam");
                    //dgvCmb.Items.Add("Jignesh");
                    //dgvCmb.Items.Add("Ishver");
                    //dgvCmb.Items.Add("Anand");
                    //dgvCmb.Name = "cmbName";
                    //dgvViewCalculation.Columns.Add(dgvCmb);
                    //dgvViewCalculation.BeginEdit(true);
                }
            }
        }
        #endregion

        #region dgvViewCalculation_CellLeave
        private void dgvViewCalculation_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)])
            //           || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.DA_AMOUNT)])
            //           || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.COMMISSION_AMOUNT)])
            //           || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.HRA_RECEIVED)])
            //           || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.RENT_PAID)]))
            //{
            //    if (dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value.ToString() == "")
            //        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value = "0.00";
            //    else
            //        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value = string.Format("{0:0.00}", Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value.ToString()));

            //}
        }
        #endregion

        #region dgvViewCalculation_CellValueChanged
        private void dgvViewCalculation_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (blExit == true) {
                if (dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)])
                       || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.DA_AMOUNT)])
                       || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.COMMISSION_AMOUNT)])
                       || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.HRA_RECEIVED)])
                       || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.RENT_PAID)])
                       || dgvViewCalculation.CurrentCell.ColumnIndex == dgvViewCalculation.Columns.IndexOf(dgvViewCalculation.Columns[Convert.ToInt32(T_HRA_GRID_COLUMN.METRO_YN)]))
                {
                    //--
                    if (dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value.ToString() == "")
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value = "0.00";
                    else
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value = string.Format("{0:0.00}", Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value.ToString()));
                    //
                    if (dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.DA_AMOUNT)].Value.ToString() == "")
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.DA_AMOUNT)].Value = "0.00";
                    else
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.DA_AMOUNT)].Value = string.Format("{0:0.00}", Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.DA_AMOUNT)].Value.ToString()));
                    //
                    if (dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.COMMISSION_AMOUNT)].Value.ToString() == "")
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.COMMISSION_AMOUNT)].Value = "0.00";
                    else
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.COMMISSION_AMOUNT)].Value = string.Format("{0:0.00}", Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.COMMISSION_AMOUNT)].Value.ToString()));
                    //
                    if (dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.HRA_RECEIVED)].Value.ToString() == "")
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.HRA_RECEIVED)].Value = "0.00";
                    else
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.HRA_RECEIVED)].Value = string.Format("{0:0.00}", Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.HRA_RECEIVED)].Value.ToString()));
                    //
                    if (dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.RENT_PAID)].Value.ToString() == "")
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.RENT_PAID)].Value = "0.00";
                    else
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.RENT_PAID)].Value = string.Format("{0:0.00}", Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.RENT_PAID)].Value.ToString()));
                    //
                    if (dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.METRO_YN)].Value.ToString().ToUpper() == "Y")
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.METRO_YN)].Value = "Y";
                    else if (dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.METRO_YN)].Value.ToString().ToUpper() == "N")
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.METRO_YN)].Value = "N";
                    else if (dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.METRO_YN)].Value.ToString().Trim() == "")
                    {
                        cmnService.J_UserMessage("Only Y/N permitted...");
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.METRO_YN)].Value = "N";
                        blExit = true;
                        //dgvViewCalculation.CurrentCell.Selected = true;

                        return;
                    }
                    else
                    {
                        cmnService.J_UserMessage("Only Y/N permitted...");
                        dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.METRO_YN)].Value = "N";
                        blExit = true;
                        return;
                    }
                    //
                    //--
                    double dblActualRent = 0, dblHRAMetro = 0, dblHRAExempted = 0, dblHRATaxable = 0;
                    //--
                    CalculateHRA(Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value.ToString()),
                                 Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.DA_AMOUNT)].Value.ToString()),
                                 Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.COMMISSION_AMOUNT)].Value.ToString()),
                                 Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value.ToString()) +
                                 Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.DA_AMOUNT)].Value.ToString()) +
                                 Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.COMMISSION_AMOUNT)].Value.ToString()),
                                 Convert.ToString(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.METRO_YN)].Value.ToString()),
                                 Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.HRA_RECEIVED)].Value.ToString()),
                                 Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.RENT_PAID)].Value.ToString()),
                                 out dblActualRent,
                                 out dblHRAMetro,
                                 out dblHRAExempted,
                                 out dblHRATaxable);
                    blExit = false;
                    dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.ACTUAL_RENT)].Value = string.Format("{0:0.00}", dblActualRent);
                    dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.HRA_METRO)].Value = string.Format("{0:0.00}", dblHRAMetro);
                    dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.HRA_EXEMPTED)].Value = string.Format("{0:0.00}", dblHRAExempted);
                    dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.HRA_TAXABLE)].Value = string.Format("{0:0.00}", dblHRATaxable);
                    dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.TOTAL_SALARY)].Value = string.Format("{0:0.00}", Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value.ToString()) +
                                                                                                                                                                          Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.DA_AMOUNT)].Value.ToString()) +
                                                                                                                                                                          Convert.ToDouble(dgvViewCalculation.Rows[dgvViewCalculation.CurrentRow.Index].Cells[Convert.ToInt32(T_HRA_GRID_COLUMN.COMMISSION_AMOUNT)].Value.ToString()));
                    CalculateGridTotal();
                    blExit = true;
                }
            }
        }
        #endregion


        #region btnPrintCalculation_Click
        private void btnPrintCalculation_Click(object sender, EventArgs e)
        {

            //-- Added By Abhishek Dey On 04/04/2019 --     
            // Veriable Declaretion
            #region Veriable Declaration
            string strTxtExcelPath = string.Empty;
            string strExcelFilePath = string.Empty;
            string strExcelFileName = "EXPORTED_HRA_DATA.XLSX";
            string strWorkSheet = "HRA CALCULATION";
            DataTable dtExport = new System.Data.DataTable();
            #endregion

            if (strEmployeePAN != "")
                strExcelFileName = strEmployeePAN + "_EXPORTED_HRA_DATA.XLSX";
            // Create a new instance of FolderBrowserDialog.
            #region FolderBrowserDialog
            FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
            // A new folder button will display in FolderBrowserDialog.
            folderBrowserDlg.ShowNewFolderButton = true;
            //Show FolderBrowserDialog
            DialogResult dlgResult = folderBrowserDlg.ShowDialog();
            if (dlgResult.Equals(DialogResult.OK))
            {
                //Show selected folder path in textbox1.
                strTxtExcelPath = folderBrowserDlg.SelectedPath;
                //Browsing start from root folder.
                Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
            }
            #endregion

            // 
            #region EXCEL EXPORT
            //-- VALIDATION
            if (string.IsNullOrEmpty(strTxtExcelPath.Trim()))
            {
                cmnService.J_UserMessage("Please select specific folder for Export");
                this.Cursor = Cursors.Default;
                return;
            }

            strExcelFilePath = strTxtExcelPath + "\\" + strExcelFileName;

            // CHECK IF SAME NAME FILE EXIST
            if (File.Exists(strExcelFilePath))
            {
                if (cmnService.J_UserMessage("File exists with same name. Do you want to replace?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
                else
                {
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
            //
            this.Cursor = Cursors.WaitCursor;
            // CREATE EXCEL FILE
            if (CREATE_EXCEL_FILE(strExcelFilePath) == false)
            {
                cmnService.J_UserMessage("Some error occurred");
                this.Cursor = Cursors.Default;
                return;
            }

            // CREATE NEW WORKAHEET
            if (CREATE_NEW_WORKSHEET(strExcelFilePath, strWorkSheet) == false) return;
            //dtExportSummary.Columns.AddRange(new DataColumn[7] { new DataColumn("[SECTION NO]"), new DataColumn("[DESCRIPTION]"), new DataColumn("[TOTAL AMOUNT]"), new DataColumn("[TOTAL AMOUNT WITHOUT 'NO DEDUCTION']"), new DataColumn("[TOTAL TDS]"), new DataColumn("[TOTAL AMOUNT DEDUCTED AT LOWER RATE]"), new DataColumn("[TDS DEDUCTED AT LOWER RATE]") });

            DataTable dtGridSource = (DataTable)dgvViewCalculation.DataSource;
            // MODIFICATION OF DATATABLE
            dtGridSource.Columns.Remove("MONTH_ID");
            dtGridSource.Columns.Remove("TOTAL_SALARY");
            dtGridSource.Columns.Remove("ACTUAL_RENT");
            dtGridSource.Columns.Remove("HRA_METRO");

            // RECONSTRUCT DATATBLE
            dtExport.Columns.AddRange(new DataColumn[9] { new DataColumn("[MONTH]"), new DataColumn("[BASIC]"), new DataColumn("[DA]"), new DataColumn("[COMMISSION]"), new DataColumn("[METRO(Y/N)]"), new DataColumn("[HRA]"), new DataColumn("[RENT]"), new DataColumn("[EXEMPTED HRA]"), new DataColumn("[TAXABLE HRA]") });

            foreach (DataRow dr in dtGridSource.Rows)
            {
                dtExport.Rows.Add(dr.ItemArray);
            }

            dtExport.Rows.Add("", txtTotalBasic.Text.Trim(), txtTotalDA.Text.Trim(), txtTotalCommission.Text.Trim(), "", "", "", "", "");
            dtExport.Rows.Add("TOTAL", "", "", txtTotalSalary.Text.Trim(), "", txtTotalHRAReceived.Text.Trim(), txtTotalRentPaid.Text.Trim(), txtTotalHRAExempted.Text.Trim(), txtTotalHRATaxable.Text.Trim());


            // EXCEL EXPORT
            if (ExportToExcelFromDataTable(dtExport, strWorkSheet, strExcelFilePath) == false)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("Failed export data to excel ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            System.Diagnostics.Process.Start(strExcelFilePath);
            this.Cursor = Cursors.Default;
            #endregion
            //--------------------------------------------------

        }
        #endregion

        #endregion

        #region User Defined Functions

        #region NumericCurrencyControl_Leave
        private void NumericCurrencyControl_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "." || txtBox.Text == "") txtBox.Text = "0.00";
            txtBox.Text = string.Format("{0:0.00}", Convert.ToDouble(cmnService.J_NumericData(txtBox)));
        }
        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            txtBasicSalary.Text ="0.00";
            txtDAFormingPartOfSalary.Text ="0.00";
            txtHRAReceivedAmount.Text = "0.00";
            txtCommissionAmount.Text = "0.00";
            txtRentPaidAmount.Text = "0.00";
            chkMetroCity.Checked = false;
            lblActualRent.Text = "0.00";
            lblMetroHRA.Text = "0.00";
            lblExemptedHRA.Text = "0.00";
            lblTaxableHRA.Text = "0.00";
            //
            txtTotalBasic.Text = "0.00";
            txtTotalDA.Text = "0.00";
            txtTotalCommission.Text = "0.00";
            txtTotalSalary.Text = "0.00";
            txtTotalHRAReceived.Text = "0.00";
            txtTotalRentPaid.Text = "0.00";
            txtTotalHRAExempted.Text = "0.00";
            txtTotalHRATaxable.Text = "0.00";
        }
        #endregion

        #region CalcTotalSalary
        private void CalcTotalSalary(object sender, EventArgs e)
        {
            if (txtBasicSalary.Text == "") txtBasicSalary.Text = "0.00";
            if (txtDAFormingPartOfSalary.Text == "") txtDAFormingPartOfSalary.Text = "0.00";
            if (txtCommissionAmount.Text == "") txtCommissionAmount.Text = "0.00";
            //-- TOTAL SALARY
            lblTotalSalary.Text = string.Format("{0:0.00}", Convert.ToDouble(txtBasicSalary.Text) + Convert.ToDouble(txtDAFormingPartOfSalary.Text) + Convert.ToDouble(txtCommissionAmount.Text));
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                //TAXABLE AMOUNT -DEDUCTION
                if ((cmnService.J_ReturnDoubleValue(txtBasicSalary.Text)<= 0) && (cmnService.J_ReturnDoubleValue(txtDAFormingPartOfSalary.Text)<= 0))
                {
                    grpTaxCalculation.Visible = false;
                    return false;
                }

                return true;

            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
        }
        #endregion 

        #region LoadHRAGrid
        private void LoadHRAGrid()
        {
            #region COMMENT
            //grpTaxCalculation.Visible = true;
            //if (cmnService.J_ReturnDoubleValue(txtBasicSalary.Text) < cmnService.J_ReturnDoubleValue(txtDAFormingPartOfSalary.Text))
            //{
            //    lblTotTaxableAmt.Text = lblLessTaxCreditAmt.Text = lblITaxOnTaxableAmt.Text = lblSurchaargeAmt.Text = lblEducationCessAmt.Text = lblTotTaxAmt.Text = lblMonthlyTDSAmt.Text = "0.00";
            //    return;
            //}
            //if (dblTaxableAmount > 0)
            //{
            //    //INITIALIZATION 
            //    strCategory = cmbEmployeeCategory.Text.Substring(0, 1);
            //    intAsstId = Convert.ToInt32(cmnService.J_GetComboBoxItemId(ref cmbFinancialYear, cmbFinancialYear.SelectedIndex));

            //    //-- upto fayear 10-11 -> SuperSeniorCitizen will be SeniorCitizen
            //    if (intAsstId <= T_FinancialYearID.F2010_11ID && cmbEmployeeCategory.Text == T_EmployeeCategory.SuperSeniorCitizen)
            //        strCategory = T_EmployeeCategory.SeniorCitizen;


            //    //TAX CALCULATION
            //    dblCalculatedTax = TdsMan.CalculateIncomeTaxAmount(intAsstId, strCategory, dblTaxableAmount, out dblCalculatedECess, out dblCalculatedSurcharge, out dblTaxCredit);

            //    if (dblCalculatedTax > 0)
            //    {
            //        lblLessTaxCreditAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblTaxCredit));
            //        lblITaxOnTaxableAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblCalculatedTax));
            //        lblEducationCessAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblCalculatedECess));
            //        lblSurchaargeAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblCalculatedSurcharge));
            //        lblTotTaxAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblCalculatedTax + dblCalculatedECess + dblCalculatedSurcharge));
            //        lblMonthlyTDSAmt.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDouble((dblCalculatedTax + dblCalculatedECess + dblCalculatedSurcharge) / 12), 0));

            //    }
            //    else
            //    {
            //        lblLessTaxCreditAmt.Text = "0.00";
            //        lblITaxOnTaxableAmt.Text = "0.00";
            //        lblSurchaargeAmt.Text = "0.00";
            //        lblEducationCessAmt.Text = "0.00";
            //        lblTotTaxAmt.Text = "0.00";
            //        lblMonthlyTDSAmt.Text = "0.00";
            //    }


            //    //dblCalculateTaxCreditAmt = 0;
            //    //if (intAsstId == T_FinancialYearID.F2013_14ID)
            //    ////if (Category == "G" && AsstId == 9 && Convert.ToDouble(ds.Tables[0].Rows[i - 1]["INCOME_FROM"]) == 200000 && Convert.ToDouble(ds.Tables[0].Rows[i - 1]["INCOME_TO"]) == 500000)
            //    //{
            //    //    if ((dblTaxableAmount >= 200000 && dblTaxableAmount <= 500000) && (strCategory == "G" || strCategory == "W"))
            //    //    {
            //    //        //------------
            //    //        //Calculating Tax Credit
            //    //        dblCalculateTaxCreditAmt = (dblTaxableAmount * 10.00) / 100;
            //    //        //------------
            //    //        //Checking the threshold limit and assigning value
            //    //        if (dblCalculateTaxCreditAmt > 2000)
            //    //            dblCalculateTaxCreditAmt = 2000;
            //    //    }
            //    //}
            //    //if (dblCalculateTaxCreditAmt>0)
            //    //    lblLessTaxCreditAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblCalculateTaxCreditAmt));
            //    //else
            //    //    lblLessTaxCreditAmt.Text = "0.00";
            //}
            #endregion
            //--
            //string strMetro = "N";
            //if (chkMetroCity.Checked == true)
            //    strMetro = "Y";
            string strUpper = "", strLower = "";
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                strUpper = "UPPER";
                strLower = "LOWER";
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                strUpper = "UCASE";
                strLower = "LCASE";
            }
            //--
            string[,] strMatrixGrid = {{"MONTH_ID", "0", "", "", "", "", ""},
                                    {"Month", "85", "", "", "", "", "T"},
                                    {"Basic", "100", "0.00", "R", "", "", "T"},
                                    {"DA", "75", "0.00", "R", "", "", "T"},
                                    {"Commission", "75", "0.00", "R", "", "", "T"},
                                    {"Total Salary", "0", "", "", "", "", ""},
                                    {"Metro(Y/N)", "75", "", "", "", "", "T"},
                                    {"HRA", "95", "0.00", "R", "", "", "T"},
                                    {"Rent", "80", "0.00", "R", "", "", "T"},
                                    {"Actual Rent", "0", "", "", "", "", ""},
                                    {"HRA based on Metro", "0", "", "", "", "", ""},
                                    {"Exempted HRA", "100", "0.00", "R", "", "", "T"},
                                    {"Taxable HRA", "100", "0.00", "R", "", "", "T"}};

            //string[,] strMatrixGrid = {{"MONTH_ID", "0", "", "R", "", "", ""},
            //                        {"Month", "85", "", "", "", "", ""},
            //                        {"Basic", "100", "", "R", "", "", ""},
            //                        {"DA", "75", "", "R", "", "", ""},
            //                        {"Commission", "75", "", "R", "", "", ""},
            //                        {"Total Salary", "0", "", "", "", "", ""},
            //                        {"Metro(Y/N)", "75", "", "C", "", "", ""},
            //                        {"HRA", "95", "", "R", "", "", ""},
            //                        {"Rent", "80", "", "R", "", "", ""},
            //                        {"Actual Rent", "0", "", "", "", "", ""},
            //                        {"HRA based on Metro", "0", "", "", "", "", ""},
            //                        {"Exempted HRA", "100", "", "R", "", "", ""},
            //                        {"Taxable HRA", "100", "", "R", "", "", ""}};
            //--
            //UPPER(LEFT(MONTH_DESC, 1)) + LOWER(RIGHT(MONTH_DESC, LEN(MONTH_DESC) - 1)) AS MONTH_DESC
            //--
            strSQL = "SELECT MONTH_ID                               AS MONTH_ID," +
                "            " + strUpper + "(LEFT(MONTH_DESC, 1)) + " + strLower + "(RIGHT(MONTH_DESC, LEN(MONTH_DESC) - 1)) AS MONTH_DESC, " +
                "           '" + txtBasicSalary.Text + "'           AS BASIC, " +
                "           '" + txtDAFormingPartOfSalary.Text + "' AS DA, " +
                "           '" + txtCommissionAmount.Text + "'      AS COMMISSION, " +
                "           '" + lblTotalSalary.Text + "'           AS TOTAL_SALARY, " +
                "           '" + strMetro + "'                      AS METRO, " +
                "           '" + txtHRAReceivedAmount.Text + "'     AS HRA_RECEIVED, " +
                "           '" + txtRentPaidAmount.Text + "'        AS RENT_PAID, " +
                "           '" + lblActualRent.Text + "'            AS ACTUAL_RENT, " +
                "           '" + lblMetroHRA.Text + "'              AS HRA_METRO, " +
                "           '" + lblExemptedHRA.Text + "'           AS HRA_EXEMPTED, " +
                "           '" + lblTaxableHRA.Text + "'            AS HRA_TAXABLE " +
                "     FROM   MST_MONTH " +
                "     ORDER BY MONTH_ORDER";
            //--
            if (dsetGridHRACalculation != null) dsetGridHRACalculation.Clear();
            dsetGridHRACalculation = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvViewCalculation, strSQL, strMatrixGrid);
            dgvViewCalculation.ClearSelection();
            //--
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=GkYeCj-ompQ&t=2s");
        }

        #endregion

        #region CalculateHRA
        private void CalculateHRA(double dblBasic,
                                  double dblDA,
                                  double dblCommission,
                                  double dblTotalSalary,
                                  string strMetroYN, // Y or N 
                                  double dblHRAReceived, 
                                  double dblRentPaid, 
                                  out double dblActualRent, 
                                  out double dblHRAMetro, 
                                  out double dblHRAExempted, 
                                  out double dblHRATaxable )
        {
            //
            dblActualRent = 0; dblHRAMetro = 0; dblHRAExempted = 0; dblHRATaxable = 0;
            //-- ACTUAL RENT  10 % OF BASIC
            if (dblRentPaid - (dblTotalSalary * 0.1) < 0)
                dblActualRent = 0;
            else
                dblActualRent = dblRentPaid - (dblTotalSalary * 0.1);
            //
            if (dblActualRent < 0) dblActualRent = 0;
            //--
            //40 % OR 50 % OF BASIC BASED ON METRO
            if (strMetroYN.ToUpper() == "Y")
            {
                dblHRAMetro = (dblTotalSalary * 0.5);
            }
            else
            {
                dblHRAMetro = (dblTotalSalary * 0.4);
            }
            //-- LOWEST OF THREE
            //////if (dblHRAReceived < dblRentPaid && dblHRAReceived < dblHRAMetro)
            //////    dblHRAExempted = dblHRAReceived;
            //////else if (dblActualRent < dblHRAReceived && dblActualRent < dblHRAMetro)
            //////    dblHRAExempted = dblActualRent;
            //////else
            //////    dblHRAExempted = dblHRAMetro;
            //////dblRentPaid - dblActualRent - dblHRAMetro
            if (dblActualRent < dblRentPaid && dblActualRent < dblHRAMetro)
                dblHRAExempted = dblActualRent;
            else if (dblRentPaid < dblActualRent && dblRentPaid < dblHRAMetro)
                dblHRAExempted = dblRentPaid;
            else
                dblHRAExempted = dblHRAMetro;
            //-- TAXABLE HRA
            if (dblHRAReceived > 0)
            {
                dblHRATaxable = dblHRAReceived - dblHRAExempted;
                //-- 2023/10/19
                if (dblHRATaxable < 0)
                    dblHRATaxable = 0;
            }
        }
        
        #endregion

        #region CalculateGridTotal
        private void CalculateGridTotal()
        {
            double dblTotalBasic = 0, dblTotalDA = 0, dblTotalCommission = 0, dblTotalSalary = 0, dblTotalHRAReceived = 0, dblTotalRentPaid = 0, dblTotalHRAExempted = 0, dblTotalHRATaxable = 0;
            //--
            txtTotalBasic.Text       = "0.00";
            txtTotalDA.Text          = "0.00";
            txtTotalCommission.Text  = "0.00";
            txtTotalSalary.Text      = "0.00";
            txtTotalHRAReceived.Text = "0.00";
            txtTotalRentPaid.Text    = "0.00";
            txtTotalHRAExempted.Text = "0.00";
            txtTotalHRATaxable.Text  = "0.00";
            //--
            for (int i = 0; i <= dgvViewCalculation.RowCount - 1; i++)
            {
                //Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value);
                dblTotalBasic       = dblTotalBasic       + Convert.ToDouble(Convert.ToString(dgvViewCalculation.Rows[i].Cells[Convert.ToInt16(T_HRA_GRID_COLUMN.BASIC_AMOUNT)].Value));
                dblTotalDA          = dblTotalDA          + Convert.ToDouble(Convert.ToString(dgvViewCalculation.Rows[i].Cells[Convert.ToInt16(T_HRA_GRID_COLUMN.DA_AMOUNT)].Value));
                dblTotalCommission  = dblTotalCommission  + Convert.ToDouble(Convert.ToString(dgvViewCalculation.Rows[i].Cells[Convert.ToInt16(T_HRA_GRID_COLUMN.COMMISSION_AMOUNT)].Value));
                dblTotalSalary      = dblTotalSalary      + Convert.ToDouble(Convert.ToString(dgvViewCalculation.Rows[i].Cells[Convert.ToInt16(T_HRA_GRID_COLUMN.TOTAL_SALARY)].Value));
                dblTotalHRAReceived = dblTotalHRAReceived + Convert.ToDouble(Convert.ToString(dgvViewCalculation.Rows[i].Cells[Convert.ToInt16(T_HRA_GRID_COLUMN.HRA_RECEIVED)].Value));
                dblTotalRentPaid    = dblTotalRentPaid    + Convert.ToDouble(Convert.ToString(dgvViewCalculation.Rows[i].Cells[Convert.ToInt16(T_HRA_GRID_COLUMN.RENT_PAID)].Value));
                dblTotalHRAExempted = dblTotalHRAExempted + Convert.ToDouble(Convert.ToString(dgvViewCalculation.Rows[i].Cells[Convert.ToInt16(T_HRA_GRID_COLUMN.HRA_EXEMPTED)].Value));
                dblTotalHRATaxable  = dblTotalHRATaxable  + Convert.ToDouble(Convert.ToString(dgvViewCalculation.Rows[i].Cells[Convert.ToInt16(T_HRA_GRID_COLUMN.HRA_TAXABLE)].Value));
            }
            //
            txtTotalBasic.Text       = string.Format("{0:0.00}", dblTotalBasic);
            txtTotalDA.Text          = string.Format("{0:0.00}", dblTotalDA);
            txtTotalCommission.Text  = string.Format("{0:0.00}", dblTotalCommission);
            txtTotalSalary.Text      = string.Format("{0:0.00}", dblTotalSalary);
            txtTotalHRAReceived.Text = string.Format("{0:0.00}", dblTotalHRAReceived);
            txtTotalRentPaid.Text    = string.Format("{0:0.00}", dblTotalRentPaid);
            txtTotalHRAExempted.Text = string.Format("{0:0.00}", dblTotalHRAExempted);
            txtTotalHRATaxable.Text  = string.Format("{0:0.00}", dblTotalHRATaxable);
        }
        #endregion

        //-- Added By Abhishek dey On 04/04/2019 --
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
        private bool ExportToExcelFromDataTable(DataTable myDataTable, string SheetName, string strExcelFilePath)
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
                int rowIndex = 4;

                // WRITE MESSAGE
                if (lblEmployeePANName.Text.Trim().Length > 1)
                {
                    wsnew.get_Range("A2", "I2").MergeCells = true;
                    wsnew.get_Range("A2", m).Value2 = lblEmployeePANName.Text.Trim();
                    wsnew.get_Range("A2", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Aqua);
                    wsnew.get_Range("A2", m).Font.Size = 11;
                }

                foreach (DataColumn dc in myDataTable.Columns)
                {
                    colIndex++;
                    wsnew.Cells[4, colIndex] = dc.ColumnName;
                }
                //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 -
                foreach (DataRow dr in myDataTable.Rows)
                {
                    rowIndex++;
                    colIndex = 0;

                    foreach (DataColumn dc in myDataTable.Columns)
                    {
                        colIndex++;
                        wsnew.Cells[rowIndex, colIndex] = "'" + dr[dc.ColumnName];
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
        //--------------------------------------------------



        #endregion


    }
}