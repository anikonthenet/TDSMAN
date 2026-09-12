#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: MstDeductee
Version			: 1.0
Start Date		: 18-11-2010
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
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Collections.Generic;


//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormTrn;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

//using EASendMail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading;

#endregion


namespace TDSMAN.FormEmail
{
    public partial class SysSendEmail : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public SysSendEmail()
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
        long lngSearchId;					//For Storing the Id
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        string strTempMode;

        int intCountRecords = 0;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        ToolTip tllTip = new ToolTip();

        int intCompanyIndex;
        long lngBasicInfoID = 0;
        //--
        string strFromEmailID = "";
        string strFromDisplayName = "";
        string strSMTPUserName = "";
        string strSMTPPassword = "";
        string strSMTPPort = "";
        string strSSL = "";
        string strServerAuthentication = "" , strDisableSSLTLS = "";
        string strSMTPHost = "";
        string strEmailReplyTo = "";
        string strEmailCC = "";
        string strEmailCC2 = "";
        string strEmailBCC = "";
        string strEmailBCC2 = "";
        //
        string strEmailSubject = "";
        string strEmailBody = "";
        string strEmailHTML = "";
        //--
        long lngMaxLogId = 0;
        //--
        bool blnTextGrid = false;
        //--
        bool blnEdit = false;
        //-----------------------------------------------------------------------
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

        #region SysEmailSetup_Load

        private void SysEmailSetup_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            pnlControls.Enabled = false; grpButton.Enabled = false;
            try
            {
                //-----------------------------------------------------------
                GC.Collect();
                //
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                //-----------------------------------------------------------
                //DisableControls();
                //-----------------------------------------------------------
                ViewGrid.Height = 518;
                //
                ControlVisible(false);
                ClearControls();
                //-----------------------------------------------------------
                lblTitle.Text = "Send Emails";
                //-----------------------------------------------------------
                //ViewGrid_Click(sender, e);
                BtnAdd_Click(sender,e);
                //Starting the timer
                tmrGridRefresh.Start();
                //--------------------------------------------                
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }

        #endregion

