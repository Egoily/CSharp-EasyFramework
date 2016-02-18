using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.contract.exceptions
{
   /// <summary>
   /// Exception thrown when the external reference provided is not unique
   /// </summary>
    public class DuplicatedReferenceException : ElephantTalkBaseException
    {
       
        /// <summary>
        /// The result type when DuplicatedReferenceException happens (ResultTypes.DuplicatedReference) 
        /// </summary>
        public override ResultTypes ResultType
        {
            get { return ResultTypes.DuplicatedReference; }
        }

        /// <summary>
        /// Constructor providing error code an message
        /// </summary>
        /// <param name="message">the message for the exception</param>
        /// <param name="errorCode">the error code of the exception</param>
        public DuplicatedReferenceException(string message, int errorCode) : base(message, errorCode)
        {
        }

        /// <summary>
        /// Constructor for the exception
        /// </summary>
        /// <param name="message">message of the exception</param>
        /// <param name="inner">inner exception</param>
        /// <param name="errorCode">the error code</param>
        public DuplicatedReferenceException(string message, Exception inner, int errorCode)
            : base(message, inner, errorCode)
        {
        }
    }
}
