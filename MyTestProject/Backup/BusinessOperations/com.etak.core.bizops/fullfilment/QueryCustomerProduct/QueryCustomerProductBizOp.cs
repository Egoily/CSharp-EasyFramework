using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerId;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using log4net;

namespace com.etak.core.bizops.fullfilment.QueryCustomerProduct
{
    /// <summary>
    /// Business Operation :  Get all ProductPurchaseDTO by specific Customer
    /// </summary>
    public class QueryCustomerProductBizOp : AbstractBusinessOperation<QueryCustomerProductRequestDTO, QueryCustomerProductResponseDTO, QueryCustomerProductRequestInternal, QueryCustomerProductResponseInternal>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///  Check if coreInput.Customer entitiy exists
        /// </summary>
        /// <param name="dtoRequest"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedInboundProperties(QueryCustomerProductRequestDTO dtoRequest,
            ref QueryCustomerProductRequestInternal coreInput)
        {
            if (coreInput.Customer == null || coreInput.Customer.CustomerID == null)
                throw new BusinessLogicErrorException(string.Format("Cannot get Customer.CustomerId."), BizOpsErrors.CustomerNotFound);
            
        }

        /// <summary>
        /// Convert CustomerProductAssignment entities to ProductPurchaseDTO
        /// </summary>
        /// <param name="coreOutput"></param>
        /// <param name="dtoOutput"></param>
        protected override void MapNotAutomappedOutboundProperties(QueryCustomerProductResponseInternal coreOutput,
            ref QueryCustomerProductResponseDTO dtoOutput)
        {
            if (coreOutput.CustomerProductAssignments != null)
            {
                IList<CustomerProductAssignmentDTO> productCatalog = coreOutput.CustomerProductAssignments.Select(x => x.ToDto()).ToList();
                dtoOutput.ProductPurchaseDto = productCatalog;
            }
 
            dtoOutput.resultType = coreOutput.ResultType;
        }

        /// <summary>
        ///  Get all CustomerProductAssignments by specific Customer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="runningOperation"></param>
        /// <param name="invoker"></param>
        /// <returns>QueryCustomerProductResponseInternal with enumerable of CustomerProductAssignment entities </returns>
        protected override QueryCustomerProductResponseInternal ProcessBusinessLogic(QueryCustomerProductRequestInternal request,
            BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            var getCustomerProductAssignmentsByCustomerIdMs = MicroServiceManager.GetMicroService<GetCustomerProductAssignmentsByCustomerIdRequest, GetCustomerProductAssignmentsByCustomerIdResponse>();
            
            var getCustomerProductAssignmentsByCustomerIdRequest = new GetCustomerProductAssignmentsByCustomerIdRequest();
            
            if (Log.IsDebugEnabled)
               Log.InfoFormat("Set GetCustomerProductAssignmentsByCustomerIdRequest with customerId ({0}) specified.", request.Customer.CustomerID.Value);  

            getCustomerProductAssignmentsByCustomerIdRequest.CustomerId = request.Customer.CustomerID.Value;
           
            var getCustomerProductAssignmentsByCustomerIdResponse = getCustomerProductAssignmentsByCustomerIdMs.Process(getCustomerProductAssignmentsByCustomerIdRequest, invoker);
            return new QueryCustomerProductResponseInternal()
            {
                CustomerProductAssignments = getCustomerProductAssignmentsByCustomerIdResponse.CustomerProductAssignments.ToList(),
                Subscription = request.Customer.ResourceMBInfo.FirstOrDefault(x=>x.StatusID == (int)ResourceStatus.Active)
            };
        }

        /// <summary>
        ///  QueryCustomerProductBizOp Operation code 
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryCustomerProductOperation; }
        }

        /// <summary>
        /// QueryCustomerProductBizOp Operation discriminator
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryCustomerProductOperation; }
        }
    }
}
