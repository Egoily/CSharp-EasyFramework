using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]    
    public enum AccountTypes
    {
        [EnumMember] Currency,
        [EnumMember] Time,
        [EnumMember] Data,
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]    
    public class AccountDTO : LoadeableEntity
    {
        [DataMember] public long Id { get; set; }
        
        [DataMember] public Decimal Balance { get; set; }
        
        [DataMember] public Int32 BillCycleId { get; set; }
        
        [DataMember] public Int32 CustomerId { get; set; }

        [DataMember] public Int32 LastBillRunId { get; set; }    
    }
}
