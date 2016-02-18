using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.QueryCustomerByMsisdn;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.subscription;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.QueryCustomerByMsisdn
{
    [TestFixture]
    public class QueryCustomerByMsisdnBizOpUnitTests : AbstractBusinessOperationTest<QueryCustomerByMsisdnBizOp,QueryCustomerByMsisdnRequestDTO,QueryCustomerByMsisdnResponseDTO,QueryCustomerByMsisdnRequestInternal,QueryCustomerByMsisdnResponseInternal>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;

        public CustomerInfo StandardCustomerInfo()
        {
            var expectedCustomer = CreateDefaultObject.Create<CustomerInfo>();
            expectedCustomer.CustomerID = 9;
            expectedCustomer.ResourceMBInfo.Clear();
            expectedCustomer.ResourceMBInfo.Add(
                new ResourceMBInfo
            {
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
                OperatorInfo = new DealerInfo(),
                CustomerInfo = expectedCustomer,
                StatusID = (int)ResourceStatus.Active
            });

            expectedCustomer.ServicesInfo.Add(new ServicesInfo { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            expectedCustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 11 } }, Active = true, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            expectedCustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 22 } }, Active = true, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            expectedCustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 22 } }, Active = false, StartDate = DateTime.MinValue, EndDate = DateTime.MinValue });

            expectedCustomer.RevenueProductsInfo.Clear();
            expectedCustomer.RevenueProductsInfo.Add(new CustomerProductAssignment { PurchasedProduct = new Product { Id = 1, AssociatedPrmotionPlan = new RmPromotionPlanInfo { PromotionPlanId = 11, APIVisible = APIVisible.Visible }, Status = ProductStatuses.Current } });
            expectedCustomer.RevenueProductsInfo.Add(new CustomerProductAssignment { PurchasedProduct = new Product { Id = 2, AssociatedPrmotionPlan = new RmPromotionPlanInfo { PromotionPlanId = 22, APIVisible = APIVisible.Visible }, Status = ProductStatuses.Current } });
            expectedCustomer.RevenueProductsInfo.Add(new CustomerProductAssignment { PurchasedProduct = new Product { Id = 3, AssociatedPrmotionPlan = new RmPromotionPlanInfo { PromotionPlanId = 22, APIVisible = APIVisible.Visible }, Status = ProductStatuses.Test } });
            expectedCustomer.RevenueProductsInfo.Add(new CustomerProductAssignment { PurchasedProduct = new Product { Id = 4, AssociatedBundle = new BundleInfo(), Status = ProductStatuses.Current } });
            return expectedCustomer;
        }

        public void StandardMockCustomerInfo(CustomerInfo expectedCustomerInfo)
        {
            var mockedRepoResourceMB = MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
            var subscriptionNormal = expectedCustomerInfo.ResourceMBInfo.First(x=>x.StatusID == (int)ResourceStatus.Active);
            var subscriptionNullCustomer = CreateDefaultObject.Create<ResourceMBInfo>();
            subscriptionNullCustomer.CustomerInfo = null;
            mockedRepoResourceMB.GetByMSISDNAndStatusNotInAndActiveDates("0", Arg.Any<Int32[]>()).Returns((new List<ResourceMBInfo> { subscriptionNullCustomer}));
            mockedRepoResourceMB.GetByMSISDNAndStatusNotInAndActiveDates("1", Arg.Any<Int32[]>()).Returns(new List<ResourceMBInfo> { subscriptionNormal });
            mockedRepoResourceMB.GetByMSISDNAndStatusNotInAndActiveDates("2", Arg.Any<Int32[]>()).Returns(new List<ResourceMBInfo> { subscriptionNormal });
            mockedRepoResourceMB.GetByMSISDNAndStatusNotInAndActiveDates("3", Arg.Any<Int32[]>()).Returns(new List<ResourceMBInfo>{ subscriptionNormal });
            mockedRepoResourceMB.GetByMSISDNAndStatusNotInAndActiveDates("4", Arg.Any<Int32[]>()).Returns(new List<ResourceMBInfo> { subscriptionNormal });
            
        }

        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test]
        public void QueryCustomerByMsisdnBizOp_CorrectRequestGiven_ShouldReturnCorrectCustomer()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion        
            
            var request = new QueryCustomerByMsisdnRequestDTO
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1"
            };

            MockAbsctractBusinessOperation(request);
            var actualcustomer = StandardCustomerInfo();
            StandardMockCustomerInfo(actualcustomer);
            var expectedQueryCustomerByMsisdnResponseDTO = new QueryCustomerByMsisdnResponseDTO
            {
                Customer = new CustomerDTO { CustomerId = actualcustomer.CustomerID.Value}
            };

            var actualQueryCustomerByMsisdnResponseDTO = CallBizOp(request);

            AssertExt.IsTrue(actualQueryCustomerByMsisdnResponseDTO.Customer.CustomerId == expectedQueryCustomerByMsisdnResponseDTO.Customer.CustomerId);
        }

        [Test]
        public void QueryCustomerByMsisdnBizOp_ActualHasNoRevenueProductsInfo_BusinessLogicError()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var actualcustomer = StandardCustomerInfo();
            actualcustomer.RevenueProductsInfo.Clear();

            var request = new QueryCustomerByMsisdnRequestDTO
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "2"
            };

            MockAbsctractBusinessOperation(request);
            StandardMockCustomerInfo(actualcustomer);

            var QueryCustomerByMsisdnResponseDTO = CallBizOp(request);

            AssertExt.AreEqual(ResultTypes.BussinessLogicError, QueryCustomerByMsisdnResponseDTO.resultType);

        }

        [Test]
        public void QueryCustomerByMsisdnBizOp_SubscriptionHasNullCustomer_BusinessLogicError()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var actualSubscriptions = new List<ResourceMBInfo>
            {
                CreateDefaultObject.Create<ResourceMBInfo>()
            };
            actualSubscriptions.FirstOrDefault().CustomerInfo = null;
            
            var request = new QueryCustomerByMsisdnRequestDTO
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "0"
            };



            MockAbsctractBusinessOperation(request);
            MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>().GetByMSISDNAndStatusNotInAndActiveDates(request.MSISDN, new Int32[] { }).ReturnsForAnyArgs(actualSubscriptions);


            var QueryCustomerByMsisdnResponseDTO = CallBizOp(request);

            AssertExt.AreEqual(ResultTypes.BussinessLogicError, QueryCustomerByMsisdnResponseDTO.resultType);

        }

        [Test]
        public void QueryCustomerByMsisdnBizOp_CustomerHasNoServicesInfo_BusinessLogicError()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var actualcustomer = StandardCustomerInfo();
            actualcustomer.RevenueProductsInfo = new List<CustomerProductAssignment>();
            actualcustomer.ServicesInfo = null;


            var request = new QueryCustomerByMsisdnRequestDTO
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "3"
            };



            MockAbsctractBusinessOperation(request);
            StandardMockCustomerInfo(actualcustomer);
            var actualQueryCustomerByMsisdnResponseDTO = CallBizOp(request);

            AssertExt.AreEqual(ResultTypes.BussinessLogicError, actualQueryCustomerByMsisdnResponseDTO.resultType);

        }

        [Test]
        public void QueryCustomerByMsisdnBizOp_CustomerHasNoPromotions_BusinessLogicError()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var actualcustomer = StandardCustomerInfo();
            actualcustomer.ServicesInfo.Clear();
            actualcustomer.Promotions.Clear();
            actualcustomer.RevenueProductsInfo.Clear();


            var request = new QueryCustomerByMsisdnRequestDTO
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "4"
            };



            MockAbsctractBusinessOperation(request);
            StandardMockCustomerInfo(actualcustomer);

            var actualQueryCustomerByMsisdnResponseDTO = CallBizOp(request);

            AssertExt.AreEqual(ResultTypes.BussinessLogicError, actualQueryCustomerByMsisdnResponseDTO.resultType);

        }
    }
}
