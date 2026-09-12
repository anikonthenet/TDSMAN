using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using TDSMAN.Classes;

namespace TDSMAN.FormTrn
{
    public partial class TrnBrowseCSIFilePath : Form
    {
        #region Default Constructor
        public TrnBrowseCSIFilePath()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Variables Declaration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        string strFolderPath = string.Empty;
       
        #endregion

        #region User Defined Events

        #region TrnSelectTDSFile_Load
        private void TrnSelectTDSFile_Load(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_pDownloadPath = "";

            btnBrowse.Select();
        }
        #endregion

        #region btnBrowse_Click
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            strFolderPath = cmnService.J_OpenFileDialog("csi files (*.csi)|*.csi");

            if (strFolderPath != "")
                txtFilePath.Text = strFolderPath;
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateFields() == false)
            {
                return;
            }

            TDSMAN.Classes.TDSMAN.T_pDownloadPath = txtFilePath.Text;
            
            this.Close();
            this.Dispose();
        }
        #endregion

        #region btnBack_Click
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region ValidateFields
        public bool ValidateFields()
        {
            try
            {
                // **************************************************
                // **** Blank Check
                // **************************************************
                if (txtFilePath.Text == "")
                {
                    cmnService.J_UserMessage("Please select the CSI file to create the FVU file");
                    btnBrowse.Select();
                    return false;
                }
                
                return true;
            }
            catch (Exception err_handler)
            {
                //cmnService.J_UserMessage("File Format incorrect");
                btnBrowse.Select();
                return false;
            }
        }
        #endregion
        
        #endregion

    }
}