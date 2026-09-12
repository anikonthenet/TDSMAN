
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TDSMAN.Classes;

namespace TDSMAN.FormUtl
{
    public partial class UtlImportData : Form
    {

        DMLService dmlService = null;
        CommonService cmnService = null;
        string strSQL = string.Empty;

        public UtlImportData()
        {
            InitializeComponent();
        
            dmlService = new DMLService();
            cmnService = new CommonService();
        }

        private void BtnImportData_Click(object sender, EventArgs e)
        {
            string strPath = string.Empty;
            string strFolderPath = string.Empty;

            strPath = cmnService.J_OpenFileDialog("Zip Files (*.zip)|*.zip|Rar Files (*.rar)|*.rar|Text Files (*.txt)|*.txt", "Zip Files (*.zip)|*.zip", "Choose the file to import");
            if (strPath == "" || strPath == null) return;

            if (cmnService.J_Right(strPath, 3).ToUpper() != "TXT")
                strFolderPath = cmnService.J_UnZipString(strPath, J_Var.J_pZipFilePassword);
            else
                strFolderPath = cmnService.J_GetDirectoryName(strPath);

            if (strFolderPath == "" || strFolderPath == null) return;

            //-----------------------------------------------------------------------------------
            //-- Checking the Text File
            //-----------------------------------------------------------------------------------

            if (cmnService.J_IsValidTextFile(strFolderPath + "\\state.txt", 5, J_TextSeparator.Comma) == false) return;
            
            //-----------------------------------------------------------------------------------

            dmlService.J_ExecSql("DELETE FROM TXT_STATE");

            if (dmlService.J_ImportData("TXT_STATE", strFolderPath + "\\state.txt", J_TextSeparator.Comma) == false) return;

            if (cmnService.J_Right(strPath, 3).ToUpper() != "TXT")
                cmnService.J_DeleteDirectory(strFolderPath);

            cmnService.J_UserMessage("SUCCESS.....");

        }
    }
}