using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using com.etak.core.bizops.helper;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByIdAndName;
using com.etak.core.GSMSubscription.messages.CheckProductListDependencyRelationsForCustomer;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerId;
using com.etak.core.microservices.messages.GetTaxDefinitonsByCategory;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.product.message.GetProductOfferingByProductOfferingId;
using com.etak.core.service.messages.GetPriorityBundleInfoFromBundleInfos;
using log4net;

namespace com.etak.core.bizops.fullfilment.CheckPurchaseProductForCustomer
{
    /// <summary>
    /// Check if list of products can be purchased for customer
    /// </summary>
    public class CheckPurchaseProductForCustomerBizOp : AbstractBusinessOperation<CheckPurchaseProductForCustomerRequestDTO, CheckPurchaseProductForCustomerResponseDTO, CheckPurchaseProductForCustomerRequestInternal, CheckPurchaseProductForCustomerResponseInternal>
    {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const string PurchaseProductLimitPromotionCategoryName = "PURCHASE_PRODUCT_LIMIT_PROMOTION";
        private const string PurchaseProductUnLimitedPromotionCategoryName = "PURCHASE_PRODUCT_UNLIMITED_PROMOTION";
        private const string CommercialProductTypeDescription = "Commercial";
        private PurchaseHelper purchaseHelper;

        /// <summary>
        /// Standard Constructor for the business operation
        /// </summary>
        public CheckPurchaseProductForCustomerBizOp()
        {
            purchaseHelper = new PurchaseHelper();
        }

        /// <summary>
        /// Constructor passing the BusinessOperations to be mocked
        /// </summary>
        /// <param name="purchaseHelperParam"></param>
        public CheckPurchaseProductForCustomerBizOp( PurchaseHelper purchaseHelperParam )
        {
            purchaseHelper = purchaseHelperParam;
        }
        
        /// <summary>
        /// MapNotAutomappedInboundProperties
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>

