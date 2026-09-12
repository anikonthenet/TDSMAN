#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: ANIK GHOSH
Module Name		: TrnViewSelectedReportGridItems
Version			: 1.0
Start Date		: 01-03-2013
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
using System.IO;

//~~~~ User Namespaces ~~~~
//using ChequePrinting.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
//using TDSMAN.FormPar;
using TDSMAN.FormSys;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;


#endregion


namespace TDSMAN.FormTrn
{
    public partial class TrnViewSelectedReportGridItems : Form
    {
        #region System Generated Code
        public TrnViewSelectedReportGridItems()
        {
            InitializeComponent();
        }
        #endregion

        #region ENUM decleration of Selected Item Grid Column
        // enum for setting detail grid column
        enum enmSelectedItem
        {
            ITEM_DESC = 0
        }
        #endregion

        #region Objects & Variables decleration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //
        //
        ToolTip ToolTip = new ToolTip();
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        //string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string strGridSQL = "";
        //
        int lngDetailGridColumns = 0;
        //
        #endregion       

        #region User Defined Events

        #region TrnViewPreviousCheques_Load
        private void TrnViewPreviousCheques_Load(object sender, EventArgs e)
        {
            try
            {
                //
                //-----------------------------------------------------------
                //strGridSQL = "SELECT ITEM_NAME FROM TEMP_REPORT_GRID_ITEMS_SELECTED ORDER BY ITEM_NAME";
                strGridSQL = "SELECT ITEM_NAME FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_REPORT_GRID_ITEMS_SELECTED + " ORDER BY ITEM_NAME";
                
                //
                ADODB._Recordset rsDetailGrid = null;
                //--
                rsDetailGrid = dmlService.J_ExecSqlReturnADODBRecordset(strGridSQL);
                if (rsDetailGrid == null) return;
                //
                setDetailsGridRefresh(flxgrdPrntdChq);
                //
                if (rsDetailGrid.RecordCount > 0)
                {
                    //-- Clear the Flexgrid data
                    flxgrdPrntdChq.Clear();
                    flxgrdPrntdChq.DataSource = (msdatasrc.DataSource)rsDetailGrid;
                }
                else
                    flxgrdPrntdChq.FixedRows = 1;
                //--
                setPrntdChqGridColumns(flxgrdPrntdChq);
                rsDetailGrid.Close();
                //--
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region btnCloseViewLoadedRecords_Click
        private void btnCloseViewLoadedRecords_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region setDetailsGridRefresh
        private void setDetailsGridRefresh(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            HFlexGrid.set_Cols(0, lngDetailGridColumns);

            for (int intRows = 1; intRows <= HFlexGrid.Rows - 1; intRows++)
                for (int intCols = 0; intCols < lngDetailGridColumns; intCols++)
                    HFlexGrid.set_TextMatrix(intRows, intCols, "");

            HFlexGrid.Rows = 2;
        }
        #endregion

        #region setPrntdChqGridColumns
        private void setPrntdChqGridColumns(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            //
            HFlexGrid.Row = 0;
            HFlexGrid.Col = 0;
            HFlexGrid.set_ColWidth(0, 0, 250);

            // Cheque No.
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmSelectedItem.ITEM_DESC;
            HFlexGrid.Text = "Selected Item Description";
            HFlexGrid.set_ColWidth((int)enmSelectedItem.ITEM_DESC, 0, 11000);
            HFlexGrid.set_ColAlignment((int)enmSelectedItem.ITEM_DESC, (short)J_Alignment.LeftCentre);
            //HFlexGrid.set_BackColorBand((int)enmChequeBookEntry.CHEQUE_NO, 100);

        }
        #endregion


        
        #endregion

    }
}