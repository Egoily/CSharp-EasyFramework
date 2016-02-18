using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CustomerDataRoamingLimit
    {

        public virtual long? ID
        { get; set; }

        public virtual CustomerInfo Customer
        { get; set; }
        /// <summary>
        /// Data Roaming Limit
        /// </summary>
        public virtual decimal? DataRoamingLimit
        { get; set; }
        /// <summary>
        /// Continue SMS
        /// </summary>
        public virtual decimal? ContinueSUM
        { get; set; }
        /// <summary>
        /// Real Consumption.. , SYNC by DRE
        /// </summary>
        public virtual decimal? DataRoamingConsumptionCounter
        { get; set; }

        // AnnaM: 20140409
        public virtual bool? ContinueBySMS { get; set; }

    }
}
