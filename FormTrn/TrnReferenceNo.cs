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

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnReferenceNo : Form
    {
        #region System Generated Code
        public TrnReferenceNo()
        {
            InitializeComponent();
        }
        #endregion

        #region Module Overloading
        public TrnReferenceNo(string Module, string FormNo, long ReturnId)
        {
            InitializeComponent();
            lngBasicInfoID = ReturnId;
            strReturnType = Module;
            strFormNo = FormNo;
        }
        #endregion

        #region Objects & Variables decleration

        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;
        long lngBasicInfoID = 0;
        string strReturnType = "";
        string strFormNo = "";

        bool blnRestrictCellMovement = false;

        //--            
        
        #endregion

        #region User Defined Events

        #region TrnBulkPANVerification_Load
        private void TrnBulkPANVerification_Load(object sender, EventArgs e)
        {
            try
            {
                if (strFormNo == T_FormNo.F26Q)
                {
                    lblTitle.Text = "Enter Deductee Reference number for the Deductee(s)";
                }
                else if (strFormNo == T_FormNo.F24Q)
                {
                    lblTitle.Text = "Enter Employee Serial number for the Employee(s)";
                }
                //--
                LoadDeducteeGrid();
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
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

        #region btnSave_Click
        private void btnVerification_Click(object sender, EventArgs e)
        {
            string strRefNo = "";                
            try
            {
                if (ValidateFields() == false) return;
                //
                foreach (DataGridViewRow row in dgvDeductees.Rows)
                {
                    strRefNo = row.Cells["Ref"].Value.ToString().Trim();
                    //
                    if (strReturnType == T_OTHERMODULENAME.REGULAR_FORM)
                    {
                        //For Regular return

                        if (strFormNo == T_FormNo.F24Q)
                        {
                            // FOR EMPLOYEES
                            //
                            strSQL = "UPDATE MST_EMPLOYEE " +
                                     "SET    EMPLOYEE_REF = '" + cmnService.J_ReplaceQuote(strRefNo) + "' " +
                                     "WHERE  EMPLOYEE_ID = " + row.Cells[0].Value.ToString();
                            dmlService.J_ExecSql(strSQL);
                        }
                        else
                        {
                            // FOR DEDUCTEES
                            //
                            strSQL = "UPDATE MST_DEDUCTEE " +
                                     "SET    DEDUCTEE_REF = '" + cmnService.J_ReplaceQuote(strRefNo) + "'" +
                                     "WHERE  DEDUCTEE_ID = " + row.Cells[0].Value.ToString();
                            dmlService.J_ExecSql(strSQL);
                        }
                    }
                    else
                    {
                        // FOR CORRECTION RETURN
                        //
                        strSQL = "UPDATE COR_TRN_DEDUCTEE_DETAILS " +
                                 "SET    DEDUCTEE_REF  = '" + cmnService.J_ReplaceQuote(strRefNo) + "'" +
                                 "WHERE  DEDUCTEE_NAME = '" + cmnService.J_ReplaceQuote(row.Cells[1].Value.ToString()) + "'" +
                                 "AND    DEDUCTEE_PAN  = 'PANNOTAVBL'" +
                                 "AND    BATCH_HEADER_ID = " + lngBasicInfoID;
                        dmlService.J_ExecSql(strSQL);
                    }
                }
                
            }
            catch (Exception ERR)
            {
                cmnService.J_UserMessage(ERR.Message);
                this.Close();
                this.Dispose();
            }

            //Closing the pop up window after all updations is done
            cmnService.J_UserMessage("Reference no. successfully saved.");
            this.Close();
            this.Dispose();
        }
        #endregion

        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            //blnVerificationComplete = false; 
            //--
            this.Dispose();
            this.Close();
        }
        #endregion

        #endregion

        #region User Define Functions

        #region LoadDeducteeGrid

        #region LoadDeducteeGrid()
        private void LoadDeducteeGrid()
        {
            DataGridViewTextBoxColumn RefCol = new DataGridViewTextBoxColumn();
            RefCol.HeaderText = "Reference No.";
            RefCol.MaxInputLength = 10;
            RefCol.Name = "Ref";


            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
                                        {"Name", "280", "S", "", "", "", "Fill"},
                                        {"PAN No.", "125", "S", "", "", "", ""}};
                ////-----------------------------------------------------------
                ////strMatrix = strMatrix1;
                ////-----------------------------------------------------------
                ///* (1) Column Value
                // * (2) Column Data Type
                // * (3) Replace String
                // * (4) Replace String Data Type */
                ////-----------------------------------------------------------
                ////-----------------------------------------------------------
                ////
                if (strReturnType == T_OTHERMODULENAME.REGULAR_FORM)
                {
                    // Getting the Form No In case of regular return for connection to Proper Master
                    //strSQL = "SELECT FORM_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                    //strFormNo = dmlService.J_ExecSqlReturnScalar(strSQL).ToString();

                    if (strFormNo == T_FormNo.F24Q)
                    {
                        strSQL = @"SELECT DISTINCT MST_EMPLOYEE.EMPLOYEE_ID,
                                          MST_EMPLOYEE.EMPLOYEE_NAME,
                                          MST_EMPLOYEE.EMPLOYEE_PAN
                                   FROM  TRN_BASIC_INFO,
                                         TRN_DEDUCTEE_DETAILS,
                                         MST_EMPLOYEE
                                   WHERE TRN_BASIC_INFO.BASIC_INFO_ID  = TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID
                                   AND   TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_EMPLOYEE.EMPLOYEE_ID
                                   AND   TRN_BASIC_INFO.BASIC_INFO_ID  = " + lngBasicInfoID + @"
                                   AND   MST_EMPLOYEE.EMPLOYEE_PAN     = 'PANNOTAVBL'
                                   AND   MST_EMPLOYEE.EMPLOYEE_REF     = ''";
                    }
                    else
                    {
                        strSQL = @"SELECT DISTINCT MST_DEDUCTEE.DEDUCTEE_ID,
                                          MST_DEDUCTEE.DEDUCTEE_NAME,
                                          MST_DEDUCTEE.DEDUCTEE_PAN
                                   FROM  TRN_BASIC_INFO,
                                         TRN_DEDUCTEE_DETAILS,
                                         MST_DEDUCTEE
                                   WHERE TRN_BASIC_INFO.BASIC_INFO_ID  = TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID
                                   AND   TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_DEDUCTEE.DEDUCTEE_ID
                                   AND   TRN_BASIC_INFO.BASIC_INFO_ID  = " + lngBasicInfoID + @" 
                                   AND   MST_DEDUCTEE.DEDUCTEE_PAN     = 'PANNOTAVBL'
                                   AND   MST_DEDUCTEE.DEDUCTEE_REF     = ''";
                    }
                }
                else
                {
                    // For Corrections
                    strSQL = @"SELECT DISTINCT BATCH_HEADER_ID,
                                      DEDUCTEE_NAME,
                                      DEDUCTEE_PAN
                               FROM   COR_TRN_DEDUCTEE_DETAILS
                               WHERE  BATCH_HEADER_ID = " + lngBasicInfoID + @" 
                               AND    DEDUCTEE_PAN    = 'PANNOTAVBL'
                               AND    DEDUCTEE_REF    = ''";
                }

                //Clear Grid
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                
                //Add Reference Text column
                dgvDeductees.Columns.Add(RefCol);

                //Select 1st Row
                dgvDeductees.Select();
                dgvDeductees.Rows[0].Cells["Ref"].Selected = true;
            }
            catch(Exception ERR)
            {

            }
        }
        #endregion

        #endregion

        #region ClearFields
        private void ClearFields()
        {
            //lblInvalidNo.Text = "0";
            //lblVerifiedNo.Text = "0";
            //lblNotVerifiedNo.Text = "0"; 
            //btnVerification.Enabled = true;
            //btnVerification.BackColor = Color.Lavender;                
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                string strRefNo = "";
                int iDuplicateValue = 0;
                foreach (DataGridViewRow row in dgvDeductees.Rows)
                {
                    iDuplicateValue = 0;
                    //
                    if (row.Cells["Ref"].Value == null || row.Cells["Ref"].Value.ToString().Trim() == "")
                    {
                        // Message for blank value
                        cmnService.J_UserMessage("Please provide reference no. for '" + row.Cells[1].Value.ToString() + "'.");
                        dgvDeductees.Select();
                        row.Cells["Ref"].Selected = true;
                        return false;
                    }
                    else
                    {
                        strRefNo = row.Cells["Ref"].Value.ToString().Trim();
                        //Checking Duplicate records
                        int intDuplicate = 0;
                        if (strReturnType == T_OTHERMODULENAME.REGULAR_FORM)
                        {
                            // FOR REGULAR RETURN
                            if (strFormNo == T_FormNo.F24Q)
                            {
                                // FOR EMPLOYEES
                                strSQL = "SELECT COUNT(*) " +
                                         "FROM   MST_EMPLOYEE " +
                                         "WHERE  EMPLOYEE_PAN = 'PANNOTAVBL' " +
                                         "AND    EMPLOYEE_REF = '" + strRefNo + "' ";
                                intDuplicate = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));
                            }
                            else
                            {
                                // FOR DEDUCTEES
                                strSQL = "SELECT COUNT(*) " +
                                         "FROM   MST_DEDUCTEE " +
                                         "WHERE  DEDUCTEE_PAN = 'PANNOTAVBL' " +
                                         "AND    DEDUCTEE_REF = '" + strRefNo + "' ";
                                intDuplicate = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));
                            }
                        }
                        else
                        {
                            // FOR CORRECTION RETURN
                            strSQL = "SELECT COUNT(*) " +
                                     "FROM   COR_TRN_DEDUCTEE_DETAILS " +
                                     "WHERE  DEDUCTEE_PAN = 'PANNOTAVBL' " +
                                     "AND    DEDUCTEE_REF = '" + strRefNo + "' " +
                                     "AND    DEDUCTEE_NAME <> '" + row.Cells["Name"].Value.ToString() + "' ";
                            intDuplicate = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));
                        }
                        // MESSAGE FOR DUPLICATE RECORDS
                        if (intDuplicate > 0)
                        {
                            cmnService.J_UserMessage("Reference no. '" + strRefNo + "' already exists.\nPlease provide different Reference No. for this deductee.");
                            dgvDeductees.Select();
                            row.Cells["Ref"].Selected = true;
                            return false;
                        }
                        // MESSAGE FOR <10 CHARACTERS
                        if (strRefNo.Length < 10)
                        {
                            cmnService.J_UserMessage("Deductee reference no. - Should be of 10 characters");
                            dgvDeductees.Select();
                            row.Cells["Ref"].Selected = true;
                            return false;
                        }
                        // CHECK DUPLICATE IN GRID ITSELF
                        #region CHECK DUPLICATE IN GRID ITSELF
                        foreach (DataGridViewRow innerRow in dgvDeductees.Rows)
                        {
                            if (strRefNo == innerRow.Cells["Ref"].Value.ToString().Trim())
                            {
                                iDuplicateValue = iDuplicateValue  +1;
                            }
                            //--
                            if (iDuplicateValue > 1)
                            {
                                cmnService.J_UserMessage("Duplicate Reference no. not allowed.");
                                innerRow.Cells["Ref"].Selected = true;
                                return false;
                            }
                        }
                        #endregion
                        // CHECK SPACE IN BETWEEN
                        if (strRefNo.Trim().Contains(" ") == true)
                        {
                            cmnService.J_UserMessage("Reference no. - space not allowed in between.");
                            row.Cells["Ref"].Selected = true;
                            return false;
                        }

                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        
        #endregion

    }

}