using System;

namespace com.etak.core.model
{
    [Serializable]
    public class MVNOAutoTopupNotificationProvisionLog
    {
        private long? id;
        private long? notificationID;
        private int? dealerID;
        private string msisdn;
        private string requestXML;
        private string responseXML;

        private byte? notificationFrom;
        private byte? status;
        private DateTime? createDate;

        public virtual long? ID
        {
            get { return id; }
            set { id = value; }
        }

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
        public virtual string RequestXML
        {
            get { return requestXML; }
            set { requestXML = value; }
        }
        public virtual string ResponseXML
        {
            get { return responseXML; }
            set { responseXML = value; }
        }
        public virtual byte? NotificationFrom
        {
            get { return notificationFrom; }
            set { notificationFrom = value; }
        }

        public virtual byte? Status
        {
            get { return status; }
            set { status = value; }
        }
        public virtual DateTime? CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}
