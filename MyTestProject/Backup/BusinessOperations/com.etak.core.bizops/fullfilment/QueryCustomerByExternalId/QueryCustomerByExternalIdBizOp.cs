using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByExternalId
{
    /// <summary>
    /// Operation to query Customer information by External Id
    /// </summary>
    public class QueryCustomerByExternalIdBizOp : AbstractBusinessOperation<QueryCustomerByExternalIdRequestDTO,QueryCustomerByExternalIdResponseDTO,QueryCustomerByExternalIdRequestInternal,QueryCustomerByExternalIdResponseInternal>
    {
        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="dtoRequest">the dto request</param>
        /// <param name="coreInput">the core request</param>
        protected override void MapNotAutomappedInboundProperties(QueryCustomerByExternalIdRequestDTO dtoRequest, ref QueryCustomerByExternalIdRequestInternal coreInput)
        {
        }

        /// <summary>
        /// Map IEnumerable(CustomerInfo) to List of CustomerDTO
        /// </summary>
        /// <param name="coreOutput"></param>
        /// <param name="dtoOutput"></param>
        protected override void MapNotAutomappedOutboundProperties(QueryCustomerByExternalIdResponseInternal coreOutput, ref QueryCustomerByExternalIdResponseDTO dtoOutput)
        {
            var custInfos = coreOutput.CustomerInfos;
            //dtoOutput = new QueryCustomerByExternalIdResponseDTO();
            dtoOutput.CustomerDTOs = new List<CustomerDTO>();
            if (custInfos.IsNotNullOrEmpty())
            {
                foreach (var custInfo in custInfos)
                {
                    dtoOutput.CustomerDTOs.Add(custInfo.ToDto());
                }    
            }
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryCustomerByExternalIdOperation; }
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryCustomerByExternalIdOperation; }
        }

        /// <summary>
        /// Extremely simple implementation as is managed by the framework all entities used by this op
        /// </summary>
        /// <param name="request">the core request</param>
        /// <param name="runningOperation">The trace of the operation</param>
        /// <param name="invoker">the environment of the invokation</param>
        /// <returns>QueryCustomerByExternalIdResponseInternal</returns>
        protected override QueryCustomerByExternalIdResponseInternal ProcessBusinessLogic(QueryCustomerByExternalIdRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new QueryCustomerByExternalIdResponseInternal()
            {
                CustomerInfos = request.Customers,
                Customer = request.Customers.IsEmpty() ? null : request.Customers.FirstOrDefault(),
                Subscription = request.Customers.IsEmpty() ? null : request.Customers.FirstOrDefault().ResourceMBInfo.FirstOrDefault(x=>x.StatusID == (int)ResourceStatus.Active),
                ErrorCode = 0,
                Message = "Query success",
                ResultType = ResultTypes.Ok
            };
        }
    }
}
