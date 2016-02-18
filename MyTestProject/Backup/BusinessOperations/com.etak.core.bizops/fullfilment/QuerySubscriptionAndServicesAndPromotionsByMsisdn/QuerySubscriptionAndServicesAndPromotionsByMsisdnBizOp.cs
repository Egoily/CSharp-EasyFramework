using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;

namespace com.etak.core.bizops.fullfilment.QuerySubscriptionAndServicesAndPromotionsByMsisdn
{
    /// <summary>
    /// Implementation of the QuerySubscriptionAndServicesAndPromotionsByMsisdn receives an MSISDN and returns a subscription
    /// </summary>
    public class QuerySubscriptionAndServicesAndPromotionsByMsisdnBizOp : AbstractBusinessOperation<QuerySubscriptionAndServicesAndPromotionsByMsisdnDTORequest, QuerySubscriptionAndServicesAndPromotionsByMsisdnDTOResponse, QuerySubscriptionAndServicesAndPromotionsByMsisdnRequestInternal, QuerySubscriptionAndServicesAndPromotionsByMsisdnResponseInternal>
    {
        /// <summary>
        /// This method converts the DTORequest to the Request internal
        /// </summary>
        /// <param name="dtoRequest">QuerySubscriptionAndServicesAndPromotionsByMsisdnDTORequest</param><param name="coreInput">The request in Internal form</param>
        /// <returns>
        /// the operation in the et Internal form
        /// </returns>
        protected override void MapNotAutomappedInboundProperties(QuerySubscriptionAndServicesAndPromotionsByMsisdnDTORequest dtoRequest, ref QuerySubscriptionAndServicesAndPromotionsByMsisdnRequestInternal coreInput)
        {

            var subscription = coreInput.Subscription;
            if(subscription == null)
                throw new BusinessLogicErrorException("This MSISDN does not have any subscription", BizOpsErrors.CustomerWithoutSubscriptions);
			var customer = coreInput.Subscription.CustomerInfo;
			if (customer == null)
                throw new BusinessLogicErrorException("Customer not found", BizOpsErrors.CustomerNotFound);
            if (!customer.RevenueProductsInfo.Any())
                throw new BusinessLogicErrorException("Customer does not have any products assigned", BizOpsErrors.CustomerProductsNotFound);
            if (!customer.ServicesInfo.Any())
                throw new BusinessLogicErrorException("Customer does not have any services assigned", BizOpsErrors.ServicesInfoIsNull);
            if (!customer.Promotions.Any())
                throw new BusinessLogicErrorException("Customer does not have any promotions assigned", BizOpsErrors.CustomerPromotionNotFound);
            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Subscription.OperatorInfo.DealerID != null ? coreInput.Subscription.OperatorInfo.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion
        }

        /// <summary>
        /// Coverts the response/result of the process (in internal form) to the ET DTO form.
        /// </summary>
        /// <param name="coreOutput">the result of process implementation</param><param name="dtoOutput">the preallocated response, will all auto fields mapped</param>
        /// <returns>
        /// the response in the ET DTO form
        /// </returns>
        protected override void MapNotAutomappedOutboundProperties(QuerySubscriptionAndServicesAndPromotionsByMsisdnResponseInternal coreOutput, ref QuerySubscriptionAndServicesAndPromotionsByMsisdnDTOResponse dtoOutput)
        {
            if (coreOutput.Promotions != null)
            {
                #region Patch while ticket CFP-49 is done
                //Get the products assigned to a customer
                var customerProductList = coreOutput.Subscription.CustomerInfo.RevenueProductsInfo;
                var activeCustomerProductList = new List<Product>();
                // Get the list of active products of the customer
                foreach (var productassignment in customerProductList)
                {
                    if (productassignment.PurchasedProduct.Status == ProductStatuses.Current)
                        activeCustomerProductList.Add(productassignment.PurchasedProduct);
                }
                // Create the promotionDto items
                dtoOutput.CustomerPromotions = new List<CustomerPromotionDTO>();
                foreach (var promo in coreOutput.Promotions)
                {
                    var promotion = new CustomerPromotionDTO
                    {
                        CurrentLimit = promo.CurrentLimit,
                        Name = promo.PromotionDetail.RmPromotionPlanInfo.PromotionPlanName,
                        ProductId = 0,
                        PromotionPlanId = promo.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId,
                        Active = promo.Active
                    };
                    if (promo.Active)
                        dtoOutput.CustomerPromotions.Add(promotion);
                }
                //Create promotions list copy
                var promotionsCopy = dtoOutput.CustomerPromotions.Where(x => x.Active).Select(promo => promo.PromotionPlanId).ToList();
                //Associate every promotionDto item with its productid
                foreach (var product in activeCustomerProductList)
                {
                    if (product.AssociatedPrmotionPlan != null)
                    {
                        if (promotionsCopy.Contains(product.AssociatedPrmotionPlan.PromotionPlanId) && product.AssociatedPrmotionPlan.APIVisible != 0)
                        {
                            var promotion = dtoOutput.CustomerPromotions.First(
                                x =>
                                    x.PromotionPlanId == product.AssociatedPrmotionPlan.PromotionPlanId &&
                                    x.ProductId == 0);
                            if (promotion != null)
                            {
                                promotion.ProductId = product.Id;
                                promotionsCopy.Remove(promotion.PromotionPlanId);
                            }
                        }
                    }
                }
                #endregion Patch while ticket CFP-49 is done
            }
            if (coreOutput.Services != null)
            {
                dtoOutput.CustomerServices = new List<ServicesInfoDTO>();
                foreach (var service in coreOutput.Services)
                {
                    var serv = service.ToDto();
                    dtoOutput.CustomerServices.Add(serv);
                }
            }
        }

        /// <summary>
        /// OperationCode
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QuerySubscriptionAndServicesAndPromotionsByMsisdn; }
        }

        /// <summary>
        /// Unique Id of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QuerySubscriptionAndServicesAndPromotionsByMsisdn; }
        }

        /// <summary>
        /// Method implementing the business logic
        /// </summary>
        /// <param name="request">Input parameter for the request</param><param name="runningOperation">The trace for the ongoing operation</param><param name="invoker">The information about the invokation of the operation</param>
        /// <returns>
        /// The response of processing the request
        /// </returns>
        protected override QuerySubscriptionAndServicesAndPromotionsByMsisdnResponseInternal ProcessBusinessLogic(QuerySubscriptionAndServicesAndPromotionsByMsisdnRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            var customer = request.Subscription.CustomerInfo;

            return new QuerySubscriptionAndServicesAndPromotionsByMsisdnResponseInternal
            {
                ErrorCode = BizOpsErrors.Ok,
                Message = String.Empty,
                ResultType = ResultTypes.Ok,
                Subscription = request.Subscription,
                Customer = request.Subscription.CustomerInfo,
                Promotions = customer.Promotions.ToList(),
                Services = customer.ServicesInfo.ToList()
            };
        }
    }
}

