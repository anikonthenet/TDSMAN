#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: ANIK GHOSH
Module Name		: Quick Links
Version			: 2.0
Start Date		: 
End Date		: 
Tables Used     : 
Module Desc		: 
________________________________________________________________________________________________________

*/

#endregion

#region Referred Namespaces
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Data;
    using System.IO;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Net;
    //~~~~ User Namespaces ~~~~
    using TDSMAN;
    using TDSMAN.FormRpt;
    using TDSMAN.Classes;
    using TDSMAN.FormTrn;
    using TDSMAN.FormSys;
    using TDSMAN.FormMst;
    using TDSMAN.FormPar;
using TDSMAN.FormUtl;
//------------
    using System.Reflection;
using System.Collections.Generic;
#endregion

namespace TDSMAN.FormPar
{
    public partial class MstQuickLinksFullScreen : Form
    {

        ResizeForm _form_resize;

        #region Constructor
        public MstQuickLinksFullScreen()
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

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();

        ToolTip tllTip = new ToolTip();

        mdiTDSMAN mdiTDSMAN = new mdiTDSMAN();

        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        //--
        string strSQL = "";
        //--
        //int intQuickLinkID1 = 0;
        //int intQuickLinkID2 = 0;
        //int intQuickLinkID3 = 0;
        //int intQuickLinkID4 = 0;
        //int intQuickLinkID5 = 0;
        //int intQuickLinkID6 = 0;
        //int intQuickLinkID7 = 0;
        //int intQuickLinkID8 = 0;
        ////--
        //string strQuickLinkModule1 = "";
        //string strQuickLinkModule2 = "";
        //string strQuickLinkModule3 = "";
        //string strQuickLinkModule4 = "";
        //string strQuickLinkModule5 = "";
        //string strQuickLinkModule6 = "";
        //string strQuickLinkModule7 = "";
        //string strQuickLinkModule8 = "";
        ////--
        //string strQuickLinkType1 = "";
        //string strQuickLinkType2 = "";
        //string strQuickLinkType3 = "";
        //string strQuickLinkType4 = "";
        //string strQuickLinkType5 = "";
        //string strQuickLinkType6 = "";
        //string strQuickLinkType7 = "";
        //string strQuickLinkType8 = "";
        ////--
        //string strQuickLinkTitle1 = "";
        //string strQuickLinkTitle2 = "";
        //string strQuickLinkTitle3 = "";
        //string strQuickLinkTitle4 = "";
        //string strQuickLinkTitle5 = "";
        //string strQuickLinkTitle6 = "";
        //string strQuickLinkTitle7 = "";
        //string strQuickLinkTitle8 = "";
        //--
        bool blResize = true;
        #endregion

        #region Event Handlers

        #region RESIZING WINDOW
        private void _Load(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2022_23)
            _form_resize._get_initial_size();
        }

        private void _Resize(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2022_23)
            if (blResize == true)
                _form_resize._resize();
            //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        }
        #endregion

