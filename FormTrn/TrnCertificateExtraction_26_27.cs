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
using System.Text;

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
using System.Windows.Automation;
using System.Collections.Generic;
//--

using ICSharpCode.SharpZipLib.Zip;
using Excel = Microsoft.Office.Interop.Excel.Worksheet;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnCertificateExtraction_26_27 : Form
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
        public TrnCertificateExtraction_26_27()
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
            //////--
            #region COMMENTED
            ////string strBatFile = "", strJAVAFile = "", strFolder = "";

            ////if (grpSelectCertificates.Visible == true)
            ////{
            ////    if (rbnFrom1616A.Checked == true)
            ////    {
            ////        strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorBundle\\RUN_PDF_GEN.BAT");
            ////        strJAVAFile = " PDFGeneratorCLI "; strFolder = "PDFGeneratorBundle";
            ////    }
            ////    else
            ////    {
            ////        strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorPartBBundle\\RUN_PDF_GEN.BAT");
            ////        strJAVAFile = " PDFPartBGeneratorCLI "; strFolder = "PDFGeneratorPartBBundle";
            ////    }
            ////}
            ////else
            ////{                
            ////    if (txtZIPFilePath.Text.Contains("PARTB"))
            ////    {
            ////        strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorPartBBundle\\RUN_PDF_GEN.BAT");
            ////        strJAVAFile = " PDFPartBGeneratorCLI "; strFolder = "PDFGeneratorPartBBundle";
            ////    }
            ////    else if (txtZIPFilePath.Text.Contains("FORM16"))
            ////    {
            ////        strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorBundle\\RUN_PDF_GEN.BAT");
            ////        strJAVAFile = " PDFGeneratorCLI "; strFolder = "PDFGeneratorBundle";
            ////    }
            ////    else if (txtZIPFilePath.Text.Contains("FORM27D"))
            ////    {
            ////        strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorBundle27D\\RUN_PDF_GEN.BAT");
            ////        strJAVAFile = " PDFGeneratorCLI "; strFolder = "PDFGeneratorBundle27D";
            ////    }

            ////    else
            ////    {
            ////        grpSelectCertificates.Visible = true;
            ////        strJAVAFile = "";
            ////        cmnService.J_UserMessage("Select the Certificate type");
            ////        rbnFrom1616A.Focus();
            ////        return;
            ////    }
            ////}
            ////#region COMMENTED
            //////string strBatFile = Path.Combine(Application.StartupPath, "PDFGeneratorBundle\\PART_B\\RUN_PDF_GEN.BAT");
            ////////////// DELETION OF BAT FILE IF EXISTS
            ////////////if (File.Exists(strBatFile) == true)
            ////////////    File.Delete(strBatFile);
            ////////////// CREATION OF BAT FILE
            ////////////File.AppendAllText(strBatFile, "");
            //////////////
            ////////////StreamWriter StreamWriter = cmnService.J_ReturnStreamWriter(strBatFile);
            ////////////cmnService.J_WriteLine(ref StreamWriter, "@echo off");
            ////////////cmnService.J_WriteLine(ref StreamWriter, "java -cp " + (Char)34 + ".;lib/*" + (Char)34 + strJAVAFile + (Char)34 + txtZIPFilePath.Text + (Char)34 + " " + (Char)34 + txtPassword.Text + (Char)34 + " " + (Char)34 + txtOutputFolder.Text + (Char)34);
            //////////////
            ////////////StreamWriter.Flush();
            ////////////StreamWriter.Close();
            //////////////
            ////////////int ExitCode;
            //////////////
            ////////////ProcessStartInfo ProcessInfo = new ProcessStartInfo
            ////////////{
            ////////////    FileName = strBatFile,
            ////////////    CreateNoWindow = true,
            ////////////    UseShellExecute = false,
            ////////////    RedirectStandardOutput = true,
            ////////////    RedirectStandardError = true,
            ////////////    //WorkingDirectory = Path.Combine(Application.StartupPath, "PDFGeneratorBundle")
            ////////////    WorkingDirectory = Path.Combine(Application.StartupPath, strFolder)

            ////////////};
            ////////////this.Cursor = Cursors.WaitCursor;
            ////////////using (Process processBatchFile = Process.Start(ProcessInfo))
            ////////////{
            ////////////    string output = processBatchFile.StandardOutput.ReadToEnd();
            ////////////    string error = processBatchFile.StandardError.ReadToEnd();
            ////////////    processBatchFile.WaitForExit();

            ////////////    //Console.WriteLine("Output: " + output);
            ////////////    if (!string.IsNullOrWhiteSpace(error))
            ////////////    {
            ////////////        this.Cursor = Cursors.Default;
            ////////////        if (error.ToUpper().Contains("WRONG PASSWORD"))
            ////////////            cmnService.J_UserMessage("Check the password !!");
            ////////////        else if(error.ToUpper().Contains("JAVA.LANG.ARRAYINDEXOUTOFBOUNDSEXCEPTION"))
            ////////////            cmnService.J_UserMessage("Wrong input file selected !!");
            ////////////        else
            ////////////            cmnService.J_UserMessage(error);
            ////////////    }
            ////////////    else if (error.ToUpper().Contains("PDF FILES CREATED"))
            ////////////    {
            ////////////        this.Cursor = Cursors.Default;
            ////////////        //cmnService.J_UserMessage("Output: " + output);
            ////////////        //System.Diagnostics.Process.Start(txtOutputFolder.Text);
            ////////////        blFilesCreated = true;
            ////////////    }
            ////////////    else if (output.ToUpper().Contains("NO PDF FILES GENERATED"))
            ////////////    {
            ////////////        this.Cursor = Cursors.Default;
            ////////////        cmnService.J_UserMessage("No PDF files generated or an error occurred.");
            ////////////        //cmnService.J_UserMessage("Output: " + output);
            ////////////        //System.Diagnostics.Process.Start(txtOutputFolder.Text);
            ////////////        blFilesCreated = false;
            ////////////    }
            ////////////    else
            ////////////    {
            ////////////        //cmnService.J_UserMessage("PDF files creation failed");
            ////////////        this.Cursor = Cursors.Default;
            ////////////        //System.Diagnostics.Process.Start(txtOutputFolder.Text);
            ////////////        blFilesCreated = true;
            ////////////    }
            ////////////}
            ////////////this.Cursor = Cursors.Default;
            ////////////if(Directory.Exists(Path.Combine(txtOutputFolder.Text, "resources")))
            ////////////    Directory.Delete(Path.Combine(txtOutputFolder.Text, "resources"), true);
            ////////////if (Directory.Exists(Path.Combine(txtOutputFolder.Text, "temp")))
            ////////////    Directory.Delete(Path.Combine(txtOutputFolder.Text, "temp"), true);
            ////#endregion
            ///////
            ////UnZipFiles(txtZIPFilePath.Text,Path.Combine(txtOutputFolder.Text, "temp"), txtPassword.Text, false);
            //////
            ////System.Threading.Thread.Sleep(3000);
            ////string OutputPath = txtOutputFolder.Text;
            ////// Get first .txt file found in temp directory
            ////string strtxtFileName = Directory.GetFiles(Path.Combine(txtOutputFolder.Text, "temp"), "*.txt").FirstOrDefault();
            //////
            ////var iNoOfCertificates = TdsMan.CountBatchesfromExtractedTextFile(strtxtFileName, txtZIPFilePath.Text);
            ////int intNoOfCertificates = iNoOfCertificates.Values.Count();
            ///////
            ////this.Cursor = Cursors.WaitCursor;
            //////blFilesCreated = TdsMan.ExtractCertificates(strBatFile, strJAVAFile, strFolder, txtZIPFilePath.Text, txtPassword.Text, txtOutputFolder.Text);
            //////blFilesCreated = TdsMan.ExtractCertificatesDSC(strBatFile, strJAVAFile, strFolder, txtZIPFilePath.Text, txtPassword.Text, txtOutputFolder.Text);
            ////bool Signed = false;
            ////global2.PDFSign PDFSign;
            //////
            //////PDFSign = ReturnDSC("", "");
            //////--
            //////if (PDFSign == null)
            //////    Signed = false;
            //////else
            //////    Signed = true;
            ////if(chkSignedPDF.Checked == true)
            ////{
            ////    Signed = true;
            ////}
            //////--
            ////try
            ////{
            ////    if(strJAVAFile == " PDFPartBGeneratorCLI ")
            ////        blFilesCreated = TdsMan.ExtractPartBCertificatesSignedDigitally(strBatFile, strJAVAFile, strFolder, txtZIPFilePath.Text, txtPassword.Text, txtOutputFolder.Text, intNoOfCertificates, 80,  Signed);
            ////    else
            ////        blFilesCreated = TdsMan.ExtractCertificatesSignedDigitally(strBatFile, strJAVAFile, strFolder, txtZIPFilePath.Text, txtPassword.Text, txtOutputFolder.Text, intNoOfCertificates, 80, Signed);
            ////}
            ////catch (Exception err)
            ////{
            ////    cmnService.J_UserMessage(err.Message);
            ////}
            ////this.Cursor = Cursors.Default;
            //////--
            ////if (blFilesCreated == true)
            ////{
            ////    System.Diagnostics.Process.Start(txtOutputFolder.Text);
            ////}
            #endregion
            //////--
            bool success =
                   ExtractCertificatesUsingNewTracesUtility(
                    txtZIPFilePath.Text,
                    txtPassword.Text,
                    txtOutputFolder.Text,
                    chkSignedPDF.Checked);

            if (success)
            {
                Process.Start(txtOutputFolder.Text);
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

        //########################################

        #region StartNewTracesPdfUtility
        private Process StartNewTracesPdfUtility()
        {
            string utilityFolder = Path.Combine(
                Application.StartupPath,
                "TRACESPDFUtility");

            string exePath = Path.Combine(
                utilityFolder,
                "pdf_generation_utility.exe");

            if (!File.Exists(exePath))
            {
                cmnService.J_UserMessage(
                    "TRACES PDF Generation Utility not found.");

                return null;
            }

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = exePath,
                WorkingDirectory = utilityFolder,
                UseShellExecute = true
            };

            Process process = Process.Start(psi);

            if (process != null)
            {
                process.WaitForInputIdle();
            }

            return process;
        }
        #endregion

        #region GetUtilityWindow
        private AutomationElement GetUtilityWindow(Process process)
        {
            for (int i = 0; i < 50; i++)
            {
                process.Refresh();

                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    return AutomationElement.FromHandle(
                        process.MainWindowHandle);
                }

                System.Threading.Thread.Sleep(200);
            }

            return null;
        }
        #endregion

        #region InvokeButton
        private bool InvokeButton(AutomationElement root, string buttonName)
        {
            AutomationElement element =
                root.FindFirst(
                    TreeScope.Descendants,
                    new PropertyCondition(
                        AutomationElement.NameProperty,
                        buttonName));

            if (element == null)
                return false;

            object pattern;

            if (element.TryGetCurrentPattern(
                InvokePattern.Pattern,
                out pattern))
            {
                ((InvokePattern)pattern).Invoke();
                return true;
            }

            return false;
        }
        #endregion

        #region GetNewCertificateForm
        private string GetNewCertificateForm(string zipFileName)
        {
            string name = Path.GetFileName(zipFileName).ToUpper();

            if (name.Contains("FORM131"))
                return "Form 131";

            if (name.Contains("FORM133"))
                return "Form 133";

            if (name.Contains("TBR"))
                return "TBR";

            return "";
        }
        #endregion

        #region SetValue
        private bool SetValue(AutomationElement element, string value)
        {
            object pattern;

            if (element.TryGetCurrentPattern(
                ValuePattern.Pattern,
                out pattern))
            {
                ((ValuePattern)pattern).SetValue(value);
                return true;
            }

            return false;
        }
        #endregion

        #region WaitForPdfGeneration
        private bool WaitForPdfGeneration(string outputFolder, int expectedCount, int timeoutMinutes)
        {
            DateTime endTime =
                DateTime.Now.AddMinutes(timeoutMinutes);

            while (DateTime.Now < endTime)
            {
                string[] files =
                    Directory.GetFiles(
                        outputFolder,
                        "*.pdf",
                        SearchOption.AllDirectories);

                if (files.Length >= expectedCount)
                    return true;

                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
            }

            return false;
        }
        #endregion

        #region ExtractCertificatesUsingNewTracesUtility

        private bool ExtractCertificatesUsingNewTracesUtility(
            string inputZipFilePath,
            string password,
            string outputFolder,
            bool signed)
        {
            try
            {
                // ---------------------------------------------------------
                // 1. LOCATION OF NEW TRACES PDF GENERATION UTILITY
                // ---------------------------------------------------------
                string utilityFolder = Path.Combine(
                    Application.StartupPath,
                    "TRACESPDFUtility");

                string utilityExe = Path.Combine(
                    utilityFolder,
                    "pdf_generation_utility.exe");

                // ---------------------------------------------------------
                // 2. CHECK WHETHER UTILITY EXISTS
                // ---------------------------------------------------------
                if (!File.Exists(utilityExe))
                {
                    cmnService.J_UserMessage(
                        "TRACES PDF Generation Utility not found.\n\n" +
                        utilityExe);

                    return false;
                }

                // ---------------------------------------------------------
                // 3. CREATE OUTPUT FOLDER IF REQUIRED
                // ---------------------------------------------------------
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }
                Dictionary<string, string> pdfSnapshotBefore =
                    GetPdfSnapshot(outputFolder);
                // ---------------------------------------------------------
                // 4. START TRACES PDF GENERATION UTILITY
                // ---------------------------------------------------------
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = utilityExe,

                    // IMPORTANT:
                    // Flutter utility requires its DLLs/data folder from here
                    WorkingDirectory = utilityFolder,

                    UseShellExecute = true
                };

                Process process = Process.Start(processInfo);

                if (process == null)
                {
                    cmnService.J_UserMessage(
                        "Unable to start TRACES PDF Generation Utility.");

                    return false;
                }

                // ---------------------------------------------------------
                // 5. WAIT UNTIL MAIN WINDOW IS AVAILABLE
                // ---------------------------------------------------------
                IntPtr hWnd = IntPtr.Zero;

                for (int i = 0; i < 100; i++)
                {
                    try
                    {
                        process.Refresh();

                        if (process.HasExited)
                        {
                            cmnService.J_UserMessage(
                                "TRACES PDF Generation Utility closed unexpectedly.");

                            return false;
                        }

                        if (process.MainWindowHandle != IntPtr.Zero)
                        {
                            hWnd = process.MainWindowHandle;
                            break;
                        }
                    }
                    catch
                    {
                        // Process may still be starting
                    }

                    System.Threading.Thread.Sleep(200);
                }

                if (hWnd == IntPtr.Zero)
                {
                    cmnService.J_UserMessage(
                        "TRACES PDF Generation Utility window could not be detected.");

                    return false;
                }

                // ---------------------------------------------------------
                // 6. BRING TRACES WINDOW TO FRONT
                // ---------------------------------------------------------
                ShowWindow(hWnd, SW_RESTORE);
                SetForegroundWindow(hWnd);

                System.Threading.Thread.Sleep(700);

                // ---------------------------------------------------------
                // 7. CLICK "GET STARTED"
                // ---------------------------------------------------------
                if (!ClickGetStarted(hWnd))
                {
                    cmnService.J_UserMessage("Unable to click Get Started in TRACES PDF Generation Utility.");

                    return false;
                }

                // Allow Flutter to load next screen
                System.Threading.Thread.Sleep(1500);

                // Remember PDFs already present before generation
                string pdfStateBefore =
                    GetPdfFolderState(outputFolder);

                // Fill second PDF Generation screen
                if (!FillPdfGenerationInputs(
                        hWnd,
                        inputZipFilePath,
                        password,
                        outputFolder,
                        signed))
                {
                    return false;
                }
                // ---------------------------------------------------------
                // FOR THE TIME BEING RETURN TRUE HERE.
                //
                // At this stage TRUE means:
                // Utility opened + Get Started clicked successfully.
                //
                // In next step we will automate:
                // Form Type
                // ZIP File
                // Password
                // Output Folder
                // DSC = No
                // Generate
                // ---------------------------------------------------------
                // TEMPORARY TEST:
                // Allow PDF generation to complete
                // Wait until new/updated PDFs have actually
                // finished generating
                if (!WaitForPdfGenerationCompletion(
                        outputFolder,
                        pdfStateBefore,
                        300000))     // 5-minute SAFETY timeout only
                {
                    cmnService.J_UserMessage(
                        "PDF generation could not be confirmed.");

                    return false;
                }

                // PDFs are ready - close TRACES immediately
                if (!CloseTracesApplication(process))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage(
                    "Error starting TRACES PDF Generation Utility:\n\n" +
                    ex.Message);

                return false;
            }
        }

        private string GetPdfFolderState(string outputFolder)
        {
            try
            {
                if (!Directory.Exists(outputFolder))
                    return "";

                string[] pdfFiles =
                    Directory.GetFiles(
                        outputFolder,
                        "*.pdf",
                        SearchOption.AllDirectories);

                if (pdfFiles.Length == 0)
                    return "";

                var state =
                    pdfFiles
                    .OrderBy(x => x)
                    .Select(file =>
                    {
                        FileInfo fi = new FileInfo(file);

                        return file.ToUpperInvariant()
                            + "|"
                            + fi.Length.ToString()
                            + "|"
                            + fi.LastWriteTimeUtc.Ticks.ToString();
                    });

                return string.Join(
                    ";",
                    state.ToArray());
            }
            catch
            {
                return "";
            }
        }

        private bool WaitForPdfGenerationCompletion(
    string outputFolder,
    string stateBefore,
    int timeoutMilliseconds)
        {
            try
            {
                Stopwatch stopwatch =
                    Stopwatch.StartNew();

                bool generationDetected = false;

                string lastState = stateBefore;

                int stableCount = 0;

                while (stopwatch.ElapsedMilliseconds <
                       timeoutMilliseconds)
                {
                    Application.DoEvents();

                    string currentState =
                        GetPdfFolderState(outputFolder);

                    // Something has changed:
                    // new PDF / overwritten PDF / PDF size changed
                    if (!string.IsNullOrEmpty(currentState) &&
                        currentState != stateBefore)
                    {
                        generationDetected = true;
                    }

                    if (generationDetected)
                    {
                        if (currentState == lastState)
                        {
                            stableCount++;
                        }
                        else
                        {
                            // Generation is still changing files
                            stableCount = 0;
                        }

                        /*
                         * 5 checks × 400 ms = approx. 2 seconds
                         * with no PDF changes.
                         */
                        if (stableCount >= 5)
                        {
                            // Final safety check:
                            // make sure PDF files are no longer
                            // being written/locked.
                            if (ArePdfFilesReady(outputFolder))
                            {
                                stopwatch.Stop();
                                return true;
                            }
                        }
                    }

                    lastState = currentState;

                    System.Threading.Thread.Sleep(400);
                }

                stopwatch.Stop();

                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool ArePdfFilesReady(
    string outputFolder)
        {
            try
            {
                string[] pdfFiles =
                    Directory.GetFiles(
                        outputFolder,
                        "*.pdf",
                        SearchOption.AllDirectories);

                if (pdfFiles.Length == 0)
                    return false;

                foreach (string file in pdfFiles)
                {
                    try
                    {
                        using (FileStream stream =
                            new FileStream(
                                file,
                                FileMode.Open,
                                FileAccess.Read,
                                FileShare.None))
                        {
                            if (stream.Length <= 0)
                                return false;
                        }
                    }
                    catch
                    {
                        // TRACES is probably still writing the PDF
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region TRACES Utility Mouse Automation

        [System.Runtime.InteropServices.StructLayout(
            System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [System.Runtime.InteropServices.StructLayout(
            System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }


        // ---------------------------------------------------------
        // WINDOWS API
        // ---------------------------------------------------------

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool GetClientRect(
            IntPtr hWnd,
            out RECT lpRect);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ClientToScreen(
            IntPtr hWnd,
            ref POINT lpPoint);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetCursorPos(
            int X,
            int Y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern void mouse_event(
            uint dwFlags,
            uint dx,
            uint dy,
            uint dwData,
            UIntPtr dwExtraInfo);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(
            IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(
            IntPtr hWnd,
            int nCmdShow);


        // ---------------------------------------------------------
        // CONSTANTS
        // ---------------------------------------------------------

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        private const int SW_RESTORE = 9;


        // ---------------------------------------------------------
        // CLICK GET STARTED
        // ---------------------------------------------------------

        private bool ClickGetStarted(IntPtr hWnd)
        {
            try
            {
                if (hWnd == IntPtr.Zero)
                    return false;

                // Bring TRACES application to foreground
                ShowWindow(hWnd, SW_RESTORE);
                SetForegroundWindow(hWnd);

                System.Threading.Thread.Sleep(500);

                // Get actual Flutter client-area size
                RECT rect;

                if (!GetClientRect(hWnd, out rect))
                    return false;

                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;

                if (width <= 0 || height <= 0)
                    return false;

                /*
                 * TRACES PDF Generation Utility - Version 2.1
                 *
                 * Based on its opening screen:
                 *
                 * Get Started centre approximately:
                 *
                 * X = 5.7% of client width
                 * Y = 45.5% of client height
                 *
                 * We intentionally use percentage/relative
                 * coordinates instead of fixed screen coordinates.
                 */

                POINT clickPoint = new POINT
                {
                    X = (int)(width * 0.057),
                    Y = (int)(height * 0.455)
                };

                // Convert from Flutter client coordinates
                // to Windows screen coordinates
                if (!ClientToScreen(hWnd, ref clickPoint))
                    return false;

                // Move mouse
                if (!SetCursorPos(clickPoint.X, clickPoint.Y))
                    return false;

                System.Threading.Thread.Sleep(250);

                // Mouse down
                mouse_event(
                    MOUSEEVENTF_LEFTDOWN,
                    0,
                    0,
                    0,
                    UIntPtr.Zero);

                System.Threading.Thread.Sleep(100);

                // Mouse up
                mouse_event(
                    MOUSEEVENTF_LEFTUP,
                    0,
                    0,
                    0,
                    UIntPtr.Zero);

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region PDF Generation Screen Automation

        private bool FillPdfGenerationInputs(
            IntPtr hWnd,
            string inputZipFilePath,
            string password,
            string outputFolder,
            bool signed)
        {
            try
            {
                ShowWindow(hWnd, SW_RESTORE);
                SetForegroundWindow(hWnd);

                System.Threading.Thread.Sleep(500);

                // ---------------------------------------------------------
                // 1. SELECT FORM TYPE
                // ---------------------------------------------------------
                if (!SelectFormType(hWnd, inputZipFilePath))
                {
                    cmnService.J_UserMessage(
                        "Unable to select Form Type.");

                    return false;
                }

                System.Threading.Thread.Sleep(500);

                // ---------------------------------------------------------
                // 2. SELECT ZIP FILE
                // ---------------------------------------------------------
                if (!SelectZipFile(hWnd, inputZipFilePath))
                {
                    cmnService.J_UserMessage(
                        "Unable to select ZIP file.");

                    return false;
                }

                System.Threading.Thread.Sleep(1200);

                // ---------------------------------------------------------
                // 3. ENTER PASSWORD
                // ---------------------------------------------------------
                if (!EnterZipPassword(hWnd, password))
                {
                    cmnService.J_UserMessage(
                        "Unable to enter ZIP password.");

                    return false;
                }

                System.Threading.Thread.Sleep(500);

                // ---------------------------------------------------------
                // 4. SELECT OUTPUT FOLDER
                // ---------------------------------------------------------
                if (!SelectOutputFolder(
                        hWnd,
                        outputFolder))
                {
                    cmnService.J_UserMessage(
                        "Unable to select Output Folder.");

                    return false;
                }

                System.Threading.Thread.Sleep(1000);


                // ---------------------------------------------------------
                // 5. WITHOUT DIGITAL SIGNATURE
                // ---------------------------------------------------------
                //if (!SelectWithoutDigitalSignature(hWnd))
                //{
                //    cmnService.J_UserMessage(
                //        "Unable to select PDF WITHOUT Digital Signature.");

                //    return false;
                //}
                if (!SelectDigitalSignatureOption(
                    hWnd,
                    signed))
                {
                    cmnService.J_UserMessage(
                        "Unable to select Digital Signature option.");

                    return false;
                }
                System.Threading.Thread.Sleep(700);


                // ---------------------------------------------------------
                // 6. PROCEED
                // ---------------------------------------------------------
                if (!ClickProceed(hWnd))
                {
                    cmnService.J_UserMessage(
                        "Unable to click Proceed.");

                    return false;
                }

                return true;

                return true;
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage(
                    "Error while filling TRACES PDF Generation screen:\n\n"
                    + ex.Message);

                return false;
            }
        }

        private bool SelectDigitalSignatureOption(
    IntPtr hWnd,
    bool signed)
        {
            try
            {
                IntPtr flutterView =
                    FindFlutterViewWindow(hWnd);

                if (flutterView == IntPtr.Zero)
                    return false;

                SetForegroundWindow(hWnd);

                System.Threading.Thread.Sleep(400);

                if (signed)
                {
                    // ---------------------------------------------
                    // Generate PDF WITH Digital Signature
                    // ---------------------------------------------
                    if (!PhysicalClickFlutterRelative(
                            flutterView,
                            0.50,
                            0.49))
                    {
                        return false;
                    }
                }
                else
                {
                    // ---------------------------------------------
                    // Generate PDF WITHOUT Digital Signature
                    // ---------------------------------------------
                    if (!PhysicalClickFlutterRelative(
                            flutterView,
                            0.50,
                            0.61))
                    {
                        return false;
                    }
                }

                System.Threading.Thread.Sleep(700);

                return true;
            }
            catch
            {
                return false;
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        private const byte VK_CAPITAL = 0x14;
        private bool IsCapsLockOn()
        {
            return (GetKeyState(VK_CAPITAL) & 0x0001) != 0;
        }

        private void ToggleCapsLock()
        {
            const uint KEYUP = 0x0002;

            keybd_event(
                VK_CAPITAL,
                0,
                0,
                UIntPtr.Zero);

            System.Threading.Thread.Sleep(50);

            keybd_event(
                VK_CAPITAL,
                0,
                KEYUP,
                UIntPtr.Zero);

            System.Threading.Thread.Sleep(100);
        }


        private bool SelectFormType(
            IntPtr hWnd,
            string zipFilePath)
        {
            try
            {
                // Click Form Type dropdown
                if (!ClickRelative(hWnd, 0.50, 0.325))
                    return false;

                System.Threading.Thread.Sleep(400);

                /*
                 * Present assumption based on TRACES utility:
                 *
                 * 1 = Form 131
                 * 2 = Form 133
                 * 3 = TBR
                 *
                 * We determine it from the downloaded ZIP name.
                 */

                string fileName =
                    Path.GetFileName(zipFilePath).ToUpperInvariant();

                // Go to first option
                SendKeys.SendWait("{HOME}");

                System.Threading.Thread.Sleep(150);

                if (fileName.Contains("FORM133"))
                {
                    SendKeys.SendWait("{DOWN}");
                }
                else if (fileName.Contains("TBR"))
                {
                    SendKeys.SendWait("{DOWN}");
                    SendKeys.SendWait("{DOWN}");
                }
                else
                {
                    // Default = FORM131
                    // No DOWN required
                }

                SendKeys.SendWait("{ENTER}");

                return true;
            }
            catch
            {
                return false;
            }
        }


        private bool SelectZipFile(
            IntPtr hWnd,
            string zipFilePath)
        {
            try
            {
                SetForegroundWindow(hWnd);

                // Click Browse File / upload area
                if (!ClickRelative(hWnd, 0.50, 0.468))
                    return false;

                System.Threading.Thread.Sleep(800);

                /*
                 * Native Windows Open dialog should now be active.
                 *
                 * ALT+N normally focuses "File name".
                 */
                SendKeys.SendWait("%n");

                System.Threading.Thread.Sleep(200);

                PasteText(zipFilePath);

                System.Threading.Thread.Sleep(200);

                SendKeys.SendWait("{ENTER}");

                System.Threading.Thread.Sleep(900);

                return true;
            }
            catch
            {
                return false;
            }
        }


        private bool EnterZipPassword(
    IntPtr hWnd,
    string password)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);

                IntPtr flutterView =
                    FindFlutterViewWindow(hWnd);

                if (flutterView == IntPtr.Zero)
                {
                    cmnService.J_UserMessage(
                        "Flutter View window could not be detected.");

                    return false;
                }

                ShowWindow(hWnd, SW_RESTORE);
                SetForegroundWindow(hWnd);

                System.Threading.Thread.Sleep(300);

                RECT rect;

                if (!GetClientRect(flutterView, out rect))
                    return false;

                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;

                // -----------------------------------------------------
                // SCROLL DOWN
                // -----------------------------------------------------
                POINT centre = new POINT
                {
                    X = width / 2,
                    Y = height / 2
                };

                if (!ClientToScreen(flutterView, ref centre))
                    return false;

                SetCursorPos(centre.X, centre.Y);

                for (int i = 0; i < 5; i++)
                {
                    mouse_event(
                        MOUSEEVENTF_WHEEL,
                        0,
                        0,
                        unchecked((uint)-120),
                        UIntPtr.Zero);

                    System.Threading.Thread.Sleep(80);
                }

                System.Threading.Thread.Sleep(700);

                // -----------------------------------------------------
                // PASSWORD FIELD
                //
                // Browse Location = approx Y 0.31
                // Password        = approx Y 0.18
                // -----------------------------------------------------
                POINT passwordPoint = new POINT
                {
                    X = (int)(width * 0.50),
                    Y = (int)(height * 0.18)
                };

                if (!ClientToScreen(
                        flutterView,
                        ref passwordPoint))
                {
                    return false;
                }

                // -----------------------------------------------------
                // PHYSICALLY MOVE MOUSE TO PASSWORD FIELD
                // -----------------------------------------------------
                SetCursorPos(
                    passwordPoint.X,
                    passwordPoint.Y);

                System.Threading.Thread.Sleep(500);

                // -----------------------------------------------------
                // PHYSICAL LEFT CLICK
                // -----------------------------------------------------
                mouse_event(
                    MOUSEEVENTF_LEFTDOWN,
                    0,
                    0,
                    0,
                    UIntPtr.Zero);

                System.Threading.Thread.Sleep(100);

                mouse_event(
                    MOUSEEVENTF_LEFTUP,
                    0,
                    0,
                    0,
                    UIntPtr.Zero);

                System.Threading.Thread.Sleep(500);

                // -----------------------------------------------------
                // PASSWORD FIELD NOW HAS THE CARET
                // -----------------------------------------------------
                if (!TypeTextPhysical(password))
                {
                    cmnService.J_UserMessage(
                        "Unable to type password into TRACES utility.");

                    return false;
                }

                System.Threading.Thread.Sleep(1000);

                return true;
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage(
                    "Password automation error: " +
                    ex.Message);

                return false;
            }
        }

        private bool PhysicalClickFlutterRelative(
    IntPtr flutterView,
    double relativeX,
    double relativeY)
        {
            try
            {
                RECT rect;

                if (!GetClientRect(flutterView, out rect))
                    return false;

                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;

                POINT point = new POINT
                {
                    X = (int)(width * relativeX),
                    Y = (int)(height * relativeY)
                };

                if (!ClientToScreen(flutterView, ref point))
                    return false;

                SetCursorPos(point.X, point.Y);

                System.Threading.Thread.Sleep(300);

                mouse_event(
                    MOUSEEVENTF_LEFTDOWN,
                    0,
                    0,
                    0,
                    UIntPtr.Zero);

                System.Threading.Thread.Sleep(100);

                mouse_event(
                    MOUSEEVENTF_LEFTUP,
                    0,
                    0,
                    0,
                    UIntPtr.Zero);

                System.Threading.Thread.Sleep(300);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void PressCtrlLPhysical()
        {
            const byte VK_CONTROL = 0x11;
            const byte VK_L = 0x4C;
            const uint KEYUP = 0x0002;

            keybd_event(VK_CONTROL, 0, 0, UIntPtr.Zero);
            System.Threading.Thread.Sleep(70);

            keybd_event(VK_L, 0, 0, UIntPtr.Zero);
            System.Threading.Thread.Sleep(70);

            keybd_event(VK_L, 0, KEYUP, UIntPtr.Zero);
            System.Threading.Thread.Sleep(70);

            keybd_event(VK_CONTROL, 0, KEYUP, UIntPtr.Zero);
        }
        private void PressEnterPhysical()
        {
            const byte VK_RETURN = 0x0D;
            const uint KEYUP = 0x0002;

            keybd_event(
                VK_RETURN,
                0,
                0,
                UIntPtr.Zero);

            System.Threading.Thread.Sleep(100);

            keybd_event(
                VK_RETURN,
                0,
                KEYUP,
                UIntPtr.Zero);
        }

        private bool TypeTextPhysical(string text)
        {
            bool capsLockWasOn = false;

            try
            {
                if (string.IsNullOrEmpty(text))
                    return true;

                // -------------------------------------------------
                // IMPORTANT:
                // VkKeyScan assumes normal keyboard state.
                // Force CAPS LOCK OFF while typing.
                // -------------------------------------------------
                capsLockWasOn = IsCapsLockOn();

                if (capsLockWasOn)
                {
                    ToggleCapsLock();
                    System.Threading.Thread.Sleep(150);
                }

                const byte VK_SHIFT = 0x10;
                const byte VK_CONTROL = 0x11;
                const byte VK_MENU = 0x12;

                const uint KEYEVENTF_KEYUP = 0x0002;

                foreach (char ch in text)
                {
                    short vkInfo = VkKeyScan(ch);

                    if (vkInfo == -1)
                        return false;

                    byte virtualKey =
                        (byte)(vkInfo & 0xFF);

                    byte shiftState =
                        (byte)((vkInfo >> 8) & 0xFF);

                    bool useShift =
                        (shiftState & 1) != 0;

                    bool useCtrl =
                        (shiftState & 2) != 0;

                    bool useAlt =
                        (shiftState & 4) != 0;

                    // Modifier keys DOWN
                    if (useShift)
                        keybd_event(VK_SHIFT, 0, 0, UIntPtr.Zero);

                    if (useCtrl)
                        keybd_event(VK_CONTROL, 0, 0, UIntPtr.Zero);

                    if (useAlt)
                        keybd_event(VK_MENU, 0, 0, UIntPtr.Zero);

                    System.Threading.Thread.Sleep(20);

                    // Character DOWN
                    keybd_event(
                        virtualKey,
                        0,
                        0,
                        UIntPtr.Zero);

                    System.Threading.Thread.Sleep(40);

                    // Character UP
                    keybd_event(
                        virtualKey,
                        0,
                        KEYEVENTF_KEYUP,
                        UIntPtr.Zero);

                    System.Threading.Thread.Sleep(20);

                    // Modifier keys UP
                    if (useAlt)
                        keybd_event(
                            VK_MENU,
                            0,
                            KEYEVENTF_KEYUP,
                            UIntPtr.Zero);

                    if (useCtrl)
                        keybd_event(
                            VK_CONTROL,
                            0,
                            KEYEVENTF_KEYUP,
                            UIntPtr.Zero);

                    if (useShift)
                        keybd_event(
                            VK_SHIFT,
                            0,
                            KEYEVENTF_KEYUP,
                            UIntPtr.Zero);

                    System.Threading.Thread.Sleep(40);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "TypeTextPhysical Error: " +
                    ex.Message);

                return false;
            }
            finally
            {
                // -------------------------------------------------
                // RESTORE USER'S ORIGINAL CAPS LOCK STATE
                // -------------------------------------------------
                if (capsLockWasOn && !IsCapsLockOn())
                {
                    ToggleCapsLock();
                }
            }
        }


        [System.Runtime.InteropServices.DllImport(
    "user32.dll",
    CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern short VkKeyScan(char ch);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern void keybd_event(
    byte bVk,
    byte bScan,
    uint dwFlags,
    UIntPtr dwExtraInfo);

        private void PressCtrlVPhysical()
        {
            const byte VK_CONTROL_PHYSICAL = 0x11;
            const byte VK_V_PHYSICAL = 0x56;
            const uint KEYEVENTF_KEYUP_PHYSICAL = 0x0002;

            // CTRL DOWN
            keybd_event(
                VK_CONTROL_PHYSICAL,
                0,
                0,
                UIntPtr.Zero);

            System.Threading.Thread.Sleep(100);

            // V DOWN
            keybd_event(
                VK_V_PHYSICAL,
                0,
                0,
                UIntPtr.Zero);

            System.Threading.Thread.Sleep(100);

            // V UP
            keybd_event(
                VK_V_PHYSICAL,
                0,
                KEYEVENTF_KEYUP_PHYSICAL,
                UIntPtr.Zero);

            System.Threading.Thread.Sleep(100);

            // CTRL UP
            keybd_event(
                VK_CONTROL_PHYSICAL,
                0,
                KEYEVENTF_KEYUP_PHYSICAL,
                UIntPtr.Zero);
        }


        private bool SendCtrlV()
        {
            const ushort VK_CONTROL = 0x11;
            const ushort VK_V = 0x56;

            INPUT[] inputs = new INPUT[4];

            // CTRL DOWN
            inputs[0].type = INPUT_KEYBOARD;
            inputs[0].U.ki.wVk = VK_CONTROL;

            // V DOWN
            inputs[1].type = INPUT_KEYBOARD;
            inputs[1].U.ki.wVk = VK_V;

            // V UP
            inputs[2].type = INPUT_KEYBOARD;
            inputs[2].U.ki.wVk = VK_V;
            inputs[2].U.ki.dwFlags = KEYEVENTF_KEYUP;

            // CTRL UP
            inputs[3].type = INPUT_KEYBOARD;
            inputs[3].U.ki.wVk = VK_CONTROL;
            inputs[3].U.ki.dwFlags = KEYEVENTF_KEYUP;

            int inputSize =
                System.Runtime.InteropServices.Marshal.SizeOf(
                    typeof(INPUT));

            uint sent = SendInput(
                (uint)inputs.Length,
                inputs,
                inputSize);

            if (sent != inputs.Length)
            {
                int error =
                    System.Runtime.InteropServices.Marshal.GetLastWin32Error();

                Debug.WriteLine(
                    "SendInput failed. Sent = " +
                    sent +
                    ", Size = " +
                    inputSize +
                    ", Error = " +
                    error);

                return false;
            }

            return true;
        }

        private void SendShiftTab()
        {
            const ushort VK_SHIFT = 0x10;
            const ushort VK_TAB = 0x09;

            INPUT[] inputs = new INPUT[4];

            // SHIFT DOWN
            inputs[0].type = INPUT_KEYBOARD;
            inputs[0].U.ki.wVk = VK_SHIFT;
            inputs[0].U.ki.dwFlags = 0;

            // TAB DOWN
            inputs[1].type = INPUT_KEYBOARD;
            inputs[1].U.ki.wVk = VK_TAB;
            inputs[1].U.ki.dwFlags = 0;

            // TAB UP
            inputs[2].type = INPUT_KEYBOARD;
            inputs[2].U.ki.wVk = VK_TAB;
            inputs[2].U.ki.dwFlags = KEYEVENTF_KEYUP;

            // SHIFT UP
            inputs[3].type = INPUT_KEYBOARD;
            inputs[3].U.ki.wVk = VK_SHIFT;
            inputs[3].U.ki.dwFlags = KEYEVENTF_KEYUP;

            SendInput(
                (uint)inputs.Length,
                inputs,
                System.Runtime.InteropServices.Marshal.SizeOf(
                    typeof(INPUT)));
        }

        private bool ClickFlutterChildRelative(
    IntPtr flutterView,
    double relativeX,
    double relativeY)
        {
            try
            {
                RECT rect;

                if (!GetClientRect(
                        flutterView,
                        out rect))
                {
                    return false;
                }

                int width =
                    rect.Right - rect.Left;

                int height =
                    rect.Bottom - rect.Top;

                int x =
                    (int)(width * relativeX);

                int y =
                    (int)(height * relativeY);

                int lParam =
                    (y << 16) |
                    (x & 0xFFFF);

                // Mouse move
                SendMessage(
                    flutterView,
                    WM_MOUSEMOVE,
                    IntPtr.Zero,
                    new IntPtr(lParam));

                System.Threading.Thread.Sleep(100);

                // Left Down
                SendMessage(
                    flutterView,
                    WM_LBUTTONDOWN,
                    new IntPtr(MK_LBUTTON),
                    new IntPtr(lParam));

                System.Threading.Thread.Sleep(100);

                // Left Up
                SendMessage(
                    flutterView,
                    WM_LBUTTONUP,
                    IntPtr.Zero,
                    new IntPtr(lParam));

                return true;
            }
            catch
            {
                return false;
            }
        }
        private void SendTextToFlutterView(
    IntPtr flutterView,
    string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            foreach (char ch in text)
            {
                SendMessage(
                    flutterView,
                    WM_CHAR,
                    new IntPtr(ch),
                    IntPtr.Zero);

                System.Threading.Thread.Sleep(30);
            }
        }


        private const ushort VK_TAB = 0x09;

        private void SendVirtualKey(ushort virtualKey)
        {
            INPUT[] inputs = new INPUT[2];

            // Key Down
            inputs[0].type = INPUT_KEYBOARD;
            inputs[0].U.ki.wVk = virtualKey;
            inputs[0].U.ki.wScan = 0;
            inputs[0].U.ki.dwFlags = 0;

            // Key Up
            inputs[1].type = INPUT_KEYBOARD;
            inputs[1].U.ki.wVk = virtualKey;
            inputs[1].U.ki.wScan = 0;
            inputs[1].U.ki.dwFlags = KEYEVENTF_KEYUP;

            SendInput(
                (uint)inputs.Length,
                inputs,
                System.Runtime.InteropServices.Marshal.SizeOf(
                    typeof(INPUT)));
        }

        private void ClickFlutterWindow(
    IntPtr hWnd,
    int x,
    int y)
        {
            int lParam =
                (y << 16) |
                (x & 0xFFFF);

            // Mouse move
            SendMessage(
                hWnd,
                WM_MOUSEMOVE,
                IntPtr.Zero,
                new IntPtr(lParam));

            System.Threading.Thread.Sleep(100);

            // Left button down
            SendMessage(
                hWnd,
                WM_LBUTTONDOWN,
                new IntPtr(MK_LBUTTON),
                new IntPtr(lParam));

            System.Threading.Thread.Sleep(100);

            // Left button up
            SendMessage(
                hWnd,
                WM_LBUTTONUP,
                IntPtr.Zero,
                new IntPtr(lParam));
        }
        
        [System.Runtime.InteropServices.DllImport(
    "user32.dll")]
        private static extern IntPtr SendMessage(
    IntPtr hWnd,
    uint Msg,
    IntPtr wParam,
    IntPtr lParam);

        private const uint WM_CHAR = 0x0102;

        private const uint WM_MOUSEMOVE = 0x0200;
        private const uint WM_LBUTTONDOWN = 0x0201;
        private const uint WM_LBUTTONUP = 0x0202;

        private const int MK_LBUTTON = 0x0001;

        private void ScrollFlutterToTop(IntPtr hWnd)
        {
            try
            {
                RECT rect;

                if (!GetClientRect(hWnd, out rect))
                    return;

                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;

                POINT point = new POINT
                {
                    X = width / 2,
                    Y = height / 2
                };

                ClientToScreen(hWnd, ref point);

                SetCursorPos(point.X, point.Y);

                System.Threading.Thread.Sleep(100);

                // Scroll UP several times
                for (int i = 0; i < 10; i++)
                {
                    mouse_event(
                        MOUSEEVENTF_WHEEL,
                        0,
                        0,
                        120,
                        UIntPtr.Zero);

                    System.Threading.Thread.Sleep(30);
                }
            }
            catch
            {
            }
        }

        private const uint MOUSEEVENTF_WHEEL = 0x0800;

        [System.Runtime.InteropServices.StructLayout(
    System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public InputUnion U;
        }

        [System.Runtime.InteropServices.StructLayout(
            System.Runtime.InteropServices.LayoutKind.Explicit)]
        private struct InputUnion
        {
            [System.Runtime.InteropServices.FieldOffset(0)]
            public MOUSEINPUT mi;

            [System.Runtime.InteropServices.FieldOffset(0)]
            public KEYBDINPUT ki;

            [System.Runtime.InteropServices.FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        [System.Runtime.InteropServices.StructLayout(
            System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [System.Runtime.InteropServices.StructLayout(
            System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [System.Runtime.InteropServices.StructLayout(
            System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }



        [System.Runtime.InteropServices.DllImport(
            "user32.dll",
            SetLastError = true)]
        private static extern uint SendInput(
            uint nInputs,
            INPUT[] pInputs,
            int cbSize);

        private const uint INPUT_KEYBOARD = 1;

        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint KEYEVENTF_UNICODE = 0x0004;

        private const ushort VK_CONTROL = 0x11;
        private const ushort VK_A = 0x41;

        private void SendCtrlA()
        {
            INPUT[] inputs = new INPUT[4];

            // CTRL down
            inputs[0].type = INPUT_KEYBOARD;
            inputs[0].U.ki.wVk = VK_CONTROL;

            // A down
            inputs[1].type = INPUT_KEYBOARD;
            inputs[1].U.ki.wVk = VK_A;

            // A up
            inputs[2].type = INPUT_KEYBOARD;
            inputs[2].U.ki.wVk = VK_A;
            inputs[2].U.ki.dwFlags = KEYEVENTF_KEYUP;

            // CTRL up
            inputs[3].type = INPUT_KEYBOARD;
            inputs[3].U.ki.wVk = VK_CONTROL;
            inputs[3].U.ki.dwFlags = KEYEVENTF_KEYUP;

            SendInput(
                (uint)inputs.Length,
                inputs,
                System.Runtime.InteropServices.Marshal.SizeOf(
                    typeof(INPUT)));
        }

        private void SendUnicodeText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            foreach (char ch in text)
            {
                INPUT[] inputs = new INPUT[2];

                // Character DOWN
                inputs[0].type = INPUT_KEYBOARD;
                inputs[0].U.ki.wVk = 0;
                inputs[0].U.ki.wScan = ch;
                inputs[0].U.ki.dwFlags =
                    KEYEVENTF_UNICODE;

                // Character UP
                inputs[1].type = INPUT_KEYBOARD;
                inputs[1].U.ki.wVk = 0;
                inputs[1].U.ki.wScan = ch;
                inputs[1].U.ki.dwFlags =
                    KEYEVENTF_UNICODE |
                    KEYEVENTF_KEYUP;

                SendInput(
                    2,
                    inputs,
                    System.Runtime.InteropServices.Marshal.SizeOf(
                        typeof(INPUT)));
            }
        }


        private bool SelectOutputFolder(
    IntPtr hWnd,
    string outputFolder)
        {
            try
            {
                IntPtr flutterView =
                    FindFlutterViewWindow(hWnd);

                if (flutterView == IntPtr.Zero)
                {
                    cmnService.J_UserMessage(
                        "Flutter View window could not be detected.");

                    return false;
                }

                ShowWindow(hWnd, SW_RESTORE);
                SetForegroundWindow(hWnd);

                System.Threading.Thread.Sleep(500);

                // -----------------------------------------------------
                // CLICK OUTPUT FOLDER - BROWSE
                //
                // Current scrolled screen:
                // X ~ 62%
                // Y ~ 29%
                // -----------------------------------------------------
                if (!PhysicalClickFlutterRelative(
                        flutterView,
                        0.62,
                        0.29))
                {
                    return false;
                }

                // Wait for native Windows Folder dialog
                System.Threading.Thread.Sleep(1200);

                // -----------------------------------------------------
                // ADDRESS BAR
                // -----------------------------------------------------
                PressCtrlLPhysical();

                System.Threading.Thread.Sleep(300);

                // Type folder path physically
                if (!TypeTextPhysical(outputFolder))
                {
                    cmnService.J_UserMessage(
                        "Unable to enter Output Folder path.");

                    return false;
                }

                System.Threading.Thread.Sleep(300);

                // Navigate to folder
                PressEnterPhysical();

                System.Threading.Thread.Sleep(1000);

                // -----------------------------------------------------
                // Try to click the native "Select Folder" button
                // -----------------------------------------------------
                if (!ClickNativeFolderSelectButton())
                {
                    cmnService.J_UserMessage(
                        "Unable to confirm Output Folder.");

                    return false;
                }

                System.Threading.Thread.Sleep(1200);

                return true;
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage(
                    "Output Folder automation error: " +
                    ex.Message);

                return false;
            }
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool EnumWindows(
    EnumWindowsProc lpEnumFunc,
    IntPtr lParam);

        [System.Runtime.InteropServices.DllImport(
    "user32.dll",
    CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int GetWindowText(
    IntPtr hWnd,
    StringBuilder lpString,
    int nMaxCount);

        private const uint BM_CLICK = 0x00F5;

        private bool ClickNativeFolderSelectButton()
        {
            try
            {
                // Give Windows folder dialog time to settle
                for (int attempt = 0; attempt < 20; attempt++)
                {
                    IntPtr dialogHandle = GetForegroundWindow();

                    if (dialogHandle != IntPtr.Zero)
                    {
                        try
                        {
                            AutomationElement dialog =
                                AutomationElement.FromHandle(dialogHandle);

                            if (dialog != null)
                            {
                                string[] possibleButtonNames =
                                {
                            "Select Folder",
                            "Select folder",
                            "Select",
                            "OK"
                        };

                                foreach (string buttonName in possibleButtonNames)
                                {
                                    Condition condition =
                                        new AndCondition(
                                            new PropertyCondition(
                                                AutomationElement.ControlTypeProperty,
                                                ControlType.Button),
                                            new PropertyCondition(
                                                AutomationElement.NameProperty,
                                                buttonName));

                                    AutomationElement button =
                                        dialog.FindFirst(
                                            TreeScope.Descendants,
                                            condition);

                                    if (button != null)
                                    {
                                        object pattern;

                                        if (button.TryGetCurrentPattern(
                                            InvokePattern.Pattern,
                                            out pattern))
                                        {
                                            ((InvokePattern)pattern).Invoke();

                                            System.Threading.Thread.Sleep(700);

                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(
                                "Folder dialog automation: " +
                                ex.Message);
                        }
                    }

                    System.Threading.Thread.Sleep(200);
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "ClickNativeFolderSelectButton Error: " +
                    ex.Message);

                return false;
            }
        }


        #region CloseTracesApplication

        private bool CloseTracesApplication(Process process)
        {
            try
            {
                if (process == null)
                    return true;

                process.Refresh();

                if (process.HasExited)
                    return true;

                // Close ONLY the exact TRACES process started by TDSMAN
                process.Kill();

                process.WaitForExit(3000);

                return process.HasExited;
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage(
                    "Unable to close TRACES PDF Generation Utility.\n\n" +
                    ex.Message);

                return false;
            }
        }

        #endregion

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private bool SelectWithoutDigitalSignature(
    IntPtr hWnd)
        {
            try
            {
                IntPtr flutterView =
                    FindFlutterViewWindow(hWnd);

                if (flutterView == IntPtr.Zero)
                    return false;

                SetForegroundWindow(hWnd);

                System.Threading.Thread.Sleep(400);

                /*
                 * Current screen:
                 *
                 * "Generate PDF WITHOUT Digital Signature"
                 *
                 * X ~ 50%
                 * Y ~ 61%
                 */

                if (!PhysicalClickFlutterRelative(
                        flutterView,
                        0.50,
                        0.61))
                {
                    return false;
                }

                System.Threading.Thread.Sleep(700);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ClickProceed(
    IntPtr hWnd)
        {
            try
            {
                IntPtr flutterView =
                    FindFlutterViewWindow(hWnd);

                if (flutterView == IntPtr.Zero)
                    return false;

                SetForegroundWindow(hWnd);

                System.Threading.Thread.Sleep(400);

                /*
                 * Proceed button
                 *
                 * X ~ 58%
                 * Y ~ 75%
                 */

                if (!PhysicalClickFlutterRelative(
                        flutterView,
                        0.58,
                        0.75))
                {
                    return false;
                }

                System.Threading.Thread.Sleep(1200);

                return true;
            }
            catch
            {
                return false;
            }
        }



        private bool ClickRelative(
            IntPtr hWnd,
            double relativeX,
            double relativeY)
        {
            try
            {
                if (hWnd == IntPtr.Zero)
                    return false;

                RECT rect;

                if (!GetClientRect(hWnd, out rect))
                    return false;

                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;

                if (width <= 0 || height <= 0)
                    return false;

                POINT point = new POINT
                {
                    X = (int)(width * relativeX),
                    Y = (int)(height * relativeY)
                };

                if (!ClientToScreen(hWnd, ref point))
                    return false;

                SetForegroundWindow(hWnd);

                System.Threading.Thread.Sleep(100);

                if (!SetCursorPos(point.X, point.Y))
                    return false;

                System.Threading.Thread.Sleep(150);

                mouse_event(
                    MOUSEEVENTF_LEFTDOWN,
                    0,
                    0,
                    0,
                    UIntPtr.Zero);

                System.Threading.Thread.Sleep(80);

                mouse_event(
                    MOUSEEVENTF_LEFTUP,
                    0,
                    0,
                    0,
                    UIntPtr.Zero);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void TrnCertificateExtraction_26_27_Activated(object sender, EventArgs e)
        {
            //txtZIPFilePath.Text = @"D:\CLIENT'S PRBLM\RB\20260827\Form131_File.ZIP";
            //txtPassword.Text = "CALP08143C";
            //txtOutputFolder.Text = @"C:\Users\ANIK-PC\Desktop\daal do\20260827";
        }

        private void PasteText(string text)
        {
            if (text == null)
                text = "";

            Clipboard.SetText(text);

            System.Threading.Thread.Sleep(100);

            SendKeys.SendWait("^v");

            System.Threading.Thread.Sleep(100);
        }

        #endregion

        private Dictionary<string, string> GetPdfSnapshot(
    string outputFolder)
        {
            Dictionary<string, string> snapshot =
                new Dictionary<string, string>(
                    StringComparer.OrdinalIgnoreCase);

            try
            {
                if (!Directory.Exists(outputFolder))
                    return snapshot;

                string[] files =
                    Directory.GetFiles(
                        outputFolder,
                        "*.pdf",
                        SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);

                    snapshot[file] =
                        fi.Length.ToString() +
                        "|" +
                        fi.LastWriteTimeUtc.Ticks.ToString();
                }
            }
            catch
            {
            }

            return snapshot;
        }

        private bool WaitForNewPdfGeneration(
    string outputFolder,
    Dictionary<string, string> beforeSnapshot,
    int timeoutMinutes)
        {
            try
            {
                DateTime timeout =
                    DateTime.Now.AddMinutes(timeoutMinutes);

                string previousSignature = "";
                int stableChecks = 0;
                bool newPdfDetected = false;

                while (DateTime.Now < timeout)
                {
                    Application.DoEvents();

                    string[] pdfFiles =
                        Directory.Exists(outputFolder)
                            ? Directory.GetFiles(
                                outputFolder,
                                "*.pdf",
                                SearchOption.AllDirectories)
                            : new string[0];

                    if (pdfFiles.Length > 0)
                    {
                        List<string> currentState =
                            new List<string>();

                        foreach (string file in pdfFiles)
                        {
                            try
                            {
                                FileInfo fi =
                                    new FileInfo(file);

                                string fileState =
                                    fi.Length.ToString() +
                                    "|" +
                                    fi.LastWriteTimeUtc.Ticks.ToString();

                                currentState.Add(
                                    file.ToUpperInvariant() +
                                    "|" +
                                    fileState);

                                string oldState;

                                if (!beforeSnapshot.TryGetValue(
                                        file,
                                        out oldState))
                                {
                                    // Brand-new PDF
                                    newPdfDetected = true;
                                }
                                else if (oldState != fileState)
                                {
                                    // Existing PDF overwritten/updated
                                    newPdfDetected = true;
                                }
                            }
                            catch
                            {
                                // File may still be being written
                            }
                        }

                        currentState.Sort();

                        string currentSignature =
                            string.Join(";", currentState.ToArray());

                        if (newPdfDetected)
                        {
                            if (currentSignature ==
                                previousSignature)
                            {
                                stableChecks++;
                            }
                            else
                            {
                                stableChecks = 0;
                                previousSignature =
                                    currentSignature;
                            }

                            /*
                             * 4 stable checks × 500 ms
                             * = approx. 2 seconds without changes.
                             */
                            if (stableChecks >= 4)
                            {
                                return true;
                            }
                        }
                    }

                    System.Threading.Thread.Sleep(500);
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private void CloseTracesUtility(Process process)
        {
            try
            {
                if (process == null)
                    return;

                process.Refresh();

                if (process.HasExited)
                    return;

                // First try normal close
                process.CloseMainWindow();

                if (!process.WaitForExit(3000))
                {
                    // If Flutter refuses to close, terminate it
                    try
                    {
                        process.Kill();
                        process.WaitForExit(2000);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }


        private delegate bool EnumWindowsProc(
    IntPtr hWnd,
    IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool EnumChildWindows(
            IntPtr hWndParent,
            EnumWindowsProc lpEnumFunc,
            IntPtr lParam);

        [System.Runtime.InteropServices.DllImport(
            "user32.dll",
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int GetClassName(
            IntPtr hWnd,
            StringBuilder lpClassName,
            int nMaxCount);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(
            IntPtr hWnd,
            out uint lpdwProcessId);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool AttachThreadInput(
            uint idAttach,
            uint idAttachTo,
            bool fAttach);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SetFocus(
            IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool IsWindowVisible(
            IntPtr hWnd);

        private IntPtr FindFlutterViewWindow(IntPtr mainWindowHandle)
        {
            IntPtr flutterView = IntPtr.Zero;

            EnumChildWindows(
                mainWindowHandle,
                delegate (IntPtr childHandle, IntPtr lParam)
                {
                    try
                    {
                        if (!IsWindowVisible(childHandle))
                            return true;

                        StringBuilder className =
                            new StringBuilder(256);

                        GetClassName(
                            childHandle,
                            className,
                            className.Capacity);

                        string cls = className.ToString();

                        Debug.WriteLine(
                            "TRACES CHILD WINDOW: " +
                            cls +
                            " HWND=" +
                            childHandle.ToString());

                // Flutter Windows normally contains
                // Flutter / FlutterView in the child class.
                if (cls.IndexOf(
                                "FLUTTER",
                                StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            flutterView = childHandle;

                    // Stop enumeration
                    return false;
                        }
                    }
                    catch
                    {
                    }

                    return true;
                },
                IntPtr.Zero);

            return flutterView;
        }

        private bool ForceFocusToFlutter(
    IntPtr mainWindowHandle,
    IntPtr flutterViewHandle)
        {
            if (mainWindowHandle == IntPtr.Zero ||
                flutterViewHandle == IntPtr.Zero)
            {
                return false;
            }

            try
            {
                uint processId;

                uint targetThread =
                    GetWindowThreadProcessId(
                        flutterViewHandle,
                        out processId);

                uint currentThread =
                    GetCurrentThreadId();

                bool attached = false;

                try
                {
                    if (currentThread != targetThread)
                    {
                        attached =
                            AttachThreadInput(
                                currentThread,
                                targetThread,
                                true);
                    }

                    ShowWindow(
                        mainWindowHandle,
                        SW_RESTORE);

                    SetForegroundWindow(
                        mainWindowHandle);

                    System.Threading.Thread.Sleep(200);

                    IntPtr focused =
                        SetFocus(flutterViewHandle);

                    System.Threading.Thread.Sleep(300);

                    return focused != IntPtr.Zero;
                }
                finally
                {
                    if (attached)
                    {
                        AttachThreadInput(
                            currentThread,
                            targetThread,
                            false);
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        //########################################

    }

}
