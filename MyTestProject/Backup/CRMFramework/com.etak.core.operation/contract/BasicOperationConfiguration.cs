using System;

namespace com.etak.core.operation.contract
{
    /// <summary>
    /// Class to store basic properties that applies to all possible configurations. 
    /// </summary>
    public class BasicOperationConfiguration
    {
        /// <summary>
        /// Indicates whether an event should be sent to the event system
        /// in case the operation is sucessfully completed
        /// </summary>
        public Boolean? SendSucessfulOperationsToEventSystem { get; set; }
    }
}
