#region Refered Namespaces & Classes

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Diagnostics;

using System.Runtime.InteropServices;




//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;
using TDSMAN.FormTrn; //-- Added By Abhishek Dey On 22/05/2018--

#endregion

namespace TDSMAN.FormSys
{
    public partial class SysShowUsage : Form
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public SysShowUsage()
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

        TracesConnect objTracesConnect = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;
        string strInvalidPAN = "";
        string strUnmatchedPAN = "";
        long lngBasicInfoID = 0;
        //--            
        ToolTip tllTip = new ToolTip();
        string strEmpDed = "";
        string strbtnVerification = "&Start Validation";
        string strTitle = "Total Deductee / Collectee Records for FA year : ";
        //string strLogOff = "Log off";
        //string strLogOn = "Log on";
        //
        bool blnStatus = false;
        bool blnVerificationComplete = false;
        long lngDeducteeID = 0;
        bool blRegular = true;
        bool blnSelectComboExit = false;
        //
        int intPANId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0;
        int intVerifyId = 0;
        //
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNoBkmark = "";
        string strFAYear = "";
        //
        int j = 0;
        //
        string strNoRecordFound = "No record found";
        //--
        #endregion

        #region User Defined Events

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

        #region SysShowUsage_Activated
        private void SysShowUsage_Activated(object sender, EventArgs e)
        {
            //-
            lblTotalRecords.Text = strTitle;
            //-
            ClearControls();
            //--
            LoadControls();
            //--
        }
        #endregion
        
