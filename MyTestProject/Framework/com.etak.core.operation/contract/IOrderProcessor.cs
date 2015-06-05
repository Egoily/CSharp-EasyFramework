using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.contract
{
    /// <summary>
    /// Interface for Order processor, It performs any action of TRequest type and TResponse type of an order of type TORder
    /// </summary>
    /// <typeparam name="TOrder">The type of order that is processed</typeparam>
    /// <typeparam name="TRequest">The type of request that afects the request</typeparam>
    /// <typeparam name="TReponse">The type of the response as a consecuence of the process</typeparam>
    public interface IOrderProcessor<TOrder, TRequest, TReponse>   
            where TOrder : Order
            where TRequest : RequestBase
            where TReponse : ResponseBase
    {
        /// <summary>
        /// Process a request for the order
        /// </summary>
        /// <param name="order">The order to be processed</param>
        /// <param name="request">The request that afects the request</param>
        /// <returns>the result of processing the request for the order</returns>
        TReponse ProcessRequest(TOrder order, TRequest request);


    }
}
