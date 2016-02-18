using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using com.etak.core.bizops.assurance.ApplyChargeAndSchedule;
using com.etak.core.bizops.fullfilment.ApplyCustomerPromotion;
using com.etak.core.bizops.fullfilment.AssignProductToCustomer;
using com.etak.core.bizops.fullfilment.CancelCustomerProduct;
using com.etak.core.bizops.fullfilment.CheckPurchaseProductForCustomer;
using com.etak.core.bizops.helper;
using com.etak.core.bizops.revenue.ChargeService;
using com.etak.core.bizops.revenue.ChargeService.Proxy;
using com.etak.core.customer.message.AddChargeToCustomer;
using com.etak.core.customer.message.CreateCustomerChargeSchedule;
using com.etak.core.customer.message.GetAccountById;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.customer.message.GetInvoiceById;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using com.etak.core.customer.message.UpdateCustomerChargeSchedule;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByIdAndName;
using com.etak.core.GSMSubscription.messages.CancelCustomerProductAssignment;
using com.etak.core.GSMSubscription.messages.CreateCustomerProductAssignment;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerId;
using com.etak.core.microservices.messages.GetTaxAuthority;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.product.message.CreateProductInfo;
using com.etak.core.product.message.GetRmPromotionPlanInfosByIds;
using com.etak.core.service.messages.AddUnbilledBalance;
using com.etak.core.service.messages.CreateServicesInfo;
using com.etak.core.service.messages.CustomerHasCredit;
using com.etak.core.service.messages.GetPriorityBundleInfoFromBundleInfos;
using log4net;

namespace com.etak.core.bizops.fullfilment.PurchaseProductForCustomer
{