        protected override void MapNotAutomappedInboundProperties(CheckPurchaseProductForCustomerRequestDTO request, ref CheckPurchaseProductForCustomerRequestInternal coreInput)
        {
            if (coreInput.Customer == null)
                throw new DataValidationErrorException(string.Format("Unable to find information for the CustomerID:{0}", coreInput.Customer), BizOpsErrors.CustomerNotFound);

            if (coreInput.Customer.StatusID != null && coreInput.Customer.StatusID.Value == (int) CustomerStatus.Deleted)
                throw new DataValidationErrorException(string.Format("The customer {0} is already in Deleted Status", coreInput.Customer.CustomerID), BizOpsErrors.CustomerInDeletedStatus);

            //Get forceCreditLimit and purchaseDate
            coreInput.ForceCreditLimit = request.ForceCreditLimit;
            coreInput.PurchaseDate = request.PurchaseDate;


            #region Get list of purchase product limit promotions configurations by MVNO
            if (Log.IsDebugEnabled)
                Log.DebugFormat(
                    "Calling GetMVNOConfigActionInfosByIdAndName to get the MvnoConfigActionInfos with MvnoId ({0}) , StatusId ({1}) and CategoryName ({2}).",
                    (int)coreInput.MVNO.DealerID, (int)DefaultMVNOConfig.CPS, PurchaseProductLimitPromotionCategoryName);
            var getMVNOConfigActionInfosByIdAndNameMS = MicroServiceManager.GetMicroService<GetMVNOConfigActionInfosByIdAndNameRequest,GetMVNOConfigActionInfosByIdAndNameResponse>();
            var getMVNOConfigActionInfosByIdAndNameRequest = new GetMVNOConfigActionInfosByIdAndNameRequest()
            {
                CategoryName = PurchaseProductLimitPromotionCategoryName,
                MvnoId = (int) coreInput.MVNO.DealerID,
                StatusId = (int) DefaultMVNOConfig.CPS,
            };
            var getMVNOConfigActionInfosByIdAndNameResponse = getMVNOConfigActionInfosByIdAndNameMS.Process(getMVNOConfigActionInfosByIdAndNameRequest, null);

            if (getMVNOConfigActionInfosByIdAndNameResponse.MvnoConfigActionInfos == null || !getMVNOConfigActionInfosByIdAndNameResponse.MvnoConfigActionInfos.Any())
                throw new InternalErrorException(string.Format("There are no Configurations for Limit Services"), BizOpsErrors.NoLimitServicesConfiguration);
            coreInput.PuchaseProductLimitPromotions = getMVNOConfigActionInfosByIdAndNameResponse.MvnoConfigActionInfos;
            #endregion

            #region Get list of purchase product unlimited promotions configurations by MVNO
            if (Log.IsDebugEnabled)
                Log.DebugFormat(
                    "Calling GetMVNOConfigActionInfosByIdAndName to get the MvnoConfigActionInfos with MvnoId ({0}) , StatusId ({1}) and CategoryName ({2}).",
                    (int)coreInput.MVNO.DealerID, (int)DefaultMVNOConfig.CPS, PurchaseProductUnLimitedPromotionCategoryName);
            var getMVNOConfigActionInfosByIdAndNameRequest2 = new GetMVNOConfigActionInfosByIdAndNameRequest()
            {
                CategoryName = PurchaseProductUnLimitedPromotionCategoryName,
                MvnoId = (int)coreInput.MVNO.DealerID,
                StatusId = (int)DefaultMVNOConfig.CPS,
            };
            var getMVNOConfigActionInfosByIdAndNameResponse2 = getMVNOConfigActionInfosByIdAndNameMS.Process(getMVNOConfigActionInfosByIdAndNameRequest2, null);

            if (getMVNOConfigActionInfosByIdAndNameResponse2.ResultType==ResultTypes.Ok)
                coreInput.PuchaseProductUnLimitedPromotions = getMVNOConfigActionInfosByIdAndNameResponse2.MvnoConfigActionInfos;
            #endregion

            #region Get all purchase products
            coreInput.ListTuplesPurchaseProducts = purchaseHelper.GetProductsAndChargesOptions(request.ProductCatalogDTOs);
            #endregion

            #region Get TaxDefinition
            if (Log.IsDebugEnabled)
                Log.DebugFormat("Calling GetTaxDefinitonsByCategory with TaxCategory ({0}) .", request.TaxCategory);

            var getTaxDefinition = MicroServiceManager.GetMicroService<GetTaxDefinitonsByCategoryRequest, GetTaxDefinitonsByCategoryResponse>();
            var getTaxDefResp = getTaxDefinition.Process(new GetTaxDefinitonsByCategoryRequest()
            {
                TaxCategory = request.TaxCategory,
            }, null);

            coreInput.TaxDefinition = getTaxDefResp.TaxDefinitions.FirstOrDefault();
            #endregion

            #region get active cusotmer account
            var getActiveCusotmerAccountMs = MicroServiceManager.GetMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();

            if (Log.IsDebugEnabled)
                Log.DebugFormat(
                    "Set GetActiveCustomerAccountAssociationByDateRequest with customerid ({0}) and  ActiveCustomerAccountAssociationDate ({1}).",
                    coreInput.Customer.CustomerID.Value,
                    request.PurchaseDate);
            var getActiveCustomerAccountAssociationByDateResponse = getActiveCusotmerAccountMs.Process(new GetActiveCustomerAccountAssociationByDateRequest()
            {
                CustomerInfo = coreInput.Customer,
                ActiveCustomerAccountAssociationDate = request.PurchaseDate,
            }, null);
            if (getActiveCustomerAccountAssociationByDateResponse.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("GetActiveCustomerAccountAssociationByDate: Failed", BizOpsErrors.CustomerAccountNotFound);

            if (getActiveCustomerAccountAssociationByDateResponse.CustomerAccountAssociation == null)
            {
                throw new BusinessLogicErrorException(String.Format("Unable to find an CustomerAccountAssociation for the CustomerID:{0}",
                    coreInput.Customer.CustomerID), BizOpsErrors.CustomerAccountAssociationNotFound);
            }
            var currentAccount = getActiveCustomerAccountAssociationByDateResponse.CustomerAccountAssociation.Account;
            #endregion

            #region get billRun date
            var calculateNextBillRunDateMS = MicroServiceManager.GetMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            if (Log.IsDebugEnabled)
                Log.DebugFormat(
                    "Set CalculateNextBillRunDateForBillCycleRequest with BillCycle id ({0}) , purchaseTime ({1}) and firstDayOfWeek({2}).",
                    currentAccount.BillingCycle != null ? currentAccount.BillingCycle.Id : 0,
                    DateTime.Now,
                    CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
            var calculateNextBillRunDateForBillCycleResponse = calculateNextBillRunDateMS.Process(new CalculateNextBillRunDateForBillCycleRequest()
            {
                BillCycleDefinition = currentAccount.BillingCycle,
                PurchaseTime = request.PurchaseDate,
                FirstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek,
            }, null);
            if (calculateNextBillRunDateForBillCycleResponse.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("CalculateNextBillRunDateForBillCycle: Failed", BizOpsErrors.CalculateNextBillrunDateError);

            coreInput.NextBillRunDate = calculateNextBillRunDateForBillCycleResponse.NextBillRun;
            coreInput.TypeOfPurchaseProductOperation = TypeOfPurchaseProduct.CheckPurchaseProduct;
            #endregion
        }

        /// <summary>
        /// Mapping Internal response to DTO output
        /// </summary>
        /// <param name="coreOutput"></param>
        /// <param name="dtoOutput"></param>
        protected override void MapNotAutomappedOutboundProperties(CheckPurchaseProductForCustomerResponseInternal coreOutput, ref CheckPurchaseProductForCustomerResponseDTO dtoOutput)
        {
            dtoOutput.IsCreditEnough = coreOutput.IsCreditEnough;
            dtoOutput.AmountRequired = coreOutput.AmountRequired;
            dtoOutput.DeprovisionList = coreOutput.DeprovisionList;
            dtoOutput.IsLimitReached = coreOutput.IsLimitReached;
            dtoOutput.IsListCompatibleWithCustomerProducts = coreOutput.IsListCompatibleWithCustomerProducts;
            dtoOutput.AreListRequirementsSatisfiedForCustomer = coreOutput.AreListRequirementsSatisfiedForCustomer;
        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return (OperationCodes.CheckPurchaseProductForCustomerBizOp); }
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return (OperationCodes.CheckPurchaseProductForCustomerBizOp); }
        }

        /// <summary>
        /// Check if list of products can be purchased for customer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="runningOperation"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        protected override CheckPurchaseProductForCustomerResponseInternal ProcessBusinessLogic(CheckPurchaseProductForCustomerRequestInternal request, core.model.operation.BusinessOperationExecution runningOperation, core.operation.RequestInvokationEnvironment invoker)
        {
            var checkPurchaseProductForCustomerResponseInternal = new CheckPurchaseProductForCustomerResponseInternal();

            #region Get all customer products and add in customerProducts list
            //Get all customer product assignments by customerId
            var getCustomerProductAssignmentsByCustomerIdMs = MicroServiceManager.GetMicroService<GetCustomerProductAssignmentsByCustomerIdRequest, GetCustomerProductAssignmentsByCustomerIdResponse>();
            var getCustomerProductAssignmentsByCustomerIdRequest = new GetCustomerProductAssignmentsByCustomerIdRequest()
            {
                CustomerId = request.Customer.CustomerID.Value,
            };

            var getCustomerProductAssignmentsByCustomerIdResponse = getCustomerProductAssignmentsByCustomerIdMs.Process(getCustomerProductAssignmentsByCustomerIdRequest, null);

            var customerProducts = new List<ProductOffering>();

            if (getCustomerProductAssignmentsByCustomerIdResponse.CustomerProductAssignments.Any())
            {
                customerProducts.AddRange(getCustomerProductAssignmentsByCustomerIdResponse.CustomerProductAssignments.Select(customerProductAssignment => customerProductAssignment.ProductOffering));
            }
            #endregion

            #region Check product list dependency and relations
            bool isListCompatibleWithCustomerProducts;
            bool areListRequirementsSatisfiedForCustomer;
            List<ProductOfferingSpecificationOption> deprovisionList;

            CheckProductListDependencyAndRelations(request.ListTuplesPurchaseProducts,request.PurchaseDate, request.NextBillRunDate, request.TypeOfPurchaseProductOperation,
                getCustomerProductAssignmentsByCustomerIdResponse.CustomerProductAssignments,
                out isListCompatibleWithCustomerProducts, out areListRequirementsSatisfiedForCustomer,
                out deprovisionList);

            checkPurchaseProductForCustomerResponseInternal.IsListCompatibleWithCustomerProducts = isListCompatibleWithCustomerProducts;
            checkPurchaseProductForCustomerResponseInternal.AreListRequirementsSatisfiedForCustomer = areListRequirementsSatisfiedForCustomer;
            checkPurchaseProductForCustomerResponseInternal.DeprovisionList = deprovisionList;
            #endregion

            #region Check credit limit of new products for customer and if promotions limits is reached
            var productsToPurchase = request.ListTuplesPurchaseProducts.Select(x => x.Item1).ToList();
            
            #region get creditlimit and unbilled balance
            decimal creditLimit;
            decimal unBilledBalance;

            if (Log.IsDebugEnabled)
                Log.DebugFormat("execute GetCreditLimit");
            GetCreditLimit(request.ForceCreditLimit, productsToPurchase, customerProducts, request.Customer, out creditLimit, out unBilledBalance);
            if (Log.IsDebugEnabled)
                Log.DebugFormat("GetCreditLimit return creditLimit ({0}) and  unBilledBalance ({1}).", creditLimit, unBilledBalance);
            #endregion

            #region check credit limit and credit enough for new products
            bool isCreditEnough;
            bool isLimitReached;
            decimal amountRequired;

            CheckCreditLimitAndCreditEnoughForNewProductsForCustomer(request.ListTuplesPurchaseProducts, deprovisionList, request.PurchaseDate, request.NextBillRunDate,
                request.PuchaseProductLimitPromotions, request.PuchaseProductUnLimitedPromotions,
                request.TaxDefinition, request.Customer, creditLimit, unBilledBalance, out isCreditEnough, out isLimitReached, out amountRequired);
            if (Log.IsDebugEnabled)
                Log.DebugFormat(
                    "CheckCreditLimitAndCreditEnoughForNewProductsForCustomer return isLimitReached ({0}) , isCreditEnough ({1}) and amountRequired ({2}).",
                    isLimitReached, isCreditEnough, amountRequired);

            checkPurchaseProductForCustomerResponseInternal.IsCreditEnough = isCreditEnough;
            checkPurchaseProductForCustomerResponseInternal.IsLimitReached = isLimitReached;
            checkPurchaseProductForCustomerResponseInternal.AmountRequired = amountRequired;
            #endregion
            
            #endregion

            checkPurchaseProductForCustomerResponseInternal.ResultType = ResultTypes.Ok;
            checkPurchaseProductForCustomerResponseInternal.Subscription = request.Customer.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active);

            return checkPurchaseProductForCustomerResponseInternal;
        }

