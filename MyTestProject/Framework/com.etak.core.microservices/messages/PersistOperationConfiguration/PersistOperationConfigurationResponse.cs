using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;

namespace com.etak.core.microservices.messages.PersistOperationConfiguration
{
    /// <summary>
    /// Response for PersistOperationConfiguration Microservice
    /// </summary>
    public class PersistOperationConfigurationResponse : ResponseBase
    {
        /// <summary>
        /// The operation configuration created in the Micro service
        /// </summary>
        public OperationConfiguration OperationConfiguration { get; set; }
    }
}
