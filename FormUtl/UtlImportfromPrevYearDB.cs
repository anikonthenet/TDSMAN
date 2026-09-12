
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnFVUImport
Version			: 1.0
Start Date		: 30-12-2010
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
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;


#endregion



namespace TDSMAN.FormUtl
{
    public partial class UtlImportfromPrevYearDB : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public UtlImportfromPrevYearDB()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        //
        string strFVUPath = "";
        //-----------------------------------------------------------------------
        string OutputFolder;
        //string Filename;
        string OutputFilePath;
        //----------------------------------------------
        string strFileName;
        //----------------------------------------------

        string strImportErrorMessage = "Import Failed.";
    
        #endregion

        #region User Defined Events

        #region TrnFVUImport_Load

        private void TrnFVUImport_Load(object sender, EventArgs e)
        {
            //-----------
            lblTitle.Text = "Import From Previous Year Database";
            lblNotes1.Text = "This utility will import all the data from the source database to the existing database";

            lnkLabel2.Text = "--";

            //Added by Shrey Kejriwal on 18/03/2012
            BtnSave.Enabled = false;
            BtnSave.BackColor = Color.Silver;

            //
        }

        #endregion

        #region lnkLabel2_LinkClicked
        private void lnkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.tdsman.com/Downloads/eTutorial-TAN-Registration.zip");
        }
        #endregion

        #region btnSelectTDSPath_Click
        private void btnSelectTDSPath_Click(object sender, EventArgs e)
        {
            //strFVUPath = cmnService.J_OpenFileDialog("Import File | *.tds; *.fvu", "Import File | *.tds; *.fvu", "Choose the File to import");
            strFVUPath = cmnService.J_OpenFileDialog("Database File | *.mdb", "Choose the File to import");

            if (strFVUPath != "")
            {
                txtFVUPath.Text = strFVUPath;

                //Added by Shrey Kejriwal on 18/03/2012
                BtnSave.Enabled = true;
                BtnSave.BackColor = Color.Lavender;
            }
            else
            {
                txtFVUPath.Text = "";

                //Added by Shrey Kejriwal on 18/03/2012
                BtnSave.Enabled = false;
                BtnSave.BackColor = Color.Silver;
            }

        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            prgBar.Value = 0;
            //
            if (ValidateFields() == false) return;

            OutputFolder = Path.Combine(Application.StartupPath, "Prev Year Tables");
            strFileName = cmnService.J_GetFileName(strFVUPath);
            OutputFilePath = OutputFolder + "\\" + strFileName;

            //SAVING THE FILE SELECTED IN APPLICATION FOLDER
            cmnService.J_CreateDirectory(OutputFolder);

            #region VALIDATE SOURCE DATABASE

            //ADDED BY INDRAJIT ON 03-04-2012
            string strSourcePath = "";
            strSourcePath = strFVUPath;

            // -------------------------------------------------
            // -- NOW CHECKING IF THE SOURCE DB IS VALID TDSMAN DB
            // -------------------------------------------------

            DMLService dmlServiceSourceDB = new DMLService(J_DatabaseType.MsAccess, J_ConnectionProviderType.OleDb);

            //CHECKING IF THE CONNECTION CAN BE MADE TO THE DB
            if (dmlServiceSourceDB.J_ValidateConnection(strSourcePath, "mother") == false)
            {
                dmlServiceSourceDB.Dispose();
                cmnService.J_UserMessage("Invalid database file selected.");
                return;
            }

            if (dmlServiceSourceDB.J_IsDatabaseObjectExist("MST_ASSESSMENT") == false)
            {
                dmlServiceSourceDB.Dispose();
                cmnService.J_UserMessage("Invalid database file selected.");
                return;
            }
            ////if (cmnService.J_UserMessage("Please note : All your existing database will be over written with the selected database. \nProceed ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            ////    return;
            //--

            //CHECKING IF THE DB IS THE TDSMAN DB ONLY
            string strSQL = "SELECT GROUP_NAME FROM MST_GROUP WHERE GROUP_ID = 1";

            if (Convert.ToString(cmnService.J_NullToText(dmlServiceSourceDB.J_ExecSqlReturnScalar(strSQL))) != "TDS-MAN")
            {
                cmnService.J_UserMessage("Invalid database file selected.");
                return;
            }

            string strDestinationPath = OutputFilePath;
            //END OF ADDITION BY INDRAJIT ON 03-04-2012
            
            //////DISPLYING THE CONFIRMATION MESSAGE BEFORE IMPORTING
            ////if (cmnService.J_UserMessage("TDS File Creation Date : " + strFileCreationDate + "\n Please ensure that you are using the latest file for preparing correction statement.\n Proceed to Import?", MessageBoxButtons.YesNo) == DialogResult.No)
            ////{
            ////    BtnExit.Select();
            ////    return;
            ////}

            #endregion

            prgBar.Value = prgBar.Value + 10;
            this.Refresh();

            dmlService.J_BeginTransaction();

            #region IMPORT TO TEMP TABLES

            //IMPORTING TABLES FROM SOURCE DATABASE
            if (ImportTransactionsFromFVU(txtFVUPath.Text) == false)
            {
                prgBar.Value = 0;
                this.Cursor = Cursors.Default;
                return;
            }

            #endregion

            #region CORRECTING THE ASSESSMENT YEAR ID OF THE DATA TO BE IMPORTED

            strSQL = "SELECT TOP 1 FA_YEAR FROM TEMP_MST_ASSESSMENT ORDER BY ASST_ID";

            if (dmlService.J_ExecSqlReturnScalar(strSQL).ToString() != T_FinancialYear.F2005_06)
            {
                strSQL = "UPDATE TEMP_MST_ASSESSMENT SET ASST_ID = ASST_ID + 2";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return;
                }

                strSQL = "UPDATE TEMP_TRN_BASIC_INFO SET ASST_ID = ASST_ID + 2";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return;
                }

                strSQL = "UPDATE TEMP_COR_HDR_BATCH SET ASST_ID = ASST_ID + 2";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return;
                }

            }

            #endregion

            #region ADDING DEDUCTEE_FLAG COLUMN IN TEMP_TRN_DEDUCTEE_DETAILS

            // -------------------------------------------------------
            //ADDING A FLAG TO SEPERATE 24Q EMPLOYEES AND OTHER DEDUCTEES
            // -------------------------------------------------------
            //ADDING DEDUCTEE_FLAG COLUMN IN TEMP_TRN_DEDUCTEE_DETAILS
            strSQL = "ALTER TABLE TEMP_TRN_DEDUCTEE_DETAILS ADD COLUMN DEDUCTEE_FLAG NUMBER DEFAULT 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            
            strSQL = "UPDATE TEMP_TRN_DEDUCTEE_DETAILS SET DEDUCTEE_FLAG = 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING FLAG AS 0 FOR EMPLOYEES
            strSQL = "UPDATE TEMP_TRN_DEDUCTEE_DETAILS " +
                     "INNER JOIN TEMP_TRN_BASIC_INFO " +
                     "ON    TEMP_TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.BASIC_INFO_ID " +
                     "SET   TEMP_TRN_DEDUCTEE_DETAILS.DEDUCTEE_FLAG = 0 " +
                     "WHERE TEMP_TRN_BASIC_INFO.FORM_NO             = '" + T_FormNo.F24Q + "'";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING FLAG AS 1 FOR DEDUCTEES
            strSQL = "UPDATE TEMP_TRN_DEDUCTEE_DETAILS " +
                     "INNER JOIN TEMP_TRN_BASIC_INFO " +
                     "ON    TEMP_TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.BASIC_INFO_ID " +
                     "SET   TEMP_TRN_DEDUCTEE_DETAILS.DEDUCTEE_FLAG = 1 " +
                     "WHERE TEMP_TRN_BASIC_INFO.FORM_NO             <> '" + T_FormNo.F24Q + "'";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            // -------------------------------------------------------

            #endregion


            #region Updating Ids of Temp tables

            long lngBATCH_HEADER_ID = 0;
            long lngHDR_CHALLAN_ID = 0;
            long lngHDR_COMPANY_ID = 0;
            long lngHDR_DEDUCTEE_DETAIL_ID = 0;
            long lngHDR_SALARY_DETAILS_ID = 0;
            long lngTRN_CHALLAN_ID = 0;
            long lngTRN_COMPANY_ID = 0;
            long lngTRN_DEDUCTEE_DETAIL_ID = 0;
            long lngTRN_SALARY_DETAILS_ID = 0;
            long lngCOMPANY_ID = 0;
            long lngDEDUCTEE_ID = 0;
            long lngEMPLOYEE_ID = 0;
            long lngBASIC_INFO_ID = 0;
            long lngCHALLAN_ID = 0;
            long lngCOMPANY_INFO_ID = 0;
            long lngDEDUCTEE_DETAIL_ID = 0;
            long lngSALARY_DETAILS_ID = 0;



            #region GET LAST ID OF all tables

            //UPDATE LAST ID FOR COR_ TABLES
            strSQL = "SELECT MAX(BATCH_HEADER_ID) FROM COR_HDR_BATCH";
            lngBATCH_HEADER_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));

            strSQL = "SELECT MAX(HDR_CHALLAN_ID) FROM COR_HDR_CHALLAN";
            lngHDR_CHALLAN_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(HDR_COMPANY_ID) FROM COR_HDR_COMPANY";
            lngHDR_COMPANY_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(HDR_DEDUCTEE_DETAIL_ID) FROM COR_HDR_DEDUCTEE_DETAILS";
            lngHDR_DEDUCTEE_DETAIL_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(HDR_SALARY_DETAILS_ID) FROM COR_HDR_SALARY_DETAILS";
            lngHDR_SALARY_DETAILS_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));

            strSQL = "SELECT MAX(TRN_CHALLAN_ID) FROM COR_TRN_CHALLAN";
            lngTRN_CHALLAN_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(TRN_COMPANY_ID) FROM COR_TRN_COMPANY";
            lngTRN_COMPANY_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(TRN_DEDUCTEE_DETAIL_ID) FROM COR_TRN_DEDUCTEE_DETAILS";
            lngTRN_DEDUCTEE_DETAIL_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(TRN_SALARY_DETAILS_ID) FROM COR_TRN_SALARY_DETAILS";
            lngTRN_SALARY_DETAILS_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));


            strSQL = "SELECT MAX(COMPANY_ID) FROM MST_COMPANY";
            lngCOMPANY_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(DEDUCTEE_ID) FROM MST_DEDUCTEE";
            lngDEDUCTEE_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(EMPLOYEE_ID) FROM MST_EMPLOYEE";
            lngEMPLOYEE_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));

            strSQL = "SELECT MAX(BASIC_INFO_ID) FROM TRN_BASIC_INFO";
            lngBASIC_INFO_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(CHALLAN_ID) FROM TRN_CHALLAN";
            lngCHALLAN_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(COMPANY_INFO_ID) FROM TRN_COMPANY_INFO";
            lngCOMPANY_INFO_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(DEDUCTEE_DETAIL_ID) FROM TRN_DEDUCTEE_DETAILS";
            lngDEDUCTEE_DETAIL_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            strSQL = "SELECT MAX(SALARY_DETAILS_ID) FROM TRN_SALARY_DETAILS";
            lngSALARY_DETAILS_ID = Convert.ToInt64(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)));
            
            #endregion

            prgBar.Value = prgBar.Value + 10;
            this.Refresh();

            #region UPDATE ID OF TEMP_COR_ TABLES

            //UPDATE ID INTO TEMP_COR_ TABLES
            strSQL = "UPDATE TEMP_COR_HDR_BATCH " + 
                     "SET BATCH_HEADER_ID = BATCH_HEADER_ID + " + lngBATCH_HEADER_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_COR_HDR_CHALLAN " +
                     "SET HDR_CHALLAN_ID  = HDR_CHALLAN_ID + " + lngHDR_CHALLAN_ID + "," +
                     "    BATCH_HEADER_ID = BATCH_HEADER_ID + " + lngBATCH_HEADER_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_COR_HDR_COMPANY " +
                     "SET HDR_COMPANY_ID  = HDR_COMPANY_ID + " + lngHDR_COMPANY_ID + "," +
                     "    BATCH_HEADER_ID = BATCH_HEADER_ID + " + lngBATCH_HEADER_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_COR_HDR_DEDUCTEE_DETAILS " +
                     "SET HDR_DEDUCTEE_DETAIL_ID  = HDR_DEDUCTEE_DETAIL_ID + " + lngHDR_DEDUCTEE_DETAIL_ID + "," +
                     "    HDR_CHALLAN_ID          = HDR_CHALLAN_ID + " + lngHDR_CHALLAN_ID + "," +
                     "    BATCH_HEADER_ID         = BATCH_HEADER_ID + " + lngBATCH_HEADER_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_COR_HDR_SALARY_DETAILS " +
                     "SET HDR_SALARY_DETAILS_ID  = HDR_SALARY_DETAILS_ID + " + lngHDR_SALARY_DETAILS_ID + "," +
                     "    BATCH_HEADER_ID        = BATCH_HEADER_ID + " + lngBATCH_HEADER_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_COR_TRN_CHALLAN " +
                     "SET TRN_CHALLAN_ID  = TRN_CHALLAN_ID + " + lngTRN_CHALLAN_ID + "," +
                     "    HDR_CHALLAN_ID  = HDR_CHALLAN_ID + " + lngHDR_CHALLAN_ID + "," +
                     "    BATCH_HEADER_ID = BATCH_HEADER_ID + " + lngBATCH_HEADER_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_COR_TRN_COMPANY " +
                     "SET TRN_COMPANY_ID  = TRN_COMPANY_ID + " + lngTRN_COMPANY_ID + "," +
                     "    HDR_COMPANY_ID  = HDR_COMPANY_ID + " + lngHDR_COMPANY_ID + "," +
                     "    BATCH_HEADER_ID = BATCH_HEADER_ID + " + lngBATCH_HEADER_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_COR_TRN_DEDUCTEE_DETAILS " +
                     "SET TRN_DEDUCTEE_DETAIL_ID  = TRN_DEDUCTEE_DETAIL_ID + " + lngTRN_DEDUCTEE_DETAIL_ID + "," +
                     "    HDR_DEDUCTEE_DETAIL_ID  = HDR_DEDUCTEE_DETAIL_ID + " + lngHDR_DEDUCTEE_DETAIL_ID + "," +
                     "    TRN_CHALLAN_ID          = TRN_CHALLAN_ID + " + lngTRN_CHALLAN_ID + "," +
                     "    BATCH_HEADER_ID         = BATCH_HEADER_ID + " + lngBATCH_HEADER_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_COR_TRN_SALARY_DETAILS " +
                     "SET TRN_SALARY_DETAILS_ID  = TRN_SALARY_DETAILS_ID + " + lngTRN_SALARY_DETAILS_ID + "," +
                     "    HDR_SALARY_DETAILS_ID  = HDR_SALARY_DETAILS_ID + " + lngHDR_SALARY_DETAILS_ID + "," +
                     "    BATCH_HEADER_ID        = BATCH_HEADER_ID + " + lngBATCH_HEADER_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_MST_COMPANY " +
                     "SET COMPANY_ID  = COMPANY_ID + " + lngCOMPANY_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_MST_DEDUCTEE " +
                     "SET DEDUCTEE_ID  = DEDUCTEE_ID + " + lngDEDUCTEE_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_MST_EMPLOYEE " +
                     "SET EMPLOYEE_ID  = EMPLOYEE_ID + " + lngEMPLOYEE_ID + ", " +
                     "    COMPANY_ID  = COMPANY_ID + " + lngCOMPANY_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_BASIC_INFO " +
                     "SET BASIC_INFO_ID  = BASIC_INFO_ID + " + lngBASIC_INFO_ID + ", " +
                     "    COMPANY_ID  = COMPANY_ID + " + lngCOMPANY_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_CHALLAN " +
                     "SET CHALLAN_ID  = CHALLAN_ID + " + lngCHALLAN_ID + ", " +
                     "    BASIC_INFO_ID  = BASIC_INFO_ID + " + lngBASIC_INFO_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_COMPANY_INFO " +
                     "SET COMPANY_INFO_ID  = COMPANY_INFO_ID + " + lngCOMPANY_INFO_ID + ", " +
                     "    COMPANY_ID  = COMPANY_ID + " + lngCOMPANY_ID + ", " +
                     "    BASIC_INFO_ID  = BASIC_INFO_ID + " + lngBASIC_INFO_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_DEDUCTEE_DETAILS " +
                     "SET DEDUCTEE_DETAIL_ID  = DEDUCTEE_DETAIL_ID + " + lngDEDUCTEE_DETAIL_ID + ", " +
                     "    CHALLAN_ID          = CHALLAN_ID + " + lngCHALLAN_ID + ", " +
                     "    PARTY_ID            = PARTY_ID + " + lngDEDUCTEE_ID + ", " +
                     "    BASIC_INFO_ID       = BASIC_INFO_ID + " + lngBASIC_INFO_ID + " " +
                     "WHERE DEDUCTEE_FLAG = 1";
                         
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_DEDUCTEE_DETAILS " +
                     "SET DEDUCTEE_DETAIL_ID  = DEDUCTEE_DETAIL_ID + " + lngDEDUCTEE_DETAIL_ID + ", " +
                     "    CHALLAN_ID          = CHALLAN_ID + " + lngCHALLAN_ID + ", " +
                     "    PARTY_ID            = PARTY_ID + " + lngEMPLOYEE_ID + ", " +
                     "    BASIC_INFO_ID       = BASIC_INFO_ID + " + lngBASIC_INFO_ID + " " +
                     "WHERE DEDUCTEE_FLAG = 0";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_SALARY_DETAILS " +
                     "SET SALARY_DETAILS_ID  = SALARY_DETAILS_ID + " + lngSALARY_DETAILS_ID + ", " +
                     "    EMPLOYEE_ID          = EMPLOYEE_ID + " + lngEMPLOYEE_ID + ", " +
                     "    BASIC_INFO_ID       = BASIC_INFO_ID + " + lngBASIC_INFO_ID;
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            #endregion

            prgBar.Value = prgBar.Value + 10;
            this.Refresh();

            #endregion

            #region INSERT TO COR_ TABLES

            //INSERT DATA INTO COR_ TABLES
            strSQL = "INSERT INTO COR_HDR_BATCH " +
                     "SELECT TEMP_COR_HDR_BATCH.* " +
                     "FROM   TEMP_COR_HDR_BATCH, " +
                     "       MST_ASSESSMENT " +  //Inner Join with mst_assessment to filter out records of higher fa year(higher than allowed) 
                     "WHERE  TEMP_COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID ";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            //    
            strSQL = "INSERT INTO COR_HDR_CHALLAN " +
                     "SELECT TEMP_COR_HDR_CHALLAN.* " +
                     "FROM TEMP_COR_HDR_CHALLAN, " +
                     "     TEMP_COR_HDR_BATCH, " +
                     "     MST_ASSESSMENT " +
                     "WHERE TEMP_COR_HDR_CHALLAN.BATCH_HEADER_ID = TEMP_COR_HDR_BATCH.BATCH_HEADER_ID " +
                     "AND   TEMP_COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID ";
                                                                 
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            //
            strSQL = "INSERT INTO COR_HDR_COMPANY " +
                     "SELECT TEMP_COR_HDR_COMPANY.* " +
                     "FROM TEMP_COR_HDR_COMPANY, " +
                     "     TEMP_COR_HDR_BATCH, " +
                     "     MST_ASSESSMENT " +
                     "WHERE TEMP_COR_HDR_COMPANY.BATCH_HEADER_ID = TEMP_COR_HDR_BATCH.BATCH_HEADER_ID " +
                     "AND   TEMP_COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID ";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            //
            strSQL = "INSERT INTO COR_HDR_DEDUCTEE_DETAILS " +
                     "SELECT TEMP_COR_HDR_DEDUCTEE_DETAILS.* " +
                     "FROM TEMP_COR_HDR_DEDUCTEE_DETAILS, " +
                     "     TEMP_COR_HDR_BATCH, " +
                     "     MST_ASSESSMENT " +
                     "WHERE TEMP_COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID = TEMP_COR_HDR_BATCH.BATCH_HEADER_ID " +
                     "AND   TEMP_COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID ";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            //
            strSQL = "INSERT INTO COR_HDR_SALARY_DETAILS " +
                     "SELECT TEMP_COR_HDR_SALARY_DETAILS.* " +
                     "FROM TEMP_COR_HDR_SALARY_DETAILS, " +
                     "     TEMP_COR_HDR_BATCH, " +
                     "     MST_ASSESSMENT " +
                     "WHERE TEMP_COR_HDR_SALARY_DETAILS.BATCH_HEADER_ID = TEMP_COR_HDR_BATCH.BATCH_HEADER_ID " +
                     "AND   TEMP_COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID ";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            //
            strSQL = "INSERT INTO COR_TRN_CHALLAN " +
                     "SELECT TEMP_COR_TRN_CHALLAN.* " +
                     "FROM TEMP_COR_TRN_CHALLAN, " +
                     "     TEMP_COR_HDR_BATCH, " +
                     "     MST_ASSESSMENT " +
                     "WHERE TEMP_COR_TRN_CHALLAN.BATCH_HEADER_ID = TEMP_COR_HDR_BATCH.BATCH_HEADER_ID " +
                     "AND   TEMP_COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID ";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            //
            strSQL = "INSERT INTO COR_TRN_COMPANY " +
                     "SELECT TEMP_COR_TRN_COMPANY.* " +
                     "FROM TEMP_COR_TRN_COMPANY, " +
                     "     TEMP_COR_HDR_BATCH, " +
                     "     MST_ASSESSMENT " +
                     "WHERE TEMP_COR_TRN_COMPANY.BATCH_HEADER_ID = TEMP_COR_HDR_BATCH.BATCH_HEADER_ID " +
                     "AND   TEMP_COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID ";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            //
            strSQL = "INSERT INTO COR_TRN_DEDUCTEE_DETAILS " +
                     "SELECT TEMP_COR_TRN_DEDUCTEE_DETAILS.* " +
                     "FROM TEMP_COR_TRN_DEDUCTEE_DETAILS, " +
                     "     TEMP_COR_HDR_BATCH, " +
                     "     MST_ASSESSMENT " +
                     "WHERE TEMP_COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = TEMP_COR_HDR_BATCH.BATCH_HEADER_ID " +
                     "AND   TEMP_COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID ";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            //
            strSQL = "INSERT INTO COR_TRN_SALARY_DETAILS " +
                     "SELECT TEMP_COR_TRN_SALARY_DETAILS.* " +
                     "FROM TEMP_COR_TRN_SALARY_DETAILS, " +
                     "     TEMP_COR_HDR_BATCH, " +
                     "     MST_ASSESSMENT " +
                     "WHERE TEMP_COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID = TEMP_COR_HDR_BATCH.BATCH_HEADER_ID " +
                     "AND   TEMP_COR_HDR_BATCH.ASST_ID = MST_ASSESSMENT.ASST_ID ";

            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            prgBar.Value = prgBar.Value + 10;
            this.Refresh();

            #endregion

            #region IMPORT DATA TO MST_COMPANY TABLES FROM TEMP_MST_COMPANY TABLES

            //ADDING NEW ID COLUMN IN TEMP_MST_COMPANY
            strSQL = "ALTER TABLE TEMP_MST_COMPANY ADD COLUMN TEMP_COMPANY_ID NUMBER DEFAULT 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_MST_COMPANY SET TEMP_COMPANY_ID = 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //ADDING NEW RECORDS INTO MST_COMPANY
            strSQL = "INSERT INTO MST_COMPANY " +
                     "SELECT TEMP_MST_COMPANY.COMPANY_ID," + 
                     "       TEMP_MST_COMPANY.GROUP_ID," +
                     "       TEMP_MST_COMPANY.ASST_ID," +
                     "       TEMP_MST_COMPANY.TAN_NO," +
                     "       TEMP_MST_COMPANY.PAN_NO," +
                     "       TEMP_MST_COMPANY.COMPANY_NAME," +
                     "       TEMP_MST_COMPANY.D_CATEGORY_ID," +
                     "       TEMP_MST_COMPANY.D_STATE_ID," +
                     "       TEMP_MST_COMPANY.BRANCH_DIV," +
                     "       TEMP_MST_COMPANY.MINISTRY_ID," +
                     "       TEMP_MST_COMPANY.MINISTRY_OTHER," +
                     "       TEMP_MST_COMPANY.PAO_CODE," +
                     "       TEMP_MST_COMPANY.PAO_REG_NO," +
                     "       TEMP_MST_COMPANY.DDO_CODE," +
                     "       TEMP_MST_COMPANY.DDO_REG_NO," +
                     "       TEMP_MST_COMPANY.ADDRESS1," +
                     "       TEMP_MST_COMPANY.ADDRESS2," +
                     "       TEMP_MST_COMPANY.ADDRESS3," +
                     "       TEMP_MST_COMPANY.ADDRESS4," +
                     "       TEMP_MST_COMPANY.ADDRESS5," +
                     "       TEMP_MST_COMPANY.STATE_ID," +
                     "       TEMP_MST_COMPANY.PIN_CODE," +
                     "       TEMP_MST_COMPANY.PHONE," +
                     "       TEMP_MST_COMPANY.STD," +
                     "       TEMP_MST_COMPANY.EMAIL," +
                     "       TEMP_MST_COMPANY.PERSON_NAME," +
                     "       TEMP_MST_COMPANY.DESIGNATION," +
                     "       TEMP_MST_COMPANY.FATHER_NAME," +
                     "       TEMP_MST_COMPANY.P_ADDRESS1," +
                     "       TEMP_MST_COMPANY.P_ADDRESS2," +
                     "       TEMP_MST_COMPANY.P_ADDRESS3," +
                     "       TEMP_MST_COMPANY.P_ADDRESS4," +
                     "       TEMP_MST_COMPANY.P_ADDRESS5," +
                     "       TEMP_MST_COMPANY.P_STATE_ID," +
                     "       TEMP_MST_COMPANY.P_PIN_CODE," +
                     "       TEMP_MST_COMPANY.P_PHONE," +
                     "       TEMP_MST_COMPANY.P_STD," +
                     "       TEMP_MST_COMPANY.P_EMAIL," +
                     "       TEMP_MST_COMPANY.P_MOBILE," +
                     "       TEMP_MST_COMPANY.ASSESSING_OFFICER_CODE," +
                     "       TEMP_MST_COMPANY.TDS_CIRCLE," +
                     "       TEMP_MST_COMPANY.FILE_PREFIX," +
                     "       TEMP_MST_COMPANY.CIT_TDS_ADDRESS," +
                     "       TEMP_MST_COMPANY.CIT_TDS_CITY," +
                     "       TEMP_MST_COMPANY.CIT_TDS_PINCODE " + 
                     "FROM TEMP_MST_COMPANY " +
                     "LEFT JOIN MST_COMPANY " +
                     "ON TEMP_MST_COMPANY.TAN_NO = MST_COMPANY.TAN_NO " +
                     "WHERE MST_COMPANY.COMPANY_ID IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING TEMP_COMPANY_ID COLUMN
            strSQL = "UPDATE TEMP_MST_COMPANY " +
                     "INNER JOIN MST_COMPANY " +
                     "ON TEMP_MST_COMPANY.TAN_NO = MST_COMPANY.TAN_NO " +
                     "SET TEMP_MST_COMPANY.TEMP_COMPANY_ID = MST_COMPANY.COMPANY_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING COMPANY_ID IN ALL TEMP_TRN_ TABLES
            strSQL = "UPDATE TEMP_TRN_BASIC_INFO " + 
                     "INNER JOIN TEMP_MST_COMPANY " + 
                     "ON TEMP_TRN_BASIC_INFO.COMPANY_ID = TEMP_MST_COMPANY.COMPANY_ID " +
                     "SET TEMP_TRN_BASIC_INFO.COMPANY_ID = TEMP_MST_COMPANY.TEMP_COMPANY_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_COMPANY_INFO " +
                     "INNER JOIN TEMP_MST_COMPANY " +
                     "ON TEMP_TRN_COMPANY_INFO.COMPANY_ID = TEMP_MST_COMPANY.COMPANY_ID " +
                     "SET TEMP_TRN_COMPANY_INFO.COMPANY_ID = TEMP_MST_COMPANY.TEMP_COMPANY_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_MST_EMPLOYEE " +
                     "INNER JOIN TEMP_MST_COMPANY " +
                     "ON  TEMP_MST_EMPLOYEE.COMPANY_ID = TEMP_MST_COMPANY.COMPANY_ID " +
                     "SET TEMP_MST_EMPLOYEE.COMPANY_ID = TEMP_MST_COMPANY.TEMP_COMPANY_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            #endregion

            
            #region IMPORT DATA TO MST_DEDUCTEE TABLES FROM TEMP_MST_DEDUCTEE TABLES

            //ADDING NEW ID COLUMN IN TEMP_MST_DEDUCTEE
            strSQL = "ALTER TABLE TEMP_MST_DEDUCTEE ADD COLUMN TEMP_DEDUCTEE_ID NUMBER DEFAULT 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_MST_DEDUCTEE SET TEMP_DEDUCTEE_ID = 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //ADDING NEW RECORDS INTO MST_DEDUCTEE
            strSQL = "INSERT INTO MST_DEDUCTEE " +
                     "SELECT TEMP_MST_DEDUCTEE.DEDUCTEE_ID," +
                     "       TEMP_MST_DEDUCTEE.DEDUCTEE_CODE," +
                     "       TEMP_MST_DEDUCTEE.DEDUCTEE_NAME," +
                     "       TEMP_MST_DEDUCTEE.DEDUCTEE_PAN," +
                     "       TEMP_MST_DEDUCTEE.GROUP_ID," +
                     "       TEMP_MST_DEDUCTEE.ADDRESS1," +
                     "       TEMP_MST_DEDUCTEE.ADDRESS2," +
                     "       TEMP_MST_DEDUCTEE.ADDRESS3," +
                     "       TEMP_MST_DEDUCTEE.ADDRESS4," +
                     "       TEMP_MST_DEDUCTEE.ADDRESS5," +
                     "       TEMP_MST_DEDUCTEE.STATE_ID," +
                     "       TEMP_MST_DEDUCTEE.PIN_CODE," +
                     "       TEMP_MST_DEDUCTEE.MOBILE_NO," +
                     "       TEMP_MST_DEDUCTEE.EMAIL " +
                     "FROM   TEMP_MST_DEDUCTEE " +
                     "LEFT JOIN (SELECT DEDUCTEE_NAME, " +
                     "                  DEDUCTEE_PAN " +
                     "           FROM MST_DEDUCTEE " +
                     "           WHERE DEDUCTEE_PAN <> 'PANNOTAVBL') AS TEMP_MASTER " +
                     "ON    TEMP_MST_DEDUCTEE.DEDUCTEE_PAN  = TEMP_MASTER.DEDUCTEE_PAN " +
                     "WHERE TEMP_MST_DEDUCTEE.DEDUCTEE_PAN <> 'PANNOTAVBL' " +
                     "AND   TEMP_MASTER.DEDUCTEE_NAME IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "INSERT INTO MST_DEDUCTEE " +
                     "SELECT TEMP_MST_DEDUCTEE.DEDUCTEE_ID," +
                     "       TEMP_MST_DEDUCTEE.DEDUCTEE_CODE," +
                     "       TEMP_MST_DEDUCTEE.DEDUCTEE_NAME," +
                     "       TEMP_MST_DEDUCTEE.DEDUCTEE_PAN," +
                     "       TEMP_MST_DEDUCTEE.GROUP_ID," +
                     "       TEMP_MST_DEDUCTEE.ADDRESS1," +
                     "       TEMP_MST_DEDUCTEE.ADDRESS2," +
                     "       TEMP_MST_DEDUCTEE.ADDRESS3," +
                     "       TEMP_MST_DEDUCTEE.ADDRESS4," +
                     "       TEMP_MST_DEDUCTEE.ADDRESS5," +
                     "       TEMP_MST_DEDUCTEE.STATE_ID," +
                     "       TEMP_MST_DEDUCTEE.PIN_CODE," +
                     "       TEMP_MST_DEDUCTEE.MOBILE_NO," +
                     "       TEMP_MST_DEDUCTEE.EMAIL " +
                     "FROM   TEMP_MST_DEDUCTEE " + 
                     "LEFT JOIN (SELECT DEDUCTEE_NAME, " + 
                     "                  DEDUCTEE_PAN " +
                     "           FROM MST_DEDUCTEE " +
                     "           WHERE DEDUCTEE_PAN = 'PANNOTAVBL') AS TEMP_MASTER " +
                     "ON    TEMP_MST_DEDUCTEE.DEDUCTEE_NAME = TEMP_MASTER.DEDUCTEE_NAME " +
                     "WHERE TEMP_MST_DEDUCTEE.DEDUCTEE_PAN  = 'PANNOTAVBL' " +
                     "AND   TEMP_MASTER.DEDUCTEE_NAME IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING TEMP_DEDUCTEE_ID COLUMN
            strSQL = "UPDATE TEMP_MST_DEDUCTEE " +
                     "INNER JOIN MST_DEDUCTEE " +
                     "ON    TEMP_MST_DEDUCTEE.DEDUCTEE_PAN     = MST_DEDUCTEE.DEDUCTEE_PAN " +
                     "SET   TEMP_MST_DEDUCTEE.TEMP_DEDUCTEE_ID = MST_DEDUCTEE.DEDUCTEE_ID " +
                     "WHERE MST_DEDUCTEE.DEDUCTEE_PAN         <> 'PANNOTAVBL' " +
                     "AND   TEMP_MST_DEDUCTEE.DEDUCTEE_PAN    <> 'PANNOTAVBL'";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_MST_DEDUCTEE " +
                     "INNER JOIN MST_DEDUCTEE " +
                     "ON    TEMP_MST_DEDUCTEE.DEDUCTEE_NAME    = MST_DEDUCTEE.DEDUCTEE_NAME " +
                     "SET   TEMP_MST_DEDUCTEE.TEMP_DEDUCTEE_ID = MST_DEDUCTEE.DEDUCTEE_ID " +
                     "WHERE MST_DEDUCTEE.DEDUCTEE_PAN          = 'PANNOTAVBL' " +
                     "AND   TEMP_MST_DEDUCTEE.DEDUCTEE_PAN     = 'PANNOTAVBL'";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING PARTY_ID IN TEMP_TRN_DEDUCTEE_DETAILS TABLES
            strSQL = "UPDATE TEMP_TRN_DEDUCTEE_DETAILS " +
                     "INNER JOIN TEMP_MST_DEDUCTEE " +
                     "ON    TEMP_TRN_DEDUCTEE_DETAILS.PARTY_ID = TEMP_MST_DEDUCTEE.DEDUCTEE_ID " +
                     "SET   TEMP_TRN_DEDUCTEE_DETAILS.PARTY_ID = TEMP_MST_DEDUCTEE.TEMP_DEDUCTEE_ID " +
                     "WHERE TEMP_TRN_DEDUCTEE_DETAILS.DEDUCTEE_FLAG = 1";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            #endregion

            #region IMPORT DATA TO MST_EMPLOYEE TABLES FROM TEMP_MST_EMPLOYEE TABLES

            //ADDING NEW ID COLUMN IN TEMP_MST_EMPLOYEE
            strSQL = "ALTER TABLE TEMP_MST_EMPLOYEE ADD COLUMN TEMP_EMPLOYEE_ID NUMBER DEFAULT 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_MST_EMPLOYEE SET TEMP_EMPLOYEE_ID = 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //ADDING NEW RECORDS INTO MST_EMPLOYEE
            strSQL = "INSERT INTO MST_EMPLOYEE " +
                     "SELECT TEMP_MST_EMPLOYEE.EMPLOYEE_ID," +
                     "       TEMP_MST_EMPLOYEE.EMPLOYEE_NAME," +
                     "       TEMP_MST_EMPLOYEE.EMPLOYEE_PAN," +
                     "       TEMP_MST_EMPLOYEE.GROUP_ID," +
                     "       TEMP_MST_EMPLOYEE.COMPANY_ID," +
                     "       TEMP_MST_EMPLOYEE.DESIGNATION," +
                     "       TEMP_MST_EMPLOYEE.CATEGORY " +
                     "FROM TEMP_MST_EMPLOYEE " +
                     "LEFT JOIN (SELECT EMPLOYEE_NAME, " +
                     "                  EMPLOYEE_PAN," +
                     "                  COMPANY_ID " +
                     "           FROM MST_EMPLOYEE " +
                     "           WHERE EMPLOYEE_PAN <> 'PANNOTAVBL') AS TEMP_MASTER " +
                     "ON    TEMP_MST_EMPLOYEE.EMPLOYEE_PAN  = TEMP_MASTER.EMPLOYEE_PAN " +
                     "AND   TEMP_MST_EMPLOYEE.COMPANY_ID    = TEMP_MASTER.COMPANY_ID " +
                     "WHERE TEMP_MST_EMPLOYEE.EMPLOYEE_PAN <> 'PANNOTAVBL' " +
                     "AND   TEMP_MASTER.EMPLOYEE_NAME IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "INSERT INTO MST_EMPLOYEE " +
                     "SELECT TEMP_MST_EMPLOYEE.EMPLOYEE_ID," +
                     "       TEMP_MST_EMPLOYEE.EMPLOYEE_NAME," +
                     "       TEMP_MST_EMPLOYEE.EMPLOYEE_PAN," +
                     "       TEMP_MST_EMPLOYEE.GROUP_ID," +
                     "       TEMP_MST_EMPLOYEE.COMPANY_ID," +
                     "       TEMP_MST_EMPLOYEE.DESIGNATION," +
                     "       TEMP_MST_EMPLOYEE.CATEGORY " +
                     "FROM TEMP_MST_EMPLOYEE " +
                     "LEFT JOIN (SELECT EMPLOYEE_NAME, " +
                     "                  EMPLOYEE_PAN," +
                     "                  COMPANY_ID " +
                     "           FROM MST_EMPLOYEE " +
                     "           WHERE EMPLOYEE_PAN = 'PANNOTAVBL') AS TEMP_MASTER " +
                     "ON    TEMP_MST_EMPLOYEE.EMPLOYEE_NAME = TEMP_MASTER.EMPLOYEE_NAME " +
                     "AND   TEMP_MST_EMPLOYEE.COMPANY_ID    = TEMP_MASTER.COMPANY_ID " +
                     "WHERE TEMP_MST_EMPLOYEE.EMPLOYEE_PAN  = 'PANNOTAVBL' " +
                     "AND   TEMP_MASTER.EMPLOYEE_NAME IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING TEMP_EMPLOYEE_ID COLUMN
            strSQL = "UPDATE TEMP_MST_EMPLOYEE " +
                     "INNER JOIN MST_EMPLOYEE " +
                     "ON    TEMP_MST_EMPLOYEE.EMPLOYEE_PAN     = MST_EMPLOYEE.EMPLOYEE_PAN " +
                     "AND   TEMP_MST_EMPLOYEE.COMPANY_ID       = MST_EMPLOYEE.COMPANY_ID " +
                     "SET   TEMP_MST_EMPLOYEE.TEMP_EMPLOYEE_ID = MST_EMPLOYEE.EMPLOYEE_ID " +
                     "WHERE MST_EMPLOYEE.EMPLOYEE_PAN         <> 'PANNOTAVBL' " +
                     "AND   TEMP_MST_EMPLOYEE.EMPLOYEE_PAN    <> 'PANNOTAVBL'";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_MST_EMPLOYEE " +
                     "INNER JOIN MST_EMPLOYEE " +
                     "ON    TEMP_MST_EMPLOYEE.EMPLOYEE_NAME    = MST_EMPLOYEE.EMPLOYEE_NAME " +
                     "AND   TEMP_MST_EMPLOYEE.COMPANY_ID       = MST_EMPLOYEE.COMPANY_ID " +
                     "SET   TEMP_MST_EMPLOYEE.TEMP_EMPLOYEE_ID = MST_EMPLOYEE.EMPLOYEE_ID " +
                     "WHERE MST_EMPLOYEE.EMPLOYEE_PAN          = 'PANNOTAVBL' " +
                     "AND   TEMP_MST_EMPLOYEE.EMPLOYEE_PAN     = 'PANNOTAVBL'";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING PARTY_ID IN TEMP_TRN_DEDUCTEE_DETAILS TABLES
            strSQL = "UPDATE TEMP_TRN_DEDUCTEE_DETAILS " +
                     "INNER JOIN TEMP_MST_EMPLOYEE " +
                     "ON    TEMP_TRN_DEDUCTEE_DETAILS.PARTY_ID = TEMP_MST_EMPLOYEE.EMPLOYEE_ID " +
                     "SET   TEMP_TRN_DEDUCTEE_DETAILS.PARTY_ID = TEMP_MST_EMPLOYEE.TEMP_EMPLOYEE_ID " +
                     "WHERE TEMP_TRN_DEDUCTEE_DETAILS.DEDUCTEE_FLAG = 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING EMPLOYEE_ID IN TEMP_TRN_SALARY_DETAILS TABLES
            strSQL = "UPDATE TEMP_TRN_SALARY_DETAILS " +
                     "INNER JOIN TEMP_MST_EMPLOYEE " +
                     "ON    TEMP_TRN_SALARY_DETAILS.EMPLOYEE_ID = TEMP_MST_EMPLOYEE.EMPLOYEE_ID " +
                     "SET   TEMP_TRN_SALARY_DETAILS.EMPLOYEE_ID = TEMP_MST_EMPLOYEE.TEMP_EMPLOYEE_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            #endregion

            #region DELETE DATA BEYOND THE DB FINANCIAL YEAR FROM TEMP_TRN_BASIC_INFO TABLES

            //DELETE QUERY USING LEFT JOIN

            //DELETING ALL THE BASIC INFO RECORDS BEYOND THE ALLOWED FA YEAR IN THE CURRENT DB
            strSQL = "DELETE DISTINCTROW TEMP_TRN_BASIC_INFO.* " +
                     "FROM TEMP_TRN_BASIC_INFO " +
                     "LEFT JOIN MST_ASSESSMENT " +
                     "ON TEMP_TRN_BASIC_INFO.ASST_ID = MST_ASSESSMENT.ASST_ID " +
                     "WHERE MST_ASSESSMENT.ASST_ID IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //DELETING ALL THE CHALAN RECORDS WHOSE TEMP CHALLAN RECORDS HAS BEEN DELETED
            strSQL = "DELETE DISTINCTROW TEMP_TRN_COMPANY_INFO.* " +
                     "FROM TEMP_TRN_COMPANY_INFO " +
                     "LEFT JOIN TEMP_TRN_BASIC_INFO " +
                     "ON TEMP_TRN_COMPANY_INFO.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.BASIC_INFO_ID " +
                     "WHERE TEMP_TRN_BASIC_INFO.BASIC_INFO_ID IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }


            //DELETING ALL THE CHALAN RECORDS WHOSE TEMP CHALLAN RECORDS HAS BEEN DELETED
            strSQL = "DELETE DISTINCTROW TEMP_TRN_CHALLAN.* " +
                     "FROM TEMP_TRN_CHALLAN " +
                     "LEFT JOIN TEMP_TRN_BASIC_INFO " +
                     "ON TEMP_TRN_CHALLAN.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.BASIC_INFO_ID " +
                     "WHERE TEMP_TRN_BASIC_INFO.BASIC_INFO_ID IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //DELETING ALL THE CHALAN RECORDS WHOSE TEMP CHALLAN RECORDS HAS BEEN DELETED
            strSQL = "DELETE DISTINCTROW TEMP_TRN_DEDUCTEE_DETAILS.* " +
                     "FROM TEMP_TRN_DEDUCTEE_DETAILS " +
                     "LEFT JOIN TEMP_TRN_BASIC_INFO " +
                     "ON TEMP_TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.BASIC_INFO_ID " +
                     "WHERE TEMP_TRN_BASIC_INFO.BASIC_INFO_ID IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //DELETING ALL THE CHALAN RECORDS WHOSE TEMP CHALLAN RECORDS HAS BEEN DELETED
            strSQL = "DELETE DISTINCTROW TEMP_TRN_SALARY_DETAILS.* " +
                     "FROM TEMP_TRN_SALARY_DETAILS " +
                     "LEFT JOIN TEMP_TRN_BASIC_INFO " +
                     "ON TEMP_TRN_SALARY_DETAILS.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.BASIC_INFO_ID " +
                     "WHERE TEMP_TRN_BASIC_INFO.BASIC_INFO_ID IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            #endregion

            #region IMPORT DATA TO TRN_BASIC_INFO TABLES FROM TEMP_TRN_BASIC_INFO TABLES

            //ADDING NEW ID COLUMN IN TEMP_TRN_BASIC_INFO
            strSQL = "ALTER TABLE TEMP_TRN_BASIC_INFO ADD COLUMN TEMP_BASIC_INFO_ID NUMBER DEFAULT 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_BASIC_INFO SET TEMP_BASIC_INFO_ID = 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //ADDING NEW RECORDS INTO TRN_BASIC_INFO
            strSQL = "INSERT INTO TRN_BASIC_INFO " +
                     "SELECT TEMP_TRN_BASIC_INFO.BASIC_INFO_ID," +
                     "       TEMP_TRN_BASIC_INFO.COMPANY_ID," +
                     "       TEMP_TRN_BASIC_INFO.ASST_ID," +
                     "       TEMP_TRN_BASIC_INFO.QTR," +
                     "       TEMP_TRN_BASIC_INFO.FORM_NO," +
                     "       TEMP_TRN_BASIC_INFO.NIL_RETURN," +
                     "       TEMP_TRN_BASIC_INFO.RECEIPT_NO," +
                     "       TEMP_TRN_BASIC_INFO.DATE_OF_FILING," +
                     "       TEMP_TRN_BASIC_INFO.PRN_NO," +
                     "       TEMP_TRN_BASIC_INFO.ADDRESS_CHANGE," +
                     "       TEMP_TRN_BASIC_INFO.P_ADDRESS_CHANGE " +
                     "FROM   TEMP_TRN_BASIC_INFO " +
                     "LEFT JOIN TRN_BASIC_INFO " +
                     "ON     TEMP_TRN_BASIC_INFO.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID " +
                     "AND    TEMP_TRN_BASIC_INFO.ASST_ID    = TRN_BASIC_INFO.ASST_ID " +
                     "AND    TEMP_TRN_BASIC_INFO.FORM_NO    = TRN_BASIC_INFO.FORM_NO " +
                     "AND    TEMP_TRN_BASIC_INFO.QTR        = TRN_BASIC_INFO.QTR " +
                     "WHERE TRN_BASIC_INFO.BASIC_INFO_ID IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING TEMP_BASIC_INFO_ID COLUMN
            strSQL = "UPDATE     TEMP_TRN_BASIC_INFO " +
                     "INNER JOIN TRN_BASIC_INFO " +
                     "ON     TEMP_TRN_BASIC_INFO.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID " +
                     "AND    TEMP_TRN_BASIC_INFO.ASST_ID    = TRN_BASIC_INFO.ASST_ID " +
                     "AND    TEMP_TRN_BASIC_INFO.FORM_NO    = TRN_BASIC_INFO.FORM_NO " +
                     "AND    TEMP_TRN_BASIC_INFO.QTR        = TRN_BASIC_INFO.QTR " +
                     "SET        TEMP_TRN_BASIC_INFO.TEMP_BASIC_INFO_ID  = TRN_BASIC_INFO.BASIC_INFO_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING BASIC_INFO_ID IN ALL TEMP_TRN_ TABLES
            strSQL = "UPDATE TEMP_TRN_CHALLAN " +
                     "INNER JOIN TEMP_TRN_BASIC_INFO " +
                     "ON TEMP_TRN_CHALLAN.BASIC_INFO_ID  = TEMP_TRN_BASIC_INFO.BASIC_INFO_ID " +
                     "SET TEMP_TRN_CHALLAN.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.TEMP_BASIC_INFO_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_COMPANY_INFO " +
                     "INNER JOIN TEMP_TRN_BASIC_INFO " +
                     "ON TEMP_TRN_COMPANY_INFO.BASIC_INFO_ID  = TEMP_TRN_BASIC_INFO.BASIC_INFO_ID " +
                     "SET TEMP_TRN_COMPANY_INFO.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.TEMP_BASIC_INFO_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_DEDUCTEE_DETAILS " +
                     "INNER JOIN TEMP_TRN_BASIC_INFO " +
                     "ON  TEMP_TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.BASIC_INFO_ID " +
                     "SET TEMP_TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.TEMP_BASIC_INFO_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_SALARY_DETAILS " +
                     "INNER JOIN TEMP_TRN_BASIC_INFO " +
                     "ON  TEMP_TRN_SALARY_DETAILS.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.BASIC_INFO_ID " +
                     "SET TEMP_TRN_SALARY_DETAILS.BASIC_INFO_ID = TEMP_TRN_BASIC_INFO.TEMP_BASIC_INFO_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            #endregion

            #region IMPORT DATA TO TRN_CHALLAN TABLES FROM TEMP_TRN_CHALLAN TABLES

            //ADDING NEW RECORDS INTO TRN_CHALLAN
            strSQL = "INSERT INTO TRN_CHALLAN " +
                     "SELECT TEMP_TRN_CHALLAN.* " +
                     "FROM   TEMP_TRN_CHALLAN " +
                     "LEFT JOIN TRN_CHALLAN " +
                     "ON     TEMP_TRN_CHALLAN.BASIC_INFO_ID = TRN_CHALLAN.BASIC_INFO_ID " +
                     "AND    TEMP_TRN_CHALLAN.SL_NO         = TRN_CHALLAN.SL_NO " +
                     "WHERE TRN_CHALLAN.BASIC_INFO_ID IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //ADDING NEW ID COLUMN IN TEMP_TRN_CHALLAN
            strSQL = "ALTER TABLE TEMP_TRN_CHALLAN ADD COLUMN TEMP_CHALLAN_ID NUMBER DEFAULT 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            strSQL = "UPDATE TEMP_TRN_CHALLAN SET TEMP_CHALLAN_ID = 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING TEMP_CHALLAN_ID COLUMN
            strSQL = "UPDATE TEMP_TRN_CHALLAN " +
                     "INNER JOIN TRN_CHALLAN " +
                     "ON     TEMP_TRN_CHALLAN.BASIC_INFO_ID = TRN_CHALLAN.BASIC_INFO_ID " +
                     "AND    TEMP_TRN_CHALLAN.SL_NO         = TRN_CHALLAN.SL_NO " +
                     "SET TEMP_TRN_CHALLAN.TEMP_CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            //UPDATING CHALLAN_ID IN ALL TEMP_TRN_ TABLES
            strSQL = "UPDATE TEMP_TRN_DEDUCTEE_DETAILS " +
                     "INNER JOIN TEMP_TRN_CHALLAN " +
                     "ON  TEMP_TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TEMP_TRN_CHALLAN.CHALLAN_ID " +
                     "SET TEMP_TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TEMP_TRN_CHALLAN.TEMP_CHALLAN_ID";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            #endregion

            #region IMPORT DATA TO TRN_COMPANY_INFO TABLE

            strSQL = "INSERT INTO TRN_COMPANY_INFO " +
                     "SELECT TEMP_TRN_COMPANY_INFO.BASIC_INFO_ID," +
                     "       TEMP_TRN_COMPANY_INFO.COMPANY_ID," +
                     "       TEMP_TRN_COMPANY_INFO.GROUP_ID," +
                     "       TEMP_TRN_COMPANY_INFO.ASST_ID," +
                     "       TEMP_TRN_COMPANY_INFO.TAN_NO," +
                     "       TEMP_TRN_COMPANY_INFO.PAN_NO," +
                     "       TEMP_TRN_COMPANY_INFO.COMPANY_NAME," +
                     "       TEMP_TRN_COMPANY_INFO.D_CATEGORY_ID," +
                     "       TEMP_TRN_COMPANY_INFO.D_STATE_ID," +
                     "       TEMP_TRN_COMPANY_INFO.BRANCH_DIV," +
                     "       TEMP_TRN_COMPANY_INFO.MINISTRY_ID," +
                     "       TEMP_TRN_COMPANY_INFO.MINISTRY_OTHER," +
                     "       TEMP_TRN_COMPANY_INFO.PAO_CODE," +
                     "       TEMP_TRN_COMPANY_INFO.PAO_REG_NO," +
                     "       TEMP_TRN_COMPANY_INFO.DDO_CODE," +
                     "       TEMP_TRN_COMPANY_INFO.DDO_REG_NO," +
                     "       TEMP_TRN_COMPANY_INFO.ADDRESS1," +
                     "       TEMP_TRN_COMPANY_INFO.ADDRESS2," +
                     "       TEMP_TRN_COMPANY_INFO.ADDRESS3," +
                     "       TEMP_TRN_COMPANY_INFO.ADDRESS4," +
                     "       TEMP_TRN_COMPANY_INFO.ADDRESS5," +
                     "       TEMP_TRN_COMPANY_INFO.STATE_ID," +
                     "       TEMP_TRN_COMPANY_INFO.PIN_CODE," +
                     "       TEMP_TRN_COMPANY_INFO.PHONE," +
                     "       TEMP_TRN_COMPANY_INFO.STD," +
                     "       TEMP_TRN_COMPANY_INFO.EMAIL," +
                     "       TEMP_TRN_COMPANY_INFO.PERSON_NAME," +
                     "       TEMP_TRN_COMPANY_INFO.DESIGNATION," +
                     "       TEMP_TRN_COMPANY_INFO.FATHER_NAME," +
                     "       TEMP_TRN_COMPANY_INFO.P_ADDRESS1," +
                     "       TEMP_TRN_COMPANY_INFO.P_ADDRESS2," +
                     "       TEMP_TRN_COMPANY_INFO.P_ADDRESS3," +
                     "       TEMP_TRN_COMPANY_INFO.P_ADDRESS4," +
                     "       TEMP_TRN_COMPANY_INFO.P_ADDRESS5," +
                     "       TEMP_TRN_COMPANY_INFO.P_STATE_ID," +
                     "       TEMP_TRN_COMPANY_INFO.P_PIN_CODE," +
                     "       TEMP_TRN_COMPANY_INFO.P_PHONE," +
                     "       TEMP_TRN_COMPANY_INFO.P_STD," +
                     "       TEMP_TRN_COMPANY_INFO.P_EMAIL," +
                     "       TEMP_TRN_COMPANY_INFO.P_MOBILE," +
                     "       TEMP_TRN_COMPANY_INFO.ASSESSING_OFFICER_CODE," +
                     "       TEMP_TRN_COMPANY_INFO.TDS_CIRCLE," +
                     "       TEMP_TRN_COMPANY_INFO.FILE_PREFIX," +
                     "       TEMP_TRN_COMPANY_INFO.CIT_TDS_ADDRESS," +
                     "       TEMP_TRN_COMPANY_INFO.CIT_TDS_CITY," +
                     "       TEMP_TRN_COMPANY_INFO.CIT_TDS_PINCODE," +
                     "       TEMP_TRN_COMPANY_INFO.ADDRESS_CHANGE," +
                     "       TEMP_TRN_COMPANY_INFO.P_ADDRESS_CHANGE," +
                     "       TEMP_TRN_COMPANY_INFO.UPDATE_FLAG " +
                     "FROM TEMP_TRN_COMPANY_INFO " +
                     "LEFT JOIN TRN_COMPANY_INFO " +
                     "ON TEMP_TRN_COMPANY_INFO.BASIC_INFO_ID = TRN_COMPANY_INFO.BASIC_INFO_ID " +
                     "WHERE TRN_COMPANY_INFO.BASIC_INFO_ID IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            #endregion

            #region IMPORT DATA TO TRN_DEDUCTEE_DETAILS TABLE

            strSQL = "INSERT INTO TRN_DEDUCTEE_DETAILS " +
                     "SELECT TEMP_TRN_DEDUCTEE_DETAILS.CHALLAN_ID," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.SL_NO," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.PARTY_ID," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.RATE," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.TAX_AMOUNT," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.CESS_AMOUNT," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.NON_DEDUCTION_FLAG," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.FROM_DATE," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.TO_DATE," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR," +
                     "       TEMP_TRN_DEDUCTEE_DETAILS.TOT_VALUE_PURCHASE " + 
                     "FROM TEMP_TRN_DEDUCTEE_DETAILS " +
                     "LEFT JOIN TRN_DEDUCTEE_DETAILS " +
                     "ON  TEMP_TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                     "AND TEMP_TRN_DEDUCTEE_DETAILS.SL_NO      = TRN_DEDUCTEE_DETAILS.SL_NO " +
                     "WHERE TRN_DEDUCTEE_DETAILS.CHALLAN_ID IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            #endregion

            #region IMPORT DATA TO TRN_SALARY_DETAILS TABLE

            strSQL = "INSERT INTO TRN_SALARY_DETAILS " +
                     "SELECT TEMP_TRN_SALARY_DETAILS.BASIC_INFO_ID," +
                     "       TEMP_TRN_SALARY_DETAILS.EMPLOYEE_ID," +
                     "       TEMP_TRN_SALARY_DETAILS.FROM_DATE," +
                     "       TEMP_TRN_SALARY_DETAILS.TO_DATE," +
                     "       TEMP_TRN_SALARY_DETAILS.TS_GS_SEC_17_1," +
                     "       TEMP_TRN_SALARY_DETAILS.TS_GS_SEC_17_2," +
                     "       TEMP_TRN_SALARY_DETAILS.TS_GS_SEC_17_3," +
                     "       TEMP_TRN_SALARY_DETAILS.TS_GS_TOTAL," +
                     "       TEMP_TRN_SALARY_DETAILS.TS_LA_ITEM_1_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.TS_LA_ITEM_1," +
                     "       TEMP_TRN_SALARY_DETAILS.TS_LA_ITEM_2_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.TS_LA_ITEM_2," +
                     "       TEMP_TRN_SALARY_DETAILS.TS_LA_TOTAL," +
                     "       TEMP_TRN_SALARY_DETAILS.TS_BALANCE," +
                     "       TEMP_TRN_SALARY_DETAILS.US_16_EA," +
                     "       TEMP_TRN_SALARY_DETAILS.US_16_TE," +
                     "       TEMP_TRN_SALARY_DETAILS.US_16_AGGREGATE," +
                     "       TEMP_TRN_SALARY_DETAILS.INCOME_CHARGEABLE," +
                     "       TEMP_TRN_SALARY_DETAILS.AIS_ITEM_1_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.AIS_ITEM_1," +
                     "       TEMP_TRN_SALARY_DETAILS.AIS_ITEM_2_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.AIS_ITEM_2," +
                     "       TEMP_TRN_SALARY_DETAILS.AIS_ITEM_3_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.AIS_ITEM_3," +
                     "       TEMP_TRN_SALARY_DETAILS.AIS_ITEM_4_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.AIS_ITEM_4," +
                     "       TEMP_TRN_SALARY_DETAILS.AIS_Total," +
                     "       TEMP_TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_1_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_1," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_2_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_2," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_3_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_3," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_4_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_4," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_5_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_5," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_6_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_6," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_GROSS_TOTAL," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80C_DED_TOTAL," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80CCC_GROSS_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80CCC_DED_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80CCD_GROSS_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80CCD_DED_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_GROSS_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_QUAL_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DED_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_GROSS_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_QUAL_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DED_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_GROSS_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_QUAL_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DED_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_GROSS_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_QUAL_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DED_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DESC," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_GROSS_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_QUAL_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DED_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_DED_TOTAL," +
                     "       TEMP_TRN_SALARY_DETAILS.TOTAL_INCOME," +
                     "       TEMP_TRN_SALARY_DETAILS.TAX_TOTAL_INCOME," +
                     "       TEMP_TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME," +
                     "       TEMP_TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME," +
                     "       TEMP_TRN_SALARY_DETAILS.TAX_PAYABLE_AGGREGATE," +
                     "       TEMP_TRN_SALARY_DETAILS.US_89_LESS," +
                     "       TEMP_TRN_SALARY_DETAILS.TAX_PAYABLE," +
                     "       TEMP_TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED," +
                     "       TEMP_TRN_SALARY_DETAILS.SHORTFALL_TAX," +
                     "       TEMP_TRN_SALARY_DETAILS.ENTRY_MODE," +
                     "       TEMP_TRN_SALARY_DETAILS.SL_NO," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80CCF_GROSS_AMOUNT," +
                     "       TEMP_TRN_SALARY_DETAILS.CVIA_SEC80CCF_DED_AMOUNT " +
                     "FROM TEMP_TRN_SALARY_DETAILS " +
                     "LEFT JOIN TRN_SALARY_DETAILS " +
                     "ON  TEMP_TRN_SALARY_DETAILS.BASIC_INFO_ID = TRN_SALARY_DETAILS.BASIC_INFO_ID " +
                     "AND TEMP_TRN_SALARY_DETAILS.SL_NO         = TRN_SALARY_DETAILS.SL_NO " +
                     "WHERE TRN_SALARY_DETAILS.BASIC_INFO_ID IS NULL";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }

            #endregion


            dmlService.J_Commit();
            //
            prgBar.Value = prgBar.Value + 5;
            this.Refresh();
            //
            for (int i = prgBar.Minimum; i <= prgBar.Maximum; i++)
            {
                prgBar.PerformStep();
            }
            this.Cursor = Cursors.Default;
            //
            txtFVUPath.Text = "";
            //---------------
            cmnService.J_UserMessage("Import Completed");
            //
            prgBar.Value = 0;
            //            
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                //ADDED BY SHREY KEJRIWAL ON 18/03/2012
                //FILE PATH BLANK CHECK
                if (txtFVUPath.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Select the TDS file that you want to import.");
                    btnSelectTDSPath.Select();
                    return false;
                }


                // FILE EXIST
                if (cmnService.J_IsFileExist(txtFVUPath.Text) == false)
                {
                    cmnService.J_UserMessage("Selected File not found");
                    btnSelectTDSPath.Select();
                    return false;
                }

                // FILE OPEN
                if (cmnService.J_IsProcessOpen(txtFVUPath.Text) == true)
                {
                    cmnService.J_UserMessage("Selected File is open");
                    btnSelectTDSPath.Select();
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

        #region ImportTransactionsFromFVU
        private bool ImportTransactionsFromFVU(string FVUPath)
        {
            try
            {
                //TABLE NAME FOR DATA IMPORT
                string[] strImporttableName = {"COR_HDR_BATCH",
                                               "COR_HDR_CHALLAN",
                                               "COR_HDR_COMPANY",
                                               "COR_HDR_DEDUCTEE_DETAILS",
                                               "COR_HDR_SALARY_DETAILS",
                                               "COR_TRN_CHALLAN",
                                               "COR_TRN_COMPANY",
                                               "COR_TRN_DEDUCTEE_DETAILS",
                                               "COR_TRN_SALARY_DETAILS",
                                               "MST_ASSESSMENT",
                                               "MST_COMPANY",
                                               "MST_DEDUCTEE",
                                               "MST_EMPLOYEE",
                                               "TRN_BASIC_INFO",
                                               "TRN_CHALLAN",
                                               "TRN_COMPANY_INFO",
                                               "TRN_DEDUCTEE_DETAILS",
                                               "TRN_SALARY_DETAILS"};

                string[] strDumptableName = {"TEMP_COR_HDR_BATCH",
                                             "TEMP_COR_HDR_CHALLAN",
                                             "TEMP_COR_HDR_COMPANY",
                                             "TEMP_COR_HDR_DEDUCTEE_DETAILS",
                                             "TEMP_COR_HDR_SALARY_DETAILS",
                                             "TEMP_COR_TRN_CHALLAN",
                                             "TEMP_COR_TRN_COMPANY",
                                             "TEMP_COR_TRN_DEDUCTEE_DETAILS",
                                             "TEMP_COR_TRN_SALARY_DETAILS",
                                             "TEMP_MST_ASSESSMENT",
                                             "TEMP_MST_COMPANY",
                                             "TEMP_MST_DEDUCTEE",
                                             "TEMP_MST_EMPLOYEE",
                                             "TEMP_TRN_BASIC_INFO",
                                             "TEMP_TRN_CHALLAN",
                                             "TEMP_TRN_COMPANY_INFO",
                                             "TEMP_TRN_DEDUCTEE_DETAILS",
                                             "TEMP_TRN_SALARY_DETAILS"};


                #region TABLE to TEXT

                cmnService.J_DeleteDirectory(Path.Combine(Application.StartupPath, "Prev Year Tables"));

                //Creation of TEXT file from REMOTE table
                if (T_TextFileFromTable(strImporttableName, true) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }

                #endregion


                prgBar.Value = prgBar.Value + 10;
                this.Refresh();

                for (int i = 0; i <= strDumptableName.Length - 1; i++)
                {
                    File.Copy(OutputFolder + "\\" + strImporttableName[i].ToString() + ".txt", OutputFolder + "\\" + strDumptableName[i].ToString() + ".txt");
                    File.Delete(OutputFolder + "\\" + strImporttableName[i].ToString() + ".txt");
                }

                #region TEXT file Import

                //Creation of TEMP table from .txt file
                if (T_BulkImportFromTextFile(strDumptableName, true) == false)
                {
                    dmlService.J_Rollback();
                    return false;
                }
                
                #endregion

                prgBar.Value = prgBar.Value + 10;
                this.Refresh();
            
                //Deleting .TXT file
                for (int i = 0; i <= strDumptableName.Length - 1; i++)
                {
                    File.Delete(OutputFolder + "\\" + strDumptableName[i].ToString() + ".txt");
                }

                return true;
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(strImportErrorMessage);
                return false;
            }
        }

        #endregion

        #region T_TextFileFromTable
        private bool T_TextFileFromTable(string[] tbl, bool FirstRowAsColumnHeader)
        {
            cmnService.J_CreateDirectory(Path.Combine(Application.StartupPath, "Prev Year Tables"));

            //Recreating schema file
            if (File.Exists(OutputFolder + "\\schema.ini") == true)
                File.Delete(OutputFolder + "\\schema.ini");

            StreamWriter StreamWriter = new StreamWriter(OutputFolder + "\\schema.ini");
            for (int i = 0; i <= tbl.Length - 1; i++)
            {
                StreamWriter.WriteLine("[" + tbl[i].ToString() + ".txt]");
                StreamWriter.WriteLine("ColNameHeader=" + (FirstRowAsColumnHeader == true ? "true" : "false") + "");
                StreamWriter.WriteLine("Format=Delimited(^)");
                StreamWriter.WriteLine("TextDelimeter=\"none\"");
                StreamWriter.WriteLine("MaxScanRows=0");
                StreamWriter.WriteLine("CharacterSet=1252");
                StreamWriter.WriteLine("DateTimeFormat=dd/MM/yyyy");
            }
            StreamWriter.Close();

            //OPENING SOURCE DATABASE
            DMLService dmlServiceSourceDB = new DMLService(J_DatabaseType.MsAccess, J_ConnectionProviderType.OleDb);

            //CHECKING IF THE CONNECTION CAN BE MADE TO THE DB
            if (dmlServiceSourceDB.J_ValidateConnection(strFVUPath, "mother") == false)
            {
                dmlServiceSourceDB.Dispose();
                cmnService.J_UserMessage("Invalid database file selected.");
                return false;
            }            
            //QUERY TO IMPORT THE DATA FROM TEXT FILE
            for (int i = 0; i <= tbl.Length - 1; i++)
            {
                strSQL = "SELECT * INTO [Text; DATABASE=" + OutputFolder + "].[" + tbl[i].ToString() + ".txt" + "] " +
                         "FROM [" + tbl[i].ToString() + "] ";

                if (dmlServiceSourceDB.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
            }
            dmlServiceSourceDB.Dispose();
            return true;
        }

        #endregion

        #region T_BulkImportFromTextFile
        private bool T_BulkImportFromTextFile(string[] tbl, bool FirstRowAsColumnHeader)
        {
            //Recreating schema file
            if (File.Exists(OutputFolder + "\\schema.ini") == true)
                File.Delete(OutputFolder + "\\schema.ini");

            StreamWriter StreamWriter = new StreamWriter(OutputFolder + "\\schema.ini");
            for (int i = 0; i <= tbl.Length - 1; i++)
            {
                StreamWriter.WriteLine("[" + tbl[i].ToString() + ".txt]");
                StreamWriter.WriteLine("ColNameHeader=" + (FirstRowAsColumnHeader == true ? "true" : "false") + "");
                StreamWriter.WriteLine("Format=Delimited(^)");
                StreamWriter.WriteLine("MaxScanRows=0");
                StreamWriter.WriteLine("CharacterSet=ANSI");

                #region DEFINING THE COLUMN NAMES

                strSQL = "SELECT TOP 1 * FROM " + cmnService.J_Mid(tbl[i].ToString(), 5, (tbl[i].ToString().Length)-5);
                DataTable datatable = dmlService.J_ExecSqlReturnDataTable(strSQL);

                //----WRITE COLUMN DETAILS 
                int intCounter = 1;
                string strDesc = "";

                foreach (DataColumn column in datatable.Columns)
                {
                    switch (column.DataType.Name)
                    {
                        case "Int16":
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Integer";
                            break;
                        case "Double":
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Double";
                            break;
                        case J_DataTableDataType.Integer:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Integer";
                            break;
                        case J_DataTableDataType.Long:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Integer";
                            break;
                        case J_DataTableDataType.Decimal:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Currency";
                            break;
                        case J_DataTableDataType.String:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Char";
                            break;
                        case J_DataTableDataType.DateTime:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Date";
                            break;
                        case J_DataTableDataType.Byte:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Byte";
                            break;
                        case J_DataTableDataType.Boolean:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Bit";
                            break;
                        case J_DataTableDataType.Currency:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Currency";
                            break;
                        case J_DataTableDataType.Binary:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " OLE";
                            break;
                    }
                    StreamWriter.WriteLine(strDesc);
                    intCounter++;
                }
                datatable.Dispose();
                
                #endregion
            }
            StreamWriter.Close();

            for (int i = 0; i <= tbl.Length - 1; i++)
            {
                //CREATING THE IMPORT TABLE
                if (dmlService.J_IsDatabaseObjectExist(tbl[i].ToString()) == true)
                {
                    strSQL = "DROP TABLE [" + tbl[i].ToString() + "]";

                    if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false)
                    {
                        dmlService.J_Rollback();
                        cmnService.J_UserMessage("Import Failed!");
                        return false;
                    }
                }

                //QUERY TO IMPORT THE DATA FROM TEXT FILE
                strSQL = "SELECT * INTO [" + tbl[i].ToString() + "] FROM " +
                         "[Text; DATABASE=" + OutputFolder + "].[" + tbl[i].ToString() + ".txt" + "]";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    cmnService.J_UserMessage("Import Failed!");
                    return false;
                }
            }
            return true;
        }

        #endregion

        #endregion

    }
}

