
#region Importing Namespace

extern alias global2;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Security.AccessControl;
using System.IO;
using System.Threading;
//
using System.Security.AccessControl;

using iTextSharp.text.pdf;
// This namespace are using for Combo Box
using Microsoft.VisualBasic.Compatibility.VB6;

// User Namespaces
using TDSMAN.Classes;

//-- DIGITAL SIGNATURE
using System.Security.Cryptography.X509Certificates;
using TDSMAN.FormRpt;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnMergeForm16PartAPartBCertificates : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnMergeForm16PartAPartBCertificates()
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
        // Declaration of objects
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        ReportService rptService = null;
        RptDialog rptDialog = new RptDialog();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();

        string strSQL = string.Empty;
        string strQueryString = string.Empty;
        string strQueryStringGroup = string.Empty;
        //
        string strLastWorkedFinancialYear = "";
        int intLastWorkedFinancialYearID = 0;
        int intLastWorkedCompanyID = 0;
        string strLastWorkedCompanyName = "";
        string strLastWorkedFormNo = "";
        string strLastWorkedQtr = "";
        string strOutputPDFPath = "";
        string strGeneratePDFPath = "";
        string strFormNoSelected = "";
        long lngBasicInfoID = 0;
        #endregion

        #region EVENT HANDLER OF CONTROLS

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

        #region TrnViewReturnStatusOnline_Load
        private void TrnMergeForm16PartAPartBCertificates_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //--
            //intLastWorkedFinancialYearID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT ASST_ID FROM TRN_LAST_WORKED")));
            //strLastWorkedFinancialYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FA_YEAR FROM MST_ASSESSMENT WHERE ASST_ID = " + intLastWorkedFinancialYearID));
            //intLastWorkedCompanyID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_ID FROM TRN_LAST_WORKED")));
            //strLastWorkedCompanyName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_NAME FROM MST_COMPANY WHERE COMPANY_ID = " + intLastWorkedCompanyID));
            //strLastWorkedFormNo = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM TRN_LAST_WORKED"));
            //strLastWorkedQtr = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT QTR FROM TRN_LAST_WORKED"));
            //--
            lblTitle.Text = "Form16 - Merge Part A and Part B";
            //--
            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION
                || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION
                || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION
                || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE //--2022/04/11
                || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)
                chkUseDigitalSignature.Visible = true;
            else
                chkUseDigitalSignature.Visible = false;
            //-- FINANCIAL YEAR
            strSQL = "SELECT ASST_ID," +
                                 "       FA_YEAR " +
                                 "FROM   MST_ASSESSMENT " +
                                 "WHERE  VISIBILITY_FLAG = 0 " +
                                 "ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFAYear) == false) return;
            //cmbFAyear.Text = strLastWorkedFinancialYear;
            //-----------------------------------------
            //-- COMPANY
            strSQL = " SELECT COMPANY_ID," +
                     "        COMPANY_NAME + ' [' + TAN_NO + ']' " +
                     " FROM   MST_COMPANY " +
                     " ORDER BY COMPANY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //cmbCompany.Text = strLastWorkedCompanyName;
            //
            cmbFAYear.Select();

        }
        #endregion        

        #region btnCertificatePathA_Click
        private void btnCertificatePathA_Click(object sender, EventArgs e)
        {
            int intCountA = 0;
            string strEmployeeName = "";
            try
            {
                string strPDFFolder = cmnService.J_OpenFolderDialog("Select Destination Folder");
                txtCertificatePathA.Text = strPDFFolder;
                //
                //if (ValidateFields() == false)
                //{
                //    txtCertificatePathA.Text = string.Empty;
                //    return;
                //}
                //
                if (txtCertificatePathA.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("PDF file path - can not be Blank");
                    btnCertificatePathA.Select();
                    //return;
                }
                //--
                this.Cursor = Cursors.WaitCursor;
                //
                CREATE_TEMP_TABLES();
                //--
                if (txtCertificatePathA.Text.Trim() != "")
                {
                    DirectoryInfo diPath = new DirectoryInfo(txtCertificatePathA.Text);
                    //
                    int i = 0;
                    //
                    if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE) == true)
                    {
                        foreach (var file in diPath.GetFiles("*.pdf"))
                        {
                            strEmployeeName = "";
                            if (file.Name.Length >= 10)
                            {
                                if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(cmnService.J_Left(file.Name, 10), 5), J_DataType.Character) == false)
                                {
                                    goto startloop;
                                }
                                //---------------------------
                                if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(cmnService.J_Left(file.Name, 10), 5, 4), J_DataType.Numeric) == false)
                                {
                                    goto startloop;
                                }
                                //---------------------------
                                if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(cmnService.J_Left(file.Name, 10), 1), J_DataType.Character) == false)
                                {
                                    goto startloop;
                                }
                                //
                                if (lngBasicInfoID > 0)
                                {
                                    strEmployeeName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MST_EMPLOYEE.EMPLOYEE_NAME FROM MST_EMPLOYEE, TRN_SALARY_DETAILS, TRN_BASIC_INFO WHERE MST_EMPLOYEE.EMPLOYEE_ID = TRN_SALARY_DETAILS.EMPLOYEE_ID AND MST_EMPLOYEE.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID AND MST_EMPLOYEE.EMPLOYEE_PAN = '" + cmnService.J_Left(file.Name, 10) + "' AND TRN_SALARY_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID));
                                }
                                //
                                if (strEmployeeName == "")
                                    strEmployeeName = "- NA -";
                                //
                                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE + " (CERTIFICATE_PAN, EMPLOYEE_NAME) VALUES ('" + cmnService.J_Left(file.Name, 10) + "','" + cmnService.J_ReplaceQuote(strEmployeeName) + "')";
                                dmlService.J_ExecSql(strSQL);
                                //
                            }
                            startloop:
                            //
                            i++;
                        }
                        //--
                        //-- COUNT --
                        strSQL = "SELECT COUNT(*) AS TOTAL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE;
                        lblPartACount.Text = "Count :  " + Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                        lblPartACount.Visible = true;
                    }
                    else
                        return;
                    //-------------
                    //--
                    if (i == 0)
                    {
                        cmnService.J_UserMessage("The folder does not have any pdf files !!");
                        //return;
                    }
                    //
                }
                //string[,] strMatrixEmployee1 = {{"CERTIFICATE_PAN_ID", "0", "", "Right", "", "", ""},
                //                                {"PAN", "400", "S", "", "", "", ""},
                //                                {"CERTIFICATE_PATH", "0", "0", "", "", "", ""}};

                strSQL = @"SELECT CERTIFICATE_PAN AS PAN, 
                                  EMPLOYEE_NAME   AS NAME
                           FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE + @" 
                           ORDER BY CERTIFICATE_PAN ";

                //TdsMan.PopulateGridView(grdvPartA, dmlService.J_pCommand, strSQL, strMatrixEmployee);
                //dmlService.J_ShowDataInGrid(ref grdvPartA, strSQL, strMatrixEmployee1);
                DataSet dsPartA = dmlService.J_ExecSqlReturnDataSet(strSQL);
                grdvPartA.DataSource = dsPartA.Tables[0];
                grdvPartA.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                grdvPartA.Columns["PAN"].ReadOnly = true;
                grdvPartA.Columns["PAN"].Width = 100;
                grdvPartA.Columns["NAME"].ReadOnly = true;
                //--
                this.Cursor = Cursors.Default;
                //Grid_Status();
            }
            catch (Exception err_handler)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region  btnCertificatePathB_Click
        private void btnCertificatePathB_Click(object sender, EventArgs e)
        {
            string strEmployeeName = "";
            try
            {
                string strPDFFolder = cmnService.J_OpenFolderDialog("Select Destination Folder");
                txtCertificatePathB.Text = strPDFFolder;
                //
                //if (ValidateFields() == false)
                //{
                //    txtCertificatePathB.Text = string.Empty;
                //    return;
                //}
                //
                if (txtCertificatePathB.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("PDF file path - can not be Blank");
                    btnCertificatePathB.Select();
                    //return;
                }
                //--
                this.Cursor = Cursors.WaitCursor;
                //--
                CREATE_TEMP_TABLES();
                //--
                if (txtCertificatePathB.Text.Trim() != "")
                {
                    DirectoryInfo diPath = new DirectoryInfo(txtCertificatePathB.Text);
                    //
                    int i = 0;
                    //
                    if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE) == true)
                    {
                        foreach (var file in diPath.GetFiles("*.pdf"))
                        {
                            strEmployeeName = "";
                            if (file.Name.Length >= 10)
                            {
                                if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(cmnService.J_Left(file.Name, 10), 5), J_DataType.Character) == false)
                                {
                                    goto startloop;
                                }
                                //---------------------------
                                if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(cmnService.J_Left(file.Name, 10), 5, 4), J_DataType.Numeric) == false)
                                {
                                    goto startloop;
                                }
                                //---------------------------
                                if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(cmnService.J_Left(file.Name, 10), 1), J_DataType.Character) == false)
                                {
                                    goto startloop;
                                }
                                //
                                if (lngBasicInfoID > 0)
                                {
                                    strEmployeeName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MST_EMPLOYEE.EMPLOYEE_NAME FROM MST_EMPLOYEE, TRN_SALARY_DETAILS, TRN_BASIC_INFO WHERE MST_EMPLOYEE.EMPLOYEE_ID = TRN_SALARY_DETAILS.EMPLOYEE_ID AND MST_EMPLOYEE.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID AND MST_EMPLOYEE.EMPLOYEE_PAN = '" + cmnService.J_Left(file.Name, 10) + "' AND TRN_SALARY_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID));
                                }
                                //
                                if (strEmployeeName == "")
                                    strEmployeeName = "- NA -";
                                //
                                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE + " (CERTIFICATE_PAN, EMPLOYEE_NAME) VALUES ('" + cmnService.J_Left(file.Name, 10) + "','" + cmnService.J_ReplaceQuote(strEmployeeName) + "')";
                                dmlService.J_ExecSql(strSQL);
                            }
                            startloop:
                            //
                            i++;
                        }
                        //--
                        //-- COUNT --
                        strSQL = "SELECT COUNT(*) AS TOTAL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE;
                        lblPartBCount.Text = "Count :  " + Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                        lblPartBCount.Visible = true;
                    }
                    else
                        return;
                    //-------------
                    if (i == 0)
                    {
                        cmnService.J_UserMessage("The folder does not have any pdf files !!");
                        //return;
                    }
                }
                //
                //string[,] strMatrixEmployee2 = {{"CERTIFICATE_PAN_ID", "0", "", "Right", "", "", ""},
                //                                {"PAN DETAILS", "400", "S", "", "", "", ""},
                //                                {"CERTIFICATE_PATH", "0", "0", "", "", "", ""}};

                //strSQL = @"SELECT CERTIFICATE_PAN_ID,
                //                              CERTIFICATE_PAN,
                //                              CERTIFICATE_PATH
                //                   FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE + @" 
                //                   ORDER BY CERTIFICATE_PAN ";

                strSQL = @"SELECT CERTIFICATE_PAN AS PAN, 
                                  EMPLOYEE_NAME   AS NAME
                           FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE + @" 
                           ORDER BY CERTIFICATE_PAN ";

                DataSet dsPartB = dmlService.J_ExecSqlReturnDataSet(strSQL);
                grdvPartB.DataSource = dsPartB.Tables[0];
                grdvPartB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                grdvPartB.Columns["PAN"].ReadOnly = true;
                grdvPartB.Columns["PAN"].Width = 100;
                grdvPartB.Columns["NAME"].ReadOnly = true;
                //--
                this.Cursor = Cursors.Default;
                //Grid_Status();
            }
            catch (Exception err_handler)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region cmb_SelectedIndexChanged
        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFAYear.SelectedIndex <= 0)
            {
                lngBasicInfoID = 0;
                return;
            }
            if (cmbCompany.SelectedIndex <= 0)
            {
                lngBasicInfoID = 0;
                return;
            }
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)),
                                                        T_Qtr.Q4,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        T_FormNo.F24Q);

        }
        #endregion

        #region chkUseDigitalSignature_CheckedChanged
        private void chkUseDigitalSignature_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseDigitalSignature.Checked == true)
                grpExportToPDF.Visible = true;
            else
                grpExportToPDF.Visible = false;
            //--
            txtSignaturePath.Text = "";
            txtSignaturePassword.Text = "";
        }
        #endregion

        #region btnOutputFolderPath_Click
        private void btnOutputFolderPath_Click(object sender, EventArgs e)
        {
            string strPDFFolder = cmnService.J_OpenFolderDialog("Select Destination Folder");
            txtOutputFolderPath.Text = strPDFFolder;
        }
        #endregion

        #region btnSignaturePath_Click
        private void btnSignaturePath_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Certificate files *.pfx|*.pfx";
                openFile.Title = "Select a file";
                if (openFile.ShowDialog() != DialogResult.OK)
                    return;

                txtSignaturePath.Text = openFile.FileName;
            }
            catch (Exception Err)
            {
                cmnService.J_UserMessage(Err.Message);
            }
        }
        #endregion

        #region btnVerification_Click
        private void btnVerification_Click(object sender, EventArgs e)
        {
            string[] strListofPdfFiles = new string[3];
            //
            if (ValidateFields() == false)
            {
                return;
            }                    
            //
            try
            {
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //--
                if (!string.IsNullOrEmpty(txtCertificatePathA.Text.Trim()) && !string.IsNullOrEmpty(txtCertificatePathB.Text.Trim()) && !string.IsNullOrEmpty(txtOutputFolderPath.Text.Trim()))
                {
                    //--
                    //DirectoryInfo diPathPartB = new DirectoryInfo(txtCertificatePathB.Text);
                    //foreach (var file in diPathPartB.GetFiles("*.pdf"))
                    //{

                    //}
                    //--
                    DirectoryInfo diPath = new DirectoryInfo(txtCertificatePathA.Text);
                    //
                    int i = 0;
                    string strPan;
                    //
                    // VALIDATE DIGITAL SIGNATURE (P12/PFX file)
                    if (chkUseDigitalSignature.Checked == true && rbnPFX.Checked == true)
                    {
                        if (TdsMan.VERIFY_DIGITAL_SIGNATURE_PASSOWRD(txtSignaturePath.Text.Trim(), txtSignaturePassword.Text.Trim()) == false)
                        {
                            cmnService.J_UserMessage("Wrong digital signature password");
                            txtSignaturePassword.Select();
                            return;
                        }
                    }
                    //-----------------------------------------------------
                    // CREATE NEW DIRECTRY
                    //-----------------------------------------------------
                    string[] strCompanyTAN = cmbCompany.Text.Split('[', ']');
                    strOutputPDFPath = Path.Combine(strCompanyTAN[1] + "_" + cmbFAYear.Text, "Form16");
                    strGeneratePDFPath = Path.Combine(txtOutputFolderPath.Text, strOutputPDFPath);
                    //
                    if (Directory.Exists(strGeneratePDFPath) == true)
                    {
                        if (cmnService.J_UserMessage("A folder with same name already exists\nDo you want to replace it ?", MessageBoxButtons.YesNo) == DialogResult.No) return;
                        //-- DELETE THE FOLDER
                        Directory.Delete(strGeneratePDFPath, true);
                    }
                    else
                    {
                        //--CREATING DIRECTORY [WHILE EXPORT TO PDF ONLY WITHOUT SIGNATURE]
                        Directory.CreateDirectory(strGeneratePDFPath);
                    }
                    //
                    if (Directory.Exists(strGeneratePDFPath) == false)
                    {
                        Directory.CreateDirectory(strGeneratePDFPath); 
                    }
                    //
                    //--
                    //-----------------------------
                    // DIGITAL SIGNATURE
                    //-----------------------------
                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule("Everyone", System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.InheritanceFlags.ContainerInherit | System.Security.AccessControl.InheritanceFlags.ObjectInherit, System.Security.AccessControl.PropagationFlags.None, System.Security.AccessControl.AccessControlType.Allow));
                    //
                    global2.PDFSign PDFSign;
                    global2.PDFSign PDFSign2;
                    //--
                    //--------------------------------------------
                    // VALIDATE DIGITAL SIGNATURE
                    //--------------------------------------------
                    if (chkUseDigitalSignature.Checked == true)
                    {
                        PDFSign = ReturnDSC(txtSignaturePath.Text.Trim(), txtSignaturePassword.Text.Trim());
                        //--
                        if (PDFSign == null) return;
                    }
                    else
                    {
                        PDFSign = null;
                    }
                    //--
                    int iSuccess = 0;
                    int iNotMerged = 0;
                    //--
                    foreach (var file in diPath.GetFiles("*.pdf"))
                    {
                        strPan = string.Empty;
                        //--
                        if (file.Name.Length >= 10)
                            strPan = cmnService.J_Left(file.Name, 10);
                        //--
                        if (Directory.Exists(txtCertificatePathA.Text) == true && Directory.Exists(txtCertificatePathB.Text) == true)
                        {
                            string[] files = System.IO.Directory.GetFiles(txtCertificatePathB.Text, strPan + "*.pdf", System.IO.SearchOption.TopDirectoryOnly);
                            //-- WHEN MATCHED
                            #region WHEN MATCHED
                            if (files.Length > 0)
                            {
                                strListofPdfFiles = string.Concat(file.FullName + ",", files[0]).Split(',');
                                rptDialog.MergePdfFiles(Path.Combine(strGeneratePDFPath.Trim(), strPan) + ".pdf", strListofPdfFiles);
                                //
                                iSuccess = iSuccess + 1;
                                //
                                if (chkUseDigitalSignature.Checked == true)
                                {                                    
                                    //-------------------------------------------------------------------------------------------
                                    //-- DIGITAL SIGNATURE WILL ATTACH WITH [MERGED PDF IF EXISTS OR WITH THE GENERATED PDF]
                                    //-------------------------------------------------------------------------------------------
                                    PdfReader pdfReader = new PdfReader(Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"));
                                    int numberOfPages = pdfReader.NumberOfPages;
                                    //-------------------------------------------------------------------------------------------
                                    if (Create_Signature(PDFSign,
                                                            Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"),
                                                            Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"),
                                                            txtSignaturePassword.Text,
                                                            400,
                                                            10,
                                                            100,
                                                            50,
                                                            numberOfPages) == false)
                                    {
                                        File.Delete(Path.Combine(strGeneratePDFPath.Trim(), strPan) + ".pdf");
                                        txtSignaturePath.Select();
                                        return;
                                    }
                                }
                                else
                                    PDFSign = null;
                            }
                            #endregion
                            //-- WHEN NOT MATCHED
                            #region WHEN NOT MATCHED
                            else
                            {
                                iNotMerged = iNotMerged + 1;
                                File.Copy(Path.Combine(txtCertificatePathA.Text, file.Name), Path.Combine(strGeneratePDFPath.Trim(), strPan) + ".pdf");
                                //--
                                if (chkUseDigitalSignature.Checked == true)
                                {
                                    //-------------------------------------------------------------------------------------------
                                    //-- DIGITAL SIGNATURE WILL ATTACH WITH [MERGED PDF IF EXISTS OR WITH THE GENERATED PDF]
                                    //-------------------------------------------------------------------------------------------
                                    PdfReader pdfReader = new PdfReader(Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"));
                                    int numberOfPages = pdfReader.NumberOfPages;
                                    //-------------------------------------------------------------------------------------------
                                    if (Create_Signature(PDFSign,
                                                            Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"),
                                                            Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"),
                                                            txtSignaturePassword.Text,
                                                            400,
                                                            10,
                                                            100,
                                                            50,
                                                            numberOfPages) == false)
                                    {
                                        File.Delete(Path.Combine(strGeneratePDFPath.Trim(), strPan) + ".pdf");
                                        txtSignaturePath.Select();
                                        return;
                                    }
                                }
                                else
                                    PDFSign = null;
                            }
                            #endregion
                            //
                            #region COMMENT
                            //else
                            //{
                            //    //
                            //    if (chkUseDigitalSignature.Checked == true)
                            //    {
                            //        //-------------------------------------------------------------------------------------------
                            //        //-- DIGITAL SIGNATURE WILL ATTACH WITH [MERGED PDF IF EXISTS OR WITH THE GENERATED PDF]
                            //        //-------------------------------------------------------------------------------------------
                            //        PdfReader pdfReader = new PdfReader(Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"));
                            //        int numberOfPages = pdfReader.NumberOfPages;
                            //        //-------------------------------------------------------------------------------------------
                            //        if (Create_Signature(PDFSign,
                            //                                Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"),
                            //                                Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"),
                            //                                txtSignaturePassword.Text,
                            //                                400,
                            //                                10,
                            //                                100,
                            //                                50,
                            //                                numberOfPages) == false)
                            //        {
                            //            File.Delete(Path.Combine(strGeneratePDFPath.Trim(), strPan) + ".pdf");
                            //            txtSignaturePath.Select();
                            //            return;
                            //        }
                            //    }
                            //    else
                            //        PDFSign = null;
                            //}
                            #endregion
                        }
                    }
                    //###### 2021/01/08 (WHEN PART FILES OF PART B NOT MATCHED WITH PART A)
                    if (chkUseDigitalSignature.Checked == true)
                    {
                        PDFSign2 = ReturnDSC(txtSignaturePath.Text.Trim(), txtSignaturePassword.Text.Trim());
                        //--
                        if (PDFSign2 == null) return;
                    }
                    else
                    {
                        PDFSign2 = null;
                    }
                    DirectoryInfo diPartB = new DirectoryInfo(txtCertificatePathB.Text);
                    foreach (var file in diPartB.GetFiles("*.pdf"))
                    {
                        strPan = string.Empty;
                        if(file.Name.Length >= 10)
                            strPan = cmnService.J_Left(file.Name, 10);
                        //
                        string[] files = System.IO.Directory.GetFiles(txtCertificatePathA.Text, strPan + "*.pdf", System.IO.SearchOption.TopDirectoryOnly);

                        #region WHEN NOT MATCHED
                        if (files.Length <= 0)
                        {
                            iNotMerged = iNotMerged + 1;
                            File.Copy(Path.Combine(txtCertificatePathB.Text, file.Name), Path.Combine(strGeneratePDFPath.Trim(), strPan) + ".pdf");
                            //--
                            if (chkUseDigitalSignature.Checked == true)
                            {
                                //-------------------------------------------------------------------------------------------
                                //-- DIGITAL SIGNATURE WILL ATTACH WITH [MERGED PDF IF EXISTS OR WITH THE GENERATED PDF]
                                //-------------------------------------------------------------------------------------------
                                PdfReader pdfReader = new PdfReader(Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"));
                                int numberOfPages = pdfReader.NumberOfPages;
                                //-------------------------------------------------------------------------------------------
                                if (Create_Signature(PDFSign2,
                                                        Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"),
                                                        Path.Combine(strGeneratePDFPath.Trim(), strPan + ".pdf"),
                                                        txtSignaturePassword.Text,
                                                        400,
                                                        10,
                                                        100,
                                                        50,
                                                        numberOfPages) == false)
                                {
                                    File.Delete(Path.Combine(strGeneratePDFPath.Trim(), strPan) + ".pdf");
                                    txtSignaturePath.Select();
                                    return;
                                }
                            }
                            else
                                PDFSign2 = null;
                        }
                        #endregion
                    }
                    //################# 
                    // END OF FOR LOOP
                    if (iSuccess + iNotMerged > 0)
                    {
                        //cmnService.J_UserMessage("Successfully Merged...\nFile(s) : " + iSuccess.ToString());
                        if(iNotMerged ==0 & iSuccess>0)
                            cmnService.J_UserMessage("Successfully Merged...\nFile(s) : " + iSuccess.ToString());
                        else if (iNotMerged > 0 & iSuccess == 0)
                            cmnService.J_UserMessage("File(s) Merged : " + iSuccess.ToString() + "\nFile(s) Copied : " + iNotMerged.ToString());
                        else if (iNotMerged > 0 & iSuccess > 0)
                            cmnService.J_UserMessage("Successfully Merged...\nFile(s) Merged : " + iSuccess.ToString() + "\nFile(s) Copied  : " + iNotMerged.ToString());
                        //
                        System.Diagnostics.Process.Start("explorer.exe", strGeneratePDFPath);
                        //
                        this.Close();
                    }
                    else
                        cmnService.J_UserMessage("No Files Merged...");
                }
                else
                {
                    if (string.IsNullOrEmpty(txtCertificatePathA.Text.Trim()))
                    {
                        cmnService.J_UserMessage("Select the path of PART A");
                        return;
                    }
                    //
                    if (string.IsNullOrEmpty(txtCertificatePathB.Text.Trim()))
                    {
                        cmnService.J_UserMessage("Select the path of PART B");
                        return;
                    }
                    //
                    if (string.IsNullOrEmpty(txtOutputFolderPath.Text.Trim()))
                    {
                        cmnService.J_UserMessage("Select the output folder path");
                        return;
                    }
                    
                }
            }
            catch (Exception err_handler)
            {
                //PDFSign = null;
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion


        #region rbnPFX_CheckedChanged
        private void rbnPFX_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnPFX.Checked == true)
            {
                txtSignaturePath.Visible = true;
                txtSignaturePassword.Visible = true;
                btnSignaturePath.Visible = true;
                lblSignaturePath.Visible = true;
                lblPassword.Visible = true;
            }
            else
            {
                txtSignaturePath.Visible = false;
                txtSignaturePassword.Visible = false;
                btnSignaturePath.Visible = false;
                lblSignaturePath.Visible = false;
                lblPassword.Visible = false;
            }
        }
        #endregion

        #region rbnWindows_CheckedChanged
        private void rbnWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnWindows.Checked == true)
            {
                txtSignaturePath.Visible = false;
                txtSignaturePassword.Visible = false;
                btnSignaturePath.Visible = false;
                lblSignaturePath.Visible = false;
                lblPassword.Visible = false;
            }
            else
            {
                txtSignaturePath.Visible = true;
                txtSignaturePassword.Visible = true;
                btnSignaturePath.Visible = true;
                lblSignaturePath.Visible = true;
                lblPassword.Visible = true;
            }
        }
        #endregion

        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        #endregion


        #region User Define Functions

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                //-----------------------------------------------------------------------
                //-- FA YEAR
                //-----------------------------------------------------------------------
                if (cmbFAYear.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("FA Year - Cannot be Blank");
                    cmbFAYear.Select();
                    return false;
                }
                //-----------------------------------------------------------------------
                //-- COMPANY 
                //-----------------------------------------------------------------------
                if (cmbCompany.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Company - Cannot be Blank");
                    cmbCompany.Select();
                    return false;
                }
                //
                if (string.IsNullOrEmpty(txtCertificatePathA.Text.Trim()))
                {
                    cmnService.J_UserMessage("Select the path of PART A");
                    btnCertificatePathA.Select();
                    return false;
                }
                //
                if (string.IsNullOrEmpty(txtCertificatePathB.Text.Trim()))
                {
                    cmnService.J_UserMessage("Select the path of PART B");
                    btnCertificatePathB.Select();
                    return false;
                }
                //
                if (string.IsNullOrEmpty(txtOutputFolderPath.Text.Trim()))
                {
                    cmnService.J_UserMessage("Select the output folder path");
                    btnOutputFolderPath.Select();
                    return false;
                }
                //
                if (grdvPartA.RowCount <= 0)
                {
                    cmnService.J_UserMessage("No Part A file(s) found !!");
                    btnCertificatePathA.Select();
                    return false;
                }
                //
                if (grdvPartB.RowCount <= 0)
                {
                    cmnService.J_UserMessage("No Part B file(s) found !!");
                    btnCertificatePathB.Select();
                    return false;
                }
                //-----------------------------------------------------------------------
                //-- DSC 
                //-----------------------------------------------------------------------
                if (chkUseDigitalSignature.Checked==true)
                {
                    if (rbnPFX.Checked == false && rbnWindows.Checked == false)
                    {
                        cmnService.J_UserMessage("Select Digital Signature type");
                        rbnPFX.Select();
                        return false;
                    }
                    //
                    if (rbnPFX.Checked == true)
                    {
                        if(txtSignaturePath.Text == "")
                        {
                            cmnService.J_UserMessage("Signature Path - Cannot be Blank");
                            txtSignaturePath.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_IsFileExist(txtSignaturePath.Text) == false)
                        {
                            cmnService.J_UserMessage("Signature Path - File does not exist");
                            txtSignaturePath.Select();
                            return false;
                        }
                        //
                        if (txtSignaturePassword.Text == "")
                        {
                            cmnService.J_UserMessage("Signature Path - Cannot be Blank");
                            txtSignaturePassword.Select();
                            return false;
                        }
                    }
                }
                //---
                return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region CREATE_TEMP_TABLES
        private bool CREATE_TEMP_TABLES()
        {
            try
            {
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE + "";
                    dmlService.J_ExecSql(strSQL);
                        //return false;
                }
                //--
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE + "") == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LOAD_PDF_MERGE_CERTIFICATE + " (" +
                         "                  " + cmnService.J_GetDataType("CERTIFICATE_PAN_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("CERTIFICATE_PAN", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 255) + ")";
                    dmlService.J_ExecSql(strSQL);
                        //return false;
                }

                return true;
            }
            catch
            {
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
                        cmnService.J_UserMessage("No Digital Signature found !!");
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
                        cmnService.J_UserMessage("No Digital Signature found in Windows Store !!");
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
                PDFSign.SignerContactInformation = PDFSign.DigitalSignatureCertificate.GetNameInfo(X509NameType.EmailName,false);

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

        #region Grid_Status
        private void Grid_Status()
        {
            try
            {
                foreach(DataRow dr in grdvPartA.Rows)
                {

                }
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0085", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        private void cmbCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnMergeForm16PartAPartBCertificates");
                Tan.ShowDialog();
                //--------------
                if (TDSMAN.Classes.TDSMAN.T_pTAN != "")
                    cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
            }
        }
    }
}
