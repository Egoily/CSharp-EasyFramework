using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;

namespace com.etak.core.bizops.fullfilment.QuerySubscriptionByCustomerId
{
    /// <summary>
    /// Gets the GSM subscription of a customer
    /// </summary>
    public class QuerySubscriptionByCustomerIdBizOp
        : AbstractBusinessOperation<QuerySubscriptionByCustomerIdRequestDTO, QuerySubscriptionByCustomerIdResponseDTO,
                                    QuerySubscriptionByCustomerIdRequestInternal, QuerySubscriptionByCustomerIdResponseInternal>
    {
        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QuerySubscriptionByCustomerId; }
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QuerySubscriptionByCustomerId; }
        }

        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="dtoRequest">the dto request</param>
        /// <param name="coreInput">the core request</param>
        protected override void MapNotAutomappedInboundProperties(QuerySubscriptionByCustomerIdRequestDTO dtoRequest, ref QuerySubscriptionByCustomerIdRequestInternal coreInput)
        {
        }

        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="coreOutput">the core response</param>
        /// <param name="dtoOutput">the dto response</param>
        protected override void MapNotAutomappedOutboundProperties(QuerySubscriptionByCustomerIdResponseInternal coreOutput, ref QuerySubscriptionByCustomerIdResponseDTO dtoOutput)
        {
            
        }

        /// <summary>
        /// Gets the customer from the request and gets the first active subscription, fails if none or more than one
        /// </summary>
        /// <param name="request">the request in core form</param>
        /// <param name="runningOperation">The trace of the operation</param>
        /// <param name="invoker">the environemnt of the invokation</param>
        /// <returns>The Subscription found</returns>
        protected override QuerySubscriptionByCustomerIdResponseInternal ProcessBusinessLogic(QuerySubscriptionByCustomerIdRequestInternal request, BusinessOperationExecution runningOperation, operation.RequestInvokationEnvironment invoker)
        {
            CustomerInfo customer = request.Customer;
          
            if (customer == null)
                throw new BusinessLogicErrorException("Customer not found", BizOpsErrors.CustomerNotFound);

            if (!customer.ResourceMBInfo.Any())
                throw new BusinessLogicErrorException("Customer does not have any subscription", BizOpsErrors.CustomerWithoutSubscriptions);

            IList<ResourceMBInfo> subscriptionList = customer.ResourceMBInfo.Where(x => x.StartDate < DateTime.Now && (x.EndDate == null || x.EndDate > DateTime.Now)).ToList();
            if (!subscriptionList.Any())
                throw new BusinessLogicErrorException("Customer does not have any active subscription", BizOpsErrors.CustomerWithoutSubscriptions);

            if (subscriptionList.Count() > 1)
                throw new BusinessLogicErrorException("Customer with multiple active subscription", BizOpsErrors.CustomerMultipleSubscriptions);

            ResourceMBInfo subscription = subscriptionList.First();

            var response = new QuerySubscriptionByCustomerIdResponseInternal()
            {
                ErrorCode = 0,
                Message = "Successfull query",
                ResultType = ResultTypes.Ok,
                Subscription = subscription
            };

            return response;
        }
    }
}
