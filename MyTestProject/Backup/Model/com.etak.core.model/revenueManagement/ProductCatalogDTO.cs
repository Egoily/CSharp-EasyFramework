using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    public class ProductCatalogDTO
    {
        /// <summary>
        /// Unique identifier of the product
        /// </summary>
        [DataMember]
        public Int64 Id;

        /// <summary>
        /// The name of the product. To be used to display to internal personnel, like to display on the application screens.
        /// </summary>
        [DataMember]
        public IList<TextualDescription> Names;

        /// <summary>
        /// The description of the product (for presenting to the customer).
        /// </summary>
        [DataMember]
        public IList<TextualDescription> Descriptions;

        /// <summary>
        /// The description of the product (for presenting to the customer).
        /// </summary>
        [DataMember]
        public IList<ProductPurchaseChargingOptionDTO> PurchaseOptions;

        /// <summary>
        /// The list of child products of this product in case a product group, null otherwise
        /// </summary>
        [DataMember]
        public IList<Int64> ChildProducts;

        /// <summary>
        /// The parent product in case of this product belongs to a product group, null otherwise
        /// </summary>
        [DataMember] //public Int64 ParentProduct; Previous declaration
        public IList<Int64> ParentProducts;

    }
}
