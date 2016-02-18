using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// mapping table crm_sms_queue
    /// </summary>
    [DataContract]
    [Serializable]
    public class SmsLogQueue
    {
        #region 构造函数
        public SmsLogQueue()
        { }

        public SmsLogQueue(int? dealerid, int? flagImmediately, int? flagOther, DateTime nextScanTime, byte[] UpdateStamp)
        {
            this._DealerID = dealerid;
            this._FlagImmediately = flagImmediately;
            this._FlagOther = flagOther;
            this.NextScanTime = nextScanTime;
            this.UpdateStamp = UpdateStamp;          
        }

        #endregion

        #region 成员
        private int? _DealerID = null;
        private int? _FlagImmediately = null;
        private int? _FlagOther = null;
        private DateTime? _NextScanTime;
        private byte[] _UpdateStamp;
        #endregion

        #region 属性
        public virtual int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual int? FlagImmediately
        {
            get { return _FlagImmediately; }
            set { _FlagImmediately = value; }
        }

        public virtual int? FlagOther
        {
            get { return _FlagOther; }
            set { _FlagOther = value; }
        }

        public virtual DateTime? NextScanTime
        {
            get { return _NextScanTime; }
            set { _NextScanTime = value; }
        }


        public virtual byte[] UpdateStamp
        {
            get { return _UpdateStamp; }
            set { _UpdateStamp = value; }
        }
        #endregion

        public virtual SmsLogQueue Clone()
        {
            SmsLogQueue smsQueue = this.MemberwiseClone() as SmsLogQueue;

            return smsQueue;
        }
    }
}
