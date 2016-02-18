using System;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.implementation;

namespace com.etak.core.bizops.fullfilment.QuerySubscriptionByMsisdn
{
    /// <summary>
    /// Implementation of the QuerySubscriptionByMsisdnOperation receives an MSISDN and returns a subscription
    /// </summary>
    public  class QuerySubscriptionByMsisdnOperation : AbstractBusinessOperation<QuerySubscriptionByMsisdnDTORequest, QuerySubscriptionByMsisdnDTOResponse, QuerySubscriptionByMsisdnRequestInternal, QuerySubscriptionByMsisdnResponseInternal>
    {
        /// <summary>
        /// This bizop does not have not automapped fields
        /// </summary>
        /// <param name="dtoRequest">The request in ET DTO form</param><param name="coreInput">The request in Internal form, prefilled with the common parameters</param>
        /// <returns>
        /// the operation in the et Internal form
        /// </returns>
        protected override void MapNotAutomappedInboundProperties(QuerySubscriptionByMsisdnDTORequest dtoRequest, ref QuerySubscriptionByMsisdnRequestInternal coreInput)
        {
           
        }

        /// <summary>
        /// This bizop does not have not automapped fields
        /// </summary>
        /// <param name="coreOutput">the result of process implementation</param>
        /// <param name="dtoOutput">the preallocated response, will all auto fields mapped</param>
        /// <returns>
        /// the response in the ET DTO form
        /// </returns>
        protected override void MapNotAutomappedOutboundProperties(QuerySubscriptionByMsisdnResponseInternal coreOutput, ref QuerySubscriptionByMsisdnDTOResponse dtoOutput)
        {
           
        }

        /// <summary>
        /// Operation code of the operation <see cref="OperationCodes.QuerySubscriptionByMSISDN"/> 
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QuerySubscriptionByMSISDN; }
        }

        /// <summary>
        /// Unique identifier of the operation <see cref="OperationDiscriminators.QuerySubscriptionByMSISDN"/> 
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QuerySubscriptionByMSISDN; }
        }

        /// <summary>
        /// Just returns the subscription received in the request
        /// </summary>
        /// <param name="request">Input parameter for the request</param>
        /// <param name="runningOperation">The trace for the ongoing operation</param>
        /// <param name="invoker">The information about the invokation of the operation</param>
        /// <returns>
        /// the subscription received in the request
        /// </returns>
        protected override QuerySubscriptionByMsisdnResponseInternal ProcessBusinessLogic(QuerySubscriptionByMsisdnRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new QuerySubscriptionByMsisdnResponseInternal
            {
                ErrorCode = BizOpsErrors.Ok,
                Message = String.Empty,
                ResultType = ResultTypes.Ok,
                Subscription = request.Subscription,
                Customer = request.Subscription.CustomerInfo
            };
        }
    }
}