        /// <summary>
        /// check product list depenecy and relations, check next billRun products 
        /// </summary>
        /// <returns></returns>
        private void CheckProductListDependencyAndRelations(List<Tuple<ProductOffering, ProductChargeOption>> listTuplesPurchaseProducts, DateTime purchaseDate, DateTime nextBillRunDate,
                TypeOfPurchaseProduct typeOfPurchaseProductOperation,
                IEnumerable<CustomerProductAssignment> customerProductAssignments,
                out bool isListCompatibleWithCustomerProducts, out bool areListRequirementsSatisfiedForCustomer,
                out List<ProductOfferingSpecificationOption> deprovisionList)
        {
            //TODO check if you can buy in the specified productChargeOptionRange

            #region Check product list dependency and relations
            var checkProductListDependencyRelationsForCustomerMS = MicroServiceManager.GetMicroService<CheckProductListDependencyRelationsForCustomerRequest, CheckProductListDependencyRelationsForCustomerResponse>();

            #region Check product relations, if IsListCompatibleWithCustomerProducts is false we will recheck split by billrun date
            var checkProductListDependencyRelationsFromNextBillRunForCustomerRequest = new CheckProductListDependencyRelationsForCustomerRequest()
            {
                ProductsToPurchase = listTuplesPurchaseProducts.Select(x => x.Item1).ToList(),
                CustomerProducts = customerProductAssignments.ToList()
            };

            var checkProductListDependencyRelationsFromNextBillRunForCustomerResponse =
                checkProductListDependencyRelationsForCustomerMS.Process(
                    checkProductListDependencyRelationsFromNextBillRunForCustomerRequest, null);
            //set response to responseInternal
            isListCompatibleWithCustomerProducts =
                checkProductListDependencyRelationsFromNextBillRunForCustomerResponse
                    .IsListCompatibleWithCustomerProducts;
            areListRequirementsSatisfiedForCustomer =
                checkProductListDependencyRelationsFromNextBillRunForCustomerResponse
                    .AreListRequirementsSatisfiedForCustomer;
            deprovisionList = checkProductListDependencyRelationsFromNextBillRunForCustomerResponse.DeprovisionList;
            #endregion


            if (!isListCompatibleWithCustomerProducts)
            {
                #region add products to purchase that start in next billRun to productsToPurchaseStartNextBillRun and others to productsToPurchaseStartNow
                var productsToPurchaseWithStartDateNextBillRun = new List<ProductOffering>();
                var productsToPurchaseWithStartDateNow = new List<ProductOffering>();
                foreach (var tuplesPurchaseProduct in listTuplesPurchaseProducts)
                {
                    var productChargeOption = tuplesPurchaseProduct.Item2;
                    var startProductDate =
                        PurchaseHelper.GetStartDateByProductChargeOptionAndTypeOfPurchaseProduct(productChargeOption,
                            purchaseDate, nextBillRunDate, typeOfPurchaseProductOperation);
                    if (startProductDate >= nextBillRunDate)
                    {
                        productsToPurchaseWithStartDateNextBillRun.Add(tuplesPurchaseProduct.Item1);
                    }
                    else
                    {
                        productsToPurchaseWithStartDateNow.Add(tuplesPurchaseProduct.Item1);
                    }

                }

                #endregion

                #region add customer products that start in next billRun to customerProductsStartNextBillRun and others to customerProductsStartNow
                var customerProductsStartNextBillRun = new List<CustomerProductAssignment>();
                var customerProductsStartNow = new List<CustomerProductAssignment>();
                //TODO check if we need to use getCustomerProductAssignmentsByCustomerIdResponse.CustomerProductAssignments or customerProducts
                foreach (var customerProductAssignment in customerProductAssignments)
                {
                    if ((customerProductAssignment.EndDate > nextBillRunDate) || (customerProductAssignment.EndDate == null))
                    {
                        customerProductsStartNextBillRun.Add(customerProductAssignment);
                        customerProductsStartNow.Add(customerProductAssignment);
                    }
                    else
                    {
                        customerProductsStartNow.Add(customerProductAssignment);
                    }
                }

                #endregion

                #region check products to purchase that start in next billRun

                if (productsToPurchaseWithStartDateNextBillRun.Any())
                {
                    
                    checkProductListDependencyRelationsFromNextBillRunForCustomerRequest = new CheckProductListDependencyRelationsForCustomerRequest
                        ()
                    {
                        ProductsToPurchase = productsToPurchaseWithStartDateNextBillRun,
                        CustomerProducts = customerProductsStartNextBillRun
                    };
                    checkProductListDependencyRelationsFromNextBillRunForCustomerResponse =
                        checkProductListDependencyRelationsForCustomerMS.Process(
                            checkProductListDependencyRelationsFromNextBillRunForCustomerRequest, null);
                    //set response to responseInternal
                    isListCompatibleWithCustomerProducts =
                        checkProductListDependencyRelationsFromNextBillRunForCustomerResponse
                            .IsListCompatibleWithCustomerProducts;
                    areListRequirementsSatisfiedForCustomer =
                        checkProductListDependencyRelationsFromNextBillRunForCustomerResponse
                            .AreListRequirementsSatisfiedForCustomer;
                }

                #endregion

                #region check products to purchase that start now

                if (productsToPurchaseWithStartDateNow.Any())
                {

                    var checkProductListDependencyRelationsForCustomerRequest = new CheckProductListDependencyRelationsForCustomerRequest
                        ()
                    {
                        ProductsToPurchase = productsToPurchaseWithStartDateNow,
                        CustomerProducts = customerProductsStartNow
                    };
                    var checkProductListDependencyRelationsForCustomerResponse =
                        checkProductListDependencyRelationsForCustomerMS.Process(
                            checkProductListDependencyRelationsForCustomerRequest, null);

                    //update responseInternal
                    isListCompatibleWithCustomerProducts = isListCompatibleWithCustomerProducts &&
                                                           checkProductListDependencyRelationsForCustomerResponse
                                                               .IsListCompatibleWithCustomerProducts;
                    areListRequirementsSatisfiedForCustomer = areListRequirementsSatisfiedForCustomer &&
                                                              checkProductListDependencyRelationsForCustomerResponse
                                                                  .AreListRequirementsSatisfiedForCustomer;
                }

                #endregion
            }

            #endregion 
            if (!areListRequirementsSatisfiedForCustomer)
                throw new BusinessLogicErrorException(
                    "The list of requirements are not satisfied for customer products",
                    BizOpsErrors.ListRequirementsNotSatisfiedForCustomer);

            if (!isListCompatibleWithCustomerProducts)
                throw new BusinessLogicErrorException(
                    "The products in requested list have incompatibilities with other products in the same list ",
                    BizOpsErrors.ListNotCompatibleWithCustomerProducts);
        }

