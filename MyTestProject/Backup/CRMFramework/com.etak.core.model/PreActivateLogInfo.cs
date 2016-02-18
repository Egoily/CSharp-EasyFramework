using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class PreActivateLogInfo
    {
        private string _TransactionID;        
        private string _ICCID;        
        private string _Resource;        
        private int? _DealerID = null;        
        private DateTime? _StartTime = null;        
        private DateTime? _EndTime = null;        
        private long? _MessageID = null;        
        private string _Description;        
        private int? _UserID = null;        


        #region Attribute
        public string TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }

        public string ICCID
        {
            get { return _ICCID; }
            set { _ICCID = value; }
        }

        public string Resource
        {
            get { return _Resource; }
            set { _Resource = value; }
        }

        public int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public DateTime? StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        public DateTime? EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        public long? MessageID
        {
            get { return _MessageID; }
            set { _MessageID = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public int? UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        #endregion
       
    }
}
