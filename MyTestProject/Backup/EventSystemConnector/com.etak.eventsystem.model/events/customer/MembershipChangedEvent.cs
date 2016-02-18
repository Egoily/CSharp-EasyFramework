using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.customer
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class IvoryToSilverEvent : NotificationEvent { }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SilverToGoldEvent : NotificationEvent { }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class GoldToPlatinumEvent : NotificationEvent { }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class SilverToIvoryEvent : NotificationEvent { }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class GoldToSilverEvent : NotificationEvent { }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class PlatinumToGoldEvent : NotificationEvent { }
}