        /// <summary>
        /// Get first price applied in specific date of purchase 
        /// </summary>
        /// <param name="charge"></param>
        /// <param name="datePurchase"></param>
        /// <returns></returns>
        private ChargePrice getPrice(Charge charge, DateTime datePurchase)
        {
            var chargePrice = charge.Prices.OrderByDescending(x => x.StartDate).FirstOrDefault(x => (x.EndDate == null && datePurchase >= x.StartDate)
                                                                                              || (datePurchase >= x.StartDate && datePurchase <= x.EndDate));

            if (chargePrice == null)
                throw new InternalErrorException(string.Format("There is not a price for ChargeID {0} and the date:{1}", charge.Id, datePurchase), BizOpsErrors.NotPriceForChargeIdInDate);

            return chargePrice;
        }

        /// <summary>
        /// Get first taxRate applied in specific date
        /// </summary>
        /// <param name="taxDefinition"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private TaxRates getTaxRate(TaxDefinition taxDefinition, DateTime dateTime)
        {
            var taxRates = taxDefinition.Rates.OrderByDescending(x => x.StartDate).FirstOrDefault(x => (x.EndDate == null && dateTime >= x.StartDate)
                                                                    || (dateTime >= x.StartDate && dateTime <= x.EndDate));

            if (taxRates == null)
                throw new InternalErrorException(string.Format("There is not a TaxRate for the TaxDefinitionID:{0}", taxDefinition.Id), BizOpsErrors.NoTaxRatesForTaxDefinitionId);

            return taxRates;
        }


