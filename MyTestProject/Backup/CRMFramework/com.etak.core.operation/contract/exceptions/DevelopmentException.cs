
using System;

namespace com.etak.core.operation.contract.exceptions
{
    /// <summary>
    /// This exception is thrown when the framework detects an error
    /// that is produced by a development error and will never work
    /// </summary>
    public class DevelopmentException : Exception
    {
        /// <summary>
        /// Constructor defined to force calling the Exception Constructor
        /// </summary>
        public DevelopmentException() : base()
        {
        }

        /// <summary>
        /// Constructor defined to force calling the Exception Constructor
        /// </summary>
        public DevelopmentException(string message) : base(message) { }

        /// <summary>
        /// Constructor defined to force calling the Exception Constructor
        /// </summary>
        public DevelopmentException(string message, Exception innerException) : base(message, innerException) { }

    }
}
