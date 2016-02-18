using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.subscription.catalog
{
    [DataContract]
    public class PurchaseProductOfferingSelectionDTO
    {
        /// <summary>
        /// The ProductOffering to be purchased
        /// </summary>
        [DataMember] public int ProductOfferingId;

        /// <summary>
        /// The ProductChargeOptionId to be applied
        /// </summary>
        [DataMember]
        public int ProductOfferingChargeOptionId;

        /// <summary>
        /// A list of product offering selection options to be purchased
        /// </summary>
        [DataMember]
        public List<PurchaseProductOfferingSelectionDTO> Options;
    }
}
