

#region Refered Namespaces & Classes

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Globalization;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

using System.Net.Http;
using Newtonsoft.Json.Linq;

using Excel = Microsoft.Office.Interop.Excel;

using TDSMAN.Classes;
#endregion

namespace TDSMAN.FormTrn
{
    
    public partial class TrnGetChallanIncomeTax : TDSMAN.FormGen.GenForm
    {
        private ChallanExtractorService _extractorService;  // Add this line

        #region System Generated Code
        public TrnGetChallanIncomeTax()
        {
            InitializeComponent();

            // Initialize the service
            _extractorService = new ChallanExtractorService();
            InitializeExtractor();
        }
        #endregion

        #region InitializeExtractor
        private void InitializeExtractor()
        {
            // 1. Status updates (login, extracting, complete)
            _extractorService.OnStatusUpdate += (status) =>
            {
                this.Invoke(new Action(() =>
                {
                    // Update a label or textbox with status
                    lblStatus.Text = status;  
                    // Replace with your control name
                    // OR
                    // txtStatus.Text = status;
                    lblStatus.Refresh();
                    Application.DoEvents(); // ✅ forces UI repaint
                }));
            };

            // 2. Progress updates (range and record count)
            _extractorService.OnProgressUpdate += (progress) =>
            {
                this.Invoke(new Action(() =>
                {
                    lblStatus.Text = progress;  
                    // Replace with your control name
                    // OR add to a multiline textbox
                    // txtProgress.AppendText(progress + Environment.NewLine);
                    lblStatus.Refresh();
                    Application.DoEvents(); // ✅ forces UI repaint
                }));
            };

            // 3. Each challan extracted in real-time
            _extractorService.OnChallanExtracted += (record) =>
            {
                this.Invoke(new Action(() =>
                {
                    // Add to DataGridView
                    // dgvChallans.Rows.Add(record.Cin, record.Amount, record.Section, record.AlternateCIN);

                    // OR add to ListBox
                    // lstChallans.Items.Add($"CIN: {record.Cin}, Amount: {record.Amount}");

                    // OR add to multiline TextBox
                    //txtProgress.AppendText($"Extracted: CIN={record.Cin}, Amount={record.Amount}" + Environment.NewLine);
                }));
            };

            // 4. Final completion
            _extractorService.OnExtractionComplete += (records, finalResult) =>
            {
                this.Invoke(new Action(() =>
                {
                    ////MessageBox.Show($"Extraction Complete!\n\nTotal Challans: {records.Count}",
                    ////    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optionally display final result JSON
                    // txtFinalResult.Text = finalResult.ToString();
                    // ✅ Save the JSON to file for inspection
                    string jsonOutPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ChallanResult.json");
                    File.WriteAllText(jsonOutPath, finalResult.ToString());
                    //MessageBox.Show($"Extraction complete! {records.Count} challans fetched.\nJSON saved at:\n{jsonOutPath}",
                    //                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // ✅ Load grid
                    LoadChallanGrid(records);
                }));
            };

            // 5. Error handling
            _extractorService.OnError += (error) =>
            {
                this.Invoke(new Action(() =>
                {
                    ////MessageBox.Show($"Error: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ////lblMessages.Text = "Error occurred!";
                }));
            };
        }
        #endregion

        #region Objects & Variables decleration

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
        //----
        #endregion

        #region SysBackup_Load
        private void SysBackup_Load(object sender, EventArgs e)
        {
            //lblTitle.Text = "Download Challan from OLTAS(NSDL)";
            lblTitle.Text = "Download Challan - Income Tax";
            //lblMessages.Text = "Challans will be extracted between two given dates (maximum range 4 months) from the Income Tax Portal using the login password of the TAN. As per rule, Challans only for current & previous financial years can be used. The process may take time depending upon the number of challans. Progress will be displayed as extraction takes place.";
            lblMessages.Text = "Challans will be extracted between two given dates (maximum range 4 months) from the Income Tax Portal using the login password of the TAN. As per rule, Challans only for current & previous financial years can be used. The process may take time depending upon the number of challans.";
            //--
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

            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            txtCompany.Text = TDSMAN.Classes.TDSMAN.T_pCompanyName;
            //strSQL = "SELECT TAN_NO, LOGIN_ID, USER_PASSWORD FROM MST_TAN_AADHAAR WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmnService.J_Right(txtCompany.Text.Trim(), 11), 10)) + "'";
            strSQL = "SELECT TAN_NO, USER_PASSWORD FROM MST_TAN_AADHAAR WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmnService.J_Right(txtCompany.Text.Trim(), 11), 10)) + "'";
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
                //txtUserID.Text = "";
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
                    //txtUserID.Text = drdLoadTan["LOGIN_ID"].ToString();
                    //txtPassword.Text = TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdLoadTan["USER_PASSWORD"].ToString());
                    txtPassword.Text = drdLoadTan["USER_PASSWORD"].ToString();
                    //
                    //txtTracesCaptcha.Select();
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
            ////InitializeCaptcha();
            //pctTracesCaptcha.Image = Properties.Resources.captcha_loading;
            //if (!bgWorkerLoadCaptcha.IsBusy)
            //    bgWorkerLoadCaptcha.RunWorkerAsync();
            //
            //
            //mskViewFileDownloadFrom.Select();
            mskViewFileDownloadTo.Text = J_ReturnServerDate();
            DateTime currentDate = Convert.ToDateTime(mskViewFileDownloadTo.Text);
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            mskViewFileDownloadFrom.Text = firstDayOfMonth.ToString("dd/MM/yyyy");
            //
            //txtTANNo.Text= cmnService.J_ReplaceQuote(cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10));
            //--
            //btnChallan.Enabled = false;
            //btnChallan.BackColor = Color.LightGray;
            //--
            btnValidate.Enabled = false;
            btnValidate.BackColor = Color.LightGray;
            //
            this.Cursor = Cursors.Default;
            //
        }
        #endregion

        #region BtnBackup_Click
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDownloadedChallanList.Rows.Count == 0)
                {
                    //MessageBox.Show("No challans available to import.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmnService.J_UserMessage("No challans available to import.");
                    return;
                }

                int importedCount = 0;
                //long basicInfoId = TDSMAN.Classes.TDSMAN.T_pBasicInfoId;

                foreach (DataGridViewRow row in dgvDownloadedChallanList.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                    string status = Convert.ToString(row.Cells["Status"].Value);

                    // Only import checked & new challans
                    if (!isSelected || status == "Challan already exists in the Return")
                        continue;

                    //------------------------------------------------------------
                    // Extract values
                    //------------------------------------------------------------
                    string depositDate = Convert.ToString(row.Cells["Deposit Date"].Value);
                    string challanNo = Convert.ToString(row.Cells["Challan No"].Value);
                    string bsrCode = Convert.ToString(row.Cells["BSR Code"].Value);
                    string sectionNo = Convert.ToString(row.Cells["Section Code"].Value);
                    string amountText = Convert.ToString(row.Cells["Amount"].Value);
                    double challanAmount = cmnService.J_ReturnDoubleValue(amountText);

                    // Convert deposit date to mm/dd/yyyy for SQL
                    string depositDateForSQL = DateTime.ParseExact(depositDate, "dd/MM/yyyy", null)
                                                         .ToString("MM/dd/yyyy");

                    //------------------------------------------------------------
                    // Get next SL_NO for TRN_CHALLAN (works for both Access & SQL Server)
                    //------------------------------------------------------------
                    string sqlMax = "SELECT MAX(SL_NO) AS MaxSl FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId;
                    DataTable dtSl = dmlService.J_ExecSqlReturnDataTable(sqlMax);

                    long nextSl = 1;
                    if (dtSl.Rows.Count > 0 && dtSl.Rows[0]["MaxSl"] != DBNull.Value)
                        nextSl = Convert.ToInt64(dtSl.Rows[0]["MaxSl"]) + 1;

                    //------------------------------------------------------------
                    // Duplicate check for safety
                    //------------------------------------------------------------
                    string dupCheck = "SELECT COUNT(*) AS Cnt FROM TRN_CHALLAN " +
                                      "WHERE BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId +
                                      " AND DEPOSIT_DATE = " + cmnService.J_DateOperator() + depositDateForSQL + cmnService.J_DateOperator() +
                                      " AND BSR_CODE = '" + cmnService.J_ReplaceQuote(bsrCode) + "'" +
                                      " AND CHALLAN_NO = '" + cmnService.J_ReplaceQuote(challanNo) + "'" +
                                      " AND TOT_TAX = " + challanAmount;

                    DataTable dtDup = dmlService.J_ExecSqlReturnDataTable(dupCheck);
                    if (dtDup.Rows.Count > 0 && Convert.ToInt32(dtDup.Rows[0]["Cnt"]) > 0)
                        continue; // skip existing challan for this return

                    //------------------------------------------------------------
                    // INSERT INTO TRN_CHALLAN
                    //------------------------------------------------------------
                    string strSQL = "INSERT INTO TRN_CHALLAN (" +
                                    "BASIC_INFO_ID, SL_NO, DEPOSIT_DATE, BSR_CODE, CHALLAN_NO, " +
                                    "TDS, TOT_TAX, MINOR_HEAD_ID) VALUES (" +
                                    TDSMAN.Classes.TDSMAN.T_pBasicInfoId + "," +
                                    nextSl + "," +
                                    cmnService.J_DateOperator() + depositDateForSQL + cmnService.J_DateOperator() + "," +
                                    "'" + cmnService.J_ReplaceQuote(bsrCode) + "'," +
                                    "'" + cmnService.J_ReplaceQuote(challanNo) + "'," +
                                    challanAmount + "," +
                                    challanAmount + "," +
                                    "1)";   

                    //------------------------------------------------------------
                    // Execute in transaction
                    //------------------------------------------------------------
                    dmlService.J_BeginTransaction();
                    if (!dmlService.J_ExecSql(dmlService.J_pCommand, strSQL))
                    {
                        dmlService.J_Rollback();
                        //MessageBox.Show("Error inserting challan: " + challanNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmnService.J_UserMessage("Error inserting challan: " + challanNo);
                        return;
                    }
                    dmlService.J_Commit();

                    importedCount++;
                }

