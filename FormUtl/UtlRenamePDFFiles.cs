
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnFVUImport
Version			: 1.0
Start Date		: 30-12-2010
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

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;


#endregion



namespace TDSMAN.FormUtl
{
    public partial class UtlRenamePDFFiles : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public UtlRenamePDFFiles()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        #region Objects & Variables decleration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        //
        //string strFVUPath = "";
        string strInputPDFFileFolderPath = "";
        string strOututPDFFileFolderPath = "";
        //-----------------------------------------------------------------------
        string OutputFolder;
        //string Filename;
        string OutputFilePath;
        //----------------------------------------------
        string strFileName;
        //----------------------------------------------

        string strImportErrorMessage = "Import Failed.";
        int intMaxChars = 15;
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        #endregion

        #region User Defined Events

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

        #region TrnFVUImport_Load

        private void TrnFVUImport_Load(object sender, EventArgs e)
        {
            //--
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //--
            //-----------
            //lblTitle.Text = "Rename PDF files";
            lblTitle.Text = "Rename PDF of TDS Certificates"; //-- DO ALSO IN MENU OF MDI FORM
            lblNotes1.Text = "The PDF Converter for TDS Certificates generates PDF files for each Deductee/Employee. The file name includes the PAN as an identifier. This utility will create another folder of these PDF files including the Deductee/Employee name (15 chars) within the filename.";
            lblNotes2.Text = "For e.g. \n'AAECP6176D_XXXXXX.pdf' would be 'PDS INFOTECH PR_AAECP6176D_XXXXXX.pdf'";
            //lnkLabel2.Text = "--";
            rbnNameFirst.Checked = true;
            rbnNameFirstLast_CheckedChanged(sender, e);
            ////Added by Shrey Kejriwal on 18/03/2012
            //BtnSave.Enabled = false;
            //BtnSave.BackColor = Color.Silver;

            //
        }

        #endregion

        #region btnSelectInputFilePDFPath_Click
        private void btnSelectInputFilePDFPath_Click(object sender, EventArgs e)
        {
            lblInputPDFFiles.Text = "";
            //--
            strInputPDFFileFolderPath = cmnService.J_OpenFolderDialog("Select Input PDF Folder");
            //--
            txtInputFilePDFPath.Text = strInputPDFFileFolderPath;
            //--
            if (txtInputFilePDFPath.Text == "") return;
            //--
            if (txtInputFilePDFPath.Text !="")
            {
                txtOutputPDFFolderPath.Text = Path.Combine(strInputPDFFileFolderPath, string.Format("{0:yyyyMMdd_HHmmss}",System.DateTime.Now));
            }
            //--
            DirectoryInfo InputFolder = new DirectoryInfo(txtInputFilePDFPath.Text.Trim());//Assuming Test is your Folder
            FileInfo[] InputFiles = InputFolder.GetFiles("*.pdf"); //Getting files
            int i = 0;
            foreach (FileInfo file in InputFiles)
            {
                i++;
            }
            if (i > 0)
            {
                lblInputPDFFiles.ForeColor = Color.Blue;
                lblInputPDFFiles.Text = i.ToString() + " PDF file(s) found...";
            }
            else if (i == 0)
            {
                lblInputPDFFiles.ForeColor = Color.Red;
                lblInputPDFFiles.Text = "No PDF file found...";
            }
            //--
        }
        #endregion

