

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
#endregion

namespace TDSMAN.FormSys
{
    public partial class SysBackup : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public SysBackup()
        {
            InitializeComponent();
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
        //----
        #endregion

        #region SysBackup_Load
        private void SysBackup_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Backup";
            //
            mskDate.Text = string.Format("{0:dd/MM/yyyy}", System.DateTime.Now.Date);
            //-- COMMENTED BY ANIK ON 2017/01/06 FOR VS 2010
            //txtTime.Text = string.Format("{0:hh:mm:ss}", System.DateTime.Now.TimeOfDay);
            txtTime.Text = string.Format("{0:hh:mm:ss}", System.DateTime.Now);
            txtPath.Text = Application.StartupPath;
            txtFileName.Text = J_Var.J_pMsAccessDatabaseName.Substring(0, 6) + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" +  string.Format("{0:HHmmss}", System.DateTime.Now) + ".MDB";
            txtNotes.Text = "";
            txtNotes.Select();
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

        #region BtnBackup_Click
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
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
                //
                //----------------------------
                this.Cursor = Cursors.WaitCursor;
                //
                System.Threading.Thread.Sleep(5000);
                //
                //TdsMan.T_CompactAccessDB(Application.StartupPath, txtFileName.Text);
                File.Copy(Path.Combine(Application.StartupPath, J_Var.J_pMsAccessDatabaseName), Path.Combine(txtPath.Text, txtFileName.Text), true);
                //
                this.Cursor = Cursors.Default;
                //----------------------------
                cmnService.J_UserMessage("Backup completed");
                //--
                this.Close();
                this.Dispose();
                //----------------------------
            }
            catch (Exception exception)
            {
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

        private void txtPath_TextChanged(object sender, EventArgs e)
        {

        }

    }
}

