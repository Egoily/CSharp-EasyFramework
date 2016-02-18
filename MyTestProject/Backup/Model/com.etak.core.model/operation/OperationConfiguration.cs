using System;

namespace com.etak.core.model.operation
{
    /// <summary>
    /// Entity to store configuration of a business operation
    /// </summary>
    public class OperationConfiguration
    {
        /// <summary>
        /// Unique Id of the entity
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// Bussiness Operation
        /// </summary>
        public virtual BusinessOperation Operation { get; set; }

        /// <summary>
        /// Start date of the validity period for this configuration
        /// </summary>
        public virtual DateTime StarTime { get; set; }

        /// <summary>
        /// End date of the validity period for this configuration
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// Dealer/VMNO that this configuration applies to
        /// </summary>
        public virtual DealerInfo MVNO { get; set; }

        /// <summary>
        /// Json serialized configuration. 
        /// </summary>
        public virtual String JSonConfig { get; set; }
    }
}