        #region btnSelectOututFilePDFPath_Click
        private void btnSelectOututFilePDFPath_Click(object sender, EventArgs e)
        {
            strOututPDFFileFolderPath = cmnService.J_OpenFolderDialog("Select Output PDF Folder");
            //
            txtOutputPDFFolderPath.Text = strOututPDFFileFolderPath; 
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                prgRenameBar.Value = 0;
                //
                #region VALIDATION CHECKS
                if (txtInputFilePDFPath.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Select PDF file input path");
                    btnSelectInputFilePDFPath.Select();
                    return;
                }
                //--
                if (txtOutputPDFFolderPath.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Select PDF file output path");
                    btnSelectOutputFilePDFPath.Select();
                    return;
                }
                //--
                if (cmnService.J_IsFolderExist(txtInputFilePDFPath.Text.Trim()) == false)
                {
                    cmnService.J_UserMessage("Input path does not exists");
                    btnSelectInputFilePDFPath.Select();
                    return;
                }
                //--
                if (cmnService.J_IsFolderExist(txtOutputPDFFolderPath.Text.Trim()) == false)
                {
                    Directory.CreateDirectory(txtOutputPDFFolderPath.Text.Trim());
                    //cmnService.J_UserMessage("Output path does not exists");
                    //btnSelectOutputFilePDFPath.Select();
                    //return;
                }
                //--
                if (txtInputFilePDFPath.Text.Trim() == txtOutputPDFFolderPath.Text.Trim())
                {
                    cmnService.J_UserMessage("Select a different output folder path");
                    btnSelectOutputFilePDFPath.Select();
                    return;
                }
                //--
                Cursor.Current = Cursors.WaitCursor;
                //--
                DirectoryInfo OutputFolder = new DirectoryInfo(txtOutputPDFFolderPath.Text.Trim());//Assuming Test is your Folder
                FileInfo[] OutputFiles = OutputFolder.GetFiles(); //Getting files
                int i = 0;
                foreach (FileInfo file in OutputFiles)
                {
                    i++;
                }
                if (i > 0)
                {
                    //if (cmnService.J_UserMessage("The output folder is not empty.\nDo you want to continue??", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    //{
                    //    btnSelectOutputFilePDFPath.Select();
                    //    return;
                    //}
                    cmnService.J_UserMessage("The output folder is not empty.");
                    btnSelectOutputFilePDFPath.Select();
                    return;
                }
                #endregion
                //--
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    Directory.Delete(txtOutputPDFFolderPath.Text.Trim());
                    return;
                }
                //--
                DirectoryInfo InputPDFFolder = new DirectoryInfo(txtInputFilePDFPath.Text.Trim());//Assuming Test is your Folder
                FileInfo[] InputPDFFiles = InputPDFFolder.GetFiles("*.pdf"); //Getting pdf files
                //--
                string strPAN = ""; int iTotalFilesCount = 0; int iRenameFilesCount = 0; string strName = ""; string strConvertedFileName = "";
                //--
                foreach (FileInfo file in InputPDFFiles)
                {
                    //--
                    if (prgRenameBar.Value == prgRenameBar.Maximum)
                    {
                    }
                    else
                        prgRenameBar.Value = prgRenameBar.Value + 5;
                    this.Refresh();
                    //--
                    if (file.Name == "PANNOTAVBL" || file.Name == "PANAPPLIED" || file.Name == "PANINVALID")
                    {
                    }
                    else
                    {
                        //--
                        //strPAN = cmnService.J_Left(file.Name, 10);
                        //
                        bool isFirst3Numeric = Regex.IsMatch(file.Name, @"^\d{3}"); 
                        //
                        if (isFirst3Numeric) //-- 2026-27
                            strPAN = cmnService.J_Mid(file.Name,4, 10);
                        else
                            strPAN = cmnService.J_Left(file.Name, 10);
                        //
                        strName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT EMPLOYEE_NAME FROM MST_EMPLOYEE WHERE EMPLOYEE_PAN ='" + cmnService.J_ReplaceQuote(strPAN) + "' ORDER BY EMPLOYEE_ID DESC"));
                        if (strName == "")
                            strName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT DEDUCTEE_NAME FROM MST_DEDUCTEE WHERE DEDUCTEE_PAN ='" + cmnService.J_ReplaceQuote(strPAN) + "' ORDER BY DEDUCTEE_ID DESC"));
                        if (strName == "")
                            strName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT DEDUCTEE_NAME FROM COR_TRN_DEDUCTEE_DETAILS WHERE DEDUCTEE_PAN ='" + cmnService.J_ReplaceQuote(strPAN) + "' ORDER BY TRN_DEDUCTEE_DETAIL_ID DESC"));
                        if (strName == "")
                            strName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT EMPLOYEE_NAME FROM COR_TRN_SALARY_DETAILS WHERE EMPLOYEE_PAN ='" + cmnService.J_ReplaceQuote(strPAN) + "' ORDER BY TRN_SALARY_DETAILS_ID DESC"));
                        //--
                        strName = strName.Replace("/", "");
                        strName = strName.Replace("\\", "");
                        strName = strName.Replace(":", "");
                        strName = strName.Replace("*", "");
                        strName = strName.Replace("?", "");
                        strName = strName.Replace("\"", "");
                        strName = strName.Replace("<", "");
                        strName = strName.Replace(">", "");
                        strName = strName.Replace("|", "");
                        strName = strName.Replace("'", "");
                        //--
                        if (strName != "")
                        {
                            if(strName.Length> intMaxChars)
                                strName = cmnService.J_Left(strName, intMaxChars);
                            //
                            //strName.Replace(" ", "_");
                            //
                            if (rbnNameFirst.Checked == true)
                                strConvertedFileName = strName + "_" + file.Name;
                            else if (rbnNameLast.Checked == true)
                                strConvertedFileName = file.Name.Replace(".pdf", "") + "_" + strName + ".pdf";
                            //--
                            //if (file.Name.Length > 10)
                            //    strConvertedFileName = strConvertedFileName + cmnService.J_Right(file.Name, file.Name.Length - 10);
                            //File.Copy(Path.Combine(txtInputFilePDFPath.Text.Trim(), file.Name), Path.Combine(txtOutputPDFFolderPath.Text.Trim(), strConvertedFileName));
                            bool blYN = true;
                            //if (cmnService.J_IsFileExist(Path.Combine(txtOutputPDFFolderPath.Text.Trim(), strConvertedFileName)) == true)
                            //{
                            //    if (cmnService.J_UserMessage("Same named file [ " + strConvertedFileName + " ] contains in the output folder.\nDo you want to replace??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            //        blYN = false;
                            //}
                            //
                            if (blYN == true)
                                File.Copy(Path.Combine(txtInputFilePDFPath.Text.Trim(), file.Name), Path.Combine(txtOutputPDFFolderPath.Text.Trim(), strConvertedFileName), blYN);
                            //--
                            iRenameFilesCount++;
                            //--
                            lblRenameProgress.Text = iRenameFilesCount + " file(s) renamed...";
                        }
                        else
                        {
                            File.Copy(Path.Combine(txtInputFilePDFPath.Text.Trim(), file.Name), Path.Combine(txtOutputPDFFolderPath.Text.Trim(), file.Name));
                        }
                    }
                    //--
                    iTotalFilesCount++;
                }
                //--                
                if (iTotalFilesCount == 0)
                {
                    //lblRenameProgress.Text = iRenameFilesCount + " file(s) out of " + iTotalFilesCount + " has been renamed...";
                    Directory.Delete(txtOutputPDFFolderPath.Text.Trim());
                    //
                    cmnService.J_UserMessage("Input folder dose not contain PDF file(s)");
                    btnSelectInputFilePDFPath.Select();
                    return;
                }
                else if (iTotalFilesCount > 0)
                    lblRenameProgress.Text = iRenameFilesCount + " file(s) out of " + iTotalFilesCount + " has been renamed...";
                //--
                prgRenameBar.Value = prgRenameBar.Maximum;
                this.Refresh();
                //--
                Cursor.Current = Cursors.Default;
                //--
                if(iTotalFilesCount == iRenameFilesCount) 
                    cmnService.J_UserMessage(iRenameFilesCount + " file(s) renamed out of " + iTotalFilesCount + " file(s).");
                else
                    cmnService.J_UserMessage(iRenameFilesCount + " file(s) renamed out of " + iTotalFilesCount + " file(s).\n" + (iTotalFilesCount - iRenameFilesCount) + " not renamed and copied as it is.");
                //--
                System.Diagnostics.Process.Start(txtOutputPDFFolderPath.Text);
                //
                prgRenameBar.Value = 0;
                txtInputFilePDFPath.Text = "";
                txtOutputPDFFolderPath.Text = "";
                lblRenameProgress.Text = "";
                lblInputPDFFiles.Text ="";
                //
                //--
            }
            catch (Exception ERR)
            {
                Cursor.Current = Cursors.Default;
                cmnService.J_UserMessage(ERR.Message);
            }
        }
        #endregion

        #region rbnNameFirstLast_CheckedChanged
        private void rbnNameFirstLast_CheckedChanged(object sender, EventArgs e)
        {
            txtSampleName.Text = "AAECP6176D_XXXXXXX";
            //--
            if (rbnNameFirst.Checked == true)
            {
                txtSampleName.Text = "PDS INFOTECH PR_" + txtSampleName.Text + ".pdf";
            }
            else if (rbnNameLast.Checked == true)
            {
                txtSampleName.Text = txtSampleName.Text + "_PDS INFOTECH PR.pdf";
            }
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=ij4eM5bAJt0");
        }
        #endregion

        #region pctManual_Click
        private void pctManual_Click(object sender, EventArgs e)
        {
            //M0066
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0134", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        #endregion

        #endregion

        #region User Defined Functions


        #endregion


        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            //tllTipManual.RemoveAll();
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctManual_MouseMove
        private void pctManual_MouseMove(object sender, MouseEventArgs e)
        {
            //tllTipVideoDemo.RemoveAll();
            tllTipManual.SetToolTip(pctManual, pctManual.Tag.ToString());
        }
        #endregion

        private async void BtnGetChallan_Click(object sender, EventArgs e)
        {
            await Task.Run(async () =>
            {
                await FetchDataAsync();// txtITUserID.Text.Trim(), txtITPassword.Text.Trim());
                //fetchCompleted = true; // mark completion
            });
        }

        #region FetchDataAsync
        private async Task FetchDataAsync()//string TAN, string Password)
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
                    var json = "{\"tan\": \"CALP08143C\",  \"password\": \"Pdsinfo@6\",  \"fromdate\": \"2025-08-01\",  \"todate\" : \"2025-09-30\",  \"maxrec\": \"15\"}";
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


    }
}

