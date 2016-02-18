using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByDocumentId
{
    /// <summary>
    /// Operation to get customer information by document id
    /// </summary>
    public class QueryCustomerByDocumentIdBizOp : AbstractBusinessOperation<QueryCustomerByDocumentIdRequestDTO, QueryCustomerByDocumentIdResponseDTO, QueryCustomerByDocumentIdRequestInternal, QueryCustomerByDocumentIdResponseInternal>
    {
        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="dtoRequest">the dto request</param>
        /// <param name="coreInput">the core input</param>
        protected override void MapNotAutomappedInboundProperties(QueryCustomerByDocumentIdRequestDTO dtoRequest, ref QueryCustomerByDocumentIdRequestInternal coreInput)
        {
        }

        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="coreOutput">the core output</param>
        /// <param name="dtoOutput">the dto output</param>
        protected override void MapNotAutomappedOutboundProperties(QueryCustomerByDocumentIdResponseInternal coreOutput, ref QueryCustomerByDocumentIdResponseDTO dtoOutput)
        {
            if (coreOutput.Customers != null)
            {
                List<CustomerDTO> listDto = new List<CustomerDTO>();
                coreOutput.Customers.ForEach(x => listDto.Add(x.ToDto()));
                dtoOutput.Customers = listDto;
            }
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryCustomerByDocumentIdOperation; }
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryCustomerByDocumentIdOperation; }
        }

        /// <summary>
        /// Extremely simple implementation as is managed by the framework all entities used by this op
        /// </summary>
        /// <param name="request">the core request</param>
        /// <param name="runningOperation">The trace of the operation</param>
        /// <param name="invoker">the environment of the invokation</param>
        /// <returns>the core response</returns>
        protected override QueryCustomerByDocumentIdResponseInternal ProcessBusinessLogic(QueryCustomerByDocumentIdRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new QueryCustomerByDocumentIdResponseInternal()
            {
                Customers = request.Customers,
                Customer = request.Customers.IsEmpty() ? null: request.Customers.FirstOrDefault(),
                Subscription = request.Customers.IsEmpty() ? null : request.Customers.FirstOrDefault().ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active),
                ErrorCode = 0,
                Message = "Query success",
                ResultType = ResultTypes.Ok
            };
        }
    }
}
