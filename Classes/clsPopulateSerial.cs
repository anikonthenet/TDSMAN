using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace TDSMAN.Classes
{
    class clsPopulateSerial
    {
        #region Objects & Variables declaration

        DMLService dmlService = new DMLService();
        Hashtable htChallanSerial = new Hashtable();


        string strSQL = string.Empty;

        #endregion

        #region CONSTRUCTOR

        #region Default Constructor
        public clsPopulateSerial()
        {

        }
        #endregion

        #region Constructor for Correction Excel
        public clsPopulateSerial(long BatchId)
        {
            //For Storing Query
            string strSQL;
            IDataReader reader = null;

            //Populate expected serial for Correction

            //if (returntype == ReturnType.Correction)
            //{

                //expected serial number not taken care of
                strSQL = @"SELECT COR_TRN_CHALLAN.SL_NO                AS   CHALLAN_SERIAL,
                                  MAX_DEDUCTEE_RECORDS.DEDUCTEE_SERIAL AS   DEDUCTEE_SERIAL
                         FROM   COR_TRN_CHALLAN,
                                (SELECT TRN_CHALLAN_ID,
                                        MAX(SL_NO) AS DEDUCTEE_SERIAL
                                 FROM   COR_TRN_DEDUCTEE_DETAILS
                                 GROUP BY TRN_CHALLAN_ID) AS MAX_DEDUCTEE_RECORDS 
                        WHERE  COR_TRN_CHALLAN.TRN_CHALLAN_ID  = MAX_DEDUCTEE_RECORDS.TRN_CHALLAN_ID
                        AND    COR_TRN_CHALLAN.BATCH_HEADER_ID = " + BatchId;


                reader = dmlService.J_ExecSqlReturnReader(strSQL);

                while (reader.Read())
                {
                    htChallanSerial[reader["CHALLAN_SERIAL"].ToString()] = Convert.ToInt32(reader["DEDUCTEE_SERIAL"].ToString());
                }

                reader.Close();
                reader.Dispose();
            //}
            }
        #endregion

        #endregion

        #region NextSerial
        public long NextSerial(string ChallanSerial)
        {
            htChallanSerial[ChallanSerial] = Convert.ToInt64(htChallanSerial[ChallanSerial]) + 1;

            return Convert.ToInt64(htChallanSerial[ChallanSerial]);
        }
        #endregion
    }
}