        #region cmbCompanyName_SelectedIndexChanged
        private void cmbCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompanyName.SelectedIndex <= 0)
            {
                cmbEmailSetup.Items.Clear();
                cmbEmailSetup.Enabled = false;
                cmbEmailFormat.Items.Clear();
                cmbEmailFormat.Enabled = false;
                return;
            }
            cmbEmailSetup.Enabled = true;
            cmbEmailFormat.Enabled = true;
            //-----------------------------------------------------------
            strSQL = "SELECT SYS_EMAIL_SETUP.EMAIL_SETUP_ID AS EMAIL_SETUP_ID," +
                "            SYS_EMAIL_SETUP.SETUP_DESC     AS SETUP_DESC " +
                "     FROM   SYS_EMAIL_SETUP," +
                "            MST_COMPANY " +
                "     WHERE  SYS_EMAIL_SETUP.COMPANY_ID    = MST_COMPANY.COMPANY_ID " +
                "     AND    SYS_EMAIL_SETUP.INACTIVE_FLAG = 0 " +
                "     AND    SYS_EMAIL_SETUP.COMPANY_ID    = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " " +
                "     ORDER BY SYS_EMAIL_SETUP.EMAIL_SETUP_ID DESC ";
            //
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbEmailSetup) == false) return;
            //-----------------------------------------------------------
            //strSQL = "SELECT SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID  AS EMAIL_FORMAT_ID," +
            //    "            SYS_EMAIL_FORMAT.EMAIL_FORMAT_DESC AS EMAIL_FORMAT_DESC " +
            //    "     FROM   SYS_EMAIL_FORMAT," +
            //    "            MST_EMAIL_CERTIFICATES," +
            //    "            MST_COMPANY " +
            //    "     WHERE  SYS_EMAIL_FORMAT.COMPANY_ID                 = MST_COMPANY.COMPANY_ID " +
            //    "     AND    SYS_EMAIL_FORMAT.EMAIL_CERTIFICATE_ID       = MST_EMAIL_CERTIFICATES.EMAIL_CERTIFICATE_ID " +
            //    "     AND    SYS_EMAIL_FORMAT.INACTIVE_FLAG               = 0 " +
            //    "     AND    MST_EMAIL_CERTIFICATES.EMAIL_CERTIFICATE_ID = " + Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) + " " +
            //    "     AND   (SYS_EMAIL_FORMAT.COMPANY_ID                 = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " " +
            //    "         OR SYS_EMAIL_FORMAT.COMPANY_ID                 = 0) " +
            //    "     ORDER BY SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID DESC ";
            ////
            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbEmailFormat) == false) return;
            if (cmbCompanyName.SelectedIndex > 0 && cmbCertificateID.SelectedIndex > 0)
            {
                LOAD_EMAIL_FORMAT(Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)), Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)));
            }
            else
                cmbEmailFormat.Items.Clear();
            //-----------------------------------------------------------
        }
        #endregion

        #region cmbCertificateID_SelectedIndexChanged
        //-- COMMENTED TO REMOVE SELECTION OF FORM NO.
        //private void cmbCertificateID_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) > 2)
        //    {
        //        lblQtr.Enabled = true;
        //        cmbQtr.Enabled = true;
        //        lblFormNo.Enabled = true;
        //        cmbFormNo.Enabled = true;
        //        //
        //        cmbQtr.Text = "";
        //        cmbFormNo.Text = "";
        //    }
        //    else if (Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) <= 2)
        //    {
        //        lblQtr.Enabled = false;
        //        cmbQtr.Enabled = false;
        //        lblFormNo.Enabled = false;
        //        cmbFormNo.Enabled = false;
        //        //
        //        cmbQtr.Text = T_Qtr.Q4;
        //        cmbFormNo.Text = T_FormNo.F24Q;
        //    }
        //    else
        //    {
        //        lblQtr.Enabled = true;
        //        cmbQtr.Enabled = true;
        //        lblFormNo.Enabled = true;
        //        cmbFormNo.Enabled = true;
        //        //
        //        cmbQtr.Text = "";
        //        cmbFormNo.Text = "";
        //    }
        //    //--
        //    if (cmbCompanyName.SelectedIndex > 0 && cmbCertificateID.SelectedIndex > 0)
        //    {
        //        LOAD_EMAIL_FORMAT(Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)), Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)));
        //    }
        //    else
        //        cmbEmailFormat.Items.Clear();
        //}
        #endregion

        #region cmbCertificateID_SelectedIndexChanged
        private void cmbCertificateID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) <= 2) //-- FORM 16 & PART B
            {
                //lblQtr.Enabled = true;
                cmbQtr.Enabled = false;
                //lblFormNo.Enabled = true;
                cmbFormNo.Enabled = false;
                //
                cmbQtr.Text = T_Qtr.Q4;
                //cmbFormNo.Text = T_FormNo.F24Q;
                cmbFormNo.Text = T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")";
            }
            else if (Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) == 3) //-- FORM 16A - 26Q
            {
                //lblQtr.Enabled = true;
                cmbQtr.Enabled = true;
                //lblFormNo.Enabled = true;
                cmbFormNo.Enabled = false;
                //
                cmbQtr.Text = "";
                //cmbFormNo.Text = T_FormNo.F26Q;
                cmbFormNo.Text = T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")";
            }
            else if (Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) == 4) //-- FORM 16A - 27Q
            {
                //lblQtr.Enabled = true;
                cmbQtr.Enabled = true;
                //lblFormNo.Enabled = true;
                cmbFormNo.Enabled = false;
                //
                cmbQtr.Text = "";
                //cmbFormNo.Text = T_FormNo.F27Q;
                cmbFormNo.Text = T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")";
            }
            else if (Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) == 5) //-- FORM 27D - 27EQ
            {
                //lblQtr.Enabled = true;
                cmbQtr.Enabled = true;
                //lblFormNo.Enabled = true;
                cmbFormNo.Enabled = false;
                //
                cmbQtr.Text = "";
                //cmbFormNo.Text = T_FormNo.F27EQ;
                cmbFormNo.Text = T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")";
            }
            else
            {
                lblQtr.Enabled = true;
                cmbQtr.Enabled = true;
                lblFormNo.Enabled = true;
                cmbFormNo.Enabled = true;
                //
                cmbQtr.Text = "";
                cmbFormNo.Text = "";
            }
            //--
            if (cmbCompanyName.SelectedIndex > 0 && cmbCertificateID.SelectedIndex > 0)
            {
                LOAD_EMAIL_FORMAT(Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)), Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)));
            }
            else
                cmbEmailFormat.Items.Clear();
        }
        #endregion

        #region BtnAdd_Click

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //if (cmbCertificateID.SelectedIndex <= 0)
                //{
                //    cmnService.J_UserMessage("Select the Company");
                //    cmbCertificateID.Select();
                //    return;
                //}
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;
                //---------------------------------------------
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                //---------------------------------------------
                strCheckFields = "";
                //---------------------------------------------
                //-----------
                //
                cmbCertificateID.Select();
                //---------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnEdit_Click

        private void BtnEdit_Click(object sender, System.EventArgs e)
        {
            try
            {
                //if (cmbCertificateID.SelectedIndex <= 0)
                //{
                //    cmnService.J_UserMessage("Select the Company");
                //    cmbCertificateID.Select();
                //    return;
                //}
                //--
                if (ViewGrid.CurrentRowIndex >= 0)
                {
                    //Added by Indrajit on 18-02-2013
                    if (Check_Record(lngSearchId) == 0) return;
                    //--------------------------------------------------
                    ControlVisible(true);
                    ClearControls();
                    //--------------------------------------------------
                    //A particular ID wise retriving the data from database
                    //if (ShowRecord(Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]))) == false)
                    //{
                    //    ControlVisible(false);
                    //    if (dsetGridClone == null) return;
                    //    dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
                    //}
                    //
                    //cmbCertificateID.Enabled = false;
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    //
                    //--------------------------------------------------
                    strCheckFields = "";
                    //--------------------------------------------------
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
                }
            }
            catch (Exception err_handler)
            {
                ControlVisible(false);
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnSave_Click

        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            //--
            blnEdit = false;
            if (ValidateFields() == false) return;
            //--
            if (grdvParty.Rows.Count <= 0)
            {
                cmnService.J_UserMessage("Party grid - Cannot be Blank");
                btnLoadParty.Select();
                return;
            }
            //--
            this.Cursor = Cursors.WaitCursor;
            //-- 
            #region ONLY FOR SQLSERVER DATABASE
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                if (dmlService.J_IsDatabaseObjectExist("TRN_EMAIL_HEADER_LOG", "EMAIL_SEND") == true)
                {
                    strSQL = "ALTER TABLE TRN_EMAIL_HEADER_LOG DROP COLUMN EMAIL_SEND";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "ALTER TABLE TRN_EMAIL_HEADER_LOG ADD EMAIL_SEND_DATETIME DATETIME";
                    dmlService.J_ExecSql(strSQL);
                }
            }
            #endregion
            //--
            strSQL = @"INSERT INTO TRN_EMAIL_HEADER_LOG (BASIC_INFO_ID, 
                                                        EMAIL_SETUP_ID, 
                                                        EMAIL_FORMAT_ID, 
                                                        EMAIL_CERTIFICATE_ID, 
                                                        EMAIL_SEND_DATETIME) 
                    VALUES                           (" + lngBasicInfoID + @",
                                                        " + Convert.ToInt32(Support.GetItemData(cmbEmailSetup, cmbEmailSetup.SelectedIndex)) + @",
                                                        " + Convert.ToInt32(Support.GetItemData(cmbEmailFormat, cmbEmailFormat.SelectedIndex)) + @",
                                                        " + Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) + @",
                                                        " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(DateTime.Now.ToString()) + cmnService.J_DateOperator() + ")";
            dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            lngMaxLogId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "TRN_EMAIL_HEADER_LOG", "EMAIL_HEADER_LOG_ID");
            //--
            bgwEmailing.RunWorkerAsync();
        }

        #endregion

        #region btnExportViewSentEmail_Click
        private void btnExportViewSentEmail_Click(object sender, EventArgs e)
        {
            //--
            blnEdit = true;
            if (ValidateFields() == false) return;
            blnEdit = false;
            //--
            string strFormName = cmbFormNo.Text.Split('(', ')')[1];
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)),
                                                            cmbQtr.Text,
                                                            Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)),
                                                            strFormName);
            //--
            strSQL = @"SELECT COUNT(*) FROM TRN_EMAIL_HEADER_LOG 
                       WHERE  BASIC_INFO_ID        = " + lngBasicInfoID + @"
                       AND    EMAIL_SETUP_ID       = " + Convert.ToInt32(Support.GetItemData(cmbEmailSetup, cmbEmailSetup.SelectedIndex)) + @" 
                       AND    EMAIL_FORMAT_ID      = " + Convert.ToInt32(Support.GetItemData(cmbEmailFormat, cmbEmailFormat.SelectedIndex)) + @"
                       AND    EMAIL_CERTIFICATE_ID = " + Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex));
            //
            if(cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
            {
                string ExcelFilePath = "";
                string ExcelFileName = "EmailLog_" + cmnService.J_Right(cmbCompanyName.Text, 11).Replace("]", "") + "_" + cmbCertificateID.Text.Replace(" ", "") + "_" + cmbFAYear.Text + ".csv";
                //--
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                // A new folder button will display in FolderBrowserDialog.
                folderBrowserDlg.ShowNewFolderButton = true;
                //Show FolderBrowserDialog
                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                if (dlgResult.Equals(DialogResult.OK))
                {
                    //Show selected folder path in textbox1.
                    ExcelFilePath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                else
                {
                    this.Cursor = Cursors.Default;  
                    return;  
                }
                //--
                string strNewExcelFileNamewithPath = string.Empty;
                strNewExcelFileNamewithPath = Path.Combine(ExcelFilePath, ExcelFileName);
                //--

                //-- EMAIL_CERTIFICATE_ID = 1,2 - MST_EMPLOYEE else MST_DEDUCTEE
                if (Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex))>= 3) //-- FORM 16Q, 27Q, 27EQ
                {
                    strSQL = @"SELECT MST_DEDUCTEE.DEDUCTEE_NAME       AS NAME,
                                      MST_DEDUCTEE.DEDUCTEE_PAN        AS PAN,
                                      TRN_EMAIL_DETAIL_LOG.TO_EMAIL_ID AS EMAIL," + 
                                      cmnService.J_SQLDBFormat("TRN_EMAIL_DETAIL_LOG.EMAIL_SEND_DATE_TIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSS) + @" AS DATE_TIME
                               FROM   TRN_EMAIL_HEADER_LOG,
                                      TRN_EMAIL_DETAIL_LOG,
                                      MST_DEDUCTEE
                               WHERE  TRN_EMAIL_DETAIL_LOG.PARTY_ID             = MST_DEDUCTEE.DEDUCTEE_ID 
                               AND    TRN_EMAIL_HEADER_LOG.EMAIL_HEADER_LOG_ID  = TRN_EMAIL_DETAIL_LOG.EMAIL_HEADER_LOG_ID 
                               AND    TRN_EMAIL_HEADER_LOG.BASIC_INFO_ID        = " + lngBasicInfoID + @"
                               AND    TRN_EMAIL_HEADER_LOG.EMAIL_SETUP_ID       = " + Convert.ToInt32(Support.GetItemData(cmbEmailSetup, cmbEmailSetup.SelectedIndex)) + @" 
                               AND    TRN_EMAIL_HEADER_LOG.EMAIL_FORMAT_ID      = " + Convert.ToInt32(Support.GetItemData(cmbEmailFormat, cmbEmailFormat.SelectedIndex)) + @"
                               AND    TRN_EMAIL_HEADER_LOG.EMAIL_CERTIFICATE_ID = " + Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) + @"
                               ORDER BY TRN_EMAIL_DETAIL_LOG.EMAIL_DETAIL_LOG_ID, 
                                      MST_DEDUCTEE.DEDUCTEE_NAME";
                }
                else //-- FORM 24Q
                {
                    strSQL = @"SELECT MST_EMPLOYEE.EMPLOYEE_NAME       AS NAME,
                                      MST_EMPLOYEE.EMPLOYEE_PAN        AS PAN,
                                      TRN_EMAIL_DETAIL_LOG.TO_EMAIL_ID AS EMAIL," +
                                      cmnService.J_SQLDBFormat("TRN_EMAIL_DETAIL_LOG.EMAIL_SEND_DATE_TIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSS) + @" AS DATE_TIME
                               FROM   TRN_EMAIL_HEADER_LOG,
                                      TRN_EMAIL_DETAIL_LOG,
                                      MST_EMPLOYEE
                               WHERE  TRN_EMAIL_DETAIL_LOG.PARTY_ID             = MST_EMPLOYEE.EMPLOYEE_ID 
                               AND    TRN_EMAIL_HEADER_LOG.EMAIL_HEADER_LOG_ID  = TRN_EMAIL_DETAIL_LOG.EMAIL_HEADER_LOG_ID 
                               AND    TRN_EMAIL_HEADER_LOG.BASIC_INFO_ID        = " + lngBasicInfoID + @"
                               AND    TRN_EMAIL_HEADER_LOG.EMAIL_SETUP_ID       = " + Convert.ToInt32(Support.GetItemData(cmbEmailSetup, cmbEmailSetup.SelectedIndex)) + @" 
                               AND    TRN_EMAIL_HEADER_LOG.EMAIL_FORMAT_ID      = " + Convert.ToInt32(Support.GetItemData(cmbEmailFormat, cmbEmailFormat.SelectedIndex)) + @"
                               AND    TRN_EMAIL_HEADER_LOG.EMAIL_CERTIFICATE_ID = " + Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) + @"
                               ORDER BY  TRN_EMAIL_DETAIL_LOG.EMAIL_DETAIL_LOG_ID, 
                                      MST_EMPLOYEE.EMPLOYEE_NAME";
                }
                //--
                ExportToCSV(strSQL, strNewExcelFileNamewithPath);
                //--
                System.Diagnostics.Process.Start(strNewExcelFileNamewithPath);
            }
            else
            {
                cmnService.J_UserMessage("Record not found");
                return;
            }
            //--
        }
        #endregion

        #region BtnCancel_Click

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED) == true)
                {
                    strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED;
                    dmlService.J_ExecSql(strSQL);
                }
                //strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED ;
                //dmlService.J_ExecSql(strSQL);
                //
                GC.Collect();
                //
                dmlService.Dispose();
                this.Close();
                this.Dispose();
                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnSort_Click
        private void BtnSort_Click(object sender, System.EventArgs e)
        {
            try
            {
                ////----------------------------------------------------------------------
                //lblSearchMode.Text = J_Mode.Sorting;
                ////----------------------------------------------------------------------
                //if (ValidateFields() == false) return;
                ////----------------------------------------------------------------------
                //grpSort.Visible = true;
                //grpSearch.Visible = false;
                ////----------------------------------------------------------------------
                //rbnSortSurveyDate.Checked = false;
                //rbnSortMemberName.Checked = false;
                //rbnSortAreaName.Checked = false;
                //rbnSortPoliceStaion.Checked = false;
                //rbnSortAsEntered.Checked = false;
                ////----------------------------------------------------------------------
                //if (strOrderBy == "TRN_SURVEY.SURVEY_DATE")
                //    rbnSortSurveyDate.Select();
                //else if (strOrderBy == "TRN_SURVEY.MEMBER_NAME")
                //    rbnSortMemberName.Select();
                //else if (strOrderBy == "MST_AREA.AREA_NAME")
                //    rbnSortAreaName.Select();
                //else if (strOrderBy == "TRN_SURVEY.POLICE_STATION")
                //    rbnSortPoliceStaion.Select();
                //else if (strOrderBy == "TRN_SURVEY.SURVEY_ID")
                //    rbnSortAsEntered.Select();
                ////----------------------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortOK_Click
        private void BtnSortOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-------------------------------------------
                //if (rbnSortSurveyDate.Checked == true)
                //    strOrderBy = "TRN_SURVEY.SURVEY_DATE";
                //else if (rbnSortMemberName.Checked == true)
                //    strOrderBy = "TRN_SURVEY.MEMBER_NAME";
                //else if (rbnSortAreaName.Checked == true)
                //    strOrderBy = "MST_AREA.AREA_NAME";
                //else if (rbnSortPoliceStaion.Checked == true)
                //    strOrderBy = "TRN_SURVEY.POLICE_STATION";
                //else if (rbnSortAsEntered.Checked == true)
                //    strOrderBy = "TRN_SURVEY.SURVEY_ID";
                ////-------------------------------------------
                //strCheckFields = "";
                //if (strCheckFields == "")
                //    strSQL = strQuery + "order by " + strOrderBy;
                //else
                //    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                ////-------------------------------------------
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                //if (dsetGridClone == null) return;
                ////-------------------------------------------
                //lblSearchMode.Text = J_Mode.General;
                //grpSort.Visible = false;
                ////-------------------------------------------
                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "SURVEY_ID", lngSearchId);
                ////-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortOK_KeyPress
        private void BtnSortOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        #region BtnSortCancel_Click
        private void BtnSortCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                ////-------------------------------------------
                //lblSearchMode.Text = J_Mode.General;
                //grpSort.Visible = false;
                ////-------------------------------------------
                //if (strCheckFields == "")
                //    strSQL = strQuery + "order by " + strOrderBy;
                //else
                //    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                ////-------------------------------------------
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                //if (dsetGridClone == null) return;
                ////-------------------------------------------
                //dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "SURVEY_ID", lngSearchId);
                ////-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortCancel_KeyPress
        private void BtnSortCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        #region BtnSearch_Click
        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbCertificateID.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select the Company");
                    cmbCertificateID.Select();
                    return;
                }
                //-------------------------------------------
                lblSearchMode.Text = J_Mode.Searching;
                //-------------------------------------------
                if (ValidateFields() == false) return;
                //-------------------------------------------
                grpSort.Visible = false;
                grpSearch.Visible = true;
                //-------------------------------------------
                txtEmailDescriptionSearch.Select();
                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_Click
        private void BtnSearchOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-------------------------------------------------------------------
                if (ValidateFields() == false) return;
                strCheckFields = "";
                //-------------------------------------------------------------------
                //--- Storing the Criteria Fiels & Values ---------------------------
                //-------------------------------------------------------------------
                //-- PAN NO 
                //-------------------------------------------------------------------
                if (txtEmailDescriptionSearch.Text.Trim() != "")
                    strCheckFields = "AND SYS_EMAIL_SETUP.SETUP_DESC like '%" + cmnService.J_ReplaceQuote(txtEmailDescriptionSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- DEDUCTEE NAME
                //-------------------------------------------------------------------
                if (txtFromEmailIDSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + " AND SYS_EMAIL_SETUP.FROM_EMAIL_ID like '%" + cmnService.J_ReplaceQuote(txtFromEmailIDSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                ////Added by Shrey Kejriwal on 17/08/2011
                ////-- COMPANY TAN
                ////-------------------------------------------------------------------
                //if (txtTANSearch.Text.Trim() != "")
                //    strCheckFields = strCheckFields + " AND MST_COMPANY.TAN_NO like '" + cmnService.J_ReplaceQuote(txtTANSearch.Text.Trim().ToUpper()) + "%' ";
                
                //----------------------------------------------------------------------
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                //----------------------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId) == false)
                {
                    txtEmailDescriptionSearch.Select();
                    return;
                }
                //----------------------------------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                //----------------------------------------------------------------------
                grpSearch.Visible = false;
                //----------------------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_KeyPress
        private void BtnSearchOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnSearchCancel_Click
        private void BtnSearchCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                //----------------------------------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                grpSearch.Visible = false;
                //----------------------------------------------------------------------
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                //----------------------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
                //----------------------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchCancel_KeyPress
        private void BtnSearchCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnDelete_Click
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (cmbCertificateID.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Select the Company");
                cmbCertificateID.Select();
                return;
            }
            //Modified by Indrajit on 02-03-2013
            if (ViewGrid.CurrentRowIndex >= 0)
            {
                lblMode.Text = J_Mode.Delete;
                Insert_Update_Delete_Data();
            }
            else
            {
                cmnService.J_UserMessage(J_Msg.DataNotFound);
                if (dsetGridClone == null) return;
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
            }
        }

        #endregion

        #region BtnRefresh_Click

        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbCertificateID.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select the Company");
                    cmbCertificateID.Select();
                    return;
                }
                //-----------------------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                //-----------------------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                //-----------------------------------------------------------
                //DisableControls();
                //-----------------------------------------------------------
                ClearControls();
                //-----------------------------------------------------------
                strCheckFields = "";
                strSQL = strQuery + "order by " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-----------------------------------------------------------
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
                //-----------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnExit_Click

        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }

        #endregion

        #region ViewGrid_Click

        private void ViewGrid_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
            {
                BtnAdd.Focus();
                return;
            }
            lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));

            ViewGrid.Select(ViewGrid.CurrentRowIndex);
            ViewGrid.Select();
            ViewGrid.Focus();
        }

        #endregion

        #region ViewGrid_DoubleClick
        private void ViewGrid_DoubleClick(object sender, System.EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }
        #endregion

        #region ViewGrid_KeyDown
        private void ViewGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (ViewGrid.CurrentRowIndex == -1) return;
                lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
                if (e.KeyCode == Keys.Enter) BtnEdit_Click(sender, e);
                if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);

                strTempMode = lblMode.Text;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region ViewGrid_CurrentCellChanged
        private void ViewGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
        }
        #endregion

        #region ViewGrid_MouseUp
        private void ViewGrid_MouseUp(object sender, MouseEventArgs e)
        {
            ViewGrid_Click(sender, e);
        }
        #endregion

        #region ViewGrid_MouseMove
        private void ViewGrid_MouseMove(object sender, MouseEventArgs e)
        {
           //cmnService.J_GridToolTip(ViewGrid, e.X, e.Y);
        }
        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region Searching_KeyPress
        private void Searching_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region Sorting_KeyPress
        private void Sorting_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSortOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        //Added by Indrajit on 12-02-2013
        #region tmrGridRefresh_Tick
        private void tmrGridRefresh_Tick(object sender, EventArgs e)
        {
            if (lblMode.Text == J_Mode.View)
            {
                //BtnRefresh_Click(sender, e);

                strCheckFields = "";
                strSQL = strQuery + "order by " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-----------------------------------------------------------
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);

            }
        }
        #endregion

        #region  btnChoosePath_Click
        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED) == true)
            {
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED;
                dmlService.J_ExecSql(strSQL);
            }
            //    
            string strExcelFolder = cmnService.J_OpenFolderDialog("Select Destination Folder");
            txtPath.Text = strExcelFolder;
        }
        #endregion   
        
        #region  btnLoadParty_Click
        private void btnLoadParty_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateFields() == false)
                    return;
                //--
                if (txtPath.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("PDF file path - can not be Blank");
                    btnChoosePath.Select();
                }
                //--
                string strFormName = cmbFormNo.Text.Split('(', ')')[1];
                lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)),
                                                            cmbQtr.Text,
                                                            Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)),
                                                            strFormName);
                //
                if (lngBasicInfoID == 0)
                {
                    cmnService.J_UserMessage("No transaction record found!!");
                    return;
                }
                //--
                CREATE_TEMP_TABLES();
                //--
                DirectoryInfo diPath = new DirectoryInfo(txtPath.Text);
                //
                int i = 0;
                //
                DMLService dmlService1 = new DMLService();
                if (dmlService1.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_PDF_PAN) == false)
                {
                    CREATE_TEMP_TABLES();
                }
                //--
                System.Threading.Thread.Sleep(10000);
                //--
                foreach (var file in diPath.GetFiles("*.pdf"))
                {
                    if (Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)) >= T_FinancialYearID.F2026_27ID)
                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PDF_PAN + " (PDF_PAN, PDF_PATH) VALUES ('" + cmnService.J_Mid(file.Name, 4, 10) + "',' " + file.FullName + "')";
                    else
                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PDF_PAN + " (PDF_PAN, PDF_PATH) VALUES ('" + cmnService.J_Left(file.Name, 10) + "',' " + file.FullName + "')";
                    //
                    dmlService1.J_BeginTransaction();
                    dmlService1.J_ExecSql(dmlService1.J_pCommand, strSQL);
                    dmlService1.J_Commit();
                    //
                    i++;
                }     
                //
                if (i == 0)
                {
                    cmnService.J_UserMessage("The folder does not have any pdf files !!");
                    return;
                }
                //
                if (Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) > 2) //-- FORM 26Q, 27Q, 27EQ
                {
                    string[,] strMatrixEmployee = {{"EMPLOYEE_ID", "0", "", "Right", "", "F", ""},
                                        {"PAN", "100", "", "", "", "", "T"},
                                        {"NAME", "200", "0", "", "", "", "T"},
                                        {"EMAIL ID", "100", "0", "", "", "", "T"},
                                        {"SEARCH", "0", "0", "", "", "F", ""},
                                        {"PATH", "0", "0", "", "", "F", ""}};
                    //
                    strSQL = "SELECT DISTINCT MST_DEDUCTEE.DEDUCTEE_ID," +
                                 "       MST_DEDUCTEE.DEDUCTEE_PAN  AS PAN," +
                                 "       MST_DEDUCTEE.DEDUCTEE_NAME AS NAME," +
                                 "       MST_DEDUCTEE.EMAIL         AS EMAIL," +
                                 "       MST_DEDUCTEE.DEDUCTEE_PAN + MST_DEDUCTEE.DEDUCTEE_NAME + MST_DEDUCTEE.EMAIL AS SEARCH_PARTY, " +
                                 "       MST_PDF_PAN.PDF_PATH  " + 
                                 "FROM   MST_DEDUCTEE," +
                                 "       TRN_DEDUCTEE_DETAILS," +
                                 "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PDF_PAN + " AS MST_PDF_PAN " +
                                 "WHERE  MST_DEDUCTEE.DEDUCTEE_ID           = TRN_DEDUCTEE_DETAILS.PARTY_ID " +
                                 "AND    MST_DEDUCTEE.DEDUCTEE_PAN          = MST_PDF_PAN.PDF_PAN " +
                                 "AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                                 "AND    MST_DEDUCTEE.EMAIL                 <> '' " +
                                 "ORDER BY  MST_DEDUCTEE.DEDUCTEE_NAME, " +
                                 "       MST_DEDUCTEE.DEDUCTEE_ID ";
                    TdsMan.PopulateGridView(grdvParty, dmlService.J_pCommand, strSQL, strMatrixEmployee);            

                }
                else if (Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) <= 2) //-- FORM 24Q
                {
                    string[,] strMatrixDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
                                        {"PAN", "100", "", "", "", "", "T"},
                                        {"NAME", "100", "", "", "", "", "T"},
                                        {"EMAIL ID", "200", "0", "", "", "", "T"},
                                        {"SEARCH", "0", "0", "", "", "F", ""},
                                        {"PATH", "0", "0", "", "", "F", ""}};
                    //
                    strSQL = "SELECT MST_EMPLOYEE.EMPLOYEE_ID," +
                           "             MST_EMPLOYEE.EMPLOYEE_PAN  AS PAN, " +
                           "             MST_EMPLOYEE.EMPLOYEE_NAME AS NAME," +
                           "             MST_EMPLOYEE.EMAIL         AS EMAILL," +
                           "             MST_EMPLOYEE.EMPLOYEE_PAN + MST_EMPLOYEE.EMPLOYEE_NAME + MST_EMPLOYEE.EMAIL AS SEARCH_PARTY, " +
                           "             MST_PDF_PAN.PDF_PATH " +
                           "     FROM    TRN_BASIC_INFO," +
                           "             MST_EMPLOYEE," +
                           "             TRN_SALARY_DETAILS," +
                           "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PDF_PAN + " AS MST_PDF_PAN " +
                           "     WHERE   TRN_SALARY_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID " +
                           "     AND     TRN_SALARY_DETAILS.EMPLOYEE_ID   = MST_EMPLOYEE.EMPLOYEE_ID " +
                           "     AND     MST_EMPLOYEE.EMPLOYEE_PAN        = MST_PDF_PAN.PDF_PAN " +
                           "     AND     TRN_SALARY_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                           "     AND     MST_EMPLOYEE.EMAIL              <> '' " +
                           "     ORDER BY MST_EMPLOYEE.EMPLOYEE_NAME," +
                           "             MST_EMPLOYEE.EMPLOYEE_ID";
                    TdsMan.PopulateGridView(grdvParty, dmlService.J_pCommand, strSQL, strMatrixDeductee);
            
                }

                        
            }
            catch
            {
            }
        }
        #endregion   

        #region cmbEmailSetup_SelectedIndexChanged
        private void cmbEmailSetup_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDataReader drdShowRecord = null;
            try
            {
                //--
                if (cmbEmailSetup.SelectedIndex <= 0)
                {
                    strFromEmailID = "";
                    strFromDisplayName = "";
                    strSMTPUserName = "";
                    strSMTPPassword = "";
                    strSMTPPort = "";
                    strSSL = "";
                    strServerAuthentication = "";
                    strSMTPHost = "";
                    strEmailReplyTo = "";
                    strEmailCC = "";
                    strEmailCC2 = "";
                    strEmailBCC = "";
                    strEmailBCC2 = "";
                }
                //--
                strSQL = "SELECT  SYS_EMAIL_SETUP.EMAIL_SETUP_ID AS EMAIL_SETUP_ID," +
                       "             SYS_EMAIL_SETUP.COMPANY_ID     AS COMPANY_ID," +
                       "             MST_COMPANY.COMPANY_NAME       AS COMPANY_NAME," +
                       "             SYS_EMAIL_SETUP.SETUP_DESC     AS SETUP_DESC," +
                       "             SYS_EMAIL_SETUP.FROM_EMAIL_ID  AS FROM_EMAIL_ID," +
                       "             SYS_EMAIL_SETUP.FROM_NAME      AS FROM_NAME," +
                       "             SYS_EMAIL_SETUP.SMTP_USERNAME  AS SMTP_USERNAME," +
                       "             SYS_EMAIL_SETUP.SMTP_PASSWORD  AS SMTP_PASSWORD," +
                       "             SYS_EMAIL_SETUP.SMTP_PORT      AS SMTP_PORT," +
                       "             SYS_EMAIL_SETUP.SMTP_HOST      AS SMTP_HOST," +
                       "             SYS_EMAIL_SETUP.SMTP_SSL       AS SMTP_SSL," +
                       "             SYS_EMAIL_SETUP.EMAIL_REPLY_TO AS EMAIL_REPLY_TO," +
                       "             SYS_EMAIL_SETUP.EMAIL_CC       AS EMAIL_CC," +
                       "             SYS_EMAIL_SETUP.EMAIL_CC2      AS EMAIL_CC2," +
                       "             SYS_EMAIL_SETUP.EMAIL_BCC      AS EMAIL_BCC," +
                       "             SYS_EMAIL_SETUP.EMAIL_BCC2     AS EMAIL_BCC2," +
                       "             SYS_EMAIL_SETUP.INACTIVE_FLAG AS INACTIVE_FLAG," +
                       "             SYS_EMAIL_SETUP.SERVER_AUTHENTICATION AS SERVER_AUTHENTICATION," +
                       "             SYS_EMAIL_SETUP.DISABLE_SSL_TLS       AS DISABLE_SSL_TLS " +
                       "     FROM    SYS_EMAIL_SETUP," +
                       "             MST_COMPANY " +
                       "     WHERE   SYS_EMAIL_SETUP.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                       "     AND     SYS_EMAIL_SETUP.EMAIL_SETUP_ID = " + Convert.ToInt32(Support.GetItemData(cmbEmailSetup, cmbEmailSetup.SelectedIndex)) + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    return ;
                }
                while (drdShowRecord.Read())
                {
                    //--
                    strFromEmailID = Convert.ToString(drdShowRecord["FROM_EMAIL_ID"]);
                    strFromDisplayName = Convert.ToString(drdShowRecord["FROM_NAME"]);
                    strSMTPUserName = Convert.ToString(drdShowRecord["SMTP_USERNAME"]);
                    strSMTPPassword = Convert.ToString(drdShowRecord["SMTP_PASSWORD"]);
                    strSMTPPort = Convert.ToString(drdShowRecord["SMTP_PORT"]);
                    strSMTPHost = Convert.ToString(drdShowRecord["SMTP_HOST"]);
                    //
                    if (Convert.ToString(drdShowRecord["SMTP_SSL"]) == "1")
                        strSSL = T_YES_NO.YES;
                    else
                        strSSL = T_YES_NO.NO;
                    //
                    strEmailReplyTo = Convert.ToString(drdShowRecord["EMAIL_REPLY_TO"]);
                    strEmailCC = Convert.ToString(drdShowRecord["EMAIL_CC"]);
                    strEmailCC2 = Convert.ToString(drdShowRecord["EMAIL_CC2"]);
                    strEmailBCC = Convert.ToString(drdShowRecord["EMAIL_BCC"]);
                    strEmailBCC2 = Convert.ToString(drdShowRecord["EMAIL_BCC2"]);
                    //
                    if (Convert.ToString(drdShowRecord["SERVER_AUTHENTICATION"]) == "1")
                        strServerAuthentication = T_TRUE_FALSE.TRUE.ToString();
                    else
                        strServerAuthentication = T_TRUE_FALSE.FALSE.ToString();
                    //
                    if (Convert.ToString(drdShowRecord["DISABLE_SSL_TLS"]) == "1")
                        strDisableSSLTLS = T_TRUE_FALSE.TRUE.ToString();
                    else
                        strDisableSSLTLS = T_TRUE_FALSE.FALSE.ToString();
                    //
                    //--
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //--
            }
            catch
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //--
            }
        }
        #endregion

        #region cmbEmailFormat_SelectedIndexChanged
        private void cmbEmailFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDataReader drdShowRecord = null;
            try
            {
                //--
                if (cmbEmailFormat.SelectedIndex <= 0)
                {
                    strEmailSubject = "";
                    strEmailBody = "";
                    strEmailHTML = "";
                }
                //--
                strSQL = "SELECT  SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID        AS EMAIL_FORMAT_ID," +
                    "             SYS_EMAIL_FORMAT.EMAIL_FORMAT_DESC      AS EMAIL_FORMAT_DESC," +
                    "             SYS_EMAIL_FORMAT.COMPANY_ID             AS COMPANY_ID," +
                    "             MST_COMPANY.COMPANY_NAME " + cmnService.J_ConcateSQLSyntaxOperator() + " '[' " + cmnService.J_ConcateSQLSyntaxOperator() + " MST_COMPANY.TAN_NO " + cmnService.J_ConcateSQLSyntaxOperator() + " ']' AS COMPANY," +
                    "             SYS_EMAIL_FORMAT.EMAIL_CERTIFICATE_ID   AS EMAIL_CERTIFICATE_ID," +
                    "             MST_EMAIL_CERTIFICATES.CERTIFICATE_DESC AS CERTIFICATE_DESC," +
                    "             SYS_EMAIL_FORMAT.EMAIL_SUBJECT          AS EMAIL_SUBJECT," +
                    "             SYS_EMAIL_FORMAT.EMAIL_BODY             AS EMAIL_BODY," +
                    "             SYS_EMAIL_FORMAT.HTML_FORMAT            AS HTML_FORMAT," +
                    "             SYS_EMAIL_FORMAT.INACTIVE_FLAG          AS INACTIVE_FLAG " +
                    "       FROM ((SYS_EMAIL_FORMAT LEFT JOIN MST_COMPANY " +
                    "       ON     SYS_EMAIL_FORMAT.COMPANY_ID = MST_COMPANY.COMPANY_ID) " +
                    "              INNER JOIN MST_EMAIL_CERTIFICATES " +
                    "       ON     SYS_EMAIL_FORMAT.EMAIL_CERTIFICATE_ID = MST_EMAIL_CERTIFICATES.EMAIL_CERTIFICATE_ID) " +
                    "     WHERE  SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID = " + Convert.ToInt32(Support.GetItemData(cmbEmailFormat, cmbEmailFormat.SelectedIndex)) + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    return;
                }
                while (drdShowRecord.Read())
                {
                    //--
                    strEmailSubject = Convert.ToString(drdShowRecord["EMAIL_SUBJECT"]);
                    strEmailBody = Convert.ToString(drdShowRecord["EMAIL_BODY"]);
                    //
                    if (Convert.ToString(drdShowRecord["HTML_FORMAT"]) == "1")
                        strEmailHTML = "True";
                    else
                        strEmailHTML = "False";
                    //--
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //--
            }
            catch
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //--
            }
        }
        #endregion        
        
        #region grdvParty_CurrentCellDirtyStateChanged
        private void grdvParty_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grdvParty.IsCurrentCellDirty)
            {
                grdvParty.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        #endregion

        #region  grdvParty_CellClick
        private void grdvParty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region COMMENT
                //if (e.RowIndex != -1)
                //{
                //    foreach (DataGridViewRow row in grdvParty.Rows)
                //    {
                //        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                //            row.Cells[0].Value = false;
                //    }
                //    //--
                //    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grdvParty.Rows[e.RowIndex].Cells[0];
                //    if (cell.Value == null || (bool)cell.Value == false)
                //    {
                //        //grdvParty.MultiSelect = false;
                //        grdvParty.Rows[e.RowIndex].Selected = true;
                //        cell.Value = true;
                //    }
                //    else
                //    {
                //        grdvParty.Rows[e.RowIndex].Selected = false;
                //        cell.Value = false;
                //    }
                //    //--
                //    #region COMMENT
                //    //DataGridViewCell cellQtr = (DataGridViewCell)grdvParty.Rows[e.RowIndex].Cells[4];
                //    //DataGridViewCell cellYr = (DataGridViewCell)grdvParty.Rows[e.RowIndex].Cells[2];
                //    //DataGridViewCell cellFormNo = (DataGridViewCell)grdvParty.Rows[e.RowIndex].Cells[3];
                //    ////
                //    //if (Convert.ToString(cellFormNo.Value) == T_FormNo.F24Q)
                //    //    rbnSort3.Text = T_TypeFilter.Employee;
                //    //else
                //    //    rbnSort3.Text = T_TypeFilter.Deductee;
                //    ////--
                //    //if (Convert.ToString(cellQtr.Value) == T_Qtr.Q4 && Convert.ToString(cellFormNo.Value) == T_FormNo.F24Q)
                //    //{
                //    //    rbnSort4.Visible = true;
                //    //    rbnSort4.Text = T_TypeFilter.SalaryDetails;
                //    //    grpSort.Height = 118;
                //    //}
                //    //else
                //    //{
                //    //    rbnSort1.Checked = true;
                //    //    rbnSort4.Visible = false;
                //    //    grpSort.Height = 99;
                //    //}
                //    ////--
                //    //Quarter = Convert.ToString(cellQtr.Value);
                //    //FinancialYear = Convert.ToString(cellYr.Value);
                //    //FormNo = Convert.ToString(cellFormNo.Value);
                //    #endregion
                //}
                //else
                //{
                //    if (e.RowIndex < 0)
                //    {
                //        //
                //        dmlService.J_ExecSql("DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED);
                //        //            
                //    }
                //}
                #endregion  
                //
                if (e.RowIndex < 0)
                {
                    //--
                    if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED) == true)
                        dmlService.J_ExecSql("DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED);
                    //--
                    chkSelectAllParty.Checked = false;
                    lblEmailCounter.Visible = false;
                    //            
                }
                blnTextGrid = true;
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);

            }
        }
        #endregion

        #region grdvParty_CellValueChanged
        private void grdvParty_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (blnTextGrid == false) return;
                foreach (DataGridViewRow row in grdvParty.Rows)
                {
                    if ((bool)row.Cells[0].Selected == true)
                    {
                        //if (blnTextGrid == false)
                        //{
                            if (dmlService.J_ReturnNoOfRows("SELECT * FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + " WHERE PARTY_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)), J_QueryType.DirectQuery) == 0)
                            {
                                //-- ANIK @ 2017/10/26
                                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + "(PARTY_ID, PARTY_PAN, PARTY_NAME) VALUES(" +
                                                    cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + ",'" +
                                                    cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[2].Value)) + "','" + cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[3].Value)) + "')";


                                dmlService.J_ExecSql(strSQL);
                                //
                            }
                            else
                            {
                                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + " WHERE PARTY_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + "";
                                dmlService.J_ExecSql(strSQL);
                                //
                            }
                        //}
                        //else
                        //{
                        //    if (dmlService.J_ReturnNoOfRows("SELECT * FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + " WHERE PARTY_NAME ='" + Convert.ToString(row.Cells[1].Value) + " - " + Convert.ToString(row.Cells[2].Value) + "'", J_QueryType.DirectQuery) == 0)
                        //    {
                        //        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + "(PARTY_ID, PARTY_NAME, PARTY_PAN) VALUES(" +
                        //                            cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + ",'" +
                        //                            cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[2].Value)) + "','" + cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[3].Value)) + "')";
                        //        dmlService.J_ExecSql(strSQL);
                        //    }
                        //    else
                        //    {
                        //        strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + " WHERE PARTY_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + "";
                        //        dmlService.J_ExecSql(strSQL);
                        //        //--
                        //    }
                        //}
                        //--
                    }
                }
                //--
                long intCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED, J_QueryType.DirectQuery);
                if (intCount > 0)
                {
                    lblEmailCounter.Visible = true;
                    lblEmailCounter.Text = intCount + " record(s) selected";
                }
                else
                    lblEmailCounter.Visible = false;
                //--
        }
        #endregion

        #region chkSelectAllParty_CheckedChanged
        private void chkSelectAllParty_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                //if (grdvCertificateDescription.Visible == false) { chkSelectDeselect.Checked = false; return; }
                //if (blnSelectDeselect == false) return;
                //--
                this.Cursor = Cursors.WaitCursor;
                //blnDeleteTempGridRecord = true;
                foreach (DataGridViewRow row in grdvParty.Rows)
                {
                    if (chkSelectAllParty.Checked == true)//checked all checkbox
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                        {
                            if (row.Cells[4].Value.ToString().Trim() != "")
                                row.Cells[0].Value = true;
                        }
                    }
                    else//Unchecked all checkbox
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                        {
                            //blnExitGrid = false;
                            row.Cells[0].Value = false;
                            //blnExitGrid = false;
                        }
                        //strSelectedLabel = "";
                    }
                }
                //
                #region COMMENT
                //blnDeleteTempGridRecord = false;

                ////ADDED BY DHRUB ON 07/02/2014
                //if ((blnDeleteTempGridRecord == false) && (grdvCertificateDescription.Rows.Count > 0))
                //    SelectedGridItemsWithSearchData(cmnService.J_GenerateDataGridViewSelectedId(grdvCertificateDescription));
                ////
                //if (chkCertificateSelectDeselect.Checked == true)
                //{
                //    //dmlService.J_ExecSql("DELETE FROM TEMP_REPORT_GRID_ITEMS_SELECTED");
                //    //--
                //    if (grdvCertificateDescription.RowCount > 0)
                //    {
                //        foreach (DataGridViewRow row in grdvCertificateDescription.Rows)
                //        {
                //            if (row.Cells[1].Value != null)
                //            {

                //                if (blnTextGrid == false)
                //                {
                //                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED, "PARTY_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value))) == false)
                //                    {
                //                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + "(PARTY_ID,PARTY_NAME) VALUES(" +
                //                                                        cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + ",'" +
                //                                                        cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[2].Value)) + "')";
                //                        dmlService.J_ExecSql(strSQL);
                //                    }
                //                }
                //                else
                //                {
                //                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED, "PARTY_NAME ='" + Convert.ToString(row.Cells[1].Value) + " - " + Convert.ToString(row.Cells[2].Value) + "'") == false)
                //                    {
                //                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + "(PARTY_NAME) VALUES('" +
                //                                                        Convert.ToString(row.Cells[1].Value) + " - " + Convert.ToString(row.Cells[2].Value) + "')";
                //                        dmlService.J_ExecSql(strSQL);
                //                    }
                //                }
                //                //
                //                //intCount = intCount + 1;
                //            }
                //        }
                //        //--
                //        intCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED, J_QueryType.DirectQuery);
                //        if (intCount > 0)
                //        {
                //            lblCertificateShowSelected.Visible = true;
                //            lblCertificateShowSelected.Text = intCount + " record(s) selected";
                //        }
                //        else
                //            lblCertificateShowSelected.Visible = false;
                //        //--
                //    }

                //}
                //else
                //{
                //    if (txtCertificateSearch.Text.Trim() == "")
                //    {
                //        dmlService.J_ExecSql("DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED);
                //        lblCertificateShowSelected.Visible = false;
                //        intCount = 0;
                //    }
                //    else
                //    {
                //        foreach (DataGridViewRow row in grdvCertificateDescription.Rows)
                //        {
                //            if (row.Cells[1].Value != null)
                //            {
                //                //if (dmlService.J_IsRecordExist("TEMP_REPORT_GRID_ITEMS_SELECTED", "PARTY_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value))) == false)
                //                //{
                //                //strSQL = "INSERT INTO TEMP_REPORT_GRID_ITEMS_SELECTED(PARTY_ID,PARTY_NAME) VALUES(" +
                //                //                                cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + ",'" +
                //                //                                cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[2].Value)) + "')";
                //                //dmlService.J_ExecSql(strSQL);
                //                //}
                //                //
                //                //intCount = intCount + 1;
                //                dmlService.J_ExecSql("DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + " WHERE PARTY_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)));
                //            }
                //        }
                //        //--
                //        intCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED, J_QueryType.DirectQuery);
                //        if (intCount > 0)
                //        {
                //            lblCertificateShowSelected.Visible = true;
                //            lblCertificateShowSelected.Text = intCount + " record(s) selected";
                //        }
                //        else
                //            lblCertificateShowSelected.Visible = false;
                //        //--

                //    }
                //}
                //
                #endregion
                //
                if (chkSelectAllParty.Checked == true)
                {
                    //dmlService.J_ExecSql("DELETE FROM TEMP_REPORT_GRID_ITEMS_SELECTED");
                    //--
                    if (grdvParty.RowCount > 0)
                    {
                        foreach (DataGridViewRow row in grdvParty.Rows)
                        {
                            if (row.Cells[1].Value != null)
                            {

                                //if (blnTextGrid == false)
                                //{
                                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED, "PARTY_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value))) == false)
                                    {
                                        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + "(PARTY_ID, PARTY_PAN, PARTY_NAME) VALUES(" +
                                                                        cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + ",'" +
                                                                        cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[2].Value)) + "','" +
                                                                        cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[3].Value)) + "')";
                                        dmlService.J_ExecSql(strSQL);
                                    }
                                //}
                                //else
                                //{
                                //    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED, "PARTY_NAME ='" + Convert.ToString(row.Cells[1].Value) + " - " + Convert.ToString(row.Cells[2].Value) + "'") == false)
                                //    {
                                //        strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + "(PARTY_NAME) VALUES('" +
                                //                                        Convert.ToString(row.Cells[1].Value) + " - " + Convert.ToString(row.Cells[2].Value) + "')";
                                //        dmlService.J_ExecSql(strSQL);
                                //    }
                                //}
                                //
                                //intCount = intCount + 1;
                            }
                        }
                        //--
                        long intCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED, J_QueryType.DirectQuery);
                        if (intCount > 0)
                        {
                            lblEmailCounter.Visible = true;
                            lblEmailCounter.Text = intCount + " record(s) selected";
                        }
                        else
                            lblEmailCounter.Visible = false;
                        //--
                    }

                }
                else
                {
                    //if (txtSearch.Text.Trim() == "")
                    //{
                    //    dmlService.J_ExecSql("DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED);
                    //    lblShowSelected.Visible = false;
                    //    intCount = 0;
                    //}
                    //else
                    //{
                        foreach (DataGridViewRow row in grdvParty.Rows)
                        {
                            if (row.Cells[1].Value != null)
                            {
                                //if (dmlService.J_IsRecordExist("TEMP_REPORT_GRID_ITEMS_SELECTED", "PARTY_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value))) == false)
                                //{
                                //strSQL = "INSERT INTO TEMP_REPORT_GRID_ITEMS_SELECTED(PARTY_ID,PARTY_NAME) VALUES(" +
                                //                                cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)) + ",'" +
                                //                                cmnService.J_ReplaceQuote(Convert.ToString(row.Cells[2].Value)) + "')";
                                //dmlService.J_ExecSql(strSQL);
                                //}
                                //
                                //intCount = intCount + 1;
                                dmlService.J_ExecSql("DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + " WHERE PARTY_ID = " + cmnService.J_ReturnInt32Value(Convert.ToString(row.Cells[1].Value)));
                            }
                        }
                    //--
                    long intCount = 0;
                    if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED) == true)
                        intCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED, J_QueryType.DirectQuery);

                        if (intCount > 0)
                        {
                            lblEmailCounter.Visible = true;
                            lblEmailCounter.Text = intCount + " record(s) selected";
                        }
                        else
                            lblEmailCounter.Visible = false;
                        //--

                    //}
                }
                this.Cursor = Cursors.Default;
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region txtSearchParty_TextChanged
        private void txtSearchParty_TextChanged(object sender, EventArgs e)
        {
            //if (rptService.J_pGridMultipleColumnStyle == false)
            //{
            //    if (rptService.J_pGridDataSet == null) return;
            //    DataView dvwFilter = rptService.J_pGridDataSet.Tables[0].DefaultView;
            //    dvwFilter.RowFilter = rptService.J_pGridDataSet.Tables[0].Columns[intCertificateSearchColumn].ColumnName + " like '%" + cmnService.J_ReplaceQuote(txtCertificateSearch.Text) + "%'";
            //    grdvCertificateDescription.DataSource = dvwFilter;
            //}
        }
        #endregion         

        #region lnkViewSetup_LinkClicked
        private void lnkViewSetup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cmbEmailSetup.Enabled==false || Convert.ToInt32(Support.GetItemData(cmbEmailSetup, cmbEmailSetup.SelectedIndex)) <= 0)
            {
                cmnService.J_UserMessage("Please select the Setup");
                return;
            }
            //--
            cmnService.J_UserMessage("From Email ID     : " + strFromEmailID + "\n" +
                                     "From Display Name : " + strFromDisplayName + "\n" +
                                     "SMTP User Name    : " + strSMTPUserName + "\n" +
                                     "SMTP Password     : " + strSMTPPassword + "\n" +
                                     "SMTP Port         : " + strSMTPPort + "\n" +
                                     "SMTP Host         : " + strSMTPHost + "\n" +
                                     "SSL               : " + strSSL + "\n" +
                                     "Email Reply To    : " + strEmailReplyTo + "\n" +
                                     "Email CC          : " + strEmailCC + "\n" +
                                     "Email CC2         : " + strEmailCC2 + "\n" +
                                     "Email BCC         : " + strEmailBCC + "\n" +
                                     "Email BCC2        : " + strEmailBCC2);
            //--
        }
        #endregion

        #region lnkViewFormat_LinkClicked
        private void lnkViewFormat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cmbEmailFormat.Enabled == false || Convert.ToInt32(Support.GetItemData(cmbEmailFormat, cmbEmailFormat.SelectedIndex)) <= 0)
            {
                cmnService.J_UserMessage("Please select the Format");
                return;
            }
            //--
            cmnService.J_UserMessage("Email Subject     : " + strEmailSubject + "\n" +
                                     "Email Body        : " + strEmailBody + "\n" +
                                     "HTML Mail         : " + strEmailHTML);
            //--
        }
        #endregion

        #region bgwEmailing_DoWork
        //private void bgwEmailing_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Label.CheckForIllegalCrossThreadCalls = false;
        //    //--
        //    foreach (DataGridViewRow row in grdvParty.Rows)
        //    {
        //        if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
        //        {
        //            if (row.Cells[4].Value != "")
        //            {
        //                //if(strSMTPHost.ToLower().Contains("office365"))
        //                if (strSMTPPort == "465")
        //                    SendEmailImplicitSSL(strFromEmailID,
        //                                strFromDisplayName,
        //                                   strEmailReplyTo,
        //                                    strEmailCC,
        //                                        strEmailCC2,
        //                                            strEmailBCC,
        //                                                strEmailBCC2,
        //                                                    strEmailSubject,
        //                                                        strEmailBody,
        //                                                            Convert.ToBoolean(strEmailHTML),
        //                                                                row.Cells[4].Value.ToString(),
        //                                                                    row.Cells[3].Value.ToString(),
        //                                                                        row.Cells[2].Value.ToString(),
        //                                                                            cmbCompanyName.Text,
        //                                                                                row.Cells[6].Value.ToString());
        //                else
        //                {

        //                    SendEmailTLS(strFromEmailID,
        //                                strFromDisplayName,
        //                                   strEmailReplyTo,
        //                                    strEmailCC,
        //                                        strEmailCC2,
        //                                            strEmailBCC,
        //                                                strEmailBCC2,
        //                                                    strEmailSubject,
        //                                                        strEmailBody,
        //                                                            Convert.ToBoolean(strEmailHTML),
        //                                                                row.Cells[4].Value.ToString(),
        //                                                                    row.Cells[3].Value.ToString(),
        //                                                                        row.Cells[2].Value.ToString(),
        //                                                                            cmbCompanyName.Text,
        //                                                                                row.Cells[6].Value.ToString());
        //                }
        //                //else
        //                //    SendEmail(strFromEmailID,
        //                //                strFromDisplayName,
        //                //                   strEmailReplyTo,
        //                //                    strEmailCC,
        //                //                        strEmailCC2,
        //                //                            strEmailBCC,
        //                //                                strEmailBCC2,
        //                //                                    strEmailSubject,
        //                //                                        strEmailBody,
        //                //                                            Convert.ToBoolean(strEmailHTML),
        //                //                                                row.Cells[4].Value.ToString(),
        //                //                                                    row.Cells[3].Value.ToString(),
        //                //                                                        row.Cells[2].Value.ToString(),
        //                //                                                            cmbCompanyName.Text,
        //                //                                                                row.Cells[6].Value.ToString());

        //                //--                        
        //            }
        //            //--
        //        }
        //    }
        //}
        private void bgwEmailing_DoWork(object sender, DoWorkEventArgs e)
        {
            Label.CheckForIllegalCrossThreadCalls = false;

            // Collect all recipients first
            var recipients = new List<(string EmailIdTo, string PartyName, string PartyPAN, string CompanyName, string PdfPath)>();

            foreach (DataGridViewRow row in grdvParty.Rows)
            {
                if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                {
                    if (row.Cells[4].Value != null && row.Cells[4].Value.ToString() != "")
                    {
                        recipients.Add((
                            row.Cells[4].Value.ToString(),   // EmailIdTo
                            row.Cells[3].Value.ToString(),   // PartyName
                            row.Cells[2].Value.ToString(),   // PartyPAN
                            cmbCompanyName.Text,             // CompanyName
                            row.Cells[6].Value.ToString()    // PdfPath
                        ));
                    }
                }
            }

            // If nothing to send, exit
            if (recipients.Count == 0)
                return;

            // Choose sending method
            if (strSMTPPort == "465")
            {
                // if you also want a bulk version of ImplicitSSL, we can adapt it
                foreach (var rec in recipients)
                {
                    SendEmailImplicitSSL(strFromEmailID,
                                         strFromDisplayName,
                                         strEmailReplyTo,
                                         strEmailCC,
                                         strEmailCC2,
                                         strEmailBCC,
                                         strEmailBCC2,
                                         strEmailSubject,
                                         strEmailBody,
                                         Convert.ToBoolean(strEmailHTML),
                                         rec.EmailIdTo,
                                         rec.PartyName,
                                         rec.PartyPAN,
                                         rec.CompanyName,
                                         rec.PdfPath);
                }
            }
            else
            {
                ////// Send all in one go using TLS bulk sender
                ////SendBulkEmailsTLS(recipients,
                ////                  strFromEmailID,
                ////                  strFromDisplayName,
                ////                  strEmailReplyTo,
                ////                  strEmailCC,
                ////                  strEmailCC2,
                ////                  strEmailBCC,
                ////                  strEmailBCC2,
                ////                  strEmailSubject,
                ////                  strEmailBody,
                ////                  Convert.ToBoolean(strEmailHTML));
                //-- to resolve "issue, after 200 mail error showing."
                SendBulkEmailsOptimized(recipients,
                                  strFromEmailID,
                                  strFromDisplayName,
                                  strEmailReplyTo,
                                  strEmailCC,
                                  strEmailCC2,
                                  strEmailBCC,
                                  strEmailBCC2,
                                  strEmailSubject,
                                  strEmailBody,
                                  Convert.ToBoolean(strEmailHTML));
            }
        }

        #endregion

        #region bgwEmailing_RunWorkerCompleted
        private void bgwEmailing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                cmnService.J_UserMessage("Email Request Cancelled");
                //
                return;
            }
            //--
            foreach (DataGridViewRow row in grdvParty.Rows)
            {
                if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                {
                    if (row.Cells[4].Value.ToString() != "")
                    {
                        //-- LOG DETAIL
                        strSQL = @"INSERT INTO TRN_EMAIL_DETAIL_LOG (EMAIL_HEADER_LOG_ID, 
                                                         PARTY_ID, 
                                                         TO_EMAIL_ID,
                                                         EMAIL_SEND_DATE_TIME) 
                                   VALUES               (" + lngMaxLogId + @",
                                                         " + Convert.ToInt32(row.Cells[1].Value.ToString()) + @",
                                                        '" + row.Cells[4].Value.ToString() + @"',
                                                         " + cmnService.J_DateOperator() + J_ReturnServerDateTimeMMDDYYYYHHMMSS() + cmnService.J_DateOperator() + ")";
                        dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                    }
                }
            }
            //
            this.Cursor = Cursors.Default;
            cmnService.J_UserMessage("Process completed");
            //--
        }
        #endregion

        #region bgwEmailing_ProgressChanged
        private void bgwEmailing_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //btnVerification.Text = btnVerification.Text + "(" + e.ProgressPercentage.ToString() + "%)";
            lblEmailCounter.Text = lblEmailCounter.Text + "(" + e.ProgressPercentage.ToString() + "%)";
        }
        #endregion

        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            FormTrn.TrnTANSearch Tan = new TrnTANSearch("SysSendEmail");
            Tan.ShowDialog();
            //--------------
            if (TDSMAN.Classes.TDSMAN.T_pTAN != "")
                cmbCompanyName.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #endregion 

        #region User Define Functions

        #region NumericControl_KeyPress
        private void NumericControl_KeyPress(object sender, KeyPressEventArgs e, int MaxLength)
        {
            TextBox txtNumeric = (TextBox)sender;
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N," + MaxLength + ",0", txtNumeric, "") == false)
                e.Handled = true;
        }
        #endregion

        #region NumericCurrencyControl_Leave
        #endregion

        #region NumericControl_Leave
        #endregion        

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            //--
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME " + cmnService.J_ConcateSQLSyntaxOperator() + " ' [' " + cmnService.J_ConcateSQLSyntaxOperator() + " TAN_NO " + cmnService.J_ConcateSQLSyntaxOperator() + " ']' " +
                "      FROM   MST_COMPANY " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY COMPANY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompanyName) == false) return;
            //--
            strSQL = " SELECT EMAIL_CERTIFICATE_ID," +
                "             CERTIFICATE_DESC " +
                "      FROM   MST_EMAIL_CERTIFICATES " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY EMAIL_CERTIFICATE_ID ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCertificateID) == false) return;
            //-----------
            //-- FINANCIAL YEAR
            //-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";

            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFAYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //-- QUARTER
            //-----------
            string[] strQtr = { T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQtr, 1);
            //-----------
            //-- FORM NO
            //-----------
            //string[] strFormNo = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ};
            string[] strFormNo = { T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ")", T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ")", T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ")", T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ")" };
            dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo, 1);
            //-----------
            txtPath.Text = "";
            //----------
            strFromEmailID = "";
            strFromDisplayName = "";
            strSMTPUserName = "";
            strSMTPPassword = "";
            strSMTPPort = "";
            strSSL = "";
            strServerAuthentication = "";
            strSMTPHost = "";
            strEmailReplyTo = "";
            strEmailCC = "";
            strEmailCC2 = "";
            strEmailBCC = "";
            strEmailBCC2 = "";
            //
            strEmailSubject = "";
            strEmailBody = "";
            strEmailHTML = "";
            //
            //
            if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED) == true)
            {
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED;
                dmlService.J_ExecSql(strSQL);
            }             
            //--------------------                  
        }
        #endregion

        #region ShowRecord
        //private bool ShowRecord(long Id)
        //{
            //IDataReader drdShowRecord = null;
            //string strCompanyName = string.Empty;
            //string strTanNo = string.Empty;
            ////-----------------------------------------------------------
            ///* (1) Column Value
            // * (2) Column Data Type
            // * (3) Replace String
            // * (4) Replace String Data Type */
            ////-----------------------------------------------------------
            ////-----------------------------------------------------------
            //try
            //{
            //    //string[,] strShowHelpEmployeeMatrix = {{"MST_EMPLOYEE.CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
            //    //                                       {"MST_EMPLOYEE.CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
            //    //                                       {"MST_EMPLOYEE.CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
            //    //                                       {"MST_EMPLOYEE.CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
            //    //                                       {"MST_EMPLOYEE.CATEGORY = ''", "F", "", "T"}};
            //    //
            //    strSQL = "SELECT  SYS_EMAIL_SETUP.EMAIL_SETUP_ID AS EMAIL_SETUP_ID," +
            //        "             SYS_EMAIL_SETUP.COMPANY_ID     AS COMPANY_ID," +
            //        "             MST_COMPANY.COMPANY_NAME       AS COMPANY_NAME," +
            //        "             SYS_EMAIL_SETUP.SETUP_DESC     AS SETUP_DESC," +
            //        "             SYS_EMAIL_SETUP.FROM_EMAIL_ID  AS FROM_EMAIL_ID," +
            //        "             SYS_EMAIL_SETUP.FROM_NAME      AS FROM_NAME," +
            //        "             SYS_EMAIL_SETUP.SMTP_USERNAME  AS SMTP_USERNAME," +
            //        "             SYS_EMAIL_SETUP.SMTP_PASSWORD  AS SMTP_PASSWORD," +
            //        "             SYS_EMAIL_SETUP.SMTP_PORT      AS SMTP_PORT," +
            //        "             SYS_EMAIL_SETUP.SMTP_HOST      AS SMTP_HOST," +
            //        "             SYS_EMAIL_SETUP.SMTP_SSL       AS SMTP_SSL," +
            //        "             SYS_EMAIL_SETUP.EMAIL_REPLY_TO AS EMAIL_REPLY_TO," +
            //        "             SYS_EMAIL_SETUP.EMAIL_CC       AS EMAIL_CC," +
            //        "             SYS_EMAIL_SETUP.EMAIL_CC2      AS EMAIL_CC2," +
            //        "             SYS_EMAIL_SETUP.EMAIL_BCC      AS EMAIL_BCC," +
            //        "             SYS_EMAIL_SETUP.EMAIL_BCC2     AS EMAIL_BCC2," +
            //        "             SYS_EMAIL_SETUP.INACTIVE_FLAG AS INACTIVE_FLAG " +
            //        "     FROM    SYS_EMAIL_SETUP," +
            //        "             MST_COMPANY " +
            //        "     WHERE   SYS_EMAIL_SETUP.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
            //        "     AND     SYS_EMAIL_SETUP.EMAIL_SETUP_ID = " + Id + " ";

            //    drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
            //    if (drdShowRecord == null)
            //    {
            //        return false;
            //    }
            //    while (drdShowRecord.Read())
            //    {
            //        lngSearchId = Id;
            //        //--
            //        txtEmailDescription.Text = Convert.ToString(drdShowRecord["SETUP_DESC"]);
            //        txtFromEmailId.Text = Convert.ToString(drdShowRecord["FROM_EMAIL_ID"]);
            //        txtFromDisplayName.Text = Convert.ToString(drdShowRecord["FROM_NAME"]);
            //        txtSMTPUserName.Text = Convert.ToString(drdShowRecord["SMTP_USERNAME"]);
            //        txtSMTPPassword.Text = Convert.ToString(drdShowRecord["SMTP_PASSWORD"]);
            //        txtSMTPPort.Text = Convert.ToString(drdShowRecord["SMTP_PORT"]);
            //        txtSMTPHost.Text = Convert.ToString(drdShowRecord["SMTP_HOST"]);
            //        //
            //        if (Convert.ToString(drdShowRecord["SMTP_SSL"]) == "1")
            //            cmbSSL.Text = T_YES_NO.YES;
            //        else
            //            cmbSSL.Text = T_YES_NO.NO;
            //        //
            //        txtEmailReplyTo.Text = Convert.ToString(drdShowRecord["EMAIL_REPLY_TO"]);
            //        txtEmailCC.Text = Convert.ToString(drdShowRecord["EMAIL_CC"]);
            //        txtEmailCC2.Text = Convert.ToString(drdShowRecord["EMAIL_CC2"]);
            //        txtEmailBCC.Text = Convert.ToString(drdShowRecord["EMAIL_BCC"]);
            //        txtEmailBCC2.Text = Convert.ToString(drdShowRecord["EMAIL_BCC2"]);
            //        //--

            //        intCompanyIndex = Convert.ToInt32(Convert.ToString(drdShowRecord["COMPANY_ID"]));
            //        //
            //        if (Convert.ToString(drdShowRecord["INACTIVE_FLAG"]) == "1")
            //        {
            //            chkInactiveSetup.Visible = true;
            //            chkInactiveSetup.Checked = true;
            //        }
            //        else
            //        {
            //            chkInactiveSetup.Visible = false;
            //        }
            //        //
            //        drdShowRecord.Close();
            //        drdShowRecord.Dispose();

            //        cmbCertificateID.Text = strCompanyName + " [" + strTanNo + "]";
            //        cmbCertificateID.Select();

            //        //drdShowRecord.Close();
            //        //drdShowRecord.Dispose();
            //        return true;
            //    }
            //    //-----------------------------------------------------------
            //    drdShowRecord.Close();
            //    drdShowRecord.Dispose();
            //    //-----------------------------------------------------------
            //    cmnService.J_UserMessage(J_Msg.RecNotExist);
            //    //-----------------------------------------------------------
            //    lngSearchId = 0;
            //    //-----------------------------------------------------------
            //    if (strCheckFields == "")
            //        strSQL = strQuery + "order by " + strOrderBy;
            //    else
            //        strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
            //    //-----------------------------------------------------------
            //    if (dsetGridClone != null) dsetGridClone.Clear();
            //    dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            //    return false;
            //}
            //catch (Exception err_handler)
            //{
            //    drdShowRecord.Close();
            //    drdShowRecord.Dispose();
            //    cmnService.J_UserMessage(err_handler.Message);
            //    return false;
            //}
        //}
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (lblSearchMode.Text == J_Mode.Sorting)
                {
                    //if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
                    //    return false;
                    //}
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    if (grpSearch.Visible == false)
                    {
                        if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            if (dsetGridClone == null) return false;
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtEmailDescriptionSearch.Text.Trim()          == ""  &&
                            txtFromEmailIDSearch.Text.Trim() == "" )
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtEmailDescriptionSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    //-----------------------------------------------------------------------
                    //-- CERTIFICATE ID 
                    //-----------------------------------------------------------------------
                    if (cmbCertificateID.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Company - Cannot be Blank");
                        cmbCertificateID.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- FA YEAR
                    //-----------------------------------------------------------------------
                    if (cmbFAYear.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("FA Year - Cannot be Blank");
                        cmbFAYear.Select();
                        return false;
                    }
                    //---
                    if (Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) > 2)
                    {
                        //-----------------------------------------------------------------------
                        //-- QTR
                        //-----------------------------------------------------------------------
                        if (cmbQtr.SelectedIndex <= 0)
                        {
                            cmnService.J_UserMessage("QTR - Cannot be Blank");
                            cmbQtr.Select();
                            return false;
                        }
                        //-----------------------------------------------------------------------
                        //-- FORM NO
                        //-----------------------------------------------------------------------
                        if (cmbFormNo.SelectedIndex <= 0)
                        {
                            cmnService.J_UserMessage("Form No. - Cannot be Blank");
                            cmbFormNo.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- COMPANY NAME 
                    //-----------------------------------------------------------------------
                    if (cmbCompanyName.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Company Name - Cannot be Blank");
                        cmbCompanyName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- EMAIL SETUP
                    //-----------------------------------------------------------------------
                    if (cmbEmailSetup.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Email Setup - Cannot be Blank");
                        cmbEmailSetup.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- EMAIL FORMAT
                    //-----------------------------------------------------------------------
                    if (cmbEmailFormat.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Email Format - Cannot be Blank");
                        cmbEmailFormat.Select();
                        return false;
                    }
                    //--
                    if (blnEdit == false)
                    {
                        if (txtPath.Text == "")
                        {
                            cmnService.J_UserMessage("Certificate folder path - Cannot be Blank");
                            btnChoosePath.Select();
                            return false;
                        }
                        //--
                        if (cmnService.J_IsFolderExist(txtPath.Text) == false)
                        {
                            cmnService.J_UserMessage("Certificate folder path - Does not exist");
                            btnChoosePath.Select();
                            return false;
                        }
                    }
                    //--
                    return true;
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            //try
            //{
            //    //--------------------------------------------
            //    int intSSL = 0;
            //    if (cmbSSL.Text == T_YES_NO.YES)
            //        intSSL = 1;
            //    //--------------------------------------------
            //    switch (lblMode.Text)
            //    {
            //        #region ADD
            //        case J_Mode.Add:
            //            //*****  For Insert
            //            //-----------------------------------------------------------
            //            if (ValidateFields() == false) return;
            //            //-----------------------------------------------------------
            //            if (cmnService.J_SaveConfirmationMessage(ref cmbCertificateID) == true) return;
            //            //-----------------------------------------------------------
            //            dmlService.J_BeginTransaction();
            //            //-----------------------------------------------------------
            //            strSQL = "INSERT INTO SYS_EMAIL_SETUP(" +
            //                    "            SETUP_DESC," +
            //                    "            COMPANY_ID," +
            //                    "            FROM_EMAIL_ID," +
            //                    "            FROM_NAME," +
            //                    "            SMTP_USERNAME," +
            //                    "            SMTP_PASSWORD," +
            //                    "            SMTP_PORT," +
            //                    "            SMTP_HOST," +
            //                    "            SMTP_SSL," +
            //                    "            EMAIL_REPLY_TO," +
            //                    "            EMAIL_CC," +
            //                    "            EMAIL_CC2," +
            //                    "            EMAIL_BCC," +
            //                    "            EMAIL_BCC2) " +
            //                    "     VALUES('" + cmnService.J_ReplaceQuote(txtEmailDescription.Text.Trim()) + "'," +
            //                    "             " + Convert.ToInt32(Support.GetItemData(cmbCertificateID, cmbCertificateID.SelectedIndex)) + "," +
            //                    "            '" + cmnService.J_ReplaceQuote(cmnService.J_ReplaceQuote(txtFromEmailId.Text.Trim())) + "'," +
            //                    "            '" + cmnService.J_ReplaceQuote(txtFromDisplayName.Text.Trim()) + "'," +
            //                    "            '" + cmnService.J_ReplaceQuote(txtSMTPUserName.Text.Trim()) + "'," +
            //                    "            '" + cmnService.J_ReplaceQuote(txtSMTPPassword.Text.Trim()) + "'," +
            //                    "            '" + cmnService.J_ReplaceQuote(txtSMTPPort.Text.Trim()) + "'," +
            //                    "            '" + cmnService.J_ReplaceQuote(txtSMTPHost.Text.Trim()) + "'," +
            //                    "             " + intSSL + "," +
            //                    "            '" + cmnService.J_ReplaceQuote(txtEmailReplyTo.Text.Trim()) + "'," +
            //                    "            '" + cmnService.J_ReplaceQuote(txtEmailCC.Text.Trim()) + "'," +
            //                    "            '" + cmnService.J_ReplaceQuote(txtEmailCC2.Text.Trim()) + "'," +
            //                    "            '" + cmnService.J_ReplaceQuote(txtEmailBCC.Text.Trim()) + "'," +
            //                    "            '" + cmnService.J_ReplaceQuote(txtEmailBCC2.Text) + "' )";
            //            //-----------------------------------------------------------
            //            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
            //            {
            //                cmbCertificateID.Select();
            //                dmlService.J_Rollback();
            //                return;
            //            }
            //            //-----------------------------------------------------------
            //            lngSearchId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "SYS_EMAIL_SETUP", "EMAIL_SETUP_ID");
            //            if (lngSearchId == 0)
            //            {
            //                dmlService.J_Rollback();
            //                return;
            //            }
            //            //-----------------------------------------------------------
            //            //-----------------------------------------------------------
            //            #region COMMENT
            //            ////Added by Indrajit on 11-02-2013
            //            ////-----------------------------------------------------------------------
            //            ////-- EMPLOYEE NAME
            //            ////-----------------------------------------------------------------------
            //            //strSQL = "SELECT COUNT(EMPLOYEE_ID) " +
            //            //    "     FROM   MST_EMPLOYEE " +
            //            //    "     WHERE  EMPLOYEE_NAME ='" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "'" +
            //            //    "     AND    EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "'" +
            //            //    "     AND    COMPANY_ID    = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " " +
            //            //    "     AND    EMPLOYEE_ID  <> " + lngSearchId;
            //            ////--
            //            //if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
            //            //{
            //            //    cmnService.J_UserMessage("Employee Name exists");
            //            //    txtEmployeeName.Select();
            //            //    dmlService.J_Rollback();
            //            //    return;
            //            //}
            //            //End of add zone  ------------------------------------------
            //            #endregion
            //            //--
            //            dmlService.J_Commit();
            //            cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
            //            //-----------------------------------------------------------
            //            ClearControls();
            //            //-----------------------------------------------------------
            //            txtEmailDescription.Select();
            //            //-----------------------------------------------------------
            //            break;
            //        #endregion

            //        #region EDIT
            //        case J_Mode.Edit:
            //            //*****  For Modify
            //            //-----------------------------------------------------------
            //            if (ValidateFields() == false) return;
            //            //-----------------------------------------------------------
            //            #region COMMENT
            //            ////if (cmnService.J_SaveConfirmationMessage(ref cmbEmployeeCategory) == true) return;
            //            ////-----------------------------------------------------------
            //            ////-----------------------------------------------------------
            //            ///*Added by Shrey Kejriwal on 17/08/2011 to check if the employee's company is changed 
            //            // and the same is used in the earlier company's transaction then user is not allowed to update*/

            //            ////--Checking Employee records in transaction table
            //            //strSQL = "SELECT COUNT(*) " +
            //            //         "FROM   TRN_BASIC_INFO, " +
            //            //         "       TRN_DEDUCTEE_DETAILS " +
            //            //         "WHERE  TRN_BASIC_INFO.BASIC_INFO_ID  = TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID " +
            //            //         "AND    TRN_BASIC_INFO.FORM_NO        = '" + T_FormNo.F24Q + "' " +
            //            //         "AND    TRN_BASIC_INFO.COMPANY_ID     <> " + Support.GetItemData(cmbCompanyName,cmbCompanyName.SelectedIndex) + " " +
            //            //         "AND    TRN_DEDUCTEE_DETAILS.PARTY_ID = " + lngSearchId + " ";

            //            //intCountRecords = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

            //            ////Checking employee records in salary detail table

            //            //strSQL = "SELECT COUNT(*) " +
            //            //         "FROM   TRN_BASIC_INFO, " +
            //            //         "       TRN_SALARY_DETAILS " +
            //            //         "WHERE  TRN_BASIC_INFO.BASIC_INFO_ID   = TRN_SALARY_DETAILS.BASIC_INFO_ID " +
            //            //         "AND    TRN_BASIC_INFO.COMPANY_ID      <> " + Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex) + " " +
            //            //         "AND    TRN_SALARY_DETAILS.EMPLOYEE_ID = " + lngSearchId + " ";

            //            //intCountRecords = intCountRecords + Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

            //            //if (intCountRecords > 0)
            //            //{
            //            //    cmnService.J_UserMessage(" Employee's company cannot be updated.\n To update this  employee's company, delete all the transactions of this employee in other companies.");
            //            //    return;
            //            //}
            //            //Added by Indrajit on 11-02-2013
            //            //if (Check_Record(lngSearchId) == 0) return;
            //            #endregion
            //            //----------
            //            int intInactiveSetup = 0;
            //            //
            //            if (chkInactiveSetup.Checked == true)
            //            {
            //                intInactiveSetup = 1;
            //            }                        
            //            //-----------------------------------------------------------
                        
            //            dmlService.J_BeginTransaction();

            //            strSQL = "UPDATE SYS_EMAIL_SETUP " +
            //                     "SET    SETUP_DESC     = '" + cmnService.J_ReplaceQuote(txtEmailDescription.Text.Trim()) + "'," +
            //                     "       FROM_EMAIL_ID  = '" + cmnService.J_ReplaceQuote(txtFromEmailId.Text.Trim()) + "'," +
            //                     "       FROM_NAME      = '" + cmnService.J_ReplaceQuote(txtFromDisplayName.Text.Trim()) + "'," +
            //                     "       SMTP_USERNAME  = '" + cmnService.J_ReplaceQuote(txtSMTPUserName.Text.Trim()) + "', " +
            //                     "       SMTP_PASSWORD  = '" + cmnService.J_ReplaceQuote(txtSMTPPassword.Text) + "', " +
            //                     "       SMTP_PORT      = '" + cmnService.J_ReplaceQuote(txtSMTPPort.Text) + "', " +
            //                     "       SMTP_HOST      = '" + cmnService.J_ReplaceQuote(txtSMTPHost.Text) + "', " +
            //                     "       SMTP_SSL       = " + intSSL + ", " +
            //                     "       EMAIL_REPLY_TO = '" + cmnService.J_ReplaceQuote(txtEmailReplyTo.Text) + "', " +
            //                     "       EMAIL_CC       = '" + cmnService.J_ReplaceQuote(txtEmailCC.Text) + "', " +
            //                     "       EMAIL_CC2      = '" + cmnService.J_ReplaceQuote(txtEmailCC2.Text) + "', " +
            //                     "       EMAIL_BCC      = '" + cmnService.J_ReplaceQuote(txtEmailBCC.Text) + "', " +
            //                     "       EMAIL_BCC2     = '" + cmnService.J_ReplaceQuote(txtEmailBCC2.Text) + "', " +
            //                     "       INACTIVE_FLAG  = " + intInactiveSetup + " " +
            //                     "WHERE  EMAIL_SETUP_ID =  " + lngSearchId + "";
            //            //-----------------------------------------------------------
            //            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
            //            {
            //                cmbCertificateID.Select();
            //                dmlService.J_Rollback();
            //                return;
            //            }
            //            //-----------------------------------------------------------
            //            //-- UPDATE THE TRANSACTION
            //            //..........................................................
            //            //-----------------------------------------------------------
            //            dmlService.J_Commit();
            //            cmnService.J_PanelMessage(0, J_Msg.EditModeSave);
            //            //-----------------------------------------------------------
            //            ClearControls();
            //            //-----------------------------------------------------------
            //            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //            //-----------------------------------------------------------
            //            if (dsetGridClone != null) dsetGridClone.Clear();
            //            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            //            if (dsetGridClone == null) return;
            //            //-----------------------------------------------------------
            //            lblMode.Text = J_Mode.View;
            //            cmnService.J_StatusButton(this, lblMode.Text);
            //            //-----------------------------------------------------------
            //            //
            //            //-----------------------------------------------------------
            //            ControlVisible(false);
            //            //
            //            cmbCertificateID.Enabled = true;
            //            //-----------------------------------------------------------
            //            dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
            //            break;
            //        #endregion

            //        #region DELETE
            //        case J_Mode.Delete:
            //            //-----------------------------------------------------------
            //            //Added by Indrajit on 11-02-2013
            //            //if (Check_Record(lngSearchId) == 0) return;
            //            //----------
            //            //-----------------------------------------------------------                        
            //            dmlService.J_BeginTransaction();
            //            //-----------------------------------------------------------
            //            //-- CHECK THE TRANSACTION
            //            //-----------------------------------------------------------------------
            //            #region COMMENT
            //            //-- TRN_DEDUCTEE_DETAILS
            //            //-----------------------------------------------------------------------
            //            //strSQL = "SELECT PARTY_ID " +
            //            //    "     FROM   TRN_DEDUCTEE_DETAILS " +
            //            //    "     WHERE  PARTY_ID = " + lngSearchId + " ";
            //            //strSQL = "SELECT PARTY_ID " +
            //            //    "     FROM   TRN_DEDUCTEE_DETAILS," +
            //            //    "            TRN_BASIC_INFO " +
            //            //    "     WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID " +
            //            //    "     AND    TRN_BASIC_INFO.FORM_NO             = '" + T_FormNo.F24Q + "' " +
            //            //    "     AND    TRN_DEDUCTEE_DETAILS.PARTY_ID      = " + lngSearchId + " ";
            //            ////--
            //            //if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
            //            //{
            //            //    cmnService.J_UserMessage("The Employee cannot be deleted");
            //            //    BtnDelete.Select();
            //            //    dmlService.J_Rollback();
            //            //    return;
            //            //}
            //            #endregion
            //            //-------------------------------------------------
            //            //-- TRN_SALARY_DETAILS
            //            strSQL = "SELECT EMAIL_SETUP_ID " +
            //                "     FROM   TRN_EMAIL_LOG_HEADER " +
            //                "     WHERE  EMAIL_SETUP_ID = " + lngSearchId + " ";
            //            //--
            //            if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
            //            {
            //                cmnService.J_UserMessage("The Email Setup cannot be deleted");
            //                BtnDelete.Select();
            //                dmlService.J_Rollback();
            //                return;
            //            }
            //            //..........................................................
            //            if (cmnService.J_UserMessage("Proceed Deletion?", MessageBoxButtons.YesNo) == DialogResult.No)
            //            {
            //                lblMode.Text = J_Mode.View;
            //                return;
            //            }
            //            //-----------------------------------------------------------
            //            strSQL = "DELETE FROM SYS_EMAIL_SETUP WHERE EMAIL_SETUP_ID =  " + lngSearchId + "";
            //            //-----------------------------------------------------------
            //            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
            //            {
            //                lblMode.Text = J_Mode.View;
            //                dmlService.J_Rollback();
            //                return;
            //            }
            //            //-----------------------------------------------------------
            //            dmlService.J_Commit();
            //            cmnService.J_PanelMessage(0, J_Msg.DeleteMode);
            //            //-----------------------------------------------------------
            //            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //            //-----------------------------------------------------------
            //            if (dsetGridClone != null) dsetGridClone.Clear();
            //            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            //            if (dsetGridClone == null) return;
            //            //-----------------------------------------------------------
            //            lblMode.Text = J_Mode.View;
            //            cmnService.J_StatusButton(this, lblMode.Text);
            //            //-----------------------------------------------------------
            //            dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "EMAIL_SETUP_ID", lngSearchId);
            //            break;
            //        #endregion
            //    }
            //}
            //catch (Exception err_handler)
            //{
            //    dmlService.J_Rollback();
            //    cmnService.J_UserMessage(err_handler.Message);
            //}
        }
        #endregion   

        //Added by Indrajit on 11-02-2013
        #region Check_Record
        private long Check_Record(long lngSrchId)
        {
            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, "SYS_EMAIL_SETUP", "EMAIL_SETUP_ID", lngSrchId) == true) return lngSrchId;

            cmnService.J_UserMessage("Record has been deleted.");
            lngSrchId = 0;
            //-------------------------------------------
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
            //-------------------------------------------
            ControlVisible(false);
            ClearControls();					//Clear all the Controls
            //-------------------------------------------
            strSQL = strQuery + "order by " + strOrderBy;
            //-------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            if (dsetGridClone == null) return 0;
            //-------------------------------------------
            BtnAdd.Select();
            //-------------------------------------------                            
            return 0;

        }
        #endregion


        #region CREATE_TEMP_TABLES
        private bool CREATE_TEMP_TABLES()
        {
            DMLService dmlService1 = new DMLService();               
            try
            {
                //
                //MessageBox.Show("1");
                dmlService1.J_BeginTransaction();
                if (dmlService1.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_PDF_PAN) == true)
                {
                    //MessageBox.Show("1.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PDF_PAN + "";
                    dmlService1.J_ExecSql(dmlService1.J_pCommand, strSQL);
                }
                //
                dmlService1.J_Commit();
                //MessageBox.Show("2");
                //
                dmlService1.J_BeginTransaction();
                if (dmlService1.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_PDF_PAN + "") == false)
                {
                    //MessageBox.Show("2.1");
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PDF_PAN + " (" +
                         "                  " + cmnService.J_GetDataType("PDF_PAN_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("PDF_PAN", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("PDF_PATH", J_ColumnType.String, 255) + ")";
                    dmlService1.J_ExecSql(dmlService1.J_pCommand, strSQL);
                }
                dmlService1.J_Commit();
                //MessageBox.Show("3");
                //
                dmlService1.J_BeginTransaction();
                if (dmlService1.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + "") == true)
                {
                    //MessageBox.Show("3.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + "";
                    dmlService1.J_ExecSql(dmlService1.J_pCommand, strSQL);
                }
                dmlService1.J_Commit();
                //
                //MessageBox.Show("4");
                dmlService1.J_BeginTransaction();
                if (dmlService1.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + "") == false)
                {
                    //MessageBox.Show("4.1");
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_EMAIL_GRID_ITEMS_SELECTED + " (" +
                         "                  " + cmnService.J_GetDataType("PARTY_ID", J_Identity.NO) + "," +
                         "                  " + cmnService.J_GetDataType("PARTY_NAME", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("PARTY_PAN", J_ColumnType.String, 10) + ")";
                    dmlService1.J_ExecSql(dmlService1.J_pCommand, strSQL);
                }
                //MessageBox.Show("5");
                dmlService1.J_Commit();
                dmlService1.Dispose();
                return true;
            }
            catch
            {
                dmlService1.J_Rollback();
                return false;
            }
        }
        #endregion

        #region DROP_TEMP_TABLES
        private bool DROP_TEMP_TABLES()
        {
            try
            {
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_PDF_PAN + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PDF_PAN + "";
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion             

        #region SendEmail
        private bool SendEmail(string EmailFrom,
                               string DisplayName,
                               string EmailReplyTo,
                               string EmailIdCC,
                               string EmailIdCC2,
                               string EmailIdBCC,
                               string EmailIdBCC2,
                               string EmailSubject,
                               string EmailBody,
                               bool   EmailHTML,
                               string EmailIdTo,
                               string PartyName,
                               string PartyPAN,
                               string CompanyName,
                               string pdfPath)
        {
            //------------------------------------------------------------------
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient();
            //------------------------------------------------------------------
            MailMessage mail = new MailMessage();
            //--
            try
            {
                SmtpServer.Credentials = new System.Net.NetworkCredential
                         (strSMTPUserName, Convert.ToString(strSMTPPassword));
                SmtpServer.Port = Convert.ToInt32(strSMTPPort);
                SmtpServer.Host = strSMTPHost;
                //--
                if (strSSL == T_YES_NO.YES)
                    SmtpServer.EnableSsl = true;
                else
                    SmtpServer.EnableSsl = false;
                //--
                if(strServerAuthentication == "")
                    SmtpServer.UseDefaultCredentials = true;
                //--------------------------------------------------------------------
                if (EmailIdTo.EndsWith(",") == true)
                    EmailIdTo = EmailIdTo.Substring(0, EmailIdTo.Length - 1);
                //--------------------------------------------------------------------
                //if (EmailIdBCC.EndsWith(",") == true)
                //    EmailIdBCC = EmailIdBCC.Substring(0, EmailIdBCC.Length - 1);
                //--------------------------------------------------------------------
                if (EmailIdTo.Trim() == "") return false;
                //--------------------------------------------------------------------                
                this.Cursor = Cursors.WaitCursor;
                //---------------------------------------------------------------------
                mail.From = new System.Net.Mail.MailAddress(EmailFrom, DisplayName, System.Text.Encoding.UTF8);
                //mail.Priority = MailPriority.High;
                mail.ReplyTo = new System.Net.Mail.MailAddress(EmailReplyTo);
                //--
                #region EMAIL CC
                if (EmailIdCC.Trim() != "")
                {
                    if (EmailIdCC2.Trim() != "")
                        mail.CC.Add(EmailIdCC + "," + EmailIdCC2);
                    else
                        mail.CC.Add(EmailIdCC);
                }
                #endregion
                //--
                #region EMAIL BCC
                if (EmailIdBCC.Trim() != "")
                {
                    if (EmailIdBCC2.Trim() != "")
                        mail.Bcc.Add(EmailIdBCC + "," + EmailIdBCC2);
                    else
                        mail.Bcc.Add(EmailIdBCC);
                }
                #endregion
                //--
                mail.To.Add(EmailIdTo);
                //-- 
                #region EMAILSUBJECT 
                EmailSubject = EmailSubject.Replace("#FA_YEAR#", cmbFAYear.Text);
                EmailSubject = EmailSubject.Replace("#QTR#", cmbQtr.Text);
                mail.Subject = EmailSubject;
                #endregion
                // 
                #region EMAIL BODY WITH ATTACHMENT
                //-- PDF ATTACHMENT
                if (File.Exists(pdfPath) == true)
                {
                    FileStream fs1 = new FileStream(pdfPath, FileMode.Open, FileAccess.Read);
                    System.Net.Mail.Attachment a1 = new System.Net.Mail.Attachment(fs1, Path.GetFileName(pdfPath), MediaTypeNames.Application.Octet);
                    mail.Attachments.Add(a1);
                }
                //---------------------------------------------------------------------
                //-- BODY
                EmailBody = EmailBody.Replace("#COMPANY_NAME#", cmnService.J_Mid(cmbCompanyName.Text, 0, cmbCompanyName.Text.Length - 12));
                EmailBody = EmailBody.Replace("#TAN_NO#", cmnService.J_Right(cmbCompanyName.Text, 11).Replace("]", ""));
                EmailBody = EmailBody.Replace("#PARTY_NAME#", PartyName);
                EmailBody = EmailBody.Replace("#PAN_NO#", PartyPAN);
                //                
                StringBuilder htmlString = new StringBuilder();
                htmlString.Append(EmailBody);
                #endregion
                //
                mail.Body = htmlString.ToString();
                mail.IsBodyHtml = EmailHTML;
                mail.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;
                SmtpServer.Timeout = 300000;//(5min) 
                SmtpServer.Send(mail);
                //
                this.Cursor = Cursors.Default;
                //
                return true;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.ToString());
                //this.Cursor = Cursors.Default;
                return false;
            }
        }
        #endregion

        #region SendEmailTLSEAMail
        //private bool SendEmailTLSEAMail(string EmailFrom,
        //                       string DisplayName,
        //                       string EmailReplyTo,
        //                       string EmailIdCC,
        //                       string EmailIdCC2,
        //                       string EmailIdBCC,
        //                       string EmailIdBCC2,
        //                       string EmailSubject,
        //                       string EmailBody,
        //                       bool EmailHTML,
        //                       string EmailIdTo,
        //                       string PartyName,
        //                       string PartyPAN,
        //                       string CompanyName,
        //                       string pdfPath)
        //{
        //    //------------------------------------------------------------------
        //    //SmtpClient SmtpServer = new SmtpClient();
        //    ////------------------------------------------------------------------
        //    //MailMessage mail = new MailMessage();
        //    //--
        //    try
        //    {
        //        SmtpMail smtpMail = new SmtpMail("Tryit");

        //        smtpMail.From = new EASendMail.MailAddress(DisplayName, EmailFrom); 

        //        #region EMAIL CC
        //        if (EmailIdCC.Trim() != "")
        //        {
        //            if (EmailIdCC2.Trim() != "")
        //                smtpMail.Cc.Add(EmailIdCC + "," + EmailIdCC2);
        //            else
        //                smtpMail.Cc.Add(EmailIdCC);
        //        }
        //        #endregion
        //        //--
        //        #region EMAIL BCC
        //        if (EmailIdBCC.Trim() != "")
        //        {
        //            if (EmailIdBCC2.Trim() != "")
        //                smtpMail.Bcc.Add(EmailIdBCC + "," + EmailIdBCC2);
        //            else
        //                smtpMail.Bcc.Add(EmailIdBCC);
        //        }
        //        #endregion
        //        //--
        //        smtpMail.To.Add(EmailIdTo);
        //        smtpMail.ReplyTo = EmailReplyTo;

        //        // Your SMTP server address
        //        SmtpServer smtpServer = new SmtpServer(strSMTPHost);
        //        // User and password for ESMTP authentication, if your server doesn't require
        //        // User authentication, please remove the following codes.
        //        smtpServer.User = strSMTPUserName;
        //        smtpServer.Password = Convert.ToString(strSMTPPassword);
        //        // Set 25 or 587 port.
        //        smtpServer.Port = Convert.ToInt32(strSMTPPort);
        //        smtpServer.DeliveryNotification = EASendMail.DeliveryNotificationOptions.OnFailure;
        //        // detect TLS connection automatically
        //        smtpServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
        //        //Console.WriteLine("start to send email ...");
        //        //EASendMail.SmtpClient smtpClient = new EASendMail.SmtpClient();
        //        //smtpClient.SendMail(smtpServer, smtpMail);
        //        //smtpClient.Timeout = 300000;//(5min) 
        //        //--------------------------------------------------------------------
        //        if (EmailIdTo.EndsWith(",") == true)
        //            EmailIdTo = EmailIdTo.Substring(0, EmailIdTo.Length - 1);
        //        //--------------------------------------------------------------------
        //        //if (EmailIdBCC.EndsWith(",") == true)
        //        //    EmailIdBCC = EmailIdBCC.Substring(0, EmailIdBCC.Length - 1);
        //        //--------------------------------------------------------------------
        //        if (EmailIdTo.Trim() == "") return false;
        //        //--------------------------------------------------------------------                
        //        this.Cursor = Cursors.WaitCursor;
        //        //---------------------------------------------------------------------
        //        //mail.From = new MailAddress(EmailFrom, DisplayName, System.Text.Encoding.UTF8);
        //        ////mail.Priority = MailPriority.High;
        //        //mail.ReplyTo = new MailAddress(EmailReplyTo);
        //        //--
        //        #region EMAIL CC
        //        //if (EmailIdCC.Trim() != "")
        //        //{
        //        //    if (EmailIdCC2.Trim() != "")
        //        //        mail.CC.Add(EmailIdCC + "," + EmailIdCC2);
        //        //    else
        //        //        mail.CC.Add(EmailIdCC);
        //        //}
        //        #endregion
        //        //--
        //        #region EMAIL BCC
        //        //if (EmailIdBCC.Trim() != "")
        //        //{
        //        //    if (EmailIdBCC2.Trim() != "")
        //        //        mail.Bcc.Add(EmailIdBCC + "," + EmailIdBCC2);
        //        //    else
        //        //        mail.Bcc.Add(EmailIdBCC);
        //        //}
        //        #endregion
        //        //--
        //        //mail.To.Add(EmailIdTo);
        //        //-- 
        //        #region EMAILSUBJECT 
        //        EmailSubject = EmailSubject.Replace("#FA_YEAR#", cmbFAYear.Text);
        //        EmailSubject = EmailSubject.Replace("#QTR#", cmbQtr.Text);
        //        smtpMail.Subject = EmailSubject;
        //        #endregion
        //        // 
        //        #region EMAIL BODY WITH ATTACHMENT
        //        //-- PDF ATTACHMENT
        //        if (File.Exists(pdfPath) == true)
        //        {
        //            //FileStream fs1 = new FileStream(pdfPath, FileMode.Open, FileAccess.Read);
        //            //Attachment a1 = new Attachment(fs1, Path.GetFileName(pdfPath), MediaTypeNames.Application.Octet);
        //            //mail.Attachments.Add(a1);
        //            smtpMail.AddAttachment(pdfPath);
        //        }
        //        //---------------------------------------------------------------------
        //        //-- BODY
        //        EmailBody = EmailBody.Replace("#COMPANY_NAME#", cmnService.J_Mid(cmbCompanyName.Text, 0, cmbCompanyName.Text.Length - 12));
        //        EmailBody = EmailBody.Replace("#TAN_NO#", cmnService.J_Right(cmbCompanyName.Text, 11).Replace("]", ""));
        //        EmailBody = EmailBody.Replace("#PARTY_NAME#", PartyName);
        //        EmailBody = EmailBody.Replace("#PAN_NO#", PartyPAN);
        //        //                
        //        StringBuilder htmlString = new StringBuilder();
        //        htmlString.Append(EmailBody);
        //        #endregion
        //        //
        //        smtpMail.HtmlBody = EmailBody;
        //        //mail.Body = htmlString.ToString();
        //        //mail.IsBodyHtml = EmailHTML;
        //        //mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        //        //SmtpServer.Timeout = 300000;//(5min) 
        //        //SmtpServer.Send(mail);
        //        smtpServer.DeliveryNotification = EASendMail.DeliveryNotificationOptions.OnFailure;
        //        // detect TLS connection automatically
        //        smtpServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
        //        EASendMail.SmtpClient smtpClient = new EASendMail.SmtpClient();
        //        smtpClient.SendMail(smtpServer, smtpMail);
        //        smtpClient.Timeout = 300000;//(5min) 
        //        //
        //        this.Cursor = Cursors.Default;
        //        //
        //        return true;
        //    }
        //    catch (Exception err)
        //    {
        //        this.Cursor = Cursors.Default;
        //        cmnService.J_UserMessage(err.ToString());
        //        //this.Cursor = Cursors.Default;
        //        return false;
        //    }
        //}
        #endregion

        #region SendEmailTLS //-- commented for jana bank
        ////private bool SendEmailTLS(string EmailFrom,
        ////                       string DisplayName,
        ////                       string EmailReplyTo,
        ////                       string EmailIdCC,
        ////                       string EmailIdCC2,
        ////                       string EmailIdBCC,
        ////                       string EmailIdBCC2,
        ////                       string EmailSubject,
        ////                       string EmailBody,
        ////                       bool EmailHTML,
        ////                       string EmailIdTo,
        ////                       string PartyName,
        ////                       string PartyPAN,
        ////                       string CompanyName,
        ////                       string pdfPath)
        ////{
        ////    //------------------------------------------------------------------
        ////    System.Net.Mail.SmtpClient smtpMail = new System.Net.Mail.SmtpClient();
        ////    //------------------------------------------------------------------
        ////    MailMessage mail = new MailMessage();
        ////    //--
        ////    try
        ////    {
        ////        smtpMail.Credentials = new System.Net.NetworkCredential
        ////                    (Convert.ToString(strSMTPUserName), Convert.ToString(strSMTPPassword));
        ////        smtpMail.Port = Convert.ToInt32(strSMTPPort); //-- JIO & ALL = 587
        ////        smtpMail.Host = strSMTPHost;
        ////        //smtpMail.EnableSsl = true;
        ////        //
        ////        mail.From = new MailAddress(EmailFrom, DisplayName, System.Text.Encoding.UTF8);
        ////        mail.Priority = MailPriority.High;
        ////        mail.ReplyTo = new MailAddress(EmailReplyTo);
        ////        //
        ////        #region EMAIL CC
        ////        if (EmailIdCC.Trim() != "")
        ////        {
        ////            if (EmailIdCC2.Trim() != "")
        ////            {
        ////                mail.CC.Add(EmailIdCC + "," + EmailIdCC2);
        ////            }
        ////            else
        ////            {
        ////                mail.CC.Add(EmailIdCC);
        ////            }
        ////        }
        ////        #endregion
        ////        //--
        ////        #region EMAIL BCC
        ////        if (EmailIdBCC.Trim() != "")
        ////        {
        ////            if (EmailIdBCC2.Trim() != "")
        ////            {
        ////                mail.Bcc.Add(EmailIdBCC + "," + EmailIdBCC2);
        ////            }
        ////            else
        ////            {
        ////                mail.Bcc.Add(EmailIdBCC);
        ////            }
        ////        }
        ////        #endregion
        ////        //--
        ////        mail.To.Add(EmailIdTo);
        ////        mail.ReplyTo = new MailAddress(EmailReplyTo);
        ////        //--------------------------------------------------------------------
        ////        if (EmailIdTo.EndsWith(",") == true)
        ////            EmailIdTo = EmailIdTo.Substring(0, EmailIdTo.Length - 1);
        ////        //--------------------------------------------------------------------
        ////        //--------------------------------------------------------------------
        ////        if (EmailIdTo.Trim() == "") return false;
        ////        //--------------------------------------------------------------------                
        ////        this.Cursor = Cursors.WaitCursor;
        ////        //---------------------------------------------------------------------
        ////        //-- 
        ////        #region EMAILSUBJECT 
        ////        EmailSubject = EmailSubject.Replace("#FA_YEAR#", cmbFAYear.Text);
        ////        EmailSubject = EmailSubject.Replace("#QTR#", cmbQtr.Text);
        ////        //smtpMail.Subject = EmailSubject;
        ////        mail.Subject = EmailSubject;
        ////        #endregion
        ////        // 
        ////        #region EMAIL BODY WITH ATTACHMENT
        ////        //-- PDF ATTACHMENT
        ////        if (File.Exists(pdfPath) == true)
        ////        {
        ////            FileStream fs1 = new FileStream(pdfPath, FileMode.Open, FileAccess.Read);
        ////            Attachment a1 = new Attachment(fs1, Path.GetFileName(pdfPath), MediaTypeNames.Application.Octet);
        ////            mail.Attachments.Add(a1);
        ////        }
        ////        //---------------------------------------------------------------------
        ////        //-- BODY
        ////        EmailBody = EmailBody.Replace("#COMPANY_NAME#", cmnService.J_Mid(cmbCompanyName.Text, 0, cmbCompanyName.Text.Length - 12));
        ////        EmailBody = EmailBody.Replace("#TAN_NO#", cmnService.J_Right(cmbCompanyName.Text, 11).Replace("]", ""));
        ////        EmailBody = EmailBody.Replace("#PARTY_NAME#", PartyName);
        ////        EmailBody = EmailBody.Replace("#PAN_NO#", PartyPAN);
        ////        //-- 2024/01/16
        ////        EmailBody = EmailBody.Replace("#FORM_NO#", cmbFormNo.Text);
        ////        EmailBody = EmailBody.Replace("#QTR#", cmbQtr.Text);
        ////        EmailBody = EmailBody.Replace("#FA_YEAR#", cmbFAYear.Text);
        ////        //                
        ////        StringBuilder htmlString = new StringBuilder();
        ////        htmlString.Append(EmailBody);
        ////        #endregion
        ////        //
        ////        mail.IsBodyHtml = true;
        ////        //
        ////        mail.Body = htmlString.ToString();
        ////        mail.IsBodyHtml = true;
        ////        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        ////        smtpMail.Timeout = 300000;//(5min) 
        ////        if (strDisableSSLTLS == T_TRUE_FALSE.FALSE.ToString())
        ////        {
        ////            smtpMail.EnableSsl = true;
        ////            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; //-- 2023/06/28
        ////        }
        ////        smtpMail.Send(mail);
        ////        //
        ////        this.Cursor = Cursors.Default;
        ////        //
        ////        return true;
        ////    }
        ////    catch (Exception err)
        ////    {
        ////        this.Cursor = Cursors.Default;
        ////        cmnService.J_UserMessage(err.ToString());
        ////        //this.Cursor = Cursors.Default;
        ////        return false;
        ////    }
        ////}

        private bool SendBulkEmailsTLS(List<(string EmailIdTo, string PartyName, string PartyPAN, string CompanyName, string PdfPath)> recipients,
                           string EmailFrom,
                           string DisplayName,
                           string EmailReplyTo,
                           string EmailIdCC,
                           string EmailIdCC2,
                           string EmailIdBCC,
                           string EmailIdBCC2,
                           string EmailSubject,
                           string EmailBody,
                           bool EmailHTML)
        {
            try
            {
                using (System.Net.Mail.SmtpClient smtpMail = new System.Net.Mail.SmtpClient())
                {
                    // ---------- Setup SMTP client once ----------
                    smtpMail.Credentials = new System.Net.NetworkCredential(
                        Convert.ToString(strSMTPUserName),
                        Convert.ToString(strSMTPPassword));

                    smtpMail.Port = Convert.ToInt32(strSMTPPort);   // usually 587
                    smtpMail.Host = strSMTPHost;
                    smtpMail.Timeout = 300000; // 5 min

                    if (strDisableSSLTLS == T_TRUE_FALSE.FALSE.ToString())
                    {
                        smtpMail.EnableSsl = true;
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    }

                    smtpMail.ServicePoint.MaxIdleTime = 2 * 60 * 1000;   // keep alive 2 mins
                    smtpMail.ServicePoint.ConnectionLimit = 1;           // avoid multiple connections

                    // ---------- Loop through all recipients ----------
                    foreach (var rec in recipients)
                    {
                        if (string.IsNullOrWhiteSpace(rec.EmailIdTo))
                            continue;

                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(EmailFrom, DisplayName, System.Text.Encoding.UTF8);
                            mail.Priority = MailPriority.High;
                            mail.ReplyToList.Add(new MailAddress(EmailReplyTo));

                            // CC
                            if (!string.IsNullOrWhiteSpace(EmailIdCC))
                                mail.CC.Add(EmailIdCC);
                            if (!string.IsNullOrWhiteSpace(EmailIdCC2))
                                mail.CC.Add(EmailIdCC2);

                            // BCC
                            if (!string.IsNullOrWhiteSpace(EmailIdBCC))
                                mail.Bcc.Add(EmailIdBCC);
                            if (!string.IsNullOrWhiteSpace(EmailIdBCC2))
                                mail.Bcc.Add(EmailIdBCC2);

                            // To
                            mail.To.Add(rec.EmailIdTo.Trim());

                            // ---------- Subject ----------
                            string subject = EmailSubject
                                                .Replace("#FA_YEAR#", cmbFAYear.Text)
                                                .Replace("#QTR#", cmbQtr.Text);
                            mail.Subject = subject;

                            // ---------- Body ----------
                            string body = EmailBody;
                            body = body.Replace("#COMPANY_NAME#", cmnService.J_Mid(cmbCompanyName.Text, 0, cmbCompanyName.Text.Length - 12));
                            body = body.Replace("#TAN_NO#", cmnService.J_Right(cmbCompanyName.Text, 11).Replace("]", ""));
                            body = body.Replace("#PARTY_NAME#", rec.PartyName);
                            body = body.Replace("#PAN_NO#", rec.PartyPAN);
                            body = body.Replace("#FORM_NO#", cmbFormNo.Text);
                            body = body.Replace("#QTR#", cmbQtr.Text);
                            body = body.Replace("#FA_YEAR#", cmbFAYear.Text);

                            mail.Body = body;
                            mail.IsBodyHtml = true;
                            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                            // ---------- Attachment ----------
                            if (File.Exists(rec.PdfPath))
                            {
                                Attachment a1 = new Attachment(rec.PdfPath, MediaTypeNames.Application.Octet);
                                mail.Attachments.Add(a1);
                            }

                            // ---------- Send ----------
                            smtpMail.Send(mail);
                        }

                        // ---------- Throttling (1-2 sec gap between emails) ----------
                        Thread.Sleep(2000);
                    }
                }

                return true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.ToString());
                return false;
            }
        }
        #endregion

        #region SendEmailImplicitSSL
        private bool SendEmailImplicitSSL(
                                        string EmailFrom,
                                        string DisplayName,
                                        string EmailReplyTo,
                                        string EmailIdCC,
                                        string EmailIdCC2,
                                        string EmailIdBCC,
                                        string EmailIdBCC2,
                                        string EmailSubject,
                                        string EmailBody,
                                        bool EmailHTML,
                                        string EmailIdTo,
                                        string PartyName,
                                        string PartyPAN,
                                        string CompanyName,
                                        string pdfPath)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(DisplayName, EmailFrom));
                message.ReplyTo.Add(new MailboxAddress("", EmailReplyTo));
                message.To.AddRange(InternetAddressList.Parse(EmailIdTo));

                if (!string.IsNullOrWhiteSpace(EmailIdCC))
                    message.Cc.AddRange(InternetAddressList.Parse(EmailIdCC));
                if (!string.IsNullOrWhiteSpace(EmailIdCC2))
                    message.Cc.AddRange(InternetAddressList.Parse(EmailIdCC2));
                if (!string.IsNullOrWhiteSpace(EmailIdBCC))
                    message.Bcc.AddRange(InternetAddressList.Parse(EmailIdBCC));
                if (!string.IsNullOrWhiteSpace(EmailIdBCC2))
                    message.Bcc.AddRange(InternetAddressList.Parse(EmailIdBCC2));

                // Replace tokens
                EmailSubject = EmailSubject.Replace("#FA_YEAR#", cmbFAYear.Text)
                                           .Replace("#QTR#", cmbQtr.Text);
                message.Subject = EmailSubject;

                EmailBody = EmailBody.Replace("#COMPANY_NAME#", cmnService.J_Mid(cmbCompanyName.Text, 0, cmbCompanyName.Text.Length - 12))
                                     .Replace("#TAN_NO#", cmnService.J_Right(cmbCompanyName.Text, 11).Replace("]", ""))
                                     .Replace("#PARTY_NAME#", PartyName)
                                     .Replace("#PAN_NO#", PartyPAN)
                                     .Replace("#FORM_NO#", cmbFormNo.Text)
                                     .Replace("#QTR#", cmbQtr.Text)
                                     .Replace("#FA_YEAR#", cmbFAYear.Text);

                var builder = new BodyBuilder
                {
                    HtmlBody = EmailBody
                };

                if (File.Exists(pdfPath))
                    builder.Attachments.Add(pdfPath);

                message.Body = builder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(strSMTPHost, 465, MailKit.Security.SecureSocketOptions.SslOnConnect);

                    if (!string.IsNullOrWhiteSpace(strSMTPUserName))
                        client.Authenticate(strSMTPUserName, strSMTPPassword);

                    client.Send(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage("Mail Error: " + ex.Message);
                return false;
            }
        }
        #endregion


        #region SendBulkEmailsOptimized
        private bool SendBulkEmailsOptimized(
                    List<(string EmailIdTo, string PartyName, string PartyPAN, string CompanyName, string PdfPath)> recipients,
                    string EmailFrom,
                    string DisplayName,
                    string EmailReplyTo,
                    string EmailIdCC,
                    string EmailIdCC2,
                    string EmailIdBCC,
                    string EmailIdBCC2,
                    string EmailSubject,
                    string EmailBody,
                    bool EmailHTML)
        {
            System.Net.Mail.SmtpClient smtp = null;
            int mailCounter = 0;

            try
            {
                // ------ Create SMTP Instance ------
                Action createSmtp = () =>
                {
                    smtp = new System.Net.Mail.SmtpClient(strSMTPHost, Convert.ToInt32(strSMTPPort));
                    smtp.Credentials = new NetworkCredential(strSMTPUserName, strSMTPPassword);
                    smtp.Timeout = 240000; // 4 min

                    if (strDisableSSLTLS == T_TRUE_FALSE.FALSE.ToString())
                    {
                        smtp.EnableSsl = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    }

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                };

                createSmtp(); // initial creation

                // ------------------------------------------------
                //             START LOOP FOR ALL EMAILS
                // ------------------------------------------------
                foreach (var rec in recipients)
                {
                    if (string.IsNullOrWhiteSpace(rec.EmailIdTo))
                        continue;

                    mailCounter++;

                    // ---------- After every 40 mails, recreate SMTP to avoid timeouts ----------
                    if (mailCounter % 40 == 0)
                    {
                        try { smtp.Dispose(); } catch { }
                        Thread.Sleep(2000);
                        createSmtp();
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(EmailFrom, DisplayName, Encoding.UTF8);
                        mail.ReplyToList.Add(new MailAddress(EmailReplyTo));
                        mail.SubjectEncoding = Encoding.UTF8;
                        mail.BodyEncoding = Encoding.UTF8;

                        // CC
                        if (!string.IsNullOrWhiteSpace(EmailIdCC))
                            mail.CC.Add(EmailIdCC);
                        if (!string.IsNullOrWhiteSpace(EmailIdCC2))
                            mail.CC.Add(EmailIdCC2);

                        // BCC
                        if (!string.IsNullOrWhiteSpace(EmailIdBCC))
                            mail.Bcc.Add(EmailIdBCC);
                        if (!string.IsNullOrWhiteSpace(EmailIdBCC2))
                            mail.Bcc.Add(EmailIdBCC2);

                        // To
                        mail.To.Add(rec.EmailIdTo.Trim());

                        // SUBJECT
                        mail.Subject = EmailSubject
                            .Replace("#FA_YEAR#", cmbFAYear.Text)
                            .Replace("#QTR#", cmbQtr.Text);

                        // BODY
                        string body = EmailBody;
                        body = body.Replace("#COMPANY_NAME#", cmnService.J_Mid(cmbCompanyName.Text, 0, cmbCompanyName.Text.Length - 12));
                        body = body.Replace("#TAN_NO#", cmnService.J_Right(cmbCompanyName.Text, 11).Replace("]", ""));
                        body = body.Replace("#PARTY_NAME#", rec.PartyName);
                        body = body.Replace("#PAN_NO#", rec.PartyPAN);
                        body = body.Replace("#FORM_NO#", cmbFormNo.Text);
                        body = body.Replace("#QTR#", cmbQtr.Text);
                        body = body.Replace("#FA_YEAR#", cmbFAYear.Text);

                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.High;

                        // ATTACHMENT
                        if (File.Exists(rec.PdfPath))
                        {
                            Attachment a1 = new Attachment(rec.PdfPath, MediaTypeNames.Application.Octet);
                            mail.Attachments.Add(a1);
                        }

                        // ---------- SEND EMAIL ----------
                        smtp.Send(mail);
                    }

                    // ---------- SMALL DELAY TO AVOID SERVER RATE LIMIT ----------
                    Thread.Sleep(300);
                }

                return true;
            }
            catch (SmtpException smtpEx)
            {
                cmnService.J_UserMessage("SMTP Error: " + smtpEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage(ex.ToString());
                return false;
            }
            finally
            {
                try { smtp?.Dispose(); } catch { }
            }
        }

        #endregion

        #region LOAD_EMAIL_FORMAT
        private void LOAD_EMAIL_FORMAT(long EmailCertificateID, long CompanyId)
        {
            //-----------------------------------------------------------
            //strSQL = "SELECT SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID  AS EMAIL_FORMAT_ID," +
            //    "            SYS_EMAIL_FORMAT.EMAIL_FORMAT_DESC AS EMAIL_FORMAT_DESC " +
            //    "     FROM   SYS_EMAIL_FORMAT," +
            //    "            MST_EMAIL_CERTIFICATES," +
            //    "            MST_COMPANY " +
            //    "     WHERE  SYS_EMAIL_FORMAT.COMPANY_ID                 = MST_COMPANY.COMPANY_ID " +
            //    "     AND    SYS_EMAIL_FORMAT.EMAIL_CERTIFICATE_ID       = MST_EMAIL_CERTIFICATES.EMAIL_CERTIFICATE_ID " +
            //    "     AND    SYS_EMAIL_FORMAT.INACTIVE_FLAG               = 0 " +
            //    "     AND    MST_EMAIL_CERTIFICATES.EMAIL_CERTIFICATE_ID = " + EmailCertificateID + " " +
            //    "     AND   (SYS_EMAIL_FORMAT.COMPANY_ID                 = " + CompanyId + " " +
            //    "         OR SYS_EMAIL_FORMAT.COMPANY_ID                 = 0) " +
            //    "     ORDER BY SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID DESC ";
            strSQL = @"SELECT SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID   AS EMAIL_FORMAT_ID, 
                              SYS_EMAIL_FORMAT.EMAIL_FORMAT_DESC AS EMAIL_FORMAT_DESC 
                       FROM  ((SYS_EMAIL_FORMAT LEFT JOIN MST_COMPANY
                       ON SYS_EMAIL_FORMAT.COMPANY_ID            = MST_COMPANY.COMPANY_ID)
                            LEFT JOIN MST_EMAIL_CERTIFICATES
                       ON  SYS_EMAIL_FORMAT.EMAIL_CERTIFICATE_ID = MST_EMAIL_CERTIFICATES.EMAIL_CERTIFICATE_ID )
                       WHERE SYS_EMAIL_FORMAT.INACTIVE_FLAG      = 0 
                       AND   MST_EMAIL_CERTIFICATES.EMAIL_CERTIFICATE_ID = " + EmailCertificateID + @" 
                       AND  (SYS_EMAIL_FORMAT.COMPANY_ID = 0 OR SYS_EMAIL_FORMAT.COMPANY_ID = " + CompanyId + @") 
                      ORDER  BY SYS_EMAIL_FORMAT.EMAIL_FORMAT_ID DESC ";
            //
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbEmailFormat) == false) return;
            //-----------------------------------------------------------
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=DyGKMf4wVUw");            
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0036", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));  
        }













        #endregion

        #region J_ReturnServerDateTimeMMDDYYYYHHMMSS
        public string J_ReturnServerDateTimeMMDDYYYYHHMMSS()
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),120)";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strSQL = "SELECT FORMAT(NOW(),'MM/dd/yyyy HH:MM:SS')";
            else
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            //--
            return cmnService.J_NullToText(dmlService.J_ExecSqlReturnScalar(strSQL));
        }
        #endregion

        #region ExportToCSV
        protected void ExportToCSV(string strSQL, string CSVFile)
        {
            //-- https://www.aspsnippets.com/Articles/Export-DataSet-or-DataTable-to-Word-Excel-PDF-and-CSV-Formats.aspx
            //Get the data from database into datatable
            //string strQuery = "select CustomerID, ContactName, City, PostalCode" +
            //     " from customers";
            //SqlCommand cmd = new SqlCommand(strQuery);
            //DataTable dt = GetData(cmd);
            System.Data.DataTable dt = dmlService.J_ExecSqlReturnDataTable(strSQL);
            //
            StreamWriter StreamWriter = cmnService.J_ReturnStreamWriter(CSVFile);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition",
            //    "attachment;filename=DataTable.csv");
            //Response.Charset = "";
            //Response.ContentType = "application/text";
            StringBuilder sb = new StringBuilder();
            //--
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator
                sb.Append(dt.Columns[k].ColumnName + ',');
            }
            //append new line
            sb.Append("\r\n");
            //--
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    //sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                    if (dt.Rows[i][k].ToString().Contains(",") == true)
                        sb.Append('"' + dt.Rows[i][k].ToString() + '"' + ',');
                    else
                        sb.Append(dt.Rows[i][k].ToString() + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            //Response.Output.Write(sb.ToString());
            cmnService.J_WriteLine(ref StreamWriter, sb.ToString());
            //
            StreamWriter.Flush();
            StreamWriter.Close();
            //Response.Flush();
            //Response.End();
        }


        #endregion

        #endregion

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            grpMainScreen.Visible = false;
            pnlControls.Enabled = true;
            grpButton.Enabled = true;
        }

        private void lblEmailCounter_Click(object sender, EventArgs e)
        {

        }
    }
}

