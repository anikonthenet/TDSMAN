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
using TDSMAN.FormTrn;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion


namespace TDSMAN.FormMst
{
    public partial class MstDeducteeMasterComplianceCheck : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public MstDeducteeMasterComplianceCheck()
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
        //string strQueryWhere;
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        string strTempMode;

        bool blnShowHelp = true;
        int intCountRecords = 0;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        ToolTip tllTip = new ToolTip();

        int intCompanyIndex;


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
            //_form_resize._dgv_Column_Adjust(dgvGrid, true);
        }
        #endregion

        #region MstDeducteeMasterComplianceCheck_Load
        private void MstDeducteeMasterComplianceCheck_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            ViewGrid.Visible = false;
        }
        #endregion

        #region MstDeducteeMasterComplianceCheck_Activated
        private void MstDeducteeMasterComplianceCheck_Activated(object sender, EventArgs e)
        {
            //-----------------------------------------------------------
            GC.Collect();
            //
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);
            ViewGrid.Visible = false;
            //-----------------------------------------------------------
            lblTitle.Text = "Deductee Master - Compliance Check for Section 206AB & 206CCA";
            //-----------------------------------------------------------
            //dgvGrid.Height = 518;
            //
            ControlVisible(false);
            ClearControls();
            //
            LoadGrid(" MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE IS NOT NULL");
            //-----------------------------------------------------------
            //Added by Indrajit on 12-02-2013
            ViewGrid_Click(sender, e);
            //Starting the timer
            //tmrGridRefresh.Start();
            lblMode.Text = J_Mode.Add;
            cmnService.J_StatusButton(this, lblMode.Text);
            ViewGrid.Visible = false;
            BtnAdd_Click(sender, e);
            txtDeducteePAN.Select();
            //-----------------------------------------------------------
        }
        #endregion


        #region ViewGrid_Click

        private void ViewGrid_Click(object sender, EventArgs e)
        {
            if (dgvGrid.CurrentRow == null)
            {
                BtnAdd.Focus();
                return;
            }
            //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.CurrentRowIndex, 0]));
            lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[1].Value));


            //dgvGrid.Select(dgvGrid.CurrentRowIndex);
            //dgvGrid.Select();
            //dgvGrid.Focus();
        }

        #endregion

        #region ViewGrid_DoubleClick
        private void ViewGrid_DoubleClick(object sender, System.EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }
        #endregion

        #region ViewGrid_KeyDown
        private void ViewGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (dgvGrid.CurrentRow == null) return;
                //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.CurrentRowIndex, 0]));
                lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
                if (e.KeyCode == Keys.Enter) BtnEdit_Click(sender, e);
                //if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);

                strTempMode = lblMode.Text;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region ViewGrid_CurrentCellChanged
        private void ViewGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.CurrentRowIndex, 0]));

            //lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
        }
        #endregion

        #region ViewGrid_MouseUp
        private void ViewGrid_MouseUp(object sender, MouseEventArgs e)
        {
            ViewGrid_Click(sender, e);
        }
        #endregion

        #region ViewGrid_MouseMove
        private void ViewGrid_MouseMove(object sender, MouseEventArgs e)
        {
            //cmnService.J_GridToolTip(dgvGrid, e.X, e.Y);
        }
        #endregion



        #region BtnAdd_Click

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //--
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                ViewGrid.Visible = false;
                lblSearchMode.Text = J_Mode.General;
                //---------------------------------------------
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                //chkSelectAll.Checked = false;
                //---------------------------------------------
                strCheckFields = "";
                //---------------------------------------------
                dgvGrid.Visible = true;
                chkHigherRateApplicable.Checked = true;
                chkHigherRateApplicable.Enabled = false;
                txtDeducteePAN.Select();
                //---------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnEdit_Click

        private void BtnEdit_Click(object sender, System.EventArgs e)
        {
            try
            {
                //if (cmbCompanyName.SelectedIndex <= 0)
                //{
                //    cmnService.J_UserMessage("Select the Company");
                //    cmbCompanyName.Select();
                //    return;
                //}
                //--
                if (dgvGrid.CurrentRow != null)
                {
                    //-- Added By Abhishek Dey On 01/11/2019 --
                    #region IS COMPANY ACCESSABLE FOR CLIENT
                    //if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                    //{
                    //    if (dmlService.J_IsDatabaseObjectExist("TRN_TAN_USER") == true)
                    //    {
                    //        if (TdsMan.IsCompanyAccessible(Convert.ToInt64(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)), TDSMAN.Classes.TDSMAN.T_MACHINE_ID) == false)
                    //        {
                    //            cmnService.J_UserMessage("You are not authorised to proceed.");
                    //            BtnCancel.Select();
                    //            return;
                    //        }

                    //        //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TRN_TAN_USER WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " AND COMPANY_ID = " + )) > 0)

                    //    }
                    //}
                    #endregion
                    //-----------------------------------------
                    //Added by Indrajit on 18-02-2013
                    if (Check_Record(lngSearchId) == 0) return;
                    //--------------------------------------------------
                    ControlVisible(true);
                    ClearControls();
                    //--------------------------------------------------
                    //A particular ID wise retriving the data from database
                    lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
                    //if (ShowRecord(Convert.ToInt64(Convert.ToString(dgvGrid[dgvGrid.CurrentRowIndex, 0]))) == false)
                    if (ShowRecord(lngSearchId) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId);
                    }
                    //
                    //cmbCompanyName.Enabled = false;
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    ViewGrid.Visible = false;
                    lblSearchMode.Text = J_Mode.General;
                    dgvGrid.Visible = true;
                    //
                    //chkHideEmployee.Visible = true;
                    //--------------------------------------------------
                    strCheckFields = "";
                    //--------------------------------------------------
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                }
            }
            catch (Exception err_handler)
            {
                ControlVisible(false);
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnSave_Click

        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            //--
            int intGridCount = 0;
            //--
            dmlService.J_BeginTransaction();
            if (ValidateFields(out intGridCount) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            //
            int intHigherRate = 0;
            //
            if (intGridCount > 0)
            {
                if (chkWhiteListedDeductee.Checked == false)
                {
                    if (cmnService.J_UserMessage("Do you want to un-mark " + intGridCount.ToString() + " PAN(s) from Higher Rate?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }
                else
                {
                    intHigherRate = 1;
                    if (cmnService.J_UserMessage("Do you want to mark " + intGridCount.ToString() + " PAN(s) for Higher Rate?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }
                //--
                this.Cursor = Cursors.WaitCursor;
                foreach (DataGridViewRow row in dgvGrid.Rows)
                {
                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                    {
                        if (intHigherRate == 0)
                        {
                            strSQL = "UPDATE MST_DEDUCTEE " +
                                     "SET    COMPLIANCE_HIGHER_RATE_FLAG              = " + intHigherRate + ", " +
                                     "       COMPLIANCE_HIGHER_RATE_WHITE_LISTED_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(J_ReturnServerDate()) + cmnService.J_DateOperator() + ", " +
                                     "       COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE = NULL " +
                                     "WHERE  DEDUCTEE_ID   =  " + Convert.ToInt32(row.Cells[1].Value) + "";
                        }
                        else if (intHigherRate == 1)
                        {
                            strSQL = "UPDATE MST_DEDUCTEE " +
                                     "SET    COMPLIANCE_HIGHER_RATE_FLAG              = " + intHigherRate + ", " +
                                     "       COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(J_ReturnServerDate()) + cmnService.J_DateOperator() + ", " +
                                     "       COMPLIANCE_HIGHER_RATE_WHITE_LISTED_DATE = NULL " +
                                     "WHERE  DEDUCTEE_ID   =  " + Convert.ToInt32(row.Cells[1].Value) + "";
                        }
                        //--
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            //txtDeducteePAN.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
            else
            {
                if (chkHigherRateApplicable.Checked == true)
                {
                    intHigherRate = 1;
                    //
                    if (cmnService.J_UserMessage("Do you want to mark [" + txtDeducteePAN.Text + "] for Higher Rate?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }
                else
                {
                    intHigherRate = 0;
                    //
                    if (cmnService.J_UserMessage("Do you want to un-mark [" + txtDeducteePAN.Text + "] from Higher Rate?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }
                //-- GET DEDUCTEE ID
                strSQL = "SELECT DEDUCTEE_ID " +
                    "     FROM   MST_DEDUCTEE " +
                    "     WHERE  DEDUCTEE_PAN  ='" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "'";
                //--
                //Modified by Indrajit on 22-02-2013
                //lngEmployeeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
                DMLService dml = new DMLService();
                lngSearchId = cmnService.J_NullToZero(dml.J_ExecSqlReturnScalar(strSQL));
                //--
                //--
                if (intHigherRate == 0)
                {
                    strSQL = "UPDATE MST_DEDUCTEE " +
                             "SET    COMPLIANCE_HIGHER_RATE_FLAG              = " + intHigherRate + ", " +
                             "       COMPLIANCE_HIGHER_RATE_WHITE_LISTED_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(J_ReturnServerDate()) + cmnService.J_DateOperator() + ", " +
                             "       COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE = NULL " +
                             "WHERE  DEDUCTEE_ID   =  " + lngSearchId + "";
                }
                else if (intHigherRate == 1)
                {
                    strSQL = "UPDATE MST_DEDUCTEE " +
                             "SET    COMPLIANCE_HIGHER_RATE_FLAG              = " + intHigherRate + ", " +
                             "       COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(J_ReturnServerDate()) + cmnService.J_DateOperator() + ", " +
                             "       COMPLIANCE_HIGHER_RATE_WHITE_LISTED_DATE = NULL " +
                             "WHERE  DEDUCTEE_ID   =  " + lngSearchId + "";
                }
                //--
                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                {
                    txtDeducteePAN.Select();
                    dmlService.J_Rollback();
                    return;
                }
            }
            //--
            dmlService.J_Commit();
            //--
            if (chkWhiteListedDeductee.Checked==true)
                LoadGrid("MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_WHITE_LISTED_DATE IS NOT NULL");
            else
                LoadGrid("MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE IS NOT NULL");
            //--
            //dmlService.J_Commit();
            cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
            //-----------------------------------------------------------
            ClearControls();
            chkSelectAll.Checked = false;
            //
            chkHigherRateApplicable.Checked = true;
            chkHigherRateApplicable.Enabled = false;
            //-----------------------------------------------------------
            txtDeducteePAN.Select();
            //-----------------------------------------------------------

        }

        #endregion

        #region BtnCancel_Click

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);      //Status[i.e. Enable/Visible] of Button, Frame, Grid
                ViewGrid.Visible = false;
                //-------------------------------------------
                //DisableControls();
                //-------------------------------------------
                ControlVisible(false);
                ClearControls();					//Clear all the Controls
                chkSelectAll.Checked = false;
                //-------------------------------------------
                //strSQL = strQuery + "ORDER BY " + strOrderBy;
                ////-------------------------------------------
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                //if (dsetGridClone == null) return;
                ////
                LoadGrid(" MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE IS NOT NULL");
                //cmbCompanyName.Enabled = true;
                //-------------------------------------------
                if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId) == false)
                    BtnAdd.Select();
                //
                //chkHideEmployee.Visible = false;
                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
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
        
        #region chkWhiteListedDeductee_CheckedChanged
        private void chkWhiteListedDeductee_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWhiteListedDeductee.Checked == true)
            {
                LoadGrid(" MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_WHITE_LISTED_DATE IS NOT NULL");
            }
            else if (chkWhiteListedDeductee.Checked == false)
            {
                LoadGrid(" MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE IS NOT NULL");
            }

        }
        #endregion

        #region txtDeducteePAN_TextChanged
        private void txtDeducteePAN_TextChanged(object sender, EventArgs e)
        {
            IDataReader drdShowDeducteeHelp = null;
            string strSQLShowHelpDeductee = "";
            //--
            try
            {
                if (txtDeducteePAN.Text.Trim() == "")
                {
                    lstDeducteeNameHelp.Visible = false;
                    TdsMan.GetSetup();
                    return;
                }
                //--
                if (blnShowHelp == false)
                    return;
                //-----------------------

                strSQLShowHelpDeductee = @"SELECT DEDUCTEE_NAME,
                                                DEDUCTEE_PAN 
                                            FROM(
                                            SELECT DEDUCTEE_NAME,
                                                    DEDUCTEE_PAN 
                                            FROM   MST_DEDUCTEE
                                            WHERE  DEDUCTEE_PAN LIKE '" + TDSMAN.Classes.TDSMAN.T_DeducteeSearchLikeOperator + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "%' " +
                                                @"AND    INACTIVE_FLAG = 0 AND COMPLIANCE_HIGHER_RATE_FLAG = 1
                                            UNION
                                            SELECT DEDUCTEE_NAME,
                                                    DEDUCTEE_PAN 
                                            FROM   COR_TRN_DEDUCTEE_DETAILS 
                                            WHERE  DEDUCTEE_PAN LIKE '" + TDSMAN.Classes.TDSMAN.T_DeducteeSearchLikeOperator + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "%' " +
                                                @"GROUP BY DEDUCTEE_NAME, DEDUCTEE_PAN)  AS REGULAR
                                            ORDER BY REGULAR.DEDUCTEE_NAME";

                drdShowDeducteeHelp = dmlService.J_ExecSqlReturnReader(strSQLShowHelpDeductee);
                //--
                if (drdShowDeducteeHelp == null)
                {
                    lstDeducteeNameHelp.Visible = false;
                    return;
                }
                else
                {
                    lstDeducteeNameHelp.Items.Clear();
                    //lstDeducteeNameHelp.Height = 19;
                    lstDeducteeNameHelp.Visible = true;
                    //lstDeducteeNameHelp.Location = new Point(4, 88);
                    while (drdShowDeducteeHelp.Read())
                    {                      
                        //lstDeducteeNameHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["DEDUCTEE_NAME"].ToString()));
                        lstDeducteeNameHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["DEDUCTEE_NAME"].ToString().PadRight(50) + drdShowDeducteeHelp["DEDUCTEE_PAN"].ToString()));
                        //--
                        //if (lstDeducteeNameHelp.Height <= 100)
                        //    lstDeducteeNameHelp.Height = lstDeducteeNameHelp.Height + 19;
                    }
                    //--
                    if (lstDeducteeNameHelp.Items.Count <= 0)
                        lstDeducteeNameHelp.Visible = false;
                    //--
                    //--
                }
                //-----------------------------------------------------------
                drdShowDeducteeHelp.Close();
                drdShowDeducteeHelp.Dispose();
                //-----------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                drdShowDeducteeHelp.Close();
                drdShowDeducteeHelp.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
            }
            //-----------------------            
        }
        #endregion


        #region txtDeducteePAN_KeyPress
        private void txtDeducteePAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                if (lstDeducteeNameHelp.Visible == true)
                {
                    lstDeducteeNameHelp.Focus();
                    lstDeducteeNameHelp.SelectedIndex = 0;
                }
                else
                    SendKeys.Send("{tab}");
            }
            else
                if (TdsMan.gTANNoPANNoValidation(txtDeducteePAN, e, T_TANPAN.PAN) == false)
                e.Handled = true;
            //
        }
        #endregion

        #region txtDeducteePAN_KeyDown
        private void txtDeducteePAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (lstDeducteeNameHelp.Visible == true)
                {
                    lstDeducteeNameHelp.Focus();
                    lstDeducteeNameHelp.SelectedIndex = 0;
                }
            }
            //
            if (e.KeyCode == Keys.F1)
            {
                TDSMAN.Classes.TDSMAN.T_ShowCompanyWiseDeductee = false;
                txtDeducteePAN_TextChanged(sender, e);
            }
        }
        #endregion

        #region txtDeducteePAN_Leave
        private void txtDeducteePAN_Leave(object sender, EventArgs e)
        {
            //if (lstDeducteeHelp.Visible == true) lstDeducteeHelp.Visible = false;
            //TdsMan.GetSetup();
        }
        #endregion
        
        #region lstDeducteeHelp_Click
        private void lstDeducteeHelp_Click(object sender, EventArgs e)
        {
            //if (strFormNo == T_FormNo.F24Q)
                //{
                //    string strEmployeeHelp = "";
                //    strEmployeeHelp = lstDeducteeNameHelp.Text.Trim();
                //    //
                //    txtDeducteePAN.Text = cmnService.J_Right(strEmployeeHelp, 10);
                //    txtDeducteeName.Text = cmnService.J_Left(strEmployeeHelp, 45).Trim();
                //}
                //else
                //{
                //--
                //if (TDSMAN.Classes.TDSMAN.T_ShowCompanyWiseDeductee == true)
                //{
                //    if (lstDeducteeNameHelp.Text == strHelpLastLineText)
                //        return;
                //}
                //--
                //if (cmnService.J_Right(lstDeducteeNameHelp.Text.Trim(), 2) == T_DeducteeCode.Company)
                //    cmbDeducteeCode.Text = T_DeducteeCodeDesc.Company;
                //else if (cmnService.J_Right(lstDeducteeNameHelp.Text.Trim(), 2) == T_DeducteeCode.NonCompany)
                //    cmbDeducteeCode.Text = T_DeducteeCodeDesc.NonCompany;
                ////
                //txtDeducteePAN.Text = cmnService.J_Left(cmnService.J_Right(lstDeducteeNameHelp.Text.Trim(), 15), 10);
                //txtDeducteeName.Text = cmnService.J_Mid(lstDeducteeNameHelp.Text.Trim(), 0, (lstDeducteeNameHelp.Text.Length - 15)).Trim();
                txtDeducteeName.Text = cmnService.J_Mid(lstDeducteeNameHelp.Text.Trim(), 0, (lstDeducteeNameHelp.Text.Length - 10)).Trim();
            txtDeducteePAN.Text = cmnService.J_Right(lstDeducteeNameHelp.Text.Trim(), 10);
            //
            //if (strFormNo == T_FormNo.F27Q)
            //{
            //    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2016_17ID)
            //    {
            //        strSQL = @"SELECT DEDUCTEE_ID 
            //                   FROM   MST_DEDUCTEE 
            //                   WHERE  DEDUCTEE_PAN ='" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text.Trim()) + @"'
            //                   AND    DEDUCTEE_NAME ='" + cmnService.J_ReplaceQuote(txtDeducteeName.Text.Trim()) + @"'";
            //        long lngDeducteeIDF27Q = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL));
            //        //--
            //        txtF27QEmail.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT EMAIL FROM MST_DEDUCTEE WHERE DEDUCTEE_ID =" + lngDeducteeIDF27Q));
            //        txtF27QContactNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MOBILE_NO FROM MST_DEDUCTEE WHERE DEDUCTEE_ID =" + lngDeducteeIDF27Q));
            //        txtF27QTaxIdentificationNumber.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT DEDUCTEE_TAX_ID FROM MST_DEDUCTEE WHERE DEDUCTEE_ID =" + lngDeducteeIDF27Q));
            //        txtF27QDeducteeAddress.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT DEDUCTEE_ADDRESS FROM MST_DEDUCTEE WHERE DEDUCTEE_ID =" + lngDeducteeIDF27Q));
            //    }
            //}
            //
            //}
            //--
            lstDeducteeNameHelp.Visible = false;
            //--
            txtDeducteePAN.Select();
        }
        #endregion

        #region lstDeducteeHelp_KeyPress
        private void lstDeducteeHelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                lstDeducteeHelp_Click(sender, e);
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstDeducteeNameHelp.Visible = false;
                txtDeducteeName.Select();
            }
        }
        #endregion


        #region BtnSearch_Click
        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            int intGridCount = 0;
            try
            {
                //-------------------------------------------
                lblSearchMode.Text = J_Mode.Searching;
                //-------------------------------------------
                if (ValidateFields(out intGridCount) == false) return;
                //-------------------------------------------
                grpSort.Visible = false;
                grpSearch.Visible = true;
                //-------------------------------------------
                txtDeducteePANSearch.Select();
                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_Click
        private void BtnSearchOK_Click(object sender, System.EventArgs e)
        {
            int intGridCount = 0;
            try
            {
                //-------------------------------------------------------------------
                if (ValidateFields(out intGridCount) == false) return;
                strCheckFields = "";
                //-------------------------------------------------------------------
                //--- Storing the Criteria Fiels & Values ---------------------------
                //-------------------------------------------------------------------
                //-- PAN NO 
                //-------------------------------------------------------------------
                if (txtDeducteePANSearch.Text.Trim() != "")
                    strCheckFields = " AND MST_DEDUCTEE.DEDUCTEE_PAN like '%" + cmnService.J_ReplaceQuote(txtDeducteePANSearch.Text.Trim().ToUpper()) + "%'  ";
                //-------------------------------------------------------------------
                //-- DEDUCTEE NAME
                //-------------------------------------------------------------------
                if (txtDeducteeNameSearch.Text.Trim() != "")
                    if (strCheckFields == "")
                        strCheckFields = " AND MST_DEDUCTEE.DEDUCTEE_NAME like '" + cmnService.J_ReplaceQuote(txtDeducteeNameSearch.Text.Trim().ToUpper()) + "%'  ";
                    else
                        strCheckFields = strCheckFields + " AND MST_DEDUCTEE.DEDUCTEE_NAME like '" + cmnService.J_ReplaceQuote(txtDeducteeNameSearch.Text.Trim().ToUpper()) + "%'  ";
                //-------------------------------------------------------------------
                //-- DEDUCTEE CODE
                //-------------------------------------------------------------------
                //if (cmbDeducteeCodeSearch.SelectedIndex > 0)
                //    if (strCheckFields == "")
                //        strCheckFields = "WHERE MST_DEDUCTEE.DEDUCTEE_CODE = '" + cmnService.J_ReplaceQuote(cmbDeducteeCodeSearch.Text.Substring(0, 2)) + "' ";
                //    else
                //        strCheckFields = strCheckFields + " AND MST_DEDUCTEE.DEDUCTEE_CODE = '" + cmnService.J_ReplaceQuote(cmbDeducteeCodeSearch.Text.Substring(0, 2)) + "' ";
                //----------------------------------------------------------------------
                //strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                ////----------------------------------------------------------------------
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                //if (dsetGridClone == null) return;
                ////----------------------------------------------------------------------
                //if (dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, "DEDUCTEE_ID", lngSearchId) == false)
                //{
                //    txtDeducteePANSearch.Select();
                //    return;
                //}
                //--
                if (chkWhiteListedDeductee.Checked == true)
                    LoadGrid(" MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_WHITE_LISTED_DATE IS NOT NULL " + strCheckFields);
                else
                    LoadGrid(" MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE IS NOT NULL "+ strCheckFields);
                //--
                //----------------------------------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                //----------------------------------------------------------------------
                grpSearch.Visible = false;
                //----------------------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_KeyPress
        private void BtnSearchOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnSearchCancel_Click
        private void BtnSearchCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                //----------------------------------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                grpSearch.Visible = false;
                //----------------------------------------------------------------------
                if (strCheckFields == "")
                    strSQL = strQuery + " order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + " order by " + strOrderBy;
                //----------------------------------------------------------------------
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                //if (dsetGridClone == null) return;
                TdsMan.PopulateGridView(dgvGrid, strSQL, strMatrix);
                //----------------------------------------------------------------------
                dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                //----------------------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchCancel_KeyPress
        private void BtnSearchCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion
        
        #endregion

        #region User Defined Functions


        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            //pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            txtDeducteePAN.Text = "";
            txtDeducteeName.Text = "";
            chkHigherRateApplicable.Checked = false;
            chkHigherRateApplicable.Enabled = true;
            //
            txtDeducteePANSearch.Text = "";
            txtDeducteeNameSearch.Text = "";
            //--
        }
        #endregion

        #region Check_Record
        private long Check_Record(long lngSrchId)
        {
            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "MST_DEDUCTEE", "DEDUCTEE_ID", lngSrchId) == true) return lngSrchId;

            cmnService.J_UserMessage("Record has been deleted.");
            lngSrchId = 0;
            //-------------------------------------------
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
            ViewGrid.Visible = false;
            //-------------------------------------------
            ControlVisible(false);
            ClearControls();					//Clear all the Controls
            //-------------------------------------------
            strSQL = strQuery + " order by " + strOrderBy;
            //-------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
            if (dsetGridClone == null) return 0;
            //-------------------------------------------
            BtnAdd.Select();
            //-------------------------------------------                            
            return 0;

        }
        #endregion
        
        #region LoadGrid
        private void LoadGrid(string QueryWhere)
        {
            string strQueryWhere = QueryWhere;
            //-----------------------------------------------------------
            string[,] strMatrix1 = {{"DeducteeID", "0", "", "", "", "F", ""},
                                    {"Deductee PAN", "150", "", "", "", "", "T"},
                                    {"Deductee Name", "645", "", "", "", "", "T"}};
            //-----------------------------------------------------------
            strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            strOrderBy = "MST_DEDUCTEE.DEDUCTEE_NAME";
            strQuery = "SELECT MST_DEDUCTEE.DEDUCTEE_ID   AS DEDUCTEE_ID," +
                "              MST_DEDUCTEE.DEDUCTEE_PAN  AS DEDUCTEE_PAN," +
                "              MST_DEDUCTEE.DEDUCTEE_NAME AS DEDUCTEE_NAME " +
                "       FROM   MST_DEDUCTEE WHERE " + strQueryWhere;
            //strQueryWhere = " WHERE  MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE IS NOT NULL";
            // OR MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_WHITE_LISTED_DATE IS NOT NULL";
            //-----------------------------------------------------------
            strSQL = strQuery + " ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
            TdsMan.PopulateGridView(dgvGrid, strSQL, strMatrix);

            dgvGrid.ClearSelection();

            //foreach (DataGridViewRow row in dgvGrid.Rows)
            //{
            //    dgvGrid.Rows[0].SetValues(true);
            //}
            //-----------------------------------------------------------
        }
        #endregion

        #region dgvGrid_CellContentClick
        private void dgvGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR)
                //{
                    if (e.RowIndex != -1)
                    {
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvGrid.Rows[e.RowIndex].Cells[0];
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
                //}
            }
            catch (Exception ERR)
            {

            }
        }

        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            string strCompanyName = string.Empty;
            string strTanNo = string.Empty;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            try
            {
                //
                strSQL = "SELECT  MST_DEDUCTEE.DEDUCTEE_ID   AS DEDUCTEE_ID," +
                    "             MST_DEDUCTEE.DEDUCTEE_PAN  AS DEDUCTEE_PAN," +
                    "             MST_DEDUCTEE.DEDUCTEE_NAME AS DEDUCTEE_NAME," +
                    "             MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_FLAG AS COMPLIANCE_HIGHER_RATE_FLAG " +
                    "     FROM    MST_DEDUCTEE " +
                    "     WHERE   MST_DEDUCTEE.DEDUCTEE_ID = " + Id;
                    //"     AND     MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE IS NOT NULL OR MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_WHITE_LISTED_DATE IS NOT NULL";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;
                    //--
                    blnShowHelp = false;
                    txtDeducteePAN.Text = Convert.ToString(drdShowRecord["DEDUCTEE_PAN"]);
                    txtDeducteeName.Text = Convert.ToString(drdShowRecord["DEDUCTEE_NAME"]);
                    blnShowHelp = true;
                    //--
                    if (Convert.ToString(drdShowRecord["COMPLIANCE_HIGHER_RATE_FLAG"]) == "1")
                    {
                        chkHigherRateApplicable.Checked = true;
                    }
                    else
                    {
                        chkHigherRateApplicable.Checked = false;
                    }
                    //
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    
                    return true;
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                lngSearchId = 0;
                //-----------------------------------------------------------
                //if (strCheckFields == "")
                //    strSQL = strQuery + "order by " + strOrderBy;
                //else
                //    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                ////-----------------------------------------------------------
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }

        #endregion

        #region ValidateFields
        private bool ValidateFields(out int iGridCount)
        {
            int intGridCount = 0;
            iGridCount = 0;
            try
            {
                if (lblSearchMode.Text == J_Mode.Sorting)
                {
                    //if (Convert.ToInt64(Convert.ToString(dgvGrid.CurrentRowIndex)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, "DEDUCTEE_ID", lngSearchId);
                    //    return false;
                    //}
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    if (grpSearch.Visible == false)
                    {
                        if (dgvGrid.CurrentRow == null)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            if (dsetGridClone == null) return false;
                            dmlService.J_setGridPosition(ref dgvGrid, dsetGridClone, lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtDeducteePANSearch.Text.Trim() == "" &&
                            txtDeducteeNameSearch.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtDeducteePANSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    if (txtDeducteePAN.Text.Trim() != "" && txtDeducteeName.Text.Trim() != "")
                    {
                        //-----------------------------------------------------------------------
                        //-- PAN
                        //-----------------------------------------------------------------------
                        if (txtDeducteePAN.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("PAN - Cannot be Blank");
                            txtDeducteePAN.Select();
                            return false;
                        }
                        //-----------------------------------------------------------------------
                        //-- PAN FORMAT
                        //-----------------------------------------------------------------------
                        if (txtDeducteePAN.Text.Length != 10)
                        {
                            cmnService.J_UserMessage("PAN No. should be of 10 characters");
                            txtDeducteePAN.Select();
                            return false;
                        }
                        if (txtDeducteePAN.Text != "PANNOTAVBL")
                        {
                            //---------------------------
                            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtDeducteePAN.Text, 5), J_DataType.Character) == false)
                            {
                                cmnService.J_UserMessage("Incorrect Format of the PAN");
                                txtDeducteePAN.Select();
                                return false;
                            }
                            //---------------------------
                            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtDeducteePAN.Text, 5, 4), J_DataType.Numeric) == false)
                            {
                                cmnService.J_UserMessage("Incorrect Format of the PAN");
                                txtDeducteePAN.Select();
                                return false;
                            }
                            //---------------------------
                            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtDeducteePAN.Text, 1), J_DataType.Character) == false)
                            {
                                cmnService.J_UserMessage("Incorrect Format of the PAN");
                                txtDeducteePAN.Select();
                                return false;
                            }
                        }
                        //--
                        strSQL = "SELECT DEDUCTEE_ID " +
                            "     FROM   MST_DEDUCTEE " +
                            "     WHERE  DEDUCTEE_PAN  = '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "' ";
                        //lngDeducteeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL));
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) == 0)
                        {
                            cmnService.J_UserMessage("This PAN does not exists in the master.\nTo specify any deductee u/s 206AB & 206CCA enter in master firstly.");
                            txtDeducteePAN.Select();
                            return false;
                        }
                        //-----------------------------------------------------------------------
                        //-- DEDUCTEE NAME
                        //-----------------------------------------------------------------------
                        if (txtDeducteeName.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Deductee Name - Cannot be Blank");
                            txtDeducteeName.Select();
                            return false;
                        }
                        // CHECK '^'
                        if (TdsMan.T_DetectCaret(txtDeducteeName.Text.Trim(), "^") == true)
                        {
                            cmnService.J_UserMessage("Deductee Name - '^' not allowed");
                            txtDeducteeName.Select();
                            return false;
                        }
                    }
                    else
                    {
                        if (dgvGrid.RowCount <= 0)
                        {
                            cmnService.J_UserMessage("No record found");
                            return false;
                        }
                        //--
                        if (J_GenerateDataGridViewSelectedId(dgvGrid, 1) == "")
                        {
                            cmnService.J_UserMessage("Please select a record");
                            return false;
                        }

                        //string strBatchIds = "";
                        int i = 0;
                        //
                        foreach (DataGridViewRow row in dgvGrid.Rows)
                        {
                            if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                            {
                                //if (row.Cells[1].Value.ToString() != "")
                                //{
                                //    strBatchIds = strBatchIds + row.Cells[1].Value.ToString() + ",";
                                //}
                                intGridCount++;
                            }
                        }
                        //
                        iGridCount = intGridCount;
                        //strBatchIds = cmnService.J_Mid(strBatchIds, 0, strBatchIds.Length - 1);
                        //
                        //if (cmnService.J_UserMessage(i.ToString() + " PAN(s) selected for white listing.\nProceed??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            return true;                        
                        //else
                        //    return false;
                    }
                    //--
                    return true;
                }
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


        #region Searching_KeyPress
        private void Searching_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }

        #endregion

        #endregion

        #region Searching_KeyPress
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);
            ViewGrid.Visible = false;
            //-----------------------------------------------------------
            lblSearchMode.Text = J_Mode.General;
            //-----------------------------------------------------------
            //DisableControls();
            //-----------------------------------------------------------
            ClearControls();
            chkSelectAll.Checked = false;
            if (chkWhiteListedDeductee.Checked == true)
                LoadGrid(" MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_WHITE_LISTED_DATE IS NOT NULL ");
            else
                LoadGrid(" MST_DEDUCTEE.COMPLIANCE_HIGHER_RATE_BLACK_LISTED_DATE IS NOT NULL ");
        }

        #endregion

        #region  dgvGrid_CellValueChanged
        private void dgvGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvGrid.Rows[e.RowIndex].Cells[0];
                //DataGridViewCell cellChildMenuID = (DataGridViewCell)dgvDeductees.Rows[e.RowIndex].Cells[1];
                //if (cell.Value == null || (bool)cell.Value == false)
                if ((bool)cell.Value == false)
                {
                    //grdvNewDescription.MultiSelect = false;
                    //grdvNewDescription.Rows[e.RowIndex].Selected = true;
                    //cell.Value = true;
                    //cmnService.J_UserMessage("UNCHECK"); 
                    //strSQL = "DELETE FROM TRN_USER_MENU_ACCESS WHERE SETUP_ID =" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + " AND CHILD_MENU_ID = " + Convert.ToInt32(cellChildMenuID.Value.ToString());
                    //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                    //lngSelectedGrid = lngSelectedGrid - 1;
                }
                else if ((bool)cell.Value == true)
                {
                    //grdvNewDescription.Rows[e.RowIndex].Selected = false;
                    //cell.Value = false;
                    //cmnService.J_UserMessage("CHECK");
                    //strSQL = "INSERT INTO TRN_USER_MENU_ACCESS (SETUP_ID, CHILD_MENU_ID) VALUES (" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + "," + Convert.ToInt32(cellChildMenuID.Value.ToString()) + ")";
                    //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                    //lngSelectedGrid = lngSelectedGrid + 1;
                }
            }
        }
        #endregion

        #region J_GenerateDataGridViewSelectedId
        public string J_GenerateDataGridViewSelectedId(DataGridView dataGridView, int Index)
        {
            //SelectedItemCount = 0;
            string strItem = "";
            if (dataGridView.RowCount > 0)
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                    {
                        strItem = strItem + "," + row.Cells[Index].Value.ToString();
                        //SelectedItemCount = SelectedItemCount + 1;
                    }
                }

                if (strItem.Length > 0)
                    strItem = cmnService.J_Mid(strItem, 1, strItem.Length - 1);
            }
            return strItem;
        }
        #endregion


        #region CheckAllGridRows
        private void CheckAllGridRows()
        {
            this.Cursor = Cursors.WaitCursor;
            //blnDeleteTempGridRecord = true;
            foreach (DataGridViewRow row in dgvGrid.Rows)
            {
                if (chkSelectAll.Checked == true)//checked all checkbox
                {
                    if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                    {
                        row.Cells[0].Value = true;
                    }
                }
                else
                {
                    if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                    {
                        row.Cells[0].Value = false;
                    }
                }
            }
            //--
            this.Cursor = Cursors.Default;
        }
        #endregion

        #region chkSelectAll_CheckedChanged
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            //--
            CheckAllGridRows();
            //--
            lblMode.Text = J_Mode.Add;
            cmnService.J_StatusButton(this, lblMode.Text);
            ViewGrid.Visible = false;
            BtnAdd_Click(sender, e);
            //
        }

        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0108", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}
