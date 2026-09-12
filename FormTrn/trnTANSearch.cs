
#region Refered Namespaces & Classes
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//~~~~ User Namespaces ~~~~
using TDSMAN.FormMst;
using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormSys;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;
#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnTANSearch : Form
    {
        #region System Generated Code
        public TrnTANSearch()
        {
            InitializeComponent();
        }

        public TrnTANSearch(string FormName)
        {
            InitializeComponent();
            this.strFormName = FormName;
        }
        #endregion

        #region Objects & Variables decleration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        //-----------------------------------------------------------------------
        string strFormName = string.Empty;
        string strSQLCompanyNameTAN = string.Empty;
        #endregion

        #region trnPANSearch_Load
        private void trnPANSearch_Load(object sender, EventArgs e)
        {
            lblTAN.Text = string.Empty;
            //
            if (strFormName == "TrnAutoFillingChallan")
                strSQLCompanyNameTAN = "COMPANY_NAME + ' - ' + TAN_NO";
            else
                strSQLCompanyNameTAN = "COMPANY_NAME + ' [' + TAN_NO  + ']'";
            //
            //"             COMPANY_NAME + ' [' + TAN_NO + ']'" +
        }
        #endregion

        #region txtTANNo_KeyPress
        private void txtTANNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                txtTANNo.Select();           
            else
                if (TdsMan.gTANNoPANNoValidation(txtTANNo, e, TDSMAN.Classes.T_TANPAN.TAN) == false)
                    e.Handled = true;
            //-----------------------------------
//            if (txtTANNo.Text.Trim().Length == 10)
//            {
//                strSQL = @"SELECT COMPANY_NAME + ' - ' + TAN_NO AS COMPANY
//                           FROM   MST_COMPANY
//                           WHERE  TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "'";

//                lblTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
//                //--------------------------------
//                if (string.IsNullOrEmpty(lblTAN.Text.Trim()))
//                {
//                    cmnService.J_UserMessage("TAN No does not exists!");
//                    txtTANNo.Select();
//                    return;
//                }
//            }
//            else
//                return;
                
        }
        #endregion

        #region txtTANNo_TextChanged
        private void txtTANNo_TextChanged(object sender, EventArgs e)
        {
            lblTAN.Text = string.Empty;
        }
        #endregion

        #region txtTANNo_Leave
        private void txtTANNo_Leave(object sender, EventArgs e)
        {
            //if (CheckTANNoFormat() == false)
            //{
            //    cmnService.J_UserMessage("Please enter valid TAN.");
            //    //txtTANNo.Select();
            //    return;
            //}
            //------------------------------------------------------
            //------------------------------------------------------
            if (txtTANNo.Text.Trim().Length == 10)
            {
                //strSQL = @"SELECT COMPANY_NAME + ' [' + TAN_NO  + ']' AS COMPANY
                strSQL = @"SELECT " + strSQLCompanyNameTAN + @" AS COMPANY
                           FROM   MST_COMPANY
                           WHERE  TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "'";
                lblTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //--
                strSQL = @"SELECT COMPANY_ID AS COMPANY_ID
                           FROM   MST_COMPANY
                           WHERE  TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "'";
                lblCompanyID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //--------------------------------
                if (strFormName == "TrnViewChallanInformationOnline")
                {
                    if (string.IsNullOrEmpty(lblTAN.Text.Trim()))
                    {
                        //strSQL = @"SELECT COR_TRN_COMPANY.COMPANY_NAME + ' [' + COR_TRN_COMPANY.TAN_NO + ']'  AS COMPANY
                        strSQL = @"SELECT " + strSQLCompanyNameTAN + @"  AS COMPANY
                                   FROM   COR_TRN_COMPANY
                                   WHERE  TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "'";

                        lblTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    }
                }
                //--------------------------------
                //if (string.IsNullOrEmpty(lblTAN.Text.Trim()))
                //{
                //    cmnService.J_UserMessage("TAN No does not exists!");
                //    //txtTANNo.Select();
                //    return;
                //}
            }
            else
                return;
        }
        #endregion

        #region btnOk_Click
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblTAN.Text.Trim()))
            {
                //if (strFormName == "TrnRegularReturn") //-- 2024/0320
                //{
                //    TDSMAN.Classes.TDSMAN.T_pCompanyName = cmnService.J_Left(lblTAN.Text, (lblTAN.Text.Length - 12)).Trim();
                //    //
                //    TDSMAN.Classes.TDSMAN.T_pCompanyId = Convert.ToInt32(lblCompanyID.Text);
                //}
                ////else if (strFormName == "RptDialog") //-- 2024/06/21
                ////{
                ////    TDSMAN.Classes.TDSMAN.T_pCompanyName = cmnService.J_Left(lblTAN.Text, (lblTAN.Text.Length - 12)).Trim();
                ////}
                //else
                    TDSMAN.Classes.TDSMAN.T_pTAN = lblTAN.Text;
            }
            else
            {
                if (txtTANNo.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Please enter valid TAN.");
                    txtTANNo.Select();
                    return;
                }
                //--
                if (CheckTANNoFormat() == false)
                {
                    //cmnService.J_UserMessage("Please enter valid TAN.");
                    //txtTANNo.Select();
                    return;
                }
                else
                {
                    strSQL = @"SELECT COMPANY_NAME + ' [' + TAN_NO  + ']' AS COMPANY
                               FROM   MST_COMPANY
                               WHERE  TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "'";

                    lblTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //--------------------------------
                    if (strFormName == "TrnViewChallanInformationOnline" ||
                        strFormName == "TrnAutoFillingChallan" ||
                        strFormName == "TrnExportImportComplianceCheck")
                    {
                        if (string.IsNullOrEmpty(lblTAN.Text.Trim()))
                        {
                            strSQL = @"SELECT COR_TRN_COMPANY.COMPANY_NAME + ' [' + COR_TRN_COMPANY.TAN_NO + ']'  AS COMPANY
                                       FROM   COR_TRN_COMPANY
                                       WHERE  TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "'";

                            lblTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                        }
                    }
                    //--------------------------------                    
                    if (string.IsNullOrEmpty(lblTAN.Text.Trim()))
                    {
                        cmnService.J_UserMessage("TAN does not exists in the Software");
                        //txtTANNo.Select();
                        return;
                    }
                    else
                        TDSMAN.Classes.TDSMAN.T_pTAN = lblTAN.Text;
                }
                //---------------
                //else
                //{
                //    cmnService.J_UserMessage("Please enter valid TAN.");
                //    txtTANNo.Select();
                //    return;
                //}
            }
            //------------------------------------------------
            //------------------------------------------------
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region btnCancel_Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_pTAN = string.Empty;
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        //-- Added By Abhishek Dey On 18/04/2018 --
        #region CheckTANNoFormat
        private bool CheckTANNoFormat()
        {
            //-----------------------------------------------------------------------
            //-- TAN FORMAT
            //-----------------------------------------------------------------------
            if (txtTANNo.Text.Length != 10)
            {
                cmnService.J_UserMessage("TAN No. should be of 10 characters");
                //txtTANNo.Select();
                return false;
            }
            //---------------------------
            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtTANNo.Text, 4), J_DataType.Character) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                //txtTANNo.Select();
                return false;
            }
            //---------------------------
            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtTANNo.Text, 4, 5), J_DataType.Numeric) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                //txtTANNo.Select();
                return false;
            }
            //---------------------------
            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtTANNo.Text, 1), J_DataType.Character) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                //txtTANNo.Select();
                return false;
            }
            return true;
            //-----------------------------------------------------------------------
        }
        #endregion
        //-----------------------------------------
    }
}
