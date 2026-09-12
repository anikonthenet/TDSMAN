#region Referred Namespaces
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
using System.Text;
//~~~~ User Namespaces ~~~~
using TDSMAN;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormTrn;
using TDSMAN.FormSys;
using TDSMAN.FormMst;
using TDSMAN.FormPar;
using TDSMAN.FormUtl;
//------------
using System.Reflection;

using Microsoft.VisualBasic.Compatibility.VB6;
#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnViewBHErr : Form
    {


        #region Objects & Variables declaration
        public TrnViewBHErr(long CompanyID, string Fields)
        {
            InitializeComponent();
            lngCompanyID = CompanyID;
            strField = Fields;
        }
        #endregion

        #region Objects & Variables declaration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();

        ToolTip tllTip = new ToolTip();

        mdiTDSMAN mdiTDSMAN = new mdiTDSMAN();

        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        string strSearchText = "-- Search here (min 3 chars) --";
        string strFilingStatusSearchText = "-- Search TAN - Name here (min 3 chars) --";
        //--
        string strSQL = "";
        //
        //long lngNoOfRecordsSQL = 5, lngNoOfReturnDays = 120;
        string strQuery = "", strQuarter = "", strCompanyName = ""; int intAsstId = 0;
        string strSortReturnsUnderProcess = "";
        bool blExit = true;
        //
        string strSQLReturnUnderProcess = "", strSQLReturnsReadyForFiling = "", strSQLFiledReturns = "", strSQLFilingStatus = "";

        
        bool blResize = true;
        long lngCompanyID = 0;
        string strField = "";
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            string strStateName = string.Empty;
            string strDistrictName = string.Empty;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            try
            {
                strSQL = "SELECT  MST_COMPANY.COMPANY_ID            AS COMPANY_ID," +
                    "             MST_COMPANY.COMPANY_NAME          AS COMPANY_NAME," +
                    "             MST_COMPANY.TAN_NO                AS TAN_NO," +
                    "             MST_COMPANY.PAN_NO                AS PAN_NO," +
                    "             MST_COMPANY.BRANCH_DIV            AS BRANCH_DIV," +
                    "             MST_COMPANY.D_CATEGORY_ID         AS D_CATEGORY_ID," +
                    "             MST_CATEGORY.CATEGORY_DESCRIPTION AS CATEGORY_DESCRIPTION," +
                    "             MST_CATEGORY.CATEGORY_CODE        AS CATEGORY_CODE," +
                    "             MST_COMPANY.FILE_PREFIX           AS FILE_PREFIX," +
                    "             MST_COMPANY.ADDRESS1              AS ADDRESS1," +
                    "             MST_COMPANY.ADDRESS2              AS ADDRESS2," +
                    "             MST_COMPANY.ADDRESS3              AS ADDRESS3," +
                    "             MST_COMPANY.ADDRESS4              AS ADDRESS4," +
                    "             MST_COMPANY.ADDRESS5              AS ADDRESS5," +
                    "             MST_COMPANY.STATE_ID              AS STATE_ID," +
                    "             MST_STATE.STATE_NAME              AS STATE_NAME," +
                    "             MST_COMPANY.PIN_CODE              AS PIN_CODE," +
                    "             MST_COMPANY.STD                   AS STD," +
                    "             MST_COMPANY.PHONE                 AS PHONE," +
                    "             MST_COMPANY.EMAIL                 AS EMAIL," +
                    "             MST_COMPANY.PERSON_NAME           AS PERSON_NAME," +
                    "             MST_COMPANY.DESIGNATION           AS DESIGNATION," +
                    "             MST_COMPANY.FATHER_NAME           AS FATHER_NAME," +
                    "             MST_COMPANY.P_ADDRESS1            AS P_ADDRESS1," +
                    "             MST_COMPANY.P_ADDRESS2            AS P_ADDRESS2," +
                    "             MST_COMPANY.P_ADDRESS3            AS P_ADDRESS3," +
                    "             MST_COMPANY.P_ADDRESS4            AS P_ADDRESS4," +
                    "             MST_COMPANY.P_ADDRESS5            AS P_ADDRESS5," +
                    "             MST_COMPANY.P_STATE_ID            AS P_STATE_ID," +
                    "             RP_STATE.STATE_NAME               AS RP_STATE_NAME," +
                    "             MST_COMPANY.P_PIN_CODE            AS P_PIN_CODE," +
                    "             MST_COMPANY.P_PHONE               AS P_PHONE," +
                    "             MST_COMPANY.P_STD                 AS P_STD," +
                    "             MST_COMPANY.P_EMAIL               AS P_EMAIL," +
                    "             MST_COMPANY.P_MOBILE              AS P_MOBILE," +
                    "             MST_COMPANY.PAO_CODE              AS PAO_CODE," +
                    "             MST_COMPANY.PAO_REG_NO            AS PAO_REG_NO," +
                    "             MST_COMPANY.DDO_CODE              AS DDO_CODE," +
                    "             MST_COMPANY.DDO_REG_NO            AS DDO_REG_NO," +
                    "             MST_COMPANY.D_STATE_ID            AS D_STATE_ID," +
                    "             D_STATE.STATE_NAME                AS D_STATE_NAME," +
                    "             MST_COMPANY.MINISTRY_ID           AS MINISTRY_ID," +
                    "             MST_MINISTRY.MINISTRY_NAME        AS MINISTRY_NAME," +
                    "             MST_COMPANY.MINISTRY_OTHER        AS MINISTRY_OTHER," +
                    "             MST_COMPANY.CIT_TDS_ADDRESS       AS CIT_TDS_ADDRESS," +
                    "             MST_COMPANY.CIT_TDS_CITY          AS CIT_TDS_CITY," +
                    "             MST_COMPANY.CIT_TDS_PINCODE       AS CIT_TDS_PINCODE," +
                    "             MST_COMPANY.ALT_STD               AS ALT_STD," +
                    "             MST_COMPANY.ALT_PHONE             AS ALT_PHONE," +
                    "             MST_COMPANY.ALT_EMAIL             AS ALT_EMAIL," +
                    "             MST_COMPANY.P_ALT_STD             AS P_ALT_STD," +
                    "             MST_COMPANY.P_ALT_PHONE           AS P_ALT_PHONE," +
                    "             MST_COMPANY.P_ALT_EMAIL           AS P_ALT_EMAIL," +
                    "             MST_COMPANY.AIN_NO                AS AIN_NO," +
                    "             MST_COMPANY.TAN_REG_NO            AS TAN_REG_NO," +
                    "             MST_COMPANY.INACTIVE_FLAG         AS INACTIVE_FLAG," +
                    "             MST_COMPANY.P_PAN                 AS P_PAN," +
                    "             MST_COMPANY.GSTN                  AS GSTN," +
                    "             MST_COMPANY.SECTION_194P_FLAG     AS SECTION_194P_FLAG," +
                    "             MST_COMPANY.CSI_FILE_DOWNLOAD_OPTION     AS CSI_FILE_DOWNLOAD_OPTION " +
                    "     FROM    (((((MST_COMPANY INNER JOIN MST_CATEGORY " +
                    "             ON MST_COMPANY.D_CATEGORY_ID     = MST_CATEGORY.CATEGORY_ID) " +
                    "     INNER JOIN MST_STATE " +
                    "             ON MST_COMPANY.STATE_ID    = MST_STATE.STATE_ID) " +
                    "     INNER JOIN MST_STATE AS RP_STATE " +
                    "             ON MST_COMPANY.P_STATE_ID = RP_STATE.STATE_ID) " +
                    "     LEFT JOIN  MST_STATE AS D_STATE " +
                    "             ON MST_COMPANY.D_STATE_ID        = D_STATE.STATE_ID) " +
                    "     LEFT JOIN  MST_MINISTRY " +
                    "             ON MST_COMPANY.MINISTRY_ID       = MST_MINISTRY.MINISTRY_ID) " +
                    "     WHERE   MST_COMPANY.COMPANY_ID      = " + Id + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    //lngSearchId = Id;

                    txtDedEmpColName.Text = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                    txtTANNo.Text = Convert.ToString(drdShowRecord["TAN_NO"]);
                    txtPANNo.Text = Convert.ToString(drdShowRecord["PAN_NO"]);
                    cmbDeductorType.Text = Convert.ToString(drdShowRecord["CATEGORY_CODE"]) + " - " + Convert.ToString(drdShowRecord["CATEGORY_DESCRIPTION"]);
                    txtBranch.Text = Convert.ToString(drdShowRecord["BRANCH_DIV"]);
                    txtAddress1.Text = Convert.ToString(drdShowRecord["ADDRESS1"]);
                    txtAddress2.Text = Convert.ToString(drdShowRecord["ADDRESS2"]);
                    txtAddress3.Text = Convert.ToString(drdShowRecord["ADDRESS3"]);
                    txtAddress4.Text = Convert.ToString(drdShowRecord["ADDRESS4"]);
                    txtAddress5.Text = Convert.ToString(drdShowRecord["ADDRESS5"]);
                    cmbState.Text = Convert.ToString(drdShowRecord["STATE_NAME"]);
                    txtPIN.Text = Convert.ToString(drdShowRecord["PIN_CODE"]);
                    txtSTD.Text = Convert.ToString(drdShowRecord["STD"]);
                    txtPhone.Text = Convert.ToString(drdShowRecord["PHONE"]);
                    txtEmail.Text = Convert.ToString(drdShowRecord["EMAIL"]);
                    txtRPName.Text = Convert.ToString(drdShowRecord["PERSON_NAME"]);
                    txtRPDesignation.Text = Convert.ToString(drdShowRecord["DESIGNATION"]);
                    txtRPFatherName.Text = Convert.ToString(drdShowRecord["FATHER_NAME"]);
                    txtRPAddress1.Text = Convert.ToString(drdShowRecord["P_ADDRESS1"]);
                    txtRPAddress2.Text = Convert.ToString(drdShowRecord["P_ADDRESS2"]);
                    txtRPAddress3.Text = Convert.ToString(drdShowRecord["P_ADDRESS3"]);
                    txtRPAddress4.Text = Convert.ToString(drdShowRecord["P_ADDRESS4"]);
                    txtRPAddress5.Text = Convert.ToString(drdShowRecord["P_ADDRESS5"]);
                    cmbRPState.Text = Convert.ToString(drdShowRecord["RP_STATE_NAME"]);
                    txtRPPIN.Text = Convert.ToString(drdShowRecord["P_PIN_CODE"]);
                    txtRPSTD.Text = Convert.ToString(drdShowRecord["P_STD"]);
                    txtRPPhone.Text = Convert.ToString(drdShowRecord["P_PHONE"]);
                    txtRPMobileNo.Text = Convert.ToString(drdShowRecord["P_MOBILE"]);
                    txtRPEmail.Text = Convert.ToString(drdShowRecord["P_EMAIL"]);
                    txtPAOCode.Text = Convert.ToString(drdShowRecord["PAO_CODE"]);
                    txtPAORegNo.Text = Convert.ToString(drdShowRecord["PAO_REG_NO"]);
                    txtDDOCode.Text = Convert.ToString(drdShowRecord["DDO_CODE"]);
                    txtDDORegNo.Text = Convert.ToString(drdShowRecord["DDO_REG_NO"]);
                    cmbGovtDedState.Text = Convert.ToString(drdShowRecord["D_STATE_NAME"]);
                    cmbMinistry.Text = Convert.ToString(drdShowRecord["MINISTRY_NAME"]);
                    txtOtherMinistry.Text = Convert.ToString(drdShowRecord["MINISTRY_OTHER"]);
                    //txtCITAddress.Text = Convert.ToString(drdShowRecord["CIT_TDS_ADDRESS"]);
                    //txtCITCity.Text = Convert.ToString(drdShowRecord["CIT_TDS_CITY"]);
                    //txtCITPin.Text = Convert.ToString(drdShowRecord["CIT_TDS_PINCODE"]);
                    txtAltSTD.Text = Convert.ToString(drdShowRecord["ALT_STD"]);
                    txtAltPhone.Text = Convert.ToString(drdShowRecord["ALT_PHONE"]);
                    txtAltEmail.Text = Convert.ToString(drdShowRecord["ALT_EMAIL"]);
                    txtRPAltSTD.Text = Convert.ToString(drdShowRecord["P_ALT_STD"]);
                    txtRPAltPhone.Text = Convert.ToString(drdShowRecord["P_ALT_PHONE"]);
                    txtRPAltEmail.Text = Convert.ToString(drdShowRecord["P_ALT_EMAIL"]);
                    txtAIN.Text = Convert.ToString(drdShowRecord["AIN_NO"]);
                    txtTANRegNo.Text = Convert.ToString(drdShowRecord["TAN_REG_NO"]);
                    //
                    //if (Convert.ToString(drdShowRecord["INACTIVE_FLAG"]) == "1")
                    //    chkInactiveCompany.Checked = true;
                    ////
                    //if (Convert.ToString(drdShowRecord["SECTION_194P_FLAG"]) == "1")
                    //    chk194P.Checked = true;
                    //else
                    //    chk194P.Checked = false;
                    //
                    txtRPPAN.Text = Convert.ToString(drdShowRecord["P_PAN"]);
                    //
                    txtGSTN.Text = Convert.ToString(drdShowRecord["GSTN"]);
                    //-- 2023/05/15
                    //if (Convert.ToString(drdShowRecord["CSI_FILE_DOWNLOAD_OPTION"]) == "1")
                    //{
                    //    chkCSIDownloadPassword.Checked = true;
                    //}
                    //else
                    //{
                    //    chkCSIDownloadPassword.Checked = false;
                    //}
                    //
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();

                    //cmbState.Text = strStateName;
                    //cmbDistrict.Text = strDistrictName;

                    //txtDedEmpColName.Select();
                    return true;
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                //lngSearchId = 0;
                ////-----------------------------------------------------------
                //if (strCheckFields == "")
                //    strSQL = strQuery + "order by " + strOrderBy;
                //else
                //    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                ////-----------------------------------------------------------
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region TrnViewDDErr_Load
        private void TrnViewDDErr_Load(object sender, EventArgs e)
        {
            ShowRecord(lngCompanyID);
            //
            if (strField.ToUpper().Contains("NAME OF EMPLOYER / DEDUCTOR / COLLECTOR") == true)
            {
                lblDedEmpColName.ForeColor = Color.Red;
                txtDedEmpColName.ForeColor = Color.Red;
            }
            //
        }
        #endregion

    }
}
