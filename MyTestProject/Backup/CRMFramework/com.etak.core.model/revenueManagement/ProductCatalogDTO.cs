using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    public class ProductCatalogDTO
    {
        public ProductCatalogDTO()
        {
            Names = new List<TextualDescription>();
            Descriptions = new List<TextualDescription>();
            PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>();
            Options = new List<ProductOfferingOptionDTO>();
        }

        /// <summary>
        /// Unique identifier of the product offering
        /// </summary>
        [DataMember]
        public Int64 Id;

        /// <summary>
        /// ProductDto that contains the basic information about the product offered by the Product Offering
        /// </summary>
        [DataMember] 
        public ProductDto ProductDto;

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
        /// Determines the options for the current product offering
        /// </summary>
        [DataMember]
        public IList<ProductOfferingOptionDTO> Options;

    }

    
}
