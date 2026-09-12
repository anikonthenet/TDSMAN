#region Refered Namespaces & Classes

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Diagnostics;

using System.Runtime.InteropServices;




//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnViewUpdates : Form
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnViewUpdates()
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

        TracesConnect objTracesConnect = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;
        string strPAN = "";
        long lngBasicInfoID = 0;
        //--            
        ToolTip tllTip = new ToolTip();
        string strEmpDed = "";
        string strbtnVerification = "&Start verifying";
        bool blnStatus = false;
        bool blnVerificationComplete = false;
        long lngDeducteeID = 0;
        bool blRegular = true;
        //
        int intPANId = 0;
        int intStatusId = 0;
        int intVerifyId = 0;
        //
        int j = 0;
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

        #region TrnViewUpdates_Load
        private void TrnViewUpdates_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //----
            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------
                strSQL = " SELECT NEW_UPDATE_ID    AS NEW_UPDATE_ID," +
                    //"             CSTR(VERSION_NO) AS VERSION_NO," +
                    " " + cmnService.J_SQLDBFormat("VERSION_NO", J_SQLColFormat.ConvertToString) + " AS VERSION_NO," +
                    "             UPDT_DATE        AS UPDT_DATE, " +
                    "             RELEASE_DATE     AS RELEASE_DATE " +
                    "      FROM   MST_NEW_UPDATE " +
                    "      ORDER BY VERSION_NO DESC";            
                //-----------
                string[,] strMatrix = {{"NEW_UPDATE_ID", "0", "", "", "", "F", ""},
                                        {"Version No.", "390", "", "", "", "", "T"},
                                        {"Update Date", "380", "", "", "", "", "T"},
                                        {"Release Date", "380", "", "", "", "", "T"}};
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvUpdates, strSQL, strMatrix);
                dgvUpdates.ClearSelection();
                //--
                #region CellFormatting(SHOW_BUTTON)

                //if (dgvUpdates.Columns[e.ColumnIndex].DataPropertyName == "SHOW_BUTTON")
                //{
                DataGridViewButtonColumn BtnShow = new DataGridViewButtonColumn();
                dgvUpdates.Columns.Add(BtnShow);
                //dgvUpdates.Columns
                BtnShow.Visible = true;
                BtnShow.HeaderText = "";
                BtnShow.Text = "Show";
                BtnShow.Name = "BtnShow";
                BtnShow.UseColumnTextForButtonValue = true;
                //}
                #endregion
                //-
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region btnShow_Click
        private void btnShow_Click(object sender, EventArgs e)
        {
            //if (cmbVersionText.SelectedIndex <= 0) { cmnService.J_UserMessage("Version - not selected"); return; }
            //--
            //string strTextValue= Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT UPDATE_TEXT_HTML FROM MST_NEW_UPDATE WHERE NEW_UPDATE_ID = " + Convert.ToInt32(Support.GetItemData(cmbVersionText, cmbVersionText.SelectedIndex)))) ;
            //--
            //if (strTextValue == "") return;
            ////--
            //WebBrowser WB = new WebBrowser();
            //string filename = string.Format(@"{0}\{1}",
            //        System.IO.Path.GetTempPath(),
            //        "UpdateList.htm");
            ////--
            //File.WriteAllText(filename, strTextValue);
            //Process.Start(filename);
            //--
        }
        #endregion


        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            blnVerificationComplete = false; 
            //--
            this.Dispose();
            this.Close();
        }
        #endregion

        #region dgvUpdates_CellClick
        private void dgvUpdates_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvUpdates.Columns[4].HeaderText == "BtnShow")
            //{
            //    cmnService.J_UserMessage("1");
            //}
            if (e.ColumnIndex.ToString() == "0")
            {
                //dgvDeductees.Rows[i].Cells[intVerifyId].Value)
                //cmnService.J_UserMessage(dgvUpdates.Row [dgvUpdates.CurrentRow].Cells[0].Value);
                strSQL = "SELECT UPDATE_TEXT_HTML FROM MST_NEW_UPDATE WHERE NEW_UPDATE_ID = " + Convert.ToString(dgvUpdates.Rows[dgvUpdates.CurrentRow.Index].Cells[1].Value);
                string strHTMLText = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //--
                string filename = string.Format(@"{0}\{1}",
                        System.IO.Path.GetTempPath(),
                        "UpdateList.htm");
                //--                
                File.WriteAllText(filename, strHTMLText);
                Process.Start(filename);
                //--
            }
            //cmnService.J_UserMessage(e.ColumnIndex.ToString());
        }
        #endregion

        #region dgvUpdates_ColumnAdded
        private void dgvUpdates_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgvUpdates.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=OakWx-a1DgM");
        }

        #endregion

        #endregion

        #region User Define Functions



        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0100", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }

}