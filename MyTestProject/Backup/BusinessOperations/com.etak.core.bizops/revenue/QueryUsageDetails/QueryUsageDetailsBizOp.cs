using System;
using System.Collections.Generic;
using System.Security.Authentication;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.GSMSubscription.messages.GetUDRecordsByCustomerIDAndBetweenDates;
using com.etak.core.GSMSubscription.messages.GetUDRecordsByMSISDNAndBetweenDates;
using com.etak.core.model.operation;
using com.etak.core.model.usage;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.repository.crm;
using com.etak.core.model;

namespace com.etak.core.bizops.revenue.QueryUsageDetails
{
    /// <summary>
    /// Operation to query usage details
    /// </summary>
    public class QueryUsageDetailsBizOp : AbstractBusinessOperation<QueryUsageDetailsRequestDTO, QueryUsageDetailsResponseDTO, QueryUsageDetailsRequestInternal, QueryUsageDetailsResponseInternal>
    {
        /// <summary>
        /// Map not automapped inbound properties and validate that customerinfo and dealer id is not null
        /// </summary>
        /// <param name="dtoRequest">the dto request</param>
        /// <param name="coreInput">the core request</param>
        protected override void MapNotAutomappedInboundProperties(QueryUsageDetailsRequestDTO dtoRequest, ref QueryUsageDetailsRequestInternal coreInput)
        {
            #region Fill the core input

            coreInput.MSISDN = dtoRequest.MSISDN;
            coreInput.PeriodStartRange = dtoRequest.PeriodStartRange;
            coreInput.PeriodEndRange = dtoRequest.PeriodEndRange;
            coreInput.SubServiceTypeID = dtoRequest.SubServiceTypeID;
            coreInput.FilterRule = dtoRequest.FilterRule;
            #endregion

            
         
           


        }

