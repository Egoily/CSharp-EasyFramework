using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.eventsystem.model.dto
{
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class CustomerChargeDTO : LoadeableEntity
    {
        /// <summary>
        /// Amount of the charge
        /// </summary>
        [DataMember]
        public virtual decimal Amount { get; set; }
       
        /// <summary>
        ///  The charge definition that has been applied
        /// </summary>
        [DataMember]
        public virtual Int32 ChargeDefinitionId { get; set; }
       
        /// <summary>
        /// the account that has been used to apply the charge to the customer
        /// </summary>
        [DataMember]
        public virtual Int64 ChargingAccountId { get; set; }
              
        /// <summary>
        /// The date in which charge is applied
        /// </summary>
        [DataMember]
        public virtual DateTime ChargingDate { get; set; }
        
        /// <summary>
        /// a free format field for potential comments.
        /// </summary>
        [DataMember]
        public virtual string Comments { get; set; }
       
        /// <summary>
        /// The currency that is used in field amount
        /// </summary>
        [DataMember]
        public virtual com.etak.core.model.ISO4217CurrencyCodes Currency { get; set; }
     
        /// <summary>
        /// Customer that who owns the product being charged
        /// </summary>
        [DataMember]
        public virtual Int32 CustomerId { get; set; }

        
        /// <summary>The number of the period the charge was applied for, for recurring charges.
        /// The period number is the period number within the cycle.
        /// </summary>
        [DataMember]
        public virtual long? CycleNumber { get; set; }
     

        /// <summary>
        ///  An external reference for the charge, if any
        /// </summary>
        [DataMember]
        public virtual string ExternalReferenceCode { get; set; }

        /// <summary>
        /// Unique Id of the customer charge
        /// </summary>
        [DataMember]
        public virtual long Id { get; set; }
       
        /// <summary>
        /// The amount/value of currency for this charge that needs to be displayed
        /// </summary>
        [DataMember]
        public virtual decimal? InformationalAmount { get; set; }
        
      
        /// <summary>
        /// The invoice that to which charge belongs
        /// </summary>
        [DataMember]
        public virtual Int64 InvoiceId { get; set; }

        /// <summary>
        /// The number of the period the charge was applied for, for recurring charges.
        ///     The period number is the period number within the cycle.
        /// </summary>
        [DataMember]
        public virtual long? PeriodNumber { get; set; }

        /// <summary>
        /// The purchase of a product that generated this charge
        /// </summary>
        [DataMember]
        public virtual Int64 CustomerProductAssignmentId { get; set; }
        
        /// <summary>
        /// The shedule that generated this charge in case of recurring charges.
        /// </summary>
        [DataMember]
        public virtual Int32 ? ScheduleId { get; set; }        
         
        /// <summary>
        /// The definition of the tax types that apply to this charge
        /// </summary>
        [DataMember]
        public virtual Int32 TaxId { get; set; }

        /// <summary>
        /// Amount of tax of this charge.
        /// </summary>
        [DataMember]
        public virtual decimal? TaxAmount { get; set; }
    }
}
