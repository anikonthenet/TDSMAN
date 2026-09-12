
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
using System.Diagnostics;
using System.Threading;

using TDSMAN.Classes;
using TDSMAN.FormTrn;
using TDSMAN.FormCmn;
using TDSMAN.FormUtl;
using TDSMAN.FormPar;

#endregion

namespace TDSMAN.FormSys
{
    public partial class SysRegistration : Form
    {
        #region System Generated Code
        public SysRegistration()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        //--
        string strDestinationPath = "";
        string strFolderPath = "";
        //
        DMLService dmlService = new DMLService();
        DateService dtService = new DateService();
        CommonService cmnService = new CommonService();
        //JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        // WEB CLASS
        TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
        //--            
        IDataReader drdShowDeducteePAN = null;
        ToolTip tllTip = new ToolTip();
        //--           
        private DataSet dsDataGrid;
        private DataSet dsDataPrint;

        private DataGridView ViewGrid;
        private ComboBox SearchCombo;
        //----
        string strLicenseeName = "";
        string strCompanyName = "";
        string strEmail = "";
        string strMobile = "";
        //-- 2017/12/07
        string strPhone = "";
        string strFAX = "";
        string strAddress = "";
        string strCity = "";
        string strState = "";
        string strPIN = "";
        //
        string strActivationNumber = "";
        string strSQL = "";
        //
        int intInstallType = 0;
        //----
        bool J_blnConnected = true;
        bool blIs_FirstTime_Reg = true;
        //
        string strUserMessage = "Internet Connectivity not found";
        string strExeName = "TDSMAN";

        bool blnOpenTabPage = false;
        string strDatabaseLocation = "";
        string strServerSerialNo = "";
        string strMachineName = "";
        //
        long lngPackageFAYear = 0;
        long lngPackageNxtFAYear = 0;
        //
        string strPrevFAYearSerial_No = "";

        string strEmailBlog = "";
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        #endregion

        #region User Defined Events