    /// <summary>
    /// PurchaseProductForCustomer BizOp
    /// </summary>
    public class PurchaseProductForCustomerBizOp : AbstractSinglePhaseOrderProcessor<PurchaseProductForCustomerRequestDTO, PurchaseProductForCustomerResponseDTO
        , PurchaseProductForCustomerRequestInternal, PurchaseProductForCustomerResponseInternal, PurchaseProductForCustomerOrder>
    {

        #region Ini Var

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private PurchaseProductForCustomerConfiguration config = null;

        /// <summary>
        /// IApplyRecurringChargeInterface
        /// </summary>
        protected IApplyRecurringChargeInterface _applyRecurringCharge = null;

        /// <summary>
        /// PurchaseHelper auxiliar
        /// </summary>
        protected PurchaseHelper _purchaseHelper = null;


        private decimal proratedLimit = 1;
        private List<ProductOffering> deprovisionedProducts = new List<ProductOffering>();
        private List<ServicesInfo> serviceInfoList = new List<ServicesInfo>();
        private List<ProductInfo> packageForCustomerList = new List<ProductInfo>();
        private bool hasNextBillRunDate=false;


        #region Protected var


        /// <summary>
        /// currentAcount of customer
        /// </summary>
        protected Account currentAccount = null;

        /// <summary>
        /// NexBillrunDate calculation
        /// </summary>
        protected DateTime nextBillRunDate = DateTime.Now;

        /// <summary>
        /// List of customerProuctAssignments
        /// </summary>
        protected List<CustomerProductAssignment> customerProductAssignmentList = new List<CustomerProductAssignment>();

        /// <summary>
        /// true if called by Register or PortIn
        /// </summary>
        protected bool isExternalCalling;


        #endregion



        #endregion

        #region Constructors


        /// <summary>
        /// purchase Product List
        /// </summary>
        public PurchaseProductForCustomerBizOp()
        {
            _applyRecurringCharge = new IApplyRecurringCharge();
            _purchaseHelper = new PurchaseHelper();

        }



        /// <summary>
        /// COnstructor for Dependency Injection
        /// </summary>
        /// <param name="applyRecurringCharge"></param>
        /// <param name="purchaseHelperParam"></param>
        public PurchaseProductForCustomerBizOp(
            IApplyRecurringChargeInterface applyRecurringCharge
            , PurchaseHelper purchaseHelperParam)
        {
            _applyRecurringCharge = applyRecurringCharge;
            _purchaseHelper = purchaseHelperParam;
        }

        #endregion

        #region Create MicroServices


        private readonly IMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse> _getActiveCusotmerAccountMs = MicroServiceManager.GetMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
        private readonly IMicroService<CreateProductInfoRequest, CreateProductInfoResponse> _createProductInfotMs = MicroServiceManager.GetMicroService<CreateProductInfoRequest, CreateProductInfoResponse>();
        private readonly IMicroService<CreateCustomerChargeScheduleRequest, CreateCustomerChargeScheduleResponse> _createCustomerChargeSchedule = MicroServiceManager.GetMicroService<CreateCustomerChargeScheduleRequest, CreateCustomerChargeScheduleResponse>();
        private readonly IMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse> _getInvoiceByLegalInvoiceNumber = MicroServiceManager.GetMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();
        private readonly IMicroService<GetMVNOConfigActionInfosByIdAndNameRequest, GetMVNOConfigActionInfosByIdAndNameResponse> _getMvnoConfigActionInfosByIdAndNameMs = MicroServiceManager.GetMicroService<GetMVNOConfigActionInfosByIdAndNameRequest, GetMVNOConfigActionInfosByIdAndNameResponse>();
        private readonly IMicroService<CreateServicesInfoRequest, CreateServicesInfoResponse> _createServiceMs = MicroServiceManager.GetMicroService<CreateServicesInfoRequest, CreateServicesInfoResponse>();
        private readonly IMicroService<GetTaxAuthorityRequest, GetTaxAuthorityResponse> _getTaxAuthorityMs = MicroServiceManager.GetMicroService<GetTaxAuthorityRequest, GetTaxAuthorityResponse>();
        private readonly IMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse> _calculateNextBillrunDateMs = MicroServiceManager.GetMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
        private readonly IMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse> _getPriorityBundleMs = MicroServiceManager.GetMicroService<GetPriorityBundleInfoFromBundleInfosRequest, GetPriorityBundleInfoFromBundleInfosResponse>();
        private readonly IMicroService<GetRmPromotionPlanInfosByIdsRequest, GetRmPromotionPlanInfosByIdsResponse> _getRmPromotionPlanInfoMs = MicroServiceManager.GetMicroService<GetRmPromotionPlanInfosByIdsRequest, GetRmPromotionPlanInfosByIdsResponse>();
        private readonly IMicroService<UpdateCustomerChargeScheduleRequest, UpdateCustomerChargeScheduleResponse> _updateCustomerChargeSchedule = MicroServiceManager.GetMicroService<UpdateCustomerChargeScheduleRequest, UpdateCustomerChargeScheduleResponse>();
        private readonly IMicroService<AddChargeToCustomerRequest, AddChargeToCustomerResponse> addChargeMS = MicroServiceManager.GetMicroService<AddChargeToCustomerRequest, AddChargeToCustomerResponse>();
        private readonly IMicroService<CustomerHasCreditRequest, CustomerHasCreditResponse> hasCreditMS = MicroServiceManager.GetMicroService<CustomerHasCreditRequest, CustomerHasCreditResponse>();
        private readonly IMicroService<AddUnbilledBalanceRequest, AddUnbilledBalanceResponse> addUnbilledBalanceMS = MicroServiceManager.GetMicroService<AddUnbilledBalanceRequest, AddUnbilledBalanceResponse>();
        #endregion

        #region Classes for internal operations



        private class AddServiceToCustomerListRequest
        {
            /// <summary>
            /// Optional credit Limit
            /// </summary>
            public Decimal? forceCreditLimit { get; set; }

            /// <summary>
            /// CustomerInfo
            /// </summary>
            public CustomerInfo CustomerDefinition { get; set; }

            /// <summary>
            /// BundleInfo list 
            /// </summary>
            public IList<BundleInfo> BundleDefinitionList { get; set; }

            /// <summary>
            /// Start date for each Bundle
            /// </summary>
            public DateTime? StartDate { get; set; }

            /// <summary>
            /// Logininfo
            /// </summary>
            public LoginInfo LoginInfo { get; set; }
        }

        /// <summary>
        /// Used internally private methods
        /// </summary>
        private class AddServiceToCustomerListResponse
        {
            /// <summary>
            /// ServicesList
            /// </summary>
            public List<ServicesInfo> ServicesList { get; set; }
        }



        #endregion Classes for internal operations

        #region BizOp Implemetation

        /// <summary>
        /// MapNotAutomappedOrderInboundProperties
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedOrderInboundProperties(PurchaseProductForCustomerRequestDTO request, ref PurchaseProductForCustomerRequestInternal coreInput)
        {
            #region Microservices
            var microServiceCheckAuthorization = MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            #endregion

            #region Validate Customer
            if (coreInput.Customer == null)
            {
                throw new DataValidationErrorException("Customer does not exist", BizOpsErrors.CustomerIsNull);
            }
            #endregion

            #region Products List


            Log.Debug("MapNotAutomappedOrderInboundProperties Dictionary Flattened Products");
            coreInput.listTuplePoducts = _purchaseHelper.GetProductsAndChargesOptions(request.products.ToList());

            #endregion

            #region Check if the products and customer corresponds to the actual MVNO
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Customer.DealerID.HasValue ? coreInput.Customer.DealerID.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);

            var listOfMvnos = coreInput.listTuplePoducts.Select(x => x.Item1.OfferedProduct.VMO.DealerID.HasValue ? x.Item1.OfferedProduct.VMO.DealerID.Value : 0).Distinct();
            foreach (var mvno in listOfMvnos)
            {
                checkAuthorizationRequest = new CheckAuthorizationRequest()
                {
                    UserInfo = coreInput.User,
                    DealerId = mvno,
                };
                checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
                if (!checkAuthorizationResponse.IsAuthorized)
                    throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            }

            #endregion

            #region Simple prop

            Log.Debug("MapNotAutomappedOrderInboundProperties Simple Prop");
            var dateNow = DateTime.Now;
            var date = String.Format("{0:s}", dateNow);
            var newDate = DateTime.Parse(date);
            coreInput.DatetimePurchase = newDate;
            coreInput.ForceCreditLimit = request.forceCreditLimit;
            coreInput.TypeOfPurchaseProductOperation = TypeOfPurchaseProduct.PurchaseProduct;

            #endregion


        }


        /// <summary>
        /// MapNotAutomappedOrderOutboundProperties
        /// </summary>
        /// <param name="source"></param>
        /// <param name="coreOutput"></param>

        protected override void MapNotAutomappedOrderOutboundProperties(PurchaseProductForCustomerResponseInternal source, ref PurchaseProductForCustomerResponseDTO coreOutput)
        {
            Log.Debug("MapNotAutomappedOrderOutboundProperties Product to DTO");
            coreOutput.productPurchase = new List<CustomerProductAssignmentDTO>();
            if (source.productPurchaseList != null)
            {
                foreach (CustomerProductAssignment product in source.productPurchaseList)
                {
                    Log.DebugFormat("ToDto Product: {0}", product.Id);
                    coreOutput.productPurchase.Add(product.ToDto());
                }
            }

            Log.Debug("MapNotAutomappedOrderOutboundProperties DeprovisonedProduct to DTO");
            coreOutput.deprovisionedProductList = new List<ProductCatalogDTO>();
            if (source.DeprovisionedProductList != null)
            {
                foreach (ProductOffering offering in source.DeprovisionedProductList)
                {
                    Log.DebugFormat("ToDto DeprovisionedProduct: {0}", offering.Id);
                    coreOutput.deprovisionedProductList.Add(offering.ToDto());
                }
            }
        }

