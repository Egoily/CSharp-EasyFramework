using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.contract.exceptions
{
    /// <summary>
    /// Exception thrown when there is an authentication problem
    /// Missing user or password, incorrect user name password combination...
    /// </summary>
    public class AuthenticationErrorException : ElephantTalkBaseException
    {
        /// <summary>
        /// Indicates the type/category of the error
        /// </summary>
        public override ResultTypes ResultType
        {
            get { return ResultTypes.AuthenticationError; }
        }

        /// <summary>
        /// Constructor for exception providing error message and error code
        /// </summary>
        /// <param name="message">the message with the cause for the exception</param>
        /// <param name="errorCode">the error code for the exception</param>
        public AuthenticationErrorException(string message, int errorCode) : base(message, errorCode) { }

        /// <summary>
        /// Constructor for exception providing error message and error code and the inner exception that caused the error
        /// </summary>
        /// <param name="message">the message with the cause for the exception</param>
        /// <param name="inner">the inner exception that rised this exception</param>
        /// <param name="errorCode">the error code for the exception</param>
        public AuthenticationErrorException(string message, Exception inner, int errorCode) : base(message, inner, errorCode) { }

       
    }
}
