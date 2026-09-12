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

using TDSMAN.Classes;
using TDSMAN.FormTrn;
using TDSMAN.FormCmn;
using TDSMAN.FormUtl;

#endregion


namespace TDSMAN.FormSys
{
    public partial class SysUpgradeMU : Form
    {
        #region System Generated Code
        public SysUpgradeMU()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
        //
        DMLService dmlService = new DMLService();
        DateService dtService = new DateService();
        CommonService cmnService = new CommonService();
        //JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        // WEB CLASS
        TDSMAN_WEB.Registration RegistrationMU = new TDSMAN.TDSMAN_WEB.Registration();
        //--            
        IDataReader drdShowDeducteePAN = null;
        ToolTip tllTip = new ToolTip();
        //
        string strSerialNo = "";
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        #endregion

        #region SysUpgradeMU_Load
        private void SysUpgradeMU_Load(object sender, EventArgs e)
        {
            txtSerialNoMU.Text = "";
            //
            #region FORM DESIGN
            lblCaption1.Visible = true;
            lblCaption1.Text = "Provide the Multi User upgrade serial no. provided & click Next.";
            lblNB1.Visible = true;
            lblNB1.Text = "NOTE : Online is an auto upgradation process. For offline you have to contact our helpdesk to get the activation code.";
            lblCaption2.Visible = true;
            lblCaption2.Text = "Please provide the activation code provided by our helpdesk for Multi User upgrade.";
            lblCaption3.Visible = true;
            lblSteps.Visible = true;
            lblStep1.Visible = true;
            lblStep3.Visible = true;
            //lblCaption3.Text = "Your software has been successfully upgraded to Multi User.\n\nShare the server folder & give Read/Write permission.\nInstall Client softwares in other machines.\nTo connect other machines with the server, install client software using the CD & connect it with the server.";
            #endregion
            //
            if (TdsMan.T_CheckInternetConnectivty() == true)
                rbnOnline.Checked = true;
            else
                rbnOffline.Checked = true;
            //
            strSerialNo = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                                                                   T_RegistrationInfo.Serial_No.ToString());
            //
            txtSerialNoMU.Select();
        }
        #endregion

