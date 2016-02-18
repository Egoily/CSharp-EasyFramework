using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.revenue.ModifyCustomerCreditLimit;
using com.etak.core.bizops.revenue.QueryCustomerRecurringCharges;
using com.etak.core.customer.message.GetCustomerChargesBetweenDatesAndCustomerId;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.dealer.messages.GetDealerInfoById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository.crm.customer;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.revenue.QueryCustomerRecurringCharges
{
    [TestFixture()]
    public class QueryCustomerRecurringChargesBizOpUnitTests : AbstractBusinessOperationTest<QueryCustomerRecurringChargesBizOp, QueryCustomerRecurringChargesRequestDTO, QueryCustomerRecurringChargesResponseDTO, QueryCustomerRecurringChargesRequestInternal, QueryCustomerRecurringChargesResponseInternal>
    {
        private DateTime date = new DateTime(2015, 4, 24);
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void QueryCustomerRecurringChargesBizOp_CorrectRequestGiven_ShouldReturnCorrectCustomerChargeInfo()
        {

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            
            var getDealerInfoByIdMSMock = MockMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerInfoByIdRequest = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == 1);
            var getDealerInfoByIdResponse = new GetDealerInfoByIdResponse()
            {
                DealerInfo = CreateDefaultObject.Create<DealerInfo>()
            };

            getDealerInfoByIdMSMock.Process(getDealerInfoByIdRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(getDealerInfoByIdResponse);

            var getCustomerChargesBetweenDatesAndCustomerIdMSMock =
                MockMicroService
                    <GetCustomerChargesBetweenDatesAndCustomerIdRequest,
                        GetCustomerChargesBetweenDatesAndCustomerIdResponse>();
            var getCustomerChargesBetweenDatesAndCustomerIdRequest = Arg.Is<GetCustomerChargesBetweenDatesAndCustomerIdRequest>(
                    x => x.CustomerId == 1000 && x.StartDate == date && x.EndDate == date);
            var CustomerChargesTemp = CreateDefaultObject.Create<CustomerCharge>();
            CustomerChargesTemp.Schedule = CreateDefaultObject.Create<CustomerChargeSchedule>();
            var getCustomerChargesBetweenDatesAndCustomerIdResponse = new GetCustomerChargesBetweenDatesAndCustomerIdResponse()
            {
                CustomerCharges = new List<CustomerCharge> { CustomerChargesTemp } 
            };
            getCustomerChargesBetweenDatesAndCustomerIdMSMock.Process(
                getCustomerChargesBetweenDatesAndCustomerIdRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(getCustomerChargesBetweenDatesAndCustomerIdResponse);
            
            var queryCustomerRecurringChargesRequestDTO = new QueryCustomerRecurringChargesRequestDTO()
            {
                user = "00",
                password = "123456",
                vmno = "970100",
                CustomerId = 1000,
                StartDate = date,
                EndDate = date,
            };

            MockAbsctractBusinessOperation(queryCustomerRecurringChargesRequestDTO);

            //Remock customerinfo

            var mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = queryCustomerRecurringChargesRequestDTO.CustomerId;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = queryCustomerRecurringChargesRequestDTO.CustomerId;
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);

            var expectedQueryCustomerRecurringChargesResponseDTO = new QueryCustomerRecurringChargesResponseDTO()
            {
                Subscription = new SubscriptionDTO { CustomerId = queryCustomerRecurringChargesRequestDTO.CustomerId },
                RecurringCharges = new List<CustomerRecurringChargeDTO> { CreateDefaultObject.Create<CustomerRecurringChargeDTO>() }
            };

            var actualQueryCustomerRecurringChargesResponseDTO = CallBizOp(queryCustomerRecurringChargesRequestDTO);

            var actualValue = actualQueryCustomerRecurringChargesResponseDTO.RecurringCharges.First();

            var expectedCheck = expectedQueryCustomerRecurringChargesResponseDTO.RecurringCharges.First();
            expectedCheck.Amount = actualValue.Amount;
            expectedCheck.ChargeId = actualValue.ChargeId;
            expectedCheck.CreateTime = actualValue.CreateTime;
            expectedCheck.Currency = actualValue.Currency;
            expectedCheck.CustomerId = actualValue.CustomerId;
            expectedCheck.Id = actualValue.Id;
            expectedCheck.InvoiceId = actualValue.InvoiceId;
            expectedCheck.ProductPurchaseId = actualValue.ProductPurchaseId;
            expectedCheck.ReferenceCode = actualValue.ReferenceCode;

            AssertExt.ObjectPropertiesAreEqual(actualValue, expectedCheck);
            Assert.IsTrue(expectedQueryCustomerRecurringChargesResponseDTO.resultType == ResultTypes.Ok);
            Assert.IsTrue(expectedQueryCustomerRecurringChargesResponseDTO.Subscription.CustomerId == actualQueryCustomerRecurringChargesResponseDTO.Subscription.CustomerId);
        }

        [Test()]
        public void QueryCustomerRecurringChargesBizOp_WrongDate_ShouldReturnDataValidationError()
        {
            var date1 = new DateTime(2015, 4, 24);
            var date2 = new DateTime(2015, 5, 24); 

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var requestDto = new QueryCustomerRecurringChargesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1000000000,
                StartDate = date2,
                EndDate = date1,
            };

            MockAbsctractBusinessOperation(requestDto);

            var res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.DataValidationError);
        }

        [Test()]
        public void QueryCustomerRecurringChargesBizOp_MSThrowException_ShouldReturnErrorCustomerChargeInfo()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var getDealerInfoByIdMSMock = MockMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerInfoByIdRequest = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == 2000);
            
            getDealerInfoByIdMSMock.Process(getDealerInfoByIdRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(x => { throw new Exception("Error"); });

            var getCustomerChargesBetweenDatesAndCustomerIdMSMock =
                MockMicroService
                    <GetCustomerChargesBetweenDatesAndCustomerIdRequest,
                        GetCustomerChargesBetweenDatesAndCustomerIdResponse>();
            var getCustomerChargesBetweenDatesAndCustomerIdRequest = Arg.Is<GetCustomerChargesBetweenDatesAndCustomerIdRequest>(
                    x => x.CustomerId == 2000 && x.StartDate == date && x.EndDate == date);
            var CustomerChargesTemp = CreateDefaultObject.Create<CustomerCharge>();
            CustomerChargesTemp.Schedule = CreateDefaultObject.Create<CustomerChargeSchedule>();
            
            getCustomerChargesBetweenDatesAndCustomerIdMSMock.Process(
                getCustomerChargesBetweenDatesAndCustomerIdRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            
            var queryCustomerRecurringChargesRequestDTO = new QueryCustomerRecurringChargesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 2000,
                StartDate = date,
                EndDate = date,
            };

            MockAbsctractBusinessOperation(queryCustomerRecurringChargesRequestDTO);
            var queryCustomerRecurringChargesResponseDTO = CallBizOp(queryCustomerRecurringChargesRequestDTO);
            Assert.IsTrue(queryCustomerRecurringChargesResponseDTO.resultType == ResultTypes.UnknownError);
        }

        [Test()]
        public void QueryCustomerRecurringChargesBizOp_NoCustomerCharge_ShouldReturnNullCustomerChargeInfo()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var getDealerInfoByIdMSMock = MockMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerInfoByIdRequest = new GetDealerInfoByIdRequest();

            getDealerInfoByIdMSMock.Process(getDealerInfoByIdRequest, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(new GetDealerInfoByIdResponse {DealerInfo = CreateDefaultObject.Create<DealerInfo>() });

            var getCustomerChargesBetweenDatesAndCustomerIdMSMock =
                MockMicroService
                    <GetCustomerChargesBetweenDatesAndCustomerIdRequest,
                        GetCustomerChargesBetweenDatesAndCustomerIdResponse>();
            var getCustomerChargesBetweenDatesAndCustomerIdRequest = Arg.Is<GetCustomerChargesBetweenDatesAndCustomerIdRequest>(
                    x => x.CustomerId == 3000 && x.StartDate == date && x.EndDate == date);
            var CustomerChargesTemp = CreateDefaultObject.Create<CustomerCharge>();
            CustomerChargesTemp.Schedule = CreateDefaultObject.Create<CustomerChargeSchedule>();
            
            getCustomerChargesBetweenDatesAndCustomerIdMSMock.Process(
                getCustomerChargesBetweenDatesAndCustomerIdRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(new GetCustomerChargesBetweenDatesAndCustomerIdResponse()
                {
                    CustomerCharges = new List<CustomerCharge>()
                });

            var queryCustomerRecurringChargesRequestDTO = new QueryCustomerRecurringChargesRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 3000,
                StartDate = date,
                EndDate = date,
            };
            MockAbsctractBusinessOperation(queryCustomerRecurringChargesRequestDTO);

            //Remock customerinfo

            var mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = queryCustomerRecurringChargesRequestDTO.CustomerId;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = queryCustomerRecurringChargesRequestDTO.CustomerId;
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);

            var expectedQueryCustomerRecurringChargesResponseDTO = new QueryCustomerRecurringChargesResponseDTO()
            {
                Subscription = new SubscriptionDTO { CustomerId = queryCustomerRecurringChargesRequestDTO.CustomerId },
                RecurringCharges = new List<CustomerRecurringChargeDTO>()
            };

            var response = CallBizOp(queryCustomerRecurringChargesRequestDTO);
            Assert.IsEmpty(response.RecurringCharges.ToList());
            Assert.IsTrue(expectedQueryCustomerRecurringChargesResponseDTO.Subscription.CustomerId == response.Subscription.CustomerId);
        }

        [Test()]
        public void QueryCustomerRecurringChargesBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var requestDto = new QueryCustomerRecurringChargesRequestDTO()
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
