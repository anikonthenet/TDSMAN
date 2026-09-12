#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#endregion

using TDSMAN.Classes;

namespace TDSMAN.FormPar
{
    public partial class ParBuyNow : Form
    {
        #region System Generated Code
        public ParBuyNow()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
        ToolTip tltip = new ToolTip();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        // WEB CLASS
        TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
        #endregion

        #region ParBuyNow_Activated
        private void ParBuyNow_Activated(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;
            //lblTop.Text = "You are using a trial copy\nUpgrade to Standard Edition and use the software without restrictions";
            //lblHeading1.Text = "Buy the TDSMAN license";
            ////lblHeading2.Text = "Use coupon code 'TDSMAN' to get 10% instant discount on your purchase. Offer valid till February 15, 2016";
            //if (Convert.ToString(Registration.Get_Campaign().CampaignText) == "")
            //    lblHeading2.Text = "";
            //else 
            //    lblHeading2.Text = Convert.ToString(Registration.Get_Campaign().CampaignText);
            ////--
            //if (Convert.ToString(Registration.Get_Campaign().CampaignName) == "")
            //    lblLink.Text = "www.tdsman.com/pricing.asp";
            //else
            //{
            //    if (GetSerialNo() == "")
            //        lblLink.Text = "www.tdsman.com/Softwares.aspx?campaign=" + Convert.ToString(Registration.Get_Campaign().CampaignName);
            //    else
            //        lblLink.Text = "www.tdsman.com/Softwares.aspx?campaign=" + Convert.ToString(Registration.Get_Campaign().CampaignName) + "&Serial_No=" + GetSerialNo();
            //}
            ////--
            //Cursor.Current = Cursors.Default;
            ////--
            //lnkClickHere.Select();
        }
        #endregion

        #region ParBuyNow_Load
        private void ParBuyNow_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            lblTop.Text = "You are using a trial copy\nUpgrade to Standard Edition and use the software without restrictions";
            lblHeading1.Text = "Buy the TDSMAN license";
            //lblHeading2.Text = "Use coupon code 'TDSMAN' to get 10% instant discount on your purchase. Offer valid till February 15, 2016";
            if (Convert.ToString(Registration.Get_Campaign().CampaignText) == "")
                lblHeading2.Text = "";
            else
                lblHeading2.Text = Convert.ToString(Registration.Get_Campaign().CampaignText);
            //--
            if (Convert.ToString(Registration.Get_Campaign().CampaignName) == "")
                lblLink.Text = "https://www.tdsman.com/pricing.asp";
            else
            {
                if (GetSerialNo() == "")
                    lblLink.Text = "www.tdsman.com/Softwares.aspx?campaign=" + Convert.ToString(Registration.Get_Campaign().CampaignName);
                else
                    lblLink.Text = "www.tdsman.com/Softwares.aspx?campaign=" + Convert.ToString(Registration.Get_Campaign().CampaignName) + "&Serial_No=" + GetSerialNo();
            }
            //--
            Cursor.Current = Cursors.Default;
            //--
            lnkClickHere.Select();
        }
        #endregion

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region lnkClickHere_LinkClicked
        private void lnkClickHere_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(lblLink.Text);
                //
                this.Close();
                this.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show("There seems to have some problem in your default browser.\nYou can open the link : 'www.tdsman.com/pricing.asp' manually.");
                //
                this.Close();
                this.Dispose();
            }
        }
        #endregion

        #region lblHeading1_Click
        private void lblHeading1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(lblLink.Text);
                //
                this.Close();
                this.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show("There seems to have some problem in your default browser.\nYou can open the link : 'www.tdsman.com/pricing.asp' manually.");
                //
                this.Close();
                this.Dispose();
            }
        }
        #endregion

        #region btnClose_MouseMove
        private void btnClose_MouseMove(object sender, MouseEventArgs e)
        {
            tltip.SetToolTip(btnClose, "Close");
        }
        #endregion

        #region btnEnter_Click
        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(lblLink.Text);
                //
                this.Close();
                this.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show("There seems to have some problem in your default browser.\nYou can open the link : 'www.tdsman.com/pricing.asp' manually.");
                //
                this.Close();
                this.Dispose();
            }
        }
        #endregion

        #region GET SERIAL NO
        private string GetSerialNo()
        {
            try
            {
                string strTrialVersionRegistryPath = TdsMan.GetRegistryFolder() + "(Trial)";
                //    
                return cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + strTrialVersionRegistryPath,
                                                                   T_RegistrationInfo.Activ_Code.ToString());
            }
            catch 
            {
                return "";
            }
        }
        #endregion

        private void pnlBorder_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}