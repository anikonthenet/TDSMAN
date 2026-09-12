
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
    using TDSMAN.FormSys;
    using TDSMAN.FormTrn;
    using TDSMAN.FormRpt;
    using TDSMAN.Classes;

    //~~~~ This namespace are using for using VB6 component
    using Microsoft.VisualBasic.Compatibility.VB6;


#endregion

namespace TDSMAN.FormMst
{
    public partial class MstViewCompanyDetails : Form
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public MstViewCompanyDetails()
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

        #region MstViewCompanyDetails_Load

        private void MstViewCompanyDetails_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            //btnClose.Text = "Return to Form [" + TDSMAN.Classes.TDSMAN.T_pFormNo + "]";
            //
            GC.Collect();
            //
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
            //
            //
            //--
            cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form " + TDSMAN.Classes.TDSMAN.T_pTabFormCaption);
            //
        }

        #endregion

        #endregion


        #region User Define Functions

        #region ClearControls
        private void ClearControls()
        {
            //--------------------------------------
            txtDedEmpColName.Text = "";
            txtTANNo.Text = "";
            txtPANNo.Text = "";
            txtBranch.Text = "";
            //-----------
            txtDeductorType.Text = "";
            //--------------------------------------
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtAddress4.Text = "";
            txtAddress5.Text = "";
            //-----------
            txtState.Text = "";
            //-----------
            txtPIN.Text = "";
            txtSTD.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            //---------------------------------------
            txtPAOCode.Text = "";
            txtPAORegNo.Text = "";
            txtDDOCode.Text = "";
            txtDDORegNo.Text = "";
            //-----------
            txtGovtDedState.Text = "";
            txtMinistryType.Text = "";
            //-----------
            txtOtherMinistry.Text = "";
            //-----------
            txtCITAddress.Text = "";
            txtCITCity.Text = "";
            txtCITPin.Text = "";
            //--
            txtAIN.Text = "";
            txtTANRegNo.Text = "";
            txtAltSTD.Text = "";
            txtAltPhone.Text = "";
            txtAltEmail.Text = "";
            txtGSTN.Text = "";
            lblAnnexIII.Visible = false;
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
                    "             " + strCompanyTableName + ".TAN_NO                AS TAN_NO," +
                    "             " + strCompanyTableName + ".PAN_NO                AS PAN_NO," +
                    "             " + strCompanyTableName + ".BRANCH_DIV            AS BRANCH_DIV," +
                    "             " + strCompanyTableName + ".D_CATEGORY_ID         AS D_CATEGORY_ID," +
                    "             MST_CATEGORY.CATEGORY_DESCRIPTION AS CATEGORY_DESCRIPTION," +
                    "             MST_CATEGORY.CATEGORY_CODE        AS CATEGORY_CODE," +
                    "             " + strCompanyTableName + ".FILE_PREFIX           AS FILE_PREFIX," +
                    "             " + strCompanyTableName + ".ADDRESS1              AS ADDRESS1," +
                    "             " + strCompanyTableName + ".ADDRESS2              AS ADDRESS2," +
                    "             " + strCompanyTableName + ".ADDRESS3              AS ADDRESS3," +
                    "             " + strCompanyTableName + ".ADDRESS4              AS ADDRESS4," +
                    "             " + strCompanyTableName + ".ADDRESS5              AS ADDRESS5," +
                    "             " + strCompanyTableName + ".STATE_ID              AS STATE_ID," +
                    "             MST_STATE.STATE_NAME              AS STATE_NAME," +
                    "             " + strCompanyTableName + ".PIN_CODE              AS PIN_CODE," +
                    "             " + strCompanyTableName + ".STD                   AS STD," +
                    "             " + strCompanyTableName + ".PHONE                 AS PHONE," +
                    "             " + strCompanyTableName + ".EMAIL                 AS EMAIL," +
                    "             " + strCompanyTableName + ".PAO_CODE              AS PAO_CODE," +
                    "             " + strCompanyTableName + ".PAO_REG_NO            AS PAO_REG_NO," +
                    "             " + strCompanyTableName + ".DDO_CODE              AS DDO_CODE," +
                    "             " + strCompanyTableName + ".DDO_REG_NO            AS DDO_REG_NO," +
                    "             " + strCompanyTableName + ".D_STATE_ID            AS D_STATE_ID," +
                    "             D_STATE.STATE_NAME                AS D_STATE_NAME," +
                    "             " + strCompanyTableName + ".MINISTRY_ID           AS MINISTRY_ID," +
                    "             MST_MINISTRY.MINISTRY_NAME        AS MINISTRY_NAME," +
                    "             " + strCompanyTableName + ".MINISTRY_OTHER        AS MINISTRY_OTHER," +
                    "             " + strCompanyTableName + ".CIT_TDS_ADDRESS       AS CIT_TDS_ADDRESS," +
                    "             " + strCompanyTableName + ".CIT_TDS_CITY          AS CIT_TDS_CITY," +
                    "             " + strCompanyTableName + ".CIT_TDS_PINCODE       AS CIT_TDS_PINCODE," +
                    "             " + strCompanyTableName + ".AIN_NO                AS AIN_NO," +
                    "             " + strCompanyTableName + ".TAN_REG_NO            AS TAN_REG_NO," +
                    "             " + strCompanyTableName + ".ALT_STD               AS ALT_STD," +
                    "             " + strCompanyTableName + ".ALT_PHONE             AS ALT_PHONE," +
                    "             " + strCompanyTableName + ".ALT_EMAIL             AS ALT_EMAIL," +
                    "             " + strCompanyTableName + ".GSTN                  AS GSTN," +
                    "             " + strCompanyTableName + ".SECTION_194P_FLAG     AS SECTION_194P_FLAG " +
                    "     FROM    ((((" + strCompanyTableName + " INNER JOIN MST_CATEGORY " +
                    "             ON " + strCompanyTableName + ".D_CATEGORY_ID     = MST_CATEGORY.CATEGORY_ID) " +
                    "     INNER JOIN MST_STATE " +
                    "             ON " + strCompanyTableName + ".STATE_ID    = MST_STATE.STATE_ID) " +
                    "     LEFT JOIN  MST_STATE AS D_STATE " +
                    "             ON " + strCompanyTableName + ".D_STATE_ID        = D_STATE.STATE_ID) " +
                    "     LEFT JOIN  MST_MINISTRY " +
                    "             ON " + strCompanyTableName + ".MINISTRY_ID       = MST_MINISTRY.MINISTRY_ID) " +
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
                    txtTANNo.Text = Convert.ToString(drdShowRecord["TAN_NO"]);
                    txtPANNo.Text = Convert.ToString(drdShowRecord["PAN_NO"]);
                    txtDeductorType.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["CATEGORY_CODE"]) + " - " + Convert.ToString(drdShowRecord["CATEGORY_DESCRIPTION"]));
                    txtBranch.Text = Convert.ToString(drdShowRecord["BRANCH_DIV"]);
                    txtAddress1.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["ADDRESS1"]));
                    txtAddress2.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["ADDRESS2"]));
                    txtAddress3.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["ADDRESS3"]));
                    txtAddress4.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["ADDRESS4"]));
                    txtAddress5.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["ADDRESS5"]));
                    txtState.Text = Convert.ToString(drdShowRecord["STATE_NAME"]);
                    txtPIN.Text = Convert.ToString(drdShowRecord["PIN_CODE"]);
                    txtSTD.Text = Convert.ToString(drdShowRecord["STD"]);
                    txtPhone.Text = Convert.ToString(drdShowRecord["PHONE"]);
                    txtEmail.Text = Convert.ToString(drdShowRecord["EMAIL"]);

                    // STATE
                    if (cmnService.J_Left(txtDeductorType.Text, 1) == "S" ||
                        cmnService.J_Left(txtDeductorType.Text, 1) == "E" ||
                        cmnService.J_Left(txtDeductorType.Text, 1) == "H" ||
                        cmnService.J_Left(txtDeductorType.Text, 1) == "N")
                    {
                        txtGovtDedState.Visible = true;
                        label42.Visible = true;
                    }
                    else
                    {
                        txtGovtDedState.Visible = false;
                        label42.Visible = false;
                    }

                    // MINISTRY
                    if (cmnService.J_Left(txtDeductorType.Text, 1) != "S")
                    {
                        label31.Visible = true;
                        txtMinistryType.Visible = true;
                        label39.Visible = true;
                        txtOtherMinistry.Visible = true;
                    }
                    else
                    {
                        label31.Visible = false;
                        txtMinistryType.Visible = false;
                        label39.Visible = false;
                        txtOtherMinistry.Visible = false;
                    }

                    // GOVT DEDUCTORS
                    if (cmnService.J_Left(txtDeductorType.Text, 1) == "A" ||
                        cmnService.J_Left(txtDeductorType.Text, 1) == "S" ||
                        cmnService.J_Left(txtDeductorType.Text, 1) == "D" ||
                        cmnService.J_Left(txtDeductorType.Text, 1) == "E" ||
                        cmnService.J_Left(txtDeductorType.Text, 1) == "G" ||
                        cmnService.J_Left(txtDeductorType.Text, 1) == "H" ||
                        cmnService.J_Left(txtDeductorType.Text, 1) == "L" ||
                        cmnService.J_Left(txtDeductorType.Text, 1) == "N")
                        grpGovtDeductors.Visible = true;
                    else
                        grpGovtDeductors.Visible = false;
                    
                    txtPAOCode.Text = Convert.ToString(drdShowRecord["PAO_CODE"]);
                    txtPAORegNo.Text = Convert.ToString(drdShowRecord["PAO_REG_NO"]);
                    txtDDOCode.Text = Convert.ToString(drdShowRecord["DDO_CODE"]);
                    txtDDORegNo.Text = Convert.ToString(drdShowRecord["DDO_REG_NO"]);
                    txtGovtDedState.Text = Convert.ToString(drdShowRecord["D_STATE_NAME"]);
                    txtMinistryType.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["MINISTRY_NAME"]));
                    txtOtherMinistry.Text = TdsMan.T_ReplaceAmpersand(TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["MINISTRY_OTHER"]))); 
                    
                    txtCITAddress.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["CIT_TDS_ADDRESS"]));
                    txtCITCity.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(drdShowRecord["CIT_TDS_CITY"]));
                    txtCITPin.Text = Convert.ToString(drdShowRecord["CIT_TDS_PINCODE"]);
                    //
                    //
                    txtAIN.Text = Convert.ToString(drdShowRecord["AIN_NO"]);
                    txtTANRegNo.Text = Convert.ToString(drdShowRecord["TAN_REG_NO"]);
                    txtAltSTD.Text = Convert.ToString(drdShowRecord["ALT_STD"]);
                    txtAltPhone.Text = Convert.ToString(drdShowRecord["ALT_PHONE"]);
                    txtAltEmail.Text = Convert.ToString(drdShowRecord["ALT_EMAIL"]);
                    txtGSTN.Text = Convert.ToString(drdShowRecord["GSTN"]);
                    //
                    if (Convert.ToString(drdShowRecord["SECTION_194P_FLAG"]) == "1")
                        lblAnnexIII.Visible = true;
                    else
                        lblAnnexIII.Visible = false;
                    //
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