        /// <summary>
        /// check if we reach limit promotions for new products and actual customer products, and check if is credit enough to buy new products
        /// If promotions limit reached (isLimitReached) then you don't need to check isCreditEnough and amountRequired
        /// </summary>
        /// <param name="ListTuplesPurchaseProducts"></param>
        /// <param name="deprovisionList"></param>
        /// <param name="purchaseDate"></param>
        /// <param name="nextBillRunDate"></param>
        /// <param name="puchaseProductLimitPromotions"></param>
        /// <param name="puchaseProductUnlimitedLimitPromotions"></param>
        /// <param name="taxDefinition"></param>
        /// <param name="customer"></param>
        /// <param name="creditLimit"></param>
        /// <param name="unBilledBalance"></param>
        /// <param name="isCreditEnough">return if credit is enough</param>
        /// <param name="isLimitReached">return if limit es reached</param>
        /// <param name="amountRequired">if limit is reached return the amount required</param>
        private void CheckCreditLimitAndCreditEnoughForNewProductsForCustomer(List<Tuple<ProductOffering, ProductChargeOption>> ListTuplesPurchaseProducts,
            List<ProductOfferingSpecificationOption> deprovisionList,
            DateTime purchaseDate, DateTime nextBillRunDate,
            IEnumerable<MVNOConfigActionInfo> puchaseProductLimitPromotions,
            IEnumerable<MVNOConfigActionInfo> puchaseProductUnlimitedLimitPromotions,
            TaxDefinition taxDefinition, CustomerInfo customer, decimal creditLimit, decimal unBilledBalance, out bool isCreditEnough, out bool isLimitReached, out decimal amountRequired)
        {
            isCreditEnough = true;
            isLimitReached = false;
            amountRequired = 0;

            //Get a list of all SubServiceTypeId of new products
            var productsPromotions = ListTuplesPurchaseProducts.Where(x => x.Item1.OfferedProduct.AssociatedPrmotionPlan != null);
            var rmPromotionPlanDetailInfoListOfPurchaseProducts = productsPromotions.Select(x => x.Item1.OfferedProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList);
            var subServiceTypeIdsOfPurchaseProducts = new List<int>();
            foreach (var rmPromotionPlanDetailInfoOfPurchaseProduct in rmPromotionPlanDetailInfoListOfPurchaseProducts.SelectMany(rmPromotionPlanDetailInfoListOfPurchaseProduct => rmPromotionPlanDetailInfoListOfPurchaseProduct))
            {
                subServiceTypeIdsOfPurchaseProducts.Add(rmPromotionPlanDetailInfoOfPurchaseProduct.SubServiceTypeId);
            }

            //Get all promotionPlanDetailId that has associated product with commercial type
            //I do this because the entity ProductInfo from CustomerInfo doesn't have Product type like entity Product
            var customerPromotionPlanDetailIdWithCommercialProducts = GetAllCustomerPromotionPlanDetailIdsWithCommercialProducts(customer);
            
            //initalize variable to control if we have enough credit
            var accumulateAmount = creditLimit - unBilledBalance;

            //initilialize dictionary to accumulate all different limit promotions with service type id from MvnoconfigAction
            var currentPromotionAccumulateType = puchaseProductLimitPromotions.ToDictionary<MVNOConfigActionInfo, int, decimal>(itemConfig => int.Parse(itemConfig.BAK1), itemConfig => 0);
            var futurePromotionAccumulateType = puchaseProductLimitPromotions.ToDictionary<MVNOConfigActionInfo, int, decimal>(itemConfig => int.Parse(itemConfig.BAK1), itemConfig => 0);

            #region  Accumulate the creditlimit of customer's promotions which has already owned
                #region Accumulate customer future promotions
                var futurePromotions = customer.Promotions.Where(x => nextBillRunDate != DateTime.MinValue && x.Active
                        && x.StartDate >= nextBillRunDate && (x.EndDate == null || x.EndDate > x.StartDate)
                        && x.PromotionDetail.PromotionPlanDetailId.In(customerPromotionPlanDetailIdWithCommercialProducts.ToArray()));
                    if (futurePromotions.Any())
                    {
                        AccumulateCustomerPromotionCreditlimit(futurePromotions, puchaseProductUnlimitedLimitPromotions, ref futurePromotionAccumulateType, deprovisionList);
                    }
                    #endregion

                #region  Accumulate customer current promotions
                var currentPromotions = customer.Promotions.Where(x =>  x.Active && ((x.EndDate == null && purchaseDate >= x.StartDate)
                                    || (purchaseDate >= x.StartDate && purchaseDate < x.EndDate)) && x.PromotionDetail.PromotionPlanDetailId.In(customerPromotionPlanDetailIdWithCommercialProducts.ToArray()));
                if (currentPromotions.Any())
                {
                    AccumulateCustomerPromotionCreditlimit(currentPromotions, puchaseProductUnlimitedLimitPromotions,
                        ref currentPromotionAccumulateType, deprovisionList);
                }
                #endregion 
            #endregion

            #region   Accumulate the limit of promotion which are purchasing

                foreach (var product in ListTuplesPurchaseProducts)
            {
                #region check if promotion limit is reached for products to purchase
                // Check only Commercial Products and not get Quality Of Service product
                if (product.Item1.OfferedProduct.AssociatedPrmotionPlan != null && product.Item1.OfferedProduct.Type.Description == CommercialProductTypeDescription)
                {
                    var rmFuturePromotionsPlanDetailInfoIds = new List<int>();
                    #region Calculate purchased future limit promotions
                    var rmFuturePromotionPlanDetailInfos = product.Item1.OfferedProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList.Where(
                                                    x => nextBillRunDate != DateTime.MinValue && x.StartDate >= nextBillRunDate).ToList();

                    // Include promotion with Source in deprovisionList, because will be in the future
                    var promotionToForceDeprovisioned = deprovisionList.Where(
                        //x => x.SourceProduct.Id == product.Item1.Id);
                        x => x.RelatedProductOffering.Id == product.Item1.Id);
                    if (promotionToForceDeprovisioned.Any())
                    {
                        rmFuturePromotionPlanDetailInfos.AddRange(product.Item1.OfferedProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList);
                    }

                    if (rmFuturePromotionPlanDetailInfos.Any())
                    {
                        AccumulatePurchasingPromotionLimit(rmFuturePromotionPlanDetailInfos, puchaseProductUnlimitedLimitPromotions, ref futurePromotionAccumulateType, deprovisionList);
                    }
                    else
                    {
                        if (Log.IsDebugEnabled)
                            Log.DebugFormat("No purchase products promotions that start in nextBillRunDate by product id ({0})", product.Item1.Id);
                    }
                    #endregion

                    #region Calculate purchased current limit promotions
                    var rmCurrentPromotionPlanDetailInfos = product.Item1.OfferedProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList.Where(
                                                 x =>((x.EndDate == null && purchaseDate >= x.StartDate)
                                                || (purchaseDate >= x.StartDate && purchaseDate < x.EndDate)));

                    if (rmCurrentPromotionPlanDetailInfos.Any())
                    {
                        AccumulatePurchasingPromotionLimit(rmCurrentPromotionPlanDetailInfos, puchaseProductUnlimitedLimitPromotions, ref currentPromotionAccumulateType, deprovisionList);
                    }
                    else
                    {
                        if (Log.IsDebugEnabled)
                            Log.DebugFormat("No purchase products promotions that start in purchaseDate range by product id ({0})", product.Item1.Id);
                    }

                    #endregion
                }
                #endregion
            }

            #endregion

            #region Check if reach the limit
            #region check current cycle
            foreach (var keypair in currentPromotionAccumulateType)
            {
                var usedPuchaseProductLimitPromotions = puchaseProductLimitPromotions.FirstOrDefault(
                 x => int.Parse(x.BAK1) == keypair.Key);
                if (usedPuchaseProductLimitPromotions != null)
                {
                    var limitAllowed = decimal.Parse(usedPuchaseProductLimitPromotions.Value);

                    // Check limits
                    if (keypair.Value > limitAllowed)
                    {
                        isLimitReached = true;
                        break;
                    }
                }
            }
            if (isLimitReached) return;
            #endregion
            #region check future cycle 
            foreach (var keypair in futurePromotionAccumulateType)
            {
                var usedPuchaseProductLimitPromotions = puchaseProductLimitPromotions.FirstOrDefault(
                 x => int.Parse(x.BAK1) == keypair.Key);
                if (usedPuchaseProductLimitPromotions != null)
                {
                    var limitAllowed = decimal.Parse(usedPuchaseProductLimitPromotions.Value);

                    // Check limits
                    if (keypair.Value > limitAllowed)
                    {
                        isLimitReached = true;
                        break;
                    }
                }
            }
            if (isLimitReached) return;
            #endregion
            #endregion


            foreach (var product in ListTuplesPurchaseProducts)
            {
                #region check if is credit enough
                if (!isLimitReached)
                {
                    foreach (var charge in product.Item2.Charges)
                    {
                        var chargePrice = getPrice(charge, purchaseDate);
                        var taxPercentage = getTaxRate(taxDefinition, purchaseDate).Percentage;

                        var amount = chargePrice.Amount;
                        var taxAmount = amount * (taxPercentage / 100);

                        accumulateAmount -= (amount + taxAmount);
                        if (accumulateAmount < 0)
                        {
                            isCreditEnough = false;
                            return;
                        }
                    }
                }
                #endregion

            }
            
            amountRequired = accumulateAmount - creditLimit;
        }

