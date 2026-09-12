
#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using System.IO;
using System.Net;
using System.Diagnostics;
using TDSMAN.Classes;

using Microsoft.Win32;

using ICSharpCode.SharpZipLib.Zip;

#endregion

namespace TDSMAN.FormSys
{
    
    public partial class SysFVUUpdate : Form
    {
        #region System Generated Code
        public SysFVUUpdate()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
        //
        DMLService dmlService = new DMLService();
        DateService dtService = new DateService();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        // WEB CLASS
        TDSMAN_WEB.Registration Registration = new TDSMAN_WEB.Registration();
        //-- 
        int intUpdated = 0;
        int intUpdateProcessFlag = 0;
        int intInactiveFlag = 0;
        //
        string strFVUVersion = "";
        string DownloadPath = "";
        //
        string strSQL = "";
        bool blnUpdateFVU_8_0 = false;
        #endregion

        #region User Defined Events

        #region SysFVUUpdate_Shown
        //private void SysUpdateApplication_Shown(object sender, EventArgs e)
        public void SysFVUUpdate_Shown(object sender, EventArgs e)
        {
            if (intUpdated > 0) return;
            //
            this.Refresh();
            //
            #region COMMENT
            //if (TdsMan.T_CheckInternetConnectivty() == true)
            //{
            //    this.Refresh();
            //    lblOnlineStatus.Text = "Checking for Update";
            //    prgBar.Value = prgBar.Value + 10;
            //    this.Refresh();
            //    //
            //    string[] strFinancialYearArray;
            //    int intFAyears;
            //    string strNewFVUVersion;
            //    string strFVUPath;

            //    string UploadedFVUPath;
            //    string UploadedFVUName;

            //    //COUNTING THE FINANCIAL YEAR IN MST_ASSESSMENT TABLE

            //    strSQL = "SELECT COUNT(*) FROM MST_ASSESSMENT";
            //    intFAyears = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

            //    //INITIALISING ARRAY
            //    strFinancialYearArray = new string[intFAyears];

            //    //TRANSFERING ALL FINANCIAL YEARS TO THE ARRAY
            //    strSQL = "SELECT FA_YEAR FROM MST_ASSESSMENT ORDER BY ASST_ID";
            //    IDataReader drdFVUVersionReader = null;

            //    drdFVUVersionReader = dmlService.J_ExecSqlReturnReader(strSQL);

            //    if (drdFVUVersionReader == null)
            //    {
            //        return;
            //    }
            //    int FYYear = 0;
            //    while (drdFVUVersionReader.Read())
            //    {
            //        strFinancialYearArray[FYYear] = drdFVUVersionReader["FA_YEAR"].ToString();
            //        FYYear++;
            //    }
            //    //--
            //    if (TdsMan.T_CheckServerTDSMAN() == false)
            //    {
            //        this.Refresh();
            //        lblOnlineStatus.Text = "www.tdsman.com server is down. Please try it later.";
            //        this.Refresh();
            //        cmnService.J_UserMessage("www.tdsman.com server is down. Please try it later.");
            //        this.Dispose();
            //        this.Close();
            //    }
            //    //----------------------------------------------------------------------------------
            //    /*
            //     * NOW 1ST CHECKING THE UPDATED FVU VERISON FOR THAT FINANCIAL YEAR
            //     * NEXT, CHECKING IF THE UPDATED FVU VERSION FILE EXIST IN APPLICATION FOLDER
            //     * IF IT DOES NOT EXISTS THEN DOWNLOADING THE SAME FROM WEBSITE
            //    */
            //    //----------------------------------------------------------------------------------
            //    for (int i = 0; i < strFinancialYearArray.Length; i++)
            //    {
            //        this.Refresh();
            //        lblOnlineStatus.Text = "Checking for Update";
            //        prgBar.Value = prgBar.Value + 5;
            //        this.Refresh();
            //        //
            //        strNewFVUVersion = Registration.Get_Latest_FVU_Version(strFinancialYearArray[i]);
            //        strFVUPath = "TDS_FVU_" + strNewFVUVersion + "\\TDS_FVU_STANDALONE.jar";
            //        //
            //        if (cmnService.J_IsFileExist(Path.Combine(Application.StartupPath, strFVUPath)) == false)
            //        {
            //            lblOnlineStatus.Text = "Downloading Update";
            //            this.Refresh();
            //            //
            //            WebClient webclient = new WebClient();
            //            //
            //            UploadedFVUPath = "http://tdsman.com/downloads/FVU_" + strNewFVUVersion + ".zip";
            //            //
            //            //-- ANIK @ 2015/09/29 
            //            //-- http://d3efm24pipr2ul.cloudfront.net/Downloads/FVU_4.8.zip
            //            //http://d2gvdvzamov71f.cloudfront.net/downloads/FVU_2.144.zip
            //            //UploadedFVUPath = "http://d2gvdvzamov71f.cloudfront.net/downloads/FVU_" + strNewFVUVersion + ".zip";
            //            UploadedFVUName = "FVU_" + strNewFVUVersion + ".zip";
            //            //
            //            webclient.DownloadFile(UploadedFVUPath, Path.Combine(Application.StartupPath, UploadedFVUName));
            //            //
            //            cmnService.J_UnZipBool(Path.Combine(Application.StartupPath, UploadedFVUName));

            //            File.Delete(Path.Combine(Application.StartupPath, UploadedFVUName));
            //            //
            //            intUpdateProcessFlag = 1;
            //        }
            //    }
            //    this.Refresh();
            //    for (int i = prgBar.Minimum; i <= prgBar.Maximum; i++)
            //    {
            //        prgBar.PerformStep();
            //    }
            //    //
            //    if (intUpdateProcessFlag > 0)
            //    {
            //        lblOnlineStatus.Text = "File Validation Utility Updated";
            //        this.Refresh();
            //        cmnService.J_UserMessage("File Validation Utility Updated");
            //    }
            //    else
            //    {
            //        lblOnlineStatus.Text = "Latest File Validation Utility already installed";
            //        this.Refresh();
            //        cmnService.J_UserMessage("Latest File Validation Utility already installed");
            //    }
            //    //
            //    this.Dispose();
            //    this.Close();
            //    //return;
            //}
            //intUpdated = 1;
            #endregion
            //
            DownloadPath = Path.Combine(Application.StartupPath, "UPDATE_DOWNLOAD" + "\\");
            // Create the subfolder.
            if (Directory.Exists(DownloadPath) == false)
                Directory.CreateDirectory(DownloadPath);
            //
            foreach (string filePath in Directory.GetFiles(@DownloadPath))
                File.Delete(filePath);
            //--
            bgwWorkerFVU.RunWorkerAsync();
            //
        }

