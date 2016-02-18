using System;

namespace com.etak.core.queue.Messages
{
    /// <summary>
    /// Enum with the possible resutls of a delivery attempt
    /// </summary>
    public enum DeliverResult
    {
        /// <summary>
        /// The delivery was sucessfull
        /// </summary>
        Ok, 
        /// <summary>
        /// The delivery failed
        /// </summary>
        Error,
    }

    /// <summary>
    /// The response of an attempt to deliver a message
    /// </summary>
    public class QueueDeliverMessageResponse
    {
        /// <summary>
        /// The result of the attempt
        /// </summary>
        public DeliverResult Result { get; set; }

        /// <summary>
        /// A code indicating the error (0 if no error happened)
        /// </summary>
        public Int32 ResultCode { get; set; }

        /// <summary>
        /// Number of elements processed in the delivery
        /// </summary>
        public Int32 ElementsProccessed { get; set; }

        /// <summary>
        /// Descriptive message of the error.
        /// </summary>
        public String ResultMessage { get; set; }

        public override string ToString()
        {
            return "Result: " + Result + " ResultCode: " + ResultCode + " ResultMessage:" + ResultMessage + " ElementsProccessed: " + ElementsProccessed;
        }
    }
}
