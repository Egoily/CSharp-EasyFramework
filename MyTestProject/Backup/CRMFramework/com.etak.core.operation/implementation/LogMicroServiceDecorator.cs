using System;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;

namespace com.etak.core.operation.implementation
{
    /// <summary>
    /// Decorator like pattern based on inheritance for MicroService, it logs before and after the 
    /// actual processing
    /// </summary>
    /// <typeparam name="TInput">The input parameter for the microservice</typeparam>
    /// <typeparam name="TOutput">The output parameter for the microservice</typeparam>
    /// <typeparam name="TProcessor">The actual processor of the microservice</typeparam>
    public class LogMicroServiceDecorator<TInput, TOutput, TProcessor> : IMicroService<TInput, TOutput> 
        where TInput : RequestBase where TOutput : ResponseBase
        where TProcessor : IMicroService<TInput, TOutput> ,new()
    {
        /// <summary>
        /// Implementation of the IMicroService logging before and
        /// after the underlaying implementation. 
        /// </summary>
        /// <param name="request">The input parameter for the original implementation</param>
        /// <param name="invoker">The invokation environment</param>
        /// <returns>the otput parameter for the original implementation</returns>
        public TOutput Process(TInput request, RequestInvokationEnvironment invoker)
        {
            TProcessor processor = new TProcessor();
            MicroServiceLogger.LogDebug(String.Format("Start Invoking MS:'{0}'", typeof(TProcessor).Name));
            TOutput result = processor.Process(request, invoker);
            MicroServiceLogger.LogDebug("MS Complete");
            return result;
        }
    }
}
