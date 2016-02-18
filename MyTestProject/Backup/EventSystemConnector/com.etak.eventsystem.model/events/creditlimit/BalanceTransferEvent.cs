using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.creditlimit
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class BalanceTransferEvent : StrongTypedEvent
    {
        [DataMember]
        public Customer FromCustomer { get; set; }
        [DataMember]
        public Customer ToCustomer { get; set; }
        [DataMember]
        public decimal TransferAmount { get; set; }
        [DataMember]
        public long TransferID { get; set; }
    }
}
