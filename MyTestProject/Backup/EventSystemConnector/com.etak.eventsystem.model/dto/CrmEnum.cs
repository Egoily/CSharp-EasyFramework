using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum NotificationType
    {
        [EnumMember()]
        SMS = 1,
        [EnumMember()]
        Email = 2,
        [EnumMember()]
        USSD = 3,
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum CustomerPaymentType
    {
        [EnumMember()]
        Unkown = -1,
        [EnumMember()]
        Postpayment = 1,
        [EnumMember()]
        Prepayment = 2
    }

    //added by damon 2014-07-04 for select language
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum USSDPushType
    {
        [EnumMember()]
        PushOnce=0,
        [EnumMember()]
        PushToTalk=1
    }
}
