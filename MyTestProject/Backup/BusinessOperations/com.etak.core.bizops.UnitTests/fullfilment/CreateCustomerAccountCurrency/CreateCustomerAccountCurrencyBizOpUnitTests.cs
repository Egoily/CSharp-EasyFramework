using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.CreateCustomerAccountCurrency;
using com.etak.core.customer.message.CreateAccountCurrency;
using com.etak.core.GSMSubscription.messages.GetResourceMBInfosByCustomerID;
using com.etak.core.microservices.messages.GetMultiLingualDescriptionById;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.test.utilities.abstracts;
using NUnit.Framework;
using com.etak.core.operation.contract;
using com.etak.core.product.message.GetBillCyclesByVMNO;
using com.etak.core.repository.crm.configuration;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.Helpers;
using Newtonsoft.Json;
using NSubstitute;

namespace com.etak.core.bizops.UnitTests.fullfilment.CreateCustomerAccountCurrency
{
    [TestFixture]
    public class CreateCustomerAccountCurrencyBizOpUnitTests : AbstractSinglePhaseOrderProcessorTest<CreateCustomerAccountCurrencyBizOp,
        CreateCustomerAccountCurrencyRequestDTO, CreateCustomerAccountCurrencyResponseDTO,
        CreateCustomerAccountCurrencyRequestInternal, CreateCustomerAccountCurrencyResponseInternal, CreateCustomerAccountCurrencyOrder>
    {

        private IMicroService<CreateAccountCurrencyRequest, CreateAccountCurrencyResponse> _createAccountCurrencyMSMock;

        private IMicroService<GetMultiLingualDescriptionByIdRequest, GetMultiLingualDescriptionByIdResponse>
            _getMultiLingualDescriptionByIdMSMock;

        private IMicroService<GetBillCyclesByVMNORequest, GetBillCyclesByVMNOResponse> _getBillCycleByVMNOMSMock;
        private IMicroService<GetResourceMBInfosByCustomerIDRequest, GetResourceMBInfosByCustomerIDResponse> _getSubscriptionByCustomerIDMSMock;

        private int expectedcustomerid = 99;

        [TestFixtureSetUp]
        public void InitializeTest()
        {
            FakeSessionFactorySingleton.Init();
            _createAccountCurrencyMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService<CreateAccountCurrencyRequest, CreateAccountCurrencyResponse>();
            _getMultiLingualDescriptionByIdMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetMultiLingualDescriptionByIdRequest, GetMultiLingualDescriptionByIdResponse>();
            _getBillCycleByVMNOMSMock =
                MockMicroServiceManager.GetMockedMicroService<GetBillCyclesByVMNORequest, GetBillCyclesByVMNOResponse>();
            _getSubscriptionByCustomerIDMSMock =
    MockMicroServiceManager.GetMockedMicroService<GetResourceMBInfosByCustomerIDRequest, GetResourceMBInfosByCustomerIDResponse>();
        }

        [TestCase(100)]
        public void ProcessRequest_CorrectRequestGiven_ShouldCreateAccountCurrencyCorrectly(int testParameter)
        {
            #region Mock OperationConfig
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var operationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var createCustomerAccountCurrencyConfig = new CreateCustomerAccountCurrencyConfiguration()
            {
                AccountCurrency = ISO4217CurrencyCodes.EUR,
                AccountDescriptionId = testParameter,
                AccountNameId = testParameter,
                BillcycleId = 1,
            };
            operationConfiguration.JSonConfig = JsonConvert.SerializeObject(createCustomerAccountCurrencyConfig);
            operationConfiguration.StarTime = DateTime.Now;
            operationConfiguration.EndDate = DateTime.Now.AddYears(1);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfiguration });
            #endregion

            #region Mock GetBillCycleMS

            var getBillCycleRes = new GetBillCyclesByVMNOResponse()
            {
                BillCycles = new List<BillCycle> { CreateDefaultObject.Create<BillCycle>() },
                ResultType = ResultTypes.Ok,
            };
            getBillCycleRes.BillCycles.FirstOrDefault().Id = 1;

