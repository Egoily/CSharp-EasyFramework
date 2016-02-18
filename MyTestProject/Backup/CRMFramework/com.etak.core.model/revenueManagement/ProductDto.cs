using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    public class ProductDto
    {
        public ProductDto()
        {
            Names = new List<TextualDescription>();
            Descriptions = new List<TextualDescription>();
        }

        /// <summary>
        /// Unique idenfitier of the product
        /// </summary>
        [DataMember]
        public Int32 Id;
        /// <summary>
        /// External Reference of the Product
        /// </summary>
        [DataMember]
        public String ExternalReference;

        /// <summary>
        /// The Description of the ProductType of the product
        /// </summary>
        [DataMember] 
        public String ProductType;

        /// <summary>
        /// CarrierId of the Carrier (if any) 
        /// </summary>
        [DataMember]
        public Int32? CarrierId;

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
    }
}