        #region BtnNext_Click
        private void BtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                #region tbpGetSerialNo
                if (tbcUpgradeMU.SelectedTab == tbpGetSerialNo)
                {
                    #region VALIDATE
                    //if (txtSerialNo.Text == "")
                    //{
                    //    cmnService.J_UserMessage("Enter the Serial No.");
                    //    txtSerialNo.Select();
                    //    return;
                    //}
                    //else
                    //{
                    //    if (txtSerialNo.Text.Trim() != cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                    //                                               T_RegistrationInfo.Serial_No.ToString()))
                    //    {
                    //        cmnService.J_UserMessage("Wrong Serial No. entered");
                    //        txtSerialNo.Select();
                    //        return;
                    //    }
                    //}
                    //
                    if (txtSerialNoMU.Text == "")
                    {
                        cmnService.J_UserMessage("Enter the Multi User Serial No.");
                        txtSerialNoMU.Select();
                        return;
                    }
                    #endregion
                    //
                    if (rbnOnline.Checked == true)
                    {
                        lblPleaseWaitMessage.Visible = true;
                        if (TdsMan.T_CheckInternetConnectivty() == true)
                        {
                            string strMessage = "";
                            int intAllowedNode = 0;
                            //-- GO TO WEB
                            // SEND SERIAL NO. & MU SERIAL NO. & SOFTWARE EDITION & FINANCIAL YEAR
                            // GET THE RESPONSE
                            if (RegistrationMU.Upgrade_to_Multi_user(strSerialNo, TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), TDSMAN.Classes.TDSMAN.T_pEditionType, txtSerialNoMU.Text.Trim(), out strMessage, out intAllowedNode) == false)
                            {
                                // IF FAILED GIVE ERROR MESSAGE
                                lblPleaseWaitMessage.Visible = false;
                                cmnService.J_UserMessage(strMessage);
                                txtSerialNoMU.Select();
                                return;
                            }
                            else
                            {
                                // IF SUCCESS WRITE TO REGISTRY & GO TO FINISHED TAB
                                //-- WRITE TO REGISTRY
                                lblPleaseWaitMessage.Visible = false;
                                T_WriteToRegistry(txtSerialNoMU.Text.Trim(), TdsMan.T_GenerateActivationNumberMU(strSerialNo, txtSerialNoMU.Text.Trim()));
                                tbcUpgradeMU.SelectTab(tbpFinished);
                                //--
                                //lblNoOfUsers.Visible = true;
                                //lblNoOfUsers.Text = "Client(s) can be added : " + intAllowedNode.ToString(); // out variable of NoOfUsers
                                lblStep2.Visible = true;
                                if(intAllowedNode>1)
                                {
                                    lblStep2.Text = "2) You can connect " + intAllowedNode.ToString() + " ( " + cmnService.J_Inwords(intAllowedNode).Replace("only.","").Trim() + " ) additional clients (users)";
                                }
                                else
                                {
                                    lblStep2.Text = "2) You can connect " + intAllowedNode.ToString() + " ( " + cmnService.J_Inwords(intAllowedNode).Replace("only.", "").Trim() + " ) additional client (user)";
                                }
                                //--
                                BtnNext.Text = "&Finish";
                                BtnNext.Select();
                            }
                            //
                        }
                        else
                        {
                            cmnService.J_UserMessage("Internet Connectivity not found");
                        }
                        //
                        lblPleaseWaitMessage.Visible = false;
                    }
                    else if (rbnOffline.Checked == true)
                    {
                        lblPleaseWaitMessage.Visible = false;
                        tbcUpgradeMU.SelectTab(tbpGetActivationCode);
                        BtnBack.Visible = true;
                        txtActivationCodeMU.Select();
                        //cmnService.J_Encode(
                    }
                }
                #endregion

                #region tbpGetActivationCode
                else if (tbcUpgradeMU.SelectedTab == tbpGetActivationCode)
                {
                    #region VALIDATE
                    if (txtActivationCodeMU.Text == "")
                    {
                        cmnService.J_UserMessage("Enter the Activation Code");
                        txtActivationCodeMU.Select();
                        return;
                    }
                    //
                    //
                    if (txtActivationCodeMU.Text != TdsMan.T_GenerateActivationNumberMU(strSerialNo, txtSerialNoMU.Text.Trim()))
                    {
                        cmnService.J_UserMessage("Wrong Activation Code");
                        txtActivationCodeMU.Select();
                        return;
                    }
                    #endregion
                    //
                    //-- WRITE TO REGISTRY
                    T_WriteToRegistry(txtSerialNoMU.Text.Trim(), txtActivationCodeMU.Text.Trim());
                    //
                    tbcUpgradeMU.SelectTab(tbpFinished);
                    BtnNext.Text = "&Finish";
                    BtnNext.Select();
                    BtnBack.Visible = false;
                }
                #endregion

                #region tbpFinished
                else if (tbcUpgradeMU.SelectedTab == tbpFinished)
                {
                    cmnService.J_UserMessage("TDSMAN application will close now.\nPlease restart the application to experience the changed effect.");
                    Application.Exit();
                }
                #endregion

            }
            catch (Exception err)
            {
                lblPleaseWaitMessage.Visible = false;
                cmnService.J_UserMessage(err.Message);
            }

        }
        #endregion

        #region BtnBack_Click
        private void BtnBack_Click(object sender, EventArgs e)
        {
            tbcUpgradeMU.SelectTab(tbpGetSerialNo);
            BtnBack.Visible = false;
        }
        #endregion        

        #region T_WriteToRegistry
        public bool T_WriteToRegistry(string SerialNoMU, string ActivationCodeMU)
        {
            try
            {
                // CREATE REGISTRY
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName);
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder());
                // SET SERIAL NO
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                                                T_RegistrationInfo.Serial_No_MU.ToString(), SerialNoMU);
                // SET ACTIVATION CODE
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                                                T_RegistrationInfo.Activ_Code_MU.ToString(), ActivationCodeMU);
                //
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion               
                
        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=BdzcqyuEg-Q");
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0039", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0010", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
#endregion
    }
}