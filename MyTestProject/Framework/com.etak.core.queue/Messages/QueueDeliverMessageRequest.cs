using System.Collections.Generic;

namespace com.etak.core.queue.Messages
{
    /// <summary>
    /// Enumerated with all possible types of serialization
    /// </summary>
    public enum SerializationType
    {
        /// <summary>
        /// Serialize using JSON
        /// </summary>
        JSON,
        /// <summary>
        /// Serialize using XML
        /// </summary>
        XML,
        RAW,
    }

    /// <summary>
    /// Message/wrapper to enqueue an element in the queue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueueDeliverMessageRequest <T>
    {
        /// <summary>
        /// The type of serialization  to be used
        /// </summary>
        public SerializationType SerializationType { get; set; }

        /// <summary>
        /// The set of elements to be queued.
        /// </summary>
        public IList<T> Elements { get; set; }
    }
}
