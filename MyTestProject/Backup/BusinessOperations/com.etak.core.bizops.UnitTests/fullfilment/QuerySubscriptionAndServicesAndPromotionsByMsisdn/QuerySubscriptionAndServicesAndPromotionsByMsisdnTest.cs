using System;
using System.Linq;
using com.etak.core.bizops.fullfilment.QuerySubscriptionAndServicesAndPromotionsByMsisdn;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository.crm.subscription;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.QuerySubscriptionAndServicesAndPromotionsByMsisdn
{
    [TestFixture()]
    public class QuerySubscriptionAndServicesAndPromotionsByMsisdnBizOpUnitTest :
        AbstractBusinessOperationTest<QuerySubscriptionAndServicesAndPromotionsByMsisdnBizOp,
            QuerySubscriptionAndServicesAndPromotionsByMsisdnDTORequest,
            QuerySubscriptionAndServicesAndPromotionsByMsisdnDTOResponse,
            QuerySubscriptionAndServicesAndPromotionsByMsisdnRequestInternal,
            QuerySubscriptionAndServicesAndPromotionsByMsisdnResponseInternal>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;      

        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();           
        }

        public QuerySubscriptionAndServicesAndPromotionsByMsisdnDTOResponse TestOperation(CustomerInfo actualcust)
        {

            actualcust.RevenueProductsInfo.Clear();
            actualcust.RevenueProductsInfo.Add(new CustomerProductAssignment { PurchasedProduct = new Product { Id = 1, AssociatedPrmotionPlan = new RmPromotionPlanInfo { PromotionPlanId = 11, APIVisible = APIVisible.Visible }, Status = ProductStatuses.Current } });
            actualcust.RevenueProductsInfo.Add(new CustomerProductAssignment { PurchasedProduct = new Product { Id = 2, AssociatedPrmotionPlan = new RmPromotionPlanInfo { PromotionPlanId = 22, APIVisible = APIVisible.Visible }, Status = ProductStatuses.Current } });
            actualcust.RevenueProductsInfo.Add(new CustomerProductAssignment { PurchasedProduct = new Product { Id = 3, AssociatedPrmotionPlan = new RmPromotionPlanInfo { PromotionPlanId = 22, APIVisible = APIVisible.Visible }, Status = ProductStatuses.Test } });
            actualcust.RevenueProductsInfo.Add(new CustomerProductAssignment { PurchasedProduct = new Product { Id = 4, AssociatedBundle = new BundleInfo(), Status = ProductStatuses.Current } });
            var request = new QuerySubscriptionAndServicesAndPromotionsByMsisdnDTORequest()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "999",
            };

            MockAbsctractBusinessOperation(request);
            MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>().GetByMSISDNAndStatusNotInAndActiveDates(request.MSISDN, new Int32[] { }).ReturnsForAnyArgs(actualcust.ResourceMBInfo);
            var response = CallBizOp(request);
            return response;
        }

        [TestCase]
        public void QuerySubscriptionAndServicesAndPromotionsByMsisdn_OK()
        {
            #region Mock CheckAuthorization

            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            
            #endregion

            var actualcustomer = CreateDefaultObject.Create<CustomerInfo>();
            actualcustomer.ResourceMBInfo.Clear();
            actualcustomer.ResourceMBInfo.Add(new ResourceMBInfo { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue, OperatorInfo = new DealerInfo() });


            actualcustomer.ResourceMBInfo.First().CustomerInfo = actualcustomer;
            actualcustomer.ResourceMBInfo.First().StatusID = (int)ResourceStatus.Active;
            actualcustomer.ServicesInfo.Add(new ServicesInfo { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            actualcustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 11 } }, Active = true, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            actualcustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 22 } }, Active = true, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            actualcustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 22 } }, Active = false, StartDate = DateTime.MinValue, EndDate = DateTime.MinValue });

            var expectedresponse = TestOperation(actualcustomer);
            Assert.IsTrue(expectedresponse.resultType == ResultTypes.Ok);
            Assert.IsNotEmpty(expectedresponse.CustomerPromotions);
            Assert.IsTrue(expectedresponse.Subscription.CustomerId == actualcustomer.CustomerID);
            Assert.IsNotEmpty(expectedresponse.CustomerServices);
            Assert.IsTrue(expectedresponse.CustomerPromotions.First(x => x.ProductId == 1).PromotionPlanId == 11);
            Assert.IsTrue(expectedresponse.CustomerPromotions.First(x => x.ProductId == 2).PromotionPlanId == 22);
            Assert.IsTrue(expectedresponse.CustomerPromotions.First(x => x.ProductId == 1).Active);
            Assert.IsTrue(expectedresponse.CustomerPromotions.First(x => x.ProductId == 2).Active);
            Assert.IsTrue(expectedresponse.Customer.CustomerId == actualcustomer.CustomerID);
        }

        [TestCase]
        public void QuerySubscriptionAndServicesAndPromotionsByMsisdn_NoSubscription()
        {
            #region Mock CheckAuthorization

            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            #endregion

            var actualcustomer = CreateDefaultObject.Create<CustomerInfo>();
            actualcustomer.ResourceMBInfo.Clear();
            actualcustomer.ServicesInfo.Add(new ServicesInfo { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            actualcustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 11 } }, Active = true, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            actualcustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 22 } }, Active = true, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            actualcustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 22 } }, Active = false, StartDate = DateTime.MinValue, EndDate = DateTime.MinValue });
            Assert.IsTrue(TestOperation(actualcustomer).resultType == ResultTypes.BussinessLogicError);
            Assert.AreEqual("This MSISDN does not have any subscription", TestOperation(actualcustomer).messages.First());
        }

        [TestCase]
        public void QuerySubscriptionAndServicesAndPromotionsByMsisdn_NoPromotions()
        {
            #region Mock CheckAuthorization

            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            #endregion

            var actualcustomer = CreateDefaultObject.Create<CustomerInfo>();
            actualcustomer.ResourceMBInfo.Clear();
            actualcustomer.ResourceMBInfo.Add(new ResourceMBInfo { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue, OperatorInfo = new DealerInfo() });
            actualcustomer.ResourceMBInfo.First().CustomerInfo = actualcustomer;
            actualcustomer.ResourceMBInfo.First().StatusID = (int)ResourceStatus.Active;
            actualcustomer.ServicesInfo.Add(new ServicesInfo { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            actualcustomer.Promotions.Clear();
            Assert.IsTrue(TestOperation(actualcustomer).resultType == ResultTypes.BussinessLogicError);
            Assert.AreEqual("Customer does not have any promotions assigned", TestOperation(actualcustomer).messages.First());
        }

        [TestCase]
        public void QuerySubscriptionAndServicesAndPromotionsByMsisdn_NoServices()
        {
            #region Mock CheckAuthorization

            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            #endregion

            var actualcustomer = CreateDefaultObject.Create<CustomerInfo>();
            actualcustomer.ResourceMBInfo.Clear();
            actualcustomer.ResourceMBInfo.Add(new ResourceMBInfo { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue, OperatorInfo = new DealerInfo() });

            actualcustomer.ResourceMBInfo.First().CustomerInfo = actualcustomer;
            actualcustomer.ResourceMBInfo.First().StatusID = (int)ResourceStatus.Active;
            actualcustomer.ServicesInfo.Clear();
            actualcustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 11 } }, Active = true, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            actualcustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 22 } }, Active = true, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue });
            actualcustomer.Promotions.Add(new CrmCustomersPromotionInfo { PromotionDetail = new RmPromotionPlanDetailInfo { RmPromotionPlanInfo = new RmPromotionPlanInfo { PromotionPlanId = 22 } }, Active = false, StartDate = DateTime.MinValue, EndDate = DateTime.MinValue });
            Assert.IsTrue(TestOperation(actualcustomer).resultType == ResultTypes.BussinessLogicError);
            Assert.AreEqual("Customer does not have any services assigned", TestOperation(actualcustomer).messages.First());
        }

        [Test()]
        public void QuerySubscriptionAndServicesAndPromotionsByMsisdnBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization
            
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            
            #endregion

            var requestDto = new QuerySubscriptionAndServicesAndPromotionsByMsisdnDTORequest()
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