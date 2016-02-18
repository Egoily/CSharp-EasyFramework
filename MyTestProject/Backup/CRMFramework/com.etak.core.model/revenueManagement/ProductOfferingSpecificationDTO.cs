using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    public class ProductOfferingSpecificationDTO : ProductOfferingOptionDTO
    {
        /// <summary>
        /// Product offering with a certain relation with the Product Offering
        /// </summary>
        [DataMember]
        public ProductCatalogDTO SpecifiedProductOffering;
    }
}
