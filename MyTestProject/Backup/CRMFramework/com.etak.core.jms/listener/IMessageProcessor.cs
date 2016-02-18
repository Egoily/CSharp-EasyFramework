using Apache.NMS;

namespace com.etak.core.jms.listener
{
    /// <summary>
    /// Interface that must be provided to the JMS listener to ivoke
    /// for each message
    /// </summary>
    public interface  IMessageProcessor
    {
        /// <summary>
        /// Process the message read from the queue
        /// if and exception is thrown and retried later with the
        /// redelivery parameters configured
        /// </summary>
        /// <param name="message">The message that must be processed</param>
        void ProcessMessage(IMessage message);
    }
}
