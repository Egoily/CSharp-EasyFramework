using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ImportLogInfo
    {
        private int? _ID = null;        
        private string _TransactionID;        
        private string _ICCID;        
        private string _Resource;
        private string _IMSI1;        
        private int? _DealerID = null;        
        private DateTime? _StartTime = null;        
        private DateTime? _EndTime = null;        
        private int? _ResultCode = null;        
        private string _Description;        
        private int? _UserID = null;       
        

        #region Attribute
        public virtual int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual string TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }

        public virtual string ICCID
        {
            get { return _ICCID; }
            set { _ICCID = value; }
        }

        public virtual string Resource
        {
            get { return _Resource; }
            set { _Resource = value; }
        }

        public string IMSI1
        {
            get { return _IMSI1; }
            set { _IMSI1 = value; }
        }

        public virtual int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual DateTime? StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        public virtual DateTime? EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        public virtual int? ResultCode
        {
            get { return _ResultCode; }
            set { _ResultCode = value; }
        }

        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public virtual int? UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        #endregion

    }
}