            _getBillCycleByVMNOMSMock.Process(Arg.Any<GetBillCyclesByVMNORequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(getBillCycleRes);
            #endregion

            #region Mock GetMultiLingualDescriptionMs

            var getMultiLingualDescriptionRes = new GetMultiLingualDescriptionByIdResponse()
            {
                MultiLingualDescription = CreateDefaultObject.Create<MultiLingualDescription>(),
                ResultType = ResultTypes.Ok
            };

            _getMultiLingualDescriptionByIdMSMock.Process(Arg.Any<GetMultiLingualDescriptionByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>()).Returns(getMultiLingualDescriptionRes);
            #endregion

            #region Mock CreateAccountCurrencyMS

            var createAccountCurrencyRes = new CreateAccountCurrencyResponse()
            {
                AccountCurrency = CreateDefaultObject.Create<AccountCurrency>(),
                ResultType = ResultTypes.Ok,
            };

            _createAccountCurrencyMSMock.Process(Arg.Any<CreateAccountCurrencyRequest>(),
                Arg.Any<RequestInvokationEnvironment>()).Returns(createAccountCurrencyRes);

            #endregion

            #region Mock GetResourceMBbyCustomerIDMS

            var mockResourceMBInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            mockResourceMBInfo.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockResourceMBInfo.CustomerInfo.CustomerID = expectedcustomerid;
            var getResourceMBInfosByCustomerIDResponse = new GetResourceMBInfosByCustomerIDResponse()
            {
                ResourceMbInfos = new List<ResourceMBInfo> { mockResourceMBInfo },
                ResultType = ResultTypes.Ok,
            };

            _getSubscriptionByCustomerIDMSMock.Process(Arg.Any<GetResourceMBInfosByCustomerIDRequest>(),
                Arg.Any<RequestInvokationEnvironment>()).Returns(getResourceMBInfosByCustomerIDResponse);

            #endregion

            var createCustomerAccountCurrencyRequestDTO = new CreateCustomerAccountCurrencyRequestDTO()
            {
                CustomerDto = CreateDefaultObject.Create<CustomerDTO>(),
                user = "1644000204",
                password = "123456",
                vmno = "970100"
            };

            createCustomerAccountCurrencyRequestDTO.CustomerDto.ChildCustomers = new List<int>();
            createCustomerAccountCurrencyRequestDTO.CustomerDto.CustomerData =
                CreateDefaultObject.Create<CustomerDataDTO>();
            createCustomerAccountCurrencyRequestDTO.CustomerDto.CustomerId = expectedcustomerid;

            MockAbstractSinglePhaseOrderProcessor(createCustomerAccountCurrencyRequestDTO);

            var actualcreateCustomerAccountCurrencyResponseDTO = CallBizOp(createCustomerAccountCurrencyRequestDTO);

            var expectedCreateCustomerAccountCurrencyResponseDTO = new CreateCustomerAccountCurrencyResponseDTO
            {
                Customer = new CustomerDTO { CustomerId = expectedcustomerid},
                Subscription = new SubscriptionDTO { CustomerId = expectedcustomerid}
            };

            Assert.AreEqual(ResultTypes.Ok, actualcreateCustomerAccountCurrencyResponseDTO.resultType);
            Assert.IsTrue(expectedCreateCustomerAccountCurrencyResponseDTO.Customer.CustomerId == actualcreateCustomerAccountCurrencyResponseDTO.Customer.CustomerId);
            Assert.IsTrue(expectedCreateCustomerAccountCurrencyResponseDTO.Subscription.CustomerId == actualcreateCustomerAccountCurrencyResponseDTO.Subscription.CustomerId);
        }

