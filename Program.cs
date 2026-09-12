 
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net;
using System.Net.Security;

using TDSMAN.FormCmn;
using TDSMAN.FormSys;
using TDSMAN.Classes;
using TDSMAN.FormTrn;


namespace TDSMAN
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //----------------------------------------------------------------------------------
            J_Var.J_pProjectName = "TDSMAN";
            //
            //
            #region TDSMAN PROCESS RUNNING
            if (TDSMAN.Classes.TDSMAN.J_IsApplicationOpen() != null)
            {
                MessageBox.Show(J_Var.J_pProjectName + " application is already running on another window", J_Var.J_pProjectName);
                return;
            }
            //--
            //-- FOR VMs
            //if (TDSMAN.Classes.TDSMAN.J_IsApplicationRunningVMs() == true)
            //{
            //    MessageBox.Show(J_Var.J_pProjectName + " application is already running on another window", J_Var.J_pProjectName);
            //    return;
            //}
            #endregion
            //
            #region ADMINISTRATOR PERMISSION
            //if (TDSMAN.Classes.TDSMAN.IsAdministrator() == false)
            //{
            //    MessageBox.Show("Please check that " + J_Var.J_pProjectName + " have administrator privileges or run the application as an administrator.\n" + 
            //                    "[RIGHT CLICK the exe (present in installed folder) > go to PROPERTIES > go to COMPATIBILITY tab > check RUN THIS PROGRAM AS AN ADMINISTRATOR]", J_Var.J_pProjectName);
            //    //return;
            //}
            #endregion            
            //
            //==================================================================================
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //==================================================================================
            J_Var.J_pLoginScreen = J_LoginScreen.NO;
            //==================================================================================
            J_Var.J_pMsAccessDatabaseName = "TDSMAN.mdb";
            //J_Var.J_pMsAccessDatabasePassword = "mother";
            //Modified by Indrajit on 11-02-2013
            J_Var.J_pApplicationType = J_ApplicationType.StandAlone_SingleMachine;
            //J_Var.J_pApplicationType = J_ApplicationType.StandAlone_Network;
            //==================================================================================
            J_Var.J_pDatabaseType = J_DatabaseType.MsAccess;
            J_Var.J_pConnectionProviderType = J_ConnectionProviderType.OleDb;
            //==================================================================================
            J_Var.J_pXmlConnectionFileName = "JCSCON";
            //J_Var.J_pXmlConnectionFileName = "JCS_MSSQLSERVER";
            //
            //-- 2016/01/30
            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
            {
                //J_Var.J_pMsAccessDatabaseName = "TDSMAN";
                J_Var.J_pSQLDatabasePassword = "Password123";
                J_Var.J_pDatabaseType = J_DatabaseType.SqlServer;
                J_Var.J_pConnectionProviderType = J_ConnectionProviderType.Sql;
                J_Var.J_pApplicationType = J_ApplicationType.StandAlone_Network;
                //-- 2017/03/27
                //if(TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION)
                //    TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication = "UpdateEnterprise.exe";
            }
            //J_Var.J_pXmlConnectionFileName = "JCSCON";
            //==================================================================================
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //==================================================================================
            DMLService dmlService = new DMLService();
            CommonService cmnService = new CommonService();
            DateService dtService = new DateService();
            TDSMAN.Classes.TDSMAN Tdsman = new TDSMAN.Classes.TDSMAN();
            //----------------------------------------------------------------------------------            
            #region GET EDITION
            //if (File.Exists(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_DllFileToDetectEdition)) == true)
            //{
            //    TDSMAN.Classes.TDSMAN.T_pVersionType = T_VERSION_TYPE.TRIAL_VERSION;
            //}
            ////-- 2016/01/15
            //if (File.Exists(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_DllFileToDetectEdition)) == false)
            //{
            //    TDSMAN.Classes.TDSMAN.T_pVersionType = T_VERSION_TYPE.TRIAL_VERSION;
            //}
            //-- 2016/01/16
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2016_17
                && TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION
                && TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.SINGLE_USER)
            {
                //if (Tdsman.GetEdition() != "")
                //{
                    if (Tdsman.GetEdition() == T_VERSION_TYPE.TRIAL_VERSION.ToString())
                    {
                        TDSMAN.Classes.TDSMAN.T_pVersionType = T_VERSION_TYPE.TRIAL_VERSION;
                    }
                //}
                //else
                //{
                //    cmnService.J_UserMessage("TDSMAN failed to detect version.\nPlease contact Helpline : +91-33-22623535, 64596006. Email : info@tdsman.com");
                //    return;
                //}
            }
            
            #endregion
            //----------------------------------------------------------------------------------            
            #region CHECK SERVER/CLIENT
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer) //-- 2015/05/19
            {
                J_Var.J_pApplicationType = J_ApplicationType.StandAlone_Network;
                //--
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                    TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.MULTI_USER;
                else
                {
                    TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.SINGLE_USER;
                    //-- 2013-12-19
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                        TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.STANDARD_USER;
                }
            }
            else
            {
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                {
                    J_Var.J_pApplicationType = J_ApplicationType.StandAlone_SingleMachineBrowser;
                    TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.MULTI_USER;
                }
                else if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.SINGLE_USER)
                {
                    J_Var.J_pApplicationType = J_ApplicationType.StandAlone_SingleMachine;
                    TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.SINGLE_USER;
                    //-- 2013-12-19
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                        TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.STANDARD_USER;
                }
            }
            #endregion
            //----------------------------------------------------------------------------------
            string strSQL = string.Empty;
            //----------------------------------------------------------------------------------
            //-- To Check the DateTime Format
            //----------------------------------------------------------------------------------
            //---
            //Commented and Added by Dhrub on 02/12/2013 For System Date Format Checking with "dd/MM/yyyy" and "dd-MM-yyyy"
            //if (dtService.J_SystemDateFormatCheck_dd_MM_yyyy() == false) return;
            if (dtService.J_SystemDateFormatCheck_dd_MM_yyyy(dtService.J_GetSystemShortDateFormat(), "dd/MM/yyyy", "dd-MM-yyyy") == false) return;
            //----
            //Comented and Added by Dhrub on 02/12/2013 For System Date Format Checking with "dd/MM/yyyy" and "dd-MM-yyyy"
            //if (dtService.J_GetSystemDateFormat() != "dd/MM/yyyy")
            if ((dtService.J_GetSystemShortDateFormat() != "dd/MM/yyyy") && (dtService.J_GetSystemShortDateFormat() != "dd-MM-yyyy"))
            {
                cmnService.J_UserMessage("The System date format has been changed.\nPlease restart the application once.");
                dmlService.Dispose();
                return;
            }
            //----------------------------------------------------------------------------------
            J_Var.J_pCommandTimeout = 99999;
            //----------------------------------------------------------------------------------
            //if (Tdsman.Is64Bit() == true)
            //{
            //    cmnService.J_UserMessage("This application will not run in 64bit OS !!");
            //    return;
            //}
            ////try
            ////{
            ////    //Dns.GetHostEntry("www.tdscpc.gov.in");
            ////    ServicePointManager.SecurityProtocol =
            ////    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ////     ServicePointManager.Expect100Continue = false;
            ////    // TLS warm-up using HEAD request
            ////    //HttpWebRequest dummy = (HttpWebRequest)WebRequest.Create("https://www.tdscpc.gov.in/");
            ////    //dummy.Method = "GET";
            ////    //dummy.Timeout = 5000;
            ////    //dummy.Accept = "text/html";
            ////    //dummy.UserAgent = "Mozilla/5.0";

            ////    //using (var response = (HttpWebResponse)dummy.GetResponse())
            ////    //using (var stream = response.GetResponseStream())
            ////    //using (var reader = new StreamReader(stream))
            ////    //{
            ////    //    char[] buffer = new char[100];
            ////    //    reader.Read(buffer, 0, buffer.Length); // Only read part of the page
            ////    //}
            ////}
            ////catch (Exception err)
            ////{
            ////    // ignore if fails silently
            ////}
            //--
            #region StandAlone_SingleMachine

            if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachine)
            {
                //----------------------------------------------------------------------------------
                if (cmnService.J_IsFileExist(Application.StartupPath + "/" + J_Var.J_pMsAccessDatabaseName) == false)
                {
                    dmlService.Dispose();
                    cmnService.J_UserMessage("Database file does not exist.\nPlease check the database file");
                    return;
                }
                //----------------------------------------------------------------------------------
                if (dmlService.J_ValidateConnection() == false)
                {
                    dmlService.Dispose();
                    cmnService.J_UserMessage("Invalid database.\nPlease check the database");
                    return;
                }
                if (dmlService.J_IsDatabaseObjectExist("MST_ASSESSMENT") == false)
                {
                    dmlService.Dispose();
                    cmnService.J_UserMessage("Invalid database structure.\nPlease check the database");
                    return;
                }
                //if (Tdsman.T_CheckDatabaseCompatibility() == false)
                //{
                //    cmnService.J_UserMessage("Invalid database.\nPlease check the database");
                //    return;
                //}
                //=================================================================
                Hashtable nameValue = new Hashtable();
                //=================================================================
                nameValue.Add("SERVERNAME", "");
                nameValue.Add("DATABASENAME", Application.StartupPath);
                nameValue.Add("USERNAME", J_Var.J_pMsAccessDatabaseName);
                nameValue.Add("PASSWORD", J_Var.J_pMsAccessDatabasePassword);
                //=================================================================
                XMLService objxml = new XMLService();
                objxml.J_CreateXMLFile(nameValue, Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
                //=================================================================
                //
                J_Var.J_pDBPath = Application.StartupPath;
                //
                if (dmlService.J_IsRecordExist("MST_ASSESSMENT") == true)
                {
                    dmlService.Dispose();
                    //--
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //--
                    //MessageBox.Show("1");
                    //--
                    TrnSplashScreen TrnSplashScreen = new TrnSplashScreen();
                    TrnSplashScreen.Show();
                    //--
                    //MessageBox.Show("2");
                    //MessageBox.Show("StandAlone_SingleMachine");
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        if (Tdsman.T_SystemMaintenance() == false)
                            return;
                    //---
                    //MessageBox.Show("3");
                    //--
                    TrnSplashScreen.Close();
                    //MessageBox.Show("4");                    
                    //----------------
                    Tdsman.GetMachineID();    //-- GET MACHINE ID (MULTIUSER)
                    //MessageBox.Show("5");
                    Tdsman.SET_TEMP_TABLES(); //-- SETTING TEMP TABLE NAMES BASED ON MACHINE ID
                    //MessageBox.Show("6");
                    Tdsman.GetSetup();        //-- RUNNING SETUP & GET THE SET VALUES
                    //MessageBox.Show("7");    
                    //----------------
                    //-- APPLYING << RECORD LIMITATIONS >> BASED ON EDITIONS
                    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2014_15)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountProfessional;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE)   //-- 2022/04/11
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountClh;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION)   //-- 2016/01/30
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterprise;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountStandard;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.LITE_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountLite;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)   //-- 2021/01/13
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterpriseLite;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)   //-- 2024/06/06
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterpriseUltimate;
                    }
                    //--
                    //MessageBox.Show("8");    
                    if (J_Var.J_pLoginScreen == J_LoginScreen.YES)
                    {
                        CmnLogin frm = new CmnLogin();
                        frm.ShowDialog();
                        frm.Dispose();
                    }
                    else if (J_Var.J_pLoginScreen == J_LoginScreen.NO)
                    {
                        //--
                        //if (Tdsman.T_SystemMaintenance() == false)
                        //    return;
                        //--                        
                        if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                        {
                            //MessageBox.Show("9");    
                            //J_Var.frmMain = new mdiTDSMAN();
                            //J_Var.frmMain.ShowDialog();
                            Tdsman.T_LoadLicensedMainForm();
                            //MessageBox.Show("10");    
                        }
                        else
                        {
                            Tdsman.T_LoadTrialMainForm();
                        }
                    }
                }
                else
                {
                    dmlService.Dispose();
                    //--
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //CmnCreate1stFAYear frm = new CmnCreate1stFAYear();
                    //frm.ShowDialog();
                    //frm.Dispose();
                }
                return;
            }
            #endregion

            #region StandAlone_SingleMachineBrowser
            if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachineBrowser)
            {
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                {
                    //----------------------------------------------------------------------------------
                    if (cmnService.J_IsFileExist(Application.StartupPath + "/" + J_Var.J_pMsAccessDatabaseName) == false)
                    {
                        dmlService.Dispose();
                        cmnService.J_UserMessage("Database file does not exist.\nPlease check the database file", MessageBoxIcon.Exclamation);
                        return;
                    }
                    //=================================================================
                    Hashtable nameValue = new Hashtable();
                    //=================================================================
                    nameValue.Add("SERVERNAME", "");
                    nameValue.Add("DATABASENAME", Application.StartupPath);
                    nameValue.Add("USERNAME", J_Var.J_pMsAccessDatabaseName);
                    nameValue.Add("PASSWORD", J_Var.J_pMsAccessDatabasePassword);
                    //=================================================================
                    XMLService objxml = new XMLService();
                    objxml.J_CreateXMLFile(nameValue, Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
                    //=================================================================
                    //----------------------------------------------------------------------------------
                    if (dmlService.J_ValidateConnection() == false)
                    {
                        dmlService.Dispose();
                        cmnService.J_UserMessage("Invalid database.\nPlease check the database", MessageBoxIcon.Exclamation);
                        return;
                    }
                    //----------------------------------------------------------------------------------
                    if (dmlService.J_IsDatabaseObjectExist("MST_SETUP") == false)
                    {
                        dmlService.Dispose();
                        cmnService.J_UserMessage("Invalid database structure.\nPlease check the database", MessageBoxIcon.Exclamation);
                        return;
                    }
                    //----------------------------------------------------------------------------------
                    TrnSplashScreen TrnSplashScreen = new TrnSplashScreen();
                    TrnSplashScreen.Show();
                    //---
                    //MessageBox.Show("StandAlone_SingleMachineBrowser");
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        if (Tdsman.T_SystemMaintenance() == false)
                            return;
                    //---
                    TrnSplashScreen.Close();
                    //if (CP.GET_TEMP_TABLES() == false)
                    //    return;
                    Tdsman.GetMachineID();
                    Tdsman.GetSetup();
                    Tdsman.SET_TEMP_TABLES();
                    //----------------------------------------------------------------------------------                
                }//--
                else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        Tdsman.RenameDatabase(J_Var.J_pMsAccessDatabaseName);
                //--
                //Tdsman.GetSetup();
                //Tdsman.GetMachineID();
                //Tdsman.SET_TEMP_TABLES();
                //--
                //-- APPLYING << RECORD LIMITATIONS >> BASED ON EDITIONS
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2014_15)
                {
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                        TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountProfessional;
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE) //-- 2022/04/11
                        TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountClh;
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                        TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountStandard;
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.LITE_EDITION)
                        TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountLite;
                }
                //TDSMAN.Classes.TDSMAN.T_pProductSerial = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + Tdsman.GetRegistryFolder(), T_RegistrationInfo.Serial_No.ToString());
                //TDSMAN.Classes.TDSMAN.T_pProductUser = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + Tdsman.GetRegistryFolder(), T_RegistrationInfo.Licensee_Name.ToString());
                //----------------------------------------------------------------------------------
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //--
                if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                    Tdsman.T_LoadLicensedMainForm();
                else
                    Tdsman.T_LoadTrialMainForm();
                //----------------------------------------------------------------------------------
            }
            #endregion

            #region StandAlone_Network

            if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_Network && (TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_LITE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION))
            {
                if (cmnService.J_IsFileExist(J_Var.J_pXmlConnectionFileName) == false)
                {
                    SysServerInfoLocal frmLocal = new SysServerInfoLocal();
                    frmLocal.ShowDialog();
                    frmLocal.Dispose();
                    return;
                }
                //--
                if (dmlService.J_ValidateConnection() == false)
                {
                    dmlService.Dispose();
                    //cmnService.J_UserMessage("Invalid database.\nPlease check the database", MessageBoxIcon.Exclamation);
                    SysServerInfoLocal frmLocal = new SysServerInfoLocal();
                    frmLocal.ShowDialog();
                    frmLocal.Dispose();
                    return;
                }
                //--
                //----------------------------------------------------------------------------------
                //MessageBox.Show("StandAlone_Network");
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                    if (Tdsman.T_SystemMaintenance() == false)
                        return;
                //if (CP.GET_TEMP_TABLES() == false)
                //    return;
                Tdsman.GetMachineID();
                Tdsman.GetSetup();
                Tdsman.SET_TEMP_TABLES();
                //-- APPLYING << RECORD LIMITATIONS >> BASED ON EDITIONS
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2014_15)
                {
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                        TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountProfessional;
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                        TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountStandard;
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE) //--2022/04/11
                        TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountClh;
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.LITE_EDITION)
                        TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountLite;
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION) //-- 2015/05/26
                        TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterprise;
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION) //-- 2021/01/13
                        TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterpriseLite;
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION) //-- 2024/06/06
                        TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterpriseUltimate;
                }
                //----------------------------------------------------------------------------------
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //--
                if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                    Tdsman.T_LoadLicensedMainForm();
                else
                    Tdsman.T_LoadTrialMainForm();
                //----------------------------------------------------------------------------------
            }
            #endregion

            #region ENTERPRISE EDITION or ENTERPRISE_LITE_EDITION
            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION)
            {
                #region SERVER_MACHINE
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                {
                    if (cmnService.J_IsFileExist(J_Var.J_pXmlConnectionFileName) == false)
                    {
                        SysServerInfoLocal frmLocal = new SysServerInfoLocal();
                        frmLocal.ShowDialog();
                        frmLocal.Dispose();
                        return;
                    }
                    //--
                    if (dmlService.J_ValidateConnection() == false)
                    {
                        dmlService.Dispose();
                        //cmnService.J_UserMessage("Invalid database.\nPlease check the database", MessageBoxIcon.Exclamation);
                        SysServerInfoLocal frmLocal = new SysServerInfoLocal();
                        frmLocal.ShowDialog();
                        frmLocal.Dispose();
                        return;
                    }
                    //--
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //--
                    TrnSplashScreen TrnSplashScreen = new TrnSplashScreen();
                    TrnSplashScreen.Show();
                    //--                    
                    //----------------------------------------------------------------------------------
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        if (Tdsman.T_SystemMaintenance() == false)
                            return;
                    //if (Tdsman.T_SystemMaintenance() == false)
                    //    return;
                    TrnSplashScreen.Close();
                    //
                    Tdsman.GetMachineID();
                    Tdsman.GetSetup();
                    Tdsman.SET_TEMP_TABLES();
                    //-- APPLYING << RECORD LIMITATIONS >> BASED ON EDITIONS
                    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2014_15)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountProfessional;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountStandard;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE) //--2022/04/11
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountClh;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.LITE_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountLite;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION) //-- 2015/05/26
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterprise;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION) //-- 2021/01/13
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterpriseLite;
                    }
                    //----------------------------------------------------------------------------------
                    //Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                        Tdsman.T_LoadLicensedMainForm();
                    else
                        Tdsman.T_LoadTrialMainForm();
                    //----------------------------------------------------------------------------------
                }
                #endregion
                //
                #region CLIENT_MACHINE
                else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                {
                    //MessageBox.Show("1");
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //--
                    Tdsman.T_LoadLicensedMainForm();
                }
                #endregion
            }
            else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)
            {
                #region SERVER_MACHINE
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                {
                    if (cmnService.J_IsFileExist(J_Var.J_pXmlConnectionFileName) == false)
                    {
                        SysServerInfoLocal frmLocal = new SysServerInfoLocal();
                        frmLocal.ShowDialog();
                        frmLocal.Dispose();
                        return;
                    }
                    //--
                    if (dmlService.J_ValidateConnection() == false)
                    {
                        dmlService.Dispose();
                        //cmnService.J_UserMessage("Invalid database.\nPlease check the database", MessageBoxIcon.Exclamation);
                        SysServerInfoLocal frmLocal = new SysServerInfoLocal();
                        frmLocal.ShowDialog();
                        frmLocal.Dispose();
                        return;
                    }
                    //--

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //--
                    TrnSplashScreen TrnSplashScreen = new TrnSplashScreen();
                    TrnSplashScreen.Show();
                    //--                    
                    //----------------------------------------------------------------------------------
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        if (Tdsman.T_SystemMaintenance() == false)
                            return;
                    //if (Tdsman.T_SystemMaintenance() == false)
                    //    return;
                    TrnSplashScreen.Close();
                    //
                    Tdsman.GetMachineID();
                    Tdsman.GetSetup();
                    Tdsman.SET_TEMP_TABLES();
                    //-- APPLYING << RECORD LIMITATIONS >> BASED ON EDITIONS
                    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2014_15)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountProfessional;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE) //--2022/04/11
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountClh;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountStandard;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.LITE_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountLite;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION) //-- 2015/05/26
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterprise;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION) //-- 2021/01/13
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterpriseLite;
                    }
                    //----------------------------------------------------------------------------------
                    //Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                        Tdsman.T_LoadLicensedMainForm();
                    else
                        Tdsman.T_LoadTrialMainForm();
                    //----------------------------------------------------------------------------------
                }
                #endregion
                //
                #region CLIENT_MACHINE
                else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                {
                    //MessageBox.Show("1");
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //--
                    Tdsman.T_LoadLicensedMainForm();
                }
                #endregion
            }
            else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
            {
                #region SERVER_MACHINE
                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                {
                    if (cmnService.J_IsFileExist(J_Var.J_pXmlConnectionFileName) == false)
                    {
                        SysServerInfoLocal frmLocal = new SysServerInfoLocal();
                        frmLocal.ShowDialog();
                        frmLocal.Dispose();
                        return;
                    }
                    //--
                    if (dmlService.J_ValidateConnection() == false)
                    {
                        dmlService.Dispose();
                        //cmnService.J_UserMessage("Invalid database.\nPlease check the database", MessageBoxIcon.Exclamation);
                        SysServerInfoLocal frmLocal = new SysServerInfoLocal();
                        frmLocal.ShowDialog();
                        frmLocal.Dispose();
                        return;
                    }
                    //--

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //--
                    TrnSplashScreen TrnSplashScreen = new TrnSplashScreen();
                    TrnSplashScreen.Show();
                    //--                    
                    //----------------------------------------------------------------------------------
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        if (Tdsman.T_SystemMaintenance() == false)
                            return;
                    //if (Tdsman.T_SystemMaintenance() == false)
                    //    return;
                    TrnSplashScreen.Close();
                    //
                    Tdsman.GetMachineID();
                    Tdsman.GetSetup();
                    Tdsman.SET_TEMP_TABLES();
                    //-- APPLYING << RECORD LIMITATIONS >> BASED ON EDITIONS
                    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2014_15)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountProfessional;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE) //--2022/04/11
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountClh;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountStandard;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.LITE_EDITION)
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountLite;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION) //-- 2015/05/26
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterprise;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION) //-- 2021/01/13
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterpriseLite;
                        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION) //-- 
                            TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterpriseUltimate;
                    }
                    //----------------------------------------------------------------------------------
                    //Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                        Tdsman.T_LoadLicensedMainForm();
                    else
                        Tdsman.T_LoadTrialMainForm();
                    //----------------------------------------------------------------------------------
                }
                #endregion
                //
                #region CLIENT_MACHINE
                else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                {
                    //MessageBox.Show("1");
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //--
                    Tdsman.T_LoadLicensedMainForm();
                }
                #endregion
            }
            #endregion
            //----------------------------------------------------------------------------------
            return;
        }
    }
}