        #endregion      

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
            //this.Dispose();
            if (BtnCancel.Text.ToUpper() == "CANCEL")
            {
                bgwWorkerFVU.CancelAsync();
                this.Cursor = Cursors.Default;
                lblOnlineStatus.Text = "Download cancelled ...";
            }
            else
            {
                this.Close();
                this.Dispose();
            }
        }
        #endregion



        #region bgwWorkerFVU_DoWork
        private void bgwWorkerFVU_DoWork(object sender, DoWorkEventArgs e)
        {
            //prgBar.Value = 0;
            //lblPrcnt.Text = "";
            DMLService dmlService = new DMLService();
            //if (intUpdated > 0) return;
            ////
            //this.Refresh();
            //
            //prgBar.Value = 0;
            //
            if (TdsMan.T_CheckInternetConnectivty() == true)
            {
                //this.Refresh();
                //lblOnlineStatus.Text = "Checking for Update";
                //prgBar.Value = prgBar.Value + 10;
                //this.Refresh();
                //
                string[] strFinancialYearArray;
                int intFAyears;
                string strNewFVUVersion;
                string strFVUPath;
                //string strFVUVersion;

                string UploadedFVUPath;
                string UploadedFVUName;

                //COUNTING THE FINANCIAL YEAR IN MST_ASSESSMENT TABLE

                strSQL = "SELECT COUNT(*) FROM MST_ASSESSMENT";
                intFAyears = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                //INITIALISING ARRAY
                strFinancialYearArray = new string[intFAyears];

                //TRANSFERING ALL FINANCIAL YEARS TO THE ARRAY
                strSQL = "SELECT FA_YEAR FROM MST_ASSESSMENT ORDER BY ASST_ID";
                IDataReader drdFVUVersionReader = null;

                drdFVUVersionReader = dmlService.J_ExecSqlReturnReader(strSQL);

                if (drdFVUVersionReader == null)
                {
                    return;
                }
                int FYYear = 0;
                while (drdFVUVersionReader.Read())
                {
                    strFinancialYearArray[FYYear] = drdFVUVersionReader["FA_YEAR"].ToString();
                    FYYear++;
                }
                //--
                if (TdsMan.T_CheckServerTDSMAN() == false)
                {
                    //this.Refresh();
                    //lblOnlineStatus.Text = "www.tdsman.com server is down. Please try it later.";
                    //this.Refresh();
                    cmnService.J_UserMessage("www.tdsman.com server is down. Please try it later.");
                    this.Dispose();
                    this.Close();
                }
                //----------------------------------------------------------------------------------
                /*
                 * NOW 1ST CHECKING THE UPDATED FVU VERISON FOR THAT FINANCIAL YEAR
                 * NEXT, CHECKING IF THE UPDATED FVU VERSION FILE EXIST IN APPLICATION FOLDER
                 * IF IT DOES NOT EXISTS THEN DOWNLOADING THE SAME FROM WEBSITE
                */
                //----------------------------------------------------------------------------------
                for (int i = 0; i < strFinancialYearArray.Length; i++)
                {
                    ////this.Refresh();
                    //lblOnlineStatus.Text = "Checking latest FVU";
                    //lblStatus.Invoke((Action)(() => lblStatus.Text = counter.ToString()));
                    //lblOnlineStatus.Invoke((Action)(( => lblOnlineStatus.Text = "Checking latest FVU")));
                    //
                    //prgBar.Value = prgBar.Value + 5;
                    //this.Refresh();
                    //
                    strNewFVUVersion = Registration.Get_Latest_FVU_Version(strFinancialYearArray[i]);
                    strFVUPath = "TDS_FVU_" + strNewFVUVersion + "\\TDS_FVU_STANDALONE.jar";
                    //-- 2023/01/24
                    blnUpdateFVU_8_0 = false;
                    if (strNewFVUVersion == "8.0")
                    {
                        string strFVU_8_0_Path = "TDS_FVU_" + strNewFVUVersion;
                        string strJavaFile = "bcprov-jdk15to18-172.jar";
                        //
                        if(Directory.Exists(Path.Combine(Application.StartupPath, strFVU_8_0_Path)) == true)
                        {
                            if (File.Exists(Path.Combine(Application.StartupPath, strFVU_8_0_Path + "\\" + strJavaFile)) == false)
                            {
                                blnUpdateFVU_8_0 = true;
                            }
                        }
                    }
                    //--
                    if (File.Exists(Path.Combine(Application.StartupPath, strFVUPath)) == false || blnUpdateFVU_8_0 == true)
                    {
                        strFVUVersion = strNewFVUVersion;
                        //WebClient webclient = new WebClient();
                        //UploadedFVUPath = "http://tdsman.com/downloads/FVU_" + strNewFVUVersion + ".zip";
                        //-- 2024/01/22
                        UploadedFVUPath = "https://swdwd.pdsinfotech.com/updates/FVU_" + strNewFVUVersion + ".zip";
                        //
                        Uri url = new Uri(UploadedFVUPath);

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                        response.Close();

                        Int64 intSize = response.ContentLength;

                        // keeps track of the total bytes downloaded so we can update the progress bar
                        Int64 intRunningByteTotal = 0;
                        //
                        //// use the webclient object to download the file
                        using (WebClient client = new WebClient())
                        {
                            // open the file at the remote URL for reading
                            using (Stream streamRemote = client.OpenRead(new Uri(UploadedFVUPath)))
                            {
                                // using the FileStream object, we can write the downloaded bytes to the file system
                                using (Stream streamLocal = new FileStream(Path.Combine(DownloadPath, "TDS_FVU_" + strNewFVUVersion), FileMode.Create, FileAccess.Write, FileShare.None))
                                {
                                    // loop the stream and get the file into the byte buffer
                                    int intByteSize = 0;

                                    byte[] byteBuffer = new byte[intSize];

                                    while ((intByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                                    {
                                        // write the bytes to the file system at the file path specified
                                        streamLocal.Write(byteBuffer, 0, intByteSize);
                                        //
                                        if (bgwWorkerFVU.CancellationPending)//checks for cancel request
                                        {
                                            break;
                                        }
                                        //
                                        intRunningByteTotal += intByteSize;
                                        // calculate the progress out of a base "100"
                                        double dblIndex = (double)(intRunningByteTotal);
                                        double dblTotal = (double)byteBuffer.Length;
                                        double dblProgressPercentage = (dblIndex / dblTotal);
                                        int intProgressPercentage = (int)(dblProgressPercentage * 100);

                                        //--    ANIK @ 2016/01/15
                                        if (intProgressPercentage > 100)
                                            intProgressPercentage = 100;
                                        // update the progress bar
                                        bgwWorkerFVU.ReportProgress(intProgressPercentage);
                                    }
                                    // clean up the file stream
                                    streamLocal.Close();
                                }
                                // close the connection to the remote server
                                streamRemote.Close();
                            }
                            //-- ANIK @ 2015/09/29 
                            //-- http://d3efm24pipr2ul.cloudfront.net/Downloads/FVU_4.8.zip
                            //http://d2gvdvzamov71f.cloudfront.net/downloads/FVU_2.144.zip
                            //UploadedFVUPath = "http://d2gvdvzamov71f.cloudfront.net/downloads/FVU_" + strNewFVUVersion + ".zip";
                            UploadedFVUName = "FVU_" + strNewFVUVersion + ".zip";
                            //
                            client.DownloadFile(UploadedFVUPath, Path.Combine(Application.StartupPath, UploadedFVUName));
                            //
                            cmnService.J_UnZipBool(Path.Combine(Application.StartupPath, UploadedFVUName));
                            //
                            File.Delete(Path.Combine(Application.StartupPath, UploadedFVUName));
                            //
                            intUpdateProcessFlag = 1;
                        }
                    }
                }
            }
            intUpdated = 1;
        }
        #endregion

        #region bgwWorkerFVU_ProgressChanged
        private void bgwWorkerFVU_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                if (strFVUVersion == "")
                    lblOnlineStatus.Text = "Downloading latest FVU...";
                else if(blnUpdateFVU_8_0 == true) //-- 2023/01/24
                    lblOnlineStatus.Text = "Re-updating " + strFVUVersion + "...";
                else
                    lblOnlineStatus.Text = "Downloading FVU " + strFVUVersion + "...";
                //
                lblPleaseWaitMessage1.Visible = true;
            }
            //--
            lblPrcnt.Text = e.ProgressPercentage.ToString() + "%";
            prgBar.Value = e.ProgressPercentage;
            //--
            if (e.ProgressPercentage == 100)
            {
                if (strFVUVersion == "")
                    lblOnlineStatus.Text = "Extracting latest FVU...";
                else
                    lblOnlineStatus.Text = "Extracting FVU " + strFVUVersion + "...";
            }
        }
        #endregion

