using System;
using log4net;

namespace com.etak.core.jms.nmstracing
{
    /// <summary>
    /// Tracing based on log4net ofr NMS Apache library
    /// </summary>
    public class NMSTraceAdapter : Apache.NMS.ITrace
    {
        //keep logs on behalf of NMS Tracer
        static readonly ILog Log = LogManager.GetLogger(typeof(Apache.NMS.Tracer));

        /// <summary>
        /// Checks if logger has Debug level enabled
        /// </summary>
        public bool IsDebugEnabled
        {
            get { return Log.IsDebugEnabled; }
        }

        /// <summary>
        /// Checks if logger has Info level enabled
        /// </summary>
        public bool IsInfoEnabled
        {
            get { return Log.IsInfoEnabled; }
        }

        /// <summary>
        /// Checks if logger has Warn level enabled
        /// </summary>
        public bool IsWarnEnabled
        {
            get { return Log.IsWarnEnabled; }
        }

        /// <summary>
        /// Checks if logger has Error level enabled
        /// </summary>
        public bool IsErrorEnabled
        {
            get { return Log.IsErrorEnabled; }
        }

        /// <summary>
        /// Checks if logger has Fatal level enabled
        /// </summary>
        public bool IsFatalEnabled
        {
            get { return Log.IsFatalEnabled; }
        }

       /// <summary>
       /// Writes a message with Debug level
       /// </summary>
       /// <param name="message">the message to write</param>
        public void Debug(string message)
        {
            Log.Debug(message);
        }

        /// <summary>
        /// Writes a message with Info level
        /// </summary>
        /// <param name="message">the message to write</param>
        public void Info(string message)
        {
            Log.Info(message);
        }

        /// <summary>
        /// Writes a message with Warn level
        /// </summary>
        /// <param name="message">the message to write</param>
        public void Warn(string message)
        {
            Log.Warn(message);
        }

        /// <summary>
        /// Writes a message with Error level
        /// </summary>
        /// <param name="message">the message to write</param>
        public void Error(string message)
        {
            Log.Error(message);
        }

        /// <summary>
        /// Writes a message with Fatal level
        /// </summary>
        /// <param name="message">the message to write</param>
        public void Fatal(object message)
        {
            Log.Fatal(message);
        }

        /// <summary>
        /// Writes a message with Fatal level
        /// </summary>
        /// <param name="message">the message to write</param>
        public void Fatal(String message)
        {
            Log.Fatal(message);
        }
    }
}
