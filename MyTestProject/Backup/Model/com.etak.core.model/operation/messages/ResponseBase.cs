using System;

namespace com.etak.core.model.operation.messages
{
    /// <summary>
    /// The base class/type for all the bussiness operations in the internal form
    /// </summary>
    abstract public class ResponseBase
    {
        /// <summary>
        /// The result type of the operation
        /// </summary>
        public virtual ResultTypes ResultType {get;set;}

        /// <summary>
        /// The error code of the opertaion
        /// </summary>
        public virtual Int32 ErrorCode { get; set; }

        /// <summary>
        /// Set of messages explaining the result of the operation
        /// </summary>
        public virtual String Message { get; set; }
    }
}
