
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
    public partial class TrnViewCDErr : Form
    {

        #region TrnViewCDErr
        public TrnViewCDErr(long BasicInfoID, long ChallanID, string Field)
        {
            InitializeComponent();
            lngBasicInfoID = BasicInfoID;
            lngChallanID = ChallanID;
            strField = Field;
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
        string strSQLReturnUnderProcess = "", strSQLReturnsReadyForFiling = "", strSQLFiledReturns = "", strSQLFilingStatus = "", strField = "";

        bool blResize = true;
        long lngBasicInfoID = 0, lngChallanID = 0;
        #endregion




        #region TrnViewCDErr_Activated
        private void TrnViewCDErr_Activated(object sender, EventArgs e)
        {
            
        }
        #endregion


        #region TrnViewCDErr_Activated
        private void TrnViewCDErr_Load(object sender, EventArgs e)
        {
            ShowChallanRecordErr(lngBasicInfoID, lngChallanID);
            //
            if(strField.ToUpper().Contains("BANK-BRANCH CODE") == true)
            {
                lblBSRCode.ForeColor = Color.Red;
                txtBSRCode.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("DATE") == true)
            {
                lblDOP.ForeColor = Color.Red;
                mskDateOfPayment.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("REMARKS") == true)
            {
                lblRemarks.ForeColor = Color.Red;
                txtRemarks.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("CHALLAN") == true)
            {
                lblTrVchNo.ForeColor = Color.Red;
                txtTransferVoucherNo.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("TDS") == true)
            {
                lblTDS.ForeColor = Color.Red;
                txtTDS.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("SURCHARGE") == true)
            {
                lblSurcharge.ForeColor = Color.Red;
                txtSurcharge.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("CESS") == true)
            {
                lblEduCess.ForeColor = Color.Red;
                txtEducationCess.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("INTEREST") == true)
            {
                lblInterest.ForeColor = Color.Red;
                txtRemarks.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("FEE") == true)
            {
                lblRemarks.ForeColor = Color.Red;
                txtInterests.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("OTHERS") == true)
            {
                lblOthers.ForeColor = Color.Red;
                txtOthers.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("MINOR") == true)
            {
                lblMinor.ForeColor = Color.Red;
                txtMinorHead.ForeColor = Color.Red;
            }

            //
        }
        #endregion

        #region ShowChallanRecordErr
        private void ShowChallanRecordErr(long BasicInfoID, long ChallanID)
        {
            IDataReader drdShowChallanRecordErr = null;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            try
            {
                strSQL = "SELECT TRN_CHALLAN.CHALLAN_ID          AS CHALLAN_ID," +
                    "            TRN_CHALLAN.SL_NO               AS SL_NO," +
                    "            TRN_CHALLAN.SECTION_ID          AS SECTION_ID," +
                    "            MST_SECTION.SECTION_NO          AS SECTION_NO," +
                    "            MST_SECTION.SECTION_DESCRIPTION AS SECTION_DESCRIPTION," +
                    "            " + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEPOSIT_DATE," +
                    "            TRN_CHALLAN.BSR_CODE            AS BSR_CODE," +
                    "            TRN_CHALLAN.CHALLAN_NO          AS CHALLAN_NO," +
                    "            TRN_CHALLAN.TRANSFER_VOUCHER_NO AS TRANSFER_VOUCHER_NO," +
                    "            TRN_CHALLAN.CHEQUE_NO           AS CHEQUE_NO," +
                    "            TRN_CHALLAN.TDS                 AS TDS," +
                    "            TRN_CHALLAN.SURCHARGE           AS SURCHARGE," +
                    "            TRN_CHALLAN.EDUCATION_CESS      AS EDUCATION_CESS," +
                    "            TRN_CHALLAN.INTEREST            AS INTEREST," +
                    "            TRN_CHALLAN.OTHERS              AS OTHERS," +
                    "            TRN_CHALLAN.TOT_TAX             AS TOT_TAX," +
                    "            TRN_CHALLAN.INTEREST_ALLOCATED  AS INTEREST_ALLOCATED," +
                    "            TRN_CHALLAN.OTHERS_ALLOCATED    AS OTHERS_ALLOCATED," +
                    "            TRN_CHALLAN.REMARKS             AS REMARKS," +
                    "            TRN_CHALLAN.BOOK_ENTRY          AS BOOK_ENTRY," +
                    "            TRN_CHALLAN.LATE_FEE            AS LATE_FEE," +
                    "            MST_MINOR_HEAD.MINOR_HEAD_CODE + '-' + MST_MINOR_HEAD.MINOR_HEAD_DESC AS MINOR_HEAD " +
                    "     FROM  ((TRN_CHALLAN LEFT JOIN MST_SECTION " +
                    "            ON  TRN_CHALLAN.SECTION_ID    = MST_SECTION.SECTION_ID) " +
                    "            LEFT JOIN MST_MINOR_HEAD " +
                    "            ON  TRN_CHALLAN.MINOR_HEAD_ID = MST_MINOR_HEAD.MINOR_HEAD_ID)" +
                    "     WHERE TRN_CHALLAN.CHALLAN_ID = " + ChallanID + " ";


                drdShowChallanRecordErr = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowChallanRecordErr == null)
                {
                    return;
                }
                while (drdShowChallanRecordErr.Read())
                {
                    //lngChallanID = Id;

                    txtChallanSrlNo.Text = Convert.ToString(drdShowChallanRecordErr["SL_NO"]);

                    //blnSectionDisplay = false;
                    txtSection.Text = Convert.ToString(drdShowChallanRecordErr["SECTION_NO"]);
                    lblSectionDisplay.Visible = true;
                    lblSectionCaption.Visible = true;
                    lblSectionDisplay.Text = Convert.ToString(drdShowChallanRecordErr["SECTION_DESCRIPTION"]);
                    if(Convert.ToString(drdShowChallanRecordErr["SECTION_NO"]).Trim() == "")
                    {
                        txtSection.Visible = false;
                        lblSectionCaption.Visible = false;
                        lblSectionDisplay.Visible = false;
                    }
                    //blnSectionDisplay = true;

                    mskDateOfPayment.Text = Convert.ToString(drdShowChallanRecordErr["DEPOSIT_DATE"]);
                    //--
                    //blnShowBSRCodeHelp = false;
                    txtBSRCode.Text = Convert.ToString(drdShowChallanRecordErr["BSR_CODE"]);
                    //blnShowBSRCodeHelp = true;
                    //--
                    txtChallanNo.Text = Convert.ToString(drdShowChallanRecordErr["CHALLAN_NO"]);
                    txtTransferVoucherNo.Text = Convert.ToString(drdShowChallanRecordErr["TRANSFER_VOUCHER_NO"]);
                    txtChequeNo.Text = Convert.ToString(drdShowChallanRecordErr["CHEQUE_NO"]);
                    txtTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecordErr["TDS"]) == "" ? "0" : Convert.ToString(drdShowChallanRecordErr["TDS"])));
                    txtSurcharge.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecordErr["SURCHARGE"]) == "" ? "0" : Convert.ToString(drdShowChallanRecordErr["SURCHARGE"])));
                    txtEducationCess.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecordErr["EDUCATION_CESS"]) == "" ? "0" : Convert.ToString(drdShowChallanRecordErr["EDUCATION_CESS"])));
                    txtInterests.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecordErr["INTEREST"]) == "" ? "0" : Convert.ToString(drdShowChallanRecordErr["INTEREST"])));
                    txtOthers.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecordErr["OTHERS"]) == "" ? "0" : Convert.ToString(drdShowChallanRecordErr["OTHERS"])));
                    txtTotalTax.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecordErr["TOT_TAX"]) == "" ? "0" : Convert.ToString(drdShowChallanRecordErr["TOT_TAX"])));
                    txtInterestAllocated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecordErr["INTEREST_ALLOCATED"]) == "" ? "0" : Convert.ToString(drdShowChallanRecordErr["INTEREST_ALLOCATED"])));
                    txtOthersAllocated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecordErr["OTHERS_ALLOCATED"]) == "" ? "0" : Convert.ToString(drdShowChallanRecordErr["OTHERS_ALLOCATED"])));
                    txtRemarks.Text = Convert.ToString(drdShowChallanRecordErr["REMARKS"]);
                    //
                    if (Convert.ToString(drdShowChallanRecordErr["BOOK_ENTRY"]) == "1")
                        chkBookEntry.Checked = true;
                    else if (Convert.ToString(drdShowChallanRecordErr["BOOK_ENTRY"]) == "0")
                        chkBookEntry.Checked = false;
                    //
                    txtFee.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecordErr["LATE_FEE"]) == "" ? "0" : Convert.ToString(drdShowChallanRecordErr["LATE_FEE"])));
                    txtMinorHead.Text = Convert.ToString(drdShowChallanRecordErr["MINOR_HEAD"]);
                    //
                    drdShowChallanRecordErr.Close();
                    drdShowChallanRecordErr.Dispose();

                    //if (cmbSection.Text != "")
                    //    cmbSection.Select();
                    //else
                    //    mskDateOfPayment.Select();
                    //ControlSummaryChallan(lngChallanID);                
                    //return true;
                    return;
                }
                //-----------------------------------------------------------
                drdShowChallanRecordErr.Close();
                drdShowChallanRecordErr.Dispose();

                //ControlSummaryChallan(lngChallanID);
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                //lngChallanID = 0;
                ////-----------------------------------------------------------
                //if (strCheckFields == "")
                //    strSQL = strQuery + "order by " + strOrderBy;
                //else
                //    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                ////-----------------------------------------------------------
                //if (dsetChallanGridClone != null) dsetChallanGridClone.Clear();
                //dsetChallanGridClone = dmlService.J_ShowDataInGrid(ref dgcViewChallan, strSQL, strMatrix);       //Show Data into the Grid
                //return false;
            }
            catch (Exception err_handler)
            {
                drdShowChallanRecordErr.Close();
                drdShowChallanRecordErr.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return;
            }
        }
        #endregion
    }
}
