using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.subscription.catalog
{
    /// <summary>
    /// The offer of a given product under an specific set of terms and conditions. 
    /// The product offering can be also a template for other products offerings, in fact a ProductOffering can reference N templates. 
    /// This means that the terms and conditions or options of an offering should be considering including all terms and conditions and options of all the templates referenced.
    /// </summary>
    public class ProductOffering
    {
        public ProductOffering()
        {            
            ChargingOptions = new List<ProductChargeOption>();
        }

        /// <summary>
        /// Unique ID of the product offering
        /// </summary>
        public virtual Int32 Id { get; set; }


        /// <summary>
        /// The product definition that this offering reffers to
        /// </summary>
        public virtual Product OfferedProduct { get; set; }


        /// <summary>
        /// Possible options that this product supports
        /// </summary>
        public virtual IList<ProductOfferingOption> Options { get; set; }

        /// <summary>
        /// Templating of the offering, the actual commitmetns will be the the result of the
        /// agggregation of all child templates.
        /// </summary>
        public virtual IList<ProductOffering> OfferingChildTemplates { get; set; }

        /// <summary>
        /// Templating of the offering, the actual commitmetns will be the the result of the
        /// agggregation of all parent templates .
        /// </summary>
        public virtual IList<ProductOffering> OfferingParentTemplates { get; set; }

        /// <summary>
        /// The name of the product offering. To be used to display to internal personnel, like to display on the application screens.
        /// </summary>
        public virtual MultiLingualDescription Names { get; set; }

        /// <summary>
        /// Set  of descriptions for the product offering.
        /// </summary>
        public virtual MultiLingualDescription Description { get; set; }


        public virtual Boolean IsProductOption { get; set; }

        public virtual ProductOfferingGroup Group { get; set; }

        /// <summary>
        /// List of charging options available to purchase the product
        /// </summary>
        public virtual IList<ProductChargeOption> ChargingOptions { get; set; }


        public virtual IList<ProductOfferingTimeRange> ProductOfferingTimeRanges { get; set; } 


    }

}
