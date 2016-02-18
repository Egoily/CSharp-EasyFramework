using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using com.etak.core.customer.message.GetCustomerChargesBetweenDatesAndCustomerId;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.dealer.messages.GetDealerInfoById;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using log4net;

namespace com.etak.core.bizops.revenue.QueryCustomerRecurringCharges
{
    /// <summary>
    /// get the customer charge data with query
    /// </summary>
    public class QueryCustomerRecurringChargesBizOp : AbstractBusinessOperation<QueryCustomerRecurringChargesRequestDTO, QueryCustomerRecurringChargesResponseDTO, QueryCustomerRecurringChargesRequestInternal, QueryCustomerRecurringChargesResponseInternal>
    {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// This method operates in ET model. Converts the DTORequest to the Request types
        /// </summary>
        /// <param name="dtoRequest">The request in ET DTO form</param>
        /// <param name="coreInput">The request in Internal form, prefilled with the common parameters</param>
        /// <returns> the operation in the et Internal form</returns>
        protected override void MapNotAutomappedInboundProperties(QueryCustomerRecurringChargesRequestDTO dtoRequest, ref QueryCustomerRecurringChargesRequestInternal coreInput)
        {
            //data validation for customer, startdate, enddate, dealerid and dealerinfo
            if (coreInput.Customer == null)
            {
                throw new DataValidationErrorException(string.Format("It doesn't exist information for the CustomerID:{0}", dtoRequest.CustomerId), BizOpsErrors.ErrorBase);
            }
            if (coreInput.Customer.DealerID == null)
            {
                throw new DataValidationErrorException(string.Format("It doesn't exist dealer information for the CustomerID:{0}", coreInput.Customer.CustomerID), BizOpsErrors.ErrorBase);
            }

            #region Validate StartDate and EndDate
            if (DateTime.Compare(dtoRequest.StartDate, dtoRequest.EndDate) > 0)
            {
                throw new DataValidationErrorException(string.Format("startDate must earlier than endDate"), BizOpsErrors.StartDateIsLaterThanEndDate);
            }
            #endregion

            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Customer.DealerID.Value };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion
            
            //Call Microservice GetDealerInfoByIdMS
            Log.InfoFormat("Calling GetDealerInfoByIdMS to get the DealerNumberInfo of the DealerId ({0}) specified.", coreInput.Customer.DealerID);
            var getDealerInfoByIdMS = MicroServiceManager.GetMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var GetDealerInfoByIdMSReq = new GetDealerInfoByIdRequest()
            {
                DealerId = coreInput.Customer.DealerID.Value
            };

            var responses = getDealerInfoByIdMS.Process(GetDealerInfoByIdMSReq, null);
            coreInput.DealerInfo = responses.DealerInfo;

            if (coreInput.DealerInfo == null)
            {
                throw new DataValidationErrorException(string.Format("The requested customer ({0}), doesn't have Dealer information.", coreInput.Customer.CustomerID), BizOpsErrors.ErrorBase);
            }

            //call microservice getCustomerChargesBetweenDatesAndCustomerIdMS
            Log.InfoFormat("Calling getCustomerChargesBetweenDatesAndCustomerIdMS to get the CustomerChargeInfo with customer id ({0}) specified.", dtoRequest.CustomerId);
            var getCustomerChargesBetweenDatesAndCustomerIdMS = MicroServiceManager.GetMicroService<GetCustomerChargesBetweenDatesAndCustomerIdRequest, GetCustomerChargesBetweenDatesAndCustomerIdResponse>();
            var getCustomerChargesBetweenDatesAndCustomerIdMSReq = new GetCustomerChargesBetweenDatesAndCustomerIdRequest()
            {
                CustomerId = dtoRequest.CustomerId,
                StartDate = dtoRequest.StartDate,
                EndDate = dtoRequest.EndDate
            };
            var responsegetCustomerCharges =
                getCustomerChargesBetweenDatesAndCustomerIdMS.Process(getCustomerChargesBetweenDatesAndCustomerIdMSReq,
                    null);
            coreInput.CustomerChargeInfo = responsegetCustomerCharges.CustomerCharges.Where(x => x.Schedule != null).ToList();
        }
        /// <summary>
        /// Send Response Internal to DTO
        /// </summary>
        /// <param name="coreOutput"></param>
        /// <param name="dtoOutput"></param>
        protected override void MapNotAutomappedOutboundProperties(QueryCustomerRecurringChargesResponseInternal coreOutput, ref QueryCustomerRecurringChargesResponseDTO dtoOutput)
        {
            if (coreOutput.RecurringCharges != null)
            {
                IList<CustomerRecurringChargeDTO> CustomerChargeDTOCatalog = new List<CustomerRecurringChargeDTO>();
                foreach (var customerCharges in coreOutput.RecurringCharges)
                {
                    CustomerChargeDTOCatalog.Add(customerCharges.ToRecurringChargeDto());
                }
                dtoOutput.RecurringCharges = CustomerChargeDTOCatalog;
            }
            else
            {
                dtoOutput.RecurringCharges = new List<CustomerRecurringChargeDTO>();
            }



        }

        /// <summary>
        /// Operation code of QueryCustomerRecurringCharges
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryCustomerRecurringChargesOperation; }
        }
        /// <summary>
        /// Operation Discriminator of QueryCustomerRecurringCharges
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryCustomerRecurringChargesOperation; }
        }
        /// <summary>
        /// Process SwapSimCard
        /// </summary>
        /// <param name="requestInternal"></param>
        /// <param name="runningOperation"></param>
        /// <param name="invoker"></param>
        /// <returns>RecurringCharges</returns>
        protected override QueryCustomerRecurringChargesResponseInternal ProcessBusinessLogic(QueryCustomerRecurringChargesRequestInternal requestInternal, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            
            //return customercharge info
            var response = new QueryCustomerRecurringChargesResponseInternal()
            {
                
                RecurringCharges = requestInternal.CustomerChargeInfo.ToList(),
                Subscription = requestInternal.Customer.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active),
                ResultType = ResultTypes.Ok

            };
            return response;
        }
    }
}
