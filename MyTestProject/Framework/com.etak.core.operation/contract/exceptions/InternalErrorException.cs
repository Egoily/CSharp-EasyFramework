using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.contract.exceptions
{
    /// <summary>
    /// Handled exception at framework level produced by an exception that is not an elephant talk exception
    /// </summary>
    public class InternalErrorException : ElephantTalkBaseException
    {
       
        /// <summary>
        /// The result type associeated to this type of exception (ResultTypes.UnknownError)
        /// </summary>
        public override ResultTypes ResultType
        {
            get { return ResultTypes.UnknownError; }
        }


        /// <summary>
        /// Constructor for exception providing error message and error code
        /// </summary>
        /// <param name="message">the message with the cause for the exception</param>
        /// <param name="errorCode">the error code for the exception</param>
        public InternalErrorException(string message, int errorCode) : base(message, errorCode) { }

        /// <summary>
        /// Constructor for exception providing error message and error code and the inner exception that caused the error
        /// </summary>
        /// <param name="message">the message with the cause for the exception</param>
        /// <param name="inner">the inner exception that rised this exception</param>
        /// <param name="errorCode">the error code for the exception</param>
        public InternalErrorException(string message, Exception inner, int errorCode) : base(message, inner, errorCode) { }
    }
}
