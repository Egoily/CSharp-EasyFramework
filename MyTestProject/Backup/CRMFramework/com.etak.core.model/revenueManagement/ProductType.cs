using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// The possible product types
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProductType
    {
        /// <summary>
        /// Unique Id of the product type
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// Description for the product type
        /// </summary>
        public virtual String Description { get; set; }
    }
}