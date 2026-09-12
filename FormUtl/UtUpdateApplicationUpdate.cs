using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;

using TDSMAN.Classes;


namespace TDSMAN.FormUtl
{
    public partial class UtUpdateApplicationUpdate : Form
    {
        #region Constructor
        public UtUpdateApplicationUpdate()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects and Variables

        CommonService cmnservice = new CommonService();
        
        string J_pProjectName = "TDSMAN";
        string strExeName = "eLicensedUpdateApplication";
        bool blnsStatus = false;

        string UploadedLEXEPath = "";
        string UploadedLicensedEXEName = "";

        #endregion

        #region UtUpdateApplicationUpdate_Load
        private void UtUpdateApplicationUpdate_Load(object sender, EventArgs e)
        {
            UploadedLEXEPath = "http://tdsman.com/downloads/" + J_pProjectName + "_" + strExeName + "_Update.zip";
            UploadedLicensedEXEName = J_pProjectName + "_" + strExeName + "_Update.zip";

            bgwWorker.RunWorkerAsync();
        }
        #endregion

        #region bgwWorker_DoWork
        private void bgwWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // first, we need to get the exact size (in bytes) of the file we are downloading
                Uri url = new Uri(UploadedLEXEPath);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                response.Close();

                // gets the size of the file in bytes
                Int64 intSize = response.ContentLength;

                // keeps track of the total bytes downloaded so we can update the progress bar
                Int64 intRunningByteTotal = 0;

                // use the webclient object to download the file
                using (WebClient client = new WebClient())
                {
                    // open the file at the remote URL for reading
                    using (Stream streamRemote = client.OpenRead(new Uri(UploadedLEXEPath)))
                    {
                        // using the FileStream object, we can write the downloaded bytes to the file system
                        using (Stream streamLocal = new FileStream(Path.Combine(Application.StartupPath, UploadedLicensedEXEName), FileMode.Create, FileAccess.Write, FileShare.None))
                        {

                            // loop the stream and get the file into the byte buffer
                            int intByteSize = 0;

                            byte[] byteBuffer = new byte[intSize];

                            while ((intByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                            {
                                // write the bytes to the file system at the file path specified
                                streamLocal.Write(byteBuffer, 0, intByteSize);

                                if (bgwWorker.CancellationPending)//checks for cancel request
                                {
                                    break;
                                }

                                intRunningByteTotal += intByteSize;

                                // calculate the progress out of a base "100"
                                double dblIndex = (double)(intRunningByteTotal);
                                double dblTotal = (double)byteBuffer.Length;
                                double dblProgressPercentage = (dblIndex / dblTotal);
                                int intProgressPercentage = (int)(dblProgressPercentage * 100);

                                // update the progress bar
                                bgwWorker.ReportProgress(intProgressPercentage);
                            }
                            
                            blnsStatus = true;
                            
                            // clean up the file stream
                            streamLocal.Close();
                        }
                        // close the connection to the remote server
                        streamRemote.Close();
                    }
                }
            }
            catch(Exception err)
            {
                blnsStatus = false;
            }
        }
        #endregion 

        #region bgwWorker_ProgressChanged
        private void bgwWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgBar.Value = e.ProgressPercentage;
        }
        #endregion

        #region bgwWorker_RunWorkerCompleted
        private void bgwWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmnservice.J_UnZipBool(Path.Combine(Application.StartupPath, UploadedLicensedEXEName));

            File.Delete(Path.Combine(Application.StartupPath, UploadedLicensedEXEName));

            TDSMAN.Classes.TDSMAN.T_pDownloadStatus = blnsStatus;

            this.Dispose();
            this.Close();
        }
        #endregion

    }
}