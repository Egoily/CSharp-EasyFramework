using System;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// A range of zip codes that identifies a TaxDefinition
    /// </summary>
    public class TaxZipRanges 
    {
        /// <summary>
        /// Unique Id the taz range
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// The owner of the ranges
        /// </summary>
        public virtual TaxDefinition RangeOwner { get; set; }

        /// <summary>
        /// The low range for the zip code
        /// </summary>
        public virtual String ZipRangesLow { get; set; }

        /// <summary>
        /// the high range for the zip code
        /// </summary>
        public virtual String ZipRangesHigh { get; set; }
    }
}
