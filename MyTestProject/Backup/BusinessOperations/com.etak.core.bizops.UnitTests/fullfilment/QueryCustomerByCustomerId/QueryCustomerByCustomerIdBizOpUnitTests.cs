using System.Linq;
using com.etak.core.bizops.fullfilment.QueryCustomerByCustomerId;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
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

namespace com.etak.core.bizops.UnitTests.fullfilment.QueryCustomerByCustomerId
{
    [TestFixture()]
    public class QueryCustomerByCustomerIdBizOpUnitTests :
        AbstractBusinessOperationTest
            <QueryCustomerByCustomerIdBizOp, QueryCustomerByCustomerIdRequestDTO, QueryCustomerByCustomerIdResponseDTO,
                QueryCustomerByCustomerIdRequestInternal, QueryCustomerByCustomerIdResponseInternal>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;
        private ICustomerInfoRepository<CustomerInfo> mockCustomerRepo;
        private int expectedcustomerid = 9;
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            
        }

        [Test()]
        public void QueryCustomerByCustomerIdBizOp_CorrectRequestGiven_ShouldReturnCorrectCustomer()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var expectedCustomer = CreateDefaultObject.Create<CustomerInfo>();
            expectedCustomer.CustomerID = expectedcustomerid;
            var expectedResponse = new QueryCustomerByCustomerIdResponseDTO()
            {
                Customer = expectedCustomer.ToDto(),
                Subscription = new SubscriptionDTO { CustomerId = expectedcustomerid }
            };

            var queryCustomerByCustomerIdBizOpRequestDTO = new QueryCustomerByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = expectedcustomerid
            };

            MockAbsctractBusinessOperation(queryCustomerByCustomerIdBizOpRequestDTO);

            #region Remock Customer 
            var mockedCustomer = CreateDefaultObject.Create<CustomerInfo>();
            mockedCustomer.CustomerID = expectedcustomerid;
            mockedCustomer.ResourceMBInfo.FirstOrDefault(x=>x.StatusID == (int)ResourceStatus.Active).CustomerInfo = mockedCustomer;
            mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            mockCustomerRepo.GetById(Arg.Any<int>()).ReturnsForAnyArgs(mockedCustomer);
            #endregion

            var queryCustomerByCustomerIdBizOpResponseDTO = CallBizOp(queryCustomerByCustomerIdBizOpRequestDTO);
            AssertExt.ObjectPropertiesAreEqual(expectedResponse.Customer,
                queryCustomerByCustomerIdBizOpResponseDTO.Customer);
            Assert.IsTrue(expectedResponse.Subscription.CustomerId ==
                queryCustomerByCustomerIdBizOpResponseDTO.Subscription.CustomerId);
        }

        [Test()]
        public void QueryCustomerByCustomerIdBizOp_CorrectRequest_ShouldReturnNullCustomer()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            var queryCustomerByCustomerIdBizOpRequestDTO = new QueryCustomerByCustomerIdRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                CustomerId = 1
            };

            MockAbsctractBusinessOperation(queryCustomerByCustomerIdBizOpRequestDTO);

            #region Remock Repository

            var repoCustomerInfoMock = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            CustomerInfo customerInfo = null;
            repoCustomerInfoMock.GetById(Arg.Any<int>()).Returns(customerInfo);

            #endregion

            var queryCustomerByCustomerIdBizOpResponseDTO = CallBizOp(queryCustomerByCustomerIdBizOpRequestDTO);
            AssertExt.IsNull(queryCustomerByCustomerIdBizOpResponseDTO.Customer);
        }

        [Test()]
        public void QueryCustomerByCustomerIdBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var requestDto = new QueryCustomerByCustomerIdRequestDTO()
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