        /// <summary>
        /// Return a list of promotionPlanDetailIds from customer that has  commercial type
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private IEnumerable<int> GetAllCustomerPromotionPlanDetailIdsWithCommercialProducts(CustomerInfo customer)
        {
            List<ProductOffering> customerCommercialProducts = new List<ProductOffering>();
            var customerPromotionPlanDetailIdWithCommercialProducts = new List<int>();

            var customerProductsIds = customer.RevenueProductsInfo.Select(x => x.ProductOffering.Id);

            var getProductByProductIdMS = MicroServiceManager.GetMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();

            #region add products in customerComercialProducts lists that product.type is equal to CommercialProductTypeDescription value
            foreach (var customerProductsId in customerProductsIds)
            {
                var getProductByProductIdRequest = new GetProductOfferingByProductOfferingIdRequest() { ProductOfferingId = (int)customerProductsId };
                var GetProductByProductIdResponse = getProductByProductIdMS.Process(getProductByProductIdRequest, null);
                if (GetProductByProductIdResponse.ResultType == ResultTypes.Ok &&
                    GetProductByProductIdResponse.ProductOffering != null && GetProductByProductIdResponse.ProductOffering.OfferedProduct.Type.Description == CommercialProductTypeDescription)
                {
                    customerCommercialProducts.Add(GetProductByProductIdResponse.ProductOffering);
                }
            }
            #endregion

            #region Get all promotion planDetailId from all customerCommercialProducts
            foreach (var customerCommercialProduct in customerCommercialProducts)
            {
                if (customerCommercialProduct.OfferedProduct.AssociatedPrmotionPlan != null)
                {
                    var RmPromotionPlanDetailInfoList =
                        customerCommercialProduct.OfferedProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList;
                    customerPromotionPlanDetailIdWithCommercialProducts.AddRange(
                        RmPromotionPlanDetailInfoList.Select(
                            RmPromotionPlanDetailInfo => RmPromotionPlanDetailInfo.PromotionPlanDetailId));
                }
            }
            #endregion

            return customerPromotionPlanDetailIdWithCommercialProducts;
        }

