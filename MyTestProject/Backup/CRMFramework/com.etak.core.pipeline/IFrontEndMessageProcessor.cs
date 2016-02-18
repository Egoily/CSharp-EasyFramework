using System.Reflection;

namespace com.etak.core.pipeline
{
    /// <summary>
    /// Interface/Contract for any front end operation (Customer contract)
    /// </summary>
    /// <typeparam name="TInput">The type of the input parameter of the operation</typeparam>
    /// <typeparam name="TOutput">The type of the output of the operation</typeparam>
    public interface IFrontEndMessageProcessor<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        /// <summary>
        /// Process the request in customer model
        /// </summary>
        /// <param name="request">The request in customer model</param>
        /// <param name="callingMethod">the front end method invoked</param>
        /// <returns>The result of the process</returns>
        TOutput ProcessRequest(TInput request, MethodBase callingMethod);
    }
}
