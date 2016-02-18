using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// BillCycleDTO object
    /// </summary>
    [DataContract]
    public class BillCycleDTO
    {
        /// <summary>
        /// Unique Id of the bill cycle
        /// </summary>
        [DataMember]
        public virtual String Id { get; set; }

        /// <summary>
        /// Unique Id of the bill cycle
        /// </summary>
        [DataMember]
        public virtual Int32 MVNOId { get; set; }

        /// <summary>
        /// BillCycle Type
        /// </summary>
        [DataMember]
        public virtual BillCycleTypeDTO Type { get; set; }
        
        /// <summary>
        /// Start date of the bill cycle
        /// </summary>
        [DataMember]
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// End date of the bill cycle
        /// </summary>
        [DataMember]
        public virtual DateTime EndDate { get; set; }
    }
}