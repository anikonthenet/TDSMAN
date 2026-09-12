using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Management;

using TDSMAN.Classes;

namespace TDSMAN
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }

        #region Install
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            ////MessageBox.Show("1");
            //if (System.IO.File.Exists(System.IO.Path.Combine(Application.StartupPath, "CRRuntime_32bit_13_0_20.msi")) == true)
            //{
            //    string strJREFile = System.IO.Path.Combine(Application.StartupPath, "CRRuntime_32bit_13_0_20.msi");
            //    System.Diagnostics.Process Proc = new System.Diagnostics.Process();
            //    Proc.StartInfo.FileName = strJREFile;
            //    Proc.Start();
            //}
            ////MessageBox.Show("2");
            base.Install(stateSaver);
            RegistrationServices regSrv = new RegistrationServices();
            regSrv.RegisterAssembly(base.GetType().Assembly,
              AssemblyRegistrationFlags.SetCodeBase);
            //--
            ////MessageBox.Show("CRRuntime_32bit_13_0_20");
            ////--
            //System.Threading.Thread.Sleep(5000);
            //System.Diagnostics.Process.Start("CRRuntime_32bit_13_0_20.msi");
            //if (System.IO.File.Exists(System.IO.Path.Combine(Application.StartupPath, "CRRuntime_32bit_13_0_20.msi")) == true)
            //{
            //    string strJREFile = System.IO.Path.Combine(Application.StartupPath, "CRRuntime_32bit_13_0_20.msi");
            //    System.Diagnostics.Process Proc = new System.Diagnostics.Process();
            //    Proc.StartInfo.FileName = strJREFile;
            //    Proc.Start();
            //}
            //--
        }
        #endregion

        #region Uninstall
        public override void Uninstall(System.Collections.IDictionary savedState)
        //protected override void OnBeforeUninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);
            //base.OnBeforeUninstall(savedState);

            CommonService cmnService = new CommonService();
            try
            {
                //
                TDSMAN.Classes.TDSMAN tdsman = new TDSMAN.Classes.TDSMAN();

                //MessageBox.Show("1.0");
                string UninstallCode = "";
                // WEB CLASS
                TDSMAN_WEB.Registration Registration = new TDSMAN_WEB.Registration();
                //MessageBox.Show("1.1");
                //..................................
                // 2011/06/22 ANIK
                if (tdsman.T_CheckInternetConnectivty() == false)
                    UninstallCode = tdsman.T_GetUninstallCode();
                //MessageBox.Show("1.1.1 : " + TDSMAN.Classes.TDSMAN.T_pCompanyName.ToString());
                //MessageBox.Show("1.1.2 : " + tdsman.GetRegistryFolder());
                // GET SERIAL NUMBER
                //TDSMAN.Classes.TDSMAN.T_ProductUpgrade = false;
                //RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + tdsman.GetRegistryFolder());
                //
                //MessageBox.Show("1.2 ");
                //if (registryKey != null)
                //{

                    //MessageBox.Show("1.2.1");
                //string strSerialNo = Convert.ToString(registryKey.GetValue("Serial_No"));
                //string strFileNo = "Serial_No.srl";
                string strFileNo = "Serial_No_" + TDSMAN.Classes.TDSMAN.T_pEditionType.ToString() + "_" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear + ".srl";

                string strSerialNo = "";
                if(File.Exists(Path.Combine(Application.StartupPath, strFileNo)))
                    strSerialNo = System.IO.File.ReadAllText(Path.Combine(Application.StartupPath, strFileNo)).ToString().Trim();
                //registryKey.Close();
                // CALL WEB FUNCTION
                string strOutValue = "";
                if (tdsman.T_CheckInternetConnectivty() == true)
                {
                    //MessageBox.Show("1.2.2");
                    //Registration.Save_User_Detail_Record(strSerialNo, "", TDSMAN.Classes.TDSMAN.T_pEditionType, TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), 3, out strOutValue);
                    string HardDiskSerialNo = GetHardDiskSerialNo();
                    Registration.Save_User_Header_Detail_Record(strSerialNo, 
                                                                "", 
                                                                TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), 
                                                                TDSMAN.Classes.TDSMAN.T_pEditionType, 
                                                                3, 
                                                                "", 
                                                                "", 
                                                                "", 
                                                                "", 
                                                                "", 
                                                                "", 
                                                                "", 
                                                                "",
                                                                "",
                                                                "",
                                                                HardDiskSerialNo,
                                                                "", 
                                                                out strOutValue);
                }
                //..................................
                //MessageBox.Show("1.2.3");
                    // DELETE REGISTRY
                    TDSMAN.Classes.TDSMAN.T_ProductUpgrade = false;
                    //registryKey = Registry.CurrentUser.OpenSubKey(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + tdsman.GetRegistryFolder());
                    ////
                    //if (registryKey != null)
                    //{
                        //MessageBox.Show("1.2.4");
                        TDSMAN.Classes.TDSMAN.T_ProductUpgrade = false;
                        //Registry.CurrentUser.DeleteSubKeyTree(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + tdsman.GetRegistryFolder());
                    //}
                //}

                //MessageBox.Show("1.3" );
                // Procedure for Uninstall Application
                Process uninstallProcess = new Process();
                uninstallProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                uninstallProcess.StartInfo.FileName = "MsiExec.exe";
                //uninstallProcess.StartInfo.Arguments = "/X{55AF868E-FDAC-47F5-AABA-3E9E2100134D}";
                System.Threading.Thread.Sleep(5000);
                //
                #region FY2011_12
                //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2011_12)
                //    uninstallProcess.StartInfo.Arguments = "/x \"{55AF868E-FDAC-47F5-AABA-3E9E2100134D}\"/qn";
                #endregion

                #region FY2012_13
                //else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2012_13)
                //    uninstallProcess.StartInfo.Arguments = "/x \"{E3ADC085-15EB-46AB-A51B-ED852E96B774}\"/qn";
                #endregion

                #region FY2013_14
                //else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2013_14)
                //    uninstallProcess.StartInfo.Arguments = "/x \"{85BB1A3F-FFA4-4180-AC28-013B38B0F975}\"/qn";
                #endregion

                #region FY2014_15
                //else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2014_15)
                //{
                //    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                //        uninstallProcess.StartInfo.Arguments = "/x \"{AC138686-A899-42C0-A7BE-CAE33760B0A6}\"/qn";
                //    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION)
                //        uninstallProcess.StartInfo.Arguments = "/x \"{4B9E1E0C-B2B4-48E8-8B5C-866949976A5F}\"/qn";
                //}
                #endregion

                #region FY2015_16
                //else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2015_16) //-- ANIK 2015-01-28
                //{
                //    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                //    {
                //        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{55017982-B1C7-466C-8D23-B1FB31AAC2F6}\"/qn";
                //        }
                //        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{1A6662C5-0562-4490-937D-2A6632808322}\"/qn";
                //        }
                //    }
                //    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION)
                //    {
                //        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{4D5FCA1C-8F9A-4BF8-84B3-855C5636EA97}\"/qn";
                //        }
                //        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{DB198034-A3A9-4EF5-92CE-B7B31CE5F6D8}\"/qn";
                //        }
                //    }
                //}
                #endregion

                #region FY2016_17
                //else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2016_17) //-- ANIK 2016-01-19
                //{
                //    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                //    {
                //        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{CBD87F62-CA78-45F6-B02A-231737F6A06F}\"/qn";
                //        }
                //        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{79A5D3D6-C975-402F-A903-DAFBC616CD2D}\"/qn";
                //        }
                //    }
                //    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION)
                //    {
                //        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{C88DBA5B-D07E-4568-8384-13ED9A8D3954}\"/qn";
                //        }
                //        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{79A5D3D6-C975-402F-A903-DAFBC616CD2D}\"/qn";
                //        }
                //    }
                //}
                #endregion

                #region FY2017_18
                //else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2017_18) //-- ANIK 2017-01-07
                //{
                //    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                //    {
                //        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{702A7C2B-3CBB-434B-BB56-7A23C8077944}\"/qn";
                //        }
                //        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{471BB62B-5AAA-4884-B866-AED86FC70944}\"/qn";
                //        }
                //    }
                //    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION)
                //    {
                //        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{6B02EE0E-5A99-40CD-885C-82E871DB38DC}\"/qn";
                //        }
                //        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{471BB62B-5AAA-4884-B866-AED86FC70944}\"/qn";
                //        }
                //    }
                //    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_EDITION)
                //    {
                //        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{0037C11D-3D7D-4246-BB53-8D59B45E0BF7}\"/qn";
                //        }
                //        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                //        {
                //            uninstallProcess.StartInfo.Arguments = "/x \"{CB311B7C-0C98-4CD8-89D1-DC07B03A34B9}\"/qn";
                //        }
                //    }
                //}
                #endregion

                #region FY2018_19
                //else 
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2018_19) //-- ANIK 2018-01-18
                {
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{6E0DAA64-05E6-4770-AADA-5AA4B43CA805}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{486BD2B2-94B5-43F0-9EE1-465EFFEFCE95}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{5910E1E3-DBF4-441A-AECB-7AD54D8A5644}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{486BD2B2-94B5-43F0-9EE1-465EFFEFCE95}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_EDITION)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{BC5D63F6-D7F0-445B-AC45-D62C61266E33}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{77425909-170D-4618-9ECB-B2F84B4967FE}\"/qn";
                        }
                    }
                }
                #endregion

                #region FY2019_20
                else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2019_20) //-- ANIK 2019-01-07
                {
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{68992053-36FB-47EB-ADFA-43E1EADF1917}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{0A391ADF-98B4-4098-B17A-502FD4D92840}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{77671CA5-EEF4-4667-B45A-23CA0E78CFA7}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{0A391ADF-98B4-4098-B17A-502FD4D92840}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_EDITION)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{22FDE525-0AC1-433C-86DE-9BFA01471A5A}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{8FEDD514-35AE-4088-B508-64E9617BD6AA}\"/qn";
                        }
                    }
                }
                #endregion

                #region FY2020_21
                //-- 2020/01/09
                else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2020_21)
                {
                    //-- //MessageBox.Show(" 6 " + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString());
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{61F3073A-DA2C-4A3D-927E-8BDFD0B316F7}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{281957A2-0330-41E4-AE59-F8D5DC81860C}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION)
                    {

                        //-- //MessageBox.Show(" 7 " + TDSMAN.Classes.TDSMAN.T_pEditionType.ToString());
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            //-- //MessageBox.Show(" 8 " + TDSMAN.Classes.TDSMAN.T_ClientServerMachine.ToString());
                            uninstallProcess.StartInfo.Arguments = "/x \"{9F3FA39E-C579-4D4A-9D05-556F31BDAD07}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{281957A2-0330-41E4-AE59-F8D5DC81860C}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_EDITION)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{2DF81734-90CB-449E-8187-4A31FA1E9311}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{BFAF872D-56CF-4FF6-A232-3D7E7D9D79D2}\"/qn";
                        }
                    }
                }
                #endregion

                #region FY2021_22
                //-- 2021/01/13
                else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2021_22)
                {
                    ////MessageBox.Show("1.5");
                    //-- //MessageBox.Show(" 6 " + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString());
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                    {
                        ////MessageBox.Show("1.6");
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            ////MessageBox.Show("1.7");
                            uninstallProcess.StartInfo.Arguments = "/x \"{82A6C385-892D-4F33-9B32-7C33A449B91B}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{B504F6A2-E8A0-4DF4-9737-1D42F0A7BB67}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION)
                    {

                        //-- //MessageBox.Show(" 7 " + TDSMAN.Classes.TDSMAN.T_pEditionType.ToString());
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            //-- //MessageBox.Show(" 8 " + TDSMAN.Classes.TDSMAN.T_ClientServerMachine.ToString());
                            uninstallProcess.StartInfo.Arguments = "/x \"{16C4D50D-DB47-4791-8DEB-C6FE1EF4FC8D}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{B504F6A2-E8A0-4DF4-9737-1D42F0A7BB67}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_EDITION)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{1D2B424A-5A11-4668-A9F9-F24A1890E011}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{87120FE8-A452-4A96-8DE3-BD429C5E8F9F}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)
                    {

                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            //uninstallProcess.StartInfo.Arguments = "/x \"{365ADEAC-DAB9-4D06-81C3-73078D801752}\"/qn";
                            uninstallProcess.StartInfo.Arguments = "/x \"{C3F094B1-6F43-4351-A76C-36E86D35C8DB}\"/qn";//-- 64BIT
                            uninstallProcess.StartInfo.Arguments = "/x \"{D0F5168D-5189-4C7D-B7CB-9C8435967DB3}\"/qn";//-- 32BIT
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{504BCDEB-7B4A-4598-812B-BDBF49D50345}\"/qn"; //-- 64BIT
                            uninstallProcess.StartInfo.Arguments = "/x \"{87A49215-F10B-47D9-A34C-AB739666F1B0}\"/qn";//-- 32BIT

                        }
                    }
                }
                #endregion

                #region FY2022_23
                //-- 2022/01/18
                else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2022_23)
                {
                    ////MessageBox.Show("1.5");
                    //-- //MessageBox.Show(" 6 " + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString());
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                    {
                        ////MessageBox.Show("1.6");
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            ////MessageBox.Show("1.7");
                            uninstallProcess.StartInfo.Arguments = "/x \"{01F8CE13-4805-4CC8-B3A2-85FFAEBDD794}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{62187250-A14E-4B23-9F27-ACAF31A3957C}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION)
                    {

                        //-- //MessageBox.Show(" 7 " + TDSMAN.Classes.TDSMAN.T_pEditionType.ToString());
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            //-- //MessageBox.Show(" 8 " + TDSMAN.Classes.TDSMAN.T_ClientServerMachine.ToString());
                            uninstallProcess.StartInfo.Arguments = "/x \"{F6C7E1B8-9194-483B-9AC8-9E7FBA05EEEB}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{62187250-A14E-4B23-9F27-ACAF31A3957C}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE) //--2022/04/11
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_EDITION)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{B4B1AD39-BBE5-4A39-99ED-9C2B3079411C}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{A7E6F1C6-28B9-4BE1-B095-02D1453F9C4E}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)
                    {

                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            //uninstallProcess.StartInfo.Arguments = "/x \"{365ADEAC-DAB9-4D06-81C3-73078D801752}\"/qn";
                            uninstallProcess.StartInfo.Arguments = "/x \"{ACC8E03E-A86A-4481-87FB-606980E33D79}\"/qn";//-- 64BIT
                            uninstallProcess.StartInfo.Arguments = "/x \"{D0F5168D-5189-4C7D-B7CB-9C8435967DB3}\"/qn";//-- 32BIT
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{EE2A5182-CA6C-4996-B743-0CE4FE49A286}\"/qn"; //-- 64BIT
                            uninstallProcess.StartInfo.Arguments = "/x \"{87A49215-F10B-47D9-A34C-AB739666F1B0}\"/qn";//-- 32BIT

                        }
                    }
                }
                #endregion
                
                #region FY2023_24
                //-- 2022/01/18
                else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2023_24)
                {

                    //MessageBox.Show("1.5");
                    //-- //MessageBox.Show(" 6 " + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString());
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                    {
                        ////MessageBox.Show("1.6");
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            ////MessageBox.Show("1.7");
                            uninstallProcess.StartInfo.Arguments = "/x \"{2F6C408A-4615-49F9-8C38-084830EC0C98}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{00780E1E-5B92-451A-BA97-FF3D0FD49CA8}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION)
                    {

                        //MessageBox.Show(" 7 " + TDSMAN.Classes.TDSMAN.T_pEditionType.ToString());
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            //-- //MessageBox.Show(" 8 " + TDSMAN.Classes.TDSMAN.T_ClientServerMachine.ToString());
                            uninstallProcess.StartInfo.Arguments = "/x \"{60DCFEBA-BC21-4A61-84B9-2AFBBC938EAB}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{00780E1E-5B92-451A-BA97-FF3D0FD49CA8}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE) //--2022/04/11
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_EDITION)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{53F47553-0AD7-4CB4-AC61-F9631BFF7887}\"/qn";
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            uninstallProcess.StartInfo.Arguments = "/x \"{0B0C5BF3-9EB8-4022-BF09-1F6AAC10FDB3}\"/qn";
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)
                    {

                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                        {
                            //uninstallProcess.StartInfo.Arguments = "/x \"{365ADEAC-DAB9-4D06-81C3-73078D801752}\"/qn";
                            //uninstallProcess.StartInfo.Arguments = "/x \"{ACC8E03E-A86A-4481-87FB-606980E33D79}\"/qn";//-- 64BIT
                            uninstallProcess.StartInfo.Arguments = "/x \"{695152A3-9A1E-4A50-BEBB-8E6A6BB54503}\"/qn";//-- 32BIT
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            //uninstallProcess.StartInfo.Arguments = "/x \"{EE2A5182-CA6C-4996-B743-0CE4FE49A286}\"/qn"; //-- 64BIT
                            uninstallProcess.StartInfo.Arguments = "/x \"{A8FC0D83-9867-440B-B39E-7757A4912EC7}\"/qn";//-- 32BIT

                        }
                    }
                }
                #endregion
                //
                //uninstallProcess.StartInfo.UseShellExecute = false;
                ////MessageBox.Show("1.8");
                uninstallProcess.Start();
                //
                uninstallProcess.Refresh();
                uninstallProcess.Close();
                //                
                //MessageBox.Show(" 9 " + UninstallCode.ToString());
                if (UninstallCode != "")
                {
                    //
                    string strUnistallFile = System.IO.Path.Combine(Application.StartupPath, "TDSMAN-Uninstall Number.txt");
                    System.IO.StreamWriter MyStream = null;
                    MyStream = System.IO.File.CreateText(strUnistallFile);
                    MyStream.Close();
                    MyStream.Dispose();
                    //
                    System.IO.StreamWriter File = new System.IO.StreamWriter(strUnistallFile);
                    File.WriteLine("");
                    File.WriteLine("Your uninstallation number : " + UninstallCode);
                    File.WriteLine("");
                    File.WriteLine("Save this file to your desired location.");
                    //File.WriteLine("Mail this code to info@tdsman.com or call us at +91-33-22623535/64596006");
                    File.WriteLine("Mail this code to info@tdsman.com or call us at +91-33-22875500/+91-33-40845500/98364 90007");
                    File.Flush();
                    File.Close();
                    //
                    if (System.IO.File.Exists(strUnistallFile) == true)
                        System.Diagnostics.Process.Start(strUnistallFile);
                }
                //
                ////MessageBox.Show("1.9");
                if (tdsman.T_CheckInternetConnectivty() == true)
                    MessageBox.Show("Uninstall completed.", "TDSMAN");
                else
                    MessageBox.Show("Uninstall completed.\n\nPlease Note down your uninstallation number : " + UninstallCode, "TDSMAN");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        #endregion

        #region GetHardDiskSerialNo
        //Added by Anik  on 12/03/2025
        public string GetHardDiskSerialNo()
        {
            try
            {
                string strHDSerialNumber = "";
                //
                ManagementClass mc = new System.Management.ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                //
                foreach (ManagementObject mo in moc)
                {
                    try
                    {
                        strHDSerialNumber = mo["SerialNumber"].ToString().Trim();
                        break;
                    }
                    catch
                    {
                        strHDSerialNumber = "";
                    }

                    if (strHDSerialNumber == "")
                        continue;
                    else
                        break;
                }
                //Checking if Hard Disk serial Number returns blank...
                if (strHDSerialNumber.Trim() == "")
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "powershell",
                        Arguments = "-Command \"Get-PhysicalDisk | Select-Object -ExpandProperty SerialNumber\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    Process process = new Process { StartInfo = psi };
                    process.Start();
                    //string result = process.StandardOutput.ReadToEnd();
                    string firstSerial = "";
                    while (!process.StandardOutput.EndOfStream)
                    {
                        firstSerial = process.StandardOutput.ReadLine().Trim();
                        if (!string.IsNullOrEmpty(firstSerial))
                            break;
                    }
                    process.WaitForExit();
                    //
                    strHDSerialNumber = firstSerial.Trim();
                }
                return strHDSerialNumber.Replace("-", "").Replace("_", "");
            }
            catch
            {
                //RETURNING A CUSTOM HARD DISK NO WHERE HARD DISK NO IS NOT CREATED
                return "TM9999";
            }
        }
        #endregion
    }
}