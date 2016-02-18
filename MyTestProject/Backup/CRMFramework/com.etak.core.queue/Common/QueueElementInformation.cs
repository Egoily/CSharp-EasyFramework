using System;

namespace com.etak.core.queue.Common
{
    /// <summary>
    /// Holds delivery status and dates of a queued element
    /// </summary>
    /// <typeparam name="T">The type of elements queued</typeparam>
    public class QueueElementInformation<T>
    {
        /// <summary>
        /// Creates a new QueueElementInformation keeping the Object of type T
        /// </summary>
        /// <param name="o">the object to be wrapped</param>
        public QueueElementInformation(T o)
        {
            TryCounter = 0;
            LastProcessedTimestamp = DateTime.MinValue;
            Element = o;
        }

        /// <summary>
        /// The times of the element has been processed.
        /// </summary>
        public Int32 TryCounter { get; set; }

        /// <summary>
        /// The last timestamp of the element was processed.
        /// </summary>
        public DateTime LastProcessedTimestamp { get; set; }

        /// <summary>
        /// The element stands for the request.
        /// </summary>
        public T Element { get; set; }
    }
}
