using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.contract
{
    /// <summary>
    /// Interface for the micro service layer, any ET micro service (unitary operation) needs to implement this interface
    /// </summary>
    /// <typeparam name="TInternalInput">The input type of the request</typeparam>
    /// <typeparam name="TInternalOutput">The output type of the request</typeparam>
    public interface IMicroService  <TInternalInput, TInternalOutput>
        where TInternalInput : RequestBase
        where TInternalOutput : ResponseBase
    {
        /// <summary>
        /// Method that performs the micro service implementation
        /// </summary>
        /// <param name="request">the micro service request</param>
        /// <param name="invoker">the information about the Invokation environment</param>
        /// <returns>the result of the micro service</returns>
        TInternalOutput Process(TInternalInput request, RequestInvokationEnvironment invoker);
    }
}
