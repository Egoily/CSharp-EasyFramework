using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    public enum DefaultOptions
    {
        Y,
        N
    }

    public enum ProductPurchaseStatus
    {
        Default = 0,
    }

    [DataContract]
    [Serializable]
    public class ProductChargeOption
    {
        public ProductChargeOption()
        {
            Charges = new List<Charge>();
        }
        /// <summary>
        /// Unique identifier of the product charge option
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// The product to which this option references
        /// </summary>
        public virtual Product ProductOfOption { get; set; }

        /// <summary>
        /// Status of the product charge option
        /// </summary>
        public virtual ProductPurchaseStatus Status { get; set; }

        /// <summary>
        /// Date in which this option was created
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// Name of the product charge option, only for internal use
        /// </summary>
        public virtual MultiLingualDescription Name { get; set; }

        /// <summary>
        /// Name of the product charge option, to be shown to the customer
        /// </summary>
        public virtual MultiLingualDescription Description { get; set; }

        /// <summary>
        /// List of charges asociated to this product charge option
        /// </summary>
        public virtual IList<Charge> Charges { get; set; } 

        /// <summary>
        /// When the option starts to become effective.
        /// </summary>
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// When the option stops to become effective.
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// Whether the option is the default one for the product.
        /// </summary>
        public virtual DefaultOptions IsDefaultOption { get; set; }
    }
}