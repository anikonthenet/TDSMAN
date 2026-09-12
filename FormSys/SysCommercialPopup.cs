using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using TDSMAN.Classes;
using TDSMAN.FormUtl;

namespace TDSMAN.FormSys
{
    public partial class SysCommercialPopup : Form
    {

        #region System Generated Code
        public SysCommercialPopup()
        {
            InitializeComponent();
        }
        #endregion

        #region Decleration Section

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();


        // WEB CLASS
        TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();

        //RptDialog rptDialog = new RptDialog();

        //private IDataReader reader;

        string strSQL = string.Empty;
        //string strDatabaseDisplayTextInStatusBar = string.Empty;

        #endregion

        #region SysPopup_Load
        private void SysPopup_Load(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_ShowCommercialPopup = false;
            //
            //this.Left = 0;// Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            //this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            //
            if (TDSMAN.Classes.TDSMAN.T_ShowCommercialPopupLink != "")
            {
                lnkViewMore.Visible = true;
                lnkViewMore.Text = TDSMAN.Classes.TDSMAN.T_ShowCommercialPopupLink;
            }
            //
            lblTitle.Text = TDSMAN.Classes.TDSMAN.T_ShowCommercialPopupTitle;
            lblDesc.Text = TDSMAN.Classes.TDSMAN.T_ShowCommercialPopupDesc;
            //--
            //lnkViewMore.Visible = true;
            //lnkViewMore.Text = "http://tdsman.com/pricing.htm";
            ////
            //lblTitle.Text = "TDSMAN for FY:2014-15";
            //lblDesc.Text = "Privilege offer for existing users - Buy TDSMAN (FY: 2014-15) before 30/04/2014 and avail 20% discount. Click on the link below for placing your order:";
            

        }
        #endregion

        #region lnkUpdateNow_LinkClicked
        private void lnkUpdateNow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ////Creating a File with Version and Financial Year
            //StreamWriter writer = new StreamWriter(Path.Combine(Application.StartupPath, "TempUpdate.txt"));

            //writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pVersionType.ToString());
            //writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString());

            //writer.Flush();
            //writer.Dispose();
            //writer.Close();

            //if (TdsMan.T_CheckServerTDSMAN() == true)
            //{
            //    //checking if the latest updator file exists
            //    if (File.Exists(Path.Combine(Application.StartupPath, "eLicensedUpdateApplication.EXE")) == false)
            //    {
            //        UtUpdateApplicationUpdate objUpdateUpdaterexe = new UtUpdateApplicationUpdate();
            //        objUpdateUpdaterexe.ShowDialog();
            //    }
            //    else
            //    {
            //        //if file exists

            //        //chekcing the version of the file
            //        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Path.Combine(Application.StartupPath, "eLicensedUpdateApplication.EXE"));
            //        double dblUpdatatorexeVersion = Convert.ToDouble(fileVersionInfo.FileMajorPart + "." + fileVersionInfo.FileMinorPart);

            //        //now checking the latest update available and downloading the same
            //        if (Registration.Get_Latest_Updater_version(dblUpdatatorexeVersion) == true)
            //        {
            //            UtUpdateApplicationUpdate objUpdateUpdaterexe = new UtUpdateApplicationUpdate();
            //            objUpdateUpdaterexe.ShowDialog();
            //        }
            //    }
            //}

            ////
            //if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.PUBLICATION_HOUSE_VERSION)
            //{
            //    cmnService.J_UserMessage("Not available in this version");
            //    return;
            //}
            //else if (cmnService.J_UserMessage("For updating of " + TDSMAN.Classes.TDSMAN.T_pPackageName + " software, the application will be closed and the " +
            //    "update application will check for new updates\n and accordingly will synchronise " + TDSMAN.Classes.TDSMAN.T_pPackageName + " to the latest version." +
            //    "\n\n Do you want to proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    //if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
            //    //{
            //    //    if (File.Exists(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pTrialUpdateApplication) == true)
            //    //    {
            //    //        dmlService.Dispose();
            //    //        this.Close();
            //    //        this.Dispose();
            //    //        Process.Start(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pTrialUpdateApplication);
            //    //    }
            //    //}
            //    //else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
            //    //{

            //    //UtUpdateApplicationUpdate objUpdateUpdaterexe = new UtUpdateApplicationUpdate();
            //    //objUpdateUpdaterexe.ShowDialog();

            //    if (File.Exists(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication) == true)
            //    {
            //        dmlService.Dispose();

            //        this.Close();
            //        this.Dispose();

            //        J_Var.frmMain.Close();
            //        J_Var.frmMain.Dispose();

            //        Process.Start(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication);
            //    }
            //    //}
            //    //else 
            //}
            //else
            //    return;
        }
        #endregion

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            //if (chkDoNotShow.Checked == true)
            //{
            //    strSQL = "UPDATE MST_SETUP SET BLOCK_POPUP_ID = " + TDSMAN.Classes.TDSMAN.T_ShowCommercialPopupID;
            //    dmlService.J_ExecSql(strSQL);
            //    //
            //}
            ////--
            dmlService.Dispose();
            //this.Opacity = 0;
            this.Close();
            this.Dispose();
        }
        #endregion

        #region lnkViewMore_LinkClicked
        private void lnkViewMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(TDSMAN.Classes.TDSMAN.T_ShowCommercialPopupLink);
            //
            strSQL = "UPDATE MST_SETUP SET BLOCK_POPUP_ID = " + TDSMAN.Classes.TDSMAN.T_ShowCommercialPopupID;
            dmlService.J_ExecSql(strSQL);
            //
            btnClose_Click(sender, e);
        }
        #endregion

        #region SysCommercialPopup_FormClosing
        private void SysCommercialPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chkDoNotShow.Checked == true)
            {
                strSQL = "UPDATE MST_SETUP SET BLOCK_POPUP_ID = " + TDSMAN.Classes.TDSMAN.T_ShowCommercialPopupID;
                dmlService.J_ExecSql(strSQL);
                //
            }
        }
        #endregion


        

        
        
    }
}