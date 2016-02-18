using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Prorating Schema of the DTO
    /// </summary>
    [DataContract]
    public class ProratingSchemaDTO
    {
        /// <summary>
        /// the number of prorateunit the proration is based upon
        /// </summary>
        [DataMember]
        public decimal Quantity;

        /// <summary>
        /// the unit of time the proration is based upon
        /// </summary>
        [DataMember]
        public TimeUnits Unit;
    }
}
