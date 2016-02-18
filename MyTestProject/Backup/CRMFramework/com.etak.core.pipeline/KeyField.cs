using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using log4net;

namespace com.etak.core.pipeline
{
    /// <summary>
    /// Renames a thread taking the method name and the KeyFieldAttribute attribute used in the request
    /// </summary>
    public class KeyFieldThreadRenamer
    {
        static private IDictionary<Type, FieldInfo> TypeToKeyMapper = new Dictionary<Type, FieldInfo>();
        static private SpinLock lockObj = new SpinLock();
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets the value for a given request type
        /// </summary>
        /// <typeparam name="T">The type of the request that will be used as source
        /// to rename the thread</typeparam>
        /// <param name="value">the request itself to get the values from</param>
        /// <returns>The value of the KeyFieldAttribute for this instance (value)</returns>
        public static Object GetKeyValue<T>(T value)
        {
            FieldInfo keyFieldReflectionInfo;

            bool lockTaken = false;
            try
            {
                //This method must be thread safe to avoid contention, so we must take a fast lock
                lockObj.Enter(ref lockTaken);
                //Check if this is the first time this method was added
                if (!TypeToKeyMapper.TryGetValue(typeof(T), out keyFieldReflectionInfo))
                {
                    //Find the Field that has KeyFieldAttribute
                    keyFieldReflectionInfo = typeof(T).GetFields().FirstOrDefault(x => x.GetCustomAttributes(typeof(KeyFieldAttribute), true).Any());

                    //keep it in the dictionary so it's faster next time, if null, it will be detected next time.
                    TypeToKeyMapper.Add(typeof(T), keyFieldReflectionInfo);
                }
            }
            finally
            {
                if (lockTaken)
                    lockObj.Exit(false);
            }

            //FieldInfo is thread safe, so we can use it in multi-threaded environment.
            if (keyFieldReflectionInfo != null)
                return keyFieldReflectionInfo.GetValue(value);

            return ("");
        }

        /// <summary>
        /// Renames the thread 
        /// </summary>
        /// <typeparam name="T">The type of the request</typeparam>
        /// <param name="request">the request containing the object</param>
        /// <param name="invokedMethod">the front end method that received the request</param>
        public static void RenameThread<T>(T request, MethodBase invokedMethod)
        {
            //as Thread name is a write once property, we need to check if it is set to name it or not.
            if (string.IsNullOrEmpty(Thread.CurrentThread.Name))
            {
                logger.Info("Renaming Thread...");
                Thread.CurrentThread.Name = invokedMethod.Name + "-" + GetKeyValue<T>(request);
            }
            else
            {
                logger.InfoFormat("Cannot rename thread {0} as it already have a name!", Thread.CurrentThread.Name);
            }
            
        }

        /// <summary>
        /// Set the thread context of Log4Net, which will be printed in the log instead of the thread
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="invokedMethod"></param>
        public static void RenameLoggerContext<T>(T request, MethodBase invokedMethod)
        {
            LoggerContext.SetThreadContext("ThreadName", invokedMethod.Name + "-" + GetKeyValue<T>(request));
        }
    }
}