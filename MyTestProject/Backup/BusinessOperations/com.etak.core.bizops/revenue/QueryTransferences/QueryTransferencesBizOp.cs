using System;
using System.Linq;
using System.Collections.Generic;
using com.etak.core.GSMSubscription.messages.GetLastSubscriptionByMsisdn;
using com.etak.core.microservices.messages.GetSucessfulOperationExecutionForCustomer;
using com.etak.core.microservices.microservices;
using com.etak.core.model.operation;
using com.etak.core.operation.contract;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.dtoConverters;
using com.etak.core.customer.microservices;
using com.etak.core.operation.manager;
using com.etak.core.bizops.revenue.BenefitTransfer;
using com.etak.core.model;
using com.etak.core.GSMSubscription.messages.GetResourceMBInfosByMSISDNAndStatusNotInAndActiveDates;
using NHibernate.Mapping;

namespace com.etak.core.bizops.revenue.QueryTransferences
{
    /// <summary>
    /// Operation for QueryTransferences
    /// </summary>
    public class QueryTransferencesBizOp 
        : AbstractBusinessOperation<QueryTransferencesRequestDTO, QueryTransferencesResponseDTO, QueryTransferencesRequestInternal, QueryTransferencesResponseInternal>
    {       
        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="dtoRequest">the dto request</param>
        /// <param name="coreInput">the core request</param>
        protected override void MapNotAutomappedInboundProperties(QueryTransferencesRequestDTO dtoRequest, ref QueryTransferencesRequestInternal coreInput)
        {
            coreInput.StartDate = dtoRequest.StartDate;
            coreInput.EndDate = dtoRequest.EndDate;
            coreInput.MSISDN = dtoRequest.MSISDN;
            coreInput.OperationDefinition = dtoRequest.OperationDefinition;
        }

        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="coreOutput">the core response</param>
        /// <param name="dtoOutput">the dto response</param>
        protected override void MapNotAutomappedOutboundProperties(QueryTransferencesResponseInternal coreOutput, ref QueryTransferencesResponseDTO dtoOutput)
        {
            if(coreOutput.ResultType == ResultTypes.Ok)
            {              
                dtoOutput.Transferences = new List<TransferenceExecutionDTO>();
                foreach (BusinessOperationExecution operation in coreOutput.Operations)
                {
                    TransferenceExecutionDTO transfer = new TransferenceExecutionDTO();
                    transfer.Operation = operation.ToDto();
                    transfer.DonorMsisdn = operation.Subscription == null ? string.Empty : operation.Subscription.Resource;
                    transfer.ReceiverMsisdn = operation.SubscriptionDestination == null ? string.Empty : operation.SubscriptionDestination.Resource;

                    (dtoOutput.Transferences as List<TransferenceExecutionDTO>).Add(transfer);
                }
            }
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode 
        { 
            get { return OperationCodes.QueryTransferencesOperation; } 
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator 
        {
            get { return OperationDiscriminators.QueryTransferencesOperation; } 
        }
      
        /// <summary>
        /// Business logic for QueryTransferences
        /// </summary>
        /// <param name="request"></param>
        /// <param name="runningOperation"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        protected override QueryTransferencesResponseInternal ProcessBusinessLogic(QueryTransferencesRequestInternal request, model.operation.BusinessOperationExecution runningOperation, operation.RequestInvokationEnvironment invoker)
        {
            #region Gets customer information by msisdn
            
            //Get the customer from the last active subscription if the customer is not set
            if (request.Customer == null)
            {
                if (request.Subscription != null && request.Subscription.CustomerInfo != null)
                {
                    request.Customer = request.Subscription.CustomerInfo;
                }
                else //Get the last subscription as an active subscription hasn't been found
                {
                    var getLastSubscriptionMs = MicroServiceManager.GetMicroService<GetLastSubscriptionByMsisdnRequest, GetLastSubscriptionByMsisdnResponse>();

                    var getLastSubscriptionReq = new GetLastSubscriptionByMsisdnRequest()
                    {
                        MVNO = request.MVNO,
                        Msisdn = request.MSISDN,
                        Status = new List<int>(),
                    };

                    var getLastSubscriptionResponse = getLastSubscriptionMs.Process(getLastSubscriptionReq, invoker);

                    if (getLastSubscriptionResponse == null || getLastSubscriptionResponse.ResultType != ResultTypes.Ok || getLastSubscriptionResponse.ResourceMBInfo == null || !getLastSubscriptionResponse.ResourceMBInfo.Any())
                        throw new BusinessLogicErrorException(string.Format("Cannot get the last subscription with MSISDN {0}", request.MSISDN), BizOpsErrors.SubscriptionNotFound);

                    request.Customer = getLastSubscriptionResponse.ResourceMBInfo.FirstOrDefault().CustomerInfo;

                    if (request.Customer == null)
                        throw new BusinessLogicErrorException(string.Format("Cannot get the customer with MSISDN {0}", request.MSISDN), BizOpsErrors.CustomerNotFound);
                }
            }

            #endregion Gets customer information by msisdn
            
            #region Date validation

            if (request.StartDate == null || request.StartDate == System.DateTime.MinValue)
                throw new BusinessLogicErrorException("StartDate is not provided.", BizOpsErrors.StartDateIsNotProvided);

            if (request.EndDate == null || request.EndDate == System.DateTime.MinValue)
                throw new BusinessLogicErrorException("EndDate is not provided.", BizOpsErrors.EndDateIsNotProvided);
            
            #endregion Date validation

            IMicroService<GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse> microService = MicroServiceManager.GetMicroService<GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse>();
            
            GetSucessfulOperationExecutionForCustomerRequest getSucessfulOperationExecutionForCustomerRequest = new GetSucessfulOperationExecutionForCustomerRequest() 
            {
                Customer = request.Customer,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                OperationDefinition = request.OperationDefinition               
            };

            GetSucessfulOperationExecutionForCustomerResponse getSucessfulOperationExecutionForCustomerResponse = microService.Process(getSucessfulOperationExecutionForCustomerRequest, invoker);

            QueryTransferencesResponseInternal response = new QueryTransferencesResponseInternal()
            {
                Operations = getSucessfulOperationExecutionForCustomerResponse.Operations,
                ErrorCode = getSucessfulOperationExecutionForCustomerResponse.ErrorCode,
                Message = string.IsNullOrEmpty(getSucessfulOperationExecutionForCustomerResponse.Message) ? "OK" : getSucessfulOperationExecutionForCustomerResponse.Message,
                ResultType = getSucessfulOperationExecutionForCustomerResponse.ResultType
            };

            return response;
        } 

    }

}
