using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.eventsystem.model.dto
{

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)] 
    public enum ScheduleChargeStatuses
    {
          Default = 0,
    }
    
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]    
    public class CustomerChargeScheduleDTO : LoadeableEntity
    {
        /// <summary>
        /// Unique Id of the CustomerChargeSchedule
        /// </summary>
        [DataMember]
        public virtual Int64 Id { get; set; }

         /// <summary>
         /// Account that is being charged
         /// </summary>
         [DataMember]
        public Int64 ChargedAccountId { get; set; }
       
        /// <summary>
        /// The charge in the catalog that is being charged for this customer
        /// </summary>
        [DataMember]
        public Int32 ChargeDefinitionId { get; set; }
        
        /// <summary>
        ///  When the schedule entity was created.
        /// </summary>
        [DataMember]
        public DateTime CreateDate { get; set; }
                  
        /// <summary>
        /// The number of the cycle that will be charged at the nextchargedate
        /// </summary>
        [DataMember]
        public Int32 CurrentCyclenumber { get; set; }
        
        /// <summary>
        /// The Customer that owns the product that will be charged
        /// </summary>
        [DataMember]
        public Int32 CustomerId { get; set; }
              
        /// <summary>
        /// The next date that charging needs to occur for that charge and that customer.
        /// </summary>
        [DataMember]
        public DateTime? NextChargeDate { get; set; }
       
        /// <summary>
        /// The number of the period that has will be charged at nextchargedate.
        /// </summary>
        [DataMember]
        public int NextPeriodNumber { get; set; }
        
        /// <summary>When the agreement with the customer is to use the price set at a given date
        /// to charge him, the date for which the price needs to be used.
        /// </summary>
        [DataMember]
        public DateTime? PriceEffectiveDate { get; set; }
        
         /// <summary>
        /// The Purchase that generated this charge
        /// </summary>
        [DataMember]
        public Int64 CustomerProductAssignmentId { get; set; }
                 
        /// <summary>
        /// Current status of the charge
        /// </summary>
        [DataMember]
        public ScheduleChargeStatuses Status { get; set; }
       
        /// <summary>
        /// When the shedule was modified for the last time
        /// </summary>
        [DataMember]
        public virtual DateTime? UpdateDate { get; set; }       
    }
}
