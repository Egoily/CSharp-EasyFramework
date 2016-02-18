using com.etak.eventsystem.model.dto;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.events.creditlimit
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class TransferRequestTerminationForANotificationEvent : NotificationEvent
    {
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class TransferRequestTerminationForBNotificationEvent : NotificationEvent
    {
    }
}
