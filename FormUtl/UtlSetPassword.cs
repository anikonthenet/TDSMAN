
#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TDSMAN.Classes;

#endregion

namespace TDSMAN.FormUtl
{
    public partial class UtlSetPassword : TDSMAN.FormGen.GenForm
    {
        #region UtlSetPassword
        public UtlSetPassword()
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
        //-----------------------------------------------------------------------
        long lngSearchId;					//For Storing the Id
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        DataSet dsetChallanGridClone = new DataSet();
        DataSet dsetChallanDetailsGridClone = new DataSet();
        //-----------------------------------------------------------------------
        string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        //-----------------------------------------------------------------------
        string strSQLGridViewTabPages;
        long lngBasicInfoID;
        long lngChallanID;
        //long lngChallanDetailID;
        long lngDeducteeDetailID;
        long lngDeducteeID;
        string strSQLShowHelpDeductee;
        string strSQLShowHelpPAN;

        string strStateCode;
        string strRPStateCode;
        string strDStateCode;
        string strMinistryCode;

        string strBatFile = "";
        string strFVUFile = "";
        string strCSIDownloadFilePath = "";

        //Added by Shrey Kejriwal on 19/01/2011
        string strConsolidatedStatementPath = "";

        string newOutputFileName = "";

        string[,] strArray;

        bool blnShowHelp = true;
        bool blnShowPANHelp = true;
        bool blnChkChanged = true;
        bool blnSectionDisplay = true;
        //--            
        IDataReader drdShowDeducteePAN = null;
        ToolTip tllTip = new ToolTip();
        //--            
        //----
        //----
        string strFormNo = T_FormNo.F26Q;

        #endregion


        #region UtlSetPassword_Load
        private void UtlSetPassword_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Normal;
            lblTitle.Text = "Set Password";
        }
        #endregion

        #region btnSavePassword_Click
        private void btnSavePassword_Click(object sender, EventArgs e)
        {
            //--
            if (txtNewPassword.Text.Trim() == "")
            {
                cmnService.J_UserMessage("New Password can't be blank");
                txtNewPassword.Select();
                return;
            }
            //
            if (txtReenterPassword.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Re-enter Password can't be blank");
                txtReenterPassword.Select();
                return;
            }
            //
            if (txtNewPassword.Text != txtReenterPassword.Text)
            {
                cmnService.J_UserMessage("New Password mismatch with Re-enter Password");
                txtReenterPassword.Select();
                return;
            }
            //--
            strSQL = "UPDATE MST_SETUP SET PASSWD = '" + cmnService.J_ReplaceQuote(txtNewPassword.Text.Trim()) + "'";
            if (dmlService.J_ExecSql(strSQL) == false) { return; }
            //--
            cmnService.J_UserMessage("Password has been set successfully");
            //--
            GC.Collect();
            this.Close();
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        

    }
}

