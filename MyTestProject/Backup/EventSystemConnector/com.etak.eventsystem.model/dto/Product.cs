using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class Product : LoadeableEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public virtual Int32 ProductID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? ServiceTypeID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? PackageID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? CreditLimit { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? SpecialCreditLimit { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? StartDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? EndDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? CreateDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual int? UserID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? ExactcreditLimit { get; set; }       
    }
}