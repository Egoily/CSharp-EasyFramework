using com.etak.core.microservices.messages.PersistOperationConfiguration;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm.configuration;
using Newtonsoft.Json;

namespace com.etak.core.microservices.microservices
{
    /// <summary>
    /// Class that persist an OperationConfiguration
    /// </summary>
    public class PersistOperationConfigurationMS: IMicroService<PersistOperationConfigurationRequest, PersistOperationConfigurationResponse>
    {
        /// <summary>
        /// Implementation of the mircoservice, creates a new OperationConfiguration with the input parameters
        /// in the request
        /// </summary>
        /// <param name="request">Contains the fields to create the operation config</param>
        /// <param name="invoker">the environment that is invoking the MS, ignored for this micro service</param>
        /// <returns>the Operation configurationc created.</returns>
        public PersistOperationConfigurationResponse Process(PersistOperationConfigurationRequest request, RequestInvokationEnvironment invoker)
        {
            OperationConfiguration configToPersist = new OperationConfiguration
            {
                StarTime = request.StartDate,
                EndDate = request.EndDate,
                Operation = request.OperationDefinition,
                MVNO = request.MVNO,
                JSonConfig = JsonConvert.SerializeObject(request.ConfigSettings),
              
            };
            IOperationConfigurationRepository<OperationConfiguration> opConfigRepo = RepositoryManager.GetRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            opConfigRepo.Create(configToPersist);

            return new PersistOperationConfigurationResponse
            {
                ResultType = ResultTypes.Ok,
                ErrorCode = 0,
                Message = null,
                OperationConfiguration = configToPersist,
            };
        }
    }
}
