using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using TDSMAN.Classes;

namespace TDSMAN.FormTrn
{
    public partial class TrnSelectTDSFile : Form
    {
        #region Default Constructor
        public TrnSelectTDSFile()
        {
            InitializeComponent();
        }
        #endregion

        #region User Defined Constructor
        public TrnSelectTDSFile(long BatchHeaderId)
        {
            InitializeComponent();
            lngBatchHeaderId = BatchHeaderId;
        }
        #endregion

        #region Private Variables Declaration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        
        string strSQL;						//For Storing the Local SQL Query
        
        string strFilePath;
        int intCaratPosition;

        string strHashValue;

        
        long lngBatchHeaderId;

        #endregion

        #region User Defined Events

        #region TrnSelectTDSFile_Load
        private void TrnSelectTDSFile_Load(object sender, EventArgs e)
        {
            lblDisplayMessage.Text = "As per new rules of TIN-NSDL you have to provide the .TDS file path for .FVU file generation.";
            IDataReader reader;

            //Initialising File Path & Hash Value
            TDSMAN.Classes.TDSMAN.T_pHashValue = ""; 

            strSQL = "SELECT COR_TRN_COMPANY.TAN_NO       AS TAN_NO," +
                     "       COR_TRN_COMPANY.COMPANY_NAME AS COMPANY_NAME," +
                     "       COR_HDR_BATCH.FORM_NO        AS FORM_NO," +
                     "       COR_HDR_BATCH.QTR            AS QTR," +
                     "       MST_ASSESSMENT.FA_YEAR       AS FA_YEAR " +
                     "FROM   COR_HDR_BATCH, " +
                     "       COR_TRN_COMPANY, " +
                     "       MST_ASSESSMENT " +
                     "WHERE  COR_HDR_BATCH.BATCH_HEADER_ID = COR_TRN_COMPANY.BATCH_HEADER_ID " +
                     "AND    COR_HDR_BATCH.ASST_ID         = MST_ASSESSMENT.ASST_ID " +
                     "AND    COR_HDR_BATCH.BATCH_HEADER_ID = " + lngBatchHeaderId;

            reader = dmlService.J_ExecSqlReturnReader(strSQL);

            while (reader.Read())
            {
                lblFinancialYear.Text = reader["FA_YEAR"].ToString();
                lblQtr.Text           = reader["QTR"].ToString();
                lblFormNo.Text        = reader["FORM_NO"].ToString();
                lblCompanyTAN.Text    = reader["TAN_NO"].ToString();
                lblCompanyName.Text   = reader["COMPANY_NAME"].ToString();
            }

            reader.Close();
            reader.Dispose();

            btnBrowse.Select();

        }
        #endregion

        #region btnBrowse_Click
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            strFilePath = cmnService.J_OpenFileDialog("Import File | *.tds", "Import File | *.tds", "Choose the File to import");

            if (strFilePath != "")
                txtFilePath.Text = strFilePath;
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            // *************************************************************
            // *** Copying the .TDS file to application folder
            // *************************************************************

            string Filename = cmnService.J_GetFileName(txtFilePath.Text);
            string OutputFilePath = Path.Combine(Application.StartupPath, "Correction Input Files") + "\\" + Filename;

            cmnService.J_CreateDirectory(cmnService.J_GetDirectoryName(OutputFilePath));

            if (ValidateFields() == false)
                return;

            if(txtFilePath.Text != OutputFilePath)
                File.Copy(txtFilePath.Text, OutputFilePath, true);

            //Returning File patha nd hash Value
            TDSMAN.Classes.TDSMAN.T_pHashValue = strHashValue;

            // **************************************************************
            // *** Now Updating the Hash Value and File Path
            // **************************************************************

            dmlService.J_BeginTransaction();
            strSQL = "UPDATE COR_HDR_BATCH " +
                     "SET    HASH_VALUE      = '" + strHashValue + "', " +
                     "       TDS_FILE_PATH   = '" + OutputFilePath + "' " +
                     "WHERE  BATCH_HEADER_ID = " + lngBatchHeaderId;

            dmlService.J_ExecSql(strSQL);

            dmlService.J_Commit();

            this.Dispose();
            this.Close();
        }
        #endregion

