using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MVNOAutoTopupNotificationLog
    {
        private long? notificationID;
        private int? dealerID;
        private string msisdn;
        private int? customerID;
        private decimal? threshold;
        private decimal? currentLimit;
        private decimal? topupAmount;
        private byte? notificationFrom;
        private DateTime? createDate;

        private byte? status;
        private byte? executeCount;
        private DateTime? operationDate;

        public virtual long? NotificationID
        {
            get { return notificationID; }
            set { notificationID = value; }
        }

        public virtual int? DealerID
        {
            get { return dealerID; }
            set { dealerID = value; }
        }
        public virtual string MSISDN
        {
            get { return msisdn; }
            set { msisdn = value; }
        }
        public virtual int? CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        public virtual decimal? Threshold
        {
            get { return threshold; }
            set { threshold = value; }
        }
        public virtual decimal? CurrentLimit
        {
            get { return currentLimit; }
            set { currentLimit = value; }
        }
        public virtual decimal? TopupAmount
        {
            get { return topupAmount; }
            set { topupAmount = value; }
        }
        public virtual byte? NotificationFrom
        {
            get { return notificationFrom; }
            set { notificationFrom = value; }
        }
        public virtual DateTime? CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        public virtual byte? Status
        {
            get { return status; }
            set { status = value; }
        }
        public virtual byte? ExecuteCount
        {
            get { return executeCount; }
            set { executeCount = value; }
        }
        public virtual DateTime? OperationDate
        {
            get { return operationDate; }
            set { operationDate = value; }
        }

    }
}
