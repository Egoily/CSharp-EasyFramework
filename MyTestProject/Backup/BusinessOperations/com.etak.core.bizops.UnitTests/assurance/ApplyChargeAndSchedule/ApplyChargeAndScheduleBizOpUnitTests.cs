using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.assurance.ApplyChargeAndSchedule;
using com.etak.core.customer.message.AddChargeScheduleToCustomer;
using com.etak.core.customer.message.AddChargeToCustomer;
using com.etak.core.customer.message.GetCustomerChargesScheduleById;
using com.etak.core.customer.message.GetLastNInvoicesByCustomerIdAndInvoiceStatuses;
using com.etak.core.dealer.messages.GetDealerInfoById;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentsByCustomerIDAndDatePeriod;
using com.etak.core.microservices.messages.GetTaxDefinitonsByCategory;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.product.message.GetChargeById;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.operation;
using com.etak.core.service.messages.AddUnbilledBalance;
using com.etak.core.service.messages.CustomerHasCredit;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using com.etak.eventsystem.model;
using com.etak.eventsystem.model.events;
using com.etak.eventsystem.wcfSender;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.assurance.ApplyChargeAndSchedule
{
    [TestFixture]
    public class ApplyChargeAndScheduleBizOpUnitTests :
        AbstractSinglePhaseOrderProcessorTest
            <ApplyChargeAndScheduleBizOp, ApplyChargeAndScheduleDTORequest, ApplyChargeAndScheduleDTOResponse, ApplyChargeAndScheduleRequest,
            ApplyChargeAndScheduleResponse, ApplyChargeAndScheduleOrder>
    {
        private IMicroService<AddChargeToCustomerRequest, AddChargeToCustomerResponse> mockedAddCharge;

        private IMicroService<AddChargeScheduleToCustomerRequest, AddChargeScheduleToCustomerResponse> mockedAddChargeSchedule;

        private IMicroService<CustomerHasCreditRequest, CustomerHasCreditResponse> mockedHasCredit;

        private IMicroService<AddUnbilledBalanceRequest, AddUnbilledBalanceResponse> mockedAddUnbilledBalance;

        private DateTime startDate = new DateTime(2015, 5, 4);

        private DateTime? priceEffectiveDate = null;
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockedAddCharge = MockMicroService<AddChargeToCustomerRequest, AddChargeToCustomerResponse>();
            mockedAddChargeSchedule = MockMicroService<AddChargeScheduleToCustomerRequest, AddChargeScheduleToCustomerResponse>();
            mockedHasCredit = MockMicroService<CustomerHasCreditRequest, CustomerHasCreditResponse>();
            mockedAddUnbilledBalance = MockMicroService<AddUnbilledBalanceRequest, AddUnbilledBalanceResponse>();

            var mockedEventSystem = Substitute.For<EventSystemContract>();
            mockedEventSystem.ProcessEvent(Arg.Any<CustomPayloadEvent>());

            #region Event Sender initialization

            //Initialize the factory that will be used by the queue to deliver the events.
            EventSenderSingleton.Init();

            #endregion
        }

        [TestCase(100)]
        public void ApplyChargeAndScheduleBizOp_NonRecurringOk_ShouldReturnCustomerCharge(int testParameter)
        {
            #region AddChargeToCustomer Mock

            var addChargeReq = Arg.Is<AddChargeToCustomerRequest>(x => x.Amount == 900);
            var addchargeResp = CreateDefaultObject.Create<AddChargeToCustomerResponse>();
            addchargeResp.ChargeCreated = CreateDefaultObject.Create<CustomerCharge>();
            addchargeResp.ChargeCreated.Amount = 999;
            mockedAddCharge.Process(addChargeReq, Arg.Any<RequestInvokationEnvironment>()).Returns(addchargeResp);

            #endregion

            #region Get Configuration
            var mockGetConfigurationRepo = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var config = CreateDefaultObject.Create<ApplyChargeAndScheduleConfiguration>();
            config.SendChargeEvent = true;

            var mockBusinessOprepo = MockRepositoryManager.GetMockedRepository<IBusinessOperationRepository<BusinessOperation>>();
            mockBusinessOprepo.GetById(Arg.Any<int>()).ReturnsForAnyArgs(new ApplyChargeAndScheduleBizOp());


            var operationConfig = new OperationConfiguration()
            {
                Operation = new ApplyChargeAndScheduleBizOp(),
                EndDate = null,
                StarTime = DateTime.Now.AddDays(-1),
                JSonConfig = JsonConvert.SerializeObject(config),
            };

            mockGetConfigurationRepo.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfig });
            #endregion

            #region CustomerHasCredit Mock

            ServicesInfo masterBundle = CreateDefaultObject.Create<ServicesInfo>();
            masterBundle.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            masterBundle.BundleDefinition.BundleID = testParameter;

            var hasCreditReq = Arg.Is<CustomerHasCreditRequest>(x => x.CustomerInfo.CustomerID == testParameter);
            var hasCreditResp = new CustomerHasCreditResponse() { HasCredit = true, MasterBundle = masterBundle, ResultType = ResultTypes.Ok, };
            mockedHasCredit.Process(hasCreditReq, Arg.Any<RequestInvokationEnvironment>()).Returns(hasCreditResp);

            #endregion

            #region AddUnbilled Mock

            var addUnbilledReq = Arg.Is<AddUnbilledBalanceRequest>(x => x.ServicesInfo.BundleDefinition.BundleID == testParameter);
            var addUnbilledResp = CreateDefaultObject.Create<AddUnbilledBalanceResponse>();
            addUnbilledResp.ResultType = ResultTypes.Ok;
            mockedAddUnbilledBalance.Process(addUnbilledReq, Arg.Any<RequestInvokationEnvironment>()).Returns(addUnbilledResp);

            #endregion

            ChargeNonRecurring chargeNonRecurring = CreateDefaultObject.Create<ChargeNonRecurring>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.CustomerID = testParameter;

            var bizOp = new ApplyChargeAndScheduleBizOp();

            var request = new ApplyChargeAndScheduleRequest()
                              {
                                  ChargeToAdd = chargeNonRecurring,
                                  Account = CreateDefaultObject.Create<Account>(),
                                  CustomAmount = 900,
                                  CustomerDealer = CreateDefaultObject.Create<DealerInfo>(),
                                  CustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>(),
                                  Customer = customerInfo,
                                  CycleNumber = 1,
                                  InvoiceOfCharge = CreateDefaultObject.Create<Invoice>(),
                                  PriceEffectiveDate = priceEffectiveDate,
                                  Schedule = CreateDefaultObject.Create<CustomerChargeSchedule>(),
                                  StartDate = startDate,
                                  TaxDefinition = CreateDefaultObject.Create<TaxDefinition>(),
                                  TypeOfCharges = ApplyChargeAndScheduleBizOp.TypeOfCharges.NonRecurringChargeAllowed
                              };

            var resp = bizOp.ProcessRequest(null, request);

            AssertExt.ObjectPropertiesAreEqual(resp.ChargeAdde, addchargeResp.ChargeCreated);
            Assert.IsTrue(resp.ScheduleAdded == null);
            Assert.IsTrue(resp.ResultType == ResultTypes.Ok);
            Assert.IsNotNull(resp.Subscription);
        }

        [TestCase(100)]
        public void ApplyChargeAndScheduleBizOp_RecurringOk_ShouldReturnCustomerChargeSchedule(int testParameter)
        {
            var accountMock = CreateDefaultObject.Create<Account>();
            var customerInfoMock = CreateDefaultObject.Create<CustomerInfo>();
            var customerProductAssignmentMock = CreateDefaultObject.Create<CustomerProductAssignment>();

            #region AddChargeScheduleToCustomer Mock

            var addChargeReq = Arg.Is<AddChargeScheduleToCustomerRequest>(x => x.AccountCharged == accountMock);

            var Response = new AddChargeScheduleToCustomerResponse() { CreatedSchedule = CreateDefaultObject.Create<CustomerChargeSchedule>() };

            mockedAddChargeSchedule.Process(addChargeReq, Arg.Any<RequestInvokationEnvironment>()).Returns(Response);

            #endregion

            #region CustomerHasCredit Mock

            ServicesInfo masterBundle = CreateDefaultObject.Create<ServicesInfo>();
            masterBundle.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            masterBundle.BundleDefinition.BundleID = testParameter;

            var hasCreditReq = Arg.Is<CustomerHasCreditRequest>(x => x.CustomerInfo.CustomerID == 1);
            var hasCreditResp = new CustomerHasCreditResponse() { HasCredit = true, MasterBundle = masterBundle, ResultType = ResultTypes.Ok, };
            mockedHasCredit.Process(hasCreditReq, Arg.Any<RequestInvokationEnvironment>()).Returns(hasCreditResp);

            #endregion

            #region AddUnbilled Mock

            var addUnbilledReq = Arg.Is<AddUnbilledBalanceRequest>(x => x.ServicesInfo.BundleDefinition.BundleID == testParameter);
            var addUnbilledResp = CreateDefaultObject.Create<AddUnbilledBalanceResponse>();
            addUnbilledResp.ResultType = ResultTypes.Ok;
            mockedAddUnbilledBalance.Process(addUnbilledReq, Arg.Any<RequestInvokationEnvironment>()).Returns(addUnbilledResp);

            #endregion

            #region Get Configuration
            var mockGetConfigurationRepo = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var config = CreateDefaultObject.Create<ApplyChargeAndScheduleConfiguration>();
            config.SendChargeEvent = true;

            var mockBusinessOprepo = MockRepositoryManager.GetMockedRepository<IBusinessOperationRepository<BusinessOperation>>();
            mockBusinessOprepo.GetById(Arg.Any<int>()).ReturnsForAnyArgs(new ApplyChargeAndScheduleBizOp());


            var operationConfig = new OperationConfiguration()
            {
                Operation = new ApplyChargeAndScheduleBizOp(),
                EndDate = null,
                StarTime = DateTime.Now.AddDays(-1),
                JSonConfig = JsonConvert.SerializeObject(config),
            };

            mockGetConfigurationRepo.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfig });
            #endregion

            ChargeRecurring chargeRecurring = CreateDefaultObject.Create<ChargeRecurring>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.CustomerID = testParameter;

            var bizOp = new ApplyChargeAndScheduleBizOp();

            var request = new ApplyChargeAndScheduleRequest()
            {
                ChargeToAdd = chargeRecurring,
                Account = accountMock,
                Customer = customerInfoMock,
                StartDate = startDate,
                CustomerProductAssignment = customerProductAssignmentMock,
                PriceEffectiveDate = priceEffectiveDate,
                CustomerDealer = CreateDefaultObject.Create<DealerInfo>(),
                CycleNumber = 1,
                InvoiceOfCharge = CreateDefaultObject.Create<Invoice>(),
                Schedule = CreateDefaultObject.Create<CustomerChargeSchedule>(),
                TaxDefinition = CreateDefaultObject.Create<TaxDefinition>(),
                CustomAmount = 900,
                TypeOfCharges = ApplyChargeAndScheduleBizOp.TypeOfCharges.AllChargesAllowed
            };

            var resp = bizOp.ProcessRequest(null, request);

            AssertExt.ObjectPropertiesAreEqual(resp.ScheduleAdded, Response.CreatedSchedule);
            Assert.IsTrue(resp.ChargeAdde == null);
            Assert.IsTrue(resp.ResultType == ResultTypes.Ok);
            Assert.IsNotNull(resp.Subscription);
        }

        [TestCase(100)]
        public void ApplyChargeAndScheduleBizOp_RecurringNOk_ShouldReturnExceptionBecauseOfTypeOfCharge(int testParameter)
        {
            var accountMock = CreateDefaultObject.Create<Account>();
            var customerInfoMock = CreateDefaultObject.Create<CustomerInfo>();
            var customerProductAssignmentMock = CreateDefaultObject.Create<CustomerProductAssignment>();

            #region AddChargeScheduleToCustomer Mock

            var addChargeReq = Arg.Is<AddChargeScheduleToCustomerRequest>(x => x.AccountCharged == accountMock);

            var Response = new AddChargeScheduleToCustomerResponse() { CreatedSchedule = CreateDefaultObject.Create<CustomerChargeSchedule>() };

            mockedAddChargeSchedule.Process(addChargeReq, Arg.Any<RequestInvokationEnvironment>()).Returns(Response);

            #endregion

            #region Get Configuration
            var mockGetConfigurationRepo = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var config = CreateDefaultObject.Create<ApplyChargeAndScheduleConfiguration>();
            config.SendChargeEvent = true;

            var mockBusinessOprepo = MockRepositoryManager.GetMockedRepository<IBusinessOperationRepository<BusinessOperation>>();
            mockBusinessOprepo.GetById(Arg.Any<int>()).ReturnsForAnyArgs(new ApplyChargeAndScheduleBizOp());


            var operationConfig = new OperationConfiguration()
            {
                Operation = new ApplyChargeAndScheduleBizOp(),
                EndDate = null,
                StarTime = DateTime.Now.AddDays(-1),
                JSonConfig = JsonConvert.SerializeObject(config),
            };

            mockGetConfigurationRepo.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfig });
            #endregion

            #region CustomerHasCredit Mock

            ServicesInfo masterBundle = CreateDefaultObject.Create<ServicesInfo>();
            masterBundle.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            masterBundle.BundleDefinition.BundleID = testParameter;

            var hasCreditReq = Arg.Is<CustomerHasCreditRequest>(x => x.CustomerInfo.CustomerID == 1);
            var hasCreditResp = new CustomerHasCreditResponse() { HasCredit = true, MasterBundle = masterBundle, ResultType = ResultTypes.Ok, };
            mockedHasCredit.Process(hasCreditReq, Arg.Any<RequestInvokationEnvironment>()).Returns(hasCreditResp);

            #endregion

            #region AddUnbilled Mock

            var addUnbilledReq = Arg.Is<AddUnbilledBalanceRequest>(x => x.ServicesInfo.BundleDefinition.BundleID == testParameter);
            var addUnbilledResp = CreateDefaultObject.Create<AddUnbilledBalanceResponse>();
            addUnbilledResp.ResultType = ResultTypes.Ok;
            mockedAddUnbilledBalance.Process(addUnbilledReq, Arg.Any<RequestInvokationEnvironment>()).Returns(addUnbilledResp);

            #endregion

            ChargeRecurring chargeRecurring = CreateDefaultObject.Create<ChargeRecurring>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.CustomerID = testParameter;

            var bizOp = new ApplyChargeAndScheduleBizOp();

            var request = new ApplyChargeAndScheduleRequest()
            {
                ChargeToAdd = chargeRecurring,
                Account = accountMock,
                Customer = customerInfoMock,
                StartDate = startDate,
                CustomerProductAssignment = customerProductAssignmentMock,
                PriceEffectiveDate = priceEffectiveDate,
                CustomerDealer = CreateDefaultObject.Create<DealerInfo>(),
                CycleNumber = 1,
                InvoiceOfCharge = CreateDefaultObject.Create<Invoice>(),
                Schedule = CreateDefaultObject.Create<CustomerChargeSchedule>(),
                TaxDefinition = CreateDefaultObject.Create<TaxDefinition>(),
                CustomAmount = 900,
                TypeOfCharges = ApplyChargeAndScheduleBizOp.TypeOfCharges.NonRecurringChargeAllowed
            };

            Assert.Throws<BusinessLogicErrorException>(() => bizOp.ProcessRequest(null, request));

        }
        
        [TestCase(200)]
        public void ApplyChargeAndScheduleBizOp_ShouldReturnThrowException(int testParameter)
        {

            #region Get Configuration
            var mockGetConfigurationRepo = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var config = CreateDefaultObject.Create<ApplyChargeAndScheduleConfiguration>();
            config.SendChargeEvent = true;

            var mockBusinessOprepo = MockRepositoryManager.GetMockedRepository<IBusinessOperationRepository<BusinessOperation>>();
            mockBusinessOprepo.GetById(Arg.Any<int>()).ReturnsForAnyArgs(new ApplyChargeAndScheduleBizOp());


            var operationConfig = new OperationConfiguration()
            {
                Operation = new ApplyChargeAndScheduleBizOp(),
                EndDate = null,
                StarTime = DateTime.Now.AddDays(-1),
                JSonConfig = JsonConvert.SerializeObject(config),
            };

            mockGetConfigurationRepo.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfig });
            #endregion

            var getChargeByIdMS = MockMicroService<GetChargeByIdRequest, GetChargeByIdResponse>();
            var getChargeByIdRequest = Arg.Is<GetChargeByIdRequest>(x => x.ChargeId == testParameter);

            getChargeByIdMS.Process(getChargeByIdRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            var getLastInvoiceMS =
                MockMicroService<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest, GetLastNInvoicesByCustomerIdAndInvoiceStatusesResponse>();
            var getLastInvoiceRequest = Arg.Is<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest>(x => x.CustomerId == testParameter);
            getLastInvoiceMS.Process(getLastInvoiceRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            var getCustomerProductAssignment =
                MockMicroService<GetCustomerProductAssignmentsByCustomerIDAndDatePeriodRequest, GetCustomerProductAssignmentsByCustomerIDAndDatePeriodResponse>(
                    );
            var getCustomerProductAssignmentRequest =
                Arg.Is<GetCustomerProductAssignmentsByCustomerIDAndDatePeriodRequest>(
                    x => x.CustomerID == testParameter && x.StartDate == startDate && x.EndDate == startDate);
            getCustomerProductAssignment.Process(getCustomerProductAssignmentRequest, Arg.Any<RequestInvokationEnvironment>()).
                Returns(x => { throw new Exception("Error"); });

            var getDealerByIdMS = MockMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerByIdRequest = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == testParameter);
            getDealerByIdMS.Process(getDealerByIdRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            var getScheduleByIdMS = MockMicroService<GetCustomerChargesScheduleByIdRequest, GetCustomerChargesScheduleByIdResponse>();
            var getScheduleByIdRequest = Arg.Is<GetCustomerChargesScheduleByIdRequest>(x => x.customerChargeId == testParameter);
            getScheduleByIdMS.Process(getScheduleByIdRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            var getTaxDefinition = MockMicroService<GetTaxDefinitonsByCategoryRequest, GetTaxDefinitonsByCategoryResponse>();
            var getTaxDefinitionRequest = Arg.Is<GetTaxDefinitonsByCategoryRequest>(x => x.TaxCategory == testParameter);
            getTaxDefinition.Process(getTaxDefinitionRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            var applyChargeAndScheduleDTORequest = new ApplyChargeAndScheduleDTORequest()
                                                       {
                                                           CustomerId = testParameter,
                                                           AccountId = testParameter,
                                                           Amount = 900,
                                                           ChargeId = testParameter,
                                                           CustomerProductAssignmentId = testParameter,
                                                           InvoiceId = testParameter,
                                                           PriceEffectiveDate = priceEffectiveDate,
                                                           ScheduleId = testParameter,
                                                           StartDate = startDate,
                                                           TaxCategory = testParameter,
                                                           vmno = "970100",
                                                           password = "123456",
                                                           user = "1644000204"
                                                       };
            MockAbstractSinglePhaseOrderProcessor(applyChargeAndScheduleDTORequest);
            var response = CallBizOp(applyChargeAndScheduleDTORequest);
            Assert.IsTrue(response.resultType == ResultTypes.UnknownError);
        }

        [TestCase(300)]
        public void ApplyChargeAndScheduleBizOp_ProcessRequestNonRecurring_CustomerChargeNull_ReturnThrowBusinessLogicError(int testParameter)
        {
            #region AddChargeToCustomer Mock

            var addChargeReq = Arg.Is<AddChargeToCustomerRequest>(x => x.Amount == 900);
            mockedAddCharge.Process(addChargeReq, Arg.Any<RequestInvokationEnvironment>()).Returns(new AddChargeToCustomerResponse() { ChargeCreated = null });

            #endregion

            #region Get Configuration
            var mockGetConfigurationRepo = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var config = CreateDefaultObject.Create<ApplyChargeAndScheduleConfiguration>();
            config.SendChargeEvent = true;

            var mockBusinessOprepo = MockRepositoryManager.GetMockedRepository<IBusinessOperationRepository<BusinessOperation>>();
            mockBusinessOprepo.GetById(Arg.Any<int>()).ReturnsForAnyArgs(new ApplyChargeAndScheduleBizOp());


            var operationConfig = new OperationConfiguration()
            {
                Operation = new ApplyChargeAndScheduleBizOp(),
                EndDate = null,
                StarTime = DateTime.Now.AddDays(-1),
                JSonConfig = JsonConvert.SerializeObject(config),
            };

            mockGetConfigurationRepo.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfig });
            #endregion

            #region CustomerHasCredit Mock

            ServicesInfo masterBundle = CreateDefaultObject.Create<ServicesInfo>();
            masterBundle.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            masterBundle.BundleDefinition.BundleID = testParameter;

            var hasCreditReq = Arg.Is<CustomerHasCreditRequest>(x => x.CustomerInfo.CustomerID == 1);
            mockedHasCredit.Process(hasCreditReq, Arg.Any<RequestInvokationEnvironment>()).
                Returns(new CustomerHasCreditResponse() { HasCredit = true, MasterBundle = null });

            #endregion

            #region AddUnbilled Mock

            var addUnbilledReq =
                Arg.Is<AddUnbilledBalanceRequest>(
                    x => x.ServicesInfo.BundleDefinition.BundleID == testParameter && x.ServicesInfo.UnBilledBalance == testParameter);
            mockedAddUnbilledBalance.Process(addUnbilledReq, Arg.Any<RequestInvokationEnvironment>()).
                Returns(new AddUnbilledBalanceResponse() { ServicesInfo = null });

            #endregion

            ChargeNonRecurring chargeNonRecurring = CreateDefaultObject.Create<ChargeNonRecurring>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.CustomerID = testParameter;

            var bizOp = new ApplyChargeAndScheduleBizOp();

            var request = new ApplyChargeAndScheduleRequest()
                              {
                                  ChargeToAdd = chargeNonRecurring,
                                  Account = CreateDefaultObject.Create<Account>(),
                                  CustomAmount = 900,
                                  CustomerDealer = CreateDefaultObject.Create<DealerInfo>(),
                                  CustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>(),
                                  Customer = customerInfo,
                                  CycleNumber = 1,
                                  InvoiceOfCharge = CreateDefaultObject.Create<Invoice>(),
                                  PriceEffectiveDate = priceEffectiveDate,
                                  Schedule = CreateDefaultObject.Create<CustomerChargeSchedule>(),
                                  StartDate = startDate,
                                  TaxDefinition = CreateDefaultObject.Create<TaxDefinition>(),
                              };

            Assert.Throws<BusinessLogicErrorException>(() => bizOp.ProcessRequest(null, request));
        }

        [TestCase(300)]
        public void ApplyChargeAndScheduleBizOp_ProcessRequestRecurring_masterBundleNull_ReturnThrowBusinessLogicError(int testParameter)
        {
            var AccountMock = CreateDefaultObject.Create<Account>();
            var CustomerInfoMock = CreateDefaultObject.Create<CustomerInfo>();
            var CustomerProductAssignmentMock = CreateDefaultObject.Create<CustomerProductAssignment>();

            #region AddChargeScheduleToCustomer Mock

            var addChargeReq = Arg.Is<AddChargeScheduleToCustomerRequest>(x => x.AccountCharged == AccountMock);

            var Response = new AddChargeScheduleToCustomerResponse() { CreatedSchedule = null };

            mockedAddChargeSchedule.Process(addChargeReq, Arg.Any<RequestInvokationEnvironment>()).Returns(Response);

            #endregion

            #region Get Configuration
            var mockGetConfigurationRepo = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var config = CreateDefaultObject.Create<ApplyChargeAndScheduleConfiguration>();
            config.SendChargeEvent = true;

            var mockBusinessOprepo = MockRepositoryManager.GetMockedRepository<IBusinessOperationRepository<BusinessOperation>>();
            mockBusinessOprepo.GetById(Arg.Any<int>()).ReturnsForAnyArgs(new ApplyChargeAndScheduleBizOp());


            var operationConfig = new OperationConfiguration()
            {
                Operation = new ApplyChargeAndScheduleBizOp(),
                EndDate = null,
                StarTime = DateTime.Now.AddDays(-1),
                JSonConfig = JsonConvert.SerializeObject(config),
            };

            mockGetConfigurationRepo.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfig });
            #endregion

            #region CustomerHasCredit Mock

            ServicesInfo masterBundle = CreateDefaultObject.Create<ServicesInfo>();
            masterBundle.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            masterBundle.BundleDefinition.BundleID = testParameter;

            var hasCreditReq = Arg.Is<CustomerHasCreditRequest>(x => x.CustomerInfo.CustomerID == 1);
            mockedHasCredit.Process(hasCreditReq, Arg.Any<RequestInvokationEnvironment>()).
                Returns(new CustomerHasCreditResponse() { HasCredit = true, MasterBundle = null });

            #endregion

            #region AddUnbilled Mock

            var addUnbilledReq =
                Arg.Is<AddUnbilledBalanceRequest>(
                    x => x.ServicesInfo.BundleDefinition.BundleID == testParameter && x.ServicesInfo.UnBilledBalance == testParameter);
            mockedAddUnbilledBalance.Process(addUnbilledReq, Arg.Any<RequestInvokationEnvironment>()).
                Returns(new AddUnbilledBalanceResponse() { ServicesInfo = null });

            #endregion

            ChargeRecurring chargeRecurring = CreateDefaultObject.Create<ChargeRecurring>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.CustomerID = testParameter;

            var bizOp = new ApplyChargeAndScheduleBizOp();

            var request = new ApplyChargeAndScheduleRequest()
                              {
                                  ChargeToAdd = chargeRecurring,
                                  Account = AccountMock,
                                  Customer = CustomerInfoMock,
                                  StartDate = startDate,
                                  CustomerProductAssignment = CustomerProductAssignmentMock,
                                  PriceEffectiveDate = priceEffectiveDate,
                                  CustomerDealer = CreateDefaultObject.Create<DealerInfo>(),
                                  CycleNumber = 1,
                                  InvoiceOfCharge = CreateDefaultObject.Create<Invoice>(),
                                  Schedule = CreateDefaultObject.Create<CustomerChargeSchedule>(),
                                  TaxDefinition = CreateDefaultObject.Create<TaxDefinition>(),
                                  CustomAmount = 900,
                              };

            Assert.Throws<BusinessLogicErrorException>(() => bizOp.ProcessRequest(null, request));
        }


        [TestCase(100)]
        public void ApplyChargeAndScheduleBizOp_NonRecurringOk_ShouldReturnCustomerCharge_AmountIsInformational_True(int testParameter)
        {
            #region AddChargeToCustomer Mock

            var addChargeReq = Arg.Is<AddChargeToCustomerRequest>(x => x.Amount == 900&&x.AmountIsInformational==true);
            var addchargeResp = CreateDefaultObject.Create<AddChargeToCustomerResponse>();
            addchargeResp.ChargeCreated = CreateDefaultObject.Create<CustomerCharge>();
            addchargeResp.ChargeCreated.Amount = 0;
            addchargeResp.ChargeCreated.InformationalAmount = 900;
            mockedAddCharge.Process(addChargeReq, Arg.Any<RequestInvokationEnvironment>()).Returns(addchargeResp);

            #endregion

            #region Get Configuration
            var mockGetConfigurationRepo = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var config = CreateDefaultObject.Create<ApplyChargeAndScheduleConfiguration>();
            config.SendChargeEvent = true;

            var mockBusinessOprepo = MockRepositoryManager.GetMockedRepository<IBusinessOperationRepository<BusinessOperation>>();
            mockBusinessOprepo.GetById(Arg.Any<int>()).ReturnsForAnyArgs(new ApplyChargeAndScheduleBizOp());


            var operationConfig = new OperationConfiguration()
            {
                Operation = new ApplyChargeAndScheduleBizOp(),
                EndDate = null,
                StarTime = DateTime.Now.AddDays(-1),
                JSonConfig = JsonConvert.SerializeObject(config),
            };

            mockGetConfigurationRepo.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfig });
            #endregion

            #region CustomerHasCredit Mock

            ServicesInfo masterBundle = CreateDefaultObject.Create<ServicesInfo>();
            masterBundle.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            masterBundle.BundleDefinition.BundleID = testParameter;

            var hasCreditReq = Arg.Is<CustomerHasCreditRequest>(x => x.CustomerInfo.CustomerID == testParameter);
            var hasCreditResp = new CustomerHasCreditResponse() { HasCredit = true, MasterBundle = masterBundle, ResultType = ResultTypes.Ok, };
            mockedHasCredit.Process(hasCreditReq, Arg.Any<RequestInvokationEnvironment>()).Returns(hasCreditResp);

            #endregion

            #region AddUnbilled Mock

            var addUnbilledReq = Arg.Is<AddUnbilledBalanceRequest>(x => x.ServicesInfo.BundleDefinition.BundleID == testParameter);
            var addUnbilledResp = CreateDefaultObject.Create<AddUnbilledBalanceResponse>();
            addUnbilledResp.ResultType = ResultTypes.Ok;
            mockedAddUnbilledBalance.Process(addUnbilledReq, Arg.Any<RequestInvokationEnvironment>()).Returns(addUnbilledResp);

            #endregion

            ChargeNonRecurring chargeNonRecurring = CreateDefaultObject.Create<ChargeNonRecurring>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.CustomerID = testParameter;

            var bizOp = new ApplyChargeAndScheduleBizOp();

            var request = new ApplyChargeAndScheduleRequest()
            {
                ChargeToAdd = chargeNonRecurring,
                Account = CreateDefaultObject.Create<Account>(),
                CustomAmount = 900,
                CustomerDealer = CreateDefaultObject.Create<DealerInfo>(),
                CustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>(),
                Customer = customerInfo,
                CycleNumber = 1,
                InvoiceOfCharge = CreateDefaultObject.Create<Invoice>(),
                PriceEffectiveDate = priceEffectiveDate,
                Schedule = CreateDefaultObject.Create<CustomerChargeSchedule>(),
                StartDate = startDate,
                TaxDefinition = CreateDefaultObject.Create<TaxDefinition>(),
                TypeOfCharges = ApplyChargeAndScheduleBizOp.TypeOfCharges.NonRecurringChargeAllowed,
                AmountIsInformational=true
            };

            var resp = bizOp.ProcessRequest(null, request);

            AssertExt.ObjectPropertiesAreEqual(resp.ChargeAdde, addchargeResp.ChargeCreated);
            Assert.IsTrue(resp.ScheduleAdded == null);

            Assert.AreEqual(resp.ChargeAdde.Amount,0);
            Assert.AreEqual(resp.ChargeAdde.InformationalAmount, 900);
            Assert.IsTrue(resp.ResultType == ResultTypes.Ok);
            Assert.IsNotNull(resp.Subscription);
        }

        [TestCase(100)]
        public void ApplyChargeAndScheduleBizOp_NonRecurringOk_ShouldReturnCustomerCharge_AmountIsInformational_False(int testParameter)
        {
            #region AddChargeToCustomer Mock

            var addChargeReq = Arg.Is<AddChargeToCustomerRequest>(x => x.Amount == 900 && x.AmountIsInformational == false);
            var addchargeResp = CreateDefaultObject.Create<AddChargeToCustomerResponse>();
            addchargeResp.ChargeCreated = CreateDefaultObject.Create<CustomerCharge>();
            addchargeResp.ChargeCreated.Amount = 900;
            mockedAddCharge.Process(addChargeReq, Arg.Any<RequestInvokationEnvironment>()).Returns(addchargeResp);

            #endregion

            #region Get Configuration
            var mockGetConfigurationRepo = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var config = CreateDefaultObject.Create<ApplyChargeAndScheduleConfiguration>();
            config.SendChargeEvent = true;

            var mockBusinessOprepo = MockRepositoryManager.GetMockedRepository<IBusinessOperationRepository<BusinessOperation>>();
            mockBusinessOprepo.GetById(Arg.Any<int>()).ReturnsForAnyArgs(new ApplyChargeAndScheduleBizOp());


            var operationConfig = new OperationConfiguration()
            {
                Operation = new ApplyChargeAndScheduleBizOp(),
                EndDate = null,
                StarTime = DateTime.Now.AddDays(-1),
                JSonConfig = JsonConvert.SerializeObject(config),
            };

            mockGetConfigurationRepo.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfig });
            #endregion

            #region CustomerHasCredit Mock

            ServicesInfo masterBundle = CreateDefaultObject.Create<ServicesInfo>();
            masterBundle.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            masterBundle.BundleDefinition.BundleID = testParameter;

            var hasCreditReq = Arg.Is<CustomerHasCreditRequest>(x => x.CustomerInfo.CustomerID == testParameter);
            var hasCreditResp = new CustomerHasCreditResponse() { HasCredit = true, MasterBundle = masterBundle, ResultType = ResultTypes.Ok, };
            mockedHasCredit.Process(hasCreditReq, Arg.Any<RequestInvokationEnvironment>()).Returns(hasCreditResp);

            #endregion

            #region AddUnbilled Mock

            var addUnbilledReq = Arg.Is<AddUnbilledBalanceRequest>(x => x.ServicesInfo.BundleDefinition.BundleID == testParameter);
            var addUnbilledResp = CreateDefaultObject.Create<AddUnbilledBalanceResponse>();
            addUnbilledResp.ResultType = ResultTypes.Ok;
            mockedAddUnbilledBalance.Process(addUnbilledReq, Arg.Any<RequestInvokationEnvironment>()).Returns(addUnbilledResp);

            #endregion

            ChargeNonRecurring chargeNonRecurring = CreateDefaultObject.Create<ChargeNonRecurring>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.CustomerID = testParameter;

            var bizOp = new ApplyChargeAndScheduleBizOp();

            var request = new ApplyChargeAndScheduleRequest()
            {
                ChargeToAdd = chargeNonRecurring,
                Account = CreateDefaultObject.Create<Account>(),
                CustomAmount = 900,
                CustomerDealer = CreateDefaultObject.Create<DealerInfo>(),
                CustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>(),
                Customer = customerInfo,
                CycleNumber = 1,
                InvoiceOfCharge = CreateDefaultObject.Create<Invoice>(),
                PriceEffectiveDate = priceEffectiveDate,
                Schedule = CreateDefaultObject.Create<CustomerChargeSchedule>(),
                StartDate = startDate,
                TaxDefinition = CreateDefaultObject.Create<TaxDefinition>(),
                TypeOfCharges = ApplyChargeAndScheduleBizOp.TypeOfCharges.NonRecurringChargeAllowed,
                AmountIsInformational = false
            };

            var resp = bizOp.ProcessRequest(null, request);

            AssertExt.ObjectPropertiesAreEqual(resp.ChargeAdde, addchargeResp.ChargeCreated);
            Assert.IsTrue(resp.ScheduleAdded == null);
            Assert.AreEqual(resp.ChargeAdde.Amount, 900);
            Assert.IsTrue(resp.ResultType == ResultTypes.Ok);
            Assert.IsNotNull(resp.Subscription);
        }

        [TestCase(100)]
        public void ApplyChargeAndScheduleBizOp_NonRecurringOk_ScheduleNull_ShouldReturnCustomerCharge(int testParameter)
        {
            #region AddChargeToCustomer Mock

            var addChargeReq = Arg.Is<AddChargeToCustomerRequest>(x => x.Amount == 900);
            var addchargeResp = CreateDefaultObject.Create<AddChargeToCustomerResponse>();
            addchargeResp.ChargeCreated = CreateDefaultObject.Create<CustomerCharge>();
            addchargeResp.ChargeCreated.Amount = 999;
            mockedAddCharge.Process(addChargeReq, Arg.Any<RequestInvokationEnvironment>()).Returns(addchargeResp);

            #endregion

            #region Get Configuration
            var mockGetConfigurationRepo = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var config = CreateDefaultObject.Create<ApplyChargeAndScheduleConfiguration>();
            config.SendChargeEvent = true;

            var mockBusinessOprepo = MockRepositoryManager.GetMockedRepository<IBusinessOperationRepository<BusinessOperation>>();
            mockBusinessOprepo.GetById(Arg.Any<int>()).ReturnsForAnyArgs(new ApplyChargeAndScheduleBizOp());


            var operationConfig = new OperationConfiguration()
            {
                Operation = new ApplyChargeAndScheduleBizOp(),
                EndDate = null,
                StarTime = DateTime.Now.AddDays(-1),
                JSonConfig = JsonConvert.SerializeObject(config),
            };

            mockGetConfigurationRepo.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfig });
            #endregion

            #region CustomerHasCredit Mock

            ServicesInfo masterBundle = CreateDefaultObject.Create<ServicesInfo>();
            masterBundle.BundleDefinition = CreateDefaultObject.Create<BundleInfo>();
            masterBundle.BundleDefinition.BundleID = testParameter;

            var hasCreditReq = Arg.Is<CustomerHasCreditRequest>(x => x.CustomerInfo.CustomerID == testParameter);
            var hasCreditResp = new CustomerHasCreditResponse() { HasCredit = true, MasterBundle = masterBundle, ResultType = ResultTypes.Ok, };
            mockedHasCredit.Process(hasCreditReq, Arg.Any<RequestInvokationEnvironment>()).Returns(hasCreditResp);

            #endregion

            #region AddUnbilled Mock

            var addUnbilledReq = Arg.Is<AddUnbilledBalanceRequest>(x => x.ServicesInfo.BundleDefinition.BundleID == testParameter);
            var addUnbilledResp = CreateDefaultObject.Create<AddUnbilledBalanceResponse>();
            addUnbilledResp.ResultType = ResultTypes.Ok;
            mockedAddUnbilledBalance.Process(addUnbilledReq, Arg.Any<RequestInvokationEnvironment>()).Returns(addUnbilledResp);

            #endregion

            ChargeNonRecurring chargeNonRecurring = CreateDefaultObject.Create<ChargeNonRecurring>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.CustomerID = testParameter;
            customerInfo.ResourceMBInfo = new List<ResourceMBInfo>();

            var bizOp = new ApplyChargeAndScheduleBizOp();

            var request = new ApplyChargeAndScheduleRequest()
            {
                ChargeToAdd = chargeNonRecurring,
                Account = CreateDefaultObject.Create<Account>(),
                CustomAmount = 900,
                CustomerDealer = CreateDefaultObject.Create<DealerInfo>(),
                CustomerProductAssignment = CreateDefaultObject.Create<CustomerProductAssignment>(),
                Customer = customerInfo,
                CycleNumber = 1,
                InvoiceOfCharge = CreateDefaultObject.Create<Invoice>(),
                PriceEffectiveDate = priceEffectiveDate,
                Schedule = null,
                StartDate = startDate,
                TaxDefinition = CreateDefaultObject.Create<TaxDefinition>(),
                TypeOfCharges = ApplyChargeAndScheduleBizOp.TypeOfCharges.NonRecurringChargeAllowed
            };

            var resp = bizOp.ProcessRequest(null, request);

            AssertExt.ObjectPropertiesAreEqual(resp.ChargeAdde, addchargeResp.ChargeCreated);
            Assert.IsTrue(resp.ScheduleAdded == null);
            Assert.IsTrue(resp.ResultType == ResultTypes.Ok);
            Assert.IsNull(resp.Subscription);
        }
    }
}