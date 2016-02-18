using System;

namespace com.etak.core.model.operation.messages
{
    /// <summary>
    /// The base class for any request in ET internal form
    /// </summary>
    abstract public  class RequestBase
    {
        /// <summary>
        /// The user authenticated
        /// </summary>
        public virtual LoginInfo User { get; set; }
       
        /// <summary>
        /// The Dealer obtained as a result of vmno parameter sent
        /// </summary>
        public virtual DealerInfo MVNO { get; set; }

        /// <summary>
        /// The external channel that performed the operation.
        /// </summary>
        public virtual String Channel { get; set; }
    }
}
