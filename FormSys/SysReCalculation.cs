
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: SysReCalculation
Version			: 1.0
Start Date		: 12/10/2015
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
using TDSMAN.FormMst;
using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion


namespace TDSMAN.FormSys
{
    public partial class SysReCalculation : Form
    {
        #region System Generated Code
        public SysReCalculation()
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
        long lngSearchId;					//For Storing the Id
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        //DataSet dsetGridClone = new DataSet();
        //DataSet dsetChallanGridClone = new DataSet();
        //DataSet dsetChallanDetailsGridClone = new DataSet();
        //RptDialog rptDialog = new RptDialog();
        //-----------------------------------------------------------------------
        string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        //-----------------------------------------------------------------------
        string strSQLGridViewTabPages;
        long lngBasicInfoID;
        long lngChallanID;
        //long lngChallanDetailID;
        long lngDeducteeDetailID;
        long lngDeducteeID;
        string strSQLShowHelpDeductee;
        string strSQLShowHelpPAN;

        string strStateCode;
        string strRPStateCode;
        string strDStateCode;
        string strMinistryCode;

        string strBatFile = "";
        string strFVUFile = "";
        string strCSIDownloadFilePath = "";

        //Added by Shrey Kejriwal on 19/01/2011
        string strConsolidatedStatementPath = "";

        string newOutputFileName = "";

        string[,] strArray;

        bool blnShowHelp = true;
        bool blnShowPANHelp = true;
        bool blnChkChanged = true;
        bool blnSectionDisplay = true;
        bool blnSectionDDDisplay = true;
        bool blnRegularStatemnt = true;
        //--            
        IDataReader drdShowDeducteePAN = null;
        ToolTip tllTip = new ToolTip();
        //--
        string strPassword="";

        
        //----

        #endregion

        #region T_GET_CHALLAN
        public enum T_GET_CHALLAN
        {
            CHALLAN_ID = 0,
            CTRL_TDS = 1,
            CTRL_SURCHARGE = 2,
            CTRL_EDU_CESS = 3,
            CTRL_TOT_TAX = 4,
            CTRL_TOT = 5
        }
        #endregion

        #region  User Defined Events

        #region SysReCalculation_Load
        private void SysReCalculation_Load(object sender, EventArgs e)
        {
            //-----------
            //-- FINANCIAL YEAR
            //-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";

            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
                
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //--
                #region VALIDATION
                if (cmbFinancialYear.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select Financial Year");
                    cmbFinancialYear.Select();
                    return;
                }
                //
                bool blnReSerial = false;
                if (chkReserialising.Checked == true)
                    blnReSerial = true;
                //
                bool blnReCalc = false;
                if (chkRecalculation.Checked == true)
                    blnReCalc = true;
                //
                bool blnReIndex = false;
                if (ckhReIndexing.Checked == true)
                    blnReIndex = true;
                //
                if (blnReSerial == false && blnReCalc == false && blnReIndex == false)
                {
                    cmnService.J_UserMessage("Please check any option");
                    chkReserialising.Select();
                    return;
                }
                else if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }
                //--
                #endregion
                //--
                this.Cursor = Cursors.WaitCursor; 
                //--
                EnablDisable(false);
                //--
                DataSet dsetChallanDetails = new DataSet();
                //--
                if (blnReCalc == true)
                {
                    #region RECALCULATION
                    //-- REGULAR RETURN
                    #region REGULAR RETURN
                    strSQL = @"SELECT CHALLAN_ID,
                                  SUM(TAX_AMOUNT)           AS CTRL_TDS, 
                                  SUM(SURCHARGE_AMOUNT)     AS CTRL_SURCHARGE, 
                                  SUM(CESS_AMOUNT)          AS CTRL_EDU_CESS, 
                                  SUM(TOTAL_AMOUNT)         AS CTRL_TOT_TAX, 
                                  SUM(TAX_DEPOSITED_AMOUNT) AS CTRL_TOT
                           FROM   TRN_DEDUCTEE_DETAILS, 
                                  TRN_BASIC_INFO, 
                                  MST_ASSESSMENT 
                           WHERE  MST_ASSESSMENT.ASST_ID             = TRN_BASIC_INFO.ASST_ID
                           AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                           AND    TRN_BASIC_INFO.ASST_ID             = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                           GROUP BY TRN_DEDUCTEE_DETAILS.CHALLAN_ID";
                    //
                    dsetChallanDetails = dmlService.J_ExecSqlReturnDataSet(strSQL);
                    //
                    foreach (DataRow myDataRow in dsetChallanDetails.Tables[0].Rows)
                    {
                        //Stores info in Datarow into an array
                        //Object[] cells = myDataRow.ItemArray;                        
                        //
                        strSQL = "UPDATE TRN_CHALLAN " +
                        "         SET    CTRL_TDS       = " + myDataRow[(int)T_GET_CHALLAN.CTRL_TDS] + "," +
                        "                CTRL_SURCHARGE = " + myDataRow[(int)T_GET_CHALLAN.CTRL_SURCHARGE] + "," +
                        "                CTRL_EDU_CESS  = " + myDataRow[(int)T_GET_CHALLAN.CTRL_EDU_CESS] + "," +
                        "                CTRL_TOT       = " + myDataRow[(int)T_GET_CHALLAN.CTRL_TOT] + "," +
                        "                CTRL_TOT_TAX   = " + myDataRow[(int)T_GET_CHALLAN.CTRL_TOT_TAX] + " " +
                        "         WHERE  CHALLAN_ID     = " + myDataRow[(int)T_GET_CHALLAN.CHALLAN_ID] + " ";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            EnablDisable(true);
                            //--
                            this.Cursor = Cursors.Default;
                            //--
                        }
                    }
                    //
                    dsetChallanDetails.Dispose();
                    //--
                    #endregion
                    //-- CORRECTION RETURN
                    #region CORRECTION RETURN

