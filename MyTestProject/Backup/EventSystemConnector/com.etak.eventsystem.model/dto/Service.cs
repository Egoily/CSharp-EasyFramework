using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class Service : LoadeableEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public virtual int? ServiceID {get;set;}

        [DataMember(EmitDefaultValue = false)]
        public virtual int? CustomerID { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual int? BundleID { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? CreditLimit { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? UnBilledBalance { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual decimal? BilledBalance { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual int? InvoiceTemplateID { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? StartDate { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? EndDate { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual DateTime? CreateDate { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual int? UserID { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual int? ProductID { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual double? DepositAmount { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual bool DeleteFlag { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual byte[] UpdateStamp { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual int CREDITLIMITBASEBUNDLEID { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public virtual int? Status { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public bool PointToBaseBundle { get; set; }       
    }
}