﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using com.etak.eventsystem.model.dto;

namespace com.etak.eventsystem.model.events.dre
{
    
    /// <summary>
    /// Event generated by the DRE after any suscessfull charge complete
    /// </summary>
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class UsageChargeEvent : StrongTypedEvent
    {
        /// <summary>
        /// Charged Customer
        /// </summary>
        [DataMember()]
        public virtual Customer ChargedCustomer { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember()]
        public virtual Dealer CustomerDealer { get; set; }

        /// <summary>
        /// List of usage details
        /// </summary>
        [DataMember()]
        public IList<CallDetailRecord> CDRs { get; set; }       
        
    }
}