        #region cmbMain_SelectedIndexChanged
        private void cmbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (blnSelectComboExit == true)
            //    return;
            //
            ClearControls();
            //
            string strFormNo = "";
            //
            lngBasicInfoID = 0;
            //
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                //grpControlSummary.Visible = false;
                //pnlLine1.Visible = false;
                //pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                lblTotalRecords.Text = strTitle;
                ClearControls();
                grpControlSummary.Enabled = false;
                //lblTotalRecordsReturn.Visible = false;
                //lblTotalRecords.Visible = false;
                //btnNext.Enabled = false;
                //btnNext.BackColor = Color.LightGray;
                return;
            }
            else if (cmbFinancialYear.SelectedIndex > 0)
            {
                //
                long lngTotalRecords = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS, TRN_BASIC_INFO WHERE TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID AND TRN_BASIC_INFO.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), J_QueryType.DirectQuery);
                lblTotalRecords.Text = strTitle + cmbFinancialYear.Text;
                //
                grpControlSummary.Enabled = true;
                //if (lngTotalRecords > 0)
                //{
                //lblTotalRecords.Visible = true;
                //lblTotalRecords.Text = "Total Records for FA year [" + cmbFinancialYear.Text + "] : " + Convert.ToString(lngTotalRecords);
                //}
                //else
                //    lblTotalRecords.Visible = false;
            }
            //--
            //if (cmbCompany.SelectedIndex <= 0)
            //{
            //    //grpControlSummary.Visible = false;
            //    //pnlLine1.Visible = false;
            //    //pnlLine2.Visible = false;
            //    //grpReturnFilingStatus.Visible = false;
            //    ClearControls();
            //    lblTotalRecordsReturn.Visible = false;
            //    //btnNext.Enabled = false;
            //    //btnNext.BackColor = Color.LightGray;
            //    return;
            //}
            if (cmbFormNo.Text == "")
            {
                //grpControlSummary.Visible = false;
                //pnlLine1.Visible = false;
                //pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                ClearControls();
                //lblTotalRecordsReturn.Visible = false;
                //btnNext.Enabled = false;
                //btnNext.BackColor = Color.LightGray;
                lblForm24Q.Visible = true;
                lblForm24QCount.Visible = true;
                lblForm26Q.Visible = true;
                lblForm26QCount.Visible = true;
                lblForm27Q.Visible = true;
                lblForm27QCount.Visible = true;
                lblForm27EQ.Visible = true;
                lblForm27EQCount.Visible = true;
                //return;
            }
            else
            {
                strFormNo = cmbFormNo.Text.Substring(cmbFormNo.Text.IndexOf('(') + 1, cmbFormNo.Text.IndexOf(')') - cmbFormNo.Text.IndexOf('(') - 1);
                //
                //if (cmbFormNo.Text == T_FormNo.F24Q)
                if (strFormNo == T_FormNo.F24Q)
                {
                    lblForm24Q.Visible = true;
                    lblForm24QCount.Visible = true;
                    lblForm26Q.Visible = false;
                    lblForm26QCount.Visible = false;
                    lblForm27Q.Visible = false;
                    lblForm27QCount.Visible = false;
                    lblForm27EQ.Visible = false;
                    lblForm27EQCount.Visible = false;
                }
                //else if (cmbFormNo.Text == T_FormNo.F26Q)
                else if (strFormNo == T_FormNo.F26Q)
                {
                    lblForm24Q.Visible = false;
                    lblForm24QCount.Visible = false;
                    lblForm26Q.Visible = true;
                    lblForm26QCount.Visible = true;
                    lblForm27Q.Visible = false;
                    lblForm27QCount.Visible = false;
                    lblForm27EQ.Visible = false;
                    lblForm27EQCount.Visible = false;
                }
                //else if (cmbFormNo.Text == T_FormNo.F27Q)
                else if (strFormNo == T_FormNo.F27Q)
                {
                    lblForm24Q.Visible = false;
                    lblForm24QCount.Visible = false;
                    lblForm26Q.Visible = false;
                    lblForm26QCount.Visible = false;
                    lblForm27Q.Visible = true;
                    lblForm27QCount.Visible = true;
                    lblForm27EQ.Visible = false;
                    lblForm27EQCount.Visible = false;
                }
                //else if (cmbFormNo.Text == T_FormNo.F27EQ)
                else if (strFormNo == T_FormNo.F27EQ)
                {
                    lblForm24Q.Visible = false;
                    lblForm24QCount.Visible = false;
                    lblForm26Q.Visible = false;
                    lblForm26QCount.Visible = false;
                    lblForm27Q.Visible = false;
                    lblForm27QCount.Visible = false;
                    lblForm27EQ.Visible = true;
                    lblForm27EQCount.Visible = true;
                }
            }
            //--
            //if (cmbQuarter.Text == "")
            //{
            //    //grpControlSummary.Visible = false;
            //    //pnlLine1.Visible = false;
            //    //pnlLine2.Visible = false;
            //    //grpReturnFilingStatus.Visible = false;
            //    ClearControls();
            //    lblTotalRecordsReturn.Visible = false;
            //    //btnNext.Enabled = false;
            //    //btnNext.BackColor = Color.LightGray;
            //    return;
            //}
            //--
            //lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
            //                                            cmbQuarter.Text,
            //                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
            //                                            cmbFormNo.Text);
            ////--
            ControlSummaryBasicInfo(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), strFormNo, cmbQuarter.Text, Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
            //-- 
        }
        #endregion

        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            //
            GC.Collect();
            //
            blnVerificationComplete = false;
            //--
            this.Dispose();
            this.Close();
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region ClearControls
        private void ClearControls()
        {
            //grpButtons.Enabled = true;
            //
            //txtTotalChallanRecords.Text = "0";
            //txtTotalDeducteeRecords.Text = "0";
            //txtTotalChallanAmount.Text = "0.00";
            //txtTotalDeducteeTDS.Text = "0.00";
            //txtAmountPaid.Text = "0.00";
            ////
            //txtReceiptNo.Text = "";
            //txtDateofFiling.Text = "";
            //txtTokenNo.Text = "";
        }
        #endregion

        #region LoadControls
        private void LoadControls()
        {
            //
            blnSelectComboExit = true;
            ////-----------
            ////-- FINANCIAL YEAR
            ////-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, J_ComboBoxSelectedIndex.YES) == false) return;
            ////-----------
            //-- QUARTER
            //-----------
            string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter);
            //-----------
            //-- FORM NO
            //-----------
            //string[] strFormNo1 ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            string[] strFormNo1 = { T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")", T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")", T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")", T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")" };
            dmlService.J_PopulateComboBox(strFormNo1, ref cmbFormNo);
            //-----------
            //-- COMPANY
            //-----------
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME + ' [' + TAN_NO + ']' " +
                "      FROM   MST_COMPANY " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //
            blnSelectComboExit = false;
            //-----------
        }
        #endregion

        #region ControlSummaryBasicInfo
        private void ControlSummaryBasicInfo(long FaYearID, string FormNo, string Qtr, long CompanyID)
        {
            //--
            pnlLine1.Visible = true;
            pnlLine2.Visible = true;
            //lblTotalRecordsReturn.Visible = true;
            //--
            #region F24Q
            strSQL = @"SELECT COUNT(*) 
                       FROM   TRN_DEDUCTEE_DETAILS,
                              TRN_BASIC_INFO
                       WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                       AND    TRN_BASIC_INFO.ASST_ID = " + FaYearID;
            //if (FormNo == T_FormNo.F24Q)
            //{
                strSQL = strSQL + " AND TRN_BASIC_INFO.FORM_NO ='" + T_FormNo.F24Q + "' ";
            //}
            //
            if (Qtr != "")
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.QTR ='" + Qtr + "' ";
            }
            //
            if (CompanyID > 0)
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.COMPANY_ID = " + CompanyID + " ";
            }
            lblForm24QCount.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (lblForm24QCount.Text == "")
            {
                lblForm24Q.Visible = false;
                lblForm24QCount.Visible = false;
            }
            #endregion
            //--
            #region F26Q
            strSQL = @"SELECT COUNT(*) 
                       FROM   TRN_DEDUCTEE_DETAILS,
                              TRN_BASIC_INFO
                       WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                       AND    TRN_BASIC_INFO.ASST_ID = " + FaYearID;
            //if (FormNo == T_FormNo.F26Q)
            //{
                strSQL = strSQL + " AND TRN_BASIC_INFO.FORM_NO ='" + T_FormNo.F26Q + "' ";
            //}
            //
            if (Qtr != "")
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.QTR ='" + Qtr + "' ";
            }
            //
            if (CompanyID > 0)
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.COMPANY_ID = " + CompanyID + " ";
            }
            lblForm26QCount.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (lblForm26QCount.Text == "")
            {
                lblForm26Q.Visible = false;
                lblForm26QCount.Visible = false;
            }
            #endregion
            //--
            #region F27Q
            strSQL = @"SELECT COUNT(*) 
                       FROM   TRN_DEDUCTEE_DETAILS,
                              TRN_BASIC_INFO
                       WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                       AND    TRN_BASIC_INFO.ASST_ID = " + FaYearID;
            //if (FormNo == T_FormNo.F27Q)
            //{
                strSQL = strSQL + " AND TRN_BASIC_INFO.FORM_NO ='" + T_FormNo.F27Q + "' ";
            //}
            //
            if (Qtr != "")
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.QTR ='" + Qtr + "' ";
            }
            //
            if (CompanyID > 0)
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.COMPANY_ID = " + CompanyID + " ";
            }
            lblForm27QCount.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (lblForm27QCount.Text == "")
            {
                lblForm27Q.Visible = false;
                lblForm27QCount.Visible = false;
            }
            #endregion
            //--
            #region F27EQ
            strSQL = @"SELECT COUNT(*) 
                       FROM   TRN_DEDUCTEE_DETAILS,
                              TRN_BASIC_INFO
                       WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                       AND    TRN_BASIC_INFO.ASST_ID = " + FaYearID;
            //if (FormNo == T_FormNo.F27EQ)
            //{
                strSQL = strSQL + " AND TRN_BASIC_INFO.FORM_NO ='" + T_FormNo.F27EQ + "' ";
            //}
            //
            if (Qtr != "")
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.QTR ='" + Qtr + "' ";
            }
            //
            if (CompanyID > 0)
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.COMPANY_ID = " + CompanyID + " ";
            }
            lblForm27EQCount.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (lblForm27EQCount.Text == "")
            {
                lblForm27EQ.Visible = false;
                lblForm27EQCount.Visible = false;
            }
            #endregion
            //--
            #region TOTAL
            if (FormNo == T_FormNo.F24Q)
            {
                lblForm26QCount.Text = "0";
                lblForm27QCount.Text = "0";
                lblForm27EQCount.Text = "0";
            }
            else if (FormNo == T_FormNo.F26Q)
            {
                lblForm24QCount.Text = "0";
                lblForm27QCount.Text = "0";
                lblForm27EQCount.Text = "0";
            }
            else if (FormNo == T_FormNo.F27Q)
            {
                lblForm24QCount.Text = "0";
                lblForm26QCount.Text = "0";
                lblForm27EQCount.Text = "0";
            }
            else if (FormNo == T_FormNo.F27EQ)
            {
                lblForm24QCount.Text = "0";
                lblForm26QCount.Text = "0";
                lblForm27QCount.Text = "0";
            }
            //--
            lblTotalCount.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblForm24QCount.Text) + cmnService.J_ReturnInt64Value(lblForm26QCount.Text) + cmnService.J_ReturnInt64Value(lblForm27QCount.Text) + cmnService.J_ReturnInt64Value(lblForm27EQCount.Text));
            //--
            if (lblForm24QCount.Text == "0")
                lblForm24QCount.Text = strNoRecordFound;
            //
            if (lblForm26QCount.Text == "0")
                lblForm26QCount.Text = strNoRecordFound;
            //
            if (lblForm27QCount.Text == "0")
                lblForm27QCount.Text = strNoRecordFound;
            //
            if (lblForm27EQCount.Text == "0")
                lblForm27EQCount.Text = strNoRecordFound;
            //
            #endregion
            //
        }
        #endregion


        //-- Added By Abhishek Dey On 22/05/2018 --
        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("SysShowUsage");
            Tan.ShowDialog();
            //--------------
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }

        #endregion
        //-----------------------------------------



        #endregion


        #region SysShowUsage_Load
        private void SysShowUsage_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
        }
        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0112", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}