        /// <summary>
        /// ProcessRequest
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override PurchaseProductForCustomerResponseInternal ProcessRequest(PurchaseProductForCustomerOrder order, PurchaseProductForCustomerRequestInternal request)
        {
            #region Validate Customer and Resources
            ResourceMBInfo resourceMBInfo = null;
            var customerPropertyInfo = new com.etak.core.model.PropertyInfo();
            if (request.Customer.StatusID == null)
            {
                customerPropertyInfo = request.Customer.PropertyInfo.FirstOrDefault();
                if (customerPropertyInfo.PendingStatus == (int)PendingStatus.Terminated)
                    throw new DataValidationErrorException(string.Format("Cannot be proceed, CustomerId {0} is terminated", request.Customer.CustomerID), BizOpsErrors.CustomerTerminated);
                else if (customerPropertyInfo.PendingStatus == (int)PendingStatus.Rejected)
                    throw new DataValidationErrorException(string.Format("Cannot be proceed, CustomerId {0} is rejected", request.Customer.CustomerID), BizOpsErrors.CustomerRejected);
            }
            else
            {
                if (request.Customer.StatusID == (int)CustomerStatus.Deleted)
                    throw new DataValidationErrorException(string.Format("Cannot be proceed, CustomerId {0} has been deleted", request.Customer.CustomerID), BizOpsErrors.CustomerIdDeleted);
            }

            if (!request.WithoutSubscription)
            {
                //checking customer active resources
                if (request.Customer.ResourceMBInfo != null)
                    resourceMBInfo =
                        request.Customer.ResourceMBInfo.Where(x => x.StatusID == (int) ResourceStatus.Active)
                            .FirstOrDefault();
                if (resourceMBInfo == null)
                    throw new DataValidationErrorException(string.Format("Customer doesn't have Active Resource"),
                        BizOpsErrors.CustomerDoesNotHaveActiveResource);
            }

            #endregion

            #region BizOP Manager

            var checkPurchaseBizOp =
               BusinessOperationManager
                   .GetCoreBusinessOperation
                   <CheckPurchaseProductForCustomerRequestInternal, CheckPurchaseProductForCustomerResponseInternal>
                   ((int)request.MVNO.DealerID);

            var applyChargeAndScheduleBizOp =
                BusinessOperationManager.GetCoreBusinessOperation<ApplyChargeAndScheduleRequest, ApplyChargeAndScheduleResponse>((int)request.MVNO.DealerID);

            var chargeServiceBizOp = BusinessOperationManager.GetCoreBusinessOperation<ChargeServiceRequestInternal, ChargeServiceResponseInternal>((int)request.MVNO.DealerID);

            var cancelCustomerProductBizOp = BusinessOperationManager.GetCoreBusinessOperation<CancelCustomerProductRequestInternal, CancelCustomerProductResponseInternal>((int)request.MVNO.DealerID);

            var applyCustomerPromotion =
                BusinessOperationManager
                    .GetCoreBusinessOperation
                    <ApplyCustomerPromotionRequestInternal, ApplyCustomerPromotionResponseInternal>(
                        (int)request.MVNO.DealerID);

            var assignProductToCustomerBizOP = BusinessOperationManager.GetCoreBusinessOperation<AssignProductOfferingToCustomerRequestInternal, AssignProductOfferingToCustomerResponseInternal>((int)request.MVNO.DealerID);



            #endregion

            #region variables

            PurchaseProductForCustomerResponseInternal response = new PurchaseProductForCustomerResponseInternal();

            CheckPurchaseProductForCustomerResponseInternal respCheckPurchaseBizOp;


            CancelCustomerProductResponseInternal respCancelCustomer = new CancelCustomerProductResponseInternal();
            GetActiveCustomerAccountAssociationByDateResponse getCustomerAccountResponse = new GetActiveCustomerAccountAssociationByDateResponse();
            CalculateNextBillRunDateForBillCycleResponse calculateNextBillRunDateResponse = new CalculateNextBillRunDateForBillCycleResponse();
            AddServiceToCustomerListResponse addServiceToCustomerResponse = new AddServiceToCustomerListResponse();
            ApplyChargeAndScheduleResponse applyChargeAnsScheduleResponse = new ApplyChargeAndScheduleResponse();
            CreateCustomerChargeScheduleResponse createCustomerChargeAndcheduleResponse = new CreateCustomerChargeScheduleResponse();
            CreateProductInfoResponse assignPackageForCustomerResponse = new CreateProductInfoResponse();
            ApplyCustomerPromotionResponseInternal applyCusotmerPromotionResponse = new ApplyCustomerPromotionResponseInternal();
            UpdateCustomerChargeScheduleResponse updateCusotmerChargeAndScheduleResponse = new UpdateCustomerChargeScheduleResponse();

            AssignProductOfferingToCustomerResponseInternal assignProductToCustomerResponse = new AssignProductOfferingToCustomerResponseInternal();


            isExternalCalling = PurchaseHelper.IsNotCallingPurchaseProductType(request.TypeOfPurchaseProductOperation);


            TaxDefinition currentTaxDefinition =
                _getTaxAuthorityMs.Process(
                    new GetTaxAuthorityRequest() { Customer = request.Customer, MVNO = request.MVNO }, null)
                    .TaxDefinitions.FirstOrDefault();

            #endregion

            #region BizOp Configuration

            Log.Debug("GetOperationConfigForDealer");
            config = GetOperationConfigForDealer<PurchaseProductForCustomerConfiguration>(request.MVNO);


            #endregion

            #region Get CustomerAccount

            #region Logic upon TypeOfPurchaseProduct

            if (request.TypeOfPurchaseProductOperation == TypeOfPurchaseProduct.PurchaseProduct)
            {
                #region Type PurchaseProduct

                Log.Debug("GetCustomerAccount");
                getCustomerAccountResponse = _getActiveCusotmerAccountMs.Process(new GetActiveCustomerAccountAssociationByDateRequest()
                {
                    CustomerInfo = request.Customer,
                    ActiveCustomerAccountAssociationDate = (DateTime)request.DatetimePurchase
                }, null);
                if (getCustomerAccountResponse.ResultType != ResultTypes.Ok)
                    throw new BusinessLogicErrorException("GetActiveCustomerAccountAssociationByDate: Failed", BizOpsErrors.CustomerAccountNotFound);


                if (getCustomerAccountResponse.CustomerAccountAssociation == null)
                {
                    throw new BusinessLogicErrorException(String.Format("Unable to find an CustomerAccountAssociation for the CustomerID:{0}",
                        request.Customer.CustomerID), BizOpsErrors.CustomerAccountAssociationNotFound);
                }
                else
                {
                    Log.DebugFormat("Response GetCustomerAccount: {0}", getCustomerAccountResponse.CustomerAccountAssociation.Id);
                    currentAccount = getCustomerAccountResponse.CustomerAccountAssociation.Account;
                }

                #endregion
            }
            else
            {
                #region Other types

                // Use de Account passed in the request
                currentAccount = request.AccountDefinition;

                #endregion
            }

            #endregion

            if (currentAccount == null)
                throw new BusinessLogicErrorException(String.Format("Unable to find an Account for the CustomerID:{0}", request.Customer.CustomerID), BizOpsErrors.CustomerAccountNotFound);

            #endregion Get CustomerAccount

            #region Calculate Next BillRunDate

            if (currentAccount.BillingCycle != null)
            {

                Log.Debug("Calculate Next BillRunDate");
                calculateNextBillRunDateResponse = _calculateNextBillrunDateMs.Process(new CalculateNextBillRunDateForBillCycleRequest()
                {
                    BillCycleDefinition = currentAccount.BillingCycle,
                    PurchaseTime = (DateTime)request.DatetimePurchase,
                    FirstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek
                }, null);
                if (calculateNextBillRunDateResponse.ResultType != ResultTypes.Ok)
                    throw new BusinessLogicErrorException("CalculateNextBillRunDateForBillCycle: Failed", BizOpsErrors.CalculateNextBillrunDateError);

                nextBillRunDate = calculateNextBillRunDateResponse.NextBillRun;
                hasNextBillRunDate = true;
                Log.DebugFormat("Response Calculate Next BillRunDate: {0}", nextBillRunDate);
            }

            #endregion Calculate Next BillRunDate

            #region Check product avaialable & product comaptible with itself & product compatible with actual customer Products


            Log.Debug("CheckPurchaseProductForCustomer: products to purchase:");
            foreach (Tuple<ProductOffering, ProductChargeOption> pairProduct in request.listTuplePoducts)
            {
                Log.DebugFormat(",Product:{0} - ChargeOption:{1}", pairProduct.Item1, pairProduct.Item2);
            }




            // Call BizOp
            respCheckPurchaseBizOp = checkPurchaseBizOp.Process(new CheckPurchaseProductForCustomerRequestInternal()
            {
                Customer = request.Customer,
                ForceCreditLimit = request.ForceCreditLimit,
                ListTuplesPurchaseProducts = request.listTuplePoducts,
                PuchaseProductLimitPromotions = _getMvnoConfigActionInfosByIdAndNameMs.Process(new GetMVNOConfigActionInfosByIdAndNameRequest()
                {
                    MvnoId = (int)request.MVNO.DealerID,
                    CategoryName = config.CategoryMVNOConfigForPromotionLimit,
                    StatusId = 1
                }, null).MvnoConfigActionInfos,
                PuchaseProductUnLimitedPromotions = _getMvnoConfigActionInfosByIdAndNameMs.Process(new GetMVNOConfigActionInfosByIdAndNameRequest()
                {
                    MvnoId = (int)request.MVNO.DealerID,
                    CategoryName = config.UnlimitedPromotionForPurchaseProduct,
                    StatusId = 1
                }, null).MvnoConfigActionInfos,
                PurchaseDate = (DateTime)request.DatetimePurchase,
                TaxDefinition = currentTaxDefinition,
                NextBillRunDate = nextBillRunDate,
                TypeOfPurchaseProductOperation = request.TypeOfPurchaseProductOperation,
                MVNO = request.MVNO,
                User = request.User,
                Channel = request.Channel

            }, null);

            if (respCheckPurchaseBizOp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("CheckPurchaseProductForCustomer: Failed", BizOpsErrors.CheckPurchaseProductsFailed);

            if (!respCheckPurchaseBizOp.IsCreditEnough)
                throw new BusinessLogicErrorException("CheckPurchaseProductForCustomer: Credit is not enough", BizOpsErrors.CreditNotEnough);

            if (request.TypeOfPurchaseProductOperation != TypeOfPurchaseProduct.BenefitTransfer)
            {
                if (respCheckPurchaseBizOp.IsLimitReached)
                    throw new BusinessLogicErrorException("CheckPurchaseProductForCustomer: Promotion Limit reached", BizOpsErrors.PromotionLimitReached);
            }


            #endregion

            #region Process Deprovisioning

            if (respCheckPurchaseBizOp.DeprovisionList != null && respCheckPurchaseBizOp.DeprovisionList.Count > 0)
            {

                Log.Debug("Deprovisioning Products:");
                foreach (ProductOfferingSpecificationOption item in respCheckPurchaseBizOp.DeprovisionList)
                {
                    Log.DebugFormat("ProductOffering: {0}, Product: {1}", item.SpecifiedProductOffering.Id, item.SpecifiedProductOffering.OfferedProduct.Id);

                    // Get customerProductAssignment neede for the BizOp
                    CustomerProductAssignment customerProductAssign =
                        request.Customer.RevenueProductsInfo.FirstOrDefault(x => x.ProductOffering.Id == item.SpecifiedProductOffering.Id && x.EndDate == null);
                    if (customerProductAssign.IsNotNull())
                    {
                        //If nothing has to be cancelled
                        respCancelCustomer = cancelCustomerProductBizOp.Process(new CancelCustomerProductRequestInternal()
                        {
                            CustomerProductAssignment = customerProductAssign,
                            CancelDate = DateTime.Now,
                            NextBillRunDate = nextBillRunDate,
                            UseNextBillCycleEndDateInRecurring = true,
                            User = request.User,
                            MVNO = request.MVNO,
                            Channel = request.Channel
                        },
                            null);
                        if (respCancelCustomer.ResultType != ResultTypes.Ok)
                            throw new BusinessLogicErrorException("CheckPurchaseProductForCustomer: Failed", BizOpsErrors.CheckPurchaseProductsFailed);
                        
                        deprovisionedProducts.Add(item.SpecifiedProductOffering);
                    }

                }

            }

            #endregion

            #region Assign Bundle

            Log.Debug("Assign Bundle");
            if (request.listTuplePoducts.Any(x => x.Item1.OfferedProduct.AssociatedBundle != null))
            {
                List<BundleInfo> bundleInfoList = new List<BundleInfo>();
                foreach (Tuple<ProductOffering, ProductChargeOption> dicItem in request.listTuplePoducts.Where(x => x.Item1.OfferedProduct.AssociatedBundle != null))
                    bundleInfoList.Add(dicItem.Item1.OfferedProduct.AssociatedBundle);

                AddServiceToCustomerListRequest assignBundleForCustomerRequest = new AddServiceToCustomerListRequest()
                {
                    CustomerDefinition = request.Customer,
                    BundleDefinitionList = bundleInfoList,
                    forceCreditLimit = request.ForceCreditLimit,
                    StartDate = request.DatetimePurchase,
                    LoginInfo = request.User
                };

                addServiceToCustomerResponse = AddServiceToCustomerList(assignBundleForCustomerRequest);
                serviceInfoList.AddRange(addServiceToCustomerResponse.ServicesList);

                // Add services to the existing Custmer
                request.Customer.ServicesInfo.AddRange(serviceInfoList.ToArray());

                foreach (ServicesInfo info in addServiceToCustomerResponse.ServicesList)
                {
                    Log.DebugFormat("Bundle: {0}", info.ServiceID);
                }

            }

            #endregion Assign Bundle

            #region Loop Assign Products to Customer,calling Private BizOp

            Log.Debug("Loop Parent products to call PurchaseIndividualProduct Assign Product to Customer");

            // URL API Apply charges
            _applyRecurringCharge.Url = config.UrlAPIApplyCharges;
            var currentInvoice = isExternalCalling ? request.Invoice : getInvoiceByLegalInvoiceNumbe(request.Customer, false);

            DateTime? priceEffectiveDate = null;//Set it to null so far.

            foreach (Tuple<ProductOffering, ProductChargeOption> pc in request.listTuplePoducts)
            {
                #region Assign Product to Customer

                Log.Debug("Assign Product to Customer BizOp");

                AssignProductOfferingToCustomerRequestInternal assignProductRequest = new AssignProductOfferingToCustomerRequestInternal
                {
                    Customer = request.Customer,
                    CreateDate = DateTime.Now,
                    StartDate =
                            PurchaseHelper.GetStartDateByProductChargeOptionAndTypeOfPurchaseProduct(pc.Item2,
                                (DateTime)request.DatetimePurchase, nextBillRunDate,
                                request.TypeOfPurchaseProductOperation),
                    EndDate = null,
                    ProductOffering = pc,
                    MVNO = request.MVNO,
                    User = request.User,
                    Channel = request.Channel
                };


                assignProductToCustomerResponse =
                    assignProductToCustomerBizOP.Process(assignProductRequest, null);
                if (assignProductToCustomerResponse.ResultType != ResultTypes.Ok)
                    throw new BusinessLogicErrorException("CreateCustomerProductAssignment: Failed",
                        BizOpsErrors.CreateCustomerProductAssignmentError);

                Log.DebugFormat("Product: {0}", pc.Item1.Id);

                customerProductAssignmentList.Add(assignProductToCustomerResponse.productPurchased);



                #endregion Assign Product to Customer

                #region Apply Charges

                Log.DebugFormat("ChargeOption: {0}", pc.Item2.Id);
                if (pc.Item2 != null)
                {
                    foreach (Charge charge in pc.Item2.Charges)
                    {
                        #region ini variables

                        DateTime startDate = PurchaseHelper.GetStartDateByProductChargeOptionAndTypeOfPurchaseProduct(
                            pc.Item2,
                            (DateTime)request.DatetimePurchase,
                            nextBillRunDate,
                            request.TypeOfPurchaseProductOperation);

                        #endregion

                        #region Create charge

                        Log.Debug("Assign Charges");
                        if (request.TypeOfPurchaseProductOperation != TypeOfPurchaseProduct.BenefitTransfer)
                        {
                            var requestApplyChargeAndSchedule = new ApplyChargeAndScheduleRequest()
                            {
                                Account = currentAccount,
                                ChargeToAdd = charge,
                                CustomAmount = null,
                                Customer = request.Customer,
                                CustomerDealer = request.MVNO,
                                CustomerProductAssignment = assignProductToCustomerResponse.productPurchased,
                                CycleNumber = 0,
                                InvoiceOfCharge = currentInvoice,
                                PriceEffectiveDate = priceEffectiveDate,
                                Schedule = null,
                                StartDate = startDate,
                                TaxDefinition = currentTaxDefinition,
                                TypeOfCharges = ApplyChargeAndScheduleBizOp.TypeOfCharges.AllChargesAllowed,
                            MVNO = request.MVNO,
                            User = request.User,
                            Channel = request.Channel,
                            ExternalReference = request.ExternalReference
                            };

                            applyChargeAnsScheduleResponse = applyChargeAndScheduleBizOp.Process(requestApplyChargeAndSchedule, null);
                            if (applyChargeAnsScheduleResponse.ResultType != ResultTypes.Ok) throw new BusinessLogicErrorException("ApplyChargeAndSchedule: Failed", BizOpsErrors.ApplyChargeAndScheduleError);

                            if (applyChargeAnsScheduleResponse.ScheduleAdded != null) Log.DebugFormat("Charge Create: {0}", applyChargeAnsScheduleResponse.ScheduleAdded.Id);

                            if (applyChargeAnsScheduleResponse.ChargeAdde != null) Log.DebugFormat("Charge Create: {0}", applyChargeAnsScheduleResponse.ChargeAdde.Id);
                        }

                        #endregion

                        // Only if charge created is RECURRING charge AND is external call
                        if (isExternalCalling && applyChargeAnsScheduleResponse.ScheduleAdded != null)
                        {
                            Log.Debug("IsExternalCalling & charge is recurring");

                            #region External API Prorate charge will create new charges

                            ChargeServiceResponseInternal customersCharges = chargeServiceBizOp.Process(
                                new ChargeServiceRequestInternal()
                                {
                                    CustomerChargeSchedule = applyChargeAnsScheduleResponse.ScheduleAdded,
                                    datetimePurchase = (DateTime)request.DatetimePurchase,
                                    Invoice = currentInvoice,
                                    datetimePriceEffective = null,
                            Url = _applyRecurringCharge.Url,
                            MVNO = request.MVNO,
                            User = request.User,
                            Channel = request.Channel

                                },
                                null);
                            foreach (CustomerCharge customerCharge in customersCharges.CustomerCharges)
                            {
                                Log.DebugFormat("Charge Returned: {0}", customerCharge.Id);
                            }


                            #endregion

                            #region Ceate new charges returned by API

                            // External calling with purchase time today, normally will be Register
                            if (((DateTime)request.DatetimePurchase - DateTime.Now).TotalDays.Round() == 0)
                            {
                                #region case Register

                                Log.Debug("Case Register");
                                foreach (CustomerCharge chargeReturnedApi in customersCharges.CustomerCharges)
                                {

                                    var requestUpdateCustomerChargeAndSchedule = new UpdateCustomerChargeScheduleRequest()
                                    {
                                        CustomerChargeSchedule = chargeReturnedApi.Schedule,
                                        MVNO = request.MVNO,
                                        User = request.User
                                    };
                                    updateCusotmerChargeAndScheduleResponse = _updateCustomerChargeSchedule.Process(
                                        requestUpdateCustomerChargeAndSchedule,
                                        null);
                                    if (updateCusotmerChargeAndScheduleResponse.ResultType != ResultTypes.Ok)
                                        throw new BusinessLogicErrorException(
                                            "ApplyChargeAndSchedule for Prorated charges: Failed",
                                            BizOpsErrors.ApplyChargeAndScheduleError);

                                    Log.DebugFormat("CustomerCharge updated: {0}", chargeReturnedApi.Id);

                                    #region Create Charge for first period  on pro-rated

                                    Log.Info("Create Charge to customer for first period  on pro-rated");
                                    AddChargeToCustomerRequest chargeReq = new AddChargeToCustomerRequest
                                    {
                                        Amount = chargeReturnedApi.Amount,
                                        TaxToApply = currentTaxDefinition,
                                        AccountToBeCharged = currentAccount,
                                        ChargeInCatalog = charge,
                                        ChargingDate = chargeReturnedApi.ChargingDate,
                                        CustomerProductAssingment = assignProductToCustomerResponse.productPurchased,
                                        CustomerToBeCharged = request.Customer,
                                        InvoceToBeCharged = chargeReturnedApi.Invoice,
                                        CycleNumber = chargeReturnedApi.CycleNumber,
                                        Schedule = chargeReturnedApi.Schedule,
                                        PeriodNumber = chargeReturnedApi.CycleNumber,
                                    };

                                    var chargeRes = addChargeMS.Process(chargeReq, null);
                                    if (chargeRes.ChargeCreated == null)
                                        throw new BusinessLogicErrorException(
                                            string.Format(
                                                "Create Charge to customer#{0} for the first period  on pro-rated failed!!!",
                                                request.Customer.CustomerID.Value),
                                            BizOpsErrors.ErrorBase);

                                    #endregion

                                    #region Check if Customer has credit

                                    var hasCreditReq = new CustomerHasCreditRequest()
                                    {
                                        Amount = chargeReturnedApi.Amount,
                                        DateOfCharge = startDate,
                                        CustomerInfo = request.Customer
                                    };

                                    var hasCreditResp = hasCreditMS.Process(hasCreditReq, null);

                                    if (!hasCreditResp.HasCredit) throw new BusinessLogicErrorException("The Customer doesn't have enough credit", BizOpsErrors.CreditNotEnough);

                                    #endregion

                                    #region Update Unbilled Balance

                                    var addUnbilledReq = new AddUnbilledBalanceRequest()
                                    {
                                        Amount = chargeReturnedApi.Amount,
                                        ServicesInfo = hasCreditResp.MasterBundle,
                                    };
                                    var addUnbilledResp = addUnbilledBalanceMS.Process(addUnbilledReq, null);
                                    if (addUnbilledResp.ResultType != ResultTypes.Ok)
                                        throw new BusinessLogicErrorException(
                                            string.Format(
                                                "Error updating the Unbilled Balance ({0}) with an amount of {1} for Customer {2}",
                                                hasCreditResp.MasterBundle.UnBilledBalance,
                                                chargeReturnedApi.Amount,
                                                request.Customer.CustomerID.Value),
                                            BizOpsErrors.ErrorUpdatingUnbilledBalance);

                                    #endregion

                                    // We take the factor using the Amount retuned by the API and the Amount configured
                                    ChargePrice chargePrice = getPrice(chargeReturnedApi.ChargeDefinition, (DateTime)request.DatetimePurchase);
                                    proratedLimit = chargeReturnedApi.Amount / chargePrice.Amount; //same value always
                                }



                                #endregion
                            }

                            // purchase time in the future, normally Case PortIn
                            else
                            {
                                #region case PortIn

                                Log.Debug("Case Register");
                                applyChargeAnsScheduleResponse.ScheduleAdded.PriceEffectiveDate = null;
                                applyChargeAnsScheduleResponse.ScheduleAdded.NextChargeDate = request.DatetimePurchase; // future datetime
                                applyChargeAnsScheduleResponse.ScheduleAdded.NextPeriodNumber = 1;
                                applyChargeAnsScheduleResponse.ScheduleAdded.CurrentCyclenumber = 0;
                                applyChargeAnsScheduleResponse.ScheduleAdded.Status = ScheduleChargeStatus.InProcess;

                                createCustomerChargeAndcheduleResponse = _createCustomerChargeSchedule.Process(
                                    new CreateCustomerChargeScheduleRequest()
                                    {
                                        CustomerChargeSchedule = applyChargeAnsScheduleResponse.ScheduleAdded
                                    },
                                    null);
                                if (createCustomerChargeAndcheduleResponse.ResultType != ResultTypes.Ok)
                                    throw new BusinessLogicErrorException(
                                        "CreateCustomerChargeSchedule: Failed",
                                        BizOpsErrors.CreateCustomerChargeAndScheduleError);

                                Log.DebugFormat("CustomerCharge created: {0}", applyChargeAnsScheduleResponse.ScheduleAdded.Id);

                                #endregion

                            }

                            #endregion
                        }
                    }
                }

                #endregion Apply Charges
            }

            #endregion

            #region Apply Packages

            Log.Debug("Apply Packages");
            foreach (Tuple<ProductOffering, ProductChargeOption> product in request.listTuplePoducts.Where(x => x.Item1.OfferedProduct.AssociatedPackage != null))
            {
                var productInfo = product.Item1.OfferedProduct;
                var package = productInfo.AssociatedPackage;
                CreateProductInfoRequest assignPackageForCustomerRequest = new CreateProductInfoRequest()
                {
                    ProductInfo = new ProductInfo()
                    {
                        CreateDate = request.DatetimePurchase,
                        CustomerInfo = request.Customer,
                        PackageDefinition = package,
                        ServiceInfo = serviceInfoList,
                        ServiceTypeID = package.ServiceTypeID,
                        CreditLimit = package.CreditLimit,
                        UserID = request.User.UserID,
                        SpecialCreditLimit = package.SpecialCreditLimit,
                        StartDate = request.DatetimePurchase
                    }
                };
                assignPackageForCustomerResponse = _createProductInfotMs.Process(assignPackageForCustomerRequest, null);
                packageForCustomerList.Add(assignPackageForCustomerResponse.ProductInfo);

                Log.DebugFormat("Package crested: {0}", product.Item1.Id);
            }

            #endregion Apply Packages

            #region Apply Promotion
            DateTime? applyCusotmerPromotionEndDate = null;

            Log.Debug("Apply Promotion");
            foreach (Tuple<ProductOffering, ProductChargeOption> product in request.listTuplePoducts.Where(x => x.Item1.OfferedProduct.AssociatedPrmotionPlan != null))
            {
                DateTime newStartDate = PurchaseHelper.GetStartDateByProductChargeOptionAndTypeOfPurchaseProduct(product.Item2, (DateTime)request.DatetimePurchase, nextBillRunDate, request.TypeOfPurchaseProductOperation);
                RmPromotionPlanInfo promotionPlanInfo = _getRmPromotionPlanInfoMs.Process(new GetRmPromotionPlanInfosByIdsRequest()
                {
                    PromotionPlanIds = new List<int>() { product.Item1.OfferedProduct.AssociatedPrmotionPlan.PromotionPlanId }
                }, null).RmPromotionPlanInfos.FirstOrDefault();

                applyCusotmerPromotionResponse = applyCustomerPromotion.Process(new ApplyCustomerPromotionRequestInternal()
                {
                    FactorToCreditLimit = (currentAccount.BillingCycle == null || !isExternalCalling || !PurchaseHelper.HasRecurringCharge(product.Item2) ? 1 : proratedLimit),
                    Customer = request.Customer,
                    StartDate = newStartDate,
                    EndDate = hasNextBillRunDate ? nextBillRunDate.AddSeconds(-1) : applyCusotmerPromotionEndDate,
                    PromotionPlans = new List<RmPromotionPlanInfo>() { promotionPlanInfo },
                    MVNO = request.MVNO,
                    User = request.User,
                    Channel = request.Channel,
                    ExternalReference = request.ExternalReference
                }, null);
                if (applyCusotmerPromotionResponse.ResultType != ResultTypes.Ok)
                    throw new BusinessLogicErrorException("ApplyCustomerPromotion: Failed", BizOpsErrors.ApplyCustomerPromotionError);
                Log.DebugFormat("Apply PromotionPlanInfoId: {0}", promotionPlanInfo.PromotionPlanId);
            }



            #endregion

            #region Response

            response.productPurchaseList = customerProductAssignmentList;
            response.DeprovisionedProductList = deprovisionedProducts;
            response.Subscription = resourceMBInfo;
            response.ResultType = ResultTypes.Ok;
            return response;

            #endregion

        }

        /// <summary>
        /// OperationCode
        /// </summary>
        public override string OperationCode
        {
            get { return (OperationCodes.PurchaseProductOperation); }
        }

        /// <summary>
        /// OperationDiscriminator
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return (OperationDiscriminators.PurchaseProductOperation); }
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// AddServiceToCustomerList
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private AddServiceToCustomerListResponse AddServiceToCustomerList(AddServiceToCustomerListRequest request)
        {

            AddServiceToCustomerListResponse response = new AddServiceToCustomerListResponse();
            List<BundleInfo> allCustomerBundles = new List<BundleInfo>();


            List<ServicesInfo> subServiceList = null;
            BundleInfo bundlePriority = null;
            int actualBaseBundle = 0;


            if (request.BundleDefinitionList != null)
            {
                subServiceList = new List<ServicesInfo>();


                // GetPriority bundel taking in account ALL bundles (new purchase + exisitng ones)
                foreach (BundleInfo info in request.BundleDefinitionList)
                {
                    allCustomerBundles.Add(info);
                }
                foreach (ServicesInfo service in request.CustomerDefinition.ServicesInfo)
                {
                    allCustomerBundles.Add(service.BundleDefinition);
                    actualBaseBundle = service.CREDITLIMITBASEBUNDLEID;
                }



                bundlePriority = _getPriorityBundleMs.Process(new GetPriorityBundleInfoFromBundleInfosRequest()
                {
                    BundleDefinitionList = allCustomerBundles
                }, null).PriorityBundle;


                // Check if different base bubdles can occur
                if (bundlePriority.BundleID != actualBaseBundle && actualBaseBundle != 0)
                {
                    throw new BusinessLogicErrorException("AddServiceToCustomerList: Failed, Multiple BaseBundle detected", BizOpsErrors.MultipleBaseBubdleInCustomerServices);
                }

                foreach (BundleInfo bundleInfo in request.BundleDefinitionList)
                {
                    ServicesInfo subService = new ServicesInfo()
                    {
                        BundleDefinition = bundleInfo,
                        CustomerInfo = request.CustomerDefinition,
                        StartDate = request.StartDate,
                        UserID = request.LoginInfo.UserID,
                        UnBilledBalance = 0.00M,
                        BilledBalance = 0,
                        InvoiceTemplateID = 1,  // Value forLegacy code 
                        CreateDate = DateTime.Now
                    };

                    if (bundlePriority.BundleID == bundleInfo.BundleID)
                    {
                        subService.CreditLimit = request.forceCreditLimit.HasValue ? request.forceCreditLimit : bundleInfo.CreditLimit;
                        subService.CREDITLIMITBASEBUNDLEID = bundleInfo.BundleID.Value;
                    }
                    else
                    {
                        subService.CreditLimit = 0;
                        subService.CREDITLIMITBASEBUNDLEID = bundlePriority.BundleID.Value;
                    }

                    subService = _createServiceMs.Process(new CreateServicesInfoRequest() { subService = subService }, null).ServicesInfos;
                    subServiceList.Add(subService);
                }
            }

            response.ServicesList = subServiceList;

            return response;
        }




