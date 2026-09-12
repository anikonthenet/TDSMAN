#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: MstDeductee
Version			: 1.0
Start Date		: 18-11-2010
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
using System.Data.SqlClient;

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion


namespace TDSMAN.FormMst
{
    public partial class MstTdsRateChartFAyear : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public MstTdsRateChartFAyear()
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

        //-----------------------------------------------------------------------
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

        #region MstTdsRateChartFAyear_Load

        private void MstTdsRateChartFAyear_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            try
            {
                //-----------------------------------------------------------
                GC.Collect();
                //
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                //-----------------------------------------------------------
                BtnAdd.Visible = false;               
                BtnDelete.Visible = false;                
                BtnEdit.Visible = false;
                BtnCancel.Visible = false; 
                BtnPrint.Visible = false;
                BtnRefresh.Visible = false;
                BtnSave.Visible = false;
                BtnSearch.Visible = false;
                BtnSearchCancel.Visible = false;
                BtnSearchOK.Visible = false;
                BtnSort.Visible = false;
                BtnSortOK.Visible = false;
                //
                grpFilter.Visible = true;
                //dgvGrid.Visible = true;
                //------------------------------------------------------------
                //                // FROM DATE
                //                strSQL = "SELECT " + cmnService.J_SQLDBFormat("START_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS START_DATE " +
                //                                "FROM   MST_ASSESSMENT " +
                //                                "WHERE  ASST_ID = (SELECT MAX(ASST_ID) FROM MST_ASSESSMENT)";
                //                mskFromDate.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //                //mskFromDate.Text = J_ReturnServerDate();
                //                //mskToDate.Text = J_ReturnServerDate(); //-- Commented On 11/04/2018 --
                //                //-----------------------------------------------------------
                //                // TO DATE
                //                //----------- Added On 11/04/2018 ---------------------------
                //                strSQL = "SELECT " + cmnService.J_SQLDBFormat("END_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS END_DATE " +
                //                         "FROM   MST_ASSESSMENT " +
                //                         "WHERE  ASST_ID = (SELECT MAX(ASST_ID) FROM MST_ASSESSMENT)";
                //                mskToDate.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));                
                //                //
                //                if (dtService.J_ConvertToIntYYYYMMDD(mskToDate.Text) < dtService.J_ConvertToIntYYYYMMDD(mskFromDate.Text))
                //                    mskToDate.Text = mskFromDate.Text;
                //                //-----------------------------------------------------------
                //                strSQL = @" SELECT FA_YEAR
                //                            FROM MST_ASSESSMENT
                //                            WHERE   START_DATE <= " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " " + @"
                //                            AND END_DATE >= " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " ";
                //                lblFAYear.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //                //-----------------------------------------------------------
                //BtnDelete.Enabled = false;
                //BtnDelete.BackColor = Color.LightGray;

                //-----------------------------------------------------------
                ////-----------
                ////-- SECTION
                ////-----------
                //strSQL = " SELECT SECTION_ID," +
                //    "             SECTION_NO  + ' - ' + SECTION_DESCRIPTION + ' - ' + FORM_NAME AS SECTION_NO " +
                //    "      FROM   MST_SECTION " +
                //    "      WHERE  SECTION_ID > 0 " +
                //    "      AND    FORM_NAME <> '24Q' " +
                //    "      ORDER BY FORM_NAME, " +
                //    "             SECTION_NO ";
                //    ////////"      WHERE  FORM_NAME = '" + strFormNo + "' " +
                //    ////////"      AND    ASST_ID  <= " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + " " +
                //    ////////"      ORDER BY SECTION_ID";
                    
                //if (dmlService.J_PopulateComboBox(strSQL, ref cmbSection) == false) return;
                //-----------
                //-- FA YEAR
                //-----------
                strSQL = " SELECT ASST_ID," +
                         "        FA_YEAR  " +
                         " FROM   MST_ASSESSMENT " +
                         " WHERE  VISIBILITY_FLAG = 0 " +
                         " AND    ASST_ID > 4 " +
                         " ORDER BY ASST_ID DESC ";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbFAYear) == false) return;
                //--
                cmbFAYear.SelectedIndex = 1;
                //-----------------------------------------------------------
                ControlVisible(false);
                ClearControls();
                //-----------------------------------------------------------
                lblTitle.Text = "TDS Rate Chart";
                //-----------------------------------------------------------
                ShowDataInGridBlank();

                //dgvGrid.Visible = true;
                //-----------------------------------------------------------
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }

        #endregion


        #region cmbSection_KeyPress
        private void cmbSection_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbSection_Leave
        private void cmbSection_Leave(object sender, EventArgs e)
        {
            //ShowDataInGrid();
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

        #region lnkClick_LinkClicked
        private void lnkClick_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.tdsman.com/tds-rate-chart.asp");  
        }
        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion       
        
        #endregion

        #region User Define Functions

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
           
        }
        #endregion       

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                //----------------------------------------------------------
                //-- SECTION
                //----------------------------------------------------------
                if (cmbSection.SelectedIndex == 0)
                {
                    cmnService.J_UserMessage("Please select Section");
                    cmbSection.Select();
                    return false;
                }
                //
                if (cmbFAYear.SelectedIndex == 0)
                {
                    cmnService.J_UserMessage("Please select FA Year");
                    cmbFAYear.Select();
                    return false;
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

        #region ShowDataInGridBlank
        private void ShowDataInGridBlank()
        {
            try
            {
                string strQuery1 = string.Empty;
                string[,] strMatrix = {{"NONSALARY_TAX_SLAB_ID", "0", "", "", "", "F", ""},
                                    {"Company", "300", "", "", "", "", "T"},
                                    {"Non company", "300", "", "", "", "", "T"},
                                    {"Invalid PAN", "300", "", "", "", "", "T"}};
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //strOrderBy = "NONSALARY_TAX_SLAB_ID";
                strSQL = @" SELECT NONSALARY_TAX_SLAB_ID,
                                     COMPANY_RATE,
                                     NON_COMPANY_RATE,
                                     INVALID_PAN_RATE 
                               FROM  MST_NONSALARY_TAX_SLAB,
                                     MST_ASSESSMENT 
                               WHERE 1=2";
                //if (cmbSection.SelectedIndex > 0)
                //{
                //    strQuery += " WHERE SECTION_ID = " + Convert.ToInt32(Support.GetItemData(cmbSection, cmbSection.SelectedIndex)) + " ";
                //    strQuery1 = " AND";
                //}
                //else
                //    strQuery1 = " WHERE ";
                ////--
                //if (cmbFAYear.SelectedIndex > 0)
                //{
                //    strQuery += strQuery1;
                //    strQuery += " MST_ASSESSMENT.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)) + " ";
                //}
                //strQuery += @" AND MST_NONSALARY_TAX_SLAB.FROM_DATE <= MST_ASSESSMENT.END_DATE
                //               AND MST_NONSALARY_TAX_SLAB.TO_DATE >= MST_ASSESSMENT.START_DATE ";
                //                if (!string.IsNullOrEmpty(mskFromDate.Text.Trim()) && !string.IsNullOrEmpty(mskToDate.Text.Trim()))
                //                {
                //                    strQuery += strQuery1;
                //                    strQuery += "       FROM_DATE <=  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + " " + @"
                //                                  AND   TO_DATE   >= " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " ";
                //                                  //AND   YEAR(FROM_DATE) = YEAR( " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " ) ";
                //                }
                //-----------------------------------------------------------
                //strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);  //Show Data into the Grid
                if (dsetGridClone == null) return;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region ShowDataInGrid
        private void ShowDataInGrid()
        {
            try
            {
                string strQuery1 = string.Empty;
                //string[,] strMatrix = {{"CompID", "0", "", "", "", "F", ""},
                //                        {"Deductor/ Employer/ Collector Name", "250", "S", "", "", "", "T"},
                //                        {"TAN No.", "80", "", "", "", "", "T"},
                //                        {"PAN No.", "80", "", "", "", "", "T"},
                //                        {"Deductor Type", "150", "", "", "", "", "T"},
                //                        {"Responsible Person", "180", "", "", "", "", "T"},
                //                        {"GSTN", "100", "", "", "", "", "T"},
                //                        {"Annex III", "60", "", "", "", "", "T"},
                //                        {"Block ", "40", "", "", "", "", "T"}};

                //string[,] strMatrix = {{"NONSALARY_TAX_SLAB_ID", "0", "", "", "", "F", ""},
                //  {"Company", "80", "", "", "", "", "T"},
                //  {"Non company", "80", "", "", "", "", "T"},
                //  {"Invalid PAN", "80", "", "", "", "", "T"}};

                string[,] strMatrix = {{"NONSALARY_TAX_SLAB_ID", "0", "", "", "", "F", ""},
                                    {"Company", "300", "", "", "", "", "T"},
                                    {"Non company", "300", "", "", "", "", "T"},
                                    {"Invalid PAN", "300", "", "", "", "", "T"}};
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                strOrderBy = "NONSALARY_TAX_SLAB_ID";
                strQuery = @" SELECT NONSALARY_TAX_SLAB_ID,
                                     COMPANY_RATE,
                                     NON_COMPANY_RATE,
                                     INVALID_PAN_RATE 
                               FROM  MST_NONSALARY_TAX_SLAB,
                                     MST_ASSESSMENT ";
                if (cmbSection.SelectedIndex > 0)
                {
                    strQuery += " WHERE SECTION_ID = " + Convert.ToInt32(Support.GetItemData(cmbSection, cmbSection.SelectedIndex)) + " ";
                    strQuery1 = " AND";
                }
                else
                    strQuery1 = " WHERE ";
                //--
                if (cmbFAYear.SelectedIndex > 0)
                {
                    strQuery += strQuery1;
                    strQuery += " MST_ASSESSMENT.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)) + " ";
                }
                strQuery += @" AND MST_NONSALARY_TAX_SLAB.FROM_DATE <= MST_ASSESSMENT.END_DATE
                               AND MST_NONSALARY_TAX_SLAB.TO_DATE >= MST_ASSESSMENT.START_DATE "; 
//                if (!string.IsNullOrEmpty(mskFromDate.Text.Trim()) && !string.IsNullOrEmpty(mskToDate.Text.Trim()))
//                {
//                    strQuery += strQuery1;
//                    strQuery += "       FROM_DATE <=  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + " " + @"
//                                  AND   TO_DATE   >= " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " ";
//                                  //AND   YEAR(FROM_DATE) = YEAR( " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " ) ";
//                }
                //-----------------------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);  //Show Data into the Grid
                if (dsetGridClone == null) return;
                //--
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region btnGo_Click
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (ValidateFields() == true)
            {
                ShowDataInGrid();
            }
        }


        #endregion

        #endregion

        private void cmbFAYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFAYear.SelectedIndex <= 0)
            {
                ShowDataInGridBlank();
                cmbSection.Items.Clear();
            }
            //
            //-----------
            //-- SECTION
            //-----------
            strSQL = " SELECT SECTION_ID," +
                "             SECTION_NO  + ' - ' + SECTION_DESCRIPTION + ' - ' + FORM_NAME AS SECTION_NO " +
                "      FROM   MST_SECTION " +
                "      WHERE  SECTION_ID > 0 " +
                "      AND    FORM_NAME <> '24Q' " +
                //"      AND    ASST_ID <= " + Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)) + " " +
                "      AND ASST_ID <= " + Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)) + " " +
                "      AND (VALID_UPTO_ASST_ID >= " + Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)) + " OR VALID_UPTO_ASST_ID = 0) " +
                "      ORDER BY SECTION_NO ";
            ////////"      WHERE  FORM_NAME = '" + strFormNo + "' " +
            ////////"      AND    ASST_ID  <= " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + " " +
            ////////"      ORDER BY SECTION_ID";

            if (dmlService.J_PopulateComboBox(strSQL, ref cmbSection) == false) return;
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSection.SelectedIndex <= 0)
                ShowDataInGridBlank();
        }

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0105", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}

