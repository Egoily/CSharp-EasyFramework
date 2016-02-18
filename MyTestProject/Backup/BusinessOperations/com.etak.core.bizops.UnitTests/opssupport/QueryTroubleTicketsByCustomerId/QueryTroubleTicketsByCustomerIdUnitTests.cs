using com.etak.core.bizops.fullfilment.QueryTroubleTicketsByCustomerId;
using com.etak.core.bizops.opssupport.QueryTroubleTicketsByCustomerId;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository.crm.customer;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.opssupport.QueryTroubleTicketsByCustomerId
{
    [TestFixture()]
    public class QueryTroubleTicketsByCustomerIdUnitTests : AbstractBusinessOperationTest<QueryTroubleTicketsByCustomerIdBizOp, QueryTroubleTicketsByCustomerIdRequestDTO, QueryTroubleTicketsByCustomerIdResponseDTO, QueryTroubleTicketsByCustomerIdRequestInternal, QueryTroubleTicketsByCustomerIdResponseInternal>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;

        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void QueryTroubleTicketsByCustomerId_NoAuthorization_ReturnAuthorizationError()
        {

            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var requestDto = new QueryTroubleTicketsByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 123
            };

            MockAbsctractBusinessOperation(requestDto);

            var res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.AuthorizationError);
            Assert.AreEqual(res.errorCode, BizOpsErrors.AuthorizeErrorUser);

        }

        [Test()]
        public void QueryTroubleTicketsByCustomerId_CustomerExist_OK()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var requestDTO = new QueryTroubleTicketsByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 12
            };

            MockAbsctractBusinessOperation(requestDTO);

            var responseDTO = CallBizOp(requestDTO);
            AssertExt.ObjectPropertiesAreEqual(responseDTO.resultType, ResultTypes.Ok);


        }
        [Test()]
        public void QueryTroubleTicketsByCustomerId_CustomerNotExist_ReturnBusinessLogicError()
        {
            // Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            var requestDTO = new QueryTroubleTicketsByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 12
            };

            MockAbsctractBusinessOperation(requestDTO);

            #region Remock Repository

            var repoCustomerInfoMock = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            CustomerInfo customerInfo = null;
            repoCustomerInfoMock.GetById(Arg.Any<int>()).Returns(customerInfo);

            #endregion

            var responseDTO = CallBizOp(requestDTO);
            AssertExt.ObjectPropertiesAreEqual(responseDTO.resultType, ResultTypes.BussinessLogicError);
        }
    }
}
