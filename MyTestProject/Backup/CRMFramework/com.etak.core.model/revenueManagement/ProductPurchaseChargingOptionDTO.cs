using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    public class ProductPurchaseChargingOptionDTO
    {
        /// <summary>
        /// Unique identifier of the pruduct charging option 
        /// </summary>
        [DataMember]
        public Int32 Id;

        /// <summary>
        /// identifier of the product that this option aplies to
        /// </summary>
        [DataMember]
        public Int32 ProductId;

        /// <summary>
        /// Description of the product charge option
        /// </summary>
        [DataMember]
        public IList<TextualDescription> Description;

        /// <summary>
        /// The start date of the period of the validity to purchase this product
        /// </summary>
        [DataMember]
        public DateTime EffectiveDate;

        /// <summary>
        /// The end date of the period of the validity to purchase this product
        /// </summary>
        [DataMember]
        public DateTime? ExpirationDate;


        /// <summary>
        /// List of charges asociated to the charge option
        /// </summary>
        [DataMember]
        public IList<ChargeCatalogDTO> Charges;
    }
}