        /// <summary>
        /// Map List of UsageDetailRecord to UsageDetailDTO
        /// </summary>
        /// <param name="coreOutput">the core Output</param>
        /// <param name="dtoOutput">the dto Output</param>
        protected override void MapNotAutomappedOutboundProperties(QueryUsageDetailsResponseInternal coreOutput, ref QueryUsageDetailsResponseDTO dtoOutput)
        {
            #region map list of UD Record to UD DTO
            dtoOutput.UsageDetails = new List<UsageDetailDTO>();
            if (coreOutput.UsageDetails != null)
            {
                foreach (var usageDetail in coreOutput.UsageDetails)
                {
                    dtoOutput.UsageDetails.Add(usageDetail.ToDto());
                }
            }
            #endregion
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryUsageDetailsOperation; }
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryUsageDetailsOperation; }
        }

        /// <summary>
        /// Dictionary to map Int32 to FilterCdrDates
        /// </summary>
        private readonly Dictionary<Int32, FilterCdrDates> _intToFilterCdrDates = new Dictionary<int, FilterCdrDates>()
        {
            {0, FilterCdrDates.StartDate},
            {1, FilterCdrDates.EndDate},
        };

        /// <summary>
        /// Query Usage Details by CustomerID or MSISDN using the request internal
        /// </summary>
        /// <param name="request">the core request</param>
        /// <param name="runningOperation">The trace of the operation</param>
        /// <param name="invoker">the environment of the invokation</param>
        /// <returns>QueryUsageDetailsResponseInternal with the usage detail records</returns>
        protected override QueryUsageDetailsResponseInternal ProcessBusinessLogic(QueryUsageDetailsRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            //if the msisdn in the request is null, use get by customerId
            if (string.IsNullOrEmpty(request.MSISDN))
            {
                //CustomerInfo has already been managed by the framework, so it is already in request.Customer
                #region Validate CustomerInfo
                if (request.Customer == null)
                {
                    throw new DataValidationErrorException(string.Format("Customer is not exist"), BizOpsErrors.CustomerNotFound);
                }
                if (request.Customer.DealerID == null)
                {
                    throw new DataValidationErrorException(string.Format("It doesn't exist dealer information for the CustomerID:{0}", request.Customer.CustomerID), BizOpsErrors.CustomerWithoutDealer);
                }
                #endregion

                #region checkAuthorization
                var microServiceCheckAuthorization =
                    MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
                var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = request.User, DealerId = request.Customer.DealerID != null ? request.Customer.DealerID.Value : 0 };
                var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
                if (!checkAuthorizationResponse.IsAuthorized)
                    throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);

                #endregion

                #region Validate StartDate and EndDate
                if (DateTime.Compare(request.PeriodStartRange, request.PeriodEndRange) > 0)
                {
                    throw new DataValidationErrorException(string.Format("periodStartRange must earlier than periodEndRange"), BizOpsErrors.StartDateIsLaterThanEndDate);
                }
                #endregion

                var getUDRecordsByCustomerIDAndBetweenDatesReq = new GetUDRecordsByCustomerIDAndBetweenDatesRequest()
                {
                    CustomerId = request.Customer.CustomerID.Value,
                    dateToFilter = _intToFilterCdrDates[request.FilterRule],
                    EndDate = request.PeriodEndRange,
                    StartDate = request.PeriodStartRange,
                    SubServiceTypeId = request.SubServiceTypeID,
                    VmoId = request.MVNO.DealerID.Value
                };
                var getUDRecordsByCustomerIDAndBetweenDatesMS =
                    MicroServiceManager
                        .GetMicroService
                        <GetUDRecordsByCustomerIDAndBetweenDatesRequest, GetUDRecordsByCustomerIDAndBetweenDatesResponse
                            >();
                var getUDRecordsByCustomerIDAndBetweenDatesRes =
                    getUDRecordsByCustomerIDAndBetweenDatesMS.Process(getUDRecordsByCustomerIDAndBetweenDatesReq, null);
                return new QueryUsageDetailsResponseInternal()
                {
                    UsageDetails = getUDRecordsByCustomerIDAndBetweenDatesRes.UsageDetailRecords,
                    ErrorCode = 0,
                    ResultType = getUDRecordsByCustomerIDAndBetweenDatesRes.ResultType,
                    Message = "Query success"

                };
            }
            else
            {
                #region request subscription in null

                if (request.Subscription == null)
                    throw new BusinessLogicErrorException(string.Format("MSISDN {0} doesn't have subscription", request.MSISDN), BizOpsErrors.SubscriptionNotFound);

                if (request.Subscription.CustomerInfo == null)
                    throw new BusinessLogicErrorException(string.Format("MSISDN {0} have subscription without customer!", request.MSISDN), BizOpsErrors.SubcriptionWithoutCustomer);

                #endregion

                #region checkAuthorization
                var microServiceCheckAuthorization =
                    MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
                var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = request.User, DealerId = request.Subscription.CustomerInfo.DealerID != null ? request.Subscription.CustomerInfo.DealerID.Value : 0 };
                var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
                if (!checkAuthorizationResponse.IsAuthorized)
                    throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
                #endregion

                #region Validate StartDate and EndDate
                if (DateTime.Compare(request.PeriodStartRange, request.PeriodEndRange) > 0)
                {
                    throw new DataValidationErrorException(string.Format("periodStartRange must earlier than periodEndRange"), BizOpsErrors.StartDateIsLaterThanEndDate);
                }
                #endregion

                var getUDRecordsByMSISDNAndBetweenDatesReq = new GetUDRecordsByMSISDNAndBetweenDatesRequest()
                {
                    MSISDN = request.MSISDN,
                    dateToFilter = _intToFilterCdrDates[request.FilterRule],
                    EndDate = request.PeriodEndRange,
                    StartDate = request.PeriodStartRange,
                    SubServiceTypeId = request.SubServiceTypeID,
                    VmoId = request.MVNO.DealerID.Value
                };
                var getUDRecordsByMSISDNAndBetweenDatesMS =
                    MicroServiceManager
                        .GetMicroService
                        <GetUDRecordsByMSISDNAndBetweenDatesRequest, GetUDRecordsByMSISDNAndBetweenDatesResponse>();
                var getUDRecordsByMSISDNAndBetweenDatesRes =
                    getUDRecordsByMSISDNAndBetweenDatesMS.Process(getUDRecordsByMSISDNAndBetweenDatesReq, null);
                return new QueryUsageDetailsResponseInternal()
                {
                    UsageDetails = getUDRecordsByMSISDNAndBetweenDatesRes.UsageDetailRecords,
                    ErrorCode = 0,
                    ResultType = getUDRecordsByMSISDNAndBetweenDatesRes.ResultType,
                    Message = "Query success"
                };
            }
        }
    }
}
