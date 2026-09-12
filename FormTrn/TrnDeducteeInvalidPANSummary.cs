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

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnDeducteeInvalidPANSummary : Form
    {
        #region Constructor

        public TrnDeducteeInvalidPANSummary(string FormNo, int CompanyId, int AsstId, string Quarter)
        {
            strFormNo = FormNo;
            intCompanyId = CompanyId;
            intAsstId = AsstId;
            strQuarter = Quarter;
            InitializeComponent();
            PopulateGrid();
        }

        public TrnDeducteeInvalidPANSummary()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Variables and Class Objects

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
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        string strTempMode;

        int intCountRecords = 0;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        //-----------------------------------------------------------------------
        string strFormNo = "";
        int intCompanyId = 0;
        string strCompanyName = "";
        string strCompanyTAN = "";
        int intAsstId = 0;
        string strFAYear = "";
        string strQuarter = "";

        #endregion 

        #region User Defined Events

        #region TrnDeducteeInvalidPANSummary_Load
        private void TrnDeducteeInvalidPANSummary_Load(object sender, EventArgs e)
        {
            IDataReader drdGetCompanyDetail = null;
            GC.Collect();

            //Get the Assessment Id from Financial Year
            strSQL = "SELECT FA_YEAR FROM MST_ASSESSMENT" +
                   "     WHERE  ASST_ID = " + intAsstId + " ";
            //
            strFAYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

            lblFAYear.Text = strFAYear;
            lblFormNo.Text = strFormNo;
            lblQuarter.Text = strQuarter;

            //Get the Company Details from Company_id 
            strSQL = @"SELECT COMPANY_NAME,
                              TAN_NO
                       FROM   MST_COMPANY
                       WHERE  COMPANY_ID = " + intCompanyId + "";
            //
            drdGetCompanyDetail = dmlService.J_ExecSqlReturnReader(strSQL);
            //
            if (drdGetCompanyDetail == null)
                return;
            //
            while (drdGetCompanyDetail.Read())
            {
                strCompanyName = Convert.ToString(drdGetCompanyDetail["COMPANY_NAME"]);
                lblCompanyName.Text = strCompanyName;
                strCompanyTAN = Convert.ToString(drdGetCompanyDetail["TAN_NO"]);
                lblTAN.Text = strCompanyTAN;
            }
            drdGetCompanyDetail.Close();
            drdGetCompanyDetail.Dispose();

            //Populating Grid
            PopulateGrid();
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            GC.Collect();
            //
            this.Dispose();
            this.Close();
        }
        #endregion

       
        #region txtDeducteeName_TextChanged
        private void txtDeducteeName_TextChanged(object sender, EventArgs e)
        {
           
            PopulateGrid();
        }
        #endregion

        #region txtDeducteePAN_TextChanged
        private void txtDeducteePAN_TextChanged(object sender, EventArgs e)
        {
            PopulateGrid();
        }
        #endregion 

        #region btnPrintData_Click
        private void btnPrintData_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Cursor = Cursors.Default;
                //RptDialog rptDialog = new RptDialog();
                //rptDialog.PrintInvalidPANChecking(strFormNo,
                //                                     intCompanyId,
                //                                     intAsstId,
                //                                     strFAYear,
                //                                     strQuarter,
                //                                     Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                //                                     Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)));
                
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region PopulateGrid
        public void PopulateGrid()
        {

            string[,] strMatrixViewTaxSlab = {{"Challan Srl.No.","80", "", "", "", "", ""},
                                              {"Deductee Srl.No","80", "", "", "", "", ""},
                                              {"PAN","100", "", "", "", "", ""},
                                              {"Deductee Name","300", "", "", "", "", ""}};

            // Get the details of Grid
            strSQL = @" SELECT    TRN_CHALLAN.SL_NO,
                                  TRN_DEDUCTEE_DETAILS.SL_NO,                                  
                                  MST_DEDUCTEE.DEDUCTEE_PAN,                                  
                                  MST_DEDUCTEE.DEDUCTEE_NAME
                        FROM      TRN_BASIC_INFO,
                                  TRN_CHALLAN,
                                  TRN_DEDUCTEE_DETAILS, 
                                  MST_DEDUCTEE
                        WHERE     TRN_BASIC_INFO.BASIC_INFO_ID =TRN_CHALLAN.BASIC_INFO_ID
                        AND       TRN_CHALLAN.CHALLAN_ID =TRN_DEDUCTEE_DETAILS.CHALLAN_ID 
                        AND       TRN_DEDUCTEE_DETAILS.PARTY_ID=MST_DEDUCTEE.DEDUCTEE_ID  
                        AND       IIF(MST_DEDUCTEE.DEDUCTEE_PAN <> 'PANNOTAVBL' 
                                          AND             MST_DEDUCTEE.DEDUCTEE_PAN <> 'PANAPPLIED' 
                                         AND             MST_DEDUCTEE.DEDUCTEE_PAN <> 'PANINVALID' 
                                         AND             MID(MST_DEDUCTEE.DEDUCTEE_PAN, 4, 1) <> 'C' 
                                         AND             MID(MST_DEDUCTEE.DEDUCTEE_PAN, 4, 1) <> 'P'  
                                         AND             MID(MST_DEDUCTEE.DEDUCTEE_PAN, 4, 1) <> 'H'  
                                         AND             MID(MST_DEDUCTEE.DEDUCTEE_PAN, 4, 1) <> 'F'  
                                         AND             MID(MST_DEDUCTEE.DEDUCTEE_PAN, 4, 1) <> 'A'  
                                         AND             MID(MST_DEDUCTEE.DEDUCTEE_PAN, 4, 1) <> 'T'  
                                         AND             MID(MST_DEDUCTEE.DEDUCTEE_PAN, 4, 1) <> 'B'  
                                         AND             MID(MST_DEDUCTEE.DEDUCTEE_PAN, 4, 1) <> 'L'  
                                         AND             MID(MST_DEDUCTEE.DEDUCTEE_PAN, 4, 1) <> 'J'  
                                         AND             MID(MST_DEDUCTEE.DEDUCTEE_PAN, 4, 1) <> 'G'  
                                         ,'INVALID'
                                         ,'VALID') ='INVALID'";
            if (intCompanyId > 0)
                strSQL = strSQL + " AND TRN_BASIC_INFO.COMPANY_ID = " + intCompanyId + "  ";
            if (intAsstId > 0)
                strSQL = strSQL + " AND TRN_BASIC_INFO.ASST_ID = " + intAsstId + "  ";
            if (strQuarter != "")
                strSQL = strSQL + " AND TRN_BASIC_INFO.QTR = '" + strQuarter + "' ";
            if (strFormNo != "")
                strSQL = strSQL + " AND TRN_BASIC_INFO.FORM_NO = '" + strFormNo + "' ";

            if (txtDeducteeName.Text != "")
                strSQL = strSQL + " AND MST_DEDUCTEE.DEDUCTEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "%'";
            if (txtDeducteePAN.Text != "")
                strSQL = strSQL + " AND   MST_DEDUCTEE.DEDUCTEE_PAN  LIKE '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "%'";

            strOrderBy = @" ORDER BY MST_DEDUCTEE.DEDUCTEE_NAME ";

            strSQL = strSQL + strOrderBy;

            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgcViewRecords, strSQL, strMatrixViewTaxSlab);
            
        }
        #endregion


    #endregion
    }

}