        #region btnBack_Click
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region ValidateFields
        public bool ValidateFields()
        {
            try
            {
                // **************************************************
                // **** Blank Check
                // **************************************************
                if (txtFilePath.Text == "")
                {
                    cmnService.J_UserMessage("Please Browse the .TDS file of this return");
                    btnBrowse.Select();
                    return false;
                }

                // *******************************************************
                // *** Reading .TDS file and comparing the return fields
                // *******************************************************

                string strFinancialYear = "";
                string FinancialYear = "";

                TextReader txtRdr = new StreamReader(txtFilePath.Text);
                //
                int NumberOfLines = 2;
                string[] ListLines = new string[NumberOfLines];
                //
                intCaratPosition = 0;
                //
                for (int i = 0; i < NumberOfLines; i++)
                {
                    ListLines[i] = txtRdr.ReadLine();
                    intCaratPosition = ListLines[i].IndexOf("^");

                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "FH")
                    {
                        //Reading File Hash Value
                        for (int a = 1; a < 14; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strHashValue = ListLines[i].Substring(intCaratPosition + 1);
                        strHashValue = ListLines[i].Substring(intCaratPosition + 1, strHashValue.Trim().IndexOf("^"));
                    }

                    if (cmnService.J_Mid(ListLines[i], intCaratPosition + 1, 2) == "BH")
                    {
                        string strOriginalPRN = "";
                        string strPreviousPRN = "";
                        string strFormNo = "";
                        string strTAN = "";
                        string strQuarter = "";

                        // ---------------------------------------------------
                        // ---- Checking Form No
                        // ---------------------------------------------------
                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strFormNo = ListLines[i].Substring(intCaratPosition + 1);
                        strFormNo = ListLines[i].Substring(intCaratPosition + 1, strFormNo.Trim().IndexOf("^"));

                        if (strFormNo != lblFormNo.Text)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the File. Please check.");
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }
                        // ---------------------------------------------------

                        for (int a = 1; a < 4; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strOriginalPRN = ListLines[i].Substring(intCaratPosition + 1);
                        strOriginalPRN = ListLines[i].Substring(intCaratPosition + 1, strOriginalPRN.Trim().IndexOf("^"));

                        if (strOriginalPRN == "")
                        {
                            cmnService.J_UserMessage("Incorrect Format of the File. Please check.");
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }

                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strPreviousPRN = ListLines[i].Substring(intCaratPosition + 1);
                        strPreviousPRN = ListLines[i].Substring(intCaratPosition + 1, strPreviousPRN.Trim().IndexOf("^"));

                        if (strPreviousPRN == "")
                        {
                            cmnService.J_UserMessage("Incorrect Format of the File. Please check.");
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }

                        // ---------------------------------------------------
                        // ---- Checking TAN
                        // ---------------------------------------------------
                        for (int a = 1; a < 5; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strTAN = ListLines[i].Substring(intCaratPosition + 1);
                        strTAN = ListLines[i].Substring(intCaratPosition + 1, strTAN.Trim().IndexOf("^"));

                        if (strTAN != lblCompanyTAN.Text)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the File. Please check.");
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }
                        // ---------------------------------------------------



                        // ---------------------------------------------------
                        // -- Chekcing financial year
                        // ---------------------------------------------------
                        for (int a = 1; a < 5; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strFinancialYear = ListLines[i].Substring(intCaratPosition + 1);
                        strFinancialYear = ListLines[i].Substring(intCaratPosition + 1, strFinancialYear.Trim().IndexOf("^"));

                        FinancialYear = cmnService.J_Left(strFinancialYear, 4) + "-" + cmnService.J_Right(strFinancialYear, 2);

                        if (FinancialYear != lblFinancialYear.Text)
                        {
                            cmnService.J_UserMessage("Please select Valid .TDS file");
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }
                        // ---------------------------------------------------


                        // ---------------------------------------------------
                        // ---- Checking Quarter
                        // ---------------------------------------------------
                        for (int a = 1; a < 2; a++)
                        {
                            intCaratPosition = ListLines[i].IndexOf("^", intCaratPosition + 1);
                        }
                        strQuarter = ListLines[i].Substring(intCaratPosition + 1);
                        strQuarter = ListLines[i].Substring(intCaratPosition + 1, strQuarter.Trim().IndexOf("^"));

                        if (strQuarter != lblQtr.Text)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the File. Please check.");
                            txtRdr.Close();
                            txtRdr.Dispose();
                            return false;
                        }
                        // ---------------------------------------------------
                    }
                }
                txtRdr.Close();
                txtRdr.Dispose();

                return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage("File Format incorrect");
                btnBrowse.Select();
                return false;
            }
        }
        #endregion
        
        #endregion

    }
}