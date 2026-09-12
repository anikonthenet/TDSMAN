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
    public partial class TrnDeleteReturn : Form
    {
       
        ResizeForm _form_resize;
        #region System Generated Code
        public TrnDeleteReturn()
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
        bool blnSelectComboExit = false;
        //
        int intPANId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0;
        int intVerifyId = 0;
        //
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNoBkmark = "";
        string strFAYear = "", strOrderBy = "", strQuery = "" ;
        //
        int j = 0;
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
        string strNOTAVAILABLE = "NOT AVAILABLE";
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

        #region TrnDeleteReturn_Load
        private void TrnDeleteReturn_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            try
            {
                ClearControls(); 
                //--
                LoadControls();
                //--
                btnBack.Enabled = false;
                btnBack.BackColor = Color.LightGray;
                //--
                grpControlSummary.Visible = false;
                pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                grpReturnFilingStatus.Visible = false;
                //--
                cmbFinancialYear.Select();
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

        

        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            //
            GC.Collect(); 
            //
            blnVerificationComplete = false; 
            //--
            this.Dispose();
            this.Close();
        }
        #endregion

        #region btnNext_Click
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNext.Text == "&Next")
            {
                if (rbnRegularReturn.Checked == true)
                {
                    btnNext.Text = "&Delete";
                    btnNext.Tag = "";
                    tbcDeleteReturn.SelectTab(tbpDeleteReturn);
                    lblSteps.Text = "Step 2 of 2";
                    btnBack.Enabled = true;
                    btnBack.BackColor = Color.Lavender;
                    if (lngBasicInfoID == 0)
                    {
                        btnNext.BackColor = Color.LightGray;
                        btnNext.Enabled = false;
                    }
                    else
                    {
                        btnNext.BackColor = Color.Lavender;
                        btnNext.Enabled = true;
                    }
                    //
                    cmbFinancialYear.Select();
                }
                else
                {
                    btnNext.Text = "&Delete";
                    btnNext.Tag = T_MODULENAME.CORRECTION_RETURN;
                    tbcDeleteReturn.SelectTab(tbpDeleteReturnCorrection);
                    lblSteps.Text = "Step 2 of 2";
                    btnBack.Enabled = true;
                    btnBack.BackColor = Color.Lavender;
                    //-- LOAD CORRECTION GRID...
                    LoadBatch();
                }
            }
            else
            {
                if (btnNext.Tag == "")
                {
                    if (lngBasicInfoID == 0)
                    {
                        cmnService.J_UserMessage("No return found !!!");
                        return;
                    }
                    //--
                    if (chkDeleteMasterOnly.Checked == true)
                    {
                        if (cmnService.J_UserMessage("FINANCIAL YEAR \t: " + cmbFinancialYear.Text + " \n " +
                                       "QUARTER \t: " + cmbQuarter.Text + " \n " +
                                       "FORM NO. \t: " + cmbFormNo.Text + " \n " +
                                       "COMPANY  \t: " + cmbCompany.Text + " \n " +
                                       "Are you sure you want to delete data of the following return (master data also)??", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    else
                    {
                        if (cmnService.J_UserMessage("FINANCIAL YEAR \t: " + cmbFinancialYear.Text + " \n " +
                                    "QUARTER \t: " + cmbQuarter.Text + " \n " +
                                    "FORM NO. \t: " + cmbFormNo.Text + " \n " +
                                    "COMPANY  \t: " + cmbCompany.Text + " \n " +
                                    "Are you sure you want to delete data of the following return ??", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    //--
                    this.Cursor = Cursors.WaitCursor;
                    //
                    if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    {
                        #region AUTO BACKUP
                        //if (ChequePrinting.CheckAdminUser() == true)
                        //{
                        //-- AUTO BACKUP MECHANISM
                        //
                        //string strBkUpFileName = "DELETION_ACCOUNTS-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".MDB";
                        string strBkUpFileName = "DEL_" + cmbFinancialYear.Text + "-" + cmbQuarter.Text + "-" + cmbFormNo.Text + "-" + cmnService.J_Right(cmbCompany.Text, 12) + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + "";
                        strSQL = "INSERT INTO TRN_BACKUP (" +
                                        "            BACKUP_DATE," +
                                        "            BACKUP_TIME," +
                                        "            BACKUP_PATH," +
                                        "            BACKUP_FILE_NAME," +
                                        "            BACKUP_NOTES) " +
                                        "VALUES (" +
                                        //"            " + cmnService.J_DateOperator() + string.Format("{0:dd/MM/yyyy}", System.DateTime.Now.Date) + cmnService.J_DateOperator() + "," +
                                        //-- ANIK @ 2015/09/18 
                                        "            " + cmnService.J_DateOperator() + string.Format("{0:MM/dd/yyyy}", System.DateTime.Now.Date) + cmnService.J_DateOperator() + "," +
                                        //-- COMMENTED BY ANIK ON 2017/01/06 FOR VS 2010
                                        //"           '" + string.Format("{0:hh:mm:ss}", System.DateTime.Now.TimeOfDay) + "'," +
                                        "           '" + string.Format("{0:hh:mm:ss}", System.DateTime.Now) + "'," +
                                        "           '" + Application.StartupPath + "'," +
                                        "           '" + strBkUpFileName + "'," +
                                        "           'RETURN DELETION')";

                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        //
                        File.Copy(Path.Combine(Application.StartupPath, J_Var.J_pMsAccessDatabaseName), Path.Combine(Application.StartupPath, strBkUpFileName), true);
                        //}
                        //
                        #endregion
                    }
                    //-- 
                    #region DELETION
                    dmlService.J_BeginTransaction();
                    //
                    string strFormNo = cmbFormNo.Text.Substring(cmbFormNo.Text.IndexOf('(') + 1, cmbFormNo.Text.IndexOf(')') - cmbFormNo.Text.IndexOf('(') - 1);
                    strSQL = "DELETE FROM TRN_COMPANY_INFO WHERE BASIC_INFO_ID =  " + lngBasicInfoID + "";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    #region Delete Master - Get IDs
                    IDataReader drdGetPartyID = null;
                    string strMasterTable = ""; int i = 0; string[] arrPartyID = null; string[] arrPartyID24Q = null;
                    if (chkDeleteMasterOnly.Checked == true)
                    {
                        if (strFormNo == T_FormNo.F24Q)
                            strMasterTable = "EMPLOYEE";
                        else
                            strMasterTable = "DEDUCTEE";
                        //-- GET PARTY_ID
                        strSQL = @"SELECT COUNT(PARTY_ID) FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID =  " + lngBasicInfoID + "";
                        arrPartyID = new String[cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)))];
                        //string[] arrPartyID = null;
                        strSQL = @"SELECT DISTINCT PARTY_ID FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID =  " + lngBasicInfoID + "";
                        drdGetPartyID = dmlService.J_ExecSqlReturnReader(strSQL);
                        if (drdGetPartyID == null)
                        {
                        }
                        while (drdGetPartyID.Read())
                        {
                            arrPartyID[i] = Convert.ToString(drdGetPartyID["PARTY_ID"]);
                            i++;
                        }
                        drdGetPartyID.Close();
                        drdGetPartyID.Dispose();
                        //////
                        ////for (int j = 0; j < arrPartyID.Length; j++)
                        ////{
                        ////    if (arrPartyID[j] != null)
                        ////    {
                        ////        if (cmbFormNo.Text == T_FormNo.F24Q)
                        ////            strSQL = @"DELETE FROM MST_" + strMasterTable + " WHERE " + strMasterTable + "_ID = " + arrPartyID[j] + " AND " + strMasterTable + "_ID NOT IN (SELECT PARTY_ID FROM TRN_DEDUCTEE_DETAILS) AND NOT " + strMasterTable + "_ID NOT IN (SELECT EMPLOYEE_ID FROM TRN_SALARY_DETAILS) ";
                        ////        else
                        ////            strSQL = @"DELETE FROM MST_" + strMasterTable + " WHERE " + strMasterTable + "_ID = " + arrPartyID[j] + " AND " + strMasterTable + "_ID NOT IN (SELECT PARTY_ID FROM TRN_DEDUCTEE_DETAILS)";
                        ////        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        ////        {
                        ////            this.Cursor = Cursors.Default;
                        ////            dmlService.J_Rollback();
                        ////            return;
                        ////        }
                        ////    }
                        ////}
                        //-- FOR SALARY DETAILS
                        //-- GET PARTY_ID
                        //drdGetPartyID = null; i = 0;
                        //strSQL = @"SELECT COUNT(EMPLOYEE_ID) FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID =  " + lngBasicInfoID + "";
                        //arrPartyID24Q = new String[cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)))];
                        ////
                        //strSQL = @"SELECT DISTINCT EMPLOYEE_ID FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID =  " + lngBasicInfoID + "";
                        //drdGetPartyID = dmlService.J_ExecSqlReturnReader(strSQL);
                        //if (drdGetPartyID == null)
                        //{
                        //}
                        //while (drdGetPartyID.Read())
                        //{
                        //    arrPartyID24Q[i] = Convert.ToString(drdGetPartyID["EMPLOYEE_ID"]);
                        //    i++;
                        //}
                        //drdGetPartyID.Close();
                        //drdGetPartyID.Dispose();
                        ////
                        //for (int j = 0; j < arrPartyID24Q.Length; j++)
                        //{
                        //    if (arrPartyID24Q[j] != null)
                        //    {
                        //        strSQL = @"DELETE FROM MST_" + strMasterTable + " WHERE " + strMasterTable + "_ID = " + arrPartyID24Q[j] + " AND " + strMasterTable + "_ID NOT IN (SELECT PARTY_ID FROM TRN_DEDUCTEE_DETAILS) AND NOT " + strMasterTable + "_ID NOT IN (SELECT EMPLOYEE_ID FROM TRN_SALARY_DETAILS) ";
                        //        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        //        {
                        //            this.Cursor = Cursors.Default;
                        //            dmlService.J_Rollback();
                        //            return;
                        //        }
                        //    }
                        //}
                    }
                    #endregion
                    //--
                    strSQL = "DELETE FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID =  " + lngBasicInfoID + "";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    #region Delete Master - Deletion
                    if (chkDeleteMasterOnly.Checked == true)
                    {
                        for (int j = 0; j < arrPartyID.Length; j++)
                        {
                            if (arrPartyID[j] != null)
                            {
                                if (strFormNo == T_FormNo.F24Q)
                                    strSQL = @"DELETE FROM MST_" + strMasterTable + " WHERE " + strMasterTable + "_ID = " + arrPartyID[j] + " AND " + strMasterTable + "_ID NOT IN (SELECT PARTY_ID FROM TRN_DEDUCTEE_DETAILS) AND " + strMasterTable + "_ID NOT IN (SELECT EMPLOYEE_ID FROM TRN_SALARY_DETAILS) ";
                                else
                                    strSQL = @"DELETE FROM MST_" + strMasterTable + " WHERE " + strMasterTable + "_ID = " + arrPartyID[j] + " AND " + strMasterTable + "_ID NOT IN (SELECT PARTY_ID FROM TRN_DEDUCTEE_DETAILS)";
                                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                {
                                    this.Cursor = Cursors.Default;
                                    dmlService.J_Rollback();
                                    return;
                                }
                            }
                        }
                    }
                    #endregion
                    //
                    strSQL = "DELETE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID + "";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    #region Delete Master - Get Ids - SD
                    if (chkDeleteMasterOnly.Checked == true)
                    {
                        //string strMasterTable = "";
                        if (strFormNo == T_FormNo.F24Q && cmbQuarter.Text == T_Qtr.Q4)
                        {
                            strMasterTable = "EMPLOYEE";
                            //else
                            //    strMasterTable = "DEDUCTEE";
                            //-- GET PARTY_ID
                            drdGetPartyID = null; i = 0; arrPartyID24Q = null;
                            //-- FOR SALARY DETAILS
                            //-- GET PARTY_ID
                            drdGetPartyID = null; i = 0;
                            strSQL = @"SELECT COUNT(EMPLOYEE_ID) FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID =  " + lngBasicInfoID + "";
                            arrPartyID24Q = new String[cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)))];
                            //
                            strSQL = @"SELECT DISTINCT EMPLOYEE_ID FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID =  " + lngBasicInfoID + "";
                            drdGetPartyID = dmlService.J_ExecSqlReturnReader(strSQL);
                            if (drdGetPartyID == null)
                            {
                            }
                            while (drdGetPartyID.Read())
                            {
                                arrPartyID24Q[i] = Convert.ToString(drdGetPartyID["EMPLOYEE_ID"]);
                                i++;
                            }
                            drdGetPartyID.Close();
                            drdGetPartyID.Dispose();
                            //////
                            ////for (int j = 0; j < arrPartyID24Q.Length; j++)
                            ////{
                            ////    if (arrPartyID24Q[j] != null)
                            ////    {
                            ////        strSQL = @"DELETE FROM MST_" + strMasterTable + " WHERE " + strMasterTable + "_ID = " + arrPartyID24Q[j] + " AND " + strMasterTable + "_ID NOT IN (SELECT PARTY_ID FROM TRN_DEDUCTEE_DETAILS)"; ;
                            ////        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            ////        {
                            ////            this.Cursor = Cursors.Default;
                            ////            dmlService.J_Rollback();
                            ////            return;
                            ////        }
                            ////    }
                            ////}
                        }
                    }
                    #endregion
                    //--
                    strSQL = "DELETE FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID + "";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    #region Delete Master - Deletion - SD
                    //
                    if (chkDeleteMasterOnly.Checked == true)
                    {
                        if (strFormNo == T_FormNo.F24Q && cmbQuarter.Text == T_Qtr.Q4)
                        {
                            for (int j = 0; j < arrPartyID24Q.Length; j++)
                            {
                                if (arrPartyID24Q[j] != null)
                                {
                                    strSQL = @"DELETE FROM MST_" + strMasterTable + " WHERE " + strMasterTable + "_ID = " + arrPartyID24Q[j] + " AND " + strMasterTable + "_ID NOT IN (SELECT PARTY_ID FROM TRN_DEDUCTEE_DETAILS) AND " + strMasterTable + "_ID NOT IN (SELECT EMPLOYEE_ID FROM TRN_SALARY_DETAILS) ";
                                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                    {
                                        this.Cursor = Cursors.Default;
                                        dmlService.J_Rollback();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    //
                    strSQL = "DELETE FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + lngBasicInfoID + "";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    strSQL = "DELETE FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID =  " + lngBasicInfoID + "";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    dmlService.J_Commit();
                    //
                    #endregion
                    //--
                    ClearControls();
                    //
                    LoadControls();
                    //--
                    this.Cursor = Cursors.Default;
                    //
                    cmnService.J_UserMessage("Deletion Completed");
                    //--
                    this.Close();
                    this.Dispose();
                    //
                }
                else if(btnNext.Tag == T_MODULENAME.CORRECTION_RETURN)
                {
                    if (grdvCorrectionBatch.RowCount <= 0)
                    {
                        cmnService.J_UserMessage("No return found");
                        return;
                    }
                    //--
                    if (J_GenerateDataGridViewSelectedId(grdvCorrectionBatch,1) =="")
                    {
                        cmnService.J_UserMessage("Please select a record");
                        return;
                    }

                    string strBatchIds = "";
                    //
                    foreach (DataGridViewRow row in grdvCorrectionBatch.Rows)
                    {
                        if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                        {
                            if (row.Cells[1].Value.ToString() != "")
                            {
                                strBatchIds = strBatchIds + row.Cells[1].Value.ToString() + ",";
                            }
                        }
                    }
                    //
                    strBatchIds = cmnService.J_Mid(strBatchIds, 0, strBatchIds.Length - 1);
                    //
                    if (strBatchIds.Contains(",") == true )
                    {
                        if (cmnService.J_UserMessage("Are you sure you want to delete these correction batches ??", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    else
                    {
                        if (cmnService.J_UserMessage("Are you sure you want to delete this correction batch ??", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    //--
                    this.Cursor = Cursors.WaitCursor;
                    //--
                    if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    {
                        #region AUTO BACKUP
                        //if (ChequePrinting.CheckAdminUser() == true)
                        //{
                        //-- AUTO BACKUP MECHANISM
                        //
                        //string strBkUpFileName = "DELETION_ACCOUNTS-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".MDB";
                        string strBkUpFileName = "DEL_CORRECTION_BATCH-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + "";
                        strSQL = "INSERT INTO TRN_BACKUP (" +
                                        "            BACKUP_DATE," +
                                        "            BACKUP_TIME," +
                                        "            BACKUP_PATH," +
                                        "            BACKUP_FILE_NAME," +
                                        "            BACKUP_NOTES) " +
                                        "VALUES (" +
                                        //"            " + cmnService.J_DateOperator() + string.Format("{0:dd/MM/yyyy}", System.DateTime.Now.Date) + cmnService.J_DateOperator() + "," +
                                        //-- ANIK @ 2015/09/18 
                                        "            " + cmnService.J_DateOperator() + string.Format("{0:MM/dd/yyyy}", System.DateTime.Now.Date) + cmnService.J_DateOperator() + "," +
                                        //-- COMMENTED BY ANIK ON 2017/01/06 FOR VS 2010
                                        //"           '" + string.Format("{0:hh:mm:ss}", System.DateTime.Now.TimeOfDay) + "'," +
                                        "           '" + string.Format("{0:hh:mm:ss}", System.DateTime.Now) + "'," +
                                        "           '" + Application.StartupPath + "'," +
                                        "           '" + strBkUpFileName + "'," +
                                        "           'RETURN DELETION')";

                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        //
                        File.Copy(Path.Combine(Application.StartupPath, J_Var.J_pMsAccessDatabaseName), Path.Combine(Application.StartupPath, strBkUpFileName), true);
                        //}
                        //
                        #endregion
                    }
                    //-- 
                    #region DELETION
                    dmlService.J_BeginTransaction();
                    //
                    strSQL = "DELETE FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID IN (" + strBatchIds + ")";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    strSQL = "DELETE FROM COR_HDR_CHALLAN WHERE BATCH_HEADER_ID IN (" + strBatchIds + ")";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    strSQL = "DELETE FROM COR_HDR_COMPANY WHERE BATCH_HEADER_ID IN (" + strBatchIds + ")";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    strSQL = "DELETE FROM COR_HDR_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID IN (" + strBatchIds + ")";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    strSQL = "DELETE FROM COR_HDR_SALARY_DETAILS WHERE BATCH_HEADER_ID IN (" + strBatchIds + ")";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    strSQL = "DELETE FROM COR_TRN_CHALLAN WHERE BATCH_HEADER_ID IN (" + strBatchIds + ")";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    strSQL = "DELETE FROM COR_TRN_COMPANY WHERE BATCH_HEADER_ID IN (" + strBatchIds + ")";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    strSQL = "DELETE FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID IN (" + strBatchIds + ")";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    strSQL = "DELETE FROM COR_TRN_SALARY_DETAILS WHERE BATCH_HEADER_ID IN (" + strBatchIds + ")";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    {
                        this.Cursor = Cursors.Default;
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    dmlService.J_Commit();
                    //
                    #endregion
                    //
                    LoadBatch();
                    //--
                    this.Cursor = Cursors.Default;
                    //
                    cmnService.J_UserMessage("Deletion Completed");
                    //--
                    this.Close();
                    this.Dispose();
                    //
                }
            }
        }
        #endregion

        #region btnBack_Click
        private void btnBack_Click(object sender, EventArgs e)
        {
            btnNext.Text = "&Next";
            tbcDeleteReturn.SelectTab(tbpWarning);
            lblSteps.Text = "Step 1 of 2";
            btnBack.Enabled = false;
            btnBack.BackColor = Color.LightGray;
            btnNext.Enabled = true; 
            btnNext.BackColor = Color.Lavender;                    
        }
        #endregion

        #region cmbMain_SelectedIndexChanged
        private void cmbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnSelectComboExit == true)
                return;
            //
            ClearControls();
            //
            string strFormNo = "";
            lngBasicInfoID = 0;
            //
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                grpControlSummary.Visible = false;
                pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false; 
                lblTotalRecords.Visible = false;
                btnNext.Enabled = false; 
                btnNext.BackColor = Color.LightGray;                    
                return;
            }
            else if (cmbFinancialYear.SelectedIndex > 0)
            {
                //
                long lngTotalRecords = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS, TRN_BASIC_INFO WHERE TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID AND TRN_BASIC_INFO.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), J_QueryType.DirectQuery);
                //
                //if (lngTotalRecords > 0)
                //{
                    lblTotalRecords.Visible = true;
                    lblTotalRecords.Text = "Total Records for FA year [" + cmbFinancialYear.Text + "] : " + Convert.ToString(lngTotalRecords);
                //}
                //else
                //    lblTotalRecords.Visible = false;
            }
            //--
            if (cmbCompany.SelectedIndex <= 0)
            {
                grpControlSummary.Visible = false;
                pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.Enabled = false;
                btnNext.BackColor = Color.LightGray; 
                return;
            }
            if (cmbFormNo.Text == "")
            {
                grpControlSummary.Visible = false;
                pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.Enabled = false;
                btnNext.BackColor = Color.LightGray; 
                return;
            }
            else
            {
                strFormNo = cmbFormNo.Text.Substring(cmbFormNo.Text.IndexOf('(') + 1, cmbFormNo.Text.IndexOf(')') - cmbFormNo.Text.IndexOf('(') - 1);
                if (strFormNo == T_FormNo.F24Q)
                    chkDeleteMasterOnly.Text = "Delete Employee(s) from Master.";
                else
                    chkDeleteMasterOnly.Text = "Delete Deductee(s) from Master.";
            }
            //
            if (cmbQuarter.Text == "")
            {
                grpControlSummary.Visible = false;
                pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                grpReturnFilingStatus.Visible = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.Enabled = false;
                btnNext.BackColor = Color.LightGray; 
                return;
            }
            //--
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        cmbQuarter.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        strFormNo);
            //if (lngBasicInfoID == 0)
            //{
            //    lblHideTabs.Text = "No records found for the selected return";
            //    return;
            //}
            
            //--
            ControlSummaryBasicInfo(lngBasicInfoID);
            ReturnFilingStatus(lngBasicInfoID);
            //-- 
        }
        #endregion

        #endregion

        #region User Define Functions

        #region ClearControls
        private void ClearControls()
        {            
            //grpButtons.Enabled = true;
            //
            txtTotalChallanRecords.Text = "";
            txtTotalDeducteeRecords.Text = "";
            txtTotalChallanAmount.Text = "";
            txtTotalDeducteeTDS.Text = "";
            txtAmountPaid.Text = "";
            //
            txtReceiptNo.Text = "";
            txtDateofFiling.Text = "";
            txtTokenNo.Text = "";
        }
        #endregion

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
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //-- QUARTER
            //-----------
            string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter);
            //-----------
            //-- FORM NO
            //-----------
            //string[] strFormNo1 ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            string[] strFormNo1 = { T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")", T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")", T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")", T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")" };
            dmlService.J_PopulateComboBox(strFormNo1, ref cmbFormNo);
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

        #region ControlSummaryBasicInfo
        private void ControlSummaryBasicInfo(long BasicInfoID)
        {
            if (BasicInfoID == 0)
            {
                txtTotalChallanRecords.Text = "";
                txtTotalDeducteeRecords.Text = "";
                txtTotalChallanAmount.Text = "";
                txtTotalDeducteeTDS.Text = "";
                txtAmountPaid.Text = "";
                grpControlSummary.Visible = false;
                pnlLine1.Visible = false;
                pnlLine2.Visible = false;
                btnNext.Enabled = false;
                lblTotalRecordsReturn.Visible = false;
                btnNext.BackColor = Color.LightGray; 
                return;
            }
            grpControlSummary.Visible = true;
            pnlLine1.Visible = true;
            pnlLine2.Visible = true;
            //lblTotalRecordsReturn.Visible = true;
            btnNext.Enabled = true;
            btnNext.BackColor = Color.Lavender; 
            txtTotalChallanRecords.Text = "0";
            txtTotalDeducteeRecords.Text = "0";
            txtTotalChallanAmount.Text = "0.00";
            txtTotalDeducteeTDS.Text = "0.00";
            txtAmountPaid.Text = "0.00";

            // CONTROL SUMMARY VALUES
            txtTotalChallanRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(CHALLAN_ID) AS COUNT_CHALLAN_ID FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID));
            txtTotalDeducteeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID));
            txtTotalChallanAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID))));
            txtTotalDeducteeTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
            txtAmountPaid.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
            //
            long lngTotalRecordsReturn = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID, J_QueryType.DirectQuery);
            lblTotalRecordsReturn.Text = "Total Records in this return : " + Convert.ToString(lngTotalRecordsReturn);
            //
            string strFormNo = cmbFormNo.Text.Substring(cmbFormNo.Text.IndexOf('(') + 1, cmbFormNo.Text.IndexOf(')') - cmbFormNo.Text.IndexOf('(') - 1);
            if (strFormNo == T_FormNo.F24Q && cmbQuarter.Text == T_Qtr.Q4)
            {
                lblNetTaxableIncome.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID)) == "" ? "0.00" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID))));
                lblNetTaxableIncome.Visible = true;
                lblNetTaxableIncomeCaption.Visible = true;
            }
            else
            {
                lblNetTaxableIncome.Visible = false;
                lblNetTaxableIncomeCaption.Visible = false;
            }
            //if (Convert.ToDouble(txtTotalChallanRecords.Text) > 0 && txtTotalChallanRecords.Visible == true)
            //{
            //    BtnCancel.Enabled = true;
            //    BtnCancel.BackColor = Color.Lavender;
            //}
            //else
            //{
            //    BtnCancel.Enabled = false;
            //    BtnCancel.BackColor = Color.LightGray;
            //}
        }
        #endregion

        #region ReturnFilingStatus
        private void ReturnFilingStatus(long BasicInfoId)
        {
            IDataReader drdShowRecord = null;
            //
            try
            {
                txtReceiptNo.Text = "";
                txtDateofFiling.Text = "";
                txtTokenNo.Text = "";                    
                //  
                if (BasicInfoId == 0)
                {
                    grpReturnFilingStatus.Visible = false;
                    pnlLine2.Visible = false;               
                    //
                    return;
                }
                grpReturnFilingStatus.Visible = true;
                pnlLine2.Visible = true; 
                // CHECK IF CHALLAN EXISTS
                strSQL = "SELECT RECEIPT_NO," +
                    "            " + cmnService.J_SQLDBFormat("DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DATE_OF_FILING," +
                    "            PRN_NO," +
                    "            PREV_FILED," +
                    "            PREV_PRN_NO " +
                    "     FROM   TRN_BASIC_INFO " +
                    "     WHERE  BASIC_INFO_ID = " + BasicInfoId;
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                    return;
                //
                while (drdShowRecord.Read())
                {
                    //
                    txtReceiptNo.Text = Convert.ToString(drdShowRecord["RECEIPT_NO"]);
                    txtDateofFiling.Text = Convert.ToString(drdShowRecord["DATE_OF_FILING"]);
                    txtTokenNo.Text = Convert.ToString(drdShowRecord["PRN_NO"]);                    
                    //--
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    return;
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }


        #endregion

        #region chkNewSelectDeselect_CheckedChanged
        private void chkNewSelectDeselect_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (grdvCorrectionBatch.Visible == false) { chkNewSelectDeselect.Checked = false; return; }
                //if (blnSelectDeselect == false) return;
                //--
                this.Cursor = Cursors.WaitCursor;
                //blnDeleteTempGridRecord = true;
                foreach (DataGridViewRow row in grdvCorrectionBatch.Rows)
                {
                    if (chkNewSelectDeselect.Checked == true)//checked all checkbox
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
                            //blnExitGrid = false;
                            row.Cells[0].Value = false;
                            //blnExitGrid = false;
                        }
                        //strSelectedLabel = "";
                    }
                }
                //blnDeleteTempGridRecord = false;
                
                //
                this.Cursor = Cursors.Default;
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        private void chkDeleteMasterOnly_MouseMove(object sender, MouseEventArgs e)
        {

        }

        //-- Added By Abhishek Dey On 22/05/2018 --
        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnDeleteReturn");
            Tan.ShowDialog();
            //--------------
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0113", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        //-----------------------------------------
        #region LoadBatch
        private void LoadBatch()
        {
            //-----------------------------------------------------------
            string[,] strMatrixBatchGrid = {{"BATCH_HEADER_ID", "0", "", "", "", "F", ""},
                                        {"FA Year", "70", "S", "", "", "", "T"},
                                        {"Form No", "75", "S", "", "", "", "T"},
                                        {"Qtr", "35", "", "", "", "", "T"},
                                        {"Company Name", "150", "", "", "", "", "T"},
                                        {"TAN No.", "90", "", "", "", "", "T"},
                                        {"Imported date & Time", "120", "dd/MM/yyyy", "", "", "", "T"},
                                        {"Total Corrections", "90", "", "Right", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrixBatchGrid = strMatrixBatch;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------

            string[,] strLoadCorrectionMatrix = {{"COR_TRN_COMPANY.MODE = '" + T_CorrectionMode.TANUpdation + "'", "F", "Cancelled", "T"},
                                                 {"JAYA", "F", "COR_HDR_BATCH.TOTAL_CORRECTION", "F"}};

            strOrderBy = "COR_HDR_BATCH.BATCH_HEADER_ID DESC";
            strQuery = "SELECT COR_HDR_BATCH.BATCH_HEADER_ID  AS BATCH_HEADER_ID," +
                      "        MST_ASSESSMENT.FA_YEAR         AS FA_YEAR," +
                      "        COR_HDR_BATCH.FORM_NO          AS FORM_NO," +
                      "        COR_HDR_BATCH.QTR              AS QTR," +
                      "        COR_TRN_COMPANY.COMPANY_NAME   AS COMPANY_NAME," +
                      "        COR_TRN_COMPANY.TAN_NO         AS TAN_NO," +
                      //"        FORMAT(COR_HDR_BATCH.IMPORTED_DATE, \"dd/MM/yyyy h:m AMPM\")   AS IMPORTED_DATE," +
                      "        " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.IMPORTED_DATE", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSS) + " AS IMPORTED_DATE, " +
                      "        " + cmnService.J_SQLDBFormat(strLoadCorrectionMatrix, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS TOTAL_CORRECTION " +
                      "FROM    COR_HDR_BATCH," +
                      "        MST_ASSESSMENT," +
                      "        COR_TRN_COMPANY " +
                      "WHERE   COR_HDR_BATCH.ASST_ID            = MST_ASSESSMENT.ASST_ID " +
                      "AND     COR_HDR_BATCH.BATCH_HEADER_ID    = COR_TRN_COMPANY.BATCH_HEADER_ID ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            TdsMan.PopulateGridView(grdvCorrectionBatch, dmlService.J_pCommand, strSQL, strMatrixBatchGrid);
            _form_resize._resize();
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

        #endregion

    }

}