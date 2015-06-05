using System;
using System.Reflection;
using log4net;

namespace com.etak.core.operation.implementation
{
    /// <summary>
    /// static Logger non generic for micro services
    /// </summary>
    public static class MicroServiceLogger
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Logs a message with debug level
        /// </summary>
        /// <param name="message">the message to log</param>
        public static void LogDebug(String message)
        {
            if (Log.IsDebugEnabled)
                Log.Debug(message);
        }
    }
}
