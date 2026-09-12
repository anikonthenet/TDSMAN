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
using System.Text;
using System.Text.RegularExpressions;

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion


namespace TDSMAN.FormMst
{
    public partial class MstDTAARateChart27Q : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public MstDTAARateChart27Q()
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
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        string strCSVExportSQL = "";
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

        #region MstTdsRateChartFAyear_Load

        private void MstTdsRateChartFAyear_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            try
            {
                //-----------------------------------------------------------
                GC.Collect();
                //
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                //-----------------------------------------------------------
                BtnAdd.Visible = false;               
                BtnDelete.Visible = false;                
                BtnEdit.Visible = false;
                BtnCancel.Visible = false; 
                BtnPrint.Visible = false;
                //BtnRefresh.Visible = false;
                BtnSave.Visible = false;
                BtnSearch.Visible = false;
                BtnSearchCancel.Visible = false;
                BtnSearchOK.Visible = false;
                BtnSort.Visible = false;
                BtnSortOK.Visible = false;
                //
                grpFilter.Visible = true;
                //dgvGrid.Visible = true;
                //------------------------------------------------------------
                //                // FROM DATE
                //                strSQL = "SELECT " + cmnService.J_SQLDBFormat("START_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS START_DATE " +
                //                                "FROM   MST_ASSESSMENT " +
                //                                "WHERE  ASST_ID = (SELECT MAX(ASST_ID) FROM MST_ASSESSMENT)";
                //                mskFromDate.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //                //mskFromDate.Text = J_ReturnServerDate();
                //                //mskToDate.Text = J_ReturnServerDate(); //-- Commented On 11/04/2018 --
                //                //-----------------------------------------------------------
                //                // TO DATE
                //                //----------- Added On 11/04/2018 ---------------------------
                //                strSQL = "SELECT " + cmnService.J_SQLDBFormat("END_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS END_DATE " +
                //                         "FROM   MST_ASSESSMENT " +
                //                         "WHERE  ASST_ID = (SELECT MAX(ASST_ID) FROM MST_ASSESSMENT)";
                //                mskToDate.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));                
                //                //
                //                if (dtService.J_ConvertToIntYYYYMMDD(mskToDate.Text) < dtService.J_ConvertToIntYYYYMMDD(mskFromDate.Text))
                //                    mskToDate.Text = mskFromDate.Text;
                //                //-----------------------------------------------------------
                //                strSQL = @" SELECT FA_YEAR
                //                            FROM MST_ASSESSMENT
                //                            WHERE   START_DATE <= " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " " + @"
                //                            AND END_DATE >= " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " ";
                //                lblFAYear.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //                //-----------------------------------------------------------
                //BtnDelete.Enabled = false;
                //BtnDelete.BackColor = Color.LightGray;

                //-----------------------------------------------------------
                //-----------
                //-- SECTION
                //-----------
                //strSQL = " SELECT REMITTANCE_ID," +
                //    "             REMITTANCE_CODE + ' - ' + REMITTANCE_DESC AS REMITTANCE  " +
                //    "      FROM   MST_REMITTANCE " +
                //    "      WHERE  DTAA_FLAG > 0 " +
                //    "      ORDER BY REMITTANCE_CODE";                    
                //if (dmlService.J_PopulateComboBox(strSQL, ref cmbRemittance) == false) return;
                //-----------
                //-- FA YEAR
                //-----------
                strSQL = " SELECT ASST_ID," +
                         "        FA_YEAR  " +
                         " FROM   MST_ASSESSMENT " +
                         " WHERE  VISIBILITY_FLAG = 0 " +
                         " AND    ASST_ID > 4 " +
                         " ORDER BY ASST_ID DESC ";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbFAYear,1) == false) return;
                //--
                cmbFAYear.SelectedIndex = 1;
                //-----------------------------------------------------------
                ControlVisible(false);
                ClearControls();
                //-----------------------------------------------------------
                lblTitle.Text = "DTAA Rate Chart";
                //-----------------------------------------------------------
                ShowDataInGridBlank();

                //dgvGrid.Visible = true;
                //-----------------------------------------------------------
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }

        #endregion


        #region cmbSection_KeyPress
        private void cmbSection_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbSection_Leave
        private void cmbSection_Leave(object sender, EventArgs e)
        {
            //ShowDataInGrid();
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

        #region lnkClick_LinkClicked
        private void lnkClick_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.tdsman.com/tds-rate-chart.asp");  
        }
        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
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
           
        }
        #endregion       

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                //----------------------------------------------------------
                //-- SECTION
                //----------------------------------------------------------
                if (cmbRemittance.SelectedIndex == 0)
                {
                    cmnService.J_UserMessage("Please select Remittance");
                    cmbRemittance.Select();
                    return false;
                }
                //
                if (cmbFAYear.SelectedIndex == 0)
                {
                    cmnService.J_UserMessage("Please select FA Year");
                    cmbFAYear.Select();
                    return false;
                }

                return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion            
        
        #region J_ReturnServerDate
        public string J_ReturnServerDate()
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strSQL = "SELECT FORMAT(DATE(),'dd/MM/yyyy')";
            else
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            //--
            return cmnService.J_NullToText(dmlService.J_ExecSqlReturnScalar(strSQL));
        }
        #endregion

        #region ShowDataInGridBlank
        private void ShowDataInGridBlank()
        {
            try
            {
                string strQuery1 = string.Empty;
                strCSVExportSQL = string.Empty;
                string[,] strMatrix = {{"PAR_DTAA_RATE_ID", "0", "", "", "", "F", ""},
                                    {"COUNTRY_ID", "0", "", "", "", "F", ""},
                                    {"Country Code", "300", "", "", "", "", "T"},
                                    {"Country", "300", "", "", "", "", "T"},
                                    {"From Date", "300", "", "", "", "", "T"},
                                    {"To Date", "300", "", "", "", "", "T"},
                                    {"DTAA Rate", "300", "", "", "", "", "T"}};
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //strOrderBy = "NONSALARY_TAX_SLAB_ID";
                strSQL = @" SELECT PAR_DTAA_RATE_ID,
                                       MST_COUNTRY.COUNTRY_ID, 
	                                   MST_COUNTRY.COUNTRY_CODE, 
	                                   MST_COUNTRY.COUNTRY_DESC, 
	                                   FROM_DATE, 
	                                   TO_DATE, 
	                                   DTAA_RATE
                              FROM   PAR_DTAA_RATE, MST_COUNTRY
                              WHERE  PAR_DTAA_RATE.COUNTRY_ID =  MST_COUNTRY.COUNTRY_ID 
                              AND    1=2";
                //
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);  //Show Data into the Grid
                if (dsetGridClone == null) return;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region ShowDataInGrid
        private void ShowDataInGrid()
        {
            try
            {
                string strQuery1 = string.Empty;
                strCSVExportSQL = string.Empty;
                string[,] strMatrix = {{"PAR_DTAA_RATE_ID", "0", "", "", "", "F", ""},
                                    {"COUNTRY_ID", "0", "", "", "", "F", ""},
                                    {"Country Code", "300", "", "", "", "", "T"},
                                    {"Country", "300", "", "", "", "", "T"},
                                    {"From Date", "300", "", "", "", "", "T"},
                                    {"To Date", "300", "", "", "", "", "T"},
                                    {"DTAA Rate", "300", "", "", "", "", "T"}};
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //strOrderBy = "NONSALARY_TAX_SLAB_ID";
                strSQL = @" SELECT PAR_DTAA_RATE_ID,
                                       MST_COUNTRY.COUNTRY_ID, 
	                                   MST_COUNTRY.COUNTRY_CODE, 
	                                   MST_COUNTRY.COUNTRY_DESC, 
	                                   FROM_DATE, 
	                                   TO_DATE, 
	                                   DTAA_RATE
                              FROM   PAR_DTAA_RATE, MST_COUNTRY, MST_ASSESSMENT
                              WHERE  PAR_DTAA_RATE.COUNTRY_ID =  MST_COUNTRY.COUNTRY_ID ";
                if (cmbRemittance.SelectedIndex > 0)
                {
                    strSQL += " AND PAR_DTAA_RATE.REMITTANCE_ID = " + Convert.ToInt32(Support.GetItemData(cmbRemittance, cmbRemittance.SelectedIndex)) + " ";
                }
                //--
                if (cmbFAYear.SelectedIndex > 0)
                {
                    strSQL += " AND MST_ASSESSMENT.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex)) + " ";
                }
                strSQL += @" AND PAR_DTAA_RATE.FROM_DATE <= MST_ASSESSMENT.END_DATE
                               AND PAR_DTAA_RATE.TO_DATE   >= MST_ASSESSMENT.START_DATE "; 
                //-----------------------------------------------------------
                strSQL = strSQL + "ORDER BY MST_COUNTRY.COUNTRY_DESC";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);  //Show Data into the Grid
                strCSVExportSQL = strSQL;
                if (dsetGridClone == null) return;
                //--
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region btnGo_Click
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (ValidateFields() == true)
            {
                ShowDataInGrid();
            }
        }


        #endregion

        #endregion

        private void cmbFAYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(cmbFAYear.SelectedIndex <=0)
            if (cmbFAYear.SelectedIndex >= 0)
            {
                strSQL = " SELECT REMITTANCE_ID," +
                    "             REMITTANCE_CODE + ' - ' + REMITTANCE_DESC AS REMITTANCE  " +
                    "      FROM   MST_REMITTANCE " +
                    @"      WHERE  DTAA_FLAG > 0 
                           AND    MST_REMITTANCE.INACTIVE_FLAG = 0
                           AND MST_REMITTANCE.ASST_ID <= " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + @"
                           AND(MST_REMITTANCE.VALID_UPTO_ASST_ID >= " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + @" OR MST_REMITTANCE.VALID_UPTO_ASST_ID = 0)
                           ORDER BY REMITTANCE_CODE";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbRemittance) == false) return;
            }
            //
            ShowDataInGridBlank();
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbRemittance.SelectedIndex <= 0)
                ShowDataInGridBlank();
        }

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0105", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        #region BtnRefresh_Click
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //
            if(dgvGrid.RowCount <= 0)
            {
                cmnService.J_UserMessage("No Records to Show !!");
                return;
            }
            //
            string strCSVPath = "", strfolderName = "DTAA_RATES_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"), strCSVFileName = cmbRemittance.Text.Replace("/","_") + "_" + cmbFAYear.Text + ".csv";            
            // Create a new instance of FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
            // A new folder button will display in FolderBrowserDialog.
            folderBrowserDlg.ShowNewFolderButton = true;
            //Show FolderBrowserDialog
            DialogResult dlgResult = folderBrowserDlg.ShowDialog();
            if (dlgResult.Equals(DialogResult.OK))
            {
                //Show selected folder path in textbox1.
                strCSVPath = folderBrowserDlg.SelectedPath;
                //Browsing start from root folder.
                Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
            }

            string fullPath = Path.Combine(strCSVPath, strfolderName);
            // Create the directory
            Directory.CreateDirectory(fullPath);
            //
            ExportToCSV(strCSVExportSQL, Path.Combine(fullPath, strCSVFileName));
            //
            cmnService.J_UserMessage("Export completed");
            //
            System.Diagnostics.Process.Start(Path.Combine(fullPath, strCSVFileName));
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
    }
}

