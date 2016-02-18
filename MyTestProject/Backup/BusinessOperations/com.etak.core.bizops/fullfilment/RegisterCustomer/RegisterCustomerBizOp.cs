//using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using com.etak.core.bizops.fullfilment.CreateCustomer;
using com.etak.core.bizops.fullfilment.PurchaseProductForCustomer;
using com.etak.core.bizops.helper;
using com.etak.core.customer.message.AssignCustomerInfoToAccount;
using com.etak.core.customer.message.CreateAccountCurrency;
using com.etak.core.customer.message.CreateInvoice;
using com.etak.core.customer.message.UpdateCustomerInfo;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.GSMSubscription.messages.CreateResourceMBInfo;
using com.etak.core.GSMSubscription.messages.GetProvisioningTemplateByProvisionName;
using com.etak.core.microservices.messages.GetMultiLingualDescriptionById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.product.message.GetBillCyclesByVMNO;
using com.etak.core.resource.msisdn.message.ActivateNumberMS;
using com.etak.core.resource.msisdn.message.GetDealerNumberByResource;
using com.etak.core.resource.msisdn.microservices;
using com.etak.core.resource.simCard.message.ActiveSimCard;
using log4net;
using NumberInfo = com.etak.core.model.NumberInfo;
using Product = com.etak.core.model.revenueManagement.Product;

namespace com.etak.core.bizops.fullfilment.RegisterCustomer
{
    /// <summary>
    /// Register Customer Business Operation. That bizOp will create a Customer, doing all the operations needed to
    /// provision the customer, create the resource, etc.
    /// </summary>
    public class RegisterCustomerBizOp : AbstractSinglePhaseOrderProcessor<RegisterCustomerRequestDTO, RegisterCustomerResponseDTO, 
                                                                           RegisterCustomerRequestInternal, RegisterCustomerResponseInternal, RegisterCustomerOrder>
                                        
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private String OrderNumber;
        /// <summary>
        /// PurchaseHelper instance
        /// </summary>
        protected PurchaseHelper purchaseHelper;

        /// <summary>
        /// Standard Constructor for the business operation
        /// </summary>
        public RegisterCustomerBizOp()
        {
            purchaseHelper = new PurchaseHelper();
        }

        /// <summary>
        /// Constructor passing the BusinessOperations to be mocked
        /// </summary>
        /// <param name="purchaseHelperParam"></param>
        public RegisterCustomerBizOp(PurchaseHelper purchaseHelperParam)
        {
            purchaseHelper = purchaseHelperParam;
        }

        /// <summary>
        /// Maps all the inboud properties of the request that are not mapped by the framework
        /// </summary>
        /// <param name="request">the request in ET DTO Form</param><param name="coreInput">the resquest partially mapped by the core and needs to be updated</param>
        protected override void MapNotAutomappedOrderInboundProperties(RegisterCustomerRequestDTO request, ref RegisterCustomerRequestInternal coreInput)
        {
            var getDealerMs = MicroServiceManager.GetMicroService<GetDealerNumberByResourceRequest, GetDealerNumberByResourceResponse>();
            var getDealerNumberRequest = new GetDealerNumberByResourceRequest {Msisdn = request.MSISDN};
            var getDealerNumberResponse = getDealerMs.Process(getDealerNumberRequest, null);
            var dealerId = getDealerNumberResponse.DealerNumberInfo.FirstOrDefault() != null
                ? getDealerNumberResponse.DealerNumberInfo.FirstOrDefault().DealerID
                : 0;
            #region checkAuthorization
            var microServiceCheckAuthorization =
                MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = dealerId != null ? dealerId.Value : 0 };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            #endregion

            logInfo("Converting the CustomerDTO to CustomerInfo");

            #region CustomerInfo
            CustomerInfo customerInfo = request.CustomerData.ToCore();
            #endregion

