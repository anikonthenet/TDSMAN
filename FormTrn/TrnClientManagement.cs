using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.IO;

using TDSMAN.Classes;


namespace TDSMAN.FormTrn
{
    public partial class TrnClientManagement : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnClientManagement()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            ////--
        }
        #endregion

        #region Objects & Variables decleration
        CommonService cmnService = new CommonService();
        DMLService dmlService = new DMLService();
        TDSMAN.Classes.TDSMAN Tdsman = new TDSMAN.Classes.TDSMAN();
        String strSQL = "";
        //
        ToolTip ToolTip = new ToolTip();

        #endregion

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

        #region TrnClientManagement_Load
        private void TrnClientManagement_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //--
            lblTitle.Text = "Client Management";
            //
            DataGridViewTextBoxColumn MachineNo = new DataGridViewTextBoxColumn();
            MachineNo.HeaderText = T_CLIENT_MANAGEMENT_GRID.MACHINE_NO;
            MachineNo.Visible = false;
            dsClientsView.Columns.Add(MachineNo);

            DataGridViewTextBoxColumn MachineName = new DataGridViewTextBoxColumn();
            MachineName.HeaderText = T_CLIENT_MANAGEMENT_GRID.MACHINE_NAME;
            MachineName.Visible = true;
            MachineName.ReadOnly = true;
            MachineName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dsClientsView.Columns.Add(MachineName);


            DataGridViewButtonColumn DetachColumn = new DataGridViewButtonColumn();
            DetachColumn.Name = ""; //T_CLIENT_MANAGEMENT_GRID.DETACH;
            DetachColumn.Text = T_CLIENT_MANAGEMENT_GRID.DETACH;
            DetachColumn.UseColumnTextForButtonValue = true;
            dsClientsView.Columns.Add(DetachColumn);

            AddNodesToGrid();

        }
        #endregion

        #region TrnClientManagement_Activated
        private void TrnClientManagement_Activated(object sender, EventArgs e)
        {
            AddNodesToGrid();
        }
        #endregion

        #region dsClientsView_CellClick
        private void dsClientsView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //-- TO DETACH 
                string MachineID = "";
                string MachineName = "";
                if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

                if (dsClientsView.CurrentCell.ColumnIndex == 2)// columns  [2].Selected == true)
                {
                    if (cmnService.J_UserMessage("Are you sure you want to detach [" + Convert.ToString(dsClientsView[1, e.RowIndex].Value + "] machine.\n" +
                        "Please note after this machine is detached, user will not be able to connect to the server until the machine is registered again."), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
                    //--
                    MachineID = Convert.ToString(dsClientsView[0, e.RowIndex].Value);
                    MachineName = Convert.ToString(dsClientsView[1, e.RowIndex].Value);
                    //--
                    #region DELETE FROM XML FILE
                    //
                    XmlDocument XMLDoc = new XmlDocument();
                    XMLDoc.Load(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer));
                    //
                    XmlNodeList xnList = XMLDoc.GetElementsByTagName(T_XML.NODE);
                    //
                    foreach (XmlNode xn in xnList)
                    {
                        if (MachineID == cmnService.J_Decode(xn[T_XML.MACHINENO].InnerText) && MachineName == cmnService.J_Decode(xn[T_XML.MACHINENAME].InnerText))
                        {
                            xn.RemoveAll();
                            //xn.RemoveChild(xn.ParentNode);
                            XMLDoc.Save(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer));
                            //
                            break;
                        }
                    }
                    //--
                    XmlNodeList emptyElements = XMLDoc.SelectNodes(@"//*[not(node())]");
                    //--
                    for (int i = emptyElements.Count - 1; i >= 0; i--)
                    {
                        emptyElements[i].ParentNode.RemoveChild(emptyElements[i]);
                    }
                    //--
                    XMLDoc.Save(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer));
                    //--
                    #endregion
                    //--
                    #region DELETE FROM MST_SETUP & DROP TEMP TABLES
                    Tdsman.CLEAN_TEMP_DATABASE(cmnService.J_ReturnInt64Value(MachineID));
                    #endregion
                    //--
                    AddNodesToGrid();
                }
            }
            catch(Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region AddNodesToGrid
        private bool AddNodesToGrid()
        {
            //First select all the nodes from the text file
            //--
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer));
            //
            XmlNodeList xnList = XMLDoc.GetElementsByTagName(T_XML.NODE);
            //
            dsClientsView.Rows.Clear();
            foreach (XmlNode xn in xnList)
            {
                dsClientsView.Rows.Add();
                //
                dsClientsView.Rows[dsClientsView.Rows.Count - 1].Cells[0].Value = cmnService.J_Decode(xn[T_XML.MACHINENO].InnerText);
                dsClientsView.Rows[dsClientsView.Rows.Count - 1].Cells[1].Value = cmnService.J_Decode(xn[T_XML.MACHINENAME].InnerText);
            }
            //
            if (dsClientsView.Rows.Count <= 0)
            {
                lblNoClients.Visible = true;
                btnRefreshGrid.Visible = false;
                dsClientsView.Visible = false;
            }
            else
            {
                lblNoClients.Visible = false;
                btnRefreshGrid.Visible = true;
                dsClientsView.Visible = true;
            }
            //--
            return true;
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            //
            GC.Collect();
            //-- dmlService.Dispose();
            this.Close();
            this.Dispose();
            //
        }
        #endregion

        #region btnRefreshGrid_Click
        private void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            AddNodesToGrid();
        }
        #endregion

        #region btnRefreshGrid_MouseMove
        private void btnRefreshGrid_MouseMove(object sender, MouseEventArgs e)
        {
            //ToolTip.SetToolTip(btnRefreshGrid, "Click to refresh the above grid");
        }

        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0117", Tdsman.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}

