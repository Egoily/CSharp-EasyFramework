using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.CreateCustomer;
using com.etak.core.bizops.fullfilment.PurchaseProductForCustomer;
using com.etak.core.bizops.fullfilment.RegisterCustomer;
using com.etak.core.bizops.helper;
using com.etak.core.customer.message.AssignCustomerInfoToAccount;
using com.etak.core.customer.message.CreateAccountCurrency;
using com.etak.core.customer.message.CreateInvoice;
using com.etak.core.customer.message.UpdateCustomerInfo;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItem;
using com.etak.core.GSMSubscription.messages.CreateResourceMBInfo;
using com.etak.core.GSMSubscription.messages.GetProvisioningTemplateByProvisionName;
using com.etak.core.microservices.messages.GetMultiLingualDescriptionById;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.product.message.GetBillCyclesByVMNO;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.resource;
using com.etak.core.resource.msisdn.message.ActivateNumberMS;
using com.etak.core.resource.msisdn.message.GetDealerNumberByResource;
using com.etak.core.resource.simCard.message.ActiveSimCard;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using com.etak.eventsystem.model;
using com.etak.eventsystem.model.events;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.RegisterCustomer
{
    [TestFixture]
    public class RegisterCustomerBizOpTests : AbstractSinglePhaseOrderProcessorTest<RegisterCustomerBizOp, 
                                                                        RegisterCustomerRequestDTO, RegisterCustomerResponseDTO, 
                                                                        RegisterCustomerRequestInternal, RegisterCustomerResponseInternal, RegisterCustomerOrder>
    {
        private const int DealerIdToMock = 1;
        private ICoreBusinessOperation<PurchaseProductForCustomerRequestInternal, PurchaseProductForCustomerResponseInternal> mockPurchaseProduct;
        private ICoreBusinessOperation<CreateCustomerRequestInternal, CreateCustomerResponseInternal> mockCreateCustomer;
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;
        private IMicroService<GetDealerNumberByResourceRequest, GetDealerNumberByResourceResponse> mockMicroServiceGetDealer;
        [TestFixtureSetUp]
        public void InitializeTest()
        {
            FakeSessionFactorySingleton.Init();

            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            mockMicroServiceGetDealer = MockMicroService<GetDealerNumberByResourceRequest, GetDealerNumberByResourceResponse>();

            var mockedEventSystem = Substitute.For<EventSystemContract>();
            mockedEventSystem.ProcessEvent(Arg.Any<CustomPayloadEvent>());

            #region Event Sender initialization

            //Initialize the factory that will be used by the queue to deliver the events.
            EventSenderSingleton.Init();

            #endregion

            mockPurchaseProduct = Substitute.For<ICoreBusinessOperation<PurchaseProductForCustomerRequestInternal, PurchaseProductForCustomerResponseInternal>>();
            mockCreateCustomer = Substitute.For<ICoreBusinessOperation<CreateCustomerRequestInternal, CreateCustomerResponseInternal>>();

            BusinessOperationManager.RebindCoreInterfaceToConstant(DealerIdToMock, mockPurchaseProduct);
            BusinessOperationManager.RebindCoreInterfaceToConstant(DealerIdToMock, mockCreateCustomer);


        }


        [TestCase(100)]
        public void RegisterCustomerOkWithSubscription(int testParameter)
        {


            CustomerInfo expectedCustomer = CreateDefaultObject.Create<CustomerInfo>();
            expectedCustomer.ResourceMBInfo = new List<ResourceMBInfo>() {CreateDefaultObject.Create<ResourceMBInfo>()};
            var customerDto = expectedCustomer.ToDto();

            #region Mocks.... a lot of mocks
            #region Prepare Request DTO
            CustomerDTO custDto = CreateDefaultObject.Create<CustomerDTO>();
            custDto.CustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
            custDto.CustomerData.BankInformation = CreateDefaultObject.Create<BankInformationDTO>();
            custDto.CustomerData.DeliveryAddress = CreateDefaultObject.Create<AddressDTO>();

            List<ProductCatalogDTO> productsToPurchase = new List<ProductCatalogDTO>();
            ProductCatalogDTO productDto = CreateDefaultObject.Create<ProductCatalogDTO>();
            productDto.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>() { CreateDefaultObject.Create<ProductPurchaseChargingOptionDTO>() };

            RegisterCustomerRequestDTO reqDto = new RegisterCustomerRequestDTO()
            {
                BillCycleId = testParameter,
                CustomerData = custDto,
                PurchasedProducts = productsToPurchase,
                MSISDN = testParameter.ToString(),
                ICCID = testParameter.ToString(),
                HLRProfile = testParameter.ToString(),
                CreditLimit = null,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
            };
            #endregion

            #region Mock OperationConfig
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var operationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var registerConfig = new RegisterCustomerConfiguration()
            {
                AccountCurrency = ISO4217CurrencyCodes.EUR,
                AccountDescriptionId = testParameter,
                AccountNameId = testParameter,
                BillcycleId = testParameter,
                ResourceBearerServiceList = testParameter.ToString(),
                ResourceCBPassword = testParameter.ToString(),
                ResourceCBSuboption = testParameter,
                ResourceCBWrongAttemps = testParameter,
                ResourceTeleServiceList = testParameter.ToString(),
                LanguageId = testParameter,
            };
            operationConfiguration.JSonConfig = JsonConvert.SerializeObject(registerConfig);
            operationConfiguration.StarTime = DateTime.Now;
            operationConfiguration.EndDate = DateTime.Now.AddYears(1);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfiguration });
            #endregion

            #region Mock GetBillCycle

            var mockGetBillCycleMs = MockMicroServiceManager.GetMockedMicroService<GetBillCyclesByVMNORequest, GetBillCyclesByVMNOResponse>();
            var getBillCycleReq = Arg.Is<GetBillCyclesByVMNORequest>(x => x.DealerInfo.DealerID == 1);
            BillCycle expectedBillCycle = CreateDefaultObject.Create<BillCycle>();
            expectedBillCycle.Id = testParameter;
            var getBillCycleResp = new GetBillCyclesByVMNOResponse()
            {
                BillCycles = new List<BillCycle>() { expectedBillCycle },
                ResultType = ResultTypes.Ok,
            };
            mockGetBillCycleMs.Process(getBillCycleReq, Arg.Any<RequestInvokationEnvironment>())
                .Returns(getBillCycleResp);
            #endregion

            MockAbstractSinglePhaseOrderProcessor(reqDto);

            #region Mock GetDealer
            var actualGetDealerResponse = new GetDealerNumberByResourceResponse() { DealerNumberInfo = new List<DealerNumberInfo> { new DealerNumberInfo { DealerID = 190000 } } };

            var actualGetDealerRequest = CreateDefaultObject.Create<GetDealerNumberByResourceRequest>();
            mockMicroServiceGetDealer.Process(
                actualGetDealerRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualGetDealerResponse);
            #endregion

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            #region Mock INumberInfoRepo

            var expectedNumberInfo = CreateDefaultObject.Create<NumberInfo>();
            expectedNumberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
            expectedNumberInfo.NumberProperty.StatusID = (int)ResourceStatus.Init;
            expectedNumberInfo.NumberProperty.Resource = testParameter.ToString();

            var mockRepo = MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            mockRepo.GetById(Arg.Any<string>()).Returns(expectedNumberInfo);
            #endregion

            #region Mock ISimcardInfoRepo

            var expectedSimcardInfo = CreateDefaultObject.Create<SIMCardInfo>();
            expectedSimcardInfo.ICCID = testParameter.ToString();
            expectedSimcardInfo.Status = (int)SIMCardStatus.Init;

            expectedNumberInfo.NumberProperty.StatusID = (int)ResourceStatus.Init;

            var mockRepoSimcard = MockRepositoryManager.GetMockedRepository<ISIMCardInfoRepository<SIMCardInfo>>();
            mockRepoSimcard.GetById(Arg.Any<string>()).Returns(expectedSimcardInfo);
            #endregion

            #region Mock Activate Simcard
            var expectedSimcardInfoActivated = CreateDefaultObject.Create<SIMCardInfo>();
            expectedSimcardInfoActivated.ICCID = testParameter.ToString();
            expectedSimcardInfoActivated.Status = (int)SIMCardStatus.Init;
            var mockActiveSimcard = MockMicroService<ActiveSimCardRequest, ActiveSimCardResponse>();
            var activeSimcardReq = Arg.Is<ActiveSimCardRequest>(x => x.SimCardInfo.ICCID == testParameter.ToString());
            var activeSimcardResp = new ActiveSimCardResponse()
            {
                ResultType = ResultTypes.Ok,
                SimCardInfo = expectedSimcardInfoActivated
            };
            mockActiveSimcard.Process(activeSimcardReq, Arg.Any<RequestInvokationEnvironment>()).Returns(activeSimcardResp);
            #endregion

            #region Mock Purchase Product
            var purchaseResp = new PurchaseProductForCustomerResponseInternal()
            {
                ResultType = ResultTypes.Ok,
                productPurchaseList = new List<CustomerProductAssignment>() { CreateDefaultObject.Create<CustomerProductAssignment>()}
            };
            purchaseResp.productPurchaseList[0].Id = testParameter;

            //This method is not valid. It will go inside the method
            mockPurchaseProduct.Process(Arg.Any<PurchaseProductForCustomerRequestInternal>(), Arg.Any<RequestInvokationEnvironment>()).Returns(x => purchaseResp);
            #endregion

            #region Mock Create Customer
            CreateCustomerResponseInternal createCustomerResp = new CreateCustomerResponseInternal()
            {
                Customer = expectedCustomer,
            };
            mockCreateCustomer.Process(Arg.Any<CreateCustomerRequestInternal>(), Arg.Any<RequestInvokationEnvironment>()).Returns(createCustomerResp);
            #endregion

            #region Mock CreateAccount
            var mockCreateAccount = MockMicroServiceManager.GetMockedMicroService<CreateAccountCurrencyRequest, CreateAccountCurrencyResponse>();
            var createAccountReq = Arg.Is<CreateAccountCurrencyRequest>(x => x.AccountCurrency.BillingCycle.Id == testParameter);
            var createAccountResp = new CreateAccountCurrencyResponse()
            {
                AccountCurrency = CreateDefaultObject.Create<AccountCurrency>(),
                ResultType = ResultTypes.Ok,
            };
            mockCreateAccount.Process(createAccountReq, Arg.Any<RequestInvokationEnvironment>()).Returns(createAccountResp);
            #endregion


            #region Mock CreateAccountAssociation
            var mockassignCustomerAccountAssn = MockMicroServiceManager.GetMockedMicroService<AssignCustomerInfoToAccountRequest, AssignCustomerInfoToAccountResponse>();
            var createAccountAssnResp = new AssignCustomerInfoToAccountResponse()
            {
                CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                ResultType = ResultTypes.Ok,
            };
            mockassignCustomerAccountAssn.Process(Arg.Any<AssignCustomerInfoToAccountRequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(createAccountAssnResp);
            #endregion


            #region Mock Create Resource
            var mockCreateResource = MockMicroServiceManager.GetMockedMicroService<CreateResourceMBInfoRequest, CreateResourceMBInfoResponse>();
            var createResource = Arg.Is<CreateResourceMBInfoRequest>(x => x.ResourceMBInfoObj.ICC == testParameter.ToString());
            var createResourceResp = new CreateResourceMBInfoResponse()
            {
                ResultType = ResultTypes.Ok,
                ResourceMBInfoObj = CreateDefaultObject.Create<ResourceMBInfo>()
            };
            mockCreateResource.Process(createResource, Arg.Any<RequestInvokationEnvironment>()).Returns(createResourceResp);
            #endregion

            #region Mock Create Invoice
            var mockCreateInvoice = MockMicroServiceManager.GetMockedMicroService<CreateInvoiceRequest, CreateInvoiceResponse>();
            var createInvoiceReq = Arg.Is<CreateInvoiceRequest>(x => x.Invoice.ChargedCustomer.CustomerID == 1);
            var createInvoiceResp = new CreateInvoiceResponse()
            {
                ResultType = ResultTypes.Ok,
                Invoice = CreateDefaultObject.Create<Invoice>(),
            };
            mockCreateInvoice.Process(createInvoiceReq, Arg.Any<RequestInvokationEnvironment>()).Returns(createInvoiceResp);
            #endregion

            #region Mock Activate Number

            var mockActivateNumber = MockMicroServiceManager.GetMockedMicroService<ActivateNumberRequest, ActivateNumberResponse>();
            var activateNumberReq = Arg.Is<ActivateNumberRequest>(x => x.NumberPropertyInfo.Resource == testParameter.ToString());
            var activateNumberResp = new ActivateNumberResponse()
            {
                ResultType = ResultTypes.Ok,
                NumberPropertyInfo = expectedNumberInfo.NumberProperty,
            };
            mockActivateNumber.Process(activateNumberReq, Arg.Any<RequestInvokationEnvironment>()).Returns(activateNumberResp);
            #endregion

            #region Mock Multilingual
            var mockMultilingual = MockMicroServiceManager.GetMockedMicroService<GetMultiLingualDescriptionByIdRequest, GetMultiLingualDescriptionByIdResponse>();
            var getMultilingualReq = Arg.Is<GetMultiLingualDescriptionByIdRequest>(x => x.MultiLingualDescriptionId == testParameter);
            var getMultilingualResp = new GetMultiLingualDescriptionByIdResponse()
            {
                ResultType = ResultTypes.Ok,
                MultiLingualDescription = CreateDefaultObject.Create<MultiLingualDescription>(),
            };
            mockMultilingual.Process(getMultilingualReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getMultilingualResp);
            #endregion

            #region Get
            var mockConfigAction = MockMicroServiceManager.GetMockedMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest, GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();

            #endregion

            #region Mock CalculateNextBillRun
            var mockCalculateNextBillRun = MockMicroServiceManager.GetMockedMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            var calculateNextBillRunReq = Arg.Is<CalculateNextBillRunDateForBillCycleRequest>(x => x.BillCycleDefinition.Id == testParameter);
            var calculateNextBillRunResp = new CalculateNextBillRunDateForBillCycleResponse()
            {
                ResultType = ResultTypes.Ok,
                NextBillRun = DateTime.Now.AddMonths(1),
            };
            mockCalculateNextBillRun.Process(calculateNextBillRunReq, Arg.Any<RequestInvokationEnvironment>()).Returns(calculateNextBillRunResp);
            #endregion

            #region Mock PurchaseHelper
            var mockPurchaseHelper = Substitute.For<PurchaseHelper>();
            var productDict = CreateDefaultObject.Create<ProductOffering>();
            var chargeOptionDict = new ProductChargeOption()
            {
                CreateDate = DateTime.Now,
                Charges = new List<Charge>() { CreateDefaultObject.Create<ChargeNonRecurring>() },
                Id = 1,
                ProductOffering = productDict,
                StartDate = DateTime.Now,
                IsDefaultOption = DefaultOptions.Y,
                Status = ProductPurchaseStatus.Default
            };
            var expectedDict = new List<Tuple<ProductOffering, ProductChargeOption>>(){Tuple.Create(productDict, chargeOptionDict)};
            mockPurchaseHelper.GetProductsAndChargesOptions(null).ReturnsForAnyArgs(expectedDict);
            #endregion

            #region Mock GetProvisioningTemplateByName
            var mockGetProvisioningMS = MockMicroServiceManager.GetMockedMicroService<GetProvisioningTemplateByProvisionNameRequest, GetProvisioningTemplateByProvisionNameResponse>();
            var getProvisioningReq = Arg.Is<GetProvisioningTemplateByProvisionNameRequest>(x => x.ProvisionName == testParameter.ToString());
            var getProvisionResp = new GetProvisioningTemplateByProvisionNameResponse()
            {
                CrmDefaultProvisionInfos = new List<CrmDefaultProvisionInfo>() { CreateDefaultObject.Create<CrmDefaultProvisionInfo>()},
                ResultType = ResultTypes.Ok,
            };
            getProvisionResp.CrmDefaultProvisionInfos.First().DEALERID = 1;
            mockGetProvisioningMS.Process(getProvisioningReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getProvisionResp);
            #endregion

            #region Mock Update Customer

            var mockUpdateMs = MockMicroService<UpdateCustomerInfoRequest, UpdateCustomerInfoResponse>();
            mockUpdateMs.Process(Arg.Any<UpdateCustomerInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns(new UpdateCustomerInfoResponse()
                {
                    ResultType = ResultTypes.Ok,
                    CustomerInfo = expectedCustomer,
                });
            #endregion

            #endregion

            var response = new RegisterCustomerResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var registerbizOp = new RegisterCustomerBizOp(mockPurchaseHelper);

                response = registerbizOp.ProcessFromCustomerModel(new NullValidator<RegisterCustomerRequestDTO>(), new SameTypeConverter<RegisterCustomerRequestDTO>(),
                    new SameTypeConverter<RegisterCustomerResponseDTO>(), reqDto, FakeInvoker);

            }
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(response.Customer, customerDto);
            Assert.IsNotNull(response.Subscription);
            
            RepositoryManager.CloseConnection();
            
        }


        [Test]
        public void TestSerializedObject()
        {
            var config = CreateDefaultObject.Create<RegisterCustomerConfiguration>();
            config.AccountCurrency = ISO4217CurrencyCodes.EUR;
            config.AccountDescriptionId = 1674015540;
            config.AccountNameId = 1674015540;
            config.BillcycleId = 1010000000;
            config.LanguageId = 3082;
            config.ResourceBearerServiceList = "BS1F,BS26,BS2F";
            config.ResourceCBPassword = "0000";
            config.ResourceCBSuboption = -1;
            config.ResourceCBWrongAttemps = -1;
            config.ResourceTeleServiceList = "TS11,TS12,TS21,TS22";
            var serialized = JsonConvert.SerializeObject(config);

            Console.Write(serialized);

            var configDes = JsonConvert.DeserializeObject<RegisterCustomerConfiguration>(serialized);
            Console.WriteLine(configDes);

        }
        [Test()]
        public void RegisterCustomerBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            #region Mock GetDealer
            var actualGetDealerResponse = new GetDealerNumberByResourceResponse() { DealerNumberInfo = new List<DealerNumberInfo> { new DealerNumberInfo { DealerID = 190000 } } };

            var actualGetDealerRequest = CreateDefaultObject.Create<GetDealerNumberByResourceRequest>();
            mockMicroServiceGetDealer.Process(
                actualGetDealerRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualGetDealerResponse);
            #endregion

            var requestDto = new RegisterCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1000000000"
            };

            MockAbsctractBusinessOperation(requestDto);

            var res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.AuthorizationError);
            Assert.AreEqual(res.errorCode, BizOpsErrors.AuthorizeErrorUser);
        }


        [TestCase(100)]
        public void RegisterCustomerOkWithoutSubscription(int testParameter)
        {


            CustomerInfo expectedCustomer = CreateDefaultObject.Create<CustomerInfo>();
            expectedCustomer.ResourceMBInfo = new List<ResourceMBInfo>() { CreateDefaultObject.Create<ResourceMBInfo>() };
            var customerDto = expectedCustomer.ToDto();

            #region Mocks.... a lot of mocks
            #region Prepare Request DTO
            CustomerDTO custDto = CreateDefaultObject.Create<CustomerDTO>();
            custDto.CustomerData = CreateDefaultObject.Create<CustomerDataDTO>();
            custDto.CustomerData.BankInformation = CreateDefaultObject.Create<BankInformationDTO>();
            custDto.CustomerData.DeliveryAddress = CreateDefaultObject.Create<AddressDTO>();

            List<ProductCatalogDTO> productsToPurchase = new List<ProductCatalogDTO>();
            ProductCatalogDTO productDto = CreateDefaultObject.Create<ProductCatalogDTO>();
            productDto.PurchaseOptions = new List<ProductPurchaseChargingOptionDTO>() { CreateDefaultObject.Create<ProductPurchaseChargingOptionDTO>() };

            RegisterCustomerRequestDTO reqDto = new RegisterCustomerRequestDTO()
            {
                BillCycleId = testParameter,
                CustomerData = custDto,
                PurchasedProducts = productsToPurchase,
                MSISDN = testParameter.ToString(),
                ICCID = testParameter.ToString(),
                HLRProfile = testParameter.ToString(),
                CreditLimit = null,
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                WithoutSubscription = true,
            };
            #endregion

            #region Mock OperationConfig
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var operationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var registerConfig = new RegisterCustomerConfiguration()
            {
                AccountCurrency = ISO4217CurrencyCodes.EUR,
                AccountDescriptionId = testParameter,
                AccountNameId = testParameter,
                BillcycleId = testParameter,
                ResourceBearerServiceList = testParameter.ToString(),
                ResourceCBPassword = testParameter.ToString(),
                ResourceCBSuboption = testParameter,
                ResourceCBWrongAttemps = testParameter,
                ResourceTeleServiceList = testParameter.ToString(),
                LanguageId = testParameter,
            };
            operationConfiguration.JSonConfig = JsonConvert.SerializeObject(registerConfig);
            operationConfiguration.StarTime = DateTime.Now;
            operationConfiguration.EndDate = DateTime.Now.AddYears(1);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfiguration });
            #endregion

            #region Mock GetBillCycle

            var mockGetBillCycleMs = MockMicroServiceManager.GetMockedMicroService<GetBillCyclesByVMNORequest, GetBillCyclesByVMNOResponse>();
            var getBillCycleReq = Arg.Is<GetBillCyclesByVMNORequest>(x => x.DealerInfo.DealerID == 1);
            BillCycle expectedBillCycle = CreateDefaultObject.Create<BillCycle>();
            expectedBillCycle.Id = testParameter;
            var getBillCycleResp = new GetBillCyclesByVMNOResponse()
            {
                BillCycles = new List<BillCycle>() { expectedBillCycle },
                ResultType = ResultTypes.Ok,
            };
            mockGetBillCycleMs.Process(getBillCycleReq, Arg.Any<RequestInvokationEnvironment>())
                .Returns(getBillCycleResp);
            #endregion

            MockAbstractSinglePhaseOrderProcessor(reqDto);

            #region Mock GetDealer
            var actualGetDealerResponse = new GetDealerNumberByResourceResponse() { DealerNumberInfo = new List<DealerNumberInfo> { new DealerNumberInfo { DealerID = 190000 } } };

            var actualGetDealerRequest = CreateDefaultObject.Create<GetDealerNumberByResourceRequest>();
            mockMicroServiceGetDealer.Process(
                actualGetDealerRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualGetDealerResponse);
            #endregion

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            #region Mock INumberInfoRepo

            var expectedNumberInfo = CreateDefaultObject.Create<NumberInfo>();
            expectedNumberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
            expectedNumberInfo.NumberProperty.StatusID = (int)ResourceStatus.Init;
            expectedNumberInfo.NumberProperty.Resource = testParameter.ToString();

            var mockRepo = MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            mockRepo.GetById(Arg.Any<string>()).Returns(expectedNumberInfo);
            #endregion

            #region Mock ISimcardInfoRepo

            var expectedSimcardInfo = CreateDefaultObject.Create<SIMCardInfo>();
            expectedSimcardInfo.ICCID = testParameter.ToString();
            expectedSimcardInfo.Status = (int)SIMCardStatus.Init;

            expectedNumberInfo.NumberProperty.StatusID = (int)ResourceStatus.Init;

            var mockRepoSimcard = MockRepositoryManager.GetMockedRepository<ISIMCardInfoRepository<SIMCardInfo>>();
            mockRepoSimcard.GetById(Arg.Any<string>()).Returns(expectedSimcardInfo);
            #endregion

            #region Mock Activate Simcard
            var expectedSimcardInfoActivated = CreateDefaultObject.Create<SIMCardInfo>();
            expectedSimcardInfoActivated.ICCID = testParameter.ToString();
            expectedSimcardInfoActivated.Status = (int)SIMCardStatus.Init;
            var mockActiveSimcard = MockMicroService<ActiveSimCardRequest, ActiveSimCardResponse>();
            var activeSimcardReq = Arg.Is<ActiveSimCardRequest>(x => x.SimCardInfo.ICCID == testParameter.ToString());
            var activeSimcardResp = new ActiveSimCardResponse()
            {
                ResultType = ResultTypes.Ok,
                SimCardInfo = expectedSimcardInfoActivated
            };
            mockActiveSimcard.Process(activeSimcardReq, Arg.Any<RequestInvokationEnvironment>()).Returns(activeSimcardResp);
            #endregion

            #region Mock Purchase Product
            var purchaseResp = new PurchaseProductForCustomerResponseInternal()
            {
                ResultType = ResultTypes.Ok,
                productPurchaseList = new List<CustomerProductAssignment>() { CreateDefaultObject.Create<CustomerProductAssignment>() }
            };
            purchaseResp.productPurchaseList[0].Id = testParameter;

            //This method is not valid. It will go inside the method
            mockPurchaseProduct.Process(Arg.Any<PurchaseProductForCustomerRequestInternal>(), Arg.Any<RequestInvokationEnvironment>()).Returns(x => purchaseResp);
            #endregion

            #region Mock Create Customer
            CreateCustomerResponseInternal createCustomerResp = new CreateCustomerResponseInternal()
            {
                Customer = expectedCustomer,
            };
            mockCreateCustomer.Process(Arg.Any<CreateCustomerRequestInternal>(), Arg.Any<RequestInvokationEnvironment>()).Returns(createCustomerResp);
            #endregion

            #region Mock CreateAccount
            var mockCreateAccount = MockMicroServiceManager.GetMockedMicroService<CreateAccountCurrencyRequest, CreateAccountCurrencyResponse>();
            var createAccountReq = Arg.Is<CreateAccountCurrencyRequest>(x => x.AccountCurrency.BillingCycle.Id == testParameter);
            var createAccountResp = new CreateAccountCurrencyResponse()
            {
                AccountCurrency = CreateDefaultObject.Create<AccountCurrency>(),
                ResultType = ResultTypes.Ok,
            };
            mockCreateAccount.Process(createAccountReq, Arg.Any<RequestInvokationEnvironment>()).Returns(createAccountResp);
            #endregion


            #region Mock CreateAccountAssociation
            var mockassignCustomerAccountAssn = MockMicroServiceManager.GetMockedMicroService<AssignCustomerInfoToAccountRequest, AssignCustomerInfoToAccountResponse>();
            var createAccountAssnResp = new AssignCustomerInfoToAccountResponse()
            {
                CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                ResultType = ResultTypes.Ok,
            };
            mockassignCustomerAccountAssn.Process(Arg.Any<AssignCustomerInfoToAccountRequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(createAccountAssnResp);
            #endregion


            #region Mock Create Resource
            var mockCreateResource = MockMicroServiceManager.GetMockedMicroService<CreateResourceMBInfoRequest, CreateResourceMBInfoResponse>();
            var createResource = Arg.Is<CreateResourceMBInfoRequest>(x => x.ResourceMBInfoObj.ICC == testParameter.ToString());
            var createResourceResp = new CreateResourceMBInfoResponse()
            {
                ResultType = ResultTypes.Ok,
                ResourceMBInfoObj = CreateDefaultObject.Create<ResourceMBInfo>()
            };
            mockCreateResource.Process(createResource, Arg.Any<RequestInvokationEnvironment>()).Returns(createResourceResp);
            #endregion

            #region Mock Create Invoice
            var mockCreateInvoice = MockMicroServiceManager.GetMockedMicroService<CreateInvoiceRequest, CreateInvoiceResponse>();
            var createInvoiceReq = Arg.Is<CreateInvoiceRequest>(x => x.Invoice.ChargedCustomer.CustomerID == 1);
            var createInvoiceResp = new CreateInvoiceResponse()
            {
                ResultType = ResultTypes.Ok,
                Invoice = CreateDefaultObject.Create<Invoice>(),
            };
            mockCreateInvoice.Process(createInvoiceReq, Arg.Any<RequestInvokationEnvironment>()).Returns(createInvoiceResp);
            #endregion

            #region Mock Activate Number

            var mockActivateNumber = MockMicroServiceManager.GetMockedMicroService<ActivateNumberRequest, ActivateNumberResponse>();
            var activateNumberReq = Arg.Is<ActivateNumberRequest>(x => x.NumberPropertyInfo.Resource == testParameter.ToString());
            var activateNumberResp = new ActivateNumberResponse()
            {
                ResultType = ResultTypes.Ok,
                NumberPropertyInfo = expectedNumberInfo.NumberProperty,
            };
            mockActivateNumber.Process(activateNumberReq, Arg.Any<RequestInvokationEnvironment>()).Returns(activateNumberResp);
            #endregion

            #region Mock Multilingual
            var mockMultilingual = MockMicroServiceManager.GetMockedMicroService<GetMultiLingualDescriptionByIdRequest, GetMultiLingualDescriptionByIdResponse>();
            var getMultilingualReq = Arg.Is<GetMultiLingualDescriptionByIdRequest>(x => x.MultiLingualDescriptionId == testParameter);
            var getMultilingualResp = new GetMultiLingualDescriptionByIdResponse()
            {
                ResultType = ResultTypes.Ok,
                MultiLingualDescription = CreateDefaultObject.Create<MultiLingualDescription>(),
            };
            mockMultilingual.Process(getMultilingualReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getMultilingualResp);
            #endregion

            #region Get
            var mockConfigAction = MockMicroServiceManager.GetMockedMicroService<GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest, GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemResponse>();

            #endregion

            #region Mock CalculateNextBillRun
            var mockCalculateNextBillRun = MockMicroServiceManager.GetMockedMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            var calculateNextBillRunReq = Arg.Is<CalculateNextBillRunDateForBillCycleRequest>(x => x.BillCycleDefinition.Id == testParameter);
            var calculateNextBillRunResp = new CalculateNextBillRunDateForBillCycleResponse()
            {
                ResultType = ResultTypes.Ok,
                NextBillRun = DateTime.Now.AddMonths(1),
            };
            mockCalculateNextBillRun.Process(calculateNextBillRunReq, Arg.Any<RequestInvokationEnvironment>()).Returns(calculateNextBillRunResp);
            #endregion

            #region Mock PurchaseHelper
            var mockPurchaseHelper = Substitute.For<PurchaseHelper>();
            var productDict = CreateDefaultObject.Create<ProductOffering>();
            var chargeOptionDict = new ProductChargeOption()
            {
                CreateDate = DateTime.Now,
                Charges = new List<Charge>() { CreateDefaultObject.Create<ChargeNonRecurring>() },
                Id = 1,
                ProductOffering = productDict,
                StartDate = DateTime.Now,
                IsDefaultOption = DefaultOptions.Y,
                Status = ProductPurchaseStatus.Default
            };
            var expectedDict = new List<Tuple<ProductOffering, ProductChargeOption>>() { Tuple.Create(productDict, chargeOptionDict) };
            mockPurchaseHelper.GetProductsAndChargesOptions(null).ReturnsForAnyArgs(expectedDict);
            #endregion

            #region Mock GetProvisioningTemplateByName
            var mockGetProvisioningMS = MockMicroServiceManager.GetMockedMicroService<GetProvisioningTemplateByProvisionNameRequest, GetProvisioningTemplateByProvisionNameResponse>();
            var getProvisioningReq = Arg.Is<GetProvisioningTemplateByProvisionNameRequest>(x => x.ProvisionName == testParameter.ToString());
            var getProvisionResp = new GetProvisioningTemplateByProvisionNameResponse()
            {
                CrmDefaultProvisionInfos = new List<CrmDefaultProvisionInfo>() { CreateDefaultObject.Create<CrmDefaultProvisionInfo>() },
                ResultType = ResultTypes.Ok,
            };
            getProvisionResp.CrmDefaultProvisionInfos.First().DEALERID = 1;
            mockGetProvisioningMS.Process(getProvisioningReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getProvisionResp);
            #endregion

            #region Mock Update Customer

            var mockUpdateMs = MockMicroService<UpdateCustomerInfoRequest, UpdateCustomerInfoResponse>();
            mockUpdateMs.Process(Arg.Any<UpdateCustomerInfoRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns(new UpdateCustomerInfoResponse()
                {
                    ResultType = ResultTypes.Ok,
                    CustomerInfo = expectedCustomer,
                });
            #endregion

            #endregion

            var response = new RegisterCustomerResponseDTO();
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var registerbizOp = new RegisterCustomerBizOp(mockPurchaseHelper);

                response = registerbizOp.ProcessFromCustomerModel(new NullValidator<RegisterCustomerRequestDTO>(), new SameTypeConverter<RegisterCustomerRequestDTO>(),
                    new SameTypeConverter<RegisterCustomerResponseDTO>(), reqDto, FakeInvoker);

            }
            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(response.Customer, customerDto);
            Assert.IsNull(response.Subscription);
            
            RepositoryManager.CloseConnection();

        }
    }
}
