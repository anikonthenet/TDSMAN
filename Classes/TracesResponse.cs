using System;
using System.Collections.Generic;
using System.Text;

namespace TDSMAN.Classes
{
    #region Enum
    public enum enmResponse
    {
        Success,
        Failed,
        SessionTimeout,
        ReturnRejected,
        Requested

    }

    #endregion
    
    #region TRACESResponse
    public class TracesResponse
    {
        private enmResponse Response;
        private string strMessage;
        private TracesData objTraceData;
        //private object CustomTypes;
        public object CustomTypes;


        public enmResponse Respons
        {
            get { return this.Response; }
            set { Response = value; }
        }

        public string Message
        {
            get { return strMessage; }
            set { strMessage = value; }
        }

        public TracesData UserData
        {
            get { return objTraceData; }
            set { objTraceData = value; }
        }

        public object CustomeTypes
        {
            get { return CustomTypes; }
            set { CustomTypes = value; }
        }

        public object Data { get; set; }
    }

    #endregion
    
    #region ErrorDB
    public struct ErrorDB<T1, T2, T3>
    {
        private T1 tKey1;
        private T2 tKey2;
        private T3 tKey3;

        public T1 KEY1
        {
            get { return tKey1; }
            set { tKey1 = value; }
        }

        public T2 KEY2
        {
            get { return tKey2; }
            set { tKey2 = value; }
        }

        public T3 KEY3
        {
            get { return tKey3; }
            set { tKey3 = value; }
        }

        public ErrorDB(T1 t1, T2 t2, T3 t3)
        {
            tKey1 = t1;
            tKey2 = t2;
            tKey3 = t3;

        }
    }

    #endregion


    


}
