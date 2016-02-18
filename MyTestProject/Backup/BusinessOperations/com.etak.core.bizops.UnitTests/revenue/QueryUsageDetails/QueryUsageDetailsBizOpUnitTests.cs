using System;
using System.Collections.Generic;
using com.etak.core.bizops.revenue.QueryUsageDetails;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.GSMSubscription.messages.GetUDRecordsByCustomerIDAndBetweenDates;
using com.etak.core.GSMSubscription.messages.GetUDRecordsByMSISDNAndBetweenDates;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.usage;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.dtoConverters;
using com.etak.core.repository.crm.customer;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.revenue.QueryUsageDetails
{
    [TestFixture()]
    public class QueryUsageDetailsBizOpUnitTests : AbstractBusinessOperationTest<QueryUsageDetailsBizOp, QueryUsageDetailsRequestDTO, QueryUsageDetailsResponseDTO, QueryUsageDetailsRequestInternal, QueryUsageDetailsResponseInternal>
    {
        private IMicroService <CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;

        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void QueryUsageDetailsBizOp_CorrectRequestGivenMSISDNIsEmpty_ShouldReturnCorrectUsageDetails()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var date = new DateTime(2015, 4, 24);

            //Mock GetUDRecordsByCustomerIDAndBetweenDatesMS
            var getUDRecordsByCustomerIDAndBetweenDatesMSMock =
                MockMicroService
                    <GetUDRecordsByCustomerIDAndBetweenDatesRequest, GetUDRecordsByCustomerIDAndBetweenDatesResponse>();
            var getUDRecordsByCustomerIDAndBetweenDatesRequest = Arg.Is<GetUDRecordsByCustomerIDAndBetweenDatesRequest>(x => x.CustomerId == 1 && x.EndDate == date && x.StartDate == date);
            var getUDRecordsByCustomerIDAndBetweenDatesResponse = new GetUDRecordsByCustomerIDAndBetweenDatesResponse()
            {
                UsageDetailRecords = new List<UsageDetailRecord> { CreateDefaultObject.Create<UsageDetailRecord>() }
            };

            getUDRecordsByCustomerIDAndBetweenDatesMSMock.Process(getUDRecordsByCustomerIDAndBetweenDatesRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(getUDRecordsByCustomerIDAndBetweenDatesResponse);

            var queryUsageDetailsRequestDTO = new QueryUsageDetailsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                FilterRule = 1,
                PeriodEndRange = date,
                PeriodStartRange = date,
                SubServiceTypeID = null
            };
            MockAbsctractBusinessOperation(queryUsageDetailsRequestDTO);

            var queryUsageDetailsResponseDTO = CallBizOp(queryUsageDetailsRequestDTO);

            var expectedQueryUsageDetailsResponseDTO = new QueryUsageDetailsResponseDTO()
            {
                UsageDetails = new List<UsageDetailDTO> { CreateDefaultObject.Create<UsageDetailRecord>().ToDto() }
            };

            AssertExt.ObjectPropertiesAreEqual(queryUsageDetailsResponseDTO.UsageDetails, expectedQueryUsageDetailsResponseDTO.UsageDetails);
        }

        [Test()]
        public void QueryUsageDetailsBizOp_MSISDNEmptyAndResultIsNull_ShouldReturnEmptyUsageDetails()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var date = new DateTime(2015, 4, 24);

            //Mock GetUDRecordsByCustomerIDAndBetweenDatesMS
            var getUDRecordsByCustomerIDAndBetweenDatesMSMock =
                MockMicroService
                    <GetUDRecordsByCustomerIDAndBetweenDatesRequest, GetUDRecordsByCustomerIDAndBetweenDatesResponse>();
            var getUDRecordsByCustomerIDAndBetweenDatesRequest = Arg.Is<GetUDRecordsByCustomerIDAndBetweenDatesRequest>(x => x.CustomerId == 1 && x.EndDate == date && x.StartDate == date);

            getUDRecordsByCustomerIDAndBetweenDatesMSMock.Process(getUDRecordsByCustomerIDAndBetweenDatesRequest, Arg.Any<RequestInvokationEnvironment>()).Returns((GetUDRecordsByCustomerIDAndBetweenDatesResponse)null);

            var queryUsageDetailsRequestDTO = new QueryUsageDetailsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                FilterRule = 1,
                PeriodEndRange = date,
                PeriodStartRange = date,
                SubServiceTypeID = null
            };
            MockAbsctractBusinessOperation(queryUsageDetailsRequestDTO);

            var queryUsageDetailsResponseDTO = CallBizOp(queryUsageDetailsRequestDTO);

            AssertExt.IsEmpty(queryUsageDetailsResponseDTO.UsageDetails);
        }

        [Test()]
        public void QueryUsageDetailsBizOp_MSISDNEmptyAndMSThrowException_ShouldThrowException()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var date = new DateTime(2015, 4, 24);

            //Mock GetUDRecordsByCustomerIDAndBetweenDatesMS
            var getUDRecordsByCustomerIDAndBetweenDatesMSMock =
                MockMicroService
                    <GetUDRecordsByCustomerIDAndBetweenDatesRequest, GetUDRecordsByCustomerIDAndBetweenDatesResponse>();
            var getUDRecordsByCustomerIDAndBetweenDatesRequest = Arg.Is<GetUDRecordsByCustomerIDAndBetweenDatesRequest>(x => x.CustomerId == 1 && x.EndDate == date && x.StartDate == date);

            getUDRecordsByCustomerIDAndBetweenDatesMSMock.Process(getUDRecordsByCustomerIDAndBetweenDatesRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            var queryUsageDetailsRequestDTO = new QueryUsageDetailsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                FilterRule = 1,
                PeriodEndRange = date,
                PeriodStartRange = date,
                SubServiceTypeID = null
            };
            MockAbsctractBusinessOperation(queryUsageDetailsRequestDTO);

            var queryUsageDetailsResponseDTO = CallBizOp(queryUsageDetailsRequestDTO);

            AssertExt.AreEqual(ResultTypes.UnknownError, queryUsageDetailsResponseDTO.resultType);
        }

        [Test()]
        public void QueryUsageDetailsBizOp_CorrectRequestGivenMSISDNIsNotEmpty_ShouldReturnCorrectUsageDetails()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var date = new DateTime(2015, 4, 24);

            //Mock GetUDRecordsByCustomerIDAndBetweenDatesMS
            var getUDRecordsByMSISDNAndBetweenDatesMSMock =
                MockMicroService
                    <GetUDRecordsByMSISDNAndBetweenDatesRequest, GetUDRecordsByMSISDNAndBetweenDatesResponse>();
            var getUDRecordsByMSISDNAndBetweenDatesRequest = Arg.Is<GetUDRecordsByMSISDNAndBetweenDatesRequest>(x => x.MSISDN == "085729292" && x.EndDate == date && x.StartDate == date);
            var getUDRecordsByMSISDNAndBetweenDatesResponse = new GetUDRecordsByMSISDNAndBetweenDatesResponse()
            {
                UsageDetailRecords = new List<UsageDetailRecord> { CreateDefaultObject.Create<UsageDetailRecord>() }
            };

            getUDRecordsByMSISDNAndBetweenDatesMSMock.Process(getUDRecordsByMSISDNAndBetweenDatesRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(getUDRecordsByMSISDNAndBetweenDatesResponse);

            var queryUsageDetailsRequestDTO = new QueryUsageDetailsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                MSISDN = "085729292",
                FilterRule = 1,
                PeriodEndRange = date,
                PeriodStartRange = date,
                SubServiceTypeID = null
            };
            MockAbsctractBusinessOperation(queryUsageDetailsRequestDTO);

            var actualqueryUsageDetailsResponseDTO = CallBizOp(queryUsageDetailsRequestDTO);

            var expectedQueryUsageDetailsResponseDTO = new QueryUsageDetailsResponseDTO()
            {
                UsageDetails = new List<UsageDetailDTO> { CreateDefaultObject.Create<UsageDetailRecord>().ToDto() }
            };

            AssertExt.ObjectPropertiesAreEqual(actualqueryUsageDetailsResponseDTO.UsageDetails, expectedQueryUsageDetailsResponseDTO.UsageDetails);
        }

        [Test()]
        public void QueryUsageDetailsBizOp_MSISDNIsNotEmptyAndResultIsNull_ShouldReturnEmptyUsageDetails()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var date = new DateTime(2015, 4, 24);

            //Mock GetUDRecordsByCustomerIDAndBetweenDatesMS
            var getUDRecordsByMSISDNAndBetweenDatesMSMock =
                MockMicroService
                    <GetUDRecordsByMSISDNAndBetweenDatesRequest, GetUDRecordsByMSISDNAndBetweenDatesResponse>();
            var getUDRecordsByMSISDNAndBetweenDatesRequest = Arg.Is<GetUDRecordsByMSISDNAndBetweenDatesRequest>(x => x.MSISDN == "085729292" && x.EndDate == date && x.StartDate == date);

            getUDRecordsByMSISDNAndBetweenDatesMSMock.Process(getUDRecordsByMSISDNAndBetweenDatesRequest, Arg.Any<RequestInvokationEnvironment>()).Returns((GetUDRecordsByMSISDNAndBetweenDatesResponse)null);

            var queryUsageDetailsRequestDTO = new QueryUsageDetailsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                MSISDN = "085729292",
                FilterRule = 1,
                PeriodEndRange = date,
                PeriodStartRange = date,
                SubServiceTypeID = null
            };
            MockAbsctractBusinessOperation(queryUsageDetailsRequestDTO);

            var queryUsageDetailsResponseDTO = CallBizOp(queryUsageDetailsRequestDTO);

            AssertExt.IsEmpty(queryUsageDetailsResponseDTO.UsageDetails);
        }

        [Test()]
        public void QueryUsageDetailsBizOp_MSISDNIsNotEmptyAndMSThrowException_ShouldThrowException()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var date = new DateTime(2015, 4, 24);

            //Mock GetUDRecordsByCustomerIDAndBetweenDatesMS
            var getUDRecordsByMSISDNAndBetweenDatesMSMock =
                MockMicroService
                    <GetUDRecordsByMSISDNAndBetweenDatesRequest, GetUDRecordsByMSISDNAndBetweenDatesResponse>();
            var getUDRecordsByMSISDNAndBetweenDatesRequest = Arg.Is<GetUDRecordsByMSISDNAndBetweenDatesRequest>(x => x.MSISDN == "085729292" && x.EndDate == date && x.StartDate == date);
            getUDRecordsByMSISDNAndBetweenDatesMSMock.Process(getUDRecordsByMSISDNAndBetweenDatesRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            //Create 6 TestCase : each mock 3 default

            var queryUsageDetailsRequestDTO = new QueryUsageDetailsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                MSISDN = "085729292",
                FilterRule = 1,
                PeriodEndRange = date,
                PeriodStartRange = date,
                SubServiceTypeID = null
            };
            MockAbsctractBusinessOperation(queryUsageDetailsRequestDTO);

            var queryUsageDetailsResponseDTO = CallBizOp(queryUsageDetailsRequestDTO);

            AssertExt.AreEqual(ResultTypes.UnknownError, queryUsageDetailsResponseDTO.resultType);
        }

        [Test()]
        public void QueryUsageDetailsBizOp_CustomerInfoNullMSISDNEmpty_ShouldThrowException()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var date = new DateTime(2015, 4, 24);
            //Remock Repository CustomerInfo
            var customerInfoRepoMock =
                MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            customerInfoRepoMock.GetById(Arg.Any<int>()).Returns((CustomerInfo)null);

            var queryUsageDetailsRequestDTO = new QueryUsageDetailsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                MSISDN = null,
                FilterRule = 1,
                PeriodEndRange = date,
                PeriodStartRange = date,
                SubServiceTypeID = null
            };
            MockAbsctractBusinessOperation(queryUsageDetailsRequestDTO);

            #region Remock Repository

            //Remock Repo PropertyInfo
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            CustomerInfo customerInfo = null;
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(customerInfo);

            #endregion

            var queryUsageDetailsResponseDTO = CallBizOp(queryUsageDetailsRequestDTO);

            AssertExt.AreEqual(ResultTypes.DataValidationError, queryUsageDetailsResponseDTO.resultType);
        }

        [Test()]
        public void QueryUsageDetailsBizOp_DealerIdIsNullMSISDNEmpty_ShouldThrowException()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var date = new DateTime(2015, 4, 24);
            //Remock Repository CustomerInfo
            var customerInfoRepoMock =
                MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            customerInfoRepoMock.GetById(Arg.Any<int>()).Returns((CustomerInfo)null);

            var queryUsageDetailsRequestDTO = new QueryUsageDetailsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                MSISDN = null,
                FilterRule = 1,
                PeriodEndRange = date,
                PeriodStartRange = date,
                SubServiceTypeID = null
            };
            MockAbsctractBusinessOperation(queryUsageDetailsRequestDTO);

            #region Remock Repository

            //Remock Repo PropertyInfo
            var mockedRepoCustomerInfo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            CustomerInfo customerInfo = CreateDefaultObject.Create<CustomerInfo>();
            customerInfo.DealerID = null;
            mockedRepoCustomerInfo.GetById(Arg.Any<int>()).Returns(customerInfo);

            #endregion

            var queryUsageDetailsResponseDTO = CallBizOp(queryUsageDetailsRequestDTO);

            AssertExt.AreEqual(ResultTypes.DataValidationError, queryUsageDetailsResponseDTO.resultType);
        }

        [Test()]
        public void QueryUsageDetailsBizOp_WrongDate_ShouldReturnException()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var date1 = new DateTime(2015, 4, 24);
            var date2 = new DateTime(2015, 5, 24);

            //Mock GetUDRecordsByCustomerIDAndBetweenDatesMS
            var getUDRecordsByCustomerIDAndBetweenDatesMSMock =
                MockMicroService
                    <GetUDRecordsByCustomerIDAndBetweenDatesRequest, GetUDRecordsByCustomerIDAndBetweenDatesResponse>();
            var getUDRecordsByCustomerIDAndBetweenDatesRequest = Arg.Is<GetUDRecordsByCustomerIDAndBetweenDatesRequest>(x => x.CustomerId == 1 && x.EndDate == date1 && x.StartDate == date2);
            var getUDRecordsByCustomerIDAndBetweenDatesResponse = new GetUDRecordsByCustomerIDAndBetweenDatesResponse()
            {
                UsageDetailRecords = new List<UsageDetailRecord> { CreateDefaultObject.Create<UsageDetailRecord>() }
            };

            getUDRecordsByCustomerIDAndBetweenDatesMSMock.Process(getUDRecordsByCustomerIDAndBetweenDatesRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(getUDRecordsByCustomerIDAndBetweenDatesResponse);

            var queryUsageDetailsRequestDTO = new QueryUsageDetailsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1,
                FilterRule = 1,
                PeriodEndRange = date1,
                PeriodStartRange = date2,
                SubServiceTypeID = null
            };
            MockAbsctractBusinessOperation(queryUsageDetailsRequestDTO);

            var queryUsageDetailsResponseDTO = CallBizOp(queryUsageDetailsRequestDTO);

            var expectedQueryUsageDetailsResponseDTO = new QueryUsageDetailsResponseDTO()
            {
                UsageDetails = new List<UsageDetailDTO> { CreateDefaultObject.Create<UsageDetailRecord>().ToDto() }
            };

            AssertExt.AreEqual(ResultTypes.DataValidationError, queryUsageDetailsResponseDTO.resultType);
        }

        [Test()]
        public void QueryUsageDetailsBizOp_NOK_authorizationfailed()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var requestDto = new QueryUsageDetailsRequestDTO()
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