                    strSQL = @"SELECT TRN_CHALLAN_ID,
                                  SUM(TAX_AMOUNT)           AS CTRL_TDS, 
                                  SUM(SURCHARGE_AMOUNT)     AS CTRL_SURCHARGE, 
                                  SUM(CESS_AMOUNT)          AS CTRL_EDU_CESS, 
                                  SUM(TOTAL_AMOUNT)         AS CTRL_TOT_TAX, 
                                  SUM(TAX_DEPOSITED_AMOUNT) AS CTRL_TOT
                           FROM   COR_TRN_DEDUCTEE_DETAILS, 
                                  COR_HDR_BATCH, 
                                  MST_ASSESSMENT 
                           WHERE  MST_ASSESSMENT.ASST_ID             = COR_HDR_BATCH.ASST_ID
                           AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID
                           AND    COR_HDR_BATCH.ASST_ID             = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                           GROUP BY COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID";
                    //
                    dsetChallanDetails = dmlService.J_ExecSqlReturnDataSet(strSQL);
                    //
                    foreach (DataRow myDataRow in dsetChallanDetails.Tables[0].Rows)
                    {
                        //
                        strSQL = "UPDATE COR_TRN_CHALLAN " +
                        "         SET    CTRL_TDS       = " + myDataRow[(int)T_GET_CHALLAN.CTRL_TDS] + "," +
                        "                CTRL_SURCHARGE = " + myDataRow[(int)T_GET_CHALLAN.CTRL_SURCHARGE] + "," +
                        "                CTRL_EDU_CESS  = " + myDataRow[(int)T_GET_CHALLAN.CTRL_EDU_CESS] + "," +
                        "                CTRL_TOT       = " + myDataRow[(int)T_GET_CHALLAN.CTRL_TOT] + "," +
                        "                CTRL_TOT_TAX   = " + myDataRow[(int)T_GET_CHALLAN.CTRL_TOT_TAX] + " " +
                        "         WHERE  TRN_CHALLAN_ID = " + myDataRow[(int)T_GET_CHALLAN.CHALLAN_ID] + " ";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            EnablDisable(true);
                            //--
                            this.Cursor = Cursors.Default;
                            //--
                        }
                    }
                    //
                    dsetChallanDetails.Dispose();
                    //--
                    #endregion
                    //
                    #endregion
                }
                //--
                if (blnReSerial == true)
                {
                    #region RESERIALISATION
                    //-- REGULAR RETURN
                    #region REGULAR RETURN
                    //--
                    #region CHALLAN
                    strSQL = @"SELECT TOP 1 COUNT(*) 
                               FROM   TRN_CHALLAN,
                                      TRN_BASIC_INFO
                               WHERE  TRN_CHALLAN.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                               AND    TRN_BASIC_INFO.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"                               
                               GROUP BY TRN_CHALLAN.BASIC_INFO_ID, 
                                      TRN_CHALLAN.SL_NO
                               HAVING COUNT(TRN_CHALLAN.SL_NO) > 1";
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        DataSet dsetChallan = new DataSet();
                        //--
                        strSQL = @"SELECT TRN_CHALLAN.BASIC_INFO_ID 
                                   FROM   TRN_CHALLAN,
                                          TRN_BASIC_INFO
                                   WHERE  TRN_CHALLAN.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                                   AND    TRN_BASIC_INFO.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"                               
                                   GROUP BY TRN_CHALLAN.BASIC_INFO_ID, 
                                          TRN_CHALLAN.SL_NO
                                   HAVING COUNT(TRN_CHALLAN.SL_NO) > 1";
                        //--
                        dsetChallan = dmlService.J_ExecSqlReturnDataSet(strSQL);
                        //
                        foreach (DataRow myDataRow in dsetChallan.Tables[0].Rows)
                        {
                            //--
                            strSQL = @"SELECT CHALLAN_ID FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + myDataRow[0] + " ORDER BY CHALLAN_ID";
                            dsetChallanDetails = dmlService.J_ExecSqlReturnDataSet(strSQL);
                            //
                            long lngSerialNo = 1;
                            //
                            foreach (DataRow myDataRow1 in dsetChallanDetails.Tables[0].Rows)
                            {
                                //--
                                strSQL = @"UPDATE TRN_CHALLAN SET SL_NO = " + lngSerialNo + " WHERE CHALLAN_ID = " + myDataRow1[0];
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    EnablDisable(true);
                                    //--
                                    this.Cursor = Cursors.Default;
                                    //--
                                }
                                lngSerialNo = lngSerialNo + 1;
                            }
                            //
                            dsetChallanDetails.Dispose();
                            //--
                        }
                        dsetChallan.Dispose();
                    }
                    #endregion
                    //--
                    #region 24Q-26Q-27Q-27EQ
                    strSQL = @"SELECT TOP 1 COUNT(*)
                               FROM   TRN_DEDUCTEE_DETAILS,
                                      TRN_BASIC_INFO
                               WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                               AND    TRN_BASIC_INFO.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                               GROUP BY CHALLAN_ID, SL_NO
                               HAVING COUNT(SL_NO) > 1 ";
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        DataSet dsetDeducteeDetails = new DataSet();
                        //--
                        strSQL = @"SELECT CHALLAN_ID
                                   FROM   TRN_DEDUCTEE_DETAILS,
                                          TRN_BASIC_INFO
                                   WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                                   AND    TRN_BASIC_INFO.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                                   GROUP BY CHALLAN_ID, SL_NO
                                   HAVING COUNT(SL_NO) > 1";
                        dsetChallanDetails = dmlService.J_ExecSqlReturnDataSet(strSQL);
                        //
                        foreach (DataRow myDataRow in dsetChallanDetails.Tables[0].Rows)
                        {
                            //--
                            strSQL = @"SELECT DEDUCTEE_DETAIL_ID FROM TRN_DEDUCTEE_DETAILS WHERE CHALLAN_ID = " + myDataRow[0] + " ORDER BY DEDUCTEE_DETAIL_ID";
                            dsetDeducteeDetails = dmlService.J_ExecSqlReturnDataSet(strSQL);
                            //
                            long lngSerialNo = 1;
                            //
                            foreach (DataRow myDataRow1 in dsetDeducteeDetails.Tables[0].Rows)
                            {
                                //-- Stores info in Datarow into an array
                                //Object[] cells1 = myDataRow1.ItemArray;
                                //--
                                strSQL = @"UPDATE TRN_DEDUCTEE_DETAILS SET SL_NO = " + lngSerialNo + " WHERE DEDUCTEE_DETAIL_ID = " + myDataRow1[0];
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    EnablDisable(true);
                                    //--
                                    this.Cursor = Cursors.Default;
                                    //--
                                }
                                lngSerialNo = lngSerialNo + 1;
                            }
                            //
                            dsetDeducteeDetails.Dispose();
                            //--
                        }
                        //
                    }
                    //
                    dsetChallanDetails.Dispose();
                    //
                    #endregion
                    //
                    #region SALARY DETAILS

                    strSQL = @"SELECT TOP 1 COUNT(*)
                               FROM   TRN_SALARY_DETAILS,
                                      TRN_BASIC_INFO
                               WHERE  TRN_SALARY_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                               AND    TRN_BASIC_INFO.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                               GROUP BY TRN_SALARY_DETAILS.BASIC_INFO_ID, SL_NO
                               HAVING COUNT(SL_NO) > 1 ";
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        DataSet dsetDeducteeDetails = new DataSet();
                        //--
                        strSQL = @"SELECT TRN_SALARY_DETAILS.BASIC_INFO_ID
                                   FROM   TRN_SALARY_DETAILS,
                                          TRN_BASIC_INFO
                                   WHERE  TRN_SALARY_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                                   AND    TRN_BASIC_INFO.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                                   GROUP BY TRN_SALARY_DETAILS.BASIC_INFO_ID, SL_NO
                                   HAVING COUNT(SL_NO) > 1";
                        dsetChallanDetails = dmlService.J_ExecSqlReturnDataSet(strSQL);
                        //
                        foreach (DataRow myDataRow in dsetChallanDetails.Tables[0].Rows)
                        {
                            //-- Stores info in Datarow into an array
                            //Object[] cells = myDataRow.ItemArray;
                            //--
                            strSQL = @"SELECT SALARY_DETAILS_ID FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + myDataRow[0];
                            dsetDeducteeDetails = dmlService.J_ExecSqlReturnDataSet(strSQL);
                            //
                            long lngSerialNo = 1;
                            //
                            foreach (DataRow myDataRow1 in dsetDeducteeDetails.Tables[0].Rows)
                            {
                                //-- Stores info in Datarow into an array
                                //Object[] cells1 = myDataRow1.ItemArray;
                                //--
                                strSQL = @"UPDATE TRN_SALARY_DETAILS SET SL_NO = " + lngSerialNo + " WHERE SALARY_DETAILS_ID = " + myDataRow1[0];
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    EnablDisable(true);
                                    //--
                                    this.Cursor = Cursors.Default;
                                    //--
                                }
                                lngSerialNo = lngSerialNo + 1;
                            }
                            //
                            dsetDeducteeDetails.Dispose();
                            //--
                        }
                        //
                    }
                    //
                    dsetChallanDetails.Dispose();
                    #endregion
                    //
                    #region ALLOT SERIAL NUMBER TO '0'
                    //
                    #region CHALLAN
                    strSQL = @"SELECT TOP 1 COUNT(*) 
                               FROM   TRN_CHALLAN,
                                      TRN_BASIC_INFO
                               WHERE  TRN_CHALLAN.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                               AND    TRN_BASIC_INFO.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"                               
                               AND    TRN_CHALLAN.SL_NO         = 0 ";
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        DataSet dsetChallan = new DataSet();
                        //
                        strSQL = @"SELECT TRN_CHALLAN.CHALLAN_ID,
                                          TRN_CHALLAN.BASIC_INFO_ID
                                   FROM   TRN_CHALLAN,
                                          TRN_BASIC_INFO
                                   WHERE  TRN_CHALLAN.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                                   AND    TRN_BASIC_INFO.ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"                               
                                   AND    TRN_CHALLAN.SL_NO         = 0 ";                        
                        //--
                        dsetChallan = dmlService.J_ExecSqlReturnDataSet(strSQL);
                        //
                        foreach (DataRow myDataRow in dsetChallan.Tables[0].Rows)
                        {
                            strSQL = "SELECT MAX(SL_NO) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + myDataRow[1];
                            long lngChallanMAXSerialNo = 0;
                            lngChallanMAXSerialNo = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                            //
                            strSQL = "UPDATE TRN_CHALLAN SET SL_NO = " + (lngChallanMAXSerialNo + 1) + " WHERE CHALLAN_ID = " + myDataRow[0];
                            dmlService.J_ExecSql(strSQL);
                            //
                        }
                        dsetChallan.Dispose();
                    }
                    #endregion
                    //
                    #region DEDUCTEE DETAILS

                    strSQL = @"SELECT TOP 1 COUNT(*) 
                               FROM   TRN_DEDUCTEE_DETAILS,
                                      TRN_BASIC_INFO
                               WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                               AND    TRN_BASIC_INFO.ASST_ID             = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"                               
                               AND    TRN_DEDUCTEE_DETAILS.SL_NO         = 0 ";
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        DataSet dsetChallan = new DataSet();
                        //
                        strSQL = @"SELECT TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID,
                                          TRN_DEDUCTEE_DETAILS.CHALLAN_ID
                                   FROM   TRN_DEDUCTEE_DETAILS,
                                          TRN_BASIC_INFO
                                   WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                                   AND    TRN_BASIC_INFO.ASST_ID             = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"                               
                                   AND    TRN_DEDUCTEE_DETAILS.SL_NO         = 0 ";
                        //--
                        dsetChallan = dmlService.J_ExecSqlReturnDataSet(strSQL);
                        //
                        foreach (DataRow myDataRow in dsetChallan.Tables[0].Rows)
                        {
                            strSQL = "SELECT MAX(SL_NO) FROM TRN_DEDUCTEE_DETAILS WHERE CHALLAN_ID = " + myDataRow[1];
                            long lngDeducteeMAXSerialNo = 0;
                            lngDeducteeMAXSerialNo = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                            //
                            strSQL = "UPDATE TRN_DEDUCTEE_DETAILS SET SL_NO = " + (lngDeducteeMAXSerialNo + 1) + " WHERE DEDUCTEE_DETAIL_ID = " + myDataRow[0];
                            dmlService.J_ExecSql(strSQL);
                            //
                        }
                        dsetChallan.Dispose();
                    }
                    #endregion
                    //
                    #region SALARY DETAILS

                    strSQL = @"SELECT TOP 1 COUNT(*) 
                               FROM   TRN_SALARY_DETAILS,
                                      TRN_BASIC_INFO
                               WHERE  TRN_SALARY_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                               AND    TRN_BASIC_INFO.ASST_ID           = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"                               
                               AND    TRN_SALARY_DETAILS.SL_NO         = 0 ";
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        DataSet dsetChallan = new DataSet();
                        //
                        strSQL = @"SELECT TRN_SALARY_DETAILS.SALARY_DETAILS_ID,
                                          TRN_SALARY_DETAILS.BASIC_INFO_ID
                                   FROM   TRN_SALARY_DETAILS,
                                          TRN_BASIC_INFO
                                   WHERE  TRN_SALARY_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID
                                   AND    TRN_BASIC_INFO.ASST_ID           = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"                               
                                   AND    TRN_SALARY_DETAILS.SL_NO         = 0 ";
                        //--
                        dsetChallan = dmlService.J_ExecSqlReturnDataSet(strSQL);
                        //
                        foreach (DataRow myDataRow in dsetChallan.Tables[0].Rows)
                        {
                            strSQL = "SELECT MAX(SL_NO) FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + myDataRow[1];
                            long lngChallanMAXSerialNo = 0;
                            lngChallanMAXSerialNo = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                            //
                            strSQL = "UPDATE TRN_SALARY_DETAILS SET SL_NO = " + (lngChallanMAXSerialNo + 1) + " WHERE SALARY_DETAILS_ID = " + myDataRow[0];
                            dmlService.J_ExecSql(strSQL);
                            //
                        }
                        dsetChallan.Dispose();
                    }
                    #endregion

                    #endregion
                    //--
                    #endregion
                    //-- CORRECTION RETURN
                    #region CORRECTION RETURN

                    #endregion
                    //--
                    #endregion
                }
                //--
                if (blnReIndex == true)
                {
                    #region REINDEXING
                    //
                    #region MST_SECTION
                    strSQL = @"SELECT COUNT(*)
                               FROM   MST_SECTION
                               GROUP BY SECTION_ID,FORM_NAME, SECTION_NAME
                               HAVING COUNT(SECTION_NAME) > 1";
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        DataSet dsetSection = new DataSet();
                        //--
                        strSQL = @"SELECT SECTION_ID,
                                          FORM_NAME,
                                          SECTION_NAME,
                                          SECTION_NO,
                                          SECTION_DESCRIPTION, 
                                          ASST_ID, 
                                          VALID_UPTO_ASST_ID, 
                                          CERTIFICATE_ALLOWED
                                   FROM   MST_SECTION
                                   GROUP BY SECTION_ID,
                                          FORM_NAME,
                                          SECTION_NAME,
                                          SECTION_NO,
                                          SECTION_DESCRIPTION, 
                                          ASST_ID, 
                                          VALID_UPTO_ASST_ID, 
                                          CERTIFICATE_ALLOWED
                                   HAVING COUNT(SECTION_NAME) > 1";
                        //--
                        dsetSection = dmlService.J_ExecSqlReturnDataSet(strSQL);
                        //
                        foreach (DataRow myDataRow in dsetSection.Tables[0].Rows)
                        {
                            //--
                            strSQL = @"DELETE FROM MST_SECTION WHERE SECTION_ID = " + myDataRow[0];
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                EnablDisable(true);
                                //--
                                this.Cursor = Cursors.Default;
                                //--
                            }
                            //--
                            strSQL = "INSERT INTO MST_SECTION (" +
                                     "       SECTION_ID," +
                                     "       FORM_NAME," +
                                     "       SECTION_TYPE," +
                                     "       SECTION_NAME," +
                                     "       SECTION_NO," +
                                     "       SECTION_DESCRIPTION," +
                                     "       VALID_UPTO_ASST_ID," +
                                     "       ASST_ID," +
                                     "       CERTIFICATE_ALLOWED) " +
                                     "VALUES(" + myDataRow[0] + ", " + //SECTION_ID
                                     "       '" + myDataRow[1] + "', " + //FORM NAME
                                     "       ''," + //SECTION TYPE -- NO LONGER USED
                                     "       '" + myDataRow[2] + "', " + //SECTION 3 DIGIT CODE
                                     "       '" + myDataRow[3] + "', " + //SECTION CODE
                                     "       '" + myDataRow[4] + "', " + //SECTION DESCRIPTION
                                     "        " + myDataRow[5] + ", " + //
                                     "        " + myDataRow[6] + ", " + //
                                     "        " + myDataRow[7] + ") "; //
                            //--
                            dmlService.J_ExecSql(strSQL);
                        }
                        dsetSection.Dispose();
                    }
                    #endregion
                    //
                    #region MST_REASON
                    strSQL = "UPDATE MST_REASON SET REASON = '' WHERE REASON IS NULL";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"SELECT COUNT(*)
                               FROM   MST_REASON
                               GROUP BY REASON_ID, FORM_NO, REASON
                               HAVING COUNT(REASON_ID) > 1";
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        DataSet dsetReason = new DataSet();
                        //--
                        strSQL = @"SELECT REASON_ID,
                                          FORM_NO,
                                          REASON,
                                          DESCRIPTION,
                                          CERTIFICATE_REQD,
                                          CERTIFICATE_NOT_ALLOWED
                                   FROM   MST_REASON
                                   GROUP BY REASON_ID,
                                          FORM_NO,
                                          REASON,
                                          DESCRIPTION,
                                          CERTIFICATE_REQD, 
                                          CERTIFICATE_NOT_ALLOWED
                                   HAVING COUNT(REASON_ID) > 1";                        
                        //--
                        dsetReason = dmlService.J_ExecSqlReturnDataSet(strSQL);
                        //
                        foreach (DataRow myDataRow in dsetReason.Tables[0].Rows)
                        {
                            //--
                            strSQL = @"DELETE FROM MST_REASON WHERE REASON_ID = " + myDataRow[0];
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                EnablDisable(true);
                                //--
                                this.Cursor = Cursors.Default;
                                //--
                            }
                            //--
                            strSQL = "INSERT INTO MST_REASON (" +
                                     "       REASON_ID," +
                                     "       FORM_NO," +
                                     "       REASON," +
                                     "       DESCRIPTION," +
                                     "       CERTIFICATE_REQD," +
                                     "       CERTIFICATE_NOT_ALLOWED) " +
                                     "VALUES(" + myDataRow[0] + ", " +
                                     "       '" + myDataRow[1] + "', " + 
                                     "       '" + myDataRow[2] + "', " + 
                                     "       '" + myDataRow[3] + "', " + 
                                     "       '" + myDataRow[4] + "', " + 
                                     "        " + myDataRow[5] + ") "; 
                            //--
                            dmlService.J_ExecSql(strSQL);
                        }
                        dsetReason.Dispose();
                    }
                    #endregion
                    //
                    #region MST_MINOR_HEAD
                    strSQL = @"SELECT COUNT(*)
                               FROM   MST_MINOR_HEAD
                               GROUP BY MINOR_HEAD_ID, MINOR_HEAD_CODE
                               HAVING COUNT(MINOR_HEAD_CODE) > 1";
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        DataSet dsetMinorHead = new DataSet();
                        //--
                        strSQL = @"SELECT MINOR_HEAD_ID,
                                          MINOR_HEAD_CODE,
                                          MINOR_HEAD_DESC
                                   FROM   MST_MINOR_HEAD
                                   GROUP BY MINOR_HEAD_ID,
                                          MINOR_HEAD_CODE,
                                          MINOR_HEAD_DESC
                                   HAVING COUNT(MINOR_HEAD_CODE) > 1";
                        //--
                        dsetMinorHead = dmlService.J_ExecSqlReturnDataSet(strSQL);
                        //
                        foreach (DataRow myDataRow in dsetMinorHead.Tables[0].Rows)
                        {
                            //--
                            strSQL = @"DELETE FROM MST_MINOR_HEAD WHERE MINOR_HEAD_ID = " + myDataRow[0];
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                EnablDisable(true);
                                //--
                                this.Cursor = Cursors.Default;
                                //--
                            }
                            //--
                            strSQL = "INSERT INTO MST_MINOR_HEAD (" +
                                     "       MINOR_HEAD_ID, " +
                                     "       MINOR_HEAD_CODE, " +
                                     "       MINOR_HEAD_DESC) " +
                                     "VALUES(" + myDataRow[0] + ", " +
                                     "       '" + myDataRow[1] + "', " +
                                     "       '" + myDataRow[2] + "') ";
                            //--
                            dmlService.J_ExecSql(strSQL);
                        }
                        dsetMinorHead.Dispose();
                    }
                    #endregion
                    //
                    #endregion
                }
                //--
                EnablDisable(true);
                //--
                this.Cursor = Cursors.Default;
                //--
                GC.Collect();
                this.Close();
                this.Dispose();
                //--
            }
            catch (Exception err)
            {
                EnablDisable(true);
                //--
                this.Cursor = Cursors.Default;
                //--
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            //
            GC.Collect();
            this.Close();
            this.Dispose();
            //
        }
        #endregion

        #endregion        

        #region  User Defined Functions

        #region EnablDisable
        private void EnablDisable(Boolean EnablDisable)
        {
            cmbFinancialYear.Enabled = EnablDisable;
            chkReserialising.Enabled = EnablDisable;
            chkRecalculation.Enabled = EnablDisable;
            ckhReIndexing.Enabled = EnablDisable;
            BtnSave.Enabled = EnablDisable;
            BtnExit.Enabled = EnablDisable;
        }
        #endregion

        #endregion
    }
}