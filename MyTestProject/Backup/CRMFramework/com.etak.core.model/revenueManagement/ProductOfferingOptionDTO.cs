using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    public class ProductOfferingOptionDTO
    {
        /// <summary>
        /// The if of the option
        /// </summary>
        [DataMember]
        public Int32 Id;

        /// <summary>
        /// Strategy to be applied
        /// </summary>
        [DataMember]
        public ProductConflictResolutionsStrategies Strategy;

        /// <summary>
        /// Max Occurs value for this option (= 0 means restristed)
        /// </summary>
        [DataMember]
        public int MaxOccurs;

        /// <summary>
        /// Min Occurs value for this option (>= 1 means mandatory)
        /// </summary>
        [DataMember]
        public int MinOccurs;

        /// <summary>
        /// Determines the type of OptionType
        /// </summary>
        [DataMember]
        public ProductOfferingOptionTypes OptionType;

    }
}
