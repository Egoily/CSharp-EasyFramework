using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.contract.exceptions
{
    /// <summary>
    /// Exceptions that operation throws in case of business logic error
    /// </summary>
    public class BusinessLogicErrorException : ElephantTalkBaseException
    {
        /// <summary>
        /// The result type associated to this exception
        /// </summary>
        public override ResultTypes ResultType
        {
            get { return ResultTypes.BussinessLogicError; }
        }

        /// <summary>
        /// Constructor for exception providing error message and error code
        /// </summary>
        /// <param name="message">the message with the cause for the exception</param>
        /// <param name="errorCode">the error code for the exception</param>
        public BusinessLogicErrorException(string message, int errorCode) : base(message, errorCode) { }


        /// <summary>
        /// Constructor for exception providing error message and error code and the inner exception that caused the error
        /// </summary>
        /// <param name="message">the message with the cause for the exception</param>
        /// <param name="inner">the inner exception that rised this exception</param>
        /// <param name="errorCode">the error code for the exception</param>
        public BusinessLogicErrorException(string message, Exception inner, int errorCode) : base(message, inner, errorCode) { }
    }
}
