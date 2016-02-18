using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class CustomerProductAssignmentDTO : LoadeableEntity
    {
        [DataMember] public long Id { get; set; }

        [DataMember] public DateTime CreateDate { get; set; }

        [DataMember] public DateTime StartDate { get; set; }

        [DataMember] public DateTime ?  EndDate { get; set; }

        [DataMember] public Int32 ProductChargePurchased { get; set; }

        [DataMember] public Int32 PurchasedProduct { get; set; }

        [DataMember] public Int32 PurchasingCustomer { get; set; }
    }
}
