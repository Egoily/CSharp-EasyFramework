using System;

namespace com.etak.core.model.revenueManagement
{
    [Serializable]
    public class TaxRates
    {
        /// <summary>
        /// Unique id of the tax
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// Reference to the definition (FK)
        /// </summary>
        public virtual TaxDefinition Definition { get; set; }

        /// <summary>
        /// Start time for tax ranges validity period
        /// </summary>
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// End time for tax ranges validity period
        /// </summary>
        public virtual DateTime ? EndDate { get; set; }

        /// <summary>
        /// Percentage to apply
        /// </summary>
        public virtual Decimal Percentage { get; set; }
    }
}
