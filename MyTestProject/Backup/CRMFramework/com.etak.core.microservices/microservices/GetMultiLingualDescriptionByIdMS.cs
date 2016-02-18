using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.microservices.messages.GetMultiLingualDescriptionById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// Microservice to get a specific MultiLingual using its ID
    /// </summary>
    public class GetMultiLingualDescriptionByIdMS : IMicroService<GetMultiLingualDescriptionByIdRequest, GetMultiLingualDescriptionByIdResponse>
    {
        /// <summary>
        /// Method that performs the micro service implementation
        /// </summary>
        /// <param name="request">the micro service request</param>
        /// <param name="invoker">the information about the Invokation environment</param>
        /// <returns>the result of the micro service</returns>
        public GetMultiLingualDescriptionByIdResponse Process(GetMultiLingualDescriptionByIdRequest request, operation.RequestInvokationEnvironment invoker)
        {
            var repoMultiLingual = RepositoryManager.GetRepository<IMultiLingualDescriptionRepository<MultiLingualDescription>>();

            MultiLingualDescription multiLingualDescription = repoMultiLingual.GetById(request.MultiLingualDescriptionId);

            return new GetMultiLingualDescriptionByIdResponse()
            {
                MultiLingualDescription = multiLingualDescription,
                ResultType = ResultTypes.Ok,
            };
        }
    }
}
