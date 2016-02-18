using System;
using System.Collections.Generic;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Hold the definiton of a tax. 
    /// </summary>
    [Serializable]
    public class TaxDefinition
    {
        /// <summary>
        /// Unique Id of the tax definition
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// Texts descriptions for the tax
        /// </summary>
        public virtual MultiLingualDescription Description { get; set; }

        /// <summary>
        /// The category of the charge
        /// </summary>
        public virtual Int32 TaxCategory { get; set; }

        /// <summary>
        /// List of rates with time ranges. 
        /// </summary>
        public virtual IList<TaxRates> Rates { get; set; }

        /// <summary>
        /// List of zip code ranges that apply to this definition 
        /// </summary>
        public virtual IList<TaxZipRanges> ZipRanges { get; set; }
    }
}
