using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.bizops.revenue.QueryBalance;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository.crm.subscription;
using com.etak.core.service.messages.CustomerHasCredit;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.revenue.QueryBalance
{
    [TestFixture]
    public class QueryBalanceBizOpUnitTests : AbstractBusinessOperationTest<QueryBalanceBizOp, QueryBalanceRequestDto, QueryBalanceResponseDto, QueryBalanceRequestInternal, QueryBalanceResponseInternal>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;
        private IMicroService<CustomerHasCreditRequest, CustomerHasCreditResponse> mockCustomerHasCredit;

        private const int customerIdOK = 100;
        private const int customerIdNok = 200;
        private const String msisdnOk = "6666666";
        private const String msisdnNok = "5555555";

        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            mockCustomerHasCredit = MockMicroService<CustomerHasCreditRequest, CustomerHasCreditResponse>();

        }

        private void SetupMocks()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            #region Mock CustomerHasCreditMs

            #region Ok Response
            var masterBundle = CreateDefaultObject.Create<ServicesInfo>();
            masterBundle.BundleDefinition.BundleID = 1000;
            masterBundle.CREDITLIMITBASEBUNDLEID = 1000;

            var customerHasCreditRespOk = new CustomerHasCreditResponse()
            {
                HasCredit = false,
                ResultType = ResultTypes.Ok,
                MasterBundle = masterBundle,
            }; 
            #endregion

            #region NOk Response

            var customerHasCreditRespNok = new CustomerHasCreditResponse()
            {
                ErrorCode = 100,
                ResultType = ResultTypes.BussinessLogicError,
                
            };

            #endregion

            #region MasterBundle Null

            var customerHasCreditRespMasterBundleNull = new CustomerHasCreditResponse()
            {
                ResultType = ResultTypes.Ok,
                MasterBundle = null,
            };
            #endregion

            var customerHasCreditReq = Arg.Is<CustomerHasCreditRequest>(x => x.CustomerInfo.CustomerID.Value == customerIdOK);
            mockCustomerHasCredit.Process(customerHasCreditReq, null).Returns(customerHasCreditRespOk);

            var customerHasCreditReqNok = Arg.Is<CustomerHasCreditRequest>(x => x.CustomerInfo.CustomerID.Value == customerIdNok);
            mockCustomerHasCredit.Process(customerHasCreditReqNok, null).Returns(customerHasCreditRespNok);

            #endregion

            #region Mock GetLastSubscriptionRepo

            #region Subscription Ok
            var expectedSubscription = CreateDefaultObject.Create<ResourceMBInfo>();
            expectedSubscription.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            expectedSubscription.CustomerInfo.CustomerID = customerIdOK;
            expectedSubscription.StatusID = (int)ResourceStatus.Active; 
            #endregion

            #region Subscription NOk
            var expectedSubscriptionNok = CreateDefaultObject.Create<ResourceMBInfo>();
            expectedSubscriptionNok.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            expectedSubscriptionNok.CustomerInfo.CustomerID = customerIdNok;
            expectedSubscriptionNok.StatusID = (int)ResourceStatus.Active;
            #endregion


            var getSubsRepo = MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
            getSubsRepo.GetByMSISDNAndStatusNotInAndActiveDates(Arg.Is<String>(x => x == msisdnOk), Arg.Any<IEnumerable<int>>()).Returns(new List<ResourceMBInfo>() { expectedSubscription });
            getSubsRepo.GetByMSISDNAndStatusNotInAndActiveDates(Arg.Is<String>(x => x == msisdnNok), Arg.Any<IEnumerable<int>>()).Returns(new List<ResourceMBInfo>() { expectedSubscriptionNok });
            #endregion
        }

        [TestCase(msisdnOk)]
        public void QueryBalance_CorrectRequestGiven_ShouldReturnOk(string msisdn)
        {
            var queryBalanceRequest = new QueryBalanceRequestDto()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = msisdn
            };
            
            MockAbsctractBusinessOperation(queryBalanceRequest);
            SetupMocks();
            var response = CallBizOp(queryBalanceRequest);

            Assert.AreEqual(response.resultType, ResultTypes.Ok);
        }

        [TestCase(msisdnNok)]
        public void QueryBalance_NoMasterBundle_ReturnError(string msisdn)
        {
            var queryBalanceRequest = new QueryBalanceRequestDto()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = msisdn
            };

            MockAbsctractBusinessOperation(queryBalanceRequest);
            SetupMocks();
            var response = CallBizOp(queryBalanceRequest);

            Assert.AreEqual(response.resultType, ResultTypes.BussinessLogicError);
            Assert.AreEqual(response.errorCode, BizOpsErrors.MasterBundleNotFound);
        }
    }
}
