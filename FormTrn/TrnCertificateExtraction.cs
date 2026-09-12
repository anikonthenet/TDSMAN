#region Refered Namespaces & Classes

extern alias global2;

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
using System.Security.AccessControl;
using System.IO;
using System.Net;

//using Microsoft.Office.Interop.Access;

//using System.Runtime.InteropServices;
using System.Data.OleDb;
//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using System.Security.Cryptography.X509Certificates;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using System.Linq;
using iTextSharp.text.pdf;
//--

using ICSharpCode.SharpZipLib.Zip;
using Excel = Microsoft.Office.Interop.Excel.Worksheet;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnCertificateExtraction : Form
    {
        #region Objects & Variables declaration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        ExcelService ExcelService = new ExcelService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //-----------------------------------------------------------------------
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        //string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        //string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //Microsoft.Office.Interop.Access.Application Access = new Microsoft.Office.Interop.Access.Application();
        //-----------------------------------------------------------------------
        int intCaratPosition = 0;
        string strFVUPath = "", strCDCsvPath = "", strDDCsvPath = "";
        long lngBasicInfoID = 0;
        string strErrorWorksheetName = "Validation Error";
        //-----------------------------------------------------------------------
        string strNewDeducteeWorkSheetName = "New Deductees Found";
        string strDeductionsChVIAWorkSheetName = "Deductions ChVI-A"; //-- 2020/05/13
        string strAnnexureIIIChVIAWorkSheetName = "Deductions ChVI-A"; //-- 2020/05/13
        long lngRowCount;
        long lngNewDeducteesCreated;
        //
        string strErrorMessage = "";

        string strTemporaryfileChallanPath = "";
        string strTemporaryfileDeducteePath = "";
        string strTemporaryfileSalaryPath = "";
        string strTemporaryfileForm16SalaryPath = "", strTemporaryfileSalaryCHVIAPath = "";
        string strTemporaryfileAnnexureIII = "", strTemporaryfileAnnexureIIICHVIAPath = "";

        OleDbDataAdapter myCommand;
        string strConnectionString = "";
        OleDbConnection con;
        //
        bool blnOpenTabPage = false;
        int CSVImport = 0;

        //ADDED BY DHRUB FOR ADDING NEW FIELDS INTO TEXT
        //      FOR SALARY DETAILS 2013-14  AND ONWARDS 
        int intInitialColumnAfterZ = 0;
        //int intInitialColumnNumbering = 1;
        string strExcelFieldAsciiAfterZ = "";
        string strAAsciiAfterZ = "";
        string strBAsciiAfterZ = "";
        string strCAsciiAfterZ = "";
        string strDAsciiAfterZ = "";
        string strEAsciiAfterZ = "";

        //ADDED BY DHRUB ON 30/12/2013 FOR ADDING THE EXCEL IMPORT VALUES 
        //     FOR INTEREST ALLOCATED AND OTHER INTEREST ALLOCATED 
        string strImportValuesToAllocated = "";

        //Added by Dhrub Mukherjee On 07/01/2014
        //---------------------------------------
        //---------------------------------------
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        ToolTip tllTip = new ToolTip();

        double dblTotalExcelRecords = 0; //-- 2015/07/31
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        bool blnEmpReferenceNo = false; //-- 2020/06/04
        //--
        string strCheckExcelStructureMessage = "";
        //
        bool blnSALARY_DETAILS_CHVIA = false;
        bool blnANNEXUREIII_CHVIA = false;
        bool blnSection194NF = false, blnSection194N = false;
        string strFormNo = "", strFinancialYear = "", strQtr = "", strCompany = "";

        

        bool blnDeducteeDetailsOnly = false;
        long intFinancialYearId = 0, intCompanyId = 0;

        bool Incremental = false, NewImport = false, blnCSVImport = false;

        

        long lngMAXLineNumbersPANVerificationMessage = 500;
        string strMessagePANVerification = "";
        #endregion

        #region TrnCertificateExtraction
        public TrnCertificateExtraction()
        {
            InitializeComponent();
        }
        #endregion

        #region BtnExtract_Click
        private void BtnExtract_Click(object sender, EventArgs e)
        {
            bool blFilesCreated = false;
            if (txtZIPFilePath.Text == "")
            {
                cmnService.J_UserMessage("Choose the ZIP file");
                return;
            }
            if (txtPassword.Text == "")
            {
                cmnService.J_UserMessage("Enter the Password");
                return;
            }
            if (txtOutputFolder.Text == "")
            {
                cmnService.J_UserMessage("Select the Output Folder");
                return;
            }
            //--
            string strBatFile = "", strJAVAFile = "", strFolder = "";

            if (grpSelectCertificates.Visible == true)
            {
                if (rbnFrom1616A.Checked == true)
                {
                    strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorBundle\\RUN_PDF_GEN.BAT");
                    strJAVAFile = " PDFGeneratorCLI "; strFolder = "PDFGeneratorBundle";
                }
                else
                {
                    strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorPartBBundle\\RUN_PDF_GEN.BAT");
                    strJAVAFile = " PDFPartBGeneratorCLI "; strFolder = "PDFGeneratorPartBBundle";
                }
            }
            else
            {                
                if (txtZIPFilePath.Text.Contains("PARTB"))
                {
                    strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorPartBBundle\\RUN_PDF_GEN.BAT");
                    strJAVAFile = " PDFPartBGeneratorCLI "; strFolder = "PDFGeneratorPartBBundle";
                }
                else if (txtZIPFilePath.Text.Contains("FORM16"))
                {
                    strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorBundle\\RUN_PDF_GEN.BAT");
                    strJAVAFile = " PDFGeneratorCLI "; strFolder = "PDFGeneratorBundle";
                }
                else if (txtZIPFilePath.Text.Contains("FORM27D"))
                {
                    strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorBundle27D\\RUN_PDF_GEN.BAT");
                    strJAVAFile = " PDFGeneratorCLI "; strFolder = "PDFGeneratorBundle27D";
                }

                else
                {
                    grpSelectCertificates.Visible = true;
                    strJAVAFile = "";
                    cmnService.J_UserMessage("Select the Certificate type");
                    rbnFrom1616A.Focus();
                    return;
                }
            }
            #region COMMENTED
            //string strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorBundle\\PART_B\\RUN_PDF_GEN.BAT");
            ////////// DELETION OF BAT FILE IF EXISTS
            ////////if (File.Exists(strBatFile) == true)
            ////////    File.Delete(strBatFile);
            ////////// CREATION OF BAT FILE
            ////////File.AppendAllText(strBatFile, "");
            //////////
            ////////StreamWriter StreamWriter = cmnService.J_ReturnStreamWriter(strBatFile);
            ////////cmnService.J_WriteLine(ref StreamWriter, "@echo off");
            ////////cmnService.J_WriteLine(ref StreamWriter, "java -cp " + (Char)34 + ".;lib/*" + (Char)34 + strJAVAFile + (Char)34 + txtZIPFilePath.Text + (Char)34 + " " + (Char)34 + txtPassword.Text + (Char)34 + " " + (Char)34 + txtOutputFolder.Text + (Char)34);
            //////////
            ////////StreamWriter.Flush();
            ////////StreamWriter.Close();
            //////////
            ////////int ExitCode;
            //////////
            ////////ProcessStartInfo ProcessInfo = new ProcessStartInfo
            ////////{
            ////////    FileName = strBatFile,
            ////////    CreateNoWindow = true,
            ////////    UseShellExecute = false,
            ////////    RedirectStandardOutput = true,
            ////////    RedirectStandardError = true,
            ////////    //WorkingDirectory = Path.Combine(Application.StartupPath, "PDFGeneratorBundle")
            ////////    WorkingDirectory = Path.Combine(Application.StartupPath, strFolder)

            ////////};
            ////////this.Cursor = Cursors.WaitCursor;
            ////////using (Process processBatchFile = Process.Start(ProcessInfo))
            ////////{
            ////////    string output = processBatchFile.StandardOutput.ReadToEnd();
            ////////    string error = processBatchFile.StandardError.ReadToEnd();
            ////////    processBatchFile.WaitForExit();

            ////////    //Console.WriteLine("Output: " + output);
            ////////    if (!string.IsNullOrWhiteSpace(error))
            ////////    {
            ////////        this.Cursor = Cursors.Default;
            ////////        if (error.ToUpper().Contains("WRONG PASSWORD"))
            ////////            cmnService.J_UserMessage("Check the password !!");
            ////////        else if(error.ToUpper().Contains("JAVA.LANG.ARRAYINDEXOUTOFBOUNDSEXCEPTION"))
            ////////            cmnService.J_UserMessage("Wrong input file selected !!");
            ////////        else
            ////////            cmnService.J_UserMessage(error);
            ////////    }
            ////////    else if (error.ToUpper().Contains("PDF FILES CREATED"))
            ////////    {
            ////////        this.Cursor = Cursors.Default;
            ////////        //cmnService.J_UserMessage("Output: " + output);
            ////////        //System.Diagnostics.Process.Start(txtOutputFolder.Text);
            ////////        blFilesCreated = true;
            ////////    }
            ////////    else if (output.ToUpper().Contains("NO PDF FILES GENERATED"))
            ////////    {
            ////////        this.Cursor = Cursors.Default;
            ////////        cmnService.J_UserMessage("No PDF files generated or an error occurred.");
            ////////        //cmnService.J_UserMessage("Output: " + output);
            ////////        //System.Diagnostics.Process.Start(txtOutputFolder.Text);
            ////////        blFilesCreated = false;
            ////////    }
            ////////    else
            ////////    {
            ////////        //cmnService.J_UserMessage("PDF files creation failed");
            ////////        this.Cursor = Cursors.Default;
            ////////        //System.Diagnostics.Process.Start(txtOutputFolder.Text);
            ////////        blFilesCreated = true;
            ////////    }
            ////////}
            ////////this.Cursor = Cursors.Default;
            ////////if(Directory.Exists(Path.Combine(txtOutputFolder.Text, "resources")))
            ////////    Directory.Delete(Path.Combine(txtOutputFolder.Text, "resources"), true);
            ////////if (Directory.Exists(Path.Combine(txtOutputFolder.Text, "temp")))
            ////////    Directory.Delete(Path.Combine(txtOutputFolder.Text, "temp"), true);
            #endregion
            ///
            UnZipFiles(txtZIPFilePath.Text,Path.Combine(txtOutputFolder.Text, "temp"), txtPassword.Text, false);
            //
            System.Threading.Thread.Sleep(3000);
            string OutputPath = txtOutputFolder.Text;
            // Get first .txt file found in temp directory
            string strtxtFileName = Directory.GetFiles(Path.Combine(txtOutputFolder.Text, "temp"), "*.txt").FirstOrDefault();
            //
            var iNoOfCertificates = TdsMan.CountBatchesfromExtractedTextFile(strtxtFileName, txtZIPFilePath.Text);
            int intNoOfCertificates = iNoOfCertificates.Values.Count();
            ///
            this.Cursor = Cursors.WaitCursor;
            //blFilesCreated = TdsMan.ExtractCertificates(strBatFile, strJAVAFile, strFolder, txtZIPFilePath.Text, txtPassword.Text, txtOutputFolder.Text);
            //blFilesCreated = TdsMan.ExtractCertificatesDSC(strBatFile, strJAVAFile, strFolder, txtZIPFilePath.Text, txtPassword.Text, txtOutputFolder.Text);
            bool Signed = false;
            global2.PDFSign PDFSign;
            //
            //PDFSign = ReturnDSC("", "");
            //--
            //if (PDFSign == null)
            //    Signed = false;
            //else
            //    Signed = true;
            if(chkSignedPDF.Checked == true)
            {
                Signed = true;
            }
            //--
            try
            {
                if(strJAVAFile == " PDFPartBGeneratorCLI ")
                    blFilesCreated = TdsMan.ExtractPartBCertificatesSignedDigitally(strBatFile, strJAVAFile, strFolder, txtZIPFilePath.Text, txtPassword.Text, txtOutputFolder.Text, intNoOfCertificates, 80,  Signed);
                else
                    blFilesCreated = TdsMan.ExtractCertificatesSignedDigitally(strBatFile, strJAVAFile, strFolder, txtZIPFilePath.Text, txtPassword.Text, txtOutputFolder.Text, intNoOfCertificates, 80, Signed);
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
            this.Cursor = Cursors.Default;
            //--
            if (blFilesCreated == true)
            {
                System.Diagnostics.Process.Start(txtOutputFolder.Text);
            }
        }
        #endregion 

        #region BtnGetOutputFolderPath_Click
        private void BtnGetZipFile_Click(object sender, EventArgs e)
        {
            string strZIPPath = cmnService.J_OpenFileDialog("ZIP File | *.zip", "ZIP File | *.zip", "Choose the zip File to import");
            //--
            if (strZIPPath != "")
                txtZIPFilePath.Text = strZIPPath;
        }
        #endregion  

        #region BtnGetOutputFolderPath_Click
        private void BtnGetOutputFolderPath_Click(object sender, EventArgs e)
        {
            string strBackupFolderPath = cmnService.J_OpenFolderDialog();
            if (strBackupFolderPath == "")
            {
                MessageBox.Show("Select the Output Folder", "TEST", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //BtnExit.Select();
                return;
            }
            txtOutputFolder.Text = strBackupFolderPath;
        }
#endregion

        #region Create_Signature
        private bool Create_Signature(global2.PDFSign PDFSign, string SourcePath, string SignedFilePath, string strPassword, int x, int y, int x1, int y1, int LastPage)
        {
            //global2.PDFSign PDFSign = new global2.PDFSign("serial number");
            try
            {
                //global2.PDFSign PDFSign = new global2.PDFSign("serial number");
                PDFSign.LoadPDFDocument(SourcePath);
                ////
                //if (strPFXFilePath != "")
                //{
                //    PDFSign.DigitalSignatureCertificate = PDFSign.LoadCertificate(File.ReadAllBytes(strPFXFilePath), strPassword); 
                //    //
                //    if (PDFSign.DigitalSignatureCertificate == null)
                //    {
                //        cmnService.J_UserMessage("No Digital Signature found !!");
                //        return false;
                //    }
                //    //
                //}
                //else
                //{
                //    //load the certificate from Microsoft Store
                //    PDFSign.DigitalSignatureCertificate = PDFSign.LoadCertificate(false, "", "Digital certificates", "Select the digital certificate", global2.DigitalCertificateScope.ForDigitalSignature);
                //    //PDFSign.DigitalSignatureCertificate = PDFSign.LoadCertificate(false, DigitalCertificateSearchCriteria.EmailE, "test@test.com", DigitalCertificateScope.ForDigitalSignature);
                //    if(PDFSign.DigitalSignatureCertificate == null)
                //    {
                //        cmnService.J_UserMessage("No Digital Signature found in Windows Store !!");
                //        return false;
                //    }
                //}
                //--
                //PDFSign.SignaturePage = 2;
                PDFSign.SignaturePage = LastPage;

                //set the signature position
                // System.Drawing.Point pageRectangle = PDFSign.DocumentPageSize(1);
                //put the signature on the middle of the page
                // PDFSign.SignaturePosition = new System.Drawing.Rectangle(pageRectangle.X / 2, pageRectangle.Y / 2, 100, 50);

                //Set the signature rectangle attributes
                //PDFSign.SignaturePage = 1;
                //PDFSign.SignatureBasicPosition = global2.BasicSignatureLocation.TopRight;

                //PDFSign.SigningReason = "I approve this document";
                //PDFSign.SigningLocation = "Kolkata";
                //PDFSign.SignerContactInformation = "Author contact information";
                PDFSign.SignedBy = "Digitally signed by the " + PDFSign.DigitalSignatureCertificate.GetNameInfo(X509NameType.SimpleName, false);// false);
                //
                PDFSign.SignerContactInformation = PDFSign.DigitalSignatureCertificate.GetNameInfo(X509NameType.EmailName, false);

                //PDFSign.FontFile = "c:\\windows\\fonts\\verdana.ttf";
                //PDFSign.FontSize = 6;
                //PDFSign.SignatureBasicPosition = global2.BasicSignatureLocation.BottomRight;
                ////PDFSign.SignatureDate = new DateTime(2014, 12, 20, 13, 00, 00);

                ////Custom signature position
                PDFSign.SignaturePosition = new System.Drawing.Rectangle(x, y, x1, y1);
                //PDFSign.SignatureText = PDFSign.SignedBy +
                //                        "\n Date:" + DateTime.Now.ToString("yyyy.MM.dd HH:mm") + "\n" +
                //                        "Reason:" + PDFSign.SigningReason;

                PDFSign.SignatureText = PDFSign.SignedBy + "<" + PDFSign.SignerContactInformation + ">" +
                                        "\n Date:" + DateTime.Now.ToString("yyyy.MM.dd HH:mm");
                PDFSign.IncludeCRLRevocationInfo = false;
                //-- 2021/07/16
                if (File.Exists(Path.Combine(Application.StartupPath, "GREEN_TICK.PNG")) == true)
                    PDFSign.SignatureImage = System.IO.File.ReadAllBytes(Application.StartupPath + "/GREEN_TICK.PNG");
                //////////////////// PDFSign.TextDirection = TextDirection.RightToLeft;
                //////////////////// File.WriteAllBytes("c:\\dest.pdf", PDFSign.ApplyDigitalSignature());

                //File.WriteAllBytes(Environment.CurrentDirectory + "\\dest.pdf", PDFSign.ApplyDigitalSignature());
                File.WriteAllBytes(SignedFilePath, PDFSign.ApplyDigitalSignature());
                //
                return true;
            }
            catch (Exception err)
            {
                //PDFSign.
                cmnService.J_UserMessage(err.Message, MessageBoxIcon.Error);
                return false;
            }
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

        #region ExtractCertificates
        public bool ExtractCertificates(string BatFile, string JavaFile, string FolderPath,
                                        string InputZipFilePath, string PasswordZip, string OutputFolder)
        {
            try
            {
                // DELETION OF BAT FILE IF EXISTS
                if (File.Exists(BatFile) == true)
                    File.Delete(BatFile);

                // CREATION OF BAT FILE
                File.AppendAllText(BatFile, "");
                //
                StreamWriter StreamWriter = cmnService.J_ReturnStreamWriter(BatFile);
                cmnService.J_WriteLine(ref StreamWriter, "@echo off");
                cmnService.J_WriteLine(ref StreamWriter, "java -cp " + (Char)34 + ".;lib/*" + (Char)34 + JavaFile + (Char)34 + InputZipFilePath + (Char)34 + " " + (Char)34 + PasswordZip + (Char)34 + " " + (Char)34 + OutputFolder + (Char)34);               
                //
                StreamWriter.Flush();
                StreamWriter.Close();
                //
                int ExitCode;
                //
                ProcessStartInfo ProcessInfo = new ProcessStartInfo
                {
                    FileName = BatFile,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = Path.Combine(Application.StartupPath, FolderPath)

                };
                this.Cursor = Cursors.WaitCursor;
                using (Process processBatchFile = Process.Start(ProcessInfo))
                {
                    string output = processBatchFile.StandardOutput.ReadToEnd();
                    string error = processBatchFile.StandardError.ReadToEnd();
                    processBatchFile.WaitForExit();
                    //
                    if (Directory.Exists(Path.Combine(OutputFolder, "resources")))
                        Directory.Delete(Path.Combine(OutputFolder, "resources"), true);
                    if (Directory.Exists(Path.Combine(OutputFolder, "temp")))
                        Directory.Delete(Path.Combine(OutputFolder, "temp"), true);
                    //Console.WriteLine("Output: " + output);
                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        this.Cursor = Cursors.Default;
                        if (error.ToUpper().Contains("WRONG PASSWORD"))
                            cmnService.J_UserMessage("Check the password !!");
                        else if (error.ToUpper().Contains("JAVA.LANG.ARRAYINDEXOUTOFBOUNDSEXCEPTION"))
                            cmnService.J_UserMessage("Wrong input file selected !!");
                        else
                            cmnService.J_UserMessage(error);
                    }
                    else if (error.ToUpper().Contains("PDF FILES CREATED"))
                    {
                        this.Cursor = Cursors.Default;
                        return true;
                    }
                    else if (output.ToUpper().Contains("NO PDF FILES GENERATED"))
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("No PDF files generated or an error occurred.");
                        return false;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        return true;
                    }
                }
                this.Cursor = Cursors.Default;
                //if (Directory.Exists(Path.Combine(OutputFolder, "resources")))
                //    Directory.Delete(Path.Combine(OutputFolder, "resources"), true);
                //if (Directory.Exists(Path.Combine(OutputFolder, "temp")))
                //    Directory.Delete(Path.Combine(OutputFolder, "temp"), true);
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion

        #region BgReadingTextFile_DoWork
        private void BgReadingTextFile_DoWork(object sender, DoWorkEventArgs e)
        {
            //string OutputPath = txtOutputFolder.Text;

            //// Get first .txt file found in temp directory
            //string txtFileName = Directory.GetFiles(OutputPath, "*.txt").FirstOrDefault();
            ////
            //int iNoOfCertificates =  TdsMan.CountBatchesfromExtractedTextFile(Path.Combine(OutputPath, txtFileName));
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

    }

}