            #region Subscription dependant code
            //If WithoutSubscription is true, we don't need to check then.
            if (!request.WithoutSubscription)
            {
                logInfo("Check that Automapped fields are set");
                if (coreInput.NumberInPool == null)
                    throw new BusinessLogicErrorException(OrderNumber + "Automapped inbound property NumberInPool is null when registering msisdn " + request.MSISDN,
                                                          BizOpsErrors.NumberPropertyNotFound);

                if (coreInput.NumberInPool.NumberProperty.StatusID != (int)ResourceStatus.Init)
                    throw new BusinessLogicErrorException(OrderNumber + "The Number is not in Init Status", BizOpsErrors.NumberInWrongStatus);

                if (coreInput.SimCard == null)
                    throw new BusinessLogicErrorException(OrderNumber + "Automapped inbound property SimCard is null when registering IccId " + request.ICCID,
                                                          BizOpsErrors.NumberPropertyNotFound);

                logInfo("Get ProvisionTemplate");
                coreInput.ProvisionInfoDefinition = GetProvisionTemplate(request.HLRProfile, coreInput.MVNO);
            } 
            #endregion

            #region Convert the list of Purchased Products
            logInfo("Converting the list of Purchased Products (DTO) to Dictionary");
            coreInput.PurchasedProducts = purchaseHelper.GetProductsAndChargesOptions(request.PurchasedProducts.ToList());
            #endregion

            #region Get the BillCycle (if defined)
            if (request.BillCycleId.HasValue)
            {
                logInfo("Get BillCycle passed as a parameter...");
                BillCycle billCycle = GetBillCycle(coreInput.MVNO, request.BillCycleId.Value);

                coreInput.BillCycleForCustomer = billCycle;
            }
            #endregion
            
            #region Fill coreInput
            logInfo("Filling the CoreInput Request");
            coreInput.CustomerInfoDefinition = customerInfo;
            coreInput.CreditLimit = request.CreditLimit;
            coreInput.IsContain4GProduct = false;
            coreInput.WithoutSubscription = request.WithoutSubscription;

