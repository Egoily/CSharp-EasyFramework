using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// DTO object to determine the type of BillCycle
    /// </summary>
    [DataContract]
    public class BillCycleTypeDTO
    {
        /// <summary>
        /// The Id
        /// </summary>
        [DataMember]
        public String Id { get; set; }
        /// <summary>
        /// The name of the BillCycle
        /// </summary>
        [DataMember]
        public String Name { get; set; }

        /// <summary>
        /// The Description of the Billcycle
        /// </summary>
        [DataMember]
        public String Description { get; set; }
    }
}