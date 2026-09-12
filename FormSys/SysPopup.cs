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

using Microsoft.Win32;

namespace TDSMAN.FormSys
{
    public partial class SysPopup : Form
    {

        #region System Generated Code
        public SysPopup()
        {
            InitializeComponent();
            tmrFading.Start();
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

        //string strSQL = string.Empty;
        //string strDatabaseDisplayTextInStatusBar = string.Empty;

        #endregion

        #region SysPopup_Load
        private void SysPopup_Load(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_pShowPopup = false;
            //
            //this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            //this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            //
        }
        #endregion

        #region tmrFading_Tick
        private void tmrFading_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.003;
            if (this.Opacity == 1)
            {
                tmrFading.Stop(); tmrFadingOut.Start();
            }
        }
        #endregion

        #region tmrFadingOut_Tick
        private void tmrFadingOut_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.005;
            if (this.Opacity == 0)
            {
                tmrFading.Stop();
                this.Close();
                this.Dispose();
            }
        }
        #endregion

        #region lnkUpdateNow_LinkClicked
        private void lnkUpdateNow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //--
            #region GetConnectedExternalDrives
            List<string> externalDevices = TDSMAN.Classes.TDSMAN.GetConnectedExternalDrives();
            if (externalDevices.Count > 0)
            {
                //
                foreach (string device in externalDevices)
                {
                    Console.WriteLine(device);
                }
                //
                cmnService.J_UserMessage("It seems you have external Pen Drive/Hard Drive attached.\nPlease remove before software update can take place.", MessageBoxIcon.Stop);
                return;
            }
            #endregion
            //Creating a File with Version and Financial Year
            StreamWriter writer = new StreamWriter(Path.Combine(Application.StartupPath, "TempUpdate.txt"));
            //
            writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pVersionType.ToString());
            writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString());
            writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pEditionType.ToString());
            writer.WriteLine(TDSMAN.Classes.TDSMAN.T_ClientServerMachine.ToString()); //-- ANIK 2016-06-30
            writer.WriteLine(System.IntPtr.Size.ToString());                          //-- ANIK 2021-02-05
            //
            writer.Flush();
            writer.Dispose();
            writer.Close();
            //Creating a File with Version and Financial Year
            StreamWriter writerMyDoc = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TempUpdate.txt"));
            //
            writerMyDoc.WriteLine(TDSMAN.Classes.TDSMAN.T_pVersionType.ToString());
            writerMyDoc.WriteLine(TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString());
            writerMyDoc.WriteLine(TDSMAN.Classes.TDSMAN.T_pEditionType.ToString());
            writerMyDoc.WriteLine(TDSMAN.Classes.TDSMAN.T_ClientServerMachine.ToString()); //-- ANIK 2015-01-30
            writerMyDoc.WriteLine(System.IntPtr.Size.ToString());                          //-- ANIK 2021-02-05
            //
            writerMyDoc.Flush();
            writerMyDoc.Dispose();
            writerMyDoc.Close();
            //
            
            if (TdsMan.T_CheckServerTDSMAN() == true)
            {
                //checking if the latest updator file exists
                if (File.Exists(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication)) == false)
                {
                    UtUpdateApplicationUpdate objUpdateUpdaterexe = new UtUpdateApplicationUpdate();
                    objUpdateUpdaterexe.ShowDialog();
                }
                else
                {
                    //if file exists

                    //chekcing the version of the file
                    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication));
                    double dblUpdatatorexeVersion = Convert.ToDouble(fileVersionInfo.FileMajorPart + "." + fileVersionInfo.FileMinorPart);

                    //now checking the latest update available and downloading the same
                    if (Registration.Get_Latest_Updater_version(dblUpdatatorexeVersion) == true)
                    {
                        UtUpdateApplicationUpdate objUpdateUpdaterexe = new UtUpdateApplicationUpdate();
                        objUpdateUpdaterexe.ShowDialog();
                    }
                }
            }
            //-- CHECK MULTI INSTALLATION OF A SINGLE SERIAL NO. 2025/03/15
            if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE 
                && TDSMAN.Classes.TDSMAN.T_pVersionType != T_VERSION_TYPE.TRIAL_VERSION)
            {
                string Serial_No = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                                                                       T_RegistrationInfo.Serial_No.ToString());
                if (Serial_No == "")
                {
                    cmnService.J_UserMessage("Software updation has failed - contact Helpdesk at +91-33-22875500, +91-33-40845500, 9836490007.");
                    return;
                }
                string HardDiskSerialNo = TdsMan.GetHardDiskSerialNo();
                string machineName = Environment.MachineName;
                //
                if (Registration.Update_Check_Machine_Unique_No(Serial_No, HardDiskSerialNo, machineName) == false)
                {
                    cmnService.J_UserMessage("Software updation has failed - contact Helpdesk at +91-33-22875500, +91-33-40845500, 9836490007.");
                    //
                    RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(), RegistryKeyPermissionCheck.ReadWriteSubTree);
                    if (registryKey != null)
                    {
                        registryKey.Close();
                        // DELETE REGISTRY
                        registryKey = Registry.CurrentUser.OpenSubKey(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder());
                        //
                        if (registryKey != null)
                        {
                            Registry.CurrentUser.DeleteSubKeyTree(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder());
                        }
                    }
                    return;
                }
            }
            //
            if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.PUBLICATION_HOUSE_VERSION)
            {
                cmnService.J_UserMessage("Not available in this version");
                return;
            }
            else if (cmnService.J_UserMessage("For updating of " + TDSMAN.Classes.TDSMAN.T_pPackageName + " software, the application will be closed and the " +
                "update application will check for new updates\n and accordingly will synchronise " + TDSMAN.Classes.TDSMAN.T_pPackageName + " to the latest version." +
                "\n\n Do you want to proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Added by Shrey Kejriwal on 12-02-2014
                TdsMan.AutoBackupData("Before update");
                //if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                //{
                //    if (File.Exists(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pTrialUpdateApplication) == true)
                //    {
                //        dmlService.Dispose();
                //        this.Close();
                //        this.Dispose();
                //        Process.Start(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pTrialUpdateApplication);
                //    }
                //}
                //else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                //{

                //UtUpdateApplicationUpdate objUpdateUpdaterexe = new UtUpdateApplicationUpdate();
                //objUpdateUpdaterexe.ShowDialog();

                if (File.Exists(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication) == true)
                {
                    dmlService.Dispose();

                    this.Close();
                    this.Dispose();

                    J_Var.frmMain.Close();
                    J_Var.frmMain.Dispose();

                    Process.Start(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication);
                }
                //}
                //else 
            }
            else
                return;
        }
        #endregion

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion
        
    }
}