
#region Referred Namespaces
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

#endregion

namespace TDSMAN.FormUtl
{
    public partial class UtUpdatePDFDlls : Form
    {
        #region Constructor
        public UtUpdatePDFDlls()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects and Variables

        CommonService cmnservice = new CommonService();

        string UploadedDLLPath;
        string UploadedDLLName;
        //
        string UploadedLEXEPath;
        string UploadedLicensedEXEName;
        
        #endregion

        #region UtUpdateApplicationUpdate_Load
        private void UtUpdateApplicationUpdate_Load(object sender, EventArgs e)
        {
            this.Refresh();

            //UploadedLEXEPath = "http://tdsman.com/downloads/tdsman-software-button13-14.zip";
            //UploadedLicensedEXEName = "tdsman-software-button13-14.zip";

            //UploadedDLLPath = "http://tdsman.com/downloads/itextsharp.dll";
            //UploadedDLLName = "itextsharp.dll";
            UploadedDLLPath = "http://tdsman.com/downloads/PDFSignDll.zip";
            UploadedDLLName = "PDFSignDll.dll";

            bgwItextsharpdll.RunWorkerAsync();

            //UploadedLEXEPath = "http://tdsman.com/downloads/TDSMANSoftwareDLL.zip";
            //UploadedLicensedEXEName = "TDSMANSoftwareDLL.zip";
            
            //if (File.Exists(Path.Combine(Application.StartupPath, "itextsharp.dll")) == false)
            //    bgwItextsharpdll.RunWorkerAsync();
            //else
            //    bgwWorker.RunWorkerAsync();
        }
        #endregion

        #region bgwItextsharpdll_DoWork
        private void bgwItextsharpdll_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (File.Exists(Path.Combine(Application.StartupPath, "PDFSignDll.dll")) == true)
                    return;
                // first, we need to get the exact size (in bytes) of the file we are downloading
                Uri url = new Uri(UploadedDLLPath);
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
                    using (Stream streamDLLRemote = client.OpenRead(new Uri(UploadedDLLPath)))
                    {
                        // using the FileStream object, we can write the downloaded bytes to the file system
                        using (Stream streamDLL = new FileStream(Path.Combine(Application.StartupPath, UploadedDLLName), FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            // loop the stream and get the file into the byte buffer
                            int intByteSize = 0;
                            byte[] byteBuffer = new byte[intSize];
                            //
                            while ((intByteSize = streamDLLRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                            {
                                // write the bytes to the file system at the file path specified
                                streamDLL.Write(byteBuffer, 0, intByteSize);
                                //
                                //if (bgwWorker.CancellationPending)//checks for cancel request
                                //{
                                //    break;
                                //}
                                //
                                intRunningByteTotal += intByteSize;
                                // calculate the progress out of a base "100"
                                double dblIndex = (double)(intRunningByteTotal);
                                double dblTotal = (double)byteBuffer.Length;
                                double dblProgressPercentage = (dblIndex / dblTotal);
                                int intProgressPercentage = (int)(dblProgressPercentage * 100);
                                // update the progress bar
                                bgwItextsharpdll.ReportProgress(intProgressPercentage);
                            }                            
                            // clean up the file stream
                            streamDLL.Close();
                        }
                        // close the connection to the remote server
                        streamDLLRemote.Close();
                    }
                }
            }
            catch
            {
            }
        }
        #endregion

        #region bgwItextsharpdll_RunWorkerCompleted
        private void bgwItextsharpdll_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmnservice.J_UnZipBool(Path.Combine(Application.StartupPath, UploadedDLLName));
            File.Delete(Path.Combine(Application.StartupPath, UploadedDLLName));
            //
            this.Dispose();
            this.Close();
        }
        #endregion

        #region bgwItextsharpdll_ProgressChanged
        private void bgwItextsharpdll_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgBar.Value = e.ProgressPercentage;
        }
        #endregion
    }
}