            #endregion

        }

        /// <summary>
        /// Maps all the outboud properties of the response that are not mapped by the framework
        /// </summary>
        /// <param name="source">the response of the core operation that needs to be mapped</param><param name="DTOOutput">the response of the operation in DTO format</param>
        protected override void MapNotAutomappedOrderOutboundProperties(RegisterCustomerResponseInternal source, ref RegisterCustomerResponseDTO DTOOutput)
        {

        }

        /// <summary>
        /// The implementation of IOrderProcessor
        /// </summary>
        /// <param name="order">The order to be processed</param><param name="request">The request to process</param>
        /// <returns>
        /// The result of the process
        /// </returns>
        public override RegisterCustomerResponseInternal ProcessRequest(RegisterCustomerOrder order, RegisterCustomerRequestInternal request)
        {
            #region BizOP Manager

            var purchaseProduct =
                BusinessOperationManager
                    .GetCoreBusinessOperation
                    <PurchaseProductForCustomerRequestInternal, PurchaseProductForCustomerResponseInternal>(
                        (int) request.MVNO.DealerID);

            var createCustomerBizOp =
                BusinessOperationManager
                    .GetCoreBusinessOperation<CreateCustomerRequestInternal, CreateCustomerResponseInternal>(
                        (int) request.MVNO.DealerID);
                

            #endregion

            OrderNumber = String.Format("Order {0} : ", order.Id);
            DateTime createDate = DateTime.Now;

            logInfo("Get Specific Configuration for the Operation");
            RegisterCustomerConfiguration specificConfig = GetOperationConfigForDealer<RegisterCustomerConfiguration>(request.MVNO);
            
            CustomerInfo customerToBeCreated = request.CustomerInfoDefinition;
            SIMCardInfo simCardInfo = request.SimCard;
            NumberInfo numberInfo = request.NumberInPool;
            DealerInfo mvno = request.MVNO;

            #region Create Customer
            logInfo("Calling BizOp Create Customer");
            String externId = customerToBeCreated.PropertyInfo == null
                ? ""
                : customerToBeCreated.PropertyInfo.FirstOrDefault().ExternalId;
            CreateCustomerRequestInternal createCustomerReq = new CreateCustomerRequestInternal()
            {
                CustomerToBeCreated = customerToBeCreated,
                ExternalCustomerID = externId,
                PendingStatus = DtoDictionaries.PendingStatusEnumToInt[PendingStatus.Active],
                LanguageId = specificConfig.LanguageId ?? request.User.LanguageID,
                MVNO = request.MVNO,
                User = request.User,
                Channel = request.Channel,
                ExternalReference = request.ExternalReference
            };

            var createCustomerResp = createCustomerBizOp.Process(createCustomerReq, null);
            if (createCustomerResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(OrderNumber + "Cannot Create the customer", BizOpsErrors.CustomerNotCreated);
            customerToBeCreated = createCustomerResp.Customer;

            #endregion


            #region Create ResourceMB and assign it to the Customer

            ResourceMBInfo resourceCreated = null;

            if (!request.WithoutSubscription)
            { 
                logInfo("Create Resource...");
                var createSubscriptionResp = CreateSubscription(customerToBeCreated, numberInfo, simCardInfo, createDate, request.User, request.MVNO, specificConfig,request.ProvisionInfoDefinition);
                if (createSubscriptionResp.ResultType != ResultTypes.Ok)
                    throw new BusinessLogicErrorException(string.Format("Cannot create the ResourceMB with MSISDN {0}", createSubscriptionResp.ResourceMBInfoObj.Resource), BizOpsErrors.ErrorCreatingSubscription);
                resourceCreated = createSubscriptionResp.ResourceMBInfoObj;

                logInfo(string.Format("Resource Created with Id {0}. Assigning it to the Customer", resourceCreated.ResourceID));
                customerToBeCreated.ResourceMBInfo = new List<ResourceMBInfo>() { createSubscriptionResp.ResourceMBInfoObj };
            }

            #endregion

            #region CreateAccount

            logInfo("Get the BillCycle for the Customer by configuration if it's not set");
            request.BillCycleForCustomer = request.BillCycleForCustomer ?? GetBillCycle(mvno, specificConfig.BillcycleId);
            logInfo("Create the Customer's Account");
            CreateAccountCurrencyResponse createAccountResp = CreateCustomerAccount(customerToBeCreated, request.BillCycleForCustomer, specificConfig);
            if (createAccountResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(OrderNumber + "Error creating the Customer Account", BizOpsErrors.ErrorCreatingcustomerAccount);
            Account customerAccount = createAccountResp.AccountCurrency;

            #endregion


            #region Assign Account to Customer


            logInfo("Assign the previous created Account to ASSN");
            AssignCustomerInfoToAccountResponse assignAccountASSNResp = CreateCustomerAccountAssn(customerToBeCreated, createAccountResp.AccountCurrency, createDate);
            if (assignAccountASSNResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(OrderNumber + "Error creating the Customer Account ASSN", BizOpsErrors.ErrorCreatingcustomerAccountASSN);


            #endregion

            #region Create Invoice
            logInfo("Create Customer's first Invoice to be passed to Purchase Product");
            var createInvoiceResp = CreateInvoice(customerToBeCreated,customerAccount, createDate, request.BillCycleForCustomer, specificConfig);
            if (createInvoiceResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(OrderNumber + "Cannot create invoice", BizOpsErrors.ErrorCreatingInvoice);
            Invoice invoiceCreated = createInvoiceResp.Invoice;
            #endregion

            #region Purchase Product
            PurchaseProductForCustomerRequestInternal purchaseProductReq = new PurchaseProductForCustomerRequestInternal()
            {
                AccountDefinition = customerAccount,
                Customer = customerToBeCreated,
                DatetimePurchase = createDate,
                ForceCreditLimit = request.CreditLimit,
                Invoice = invoiceCreated,
                TypeOfPurchaseProductOperation = TypeOfPurchaseProduct.Register,
                listTuplePoducts = new List<Tuple<ProductOffering, ProductChargeOption>>(request.PurchasedProducts),
                MVNO = request.MVNO,
                User = request.User,
                Channel = request.Channel,
                ExternalReference = request.ExternalReference,
                WithoutSubscription = request.WithoutSubscription
            };
            var purchaseResp = purchaseProduct.Process(purchaseProductReq, null);
            if (purchaseResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(OrderNumber + string.Format("Cannot Purchase Products in Register process for Msisdn {0}", request.NumberInPool.Resource), BizOpsErrors.ErrorPurchasingProducts);
            #endregion
            
            #region Update Customer with all the new entities and Activate Simcard and NumberInfo
            UpdateLifecycleStatuses(customerToBeCreated,simCardInfo, numberInfo, mvno, request.User, request.WithoutSubscription);
            #endregion

            //TODO We need to move the Provision part from CRM to here


            //TODO When the provisioning part is in CORE, we should enable this part of code
            #region Last Step... Send Registration Event
            //SendRegistrationEvent(customerToBeCreated,mvno, createDate);
            #endregion

            return new RegisterCustomerResponseInternal()
            {
                Customer = customerToBeCreated,
                Subscription = resourceCreated,
                Invoice = invoiceCreated,
                ResultType = ResultTypes.Ok,
            };

        }


        #region Operation Code and Discriminator
        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.RegisterCustomer; }
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.RegisterCustomer; }
        } 
        #endregion

        #region privates methods
        
        /// <summary>
        /// Create Informational Log 
        /// </summary>
        /// <param name="message"></param>
        private void logInfo(String message)
        {
            if(Log.IsDebugEnabled)
                Log.Info(OrderNumber + message);
        }

        /// <summary>
        /// Private function which will create a new object ResourceMBInfo, and create it calling 
        /// to CreateSubscription Micro Service.
        /// </summary>
        /// <param name="customerInfo">Customer Info to be associated</param>
        /// <param name="numberInfo">NumberInfo to be used to create the subscription</param>
        /// <param name="simCardInfo">Simcard to be associated</param>
        /// <param name="operationTime">Datetime to be set in all the properties</param>
        /// <param name="userInfo">LoginInfo in reference to the permission of the user</param>
        /// <param name="mvno">DealerInfo doing the call</param>
        /// <param name="config">Configuration based on the Dealer</param>
        /// <param name="HLRProvision">The provisioning template obtained</param>
        /// <returns></returns>
        private CreateResourceMBInfoResponse CreateSubscription(CustomerInfo customerInfo, NumberInfo numberInfo,
            SIMCardInfo simCardInfo, DateTime operationTime, LoginInfo userInfo, DealerInfo mvno, RegisterCustomerConfiguration config, CrmDefaultProvisionInfo HLRProvision)
        {
            var createSubscriptionMs = MicroServiceManager.GetMicroService<CreateResourceMBInfoRequest, CreateResourceMBInfoResponse>();
            logInfo("Fill all the values for ResourceMBInfo");
            ResourceMBInfo resourceMb = new ResourceMBInfo()
            {
                CustomerInfo = customerInfo,
                Resource = numberInfo.Resource,
                MsIsdnAlertInd = numberInfo.Resource,
                ICC = simCardInfo.ICCID,
                IMSI = simCardInfo.IMSI1,
                PUK = simCardInfo.PUK1,
                OperatorInfo = mvno,
                UserID = userInfo.UserID,
                StartDate = operationTime,
                CreateDate = operationTime,
                ChangeStatusDate = operationTime,
                ActiveDeadlineDate = null,

                #region Default Values (some of them using Specific Configuration)
                CBSubsoption = config.ResourceCBSuboption,
                CBPassword = config.ResourceCBPassword,
                CBWrongAttempts = config.ResourceCBWrongAttemps,
                TeleServiceList = config.ResourceTeleServiceList,
                BearerServiceList = config.ResourceBearerServiceList,

                Remarks = null,
                UssdAllowed = false,
                Calculation = 2,
                FirstUsed = null,
                LastUsed = null,
                EndDate = null,
                StatusID = (int) ResourceStatus.Active,
                ODBMask = null,
                PortedNO = string.Empty,
                TempNO = string.Empty,
                MobileType = string.Empty,
                PINInvalidTimes = 0,
                PINInvalidTimesTotal = 0,
                OCPPlmnTemplateId = 0,
                NAM = 0
                #endregion

            };

            #region Provisioning Template values
            if (HLRProvision != null)
            {
                logInfo("As we have HLR Provision template, we are going to use the values to fill the resourceMBInfo");
                resourceMb.CBSubsoption = HLRProvision.CB_SUBSOPTION;
                resourceMb.CBPassword = HLRProvision.CB_PASSWORD;
                resourceMb.CBWrongAttempts = HLRProvision.CB_WRONGATTEMPTS;
                resourceMb.TeleServiceList = HLRProvision.TELESERVICELIST;
                resourceMb.BearerServiceList = HLRProvision.BEARERSERVICELIST;
                resourceMb.ODBMask = HLRProvision.ODBMASK;
                resourceMb.ProvisionId = HLRProvision.PROVISIONID;
                resourceMb.NAM = HLRProvision.NAM;
                resourceMb.FTNRule = HLRProvision.FTNRule;
            } 
            #endregion

            var createSubscriptionReq = new CreateResourceMBInfoRequest()
            {
                ResourceMBInfoObj = resourceMb,
            };
            logInfo(string.Format("Create Subscription for Customer {0}, resource {1}, ICC {2}, IMSI {3} and PUK {4}.", customerInfo.CustomerID.Value, numberInfo.Resource, simCardInfo.ICCID,
                simCardInfo.IMSI1, simCardInfo.PUK1));

            //TODO Modify Micro Service to create the provisioning to the HLR
            var createSubscriptionResp = createSubscriptionMs.Process(createSubscriptionReq, null);

            return createSubscriptionResp;
        }


        /// <summary>
        /// Private function to associate the AccountCurrency to the Customer
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="accountCurrency"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        private AssignCustomerInfoToAccountResponse CreateCustomerAccountAssn(CustomerInfo customerInfo, AccountCurrency accountCurrency, DateTime startDate)
        {
            
            var assignCustomerAccountAssn = MicroServiceManager.GetMicroService<AssignCustomerInfoToAccountRequest, AssignCustomerInfoToAccountResponse>();
            
            

            #region Assign CustomerAccountASSN


            logInfo(string.Format("Create Customer Account ASSN for customer {0} with AcountId {1}"
                , customerInfo.CustomerID.Value, accountCurrency.Id));

            AssignCustomerInfoToAccountRequest assignCustomerAccountAssnRequest = new AssignCustomerInfoToAccountRequest()
            {
                Account = accountCurrency,
                CustomerInfo = customerInfo,
                StartDate = startDate,
                EndDate = null
            };
            AssignCustomerInfoToAccountResponse assignCusotmerAccountAssnResponse =
                assignCustomerAccountAssn.Process(assignCustomerAccountAssnRequest, null);


            #endregion

            return assignCusotmerAccountAssnResponse;
        }



        /// <summary>
        /// Private function to create the AccountCurrency that will be associated to the Customer
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="billCycle"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private CreateAccountCurrencyResponse CreateCustomerAccount(CustomerInfo customerInfo, BillCycle billCycle, RegisterCustomerConfiguration config)
        {
            var createAccountMs = MicroServiceManager.GetMicroService<CreateAccountCurrencyRequest, CreateAccountCurrencyResponse>();
            var getMultiLingualDescriptionMs = MicroServiceManager.GetMicroService<GetMultiLingualDescriptionByIdRequest, GetMultiLingualDescriptionByIdResponse>();
           
            #region Get Description and Name

            logInfo(string.Format("Get MultiLingualDescription for description with ID {0}", config.AccountDescriptionId));
            var getMultiLingualReq = new GetMultiLingualDescriptionByIdRequest()
            {
                MultiLingualDescriptionId = config.AccountDescriptionId,
            };
            var getMultiLingualResp = getMultiLingualDescriptionMs.Process(getMultiLingualReq, null);
            if (getMultiLingualResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(OrderNumber + "Cannot get the MultiLinguialDescription with Id " + config.AccountDescriptionId, BizOpsErrors.MultiLingualDescriptionNotFound);
            MultiLingualDescription accountDescription = getMultiLingualResp.MultiLingualDescription;

            logInfo(string.Format("Get MultiLingualDescription for Name with ID {0}", config.AccountNameId));
            getMultiLingualReq.MultiLingualDescriptionId = config.AccountNameId;
            getMultiLingualResp = getMultiLingualDescriptionMs.Process(getMultiLingualReq, null);
            if (getMultiLingualResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(OrderNumber + "Cannot get the MultiLinguialDescription with Id " + config.AccountDescriptionId, BizOpsErrors.MultiLingualDescriptionNotFound);
            MultiLingualDescription accountName = getMultiLingualResp.MultiLingualDescription;
            #endregion

            #region Create Customer Account

            logInfo(string.Format("Create Customer Account for customer {0} with Billcycle {1}, currency {2}, description {3} and name {4}", customerInfo.CustomerID.Value,
                billCycle.Id, config.AccountCurrency, accountDescription, accountName));
            AccountCurrency customerAccount = new AccountCurrency()
            {
                CurrentAsignedCustomer = customerInfo,
                Balance = new BalanceForAccount() { Balance = 0},
                BillingCycle = billCycle,
                LastBillRun = null,
                Currency = config.AccountCurrency,
                Description = accountDescription,
                Name = accountName,
            };
            customerAccount.Balance.Account = customerAccount;

            var createAccountReq = new CreateAccountCurrencyRequest()
            {
                AccountCurrency = customerAccount
            };
            var createAccountResp = createAccountMs.Process(createAccountReq, null);

            #endregion

            return createAccountResp;
        }

        /// <summary>
        /// Method to create the Invoice for the Customer
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <param name="customerAccount"></param>
        /// <param name="createTime"></param>
        /// <param name="billCycle"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private CreateInvoiceResponse CreateInvoice(CustomerInfo customerInfo, Account customerAccount, DateTime createTime, BillCycle billCycle, RegisterCustomerConfiguration config)
        {
            var createInvoiceMs = MicroServiceManager.GetMicroService<CreateInvoiceRequest, CreateInvoiceResponse>();
            var calculateNextBillRun = MicroServiceManager.GetMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();

            #region Get NextBillRun
            logInfo(string.Format("Calculate Next BillRun Date For BillCycle {0} and Purchase time {1}", billCycle.Id, createTime));
            var calculateNextBillRunReq = new CalculateNextBillRunDateForBillCycleRequest()
            {
                BillCycleDefinition = billCycle,
                FirstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek,
                PurchaseTime = createTime,
            };
            var calculateNextBillRunResp = calculateNextBillRun.Process(calculateNextBillRunReq, null);
            if (calculateNextBillRunResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(OrderNumber + "Error Calculating the NextBillRun.", BizOpsErrors.ErrorGettingNextBillRun);
            DateTime nextBillRun = calculateNextBillRunResp.NextBillRun;
            #endregion

            
            Invoice customerInvoice = new Invoice()
            {
                ChargingAccount = customerAccount,
                ChargedCustomer = customerInfo,
                EndDate = nextBillRun.AddSeconds(-1),
                StartDate = createTime,
            };
            var createInvoiceReq = new CreateInvoiceRequest()
            {
                Invoice = customerInvoice,
            };
            logInfo(string.Format("Creating Invoice for customer {0} with StartDate {1}, EndDate {2} and charging account {3}", customerInfo.CustomerID.Value,customerInvoice.StartDate, customerInvoice.EndDate, customerInvoice.ChargingAccount.Id ));
            var createInvoiceResp = createInvoiceMs.Process(createInvoiceReq, null);

            return createInvoiceResp;
        }

        /// <summary>
        /// Function to get the corresponding billcycle by Id
        /// </summary>
        /// <param name="mvno">The MVNO registering</param>
        /// <param name="billCycleId">The Id to be getted</param>
        /// <returns></returns>
        private BillCycle GetBillCycle(DealerInfo mvno, Int32 billCycleId)
        {
            var getBillCycleMs = MicroServiceManager.GetMicroService<GetBillCyclesByVMNORequest, GetBillCyclesByVMNOResponse>();

            logInfo(string.Format("Get Billcycle with ID {0} for the mvno {1}", billCycleId, mvno.DealerID.Value));
            var getBillCycleReq = new GetBillCyclesByVMNORequest() { DealerInfo = mvno };
            var billCycleResp = getBillCycleMs.Process(getBillCycleReq, null);
            if (billCycleResp.ResultType != ResultTypes.Ok || billCycleResp == null || !billCycleResp.BillCycles.Any())
                throw new BusinessLogicErrorException(OrderNumber + "Cannot get the BillCycle", BizOpsErrors.BillcycleNotFound);

            BillCycle billCycle = billCycleResp.BillCycles.FirstOrDefault(x => x.Id == billCycleId);

            if (billCycle == null)
                throw new BusinessLogicErrorException(OrderNumber + string.Format("Cannot get the specified BillCycleId {0}", billCycleId), BizOpsErrors.BillcycleNotFound);

            return billCycle;
        }

        ///// <summary>
        ///// Function to send an Event when the register process have been done
        ///// </summary>
        ///// <param name="customer"></param>
        ///// <param name="mvno"></param>
        ///// <param name="registerDate"></param>
        //private void SendRegistrationEvent(CustomerInfo customer, DealerInfo mvno, DateTime registerDate)
        //{
        //    RegistrationEvent ev = new RegistrationEvent()
        //    {
        //        Customer = EventToCoreModelHelper.FromCoreCustomerInfo(customer),
        //        Dealer = EventToCoreModelHelper.FromCoreDealerInfo(mvno),
        //        EventDate = registerDate,
        //        EventId = Guid.NewGuid().ToString(),
        //        MVNOID = mvno.DealerID.Value,
        //        ReferralCustomer = null,
        //    };
        //    logInfo(string.Format("Sending registration event to customer {0}, dealer {1} and event {2}", ev.Customer.CustomerID, ev.Dealer.DealerID, ev.EventId));
        //    QueuedEventSender.GetInstance().ProcessEvent(ev);
        //}

        private void UpdateLifecycleStatuses(CustomerInfo customer, SIMCardInfo simcard, NumberInfo number, DealerInfo mvno, LoginInfo user, Boolean withoutSubscription)
        {
            #region Update Customer
            logInfo(string.Format("Updating Customer {0} after all the assignments and initilizations", customer.CustomerID.Value));
            var updateCustomerMs = MicroServiceManager.GetMicroService<UpdateCustomerInfoRequest, UpdateCustomerInfoResponse>();
            var updateCustomerReq = new UpdateCustomerInfoRequest()
            {
                CustomerInfo = customer,
            };
            var updateCustomerResp = updateCustomerMs.Process(updateCustomerReq, null);
            if (updateCustomerResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(OrderNumber + string.Format("Error updating customer {0}", customer.CustomerID.Value), BizOpsErrors.ErrorUpdatingCustomer);
            #endregion

            //If the register is being doing without subscription, we can return now
            if (withoutSubscription) 
                return;

            #region Update Simcard

            logInfo(string.Format("Activating Simcard with IMSI {0} and ICCId {1}", simcard.IMSI1, simcard.ICCID));
            var activateSimcardMs =
                MicroServiceManager.GetMicroService<ActiveSimCardRequest, ActiveSimCardResponse>();
            var activateSimcardReq = new ActiveSimCardRequest()
            {
                SimCardInfo = simcard,
                CustomerInfo = customer,
                MVNO = mvno,
                User = user,
            };
            var activateSimcardResp = activateSimcardMs.Process(activateSimcardReq, null);
            if (activateSimcardResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(
                    OrderNumber + string.Format("Error activating simcard with IccId {0}", simcard.ICCID),
                    BizOpsErrors.ErrorActivatingSimcard);

            #endregion

            #region Update Number

            logInfo(string.Format("Activating Number {0} which has status {1}", number.Resource,
                number.NumberProperty.StatusID));
            var activateNumberMs =
                MicroServiceManager.GetMicroService<ActivateNumberRequest, ActivateNumberResponse>();
            var activateNumberReq = new ActivateNumberRequest()
            {
                NumberPropertyInfo = number.NumberProperty,
                CustomerInfo = customer,
                MVNO = mvno,
                User = user,
            };
            var activateNumberResp = activateNumberMs.Process(activateNumberReq, null);
            if (activateNumberResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(
                    OrderNumber + string.Format("Error activating number {0}", number.Resource),
                    BizOpsErrors.ErrorActivatingNumber);

            #endregion
        }

        /// <summary>
        /// Function to get the provision Template from DB to set the default values
        /// </summary>
        /// <param name="template">Template name to be taken</param>
        /// <param name="mvno">MVNO to take into account to have the provisioning template</param>
        /// <returns></returns>
        private CrmDefaultProvisionInfo GetProvisionTemplate(string template, DealerInfo mvno)
        {
            var getProvisionByProvisionNameMs = MicroServiceManager.GetMicroService<GetProvisioningTemplateByProvisionNameRequest, GetProvisioningTemplateByProvisionNameResponse>();
            var getProvisionReq = new GetProvisioningTemplateByProvisionNameRequest()
            {
                ProvisionName = template,
            };
            logInfo(string.Format("Calling GetProvisioningTemplateByProvisionNameMS using template {0}", template));
            var getProvisionResp = getProvisionByProvisionNameMs.Process(getProvisionReq, null);
            if (getProvisionResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(string.Format("Error trying to get the Provion Template with name [{0}]",template), BizOpsErrors.ProvisionTemplateNotFound);

            var provision = getProvisionResp.CrmDefaultProvisionInfos.OrderByDescending(x => x.CREATEDATE).FirstOrDefault(x => x.DEALERID == mvno.DealerID);
            if (provision == null)
                throw new BusinessLogicErrorException(string.Format("Cannot find Provision Template with Name [{0}] for mvno [{1}]", template, mvno.DealerID.Value), BizOpsErrors.ProvisionTemplateNotFound);
            return provision;
        }

        #endregion
    }
}
