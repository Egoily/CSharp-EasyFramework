using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.PurchaseProductForCustomer;
using com.etak.core.bizops.helper;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.customer.message.GetCustomersActivePromotionInfo;
using com.etak.core.microservices.messages.GetSucessfulOperationExecutionForCustomer;
using com.etak.core.microservices.messages.GetTaxAuthority;
using com.etak.core.microservices.microservices;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetCurrentBillRunForBillCycle;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.product.message.GetProductChargeOptionByProductChargeOptionId;
using com.etak.core.product.message.GetProductChargeOptionsByProductId;
using com.etak.core.product.message.GetProductOfferingByProductOfferingId;
using com.etak.core.repository;
using com.etak.core.repository.crm.promotion;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.customer.message.SubtractBalance;
using com.etak.core.operation.dtoConverters;
using com.etak.core.promotion.microservices;
using com.etak.core.promotion.messages.UpdateCustomersPromotion;
using com.etak.core.GSMSubscription.messages.CheckProductListDependencyRelationsForCustomer;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerId;
using com.etak.core.customer.message.AddCrmCustomersBalanceTransationHistory;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using System.Net;
using log4net;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using com.etak.core.bizops.revenue.CallDREQuerySubscriberPromotion;
using com.etak.core.bizops.revenue.CallDREUpdateSubscriberPromotion;





namespace com.etak.core.bizops.revenue.BenefitTransfer
{
    /// <summary>
    /// BenefitTransferBizOp
    /// </summary>
    public class BenefitTransferBizOp : AbstractSinglePhaseOrderProcessor<BenefitTransferRequestDTO, BenefitTransferResponseDTO, BenefitTransferRequestInternal, BenefitTransferResponseInternal, BenefitTransferOrder>
    {

