
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TDSMAN.Classes;
using ICSharpCode.SharpZipLib.Zip;

namespace TDSMAN.FormUtl
{
    public partial class UtlExportData : Form
    {

        DMLService dmlService = null;
        CommonService cmnService = null;
        string strSQL = string.Empty;

        public UtlExportData()
        {
            InitializeComponent();
        
            dmlService = new DMLService();
            cmnService = new CommonService();
        }

        private void BtnExportData_Click(object sender, EventArgs e)
        {
            //----------------------------------------------------------------------
            
            string strDestinationFolderPath = cmnService.J_OpenFolderDialog();
            string strFolderName = J_Var.J_pBranchCode + "-EXP-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now);
            cmnService.J_CreateDirectory(strDestinationFolderPath + "\\" + strFolderName);

            //----------------------------------------------------------------------
            //-- Export Logic
            //----------------------------------------------------------------------
            string strData = "";
            long IndAsciiValue = 0;
            long TotAsciiValue = 0;
            StreamWriter streamWriter = null;
            //----------------------------------------------------------------------
            streamWriter = cmnService.J_ReturnStreamWriter(strDestinationFolderPath + "\\" + strFolderName + "\\state.txt");
            
            strSQL = "SELECT STATE_ID," +
                     "       STATE_CODE," +
                     "       STATE_NAME," +
                     "       DEFAULT_FLAG " +
                     "FROM   MST_STATE " +
                     "ORDER BY STATE_ID";
            IDataReader reader = dmlService.J_ExecSqlReturnReader(strSQL);
            if (reader == null) return;

            cmnService.J_WriteLine(ref streamWriter, "STATE_ID,STATE_CODE,STATE_NAME,DEFAULT_FLAG,ASCVAL");
            
            while (reader.Read())
            {
                strData = Convert.ToString(reader["STATE_ID"])
                        + Convert.ToString(reader["STATE_CODE"])
                        + Convert.ToString(reader["STATE_NAME"])
                        + Convert.ToString(reader["DEFAULT_FLAG"]);

                IndAsciiValue = cmnService.J_ReturnAsciiCode(strData, J_ExportImport.YES);
                TotAsciiValue = TotAsciiValue + IndAsciiValue;

                cmnService.J_Write(ref streamWriter, Convert.ToString(reader["STATE_ID"]));
                cmnService.J_Write(ref streamWriter, "," + Convert.ToString(reader["STATE_CODE"]));
                cmnService.J_Write(ref streamWriter, "," + Convert.ToString(reader["STATE_NAME"]));
                cmnService.J_Write(ref streamWriter, "," + Convert.ToString(reader["DEFAULT_FLAG"]));
                cmnService.J_Write(ref streamWriter, "," + Convert.ToString(IndAsciiValue), J_NewLine.YES);
            }

            IndAsciiValue = cmnService.J_ReturnAsciiCode("0SS00SSSNAME0", J_ExportImport.YES);
            TotAsciiValue = TotAsciiValue + IndAsciiValue;

            cmnService.J_Write(ref streamWriter, "0");
            cmnService.J_Write(ref streamWriter, ",SS00SS");
            cmnService.J_Write(ref streamWriter, ",SNAME");
            cmnService.J_Write(ref streamWriter, ",0");
            cmnService.J_Write(ref streamWriter, "," + Convert.ToString(TotAsciiValue));

            reader.Close();
            reader.Dispose();

            streamWriter.Close();

            //----------------------------------------------------------------------

            cmnService.J_Zip(strDestinationFolderPath, strFolderName, "\\" + strFolderName + ".zip", J_Var.J_pZipFilePassword);
            cmnService.J_DeleteDirectory(strDestinationFolderPath + "\\" + strFolderName);
            cmnService.J_UserMessage("Export Completed");

            //----------------------------------------------------------------------

        }

        private void BtnUnzip_Click(object sender, EventArgs e)
        {
            string strPath = cmnService.J_OpenFileDialog("Zip Files (*.zip)|*.zip|Rar Files (*.rar)|*.rar", "Zip Files (*.zip)|*.zip", "Choose the file to Unzip");
            if (cmnService.J_UnZipBool(strPath, J_Var.J_pZipFilePassword) == false) return; ;
            cmnService.J_UserMessage("Unzip Completed");

        }

    }
}