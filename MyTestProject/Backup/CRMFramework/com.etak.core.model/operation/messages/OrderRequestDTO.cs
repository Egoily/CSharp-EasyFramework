using System;

namespace com.etak.core.model.operation.messages
{
    /// <summary>
    /// Base class for any order input parameter in DTO model
    /// </summary>
    public class OrderRequestDTO : RequestBaseDTO
    {
        /// <summary>
        /// The channel that performed the operation
        /// </summary>
        public String channel;

        /// <summary>
        /// The reference on the external system
        /// </summary>
        public String orderReference;
        
        /// <summary>
        /// comments of the order to improve trackability
        /// </summary>
        public String comments;
    }
}
