using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace com.etak.core.pipeline
{
    /// <summary>
    /// Renames a thread taking the method name and the KeyFieldAttribute attribute used in the request
    /// </summary>
    public class KeyFieldThreadRenamer
    {
        static private IDictionary<Type, FieldInfo> TypeToKeyMapper = new Dictionary<Type, FieldInfo>();
        static private SpinLock lockObj = new SpinLock();

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
            if (string.IsNullOrEmpty(Thread.CurrentThread.Name))
                Thread.CurrentThread.Name = invokedMethod.Name + "-" + GetKeyValue<T>(request);
        }
    }
}