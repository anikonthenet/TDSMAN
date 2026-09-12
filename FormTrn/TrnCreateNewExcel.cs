
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

//using Microsoft.Office.Interop.Access;

//using System.Runtime.InteropServices;
using System.Data.OleDb;
//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
//--

using ICSharpCode.SharpZipLib.Zip;
//using Excel = Microsoft.Office.Interop.Excel.Worksheet;
using Excel = Microsoft.Office.Interop.Excel;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnCreateNewExcel : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnCreateNewExcel()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        # region enum FinancialYearSelection
        //private enum FinancialYearSelection
        //{
        //    FY1314Onwards = 1,
        //    uptoFY1213 = 2
        //}
        #endregion

        #region Objects & Variables declaration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        ExcelService ExcelService = new ExcelService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        string strExcelFolder = "";
        string ExcelFileName = "";

        string strSourcePath = Path.Combine(Application.StartupPath, "BLANK EXCEL FILE FORMAT");

        string strSourceFile = "";
        string strDestFile = "";
        //--
        string strSQL = "";
        //--
        string strCompanyMaster = "Company Master";
        string strDeducteeMaster = "Deductee Master";
        string strEmployeeMaster = "Employee Master";
        string strSalaryDetail = "Salary Details";

        bool blnUpdateTextfileName = true;
        string strFormNo = "";
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

        #region TrnExcelImport_Load

        private void TrnExcelImport_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            //-----------
            lblTitle.Text = "Get Blank File Format";
            //
            //-- FORM NO
            //string[] strQuarterWiseDeducteeReportForm ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QForm16SalaryDetails, strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
            //string[] strQuarterWiseDeducteeReportForm ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, strDeducteeMaster };
            //dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            ////-- 2017/01/09
            //if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
            //{
            //    string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QAnnexureIII, strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
            //    dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            //}
            //else
            //{
            //    string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QForm16SalaryDetails, T_FormNo.F24QAnnexureIII, strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
            //    dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            //}
            LoadImportType();
            //---- FILE TYPE
            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
            {
                string[] FileType = { "xlsx" };
                dmlService.J_PopulateComboBox(FileType, ref cmbFileType, 1);
            }
            else
            {
                string[] FileType = { "xls", "xlsx" };
                dmlService.J_PopulateComboBox(FileType, ref cmbFileType, 1);
            }

            // Added by Shrey Kejriwal on 03-07-2013
            // -- FINANCIAL YEAR 
            //string[] FinancialYear = { "Finacial Year 2013-14 onwards", "Upto Financial Year 2012-13" };
            //dmlService.J_PopulateComboBox(FinancialYear, ref cmbFinancialYear, 1);//-- FINANCIAL YEAR
            //-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            //
        }
        #endregion

        #region btnSelectExcelPath_Click
        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
            strExcelFolder = cmnService.J_OpenFolderDialog("Select Destination Folder");

            txtExcelPath.Text = strExcelFolder;

        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            #region VALIDATION
            if (cmbFormNo.Text == "")
            {
                cmnService.J_UserMessage("Select a import type");
                cmbFormNo.Select();
                return;
            }
            //
            if (rbnExcel.Checked == true)
            {
                if (cmbFileType.Text == "")
                {
                    cmnService.J_UserMessage("Select a particular file type");
                    cmbFileType.Select();
                    return;
                }
            }
            //
            if (cmbFinancialYear.Text == "")
            {
                cmnService.J_UserMessage("Select the relevant Financial year");
                cmbFinancialYear.Select();
                return;
            }
            //
            if (txtExcelPath.Text == "")
            {
                cmnService.J_UserMessage("Select Folder to Save the File");
                btnSelectExcelPath.Select();
                return;
            }
            //--
            if (txtDestinationFileName.Text == "")
            {
                cmnService.J_UserMessage("No filename provided.");
                btnSelectExcelPath.Select();
                return;
            }
            #endregion
            //--
            if (Directory.Exists(strSourcePath) == false)
                Directory.CreateDirectory(strSourcePath); // Delete if the file exists.
            //
            //-- 
            #region GET FORM NO.
            strFormNo = "";
            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2026_27ID)
            {
                if (cmbFormNo.Text == T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")")
                    strFormNo = T_FormNo.F24Q;
                else if (cmbFormNo.Text == T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")")
                    strFormNo = T_FormNo.F26Q;
                else if (cmbFormNo.Text == T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")")
                    strFormNo = T_FormNo.F27Q;
                else if (cmbFormNo.Text == T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")")
                    strFormNo = T_FormNo.F27EQ;
                else
                    strFormNo = cmbFormNo.Text;
            }
            else
            {
                strFormNo = cmbFormNo.Text;
            }
            #endregion
            //
            //---Source FILE NAME FOR EXCEL
            if (rbnExcel.Checked == true)
            {
                if (strFormNo == T_FormNo.F24QForm16SalaryDetails)
                {
                    if (TdsMan.T_CheckInternetConnectivty() == false)
                    {
                        cmnService.J_UserMessage("It needs Internet Connectivity (currently absent) to download the Form 16 excel format.");
                        return;
                    }
                    //System.Diagnostics.Process.Start("http://www.tdsman.com/Downloads/24Q-SALARYDETAILS-F16." + cmbFileType.Text);
                    //using (WebClient wc = new WebClient())
                    //    wc.DownloadFile("http://www.tdsman.com/Downloads/24Q-SALARYDETAILS-F16." + cmbFileType.Text, Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text));
                    using (WebClient wc = new WebClient())
                        wc.DownloadFile("http://www.tdsman.com/Downloads/24Q-SD-v1-F16." + cmbFileType.Text, Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text));
                    //--
                    System.Threading.Thread.Sleep(100);
                    //--
                    if (cmnService.J_IsFileExist(Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text)) == true)
                    {
                        cmnService.J_UserMessage("Blank Excel File Created");
                        //--
                        System.Diagnostics.Process.Start(Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text));
                    }
                    else
                        cmnService.J_UserMessage("Blank Excel File Creation Failed");
                    //--
                    txtDestinationFileName.Text = "";
                    //--
                    return;
                }
                else if (strFormNo == strDeducteeMaster)
                    ExcelFileName = cmbFileType.Text.ToUpper() + "_DEDUCTEE_MASTER." + cmbFileType.Text;
                else if (strFormNo == strEmployeeMaster)
                    ExcelFileName = cmbFileType.Text.ToUpper() + "_EMPLOYEE_MASTER(v1)." + cmbFileType.Text;
                else if (strFormNo == strCompanyMaster)
                {
                    //ExcelFileName = cmbFileType.Text.ToUpper() + "_COMPANY_MASTER." + cmbFileType.Text;
                    ExcelFileName = cmbFileType.Text.ToUpper() + "_COMPANY_MASTER(V1)." + cmbFileType.Text;
                }
                else if (strFormNo == T_FormNo.F24QSalaryDetails)
                {
                    #region F24QSalaryDetails
                    //----
                    //COMMENTED BY DHRUB ON 11/12/2013 TO MAKE THE SALARY DETAIL EXCEL F.Y SPECIFIC
                    //----
                    //ExcelFileName = cmbFileType.Text.ToUpper() + "_SALARY_DETAIL." + cmbFileType.Text;

                    //----ADDED BY DHRUB ON 11/12/2013 TO MAKE THE SALARY DETAIL EXCEL F.Y SPECIFIC
                    //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                    //-- 2016/04/22
                    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                    {
                        //-- ANIK 2019/05/12
                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2018_19ID)
                        {
                            if (TdsMan.T_CheckInternetConnectivty() == false)
                            {
                                cmnService.J_UserMessage("It needs Internet Connectivity (currently absent) to download the Form 16 excel format.");
                                return;
                            }
                            //--
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2020_21ID)
                            {
                                using (WebClient wcF2020_21ID = new WebClient())
                                    wcF2020_21ID.DownloadFile("http://www.tdsman.com/Downloads/24Q-SD-v2." + cmbFileType.Text, Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text));

                            }
                            else if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2018_19ID)
                            {
                                using (WebClient wc = new WebClient())
                                    wc.DownloadFile("http://www.tdsman.com/Downloads/24Q-SD-v1." + cmbFileType.Text, Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text));
                            }
                            //--
                            System.Threading.Thread.Sleep(100);
                            //--
                            if (cmnService.J_IsFileExist(Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text)) == true)
                            {
                                cmnService.J_UserMessage("Blank Excel File Created");
                                //--
                                System.Diagnostics.Process.Start(Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text));
                            }
                            else
                                cmnService.J_UserMessage("Blank Excel File Creation Failed");
                            //--
                            txtDestinationFileName.Text = "";
                            //
                            return;
                        }
                        //-- ANIK @ 2018-12-22
                        ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314(v8.1)." + cmbFileType.Text;//ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314." + cmbFileType.Text;
                                                                                                                         //-- ANIK @ 2016-11-30
                                                                                                                         //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314(v7)." + cmbFileType.Text;
                                                                                                                         //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314(v8)." + cmbFileType.Text;
                                                                                                                         ////-- ANIK @ 2016-11-08
                                                                                                                         //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314(v6)." + cmbFileType.Text;
                                                                                                                         //-- ANIK @ 2016-05-02
                                                                                                                         //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314(v5)." + cmbFileType.Text;
                                                                                                                         //-- ANIK @ 2015-04-20
                                                                                                                         //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314(v4)." + cmbFileType.Text;
                                                                                                                         //-- ANIK @ 2014-05-03
                                                                                                                         //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314(v1)." + cmbFileType.Text;
                                                                                                                         //-- ANIK @ 2014-05-03
                                                                                                                         //if (TDSMAN.Classes.TDSMAN.T_AllotAutomaticRefNo == true)
                                                                                                                         //    ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314(v2)." + cmbFileType.Text;
                                                                                                                         //else // 2014-09-25
                                                                                                                         //    ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314(v3)." + cmbFileType.Text;
                    }
                    else
                    {
                        //--2018-12-22
                        ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1213(v8.1)." + cmbFileType.Text;
                        //if (TDSMAN.Classes.TDSMAN.T_AllotAutomaticRefNo == true)
                        //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1213." + cmbFileType.Text;
                        //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1213(v8)." + cmbFileType.Text;
                        //else // 2014-09-25
                        //    ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1213(v3)." + cmbFileType.Text;
                    }
                    #endregion
                }
                else if (strFormNo == T_FormNo.F24QAnnexureIII)
                {
                    #region AnnexureIII
                    //----
                    //-- ANIK 2019/05/12
                    if (TdsMan.T_CheckInternetConnectivty() == false)
                    {
                        cmnService.J_UserMessage("It needs Internet Connectivity (currently absent) to download the Form 16 excel format.");
                        return;
                    }
                    //--
                    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2020_21ID)
                    {
                        using (WebClient wcF2020_21ID = new WebClient())
                            wcF2020_21ID.DownloadFile("http://www.tdsman.com/Downloads/24Q-AnnexureIII." + cmbFileType.Text, Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text));

                    }
                    else if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2018_19ID)
                    {
                        using (WebClient wc = new WebClient())
                            wc.DownloadFile("http://www.tdsman.com/Downloads/24Q-AnnexureIII." + cmbFileType.Text, Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text));
                    }
                    //--
                    System.Threading.Thread.Sleep(100);
                    //--
                    if (cmnService.J_IsFileExist(Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text)) == true)
                    {
                        cmnService.J_UserMessage("Blank Excel File Created");
                        //--
                        System.Diagnostics.Process.Start(Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text));
                    }
                    else
                        cmnService.J_UserMessage("Blank Excel File Creation Failed");
                    //--
                    txtDestinationFileName.Text = "";
                    //
                    return;
                    //}
                    //-- ANIK @ 2018-12-22
                    ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314(v8.1)." + cmbFileType.Text;//ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + "_SD" + "1314." + cmbFileType.Text;

                    #endregion
                }
                else
                {
                    //For Financial Year 2014-14 and onwards
                    //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                    //-- 2016/04/22
                    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                    {
                        //if (TDSMAN.Classes.TDSMAN.T_AllotAutomaticRefNo == true)
                        //{
                        //    ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314." + cmbFileType.Text;
                        //}
                        //-- 2014-09-25
                        if (strFormNo.Trim() == T_FormNo.F24Q || strFormNo.Trim() == T_FormNo.F26Q)
                        {
                            //if (TDSMAN.Classes.TDSMAN.T_AllotAutomaticRefNo == true)
                            //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314." + cmbFileType.Text;
                            //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314(v8)." + cmbFileType.Text;
                            ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "1314(v8.1)." + cmbFileType.Text;
                            //else
                            //    ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314(F2426v1)." + cmbFileType.Text;
                        }
                        else if (strFormNo.Trim() == T_FormNo.F27Q)
                        {
                            //-- 2016/11/04
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2016_17ID)
                                //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314(v2)." + cmbFileType.Text;
                                ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "1314(v8.1)." + cmbFileType.Text;
                            else
                                //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314(v1)." + cmbFileType.Text;
                                //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314(v8.2)." + cmbFileType.Text;
                                ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "1314(v8.3)." + cmbFileType.Text;
                        }
                        else if (strFormNo.Trim() == T_FormNo.F27EQ)
                        {
                            //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314(v5_8)." + cmbFileType.Text;
                            //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314(v8)." + cmbFileType.Text;
                            //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314(v8.1)." + cmbFileType.Text;
                            ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "1314(v8.2)." + cmbFileType.Text;
                        }
                        else
                        {
                            //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314(v1)." + cmbFileType.Text;
                            ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "1314(v8)." + cmbFileType.Text;
                        }
                        //-- 2026/05/18
                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2026_27ID)
                        {
                            ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "2627." + cmbFileType.Text;
                        }
                        //-- 2020/03/02
                        else if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2019_20ID)
                        {
                            if (strFormNo.Trim() == T_FormNo.F26Q || strFormNo.Trim() == T_FormNo.F27Q)
                            {
                                //-- COMMENTED 2020/10/30
                                //if (TDSMAN.Classes.TDSMAN.T_SEC194N_EXCESS_1CRORE_EXCEL_IMPORT_OPTION == true)
                                //{
                                ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "2021(v21)." + cmbFileType.Text;
                                //}
                                //-- 2020/10/30
                                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2020_21ID)
                                    ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "2021(v22)." + cmbFileType.Text;
                                //-- FVU 8.2 - 2023/08/14
                                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2023_24ID)
                                    ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "2023(v24)." + cmbFileType.Text;
                                //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "2023(v23)." + cmbFileType.Text;
                            }
                            else if (strFormNo.Trim() == T_FormNo.F27EQ)
                            {
                                //-- FVU 8.2 - 2023/08/14
                                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2023_24ID)
                                    ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "2023(v24)." + cmbFileType.Text;
                                //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "2023(v23)." + cmbFileType.Text;
                            }
                        }
                    }
                    else
                    {
                        //if (TDSMAN.Classes.TDSMAN.T_AllotAutomaticRefNo == true)
                        //{
                        //    ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1213." + cmbFileType.Text;
                        //}
                        //-- 2014-09-25
                        if (strFormNo.Trim() == T_FormNo.F24Q || strFormNo.Trim() == T_FormNo.F26Q)
                        {
                            //if (TDSMAN.Classes.TDSMAN.T_AllotAutomaticRefNo == true)
                            //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1213." + cmbFileType.Text;
                            ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "1213(v8)." + cmbFileType.Text;
                            //else
                            //    ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1213(F2426v1)." + cmbFileType.Text;
                        }
                        else
                            //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1213." + cmbFileType.Text;
                            ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + strFormNo.ToUpper() + "1213(v8)." + cmbFileType.Text;
                    }
                }
                //
                //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text + "." + cmbFileType.Text;
                //-- MessageBox.Show("1");
                //--SOURCE FILE 
                strSourceFile = Path.Combine(strSourcePath, ExcelFileName);
                //-- MessageBox.Show("2");
                //--DESTINATION FILE
                strDestFile = Path.Combine(strExcelFolder, txtDestinationFileName.Text + "." + cmbFileType.Text);
                //-- MessageBox.Show("3");
                //
                if (cmnService.J_IsFileExist(strDestFile) == true)
                {
                    if (cmnService.J_UserMessage("File exists with same name.\nDo you want to replace the old file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
                }
                else
                {
                    if (cmnService.J_UserMessage("Blank excel file for Form No. " + strFormNo.ToUpper() + " will be created.\nProceed??", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
                //
            }
            //-- MessageBox.Show("4");
            //if (cmnService.J_IsFileExist(strSourceFile) == false)
            //{
            //    lblPleaseWait.Visible = true;
            //    this.Refresh();
            //    //
            //    //-- MessageBox.Show("5");
            //    btnCreateExcel_Click(sender, e);
            //    //cmnService.J_UserMessage("Blank File does not exist in application folder. \nContact TDSMAN");
            //    //return;
            //    //-- MessageBox.Show("6");
            //}
            //
            if (rbnExcel.Checked == true)
            {
                if (cmnService.J_IsFileExist(strSourceFile) == false)
                {
                    lblPleaseWait.Visible = true;
                    this.Refresh();
                    //
                    //-- MessageBox.Show("5");
                    btnCreateExcel_Click(sender, e);
                    //cmnService.J_UserMessage("Blank File does not exist in application folder. \nContact TDSMAN");
                    //return;
                    //-- MessageBox.Show("6");
                }

                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2026_27ID)
                {
                    // TO COPY EXCEL FILE FROM SOURCE LOCATION TO DESTINATION LOCATION
                    System.IO.File.Copy(strSourceFile, strDestFile, true);
                }
                else
                {
                    if(strFormNo == strCompanyMaster || strFormNo == strDeducteeMaster || strFormNo == strEmployeeMaster )
                        System.IO.File.Copy(strSourceFile, strDestFile, true);
                }
                //-- MessageBox.Show("7");
                //
                //CHECK IF FILE IS CREATED OR NOT
                if (cmnService.J_IsFileExist(strDestFile) == true)
                {
                    cmnService.J_UserMessage("Blank File Created");
                    //--
                    System.Diagnostics.Process.Start(strDestFile);
                }
                else
                    cmnService.J_UserMessage("Blank File Creation Failed");
            }
            else if (rbnCSV.Checked == true)
            {
                lblPleaseWait.Visible = true;
                this.Refresh();
                //
                btnCreateExcel_Click(sender, e);
                //--
                if (cmnService.J_IsFolderExist(Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text)) == true)
                {
                    cmnService.J_UserMessage("Blank File Created");
                    //--
                    System.Diagnostics.Process.Start(Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                }
                else
                    cmnService.J_UserMessage("Blank File Creation Failed");
            }
            //
            lblPleaseWait.Visible = false;
            this.Refresh();
            ////
            //lblPleaseWait.Visible = false;
            //this.Refresh();
            ////CHECK IF FILE IS CREATED OR NOT
            //if (cmnService.J_IsFileExist(strDestFile) == true)
            //{
            //    cmnService.J_UserMessage("Blank File Created");
            //    //--
            //    System.Diagnostics.Process.Start(strDestFile);
            //}
            //else
            //    cmnService.J_UserMessage("Blank File Creation Failed");

            blnUpdateTextfileName = true;
            txtDestinationFileName.Text = "";
            //
            return;
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region cmbFormNo_SelectedIndexChanged
        private void cmbFormNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDestinationFileName();
        }
        #endregion

        #region cmbFileType_SelectedIndexChanged
        private void cmbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDestinationFileName();
        }
        #endregion

        #region cmbFinancialYear_SelectedIndexChanged
        private void cmbFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDestinationFileName();

            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2026_27ID) //-- 2026/07/17
            {
                string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")", T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")", T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")", T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")", T_FormNo.F24QSalaryDetails, strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
                dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            }
            else if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2018_19ID)//-- 2021/02/19
            {
                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2021_22ID)
                {
                    //string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QAnnexureIII, strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
                    string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails,  strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
                    dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
                }
                else
                {
                    string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
                    dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
                }
            }
            else if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2018_19ID)
            {
                string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QForm16SalaryDetails, strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
                dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            }
        }
        #endregion

        #region txtDestinationFileName_KeyPress
        private void txtDestinationFileName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 9 && e.KeyChar != 13)
            {
                blnUpdateTextfileName = false;
            }
        }
        #endregion

        #region btnCreateExcel_Click
        private void btnCreateExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbnCSV.Checked == true)
                {
                    #region CSV
                    if (cmbFormNo.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Form No. - Cannot be Blank");
                        cmbFormNo.Select();
                        return;
                    }
                    //--
                    string CSVFileName = "", CSVFolderName = "";
                    if (TdsMan.T_CheckInternetConnectivty() == false)
                    {
                        cmnService.J_UserMessage("It needs Internet Connectivity (currently absent) to download the Form 16 excel format.");
                        return;
                    }
                    //--
                    //string strCSVPath = cmnService.J_OpenFolderDialog("Select Destination Folder");
                    string strCSVPath = txtExcelPath.Text;//, txtDestinationFileName.Text);
                    //
                    if (strCSVPath == "") return;
                    //--
                    //if(!Directory.Exists(strCSVPath))
                    //{
                    //    Directory.CreateDirectory(strCSVPath);
                    //}
                    //
                    //--
                    int intIncrement = 0;
                    ////
                    //strFormNo = "";
                    //if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2026_27ID)
                    //{
                    //    if (cmbFormNo.Text == T_FormNo.F138_24Q + "(" + T_FormNo.F24Q + ")")
                    //        strFormNo = T_FormNo.F24Q;
                    //    else if (cmbFormNo.Text == T_FormNo.F140_26Q + "(" + T_FormNo.F26Q + ")")
                    //        strFormNo = T_FormNo.F26Q;
                    //    else if (cmbFormNo.Text == T_FormNo.F144_27Q + "(" + T_FormNo.F27Q + ")")
                    //        strFormNo = T_FormNo.F27Q;
                    //    else if (cmbFormNo.Text == T_FormNo.F143_27EQ + "(" + T_FormNo.F27EQ + ")")
                    //        strFormNo = T_FormNo.F27EQ;
                    //    else
                    //        strFormNo = cmbFormNo.Text;
                    //}
                    //else
                    //{
                    //    strFormNo = cmbFormNo.Text;
                    //}
                    //
                    if (strFormNo == T_FormNo.F24Q)
                    {
                        #region F24Q
                        //--
                        this.Cursor = Cursors.WaitCursor;
                        //--
                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2026_27ID)
                            CSVFileName = "CSV_BLANK_FORMAT_24Q.zip";
                        else
                            CSVFileName = "CSV_BLANK_FORMAT_138_2627.zip";
                        CSVFolderName = txtDestinationFileName.Text;// "CSV_BLANK_FORMAT_24Q";
                        do
                        {
                            if (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true)
                            {
                                intIncrement++;
                                CSVFolderName = cmnService.J_Mid(CSVFolderName, 0, CSVFolderName.Length - 4) + " (" + intIncrement + ")";  //-- 12/11/2018 --
                            }
                        } while (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true);
                        //--
                        if (!Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)))
                        {
                            Directory.CreateDirectory(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        txtDestinationFileName.Text = CSVFolderName.ToString();
                        //--
                        strCSVPath = Path.Combine(strCSVPath, CSVFolderName);
                        //--
                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2026_27ID)
                        {
                            using (WebClient wc = new WebClient())
                                wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_BLANK_FORMAT_24Q.zip", Path.Combine(strCSVPath, CSVFileName));
                        }
                        else
                        {
                            using (WebClient wc = new WebClient())
                                wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_BLANK_FORMAT_24Q_2627.zip", Path.Combine(strCSVPath, CSVFileName));

                        }
                        //--
                        System.Threading.Thread.Sleep(300);
                        //--
                        if (Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)) == true)
                        {
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFileName));
                        }
                        else
                        {
                            J_UnZipString(Path.Combine(strCSVPath, CSVFileName));
                            System.Threading.Thread.Sleep(300);
                            if (File.Exists(Path.Combine(strCSVPath, CSVFileName)) == true)
                            {
                                File.Delete(Path.Combine(strCSVPath, CSVFileName));
                            }
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        this.Cursor = Cursors.Default;
                        //--
                        #endregion
                    }
                    else if (strFormNo == T_FormNo.F26Q)
                    {
                        #region F26Q
                        //--
                        this.Cursor = Cursors.WaitCursor;
                        //--
                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2026_27ID)
                            CSVFileName = "CSV_BLANK_FORMAT_26Q.zip";
                        else
                            CSVFileName = "CSV_BLANK_FORMAT_140_2627.zip";
                        CSVFolderName = txtDestinationFileName.Text;//  "CSV_BLANK_FORMAT_26Q";
                        do
                        {
                            if (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true)
                            {
                                intIncrement++;
                                CSVFolderName = cmnService.J_Mid(CSVFolderName, 0, CSVFolderName.Length - 4) + " (" + intIncrement + ")";  //-- 12/11/2018 --
                            }
                        } while (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true);
                        //--
                        if (!Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)))
                        {
                            Directory.CreateDirectory(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        txtDestinationFileName.Text = CSVFolderName.ToString();
                        //--
                        strCSVPath = Path.Combine(strCSVPath, CSVFolderName);
                        //--
                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2026_27ID)
                            using (WebClient wc = new WebClient())
                                wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_BLANK_FORMAT_26Q.zip", Path.Combine(strCSVPath, CSVFileName));
                        else
                            using (WebClient wc = new WebClient())
                                wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_BLANK_FORMAT_26Q_2627.zip", Path.Combine(strCSVPath, CSVFileName));
                        //--
                        System.Threading.Thread.Sleep(300);
                        //--
                        if (Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)) == true)
                        {
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFileName));
                        }
                        else
                        {
                            J_UnZipString(Path.Combine(strCSVPath, CSVFileName));
                            System.Threading.Thread.Sleep(300);
                            if (File.Exists(Path.Combine(strCSVPath, CSVFileName)) == true)
                            {
                                File.Delete(Path.Combine(strCSVPath, CSVFileName));
                            }
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        this.Cursor = Cursors.Default;
                        //--
                        #endregion
                    }
                    else if (strFormNo == T_FormNo.F27Q)
                    {
                        #region F27Q
                        //--
                        this.Cursor = Cursors.WaitCursor;
                        //--
                        //CSVFileName = "CSV_BLANK_FORMAT_27Q.zip";
                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2026_27ID)
                            CSVFileName = "CSV_BLANK_FORMAT_27Q.zip";
                        else
                            CSVFileName = "CSV_BLANK_FORMAT_144_2627.zip";
                        //
                        CSVFolderName = txtDestinationFileName.Text;// "CSV_BLANK_FORMAT_27Q";
                        do
                        {
                            if (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true)
                            {
                                intIncrement++;
                                CSVFolderName = cmnService.J_Mid(CSVFolderName, 0, CSVFolderName.Length - 4) + " (" + intIncrement + ")";  //-- 12/11/2018 --
                            }
                        } while (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true);
                        //--
                        if (!Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)))
                        {
                            Directory.CreateDirectory(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        txtDestinationFileName.Text = CSVFolderName.ToString();
                        //--
                        strCSVPath = Path.Combine(strCSVPath, CSVFolderName);
                        //--
                        //using (WebClient wc = new WebClient())
                        //    wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_BLANK_FORMAT_27Q.zip", Path.Combine(strCSVPath, CSVFileName));
                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2026_27ID)
                            using (WebClient wc = new WebClient())
                                wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_BLANK_FORMAT_27Q.zip", Path.Combine(strCSVPath, CSVFileName));
                        else
                            using (WebClient wc = new WebClient())
                                wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_BLANK_FORMAT_27Q_2627.zip", Path.Combine(strCSVPath, CSVFileName));
                        //--
                        System.Threading.Thread.Sleep(300);
                        //--
                        if (Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)) == true)
                        {
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFileName));
                        }
                        else
                        {
                            J_UnZipString(Path.Combine(strCSVPath, CSVFileName));
                            System.Threading.Thread.Sleep(300);
                            if (File.Exists(Path.Combine(strCSVPath, CSVFileName)) == true)
                            {
                                File.Delete(Path.Combine(strCSVPath, CSVFileName));
                            }
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        this.Cursor = Cursors.Default;
                        //--
                        #endregion
                    }
                    else if (strFormNo == T_FormNo.F27EQ)
                    {
                        #region F27EQ
                        //--
                        this.Cursor = Cursors.WaitCursor;
                        //--
                        //CSVFileName = "CSV_BLANK_FORMAT_27EQ.zip";
                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2026_27ID)
                            CSVFileName = "CSV_BLANK_FORMAT_27EQ.zip";
                        else
                            CSVFileName = "CSV_BLANK_FORMAT_143_2627.zip";
                        CSVFolderName = txtDestinationFileName.Text;//  "CSV_BLANK_FORMAT_27EQ";
                        do
                        {
                            if (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true)
                            {
                                intIncrement++;
                                CSVFolderName = cmnService.J_Mid(CSVFolderName, 0, CSVFolderName.Length - 4) + " (" + intIncrement + ")";  //-- 12/11/2018 --
                            }
                        } while (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true);
                        //--
                        if (!Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)))
                        {
                            Directory.CreateDirectory(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        txtDestinationFileName.Text = CSVFolderName.ToString();
                        //--
                        strCSVPath = Path.Combine(strCSVPath, CSVFolderName);
                        //--
                        //using (WebClient wc = new WebClient())
                        //    wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_BLANK_FORMAT_27EQ.zip", Path.Combine(strCSVPath, CSVFileName));
                        if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2026_27ID)
                            using (WebClient wc = new WebClient())
                                wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_BLANK_FORMAT_27EQ.zip", Path.Combine(strCSVPath, CSVFileName));
                        else
                            using (WebClient wc = new WebClient())
                                wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_BLANK_FORMAT_27EQ_2627.zip", Path.Combine(strCSVPath, CSVFileName));
                        //--
                        System.Threading.Thread.Sleep(300);
                        //--
                        if (Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)) == true)
                        {
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFileName));
                        }
                        else
                        {
                            J_UnZipString(Path.Combine(strCSVPath, CSVFileName));
                            System.Threading.Thread.Sleep(300);
                            if (File.Exists(Path.Combine(strCSVPath, CSVFileName)) == true)
                            {
                                File.Delete(Path.Combine(strCSVPath, CSVFileName));
                            }
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        this.Cursor = Cursors.Default;
                        //--
                        #endregion
                    }
                    else if (strFormNo == T_FormNo.F24QSalaryDetails)
                    {
                        #region F24QSalaryDetails
                        //--
                        this.Cursor = Cursors.WaitCursor;
                        //--
                        CSVFileName = "CSV_BLANK_FORMAT_24QSD.zip";
                        CSVFolderName = txtDestinationFileName.Text;// "CSV_BLANK_FORMAT_24QSD";
                        do
                        {
                            if (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true)
                            {
                                intIncrement++;
                                CSVFolderName = cmnService.J_Mid(CSVFolderName, 0, CSVFolderName.Length - 4) + " (" + intIncrement + ")";  //-- 12/11/2018 --
                            }
                        } while (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true);
                        //--
                        if (!Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)))
                        {
                            Directory.CreateDirectory(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        txtDestinationFileName.Text = CSVFolderName.ToString();
                        //--
                        strCSVPath = Path.Combine(strCSVPath, CSVFolderName);
                        //--
                        using (WebClient wc = new WebClient())
                            wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_BLANK_FORMAT_24QSD.zip", Path.Combine(strCSVPath, CSVFileName));
                        //--
                        System.Threading.Thread.Sleep(300);
                        //--
                        if (Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)) == true)
                        {
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFileName));
                        }
                        else
                        {
                            J_UnZipString(Path.Combine(strCSVPath, CSVFileName));
                            System.Threading.Thread.Sleep(300);
                            if (File.Exists(Path.Combine(strCSVPath, CSVFileName)) == true)
                            {
                                File.Delete(Path.Combine(strCSVPath, CSVFileName));
                            }
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        this.Cursor = Cursors.Default;
                        //--
                        #endregion
                    }
                    else if (strFormNo == strDeducteeMaster)
                    {
                        #region Deductee Master
                        //--
                        this.Cursor = Cursors.WaitCursor;
                        //--
                        CSVFileName = "CSV_DEDUCTEE_MASTER.zip";
                        CSVFolderName = txtDestinationFileName.Text;// "CSV_BLANK_FORMAT_24Q";
                        do
                        {
                            if (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true)
                            {
                                intIncrement++;
                                CSVFolderName = cmnService.J_Mid(CSVFolderName, 0, CSVFolderName.Length - 4) + " (" + intIncrement + ")";  //-- 12/11/2018 --
                            }
                        } while (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true);
                        //--
                        if (!Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)))
                        {
                            Directory.CreateDirectory(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        txtDestinationFileName.Text = CSVFolderName.ToString();
                        //--
                        strCSVPath = Path.Combine(strCSVPath, CSVFolderName);
                        //--
                        using (WebClient wc = new WebClient())
                            wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_DEDUCTEE_MASTER.zip", Path.Combine(strCSVPath, CSVFileName));
                        //--
                        System.Threading.Thread.Sleep(300);
                        //--
                        if (Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)) == true)
                        {
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFileName));
                        }
                        else
                        {
                            J_UnZipString(Path.Combine(strCSVPath, CSVFileName));
                            System.Threading.Thread.Sleep(300);
                            if (File.Exists(Path.Combine(strCSVPath, CSVFileName)) == true)
                            {
                                File.Delete(Path.Combine(strCSVPath, CSVFileName));
                            }
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        this.Cursor = Cursors.Default;
                        //--
                        #endregion
                    }
                    else if (strFormNo == strEmployeeMaster)
                    {
                        #region Employee Master
                        //--
                        this.Cursor = Cursors.WaitCursor;
                        //--
                        CSVFileName = "CSV_EMPLOYEE_MASTER.zip";
                        CSVFolderName = txtDestinationFileName.Text;// "CSV_BLANK_FORMAT_24Q";
                        do
                        {
                            if (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true)
                            {
                                intIncrement++;
                                CSVFolderName = cmnService.J_Mid(CSVFolderName, 0, CSVFolderName.Length - 4) + " (" + intIncrement + ")";  //-- 12/11/2018 --
                            }
                        } while (cmnService.J_IsFolderExist(Path.Combine(strCSVPath, CSVFolderName)) == true);
                        //--
                        if (!Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)))
                        {
                            Directory.CreateDirectory(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        txtDestinationFileName.Text = CSVFolderName.ToString();
                        //--
                        strCSVPath = Path.Combine(strCSVPath, CSVFolderName);
                        //--
                        using (WebClient wc = new WebClient())
                            wc.DownloadFile("http://www.tdsman.com/Downloads/CSV_EMPLOYEE_MASTER.zip", Path.Combine(strCSVPath, CSVFileName));
                        //--
                        System.Threading.Thread.Sleep(300);
                        //--
                        if (Directory.Exists(Path.Combine(strCSVPath, CSVFolderName)) == true)
                        {
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFileName));
                        }
                        else
                        {
                            J_UnZipString(Path.Combine(strCSVPath, CSVFileName));
                            System.Threading.Thread.Sleep(300);
                            if (File.Exists(Path.Combine(strCSVPath, CSVFileName)) == true)
                            {
                                File.Delete(Path.Combine(strCSVPath, CSVFileName));
                            }
                            //System.Diagnostics.Process.Start(Path.Combine(strCSVPath, CSVFolderName));
                        }
                        //--
                        this.Cursor = Cursors.Default;
                        //--
                        #endregion
                    }
                    else if (strFormNo == strCompanyMaster)
                    {
                        #region Company Master
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    //-- MessageBox.Show("5.0");
                    string strExcelFieldAsciiAfterZ = "";
                    bool blnExcelFieldAsciiAfterZ = false; //FOR EXCEL INITIAL CELL VALUE A 

                    int intColumn;
                    int intRow;
                    //--
                    //if (cmnService.J_UserMessage("Proceed??", MessageBoxButtons.YesNo) == DialogResult.No)
                    //    return;
                    //--
                    this.Cursor = Cursors.WaitCursor;
                    //--
                    //-- 2026/05/18
                    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2026_27ID)
                    {
                        using (WebClient wc = new WebClient())
                            wc.DownloadFile("http://www.tdsman.com/Downloads/" + ExcelFileName, strDestFile);
                    }
                    else
                    {
                        //--
                        //    
                        string strExcelFileName = ExcelFileName;
                        //
                        //if(cmbFormNo.Text == strDeducteeMaster)
                        //    strExcelFileName = cmbFileType.Text.ToUpper() + "_DEDUCTEE_MASTER." + cmbFileType.Text;
                        //else
                        //    strExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "." + cmbFileType.Text;
                        //
                        //-- MessageBox.Show("5.0.1");
                        if (CREATE_EXCEL_FILE(Path.Combine(strSourcePath, strExcelFileName)) == false) return;
                        //-- MessageBox.Show("5.1");
                        //--
                        #region F24Q
                        if (strFormNo == T_FormNo.F24Q)
                        {
                            //-- MessageBox.Show("5.2");
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Read me") == false) return;
                            //
                            if (WRITE_READ_ME_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Read me", strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Remarks") == false) return;
                            //
                            if (WRITE_REMARKS_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Remarks",
                                                      strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Section") == false) return;
                            //
                            if (WRITE_SECTION_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Section",
                                                      strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.EMPLOYEE_DETAILS) == false) return;
                            //
                            intColumn = 64;
                            intRow = 1;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Employee Serial No (313)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Challan Serial Reference (301)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "PAN of the Employee (315)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Name of the Employee (316)", true) == false) return;
                            //

                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.EMPLOYEE_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Section Code (317)", true) == false) return;
                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Payment/Credit Date (dd/mm/yyyy) (318)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Amount Paid/Credited (320)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "TDS (321)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Surcharge", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Education Cess (322)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Deducted (323)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Deposited (324)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.EMPLOYEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Reason for Non-deduction/Lower Deduction (326)", false) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- MessageBox.Show("5.3");
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.EMPLOYEE_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Certificate number for Lower/non deduction (327)", false) == false) return;
                            }
                            //
                            //-- MessageBox.Show("5.4");
                            //if (TDSMAN.Classes.TDSMAN.T_AllotAutomaticRefNo == false)
                            //{
                            //    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                              T_Sheet_Name.EMPLOYEE_DETAILS,
                            //                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                              "Employee Serial No", false) == false) return;
                            //}
                            //-----------------------------------------------------------------------------------------------------
                            intColumn = 64;
                            //
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.CHALLAN_DETAILS) == false) return;
                            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Running Serial No (301)", true) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.uptoFY1213)
                            //-- MessageBox.Show("5.5");
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.CHALLAN_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Section", true) == false) return;
                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "TDS (302)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Surcharge", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Education Cess (303)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Interest (304)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Fee (305)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Others (306)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Deposited (307)", false) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.uptoFY1213)
                            //-- MessageBox.Show("5.6");
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.CHALLAN_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Cheque No", false) == false) return;
                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "BSR Code / 24G Receipt No (309)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Date on Tax Deposited (dd/mm/yyyy) (311)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Transfer Voucher/Challan Serial No (310)", false) == false) return;

                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Whether TDS Deposited by Book Entry (308)", false) == false) return;

                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- MessageBox.Show("5.7");
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.CHALLAN_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Minor head (312)", false) == false) return;
                            }

                            //
                            //if (PROTECT_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                          T_Sheet_Name.CHALLAN_DETAILS) == false) return;
                            // 
                            //-- Added By Abhishek Dey On 04/04/2018 --
                            // COMPANY DETAILS
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.COMPANY_DETAILS) == false) return;
                            //
                            if (WRITE_COMPANY_DETAILS_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.COMPANY_DETAILS, strFormNo) == false) return;
                            //-----------------------------------------
                            //               
                        }
                        #endregion
                        //
                        #region F26Q
                        else if (strFormNo == T_FormNo.F26Q)
                        {
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Read me") == false) return;
                            //
                            if (WRITE_READ_ME_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Read me", strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Remarks") == false) return;
                            //
                            if (WRITE_REMARKS_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Remarks",
                                                      strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Deductee Code") == false) return;
                            //
                            if (WRITE_DEDUCTEE_CODE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Deductee Code") == false) return;
                            //-----------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Section") == false) return;
                            //
                            if (WRITE_SECTION_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Section",
                                                      strFormNo) == false) return;
                            //                
                            intColumn = 64;
                            intRow = 1;
                            //-----------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.DEDUCTEE_DETAILS) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Deductee Serial No (414)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Challan Serial Reference (401)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Deductee Code (414)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "PAN of the Deductee (415)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Name of the Deductee (416)", true) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.DEDUCTEE_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Section Code (417)", true) == false) return;
                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Payment/Credit Date (dd/mm/yyyy) (418)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Amount Paid/Credited (419)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "TDS", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Surcharge", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Education Cess", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Deducted (420)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Deposited (421)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Rate at which deducted (423)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Reason for Non-deduction/Lower Deduction (424)", false) == false) return;

                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.DEDUCTEE_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Certificate number for Lower/non deduction (425)", false) == false) return;
                            }
                            //-- 2020/03/02
                            //-- COMMENTED ON 2020/10/30
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2019_20ID)
                            {
                                //    if (TDSMAN.Classes.TDSMAN.T_SEC194N_EXCESS_1CRORE_EXCEL_IMPORT_OPTION == true)
                                //    {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                              T_Sheet_Name.DEDUCTEE_DETAILS,
                                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                              "Amount Excess 1Cr - Sec194N", false) == false) return;
                                //    }
                            }
                            //
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2020_21ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                              T_Sheet_Name.DEDUCTEE_DETAILS,
                                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                              "Section 194NF; ITR not Filed (1 : 20L-1Cr, 2 : excess of 1Cr)", false) == false) return;
                            }
                            //
                            //-- FVU 8.2 2023/08/14
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2023_24ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                              T_Sheet_Name.DEDUCTEE_DETAILS,
                                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                              "Amount Excess 3Cr - Sec194NC", false) == false) return;
                                //
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                              T_Sheet_Name.DEDUCTEE_DETAILS,
                                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                              "Section 194N-FT: ITR not Filed (1 : 20L-3Cr; 2 : excess of 3Cr)", false) == false) return;
                            }
                            //if (TDSMAN.Classes.TDSMAN.T_AllotAutomaticRefNo == false)
                            //{
                            //    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                              T_Sheet_Name.DEDUCTEE_DETAILS,
                            //                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                              "Deductee Reference No", false) == false) return;
                            //}
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.CHALLAN_DETAILS) == false) return;
                            //
                            intColumn = 64;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Running Serial No (401)", true) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.uptoFY1213)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Section-Code", true) == false) return;
                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "TDS (402)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Surcharge", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Education Cess", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Interest (403)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Fee (404)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Others (405)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Deposited (406)", true) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.uptoFY1213)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Cheque No", false) == false) return;
                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "BSR Code / 24G Receipt No (408)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Date on Tax Deposited (dd/mm/yyyy) (410)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Transfer Voucher/Challan Serial No (409)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Whether TDS Deposited by Book Entry (407)", false) == false) return;

                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.CHALLAN_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Minor head (411)", false) == false) return;
                            }
                            //-- Added By Abhishek Dey On 04/04/2018 --
                            // COMPANY DETAILS
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.COMPANY_DETAILS) == false) return;
                            //
                            if (WRITE_COMPANY_DETAILS_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.COMPANY_DETAILS, strFormNo) == false) return;
                            //-----------------------------------------
                            //
                        }
                        #endregion
                        //
                        #region F27Q
                        else if (strFormNo == T_FormNo.F27Q)
                        {
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Read me") == false) return;
                            //
                            if (WRITE_READ_ME_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Read me", strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Remarks") == false) return;
                            //
                            if (WRITE_REMARKS_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Remarks",
                                                      strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Deductee Code") == false) return;
                            //
                            if (WRITE_DEDUCTEE_CODE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Deductee Code") == false) return;
                            //-----------------------------------------------------------------------------------------------------------
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            //if (cmbFinancialYear.SelectedIndex >= (int)T_FinancialYearID.F2013_14ID)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "TDS Rate Act Codes") == false) return;
                                //
                                if (WRITE_TDS_RATE_ACT_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "TDS Rate Act Codes") == false) return;
                                //-----------------------------------------------------------------------------------------------------------
                                if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Country Codes") == false) return;
                                //
                                if (WRITE_COUNTRY_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Country Codes") == false) return;
                                //-----------------------------------------------------------------------------------------------------------
                                if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Remittance Codes") == false) return;
                                //
                                if (WRITE_REMITTANCE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Remittance Codes") == false) return;
                            }
                            //-----------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Section") == false) return;
                            //
                            if (WRITE_SECTION_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Section",
                                                      strFormNo) == false) return;

                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.DEDUCTEE_DETAILS) == false) return;
                            //
                            intColumn = 64;
                            intRow = 1;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Deductee Serial No (714)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Challan Serial Reference (701)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Deductee Code (716)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "PAN of the Deductee (717)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Name of the Deductee (718)", true) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.DEDUCTEE_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Section Code (719)", true) == false) return;
                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Payment/Credit Date (dd/mm/yyyy) (727)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Amount Paid/Credited (721)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "TDS (722)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Surcharge (723)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Education Cess (724)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Deducted (725)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Deposited (726)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Rate at which deducted (728)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Reason for Non-deduction/Lower Deduction (729)", false) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.DEDUCTEE_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Certificate number for Lower/non deduction (730)", false) == false) return;
                            }
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.uptoFY1213)
                            //-- 2016/04/22
                            //if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2013_14ID)
                            //{
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                  T_Sheet_Name.DEDUCTEE_DETAILS,
                                                  (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                  "Grossing Up Indicator", false) == false) return;
                            //}

                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "TDS Rate Act Code (731)", false) == false) return;

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Remittance code (732)", false) == false) return;

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Acknowledgment No Form 15CA (733)", false) == false) return;

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Country code (734)", false) == false) return;
                                //-- 2016/11/04
                                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2016_17ID)
                                {
                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Contact No (735)", false) == false) return;

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.DEDUCTEE_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Email (736)", false) == false) return;

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.DEDUCTEE_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Tax Identification No (737)", false) == false) return;

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.DEDUCTEE_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Address (738)", false) == false) return;
                                }
                                //--
                                //-- 2020/03/02
                                //-- COMMENTED ON 2020/10/30
                                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2019_20ID)
                                {
                                    //if (TDSMAN.Classes.TDSMAN.T_SEC194N_EXCESS_1CRORE_EXCEL_IMPORT_OPTION == true)
                                    //{
                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                  T_Sheet_Name.DEDUCTEE_DETAILS,
                                                                  (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                  "Amount Excess 1Cr - Sec194N", false) == false) return;
                                    //}
                                }
                                //-- 2020/10/30
                                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2020_21ID)
                                {
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                    }
                                    //--
                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                  T_Sheet_Name.DEDUCTEE_DETAILS,
                                                                  ("A" + Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                  "Section 194NF; ITR not Filed (1 : 20L-1Cr, 2 : excess of 1Cr)", false) == false) return;
                                    //-- FVU 8.2 2023/08/14
                                    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2023_24ID)
                                    {
                                        if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                                      ("A" + Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                      "Amount Excess 3Cr - Sec194NC", false) == false) return;
                                        //
                                        if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                                      ("A" + Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                      "Section 194N-FT: ITR not Filed (1 : 20L-3Cr; 2 : excess of 3Cr)", false) == false) return;
                                        //
                                        if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          T_Sheet_Name.DEDUCTEE_DETAILS,
                                                                          ("A" + Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Opting out of taxation regime u/s 115BAC(1A)-(Y/N)", false) == false) return;
                                    }
                                }
                            }
                            //----------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.CHALLAN_DETAILS) == false) return;
                            //
                            intColumn = 64;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Running Serial No (701)", true) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.uptoFY1213)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Section-Code", true) == false) return;
                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "TDS (702)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Surcharge (703)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Education Cess (704)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Interest (705)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Fee (706)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Others (707)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Deposited (708)", true) == false) return;
                            //

                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.uptoFY1213)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Cheque No", false) == false) return;
                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "BSR Code / 24G Receipt No (710)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Date on Tax Deposited (dd/mm/yyyy) (712)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Transfer Voucher/Challan Serial No (711)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Whether TDS Deposited by Book Entry (709)", false) == false) return;

                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)  
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.CHALLAN_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Minor head (713)", false) == false) return;
                            }
                            //--------------------------------------------------------------------------------------------------
                            //
                            //-- Added By Abhishek Dey On 04/04/2018 --
                            // COMPANY DETAILS
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.COMPANY_DETAILS) == false) return;
                            //
                            if (WRITE_COMPANY_DETAILS_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.COMPANY_DETAILS, strFormNo) == false) return;
                            //-----------------------------------------
                            //
                        }
                        #endregion
                        //
                        #region F27EQ
                        else if (strFormNo == T_FormNo.F27EQ)
                        {
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Read me") == false) return;
                            //
                            if (WRITE_READ_ME_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Read me", strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Remarks") == false) return;
                            //
                            if (WRITE_REMARKS_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Remarks",
                                                      strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Deductee Code") == false) return;
                            //
                            if (WRITE_DEDUCTEE_CODE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Deductee Code") == false) return;
                            //-----------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Collection Code") == false) return;
                            //
                            if (WRITE_SECTION_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Collection Code",
                                                      strFormNo) == false) return;
                            //------------------------------------------------------------------------------------------------------               
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.DEDUCTEE_DETAILS) == false) return;
                            //                    
                            intColumn = 64;
                            intRow = 1;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Party Serial No (664)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Challan Serial Reference (651)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Party Code (666)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "PAN of the Deductee (667)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Name of the Party (668)", true) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.DEDUCTEE_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Section Code (672)", true) == false) return;
                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Payment/Debit Date (dd/mm/yyyy) (671)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Amount Paid/Debited (670)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "TCS (673)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Surcharge (674)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Education Cess (675)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Collected (676)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Deposited (677)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Rate at which Collected (679)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Reason for Non-Collection/Lower Collection (680)", false) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.DEDUCTEE_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Certificate number for Lower/non deduction (681)", false) == false) return;
                            }

                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Value of Purchase (669)", false) == false) return;
                            //-- 2018/01/11
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Non-Resident (Y/N)", false) == false) return;

                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Permanently Established (Y/N)", false) == false) return;
                            //-- 2021/01/04 - FVU 7.0
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Challan Number (for reason F & G)", false) == false) return;

                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.DEDUCTEE_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Challan Date (for reason F & G)", false) == false) return;
                            //-- FVU 8.2 2023/08/14
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2023_24ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                  T_Sheet_Name.DEDUCTEE_DETAILS,
                                                                  (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                  "Opting out of taxation regime u/s 115BAC(1A)-(Y/N)", false) == false) return;
                            }
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.CHALLAN_DETAILS) == false) return;
                            //
                            intColumn = 64;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Running Serial No (651)", true) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.uptoFY1213)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.CHALLAN_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Section-Code", true) == false) return;
                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "TCS (652)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Surcharge (653)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Education Cess (654)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Interest (655)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Fee (656)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Others (657)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Total Tax Deposited (658)", true) == false) return;
                            //
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.uptoFY1213)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Cheque No", false) == false) return;

                            }
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "BSR Code / 24G Receipt No (660)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Date on Tax Deposited (dd/mm/yyyy) (662)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Transfer Voucher/Challan Serial No (661)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.CHALLAN_DETAILS,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Whether TDS Deposited by Book Entry (659)", false) == false) return;

                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                          T_Sheet_Name.CHALLAN_DETAILS,
                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                          "Minor head (663)", false) == false) return;
                            }
                            //
                            //-- Added By Abhishek Dey On 04/04/2018 --
                            // COMPANY DETAILS
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), T_Sheet_Name.COMPANY_DETAILS) == false) return;
                            //
                            if (WRITE_COMPANY_DETAILS_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      T_Sheet_Name.COMPANY_DETAILS, strFormNo) == false) return;
                            //-----------------------------------------
                            //
                        }
                        #endregion
                        //--
                        #region Deductee Master
                        else if (strFormNo == strDeducteeMaster)
                        {
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Read me") == false) return;
                            //
                            if (WRITE_READ_ME_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Read me", strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Deductee Code") == false) return;
                            //
                            if (WRITE_DEDUCTEE_CODE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Deductee Code") == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "STATE MASTER") == false) return;
                            //
                            if (WRITE_STATE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "STATE MASTER") == false) return;
                            //--------------------------------------------------------------------------------------------------
                            intColumn = 64;
                            intRow = 1;
                            //-----------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), strDeducteeMaster) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      strDeducteeMaster,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "DEDUCTEE NAME", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strDeducteeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "DEDUCTEE PAN", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strDeducteeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "DEDUCTEE CODE", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strDeducteeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "ADDRESS 1", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strDeducteeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "ADDRESS 2", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strDeducteeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "ADDRESS 3", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strDeducteeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "ADDRESS 4", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strDeducteeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "ADDRESS 5", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strDeducteeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "STATE", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strDeducteeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "PIN", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strDeducteeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "MOBILE", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strDeducteeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "EMAIL", false) == false) return;
                            //-----------------------------------------------------------------------------------------------------------


                        }
                        #endregion
                        //--
                        #region Salary Detail
                        else if (strFormNo == T_FormNo.F24QSalaryDetails)
                        {
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Read me") == false) return;
                            //
                            if (WRITE_READ_ME_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Read me", strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            intColumn = 64;
                            intRow = 1;
                            //-----------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), strSalaryDetail) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                          strSalaryDetail,
                            //                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                          "Employee Serial No (327)", true) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      strSalaryDetail,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Employee Serial No (328)", true) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "PAN of the Employee (328)", true) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "PAN of the Employee (329)", true) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Name of the Employee (329)", true) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Name of the Employee (330)", true) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Category of the Employee (330)", true) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Category of the Employee (331)", true) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Period of employment : From Date (dd/mm/yyyy) (331)", true) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Period of employment : From Date (dd/mm/yyyy) (332)", true) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Period of employment : To Date (dd/mm/yyyy) (331)", true) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Period of employment : To Date (dd/mm/yyyy) (332)", true) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Total Salary(332)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Total Salary(335)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Gross Deduction under section 16(ii)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Gross Deduction under section 16(iii)", false) == false) return;
                            ////-- 2018/12/22
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Gross Deduction under section 16(ia)", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Gross Total Deduction under section 16(iii) (333)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Gross Total Deduction under section 16(ii), 16(iii) & 16(ia)", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Income Chargeable under head Salaries(334)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Income Chargeable under head Salaries(338)", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Income other than Salary (335)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Income other than Salary (339)", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Gross Total Income(336)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Gross Total Income(340)", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Deduction under Chapter VIA under section 80CCE", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Deduction under Chapter VIA under section 80CCF", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Deduction under Chapter VIA under Other sections", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Gross Total Deduction under chapter VIA (339)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Gross Total Deduction under chapter VIA (343)", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Total Taxable Income (340)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Total Taxable Income (344)", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Income Tax on Total Income (341)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Income Tax on Total Income (345)", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Surcharge (342)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Surcharge", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Education Cess (343)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Education Cess (346)", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Income Tax Releif under section 89 (344)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Income Tax Releif under section 89 (347)", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Net Tax Payable (345)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Net Tax Payable (348)", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Total TDS Deducted (346)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Total TDS Deducted (351)", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Shortfall/Excess Deduction of Tax(347)", false) == false) return;
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Shortfall/Excess Deduction of Tax(352)", false, true) == false) return;
                            ////-- 2018/12/21
                            //if (intColumn == 90)
                            //{
                            //    intColumn = 64;
                            //    blnExcelFieldAsciiAfterZ = true;
                            //}
                            //if (blnExcelFieldAsciiAfterZ == true)
                            //{
                            //    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                            //    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                            //}
                            //else
                            //    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strSalaryDetail,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Shortfall/Excess Deduction of Tax(352)", false, true) == false) return;
                            //-----------------------------------------------------------------------------------------------------------

                            //ADDED BY DHRUB ON 11/12/2013 FOR NEW FIELDS INTO THE SALARY DETAILS
                            //--------------------------------------------------------------------
                            //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                            //-- 2016/04/22
                            if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                            {
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                //-- 2015/04/20
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "Current employer salary(333)", false) == false) return;
                                //"Taxable Amount on which tax is deducted by the current employer(333)"
                                //------------------------------------
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }

                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                //-- 2015/04/20
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "Previous employer salary(334)", false) == false) return;
                                //"Reported Taxable Amount on which tax is deducted by previous employer.(334)"
                                //-------------------------------------
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                //-- 2015/04/20
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                              strSalaryDetail,
                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                              "Current employer TDS(349)", false) == false) return;

                                //"Total Amount of tax deducted at source by the current employer for the whole year.(349)"
                                //--------------------------------------
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                //-- 2015/04/20
                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "Previous employer TDS(350)", false) == false) return;
                                //"Reported amount of Tax deducted at source by previous employer(s)/deductor(s) (350)"
                                //---------------------------------------
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "Whether tax deducted at Higher rate due to non furnishing(353)", false) == false) return;
                                //---------------------------------------
                                //-- 2016/12/01
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "TDS including Superannuation", false, true) == false) return;
                                //-- 2016/11/10
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "Whether contributions paid by trustees of an approved Superannuation fund (Y/N)", false) == false) return;
                                //-- 2016/11/10
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "Name of Superannuation Fund", false) == false) return;
                                //-- 2016/11/10
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "From Date", false) == false) return;
                                //-- 2016/11/10
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "To Date", false) == false) return;
                                //-- 2016/11/10
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "Amount of contribution repaid", false) == false) return;
                                //-- 2016/11/10
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "Average rate of deduction", false) == false) return;
                                //-- 2016/11/10
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "Amount of Tax deducted", false) == false) return;
                                //-- 2016/11/10
                                if (intColumn == 90)
                                {
                                    intColumn = 64;
                                    blnExcelFieldAsciiAfterZ = true;
                                }
                                //--
                                if (blnExcelFieldAsciiAfterZ == true)
                                {
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                    strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                }
                                else
                                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strSalaryDetail,
                                                                          (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                          "Gross Total Income", false, true) == false) return;
                                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2016_17ID)
                                {
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "20) Whether rent payment exceeds 1lakh during previous year(Y/N)", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "PAN of landlord 1", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "Name of landlord 1", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "PAN of landlord 2", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "Name of landlord 2", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "PAN of landlord 3", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "Name of landlord 3", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "PAN of landlord 4", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "Name of landlord 4", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "21) Whether interest paid exceeds 1lakh (Y/N)", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "PAN of lender 1", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "A" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "Name of lender 1", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "B" + strExcelFieldAsciiAfterZ; //-- 2018/12/22
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "PAN of lender 2", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "B" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "Name of lender 2", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "B" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "PAN of lender 3", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "B" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "Name of lender 3", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "B" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "PAN of lender 4", false) == false) return;
                                    //-- 2016/11/30
                                    if (intColumn == 90)
                                    {
                                        intColumn = 64;
                                        blnExcelFieldAsciiAfterZ = true;
                                    }
                                    //--
                                    if (blnExcelFieldAsciiAfterZ == true)
                                    {
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));
                                        strExcelFieldAsciiAfterZ = "B" + strExcelFieldAsciiAfterZ;
                                    }
                                    else
                                        strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intColumn += 1));

                                    if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                              strSalaryDetail,
                                                                              (strExcelFieldAsciiAfterZ + Convert.ToString(intRow)),
                                                                              "Name of lender 4", false) == false) return;
                                }
                                //--
                                blnExcelFieldAsciiAfterZ = false;
                            }
                        }
                        #endregion
                        //--
                        #region Employeee Master
                        else if (strFormNo == strEmployeeMaster)
                        {
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Read me") == false) return;
                            //
                            if (WRITE_READ_ME_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Read me", strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Employee Category") == false) return;
                            ////
                            if (WRITE_EMPLOYEE_CATEGORY_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Employee Category") == false) return;
                            ////--------------------------------------------------------------------------------------------------
                            //if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "STATE MASTER") == false) return;
                            ////
                            //if (WRITE_STATE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "STATE MASTER") == false) return;
                            //--------------------------------------------------------------------------------------------------
                            intColumn = 64;
                            intRow = 1;
                            //-----------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), strEmployeeMaster) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      strEmployeeMaster,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "EMPLOYEE NAME", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strEmployeeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "EMPLOYEE PAN", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strEmployeeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "EMPLOYEE CATEGORY", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strEmployeeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "REFERENCE NO", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strEmployeeMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "DESIGNATION", false) == false) return;
                            //-----------------------------------------------------------------------------------------------------------
                        }
                        #endregion
                        //--
                        #region Company Master
                        else if (strFormNo == strCompanyMaster)
                        {
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Read me") == false) return;
                            //
                            if (WRITE_READ_ME_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      "Read me", strFormNo) == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "STATE MASTER") == false) return;
                            //
                            if (WRITE_STATE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "STATE MASTER") == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "DEDUCTOR TYPE") == false) return;
                            //
                            if (WRITE_DEDUCTOR_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "DEDUCTOR TYPE") == false) return;
                            //--------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "MINISTRY") == false) return;
                            //
                            if (WRITE_MINISTRY_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "MINISTRY") == false) return;
                            //--------------------------------------------------------------------------------------------------
                            intColumn = 64;
                            intRow = 1;
                            //-----------------------------------------------------------------------------------------------------------
                            if (CREATE_NEW_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), strCompanyMaster) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                      strCompanyMaster,
                                                      (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                      "Company name", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "TAN", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "PAN", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Branch/Division", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Deductor type", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "TAN Reg No", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Flt/Dr/Blck No", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Building", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Rd/Strt/Lane", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Area/Locality", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Town/District", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "PIN", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "State", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "STD", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Phone", true) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strCompanyMaster,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Alt STD", false) == false) return;
                            ////
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strCompanyMaster,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Alt Phone", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Email", true) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strCompanyMaster,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Alt Email", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Responsible Person's Name", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Designation", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Father's Name", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Mobile No", true) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strCompanyMaster,
                            //                                              (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                            //                                              "Responsible Person's address same as of Company(Y/N)", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Responsible Person's Flt/Dr/Blck No", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                                                          "Responsible Person's Building", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Responsible Person's Rd/Strt/Lane", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Responsible Person's Area/Locality", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Responsible Person's Town/District", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Responsible Person's PIN", true) == false) return;
                            //
                            intColumn = 65;
                            int intColumn1 = 64; // for AA after Z
                                                 //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Responsible Person's State", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Responsible Person's STD", true) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Responsible Person's Phone", true) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strCompanyMaster,
                            //                                              (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                            //                                              "Alt STD", false) == false) return;
                            ////
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strCompanyMaster,
                            //                                              (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                            //                                              "Alt Phone", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Responsible Person's Email", true) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strCompanyMaster,
                            //                                              (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                            //                                              "Alt Email", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "PAO Code", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "PAO Reg No", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "DDO Code", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "DDO Reg No", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "State", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Ministry", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Other Ministry", false) == false) return;
                            //
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Account Office Identification Number", false) == false) return;
                            //-- 2015/04/21
                            if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                                                                          strCompanyMaster,
                                                                          (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                                                                          "Responsible Person's PAN", false) == false) return;
                            //
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strCompanyMaster,
                            //                                              (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                            //                                              "CIT Address", false) == false) return;
                            ////
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strCompanyMaster,
                            //                                              (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                            //                                              "CIT City", false) == false) return;
                            ////
                            //if (WRITE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName),
                            //                                              strCompanyMaster,
                            //                                              (Convert.ToChar(intColumn).ToString() + Convert.ToChar(intColumn1 += 1).ToString() + Convert.ToString(intRow)),
                            //                                              "CIT PIN", false) == false) return;
                            //-----------------------------------------------------------------------------------------------------------
                        }
                        #endregion
                        //--
                        if (DELETE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Sheet1") == false) //return;
                            if (DELETE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Sheet2") == false) // return;
                                if (DELETE_WORKSHEET(Path.Combine(strSourcePath, strExcelFileName), "Sheet3") == false) //return;
                                                                                                                        //------------------------------------
                                    if (KILL_EXCEL() == false)
                                        return;
                        //
                    }
                    //--
                    this.Cursor = Cursors.Default;
                    //
                }
                
                //cmnService.J_UserMessage("Excel file created");
            }
            catch (Exception err)
            {

                cmnService.J_UserMessage("Excel file creation failed");
                //cmnService.J_UserMessage(err.Message);
                //--
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0045", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region rbnExcel_CheckedChanged
        private void rbnExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnExcel.Checked == true)
            {
                lblFileType.Enabled = true; cmbFileType.Enabled = true;
                SetDestinationFileName();
                //LoadImportType();
            }
            else if (rbnCSV.Checked == true)
            {
                lblFileType.Enabled = false; cmbFileType.Enabled = false;
                SetDestinationFileName();
                //LoadImportType();
            }
        }
        #endregion

        #endregion

        #region User Defined Functions

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
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion

        #region CREATE EXCEL FILE
        // SOURCE PATH : http://csharp.net-informations.com/excel/csharp-create-excel.htm
        private bool CREATE_EXCEL_FILE(string ExcelFilePath)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                //-- MessageBox.Show("5.0.1.1");
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                //Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                //--
                //-- MessageBox.Show("5.0.1.2");
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
                //-- MessageBox.Show("5.0.1.3");
                xlWorkBook.Close(true, misValue, misValue);
                //-- MessageBox.Show("5.0.1.4");
                xlApp.Quit();
                //-- MessageBox.Show("5.0.1.5");

                //ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                //-- MessageBox.Show("5.0.1.6");
                ReleaseObject(xlApp);
                //-- MessageBox.Show("5.0.1.7");
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

        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath, string WorksheetName)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //
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
                if (KILL_EXCEL() == false)
                    return false;
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
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region WRITE WORKSHEET

        #region WRITE WORKSHEET
        private bool WRITE_WORKSHEET(string ExcelFilePath, string SheetName, string Cell, string CellValue, bool Mandatory)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range(Cell, m).Value2 = CellValue;
                wsnew.get_Range(Cell, m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range(Cell, m).Borders.Value = true;
                wsnew.get_Range(Cell, m).RowHeight = 48.75;
                wsnew.get_Range(Cell, m).ColumnWidth = 12;
                wsnew.get_Range(Cell, m).WrapText = true;
                wsnew.get_Range(Cell, m).Font.Name = "Arial";
                wsnew.get_Range(Cell, m).Font.Bold = true;
                wsnew.get_Range(Cell, m).Font.Size = 9;
                //@@@@@@@@@@@@@@@
                if (Mandatory == true)
                    //                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow);
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 153));
                else if (Mandatory == false)
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(204, 255, 204));
                //                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region WRITE WORKSHEET
        private bool WRITE_WORKSHEET(string ExcelFilePath, string SheetName, string Cell, string CellValue, bool Mandatory, bool Calculated)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range(Cell, m).Value2 = CellValue;
                wsnew.get_Range(Cell, m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range(Cell, m).Borders.Value = true;
                wsnew.get_Range(Cell, m).RowHeight = 48.75;
                wsnew.get_Range(Cell, m).ColumnWidth = 12;
                wsnew.get_Range(Cell, m).WrapText = true;
                wsnew.get_Range(Cell, m).Font.Name = "Arial";
                wsnew.get_Range(Cell, m).Font.Bold = true;
                wsnew.get_Range(Cell, m).Font.Size = 9;
                //@@@@@@@@@@@@@@@
                if (Mandatory == true)
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 153));
                else if (Mandatory == false)
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(204, 255, 204));
                //--
                if (Calculated == true)
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(191, 187, 195));
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region WRITE SECTION WORKSHEET
        private bool WRITE_SECTION_WORKSHEET(string ExcelFilePath, string SheetName, string FormNo)
        {
            try
            {
                IDataReader drdGetSheetRecord = null;
                //--
                if (KILL_EXCEL() == false)
                    return false;
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("A1", m).Value2 = "Section";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 12;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "Section Description";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT SECTION_ID," +
                    "            SECTION_NO," +
                    "            SECTION_DESCRIPTION " +
                    "     FROM   MST_SECTION " +
                    "     WHERE  FORM_NAME ='" + FormNo + "' ";
                if (FormNo == T_FormNo.F24Q)
                    strSQL = strSQL + "AND SECTION_NO <> '' ";
                strSQL = strSQL + "ORDER BY SECTION_ID";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["SECTION_NO"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["SECTION_DESCRIPTION"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                //
                //wsnew.get_Range("A1", "M18").Locked = false;
                //wsnew.get_Range("A1", "M18").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //                
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #region WRITE DEDUCTEE CODE WORKSHEET
        private bool WRITE_DEDUCTEE_CODE_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                //--
                if (KILL_EXCEL() == false)
                    return false;
                //--
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("A1", m).Value2 = "Deductee Code";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 25;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("A2", m).Value2 = "01";
                wsnew.get_Range("A2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A2", m).Borders.Value = true;
                wsnew.get_Range("A2", m).ColumnWidth = 25;
                wsnew.get_Range("A2", m).WrapText = true;
                wsnew.get_Range("A2", m).Font.Name = "Arial";
                wsnew.get_Range("A2", m).Font.Bold = true;
                wsnew.get_Range("A2", m).Font.Size = 10;
                //
                wsnew.get_Range("A3", m).Value2 = "02";
                wsnew.get_Range("A3", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A3", m).Borders.Value = true;
                wsnew.get_Range("A3", m).ColumnWidth = 25;
                wsnew.get_Range("A3", m).WrapText = true;
                wsnew.get_Range("A3", m).Font.Name = "Arial";
                wsnew.get_Range("A3", m).Font.Bold = true;
                wsnew.get_Range("A3", m).Font.Size = 10;
                //
                wsnew.get_Range("B1", m).Value2 = "Deductee Type";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B2", m).Value2 = "Company";
                wsnew.get_Range("B2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B2", m).Borders.Value = true;
                wsnew.get_Range("B2", m).ColumnWidth = 25;
                wsnew.get_Range("B2", m).WrapText = true;
                wsnew.get_Range("B2", m).Font.Name = "Arial";
                wsnew.get_Range("B2", m).Font.Bold = true;
                wsnew.get_Range("B2", m).Font.Size = 10;
                //
                wsnew.get_Range("B3", m).Value2 = "Non-Company";
                wsnew.get_Range("B3", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B3", m).Borders.Value = true;
                wsnew.get_Range("B3", m).ColumnWidth = 25;
                wsnew.get_Range("B3", m).WrapText = true;
                wsnew.get_Range("B3", m).Font.Name = "Arial";
                wsnew.get_Range("B3", m).Font.Bold = true;
                wsnew.get_Range("B3", m).Font.Size = 10;
                //-- FVU 8.2 2023/09/04
                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2023_24ID)
                {
                    if (strFormNo == T_FormNo.F27Q || strFormNo == T_FormNo.F27EQ)
                    {
                        //--
                        wsnew.get_Range("A4", m).Value2 = "03";
                        wsnew.get_Range("A4", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("A4", m).Borders.Value = true;
                        wsnew.get_Range("A4", m).ColumnWidth = 25;
                        wsnew.get_Range("A4", m).WrapText = true;
                        wsnew.get_Range("A4", m).Font.Name = "Arial";
                        wsnew.get_Range("A4", m).Font.Bold = true;
                        wsnew.get_Range("A4", m).Font.Size = 10;
                        //
                        wsnew.get_Range("B4", m).Value2 = "Hindu Undivided Family";
                        wsnew.get_Range("B4", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("B4", m).Borders.Value = true;
                        wsnew.get_Range("B4", m).ColumnWidth = 25;
                        wsnew.get_Range("B4", m).WrapText = true;
                        wsnew.get_Range("B4", m).Font.Name = "Arial";
                        wsnew.get_Range("B4", m).Font.Bold = true;
                        wsnew.get_Range("B4", m).Font.Size = 10;
                        //--
                        wsnew.get_Range("A5", m).Value2 = "04";
                        wsnew.get_Range("A5", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("A5", m).Borders.Value = true;
                        wsnew.get_Range("A5", m).ColumnWidth = 25;
                        wsnew.get_Range("A5", m).WrapText = true;
                        wsnew.get_Range("A5", m).Font.Name = "Arial";
                        wsnew.get_Range("A5", m).Font.Bold = true;
                        wsnew.get_Range("A5", m).Font.Size = 10;
                        //
                        wsnew.get_Range("B5", m).Value2 = "Association of Persons (AOP) except in case of AOP consisting of only companies as its members";
                        wsnew.get_Range("B5", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("B5", m).Borders.Value = true;
                        wsnew.get_Range("B5", m).ColumnWidth = 25;
                        wsnew.get_Range("B5", m).WrapText = true;
                        wsnew.get_Range("B5", m).Font.Name = "Arial";
                        wsnew.get_Range("B5", m).Font.Bold = true;
                        wsnew.get_Range("B5", m).Font.Size = 10;
                        //--
                        wsnew.get_Range("A6", m).Value2 = "05";
                        wsnew.get_Range("A6", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("A6", m).Borders.Value = true;
                        wsnew.get_Range("A6", m).ColumnWidth = 25;
                        wsnew.get_Range("A6", m).WrapText = true;
                        wsnew.get_Range("A6", m).Font.Name = "Arial";
                        wsnew.get_Range("A6", m).Font.Bold = true;
                        wsnew.get_Range("A6", m).Font.Size = 10;
                        //
                        wsnew.get_Range("B6", m).Value2 = "Association of Persons (AOP) consisting of only companies as its members";
                        wsnew.get_Range("B6", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("B6", m).Borders.Value = true;
                        wsnew.get_Range("B6", m).ColumnWidth = 25;
                        wsnew.get_Range("B6", m).WrapText = true;
                        wsnew.get_Range("B6", m).Font.Name = "Arial";
                        wsnew.get_Range("B6", m).Font.Bold = true;
                        wsnew.get_Range("B6", m).Font.Size = 10;
                        //--
                        wsnew.get_Range("A7", m).Value2 = "06";
                        wsnew.get_Range("A7", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("A7", m).Borders.Value = true;
                        wsnew.get_Range("A7", m).ColumnWidth = 25;
                        wsnew.get_Range("A7", m).WrapText = true;
                        wsnew.get_Range("A7", m).Font.Name = "Arial";
                        wsnew.get_Range("A7", m).Font.Bold = true;
                        wsnew.get_Range("A7", m).Font.Size = 10;
                        //
                        wsnew.get_Range("B7", m).Value2 = "Co-operative Society";
                        wsnew.get_Range("B7", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("B7", m).Borders.Value = true;
                        wsnew.get_Range("B7", m).ColumnWidth = 25;
                        wsnew.get_Range("B7", m).WrapText = true;
                        wsnew.get_Range("B7", m).Font.Name = "Arial";
                        wsnew.get_Range("B7", m).Font.Bold = true;
                        wsnew.get_Range("B7", m).Font.Size = 10;
                        //--
                        wsnew.get_Range("A8", m).Value2 = "07";
                        wsnew.get_Range("A8", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("A8", m).Borders.Value = true;
                        wsnew.get_Range("A8", m).ColumnWidth = 25;
                        wsnew.get_Range("A8", m).WrapText = true;
                        wsnew.get_Range("A8", m).Font.Name = "Arial";
                        wsnew.get_Range("A8", m).Font.Bold = true;
                        wsnew.get_Range("A8", m).Font.Size = 10;
                        //
                        wsnew.get_Range("B8", m).Value2 = "Firm";
                        wsnew.get_Range("B8", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("B8", m).Borders.Value = true;
                        wsnew.get_Range("B8", m).ColumnWidth = 25;
                        wsnew.get_Range("B8", m).WrapText = true;
                        wsnew.get_Range("B8", m).Font.Name = "Arial";
                        wsnew.get_Range("B8", m).Font.Bold = true;
                        wsnew.get_Range("B8", m).Font.Size = 10;
                        //--
                        wsnew.get_Range("A9", m).Value2 = "08";
                        wsnew.get_Range("A9", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("A9", m).Borders.Value = true;
                        wsnew.get_Range("A9", m).ColumnWidth = 25;
                        wsnew.get_Range("A9", m).WrapText = true;
                        wsnew.get_Range("A9", m).Font.Name = "Arial";
                        wsnew.get_Range("A9", m).Font.Bold = true;
                        wsnew.get_Range("A9", m).Font.Size = 10;
                        //
                        wsnew.get_Range("B9", m).Value2 = "Body Of Individuals";
                        wsnew.get_Range("B9", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("B9", m).Borders.Value = true;
                        wsnew.get_Range("B9", m).ColumnWidth = 25;
                        wsnew.get_Range("B9", m).WrapText = true;
                        wsnew.get_Range("B9", m).Font.Name = "Arial";
                        wsnew.get_Range("B9", m).Font.Bold = true;
                        wsnew.get_Range("B9", m).Font.Size = 10;
                        //--
                        wsnew.get_Range("A10", m).Value2 = "09";
                        wsnew.get_Range("A10", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("A10", m).Borders.Value = true;
                        wsnew.get_Range("A10", m).ColumnWidth = 25;
                        wsnew.get_Range("A10", m).WrapText = true;
                        wsnew.get_Range("A10", m).Font.Name = "Arial";
                        wsnew.get_Range("A10", m).Font.Bold = true;
                        wsnew.get_Range("A10", m).Font.Size = 10;
                        //
                        wsnew.get_Range("B10", m).Value2 = "Artificial juridical person referred to in sub-clause (vii) of clause (31) of section 2 of the Income-tax Act 1961";
                        wsnew.get_Range("B10", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("B10", m).Borders.Value = true;
                        wsnew.get_Range("B10", m).ColumnWidth = 25;
                        wsnew.get_Range("B10", m).WrapText = true;
                        wsnew.get_Range("B10", m).Font.Name = "Arial";
                        wsnew.get_Range("B10", m).Font.Bold = true;
                        wsnew.get_Range("B10", m).Font.Size = 10;
                        //--
                        wsnew.get_Range("A11", m).Value2 = "10";
                        wsnew.get_Range("A11", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("A11", m).Borders.Value = true;
                        wsnew.get_Range("A11", m).ColumnWidth = 25;
                        wsnew.get_Range("A11", m).WrapText = true;
                        wsnew.get_Range("A11", m).Font.Name = "Arial";
                        wsnew.get_Range("A11", m).Font.Bold = true;
                        wsnew.get_Range("A11", m).Font.Size = 10;
                        //
                        wsnew.get_Range("B11", m).Value2 = "Others";
                        wsnew.get_Range("B11", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        wsnew.get_Range("B11", m).Borders.Value = true;
                        wsnew.get_Range("B11", m).ColumnWidth = 25;
                        wsnew.get_Range("B11", m).WrapText = true;
                        wsnew.get_Range("B11", m).Font.Name = "Arial";
                        wsnew.get_Range("B11", m).Font.Bold = true;
                        wsnew.get_Range("B11", m).Font.Size = 10;
                    }
                }
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                ////
                //wsnew.get_Range("A1", "B10").Locked = false;
                //wsnew.get_Range("A1", "B10").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //                                
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #region WRITE EMPLOYEE CATEGORY WORKSHEET
        private bool WRITE_EMPLOYEE_CATEGORY_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                //--
                if (KILL_EXCEL() == false)
                    return false;
                //--
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("A1", m).Value2 = "Employee Category";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 25;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("A2", m).Value2 = "G";
                wsnew.get_Range("A2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A2", m).Borders.Value = true;
                wsnew.get_Range("A2", m).ColumnWidth = 25;
                wsnew.get_Range("A2", m).WrapText = true;
                wsnew.get_Range("A2", m).Font.Name = "Arial";
                wsnew.get_Range("A2", m).Font.Bold = true;
                wsnew.get_Range("A2", m).Font.Size = 10;
                //
                wsnew.get_Range("A3", m).Value2 = "W";
                wsnew.get_Range("A3", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A3", m).Borders.Value = true;
                wsnew.get_Range("A3", m).ColumnWidth = 25;
                wsnew.get_Range("A3", m).WrapText = true;
                wsnew.get_Range("A3", m).Font.Name = "Arial";
                wsnew.get_Range("A3", m).Font.Bold = true;
                wsnew.get_Range("A3", m).Font.Size = 10;
                //
                wsnew.get_Range("A4", m).Value2 = "S";
                wsnew.get_Range("A4", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A4", m).Borders.Value = true;
                wsnew.get_Range("A4", m).ColumnWidth = 25;
                wsnew.get_Range("A4", m).WrapText = true;
                wsnew.get_Range("A4", m).Font.Name = "Arial";
                wsnew.get_Range("A4", m).Font.Bold = true;
                wsnew.get_Range("A4", m).Font.Size = 10;
                //
                wsnew.get_Range("A5", m).Value2 = "O";
                wsnew.get_Range("A5", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A5", m).Borders.Value = true;
                wsnew.get_Range("A5", m).ColumnWidth = 25;
                wsnew.get_Range("A5", m).WrapText = true;
                wsnew.get_Range("A5", m).Font.Name = "Arial";
                wsnew.get_Range("A5", m).Font.Bold = true;
                wsnew.get_Range("A5", m).Font.Size = 10;
                //
                wsnew.get_Range("B1", m).Value2 = "Description";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B2", m).Value2 = "General";
                wsnew.get_Range("B2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B2", m).Borders.Value = true;
                wsnew.get_Range("B2", m).ColumnWidth = 25;
                wsnew.get_Range("B2", m).WrapText = true;
                wsnew.get_Range("B2", m).Font.Name = "Arial";
                wsnew.get_Range("B2", m).Font.Bold = true;
                wsnew.get_Range("B2", m).Font.Size = 10;
                //
                wsnew.get_Range("B3", m).Value2 = "Female";
                wsnew.get_Range("B3", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B3", m).Borders.Value = true;
                wsnew.get_Range("B3", m).ColumnWidth = 25;
                wsnew.get_Range("B3", m).WrapText = true;
                wsnew.get_Range("B3", m).Font.Name = "Arial";
                wsnew.get_Range("B3", m).Font.Bold = true;
                wsnew.get_Range("B3", m).Font.Size = 10;
                //
                wsnew.get_Range("B4", m).Value2 = "Senior Citizen";
                wsnew.get_Range("B4", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B4", m).Borders.Value = true;
                wsnew.get_Range("B4", m).ColumnWidth = 25;
                wsnew.get_Range("B4", m).WrapText = true;
                wsnew.get_Range("B4", m).Font.Name = "Arial";
                wsnew.get_Range("B4", m).Font.Bold = true;
                wsnew.get_Range("B4", m).Font.Size = 10;
                //
                wsnew.get_Range("B5", m).Value2 = "Very Senior Citizen";
                wsnew.get_Range("B5", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B5", m).Borders.Value = true;
                wsnew.get_Range("B5", m).ColumnWidth = 25;
                wsnew.get_Range("B5", m).WrapText = true;
                wsnew.get_Range("B5", m).Font.Name = "Arial";
                wsnew.get_Range("B5", m).Font.Bold = true;
                wsnew.get_Range("B5", m).Font.Size = 10;
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                ////
                //wsnew.get_Range("A1", "B10").Locked = false;
                //wsnew.get_Range("A1", "B10").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //                                
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #region WRITE_REMARKS_WORKSHEET
        private bool WRITE_REMARKS_WORKSHEET(string ExcelFilePath, string SheetName, string FormNo)
        {
            try
            {
                IDataReader drdGetSheetRecord = null;
                //--
                if (KILL_EXCEL() == false)
                    return false;
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("A1", m).Value2 = "Reason";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 12;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "Reason Description";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT REASON_ID," +
                    "            REASON," +
                    "            DESCRIPTION " +
                    "     FROM   MST_REASON " +
                    "     WHERE  FORM_NO ='" + FormNo + "' ";
                strSQL = strSQL + "ORDER BY REASON";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["REASON"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["DESCRIPTION"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                //
                //wsnew.get_Range("A1", "M18").Locked = false;
                //wsnew.get_Range("A1", "M18").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //                
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #region WRITE READ ME WORKSHEET
        private bool WRITE_READ_ME_WORKSHEET(string ExcelFilePath, string SheetName, string FormName)
        {
            try
            {
                //--
                if (KILL_EXCEL() == false)
                    return false;
                //--
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("B4", m).Borders.Value = true;
                wsnew.get_Range("B4", m).ColumnWidth = 25;
                wsnew.get_Range("B4", m).RowHeight = 15;
                wsnew.get_Range("B4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 153));
                //
                wsnew.get_Range("B5", m).Borders.Value = true;
                wsnew.get_Range("B5", m).ColumnWidth = 25;
                wsnew.get_Range("B5", m).RowHeight = 15;
                wsnew.get_Range("B5", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(204, 255, 204));
                //
                wsnew.get_Range("C4", m).Value2 = "Light yellow Color in header, should be Mandatory";
                wsnew.get_Range("C4", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C4", m).Borders.Value = true;
                wsnew.get_Range("C4", m).ColumnWidth = 55;
                wsnew.get_Range("C4", m).WrapText = true;
                wsnew.get_Range("C4", m).Font.Name = "Arial";
                wsnew.get_Range("C4", m).Font.Bold = true;
                wsnew.get_Range("C4", m).Font.Size = 10;
                //
                wsnew.get_Range("C5", m).Value2 = "Light Green Color in header, Optional";
                wsnew.get_Range("C5", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C5", m).Borders.Value = true;
                wsnew.get_Range("C5", m).ColumnWidth = 55;
                wsnew.get_Range("C5", m).WrapText = true;
                wsnew.get_Range("C5", m).Font.Name = "Arial";
                wsnew.get_Range("C5", m).Font.Bold = true;
                wsnew.get_Range("C5", m).Font.Size = 10;
                //
                //wsnew.get_Range("B3", "C6").BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black);
                wsnew.get_Range("B3", "C6").Borders.Value = true;
                //
                wsnew.get_Range("C8", m).Value2 = "ERROR Color in Excel format";
                wsnew.get_Range("C8", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C8", m).Borders.Value = true;
                wsnew.get_Range("C8", m).ColumnWidth = 55;
                wsnew.get_Range("C8", m).WrapText = true;
                wsnew.get_Range("C8", m).Font.Name = "Arial";
                wsnew.get_Range("C8", m).Font.Bold = true;
                wsnew.get_Range("C8", m).Font.Size = 12;
                wsnew.get_Range("C8", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                wsnew.get_Range("C8", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                //
                wsnew.get_Range("B10", m).Value2 = "Color";
                wsnew.get_Range("B10", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B10", m).Borders.Value = true;
                wsnew.get_Range("B10", m).ColumnWidth = 55;
                wsnew.get_Range("B10", m).WrapText = true;
                wsnew.get_Range("B10", m).Font.Name = "Arial";
                wsnew.get_Range("B10", m).Font.Bold = true;
                wsnew.get_Range("B10", m).Font.Size = 10;
                wsnew.get_Range("B10", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                wsnew.get_Range("B10", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                //
                wsnew.get_Range("C10", m).Value2 = "Message";
                wsnew.get_Range("C10", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C10", m).Borders.Value = true;
                wsnew.get_Range("C10", m).ColumnWidth = 55;
                wsnew.get_Range("C10", m).WrapText = true;
                wsnew.get_Range("C10", m).Font.Name = "Arial";
                wsnew.get_Range("C10", m).Font.Bold = true;
                wsnew.get_Range("C10", m).Font.Size = 10;
                wsnew.get_Range("C10", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                wsnew.get_Range("C10", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                //
                wsnew.get_Range("B12", m).Borders.Value = true;
                wsnew.get_Range("B12", m).ColumnWidth = 55;
                wsnew.get_Range("B12", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SteelBlue);
                //
                wsnew.get_Range("C12", m).Value2 = "Duplication (It should be unique)";
                wsnew.get_Range("C12", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C12", m).Borders.Value = true;
                wsnew.get_Range("C12", m).ColumnWidth = 55;
                wsnew.get_Range("C12", m).WrapText = true;
                wsnew.get_Range("C12", m).Font.Name = "Arial";
                wsnew.get_Range("C12", m).Font.Bold = true;
                wsnew.get_Range("C12", m).Font.Size = 10;
                //
                wsnew.get_Range("B14", m).Borders.Value = true;
                wsnew.get_Range("B14", m).ColumnWidth = 55;
                wsnew.get_Range("B14", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Tan);
                //
                wsnew.get_Range("C14", m).Value2 = "Error in data length";
                wsnew.get_Range("C14", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C14", m).Borders.Value = true;
                wsnew.get_Range("C14", m).ColumnWidth = 55;
                wsnew.get_Range("C14", m).WrapText = true;
                wsnew.get_Range("C14", m).Font.Name = "Arial";
                wsnew.get_Range("C14", m).Font.Bold = true;
                wsnew.get_Range("C14", m).Font.Size = 10;
                //
                wsnew.get_Range("B16", m).Borders.Value = true;
                wsnew.get_Range("B16", m).ColumnWidth = 55;
                wsnew.get_Range("B16", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SpringGreen);
                //
                wsnew.get_Range("C16", m).Value2 = "Only numeric data allowed";
                wsnew.get_Range("C16", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C16", m).Borders.Value = true;
                wsnew.get_Range("C16", m).ColumnWidth = 55;
                wsnew.get_Range("C16", m).WrapText = true;
                wsnew.get_Range("C16", m).Font.Name = "Arial";
                wsnew.get_Range("C16", m).Font.Bold = true;
                wsnew.get_Range("C16", m).Font.Size = 10;
                //
                wsnew.get_Range("B18", m).Borders.Value = true;
                wsnew.get_Range("B18", m).ColumnWidth = 55;
                wsnew.get_Range("B18", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Chocolate);
                //
                wsnew.get_Range("C18", m).Value2 = "Invalid Data";
                wsnew.get_Range("C18", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C18", m).Borders.Value = true;
                wsnew.get_Range("C18", m).ColumnWidth = 55;
                wsnew.get_Range("C18", m).WrapText = true;
                wsnew.get_Range("C18", m).Font.Name = "Arial";
                wsnew.get_Range("C18", m).Font.Bold = true;
                wsnew.get_Range("C18", m).Font.Size = 10;
                //
                wsnew.get_Range("B20", m).Borders.Value = true;
                wsnew.get_Range("B20", m).ColumnWidth = 55;
                wsnew.get_Range("B20", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                //
                wsnew.get_Range("C20", m).Value2 = "Missing Data";
                wsnew.get_Range("C20", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C20", m).Borders.Value = true;
                wsnew.get_Range("C20", m).ColumnWidth = 55;
                wsnew.get_Range("C20", m).WrapText = true;
                wsnew.get_Range("C20", m).Font.Name = "Arial";
                wsnew.get_Range("C20", m).Font.Bold = true;
                wsnew.get_Range("C20", m).Font.Size = 10;
                //
                wsnew.get_Range("B22", m).Borders.Value = true;
                wsnew.get_Range("B22", m).ColumnWidth = 55;
                wsnew.get_Range("B22", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Tomato);
                //
                wsnew.get_Range("C22", m).Value2 = "Invalid Format";
                wsnew.get_Range("C22", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C22", m).Borders.Value = true;
                wsnew.get_Range("C22", m).ColumnWidth = 55;
                wsnew.get_Range("C22", m).WrapText = true;
                wsnew.get_Range("C22", m).Font.Name = "Arial";
                wsnew.get_Range("C22", m).Font.Bold = true;
                wsnew.get_Range("C22", m).Font.Size = 10;
                //
                wsnew.get_Range("B8", "C27").Borders.Value = true;
                //
                #region SALARY DETAIL
                if (FormName == T_FormNo.F24QSalaryDetails)
                {
                    //
                    wsnew.get_Range("C30", m).Value2 = "Column Explaination of Salary Details";
                    wsnew.get_Range("C30", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("C30", m).Borders.Value = true;
                    wsnew.get_Range("C30", m).ColumnWidth = 55;
                    wsnew.get_Range("C30", m).WrapText = true;
                    wsnew.get_Range("C30", m).Font.Name = "Arial";
                    wsnew.get_Range("C30", m).Font.Bold = true;
                    wsnew.get_Range("C30", m).Font.Size = 10;
                    wsnew.get_Range("C30", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("C30", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("A32", m).Value2 = "Col Name";
                    wsnew.get_Range("A32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("A32", m).Borders.Value = true;
                    wsnew.get_Range("A32", m).ColumnWidth = 55;
                    wsnew.get_Range("A32", m).WrapText = true;
                    wsnew.get_Range("A32", m).Font.Name = "Arial";
                    wsnew.get_Range("A32", m).Font.Bold = true;
                    wsnew.get_Range("A32", m).Font.Size = 10;
                    wsnew.get_Range("A32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("A32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("A32", m).Value2 = "Col Name";
                    wsnew.get_Range("A32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("A32", m).Borders.Value = true;
                    wsnew.get_Range("A32", m).ColumnWidth = 55;
                    wsnew.get_Range("A32", m).WrapText = true;
                    wsnew.get_Range("A32", m).Font.Name = "Arial";
                    wsnew.get_Range("A32", m).Font.Bold = true;
                    wsnew.get_Range("A32", m).Font.Size = 10;
                    wsnew.get_Range("A32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("A32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("B32", m).Value2 = "Column Header";
                    wsnew.get_Range("B32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B32", m).Borders.Value = true;
                    wsnew.get_Range("B32", m).ColumnWidth = 55;
                    wsnew.get_Range("B32", m).WrapText = true;
                    wsnew.get_Range("B32", m).Font.Name = "Arial";
                    wsnew.get_Range("B32", m).Font.Bold = true;
                    wsnew.get_Range("B32", m).Font.Size = 10;
                    wsnew.get_Range("B32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("B32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("C32", m).Value2 = "Description";
                    wsnew.get_Range("C32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C32", m).Borders.Value = true;
                    wsnew.get_Range("C32", m).ColumnWidth = 55;
                    wsnew.get_Range("C32", m).WrapText = true;
                    wsnew.get_Range("C32", m).Font.Name = "Arial";
                    wsnew.get_Range("C32", m).Font.Bold = true;
                    wsnew.get_Range("C32", m).Font.Size = 10;
                    wsnew.get_Range("C32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("C32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("D32", m).Value2 = "Format";
                    wsnew.get_Range("D32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D32", m).Borders.Value = true;
                    wsnew.get_Range("D32", m).ColumnWidth = 55;
                    wsnew.get_Range("D32", m).WrapText = true;
                    wsnew.get_Range("D32", m).Font.Name = "Arial";
                    wsnew.get_Range("D32", m).Font.Bold = true;
                    wsnew.get_Range("D32", m).Font.Size = 10;
                    wsnew.get_Range("D32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("D32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("E32", m).Value2 = "M / O";
                    wsnew.get_Range("E32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E32", m).Borders.Value = true;
                    wsnew.get_Range("E32", m).ColumnWidth = 55;
                    wsnew.get_Range("E32", m).WrapText = true;
                    wsnew.get_Range("E32", m).Font.Name = "Arial";
                    wsnew.get_Range("E32", m).Font.Bold = true;
                    wsnew.get_Range("E32", m).Font.Size = 10;
                    wsnew.get_Range("E32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("E32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    wsnew.get_Range("F32", m).Value2 = "Max Length";
                    wsnew.get_Range("F32", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F32", m).Borders.Value = true;
                    wsnew.get_Range("F32", m).ColumnWidth = 55;
                    wsnew.get_Range("F32", m).WrapText = true;
                    wsnew.get_Range("F32", m).Font.Name = "Arial";
                    wsnew.get_Range("F32", m).Font.Bold = true;
                    wsnew.get_Range("F32", m).Font.Size = 10;
                    wsnew.get_Range("F32", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    wsnew.get_Range("F32", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                    //
                    #region Col Name
                    wsnew.get_Range("A33", m).Value2 = "A";
                    wsnew.get_Range("A33", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A33", m).Borders.Value = true;
                    wsnew.get_Range("A33", m).ColumnWidth = 9;
                    wsnew.get_Range("A33", m).Font.Name = "Arial";
                    wsnew.get_Range("A33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A34", m).Value2 = "B";
                    wsnew.get_Range("A34", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A34", m).Borders.Value = true;
                    wsnew.get_Range("A34", m).ColumnWidth = 9;
                    wsnew.get_Range("A34", m).Font.Name = "Arial";
                    wsnew.get_Range("A34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A35", m).Value2 = "C";
                    wsnew.get_Range("A35", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A35", m).Borders.Value = true;
                    wsnew.get_Range("A35", m).ColumnWidth = 9;
                    wsnew.get_Range("A35", m).Font.Name = "Arial";
                    wsnew.get_Range("A35", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A36", m).Value2 = "D";
                    wsnew.get_Range("A36", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A36", m).Borders.Value = true;
                    wsnew.get_Range("A36", m).ColumnWidth = 9;
                    wsnew.get_Range("A36", m).Font.Name = "Arial";
                    wsnew.get_Range("A36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A37", m).Value2 = "E";
                    wsnew.get_Range("A37", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A37", m).Borders.Value = true;
                    wsnew.get_Range("A37", m).ColumnWidth = 9;
                    wsnew.get_Range("A37", m).Font.Name = "Arial";
                    wsnew.get_Range("A37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A38", m).Value2 = "F";
                    wsnew.get_Range("A38", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A38", m).Borders.Value = true;
                    wsnew.get_Range("A38", m).ColumnWidth = 9;
                    wsnew.get_Range("A38", m).Font.Name = "Arial";
                    wsnew.get_Range("A38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A39", m).Value2 = "G";
                    wsnew.get_Range("A39", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A39", m).Borders.Value = true;
                    wsnew.get_Range("A39", m).ColumnWidth = 9;
                    wsnew.get_Range("A39", m).Font.Name = "Arial";
                    wsnew.get_Range("A39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A40", m).Value2 = "H";
                    wsnew.get_Range("A40", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A40", m).Borders.Value = true;
                    wsnew.get_Range("A40", m).ColumnWidth = 9;
                    wsnew.get_Range("A40", m).Font.Name = "Arial";
                    wsnew.get_Range("A40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A41", m).Value2 = "I";
                    wsnew.get_Range("A41", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A41", m).Borders.Value = true;
                    wsnew.get_Range("A41", m).ColumnWidth = 9;
                    wsnew.get_Range("A41", m).Font.Name = "Arial";
                    wsnew.get_Range("A41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A42", m).Value2 = "J";
                    wsnew.get_Range("A42", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A42", m).Borders.Value = true;
                    wsnew.get_Range("A42", m).ColumnWidth = 9;
                    wsnew.get_Range("A42", m).Font.Name = "Arial";
                    wsnew.get_Range("A42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A43", m).Value2 = "K";
                    wsnew.get_Range("A43", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A43", m).Borders.Value = true;
                    wsnew.get_Range("A43", m).ColumnWidth = 9;
                    wsnew.get_Range("A43", m).Font.Name = "Arial";
                    wsnew.get_Range("A43", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A44", m).Value2 = "L";
                    wsnew.get_Range("A44", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A44", m).Borders.Value = true;
                    wsnew.get_Range("A44", m).ColumnWidth = 9;
                    wsnew.get_Range("A44", m).Font.Name = "Arial";
                    wsnew.get_Range("A44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A45", m).Value2 = "M";
                    wsnew.get_Range("A45", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A45", m).Borders.Value = true;
                    wsnew.get_Range("A45", m).ColumnWidth = 9;
                    wsnew.get_Range("A45", m).Font.Name = "Arial";
                    wsnew.get_Range("A45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A46", m).Value2 = "N";
                    wsnew.get_Range("A46", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A46", m).Borders.Value = true;
                    wsnew.get_Range("A46", m).ColumnWidth = 9;
                    wsnew.get_Range("A46", m).Font.Name = "Arial";
                    wsnew.get_Range("A46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A47", m).Value2 = "O";
                    wsnew.get_Range("A47", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A47", m).Borders.Value = true;
                    wsnew.get_Range("A47", m).ColumnWidth = 9;
                    wsnew.get_Range("A47", m).Font.Name = "Arial";
                    wsnew.get_Range("A47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A48", m).Value2 = "P";
                    wsnew.get_Range("A48", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A48", m).Borders.Value = true;
                    wsnew.get_Range("A48", m).ColumnWidth = 9;
                    wsnew.get_Range("A48", m).Font.Name = "Arial";
                    wsnew.get_Range("A48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A49", m).Value2 = "Q";
                    wsnew.get_Range("A49", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A49", m).Borders.Value = true;
                    wsnew.get_Range("A49", m).ColumnWidth = 9;
                    wsnew.get_Range("A49", m).Font.Name = "Arial";
                    wsnew.get_Range("A49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A50", m).Value2 = "R";
                    wsnew.get_Range("A50", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A50", m).Borders.Value = true;
                    wsnew.get_Range("A50", m).ColumnWidth = 9;
                    wsnew.get_Range("A50", m).Font.Name = "Arial";
                    wsnew.get_Range("A50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A51", m).Value2 = "S";
                    wsnew.get_Range("A51", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A51", m).Borders.Value = true;
                    wsnew.get_Range("A51", m).ColumnWidth = 9;
                    wsnew.get_Range("A51", m).Font.Name = "Arial";
                    wsnew.get_Range("A51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A52", m).Value2 = "T";
                    wsnew.get_Range("A52", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A52", m).Borders.Value = true;
                    wsnew.get_Range("A52", m).ColumnWidth = 9;
                    wsnew.get_Range("A52", m).Font.Name = "Arial";
                    wsnew.get_Range("A52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A53", m).Value2 = "U";
                    wsnew.get_Range("A53", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A53", m).Borders.Value = true;
                    wsnew.get_Range("A53", m).ColumnWidth = 9;
                    wsnew.get_Range("A53", m).Font.Name = "Arial";
                    wsnew.get_Range("A53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A54", m).Value2 = "V";
                    wsnew.get_Range("A54", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A54", m).Borders.Value = true;
                    wsnew.get_Range("A54", m).ColumnWidth = 9;
                    wsnew.get_Range("A54", m).Font.Name = "Arial";
                    wsnew.get_Range("A54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A55", m).Value2 = "W";
                    wsnew.get_Range("A55", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A55", m).Borders.Value = true;
                    wsnew.get_Range("A55", m).ColumnWidth = 9;
                    wsnew.get_Range("A55", m).Font.Name = "Arial";
                    wsnew.get_Range("A55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A56", m).Value2 = "X";
                    wsnew.get_Range("A56", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A56", m).Borders.Value = true;
                    wsnew.get_Range("A56", m).ColumnWidth = 9;
                    wsnew.get_Range("A56", m).Font.Name = "Arial";
                    wsnew.get_Range("A56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("A57", m).Value2 = "Y";
                    wsnew.get_Range("A57", m).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    wsnew.get_Range("A57", m).Borders.Value = true;
                    wsnew.get_Range("A57", m).ColumnWidth = 9;
                    wsnew.get_Range("A57", m).Font.Name = "Arial";
                    wsnew.get_Range("A57", m).Font.Size = 10;
                    //
                    #endregion
                    //
                    #region Column Header
                    wsnew.get_Range("B33", m).Value2 = "Employee Serial No (327)";
                    wsnew.get_Range("B33", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B33", m).Borders.Value = true;
                    wsnew.get_Range("B33", m).ColumnWidth = 28;
                    wsnew.get_Range("B33", m).Font.Name = "Arial";
                    wsnew.get_Range("B33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B34", m).Value2 = "PAN of the Employee (328)";
                    wsnew.get_Range("B34", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B34", m).Borders.Value = true;
                    wsnew.get_Range("B34", m).ColumnWidth = 28;
                    wsnew.get_Range("B34", m).Font.Name = "Arial";
                    wsnew.get_Range("B34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B35", m).Value2 = "Name of the Employee (329)";
                    wsnew.get_Range("B35", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B35", m).Borders.Value = true;
                    wsnew.get_Range("B35", m).ColumnWidth = 28;
                    wsnew.get_Range("B35", m).Font.Name = "Arial";
                    wsnew.get_Range("B35", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B36", m).Value2 = "Category of the Employee (330)";
                    wsnew.get_Range("B36", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B36", m).Borders.Value = true;
                    wsnew.get_Range("B36", m).ColumnWidth = 28;
                    wsnew.get_Range("B36", m).Font.Name = "Arial";
                    wsnew.get_Range("B36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B37", m).Value2 = "Period of employment : From Date (dd/mm/yyyy) (331)";
                    wsnew.get_Range("B37", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B37", m).Borders.Value = true;
                    wsnew.get_Range("B37", m).ColumnWidth = 28;
                    wsnew.get_Range("B37", m).Font.Name = "Arial";
                    wsnew.get_Range("B37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B38", m).Value2 = "Period of employment : To Date (dd/mm/yyyy) (331)";
                    wsnew.get_Range("B38", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B38", m).Borders.Value = true;
                    wsnew.get_Range("B38", m).ColumnWidth = 28;
                    wsnew.get_Range("B38", m).Font.Name = "Arial";
                    wsnew.get_Range("B38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B39", m).Value2 = "Total Salary(332)";
                    wsnew.get_Range("B39", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B39", m).Borders.Value = true;
                    wsnew.get_Range("B39", m).ColumnWidth = 28;
                    wsnew.get_Range("B39", m).Font.Name = "Arial";
                    wsnew.get_Range("B39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B40", m).Value2 = "Gross Deduction under section 16(ii)";
                    wsnew.get_Range("B40", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B40", m).Borders.Value = true;
                    wsnew.get_Range("B40", m).ColumnWidth = 28;
                    wsnew.get_Range("B40", m).Font.Name = "Arial";
                    wsnew.get_Range("B40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B41", m).Value2 = "Gross Deduction under section 16(iii)";
                    wsnew.get_Range("B41", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B41", m).Borders.Value = true;
                    wsnew.get_Range("B41", m).ColumnWidth = 28;
                    wsnew.get_Range("B41", m).Font.Name = "Arial";
                    wsnew.get_Range("B41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B42", m).Value2 = "Gross Total Deduction under section 16(iii) (333)";
                    wsnew.get_Range("B42", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B42", m).Borders.Value = true;
                    wsnew.get_Range("B42", m).ColumnWidth = 28;
                    wsnew.get_Range("B42", m).Font.Name = "Arial";
                    wsnew.get_Range("B42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B43", m).Value2 = "Income Chargeable under head Salaries(334)";
                    wsnew.get_Range("B43", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B43", m).Borders.Value = true;
                    wsnew.get_Range("B43", m).ColumnWidth = 28;
                    wsnew.get_Range("B43", m).Font.Name = "Arial";
                    wsnew.get_Range("B33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B44", m).Value2 = "Income other than Salary (335)";
                    wsnew.get_Range("B44", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B44", m).Borders.Value = true;
                    wsnew.get_Range("B44", m).ColumnWidth = 28;
                    wsnew.get_Range("B44", m).Font.Name = "Arial";
                    wsnew.get_Range("B44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B45", m).Value2 = "Gross Total Income(336)";
                    wsnew.get_Range("B45", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B45", m).Borders.Value = true;
                    wsnew.get_Range("B45", m).ColumnWidth = 28;
                    wsnew.get_Range("B45", m).Font.Name = "Arial";
                    wsnew.get_Range("B45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B46", m).Value2 = "Deduction under Chapter VIA under section 80CCE";
                    wsnew.get_Range("B46", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B46", m).Borders.Value = true;
                    wsnew.get_Range("B46", m).ColumnWidth = 28;
                    wsnew.get_Range("B46", m).Font.Name = "Arial";
                    wsnew.get_Range("B46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B47", m).Value2 = "Deduction under Chapter VIA under section 80CCF";
                    wsnew.get_Range("B47", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B47", m).Borders.Value = true;
                    wsnew.get_Range("B47", m).ColumnWidth = 28;
                    wsnew.get_Range("B47", m).Font.Name = "Arial";
                    wsnew.get_Range("B47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B48", m).Value2 = "Deduction under Chapter VIA under Other sections";
                    wsnew.get_Range("B48", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B48", m).Borders.Value = true;
                    wsnew.get_Range("B48", m).ColumnWidth = 28;
                    wsnew.get_Range("B48", m).Font.Name = "Arial";
                    wsnew.get_Range("B48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B49", m).Value2 = "Gross Total Deduction under chapter VIA (339)";
                    wsnew.get_Range("B49", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B49", m).Borders.Value = true;
                    wsnew.get_Range("B49", m).ColumnWidth = 28;
                    wsnew.get_Range("B49", m).Font.Name = "Arial";
                    wsnew.get_Range("B49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B50", m).Value2 = "Total Taxable Income (340)";
                    wsnew.get_Range("B50", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B50", m).Borders.Value = true;
                    wsnew.get_Range("B50", m).ColumnWidth = 28;
                    wsnew.get_Range("B50", m).Font.Name = "Arial";
                    wsnew.get_Range("B50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B51", m).Value2 = "Income Tax on Total Income (341)";
                    wsnew.get_Range("B51", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B51", m).Borders.Value = true;
                    wsnew.get_Range("B51", m).ColumnWidth = 28;
                    wsnew.get_Range("B51", m).Font.Name = "Arial";
                    wsnew.get_Range("B51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B52", m).Value2 = "Surcharge (342)";
                    wsnew.get_Range("B52", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B52", m).Borders.Value = true;
                    wsnew.get_Range("B52", m).ColumnWidth = 28;
                    wsnew.get_Range("B52", m).Font.Name = "Arial";
                    wsnew.get_Range("B52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B53", m).Value2 = "Education Cess (343)";
                    wsnew.get_Range("B53", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B53", m).Borders.Value = true;
                    wsnew.get_Range("B53", m).ColumnWidth = 28;
                    wsnew.get_Range("B53", m).Font.Name = "Arial";
                    wsnew.get_Range("B53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B54", m).Value2 = "Income Tax Releif under section 89 (344)";
                    wsnew.get_Range("B54", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B54", m).Borders.Value = true;
                    wsnew.get_Range("B54", m).ColumnWidth = 28;
                    wsnew.get_Range("B54", m).Font.Name = "Arial";
                    wsnew.get_Range("B54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B55", m).Value2 = "Net Tax Payable (345)";
                    wsnew.get_Range("B55", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B55", m).Borders.Value = true;
                    wsnew.get_Range("B55", m).ColumnWidth = 28;
                    wsnew.get_Range("B55", m).Font.Name = "Arial";
                    wsnew.get_Range("B55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B56", m).Value2 = "Total TDS Deducted (346)";
                    wsnew.get_Range("B56", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B56", m).Borders.Value = true;
                    wsnew.get_Range("B56", m).ColumnWidth = 28;
                    wsnew.get_Range("B56", m).Font.Name = "Arial";
                    wsnew.get_Range("B56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("B57", m).Value2 = "Shortfall/Excess Deduction of Tax(347)";
                    wsnew.get_Range("B57", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("B57", m).Borders.Value = true;
                    wsnew.get_Range("B57", m).ColumnWidth = 28;
                    wsnew.get_Range("B57", m).Font.Name = "Arial";
                    wsnew.get_Range("B57", m).Font.Size = 10;
                    //
                    #endregion
                    //
                    #region Description
                    wsnew.get_Range("C33", m).Value2 = "Running serial no to indicate detail record no.";
                    wsnew.get_Range("C33", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C33", m).Borders.Value = true;
                    wsnew.get_Range("C33", m).ColumnWidth = 110;
                    wsnew.get_Range("C33", m).Font.Name = "Arial";
                    wsnew.get_Range("C33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C34", m).Value2 = "PAN of the employee.";
                    wsnew.get_Range("C34", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C34", m).Borders.Value = true;
                    wsnew.get_Range("C34", m).ColumnWidth = 110;
                    wsnew.get_Range("C34", m).Font.Name = "Arial";
                    wsnew.get_Range("C34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C35", m).Value2 = "Mention the Name of the employee.";
                    wsnew.get_Range("C35", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C35", m).Borders.Value = true;
                    wsnew.get_Range("C35", m).ColumnWidth = 110;
                    wsnew.get_Range("C35", m).Font.Name = "Arial";
                    wsnew.get_Range("C35", m).Font.Size = 10;
                    //
                    //wsnew.get_Range("C36", m).Value2 = "Mention the Category of the employee. G for General, W for Woman, S for Senior Citizen.";
                    wsnew.get_Range("C36", m).Value2 = "Mention the Category of the employee. G for General, W for Woman, S for Senior Citizen, O for Very Senior Citizen.";                    
                    wsnew.get_Range("C36", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C36", m).Borders.Value = true;
                    wsnew.get_Range("C36", m).ColumnWidth = 110;
                    wsnew.get_Range("C36", m).Font.Name = "Arial";
                    wsnew.get_Range("C36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C37", m).Value2 = "Mention the From date of period of employment.";
                    wsnew.get_Range("C37", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C37", m).Borders.Value = true;
                    wsnew.get_Range("C37", m).ColumnWidth = 110;
                    wsnew.get_Range("C37", m).Font.Name = "Arial";
                    wsnew.get_Range("C37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C38", m).Value2 = "Mention the From to of period of employment.";
                    wsnew.get_Range("C38", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C38", m).Borders.Value = true;
                    wsnew.get_Range("C38", m).ColumnWidth = 110;
                    wsnew.get_Range("C38", m).Font.Name = "Arial";
                    wsnew.get_Range("C38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C39", m).Value2 = "Mention the Total Salary of the Employee.";
                    wsnew.get_Range("C39", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C39", m).Borders.Value = true;
                    wsnew.get_Range("C39", m).ColumnWidth = 110;
                    wsnew.get_Range("C39", m).Font.Name = "Arial";
                    wsnew.get_Range("C39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C40", m).Value2 = "Mention the Gross Deduction under section 16(ii) of the Employee.";
                    wsnew.get_Range("C40", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C40", m).Borders.Value = true;
                    wsnew.get_Range("C40", m).ColumnWidth = 110;
                    wsnew.get_Range("C40", m).Font.Name = "Arial";
                    wsnew.get_Range("C40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C41", m).Value2 = "Mention the Total Salary of the Employee.";
                    wsnew.get_Range("C41", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C41", m).Borders.Value = true;
                    wsnew.get_Range("C41", m).ColumnWidth = 110;
                    wsnew.get_Range("C41", m).Font.Name = "Arial";
                    wsnew.get_Range("C41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C42", m).Value2 = "Mention the Gross Deduction under section 16(iii) of the Employee.";
                    wsnew.get_Range("C42", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C42", m).Borders.Value = true;
                    wsnew.get_Range("C42", m).ColumnWidth = 110;
                    wsnew.get_Range("C42", m).Font.Name = "Arial";
                    wsnew.get_Range("C42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C43", m).Value2 = "This field shows Income Chargeable under head Salaries.";
                    wsnew.get_Range("C43", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C43", m).Borders.Value = true;
                    wsnew.get_Range("C43", m).ColumnWidth = 110;
                    wsnew.get_Range("C43", m).Font.Name = "Arial";
                    wsnew.get_Range("C33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C44", m).Value2 = "Mention the Income other than Salary of the Employee.";
                    wsnew.get_Range("C44", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C44", m).Borders.Value = true;
                    wsnew.get_Range("C44", m).ColumnWidth = 110;
                    wsnew.get_Range("C44", m).Font.Name = "Arial";
                    wsnew.get_Range("C44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C45", m).Value2 = "This field shows Gross Total Income under head Salaries.";
                    wsnew.get_Range("C45", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C45", m).Borders.Value = true;
                    wsnew.get_Range("C45", m).ColumnWidth = 110;
                    wsnew.get_Range("C45", m).Font.Name = "Arial";
                    wsnew.get_Range("C45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C46", m).Value2 = "Mention the Deduction under Chapter VIA under section 80CCE of the Employee.";
                    wsnew.get_Range("C46", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C46", m).Borders.Value = true;
                    wsnew.get_Range("C46", m).ColumnWidth = 110;
                    wsnew.get_Range("C46", m).Font.Name = "Arial";
                    wsnew.get_Range("C46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C47", m).Value2 = "Mention the Deduction under Chapter VIA under section 80CCF of the Employee.";
                    wsnew.get_Range("C47", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C47", m).Borders.Value = true;
                    wsnew.get_Range("C47", m).ColumnWidth = 110;
                    wsnew.get_Range("C47", m).Font.Name = "Arial";
                    wsnew.get_Range("C47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C48", m).Value2 = "Mention the Deduction under Chapter VIA under Other sections of the Employee.";
                    wsnew.get_Range("C48", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C48", m).Borders.Value = true;
                    wsnew.get_Range("C48", m).ColumnWidth = 110;
                    wsnew.get_Range("C48", m).Font.Name = "Arial";
                    wsnew.get_Range("C48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C49", m).Value2 = "This field shows Gross Total Deduction under chapter VIA under head Salaries.";
                    wsnew.get_Range("C49", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C49", m).Borders.Value = true;
                    wsnew.get_Range("C49", m).ColumnWidth = 110;
                    wsnew.get_Range("C49", m).Font.Name = "Arial";
                    wsnew.get_Range("C49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C50", m).Value2 = "This field shows Gross Total Taxable Income under chapter VIA under head Salaries.";
                    wsnew.get_Range("C50", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C50", m).Borders.Value = true;
                    wsnew.get_Range("C50", m).ColumnWidth = 110;
                    wsnew.get_Range("C50", m).Font.Name = "Arial";
                    wsnew.get_Range("C50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C51", m).Value2 = "Mention the Income Tax on Total Income of the Employee.";
                    wsnew.get_Range("C51", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C51", m).Borders.Value = true;
                    wsnew.get_Range("C51", m).ColumnWidth = 110;
                    wsnew.get_Range("C51", m).Font.Name = "Arial";
                    wsnew.get_Range("C51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C52", m).Value2 = "Mention the Surcharge of the Employee.";
                    wsnew.get_Range("C52", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C52", m).Borders.Value = true;
                    wsnew.get_Range("C52", m).ColumnWidth = 110;
                    wsnew.get_Range("C52", m).Font.Name = "Arial";
                    wsnew.get_Range("C52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C53", m).Value2 = "Mention the Education Cess of the Employee.";
                    wsnew.get_Range("C53", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C53", m).Borders.Value = true;
                    wsnew.get_Range("C53", m).ColumnWidth = 110;
                    wsnew.get_Range("C53", m).Font.Name = "Arial";
                    wsnew.get_Range("C53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C54", m).Value2 = "Mention the Income Tax Releif under section 89 of the Employee.";
                    wsnew.get_Range("C54", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C54", m).Borders.Value = true;
                    wsnew.get_Range("C54", m).ColumnWidth = 110;
                    wsnew.get_Range("C54", m).Font.Name = "Arial";
                    wsnew.get_Range("C54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C55", m).Value2 = "Mention the Net Tax Payable of the Employee.";
                    wsnew.get_Range("C55", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C55", m).Borders.Value = true;
                    wsnew.get_Range("C55", m).ColumnWidth = 110;
                    wsnew.get_Range("C55", m).Font.Name = "Arial";
                    wsnew.get_Range("C55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C56", m).Value2 = "Mention the Total TDS Deducted  of the Employee.";
                    wsnew.get_Range("C56", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C56", m).Borders.Value = true;
                    wsnew.get_Range("C56", m).ColumnWidth = 110;
                    wsnew.get_Range("C56", m).Font.Name = "Arial";
                    wsnew.get_Range("C56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("C57", m).Value2 = "This field shows Shortfall/Excess Deduction of Tax.";
                    wsnew.get_Range("C57", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("C57", m).Borders.Value = true;
                    wsnew.get_Range("C57", m).ColumnWidth = 110;
                    wsnew.get_Range("C57", m).Font.Name = "Arial";
                    wsnew.get_Range("C57", m).Font.Size = 10;
                    //
                    #endregion
                    //
                    #region Format
                    wsnew.get_Range("D33", m).Value2 = "Numeric";
                    wsnew.get_Range("D33", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D33", m).Borders.Value = true;
                    wsnew.get_Range("D33", m).ColumnWidth = 9;
                    wsnew.get_Range("D33", m).Font.Name = "Drial";
                    wsnew.get_Range("D33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D34", m).Value2 = "Text";
                    wsnew.get_Range("D34", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D34", m).Borders.Value = true;
                    wsnew.get_Range("D34", m).ColumnWidth = 9;
                    wsnew.get_Range("D34", m).Font.Name = "Drial";
                    wsnew.get_Range("D34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D35", m).Value2 = "Text";
                    wsnew.get_Range("D35", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D35", m).Borders.Value = true;
                    wsnew.get_Range("D35", m).ColumnWidth = 9;
                    wsnew.get_Range("D35", m).Font.Name = "Drial";
                    wsnew.get_Range("D35", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D36", m).Value2 = "Numeric";
                    wsnew.get_Range("D36", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D36", m).Borders.Value = true;
                    wsnew.get_Range("D36", m).ColumnWidth = 9;
                    wsnew.get_Range("D36", m).Font.Name = "Drial";
                    wsnew.get_Range("D36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D37", m).Value2 = "Date";
                    wsnew.get_Range("D37", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D37", m).Borders.Value = true;
                    wsnew.get_Range("D37", m).ColumnWidth = 9;
                    wsnew.get_Range("D37", m).Font.Name = "Drial";
                    wsnew.get_Range("D37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D38", m).Value2 = "Date";
                    wsnew.get_Range("D38", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D38", m).Borders.Value = true;
                    wsnew.get_Range("D38", m).ColumnWidth = 9;
                    wsnew.get_Range("D38", m).Font.Name = "Drial";
                    wsnew.get_Range("D38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D39", m).Value2 = "Numeric";
                    wsnew.get_Range("D39", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D39", m).Borders.Value = true;
                    wsnew.get_Range("D39", m).ColumnWidth = 9;
                    wsnew.get_Range("D39", m).Font.Name = "Drial";
                    wsnew.get_Range("D39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D40", m).Value2 = "Numeric";
                    wsnew.get_Range("D40", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D40", m).Borders.Value = true;
                    wsnew.get_Range("D40", m).ColumnWidth = 9;
                    wsnew.get_Range("D40", m).Font.Name = "Drial";
                    wsnew.get_Range("D40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D41", m).Value2 = "Numeric";
                    wsnew.get_Range("D41", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D41", m).Borders.Value = true;
                    wsnew.get_Range("D41", m).ColumnWidth = 9;
                    wsnew.get_Range("D41", m).Font.Name = "Drial";
                    wsnew.get_Range("D41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D42", m).Value2 = "Numeric";
                    wsnew.get_Range("D42", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D42", m).Borders.Value = true;
                    wsnew.get_Range("D42", m).ColumnWidth = 9;
                    wsnew.get_Range("D42", m).Font.Name = "Drial";
                    wsnew.get_Range("D42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D43", m).Value2 = "Numeric";
                    wsnew.get_Range("D43", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D43", m).Borders.Value = true;
                    wsnew.get_Range("D43", m).ColumnWidth = 9;
                    wsnew.get_Range("D43", m).Font.Name = "Drial";
                    wsnew.get_Range("D43", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D44", m).Value2 = "Numeric";
                    wsnew.get_Range("D44", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D44", m).Borders.Value = true;
                    wsnew.get_Range("D44", m).ColumnWidth = 9;
                    wsnew.get_Range("D44", m).Font.Name = "Drial";
                    wsnew.get_Range("D44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D45", m).Value2 = "Numeric";
                    wsnew.get_Range("D45", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D45", m).Borders.Value = true;
                    wsnew.get_Range("D45", m).ColumnWidth = 9;
                    wsnew.get_Range("D45", m).Font.Name = "Drial";
                    wsnew.get_Range("D45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D46", m).Value2 = "Numeric";
                    wsnew.get_Range("D46", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D46", m).Borders.Value = true;
                    wsnew.get_Range("D46", m).ColumnWidth = 9;
                    wsnew.get_Range("D46", m).Font.Name = "Drial";
                    wsnew.get_Range("D46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D47", m).Value2 = "Numeric";
                    wsnew.get_Range("D47", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D47", m).Borders.Value = true;
                    wsnew.get_Range("D47", m).ColumnWidth = 9;
                    wsnew.get_Range("D47", m).Font.Name = "Drial";
                    wsnew.get_Range("D47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D48", m).Value2 = "Numeric";
                    wsnew.get_Range("D48", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D48", m).Borders.Value = true;
                    wsnew.get_Range("D48", m).ColumnWidth = 9;
                    wsnew.get_Range("D48", m).Font.Name = "Drial";
                    wsnew.get_Range("D48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D49", m).Value2 = "Numeric";
                    wsnew.get_Range("D49", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D49", m).Borders.Value = true;
                    wsnew.get_Range("D49", m).ColumnWidth = 9;
                    wsnew.get_Range("D49", m).Font.Name = "Drial";
                    wsnew.get_Range("D49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D50", m).Value2 = "Numeric";
                    wsnew.get_Range("D50", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D50", m).Borders.Value = true;
                    wsnew.get_Range("D50", m).ColumnWidth = 9;
                    wsnew.get_Range("D50", m).Font.Name = "Drial";
                    wsnew.get_Range("D50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D51", m).Value2 = "Numeric";
                    wsnew.get_Range("D51", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D51", m).Borders.Value = true;
                    wsnew.get_Range("D51", m).ColumnWidth = 9;
                    wsnew.get_Range("D51", m).Font.Name = "Drial";
                    wsnew.get_Range("D51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D52", m).Value2 = "Numeric";
                    wsnew.get_Range("D52", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D52", m).Borders.Value = true;
                    wsnew.get_Range("D52", m).ColumnWidth = 9;
                    wsnew.get_Range("D52", m).Font.Name = "Drial";
                    wsnew.get_Range("D52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D53", m).Value2 = "Numeric";
                    wsnew.get_Range("D53", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D53", m).Borders.Value = true;
                    wsnew.get_Range("D53", m).ColumnWidth = 9;
                    wsnew.get_Range("D53", m).Font.Name = "Drial";
                    wsnew.get_Range("D53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D54", m).Value2 = "Numeric";
                    wsnew.get_Range("D54", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D54", m).Borders.Value = true;
                    wsnew.get_Range("D54", m).ColumnWidth = 9;
                    wsnew.get_Range("D54", m).Font.Name = "Drial";
                    wsnew.get_Range("D54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D55", m).Value2 = "Numeric";
                    wsnew.get_Range("D55", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D55", m).Borders.Value = true;
                    wsnew.get_Range("D55", m).ColumnWidth = 9;
                    wsnew.get_Range("D55", m).Font.Name = "Drial";
                    wsnew.get_Range("D55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D56", m).Value2 = "Numeric";
                    wsnew.get_Range("D56", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D56", m).Borders.Value = true;
                    wsnew.get_Range("D56", m).ColumnWidth = 9;
                    wsnew.get_Range("D56", m).Font.Name = "Drial";
                    wsnew.get_Range("D56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("D57", m).Value2 = "Numeric";
                    wsnew.get_Range("D57", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("D57", m).Borders.Value = true;
                    wsnew.get_Range("D57", m).ColumnWidth = 9;
                    wsnew.get_Range("D57", m).Font.Name = "Drial";
                    wsnew.get_Range("D57", m).Font.Size = 10;
                    //
                    #endregion
                    //
                    #region M / O
                    wsnew.get_Range("E33", m).Value2 = "Mandatory";
                    wsnew.get_Range("E33", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E33", m).Borders.Value = true;
                    wsnew.get_Range("E33", m).ColumnWidth = 9;
                    wsnew.get_Range("E33", m).Font.Name = "Arial";
                    wsnew.get_Range("E33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E34", m).Value2 = "Mandatory";
                    wsnew.get_Range("E34", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E34", m).Borders.Value = true;
                    wsnew.get_Range("E34", m).ColumnWidth = 9;
                    wsnew.get_Range("E34", m).Font.Name = "Arial";
                    wsnew.get_Range("E34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E35", m).Value2 = "Mandatory";
                    wsnew.get_Range("E35", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E35", m).Borders.Value = true;
                    wsnew.get_Range("E35", m).ColumnWidth = 9;
                    wsnew.get_Range("E35", m).Font.Name = "Arial";
                    wsnew.get_Range("E35", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E36", m).Value2 = "Mandatory";
                    wsnew.get_Range("E36", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E36", m).Borders.Value = true;
                    wsnew.get_Range("E36", m).ColumnWidth = 9;
                    wsnew.get_Range("E36", m).Font.Name = "Arial";
                    wsnew.get_Range("E36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E37", m).Value2 = "Mandatory";
                    wsnew.get_Range("E37", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E37", m).Borders.Value = true;
                    wsnew.get_Range("E37", m).ColumnWidth = 9;
                    wsnew.get_Range("E37", m).Font.Name = "Arial";
                    wsnew.get_Range("E37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E38", m).Value2 = "Mandatory";
                    wsnew.get_Range("E38", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E38", m).Borders.Value = true;
                    wsnew.get_Range("E38", m).ColumnWidth = 9;
                    wsnew.get_Range("E38", m).Font.Name = "Arial";
                    wsnew.get_Range("E38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E39", m).Value2 = "Mandatory";
                    wsnew.get_Range("E39", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E39", m).Borders.Value = true;
                    wsnew.get_Range("E39", m).ColumnWidth = 9;
                    wsnew.get_Range("E39", m).Font.Name = "Arial";
                    wsnew.get_Range("E39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E40", m).Value2 = "Optional";
                    wsnew.get_Range("E40", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E40", m).Borders.Value = true;
                    wsnew.get_Range("E40", m).ColumnWidth = 9;
                    wsnew.get_Range("E40", m).Font.Name = "Arial";
                    wsnew.get_Range("E40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E41", m).Value2 = "Optional";
                    wsnew.get_Range("E41", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E41", m).Borders.Value = true;
                    wsnew.get_Range("E41", m).ColumnWidth = 9;
                    wsnew.get_Range("E41", m).Font.Name = "Arial";
                    wsnew.get_Range("E41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E42", m).Value2 = "Optional";
                    wsnew.get_Range("E42", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E42", m).Borders.Value = true;
                    wsnew.get_Range("E42", m).ColumnWidth = 9;
                    wsnew.get_Range("E42", m).Font.Name = "Arial";
                    wsnew.get_Range("E42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E43", m).Value2 = "Mandatory";
                    wsnew.get_Range("E43", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E43", m).Borders.Value = true;
                    wsnew.get_Range("E43", m).ColumnWidth = 9;
                    wsnew.get_Range("E43", m).Font.Name = "Arial";
                    wsnew.get_Range("E43", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E44", m).Value2 = "Optional";
                    wsnew.get_Range("E44", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E44", m).Borders.Value = true;
                    wsnew.get_Range("E44", m).ColumnWidth = 9;
                    wsnew.get_Range("E44", m).Font.Name = "Arial";
                    wsnew.get_Range("E44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E45", m).Value2 = "Mandatory";
                    wsnew.get_Range("E45", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E45", m).Borders.Value = true;
                    wsnew.get_Range("E45", m).ColumnWidth = 9;
                    wsnew.get_Range("E45", m).Font.Name = "Arial";
                    wsnew.get_Range("E45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E46", m).Value2 = "Optional";
                    wsnew.get_Range("E46", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E46", m).Borders.Value = true;
                    wsnew.get_Range("E46", m).ColumnWidth = 9;
                    wsnew.get_Range("E46", m).Font.Name = "Arial";
                    wsnew.get_Range("E46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E47", m).Value2 = "Optional";
                    wsnew.get_Range("E47", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E47", m).Borders.Value = true;
                    wsnew.get_Range("E47", m).ColumnWidth = 9;
                    wsnew.get_Range("E47", m).Font.Name = "Arial";
                    wsnew.get_Range("E47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E48", m).Value2 = "Optional";
                    wsnew.get_Range("E48", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E48", m).Borders.Value = true;
                    wsnew.get_Range("E48", m).ColumnWidth = 9;
                    wsnew.get_Range("E48", m).Font.Name = "Arial";
                    wsnew.get_Range("E48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E49", m).Value2 = "Optional";
                    wsnew.get_Range("E49", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E49", m).Borders.Value = true;
                    wsnew.get_Range("E49", m).ColumnWidth = 9;
                    wsnew.get_Range("E49", m).Font.Name = "Arial";
                    wsnew.get_Range("E49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E50", m).Value2 = "Mandatory";
                    wsnew.get_Range("E50", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E50", m).Borders.Value = true;
                    wsnew.get_Range("E50", m).ColumnWidth = 9;
                    wsnew.get_Range("E50", m).Font.Name = "Arial";
                    wsnew.get_Range("E50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E51", m).Value2 = "Optional";
                    wsnew.get_Range("E51", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E51", m).Borders.Value = true;
                    wsnew.get_Range("E51", m).ColumnWidth = 9;
                    wsnew.get_Range("E51", m).Font.Name = "Arial";
                    wsnew.get_Range("E51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E52", m).Value2 = "Optional";
                    wsnew.get_Range("E52", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E52", m).Borders.Value = true;
                    wsnew.get_Range("E52", m).ColumnWidth = 9;
                    wsnew.get_Range("E52", m).Font.Name = "Arial";
                    wsnew.get_Range("E52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E53", m).Value2 = "Optional";
                    wsnew.get_Range("E53", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E53", m).Borders.Value = true;
                    wsnew.get_Range("E53", m).ColumnWidth = 9;
                    wsnew.get_Range("E53", m).Font.Name = "Arial";
                    wsnew.get_Range("E53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E54", m).Value2 = "Optional";
                    wsnew.get_Range("E54", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E54", m).Borders.Value = true;
                    wsnew.get_Range("E54", m).ColumnWidth = 9;
                    wsnew.get_Range("E54", m).Font.Name = "Arial";
                    wsnew.get_Range("E54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E55", m).Value2 = "Optional";
                    wsnew.get_Range("E55", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E55", m).Borders.Value = true;
                    wsnew.get_Range("E55", m).ColumnWidth = 9;
                    wsnew.get_Range("E55", m).Font.Name = "Arial";
                    wsnew.get_Range("E55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E56", m).Value2 = "Optional";
                    wsnew.get_Range("E56", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E56", m).Borders.Value = true;
                    wsnew.get_Range("E56", m).ColumnWidth = 9;
                    wsnew.get_Range("E56", m).Font.Name = "Arial";
                    wsnew.get_Range("E56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("E57", m).Value2 = "Optional";
                    wsnew.get_Range("E57", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("E57", m).Borders.Value = true;
                    wsnew.get_Range("E57", m).ColumnWidth = 9;
                    wsnew.get_Range("E57", m).Font.Name = "Arial";
                    wsnew.get_Range("E57", m).Font.Size = 10;
                    //
                    #endregion
                    //
                    #region Max Length
                    wsnew.get_Range("F33", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F33", m).Borders.Value = true;
                    wsnew.get_Range("F33", m).ColumnWidth = 9;
                    wsnew.get_Range("F33", m).Font.Name = "Arial";
                    wsnew.get_Range("F33", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F34", m).Value2 = "10";
                    wsnew.get_Range("F34", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F34", m).Borders.Value = true;
                    wsnew.get_Range("F34", m).ColumnWidth = 9;
                    wsnew.get_Range("F34", m).Font.Name = "Arial";
                    wsnew.get_Range("F34", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F35", m).Value2 = "75";
                    wsnew.get_Range("F35", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F35", m).Borders.Value = true;
                    wsnew.get_Range("F35", m).ColumnWidth = 9;
                    wsnew.get_Range("F35", m).Font.Name = "Arial";
                    wsnew.get_Range("F35", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F36", m).Value2 = "1";
                    wsnew.get_Range("F36", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F36", m).Borders.Value = true;
                    wsnew.get_Range("F36", m).ColumnWidth = 9;
                    wsnew.get_Range("F36", m).Font.Name = "Arial";
                    wsnew.get_Range("F36", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F37", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F37", m).Borders.Value = true;
                    wsnew.get_Range("F37", m).ColumnWidth = 9;
                    wsnew.get_Range("F37", m).Font.Name = "Arial";
                    wsnew.get_Range("F37", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F38", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F38", m).Borders.Value = true;
                    wsnew.get_Range("F38", m).ColumnWidth = 9;
                    wsnew.get_Range("F38", m).Font.Name = "Arial";
                    wsnew.get_Range("F38", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F39", m).Value2 = "15";
                    wsnew.get_Range("F39", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F39", m).Borders.Value = true;
                    wsnew.get_Range("F39", m).ColumnWidth = 9;
                    wsnew.get_Range("F39", m).Font.Name = "Arial";
                    wsnew.get_Range("F39", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F40", m).Value2 = "15";
                    wsnew.get_Range("F40", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F40", m).Borders.Value = true;
                    wsnew.get_Range("F40", m).ColumnWidth = 9;
                    wsnew.get_Range("F40", m).Font.Name = "Arial";
                    wsnew.get_Range("F40", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F41", m).Value2 = "15";
                    wsnew.get_Range("F41", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F41", m).Borders.Value = true;
                    wsnew.get_Range("F41", m).ColumnWidth = 9;
                    wsnew.get_Range("F41", m).Font.Name = "Arial";
                    wsnew.get_Range("F41", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F42", m).Value2 = "15";
                    wsnew.get_Range("F42", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F42", m).Borders.Value = true;
                    wsnew.get_Range("F42", m).ColumnWidth = 9;
                    wsnew.get_Range("F42", m).Font.Name = "Arial";
                    wsnew.get_Range("F42", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F43", m).Value2 = "15";
                    wsnew.get_Range("F43", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F43", m).Borders.Value = true;
                    wsnew.get_Range("F43", m).ColumnWidth = 9;
                    wsnew.get_Range("F43", m).Font.Name = "Arial";
                    wsnew.get_Range("F43", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F44", m).Value2 = "15";
                    wsnew.get_Range("F44", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F44", m).Borders.Value = true;
                    wsnew.get_Range("F44", m).ColumnWidth = 9;
                    wsnew.get_Range("F44", m).Font.Name = "Arial";
                    wsnew.get_Range("F44", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F45", m).Value2 = "15";
                    wsnew.get_Range("F45", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F45", m).Borders.Value = true;
                    wsnew.get_Range("F45", m).ColumnWidth = 9;
                    wsnew.get_Range("F45", m).Font.Name = "Arial";
                    wsnew.get_Range("F45", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F46", m).Value2 = "15";
                    wsnew.get_Range("F46", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F46", m).Borders.Value = true;
                    wsnew.get_Range("F46", m).ColumnWidth = 9;
                    wsnew.get_Range("F46", m).Font.Name = "Arial";
                    wsnew.get_Range("F46", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F47", m).Value2 = "15";
                    wsnew.get_Range("F47", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F47", m).Borders.Value = true;
                    wsnew.get_Range("F47", m).ColumnWidth = 9;
                    wsnew.get_Range("F47", m).Font.Name = "Arial";
                    wsnew.get_Range("F47", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F48", m).Value2 = "15";
                    wsnew.get_Range("F48", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F48", m).Borders.Value = true;
                    wsnew.get_Range("F48", m).ColumnWidth = 9;
                    wsnew.get_Range("F48", m).Font.Name = "Arial";
                    wsnew.get_Range("F48", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F49", m).Value2 = "15";
                    wsnew.get_Range("F49", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F49", m).Borders.Value = true;
                    wsnew.get_Range("F49", m).ColumnWidth = 9;
                    wsnew.get_Range("F49", m).Font.Name = "Arial";
                    wsnew.get_Range("F49", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F50", m).Value2 = "15";
                    wsnew.get_Range("F50", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F50", m).Borders.Value = true;
                    wsnew.get_Range("F50", m).ColumnWidth = 9;
                    wsnew.get_Range("F50", m).Font.Name = "Arial";
                    wsnew.get_Range("F50", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F51", m).Value2 = "15";
                    wsnew.get_Range("F51", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F51", m).Borders.Value = true;
                    wsnew.get_Range("F51", m).ColumnWidth = 9;
                    wsnew.get_Range("F51", m).Font.Name = "Arial";
                    wsnew.get_Range("F51", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F52", m).Value2 = "15";
                    wsnew.get_Range("F52", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F52", m).Borders.Value = true;
                    wsnew.get_Range("F52", m).ColumnWidth = 9;
                    wsnew.get_Range("F52", m).Font.Name = "Arial";
                    wsnew.get_Range("F52", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F53", m).Value2 = "15";
                    wsnew.get_Range("F53", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F53", m).Borders.Value = true;
                    wsnew.get_Range("F53", m).ColumnWidth = 9;
                    wsnew.get_Range("F53", m).Font.Name = "Arial";
                    wsnew.get_Range("F53", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F54", m).Value2 = "15";
                    wsnew.get_Range("F54", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F54", m).Borders.Value = true;
                    wsnew.get_Range("F54", m).ColumnWidth = 9;
                    wsnew.get_Range("F54", m).Font.Name = "Arial";
                    wsnew.get_Range("F54", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F55", m).Value2 = "15";
                    wsnew.get_Range("F55", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F55", m).Borders.Value = true;
                    wsnew.get_Range("F55", m).ColumnWidth = 9;
                    wsnew.get_Range("F55", m).Font.Name = "Arial";
                    wsnew.get_Range("F55", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F56", m).Value2 = "15";
                    wsnew.get_Range("F56", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F56", m).Borders.Value = true;
                    wsnew.get_Range("F56", m).ColumnWidth = 9;
                    wsnew.get_Range("F56", m).Font.Name = "Arial";
                    wsnew.get_Range("F56", m).Font.Size = 10;
                    //
                    wsnew.get_Range("F57", m).Value2 = "15";
                    wsnew.get_Range("F57", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    wsnew.get_Range("F57", m).Borders.Value = true;
                    wsnew.get_Range("F57", m).ColumnWidth = 9;
                    wsnew.get_Range("F57", m).Font.Name = "Arial";
                    wsnew.get_Range("F57", m).Font.Size = 10;
                    //
                    #endregion
                }
                #endregion
                //
                //wsnew.get_Range("A1", "F61").Locked = false;
                //wsnew.get_Range("A1", "F61").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //                        
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #region WRITE STATE WORKSHEET
        private bool WRITE_STATE_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                IDataReader drdGetSheetRecord = null;
                //--
                if (KILL_EXCEL() == false)
                    return false;
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("B1", m).Value2 = "STATE NAME";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT STATE_ID," +
                    "            STATE_NAME " +
                    "     FROM   MST_STATE " +
                    "     ORDER BY STATE_NAME";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["STATE_NAME"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //
                //wsnew.get_Range("A1", "M18").Locked = false;
                //wsnew.get_Range("A1", "M18").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //                
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #region WRITE DEDUCTOR WORKSHEET
        private bool WRITE_DEDUCTOR_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                IDataReader drdGetSheetRecord = null;
                //--
                if (KILL_EXCEL() == false)
                    return false;
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("A1", m).Value2 = "CATEGORY CODE";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 65;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "CATEGORY DESCRIPTION";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT CATEGORY_CODE," +
                    "            CATEGORY_DESCRIPTION " +
                    "     FROM   MST_CATEGORY " +
                    "     ORDER BY CATEGORY_CODE";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["CATEGORY_CODE"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["CATEGORY_DESCRIPTION"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #region WRITE MINISTRY WORKSHEET
        private bool WRITE_MINISTRY_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                IDataReader drdGetSheetRecord = null;
                //--
                if (KILL_EXCEL() == false)
                    return false;
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("B1", m).Value2 = "MINISTRY NAME";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT MINISTRY_NAME " +
                    "     FROM   MST_MINISTRY " +
                    "     ORDER BY MINISTRY_NAME";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                    return false;
                //
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["MINISTRY_NAME"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion


        #region WRITE COUNTRY WORKSHEET
        private bool WRITE_COUNTRY_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                IDataReader drdGetSheetRecord = null;
                //--
                if (KILL_EXCEL() == false)
                    return false;
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("A1", m).Value2 = "COUNTRY";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 55;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "COUNTRY CODE";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 25;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT COUNTRY_ID," +
                    "            COUNTRY_DESC," +
                    //"            CSTR(COUNTRY_CODE) AS COUNTRY_CODE " +
                    "           " + cmnService.J_SQLDBFormat("COUNTRY_CODE", J_SQLColFormat.ConvertToString) + " AS COUNTRY_CODE " +
                    "     FROM   MST_COUNTRY ";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["COUNTRY_DESC"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = "'" + Convert.ToString(drdGetSheetRecord["COUNTRY_CODE"]);
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                //
                //wsnew.get_Range("A1", "M18").Locked = false;
                //wsnew.get_Range("A1", "M18").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //                
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #region WRITE REMITTANCE WORKSHEET
        private bool WRITE_REMITTANCE_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                IDataReader drdGetSheetRecord = null;
                //--
                if (KILL_EXCEL() == false)
                    return false;
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("A1", m).Value2 = "REMITTANCE";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 55;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "REMITTANCE CODE";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 25;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT REMITTANCE_ID," +
                    "            REMITTANCE_DESC," +
                    "            REMITTANCE_CODE " +
                    "     FROM   MST_REMITTANCE " +
                    "     WHERE  INACTIVE_FLAG = 0";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["REMITTANCE_DESC"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["REMITTANCE_CODE"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                //
                //wsnew.get_Range("A1", "M18").Locked = false;
                //wsnew.get_Range("A1", "M18").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //                
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #region WRITE TDS_RATE_ACT WORKSHEET
        private bool WRITE_TDS_RATE_ACT_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                IDataReader drdGetSheetRecord = null;
                //--
                if (KILL_EXCEL() == false)
                    return false;
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("A1", m).Value2 = "TDS RATE ACT";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 50;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "TDS RATE ACT CODE";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 25;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT TDS_APPLICABILITY_ID," +
                    "            TDS_APPLICABILITY_DESC," +
                    "            TDS_APPLICABILITY_CODE " +
                    "     FROM   MST_TDS_APPLICABILITY ";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["TDS_APPLICABILITY_DESC"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["TDS_APPLICABILITY_CODE"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                //
                //wsnew.get_Range("A1", "M18").Locked = false;
                //wsnew.get_Range("A1", "M18").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //                
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #endregion

        #region PROTECT WORKSHEET
        private bool PROTECT_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //

                //wsnew.get_Range("A1", "M100").Locked = false;
                //wsnew.get_Range("A1", "M1").Locked = false;
                //wsnew.get_Range("A1", "M1").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #region KILL EXCEL
        private bool KILL_EXCEL()
        {
            try
            {
                //--
                //foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                //{
                //    if (process.MainModule.ModuleName.ToUpper().Equals("EXCEL.EXE"))
                //    {
                //        process.Kill();
                //        //process.Close();
                //        //process.Dispose();
                //        break;
                //    }
                //}
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region LoadImportType
        public void LoadImportType()
        {
            if (rbnCSV.Checked == true)
            {
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                {
                    if (cmbFinancialYear.SelectedIndex > 0 && Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2026_27ID)
                    {
                        string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")", T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")", T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")", T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")", T_FormNo.F24QSalaryDetails, strDeducteeMaster, strEmployeeMaster };
                        dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
                    }
                    else
                    {
                        string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, strDeducteeMaster, strEmployeeMaster };
                        dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
                    }
                }
                else
                {
                    if (cmbFinancialYear.SelectedIndex > 0 && Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2026_27ID)
                    {
                        string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")", T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")", T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")", T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")", T_FormNo.F24QSalaryDetails, T_FormNo.F24QForm16SalaryDetails, strDeducteeMaster, strEmployeeMaster };
                        dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
                    }
                    else
                    {
                        string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QForm16SalaryDetails, strDeducteeMaster, strEmployeeMaster };
                        dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
                    }

                }
            }
            else if (rbnExcel.Checked == true)
            {
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                {
                    if (cmbFinancialYear.SelectedIndex > 0 && Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2026_27ID)
                    {
                        string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")", T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")", T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")", T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")", T_FormNo.F24QSalaryDetails, T_FormNo.F24QAnnexureIII, strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
                        dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
                    }
                    else
                    {
                        string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QAnnexureIII, strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
                        dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
                    }
                }
                else
                {
                    if (cmbFinancialYear.SelectedIndex > 0 && Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2026_27ID)
                    {
                        string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")", T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")", T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")", T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")", T_FormNo.F24QSalaryDetails, T_FormNo.F24QForm16SalaryDetails, T_FormNo.F24QAnnexureIII, strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
                        dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
                    }
                    else
                    {
                        string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QForm16SalaryDetails, T_FormNo.F24QAnnexureIII, strCompanyMaster, strDeducteeMaster, strEmployeeMaster };
                        dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
                    }
                }
            }
        }
        #endregion

        #region SetDestinationFileName
        public void SetDestinationFileName()
        {
            lblImportTypeCaption.Text = "";
            string strFileType = "";
            //
            if (blnUpdateTextfileName == true || txtDestinationFileName.Text.Trim() == "")
            {
                if (rbnExcel.Checked == true)
                {
                    if (cmbFinancialYear.SelectedIndex <= 0 || cmbFormNo.SelectedIndex <= 0 || cmbFileType.SelectedIndex <= 0)
                    {
                        txtDestinationFileName.Text = "";
                        return;
                    }
                    strFileType = cmbFileType.Text.ToUpper();
                }
                else if (rbnCSV.Checked == true)
                {
                    if (cmbFinancialYear.SelectedIndex <= 0 || cmbFormNo.SelectedIndex <= 0 )//|| cmbFileType.SelectedIndex <= 0)
                    {
                        txtDestinationFileName.Text = "";
                        return;
                    }
                    strFileType = "CSV";
                }
                //--
                strFormNo = "";
                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2026_27ID)
                {
                    if (cmbFormNo.Text == T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")")
                        strFormNo = T_FormNo.F24Q;
                    else if (cmbFormNo.Text == T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")")
                        strFormNo = T_FormNo.F26Q;
                    else if (cmbFormNo.Text == T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")")
                        strFormNo = T_FormNo.F27Q;
                    else if (cmbFormNo.Text == T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")")
                        strFormNo = T_FormNo.F27EQ;
                    else
                        strFormNo = cmbFormNo.Text;
                }
                else
                {
                    strFormNo = cmbFormNo.Text;
                }
                //
                //------------------------------------------------------------------
                //-GENERATE FILE NAME F.Y - WISE NEW CODE 
                //------------------------------------------------------------------
                if (strFormNo == strDeducteeMaster)
                    txtDestinationFileName.Text = strFileType + "_DEDUCTEE_MASTER";
                else if (strFormNo == strEmployeeMaster)
                    txtDestinationFileName.Text = strFileType + "_EMPLOYEE_MASTER";
                else if (strFormNo == strCompanyMaster) //-- ANIK@2014-09-12
                    txtDestinationFileName.Text = strFileType + "_COMPANY_MASTER";
                else if (strFormNo == T_FormNo.F24QSalaryDetails)
                    txtDestinationFileName.Text = strFileType + "_BLANK" + "_SD" + cmnService.J_Right(cmbFinancialYear.Text, 5).Replace("-", "");
                else if (strFormNo == T_FormNo.F24QAnnexureIII)
                {
                    txtDestinationFileName.Text = strFileType + "_BLANK" + "_ANNEXURE_III";// + cmnService.J_Right(cmbFinancialYear.Text, 5).Replace("-", "");
                    lblImportTypeCaption.Text = "This is applicable only for 'Specified Banks' handling Pension of Senior Citizens.";
                }
                else
                {
                    //For Financial Year 2013-14 and onwards
                    //if (cmbFinancialYear.SelectedIndex == (int)FinancialYearSelection.FY1314Onwards)
                    //-- 2016/04/22
                    //if (cmbFinancialYear.SelectedIndex >= (int)T_FinancialYearID.F2013_14ID)
                    //-- 2016/04/22
                    //if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                    //    txtDestinationFileName.Text = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1314";
                    //else
                    //    txtDestinationFileName.Text = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "1213";
                    txtDestinationFileName.Text = strFileType + "_BLANK" + cmbFormNo.Text.ToUpper() + cmnService.J_Right(cmbFinancialYear.Text, 5).Replace("-", "");
                }
                //--------------------------------------------------------------------
            }
        }

        #endregion

        //-- Added By Abhishek Dey On 04/04/2018 --
        #region WRITE COMPANY DETAILS WORKSHEET
        private bool WRITE_COMPANY_DETAILS_WORKSHEET(string ExcelFilePath, string SheetName, string FormName)
        {
            try
            {
                //--
                if (KILL_EXCEL() == false)
                    return false;
                //--
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

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //                
                //wsnew.get_Range("A1", m).Value2 = "Please enter the Company details of which you are doing excel import.";
                wsnew.get_Range("A1", m).Value2 = "Provide TAN information for the data import";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                //wsnew.get_Range("A1", "F1").Borders.Value = true;
                wsnew.Range["A1:F1"].Merge();
                wsnew.get_Range("A1", m).ColumnWidth = 55;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", "C1").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                //
                wsnew.get_Range("A2", m).Value2 = "COMPANY TAN";
                wsnew.get_Range("A2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A2", m).Borders.Value = true;
                wsnew.get_Range("A2", m).ColumnWidth = 35;
                wsnew.get_Range("A2", m).WrapText = true;
                wsnew.get_Range("A2", m).Font.Name = "Arial";
                wsnew.get_Range("A2", m).Font.Bold = true;
                wsnew.get_Range("A2", m).Font.Size = 10;
                wsnew.get_Range("A2", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 153));
                //                
                wsnew.get_Range("B2", m).Value2 = "COMPANY PAN";
                wsnew.get_Range("B2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B2", m).Borders.Value = true;
                wsnew.get_Range("B2", m).ColumnWidth = 35;
                wsnew.get_Range("B2", m).WrapText = true;
                wsnew.get_Range("B2", m).Font.Name = "Arial";
                wsnew.get_Range("B2", m).Font.Bold = true;
                wsnew.get_Range("B2", m).Font.Size = 10;
                wsnew.get_Range("B2", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(204, 255, 204));
                //wsnew.get_Range("C8", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                //
                wsnew.get_Range("C2", m).Value2 = "COMPANY NAME";
                wsnew.get_Range("C2", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C2", m).Borders.Value = true;
                wsnew.get_Range("C2", m).ColumnWidth = 55;
                wsnew.get_Range("C2", m).WrapText = true;
                wsnew.get_Range("C2", m).Font.Name = "Arial";
                wsnew.get_Range("C2", m).Font.Bold = true;
                wsnew.get_Range("C2", m).Font.Size = 10;
                wsnew.get_Range("C2", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(204, 255, 204));
                //wsnew.get_Range("B10", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);               
                //                        
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }


        #endregion


        #region UNZIP FILE AND RETURN STRING VALUE [ OVERLOADED METHOD ]

        #region J_UnZipString [1]
        public string J_UnZipString(string FilePath)
        {
            return this.J_UnZipString(FilePath, "");
        }
        #endregion

        #region J_UnZipString
        public string J_UnZipString(string FilePath, string password)
        {
            int iCounter = 0;
            string strFolder = string.Empty;

            try
            {
                string strFolderPath = cmnService.J_GetDirectoryName(FilePath);
                string strFileName = cmnService.J_GetFileName(FilePath);

                ICSharpCode.SharpZipLib.Zip.ZipEntry theEntry;
                //ZipEntry theEntry;
                string tmpEntry = String.Empty;
                ZipInputStream s = new ZipInputStream(File.OpenRead(FilePath));

                if (password != null && password != String.Empty)
                    s.Password = password;

                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory 
                    if (strFolderPath != "")
                        Directory.CreateDirectory(strFolderPath);

                    if (fileName != String.Empty)
                    {
                        if (theEntry.Name.IndexOf(".ini") < 0)
                        {
                            string fullPath = strFolderPath + "\\" + theEntry.Name;
                            fullPath = fullPath.Replace("\\ ", "\\");
                            string fullDirPath = Path.GetDirectoryName(fullPath);

                            if (iCounter == 0)
                            {
                                strFolder = fullDirPath;
                                iCounter = 1;
                            }

                            if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);

                            FileStream streamWriter = File.Create(fullPath);
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                    streamWriter.Write(data, 0, size);
                                else
                                    break;
                            }
                            streamWriter.Close();
                        }
                    }
                }
                s.Close();
                return strFolder;
            }
            catch (Exception exception)
            {
                cmnService.J_UserMessage(exception.Message);
                return strFolder;
            }
        }
        #endregion

        #endregion

        #endregion
    }

}

