using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.revenue.QueryCustomerRecurringCharges;
using com.etak.core.bizops.revenue.QueryCustomerUnbilledCharges;
using com.etak.core.customer.message.GetCustomerChargeByCustomerIdAndInvoice;
using com.etak.core.customer.message.GetInvoicesByCustomerIdAndLegalInvoiceNumber;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.dtoConverters;
using com.etak.core.repository.crm.customer;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.revenue.QueryCustomerUnbilledCharges
{
    [TestFixture()]
    public class QueryCustomerUnbilledChargesBizOpUnitTests : AbstractBusinessOperationTest<QueryCustomerUnbilledChargesBizOp, QueryCustomerUnbilledChargesRequestDTO, QueryCustomerUnbilledChargesResponseDTO, QueryCustomerUnbilledChargesRequestInternal, QueryCustomerUnbilledChargesResponseInternal>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void QueryCustomerUnbilledChargesBizOp_CorrectRequestGiven_ShouldReturnCorrectCustomerCharge()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //Mock GetInvoicesByCustomerIdAndLegalInvoiceNumberMS
            var getInvoicesByCustomerIdAndLegalInvoiceNumberRequest =
                Arg.Is<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(x => x.CustomerId == 1);
            var getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock = MockMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();
            var getInvoicesByCustomerIdAndLegalInvoiceNumberResponse = new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                ()
            {
                Invoices = new List<Invoice>() { CreateDefaultObject.Create<Invoice>() }
            };
            getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock.Process(
                getInvoicesByCustomerIdAndLegalInvoiceNumberRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(getInvoicesByCustomerIdAndLegalInvoiceNumberResponse);

            //Mock GetCustomerChargeByCustomerIdAndInvoiceMS

            var getCustomerChargeByCustomerIdAndInvoiceRequest =
                Arg.Is<GetCustomerChargeByCustomerIdAndInvoiceRequest>(
                    x => x.CustomerId == 1 && x.Invoice.Equals(getInvoicesByCustomerIdAndLegalInvoiceNumberResponse.Invoices.First()));
            var getCustomerChargeByCustomerIdAndInvoiceMSMock =
                MockMicroService
                    <GetCustomerChargeByCustomerIdAndInvoiceRequest, GetCustomerChargeByCustomerIdAndInvoiceResponse>();
            //Create CustomerCharge with specific properties
            var tempCustomerCharge = CreateDefaultObject.Create<CustomerCharge>();
            tempCustomerCharge.Product = CreateDefaultObject.Create<CustomerProductAssignment>();
            tempCustomerCharge.Product.PurchasedProduct = CreateDefaultObject.Create<Product>();
            tempCustomerCharge.Product.PurchasedProduct.AssociatedPrmotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            tempCustomerCharge.Product.PurchasedProduct.AssociatedPrmotionPlan.APIVisible = APIVisible.VisibleOnAdd;
            tempCustomerCharge.ChargeDefinition = CreateDefaultObject.Create<ChargeAggregate>();
            tempCustomerCharge.ChargeDefinition.InformationalOnly = InformationalTypes.N;

            var getCustomerChargeByCustomerIdAndInvoiceResponse = new GetCustomerChargeByCustomerIdAndInvoiceResponse()
            {
                CustomerCharges = new List<CustomerCharge>
                {
                    tempCustomerCharge
                }
            };

            getCustomerChargeByCustomerIdAndInvoiceMSMock.Process(getCustomerChargeByCustomerIdAndInvoiceRequest,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getCustomerChargeByCustomerIdAndInvoiceResponse);

            var queryCustomerUnbilledChargesRequestDTO = new QueryCustomerUnbilledChargesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1
            };

            MockAbsctractBusinessOperation(queryCustomerUnbilledChargesRequestDTO);

            //Remock customerinfo

            var mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = queryCustomerUnbilledChargesRequestDTO.CustomerId;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = queryCustomerUnbilledChargesRequestDTO.CustomerId;
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);

            var expectedQueryCustomerUnbilledChargesResponseDTO = new QueryCustomerUnbilledChargesResponseDTO()
            {
                CustomerCharges = new List<CustomerChargeDTO>()
                {
                    tempCustomerCharge.ToDto()
                },
                Subscription = new SubscriptionDTO { CustomerId = queryCustomerUnbilledChargesRequestDTO.CustomerId }
            };

            var actualQueryCustomerUnbilledChargesResponseDTO = CallBizOp(queryCustomerUnbilledChargesRequestDTO);
            

            AssertExt.ObjectPropertiesAreEqual(expectedQueryCustomerUnbilledChargesResponseDTO.CustomerCharges, actualQueryCustomerUnbilledChargesResponseDTO.CustomerCharges);
            Assert.IsTrue(actualQueryCustomerUnbilledChargesResponseDTO.Subscription.CustomerId == actualQueryCustomerUnbilledChargesResponseDTO.Subscription.CustomerId);
        }

        [Test()]
        public void QueryCustomerUnbilledChargesBizOp_CustomerIsNull_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //Remock Repository CustomerInfo
            var customerInfoRepoMock =
                MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            customerInfoRepoMock.GetById(Arg.Any<int>()).Returns((CustomerInfo)null);

            var queryCustomerUnbilledChargesRequestDTO = new QueryCustomerUnbilledChargesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1
            };

            MockAbsctractBusinessOperation(queryCustomerUnbilledChargesRequestDTO);

            #region Remock Repository

            //Remock Repo PropertyInfo
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            CustomerInfo customerInfo = null;
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(customerInfo);

            #endregion

            var actualQueryCustomerUnbilledChargesResponseDTO = CallBizOp(queryCustomerUnbilledChargesRequestDTO);

            AssertExt.AreEqual(ResultTypes.DataValidationError, actualQueryCustomerUnbilledChargesResponseDTO.resultType);
        }

        [Test()]
        public void QueryCustomerUnbilledChargesBizOp_CustomerWithEmptyCharge_ShouldReturnEmptyCharge()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //Mock GetInvoicesByCustomerIdAndLegalInvoiceNumberMS
            var getInvoicesByCustomerIdAndLegalInvoiceNumberRequest =
                Arg.Is<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(x => x.CustomerId == 1);
            var getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock = MockMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();
            var getInvoicesByCustomerIdAndLegalInvoiceNumberResponse = new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                ()
            {
                Invoices = new List<Invoice>() { CreateDefaultObject.Create<Invoice>() }
            };
            getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock.Process(
                getInvoicesByCustomerIdAndLegalInvoiceNumberRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(getInvoicesByCustomerIdAndLegalInvoiceNumberResponse);

            //Mock GetCustomerChargeByCustomerIdAndInvoiceMS

            var getCustomerChargeByCustomerIdAndInvoiceRequest =
                Arg.Is<GetCustomerChargeByCustomerIdAndInvoiceRequest>(
                    x => x.CustomerId == 1 && x.Invoice.Equals(getInvoicesByCustomerIdAndLegalInvoiceNumberResponse.Invoices.First()));
            var getCustomerChargeByCustomerIdAndInvoiceMSMock =
                MockMicroService
                    <GetCustomerChargeByCustomerIdAndInvoiceRequest, GetCustomerChargeByCustomerIdAndInvoiceResponse>();
            //Create CustomerCharge with specific properties
            var tempCustomerCharge = CreateDefaultObject.Create<CustomerCharge>();

            var getCustomerChargeByCustomerIdAndInvoiceResponse = new GetCustomerChargeByCustomerIdAndInvoiceResponse()
            {
                CustomerCharges = new List<CustomerCharge>
                {
                    tempCustomerCharge
                }
            };

            getCustomerChargeByCustomerIdAndInvoiceMSMock.Process(getCustomerChargeByCustomerIdAndInvoiceRequest,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getCustomerChargeByCustomerIdAndInvoiceResponse);

            var queryCustomerUnbilledChargesRequestDTO = new QueryCustomerUnbilledChargesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1
            };

            MockAbsctractBusinessOperation(queryCustomerUnbilledChargesRequestDTO);

            //Remock customerinfo

            var mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = queryCustomerUnbilledChargesRequestDTO.CustomerId;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = queryCustomerUnbilledChargesRequestDTO.CustomerId;
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);

            var expectedQueryCustomerUnbilledChargesResponseDTO = new QueryCustomerUnbilledChargesResponseDTO()
            {
                CustomerCharges = new List<CustomerChargeDTO>()
                {
                    tempCustomerCharge.ToDto()
                },
                Subscription = new SubscriptionDTO { CustomerId = queryCustomerUnbilledChargesRequestDTO.CustomerId }
            };

            var actualQueryCustomerUnbilledChargesResponseDTO = CallBizOp(queryCustomerUnbilledChargesRequestDTO);

            AssertExt.ObjectPropertiesAreEqual(expectedQueryCustomerUnbilledChargesResponseDTO.CustomerCharges, actualQueryCustomerUnbilledChargesResponseDTO.CustomerCharges);
            AssertExt.IsTrue(expectedQueryCustomerUnbilledChargesResponseDTO.Subscription.CustomerId == actualQueryCustomerUnbilledChargesResponseDTO.Subscription.CustomerId);
        }

        [Test()]
        public void QueryCustomerUnbilledChargesBizOp_CustomerHasNoInvoices_ShouldThrowDataValidationException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //Mock GetInvoicesByCustomerIdAndLegalInvoiceNumberMS
            var getInvoicesByCustomerIdAndLegalInvoiceNumberRequest =
                Arg.Is<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(x => x.CustomerId == 1);
            var getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock = MockMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();

            getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock.Process(
                getInvoicesByCustomerIdAndLegalInvoiceNumberRequest, Arg.Any<RequestInvokationEnvironment>()).Returns((GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse)null);

            var queryCustomerUnbilledChargesRequestDTO = new QueryCustomerUnbilledChargesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1
            };

            MockAbsctractBusinessOperation(queryCustomerUnbilledChargesRequestDTO);

            var actualQueryCustomerUnbilledChargesResponseDTO = CallBizOp(queryCustomerUnbilledChargesRequestDTO);
            Assert.AreEqual(ResultTypes.DataValidationError, actualQueryCustomerUnbilledChargesResponseDTO.resultType);
        }

        [Test()]
        public void QueryCustomerUnbilledChargesBizOp_MS1ThrowException_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //Mock GetInvoicesByCustomerIdAndLegalInvoiceNumberMS
            var getInvoicesByCustomerIdAndLegalInvoiceNumberRequest =
                Arg.Is<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(x => x.CustomerId == 1);
            var getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock = MockMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();

            getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock.Process(
                getInvoicesByCustomerIdAndLegalInvoiceNumberRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            var queryCustomerUnbilledChargesRequestDTO = new QueryCustomerUnbilledChargesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1
            };

            MockAbsctractBusinessOperation(queryCustomerUnbilledChargesRequestDTO);

            var actualQueryCustomerUnbilledChargesResponseDTO = CallBizOp(queryCustomerUnbilledChargesRequestDTO);
            Assert.AreEqual(ResultTypes.UnknownError, actualQueryCustomerUnbilledChargesResponseDTO.resultType);
        }

        [Test()]
        public void QueryCustomerUnbilledChargesBizOp_MS2ThrowException_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //Mock GetInvoicesByCustomerIdAndLegalInvoiceNumberMS
            var getInvoicesByCustomerIdAndLegalInvoiceNumberRequest =
                Arg.Is<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest>(x => x.CustomerId == 1);
            var getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock = MockMicroService<GetInvoicesByCustomerIdAndLegalInvoiceNumberRequest, GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse>();
            var getInvoicesByCustomerIdAndLegalInvoiceNumberResponse = new GetInvoicesByCustomerIdAndLegalInvoiceNumberResponse
                ()
            {
                Invoices = new List<Invoice>() { CreateDefaultObject.Create<Invoice>() }
            };
            getInvoicesByCustomerIdAndLegalInvoiceNumberMSMock.Process(
                getInvoicesByCustomerIdAndLegalInvoiceNumberRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(getInvoicesByCustomerIdAndLegalInvoiceNumberResponse);

            //Mock GetCustomerChargeByCustomerIdAndInvoiceMS

            var getCustomerChargeByCustomerIdAndInvoiceRequest =
                Arg.Is<GetCustomerChargeByCustomerIdAndInvoiceRequest>(
                    x => x.CustomerId == 1 && x.Invoice.Equals(getInvoicesByCustomerIdAndLegalInvoiceNumberResponse.Invoices.First()));
            var getCustomerChargeByCustomerIdAndInvoiceMSMock =
                MockMicroService
                    <GetCustomerChargeByCustomerIdAndInvoiceRequest, GetCustomerChargeByCustomerIdAndInvoiceResponse>();

            getCustomerChargeByCustomerIdAndInvoiceMSMock.Process(getCustomerChargeByCustomerIdAndInvoiceRequest,
                Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception(); });

            var queryCustomerUnbilledChargesRequestDTO = new QueryCustomerUnbilledChargesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1
            };

            MockAbsctractBusinessOperation(queryCustomerUnbilledChargesRequestDTO);

            var actualQueryCustomerUnbilledChargesResponseDTO = CallBizOp(queryCustomerUnbilledChargesRequestDTO);

            AssertExt.ObjectPropertiesAreEqual(ResultTypes.UnknownError, actualQueryCustomerUnbilledChargesResponseDTO.resultType);
        }
        [Test()]
        public void QueryCustomerUnbilledChargesBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var requestDto = new QueryCustomerUnbilledChargesRequestDTO()
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