        #region BizOp Implemetation
        /// <summary>
        /// OperationCode
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.BenefitTransferOperation; }
        }

        /// <summary>
        /// OperationDiscriminator
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.BenefitTransferOperation; }
        }


        /// <summary>
        /// MapNotAutomappedOrderInboundProperties
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedOrderInboundProperties(BenefitTransferRequestDTO request, ref BenefitTransferRequestInternal coreInput)
        {


        }

        /// <summary>
        /// MapNotAutomappedOrderOutboundProperties
        /// </summary>
        /// <param name="source"></param>
        /// <param name="coreOutput"></param>
        protected override void MapNotAutomappedOrderOutboundProperties(BenefitTransferResponseInternal source, ref BenefitTransferResponseDTO coreOutput)
        {
            //TODO:is this right？
            //coreOutput.orderCode = source.CreatedOrder.Id;
            coreOutput.PurchasedPromotionInfo = source.PurchasedPromotionInfo.ToDto();

            //set SourceProductCustomerAssociation
            List<CustomerProductAssignmentDTO> sourceProductCustomerAssociation = new List<CustomerProductAssignmentDTO>();
            if (source.SourceProductCustomerAssociation != null)
            {
                foreach (var item in source.SourceProductCustomerAssociation)
                {
                    sourceProductCustomerAssociation.Add(item.ToDto());
                }
            }
            coreOutput.SourceProductCustomerAssociation = sourceProductCustomerAssociation;

            //set DestinationProductCustomerAssociation
            List<CustomerProductAssignmentDTO> destinationProductCustomerAssociation = new List<CustomerProductAssignmentDTO>();
            if (source.DestinationProductCustomerAssociation != null)
            {
                foreach (var item in source.DestinationProductCustomerAssociation)
                {
                    destinationProductCustomerAssociation.Add(item.ToDto());
                }
            }
            coreOutput.DestinationProductCustomerAssociation = destinationProductCustomerAssociation;

            //set TransferredPormotions
            Dictionary<CrmCustomersPromotionInfoDTO, decimal> transferredList = new Dictionary<CrmCustomersPromotionInfoDTO, decimal>();

            if (source.TransferredPormotions != null)
            {
                foreach (KeyValuePair<CrmCustomersPromotionInfo, decimal> item in source.TransferredPormotions)
                {
                    var transferredDTO = item.Key.ToDto();
                    transferredList.Add(transferredDTO, item.Value);
                }
            }
            coreOutput.TransferredPormotions = transferredList;
        }


        /// <summary>
        /// ProcessRequest
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override BenefitTransferResponseInternal ProcessRequest(BenefitTransferOrder order, BenefitTransferRequestInternal request)
        {
            #region check customerInfo

            if (request.SourceCustomerInfo == null)
            {
                throw new BusinessLogicErrorException("Can't process a Benefit transfer if source customer for the transference is not provided", BizOpsErrors.BenefitTransferSourceCustomerNull);
            }

            if (request.DestinationCustomerInfo == null)
            {
                throw new BusinessLogicErrorException("Can't process a Benefit transfer if destination customer for the transference is not provided", BizOpsErrors.BenefitTransferDestinationCustomerNull);
            }

            #endregion

            #region get configuration of this operation
            BenefitTransferConfiguration config = GetOperationConfigForDealer<BenefitTransferConfiguration>(request.MVNO);
            #endregion

            #region check send permission
            bool hasSendPermission = CheckPermission(request.SourceCustomerInfo, config.BenefitSourceTransferProductId);
            if (!hasSendPermission)
            {
                throw new BusinessLogicErrorException("Donor_MSISDN doesn't have send permission", BizOpsErrors.BenefitTransferNoSendPermission);
            }

            #endregion

            #region check receive permission

            bool hasReceivePermission = CheckPermission(request.DestinationCustomerInfo, config.BenefitDestinationTransferProductId);
            if (!hasReceivePermission)
            {
                throw new BusinessLogicErrorException("Receiver_MSISDN doesn't have receive permission", BizOpsErrors.BenefitTransferNoReceivePermission);
            }

            #endregion

            #region Check BenefitTransferSenderLimit
            var operationsSource = GetOperationsBycutomer(request.SourceCustomerInfo);
            var hasDonatedAmont = operationsSource.Where(x => x.Customer.CustomerID == request.SourceCustomerInfo.CustomerID).Sum(x => x.Amount);
            if (hasDonatedAmont + request.Amount > config.BenefitTransferSenderLimit)
            {
                throw new BusinessLogicErrorException("Donor_MSISDN has reached the Sender Limit in current bill cycle", BizOpsErrors.BenefitTransferSenderLimit);
            }
            #endregion

            #region check MaxTransferDestinationLimit
            int currentDay = DateTime.Now.Day;
            DateTime startDate = GetStartDate(currentDay);
            DateTime endDate = GetEndtDate(currentDay);

            GetSucessfulOperationExecutionForCustomerRequest getOperationRequest = new GetSucessfulOperationExecutionForCustomerRequest
            {
                Customer = request.SourceCustomerInfo,
                StartDate = startDate,
                EndDate = endDate,
                OperationDefinition = this,
            };
            var operationsInMonth = GetOperations(getOperationRequest);
            var monthNumbers = operationsInMonth.Where(x => x.Customer.CustomerID == request.SourceCustomerInfo.CustomerID).GroupBy(x => x.CustomerDestination.CustomerID).Count();
            var destinationNumbers = operationsInMonth.Where(x => x.Customer.CustomerID == request.SourceCustomerInfo.CustomerID && x.CustomerDestination.CustomerID == request.DestinationCustomerInfo.CustomerID).Count();
            if (destinationNumbers == 0)
            {
                monthNumbers += 1;
            }
            if (monthNumbers > config.MaxTransferDestinationLimit)
            {
                throw new BusinessLogicErrorException("Donor_MSISDN has reached the maximum number of different MSISDNs transferred in current month", BizOpsErrors.MaxTransferDestinationLimit);
            }
            #endregion

            #region Check BenefitTransferReceiverLimit

            var operationsReceiver = GetOperationsBycutomer(request.DestinationCustomerInfo);
            var hasReceivedAmount = operationsReceiver.Where(x => x.CustomerDestination.CustomerID == request.DestinationCustomerInfo.CustomerID).Sum(x => x.Amount);
            if (hasReceivedAmount + request.Amount > config.BenefitTransferReceiverLimit)
            {
                throw new BusinessLogicErrorException("Receiver_MSISDN exceeds receiver limit in current bill cycle", BizOpsErrors.BenefitTransferReceiverLimit);
            }
            #endregion

            #region check TotalBenefitLimit

            var destinationCustomerPromotions = GetCustomerPromotion(request.DestinationCustomerInfo);

            //get limit
            decimal acumulateLimit = 0;
            foreach (var promotionInfo in destinationCustomerPromotions)
            {
                if (promotionInfo.PromotionDetail.RmPromotionPlanInfo.Accumulative && promotionInfo.PromotionDetail.SubServiceTypeId == request.TransferType)
                {
                    acumulateLimit += promotionInfo.CurrentLimit;
                }
            }

            decimal transferedPromotionLimit = GetTransferedLimit(request.DestinationCustomerInfo, destinationCustomerPromotions, config.BenefitDestinationTransferProductId);

            if ((acumulateLimit + transferedPromotionLimit + request.Amount) > config.TotalBenefitLimit)
            {
                throw new BusinessLogicErrorException("Receiver_MSISDN has reached the Total Amount to transfer in current bill cycle", BizOpsErrors.TotalBenefitLimit);
            }


            #endregion

            #region Check customer balance
            //DRE service API url
            string serverURL = ConfigurationManager.AppSettings["DREServiceAddress"];
            var callDREQuerySubscriberPromotion = BusinessOperationManager.GetCoreBusinessOperation<CallDREQuerySubscriberPromotionRequestInternal, CallDREQuerySubscriberPromotionResponseInternal>((int)request.MVNO.DealerID);

            //get customer msisdn
            string sourceMSISDN = string.Empty;
            var sourceCustomerResource = request.SourceCustomerInfo.ResourceMBInfo.Where(x => x.StartDate <= DateTime.Now && (!x.EndDate.HasValue || x.EndDate >= DateTime.Now) && x.StatusID == (int)ResourceStatus.Active).FirstOrDefault();
            if (sourceCustomerResource != null)
            {
                sourceMSISDN = sourceCustomerResource.Resource;
            }

            var sourceCustomerPormotions = GetCustomerPromotion(request.SourceCustomerInfo);
            decimal customerLimit = 0;
            foreach (var promotion in sourceCustomerPormotions)
            {
                foreach (var id in config.ValidSourcePromotions)
                {
                    if (promotion.PromotionDetail.PromotionPlanDetailId == id && promotion.PromotionDetail.SubServiceTypeId == request.TransferType)
                    {
                        CallDREQuerySubscriberPromotionRequestInternal callDREQueryRequest = new CallDREQuerySubscriberPromotionRequestInternal()
                        {
                            msisdn = sourceMSISDN,
                            promotionId = promotion.PromotionId.ToString(),
                            serverURL = serverURL,
                            millionSecond = int.MaxValue,
                        };

                        //invoke DRE QuerySubscriberPromotion API
                        CallDREQuerySubscriberPromotionResponseInternal dreResponse = callDREQuerySubscriberPromotion.Process(callDREQueryRequest, null);
                        if (dreResponse.errorCode == 0)
                        {
                            customerLimit += dreResponse.currentLimit - dreResponse.frozenLimit;
                        }
                        else
                        {
                            customerLimit += promotion.CurrentLimit;

                        }
                        break;
                    }
                }
            }

            if (customerLimit < request.Amount)
            {
                throw new BusinessLogicErrorException("Donor_MSISDN doesn't have enough balance", BizOpsErrors.BenefitTransferNotEnoughBalance);
            }

            #endregion

            #region check prodcut by configuraion

            //check source product
            CheckProductIsExistById(config.BenefitSourceTransferProductId);

            //check destination product
            CheckProductIsExistById(config.BenefitDestinationTransferProductId);

            #endregion

            #region invoke PurchaseProductForCustomerBizOp

            var op = BusinessOperationManager.GetCoreBusinessOperation<PurchaseProductForCustomerRequestInternal, PurchaseProductForCustomerResponseInternal>((int)request.MVNO.DealerID);
            DateTime purchaseDate = DateTime.Now;

            #endregion

            #region purchase product for source customer

            PurchaseProductForCustomerRequestInternal sourcePurchaseProductRequest = GetPurchaseProductRequest(request.SourceCustomerInfo, config.BenefitSourceTransferProductId, request.Amount, purchaseDate);
            sourcePurchaseProductRequest.MVNO = request.MVNO;
            sourcePurchaseProductRequest.User = request.User;
            sourcePurchaseProductRequest.Channel = request.Channel;

            PurchaseProductForCustomerResponseInternal sourceProductPurchaseResponse = new PurchaseProductForCustomerResponseInternal();
            sourceProductPurchaseResponse = op.Process(sourcePurchaseProductRequest, null);

            #endregion

            #region purchase product for destination customer

            PurchaseProductForCustomerRequestInternal destinationPurchaseProductRequest = GetPurchaseProductRequest(request.DestinationCustomerInfo, config.BenefitDestinationTransferProductId, request.Amount, purchaseDate);
            destinationPurchaseProductRequest.MVNO = request.MVNO;
            destinationPurchaseProductRequest.User = request.User;
            destinationPurchaseProductRequest.Channel = request.Channel;

            PurchaseProductForCustomerResponseInternal destinationProductPurchaseResponse = new PurchaseProductForCustomerResponseInternal();
            destinationProductPurchaseResponse = op.Process(destinationPurchaseProductRequest, null);

            #endregion

            #region write customer balance transation history for destination customer

            AddBalanceTransationHistory(order, request, config.BenefitDestinationTransferProductId);

            #endregion

            #region update purchased customer promotion

            var destinationCustomerPromotionInfo = GetCustomerPromotion(request.DestinationCustomerInfo);
            var promotionDetailId = destinationProductPurchaseResponse.productPurchaseList.Where(x => x.PurchasedProduct.Id == config.BenefitDestinationTransferProductId).FirstOrDefault().PurchasedProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList.FirstOrDefault().PromotionPlanDetailId;
            var purchasedCustomerPromotion = destinationCustomerPromotionInfo.Where(x => x.PromotionDetail.PromotionPlanDetailId == promotionDetailId).OrderByDescending(x => x.StartDate).FirstOrDefault();

            purchasedCustomerPromotion.CurrentLimit = request.Amount;
            purchasedCustomerPromotion.InitialLimit = request.Amount;

            var updateCustomerPromotion = MicroServiceManager.GetMicroService<UpdateCustomersPromotionRequest, UpdateCustomersPromotionResponse>();
            UpdateCustomersPromotionRequest updateCustomerPromotionRequest = new UpdateCustomersPromotionRequest();
            updateCustomerPromotionRequest.CrmCustomersPromotionInfo = purchasedCustomerPromotion;
            updateCustomerPromotion.Process(updateCustomerPromotionRequest, null);


            #endregion

            #region move the balances from the source Promotions

            decimal transferredAmount = request.Amount;
            var subtractBalance = MicroServiceManager.GetMicroService<SubtractBalanceRequest, SubtractBalanceResponse>();


            Dictionary<CrmCustomersPromotionInfo, decimal> transferPormtions = new Dictionary<CrmCustomersPromotionInfo, decimal>();
            decimal subtractAmount = 0;

            var callDREUpdateSubscriberPromotion = BusinessOperationManager.GetCoreBusinessOperation<CallDREUpdateSubscriberPromotionRequestInternal, CallDREUpdateSubscriberPromotionResponseInternal>((int)request.MVNO.DealerID);

            foreach (var item in config.ValidSourcePromotions)
            {
                var customerPromos = sourceCustomerPormotions.Where(x => x.PromotionDetail.PromotionPlanDetailId == item && x.PromotionDetail.SubServiceTypeId == request.TransferType);
                foreach (var promotion in customerPromos)
                {
                    if (promotion.CurrentLimit > 0)
                    {
                        if (transferredAmount != 0)
                        {
                            if (promotion.CurrentLimit >= transferredAmount)
                            {
                                subtractAmount = transferredAmount;
                                transferredAmount = 0;
                            }
                            else
                            {
                                if (promotion.CurrentLimit > 0)
                                {
                                    transferredAmount = transferredAmount - promotion.CurrentLimit;
                                    subtractAmount = promotion.CurrentLimit;
                                }
                            }

                            transferPormtions.Add(promotion, subtractAmount);

                            CallDREUpdateSubscriberPromotionRequestInternal callDREUpdateRequest = new CallDREUpdateSubscriberPromotionRequestInternal()
                            {
                                ServerURL = serverURL,
                                Msisdn = sourceMSISDN,
                                PromotionId = promotion.PromotionId,
                                DecrementValue = (float)subtractAmount,
                                IncrementValue = 0,
                            };

                            //invoke DRE UpdateSubscriberPromotion API
                            CallDREUpdateSubscriberPromotionResponseInternal callDREUpdateSubscriberPromotionResponse = callDREUpdateSubscriberPromotion.Process(callDREUpdateRequest, null);
                            if (callDREUpdateSubscriberPromotionResponse.ErrorCode == 2)
                            {

                                throw new BusinessLogicErrorException("Donor_MSISDN doesn't have enough balance", BizOpsErrors.BenefitTransferNotEnoughBalance);

                            }

                            //subtract balance from customer promotion
                            SubtractBalanceRequest subtractBalanceRequest = new SubtractBalanceRequest()
                            {
                                Amount = subtractAmount,
                                Customer = request.SourceCustomerInfo,
                                CustomerPromotionInfo = promotion,
                                BusinessOperation = order.OperationsForOrder.FirstOrDefault()
                            };

                            subtractBalance.Process(subtractBalanceRequest, null);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (transferredAmount == 0)
                {
                    break;
                }
            }

            #endregion

            #region create response

            BenefitTransferResponseInternal response = new BenefitTransferResponseInternal();
            response.CreatedOrder = order;
            response.TransferredPormotions = transferPormtions;
            response.PurchasedPromotionInfo = purchasedCustomerPromotion;
            response.SourceProductCustomerAssociation = sourceProductPurchaseResponse.productPurchaseList;
            response.DestinationProductCustomerAssociation = destinationProductPurchaseResponse.productPurchaseList;
            response.ErrorCode = 0;
            response.Message = "Benefit transfer successfully";
            response.ResultType = ResultTypes.Ok;

            #endregion

            return response;
        }

        #endregion

        #region common method

        /// <summary>
        /// create PurchaseProductForCustomerRequestInternal
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="productOfferingId"></param>
        /// <param name="amount"></param>
        /// <param name="purchaseDate"></param>
        /// <returns></returns>
        private PurchaseProductForCustomerRequestInternal GetPurchaseProductRequest(CustomerInfo customerInfo, int productOfferingId, decimal amount, DateTime purchaseDate)
        {
            #region check customer account
            var _getActiveCusotmerAccountMS = MicroServiceManager.GetMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();

            GetActiveCustomerAccountAssociationByDateResponse customerAccountResponse = _getActiveCusotmerAccountMS.Process(new GetActiveCustomerAccountAssociationByDateRequest()
            {
                CustomerInfo = customerInfo,
                ActiveCustomerAccountAssociationDate = purchaseDate
            }, null);

            if (customerAccountResponse.CustomerAccountAssociation == null)
            {
                throw new BusinessLogicErrorException(String.Format("Unable to find an CustomerAccountAssociation for the CustomerID:{0}", customerInfo.CustomerID), BizOpsErrors.CustomerAccountAssociationNotFound);
            }
            #endregion
            #region Responset

            var coreInput = new PurchaseProductForCustomerRequestInternal();
            var helper = new PurchaseHelper();

            var productCatalog = new ProductCatalogDTO()
            {
                Id = productOfferingId,
                PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>()
            };
            var listDto = new List<ProductCatalogDTO>() {productCatalog};

            coreInput.Customer = customerInfo;
            coreInput.AccountDefinition = customerAccountResponse.CustomerAccountAssociation.Account;
            coreInput.ForceCreditLimit = null;
            coreInput.TypeOfPurchaseProductOperation = TypeOfPurchaseProduct.BenefitTransfer;
            coreInput.DatetimePurchase = purchaseDate;
            coreInput.listTuplePoducts = helper.GetProductsAndChargesOptions(listDto);

            #endregion

            return coreInput;
        }


        /// <summary>
        /// GetOperationsBycutomer
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        private IEnumerable<BusinessOperationExecution> GetOperationsBycutomer(CustomerInfo customerInfo)
        {
            IEnumerable<BusinessOperationExecution> businessOperations = new List<BusinessOperationExecution>();

            //get customer account
            var getCustomerAccountService = MicroServiceManager.GetMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();

            GetActiveCustomerAccountAssociationByDateRequest getCustomerAccountRequest = new GetActiveCustomerAccountAssociationByDateRequest();
            getCustomerAccountRequest.CustomerInfo = customerInfo;
            getCustomerAccountRequest.ActiveCustomerAccountAssociationDate = DateTime.Now;

            GetActiveCustomerAccountAssociationByDateResponse getCustomerAccountResponse = getCustomerAccountService.Process(getCustomerAccountRequest, null);

            if (getCustomerAccountResponse.CustomerAccountAssociation != null && getCustomerAccountResponse.CustomerAccountAssociation.Account != null)
            {
                GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest getInvoiceRequest = new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest();
                getInvoiceRequest.CustomerId = customerInfo.CustomerID.Value;
                getInvoiceRequest.LegalInvoiceNumber = null;

                var getInvoice = MicroServiceManager.GetMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();
                GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse getInvoiceResponse = getInvoice.Process(getInvoiceRequest, null);

                DateTime dtNow = DateTime.Now;
                var currentInvoice = getInvoiceResponse.Invoices.Where(x => x.ChargingAccount == getCustomerAccountResponse.CustomerAccountAssociation.Account && x.StartDate <= dtNow && x.EndDate >= dtNow).FirstOrDefault();


                if (currentInvoice != null)
                {
                    //get operations
                    GetSucessfulOperationExecutionForCustomerRequest getOperationsRequest = new GetSucessfulOperationExecutionForCustomerRequest
                    {
                        Customer = customerInfo,
                        StartDate = currentInvoice.StartDate,
                        EndDate = currentInvoice.EndDate ?? DateTime.Now,
                        OperationDefinition = this,
                    };

                    businessOperations = GetOperations(getOperationsRequest);
                }

            }

            //return
            return businessOperations;
        }


        /// <summary>
        /// get customer operation
        /// </summary>
        /// <param name="getOperationsRequest"></param>
        /// <returns></returns>
        private IEnumerable<BusinessOperationExecution> GetOperations(GetSucessfulOperationExecutionForCustomerRequest getOperationsRequest)
        {
            var getOperations = MicroServiceManager.GetMicroService<GetSucessfulOperationExecutionForCustomerRequest, GetSucessfulOperationExecutionForCustomerResponse>();
            GetSucessfulOperationExecutionForCustomerResponse getOperationsResponse = getOperations.Process(getOperationsRequest, null);
            return getOperationsResponse.Operations;
        }

        /// <summary>
        /// get customer promotion
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        private List<CrmCustomersPromotionInfo> GetCustomerPromotion(CustomerInfo customerInfo)
        {

            var customerPromotionService = MicroServiceManager.GetMicroService<GetCustomersActivePromotionInfoRequest, GetCustomersActivePromotionInfoResponse>();
            GetCustomersActivePromotionInfoRequest customerPromotionRequest = new GetCustomersActivePromotionInfoRequest();
            customerPromotionRequest.Customer = customerInfo;
            GetCustomersActivePromotionInfoResponse customerPromotionResponse = customerPromotionService.Process(customerPromotionRequest, null);
            var promotions = customerPromotionResponse.Promotions.Where(x => x.Active);
            return promotions.Any() ? promotions.ToList() : new List<CrmCustomersPromotionInfo>();
        }

        /// <summary>
        /// check product is exist
        /// </summary>
        /// <param name="productId"></param>
        private void CheckProductIsExistById(int productId)
        {
            var getProductByProductId = MicroServiceManager.GetMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();
            GetProductByProductIdRequest getProductByProductIdRequest = new GetProductByProductIdRequest()
            {
                ProductId = productId
            };

            GetProductByProductIdResponse getProductByProductIdReponse = getProductByProductId.Process(getProductByProductIdRequest, null);
            if (getProductByProductIdReponse == null && getProductByProductIdReponse.Product == null)
            {
                throw new DataValidationErrorException(String.Format("Unable to find information for the ProductID:{0}", productId), BizOpsErrors.ProductNotFound);

            }
        }

        /// <summary>
        /// get product offering by product offering id
        /// </summary>
        /// <param name="productOfferingId">product offering id</param>
        /// <returns></returns>
        private ProductOffering GetProductOfferingById(int productOfferingId)
        {
            var getProductByProductId = MicroServiceManager.GetMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();
            var getProductByProductIdRequest = new GetProductOfferingByProductOfferingIdRequest()
            {
                ProductOfferingId = productOfferingId
            };

            var getProductByProductIdReponse = getProductByProductId.Process(getProductByProductIdRequest, null);
            return getProductByProductIdReponse.ProductOffering;

        }

        /// <summary>
        /// get start date
        /// </summary>
        /// <param name="currentDay"></param>
        /// <returns></returns>
        private DateTime GetStartDate(int currentDay)
        {
            return DateTime.Now.AddDays(1 - currentDay).Date;
        }

        /// <summary>
        /// get end date
        /// </summary>
        /// <param name="currentDay"></param>
        /// <returns></returns>
        private DateTime GetEndtDate(int currentDay)
        {
            return DateTime.Now.AddDays(1 - currentDay).AddMonths(1).Date.AddSeconds(-1);
        }

        /// <summary>
        /// check purchase product permission
        /// </summary>
        /// <param name="customer">the customer that will purchase the product</param>
        /// <param name="purchaseProductId">the product id will be purchased by customer</param>
        /// <returns></returns>
        private bool CheckPermission(CustomerInfo customer, int purchaseProductId)
        {

            bool hasPermission = false;

            //get all customer product assignments by customerId
            var getCustomerProductAssignmentsByCustomerIdMs = MicroServiceManager.GetMicroService<GetCustomerProductAssignmentsByCustomerIdRequest, GetCustomerProductAssignmentsByCustomerIdResponse>();
            var getCustomerProductAssignmentsByCustomerIdRequest = new GetCustomerProductAssignmentsByCustomerIdRequest()
            {
                CustomerId = customer.CustomerID.Value,
            };
            var getCustomerProductAssignmentsByCustomerIdResponse = getCustomerProductAssignmentsByCustomerIdMs.Process(getCustomerProductAssignmentsByCustomerIdRequest, null);


            //get purchase product
            var purchaseProduct = GetProductOfferingById(purchaseProductId);
            var PurchaseProductLs = new List<ProductOffering>();
            PurchaseProductLs.Add(purchaseProduct);

            //check permission
            var checkProductListDependencyRelationsForCustomerMS = MicroServiceManager.GetMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();
            var checkProductListDependencyRelationsForCustomerRequest = new CheckProductListDependencyRelationsForCustomerRequest()
            {
                ProductsToPurchase = PurchaseProductLs,
                CustomerProducts = getCustomerProductAssignmentsByCustomerIdResponse.CustomerProductAssignments.ToList()
            };

            var checkProductListDependencyRelationsForCustomerResponse = checkProductListDependencyRelationsForCustomerMS.Process(checkProductListDependencyRelationsForCustomerRequest, null);
            if (checkProductListDependencyRelationsForCustomerResponse.AreListRequirementsSatisfiedForCustomer)
            {
                hasPermission = true;
            }

            return hasPermission;

        }


        /// <summary>
        /// add customer balance transation hitory
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <param name="productId"></param>
        private void AddBalanceTransationHistory(BenefitTransferOrder order, BenefitTransferRequestInternal request, int productId)
        {
            ProductOffering destinationProduct = GetProductOfferingById(productId);
            var promotionPlanId = destinationProduct.OfferedProduct.AssociatedPrmotionPlan.PromotionPlanId;
            var purchaseCustoemrPromotin = request.DestinationCustomerInfo.Promotions.FirstOrDefault(x => x.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId == promotionPlanId);

            CrmCustomersBalanceTransationHistory destinationCustomerHistory = new CrmCustomersBalanceTransationHistory
            {
                Amount = request.Amount,
                AmountBefore = 0,
                AmountAfter = request.Amount,
                ChangingOperation = order.OperationsForOrder.FirstOrDefault(),
                Customer = request.DestinationCustomerInfo,
                CustomerPromotion = purchaseCustoemrPromotin,
                TransactionTime = DateTime.Now,
                PromotionPlan = destinationProduct.OfferedProduct.AssociatedPrmotionPlan
            };

            var addTransationHistory = MicroServiceManager.GetMicroService<AddCrmCustomersBalanceTransationHistoryRequest, AddCrmCustomersBalanceTransationHistoryResponse>();
            var addTransationHistoryRequest = new AddCrmCustomersBalanceTransationHistoryRequest
            {
                customersBalanceTransationHistory = destinationCustomerHistory
            };
            addTransationHistory.Process(addTransationHistoryRequest, null);
        }

        /// <summary>
        /// get transfered promotion limit during a bill cycle 
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="customerPromotions"></param>
        /// <param name="destinationTransferProductId"></param>
        /// <returns></returns>
        private decimal GetTransferedLimit(CustomerInfo customerInfo, List<CrmCustomersPromotionInfo> customerPromotions, int destinationTransferProductId)
        {

            decimal transferedLimit = 0;
            int benefitPromotionPlanDetailId = 0;

            var getProductByProductId = MicroServiceManager.GetMicroService<GetProductByProductIdRequest, GetProductByProductIdResponse>();
            GetProductByProductIdRequest getProductByProductIdRequest = new GetProductByProductIdRequest()
            {
                ProductId = destinationTransferProductId
            };

            GetProductByProductIdResponse getProductByProductIdReponse = getProductByProductId.Process(getProductByProductIdRequest, null);

            RmPromotionPlanDetailInfo promotionPlanDetailInfo = getProductByProductIdReponse.Product.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList.FirstOrDefault();

            if (promotionPlanDetailInfo != null)
            {
                benefitPromotionPlanDetailId = promotionPlanDetailInfo.PromotionPlanDetailId;
            }


            //get customer account
            var getCustomerAccountService = MicroServiceManager.GetMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();

            GetActiveCustomerAccountAssociationByDateRequest getCustomerAccountRequest = new GetActiveCustomerAccountAssociationByDateRequest();
            getCustomerAccountRequest.CustomerInfo = customerInfo;
            getCustomerAccountRequest.ActiveCustomerAccountAssociationDate = DateTime.Now;

            GetActiveCustomerAccountAssociationByDateResponse getCustomerAccountResponse = getCustomerAccountService.Process(getCustomerAccountRequest, null);

            if (getCustomerAccountResponse.CustomerAccountAssociation != null && getCustomerAccountResponse.CustomerAccountAssociation.Account != null)
            {
                GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest getInvoiceRequest = new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest();
                getInvoiceRequest.CustomerId = customerInfo.CustomerID.Value;
                getInvoiceRequest.LegalInvoiceNumber = null;

                var getInvoice = MicroServiceManager.GetMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();
                GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse getInvoiceResponse = getInvoice.Process(getInvoiceRequest, null);

                DateTime dtNow = DateTime.Now;
                var currentInvoice = getInvoiceResponse.Invoices.Where(x => x.ChargingAccount == getCustomerAccountResponse.CustomerAccountAssociation.Account && x.StartDate <= dtNow && x.EndDate >= dtNow).FirstOrDefault();

                if (currentInvoice != null)
                {
                    var transferPromotion = customerPromotions.Where(x => x.PromotionDetail.PromotionPlanDetailId == benefitPromotionPlanDetailId && x.Active && x.CurrentLimit > 0 && x.StartDate >= currentInvoice.StartDate && (!x.EndDate.HasValue || x.EndDate <= currentInvoice.EndDate));
                    if (transferPromotion != null)
                    {
                        transferedLimit = transferPromotion.Sum(x => x.CurrentLimit);
                    }
                }
            }

            return transferedLimit;

        }


        /* private QuerySubscriberPromotionRes QuerySubscriberPromotion(string msisdn, string promotionId, string serverURL)
         {
              CallDREQuerySubscriberPromotionRequest dreRequest = new CallDREQuerySubscriberPromotionRequest();
              dreRequest.msisdn = msisdn;
              dreRequest.promotionId = promotionId;
              dreRequest.serverURL = serverURL;


              var DREServiceQuery = MicroServiceManager.GetMicroService<CallDREQuerySubscriberPromotionRequest, CallDREQuerySubscriberPromotionResponse>();
              CallDREQuerySubscriberPromotionResponse dreResponse = DREServiceQuery.Process(dreRequest, null);
              return dreResponse.DREResponse;

             return new QuerySubscriberPromotionRes();
         }

         private UpdateSubscriberPromotionRes UpdateSubscriberPromotion(string msisdn, long promotionId, float descrement, string serverURL)
         {
             UpdateSubscriberPromotionReq DRERequest = new UpdateSubscriberPromotionReq();
             DRERequest.Msisdn = msisdn;
             DRERequest.PromotionId = promotionId;
             DRERequest.DecrementValue = descrement;

             CallDREUpdateSubscriberPromotionRequest request = new CallDREUpdateSubscriberPromotionRequest();
             request.DRERequest = DRERequest;
             request.serverURL = serverURL;

             var DREServiceUpdate = MicroServiceManager.GetMicroService<CallDREUpdateSubscriberPromotionRequest, CallDREUpdateSubscriberPromotionResponse>();
             CallDREUpdateSubscriberPromotionResponse dreResponse = DREServiceUpdate.Process(request, null);
             return dreResponse.DREResponse;

         }*/

        #endregion

    }
}

