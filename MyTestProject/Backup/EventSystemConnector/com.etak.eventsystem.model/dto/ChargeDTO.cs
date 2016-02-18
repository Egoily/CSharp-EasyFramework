using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.eventsystem.model.dto
{
    /// <summary>
    /// Enum to indicate if the charge is informational only
    /// </summary>
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]    
    public enum InformationalTypes
    {
        /// <summary>
        /// The charge is not informational
        /// </summary>
        [EnumMember]
        N = 0,
        
        /// <summary>
        /// The charge is informational only
        /// </summary>
        [EnumMember]
        Y = 1,
    }

    /// <summary>
    /// Enum to specofy the possible states of a charge
    /// </summary>
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)] 
    public enum ChargeStatuses
    {  
        /// <summary>
        /// Only defined for test purposes and should not visible/usable through normal
        ///     processes/API.
        /// </summary>
        [EnumMember]
        Test = 0,
       
        /// <summary>
        ///  The charge is usable
        /// </summary>
        [EnumMember]
        Active = 1,
        
        /// <summary>
        /// The charge should not be presented as options in front end operations, but
        ///     all the schedules referring to this charge should continue.
        /// </summary>
        [EnumMember]
        EndOfLife = 2,
        
        
        /// <summary>
        /// The charge is completely disabled, can't be used in any charge option, there
        /// can't be any active scheduled charge to be able to set the charge to this state.
        /// </summary>
        [EnumMember]
        Disabled = 3,
    }
    
   
    /// <summary>
    /// Indicates the time of charge is done InAdvance or Arrear
    /// </summary>
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum TimesOfCharge
    {
        [EnumMember]
        InAdvance = 0,

        [EnumMember]
        Arrear = 1,
    }


    /// <summary>
    /// DTO representation of a core Charge
    /// </summary>
    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]    
    public class ChargeDTO : LoadeableEntity
    {  
        /// <summary>
        ///  Facility to organize charges by category
        /// </summary>
        public virtual int? Category { get; set; }
       
        /// <summary>
        /// Date in which the charge was created
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        
        /// <summary>
        /// General Ledger account for the charge
        /// </summary>
        public virtual string GeneralLedgerAccount { get; set; }
        
        /// <summary>
        /// unique Id of the charge
        /// </summary>
        public virtual int Id { get; set; }
        
        /// <summary>
        /// Indicates if this charge is informational only
        /// </summary>
        public virtual InformationalTypes InformationalOnly { get; set; }
       

        /// <summary>
        /// the number of prorateunit the proration is based upon.
        /// </summary>
        public virtual decimal? ProrateQty { get; set; }
       
        /// <summary>
        /// the unit of time the proration is based upon
        /// </summary>
        public virtual com.etak.core.model.TimeUnits ? ProrateUnit { get; set; }
       
        /// <summary>
        /// the status of the charge, used to determine if the charge is operative
        /// </summary>
        public virtual ChargeStatuses Status { get; set; }
        
        /// <summary>
        ///  Wether the charge is in Advance or in Arrear
        /// </summary>
        public virtual TimesOfCharge TypeOfTimeOfCharge { get; set; }
    }
}
