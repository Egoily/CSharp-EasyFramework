using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.contract.exceptions
{   
    /// <summary>
    /// Exception thrown when the validation is not successfull
    /// </summary>
    public class DataValidationErrorException : ElephantTalkBaseException
    {
      
        /// <summary>
        /// The result type associated to this exception
        /// </summary>
        public override ResultTypes ResultType
        {
            get { return ResultTypes.DataValidationError; }
        }

        /// <summary>
        /// Constructor for exception providing error message and error code
        /// </summary>
        /// <param name="message">the message with the cause for the exception</param>
        /// <param name="errorCode">the error code for the exception</param>
        public DataValidationErrorException(string message, int errorCode) : base(message, errorCode) { }

        /// <summary>
        /// Constructor for exception providing error message and error code and the inner exception that caused the error
        /// </summary>
        /// <param name="message">the message with the cause for the exception</param>
        /// <param name="inner">the inner exception that rised this exception</param>
        /// <param name="errorCode">the error code for the exception</param>
        public DataValidationErrorException(string message, Exception inner, int errorCode) : base(message, inner, errorCode) { }
    }
}
