using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace com.etak.core.pipeline
{
    /// <summary>
    /// Class to manage the Log4Net Contexts
    /// </summary>
    public class LoggerContext
    {
        /// <summary>
        /// Sets a Property Value in a thread context
        /// </summary>
        /// <param name="property">Property name to be set</param>
        /// <param name="value">Value of the Property</param>
        public static void SetThreadContext(String property, String value)
        {
            ThreadContext.Properties[property] = value;
        }

        /// <summary>
        /// Sets a Property Value in a thread context
        /// </summary>
        /// <param name="property">Property name to be set</param>
        /// <param name="value">Value of the Property</param>
        public static void SetGlobalContext(String property, String value)
        {
            GlobalContext.Properties[property] = value;
        }
    }
}
