using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CustomerDataRoamingLimitNotification
    {
        private long? _id;
        private int? _mvnoNotificationId;
        private int? _statusId;
        private int? _isSent;
        private int? _notificationType;
         
        /// <summary>
        /// Unique ID
        /// </summary>
        public virtual long? ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// the Id of Data Roaming MVNO Notification Setting
        /// </summary>
        public virtual MVNODataRoamingLimitNotification MVNODataRoamingLimitNotification { get; set; }

        /// <summary>
        /// Customer 
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// StatusId  
        /// 0, disable; 1, enable
        /// </summary>
        public virtual int? StatusID
        {
            get { return _statusId; }
            set { _statusId = value; }
        }

        /// <summary>
        /// Notification Sent Flag, Only effect when notificationType is Threshold
        /// </summary>

        public virtual int? ISSent
        {
            get { return _isSent; }
            set { _isSent = value; }
        }

        /// <summary>
        /// Notification Type
        /// 1 �C apply/modified
        /// 2 �C Threshold
        /// 3 �C Daily Accumulated
        /// 4 �C Continue
        /// 5 �C Interrupt
        /// 6 - ThresholdControl
        /// </summary>
        public virtual int? NotificationType
        {
            get { return _notificationType; }
            set { _notificationType = value; }
        }

         

        public virtual CustomerDataRoamingLimitNotification Clone()
        {
            CustomerDataRoamingLimitNotification customerDataRoamingLimitNotification = this.MemberwiseClone() as CustomerDataRoamingLimitNotification;


            return customerDataRoamingLimitNotification;
        }

    }
}
