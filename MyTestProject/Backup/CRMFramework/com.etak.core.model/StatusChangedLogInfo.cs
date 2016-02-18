using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class StatusChangedLogInfo
    {

        #region 成员
        private long? logId;
        private int? customerId;
        private int? dealerId;
        private string dealerName;
        private string customerName;
        private string resourceNumber;
        private int? resourceTypeId;
        private string resourceTypeName;
        private int? currentStatusId;
        private int? nextStatusId;
        private DateTime? createDate;
        private int? eventId;
        private string eventName;
        private int? userId;
        private string userName;

        public virtual long? LogId
        {
            get { return logId; }
            set { logId = value; }
        }
        public virtual int? CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public virtual int? DealerId
        {
            get { return dealerId; }
            set { dealerId = value; }
        }
        public virtual string DealerName
        {
            get { return dealerName; }
            set { dealerName = value; }
        }
        public virtual string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }
        public virtual string ResourceNumber
        {
            get { return resourceNumber; }
            set { resourceNumber = value; }
        }
        public virtual int? ResourceTypeId
        {
            get { return resourceTypeId; }
            set { resourceTypeId = value; }
        }
        public virtual string ResourceTypeName
        {
            get { return resourceTypeName; }
            set { resourceTypeName = value; }
        }
        public virtual int? CurrentStatusId
        {
            get { return currentStatusId; }
            set { currentStatusId = value; }
        }
        public virtual string CurrentStatusName
        {
            get
            {
                string statusName = string.Empty;
                if (currentStatusId.HasValue)
                {
                    if (resourceTypeId == (int)RESOURCEType.MSISDN)
                        statusName = ((MSISDNStatus)currentStatusId).ToString();
                    if (resourceTypeId == (int)RESOURCEType.ICCID)
                        statusName = ((SIMCardStatus)currentStatusId).ToString();
                    if (resourceTypeId == (int)RESOURCEType.SIMCard)
                        statusName = ((ResourceStatus)currentStatusId).ToString();
                    if (resourceTypeId == (int)RESOURCEType.VoucherCard)
                        statusName = ((VoucherStatus)currentStatusId).ToString();
                }
                return statusName;
            }
        }
        public virtual int? NextStatusId
        {
            get { return nextStatusId; }
            set { nextStatusId = value; }
        }
        public virtual string NextStatusName
        {
            get
            {
                string statusName = string.Empty;
                if (null != resourceTypeId && resourceTypeId.HasValue &&
                    null != nextStatusId && nextStatusId.HasValue)//modified by Liny,2012-03-28,because an error will be occur when the nextStatusId is null in DB.
                {
                    if (resourceTypeId == (int)RESOURCEType.MSISDN)
                        statusName = ((MSISDNStatus)nextStatusId).ToString();
                    if (resourceTypeId == (int)RESOURCEType.ICCID)
                        statusName = ((SIMCardStatus)nextStatusId).ToString();
                    if (resourceTypeId == (int)RESOURCEType.SIMCard)
                        statusName = ((ResourceStatus)nextStatusId).ToString();
                    if (resourceTypeId == (int)RESOURCEType.VoucherCard)
                        statusName = ((VoucherStatus)nextStatusId).ToString();
                }
                return statusName;
            }
        }
        public virtual DateTime? CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        public virtual int? EventId
        {
            get { return eventId; }
            set { eventId = value; }
        }
        public virtual string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }
        public virtual int? UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public virtual string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        #endregion
                
        public virtual StatusChangedLogInfo Clone()
        {
            return this.MemberwiseClone() as StatusChangedLogInfo;
        }
    }
}
