using System;

namespace com.etak.core.model.operation.messages
{
    /// <summary>
    /// Base class for any order in ET internal Model
    /// </summary>
    abstract public class CreateNewOrderRequest : RequestBase
    {

        /// <summary>
        /// The reference provided by the external system as unique reference
        /// </summary>
        public virtual String ExternalReference { get; set; }
    }
}