        #region bgwWorkerFVU_RunWorkerCompleted
        private void bgwWorkerFVU_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //MessageBox.Show("Update cancelled.");
                cmnService.J_UserMessage("Update cancelled.");
                //
                BtnCancel.Text = "E&xit";
                prgBar.Value = 0;
                lblPrcnt.Text = "";
                return;
            }
            //--
            //Delete DOWNLOAD folder
            Directory.Delete(DownloadPath, true);
            //J_UnZipString(Path.Combine(Application.StartupPath, UploadedFVUName));
            ////
            //File.Delete(Path.Combine(Application.StartupPath, UploadedFVUName));
            //--
            if (intUpdateProcessFlag > 0)
            {
                lblOnlineStatus.Text = "File Validation Utility Updated";
                this.Refresh();
                cmnService.J_UserMessage("File Validation Utility Updated");
            }
            else
            {
                lblOnlineStatus.Text = "Latest File Validation Utility already installed";
                this.Refresh();
                cmnService.J_UserMessage("Latest File Validation Utility already installed");
            }
            //
            //if (File.Exists(Path.Combine(activeDir, J_pProjectName + ".EXE")) == true)
            //{
            this.Close();
            this.Dispose();
            //    //
            //    Process.Start(Path.Combine(activeDir, J_pProjectName + ".EXE"));
            //}
        }
        #endregion


        #endregion

        #region User Defined Functions
        

        #endregion
    }
}