        #region MstQuickLinks_Load
        private void MstQuickLinks_Load(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2022_23)
            //{
            //blResize = true;
            //int h = Screen.PrimaryScreen.WorkingArea.Height;
            //int w = Screen.PrimaryScreen.WorkingArea.Width;
            //this.ClientSize = new Size(w, h);
            //}
            //pnlQuickLinks.Height = 15;
            GC.Collect();
            //-----------------------------------------------------------
            this.Top = 0;
            //
            try
            {
                #region DESKTOP INFORMATION
                // LOAD DESKTOP INFORMATION
                if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION ||
                    TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.PUBLICATION_HOUSE_VERSION)
                {
                    //string strTrialVersionRegistryPath = TDSMAN.Classes.TDSMAN.T_pPackageName + "(Trial)";
                    string strTrialVersionRegistryPath = TdsMan.GetRegistryFolder() + "(Trial)";
                    //    
                    lblLicenseeName.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + strTrialVersionRegistryPath,
                                                                       T_RegistrationInfo.Licensee_Name.ToString());
                    //
                    lblSerialNo.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + strTrialVersionRegistryPath,
                                                                       T_RegistrationInfo.Activ_Code.ToString());
                    //-- 2021/04/06
                    TDSMAN.Classes.TDSMAN.T_pTDSMANSerialNo = lblSerialNo.Text;
                    //-- 2023/01/06
                    //    
                    lblLicenseeName2223.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + strTrialVersionRegistryPath,
                                                                       T_RegistrationInfo.Licensee_Name.ToString());
                    //
                    lblSerialNo2223.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + strTrialVersionRegistryPath,
                                                                       T_RegistrationInfo.Activ_Code.ToString());
                    //-- 2023/01/28
                    TDSMAN.Classes.TDSMAN.T_pTMSerialNo = lblSerialNo2223.Text;
                }
                else
                {
                    lblLicenseeName.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                                                                       T_RegistrationInfo.Licensee_Name.ToString());
                    //
                    lblSerialNo.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                                                                       T_RegistrationInfo.Serial_No.ToString());
                    //--
                    TDSMAN.Classes.TDSMAN.T_pTDSMANSerialNo = lblSerialNo.Text;
                    //--
                    #region FOR CLIENT'S TEXT
                    if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                        {
                            lblSerialNo.Text = lblSerialNo.Text + " (Client User)";
                        }
                    }
                    #endregion
                    //-- 2023/01/06
                    //    
                    lblLicenseeName2223.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                                                                       T_RegistrationInfo.Licensee_Name.ToString());
                    //
                    lblSerialNo2223.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                                                                       T_RegistrationInfo.Serial_No.ToString());
                    //-- 2023/01/28
                    TDSMAN.Classes.TDSMAN.T_pTMSerialNo = lblSerialNo2223.Text;
                }
                //
                //MessageBox.Show(TDSMAN.Classes.TDSMAN.T_pTMSerialNo.ToString());
                //ADDED BY SHREY
                if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                {
                    string strMaxFAYear = "";
                    string strMinFAYear = "";

                    //COMMENTED BY SHREY KEJRIWAL ON 06/04/2012 (SUBQUERY CHANGED)
                    //strSQL = "SELECT FA_YEAR FROM MST_ASSESSMENT WHERE ASST_ID = (SELECT MAX(ASST_ID) FROM MST_ASSESSMENT)";

                    strSQL = "SELECT TOP 1 FA_YEAR FROM MST_ASSESSMENT ORDER BY ASST_ID DESC";
                    strMaxFAYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                    //COMMENTED BY SHREY KEJRIWAL ON 06/04/2012 (SUBQUERY CHANGED)
                    //strSQL = "SELECT FA_YEAR FROM MST_ASSESSMENT WHERE ASST_ID = (SELECT MIN(ASST_ID) FROM MST_ASSESSMENT)";

                    strSQL = "SELECT TOP 1 FA_YEAR FROM MST_ASSESSMENT ORDER BY ASST_ID";
                    strMinFAYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                    lblFinancialYear.Text = strMinFAYear + " To : " + strMaxFAYear;
                    //
                    lblFinancialYear2223.Text = strMinFAYear + " To : " + strMaxFAYear;
                }
                else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION ||
                    TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.PUBLICATION_HOUSE_VERSION)
                {
                    string strMaxFAYear = "";

                    strSQL = "SELECT FA_YEAR FROM MST_ASSESSMENT WHERE ASST_ID = (SELECT MAX(ASST_ID) FROM MST_ASSESSMENT)";
                    strMaxFAYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                    lblFinancialYear.Text = strMaxFAYear;
                    //
                    lblFinancialYear2223.Text = strMaxFAYear;
                }
                //
                lblUpdatedOn.Text = TDSMAN.Classes.TDSMAN.T_pLastUpdtDate;
                lblUpdatedOn2223.Text = TDSMAN.Classes.TDSMAN.T_pLastUpdtDate;
                //-------------------------------------------------------
                // BACKGROUND PICTURE
                if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION ||
                    TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.PUBLICATION_HOUSE_VERSION)
                {
                    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2019_20)
                        lblTrialVersionLabel.Visible = true;
                    else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear > (int)Software_Version.FY2019_20)
                        lblTrialVersionLabelBlack.Visible = true;
                }
                else
                    lblTrialVersionLabel.Visible = false;
                //--
                #endregion
                //
                //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2013_14)
                //{
                #region DOWNLOAD dlls & others
                if (TdsMan.T_CheckServerTDSMAN() == true)
                {
                    //check if the latest dll is installed
                    //if (File.Exists(Path.Combine(Application.StartupPath, "CompressDB.exe")) == false ||
                    //    File.Exists(Path.Combine(Application.StartupPath, "DGVControl.dll")) == false ||
                    //    File.Exists(Path.Combine(Application.StartupPath, "Interop.JRO.dll")) == false ||
                    //    File.Exists(Path.Combine(Application.StartupPath, "System.Management.dll")) == false)
                    //if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-software-button13-14.png")) == false)
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType != (int)T_EDITION_TYPE.ENTERPRISE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != (int)T_EDITION_TYPE.ENTERPRISE_LITE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != (int)T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                    {
                        if (File.Exists(Path.Combine(Application.StartupPath, "CompressDB.exe")) == false ||
                            File.Exists(Path.Combine(Application.StartupPath, "DGVControl.dll")) == false ||
                            File.Exists(Path.Combine(Application.StartupPath, "Interop.JRO.dll")) == false ||
                            File.Exists(Path.Combine(Application.StartupPath, "System.Management.dll")) == false ||
                            //File.Exists(Path.Combine(Application.StartupPath, "tdsman-software-button13-14.png")) == false ||
                            File.Exists(Path.Combine(Application.StartupPath, "HtmlAgilityPack.dll")) == false ||
                            File.Exists(Path.Combine(Application.StartupPath, "Newtonsoft.Json.dll")) == false ||
                            File.Exists(Path.Combine(Application.StartupPath, "itextsharp.dll")) == false)
                        {
                            //downloading the dlls
                            //UtUpdateAllDlls objUpdatedll = new UtUpdateAllDlls();
                            //objUpdatedll.ShowDialog();
                        }
                    }
                    //-- 2015/01/08
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION ||
                        TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE) //-- 2022/04/11
                    {
                        if (File.Exists(Path.Combine(Application.StartupPath, "PDFSignDll.dll")) == false)
                        {
                            //downloading the dlls
                            //UtUpdatePDFDlls objUpdatePDFdll = new UtUpdatePDFDlls();
                            //objUpdatePDFdll.ShowDialog();
                        }
                    }
                    //-- ANIK @ 2015/09/29
                    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2014_15)
                    {
                        if (File.Exists(Path.Combine(Application.StartupPath, "PDFSignDll.dll")) == false)
                        {
                            //downloading the dlls
                            //UtUpdatePDFDlls objUpdatePDFdll = new UtUpdatePDFDlls();
                            //objUpdatePDFdll.ShowDialog();
                        }
                    }
                    //-- ANIK @ 2017/03/09
                    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2017_18)
                    {
                        if (File.Exists(Path.Combine(Application.StartupPath, "WebDriver.dll")) == false)
                        {
                            //downloading the dlls
                            //UtWebUpdateDlls objUpdatedll = new UtWebUpdateDlls();
                            //objUpdatedll.ShowDialog();
                        }
                    }
                }
                #endregion
                //}
                #region DOWNLOAD EDITION IMAGE
                if (TdsMan.T_CheckServerTDSMAN() == true)
                {
                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
                    {
                        if (File.Exists(Path.Combine(Application.StartupPath, "logo-standard.png")) == false)
                        {
                            //downloading the dlls
                            UtWebUpdateImages objUpdatePDFdll = new UtWebUpdateImages();
                            objUpdatePDFdll.ShowDialog();
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.PROFESSIONAL_EDITION)
                    {
                        if (File.Exists(Path.Combine(Application.StartupPath, "logo-professional.png")) == false)
                        {
                            //downloading the dlls
                            UtWebUpdateImages objUpdatePDFdll = new UtWebUpdateImages();
                            objUpdatePDFdll.ShowDialog();
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)
                    {
                        if (File.Exists(Path.Combine(Application.StartupPath, "logo-enterprise-lite.png")) == false)
                        {
                            //downloading the dlls
                            UtWebUpdateImages objUpdatePDFdll = new UtWebUpdateImages();
                            objUpdatePDFdll.ShowDialog();
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_EDITION)
                    {
                        if (File.Exists(Path.Combine(Application.StartupPath, "logo-enterprise.png")) == false)
                        {
                            //downloading the dlls
                            UtWebUpdateImages objUpdatePDFdll = new UtWebUpdateImages();
                            objUpdatePDFdll.ShowDialog();
                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                    {
                        if (File.Exists(Path.Combine(Application.StartupPath, "logo-ultimate.png")) == false)
                        {
                            //downloading the dlls
                            UtWebUpdateImages objUpdatePDFdll = new UtWebUpdateImages();
                            objUpdatePDFdll.ShowDialog();
                        }
                    }
                }
                #endregion
                //--
                #region DESIGN CHANGE

                #region COMMENTED
                //-- 2022/01/15
                //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2022_23)
                //{
                //    this.BackgroundImageLayout = ImageLayout.Stretch;
                //    //--
                //    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION ||
                //    TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.PUBLICATION_HOUSE_VERSION)
                //    {
                //        //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2019_20)
                //        //    lblTrialVersionLabel.Visible = true;
                //        //else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear > (int)Software_Version.FY2019_20)
                //        //lblTrialVersionLabelBlack.Visible = true;
                //        //lblTrialVersionLabel.Visible = false;
                //    }
                //    else
                //    {
                //        //lblTrialVersionLabel.Visible = false;
                //        //lblTrialVersionLabelBlack.Visible = false;
                //    }
                //    //--
                //    pnlQuickLinks.Location = new Point(0, 0);
                //    pnlQuickLinks.Width = 262;
                //    pnlQuickLinks.Height = 764;
                //    pnlQuickLinks.BorderStyle = BorderStyle.None ;
                //    Color myColor = Color.FromArgb(60, Color.White);
                //    pnlQuickLinks.BackColor = myColor;
                //    //--
                //    if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-software-background-22-23.jpg")) == true)
                //        this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-background-22-23.jpg"));
                //    //--
                //    pictureBox1.Visible = false;
                //    pictureBox2.Visible = false;
                //    pictureBox3.Visible = false;
                //    pictureBox4.Visible = false;
                //    pictureBox5.Visible = false;
                //    pictureBox6.Visible = false;
                //    pictureBox8.Visible = false;
                //    pictureBox9.Visible = false;
                //    //
                //    lblQuickLinks1.BackColor = myColor;
                //    lblQuickLinks2.BackColor = myColor;
                //    lblQuickLinks3.BackColor = myColor;
                //    lblQuickLinks4.BackColor = myColor;
                //    lblQuickLinks5.BackColor = myColor;
                //    lblQuickLinks6.BackColor = myColor;
                //    lblQuickLinks7.BackColor = myColor;
                //    lblQuickLinks8.BackColor = myColor;
                //    //
                //    lblDevelopedBy.Visible = true;
                //    lblDevelopedBy.BackColor = myColor;
                //    lblDevelopedBy.Height = 20;
                //    lblDevelopedByAddress.Visible = true;
                //    lblDevelopedByAddress.BackColor = myColor;
                //    lblDevelopedByAddress.Height = 75;
                //    //
                //    lblQuickLinks1.TextAlign = ContentAlignment.MiddleLeft;
                //    lblQuickLinks2.TextAlign = ContentAlignment.MiddleLeft;
                //    lblQuickLinks3.TextAlign = ContentAlignment.MiddleLeft;
                //    lblQuickLinks4.TextAlign = ContentAlignment.MiddleLeft;
                //    lblQuickLinks5.TextAlign = ContentAlignment.MiddleLeft;
                //    lblQuickLinks6.TextAlign = ContentAlignment.MiddleLeft;
                //    lblQuickLinks7.TextAlign = ContentAlignment.MiddleLeft;
                //    lblQuickLinks8.TextAlign = ContentAlignment.MiddleLeft;
                //    //
                //    lblQuickLinks1.ForeColor = Color.Black;
                //    lblQuickLinks2.ForeColor = Color.Black;
                //    lblQuickLinks3.ForeColor = Color.Black;
                //    lblQuickLinks4.ForeColor = Color.Black;
                //    lblQuickLinks5.ForeColor = Color.Black;
                //    lblQuickLinks6.ForeColor = Color.Black;
                //    lblQuickLinks7.ForeColor = Color.Black;
                //    lblQuickLinks8.ForeColor = Color.Black;
                //    //
                //    pnlLine1.Visible = true;
                //    pnlLine2.Visible = true;
                //    pnlLine3.Visible = true;
                //    pnlLine4.Visible = true;
                //    pnlLine5.Visible = true;
                //    pnlLine6.Visible = true;
                //    pnlLine7.Visible = true;
                //    pnlLine8.Visible = true;
                //    //
                //    //lblHelpLine.Visible = true;
                //    //lblHelpEmail.Visible = true;
                //    //lblWebsite.Visible = true;
                //    pnlHelpLine.Visible = true;
                //    //lnkWebsite.Font = new Font("Microsoft Sans Serif", 24);
                //    //--
                //    pctBoxLogo.Visible = true;
                //    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                //    {
                //        if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-STD-22-23.png")) == true)
                //            pctBoxLogo.Image = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-STD-22-23.png"));
                //    }
                //    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                //    {
                //        if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-PRO-22-23.png")) == true)
                //            pctBoxLogo.Image = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-PRO-22-23.png"));
                //    }
                //    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION)
                //    {
                //        if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-ENT-22-23.png")) == true)
                //            pctBoxLogo.Image = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-ENT-22-23.png"));
                //    }
                //    else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)
                //    {
                //        if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-LITE-22-23.png")) == true)
                //            pctBoxLogo.Image = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-LITE-22-23.png"));
                //    }
                //    //
                //    #region DESKTOP INFORMATION
                //    // LOAD DESKTOP INFORMATION
                //    lblLicenseeName.Visible = false;
                //    lblLicenseeNameColon.Visible = false;
                //    lblLicenseeNameDisp.Visible = false;
                //    lblSerialNo.Visible = false;
                //    lblSerialNoColon.Visible = false;
                //    lblSerialNoDisp.Visible = false;
                //    lblFinancialYearDisp.Visible = false;
                //    lblFinancialYearColon.Visible = false;
                //    lblFinancialYear.Visible = false;
                //    lblUpdatedOnDisp.Visible = false;
                //    lblUpdatedOnColon.Visible = false;
                //    lblUpdatedOn.Visible = false;
                //    pnlDisplayBox.Visible = true;
                //    //
                //    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION ||
                //        TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.PUBLICATION_HOUSE_VERSION)
                //    {
                //        //string strTrialVersionRegistryPath = TDSMAN.Classes.TDSMAN.T_pPackageName + "(Trial)";
                //        string strTrialVersionRegistryPath = TdsMan.GetRegistryFolder() + "(Trial)";
                //        //    
                //        lblLicenseeName2223.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + strTrialVersionRegistryPath,
                //                                                           T_RegistrationInfo.Licensee_Name.ToString());
                //        //
                //        lblSerialNo2223.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + strTrialVersionRegistryPath,
                //                                                           T_RegistrationInfo.Activ_Code.ToString());
                //        //-- 2021/04/06
                //        TDSMAN.Classes.TDSMAN.T_pTDSMANSerialNo = lblSerialNo.Text;
                //    }
                //    else
                //    {
                //        lblLicenseeName2223.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                //                                                           T_RegistrationInfo.Licensee_Name.ToString());
                //        //
                //        lblSerialNo2223.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                //                                                           T_RegistrationInfo.Serial_No.ToString());
                //        //--
                //        TDSMAN.Classes.TDSMAN.T_pTDSMANSerialNo = lblSerialNo.Text;
                //        //--
                //        #region FOR CLIENT'S TEXT
                //        if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                //        {
                //            if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                //            {
                //                lblSerialNo2223.Text = lblSerialNo2223.Text + " (Client User)";
                //            }
                //        }
                //        #endregion
                //    }
                //    //
                //    //ADDED BY SHREY
                //    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                //    {
                //        string strMaxFAYear = "";
                //        string strMinFAYear = "";

                //        //COMMENTED BY SHREY KEJRIWAL ON 06/04/2012 (SUBQUERY CHANGED)
                //        //strSQL = "SELECT FA_YEAR FROM MST_ASSESSMENT WHERE ASST_ID = (SELECT MAX(ASST_ID) FROM MST_ASSESSMENT)";

                //        strSQL = "SELECT TOP 1 FA_YEAR FROM MST_ASSESSMENT ORDER BY ASST_ID DESC";
                //        strMaxFAYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                //        //COMMENTED BY SHREY KEJRIWAL ON 06/04/2012 (SUBQUERY CHANGED)
                //        //strSQL = "SELECT FA_YEAR FROM MST_ASSESSMENT WHERE ASST_ID = (SELECT MIN(ASST_ID) FROM MST_ASSESSMENT)";

                //        strSQL = "SELECT TOP 1 FA_YEAR FROM MST_ASSESSMENT ORDER BY ASST_ID";
                //        strMinFAYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                //        lblFinancialYear2223.Text = strMinFAYear + " To : " + strMaxFAYear;
                //    }
                //    else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION ||
                //        TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.PUBLICATION_HOUSE_VERSION)
                //    {
                //        string strMaxFAYear = "";

                //        strSQL = "SELECT FA_YEAR FROM MST_ASSESSMENT WHERE ASST_ID = (SELECT MAX(ASST_ID) FROM MST_ASSESSMENT)";
                //        strMaxFAYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                //        lblFinancialYear2223.Text = strMaxFAYear;
                //    }
                //    //
                //    lblUpdatedOn2223.Text = TDSMAN.Classes.TDSMAN.T_pLastUpdtDate;
                //    //-------------------------------------------------------
                //    // BACKGROUND PICTURE
                //    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION ||
                //        TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.PUBLICATION_HOUSE_VERSION)
                //    {
                //        lblTrialVersionLabel.Visible = false;
                //        lblTrialVersionLabelBlack.Visible = true;

                //    }
                //    else
                //    {
                //        lblTrialVersionLabel.Visible = false;
                //        lblTrialVersionLabelBlack.Visible = false;
                //    }
                //    //--
                //    #endregion
                //    //
                //}
                //else 
                #endregion
                //
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2011_12)
                {
                    this.BackgroundImageLayout = ImageLayout.None;
                    //
                    #region 2013_14 && 2014-15
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2013_14)
                        {
                            #region FY2013_14
                            //-- 2013-14
                            if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-software-background-13-14.jpg")) == true)
                                this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-background-13-14.jpg"));
                            //-- DETAILS TEXT COLOR TO WHITE
                            lblLicenseeNameDisp.ForeColor = Color.White;
                            lblSerialNoDisp.ForeColor = Color.White;
                            lblFinancialYearDisp.ForeColor = Color.White;
                            lblUpdatedOnDisp.ForeColor = Color.White;

                            lblLicenseeNameColon.ForeColor = Color.White;
                            lblSerialNoColon.ForeColor = Color.White;
                            lblFinancialYearColon.ForeColor = Color.White;
                            lblUpdatedOnColon.ForeColor = Color.White;

                            lblLicenseeName.ForeColor = Color.White;
                            lblSerialNo.ForeColor = Color.White;
                            lblFinancialYear.ForeColor = Color.White;
                            lblUpdatedOn.ForeColor = Color.White;
                            //-- 
                            lblQuickLinksForms.BackColor = Color.White;
                            lblQuickLinksForms.ForeColor = Color.DarkRed;
                            #endregion
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2014_15)
                        {

                            #region FY2014_15
                            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                            {
                                if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-software-background-14-15.jpg")) == true)
                                    this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-background-14-15.jpg"));
                            }
                            else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                            {
                                if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-software-background-14-15-pro.jpg")) == true)
                                    this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-background-14-15-pro.jpg"));
                            }
                            #endregion
                        }
                        //-- ANIK 2015-01-29
                        else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2015_16)
                        {
                            #region FY2015_16
                            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                            {
                                if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-software-background-15-16.jpg")) == true)
                                    this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-background-15-16.jpg"));
                            }
                            else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                            {
                                if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-software-background-15-16-pro.jpg")) == true)
                                    this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-background-15-16-pro.jpg"));
                            }
                            #endregion
                        }
                        ////-- ANIK 2016-01-09
                        //else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2016_17)
                        //-- ANIK 2016-01-09
                        else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2016_17)
                        {
                            #region > FY2016_17
                            //--
                            string strFY = "";
                            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2016_17)
                                strFY = "16-17";
                            else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2017_18)
                                strFY = "17-18";
                            else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2018_19)
                                strFY = "18-19";
                            else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2019_20) //-- 2019/01/01
                                strFY = "19-20";
                            else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2020_21) //-- 2020/01/08
                                strFY = "20-21";
                            else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2021_22) //-- 2021/01/13
                                strFY = "21-22";
                            else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2022_23) //-- 2022/01/20
                                strFY = "22-23";
                            else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2023_24) //-- 2026/01/28
                                strFY = "23-24";
                            else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2024_25) //-- 2026/01/28
                                strFY = "24-25";
                            else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2025_26) //-- 2026/01/28
                                strFY = "25-26";
                            else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2026_27) //-- 2026/01/28
                                strFY = "26-27";
                            //--
                            //-- DETAILS TEXT COLOR TO WHITE
                            lblLicenseeNameDisp.ForeColor = Color.White;
                            lblSerialNoDisp.ForeColor = Color.White;
                            lblFinancialYearDisp.ForeColor = Color.White;
                            lblUpdatedOnDisp.ForeColor = Color.White;

                            lblLicenseeNameColon.ForeColor = Color.White;
                            lblSerialNoColon.ForeColor = Color.White;
                            lblFinancialYearColon.ForeColor = Color.White;
                            lblUpdatedOnColon.ForeColor = Color.White;

                            lblLicenseeName.ForeColor = Color.White;
                            lblSerialNo.ForeColor = Color.White;
                            lblFinancialYear.ForeColor = Color.White;
                            lblUpdatedOn.ForeColor = Color.White;
                            //-- 
                            this.BackgroundImage = base.BackgroundImage;
                            pctBoxLogo.Visible = true;
                            pnlDisplayBox.Visible = true;
                            //
                            lblLicenseeNameDisp.Visible = false;
                            lblSerialNoDisp.Visible = false;
                            lblFinancialYearDisp.Visible = false;
                            lblUpdatedOnDisp.Visible = false;

                            lblLicenseeNameColon.Visible = false;
                            lblSerialNoColon.Visible = false;
                            lblFinancialYearColon.Visible = false;
                            lblUpdatedOnColon.Visible = false;

                            lblLicenseeName.Visible = false;
                            lblSerialNo.Visible = false;
                            lblFinancialYear.Visible = false;
                            lblUpdatedOn.Visible = false;
                            //
                            pnlRightSide.Visible = true;
                            pnlHelpLine.Visible = true;
                            lblDevelopedBy.Visible = true;
                            lblDevelopedByAddress.Visible = true;

                            #endregion
                        }
                        //                        
                        //
                        if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-software-button13-14.png")) == true)
                        {
                            //pictureBox1.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-button13-14.png"));
                            //pictureBox2.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-button13-14.png"));
                            //pictureBox3.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-button13-14.png"));
                            //pictureBox4.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-button13-14.png"));
                            //pictureBox5.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-button13-14.png"));
                            //pictureBox6.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-button13-14.png"));
                            //pictureBox9.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-button13-14.png"));
                            //pictureBox8.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-button13-14.png"));
                        }
                        //-- COMMENTED ON 2023/01/16
                        //lblQuickLinks1.ForeColor = Color.Black;
                        //lblQuickLinks2.ForeColor = Color.Black;
                        //lblQuickLinks3.ForeColor = Color.Black;
                        //lblQuickLinks4.ForeColor = Color.Black;
                        //lblQuickLinks5.ForeColor = Color.Black;
                        //lblQuickLinks6.ForeColor = Color.Black;
                        //lblQuickLinks7.ForeColor = Color.Black;
                        //lblQuickLinks8.ForeColor = Color.Black;
                        ////
                        //lblQuickLinks1.BackColor = Color.White;
                        //lblQuickLinks2.BackColor = Color.White;
                        //lblQuickLinks3.BackColor = Color.White;
                        //lblQuickLinks4.BackColor = Color.White;
                        //lblQuickLinks5.BackColor = Color.White;
                        //lblQuickLinks6.BackColor = Color.White;
                        //lblQuickLinks7.BackColor = Color.White;
                        //lblQuickLinks8.BackColor = Color.White;
                        //--
                        //lblQuickLinks1.ForeColor = Color.White;
                        //lblQuickLinks2.ForeColor = Color.White;
                        //lblQuickLinks3.ForeColor = Color.White;
                        //lblQuickLinks4.ForeColor = Color.White;
                        //lblQuickLinks5.ForeColor = Color.White;
                        //lblQuickLinks6.ForeColor = Color.White;
                        //lblQuickLinks7.ForeColor = Color.White;
                        //lblQuickLinks8.ForeColor = Color.White;
                        ////
                        //lblQuickLinks1.BackColor = Color.Maroon;
                        //lblQuickLinks2.BackColor = Color.Maroon;
                        //lblQuickLinks3.BackColor = Color.Maroon;
                        //lblQuickLinks4.BackColor = Color.Maroon;
                        //lblQuickLinks5.BackColor = Color.Maroon;
                        //lblQuickLinks6.BackColor = Color.Maroon;
                        //lblQuickLinks7.BackColor = Color.Maroon;
                        //lblQuickLinks8.BackColor = Color.Maroon;
                        //
                        //lblQuickLinks1.Location = new Point(55, 103);
                        //lblQuickLinks2.Location = new Point(55, 151);
                        //lblQuickLinks3.Location = new Point(55, 198);
                        //lblQuickLinks4.Location = new Point(55, 245);
                        //lblQuickLinks5.Location = new Point(55, 292);
                        //lblQuickLinks6.Location = new Point(55, 339);
                        //lblQuickLinks7.Location = new Point(55, 386);
                        //lblQuickLinks8.Location = new Point(55, 433);
                        //
                        //lblQuickLinks1.Height = 20;
                        //lblQuickLinks1.Width = 191;

                        //lblQuickLinks2.Height = 20;
                        //lblQuickLinks2.Width = 191;

                        //lblQuickLinks3.Height = 20;
                        //lblQuickLinks3.Width = 191;

                        //lblQuickLinks4.Height = 20;
                        //lblQuickLinks4.Width = 191;

                        //lblQuickLinks5.Height = 20;
                        //lblQuickLinks5.Width = 191;

                        //lblQuickLinks6.Height = 20;
                        //lblQuickLinks6.Width = 191;

                        //lblQuickLinks7.Height = 20;
                        //lblQuickLinks7.Width = 191;

                        //lblQuickLinks8.Height = 20;
                        //lblQuickLinks8.Width = 191;
                    }
                    #endregion
                    //--
                    #region 2011-12, 2012-13
                    else
                    {
                        //-- 2011-12, 2012-13
                        if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-software-background_new.jpg")) == true)
                            this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-background_new.jpg"));
                    }
                    #endregion
                    //--
                    #region 2014-15 onwards
                    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2014_15)
                    {
                        //-- 2018/01/17
                        if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2018_19)
                        {

                            lblLicenseeNameDisp.ForeColor = Color.Black;
                            lblLicenseeNameDisp.Location = new Point(494, 300); //-123
                            lblSerialNoDisp.ForeColor = Color.Black;
                            lblSerialNoDisp.Location = new Point(494, 320);//-123
                            lblFinancialYearDisp.Location = new Point(494, 340);//-123
                            lblFinancialYearDisp.ForeColor = Color.Black;
                            lblUpdatedOnDisp.Location = new Point(494, 360);//-123
                            lblUpdatedOnDisp.ForeColor = Color.Black;

                            lblLicenseeNameColon.Location = new Point(595, 300);
                            lblLicenseeNameColon.ForeColor = Color.Black;
                            lblSerialNoColon.Location = new Point(595, 320);
                            lblSerialNoColon.ForeColor = Color.Black;
                            lblFinancialYearColon.Location = new Point(595, 340);
                            lblFinancialYearColon.ForeColor = Color.Black;
                            lblUpdatedOnColon.Location = new Point(595, 360);
                            lblUpdatedOnColon.ForeColor = Color.Black;

                            lblLicenseeName.Location = new Point(608, 300);
                            lblLicenseeName.ForeColor = Color.Black;
                            lblSerialNo.Location = new Point(608, 320);
                            lblSerialNo.ForeColor = Color.Black;
                            lblFinancialYear.Location = new Point(608, 340);
                            lblFinancialYear.ForeColor = Color.Black;
                            lblUpdatedOn.Location = new Point(608, 360);
                            lblUpdatedOn.ForeColor = Color.Black;
                            //
                            lblTrialVersionLabel.Location = new Point(564, 420);
                        }
                        //else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2019_20) //-- 2019/01/01
                        else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2019_20) //-- 2020/01/08
                        {
                            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2021_22) //-- 2021/01/13
                            {
                                lblLicenseeNameDisp.ForeColor = Color.Black;
                                lblLicenseeNameDisp.Location = new Point(494, 421);
                                lblSerialNoDisp.ForeColor = Color.Black;
                                lblSerialNoDisp.Location = new Point(494, 441);
                                lblFinancialYearDisp.Location = new Point(494, 461);
                                lblFinancialYearDisp.ForeColor = Color.Black;
                                lblUpdatedOnDisp.Location = new Point(494, 481);
                                lblUpdatedOnDisp.ForeColor = Color.Black;

                                lblLicenseeNameColon.Location = new Point(595, 421);
                                lblLicenseeNameColon.ForeColor = Color.Black;
                                lblSerialNoColon.Location = new Point(595, 441);
                                lblSerialNoColon.ForeColor = Color.Black;
                                lblFinancialYearColon.Location = new Point(595, 461);
                                lblFinancialYearColon.ForeColor = Color.Black;
                                lblUpdatedOnColon.Location = new Point(595, 481);
                                lblUpdatedOnColon.ForeColor = Color.Black;

                                lblLicenseeName.Location = new Point(608, 421);
                                lblLicenseeName.ForeColor = Color.Black;
                                lblSerialNo.Location = new Point(608, 441);
                                lblSerialNo.ForeColor = Color.Black;
                                lblFinancialYear.Location = new Point(608, 461);
                                lblFinancialYear.ForeColor = Color.Black;
                                lblUpdatedOn.Location = new Point(608, 481);
                                lblUpdatedOn.ForeColor = Color.Black;
                                //
                                //lblTrialVersionLabelBlack.Location = new Point(578, 250);
                            }
                            else
                            {
                                lblLicenseeNameDisp.ForeColor = Color.Black;
                                lblLicenseeNameDisp.Location = new Point(494, 381);
                                lblSerialNoDisp.ForeColor = Color.Black;
                                lblSerialNoDisp.Location = new Point(494, 401);
                                lblFinancialYearDisp.Location = new Point(494, 421);
                                lblFinancialYearDisp.ForeColor = Color.Black;
                                lblUpdatedOnDisp.Location = new Point(494, 441);
                                lblUpdatedOnDisp.ForeColor = Color.Black;

                                lblLicenseeNameColon.Location = new Point(595, 381);
                                lblLicenseeNameColon.ForeColor = Color.Black;
                                lblSerialNoColon.Location = new Point(595, 401);
                                lblSerialNoColon.ForeColor = Color.Black;
                                lblFinancialYearColon.Location = new Point(595, 421);
                                lblFinancialYearColon.ForeColor = Color.Black;
                                lblUpdatedOnColon.Location = new Point(595, 441);
                                lblUpdatedOnColon.ForeColor = Color.Black;

                                lblLicenseeName.Location = new Point(608, 381);
                                lblLicenseeName.ForeColor = Color.Black;
                                lblSerialNo.Location = new Point(608, 401);
                                lblSerialNo.ForeColor = Color.Black;
                                lblFinancialYear.Location = new Point(608, 421);
                                lblFinancialYear.ForeColor = Color.Black;
                                lblUpdatedOn.Location = new Point(608, 441);
                                lblUpdatedOn.ForeColor = Color.Black;
                                //
                                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2019_20)
                                    lblTrialVersionLabel.Location = new Point(560, 332);
                                else
                                {
                                    //lblTrialVersionLabelBlack.Location = new Point(578, 210);
                                }
                            }
                        }
                        else if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2017_18) //-- 2017/01/07
                        {
                            lblLicenseeNameDisp.Location = new Point(494, 423);
                            lblSerialNoDisp.Location = new Point(494, 443);
                            lblFinancialYearDisp.Location = new Point(494, 463);
                            lblUpdatedOnDisp.Location = new Point(494, 483);

                            lblLicenseeNameColon.Location = new Point(595, 423);
                            lblSerialNoColon.Location = new Point(595, 443);
                            lblFinancialYearColon.Location = new Point(595, 463);
                            lblUpdatedOnColon.Location = new Point(595, 483);

                            lblLicenseeName.Location = new Point(608, 423);
                            lblSerialNo.Location = new Point(608, 443);
                            lblFinancialYear.Location = new Point(608, 463);
                            lblUpdatedOn.Location = new Point(608, 483);

                            lblTrialVersionLabel.Location = new Point(564, 378);
                        }
                        else
                        {
                            lblLicenseeNameDisp.Location = new Point(494, 375);
                            lblSerialNoDisp.Location = new Point(494, 395);
                            lblFinancialYearDisp.Location = new Point(494, 415);
                            lblUpdatedOnDisp.Location = new Point(494, 435);

                            lblLicenseeNameColon.Location = new Point(595, 375);
                            lblSerialNoColon.Location = new Point(595, 395);
                            lblFinancialYearColon.Location = new Point(595, 415);
                            lblUpdatedOnColon.Location = new Point(595, 435);

                            lblLicenseeName.Location = new Point(608, 375);
                            lblSerialNo.Location = new Point(608, 395);
                            lblFinancialYear.Location = new Point(608, 415);
                            lblUpdatedOn.Location = new Point(608, 435);
                            //
                            lblTrialVersionLabel.Location = new Point(564, 325);
                        }
                    }
                    else
                    {
                        lblLicenseeNameDisp.Location = new Point(494, 366);
                        lblSerialNoDisp.Location = new Point(494, 386);
                        lblFinancialYearDisp.Location = new Point(494, 406);
                        lblUpdatedOnDisp.Location = new Point(494, 426);

                        lblLicenseeNameColon.Location = new Point(595, 366);
                        lblSerialNoColon.Location = new Point(595, 386);
                        lblFinancialYearColon.Location = new Point(595, 406);
                        lblUpdatedOnColon.Location = new Point(595, 426);

                        lblLicenseeName.Location = new Point(608, 366);
                        lblSerialNo.Location = new Point(608, 386);
                        lblFinancialYear.Location = new Point(608, 406);
                        lblUpdatedOn.Location = new Point(608, 426);
                        //
                        lblTrialVersionLabel.Location = new Point(564, 267);
                    }
                    #endregion
                    //--
                    #region Side Buttons
                    //lblQuickLinks1.Location = new Point(55, 103);
                    //lblQuickLinks2.Location = new Point(55, 151);
                    //lblQuickLinks3.Location = new Point(55, 198);
                    //lblQuickLinks4.Location = new Point(55, 245);
                    //lblQuickLinks5.Location = new Point(55, 292);
                    //lblQuickLinks6.Location = new Point(55, 339);
                    //lblQuickLinks7.Location = new Point(55, 386);
                    //lblQuickLinks8.Location = new Point(55, 433);
                    //lblQuickLinks1.Font = new Font("Tahoma", 12, FontStyle.Bold);
                    //
                    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2022_23)
                    {
                        lblLicenseeNameDisp.Location = new Point(494, 370);
                        lblSerialNoDisp.Location = new Point(494, 390);
                        lblFinancialYearDisp.Location = new Point(494, 410);
                        lblUpdatedOnDisp.Location = new Point(494, 430);

                        lblLicenseeNameColon.Location = new Point(595, 370);
                        lblSerialNoColon.Location = new Point(595, 390);
                        lblFinancialYearColon.Location = new Point(595, 410);
                        lblUpdatedOnColon.Location = new Point(595, 430);

                        lblLicenseeName.Location = new Point(608, 370);
                        lblSerialNo.Location = new Point(608, 390);
                        lblFinancialYear.Location = new Point(608, 410);
                        lblUpdatedOn.Location = new Point(608, 430);
                        //
                        //lblTrialVersionLabelBlack.Location = new Point(578, 325);
                    }
                    //

                    //lblQuickLinks1.Height = 21;
                    //lblQuickLinks1.Width = 163;

                    //lblQuickLinks2.Height = 21;
                    //lblQuickLinks2.Width = 163;

                    //lblQuickLinks3.Height = 21;
                    //lblQuickLinks3.Width = 163;

                    //lblQuickLinks4.Height = 21;
                    //lblQuickLinks4.Width = 163;

                    //lblQuickLinks5.Height = 21;
                    //lblQuickLinks5.Width = 163;

                    //lblQuickLinks6.Height = 21;
                    //lblQuickLinks6.Width = 163;

                    //lblQuickLinks7.Height = 21;
                    //lblQuickLinks7.Width = 163;

                    //lblQuickLinks8.Height = 21;
                    //lblQuickLinks8.Width = 163;
                    #endregion
                    //--
                    //--
                }
                #endregion
                //--
                #region LOGO CHANGE - FULL SCREEN
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                {
                    if (File.Exists(Path.Combine(Application.StartupPath, "logo-standard.PNG")) == true)
                        pctBoxLogo.Image = Image.FromFile(Path.Combine(Application.StartupPath, "logo-standard.PNG"));
                }
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                {
                    if (File.Exists(Path.Combine(Application.StartupPath, "logo-professional.PNG")) == true)
                        pctBoxLogo.Image = Image.FromFile(Path.Combine(Application.StartupPath, "logo-professional.PNG"));
                }
                //else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE) //-- 2022/04/11
                //{
                //    if (File.Exists(Path.Combine(Application.StartupPath, "tdsman-software-background-" + strFY + "-clh.jpg")) == true)
                //        this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "tdsman-software-background-" + strFY + "-clh.jpg"));
                //}
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION) //-- 2016/02/01
                {
                    if (File.Exists(Path.Combine(Application.StartupPath, "logo-enterprise.PNG")) == true)
                        pctBoxLogo.Image = Image.FromFile(Path.Combine(Application.StartupPath, "logo-enterprise.PNG"));
                }
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION) //-- 2021/01/13
                {
                    if (File.Exists(Path.Combine(Application.StartupPath, "logo-enterprise-lite.PNG")) == true)
                        pctBoxLogo.Image = Image.FromFile(Path.Combine(Application.StartupPath, "logo-enterprise-lite.PNG"));
                }
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION) //-- 2021/01/13
                {
                    if (File.Exists(Path.Combine(Application.StartupPath, "logo-ultimate.PNG")) == true)
                        pctBoxLogo.Image = Image.FromFile(Path.Combine(Application.StartupPath, "logo-ultimate.PNG"));
                }
                #endregion
                //--
                #region SERVER IMAGE
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                {
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.SERVER_MACHINE)
                    {
                        lblServerLabel.Visible = true;
                        //-- 2020/01/06
                        int AllowedNodes = cmnService.J_ReturnInt32Value(TdsMan.GetAllowedVersionXMLFileForSERVER(Application.StartupPath + "/" + TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer, T_XML.ALLOWEDNODES));
                        if (AllowedNodes > 0)
                        {
                            if (AllowedNodes > 1)
                            {
                                lblServerLabel.Text = "Server (1 + " + AllowedNodes + " Users)";
                            }
                            else
                            {
                                lblServerLabel.Text = "Server (1 + " + AllowedNodes + " User)";
                            }
                        }
                        //-- 2022/01/19
                        if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear == (int)Software_Version.FY2022_23)
                        {
                            lblServerLabel.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        #region FOR CLIENT'S TEXT
                        if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                        {
                            if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                            {
                                lblSerialNo2223.Text = lblSerialNo2223.Text + " (Client User)";
                            }
                        }
                        #endregion
                    }
                }
                #endregion
                //--
                #region T_DO_NOT_SHOW_AGAIN_KEYBOARDSHORTCUT_BANNER
                //if (TDSMAN.Classes.TDSMAN.T_DO_NOT_SHOW_AGAIN_KEYBOARDSHORTCUT_BANNER == true)
                //{
                //    pnlKeyBoardShortCutBanner.Visible = false;
                //}
                //    else
                //{
                //    pnlKeyBoardShortCutBanner.Visible = true;
                //}
                #endregion
                //--
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //-- 2026/02/18
                if (!bgCalcConsumption.IsBusy)
                    bgCalcConsumption.RunWorkerAsync();
            }
            catch
            {

            }
        }
        #endregion

        #region MstQuickLinks_Activated
        private void MstQuickLinks_Activated(object sender, EventArgs e)
        {
            //MstQuickLinks_Load(sender, e);

            #region ENABLE DISABLE MENU
            if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
            {
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION
                    || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION
                    || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE //--2022/04/11
                    || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION
                    || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)
                {
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE && TDSMAN.Classes.TDSMAN.T_IGNORE_MENU_MANAGEMENT_FLAG == true)
                    {
                        DataSet dsNew = new DataSet();
                        strSQL = @"SELECT  CHILD_MENU_ID  
                                 FROM  MST_MENU_CHILD 
                                WHERE  CHILD_MENU_ID NOT IN (SELECT CHILD_MENU_ID 
                                                             FROM TRN_USER_MENU_ACCESS 
                                                             WHERE SETUP_ID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + ")";
                        dsNew = dmlService.J_ExecSqlReturnDataSet(strSQL);

                        if (dsNew.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsNew.Tables[0].Rows)
                            {
                                if (Convert.ToInt32(dr["CHILD_MENU_ID"].ToString()) == 1)
                                {
                                    lblQuickLinks1.Enabled = false;
                                }
                                if (Convert.ToInt32(dr["CHILD_MENU_ID"].ToString()) == 2)
                                {
                                    lblQuickLinks2.Enabled = false;
                                }
                                if (Convert.ToInt32(dr["CHILD_MENU_ID"].ToString()) == 3)
                                {
                                    lblQuickLinks8.Enabled = false;
                                }
                                if (Convert.ToInt32(dr["CHILD_MENU_ID"].ToString()) == 5)
                                {
                                    lblQuickLinks3.Enabled = false;
                                }
                                if (Convert.ToInt32(dr["CHILD_MENU_ID"].ToString()) == 6)
                                {
                                    lblQuickLinks4.Enabled = false;
                                }
                                if (Convert.ToInt32(dr["CHILD_MENU_ID"].ToString()) == 20)
                                {
                                    lblQuickLinks5.Enabled = false;
                                }
                                if (Convert.ToInt32(dr["CHILD_MENU_ID"].ToString()) == 22)
                                {
                                    lblQuickLinks6.Enabled = false;
                                }
                                if (Convert.ToInt32(dr["CHILD_MENU_ID"].ToString()) == 26)
                                {
                                    lblQuickLinks7.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            //--
            blResize = false;
        }
        #endregion

        #region lblQuickLinksForms_Click
        private void lblQuickLinksForms_Click(object sender, EventArgs e)
        {
            //if (pnlQuickLinks.Height == 318)
            //    pnlQuickLinks.Height = 15;
            //else
            //    pnlQuickLinks.Height = 318;
        }
        #endregion

        #region lblQuickLinksForms_MouseMove
        private void lblQuickLinksForms_MouseMove(object sender, MouseEventArgs e)
        {

        }
        #endregion

        #region MouseClick

        #region lblQuickLinks1_Click
        private void lblQuickLinks1_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new MstCompany(), J_Var.frmMain, "Company Master");
        }
        #endregion

        #region lblQuickLinks2_Click
        private void lblQuickLinks2_Click(object sender, EventArgs e)
        {
            ////cmnService.J_ShowChildForm(new MstDeductee(), J_Var.frmMain, "Deductee Master");
            //-- 2021/01/21
            TDSMAN.Classes.TDSMAN.T_pTabFormCaption = T_FormNo.F24Q;
            //
            //TdsMan.CloseChildForm(new TrnRegularReturn(0), this);

            //cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form 24Q");
            //
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2026_27)
            {
                TdsMan.CloseChildForm(new TrnRegularReturn_26_27(0), this);
                cmnService.J_ShowChildForm(new TrnRegularReturn_26_27(0), J_Var.frmMain, "Form " + T_FormNoCaption.F24Q);
            }
            else
            {
                TdsMan.CloseChildForm(new TrnRegularReturn(0), this);
                cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form 24Q");
            }
        }
        #endregion

        #region lblQuickLinks3_Click
        private void lblQuickLinks3_Click(object sender, EventArgs e)
        {
            //cmnService.J_ShowChildForm(new TrnForm24Q(), J_Var.frmMain, "Form 24Q");
            TDSMAN.Classes.TDSMAN.T_pTabFormCaption = T_FormNo.F26Q;
            //
            //TdsMan.CloseChildForm(new TrnRegularReturn(0), this);
            ////
            //cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form 26Q");

            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2026_27)
            {
                TdsMan.CloseChildForm(new TrnRegularReturn_26_27(0), this);
                cmnService.J_ShowChildForm(new TrnRegularReturn_26_27(0), J_Var.frmMain, "Form " + T_FormNoCaption.F26Q);
            }
            else
            {
                TdsMan.CloseChildForm(new TrnRegularReturn(0), this);
                cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form 26Q");
            }
        }
        #endregion

        #region lblQuickLinks4_Click
        private void lblQuickLinks4_Click(object sender, EventArgs e)
        {
            //cmnService.J_ShowChildForm(new TrnForm26Q(), J_Var.frmMain, "Form 26Q");
            TDSMAN.Classes.TDSMAN.T_pTabFormCaption = T_FormNo.F27Q;
            //
            //TdsMan.CloseChildForm(new TrnRegularReturn(0), this);
            ////
            //cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form 27Q");

            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2026_27)
            {
                TdsMan.CloseChildForm(new TrnRegularReturn_26_27(0), this);
                cmnService.J_ShowChildForm(new TrnRegularReturn_26_27(0), J_Var.frmMain, "Form " + T_FormNoCaption.F27Q);
            }
            else
            {
                TdsMan.CloseChildForm(new TrnRegularReturn(0), this);
                cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form 27Q");
            }
        }
        #endregion

        #region lblQuickLinks5_Click
        private void lblQuickLinks5_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new Trn26QCorrectionReturn(), J_Var.frmMain, "Make Corrections");
        }
        #endregion

        #region lblQuickLinks6_Click
        private void lblQuickLinks6_Click(object sender, EventArgs e)
        {
            //cmnService.J_ShowChildForm(new TrnRequestConsolidatedFile(T_NSDL_FORM_TYPE.ConsolidatedStatement), J_Var.frmMain, "Request Consolidated File");
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.QuarterWiseReport, "Quarter wise Report");
        }
        #endregion

        #region lblQuickLinks7_Click
        private void lblQuickLinks7_Click(object sender, EventArgs e)
        {
            //cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.CertForm16A26Q, "Form No. 16A [26Q]");
            //-- 2021/01/21
            TDSMAN.Classes.TDSMAN.T_pTabFormCaption = T_FormNo.F27EQ;
            //
            //TdsMan.CloseChildForm(new TrnRegularReturn(0), this);
            ////
            //cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form 27EQ");
            TdsMan.CloseChildForm(new TrnRegularReturn_26_27(0), this);
            cmnService.J_ShowChildForm(new TrnRegularReturn_26_27(0), J_Var.frmMain, "Form " + T_FormNoCaption.F27EQ);
        }
        #endregion

        #region lblQuickLinks8_Click
        private void lblQuickLinks8_Click(object sender, EventArgs e)
        {
            //cmnService.J_ShowChildForm(new MstEmployee(), J_Var.frmMain, "Employee Master");
            cmnService.J_ShowChildForm(new MstReceiptNo(), J_Var.frmMain, "Receipt No. Master");
        }
        #endregion

        #region lblQuickLinks9_Click
        private void LblQuickLinks9_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new MstDeductee(), J_Var.frmMain, "Deductee Master");
        }
        #endregion

        #region lblQuickLinks10_Click
        private void LblQuickLinks10_Click(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
            {
                cmnService.J_UserMessage("Backup can only be performed from the server machine.\nPlease initiate the backup from the server to maintain data safety and integrity.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                cmnService.J_ShowChildForm(new SysBackupCompanyWise(), J_Var.frmMain, "Backup");
            }
        }
        #endregion

        #region lblQuickLinks11_Click
        private void LblQuickLinks11_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region lblQuickLinks12_Click
        private void LblQuickLinks12_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #endregion

        #region MouseMove

        #region lblQuickLinks1_MouseMove
        private void lblQuickLinks1_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    //lblQuickLinks1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks1.ForeColor = Color.Red;
                    lblQuickLinks2.ForeColor = Color.Black;
                    lblQuickLinks3.ForeColor = Color.Black;
                    lblQuickLinks4.ForeColor = Color.Black;
                    lblQuickLinks5.ForeColor = Color.Black;
                    lblQuickLinks6.ForeColor = Color.Black;
                    lblQuickLinks7.ForeColor = Color.Black;
                    lblQuickLinks8.ForeColor = Color.Black;
                    lblQuickLinks9.ForeColor = Color.Black;
                    lblQuickLinks10.ForeColor = Color.Black;
                    //lblQuickLinks11.ForeColor = Color.Black;
                    //lblQuickLinks12.ForeColor = Color.Black;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.RosyBrown;
                    lblQuickLinks2.ForeColor = Color.White;
                    lblQuickLinks3.ForeColor = Color.White;
                    lblQuickLinks4.ForeColor = Color.White;
                    lblQuickLinks5.ForeColor = Color.White;
                    lblQuickLinks6.ForeColor = Color.White;
                    lblQuickLinks7.ForeColor = Color.White;
                    lblQuickLinks8.ForeColor = Color.White;
                }
            }
        }
        #endregion

        #region lblQuickLinks2_MouseMove
        private void lblQuickLinks2_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    lblQuickLinks1.ForeColor = Color.Black;
                    //lblQuickLinks2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks2.ForeColor = Color.Red;
                    lblQuickLinks3.ForeColor = Color.Black;
                    lblQuickLinks4.ForeColor = Color.Black;
                    lblQuickLinks5.ForeColor = Color.Black;
                    lblQuickLinks6.ForeColor = Color.Black;
                    lblQuickLinks7.ForeColor = Color.Black;
                    lblQuickLinks8.ForeColor = Color.Black;
                    lblQuickLinks9.ForeColor = Color.Black;
                    lblQuickLinks10.ForeColor = Color.Black;
                    //lblQuickLinks11.ForeColor = Color.Black;
                    //lblQuickLinks12.ForeColor = Color.Black;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.White;
                    lblQuickLinks2.ForeColor = Color.RosyBrown;
                    lblQuickLinks3.ForeColor = Color.White;
                    lblQuickLinks4.ForeColor = Color.White;
                    lblQuickLinks5.ForeColor = Color.White;
                    lblQuickLinks6.ForeColor = Color.White;
                    lblQuickLinks7.ForeColor = Color.White;
                    lblQuickLinks8.ForeColor = Color.White;
                }
            }
        }
        #endregion

        #region lblQuickLinks3_MouseMove
        private void lblQuickLinks3_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    lblQuickLinks1.ForeColor = Color.Black;
                    lblQuickLinks2.ForeColor = Color.Black;
                    //lblQuickLinks3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks3.ForeColor = Color.Red;
                    lblQuickLinks4.ForeColor = Color.Black;
                    lblQuickLinks5.ForeColor = Color.Black;
                    lblQuickLinks6.ForeColor = Color.Black;
                    lblQuickLinks7.ForeColor = Color.Black;
                    lblQuickLinks8.ForeColor = Color.Black;
                    lblQuickLinks9.ForeColor = Color.Black;
                    lblQuickLinks10.ForeColor = Color.Black;
                    //lblQuickLinks11.ForeColor = Color.Black;
                    //lblQuickLinks12.ForeColor = Color.Black;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.White;
                    lblQuickLinks2.ForeColor = Color.White;
                    lblQuickLinks3.ForeColor = Color.RosyBrown;
                    lblQuickLinks4.ForeColor = Color.White;
                    lblQuickLinks5.ForeColor = Color.White;
                    lblQuickLinks6.ForeColor = Color.White;
                    lblQuickLinks7.ForeColor = Color.White;
                    lblQuickLinks8.ForeColor = Color.White;
                }
            }
        }
        #endregion

        #region lblQuickLinks4_MouseMove
        private void lblQuickLinks4_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    lblQuickLinks1.ForeColor = Color.Black;
                    lblQuickLinks2.ForeColor = Color.Black;
                    lblQuickLinks3.ForeColor = Color.Black;
                    //lblQuickLinks4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks4.ForeColor = Color.Red;
                    lblQuickLinks5.ForeColor = Color.Black;
                    lblQuickLinks6.ForeColor = Color.Black;
                    lblQuickLinks7.ForeColor = Color.Black;
                    lblQuickLinks8.ForeColor = Color.Black;
                    lblQuickLinks9.ForeColor = Color.Black;
                    lblQuickLinks10.ForeColor = Color.Black;
                    //lblQuickLinks11.ForeColor = Color.Black;
                    //lblQuickLinks12.ForeColor = Color.Black;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.White;
                    lblQuickLinks2.ForeColor = Color.White;
                    lblQuickLinks3.ForeColor = Color.White;
                    lblQuickLinks4.ForeColor = Color.RosyBrown;
                    lblQuickLinks5.ForeColor = Color.White;
                    lblQuickLinks6.ForeColor = Color.White;
                    lblQuickLinks7.ForeColor = Color.White;
                    lblQuickLinks8.ForeColor = Color.White;
                }
            }
        }
        #endregion

        #region lblQuickLinks5_MouseMove
        private void lblQuickLinks5_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    lblQuickLinks1.ForeColor = Color.Black;
                    lblQuickLinks2.ForeColor = Color.Black;
                    lblQuickLinks3.ForeColor = Color.Black;
                    lblQuickLinks4.ForeColor = Color.Black;
                    //lblQuickLinks5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks5.ForeColor = Color.Red;
                    lblQuickLinks6.ForeColor = Color.Black;
                    lblQuickLinks7.ForeColor = Color.Black;
                    lblQuickLinks8.ForeColor = Color.Black;
                    lblQuickLinks9.ForeColor = Color.Black;
                    lblQuickLinks10.ForeColor = Color.Black;
                    //lblQuickLinks11.ForeColor = Color.Black;
                    //lblQuickLinks12.ForeColor = Color.Black;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.White;
                    lblQuickLinks2.ForeColor = Color.White;
                    lblQuickLinks3.ForeColor = Color.White;
                    lblQuickLinks4.ForeColor = Color.White;
                    lblQuickLinks5.ForeColor = Color.RosyBrown;
                    lblQuickLinks6.ForeColor = Color.White;
                    lblQuickLinks7.ForeColor = Color.White;
                    lblQuickLinks8.ForeColor = Color.White;
                }
            }
        }
        #endregion

        #region lblQuickLinks6_MouseMove
        private void lblQuickLinks6_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    lblQuickLinks1.ForeColor = Color.Black;
                    lblQuickLinks2.ForeColor = Color.Black;
                    lblQuickLinks3.ForeColor = Color.Black;
                    lblQuickLinks4.ForeColor = Color.Black;
                    lblQuickLinks5.ForeColor = Color.Black;
                    //lblQuickLinks6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks6.ForeColor = Color.Red;
                    lblQuickLinks7.ForeColor = Color.Black;
                    lblQuickLinks8.ForeColor = Color.Black;
                    lblQuickLinks9.ForeColor = Color.Black;
                    lblQuickLinks10.ForeColor = Color.Black;
                    //lblQuickLinks11.ForeColor = Color.Black;
                    //lblQuickLinks12.ForeColor = Color.Black;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.White;
                    lblQuickLinks2.ForeColor = Color.White;
                    lblQuickLinks3.ForeColor = Color.White;
                    lblQuickLinks4.ForeColor = Color.White;
                    lblQuickLinks5.ForeColor = Color.White;
                    lblQuickLinks6.ForeColor = Color.RosyBrown;
                    lblQuickLinks7.ForeColor = Color.White;
                    lblQuickLinks8.ForeColor = Color.White;
                }
            }
        }
        #endregion

        #region lblQuickLinks7_MouseMove
        private void lblQuickLinks7_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    lblQuickLinks1.ForeColor = Color.Black;
                    lblQuickLinks2.ForeColor = Color.Black;
                    lblQuickLinks3.ForeColor = Color.Black;
                    lblQuickLinks4.ForeColor = Color.Black;
                    lblQuickLinks5.ForeColor = Color.Black;
                    lblQuickLinks6.ForeColor = Color.Black;
                    //lblQuickLinks7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks7.ForeColor = Color.Red;
                    lblQuickLinks8.ForeColor = Color.Black;
                    lblQuickLinks9.ForeColor = Color.Black;
                    lblQuickLinks10.ForeColor = Color.Black;
                    //lblQuickLinks11.ForeColor = Color.Black;
                    //lblQuickLinks12.ForeColor = Color.Black;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.White;
                    lblQuickLinks2.ForeColor = Color.White;
                    lblQuickLinks3.ForeColor = Color.White;
                    lblQuickLinks4.ForeColor = Color.White;
                    lblQuickLinks5.ForeColor = Color.White;
                    lblQuickLinks6.ForeColor = Color.White;
                    lblQuickLinks7.ForeColor = Color.RosyBrown;
                    lblQuickLinks8.ForeColor = Color.White;
                }
            }
        }
        #endregion

        #region lblQuickLinks8_MouseMove
        private void lblQuickLinks8_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    lblQuickLinks1.ForeColor = Color.Black;
                    lblQuickLinks2.ForeColor = Color.Black;
                    lblQuickLinks3.ForeColor = Color.Black;
                    lblQuickLinks4.ForeColor = Color.Black;
                    lblQuickLinks5.ForeColor = Color.Black;
                    lblQuickLinks6.ForeColor = Color.Black;
                    lblQuickLinks7.ForeColor = Color.Black;
                    //lblQuickLinks8.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks8.ForeColor = Color.Red;
                    lblQuickLinks9.ForeColor = Color.Black;
                    lblQuickLinks10.ForeColor = Color.Black;
                    //lblQuickLinks11.ForeColor = Color.Black;
                    //lblQuickLinks12.ForeColor = Color.Black;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.White;
                    lblQuickLinks2.ForeColor = Color.White;
                    lblQuickLinks3.ForeColor = Color.White;
                    lblQuickLinks4.ForeColor = Color.White;
                    lblQuickLinks5.ForeColor = Color.White;
                    lblQuickLinks6.ForeColor = Color.White;
                    lblQuickLinks7.ForeColor = Color.White;
                    lblQuickLinks8.ForeColor = Color.RosyBrown;
                }
            }
        }
        #endregion


        private void LblQuickLinks9_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    lblQuickLinks1.ForeColor = Color.Black;
                    lblQuickLinks2.ForeColor = Color.Black;
                    lblQuickLinks3.ForeColor = Color.Black;
                    lblQuickLinks4.ForeColor = Color.Black;
                    lblQuickLinks5.ForeColor = Color.Black;
                    lblQuickLinks6.ForeColor = Color.Black;
                    lblQuickLinks7.ForeColor = Color.Black;
                    //lblQuickLinks8.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks8.ForeColor = Color.Black;
                    lblQuickLinks9.ForeColor = Color.Red;
                    lblQuickLinks10.ForeColor = Color.Black;
                    //lblQuickLinks11.ForeColor = Color.Black;
                    //lblQuickLinks12.ForeColor = Color.Black;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.White;
                    lblQuickLinks2.ForeColor = Color.White;
                    lblQuickLinks3.ForeColor = Color.White;
                    lblQuickLinks4.ForeColor = Color.White;
                    lblQuickLinks5.ForeColor = Color.White;
                    lblQuickLinks6.ForeColor = Color.White;
                    lblQuickLinks7.ForeColor = Color.White;
                    lblQuickLinks8.ForeColor = Color.RosyBrown;
                }
            }
        }

        private void LblQuickLinks10_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    lblQuickLinks1.ForeColor = Color.Black;
                    lblQuickLinks2.ForeColor = Color.Black;
                    lblQuickLinks3.ForeColor = Color.Black;
                    lblQuickLinks4.ForeColor = Color.Black;
                    lblQuickLinks5.ForeColor = Color.Black;
                    lblQuickLinks6.ForeColor = Color.Black;
                    lblQuickLinks7.ForeColor = Color.Black;
                    //lblQuickLinks8.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks8.ForeColor = Color.Black;
                    lblQuickLinks9.ForeColor = Color.Black;
                    lblQuickLinks10.ForeColor = Color.Red;
                    //lblQuickLinks11.ForeColor = Color.Black;
                    //lblQuickLinks12.ForeColor = Color.Black;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.White;
                    lblQuickLinks2.ForeColor = Color.White;
                    lblQuickLinks3.ForeColor = Color.White;
                    lblQuickLinks4.ForeColor = Color.White;
                    lblQuickLinks5.ForeColor = Color.White;
                    lblQuickLinks6.ForeColor = Color.White;
                    lblQuickLinks7.ForeColor = Color.White;
                    lblQuickLinks8.ForeColor = Color.RosyBrown;
                }
            }
        }

        private void LblQuickLinks11_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    lblQuickLinks1.ForeColor = Color.Black;
                    lblQuickLinks2.ForeColor = Color.Black;
                    lblQuickLinks3.ForeColor = Color.Black;
                    lblQuickLinks4.ForeColor = Color.Black;
                    lblQuickLinks5.ForeColor = Color.Black;
                    lblQuickLinks6.ForeColor = Color.Black;
                    lblQuickLinks7.ForeColor = Color.Black;
                    //lblQuickLinks8.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks8.ForeColor = Color.Black;
                    lblQuickLinks9.ForeColor = Color.Black;
                    lblQuickLinks10.ForeColor = Color.Black;
                    //lblQuickLinks11.ForeColor = Color.Red;
                    //lblQuickLinks12.ForeColor = Color.Black;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.White;
                    lblQuickLinks2.ForeColor = Color.White;
                    lblQuickLinks3.ForeColor = Color.White;
                    lblQuickLinks4.ForeColor = Color.White;
                    lblQuickLinks5.ForeColor = Color.White;
                    lblQuickLinks6.ForeColor = Color.White;
                    lblQuickLinks7.ForeColor = Color.White;
                    lblQuickLinks8.ForeColor = Color.RosyBrown;
                }
            }
        }

        private void LblQuickLinks12_MouseMove(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            {
                if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
                {
                    lblQuickLinks1.ForeColor = Color.Black;
                    lblQuickLinks2.ForeColor = Color.Black;
                    lblQuickLinks3.ForeColor = Color.Black;
                    lblQuickLinks4.ForeColor = Color.Black;
                    lblQuickLinks5.ForeColor = Color.Black;
                    lblQuickLinks6.ForeColor = Color.Black;
                    lblQuickLinks7.ForeColor = Color.Black;
                    //lblQuickLinks8.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D31D26");
                    lblQuickLinks8.ForeColor = Color.Black;
                    lblQuickLinks9.ForeColor = Color.Black;
                    lblQuickLinks10.ForeColor = Color.Black;
                    //lblQuickLinks11.ForeColor = Color.Black;
                    //lblQuickLinks12.ForeColor = Color.Red;
                }
                else
                {
                    lblQuickLinks1.ForeColor = Color.White;
                    lblQuickLinks2.ForeColor = Color.White;
                    lblQuickLinks3.ForeColor = Color.White;
                    lblQuickLinks4.ForeColor = Color.White;
                    lblQuickLinks5.ForeColor = Color.White;
                    lblQuickLinks6.ForeColor = Color.White;
                    lblQuickLinks7.ForeColor = Color.White;
                    lblQuickLinks8.ForeColor = Color.RosyBrown;
                }
            }
        }

        #endregion

        #region MouseLeave

        #region lblQuickLinks1_MouseLeave
        private void lblQuickLinks1_MouseLeave(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            //{
            //    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
            //        lblQuickLinks1.ForeColor = Color.Black;
            //    else
            //        lblQuickLinks1.ForeColor = Color.White;
            //}
            lblQuickLinks1.ForeColor = Color.Black;
        }
        #endregion

        #region lblQuickLinks2_MouseLeave
        private void lblQuickLinks2_MouseLeave(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            //{
            //    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
            //        lblQuickLinks2.ForeColor = Color.Black;
            //    else
            //        lblQuickLinks2.ForeColor = Color.White;
            //}
            lblQuickLinks2.ForeColor = Color.Black;
        }
        #endregion

        #region lblQuickLinks3_MouseLeave
        private void lblQuickLinks3_MouseLeave(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            //{
            //    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
            //        lblQuickLinks3.ForeColor = Color.Black;
            //    else
            //        lblQuickLinks3.ForeColor = Color.White;
            //}
            lblQuickLinks3.ForeColor = Color.Black;
        }
        #endregion

        #region lblQuickLinks4_MouseLeave
        private void lblQuickLinks4_MouseLeave(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            //{
            //    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
            //        lblQuickLinks4.ForeColor = Color.Black;
            //    else
            //        lblQuickLinks4.ForeColor = Color.White;
            //}
            lblQuickLinks4.ForeColor = Color.Black;
        }
        #endregion

        #region lblQuickLinks5_MouseLeave
        private void lblQuickLinks5_MouseLeave(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            //{
            //    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
            //        lblQuickLinks5.ForeColor = Color.Black;
            //    else
            //        lblQuickLinks5.ForeColor = Color.White;
            //}
            lblQuickLinks5.ForeColor = Color.Black;
        }
        #endregion

        #region lblQuickLinks6_MouseLeave
        private void lblQuickLinks6_MouseLeave(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            //{
            //    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
            //        lblQuickLinks6.ForeColor = Color.Black;
            //    else
            //        lblQuickLinks6.ForeColor = Color.White;
            //}
            lblQuickLinks6.ForeColor = Color.Black;
        }
        #endregion

        #region lblQuickLinks7_MouseLeave
        private void lblQuickLinks7_MouseLeave(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            //{
            //    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
            //        lblQuickLinks7.ForeColor = Color.Black;
            //    else
            //        lblQuickLinks7.ForeColor = Color.White;
            //}
            lblQuickLinks7.ForeColor = Color.Black;
        }
        #endregion

        #region lblQuickLinks8_MouseLeave
        private void lblQuickLinks8_MouseLeave(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear <= (int)Software_Version.FY2022_23)
            //{
            //    if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear >= (int)Software_Version.FY2013_14)
            //        lblQuickLinks8.ForeColor = Color.Black;
            //    else
            //        lblQuickLinks8.ForeColor = Color.White;
            //}
            lblQuickLinks8.ForeColor = Color.Black;
        }
        #endregion

        #region LblQuickLinks9_MouseLeave
        private void LblQuickLinks9_MouseLeave(object sender, EventArgs e)
        {
            lblQuickLinks9.ForeColor = Color.Black;
        }
        #endregion

        #region LblQuickLinks10_MouseLeave
        private void LblQuickLinks10_MouseLeave(object sender, EventArgs e)
        {
            lblQuickLinks10.ForeColor = Color.Black;
        }
        #endregion

        #region LblQuickLinks11_MouseLeave
        private void LblQuickLinks11_MouseLeave(object sender, EventArgs e)
        {
            //lblQuickLinks11.ForeColor = Color.Black;
        }
        #endregion

        #region LblQuickLinks12_MouseLeave
        private void LblQuickLinks12_MouseLeave(object sender, EventArgs e)
        {
            //lblQuickLinks12.ForeColor = Color.Black;
        }
        #endregion

        #endregion

        #region lnkClickHere_LinkClicked
        private void lnkClickHere_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.tdsman.com/pricing.asp");
        }
        #endregion

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region lnkTDSMAN_LinkClicked
        private void lnkTDSMAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.tdsman.com");
        }
        #endregion


        #region btnCloseKeyBoardShortCutBanner_Click
        private void btnCloseKeyBoardShortCutBanner_Click(object sender, EventArgs e)
        {
            if (chkDoNotShowAgain.Checked == true)
            {
                strSQL = "UPDATE MST_SETUP SET DO_NOT_SHOW_AGAIN_KEYBOARDSHORTCUT_BANNER = 1";
                dmlService.J_ExecSql(strSQL);
            }
            pnlKeyBoardShortCutBanner.Visible = false;
        }
        #endregion


        #region btnCloseKeyBoardShortCutBanner_MouseMove
        private void btnCloseKeyBoardShortCutBanner_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnCloseKeyBoardShortCutBanner, "Click to close this window");
        }
        #endregion

        #endregion

        #region User Define Functions

        #region LoadFormReport
        private bool LoadFormReport(string FormReportType, string FormReportModule, string FormReportTitle)
        {
            try
            {
                //--
                if (FormReportType.ToUpper() == "E")
                {
                    object oForm = new object();
                    Type tForm = Assembly.GetExecutingAssembly().GetType(FormReportModule);
                    if (tForm != null)
                    {
                        oForm = Activator.CreateInstance(tForm);

                        ((Form)oForm).MdiParent = mdiTDSMAN.ActiveForm;
                        ((Form)oForm).WindowState = FormWindowState.Maximized;
                        ((Form)oForm).Text = FormReportTitle;
                        ((Form)oForm).Show();
                    }
                }
                else
                {
                    //RptDialog childForm = new RptDialog();
                    //foreach (Form frmChild in Parent.MdiChildren)
                    //    if (string.Compare(childForm.Name, frmChild.Name, true) == 0)
                    //        frmChild.Dispose();
                    //childForm.WindowState = FormWindowState.Maximized;
                    //childForm.Text = ReportTitle;
                    //childForm.MdiParent = Parent;
                    //childForm.SetRptDialogOptions(enmReport, ReportTitle);
                    //childForm.Show();
                    //----------------------
                    RptDialog childForm = new RptDialog();
                    //foreach (Form frmChild in Parent.MdiChildren)
                    //    if (string.Compare(childForm.Name, frmChild.Name, true) == 0)
                    //        frmChild.Dispose();
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.MdiParent = mdiTDSMAN.ActiveForm;
                    //childForm.Text = FormReportTitle;
                    childForm.SetRptDialogOptions((J_Reports)Enum.Parse(typeof(J_Reports), FormReportModule), FormReportTitle);
                    childForm.Show();
                    //------------------------
                }
                //----------------------------
                return true;
            }
            catch
            {
                return false;
            }
        }





        #endregion

        #endregion


        #region lnkWebsite_LinkClicked
        private void lnkWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.tdsman.com/");
        }
        #endregion


        #region lblHelpEmail_Click
        private void lblHelpEmail_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:info@tdsman.com");
        }
        #endregion

        private void lblWebsite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.tdsman.com/");
        }

        #region lblOpenDashboard_Click
        private void lblOpenDashboard_Click(object sender, EventArgs e)
        {
            //foreach (Form frmChild in Parent.MdiChildren)
            //{
            //    if (string.Compare(Child.Name, frmChild.Name, true) == 0)
            //    {
            //        frmChild.Activate();
            //        return;
            //    }
            //}
            //Child.WindowState = FormWindowState.Maximized;
            //Child.Text = FormTitle;
            //Child.MdiParent = Parent;
            //Child.Show();
            //cmnService.J_ShowChildForm(new TrnDashboard(), J_Var.frmMain, "Dashboard");
            //
            TdsMan.GetSetup();
            //
            //TrnDashboard TrnDashboard = new FormTrn.TrnDashboard();
            TrnTabbedDashboard TrnDashboard = new FormTrn.TrnTabbedDashboard();
            TrnDashboard.Text = "Dashboard";
            TrnDashboard.MdiParent = J_Var.frmMain;
            TrnDashboard.WindowState = FormWindowState.Normal;
            TrnDashboard.Show();
        }
        #endregion

        #region lblQuickLinks9_MouseClick
        private void lblQuickLinks9_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ctxtMnuBulkImport.Show(lblQuickLinks9, new Point(e.X, e.Y));
        }
        #endregion


        #region mnuExcel_Click
        private void mnuExcel_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnExcelImportIncremental(0), J_Var.frmMain, "Import from Excel file");
        }
        #endregion

        #region mnuCSV_Click
        private void mnuCSV_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnExcelImportIncremental(1), J_Var.frmMain, "Import from CSV file");
        }

        #endregion


        #region BgCalcConsumption_DoWork
        private void BgCalcConsumption_DoWork(object sender, DoWorkEventArgs e)
        {
            #region GET CONSUMPTION COUNT //-- 2025/07/17
            string SoftwareVersion = TDSMAN.Classes.TDSMAN.T_pEditionType + "." + TDSMAN.Classes.TDSMAN.T_pPackageFAYear;
            long lngRegDeducteeCount = 0, lngRegSalaryCount = 0, lngReg194PCount = 0, lngTotalRecords = 0;
            long lngCorrDeducteeAddCount = 0, lngCorrDeducteeModifyNullifyCount = 0, lngCorrDeducteePANChangeCount = 0, lngCorrSalaryChangeCount = 0, lngCorrSalaryPANChangeCount = 0, lngCorr194PChangeCount = 0, lngCorr194PPANChangeCount = 0;
            //
            TdsMan.Total_Consumption_Count(Convert.ToDouble(SoftwareVersion),
                                    out lngRegDeducteeCount,
                                    out lngRegSalaryCount,
                                    out lngReg194PCount,
                                    out lngCorrDeducteeAddCount,
                                    out lngCorrDeducteeModifyNullifyCount,
                                    out lngCorrDeducteePANChangeCount,
                                    out lngCorrSalaryChangeCount,
                                    out lngCorrSalaryPANChangeCount,
                                    out lngCorr194PChangeCount,
                                    out lngCorr194PPANChangeCount);
            lngTotalRecords = lngRegDeducteeCount + lngRegSalaryCount + lngReg194PCount + lngCorrDeducteeAddCount + lngCorrDeducteeModifyNullifyCount +
                                lngCorrDeducteePANChangeCount + lngCorrSalaryChangeCount + lngCorrSalaryPANChangeCount + lngCorr194PChangeCount +
                                lngCorr194PPANChangeCount;
            //
            e.Result = lngTotalRecords;
            //
            #region comment
            //string strConsumptionPercentage = ""; double dblConsumptionPercentage = 0;
            //if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
            //{
            //    dblConsumptionPercentage = (double)lngTotalRecords * 100 / TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions;
            //    strConsumptionPercentage = "(" + Convert.ToString(dblConsumptionPercentage) + "%)";
            //    lblConsumption.Text = "Utilized: " + lngTotalRecords.ToString("N0") + " / " + TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions.ToString("N0") + " records " + strConsumptionPercentage;
            //    //                
            //}
            //else
            //{
            //    dblConsumptionPercentage = (double)lngTotalRecords * 100 / TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountTrialEditions;
            //    strConsumptionPercentage = "(" + Convert.ToString(dblConsumptionPercentage) + "%)";
            //    lblConsumption.Text = "Utilized: " + lngTotalRecords.ToString("N0") + " / " + TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountTrialEditions.ToString("N0") + " records " + strConsumptionPercentage;
            //}
            ////--
            //if (dblConsumptionPercentage >= 50)
            //{
            //    ////-- UNCOMMENT PLS
            //    lblConsumption.Visible = true;//-- COMMENTED BEFORE IMPLEMENTATION
            //    if (dblConsumptionPercentage >= 80)
            //    {
            //        lblConsumption.BackColor = Color.LightGray;
            //        lblConsumption.ForeColor = Color.Red;
            //        lblConsumption.BorderStyle = BorderStyle.FixedSingle;
            //    }
            //    else
            //    {
            //        lblConsumption.BackColor = Color.Transparent;
            //        lblConsumption.ForeColor = Color.White;
            //        lblConsumption.BorderStyle = BorderStyle.None;
            //    }
            //}
            //else
            //    lblConsumption.Visible = false;
            #endregion
            //
            #endregion
        }
        #endregion

        #region BgCalcConsumption_RunWorkerCompleted
        private void BgCalcConsumption_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            #region SHOW CONSUMPTION COUNT //-- 2026/02/19
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            long lngTotalRecords = Convert.ToInt64(e.Result);
            double dblConsumptionPercentage = 0;
            string strConsumptionPercentage = "";

            long maxCapacity = 0;

            if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
            {
                #region TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                    TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountProfessional;
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE) //-- 2022/04/11
                    TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountClh;
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION)  //-- 2016/01/29
                    TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterprise;
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                    TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountStandard;
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.LITE_EDITION)
                    TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountLite;
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)  //-- 2021/01/13
                    TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterpriseLite;
                else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)  //-- 2024/06/06
                    TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEnterpriseUltimate;
                #endregion
                //-- 2026/02/26
                maxCapacity = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions;
            }
            else
                maxCapacity = TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountTrialEditions;

            dblConsumptionPercentage = (double)lngTotalRecords * 100 / maxCapacity;
            strConsumptionPercentage = "(" + dblConsumptionPercentage.ToString("0.00") + "%)";

            lblConsumption.Text = "Utilized: " +
                                  lngTotalRecords.ToString("N0") +
                                  " / " +
                                  maxCapacity.ToString("N0") +
                                  " records " +
                                  strConsumptionPercentage;
            // Styling
            if (dblConsumptionPercentage >= 50)
            {
                lblConsumption.Visible = true;

                if (dblConsumptionPercentage >= 80)
                {
                    lblConsumption.BackColor = Color.LightGray;
                    lblConsumption.ForeColor = Color.Red;
                    lblConsumption.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    lblConsumption.BackColor = Color.Transparent;
                    lblConsumption.ForeColor = Color.White;
                    lblConsumption.BorderStyle = BorderStyle.None;
                }
            }
            else
            {
                lblConsumption.Visible = false;
            }

            #endregion
        }
        #endregion

    }
}