        /// <summary>
        /// Check if promotion limit is reached wit specific list of promotion plan detail
        /// </summary>
        /// <param name="promotionPlanDetailInfos"></param>
        /// <param name="puchaseProductLimitPromotions"></param>
        ///  <param name="promotionAccumulateType"></param>
        /// <param name="deprovisionList"></param>
        /// <returns></returns>
        private bool CheckIfLimitIsReached(IEnumerable<RmPromotionPlanDetailInfo> promotionPlanDetailInfos, 
            IEnumerable<MVNOConfigActionInfo> puchaseProductLimitPromotions,
            ref Dictionary<int, decimal> promotionAccumulateType,
            List<ProductDependencyRelation> deprovisionList)
        {
            //check if Limit allowed is reached
            foreach (var promotionPlanDetailInfo in promotionPlanDetailInfos)
            {
                // Check if this promotion is about to be deprovisioned
                var promotionToBeDeprovisioned =deprovisionList.Any(
                    x =>
                         x.RelatedProduct.AssociatedPrmotionPlan != null && x.RelatedProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList.Any(
                            y => y.PromotionPlanDetailId == promotionPlanDetailInfo.PromotionPlanDetailId));
                if (promotionToBeDeprovisioned) continue;

                var usedPuchaseProductLimitPromotions = puchaseProductLimitPromotions.FirstOrDefault(
                    x => int.Parse(x.BAK1) == promotionPlanDetailInfo.SubServiceTypeId);
                if (usedPuchaseProductLimitPromotions != null)
                {
                    var limitAllowed = decimal.Parse(usedPuchaseProductLimitPromotions.Value);
                    promotionAccumulateType[promotionPlanDetailInfo.SubServiceTypeId] +=
                        promotionPlanDetailInfo.Limit;

                    // Check limits
                    if (promotionAccumulateType[promotionPlanDetailInfo.SubServiceTypeId] > limitAllowed)
                    {
                        return true;
                    }
                }
  
            }
            return false;
        }

        /// <summary>
        ///  Accumulate Creditlimit of all Customer's Promotion
        /// </summary>
        /// <param name="customerPromotions"></param>
        /// <param name="puchaseProductUnlimitedLimitPromotions"></param>
        /// <param name="promotionAccumulateType"></param>
        /// <param name="deprovisionList"></param>
        private void AccumulateCustomerPromotionCreditlimit(
            IEnumerable<CrmCustomersPromotionInfo> customerPromotions,
             IEnumerable<MVNOConfigActionInfo> puchaseProductUnlimitedLimitPromotions,
            ref Dictionary<int, decimal> promotionAccumulateType,
            List<ProductOfferingSpecificationOption> deprovisionList)
        {

            //check if Limit allowed is reached
            foreach (var cp in customerPromotions)
            {
                // Check if this promotion is about to be deprovisioned
                var promotionToBeDeprovisioned = deprovisionList.Any(
                    x =>
                         x.SpecifiedProductOffering.OfferedProduct.AssociatedPrmotionPlan != null && x.SpecifiedProductOffering.OfferedProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList.Any(
                            y => y.PromotionPlanDetailId == cp.PromotionDetail.PromotionPlanDetailId));
                if (promotionToBeDeprovisioned) continue;

                //exclude some configured promotions so that it won't be calculated 
                if (puchaseProductUnlimitedLimitPromotions != null && puchaseProductUnlimitedLimitPromotions.Any())
                {
                    var config  =puchaseProductUnlimitedLimitPromotions.FirstOrDefault(x => int.Parse(x.BAK1) == cp.PromotionDetail.SubServiceTypeId);
                    if (config != null)
                    {
                        if (!string.IsNullOrEmpty(config.Value) 
                            && config.Value.Split(new char[] {',', ';'}).Contains(cp.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId.ToString()))
                            continue;
                    }
                  
                }

                promotionAccumulateType[cp.PromotionDetail.SubServiceTypeId] += cp.CurrentLimit;
            }
        }
        /// <summary>
        /// Accumulate limit of purchasing promotion
        /// </summary>
        /// <param name="promotionPlanDetailInfos"></param>
        /// <param name="puchaseProductUnlimitedLimitPromotions"></param>
        /// <param name="promotionAccumulateType"></param>
        /// <param name="deprovisionList"></param>
        private void AccumulatePurchasingPromotionLimit(IEnumerable<RmPromotionPlanDetailInfo> promotionPlanDetailInfos,
           IEnumerable<MVNOConfigActionInfo> puchaseProductUnlimitedLimitPromotions,
            ref Dictionary<int, decimal> promotionAccumulateType,
            List<ProductOfferingSpecificationOption> deprovisionList)
        {
          
            foreach (var promotionPlanDetailInfo in promotionPlanDetailInfos)
            {
                // Check if this promotion is about to be deprovisioned
                var promotionToBeDeprovisioned = deprovisionList.Any(
                    x =>
                         x.SpecifiedProductOffering.OfferedProduct.AssociatedPrmotionPlan != null && x.SpecifiedProductOffering.OfferedProduct.AssociatedPrmotionPlan.RmPromotionPlanDetailInfoList.Any(
                            y => y.PromotionPlanDetailId == promotionPlanDetailInfo.PromotionPlanDetailId));
                if (promotionToBeDeprovisioned) continue;


                //exclude some configured promotions so that it won't be calculated 
                if (puchaseProductUnlimitedLimitPromotions != null && puchaseProductUnlimitedLimitPromotions.Any())
                {
                    var config = puchaseProductUnlimitedLimitPromotions.FirstOrDefault(x => int.Parse(x.BAK1) == promotionPlanDetailInfo.SubServiceTypeId);
                    if (config != null)
                    {
                        if (!string.IsNullOrEmpty(config.Value) &&
                            config.Value.Split(new char[] { ',', ';' }).Contains(promotionPlanDetailInfo.RmPromotionPlanInfo.PromotionPlanId.ToString()))
                            continue;
                    }
                }

               promotionAccumulateType[promotionPlanDetailInfo.SubServiceTypeId] +=promotionPlanDetailInfo.Limit;

            }
        }

