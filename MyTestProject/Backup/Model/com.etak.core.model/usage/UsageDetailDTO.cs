using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.usage
{
    /// <summary>
    /// Definition of a charge in the catalog
    /// </summary>
    [DataContract]
    public class UsageDetailDTO
    {
        //[DataMember]
        //public Int64 Id { get; set; }

        /// <summary>
        /// Voice = 3001, SMS = 3002, etc.
        /// </summary>
        [DataMember]
        public UsagesSubTypes SubServiceTypeId { get; set; }

        /// <summary>
        /// onnet, offnet, international, etc.
        /// </summary>
        [DataMember]
        public Int32 CallTypeId { get; set; }

        /// <summary>
        /// Will identify the roaming operator (including the country) or will be null for national calls
        /// </summary>
        [DataMember]
        public String TSC { get; set; }

        [DataMember]
        public String BNumber { get; set; }

        /// <summary>
        /// Outgoing = 3000, Incoming = 3001
        /// </summary>
        [DataMember]
        public Int32 CallDirectionId { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Duration or amount (in case of data)
        /// </summary>
        [DataMember]
        public Decimal Bleg { get; set; }

        /// <summary>
        /// Promotion Plan ID, Will be NULL in case of being charged to unbilled balance
        /// </summary>
        [DataMember]
        public Int32 PromotionPlanId { get; set; }

        /// <summary>
        /// Charge + Amount1 + Amount2
        /// </summary>
        [DataMember]
        public Decimal Amount { get; set; }
    }
}
