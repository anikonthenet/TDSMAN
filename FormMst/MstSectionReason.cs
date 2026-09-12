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
    public partial class MstSectionReason : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public MstSectionReason()
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
                //--
                string[] strFormNo = { T_FormNo.F138_24Q, T_FormNo.F140_26Q, T_FormNo.F143_27EQ, T_FormNo.F144_27Q};
                dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);
                //-----------
                //-- FA YEAR
                //-----------
                //strSQL = " SELECT ASST_ID," +
                //         "        FA_YEAR  " +
                //         " FROM   MST_ASSESSMENT " +
                //         " WHERE  VISIBILITY_FLAG = 0 " +
                //         " AND    ASST_ID > 4 " +
                //         " ORDER BY ASST_ID DESC ";
                strSQL = " SELECT ASST_ID," +
                    "             FA_YEAR " +
                    "      FROM   MST_ASSESSMENT " +
                    "      WHERE  VISIBILITY_FLAG = 0 " +
                    "      AND    FLAG_26_27_ONWARDS = 1 " +
                    "      ORDER BY ASST_ID DESC";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbFAYear, 1) == false) return;
                //--
                cmbFAYear.SelectedIndex = 1;
                //-----------------------------------------------------------
                ControlVisible(false);
                ClearControls();
                //-----------------------------------------------------------
                lblTitle.Text = "Section - Reason";
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
                if (cmbFormNo.SelectedIndex == 0)
                {
                    cmnService.J_UserMessage("Please Form");
                    cmbFormNo.Select();
                    return false;
                }

                if (cmbFAYear.SelectedIndex == 0)
                {
                    cmnService.J_UserMessage("Please select FA Year");
                    cmbFAYear.Select();
                    return false;
                }

                if (cmbSection.SelectedIndex == 0)
                {
                    cmnService.J_UserMessage("Please select Section");
                    cmbSection.Select();
                    return false;
                }
                //
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
                string[,] strMatrix = {{"REASON_ID", "0", "", "", "", "F", ""},
                                    {"Reason", "100", "", "", "", "", ""},
                                    {"Reason Description", "300", "", "", "", "", "T"}};
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //strOrderBy = "NONSALARY_TAX_SLAB_ID";
                strSQL = @" SELECT REASON_ID,
                                     REASON,
                                     DESCRIPTION 
                               FROM  MST_REASON
                               WHERE 1=2";

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
                string FormNo = TdsMan.GetFormNoIT1961(cmbFormNo.Text);
                string Section = cmbSection.Text.Substring(0, 4);
                string[,] strMatrix = {{"REASON_ID", "0", "", "", "", "F", ""},
                                    {"Reason", "100", "", "", "", "", ""},
                                    {"Reason Description", "300", "", "", "", "", "T"}};
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //strOrderBy = "NONSALARY_TAX_SLAB_ID";
                strQuery = @" SELECT MST_REASON.REASON_ID,
                                     MST_REASON.REASON,
                                     MST_REASON.DESCRIPTION 
                               FROM  MST_REASON INNER JOIN PAR_SECTION_REASON
                                ON MST_REASON.REASON = PAR_SECTION_REASON.REASON
                               WHERE PAR_SECTION_REASON.FORM_NO = '" + FormNo + @"'
                               AND   MST_REASON.FORM_NO = '" + FormNo + @"'
                               AND   PAR_SECTION_REASON.SECTION_NO = '" + Section + @"'
                               AND   PAR_SECTION_REASON.VALID_FROM_ASST_ID >= " + Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)) + @"
                               ORDER BY MST_REASON.REASON ";
                //if (cmbSection.SelectedIndex > 0)
                //{
                //    strQuery += " WHERE SECTION_ID = " + Convert.ToInt32(Support.GetItemData(cmbSection, cmbSection.SelectedIndex)) + " ";
                //    strQuery1 = " AND";
                //}
                //else
                //    strQuery1 = " WHERE ";
                //--
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
                strSQL = strQuery;
                //--
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

        #region LoadSection
        private void LoadSection(long FAYearId, string FormNo)
        {
            FormNo = TdsMan.GetFormNoIT1961(FormNo);
            //
            strSQL = " SELECT SECTION_ID," +
                "             SECTION_NO  + ' - ' + SECTION_DESCRIPTION + ' - ' + FORM_NAME AS SECTION_NO " +
                "      FROM   MST_SECTION " +
                "      WHERE  SECTION_ID > 0 " +
                "      AND    FORM_NAME = '" + FormNo + "' " +
                "      AND ASST_ID <= " + FAYearId + " " +
                "      AND (VALID_UPTO_ASST_ID >= " + FAYearId + " OR VALID_UPTO_ASST_ID = 0) " +
                "      ORDER BY SECTION_NO ";
            //
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbSection) == false) return;

        }
        #endregion
        private void cmbFAYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFAYear.SelectedIndex <= 0)
            {
                ShowDataInGridBlank();
                cmbSection.Items.Clear();
                return;
            }
            //
            if (cmbFormNo.SelectedIndex <= 0)
            {
                ShowDataInGridBlank();
                cmbSection.Items.Clear();
                return;
            }
            //-----------
            //-- SECTION
            //-----------
            LoadSection(Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)), cmbFormNo.Text);
            //strSQL = " SELECT SECTION_ID," +
            //    "             SECTION_NO  + ' - ' + SECTION_DESCRIPTION + ' - ' + FORM_NAME AS SECTION_NO " +
            //    "      FROM   MST_SECTION " +
            //    "      WHERE  SECTION_ID > 0 " +
            //    "      AND    FORM_NAME <> '24Q' " +
            //    //"      AND    ASST_ID <= " + Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)) + " " +
            //    "      AND ASST_ID <= " + Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)) + " " +
            //    "      AND (VALID_UPTO_ASST_ID >= " + Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)) + " OR VALID_UPTO_ASST_ID = 0) " +
            //    "      ORDER BY SECTION_NO ";
            //////////"      WHERE  FORM_NAME = '" + strFormNo + "' " +
            //////////"      AND    ASST_ID  <= " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + " " +
            //////////"      ORDER BY SECTION_ID";

            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbSection) == false) return;
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

