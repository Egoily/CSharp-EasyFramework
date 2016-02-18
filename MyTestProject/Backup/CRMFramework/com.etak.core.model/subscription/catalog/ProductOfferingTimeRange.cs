using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.subscription.catalog
{
    /// <summary>
    /// Possible kind of ProductOptions
    /// </summary>
    public enum ProductOfferingTimeRangeStatus
    {
        Active = 0,

        Deactive = 1,

    }

    public class ProductOfferingTimeRange
    {
        /// <summary>
        /// Unique ID of the product time range
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// The range of dates in which this offering is valid.
        /// </summary>
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// The range of dates in which this offering is valid.
        /// </summary>
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// The status of the Time Range option
        /// </summary>
        public virtual ProductOfferingTimeRangeStatus Status { get; set; }

        /// <summary>
        /// The product offering that applies to this time range
        /// </summary>
        public virtual ProductOffering ProductOffering { get; set; }
    }
}
