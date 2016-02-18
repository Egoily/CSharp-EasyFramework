using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    //add by damon 2013-11-12
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class Package : LoadeableEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public virtual Int64 PackageID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual Dealer Dealer { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string PackageName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int PaymentTypeID { get; set; }
    }
}
