using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.revenue.QueryCustomerUnbilledCharges;
using com.etak.core.bizops.revenue.QueryInvoicesByCustomerId;
using com.etak.core.customer.message.GetLastNInvoicesByCustomerIdAndInvoiceStatuses;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.customer;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.revenue.QueryInvoicesByCustomerId
{
    [TestFixture]
    public class QueryInvoicesByCustomerIdBizOpUnitTests :
        AbstractBusinessOperationTest<QueryInvoicesByCustomerIdBizOp, QueryInvoicesByCustomerIdRequestDTO,
        QueryInvoicesByCustomerIdResponseDTO, QueryInvoicesByCustomerIdRequestInternal, QueryInvoicesByCustomerIdResponseInternal>
    {
        private const int TotalAmountChargeId = 111111;
        private const int nInvoices = 10;
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;

        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            log4net.Config.XmlConfigurator.Configure();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        public static void MockConfiguration()
        {
            var mockedRepoConfig = MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();

            var queryConfiguration = new QueryInvoicesByCustomerIdConfiguration
            {
                AggregateTotalInvoiceChargeId = TotalAmountChargeId,
                NumberOfInvoices = nInvoices
            };

            var bizOpConfig = new OperationConfiguration
            {
                JSonConfig = JsonConvert.SerializeObject(queryConfiguration),
                StarTime = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1)
            };
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>()).Returns(new[] { bizOpConfig });
        }

        [Test()]
        public void QueryInvoicesByCustomerIdBizOp_ExistingCustomerId_ShouldReturnCorrectNumber()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            decimal totalAmount = new decimal(12.34);

            var getLastNInvoicesMSReq = Arg.Any<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest>();
            var getLastNInvoicesMSRes = new GetLastNInvoicesByCustomerIdAndInvoiceStatusesResponse();

            IList<Invoice> invoices = new List<Invoice>();
            for (int i = 0; i < 10; i++)
            {
                CustomerCharge totalAmountCharge = CreateDefaultObject.Create<CustomerCharge>();
                Invoice invoice = CreateDefaultObject.Create<Invoice>();

                totalAmountCharge.ChargeDefinition = CreateDefaultObject.Create<ChargeNonRecurring>();
                totalAmountCharge.ChargeDefinition.Id = TotalAmountChargeId;
                totalAmountCharge.InformationalAmount = totalAmount;

                invoice.Charges = new List<CustomerCharge> { totalAmountCharge };
                invoices.Add(invoice);
            }
            getLastNInvoicesMSRes.Invoices = invoices;

            var getLastNInvoicesMS = MockMicroService<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest, GetLastNInvoicesByCustomerIdAndInvoiceStatusesResponse>();

            getLastNInvoicesMS.Process(getLastNInvoicesMSReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getLastNInvoicesMSRes);

            var queryInvoicesByCustomerIdRequestDTO = new QueryInvoicesByCustomerIdRequestDTO
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
            };

            MockAbsctractBusinessOperation(queryInvoicesByCustomerIdRequestDTO);
            MockConfiguration();

            //Remock customerinfo

            var mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = queryInvoicesByCustomerIdRequestDTO.CustomerId;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = queryInvoicesByCustomerIdRequestDTO.CustomerId;
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);

            var expectedQueryInvoicesByCustomerIdResponseDTO = new QueryInvoicesByCustomerIdResponseDTO()
            {
                Subscription = new SubscriptionDTO { CustomerId = queryInvoicesByCustomerIdRequestDTO.CustomerId }
            };


            var actualQueryInvoicesByCustomerIdResponseDTO = CallBizOp(queryInvoicesByCustomerIdRequestDTO);
            foreach (var invoiceDTO in actualQueryInvoicesByCustomerIdResponseDTO.Invoices)
            {
                Assert.AreEqual(totalAmount, invoiceDTO.Amount);
            }

            Assert.IsTrue(expectedQueryInvoicesByCustomerIdResponseDTO.Subscription.CustomerId == actualQueryInvoicesByCustomerIdResponseDTO.Subscription.CustomerId);
        }

        [Test()]
        public void QueryInvoicesByCustomerIdBizOp_NullInvoice_ShouldReturnNullInvoice()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            const int nInvoices = 10;

            var getLastNInvoicesMSReq = Arg.Any<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest>();
            var getLastNInvoicesMSRes = new GetLastNInvoicesByCustomerIdAndInvoiceStatusesResponse();

            IList<Invoice> invoices = null;
            getLastNInvoicesMSRes.Invoices = invoices;

            var getLastNInvoicesMS = MockMicroService<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest, GetLastNInvoicesByCustomerIdAndInvoiceStatusesResponse>();

            getLastNInvoicesMS.Process(getLastNInvoicesMSReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getLastNInvoicesMSRes);

            var queryInvoicesByCustomerIdRequestDTO = new QueryInvoicesByCustomerIdRequestDTO
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                NumberOfInvoices = nInvoices,
                CustomerId = 1,
            };

            MockAbsctractBusinessOperation(queryInvoicesByCustomerIdRequestDTO);
            MockConfiguration();

            //Remock customerinfo

            var mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = queryInvoicesByCustomerIdRequestDTO.CustomerId;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = queryInvoicesByCustomerIdRequestDTO.CustomerId;
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);

            var expectedQueryInvoicesByCustomerIdResponseDTO = new QueryInvoicesByCustomerIdResponseDTO()
            {
                Subscription = new SubscriptionDTO { CustomerId = queryInvoicesByCustomerIdRequestDTO.CustomerId }
            };
            var actualQueryInvoicesByCustomerIdResponseDTO = CallBizOp(queryInvoicesByCustomerIdRequestDTO);
            IEnumerable result = actualQueryInvoicesByCustomerIdResponseDTO.Invoices;
            Assert.IsEmpty(result);
            Assert.IsTrue(expectedQueryInvoicesByCustomerIdResponseDTO.Subscription.CustomerId == actualQueryInvoicesByCustomerIdResponseDTO.Subscription.CustomerId);
        }
        [Test()]
        public void QueryInvoicesByCustomerIdBizOp_MSThrowException_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            const int nInvoices = 10;

            //Mock GetLastNInvoicesByCustomerIdAndInvoiceStatusesMS
            var getLastNInvoicesByCustomerIdAndInvoiceStatusesRequest =
                Arg.Is<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest>(x => x.CustomerId == 1);
            var getLastNInvoicesByCustomerIdAndInvoiceStatusesMSMock = MockMicroService<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest, GetLastNInvoicesByCustomerIdAndInvoiceStatusesResponse>();

            getLastNInvoicesByCustomerIdAndInvoiceStatusesMSMock.Process(
                getLastNInvoicesByCustomerIdAndInvoiceStatusesRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            var queryInvoicesByCustomerIdRequestDTO = new QueryInvoicesByCustomerIdRequestDTO
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                NumberOfInvoices = nInvoices,
                CustomerId = 1,
            };

            MockAbsctractBusinessOperation(queryInvoicesByCustomerIdRequestDTO);
            MockConfiguration();

            var actualqueryInvoicesByCustomerIdResponseDTO = CallBizOp(queryInvoicesByCustomerIdRequestDTO);
            Assert.AreEqual(ResultTypes.UnknownError, actualqueryInvoicesByCustomerIdResponseDTO.resultType);
        }

        [Test()]
        public void QueryInvoicesByCustomerIdBizOp_NullCustomerInfo_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            const int nInvoices = 10;

            var queryInvoicesByCustomerIdRequestDTO = new QueryInvoicesByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                NumberOfInvoices = nInvoices,
                CustomerId = 1
            };

            MockAbsctractBusinessOperation(queryInvoicesByCustomerIdRequestDTO);

            #region Remock Repository
            var mockedRepoCustomerInfo =
                MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo = null;

            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(customerInfo);
            #endregion

            var actualQueryInvoicesByCustomerIdResponseDto = CallBizOp(queryInvoicesByCustomerIdRequestDTO);

            AssertExt.AreEqual(ResultTypes.BussinessLogicError, actualQueryInvoicesByCustomerIdResponseDto.resultType);
        }

        [Test()]
        public void QueryInvoicesByCustomerIdBizOp_NullCustomerId_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            const int nInvoices = 10;

            var queryInvoicesByCustomerIdRequestDTO = new QueryInvoicesByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                NumberOfInvoices = nInvoices,
                CustomerId = 1
            };

            MockAbsctractBusinessOperation(queryInvoicesByCustomerIdRequestDTO);

            #region Remock Repository
            var mockedRepoCustomerInfo =
                MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.CustomerID = null;

            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(customerInfo);
            #endregion

            var actualQueryInvoicesByCustomerIdResponseDto = CallBizOp(queryInvoicesByCustomerIdRequestDTO);

            AssertExt.AreEqual(ResultTypes.BussinessLogicError, actualQueryInvoicesByCustomerIdResponseDto.resultType);
        }
        [Test()]
        public void QueryInvoicesByCustomerIdBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var requestDto = new QueryInvoicesByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1000000000
            };

            MockAbsctractBusinessOperation(requestDto);

            var res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.AuthorizationError);
            Assert.AreEqual(res.errorCode, BizOpsErrors.AuthorizeErrorUser);
        }
    }
    
}