        /// <summary>
        /// Get credit limit. Try to get PriortyBundle.CreditLimit from products to purchase if is not possible then get this information from customer products.
        /// </summary>
        /// <param name="forceCreditLimit"></param>
        /// <param name="productsToPurchase"></param>
        /// <param name="customerProducts"></param>
        /// <param name="customerInfo"></param>
        /// <param name="creditLimit"></param>
        /// <param name="unBilledBalance"></param>
        /// <returns></returns>
        private void GetCreditLimit(Decimal? forceCreditLimit, IEnumerable<ProductOffering> productsToPurchase, IEnumerable<ProductOffering> customerProducts, CustomerInfo customerInfo, out decimal creditLimit, out decimal unBilledBalance)
        {
            unBilledBalance = 0;
            creditLimit = 0;

            #region Get priorityBundle from customerProducts, then get creditlimit and unBilledBalance
            var getPriorityBundleInfoFromBundleInfosMs = MicroServiceManager.GetMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse>();
            GetPriorityBundleInfoFromBundleInfosResponse getPriorityBundleInfoFromBundleInfosResponse;
            var customerProductBundles = GetBundles(customerProducts);
            if (customerProductBundles.Any())
            {
                var getPriorityBundleInfoFromBundleInfosRequest = new GetPriorityBundleInfoFromBundleInfosRequest()
                {
                    BundleDefinitionList = customerProductBundles,
                };

                getPriorityBundleInfoFromBundleInfosResponse = getPriorityBundleInfoFromBundleInfosMs.Process(getPriorityBundleInfoFromBundleInfosRequest, null);

                if (getPriorityBundleInfoFromBundleInfosResponse.ResultType == ResultTypes.Ok && getPriorityBundleInfoFromBundleInfosResponse.PriorityBundle != null)
                {
                    //Get the unBilledBalance and creditlimit from the actual PriorityBundle service 
                    if (customerInfo.ServicesInfo.Any())
                    {
                        var serviceInfo = customerInfo.ServicesInfo.FirstOrDefault(x => x.BundleDefinition.BundleID == getPriorityBundleInfoFromBundleInfosResponse.PriorityBundle.BundleID);
                        if (serviceInfo != null)
                        {
                            unBilledBalance = serviceInfo.UnBilledBalance.HasValue ? serviceInfo.UnBilledBalance.Value : 0;
                            creditLimit = serviceInfo.CreditLimit.HasValue ? serviceInfo.CreditLimit.Value : 0;
                        }
                    }
                   
                }

            }
            #endregion

            #region Get bundle credit limit or forcedCreditLimit
            if (forceCreditLimit.HasValue)
            {
                creditLimit =  forceCreditLimit.Value;
            }
            else
            {
                #region Try to get PriorityBundle from products to purchase
                var bundles = GetBundles(productsToPurchase);
                if (bundles.Any())
                {
                    var getPriorityBundleInfoFromBundleInfosRequest = new GetPriorityBundleInfoFromBundleInfosRequest()
                    {
                        BundleDefinitionList = bundles,
                    };

                    getPriorityBundleInfoFromBundleInfosResponse = getPriorityBundleInfoFromBundleInfosMs.Process(getPriorityBundleInfoFromBundleInfosRequest, null);

                    if (getPriorityBundleInfoFromBundleInfosResponse.ResultType == ResultTypes.Ok && getPriorityBundleInfoFromBundleInfosResponse.PriorityBundle != null)
                    {
                        //it's new priority bundle, get the credit limit without cheking customer service limit
                        unBilledBalance = 0;
                        creditLimit = getPriorityBundleInfoFromBundleInfosResponse.PriorityBundle.CreditLimit.HasValue
                            ? getPriorityBundleInfoFromBundleInfosResponse.PriorityBundle.CreditLimit.Value 
                            : 0;
                    }
                }
                #endregion

            }
            #endregion
        }



        /// <summary>
        /// Get a list of bundles 
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        private List<BundleInfo> GetBundles(IEnumerable<ProductOffering> products)
        {
            return products.Select(x => x.OfferedProduct).Where(x => x.AssociatedBundle != null).Select(product => product.AssociatedBundle).ToList();
        }

    }

    

}
