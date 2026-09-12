
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: MstViewCompanyDetails
Version			: 1.0
Start Date		: 27-12-2010
End Date		: 
Last Updated    : 
Tables Used     : 
Module Desc		: 
________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

    //~~~~ System Namespaces ~~~~
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Data;
    using System.Data.SqlClient;

    //~~~~ User Namespaces ~~~~
    using TDSMAN.FormTrn;
    using TDSMAN.FormRpt;
    using TDSMAN.Classes;

    //~~~~ This namespace are using for using VB6 component
    using Microsoft.VisualBasic.Compatibility.VB6;


#endregion

namespace TDSMAN.FormMst
{
    public partial class MstViewResponsiblePersonDetails : Form
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public MstViewResponsiblePersonDetails()
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
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //-----------------------------------------------------------------------
        long lngSearchId;					//For Storing the Id
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------        
        #endregion


        #region User Defined Events
            
        #region RESIZING WINDOW
        private void _Load(object sender, EventArgs e)
        {
            _form_resize._get_initial_size();
        }

        private void _Resize(object sender, EventArgs e)
        {
            _form_resize._resize();
            //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        }
        #endregion

        #region MstViewResponsiblePersonDetails_Load

        private void MstViewResponsiblePersonDetails_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            GC.Collect();
            //
            //btnClose.Text = "Return to Form [" + TDSMAN.Classes.TDSMAN.T_pFormNo + "]";
            if (ShowRecord(TDSMAN.Classes.TDSMAN.T_pCompanyId, TDSMAN.Classes.TDSMAN.T_pBasicInfoId) == false)
            {
                return;
            }
        }

        #endregion


        #region btnClose_Click

        private void btnClose_Click(object sender, EventArgs e)
        {
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
            //--
            cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form " + TDSMAN.Classes.TDSMAN.T_pTabFormCaption);
        }

        #endregion


        #endregion


        #region User Define Functions

        #region ClearControls
        private void ClearControls()
        {
            //--------------------------------------
            txtDedEmpColName.Text = "";
            //---------------------------------------
            txtRPName.Text = "";
            txtRPDesignation.Text = "";
            txtRPFatherName.Text = "";
            txtRPAddress1.Text = "";
            txtRPAddress2.Text = "";
            txtRPAddress3.Text = "";
            txtRPAddress4.Text = "";
            txtRPAddress5.Text = "";
            txtRPState.Text = "";
            //-----------
            txtRPPIN.Text = "";
            txtRPSTD.Text = "";
            txtRPPhone.Text = "";
            txtRPMobileNo.Text = "";
            txtRPEmail.Text = "";
            //--
            txtAltRPSTD.Text = "";
            txtAltRPPhone.Text = "";
            txtAltRPEmail.Text = "";
            //----------------------------------------
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id, long BasicInfoId)
        {
            IDataReader drdShowRecord = null;
            string strStateName = string.Empty;
            string strDistrictName = string.Empty;
            string strCompanyTableName = string.Empty;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            try
            {
                if (BasicInfoId == 0)
                    strCompanyTableName = "MST_COMPANY";
                else
                    strCompanyTableName = "TRN_COMPANY_INFO";

                strSQL = "SELECT  " + strCompanyTableName + ".COMPANY_ID            AS COMPANY_ID," +
                    "             " + strCompanyTableName + ".COMPANY_NAME          AS COMPANY_NAME," +
                    "             " + strCompanyTableName + ".PERSON_NAME           AS PERSON_NAME," +
                    "             " + strCompanyTableName + ".DESIGNATION           AS DESIGNATION," +
                    "             " + strCompanyTableName + ".FATHER_NAME           AS FATHER_NAME," +
                    "             " + strCompanyTableName + ".P_ADDRESS1            AS P_ADDRESS1," +
                    "             " + strCompanyTableName + ".P_ADDRESS2            AS P_ADDRESS2," +
                    "             " + strCompanyTableName + ".P_ADDRESS3            AS P_ADDRESS3," +
                    "             " + strCompanyTableName + ".P_ADDRESS4            AS P_ADDRESS4," +
                    "             " + strCompanyTableName + ".P_ADDRESS5            AS P_ADDRESS5," +
                    "             " + strCompanyTableName + ".P_STATE_ID            AS P_STATE_ID," +
                    "             MST_STATE.STATE_NAME               AS RP_STATE_NAME," +
                    "             " + strCompanyTableName + ".P_PIN_CODE            AS P_PIN_CODE," +
                    "             " + strCompanyTableName + ".P_PHONE               AS P_PHONE," +
                    "             " + strCompanyTableName + ".P_STD                 AS P_STD," +
                    "             " + strCompanyTableName + ".P_EMAIL               AS P_EMAIL," +
                    "             " + strCompanyTableName + ".P_MOBILE              AS P_MOBILE," +
                    "             " + strCompanyTableName + ".P_ALT_STD             AS P_ALT_STD," +
                    "             " + strCompanyTableName + ".P_ALT_PHONE           AS P_ALT_PHONE," +
                    "             " + strCompanyTableName + ".P_ALT_EMAIL           AS P_ALT_EMAIL " +
                    "     FROM    (" + strCompanyTableName + " LEFT JOIN  MST_STATE " +
                    "             ON " + strCompanyTableName + ".P_STATE_ID        = MST_STATE.STATE_ID) " +
                    "     WHERE   " + strCompanyTableName + ".COMPANY_ID      = " + Id + " ";

                if (BasicInfoId > 0)
                    strSQL = strSQL + " AND " + strCompanyTableName + ".BASIC_INFO_ID = " + BasicInfoId + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;

                    txtDedEmpColName.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["COMPANY_NAME"]));
                    txtRPName.Text = Convert.ToString(drdShowRecord["PERSON_NAME"]);
                    txtRPDesignation.Text = Convert.ToString(drdShowRecord["DESIGNATION"]);
                    txtRPFatherName.Text = Convert.ToString(drdShowRecord["FATHER_NAME"]);
                    txtRPAddress1.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["P_ADDRESS1"]));
                    txtRPAddress2.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["P_ADDRESS2"]));
                    txtRPAddress3.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["P_ADDRESS3"]));
                    txtRPAddress4.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["P_ADDRESS4"]));
                    txtRPAddress5.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["P_ADDRESS5"]));
                    txtRPState.Text = Convert.ToString(drdShowRecord["RP_STATE_NAME"]);
                    txtRPPIN.Text = Convert.ToString(drdShowRecord["P_PIN_CODE"]);
                    txtRPSTD.Text = Convert.ToString(drdShowRecord["P_STD"]);
                    txtRPPhone.Text = Convert.ToString(drdShowRecord["P_PHONE"]);
                    txtRPMobileNo.Text = Convert.ToString(drdShowRecord["P_MOBILE"]);
                    txtRPEmail.Text = Convert.ToString(drdShowRecord["P_EMAIL"]);
                    //
                    txtAltRPSTD.Text = Convert.ToString(drdShowRecord["P_ALT_STD"]);
                    txtAltRPPhone.Text = Convert.ToString(drdShowRecord["P_ALT_PHONE"]);
                    txtAltRPEmail.Text = Convert.ToString(drdShowRecord["P_ALT_EMAIL"]);

                    drdShowRecord.Close();
                    drdShowRecord.Dispose();

                    //cmbState.Text = strStateName;
                    //cmbDistrict.Text = strDistrictName;

                    btnClose.Select();
                    return true;
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                lngSearchId = 0;
                //-----------------------------------------------------------
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        

        #endregion
    }
}