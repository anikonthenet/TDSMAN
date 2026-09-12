

#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using TDSMAN.Classes;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormSys
{
    public partial class SysBackupCompanyWise : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public SysBackupCompanyWise()
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

        //--
        string strFolderPath = "";
        //
        DMLService dmlService = new DMLService();
        DateService dtService = new DateService();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //--            
        ToolTip tllTip = new ToolTip();
        //--            
        string strSQL = "";
        long lngRegCompanyID = 0;
        string strCorCompanyID = "";
        long lngWaitMessage = 0;
        string strFullBackup = "FULL BACKUP";
        //----
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        #endregion

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

        #region SysBackup_Load
        private void SysBackup_Load(object sender, EventArgs e)
        {
            IDataReader drdLoadCombo = null;
            try
            {
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //--
                lblTitle.Text = "Backup";
                lblCompanyName.Text = strFullBackup;
                //
                tmrWaitMessage.Stop();
                //--
                mskDate.Text = string.Format("{0:dd/MM/yyyy}", System.DateTime.Now.Date);
                //-- COMMENTED BY ANIK ON 2017/01/06 FOR VS 2010
                //txtTime.Text = string.Format("{0:hh:mm:ss}", System.DateTime.Now.TimeOfDay);
                txtTime.Text = string.Format("{0:hh:mm:ss}", System.DateTime.Now);
                txtPath.Text = Application.StartupPath;
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    txtFileName.Text = J_Var.J_pMsAccessDatabaseName.Substring(0, 6) + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".BAK";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    txtFileName.Text = J_Var.J_pMsAccessDatabaseName.Substring(0, 6) + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".MDB";
                //--
                txtNotes.Text = "";
                //----------
                string strDatabasePath = "";
                TdsMan.GetDatabasePathExists(out strDatabasePath);
                if (System.IO.Directory.Exists(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder)) == false)
                {
                    chkReceiptImages.Visible = false;
                }
                else
                {
                    if(IsDirectoryEmpty(new DirectoryInfo(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder))) == true)
                        chkReceiptImages.Visible = false;
                }
                //----------
//                strSQL = @" SELECT COMPANY_ID,
//                                   TAN_NO + ' - ' + COMPANY_NAME                            
//                            FROM   MST_COMPANY
                //                            ORDER BY TAN_NO";
                //                strSQL = @"SELECT TAN_NO + ' - ' + COMPANY_NAME AS TAN
                //                        FROM   MST_COMPANY                                
                //                        UNION 
                //                        SELECT DISTINCT TAN_NO + ' - ' + COMPANY_NAME AS TAN
                //                        FROM   COR_HDR_COMPANY";
                if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    strSQL = @"SELECT DISTINCT TAN FROM (
                           SELECT TAN_NO AS TAN
                           FROM   MST_COMPANY                                
                           UNION 
                           SELECT DISTINCT TAN_NO AS TAN
                           FROM   COR_HDR_COMPANY)
                           ORDER BY TAN ";

                    drdLoadCombo = dmlService.J_ExecSqlReturnReader(strSQL);
                    if (drdLoadCombo == null)
                        return;
                    //
                    cmbSelectTAN.Items.Clear();
                    cmbSelectTAN.Items.Add("");
                    while (drdLoadCombo.Read())
                    {
                        cmbSelectTAN.Items.Add(Convert.ToString(drdLoadCombo["TAN"]));
                    }
                    drdLoadCombo.Close();
                    drdLoadCombo.Dispose();
                    //if (dmlService.J_PopulateComboBox(strSQL, ref  cmbSelectTAN) == false) return;
                }
                //-----------
                cmbSelectTAN.Select();
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region btnChangePath_Click
        private void btnChangePath_Click(object sender, EventArgs e)
        {
            strFolderPath = cmnService.J_OpenFolderDialog();
            if (strFolderPath == "")
            {
                MessageBox.Show("Select Folder to Save Backup File", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnExit.Select();
                return;
            }
            txtPath.Text = strFolderPath;
        }
        #endregion

        #region cmbSelectTAN_SelectedIndexChanged
        private void cmbSelectTAN_SelectedIndexChanged(object sender, EventArgs e)
        {
            lngRegCompanyID = 0;
            strCorCompanyID = "";
            //--
            if (cmbSelectTAN.SelectedIndex <= 0)
            {
                //txtFileName.Text = "";
                lblCompanyName.Text = strFullBackup;
                txtFileName.Text = J_Var.J_pMsAccessDatabaseName.Substring(0, 6) + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" +  string.Format("{0:HHmmss}", System.DateTime.Now) + ".MDB";
                return;
            }
            //--
            txtFileName.Text = J_Var.J_pMsAccessDatabaseName.Substring(0, 6) + " - TAN " + cmnService.J_Left(cmbSelectTAN.Text.Trim(),10) + " - " + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".MDB";
            //
            lblCompanyName.Text =Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_NAME FROM MST_COMPANY WHERE TAN_NO ='" + cmbSelectTAN.Text + "'"));
            //
            if (lblCompanyName.Text == "")
                lblCompanyName.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_NAME FROM COR_HDR_COMPANY WHERE TAN_NO ='" + cmbSelectTAN.Text + "'"));
            //else
            //{
            //    lngRegCompanyID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_ID FROM MST_COMPANY WHERE TAN_NO ='" + cmbSelectTAN.Text + "'")));
            //}
            //
            //if (lblCompanyName.Text != "")
            //    lblCompanyName.Visible = true;
        }
        #endregion

        #region BtnBackup_Click
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            IDataReader drdLoadCorCompanyID = null;
            try
            {
                if (txtFileName.Text == "")
                {
                    cmnService.J_UserMessage("File Name can not be Blank !!");
                    return;
                }
                //--
                if (cmbSelectTAN.SelectedIndex > 0)
                {
                    if (cmnService.J_UserMessage("Backup will contain data of Company : " + cmbSelectTAN.Text + " - " + lblCompanyName.Text + "\nProceed ??", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
                else
                {
                    if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }

                //--
                //tmrWaitMessage.Enabled = true;
                //tmrWaitMessage.Start();
                //
                string strSQL = "INSERT INTO TRN_BACKUP (" +
                                "            BACKUP_DATE," +
                                "            BACKUP_TIME," +
                                "            BACKUP_PATH," +
                                "            BACKUP_FILE_NAME," +
                                "            BACKUP_NOTES) " +
                                "VALUES (" +
                                "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDate.Text) + cmnService.J_DateOperator() + "," +
                                "           '" + txtTime.Text + "'," +
                                "           '" + txtPath.Text + "'," +
                                "           '" + txtFileName.Text + "'," +
                                "           '" + txtNotes.Text + "')";
                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                    return;
                //--
                #region COMPANY-WISE BKUP
                if (cmbSelectTAN.SelectedIndex > 0)
                {
                    //-- GET REGULAR COMPANY ID
                    lngRegCompanyID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_ID FROM MST_COMPANY WHERE TAN_NO ='" + cmbSelectTAN.Text + "'")));
                    //-- GET CORRECTION COMPANY ID(s)
                    strSQL = "SELECT HDR_COMPANY_ID FROM COR_HDR_COMPANY WHERE TAN_NO ='" + cmbSelectTAN.Text + "'";
                    drdLoadCorCompanyID = dmlService.J_ExecSqlReturnReader(strSQL);
                    if (drdLoadCorCompanyID == null)
                        return;
                    //
                    while (drdLoadCorCompanyID.Read())
                    {
                        if (strCorCompanyID == "")
                            strCorCompanyID = Convert.ToString(drdLoadCorCompanyID["HDR_COMPANY_ID"]);
                        else
                            strCorCompanyID = strCorCompanyID + "," + Convert.ToString(drdLoadCorCompanyID["HDR_COMPANY_ID"]);
                    }
                    drdLoadCorCompanyID.Close();
                    drdLoadCorCompanyID.Dispose();
                    //--
                    //lngCorCompanyID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar()));            
                    //cmnService.J_UserMessage("INSERT INTO TRN_BACKUP");
                    //----------------------------
                    this.Cursor = Cursors.WaitCursor;
                    //
                    //System.Threading.Thread.Sleep(5000);
                    //
                    //TdsMan.T_CompactAccessDB(Application.StartupPath, txtFileName.Text);
                    File.Copy(Path.Combine(Application.StartupPath, J_Var.J_pMsAccessDatabaseName), Path.Combine(Application.StartupPath, "TEMP.MDB"), true);
                    J_Var.J_pMsAccessDatabaseName = "TEMP.MDB";
                    //-- DELETION AS PER COMPANY SELECTION
                    //long lngCompanyID = Convert.ToInt64(Support.GetItemData(cmbSelectTAN, cmbSelectTAN.SelectedIndex));
                    long lngCompanyID = lngRegCompanyID;
                    //--
                    dmlService.Dispose();
                    //--
                    #region REGULAR DELETION
                    //--
                    dmlService.J_OpenConnectionBkUp(J_Var.J_pMsAccessDatabaseName);
                    //
                    dmlService.J_BeginTransaction();
                    //
                    strSQL = @"DELETE FROM MST_COMPANY WHERE COMPANY_ID NOT IN (" + lngCompanyID + ")";
                    //cmnService.J_UserMessage(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        return;
                    //
                    strSQL = @"DELETE FROM TRN_COMPANY_INFO WHERE COMPANY_ID NOT IN (" + lngCompanyID + ")";
                    //cmnService.J_UserMessage(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        return;
                    //
                    strSQL = @"DELETE FROM MST_BOOKMARK_DETAIL WHERE COMPANY_ID NOT IN (" + lngCompanyID + ")";
                    //cmnService.J_UserMessage(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        return;
                    //
                    strSQL = @"DELETE FROM TRN_BASIC_INFO WHERE COMPANY_ID NOT IN (" + lngCompanyID + ")";
                    //cmnService.J_UserMessage(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        return;
                    //
                    strSQL = @"DELETE FROM TRN_CHALLAN 
                               WHERE NOT EXISTS (SELECT BASIC_INFO_ID 
                                             FROM   TRN_BASIC_INFO 
                                             WHERE  TRN_CHALLAN.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                                             AND    TRN_BASIC_INFO.COMPANY_ID = " + lngCompanyID + ")";
                    //cmnService.J_UserMessage(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        return;
                    //
                    strSQL = @"DELETE FROM TRN_DEDUCTEE_DETAILS 
                               WHERE NOT EXISTS (SELECT BASIC_INFO_ID 
                                             FROM   TRN_BASIC_INFO 
                                             WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                                             AND    TRN_BASIC_INFO.COMPANY_ID          = " + lngCompanyID + ")";
                    //cmnService.J_UserMessage(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        return;
                    // 
                    strSQL = @"DELETE FROM TRN_SALARY_DETAILS 
                               WHERE NOT EXISTS (SELECT BASIC_INFO_ID 
                                                 FROM   TRN_BASIC_INFO 
                                                 WHERE  TRN_SALARY_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                                                 AND    TRN_BASIC_INFO.COMPANY_ID        = " + lngCompanyID + ")";
                    //cmnService.J_UserMessage(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        return;
                    //
                    strSQL = @"DELETE FROM MST_DEDUCTEE 
                               WHERE  NOT EXISTS 
                              (SELECT PARTY_ID 
                               FROM   TRN_DEDUCTEE_DETAILS
                               WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_DEDUCTEE.DEDUCTEE_ID)";
                    //cmnService.J_UserMessage(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        return;
                    //
                    strSQL = @"DELETE FROM MST_EMPLOYEE 
                               WHERE  NOT EXISTS 
                              (SELECT PARTY_ID 
                               FROM   TRN_DEDUCTEE_DETAILS
                               WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_EMPLOYEE.EMPLOYEE_ID)";
                    //cmnService.J_UserMessage(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        return;
                    //
                    strSQL = @"DELETE FROM TRN_FILE_GENERATION_LOG 
                               WHERE NOT EXISTS (SELECT BASIC_INFO_ID 
                                                 FROM   TRN_BASIC_INFO 
                                                 WHERE  TRN_FILE_GENERATION_LOG.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                                                 AND    TRN_BASIC_INFO.COMPANY_ID             = " + lngCompanyID + ")";
                    //cmnService.J_UserMessage(strSQL);
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        return;
                    //-- 2015/05/04
                    strSQL = "DELETE FROM MST_TAN_ACCOUNT WHERE TAN_NO <> '" + cmbSelectTAN.Text + "'";
                    if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        return;
                    //
                    dmlService.J_Commit();
                    //--
                    #endregion
                    //--
                    #region CORRECTION
                    dmlService.J_OpenConnectionBkUp(J_Var.J_pMsAccessDatabaseName);
                    //
                    if (strCorCompanyID != "")
                    {
                        dmlService.J_BeginTransaction();
                        //
                        //COR_HDR_BATCH
                        strSQL = @"DELETE FROM COR_HDR_BATCH 
                                   WHERE NOT EXISTS 
                                  (SELECT BATCH_HEADER_ID
                                   FROM   COR_HDR_COMPANY
                                   WHERE  COR_HDR_BATCH.BATCH_HEADER_ID  = COR_HDR_COMPANY.BATCH_HEADER_ID
                                   AND    COR_HDR_COMPANY.HDR_COMPANY_ID IN (" + strCorCompanyID + "))";
                        //cmnService.J_UserMessage(strSQL);
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            return;
                        //COR_HDR_CHALLAN
                        strSQL = @"DELETE FROM COR_HDR_CHALLAN 
                                   WHERE NOT EXISTS 
                                  (SELECT COR_HDR_COMPANY.BATCH_HEADER_ID
                                   FROM   COR_HDR_COMPANY
                                   WHERE  COR_HDR_CHALLAN.BATCH_HEADER_ID  = COR_HDR_COMPANY.BATCH_HEADER_ID
                                   AND    COR_HDR_COMPANY.HDR_COMPANY_ID IN (" + strCorCompanyID + "))";
                        //cmnService.J_UserMessage(strSQL);
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            return;
                        //COR_HDR_COMPANY
                        strSQL = @"DELETE FROM COR_HDR_COMPANY WHERE HDR_COMPANY_ID NOT IN (" + strCorCompanyID + ")";
                        //cmnService.J_UserMessage(strSQL);
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            return;
                        //COR_HDR_DEDUCTEE_DETAILS
                        strSQL = @"DELETE FROM COR_HDR_DEDUCTEE_DETAILS 
                                   WHERE NOT EXISTS 
                                  (SELECT COR_HDR_COMPANY.BATCH_HEADER_ID
                                   FROM   COR_HDR_COMPANY
                                   WHERE  COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = COR_HDR_COMPANY.BATCH_HEADER_ID
                                   AND    COR_HDR_COMPANY.HDR_COMPANY_ID            IN (" + strCorCompanyID + "))";
                        //cmnService.J_UserMessage(strSQL);
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            return;
                        //COR_HDR_SALARY_DETAILS
                        strSQL = @"DELETE FROM COR_HDR_SALARY_DETAILS 
                                   WHERE NOT EXISTS 
                                  (SELECT COR_HDR_COMPANY.BATCH_HEADER_ID
                                   FROM   COR_HDR_COMPANY
                                   WHERE  COR_HDR_SALARY_DETAILS.BATCH_HEADER_ID  = COR_HDR_COMPANY.BATCH_HEADER_ID
                                   AND    COR_HDR_COMPANY.HDR_COMPANY_ID          IN (" + strCorCompanyID + "))";
                        //cmnService.J_UserMessage(strSQL);
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            return;
                        //COR_TRN_CHALLAN
                        strSQL = @"DELETE FROM COR_TRN_CHALLAN 
                                   WHERE NOT EXISTS 
                                  (SELECT COR_HDR_COMPANY.BATCH_HEADER_ID
                                   FROM   COR_HDR_COMPANY
                                   WHERE  COR_TRN_CHALLAN.BATCH_HEADER_ID  = COR_HDR_COMPANY.BATCH_HEADER_ID
                                   AND    COR_HDR_COMPANY.HDR_COMPANY_ID   IN (" + strCorCompanyID + "))";
                        //cmnService.J_UserMessage(strSQL);
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            return;
                        //COR_TRN_COMPANY
                        strSQL = @"DELETE FROM COR_TRN_COMPANY WHERE HDR_COMPANY_ID NOT IN (" + strCorCompanyID + ")";
                        //cmnService.J_UserMessage(strSQL);
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            return;
                        //COR_TRN_DEDUCTEE_DETAILS
                        strSQL = @"DELETE FROM COR_TRN_DEDUCTEE_DETAILS 
                                   WHERE NOT EXISTS 
                                  (SELECT COR_HDR_COMPANY.BATCH_HEADER_ID
                                   FROM   COR_HDR_COMPANY
                                   WHERE  COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID  = COR_HDR_COMPANY.BATCH_HEADER_ID
                                   AND    COR_HDR_COMPANY.HDR_COMPANY_ID            IN (" + strCorCompanyID + "))";
                        //cmnService.J_UserMessage(strSQL);
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            return;
                        //COR_TRN_SALARY_DETAILS
                        strSQL = @"DELETE FROM COR_TRN_SALARY_DETAILS 
                                   WHERE NOT EXISTS 
                                  (SELECT COR_HDR_COMPANY.BATCH_HEADER_ID
                                   FROM   COR_HDR_COMPANY
                                   WHERE  COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID  = COR_HDR_COMPANY.BATCH_HEADER_ID
                                   AND    COR_HDR_COMPANY.HDR_COMPANY_ID          IN (" + strCorCompanyID + "))";
                        //cmnService.J_UserMessage(strSQL);
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            return;
                        //
                        dmlService.J_Commit();
                    }
                    #endregion
                    //--
                    File.Copy(Path.Combine(Application.StartupPath, J_Var.J_pMsAccessDatabaseName), Path.Combine(txtPath.Text, txtFileName.Text), true);
                    //File.Delete(Path.Combine(Application.StartupPath, txtFileName.Text));
                    //cmnService.J_UserMessage("COPIED");
                    //--
                    J_Var.J_pMsAccessDatabaseName = "TDSMAN.MDB";
                    //--
                    dmlService.Dispose();
                    dmlService.J_OpenConnectionBkUp(J_Var.J_pMsAccessDatabaseName);
                    //
                    if (File.Exists(Path.Combine(Application.StartupPath, "TEMP.MDB")) == true)
                    {
                        File.Delete(Path.Combine(Application.StartupPath, "TEMP.MDB"));
                    }
                    //--
                    this.Cursor = Cursors.Default;
                    //----------------------------
                    //tmrWaitMessage.Stop();
                    lblPleaseWaitMessage.Visible = false;
                    //
                    cmnService.J_UserMessage("Company : " + cmbSelectTAN.Text + " - " + lblCompanyName.Text + "\nBackup completed");
                    //--
                }
                #endregion
                //--
                #region TOTAL BKUP
                else
                {
                    //----------------------------
                    this.Cursor = Cursors.WaitCursor;
                    //
                    System.Threading.Thread.Sleep(5000);
                    //
                    if (chkReceiptImages.Checked == true)
                    {
                        if (System.IO.Directory.Exists(Path.Combine(txtPath.Text, txtFileName.Text)) == false)
                            System.IO.Directory.CreateDirectory(Path.Combine(txtPath.Text, txtFileName.Text));
                        //--
                        string[] files = System.IO.Directory.GetFiles(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder));

                        // Copy the files and overwrite destination files if they already exist.
                        foreach (string s in files)
                        {
                            // Use static Path methods to extract only the file name from the path.
                            string fileName = System.IO.Path.GetFileName(s);
                            string destFile = System.IO.Path.Combine(Path.Combine(txtPath.Text, txtFileName.Text), fileName);
                            System.IO.File.Copy(s, destFile, true);
                        }
                        //
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Receipt files Backup Completed");
                        //
                    }
                    else
                    {
                        if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        {
                            //TdsMan.T_CompactAccessDB(Application.StartupPath, txtFileName.Text);
                            File.Copy(Path.Combine(Application.StartupPath, J_Var.J_pMsAccessDatabaseName), Path.Combine(txtPath.Text, txtFileName.Text), true);
                            //
                        }
                        else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer) //-- 2017/07/14
                        {
                            //strSQL = "EXEC SP_MSFOREACHTABLE " +
                            //        "@COMMAND1 = \"DROP TABLE ?\"," +
                            //        "@WHEREAND = \"AND ID IN (SELECT ID FROM SYSOBJECTS WHERE XTYPE = 'U' AND (NAME LIKE 'TMP%' OR NAME LIKE 'TEMP%'))\"";
                            //dmlService.J_ExecSql(strSQL);

                            string strPath = strFolderPath + '\\' + txtFileName.Text;
                            strSQL = "BACKUP DATABASE [TDSMAN] " +
                                     "       TO DISK = '" + strPath + "'";
                            if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                            {
                                this.Cursor = Cursors.Default;
                                //----------------------------
                                cmnService.J_UserMessage("Backup Failed", MessageBoxIcon.Error);
                                return;
                            }
                        }
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Database Backup Completed");
                    }
                    this.Cursor = Cursors.Default;
                    //----------------------------
                    //cmnService.J_UserMessage("Backup Completed");
                    //--
                }
                #endregion
                //--
                //this.Close();
                //this.Dispose();
                //----------------------------
            }
            catch (Exception exception)
            {
                this.Cursor = Cursors.Default;
                //
                //tmrWaitMessage.Stop();
                //lblPleaseWaitMessage.Visible = false;
                //
                dmlService.J_Rollback();
                //
                MessageBox.Show(exception.Message, J_Var.J_pProjectName);
            }
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region tmrWaitMessage_Tick
        private void tmrWaitMessage_Tick(object sender, EventArgs e)
        {
            //lngWaitMessage = lngWaitMessage + 1;
            //if (lngWaitMessage == 200)
            //{
            //    lblPleaseWaitMessage.Visible = true;
            //}
        }
        #endregion

        #region IsDirectoryEmpty
        public static bool IsDirectoryEmpty(DirectoryInfo directory)
        {
            FileInfo[] files = directory.GetFiles();
            DirectoryInfo[] subdirs = directory.GetDirectories();

            return (files.Length == 0 && subdirs.Length == 0);
        }
        #endregion

        #region chkReceiptImages_CheckedChanged
        private void chkReceiptImages_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReceiptImages.Checked == true)
                txtFileName.Text = TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now);
            else
            {
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    txtFileName.Text = J_Var.J_pMsAccessDatabaseName.Substring(0, 6) + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".BAK";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    txtFileName.Text = J_Var.J_pMsAccessDatabaseName.Substring(0, 6) + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".MDB";
                //--
            }
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=CH13rus01-U");                                      
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0046", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));

        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0135", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

    }
}

