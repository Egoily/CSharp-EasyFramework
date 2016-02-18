using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.revenue.QueryInvoicesByCustomerId;
using com.etak.core.bizops.revenue.QueryInvoicesByMSISDN;
using com.etak.core.customer.message.GetLastNInvoicesByCustomerIdAndInvoiceStatuses;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.manager;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.subscription;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using log4net.Config;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.revenue.QueryInvoicesByMSISDN
{
    [TestFixture()]
    public class QueryInvoicesByMSISDNBizOpUnitTests : AbstractBusinessOperationTest
        <QueryInvoicesByMSISDNBizOp, QueryInvoicesByMSISDNRequestDTO, QueryInvoicesByMSISDNResponseDTO, QueryInvoicesByMSISDNRequestInternal, QueryInvoicesByMSISDNResponseInternal>
    {
        private const int TotalAmountChargeId = 111111;
        private const int nInvoices = 10;
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;

        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            XmlConfigurator.Configure();
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

        [Test]
        public void QueryInvoicesByMSISDNBizOp_ExistingMsisdn_ShouldReturnCorrectInvoices()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            decimal totalAmount = new decimal(12.34);

            var getLasNInvoicesMSReq = Arg.Any<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest>();
            var getLasNInvoicesMSRes = new GetLastNInvoicesByCustomerIdAndInvoiceStatusesResponse();

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
            getLasNInvoicesMSRes.Invoices = invoices;
            IList<KeyValuePair<Invoice, CustomerCharge>> invoicesKeyValue = new List<KeyValuePair<Invoice, CustomerCharge>>();
            foreach (var invoice in invoices)
            {
                KeyValuePair<Invoice, CustomerCharge> keyValue = new KeyValuePair<Invoice, CustomerCharge>
                        (
                            invoice,
                            invoice.Charges.FirstOrDefault(x => x.ChargeDefinition.Id == TotalAmountChargeId)
                        );
                invoicesKeyValue.Add(keyValue);
            }

            var getLastNInvoicesMS = MockMicroService<GetLastNInvoicesByCustomerIdAndInvoiceStatusesRequest, GetLastNInvoicesByCustomerIdAndInvoiceStatusesResponse>();

            getLastNInvoicesMS.Process(getLasNInvoicesMSReq, Arg.Any<RequestInvokationEnvironment>()).Returns(getLasNInvoicesMSRes);

            var queryInvoicesByMSISDNRequestDTO = new QueryInvoicesByMSISDNRequestDTO
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "6287896542"
            };

            var mockedQueryInvoicesByCustomerIdBizOp = Substitute.For<ICoreBusinessOperation<QueryInvoicesByCustomerIdRequestInternal, QueryInvoicesByCustomerIdResponseInternal>>();
            mockedQueryInvoicesByCustomerIdBizOp.Process(Arg.Any<QueryInvoicesByCustomerIdRequestInternal>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns(new QueryInvoicesByCustomerIdResponseInternal()
                {
                    ResultType = ResultTypes.Ok,
                    Invoices = invoicesKeyValue
                });
            BusinessOperationManager.RebindCoreInterfaceToConstant(1, mockedQueryInvoicesByCustomerIdBizOp);

            var ResourceMBInfoRepoMock = MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
            var resourceMBInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            resourceMBInfo.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            resourceMBInfo.CustomerInfo.CustomerID = 1;
            var resourceMBInfoList = new List<ResourceMBInfo>()
            {
                resourceMBInfo
            };
            ResourceMBInfoRepoMock.GetByMSISDNAndStatusNotInAndActiveDates(Arg.Is("6287896542"), Arg.Any<Int32[]>()).Returns(resourceMBInfoList);

            MockAbsctractBusinessOperation(queryInvoicesByMSISDNRequestDTO);
            MockConfiguration();

            var queryInvoicesRes = CallBizOp(queryInvoicesByMSISDNRequestDTO);
            foreach (var invoiceDTO in queryInvoicesRes.Invoices)
            {
                Assert.AreEqual(totalAmount, invoiceDTO.Amount);
            }
        }

        [Test()]
        public void QueryInvoicesByMSISDNBizOp_NullResourceMb_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            const int nInvoices = 10;

            var queryInvoicesByMsisdnRequestDTO = new QueryInvoicesByMSISDNRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                NumberOfInvoices = nInvoices,
                MSISDN = "123"
            };

            MockAbsctractBusinessOperation(queryInvoicesByMsisdnRequestDTO);

            #region Remock Repository

            var ResourceMBInfoRepoMock = MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
            var resourceMBInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            var resourceMBInfoList = new List<ResourceMBInfo>()
            {
                resourceMBInfo
            };
            ResourceMBInfoRepoMock.GetByMSISDNAndStatusNotInAndActiveDates(Arg.Is("6287896542"), Arg.Any<Int32[]>()).Returns(resourceMBInfoList);

            #endregion

            var actualQueryInvoicesByMsisdnResponseDto = CallBizOp(queryInvoicesByMsisdnRequestDTO);

            AssertExt.AreEqual(ResultTypes.BussinessLogicError, actualQueryInvoicesByMsisdnResponseDto.resultType);
        }

        [Test()]
        public void QueryInvoicesByMSISDNBizOp_NullCustomerInfo_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            const int nInvoices = 10;

            var queryInvoicesByMsisdnRequestDTO = new QueryInvoicesByMSISDNRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                NumberOfInvoices = nInvoices,
                MSISDN = "123789456"
            };

            MockAbsctractBusinessOperation(queryInvoicesByMsisdnRequestDTO);

            #region Remock Repository

            var ResourceMBInfoRepoMock = MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
            var resourceMBInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            resourceMBInfo.CustomerInfo = null;
            var resourceMBInfoList = new List<ResourceMBInfo>()
            {
                resourceMBInfo
            };
            ResourceMBInfoRepoMock.GetByMSISDNAndStatusNotInAndActiveDates(Arg.Is("123789456"), Arg.Any<Int32[]>()).Returns(resourceMBInfoList);

            #endregion

            var actualQueryInvoicesByMsisdnResponseDto = CallBizOp(queryInvoicesByMsisdnRequestDTO);

            AssertExt.AreEqual(ResultTypes.BussinessLogicError, actualQueryInvoicesByMsisdnResponseDto.resultType);
        }

        [Test()]
        public void QueryInvoicesByMSISDNBizOp_NullCustomerId_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            const int nInvoices = 10;

            var queryInvoicesByMsisdnRequestDTO = new QueryInvoicesByMSISDNRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                NumberOfInvoices = nInvoices,
                MSISDN = "987654321"
            };

            MockAbsctractBusinessOperation(queryInvoicesByMsisdnRequestDTO);

            #region Remock Repository

            var ResourceMBInfoRepoMock = MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
            var resourceMBInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            resourceMBInfo.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            resourceMBInfo.CustomerInfo.CustomerID = null;
            var resourceMBInfoList = new List<ResourceMBInfo>()
            {
                resourceMBInfo
            };
            ResourceMBInfoRepoMock.GetByMSISDNAndStatusNotInAndActiveDates(Arg.Is("987654321"), Arg.Any<Int32[]>()).Returns(resourceMBInfoList);

            #endregion

            var actualQueryInvoicesByMsisdnResponseDto = CallBizOp(queryInvoicesByMsisdnRequestDTO);

            AssertExt.AreEqual(ResultTypes.BussinessLogicError, actualQueryInvoicesByMsisdnResponseDto.resultType);
        }
        [Test()]
        public void QueryInvoicesByMSISDNBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var requestDto = new QueryInvoicesByMSISDNRequestDTO()
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
    }
}