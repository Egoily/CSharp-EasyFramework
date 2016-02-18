using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.customer
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class DataOnlyActivedEvent : NotificationEvent
    {
        public string CustomerName
        {
            get
            {
                return string.Format("{0} {1} {2}", ToCustomer.Initials, ToCustomer.MiddleName, ToCustomer.LastName);
            }
        }
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PostpaidActivedEvent : NotificationEvent
    {
        public string CustomerName
        {
            get
            {
                return string.Format("{0} {1} {2}", ToCustomer.Initials, ToCustomer.MiddleName, ToCustomer.LastName);
            }
        }
    }
}