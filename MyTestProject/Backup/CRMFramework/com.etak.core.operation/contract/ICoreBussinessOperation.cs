using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.contract
{
    /// <summary>
    /// Interface for business operation in the conencted/core model
    /// </summary>
    /// <typeparam name="TInternalInput">The type of the request, must extend RequestBase</typeparam>
    /// <typeparam name="TInternalOutput">The type of the response, must extend ResponseBase</typeparam>
    public interface ICoreBusinessOperation <TInternalInput, TInternalOutput>
        where TInternalInput : RequestBase
        where TInternalOutput : ResponseBase
    {
        /// <summary>
        /// Implementation of the operation
        /// </summary>
        /// <param name="request">the request in <typeparamref name="TInternalInput"/> format</param>
        /// <param name="invoker">the information with the environment of the request</param>
        /// <returns><typeparamref name="TInternalOutput"/>Result of the process</returns>
        TInternalOutput Process(TInternalInput request, RequestInvokationEnvironment invoker);

    }
}
