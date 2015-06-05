using System;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.PersistOperationConfiguration
{
    /// <summary>
    /// Request for PersistOperationConfiguration Microservice
    /// </summary>
    public class PersistOperationConfigurationRequest : RequestBase
    {
        /// <summary>
        /// Start date for the validity period of the configuration
        /// </summary>
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// EndDate for the validity period of the configuration
        /// </summary>
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// Object containing the configurations to persist.
        /// </summary>
        public virtual Object ConfigSettings { get; set; }

        /// <summary>
        /// MVNO to which this operation configuration applies to
        /// </summary>
        public override DealerInfo MVNO { get; set; }

        /// <summary>
        /// Discriminator of the operation to which this configuration applies
        /// </summary>
        public string OperationDiscriminator { get; set; }

        /// <summary>
        /// Operation code of the operation to which this configuration applies
        /// </summary>
        public string OperationCode { get; set; }
    }

}
