using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class SmsLogInfo
    {
        #region 构造函数
        public SmsLogInfo()
        { }

        public SmsLogInfo(string smsMessage, string sender)
        {
            this.SmsMessage = smsMessage;
            this.Sender = sender;
        }

        #endregion

        #region 成员
        private long? logId;
        private int? dealerId;
        private string sender;
        private string msisdn;
        private string smsMessage;
        private int? categoryId;
        private int? priority = 0;
        private int? validity = 0;
        private int? trySentTimes = null;
        private int? maxSentTimes = null;
        private string name1;
        private string name2;
        private string name3;
        private string name4;
        private string name5;
        private string name6;
        private string value1;
        private string value2;
        private string value3;
        private string value4;
        private string value5;
        private string value6;
        private bool isSent = false;
        private bool searchByIsSent = false;
        private bool virtualDeleted = false;
        private bool searchByVirtualDeleted = false;
        private DateTime? createDate;
        private DateTime? sentDate;
        private int? userId;
        //20110417
        private int? isSmsInQueue;
        private int? deliveryStatus;

        #endregion

        #region 属性
        public virtual long? LogId
        {
            get { return logId; }
            set { logId = value; }
        }
        public virtual int? DealerId
        {
            get { return dealerId; }
            set { dealerId = value; }
        }
        public virtual string Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        public virtual string MSISDN
        {
            get { return msisdn; }
            set { msisdn = value; }
        }
        public virtual string SmsMessage
        {
            get { return smsMessage; }
            set { smsMessage = value; }
        }
        public virtual int? CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }
        public virtual int? Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        public virtual int? Validity
        {
            get { return validity; }
            set { validity = value; }
        }
        public virtual int? TrySentTimes
        {
            get { return trySentTimes; }
            set { trySentTimes = value; }
        }
        public virtual int? MaxSentTimes
        {
            get { return maxSentTimes; }
            set { maxSentTimes = value; }
        }
        public virtual string Name1
        {
            get { return name1; }
            set { name1 = value; }
        }
        public virtual string Name2
        {
            get { return name2; }
            set { name2 = value; }
        }
        public virtual string Name3
        {
            get { return name3; }
            set { name3 = value; }
        }
        public virtual string Name4
        {
            get { return name4; }
            set { name4 = value; }
        }
        public virtual string Name5
        {
            get { return name5; }
            set { name5 = value; }
        }
        public virtual string Name6
        {
            get { return name6; }
            set { name6 = value; }
        }
        /// <summary>
        /// Lifecycle sms log will instore current status id
        /// </summary>
        public virtual string Value1
        {
            get { return value1; }
            set { value1 = value; }
        }
        /// <summary>
        /// Lifecycle sms log will instore next status id
        /// </summary>
        public virtual string Value2
        {
            get { return value2; }
            set { value2 = value; }
        }
        /// <summary>
        /// Lifecycle sms log will instore event code
        /// </summary>
        public virtual string Value3
        {
            get { return value3; }
            set { value3 = value; }
        }
        /// <summary>
        /// Lifecycle sms log will instore remind sms type
        /// </summary>
        public virtual string Value4
        {
            get { return value4; }
            set { value4 = value; }
        }
        public virtual string Value5
        {
            get { return value5; }
            set { value5 = value; }
        }
        public virtual string Value6
        {
            get { return value6; }
            set { value6 = value; }
        }
        public virtual bool IsSent
        {
            get { return isSent; }
            set { isSent = value; }
        }
        public virtual bool SearchByIsSent
        {
            get { return searchByIsSent; }
            set { searchByIsSent = value; }
        }
        public virtual bool VirtualDeleted
        {
            get { return virtualDeleted; }
            set { virtualDeleted = value; }
        }
        public virtual bool SearchByVirtualDeleted
        {
            get { return searchByVirtualDeleted; }
            set { searchByVirtualDeleted = value; }
        }
        public virtual DateTime? CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        public virtual DateTime? SentDate
        {
            get { return sentDate; }
            set { sentDate = value; }
        }
        public virtual int? UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public virtual int? IsSmsInQueue
        {
            get { return isSmsInQueue; }
            set { isSmsInQueue = value; }
        }

        public virtual int? DeliveryStatus
        {
            get { return deliveryStatus; }
            set { deliveryStatus = value; }
        }

        public virtual DateTime? ScheduleDeliveryTime
        {
            get;
            set;
        }
        #endregion
    }
}
