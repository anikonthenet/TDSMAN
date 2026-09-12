
#region Refered Namespaces & Classes

extern alias global2;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormBrowser;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnDownloadFilesTraces : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnDownloadFilesTraces()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        #region Objects & Variables declaration

        TracesConnect objAccount = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        
        DirectoryInfo directorySelected;

        DMLService dmlService = new DMLService();

        string strSQL = string.Empty;
        bool blnShowHelp = false;

        //ADDED BY DHRUB  ON 01/01/2014 FOR UNZIP DOWNLOADED FILE 
        string strExtractPath = "";
        string strDownloadedFileType = "";
        string strRequestNo = "";
        string strFormNo = "";
        string strQuarter = "";
        string strFaYear = "";
        string strPassWord = "";
        int intFaYearID = 0, intNoOfCertificates = 0;
         string strFileName = "";

        //--            
        ToolTip tllTip = new ToolTip();

        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        private string CurrentCaptchaId = "";
        enum enmRequestType
        {
            Login,
            DownloadList,
            Download,
            Extraction16,
            Extraction16_PartB,
            LogOff
        }
        enum enmExtractionType
        {
            Form16_16A,
            Form16_PartB,
            Form27D,
            Signing
        }

        int intRowIndex = 0;
        int intColumnIndex = 0;
        bool blFilesExtracted = false;
        bool blFilesSigned = false;
        string strExtractionFolder = "", strExtractionType="";
        int intProgressValueExtraction = 0, intTimeInterval = 80;
        #endregion

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

        //-- Added By Abhishek Dey On 22/08/2018 --
        #region TrnDownloadFilesTraces_Activated
        private void TrnDownloadFilesTraces_Activated(object sender, EventArgs e)
        {
            //-- Added By Abhishek Dey On 20/08/2018 --
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //-----------------------------------------
            //
        }
        #endregion
        //-----------------------------------------


        #region TrnViewReturnStatusOnline_Load
        private void TrnViewReturnStatusOnline_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //--
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.Expect100Continue = false;
            //--InitializeCaptcha();
            picCaptcha.Image = Properties.Resources.captcha_loading;
            if (!bgWorkerLoadCaptcha.IsBusy)
                bgWorkerLoadCaptcha.RunWorkerAsync();
            //--
            lblTitle.Text = "Download Requested Files";
            //
            txtUserID.Text = "";
            txtPassword.Text = "";
            txtCaptchaCode.Text = "";
            txtTANNo.Text = "";
            grpDownloadList.Visible = false;
            grpLoginDetails.Visible = true;
            grpProgress.Visible = true;
            //For Showing the helg grid when tan text is changed
            blnShowHelp = true;
            //
            //-- 2018/09/04
            if (TDSMAN.Classes.TDSMAN.T_BROWSER_LOGIN_TRACES == true)
            {
                grpProgress.Visible = false;
                btnCaptchaRefresh.Visible = false;
                lblCaptchaCaption.Visible = false;
                txtCaptchaCode.Visible = false;
                picCaptcha.Visible = false;
                //--
                grpTextMessage.Visible = true;
                lblTextMessage1.Text = "1. Open Internet Properties - either by 'type inetcpl.cpl in Run' or 'Open the Internet Explorer > tools >       Internet Options',";
                lblTextMessage2.Text = "2. goto 'Security' tab,";
                lblTextMessage3.Text = "3. goto 'Custom level of Internet',";
                lblTextMessage4.Text = "4. scroll down to 'Downloads',";
                lblTextMessage5.Text = "5. click enable of 'Automatic prompting for file downloads'.";
                lblTextMessage6.Text = "Please click 'Logout' after completing the task.";
                //--
            }
            //--
            txtTANNo.Select();
        }
        #endregion              

        #region btnLogin_Click
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("1");
                //if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    return;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
                    return;
                }
                //--
                //MessageBox.Show("1.1");
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
                    //--
                    strSQL = "UPDATE MST_TAN_ACCOUNT " +
                             "INNER JOIN MST_COMPANY " +
                             "ON    MST_TAN_ACCOUNT.TAN_NO       = MST_COMPANY.TAN_NO " +
                             "SET   MST_TAN_ACCOUNT.COMPANY_NAME = MST_COMPANY.COMPANY_NAME " +
                             "WHERE MST_TAN_ACCOUNT.COMPANY_NAME = ''";
                    dmlService.J_ExecSql(strSQL);
                    //--
                    strSQL = "UPDATE MST_TAN_ACCOUNT " +
                             "INNER JOIN COR_HDR_COMPANY " +
                             "ON    MST_TAN_ACCOUNT.TAN_NO       = COR_HDR_COMPANY.TAN_NO " +
                             "SET   MST_TAN_ACCOUNT.COMPANY_NAME = COR_HDR_COMPANY.COMPANY_NAME " +
                             "WHERE MST_TAN_ACCOUNT.COMPANY_NAME = ''";
                    dmlService.J_ExecSql(strSQL);
                    //--
                    strSQL = "UPDATE MST_TAN_ACCOUNT " +
                             "SET    COMPANY_NAME = '<NOT AVAILABLE>' " +
                             "WHERE  COMPANY_NAME = ''";
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
                //--

                //MessageBox.Show("1.2");
                //--
                if (TDSMAN.Classes.TDSMAN.T_BROWSER_LOGIN_TRACES == true)
                {
                    #region TRACES - BROWSER

                    if (cmnService.J_UserMessage("This will take you to 'tdscpc.gov.in' webpage, where you have to enter the captcha/verification code for login, for any query regarding the\n contents of the linked page please contact the concerned website.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                    //PROVIDING LOGIN DETAILS
                    TDSMAN.Classes.TDSMAN.T_TracesUserID = txtUserID.Text.Trim();
                    TDSMAN.Classes.TDSMAN.T_TracesPassword = txtPassword.Text.Trim();
                    TDSMAN.Classes.TDSMAN.T_TracesTAN = txtTANNo.Text.Trim();
                    TDSMAN.Classes.TDSMAN.T_TracesDownloadFiles = true;
                    //objLogin.CaptchaCode = txtCaptcha.Text;
                    //--
                    TrnRequestConsolidatedFileBrowser TrnRequestConsolidatedFileBrowser = new TrnRequestConsolidatedFileBrowser();
                    TrnRequestConsolidatedFileBrowser.MdiParent = TrnDownloadFilesTraces.ActiveForm;
                    TrnRequestConsolidatedFileBrowser.Show();

                    #endregion
                }
                else
                {
                    //MessageBox.Show("1.3");
                    TracesLogin objLogin = new TracesLogin();
                    objLogin.UserID = txtUserID.Text;
                    objLogin.Password = txtPassword.Text;
                    objLogin.TAN = txtTANNo.Text;
                    objLogin.CaptchaCode = txtCaptchaCode.Text;
                    objLogin.CaptchaId = this.CurrentCaptchaId; //-- 2026/04/08

                    //MessageBox.Show("1.4");
                    //--------------------------------------------
                    ArrayList objList = new ArrayList();
                    objList.Add(enmRequestType.Login);
                    objList.Add(objLogin);

                    //MessageBox.Show("1.5");
                    //-------------------------------------------
                    pgTimer.Start();
                    //-------------------------------------------
                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync(objList);
                }
                //
            }
            catch //(Exception err)
            {
                //cmnService.J_UserMessage(err.Message);
                cmnService.J_UserMessage("We are not able to receive the response from the TRACES webiste.\n It is requested to check your details at TRACES website with the given parameters");
            }
        }

        #endregion
        
        #region btnLoginCancel_Click
        private void btnLoginCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            InitializeCaptcha();
        }

        #endregion

        #region btnCaptchaRefresh_MouseMove
        private void btnCaptchaRefresh_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.Show("Click to refresh image", btnCaptchaRefresh);
        }
        #endregion

        #region bgWorker_DoWork
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayList objList = (ArrayList)e.Argument;
            ArrayList objRetval = new ArrayList();            
            //-------------------------------------------------------
            enmRequestType enReqType = (enmRequestType)objList[0];
            TracesResponse objResponse;
            //--
            //-------------------------------------------------------
            switch (enReqType)
            {
                // LOGIN REQUEST
                case enmRequestType.Login:
                    ////MessageBox.Show("1");
                    //objResponse = objAccount.makeLoginToTRACES((TracesLogin)objList[1]);

                    //MessageBox.Show("1.6");
                    objResponse = objAccount.makeLoginToTraces_New((TracesLogin)objList[1]);
                    //---------------------------------------------------
                    objRetval.Add(enmRequestType.Login);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    //---------------------------------------------------
                    break;
                // LIST OF DOWNLOAD FILES
                case enmRequestType.DownloadList:
                    ////MessageBox.Show("2");
                    DataTable table;
                    //TracesResponse response = objAccount.RequestForAllDownloadList(out table);
                    TracesResponse response = objAccount.RequestForAllDownloadList_New(out table);
                    ////MessageBox.Show("2.1");
                    objRetval.Add(enmRequestType.DownloadList);
                    objRetval.Add(response);
                    objRetval.Add(table);
                    e.Result = objRetval;
                    break;
                 // REQUEST FOR DOWNLOAD LIST
                case enmRequestType.Download:

                    string strPath = (string)objList[2];
                    objResponse = objAccount.RequestForDownloadFile((string)objList[1], strPath);

                    objRetval.Add(enmRequestType.Download);
                    objRetval.Add(objResponse);
                    e.Result = objRetval;

                    break;
                
                //REQUEST FOR LOG OFF
                case enmRequestType.LogOff:
                    objResponse = objAccount.Logoff();
                    objRetval.Add(enmRequestType.LogOff);
                    objRetval.Add(objResponse);
                    e.Result = objRetval;
                    break;
                    ////-- 2024/04/19
                    //case enmRequestType.Extraction16:
                    //    strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorBundle\\RUN_PDF_GEN.BAT");
                    //    strJAVAFile = " PDFGeneratorCLI "; strworkingFolder = "PDFGeneratorBundle";
                    //    strExtractionFolder = txtTANNo.Text + "_" + dgvDownloadList.Rows[intRowIndex].Cells[2].Value.ToString() + "_" + dgvDownloadList.Rows[intRowIndex].Cells[4].Value.ToString() + "_" + dgvDownloadList.Rows[intRowIndex].Cells[3].Value.ToString() + "_" + TdsMan.J_ReturnServerDateTimeMMDDYYYYHHMMSS().ToString().Replace("/","").Replace(":","");
                    //    //this.Cursor = Cursors.WaitCursor;
                    //    blFilesExtracted = TdsMan.ExtractCertificates(strBatFile, strJAVAFile, strworkingFolder, Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), txtTANNo.Text, Path.Combine(strExtractPath, strExtractionFolder));
                    //    //this.Cursor = Cursors.Default;                    
                    //    objRetval.Add(enmRequestType.Extraction16);
                    //    objRetval.Add();
                    //    e.Result = objRetval;
                    //    break;
                    //case enmRequestType.Extraction16_PartB:
                    //    strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorPartBBundle\\RUN_PDF_GEN.BAT");
                    //    strJAVAFile = " PDFPartBGeneratorCLI "; strworkingFolder = "PDFGeneratorPartBBundle";
                    //    strExtractionFolder = txtTANNo.Text + "_" + dgvDownloadList.Rows[intRowIndex].Cells[2].Value.ToString() + "_" + dgvDownloadList.Rows[intRowIndex].Cells[4].Value.ToString() + "_" + dgvDownloadList.Rows[intRowIndex].Cells[3].Value.ToString() + "_" + TdsMan.J_ReturnServerDateTimeMMDDYYYYHHMMSS().ToString().Replace("/", "").Replace(":", "");
                    //    //this.Cursor = Cursors.WaitCursor;
                    //    blFilesExtracted = TdsMan.ExtractCertificates(strBatFile, strJAVAFile, strworkingFolder, Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), txtTANNo.Text, Path.Combine(strExtractPath, strExtractionFolder));
                    //    //this.Cursor = Cursors.Default;
                    //    objRetval.Add(enmRequestType.Extraction16_PartB);
                    //    objRetval.Add("");
                    //    e.Result = objRetval;
                    //    break;
            }
        }

        #endregion

        #region bgWorker_RunWorkerCompleted
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Default;
                //
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
                            ////MessageBox.Show("1.2");
                            ShowHideLoginDetails(enmRequestType.DownloadList);
                            //
                            ArrayList objList = new ArrayList();
                            objList.Add(enmRequestType.DownloadList);
                            //-------------------------------------------
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(objList);
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            pgTimer.Stop();
                            pBar.Value = 100;
                            cmnService.J_UserMessage(objResponse.Message);
                            InitializeCaptcha();
                            return;
                        }
                        //---------------------------------------------------
                        pBar.Value = 0;
                        //---------------------------------------------------
                        break;

                    case enmRequestType.DownloadList:

                        this.pgTimer.Stop();
                        //this.pgTimer.Interval = 1000;
                        pBar.Value = 100;
                        DataTable dTable = (DataTable)objMessage[2];
                        //MessageBox.Show("2.2");
                        PopulateDatagridView(dTable);
                        break;
                    case enmRequestType.Download:
                        pgTimerGrid.Stop();
                        dgvDownloadList.Rows[intRowIndex].Cells[9].Value = 100;
                        if (objResponse.Respons == enmResponse.Success)
                           //ADDED BY DHRUB ON 01/01/2014 FOR UNZIP FILE 
                            switch (strDownloadedFileType)
                            {
                                case T_DownloadedOutputFileType.NSDLConsoFile:
                                    //break;
                                case T_DownloadedOutputFileType.JustificationReport:
                                    if (cmnService.J_UserMessage("Your requested file has been downloaded.Do you want to extract the downloaded file?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        if(CreatePassword(strDownloadedFileType) ==true)
                                            cmnService.J_UserMessage("File has been extracted sucessfully..");
                                        else
                                            cmnService.J_UserMessage("File extraction Failed!",MessageBoxIcon.Exclamation);
                                    }
                                    break;
                                case T_DownloadedOutputFileType.Form16A: //-- 2025/04/19
                                case T_DownloadedOutputFileType.BulkForm16AFile: //-- 2025/04/19
                                case T_DownloadedOutputFileType.BulkForm16File: //-- 2025/04/19
                                case T_DownloadedOutputFileType.BulkForm27DFile: //-- 2025/05/06
                                    //if (cmnService.J_UserMessage("Download Completed.\nDo You want to extract the Certificates (insert the DSC for signing)?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    //if (cmnService.J_UserMessage("Download Completed.\n\nDo you want to extract the certificates?\n(DSC signing will occur automatically if inserted, and this process may take additional time)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    if (cmnService.J_UserMessage("Download Completed.\nProceed with TDS/TCS Certificate extraction?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        TdsMan.DeleteAllFilesInFolder(Path.Combine(Application.StartupPath, "temp"));
                                        //
                                        UnZipFiles(Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), Path.Combine(Application.StartupPath, "temp"), txtTANNo.Text, false);
                                        //
                                        string strtxtFileName = Directory.GetFiles(Path.Combine(Application.StartupPath, "temp"), "*.txt").FirstOrDefault();
                                        //
                                        var iNoOfCertificates = TdsMan.CountBatchesfromExtractedTextFile(strtxtFileName, dgvDownloadList.Rows[intRowIndex].Cells[7].Value.ToString());
                                        intNoOfCertificates = 0;
                                        intNoOfCertificates = iNoOfCertificates.Values.Count();
                                        //
                                        lblCertExtractionPath.Text = strExtractPath;
                                        ////ArrayList objList = new ArrayList();
                                        ////if(dgvDownloadList.Rows[intRowIndex].Cells[7].Value.ToString() == "PartB")
                                        ////    strExtractionType = enmExtractionType.Form16_PartB.ToString();
                                        ////else if (dgvDownloadList.Rows[intRowIndex].Cells[5].Value.ToString().Contains("27D") == true )
                                        ////    strExtractionType = enmExtractionType.Form27D.ToString();
                                        ////else
                                        ////    strExtractionType = enmExtractionType.Form16_16A.ToString();
                                        //////--
                                        ////this.Cursor = Cursors.WaitCursor;
                                        ////if (!bgWorkerCertificateExtraction.IsBusy)
                                        ////    bgWorkerCertificateExtraction.RunWorkerAsync(objList);
                                        //
                                        rbnGenCert.Checked = false;
                                        rbnGenCertDSC.Checked = false;
                                        btnProceedExtraction.Visible = true;
                                        btnCancelExtraction.Text = "&Cancel";
                                        grpCertExtraction.Visible = true;
                                        lblCertExtractionText5.ForeColor = Color.Blue;
                                        lnkCertExtractionText7.Font = new Font(lnkCertExtractionText7.Font.FontFamily, lnkCertExtractionText7.Font.Size, FontStyle.Regular);
                                        tbcCertificateExtraction.SelectTab(tbpCertExtraction1);
                                        lblCertExtractionHeaderText.Text = "TAN: " + txtTANNo.Text + " | " + dgvDownloadList.Rows[intRowIndex].Cells[3].Value + " of " + dgvDownloadList.Rows[intRowIndex].Cells[2].Value + " | Type: " + dgvDownloadList.Rows[intRowIndex].Cells[5].Value + " " + dgvDownloadList.Rows[intRowIndex].Cells[7].Value + " | Total : " + intNoOfCertificates.ToString() + " Nos.";
                                    }
                                    break;
                                //case T_DownloadedOutputFileType.BulkForm16AFile: //-- 2025/04/19
                                //    if (cmnService.J_UserMessage("Download Completed.\nDo You want to extract the Certificates (insert the DSC for signing)?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                //    {
                                //        ArrayList objList = new ArrayList();
                                //        objList.Add(enmRequestType.Extraction16);
                                //        //--
                                //        if (!bgWorker.IsBusy)
                                //            bgWorker.RunWorkerAsync(objList);
                                //    }
                                //    break;
                                case T_DownloadedOutputFileType.TANPANFile:
                                    cmnService.J_UserMessage("Download Completed.");
                                    break;
                                    //case T_DownloadedOutputFileType.TANPANFile: 
                                    //    if (cmnService.J_UserMessage("Your requested file has been downloaded.Do you want to extract the downloaded file?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    //    {
                                    //        CreatePassword(strDownloadedFileType);
                                    //        cmnService.J_UserMessage("File has been extracted sucessfully..");
                                    //    }
                                    //    break;
                                    //default:
                                    //    cmnService.J_UserMessage("Download Completed.");
                                    //break;
                            }
                            
                           //-------------------------------------
                           
                        else
                        {
                            cmnService.J_UserMessage("Download Failed :: " + objResponse.Message,MessageBoxIcon.Exclamation);
                           // ShowHideLoginDetails(enmRequestType.LogOff);
                        }

                        break;
                    //case enmRequestType.Extraction16:
                    //case enmRequestType.Extraction16_PartB:
                    //    if(blFilesExtracted == true)
                    //    {
                    //        System.Diagnostics.Process.Start(Path.Combine(strExtractPath, strExtractionFolder ));
                    //    }
                    //    break;
                    case enmRequestType.LogOff:
                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtTANNo.Text = "";
                        txtCaptchaCode.Text = "";
                        grpDownloadList.Visible = false;
                        grpLoginDetails.Visible = true;
                        grpProgress.Visible = true;
                        BtnSave.Enabled = true;
                        BtnSave.BackColor = Color.Lavender;
                        InitializeCaptcha();
                        pBar.Value = 0;
                        break;
                }
                //-------------------------------------------------------
            }
            catch(Exception err)
            {
                cmnService.J_UserMessage(err.Message,MessageBoxIcon.Error);
            }
        }

        #endregion

        #region pgTimer_Tick
        private void pgTimer_Tick(object sender, EventArgs e)
        {
            // Slow down
            //this.pgTimer.Interval = (this.pgTimer.Interval * 2);

            // SLOW DOWN THE INTERVAL
            this.pgTimer.Interval = 1000;
            this.pBar.Step = 5;

            // Update progress bar
            if ((pBar.Value + pBar.Step) > pBar.Maximum)
            {
                pBar.Value = pBar.Minimum;
            }
            else
            {
                pBar.Value += pBar.Step;
            }
        }

        #endregion

        #region pgTimerGrid_Tick
        private void pgTimerGrid_Tick(object sender, EventArgs e)
        {
            int intValue = Convert.ToInt32(Convert.ToString(dgvDownloadList.Rows[intRowIndex].Cells[9].Value) == "" ? "0" : dgvDownloadList.Rows[intRowIndex].Cells[9].Value);
            if (intValue == 100) intValue = 0;

            // Slow down
            this.pgTimerGrid.Interval = (this.pgTimerGrid.Interval * 2);

            //Update progress bar
            if ((intValue + 1) > 100)
            {
                dgvDownloadList.Rows[intRowIndex].Cells[9].Value = 100;
            }
            else
            {
                intValue += 1;
                dgvDownloadList.Rows[intRowIndex].Cells[9].Value = intValue;
            }
        }

        #endregion

        #region dgvDownloadDetails_CellClick
        private void dgvDownloadList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //GETTING ROW & COLUMN INDEX OF SELECT ROW
            intRowIndex = e.RowIndex;
            intColumnIndex = e.ColumnIndex;
            //-------------------------------------------------------------------------------
            if (e.ColumnIndex == dgvDownloadList.Columns["btnDownload"].Index && e.RowIndex >= 0)
            {
                if (Convert.ToString(dgvDownloadList.Rows[e.RowIndex].Cells[intColumnIndex].Value).ToLower() == "download")
                {
                    DialogResult result = fbdFolder.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        //-------------------------------------------------------------------------------
                        pgTimerGrid.Start();
                        // //MessageBox.Show("Button on row {0} clicked" + e.RowIndex + " " + dgvDownloadDetails.Rows[e.RowIndex].Cells[intColumnIndex].Value);

                        ArrayList objList = new ArrayList();
                        //-------------------------------------------------------------------------------
                        objList.Add(enmRequestType.Download);
                        objList.Add(dgvDownloadList.Rows[e.RowIndex].Cells[1].Value);
                        objList.Add(fbdFolder.SelectedPath);
                        
                        //-------------------------------------------------------------------------------
                        //ADDED BY DHRUB ON 02/01/2014 FOR UNZIP THE DOWNLOADED ZIP FILE 
                        //-------------------------------------------------------------------------------
                        strRequestNo = Convert.ToString(dgvDownloadList.Rows[e.RowIndex].Cells[1].Value);
                        strFaYear = Convert.ToString(dgvDownloadList.Rows[e.RowIndex].Cells[2].Value);
                        strQuarter = Convert.ToString(dgvDownloadList.Rows[e.RowIndex].Cells[3].Value);
                        strFormNo = Convert.ToString(dgvDownloadList.Rows[e.RowIndex].Cells[4].Value);
                        strDownloadedFileType = Convert.ToString(dgvDownloadList.Rows[e.RowIndex].Cells[5].Value);
                        directorySelected = new DirectoryInfo(fbdFolder.SelectedPath);
                        strExtractPath = fbdFolder.SelectedPath;
                        //-------------------------------------------------------------------------------
                        this.Cursor = Cursors.WaitCursor;
                        if (!bgWorker.IsBusy)
                            bgWorker.RunWorkerAsync(objList);
                        //-------------------------------------------------------------------------------
                    }

                }
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
                if (TdsMan.gTANNoPANNoValidation(txtTANNo, e, T_TANPAN.TAN) == false)
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
                if (txtTANNo.Text.Trim() == "")
                {
                    lstDeducteeHelp.Visible = false;
                    return;
                }

                if (blnShowHelp == false)
                    return;
                //-----------------------
                strSQL = "SELECT TAN_ACCOUNT_ID," +
                         "       TAN_NO," +
                         "       LOGIN_ID," +
                         "       USER_PASSWORD," +
                         "       COMPANY_NAME " +
                         "FROM   MST_TAN_ACCOUNT " +
                         "WHERE  TAN_NO LIKE '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "%' " +
                         "ORDER BY TAN_NO, TAN_ACCOUNT_ID";

                drdShowDeducteeHelp = dmlService.J_ExecSqlReturnReader(strSQL);
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
                    //lstDeducteeHelp.Height = 15;
                    lstDeducteeHelp.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) +
                                                                  drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15) +
                                                                  TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdShowDeducteeHelp["USER_PASSWORD"].ToString()).PadRight(10) +
                                                                  " " + drdShowDeducteeHelp["COMPANY_NAME"].ToString(),
                                                                  Convert.ToInt32(drdShowDeducteeHelp["TAN_ACCOUNT_ID"])));
                        //--
                        //if (lstDeducteeHelp.Height <= 300)
                        //    lstDeducteeHelp.Height = lstDeducteeHelp.Height + 19;
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
            if (txtTANNo.Text.Trim() == "") return;
            //INITIALIZE CAPTCHA CODE
            //InitializeCaptcha();

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
                txtTANNo.Select();
            }
        }
        #endregion

        #region lstDeducteeHelp_Click
        private void lstDeducteeHelp_Click(object sender, EventArgs e)
        {
            //string strlstDeducteeHelp = lstDeducteeHelp.Text;
            //txtTANNo.Text = cmnService.J_Left(strlstDeducteeHelp, 10);
            //txtUserID.Text = cmnService.J_Mid(strlstDeducteeHelp, strlstDeducteeHelp.IndexOf(' '),
            //                                  strlstDeducteeHelp.LastIndexOf(' ') - strlstDeducteeHelp.IndexOf(' ')).Trim();
            //txtPassword.Text = cmnService.J_Right(strlstDeducteeHelp, strlstDeducteeHelp.Length - strlstDeducteeHelp.LastIndexOf(' ')).Trim();
            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstDeducteeHelp, lstDeducteeHelp.SelectedIndex));
            txtTANNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtUserID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            //--
            lstDeducteeHelp.Visible = false;
            //--
            txtCaptchaCode.Select();
        }
        #endregion

        #region txtRequestNo_TextChanged
        private void txtRequestNo_TextChanged(object sender, EventArgs e)
        {
            //------------------------------------------------------------
            DataTable dataTable = (DataTable)dgvDownloadList.DataSource;
            DataView dataView = dataTable.DefaultView;
            dataView.RowFilter = string.Format("[Request Number] like '{0}%'", txtRequestNo.Text);
            /* ------------------------------------------------------------
               REMOVE DOWNLOAD BUTTON
            //------------------------------------------------------------ */
            foreach (DataGridViewRow gridRow in dgvDownloadList.Rows)
            {
                DataGridViewCell BookButtonCell = gridRow.Cells["Status"];
                DataGridViewButtonCell EndDateCell = (DataGridViewButtonCell)gridRow.Cells["btnDownload"];

                //if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "NOT AVAILABLE" || Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "SUBMITTED")
                //{
                //    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                //    gridRow.Cells["btnDownload"] = new DataGridViewTextBoxCell();
                //}

                if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() != "AVAILABLE")
                {
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    gridRow.Cells["btnDownload"] = new DataGridViewTextBoxCell();
                }


            }
        }

        #endregion

        #region lnkLogOff_Click
        private void lnkLogOff_Click(object sender, EventArgs e)
        {
            ShowHideLoginDetails(enmRequestType.LogOff);
        }
        #endregion

        #region InitializeCaptcha
        //private void InitializeCaptcha()
        //{//--
        //    if (TDSMAN.Classes.TDSMAN.T_BROWSER_LOGIN_TRACES == true)
        //        return;
        //    //--
        //    //--
        //    if (TdsMan.T_CheckInternetConnectivty() == false)
        //    {
        //        picCaptcha.Image = Properties.Resources.captcha_loading_failed;
        //        cmnService.J_UserMessage("Internet Connectivity not found");
        //        return;
        //    }
        //    try
        //    {
        //        //----------------------------------------------------
        //        objAccount = new TracesConnect();
        //        Stream imgStream = objAccount.MakeInitialRequest();
        //        Image img = Image.FromStream(imgStream);
        //        this.picCaptcha.Image = img;
        //        //-------------------------------------------------------
        //        txtCaptchaCode.Text = "";
        //    }
        //    catch(Exception err)
        //    {
        //        txtCaptchaCode.Text = "";
        //        picCaptcha.Image = Properties.Resources.captcha_loading_failed;
        //    }
        //}
        #endregion


        #region InitializeCaptcha
        private void InitializeCaptcha()
        {
            if (TDSMAN.Classes.TDSMAN.T_BROWSER_LOGIN_TRACES == true)
                return;

            if (!TdsMan.T_CheckInternetConnectivty())
            {
                picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                cmnService.J_UserMessage("Internet Connectivity not found");
                return;
            }

            Task.Run(() =>
            {
                try
                {
                    //--
                    if (objAccount == null)
                    {
                        PreWarmConnection();
                    }
                    //--
                    objAccount = new TracesConnect();  // Your class
                    //Stream imgStream = objAccount.MakeInitialRequest(); // Triggers login.xhtml + captcha fetch
                    //Image captchaImage = Image.FromStream(imgStream);

                    var captcha = objAccount.MakeInitialRequest_NEW();
                    this.CurrentCaptchaId = captcha.CaptchaId;
                    Image captchaImage = captcha.CaptchaImage;

                    this.Invoke(new Action(() =>
                    {
                        picCaptcha.Image = captchaImage;
                        txtCaptchaCode.Text = "";
                    }));
                }
                catch (Exception)
                {
                    this.Invoke(new Action(() =>
                    {
                        picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                        txtCaptchaCode.Text = "";
                    }));
                }
            });
        }
        #endregion

        private void PreWarmConnection()
        {
            try
            {
                // Make a simple HEAD request to establish connection
                var preWarmRequest = (HttpWebRequest)HttpWebRequest.Create("https://www.tdscpc.gov.in");
                preWarmRequest.Method = "HEAD";
                preWarmRequest.Timeout = 10000; // 10 seconds
                preWarmRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";

                using (var response = (HttpWebResponse)preWarmRequest.GetResponse())
                {
                    // Connection is now established and cached
                }
            }
            catch
            {
                // Ignore pre-warm failures
            }
        }

        #region ValidateFields
        bool ValidateFields()
        {
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

            if (TDSMAN.Classes.TDSMAN.T_BROWSER_LOGIN_TRACES == false)
            {
                if (string.IsNullOrEmpty(txtCaptchaCode.Text))
                {
                    cmnService.J_UserMessage("Please enter Captcha Code");
                    txtCaptchaCode.Focus();
                    return false;
                }
            }

            return true;
        }

        #endregion               

        #region PopulateDatagridView
        void PopulateDatagridView(DataTable dsRecords)
        {
            //MessageBox.Show("3.1");
            // DATA BIND TO GRIDVIEW CONTROL
            dgvDownloadList.Columns.Clear();
            //MessageBox.Show("3.2");

            //MessageBox.Show("3.3");
            dgvDownloadList.DataSource = null;
            //MessageBox.Show("3.4");
            dgvDownloadList.DataSource = dsRecords;
            //MessageBox.Show("3.5");
            dgvDownloadList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            //MessageBox.Show("3.6");

            // CREATE DOWNLOAD BUTTON & PROGRESSBAR CONTROL COLUMNS 
            //------------------------------------------------------------
            //-- REQUEST DATE
            dgvDownloadList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDownloadList.Columns[0].Width = 80;
            dgvDownloadList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //-- REQUEST NUMBER
            dgvDownloadList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDownloadList.Columns[1].Width = 70;
            dgvDownloadList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //-- FINANCIAL YEAR
            dgvDownloadList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDownloadList.Columns[2].HeaderText = "Financial Year";
            dgvDownloadList.Columns[2].Width = 80;
            dgvDownloadList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //-- QUARTER
            dgvDownloadList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDownloadList.Columns[3].Width = 50;
            dgvDownloadList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //-- FORM TYPE
            dgvDownloadList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDownloadList.Columns[4].Width = 80;
            dgvDownloadList.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //-- FILE PROCESSED
            dgvDownloadList.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDownloadList.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //-- STATUS
            dgvDownloadList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDownloadList.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //-- REMARKS
            dgvDownloadList.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDownloadList.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //-- DOWNLOAD
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dgvDownloadList.Columns.Add(btn);
            btn.HeaderText = "";
            btn.Text = "Download";
            btn.Name = "btnDownload";
            //------------------------------------------------------------
            btn.UseColumnTextForButtonValue = true;
            //------------------------------------------------------------
            //-- PROGRESS BAR
            DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            dgvDownloadList.Columns.Add(prg);
            prg.Name = "Progressbar";
            prg.ProgressBarColor = Color.LightGreen;
            /* ------------------------------------------------------------
               REMOVE DOWNLOAD BUTTON
            //------------------------------------------------------------ */
            //MessageBox.Show("3.8");
            foreach (DataGridViewRow gridRow in dgvDownloadList.Rows)
            {
                DataGridViewCell BookButtonCell = gridRow.Cells["Status"];
                DataGridViewButtonCell EndDateCell = (DataGridViewButtonCell)gridRow.Cells["btnDownload"];

                //COMMENTED BY ARUP ON 14-04-2014
                //if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "NOT AVAILABLE" || Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "SUBMITTED")
                //{
                //    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                //    gridRow.Cells["btnDownload"] = new DataGridViewTextBoxCell();
                //    //gridRow.Cells["btnDownload"].Value = "........oooops";
                //}


                if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() != "AVAILABLE")
                {
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    gridRow.Cells["btnDownload"] = new DataGridViewTextBoxCell();
                    //gridRow.Cells["btnDownload"].Value = "........oooops";
                }


            }
            //MessageBox.Show("3.9");
            //------------------------------------------------------------
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
                    txtTANNo.Text = "";
                    grpDownloadList.Visible = false;
                    grpLoginDetails.Visible = true;
                    grpProgress.Visible = true;
                    BtnSave.Enabled = true;
                    InitializeCaptcha();
                    break;
                case enmRequestType.DownloadList:
                    txtRequestNo.Text = "";
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                    grpProgress.Visible = false;
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;
                    break;

                case enmRequestType.LogOff:
                    ArrayList objList = new ArrayList();
                    objList.Add(enmRequestType.LogOff);
                    //-------------------------------------------
                    pgTimer.Start();
                    //-------------------------------------------
                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync(objList);

                    break;

            }
        }
        #endregion

        
        //----------------------------------------------------------------
        //ADDED BY DHRUB ON 02/01/2014 FOR UNZIP THE DOWNLOADED ZIP FILE 
        //----------------------------------------------------------------

        #region Decompress
        void Decompress(FileInfo fileToDecompress)
        {
            //using (FileStream originalFileStream = fileToDecompress.OpenRead())
            //{
            //    string currentFileName = fileToDecompress.FullName;
            //    string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

            //    using (FileStream decompressedFileStream = File.Create(newFileName))
            //    {
            //        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
            //        {
            //            decompressionStream.Equals(decompressedFileStream);
            //            Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
            //        }
            //    }

            //}
        }
        #endregion 

        #region CreatePassword
        bool CreatePassword(string strOutputFileType)
        {
            strPassWord = "";
            string strPathAndFile = "";
            intFaYearID =0;
            strFileName = TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces;
            //strFileName = strFileName.Substring(strFileName.LastIndexOf("/") + 1, (strFileName.Length - 1) - (strFileName.LastIndexOf("/"))); 
            try
            {
                switch (strOutputFileType)
                {
                    case T_DownloadedOutputFileType.NSDLConsoFile:
                        //GENERATE PASSWORD CALN00637A_5207168
                        strPassWord = txtTANNo.Text + "_" + strRequestNo;
                        //----------------------------
                        //CALN00637A_201213_26Q_Q4
                        //strFileName = txtTANNo.Text + "_" + strFaYear.ToString().Replace("-", "") + "_" + strFormNo.Substring(0, 3) + "_" + strQuarter + ".zip";

                        //-- ANIK @ 2014-01-27
                        //strFileName = txtTANNo.Text + "_" + strFaYear.ToString().Replace("-", "") + "_" + strFormNo + "_" + strQuarter + ".zip";
                        strPathAndFile = strExtractPath + "\\" + strFileName;
                        break;
                    case T_DownloadedOutputFileType.Form16A:
                        //GENERATE PASSWORD 
                        strPassWord = txtTANNo.Text;
                        //----------------------------
                        //strFileName = txtTANNo.Text.Substring(0, 3) + "XXXXX" + txtTANNo.Text.Substring(8, 2) + "_FORM16A" + "_" + strFaYear + "_" + strQuarter + "_" + strRequestNo + ".zip";
                        strPathAndFile = strExtractPath + "\\" + strFileName;
                        break;
                    case T_DownloadedOutputFileType.BulkForm16AFile:
                        //GENERATE PASSWORD 
                        strPassWord = txtTANNo.Text;
                        //----------------------------
                        //strFileName = txtTANNo.Text.Substring(0, 3) + "XXXXX" + txtTANNo.Text.Substring(8, 2) + "_FORM16A" + "_" + strFaYear + "_" + strQuarter + "_" + strRequestNo + ".zip";
                        strPathAndFile = strExtractPath + "\\" + strFileName;
                        break;
                    case T_DownloadedOutputFileType.JustificationReport:

                        //GENERATE PASSWORD 
                        //JR_CALN00637A_26Q_Q1_2012-13

                        strPassWord = "JR_" + txtTANNo.Text + "_" + strFormNo + "_" + strQuarter + "_" + strFaYear;
                        //----------------------------
                        //CALXXXXX7A_26Q_Q1_2012-13
                        //strFileName = txtTANNo.Text.Substring(0, 3) + "XXXXX" + txtTANNo.Text.Substring(8, 2) + "_" + strFormNo.Substring(0, 3) + "_" + strQuarter + "_" + strFaYear.ToString() + ".zip";
                        //-- ANIK @ 2014-01-27
                        strSQL = "SELECT ASST_ID FROM MST_ASSESSMENT WHERE FA_YEAR='" + strFaYear + "'";
                        intFaYearID = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));
                        
                        //if (intFaYearID > Convert.ToInt32(T_FinancialYearID.F2010_11ID ))
                        //    strFileName = txtTANNo.Text.Substring(0, 3) + "XXXXX" + txtTANNo.Text.Substring(8, 2) + "_" + strFormNo + "_" + strQuarter + "_" + strFaYear.ToString() + ".zip";
                        //else
                        //    strFileName = txtTANNo.Text + "_" + strFormNo + "_" + strQuarter + "_" + strFaYear.Replace("-","") + ".zip";

                        strPathAndFile = strExtractPath + "\\" + strFileName;
                        break;
                    case T_DownloadedOutputFileType.TANPANFile:
                        //GENERATE PASSWORD
                        strPassWord = "";
                        //----------------------------
                        //-- ANIK @ 2014-01-27
                        //TAN-PAN_CALP08143C_2012-13_9155733.zip
                        strFileName = "TAN-PAN_" + txtTANNo.Text.Trim() + "_" + strFaYear.ToString() + "_" + strRequestNo + ".zip";
                        strPathAndFile = strExtractPath + "\\" + strFileName;
                        break;
                }
                //---FOR Multiple Downloades File Exist or not checking
                //foreach (FileInfo fileToDecompress in directorySelected.GetFiles(strFileName))
                //{
                if (File.Exists(strPathAndFile) == true)
                {
                    if (UnZipFiles(strPathAndFile, strExtractPath, strPassWord, false) == true)
                        return true;
                    else
                        return false;
                }
                else 
                    return false;                  
            }
            catch (Exception err)
            {
                return false;
               // cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion 

        #region UnZipFiles
        bool UnZipFiles(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
        {
            try
            {
                ZipInputStream s = new ZipInputStream(File.OpenRead(zipPathAndFile));
                if (password != null && password != String.Empty)
                    s.Password = password;
                ZipEntry theEntry;
                string tmpEntry = String.Empty;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = outputFolder;
                    string fileName = Path.GetFileName(theEntry.Name);
                    // create directory
                    if (directoryName != "")
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    if (fileName != String.Empty)
                    {
                        if (theEntry.Name.IndexOf(".ini") < 0)
                        {
                            string fullPath = directoryName + "\\" + theEntry.Name;
                            fullPath = fullPath.Replace("\\ ", "\\");
                            string fullDirPath = Path.GetDirectoryName(fullPath);
                            if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);
                            FileStream streamWriter = File.Create(fullPath);
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            streamWriter.Close();
                            streamWriter.Dispose();
                        }
                    }
                }
                s.Close();
                s.Dispose();
                theEntry = null;
                if (deleteZipFile)
                    File.Delete(zipPathAndFile);

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion 

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=BJmS8CBNMGs");
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0006", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0055", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region bgWorkerLoadCaptcha_DoWork
        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }
        #endregion

        #region BgWorkerCertificateExtraction_DoWork
        private void BgWorkerCertificateExtraction_DoWork(object sender, DoWorkEventArgs e)
        {
            string strBatFile = "", strJAVAFile = "", strworkingFolder = "";
            try
            {
                //-- 2024/04/19
                if (strExtractionType == enmExtractionType.Form16_16A.ToString())
                {
                    strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorBundle\\RUN_PDF_GEN.BAT");
                    strJAVAFile = " PDFGeneratorCLI "; strworkingFolder = "PDFGeneratorBundle";
                    strExtractionFolder = txtTANNo.Text + "_" + dgvDownloadList.Rows[intRowIndex].Cells[2].Value.ToString() + "_" + dgvDownloadList.Rows[intRowIndex].Cells[4].Value.ToString() + "_" + dgvDownloadList.Rows[intRowIndex].Cells[3].Value.ToString() + "_" + TdsMan.J_ReturnServerDateTimeMMDDYYYYHHMMSS().ToString().Replace("/", "").Replace(":", "");
                    //
                    UpdateLabel(intNoOfCertificates, Path.Combine(strExtractPath, strExtractionFolder));
                    //this.Cursor = Cursors.WaitCursor; 
                    //blFilesExtracted = TdsMan.ExtractCertificates(strBatFile, strJAVAFile, strworkingFolder, Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), txtTANNo.Text, Path.Combine(strExtractPath, strExtractionFolder));
                    if (rbnGenCert.Checked == true)
                        blFilesExtracted = TdsMan.ExtractCertificatesSignedDigitally(strBatFile, strJAVAFile, strworkingFolder, Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), txtTANNo.Text, Path.Combine(strExtractPath, strExtractionFolder),intNoOfCertificates, intTimeInterval, false);
                    else if(rbnGenCertDSC.Checked == true)
                        blFilesExtracted = TdsMan.ExtractCertificatesSignedDigitally(strBatFile, strJAVAFile, strworkingFolder, Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), txtTANNo.Text, Path.Combine(strExtractPath, strExtractionFolder), intNoOfCertificates, intTimeInterval, true);
                    //this.Cursor = Cursors.Default;            
                }
                else if (strExtractionType == enmExtractionType.Form27D.ToString()) //-- 2025/05/06
                {
                    strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorBundle27D\\RUN_PDF_GEN.BAT");
                    strJAVAFile = " PDFGeneratorCLI "; strworkingFolder = "PDFGeneratorBundle27D";
                    strExtractionFolder = txtTANNo.Text + "_" + dgvDownloadList.Rows[intRowIndex].Cells[2].Value.ToString() + "_" + dgvDownloadList.Rows[intRowIndex].Cells[4].Value.ToString() + "_" + dgvDownloadList.Rows[intRowIndex].Cells[3].Value.ToString() + "_" + TdsMan.J_ReturnServerDateTimeMMDDYYYYHHMMSS().ToString().Replace("/", "").Replace(":", "");
                    //
                    UpdateLabel(intNoOfCertificates, Path.Combine(strExtractPath, strExtractionFolder));
                    //this.Cursor = Cursors.WaitCursor;
                    //blFilesExtracted = TdsMan.ExtractCertificates(strBatFile, strJAVAFile, strworkingFolder, Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), txtTANNo.Text, Path.Combine(strExtractPath, strExtractionFolder));
                    if (rbnGenCert.Checked == true)
                        blFilesExtracted = TdsMan.ExtractCertificatesSignedDigitally(strBatFile, strJAVAFile, strworkingFolder, Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), txtTANNo.Text, Path.Combine(strExtractPath, strExtractionFolder), intNoOfCertificates, intTimeInterval, false);
                    else if (rbnGenCertDSC.Checked == true)
                        blFilesExtracted = TdsMan.ExtractCertificatesSignedDigitally(strBatFile, strJAVAFile, strworkingFolder, Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), txtTANNo.Text, Path.Combine(strExtractPath, strExtractionFolder), intNoOfCertificates, intTimeInterval, true);

                    //this.Cursor = Cursors.Default;            
                }
                else if (strExtractionType == enmExtractionType.Form16_PartB.ToString())
                {

                    strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorPartBBundle\\RUN_PDF_GEN.BAT");
                    strJAVAFile = " PDFPartBGeneratorCLI "; strworkingFolder = "PDFGeneratorPartBBundle";
                    strExtractionFolder = txtTANNo.Text + "_" + dgvDownloadList.Rows[intRowIndex].Cells[2].Value.ToString() + "_" + dgvDownloadList.Rows[intRowIndex].Cells[4].Value.ToString() + "_" + dgvDownloadList.Rows[intRowIndex].Cells[3].Value.ToString() + "_" + TdsMan.J_ReturnServerDateTimeMMDDYYYYHHMMSS().ToString().Replace("/", "").Replace(":", "");
                    //
                    UpdateLabel(intNoOfCertificates, Path.Combine(strExtractPath, strExtractionFolder));
                    //this.Cursor = Cursors.WaitCursor;
                    //blFilesExtracted = TdsMan.ExtractCertificates(strBatFile, strJAVAFile, strworkingFolder, Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), txtTANNo.Text, Path.Combine(strExtractPath, strExtractionFolder));
                    if (rbnGenCert.Checked == true)
                        blFilesExtracted = TdsMan.ExtractPartBCertificatesSignedDigitally(strBatFile, strJAVAFile, strworkingFolder, Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), txtTANNo.Text, Path.Combine(strExtractPath, strExtractionFolder), intNoOfCertificates, intTimeInterval, false);
                    else if (rbnGenCertDSC.Checked == true)
                        blFilesExtracted = TdsMan.ExtractPartBCertificatesSignedDigitally(strBatFile, strJAVAFile, strworkingFolder, Path.Combine(strExtractPath, TDSMAN.Classes.TDSMAN.T_DownloadedFileNameFromTraces), txtTANNo.Text, Path.Combine(strExtractPath, strExtractionFolder), intNoOfCertificates, intTimeInterval, true);
                    //this.Cursor = Cursors.Default;
                }
                //else if (strExtractionType == enmExtractionType.Signing.ToString())
                //{
                //    blFilesSigned=TdsMan.SignCertificates(Path.Combine(strExtractPath, strExtractionFolder));
                //}
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region BgWorkerCertificateExtraction_RunWorkerCompleted
        private void BgWorkerCertificateExtraction_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string[] strpdfFiles = Directory.GetFiles(Path.Combine(strExtractPath, strExtractionFolder), "*.pdf", SearchOption.TopDirectoryOnly);
            //if(strpdfFiles.Length <= 0)
            //{
            //    if (!bgWorkerCertificateExtraction.IsBusy)
            //        bgWorkerCertificateExtraction.RunWorkerAsync();
            //}

            this.Cursor = Cursors.Default;
            //if (strExtractionType == enmExtractionType.Form16_16A.ToString() ||
            //   strExtractionType == enmExtractionType.Form16_PartB.ToString() ||
            //   strExtractionType == enmExtractionType.Form27D.ToString())
            //{
            //    strExtractionType = enmExtractionType.Signing.ToString();

            //    this.Cursor = Cursors.WaitCursor;
            //    if (!bgWorkerCertificateExtraction.IsBusy)
            //        bgWorkerCertificateExtraction.RunWorkerAsync();
            //    //if (blFilesExtracted == true)
            //    //{
            //    //    strExtractionType = enmExtractionType.Signing.ToString();

            //    //    //System.Diagnostics.Process.Start(Path.Combine(strExtractPath, strExtractionFolder));
            //    //}
            //}
            //else if (strExtractionType == enmExtractionType.Signing.ToString())
            //{
            pgTimerExtraction.Stop();
            pgExtractionBar.Value = 100;
            lblCertExtractionText4.Visible = false;
            //--
            ////try
            ////{
            ////    ////TdsMan.DeleteAllFilesInFolder(Path.Combine(lnkCertExtractionText7.Text, "resources"));
            ////    ////if (Directory.Exists(Path.Combine(lnkCertExtractionText7.Text, "resources")))
            ////    ////    Directory.Delete(Path.Combine(lnkCertExtractionText7.Text, "resources"), true);
            ////    //////
            ////    ////TdsMan.DeleteAllFilesInFolder(Path.Combine(lnkCertExtractionText7.Text, "temp"));
            ////    ////if (Directory.Exists(Path.Combine(lnkCertExtractionText7.Text, "temp")))
            ////    ////    Directory.Delete(Path.Combine(lnkCertExtractionText7.Text, "temp"), true);
            ////}
            ////catch(Exception err)
            ////{

            ////}
            //finally (){ }
            //--
            //string[] strpdfFiles = Directory.GetFiles(Path.Combine(strExtractPath, strExtractionFolder), "*.pdf", SearchOption.TopDirectoryOnly);
            if (blFilesExtracted == true)
            {
                string[] strpdfFiles1 = Directory.GetFiles(Path.Combine(strExtractPath, strExtractionFolder), "*.pdf", SearchOption.TopDirectoryOnly);
                lblCertExtractionText5.Visible = true;
                lblCertExtractionText5.ForeColor = Color.Green;
                lblCertExtractionText5.Text = "Extraction completed.";
                lblCertExtractionText4.Visible = false;
                //lblCertExtractionText5.Text = strpdfFiles1.Length.ToString() + " nos. extracted";
                lnkCertExtractionText7.Font = new Font(lnkCertExtractionText7.Font.FontFamily, lnkCertExtractionText7.Font.Size, FontStyle.Bold);
                return;
            }
            else
            {
                //
                if(intNoOfCertificates < 15) // for less number of certificates...
                {
                    ////if (intNoOfCertificates < 2)
                    ////    Thread.Sleep(4000);
                    ////else
                        Thread.Sleep(2500);
                    //--
                    string[] strpdfFiles1 = Directory.GetFiles(Path.Combine(strExtractPath, strExtractionFolder), "*.pdf", SearchOption.TopDirectoryOnly);
                    if (intNoOfCertificates == strpdfFiles.Length)
                    {
                        lblCertExtractionText5.Visible = true;
                        lblCertExtractionText5.ForeColor = Color.Green;
                        lblCertExtractionText5.Text = "Extraction completed.";
                        lblCertExtractionText4.Visible = false;
                        //lblCertExtractionText5.Text = strpdfFiles1.Length.ToString() + " nos. extracted";
                        lnkCertExtractionText7.Font = new Font(lnkCertExtractionText7.Font.FontFamily, lnkCertExtractionText7.Font.Size, FontStyle.Bold);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                //
                ////lblCertExtractionText5.Visible = true;
                ////lblCertExtractionText5.ForeColor = Color.Red;
                ////lblCertExtractionText5.Text = "Extraction failed";
            }
            this.Cursor = Cursors.Default;
            //if (blFilesExtracted == true)
            //System.Diagnostics.Process.Start(Path.Combine(strExtractPath, strExtractionFolder));
            //}
        }
        #endregion

        #region BtnCancelExtraction_Click
        private void BtnCancelExtraction_Click(object sender, EventArgs e)
        {
            grpCertExtraction.Visible = false;
        }
        #endregion


        #region BtnProceedExtraction_Click
        private void BtnProceedExtraction_Click(object sender, EventArgs e)
        {
            try
            {
                //--
                if(rbnGenCert.Checked==false && rbnGenCertDSC.Checked == false)
                {
                    cmnService.J_UserMessage("Select any one option");
                    rbnGenCert.Focus();
                    return;
                }
                //--
                ArrayList objList = new ArrayList();
                if (dgvDownloadList.Rows[intRowIndex].Cells[7].Value.ToString() == "PartB")
                    strExtractionType = enmExtractionType.Form16_PartB.ToString();
                else if (dgvDownloadList.Rows[intRowIndex].Cells[5].Value.ToString().Contains("27D") == true)
                    strExtractionType = enmExtractionType.Form27D.ToString();
                else
                    strExtractionType = enmExtractionType.Form16_16A.ToString();
                //--
                tbcCertificateExtraction.SelectTab(tbpCertExtraction2);
                btnProceedExtraction.Visible = false;
                btnCancelExtraction.Text = "Close";
                pgExtractionBar.Value = 0;
                intProgressValueExtraction = 0;
                //
                if (intNoOfCertificates < 15)
                {
                    pgTimerExtraction.Interval = 15000;// 5000;// 5sec
                }
                else
                {
                    pgTimerExtraction.Interval = 2000;// 2sec
                }
                //
                pgTimerExtraction.Start();
                //--
                this.Cursor = Cursors.WaitCursor;
                if (!bgWorkerCertificateExtraction.IsBusy)
                    bgWorkerCertificateExtraction.RunWorkerAsync(objList);
            }
            catch (Exception err)
            {
                
            }
        }
        #endregion


        #region BgWorkerCertificateExtraction_ProgressChanged
        private void BgWorkerCertificateExtraction_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if ((intNoOfCertificates * 80) / 60 < 60)
            //    lblCertExtractionText4.Text = "The process will take appx. less than a minute";
            //else if (((intNoOfCertificates * 80) / 60) / 60 < 60)
            //    lblCertExtractionText4.Text = "The process will take appx. less than " + Math.Round(cmnService.J_ReturnDoubleValue((((intNoOfCertificates * 80) / 60) / 60) + 1)).ToString() + " minutes";
            //else
            //    lblCertExtractionText4.Text = "The process will take appx. less than " + Math.Round(cmnService.J_ReturnDoubleValue(((((intNoOfCertificates * 80) / 60) / 60) / 60) + 1)).ToString() + " hour(s)";
            ////
            //lblCertExtractionText6.Text = "Please check the folder: " + Path.Combine(strExtractPath, strExtractionFolder);
            //
            //pgExtractionBar.Value = e.ProgressPercentage;
        }
        #endregion


        #region PgTimerExtraction_Tick
        private void PgTimerExtraction_Tick(object sender, EventArgs e)
        {
            //if (intProgressValueExtraction < 90)   // Stop at 95% until work completes
            //{
            //    intProgressValueExtraction++;
            //    pgExtractionBar.Value = intProgressValueExtraction;
            //}
            if(pgTimerExtraction.Interval == 15000)// 5000)
                pgTimerExtraction.Interval = 10000;
            //--
            if (lnkCertExtractionText7.Text != "")
            {
                lblCertExtractionText5.Visible = true;
                intProgressValueExtraction = Directory.GetFiles(lnkCertExtractionText7.Text, "*.pdf").Length;
                if (rbnGenCert.Checked == true)
                    lblCertExtractionText5.Text = intProgressValueExtraction.ToString() + " Certificates generated...";
                else if (rbnGenCertDSC.Checked == true)
                    lblCertExtractionText5.Text = intProgressValueExtraction.ToString() + " (Digitally Signed) Certificates generated...";
            }
            //--
            ////if (intProgressValueExtraction < intNoOfCertificates)   // Stop at 95% until work completes
            ////{
            ////    //intProgressValueExtraction
            ////    //intProgressValueExtraction++;
            ////    pgExtractionBar.Value = intProgressValueExtraction;
            ////}
            //////if (intProgressValueExtraction < intNoOfCertificates)
            //////{
            //////    if (intProgressValueExtraction >= pgExtractionBar.Minimum && intProgressValueExtraction <= pgExtractionBar.Maximum)
            //////    {
            //////        pgExtractionBar.Value = intProgressValueExtraction;
            //////    }
            //////    else
            //////    {
            //////        pgExtractionBar.Value = pgExtractionBar.Maximum; // Prevent exceeding maximum
            //////    }
            //////}
            int percentComplete = (int)((double)intProgressValueExtraction / intNoOfCertificates * pgExtractionBar.Maximum);
            pgExtractionBar.Value = Math.Min(percentComplete, pgExtractionBar.Maximum);
            //--
            string[] strpdfFiles = Directory.GetFiles(Path.Combine(strExtractPath, strExtractionFolder), "*.pdf", SearchOption.TopDirectoryOnly);
            if (intNoOfCertificates == strpdfFiles.Length)
            {
                lblCertExtractionText5.ForeColor = Color.Green;
                lblCertExtractionText5.Text = "Extraction completed.";
                lblCertExtractionText4.Visible = false;
                lnkCertExtractionText7.Font = new Font(lnkCertExtractionText7.Font.FontFamily, lnkCertExtractionText7.Font.Size, FontStyle.Bold);
                this.Cursor = Cursors.Default;
                return;
            }
        }
        #endregion

        #region RbnGenCert_CheckedChanged
        private void RbnGenCert_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnGenCert.Checked == true)
            {
                lblCertExtractionText3.Visible = false;
                intTimeInterval = 8;
            }
            else if (rbnGenCertDSC.Checked == true)
            {
                lblCertExtractionText3.Visible = true;
                intTimeInterval = 80;
            }
        }
        #endregion

        #region LnkCertExtractionText7_LinkClicked
        private void LnkCertExtractionText7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread.Sleep(1500);
                //--
                TdsMan.DeleteAllFilesInFolder(Path.Combine(lnkCertExtractionText7.Text, "resources"));
                if (Directory.Exists(Path.Combine(lnkCertExtractionText7.Text, "resources")))
                    Directory.Delete(Path.Combine(lnkCertExtractionText7.Text, "resources"), true);
                //
                TdsMan.DeleteAllFilesInFolder(Path.Combine(lnkCertExtractionText7.Text, "temp"));
                if (Directory.Exists(Path.Combine(lnkCertExtractionText7.Text, "temp")))
                    Directory.Delete(Path.Combine(lnkCertExtractionText7.Text, "temp"), true);
            }
            catch (Exception err)
            {

            }
            System.Diagnostics.Process.Start(lnkCertExtractionText7.Text);
        }

        #endregion

        #region ReturnDSC
        private global2.PDFSign ReturnDSC(string strPFXFilePath, string strPassword)
        {
            try
            {
                global2.PDFSign PDFSign = new global2.PDFSign("serial number");
                //
                if (strPFXFilePath != "")
                {
                    PDFSign.DigitalSignatureCertificate = PDFSign.LoadCertificate(File.ReadAllBytes(strPFXFilePath), strPassword);
                    //
                    if (PDFSign.DigitalSignatureCertificate == null)
                    {
                        //cmnService.J_UserMessage("No Digital Signature found !!");
                        return null;
                    }
                    //
                }
                else
                {
                    //load the certificate from Microsoft Store
                    PDFSign.DigitalSignatureCertificate = PDFSign.LoadCertificate(false, "", "Digital certificates", "Select the digital certificate", global2.DigitalCertificateScope.ForDigitalSignature);
                    //PDFSign.DigitalSignatureCertificate = PDFSign.LoadCertificate(false, DigitalCertificateSearchCriteria.EmailE, "test@test.com", DigitalCertificateScope.ForDigitalSignature);
                    if (PDFSign.DigitalSignatureCertificate == null)
                    {
                        //cmnService.J_UserMessage("No Digital Signature found in Windows Store !!");
                        return null;
                    }
                }
                //
                return PDFSign;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return null;
            }
        }
        #endregion

        #region UpdateLabel
        //private void UpdateLabel(int intNoOfCertificates, string strExtractionPath)
        //{
        //    try
        //    {
        //        lblCertExtractionText4.Visible = true;
        //        if ((intNoOfCertificates * intTimeInterval ) / 60 < 60)
        //            lblCertExtractionText4.Text = "The process will take appx. a minute";
        //        else if (((intNoOfCertificates * intTimeInterval ) / 60) / 60 < 60)
        //            lblCertExtractionText4.Text = "The process will take appx. " + Math.Round(cmnService.J_ReturnDoubleValue((((intNoOfCertificates * intTimeInterval ) / 60) / 60) + 1)).ToString() + " minute(s)";
        //        else
        //            lblCertExtractionText4.Text = "The process will take appx. " + Math.Round(cmnService.J_ReturnDoubleValue(((((intNoOfCertificates * intTimeInterval ) / 60) / 60) / 60) + 1)).ToString() + " hour(s)";
        //        //
        //        //lblCertExtractionText6.Text = "Please check the folder: " + strExtractionPath;
        //        lnkCertExtractionText7.Text = strExtractionPath;
        //        //
        //    }
        //    catch (Exception ERR)
        //    {
        //        cmnService.J_UserMessage("UpdateLabel " + ERR.Message);
        //    }
        //}
        #endregion

        private void UpdateLabel(int numberOfCertificates, string extractionPath)
        {
            bool isSigned = false;
            try
            {
                if(rbnGenCertDSC.Checked == true)
                    isSigned = true;
                //
                lblCertExtractionText4.Visible = true;

                // Approximate time per certificate in seconds
                double timePerCertificate = isSigned ? 1.15 : 0.29;  // From your observation

                // Total time in seconds
                double totalTimeSeconds = numberOfCertificates * timePerCertificate;

                string estimate;
                ////if (totalTimeSeconds < 60)
                ////{
                ////    estimate = "The process will take approximately a few seconds";
                ////}
                ////else 
                if (totalTimeSeconds < 3600)
                {
                    double minutes = Math.Ceiling(totalTimeSeconds / 60);
                    estimate = $"This process will take approximately {minutes} minute(s)";
                }
                else
                {
                    double hours = Math.Ceiling(totalTimeSeconds / 3600);
                    estimate = $"This process will take approximately {hours} hour(s)";
                }

                lblCertExtractionText4.Text = estimate;
                lnkCertExtractionText7.Text = extractionPath;
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage("Error in UpdateExtractionTimeEstimate: " + ex.Message);
            }
        }

        //----------------------------------------------------------------


    }
}

