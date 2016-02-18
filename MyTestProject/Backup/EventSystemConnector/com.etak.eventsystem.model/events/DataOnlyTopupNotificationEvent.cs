using System;
using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class DataOnlyTopupNotificationEvent : NotificationEvent
    {
        public string CustomerName
        {
            get
            {
                return string.Format("{0} {1} {2}", ToCustomer.Initials, ToCustomer.MiddleName, ToCustomer.LastName);
            }
        }

        [DataMember]
        public decimal CreditBalance { get; set; }

        [DataMember]
        public DateTime CreditBalanceValidityDate { get; set; }

        [DataMember]
        public decimal TotalData { get; set; }
    }
}