        #region SysPage1_Load
        private void SysPage1_Load(object sender, EventArgs e)
        {
            if(TDSMAN.Classes.TDSMAN.T_pEditionType== T_EDITION_TYPE.STANDARD_EDITION)
                this.Text = "TDSMAN " + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear - 1) + "-" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + " Registration";
            else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                this.Text = "TDSMAN " + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear - 1) + "-" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + " (Pro) Registration";
            else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE)//--2022/04/11
                this.Text = "TDSMAN " + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear - 1) + "-" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + " (BETA) Registration";
            else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION)  //-- 2015/05/26
                this.Text = "TDSMAN " + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear - 1) + "-" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + " (Ent) Registration";
            else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)  //-- 2021/01/13
                this.Text = "TDSMAN " + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear - 1) + "-" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + " (Ent)-Lite Registration";
            else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)  //-- 2024/06/06
                this.Text = "TDSMAN " + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear - 1) + "-" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + " (Ent)-Ulti Registration";
            //--
            lblHeaderMessage.Text = "\n Welcome to TDSMAN \n Registration";
            //
            lblMessage1.Text = "The software needs to be registered before it can be used. \n\nSelect the process for registration from the following options then click Next:";
            lblNote1.Text = "NOTE : Online is an automatic Registration process. Offline process can be done either over phone or through internet access.";
            //lblMessage2.Text = "Provide the serial number as provided along with the software.";
            //lblMessage3.Text = "Provide the following information then click Next";
            //lblMessage4.Text = "For offline registration visit the following URL :";
            lblMessage4.Text = "To register and receive activation code : ";
            //lblMessage5.Text = " or call PDS Infotech at +91-33-22623535, 64596006.";
            //lblMessage5.Text = " or call PDS Infotech at +91-33-22623535, 98364 90007.";
            //lblMessage5.Text = " or call PDS Infotech at +91-33-22875500, 98364 90007.";
            //lblMessage6.Text = "Please note while Registering you need the Serial No. (provided along with software) and System Id as displayed below.";
            lblMessage5.Text = "• Visit ";
            lblMessage6.Text = "• Call PDS Infotech at +91-33-22875500, +91-33-40845500, 98364 90007";
            lblMessage9.Text = "Please enter the activation code once received and then click Next.";
            lblMessage7.Text = "TDSMAN has been successfully registered. \n Click Finish to Exit.";
            lblMessage8.Text = "The Serial No. is registered in the name of the licensee as mentioned below : ";
            //
            lblHelpLine.Text = "Helpline        : +91-33-22875500, +91-33-40845500, 98364 90007";
            lblHelpEmail.Text = "Email            : info@tdsman.com";
            lblUpdtDate.Text = "Version date  : " + TDSMAN.Classes.TDSMAN.T_pLastUpdtDate;
            //
            #region COMMENT
            //if(TdsMan.T_CheckInternetConnectivty() == true)
            //    rbnOnline.Checked = true;
            //else
            //    rbnOffline.Checked = true;
            //
            //if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
            //{
            //    blnOpenTabPage = true;
            //    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
            //    {
            //        lblHeaderMessage.Text = "\n Welcome to " + strExeName + " (Client) \n Registration";
            //        tbcRegistration.SelectTab(tbpClientMultiUser);
            //        //grpServerSerialNo.Enabled = false;
            //        //grpComputerName.Enabled = false;
            //        //txtMachineName.Text = System.Environment.MachineName;
            //    }
            //    else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
            //    {
            //        lblHeaderMessage.Text = "\n Welcome to " + strExeName + " (Server) \n Registration";
            //        tbcRegistration.SelectTab(tbpSelectType);
            //    }
            //    TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.MULTI_USER;
            //}
            //else if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.SINGLE_USER)
            //{
            //    lblHeaderMessage.Text = "\n Welcome to " + strExeName + " \n Registration";
            //    blnOpenTabPage = true;
            //    tbcRegistration.SelectTab(tbpSelectType);
            //    TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.SINGLE_USER;
            //    //-- 2013-12-19
            //    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
            //        TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.STANDARD_USER;
            //}     
            #endregion
            //
            if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == false)
            {
                pctVideoDemo.Visible = false;
                pctManual.Visible = false;
                //
                lblMessage2.Height = 36;
                //lblSerialNoCaption.Location = new Point(7, 102);
                //txtEnterSerialNoOnline.Location = new Point(88, 102);
                //lnkBuyNow.Location = new Point(350, 102);
                //
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                {
                    blnOpenTabPage = true;
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                    {
                        lblHeaderMessage.Text = "\n Welcome to " + strExeName + " (Client) \n Registration";
                        tbcRegistration.SelectTab(tbpClientMultiUser);
                        //grpServerSerialNo.Enabled = false;
                        //grpComputerName.Enabled = false;
                        //txtMachineName.Text = System.Environment.MachineName;
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                    {
                        lblHeaderMessage.Text = "\n Welcome to " + strExeName + " (Server) \n Registration";
                        tbcRegistration.SelectTab(tbpSelectType);
                    }
                    TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.MULTI_USER;
                }
                else if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.SINGLE_USER)
                {
                    pctVideoDemoSelectType.Visible = true;
                    pctManualSelectType.Visible = true;
                    //
                    lblHeaderMessage.Text = "\n Welcome to " + strExeName + " \n Registration";
                    blnOpenTabPage = true;
                    tbcRegistration.SelectTab(tbpSelectType);
                    TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.SINGLE_USER;
                    //-- 2013-12-19
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                        TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.STANDARD_USER;
                }
                //
                lngPackageFAYear = TDSMAN.Classes.TDSMAN.T_pPackageFAYear;
            }
            else //-- UPGRADATION FROM MENU
            {
                pctVideoDemo.Visible = true;
                pctManual.Visible = true;
                //
                //lblMessage2.Text = "Once you have the license key for TDSMAN (" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear) + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1).ToString() + "), you may:\n" +
                //                   "> either install it separately, or\n" +lblStep1of2.Visible
                //                   "> upgrade the existing TDSMAN (" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear - 1) + "-" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + ") to TDSMAN (" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear) + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1).ToString() + ")\n\n" +
                //                   "Upgrading automatically retains all data of earlier financial years.\n" +
                //                   "Follow the instruction below for upgrade to TDSMAN (" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear) + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1).ToString() + ").\n\n" +
                //                   "-------------------------------------------------------------------------------------------------------------------------------------------------------\n" +
                //                   "Provide the serial number as provided along with the software.\n";
                //lblMessage2Upgradation.Font = new Font(lblMessage2Upgradation.Font, FontStyle.Regular);
                lblMessage2Upgradation.Text = "First, purchase TDSMAN (" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear) + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1).ToString() + ") to receive your new serial number:";
                lblMessage2Upgradation.Location = new Point(9, 10);
                lblMessage2Upgradation.Width = 390;
                lblMessage2Upgradation.Visible = true;
                //
                lnkBuyNow.Visible = true;
                lnkBuyNow.Location = new Point(400, 11);
                lnkBuyNow.Text = "Buy TDSMAN (" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear) + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1).ToString() + ")";
                //
                lblMessage2.Location = new Point(9, 45);
                lblMessage2.Height = 80;
                //lblMessage2.Font = new Font(lblMessage2.Font, FontStyle.Regular);
                lblMessage2.Visible = true;
                lblMessage2.Text = "Once you have the Serial Number for TDSMAN (" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear) + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1).ToString() + "), you may:\n\n" +
                   "• Either install the software separately OR\n\n" +
                   "• Upgrade the existing TDSMAN (" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear - 1) + "-" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + ") to TDSMAN (" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear) + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1).ToString() + ") (This automatically retains all data of earlier financial years)\n\n" +
                   "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n";
                //
                lblMessage2UpgradationTitle.Visible = true;
                lblMessage2UpgradationTitle.Location = new Point(9, 125);
                lblMessage2UpgradationTitle.Text = "TDSMAN (" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear) + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1).ToString() + ") Upgrade Process";
                //
                lblStep1of2.Visible = true;
                lblStep1of2.Location = new Point(9, 150);
                ////
                lblEnterSerialNo.Visible = true;
                lblEnterSerialNo.Location = new Point(9, 170);
                lblEnterSerialNo.Text = "Enter the Serial Number as provided along with the software and click Next.";
                //
                lblSerialNoCaption.Visible = true;
                lblSerialNoCaption.Location = new Point(9, 198);
                txtEnterSerialNoOnline.Visible = true;
                txtEnterSerialNoOnline.Location = new Point(100, 198);
                //
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                    this.Text = "TDSMAN " + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1) + " Registration";
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                    this.Text = "TDSMAN " + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1) + " (Pro) Registration";
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE)//--2022/04/11
                    this.Text = "TDSMAN " + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1) + " (BETA) Registration";
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION)  //-- 2015/05/26
                    this.Text = "TDSMAN " + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1) + " (Ent) Registration";
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)  //-- 2021/01/13
                    this.Text = "TDSMAN " + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + "-" + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1) + " (Ent)-Lite Registration";
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)  //-- 2024/06/06
                    this.Text = "TDSMAN " + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear - 1) + "-" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + " (Ent)-Ulti Registration";
                //###################
                lngPackageFAYear = TDSMAN.Classes.TDSMAN.T_pPackageFAYear;
                lngPackageNxtFAYear = TDSMAN.Classes.TDSMAN.T_pPackageFAYear + 1;
                txtEnterConfigurationNoOnline.Text = TdsMan.T_GenerateNewConfigNo(DateTime.Now.ToString());
                //
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                {
                    blnOpenTabPage = true;
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                    {
                        lblHeaderMessage.Text = "\n Welcome to " + strExeName + " (Client) \n Registration";
                        tbcRegistration.SelectTab(tbpClientMultiUser);
                        //grpServerSerialNo.Enabled = false;
                        //grpComputerName.Enabled = false;
                        //txtMachineName.Text = System.Environment.MachineName;
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                    {
                        lblHeaderMessage.Text = "\n Welcome to " + strExeName + " (Server) \n Registration";
                        tbcRegistration.SelectTab(tbpOnline1);
                    }
                    TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.MULTI_USER;
                }
                else if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.SINGLE_USER)
                {
                    lblHeaderMessage.Text = "\n Welcome to " + strExeName + " \n Registration";
                    blnOpenTabPage = true;
                    tbcRegistration.SelectTab(tbpOnline1);
                    TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.SINGLE_USER;
                    //-- 2013-12-19
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                        TDSMAN.Classes.TDSMAN.T_SoftwareProductID = T_SOFTWARE_PRODUCT_ID.STANDARD_USER;
                }
            }

            BtnBack.Enabled = false;
            BtnBack.BackColor = Color.LightGray;
        }
        #endregion

        #region BtnNext_Click
        private void BtnNext_Click(object sender, EventArgs e)
        {
            string strSerialNoCurrentFY = "";
            try
            {
                #region tbpSelectDatabaseFolder
                if (tbcRegistration.SelectedTab == tbpClientMultiUser)
                {
                    this.Cursor = Cursors.WaitCursor;
                    //
                    lblPleaseWaitMessage5.Visible = true;
                    //
                    this.Refresh();
                    //-- VALIDATE DATABASE
                    if (txtDatabaseLocation.Text.Trim() == "")
                    {
                        this.Cursor = Cursors.Default;
                        lblPleaseWaitMessage5.Visible = false;
                        cmnService.J_UserMessage("Database file not selected !!");
                        btnBrowseDatabaseFolder.Select();
                        return;
                    }
                    //

                    if (TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_LITE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)//-- 2016/02/03
                    {
                        if (File.Exists(Path.Combine(strDatabaseLocation, J_Var.J_pMsAccessDatabaseName)) == false)
                        {
                            this.Cursor = Cursors.Default;
                            lblPleaseWaitMessage5.Visible = false;
                            cmnService.J_UserMessage("Database file not found !!");
                            btnBrowseDatabaseFolder.Select();
                            return;
                        }
                    }
                    //-- VERIFY SERVER'S SERIAL NUMBER
                    #region VERIFY SERVER'S SERIAL NUMBER
                    if (txtServerSerialNo.Text == "")
                    {
                        this.Cursor = Cursors.Default;
                        lblPleaseWaitMessage5.Visible = false;
                        cmnService.J_UserMessage("Enter Server Serial No.");
                        txtServerSerialNo.Select();
                        return;
                    }
                    else
                    {
                        DataSet dtSet = null;
                        if (dtSet != null) dtSet.Clear();
                        dtSet = dmlService.J_ConvertXmlToDataSet(Path.Combine(strDatabaseLocation, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer));
                        if (dtSet == null)
                        {
                            dmlService.Dispose();
                            //
                            this.Cursor = Cursors.Default;
                            lblPleaseWaitMessage5.Visible = false;
                            cmnService.J_UserMessage("Could Not Connect to the Server");
                            txtServerSerialNo.Select();
                            return;
                        }
                        //
                        //if (txtServerSerialNo.Text.Trim().ToUpper() != cmnService.J_Decode(dtSet.Tables[0].Rows[0][cmnService.J_Encode("SERIALNO")].ToString()).ToUpper())
                        //string str = dtSet.Tables[0].Rows[0][T_XML.SERIAL].ToString().ToUpper();
                        if (txtServerSerialNo.Text.Trim().ToUpper() != cmnService.J_Decode(dtSet.Tables[0].Rows[0][T_XML.SERIAL].ToString()).ToUpper())
                        {
                            this.Cursor = Cursors.Default;
                            lblPleaseWaitMessage5.Visible = false;
                            cmnService.J_UserMessage("Incorrect Server Serial No.", MessageBoxIcon.Exclamation);
                            txtServerSerialNo.Select();
                            return;
                        }
                    }
                    #endregion

                    //-- CHECK MACHINE NAME IN XML FILE
                    #region CHECK MACHINE NAME IN XML FILE
                    if (txtMachineName.Text == "")
                    {
                        this.Cursor = Cursors.Default;
                        lblPleaseWaitMessage5.Visible = false;
                        cmnService.J_UserMessage("Enter Name for this machine");
                        txtMachineName.Select();
                        return;
                    }
                    else
                    {
                        if (txtMachineName.Text.Trim().ToUpper() == "SERVER")
                        {
                            this.Cursor = Cursors.Default;
                            lblPleaseWaitMessage5.Visible = false;
                            cmnService.J_UserMessage("Enter some other Machine Name other than [" + txtMachineName.Text.Trim() + "]");
                            txtMachineName.Select();
                            return;
                        }
                        //--
                        if (File.Exists(Path.Combine(strDatabaseLocation, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer)) == true)
                        {
                            //-- CHECK ALLOWED NODES
                            int AllowedNodesXML = cmnService.J_ReturnInt32Value(TdsMan.GetAllowedVersionXMLFileForSERVER(Path.Combine(strDatabaseLocation, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer), T_XML.ALLOWEDNODES));
                            //
                            if (TdsMan.AllowClientRegistration(Path.Combine(strDatabaseLocation, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer), AllowedNodesXML) == false)
                            {
                                this.Cursor = Cursors.Default;
                                lblPleaseWaitMessage5.Visible = false;
                                cmnService.J_UserMessage("No further client installations are allowed in the server.\n To use this machine you have detach one of the client from 'Client Management' module present in the server");
                                return;
                            }
                            //-- CHECK DUPLICATE MACHINE NAME
                            if (TdsMan.MatchMachineName(Path.Combine(strDatabaseLocation, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer), txtMachineName.Text.Trim()) == false)
                            {
                                this.Cursor = Cursors.Default;
                                lblPleaseWaitMessage5.Visible = false;
                                cmnService.J_UserMessage("The machine name [" + txtMachineName.Text.Trim() + "] has already been alloted to one of the client machines installed in this server.\n Please provide a unique name for this machine.");
                                txtMachineName.Select();
                                return;
                            }
                            //-- CHECK EDITION
                            if (TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_LITE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                                TDSMAN.Classes.TDSMAN.T_pEditionType = cmnService.J_ReturnInt16Value(TdsMan.GetServerEdition());
                            //
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            lblPleaseWaitMessage5.Visible = false;
                            cmnService.J_UserMessage("Server config file not found !!");
                            return;
                        }
                    }
                    #endregion
                    //--
                    strServerSerialNo = txtServerSerialNo.Text.Trim();
                    strMachineName = txtMachineName.Text.Trim();
                    //
                    this.Cursor = Cursors.Default;
                    //
                    lblPleaseWaitMessage5.Visible = false;
                    ////
                    this.Refresh();
                    ////--
                    lblHeaderMessage.Text = "\n   Registration Complete";
                    BtnNext.Text = "&Finish";
                    BtnBack.Enabled = false;
                    BtnBack.BackColor = Color.LightGray;
                    BtnCancel.Enabled = false;
                    BtnCancel.BackColor = Color.LightGray;
                    blnOpenTabPage = true;
                    tbcRegistration.SelectTab(tbpSucessfullyRegistered);
                    //
                }
                #endregion
                //--
                #region tbpSelectType
                else if (tbcRegistration.SelectedTab == tbpSelectType)
                {
                    if (rbnOffline.Checked == true)
                    {
                        // ---------------------------------------
                        //OFFLINE REGISTRATION 1ST PAGE
                        // ---------------------------------------

                        this.Cursor = Cursors.WaitCursor;
                        //
                        lblPleaseWaitMessage1.Visible = true;
                        //
                        this.Refresh();

                        txtConfigurationNoOffline.Text = TdsMan.T_GenerateNewConfigNo(DateTime.Now.ToString());
                        //
                        blnOpenTabPage = true;
                        tbcRegistration.SelectTab(tbpOffline1);
                        //
                        lblHeaderMessage.Text = "\n Offline Registration Process";
                        //
                        txtActivationCodeOffline.Select();
                        //
                        BtnBack.Enabled = true;
                        BtnBack.BackColor = Color.Lavender;
                        BtnNext.Text = "&Next >";
                        //
                        this.Cursor = Cursors.Default;
                        //                    
                    }
                    else if (rbnOnline.Checked == true)
                    {
                        //-----------------------------------------------
                        this.Cursor = Cursors.WaitCursor;

                        // -----------------------------------------
                        // -- ONLINE STEP 1 -- SERIAL NO ENTRY SCREEN
                        // -----------------------------------------

                        //
                        lblPleaseWaitMessage1.Visible = true;
                        //
                        this.Refresh();

                        txtEnterConfigurationNoOnline.Text = TdsMan.T_GenerateNewConfigNo(DateTime.Now.ToString());
                        //
                        blnOpenTabPage = true;
                        tbcRegistration.SelectTab(tbpOnline1);
                        //
                        lblHeaderMessage.Text = "\n Online Registration Process";
                        //
                        txtEnterSerialNoOnline.Select();
                        //
                        BtnBack.Enabled = true;
                        BtnBack.BackColor = Color.Lavender;
                        BtnNext.Text = "&Next >";
                        //
                        this.Cursor = Cursors.Default;
                        //
                        lblPleaseWaitMessage1.Visible = false;
                    }
                }
                #endregion

                #region tbpOffline1
                else if (tbcRegistration.SelectedTab == tbpOffline1)
                {

                    // ---------------------------------------
                    // OFFLINE REGISTRATION 2ND PAGE (VERIFYING THE ACTIVATION NUM ENTERED
                    // ---------------------------------------


                    if (ValidateFields() == false) return;
                    //
                    blnOpenTabPage = true;
                    tbcRegistration.SelectTab(tbpOffline2);
                    //
                    lblHeaderMessage.Text = "\n Offline Registration Process";
                    //
                    txtEnterSerialNoOffline.Select();
                    BtnNext.Text = "&Next >";
                }
                #endregion

                #region tbpOffline2
                else if (tbcRegistration.SelectedTab == tbpOffline2)
                {

                    // ------------------------------------------
                    // -- SUCCESSFULL OFFLINE REGISTRATION
                    // ------------------------------------------

                    if (ValidateFields() == false) return;
                    //
                    if (T_WriteToRegistry(txtEnterSerialNoOffline.Text,
                                          txtLicenseeNameOffline.Text,
                                          Convert.ToString(dtService.J_ConvertddMMyyyy(System.DateTime.Now.ToString())),
                                          txtActivationCodeOffline.Text) == true)
                    {
                        //
                        blnOpenTabPage = true;
                        tbcRegistration.SelectTab(tbpSucessfullyRegistered);
                        //
                        lblHeaderMessage.Text = "\n   Registration Complete";
                        BtnNext.Text = "&Finish";
                        BtnBack.Enabled = false;
                        BtnBack.BackColor = Color.LightGray;
                        BtnCancel.Enabled = false;
                        BtnCancel.BackColor = Color.LightGray;
                    }
                    else
                    {
                        cmnService.J_UserMessage("REGISTRATION FAILED", MessageBoxIcon.Error);
                    }
                }
                #endregion

                #region tbpSucessfullyRegistered
                else if (tbcRegistration.SelectedTab == tbpSucessfullyRegistered)
                {
                    // -----------------------------------
                    // -- SUCCESSFULLY REGISTERED
                    // -----------------------------------
                    if (ValidateFields() == false) return;
                    //
                    ////-- 2025/04/22
                    //if (strEmailBlog != "")
                    //{
                    //    if (TdsMan.T_CheckInternetConnectivty() == true)
                    //    {
                    //        TrnSubscribeBlogMimibrowser objBrow = new TrnSubscribeBlogMimibrowser();
                    //        objBrow.Email = strEmailBlog;
                    //        objBrow.Show();
                    //        //--
                    //        Registration.SaveBlogDetails(TDSMAN.Classes.TDSMAN.T_pVersionType, TDSMAN.Classes.TDSMAN.T_pTDSMANSerialNo.ToString(), strEmailBlog);
                    //        if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                    //        {
                    //            //--
                    //            string strTrialVersionRegistryPath = TdsMan.GetRegistryFolder() + "(Trial)";
                    //            cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, strTrialVersionRegistryPath,
                    //                                            T_RegistrationInfo.Blog_email_flag.ToString(),
                    //                                            T_YES_NO.YES.ToString());
                    //        }
                    //        else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                    //        {
                    //            cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                    //                                            T_RegistrationInfo.Blog_email_flag.ToString(),
                    //                                            T_YES_NO.YES.ToString());
                    //        }
                    //        //--
                    //        System.Threading.Thread.Sleep(3000);
                    //        //objBrow.Close();
                    //        //objBrow.Dispose();
                    //    }
                    //}
                    //--
                    //strSerialNoCurrentFY = txtEnterSerialNoOnline.Text;
                    string STR1 = strPrevFAYearSerial_No;
                    //
                    this.Close();
                    this.Dispose();
                    //
                    #region MULTI-USER -- COMMENTED 2016-02-03
                    //if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                    //{
                    //    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                    //    {
                    //        //-- WRITE TO REGISTRY
                    //        //GENERATE RANDOM NUMBER
                    //        string MachineRndmNumbr = TdsMan.T_GenerateRandomNumber();
                    //        //
                    //        if (T_WriteToRegistry(strServerSerialNo,
                    //                            strMachineName,
                    //                            System.DateTime.Now.Date.ToString("dd/MM/yyyy"),
                    //                            MachineRndmNumbr) == true)
                    //        {
                    //            //--
                    //            if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                    //                if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                    //                    TdsMan.RenameDatabase(J_Var.J_pMsAccessDatabaseName);
                    //            //--
                    //            //-- WRITE XML FILE
                    //            //=================================================================
                    //            Hashtable nameValue = new Hashtable();
                    //            //=================================================================
                    //            nameValue.Add("SERVERNAME", "");
                    //            nameValue.Add("DATABASENAME", strDatabaseLocation);
                    //            nameValue.Add("USERNAME", J_Var.J_pMsAccessDatabaseName);
                    //            nameValue.Add("PASSWORD", J_Var.J_pMsAccessDatabasePassword);
                    //            //=================================================================
                    //            XMLService objxml = new XMLService();
                    //            objxml.J_CreateXMLFile(nameValue, Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
                    //            //=================================================================  
                    //            TdsMan.InsertMachineSrlNo(strMachineName, MachineRndmNumbr);
                    //            //--
                    //            //-- WRITE TO SERVER'S XML FILE (MACHINE NAME & NUMBER)
                    //            ////GENERATE RANDOM NUMBER
                    //            //string MachineRndmNumbr = ChequePrinting.T_GenerateRandomNumber();
                    //            //
                    //            TdsMan.UpdateXMLFileForCLIENT(Path.Combine(strDatabaseLocation, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer), strMachineName, MachineRndmNumbr);
                    //            //
                    //        }//--
                    //        if (cmnService.J_IsFileExist(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName) == false)
                    //        {
                    //            dmlService.Dispose();
                    //            //cmnService.J_UserMessage("Database file does not exist.\nPlease select the database location", MessageBoxIcon.Exclamation);
                    //            //--
                    //            //SysSelectServerDatabase frm = new SysSelectServerDatabase();
                    //            //frm.ShowDialog();
                    //            //frm.Dispose();
                    //            SysServerDisconnected SysServerDisconnected = new SysServerDisconnected();
                    //            SysServerDisconnected.StartPosition = FormStartPosition.CenterScreen;
                    //            SysServerDisconnected.ShowDialog();
                    //            //--
                    //            return;
                    //        }
                    //        //--
                    //        if (dmlService.J_ValidateConnection() == false)
                    //        {
                    //            dmlService.Dispose();
                    //            //cmnService.J_UserMessage("Invalid database.\nPlease check the database", MessageBoxIcon.Exclamation);
                    //            ////--
                    //            ////Application.EnableVisualStyles();
                    //            ////Application.SetCompatibleTextRenderingDefault(false);
                    //            ////
                    //            //SysSelectServerDatabase frm = new SysSelectServerDatabase();
                    //            //frm.ShowDialog();
                    //            //frm.Dispose();
                    //            SysServerDisconnected SysServerDisconnected = new SysServerDisconnected();
                    //            SysServerDisconnected.StartPosition = FormStartPosition.CenterScreen;
                    //            SysServerDisconnected.ShowDialog();
                    //            //--
                    //            return;
                    //        }
                    //        //--
                    //        if (dmlService.J_IsDatabaseObjectExist("MST_SETUP") == false)
                    //        {
                    //            dmlService.Dispose();
                    //            //cmnService.J_UserMessage("Invalid database structure.\nPlease check the database", MessageBoxIcon.Exclamation);
                    //            ////--
                    //            //SysSelectServerDatabase frm = new SysSelectServerDatabase();
                    //            //frm.ShowDialog();
                    //            //frm.Dispose();
                    //            SysServerDisconnected SysServerDisconnected = new SysServerDisconnected();
                    //            SysServerDisconnected.StartPosition = FormStartPosition.CenterScreen;
                    //            SysServerDisconnected.ShowDialog();
                    //            //--
                    //            return;
                    //        }
                    //        //--
                    //        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                    //            if (TdsMan.T_SystemMaintenance() == false)
                    //                return;
                    //        //--
                    //        //if (ChequePrinting.GET_TEMP_TABLES() == false)
                    //        //    return;
                    //        TdsMan.GetMachineID();
                    //        TdsMan.SET_TEMP_TABLES();
                    //        //--
                    //        //-- CHECK EDITION
                    //        TDSMAN.Classes.TDSMAN.T_pEditionType = cmnService.J_ReturnInt16Value(TdsMan.GetServerEdition());
                    //        //
                    //        #region SERVER'S VERSION
                    //        //-- GET SERVER'S VERSION
                    //        System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(Path.Combine(Application.StartupPath, J_Var.J_pProjectName + ".EXE"));
                    //        double ClientVersion = Convert.ToDouble(fileVersionInfo.FileMajorPart + "." + fileVersionInfo.FileMinorPart);
                    //        double ServerVersion = Convert.ToDouble(TdsMan.GetServerVersion());
                    //        //
                    //        if (ServerVersion > 0)
                    //        {
                    //            if (ClientVersion > ServerVersion)
                    //            {
                    //                cmnService.J_UserMessage("SERVER application is of the previous version.\n Kindly update the SERVER application.");
                    //                return;
                    //            }
                    //            else if (ClientVersion < ServerVersion)
                    //            {
                    //                if (cmnService.J_UserMessage("This application file is of the previous version.\n Click on 'Yes' to update the file now or click 'No' to exit the software.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //                {
                    //                    //    if (File.Exists(Path.Combine(Application.StartupPath, "eLicensedUpdateApplication.EXE")) == false)
                    //                    //    {
                    //                    //        TDSMAN.Classes.TDSMAN.MDIExit = true;
                    //                    //        //
                    //                    //        //--
                    //                    //        Application.Exit();
                    //                    //        //
                    //                    //        UtUpdateApplicationUpdate objUpdateUpdaterexe = new UtUpdateApplicationUpdate();
                    //                    //        objUpdateUpdaterexe.ShowDialog();
                    //                    //        return;
                    //                    //    }
                    //                    //}
                    //                    //else
                    //                    //    return;
                    //                    if (TdsMan.T_CheckServerTDSMAN() == true)
                    //                    {
                    //                        //checking if the latest updator file exists
                    //                        if (File.Exists(Path.Combine(Application.StartupPath, "eLicensedUpdateApplication.EXE")) == false)
                    //                        {
                    //                            UtUpdateApplicationUpdate objUpdateUpdaterexe = new UtUpdateApplicationUpdate();
                    //                            objUpdateUpdaterexe.ShowDialog();
                    //                        }
                    //                        else
                    //                        {
                    //                            //if file exists

                    //                            //chekcing the version of the file
                    //                            FileVersionInfo fileVersionInfoU = FileVersionInfo.GetVersionInfo(Path.Combine(Application.StartupPath, "eLicensedUpdateApplication.EXE"));
                    //                            double dblUpdatatorexeVersion = Convert.ToDouble(fileVersionInfoU.FileMajorPart + "." + fileVersionInfoU.FileMinorPart);

                    //                            //now checking the latest update available and downloading the same
                    //                            if (Registration.Get_Latest_Updater_version(dblUpdatatorexeVersion) == true)
                    //                            {
                    //                                UtUpdateApplicationUpdate objUpdateUpdaterexe = new UtUpdateApplicationUpdate();
                    //                                objUpdateUpdaterexe.ShowDialog();
                    //                            }
                    //                        }
                    //                    }
                    //                    //--
                    //                    //Creating a File with Version and Financial Year
                    //                    StreamWriter writer = new StreamWriter(Path.Combine(Application.StartupPath, "TempUpdate.txt"));

                    //                    writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pVersionType.ToString());
                    //                    writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString());
                    //                    writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pEditionType.ToString());
                    //                    writer.WriteLine(TDSMAN.Classes.TDSMAN.T_ClientServerMachine.ToString()); //-- ANIK 2015-01-30

                    //                    writer.Flush();
                    //                    writer.Dispose();
                    //                    writer.Close();
                    //                    //--
                    //                    if (File.Exists(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication) == true)
                    //                    {
                    //                        dmlService.Dispose();
                    //                        //this.Close();
                    //                        //this.Dispose();
                    //                        //J_Var.frmMain.Close();
                    //                        //J_Var.frmMain.Dispose();
                    //                        TDSMAN.Classes.TDSMAN.MDIExit = true;
                    //                        Application.Exit();
                    //                        Process.Start(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication);
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            cmnService.J_UserMessage("Due to some techinical error the server version is not getting accessed.\n Kindly call us on 033-22623535/64596006 for further assistance.");
                    //            return;
                    //        }
                    //        #endregion
                    //    }
                    //}
                    #endregion
                    //
                    #region MULTI-USER
                    if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            //-- WRITE TO REGISTRY
                            //GENERATE RANDOM NUMBER
                            string MachineRndmNumbr = TdsMan.T_GenerateRandomNumber();
                            //
                            if (T_WriteToRegistry(strServerSerialNo,
                                                strMachineName,
                                                System.DateTime.Now.Date.ToString("dd/MM/yyyy"),
                                                MachineRndmNumbr) == true)
                            {
                                //--
                                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                                        TdsMan.RenameDatabase(J_Var.J_pMsAccessDatabaseName);
                                //--
                                //-- GET SERVER EDITION
                                //DataSet dset = new DataSet();
                                //dset = dmlService.J_ConvertXmlToDataSet(Path.Combine(strDatabaseLocation, J_Var.J_pXmlConnectionFileName));
                                //if (cmnService.J_Decode(dset.Tables[0].Rows[0][cmnService.J_Encode("VERSION")].ToString()) == "")
                                //    TDSMAN.Classes.TDSMAN.T_pEditionType = 0;
                                //else
                                //    TDSMAN.Classes.TDSMAN.T_pEditionType = Convert.ToInt32(cmnService.J_Decode(dset.Tables[0].Rows[0][cmnService.J_Encode("VERSION")].ToString()));
                                //-- WRITE XML FILE
                                //=================================================================
                                Hashtable nameValue = new Hashtable();
                                //=================================================================
                                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                                {
                                    DataSet dsetEnterprise = new DataSet();
                                    dsetEnterprise = dmlService.J_ConvertXmlToDataSet(Path.Combine(strDatabaseLocation, J_Var.J_pXmlConnectionFileName));
                                    nameValue.Add("SERVERNAME", cmnService.J_Decode(dsetEnterprise.Tables[0].Rows[0][cmnService.J_Encode("SERVERNAME")].ToString()));
                                    nameValue.Add("DATABASENAME", cmnService.J_Decode(dsetEnterprise.Tables[0].Rows[0][cmnService.J_Encode("DATABASENAME")].ToString()));
                                    nameValue.Add("USERNAME", cmnService.J_Decode(dsetEnterprise.Tables[0].Rows[0][cmnService.J_Encode("USERNAME")].ToString()));
                                    nameValue.Add("PASSWORD", cmnService.J_Decode(dsetEnterprise.Tables[0].Rows[0][cmnService.J_Encode("PASSWORD")].ToString()));
                                    nameValue.Add("SERVERPATH", cmnService.J_Decode(cmnService.J_Encode(strDatabaseLocation).ToString()));
                                    //
                                    J_Var.J_pDatabaseType = J_DatabaseType.SqlServer;
                                    J_Var.J_pConnectionProviderType = J_ConnectionProviderType.Sql;
                                    J_Var.J_pApplicationType = J_ApplicationType.StandAlone_Network;
                                }
                                else
                                {
                                    nameValue.Add("SERVERNAME", "");
                                    nameValue.Add("DATABASENAME", strDatabaseLocation);
                                    nameValue.Add("USERNAME", J_Var.J_pMsAccessDatabaseName);
                                    nameValue.Add("PASSWORD", J_Var.J_pMsAccessDatabasePassword);
                                }
                                //=================================================================
                                XMLService objxml = new XMLService();
                                objxml.J_CreateXMLFile(nameValue, Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
                                //=================================================================  
                                TdsMan.InsertMachineSrlNo(strMachineName, MachineRndmNumbr);
                                //--
                                //-- WRITE TO SERVER'S XML FILE (MACHINE NAME & NUMBER)
                                ////GENERATE RANDOM NUMBER
                                //string MachineRndmNumbr = ChequePrinting.T_GenerateRandomNumber();
                                //
                                TdsMan.UpdateXMLFileForCLIENT(Path.Combine(strDatabaseLocation, TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer), strMachineName, MachineRndmNumbr);
                                //--
                                #region TRN_USER_MENU_ACCESS
                                //if (dmlService.J_IsDatabaseObjectExist("TRN_USER_MENU_ACCESS") == false)
                                //{
                                //    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION)
                                //    {
                                //        strSQL = "CREATE TABLE TRN_USER_MENU_ACCESS (" +
                                //                "                USER_MENU_ACCESS_ID BIGINT IDENTITY(1,1)," +
                                //                "                SETUP_ID            BIGINT DEFAULT 0," +
                                //                "                CHILD_MENU_ID       BIGINT DEFAULT 0)";
                                //        dmlService.J_ExecSql(strSQL);
                                //    }
                                //    else
                                //    {
                                //        strSQL = "CREATE TABLE TRN_USER_MENU_ACCESS (" +
                                //                "                USER_MENU_ACCESS_ID COUNTER," +
                                //                "                SETUP_ID            LONG DEFAULT 0," +
                                //                "                CHILD_MENU_ID       LONG DEFAULT 0)";
                                //        dmlService.J_ExecSql(strSQL);
                                //    }
                                //}
                                if (dmlService.J_IsDatabaseObjectExist("TRN_USER_MENU_ACCESS") == true)
                                {
                                    //-- INSERT DATA TO TRN_USER_MENU_ACCESS
                                    long lngCount = dmlService.J_ReturnNoOfRows("MST_SETUP");
                                    if (lngCount > 1)
                                    {
                                        //string[] strSetupID = new string[lngCount];
                                        //IDataReader drdGetUser = null;
                                        //strSQL = "SELECT SETUPID, MACHINE_NAME FROM MST_SETUP WHERE SETUPID = " + MachineRndmNumbr;
                                        //drdGetUser = dmlService.J_ExecSqlReturnReader(strSQL);
                                        //if (drdGetUser != null)
                                        //{
                                        //    int intCounter = 0;
                                        //    while (drdGetUser.Read())
                                        //    {
                                        //        strSetupID[intCounter] = drdGetUser["SETUPID"].ToString();
                                        //        intCounter++;
                                        //    }
                                        //    drdGetUser.Close();
                                        //    drdGetUser.Dispose();
                                        //}
                                        //--
                                        //for (int intCounter = 1; intCounter <= strSetupID.GetUpperBound(0); intCounter++)
                                        //{
                                        for (int i = 1; i < 95; i++)
                                        {
                                            strSQL = "INSERT INTO TRN_USER_MENU_ACCESS ( SETUP_ID, CHILD_MENU_ID)VALUES (" + MachineRndmNumbr + "," + i + ")";
                                            dmlService.J_ExecSql(strSQL);
                                        }
                                        //}
                                    }
                                }
                                //}
                                #endregion
                                //--
                            }
                            //--
                            if (cmnService.J_IsFileExist(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName) == false)
                            {
                                dmlService.Dispose();
                                //cmnService.J_UserMessage("Database file does not exist.\nPlease select the database location", MessageBoxIcon.Exclamation);
                                //--
                                //SysSelectServerDatabase frm = new SysSelectServerDatabase();
                                //frm.ShowDialog();
                                //frm.Dispose();
                                SysServerDisconnected SysServerDisconnected = new SysServerDisconnected();
                                SysServerDisconnected.StartPosition = FormStartPosition.CenterScreen;
                                SysServerDisconnected.ShowDialog();
                                //--
                                return;
                            }
                            //--
                            DMLService dmlService1 = new DMLService();
                            if (dmlService1.J_ValidateConnection() == false)
                            {
                                dmlService1.Dispose();
                                //cmnService.J_UserMessage("Invalid database.\nPlease check the database", MessageBoxIcon.Exclamation);
                                ////--
                                ////Application.EnableVisualStyles();
                                ////Application.SetCompatibleTextRenderingDefault(false);
                                ////
                                //SysSelectServerDatabase frm = new SysSelectServerDatabase();
                                //frm.ShowDialog();
                                //frm.Dispose();
                                SysServerDisconnected SysServerDisconnected = new SysServerDisconnected();
                                SysServerDisconnected.StartPosition = FormStartPosition.CenterScreen;
                                SysServerDisconnected.ShowDialog();
                                //--
                                return;
                            }
                            //--
                            if (dmlService1.J_IsDatabaseObjectExist("MST_SETUP") == false)
                            {
                                dmlService1.Dispose();
                                //cmnService.J_UserMessage("Invalid database structure.\nPlease check the database", MessageBoxIcon.Exclamation);
                                ////--
                                //SysSelectServerDatabase frm = new SysSelectServerDatabase();
                                //frm.ShowDialog();
                                //frm.Dispose();
                                SysServerDisconnected SysServerDisconnected = new SysServerDisconnected();
                                SysServerDisconnected.StartPosition = FormStartPosition.CenterScreen;
                                SysServerDisconnected.ShowDialog();
                                //--
                                return;
                            }
                            //--
                            if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                                if (TdsMan.T_SystemMaintenance() == false)
                                    return;
                            //--
                            //if (ChequePrinting.GET_TEMP_TABLES() == false)
                            //    return;
                            TdsMan.GetMachineID();
                            TdsMan.SET_TEMP_TABLES();
                            //--
                            //-- CHECK EDITION
                            if (TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_LITE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                                TDSMAN.Classes.TDSMAN.T_pEditionType = cmnService.J_ReturnInt16Value(TdsMan.GetServerEdition());
                            //
                            #region SERVER'S VERSION
                            //-- GET SERVER'S VERSION
                            System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(Path.Combine(Application.StartupPath, J_Var.J_pProjectName + ".EXE"));
                            double ClientVersion = Convert.ToDouble(fileVersionInfo.FileMajorPart + "." + fileVersionInfo.FileMinorPart);
                            double ServerVersion = Convert.ToDouble(TdsMan.GetServerVersion());
                            //
                            if (ServerVersion > 0)
                            {
                                if (ClientVersion > ServerVersion)
                                {
                                    cmnService.J_UserMessage("SERVER application is of the previous version.\n Kindly update the SERVER application.");
                                    return;
                                }
                                else if (ClientVersion < ServerVersion)
                                {
                                    if (cmnService.J_UserMessage("This application file is of the previous version.\n Click on 'Yes' to update the file now or click 'No' to exit the software.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        //    if (File.Exists(Path.Combine(Application.StartupPath, "eLicensedUpdateApplication.EXE")) == false)
                                        //    {
                                        //        TDSMAN.Classes.TDSMAN.MDIExit = true;
                                        //        //
                                        //        //--
                                        //        Application.Exit();
                                        //        //
                                        //        UtUpdateApplicationUpdate objUpdateUpdaterexe = new UtUpdateApplicationUpdate();
                                        //        objUpdateUpdaterexe.ShowDialog();
                                        //        return;
                                        //    }
                                        //}
                                        //else
                                        //    return;
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
                                                FileVersionInfo fileVersionInfoU = FileVersionInfo.GetVersionInfo(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication));
                                                double dblUpdatatorexeVersion = Convert.ToDouble(fileVersionInfoU.FileMajorPart + "." + fileVersionInfoU.FileMinorPart);

                                                //now checking the latest update available and downloading the same
                                                if (Registration.Get_Latest_Updater_version(dblUpdatatorexeVersion) == true)
                                                {
                                                    UtUpdateApplicationUpdate objUpdateUpdaterexe = new UtUpdateApplicationUpdate();
                                                    objUpdateUpdaterexe.ShowDialog();
                                                }
                                            }
                                        }
                                        //--
                                        //Creating a File with Version and Financial Year
                                        StreamWriter writer = new StreamWriter(Path.Combine(Application.StartupPath, "TempUpdate.txt"));

                                        writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pVersionType.ToString());
                                        //writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString());//-- 2017/12/07
                                        writer.WriteLine(lngPackageFAYear.ToString());
                                        writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pEditionType.ToString());
                                        writer.WriteLine(TDSMAN.Classes.TDSMAN.T_ClientServerMachine.ToString()); //-- ANIK 2015-01-30
                                        writer.WriteLine(System.IntPtr.Size.ToString());                          //-- ANIK 2021-02-05

                                        writer.Flush();
                                        writer.Dispose();
                                        writer.Close();
                                        //--
                                        if (File.Exists(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication) == true)
                                        {
                                            dmlService.Dispose();
                                            //this.Close();
                                            //this.Dispose();
                                            //J_Var.frmMain.Close();
                                            //J_Var.frmMain.Dispose();
                                            TDSMAN.Classes.TDSMAN.MDIExit = true;
                                            Application.Exit();
                                            Process.Start(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //cmnService.J_UserMessage("Due to some techinical error the server version is not getting accessed.\n Kindly call us on 033-22623535/64596006 for further assistance.");
                                cmnService.J_UserMessage("Due to some techinical error the server version is not getting accessed.\n Kindly call us on 033-22623535/9836490007 for further assistance.");
                                return;
                            }
                            #endregion
                            //
                        }
                    }
                    #endregion
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                    {
                        //--
                        if (TdsMan.T_CheckInternetConnectivty() == true)
                        {
                            //Registration.Save_User_Detail_Record(strSerialNo, "", TDSMAN.Classes.TDSMAN.T_pEditionType, TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), 3, out strOutValue);
                            string strOutValue = "";
                            //--
                            Registration.Save_User_Header_Detail_Record(strSerialNoCurrentFY, 
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
                                                                        STR1,
                                                                        out strOutValue);
                        }
                        //--
                        //Creating a File with Version and Financial Year
                        StreamWriter writer = new StreamWriter(Path.Combine(Application.StartupPath, "TempUpdate.txt"));

                        writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pVersionType.ToString());
                        writer.WriteLine(lngPackageNxtFAYear.ToString());
                        writer.WriteLine(TDSMAN.Classes.TDSMAN.T_pEditionType.ToString());
                        writer.WriteLine(TDSMAN.Classes.TDSMAN.T_ClientServerMachine.ToString()); //-- ANIK 2015-01-30
                        writer.WriteLine(System.IntPtr.Size.ToString());                          //-- ANIK 2021-02-05

                        writer.Flush();
                        writer.Dispose();
                        writer.Close();
                        //Creating a File with Version and Financial Year
                        StreamWriter writerMyDoc = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TempUpdate.txt"));
                        //
                        writerMyDoc.WriteLine(TDSMAN.Classes.TDSMAN.T_pVersionType.ToString());
                        writerMyDoc.WriteLine(lngPackageNxtFAYear.ToString());
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
                        //--
                        //-- AUTO BACKUP MECHANISM
                        TdsMan.AutoBackupData("After upgrade");
                        //
                        if (File.Exists(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication) == true)
                        {
                            dmlService.Dispose();
                            this.Close();
                            this.Dispose();
                            //
                            J_Var.frmMain.Close();
                            J_Var.frmMain.Dispose();
                            //--
                            Process.Start(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_pLicensedUpdateApplication);
                        }
                        return;
                    }
                    //--
                    ////////-- 2025/04/22
                    //////if (strEmailBlog != "")
                    //////{
                    //////    TrnSubscribeBlogMimibrowser objBrow = new TrnSubscribeBlogMimibrowser();
                    //////    objBrow.Email = strEmailBlog;
                    //////    objBrow.Show();
                    //////    //--
                    //////    Registration.SaveBlogDetails(TDSMAN.Classes.TDSMAN.T_pVersionType, TDSMAN.Classes.TDSMAN.T_pTDSMANSerialNo.ToString(), strEmailBlog);
                    //////    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                    //////    {
                    //////        //--
                    //////        string strTrialVersionRegistryPath = TdsMan.GetRegistryFolder() + "(Trial)";
                    //////        cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, strTrialVersionRegistryPath,
                    //////                                        T_RegistrationInfo.Blog_email_flag.ToString(),
                    //////                                        T_YES_NO.YES.ToString());
                    //////    }
                    //////    else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                    //////    {
                    //////        cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                    //////                                        T_RegistrationInfo.Blog_email_flag.ToString(),
                    //////                                        T_YES_NO.YES.ToString());
                    //////    }
                    //////    //--
                    //////    System.Threading.Thread.Sleep(2000);
                    //////}
                    ////////--
                    TDSMAN.Classes.TDSMAN.T_pShowUpdateMessage = true;
                    //
                    //J_Var.frmMain = new mdiTDSMAN();
                    //J_Var.frmMain.ShowDialog();
                    //BtnNext.Text = "&Finish";                    
                    //-- 2015-12-04 - [TRIAL TO STANDARD LICENSED]
                    if (TDSMAN.Classes.TDSMAN.T_TrialActivateToLicense == 1)
                    {
                        //if (File.Exists(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_DllFileToDetectEdition)) == true)
                        //    File.Delete(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_DllFileToDetectEdition));
                        //-- 2016/01/15
                        //CreateDETECTED();
                        //-- 2016/01/15
                        TdsMan.SetLicensedEdition();
                        //--
                        cmnService.J_UserMessage("TDSMAN application will close now.\nPlease restart the application to experience the changed effect.");
                        Application.Exit();
                    }
                    else
                    {
                        if (J_Var.J_pLoginScreen == J_LoginScreen.YES)
                        {
                            CmnLogin frm = new CmnLogin();
                            frm.ShowDialog();
                            frm.Dispose();
                        }
                        else if (J_Var.J_pLoginScreen == J_LoginScreen.NO)
                        {
                            J_Var.frmMain = new mdiTDSMAN();
                            J_Var.frmMain.ShowDialog();
                        }
                    }
                }
                #endregion

                #region tbpOnline1
                else if (tbcRegistration.SelectedTab == tbpOnline1)
                {
                    //-----------------------------------------------
                    if (TdsMan.T_CheckInternetConnectivty() == false)
                    {
                        cmnService.J_UserMessage(strUserMessage);
                        return;
                    }
                    //
                    if (TdsMan.T_CheckServerTDSMAN() == false)
                    {
                        cmnService.J_UserMessage("Software is not able to connect www.tdsman.com\nPlease try Offline Registration.");
                        return;
                    }
                    if (TDSMAN.Classes.TDSMAN.T_TrialActivateToLicense == 1)
                    {
                        string strGetResult = CheckRegistry(txtEnterSerialNoOnline.Text.Trim());
                        //
                        if (strGetResult == T_TRUE_FALSE.FALSE.ToString())
                        {
                            return;
                        }
                        else if (strGetResult == T_TRUE_FALSE.TRUE.ToString())
                        {
                            //if (File.Exists(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_DllFileToDetectEdition)) == true)
                            //    File.Delete(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_DllFileToDetectEdition));
                            //-- 2016/01/15
                            //CreateDETECTED();
                            //-- 2016/01/15
                            TdsMan.SetLicensedEdition();
                            //--
                            cmnService.J_UserMessage("TDSMAN application will close now.\nPlease restart the application to experience the changed effect.");
                            Application.Exit();
                        }
                    }
                    //--
                    //--
                    this.Cursor = Cursors.WaitCursor;
                    //
                    lblPleaseWaitMessage2.Visible = true;
                    //
                    this.Refresh();
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_TrialActivateToLicense == 1) //-- 2015-12-05 - [TRIAL TO STANDARD LICENSED]
                        txtEnterConfigurationNoOnline.Text = TdsMan.T_GenerateNewConfigNo(DateTime.Now.ToString());
                    //-- CHECK WHETHER RE-REGISTRATION FROM 2018-19
                    if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                    {
                        strSerialNoCurrentFY = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                                                                   T_RegistrationInfo.Serial_No.ToString());
                        if (strSerialNoCurrentFY != "")
                        {
                            //if (strSerialNoCurrentFY == txtEnterSerialNoOnline.Text.Trim())
                            //{
                            //    string strUpgrade = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                            //                                       T_RegistrationInfo.Upgrade.ToString());
                            //    if (strUpgrade.ToUpper() == T_TRUE_FALSE.FALSE.ToString().ToUpper())
                            //    {
                            cmnService.J_UserMessage("TDSMAN (FY:" + (lngPackageNxtFAYear - 1).ToString() + "-" + lngPackageNxtFAYear.ToString() + ") is already installed in the system as such upgrade is not permitted.");
                            dmlService.Dispose();
                            this.Close();
                            this.Dispose();
                            //
                            return;
                            //    }
                            //}
                        }
                    }
                    // FIRST TIME REGISTRATION
                    //blIs_FirstTime_Reg = Registration.Is_FirstTime_Reg_New(txtEnterSerialNoOnline.Text.Trim(), TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), TDSMAN.Classes.TDSMAN.T_pEditionType);
                    if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == false)
                        //-- 2017/12/07
                        blIs_FirstTime_Reg = Registration.Is_FirstTime_Reg_New(txtEnterSerialNoOnline.Text.Trim(), lngPackageFAYear.ToString(), TDSMAN.Classes.TDSMAN.T_pEditionType);
                    else if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                        blIs_FirstTime_Reg = Registration.Is_FirstTime_Reg_New(txtEnterSerialNoOnline.Text.Trim(), lngPackageNxtFAYear.ToString(), TDSMAN.Classes.TDSMAN.T_pEditionType);
                    //
                    if (ValidateFields() == false) return;
                    //
                    if (blIs_FirstTime_Reg == false)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == false)
                        {
                            if (Registration.Get_UserDetail_New(txtEnterSerialNoOnline.Text.Trim(),
                                //TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(),
                                                            lngPackageFAYear.ToString(),
                                                            TDSMAN.Classes.TDSMAN.T_pEditionType,
                                                            out strLicenseeName,
                                                            out strCompanyName,
                                                            out strEmail,
                                                            out strMobile,
                                                            out strPhone,
                                                            out strFAX,
                                                            out strAddress,
                                                            out strCity,
                                                            out strState,
                                                            out strPIN) == false)
                                return;
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                        {
                            if (Registration.Get_UserDetail_New(txtEnterSerialNoOnline.Text.Trim(),
                                //TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(),
                                                            lngPackageNxtFAYear.ToString(),
                                                            TDSMAN.Classes.TDSMAN.T_pEditionType,
                                                            out strLicenseeName,
                                                            out strCompanyName,
                                                            out strEmail,
                                                            out strMobile,
                                                            out strPhone,
                                                            out strFAX,
                                                            out strAddress,
                                                            out strCity,
                                                            out strState,
                                                            out strPIN) == false)
                                return;
                        }
                        txtViewLicenseeNameOnline.Text = strLicenseeName;
                        txtViewCompanyNameOnline.Text = strCompanyName;
                        txtViewEmailOnline.Text = strEmail;
                        strEmailBlog = strEmail;
                        txtViewMobilOnline.Text = strMobile;
                        //
                        blnOpenTabPage = true;
                        tbcRegistration.SelectTab(tbpOnline3);
                        //
                        lblHeaderMessage.Text = "\n   Preview User details";
                        //
                        BtnNext.Select();
                    }
                    else
                    {
                        blnOpenTabPage = true;
                        tbcRegistration.SelectTab(tbpOnline2);
                        //-- STATE
                        //-----------
                        strSQL = " SELECT STATE_ID," +
                            "             STATE_NAME " +
                            "      FROM   MST_STATE " +
                            "      ORDER BY STATE_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbLicenseeStateOnline) == false) return;
                        //
                        lblHeaderMessage.Text = "\n Online Registration Process";
                    }
                    BtnNext.Text = "&Next >";
                    //
                    if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true && blIs_FirstTime_Reg == true)
                    {
                        BtnBack.Enabled = true;
                        //
                        TDSMAN.Classes.TDSMAN.T_ProductUpgrade = false;
                        strPrevFAYearSerial_No = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                                                                   T_RegistrationInfo.Serial_No.ToString());
                        TDSMAN.Classes.TDSMAN.T_ProductUpgrade = true;
                        //
                        if (Registration.Get_UserDetail_New(strPrevFAYearSerial_No,
                                                        lngPackageFAYear.ToString(),
                                                        TDSMAN.Classes.TDSMAN.T_pEditionType,
                                                        out strLicenseeName,
                                                        out strCompanyName,
                                                        out strEmail,
                                                        out strMobile,
                                                        out strPhone,
                                                        out strFAX,
                                                        out strAddress,
                                                        out strCity,
                                                        out strState,
                                                        out strPIN) == false)
                        {
                            return;
                        }
                        txtLicenseeNameOnline.Text = strLicenseeName;
                        txtCompanyNameOnline.Text = strCompanyName;
                        txtLicenseeEmailOnline.Text = strEmail;
                        strEmailBlog = strEmail;
                        txtLicenseeMobileOnline.Text = strMobile;
                        txtLicenseeAddressOnline.Text = strAddress;
                        txtLicenseeCityOnline.Text = strCity;
                        txtLicenseePINOnline.Text = strPIN;
                        cmbLicenseeStateOnline.Text = strState;
                        txtLicenseePhoneNoOnline.Text = strPhone;
                        txtLicenseeFAXOnline.Text = strFAX;
                        //
                        BtnNext.Select();
                    }
                    this.Cursor = Cursors.Default;
                    //
                    //-- 2025/04/22
                    if (strEmailBlog != "")
                    {
                        if (TdsMan.T_CheckInternetConnectivty() == true)
                        {
                            //TrnSubscribeBlogMimibrowser objBrow = new TrnSubscribeBlogMimibrowser();
                            TrnSubscribeBlogSendy objBrow = new TrnSubscribeBlogSendy();  //-- 2026/02/16
                            objBrow.Email = strEmailBlog;
                            objBrow.ShowInTaskbar = false;
                            objBrow.Opacity = 0;
                            objBrow.StartPosition = FormStartPosition.Manual;
                            objBrow.Location = new Point(-2000, -2000);
                            objBrow.Show();
                            //--
                            Registration.SaveBlogDetails(TDSMAN.Classes.TDSMAN.T_pVersionType, TDSMAN.Classes.TDSMAN.T_pTDSMANSerialNo.ToString(), strEmailBlog);
                            if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                            {
                                //--
                                string strTrialVersionRegistryPath = TdsMan.GetRegistryFolder() + "(Trial)";
                                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, strTrialVersionRegistryPath,
                                                                T_RegistrationInfo.Blog_email_flag.ToString(),
                                                                T_YES_NO.YES.ToString());
                            }
                            else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                            {
                                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                                                                T_RegistrationInfo.Blog_email_flag.ToString(),
                                                                T_YES_NO.YES.ToString());
                            }
                            //--
                            //System.Threading.Thread.Sleep(3000);
                            //objBrow.Close();
                            //objBrow.Dispose();
                        }
                    }
                    //
                    lblPleaseWaitMessage2.Visible = false;
                }
                #endregion

                #region tbpOnline2
                else if (tbcRegistration.SelectedTab == tbpOnline2)
                {
                    if (TdsMan.T_CheckInternetConnectivty() == false)
                    {
                        cmnService.J_UserMessage(strUserMessage);
                        return;
                    }
                    //
                    if (TdsMan.T_CheckServerTDSMAN() == false)
                    {
                        cmnService.J_UserMessage("Software is not able to connect www.tdsman.com\nPlease try Offline Registration.");
                        return;
                    }
                    //
                    this.Cursor = Cursors.WaitCursor;
                    //
                    lblPleaseWaitMessage3.Visible = true;
                    //
                    this.Refresh();
                    if (ValidateFields() == false) return;
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                    {
                        // SAVE USER RECORD
                        if (Registration.Save_User_Header_Detail_Record(txtEnterSerialNoOnline.Text.Trim(), txtEnterConfigurationNoOnline.Text.Trim().Replace("-", ""),
                            //TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), //-- 2017/12/07
                                                                lngPackageNxtFAYear.ToString(),
                                                                TDSMAN.Classes.TDSMAN.T_pEditionType,
                                                                intInstallType,
                                                                txtLicenseeNameOnline.Text.Trim(),
                                                                txtCompanyNameOnline.Text.Trim(),
                                                                txtLicenseeEmailOnline.Text.Trim(),
                                                                txtLicenseeMobileOnline.Text.Trim(),
                                                                txtLicenseePhoneNoOnline.Text.Trim(),
                                                                txtLicenseeFAXOnline.Text.Trim(),
                                                                txtLicenseeAddressOnline.Text.Trim(),
                                                                txtLicenseeCityOnline.Text.Trim(),
                                                                cmbLicenseeStateOnline.Text.Trim(),
                                                                txtLicenseePINOnline.Text.Trim(),
                                                                strPrevFAYearSerial_No, //strSerialNoCurrentFY,
                                                                out strActivationNumber) == false)
                            return;
                        //-- UNINSTALL THE PREV YR SERIAL WHEN UPGRADING
                        //Registration.Save_User_Header_Detail_Record(strSerialNo, "", TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), TDSMAN.Classes.TDSMAN.T_pEditionType, 3, "", "", "", "", "", "", "", "", "", "", out strOutValue);
                        //Registration.SaveUserDetailRecordUnInstallation(strPrevFAYearSerial_No,"",
                    }
                    else
                    {
                        // SAVE USER RECORD
                        if (Registration.Save_User_Header_Detail_Record(txtEnterSerialNoOnline.Text.Trim(), txtEnterConfigurationNoOnline.Text.Trim().Replace("-", ""),
                            //TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), //-- 2017/12/07
                                                                lngPackageFAYear.ToString(),
                                                                TDSMAN.Classes.TDSMAN.T_pEditionType, intInstallType, txtLicenseeNameOnline.Text.Trim(),
                                                                txtCompanyNameOnline.Text.Trim(), txtLicenseeEmailOnline.Text.Trim(),
                                                                txtLicenseeMobileOnline.Text.Trim(), txtLicenseePhoneNoOnline.Text.Trim(),
                                                                txtLicenseeFAXOnline.Text.Trim(), txtLicenseeAddressOnline.Text.Trim(),
                                                                txtLicenseeCityOnline.Text.Trim(), cmbLicenseeStateOnline.Text.Trim(),
                                                                txtLicenseePINOnline.Text.Trim(), out strActivationNumber) == false)
                            return;
                    }
                    //
                    strEmailBlog = txtLicenseeEmailOnline.Text.Trim();
                    //if (Registration.Save_User_Detail_Record(txtEnterSerialNoOnline.Text.Trim(), txtEnterConfigurationNoOnline.Text.Trim().Replace("-",""),
                    //                                        TDSMAN.Classes.TDSMAN.T_pPackageVersionNo,intInstallType, out strActivationNumber) == false)
                    //    return;
                    //
                    if (T_WriteToRegistry(txtEnterSerialNoOnline.Text.Trim(),
                                        txtLicenseeNameOnline.Text.Trim(),
                                        Convert.ToString(dtService.J_ConvertddMMyyyy(System.DateTime.Now.Date.ToString())),
                                        strActivationNumber) == true)
                    {
                        //
                        blnOpenTabPage = true;
                        tbcRegistration.SelectTab(tbpSucessfullyRegistered);
                        //
                        lblHeaderMessage.Text = "\n   Registration Complete";
                        BtnNext.Text = "&Finish";
                        BtnBack.Enabled = false;
                        BtnBack.BackColor = Color.LightGray;
                        BtnCancel.Enabled = false;
                        BtnCancel.BackColor = Color.LightGray;
                        BtnNext.Select();
                    }
                    else
                    {
                        cmnService.J_UserMessage("REGISTRATION FAILED", MessageBoxIcon.Error);
                    }
                    //
                    this.Cursor = Cursors.Default;
                    //
                    //-- 2025/04/22
                    if (strEmailBlog != "")
                    {
                        if (TdsMan.T_CheckInternetConnectivty() == true)
                        {
                            //TrnSubscribeBlogMimibrowser objBrow = new TrnSubscribeBlogMimibrowser();
                            TrnSubscribeBlogSendy objBrow = new TrnSubscribeBlogSendy();
                            objBrow.Email = strEmailBlog;
                            objBrow.Show();
                            //--
                            //Registration.SaveBlogDetails(TDSMAN.Classes.TDSMAN.T_pVersionType, TDSMAN.Classes.TDSMAN.T_pTDSMANSerialNo.ToString(), strEmailBlog);
                            Registration.SaveBlogDetails(TDSMAN.Classes.TDSMAN.T_pVersionType, txtEnterSerialNoOnline.Text , strEmailBlog);
                            if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                            {
                                //--
                                string strTrialVersionRegistryPath = TdsMan.GetRegistryFolder() + "(Trial)";
                                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, strTrialVersionRegistryPath,
                                                                T_RegistrationInfo.Blog_email_flag.ToString(),
                                                                T_YES_NO.YES.ToString());
                            }
                            else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                            {
                                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                                                                T_RegistrationInfo.Blog_email_flag.ToString(),
                                                                T_YES_NO.YES.ToString());
                            }
                            //--
                            //System.Threading.Thread.Sleep(3000);
                            //objBrow.Close();
                            //objBrow.Dispose();
                        }
                    }
                    //
                    lblPleaseWaitMessage3.Visible = false;
                }
                #endregion

                #region tbpOnline3
                else if (tbcRegistration.SelectedTab == tbpOnline3)
                {
                    //---------------------------------
                    if (TdsMan.T_CheckInternetConnectivty() == false)
                    {
                        cmnService.J_UserMessage(strUserMessage);
                        return;
                    }
                    //
                    if (TdsMan.T_CheckServerTDSMAN() == false)
                    {
                        cmnService.J_UserMessage("Software is not able to connect www.tdsman.com\nPlease try Offline Registration.");
                        return;
                    }
                    //
                    this.Cursor = Cursors.WaitCursor;
                    //
                    lblPleaseWaitMessage4.Visible = true;
                    //
                    this.Refresh();
                    if (ValidateFields() == false) return;
                    // SAVE USER DETAILS
                    //
                    if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == false)
                    {
                        if (Registration.Save_User_Detail_Record(txtEnterSerialNoOnline.Text.Trim(),
                                                                txtEnterConfigurationNoOnline.Text.Trim().Replace("-", ""),
                                                                TDSMAN.Classes.TDSMAN.T_pEditionType,
                            //TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(),//-- 2017/12/07
                                                                lngPackageFAYear.ToString(),
                                                                intInstallType,
                                                                out strActivationNumber) == false)
                            return;
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                    {
                        if (Registration.Save_User_Detail_Record(txtEnterSerialNoOnline.Text.Trim(),
                                                                   txtEnterConfigurationNoOnline.Text.Trim().Replace("-", ""),
                                                                   TDSMAN.Classes.TDSMAN.T_pEditionType,
                            //TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(),//-- 2017/12/07
                                                                   lngPackageNxtFAYear.ToString(),
                                                                   intInstallType,
                                                                   out strActivationNumber) == false)
                            return;
                    }
                    //
                    this.Refresh();
                    //
                    if (T_WriteToRegistry(txtEnterSerialNoOnline.Text.Trim(),
                                        txtViewLicenseeNameOnline.Text.Trim(),
                                        Convert.ToString(dtService.J_ConvertddMMyyyy(System.DateTime.Now.Date.ToString())),
                                        strActivationNumber) == true)
                    {
                        //
                        blnOpenTabPage = true;
                        tbcRegistration.SelectTab(tbpSucessfullyRegistered);
                        //-- 2017/12/08
                        if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                        {
                            File.Delete(Path.Combine(Application.StartupPath , TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer));
                            lblMessage7.Text = "TDSMAN has been successfully registered. The software will run update now. \n Click Finish to run Update.";
                        }
                        //
                        lblHeaderMessage.Text = "\n   Registration Complete";
                        BtnNext.Text = "&Finish";
                        BtnBack.Enabled = false;
                        BtnBack.BackColor = Color.LightGray;
                        BtnCancel.Enabled = false;
                        BtnCancel.BackColor = Color.LightGray;
                    }
                    else
                    {
                        cmnService.J_UserMessage("REGISTRATION FAILED", MessageBoxIcon.Error);
                    }
                    //
                    this.Cursor = Cursors.Default;
                    //
                    lblPleaseWaitMessage4.Visible = false;
                }
                #endregion
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                lblPleaseWaitMessage1.Visible = false;
                lblPleaseWaitMessage2.Visible = false;
                lblPleaseWaitMessage3.Visible = false;
                lblPleaseWaitMessage4.Visible = false;
                lblPleaseWaitMessage5.Visible = false;
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region BtnBack_Click
        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (tbcRegistration.SelectedTab == tbpOffline1)
            {
                blnOpenTabPage = true;
                tbcRegistration.SelectTab(tbpSelectType);
                //
                //lblHeaderMessage.Text = "\n Welcome to TDSMAN \n Registration";
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                    lblHeaderMessage.Text = "\n Welcome to " + strExeName + " (Server) \n Registration";
                else
                    lblHeaderMessage.Text = "\n Welcome to " + strExeName + " \n Registration";
                //
                lblPleaseWaitMessage1.Visible = false;
                //
                BtnBack.Enabled = false;
                BtnBack.BackColor = Color.LightGray;
                //    
                BtnNext.Text = "&Next >";
            }
            else if (tbcRegistration.SelectedTab == tbpOffline2)
            {
                blnOpenTabPage = true;
                tbcRegistration.SelectTab(tbpOffline1);
                //
                lblHeaderMessage.Text = "\n Offline Registration Process";
                //
                BtnNext.Text = "&Next >";
                //-- 2015-12-04 - [TRIAL TO STANDARD LICENSED]
                if (TDSMAN.Classes.TDSMAN.T_TrialActivateToLicense == 1)
                {
                    BtnBack.Enabled = false;
                    BtnBack.BackColor = Color.LightGray;
                }
                //  
            }
            else if (tbcRegistration.SelectedTab == tbpSucessfullyRegistered)
            {
                //tbcRegistration.SelectTab(tbpOffline2);
                //BtnNext.Text = "&Next >";
            }
            else if (tbcRegistration.SelectedTab == tbpOnline1)
            {
                blnOpenTabPage = true;
                tbcRegistration.SelectTab(tbpSelectType);
                //
                //lblHeaderMessage.Text = "\n Welcome to TDSMAN \n Registration";
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                    lblHeaderMessage.Text = "\n Welcome to " + strExeName + " (Server) \n Registration";
                else
                    lblHeaderMessage.Text = "\n Welcome to " + strExeName + " \n Registration";
                //
                lblPleaseWaitMessage1.Visible = false;
                //
                BtnBack.Enabled = false;
                BtnBack.BackColor = Color.LightGray;
                //
                BtnNext.Text = "&Next >";
            }
            else if (tbcRegistration.SelectedTab == tbpOnline2)
            {
                blnOpenTabPage = true;
                tbcRegistration.SelectTab(tbpOnline1);
                //
                lblHeaderMessage.Text = "\n Online Registration Process";
                BtnNext.Text = "&Next >";
            }
            else if (tbcRegistration.SelectedTab == tbpOnline3)
            {
                blnOpenTabPage = true;
                tbcRegistration.SelectTab(tbpOnline1);
                //
                lblHeaderMessage.Text = "\n Online Registration Process";
                BtnNext.Text = "&Next >";
            }
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
            //this.Dispose();
            //-- 2015-12-04 - [TRIAL TO STANDARD LICENSED]
            if (TDSMAN.Classes.TDSMAN.T_TrialActivateToLicense == 1)
            {
                this.Close();
                this.Dispose();
                //--
                J_Var.frmMain = new mdiTDSMAN();
                J_Var.frmMain.ShowDialog();
                return;
            }
            else
            {
                this.Close();
                this.Dispose();
            }
        }
        #endregion

        #region txtActivationCodeOffline_KeyPress
        private void txtActivationCodeOffline_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnNext_Click(sender, e);
        }

        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtEnterSerialNoOnline_KeyPress
        private void txtEnterSerialNoOnline_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnNext_Click(sender, e);
        }
        #endregion

        #region Searching_KeyPress
        private void Searching_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            //if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region Sorting_KeyPress
        private void Sorting_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) BtnSortOK_Click(sender, e);
            //if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion        

        #region tbcRegistration_Selecting
        private void tbcRegistration_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.Action == TabControlAction.Selecting)
            {
                if (blnOpenTabPage == false)
                {
                    e.Cancel = true;
                }
                else
                    blnOpenTabPage = false;
            }
        }
        #endregion

        #region btnBrowseDatabaseFolder_Click
        private void btnBrowseDatabaseFolder_Click(object sender, EventArgs e)
        {
            //--
            FolderBrowserDialog fldrBrowser = new FolderBrowserDialog();
            fldrBrowser.Description = "Select Database Location";
            fldrBrowser.RootFolder = Environment.SpecialFolder.Desktop;
            fldrBrowser.ShowNewFolderButton = false;
            fldrBrowser.ShowDialog();
            //--
            strDatabaseLocation = fldrBrowser.SelectedPath;// cmnService.J_OpenFolderDialog("Select Database location");
            //--
            txtDatabaseLocation.Text = strDatabaseLocation;
            //-- VALIDATE
            if (TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_LITE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
            {
                if (File.Exists(Path.Combine(strDatabaseLocation, J_Var.J_pMsAccessDatabaseName)) == false)
                {
                    cmnService.J_UserMessage("Database Location not found");
                    return;
                }
            }
            //
            txtServerSerialNo.Select();
            //--
        }
        #endregion

        #region txtDatabaseLocation_Leave
        private void txtDatabaseLocation_Leave(object sender, EventArgs e)
        {
            if (txtDatabaseLocation.Text.Trim() != "")
                strDatabaseLocation = txtDatabaseLocation.Text.Trim();
        }
        #endregion

        #region lnkBuyNow_LinkClicked
        private void lnkBuyNow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.tdsman.com/pricing.asp");
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                //if (tbcRegistration.SelectedTab == tbpSelectType)
                //{
                //    if (rbnOffline.Checked == true)
                //    {
                //        tbcRegistration.SelectTab(tbpOffline1);
                //        //
                //        BtnBack.Visible = true;
                //        BtnNext.Text = "&Next >";
                //    }
                //    else if (rbnOnline.Checked == true)
                //    {
                //        tbcRegistration.SelectTab(tbpOnline1);
                //        //
                //        BtnBack.Visible = true;
                //        BtnNext.Text = "&Next >";
                //    }
                //}
                //else 
                if (tbcRegistration.SelectedTab == tbpOffline2)
                {
                    if (txtEnterSerialNoOffline.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Enter the Serial No.");
                        txtEnterSerialNoOffline.Select();
                        return false;
                    }
                    if (txtLicenseeNameOffline.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Enter the Licensee Name");
                        txtLicenseeNameOffline.Select();
                        return false;
                    }
                    if (txtEmailOffline.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Enter the Licensee Email");
                        txtEmailOffline.Select();
                        return false;
                    }
                    //
                    if (TdsMan.T_ValidateEmail(txtEmailOffline.Text) == false)
                    {
                        cmnService.J_UserMessage("Enter valid Email");
                        txtEmailOffline.Select();
                        return false;
                    }
                    //-- ANIK @ 2017/04/17
                    if (TdsMan.T_CheckEmailFormat(txtEmailOffline.Text.Trim()) == false)
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        txtEmailOffline.Select();
                        return false;
                    }
                    //           
                }
                else if (tbcRegistration.SelectedTab == tbpOffline1)
                {
                    if (txtConfigurationNoOffline.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("No System Id provided");
                        txtConfigurationNoOffline.Select();
                        return false;
                    }
                    //
                    if (txtActivationCodeOffline.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Enter the Activation Code");
                        txtActivationCodeOffline.Select();
                        return false;
                    }
                    // VALID ACTIVATION CODE CHECK
                    //if (txtConfigurationNoOffline.Text.Trim() != txtActivationCodeOffline.Text.Trim())
                    //if (TdsMan.T_GenerateActivationNumber(txtConfigurationNoOffline.Text.Trim(), TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString()) != txtActivationCodeOffline.Text.Trim())
                    //-- 2017/12/07
                    if (TdsMan.T_GenerateActivationNumber(txtConfigurationNoOffline.Text.Trim(), lngPackageFAYear.ToString()) != txtActivationCodeOffline.Text.Trim())
                    {
                        cmnService.J_UserMessage("Wrong Activation Code");
                        txtActivationCodeOffline.Select();
                        return false;
                    }
                }
                else if (tbcRegistration.SelectedTab == tbpSucessfullyRegistered)
                {
                }
                else if (tbcRegistration.SelectedTab == tbpOnline1)
                {
                    if (txtEnterSerialNoOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage2.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter the Serial No.");
                        txtEnterSerialNoOnline.Select();
                        return false;
                    }
                    //
                    if (TdsMan.T_CheckInternetConnectivty() == false)
                    {
                        lblPleaseWaitMessage2.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage(strUserMessage);
                        txtEnterSerialNoOnline.Select();
                        return false;
                    }
                    //
                    if (blIs_FirstTime_Reg == true)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == false)
                        {
                            if (Registration.Is_Allow_For_Registration(txtEnterSerialNoOnline.Text.Trim(),
                                //TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), //-- 2017/12/07
                                                                       lngPackageFAYear.ToString(),
                                                                       TDSMAN.Classes.TDSMAN.T_pEditionType,
                                                                       txtEnterConfigurationNoOnline.Text.Trim().Replace("-", ""),
                                                                       out intInstallType) == false)
                            {
                                lblPleaseWaitMessage2.Visible = false;
                                //
                                this.Cursor = Cursors.Default;
                                //
                                cmnService.J_UserMessage("Invalid Serial No.");
                                //
                                BtnCancel.Select();
                                return false;
                            }
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                        {
                            if (Registration.Is_Allow_For_Registration(txtEnterSerialNoOnline.Text.Trim(),
                                //TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), //-- 2017/12/07
                                                                          lngPackageNxtFAYear.ToString(),
                                                                          TDSMAN.Classes.TDSMAN.T_pEditionType,
                                                                          txtEnterConfigurationNoOnline.Text.Trim().Replace("-", ""),
                                                                          out intInstallType) == false)
                            {
                                lblPleaseWaitMessage2.Visible = false;
                                //
                                this.Cursor = Cursors.Default;
                                //
                                cmnService.J_UserMessage("Invalid Serial No.");
                                //
                                BtnCancel.Select();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //if (Registration.Is_Allow_For_Registration(txtEnterSerialNoOnline.Text.Trim(), TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), TDSMAN.Classes.TDSMAN.T_pEditionType, txtEnterConfigurationNoOnline.Text.Trim().Replace("-", ""), out intInstallType) == false)
                        if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == false)
                        {
                            //-- 2017/12/07
                            if (Registration.Is_Allow_For_Registration(txtEnterSerialNoOnline.Text.Trim(), lngPackageFAYear.ToString(), TDSMAN.Classes.TDSMAN.T_pEditionType, txtEnterConfigurationNoOnline.Text.Trim().Replace("-", ""), out intInstallType) == false)
                            {
                                lblPleaseWaitMessage2.Visible = false;
                                //
                                this.Cursor = Cursors.Default;
                                //
                                cmnService.J_UserMessage("Serial No. is already Registered.");
                                //
                                BtnCancel.Select();
                                return false;
                            }
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                        {
                            //-- 2017/12/07
                            if (Registration.Is_Allow_For_Registration(txtEnterSerialNoOnline.Text.Trim(), lngPackageNxtFAYear.ToString(), TDSMAN.Classes.TDSMAN.T_pEditionType, txtEnterConfigurationNoOnline.Text.Trim().Replace("-", ""), out intInstallType) == false)
                            {
                                lblPleaseWaitMessage2.Visible = false;
                                //
                                this.Cursor = Cursors.Default;
                                //
                                cmnService.J_UserMessage("Serial No. is already Registered.");
                                //
                                BtnCancel.Select();
                                return false;
                            }
                        }
                    }
                }
                else if (tbcRegistration.SelectedTab == tbpOnline2)
                {
                    if (txtLicenseeNameOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter the Licensee Name");
                        txtLicenseeNameOnline.Select();
                        return false;
                    }
                    //
                    if (txtCompanyNameOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter the Company Name");
                        txtCompanyNameOnline.Select();
                        return false;
                    }
                    //
                    if (txtLicenseeEmailOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter the Email");
                        txtLicenseeEmailOnline.Select();
                        return false;
                    }
                    //
                    if (TdsMan.T_ValidateEmail(txtLicenseeEmailOnline.Text) == false)
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter valid Email");
                        txtLicenseeEmailOnline.Select();
                        return false;
                    }
                    //-- ANIK @ 2017/04/17
                    if (TdsMan.T_CheckEmailFormat(txtLicenseeEmailOnline.Text.Trim()) == false)
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        txtLicenseeEmailOnline.Select();
                        return false;
                    }
                    //
                    //
                    if (txtLicenseeMobileOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter the Mobile No.");
                        txtLicenseeMobileOnline.Select();
                        return false;
                    }
                    //
                    if (txtLicenseeMobileOnline.Text.Length < 10)
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter valid Mobile No.");
                        txtLicenseeMobileOnline.Select();
                        return false;
                    }
                    //
                    if (txtLicenseeCityOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter your City.");
                        txtLicenseeCityOnline.Select();
                        return false;
                    }
                    if (cmbLicenseeStateOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter your State.");
                        cmbLicenseeStateOnline.Select();
                        return false;
                    }

                }
                else if (tbcRegistration.SelectedTab == tbpOnline3)
                {
                }
                //
                return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion
        
        #region T_WriteToRegistry
        public bool T_WriteToRegistry(string SerialNo, string LicenseeName, string Date, string ActivationCode)
        {
            try
            {
                // CREATE REGISTRY
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName);
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder());
                // SET SERIAL NO
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                                                T_RegistrationInfo.Serial_No.ToString(), SerialNo);

                if (LicenseeName != "")
                    // SET LICENSEE NAME
                    cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                                                   T_RegistrationInfo.Licensee_Name.ToString(), LicenseeName);

                // SET SYSTEM DATE
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                                                T_RegistrationInfo.Reg_Date.ToString(), Date);

                // SET ACTIVATION CODE
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                                                T_RegistrationInfo.Activ_Code.ToString(), ActivationCode);

                //-- 2017/12/08 - AUTO UPGRADE FLAG TO RUN UPDATE AUTOMATICALLY AFTER REGISTRATION 
                if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                {
                    cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                                                    T_RegistrationInfo.Upgrade.ToString(), T_TRUE_FALSE.TRUE.ToString());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion       

        #region lnkChangeInternetConnectionSettings_LinkClicked
        private void lnkChangeInternetConnectionSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrnProxySettings TrnProxySettings = new TrnProxySettings();
            TrnProxySettings.ShowDialog();
        }
        #endregion

        #region CheckRegistry
        private string CheckRegistry(string SerialNo)
        {
            string strSerialNo = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                                                                   T_RegistrationInfo.Serial_No.ToString());
            //--
            if (strSerialNo != "")
            {
                if (strSerialNo == SerialNo)
                {
                    if (cmnService.J_UserMessage("TDSMAN license is already installed in this system.\nDo you still want to convert this Trial to License one.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        return T_TRUE_FALSE.TRUE.ToString();
                    else
                        return T_TRUE_FALSE.FALSE.ToString();
                }
                else
                {
                    cmnService.J_UserMessage("TDSMAN license is already installed in this system with a different Serial No.\nFurther conversion to License from Trial is not possible in this system.");
                    return T_TRUE_FALSE.FALSE.ToString();
                }
            }
            return "";
        }
        #endregion


        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=W8yX8Dp6jUM&t=55s");
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0026", txtEnterSerialNoOnline.Text, TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            }
            else
            {
            }
        }
        #endregion


        #region pctVideoDemoSelectType_Click
        private void pctVideoDemoSelectType_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/embed/n5gwPfAzVm8?autoplay=1");
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0041", txtEnterSerialNoOnline.Text, TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctVideoDemoClientRegistration_Click
        private void pctVideoDemoClientRegistration_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=BdzcqyuEg-Q");
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0039", txtEnterSerialNoOnline.Text , TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region CreateDETECTED
        //-- 2016/01/15
        //private void CreateDETECTED()
        //{
        //    File.Create(Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_DllFileToDetectEdition)).Dispose();
        //}
        #endregion

        #endregion

        #region pctManualSelectType_Click
        private void pctManualSelectType_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0009", txtEnterSerialNoOnline.Text, TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion


        #region pctVideoDemoSelectType_MouseMove
        private void pctVideoDemoSelectType_MouseMove(object sender, MouseEventArgs e)
        {
            //tllTipManual.RemoveAll();
            tllTipVideoDemo.SetToolTip(pctVideoDemoSelectType, pctVideoDemoSelectType.Tag.ToString());
        }
        #endregion

        #region pctManualSelectType_MouseMove
        private void pctManualSelectType_MouseMove(object sender, MouseEventArgs e)
        {
            //tllTipVideoDemo.RemoveAll();
            tllTipManual.SetToolTip(pctManualSelectType, pctManualSelectType.Tag.ToString());
        }
        #endregion

        #region pctManual_Click
        private void pctManual_Click(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0076", txtEnterSerialNoOnline.Text, TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            else
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0010", txtEnterSerialNoOnline.Text, TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion


        #region pctManualClientRegistration_Click
        private void pctManualClientRegistration_Click(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_ProductUpgrade == true)
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0076", txtEnterSerialNoOnline.Text, TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            else
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0011", txtEnterSerialNoOnline.Text, TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion


        #region pctVideoDemoClientRegistration_MouseMove
        private void pctVideoDemoClientRegistration_MouseMove(object sender, MouseEventArgs e)
        {
            //tllTipManual.RemoveAll();
            tllTipVideoDemo.SetToolTip(pctVideoDemoClientRegistration, pctVideoDemoClientRegistration.Tag.ToString());
        }
        #endregion

        #region pctManualClientRegistration_MouseMove
        private void pctManualClientRegistration_MouseMove(object sender, MouseEventArgs e)
        {
            //tllTipVideoDemo.RemoveAll();
            tllTipManual.SetToolTip(pctManualClientRegistration, pctManualClientRegistration.Tag.ToString());
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctManual_MouseMove
        private void pctManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctManual, pctManual.Tag.ToString());
        }
        #endregion
    }
}