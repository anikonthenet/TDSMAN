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

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;
using Excel = Microsoft.Office.Interop.Excel;

#endregion


namespace TDSMAN.FormMst
{
    public partial class MstCorrReceiptNo : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public MstCorrReceiptNo()
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
        string strImagePath = "";
        string strDatabasePath = "";
        //-----------------------------------------------------------------------
        string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;

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

        #region MstReceiptNo_Load

        private void MstReceiptNo_Load(object sender, EventArgs e)
        {
            try
            {
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //
                GC.Collect();
                //
                //
                TdsMan.GetDatabasePathExists(out strDatabasePath);
                //
                //-----------------------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                BtnAdd.Enabled = false;
                BtnAdd.BackColor = Color.LightGray;
                BtnDelete.Enabled = false;
                BtnDelete.BackColor = Color.LightGray;
                ViewGrid.Height= 553;
                //-----------------------------------------------------------
                //DisableControls();
                //-----------------------------------------------------------
                ControlVisible(false);
                ClearControls();
                //-----------------------------------------------------------
                //-- set the Help Grid Column Header Text & behavior
                //-- (0) Header Text
                //-- (1) Width
                //-- (2) Format
                //-- (3) Alignment
                //-- (4) NullToText
                //-- (5) Visible
                //-- (6) AutoSizeMode
                //
                //DATALENGTH; LEN
                string strLength = "";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strLength = "DATALENGTH";
                else  if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strLength = "LEN";
                //
                string[,] strImageMatrix = {{strLength + "(COR_HDR_BATCH.RECEIPT_IMAGE_PATH) > 0", "F", "Yes", "T"},
                                        {strLength + "(COR_HDR_BATCH.RECEIPT_IMAGE_PATH) = 0", "F", "", "T"}};
                //-----------------------------------------------------------
                string[,] strMatrix1 = {{"BasicInfoID", "0", "", "Right", "", "F", ""},
							            {"Financial Year", "200", "", "", "", "", "T"},
							            {"Quarter", "100", "", "", "", "", "T"},
							            {"Company Name", "400", "", "", "", "", "T"},
							            {"Form No.", "200", "", "", "", "", "T"},
                                        {"Receipt No.", "200", "", "", "", "", "T"},
                                        {"Image Available", "150", "", "Left", "", "", "T"}};
                //-----------------------------------------------------------
                strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //strOrderBy = "MST_ASSESSMENT.FA_YEAR, COR_HDR_BATCH.QTR, COR_TRN_COMPANY.COMPANY_NAME, COR_HDR_BATCH.FORM_NO";
                strOrderBy = "COR_HDR_BATCH.BATCH_HEADER_ID DESC";
                strQuery = "SELECT COR_HDR_BATCH.BATCH_HEADER_ID AS BATCH_HEADER_ID," +
                    "              MST_ASSESSMENT.FA_YEAR        AS FA_YEAR," +
                    "              COR_HDR_BATCH.QTR             AS QTR," +
                    "              COR_TRN_COMPANY.COMPANY_NAME  AS COMPANY_NAME," +
                    "              COR_HDR_BATCH.FORM_NO         AS FORM_NO," +
                    "              COR_HDR_BATCH.RECEIPT_NO      AS RECEIPT_NO," +
                    "              " + cmnService.J_SQLDBFormat(strImageMatrix, J_SQLColFormat.Case_End) + " AS RECEIPT_IMAGE_PATH " +
                    "       FROM   COR_HDR_BATCH," + 
                    "              COR_TRN_COMPANY," +
                    "              MST_ASSESSMENT " +
                    "       WHERE  COR_HDR_BATCH.BATCH_HEADER_ID = COR_TRN_COMPANY.BATCH_HEADER_ID " +
                    "       AND    COR_HDR_BATCH.ASST_ID         = MST_ASSESSMENT.ASST_ID ";
                //-----------------------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                //-----------------------------------------------------------
                lblTitle.Text = "Correction Receipt No. Master";
                //-----------------------------------------------------------
                //ViewGrid_Click(sender, e);
                //-- 2021/12/04
                if (TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard == true)
                {
                    BtnEdit_Click(sender, e);
                }
                //--
                //-----------------------------------------------------------
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }

        #endregion

        #region BtnAdd_Click

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                BtnAdd.Enabled = false;
                BtnAdd.BackColor = Color.LightGray;
                lblSearchMode.Text = J_Mode.General;
                //---------------------------------------------
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                //---------------------------------------------
                strCheckFields = "";
                //cmbFormNo.Select();
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
                if (ViewGrid.CurrentRowIndex >= 0)
                {
                    //--------------------------------------------------
                    ControlVisible(true);
                    ClearControls();
                    //--------------------------------------------------
                    long lngID = 0;
                    if (TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard == true)
                        lngID = TDSMAN.Classes.TDSMAN.T_pBatchId;
                    else
                        lngID = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
                    //A particular ID wise retriving the data from database
                    if (ShowRecord(lngID) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "BATCH_HEADER_ID", lngSearchId);
                    }
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    BtnAdd.Enabled = false;
                    BtnAdd.BackColor = Color.LightGray;
                    BtnDelete.Enabled = false;
                    BtnDelete.BackColor = Color.LightGray;
                    lblSearchMode.Text = J_Mode.General;
                    //--------------------------------------------------
                    strCheckFields = "";
                    //--------------------------------------------------
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "BATCH_HEADER_ID", lngSearchId);
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
            Insert_Update_Delete_Data();
        }

        #endregion

        #region BtnCancel_Click

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard == true)
                {
                    TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard = false;
                    //
                    GC.Collect();
                    //
                    dmlService.Dispose();
                    this.Close();
                    this.Dispose();
                }
                //-------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
                BtnAdd.Enabled = false;
                BtnAdd.BackColor = Color.LightGray;
                BtnDelete.Enabled = false;
                BtnDelete.BackColor = Color.LightGray;
                //-------------------------------------------
                //DisableControls();
                //-------------------------------------------
                ControlVisible(false);
                ClearControls();					//Clear all the Controls
                //-------------------------------------------
                strSQL = strQuery + "order by " + strOrderBy;
                //-------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-------------------------------------------
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "BATCH_HEADER_ID", lngSearchId) == false)
                    BtnEdit.Select();
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
                //-------------------------------------------
                lblSearchMode.Text = J_Mode.Searching;
                //-------------------------------------------
                if (ValidateFields() == false) return;
                //-------------------------------------------
                grpSort.Visible = false;
                grpSearch.Visible = true;
                //-------------------------------------------
                cmbFinancialYearSearch.Select();
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
                //-- FINANCIAL YEAR
                //-------------------------------------------------------------------
                if (cmbFinancialYearSearch.SelectedIndex > 0)
                    strCheckFields = "AND MST_ASSESSMENT.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYearSearch, cmbFinancialYearSearch.SelectedIndex)) + " ";
                //-------------------------------------------------------------------
                //-- QUARTER
                //-------------------------------------------------------------------
                if (cmbQuarterSearch.SelectedIndex > 0)
                    strCheckFields = strCheckFields + " AND COR_HDR_BATCH.QTR = '" + cmnService.J_ReplaceQuote(cmbQuarterSearch.Text.Trim().ToUpper()) + "' ";
                //-------------------------------------------------------------------
                //-- COMPANY NAME
                //-------------------------------------------------------------------
                if (txtCompanyNameSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + " AND COR_TRN_COMPANY.COMPANY_NAME like '%" + cmnService.J_ReplaceQuote(txtCompanyNameSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- FORM NO
                //-------------------------------------------------------------------
                if (cmbFormNoSearch.SelectedIndex > 0)
                    strCheckFields = strCheckFields + " AND COR_HDR_BATCH.FORM_NO = '" + cmnService.J_ReplaceQuote(cmbFormNoSearch.Text.Trim().ToUpper()) + "' ";
                //-------------------------------------------------------------------
                //-- RECEIPT NO.
                //-------------------------------------------------------------------
                if (txtReceiptNoSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + " AND COR_HDR_BATCH.RECEIPT_NO like '%" + cmnService.J_ReplaceQuote(txtReceiptNoSearch.Text.Trim().ToUpper()) + "%' ";
                //----------------------------------------------------------------------
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                //----------------------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "BATCH_HEADER_ID", lngSearchId) == false)
                {
                    cmbFinancialYearSearch.Select();
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
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "BATCH_HEADER_ID", lngSearchId);
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
            lblMode.Text = J_Mode.Delete;
            Insert_Update_Delete_Data();
        }

        #endregion

        #region BtnRefresh_Click

        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-----------------------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                BtnAdd.Enabled = false;
                BtnAdd.BackColor = Color.LightGray;
                BtnDelete.Enabled = false;
                BtnDelete.BackColor = Color.LightGray;
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
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "BATCH_HEADER_ID", lngSearchId);
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
                BtnEdit.Focus();
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
                //if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);

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


        #region lblLink_LinkClicked
        private void lblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://onlineservices.tin.nsdl.com/TIN/JSP/tds/linktoUnAuthorizedInput.jsp");
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



        #region btnBrowsePath_Click
        private void btnBrowsePath_Click(object sender, EventArgs e)
        {
            strImagePath = cmnService.J_OpenFileDialog("Image/PDF File | *.jpg; *.bmp; *.pdf", "Image/PDF File | *.jpg; *.bmp; *.pdf", "Choose the Receipt File");
            //--
            if (strImagePath != "")
            {
                txtScannedImagePath.Text = strImagePath;
                btnPreviewImage.Enabled = true;
            }
            else
            {
                txtScannedImagePath.Text = "";
                btnPreviewImage.Enabled = false;
            }
        }
        #endregion

        #region btnPreviewImage_Click
        private void btnPreviewImage_Click(object sender, EventArgs e)
        {
            if (txtScannedImagePath.Text.Trim() == "")
            {
                cmnService.J_UserMessage("No path selected");
                return;
            }
            //--
            if (cmnService.J_IsFileExist(txtScannedImagePath.Text) == false)
            {
                cmnService.J_UserMessage("Selected File not found");
                return;
            }
            //--
            //if(Path.GetExtension(txtScannedImagePath.Text.Trim().ToUpper()) == ".PDF")
            //{
            System.Diagnostics.Process.Start(txtScannedImagePath.Text.Trim());
            //}
            //else
            //{
            //    TDSMAN.Classes.TDSMAN.T_ReceiptImageFilePath = txtScannedImagePath.Text;
            //    //--
            //    MstImagePreview MstImagePreview = new MstImagePreview();
            //    MstImagePreview.StartPosition = FormStartPosition.CenterScreen;
            //    MstImagePreview.ShowDialog();
            //    //--
            //}
        }
        #endregion

        #region BtnPrint_Click
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //--
                if (ViewGrid.VisibleRowCount <= 0)
                {
                    cmnService.J_UserMessage("No record exists");
                    BtnAdd.Select();
                    return;
                }
                string strPath = "", strFileName = "";
                // Create a new instance of FolderBrowserDialog.
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                // A new folder button will display in FolderBrowserDialog.
                folderBrowserDlg.ShowNewFolderButton = true;
                //Show FolderBrowserDialog
                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                if (dlgResult.Equals(DialogResult.OK))
                {
                    //Show selected folder path in textbox1.
                    strPath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                else
                    return;
                //--
                strSQL = "SELECT COR_HDR_BATCH.BATCH_HEADER_ID   AS BATCH_HEADER_ID," +
                    "              MST_ASSESSMENT.FA_YEAR        AS FA_YEAR," +
                    "              COR_HDR_BATCH.FORM_NO         AS FORM_NO," +
                    "              COR_HDR_BATCH.QTR             AS QTR," +
                    "              COR_TRN_COMPANY.COMPANY_NAME  AS COMPANY_NAME," +
                    "              COR_TRN_COMPANY.TAN_NO        AS TAN_NO," +
                    "              COR_HDR_BATCH.PRN_NO          AS TOKEN_NO, " +
                    "              " + cmnService.J_SQLDBFormat(cmnService.J_SQLDBFormat("COR_HDR_BATCH.DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY), J_SQLColFormat.ConvertToString) + " AS DATE_OF_FILING," +
                    "              COR_HDR_BATCH.RECEIPT_NO      AS RECEIPT_NO " +
                    "       FROM   COR_HDR_BATCH," +
                    "              COR_TRN_COMPANY," +
                    "              MST_ASSESSMENT " +
                    "       WHERE  COR_HDR_BATCH.BATCH_HEADER_ID = COR_TRN_COMPANY.BATCH_HEADER_ID " +
                    "       AND    COR_HDR_BATCH.ASST_ID         = MST_ASSESSMENT.ASST_ID ";
                //APPLYLING FILTER FOR FINANCIAL YEAR
                if (cmbFinancialYearSearch.SelectedIndex > 0)
                    strSQL += "AND COR_HDR_BATCH.ASST_ID = " + Support.GetItemData(cmbFinancialYearSearch, cmbFinancialYearSearch.SelectedIndex) + " ";

                //APPLYING FILTER FOR COMPANY
                if (txtCompanyNameSearch.Text.Trim() != "")
                    strSQL += "AND COR_TRN_COMPANY.COMPANY_NAME LIKE '%" + txtCompanyNameSearch.Text.Trim() + "%' ";

                //APPLYING FILTER FOR FORM NO
                if (cmbFormNoSearch.SelectedIndex > 0)
                    strSQL += "AND COR_HDR_BATCH.FORM_NO = '" + cmbFormNoSearch.Text + "' ";

                //APPLYING FILTER FOR QUARTER
                if (cmbQuarterSearch.SelectedIndex > 0)
                    strSQL += "AND COR_HDR_BATCH.QTR = '" + cmbQuarterSearch.Text + "' ";
                //-----------------------------------------------------------
                strSQL = strSQL + " ORDER BY MST_ASSESSMENT.FA_YEAR, COR_HDR_BATCH.QTR, COR_TRN_COMPANY.COMPANY_NAME, COR_HDR_BATCH.FORM_NO";
                //--
                strFileName = "XL_CORR_RECEIPT_NO_" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".XLSX";
                //--
                this.Cursor = Cursors.WaitCursor;
                //--
                if (CREATE_EXCEL_FILE(Path.Combine(strPath, strFileName)) == false)
                {
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("Some error occurred");
                    return;
                }
                else
                {
                    // EMPLOYEE DETAILS
                    if (CREATE_NEW_WORKSHEET(Path.Combine(strPath, strFileName), "RECEIPT NO. LIST") == false) return;
                    //
                    if (WRITE_RECEIPT_NO_REG_WORKSHEET(Path.Combine(strPath, strFileName), "RECEIPT NO. LIST", strSQL) == false) return;
                }
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Data Exported");
                System.Diagnostics.Process.Start(Path.Combine(strPath, strFileName));
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(ERR.Message);
            }
        }
        #endregion

        #endregion

        #region User Define Functions

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
            txtFinancialYear.Text = "";
            txtQuarter.Text = "";
            txtCompanyName.Text = "";
            txtFormNo.Text = "";
            txtReceiptNo.Text = "";
            mskDateOfFiling.Text = "";
            txtPRNNo.Text = "";
            //--------------------//-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYearSearch) == false) return;                
            //--
            string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQuarterSearch);
            //--
            txtCompanyNameSearch.Text = "";
            //--
            string[] strFormNo ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNoSearch);
            //--
            txtReceiptNoSearch.Text = "";
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            try
            {
                strSQL ="SELECT COR_HDR_BATCH.BATCH_HEADER_ID     AS BATCH_HEADER_ID," +
                    "              MST_ASSESSMENT.FA_YEAR        AS FA_YEAR," +
                    "              COR_HDR_BATCH.QTR            AS QTR," +
                    "              COR_TRN_COMPANY.COMPANY_NAME      AS COMPANY_NAME," +
                    "              COR_HDR_BATCH.FORM_NO        AS FORM_NO," +
                    "              COR_HDR_BATCH.RECEIPT_NO     AS RECEIPT_NO," +
                    "            " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DATE_OF_FILING," +
                    "              COR_HDR_BATCH.PRN_NO         AS PRN_NO," +
                    "              COR_HDR_BATCH.RECEIPT_IMAGE_PATH AS RECEIPT_IMAGE_PATH " +
                    "       FROM   COR_HDR_BATCH," + 
                    "              COR_TRN_COMPANY," +
                    "              MST_ASSESSMENT " +
                    "       WHERE  COR_HDR_BATCH.BATCH_HEADER_ID = COR_TRN_COMPANY.BATCH_HEADER_ID " +
                    "       AND    COR_HDR_BATCH.ASST_ID         = MST_ASSESSMENT.ASST_ID " +
                    "       AND    COR_HDR_BATCH.BATCH_HEADER_ID = " + Id + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;

                    txtFinancialYear.Text = Convert.ToString(drdShowRecord["FA_YEAR"]);
                    txtQuarter.Text = Convert.ToString(drdShowRecord["QTR"]);
                    txtCompanyName.Text = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                    txtFormNo.Text = Convert.ToString(drdShowRecord["FORM_NO"]);
                    txtReceiptNo.Text = Convert.ToString(drdShowRecord["RECEIPT_NO"]);
                    mskDateOfFiling.Text = Convert.ToString(drdShowRecord["DATE_OF_FILING"]);
                    txtPRNNo.Text = Convert.ToString(drdShowRecord["PRN_NO"]);
                    //--
                    txtScannedImagePath.Text = Convert.ToString(drdShowRecord["RECEIPT_IMAGE_PATH"]);
                    txtScannedImagePathM.Text = Convert.ToString(drdShowRecord["RECEIPT_IMAGE_PATH"]);
                    if (txtScannedImagePath.Text.Trim() == "")
                        btnPreviewImage.Enabled = false;
                    else
                        btnPreviewImage.Enabled = true;
                    //
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        lblComment.Text = "The Receipt file saved in the installed folder with name <" + TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder + ">.\nFolder Path :- " + strDatabasePath + "\\" + TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder;
                    else
                        lblComment.Text = "The Receipt file saved in the server with folder name <" + TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder + ">.\nFolder Path :- " + strDatabasePath + "\\" + TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder;
                    //--
                    txtReceiptNo.Select();

                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    return true;
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                lngSearchId = 0;
                //-----------------------------------------------------------
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
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
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "DEDUCTEE_ID", lngSearchId);
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
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "BATCH_HEADER_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (cmbFinancialYearSearch.SelectedIndex <= 0 &&
                            cmbQuarterSearch.SelectedIndex <= 0 &&
                            txtCompanyNameSearch.Text.Trim() == "" &&
                            cmbFormNoSearch.SelectedIndex <= 0 &&
                            txtReceiptNoSearch.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            cmbFinancialYearSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    //-----------------------------------------------------------------------
                    //-- FINANCIAL YEAR
                    //-----------------------------------------------------------------------
                    if (txtFinancialYear.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Financial Year - Cannot be Blank");
                        txtFinancialYear.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- QUARTER
                    //-----------------------------------------------------------------------
                    if (txtQuarter.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Quarter - Cannot be Blank");
                        txtQuarter.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- COMPANY NAME
                    //-----------------------------------------------------------------------
                    if (txtCompanyName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Company Name - Cannot be Blank");
                        txtCompanyName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- FORM NO
                    //-----------------------------------------------------------------------
                    if (txtFormNo.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Form No. - Cannot be Blank");
                        txtFormNo.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- RECEIPT NO
                    //-----------------------------------------------------------------------
                    //if (txtReceiptNo.Text.Trim() == "")
                    //{
                    //    cmnService.J_UserMessage("Receipt No. - Cannot be Blank");
                    //    txtReceiptNo.Select();
                    //    return false;
                    //}
                    //----------------------------------------------------------
                    //-- VALID DATE CHECK
                    //----------------------------------------------------------
                    if (dtService.J_IsBlankDateCheck(ref mskDateOfFiling, J_ShowMessage.NO) == false)
                    {
                        if (dtService.J_IsDateValid(mskDateOfFiling) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the Date of Filing");
                            mskDateOfFiling.Select();
                            return false;
                        }
                    }
                    else
                    {
                        cmnService.J_UserMessage("Date of Filing - Cannot be Blank");
                        mskDateOfFiling.Select();
                        return false;
                    }
                    //-- 
                    if (txtPRNNo.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Token No. - Cannot be Blank");
                        txtPRNNo.Select();
                        return false;
                    }
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
            try
            {
                //--------------------------------------------
                int intUpdateFlag = 0;
                //
                if (txtReceiptNo.Text.Trim() == "")
                    intUpdateFlag = 0;
                else
                    intUpdateFlag = 1;
                //--------------------------------------------
                string strDateOfFiling = "";
                //
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        //*****  For Insert
                        break;
                    case J_Mode.Edit:
                        //*****  For Modify
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;
                        //-----------------------------------------------------------
                        if (dtService.J_IsBlankDateCheck(ref mskDateOfFiling, J_ShowMessage.NO) == true)
                        {
                            strDateOfFiling = "NULL";
                        }
                        else
                        {
                            strDateOfFiling = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDateOfFiling) + cmnService.J_DateOperator();
                        }

                        //-- COPY/DELETE THE IMAGE FILE
                        if (txtScannedImagePath.Text.Trim() == "")
                        {
                            if (txtScannedImagePathM.Text.Trim() != "")
                            {
                                if (System.IO.File.Exists(txtScannedImagePathM.Text.Trim()) == true)
                                    File.Delete(txtScannedImagePathM.Text.Trim());
                            }
                        }
                        //--
                        if (txtScannedImagePath.Text.Trim() != txtScannedImagePathM.Text.Trim() && txtScannedImagePath.Text.Trim() != "")
                        {
                            if (System.IO.Directory.Exists(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder)) == false)
                                System.IO.Directory.CreateDirectory(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder));
                            //
                            if (System.IO.File.Exists(Path.Combine(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder), "CORR" + lngSearchId + Path.GetExtension(txtScannedImagePath.Text.Trim()))) == true)
                            {
                                if (System.IO.File.Exists(txtScannedImagePathM.Text.Trim()) == true)
                                    File.Delete(txtScannedImagePathM.Text.Trim());
                                //
                                File.Copy(txtScannedImagePath.Text.Trim(), Path.Combine(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder), "CORR" + lngSearchId + Path.GetExtension(txtScannedImagePath.Text.Trim())));
                            }
                            else
                            {
                                File.Copy(txtScannedImagePath.Text.Trim(), Path.Combine(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder), "CORR" + lngSearchId + Path.GetExtension(txtScannedImagePath.Text.Trim())));
                            }
                            //
                            txtScannedImagePath.Text = Path.Combine(Path.Combine(strDatabasePath, TDSMAN.Classes.TDSMAN.T_ReceiptImageFolder), "CORR" + lngSearchId + Path.GetExtension(txtScannedImagePath.Text.Trim()));
                        }
                        //--
                        //if (cmnService.J_SaveConfirmationMessage(ref cmbFormNo) == true) return;
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        strSQL = "UPDATE COR_HDR_BATCH " +
                                 "SET    RECEIPT_NO         ='" + cmnService.J_ReplaceQuote(txtReceiptNo.Text.Trim()) + "'," +
                                 "       DATE_OF_FILING     = " + strDateOfFiling + "," +
                                 "       PRN_NO             ='" + cmnService.J_ReplaceQuote(txtPRNNo.Text.Trim()) + "'," +
                                 "       RECEIPT_IMAGE_PATH ='" + cmnService.J_ReplaceQuote(txtScannedImagePath.Text.Trim()) + "' " +
                                 "WHERE  BATCH_HEADER_ID    =  " + lngSearchId + "";
                        //----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            txtReceiptNo.Select();
                            return;
                        }
                        //-----------------------------------------------------------
                        //strSQL = "UPDATE TRN_COMPANY_INFO " +
                        //         "SET    UPDATE_FLAG   = " + intUpdateFlag + " " +
                        //         "WHERE  BATCH_HEADER_ID = " + lngSearchId + "";
                        ////----------------------------------------------------------
                        //if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        //{
                        //    txtReceiptNo.Select();
                        //    return;
                        //}
                        //-----------------------------------------------------------
                        //-- UPDATE THE TRANSACTION
                        //..........................................................
                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.EditModeSave);
                        //-----------------------------------------------------------
                        ClearControls();
                        //-----------------------------------------------------------
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        //-----------------------------------------------------------
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        BtnAdd.Enabled = false;
                        BtnAdd.BackColor = Color.LightGray;
                        BtnDelete.Enabled = false;
                        BtnDelete.BackColor = Color.LightGray;
                        //-----------------------------------------------------------
                        if (TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard == true)
                        {
                            TDSMAN.Classes.TDSMAN.T_TMOLikeDashBoard = false;
                            //
                            GC.Collect();
                            //
                            dmlService.Dispose();
                            this.Close();
                            this.Dispose();
                        }
                        //DisableControls();
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "BATCH_HEADER_ID", lngSearchId);
                        break;
                    case J_Mode.Delete:
                        break;
                }
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion


        #region CREATE EXCEL FILE
        // SOURCE PATH : http://csharp.net-informations.com/excel/csharp-create-excel.htm
        private bool CREATE_EXCEL_FILE(string ExcelFilePath)
        {
            try
            {
                //MessageBox.Show("5");
                //--
                //if (rbnCSVOption.Checked == true)
                //{
                //    if (Directory.Exists(ExcelFilePath) == false)
                //        Directory.CreateDirectory(ExcelFilePath);
                //    return true; //-- 2019/01/22
                //}
                //MessageBox.Show("5.0.1.1");
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                //Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                //--
                //MessageBox.Show("5.0.1.2");
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
                //MessageBox.Show("5.0.1.3");
                xlWorkBook.Close(true, misValue, misValue);
                //MessageBox.Show("5.0.1.4");
                xlApp.Quit();
                //MessageBox.Show("5.0.1.5");

                //ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                //MessageBox.Show("5.0.1.6");
                ReleaseObject(xlApp);
                //MessageBox.Show("5.0.1.7");
                //--
                return true;
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
                //cmnService.J_UserMessage(ERR.Message);
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
                //
                //if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
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

        #region WRITE RECEIPT NO. WORKSHEET
        private bool WRITE_RECEIPT_NO_REG_WORKSHEET(string ExcelFilePath, string SheetName, string SQL)
        {
            try
            {
                //if (rbnCSVOption.Checked == true) return true; //-- 2019/01/22
                //--
                IDataReader drdGetSheetRecord = null;
                //--
                //if (KILL_EXCEL() == false)
                //    return false;
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
                wsnew.get_Range("A1", m).Value2 = "Tax Year";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 20;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "Form No.";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 20;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("C1", m).Value2 = "Quarter";
                wsnew.get_Range("C1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C1", m).Borders.Value = true;
                wsnew.get_Range("C1", m).ColumnWidth = 20;
                wsnew.get_Range("C1", m).WrapText = true;
                wsnew.get_Range("C1", m).Font.Name = "Arial";
                wsnew.get_Range("C1", m).Font.Bold = true;
                wsnew.get_Range("C1", m).Font.Size = 10;
                wsnew.get_Range("C1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("C1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("D1", m).Value2 = "Company Name";
                wsnew.get_Range("D1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("D1", m).Borders.Value = true;
                wsnew.get_Range("D1", m).ColumnWidth = 50;
                wsnew.get_Range("D1", m).WrapText = true;
                wsnew.get_Range("D1", m).Font.Name = "Arial";
                wsnew.get_Range("D1", m).Font.Bold = true;
                wsnew.get_Range("D1", m).Font.Size = 10;
                wsnew.get_Range("D1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("D1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("E1", m).Value2 = "TAN";
                wsnew.get_Range("E1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("E1", m).Borders.Value = true;
                wsnew.get_Range("E1", m).ColumnWidth = 20;
                wsnew.get_Range("E1", m).WrapText = true;
                wsnew.get_Range("E1", m).Font.Name = "Arial";
                wsnew.get_Range("E1", m).Font.Bold = true;
                wsnew.get_Range("E1", m).Font.Size = 10;
                wsnew.get_Range("E1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("E1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("F1", m).Value2 = "Receipt No.";
                wsnew.get_Range("F1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("F1", m).Borders.Value = true;
                wsnew.get_Range("F1", m).ColumnWidth = 65;
                wsnew.get_Range("F1", m).WrapText = true;
                wsnew.get_Range("F1", m).Font.Name = "Arial";
                wsnew.get_Range("F1", m).Font.Bold = true;
                wsnew.get_Range("F1", m).Font.Size = 10;
                wsnew.get_Range("F1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("F1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("G1", m).Value2 = "Date of Filing";
                wsnew.get_Range("G1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("G1", m).Borders.Value = true;
                wsnew.get_Range("G1", m).ColumnWidth = 40;
                wsnew.get_Range("G1", m).WrapText = true;
                wsnew.get_Range("G1", m).Font.Name = "Arial";
                wsnew.get_Range("G1", m).Font.Bold = true;
                wsnew.get_Range("G1", m).Font.Size = 10;
                wsnew.get_Range("G1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("G1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("H1", m).Value2 = "Token No.";
                wsnew.get_Range("H1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("H1", m).Borders.Value = true;
                wsnew.get_Range("H1", m).ColumnWidth = 65;
                wsnew.get_Range("H1", m).WrapText = true;
                wsnew.get_Range("H1", m).Font.Name = "Arial";
                wsnew.get_Range("H1", m).Font.Bold = true;
                wsnew.get_Range("H1", m).Font.Size = 10;
                wsnew.get_Range("H1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("H1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                //strSQL = "SELECT TRN_BASIC_INFO.BASIC_INFO_ID," +
                //    "            " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.RECEIPT_NO", J_SQLColFormat.ConvertToString) + "        AS RECEIPT_NO," +
                //    "            " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DATE_OF_FILING," +
                //    "            " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.PRN_NO", J_SQLColFormat.ConvertToString) + "            AS TOKEN_NO " +
                //    "     FROM   TRN_BASIC_INFO " +
                //    "     WHERE  TRN_BASIC_INFO.BASIC_INFO_ID = " + BasicInfo + " ";
                //if (FormNo == T_FormNo.F24Q)
                //    strSQL = strSQL + "AND SECTION_NO <> '' ";
                //strSQL = strSQL + "ORDER BY SECTION_ID";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(SQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {

                    //
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["FA_YEAR"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["FORM_NO"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("C" + lngSheetRow, m).Value2 = drdGetSheetRecord["QTR"].ToString();
                    wsnew.get_Range("C" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("C" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("D" + lngSheetRow, m).Value2 = drdGetSheetRecord["COMPANY_NAME"].ToString();
                    wsnew.get_Range("D" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("D" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("E" + lngSheetRow, m).Value2 = drdGetSheetRecord["TAN_NO"].ToString();
                    wsnew.get_Range("E" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("E" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("F" + lngSheetRow, m).Value2 = "'" + drdGetSheetRecord["RECEIPT_NO"].ToString();
                    //wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("F" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("F" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("G" + lngSheetRow, m).Value2 = drdGetSheetRecord["DATE_OF_FILING"].ToString();
                    wsnew.get_Range("G" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("G" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("G" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("G" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("H" + lngSheetRow, m).Value2 = "'" + drdGetSheetRecord["TOKEN_NO"].ToString();
                    wsnew.get_Range("H" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("H" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("H" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("H" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //

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

        #endregion

        private void pctManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0131", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}

