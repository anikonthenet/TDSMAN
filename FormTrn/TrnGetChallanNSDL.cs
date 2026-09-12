

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
using System.Globalization;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

using Excel = Microsoft.Office.Interop.Excel;

using TDSMAN.Classes;
#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnGetChallanNSDL : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnGetChallanNSDL()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables declaration

        //--
        string strFolderPath = "";
        string strHtml = "";
        //
        DMLService dmlService = new DMLService();
        DateService dtService = new DateService();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //--            
        ToolTip tllTip = new ToolTip();
        //--
        string strSQL = "";
        TracesConnect objTracesConnect = new TracesConnect();
        string strExcelPath = "", strExcelFile = "";

        enum enmRequestType
        {
            Login,
            PanValidation,
            LogOff,
            CINPP,
            CINParticulars,
            ConsumptionDetails,
            BINPP,
            BINParticulars,
            BINPP_Details
        }
        //
        //--            
        int j = 0, J = 0;
        int intRowIndex = 0;
        int intColumnIndex = 0;

        private string CurrentCaptchaId = "";
        //----
        #endregion

        #region SysBackup_Load
        private void SysBackup_Load(object sender, EventArgs e)
        {
            //lblTitle.Text = "Download Challan from OLTAS(NSDL)";
            lblTitle.Text = "Download Challan";
            //--
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //BtnRefresh.Visible = false;
            //-----------
            //-- COMPANY
            //-----------
            //string strSQL = @"SELECT MAX(ID),
            //                         COMPANY 
            //                  FROM (SELECT COMPANY_ID AS ID,
            //                                COMPANY_NAME + ' [' + TAN_NO + ']' AS COMPANY
            //                         FROM   MST_COMPANY 
            //                         WHERE  INACTIVE_FLAG = 0
            //                         UNION 
            //                         SELECT COR_TRN_COMPANY.BATCH_HEADER_ID AS ID,
            //                                COR_TRN_COMPANY.COMPANY_NAME + ' [' + COR_TRN_COMPANY.TAN_NO + ']'  AS COMPANY
            //                         FROM COR_TRN_COMPANY) AS ALL_COMPANIES
            //                  GROUP BY COMPANY
            //                  ORDER BY COMPANY";
            //--
            strSQL = @"SELECT MST_CATEGORY.CATEGORY_CODE 
                    FROM MST_COMPANY, 
                        TRN_BASIC_INFO,     
                        MST_CATEGORY 
                    WHERE MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID 
                    AND MST_COMPANY.D_CATEGORY_ID = MST_CATEGORY.CATEGORY_ID 
                    AND TRN_BASIC_INFO.BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId;
            //
            if (Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)) == "A" || Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)) == "S")
            {
                chkBIN.Visible = true;
            }
            else
            {
                chkBIN.Visible = false;
            }
            //--
            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            txtCompany.Text = TDSMAN.Classes.TDSMAN.T_pCompanyName;
            strSQL = "SELECT TAN_NO, LOGIN_ID, USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmnService.J_Right(txtCompany.Text.Trim(), 11), 10)) + "'";
            IDataReader drdLoadTan = null;
            drdLoadTan = dmlService.J_ExecSqlReturnReader(strSQL);
            //--
            //blnShowHelp = false;
            txtTANNo.Text = cmnService.J_ReplaceQuote(cmnService.J_Left(cmnService.J_Right(txtCompany.Text.Trim(), 11), 10));
            //blnShowHelp = true;
            //--
            if (drdLoadTan == null)
            {
                //blnShowHelp = false;
                txtTANNo.Text = cmnService.J_ReplaceQuote(cmnService.J_Left(cmnService.J_Right(txtCompany.Text.Trim(), 11), 10));
                //blnShowHelp = true;
                txtUserID.Text = "";
                txtPassword.Text = "";
                return;
            }
            else
            {
                while (drdLoadTan.Read())
                {
                    //blnShowHelp = false;
                    txtTANNo.Text = drdLoadTan["TAN_NO"].ToString();
                    //blnShowHelp = true;
                    txtUserID.Text = drdLoadTan["LOGIN_ID"].ToString();
                    txtPassword.Text =  drdLoadTan["USER_PASSWORD"].ToString();
                    //
                    txtTracesCaptcha.Select();
                }
            }
            drdLoadTan.Close();
            drdLoadTan.Dispose();
            //if (chkValidationType.Checked)
            //    InitializeCaptcha(enmValidationType.BinView);
            //else
            //    InitializeCaptcha(enmValidationType.CinView);
            //
            //grpLoginDetails.Visible = true;
            this.Cursor = Cursors.WaitCursor;
            //
            //InitializeCaptcha();
            pctTracesCaptcha.Image = Properties.Resources.captcha_loading;
            if (!bgWorkerLoadCaptcha.IsBusy)
                bgWorkerLoadCaptcha.RunWorkerAsync();
            //
            //
            mskViewFileDownloadFrom.Select();
            //
            //txtTANNo.Text= cmnService.J_ReplaceQuote(cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10));
            //--
            //btnChallan.Enabled = false;
            //btnChallan.BackColor = Color.LightGray;
            //--
            btnValidate.Enabled = false;
            btnValidate.BackColor = Color.LightGray;

            this.Cursor = Cursors.Default;
        }
        #endregion

        #region BtnBackup_Click
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dgvStatementList.RowCount == 0)
                {
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("No records for import.");
                    return;
                }
                //--
                long lngGetMaxChallan = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId, J_QueryType.DirectQuery)));
                string ChallanDate = "", ChallanNo = "", BSRCode = "", strSQL="";
                double ChallanAmount = 0;int MinorCode = 0, SrlNo=0, ImportedChallanNo = 0;
                int intChallanAlreadyExists = 0, intBookEntry = 0;
                //
                int iColMatched = 0;
                if (chkBIN.Checked == true)
                    iColMatched = 7;
                else
                    iColMatched = 8;
                //
                for (int i = 0; i < dgvStatementList.RowCount; i++)
                {
                    if (Convert.ToString(dgvStatementList.Rows[i].Cells[iColMatched].Value) == "100")// T_ChallanStatusResult.AMOUNT_MATCHED)
                    {
                        //SrlNo = cmnService.J_ReturnInt32Value(dgvStatementList.Rows[i].Cells[3].Value.ToString());
                        //ChallanDate = dgvStatementList.Rows[i].Cells[2].Value.ToString();
                        if (chkBIN.Checked == true)
                        {
                            ChallanDate = Convert.ToString(Convert.ToDateTime(dgvStatementList.Rows[i].Cells[0].Value.ToString()).ToString("dd/MM/yyyy"));
                            ChallanNo = dgvStatementList.Rows[i].Cells[1].Value.ToString();
                            BSRCode = dgvStatementList.Rows[i].Cells[4].Value.ToString();
                            ChallanAmount = cmnService.J_ReturnDoubleValue(dgvStatementList.Rows[i].Cells[3].Value.ToString());
                            intBookEntry = 1;
                        }
                        else
                        {
                            ChallanDate = Convert.ToString(Convert.ToDateTime(dgvStatementList.Rows[i].Cells[2].Value.ToString()).ToString("dd/MM/yyyy"));
                            ChallanNo = dgvStatementList.Rows[i].Cells[3].Value.ToString();
                            BSRCode = dgvStatementList.Rows[i].Cells[0].Value.ToString() + dgvStatementList.Rows[i].Cells[1].Value.ToString();
                            ChallanAmount = cmnService.J_ReturnDoubleValue(dgvStatementList.Rows[i].Cells[6].Value.ToString());
                        }
                        MinorCode = 1;// cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MINOR_HEAD_ID FROM MST_MINOR_HEAD WHERE MINOR_HEAD_CODE ='" + dgvStatementList.Rows[i].Cells[6].Value.ToString() + "'")));
                                      //                        
                                      //strSQL = "SELECT COUNT(*) FROM TRN_CHALLAN WHERE CHALLAN_NO = '" + ChallanNo + "' AND BSR_CODE = '" + BSRCode + "' AND DEPOSIT_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(ChallanDate) + cmnService.J_DateOperator() + "";
                        strSQL = "SELECT COUNT(*) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + " AND CHALLAN_NO = '" + ChallanNo + "' AND BSR_CODE = '" + BSRCode + "' AND DEPOSIT_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(ChallanDate) + cmnService.J_DateOperator() + "";
                        if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery))) == 0)
                        {
                            lngGetMaxChallan = lngGetMaxChallan + 1;
                            //for (int j = 0; j < dgvStatementList.RowCount; j++)
                            //{
                            //    if (cmnService.J_ReturnInt32Value(dgvStatementList.Rows[j].Cells[0].Value.ToString()) == SrlNo)
                            //    {
                            //        ChallanAmount = cmnService.J_ReturnDoubleValue(dgvStatementList.Rows[j].Cells[6].Value.ToString());
                            //    }
                            //}
                            //
                            strSQL = "INSERT INTO TRN_CHALLAN (" +
                                             "            BASIC_INFO_ID," +
                                             "            SL_NO," +
                                             "            DEPOSIT_DATE," +
                                             "            BSR_CODE," +
                                             "            CHALLAN_NO," +
                                             "            TDS," +
                                             "            TOT_TAX," +
                                             "            BOOK_ENTRY," +
                                             "            MINOR_HEAD_ID) " +
                                             "     VALUES(" + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + "," +
                                             "            " + lngGetMaxChallan + "," +
                                             "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(ChallanDate) + cmnService.J_DateOperator() + "," +
                                             "           '" + cmnService.J_ReplaceQuote(BSRCode) + "'," +
                                             "           '" + cmnService.J_ReplaceQuote(ChallanNo) + "'," +
                                             "            " + cmnService.J_ReturnDoubleValue(ChallanAmount) + "," +
                                             "            " + cmnService.J_ReturnDoubleValue(ChallanAmount) + "," +
                                             "            " + intBookEntry + "," +
                                             "            " + MinorCode + ")";
                            //-----------------------------------------------------------
                            dmlService.J_BeginTransaction();
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                            dmlService.J_Commit();
                            //
                            ImportedChallanNo = ImportedChallanNo + 1;
                        }
                        else
                        {
                            intChallanAlreadyExists = intChallanAlreadyExists + 1;
                        }
                    }
                }
                //--
                System.Threading.Thread.Sleep(100);
                this.Cursor = Cursors.Default;
                //
                if (ImportedChallanNo == 0)
                {
                    if (intChallanAlreadyExists == 0)
                        cmnService.J_UserMessage("No challan(s) imported.");
                    else if (intChallanAlreadyExists > 0)
                        cmnService.J_UserMessage("No challan(s) imported, as it is already present.");

                }
                else
                {
                    if (intChallanAlreadyExists == 0)
                        cmnService.J_UserMessage(ImportedChallanNo + " challan(s) imported.");
                    else if (intChallanAlreadyExists > 0)
                        cmnService.J_UserMessage(ImportedChallanNo + " challan(s) imported and " + intChallanAlreadyExists  + " challan(s) already present.");
                    //
                    dmlService.Dispose();
                    this.Close();
                    this.Dispose();
                }
                ////-
                //dmlService.Dispose();
                //this.Close();
                //this.Dispose();
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        //-- DOWNLOAD CSI FILE
        #region BtnRefresh_Click 
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            mskViewFileDownloadFrom.Text = "";
            mskViewFileDownloadTo.Text = "";
            InitializeCaptcha();
            btnChallan.Enabled = true;
            grpTracesDetails.Enabled = true;
            grpBackUp.Enabled = true;
            btnChallan.BackColor = Color.Lavender;
            dgvStatementList.Columns.Clear();
            dgvResult.Columns.Clear();
            mskViewFileDownloadFrom.Select();
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (BtnRefresh.Text.ToUpper().Trim() == "CONTINUE")
            {
                grpCaptcha.Visible = false;
                //lblBottomMessage.Text = "Security introduced by IT Dept. for CSI file download w.e.f. from Dec 2016. Enter correct text & continue.";            
                BtnSave.Enabled = true;
                BtnRefresh.Text = "&Download";
                txtCaptchaCode.Text = "";
                //
                txtCaptchaCode.Select();                 
            }
            else 
            {
              this.Close();
              this.Dispose();
            }
            
            
        }
        #endregion

        #region cmbCompany_KeyPress
        private void cmbCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region mskViewFileDownloadFrom_KeyPress
        private void mskViewFileDownloadFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region mskViewFileDownloadTo_KeyPress
        private void mskViewFileDownloadTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion



        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnViewChallanInformationOnline");
            Tan.ShowDialog();
            //--------------
            //cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #region lnkSearchByTAN_MouseMove
        private void lnkSearchByTAN_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(lnkSearchByTAN, "Click here to search the Company by TAN");
        }
        #endregion


        #region InitializeCaptcha
        private void InitializeCaptcha()
        {
            try
            {
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    return;
                }
                    //----------------------------------------------------
                    objTracesConnect = new TracesConnect();
                    //Stream imgStream = objTracesConnect.MakeInitialRequest();
                    //Image img = Image.FromStream(imgStream);
                    //this.pctTracesCaptcha.Image = img;
                    var captcha = objTracesConnect.MakeInitialRequest_NEW();
                    this.CurrentCaptchaId = captcha.CaptchaId;
                    Image captchaImage = captcha.CaptchaImage;
                this.pctTracesCaptcha.Image = captchaImage;
                //-------------------------------------------------------
                txtTracesCaptcha.Text = "";
                    txtTANNo.Select();
            }
            catch (Exception err)
            {
                picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion



        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            InitializeCaptcha();
            txtCaptchaCode.Select();
        }
        #endregion
        
        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=fEDSUM29LU8&list=PLy1JUN9HgGMxvrbc8Zf2dVCTkf4FQEEjT");
        }
        #endregion

        #region PopulateDatagridView
        void PopulateDatagridView(DataTable dsRecords)
        {
            // DATA BIND TO GRIDVIEW CONTROL
            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = null;
            dgvStatementList.DataSource = dsRecords;
            //---------------------------------------
            //CHECKING IF RECORD EXISTS OR NOT

            if (dsRecords.Rows.Count <= 0)
            {
                cmnService.J_UserMessage("No data available for the specified search criteria");
            }
            //---------------------------------------
            // CREATE DOWNLOAD BUTTON & PROGRESSBAR CONTROL COLUMNS 
            //------------------------------------------------------------
            // dgvStatementList.Columns[0].Width = 0;
            // dgvStatementList.Columns[0].Visible = false;
            //-- No
            dgvStatementList.Columns[0].Width =40;
            dgvStatementList.Columns[0].ReadOnly = true;
            // dgvStatementList.Columns[1].Width = 0;
            // dgvStatementList.Columns[1].Visible = false;

            //-- Challan Tender Date
            dgvStatementList.Columns[1].ReadOnly = true;
            dgvStatementList.Columns[1].Width =90;

            //-- Challan Serial No
            dgvStatementList.Columns[2].Width =80;
            dgvStatementList.Columns[2].ReadOnly = true;

            //-- BSR Code
            dgvStatementList.Columns[3].Width = 80;
            dgvStatementList.Columns[3].ReadOnly = true;

            //-- Received Date
            dgvStatementList.Columns[4].Width = 90;
            dgvStatementList.Columns[4].ReadOnly = true;

            //-- Major Head Code
            dgvStatementList.Columns[5].Visible = false;

            //-- Minor Head Code
            dgvStatementList.Columns[6].Width = 90;
            dgvStatementList.Columns[6].ReadOnly = true;

            //-- Nature of Payment
            dgvStatementList.Columns[7].Width = 250;
            dgvStatementList.Columns[7].ReadOnly = true;

            //dgvStatementList.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvStatementList.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvStatementList.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvStatementList.Columns[7].ReadOnly = false;

            DataGridViewTextBoxColumn btn = new DataGridViewTextBoxColumn();
            dgvStatementList.Columns.Add(btn);
            dgvStatementList.Columns[8].HeaderText = "Enter Challan Amount";
            //  btn.HeaderCell.GetContentBounds = "View Details";

            //DataGridViewLinkColumn btn = new DataGridViewLinkColumn();
            //dgvStatementList.Columns.Add(btn);
            //btn.HeaderText = "";
            ////btn.Text = "View Details";
            //btn.Text = "Match Challan Amount";
            //btn.Name = "lnkDetails";
            //btn.Width = 150;
            //------------------------------------------------------------
            //btn.UseColumnTextForLinkValue = true;
            //------------------------------------------------------------
            //DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            //dgvStatementList.Columns.Add(prg);
            //prg.Name = "";
            //prg.ProgressBarColor = Color.LightGreen;
            //------------------------------------------------------------
            // SET FOCUS 
            if (dsRecords.Rows.Count > 0)
            {
                dgvStatementList.Rows[0].Cells[6].Selected = true;
                //
                dgvStatementList.CurrentCell = dgvStatementList.Rows[0].Cells[6];
                dgvStatementList.BeginEdit(true);
            }                       
        }
        #endregion

        #region btnChallan_Click
        private void btnChallan_Click(object sender, EventArgs e)
        {
            try
            {
                //----------------------------
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
                    return;
                }
                //
                //if (cmbCompany.SelectedIndex <= 0)
                //{
                //    cmnService.J_UserMessage("Select the Company");
                //    cmbCompany.Select();
                //    return;
                //}
                //
                if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadFrom, "Challan Date From - Cannot be Blank") == true)
                    return;
                //
                if (dtService.J_IsDateValid(mskViewFileDownloadFrom) == false)
                {
                    cmnService.J_UserMessage("Incorrect Format of the Challan Date From");
                    mskViewFileDownloadFrom.Select();
                    return;
                }
                //
                if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadTo, "Challan Date To - Cannot be Blank") == true)
                    return;
                //
                if (dtService.J_IsDateValid(mskViewFileDownloadTo) == false)
                {
                    cmnService.J_UserMessage("Incorrect Format of the Challan Date To");
                    mskViewFileDownloadTo.Select();
                    return;
                }
                // FROM DATE & TO DATE DURATION 24 MONTHS
                if (TdsMan.T_DateDiff(T_DATE_DIFF.MONTH, dtService.J_ConvertddMMyyyy(mskViewFileDownloadFrom), dtService.J_ConvertddMMyyyy(mskViewFileDownloadTo)) > 24)
                {
                    cmnService.J_UserMessage("Period selected should be within 24 months");
                    mskViewFileDownloadFrom.Select();
                    return;
                }
                //--

                if (string.IsNullOrEmpty(txtCaptchaCode.Text))
                {
                    cmnService.J_UserMessage("Enter Captcha Code");
                    txtCaptchaCode.Select();
                    return;

                }
                btnChallan.Text = "Wait ...";
                ChallanQuery chnl = new ChallanQuery();
                //chnl.TAN = cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10);
                chnl.TAN = cmnService.J_Left(cmnService.J_Right(txtCompany.Text.Trim(), 11), 10);
                chnl.FromDate = mskViewFileDownloadFrom.Text;
                chnl.ToDate = mskViewFileDownloadTo.Text;
                //
                TracesResponse res = objTracesConnect.SearchChallanDetails(chnl, txtCaptchaCode.Text, out strHtml);
                //
                if (res.Respons == enmResponse.Success)
                {
                    DataTable dsRecord = (DataTable)res.CustomeTypes;
                    PopulateDatagridView(dsRecord);
                    //
                    btnValidate.Enabled = true;
                    btnValidate.BackColor = Color.Lavender;
                    //
                    btnChallan.Enabled = false;
                    btnChallan.BackColor = Color.LightGray;
                }
                else
                {
                    cmnService.J_UserMessage(res.Message);
                }

                btnChallan.Text = "View Challan";
            }
            catch (Exception ERR)
            {

                btnChallan.Text = "View Challan";
            }
        }
        #endregion

        #region  btnValidate_Click
        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                btnValidate.Text = "Wait ...";
                List<ChallanQuery> listChlnQuery = new List<ChallanQuery>();
                //
                for (int i = 0; i < dgvStatementList.RowCount; i++)
                {
                    ChallanQuery ChallanQuery = new ChallanQuery();
                    //ChallanQuery.TAN = cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10);
                    ChallanQuery.TAN = txtCompany.Text.Substring(txtCompany.Text.Length - 11, 10);


                    if (Convert.ToString(dgvStatementList.Rows[i].Cells[8].Value) != "")
                    {
                        string strVal = Convert.ToString(dgvStatementList.Rows[i].Cells[7].Value);
                        //    
                        ChallanQuery.BSRCode = Convert.ToString(dgvStatementList.Rows[i].Cells[3].Value);
                        ChallanQuery.ChallanDate = Convert.ToString(dgvStatementList.Rows[i].Cells[1].Value);
                        ChallanQuery.ChallanNo = Convert.ToString(dgvStatementList.Rows[i].Cells[2].Value);
                        ChallanQuery.ChallanAmount = Convert.ToString(dgvStatementList.Rows[i].Cells[8].Value).Replace(".00", "");
                        //--
                        listChlnQuery.Add(ChallanQuery);
                    }
                }
                //===============================
                if (listChlnQuery.Count > 0)
                {
                    TracesResponse chnlQuery = objTracesConnect.getChallanStatus(listChlnQuery, strHtml);

                    dgvResult.Columns.Clear();
                    dgvResult.DataSource = null;
                    dgvResult.DataSource = (DataTable)chnlQuery.CustomeTypes;

                    //-- No
                    dgvResult.Columns[0].Width = 40;
                    dgvResult.Columns[0].ReadOnly = true;
                    // dgvStatementList.Columns[1].Width = 0;
                    // dgvStatementList.Columns[1].Visible = false;

                    //-- Challan Tender Date
                    dgvResult.Columns[1].ReadOnly = true;
                    dgvResult.Columns[1].Width = 90;

                    //-- Challan Serial No
                    dgvResult.Columns[2].Width = 80;
                    dgvResult.Columns[2].ReadOnly = true;

                    //-- BSR Code
                    dgvResult.Columns[3].Width = 80;
                    dgvResult.Columns[3].ReadOnly = true;

                    //-- Received Date
                    dgvResult.Columns[4].Width = 90;
                    dgvResult.Columns[4].ReadOnly = true;

                    //-- Major Head Code
                    dgvResult.Columns[5].Visible = false;

                    //-- Minor Head Code
                    dgvResult.Columns[6].Width = 90;
                    dgvResult.Columns[6].ReadOnly = true;

                    //-- Nature of Payment
                    dgvResult.Columns[7].Width = 180;
                    dgvResult.Columns[7].ReadOnly = true;

                    //-- Result
                    dgvResult.Columns[8].Width = 180;
                    dgvResult.Columns[8].ReadOnly = true;
                    //--
                    //for (int i = 0; i < dgvResult.RowCount; i++)
                    //{
                    //    if(Convert.ToString(dgvResult.Rows[i].Cells[8].Value) == T_ChallanStatusResult.AMOUNT_MATCHED)
                    //    {
                    //        dgvResult.Rows[i].Cells[8].
                    //    }
                    //    else if (Convert.ToString(dgvResult.Rows[i].Cells[8].Value) == T_ChallanStatusResult.AMOUNT_NOT_MATCHED)
                    //    {

                    //    }
                    //}
                    foreach (DataGridViewRow row in dgvResult.Rows)
                    {
                        if (Convert.ToString(row.Cells[8].Value) == T_ChallanStatusResult.AMOUNT_MATCHED)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else if (Convert.ToString(row.Cells[8].Value) == T_ChallanStatusResult.AMOUNT_NOT_MATCHED)
                        {
                            row.DefaultCellStyle.BackColor = Color.IndianRed;
                        }
                    }
                    //--
                }
                else
                {
                    cmnService.J_UserMessage("No Challan Amount entered");
                    return;
                }
                //--
                btnValidate.Text = "Validate Amount";
                btnValidate.Enabled = false;
                btnValidate.BackColor = Color.LightGray;
            }
            catch (Exception err)
            {
                btnValidate.Text = "Validate Amount";
            }
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(btnChallan.Enabled==false)
            {
                InitializeCaptcha();
                btnChallan.Enabled = true;
                btnChallan.BackColor = Color.Lavender;
                dgvStatementList.Columns.Clear();
                dgvResult.Columns.Clear();
            }
        }

        #endregion

        #region BtnSave_MouseClick
        private void BtnSave_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                cntxtMnuDownload.Show(BtnSave, new Point(e.X, e.Y));
        }
        #endregion

        #region mnuExportChallanToExcel_Click
        private void mnuExportChallanToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string strSerialNo = "[Running Serial No]", strTDS = "[TDS]", strBSRCode = "[BSR Code/24G Receipt No]",  strTotalTaxDeposited = "[Total Tax Deposited]", strTransferVoucherChallanSerialNo = "[Transfer Voucher/Challan Serial No (409)]"; ;
                string strDateOnTaxDeposited = "[Date on Tax Deposited (dd/mm/yyyy)]",  strMinorHead = "[Minor head]";
                //
                if (dgvStatementList.RowCount == 0)
                {
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("No records for import.");
                    return;
                }
                //--
                // Create a new instance of FolderBrowserDialog.
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                // A new folder button will display in FolderBrowserDialog.
                folderBrowserDlg.ShowNewFolderButton = true;
                //Show FolderBrowserDialog
                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                if (dlgResult.Equals(DialogResult.OK))
                {
                    //Show selected folder path in textbox1.
                    strExcelPath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                else
                    return;
                //--
                //strExcelFile = txtCompany.Text.Substring(txtCompany.Text.Length - 11, 10) + "-CHALLAN_OLTAS.XLS";
                strExcelFile = txtCompany.Text.Substring(txtCompany.Text.Length - 11, 10) + "-CHALLAN_DETAILS.XLS";
                strExcelPath = Path.Combine(strExcelPath, strExcelFile);
                //--
                this.Cursor = Cursors.WaitCursor;
                long lngGetMaxChallan = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId, J_QueryType.DirectQuery)));
                string ChallanDate = "", ChallanNo = "", BSRCode = "", strSQL = ""; int MinorCode = 0;
                double ChallanAmount = 0; int SrlNo = 0, ImportedChallanNo = 0;
                //--
                #region T_tblTEMP_DOWNLOAD_CHALLAN
                dmlService.J_BeginTransaction();
                //MessageBox.Show("2");
                if (dmlService.J_IsDatabaseObjectExist( TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS ) == true)
                {
                    //MessageBox.Show("2.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS + "";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //MessageBox.Show("3");
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS ) == false)
                {
                    //MessageBox.Show("3.1");
                    strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS + @" (
                                            " + cmnService.J_GetDataType("DOWNLOAD_CHALLANS_ID", J_Identity.YES) + @",
                                            " + cmnService.J_GetDataType("RUNNING_SERIAL_NO", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("DATE_TAX_DEPOSITED", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("CHALLAN_SERIAL_NO", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("BSR_CODE", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("TDS", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("TOTAL_TAX_DEPOSITED", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("MINOR_HEAD", J_ColumnType.String, 255) + @")";
                    //MessageBox.Show(strSQL);
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                #endregion
                //--
                //
                int iColMatched = 0;
                if (chkBIN.Checked == true)
                    iColMatched = 7;
                else
                    iColMatched = 8;
                //
                for (int i = 0; i < dgvStatementList.RowCount; i++)
                {
                    if (Convert.ToString(dgvStatementList.Rows[i].Cells[iColMatched].Value) == "100")//T_ChallanStatusResult.AMOUNT_MATCHED)
                    {
                        //SrlNo = cmnService.J_ReturnInt32Value(dgvStatementList.Rows[i].Cells[0].Value.ToString());
                        //ChallanDate = dgvStatementList.Rows[i].Cells[1].Value.ToString();
                        //ChallanNo = dgvStatementList.Rows[i].Cells[2].Value.ToString();
                        //BSRCode = dgvStatementList.Rows[i].Cells[3].Value.ToString();
                        ////ChallanAmount = cmnService.J_ReturnDoubleValue(dgvStatementList.Rows[0].Cells[9].Value.ToString());
                        ////MinorCode = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MINOR_HEAD_ID FROM MST_MINOR_HEAD WHERE MINOR_HEAD_CODE ='" + dgvStatementList.Rows[i].Cells[6].Value.ToString() + "'")));
                        //MinorCode =  dgvStatementList.Rows[i].Cells[6].Value.ToString();
                        if (chkBIN.Checked == true)
                        {
                            ChallanDate = Convert.ToString(Convert.ToDateTime(dgvStatementList.Rows[i].Cells[0].Value.ToString()).ToString("dd/MM/yyyy"));
                            ChallanNo = dgvStatementList.Rows[i].Cells[1].Value.ToString();
                            BSRCode = dgvStatementList.Rows[i].Cells[4].Value.ToString();
                            ChallanAmount = cmnService.J_ReturnDoubleValue(dgvStatementList.Rows[i].Cells[3].Value.ToString());
                        }
                        else
                        {
                            ChallanDate = Convert.ToString(Convert.ToDateTime(dgvStatementList.Rows[i].Cells[2].Value.ToString()).ToString("dd/MM/yyyy"));
                            ChallanNo = dgvStatementList.Rows[i].Cells[3].Value.ToString();
                            BSRCode = dgvStatementList.Rows[i].Cells[0].Value.ToString() + dgvStatementList.Rows[i].Cells[1].Value.ToString();
                            ChallanAmount = cmnService.J_ReturnDoubleValue(dgvStatementList.Rows[i].Cells[6].Value.ToString());
                        }
                        MinorCode = 1;
                        //
                        //strSQL = "SELECT COUNT(*) FROM TRN_CHALLAN WHERE CHALLAN_NO = '" + ChallanNo + "' AND BSR_CODE = '" + BSRCode + "' AND DEPOSIT_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(ChallanDate) + cmnService.J_DateOperator() + "";
                        //if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery))) == 0)
                        //{
                        lngGetMaxChallan = lngGetMaxChallan + 1;
                        SrlNo = SrlNo + 1;
                        //for (int j = 0; j < dgvStatementList.RowCount; j++)
                        //{
                        //    if (cmnService.J_ReturnInt32Value(dgvStatementList.Rows[j].Cells[0].Value.ToString()) == SrlNo)
                        //    {
                        //        ChallanAmount = cmnService.J_ReturnDoubleValue(dgvStatementList.Rows[j].Cells[8].Value.ToString());
                        //    }
                        //}
                        //
                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS + "(" +
                                            "            RUNNING_SERIAL_NO," +
                                            "            DATE_TAX_DEPOSITED," +
                                            "            CHALLAN_SERIAL_NO," +
                                            "            BSR_CODE," +
                                            "            TDS," +
                                            "            TOTAL_TAX_DEPOSITED," +
                                            "            MINOR_HEAD) " +
                                            "     VALUES('" + SrlNo + "'," +
                                            "            '" + ChallanDate + "'," +
                                            "            '" + cmnService.J_ReplaceQuote(ChallanNo) + "'," +
                                            "            '" + cmnService.J_ReplaceQuote(BSRCode) + "'," +
                                            "            '" + cmnService.J_ReturnDoubleValue(ChallanAmount) + "'," +
                                            "            '" + cmnService.J_ReturnDoubleValue(ChallanAmount) + "'," +
                                            "            '" + MinorCode + "')";
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        dmlService.J_Commit();
                        //
                        ImportedChallanNo = ImportedChallanNo + 1;
                        //}
                    }
                }
                //--
                //System.Threading.Thread.Sleep(100)
                //--
                if (CREATE_EXCEL_FILE(strExcelPath) == false)
                {
                    cmnService.J_UserMessage("Some error occurred while creating Excel file");
                    this.Cursor = Cursors.Default;
                    return;
                }
                //--
                if (CREATE_NEW_WORKSHEET(strExcelPath, "CHALLAN DETAILS") == false)
                {
                    cmnService.J_UserMessage("Some error occurred while creating Excel sheet");
                    this.Cursor = Cursors.Default;
                    return;
                }
                //--- DELETE WORKSHEET
                if (DELETE_WORKSHEET(strExcelPath, "Sheet1") == false)
                {
                    this.Cursor = Cursors.Default;
                    //return;
                }
                //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                if (DELETE_WORKSHEET(strExcelPath, "Sheet2") == false) //return;
                {
                    this.Cursor = Cursors.Default;
                }
                //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                if (DELETE_WORKSHEET(strExcelPath, "Sheet3") == false) //return;
                {
                    this.Cursor = Cursors.Default;
                }
                //
                strSQL = "SELECT RUNNING_SERIAL_NO    AS " + strSerialNo + "," +
                    "            TDS                  AS " + strTDS + "," +
                    "            TOTAL_TAX_DEPOSITED  AS " + strTotalTaxDeposited + "," +
                    "            BSR_CODE             AS " + strBSRCode + "," +
                    "            DATE_TAX_DEPOSITED   AS " + strDateOnTaxDeposited + " ," +
                    "            CHALLAN_SERIAL_NO    AS " + strTransferVoucherChallanSerialNo + " ," +
                    "            MINOR_HEAD           AS " + strMinorHead + " " + 
                    "     FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS + " ORDER BY RUNNING_SERIAL_NO";
                if (ExportToExcelFromSQL(strSQL, "CHALLAN DETAILS") == false)
                {
                    cmnService.J_UserMessage("Export data failed ..", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    this.Cursor = Cursors.Default;
                    return;
                }
                //--;
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("Data Exported Successfully..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                //-
                dmlService.Dispose();
                this.Close();
                this.Dispose();
                //
                System.Diagnostics.Process.Start(strExcelPath);
            }
            catch(Exception err)
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion


        #region CREATE EXCEL FILE
        // SOURCE PATH : http://csharp.net-informations.com/excel/csharp-create-excel.htm
        private bool CREATE_EXCEL_FILE(string ExcelFilePath)
        {
            try
            {
                //MessageBox.Show("5");
                //--
                //if (rbnCSVOption.Checked == true)
                //{
                //    if (Directory.Exists(ExcelFilePath) == false)
                //        Directory.CreateDirectory(ExcelFilePath);
                //    return true; //-- 2019/01/22
                //}
                //MessageBox.Show("5.0.1.1");
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                //Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                //--
                //MessageBox.Show("5.0.1.2");
                //string strExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "." + cmbFileType.Text;
                //--

                //xlApp = new Excel.ApplicationClass();
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);

                //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                //xlWorkSheet.Cells[1, 1] = "http://csharp.net-informations.com";
                //--
                //------------------------------------
                if (Path.GetExtension(ExcelFilePath).ToUpper() == ".XLS")
                    xlWorkBook.SaveAs(ExcelFilePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                else if (Path.GetExtension(ExcelFilePath).ToUpper() == ".XLSX")
                    xlWorkBook.SaveAs(ExcelFilePath, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                //------------------------------------
                //MessageBox.Show("5.0.1.3");
                xlWorkBook.Close(true, misValue, misValue);
                //MessageBox.Show("5.0.1.4");
                xlApp.Quit();
                //MessageBox.Show("5.0.1.5");

                //ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                //MessageBox.Show("5.0.1.6");
                ReleaseObject(xlApp);
                //MessageBox.Show("5.0.1.7");
                //--
                return true;
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
                //cmnService.J_UserMessage(ERR.Message);
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region ReleaseObject
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion


        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath, string WorksheetName)
        {
            try
            {
                //
                //if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
                //Microsoft.Office.Interop.Excel.Worksheet WrkSheet;
                //WrkSheet =   (Microsoft.Office.Interop.Excel.Worksheet)Globals.ThisWorkbook.Worksheets.Add(missing, missing, missing, missing);
                //
                //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //string myPath = @"" + ExcelFilePath;
                //excelApp.Workbooks.Open(myPath);
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                excelapp.DisplayAlerts = false;

                //if (excelapp == null) throw new Exception("Can't start Excel");
                if (excelapp == null) return false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                //if I create a new file and then add a worksheet,
                //it will exit normally (i.e. if you uncomment the next two lines
                //and comment out the .Open() line below):
                //Excel.Workbook wb = wbs.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                //wb.SaveAs(filename, m, m, m, m, m, 
                //          Excel.XlSaveAsAccessMode.xlExclusive,
                //          m, m, m, m, m);

                //but if I open an existing file and add a worksheet,
                //it won't exit (leaves zombie excel processes)
                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                //Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                //                             m, m, m, m, m, m,
                //                             Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook,
                //                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                Microsoft.Office.Interop.Excel.Worksheet wsnew = sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                wsnew.Name = WorksheetName;

                //N.B. it doesn't help if I try specifying the parameters in Add() above

                wb.Save();
                wb.Close(m, m, m);

                //overkill to do GC so many times, but shows that doesn't fix it
                //GC();
                //cleanup COM references
                //changing these all to FinalReleaseComObject doesn't help either
                //while (Marshal.ReleaseComObject(wsnew) > 0) { }
                wsnew = null;
                //while (Marshal.ReleaseComObject(sheets) > 0) { }
                sheets = null;
                //while (Marshal.ReleaseComObject(wb) > 0) { }
                wb = null;
                //while (Marshal.ReleaseComObject(wbs) > 0) { }
                wbs = null;
                //GC();
                excelapp.Quit();
                //while (Marshal.ReleaseComObject(excelapp) > 0) { }
                excelapp = null;
                //GC();

                return true;
            }
            catch (Exception e)
            {
                cmnService.J_UserMessage(e.Message);
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region DELETE WORKSHEET
        private bool DELETE_WORKSHEET(string ExcelFilePath, string ExcelSheet)
        {
            try
            {
                //if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22        
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                foreach (Microsoft.Office.Interop.Excel.Worksheet ws in wb.Sheets)
                {
                    if (ws.Name.ToString().Trim() == ExcelSheet)
                    {
                        ws.Delete();
                        break;
                    }
                }

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch(Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region btnGoTraces_Click
        private void btnGoTraces_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    btnCloseTraces.Select();
                    return;
                }
                //--
                if (!ValidateFields()) return;
                //--
                //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER 

                strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";
                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    //insering new record in the tan login master
                    strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

                    dmlService.J_ExecSql(strSQL);
                }
                else
                {
                    //updating the existing record in the master

                    strSQL = "UPDATE MST_TAN_ACCOUNT " +
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                             "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";

                    dmlService.J_ExecSql(strSQL);
                }
                //#################
                this.Cursor = Cursors.WaitCursor;
                //--
                TracesLogin objLogin = new TracesLogin();
                objLogin.UserID = txtUserID.Text;
                objLogin.Password = txtPassword.Text;
                objLogin.TAN = txtTANNo.Text;
                objLogin.CaptchaCode = txtTracesCaptcha.Text;
                objLogin.CaptchaId = this.CurrentCaptchaId; //-- 2026/04/08
                //---------
                ArrayList objList = new ArrayList();
                objList.Add(enmRequestType.Login);
                objList.Add(objLogin);
                //-------------------------------------------
                //pgTimer.Start();
                //-------------------------------------------
                if (!bgwTracesChallanVerification.IsBusy)
                    bgwTracesChallanVerification.RunWorkerAsync(objList);
                //
            }
            catch //(Exception err)
            {
                //cmnService.J_UserMessage(err.Message);
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("We are not able to receive the response from the TRACES webiste.\n It is requested to check your details at TRACES website with the given parameters");

            }
        }
        #endregion

        #region ValidateFields
        bool ValidateFields()
        {
            if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadFrom, "Challan Date From - Cannot be Blank") == true)
                return false;
            //
            if (dtService.J_IsDateValid(mskViewFileDownloadFrom) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the Challan Date From");
                mskViewFileDownloadFrom.Select();
                return false;
            }
            //
            if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadTo, "Challan Date To - Cannot be Blank") == true)
                return false;
            //
            if (dtService.J_IsDateValid(mskViewFileDownloadTo) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the Challan Date To");
                mskViewFileDownloadTo.Select();
                return false;
            }

            if (dtService.J_IsDateGreater(ref mskViewFileDownloadFrom, ref mskViewFileDownloadTo, J_ShowMessage.NO) == false)
            {
                cmnService.J_UserMessage("From Date cannot be greater than To Date");
                mskViewFileDownloadFrom.Select();
                return false;
            }

            // FROM DATE & TO DATE DURATION 24 MONTHS
            if (TdsMan.T_DateDiff(T_DATE_DIFF.MONTH, dtService.J_ConvertddMMyyyy(mskViewFileDownloadFrom), dtService.J_ConvertddMMyyyy(mskViewFileDownloadTo)) > 24)
            {
                cmnService.J_UserMessage("Period selected should be within 24 months");
                mskViewFileDownloadFrom.Select();
                return false;
            }
            //--
            if (string.IsNullOrEmpty(txtTANNo.Text))
            {
                cmnService.J_UserMessage("Please enter TAN");
                txtTANNo.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtUserID.Text))
            //{
            //    cmnService.J_UserMessage("Please enter User ID");
            //    txtUserID.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                cmnService.J_UserMessage("Please enter Password");
                txtPassword.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTracesCaptcha.Text))
            {
                cmnService.J_UserMessage("Please enter Captcha Code");
                txtCaptchaCode.Focus();
                return false;
            }

            return true;
        }
        #endregion


        #region bgwTracesChallanVerification_DoWork
        private void bgwTracesChallanVerification_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayList objList = (ArrayList)e.Argument;
            ArrayList objRetval = new ArrayList();
            //-------------------------------------------------------
            enmRequestType enReqType = (enmRequestType)objList[0];
            TracesResponse objResponse;
            //-------------------------------------------------------
            switch (enReqType)
            {
                // LOGIN REQUEST
                case enmRequestType.Login:
                    //objResponse = objTracesConnect.makeLoginToTRACES((TracesLogin)objList[1]);
                    objResponse = objTracesConnect.makeLoginToTraces_New((TracesLogin)objList[1]);
                    //---------------------------------------------------
                    objRetval.Add(enmRequestType.Login);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    //---------------------------------------------------
                    break;
                // LIST OF CHALLAN ENQUIRY CIN - Period Payment 
                case enmRequestType.CINPP:
                    //TracesResponse response = objTracesConnect.CIN_Period_Payment((TracesData)objList[1]);
                    TracesResponse response = objTracesConnect.CIN_Period_Payment_New((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.CINPP);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;
                // LIST OF CHALLAN ENQUIRY CIN - CIN/BIN Particulars
                case enmRequestType.CINParticulars:
                    //response = objTracesConnect.CIN_CIN_BINParticulars((TracesData)objList[1]);
                    response = objTracesConnect.CIN_CIN_BINParticulars_New((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.CINParticulars);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                case enmRequestType.BINPP:
                    //response = objTracesConnect.BIN_Period_Payment((TracesData)objList[1]);
                    response = objTracesConnect.BIN_Period_Payment_New((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.BINPP);
                    objRetval.Add(response);
                    e.Result = objRetval;

                    break;

                case enmRequestType.BINParticulars:
                    //response = objTracesConnect.BIN_Particulars((TracesData)objList[1]);
                    response = objTracesConnect.BIN_Particulars_New((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.BINParticulars);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;


                // VIEW CONSUMPTION DETAILS
                case enmRequestType.ConsumptionDetails:
                    if (chkBIN.Checked == true) //-- 2026/02/17
                    {
                        response = objTracesConnect.RequestForConsumptionDetailsBIN((TracesData)objList[1]);
                    }
                    else
                    {
                        response = objTracesConnect.RequestForConsumptionDetailsCIN((TracesData)objList[1]);
                    }
                    objRetval.Add(enmRequestType.ConsumptionDetails);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                // VIEW CONSUMPTION DETAILS
                case enmRequestType.BINPP_Details:

                    response = objTracesConnect.RequestForBIN_Details((List<string>)objList[1]);
                    objRetval.Add(enmRequestType.BINPP_Details);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;


                //REQUEST FOR LOG OFF
                case enmRequestType.LogOff:
                    objResponse = objTracesConnect.Logoff();
                    objRetval.Add(enmRequestType.LogOff);
                    objRetval.Add(objResponse);
                    e.Result = objRetval;
                    break;
            }
        }
        #endregion

        #region bgwTracesChallanVerification_RunWorkerCompleted
        private void bgwTracesChallanVerification_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ArrayList objMessage = (ArrayList)e.Result;
                enmRequestType enmReqType = (enmRequestType)objMessage[0];
                TracesResponse objResponse = (TracesResponse)objMessage[1];
                //---------------------------------------------------------
                switch (enmReqType)
                {
                    case enmRequestType.Login:
                        //pgTimer.Stop();
                        //pgTimer.Interval = 1000;
                        //pBar.Value = 99;
                        //---------------------------------------------------
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            grpTracesDetails.Enabled = false;
                            grpBackUp.Enabled = false;
                            //
                            pgLoadGrid.Start();
                            //    ShowHideLoginDetails(enmRequestType.CINPP);
                        }
                        //
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            //pgTimer.Stop();
                            //pBar.Value = 100;
                            this.Cursor = Cursors.Default;
                            grpTracesDetails.Enabled = true;
                            grpBackUp.Enabled = true;
                            cmnService.J_UserMessage(objResponse.Message);
                            InitializeCaptcha();
                            return;
                        }
                        else
                        {
                            //---------------------------------------------------
                            //pBar.Value = 0;
                            //pgTimer.Stop();

                            //---------------------------------------------------
                        }
                        break;

                    case enmRequestType.CINPP:
                    case enmRequestType.CINParticulars:
                        //this.pgTimer.Stop();
                        //pBar.Value = 100;
                        //-------------------------------------------------------
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            //ShowHideLoginDetails(enmRequestType.LogOff);
                            this.Cursor = Cursors.Default;
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        //--------------------------------------------------------
                        DataTable dTable = (DataTable)objResponse.CustomeTypes;

                        PopulateDatagridView(dTable, enmReqType);
                        this.Cursor = Cursors.Default;
                        break;

                    case enmRequestType.BINPP:
                    case enmRequestType.BINParticulars:
                        //this.pgTimer.Stop();
                        //pBar.Value = 100;
                        //-------------------------------------------------------
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            //ShowHideLoginDetails(enmRequestType.LogOff);
                            this.Cursor = Cursors.Default;
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        //--------------------------------------------------------
                        dTable = (DataTable)objResponse.CustomeTypes;

                        PopulateDatagridView(dTable, enmReqType);
                        break;

                    case enmRequestType.BINPP_Details:
                        pgTimerGrid.Stop();
                        dgvStatementList.Rows[intRowIndex].Cells[7].Value = 100;
                        //-----------------------------------------------------
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            //ShowHideLoginDetails(enmRequestType.LogOff);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage(objResponse.Message);
                            //
                            dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex - 3].Selected = true;
                            dgvStatementList.CurrentCell = dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex - 3];
                            dgvStatementList.BeginEdit(true);
                            return;
                        }
                        //-----------------------------------------------------
                        dTable = (DataTable)objResponse.CustomeTypes;
                        //------------------------------------------------------
                        dgvStatementList.Columns.Clear();
                        dgvStatementList.DataSource = null;
                        if (dTable.Rows.Count <= 0)
                        {
                            cmnService.J_UserMessage("No Data Available. Please check after 3 working days from the date of filing of the statement at TIN-FC");
                            return;
                        }
                        dgvStatementList.DataSource = dTable;
                        dgvStatementList.Columns[0].Width = 110;
                        dgvStatementList.Columns[1].Width = 110;
                        dgvStatementList.Columns[2].Width = 60;
                        dgvStatementList.Columns[3].Width = 90;
                        dgvStatementList.Columns[6].Width = 150;
                        dgvStatementList.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvStatementList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvStatementList.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvStatementList.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvStatementList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvStatementList.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dgvStatementList.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvStatementList.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvStatementList.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        break;

                    case enmRequestType.ConsumptionDetails:
                        pgTimerGrid.Stop();
                        //dgvStatementList.Rows[intRowIndex].Cells[8].Value = 100;
                        //-----------------------------------------------------
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            this.Cursor = Cursors.Default;
                            //ShowHideLoginDetails(enmRequestType.LogOff);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage(objResponse.Message);
                            //
                            if (chkBIN.Checked == true)
                            {
                                dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex - 3].Selected = true;
                                dgvStatementList.CurrentCell = dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex - 3];
                                //
                                dgvStatementList.Rows[intRowIndex].Cells[7].Value = 0;
                            }
                            else
                            {
                                dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex - 1].Selected = true;
                                dgvStatementList.CurrentCell = dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex - 1];
                                //
                                dgvStatementList.Rows[intRowIndex].Cells[8].Value = 0;
                            }
                            dgvStatementList.BeginEdit(true);

                            return;
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            if (chkBIN.Checked == true)
                                dgvStatementList.Rows[intRowIndex].Cells[7].Value = 100;
                            else
                                dgvStatementList.Rows[intRowIndex].Cells[8].Value = 100;
                        }

                        //-----------------------------------------------------
                        //dTable = (DataTable)objResponse.CustomeTypes;
                        //------------------------------------------------------
                        //dgvConsumption.Columns.Clear();
                        //dgvConsumption.DataSource = null;
                        //dgvConsumption.DataSource = dTable;
                        //dgvConsumption.Columns[0].Width = 150;
                        //dgvConsumption.Columns[1].Width = 120;
                        //dgvConsumption.Columns[4].Width = 120;
                        //dgvConsumption.Columns[6].Width = 150;
                        //dgvConsumption.Columns[7].Width = 150;
                        //dgvStatementList.Rows[intRowIndex].Cells[9].Value = "Amount Matched";
                        //dgvConsumption.Columns[7].Visible = false;

                        //if (dTable.Rows.Count <= 0)
                        //    cmnService.J_UserMessage("Records not found");

                        break;
                    case enmRequestType.LogOff:
                        //this.pgTimer.Stop();
                        //pBar.Value = 100;

                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtTANNo.Text = "";
                        txtCaptchaCode.Text = "";
                        //grpDownloadList.Visible = false;
                        //grpLoginDetails.Visible = true;
                        // grpProgress.Visible = true;
                        BtnSave.Enabled = true;
                        BtnSave.BackColor = Color.Lavender;
                        InitializeCaptcha();
                        //pBar.Value = 0;
                        break;
                }
                //-------------------------------------------------------
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region pgLoadGrid_Tick
        private void pgLoadGrid_Tick(object sender, EventArgs e)
        {
            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = null;

            TracesData objData = new TracesData();
            //-------------------------------------
            //objData.FromChallanDepositDate = strChallanFromDate;
            //objData.ToChallanDepositDate = strChallanToDate;
            objData.FromChallanDepositDate = dtService.J_ConvertddMMyyyy(mskViewFileDownloadFrom).ToString("dd-MMM-yyyy");
            objData.ToChallanDepositDate = dtService.J_ConvertddMMyyyy(mskViewFileDownloadTo).ToString("dd-MMM-yyyy");
            objData.ChallanStatus = "A";
            //-- VALIDATION -----------------------
            ////if (!IsValidCINPeriodPayment(ref objData)) return;
            ////------------------------------------------------
            ArrayList objList = new ArrayList();
            //
            if (chkBIN.Checked == true)
            {
                objList.Add(enmRequestType.BINPP);
                objList.Add(objData);
            }
            else
            {
                objList.Add(enmRequestType.CINPP);
                objList.Add(objData);
            }
            //
            if (!bgwTracesChallanVerification.IsBusy)
                bgwTracesChallanVerification.RunWorkerAsync(objList);
            pgLoadGrid.Stop();
        }
        #endregion


        #region dgvStatementList_CellClick
        private void dgvStatementList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //GETTING ROW & COLUMN INDEX OF SELECT ROW
            intRowIndex = e.RowIndex;
            intColumnIndex = e.ColumnIndex;
            //-------------------------------------------------------------------------------
            if (e.ColumnIndex == dgvStatementList.Columns["lnkDetails"].Index && e.RowIndex >= 0)
            {
                //if (Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "view details")
                if (Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "match challan amount")
                {
                    //SEARCH ON CIN
                    //if (rbnCINSearch.Checked)
                    //{
                    //dgvStatementList.Columns.Clear();
                    //dgvStatementList.DataSource = null;
                    //--------------------------------
                    string strAmount = "";
                    //  VALIDATION AMOUNT - update on 08/09/2016
                    if(chkBIN.Checked == true)
                        strAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value) == "" ? "0.00" : Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value);
                    else
                        strAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value) == "" ? "0.00" : Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value);
                    //
                    if (!cmnService.J_IsNumeric(strAmount))
                    {
                        cmnService.J_UserMessage("Please Enter Challan Amount");
                        dgvStatementList.Rows[e.RowIndex].Cells[6].Selected = true;

                        dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[6];
                        dgvStatementList.BeginEdit(true);
                        return;
                    }
                    else
                    {
                        if (Convert.ToDouble(strAmount) <= 0)
                        {
                            cmnService.J_UserMessage("Please Enter Challan Amount");
                            if (chkBIN.Checked == true)
                            {
                                dgvStatementList.Rows[e.RowIndex].Cells[3].Selected = true;
                                dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[3];
                            }
                            else
                            {
                                dgvStatementList.Rows[e.RowIndex].Cells[6].Selected = true;
                                dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[6];
                            }
                            //
                            dgvStatementList.BeginEdit(true);
                            //
                            return;
                        }
                    }

                    if (chkBIN.Checked == true)
                        dgvStatementList.Rows[e.RowIndex].Cells[3].Value = string.Format("{0:0.00}", Convert.ToDouble(strAmount));
                    else
                        dgvStatementList.Rows[e.RowIndex].Cells[6].Value = string.Format("{0:0.00}", Convert.ToDouble(strAmount));

                    ArrayList objList = new ArrayList();
                    TracesData objData = new TracesData();
                    //---------------------------------
                    if (chkBIN.Checked == true)
                    {
                        //objData.BSRCode = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[0].Value) + Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[1].Value);
                        //objData.FromChallanDepositDate = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[2].Value);
                        //objData.ChallanSerialNo = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value);
                        //objData.ChallanAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value);
                        //objData.PRN_NO = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[5].Value);

                        objData.TransferVoucherDate = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[0].Value);
                        objData.DDOSerialNumber = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[1].Value);
                        objData.ChallanAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value);
                        objData.ReciptNumber = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[4].Value);
                        objData.RecordID = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[5].Value);
                    }
                    else
                    {
                        objData.BSRCode = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[0].Value) + Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[1].Value);
                        objData.FromChallanDepositDate = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[2].Value);
                        objData.ChallanSerialNo = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value);
                        objData.ChallanAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value);
                        objData.PRN_NO = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[5].Value);
                    }
                    //---------------------------------------------


                    pgTimerGrid.Start();
                    objList.Add(enmRequestType.ConsumptionDetails);
                    objList.Add(objData);

                    if (!bgwTracesChallanVerification.IsBusy)
                        bgwTracesChallanVerification.RunWorkerAsync(objList);

                    //}
                    //-------------------------------------------------------------------------------
                    //FOR BIN SEARCH
                    //if (rbnBINSearch.Checked)
                    //{
                    //    dgvConsumption.Columns.Clear();
                    //    dgvConsumption.DataSource = null;
                    //    //-------------------------------

                    //    //  VALIDATION AMOUNT - update on 08/09/2016
                    //    string strAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value) == "" ? "0.00" : Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value);

                    //    if (!cmnService.J_IsNumeric(strAmount))
                    //    {
                    //        cmnService.J_UserMessage("Please Enter Amount");
                    //        dgvStatementList.Rows[e.RowIndex].Cells[3].Selected = true;

                    //        dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[3];
                    //        dgvStatementList.BeginEdit(true);
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        if (Convert.ToDouble(strAmount) <= 0)
                    //        {
                    //            cmnService.J_UserMessage("Please Enter Amount");
                    //            dgvStatementList.Rows[e.RowIndex].Cells[3].Selected = true;

                    //            dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[3];
                    //            dgvStatementList.BeginEdit(true);

                    //            return;
                    //        }
                    //    }


                    //    dgvStatementList.Rows[e.RowIndex].Cells[3].Value = string.Format("{0:0.00}", Convert.ToDouble(strAmount));


                    //    pgTimerGrid.Start();
                    //    ArrayList objList = new ArrayList();
                    //    List<string> strParam = new List<string>();

                    //    //RECORD_ID
                    //    strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[5].Value));
                    //    //RECEIPT NO
                    //    strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[4].Value));
                    //    //DDOS NO
                    //    strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[1].Value));
                    //    //CHALLAN AMOUNT
                    //    strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value));
                    //    //DATE OF DEPOSIT
                    //    strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[0].Value));


                    //    objList.Add(enmRequestType.BINPP_Details);
                    //    objList.Add(strParam);

                    //    if (!bgWorker.IsBusy)
                    //        bgWorker.RunWorkerAsync(objList);

                    //}

                }
            }
        }
        #endregion

        #region btnTracesCaptchaRefresh_Click
        private void btnTracesCaptchaRefresh_Click(object sender, EventArgs e)
        {

            InitializeCaptcha();
        }
        #endregion

        private void mnuExportChallanToCSV_Click(object sender, EventArgs e)
        {
            try
            {
                string strSerialNo = "[Running Serial No]", strTDS = "[TDS]", strBSRCode = "[BSR Code/24G Receipt No]", strTotalTaxDeposited = "[Total Tax Deposited]", strTransferVoucherChallanSerialNo = "[Transfer Voucher/Challan Serial No (409)]"; ;
                string strDateOnTaxDeposited = "[Date on Tax Deposited (dd/mm/yyyy)]", strMinorHead = "[Minor head]";
                //
                if (dgvStatementList.RowCount == 0)
                {
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("No records for import.");
                    return;
                }
                //--
                // Create a new instance of FolderBrowserDialog.
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                // A new folder button will display in FolderBrowserDialog.
                folderBrowserDlg.ShowNewFolderButton = true;
                //Show FolderBrowserDialog
                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                if (dlgResult.Equals(DialogResult.OK))
                {
                    //Show selected folder path in textbox1.
                    strExcelPath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                else
                    return;
                //--
                //strExcelFile = txtCompany.Text.Substring(txtCompany.Text.Length - 11, 10) + "-CHALLAN_OLTAS.XLS";
                strExcelFile = txtCompany.Text.Substring(txtCompany.Text.Length - 11, 10) + "-CHALLAN_DETAILS.CSV";
                strExcelPath = Path.Combine(strExcelPath, strExcelFile);
                //--
                this.Cursor = Cursors.WaitCursor;
                long lngGetMaxChallan = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId, J_QueryType.DirectQuery)));
                string ChallanDate = "", ChallanNo = "", BSRCode = "", strSQL = ""; int MinorCode = 0;
                double ChallanAmount = 0; int SrlNo = 0, ImportedChallanNo = 0;
                //--
                #region T_tblTEMP_DOWNLOAD_CHALLAN
                dmlService.J_BeginTransaction();
                //MessageBox.Show("2");
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS) == true)
                {
                    //MessageBox.Show("2.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS + "";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //MessageBox.Show("3");
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS) == false)
                {
                    //MessageBox.Show("3.1");
                    strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS + @" (
                                            " + cmnService.J_GetDataType("DOWNLOAD_CHALLANS_ID", J_Identity.YES) + @",
                                            " + cmnService.J_GetDataType("RUNNING_SERIAL_NO", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("DATE_TAX_DEPOSITED", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("CHALLAN_SERIAL_NO", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("BSR_CODE", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("TDS", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("TOTAL_TAX_DEPOSITED", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("MINOR_HEAD", J_ColumnType.String, 255) + @")";
                    //MessageBox.Show(strSQL);
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                #endregion
                //--
                //
                int iColMatched = 0;
                if (chkBIN.Checked == true)
                    iColMatched = 7;
                else
                    iColMatched = 8;
                //
                for (int i = 0; i < dgvStatementList.RowCount; i++)
                {
                    if (Convert.ToString(dgvStatementList.Rows[i].Cells[iColMatched].Value) == "100")//T_ChallanStatusResult.AMOUNT_MATCHED)
                    {
                        //SrlNo = cmnService.J_ReturnInt32Value(dgvStatementList.Rows[i].Cells[0].Value.ToString());
                        //ChallanDate = dgvStatementList.Rows[i].Cells[1].Value.ToString();
                        //ChallanNo = dgvStatementList.Rows[i].Cells[2].Value.ToString();
                        //BSRCode = dgvStatementList.Rows[i].Cells[3].Value.ToString();
                        ////ChallanAmount = cmnService.J_ReturnDoubleValue(dgvStatementList.Rows[0].Cells[9].Value.ToString());
                        ////MinorCode = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MINOR_HEAD_ID FROM MST_MINOR_HEAD WHERE MINOR_HEAD_CODE ='" + dgvStatementList.Rows[i].Cells[6].Value.ToString() + "'")));
                        //MinorCode =  dgvStatementList.Rows[i].Cells[6].Value.ToString();
                        if (chkBIN.Checked == true)
                        {
                            ChallanDate = Convert.ToString(Convert.ToDateTime(dgvStatementList.Rows[i].Cells[0].Value.ToString()).ToString("dd/MM/yyyy"));
                            ChallanNo = dgvStatementList.Rows[i].Cells[1].Value.ToString();
                            BSRCode = dgvStatementList.Rows[i].Cells[4].Value.ToString();
                            ChallanAmount = cmnService.J_ReturnDoubleValue(dgvStatementList.Rows[i].Cells[3].Value.ToString());
                        }
                        else
                        {
                            ChallanDate = Convert.ToString(Convert.ToDateTime(dgvStatementList.Rows[i].Cells[2].Value.ToString()).ToString("dd/MM/yyyy"));
                            ChallanNo = dgvStatementList.Rows[i].Cells[3].Value.ToString();
                            BSRCode = dgvStatementList.Rows[i].Cells[0].Value.ToString() + dgvStatementList.Rows[i].Cells[1].Value.ToString();
                            ChallanAmount = cmnService.J_ReturnDoubleValue(dgvStatementList.Rows[i].Cells[6].Value.ToString());
                        }
                        MinorCode = 1;
                        //
                        //strSQL = "SELECT COUNT(*) FROM TRN_CHALLAN WHERE CHALLAN_NO = '" + ChallanNo + "' AND BSR_CODE = '" + BSRCode + "' AND DEPOSIT_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(ChallanDate) + cmnService.J_DateOperator() + "";
                        //if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery))) == 0)
                        //{
                        lngGetMaxChallan = lngGetMaxChallan + 1;
                        SrlNo = SrlNo + 1;
                        //for (int j = 0; j < dgvStatementList.RowCount; j++)
                        //{
                        //    if (cmnService.J_ReturnInt32Value(dgvStatementList.Rows[j].Cells[0].Value.ToString()) == SrlNo)
                        //    {
                        //        ChallanAmount = cmnService.J_ReturnDoubleValue(dgvStatementList.Rows[j].Cells[8].Value.ToString());
                        //    }
                        //}
                        //
                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS + "(" +
                                            "            RUNNING_SERIAL_NO," +
                                            "            DATE_TAX_DEPOSITED," +
                                            "            CHALLAN_SERIAL_NO," +
                                            "            BSR_CODE," +
                                            "            TDS," +
                                            "            TOTAL_TAX_DEPOSITED," +
                                            "            MINOR_HEAD) " +
                                            "     VALUES('" + SrlNo + "'," +
                                            "            '" + ChallanDate + "'," +
                                            "            '" + cmnService.J_ReplaceQuote(ChallanNo) + "'," +
                                            "            '" + cmnService.J_ReplaceQuote(BSRCode) + "'," +
                                            "            '" + cmnService.J_ReturnDoubleValue(ChallanAmount) + "'," +
                                            "            '" + cmnService.J_ReturnDoubleValue(ChallanAmount) + "'," +
                                            "            '" + MinorCode + "')";
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        dmlService.J_Commit();
                        //
                        ImportedChallanNo = ImportedChallanNo + 1;
                        //}
                    }
                }
                //--
                //System.Threading.Thread.Sleep(100)
                //--
                //if (CREATE_EXCEL_FILE(strExcelPath) == false)
                //{
                //    cmnService.J_UserMessage("Some error occurred while creating Excel file");
                //    this.Cursor = Cursors.Default;
                //    return;
                //}
                ////--
                //if (CREATE_NEW_WORKSHEET(strExcelPath, "CHALLAN DETAILS") == false)
                //{
                //    cmnService.J_UserMessage("Some error occurred while creating Excel sheet");
                //    this.Cursor = Cursors.Default;
                //    return;
                //}
                ////--- DELETE WORKSHEET
                //if (DELETE_WORKSHEET(strExcelPath, "Sheet1") == false)
                //{
                //    this.Cursor = Cursors.Default;
                //    //return;
                //}
                ////prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                //if (DELETE_WORKSHEET(strExcelPath, "Sheet2") == false) //return;
                //{
                //    this.Cursor = Cursors.Default;
                //}
                ////prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                //if (DELETE_WORKSHEET(strExcelPath, "Sheet3") == false) //return;
                //{
                //    this.Cursor = Cursors.Default;
                //}
                //
                strSQL = "SELECT RUNNING_SERIAL_NO    AS " + strSerialNo + "," +
                    "            TDS                  AS " + strTDS + "," +
                    "            TOTAL_TAX_DEPOSITED  AS " + strTotalTaxDeposited + "," +
                    "            BSR_CODE             AS " + strBSRCode + "," +
                    "            DATE_TAX_DEPOSITED   AS " + strDateOnTaxDeposited + " ," +
                    "            CHALLAN_SERIAL_NO    AS " + strTransferVoucherChallanSerialNo + " ," +
                    "            MINOR_HEAD           AS " + strMinorHead + " " +
                    "     FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DOWNLOAD_CHALLANS + " ORDER BY RUNNING_SERIAL_NO";
                if (ExportToCSV(strSQL, strExcelPath) == false)
                {
                    cmnService.J_UserMessage("Export data failed ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return;
                }
                //--;
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("Data Exported Successfully..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                //-
                dmlService.Dispose();
                this.Close();
                this.Dispose();
                //
                System.Diagnostics.Process.Start(strExcelPath);
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
            }
        }

        #region PopulateDatagridView
        void PopulateDatagridView(DataTable dsRecords, enmRequestType enmRecType)
        {
            // DATA BIND TO GRIDVIEW CONTROL
            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = null;
            dgvStatementList.DataSource = dsRecords;
            //---------------------------------------
            //CHECKING IF RECORD EXISTS OR NOT

            if (dsRecords == null)
            {
                cmnService.J_UserMessage("No data available for the specified search criteria");
                pgTimerMatchingRecords.Stop();
                return;
            }
            else
                pgTimerMatchingRecords.Start();
            //---------------------------------------
            // CREATE DOWNLOAD BUTTON & PROGRESSBAR CONTROL COLUMNS 
            //------------------------------------------------------------
            if (enmRecType == enmRequestType.CINPP || enmRecType == enmRequestType.CINParticulars)
            {
                dgvStatementList.Columns[0].Width = 80;
                //dgvStatementList.Columns[0].Visible = false;
                dgvStatementList.Columns[0].ReadOnly = true;
                dgvStatementList.Columns[1].Width = 80;
                //dgvStatementList.Columns[1].Visible = false;
                dgvStatementList.Columns[1].ReadOnly = true;
                dgvStatementList.Columns[2].Width = 100;
                dgvStatementList.Columns[2].ReadOnly = true;

                dgvStatementList.Columns[3].Width = 80;
                dgvStatementList.Columns[3].ReadOnly = true;
                dgvStatementList.Columns[4].ReadOnly = true;
                //dgvStatementList.Columns[5].ReadOnly = true;
                dgvStatementList.Columns[5].Visible = false;
                dgvStatementList.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatementList.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatementList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvStatementList.Columns[6].ReadOnly = false;



            }

            if (enmRecType == enmRequestType.BINPP)
            {
                dgvStatementList.Columns[0].Width = 150;
                dgvStatementList.Columns[1].Width = 150;
                //dgvStatementList.Columns[3].Visible = false;
                dgvStatementList.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatementList.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatementList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvStatementList.Columns[4].Visible = false;
                dgvStatementList.Columns[5].Visible = false;

                dgvStatementList.Columns[1].ReadOnly = true;
                dgvStatementList.Columns[2].ReadOnly = true;
                dgvStatementList.Columns[3].ReadOnly = false;
                dgvStatementList.Columns[4].ReadOnly = true;
                dgvStatementList.Columns[5].ReadOnly = true;


            }


            DataGridViewLinkColumn btn = new DataGridViewLinkColumn();
            dgvStatementList.Columns.Add(btn);
            btn.HeaderText = "";
            //btn.Text = "View Details";
            btn.Text = "Match Challan Amount";
            btn.Name = "lnkDetails";
            btn.Width = 150;
            //------------------------------------------------------------
            btn.UseColumnTextForLinkValue = true;
            //------------------------------------------------------------
            DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            dgvStatementList.Columns.Add(prg);
            prg.Name = "";
            prg.ProgressBarColor = Color.LightGreen;
            //------------------------------------------------------------
            // SET FOCUS 
            if (dsRecords.Rows.Count > 0)
            {
                if (enmRecType == enmRequestType.CINPP || enmRecType == enmRequestType.CINParticulars)
                {
                    dgvStatementList.Rows[0].Cells[6].Selected = true;

                    dgvStatementList.CurrentCell = dgvStatementList.Rows[0].Cells[6];
                    dgvStatementList.BeginEdit(true);
                }
                else
                {
                    dgvStatementList.Rows[0].Cells[3].Selected = true;

                    dgvStatementList.CurrentCell = dgvStatementList.Rows[0].Cells[3];
                    dgvStatementList.BeginEdit(true);
                }
            }


            //-- ARUP @ 2014/09/16
            //foreach (DataGridViewRow gridRow in dgvStatementList.Rows)
            //{
            //    if (gridRow.Cells["Challan Status"] != null)
            //    {
            //        DataGridViewCell BookButtonCell = gridRow.Cells["Challan Status"];
            //        //
            //        if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "UNCLAIMED")
            //            gridRow.Cells["lnkDetails"] = new DataGridViewTextBoxCell();
            //    }
            //}
            //--
        }
        #endregion

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }

        private void btnCloseTraces_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void BtnExport_Click(object sender, EventArgs e)
        {

        }


        #region ExportToExcelFromSQL
        private bool ExportToExcelFromSQL(string strSQL, string SheetName)
        {
            //-- 20/02/2018 --
            //GC.Collect();
            GC.WaitForPendingFinalizers();

            DataSet myDataSet;
            //----------------
            //
            try
            {
                //if (rbnCSVOption.Checked == true)  //-- 2019/01/22
                //{
                //    ExportToCSV(strSQL, Path.Combine(Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text), SheetName + ".csv"));
                //    return true;
                //}
                //else
                //{
                    myDataSet = new DataSet();
                    myDataSet = dmlService.J_ExecSqlReturnDataSet(strSQL);
                    //
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                    Microsoft.Office.Interop.Excel.Workbooks workbook = app.Workbooks;
                    //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 -
                    //
                    object m = Type.Missing;
                    Microsoft.Office.Interop.Excel.Workbook wb = workbook.Open(strExcelPath,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                    Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                    wsnew.Name = SheetName;
                    //

                    int colIndex = 0;
                    int rowIndex = 1;

                    foreach (DataColumn dc in myDataSet.Tables[0].Columns)
                    {
                        colIndex++;
                        wsnew.Cells[1, colIndex] = dc.ColumnName;
                    }
                    //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 -
                    foreach (DataRow dr in myDataSet.Tables[0].Rows)
                    {
                        rowIndex++;
                        colIndex = 0;

                        foreach (DataColumn dc in myDataSet.Tables[0].Columns)
                        {
                            colIndex++;
                            //wsnew.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                            //if (dr[dc.ColumnName].ToString().Length == 10 && dr[dc.ColumnName].ToString().Contains("/") == true)
                            if (dr[dc.ColumnName].ToString().Length == 10
                                && (cmnService.J_Mid(dr[dc.ColumnName].ToString(), 2, 1) == "/" || cmnService.J_Mid(dr[dc.ColumnName].ToString(), 2, 1) == "-")
                                && (cmnService.J_Mid(dr[dc.ColumnName].ToString(), 5, 1) == "/" || cmnService.J_Mid(dr[dc.ColumnName].ToString(), 5, 1) == "-"))
                                wsnew.Cells[rowIndex, colIndex] = "'" + dr[dc.ColumnName];
                            else
                                wsnew.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                        }
                    }

                    wsnew.Columns.AutoFit();

                    wb.Save();
                    wb.Close(m, m, m);

                    wb = null;
                    workbook = null;
                    //
                    wsnew = null;

                    //Marshal.ReleaseComObject(wsnew);
                    //Marshal.ReleaseComObject(wsnew);

                    //wsnew.Delete(); //-- 20/02/2018 --

                    app.Quit();
                    app = null;
                //}
                //--
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
            return true;
        }
        #endregion

        #region ExportToCSV
        protected bool ExportToCSV(string strSQL, string CSVFile)
        {
            try
            {
                //-- https://www.aspsnippets.com/Articles/Export-DataSet-or-DataTable-to-Word-Excel-PDF-and-CSV-Formats.aspx
                //Get the data from database into datatable
                //string strQuery = "select CustomerID, ContactName, City, PostalCode" +
                //     " from customers";
                //SqlCommand cmd = new SqlCommand(strQuery);
                //DataTable dt = GetData(cmd);
                System.Data.DataTable dt = dmlService.J_ExecSqlReturnDataTable(strSQL);
                //
                StreamWriter StreamWriter = cmnService.J_ReturnStreamWriter(CSVFile);
                //Response.Clear();
                //Response.Buffer = true;
                //Response.AddHeader("content-disposition",
                //    "attachment;filename=DataTable.csv");
                //Response.Charset = "";
                //Response.ContentType = "application/text";
                StringBuilder sb = new StringBuilder();
                //--
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Columns[k].ColumnName + ',');
                }
                //append new line
                sb.Append("\r\n");
                //--
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        //add separator
                        //sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                        if (dt.Rows[i][k].ToString().Contains(",") == true)
                            sb.Append('"' + dt.Rows[i][k].ToString() + '"' + ',');
                        else
                            sb.Append(dt.Rows[i][k].ToString() + ',');
                    }
                    //append new line
                    sb.Append("\r\n");
                }
                //Response.Output.Write(sb.ToString());
                cmnService.J_WriteLine(ref StreamWriter, sb.ToString());
                //
                StreamWriter.Flush();
                StreamWriter.Close();
                //Response.Flush();
                //Response.End();
                return true;
            }
            catch(Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
        }
        #endregion
    }
}

