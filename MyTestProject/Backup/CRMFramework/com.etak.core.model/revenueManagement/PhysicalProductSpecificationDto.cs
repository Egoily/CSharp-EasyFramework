using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.revenueManagement
{
    public class PhysicalProductSpecificationDto : ProductDto
    {
        /// <summary>
        /// The Id of the specification
        /// </summary>
        [DataMember]
        public long SpecificationId;

        /// <summary>
        /// SKU value of the physical product
        /// </summary>
        [DataMember]
        public string SKU;

        /// <summary>
        /// The model number of the physical product
        /// </summary>
        [DataMember]
        public string ModelNumber;

        /// <summary>
        /// The image Url of the product
        /// </summary>
        [DataMember]
        public string ImageUrl;

        /// <summary>
        /// The name of the product. To be used to display to internal personnel, like to display on the application screens.
        /// </summary>
        [DataMember]
        public IList<TextualDescription> SpecificationNames;

        /// <summary>
        /// The description of the product (for presenting to the customer).
        /// </summary>
        [DataMember]
        public IList<TextualDescription> SpecificationDescriptions;

        /// <summary>
        /// Color description of the device
        /// </summary>
        [DataMember]
        public IList<TextualDescription> Colors;

        /// <summary>
        /// Capacity of the device
        /// </summary>
        [DataMember]
        public IList<TextualDescription> Storages;

        /// <summary>
        /// OS of the device
        /// </summary>
        [DataMember]
        public IList<TextualDescription> OperationSystems;

        /// <summary>
        /// Brand of the device
        /// </summary>
        [DataMember]
        public IList<TextualDescription> Brands;

        /// <summary>
        /// Front camera
        /// </summary>
        [DataMember]
        public IList<TextualDescription> FrontCamera;

        [DataMember]
        public IList<TextualDescription> BackCamera;

    }
}
