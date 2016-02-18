using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.microservices.messages.GetOrderById;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm.operation;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// GetOrderById Microservice, to get a certain order from db
    /// </summary>
    public class GetOrderByIdMS : IMicroService<GetOrderByIdRequest, GetOrderByIdResponse>
    {
        /// <summary>
        /// Get an Order using the OrderId specified
        /// </summary>
        /// <param name="request">the micro service request</param>
        /// <param name="invoker">the information about the Invokation environment</param>
        /// <returns>the result of the micro service</returns>
        public GetOrderByIdResponse Process(GetOrderByIdRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var repoOrder = RepositoryManager.GetRepository<IOrderRepository<Order>>();
            var order = repoOrder.GetById(request.OrderId);

            return new GetOrderByIdResponse()
            {
                ResultType = ResultTypes.Ok,
                Order = order
            };
        }
    }
}
