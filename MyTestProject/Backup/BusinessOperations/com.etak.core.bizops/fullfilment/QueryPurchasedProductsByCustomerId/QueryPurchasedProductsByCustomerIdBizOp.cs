using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerId;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.aaa;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using log4net;

namespace com.etak.core.bizops.fullfilment.QueryPurchasedProductsByCustomerId
{
    /// <summary>
    /// Query Purchased Product bases on given customer Id
    /// </summary>
    public class QueryPurchasedProductsByCustomerIdBizOp : AbstractBusinessOperation<QueryPurchasedProductsByCustomerIdRequestDTO, QueryPurchasedProductsByCustomerIdResponseDTO, QueryPurchasedProductsByCustomerIdRequestInternal, QueryPurchasedProductsByCustomerIdResponseInternal>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Operation code for Query Purchase Product Operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryPurchaseProductOperation; }
        }


        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="dtoRequest">the dto request</param>
        /// <param name="coreInput">the core request</param>
        protected override void MapNotAutomappedInboundProperties(QueryPurchasedProductsByCustomerIdRequestDTO dtoRequest, ref QueryPurchasedProductsByCustomerIdRequestInternal coreInput)
        {
            if (dtoRequest.StartDate > dtoRequest.EndDate)
            {
                throw new DataValidationErrorException("End date must be later than start date",BizOpsErrors.StartDateIsLaterThanEndDate);
            }

            if (coreInput.Customer == null)
                throw new BusinessLogicErrorException(String.Format("It doesn't exist information for the CustomerID:{0}",
                    dtoRequest.CustomerId), BizOpsErrors.CustomerNotFound);
            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest {UserInfo = coreInput.User, DealerId = coreInput.Customer.DealerID != null ? coreInput.Customer.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion
            #region Get PurchasedProduct using microservice GetCustomerProductAssignmentsByCustomerIdMS

            Log.InfoFormat("Calling microservice : GetCustomerProductAssignmentsByCustomerIdMS, get customer pruchased product based on customer Id : ({0}). ", dtoRequest.CustomerId);
            IMicroService<GetCustomerProductAssignmentsByCustomerIdRequest, GetCustomerProductAssignmentsByCustomerIdResponse> microServiceGetCustomerProductAssignmentsByCustomerId = MicroServiceManager.GetMicroService<GetCustomerProductAssignmentsByCustomerIdRequest, GetCustomerProductAssignmentsByCustomerIdResponse>();
            var getCustomerProductRequest = new GetCustomerProductAssignmentsByCustomerIdRequest()
            {
                CustomerId = dtoRequest.CustomerId

            };
            var getCustomerProductResponse =
                microServiceGetCustomerProductAssignmentsByCustomerId.Process(getCustomerProductRequest, null);

            List<CustomerProductAssignment> list = new List<CustomerProductAssignment>();
            if (getCustomerProductResponse.CustomerProductAssignments != null)
            {
                foreach (CustomerProductAssignment item in getCustomerProductResponse.CustomerProductAssignments)
                {

                    // Not show products whose promotion are set to not visible
                    if (!(item.PurchasedProduct.AssociatedPrmotionPlan != null && item.PurchasedProduct.AssociatedPrmotionPlan.APIVisible == APIVisible.Invisible)
                        && item.StartDate >= dtoRequest.StartDate && ((dtoRequest.EndDate != DateTime.MinValue) && (item.StartDate <= dtoRequest.EndDate) || dtoRequest.EndDate == DateTime.MinValue) && (!item.EndDate.HasValue || item.EndDate <= dtoRequest.EndDate || dtoRequest.EndDate == DateTime.MinValue))
                    {
                        list.Add(item);
                    }
                }
            }
            //add list of customer purchasedProduct to Internal request
            coreInput.CustomerProductAssignments = list;

            #endregion Get PurchasedProduct using microservice GetCustomerProductAssignmentsByCustomerIdMS
        }

        /// <summary>
        /// We don't need to map anything because all the properties are managed by the framework
        /// </summary>
        /// <param name="coreOutput">the core response</param>
        /// <param name="dtoOutput">the dto response</param>
        protected override void MapNotAutomappedOutboundProperties(QueryPurchasedProductsByCustomerIdResponseInternal coreOutput, ref QueryPurchasedProductsByCustomerIdResponseDTO dtoOutput)
        {

            IList<CustomerProductAssignmentDTO> purchaseProducts = new List<CustomerProductAssignmentDTO>();
            if (coreOutput.Products != null)
            {
                //CustomerProductAssignmentDTO
                purchaseProducts = coreOutput.Products.Select(x => x.ToDto()).ToList();

            }
            dtoOutput.ProductsPurchaseDto = purchaseProducts;
            dtoOutput.resultType = coreOutput.ResultType;
        }
        /// <summary>
        /// Operation Discrimator for Query Purchase Product
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryPurchaseProductOperation; }
        }

        /// <summary>
        /// Business Logic of Query Purchase Product Operation
        /// </summary>
        /// <param name="request">QueryPurchasedProductsByCustomerIdRequestInternal</param>
        /// <param name="runningOperation">BusinessOperationExecution</param>
        /// <param name="invoker">RequestInvokationEnvironment</param>
        /// <returns>QueryPurchasedProductsByCustomerIdResponseInternal</returns>
        protected override QueryPurchasedProductsByCustomerIdResponseInternal ProcessBusinessLogic(QueryPurchasedProductsByCustomerIdRequestInternal request, model.operation.BusinessOperationExecution runningOperation, operation.RequestInvokationEnvironment invoker)
        {
            return new QueryPurchasedProductsByCustomerIdResponseInternal()
            {
                Products = request.CustomerProductAssignments,
                Subscription = request.Customer.ResourceMBInfo.FirstOrDefault(x=>x.StatusID == (int)ResourceStatus.Active)
            };

        }
    }
}