                //------------------------------------------------------------
                // Final message
                //------------------------------------------------------------
                if (importedCount > 0)
                {
                    //MessageBox.Show(importedCount + " challan(s) successfully imported into the Return.",
                    //                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmnService.J_UserMessage(importedCount + " challan(s) successfully imported into the Return.");

                    // Optional: refresh the grid to mark imported challans as "exists"
                    //ViewPreviouslyFetchedChallans();
                    this.Close();
                    this.Dispose();
                }
                else
                {
                    //MessageBox.Show("No new challans were selected for import.",
                    //                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmnService.J_UserMessage("No new challans were selected for import.");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error during import: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmnService.J_UserMessage("Error during import: " + ex.Message);
            }
        }
        #endregion

        //-- DOWNLOAD CSI FILE
        #region BtnRefresh_Click 
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            mskViewFileDownloadFrom.Text = "";
            mskViewFileDownloadTo.Text = "";
            ////InitializeCaptcha();
            //btnChallan.Enabled = true;
            grpITDetails.Enabled = true;
            grpBackUp.Enabled = true;
            //btnChallan.BackColor = Color.Lavender;
            dgvDownloadedChallanList.Columns.Clear();
            dgvResult.Columns.Clear();
            mskViewFileDownloadFrom.Select();
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (BtnRefresh.Text.ToUpper().Trim() == "CONTINUE")
            {
                //grpCaptcha.Visible = false;
                //lblBottomMessage.Text = "Security introduced by IT Dept. for CSI file download w.e.f. from Dec 2016. Enter correct text & continue.";            
                BtnSave.Enabled = true;
                BtnRefresh.Text = "&Download";
                //txtCaptchaCode.Text = "";
                //
                //txtCaptchaCode.Select();                 
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
            //try
            //{
            //    //--
            //    if (TdsMan.T_CheckInternetConnectivty() == false)
            //    {
            //        picCaptcha.Image = Properties.Resources.captcha_loading_failed;
            //        cmnService.J_UserMessage("Internet Connectivity not found");
            //        return;
            //    }
            //        //----------------------------------------------------
            //        objTracesConnect = new TracesConnect();
            //        Stream imgStream = objTracesConnect.MakeInitialRequest();
            //        Image img = Image.FromStream(imgStream);
            //        this.pctTracesCaptcha.Image = img;
            //        //-------------------------------------------------------
            //        txtTracesCaptcha.Text = "";
            //        txtTANNo.Select();
            //}
            //catch (Exception err)
            //{
            //    picCaptcha.Image = Properties.Resources.captcha_loading_failed;
            //    cmnService.J_UserMessage(err.Message);
            //}
        }

        #endregion



        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            //InitializeCaptcha();
            //txtCaptchaCode.Select();
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
            dgvDownloadedChallanList.Columns.Clear();
            dgvDownloadedChallanList.DataSource = null;
            dgvDownloadedChallanList.DataSource = dsRecords;
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
            dgvDownloadedChallanList.Columns[0].Width =40;
            dgvDownloadedChallanList.Columns[0].ReadOnly = true;
            // dgvStatementList.Columns[1].Width = 0;
            // dgvStatementList.Columns[1].Visible = false;

            //-- Challan Tender Date
            dgvDownloadedChallanList.Columns[1].ReadOnly = true;
            dgvDownloadedChallanList.Columns[1].Width =90;

            //-- Challan Serial No
            dgvDownloadedChallanList.Columns[2].Width =80;
            dgvDownloadedChallanList.Columns[2].ReadOnly = true;

            //-- BSR Code
            dgvDownloadedChallanList.Columns[3].Width = 80;
            dgvDownloadedChallanList.Columns[3].ReadOnly = true;

            //-- Received Date
            dgvDownloadedChallanList.Columns[4].Width = 90;
            dgvDownloadedChallanList.Columns[4].ReadOnly = true;

            //-- Major Head Code
            dgvDownloadedChallanList.Columns[5].Visible = false;

            //-- Minor Head Code
            dgvDownloadedChallanList.Columns[6].Width = 90;
            dgvDownloadedChallanList.Columns[6].ReadOnly = true;

            //-- Nature of Payment
            dgvDownloadedChallanList.Columns[7].Width = 250;
            dgvDownloadedChallanList.Columns[7].ReadOnly = true;

            //dgvStatementList.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvStatementList.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvStatementList.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvStatementList.Columns[7].ReadOnly = false;

            DataGridViewTextBoxColumn btn = new DataGridViewTextBoxColumn();
            dgvDownloadedChallanList.Columns.Add(btn);
            dgvDownloadedChallanList.Columns[8].HeaderText = "Enter Challan Amount";
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
                dgvDownloadedChallanList.Rows[0].Cells[6].Selected = true;
                //
                dgvDownloadedChallanList.CurrentCell = dgvDownloadedChallanList.Rows[0].Cells[6];
                dgvDownloadedChallanList.BeginEdit(true);
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

                //if (string.IsNullOrEmpty(txtCaptchaCode.Text))
                //{
                //    cmnService.J_UserMessage("Enter Captcha Code");
                //    txtCaptchaCode.Select();
                //    return;

                //}
                //btnChallan.Text = "Wait ...";
                ChallanQuery chnl = new ChallanQuery();
                //chnl.TAN = cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10);
                chnl.TAN = cmnService.J_Left(cmnService.J_Right(txtCompany.Text.Trim(), 11), 10);
                chnl.FromDate = mskViewFileDownloadFrom.Text;
                chnl.ToDate = mskViewFileDownloadTo.Text;
                //
                //TracesResponse res = objTracesConnect.SearchChallanDetails(chnl, txtCaptchaCode.Text, out strHtml);
                ////
                //if (res.Respons == enmResponse.Success)
                //{
                //    DataTable dsRecord = (DataTable)res.CustomeTypes;
                //    PopulateDatagridView(dsRecord);
                //    //
                //    btnValidate.Enabled = true;
                //    btnValidate.BackColor = Color.Lavender;
                //    //
                //    btnChallan.Enabled = false;
                //    btnChallan.BackColor = Color.LightGray;
                //}
                //else
                //{
                //    cmnService.J_UserMessage(res.Message);
                //}
                //btnChallan.Text = "View Challan";

                //await Task.Run(async () =>
                //{
                //    await FetchDataAsync(txtTANNo.Text.Trim(), txtPassword.Text.Trim(), mskViewFileDownloadFrom.Text, mskViewFileDownloadTo.Text,15);
                //    fetchCompleted = true; // mark completion
                //});
            }
            catch (Exception ERR)
            {

                //btnChallan.Text = "View Challan";
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
                for (int i = 0; i < dgvDownloadedChallanList.RowCount; i++)
                {
                    ChallanQuery ChallanQuery = new ChallanQuery();
                    //ChallanQuery.TAN = cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10);
                    ChallanQuery.TAN = txtCompany.Text.Substring(txtCompany.Text.Length - 11, 10);


                    if (Convert.ToString(dgvDownloadedChallanList.Rows[i].Cells[8].Value) != "")
                    {
                        string strVal = Convert.ToString(dgvDownloadedChallanList.Rows[i].Cells[7].Value);
                        //    
                        ChallanQuery.BSRCode = Convert.ToString(dgvDownloadedChallanList.Rows[i].Cells[3].Value);
                        ChallanQuery.ChallanDate = Convert.ToString(dgvDownloadedChallanList.Rows[i].Cells[1].Value);
                        ChallanQuery.ChallanNo = Convert.ToString(dgvDownloadedChallanList.Rows[i].Cells[2].Value);
                        ChallanQuery.ChallanAmount = Convert.ToString(dgvDownloadedChallanList.Rows[i].Cells[8].Value).Replace(".00", "");
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
            //if(btnChallan.Enabled==false)
            //{
            //    //InitializeCaptcha();
            //    btnChallan.Enabled = true;
            //    btnChallan.BackColor = Color.Lavender;
            //    dgvStatementList.Columns.Clear();
            //    dgvResult.Columns.Clear();
            //}
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
                string strExcelCellSerialNo = "[Running Serial No]", strExcelCellTDS = "[TDS]", strExcelCellBSRCode = "[BSR Code/24G Receipt No]", strExcelCellTotalTaxDeposited = "[Total Tax Deposited]", strExcelCellTransferVoucherChallanSerialNo = "[Transfer Voucher/Challan Serial No (409)]"; ;
                string strExcelCellDateOnTaxDeposited = "[Date on Tax Deposited (dd/mm/yyyy)]", strExcelCellMinorHead = "[Minor head]";
                //
                if (dgvDownloadedChallanList.RowCount == 0)
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
                //string ChallanDate = "", ChallanNo = "", BSRCode = "", strSQL = "";
                int MinorCode = 0;
                double ChallanAmount = 0; int SrlNo = 0, ImportedChallanNo = 0;
                string strDepositDate = "", strChallanNo = "", strBSRCode = "", strSectionNo = "", strAmountText = "", strDepositDateForInsert= "", strMinorCode="200";
                double dblChallanAmount = 0;
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
                //for (int i = 0; i < dgvDownloadedChallanList.RowCount; i++)
                foreach (DataGridViewRow row in dgvDownloadedChallanList.Rows)
                {
                    strDepositDate = Convert.ToString(row.Cells["Deposit Date"].Value);
                    strChallanNo = Convert.ToString(row.Cells["Challan No"].Value);
                    strBSRCode = Convert.ToString(row.Cells["BSR Code"].Value);
                    strSectionNo = Convert.ToString(row.Cells["Section Code"].Value);
                    strAmountText = Convert.ToString(row.Cells["Amount"].Value);
                    dblChallanAmount = cmnService.J_ReturnDoubleValue(strAmountText);
                    // Convert deposit date to mm/dd/yyyy for SQL
                    strDepositDateForInsert = DateTime.ParseExact(strDepositDate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //
                    lngGetMaxChallan = lngGetMaxChallan + 1;
                    SrlNo = SrlNo + 1;
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
                                        "            '" + strDepositDateForInsert + "'," +
                                        "            '" + cmnService.J_ReplaceQuote(strChallanNo) + "'," +
                                        "            '" + cmnService.J_ReplaceQuote(strBSRCode) + "'," +
                                        "            '" + cmnService.J_ReturnDoubleValue(dblChallanAmount) + "'," +
                                        "            '" + cmnService.J_ReturnDoubleValue(dblChallanAmount) + "'," +
                                        "            '" + strMinorCode + "')";
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
                strSQL = "SELECT RUNNING_SERIAL_NO    AS " + strExcelCellSerialNo + "," +
                    "            TDS                  AS " + strExcelCellTDS + "," +
                    "            TOTAL_TAX_DEPOSITED  AS " + strExcelCellTotalTaxDeposited + "," +
                    "            BSR_CODE             AS " + strBSRCode + "," +
                    "            DATE_TAX_DEPOSITED   AS " + strExcelCellDateOnTaxDeposited + " ," +
                    "            CHALLAN_SERIAL_NO    AS " + strExcelCellTransferVoucherChallanSerialNo + " ," +
                    "            MINOR_HEAD           AS " + strExcelCellMinorHead + " " + 
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
                //MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
                cmnService.J_UserMessage("Exception Occured while releasing object " + ex.ToString());
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
        private async void btnGoTraces_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "-";
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //--
                dgvDownloadedChallanList.Columns.Clear();
                dgvDownloadedChallanList.DataSource = null;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    btnCloseIT.Select();
                    return;
                }
                //--
                if (!ValidateFields()) return;
                //--
                //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER 

                strSQL = "SELECT COUNT(*) FROM MST_TAN_AADHAAR WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";
                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    //insering new record in the tan login master
                    strSQL = "INSERT INTO MST_TAN_AADHAAR(TAN_NO, USER_PASSWORD) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

                    dmlService.J_ExecSql(strSQL);
                }
                else
                {
                    //updating the existing record in the master

                    strSQL = "UPDATE MST_TAN_AADHAAR " +
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                             "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";

                    dmlService.J_ExecSql(strSQL);
                }
                //#################
                this.Cursor = Cursors.WaitCursor;
                //--
                //TracesLogin objLogin = new TracesLogin();
                //objLogin.UserID = txtUserID.Text;
                //objLogin.Password = txtPassword.Text;
                //objLogin.TAN = txtTANNo.Text;
                //objLogin.CaptchaCode = txtTracesCaptcha.Text;
                ////---------
                //ArrayList objList = new ArrayList();
                //objList.Add(enmRequestType.Login);
                //objList.Add(objLogin);
                ////-------------------------------------------
                ////pgTimer.Start();
                ////-------------------------------------------
                //if (!bgwTracesChallanVerification.IsBusy)
                //    bgwTracesChallanVerification.RunWorkerAsync(objList);
                //
                // Get values from your textboxes/controls
                string tan = txtTANNo.Text.Trim(); 
                string password = txtPassword.Text.Trim();

                // Convert date format
                DateTime fromDate = DateTime.ParseExact(mskViewFileDownloadFrom.Text.Replace("/","-"), "dd-MM-yyyy", null);
                DateTime toDate = DateTime.ParseExact(mskViewFileDownloadTo.Text.Replace("/", "-"), "dd-MM-yyyy", null);

                await _extractorService.ExtractChallansAsync(
                    tan,
                    password,
                    fromDate.ToString("yyyy-MM-dd"),
                    toDate.ToString("yyyy-MM-dd"),
                    25
                );
                //
                this.Cursor = Cursors.Default;
            }
            catch (Exception err)
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
            //--
            string selectedFY = TDSMAN.Classes.TDSMAN.T_pFinancialYear;// cmbFialYear.Text.Trim(); // example: "2025-26"

            // Extract start year from "2025-26"
            int fyStartYear = int.Parse(selectedFY.Substring(0, 4));
            DateTime fyStart = new DateTime(fyStartYear, 4, 1);            // 01-Apr-2025
            DateTime fyEnd = fyStart.AddYears(1).AddDays(-1);              // 31-Mar-2026

            DateTime prevFYStart = fyStart.AddYears(-1);
            DateTime prevFYEnd = fyEnd.AddYears(-1);

            // ADD THIS
            DateTime nextFYEnd = fyEnd.AddYears(1);

            // Get Quarter
            string quarter = TDSMAN.Classes.TDSMAN.T_pQuarter.ToString(); // "Q1", "Q2", "Q3", "Q4"

            // Now validate user dates
            DateTime fromDate = DateTime.ParseExact(mskViewFileDownloadFrom.Text, "dd/MM/yyyy", null);
            DateTime toDate = DateTime.ParseExact(mskViewFileDownloadTo.Text, "dd/MM/yyyy", null);

            // 1️ Range limit: max 4 months
            if ((toDate - fromDate).TotalDays > 124)
            {
                cmnService.J_UserMessage("The date range cannot exceed 4 months.");
                return false;
            }

            // 2️ Must be within current or previous FY
            //bool isFromValid = (fromDate >= prevFYStart && fromDate <= fyEnd);
            //bool isToValid = (toDate >= prevFYStart && toDate <= fyEnd);

            bool isValid = false;

            if (quarter == "Q4")
            {
                // 👇 Allow till next FY end
                isValid = (fromDate >= prevFYStart && fromDate <= nextFYEnd) &&
                          (toDate >= prevFYStart && toDate <= nextFYEnd);
            }
            else
            {
                isValid = (fromDate >= prevFYStart && fromDate <= fyEnd) &&
                          (toDate >= prevFYStart && toDate <= fyEnd);
            }

            if (!isValid)
            {
                cmnService.J_UserMessage("Challan dates must fall within the selected or previous financial year.");
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
            //if (string.IsNullOrEmpty(txtTracesCaptcha.Text))
            //{
            //    cmnService.J_UserMessage("Please enter Captcha Code");
            //    txtCaptchaCode.Focus();
            //    return false;
            //}

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
                    objResponse = objTracesConnect.makeLoginToTRACES((TracesLogin)objList[1]);
                    //---------------------------------------------------
                    objRetval.Add(enmRequestType.Login);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    //---------------------------------------------------
                    break;
                // LIST OF CHALLAN ENQUIRY CIN - Period Payment 
                case enmRequestType.CINPP:
                    TracesResponse response = objTracesConnect.CIN_Period_Payment((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.CINPP);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;
                // LIST OF CHALLAN ENQUIRY CIN - CIN/BIN Particulars
                case enmRequestType.CINParticulars:
                    response = objTracesConnect.CIN_CIN_BINParticulars((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.CINParticulars);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;

                case enmRequestType.BINPP:
                    response = objTracesConnect.BIN_Period_Payment((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.BINPP);
                    objRetval.Add(response);
                    e.Result = objRetval;

                    break;

                case enmRequestType.BINParticulars:
                    response = objTracesConnect.BIN_Particulars((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.BINPP);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;


                // VIEW CONSUMPTION DETAILS
                case enmRequestType.ConsumptionDetails:

                    response = objTracesConnect.RequestForConsumptionDetailsCIN((TracesData)objList[1]);
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
                            grpITDetails.Enabled = false;
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
                            grpITDetails.Enabled = true;
                            grpBackUp.Enabled = true;
                            cmnService.J_UserMessage(objResponse.Message);
                            //InitializeCaptcha();
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
                        dgvDownloadedChallanList.Rows[intRowIndex].Cells[7].Value = 100;
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

                            dgvDownloadedChallanList.Rows[intRowIndex].Cells[intColumnIndex - 3].Selected = true;
                            dgvDownloadedChallanList.CurrentCell = dgvDownloadedChallanList.Rows[intRowIndex].Cells[intColumnIndex - 3];
                            dgvDownloadedChallanList.BeginEdit(true);
                            return;
                        }
                        //-----------------------------------------------------
                        dTable = (DataTable)objResponse.CustomeTypes;
                        //------------------------------------------------------
                        dgvDownloadedChallanList.Columns.Clear();
                        dgvDownloadedChallanList.DataSource = null;
                        if (dTable.Rows.Count <= 0)
                        {
                            cmnService.J_UserMessage("No Data Available. Please check after 3 working days from the date of filing of the statement at TIN-FC");
                            return;
                        }
                        dgvDownloadedChallanList.DataSource = dTable;
                        dgvDownloadedChallanList.Columns[0].Width = 110;
                        dgvDownloadedChallanList.Columns[1].Width = 110;
                        dgvDownloadedChallanList.Columns[2].Width = 60;
                        dgvDownloadedChallanList.Columns[3].Width = 90;
                        dgvDownloadedChallanList.Columns[6].Width = 150;
                        dgvDownloadedChallanList.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvDownloadedChallanList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvDownloadedChallanList.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvDownloadedChallanList.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvDownloadedChallanList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvDownloadedChallanList.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dgvDownloadedChallanList.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvDownloadedChallanList.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvDownloadedChallanList.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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
                            dgvDownloadedChallanList.Rows[intRowIndex].Cells[intColumnIndex - 1].Selected = true;
                            dgvDownloadedChallanList.CurrentCell = dgvDownloadedChallanList.Rows[intRowIndex].Cells[intColumnIndex - 1];
                            dgvDownloadedChallanList.BeginEdit(true);

                            return;
                        }
                        else
                            dgvDownloadedChallanList.Rows[intRowIndex].Cells[8].Value = 100;

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

                        //txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtTANNo.Text = "";
                        //txtCaptchaCode.Text = "";
                        //grpDownloadList.Visible = false;
                        //grpLoginDetails.Visible = true;
                        // grpProgress.Visible = true;
                        BtnSave.Enabled = true;
                        BtnSave.BackColor = Color.Lavender;
                        //InitializeCaptcha();
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
            dgvDownloadedChallanList.Columns.Clear();
            dgvDownloadedChallanList.DataSource = null;

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
            //if (chkValidationType.Checked == true)
            //{
            //    objList.Add(enmRequestType.BINPP);
            //    objList.Add(objData);
            //}
            //else
            //{
                objList.Add(enmRequestType.CINPP);
                objList.Add(objData);
            //}
            //
            if (!bgwTracesChallanVerification.IsBusy)
                bgwTracesChallanVerification.RunWorkerAsync(objList);
            pgLoadGrid.Stop();
        }
        #endregion


        #region dgvStatementList_CellClick
        private void dgvStatementList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ////GETTING ROW & COLUMN INDEX OF SELECT ROW
            //intRowIndex = e.RowIndex;
            //intColumnIndex = e.ColumnIndex;
            ////-------------------------------------------------------------------------------
            //if (e.ColumnIndex == dgvStatementList.Columns["lnkDetails"].Index && e.RowIndex >= 0)
            //{
            //    //if (Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "view details")
            //    if (Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "match challan amount")
            //    {
            //        //SEARCH ON CIN
            //        //if (rbnCINSearch.Checked)
            //        //{
            //            //dgvStatementList.Columns.Clear();
            //            //dgvStatementList.DataSource = null;
            //            //--------------------------------

            //            //  VALIDATION AMOUNT - update on 08/09/2016
            //            string strAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value) == "" ? "0.00" : Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value);

            //            if (!cmnService.J_IsNumeric(strAmount))
            //            {
            //                cmnService.J_UserMessage("Please Enter Challan Amount");
            //                dgvStatementList.Rows[e.RowIndex].Cells[6].Selected = true;

            //                dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[6];
            //                dgvStatementList.BeginEdit(true);
            //                return;
            //            }
            //            else
            //            {
            //                if (Convert.ToDouble(strAmount) <= 0)
            //                {
            //                    cmnService.J_UserMessage("Please Enter Challan Amount");
            //                    dgvStatementList.Rows[e.RowIndex].Cells[6].Selected = true;

            //                    dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[6];
            //                    dgvStatementList.BeginEdit(true);

            //                    return;
            //                }
            //            }


            //            dgvStatementList.Rows[e.RowIndex].Cells[6].Value = string.Format("{0:0.00}", Convert.ToDouble(strAmount));


            //            ArrayList objList = new ArrayList();
            //            TracesData objData = new TracesData();
            //            //---------------------------------
            //            objData.BSRCode = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[0].Value) + Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[1].Value);
            //            objData.FromChallanDepositDate = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[2].Value);
            //            objData.ChallanSerialNo = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value);
            //            objData.ChallanAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value);
            //            objData.PRN_NO = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[5].Value);
            //            //---------------------------------------------


            //            pgTimerGrid.Start();
            //            objList.Add(enmRequestType.ConsumptionDetails);
            //            objList.Add(objData);

            //            if (!bgwTracesChallanVerification.IsBusy)
            //                bgwTracesChallanVerification.RunWorkerAsync(objList);

            //        //}
            //        //-------------------------------------------------------------------------------
            //        //FOR BIN SEARCH
            //        //if (rbnBINSearch.Checked)
            //        //{
            //        //    dgvConsumption.Columns.Clear();
            //        //    dgvConsumption.DataSource = null;
            //        //    //-------------------------------

            //        //    //  VALIDATION AMOUNT - update on 08/09/2016
            //        //    string strAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value) == "" ? "0.00" : Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value);

            //        //    if (!cmnService.J_IsNumeric(strAmount))
            //        //    {
            //        //        cmnService.J_UserMessage("Please Enter Amount");
            //        //        dgvStatementList.Rows[e.RowIndex].Cells[3].Selected = true;

            //        //        dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[3];
            //        //        dgvStatementList.BeginEdit(true);
            //        //        return;
            //        //    }
            //        //    else
            //        //    {
            //        //        if (Convert.ToDouble(strAmount) <= 0)
            //        //        {
            //        //            cmnService.J_UserMessage("Please Enter Amount");
            //        //            dgvStatementList.Rows[e.RowIndex].Cells[3].Selected = true;

            //        //            dgvStatementList.CurrentCell = dgvStatementList.Rows[e.RowIndex].Cells[3];
            //        //            dgvStatementList.BeginEdit(true);

            //        //            return;
            //        //        }
            //        //    }


            //        //    dgvStatementList.Rows[e.RowIndex].Cells[3].Value = string.Format("{0:0.00}", Convert.ToDouble(strAmount));


            //        //    pgTimerGrid.Start();
            //        //    ArrayList objList = new ArrayList();
            //        //    List<string> strParam = new List<string>();

            //        //    //RECORD_ID
            //        //    strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[5].Value));
            //        //    //RECEIPT NO
            //        //    strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[4].Value));
            //        //    //DDOS NO
            //        //    strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[1].Value));
            //        //    //CHALLAN AMOUNT
            //        //    strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value));
            //        //    //DATE OF DEPOSIT
            //        //    strParam.Add(Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[0].Value));


            //        //    objList.Add(enmRequestType.BINPP_Details);
            //        //    objList.Add(strParam);

            //        //    if (!bgWorker.IsBusy)
            //        //        bgWorker.RunWorkerAsync(objList);

            //        //}

            //    }
            //}
        }
        #endregion

        #region btnTracesCaptchaRefresh_Click
        private void btnTracesCaptchaRefresh_Click(object sender, EventArgs e)
        {

            //InitializeCaptcha();
        }
        #endregion

        #region  mnuExportChallanToCSV_Click
        private void mnuExportChallanToCSV_Click(object sender, EventArgs e)
        {
            try
            {
                string strExcelCellSerialNo = "[Running Serial No]", strExcelCellTDS = "[TDS]", strExcelCellBSRCode = "[BSR Code/24G Receipt No]", strExcelCellTotalTaxDeposited = "[Total Tax Deposited]", strExcelCellTransferVoucherChallanSerialNo = "[Transfer Voucher/Challan Serial No (409)]"; ;
                string strExcelCellDateOnTaxDeposited = "[Date on Tax Deposited (dd/mm/yyyy)]", strExcelCellMinorHead = "[Minor head]";
                //
                if (dgvDownloadedChallanList.RowCount == 0)
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
                //
                if (File.Exists(strExcelPath) == true)
                {
                    File.Delete(strExcelPath);
                }
                //--
                this.Cursor = Cursors.WaitCursor;
                long lngGetMaxChallan = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId, J_QueryType.DirectQuery)));
                //string ChallanDate = "", ChallanNo = "", BSRCode = "", strSQL = "";
                int MinorCode = 0;
                double ChallanAmount = 0; int SrlNo = 0, ImportedChallanNo = 0;
                string strDepositDate = "", strChallanNo = "", strBSRCode = "", strSectionNo = "", strAmountText = "", strDepositDateForInsert = "", strMinorCode = "200";
                double dblChallanAmount = 0;
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
                foreach (DataGridViewRow row in dgvDownloadedChallanList.Rows)
                {
                    strDepositDate = Convert.ToString(row.Cells["Deposit Date"].Value);
                    strChallanNo = Convert.ToString(row.Cells["Challan No"].Value);
                    strBSRCode = Convert.ToString(row.Cells["BSR Code"].Value);
                    strSectionNo = Convert.ToString(row.Cells["Section Code"].Value);
                    strAmountText = Convert.ToString(row.Cells["Amount"].Value);
                    dblChallanAmount = cmnService.J_ReturnDoubleValue(strAmountText);
                    // Convert deposit date to mm/dd/yyyy for SQL
                    strDepositDateForInsert = DateTime.ParseExact(strDepositDate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //
                    lngGetMaxChallan = lngGetMaxChallan + 1;
                    SrlNo = SrlNo + 1;
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
                                        "            '" + strDepositDateForInsert + "'," +
                                        "            '" + cmnService.J_ReplaceQuote(strChallanNo) + "'," +
                                        "            '" + cmnService.J_ReplaceQuote(strBSRCode) + "'," +
                                        "            '" + cmnService.J_ReturnDoubleValue(dblChallanAmount) + "'," +
                                        "            '" + cmnService.J_ReturnDoubleValue(dblChallanAmount) + "'," +
                                        "            '" + strMinorCode + "')";
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
                }
                //--
                strSQL = "SELECT RUNNING_SERIAL_NO    AS " + strExcelCellSerialNo + "," +
                    "            TDS                  AS " + strExcelCellTDS + "," +
                    "            TOTAL_TAX_DEPOSITED  AS " + strExcelCellTotalTaxDeposited + "," +
                    "            BSR_CODE             AS " + strExcelCellBSRCode + "," +
                    "            DATE_TAX_DEPOSITED   AS " + strExcelCellDateOnTaxDeposited + " ," +
                    "            CHALLAN_SERIAL_NO    AS " + strExcelCellTransferVoucherChallanSerialNo + " ," +
                    "            MINOR_HEAD           AS " + strExcelCellMinorHead + " " +
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
        #endregion

        #region PopulateDatagridView
        void PopulateDatagridView(DataTable dsRecords, enmRequestType enmRecType)
        {
            // DATA BIND TO GRIDVIEW CONTROL
            dgvDownloadedChallanList.Columns.Clear();
            dgvDownloadedChallanList.DataSource = null;
            dgvDownloadedChallanList.DataSource = dsRecords;
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
                dgvDownloadedChallanList.Columns[0].Width = 80;
                //dgvStatementList.Columns[0].Visible = false;
                dgvDownloadedChallanList.Columns[0].ReadOnly = true;
                dgvDownloadedChallanList.Columns[1].Width = 80;
                //dgvStatementList.Columns[1].Visible = false;
                dgvDownloadedChallanList.Columns[1].ReadOnly = true;
                dgvDownloadedChallanList.Columns[2].Width = 100;
                dgvDownloadedChallanList.Columns[2].ReadOnly = true;

                dgvDownloadedChallanList.Columns[3].Width = 80;
                dgvDownloadedChallanList.Columns[3].ReadOnly = true;
                dgvDownloadedChallanList.Columns[4].ReadOnly = true;
                //dgvStatementList.Columns[5].ReadOnly = true;
                dgvDownloadedChallanList.Columns[5].Visible = false;
                dgvDownloadedChallanList.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDownloadedChallanList.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDownloadedChallanList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDownloadedChallanList.Columns[6].ReadOnly = false;



            }

            if (enmRecType == enmRequestType.BINPP)
            {
                dgvDownloadedChallanList.Columns[0].Width = 150;
                dgvDownloadedChallanList.Columns[1].Width = 150;
                //dgvStatementList.Columns[3].Visible = false;
                dgvDownloadedChallanList.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDownloadedChallanList.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDownloadedChallanList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDownloadedChallanList.Columns[4].Visible = false;
                dgvDownloadedChallanList.Columns[5].Visible = false;

                dgvDownloadedChallanList.Columns[1].ReadOnly = true;
                dgvDownloadedChallanList.Columns[2].ReadOnly = true;
                dgvDownloadedChallanList.Columns[3].ReadOnly = false;
                dgvDownloadedChallanList.Columns[4].ReadOnly = true;
                dgvDownloadedChallanList.Columns[5].ReadOnly = true;


            }


            DataGridViewLinkColumn btn = new DataGridViewLinkColumn();
            dgvDownloadedChallanList.Columns.Add(btn);
            btn.HeaderText = "";
            //btn.Text = "View Details";
            btn.Text = "Match Challan Amount";
            btn.Name = "lnkDetails";
            btn.Width = 150;
            //------------------------------------------------------------
            btn.UseColumnTextForLinkValue = true;
            //------------------------------------------------------------
            DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            dgvDownloadedChallanList.Columns.Add(prg);
            prg.Name = "";
            prg.ProgressBarColor = Color.LightGreen;
            //------------------------------------------------------------
            // SET FOCUS 
            if (dsRecords.Rows.Count > 0)
            {
                if (enmRecType == enmRequestType.CINPP || enmRecType == enmRequestType.CINParticulars)
                {
                    dgvDownloadedChallanList.Rows[0].Cells[6].Selected = true;

                    dgvDownloadedChallanList.CurrentCell = dgvDownloadedChallanList.Rows[0].Cells[6];
                    dgvDownloadedChallanList.BeginEdit(true);
                }
                else
                {
                    dgvDownloadedChallanList.Rows[0].Cells[3].Selected = true;

                    dgvDownloadedChallanList.CurrentCell = dgvDownloadedChallanList.Rows[0].Cells[3];
                    dgvDownloadedChallanList.BeginEdit(true);
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

        #region  btnCloseTraces_Click
        private void btnCloseTraces_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion



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

        private void BtnSave_Click(object sender, EventArgs e)
        {

        }

        private void PnlControls_Paint(object sender, PaintEventArgs e)
        {

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

        #region FetchDataAsync
        private async Task FetchDataAsync(string TAN, string Password, string FromDate, string ToDate, int MaxRec)
        {
            try
            {
                //this.Cursor = Cursors.WaitCursor;
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(30); // extend timeout
                    //var json = "{\"uid\":\"CALP08143C\",\"password\":\"Pdsinfo@6\"}";
                    //var json = "{\"uid\":\"" + TAN + "\",\"password\":\"" + Password + "\"}";
                    //var json = "{\"uid\":\"MRTS22129C \",\"password\":\"Saurabh#1981\"}";
                    //var json = "{\"uid\":\"DELA45227A\",\"password\":\"Tarun@1236\"}";
                    //var json = "{\"tan\": \"CALP08143C\",  \"password\": \"Pdsinfo@6\",  \"fromdate\": \"2025-07-04\",  \"todate\" : \"2025-07-05\",  \"maxrec\": \"15\"}";
                    var json = "{\"tan\": \"" + TAN + "\",  \"password\": \"" + Password + "\",  \"fromdate\": \"" + FromDate + "\",  \"todate\" : \"" + ToDate + "\",  \"maxrec\": \"" + MaxRec + "\"}";
                    //var json = "{\"tan\": \"CALP08143C\",  \"password\": \"Pdsinfo@6\",  \"fromdate\": \"2025-07-04\",  \"todate\" : \"2025-09-27\",  \"maxrec\": \"15\"}";
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    DateTime startTime = DateTime.Now;
                    HttpResponseMessage response = await client.PostAsync("http://www.tdsman.com/challan-extract", content);
                    //response.EnsureSuccessStatusCode();
                    if (!response.IsSuccessStatusCode)
                    {
                        ////string errorMsg = await response.Content.ReadAsStringAsync();
                        ////MessageBox.Show($"Login failed. Server returned {response.StatusCode}.\nDetails: {errorMsg}");
                        //prgFetchITBar.Value = prgFetchITBar.Maximum;
                        //// Stop the timer
                        //tmrFetchITPortal.Stop();
                        //// Set progress bar to full
                        cmnService.J_UserMessage("Login failed.");
                        //--
                        //grpITLogin.Visible = false;
                        ////
                        //grpCompanyName.Enabled = true;
                        //grpBasicInformation.Enabled = true;
                        //grpAddress.Enabled = true;
                        //grpResponsiblePersonDetails.Enabled = true;
                        //grpResponsiblePerson.Enabled = true;
                        //grpInactiveTAN.Enabled = true;
                        //grpGovtDeductors.Enabled = true;
                        //grpCITDetails.Enabled = true;
                        //--
                        return; // stop further processing
                    }

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    //File.WriteAllText("company-data.json", jsonResponse);

                    //MessageBox.Show("Data fetched and saved successfully!", "Success",
                    //    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var obj = JObject.Parse(jsonResponse);

                    var records = obj["results"]?[0]?["records"];

                    // Check success
                    if (obj["success"] != null && obj["success"].Value<bool>())
                    {
                        if (records != null)
                        {
                            var sb = new StringBuilder();
                            foreach (var record in records)
                            {
                                string section = record["Section"]?.ToString();
                                string amount = record["Amount"]?.ToString();
                                amount = amount?.Replace("₹", "").Replace(",", "").Trim();
                                string altCIN = record["AlternateCIN"]?.ToString();

                                // Extract parts from AlternateCIN
                                string bsrCode = altCIN?.Substring(0, 7);        // first 7 digits
                                string challanDate = altCIN?.Substring(7, 8);    // ddmmyyyy
                                string challanNo = altCIN?.Substring(15);        // rest

                                // Convert date properly
                                string formattedDate = DateTime.ParseExact(challanDate, "ddMMyyyy", null)
                                                                 .ToString("dd-MM-yyyy");

                                //MessageBox.Show(
                                //    $"Section: {section}\n" +
                                //    $"Amount: {amount}\n" +
                                //    $"BSR Code: {bsrCode}\n" +
                                //    $"Date: {formattedDate}\n" +
                                //    $"Challan No: {challanNo}",
                                //    "Challan Record",
                                //    MessageBoxButtons.OK,
                                //    MessageBoxIcon.Information
                                //);
                                sb.AppendLine($"Section: {section}");
                                sb.AppendLine($"Amount: {amount}");
                                sb.AppendLine($"BSR Code: {bsrCode}");
                                sb.AppendLine($"Date: {formattedDate}");
                                sb.AppendLine($"Challan No: {challanNo}");
                                sb.AppendLine(new string('-', 40));
                            }
                            // Write to text file

                            DateTime endTime = DateTime.Now;
                            //
                            sb.AppendLine("Process Started: " + startTime.ToString("dd-MM-yyyy HH:mm:ss"));
                            sb.AppendLine("Process Ended  : " + endTime.ToString("dd-MM-yyyy HH:mm:ss"));
                            //sb.AppendLine("Duration       : " + (endTime - startTime).TotalSeconds + " seconds");
                            sb.AppendLine("Duration       : " + (endTime - startTime).TotalSeconds / 60 + " mins");
                            //
                            File.WriteAllText("ChallanRecords.txt", sb.ToString(), Encoding.UTF8);
                            Process.Start(new ProcessStartInfo("ChallanRecords.txt") { UseShellExecute = true });
                        }
                    }
                }
                //--
                //
                //grpITLogin.Visible = false;
                ////
                //grpCompanyName.Enabled = true;
                //grpBasicInformation.Enabled = true;
                //grpAddress.Enabled = true;
                //grpResponsiblePersonDetails.Enabled = true;
                //grpResponsiblePerson.Enabled = true;
                //grpInactiveTAN.Enabled = true;
                //grpGovtDeductors.Enabled = true;
                //grpCITDetails.Enabled = true;
                //--
                //this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        #endregion

        #region LoadChallanGrid
        ////private void LoadChallanGrid(List<ChallanRecord> challans)
        ////{
        ////    try
        ////    {
        ////        dgvDownloadedChallanList.Columns.Clear();
        ////        dgvDownloadedChallanList.DataSource = null;

        ////        // Create DataTable matching your target grid
        ////        DataTable dt = new DataTable();
        ////        dt.Columns.Add("Select", typeof(bool));
        ////        dt.Columns.Add("Deposit Date", typeof(string));
        ////        dt.Columns.Add("Challan No", typeof(string));
        ////        dt.Columns.Add("BSR Code", typeof(string));
        ////        dt.Columns.Add("Section Code", typeof(string));
        ////        dt.Columns.Add("Amount", typeof(string));
        ////        dt.Columns.Add("Status", typeof(string));
        ////        //-- INSERT <>
        ////        strSQL = @"INSERT INTO MST_IT_CHALLAN_HEADER (
        ////                            CREATE_DATETIME,
        ////                            COMPANY_ID,
        ////                            IMPORT_FROM_DATE,
        ////                            IMPORT_TO_DATE)
        ////                            VALUES (" +
        ////                            cmnService.J_DateOperator() + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + cmnService.J_DateOperator() + "," +
        ////                            TDSMAN.Classes.TDSMAN.T_pCompanyId + "," +
        ////                            cmnService.J_DateOperator() + mskViewFileDownloadFrom.Text + cmnService.J_DateOperator() + "," +
        ////                            cmnService.J_DateOperator() + mskViewFileDownloadTo.Text + cmnService.J_DateOperator() + ")";
        ////        // Transaction block
        ////        dmlService.J_BeginTransaction();
        ////        if (!dmlService.J_ExecSql(dmlService.J_pCommand, strSQL))
        ////        {
        ////            dmlService.J_Rollback();
        ////            return;
        ////        }
        ////        long lngIT_CHALLAN_HEADER_ID = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "MST_IT_CHALLAN_HEADER", "IT_CHALLAN_HEADER_ID");
        ////        //dmlService.J_Commit();
        ////        //--
        ////        foreach (var c in challans)
        ////        {
        ////            if (string.IsNullOrEmpty(c.AlternateCIN) || c.AlternateCIN.Length < 15)
        ////                continue;

        ////            string bsrCode = c.AlternateCIN.Substring(0, 7);
        ////            string challanDate = c.AlternateCIN.Substring(7, 8);
        ////            string challanNo = c.AlternateCIN.Substring(15);

        ////            string depositDate = "";
        ////            try
        ////            {
        ////                depositDate = DateTime.ParseExact(challanDate, "ddMMyyyy", null)
        ////                                        .ToString("dd/MM/yyyy");
        ////            }
        ////            catch
        ////            {
        ////                depositDate = challanDate;
        ////            }

        ////            // Example status – customize as needed (e.g. from database lookup)
        ////            string status = "";// "Challan already entered";

        ////            string cleanAmount = c.Amount?.Replace("₹", "").Replace(",", "").Trim();
        ////            dt.Rows.Add(false, depositDate, challanNo, bsrCode, c.Section, cleanAmount, status);
        ////            //-- INSERT DATA TO <MST_IT_CHALLAN_DETAIL>
        ////            strSQL = @"INSERT INTO MST_IT_CHALLAN_DETAIL (IT_CHALLAN_HEADER_ID,
        ////                                   CIN, 
        ////                                   ALTERNATE_CIN, 
        ////                                   SECTION_NO, 
        ////                                   TOT_TAX, 
        ////                                   TAN_NO, 
        ////                                   CHALLAN_DATE_YYYYMMDD, 
        ////                                   CHALLAN_DATE_DDMMYYYY, 
        ////                                   BSR_CODE, 
        ////                                   CHALLAN_NO)
        ////                    VALUES (" + lngIT_CHALLAN_HEADER_ID + ",'" + cmnService.J_ReplaceQuote(c.Cin ?? "") + "'," +
        ////                    "'" + cmnService.J_ReplaceQuote(c.AlternateCIN ?? "") + "'," +
        ////                    "'" + cmnService.J_ReplaceQuote(c.Section ?? "") + "'," +
        ////                    cleanAmount + "," +
        ////                    "'" + cmnService.J_ReplaceQuote(txtTANNo.Text ?? "") + "'," +
        ////                    "'" + DateTime.ParseExact(challanDate, "ddMMyyyy", null).ToString("yyyyMMdd") + "'," +
        ////                    "'" + DateTime.ParseExact(challanDate, "ddMMyyyy", null).ToString("ddMMyyyy") + "'," +
        ////                    "'" + cmnService.J_ReplaceQuote(bsrCode) + "'," +
        ////                    "'" + cmnService.J_ReplaceQuote(challanNo) + "')";

        ////            // Transaction block
        ////            //dmlService.J_BeginTransaction();
        ////            if (!dmlService.J_ExecSql(dmlService.J_pCommand, strSQL))
        ////            {
        ////                dmlService.J_Rollback();
        ////                return;
        ////            }
        ////            //--
        ////        }
        ////        dmlService.J_Commit();

        ////        dgvDownloadedChallanList.DataSource = dt;

        ////        // Format grid
        ////        dgvDownloadedChallanList.Columns["Select"].HeaderText = "";
        ////        dgvDownloadedChallanList.Columns["Select"].Width = 60;
        ////        dgvDownloadedChallanList.Columns["Deposit Date"].Width = 100;
        ////        dgvDownloadedChallanList.Columns["Challan No"].Width = 100;
        ////        dgvDownloadedChallanList.Columns["BSR Code"].Width = 100;
        ////        dgvDownloadedChallanList.Columns["Section Code"].Width = 100;
        ////        dgvDownloadedChallanList.Columns["Amount"].Width = 100;
        ////        dgvDownloadedChallanList.Columns["Status"].Width = 200;

        ////        dgvDownloadedChallanList.Columns["Select"].ReadOnly = false;
        ////        for (int i = 1; i < dgvDownloadedChallanList.Columns.Count; i++)
        ////            dgvDownloadedChallanList.Columns[i].ReadOnly = true;

        ////        // ✅ Evenly distribute all columns to fill the available width
        ////        dgvDownloadedChallanList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        ////        dgvDownloadedChallanList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        ////        dgvDownloadedChallanList.AllowUserToResizeColumns = false;
        ////        dgvDownloadedChallanList.AllowUserToResizeRows = false;

        ////        // Optional – make headers and cell text nicely aligned
        ////        dgvDownloadedChallanList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        ////        dgvDownloadedChallanList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        ////        // Optional – remove vertical gray area if present
        ////        dgvDownloadedChallanList.RowHeadersVisible = false;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        MessageBox.Show($"Error loading grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        ////    }
        ////}

        #endregion

        #region LoadChallanGrid
        private void LoadChallanGrid(List<ChallanRecord> challans)
        {
            try
            {
                dgvDownloadedChallanList.Columns.Clear();
                dgvDownloadedChallanList.DataSource = null;

                //------------------------------------------------------------
                // CREATE DATATABLE FOR GRID
                //------------------------------------------------------------
                DataTable dt = new DataTable();
                dt.Columns.Add("Select", typeof(bool));
                dt.Columns.Add("Deposit Date", typeof(string));
                dt.Columns.Add("Challan No", typeof(string));
                dt.Columns.Add("BSR Code", typeof(string));
                dt.Columns.Add("Section Code", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                dt.Columns.Add("Status", typeof(string));

                //------------------------------------------------------------
                // INSERT HEADER RECORD
                //------------------------------------------------------------
                string strSQL = @"INSERT INTO MST_IT_CHALLAN_HEADER (
                                CREATE_DATETIME,
                                COMPANY_ID,
                                IMPORT_FROM_DATE,
                                IMPORT_TO_DATE)
                          VALUES (" +
                                  cmnService.J_DateOperator() + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + cmnService.J_DateOperator() + "," +
                                  TDSMAN.Classes.TDSMAN.T_pCompanyId + "," +
                                  cmnService.J_DateOperator() + mskViewFileDownloadFrom.Text + cmnService.J_DateOperator() + "," +
                                  cmnService.J_DateOperator() + mskViewFileDownloadTo.Text + cmnService.J_DateOperator() + ")";

                dmlService.J_BeginTransaction();
                if (!dmlService.J_ExecSql(dmlService.J_pCommand, strSQL))
                {
                    dmlService.J_Rollback();
                    return;
                }

                // FETCH NEW HEADER ID
                strSQL = "SELECT @@IDENTITY AS NewID";
                DataTable dtID = dmlService.J_ExecSqlReturnDataTable(strSQL);
                if (dtID.Rows.Count == 0)
                {
                    dmlService.J_Rollback();
                    //MessageBox.Show("Failed to retrieve Header ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmnService.J_UserMessage("Failed to retrieve Header ID");
                    return;
                }

                long headerId = Convert.ToInt64(dtID.Rows[0]["NewID"]);
                dmlService.J_Commit();

                //------------------------------------------------------------
                // 2️⃣ LOOP THROUGH CHALLANS
                //------------------------------------------------------------
                foreach (var c in challans)
                {
                    if (string.IsNullOrEmpty(c.AlternateCIN) || c.AlternateCIN.Length < 15)
                        continue;

                    string bsrCode = c.AlternateCIN.Substring(0, 7);
                    string challanDate = c.AlternateCIN.Substring(7, 8);
                    string challanNo = Convert.ToString(Convert.ToInt64(c.AlternateCIN.Substring(15)));

                    string depositDate = "";
                    try
                    {
                        depositDate = DateTime.ParseExact(challanDate, "ddMMyyyy", null)
                                                .ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        depositDate = challanDate;
                    }

                    string cleanAmount = c.Amount?.Replace("₹", "").Replace(",", "").Trim();
                    if (string.IsNullOrEmpty(cleanAmount)) cleanAmount = "0";
                    double challanAmount = cmnService.J_ReturnDoubleValue(cleanAmount);

                    //------------------------------------------------------------
                    // 3️⃣ CHECK IF CHALLAN ALREADY EXISTS IN TRN_CHALLAN
                    //------------------------------------------------------------
                    string status = "New challan";
                    bool disableCheckbox = false;

                    try
                    {
                        string challanDateForQuery = DateTime.ParseExact(challanDate, "ddMMyyyy", null)
                                                             .ToString("MM/dd/yyyy");

                        string checkSQL =
                            "SELECT COUNT(*) AS CNT FROM TRN_CHALLAN " +
                            "WHERE DEPOSIT_DATE = " + cmnService.J_DateOperator() + challanDateForQuery + cmnService.J_DateOperator() +
                            " AND CHALLAN_NO = '" + cmnService.J_ReplaceQuote(challanNo) + "'" +
                            " AND BSR_CODE = '" + cmnService.J_ReplaceQuote(bsrCode) + "'" +
                            " AND TOT_TAX = " + challanAmount + " " +
                            " AND BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId;

                        DataTable dtCheck = dmlService.J_ExecSqlReturnDataTable(checkSQL);

                        if (dtCheck.Rows.Count > 0 && Convert.ToInt32(dtCheck.Rows[0]["CNT"]) > 0)
                        {
                            status = "Challan already exists in the Return";
                            disableCheckbox = true;
                        }
                    }
                    catch
                    {
                        status = "Status check failed";
                    }

                    //------------------------------------------------------------
                    // 4️⃣ INSERT INTO MST_IT_CHALLAN_DETAIL
                    //------------------------------------------------------------
                    strSQL = @"INSERT INTO MST_IT_CHALLAN_DETAIL (
                                IT_CHALLAN_HEADER_ID,
                                CIN, 
                                ALTERNATE_CIN, 
                                SECTION_NO, 
                                TOT_TAX, 
                                TAN_NO, 
                                CHALLAN_DATE_YYYYMMDD, 
                                CHALLAN_DATE_DDMMYYYY, 
                                BSR_CODE, 
                                CHALLAN_NO)
                       VALUES (" +
                                        headerId + "," +
                                        "'" + cmnService.J_ReplaceQuote(c.Cin ?? "") + "'," +
                                        "'" + cmnService.J_ReplaceQuote(c.AlternateCIN ?? "") + "'," +
                                        "'" + cmnService.J_ReplaceQuote(c.Section ?? "") + "'," +
                                        challanAmount + "," +
                                        "'" + cmnService.J_ReplaceQuote(txtTANNo.Text ?? "") + "'," +
                                        "'" + DateTime.ParseExact(challanDate, "ddMMyyyy", null).ToString("yyyyMMdd") + "'," +
                                        "'" + DateTime.ParseExact(challanDate, "ddMMyyyy", null).ToString("ddMMyyyy") + "'," +
                                        "'" + cmnService.J_ReplaceQuote(bsrCode) + "'," +
                                        "'" + cmnService.J_ReplaceQuote(challanNo) + "')";

                    dmlService.J_BeginTransaction();
                    if (!dmlService.J_ExecSql(dmlService.J_pCommand, strSQL))
                    {
                        dmlService.J_Rollback();
                        return;
                    }
                    dmlService.J_Commit();

                    //------------------------------------------------------------
                    // 5️⃣ ADD ROW TO DATATABLE (FOR GRID DISPLAY)
                    //------------------------------------------------------------
                    dt.Rows.Add(!disableCheckbox, depositDate, challanNo, bsrCode, c.Section, cleanAmount, status);
                }

                //------------------------------------------------------------
                // 6️⃣ BIND DATATABLE TO GRID
                //------------------------------------------------------------
                dgvDownloadedChallanList.DataSource = dt;

                // Formatting
                dgvDownloadedChallanList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDownloadedChallanList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvDownloadedChallanList.AllowUserToResizeColumns = false;
                dgvDownloadedChallanList.AllowUserToResizeRows = false;
                dgvDownloadedChallanList.RowHeadersVisible = false;
                dgvDownloadedChallanList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDownloadedChallanList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Column widths
                dgvDownloadedChallanList.Columns["Select"].Width = 60;
                dgvDownloadedChallanList.Columns["Deposit Date"].Width = 100;
                dgvDownloadedChallanList.Columns["Challan No"].Width = 100;
                dgvDownloadedChallanList.Columns["BSR Code"].Width = 100;
                dgvDownloadedChallanList.Columns["Section Code"].Width = 100;
                dgvDownloadedChallanList.Columns["Amount"].Width = 100;
                dgvDownloadedChallanList.Columns["Status"].Width = 250;

                // Allow checkbox only on new challans
                foreach (DataGridViewRow row in dgvDownloadedChallanList.Rows)
                {
                    string statusText = Convert.ToString(row.Cells["Status"].Value);

                    if (statusText == "Challan already exists in the Return")
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Select"];
                        chk.Value = false;
                        chk.ReadOnly = true;
                        row.DefaultCellStyle.ForeColor = Color.Gray; // optional visual cue
                                                                     // row.DefaultCellStyle.BackColor = Color.LightGray; // optional highlight
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error loading grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmnService.J_UserMessage($"Error loading grid: {ex.Message}");
            }
        }
        #endregion


        #region LnkViewPreviouslyFetchedChallan_LinkClicked
        private void LnkViewPreviouslyFetchedChallan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewPreviouslyFetchedChallans();
        }
        #endregion

        #region J_ReturnServerDate 
        public string J_ReturnServerDate()
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strSQL = "SELECT FORMAT(DATE(),'dd/MM/yyyy')";
            else
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            //--
            return cmnService.J_NullToText(dmlService.J_ExecSqlReturnScalar(strSQL));
        }
        #endregion

        #region ChkSelectAll_CheckedChanged
        private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkSelectAll.Checked;

            foreach (DataGridViewRow row in dgvDownloadedChallanList.Rows)
            {
                string status = Convert.ToString(row.Cells["Status"].Value);
                if (status == "Challan already exists in the Return")
                    continue;

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Select"];
                chk.Value = isChecked;
            }

            dgvDownloadedChallanList.EndEdit();
        }
        #endregion

        #region ViewPreviouslyFetchedChallans
        private void ViewPreviouslyFetchedChallans_old()
        {
            try
            {
                dgvDownloadedChallanList.Columns.Clear();
                dgvDownloadedChallanList.DataSource = null;

                // Create DataTable for grid display
                DataTable dt = new DataTable();
                dt.Columns.Add("Select", typeof(bool));
                dt.Columns.Add("Deposit Date", typeof(string));
                dt.Columns.Add("Challan No", typeof(string));
                dt.Columns.Add("BSR Code", typeof(string));
                dt.Columns.Add("Section Code", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                dt.Columns.Add("Status", typeof(string));

                //------------------------------------------------------------
                // 1️⃣ Fetch from MST_IT_CHALLAN_DETAIL
                //------------------------------------------------------------
                string strSQL = @"SELECT 
                            CHALLAN_DATE_DDMMYYYY AS DepositDate,
                            CHALLAN_NO,
                            BSR_CODE,
                            SECTION_NO,
                            TOT_TAX,
                            TAN_NO
                          FROM MST_IT_CHALLAN_DETAIL
                          WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + @"' 
                          ORDER BY CHALLAN_DATE_DDMMYYYY DESC";
        
        DataTable dtFetched = dmlService.J_ExecSqlReturnDataTable(strSQL);

                if (dtFetched.Rows.Count == 0)
                {
                    //MessageBox.Show("No previously fetched challans found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmnService.J_UserMessage("No previously fetched challans found.");
                    return;
                }

                //------------------------------------------------------------
                // 2️⃣ Compare with TRN_CHALLAN and populate grid
                //------------------------------------------------------------
                foreach (DataRow row in dtFetched.Rows)
                {
                    string depositDate = Convert.ToString(row["DepositDate"]);
                    string challanNo = Convert.ToString(row["CHALLAN_NO"]);
                    string bsrCode = Convert.ToString(row["BSR_CODE"]);
                    string sectionNo = Convert.ToString(row["SECTION_NO"]);
                    double challanAmount = cmnService.J_ReturnDoubleValue(Convert.ToString(row["TOT_TAX"]));

                    string status = "New challan";
                    bool disableCheckbox = false;

                    try
                    {
                        string challanDateForQuery = DateTime.ParseExact(depositDate, "ddMMyyyy", null)
                                                             .ToString("MM/dd/yyyy");

                        string checkSQL =
                            "SELECT COUNT(*) AS CNT FROM TRN_CHALLAN " +
                            "WHERE DEPOSIT_DATE = " + cmnService.J_DateOperator() + challanDateForQuery + cmnService.J_DateOperator() +
                            " AND CHALLAN_NO = '" + cmnService.J_ReplaceQuote(challanNo) + "'" +
                            " AND BSR_CODE = '" + cmnService.J_ReplaceQuote(bsrCode) + "'" +
                            " AND TOT_TAX = " + challanAmount + " " +
                            " AND BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId; 

                        DataTable dtCheck = dmlService.J_ExecSqlReturnDataTable(checkSQL);

                        if (dtCheck.Rows.Count > 0 && Convert.ToInt32(dtCheck.Rows[0]["CNT"]) > 0)
                        {
                            status = "Challan already exists in the Return";
                            disableCheckbox = true;
                        }
                    }
                    catch
                    {
                        status = "Status check failed";
                    }

                    dt.Rows.Add(!disableCheckbox, DateTime.ParseExact(depositDate, "ddMMyyyy", null).ToString("dd/MM/yyyy"), challanNo, bsrCode, sectionNo, challanAmount.ToString("N2"), status);
                }

                //------------------------------------------------------------
                // 3️⃣ Bind grid
                //------------------------------------------------------------
                dgvDownloadedChallanList.DataSource = dt;

                // Grid formatting
                dgvDownloadedChallanList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDownloadedChallanList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvDownloadedChallanList.AllowUserToResizeColumns = false;
                dgvDownloadedChallanList.AllowUserToResizeRows = false;
                dgvDownloadedChallanList.RowHeadersVisible = false;
                dgvDownloadedChallanList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDownloadedChallanList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvDownloadedChallanList.Columns["Select"].Width = 60;
                dgvDownloadedChallanList.Columns["Deposit Date"].Width = 100;
                dgvDownloadedChallanList.Columns["Challan No"].Width = 100;
                dgvDownloadedChallanList.Columns["BSR Code"].Width = 100;
                dgvDownloadedChallanList.Columns["Section Code"].Width = 100;
                dgvDownloadedChallanList.Columns["Amount"].Width = 100;
                dgvDownloadedChallanList.Columns["Status"].Width = 250;

                // Disable already existing challans
                foreach (DataGridViewRow row in dgvDownloadedChallanList.Rows)
                {
                    string statusText = Convert.ToString(row.Cells["Status"].Value);

                    if (statusText == "Challan already exists in the Return")
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Select"];
                        chk.Value = false;
                        chk.ReadOnly = true;
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                        // row.DefaultCellStyle.BackColor = Color.LightGray; // optional
                    }
                }

                MessageBox.Show(dt.Rows.Count + " challans loaded from previous fetch.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching challans: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region ViewPreviouslyFetchedChallans
        private void ViewPreviouslyFetchedChallans()
        {
            try
            {
                dgvDownloadedChallanList.Columns.Clear();
                dgvDownloadedChallanList.DataSource = null;

                // Create DataTable for grid display
                DataTable dt = new DataTable();
                dt.Columns.Add("Select", typeof(bool));
                dt.Columns.Add("Deposit Date", typeof(string));
                dt.Columns.Add("Challan No", typeof(string));
                dt.Columns.Add("BSR Code", typeof(string));
                dt.Columns.Add("Section Code", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                dt.Columns.Add("Status", typeof(string));

                //------------------------------------------------------------
                // 1️⃣ Fetch from MST_IT_CHALLAN_DETAIL
                //------------------------------------------------------------
                string strSQL = @"SELECT DISTINCT
                            CHALLAN_DATE_DDMMYYYY AS DepositDate,
                            CHALLAN_NO,
                            BSR_CODE,
                            SECTION_NO,
                            TOT_TAX,
                            TAN_NO
                          FROM MST_IT_CHALLAN_DETAIL
                          WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + @"' 
                          ORDER BY CHALLAN_DATE_DDMMYYYY DESC";

                DataTable dtFetched = dmlService.J_ExecSqlReturnDataTable(strSQL);

                if (dtFetched.Rows.Count == 0)
                {
                    //MessageBox.Show("No previously fetched challans found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmnService.J_UserMessage("No previously fetched challans found.");
                    return;
                }

                //------------------------------------------------------------
                // 2️⃣ Compare with TRN_CHALLAN and populate grid
                //------------------------------------------------------------
                foreach (DataRow row in dtFetched.Rows)
                {
                    string depositDate = Convert.ToString(row["DepositDate"]);
                    string challanNo = Convert.ToString(row["CHALLAN_NO"]);
                    string bsrCode = Convert.ToString(row["BSR_CODE"]);
                    string sectionNo = Convert.ToString(row["SECTION_NO"]);
                    double challanAmount = cmnService.J_ReturnDoubleValue(Convert.ToString(row["TOT_TAX"]));

                    string status = "New challan";
                    bool disableCheckbox = false;

                    try
                    {
                        string challanDateForQuery = DateTime.ParseExact(depositDate, "ddMMyyyy", null)
                                                             .ToString("MM/dd/yyyy");

                        string checkSQL =
                            "SELECT COUNT(*) AS CNT FROM TRN_CHALLAN " +
                            "WHERE DEPOSIT_DATE = " + cmnService.J_DateOperator() + challanDateForQuery + cmnService.J_DateOperator() +
                            " AND CHALLAN_NO = '" + cmnService.J_ReplaceQuote(challanNo) + "'" +
                            " AND BSR_CODE = '" + cmnService.J_ReplaceQuote(bsrCode) + "'" +
                            " AND TOT_TAX = " + challanAmount +
                            " AND BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId;

                        DataTable dtCheck = dmlService.J_ExecSqlReturnDataTable(checkSQL);

                         if (dtCheck.Rows.Count > 0 && Convert.ToInt32(dtCheck.Rows[0]["CNT"]) > 0)
                        {
                            status = "Challan already exists in the Return";
                            disableCheckbox = true;
                        }
                    }
                    catch
                    {
                        status = "Status check failed";
                    }

                    dt.Rows.Add(!disableCheckbox,
                                DateTime.ParseExact(depositDate, "ddMMyyyy", null).ToString("dd/MM/yyyy"),
                                challanNo, bsrCode, sectionNo, challanAmount.ToString("N2"), status);
                }

                //------------------------------------------------------------
                // 3️⃣ Sort: Existing challans (gray) on top, then new challans
                //------------------------------------------------------------
                if (!dt.Columns.Contains("SortOrder"))
                    dt.Columns.Add("SortOrder", typeof(int));
                if (!dt.Columns.Contains("DepositDateSort"))
                    dt.Columns.Add("DepositDateSort", typeof(DateTime));

                foreach (DataRow r in dt.Rows)
                {
                    string statusText = Convert.ToString(r["Status"]);
                    r["SortOrder"] = (statusText == "Challan already exists in the Return") ? 0 : 1;

                    string ddmmyyyy = Convert.ToString(r["Deposit Date"]);
                    try
                    {
                        r["DepositDateSort"] = DateTime.ParseExact(ddmmyyyy, "dd/MM/yyyy", null);
                    }
                    catch
                    {
                        r["DepositDateSort"] = DBNull.Value;
                    }
                }

                DataView dv = dt.DefaultView;
                dv.Sort = "SortOrder ASC, DepositDateSort DESC";
                DataTable sortedTable = dv.ToTable();

                //------------------------------------------------------------
                // 4️⃣ Bind sorted data to grid
                //------------------------------------------------------------
                dgvDownloadedChallanList.DataSource = sortedTable;

                // Grid formatting
                dgvDownloadedChallanList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDownloadedChallanList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvDownloadedChallanList.AllowUserToResizeColumns = false;
                dgvDownloadedChallanList.AllowUserToResizeRows = false;
                dgvDownloadedChallanList.RowHeadersVisible = false;
                dgvDownloadedChallanList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDownloadedChallanList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvDownloadedChallanList.Columns["Select"].Width = 60;
                dgvDownloadedChallanList.Columns["Deposit Date"].Width = 100;
                dgvDownloadedChallanList.Columns["Challan No"].Width = 100;
                dgvDownloadedChallanList.Columns["BSR Code"].Width = 100;
                dgvDownloadedChallanList.Columns["Section Code"].Width = 100;
                dgvDownloadedChallanList.Columns["Amount"].Width = 100;
                dgvDownloadedChallanList.Columns["Status"].Width = 250;

                // Hide helper columns if surfaced
                if (dgvDownloadedChallanList.Columns.Contains("SortOrder"))
                    dgvDownloadedChallanList.Columns["SortOrder"].Visible = false;
                if (dgvDownloadedChallanList.Columns.Contains("DepositDateSort"))
                    dgvDownloadedChallanList.Columns["DepositDateSort"].Visible = false;

                //------------------------------------------------------------
                // 5️⃣ Disable already existing challans (gray out)
                //------------------------------------------------------------
                foreach (DataGridViewRow row in dgvDownloadedChallanList.Rows)
                {
                    string statusText = Convert.ToString(row.Cells["Status"].Value);

                    if (statusText == "Challan already exists in the Return")
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Select"];
                        chk.Value = false;
                        chk.ReadOnly = true;
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                        // row.DefaultCellStyle.BackColor = Color.LightGray; // optional
                    }
                }

                //MessageBox.Show(sortedTable.Rows.Count + " challans loaded from previous fetch.",
                //                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmnService.J_UserMessage(sortedTable.Rows.Count + " challans loaded.");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error fetching challans: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmnService.J_UserMessage("Error fetching challans: " + ex.Message);
            }
        }
        #endregion


    }
}

