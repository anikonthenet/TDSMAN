#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnForm24Q
Version			: 1.0
Start Date		: 29-10-2010
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

namespace TDSMAN.FormTrn
{
    public partial class TrnForm26Q : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnForm26Q()
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
        DataSet dsetGridClone = new DataSet();
        DataSet dsetChallanGridClone = new DataSet();
        DataSet dsetChallanDetailsGridClone = new DataSet();
        RptDialog rptDialog = new RptDialog();
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
        //Added by Dhrub Mukherjee On 08/11/2013
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";

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
        //----
        #region Tabbed_Mode
        private struct Tabbed_Mode
        {
            public const string Basic = "Basic Information";
            public const string Challan = "Challan Details";
            public const string Deductee = "Deductee Details";
            public const string GenerateTDS = "Generate TDS Return";
        }
        #endregion
        //----
        string strFormNo = T_FormNo.F26Q;

        // For NonSalary TDS RATE Calculation Dhrub on 29/01/2014
        string strSectionNo = "";

        int intBookmark = 0;
        //----------------------------------------------------------------
        //ADDED BY DHRUB ON 2014/01/11 DECLARE FOR DEDUCTEE DATE CHECKING
        //----------------------------------------------------------------
        string strChallanDepositDate = "";

        #endregion

        #region set CHALLAN Index through ENUM

        public enum enmCHALLANIndex
        {
            CHALLAN_ID = 0,
            SL_NO = 1,
            SECTION_ID = 2,
            SECTION_NAME = 3,
            DEPOSIT_DATE = 4,
            BSR_CODE = 5,
            CHALLAN_NO = 6,
            TRANSFER_VOUCHER_NO = 7,
            CHEQUE_NO = 8,
            TDS = 9,
            SURCHARGE = 10,
            EDUCATION_CESS = 11,
            INTEREST = 12,
            OTHERS = 13,
            TOT_TAX = 14,
            CTRL_TDS = 15,
            CTRL_SURCHARGE = 16,
            CTRL_EDU_CESS = 17,
            CTRL_TOT_TAX = 18,
            CTRL_TOT = 19,
            INTEREST_ALLOCATED = 20,
            OTHERS_ALLOCATED = 21,
            REMARKS = 22,
            BOOK_ENTRY = 23,
            FEE = 24,
            MINOR_HEAD = 25
        }
        #endregion

        #region set enmUPDATESrlNo

        public enum enmUPDATESrlNo
        {
            CHALLAN_ID = 0,
            SL_NO = 1
        }
        #endregion               

        #region User Defined Events

        #region TrnForm26Q_Load

        private void TrnForm26Q_Load(object sender, EventArgs e)
        {
            try
            {
                GC.Collect();
                //
                //Added by Indrajit on 28-09-2012
                tmrLoginRefresh.Interval = (int)TDSMAN.Classes.TDSMAN.T_pLockInterval * 60000;
                tmrLoginRefresh.Start();
                //-----
                //-----------------------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                BtnExit.Text = "E&xit from " + strFormNo;
                //-----------------------------------------------------------
                lblTitle.Text = "Form 26Q (Domestic)";
                //-----------------------------------------------------------
                //ViewGrid_Click(sender, e);
                BtnAdd_Click(sender, e);
                lblSearchMode.Text = "Basic Information";          
                //-----------
                tbcCompany.Enabled = false;
                //-----------
                //-- FINANCIAL YEAR
                //-----------
                strSQL = " SELECT ASST_ID," +
                    "             FA_YEAR " +
                    "      FROM   MST_ASSESSMENT " +
                    "      WHERE  VISIBILITY_FLAG = 0 " +
                    "      ORDER BY ASST_ID DESC";

                if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;

                //if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear) == false) return;
                //-----------
                //-- QUARTER
                //-----------
                string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
                dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter, 1);
                //-----------
                //-- COMPANY
                //-----------
                strSQL = " SELECT COMPANY_ID," +
                    "             COMPANY_NAME " +
                    "      FROM   MST_COMPANY " +
                    "      ORDER BY COMPANY_NAME";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
                //-----------
                cmbCompany.Select();
                //-----------------------------------------------------------


                //----------
                //Added by Dhrub On 08/11/2013 FOR Form Bookmark

                intBookmark = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT SHOW_BOOKMARK FROM MST_SETUP"));
                if (intBookmark == 0)
                    TDSMAN.Classes.TDSMAN.T_pBookMarkOption = false;
                else
                    TDSMAN.Classes.TDSMAN.T_pBookMarkOption = true;

                if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == false) return;

                strQuery = @" FORM_NAME= '" + strFormNo + "' ";

                if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL", strQuery) == true)
                {
                    intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.SHOW.ToString(), 0, "", strFormNo, 0, out strQuarter, out strCompanyName);
                    strSQL = @"SELECT FA_YEAR
                              FROM MST_ASSESSMENT
                              WHERE ASST_ID=" + intAsstId + "";
                    cmbFinancialYear.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));//Convert.ToInt32(Support.SetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex, intAsstId));
                    cmbQuarter.Text = strQuarter;
                    cmbCompany.Text = strCompanyName;
                }
                //----------------------------------------------------------
                //ADDED BY DHRUB ON 13/01/2014 FOR DEDUCTED DATE VISIBILITY 
                //----------------------------------------------------------
                TdsMan.GetSetup();                              
                //------------------
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }
        #endregion

        #region BtnAdd_Click

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                //---------------------------------------------
                lblMode.Text = J_Mode.View;
                lblSearchMode.Text = J_Mode.General;
                lblMode.ForeColor = Color.Yellow;
                //---------------------------------------------
                BtnSave.Enabled   = false;
                BtnSave.BackColor = Color.LightGray;
                //--
                BtnCancel.Enabled = false;
                BtnCancel.BackColor = Color.LightGray;
                //--
                BtnExit.Enabled = true;
                BtnExit.BackColor = Color.Lavender;
                //---------------------------------------------
                //ControlVisible(true);
                //ClearControls();					//Clear all the Controls
                //---------------------------------------------
                strCheckFields = "";
                cmbFinancialYear.Select();
                //---------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnEdit_Click
        private void BtnEdit_Click(object sender, System.EventArgs e)
        {
            if (lblSearchMode.Text == Tabbed_Mode.Challan)
            {
                dgcViewChallan_DoubleClick(sender, e);
            }
            else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
            {
                dgcViewDeductee_DoubleClick(sender, e);
            }
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            Insert_Update_Delete_Data();
        }
        #endregion

        #region BACK

            #region BtnCancel_Click
            private void BtnCancel_Click(object sender, System.EventArgs e)
            {
                try
                {
                    //Added by Indrajit on 23-02-2013 to add the basic information
                    //-----------------------------------------------------------
                    if (lngBasicInfoID == 0)
                    {
                        dmlService.J_BeginTransaction();

                        if (TdsMan.T_GenerateBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                cmbQuarter.Text,
                                                Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                strFormNo) == true)
                        {
                            strSQL = "SELECT COUNT(*) " +
                                "     FROM   TRN_BASIC_INFO " +
                                "     WHERE  ASST_ID     = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) +
                                "     AND    QTR         ='" + cmbQuarter.Text + "'" +
                                "     AND    COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) +
                                "     AND    FORM_NO     ='" + cmnService.J_ReplaceQuote(strFormNo) + "'";
                            //--
                            if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 1)
                            {
                                //cmnService.J_UserMessage("This header entry exists");
                                dmlService.J_Rollback();
                            }
                            else
                            {
                                //-----------------------------------------------
                                lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                    cmbQuarter.Text,
                                                    Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                    strFormNo);
                                //-----------------------------------------------
                                InsertCompanyBasicInfo(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), lngBasicInfoID);
                                dmlService.J_Commit();
                            }
                        }
                        else
                            dmlService.J_Rollback();
                    }
                    //-----------------------------------------------------------
                    //Added by Indrajit on 22-02-2013 to incorporate Basic Info locking
                    #region INCORPORATE_BASIC_NFO_LOCKING

                    TDSMAN.Classes.TDSMAN.T_pBasicInfoId = lngBasicInfoID;
                    //================================================
                    if (TdsMan.LockBasicInfoEntry(T_FormNo.F26Q) == false)
                    {
                        BtnExit.Select();
                        return;
                    }

                    #endregion
  
                    if (lblSearchMode.Text == Tabbed_Mode.Basic)
                    {
                        //Added by Dhrub On 08/11/2013 For BookMark
                        //---------------------------------------------------
                        if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
                        {
                            strQuery = @"FORM_NAME= '" + strFormNo + "' ";
                            if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL", strQuery) == true)
                            {
                                //Update the Form BookMarkDetail [T_TransactionMode.UPDATE]
                                intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.UPDATE.ToString(),
                                                                    Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                                    cmbQuarter.Text,
                                                                    strFormNo,
                                                                    Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                                    out strQuarter,
                                                                    out strCompanyName
                                                                   );
                            }
                            else
                            {
                                //Inserting the Form BookMarkDetail [T_TransactionMode.INSERT]
                                intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.INSERT.ToString(),
                                                                    Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                                    cmbQuarter.Text,
                                                                    strFormNo,
                                                                    Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                                    out strQuarter,
                                                                    out strCompanyName
                                                                   );
                            }
                        }
                        //---------------------------------------------------
                        if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                        {
                            if (cmbQuarter.Text == T_Qtr.Q4)
                            {
                                TrnTrialMessageBox TrialMessageBox = new TrnTrialMessageBox();
                                TrialMessageBox.StartPosition = FormStartPosition.CenterScreen;
                                TrialMessageBox.lblMessage2.Text = "Return cannot be generated for Qtr 4 in Trial Version";
                                TrialMessageBox.Show();
                                return;
                            }
                            //--####################
                            if (TdsMan.AllowTrialReturn(lngBasicInfoID) == false)
                            {
                                TrnTrialMessageBox TrialMessageBox = new TrnTrialMessageBox();
                                TrialMessageBox.StartPosition = FormStartPosition.CenterScreen;
                                //TrialMessageBox.lblMessage2.Text = "Return cannot be generated as the trial version supports only " + TDSMAN.Classes.TDSMAN.T_pMaxDeducteeDetailsCount + " nos. of deductee records in total.";
                                TrialMessageBox.lblMessage2.Text = "Return cannot be generated as the trial version supports only 1 return in total";
                                TrialMessageBox.Show();
                                return;
                            }
                            //--####################
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.PUBLICATION_HOUSE_VERSION)
                        {
                            if (cmbQuarter.Text != T_Qtr.Q1)
                            {
                                cmnService.J_UserMessage("Only 1st Quarter can be generated in this version");
                                return;
                            }
                        }
                        //-- ANIK @ 2013/09/26 for FVU 4.0
                        strSQL = "SELECT COUNT(*) " +
                                 "FROM  TRN_CHALLAN " + 
                                 "LEFT JOIN (SELECT COUNT(*) AS COUNT_REC, CHALLAN_ID FROM TRN_DEDUCTEE_DETAILS GROUP BY CHALLAN_ID) AS DED " +
                                 "ON TRN_CHALLAN.CHALLAN_ID       = DED.CHALLAN_ID " +
                                 "WHERE TRN_CHALLAN.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                                 "AND   " + cmnService.J_SQLDBFormat("DED.COUNT_REC", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " = 0 " +
                                 "AND   TRN_CHALLAN.TOT_TAX       = 0";
                        if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                        {
                            cmnService.J_UserMessage("Nil challans/transfer vouchers need to mandatorily have Deductee records");
                            return;
                        }
                        //--
                        if (cmbRegularStatement.Text == T_YES_NO.YES && txtPrevTokenNo.Text=="")
                        {   
                            cmnService.J_UserMessage("Enter Token No. of previous regular statement (26Q)");
                            txtPrevTokenNo.Select();
                            return;
                        }
                        //--
                        if (txtPrevTokenNo.Text != "")
                        {
                            if (txtPrevTokenNo.Text.Length < 15)
                            {
                                cmnService.J_UserMessage("Token No. should be of 15 digits");
                                txtPrevTokenNo.Select();
                                return;
                            }
                        }
                        //--
                        btnGenerateReturns_Click(sender, e);
                        //--
                    }
                    else
                    {
                        lblMode.Text = J_Mode.View;
                        lblMode.ForeColor = Color.Yellow;

                        lblSearchMode.Text = Tabbed_Mode.Basic;

                        tbcCompany.TabPages.Add(tbpBasicInformation);
                        tbcCompany.TabPages.Remove(tbpChallanDetails);

                        tbcCompany.SelectTab(tbpBasicInformation);

                        cmbMain_SelectedIndexChanged(sender, e);
                        ////---------------------------------------------
                        BtnSave.Enabled = false;
                        BtnSave.BackColor = Color.LightGray;
                        //--
                        btnChallanDeducteeEntry.Text = "Challan && Deductee Entry";
                        //--
                        BtnEdit.Enabled = false;
                        BtnEdit.BackColor = Color.LightGray;
                        //--
                        BtnDelete.Enabled = false;
                        BtnDelete.BackColor = Color.LightGray;
                        ////--
                        //BtnCancel.Enabled = false;
                        //BtnCancel.BackColor = Color.LightGray;
                        //BtnCancel.Text = "Back";
                        ////--
                        BtnSearch.Enabled = false;
                        BtnSearch.BackColor = Color.LightGray;
                        ////--
                        BtnSort.Enabled = false;
                        BtnSort.BackColor = Color.LightGray;
                        ////--
                        BtnRefresh.Enabled = false;
                        BtnRefresh.BackColor = Color.LightGray;
                        //--
                        //BtnSort_Click(sender, e);
                        //
                        grpSearch.Visible = false;
                        grpSearchDeductee.Visible = false;
                        lblMsgDisplay.Visible = false;
                        grpMain.Enabled = true;                        
                        //---------------------------------------------
                    }
                }
                catch (Exception err_handler)
                {
                    cmnService.J_UserMessage(err_handler.Message);
                }
            }
            #endregion
        
        #endregion

        #region CANCEL
        
        #region BtnSort_Click

        private void BtnSort_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSearchMode.Text == Tabbed_Mode.Challan)
                {
                    //--
                    if (lblMode.Text == J_Mode.Edit)
                    {
                        btnChallanDeducteeEntry.Enabled = true;
                        btnChallanDeducteeEntry.BackColor = Color.Lavender;
                        //
                        BtnEdit.Enabled = true;
                        BtnEdit.BackColor = Color.Lavender;
                        //--
                        BtnDelete.Enabled = true;
                        BtnDelete.BackColor = Color.Lavender;                    
                    }
                    //--
                    lblMode.Text = J_Mode.Add;
                    lblMode.ForeColor = Color.GreenYellow;
                    //--
                    ClearControls();
                    //--
                    lblInterestAllocated.Visible = false;
                    txtInterestAllocated.Visible = false;
                    lblOthersAllocated.Visible = false;
                    txtOthersAllocated.Visible = false;
                    //lblRemarks.Location = new Point(14, 334);
                    //txtRemarks.Location = new Point(74, 331);
                    //--
                    BackgroundColorChangeChallan(lblMode.Text);
                    //-- Generate Challan Srl No.
                    txtChallanSrlNo.Text = Convert.ToString(TdsMan.T_ReturnSrlNo(lngBasicInfoID, T_SRL_NO.CHALLAN));
                    cmbSection.Select();
                    //--
                    BtnSearch.Enabled = true;
                    BtnSearch.BackColor = Color.Lavender;
                    //--
                    BtnRefresh.Enabled = true;
                    BtnRefresh.BackColor = Color.Lavender;
                }
                else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
                {
                    //--
                    if (lblMode.Text == J_Mode.Edit)
                    {
                        btnChallanDeducteeEntry.Enabled = true;
                        btnChallanDeducteeEntry.BackColor = Color.Lavender;
                        //
                        BtnEdit.Enabled = true;
                        BtnEdit.BackColor = Color.Lavender;
                        //--
                        BtnDelete.Enabled = true;
                        BtnDelete.BackColor = Color.Lavender;
                    }
                    lblMode.Text = J_Mode.Add;
                    lblMode.ForeColor = Color.GreenYellow;

                    //--
                    ClearControls();

                    //lblSearchMode.Text = Tabbed_Mode.Basic;
                    //--
                    BackgroundColorChangeDeductee(lblMode.Text);
                    //-- Generate Challan Srl No.
                    txtDeducteeSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_DEDUCTEE_DETAILS", "SL_NO", "CHALLAN_ID = " + lngChallanID + "") + 1);
                    //--
                    BtnSearch.Enabled = true;
                    BtnSearch.BackColor = Color.Lavender;
                    //--
                    BtnRefresh.Enabled = true;
                    BtnRefresh.BackColor = Color.Lavender;

                    txtDeducteeName.Select();
                }
                else if (lblSearchMode.Text == Tabbed_Mode.Basic)
                {

                    //
                    grpMain.Enabled = true;
                    grpNILReturn.Enabled = true;
                    grpAddressChange.Enabled = true;
                    //-- ANIK 2013/09/27 FVU 4.0
                    grpLastProvisionalReceiptNo.Enabled = true;
                    //
                    if (chkNILReturn.Checked == false)
                    {
                        btnChallanDeducteeEntry.Enabled = true;
                        btnChallanDeducteeEntry.BackColor = Color.Lavender;
                    }
                    //
                    BtnCancel.Enabled = true;
                    BtnCancel.BackColor = Color.Lavender;
                    //
                    grpGenerateReturns.Visible = false;
                    //
                    grpLastGenerationStatus.Visible = false;
                    //
                    BtnSort.Enabled = false;
                    BtnSort.BackColor = Color.LightGray;

                    EnabilityNILReturn(lngBasicInfoID);
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region btnChallanEntry_Click
        private void btnChallanEntry_Click(object sender, EventArgs e)
        {
            //cmnService.J_UserMessage("Challan Details", MessageBoxButtons.OK);  
            BtnCancel.Visible = false;
            //
            if (lblSearchMode.Text == Tabbed_Mode.Basic)
            {
                tbcCompany.TabPages.Remove(tbpBasicInformation);
                //--
            }
            else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
            {
                tbcCompany.TabPages.Remove(tbpDeducteeDetails);
                //--
            }
            tbcCompany.TabPages.Add(tbpChallanDetails);
            //-----------------------------------------------------------
            lblMode.Text = J_Mode.Add;
            lblMode.ForeColor = Color.GreenYellow;
            lblSearchMode.Text = Tabbed_Mode.Challan;
            //--
            BackgroundColorChangeChallan(lblMode.Text);
            //---------------------------------------------
            BtnSave.Enabled = true;
            BtnSave.BackColor = Color.Lavender;
            //--
            BtnCancel.Enabled = true;
            BtnCancel.BackColor = Color.Lavender;
            BtnCancel.Text = "Back to Home Screen";
            //--
            BtnSort.Enabled = true;
            BtnSort.BackColor = Color.Lavender;
            //--
            BtnSearch.Enabled = true;
            BtnSearch.BackColor = Color.Lavender;
            //--
            BtnRefresh.Enabled = true;
            BtnRefresh.BackColor = Color.Lavender;
            //--
            //BtnDelete.Enabled = false;
            //BtnDelete.BackColor = Color.Lavender;
            //--
            //BtnExit.Enabled = false;
            //BtnExit.BackColor = Color.LightGray;
            //--
            lblInterestAllocated.Visible = false;
            txtInterestAllocated.Visible = false;
            lblOthersAllocated.Visible = false;
            txtOthersAllocated.Visible = false;
            //lblRemarks.Location = new Point(14, 334);
            //txtRemarks.Location = new Point(74, 331);
            //--
            btnChallanDeducteeEntry.Visible = true;
            btnChallanDeducteeEntry.Enabled = false;
            btnChallanDeducteeEntry.BackColor = Color.LightGray;
            //---------------------------------------------
            grpMain.Enabled = false;
            //--
            ClearControls();
            //-----------
            //-- SECTION
            //-----------
            strSQL = " SELECT SECTION_ID," +
                "             SECTION_NO " +
                "      FROM   MST_SECTION " +
                "      WHERE  FORM_NAME = '" + strFormNo + "' " +
                "      ORDER BY SECTION_ID";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbSection) == false) return;
            //-----------
            mskDateOfPayment.Text = System.DateTime.Now.ToString();
            txtBSRCode.Text = "";
            txtChequeNo.Text = "";
            //-----------------------------------------------------------
            //-- Generate Challan Srl No.
            txtChallanSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_CHALLAN", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
            //--
            if (txtDeductorType.Text.Substring(0, 1) == "A" || txtDeductorType.Text.Substring(0, 1) == "S")
            {
                //txtTransferVoucherNo.Enabled = true;
                //txtTransferVoucherNo.BackColor = Color.White;
                //
                chkBookEntry.Visible = true;
                lblSrlNo.Location = new Point(17, 14);
                txtChallanSrlNo.Location = new Point(102, 10);
            }
            else
            {
                //txtTransferVoucherNo.Enabled = false;
                //txtTransferVoucherNo.BackColor = Color.LightGray;
                //
                chkBookEntry.Visible = false;
                lblSrlNo.Location = new Point(85, 14);
                txtChallanSrlNo.Location = new Point(170, 10);
            }
            //--
            LoadChallanGrid(lngBasicInfoID, sender, e);
            //--
            cmbSection.Select();

            ControlSummaryVisible(false, false, false, false, false, false, false);
        }
        #endregion

        #region btnGenerateReturns_Click
        private void btnGenerateReturns_Click(object sender, EventArgs e)
        {
            if (cmnService.J_ReturnInt32Value(txtTotalChallanRecords.Text) <= 0)
            {
                cmnService.J_UserMessage("At least one Challan needed for File Generation");
                btnChallanDeducteeEntry.Select();
                return;
            }
            //
            if (TdsMan.IsUacEnabled() == true)
                cmnService.J_UserMessage("UAC (User Account Control) is enabled in your machine, which may restrict text file generation and FVU file creation.\n" +
                    "Please disable the UAC settings or call our Customer Care for technical assistance.", MessageBoxIcon.Exclamation);
            //--
            grpGenerateReturns.Visible = true;
            //
            grpMain.Enabled = false;
            grpNILReturn.Enabled = false;
            grpAddressChange.Enabled = false;
            //-- ANIK 2013/09/27 FVU 4.0
            grpLastProvisionalReceiptNo.Enabled = false;
            //
            btnChallanDeducteeEntry.Enabled = false;
            btnChallanDeducteeEntry.BackColor = Color.LightGray;
            //
            BtnCancel.Enabled = false;
            BtnCancel.BackColor = Color.LightGray;
            //
            chkCSIFileDownload.Checked = false;
            // CANCEL
            //BtnSort.Enabled = true;
            //BtnSort.BackColor = Color.Lavender;
            //
            btnOpenOutputFolder.Enabled = false;
            btnOpenOutputFolder.BackColor = Color.LightGray;
            //
            //Added by Shrey Kejriwal on 04/01/2014
            //Disable button for FVU generated Form 27A for returns not validated.
            btnFVU27A.Enabled = false;
            btnFVU27A.BackColor = Color.LightGray;
            //
            lblGenerateTDSMessage.Visible = true;
            //lblGenerateTDSMessage.Text = "TDS-MAN has automated the process of TDS return generation && validation using the FVU utility provided by Income Tax Dept.";
            lblGenerateTDSMessage.Text = "TDS filing involves following steps :";
            lblGenerateTDSMessage1.Text = " 1. Generation of text file. \n 2. Creation of FVU file using the text file. \n 3. Printing of Form 27A.";

            //lblAutomaticCSIFileDownloadMessage.Text = "To verify Challan data with Income Tax Dept. records (Internet Connection required)";

            txtAssessmentYear.Text = TdsMan.T_ReturnAssessmentYear(cmbFinancialYear.Text);

            txtInputFileName.Text = "";

            txtOutputFolder.Text = Path.Combine(Application.StartupPath, "FVU Files\\" + txtTAN.Text + "\\" +
                                                                          cmbFinancialYear.Text + "\\" +
                                                                          T_FormNo.F26Q + "\\" +
                                                                          cmbQuarter.Text);


            EnabilityLastGenerationStatus(lngBasicInfoID, false);
            //
            //if (TdsMan.T_CheckInternetConnectivty() == false)
            //    chkCSIFileDownload.Enabled = false;   

            //Added by Shrey Kejriwal on 04/10/2012
            //Checking if CSI file should be downloaded
            //i.e checking if any non challans is used in this return

            strSQL = "SELECT COUNT(*) " +
                     "FROM TRN_CHALLAN " +
                     "WHERE TOT_TAX > 0 " +
                     "AND   BOOK_ENTRY = 0 " +
                     "AND   BASIC_INFO_ID = " + lngBasicInfoID;

            int intCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

            if (intCount == 0) //No Challan found
            {
                chkCSIFileDownload.Checked = false;
                chkCSIFileDownload.Enabled = false;
            }
            else
            {
                chkCSIFileDownload.Enabled = true;

                //Now checking if the Internet Connection is present 
                //and accordingly selecting the Automatic CSI file download option

                if (TdsMan.T_CheckInternetConnectivty() == true)
                    chkCSIFileDownload.Checked = true;
                else
                    chkCSIFileDownload.Checked = false;
            }

            //Commented by Shrey Kejriwal on 04/10/2012
            //if (chkNILReturn.Checked == true)
            //    chkCSIFileDownload.Enabled = false;
            //else
            //    chkCSIFileDownload.Enabled = true;

            btnGenerateTextFile.Select();

        }
        #endregion

        #region BtnRefresh_Click
        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-----------------------------------------------------------
                //lblMode.Text = J_Mode.View;
                lblMode.Text = J_Mode.Add;
                //cmnService.J_StatusButton(this, lblMode.Text);
                ////-----------------------------------------------------------
                //lblSearchMode.Text = J_Mode.General;
                //-----------------------------------------------------------
                ClearControls();
                //-----------------------------------------------------------
                //if (dgcViewChallan.Visible == true)
                if (lblSearchMode.Text == Tabbed_Mode.Challan)
                {
                    BackgroundColorChangeChallan(lblMode.Text);

                    grpSearch.Visible = false;

                    strCheckFields = "";
                    strSQL = strQuery + "order by " + strOrderBy;
                    //-----------------------------------------------------------
                    if (dsetGridClone != null) dsetGridClone.Clear();
                    dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewChallan, strSQL, strMatrix);       //Show Data into the Grid
                    if (dsetGridClone == null) return;
                    //-----------------------------------------------------------
                    dmlService.J_setGridPosition(ref dgcViewChallan, dsetGridClone, "CHALLAN_ID", lngChallanID);
                    //
                    txtChallanSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_CHALLAN", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
                }
                else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
                {
                    BackgroundColorChangeDeductee(lblMode.Text);

                    grpSearchDeductee.Visible = false;

                    strCheckFields = "";
                    strSQL = strQuery + "order by " + strOrderBy;
                    //-----------------------------------------------------------
                    if (dsetGridClone != null) dsetGridClone.Clear();
                    dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewDeductee, strSQL, strMatrix);       //Show Data into the Grid
                    if (dsetGridClone == null) return;
                    //-----------------------------------------------------------
                    dmlService.J_setGridPosition(ref dgcViewDeductee, dsetGridClone, "DEDUCTEE_DETAIL_ID", lngDeducteeDetailID);
                    //
                    txtDeducteeSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_DEDUCTEE_DETAILS", "SL_NO", "CHALLAN_ID = " + lngChallanID + "") + 1);
                }
                //-----------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnDelete_Click
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lblMode.Text == J_Mode.Edit)
                return;

            lblMode.Text = J_Mode.Delete;

            grpSearch.Visible = false;
            grpSearchDeductee.Visible = false;

            Insert_Update_Delete_Data();

            if (lblSearchMode.Text == Tabbed_Mode.Challan)
                dgcViewChallan_Click(sender, e);
            else
                dgcViewDeductee_Click(sender, e);
        }

        #endregion

        #region btnFVU27A_Click
        private void btnFVU27A_Click(object sender, EventArgs e)
        {
            //Added by Shrey Kejriwal on 03/01/2014

            try
            {
                if (File.Exists(ReturnGeneratedForm27APath()))
                    Process.Start(ReturnGeneratedForm27APath());
                else
                    cmnService.J_UserMessage("Form 27A not found in the output folder.");
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region cmbMain_SelectedIndexChanged
        private void cmbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                ClearControls();
                tbcCompany.Visible = false;
                ControlSummaryVisible(false, false, false, false, false, false, false);
                ReturnFilingStatusVisible(false, false, false, false,false,false);
                //
                // GENERATE RETURNS
                BtnCancel.Visible = false;
                btnChallanDeducteeEntry.Visible = false;
                return;
            }
            if (cmbCompany.SelectedIndex <= 0)
            {
                ClearControls();
                tbcCompany.Visible = false;
                ControlSummaryVisible(false, false, false, false, false, false, false);
                ReturnFilingStatusVisible(false, false, false, false, false, false);
                //
                // GENERATE RETURNS
                BtnCancel.Visible = false;
                btnChallanDeducteeEntry.Visible = false;
                return;
            }
            if (cmbQuarter.Text == "")
            {
                ClearControls();
                tbcCompany.Visible = false;
                ControlSummaryVisible(false, false, false, false, false, false, false);
                ReturnFilingStatusVisible(false, false, false, false, false, false);
                //
                // GENERATE RETURNS
                BtnCancel.Visible = false;
                btnChallanDeducteeEntry.Visible = false;
                return;
            }
            //-----------------------------------------------
            ClearControls();
            //-----------------------------------------------
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        cmbQuarter.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        strFormNo);
            //--


            ////Added by Indrajit on 22-02-2013 to incorporate Basic Info locking
            //#region INCORPORATE_BASIC_NFO_LOCKING

            //TDSMAN.Classes.TDSMAN.T_pBasicInfoId = lngBasicInfoID;
            ////================================================
            //if (TdsMan.LockBasicInfoEntry(T_MODULENAME.FORM_26Q) == false)
            //{
            //    //cmbStartingChequeNo.SelectedIndex = 0;
            //    //lblChequeLeavesAvailable.Visible = false;
            //    BtnExit.Select();
            //    return;
            //}

            //#endregion
            
            
            if (LoadBasicDetails(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), lngBasicInfoID) == true)
            {
                tbcCompany.Enabled = true;
                tbcCompany.Visible = true;
                //
                // GENERATE RETURNS
                BtnCancel.Visible = true;
                BtnCancel.Text = "Generate Returns";
                btnChallanDeducteeEntry.Visible = true;                
            }
            else
                return;
            //-----------------------------------------------
            EnabilityNILReturn(lngBasicInfoID);
            EnabilityAddressChange(lngBasicInfoID);
            EnabilityReturnFilingStatus(lngBasicInfoID);
            //-- ANIK @ 2014-01-29
            string strPrevTokenNo = TdsMan.GetPreviousTokenNo(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), cmbQuarter.Text, strFormNo, Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
            //
            if (strPrevTokenNo != "" && txtPrevTokenNo.Text == "" && cmbRegularStatement.Text == T_YES_NO.YES)
                txtPrevTokenNo.Text = strPrevTokenNo;
            // CONTROL SUMMARY
            ControlSummaryBasicInfo(lngBasicInfoID);
            //
            EnabilityGrpButtons(lngBasicInfoID);
        }
        #endregion

        #region cmbFinancialYear_KeyPress
        private void cmbFinancialYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbQuarter_KeyPress
        private void cmbQuarter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbCompany_KeyPress
        private void cmbCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region tbcCompany_SelectedIndexChanged
        private void tbcCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcCompany.SelectedTab == tbpBasicInformation)
            {
                //cmnService.J_UserMessage("Basic Information", MessageBoxButtons.OK);
                lblMode.Text = J_Mode.View;
                lblSearchMode.Text = Tabbed_Mode.Basic;
                //---------------------------------------------
                BtnSave.Enabled = false;
                BtnSave.BackColor = Color.LightGray;
                //--
                BtnCancel.Enabled = false;
                BtnCancel.BackColor = Color.LightGray;
                //--
                BtnExit.Enabled = true;
                BtnExit.BackColor = Color.Lavender;
                //---------------------------------------------
                grpMain.Enabled = true;
            }
            else if (tbcCompany.SelectedTab == tbpChallanDetails)
            {
                //cmnService.J_UserMessage("Challan Details", MessageBoxButtons.OK);
                //-----------------------------------------------------------
                lblMode.Text = J_Mode.Add;
                lblSearchMode.Text = Tabbed_Mode.Challan;
                //---------------------------------------------
                BtnSave.Enabled = true;
                BtnSave.BackColor = Color.Lavender;
                //--
                BtnCancel.Enabled = true;
                BtnCancel.BackColor = Color.Lavender;
                //--
                //BtnExit.Enabled = false;
                //BtnExit.BackColor = Color.LightGray;
                //---------------------------------------------
                grpMain.Enabled = false;
                //--
                ClearControls();
                //-----------------------------------------------------------
                //-- Generate Challan Srl No.
                //txtChallanSrlNo.Text = Convert.ToString(TdsMan.T_ReturnSrlNo(lngBasicInfoID,T_SRL_NO.CHALLAN));
                txtChallanSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_CHALLAN", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
                //--
                if (txtDeductorType.Text.Substring(0, 1) == "A" || txtDeductorType.Text.Substring(0, 1) == "S")
                {
                    txtTransferVoucherNo.Enabled = true;
                    txtTransferVoucherNo.BackColor = Color.White;
                }
                else
                {
                    txtTransferVoucherNo.Enabled = false;
                    txtTransferVoucherNo.BackColor = Color.LightGray;
                }
                //--
                LoadChallanGrid(lngBasicInfoID);
                //--
                cmbSection.Select();
            }
            else if (tbcCompany.SelectedTab == tbpDeducteeDetails)
            {
                //cmnService.J_UserMessage("Deductee Details", MessageBoxButtons.OK);
                lblMode.Text = J_Mode.Add;
                lblSearchMode.Text = Tabbed_Mode.Deductee;

                BtnSave.Enabled = true;
                BtnSave.BackColor = Color.Lavender;
                
                BtnCancel.Enabled = true;
                BtnCancel.BackColor = Color.Lavender;
                
                //BtnExit.Enabled = false;
                //BtnExit.BackColor = Color.LightGray;
                
                grpMain.Enabled = false;
                
            }
            else if (tbcCompany.SelectedTab == tbpGenerateTDSReturn)
            {
                //cmnService.J_UserMessage("Generate TDS Return", MessageBoxButtons.OK);
                lblMode.Text = J_Mode.Add;
                lblSearchMode.Text = Tabbed_Mode.GenerateTDS;

                BtnSave.Enabled = true;
                BtnSave.BackColor = Color.Lavender;

                BtnCancel.Enabled = true;
                BtnCancel.BackColor = Color.Lavender;

                //BtnExit.Enabled = false;
                //BtnExit.BackColor = Color.LightGray;

                grpMain.Enabled = false;
                //--
                ClearControls();
                //--
                //LoadDeducteeGrid(lngChallanDetailID);
                if (ShowRecordGenerateTDS(lngBasicInfoID) == false)
                {
                    //btnValidate.Enabled = false;
                    //btnValidate.BackColor = Color.Gray;
                    btnErrorViewClose.Enabled = false;
                    btnErrorViewClose.BackColor = Color.Gray;
                    //btnFVUPathModify.Enabled = false;
                    //btnFVUPathModify.BackColor = Color.Gray;
                    //btnInputFilePathModify.Enabled = false;
                    //btnInputFilePathModify.BackColor = Color.Gray;
                }
                else
                {
                    //btnValidate.Enabled = true;
                    //btnValidate.BackColor = Color.Blue;
                    btnErrorViewClose.Enabled = false;
                    btnErrorViewClose.BackColor = Color.Gray;
                    //btnFVUPathModify.Enabled = true;
                    //btnFVUPathModify.BackColor = Color.Blue;
                    //btnInputFilePathModify.Enabled = true;
                    //btnInputFilePathModify.BackColor = Color.Blue;

                    //btnValidate.Select();
                }
            }
        }
        #endregion

        #region lnkViewCompanyDetails_LinkClicked
        private void lnkViewCompanyDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_pCompanyId = Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
            TDSMAN.Classes.TDSMAN.T_pFormNo = strFormNo;
            TDSMAN.Classes.TDSMAN.T_pBasicInfoId = lngBasicInfoID;
            //
            MstViewCompanyDetails ViewForm = new MstViewCompanyDetails();
            ViewForm.MdiParent = TrnForm26Q.ActiveForm;
            ViewForm.Show();
            ////cmnService.J_ShowChildForm(new TrnForm26Q(), this, "Form 26Q");
        }
        #endregion

        #region lnkViewRPDetails_LinkClicked
        private void lnkViewRPDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_pCompanyId = Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
            TDSMAN.Classes.TDSMAN.T_pFormNo = strFormNo;
            TDSMAN.Classes.TDSMAN.T_pBasicInfoId = lngBasicInfoID;
            //
            MstViewResponsiblePersonDetails ViewForm = new MstViewResponsiblePersonDetails();
            ViewForm.MdiParent = TrnForm26Q.ActiveForm;
            ViewForm.Show();
        }
        #endregion

        #region SEARCH

        #region BtnSearch_Click
        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2013_14ID)
                {
                    label109.Visible = false;
                    cmbSectionNoSearch.Visible = false;
                }
                else
                {
                    label109.Visible = true;
                    cmbSectionNoSearch.Visible = true;
                }
                //Added by Indrajit on 04-03-2013
                strCheckFields = "";
                //-------------------------------------------
                //lblSearchMode.Text = J_Mode.Searching;
                lblMode.Text = J_Mode.Searching;
                //-------------------------------------------
                if (ValidateFields() == false) return;
                //-------------------------------------------
                grpSort.Visible = false;
                //-------------------------------------------
                //if (dgcViewChallan.Visible == true)
                if(lblSearchMode.Text == Tabbed_Mode.Challan)
                {
                    grpSearch.Visible = true;
                    //-------------------------------------------
                    // SEARCH MODE

                    txtSlNoSearch.Text = "";
                    //-----------
                    //-- SECTION
                    //-----------
                    strSQL = " SELECT SECTION_ID," +
                        "             SECTION_NO " +
                        "      FROM   MST_SECTION " +
                        "      WHERE  FORM_NAME = '" + strFormNo + "' " +
                        "      ORDER BY SECTION_ID";
                    if (dmlService.J_PopulateComboBox(strSQL, ref cmbSectionNoSearch) == false) return;
                    //-----------                
                    mskDepositDateSearch.Text = "";
                    txtChallanNoSearch.Text = "";
                    txtTransferVoucherNoSearch.Text = "";

                    txtSlNoSearch.Select();
                }
                //else if(dgcViewChallanDetails.Visible == true)
                else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
                {
                    grpSearchDeductee.Visible = true;
                    //-------------------------------------------
                    // SEARCH MODE

                    txtSlNoDSearch.Text = "";
                    txtPANSearch.Text = "";
                    txtDeducteeNameSearch.Text = "";
                    mskDateSearch.Text = "";

                    txtSlNoDSearch.Select();
                }
                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_Click
        private void BtnSearchOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                //----------------------------------------------------------------------
                //-- In case the user enters a blank user name -------------------------
                //----------------------------------------------------------------------
                if (ValidateFields() == false) return;
                //----------------------------------------------------------------------
                strCheckFields = "";
                //if (dgcViewChallan.Visible == true)
                if (lblSearchMode.Text == Tabbed_Mode.Challan)
                {
                    //----------------------------------------------------------------------
                    //-- SL. NO.
                    //----------------------------------------------------------------------
                    if (txtSlNoSearch.Text.Trim() != "")
                        strCheckFields = strCheckFields + "AND TRN_CHALLAN.SL_NO = " + cmnService.J_ReturnDoubleValue(txtSlNoSearch.Text) + " ";
                    //----------------------------------------------------------------------
                    //-- SECTION NO.
                    //----------------------------------------------------------------------
                    if (cmbSectionNoSearch.SelectedIndex > 0)
                        strCheckFields = strCheckFields + "AND MST_SECTION.SECTION_ID = " + Convert.ToInt32(Support.GetItemData(cmbSectionNoSearch, cmbSectionNoSearch.SelectedIndex)) + " ";
                    //----------------------------------------------------------------------
                    //-- CHALLAN NO
                    //----------------------------------------------------------------------
                    if (txtChallanNoSearch.Text.Trim() != "")
                        strCheckFields = strCheckFields + "AND TRN_CHALLAN.CHALLAN_NO like '%" + cmnService.J_ReplaceQuote(txtChallanNoSearch.Text.Trim().ToUpper()) + "%' ";
                    //----------------------------------------------------------------------
                    //-- TRANSFER VOUCHER NO
                    //----------------------------------------------------------------------
                    if (txtTransferVoucherNoSearch.Text.Trim() != "")
                        strCheckFields = strCheckFields + "AND TRN_CHALLAN.TRANSFER_VOUCHER_NO like '%" + cmnService.J_ReplaceQuote(txtTransferVoucherNoSearch.Text.Trim().ToUpper()) + "%' ";
                    //----------------------------------------------------------------------
                    //-- DEPOSIT DATE
                    //----------------------------------------------------------------------
                    if (dtService.J_IsBlankDateCheck(ref mskDepositDateSearch, J_ShowMessage.NO) == false)
                        strCheckFields = "AND TRN_CHALLAN.DEPOSIT_DATE =  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDepositDateSearch) + cmnService.J_DateOperator() + " ";
                    //----------------------------------------------------------------------
                    strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                    //----------------------------------------------------------------------
                    if (dsetGridClone != null) dsetGridClone.Clear();
                    dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewChallan, strSQL, strMatrix);       //Show Data into the Grid
                    if (dsetGridClone == null) return;
                    //----------------------------------------------------------------------
                    if (dmlService.J_setGridPosition(ref dgcViewChallan, dsetGridClone, "CHALLAN_ID", lngChallanID) == false)
                    {
                        txtSlNoSearch.Select();
                        return;
                    }
                    //----------------------------------------------------------------------
                    lngChallanID = Convert.ToInt64(Convert.ToString(dgcViewChallan[dgcViewChallan.CurrentRowIndex, 0]));
                    if (lngChallanID > 0)
                    {
                        if (lblMode.Text == J_Mode.Add)
                        {
                            btnChallanDeducteeEntry.Enabled = true;
                            btnChallanDeducteeEntry.BackColor = Color.Lavender;
                            //
                            BtnEdit.Enabled = true;
                            BtnEdit.BackColor = Color.Lavender;
                        }
                    }
                    else
                    {
                        btnChallanDeducteeEntry.Enabled = false;
                        btnChallanDeducteeEntry.BackColor = Color.LightGray;
                    }
                    //----------------------------------------------------------------------
                    //lblSearchMode.Text = J_Mode.General;
                    lblMode.Text = J_Mode.Add;
                    //----------------------------------------------------------------------
                    grpSearch.Visible = false;
                    //----------------------------------------------------------------------
                }
                //else if (dgcViewChallanDetails.Visible == true)
                else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
                {
                    //----------------------------------------------------------------------
                    //-- SL. NO.
                    //----------------------------------------------------------------------
                    if (txtSlNoDSearch.Text.Trim() != "")
                        strCheckFields = strCheckFields + "AND TRN_DEDUCTEE_DETAILS.SL_NO = " + cmnService.J_ReturnDoubleValue(txtSlNoDSearch.Text) + " ";
                    //----------------------------------------------------------------------
                    //-- CHALLAN NO
                    //----------------------------------------------------------------------
                    if (txtPANSearch.Text.Trim() != "")
                        strCheckFields = strCheckFields + "AND MST_DEDUCTEE.DEDUCTEE_PAN ='" + cmnService.J_ReplaceQuote(txtPANSearch.Text.Trim().ToUpper()) + "' ";
                    //----------------------------------------------------------------------
                    //-- TRANSFER VOUCHER NO
                    //----------------------------------------------------------------------
                    if (txtDeducteeNameSearch.Text.Trim() != "")
                        strCheckFields = strCheckFields + "AND MST_DEDUCTEE.DEDUCTEE_NAME like '" + cmnService.J_ReplaceQuote(txtDeducteeNameSearch.Text.Trim().ToUpper()) + "%' ";
                    //----------------------------------------------------------------------
                    //-- DEPOSIT DATE
                    //----------------------------------------------------------------------
                    if (dtService.J_IsBlankDateCheck(ref mskDateSearch, J_ShowMessage.NO) == false)
                        strCheckFields = strCheckFields + "AND TRN_DEDUCTEE_DETAILS.PAYMENT_DATE =  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDateSearch) + cmnService.J_DateOperator() + " ";
                    //----------------------------------------------------------------------
                    strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                    //----------------------------------------------------------------------
                    if (dsetGridClone != null) dsetGridClone.Clear();
                    dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewDeductee, strSQL, strMatrix);       //Show Data into the Grid
                    if (dsetGridClone == null) return;
                    //----------------------------------------------------------------------
                    if (dmlService.J_setGridPosition(ref dgcViewDeductee, dsetGridClone, "DEDUCTEE_DETAIL_ID", lngDeducteeDetailID) == false)
                    {
                        txtSlNoDSearch.Select();
                        return;
                    }
                    //----------------------------------------------------------------------
                    //lblSearchMode.Text = J_Mode.General;
                    lblMode.Text = J_Mode.Add;
                    //----------------------------------------------------------------------
                    grpSearchDeductee.Visible = false;
                    //----------------------------------------------------------------------
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_KeyPress
        private void BtnSearchOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnSearchCancel_Click
        private void BtnSearchCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                //----------------------------------------------------------------------
                //lblSearchMode.Text = J_Mode.General;
                lblMode.Text = J_Mode.Add;
                grpSearch.Visible = false;
                grpSearchDeductee.Visible = false;
                //----------------------------------------------------------------------
                //if (dgcViewChallan.Visible == true)
                if (lblSearchMode.Text == Tabbed_Mode.Challan)
                {
                    if (strCheckFields == "")
                        strSQL = strQuery + "order by " + strOrderBy;
                    else
                        strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                    //----------------------------------------------------------------------
                    if (dsetGridClone != null) dsetGridClone.Clear();
                    dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewChallan, strSQL, strMatrix);       //Show Data into the Grid
                    if (dsetGridClone == null) return;
                    //----------------------------------------------------------------------
                    dmlService.J_setGridPosition(ref dgcViewChallan, dsetGridClone, "CHALLAN_ID", lngChallanID);
                    //----------------------------------------------------------------------
                    if(dgcViewChallan.CurrentRowIndex < 0 )  return;
                    lngChallanID = Convert.ToInt64(Convert.ToString(dgcViewChallan[dgcViewChallan.CurrentRowIndex, 0]));
                    if (lngChallanID > 0)
                    {
                        if (lblMode.Text == J_Mode.Add)
                        {
                            btnChallanDeducteeEntry.Enabled = true;
                            btnChallanDeducteeEntry.BackColor = Color.Lavender;
                            //
                            BtnEdit.Enabled = true;
                            BtnEdit.BackColor = Color.Lavender;
                        }
                    }
                    else
                    {
                        btnChallanDeducteeEntry.Enabled = false;
                        btnChallanDeducteeEntry.BackColor = Color.LightGray;
                    }
                    //----------------------------------------------------------------------
                }
                if (lblSearchMode.Text == Tabbed_Mode.Deductee)
                {
                    if (strCheckFields == "")
                        strSQL = strQuery + "order by " + strOrderBy;
                    else
                        strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                    //----------------------------------------------------------------------
                    if (dsetGridClone != null) dsetGridClone.Clear();
                    dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewDeductee, strSQL, strMatrix);       //Show Data into the Grid
                    if (dsetGridClone == null) return;
                    //----------------------------------------------------------------------
                    dmlService.J_setGridPosition(ref dgcViewDeductee, dsetGridClone, "DEDUCTEE_DETAIL_ID", lngDeducteeDetailID);
                    //----------------------------------------------------------------------
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchCancel_KeyPress
        private void BtnSearchCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #endregion

        #region txtSlNoSearch_KeyPress
        private void txtSlNoSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);

            //Modified by Indrajit on 05-03-2013
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,5,0", txtSlNoSearch, "") == false)
            //    e.Handled = true;
            TextBox txtsender = (TextBox)sender;
            if (txtsender.Name == "txtSlNoSearch")
            {
                if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,5,0", txtSlNoSearch, "") == false)
                    e.Handled = true;
            }
            else if (txtsender.Name == "txtSlNoDSearch")
            {
                if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,5,0", txtSlNoDSearch, "") == false)
                    e.Handled = true;
            }

        }
        #endregion

        #region txtSlNoDSearch_KeyPress
        private void txtSlNoDSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,5,0", txtSlNoDSearch, "") == false)
                e.Handled = true;
        }
        #endregion

        #region ChallanDetails

        #region dgcViewChallan_Click
        private void dgcViewChallan_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(Convert.ToString(dgcViewChallan.CurrentRowIndex)) < 0)
            {
                cmbSection.Focus();
                return;
            }

            if (lblMode.Text != J_Mode.Edit)
                lngChallanID = Convert.ToInt64(Convert.ToString(dgcViewChallan[dgcViewChallan.CurrentRowIndex, 0]));

            if (lngChallanID > 0)
            {
                if (lblMode.Text == J_Mode.Add)
                {
                    btnChallanDeducteeEntry.Enabled = true;
                    btnChallanDeducteeEntry.BackColor = Color.Lavender;
                    //
                    BtnEdit.Enabled = true;
                    BtnEdit.BackColor = Color.Lavender;
                    //
                    BtnDelete.Enabled = true;
                    BtnDelete.BackColor = Color.Lavender;
                }
            }
            else
            {
                btnChallanDeducteeEntry.Enabled = false;
                btnChallanDeducteeEntry.BackColor = Color.LightGray;

                BtnEdit.Enabled = false;
                BtnEdit.BackColor = Color.LightGray;
            }
            //----------------------------------------------------
            dgcViewChallan.Select(dgcViewChallan.CurrentRowIndex);
            dgcViewChallan.Select();
            dgcViewChallan.Focus();
        }
        #endregion

        #region dgcViewChallan_DoubleClick
        private void dgcViewChallan_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgcViewChallan.CurrentRowIndex >= 0)
                {
                    //--------------------------------------------------
                    ClearControls();
                    //--------------------------------------------------
                    //A particular ID wise retriving the data from database
                    if (ShowChallanRecord(Convert.ToInt64(Convert.ToString(dgcViewChallan[dgcViewChallan.CurrentRowIndex, 0]))) == false)
                    {
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref dgcViewChallan, dsetGridClone, "CHALLAN_ID", lngChallanID);
                    }
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    //--
                    btnChallanDeducteeEntry.Enabled = false;
                    btnChallanDeducteeEntry.BackColor = Color.LightGray;
                    //--
                    lblInterestAllocated.Visible = true;
                    txtInterestAllocated.Visible = true;
                    lblOthersAllocated.Visible = true;
                    txtOthersAllocated.Visible = true;
                    //lblRemarks.Location = new Point(14, 383);
                    //txtRemarks.Location = new Point(74,380);
                    //--
                    BackgroundColorChangeChallan(lblMode.Text);
                    //--
                    BtnSort.Enabled = true;
                    BtnSort.BackColor = Color.Lavender;
                    //--
                    //BtnSearch.Enabled = true;
                    //BtnSearch.BackColor = Color.Lavender;
                    ////--
                    //BtnRefresh.Enabled = true;
                    //BtnRefresh.BackColor = Color.Lavender;
                    //--
                    BtnExit.Enabled = true;
                    BtnExit.BackColor = Color.Lavender;
                    //lblSearchMode.Text = J_Mode.General;
                    //--------------------------------------------------
                    strCheckFields = "";
                    //--------------------------------------------------
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref dgcViewChallan, dsetGridClone, "CHALLAN_ID", lngChallanID);
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region dgcViewChallan_KeyDown
        private void dgcViewChallan_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgcViewChallan.CurrentRowIndex == -1) return;
                lngChallanID = Convert.ToInt64(Convert.ToString(dgcViewChallan[dgcViewChallan.CurrentRowIndex, 0]));
                if (e.KeyCode == Keys.Enter) dgcViewChallan_DoubleClick(sender, e);
                if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);

                //strTempMode = lblMode.Text;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region dgcViewChallan_CurrentCellChanged
        private void dgcViewChallan_CurrentCellChanged(object sender, EventArgs e)
        {
            if (lblMode.Text != J_Mode.Edit)
                lngChallanID = Convert.ToInt64(Convert.ToString(dgcViewChallan[dgcViewChallan.CurrentRowIndex, 0]));

            if (lngChallanID > 0)
            {
                if (lblMode.Text == J_Mode.Add)
                {
                    btnChallanDeducteeEntry.Enabled = true;
                    btnChallanDeducteeEntry.BackColor = Color.Lavender; 
                    //
                    BtnEdit.Enabled = true;
                    BtnEdit.BackColor = Color.Lavender;
                }
            }
            else
            {
                btnChallanDeducteeEntry.Enabled = false;
                btnChallanDeducteeEntry.BackColor = Color.LightGray;
                //
                BtnEdit.Enabled = false;
                BtnEdit.BackColor = Color.LightGray;
            }

            //dgcViewChallan_DoubleClick(sender, e);
        }
        #endregion

        #region dgcViewChallan_MouseClick
        private void dgcViewChallan_MouseClick(object sender, MouseEventArgs e)
        {
            dgcViewChallan_Click(sender, e);
        }
        #endregion

        #region dgcViewChallan_MouseMove
        private void dgcViewChallan_MouseMove(object sender, MouseEventArgs e)
        {
            //TdsMan.T_ShowDataGridToolTip(dgcViewChallan, e.X, e.Y);
        }
        #endregion

        #region chkBookEntry_CheckedChanged
        private void chkBookEntry_CheckedChanged(object sender, EventArgs e)
        {
            if(chkBookEntry.Checked == true)
            {
                //BSR Code/24G Rcpt # [410]
                lblBSRCode.Text = "24G Receipt No. [408]";
                lblTrVchNo.Text = "Tr Vch(DDO Sl.) [409]";

                txtChallanNo.Visible = false;
                txtChallanNo.Text = "";

                txtTransferVoucherNo.Visible = true;
                if (lblMode.Text == J_Mode.Edit)
                    txtTransferVoucherNo.BackColor = Color.Honeydew;
                else
                    txtTransferVoucherNo.BackColor = Color.White;

                txtChequeNo.Enabled = false;
                txtChequeNo.BackColor = Color.LightGray;
                txtChequeNo.Text = "";

                //ADDED BY DHRUB ON 09/01/2014 TO DISABLE BOOK ENTRY WISE MINOR HEAD 
                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2013_14ID)
                {
                    lblMinor.Visible = false;
                    cmbMinorHead.Visible = false;
                    cmbMinorHead.SelectedIndex = 0;
                }
            }
            else if (chkBookEntry.Checked == false)
            {
                lblBSRCode.Text = "BSR Code [408]";
                lblTrVchNo.Text = "Challan No. [409]";

                txtTransferVoucherNo.Visible = false;
                txtTransferVoucherNo.Text = "";
                
                txtChallanNo.Visible = true;
                if (lblMode.Text == J_Mode.Edit)
                    txtChallanNo.BackColor = Color.Honeydew;
                else
                    txtChallanNo.BackColor = Color.White;

                txtChequeNo.Enabled = true;
                if (lblMode.Text == J_Mode.Edit)
                    txtChequeNo.BackColor = Color.Honeydew;
                else
                    txtChequeNo.BackColor = Color.White;

                //ADDED BY DHRUB ON 09/01/2014 TO DISABLE BOOK ENTRY WISE MINOR HEAD 
                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2013_14ID)
                {
                    lblMinor.Visible = true;
                    cmbMinorHead.Visible = true;
                }
            }
        }
        #endregion

        #region txtChallanSrlNo_KeyPress
        private void txtChallanSrlNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbSection_KeyPress
        private void cmbSection_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbSection_SelectedIndexChanged
        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnSectionDisplay == false)
                return;

            if (cmbSection.SelectedIndex <= 0)
            {
                lblSectionDisplay.Visible = false;
                return;
            }

            lblSectionDisplay.Visible = true;
            lblSectionDisplay.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_DESCRIPTION FROM MST_SECTION WHERE SECTION_ID = " + Convert.ToInt32(Support.GetItemData(cmbSection, cmbSection.SelectedIndex)))));
        }
        #endregion

        #region mskDateOfPayment_KeyPress
        private void mskDateOfPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtBSRCode_KeyPress
        private void txtBSRCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtBSRCode, "") == false)
            //    e.Handled = true;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        #endregion
        
        #region txtChallanNo_KeyPress
        private void txtChallanNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtChallanNo, "") == false)
            //    e.Handled = true;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

        }
        #endregion

        #region txtChallanNo_TextChanged
        private void txtChallanNo_TextChanged(object sender, EventArgs e)
        {
            if (txtChallanNo.Text.Trim() == "")
            {
                txtTransferVoucherNo.Enabled = true;
                txtTransferVoucherNo.BackColor = Color.White;
            }
            else
            {
                txtTransferVoucherNo.Enabled = false;
                txtTransferVoucherNo.BackColor = Color.LightGray;
            }
        }
        #endregion
        
        #region txtTransferVoucherNo_KeyPress
        private void txtTransferVoucherNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTransferVoucherNo, "") == false)
            //    e.Handled = true;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        #endregion

        #region txtTransferVoucherNo_TextChanged
        private void txtTransferVoucherNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTransferVoucherNo.Text.Trim() == "")
            {
                txtChallanNo.Enabled = true;
                txtChallanNo.BackColor = Color.White;
            }
            else
            {
                txtChallanNo.Enabled = false;
                txtChallanNo.BackColor = Color.LightGray;
            }
        }
        #endregion
        
        #region txtChequeNo_KeyPress
        private void txtChequeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtTDS_TextChanged
        private void txtTDS_TextChanged(object sender, EventArgs e)
        {
            CalcChallanTotTax();
        }
        #endregion

        #region txtTDS_KeyPress
        private void txtTDS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTDS, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSurcharge_TextChanged
        private void txtSurcharge_TextChanged(object sender, EventArgs e)
        {
            CalcChallanTotTax();
        }
        #endregion

        #region txtSurcharge_KeyPress
        private void txtSurcharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSurcharge, "") == false)
                e.Handled = true;
        }
        #endregion
        
        #region txtEducationCess_TextChanged
        private void txtEducationCess_TextChanged(object sender, EventArgs e)
        {
            CalcChallanTotTax();
        }
        #endregion

        #region txtEducationCess_KeyPress
        private void txtEducationCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtEducationCess, "") == false)
                e.Handled = true;
        }
        #endregion
        
        #region txtInterests_TextChanged
        private void txtInterests_TextChanged(object sender, EventArgs e)
        {
            CalcChallanTotTax();
            txtInterestAllocated.Text = txtInterests.Text;
        }
        #endregion

        #region txtInterests_KeyPress
        private void txtInterests_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtInterests, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtOthers_TextChanged
        private void txtOthers_TextChanged(object sender, EventArgs e)
        {
            CalcChallanTotTax();
            txtOthersAllocated.Text = txtOthers.Text;
        }
        #endregion

        #region txtOthers_KeyPress
        private void txtOthers_KeyPress(object sender, KeyPressEventArgs e)
        {
            //-----------------------------------------------
            //MODIFIED BY DHRUB FOR TAB ORDER ON 09/01/2014
            //-----------------------------------------------
            if (Convert.ToInt64(e.KeyChar) == 13) //SendKeys.Send("{tab}");
            {
                if (lblMode.Text == J_Mode.Edit && cmbMinorHead.Visible == false)
                        txtRemarks.Focus();
                else
                    SendKeys.Send("{tab}");
            }
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOthers, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtFee_TextChanged
        private void txtFee_TextChanged(object sender, EventArgs e)
        {
            CalcChallanTotTax();
        }
        #endregion

        #region txtFee_KeyPress
        private void txtFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtFee, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtTotalTax_KeyPress
        private void txtTotalTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTotalTax, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtInterestAllocated_KeyPress
        private void txtInterestAllocated_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtInterestAllocated, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtOthersAllocated_KeyPress
        private void txtOthersAllocated_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                if (lblMode.Text == J_Mode.Edit)
                    BtnSave.Focus();
                else
                    SendKeys.Send("{tab}");
            }
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            else if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOthersAllocated, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtRemarks_KeyPress
        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            //-----------------------------------------------
            //MODIFIED BY DHRUB FOR TAB ORDER ON 09/01/2014
            //-----------------------------------------------
            if (Convert.ToInt64(e.KeyChar) == 13) //SendKeys.Send("{tab}");
            {
                if (lblMode.Text == J_Mode.Edit)
                        txtInterestAllocated.Focus(); 
                else
                    BtnSave.Select(); 
            }
        }
        #endregion

        #region txtRemarks_Leave
        private void txtRemarks_Leave(object sender, EventArgs e)
        {
            BtnSave.Select();
        }
        #endregion

        #region chkBookEntry_KeyPress
        private void chkBookEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion 

        #endregion

        #region DeducteeDetails

        #region btnChallanDeducteeEntry_Click
        private void btnChallanDeducteeEntry_Click(object sender, EventArgs e)
        {
            //Added by Dhrub On 08/11/2013 For BookMark
            //-----------
            if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
            {
                strQuery = @"FORM_NAME= '" + strFormNo + "' ";
                if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL", strQuery) == true)
                {
                    //Inserting the Form BookMarkDetail [T_TransactionMode.UPDATE]
                    intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.UPDATE.ToString(),
                                                        Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        cmbQuarter.Text,
                                                        strFormNo,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        out strQuarter,
                                                        out strCompanyName
                                                       );
                }
                else
                {
                    //Inserting the Form BookMarkDetail [T_TransactionMode.INSERT]
                    intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.INSERT.ToString(),
                                                        Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        cmbQuarter.Text,
                                                        strFormNo,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        out strQuarter,
                                                        out strCompanyName
                                                       );
                }
            }

            //------------------------------------------------------------
            //Added by Indrajit on 22-02-2013 to add the basic information
            //-----------------------------------------------------------
            if (lngBasicInfoID == 0)
            {
                dmlService.J_BeginTransaction();

                if (TdsMan.T_GenerateBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                        cmbQuarter.Text,
                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                        strFormNo) == true)
                {
                    strSQL = "SELECT COUNT(*) " +
                        "     FROM   TRN_BASIC_INFO " +
                        "     WHERE  ASST_ID     = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) +
                        "     AND    QTR         ='" + cmbQuarter.Text + "'" +
                        "     AND    COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) +
                        "     AND    FORM_NO     ='" + cmnService.J_ReplaceQuote(strFormNo) + "'";
                    //--
                    if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 1)
                    {
                        //cmnService.J_UserMessage("This header entry exists");
                        dmlService.J_Rollback();
                    }
                    else
                    {
                        //-----------------------------------------------
                        lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                            cmbQuarter.Text,
                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                            strFormNo);
                        //-----------------------------------------------
                        InsertCompanyBasicInfo(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), lngBasicInfoID);
                        dmlService.J_Commit();
                    }
                }
                else
                    dmlService.J_Rollback();
            }
            //-----------------------------------------------------------

            //Added by Indrajit on 22-02-2013 to incorporate Basic Info locking
            #region INCORPORATE_BASIC_NFO_LOCKING

            TDSMAN.Classes.TDSMAN.T_pBasicInfoId = lngBasicInfoID;
            //================================================
            if (TdsMan.LockBasicInfoEntry(strFormNo) == false)
            {
                BtnExit.Select();
                return;
            }

            #endregion
   
            grpSearch.Visible = false;
            grpSearchDeductee.Visible = false;
            //
            if (lblSearchMode.Text == Tabbed_Mode.Basic)
            {
                tbcCompany.TabPages.Remove(tbpBasicInformation);
                tbcCompany.TabPages.Add(tbpChallanDetails);
                //-----------------------------------------------------------
                lblMode.Text = J_Mode.Add;
                lblMode.ForeColor = Color.GreenYellow;
                lblSearchMode.Text = Tabbed_Mode.Challan;
                //--
                BackgroundColorChangeChallan(lblMode.Text);
                //---------------------------------------------
                btnChallanDeducteeEntry.Text = "Deductee Details";
                //--
                BtnSave.Enabled = true;
                BtnSave.BackColor = Color.Lavender;
                //--
                BtnCancel.Enabled = true;
                BtnCancel.BackColor = Color.Lavender;
                BtnCancel.Text = "Back to Home Screen";
                //--
                BtnSort.Enabled = true;
                BtnSort.BackColor = Color.Lavender;
                //--
                BtnSearch.Enabled = true;
                BtnSearch.BackColor = Color.Lavender;
                //--
                BtnRefresh.Enabled = true;
                BtnRefresh.BackColor = Color.Lavender;
                //--
                //BtnDelete.Enabled = false;
                //BtnDelete.BackColor = Color.Lavender;
                //--
                //BtnExit.Enabled = false;
                //BtnExit.BackColor = Color.LightGray;
                //--
                lblInterestAllocated.Visible = false;
                txtInterestAllocated.Visible = false;
                lblOthersAllocated.Visible = false;
                txtOthersAllocated.Visible = false;
                //lblRemarks.Location = new Point(14, 334);
                //txtRemarks.Location = new Point(74, 331);
                //--
                btnChallanDeducteeEntry.Visible = true;
                btnChallanDeducteeEntry.Enabled = false;
                btnChallanDeducteeEntry.BackColor = Color.LightGray;
                //---------------------------------------------
                grpMain.Enabled = false;
                //--
                ClearControls();
                //
                LoadChallanFormComponents();
                //if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                //{
                //    pnlTop.Height = 25;
                //    //
                //    pnlBottom.Location = new Point(1, 50);
                //    cmbSection.Visible = false;
                //    mskDateOfPayment.Select();
                //    //
                //    txtChequeNo.Visible = false;
                //    lblCheque.Visible = false;
                //    cmbMinorHead.Visible = true;
                //}
                //else
                //{
                //    pnlTop.Height = 65;
                //    //
                //    pnlBottom.Location = new Point(1, 67);
                //    //
                //    cmbSection.Visible = true;
                //    cmbSection.Select();
                    //
                    //txtChequeNo.Visible = true;
                    //lblCheque.Visible = true;
                    //cmbMinorHead.Visible = false;
                    //-----------
                    //-- SECTION
                    //-----------
                    strSQL = " SELECT SECTION_ID," +
                        "             SECTION_NO " +
                        "      FROM   MST_SECTION " +
                        "      WHERE  FORM_NAME = '" + strFormNo + "' " +
                        "      AND    SECTION_ID NOT IN (50, 51) " +
                        "      ORDER BY SECTION_ID";
                    if (dmlService.J_PopulateComboBox(strSQL, ref cmbSection) == false) return;
                    //-----------
                //}
                mskDateOfPayment.Text = System.DateTime.Now.ToString();
                txtBSRCode.Text = "";
                txtChequeNo.Text = "";
                //-----------------------------------------------------------
                //-- Generate Challan Srl No.
                txtChallanSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_CHALLAN", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
                //--
                if (txtDeductorType.Text.Substring(0, 1) == "A" || txtDeductorType.Text.Substring(0, 1) == "S")
                {
                    //txtTransferVoucherNo.Enabled = true;
                    //txtTransferVoucherNo.BackColor = Color.White;
                    //
                    chkBookEntry.Visible = true;
                    //lblSrlNo.Location = new Point(17, 14);
                    //txtChallanSrlNo.Location = new Point(102, 10);
                }
                else
                {
                    //txtTransferVoucherNo.Enabled = false;
                    //txtTransferVoucherNo.BackColor = Color.LightGray;
                    //
                    chkBookEntry.Visible = false;
                    //lblSrlNo.Location = new Point(85, 14);
                    //txtChallanSrlNo.Location = new Point(170, 10);
                }
                //--
                LoadChallanGrid(lngBasicInfoID, sender, e);
                //--
                //cmbSection.Select();

                ControlSummaryVisible(false, false, false, false, false, false, false);
            }
            else if (lblSearchMode.Text == Tabbed_Mode.Challan)
            {
                //cmnService.J_UserMessage("Deductee Details", MessageBoxButtons.OK);            
                tbcCompany.TabPages.Remove(tbpChallanDetails);
                tbcCompany.TabPages.Add(tbpDeducteeDetails);

                lblMode.Text = J_Mode.Add;
                lblSearchMode.Text = Tabbed_Mode.Deductee;

                BackgroundColorChangeDeductee(lblMode.Text);

                //BtnSave.Enabled = true;
                //BtnSave.BackColor = Color.Lavender;

                //BtnCancel.Enabled = true;
                //BtnCancel.BackColor = Color.Lavender;
                //BtnCancel.Text = "Back to Challan Screen";
                btnChallanDeducteeEntry.Text = "Back to Challan Screen";
                //--
                grpSearch.Visible = false;
                ClearControls();
                //-----------
                //-- SECTION
                //-----------
                strSQL = " SELECT SECTION_ID," +
                        "             SECTION_NO " +
                        "      FROM   MST_SECTION " +
                        "      WHERE  FORM_NAME   ='" + T_FormNo.F26Q + "' " +
                        "      AND    SECTION_ID <> 27" +
                        "      ORDER BY SECTION_ID";
                
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbDDSection) == false) return;
                //-----------
                mskDeducteeDate.Text = "";
                txtDeducteeRate.Text = "0.0000";
                //--
                if (ShowChallanDetailsRecord(lngChallanID) == false)
                {
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref dgcViewChallan, dsetGridClone, "CHALLAN_ID", lngChallanID);
                }
                //--
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                {
                    lblSection.Visible = true;
                    cmbDDSection.Visible = true;
                    //lblDDSectionDisplay.Visible = true;
                    lblCertificateNo.Visible = true;
                    txtCertificateNo.Visible = true;
                    chkCashBookEntry.Visible = false;
                }
                else
                {
                    lblSection.Visible = false;
                    cmbDDSection.Visible = false;
                    //lblDDSectionDisplay.Visible = false;
                    lblCertificateNo.Visible = false;
                    txtCertificateNo.Visible = false;
                    chkCashBookEntry.Visible = true;
                }
                //-- ANIK @ 2014-01-29
                if (cmbDDSection.Visible == true)
                    btnVerifyPAN.Location = new Point(14, 76);
                else if (cmbDDSection.Visible == false)
                    btnVerifyPAN.Location = new Point(79, 76);  
                //--
                txtViewDeducteeTotalTaxDeposited_TextChanged(sender, e);
                //--
                LoadDeducteeGrid(lngChallanID, sender, e);
                //  Generate Deductee Srl No.
                txtDeducteeSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_DEDUCTEE_DETAILS", "SL_NO", "CHALLAN_ID = " + lngChallanID + "") + 1);

                BtnSave.Enabled = true;
                BtnSave.BackColor = Color.Lavender;

                BtnEdit.Enabled = false;
                BtnEdit.BackColor = Color.LightGray;

                BtnDelete.Enabled = false;
                BtnDelete.BackColor = Color.LightGray;

                //BtnCancel.Location = new Point(394, 66);

                strCheckFields = "";

                txtDeducteeName.Select();

                ControlSummaryVisible(false, false, false, false, false, false, false);
            }
            else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
            {                
                tbcCompany.TabPages.Add(tbpChallanDetails);
                tbcCompany.TabPages.Remove(tbpDeducteeDetails);

                tbcCompany.SelectTab(tbpChallanDetails);
                //
                lblMode.Text = J_Mode.Add;
                lblMode.ForeColor = Color.GreenYellow;
                lblSearchMode.Text = Tabbed_Mode.Challan;
                //--
                BackgroundColorChangeChallan(lblMode.Text);
                //---------------------------------------------
                btnChallanDeducteeEntry.Text = "Deductee Details";
                //--
                BtnSave.Enabled = true;
                BtnSave.BackColor = Color.Lavender;
                //--
                BtnCancel.Enabled = true;
                BtnCancel.BackColor = Color.Lavender;
                BtnCancel.Text = "Back to Home Screen";
                //--
                BtnSort.Enabled = true;
                BtnSort.BackColor = Color.Lavender;
                //--
                BtnSearch.Enabled = true;
                BtnSearch.BackColor = Color.Lavender;
                //--
                BtnRefresh.Enabled = true;
                BtnRefresh.BackColor = Color.Lavender;
                //--
                //BtnDelete.Enabled = false;
                //BtnDelete.BackColor = Color.Lavender;
                //--
                //BtnExit.Enabled = false;
                //BtnExit.BackColor = Color.LightGray;
                //--
                lblInterestAllocated.Visible = false;
                txtInterestAllocated.Visible = false;
                lblOthersAllocated.Visible = false;
                txtOthersAllocated.Visible = false;
                //lblRemarks.Location = new Point(14, 334);
                //txtRemarks.Location = new Point(74, 331);
                //--
                btnChallanDeducteeEntry.Visible = true;
                btnChallanDeducteeEntry.Enabled = false;
                btnChallanDeducteeEntry.BackColor = Color.LightGray;
                //---------------------------------------------
                grpMain.Enabled = false;
                //--
                ClearControls();
                //
                LoadChallanFormComponents();
                //-----------
                //-- SECTION
                //-----------
                strSQL = " SELECT SECTION_ID," +
                    "             SECTION_NO " +
                    "      FROM   MST_SECTION " +
                    "      WHERE  FORM_NAME = '" + strFormNo + "' " +
                    "      AND    SECTION_ID NOT IN (50, 51) " +
                    "      ORDER BY SECTION_ID";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbSection) == false) return;
                //-----------
                mskDateOfPayment.Text = System.DateTime.Now.ToString();
                txtBSRCode.Text = "";
                txtChequeNo.Text = "";
                //-----------------------------------------------------------
                //-- Generate Challan Srl No.
                txtChallanSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_CHALLAN", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
                //--
                if (txtDeductorType.Text.Substring(0, 1) == "A" || txtDeductorType.Text.Substring(0, 1) == "S")
                {
                    //txtTransferVoucherNo.Enabled = true;
                    //txtTransferVoucherNo.BackColor = Color.White;
                    //
                    chkBookEntry.Visible = true;
                    //lblSrlNo.Location = new Point(17, 14);
                    //txtChallanSrlNo.Location = new Point(102, 10);
                }
                else
                {
                    //txtTransferVoucherNo.Enabled = false;
                    //txtTransferVoucherNo.BackColor = Color.LightGray;
                    //
                    chkBookEntry.Visible = false;
                    //lblSrlNo.Location = new Point(85, 14);
                    //txtChallanSrlNo.Location = new Point(170, 10);
                }
                //--
                LoadChallanGrid(lngBasicInfoID, sender, e);
                //--
                cmbSection.Select();
                //--
                lblMsgDisplay.Visible = false;
                //--
                ControlSummaryVisible(false, false, false, false, false, false, false);
            }
        }
        #endregion

        #region dgcViewDeductee_Click
        private void dgcViewDeductee_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(Convert.ToString(dgcViewDeductee.CurrentRowIndex)) < 0)
            {
                txtDeducteeName.Focus();
                return;
            }
            
            if (lblMode.Text != J_Mode.Edit)
                lngDeducteeID = Convert.ToInt64(Convert.ToString(dgcViewDeductee[dgcViewDeductee.CurrentRowIndex, 0]));
            //
            if (lngDeducteeID > 0 && lblMode.Text == J_Mode.Add)
            {
                BtnEdit.Enabled = true;
                BtnEdit.BackColor = Color.Lavender;

                BtnDelete.Enabled = true;
                BtnDelete.BackColor = Color.Lavender;
            }

            //----------------------------------------------------
            dgcViewDeductee.Select(dgcViewDeductee.CurrentRowIndex);
            dgcViewDeductee.Select();
            dgcViewDeductee.Focus();
        }
        #endregion

        #region dgcViewDeductee_DoubleClick
        private void dgcViewDeductee_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgcViewDeductee.CurrentRowIndex >= 0)
                {
                    lblMode.Text = J_Mode.Edit;
                    //--------------------------------------------------
                    ClearControls();
                    //--------------------------------------------------
                    //A particular ID wise retriving the data from database
                    if (ShowDeducteeRecord(Convert.ToInt64(Convert.ToString(dgcViewDeductee[dgcViewDeductee.CurrentRowIndex, 0]))) == false)
                    {
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref dgcViewDeductee, dsetGridClone, "DEDUCTEE_DETAIL_ID", lngDeducteeID);
                    }
                    //--------------------------------------------------
                    cmnService.J_StatusButton(this, lblMode.Text);

                    BackgroundColorChangeDeductee(lblMode.Text);
                    //--
                    BtnSort.Enabled = true;
                    BtnSort.BackColor = Color.Lavender;

                    //ENABLE DEDUCTED DATE FIELD
                    lblDeductedDate.Visible = true;
                    mskDeductedDate.Visible = true;
                    lblDeductedDateFormat.Visible = true;
                    //--
                    BtnExit.Enabled = true;
                    BtnExit.BackColor = Color.Lavender;
                    //--------------------------------------------------
                    strCheckFields = "";
                    //--------------------------------------------------
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref dgcViewDeductee, dsetGridClone, "DEDUCTEE_DETAIL_ID", lngDeducteeID);
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region dgcViewDeductee_KeyDown
        private void dgcViewDeductee_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgcViewDeductee.CurrentRowIndex == -1) return;
                lngDeducteeID = Convert.ToInt64(Convert.ToString(dgcViewDeductee[dgcViewDeductee.CurrentRowIndex, 0]));
                if (e.KeyCode == Keys.Enter) dgcViewDeductee_DoubleClick(sender, e);
                if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);

                //strTempMode = lblMode.Text;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region dgcViewDeductee_CurrentCellChanged
        private void dgcViewDeductee_CurrentCellChanged(object sender, EventArgs e)
        {
            if (lblMode.Text != J_Mode.Edit)
                lngDeducteeID = Convert.ToInt64(Convert.ToString(dgcViewDeductee[dgcViewDeductee.CurrentRowIndex, 0]));
        }
        #endregion

        #region dgcViewDeductee_MouseClick
        private void dgcViewDeductee_MouseClick(object sender, MouseEventArgs e)
        {
            dgcViewDeductee_Click(sender, e);
        }
        #endregion

        #region dgcViewDeductee_MouseMove
        private void dgcViewDeductee_MouseMove(object sender, MouseEventArgs e)
        {
            cmnService.J_GridToolTip(dgcViewDeductee, e.X, e.Y);
        }
        #endregion


        #region txtDeducteeSrlNo_KeyPress
        private void txtDeducteeSrlNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion
        


        #region txtDeducteeName_TextChanged
        private void txtDeducteeName_TextChanged(object sender, EventArgs e)
        {
            IDataReader drdShowDeducteeHelp = null;
            //--
            try
            {
                if (txtDeducteeName.Text.Trim() == "")
                {
                    lstDeducteeHelp.Visible = false;
                    return;
                }

                if(blnShowHelp == false)
                    return;
                //-----------------------
                strSQLShowHelpDeductee = "SELECT DEDUCTEE_ID," +
                    "                            DEDUCTEE_NAME," +
                    "                            DEDUCTEE_PAN," +
                    "                            DEDUCTEE_CODE " +
                    "                     FROM   MST_DEDUCTEE " +
                    "                     WHERE  DEDUCTEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "%' " +
                    "                     ORDER BY DEDUCTEE_NAME, DEDUCTEE_ID";

                drdShowDeducteeHelp = dmlService.J_ExecSqlReturnReader(strSQLShowHelpDeductee);
                //--
                if (drdShowDeducteeHelp == null)
                {
                    lstDeducteeHelp.Visible = false;
                    drdShowDeducteeHelp.Close();
                    drdShowDeducteeHelp.Dispose();
                    return;
                }
                else
                {
                    lstDeducteeHelp.Items.Clear();
                    lstDeducteeHelp.Height = 19;
                    lstDeducteeHelp.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["DEDUCTEE_NAME"].ToString().PadRight(58) + drdShowDeducteeHelp["DEDUCTEE_PAN"].ToString().PadRight(13) + drdShowDeducteeHelp["DEDUCTEE_CODE"]));
                        //--
                        if (lstDeducteeHelp.Height <= 300)
                            lstDeducteeHelp.Height = lstDeducteeHelp.Height + 19;
                    }
                    //--
                    if (lstDeducteeHelp.Items.Count <= 0)
                        lstDeducteeHelp.Visible = false;
                }
                //-----------------------------------------------------------
                drdShowDeducteeHelp.Close();
                drdShowDeducteeHelp.Dispose();
                //-----------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                drdShowDeducteeHelp.Close();
                drdShowDeducteeHelp.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
            }
            //-----------------------
        }
        #endregion

        #region txtDeducteeName_KeyPress
        private void txtDeducteeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                if (lstDeducteeHelp.Visible == true)
                {
                    lstDeducteeHelp.Focus();
                    lstDeducteeHelp.SelectedIndex = 0;
                }
                else
                    SendKeys.Send("{tab}");
            }
        }
        #endregion

        #region txtDeducteeName_KeyDown
        private void txtDeducteeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (lstDeducteeHelp.Visible == true)
                {
                    lstDeducteeHelp.Focus();
                    lstDeducteeHelp.SelectedIndex = 0;
                }
            }
        }
        #endregion


        #region txtDeducteeName_Leave
        private void txtDeducteeName_Leave(object sender, EventArgs e)
        {
            //if (lstDeducteeHelp.Visible == true) lstDeducteeHelp.Visible = false;
        }
        #endregion

        #region txtDeducteeName_MouseMove
        private void txtDeducteeName_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(txtDeducteeName, txtDeducteeName.Text);
        }
        #endregion


        #region txtDeducteePAN_Enter
        private void txtDeducteePAN_Enter(object sender, EventArgs e)
        {
            try
            {
                lstDeducteeHelp.Visible = false;
                // TO CHECK THAT DEDUCTEE EXISTS OF THAT SAME VALID PAN
                //if (lstDeducteeHelp.Visible == true) return;
                if (txtDeducteePAN.Text.Length != 10)
                {
                    lstPANHelp.Visible = false;
                    return;
                }
                if (txtDeducteePAN.Text == "PANNOTAVBL") return;
                if (blnShowPANHelp == false) return;
                //--
                //-----------------------
                strSQLShowHelpDeductee = "SELECT DEDUCTEE_ID," +
                    "                            DEDUCTEE_NAME " +
                    "                     FROM   MST_DEDUCTEE " +
                    "                     WHERE  DEDUCTEE_PAN   = '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "' " +
                    "                     AND    DEDUCTEE_NAME <> '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "' " +
                    "                     ORDER BY DEDUCTEE_NAME";

                drdShowDeducteePAN = dmlService.J_ExecSqlReturnReader(strSQLShowHelpDeductee);
                //--
                if (drdShowDeducteePAN == null)
                {
                    lstPANHelp.Visible = false;
                    drdShowDeducteePAN.Close();
                    drdShowDeducteePAN.Dispose();
                    return;
                }
                else
                {
                    lstPANHelp.Items.Clear();
                    lstPANHelp.Height = 20;
                    lstPANHelp.Visible = true;
                    lstPANHelp.Items.Add(new ListBoxItem("Deductees exist with same PAN - "));
                    while (drdShowDeducteePAN.Read())
                    {
                        lstPANHelp.Items.Add(new ListBoxItem(drdShowDeducteePAN["DEDUCTEE_NAME"].ToString()));
                        //--
                        if (lstPANHelp.Height <= 260)
                            lstPANHelp.Height = lstPANHelp.Height + 19;
                    }
                    //--
                    drdShowDeducteePAN.Close();
                    drdShowDeducteePAN.Dispose();
                    //--
                    if (lstPANHelp.Items.Count <= 1)
                        lstPANHelp.Visible = false;
                }
            }
            catch (Exception err_handler)
            {
                drdShowDeducteePAN.Close();
                drdShowDeducteePAN.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
            }
            //-----------------------
        }
        #endregion

        #region txtDeducteePAN_KeyPress
        private void txtDeducteePAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                cmbDeducteeCode.Select();
            else
                if (TdsMan.gTANNoPANNoValidation(txtDeducteePAN, e, T_TANPAN.PAN) == false)
                    e.Handled = true;
        }
        #endregion

        #region txtDeducteePAN_Leave
        private void txtDeducteePAN_Leave(object sender, EventArgs e)
        {
            if (txtDeducteePAN.Text.Trim() == "") txtDeducteePAN.Text = "PANNOTAVBL";
            lstPANHelp.Visible = false;
            //if (txtDeducteePAN.Text == "PANNOTAVBL") 
            //    grpRemarks.Enabled = false;
            //else
            //    grpRemarks.Enabled = true;
        }
        #endregion

        #region txtDeducteePAN_TextChanged
        private void txtDeducteePAN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //--------------------------------------------------------------------
                //ADDED BY DHRUB ON 17/01/2014 FOR TDS RATE COMPANY & NONCOMPANY WISE 
                //--------------------------------------------------------------------
                ShowRate();
                //--------------------------------------------------------------------
                if (txtDeducteePAN.Text == "PANNOTAVBL" && Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2010_11ID)
                {
                    //grpRemarks.Enabled = false;
                    //rbnHigherRate.Checked = true;
                    cmbRemarks.SelectedIndex = 4;
                    lblMsgDisplay.Visible = true;
                    lblMsgDisplay.Text = "Higher rate of TDS applicable as PANNOTAVBL.";
                }
                else
                {
                    //grpRemarks.Enabled = true;
                    //rbnHigherRate.Checked = false;
                    //rbnNormal.Checked = true;
                    cmbRemarks.SelectedIndex = 1;

                    lblMsgDisplay.Visible = false;
                    lblMsgDisplay.Text = "";

                    // TO CHECK THAT DEDUCTEE EXISTS OF THAT SAME VALID PAN
                    if (lstDeducteeHelp.Visible == true) return;
                    if (txtDeducteePAN.Text.Length != 10)
                    {
                        lstPANHelp.Visible = false;
                        return;
                    }
                    if (blnShowPANHelp == false) return;
                    //--
                    //-----------------------
                    strSQLShowHelpDeductee = "SELECT DEDUCTEE_ID," +
                        "                            DEDUCTEE_NAME " +
                        "                     FROM   MST_DEDUCTEE " +
                        "                     WHERE  DEDUCTEE_PAN   = '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "' " +
                        "                     AND    DEDUCTEE_NAME <> '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "' " +
                        "                     ORDER BY DEDUCTEE_NAME";
                    drdShowDeducteePAN = dmlService.J_ExecSqlReturnReader(dmlService.J_pCommand,strSQLShowHelpDeductee);
                    //--
                    if (drdShowDeducteePAN == null)
                    {
                        lstPANHelp.Visible = false;
                        drdShowDeducteePAN.Close();
                        drdShowDeducteePAN.Dispose();
                        return;
                    }
                    else
                    {
                        lstPANHelp.Items.Clear();
                        lstPANHelp.Height = 20;
                        lstPANHelp.Visible = true;
                        lstPANHelp.Items.Add(new ListBoxItem("Deductees exist with same PAN - "));
                        while (drdShowDeducteePAN.Read())
                        {
                            lstPANHelp.Items.Add(new ListBoxItem(drdShowDeducteePAN["DEDUCTEE_NAME"].ToString()));
                            //--
                            if (lstPANHelp.Height <= 260)
                                lstPANHelp.Height = lstPANHelp.Height + 19;
                        }
                        //--
                        drdShowDeducteePAN.Close();
                        drdShowDeducteePAN.Dispose();
                        //--
                        if (lstPANHelp.Items.Count <= 1)
                            lstPANHelp.Visible = false;
                    }
                }
            }
            catch (Exception err_handler)
            {
                drdShowDeducteePAN.Close();
                drdShowDeducteePAN.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
            }
            //-----------------------
        }
        #endregion


        #region cmbDeducteeCode_KeyPress
        private void cmbDeducteeCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region mskDeducteeDate_KeyPress
        private void mskDeducteeDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtDeducteeAmountOfPayment_KeyPress
        private void txtDeducteeAmountOfPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtDeducteeAmountOfPayment, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDeducteeAmountOfPayment_TextChanged
        private void txtDeducteeAmountOfPayment_TextChanged(object sender, EventArgs e)
        {
            CalcDeducteeTax();
        }
        #endregion        

        #region txtDeducteeRate_KeyPress
        private void txtDeducteeRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,6,4", txtDeducteeRate, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDeducteeRate_Leave
        private void txtDeducteeRate_Leave(object sender, EventArgs e)
        {
            if (txtDeducteeRate.Text == "." || txtDeducteeRate.Text == "") txtDeducteeRate.Text = "0.0000";
            txtDeducteeRate.Text = string.Format("{0:0.0000}", Convert.ToDouble(cmnService.J_NumericData(txtDeducteeRate)));
        }
        #endregion

        #region txtDeducteeTotal_TextChanged
        private void txtDeducteeTotal_TextChanged(object sender, EventArgs e)
        {
            //ADDED BY SHREY KEJRIWAL ON 26/03/2012

            //TO DISBALE THE DEDUCTED DATE WHEN TOTAL TAX DEDUCTED IS ZERO

            //CHECKING IF THE TXT DEDUCTEE TOTAL IS NUMERIC
            if (cmnService.J_IsNumeric(txtDeducteeTotal.Text) == false)
                return;

            //for TAX DEDUCTED EQUAL TO ZERO
            if (Convert.ToDouble(txtDeducteeTotal.Text) == 0)
            {
                //DISABLE DEDUCTED DATE
                mskDeductedDate.Enabled = false;
                mskDeductedDate.BackColor = Color.LightGray;

                //SET DEDUCTED DATE VALUE EQUAL TO BLANK
                mskDeductedDate.Text = "";
            }

            else
            {
                //MODIFIED BY DHRUB ON 13/01/2014 FOR mskDeducteeDate backColour
                //----------------------------------------------------------------
                if (lblMode.Text == J_Mode.Add)
                {
                    mskDeductedDate.Enabled = true;
                    mskDeductedDate.BackColor = Color.White;
                }
                else
                {
                    mskDeductedDate.Enabled = true;
                    mskDeductedDate.BackColor = Color.Honeydew;
                }

                //IF THE DEDUCTED DATE WAS ORIGINALLY BLANK 
                //THEN COPYING THE PAYMENT DATE TO DEDUCTED DATE

                if (dtService.J_IsBlankDateCheck(ref mskDeductedDate, J_ShowMessage.NO))
                    mskDeductedDate.Text = mskDeducteeDate.Text;
            }

        }
        #endregion


        #region txtDeducteeRate_TextChanged
        private void txtDeducteeRate_TextChanged(object sender, EventArgs e)
        {
            CalcDeducteeTax();
        }
        #endregion

        #region txtDeducteeIncometax_KeyPress
        private void txtDeducteeIncometax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtDeducteeIncometax, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDeducteeIncometax_TextChanged
        private void txtDeducteeIncometax_TextChanged(object sender, EventArgs e)
        {
            CalcDeducteeTotTax();
        }
        #endregion

        #region txtDeducteeSurcharge_KeyPress
        private void txtDeducteeSurcharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtDeducteeSurcharge, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDeducteeSurcharge_TextChanged
        private void txtDeducteeSurcharge_TextChanged(object sender, EventArgs e)
        {
            CalcDeducteeTotTax();
        }
        #endregion
        
        #region txtDeducteeCess_KeyPress
        private void txtDeducteeCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtDeducteeCess, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDeducteeCess_TextChanged
        private void txtDeducteeCess_TextChanged(object sender, EventArgs e)
        {
            CalcDeducteeTotTax();
        }
        #endregion

        #region txtDeducteeTotal_KeyPress
        private void txtDeducteeTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtDeducteeTaxDeposited_KeyPress
        private void txtDeducteeTaxDeposited_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13)
            //{
            //    if (grpRemarks.Enabled == true)
            //        SendKeys.Send("{tab}");
            //    else
            //        BtnSave.Select();
            //}
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtDeducteeTaxDeposited, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDeducteeTotalTaxDeposited_KeyPress
        private void txtDeducteeTotalTaxDeposited_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region rbnLowerNoDeduction_KeyPress
        private void rbnLowerNoDeduction_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSave.Select();
        }
        #endregion

        #region rbnNoDeduction_KeyPress
        private void rbnNoDeduction_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSave.Select();
        }
        #endregion

        #region rbnSoftware_KeyPress
        private void rbnSoftware_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSave.Select();
        }
        #endregion

        #region rbnNormal_KeyPress
        private void rbnNormal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSave.Select();
        }
        #endregion

        #region rbnTransporter_KeyPress
        private void rbnTransporter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSave.Select();
        }
        #endregion

        #region rbnHigherRate_KeyPress
        private void rbnHigherRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSave.Select();
        }
        #endregion

        #region rbnThresholdLimit_KeyPress
        private void rbnThresholdLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSave.Select();
        }
        #endregion

        #region lstDeducteeHelp_Click
        private void lstDeducteeHelp_Click(object sender, EventArgs e)
        {
            if (cmnService.J_Right(lstDeducteeHelp.Text, 2) == T_DeducteeCode.Company)
                cmbDeducteeCode.Text = T_DeducteeCodeDesc.Company;
            else if (cmnService.J_Right(lstDeducteeHelp.Text, 2) == T_DeducteeCode.NonCompany)
                cmbDeducteeCode.Text = T_DeducteeCodeDesc.NonCompany;
            //cmbDeducteeCode.Text = cmnService.J_Right(lstDeducteeHelp.Text, 2);
            //txtDeducteePAN.Text = cmnService.J_Right(lstDeducteeHelp.Text, 10);
            //txtDeducteePAN.Text = cmnService.J_Mid(lstDeducteeHelp.Text, 58, 10);
            txtDeducteePAN.Text = cmnService.J_Left(cmnService.J_Right(lstDeducteeHelp.Text, 15),10);
            txtDeducteeName.Text = cmnService.J_Mid(lstDeducteeHelp.Text, 0, (lstDeducteeHelp.Text.Length - 15)).Trim();
            //--
            lstDeducteeHelp.Visible = false;
            //--
            txtDeducteePAN.Select();
        }
        #endregion

        #region lstDeducteeHelp_KeyPress
        private void lstDeducteeHelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                lstDeducteeHelp_Click(sender, e);
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstDeducteeHelp.Visible = false;
                txtDeducteeName.Select();
            }

        }
        #endregion


        #region txtViewDeducteeTotalTaxDeposited_TextChanged
        private void txtViewDeducteeTotalTaxDeposited_TextChanged(object sender, EventArgs e)
        {
            txtViewChallanDetailsDifference.Text = "0.00";
            txtViewChallanDetailsDifference.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToDouble(txtViewChallanDetailsTotalTax.Text) - Convert.ToDouble(txtViewDeducteeTotalTaxDeposited.Text)));
            //
            if (Convert.ToDouble(txtViewChallanDetailsDifference.Text) != 0)
                txtViewChallanDetailsDifference.ForeColor = Color.Red;
            else if (Convert.ToDouble(txtViewChallanDetailsDifference.Text) == 0)
                txtViewChallanDetailsDifference.ForeColor = Color.Black;
        }
        #endregion

        #region mskDeducteeDate_TextChanged
        private void mskDeducteeDate_TextChanged(object sender, EventArgs e)
        {
            //--------------------------------------------------------------------
            //ADDED BY DHRUB ON 17/01/2014 FOR TDS RATE COMPANY & NONCOMPANY WISE 
            //--------------------------------------------------------------------
            ShowRate();
            //--------------------------------------------------------------------
            //FOR ADD MODE
            //if (lblMode.Text == J_Mode.Add && Convert.ToDouble(txtDeducteeTotal.Text) != 0)
            //{
            if (Convert.ToDouble(txtDeducteeTotal.Text) != 0)
            {
                mskDeductedDate.Text = mskDeducteeDate.Text;
                //if (dtService.J_IsBlankDateCheck(ref mskDeductedDate, J_ShowMessage.NO))
                //    mskDeductedDate.Text = mskDeducteeDate.Text;
            }
        }
        #endregion

        #region cmbDDSection_SelectedIndexChanged
        private void cmbDDSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--------------------------------------------------------------------
            //ADDED BY DHRUB ON 17/01/2014 FOR TDS RATE COMPANY & NONCOMPANY WISE 
            //--------------------------------------------------------------------
            ShowRate();
            //--------------------------------------------------------------------
            if (blnSectionDDDisplay == false)
                return;

            if (cmbDDSection.SelectedIndex <= 0)
            {
                lblDDSectionDisplay.Visible = false;
                return;
            }
            //--
            lblDDSectionDisplay.Visible = true;
            lblDDSectionDisplay.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_DESCRIPTION FROM MST_SECTION WHERE SECTION_ID = " + Convert.ToInt32(Support.GetItemData(cmbDDSection, cmbDDSection.SelectedIndex)))));
            //
            #region COMMENTED
            //if (cmbDDSection.Text == "194C")
            //{
            //    //
            //    //rbnTransporter.Visible = true;
            //    //rbnNoDeduction.Visible = false;
            //    //rbnSoftware.Visible = false;
            //    //
            //    //cmbRemarks.Items.Remove(3);//--NO DEDUCTION
            //    //cmbRemarks.Items.Remove(5); //-- SOFTWARE
            //    cmbRemarks.SelectedIndex = 6; //-- TRANSPORTER
            //}
            //else if (cmbDDSection.Text == "194J")
            //{
            ////    //
            ////    rbnTransporter.Visible = false;
            ////    rbnNoDeduction.Visible = false;

            //    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2012_13ID)
            //        cmbRemarks.SelectedIndex = 5;
            //        //rbnSoftware.Visible = true;
            ////    else
            ////        rbnSoftware.Visible = false;
            //}
            //else if ((cmbDDSection.Text == "194") ||
            //        (cmbDDSection.Text == "194A") ||
            //        (cmbDDSection.Text == "194EE") ||
            //        (cmbDDSection.Text == "193"))
            //{
            ////    rbnNoDeduction.Visible = true;
            ////    rbnTransporter.Visible = false;
            ////    rbnSoftware.Visible = false;
            //    cmbRemarks.SelectedIndex = 3;
            //}
            ////else
            ////{
            ////    rbnNoDeduction.Visible = false;
            ////    rbnTransporter.Visible = false;
            ////    rbnSoftware.Visible = false;
            ////}
            //////
            ////if (txtViewChallanDetailsSection.Text == "194F")
            ////    rbnThresholdLimit.Visible = false;
            ////else
            ////    rbnThresholdLimit.Visible = true;

            ////// ONLY FOR FIANANCIAL YEAR LESS THAN 2010-11
            ////if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) < T_FinancialYearID.F2010_11ID)
            ////{
            ////    rbnTransporter.Visible = false;
            ////    rbnHigherRate.Visible = false;
            ////    rbnThresholdLimit.Visible = false;
            ////}
            #endregion
        }
        #endregion



        #region btnVerifyPAN_Click
        private void btnVerifyPAN_Click(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_GetPANforVerification = TdsMan.ValidatePAN(txtDeducteePAN.Text);
            //-------------------------------------------------------
            //ADDED BY DHRUB ON 16/01/2014 FOR PAN VERICATION LINK
            //-------------------------------------------------------
            TrnPANVerificationSummary objTrnPANVerificationSummary = new TrnPANVerificationSummary();
            objTrnPANVerificationSummary.ShowDialog();
            this.Refresh();
        }
        #endregion

        #region btnVerifyPAN_MouseMove
        private void btnVerifyPAN_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnVerifyPAN, "Verify PAN");
        }
        #endregion

        #endregion

        #region GenerateTDS

        #region btnErrorViewClose_Click
        private void btnErrorViewClose_Click(object sender, EventArgs e)
        {
            tbcCompany.TabPages.Remove(tbpGenerateTDSReturn);
            tbcCompany.TabPages.Add(tbpBasicInformation);

            btnChallanDeducteeEntry.Visible = true;
            BtnCancel.Visible = true;
            ////
            //BtnSort.Enabled = true;
            //BtnSort.BackColor = Color.Lavender;
        }
        #endregion

        #region btnGenerateTextFile_Click
        private void btnGenerateTextFile_Click(object sender, EventArgs e)
        {
            //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
            //-- CHECK FILE GENERATION BEFORE 2007-08
            if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) <= T_FinancialYearID.F2006_07ID)
            {
                cmnService.J_UserMessage("File generation for Financial Year prior to FY 2007-08 has been stopped by the department."); btnGenerateTextFile.Select();
                return;
            }
            //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
            // RESTRICTION FOR TRIAL VERSION
            if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
            {
                //strSQL = "SELECT COUNT(DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_COUNT FROM TRN_DEDUCTEE_DETAILS";
                //if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > TDSMAN.Classes.TDSMAN.T_pMaxDeducteeDetailsCount)
                //{
                //    TrnTrialMessageBox TrialMessageBox = new TrnTrialMessageBox();
                //    TrialMessageBox.StartPosition = FormStartPosition.CenterScreen;
                //    TrialMessageBox.lblMessage2.Text = "Return cannot be generated as the trial version supports only " + TDSMAN.Classes.TDSMAN.T_pMaxDeducteeDetailsCount + " nos. of deductee records in total.";
                //    TrialMessageBox.Show();
                //    return;
                //}
            }
            //
            this.Cursor = Cursors.WaitCursor;
            // Generate Text File
            txtTextFilePath.Text = CreateOutputFile_Folder(txtTAN.Text, strFormNo, cmbFinancialYear.Text, cmbQuarter.Text, txtDedEmpColName.Text, lngBasicInfoID);
            //

            //Deleting the text if found
            if (File.Exists(txtTextFilePath.Text))
                File.Delete(txtTextFilePath.Text);

            if (txtTextFilePath.Text == "")
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("Creation of Text File failed", MessageBoxIcon.Error);
                btnGenerateTextFile.Select();
                return;
            }
            if (GenerateTextFile(txtTextFilePath.Text) == false)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("Generation of Text File failed", MessageBoxIcon.Exclamation);
                btnGenerateTextFile.Select();
                return;
            }
            //
            txtInputFileName.Text = txtTextFilePath.Text;
            //
            if (TdsMan.T_InsertFileGenerationData(lngBasicInfoID, txtTextFilePath.Text, Convert.ToString(DateTime.Now),
                                                 Convert.ToInt32(txtTotalChallanRecords.Text), Convert.ToInt32(txtTotalDeducteeRecords.Text),
                                                 Convert.ToDouble(txtAmountPaid.Text),
                                                 Convert.ToDouble(txtTotalDeducteeTDS.Text)) == false)
                return;
            // 
            cmnService.J_UserMessage("Text File is successfully generated", MessageBoxIcon.Information);
            btnValidateTextFile.Select();
            //
            this.Cursor = Cursors.Default;
        }
        #endregion

        #region txtOutputFolder_MouseMove
        private void txtOutputFolder_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(txtOutputFolder, txtOutputFolder.Text);
        }
        #endregion

        #region btnOutputFolderChange_Click
        private void btnOutputFolderChange_Click(object sender, EventArgs e)
        {
            string strOutputFolderPath = cmnService.J_OpenFolderDialog();
            if (strOutputFolderPath == "")
            {
                cmnService.J_UserMessage("Select Folder to Save the Output File", MessageBoxIcon.Information);
                btnOutputFolderChange.Select();
                return;
            }

            txtOutputFolder.Text = strOutputFolderPath;
        }

        #endregion

        #region btnValidateTextFile_Click
        private void btnValidateTextFile_Click(object sender, EventArgs e)
        {
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //if (txtInputFileName.Text.Trim() == "")
            //{
            //    cmnService.J_UserMessage("No Input Text File found. Generate Text File First");
            //    btnGenerateTextFile.Select();
            //    return;
            //}
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            if (cmnService.J_IsFileExist(txtInputFileName.Text) == false)
            {
                cmnService.J_UserMessage("No Input File found. Generate Text File first", MessageBoxIcon.Information);
                btnGenerateTextFile.Select();
                return;
            }
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            if (TdsMan.T_CheckJREInstalled() == false)
            {
                if (cmnService.J_UserMessage("JAVA RUNTIME ENVIRONMENT (JRE) is not installed.. For Validation JRE is needed.. \n" +
                                             "Press <YES> to install (once only) it & then Validate again..\n\n Proceed??", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //string strJREFile = Path.Combine(Path.GetDirectoryName(txtViewFVUPath.Text), "j2re-1_4_2_02-windows-i586-p.exe");
                    string strJREFile = Path.Combine(Application.StartupPath, "j2re-1_4_2_02-windows-i586-p.exe");
                    System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                    Proc.StartInfo.FileName = strJREFile;
                    Proc.Start();
                }
                return;
            }
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            //Commented by Shrey Kejriwal on 04/10/2012
            //if (chkCSIFileDownload.Checked == true)
            //{
            //    // INTERNET Connectivity
            //    if (TdsMan.T_CheckInternetConnectivty() == false)
            //    {
            //        cmnService.J_UserMessage("No INTERNET Connectivity found. Uncheck the Automatic CSI file download");
            //        chkCSIFileDownload.Select();
            //        return;
            //    }
            //}
            //
            this.Cursor = Cursors.WaitCursor;
            //
            CleanOutputFolder(lngBasicInfoID);
            //
            strSQL = "UPDATE TRN_FILE_GENERATION_LOG SET " +
                     "       OUTPUT_FILE_PATH = '" + cmnService.J_ReplaceQuote(txtOutputFolder.Text) + "' " +
                     " WHERE FG_LOG_ID = (SELECT MAX(FG_LOG_ID) AS FG_LOG_ID FROM TRN_FILE_GENERATION_LOG)";
            //
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                return;
            }
            //txtViewInputFilePath.Text = CreateFile_Folder(txtViewTAN.Text, txtViewFormNo.Text, txtViewFinancialYear.Text, txtViewQuarter.Text);                
            //txtTextFilePath.Text = CreateOutputFile_Folder(txtTAN.Text, strFormNo, cmbFinancialYear.Text, cmbQuarter.Text, txtDedEmpColName.Text, lngBasicInfoID);

            // FVU FILE PATH
            string FVU_FileVersion = "";
            string FVU_Version = "";
            FVU_Version = Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, "SELECT FVU_VERSION FROM MST_ASSESSMENT WHERE ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex))));
            FVU_FileVersion = "TDS_FVU_" + FVU_Version + "\\TDS_FVU_STANDALONE.jar";

            txtFVUPath.Text = Path.Combine(Application.StartupPath, FVU_FileVersion);

            //-----------------------------------------------
            //this.Cursor = Cursors.WaitCursor;
            //// Generate
            //if (GenerateTextFile(txtTextFilePath.Text) == false)
            //{
            //    cmnService.J_UserMessage("Validation Failed");
            //    this.Cursor = Cursors.Default;
            //    return;
            //}

            //if (TdsMan.T_InsertFileGenerationData(lngBasicInfoID, txtTextFilePath.Text, Convert.ToString(DateTime.Now),
            //                                     Convert.ToInt32(txtTotalChallanRecords.Text), Convert.ToInt32(txtTotalDeducteeRecords.Text),
            //                                     Convert.ToDouble(txtAmountPaid.Text),
            //                                     Convert.ToDouble(txtTotalDeducteeTDS.Text)) == false)
            //    return;
            ////if (chkCSIFileDownload.Checked == true)
            //    DownloadCSIFile();
            this.Cursor = Cursors.WaitCursor;
            //Validate
            ValidateTextFile(FVU_Version);
            // LOOP TO WAIT FOR CREATION OF FILES BY FVU.
            //tmrShowValidationStatus.Enabled = true;
            //tmrShowValidationStatus.Start();
            //System.Threading.Thread.Sleep(5000);
            //


            //-- COPY TEXT FILE TO OUTPUT FOLDER //-- ANIK @ 2013/06/04
            if (File.Exists(Path.Combine(txtOutputFolder.Text, Path.GetFileName(txtInputFileName.Text))) == true)
                File.Copy(txtInputFileName.Text, Path.Combine(txtOutputFolder.Text, Path.GetFileName(txtInputFileName.Text)), true);
            // DISPLAY ERRORS
            EnabilityLastGenerationStatus(lngBasicInfoID, true);
            //

            //Added by Shrey Kejriwal on 03/01/2014
            //Checking if Form 27A is generated
            if (File.Exists(ReturnGeneratedForm27APath()) == true)
            {
                btnFVU27A.Enabled = true;
                btnFVU27A.BackColor = Color.Lavender;
            }
            //--
            if (cmnService.J_IsFolderExist(txtOutputFolder.Text) == true)
            {
                btnOpenOutputFolder.Enabled = true;
                btnOpenOutputFolder.BackColor = Color.Lavender;
            }
            //-----------------------------------------------
            this.Cursor = Cursors.Default;             
        }
        #endregion

        #region btnOpenOutputFolder_Click
        private void btnOpenOutputFolder_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", txtOutputFolder.Text);// cmnService.J_Mid(txtOutputFolder.Text, 0, (txtOutputFolder.Text.Length - 12)));
            }
            catch
            {
                cmnService.J_UserMessage("Output Folder not found");
            }
        }
        #endregion

        #region btnViewStatus_Click
        private void btnViewStatus_Click(object sender, EventArgs e)
        {
            int intShowMessage = 0;
            //
            //strSQL = "SELECT INPUT_FILE_PATH FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + lngBasicInfoID + " ORDER BY FG_LOG_ID DESC";
            //strSQL = "SELECT OUTPUT_FILE_PATH + '\\' + LEFT(INPUT_FILE_NAME,8) AS FILE_PATH FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + lngBasicInfoID + " ORDER BY FG_LOG_ID DESC";
            strSQL = "SELECT OUTPUT_FILE_PATH + '\\' + LEFT(INPUT_FILE_NAME,7) AS FILE_PATH FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + lngBasicInfoID + " ORDER BY FG_LOG_ID DESC";
            string strInputFilePath = Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
            //
            if (strInputFilePath != "")
            {
                intShowMessage = 1;
                // SUCCESSFULLY GENERATED WITH MISMATCH REPORT
                //if (File.Exists(cmnService.J_Mid(strInputFilePath, 0, (strInputFilePath.Length - 12))
                //        + cmnService.J_Left(cmnService.J_Right(strInputFilePath, 12), 8) + "_Electronic_Statement_Warning_File.html") == true)
                if (File.Exists(strInputFilePath + "_Electronic_Statement_Warning_File.html") == true)
                {
                    tbcCompany.TabPages.Remove(tbpBasicInformation);
                    tbcCompany.TabPages.Add(tbpGenerateTDSReturn);

                    grpViewErrors.Visible = true;

                    //wbrDisplayErrors.Navigate(cmnService.J_Mid(strInputFilePath, 0, (strInputFilePath.Length - 12))
                    //    + cmnService.J_Left(cmnService.J_Right(strInputFilePath, 12), 8) + "_Electronic_Statement_Warning_File.html");

                    wbrDisplayErrors.Navigate(strInputFilePath + "_Electronic_Statement_Warning_File.html");

                    btnChallanDeducteeEntry.Visible = false;
                    BtnCancel.Visible = false;
                    //
                    intShowMessage = intShowMessage + 1;
                }
                // ERROR HAS OCCURRED
                //if (File.Exists(cmnService.J_Mid(strInputFilePath, 0, (strInputFilePath.Length - 12))
                //        + cmnService.J_Left(cmnService.J_Right(strInputFilePath, 12), 8) + "err.html") == true)
                if (File.Exists(strInputFilePath + "err.html") == true)
                {
                    tbcCompany.TabPages.Remove(tbpBasicInformation);
                    tbcCompany.TabPages.Add(tbpGenerateTDSReturn);

                    grpViewErrors.Visible = true;

                    //wbrDisplayErrors.Navigate(cmnService.J_Mid(strInputFilePath, 0, (strInputFilePath.Length - 12))
                    //    + cmnService.J_Left(cmnService.J_Right(strInputFilePath, 12), 8) + "err.html");

                    wbrDisplayErrors.Navigate(strInputFilePath + "err.html");

                    btnChallanDeducteeEntry.Visible = false;
                    BtnCancel.Visible = false;
                    //
                    intShowMessage = intShowMessage + 1;
                }
                //
                if (intShowMessage == 1)
                    cmnService.J_UserMessage("Error File not found");                   

            }
            else
            {
                return;
            }
        }

        #endregion

        #region btnPrint27A_Click
        private void btnPrint27A_Click(object sender, EventArgs e)
        {            
            //InputBoxResult Date = "";
            //if (cmnService.J_UserMessage("Do you want to print Date ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    Date=InputBox.Show("Enter Date : ",J_Var.J_pProjectName);
            //}

            //Added by Shrey Kejriwal on 13/03/2012
            TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;

            TDSMAN.Classes.TDSMAN.T_pForm27B = 0;
            TDSMAN.Classes.TDSMAN.T_pForm27Corr = false;
            //--
            TDSMAN.Classes.TDSMAN.T_pBasicInfoId = lngBasicInfoID;
            TDSMAN.Classes.TDSMAN.T_pQuarter = cmbQuarter.Text;
            TDSMAN.Classes.TDSMAN.T_pFinancialYear = cmbFinancialYear.Text;
            TDSMAN.Classes.TDSMAN.T_pFormNo = strFormNo;
            //--
            TrnInputMessageBox InputMessageBox = new TrnInputMessageBox();
            InputMessageBox.StartPosition = FormStartPosition.CenterScreen;
            InputMessageBox.ShowDialog();
            //--
            //rptDialog.PrintForm27A(lngBasicInfoID, cmbQuarter.Text, cmbFinancialYear.Text, strFormNo);
        }

        #endregion

        #region btnPrintFVUReports_Click
        private void btnPrintFVUReports_Click(object sender, EventArgs e)
        {
            strSQL = "SELECT INPUT_FILE_PATH FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + lngBasicInfoID + " ORDER BY FG_LOG_ID DESC";
            string strInputFilePath = Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
            //
            if (strInputFilePath != "")
            {
                if (File.Exists(Path.ChangeExtension(strInputFilePath, ".html")) == true)
                    Process.Start(Path.ChangeExtension(strInputFilePath, ".html"));
                else
                    cmnService.J_UserMessage("File not Found");
            }
        }
        #endregion

        #region btnPrintErrorReport_Click
        private void btnPrintErrorReport_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------
            this.Cursor = Cursors.WaitCursor;
            //
            wbrDisplayErrors.Print();
            //
            this.Cursor = Cursors.Default;
            //-----------------------------------------------            
        }
        #endregion

        #region btnQuitGenerateReturns_Click
        private void btnQuitGenerateReturns_Click(object sender, EventArgs e)
        {
            BtnSort_Click(sender, e);
        }
        #endregion

        #region tmrShowValidationStatus_Tick
        private void tmrShowValidationStatus_Tick(object sender, EventArgs e)
        {
            //if (tmrShowValidationStatus.Interval == tmrShowValidationStatus.Tick)
            //for (tmrShowValidationStatus.Tick = 0; i <= tmrShowValidationStatus.Interval; tmrShowValidationStatus.Tick++)
            //{
            //    //Environment.TickCount
            //    i++;
            //}
                EnabilityLastGenerationStatus(lngBasicInfoID, true);
        }
        #endregion

        #endregion

        //Added by Indrajit on 22-02-2013
        #region TrnForm26Q_FormClosing
        private void TrnForm26Q_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (TdsMan.FreeBasicInfoEntry() == false)
            //    return;
        }
        #endregion

        #region tmrLoginRefresh_Tick
        private void tmrLoginRefresh_Tick(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pBasicInfoId > 0)
            //{
            //    strSQL = "UPDATE TEMP_STACK_BASIC_INFO " +
            //             "SET LAST_UPDATED_TIME   = " + TdsMan.GetServerDateTime() + " " +
            //             "WHERE BASIC_INFO_ID     = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + " " +
            //             "AND   PRINT_USER_SERIAL = '" + TDSMAN.Classes.TDSMAN.T_pProductSerial + "'";
            //    dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            //}
        }
        #endregion

        //-- ANIK @ 2013/09/27 for FVU 4.0
        #region cmbRegularStatement_SelectedIndexChanged
        private void cmbRegularStatement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnRegularStatemnt == false)
                return;
            //--
            if (cmbRegularStatement.Text == T_YES_NO.YES)
            {
                txtPrevTokenNo.Enabled = true;
                txtPrevTokenNo_Leave(sender, e);
            }
            else if (cmbRegularStatement.Text == T_YES_NO.NO)
            {
                if (cmnService.J_UserMessage("Kindly confirm that there is no statement filed for Form 26Q earlier,\n if found, the statement will be rejected at TIN central system.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtPrevTokenNo.Text = "";
                    txtPrevTokenNo.Enabled = false;
                    txtPrevTokenNo_Leave(sender, e);
                }
                else
                {
                    txtPrevTokenNo_Leave(sender, e);
                    cmbRegularStatement.Text = T_YES_NO.YES;
                }
            }
            //--
            //txtPrevTokenNo_Leave(sender, e);
        }
        #endregion

        #region cmbMinorHead_KeyPress
        private void cmbMinorHead_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) //SendKeys.Send("{tab}");
            {
                if (lblMode.Text == J_Mode.Edit)
                    txtRemarks.Focus();
                else
                    SendKeys.Send("{tab}");
            }
        }
        #endregion 

        #region txtPrevTokenNo_Leave
        private void txtPrevTokenNo_Leave(object sender, EventArgs e)
        {
            
            #region GET BASIC INFO
            //-----------------------------------------------------------
            if (lngBasicInfoID == 0)
            {
                dmlService.J_BeginTransaction();

                if (TdsMan.T_GenerateBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                        cmbQuarter.Text,
                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                        strFormNo) == true)
                {
                    strSQL = "SELECT COUNT(*) " +
                        "     FROM   TRN_BASIC_INFO " +
                        "     WHERE  ASST_ID     = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) +
                        "     AND    QTR         ='" + cmbQuarter.Text + "'" +
                        "     AND    COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) +
                        "     AND    FORM_NO     ='" + cmnService.J_ReplaceQuote(strFormNo) + "'";
                    //--
                    if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 1)
                    {
                        //cmnService.J_UserMessage("This header entry exists");
                        dmlService.J_Rollback();
                    }
                    else
                    {
                        //-----------------------------------------------
                        lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                            cmbQuarter.Text,
                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                            strFormNo);
                        //-----------------------------------------------
                        InsertCompanyBasicInfo(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), lngBasicInfoID);
                        dmlService.J_Commit();
                    }
                }
                else
                    dmlService.J_Rollback();
            }
            //-----------------------------------------------------------
            #endregion
            //-- UPDATE TRN_BASIC_INFO AS PER ENTRY
            int RegularStatement = 1;
            if (cmbRegularStatement.Text == T_YES_NO.YES)
                RegularStatement = 0;
            //
            TdsMan.SaveLastTokenNumber(lngBasicInfoID, RegularStatement, txtPrevTokenNo.Text.Trim());
            //
        }
        #endregion

        #region txtPrevTokenNo_KeyPress
        private void txtPrevTokenNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,15,0", txtPrevTokenNo, "") == false)
                e.Handled = true;
        }
        #endregion

        #endregion

        #region User Define Functions

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)SendKeys.Send("{tab}");
        }
        #endregion

        #region Searching_KeyPress
        private void Searching_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region Sorting_KeyPress
        private void Sorting_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) BtnSortOK_Click(sender, e);
            //if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        #region NumericControl_KeyPress
        private void NumericControl_KeyPress(object sender, KeyPressEventArgs e, int MaxLength)
        {
            TextBox txtNumeric = (TextBox)sender;
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N," + MaxLength + ",0", txtNumeric, "") == false)
                e.Handled = true;
        }
        #endregion

        #region NumericCurrencyControl_Leave
        private void NumericCurrencyControl_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "." || txtBox.Text == "") txtBox.Text = "0.00";
            txtBox.Text = string.Format("{0:0.00}", Convert.ToDouble(cmnService.J_NumericData(txtBox)));
        }
        #endregion

        #region NumericControl_Leave
        private void NumericControl_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "") txtBox.Text = "0";
        }
        #endregion        

        #region chkChange_CheckedChanged
        private void chkChange_CheckedChanged(object sender, EventArgs e)
        {
            if (tbcCompany.Enabled == true)
            {
                if (blnChkChanged == true)
                {
                    // GETTING BASIC INFO ID
                    if (lngBasicInfoID == 0)
                    {
                        if (TdsMan.T_GenerateBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                            cmbQuarter.Text,
                                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                            strFormNo) == true)
                        {
                            //-----------------------------------------------
                            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                cmbQuarter.Text,
                                                Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                strFormNo);
                            //-----------------------------------------------
                            InsertCompanyBasicInfo(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), lngBasicInfoID);
                        }
                    }
                    // COMPANY ADDRESS CHANGE
                    if (chkAddressChange.Checked == true)
                    {
                        strSQL = "UPDATE TRN_BASIC_INFO SET ADDRESS_CHANGE = 1 WHERE BASIC_INFO_ID = " + lngBasicInfoID + " ";
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return;
                    }
                    else if (chkAddressChange.Checked == false)
                    {
                        strSQL = "UPDATE TRN_BASIC_INFO SET ADDRESS_CHANGE = 0 WHERE BASIC_INFO_ID = " + lngBasicInfoID + " ";
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return;
                    }
                    // COMPANY RESPONSIBLE PERSON ADDRESS CHANGE
                    if (chkRPAddressChange.Checked == true)
                    {
                        strSQL = "UPDATE TRN_BASIC_INFO SET P_ADDRESS_CHANGE = 1 WHERE BASIC_INFO_ID = " + lngBasicInfoID + " ";
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return;
                    }
                    else if (chkRPAddressChange.Checked == false)
                    {
                        strSQL = "UPDATE TRN_BASIC_INFO SET P_ADDRESS_CHANGE = 0 WHERE BASIC_INFO_ID = " + lngBasicInfoID + " ";
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return;
                    }
                }
            }

        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            if (lblSearchMode.Text == Tabbed_Mode.Basic)
            {
                //--------------------------------------
                //--------------------------------------
                //--------------------------------------
                //-- BASIC DETAILS
                //tbcCompany.TabPages.Add(tbpBasicInformation);
                tbcCompany.TabPages.Remove(tbpChallanDetails);
                tbcCompany.TabPages.Remove(tbpDeducteeDetails);
                tbcCompany.TabPages.Remove(tbpGenerateTDSReturn);
                //--------------------------------------
                txtAssessmentYear.Text = "";
                txtResponsiblePersonName.Text = "";
                txtTAN.Text = "";
                txtPAN.Text = "";
                txtType.Text = "";
                //--------------------------------------
                lblDedEmpColName.Text = "";
                txtTANNo.Text = "";
                txtPANNo.Text = "";
                txtBranch.Text = "";
                txtDeductorType.Text = "";
                //-----------
                txtCompanyPrefix.Text = "";
                //--------------------------------------
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtAddress3.Text = "";
                txtAddress4.Text = "";
                txtAddress5.Text = "";
                //-----------
                //-- STATE
                //-----------
                strSQL = " SELECT STATE_ID," +
                    "             STATE_NAME " +
                    "      FROM   MST_STATE " +
                    "      ORDER BY STATE_NAME ";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbState) == false) return;
                //-----------
                txtPIN.Text = "";
                txtSTD.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";

                chkAddressChange.Checked = false;
                chkRPAddressChange.Checked = false;

                //---------------------------------------
                txtRPName.Text = "";
                txtRPDesignation.Text = "";
                txtRPFatherName.Text = "";
                txtRPAddress1.Text = "";
                txtRPAddress2.Text = "";
                txtRPAddress3.Text = "";
                txtRPAddress4.Text = "";
                txtRPAddress5.Text = "";
                //-----------
                //-- RP STATE
                //-----------
                strSQL = " SELECT STATE_ID," +
                    "             STATE_NAME " +
                    "      FROM   MST_STATE " +
                    "      ORDER BY STATE_NAME ";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbRPState) == false) return;
                //-----------
                txtRPPIN.Text = "";
                txtRPSTD.Text = "";
                txtRPPhone.Text = "";
                txtRPMobileNo.Text = "";
                txtRPEmail.Text = "";
                //----------------------------------------
                txtPAOCode.Text = "";
                txtPAORegNo.Text = "";
                txtDDOCode.Text = "";
                txtDDORegNo.Text = "";
                //-----------
                //-- Govt Deductors STATE
                //-----------
                strSQL = " SELECT STATE_ID," +
                    "             STATE_NAME " +
                    "      FROM   MST_STATE " +
                    "      ORDER BY STATE_NAME ";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbGovtDedState) == false) return;
                //-----------
                //-- Ministry
                //-----------
                strSQL = " SELECT MINISTRY_ID," +
                    "             MINISTRY_NAME " +
                    "      FROM   MST_MINISTRY " +
                    "      ORDER BY MINISTRY_NAME ";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbMinistry) == false) return;
                //-----------
                txtOtherMinistry.Text = "";
                //--
                blnRegularStatemnt = false;
                txtPrevTokenNo.Text = "";
                //
                cmbRegularStatement.Items.Clear();
                cmbRegularStatement.Items.Add(T_YES_NO.YES);
                cmbRegularStatement.Items.Add(T_YES_NO.NO);
                cmbRegularStatement.SelectedIndex = 0;
                blnRegularStatemnt = true;
                //
                //--
            }
            else if (lblSearchMode.Text == Tabbed_Mode.Challan)
            {
                //--------------------------------------
                //--------------------------------------
                //--------------------------------------
                ////-- CHALLAN DETAILS
                //--------------------------------------
                ////txtChallanSrlNo.Text = "";
                ////-----------
                ////-- SECTION
                ////-----------
                //strSQL = " SELECT SECTION_ID," +
                //    "             SECTION_NO " +
                //    "      FROM   MST_SECTION " +
                //    "      WHERE  FORM_NAME = '26Q' " +
                //    "      ORDER BY SECTION_ID";
                //if (dmlService.J_PopulateComboBox(strSQL, ref cmbSection) == false) return;
                ////-----------
                //mskDateOfPayment.Text = "";
                txtChallanNo.Text = "";
                txtTransferVoucherNo.Text = "";
                //chkBookEntry.Checked = true
                //txtBSRCode.Text = "";
                //txtChequeNo.Text = "";
                txtTDS.Text = "0.00";
                txtSurcharge.Text = "0.00";
                txtEducationCess.Text = "0.00";
                txtInterests.Text = "0.00";
                txtOthers.Text = "0.00";
                txtTotalTax.Text = "0.00";
                txtInterestAllocated.Text = "0.00";
                txtOthersAllocated.Text = "0.00";
                txtRemarks.Text = "";
                //--
                if (txtDeductorType.Text.Substring(0, 1) == "A" || txtDeductorType.Text.Substring(0, 1) == "S")
                {
                    txtTransferVoucherNo.Enabled = true;
                    txtTransferVoucherNo.BackColor = Color.White;
                }
                else
                {
                    txtTransferVoucherNo.Enabled = false;
                    txtTransferVoucherNo.BackColor = Color.LightGray;
                }
                txtFee.Text = "0.00";
                strSQL = " SELECT MINOR_HEAD_ID," +
                    "             MINOR_HEAD_CODE + '-' + MINOR_HEAD_DESC AS MINOR " +
                    "      FROM   MST_MINOR_HEAD " +
                    "      ORDER BY MINOR_HEAD_ID";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbMinorHead) == false) return;
            }
            else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
            {
                //--------------------------------------
                //--------------------------------------
                //--------------------------------------
                ////-- DEDUCTEE DETAILS                
                //tbcCompany.TabPages.Remove(tbpBasicInformation);
                //tbcCompany.TabPages.Remove(tbpChallanDetails);
                //tbcCompany.TabPages.Add(tbpDeducteeDetails);
                //tbcCompany.TabPages.Remove(tbpGenerateTDSReturn);
                //--------------------------------------
                //if (dgcViewChallanDetails.Visible == true)
                //{
                //    txtViewChallanDetailsSrlNo.Text = "";
                //    txtViewChallanDetailsSection.Text = "";
                //    mskViewChallanDetailsDate.Text = "";
                //    txtViewChallanDetailsBSRCode.Text = "";
                //    txtViewChallanDetailsChallanNo.Text = "";
                //    txtViewChallanDetailsChequeNo.Text = "";
                //    txtViewChallanDetailsTotalTax.Text = "";
                //    txtViewChallanDetailsTransferVoucherNo.Text = "";
                //}
                //--
                //txtDeducteeSrlNo.Text = "";
                txtDeducteeName.Text = "";
                txtOldDeducteeName.Text = "";
                txtDeducteePAN.Text = "";
                chkCashBookEntry.Checked = false;
                //--
                //cmbDeducteeCode.Items.Clear();
                //cmbDeducteeCode.Items.Add("");
                //cmbDeducteeCode.Items.Add("01");
                //cmbDeducteeCode.Items.Add("02");
                //cmbDeducteeCode.SelectedIndex = 0;
                string[] strDeducteeCode ={ T_DeducteeCodeDesc.Company, T_DeducteeCodeDesc.NonCompany };
                dmlService.J_PopulateComboBox(strDeducteeCode, ref cmbDeducteeCode, J_ComboBoxDefaultText.NO);
                //--
                //mskDeducteeDate.Text = "";
                txtDeducteeAmountOfPayment.Text = "0.00";
                //txtDeducteeRate.Text = "0.0000";
                txtDeducteeIncometax.Text = "0.00";
                txtOldDeducteeIncometax.Text = "0.00";
                txtDeducteeSurcharge.Text = "0.00";
                txtOldDeducteeSurcharge.Text = "0.00";
                txtDeducteeCess.Text = "0.00";
                txtOldDeducteeCess.Text = "0.00";
                txtDeducteeTotal.Text = "0.00";
                txtOldDeducteeTotal.Text = "0.00";
                txtDeducteeTaxDeposited.Text = "0.00";
                txtOldDeducteeTaxDeposited.Text = "0.00";
                //txtViewDeducteeTotalTaxDeposited.Text = "0.00";
                //rbnNormal.Checked = true;
                //
                strSQL = " SELECT REASON_ID," +
                    "             REASON + ' ' + DESCRIPTION " +
                    "      FROM   MST_REASON " +
                    "      WHERE  FORM_NO = '" +  T_FormNo.F26Q + "'" +
                    "      ORDER BY REASON";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbRemarks,  1 ) == false) return;
                //--
                txtCertificateNo.Text = "";

                //ENABLE DEDUCTED DATE FIELD
                //-------------------------------------------------------------
                //ADDED BY DHRUB FOR DEDUCTED DATE VISIBILITY FORM MAST_STEUP
                //-------------------------------------------------------------
                if (TDSMAN.Classes.TDSMAN.T_ShowDeductedDate == true)
                {
                    lblDeductedDate.Visible = true;
                    mskDeductedDate.Visible = true;
                    lblDeductedDateFormat.Visible = true;
                }
                else
                {
                    lblDeductedDate.Visible = false;
                    mskDeductedDate.Visible = false;
                    lblDeductedDateFormat.Visible = false;
                }
                //-------------------------------------------------------------
                //-------------------------------------------------------------
            }
            else if (lblSearchMode.Text == Tabbed_Mode.GenerateTDS)
            {
                //--------------------------------------
                //--------------------------------------
                //--------------------------------------
                ////-- GENERATE TDS
                //tbcCompany.TabPages.Remove(tbpBasicInformation);
                //tbcCompany.TabPages.Remove(tbpChallanDetails);
                //tbcCompany.TabPages.Remove(tbpDeducteeDetails);
                //tbcCompany.TabPages.Add(tbpGenerateTDSReturn);
                //--------------------------------------
                chkCSIFileDownload.Checked = false;
                mskViewCSIFileDownloadFrom.Text = "";
                mskViewCSIFileDownloadTo.Text = "";
                //txtViewFVUPath.Text = "";
                //txtViewInputFilePath.Text = "";

                grpViewErrors.Visible = false;
            }
        }
        #endregion
        
        #region InsertCompanyBasicInfo
        private void InsertCompanyBasicInfo(long CompanyId, long BasicInfoId)
        {
            //-----------------------------------------------------------
            strSQL = "INSERT INTO TRN_COMPANY_INFO (" +
                     "            BASIC_INFO_ID," +
                     "            COMPANY_ID," +
                     "            GROUP_ID," +
                     "            TAN_NO," +
                     "            PAN_NO," +
                     "            COMPANY_NAME," +
                     "            BRANCH_DIV," +
                     "            D_CATEGORY_ID," +
                     "            MINISTRY_ID," +
                     "            MINISTRY_OTHER," +
                     "            ADDRESS1," +
                     "            ADDRESS2," +
                     "            ADDRESS3," +
                     "            ADDRESS4," +
                     "            ADDRESS5," +
                     "            STATE_ID," +
                     "            PIN_CODE," +
                     "            STD," +
                     "            PHONE," +
                     "            EMAIL," +
                     "            PERSON_NAME," +
                     "            DESIGNATION," +
                     "            FATHER_NAME," +
                     "            P_ADDRESS1," +
                     "            P_ADDRESS2," +
                     "            P_ADDRESS3," +
                     "            P_ADDRESS4," +
                     "            P_ADDRESS5," +
                     "            P_STATE_ID," +
                     "            P_PIN_CODE," +
                     "            P_STD," +
                     "            P_PHONE," +
                     "            P_EMAIL," +
                     "            P_MOBILE," +
                     "            PAO_CODE," +
                     "            PAO_REG_NO," +
                     "            DDO_CODE," +
                     "            DDO_REG_NO," +
                     "            D_STATE_ID," +
                     "            AIN_NO," +
                     "            TAN_REG_NO," +
                     "            ALT_STD," +
                     "            ALT_PHONE," +
                     "            ALT_EMAIL," +
                     "            P_ALT_STD," +
                     "            P_ALT_PHONE," +
                     "            P_ALT_EMAIL) " +
                     "     VALUES( " + BasicInfoId + "," +
                     "             " + CompanyId + "," +
                     "             " + TDSMAN.Classes.TDSMAN.T_pGroupId + "," +
                     "            '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtPANNo.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtDedEmpColName.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtBranch.Text.Trim()) + "'," +
                     "             " + cmnService.J_ReturnInt32Value(txtDeductorTypeID.Text) + "," +
                     "             " + cmnService.J_ReturnInt32Value(txtMinistryId.Text) + "," +
                     "            '" + cmnService.J_ReplaceQuote(txtOtherMinistry.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtAddress1.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtAddress2.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtAddress3.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtAddress4.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtAddress5.Text.Trim()) + "'," +
                     "             " + Convert.ToInt32(Support.GetItemData(cmbState, cmbState.SelectedIndex)) + "," +
                     "            '" + cmnService.J_ReplaceQuote(txtPIN.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtSTD.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtPhone.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtEmail.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPName.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPDesignation.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPFatherName.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPAddress1.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPAddress2.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPAddress3.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPAddress4.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPAddress5.Text.Trim()) + "'," +
                     "             " + Convert.ToInt32(Support.GetItemData(cmbRPState, cmbRPState.SelectedIndex)) + "," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPPIN.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPSTD.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPPhone.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPEmail.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtRPMobileNo.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtPAOCode.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtPAORegNo.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtDDOCode.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtDDORegNo.Text.Trim()) + "'," +
                     "             " + cmnService.J_ReturnInt32Value(txtDeductorTypeStateID.Text) + "," +
                     "            '" + cmnService.J_ReplaceQuote(txtAIN.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtTANRegNo.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtAltSTD.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtAltPhone.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtAltEmail.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtAltRPSTD.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtAltRPPhone.Text.Trim()) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(txtAltRPEmail.Text.Trim()) + "')";
            //-----------------------------------------------------------
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                txtDedEmpColName.Select();
            }
        }
        #endregion

        #region InsertCompanyBasicInfo
        private void InsertCompanyBasicInfo(long BasicInfoId)
        {
            //-- 2013/10/07
            #region VARIABLE DECLARATION
            IDataReader drdShowRecord = null;
            string strTAN = "";
            string strPAN = "";
            string strCompanyName = "";
            string strBranchDiv = "";
            long lngCategoryID = 0;
            long lngMinistryID = 0;
            string strMinistryOther = "";
            string strAddress1 = "";
            string strAddress2 = "";
            string strAddress3 = "";
            string strAddress4 = "";
            string strAddress5 = "";
            long lngStateID = 0;
            string strPIN = "";
            string strSTD = "";
            string strPhone = "";
            string strEmail = "";
            string strPersonName = "";
            string strDesignation = "";
            string strFatherName = "";
            string strPAddress1 = "";
            string strPAddress2 = "";
            string strPAddress3 = "";
            string strPAddress4 = "";
            string strPAddress5 = "";
            long lngPStateID = 0;
            string strPPIN = "";
            string strPSTD = "";
            string strPPhone = "";
            string strPEmail = "";
            string strPMobile = "";
            string strPAOCode = "";
            string strPAORegNo = "";
            string strDDOCode = "";
            string strDDORegNo = "";
            long lngDStateID = 0;
            #endregion
            //--
            #region SELECT MST_COMPANY
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
               "             MST_COMPANY.CIT_TDS_PINCODE       AS CIT_TDS_PINCODE " +
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
               "     WHERE   MST_COMPANY.COMPANY_ID      = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

            drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
            if (drdShowRecord == null)
                return;
            //
            while (drdShowRecord.Read())
            {
                strTAN = Convert.ToString(drdShowRecord["TAN_NO"]);
                strPAN = Convert.ToString(drdShowRecord["PAN_NO"]);
                strCompanyName = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                strBranchDiv = Convert.ToString(drdShowRecord["BRANCH_DIV"]);
                lngCategoryID = cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["D_CATEGORY_ID"]));
                lngMinistryID = cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["MINISTRY_ID"]));
                strMinistryOther = Convert.ToString(drdShowRecord["MINISTRY_OTHER"]);
                strAddress1 = Convert.ToString(drdShowRecord["ADDRESS1"]);
                strAddress2 = Convert.ToString(drdShowRecord["ADDRESS2"]);
                strAddress3 = Convert.ToString(drdShowRecord["ADDRESS3"]);
                strAddress4 = Convert.ToString(drdShowRecord["ADDRESS4"]);
                strAddress5 = Convert.ToString(drdShowRecord["ADDRESS5"]);
                lngStateID = cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["STATE_ID"]));
                strPIN = Convert.ToString(drdShowRecord["PIN_CODE"]);
                strSTD = Convert.ToString(drdShowRecord["STD"]);
                strPhone = Convert.ToString(drdShowRecord["PHONE"]);
                strEmail = Convert.ToString(drdShowRecord["EMAIL"]);
                strPersonName = Convert.ToString(drdShowRecord["PERSON_NAME"]);
                strDesignation = Convert.ToString(drdShowRecord["DESIGNATION"]);
                strFatherName = Convert.ToString(drdShowRecord["FATHER_NAME"]);
                strPAddress1 = Convert.ToString(drdShowRecord["P_ADDRESS1"]);
                strPAddress2 = Convert.ToString(drdShowRecord["P_ADDRESS2"]);
                strPAddress3 = Convert.ToString(drdShowRecord["P_ADDRESS3"]);
                strPAddress4 = Convert.ToString(drdShowRecord["P_ADDRESS4"]);
                strPAddress5 = Convert.ToString(drdShowRecord["P_ADDRESS5"]);
                lngPStateID = cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["P_STATE_ID"]));
                strPPIN = Convert.ToString(drdShowRecord["P_PIN_CODE"]);
                strPSTD = Convert.ToString(drdShowRecord["P_STD"]);
                strPPhone = Convert.ToString(drdShowRecord["P_PHONE"]);
                strPEmail = Convert.ToString(drdShowRecord["P_EMAIL"]);
                strPMobile = Convert.ToString(drdShowRecord["P_MOBILE"]);
                strPAOCode = Convert.ToString(drdShowRecord["PAO_CODE"]);
                strPAORegNo = Convert.ToString(drdShowRecord["PAO_REG_NO"]);
                strDDOCode = Convert.ToString(drdShowRecord["DDO_CODE"]);
                strDDORegNo = Convert.ToString(drdShowRecord["DDO_REG_NO"]);
                lngDStateID = cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["D_STATE_ID"]));

            }
            drdShowRecord.Close();
            drdShowRecord.Dispose();
            #endregion
            //--
            #region INSERT INTO TRN_COMPANY_INFO
            strSQL = "INSERT INTO TRN_COMPANY_INFO (" +
                     "            BASIC_INFO_ID," +
                     "            COMPANY_ID," +
                     "            GROUP_ID," +
                     "            TAN_NO," +
                     "            PAN_NO," +
                     "            COMPANY_NAME," +
                     "            BRANCH_DIV," +
                     "            D_CATEGORY_ID," +
                     "            MINISTRY_ID," +
                     "            MINISTRY_OTHER," +
                     "            ADDRESS1," +
                     "            ADDRESS2," +
                     "            ADDRESS3," +
                     "            ADDRESS4," +
                     "            ADDRESS5," +
                     "            STATE_ID," +
                     "            PIN_CODE," +
                     "            STD," +
                     "            PHONE," +
                     "            EMAIL," +
                     "            PERSON_NAME," +
                     "            DESIGNATION," +
                     "            FATHER_NAME," +
                     "            P_ADDRESS1," +
                     "            P_ADDRESS2," +
                     "            P_ADDRESS3," +
                     "            P_ADDRESS4," +
                     "            P_ADDRESS5," +
                     "            P_STATE_ID," +
                     "            P_PIN_CODE," +
                     "            P_STD," +
                     "            P_PHONE," +
                     "            P_EMAIL," +
                     "            P_MOBILE," +
                     "            PAO_CODE," +
                     "            PAO_REG_NO," +
                     "            DDO_CODE," +
                     "            DDO_REG_NO," +
                     "            D_STATE_ID) " +
                     "     VALUES( " + BasicInfoId + "," +
                     "             " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + "," +
                     "             " + TDSMAN.Classes.TDSMAN.T_pGroupId + "," +
                     "            '" + cmnService.J_ReplaceQuote(strTAN) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAN) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strCompanyName) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strBranchDiv) + "'," +
                     "             " + lngCategoryID + "," +
                     "             " + lngMinistryID + "," +
                     "            '" + cmnService.J_ReplaceQuote(strMinistryOther) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAddress1) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAddress2) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAddress3) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAddress4) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAddress5) + "'," +
                     "             " + lngStateID + "," +
                     "            '" + cmnService.J_ReplaceQuote(strPIN) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strSTD) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPhone) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strEmail) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPersonName) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strDesignation) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strFatherName) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAddress1) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAddress2) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAddress3) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAddress4) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAddress5) + "'," +
                     "             " + lngPStateID + "," +
                     "            '" + cmnService.J_ReplaceQuote(strPPIN) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPSTD) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPPhone) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPEmail) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPMobile) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAOCode) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAORegNo) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strDDOCode) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strDDORegNo) + "'," +
                     "             " + lngDStateID + ")";
            //-----------------------------------------------------------
            if (dmlService.J_ExecSql(strSQL) == false)
                return;
            //
            #endregion
            //--
            return;
            //-----------------------------------------------------------
        }
        #endregion

        #region LoadBasicDetails
        private bool LoadBasicDetails(long Company,long BasicInfoId)
        {
            //-----------------------------------------------
            IDataReader drdShowRecord = null;
            string strCompanyTableName = "";
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            try
            {
                if (BasicInfoId == 0)
                    strCompanyTableName = "MST_COMPANY";
                else
                {
                    strCompanyTableName = "TRN_COMPANY_INFO";
                    //-- 2013/10/07
                    if (dmlService.J_IsRecordExist("TRN_COMPANY_INFO", "BASIC_INFO_ID = " + BasicInfoId) == false)
                        InsertCompanyBasicInfo(BasicInfoId);
                    //--
                }
                //
                strSQL = "SELECT  " + strCompanyTableName + ".COMPANY_ID            AS COMPANY_ID," +
                    "             " + strCompanyTableName + ".COMPANY_NAME          AS COMPANY_NAME," +
                    "             " + strCompanyTableName + ".TAN_NO                AS TAN_NO," +
                    "             " + strCompanyTableName + ".PAN_NO                AS PAN_NO," +
                    "             " + strCompanyTableName + ".BRANCH_DIV            AS BRANCH_DIV," +
                    "             " + strCompanyTableName + ".D_CATEGORY_ID         AS D_CATEGORY_ID," +
                    "             MST_CATEGORY.CATEGORY_DESCRIPTION                 AS CATEGORY_DESCRIPTION," +
                    "             MST_CATEGORY.CATEGORY_CODE                        AS CATEGORY_CODE," +
                    "             " + strCompanyTableName + ".FILE_PREFIX           AS FILE_PREFIX," +
                    "             " + strCompanyTableName + ".ADDRESS1              AS ADDRESS1," +
                    "             " + strCompanyTableName + ".ADDRESS2              AS ADDRESS2," +
                    "             " + strCompanyTableName + ".ADDRESS3              AS ADDRESS3," +
                    "             " + strCompanyTableName + ".ADDRESS4              AS ADDRESS4," +
                    "             " + strCompanyTableName + ".ADDRESS5              AS ADDRESS5," +
                    "             " + strCompanyTableName + ".STATE_ID              AS STATE_ID," +
                    "             MST_STATE.STATE_CODE                              AS STATE_CODE," +
                    "             MST_STATE.STATE_NAME                              AS STATE_NAME," +
                    "             " + strCompanyTableName + ".PIN_CODE              AS PIN_CODE," +
                    "             " + strCompanyTableName + ".STD                   AS STD," +
                    "             " + strCompanyTableName + ".PHONE                 AS PHONE," +
                    "             " + strCompanyTableName + ".EMAIL                 AS EMAIL," +
                    "             " + strCompanyTableName + ".PERSON_NAME           AS PERSON_NAME," +
                    "             " + strCompanyTableName + ".DESIGNATION           AS DESIGNATION," +
                    "             " + strCompanyTableName + ".FATHER_NAME           AS FATHER_NAME," +
                    "             " + strCompanyTableName + ".P_ADDRESS1            AS P_ADDRESS1," +
                    "             " + strCompanyTableName + ".P_ADDRESS2            AS P_ADDRESS2," +
                    "             " + strCompanyTableName + ".P_ADDRESS3            AS P_ADDRESS3," +
                    "             " + strCompanyTableName + ".P_ADDRESS4            AS P_ADDRESS4," +
                    "             " + strCompanyTableName + ".P_ADDRESS5            AS P_ADDRESS5," +
                    "             " + strCompanyTableName + ".P_STATE_ID            AS P_STATE_ID," +
                    "             RP_STATE.STATE_CODE                               AS RP_STATE_CODE," +
                    "             RP_STATE.STATE_NAME                               AS RP_STATE_NAME," +
                    "             " + strCompanyTableName + ".P_PIN_CODE            AS P_PIN_CODE," +
                    "             " + strCompanyTableName + ".P_PHONE               AS P_PHONE," +
                    "             " + strCompanyTableName + ".P_STD                 AS P_STD," +
                    "             " + strCompanyTableName + ".P_EMAIL               AS P_EMAIL," +
                    "             " + strCompanyTableName + ".P_MOBILE              AS P_MOBILE," +
                    "             " + strCompanyTableName + ".PAO_CODE              AS PAO_CODE," +
                    "             " + strCompanyTableName + ".PAO_REG_NO            AS PAO_REG_NO," +
                    "             " + strCompanyTableName + ".DDO_CODE              AS DDO_CODE," +
                    "             " + strCompanyTableName + ".DDO_REG_NO            AS DDO_REG_NO," +
                    "             " + strCompanyTableName + ".D_STATE_ID            AS D_STATE_ID," +
                    "             D_STATE.STATE_CODE                                AS D_STATE_CODE," +
                    "             D_STATE.STATE_NAME                                AS D_STATE_NAME," +
                    "             " + strCompanyTableName + ".MINISTRY_ID           AS MINISTRY_ID," +
                    "             MST_MINISTRY.MINISTRY_CODE                        AS MINISTRY_CODE," +
                    "             MST_MINISTRY.MINISTRY_NAME                        AS MINISTRY_NAME," +
                    "             " + strCompanyTableName + ".MINISTRY_OTHER        AS MINISTRY_OTHER," +
                    "             " + strCompanyTableName + ".AIN_NO                AS AIN_NO," +
                    "             " + strCompanyTableName + ".TAN_REG_NO            AS TAN_REG_NO," +
                    "             " + strCompanyTableName + ".ALT_STD               AS ALT_STD," +
                    "             " + strCompanyTableName + ".ALT_PHONE             AS ALT_PHONE," +
                    "             " + strCompanyTableName + ".ALT_EMAIL             AS ALT_EMAIL," +
                    "             " + strCompanyTableName + ".P_ALT_STD             AS P_ALT_STD," +
                    "             " + strCompanyTableName + ".P_ALT_PHONE           AS P_ALT_PHONE," +
                    "             " + strCompanyTableName + ".P_ALT_EMAIL           AS P_ALT_EMAIL " +
                    "     FROM    (((((" + strCompanyTableName + " INNER JOIN MST_CATEGORY " +
                    "             ON " + strCompanyTableName + ".D_CATEGORY_ID = MST_CATEGORY.CATEGORY_ID) " +
                    "     INNER JOIN MST_STATE " +
                    "             ON " + strCompanyTableName + ".STATE_ID      = MST_STATE.STATE_ID) " +
                    "     INNER JOIN MST_STATE AS RP_STATE " +
                    "             ON " + strCompanyTableName + ".P_STATE_ID    = RP_STATE.STATE_ID) " +
                    "     LEFT JOIN  MST_STATE AS D_STATE " +
                    "             ON " + strCompanyTableName + ".D_STATE_ID    = D_STATE.STATE_ID) " +
                    "     LEFT JOIN  MST_MINISTRY " +
                    "             ON " + strCompanyTableName + ".MINISTRY_ID   = MST_MINISTRY.MINISTRY_ID) " +
                    "     WHERE   " + strCompanyTableName + ".COMPANY_ID       = " + Company + " ";

                if (BasicInfoId > 0)
                    strSQL = strSQL + " AND " + strCompanyTableName + ".BASIC_INFO_ID = " + BasicInfoId + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                    return false;
                //
                while (drdShowRecord.Read())
                {
                    //lngSearchId = Id;

                    lblDedEmpColName.Text = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                    txtDedEmpColName.Text = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                    //---
                    txtResponsiblePersonName.Text = Convert.ToString(drdShowRecord["PERSON_NAME"]);
                    txtTAN.Text = Convert.ToString(drdShowRecord["TAN_NO"]);
                    txtPAN.Text = Convert.ToString(drdShowRecord["PAN_NO"]);
                    txtType.Text = Convert.ToString(drdShowRecord["CATEGORY_CODE"]) + " - " + Convert.ToString(drdShowRecord["CATEGORY_DESCRIPTION"]);
                    //---
                    txtDeductorTypeID.Text = Convert.ToString(drdShowRecord["D_CATEGORY_ID"]);
                    txtMinistryId.Text = Convert.ToString(drdShowRecord["MINISTRY_ID"]);
                    txtDeductorTypeStateID.Text = Convert.ToString(drdShowRecord["D_STATE_ID"]);
                    //
                    txtTANNo.Text         = Convert.ToString(drdShowRecord["TAN_NO"]);
                    txtPANNo.Text         = Convert.ToString(drdShowRecord["PAN_NO"]);
                    txtDeductorType.Text  = Convert.ToString(drdShowRecord["CATEGORY_CODE"]) + " - " + Convert.ToString(drdShowRecord["CATEGORY_DESCRIPTION"]);
                    txtBranch.Text        = Convert.ToString(drdShowRecord["BRANCH_DIV"]);
                    txtCompanyPrefix.Text = Convert.ToString(drdShowRecord["FILE_PREFIX"]);
                    txtAddress1.Text      = Convert.ToString(drdShowRecord["ADDRESS1"]);
                    txtAddress2.Text      = Convert.ToString(drdShowRecord["ADDRESS2"]);
                    txtAddress3.Text      = Convert.ToString(drdShowRecord["ADDRESS3"]);
                    txtAddress4.Text      = Convert.ToString(drdShowRecord["ADDRESS4"]);
                    txtAddress5.Text      = Convert.ToString(drdShowRecord["ADDRESS5"]);
                    cmbState.Text         = Convert.ToString(drdShowRecord["STATE_NAME"]);
                    strStateCode          = Convert.ToString(drdShowRecord["STATE_CODE"]);
                    txtPIN.Text           = Convert.ToString(drdShowRecord["PIN_CODE"]);
                    txtSTD.Text           = Convert.ToString(drdShowRecord["STD"]);
                    txtPhone.Text         = Convert.ToString(drdShowRecord["PHONE"]);
                    txtEmail.Text         = Convert.ToString(drdShowRecord["EMAIL"]);
                    txtRPName.Text        = Convert.ToString(drdShowRecord["PERSON_NAME"]);
                    txtRPDesignation.Text = Convert.ToString(drdShowRecord["DESIGNATION"]);
                    txtRPFatherName.Text = Convert.ToString(drdShowRecord["FATHER_NAME"]);
                    txtRPAddress1.Text    = Convert.ToString(drdShowRecord["P_ADDRESS1"]);
                    txtRPAddress2.Text    = Convert.ToString(drdShowRecord["P_ADDRESS2"]);
                    txtRPAddress3.Text    = Convert.ToString(drdShowRecord["P_ADDRESS3"]);
                    txtRPAddress4.Text    = Convert.ToString(drdShowRecord["P_ADDRESS4"]);
                    txtRPAddress5.Text    = Convert.ToString(drdShowRecord["P_ADDRESS5"]);
                    cmbRPState.Text       = Convert.ToString(drdShowRecord["RP_STATE_NAME"]);
                    strRPStateCode        = Convert.ToString(drdShowRecord["RP_STATE_CODE"]);
                    txtRPPIN.Text         = Convert.ToString(drdShowRecord["P_PIN_CODE"]);
                    txtRPSTD.Text         = Convert.ToString(drdShowRecord["P_STD"]);
                    txtRPPhone.Text       = Convert.ToString(drdShowRecord["P_PHONE"]);
                    txtRPMobileNo.Text    = Convert.ToString(drdShowRecord["P_MOBILE"]);
                    txtRPEmail.Text       = Convert.ToString(drdShowRecord["P_EMAIL"]);
                    txtPAOCode.Text       = Convert.ToString(drdShowRecord["PAO_CODE"]);
                    txtPAORegNo.Text      = Convert.ToString(drdShowRecord["PAO_REG_NO"]);
                    txtDDOCode.Text       = Convert.ToString(drdShowRecord["DDO_CODE"]);
                    txtDDORegNo.Text      = Convert.ToString(drdShowRecord["DDO_REG_NO"]);
                    cmbGovtDedState.Text  = Convert.ToString(drdShowRecord["D_STATE_NAME"]);
                    strDStateCode         = Convert.ToString(drdShowRecord["D_STATE_CODE"]);
                    strMinistryCode       = Convert.ToString(drdShowRecord["MINISTRY_CODE"]);
                    cmbMinistry.Text      = Convert.ToString(drdShowRecord["MINISTRY_NAME"]);
                    txtOtherMinistry.Text = Convert.ToString(drdShowRecord["MINISTRY_OTHER"]);
                    //
                    txtAIN.Text           = Convert.ToString(drdShowRecord["AIN_NO"]);
                    txtTANRegNo.Text      = Convert.ToString(drdShowRecord["TAN_REG_NO"]);
                    txtAltSTD.Text        = Convert.ToString(drdShowRecord["ALT_STD"]);
                    txtAltPhone.Text      = Convert.ToString(drdShowRecord["ALT_PHONE"]);
                    txtAltEmail.Text      = Convert.ToString(drdShowRecord["ALT_EMAIL"]);
                    txtAltRPSTD.Text      = Convert.ToString(drdShowRecord["P_ALT_STD"]);
                    txtAltRPPhone.Text    = Convert.ToString(drdShowRecord["P_ALT_PHONE"]);
                    txtAltRPEmail.Text    = Convert.ToString(drdShowRecord["P_ALT_EMAIL"]);
                    
                    //blnChkChanged = false;
                    //if (Convert.ToString(drdShowRecord["ADDRESS_CHANGE"]) == "1")
                    //    chkAddressChange.Checked = true;
                    //if (Convert.ToString(drdShowRecord["P_ADDRESS_CHANGE"]) == "1")
                    //    chkRPAddressChange.Checked = true;
                    blnChkChanged = true;
                    
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
                lngSearchId = 0;
                ////-----------------------------------------------------------
                //if (strCheckFields == "")
                //    strSQL = strQuery + "order by " + strOrderBy;
                //else
                //    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                ////-----------------------------------------------------------
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
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

        #region ShowChallanRecord
        private bool ShowChallanRecord(long Id)
        {
            IDataReader drdShowChallanRecord = null;
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
                    "     WHERE TRN_CHALLAN.CHALLAN_ID = " + Id + " ";


                drdShowChallanRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowChallanRecord == null)
                {
                    return false;
                }
                while (drdShowChallanRecord.Read())
                {
                    lngChallanID = Id;

                    txtChallanSrlNo.Text = Convert.ToString(drdShowChallanRecord["SL_NO"]);

                    blnSectionDisplay = false;                    
                        cmbSection.Text = Convert.ToString(drdShowChallanRecord["SECTION_NO"]);
                        lblSectionDisplay.Visible = true;
                        lblSectionDisplay.Text = Convert.ToString(drdShowChallanRecord["SECTION_DESCRIPTION"]);                    
                    blnSectionDisplay = true;
                    
                    mskDateOfPayment.Text = Convert.ToString(drdShowChallanRecord["DEPOSIT_DATE"]);
                    txtBSRCode.Text = Convert.ToString(drdShowChallanRecord["BSR_CODE"]);
                    txtChallanNo.Text = Convert.ToString(drdShowChallanRecord["CHALLAN_NO"]);
                    txtTransferVoucherNo.Text = Convert.ToString(drdShowChallanRecord["TRANSFER_VOUCHER_NO"]);
                    txtChequeNo.Text = Convert.ToString(drdShowChallanRecord["CHEQUE_NO"]);
                    txtTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecord["TDS"]) == "" ? "0" : Convert.ToString(drdShowChallanRecord["TDS"])));
                    txtSurcharge.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecord["SURCHARGE"]) == "" ? "0" : Convert.ToString(drdShowChallanRecord["SURCHARGE"])));
                    txtEducationCess.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecord["EDUCATION_CESS"]) == "" ? "0" : Convert.ToString(drdShowChallanRecord["EDUCATION_CESS"])));
                    txtInterests.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecord["INTEREST"]) == "" ? "0" : Convert.ToString(drdShowChallanRecord["INTEREST"])));
                    txtOthers.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecord["OTHERS"]) == "" ? "0" : Convert.ToString(drdShowChallanRecord["OTHERS"])));
                    txtTotalTax.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecord["TOT_TAX"]) == "" ? "0" : Convert.ToString(drdShowChallanRecord["TOT_TAX"])));
                    txtInterestAllocated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecord["INTEREST_ALLOCATED"]) == "" ? "0" : Convert.ToString(drdShowChallanRecord["INTEREST_ALLOCATED"])));
                    txtOthersAllocated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecord["OTHERS_ALLOCATED"]) == "" ? "0" : Convert.ToString(drdShowChallanRecord["OTHERS_ALLOCATED"])));
                    txtRemarks.Text = Convert.ToString(drdShowChallanRecord["REMARKS"]);
                    //
                    if (Convert.ToString(drdShowChallanRecord["BOOK_ENTRY"]) == "1")
                        chkBookEntry.Checked = true;
                    else if (Convert.ToString(drdShowChallanRecord["BOOK_ENTRY"]) == "0")
                        chkBookEntry.Checked = false;
                    //
                    txtFee.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecord["LATE_FEE"]) == "" ? "0" : Convert.ToString(drdShowChallanRecord["LATE_FEE"])));
                    cmbMinorHead.Text = Convert.ToString(drdShowChallanRecord["MINOR_HEAD"]);
                    //
                    drdShowChallanRecord.Close();
                    drdShowChallanRecord.Dispose();

                    if(cmbSection.Text != "")
                        cmbSection.Select();
                    else
                        mskDateOfPayment.Select();
                    //ControlSummaryChallan(lngChallanID);                
                    return true;
                }
                //-----------------------------------------------------------
                drdShowChallanRecord.Close();
                drdShowChallanRecord.Dispose();

                //ControlSummaryChallan(lngChallanID);
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                lngChallanID = 0;
                //-----------------------------------------------------------
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetChallanGridClone != null) dsetChallanGridClone.Clear();
                dsetChallanGridClone = dmlService.J_ShowDataInGrid(ref dgcViewChallan, strSQL, strMatrix);       //Show Data into the Grid
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowChallanRecord.Close();
                drdShowChallanRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ShowChallanDetailsRecord
        private bool ShowChallanDetailsRecord(long Id)
        {
            IDataReader drdShowChallanDetailsRecord = null;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            try
            {
                lblViewChallanDetailsSectionId.Text = "";
                //--
                string[,] strShowChallanDetailsRecordMatrix = {{"TRN_CHALLAN.BOOK_ENTRY = 1 ", "F", "TRN_CHALLAN.TRANSFER_VOUCHER_NO", "F"},
                                                               {"TRN_CHALLAN.BOOK_ENTRY = 0 ", "F", "TRN_CHALLAN.CHALLAN_NO", "F"}};

                //ADDED BY DHRUB ON 07/01/2013 FOR CHALLAN NO DEDUCT LATE_FEE FROM TOT_TAX 
                string[,] strLoadChallanTotTaxMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
                                                        {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED- TRN_CHALLAN.LATE_FEE", "F"}};

                //BLOCKED EXISTING QUERY BY DHRUB ON 07/01/2014
                //strSQL = "SELECT TRN_CHALLAN.CHALLAN_ID          AS CHALLAN_ID," +
                //    "            TRN_CHALLAN.SL_NO               AS SL_NO," +
                //    "            TRN_CHALLAN.SECTION_ID          AS SECTION_ID," +
                //    "            MST_SECTION.SECTION_NO          AS SECTION_NO," +
                //    "            " + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEPOSIT_DATE," +
                //    "            TRN_CHALLAN.BSR_CODE            AS BSR_CODE," +
                //    "            " + cmnService.J_SQLDBFormat(strShowChallanDetailsRecordMatrix, J_SQLColFormat.Case_End) + " AS CHALLAN_NO," +
                //    "            TRN_CHALLAN.CHEQUE_NO           AS CHEQUE_NO," +
                //    "            TRN_CHALLAN.TDS                 AS TDS," +
                //    "            TRN_CHALLAN.SURCHARGE           AS SURCHARGE," +
                //    "            TRN_CHALLAN.EDUCATION_CESS      AS EDUCATION_CESS," +
                //    "            TRN_CHALLAN.INTEREST            AS INTEREST," +
                //    "            TRN_CHALLAN.OTHERS              AS OTHERS," +
                //    "            TRN_CHALLAN.TOT_TAX - " +
                //    "            TRN_CHALLAN.INTEREST_ALLOCATED - " +
                //    "            TRN_CHALLAN.OTHERS_ALLOCATED    AS TOT_TAX," +
                //    "            TRN_CHALLAN.CTRL_TOT_TAX        AS CTRL_TOT_TAX," +
                //    "            TRN_CHALLAN.INTEREST_ALLOCATED  AS INTEREST_ALLOCATED," +
                //    "            TRN_CHALLAN.OTHERS_ALLOCATED    AS OTHERS_ALLOCATED," +
                //    "            TRN_CHALLAN.REMARKS             AS REMARKS," +
                //    "            TRN_CHALLAN.BOOK_ENTRY          AS BOOK_ENTRY " +
                //    "     FROM  (TRN_CHALLAN LEFT JOIN MST_SECTION " +
                //    "            ON  TRN_CHALLAN.SECTION_ID    = MST_SECTION.SECTION_ID)" +
                //    "     WHERE  TRN_CHALLAN.CHALLAN_ID = " + Id + " ";

                //MODIFIED BY DHRUB ON 07/01/2014 
                strSQL = "SELECT TRN_CHALLAN.CHALLAN_ID          AS CHALLAN_ID," +
                    "            TRN_CHALLAN.SL_NO               AS SL_NO," +
                    "            TRN_CHALLAN.SECTION_ID          AS SECTION_ID," +
                    "            MST_SECTION.SECTION_NO          AS SECTION_NO," +
                    "            " + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEPOSIT_DATE," +
                    "            TRN_CHALLAN.BSR_CODE            AS BSR_CODE," +
                    "            " + cmnService.J_SQLDBFormat(strShowChallanDetailsRecordMatrix, J_SQLColFormat.Case_End) + " AS CHALLAN_NO," +
                    "            TRN_CHALLAN.CHEQUE_NO           AS CHEQUE_NO," +
                    "            TRN_CHALLAN.TDS                 AS TDS," +
                    "            TRN_CHALLAN.SURCHARGE           AS SURCHARGE," +
                    "            TRN_CHALLAN.EDUCATION_CESS      AS EDUCATION_CESS," +
                    "            TRN_CHALLAN.INTEREST            AS INTEREST," +
                    "            TRN_CHALLAN.OTHERS              AS OTHERS," +
                    "            " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " AS TOT_TAX," +
                    "            TRN_CHALLAN.CTRL_TOT_TAX        AS CTRL_TOT_TAX," +
                    "            TRN_CHALLAN.INTEREST_ALLOCATED  AS INTEREST_ALLOCATED," +
                    "            TRN_CHALLAN.OTHERS_ALLOCATED    AS OTHERS_ALLOCATED," +
                    "            TRN_CHALLAN.REMARKS             AS REMARKS," +
                    "            TRN_CHALLAN.BOOK_ENTRY          AS BOOK_ENTRY " +
                    "     FROM  (TRN_CHALLAN LEFT JOIN MST_SECTION " +
                    "            ON  TRN_CHALLAN.SECTION_ID    = MST_SECTION.SECTION_ID)" +
                    "     WHERE  TRN_CHALLAN.CHALLAN_ID = " + Id + " ";

                drdShowChallanDetailsRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowChallanDetailsRecord == null)
                    return false;
                //
                while (drdShowChallanDetailsRecord.Read())
                {
                    lngChallanID = Id;

                    txtViewChallanDetailsSrlNo.Text = Convert.ToString(drdShowChallanDetailsRecord["SL_NO"]);
                    txtViewChallanDetailsSection.Text = Convert.ToString(drdShowChallanDetailsRecord["SECTION_NO"]);
                    lblViewChallanDetailsSectionId.Text = Convert.ToString(drdShowChallanDetailsRecord["SECTION_ID"]);
                    mskViewChallanDetailsDate.Text = Convert.ToString(drdShowChallanDetailsRecord["DEPOSIT_DATE"]);
                    txtViewChallanDetailsChallanNo.Text = Convert.ToString(drdShowChallanDetailsRecord["CHALLAN_NO"]);
                    //
                    if (Convert.ToString(drdShowChallanDetailsRecord["BOOK_ENTRY"]) == "1")
                        lblViewChallanDetailsChallanNo.Text = "Trf Vchr(DDO Sl.)";
                    else if (Convert.ToString(drdShowChallanDetailsRecord["BOOK_ENTRY"]) == "0")
                        lblViewChallanDetailsChallanNo.Text = "Challan No.";
                    
                    txtViewChallanDetailsTotalTax.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanDetailsRecord["TOT_TAX"]) == "" ? "0" : Convert.ToString(drdShowChallanDetailsRecord["TOT_TAX"])));
                    txtViewDeducteeTotalTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanDetailsRecord["CTRL_TOT_TAX"]) == "" ? "0" : Convert.ToString(drdShowChallanDetailsRecord["CTRL_TOT_TAX"])));
                    //--
                    //
                    #region COMMENTED
                    //if (txtViewChallanDetailsSection.Text == "194C")
                    //{
                    //    //
                    //    //rbnTransporter.Visible = true;
                    //    //rbnNoDeduction.Visible = false;
                    //    //rbnSoftware.Visible = false;
                    //    //
                    //    cmbRemarks.Items.Remove(3);//--NO DEDUCTION
                    //    cmbRemarks.Items.Remove(5); //-- SOFTWARE
                    //    cmbRemarks.SelectedIndex = 6; //-- TRANSPORTER
                    //}
                    //else if (txtViewChallanDetailsSection.Text == "194J")
                    //{
                    //    //
                    //    rbnTransporter.Visible = false;
                    //    rbnNoDeduction.Visible = false;

                    //    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2012_13ID)
                    //        rbnSoftware.Visible = true;
                    //    else
                    //        rbnSoftware.Visible = false;
                    //}
                    //else if ((txtViewChallanDetailsSection.Text == "194") ||
                    //         (txtViewChallanDetailsSection.Text == "194A") ||
                    //         (txtViewChallanDetailsSection.Text == "194EE") ||
                    //         (txtViewChallanDetailsSection.Text == "193"))
                    //{
                    //    rbnNoDeduction.Visible = true;
                    //    rbnTransporter.Visible = false;
                    //    rbnSoftware.Visible = false;
                    //}
                    //else
                    //{
                    //    rbnNoDeduction.Visible = false;
                    //    rbnTransporter.Visible = false;
                    //    rbnSoftware.Visible = false;
                    //}
                    ////
                    //if (txtViewChallanDetailsSection.Text == "194F")
                    //    rbnThresholdLimit.Visible = false;
                    //else
                    //    rbnThresholdLimit.Visible = true;
                    
                    //// ONLY FOR FIANANCIAL YEAR LESS THAN 2010-11
                    //if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) < T_FinancialYearID.F2010_11ID)
                    //{
                    //    rbnTransporter.Visible = false;
                    //    rbnHigherRate.Visible = false;
                    //    rbnThresholdLimit.Visible = false;
                    //}
                    #endregion
                    //--
                    drdShowChallanDetailsRecord.Close();
                    drdShowChallanDetailsRecord.Dispose();
                    //
                    if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                    {
                        lblViewChallanDetailsSection.Visible = false;
                        txtViewChallanDetailsSection.Visible = false;
                    }
                    else
                    {
                        lblViewChallanDetailsSection.Visible = true;
                        txtViewChallanDetailsSection.Visible = true;
                    }
                    //
                    txtDeducteeName.Select();
                    return true;
                }
                //-----------------------------------------------------------
                drdShowChallanDetailsRecord.Close();
                drdShowChallanDetailsRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                lngChallanID = 0;
                //-----------------------------------------------------------
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetChallanDetailsGridClone != null) dsetChallanDetailsGridClone.Clear();
                dsetChallanDetailsGridClone = dmlService.J_ShowDataInGrid(ref dgcViewChallan, strSQL, strMatrix);       //Show Data into the Grid
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowChallanDetailsRecord.Close();
                drdShowChallanDetailsRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ShowDeducteeRecord
        private bool ShowDeducteeRecord(long Id)
        {
            IDataReader drdShowDeducteeRecord = null;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            try
            {
                string[,] strShowDeducteeCodeMatrix = {{"MST_DEDUCTEE.DEDUCTEE_CODE ='" + T_DeducteeCode.Company + "'" , "F", T_DeducteeCodeDesc.Company, "T"},
                                                {"MST_DEDUCTEE.DEDUCTEE_CODE = '" + T_DeducteeCode.NonCompany + "'", "F", T_DeducteeCodeDesc.NonCompany, "T"}};

                strSQL = "SELECT TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID   AS DEDUCTEE_DETAIL_ID," +
                    "            TRN_DEDUCTEE_DETAILS.SL_NO                AS SL_NO," +
                    "            MST_DEDUCTEE.DEDUCTEE_ID                  AS DEDUCTEE_ID," +
                    "            MST_DEDUCTEE.DEDUCTEE_NAME                AS DEDUCTEE_NAME," +
                    "            MST_DEDUCTEE.DEDUCTEE_PAN                 AS DEDUCTEE_PAN," +
                    "            " + cmnService.J_SQLDBFormat(strShowDeducteeCodeMatrix, J_SQLColFormat.Case_End) + " AS DEDUCTEE_CODE," +
                    "            " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "  AS PAYMENT_DATE," +
                    "            " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEDUCTED_DATE," +
                    "            TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT       AS PAYMENT_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.RATE                 AS RATE," +
                    "            TRN_DEDUCTEE_DETAILS.TAX_AMOUNT           AS TAX_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT     AS SURCHARGE_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.CESS_AMOUNT          AS CESS_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT         AS TOTAL_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS TAX_DEPOSITED_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.NON_DEDUCTION_FLAG   AS NON_DEDUCTION_FLAG," +
                    "            TRN_DEDUCTEE_DETAILS.CASH_BOOK_ENTRY      AS CASH_BOOK_ENTRY," +
                    "            REASON + ' ' + MST_REASON.DESCRIPTION     AS REASON_DESCRIPTION," +
                    "            MST_SECTION.SECTION_NO                    AS SECTION_NO," +
                    "            MST_SECTION.SECTION_DESCRIPTION           AS SECTION_DESCRIPTION," +
                    "            TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO       AS CERTIFICATE_NO " +
                    "     FROM   TRN_DEDUCTEE_DETAILS," +
                    "            MST_DEDUCTEE," +
                    "            MST_REASON," +
                    "            MST_SECTION " +
                    "     WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID  = MST_DEDUCTEE.DEDUCTEE_ID " +
                    "     AND    TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID " +
                    "     AND    TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID " +
                    "     AND    TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID = " + Id + " ";
                //
                drdShowDeducteeRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowDeducteeRecord == null)
                    return false;
                //--
                while (drdShowDeducteeRecord.Read())
                {
                    lngDeducteeDetailID = Id;

                    blnShowHelp = false;
                    blnShowPANHelp = false;

                    txtDeducteeSrlNo.Text = Convert.ToString(drdShowDeducteeRecord["SL_NO"]);         
           
                    txtDeducteeName.Text = Convert.ToString(drdShowDeducteeRecord["DEDUCTEE_NAME"]);
                    txtDeducteeName.SelectionStart = 0;

                    txtOldDeducteeName.Text = Convert.ToString(drdShowDeducteeRecord["DEDUCTEE_NAME"]);
                    txtDeducteePAN.Text = Convert.ToString(drdShowDeducteeRecord["DEDUCTEE_PAN"]);
                    cmbDeducteeCode.Text = Convert.ToString(drdShowDeducteeRecord["DEDUCTEE_CODE"]);
                    mskDeducteeDate.Text = Convert.ToString(drdShowDeducteeRecord["PAYMENT_DATE"]);
                    txtDeducteeAmountOfPayment.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["PAYMENT_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["PAYMENT_AMOUNT"])));
                    txtDeducteeRate.Text = string.Format("{0:0.0000}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["RATE"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["RATE"])));
                    txtDeducteeIncometax.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TAX_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TAX_AMOUNT"])));
                    txtOldDeducteeIncometax.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TAX_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TAX_AMOUNT"])));

                    txtDeducteeSurcharge.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["SURCHARGE_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["SURCHARGE_AMOUNT"])));
                    txtOldDeducteeSurcharge.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["SURCHARGE_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["SURCHARGE_AMOUNT"])));

                    txtDeducteeCess.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["CESS_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["CESS_AMOUNT"])));
                    txtOldDeducteeCess.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["CESS_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["CESS_AMOUNT"])));

                    txtDeducteeTotal.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TOTAL_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TOTAL_AMOUNT"])));
                    txtOldDeducteeTotal.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TOTAL_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TOTAL_AMOUNT"])));
                    
                    txtDeducteeTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TAX_DEPOSITED_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TAX_DEPOSITED_AMOUNT"])));
                    txtOldDeducteeTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TAX_DEPOSITED_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TAX_DEPOSITED_AMOUNT"])));

                    //if (Convert.ToString(drdShowDeducteeRecord["NON_DEDUCTION_FLAG"]) == "")
                    //    rbnNormal.Checked = true;
                    //else if (Convert.ToString(drdShowDeducteeRecord["NON_DEDUCTION_FLAG"]) == "A")
                    //    rbnLowerNoDeduction.Checked = true;
                    //else if (Convert.ToString(drdShowDeducteeRecord["NON_DEDUCTION_FLAG"]) == "T")
                    //    rbnTransporter.Checked = true;
                    //else if (Convert.ToString(drdShowDeducteeRecord["NON_DEDUCTION_FLAG"]) == "B")
                    //    rbnNoDeduction.Checked = true;
                    //else if (Convert.ToString(drdShowDeducteeRecord["NON_DEDUCTION_FLAG"]) == "C")
                    //    rbnHigherRate.Checked = true;
                    //else if (Convert.ToString(drdShowDeducteeRecord["NON_DEDUCTION_FLAG"]) == "Y")
                    //    rbnThresholdLimit.Checked = true;
                    //else if (Convert.ToString(drdShowDeducteeRecord["NON_DEDUCTION_FLAG"]) == "S")
                    //    rbnSoftware.Checked = true;
                    cmbRemarks.Text = Convert.ToString(drdShowDeducteeRecord["REASON_DESCRIPTION"]);
                    //txtDeducteeTotalTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecord["TAX_DEPOSITED_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowChallanRecord["TDS"])));
                    // ANIK 2011-08-04
                    if (Convert.ToString(drdShowDeducteeRecord["CASH_BOOK_ENTRY"]) == "1")
                        chkCashBookEntry.Checked = true;
                    else
                        chkCashBookEntry.Checked = false; 

                    //Added by Shrey Kejriwal on 26/03/2012
                    mskDeductedDate.Text = Convert.ToString(drdShowDeducteeRecord["DEDUCTED_DATE"]);
                    //
                    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2013_14ID)
                    {
                        blnSectionDDDisplay = false;
                        cmbDDSection.Text = Convert.ToString(drdShowDeducteeRecord["SECTION_NO"]);
                        lblDDSectionDisplay.Visible = true;
                        lblDDSectionDisplay.Text = Convert.ToString(drdShowDeducteeRecord["SECTION_DESCRIPTION"]);
                        blnSectionDDDisplay = true;
                    }
                    //
                    txtCertificateNo.Text = Convert.ToString(drdShowDeducteeRecord["CERTIFICATE_NO"]);
                    //
                    blnShowHelp = true;
                    blnShowPANHelp = true;

                    drdShowDeducteeRecord.Close();
                    drdShowDeducteeRecord.Dispose();

                    txtDeducteeName.Select();
                    return true;
                }
                //-----------------------------------------------------------
                drdShowDeducteeRecord.Close();
                drdShowDeducteeRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                lngChallanID = 0;
                //-----------------------------------------------------------
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetChallanGridClone != null) dsetChallanGridClone.Clear();
                dsetChallanGridClone = dmlService.J_ShowDataInGrid(ref dgcViewDeductee, strSQL, strMatrix);       //Show Data into the Grid
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowDeducteeRecord.Close();
                drdShowDeducteeRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ShowRecordGenerateTDS
        private bool ShowRecordGenerateTDS(long BasicInfoId)
        {
            try
            {
                //txtViewCompanyName.Text = txtDedEmpColName.Text;
                //txtViewTAN.Text = txtTANNo.Text;
                //txtViewPAN.Text = txtPANNo.Text;
                //txtViewDeductorType.Text = txtDeductorType.Text;
                //txtViewPersonName.Text = txtRPName.Text;
                //txtViewDesignation.Text = txtRPDesignation.Text;

                //txtViewFormNo.Text = strFormNo;
                //txtViewFinancialYear.Text = cmbFinancialYear.Text;
                //txtViewAssessmentYear.Text = TdsMan.T_ReturnAssessmentYear(cmbFinancialYear.Text);
                //txtViewQuarter.Text = cmbQuarter.Text;

                //txtViewFileCreationDateTime.Text = Convert.ToString(System.DateTime.Now);

                //txtViewTotalChallan.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(CHALLAN_ID) AS COUNT_CHALLAN_ID FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID));
                
                //if (cmnService.J_ReturnDoubleValue(txtViewTotalChallan.Text) == 0)
                //    return false;

                //txtViewTotalTax.Text = string.Format("{0:0.00}", Convert.ToDouble(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID)));

                //strSQL = "SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                //txtViewTotalTDSTCS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))));

                //strSQL = "SELECT COUNT(DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                //txtViewTotalDeductees.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                //strSQL = "SELECT SUM(TAX_DEPOSITED_AMOUNT) AS SUM_TAX_DEPOSITED_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                //txtViewTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))));

                //if(Convert.ToDouble(txtViewTotalTax.Text) > 0)
                //    chkCSIFileDownload.Checked = true;
                //else
                //    chkCSIFileDownload.Checked = false;

                //mskViewCSIFileDownloadFrom.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MIN(DEPOSIT_DATE) AS MIN_DEPOSIT_DATE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID));
                //mskViewCSIFileDownloadTo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MAX(DEPOSIT_DATE) AS MAX_DEPOSIT_DATE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID));


                //File.
                //txtViewInputFilePath.Text = newPath;
                //txtViewFVUPath.Text = activeDir + "\\TDS_FVU_3.0\\TDS_FVU_STANDALONE.JAR";
                return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (lblSearchMode.Text == J_Mode.Sorting)
                {
                    //if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
                    //    return false;
                    //}
                    return true;
                }
                else if (lblMode.Text == J_Mode.Searching)
                {
                    // CHALLAN ENTRY
                    //if (dgcViewChallan.Visible == true)
                    if (lblSearchMode.Text == Tabbed_Mode.Challan)
                    {
                        if (grpSearch.Visible == false)
                        {
                            if (Convert.ToInt64(Convert.ToString(dgcViewChallan.CurrentRowIndex)) < 0)
                            {
                                //cmnService.J_UserMessage(J_Msg.DataNotFound);
                                //if (dsetGridClone == null) return false;
                                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "MEMBER_LOAN_ID", lngSearchId);
                                return false;
                            }
                        }
                        else if (grpSearch.Visible == true)
                        {
                            if (dtService.J_IsBlankDateCheck(ref mskDepositDateSearch, J_ShowMessage.NO) == true &&
                                txtSlNoSearch.Text.Trim() == "" &&
                                cmbSectionNoSearch.SelectedIndex <= 0 &&
                                txtChallanNoSearch.Text.Trim() == "" &&
                                txtTransferVoucherNoSearch.Text.Trim() == "")
                            {
                                cmnService.J_UserMessage(J_Msg.SearchingValues);
                                txtSlNoSearch.Select();
                                return false;
                            }
                            if (dtService.J_IsBlankDateCheck(ref mskDepositDateSearch, J_ShowMessage.NO) == false)
                            {
                                if (dtService.J_IsDateValid(mskDepositDateSearch) == false)
                                {
                                    cmnService.J_UserMessage("Enter valid Deposit Date to Search");
                                    mskDepositDateSearch.Select();
                                    return false;
                                }
                            }
                        }
                        return true;
                    }

                    // CHALLAN DETAILS
                    //else if (dgcViewChallanDetails.Visible == true)
                    else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
                    {
                        if (grpSearchDeductee.Visible == false)
                        {
                            if (Convert.ToInt64(Convert.ToString(dgcViewDeductee.CurrentRowIndex)) < 0)
                            {
                                //cmnService.J_UserMessage(J_Msg.DataNotFound);
                                //if (dsetGridClone == null) return false;
                                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "MEMBER_LOAN_ID", lngSearchId);
                                return false;
                            }
                        }
                        else if (grpSearchDeductee.Visible == true)
                        {
                            if (dtService.J_IsBlankDateCheck(ref mskDateSearch, J_ShowMessage.NO) == true &&
                                txtSlNoDSearch.Text.Trim() == "" &&
                                txtPANSearch.Text.Trim() == "" &&
                                txtDeducteeNameSearch.Text.Trim() == "")
                            {
                                cmnService.J_UserMessage(J_Msg.SearchingValues);
                                txtSlNoDSearch.Select();
                                return false;
                            }
                            if (dtService.J_IsBlankDateCheck(ref mskDateSearch, J_ShowMessage.NO) == false)
                            {
                                if (dtService.J_IsDateValid(mskDateSearch) == false)
                                {
                                    cmnService.J_UserMessage("Incorrect Format of the Date to Search");
                                    mskDateSearch.Select();
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                    return true;
                }
                else if (lblSearchMode.Text == Tabbed_Mode.Basic)
                {
                    return true;
                }
                else if (lblSearchMode.Text == Tabbed_Mode.Challan)
                {
                    //-----------------------------------------------------------------------
                    //-- SERIAL NO.
                    //-----------------------------------------------------------------------
                    if (txtChallanSrlNo.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Record can not be Saved without Serial No.");
                        txtChallanSrlNo.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- SECTION
                    //-----------------------------------------------------------------------
                    if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) < T_FinancialYearID.F2013_14ID)
                    {
                        if (cmbSection.SelectedIndex <= 0)
                        {
                            cmnService.J_UserMessage("Section - Cannot be Blank");
                            cmbSection.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- DATE OF PAYMENT
                    //-----------------------------------------------------------------------
                    if (dtService.J_IsBlankDateCheck(ref mskDateOfPayment, "Payment Date - Cannot be Blank") == true)
                        return false;
                    //----------------------------------------------------------
                    //-- VALID DATE CHECK
                    //----------------------------------------------------------
                    if (dtService.J_IsDateValid(mskDateOfPayment) == false)
                    {
                        cmnService.J_UserMessage("Incorrect Format of the Date of Payment");
                        mskDateOfPayment.Select();
                        return false;
                    }
                    //---------------------------------------------------------------
                    //-- VALID DATE CHECK FOR BOOKENTRY ADDED BY DHRUB ON 08/01/2014
                    //----------------------------------------------------------------
                    if(chkBookEntry.Checked == true)
                    {
                        if (DateTime.Parse(mskDateOfPayment.Text) != DateTime.Parse(TdsMan.CalcLastDateofMonth(mskDateOfPayment.Text)))
                        {
                            cmnService.J_UserMessage("Date of Transfer Voucher must be last date of month.");
                            mskDateOfPayment.Select();
                            return false;
                        }
                    }
                    //----------------------------------------------------------------
                    //-- FINANCIAL YEAR CHECK FOR PAYMENT DATE WHEN BOOKENTRY NOT CHECKED 
                    //----------------------------------------------------------------
                    if (chkBookEntry.Checked == false)
                    {
                        //if (dtService.J_ConvertToIntYYYYMMDD(mskDateOfPayment.Text) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnStartDateFinancialYear(cmbFinancialYear.Text)))
                        //-- ANIK @ 2013/09/27 for FVU 4.0
                        if (dtService.J_ConvertToIntYYYYMMDD(mskDateOfPayment.Text) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnStartDateFinancialYear(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) - 1)))
                        {
                            //cmnService.J_UserMessage("Challan Date should not be for previous financial years");
                            cmnService.J_UserMessage("Challan date cannot be before the start date of the immediate previous Financial Year.");
                            mskDateOfPayment.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------
                    //-- FINANCIAL YEAR CHECK FOR PAYMENT DATE WHEN BOOKENTRY CHECKED 
                    //-----------------------------------------------------------------
                    else if (chkBookEntry.Checked == true)
                    {
                        //if (dtService.J_ConvertToIntYYYYMMDD(mskDateOfPayment.Text) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnStartDateFinancialYear(cmbFinancialYear.Text)))
                        //-- ANIK @ 2013/09/27 for FVU 4.0
                        if (dtService.J_ConvertToIntYYYYMMDD(mskDateOfPayment.Text) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnStartDateFinancialYear(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex))))
                        {
                            //cmnService.J_UserMessage("Challan Date should not be for previous financial years");
                            cmnService.J_UserMessage("Transfer voucher date cannot be before the start date of the Financial Year.");
                            mskDateOfPayment.Select();
                            return false;
                        }
                    }
                    //----------------------------------------------------------
                    //-- QUARTER
                    //----------------------------------------------------------
                    if (dtService.J_ConvertToIntYYYYMMDD(mskDateOfPayment.Text) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text)))
                    {
                        if (cmnService.J_UserMessage("Challan Date falls under previous quarters - Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            mskDateOfPayment.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- BSR CODE AND 24G RECEIPT NO CHECK
                    //-----------------------------------------------------------------------
                    if (chkBookEntry.Checked == false)
                    {
                        if (Convert.ToDouble(txtTotalTax.Text.Trim()) == 0)
                        {
                            if (txtBSRCode.Text.Trim() != "")
                            {
                                cmnService.J_UserMessage("BSR Code - Should be Blank for NIL challan");
                                txtBSRCode.Select();
                                return false;
                            }
                        }
                        else
                        {
                            if (txtBSRCode.Text.Trim() == "")
                            {
                                cmnService.J_UserMessage("BSR Code - Cannot be Blank");
                                txtBSRCode.Select();
                                return false;
                            }
                            //ADDED BY SHREY TO CHECK BSR CODE FOR 7 DIGITS
                            if (txtBSRCode.Text.Trim().Length != 7)
                            {
                                cmnService.J_UserMessage("BSR Code should be of 7 digits");
                                txtBSRCode.Select();
                                return false;
                            }
                        }
                    }
                    //ADDED BY SHREY CHECK FOR BOOK ENTRY
                    else
                    {
                        //ADDING FINANCIAL YEAR CHECK CLAUSE FOR VALIDATING TRANSFER VOUCHER NO AND 24G RECEIPT NO

                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2010_11ID)
                        {
                            if (Convert.ToDouble(txtTotalTax.Text.Trim()) == 0)
                            {
                                if (txtBSRCode.Text.Trim() != "")
                                {
                                    cmnService.J_UserMessage("24G Receipt No - Should be Blank for NIL challan");
                                    txtBSRCode.Select();
                                    return false;
                                }
                            }
                            else
                            {
                                //Added by Shrey Kejriwal on 19/01/2011
                                if (txtBSRCode.Text.Trim() == "")
                                {
                                    cmnService.J_UserMessage("24G Receipt No - Cannot be Blank");
                                    txtBSRCode.Select();
                                    return false;
                                }
                                //ADDED BY SHREY TO CHECK BSR CODE FOR 7 DIGITS
                                if (txtBSRCode.Text.Trim().Length != 7)
                                {
                                    cmnService.J_UserMessage("24G Receipt No should be of 7 digits");
                                    txtBSRCode.Select();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            //Added by Shrey Kejriwal on 22/03/2012

                            //24G RECEIPT NO NOT APPLICABLE FOR F.Y. UPTO 2009-10
                            if (txtBSRCode.Text.Trim() != "")
                            {
                                cmnService.J_UserMessage("24G Receipt No. not applicable for F.Y. " + cmbFinancialYear.Text);
                                txtBSRCode.Select();
                                return false;
                            }
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- CHALLAN NO. VALIDATION (ADDED BY SHREY)
                    //-----------------------------------------------------------------------
                    if (chkBookEntry.Checked == false)
                    {
                        if (Convert.ToDouble(txtTotalTax.Text.Trim()) == 0)
                        {
                            if (txtChallanNo.Text.Trim() != "")
                            {
                                cmnService.J_UserMessage("Challan No. - Should be Blank for NIL challan");
                                txtChallanNo.Select();
                                return false;
                            }
                        }
                        else
                        {
                            if (txtChallanNo.Text.Trim() == "")
                            {
                                cmnService.J_UserMessage("Challan No. - Cannot be Blank");
                                txtChallanNo.Select();
                                return false;
                            }
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- TRANSFER VOUCHER NO. VALIDATION (ADDED BY SHREY)
                    //-----------------------------------------------------------------------
                    if (chkBookEntry.Checked == true)
                    {
                        if (Convert.ToDouble(txtTotalTax.Text.Trim()) == 0)
                        {
                            if (txtTransferVoucherNo.Text.Trim() != "")
                            {
                                cmnService.J_UserMessage("Transfer Voucher No. - Should be Blank for NIL challan");
                                txtTransferVoucherNo.Select();
                                return false;
                            }
                        }
                        else
                        {
                            ////Added by Shrey Kejriwal on 19/01/2012

                            //Adding Financial year clause
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2010_11ID)
                            {
                                if (txtTransferVoucherNo.Text.Trim() == "")
                                {
                                    cmnService.J_UserMessage("Transfer Voucher No. - Cannot be Blank");
                                    txtTransferVoucherNo.Select();
                                    return false;
                                }
                            }
                            if (txtTransferVoucherNo.Text.Trim().Length > 5)
                            {
                                cmnService.J_UserMessage("Transfer Voucher No. Cannot be more than 5 digits");
                                txtTransferVoucherNo.Select();
                                return false;
                            }
                        }
                    }
                    // ALLOCATED VALUE CAN NEVER BE GREATER THAN TOTAL CHALLAN DEPOSIT
                    if ((cmnService.J_ReturnDoubleValue(txtInterestAllocated.Text) +
                        cmnService.J_ReturnDoubleValue(txtOthersAllocated.Text)) > Convert.ToDouble(txtTotalTax.Text))
                    {
                        cmnService.J_UserMessage("Sum of Allocated fields can never be greater than Total Challan Deposit");
                        txtInterestAllocated.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtBSRCode.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("BSR Code - '^' not allowed");
                        txtBSRCode.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtChallanNo.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Challan No. - '^' not allowed");
                        txtChallanNo.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtChequeNo.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Cheque No. - '^' not allowed");
                        txtChequeNo.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtRemarks.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Remarks - '^' not allowed");
                        txtRemarks.Select();
                        return false;
                    }
                    //---------------
                    //------------
                    //--- CHECK IF CHALLAN DETAILS AMOUNT FIELD VALUES ARE WHOLE NUMBERS---
                    //----ADDED BY SHREY -----------------

                    // CHECK TDS AMOUNT FIELD
                    if (TdsMan.T_CheckChallanAmount(txtTDS.Text.Trim()) == false)
                    {
                        cmnService.J_UserMessage("TDS Amount should not have decimal values");
                        txtTDS.Select();
                        return false;
                    }

                    // CHECK SURCHARGE AMOUNT FIELD
                    if (TdsMan.T_CheckChallanAmount(txtSurcharge.Text.Trim()) == false)
                    {
                        cmnService.J_UserMessage("Surcharge should not have decimal values");
                        txtSurcharge.Select();
                        return false;
                    }
                    // CHECK E.CESS AMOUNT FIELD
                    if (TdsMan.T_CheckChallanAmount(txtEducationCess.Text.Trim()) == false)
                    {
                        cmnService.J_UserMessage("Education Cess should not have decimal values");
                        txtEducationCess.Select();
                        return false;
                    }

                    // CHECK INTEREST AMOUNT FIELD
                    if (TdsMan.T_CheckChallanAmount(txtInterests.Text.Trim()) == false)
                    {
                        cmnService.J_UserMessage("Interest should not have decimal values");
                        txtInterests.Select();
                        return false;
                    }
                    // CHECK OTHERS AMOUNT FIELD
                    if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2012_13ID)
                    {
                        if (TdsMan.T_CheckChallanAmount(txtFee.Text.Trim()) == false)
                        {
                            cmnService.J_UserMessage("Fee should not have decimal values");
                            txtFee.Select();
                            return false;
                        }
                    }
                    else
                    {
                        if (cmnService.J_ReturnDoubleValue(txtFee.Text) > 0)
                        {
                            cmnService.J_UserMessage("Fee is not applicable before FY 2012-13");
                            txtFee.Select();
                            return false;
                        }
                    }                    
                    // CHECK OTHERS AMOUNT FIELD
                    if (TdsMan.T_CheckChallanAmount(txtOthers.Text.Trim()) == false)
                    {
                        cmnService.J_UserMessage("Others should not have decimal values");
                        txtOthers.Select();
                        return false;
                    }
                    //-- MINOR HEAD
                    if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                    {
                        if (chkBookEntry.Checked == false)
                        {
                            if (cmnService.J_ReturnDoubleValue(txtTotalTax.Text) > 0)
                            {
                                if (cmbMinorHead.SelectedIndex <= 0)
                                {
                                    cmnService.J_UserMessage("Minor Head - Cannot be Blank");
                                    cmbMinorHead.Select();
                                    return false;
                                }
                            }
                            else
                            {
                                if (cmbMinorHead.SelectedIndex > 0)
                                {
                                    cmnService.J_UserMessage("Minor Head - Should be Blank for NIL Challan");
                                    cmbMinorHead.Select();
                                    return false;
                                }
                            }
                        }
                        else if (chkBookEntry.Checked == true)
                        {
                            //--
                            if (cmbMinorHead.SelectedIndex > 0)
                            {
                                cmnService.J_UserMessage("Minor Head should be blank for Book Entry");
                                cmbMinorHead.Select();
                                return false;
                            }
                        }
                    }
                    //--------------------------------------------
                    //ADDED BY DHRUB FOR NIL RETURN ON 10/01/2014 
                    //--------------------------------------------
                    if (Convert.ToDouble(txtTotalTax.Text.Trim()) == 0)
                    {
                        if (cmnService.J_UserMessage("Are you sure you want to save this Challan of Nil value?", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            txtBSRCode.Select();
                            return false;
                        }
                    }
                    //--------------------------------------------
                    //--------------------------------------------
                }
                else  if (lblSearchMode.Text == Tabbed_Mode.Deductee)
                {
                    //-----------------------------------------------------------------------
                    //-- DEDUCTEE NAME
                    //-----------------------------------------------------------------------
                    if (txtDeducteeName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Deductee Name - Cannot be Blank");
                        txtDeducteeName.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtDeducteeName.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Deductee Name - '^' not allowed");
                        txtDeducteeName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- DEDUCTEE PAN
                    //-----------------------------------------------------------------------
                    if (txtDeducteePAN.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Deductee PAN - Cannot be Blank");
                        txtDeducteePAN.Select();
                        return false;
                    }
                    //Added by Shrey Kejriwal on 23/09/2011
                    if (txtDeducteePAN.Text.Length != 10)
                    {
                        cmnService.J_UserMessage("Deductee PAN - Should be of 10 characters");
                        txtDeducteePAN.Select();
                        return false;
                    }

                    //Added by Shrey Kejriwal on 26/09/2011
                    if (txtDeducteePAN.Text != "PANNOTAVBL")
                    {
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtDeducteePAN.Text, 5), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtDeducteePAN.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtDeducteePAN.Text, 5, 4), J_DataType.Numeric) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtDeducteePAN.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtDeducteePAN.Text, 1), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtDeducteePAN.Select();
                            return false;
                        }
                    }

                    //-----------------------------------------------------------------------
                    //-- DEDUCTEE CODE
                    //-----------------------------------------------------------------------
                    if (cmbDeducteeCode.SelectedIndex < 0)
                    {
                        cmnService.J_UserMessage("Deductee Code - Cannot be Blank");
                        cmbDeducteeCode.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- SECTION
                    //-----------------------------------------------------------------------
                    string strValidateSection = "";
                    if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                    {
                        if (cmbDDSection.SelectedIndex <= 0)
                        {
                            cmnService.J_UserMessage("Section - Cannot be Blank");
                            cmbDDSection.Select();
                            return false;
                        }
                        else
                            strValidateSection = cmbDDSection.Text;
                    }
                    else
                    {
                        strValidateSection = txtViewChallanDetailsSection.Text;
                    }
                    //-----------------------------------------------------------------------
                    //-- BLANK DATE CHECK
                    //-----------------------------------------------------------------------
                    if (dtService.J_IsBlankDateCheck(ref mskDeducteeDate, "Date - Cannot be Blank") == true)
                        return false;
                    //----------------------------------------------------------
                    //-- VALID DATE CHECK
                    //----------------------------------------------------------
                    if (dtService.J_IsDateValid(mskDeducteeDate) == false)
                    {
                        cmnService.J_UserMessage("Incorrect Format of the Date");
                        mskDeducteeDate.Select();
                        return false;
                    }
                    //----------------------------------------------------------
                    //-- QUARTER
                    //----------------------------------------------------------

                    //BLOCKED BY DHRUB ON 11/01/2014 FOR PAYMENT DATE CHECKING
                    //---------------------------------------------------------
                    //---------------------------------------------------------
                    // ANIK GHOSH 2011-04-30
                    //if (rbnThresholdLimit.Checked == true)
                    //if (Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex)) == 9)
                    //{
                    //    if (dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnStartDateFinancialYear(cmbFinancialYear.Text)) ||
                    //        dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) > dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnEndDateFinancialYear(cmbFinancialYear.Text)))
                    //    {
                    //        cmnService.J_UserMessage("Date should be within the FA Year");
                    //        mskDeducteeDate.Select();
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                    //    //if (dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text)) ||
                    //    //    dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) > dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text)))
                    //    if (dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnStartDateFinancialYear(cmbFinancialYear.Text)) ||
                    //        dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) > dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text)))
                    //    {
                    //        cmnService.J_UserMessage("Date should be within the Financial Year and before the End Date of Quarter");
                    //        mskDeducteeDate.Select();
                    //        return false;
                    //    }
                    //}
                    //---------------------------------------------------------
                    //---------------------------------------------------------

                    //-----------------------------------------------------------------------------------------------
                    //ADDED BY DHRUB ON 11/01/2014 FOR PAYMENT DATE SHOULD WITHIN F.Y START DATE AND QUARTER END DATE  
                    //-----------------------------------------------------------------------------------------------
                    //if (dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text)) ||
                    //    dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) > dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text)))
                    if (dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnStartDateFinancialYear(cmbFinancialYear.Text)) ||
                        dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) > dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text)))
                    {
                        cmnService.J_UserMessage("Date should be within the Financial Year and before the End Date of Quarter.");
                        mskDeducteeDate.Select();
                        return false;
                    }
                    //----------------------------------------------------------
                    //-- PAYMENT DATE
                    //----------------------------------------------------------
                    if (dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) > dtService.J_ConvertToIntYYYYMMDD(mskViewChallanDetailsDate.Text))
                    {
                        if (cmnService.J_UserMessage("Date is after the Challan Date - Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            mskDeducteeDate.Select();
                            return false;
                        }
                    }
                    
                    //----------------------------------------------------
                    //ADDED BY DHRUB ON 11/01/2014 FOR DEDUCTED DATE 
                    //----------------------------------------------------
                    if (Convert.ToDouble(txtDeducteeTotal.Text) > 0)
                    {
                        //--------------------------------
                        //BLANK CHECK
                        //--------------------------------
                        if (dtService.J_IsBlankDateCheck(ref mskDeductedDate, J_ShowMessage.NO))
                        {
                            cmnService.J_UserMessage("Enter the Deducted Date of Tax deduction.");
                            if (lblMode.Text == J_Mode.Add)
                                mskDeductedDate.BackColor = Color.White;
                            else
                                mskDeductedDate.BackColor = Color.Honeydew;

                            lblDeductedDate.Visible = true;
                            mskDeductedDate.Visible = true;
                            lblDeductedDateFormat.Visible = true;
                            mskDeductedDate.Select();
                            return false;
                        }

                        //----------------------------------------------------
                        // VALID DATE CHECK 
                        //----------------------------------------------------
                        if (dtService.J_IsDateValid(mskDeductedDate) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the Date");
                            if (lblMode.Text == J_Mode.Add)
                                mskDeductedDate.BackColor = Color.White;
                            else
                                mskDeductedDate.BackColor = Color.Honeydew; 

                            lblDeductedDate.Visible = true;
                            mskDeductedDate.Visible = true;
                            lblDeductedDateFormat.Visible = true;
                            mskDeductedDate.Select();
                            return false;
                        }
                        //---------------------------------------------------------------------------
                        //DEDUCTED DATE LESS THAN QUARTER START DATE AND QUARTER END DATE WARNING
                        //---------------------------------------------------------------------------
                        if (dtService.J_ConvertToIntYYYYMMDD(mskDeductedDate.Text) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text)))
                        {
                            cmnService.J_UserMessage("Deducted Date cannot be of previous quarters.");
                            if (lblMode.Text == J_Mode.Add)
                                mskDeductedDate.BackColor = Color.White;
                            else
                                mskDeductedDate.BackColor = Color.Honeydew; 

                            lblDeductedDate.Visible = true;
                            mskDeductedDate.Visible = true;
                            lblDeductedDateFormat.Visible = true;
                            mskDeductedDate.Select();
                            return false;
                        }
                        //---------------------------------------------------------------------------
                        //DEDUCTED DATE GREATER THAN QUARTER END DATE AND QUARTER END DATE  WARNING
                        //---------------------------------------------------------------------------
                        if (dtService.J_ConvertToIntYYYYMMDD(mskDeductedDate.Text) > dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text)))
                        {
                            if (cmnService.J_UserMessage("Date of Tax deducted is beyond the quarter. - Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                if (lblMode.Text == J_Mode.Add)
                                    mskDeductedDate.BackColor = Color.White; 
                                else
                                    mskDeductedDate.BackColor = Color.Honeydew; 

                                lblDeductedDate.Visible = true;
                                mskDeductedDate.Visible = true;
                                lblDeductedDateFormat.Visible = true;
                                mskDeductedDate.Select();
                                return false;
                            }
                        }
                        //-----------------------------------------------------------------
                        //DEDUCTED DATE 
                        //-----------------------------------------------------------------
                        if ((Convert.ToDouble(txtDeducteeTotal.Text) == 0.00) && (mskDeductedDate.Text != ""))
                        {
                            cmnService.J_UserMessage("Deducted Date should be blank when Tax Deducted is zero.");
                            if (lblMode.Text == J_Mode.Add)
                                mskDeductedDate.BackColor = Color.White;
                            else
                                mskDeductedDate.BackColor = Color.Honeydew; 

                            lblDeductedDate.Visible = true;
                            mskDeductedDate.Visible = true;
                            lblDeductedDateFormat.Visible = true;
                            mskDeductedDate.Select();
                            return false;
                        }

                    }
                    //-------------------------------------------------------------------------------
                    //ADDED BY DHRUB ON 13/01/2014 CHECKING PAYMENT DATE NOT EQUAL TO DEDUCTED DATE  
                    //-------------------------------------------------------------------------------
                    if ((dtService.J_IsBlankDateCheck(ref mskDeductedDate, J_ShowMessage.NO) == false) && (dtService.J_IsBlankDateCheck(ref mskDeducteeDate, J_ShowMessage.NO) == false))
                    {
                        if (dtService.J_ConvertToIntYYYYMMDD(mskDeducteeDate.Text) != dtService.J_ConvertToIntYYYYMMDD(mskDeductedDate.Text))
                        {
                            if (cmnService.J_UserMessage("Are you sure Payment Date and Deducted Date for this record is different?", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                if (lblMode.Text == J_Mode.Add)
                                    mskDeductedDate.BackColor = Color.White;
                                else
                                    mskDeductedDate.BackColor = Color.Honeydew;

                                lblDeductedDate.Visible = true;
                                mskDeductedDate.Visible = true;
                                lblDeductedDateFormat.Visible = true;
                                mskDeducteeDate.Select();
                                return false;
                            }
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- AMOUNT OF PAYMENT
                    //-----------------------------------------------------------------------
                    if (Convert.ToDouble(cmnService.J_NumericData(txtDeducteeAmountOfPayment)) == 0)
                    {
                        cmnService.J_UserMessage("Amount of Payment - Cannot be Blank");
                        txtDeducteeAmountOfPayment.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- TAX DEPOSITED
                    //-----------------------------------------------------------------------
                    //Modified by Shrey Kejriwal on 24/02/2012
                    //if (rbnHigherRate.Checked == true)
                    if (Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex)) == 7)
                    {
                        if (Convert.ToDouble(cmnService.J_NumericData(txtDeducteeTaxDeposited)) == 0)
                        {
                            cmnService.J_UserMessage("Tax Deposited - Cannot be Blank when Higher rate is selected.");
                            txtDeducteeTaxDeposited.Select();
                            return false;
                        }
                    }

                    //-----------------------------------------------------------------------
                    // RATE CAN NOT BE ZERO IF INCOME TAX  IS PRESENT
                    //-----------------------------------------------------------------------
                    if (Convert.ToDouble(cmnService.J_NumericData(txtDeducteeIncometax)) != 0)
                    {
                        if (Convert.ToDouble(cmnService.J_NumericData(txtDeducteeRate)) == 0)
                        {
                            cmnService.J_UserMessage("Rate - Cannot be Blank for Tax greater than zero");
                            txtDeducteeRate.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- NON DEDUCTION FLAG
                    //-----------------------------------------------------------------------
                    //if (grpRemarks.Enabled == true)
                    //{
                        //Added by Shrey Kejriwal on 24/01/2012
                        //Checking if Higher Rate is selected for Invalid Pan entered
                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2010_11ID)
                        {
                            //if (txtDeducteePAN.Text == "PANNOTAVBL" && rbnHigherRate.Checked == false)
                            if (txtDeducteePAN.Text == "PANNOTAVBL" && Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex)) != 7 )
                            {
                                cmnService.J_UserMessage("For Deductees with Invalid PAN it is mandatory to deduct TDS under Higher Rate.");
                                cmbRemarks.Select();
                                return false;
                            }
                        }

                        //-----------------------------------------------------------------------
                        //Added by Shrey Kejriwal on 24/01/2012
                        //-----------------------------------------------------------------------
                        //Giving a warning message for selecting Higher rate for the valid PAN entered
                        //if (rbnHigherRate.Checked == true && txtDeducteePAN.Text != "PANNOTAVBL")
                        if (Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex)) == 7 && txtDeducteePAN.Text != "PANNOTAVBL")
                        {
                            if (cmnService.J_UserMessage("Are you sure you want to select Higher Rate for the Valid PAN entered", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                cmbRemarks.Select();
                                return false;
                            }
                        }
                        //if (rbnTransporter.Checked == true)
                        if (Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex)) == 8) 
                        {
                            // TRANSPORTER
                            if (strValidateSection != "194C")
                            {
                                cmnService.J_UserMessage("Transporter is allowed only under Section '194C'");
                                //rbnTransporter.Select();
                                cmbRemarks.Select();
                                return false;
                            }
                        }
                        //if (rbnSoftware.Checked == true)
                        if (Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex)) == 10)
                        {
                            // Checking Section
                            if (strValidateSection != "194J")
                            {
                                cmnService.J_UserMessage("Software vendor flag is allowed only under Section 194J");
                                //rbnNormal.Select();
                                cmbRemarks.Select();
                                return false;
                            }

                            //Checking FA Year

                            //Sofware flag allowed only on and after FA year 2012-13
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < T_FinancialYearID.F2012_13ID)
                            {
                                cmnService.J_UserMessage("Software vendor flag is applicable from FY 2012-13 onwards");
                                //rbnNormal.Select();
                                cmbRemarks.Select();
                                return false;
                            }
                        }
                        //else if (rbnNoDeduction.Checked == true)
                        else if (Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex)) == 6)
                        {
                            // SECTION NO.
                            if (strValidateSection != "194" &&
                                strValidateSection != "194A" &&
                                strValidateSection != "194EE" &&
                                strValidateSection != "193")
                            {
                                cmnService.J_UserMessage("No Deduction is allowed only under Section '194', '194A', '194EE' & '193'");
                                //rbnTransporter.Select();
                                return false;
                            }
                            // IF 'NO DEDUCTION' – RATE, INCOME TAX, SURCHARGE, CESS, TOTAL, TAX DEPOSITED ALL VALUE SHOULD BE 0.
                            if (Convert.ToDouble(txtDeducteeRate.Text) > 0)
                            {
                                cmnService.J_UserMessage("Rate should be 0 for <No Deduction>");
                                txtDeducteeRate.Select();
                                return false;
                            }
                            //
                            if (Convert.ToDouble(txtDeducteeIncometax.Text) > 0)
                            {
                                cmnService.J_UserMessage("Income tax should be 0 for <No Deduction>");
                                txtDeducteeIncometax.Select();
                                return false;
                            }
                            //
                            if (Convert.ToDouble(txtDeducteeSurcharge.Text) > 0)
                            {
                                cmnService.J_UserMessage("Surcharge should be 0 for <No Deduction>");
                                txtDeducteeSurcharge.Select();
                                return false;
                            }
                            //
                            if (Convert.ToDouble(txtDeducteeCess.Text) > 0)
                            {
                                cmnService.J_UserMessage("Cess should be 0 for <No Deduction>");
                                txtDeducteeCess.Select();
                                return false;
                            }
                            //
                            if (Convert.ToDouble(txtDeducteeTotal.Text) > 0)
                            {
                                cmnService.J_UserMessage("Total should be 0 for <No Deduction>");
                                txtDeducteeTotal.Select();
                                return false;
                            }
                            //
                            if (Convert.ToDouble(txtDeducteeTaxDeposited.Text) > 0)
                            {
                                cmnService.J_UserMessage("Tax Deposited should be 0 for <No Deduction>");
                                txtDeducteeTaxDeposited.Select();
                                return false;
                            }
                            //
                        }
                        //else if (rbnThresholdLimit.Checked == true)
                        else if (Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex)) == 9)
                        {
                            // ANIK GHOSH 2011-04-30
                            //// SECTION NO.
                            //if (txtViewChallanDetailsSection.Text == "194F")
                            //{
                            //    cmnService.J_UserMessage("Below Threshold Limit Reason is not allowed for Section '194F'");
                            //    //rbnTransporter.Select();
                            //    return false;
                            //}
                            //// IF 'NO DEDUCTION' – RATE, INCOME TAX, SURCHARGE, CESS, TOTAL, TAX DEPOSITED ALL VALUE SHOULD BE 0.
                            //if (Convert.ToDouble(txtDeducteeRate.Text) > 0)
                            //{
                            //    cmnService.J_UserMessage("Rate should be 0 for <Below Threshold Limit Reason>");
                            //    txtDeducteeRate.Select();
                            //    return false;
                            //}
                            ////
                            //if (Convert.ToDouble(txtDeducteeIncometax.Text) > 0)
                            //{
                            //    cmnService.J_UserMessage("Income tax should be 0 for <Below Threshold Limit Reason>");
                            //    txtDeducteeIncometax.Select();
                            //    return false;
                            //}
                            ////
                            //if (Convert.ToDouble(txtDeducteeSurcharge.Text) > 0)
                            //{
                            //    cmnService.J_UserMessage("Surcharge should be 0 for <Below Threshold Limit Reason>");
                            //    txtDeducteeSurcharge.Select();
                            //    return false;
                            //}
                            ////
                            //if (Convert.ToDouble(txtDeducteeCess.Text) > 0)
                            //{
                            //    cmnService.J_UserMessage("Cess should be 0 for <Below Threshold Limit Reason>");
                            //    txtDeducteeCess.Select();
                            //    return false;
                            //}
                            ////
                            //if (Convert.ToDouble(txtDeducteeTotal.Text) > 0)
                            //{
                            //    cmnService.J_UserMessage("Total should be 0 for <Below Threshold Limit Reason>");
                            //    txtDeducteeTotal.Select();
                            //    return false;
                            //}
                            ////
                            //if (Convert.ToDouble(txtDeducteeTaxDeposited.Text) > 0)
                            //{
                            //    cmnService.J_UserMessage("Tax Deposited should be 0 for <Below Threshold Limit Reason>");
                            //    txtDeducteeTaxDeposited.Select();
                            //    return false;
                            //}
                            ////
                        }
                    //}
                    //-----------------------------------------------------------------------
                    //-- TOTAL CHALLAN VALUE & TOTAL DEDUCTEE VALUE
                    //-----------------------------------------------------------------------
                    if ((cmnService.J_ReturnDoubleValue(txtViewDeducteeTotalTaxDeposited.Text) +
                        cmnService.J_ReturnDoubleValue(txtDeducteeTaxDeposited.Text) -
                        cmnService.J_ReturnDoubleValue(txtOldDeducteeTaxDeposited.Text)) > cmnService.J_ReturnDoubleValue(txtViewChallanDetailsTotalTax.Text))
                    {
                        cmnService.J_UserMessage("Total Deductee deposit can never be more than the Challan deposit");
                        txtDeducteeTaxDeposited.Select();
                        return false;
                    }

                    //----------------------------------------------------------
                    //-- DEDUCTED DATE
                    //----------------------------------------------------------

                    if (Convert.ToDouble(txtDeducteeTotal.Text) != 0)
                    {
                        //ADDED BY SHREY KEJRIWAL ON 26/03/2012

                        //BLANK CHECK
                        if (dtService.J_IsBlankDateCheck(ref mskDeductedDate, J_ShowMessage.NO))
                        {
                            cmnService.J_UserMessage("Date of Deduction cannot be blank");
                            mskDeductedDate.Select();
                            return false;
                        }

                        //VALID DATE CHECK
                        if (dtService.J_ConvertToIntYYYYMMDD(mskDeductedDate) < dtService.J_ConvertToIntYYYYMMDD(TdsMan.T_ReturnStartDateFinancialYear(cmbFinancialYear.Text)))
                        {
                            cmnService.J_UserMessage("Deducted date cannot be before the Financial Year : " + cmbFinancialYear.Text);
                            mskDeductedDate.Select();
                            return false;
                        }
                    }
                    //-- CERTIFICATE
                    if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                    {
                        if (txtCertificateNo.Text.Trim() == "")
                        {
                            strSQL = "SELECT CERTIFICATE_REQD FROM MST_REASON WHERE REASON_ID = " + Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex));
                            if (Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)) == "1")
                            {
                                cmnService.J_UserMessage("Certificate No. mandatory for Remarks : '" + cmbRemarks.Text + "'");
                                txtCertificateNo.Select();
                                return false;
                            }
                        }
                    }
                    //-- REMARKS
                    if (cmbRemarks.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Remarks cannot be blank");
                        cmbRemarks.Select();
                        return false;
                    }
                    //-- ANIK @ 2013/09/26 for FVU 4.0
                    strSQL = "SELECT TOT_TAX " +
                           "FROM  TRN_CHALLAN " +
                           "WHERE CHALLAN_ID = " + lngChallanID;
                    if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) == 0)
                    {
                        if (cmbRemarks.Text.Substring(0, 1) != "A" &&
                            cmbRemarks.Text.Substring(0, 1) != "B" &&
                                cmbRemarks.Text.Substring(0, 1) != "Y" &&
                                    cmbRemarks.Text.Substring(0, 1) != "S" &&
                                        cmbRemarks.Text.Substring(0, 1) != "T" &&
                                            cmbRemarks.Text.Substring(0, 1) != "Z")
                        {
                            cmnService.J_UserMessage("'" + cmbRemarks.Text.Trim() + "' Remarks not applicable for Nil challans/transfer vouchers");
                            return false;
                        }
                    }
                    //-------------------------------------------------------------------------------
                    //ADDED BY DHRUB ON 14/01/2014 
                    //-------------------------------------------------------------------------------
                    //WARNING MESSAGE : IF TAX DEPOSITED AMOUNT IS ZERO AND REMARKS IS NORMAL SELECTED 
                    //-------------------------------------------------------------------------------
                    if (cmbRemarks.SelectedIndex > 0)
                    {
                        if (Convert.ToDouble(txtDeducteeTotal.Text) == 0 && Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex)) == 12)
                        {
                            if (cmnService.J_UserMessage("Are you sure that there is no 'Remarks' for zero tax deduction?", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                cmbRemarks.Select();
                                return false;
                            }

                        }
                    }
                    //-----------------------------------------------------------------------

                    //
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtCertificateNo.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Certificate No. - '^' not allowed");
                        txtCertificateNo.Select();
                        return false;
                    }
                    ////-----------------------------------------------------------------------
                    ////-- DEDCUTEE EXIST WITH SAME VALID PAN
                    ////-----------------------------------------------------------------------
                    //if (txtDeducteePAN.Text != "PANNOTAVBL")
                    //{
                    //    string strSQLValidate = "SELECT DEDUCTEE_ID," +
                    //                       "            DEDUCTEE_NAME " +
                    //                       "     FROM   MST_DEDUCTEE " +
                    //                       "     WHERE  DEDUCTEE_PAN   = '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "' " +
                    //                       "     AND    DEDUCTEE_NAME <> '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "' " +
                    //                       "     ORDER BY DEDUCTEE_NAME";
                    //    if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQLValidate)) > 0)
                    //    {
                    //        if (cmnService.J_UserMessage("Deductee exists with the entered PAN - Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                    //        {
                    //            txtDeducteePAN.Select();
                    //            return false;
                    //        }
                    //    }
                    //}               
                    return true;
                }
                else if (lblSearchMode.Text == Tabbed_Mode.GenerateTDS)
                {
                    //-----------------------------------------------------------------------
                    //-- Total Challan
                    //-----------------------------------------------------------------------
                    if (cmnService.J_ReturnDoubleValue(txtTotalChallanRecords.Text) == 0)
                    {
                        cmnService.J_UserMessage("Total number of Challan should be at least 1");
                        BtnCancel.Select();
                        return false;
                    }

                    // AUTOMATIC CSI FILE DOWNLOAD
                    if (chkCSIFileDownload.Checked == true)
                    {
                        //-----------------------------------------------------------------------
                        //-- DATE
                        //-----------------------------------------------------------------------
                        if (dtService.J_IsBlankDateCheck(ref mskViewCSIFileDownloadFrom, "CSI File Download From Date - Cannot be Blank") == true)
                        {
                            mskViewCSIFileDownloadFrom.Select();
                            return false;
                        }
                        //-----------------------------------------------------------------------
                        if (dtService.J_IsBlankDateCheck(ref mskViewCSIFileDownloadTo, "CSI File Download To Date - Cannot be Blank") == true)
                        {
                            mskViewCSIFileDownloadTo.Select();
                            return false;
                        }
                        //----------------------------------------------------------
                        //-- VALID DATE CHECK
                        //----------------------------------------------------------
                        if (dtService.J_IsDateValid(mskViewCSIFileDownloadFrom) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the CSI File Download From Date");
                            mskViewCSIFileDownloadFrom.Select();
                            return false;
                        }
                        //----------------------------------------------------------
                        if (dtService.J_IsDateValid(mskViewCSIFileDownloadTo) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the CSI File Download To Date");
                            mskViewCSIFileDownloadTo.Select();
                            return false;
                        }

                        // FROM DATE STARTING FROM 2005
                        if (dtService.J_ConvertToIntYYYYMMDD(mskViewCSIFileDownloadFrom.Text) < 20050101)
                        {
                            cmnService.J_UserMessage("CSI File Download Date [From Date should not be less than 01/01/2005]!!");
                            mskViewCSIFileDownloadFrom.Select();
                            return false;
                        }
                        // FROM DATE & TO DATE STARTING FROM 2005
                        if (dtService.J_ConvertToIntYYYYMMDD(mskViewCSIFileDownloadTo.Text) < 20050101)
                        {
                            cmnService.J_UserMessage("CSI File Download Date [To Date should not be less than 01/01/2005]!!");
                            mskViewCSIFileDownloadTo.Select();
                            return false;
                        }

                        // FROM DATE GREATER THAN TO DATE
                        if (dtService.J_ConvertToIntYYYYMMDD(mskViewCSIFileDownloadFrom.Text) > dtService.J_ConvertToIntYYYYMMDD(mskViewCSIFileDownloadTo.Text))
                        {
                            cmnService.J_UserMessage("CSI File Download Date [From Date should not be greater than To Date]!!");
                            mskViewCSIFileDownloadFrom.Select();
                            return false;
                        }

                        // FROM DATE & TO DATE DURATION 15 MONTHS
                        if (TdsMan.T_DateDiff(T_DATE_DIFF.MONTH, dtService.J_ConvertddMMyyyy(mskViewCSIFileDownloadFrom), dtService.J_ConvertddMMyyyy(mskViewCSIFileDownloadTo)) > 15)
                        {
                            cmnService.J_UserMessage("Period selected should be within 15 months");
                            mskViewCSIFileDownloadFrom.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- FVU PATH
                    //-----------------------------------------------------------------------
                    //if (txtViewFVUPath.Text.Trim() == "")
                    //{
                    //    cmnService.J_UserMessage("FVU Path can never be blank");
                    //    btnFVUPathModify.Select();
                    //    return false;
                    //}
                    //-----------------------------------------------------------------------
                    //if ( this.J_IsProcessOpen(txtViewFVUPath.Text) == true)
                    //{
                    //    MessageBox.Show("Application is running.\nPlease close the application");
                    //    btnFVUPathModify.Select();
                    //    return false;
                    //}
                    ////-----------------------------------------------------------------------
                    //if (File.Exists(txtViewFVUPath.Text) == false)
                    //{
                    //    MessageBox.Show("Selected file does not exist");
                    //    btnFVUPathModify.Select();
                    //    return false;
                    //}
                    //-----------------------------------------------------------------------
                    //-- INPUT FILE PATH
                    //-----------------------------------------------------------------------
                    if (txtTextFilePath.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Input File Path can never be blank");
                        BtnSort.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    if (TdsMan.J_IsProcessOpen(txtTextFilePath.Text) == true)
                    {
                        MessageBox.Show("Application is running.\nPlease close the application");
                        BtnSort.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    if (File.Exists(txtTextFilePath.Text) == false)
                    {
                        MessageBox.Show("The file does not exist");
                        BtnSort.Select();
                        return false;
                    }
                    return true;
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

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {
                //--------------------------------------------
                string strNonDeductionFlag = "";
                string strDEDUCTED_DATE = "";
                double dblSumValues = 0;
                int intBookEntry = 0;
                int intCashBookEntry = 0;
                int intMinorID = 0;
                int intSectionID = 0;
                //--------------------------------------------
                switch (lblMode.Text)
                {
                    #region Add
                    case J_Mode.Add:
                        //*****  For Insert
                        #region Challan
                        if (lblSearchMode.Text == Tabbed_Mode.Challan)
                        {
                            //-----------------------------------------------------------
                            if (ValidateFields() == false) return;
                            //-----------------------------------------------------------
                            if (cmnService.J_SaveConfirmationMessage(ref cmbCompany) == true) return;
                            //-----------------------------------------------------------
                            dmlService.J_BeginTransaction();
                            //-----------------------------------------------------------
                            //Commented by Indrajit on 22-02-2013 to bypass company common information
                            //if (lngBasicInfoID == 0)
                            //{
                            //    if (TdsMan.T_GenerateBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                            //                            cmbQuarter.Text,
                            //                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                            //                            strFormNo) == true)
                            //    {
                                    //-----------------------------------------------
                                    lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        cmbQuarter.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        strFormNo);
                                    //-----------------------------------------------
                            //        InsertCompanyBasicInfo(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), lngBasicInfoID);
                            //    }
                            //    else
                            //        return;
                            //}

                            //
                            if (cmbSection.SelectedIndex > 0)
                                intSectionID = Convert.ToInt32(Support.GetItemData(cmbSection, cmbSection.SelectedIndex));
                            //
                            if (chkBookEntry.Checked == true)
                                intBookEntry = 1;
                            //------------------------------------------------------
                            //MODIFIED BY DHRUB FOR WHEN BOOK ENTRY NOT CHECKED
                            //------------------------------------------------------
                            if (chkBookEntry.Checked == false)
                            {
                                if (cmbMinorHead.SelectedIndex > 0)
                                    intMinorID = Convert.ToInt32(Support.GetItemData(cmbMinorHead, cmbMinorHead.SelectedIndex));
                            }
                            //-----------------------------------------------------------
                            strSQL = "INSERT INTO TRN_CHALLAN (" +
                                     "            BASIC_INFO_ID," +
                                     "            SL_NO," +
                                     "            SECTION_ID," +
                                     "            DEPOSIT_DATE," +
                                     "            BSR_CODE," +
                                     "            CHALLAN_NO," +
                                     "            TRANSFER_VOUCHER_NO," +
                                     "            CHEQUE_NO," +
                                     "            TDS," +
                                     "            SURCHARGE," +
                                     "            EDUCATION_CESS," +
                                     "            INTEREST," +
                                     "            OTHERS," +
                                     "            TOT_TAX," +
                                     "            INTEREST_ALLOCATED," +
                                     "            OTHERS_ALLOCATED," +
                                     "            REMARKS," +
                                     "            BOOK_ENTRY," +
                                     "            LATE_FEE," +
                                     "            MINOR_HEAD_ID) " +
                                     "     VALUES(" + lngBasicInfoID + "," +
                                     "            " + Convert.ToInt32(txtChallanSrlNo.Text.Trim()) + "," +
                                     "            " + intSectionID + "," +
                                     "            " + cmnService.J_DateOperator() +  dtService.J_ConvertMMddyyyy(mskDateOfPayment) + cmnService.J_DateOperator() + "," +
                                     "           '" + cmnService.J_ReplaceQuote(txtBSRCode.Text.Trim()) + "'," +
                                     "           '" + cmnService.J_ReplaceQuote(txtChallanNo.Text.Trim()) + "'," +
                                     "           '" + cmnService.J_ReplaceQuote(txtTransferVoucherNo.Text.Trim()) + "'," +
                                     "           '" + cmnService.J_ReplaceQuote(txtChequeNo.Text.Trim()) + "'," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtTDS.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtSurcharge.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtEducationCess.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtInterests.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtOthers.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtTotalTax.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtInterestAllocated.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtOthersAllocated.Text) + "," +
                                     "           '" + cmnService.J_ReplaceQuote(txtRemarks.Text.Trim()) + "', " +
                                     "            " + intBookEntry + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtFee.Text) + "," +
                                     "            " + intMinorID + ")";
                            //-----------------------------------------------------------
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            //-- UPDATE LAST WORKED
                            TdsMan.T_UpdateLastWorked(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), cmbQuarter.Text, strFormNo, Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
                            //-----------------------------------------------------------
                            lngChallanID = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "TRN_CHALLAN", "CHALLAN_ID", "BASIC_INFO_ID = " + lngBasicInfoID);
                            if (lngChallanID == 0)
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                            //-----------------------------------------------------------
                            dmlService.J_Commit();
                            cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                            //-----------------------------------------------------------
                            ClearControls();
                            //-----------------------------------------------------------
                            //-- Generate Challan Srl No.
                            //txtChallanSrlNo.Text = Convert.ToString(TdsMan.T_ReturnSrlNo(lngBasicInfoID,T_SRL_NO.CHALLAN));
                            txtChallanSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_CHALLAN", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
                            //--
                            LoadChallanGrid(lngBasicInfoID);
                            //--
                            dmlService.J_setGridPosition(ref dgcViewChallan, dsetGridClone, "CHALLAN_ID", lngChallanID);
                            //--
                            //-----------------------------------------------------------
                            //MODIFIED BY DHRUB ON 09/01/2014
                            //-----------------------------------------------------------
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) <= T_FinancialYearID.F2012_13ID)
                                cmbSection.Select();
                            else
                                if(chkBookEntry.Checked == true)
                                    txtTransferVoucherNo.Focus();
                                else
                                    txtChallanNo.Focus();
                            //-----------------------------------------------------------
                            //-----------------------------------------------------------
                        }
                        #endregion
                        //--
                        #region Deductee
                        else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
                        {
                            dmlService.J_BeginTransaction();
                            //-----------------------------------------------------------
                            //if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                            //{
                            //    //--
                            //    strSQL = "SELECT COUNT(DEDUCTEE_DETAIL_ID) + 1 AS MAX_DEDUCTEE_DETAIL_COUNT FROM TRN_DEDUCTEE_DETAILS";
                            //    if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > TDSMAN.Classes.TDSMAN.T_pMaxDeducteeDetailsCount)
                            //    {
                            //        cmnService.J_UserMessage("Only '" + TDSMAN.Classes.TDSMAN.T_pMaxDeducteeDetailsCount + "' deductee entry is allowed in Trial Version");
                            //        BtnSort.Select();
                            //        return;
                            //    }
                            //    //--
                            //}
                            //----------------------------------------------------------
                            //--###########################
                            if (TdsMan.LimitEditions(lngBasicInfoID, false, true, "TRN_DEDUCTEE_DETAILS", "BASIC_INFO_ID", "", "") == false)
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                            //--###########################                    
                            //-----------------------------------------------------------
                            if (ValidateFields() == false)
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                            //-----------------------------------------------------------
                            if (cmnService.J_SaveConfirmationMessage(ref cmbCompany) == true)
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                            //-----------------------------------------------------------
                            // DEDUCTED DATE
                            // modified by shrey as deducted date should be null if total deduction is 0
                            if (cmnService.J_ReturnDoubleValue(txtDeducteeTotal.Text) == 0)
                                strDEDUCTED_DATE = "NULL";
                            else
                                strDEDUCTED_DATE = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDeductedDate) + cmnService.J_DateOperator();
                            //-----------------------------------------------------------
                            //if (txtDeducteePAN.Text.ToUpper() == "PANNOTAVBL")
                            //    strNonDeductionFlag = "C";
                            //else
                            //{
                                //if (rbnLowerNoDeduction.Checked == true)
                                //    strNonDeductionFlag = "A";
                                //else if (rbnNoDeduction.Checked == true)
                                //    strNonDeductionFlag = "B";
                                //else if (rbnTransporter.Checked == true)
                                //    strNonDeductionFlag = "T";
                                //else if (rbnHigherRate.Checked == true)
                                //    strNonDeductionFlag = "C";
                                //else if (rbnNormal.Checked == true)
                                //    strNonDeductionFlag = "";
                                //else if (rbnThresholdLimit.Checked == true)
                                //    strNonDeductionFlag = "Y";
                                //else if (rbnSoftware.Checked == true)
                                //    strNonDeductionFlag = "S";
                            strSQL = "SELECT REASON FROM MST_REASON WHERE REASON_ID = " + Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex));
                            strNonDeductionFlag = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                            //}
                            //-----------------------------------------------------------
                            //-- GET DEDUCTEE_ID
                            strSQL = "SELECT DEDUCTEE_ID " +
                                "     FROM   MST_DEDUCTEE " +
                                "     WHERE  DEDUCTEE_NAME ='" + cmnService.J_ReplaceQuote(txtDeducteeName.Text.Trim()) + "'" +
                                "     AND    DEDUCTEE_PAN  ='" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text.Trim()) + "'";
                            //lngDeducteeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL));

                            //Added by Ripan on 22-02-2013
                            DMLService dml = new DMLService();
                            lngDeducteeID = cmnService.J_NullToZero(dml.J_ExecSqlReturnScalar(strSQL));
                            //--
                            //--
                            if (lngDeducteeID == 0)
                            {
                                strSQL = "INSERT INTO MST_DEDUCTEE " +
                                    "                 (DEDUCTEE_NAME," +
                                    "                  DEDUCTEE_PAN," +
                                    "                  DEDUCTEE_CODE) " +
                                    "     VALUES      ('" + cmnService.J_ReplaceQuote(txtDeducteeName.Text.Trim()) + "'," +
                                    "                  '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text.Trim()) + "'," +
                                    "                  '" + cmnService.J_ReplaceQuote(cmbDeducteeCode.Text.Trim().Substring(0,2)) + "')";
                                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                {
                                    dmlService.J_Rollback(); 
                                    return;
                                }
                                //--
                                lngDeducteeID = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "MST_DEDUCTEE", "DEDUCTEE_ID");
                            }
                            //
                            if (chkCashBookEntry.Checked == true)
                                intCashBookEntry = 1;
                            //
                            if (cmnService.J_ReturnInt32Value(lblViewChallanDetailsSectionId.Text) >0)
                                intSectionID = cmnService.J_ReturnInt32Value(lblViewChallanDetailsSectionId.Text);
                            else
                            {
                                if (cmbDDSection.SelectedIndex > 0)
                                    intSectionID = Convert.ToInt32(Support.GetItemData(cmbDDSection, cmbDDSection.SelectedIndex));                            
                            }
                            //-----------------------------------------------------------
                            strSQL = "INSERT INTO TRN_DEDUCTEE_DETAILS " +
                                     "           (CHALLAN_ID," +
                                     "            BASIC_INFO_ID," +
                                     "            SL_NO," +
                                     "            PARTY_ID," +
                                     "            PAYMENT_DATE," +
                                     "            DEDUCTED_DATE," +
                                     "            PAYMENT_AMOUNT," +
                                     "            RATE," +
                                     "            TAX_AMOUNT," +
                                     "            SURCHARGE_AMOUNT," +
                                     "            CESS_AMOUNT," +
                                     "            TOTAL_AMOUNT," +
                                     "            TAX_DEPOSITED_AMOUNT," +
                                     "            NON_DEDUCTION_FLAG," +
                                     "            CASH_BOOK_ENTRY," +
                                     "            REASON_ID," +
                                     "            SECTION_ID," +
                                     "            CERTIFICATE_NO) " +
                                     "     VALUES(" + lngChallanID + "," +
                                     "            " + lngBasicInfoID + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtDeducteeSrlNo.Text) + "," +
                                     "            " + lngDeducteeID + "," +
                                     "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDeducteeDate) + cmnService.J_DateOperator() + "," +
                                     "            " + strDEDUCTED_DATE + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtDeducteeAmountOfPayment.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtDeducteeRate.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtDeducteeIncometax.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtDeducteeSurcharge.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtDeducteeCess.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtDeducteeTotal.Text) + "," +
                                     "            " + cmnService.J_ReturnDoubleValue(txtDeducteeTaxDeposited.Text) + "," +
                                     "           '" + cmnService.J_ReplaceQuote(strNonDeductionFlag) + "'," + 
                                     "            " + intCashBookEntry + "," +
                                     "            " + Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex)) + "," +
                                     "            " + intSectionID + "," +
                                     "           '" + cmnService.J_ReplaceQuote(txtCertificateNo.Text.Trim()) + "')";
                            //-----------------------------------------------------------
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();    //Added by Indrajit on 13-02-2013
                                return;
                            }
                          
                            //-----------------------------------------------------------
                            // UPDATE TDS
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_TDS = CTRL_TDS + " + cmnService.J_ReturnDoubleValue(txtDeducteeIncometax.Text) + " - " + cmnService.J_ReturnDoubleValue(txtOldDeducteeIncometax.Text) + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            // UPDATE SURCHARGE
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_SURCHARGE = CTRL_SURCHARGE + " + cmnService.J_ReturnDoubleValue(txtDeducteeSurcharge.Text) + " - " + cmnService.J_ReturnDoubleValue(txtOldDeducteeSurcharge.Text) + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            // UPDATE CESS
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_EDU_CESS = CTRL_EDU_CESS + " + cmnService.J_ReturnDoubleValue(txtDeducteeCess.Text) + " - " + cmnService.J_ReturnDoubleValue(txtOldDeducteeCess.Text) + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            // UPDATE TOTAL
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_TOT = CTRL_TOT + " + cmnService.J_ReturnDoubleValue(txtDeducteeTotal.Text) + " - " + cmnService.J_ReturnDoubleValue(txtOldDeducteeTotal.Text) + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            // UPDATE CHALLAN TOTAL
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_TOT_TAX = CTRL_TOT_TAX + " + cmnService.J_ReturnDoubleValue(txtDeducteeTaxDeposited.Text) + " - " + cmnService.J_ReturnDoubleValue(txtOldDeducteeTaxDeposited.Text) + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            //-- UPDATE LAST WORKED
                            TdsMan.T_UpdateLastWorked(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), cmbQuarter.Text, strFormNo, Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
                            //-----------------------------------------------------------
                            lngDeducteeDetailID = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "TRN_DEDUCTEE_DETAILS", "DEDUCTEE_DETAIL_ID", "BASIC_INFO_ID = " + lngBasicInfoID);
                            if (lngDeducteeDetailID == 0)
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                            //-----------------------------------------------------------
                            dmlService.J_Commit();
                            cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                            //-----------------------------------------------------------
                            ClearControls();
                            //-- Generate Deductee Srl No.
                            //txtDeducteeSrlNo.Text = Convert.ToString(TdsMan.T_ReturnSrlNo(lngChallanDetailID, T_SRL_NO.DEDUCTEE));
                            txtDeducteeSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_DEDUCTEE_DETAILS", "SL_NO", "CHALLAN_ID = " + lngChallanID + "") + 1);
                            txtViewDeducteeTotalTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(dmlService.J_ExecSqlReturnScalar("SELECT SUM(CTRL_TOT_TAX) AS CTRL_TOT_TAX FROM TRN_CHALLAN WHERE CHALLAN_ID = " + lngChallanID)));
                            //-----------------------------------------------------------
                            LoadDeducteeGrid(lngChallanID);
                            //--
                            dmlService.J_setGridPosition(ref dgcViewDeductee, dsetGridClone, "DEDUCTEE_DETAIL_ID", lngDeducteeDetailID);
                            //--
                            txtDeducteeName.Select();
                            //-----------------------------------------------------------
                        }
                        #endregion
                        //else if (lblSearchMode.Text == Tabbed_Mode.GenerateTDS)
                        //{
                        //}
                        break;
                    #endregion 

                    #region Edit
                    case J_Mode.Edit:
                        ////*****  For Modify
                        if (lblSearchMode.Text == Tabbed_Mode.Challan)
                        {
                            //-----------------------------------------------------------
                            if (ValidateFields() == false) return;
                            //-----------------------------------------------------------
                            if (cmnService.J_SaveConfirmationMessage(ref cmbCompany) == true) return;
                            //-----------------------------------------------------------
                            dmlService.J_BeginTransaction();
                            //-----------------------------------------------------------
                            //-----------------------------------------------------------
                            //BLOCKED BY DHRUB ON 07/01/2014 FOR ADD THE FEE
                            //strSQL = "SELECT COUNT(*) FROM TRN_CHALLAN " +
                            //    "     WHERE CTRL_TOT_TAX > " + ((cmnService.J_ReturnDoubleValue(txtTotalTax.Text)) - 
                            //                                (cmnService.J_ReturnDoubleValue(txtInterestAllocated.Text)) - 
                            //                                (cmnService.J_ReturnDoubleValue(txtOthersAllocated.Text))) + " " +
                            //    "     AND   CHALLAN_ID          =  " + lngChallanID;
                            //-----------------------------------------------------------
                            //-----------------------------------------------------------


                            //-----------------------------------------------------------------------
                            //-- CHALLAN NO. VALIDATION ADDED BY DHRUB ON 07/01/2014 FOR DEDUCTION OF FEE
                            //-----------------------------------------------------------------------
                            if (chkBookEntry.Checked == false)
                            {
                                strSQL = "SELECT COUNT(*) FROM TRN_CHALLAN " +
                                "     WHERE CTRL_TOT_TAX > " + ((cmnService.J_ReturnDoubleValue(txtTotalTax.Text)) -
                                                            (cmnService.J_ReturnDoubleValue(txtInterestAllocated.Text)) -
                                                            (cmnService.J_ReturnDoubleValue(txtFee.Text)) -
                                                            (cmnService.J_ReturnDoubleValue(txtOthersAllocated.Text))) + " " +
                                "     AND   CHALLAN_ID          =  " + lngChallanID;
                            }
                            //-------------------------------------------------------------------------------
                            //-------------------------------------------------------------------------------
                            //-- TRANSFER VOUCHER NO. ADDED BY DHRUB ON 07/01/2014 FOR NON DEDUCTION OF FEE
                            //-------------------------------------------------------------------------------
                            if (chkBookEntry.Checked == true)
                            {
                                strSQL = "SELECT COUNT(*) FROM TRN_CHALLAN " +
                                "     WHERE CTRL_TOT_TAX > " + ((cmnService.J_ReturnDoubleValue(txtTotalTax.Text)) -
                                                            (cmnService.J_ReturnDoubleValue(txtInterestAllocated.Text)) -
                                                            (cmnService.J_ReturnDoubleValue(txtOthersAllocated.Text))) + " " +
                                "     AND   CHALLAN_ID          =  " + lngChallanID;
                            }
                            //-----------------------------------------------------------------
                            if (Convert.ToDouble(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
                            {
                                cmnService.J_UserMessage("Challan total can not be less than Deductee total");
                                txtTotalTax.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            //
                            //
                            if (cmbSection.SelectedIndex > 0)
                                intSectionID = Convert.ToInt32(Support.GetItemData(cmbSection, cmbSection.SelectedIndex));
                            //
                            if (chkBookEntry.Checked == true)
                                intBookEntry = 1;
                            //
                            //------------------------------------------------------
                            //MODIFIED BY DHRUB FOR WHEN BOOK ENTRY NOT CHECKED
                            //------------------------------------------------------
                            if (chkBookEntry.Checked == false)
                            {
                                if (cmbMinorHead.SelectedIndex > 0)
                                    intMinorID = Convert.ToInt32(Support.GetItemData(cmbMinorHead, cmbMinorHead.SelectedIndex));
                            }
                            //-----------------------------------------------------------
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            SECTION_ID          =  " + intSectionID + "," +
                                "            DEPOSIT_DATE        =  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDateOfPayment) + cmnService.J_DateOperator() + "," +
                                "            BSR_CODE            = '" + cmnService.J_ReplaceQuote(txtBSRCode.Text.Trim()) + "'," +
                                "            CHALLAN_NO          = '" + cmnService.J_ReplaceQuote(txtChallanNo.Text.Trim()) + "'," +
                                "            TRANSFER_VOUCHER_NO = '" + cmnService.J_ReplaceQuote(txtTransferVoucherNo.Text.Trim()) + "'," +
                                "            CHEQUE_NO           = '" + cmnService.J_ReplaceQuote(txtChequeNo.Text.Trim()) + "'," +
                                "            TDS                 =  " + cmnService.J_ReturnDoubleValue(txtTDS.Text) + "," +
                                "            SURCHARGE           =  " + cmnService.J_ReturnDoubleValue(txtSurcharge.Text) + "," +
                                "            EDUCATION_CESS      =  " + cmnService.J_ReturnDoubleValue(txtEducationCess.Text) + "," +
                                "            INTEREST            =  " + cmnService.J_ReturnDoubleValue(txtInterests.Text) + "," +
                                "            OTHERS              =  " + cmnService.J_ReturnDoubleValue(txtOthers.Text) + "," +
                                "            TOT_TAX             =  " + cmnService.J_ReturnDoubleValue(txtTotalTax.Text) + "," +
                                "            INTEREST_ALLOCATED  =  " + cmnService.J_ReturnDoubleValue(txtInterestAllocated.Text) + "," +
                                "            OTHERS_ALLOCATED    =  " + cmnService.J_ReturnDoubleValue(txtOthersAllocated.Text) + "," +
                                "            REMARKS             = '" + cmnService.J_ReplaceQuote(txtRemarks.Text.Trim()) + "'," +
                                "            BOOK_ENTRY          =  " + intBookEntry + "," +
                                "            LATE_FEE            =  " + cmnService.J_ReturnDoubleValue(txtFee.Text) + "," +
                                "            MINOR_HEAD_ID       =  " + intMinorID + " " +
                                "     WHERE  CHALLAN_ID          =  " + lngChallanID + "" ;
                            //-----------------------------------------------------------
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            //--
                            if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) < T_FinancialYearID.F2013_14ID)
                            {
                                // UPDATE SECTION
                                strSQL = "UPDATE TRN_DEDUCTEE_DETAILS SET " +
                                    "            SECTION_ID =  " + intSectionID + " " +
                                    "     WHERE  CHALLAN_ID = " + lngChallanID;
                                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                {
                                    cmbSection.Select();
                                    dmlService.J_Rollback();
                                    return;
                                }
                            }
                            //-- UPDATE LAST WORKED
                            TdsMan.T_UpdateLastWorked(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), cmbQuarter.Text, strFormNo, Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
                            //-----------------------------------------------------------
                            //lngChallanID = dmlService.J_ReturnMaxValue("TRN_CHALLAN", "CHALLAN_ID");
                            //if (lngChallanID == 0) return;
                            //-----------------------------------------------------------
                            dmlService.J_Commit();
                            cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                            //-----------------------------------------------------------
                            ClearControls();
                            //-----------------------------------------------------------
                            lblMode.Text = J_Mode.Add;
                            lblMode.ForeColor = Color.GreenYellow;
                            //--
                            btnChallanDeducteeEntry.Enabled = true;
                            btnChallanDeducteeEntry.BackColor = Color.Lavender;
                            //
                            BtnEdit.Enabled = true;
                            BtnEdit.BackColor = Color.Lavender;
                            //--
                            BtnDelete.Enabled = true;
                            BtnDelete.BackColor = Color.Lavender;
                            //--
                            BtnSearch.Enabled = true;
                            BtnSearch.BackColor = Color.Lavender;
                            //--
                            BtnRefresh.Enabled = true;
                            BtnRefresh.BackColor = Color.Lavender;
                            //--
                            lblInterestAllocated.Visible = false;
                            txtInterestAllocated.Visible = false;
                            lblOthersAllocated.Visible = false;
                            txtOthersAllocated.Visible = false;
                            //lblRemarks.Location = new Point(14, 334);
                            //txtRemarks.Location = new Point(74, 331);    
                            //--
                            BackgroundColorChangeChallan(lblMode.Text);
                            //-- Generate Challan Srl No.
                            txtChallanSrlNo.Text = Convert.ToString(TdsMan.T_ReturnSrlNo(lngBasicInfoID,T_SRL_NO.CHALLAN));
                            //--
                            LoadChallanGrid(lngBasicInfoID);
                            //--
                            dmlService.J_setGridPosition(ref dgcViewChallan, dsetGridClone, "CHALLAN_ID", lngChallanID);
                            //--
                            ControlSummaryVisible(false, false, false, false, false, false, false);
                            //--
                            //-----------------------------------------------------------
                            //MODIFIED BY DHRUB ON 09/01/2014
                            //-----------------------------------------------------------
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) <= T_FinancialYearID.F2012_13ID)
                                cmbSection.Select();
                            else
                                if (chkBookEntry.Checked == true)
                                    txtTransferVoucherNo.Focus();
                                else
                                    txtChallanNo.Focus();
                            //-----------------------------------------------------------
                        }
                        else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
                        {
                            //-----------------------------------------------------------
                            if (ValidateFields() == false) return;
                            //-----------------------------------------------------------
                            if (cmnService.J_SaveConfirmationMessage(ref cmbCompany) == true) return;
                            //-----------------------------------------------------------
                            // DEDUCTED DATE
                            
                            //COMMENTED BY SHREY KEJRIWAL ON 26/03/2012

                            // modified by shrey as deducted date should be null if total deduction is 0
                            //if (cmnService.J_ReturnDoubleValue(txtDeducteeTotal.Text) == 0)
                            //    strDEDUCTED_DATE = "NULL";
                            //else
                            //    strDEDUCTED_DATE = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDeducteeDate) + cmnService.J_DateOperator();


                            //ADDED BY SHREY KEJRIWAL ON 26/03/2012

                            //SAVING DEDUCTED DATE FROM THE DATA PROVIDED

                            if(dtService.J_IsBlankDateCheck(ref mskDeductedDate, J_ShowMessage.NO))
                                strDEDUCTED_DATE = "NULL";
                            else
                                strDEDUCTED_DATE = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDeductedDate) + cmnService.J_DateOperator();

                            //-----------------------------------------------------------
                            //if (txtDeducteePAN.Text == "PANNOTAVBL")
                            //    strNonDeductionFlag = "C";
                            //else
                            //{
                                //if (rbnLowerNoDeduction.Checked == true)
                                //    strNonDeductionFlag = "A";
                                //else if (rbnNoDeduction.Checked == true)
                                //    strNonDeductionFlag = "B";
                                //else if (rbnTransporter.Checked == true)
                                //    strNonDeductionFlag = "T";
                                //else if (rbnHigherRate.Checked == true)
                                //    strNonDeductionFlag = "C";
                                //else if (rbnNormal.Checked == true)
                                //    strNonDeductionFlag = "";
                                //else if (rbnThresholdLimit.Checked == true)
                                //    strNonDeductionFlag = "Y";
                                //else if (rbnSoftware.Checked == true)
                                //    strNonDeductionFlag = "S";
                            strSQL = "SELECT REASON FROM MST_REASON WHERE REASON_ID = " + Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex));
                            strNonDeductionFlag = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));                            
                            //}
                            //-----------------------------------------------------------
                            dmlService.J_BeginTransaction();
                            //-----------------------------------------------------------
                            //-- GET DEDUCTEE_ID
                            //strSQL = "SELECT DEDUCTEE_ID " +
                            //    "     FROM   MST_DEDUCTEE " +
                            //    "     WHERE  DEDUCTEE_PAN <> 'PANNOTAVBL'" +
                            //    "     AND    DEDUCTEE_PAN  = '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "'";
                            //lngDeducteeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL));
                            //if (lngDeducteeID > 0)
                            //{
                            //    if (cmnService.J_UserMessage("Press 'YES' to update the current Deductee & 'NO' to treat it as new entry - Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                            //    {
                            //        lngDeducteeID = 0;
                            //        strSQL = "SELECT DEDUCTEE_ID " +
                            //            "     FROM   MST_DEDUCTEE " +
                            //            "     WHERE  DEDUCTEE_NAME ='" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "'" +
                            //            "     AND    DEDUCTEE_PAN  ='" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "'";
                            //        lngDeducteeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL));
                            //        //--
                            //        if (lngDeducteeID == 0)
                            //        {
                            //            strSQL = "INSERT INTO MST_DEDUCTEE " +
                            //                "                 (DEDUCTEE_NAME," +
                            //                "                  DEDUCTEE_PAN," +
                            //                "                  DEDUCTEE_CODE) " +
                            //                "     VALUES      ('" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "'," +
                            //                "                  '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "'," +
                            //                "                  '" + cmnService.J_ReplaceQuote(cmbDeducteeCode.Text) + "')";
                            //            if (dmlService.J_ExecSql(strSQL) == false)
                            //                return;
                            //            //--
                            //            lngDeducteeID = dmlService.J_ReturnMaxValue("MST_DEDUCTEE", "DEDUCTEE_ID");
                            //        }
                            //    }
                            //}
                            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            strSQL = "SELECT DEDUCTEE_ID " +
                                "     FROM   MST_DEDUCTEE " +
                                "     WHERE  DEDUCTEE_NAME ='" + cmnService.J_ReplaceQuote(txtDeducteeName.Text.Trim()) + "'" +
                                "     AND    DEDUCTEE_PAN  ='" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "'";
                            
                            //Modifided by Indrajit on 23-02-2013
                            //lngDeducteeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
                            DMLService dml = new DMLService();
                            lngDeducteeID = cmnService.J_NullToZero(dml.J_ExecSqlReturnScalar(strSQL));
                            //--

                            if (lngDeducteeID == 0)
                            {
                                if (cmnService.J_UserMessage("New Deductee will be created - Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    txtDeducteeName.Select();
                                    dmlService.J_Rollback();
                                    return;
                                }
                                strSQL = "INSERT INTO MST_DEDUCTEE " +
                                    "                 (DEDUCTEE_NAME," +
                                    "                  DEDUCTEE_PAN," +
                                    "                  DEDUCTEE_CODE) " +
                                    "     VALUES      ('" + cmnService.J_ReplaceQuote(txtDeducteeName.Text.Trim()) + "'," +
                                    "                  '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "'," +
                                    "                  '" + cmnService.J_ReplaceQuote(cmbDeducteeCode.Text.Trim().Substring(0, 2)) + "')";
                                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                {
                                    dmlService.J_Rollback();
                                    return;
                                }
                                //--
                                lngDeducteeID = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "MST_DEDUCTEE", "DEDUCTEE_ID");
                            }
                            else
                            {
                                strSQL = "UPDATE MST_DEDUCTEE " +
                                       "  SET    DEDUCTEE_CODE = '" + cmnService.J_ReplaceQuote(cmbDeducteeCode.Text.Trim().Substring(0, 2)) + "' " +
                                       "  WHERE  DEDUCTEE_ID   = " + lngDeducteeID;
                                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                {
                                    dmlService.J_Rollback();
                                    return;
                                }
                            }
                            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            //
                            if (cmnService.J_ReturnInt32Value(lblViewChallanDetailsSectionId.Text) > 0)
                                intSectionID = cmnService.J_ReturnInt32Value(lblViewChallanDetailsSectionId.Text);
                            else
                            {
                                if (cmbDDSection.SelectedIndex > 0)
                                    intSectionID = Convert.ToInt32(Support.GetItemData(cmbDDSection, cmbDDSection.SelectedIndex));
                            }
                            //
                            if (chkCashBookEntry.Checked == true)
                                intCashBookEntry = 1;
                            //-----------------------------------------------------------
                            strSQL = "UPDATE TRN_DEDUCTEE_DETAILS SET " +
                                "            PARTY_ID             =  " + lngDeducteeID + "," +
                                "            PAYMENT_DATE         =  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDeducteeDate) + cmnService.J_DateOperator() + "," +
                                "            DEDUCTED_DATE        =  " + strDEDUCTED_DATE + "," +
                                "            PAYMENT_AMOUNT       =  " + cmnService.J_ReturnDoubleValue(txtDeducteeAmountOfPayment.Text) + "," +
                                "            RATE                 =  " + cmnService.J_ReturnDoubleValue(txtDeducteeRate.Text) + "," +
                                "            TAX_AMOUNT           =  " + cmnService.J_ReturnDoubleValue(txtDeducteeIncometax.Text) + "," +
                                "            SURCHARGE_AMOUNT     =  " + cmnService.J_ReturnDoubleValue(txtDeducteeSurcharge.Text) + "," +
                                "            CESS_AMOUNT          =  " + cmnService.J_ReturnDoubleValue(txtDeducteeCess.Text) + "," +
                                "            TOTAL_AMOUNT         =  " + cmnService.J_ReturnDoubleValue(txtDeducteeTotal.Text) + "," +
                                "            TAX_DEPOSITED_AMOUNT =  " + cmnService.J_ReturnDoubleValue(txtDeducteeTaxDeposited.Text) + "," +
                                "            NON_DEDUCTION_FLAG   = '" + cmnService.J_ReplaceQuote(strNonDeductionFlag) + "'," +
                                "            CASH_BOOK_ENTRY      =  " + intCashBookEntry + "," +
                                "            REASON_ID            =  " + Convert.ToInt32(Support.GetItemData(cmbRemarks, cmbRemarks.SelectedIndex)) + "," +
                                "            SECTION_ID           =  " + intSectionID + "," +
                                "            CERTIFICATE_NO       ='" + cmnService.J_ReplaceQuote(txtCertificateNo.Text.Trim()) + "' " +
                                "     WHERE  DEDUCTEE_DETAIL_ID   =  " + lngDeducteeDetailID + "";
                            //-----------------------------------------------------------
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }                            
                            // UPDATE TDS
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_TDS = CTRL_TDS + " + cmnService.J_ReturnDoubleValue(txtDeducteeIncometax.Text) + " - " + cmnService.J_ReturnDoubleValue(txtOldDeducteeIncometax.Text) + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            // UPDATE SURCHARGE
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_SURCHARGE = CTRL_SURCHARGE + " + cmnService.J_ReturnDoubleValue(txtDeducteeSurcharge.Text) + " - " + cmnService.J_ReturnDoubleValue(txtOldDeducteeSurcharge.Text) + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            // UPDATE CESS
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_EDU_CESS = CTRL_EDU_CESS + " + cmnService.J_ReturnDoubleValue(txtDeducteeCess.Text) + " - " + cmnService.J_ReturnDoubleValue(txtOldDeducteeCess.Text) + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            // UPDATE TOTAL
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_TOT = CTRL_TOT + " + cmnService.J_ReturnDoubleValue(txtDeducteeTotal.Text) + " - " + cmnService.J_ReturnDoubleValue(txtOldDeducteeTotal.Text) + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            // UPDATE CHALLAN TOTAL
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_TOT_TAX = CTRL_TOT_TAX + " + cmnService.J_ReturnDoubleValue(txtDeducteeTaxDeposited.Text) + " - " + cmnService.J_ReturnDoubleValue(txtOldDeducteeTaxDeposited.Text) + " " +
                                "     WHERE  CHALLAN_ID = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbSection.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            //-- UPDATE LAST WORKED
                            TdsMan.T_UpdateLastWorked(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), cmbQuarter.Text, strFormNo, Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
                            //-----------------------------------------------------------
                            //lngChallanID = dmlService.J_ReturnMaxValue("TRN_CHALLAN", "CHALLAN_ID");
                            //if (lngChallanID == 0) return;
                            //-----------------------------------------------------------
                            dmlService.J_Commit();
                            cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                            //-----------------------------------------------------------
                            ClearControls();
                            //-----------------------------------------------------------
                            lblMode.Text = J_Mode.Add;
                            lblMode.ForeColor = Color.GreenYellow;
                            //--
                            BtnEdit.Enabled = true;
                            BtnEdit.BackColor = Color.Lavender;
                            //--
                            BtnDelete.Enabled = true;
                            BtnDelete.BackColor = Color.Lavender;
                            //--
                            BtnSearch.Enabled = true;
                            BtnSearch.BackColor = Color.Lavender;
                            //--
                            BtnRefresh.Enabled = true;
                            BtnRefresh.BackColor = Color.Lavender;
                            //--
                            BackgroundColorChangeDeductee(lblMode.Text);
                            //-- Generate Challan Srl No.
                            //txtDeducteeSrlNo.Text = Convert.ToString(TdsMan.T_ReturnSrlNo(lngChallanID, T_SRL_NO.DEDUCTEE));
                            txtDeducteeSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_DEDUCTEE_DETAILS", "SL_NO", "CHALLAN_ID = " + lngChallanID + "") + 1);

                            txtViewDeducteeTotalTaxDeposited.Text = "0.00";
                            txtViewDeducteeTotalTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(dmlService.J_ExecSqlReturnScalar("SELECT SUM(CTRL_TOT_TAX) AS CTRL_TOT_TAX FROM TRN_CHALLAN WHERE CHALLAN_ID = " + lngChallanID)));
                            //--
                            LoadDeducteeGrid(lngChallanID);
                            //--
                            dmlService.J_setGridPosition(ref dgcViewDeductee, dsetGridClone, "DEDUCTEE_DETAIL_ID", lngDeducteeDetailID);
                            //--
                            txtDeducteeName.Select();
                            //-----------------------------------------------------------
                        }
                        break;
                    #endregion 

                    #region Delete
                    case J_Mode.Delete:
                        ////*****  For Delete
                        string strDeleteMessage = "";

                        if (lblSearchMode.Text == Tabbed_Mode.Challan)
                        {
                            //Added by Indrajit on 02-03-2013
                            if (dgcViewChallan.CurrentRowIndex < 0)
                            {
                                cmnService.J_UserMessage(J_Msg.DataNotFound);
                                if (dsetGridClone == null) return;
                                dmlService.J_setGridPosition(ref dgcViewChallan, dsetGridClone, "CHALLAN_ID", lngChallanID);
                                return;
                            }
                            //===============================
                            
                            if (lngChallanID <= 0)
                            {
                                cmnService.J_UserMessage("No Challan Selected ", MessageBoxIcon.Exclamation);
                                lblMode.Text = J_Mode.Add;
                                cmbSection.Select();
                                return;
                            }
                            // CHECK THE REFERENCE
                            strSQL = "SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS WHERE CHALLAN_ID = " + lngChallanID + "";
                            
                            long lngNoOfDeductees = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                            
                            // WHEN DEDUCTEE ENTRY EXISTS
                            if (lngNoOfDeductees > 0)
                            {
                                strDeleteMessage = "The Challan selected contains [" + lngNoOfDeductees + "]  nos. Deductee Detail records." +
                                    "Please note that if this Challan is deleted all the corresponding Deductee Detail records will also be deleted." +
                                    "Proceed Deletion?";
                            }
                            else
                            {
                                strDeleteMessage = "Proceed Deletion?";
                            }
                            //..........................................................
                            if (cmnService.J_UserMessage(strDeleteMessage, MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                lblMode.Text = J_Mode.Add;
                                //cmbSection.Select();
                                return;
                            }

                            dmlService.J_BeginTransaction();

                            if (lngNoOfDeductees > 0)
                            {
                                strSQL = "DELETE FROM TRN_DEDUCTEE_DETAILS WHERE CHALLAN_ID =  " + lngChallanID + "";
                                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                                {
                                    lblMode.Text = J_Mode.Add;
                                    dmlService.J_Rollback();
                                    return;
                                }
                            }
                            
                            // RECALCALUTING SRL NO.
                            if (TdsMan.T_UpdateSrlNo(dmlService.J_pCommand, cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, "SELECT SL_NO FROM TRN_CHALLAN WHERE  CHALLAN_ID = " + lngChallanID))),
                                lngBasicInfoID, Updt_SrlNo.ChlnSrlNo) == false)
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                            
                            strSQL = "DELETE FROM TRN_CHALLAN WHERE CHALLAN_ID = " + lngChallanID + "";
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                lblMode.Text = J_Mode.Add;
                                dmlService.J_Rollback();
                                return;
                            }

                            dmlService.J_Commit();
                            cmnService.J_PanelMessage(0, J_Msg.DeleteMode);
                            
                            strSQL = strQuery + "ORDER BY " + strOrderBy;
                            if (dsetGridClone != null) dsetGridClone.Clear();
                            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewChallan, strSQL, strMatrix);       //Show Data into the Grid
                            if (dsetGridClone == null) return;

                            //--
                            lngChallanID = dmlService.J_ReturnMaxValue("TRN_CHALLAN", "CHALLAN_ID", "BASIC_INFO_ID = " + lngBasicInfoID);
                            if (lngChallanID == 0)
                            {
                                btnChallanDeducteeEntry.Enabled = false;
                                btnChallanDeducteeEntry.BackColor = Color.LightGray;
                            }
                            dmlService.J_setGridPosition(ref dgcViewChallan, dsetGridClone, "CHALLAN_ID", lngChallanID);
                            
                            lblMode.Text = J_Mode.Add;
                            
                            txtChallanSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_CHALLAN", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
                            //cmnService.J_StatusButton(this, lblMode.Text);
                            //-----------------------------------------------------------
                            //dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
                        }
                        else if (lblSearchMode.Text == Tabbed_Mode.Deductee)
                        {
                            //Added by Indrajit on 02-03-2013
                            if (dgcViewDeductee.CurrentRowIndex < 0)
                            {
                                cmnService.J_UserMessage(J_Msg.DataNotFound);
                                if (dsetGridClone == null) return;
                                dmlService.J_setGridPosition(ref dgcViewDeductee, dsetGridClone, "DEDUCTEE_DETAIL_ID", lngDeducteeDetailID);
                                return;
                            }
                            //===============================

                            if (cmnService.J_UserMessage("Proceed Deletion?", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.No)
                            {
                                lblMode.Text = J_Mode.Add;
                                //txtDeducteeName.Select();
                                return;
                            }

                            dmlService.J_BeginTransaction();

                            // UPDATE TDS
                            dblSumValues = cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, "SELECT TAX_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE DEDUCTEE_DETAIL_ID =  " + lngDeducteeID)));
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_TDS = CTRL_TDS - " + dblSumValues + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                txtDeducteeName.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            
                            // UPDATE SURCHARGE
                            dblSumValues = cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, "SELECT SURCHARGE_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE DEDUCTEE_DETAIL_ID =  " + lngDeducteeID)));
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_SURCHARGE = CTRL_SURCHARGE - " + dblSumValues + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                txtDeducteeName.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            
                            // UPDATE CESS
                            dblSumValues = cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, "SELECT CESS_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE DEDUCTEE_DETAIL_ID =  " + lngDeducteeID)));
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_EDU_CESS = CTRL_EDU_CESS - " + dblSumValues + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                txtDeducteeName.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            
                            // UPDATE TOTAL
                            dblSumValues = cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, "SELECT TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE DEDUCTEE_DETAIL_ID =  " + lngDeducteeID)));
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_TOT = CTRL_TOT - " + dblSumValues + " " +
                                "     WHERE  CHALLAN_ID   = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                txtDeducteeName.Select();
                                dmlService.J_Rollback();
                                return;
                            }
                            
                            // UPDATE CHALLAN TOTAL
                            dblSumValues = cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, "SELECT TAX_DEPOSITED_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE DEDUCTEE_DETAIL_ID =  " + lngDeducteeID)));
                            strSQL = "UPDATE TRN_CHALLAN SET " +
                                "            CTRL_TOT_TAX = CTRL_TOT_TAX - " + dblSumValues + " " +
                                "     WHERE  CHALLAN_ID = " + lngChallanID;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                txtDeducteeName.Select();
                                dmlService.J_Rollback();
                                return;
                            }

                            // RECALCALUTING SRL NO.
                            if (TdsMan.T_UpdateSrlNo(dmlService.J_pCommand, cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, "SELECT SL_NO FROM TRN_DEDUCTEE_DETAILS WHERE DEDUCTEE_DETAIL_ID = " + lngDeducteeID))),
                                lngChallanID, Updt_SrlNo.DeducteeSrlNo) == false)
                            {
                                dmlService.J_Rollback();
                                return;
                            }                          
                            
                            // DELETION OF TRN_DEDUCTEE_DETAILS
                            strSQL = "DELETE FROM TRN_DEDUCTEE_DETAILS WHERE DEDUCTEE_DETAIL_ID =  " + lngDeducteeID + "";
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                lblMode.Text = J_Mode.Add;
                                dmlService.J_Rollback();
                                return;
                            }
                            //-----------------------------------------------------------
                            dmlService.J_Commit();
                            cmnService.J_PanelMessage(0, J_Msg.DeleteMode);

                            strSQL = strQuery + "ORDER BY " + strOrderBy;
                            if (dsetGridClone != null) dsetGridClone.Clear();
                            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewDeductee, strSQL, strMatrix);       //Show Data into the Grid
                            if (dsetGridClone == null) return;

                            //--
                            lngDeducteeDetailID = dmlService.J_ReturnMaxValue("TRN_DEDUCTEE_DETAILS", "DEDUCTEE_DETAIL_ID","BASIC_INFO_ID = " + lngBasicInfoID);
                            
                            dmlService.J_setGridPosition(ref dgcViewDeductee, dsetGridClone, "DEDUCTEE_DETAIL_ID", lngDeducteeDetailID);
                            
                            lblMode.Text = J_Mode.Add;

                            txtDeducteeSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_DEDUCTEE_DETAILS", "SL_NO", "CHALLAN_ID = " + lngChallanID + "") + 1);
                            txtViewDeducteeTotalTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(dmlService.J_ExecSqlReturnScalar("SELECT SUM(CTRL_TOT_TAX) AS CTRL_TOT_TAX FROM TRN_CHALLAN WHERE CHALLAN_ID = " + lngChallanID)));
                            
                        }
                        break;
                    #endregion 
                }
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region CalcChallanTotTax
        private void CalcChallanTotTax()
        {
            
            txtTotalTax.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtTDS.Text) +
                                                cmnService.J_ReturnDoubleValue(txtSurcharge.Text) +
                                                cmnService.J_ReturnDoubleValue(txtEducationCess.Text) +
                                                cmnService.J_ReturnDoubleValue(txtInterests.Text) +
                                                cmnService.J_ReturnDoubleValue(txtOthers.Text) +
                                                cmnService.J_ReturnDoubleValue(txtFee.Text)));
        }
        #endregion

        #region CalcDeducteeTotTax
        private void CalcDeducteeTotTax()
        {
            txtDeducteeTotal.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtDeducteeIncometax.Text) +
                                                cmnService.J_ReturnDoubleValue(txtDeducteeSurcharge.Text) +
                                                cmnService.J_ReturnDoubleValue(txtDeducteeCess.Text)));
            txtDeducteeTaxDeposited.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(txtDeducteeTotal.Text));
        }
        #endregion

        #region CalcDeducteeTax
        private void CalcDeducteeTax()
        {
            //txtDeducteeIncometax.Text = string.Format("{0:0.00}", (Math.Ceiling(cmnService.J_ReturnDoubleValue(cmnService.J_ReturnDoubleValue(txtDeducteeAmountOfPayment.Text) * (cmnService.J_ReturnDoubleValue(txtDeducteeRate.Text) / 100)))));
            txtDeducteeIncometax.Text = string.Format("{0:0.00}", (Math.Round(cmnService.J_ReturnDoubleValue(cmnService.J_ReturnDoubleValue(txtDeducteeAmountOfPayment.Text) * (cmnService.J_ReturnDoubleValue(txtDeducteeRate.Text) / 100)),0, MidpointRounding.AwayFromZero)));
        }
        #endregion

        #region LoadChallanGrid

        #region LoadChallanGrid
        private void LoadChallanGrid(long BasicInfoID)
        {
            //-----------------------------------------------------------
            string strBSRCodeGridColumn = "";
            string strChallanGridColumn = "";
            //--
            if (txtDeductorType.Text.Substring(0, 1) == "A" || txtDeductorType.Text.Substring(0, 1) == "S")
            {
                strBSRCodeGridColumn = "BSR/24G No.";
                strChallanGridColumn = "Challan/Trf Vch No.";
            }
            else
            {
                strBSRCodeGridColumn = "BSR Code";
                strChallanGridColumn = "Challan No.";
            }
            string strSectionSize = "0";
            if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                strSectionSize = "0";
            else
                strSectionSize = "55";
            //--
            string[,] strMatrixChallanDetails = {{"ChallanID", "0", "", "Right", "", "", ""},
                                        {"Sl", "30", "S", "", "", "", ""},
                                        {"Section", strSectionSize, "S", "", "", "", ""},
                                        {strChallanGridColumn, "85", "S", "", "", "", ""},
                                        {"Deposit Dt", "70", "dd/MM/yyyy", "", "", "", ""},
                                        {strBSRCodeGridColumn, "80", "S", "", "", "", ""},
                                        {"Tax", "89", "0.00", "R", "", "", "T"},
                                        {"Deductee Total", "89", "0.00", "R", "", "", "T"},
                                        {"Difference .", "80", "0.00", "R", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            string[,] strLoadChallanGridMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TRANSFER_VOUCHER_NO", "F"},
                                                  {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.CHALLAN_NO", "F"}};

            //-----------------------------------------------------------------
            //ADDED BY DHRUB ON 07/01/2014 TO DEDUCT LATE_FEE FOR CHALLAN 
            //-----------------------------------------------------------------
            //-----------------------------------------------------------------
            string[,] strLoadChallanTotTaxMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
                                                    {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED- TRN_CHALLAN.LATE_FEE", "F"}};

            //-----------------------------------------------------------------
            //MODIFIED BY DHRUB ON 07/01/2014 TO DEDUCT LATE_FEE FOR CHALLAN 
            //-----------------------------------------------------------------
            //-----------------------------------------------------------------
            strOrderBy = "TRN_CHALLAN.CHALLAN_ID";
            strQuery = "SELECT TRN_CHALLAN.CHALLAN_ID          AS CHALLAN_ID," +
                      "       TRN_CHALLAN.SL_NO               AS SL_NO," +
                      "       MST_SECTION.SECTION_NO          AS SECTION_NO," +
                      "       " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrix, J_SQLColFormat.Case_End) + " AS CHALLAN_TRF_NO," +
                      "       TRN_CHALLAN.DEPOSIT_DATE        AS DEPOSIT_DATE," +
                      "       TRN_CHALLAN.BSR_CODE            AS BSR_CODE," +
                      "       " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " AS TOT_TAX," +
                      "       TRN_CHALLAN.CTRL_TOT_TAX        AS DEDUCTEE_TOTAL," +
                      "       (" + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " - TRN_CHALLAN.CTRL_TOT_TAX) as DIFF " +
                      "FROM  (TRN_CHALLAN LEFT JOIN MST_SECTION " +
                      "       ON  TRN_CHALLAN.SECTION_ID    = MST_SECTION.SECTION_ID)" +
                      "WHERE  TRN_CHALLAN.BASIC_INFO_ID = " + BasicInfoID + " ";
            //-----------------------------------------------------------

            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQLGridViewTabPages, strMatrixChallanDetails);       //Show Data into the Grid                
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQL, strMatrixChallanDetails);       //Show Data into the Grid
        }
        #endregion

        #region LoadChallanGrid
        private void LoadChallanGrid(long BasicInfoID, object sender, EventArgs e)
        {
            //-----------------------------------------------------------
            string strBSRCodeGridColumn = "";
            string strChallanGridColumn = "";
            //-----------------------------------------------------------
            if (txtDeductorType.Text.Substring(0, 1) == "A" || txtDeductorType.Text.Substring(0, 1) == "S")
            {
                strBSRCodeGridColumn = "BSR/24G No.";
                strChallanGridColumn = "Challan/Trf Vch No.";
            }
            else
            {
                strBSRCodeGridColumn = "BSR Code";
                strChallanGridColumn = "Challan No.";
            } 
            //
            string strSectionSize = "0";
            if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                strSectionSize = "0";
            else
                strSectionSize = "55";
            //--
                string[,] strMatrixChallanDetails = {{"ChallanID", "0", "", "Right", "", "", ""},
                                        {"Sl", "30", "S", "", "", "", ""},
                                        {"Section", strSectionSize, "S", "", "", "", ""},
                                        {strChallanGridColumn, "85", "S", "", "", "", ""},
                                        {"Deposit Dt", "70", "dd/MM/yyyy", "", "", "", ""},
                                        {strBSRCodeGridColumn, "80", "S", "", "", "", ""},
                                        {"Tax", "89", "0.00", "R", "", "", "T"},
                                        {"Deductee Total", "89", "0.00", "R", "", "", "T"},
                                        {"Difference .", "80", "0.00", "R", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            string[,] strLoadChallanGridMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TRANSFER_VOUCHER_NO", "F"},
                                            {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.CHALLAN_NO", "F"}};

            
            //-------------------------------------------------------------
            //ADDED BY DHRUB ON 07/01/2014 TO DEDUCT LATE_FEE FOR CHALLAN 
            //-------------------------------------------------------------
            string[,] strLoadChallanTotTaxMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
                                                    {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED- TRN_CHALLAN.LATE_FEE", "F"}};
            
            
            //--BLOCKED EXISTING SQL BY DHRUB ON 07/01/2014 
            //---------------------------------------------------
            //strOrderBy = "TRN_CHALLAN.CHALLAN_ID";
            //strQuery = "SELECT TRN_CHALLAN.CHALLAN_ID          AS CHALLAN_ID," +
            //          "       TRN_CHALLAN.SL_NO               AS SL_NO," +
            //          "       MST_SECTION.SECTION_NO          AS SECTION_NO," +
            //          "       " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrix, J_SQLColFormat.Case_End) + " AS CHALLAN_TRF_NO," +
            //          "       TRN_CHALLAN.DEPOSIT_DATE        AS DEPOSIT_DATE," +
            //          "       TRN_CHALLAN.BSR_CODE            AS BSR_CODE," +
            //          "      (TRN_CHALLAN.TOT_TAX - " +
            //          "       TRN_CHALLAN.INTEREST_ALLOCATED - " +
            //          "       TRN_CHALLAN.OTHERS_ALLOCATED)   AS TOT_TAX," +
            //          "       TRN_CHALLAN.CTRL_TOT_TAX        AS DEDUCTEE_TOTAL," +
            //          "     ((TRN_CHALLAN.TOT_TAX - " +
            //          "       TRN_CHALLAN.INTEREST_ALLOCATED - " +
            //          "       TRN_CHALLAN.OTHERS_ALLOCATED) - TRN_CHALLAN.CTRL_TOT_TAX) as DIFF " +
            //          "FROM  (TRN_CHALLAN LEFT JOIN MST_SECTION " +
            //          "       ON  TRN_CHALLAN.SECTION_ID    = MST_SECTION.SECTION_ID)" +
            //          "WHERE  TRN_CHALLAN.BASIC_INFO_ID = " + BasicInfoID + " ";

            //-----------------------------------------------------------------
            //MODIFIED BY DHRUB ON 07/01/2014 TO DEDUCT LATE_FEE FOR CHALLAN 
            //-----------------------------------------------------------------
            //-----------------------------------------------------------------
            strOrderBy = "TRN_CHALLAN.CHALLAN_ID";
            strQuery = "SELECT TRN_CHALLAN.CHALLAN_ID          AS CHALLAN_ID," +
                      "       TRN_CHALLAN.SL_NO               AS SL_NO," +
                      "       MST_SECTION.SECTION_NO          AS SECTION_NO," +
                      "       " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrix, J_SQLColFormat.Case_End) + " AS CHALLAN_TRF_NO," +
                      "       TRN_CHALLAN.DEPOSIT_DATE        AS DEPOSIT_DATE," +
                      "       TRN_CHALLAN.BSR_CODE            AS BSR_CODE," +
                      "       " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " AS TOT_TAX," +
                      "       TRN_CHALLAN.CTRL_TOT_TAX        AS DEDUCTEE_TOTAL," +
                      "       ("+ cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) +" - TRN_CHALLAN.CTRL_TOT_TAX) as DIFF " +
                      "FROM  (TRN_CHALLAN LEFT JOIN MST_SECTION " +
                      "       ON  TRN_CHALLAN.SECTION_ID    = MST_SECTION.SECTION_ID)" +
                      "WHERE  TRN_CHALLAN.BASIC_INFO_ID = " + BasicInfoID + " ";

            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQLGridViewTabPages, strMatrixChallanDetails);       //Show Data into the Grid                
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQL, strMatrixChallanDetails);       //Show Data into the Grid
        }
        #endregion

        #endregion

        #region LoadDeducteeGrid

        #region LoadDeducteeGrid
        private void LoadDeducteeGrid(long ChallanDetailID)
        {
            //-----------------------------------------------------------
            string[,] strMatrixViewDeductee = {{"DEDUCTEE_DETAIL_ID", "0", "", "Right", "", "", ""},
                                        {"Sl No.", "38", "S", "", "", "", ""},
                                        {"Deductee_ID", "0", "S", "", "", "", ""},
                                        {"PAN No.", "75", "S", "", "", "", ""},
                                        {"Deductee Name", "115", "S", "", "", "", ""},
                                        {"Section", "44", "S", "", "", "", ""},
                                        {"Amount", "75", "0.00", "R", "", "", ""},
                                        {"Date", "68", "dd/MM/yyyy", "", "", "", ""},
                                        {"Total", "75", "0.00", "R", "", "", ""},
                                        {"Tax Deposited", "75", "0.00", "R", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            strOrderBy = "TRN_DEDUCTEE_DETAILS.SL_NO";
            strQuery = "SELECT TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID   AS DEDUCTEE_DETAIL_ID," +
                     "         TRN_DEDUCTEE_DETAILS.SL_NO                AS SL_NO," +
                     "         MST_DEDUCTEE.DEDUCTEE_ID                  AS DEDUCTEE_ID," +
                     "         MST_DEDUCTEE.DEDUCTEE_PAN                 AS DEDUCTEE_PAN," +
                     "         MST_DEDUCTEE.DEDUCTEE_NAME                AS DEDUCTEE_NAME," +
                     "         MST_SECTION.SECTION_NO                    AS SECTION_NO," +
                     "         TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT       AS PAYMENT_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.PAYMENT_DATE         AS PAYMENT_DATE," +
                     "         TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT         AS TOTAL_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS TAX_DEPOSITED_AMOUNT " +
                     "  FROM   TRN_DEDUCTEE_DETAILS," +
                     "         MST_DEDUCTEE," +
                     "         MST_SECTION " +
                     "  WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID   = MST_DEDUCTEE.DEDUCTEE_ID " +
                     "  AND    TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID " +
                     "  AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + ChallanDetailID + " ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewDeductee, strSQLGridViewTabPages, strMatrixViewDeductee);       //Show Data into the Grid                
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewDeductee, strSQL, strMatrixViewDeductee);       //Show Data into the Grid                
        }
        #endregion       

        #region LoadDeducteeGrid
        private void LoadDeducteeGrid(long ChallanDetailID, object sender, EventArgs e)
        {
            //-----------------------------------------------------------
            string[,] strMatrixViewDeductee = {{"DEDUCTEE_DETAIL_ID", "0", "", "Right", "", "", ""},
                                        {"Sl No.", "38", "S", "", "", "", ""},
                                        {"Deductee_ID", "0", "S", "", "", "", ""},
                                        {"PAN No.", "75", "S", "", "", "", ""},
                                        {"Deductee Name", "115", "S", "", "", "", ""},
                                        {"Section", "44", "S", "", "", "", ""},
                                        {"Amount", "75", "0.00", "R", "", "", ""},
                                        {"Date", "68", "dd/MM/yyyy", "", "", "", ""},
                                        {"Total", "75", "0.00", "R", "", "", ""},
                                        {"Tax Deposited", "75", "0.00", "R", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            strOrderBy = "TRN_DEDUCTEE_DETAILS.SL_NO";
            strQuery = "SELECT TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID   AS DEDUCTEE_DETAIL_ID," +
                     "         TRN_DEDUCTEE_DETAILS.SL_NO                AS SL_NO," +
                     "         MST_DEDUCTEE.DEDUCTEE_ID                  AS DEDUCTEE_ID," +
                     "         MST_DEDUCTEE.DEDUCTEE_PAN                 AS DEDUCTEE_PAN," +
                     "         MST_DEDUCTEE.DEDUCTEE_NAME                AS DEDUCTEE_NAME," +
                     "         MST_SECTION.SECTION_NO                    AS SECTION_NO," +
                     "         TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT       AS PAYMENT_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.PAYMENT_DATE         AS PAYMENT_DATE," +
                     "         TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT         AS TOTAL_AMOUNT," +
                     "         TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS TAX_DEPOSITED_AMOUNT " +
                     "  FROM   TRN_DEDUCTEE_DETAILS," +
                     "         MST_DEDUCTEE," +
                     "         MST_SECTION " +
                     "  WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID   = MST_DEDUCTEE.DEDUCTEE_ID " +
                     "  AND    TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID " +
                     "  AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + ChallanDetailID + " ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewDeductee, strSQLGridViewTabPages, strMatrixViewDeductee);       //Show Data into the Grid                
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewDeductee, strSQL, strMatrixViewDeductee);       //Show Data into the Grid                

            //dgcViewDeductee_Click(sender, e);
        }
        #endregion       

        #endregion

        #region GenerateTextFile
        private bool GenerateTextFile(string strInputFilePath)
        {
            //-------------------------------
            string strFVUVersion = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FVU_VERSION FROM MST_ASSESSMENT WHERE ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex))));
            //
            string strRPMobileNo = "";
            if (cmnService.J_ReturnDoubleValue(strFVUVersion) >= cmnService.J_ReturnDoubleValue(T_FVU_Version.FVU_3_0))
                strRPMobileNo = txtRPMobileNo.Text;
            //-------------------------------
            StreamWriter StreamWriter = null;

            int intPageNumber = 0;
            string strFileSequenceNo = "1";

            string strChangeAddress = "";
            string strPChangeAddress = "";

            IDataReader drdGenChallan = null;
            IDataReader drdGenDeductee = null;

            long lngChallanRowCount = 0;

            string strGenTotalNoDeducteeForChallan = "";
            long lngGenChallanID = 0;
            string strNilChallanIndiactor = "";
            string strChallanDate = "";
            string strPaymentDate = "";
            string strDeductedDate = "";
            string strBookEntry = "N";
            string strChequeNo = "N";
            double dblTotalTax = 0;
            string strFee = "";
            string strSectionNo = "";
            string strCertificateNo = "";

            try
            {
                if (ValidateFields() == false) return false;

                // INSERT FILE GENEARTION DATA

                if (chkAddressChange.Checked == true)
                    strChangeAddress = "Y";
                else
                    strChangeAddress = "N";

                if (chkRPAddressChange.Checked == true)
                    strPChangeAddress = "Y";
                else
                    strPChangeAddress = "N";

                // GENERATE TEXT FILE
                StreamWriter = cmnService.J_ReturnStreamWriter(strInputFilePath);
                // WRITE LINE
                // FILE HEADER
                intPageNumber = intPageNumber + 1;

                //Commented by Shrey Kejriwal on 19/01/2012
                //cmnService.J_WriteLine(ref StreamWriter, TdsMan.T_WriteField(intPageNumber.ToString()) + TdsMan.T_WriteField("FH") + TdsMan.T_WriteField("NS1") + 
                //                       TdsMan.T_WriteField("R") +  TdsMan.T_WriteField(Convert.ToString(TdsMan.J_ConvertToIntDDMMYYYY(Convert.ToString(DateTime.Now)))) +
                //                       TdsMan.T_WriteField(strFileSequenceNo) + TdsMan.T_WriteField("D") + TdsMan.T_WriteField(txtTAN.Text) +
                //                       TdsMan.T_WriteField("1") + TdsMan.T_WriteField("TDS-MAN") + TdsMan.T_WriteBlankField(6,"^"));

                //Added by Shrey Kejriwal on 19/01/2012
                cmnService.J_WriteLine(ref StreamWriter, TdsMan.T_WriteField(intPageNumber.ToString()) + TdsMan.T_WriteField("FH") + TdsMan.T_WriteField("NS1") +
                                       TdsMan.T_WriteField("R") + TdsMan.T_WriteField(Convert.ToString(TdsMan.J_ConvertToIntDDMMYYYY(Convert.ToString(DateTime.Now)))) +
                                       TdsMan.T_WriteField(strFileSequenceNo) + TdsMan.T_WriteField("D") + TdsMan.T_WriteField(txtTAN.Text) +
                                       TdsMan.T_WriteField("1") + TdsMan.T_WriteField("TDS-MAN") + TdsMan.T_WriteBlankField(7, "^"));

                // BATCH HEADER
                intPageNumber = intPageNumber + 1;

                #region Commented by Shrey Kejriwal
                //Commented by Shrey Kejriwal on 06/02/2012
                //cmnService.J_WriteLine(ref StreamWriter, TdsMan.T_WriteField(intPageNumber.ToString()) + TdsMan.T_WriteField("BH") + TdsMan.T_WriteField("1") +
                //                        TdsMan.T_WriteField(txtTotalChallanRecords.Text) + TdsMan.T_WriteField(strFormNo) +
                //                        TdsMan.T_WriteBlankField(7, "^") + TdsMan.T_WriteField(txtTAN.Text) +
                //                        TdsMan.T_WriteBlankField(1, "^") + TdsMan.T_WriteField(txtPAN.Text) +
                //                        TdsMan.T_WriteField(cmnService.J_Left(txtAssessmentYear.Text, 4) + cmnService.J_Right(txtAssessmentYear.Text, 2)) +
                //                        TdsMan.T_WriteField(cmnService.J_Left(cmbFinancialYear.Text, 4) + cmnService.J_Right(cmbFinancialYear.Text, 2)) +
                //                        TdsMan.T_WriteField(cmbQuarter.Text) + TdsMan.T_WriteField(txtDedEmpColName.Text) +
                //                        TdsMan.T_WriteField(txtBranch.Text) + TdsMan.T_WriteField(txtAddress1.Text) +
                //                        TdsMan.T_WriteField(txtAddress2.Text) + TdsMan.T_WriteField(txtAddress3.Text) +
                //                        TdsMan.T_WriteField(txtAddress4.Text) + TdsMan.T_WriteField(txtAddress5.Text) +
                //                        TdsMan.T_WriteField(strStateCode) + TdsMan.T_WriteField(txtPIN.Text) +
                //                        TdsMan.T_WriteField(txtEmail.Text) + TdsMan.T_WriteField(txtSTD.Text) +
                //                        TdsMan.T_WriteField(txtPhone.Text) + TdsMan.T_WriteField(strChangeAddress) +
                //                        TdsMan.T_WriteField(cmnService.J_Left(txtDeductorType.Text, 2).Trim()) +
                //                        TdsMan.T_WriteField(txtRPName.Text) + TdsMan.T_WriteField(txtRPDesignation.Text) +
                //                        TdsMan.T_WriteField(txtRPAddress1.Text) + TdsMan.T_WriteField(txtRPAddress2.Text) +
                //                        TdsMan.T_WriteField(txtRPAddress3.Text) + TdsMan.T_WriteField(txtRPAddress4.Text) +
                //                        TdsMan.T_WriteField(txtRPAddress5.Text) + TdsMan.T_WriteField(strRPStateCode) +
                //                        TdsMan.T_WriteField(txtRPPIN.Text) + TdsMan.T_WriteField(txtRPEmail.Text) +
                //                        TdsMan.T_WriteField(strRPMobileNo) + TdsMan.T_WriteField(txtRPSTD.Text) +
                //                        TdsMan.T_WriteField(txtRPPhone.Text) + TdsMan.T_WriteField(strPChangeAddress) +
                //                        TdsMan.T_WriteField(txtTotalChallanAmount.Text) + TdsMan.T_WriteBlankField(3, "^") +
                //                        TdsMan.T_WriteField("N") + TdsMan.T_WriteBlankField(2, "^") + TdsMan.T_WriteField(strDStateCode) +
                //                        TdsMan.T_WriteField(txtPAOCode.Text) + TdsMan.T_WriteField(txtDDOCode.Text) +
                //                        TdsMan.T_WriteField(strMinistryCode) + TdsMan.T_WriteField(txtOtherMinistry.Text) +
                //                        TdsMan.T_WriteBlankField(1, "^") + TdsMan.T_WriteField(txtPAORegNo.Text) +
                //                        TdsMan.T_WriteField(txtDDORegNo.Text));
                #endregion
                //Added by Shrey Kejriwal on 06/02/2012
                //changed address sequencing
                //
                string TANRegNo = "", AltSTD = "", AltPhone = "", AltEmail = "", AltRPSTD = "", AltRPPhone = "", AltRPEmail = "", AIN = "" ;
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                {
                    TANRegNo = txtTANRegNo.Text;
                    AltSTD = txtAltSTD.Text; 
                    AltPhone = txtAltPhone.Text; 
                    AltEmail = txtAltEmail.Text; 
                    AltRPSTD = txtAltRPSTD.Text; 
                    AltRPPhone = txtAltRPPhone.Text; 
                    AltRPEmail = txtAltRPEmail.Text; 
                    AIN = txtAIN.Text;
                }
                //-- ANIK @ 2013/09/27 for FVU 4.0
                string strPrevTokenNoYN = "N";
                if (txtPrevTokenNo.Text.Length > 0)
                    strPrevTokenNoYN = "Y";
                //
                cmnService.J_WriteLine(ref StreamWriter, TdsMan.T_WriteField(intPageNumber.ToString()) + TdsMan.T_WriteField("BH") + TdsMan.T_WriteField("1") +
                                       TdsMan.T_WriteField(txtTotalChallanRecords.Text) + TdsMan.T_WriteField(strFormNo) +
                                        //TdsMan.T_WriteBlankField(7, "^") +
                                       //-- ANIK @ 2013/09/27 for FVU 4.0
                                       TdsMan.T_WriteBlankField(3, "^") +
                                       TdsMan.T_WriteField(txtPrevTokenNo.Text) +
                                       TdsMan.T_WriteBlankField(3, "^") + 
                                       TdsMan.T_WriteField(txtTAN.Text) +
                                       TdsMan.T_WriteBlankField(1, "^") + TdsMan.T_WriteField(txtPAN.Text) +
                                       TdsMan.T_WriteField(cmnService.J_Left(txtAssessmentYear.Text, 4) + cmnService.J_Right(txtAssessmentYear.Text, 2)) +
                                       TdsMan.T_WriteField(cmnService.J_Left(cmbFinancialYear.Text, 4) + cmnService.J_Right(cmbFinancialYear.Text, 2)) +
                                       TdsMan.T_WriteField(cmbQuarter.Text) + TdsMan.T_WriteField(txtDedEmpColName.Text) +
                                       TdsMan.T_WriteField(txtBranch.Text) + TdsMan.T_WriteField(txtAddress1.Text) +
                                       TdsMan.T_WriteField(txtAddress4.Text) + TdsMan.T_WriteField(txtAddress2.Text) +
                                       TdsMan.T_WriteField(txtAddress5.Text) + TdsMan.T_WriteField(txtAddress3.Text) +
                                       TdsMan.T_WriteField(strStateCode) + TdsMan.T_WriteField(txtPIN.Text) +
                                       TdsMan.T_WriteField(txtEmail.Text) + TdsMan.T_WriteField(txtSTD.Text) +
                                       TdsMan.T_WriteField(txtPhone.Text) + TdsMan.T_WriteField(strChangeAddress) +
                                       TdsMan.T_WriteField(cmnService.J_Left(txtDeductorType.Text, 2).Trim()) +
                                       TdsMan.T_WriteField(txtRPName.Text) + TdsMan.T_WriteField(txtRPDesignation.Text) +
                                       TdsMan.T_WriteField(txtRPAddress1.Text) + TdsMan.T_WriteField(txtRPAddress4.Text) +
                                       TdsMan.T_WriteField(txtRPAddress2.Text) + TdsMan.T_WriteField(txtRPAddress5.Text) +
                                       TdsMan.T_WriteField(txtRPAddress3.Text) + TdsMan.T_WriteField(strRPStateCode) +
                                       TdsMan.T_WriteField(txtRPPIN.Text) + TdsMan.T_WriteField(txtRPEmail.Text) +
                                       TdsMan.T_WriteField(strRPMobileNo) + TdsMan.T_WriteField(txtRPSTD.Text) +
                                       TdsMan.T_WriteField(txtRPPhone.Text) + TdsMan.T_WriteField(strPChangeAddress) +
                                       TdsMan.T_WriteField(txtTotalChallanAmount.Text) + TdsMan.T_WriteBlankField(3, "^") +
                                       TdsMan.T_WriteField("N") + 
                                       //-- ANIK @ 2013/09/27 for FVU 4.0                
                                       //TdsMan.T_WriteBlankField(2, "^") +
                                       TdsMan.T_WriteField(strPrevTokenNoYN) + 
                                       TdsMan.T_WriteBlankField(1, "^") +
                                       TdsMan.T_WriteField(strDStateCode) +
                                       TdsMan.T_WriteField(txtPAOCode.Text) + TdsMan.T_WriteField(txtDDOCode.Text) +
                                       TdsMan.T_WriteField(strMinistryCode) + TdsMan.T_WriteField(txtOtherMinistry.Text) +
                                       TdsMan.T_WriteField(TANRegNo) +
                                       TdsMan.T_WriteField(txtPAORegNo.Text) + TdsMan.T_WriteField(txtDDORegNo.Text) +
                                       TdsMan.T_WriteField(AltSTD) + TdsMan.T_WriteField(AltPhone) +
                                       TdsMan.T_WriteField(AltEmail) + TdsMan.T_WriteField(AltRPSTD) +
                                       TdsMan.T_WriteField(AltRPPhone) + TdsMan.T_WriteField(AltRPEmail) +
                                       TdsMan.T_WriteField(AIN));
                //-- CHALLAN HEADER
                strSQL = "SELECT COUNT(*) AS CHALLN_ROW_COUNT " +
                    "     FROM   TRN_CHALLAN " +
                    "     WHERE  BASIC_INFO_ID = " + lngBasicInfoID + " ";

                lngChallanRowCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));
                strArray = new string[lngChallanRowCount, 28];

                strSQL = "SELECT TRN_CHALLAN.CHALLAN_ID          AS CHALLAN_ID," +
                    "            TRN_CHALLAN.SL_NO               AS SL_NO," +
                    "            TRN_CHALLAN.SECTION_ID          AS SECTION_ID," +
                    "            MST_SECTION.SECTION_NAME        AS SECTION_NAME," +
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
                    "            TRN_CHALLAN.CTRL_TDS            AS CTRL_TDS," +
                    "            TRN_CHALLAN.CTRL_SURCHARGE      AS CTRL_SURCHARGE," +
                    "            TRN_CHALLAN.CTRL_EDU_CESS       AS CTRL_EDU_CESS," +
                    "            TRN_CHALLAN.CTRL_TOT_TAX        AS CTRL_TOT_TAX," +
                    "            TRN_CHALLAN.CTRL_TOT            AS CTRL_TOT," +
                    "            TRN_CHALLAN.INTEREST_ALLOCATED  AS INTEREST_ALLOCATED," +
                    "            TRN_CHALLAN.OTHERS_ALLOCATED    AS OTHERS_ALLOCATED," +
                    "            TRN_CHALLAN.REMARKS             AS REMARKS," +
                    "            TRN_CHALLAN.BOOK_ENTRY          AS BOOK_ENTRY," +
                    "            TRN_CHALLAN.LATE_FEE            AS LATE_FEE," +
                    "            MST_MINOR_HEAD.MINOR_HEAD_CODE  AS MINOR_HEAD_CODE " +
                    "     FROM  ((TRN_CHALLAN LEFT JOIN MST_SECTION " +
                    "            ON  TRN_CHALLAN.SECTION_ID    = MST_SECTION.SECTION_ID)" +
                    "            LEFT JOIN MST_MINOR_HEAD " +
                    "            ON  TRN_CHALLAN.MINOR_HEAD_ID = MST_MINOR_HEAD.MINOR_HEAD_ID)" +
                    "     WHERE  BASIC_INFO_ID                 = " + lngBasicInfoID + " " +
                    "     ORDER BY SL_NO";

                drdGenChallan = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdGenChallan == null)
                    return false;
                //
                long lngArrayCounter = 0;

                while (drdGenChallan.Read())
                {
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.CHALLAN_ID] = drdGenChallan["CHALLAN_ID"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.SL_NO] = drdGenChallan["SL_NO"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.SECTION_ID] = drdGenChallan["SECTION_ID"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.SECTION_NAME] = cmnService.J_NullToText(drdGenChallan["SECTION_NAME"]);
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.DEPOSIT_DATE] = drdGenChallan["DEPOSIT_DATE"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.BSR_CODE] = drdGenChallan["BSR_CODE"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.CHALLAN_NO] = drdGenChallan["CHALLAN_NO"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.TRANSFER_VOUCHER_NO] = drdGenChallan["TRANSFER_VOUCHER_NO"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.CHEQUE_NO] = drdGenChallan["CHEQUE_NO"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.TDS] = drdGenChallan["TDS"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.SURCHARGE] = drdGenChallan["SURCHARGE"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.EDUCATION_CESS] = drdGenChallan["EDUCATION_CESS"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.INTEREST] = drdGenChallan["INTEREST"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.OTHERS] = drdGenChallan["OTHERS"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.TOT_TAX] = drdGenChallan["TOT_TAX"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.CTRL_TDS] = drdGenChallan["CTRL_TDS"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.CTRL_SURCHARGE] = drdGenChallan["CTRL_SURCHARGE"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.CTRL_EDU_CESS] = drdGenChallan["CTRL_EDU_CESS"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.CTRL_TOT_TAX] = drdGenChallan["CTRL_TOT_TAX"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.CTRL_TOT] = drdGenChallan["CTRL_TOT"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.INTEREST_ALLOCATED] = drdGenChallan["INTEREST_ALLOCATED"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.OTHERS_ALLOCATED] = drdGenChallan["OTHERS_ALLOCATED"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.REMARKS] = drdGenChallan["REMARKS"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.BOOK_ENTRY] = drdGenChallan["BOOK_ENTRY"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.FEE] = drdGenChallan["LATE_FEE"].ToString();
                    strArray[lngArrayCounter, (int)enmCHALLANIndex.MINOR_HEAD] = cmnService.J_NullToText(drdGenChallan["MINOR_HEAD_CODE"]);

                    lngArrayCounter++;
                }
                drdGenChallan.Close();

                if (lngArrayCounter > 0)
                {
                    //-------------------------------------------------------------------------
                    for (long lngCounter = 0; lngCounter <= lngArrayCounter - 1; lngCounter++)
                    {
                        //------------------------------------------------
                        intPageNumber = intPageNumber + 1;

                        //-- INITIALIZE
                        strBookEntry = "";
                        strChequeNo = "";
                        strNilChallanIndiactor = "";
                        strGenTotalNoDeducteeForChallan = "";
                        strChallanDate = "";
                        dblTotalTax = 0;
                        lngGenChallanID = 0;
                        //--

                        lngGenChallanID = cmnService.J_ReturnInt32Value(strArray[lngCounter, (int)enmCHALLANIndex.CHALLAN_ID]);

                        strGenTotalNoDeducteeForChallan = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM TRN_DEDUCTEE_DETAILS WHERE CHALLAN_ID = " + lngGenChallanID));

                        // NIL CHALLAN INDICATOR
                        dblTotalTax = cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)enmCHALLANIndex.TOT_TAX]);

                        if (dblTotalTax == 0)
                        {
                            strNilChallanIndiactor = "Y";
                            strChallanDate = TdsMan.J_ConvertToIntDDMMYYYY(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text));
                        }
                        else
                        {
                            strNilChallanIndiactor = "N";
                            strChallanDate = TdsMan.J_ConvertToIntDDMMYYYY(strArray[lngCounter, (int)enmCHALLANIndex.DEPOSIT_DATE]);
                        }

                        // BOOK ENTRY
                        if (strArray[lngCounter, (int)enmCHALLANIndex.BOOK_ENTRY].Trim() == "1")
                        {
                            strBookEntry = "Y";
                            strChequeNo = "";
                        }
                        else
                        {
                            //MODIFIED BY SHREY KEJRIWAL ON 22/03/2012
                            strBookEntry = "N";

                            if (strArray[lngCounter, (int)enmCHALLANIndex.CHEQUE_NO].Trim() == "")
                                strChequeNo = "0";
                            else
                                strChequeNo = strArray[lngCounter, (int)enmCHALLANIndex.CHEQUE_NO].Trim();
                        }

                        //COMMENTED BY SHREY KEJRIWAL ON 22/03/2012
                        //if (txtDeductorType.Text.Substring(0, 1) == "A" || txtDeductorType.Text.Substring(0, 1) == "S")
                        //    if (cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)enmCHALLANIndex.TOT_TAX]) > 0)
                        //        if (strArray[lngCounter, (int)enmCHALLANIndex.TRANSFER_VOUCHER_NO].Trim() == "" && 
                        //            strArray[lngCounter, (int)enmCHALLANIndex.BSR_CODE].Trim() == "" &&
                        //            strArray[lngCounter, (int)enmCHALLANIndex.CHALLAN_NO].Trim() == "")
                        //            strBookEntry = "Y";

                        //if (strBookEntry == "Y")
                        //    strChequeNo = "";

                        if (strNilChallanIndiactor == "Y")
                        {
                            strChequeNo = "";
                            strBookEntry = "";
                        }
                        //
                        if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2012_13ID)
                            strFee = string.Format("{0:0.00}", Convert.ToDouble(strArray[lngCounter, (int)enmCHALLANIndex.FEE].Trim() == "" ? "0" : strArray[lngCounter, (int)enmCHALLANIndex.FEE].Trim()));
                        else
                            strFee = "";
                        //
                        if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                            strChequeNo = "";
                        //--
                        cmnService.J_WriteLine(ref StreamWriter, TdsMan.T_WriteField(intPageNumber.ToString()) +
                            TdsMan.T_WriteField("CD") +
                            TdsMan.T_WriteField("1") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.SL_NO]) +
                            TdsMan.T_WriteField(strGenTotalNoDeducteeForChallan) +
                            TdsMan.T_WriteField(strNilChallanIndiactor) +
                            TdsMan.T_WriteBlankField(5, "^") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.CHALLAN_NO]) +
                            TdsMan.T_WriteBlankField(1, "^") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.TRANSFER_VOUCHER_NO]) +
                            TdsMan.T_WriteBlankField(1, "^") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.BSR_CODE]) +
                            TdsMan.T_WriteBlankField(1, "^") +
                            TdsMan.T_WriteField(strChallanDate) +
                            TdsMan.T_WriteBlankField(2, "^") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.SECTION_NAME]) +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.TDS], "0.00") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.SURCHARGE], "0.00") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.EDUCATION_CESS], "0.00") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.INTEREST], "0.00") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.OTHERS], "0.00") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.TOT_TAX], "0.00") +
                            TdsMan.T_WriteBlankField(1, "^") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.CTRL_TOT_TAX], "0.00") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.CTRL_TDS], "0.00") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.CTRL_SURCHARGE], "0.00") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.CTRL_EDU_CESS], "0.00") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.CTRL_TOT], "0.00") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.INTEREST_ALLOCATED], "0.00") +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.OTHERS_ALLOCATED], "0.00") +
                            TdsMan.T_WriteField(strChequeNo) +
                            TdsMan.T_WriteField(Convert.ToString(strBookEntry)) +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.REMARKS]) +
                            TdsMan.T_WriteField(strFee) +
                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.MINOR_HEAD]));

                        // DEDUCTEE DETAIL
                        string[,] strGenerateTextFile = {{"MST_DEDUCTEE.DEDUCTEE_PAN = 'PANNOTAVBL'", "F", "C", "T"},
                                                         {"MST_DEDUCTEE.DEDUCTEE_PAN <> 'PANNOTAVBL'", "F", "TRN_DEDUCTEE_DETAILS.NON_DEDUCTION_FLAG", "F"}};

                        strSQL = "SELECT TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID   AS DEDUCTEE_DETAIL_ID," +
                            "            TRN_DEDUCTEE_DETAILS.SL_NO                AS SL_NO," +
                            "            MST_DEDUCTEE.DEDUCTEE_ID                  AS DEDUCTEE_ID," +
                            "            MST_DEDUCTEE.DEDUCTEE_NAME                AS DEDUCTEE_NAME," +
                            "            MST_DEDUCTEE.DEDUCTEE_PAN                 AS DEDUCTEE_PAN," +
                            "            MST_DEDUCTEE.DEDUCTEE_CODE                AS DEDUCTEE_CODE," +
                            "            " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "  AS PAYMENT_DATE," +
                            "            " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEDUCTED_DATE," +
                            "            TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT       AS PAYMENT_AMOUNT," +
                            "            TRN_DEDUCTEE_DETAILS.RATE                 AS RATE," +
                            "            TRN_DEDUCTEE_DETAILS.TAX_AMOUNT           AS TAX_AMOUNT," +
                            "            TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT     AS SURCHARGE_AMOUNT," +
                            "            TRN_DEDUCTEE_DETAILS.CESS_AMOUNT          AS CESS_AMOUNT," +
                            "            TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT         AS TOTAL_AMOUNT," +
                            "            TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS TAX_DEPOSITED_AMOUNT," +
                            "            TRN_DEDUCTEE_DETAILS.CASH_BOOK_ENTRY      AS CASH_BOOK_ENTRY," +
                            "            MST_SECTION.SECTION_NAME                  AS SECTION_NAME," +
                            "            TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO       AS CERTIFICATE_NO,";
                        if (cmnService.J_ReturnDoubleValue(strFVUVersion) >= cmnService.J_ReturnDoubleValue(T_FVU_Version.FVU_3_0))
                            strSQL = strSQL + cmnService.J_SQLDBFormat(strGenerateTextFile, J_SQLColFormat.Case_End) + " AS NON_DEDUCTION_FLAG ";
                        else
                            strSQL = strSQL + "TRN_DEDUCTEE_DETAILS.NON_DEDUCTION_FLAG AS NON_DEDUCTION_FLAG ";

                        strSQL = strSQL + "FROM   TRN_DEDUCTEE_DETAILS," +
                            "            MST_DEDUCTEE," +
                            "            MST_SECTION " +
                            "     WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID   = MST_DEDUCTEE.DEDUCTEE_ID " +
                            "     AND    TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID " +
                            "     AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + lngGenChallanID + " " +
                            "     ORDER BY TRN_DEDUCTEE_DETAILS.CHALLAN_ID, TRN_DEDUCTEE_DETAILS.SL_NO";

                        drdGenDeductee = dmlService.J_ExecSqlReturnReader(strSQL);
                        if (drdGenDeductee == null)
                            return false;
                        //--
                        while (drdGenDeductee.Read())
                        {
                            intPageNumber = intPageNumber + 1;

                            strPaymentDate = TdsMan.J_ConvertToIntDDMMYYYY(Convert.ToString(drdGenDeductee["PAYMENT_DATE"]));
                            strDeductedDate = TdsMan.J_ConvertToIntDDMMYYYY(Convert.ToString(drdGenDeductee["DEDUCTED_DATE"]));
                            // ANIK 2011-08-04
                            if (Convert.ToString(drdGenDeductee["CASH_BOOK_ENTRY"]) == "1")
                                strBookEntry = "Y";
                            else
                                strBookEntry = "N";
                            //
                            if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                            {
                                strSectionNo = Convert.ToString(drdGenDeductee["SECTION_NAME"]);
                                strCertificateNo = Convert.ToString(drdGenDeductee["CERTIFICATE_NO"]);
                                strBookEntry = "";
                            }
                            else
                            {
                                strSectionNo = "";
                                strCertificateNo = "";
                            }                        
                            //
                            cmnService.J_WriteLine(ref StreamWriter, TdsMan.T_WriteField(intPageNumber.ToString()) + TdsMan.T_WriteField("DD") + TdsMan.T_WriteField("1") +
                                            TdsMan.T_WriteField(strArray[lngCounter, (int)enmCHALLANIndex.SL_NO]) +
                                            TdsMan.T_WriteField(Convert.ToString(drdGenDeductee["SL_NO"])) +
                                            TdsMan.T_WriteField("O") +
                                            TdsMan.T_WriteBlankField(1, "^") +
                                            TdsMan.T_WriteField(Convert.ToString(Convert.ToInt16(Convert.ToString(drdGenDeductee["DEDUCTEE_CODE"])))) +
                                            TdsMan.T_WriteBlankField(1, "^") +
                                            TdsMan.T_WriteField(Convert.ToString(drdGenDeductee["DEDUCTEE_PAN"])) +
                                            TdsMan.T_WriteBlankField(2, "^") +
                                            TdsMan.T_WriteField(TdsMan.RemoveSpecialCharacters(Convert.ToString(drdGenDeductee["DEDUCTEE_NAME"]))) +
                                            TdsMan.T_WriteField(Convert.ToString(drdGenDeductee["TAX_AMOUNT"]), "0.00") +
                                            TdsMan.T_WriteField(Convert.ToString(drdGenDeductee["SURCHARGE_AMOUNT"]), "0.00") +
                                            TdsMan.T_WriteField(Convert.ToString(drdGenDeductee["CESS_AMOUNT"]), "0.00") +
                                            TdsMan.T_WriteField(Convert.ToString(drdGenDeductee["TOTAL_AMOUNT"]), "0.00") +
                                            TdsMan.T_WriteBlankField(1, "^") +
                                            TdsMan.T_WriteField(Convert.ToString(drdGenDeductee["TAX_DEPOSITED_AMOUNT"]), "0.00") +
                                            TdsMan.T_WriteBlankField(2, "^") +
                                            TdsMan.T_WriteField(Convert.ToString(drdGenDeductee["PAYMENT_AMOUNT"]), "0.00") +
                                            TdsMan.T_WriteField(strPaymentDate) +
                                            TdsMan.T_WriteField(strDeductedDate) +
                                            TdsMan.T_WriteBlankField(1, "^") +
                                            TdsMan.T_WriteField(Convert.ToString(drdGenDeductee["RATE"]), "0.0000") +
                                            TdsMan.T_WriteBlankField(1, "^") +
                                            TdsMan.T_WriteField(Convert.ToString(strBookEntry)) +
                                            TdsMan.T_WriteBlankField(1, "^") +
                                            TdsMan.T_WriteField(Convert.ToString(drdGenDeductee["NON_DEDUCTION_FLAG"])) +
                                            TdsMan.T_WriteBlankField(2, "^") +
                                            TdsMan.T_WriteField(strSectionNo) + TdsMan.T_WriteField(strCertificateNo)+
                                            TdsMan.T_WriteBlankField(4, "^"));
                        }
                        drdGenDeductee.Close();
                        //------------------------------------------------
                    }
                }

                StreamWriter.Flush();
                StreamWriter.Close();

                return true;
            }
            catch (Exception err_handler)
            {
                StreamWriter.Flush();
                StreamWriter.Close();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ValidateTextFile
        private void ValidateTextFile(string FVU_Version)
        {
            StreamWriter StreamWriter = null;            
            try
            {
                // CHECK EXISTENCE OF FVU FILE
                if (File.Exists(txtFVUPath.Text) == false)
                {
                    cmnService.J_UserMessage("File Validation Utility is either not installed or is of previous verison \n Please update your File Validation Utility by clicking on Utilities menu.", MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    return;
                }
                // FVU FILE OPEN
                if (cmnService.J_IsProcessOpen(txtFVUPath.Text) == true)
                {
                    cmnService.J_UserMessage("FVU File RUNNING", MessageBoxIcon.Exclamation);
                    this.Cursor = Cursors.Default;
                    return;
                }

                // CHECK EXISTENCE OF INPUT FILE
                if (File.Exists(txtTextFilePath.Text) == false)
                {
                    cmnService.J_UserMessage("Input File not found", MessageBoxIcon.Exclamation);
                    this.Cursor = Cursors.Default;
                    return;
                }
                // INPUT FILE OPEN
                if (cmnService.J_IsProcessOpen(txtTextFilePath.Text) == true)
                {
                    cmnService.J_UserMessage("Input File RUNNING", MessageBoxIcon.Exclamation);
                    this.Cursor = Cursors.Default;
                    return;
                }
                // DOWNLOAD CSI FILE
                //DownloadCSIFile();

                // DOWNLOAD CSI FILE
                if (chkCSIFileDownload.Enabled == true)
                {
                    if (chkCSIFileDownload.Checked == true)
                    {
                        // INTERNET Connectivity
                        if (TdsMan.T_CheckInternetConnectivty() == false)
                        {
                            //Commented by Shrey Kejriwal on 03/10/2012

                            //Added by Shrey Kejriwal on 03/10/2012
                            cmnService.J_UserMessage("Internet connection not found or TIN-NSDL Server cannot be reached. \n" +
                                                     "Please download and browse the path of CSI file for creating the FVU file. ", MessageBoxIcon.Exclamation);

                            //Opening the window to browse the CSI file
                            TrnBrowseCSIFilePath CSIfilePathform = new TrnBrowseCSIFilePath();
                            CSIfilePathform.ShowDialog();
                            this.Refresh();

                            //Checking if no path is selected
                            //if (TDSMAN.Classes.TDSMAN.T_pDownloadPath == "")
                            //    return;

                            strCSIDownloadFilePath = TDSMAN.Classes.TDSMAN.T_pDownloadPath;

                        }
                        else
                        {
                            // if internet connection is found then downloading the CSI file
                            DownloadCSIFile();
                        }
                    }
                    else
                    {
                        //Added by Shrey Kejriwal on 03/10/2012

                        //If Automatic download the CSI file is not selected
                        cmnService.J_UserMessage("Please browse the path of CSI file for creating the FVU file. ", MessageBoxIcon.Exclamation);

                        //Opening the window to browse the CSI file
                        TrnBrowseCSIFilePath CSIfilePathform = new TrnBrowseCSIFilePath();
                        CSIfilePathform.ShowDialog();
                        this.Refresh();

                        //Checking if no path is selected
                        //if (TDSMAN.Classes.TDSMAN.T_pDownloadPath == "")
                        //    return;

                        strCSIDownloadFilePath = TDSMAN.Classes.TDSMAN.T_pDownloadPath;

                    }
                }

                // CSI FILE
                if (File.Exists(strCSIDownloadFilePath) == false)
                {
                    strCSIDownloadFilePath = "";
                }
                //
                strBatFile = Path.Combine(Application.StartupPath, "TDS_FVU_" + FVU_Version + "\\RUN_FVU.BAT");

                // DELETION OF BAT FILE IF EXISTS
                if (File.Exists(strBatFile) == true)
                    File.Delete(strBatFile);
                
                // CREATION OF BAT FILE
                File.AppendAllText(strBatFile, "");
                //
                //string newErrFileName = Path.ChangeExtension(txtTextFilePath.Text, ".err");
                //string newFVUFileName = Path.ChangeExtension(txtTextFilePath.Text, ".fvu");   

                //string newPath = Path.Combine(activeDir, "eReturns" + "\\");

                // Create the subfolder.
                if (Directory.Exists(txtOutputFolder.Text) == false)
                    // Delete if the file exists.
                    Directory.CreateDirectory(txtOutputFolder.Text);

                string newErrFileName = Path.Combine(txtOutputFolder.Text, Path.ChangeExtension(newOutputFileName, ".err"));
                string newFVUFileName = Path.Combine(txtOutputFolder.Text,Path.ChangeExtension(newOutputFileName, ".fvu"));
                

                StreamWriter = cmnService.J_ReturnStreamWriter(strBatFile);

                cmnService.J_WriteLine(ref StreamWriter, "start javaw -jar " +
                                       (Char)34 + txtFVUPath.Text + (Char)34 + " " +
                                       (Char)34 + txtTextFilePath.Text + (Char)34 + " " +
                                       (Char)34 + newErrFileName + (Char)34 + " " +
                                       (Char)34 + newFVUFileName + (Char)34 + " " +
                                       (Char)34 + "0" + (Char)34 + " " +
                                       (Char)34 + FVU_Version + (Char)34 + " " +
                                       (Char)34 + "1" + (Char)34 + " " +
                                       (Char)34 + strCSIDownloadFilePath + (Char)34 + " " +
                                       (Char)34 + strConsolidatedStatementPath + (Char)34);

                StreamWriter.Flush();
                StreamWriter.Close();
                //
                //Commented by Shrey Kejriwal on 19/01/2012
                //Process proc = new Process();
                //proc.EnableRaisingEvents = true;
                //proc.StartInfo.FileName = strBatFile;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //proc.Start();
                //proc.WaitForExit(10000);
                //proc.Dispose();

                //Added by Shrey Kejriwal on 19/01/2012
                int ExitCode;
                ProcessStartInfo ProcessInfo;
                Process process;

                //ProcessInfo = new ProcessStartInfo(Application.StartupPath + "\\txtmanipulator\\txtmanipulator.bat", command);
                ProcessInfo = new ProcessStartInfo(strBatFile);
                ProcessInfo.CreateNoWindow = true;
                ProcessInfo.UseShellExecute = false;
                ProcessInfo.WorkingDirectory = Path.Combine(Application.StartupPath, "TDS_FVU_" + FVU_Version);
                // *** Redirect the output ***
                ProcessInfo.RedirectStandardError = true;
                ProcessInfo.RedirectStandardOutput = true;

                process = Process.Start(ProcessInfo);
                process.WaitForExit();

                // *** Read the streams ***
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                ExitCode = process.ExitCode;

                //MessageBox.Show("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
                //MessageBox.Show("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
                //MessageBox.Show("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");
                process.Close();


                //
            }
            catch (Exception err_handler)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err_handler.Message, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region DownloadCSIFile
        private void DownloadCSIFile()
        {
            try
            {
                //string url = @"http://www.thereforesystems.com/wp-content/uploads/2008/08/image35.png";

                //// Create an instance of WebClient
                //WebClient client = new WebClient();

                //// Hookup DownloadFileCompleted Event
                ////client.DownloadFileCompleted +=
                ////    new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                //// Start the download and copy the file to c:\temp
                //client.DownloadFileAsync(new Uri(url), @"c:\temp\image35.png");

                strCSIDownloadFilePath = Path.Combine(Path.GetDirectoryName(txtTextFilePath.Text), txtTAN.Text + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";

                if (File.Exists(strCSIDownloadFilePath) == true)
                    File.Delete(strCSIDownloadFilePath);
                
                if (chkCSIFileDownload.Checked == false)
                    return;

                mskViewCSIFileDownloadFrom.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MIN(DEPOSIT_DATE) AS MIN_DEPOSIT_DATE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID));
                mskViewCSIFileDownloadTo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MAX(DEPOSIT_DATE) AS MAX_DEPOSIT_DATE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID));

                string url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch/?appUser=T&TAN_NO=" + txtTAN.Text +
                              "&TAN_FROM_DT_DD=" + cmnService.J_Left(mskViewCSIFileDownloadFrom.Text, 2) +
                              "&TAN_FROM_DT_MM=" + cmnService.J_Mid(mskViewCSIFileDownloadFrom.Text, 3, 2) +
                              "&TAN_FROM_DT_YY=" + cmnService.J_Right(mskViewCSIFileDownloadFrom.Text, 4) +
                              "&TAN_TO_DT_DD=" + cmnService.J_Left(mskViewCSIFileDownloadTo.Text, 2) +
                              "&TAN_TO_DT_MM=" + cmnService.J_Mid(mskViewCSIFileDownloadTo.Text, 3, 2) +
                              "&TAN_TO_DT_YY=" + cmnService.J_Right(mskViewCSIFileDownloadTo.Text, 4) +
                              "&submit=Download Challan file";

                WebClient client = new WebClient();

                //strCSIDownloadFilePath = "@" + txtViewInputPath.Text + "\\" + txtViewTAN.Text + Convert.ToString(System.DateTime.Now) + ".csi";
                //string url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch/?appUser=T&TAN_NO=CALB01678F&TAN_FROM_DT_DD=01&TAN_FROM_DT_MM=01&TAN_FROM_DT_YY=2010&TAN_TO_DT_DD=01&TAN_TO_DT_MM=04&TAN_TO_DT_YY=2010&submit=Download Challan file";
                //client.DownloadFileAsync(new Uri(url), @"c:\" + txtViewTAN.Text);

                client.DownloadFileAsync(new Uri(url), @"" + strCSIDownloadFilePath);
                client.Dispose();
                //
                long lngTimeOut = 0;
                for (long i = 0; i <= 2; i++)
                {
                    // GET FILE SIZE
                    FileInfo fInfo = new FileInfo(@"" + strCSIDownloadFilePath);
                    long size = fInfo.Length;
                    //
                    if (size > 0)
                        return;

                    lngTimeOut = lngTimeOut + 1;

                    if (lngTimeOut > 152912)
                        return;

                    i = 0;
                    i++;
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion
        
        #region LoadNILReturn
        private void LoadNILReturn(object sender, EventArgs e)
        {   
            if (chkNILReturn.Checked == true)
            {
                //if (cmnService.J_UserMessage("NIL Return - Proceed??", MessageBoxButtons.YesNo) == DialogResult.No)
                //{
                //    chkNILReturn.Checked = false;
                //    chkNILReturn.Focus();
                //    return;
                //}
                
                //if (lngBasicInfoID == 0)
                //{
                //    if (TdsMan.T_GenerateBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                //                                        cmbQuarter.Text,
                //                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                //                                        strFormNo) == true)
                //    {
                //        //-----------------------------------------------
                //        lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                //                            cmbQuarter.Text,
                //                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                //                            strFormNo);
                //        //-----------------------------------------------
                //        InsertCompanyBasicInfo(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), lngBasicInfoID);
                //    }
                //}


                //=====================================================                
                //Added by Indrajit on 27-02-2013 to add the basic information
                //-----------------------------------------------------------
                if (lngBasicInfoID == 0)
                {
                    dmlService.J_BeginTransaction();

                    if (TdsMan.T_GenerateBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                            cmbQuarter.Text,
                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                            strFormNo) == true)
                    {
                        strSQL = "SELECT COUNT(*) " +
                            "     FROM   TRN_BASIC_INFO " +
                            "     WHERE  ASST_ID     = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) +
                            "     AND    QTR         ='" + cmbQuarter.Text + "'" +
                            "     AND    COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) +
                            "     AND    FORM_NO     ='" + cmnService.J_ReplaceQuote(strFormNo) + "'";
                        //--
                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 1)
                        {
                            //cmnService.J_UserMessage("This header entry exists");
                            dmlService.J_Rollback();
                        }
                        else
                        {
                            //-----------------------------------------------
                            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                cmbQuarter.Text,
                                                Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                strFormNo);
                            //-----------------------------------------------
                            InsertCompanyBasicInfo(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), lngBasicInfoID);
                            dmlService.J_Commit();
                        }
                    }
                    else
                        dmlService.J_Rollback();
                }
                //-----------------------------------------------------------

                //Added by Indrajit on 27-02-2013 to incorporate Basic Info locking
                #region INCORPORATE_BASIC_NFO_LOCKING

                TDSMAN.Classes.TDSMAN.T_pBasicInfoId = lngBasicInfoID;
                //================================================
                if (TdsMan.LockBasicInfoEntry(T_FormNo.F26Q) == false)
                {
                    chkNILReturn.Checked = false;

                    BtnExit.Select();
                    return;
                }

                #endregion
                //=====================================================

                if (cmnService.J_UserMessage("NIL Return - Proceed??", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    chkNILReturn.Checked = false;
                    chkNILReturn.Focus();
                    return;
                }




                strSQL = "UPDATE TRN_BASIC_INFO SET  NIL_RETURN = 1 WHERE BASIC_INFO_ID = " + lngBasicInfoID + " ";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    chkNILReturn.Checked = false;
                    chkNILReturn.Focus();
                    return;
                }

                strSQL = "DELETE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    chkNILReturn.Checked = false;
                    chkNILReturn.Focus();
                    return;
                }

                strSQL = "DELETE FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    chkNILReturn.Checked = false;
                    chkNILReturn.Focus();
                    return;
                }

                int intSectionID = 22;
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                    intSectionID = 0;

                strSQL = "INSERT INTO TRN_CHALLAN " +
                    "                (BASIC_INFO_ID," +
                    "                 SL_NO," +
                    "                 SECTION_ID," +
                    "                 DEPOSIT_DATE)" +
                    "     VALUES " +
                    "               (" + lngBasicInfoID + "," +
                    "                 1," +
                    "                " + intSectionID + "," +
                    "                " + cmnService.J_DateOperator() + TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text) + cmnService.J_DateOperator() + " )";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    chkNILReturn.Checked = false;
                    chkNILReturn.Focus();
                    return;
                }
                            
                btnChallanDeducteeEntry.Enabled = false;
                btnChallanDeducteeEntry.BackColor = Color.LightGray;

                BtnCancel.Enabled = true;
                BtnCancel.BackColor = Color.Lavender;

                // CONTROL SUMMARY
                ControlSummaryBasicInfo(lngBasicInfoID);
            }
            else if (chkNILReturn.Checked == false)
            {
                if (cmnService.J_UserMessage("Undo NIL Return - Proceed??", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    chkNILReturn.Checked = true;
                    cmbCompany.Focus();
                    return;
                }

                strSQL = "UPDATE TRN_BASIC_INFO SET  NIL_RETURN = 0 WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                dmlService.J_ExecSql(strSQL);

                strSQL = "DELETE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                dmlService.J_ExecSql(strSQL);

                strSQL = "DELETE FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                dmlService.J_ExecSql(strSQL);

                btnChallanDeducteeEntry.Enabled = true;
                btnChallanDeducteeEntry.BackColor = Color.Lavender;

                BtnCancel.Enabled = false;
                BtnCancel.BackColor = Color.LightGray;

                // CONTROL SUMMARY VISIBILITY
                ControlSummaryVisible(false, false, false, false, false, false, false);
            }
        }
        #endregion

        #region EnabilityNILReturn
        private void EnabilityNILReturn(long BasicInfoId)
        {
            try
            {
                if (BasicInfoId == 0)
                {
                    btnChallanDeducteeEntry.Enabled = true;
                    btnChallanDeducteeEntry.BackColor = Color.Lavender;

                    if (lblMode.Text == J_Mode.Add)
                    {
                        btnChallanDeducteeEntry.Enabled = true;
                        btnChallanDeducteeEntry.BackColor = Color.Lavender;
                        //
                        BtnEdit.Enabled = true;
                        BtnEdit.BackColor = Color.Lavender;
                    }

                    BtnCancel.Enabled = true;
                    BtnCancel.BackColor = Color.Lavender;
                    
                    chkNILReturn.Checked = false;
                }
                //------------------------Commented by Shrey Kejriwal on 10/10/2013
                //------------------------Nil Return discontinued from FVU 4.0
                //
                //strSQL = "SELECT NIL_RETURN FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + BasicInfoId;
                //if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                //{

                    //grpNILReturn.Enabled = true;

                    //btnChallanDeducteeEntry.Enabled = false;
                    //btnChallanDeducteeEntry.BackColor = Color.LightGray;
                    
                    //BtnCancel.Enabled = true;
                    //BtnCancel.BackColor = Color.Lavender;

                    //chkNILReturn.Checked = true;

                ////    return;
                ////}
                ////else
                ////{
                    btnChallanDeducteeEntry.Enabled = true;
                    btnChallanDeducteeEntry.BackColor = Color.Lavender;

                    if (lblMode.Text == J_Mode.Add)
                    {
                        btnChallanDeducteeEntry.Enabled = true;
                        btnChallanDeducteeEntry.BackColor = Color.Lavender;
                        //
                        BtnEdit.Enabled = true;
                        BtnEdit.BackColor = Color.Lavender;
                    }

                    BtnCancel.Enabled = true;
                    BtnCancel.BackColor = Color.Lavender;

                    chkNILReturn.Checked = false;
                //}
                // CHECK IF CHALLAN EXISTS
                strSQL = "SELECT COUNT(*) AS REC_EXISTS FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoId;
                if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                {
                    grpNILReturn.Enabled = false;
                    return;
                }
                else
                    grpNILReturn.Enabled = true;
                
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region EnabilityAddressChange
        private void EnabilityAddressChange(long BasicInfoId)
        {
            try
            {
                if (BasicInfoId == 0)
                {
                    chkAddressChange.Checked = false;
                    chkRPAddressChange.Checked = false;
                }
                // CHECK IF CHALLAN EXISTS
                strSQL = "SELECT COUNT(*) AS REC_EXISTS FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoId;
                if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                {
                    grpAddressChange.Enabled = true;
                    //return;
                }
                else
                    grpAddressChange.Enabled = false;
                //
                
                strSQL = "SELECT ADDRESS_CHANGE FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + BasicInfoId;
                if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                {
                    chkAddressChange.Checked = true;
                }
                else
                {
                    chkAddressChange.Checked = false;
                } 
                //
                strSQL = "SELECT P_ADDRESS_CHANGE FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + BasicInfoId;
                if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                {
                    chkRPAddressChange.Checked = true;
                }
                else
                {
                    chkRPAddressChange.Checked = false;
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region EnabilityReturnFilingStatus
        private void EnabilityReturnFilingStatus(long BasicInfoId)
        {
            IDataReader drdShowRecord = null;
            //
            try
            {
                if (BasicInfoId == 0)
                {
                    //grpReturnFilingStatus.Visible = false;
                    txtPrevTokenNo.Enabled = true;
                    ReturnFilingStatusVisible(false, false, false, false, false, false);
                    //
                    return;
                }
                // CHECK IF CHALLAN EXISTS
                strSQL = "SELECT RECEIPT_NO," +
                    "            " + cmnService.J_SQLDBFormat("DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DATE_OF_FILING," +
                    "            PRN_NO," +
                    "            PREV_FILED," +
                    "            PREV_PRN_NO " +
                    "     FROM   TRN_BASIC_INFO " +
                    "     WHERE  BASIC_INFO_ID = " + BasicInfoId; 
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    return;
                }
                while (drdShowRecord.Read())
                {
                    //
                    txtReceiptNo.Text = Convert.ToString(drdShowRecord["RECEIPT_NO"]);
                    txtDateofFiling.Text = Convert.ToString(drdShowRecord["DATE_OF_FILING"]);
                    txtTokenNo.Text = Convert.ToString(drdShowRecord["PRN_NO"]);

                    if (txtReceiptNo.Text.Trim() == "")
                    {
                        //grpReturnFilingStatus.Visible = false;
                        ReturnFilingStatusVisible(false, false, false, false, false, false);
                        //
                        BtnCancel.Enabled = true;
                        BtnCancel.BackColor = Color.Lavender;
                    }
                    else
                    {
                        //grpReturnFilingStatus.Visible = true;
                        ReturnFilingStatusVisible(true, true, true, true, true, true);
                        //
                        BtnCancel.Enabled = false;
                        BtnCancel.BackColor = Color.LightGray;
                    }
                    //-- ANIK @ 2013/09/27 FVU 4.0
                    blnRegularStatemnt = false;
                    txtPrevTokenNo.Enabled = true;
                    if (Convert.ToString(drdShowRecord["PREV_FILED"]) == "0")
                    {
                        cmbRegularStatement.Text = T_YES_NO.YES;
                        txtPrevTokenNo.Enabled = true;
                        txtPrevTokenNo.Text = Convert.ToString(drdShowRecord["PREV_PRN_NO"]);
                    }
                    else if (Convert.ToString(drdShowRecord["PREV_FILED"]) == "1")
                    {
                        cmbRegularStatement.Text = T_YES_NO.NO;
                        txtPrevTokenNo.Enabled = false;
                    } 
                    blnRegularStatemnt = true;
                    //--
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    return;
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //

            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region EnabilityGrpButtons
        private void EnabilityGrpButtons(long BasicInfoId)
        {
            //
            try
            {
                if (BasicInfoId == 0)
                {
                    //grpControlSummary.Visible = false;
                    ControlSummaryVisible(false, false, false, false, false, false, false);

                    //grpReturnFilingStatus.Visible = false;
                    ReturnFilingStatusVisible(false, false, false, false, false, false);
                    return;
                }
                // RECEIPT NO.
                if (label46.Visible == true)
                {
                    BtnCancel.Enabled = false;
                    BtnCancel.BackColor = Color.LightGray;
                }
                else
                {
                    BtnCancel.Enabled = true;
                    BtnCancel.BackColor = Color.Lavender;
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region EnabilityLastGenerationStatus
        private void EnabilityLastGenerationStatus(long BasicInfoId, bool UpdtStatus)
        {
            //
            try
            {
                if (BasicInfoId == 0)
                {
                    grpLastGenerationStatus.Visible = false;
                    return;
                }
                //--
                //--######################### UPDATE TRIAL COUNTER
                if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                    TdsMan.UpdateRegistryTrialCounter(BasicInfoId);
                //--#########################                        
                //strSQL = "SELECT OUTPUT_FILE_PATH + '\\' + LEFT(INPUT_FILE_NAME,8) AS FILE_PATH FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + BasicInfoId + " ORDER BY FG_LOG_ID DESC";
                strSQL = "SELECT OUTPUT_FILE_PATH + '\\' + LEFT(INPUT_FILE_NAME,7) AS FILE_PATH FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + BasicInfoId + " ORDER BY FG_LOG_ID DESC";
                string strOutputFilePath = Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
                //
                if (strOutputFilePath != "")
                {
                    grpLastGenerationStatus.Visible = true;
                    btnViewStatus.Visible = false;
                    // SUCCESSFULLY GENERATED WITH MISMATCH REPORT
                    //if (File.Exists(cmnService.J_Mid(strOutputFilePath, 0, (strOutputFilePath.Length - 12))
                    //    + cmnService.J_Left(cmnService.J_Right(strOutputFilePath, 12), 8) + "_Electronic_Statement_Warning_File.html") == true)
                    if (File.Exists(strOutputFilePath + "_Electronic_Statement_Warning_File.html") == true)
                    {
                        //-- GETTING THE DATE & TIME
                        strSQL = "SELECT DATE_TIME_OF_CREATION FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + BasicInfoId + " ORDER BY FG_LOG_ID DESC";
                        string strDATE_TIME_OF_CREATION = Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
                        //
                        grpLastGenerationStatus.Visible = true;
                        lblLastGenerationStatus.Text = "The file is successfully generated. However, a Warning file has been gerenrated. " +
                            "Click View Report to see the Warning file. (" + strDATE_TIME_OF_CREATION + ")";
                        lblLastGenerationStatus.ForeColor = Color.Green;
                        btnViewStatus.Visible = true;
                        // -- UPDATE VALIDATION STATUS & AUTOMATIC CSI FILE DOWNLOAD
                        if (UpdtStatus == true)
                        {
                            strSQL = "UPDATE TRN_FILE_GENERATION_LOG SET " +
                                "            VALIDATION_STATUS = 2 ";
                            
                            if(chkCSIFileDownload.Checked == true)
                                strSQL = strSQL + ", AUTO_CSI_FILE_DOWNLOAD = 1 ";

                            strSQL = strSQL + " WHERE  FG_LOG_ID = (SELECT MAX(FG_LOG_ID) AS FG_LOG_ID FROM TRN_FILE_GENERATION_LOG)";
                            //
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                return;
                            }
                        }
                        //
                        return;
                    }
                    // ERROR HAS OCCURRED
                    if (File.Exists(Path.ChangeExtension(strOutputFilePath, ".err")) == true)
                    {
                        //-- GETTING THE DATE & TIME
                        strSQL = "SELECT DATE_TIME_OF_CREATION FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + BasicInfoId + " ORDER BY FG_LOG_ID DESC";
                        string strDATE_TIME_OF_CREATION = Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
                        //
                        grpLastGenerationStatus.Visible = true;
                        lblLastGenerationStatus.Text = "An error has occurred while validation. " +
                            "Click on the button below to see the error report. (" + strDATE_TIME_OF_CREATION + ")";
                        lblLastGenerationStatus.ForeColor = Color.Red;
                        btnViewStatus.Visible = true;
                        // -- UPDATE VALIDATION STATUS & AUTOMATIC CSI FILE DOWNLOAD
                        if (UpdtStatus == true)
                        {
                            strSQL = "UPDATE TRN_FILE_GENERATION_LOG SET " +
                                "            VALIDATION_STATUS = 3 ";

                            if (chkCSIFileDownload.Checked == true)
                                strSQL = strSQL + ", AUTO_CSI_FILE_DOWNLOAD = 1 ";

                            strSQL = strSQL + " WHERE  FG_LOG_ID = (SELECT MAX(FG_LOG_ID) AS FG_LOG_ID FROM TRN_FILE_GENERATION_LOG)";
                            //
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                return;
                            }
                        } return;
                    }
                    //  SUCCESSFULLY GENERATED
                    if (File.Exists(Path.ChangeExtension(strOutputFilePath, ".fvu")) == true)
                    {
                        //-- GETTING THE DATE & TIME
                        strSQL = "SELECT DATE_TIME_OF_CREATION FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + BasicInfoId + " ORDER BY FG_LOG_ID DESC";
                        string strDATE_TIME_OF_CREATION = Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));                
                        //
                        grpLastGenerationStatus.Visible = true;
                        lblLastGenerationStatus.Text = "The file is successfully generated and is ready for submission. (" + strDATE_TIME_OF_CREATION + ")";
                        lblLastGenerationStatus.ForeColor = Color.Green;
                        btnViewStatus.Visible = false;
                        // -- UPDATE VALIDATION STATUS & AUTOMATIC CSI FILE DOWNLOAD
                        if (UpdtStatus == true)
                        {
                            strSQL = "UPDATE TRN_FILE_GENERATION_LOG SET " +
                                "            VALIDATION_STATUS = 1 ";

                            if (chkCSIFileDownload.Checked == true)
                                strSQL = strSQL + ", AUTO_CSI_FILE_DOWNLOAD = 1 ";

                            strSQL = strSQL + " WHERE  FG_LOG_ID = (SELECT MAX(FG_LOG_ID) AS FG_LOG_ID FROM TRN_FILE_GENERATION_LOG)";
                            //
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                return;
                            }
                        }
                        return;
                    }
                }
                else
                {
                    grpLastGenerationStatus.Visible = false;
                    return;
                }

                //
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region CreateOutputFile_Folder
        private string CreateOutputFile_Folder(string TAN, string FormNo, string FinancialYear, string Qtr, string CompanyName, long BasicInfoId)
        {
            try
            {
                //if (cmnService.J_IsProcessOpen(txtViewFVUPath.Text) == true)
                //{
                //    cmnService.J_UserMessage("FVU Running");
                //    return "";
                //}

                //-- PATH/FOLDER/SUB-FOLDER CREATION
                // Specify a "currently active folder"
                string activeDir = Application.StartupPath;
                //string activeDir = txtOutputFolder.Text; 
                //Create a new subfolder under the current active folder

                // 2011-01-03
                //string newPath = Path.Combine(activeDir, "eReturn\\" + TAN + "\\" + FormNo + "\\" +
                //                                                   FinancialYear + "\\" + Qtr);

                //string newPath = Path.Combine(activeDir, TAN + "\\");
                string newPath = Path.Combine(activeDir, "eReturns" + "\\");
                
                // Create the subfolder.
                if (Directory.Exists(newPath) == false)
                    // Delete if the file exists.
                    Directory.CreateDirectory(newPath);


                // Create a new file.
                //newFileName = txtViewFormNo.Text + "R" + txtViewQuarter.Text + ".txt"; //string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".txt";
                newOutputFileName = TdsMan.T_GenerateOutputFile(CompanyName, FinancialYear, Qtr, FormNo, BasicInfoId, GenerateOutputFile.Regular);

                // DELETE FILES OF SAME BASIC INFO FROM THE '\FVU files' FOLDER
                strSQL = "SELECT INPUT_FILE_NAME FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + BasicInfoId;
                string strInputFileName = Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
                //
                if (strInputFileName != "")
                {
                    // DELETE .txt FILES
                    if (File.Exists(Path.Combine(newPath, Path.ChangeExtension(strInputFileName, ".txt"))) == true)
                        File.Delete(Path.Combine(newPath, Path.ChangeExtension(strInputFileName, ".txt")));
                    //// DELETE err FILES
                    //if (File.Exists(Path.Combine(newPath, cmnService.J_Mid(strInputFileName, 0, 8) + "_Electronic_Statement_Warning_File.html")) == true)
                    //    File.Delete(Path.Combine(newPath, cmnService.J_Mid(strInputFileName, 0, 8) + "_Electronic_Statement_Warning_File.html"));
                    //// DELETE err FILES
                    //if (File.Exists(Path.Combine(newPath, cmnService.J_Mid(strInputFileName, 0, 8) + "err.html")) == true)
                    //    File.Delete(Path.Combine(newPath, cmnService.J_Mid(strInputFileName, 0, 8) + "err.html"));
                    //// DELETE _BH FILES
                    //if (File.Exists(Path.Combine(newPath, cmnService.J_Mid(strInputFileName, 0, 8) + "_BH.html")) == true)
                    //    File.Delete(Path.Combine(newPath, cmnService.J_Mid(strInputFileName, 0, 8) + "_BH.html"));
                    //// DELETE _CD FILES
                    //if (File.Exists(Path.Combine(newPath, cmnService.J_Mid(strInputFileName, 0, 8) + "_CD.html")) == true)
                    //    File.Delete(Path.Combine(newPath, cmnService.J_Mid(strInputFileName, 0, 8) + "_CD.html"));
                    //// DELETE _PAN_Statistics FILES
                    //if (File.Exists(Path.Combine(newPath, cmnService.J_Mid(strInputFileName, 0, 8) + "_PAN_Statistics.html")) == true)
                    //    File.Delete(Path.Combine(newPath, cmnService.J_Mid(strInputFileName, 0, 8) + "_PAN_Statistics.html"));
                    //// DELETE .html FILES
                    //if (File.Exists(Path.Combine(newPath, Path.ChangeExtension(strInputFileName, ".html"))) == true)
                    //    File.Delete(Path.Combine(newPath, Path.ChangeExtension(strInputFileName, ".html")));
                    //// DELETE .err FILES
                    //if (File.Exists(Path.Combine(newPath, Path.ChangeExtension(strInputFileName, ".err"))) == true)
                    //    File.Delete(Path.Combine(newPath, Path.ChangeExtension(strInputFileName, ".err")));
                    //// DELETE .fvu FILES
                    //if (File.Exists(Path.Combine(newPath, Path.ChangeExtension(strInputFileName, ".fvu"))) == true)
                    //    File.Delete(Path.Combine(newPath, Path.ChangeExtension(strInputFileName, ".fvu")));
                    // DELETE ALL .csi FILES
                    foreach (string sFile in System.IO.Directory.GetFiles(newPath))
                    {
                        if (sFile.ToUpper().EndsWith(".CSI"))
                            System.IO.File.Delete(sFile);
                    }
                }
                newOutputFileName = newOutputFileName + ".txt";
                // Combine the new file name with the path
                newPath = Path.Combine(newPath, newOutputFileName);
                
                // Create the file.
                //File.Create(newPath);
                File.Delete(newPath);

                return newPath;
            }
            catch //(Exception err_handler)
            {
                //cmnService.J_UserMessage(err_handler.Message);
                return "";
            }
        }
        #endregion

        #region GetFVUPath
        private string GetFVUPath(T_FVU FVU_Version)
        {
            try
            {
                if(FVU_Version == T_FVU.FVU_3_0)
                    return Application.StartupPath + "\\TDS_FVU_3.0\\TDS_FVU_STANDALONE.JAR";
                else if (FVU_Version == T_FVU.FVU_2_129)
                    return "";
                return "FVU NOT FOUND";
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return "";
            }
        }
        #endregion

        #region DisplayErrors
        private bool DisplayErrors(string ErrorFilePath)
        {
            try
            {
                // DISPLAY ERROR
                if (File.Exists(Path.Combine(Path.GetDirectoryName(ErrorFilePath), Path.GetFileNameWithoutExtension(ErrorFilePath) + "err.html")) == true)
                {
                    btnErrorViewClose.Enabled = true;
                    btnErrorViewClose.BackColor = Color.Blue;

                    grpViewErrors.Visible = true;

                    wbrDisplayErrors.Navigate(Path.Combine(Path.GetDirectoryName(ErrorFilePath), Path.GetFileNameWithoutExtension(ErrorFilePath) + "err.html"));

                    return true;
                }
                return false;                
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ControlSummaryVisible
        private void ControlSummaryVisible(bool bVisible0, bool bVisible1, bool bVisible2, bool bVisible3, bool bVisible4, bool bVisible5, bool bVisible6)
        {
            //grpControlSummary.Visible = bVisible0;
            lblLabel1.Visible   = bVisible1;
            txtTotalChallanRecords.Visible = bVisible1;
            lblLabel2.Visible   = bVisible2;
            txtTotalDeducteeRecords.Visible = bVisible2;
            lblLabel3.Visible   = bVisible3;
            txtTotalChallanAmount.Visible = bVisible3;
            lblLabel4.Visible   = bVisible4;
            txtTotalDeducteeTDS.Visible = bVisible4;
            label30.Visible = bVisible5;
            txtAmountPaid.Visible = bVisible5;
            //lblLabel6.Visible   = bVisible6;
            //txtTextBox6.Visible = bVisible6;
        }
        #endregion

        #region ControlSummaryBasicInfo
        private void ControlSummaryBasicInfo(long BasicInfoID)
        {
            if (BasicInfoID == 0)
            {
                ControlSummaryVisible(false, false, false, false, false, false, false);
                BtnCancel.Enabled = false;
                BtnCancel.BackColor = Color.LightGray;
                return;
            }

            // CONTROL SUMMARY VISIBILITY
            ControlSummaryVisible(true, true, true, true, true, true, false);
            //lblLabel1.Text = "Total Challan Records";
            txtTotalChallanRecords.Text = "0";
            //lblLabel2.Text = "Total Deductee Records";
            txtTotalDeducteeRecords.Text = "0";
            //lblLabel3.Text = "Total Tax";
            txtTotalChallanAmount.Text = "0.00";
            //lblLabel4.Text = "Total TDS/TCS";
            txtTotalDeducteeTDS.Text = "0.00";
            //label30.Text = "Tax Deposited";
            txtAmountPaid.Text = "0.00";

            // CONTROL SUMMARY VALUES
            txtTotalChallanRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(CHALLAN_ID) AS COUNT_CHALLAN_ID FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID));
            txtTotalDeducteeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID));
            txtTotalChallanAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID))));
            txtTotalDeducteeTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
            txtAmountPaid.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
            //
            if (Convert.ToDouble(txtTotalChallanRecords.Text) > 0 && txtTotalChallanRecords.Visible == true)
            {
                BtnCancel.Enabled = true;
                BtnCancel.BackColor = Color.Lavender;
            }
            else
            {
                BtnCancel.Enabled = false;
                BtnCancel.BackColor = Color.LightGray;
            }
        }
        #endregion

        #region ControlSummaryChallan
        private void ControlSummaryChallan(long ChallanID)
        {
            //if (ChallanID == 0)
            //{
            //    ControlSummaryVisible(false, false, false, false, false, false);
            //    return;
            //}

            //// CONTROL SUMMARY VISIBILITY
            //ControlSummaryVisible(true, true, true, true, true, false);
            //lblLabel1.Text = "TDS/TCS Income Tax";
            //txtTextBox1.Text = "0.00";
            //lblLabel2.Text = "TDS/TCS Surcharge";
            //txtTextBox2.Text = "0.00";
            //lblLabel3.Text = "TDS/TCS Cess";
            //txtTextBox3.Text = "0.00";
            //lblLabel4.Text = "TDS/TCS Total";
            //txtTextBox4.Text = "0.00";
            //lblLabel5.Text = "Total Tax Deposited";
            //txtTextBox5.Text = "0.00";

            //// CONTROL SUMMARY VALUES
            //txtTextBox1.Text = string.Format("{0:0.00}", Convert.ToDouble(dmlService.J_ExecSqlReturnScalar("SELECT CTRL_TDS FROM TRN_CHALLAN WHERE CHALLAN_ID = " + ChallanID)));
            //txtTextBox2.Text = string.Format("{0:0.00}", Convert.ToDouble(dmlService.J_ExecSqlReturnScalar("SELECT CTRL_SURCHARGE FROM TRN_CHALLAN WHERE CHALLAN_ID = " + ChallanID)));
            //txtTextBox3.Text = string.Format("{0:0.00}", Convert.ToDouble(dmlService.J_ExecSqlReturnScalar("SELECT CTRL_EDU_CESS FROM TRN_CHALLAN WHERE CHALLAN_ID = " + ChallanID)));
            //txtTextBox4.Text = string.Format("{0:0.00}", Convert.ToDouble(dmlService.J_ExecSqlReturnScalar("SELECT CTRL_TOT_TAX FROM TRN_CHALLAN WHERE CHALLAN_ID = " + ChallanID)));
            //txtTextBox5.Text = string.Format("{0:0.00}", Convert.ToDouble(dmlService.J_ExecSqlReturnScalar("SELECT CTRL_TOT FROM TRN_CHALLAN WHERE CHALLAN_ID = " + ChallanID)));
        }
        #endregion

        #region ReturnFilingStatusVisible
        private void ReturnFilingStatusVisible(bool bVisible0, bool bVisible1, bool bVisible2, bool bVisible3, bool bVisible4, bool bVisible5)
        {
            label46.Visible = bVisible0;
            txtReceiptNo.Visible = bVisible1;
            label45.Visible = bVisible2;
            txtTokenNo.Visible = bVisible3;
            label36.Visible = bVisible4;
            txtDateofFiling.Visible = bVisible5;
        }
        #endregion

        #region BackgroundColorChangeChallan
        private void BackgroundColorChangeChallan(string Mode)
        {
            if (Mode.ToUpper() == "ADD MODE")
            {
                cmbSection.BackColor = Color.White;
                mskDateOfPayment.BackColor = Color.White;
                txtBSRCode.BackColor = Color.White;
                txtChallanNo.BackColor = Color.White;
                if(txtTransferVoucherNo.Enabled == true)
                    txtTransferVoucherNo.BackColor = Color.White;
                txtChequeNo.BackColor = Color.White;
                txtTDS.BackColor = Color.White;
                txtSurcharge.BackColor = Color.White;
                txtEducationCess.BackColor = Color.White;
                txtInterests.BackColor = Color.White;
                txtOthers.BackColor = Color.White;
                txtInterestAllocated.BackColor = Color.White;
                txtOthersAllocated.BackColor = Color.White;
                txtRemarks.BackColor = Color.White;
                cmbMinorHead.BackColor = Color.White;
                txtFee.BackColor = Color.White;
            }
            else if (Mode.ToUpper() == "EDIT MODE")
            {
                cmbSection.BackColor = Color.Honeydew;
                mskDateOfPayment.BackColor = Color.Honeydew;
                txtBSRCode.BackColor = Color.Honeydew;
                txtChallanNo.BackColor = Color.Honeydew;
                if (txtTransferVoucherNo.Enabled == true)
                    txtTransferVoucherNo.BackColor = Color.Honeydew;
                if (txtChequeNo.Enabled == true)
                    txtChequeNo.BackColor = Color.Honeydew;
                txtTDS.BackColor = Color.Honeydew;
                txtSurcharge.BackColor = Color.Honeydew;
                txtEducationCess.BackColor = Color.Honeydew;
                txtInterests.BackColor = Color.Honeydew;
                txtOthers.BackColor = Color.Honeydew;
                txtInterestAllocated.BackColor = Color.Honeydew;
                txtOthersAllocated.BackColor = Color.Honeydew;
                txtRemarks.BackColor = Color.Honeydew;
                cmbMinorHead.BackColor = Color.Honeydew;
                txtFee.BackColor = Color.Honeydew;
            }
        }
        #endregion

        #region BackgroundColorChangeDeductee
        private void BackgroundColorChangeDeductee(string Mode)
        {
            if (Mode.ToUpper() == "ADD MODE")
            {
                txtDeducteeName.BackColor = Color.White;
                txtDeducteePAN.BackColor = Color.White;
                cmbDeducteeCode.BackColor = Color.White;
                mskDeducteeDate.BackColor = Color.White;
                txtDeducteeAmountOfPayment.BackColor = Color.White;
                txtDeducteeRate.BackColor = Color.White;
                txtDeducteeIncometax.BackColor = Color.White;
                txtDeducteeSurcharge.BackColor = Color.White;
                txtDeducteeCess.BackColor = Color.White;
                txtDeducteeTaxDeposited.BackColor = Color.White;
                cmbDDSection.BackColor = Color.White;
                txtCertificateNo.BackColor = Color.White;
                cmbRemarks.BackColor = Color.White;
            }
            else if (Mode.ToUpper() == "EDIT MODE")
            {
                txtDeducteeName.BackColor = Color.Honeydew;
                txtDeducteePAN.BackColor = Color.Honeydew;
                cmbDeducteeCode.BackColor = Color.Honeydew;
                mskDeducteeDate.BackColor = Color.Honeydew;
                txtDeducteeAmountOfPayment.BackColor = Color.Honeydew;
                txtDeducteeRate.BackColor = Color.Honeydew;
                txtDeducteeIncometax.BackColor = Color.Honeydew;
                txtDeducteeSurcharge.BackColor = Color.Honeydew;
                txtDeducteeCess.BackColor = Color.Honeydew;
                txtDeducteeTaxDeposited.BackColor = Color.Honeydew;
                cmbDDSection.BackColor = Color.Honeydew;
                txtCertificateNo.BackColor = Color.Honeydew;
                cmbRemarks.BackColor = Color.Honeydew;
            }
        }
        #endregion 

        #region CleanOutputFolder
        private void CleanOutputFolder(long BasicInfoId)
        {
            try
            {
                // DELETE FILES OF SAME BASIC INFO FROM THE '\FVU files' FOLDER
                //strSQL = "SELECT OUTPUT_FILE_PATH + '\\' + LEFT(INPUT_FILE_NAME,8) AS FILE_PATH FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + BasicInfoId +
                //    "  AND OUTPUT_FILE_PATH <> ' ' ORDER BY  FG_LOG_ID DESC";
                strSQL = "SELECT OUTPUT_FILE_PATH + '\\' + LEFT(INPUT_FILE_NAME,7) AS FILE_PATH FROM TRN_FILE_GENERATION_LOG WHERE BASIC_INFO_ID = " + BasicInfoId +
                   "  AND OUTPUT_FILE_PATH <> ' ' ORDER BY  FG_LOG_ID DESC";
                string strInputFileName = Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
                //
                if (strInputFileName != "")
                {
                    // DELETE .txt FILES
                    //if (File.Exists(Path.Combine(newPath, Path.ChangeExtension(strInputFileName, ".txt"))) == true)
                    //    File.Delete(Path.Combine(newPath, Path.ChangeExtension(strInputFileName, ".txt")));
                    //// DELETE err FILES
                    if (File.Exists(strInputFileName + "_Electronic_Statement_Warning_File.html") == true)
                        File.Delete(strInputFileName + "_Electronic_Statement_Warning_File.html");
                    // DELETE err FILES
                    if (File.Exists(strInputFileName + "err.html") == true)
                        File.Delete(strInputFileName + "err.html");
                    // DELETE _BH FILES
                    if (File.Exists(strInputFileName + "_BH.html") == true)
                        File.Delete(strInputFileName + "_BH.html");
                    // DELETE _CD FILES
                    if (File.Exists(strInputFileName + "_CD.html") == true)
                        File.Delete(strInputFileName + "_CD.html");
                    // DELETE _PAN_Statistics FILES
                    if (File.Exists(strInputFileName + "_PAN_Statistics.html") == true)
                        File.Delete(strInputFileName + "_PAN_Statistics.html");
                    // DELETE .html FILES
                    if (File.Exists(strInputFileName + ".html") == true)
                        File.Delete(strInputFileName + ".html");
                    // DELETE .err FILES
                    if (File.Exists(strInputFileName + ".err") == true)
                        File.Delete(strInputFileName + ".err");
                    // DELETE .fvu FILES
                    if (File.Exists(strInputFileName + ".fvu") == true)
                        File.Delete(strInputFileName + ".fvu");

                    //Added by Shrey Kejriwal on 03/01/2014
                    //Delete fvu.log files
                    if (File.Exists(strInputFileName + ".fvu.log") == true)
                        File.Delete(strInputFileName + ".fvu.log");


                    ////Added by Shrey Kejriwal on 03/01/2014

                    string strForm27APath = ReturnGeneratedForm27APath();

                    if (File.Exists(strForm27APath) == true)
                    {
                        //Deleting the existing form 27A pdf file --- only if it is not open.
                        if (TdsMan.T_isFileOpenOrReadOnly(ref strForm27APath) == false)
                            File.Delete(strForm27APath);
                    }
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region LoadChallanFormComponents
        private void LoadChallanFormComponents()
        {
            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) <= T_FinancialYearID.F2012_13ID)
            {
                lblMinor.Visible = false;
                cmbMinorHead.Visible = false;

                lblCheque.Visible = true;
                txtChequeNo.Visible = true;

                label57.Visible = true;
                cmbSection.Visible = true;
                lblSectionDisplay.Visible = false;
                
                lblSrlNo.Location = new Point(80, 4);
                txtChallanSrlNo.Location = new Point(165, 1);

                chkBookEntry.Location = new Point(226, 3);

                label57.Location = new Point(76, 32);
                cmbSection.Location = new Point(165, 28);
                lblSectionDisplay.Location = new Point(2, 54);

                label13.Location = new Point(31, 10);
                mskDateOfPayment.Location = new Point(168, 8);

                lblBSRCode.Location = new Point(8, 35);
                txtBSRCode.Location = new Point(164, 32);

                lblTrVchNo.Location = new Point(8, 57);
                txtTransferVoucherNo.Location = new Point(164, 54);
                txtChallanNo.Location = new Point(164, 54);

                lblCheque.Location = new Point(51, 79);
                txtChequeNo.Location = new Point(164, 76);

                label48.Location = new Point(93, 101);
                txtTDS.Location = new Point(164, 98);

                label49.Location = new Point(93, 123);
                txtSurcharge.Location = new Point(164, 120);

                label51.Location = new Point(63, 145);
                txtEducationCess.Location = new Point(164, 142);

                label50.Location = new Point(75, 167);
                txtInterests.Location = new Point(164, 164);

                label66.Location = new Point(75, 189);
                txtFee.Location = new Point(164, 186);

                label55.Location = new Point(81, 212);
                txtOthers.Location = new Point(164, 209);

                label54.Location = new Point(29, 234);
                txtTotalTax.Location = new Point(164, 231);

                
                lblRemarks.Location = new Point(8, 258);
                txtRemarks.Location = new Point(68, 255);

                lblInterestAllocated.Location = new Point(43, 285);
                txtInterestAllocated.Location = new Point(164, 281);

                lblOthersAllocated.Location = new Point(49, 306);
                txtOthersAllocated.Location = new Point(164, 303);

                pnlTop.Height = 73;

                pnlBottom.Location = new Point(1, 73);
                pnlBottom.Height = 348;

                cmbSection.Select();
            }
            else
            {
                if (chkBookEntry.Checked == false)
                {
                    lblMinor.Visible = true;
                    cmbMinorHead.Visible = true;
                }
                lblCheque.Visible = false;
                txtChequeNo.Visible = false;

                label57.Visible = false;
                cmbSection.Visible = false;
                lblSectionDisplay.Visible = false;


                lblSrlNo.Location = new Point(80, 4);
                txtChallanSrlNo.Location = new Point(165, 1);

                chkBookEntry.Location = new Point(226, 3);


                label13.Location = new Point(31, 10);
                mskDateOfPayment.Location = new Point(164, 8);

                lblBSRCode.Location = new Point(8, 37);
                txtBSRCode.Location = new Point(164, 34);

                lblTrVchNo.Location = new Point(8, 61);
                txtTransferVoucherNo.Location = new Point(164, 58);
                txtChallanNo.Location = new Point(164, 58);


                label48.Location = new Point(93, 110);
                txtTDS.Location = new Point(164, 107);

                label49.Location = new Point(93, 134);
                txtSurcharge.Location = new Point(164, 131);

                label51.Location = new Point(63, 158);
                txtEducationCess.Location = new Point(164, 155);

                label50.Location = new Point(75, 183);
                txtInterests.Location = new Point(164, 180);

                label66.Location = new Point(75, 205);
                txtFee.Location = new Point(164, 202);

                label55.Location = new Point(81, 228);
                txtOthers.Location = new Point(164, 225);

                label54.Location = new Point(89, 252);
                txtTotalTax.Location = new Point(164, 249);

                lblMinor.Location = new Point(-2, 277);
                cmbMinorHead.Location = new Point(164, 273);

                lblRemarks.Location = new Point(8, 310); 
                txtRemarks.Location = new Point(68, 307);

                lblInterestAllocated.Location = new Point(43, 341);
                txtInterestAllocated.Location = new Point(164, 337);

                lblOthersAllocated.Location = new Point(49, 362);
                txtOthersAllocated.Location = new Point(164, 359);

                pnlTop.Height = 28;

                pnlBottom.Location = new Point(1, 28);
                pnlBottom.Height = 395;

                //lblMinor.Visible = false;
                //cmbMinorHead.Visible = false;
                mskDateOfPayment.Select();
                //                
            }
        }
        #endregion

        #region ReturnGeneratedForm27APath
        private string ReturnGeneratedForm27APath()
        {
            //Added by Shrey Kejriwal on 04/01/2014

            //Generating Form 27A File Name
            string str27APdfFileName = "27A_" + txtTAN.Text + "_" + T_FormNo.F26Q + "_" + cmbQuarter.Text + "_" + cmbFinancialYear.Text.Replace("-", "") + ".pdf";

            //Generating Full path
            return Path.Combine(txtOutputFolder.Text, str27APdfFileName);

        }
        #endregion

        #region ShowRate
        private void ShowRate()
        {
            // Added by Shrey Kejriwal on 11/02/2014
            // Not showing rate when data is getting populated for edit mode
            if (blnShowPANHelp == false)
                return;

            //------------------------------------------
            //CHECKING FOR MST_PREFERENCES 
            //------------------------------------------
            if (TDSMAN.Classes.TDSMAN.T_ShowNonSalaryTDSRate == true)
            {
                //-------------------------------
                //---Initialize the Section Id
                //-------------------------------
                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2013_14ID)
                    strSectionNo = cmbDDSection.Text.ToString();
                else
                    strSectionNo = txtViewChallanDetailsSection.Text.ToString();

                //---------------------------------
                //FETCHING TDS RATE FOR NONSALARY 
                //---------------------------------
                if (TdsMan.CalcTDSRateForNonSalary(strSectionNo, mskDeducteeDate.Text, txtDeducteePAN.Text) > 0)
                    txtDeducteeRate.Text = string.Format("{0:0.0000}", TDSMAN.Classes.TDSMAN.T_NonSalaryTDSRate);
                else
                    txtDeducteeRate.Text = "0.0000";
            }
        }
        #endregion 


        #endregion

    }
}

