using com.etak.eventsystem.model.dto;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.events.customer
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class AlElmRegisterFailureEvent : NotificationEvent
    {
        
    }
}