        /// <summary>
        /// getPrice
        /// </summary>
        /// <param name="charge"></param>
        /// <param name="datePurchase"></param>
        /// <returns></returns>
        private ChargePrice getPrice(Charge charge, DateTime datePurchase)
        {
            var chargePrice =
                charge.Prices.OrderByDescending(x => x.StartDate).
                    FirstOrDefault(x => (x.EndDate == null && datePurchase >= x.StartDate) || (datePurchase >= x.StartDate && datePurchase <= x.EndDate));

            if (chargePrice == null)
                throw new InternalErrorException(String.Format("There is not a price for ChargeID {0} and the date:{1}", charge.Id, datePurchase.ToString()), BizOpsErrors.NoPriceForCharge);

            return chargePrice;
        }






        /// <summary>
        /// getInvoiceByLegalInvoiceNumbe
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="IsInvoiceMandatory"></param>
        /// <returns></returns>
        private Invoice getInvoiceByLegalInvoiceNumbe(CustomerInfo customerInfo, bool IsInvoiceMandatory)
        {

            Invoice invoice = null;
            string legalNumber = null;
            DateTime now = DateTime.Now;

            var varInvoice =
                _getInvoiceByLegalInvoiceNumber.Process(
                    new GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest()
                    {
                        CustomerId = customerInfo.CustomerID.Value,
                        LegalInvoiceNumber = legalNumber
                    }, null).Invoices;

            if (varInvoice != null)
                invoice =
                    Enumerable.ToList<Invoice>(varInvoice)
                        .FirstOrDefault(
                            x => (x.EndDate == null && now >= x.StartDate) || (now >= x.StartDate && now < x.EndDate));
            if (IsInvoiceMandatory && invoice == null)
                throw new BusinessLogicErrorException(
                    String.Format("Unable to find an Invoice by LegalInvoiceNumber for the CustomerID:{0}",
                        customerInfo.CustomerID), BizOpsErrors.InvoiceNotFound);

            return invoice;
        }








        #endregion
    }
}
