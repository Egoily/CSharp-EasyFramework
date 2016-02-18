using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    public class ProductOfferingGroupDTO : ProductOfferingOptionDTO
    {
        /// <summary>
        /// ProductOfferingGroup Id
        /// </summary>
        [DataMember]
        public Int32 GroupId;

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
