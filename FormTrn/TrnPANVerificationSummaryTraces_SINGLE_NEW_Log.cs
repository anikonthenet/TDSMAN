
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
using System.Runtime.InteropServices;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormRpt;
using System.Text.RegularExpressions;
using System.Linq;


#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnPANVerificationSummaryTraces_SINGLE_NEW_Log : Form
    {
        #region System Generated Code
        public TrnPANVerificationSummaryTraces_SINGLE_NEW_Log()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables declaration

        TracesConnect objTracesConnect = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();

        bool blnVerificationComplete = false;
        bool blnExit = true;

        enum enmState
        {
            Captcha,
            PanNameExtract
        }
        //--
        enum enmRequestType
        {
            Login,
            UploadCSVFile,
            ViewTokenDetails,
            DownloadCSVFile,
            PanValidation,
            LogOff
        }
        //
        string strNOTAVAILABLE = "NOT AVAILABLE";
        string filePathCSV = "", filePathCSVTokenNo = "", UploadedfilePath = "", strSaveDownloadedCSVFilePath = "";
        private string CurrentCaptchaId = "";
        public class PANInfo
        {
            public string Name { get; set; }
            public string Status { get; set; }
            //public string AadhaarLinkDate { get; set; }
        }

        private const uint NERR_Success = 0;

        [DllImport("netapi32.dll", SetLastError = false)]
        private static extern uint NetApiBufferFree(IntPtr Buffer);

        [DllImport("netapi32.dll", CharSet = CharSet.Unicode,
        SetLastError = false)]
        private static extern uint NetRemoteTOD(string
        UncServerName, ref IntPtr BufferPtr);

        [StructLayout(LayoutKind.Sequential)]
        private struct TIME_OF_DAY_INFO
        {
            public uint tod_elapsedt;
            public uint tod_msecs;
            public uint tod_hours;
            public uint tod_mins;
            public uint tod_secs;
            public uint tod_hunds;
            public uint tod_timezone;
            public uint tod_tinterval;
            public uint tod_day;
            public uint tod_month;
            public uint tod_year;
            public uint tod_weekday;
        }
        #endregion

        #region User Defined Events

        //-- Added By ABhishek Dey On 22/08/2018 --
        #region TrnPANVerificationSummaryTraces_Activated
        private void TrnPANVerificationSummaryTraces_Activated(object sender, EventArgs e)
        {
            //-- Added By Abhishek Dey On 22/08/2018 --
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //-----------------------------------------
            //
        }
        #endregion
        //-----------------------------------------

        #region TrnPANVerificationSummary_Load
        private void TrnPANVerificationSummary_Load(object sender, EventArgs e)
        {
            try
            {
                ////txtPAN.Text = "BROPK6848J";
                //lblTitle.Text = "PAN Verification";
                //
                if (TDSMAN.Classes.TDSMAN.T_GetPANforVerification != "")
                {
                    txtPAN.Text = TDSMAN.Classes.TDSMAN.T_GetPANforVerification;
                    //txtCaptchaCode.Select();
                }
                ////--            
                //this.Cursor = Cursors.WaitCursor;
                ////
                ////ShowCaptcha(); 
                ////
                //this.Cursor = Cursors.Default;
                //                
                //--
                Clearcontrols();
            }
            catch (Exception err)
            {
                grpDetails.Visible = false;
                //
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #region btnReset_Click
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtPAN.Text = "";
            //ShowCaptcha();
            Clearcontrols();
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            IDataReader drdShowTracesDetails = null;
            try
            {
                Clearcontrols();
                //
                if (IsValidField())
                {
                    this.Cursor = Cursors.WaitCursor;
                    //--
                    #region COMMENT

                    /*string strname = "";
                    bool bnlStatus = objAccount.ExtractPANName(txtPAN.Text, txtCaptchaCode.Text, out strname);

                    if (bnlStatus==false)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage(strname);
                        txtPAN.Select();
                        ShowCaptcha();
                        return;
                    }
                    else
                    {
                      //  PANVerifierDetails objPan = (PANVerifierDetails)response.CustomeTypes;
                        
                        grpDetails.Visible = true;
                        
                        lblDetails.Text = txtPAN.Text;
                        lblSurname.Text = strname;
                        //lblMiddleName.Text = objPan.MiddleName;
                        //lblFirstName.Text = objPan.FirstName;
                        //lblAreaCode.Text = objPan.AreaCode;
                        //lblAOType.Text = objPan.AOType;
                        //lblRangeCode.Text = objPan.RangeCode;
                        //lblAONumber.Text = objPan.AONumber;
                        //lblJurisdiction.Text = objPan.Jurisdiction;
                        //lblBuildingName.Text = objPan.BuildingName;

                        ShowCaptcha();

                        txtCaptchaCode.Text = "";
                    } */


                    //ArrayList objData = new ArrayList();
                    //objData.Add(enmState.PanNameExtract);
                    //objData.Add(txtPAN.Text);
                    //objData.Add(txtCaptchaCode.Text);

                    //bgWorker.RunWorkerAsync(objData);


                    //this.Cursor = Cursors.Default; 

                    #endregion
                    //--
                    //--
                    if (TdsMan.T_CheckInternetConnectivty() == false)
                    {
                        cmnService.J_UserMessage("Internet Connectivity not found", MessageBoxIcon.Exclamation);
                        //BtnExit.Select();
                        return;
                    }
                    //--
                    //--
                    if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //--
                    //
                    DateTime cutoffDate = DateTime.Now.AddMonths(-6);
                    string cutoff = cutoffDate.ToString("yyyy-MM-dd");

                    string dateCondition = "";

                    if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    {
                        dateCondition = "CHECKED_DATE_TIME >= #" + cutoff + "#";
                    }
                    else // SQL Server
                    {
                        dateCondition = "CHECKED_DATE_TIME >= '" + cutoff + "'";
                    }

                    string whereClause = "PAN = '" + txtPAN.Text + "' " +
                                         "AND VERIFIED_NAME <> '' " +
                                         "AND CHECKED_DATE_TIME IS NOT NULL " +
                                         "AND " + dateCondition;
                    //
                    //if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", "PAN = '" + txtPAN.Text + "' AND VERIFIED_NAME <> '' AND CHECKED_DATE_TIME <> NULL"))
                    if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", whereClause))
                    {
                        grpDetails.Visible = true;
                        //-- GET THE STATUS
                        int iStatus = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT VERIFIED_STATUS FROM MST_VERIFIED_PAN WHERE PAN = '" + txtPAN.Text + "'")));
                        string strStatus = "";
                        if(iStatus == 0)
                            strStatus = "Valid & Operative";
                        else if (iStatus == 1)
                            strStatus = "Invalid";
                        else if (iStatus == 2)
                            strStatus = "Valid & Inoperative";
                        //
                        txtPANName.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT VERIFIED_NAME FROM MST_VERIFIED_PAN WHERE PAN = '" + txtPAN.Text + "'"));
                        lblStatus.Text = strStatus + " - verified on " + Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT CHECKED_DATE_TIME FROM MST_VERIFIED_PAN WHERE PAN = '" + txtPAN.Text + "'")) ;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //--
                    //-- Create the CSV as per PAN
                    string pan = txtPAN.Text;
                    string fileName = "SinglePAN";

                    filePathCSV = ExportSinglePANToCSV(pan, fileName);
                    //
                    if (blnVerificationComplete == true)
                    {
                        blnVerificationComplete = false;
                        bgwPANVerification.RunWorkerAsync();
                    }
                    else
                    {
                        //
                        this.Cursor = Cursors.WaitCursor;
                        //
                        //--
                        //InitializeCaptcha();
                        picCaptcha.Image = Properties.Resources.captcha_loading;
                        if (!bgWorkerLoadCaptcha.IsBusy)
                            bgWorkerLoadCaptcha.RunWorkerAsync();
                        //
                        //
                        grpLoginDetails.Visible = true;
                        //
                        if (TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES != "")
                        {
                            //-- IF TAN DETAILS FOUND FROM DATABASE
                            strSQL = "SELECT TAN_ACCOUNT_ID," +
                             "       TAN_NO," +
                             "       LOGIN_ID," +
                             "       USER_PASSWORD," +
                             "       COMPANY_NAME " +
                             "FROM   MST_TAN_ACCOUNT " +
                             "WHERE  TAN_NO ='" + cmnService.J_ReplaceQuote(TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES) + "'";

                            drdShowTracesDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                            //--
                            if (drdShowTracesDetails == null)
                            {
                                txtTAN.Select();
                                blnExit = true;                                
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            //
                            while (drdShowTracesDetails.Read())
                            {
                                blnExit = false;
                                txtTAN.Text = Convert.ToString(drdShowTracesDetails["TAN_NO"]);
                                txtUserID.Text = Convert.ToString(drdShowTracesDetails["LOGIN_ID"]);
                                txtPassword.Text = Convert.ToString(drdShowTracesDetails["USER_PASSWORD"]);
                                blnExit = true;
                                //lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12)
                                //                                        + drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15)
                                //                                        + drdShowDeducteeHelp["USER_PASSWORD"].ToString().PadRight(10)
                                //                                        + " " + drdShowDeducteeHelp["COMPANY_NAME"].ToString(),
                                //                                        Convert.ToInt32(drdShowDeducteeHelp["TAN_ACCOUNT_ID"])));                            
                            }
                            //--
                            //-----------------------------------------------------------
                            drdShowTracesDetails.Close();
                            drdShowTracesDetails.Dispose();
                            //--
                            txtCaptchaCode.Select();
                        }
                        else
                            txtTAN.Select();
                        //--
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message);
            }


        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btnPrintPan_Click
        private void btnPrintPan_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                RptDialog rptDialog = new RptDialog();
                rptDialog.PANVerification(lblDetails.Text,
                                          txtPANName.Text,
                                          lblMiddleName.Text,
                                          lblFirstName.Text,
                                          lblAreaCode.Text,
                                          lblAOType.Text,
                                          lblRangeCode.Text,
                                          lblAONumber.Text,
                                          lblJurisdiction.Text,
                                          lblBuildingName.Text);
                this.Cursor = Cursors.Default;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region IsValidField
        private bool IsValidField()
        {
            if (string.IsNullOrEmpty(txtPAN.Text.Trim()))
            {
                cmnService.J_UserMessage("Please enter the PAN");
                txtPAN.Focus();
                return false;
            }
            else
            {
                string strMessage = "";
                if (!FormValidation.IsValidPAN(txtPAN.Text, out strMessage))
                {
                    cmnService.J_UserMessage("Please enter a valid PAN");
                    txtPAN.Focus();
                    return false;
                }
            }
            //----------------------------------------------------------
            //if (string.IsNullOrEmpty(txtCaptchaCode.Text))
            //{
            //    cmnService.J_UserMessage("Please enter the Captcha Code");
            //    txtPAN.Focus();
            //    return false;

            //}
            return true;
        }
        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            InitializeCaptcha();
        }
        #endregion

        #region txtPAN_KeyPress
        private void txtPAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion 

        #region txtCaptchaCode_KeyPress
        private void txtCaptchaCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region bgwPANVerification_DoWork
        //private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        //
        //        ArrayList objRetval = new ArrayList();
        //        TracesResponse objResponse = new TracesResponse();
        //        Label.CheckForIllegalCrossThreadCalls = false;
        //        string strSTATUS = ""; string strVerifiedNAME = "";
        //        if (blnVerificationComplete == false)
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            //pctGreenDownArrow.Visible = true;
        //            //--
        //            //TracesResponse response = objTracesConnect.RequestForPANValidation(Convert.ToString(txtPAN.Text.Trim()),
        //                                                                                       //out strVerifiedNAME, out strSTATUS);
        //            TracesResponse response = objTracesConnect.RequestForPANValidation_New(Convert.ToString(txtPAN.Text.Trim()),
        //                                                                                       out strVerifiedNAME, out strSTATUS);
        //            //--
        //            grpLoginDetails.Visible = false;
        //            //
        //            this.Cursor = Cursors.Default;
        //            //
        //            if (response.Respons == enmResponse.Success)
        //            {
        //                if (strVerifiedNAME.ToUpper() == strNOTAVAILABLE)
        //                {
        //                    grpLoginDetails.Visible = false;
        //                    txtPAN.Select();
        //                    cmnService.J_UserMessage("Invalid PAN");
        //                    return;
        //                }
        //                else
        //                {
        //                    grpDetails.Visible = true;
        //                    //
        //                    lblDetails.Text = txtPAN.Text;
        //                    txtPANName.Text = strVerifiedNAME.ToUpper().Trim().Replace("AMP;", "").Replace("  ", " ");
        //                    //-- 2023/10/12
        //                    if(strSTATUS.ToUpper() == T_PANVerificationStatus.VALID_OPERATIVE || strSTATUS.ToUpper() == T_PANVerificationStatus.ACTIVE)
        //                    {
        //                        lblStatus.Visible = true;
        //                        lblStatus.ForeColor = Color.Green;
        //                        lblStatus.Text = T_PANVerificationStatus.VALID_OPERATIVE.ToString();
        //                        //--
        //                        if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
        //                        {
        //                            dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
        //                                "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
        //                                "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "')");
        //                        }
        //                        else
        //                        {
        //                            dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "' " +
        //                                " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
        //                        }
        //                    }
        //                    else if (strSTATUS.ToUpper() == T_PANVerificationStatus.VALID_INOPERATIVE)
        //                    {
        //                        lblStatus.Visible = true;
        //                        lblStatus.ForeColor = Color.Yellow;
        //                        lblStatus.BackColor = Color.Black;
        //                        lblStatus.Text = T_PANVerificationStatus.VALID_INOPERATIVE.ToString();
        //                        //--
        //                        if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
        //                        {
        //                            dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
        //                                "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
        //                                "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "')");
        //                        }
        //                        else
        //                        {
        //                            dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "' " +
        //                                " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
        //                        }
        //                    }
        //                    ////--
        //                    //if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
        //                    //{
        //                    //    dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
        //                    //        "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
        //                    //        "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "')");
        //                    //}
        //                    //else
        //                    //{
        //                    //    dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "' " +
        //                    //        " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
        //                    //}
        //                    //--
        //                }
        //            }
        //            else
        //            {
        //                txtPAN.Select();
        //                cmnService.J_UserMessage("Please try again !!");
        //                return;
        //            }
        //            blnVerificationComplete = true;
        //            //--
        //            //bgwPANVerification.CancelAsync();
        //            //bgwPANVerification.Dispose();
        //            //
        //            GC.Collect();
        //            this.Cursor = Cursors.Default;
        //            //--
        //        }
        //        //--
        //        if (bgwPANVerification.CancellationPending)
        //        {
        //            e.Cancel = true;
        //            blnVerificationComplete = false;
        //            //pctGreenDownArrow.Visible = false;
        //            return;
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        blnVerificationComplete = false;
        //        this.Cursor = Cursors.Default;
        //        cmnService.J_UserMessage(err.Message);
        //    }
        //}
        private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Label.CheckForIllegalCrossThreadCalls = false;

                if (blnVerificationComplete == false)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string pan = Convert.ToString(txtPAN.Text.Trim()).ToUpper();

                    // LOAD CSV
                    string csvPath = strSaveDownloadedCSVFilePath;
                    //var panStatusDict = ReadPANStatus(csvPath);
                    var panStatusDict = ReadPANNameStatus(csvPath);

                    string strVerifiedNAME = "";
                    string strSTATUS = "";

                    grpLoginDetails.Visible = false;

                    // CHECK PAN IN CSV
                    if (panStatusDict.ContainsKey(pan))
                    {
                        var panInfo = panStatusDict[pan];

                        strVerifiedNAME = panInfo.Name.ToUpper().Trim()
                                                .Replace("AMP;", "")
                                                .Replace("  ", " ");

                        strSTATUS = panInfo.Status.ToUpper();

                        //  INVALID CASE
                        if (string.IsNullOrEmpty(strVerifiedNAME) || strVerifiedNAME == strNOTAVAILABLE)
                        {
                            txtPAN.Select();
                            cmnService.J_UserMessage("Invalid PAN");
                            this.Cursor = Cursors.Default;
                            return;
                        }

                        //  SHOW DETAILS
                        grpDetails.Visible = true;
                        lblDetails.Text = pan;
                        txtPANName.Text = strVerifiedNAME;

                        // VALID
                        if (strSTATUS == "VALID")
                        {
                            lblStatus.Visible = true;
                            lblStatus.ForeColor = Color.Green;
                            lblStatus.Text = T_PANVerificationStatus.VALID.ToString();

                            SaveOrUpdatePAN(pan, strVerifiedNAME, cmnService.J_ReturnInt32Value(T_PANVerificationStatusId.VALID_OPERATIVE));
                        }
                        // VALID OPERATIVE
                        else if (strSTATUS.Contains("INOPERATIVE"))
                        {
                            lblStatus.Visible = true;
                            lblStatus.ForeColor = Color.Yellow;
                            lblStatus.BackColor = Color.Black;
                            lblStatus.Text = T_PANVerificationStatus.VALID_INOPERATIVE.ToString();

                            SaveOrUpdatePAN(pan, strVerifiedNAME, cmnService.J_ReturnInt32Value(T_PANVerificationStatusId.VALID_INOPERATIVE));
                        }                        
                        // VALID INOPERATIVE
                        else if (strSTATUS.Contains("VALID") && strSTATUS.Contains("OPERATIVE"))
                        {
                            lblStatus.Visible = true;
                            lblStatus.ForeColor = Color.Green;
                            lblStatus.Text = T_PANVerificationStatus.VALID_OPERATIVE.ToString();

                            SaveOrUpdatePAN(pan, strVerifiedNAME, cmnService.J_ReturnInt32Value(T_PANVerificationStatusId.VALID_OPERATIVE));
                        }
                        else
                        {
                            lblStatus.Visible = true;
                            lblStatus.ForeColor = Color.Red;
                            lblStatus.Text = "Invalid PAN";

                            SaveOrUpdatePAN(pan, strVerifiedNAME, cmnService.J_ReturnInt32Value(T_PANVerificationStatusId.INVALID));
                        }

                        // NAME MATCH CHECK (MASKED)
                        if (strVerifiedNAME != GenMaskedName(txtPANName.Text))
                        {
                            txtPANName.BackColor = Color.Tan;
                        }
                    }
                    else
                    {
                        //  PAN NOT FOUND IN CSV
                        txtPAN.Select();
                        cmnService.J_UserMessage("PAN not found in downloaded CSV");
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    blnVerificationComplete = true;
                    this.Cursor = Cursors.Default;
                    GC.Collect();
                }

                if (bgwPANVerification.CancellationPending)
                {
                    e.Cancel = true;
                    blnVerificationComplete = false;
                    return;
                }
            }
            catch (Exception err)
            {
                blnVerificationComplete = false;
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region ReadPANStatus
        public Dictionary<string, PANInfo> ReadPANStatus(string filePath)
        {
            var dict = new Dictionary<string, PANInfo>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                bool isHeader = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parts = line.Split(',');

                    if (parts.Length < 3)
                        continue;

                    string pan = parts[0].Replace("\uFEFF", "").Trim().ToUpper();
                    string name = parts[1].Trim().ToUpper();
                    string status = parts[2].Trim().ToUpper();

                    if (!dict.ContainsKey(pan))
                    {
                        dict.Add(pan, new PANInfo
                        {
                            Name = name,
                            Status = status
                        });
                    }
                }
            }

            return dict;
        }
        #endregion

        #region ReadPANNameStatus
        public Dictionary<string, PANInfo> ReadPANNameStatus(string filePath)
        {
            var dict = new Dictionary<string, PANInfo>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                bool isHeader = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Handles commas inside quotes
                    var parts = Regex.Matches(line, "(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)")
                                     .Cast<Match>()
                                     .Select(m => m.Value.Trim().Trim('"'))
                                     .ToArray();

                    if (parts.Length < 3)
                        continue;

                    string pan = parts[0].Replace("\uFEFF", "").Trim().ToUpper();
                    string name = parts[1].Trim().ToUpper();
                    string status = parts[2].Trim().ToUpper();

                    if (!dict.ContainsKey(pan))
                    {
                        dict.Add(pan, new PANInfo
                        {
                            Name = name,
                            Status = status
                        });
                    }
                }
            }

            return dict;
        }
        #endregion

        #region SaveOrUpdatePAN
        private void SaveOrUpdatePAN(string pan, string name, int statusId)
        {
            if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + pan + "'") == false)
            {
                dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME, CHECKED_DATE_TIME, VERIFIED_STATUS) " +
                    "VALUES('" + pan + "','" +
                    cmnService.J_ReplaceQuote(name) + "'," +
                    GetServerDateTime() + "," + statusId + ")");
            }
            else
            {
                dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" +
                    cmnService.J_ReplaceQuote(name) + "'," +
                    "CHECKED_DATE_TIME = " + GetServerDateTime() +
                    ", VERIFIED_STATUS = " + statusId +
                    " WHERE PAN ='" + pan + "'");
            }
        }
        #endregion

        #region GetServerName
        public string GetServerName()
        {
            DataSet dtSet = null;
            string ServerName = "";
            try
            {
                if (dtSet != null) dtSet.Clear();
                dtSet = dmlService.J_ConvertXmlToDataSet(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
                if (dtSet == null)
                {
                    dmlService.Dispose();
                    return "";
                }
                //
                //string strPath = cmnService.J_Decode(dtSet.Tables[0].Rows[0][cmnService.J_Encode("DATABASENAME")].ToString());
                //-- 2021/03/11
                string strPath = "";
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                    strPath = cmnService.J_Decode(dtSet.Tables[0].Rows[0][cmnService.J_Encode("DATABASENAME")].ToString());
                else
                    strPath = cmnService.J_Decode(dtSet.Tables[0].Rows[0][cmnService.J_Encode("SERVERPATH")].ToString());
                //
                System.Xml.XmlDocument XMLDoc = new System.Xml.XmlDocument();
                XMLDoc.Load(strPath + "/" + TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer);
                //
                System.Xml.XmlNode Root = XMLDoc.SelectSingleNode(T_XML.MULTIUSERCONNECTION + "/" + T_XML.SERVERNAME);
                ServerName = cmnService.J_Decode(Root.InnerText);
                //
                if (ServerName == "")
                    return "";
                else
                    return ServerName;
            }
            catch (Exception err)
            {
                return "";
            }
        }
        #endregion

        #region GetServerDateTime 
        //-- 2016/01/29
        public string GetServerDateTime()
        {
            string strServerDT = "";
            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
            {
                strServerDT = dmlService.J_ReturnServerDate();
                //
                if (strServerDT == "")
                    strServerDT = "null";
                else
                    strServerDT = cmnService.J_DateOperator() + strServerDT + cmnService.J_DateOperator();
            }
            else
            {
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                {
                    //Added by Indrajit on 26-09-2012 to get SERVER DATETIME
                    //string strServerDT = ExecuteCommandSync("net time \\\\Pds1");
                    //Added by Indrajit on 03-10-2012
                    if (GetServerName() != "")
                        strServerDT = cmnService.J_DateOperator() + RemoteDateTime(GetServerName()) + cmnService.J_DateOperator();

                    if (strServerDT == "")
                        strServerDT = "null";
                    //else
                    //{
                    //    strServerDT = "CDATE('" + strServerDT + "')";
                    //    strServerDT = strServerDT.Replace("AM", "");
                    //    strServerDT = strServerDT.Replace("PM", "");
                    //}
                }
                else if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.SINGLE_USER)
                {
                    if (J_Var.J_pDBPath.Substring(0, 2) == "\\\\")
                    {
                        string svr = J_Var.J_pDBPath.Substring(0, J_Var.J_pDBPath.IndexOf("\\", 2)).Substring(2);
                        strServerDT = RemoteDateTime(svr);
                    }
                    else
                        strServerDT = DateTime.Now.ToString();
                    //--
                    if (strServerDT == "")
                        strServerDT = "null";
                    else
                        strServerDT = "CDATE('" + strServerDT + "')";
                }
            }
            //--
            return strServerDT;
        }

        #endregion

        #region RemoteDateTime
        public string RemoteDateTime(string ServerAddress)
        {
            string rt = "";
            //
            IntPtr handle = IntPtr.Zero;
            if (NetRemoteTOD("\\\\" + ServerAddress, ref handle) == NERR_Success)
            {
                TIME_OF_DAY_INFO time = (TIME_OF_DAY_INFO)Marshal.PtrToStructure(handle, typeof(TIME_OF_DAY_INFO));
                // new date time. The hours are in utc, so you have to use the timezone offset.
                DateTime dt = new DateTime((int)time.tod_year,
                (int)time.tod_month, (int)time.tod_day, (int)(time.tod_hours)
                , (int)time.tod_mins, (int)time.tod_secs);
                //bb
                dt = DateTime.Parse(dt.ToString());
                dt = dt.AddMinutes(330); //-- 2014/11/18 ADDDED 5 HRS 30 MIN TO THE TIME TO GET 'INDIAWALE' TIME...
                //
                rt = Convert.ToString(dt.ToString("dd/MMM/yyyy HH:mm:ss"));
                //
                uint result = NetApiBufferFree(handle);
                //
                if (result != NERR_Success)
                    MessageBox.Show("Memory cleanup failed");
            }
            return rt;
        }
        #endregion

        #region bgwPANVerification_RunWorkerCompleted
        private void bgwPANVerification_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    //btnVerification.Text = strbtnVerification;
                    //btnVerification.ForeColor = Color.Black;
                    blnVerificationComplete = false;
                }
                //--
                if (File.Exists(strSaveDownloadedCSVFilePath))
                {
                    File.Delete(strSaveDownloadedCSVFilePath);
                }
                //
                if (File.Exists(filePathCSV))
                {
                    File.Delete(filePathCSV);
                }
                //if(bgwPANVerification.CancellationPending==false)
                if (blnVerificationComplete == true)
                {
                    //cmnService.J_UserMessage("Validation completed");
                    //btnVerification.Enabled = true;
                    //btnVerification.BackColor = Color.Lavender;
                    ////
                    ////--
                    //grpStatus.Enabled = true;
                    ////
                    //btnVerification.Enabled = false;
                    ////
                    //dgvDeductees.Enabled = true;
                    //grpReturnSelection.Enabled = true;
                    //grpRegularReturn.Enabled = true;
                    //grpCorrectionReturn.Enabled = true;
                    //j = 0;
                    //                
                }
                else
                {
                    //cmnService.J_UserMessage("Validation stopped", MessageBoxIcon.Exclamation);
                    blnVerificationComplete = false;
                    //grpReturnSelection.Enabled = true;
                    //grpRegularReturn.Enabled = true;
                    //grpCorrectionReturn.Enabled = true;
                    //bgwPANVerification.CancelAsync();
                    //bgwPANVerification.Dispose();
                    //bgwPANVerification = null;
                }
                //
                //btnVerification.Text = strbtnVerification;
                //btnVerification.ForeColor = Color.Black;
                ////blnVerificationComplete = false;
                ////
                ////--
                //if (cmnService.J_ReturnInt32Value(lblInvalidNo.Text) > 0)
                //    btnPrintInvalidPAN.Enabled = true;
                //else
                //    btnPrintInvalidPAN.Enabled = false;
                ////--
                //if (cmnService.J_ReturnInt32Value(lblUnmatchedNo.Text) > 0)
                //    btnPrintUnmatched.Enabled = true;
                //else
                //    btnPrintUnmatched.Enabled = false;
                //--            
            }
            catch (Exception err)
            {
                blnVerificationComplete = false;
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region bgWorker_DoWork
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ArrayList objList = (ArrayList)e.Argument;
                ArrayList objRetval = new ArrayList();
                //-------------------------------------------------------
                enmRequestType enReqType = (enmRequestType)objList[0];
                TracesResponse objResponse = new TracesResponse();
                //this.Cursor = Cursors.WaitCursor;
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
                        //this.Cursor = Cursors.Default;
                        //---------------------------------------------------
                        break;
                    case enmRequestType.UploadCSVFile:
                        objResponse = objTracesConnect.UploadCSVPANFile(filePathCSV, out filePathCSVTokenNo, out UploadedfilePath);
                        objRetval.Add(enmRequestType.UploadCSVFile);
                        objRetval.Add(objResponse);
                        e.Result = objRetval;
                        System.Threading.Thread.Sleep(4000);
                        break;
                    case enmRequestType.DownloadCSVFile:
                        strSaveDownloadedCSVFilePath = Path.Combine(Application.StartupPath, "PAN_Data_" + filePathCSVTokenNo + ".csv");

                        objResponse = objTracesConnect.DownloadCSVPANResult(filePathCSVTokenNo, Application.StartupPath);
                        objRetval.Add(enmRequestType.DownloadCSVFile);
                        objRetval.Add(objResponse);
                        e.Result = objRetval;
                        break;
                    // LIST OF STATEMENT STATUS FILES
                    case enmRequestType.PanValidation:
                        bool bnlSuccess = false;
                        string strPAN = "";
                        DataGridViewRowCollection rowcoll = (DataGridViewRowCollection)objList[1];
                        //
                        foreach (DataGridViewRow row in rowcoll)
                        {
                            strPAN = row.Cells[0].Value.ToString();
                            //TracesResponse response = objTracesConnect.RequestForPANValidation(strPAN);
                            TracesResponse response = objTracesConnect.RequestForPANValidation_New(strPAN);

                            if (response.Respons == enmResponse.Success)
                            {
                                bnlSuccess = true;
                                PANDetails objDetails = (PANDetails)response.CustomeTypes;
                                this.bgWorker.ReportProgress(0, objDetails);
                                //this.Cursor = Cursors.Default;
                            }

                            if (response.Respons == enmResponse.SessionTimeout)
                            {
                                objResponse.Respons = enmResponse.SessionTimeout;
                                objRetval.Add(enmRequestType.PanValidation);
                                objRetval.Add(objResponse);
                                e.Result = objRetval;
                                //this.Cursor = Cursors.Default;
                                break;
                            }
                        }

                        if (bnlSuccess)
                        {
                            objResponse = new TracesResponse();
                            //
                            objResponse.Respons = enmResponse.Success;
                            objRetval.Add(enmRequestType.PanValidation);
                            objRetval.Add(objResponse);
                            e.Result = objRetval;
                            //this.Cursor = Cursors.Default;
                        }
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
            catch (Exception err)
            {
                blnVerificationComplete = false;
                //this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #region bgWorker_RunWorkerCompleted
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ArrayList objMessage = (ArrayList)e.Result;
                ArrayList objList = new ArrayList();
                //
                enmRequestType enmReqType = (enmRequestType)objMessage[0];
                TracesResponse objResponse = (TracesResponse)objMessage[1];
                this.Cursor = Cursors.WaitCursor;
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
                            ShowHideLoginDetails(enmRequestType.PanValidation);
                            //grdPANValidate.DataSource = objBindingSource;
                            objList.Add(enmRequestType.UploadCSVFile);
                            //-------------------------------------------
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(objList);
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            pgTimer.Stop();
                            //pBar.Value = 100;
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage(objResponse.Message);
                            InitializeCaptcha();
                            return;
                        }
                        else
                        {
                            //---------------------------------------------------
                            //pBar.Value = 0;
                            pgTimer.Stop();
                            //---------------------------------------------------
                        }
                        break;

                    case enmRequestType.UploadCSVFile:
                        //ArrayList objList = new ArrayList();
                        //pnlTracesMessage.Visible = false;
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            System.Threading.Thread.Sleep(2000);
                            objList.Add(enmRequestType.DownloadCSVFile);
                            //-------------------------------------------
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(objList);
                        }
                        else
                        {
                            //btnVerification.Text = strbtnVerification;
                            //btnVerification.ForeColor = Color.Black;
                            txtCaptchaCode.Text = "";
                            //if (File.Exists(filePathCSV))
                            //{
                            //    File.Delete(filePathCSV);
                            //}
                            //cmnService.J_UserMessage("Failed - Upload");
                            //cmnService.J_UserMessage("Data parsing to TRACES failed.\nPlease try later.");
                            //We have requested TRACES to verify << PAN No.>.It is taking time as their response remains pending due to high processing load. It is recommended to verify all new PANs in bulk using the ‘PAN Name Extractor’ under TRACES menu.
                            //<< Close >> button
                            cmnService.J_UserMessage("We have requested TRACES to verify " + txtPAN.Text + ".\nIt is taking time as their response remains pending due to high processing load.\nIt is recommended to verify all new PANs in bulk using the ‘PAN Name Extractor’ under TRACES menu.");
                        }
                        break;
                    case enmRequestType.DownloadCSVFile:
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            //objList.Add(enmRequestType.ExtractZIPFile);
                            //-------------------------------------------
                            //if (!bgWorker.IsBusy)
                            //    bgWorker.RunWorkerAsync(objList);
                            //cmnService.J_UserMessage("Downloaded");
                            if (!bgwPANVerification.IsBusy)
                                bgwPANVerification.RunWorkerAsync();
                        }
                        else
                        {
                            //btnVerification.Text = strbtnVerification;
                            //btnVerification.ForeColor = Color.Black;
                            txtCaptchaCode.Text = "";
                            //if (File.Exists(filePathCSV))
                            //{
                            //    File.Delete(filePathCSV);
                            //}
                            //cmnService.J_UserMessage("Failed - Download");
                            cmnService.J_UserMessage("Error encountered at TRACES.\nPlease try later.");
                        }
                        break;

                    case enmRequestType.PanValidation:

                        this.pgTimer.Stop();
                        this.pgTimer.Interval = 1000;
                        //pBar.Value = 100;

                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            this.Cursor = Cursors.Default;
                            ShowHideLoginDetails(enmRequestType.Login);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Success)
                        {

                            //PANDetails objDetails = (PANDetails)objResponse.CustomeTypes;

                            //// grdPANValidate.Rows.Add(objDetails.Name, objDetails.PAN, objDetails.Status);

                            //DataGridViewRow PANrow = new DataGridViewRow();
                            //PANrow.CreateCells(grdPANValidate);

                            //PANrow.Cells[0].Value = objDetails.Name;
                            //PANrow.Cells[1].Value = objDetails.PAN;
                            //PANrow.Cells[2].Value = objDetails.Status;

                            //grdPANValidate.Rows.Add(PANrow);
                        }
                        break;
                    case enmRequestType.LogOff:
                        this.pgTimer.Stop();
                        //pBar.Value = 100;

                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtTAN.Text = "";
                        txtCaptchaCode.Text = "";
                        //grpDownloadList.Visible = false;
                        grpLoginDetails.Visible = true;
                        // grpProgress.Visible = true;
                        //BtnSave.Enabled = true;
                        //BtnSave.BackColor = Color.Lavender;
                        InitializeCaptcha();
                        //pBar.Value = 0;
                        break;
                }
                //-------------------------------------------------------
            }
            catch (Exception err)
            {
                blnVerificationComplete = false;
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region txtTANNo_KeyPress
        private void txtTANNo_KeyPress(object sender, KeyPressEventArgs e)
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
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstDeducteeHelp.Visible = false;
            }
            else
                if (TdsMan.gTANNoPANNoValidation(txtTAN, e, T_TANPAN.TAN) == false)
                    e.Handled = true;
        }

        #endregion

        #region txtTAN_KeyDown
        private void txtTAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstDeducteeHelp.Visible == true)
                {
                    lstDeducteeHelp.Focus();
                    lstDeducteeHelp.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region txtTAN_TextChanged
        private void txtTAN_TextChanged(object sender, EventArgs e)
        {
            //cmbFAYear_SelectedIndexChanged(sender, e);

            IDataReader drdShowDeducteeHelp = null;
            //--
            try
            {
                if (blnExit == false)
                    return;
                //
                if (txtTAN.Text.Trim() == "")
                {
                    lstDeducteeHelp.Visible = false;
                    return;
                }

                //if (blnShowHelp == false)
                //    return;
                //-----------------------
                strSQL = "SELECT TAN_ACCOUNT_ID," +
                         "       TAN_NO," +
                         "       LOGIN_ID," +
                         "       USER_PASSWORD," +
                         "       COMPANY_NAME " +
                         "FROM   MST_TAN_ACCOUNT " +
                         "WHERE  TAN_NO LIKE '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "%' " +
                         "ORDER BY TAN_NO, TAN_ACCOUNT_ID";

                drdShowDeducteeHelp = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShowDeducteeHelp == null)
                {
                    lstDeducteeHelp.Visible = false;
                    return;
                }
                else
                {
                    lstDeducteeHelp.Items.Clear();
                    lstDeducteeHelp.Height = 15;
                    lstDeducteeHelp.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12)
                                                                + drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15)
                                                                + TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdShowDeducteeHelp["USER_PASSWORD"].ToString()).PadRight(10)
                                                                + " " + drdShowDeducteeHelp["COMPANY_NAME"].ToString(),
                                                                Convert.ToInt32(drdShowDeducteeHelp["TAN_ACCOUNT_ID"])));
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

        #region txtTAN_Leave
        private void txtTAN_Leave(object sender, EventArgs e)
        {
            if (txtTAN.Text.Trim() == "") return;
            //INITIALIZE CAPTCHA CODE
            //InitializeCaptcha();

        }
        #endregion

        #region txtUserId_Enter
        private void txtUserId_Enter(object sender, EventArgs e)
        {
            //DISABLE LIST VIEW WHEN CURSOR ENTERS ANY OTHER CONTROL
            lstDeducteeHelp.Visible = false;
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
                txtTAN.Select();
            }
        }
        #endregion

        #region lstDeducteeHelp_Click
        private void lstDeducteeHelp_Click(object sender, EventArgs e)
        {
            //--
            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstDeducteeHelp, lstDeducteeHelp.SelectedIndex));
            txtTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtUserID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            //--
            lstDeducteeHelp.Visible = false;
            //--
        }
        #endregion

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            grpLoginDetails.Visible = false;
            txtPAN.Select();
            //--

        }
        #endregion

        #region btnLogging_Click
        private void btnLogging_Click(object sender, EventArgs e)
        {
            try
            {
                //if (btnLogging.Text == strLogOff)
                //{
                //    ShowHideLoginDetails(enmRequestType.LogOff);
                //    return;
                //}
                //pBar.Value = 0;
                //--
                #region VALIDATE LOGIN DETAILS
                if (txtTAN.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Enter TAN");
                    txtTAN.Select();
                    return;
                }
                //
                //if (txtUserID.Text.Trim() == "")
                //{
                //    cmnService.J_UserMessage("Enter User ID");
                //    txtUserID.Select();
                //    return;
                //}
                //
                if (txtPassword.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Enter Password");
                    txtPassword.Select();
                    return;
                }
                //
                if (txtCaptchaCode.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Enter Captcha Code");
                    txtCaptchaCode.Select();
                    return;
                }
                //
                #endregion
                //--
                //if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    return;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    //BtnExit.Select();
                    return;
                }
                //--
                //if (!ValidateFields()) return;
                //--
                //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER
                strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "' ";
                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    //insering new record in the tan login master
                    strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

                    dmlService.J_ExecSql(strSQL);
                }
                else
                {
                    //updating the existing record in the master

                    strSQL = "UPDATE MST_TAN_ACCOUNT " +
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "', " +
                             "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                             "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "' ";

                    dmlService.J_ExecSql(strSQL);
                }
                //#################
                //--
                TracesLogin objLogin = new TracesLogin();
                objLogin.UserID = txtUserID.Text;
                objLogin.Password = txtPassword.Text;
                objLogin.TAN = txtTAN.Text;
                objLogin.CaptchaCode = txtCaptchaCode.Text;
                objLogin.CaptchaId = this.CurrentCaptchaId; //-- 2026/04/08
                //--------------------------------------------
                ArrayList objList = new ArrayList();
                objList.Add(enmRequestType.Login);
                objList.Add(objLogin);
                //-------------------------------------------
                pgTimer.Start();
                blnVerificationComplete = false;
                //-------------------------------------------
                if (!bgWorker.IsBusy)
                    bgWorker.RunWorkerAsync(objList);
                //--
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return;
            }

        }
        #endregion

        #region TrnPANVerificationSummaryTraces_FormClosed
        private void TrnPANVerificationSummaryTraces_FormClosed(object sender, FormClosedEventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES = "";
        }
        #endregion

        #endregion 

        #region User Define Functions

        #region Clearcontrols
        private void Clearcontrols()
        {
           grpDetails.Visible = false;
           lblDetails.Text = "";
           txtPANName.Text = "";
           lblMiddleName.Text = "";
           lblFirstName.Text = "";
           lblAreaCode.Text = "";
           lblAOType.Text = "";
           lblRangeCode.Text = "";
           lblAONumber.Text = "";
           lblJurisdiction.Text = "";
           lblBuildingName.Text = "";
        }
        #endregion        

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region ShowCaptcha
        //private void ShowCaptcha()
        //{
        //    //--
        //    if (TdsMan.T_CheckInternetConnectivty() == false)
        //    {
        //        cmnService.J_UserMessage("Internet Connectivity not found");
        //        //BtnExit.Select();
        //        return;
        //    }
        //    //
        //    objAccount = new TracesConnect();

        //    //Stream imgStream = objAccount.GetCaptchaForPANVerify();
        //    Stream imgStream = objAccount.GetCaptchaForPANNAme();
        //    Image img = Image.FromStream(imgStream);
        //    this.picCaptcha.Image = img;
        //    //-------------------------------------------------------
        //    txtCaptchaCode.Text = "";

        //}

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
                //this.picCaptcha.Image = img;
                var captcha = objTracesConnect.MakeInitialRequest_NEW();
                this.CurrentCaptchaId = captcha.CaptchaId;
                Image captchaImage = captcha.CaptchaImage;
                this.picCaptcha.Image = captchaImage;
                //-------------------------------------------------------
                txtCaptchaCode.Text = "";
            }
            catch (Exception err)
            {
                picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #region ShowHideLoginDetails
        void ShowHideLoginDetails(enmRequestType enmStatus)
        {
            switch (enmStatus)
            {
                case enmRequestType.Login:
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    txtCaptchaCode.Text = "";
                    txtTAN.Text = "";
                    grpEnterLoginDetails.Enabled = true;
                    grpCaptcha.Enabled = true;
                    //btnLogging.Text = strLogOn;
                    //LoadDeducteeGrid();
                    //
                    InitializeCaptcha();
                    break;
                case enmRequestType.PanValidation:
                    //--
                    grpLoginDetails.Visible = false;
                    //
                    //if (!bgwPANVerification.IsBusy)
                    //    bgwPANVerification.RunWorkerAsync();
                    //
                    //--
                    break;

                case enmRequestType.LogOff:
                    ArrayList objList = new ArrayList();
                    objList.Add(enmRequestType.LogOff);
                    //-------------------------------------------
                    pgTimer.Start();
                    //-------------------------------------------
                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync(objList);
                    //
                    break;

            }
        }



        #endregion

        #endregion

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }

        #region GenMaskedName
        private string GenMaskedName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "";

            // Normalize
            name = name.ToUpper().Trim();
            name = Regex.Replace(name, @"\s+", " ");       // remove extra spaces
            name = Regex.Replace(name, @"[^A-Z ]", "");    // remove special chars

            var words = name.Split(' ');
            List<string> maskedWords = new List<string>();

            foreach (var word in words)
            {
                if (string.IsNullOrWhiteSpace(word))
                    continue;

                if (word.Length == 1)
                {
                    maskedWords.Add(word);
                }
                else if (word.Length == 2)
                {
                    maskedWords.Add(word); // usually unchanged
                }
                else
                {
                    string masked =
                        word[0] +
                        new string('X', word.Length - 2) +
                        word[word.Length - 1];

                    maskedWords.Add(masked);
                }
            }

            return string.Join(" ", maskedWords);
        }
        #endregion

        #region ExportSinglePANToCSV
        private string ExportSinglePANToCSV(string pan, string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pan))
                    throw new Exception("PAN cannot be empty.");

                pan = pan.Trim().ToUpper();

                // Build file path
                string filePath = Path.Combine(
                    Application.StartupPath,
                    fileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv"
                );

                StringBuilder sb = new StringBuilder();

                // HEADER
                sb.AppendLine("PAN");

                // DATA
                sb.AppendLine(EscapeCSV(pan));

                // WRITE FILE
                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

                return filePath; // return path if needed
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage("CSV creation failed: " + ex.Message);
                return "";
            }
        }
        #endregion

        #region EscapeCSV
        private string EscapeCSV(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            if (input.Contains(",") || input.Contains("\"") || input.Contains("\n"))
            {
                input = input.Replace("\"", "\"\"");
                input = "\"" + input + "\"";
            }

            return input;
        }
        #endregion
    }

}