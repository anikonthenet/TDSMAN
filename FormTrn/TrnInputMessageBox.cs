#region Refered Namespaces & Classes
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//~~~~ User Namespaces ~~~~
using TDSMAN.FormMst;
using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;
#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnInputMessageBox : Form
    {
        #region System Generated Code
        public TrnInputMessageBox()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //RptDialog rptDialog = new RptDialog();
        #endregion

        #region TrnInputMessageBox_Load
        private void TrnInputMessageBox_Load(object sender, EventArgs e)
        {
            mskPrintDate.Text = string.Format("{0:dd/MM/yyyy}", System.DateTime.Now.Date);
            if (TDSMAN.Classes.TDSMAN.T_pForm27B == 0)
                lblMessage.Text = lblMessage.Text + " 27A ?";
            else
                lblMessage.Text = lblMessage.Text + " 27B ?";
        }
        #endregion

        #region btnCancel_Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            GC.Collect();
            //
            this.Close();
            this.Dispose();
        }
        #endregion

        #region btnOkPrint_Click
        public void btnOkPrint_Click(object sender, EventArgs e)
        {
            if (dtService.J_IsBlankDateCheck(ref mskPrintDate, J_ShowMessage.NO) == false)
            {
                //-- VALID DATE CHECK
                //----------------------------------------------------------
                if (dtService.J_IsDateValid(mskPrintDate) == false)
                {
                    cmnService.J_UserMessage("Incorrect Format of the Print Date");
                    mskPrintDate.Select();
                    return;
                }                    
                //--
                TDSMAN.Classes.TDSMAN.T_pForm27ADate = mskPrintDate.Text;
            }
            else
                TDSMAN.Classes.TDSMAN.T_pForm27ADate = "";
            //--
            RptDialog rptDialog = new RptDialog();
            //
            if(TDSMAN.Classes.TDSMAN.T_pForm27Corr == true)
                rptDialog.PrintCorrForm27A(TDSMAN.Classes.TDSMAN.T_pBasicInfoId,
                                       TDSMAN.Classes.TDSMAN.T_pQuarter,
                                       TDSMAN.Classes.TDSMAN.T_pFinancialYear,
                                       TDSMAN.Classes.TDSMAN.T_pFormNo,
                                       mskPrintDate.Text);
            else
                rptDialog.PrintForm27A(TDSMAN.Classes.TDSMAN.T_pBasicInfoId,
                                       TDSMAN.Classes.TDSMAN.T_pQuarter,  
                                       TDSMAN.Classes.TDSMAN.T_pFinancialYear, 
                                       TDSMAN.Classes.TDSMAN.T_pFormNo,
                                       mskPrintDate.Text);
            //
            GC.Collect();
            //
            this.Close();
            this.Dispose();
        }
        #endregion

        #region chkBlankDate_CheckedChanged
        private void chkBlankDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBlankDate.Checked == true)
                mskPrintDate.Text = "";
        }
        #endregion


    }
}