        [TestCase(100)]
        public void ProcessRequest_getMultiLingualRespAccDescIsNotOk_ShouldThrowBusinessLogicExcpetion(int testParameter)
        {
            #region Mock OperationConfig
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var operationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var createCustomerAccountCurrencyConfig = new CreateCustomerAccountCurrencyConfiguration()
            {
                AccountCurrency = ISO4217CurrencyCodes.EUR,
                AccountDescriptionId = testParameter,
                AccountNameId = testParameter,
                BillcycleId = 1,
            };
            operationConfiguration.JSonConfig = JsonConvert.SerializeObject(createCustomerAccountCurrencyConfig);
            operationConfiguration.StarTime = DateTime.Now;
            operationConfiguration.EndDate = DateTime.Now.AddYears(1);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfiguration });
            #endregion

            #region Mock GetBillCycleMS

            var getBillCycleRes = new GetBillCyclesByVMNOResponse()
            {
                BillCycles = new List<BillCycle> { CreateDefaultObject.Create<BillCycle>() },
                ResultType = ResultTypes.Ok,
            };
            getBillCycleRes.BillCycles.FirstOrDefault().Id = 1;

            _getBillCycleByVMNOMSMock.Process(Arg.Any<GetBillCyclesByVMNORequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(getBillCycleRes);
            #endregion

            #region Mock GetMultiLingualDescriptionMs

            var getMultiLingualDescriptionRes = new GetMultiLingualDescriptionByIdResponse()
            {
                MultiLingualDescription = CreateDefaultObject.Create<MultiLingualDescription>(),
                ResultType = ResultTypes.BussinessLogicError
            };

            _getMultiLingualDescriptionByIdMSMock.Process(Arg.Any<GetMultiLingualDescriptionByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>()).Returns(getMultiLingualDescriptionRes);
            #endregion

            #region Mock CreateAccountCurrencyMS

            var createAccountCurrencyRes = new CreateAccountCurrencyResponse()
            {
                AccountCurrency = CreateDefaultObject.Create<AccountCurrency>(),
                ResultType = ResultTypes.Ok,
            };

            _createAccountCurrencyMSMock.Process(Arg.Any<CreateAccountCurrencyRequest>(),
                Arg.Any<RequestInvokationEnvironment>()).Returns(createAccountCurrencyRes);

            #endregion

            var createCustomerAccountCurrencyRequestDTO = new CreateCustomerAccountCurrencyRequestDTO()
            {
                CustomerDto = CreateDefaultObject.Create<CustomerDTO>(),
                user = "1644000204",
                password = "123456",
                vmno = "970100"
            };

            createCustomerAccountCurrencyRequestDTO.CustomerDto.ChildCustomers = new List<int>();
            createCustomerAccountCurrencyRequestDTO.CustomerDto.CustomerData =
                CreateDefaultObject.Create<CustomerDataDTO>();

            MockAbstractSinglePhaseOrderProcessor(createCustomerAccountCurrencyRequestDTO);

            var createCustomerAccountCurrencyResponseDTO = CallBizOp(createCustomerAccountCurrencyRequestDTO);

            Assert.AreEqual(ResultTypes.BussinessLogicError, createCustomerAccountCurrencyResponseDTO.resultType);
        }

        [TestCase(100)]
        public void ProcessRequest_NoBillCycleForMVNO_ShouldThrowBusinessLogicExcpetion(int testParameter)
        {
            #region Mock OperationConfig
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var operationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var createCustomerAccountCurrencyConfig = new CreateCustomerAccountCurrencyConfiguration()
            {
                AccountCurrency = ISO4217CurrencyCodes.EUR,
                AccountDescriptionId = testParameter,
                AccountNameId = testParameter,
                BillcycleId = 1,
            };
            operationConfiguration.JSonConfig = JsonConvert.SerializeObject(createCustomerAccountCurrencyConfig);
            operationConfiguration.StarTime = DateTime.Now;
            operationConfiguration.EndDate = DateTime.Now.AddYears(1);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfiguration });
            #endregion

            #region Mock GetBillCycleMS

            var getBillCycleRes = new GetBillCyclesByVMNOResponse()
            {
                BillCycles = new List<BillCycle> { }
            };

            _getBillCycleByVMNOMSMock.Process(Arg.Any<GetBillCyclesByVMNORequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(getBillCycleRes);
            #endregion

            #region Mock GetMultiLingualDescriptionMs

            var getMultiLingualDescriptionRes = new GetMultiLingualDescriptionByIdResponse()
            {
                MultiLingualDescription = CreateDefaultObject.Create<MultiLingualDescription>(),
                ResultType = ResultTypes.Ok
            };

            _getMultiLingualDescriptionByIdMSMock.Process(Arg.Any<GetMultiLingualDescriptionByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>()).Returns(getMultiLingualDescriptionRes);
            #endregion

            #region Mock CreateAccountCurrencyMS

            var createAccountCurrencyRes = new CreateAccountCurrencyResponse()
            {
                AccountCurrency = CreateDefaultObject.Create<AccountCurrency>(),
                ResultType = ResultTypes.Ok,
            };

            _createAccountCurrencyMSMock.Process(Arg.Any<CreateAccountCurrencyRequest>(),
                Arg.Any<RequestInvokationEnvironment>()).Returns(createAccountCurrencyRes);

            #endregion

            var createCustomerAccountCurrencyRequestDTO = new CreateCustomerAccountCurrencyRequestDTO()
            {
                CustomerDto = CreateDefaultObject.Create<CustomerDTO>(),
                user = "1644000204",
                password = "123456",
                vmno = "970100"
            };

            createCustomerAccountCurrencyRequestDTO.CustomerDto.ChildCustomers = new List<int>();
            createCustomerAccountCurrencyRequestDTO.CustomerDto.CustomerData =
                CreateDefaultObject.Create<CustomerDataDTO>();

            MockAbstractSinglePhaseOrderProcessor(createCustomerAccountCurrencyRequestDTO);

            var createCustomerAccountCurrencyResponseDTO = CallBizOp(createCustomerAccountCurrencyRequestDTO);

            Assert.AreEqual(ResultTypes.BussinessLogicError, createCustomerAccountCurrencyResponseDTO.resultType);
        }

        [TestCase(100)]
        public void ProcessRequest_NoBillCycleWithSpecifiedId_ShouldThrowBusinessLogicExcpetion(int testParameter)
        {
            #region Mock OperationConfig
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var operationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var createCustomerAccountCurrencyConfig = new CreateCustomerAccountCurrencyConfiguration()
            {
                AccountCurrency = ISO4217CurrencyCodes.EUR,
                AccountDescriptionId = testParameter,
                AccountNameId = testParameter,
                BillcycleId = testParameter,
            };
            operationConfiguration.JSonConfig = JsonConvert.SerializeObject(createCustomerAccountCurrencyConfig);
            operationConfiguration.StarTime = DateTime.Now;
            operationConfiguration.EndDate = DateTime.Now.AddYears(1);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new List<OperationConfiguration>() { operationConfiguration });
            #endregion

            #region Mock GetBillCycleMS

            var getBillCycleRes = new GetBillCyclesByVMNOResponse()
            {
                BillCycles = new List<BillCycle> { CreateDefaultObject.Create<BillCycle>()}
            };

            getBillCycleRes.BillCycles.FirstOrDefault().Id = 1;

            _getBillCycleByVMNOMSMock.Process(Arg.Any<GetBillCyclesByVMNORequest>(), Arg.Any<RequestInvokationEnvironment>()).Returns(getBillCycleRes);
            #endregion

            #region Mock GetMultiLingualDescriptionMs

            var getMultiLingualDescriptionRes = new GetMultiLingualDescriptionByIdResponse()
            {
                MultiLingualDescription = CreateDefaultObject.Create<MultiLingualDescription>(),
                ResultType = ResultTypes.Ok
            };

            _getMultiLingualDescriptionByIdMSMock.Process(Arg.Any<GetMultiLingualDescriptionByIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>()).Returns(getMultiLingualDescriptionRes);
            #endregion

            #region Mock CreateAccountCurrencyMS

            var createAccountCurrencyRes = new CreateAccountCurrencyResponse()
            {
                AccountCurrency = CreateDefaultObject.Create<AccountCurrency>(),
                ResultType = ResultTypes.Ok,
            };

            _createAccountCurrencyMSMock.Process(Arg.Any<CreateAccountCurrencyRequest>(),
                Arg.Any<RequestInvokationEnvironment>()).Returns(createAccountCurrencyRes);

            #endregion

            var createCustomerAccountCurrencyRequestDTO = new CreateCustomerAccountCurrencyRequestDTO()
            {
                CustomerDto = CreateDefaultObject.Create<CustomerDTO>(),
                user = "1644000204",
                password = "123456",
                vmno = "970100"
            };

            createCustomerAccountCurrencyRequestDTO.CustomerDto.ChildCustomers = new List<int>();
            createCustomerAccountCurrencyRequestDTO.CustomerDto.CustomerData =
                CreateDefaultObject.Create<CustomerDataDTO>();

            MockAbstractSinglePhaseOrderProcessor(createCustomerAccountCurrencyRequestDTO);

            var createCustomerAccountCurrencyResponseDTO = CallBizOp(createCustomerAccountCurrencyRequestDTO);

            Assert.AreEqual(ResultTypes.BussinessLogicError, createCustomerAccountCurrencyResponseDTO.resultType);
        }
    }
}
