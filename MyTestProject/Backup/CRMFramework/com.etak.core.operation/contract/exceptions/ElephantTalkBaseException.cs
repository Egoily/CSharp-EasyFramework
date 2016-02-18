using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.contract.exceptions
{
    /// <summary>
    /// Abstract class for operations, all Exceptions needs to inherit from this class.
    /// </summary>
    [Serializable]
    public abstract class ElephantTalkBaseException : Exception
    {
        /// <summary>
        /// Error code of the exception
        /// </summary>
        public int ErrorCode { get; set; }

           /// <summary>
        /// ResultType for the exception.
        /// </summary>
        public abstract ResultTypes ResultType { get; }
        
       

        /// <summary>
        /// Constructor providing a message and an error code
        /// </summary>
        /// <param name="message">Message of the exception</param>
        /// <param name="errorCode">Error code of the exception</param>
        protected ElephantTalkBaseException(string message, int errorCode) : base(message) { this.ErrorCode = errorCode; }

        /// <summary>
        /// onstructor providing a message and an error code
        /// </summary>
        /// <param name="message">Message of the exception</param>
        /// <param name="inner">Inner exception that triggerd this exception</param>
        /// <param name="errorCode">Error code of the exception</param>
        protected ElephantTalkBaseException(string message, Exception inner, int errorCode) : base(message, inner) { ErrorCode = errorCode; }
        
       
    }

   

  

  
}
