using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.etak.core.bizops.fullfilment.ApplyCustomerPromotion;
using com.etak.core.bizops.revenue.QueryInvoicesByCustomerId;
using com.etak.core.dealer.messages.GetDealerInfoMVNOByDealerId;
using com.etak.core.microservices.messages.CalculateNextPeriod;
using com.etak.core.microservices.microservices;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetRmPromotionPlanInfosByIds;
using com.etak.core.promotion.messages.CreateCustomersPromotion;
using com.etak.core.repository;
using com.etak.core.repository.crm.customer;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.ApplyCustomerPromotion
{
    [TestFixture]
    public class ApplyCustomerPromotionTest : AbstractSinglePhaseOrderProcessorTest<ApplyCustomerPromotionBizOp, ApplyCustomerPromotionRequestDTO, ApplyCustomerPromotionResponseDTO, ApplyCustomerPromotionRequestInternal, ApplyCustomerPromotionResponseInternal, ApplyCustomerPromotionOrder>
    {
        private const int PromotionPlanId = 100;
        private static GetDealerInfoMVNOByDealerIdResponse _getDealerResp;
        private static List<RmPromotionPlanInfo> _promotionPlans;

        [TestFixtureSetUp]
        public void InitializeTest()
        {
            FakeSessionFactorySingleton.Init();

            #region CreateCustomersPromotion
            var mockCreatePromotionMs = MockMicroServiceManager.GetMockedMicroService<CreateCustomersPromotionRequest, CreateCustomersPromotionResponse>();
            var createPromotionReq = Arg.Is<CreateCustomersPromotionRequest>(x => x.CrmCustomersPromotionInfo.CustomerId == 1);
            var createPromotionResp = new CreateCustomersPromotionResponse()
            {
                promotionPlan = CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                ResultType = ResultTypes.Ok
            };
            mockCreatePromotionMs.Process(createPromotionReq, Arg.Any<RequestInvokationEnvironment>()).Returns(createPromotionResp);
            #endregion

            #region GetDealerInfoMVNOByDealerId
            var mockGetDealerInfoByMsnoMs = MockMicroServiceManager.GetMockedMicroService<GetDealerInfoMVNOByDealerIdRequest, GetDealerInfoMVNOByDealerIdResponse>();
            var getDealerReq = Arg.Is<GetDealerInfoMVNOByDealerIdRequest>(x => x.DealerId == 1);
            _getDealerResp = new GetDealerInfoMVNOByDealerIdResponse()
            {
                DealerInfo = CreateDefaultObject.Create<DealerInfo>(),
            };
            mockGetDealerInfoByMsnoMs.Process(getDealerReq, Arg.Any<RequestInvokationEnvironment>());
            #endregion

            #region GetRmPromotionPlanInfosByIdsMs
            var mockGetRmPromPlanByIdsMs = MockMicroService<GetRmPromotionPlanInfosByIdsRequest, GetRmPromotionPlanInfosByIdsResponse>();
            var getRmPromPlanReq = Arg.Is<GetRmPromotionPlanInfosByIdsRequest>(x => x.PromotionPlanIds.FirstOrDefault() == PromotionPlanId);
            #region Create the PromotionPlan
            var promotionPlan = CreateDefaultObject.Create<RmPromotionPlanInfo>();
            promotionPlan.StartDate = DateTime.Now.AddMonths(-12);
            promotionPlan.EndDate = null;
            var planDetail0 = promotionPlan.RmPromotionPlanDetailInfoList[0];
            planDetail0.Periodicity = 1;
            planDetail0.PeriodUnit = TimeUnits.Month;
            planDetail0.PeriodCount = 1;
            planDetail0.EndPeriodNumber = 1;
            planDetail0.StartPeriodNumber = 1;

            var planDetail1 = promotionPlan.RmPromotionPlanDetailInfoList[1];
            planDetail1.Periodicity = 0;
            planDetail1.PeriodUnit = TimeUnits.Month;
            planDetail1.PeriodCount = 1;
            planDetail1.EndPeriodNumber = 0;
            planDetail1.StartPeriodNumber = 0;

            #endregion

            _promotionPlans = new List<RmPromotionPlanInfo>() { promotionPlan };
            var getRmPromPlanResp = new GetRmPromotionPlanInfosByIdsResponse()
            {
                ResultType = ResultTypes.Ok,
                RmPromotionPlanInfos = _promotionPlans,
            };
            mockGetRmPromPlanByIdsMs.Process(getRmPromPlanReq, null).Returns(getRmPromPlanResp);
            #endregion

            #region MockCalculateNextPeriod
            var mockCalculateNextPeriodMs = MockMicroServiceManager.GetMockedMicroService<CalculateNextPeriodRequest, CalculateNextPeriodResponse>();
            var calculateNextPeriodResponse = CreateDefaultObject.Create<CalculateNextPeriodResponse>();
            calculateNextPeriodResponse.NextDate = new DateTime(2015, 7, 31, 23, 59, 59).AddSeconds(1);
            mockCalculateNextPeriodMs.Process(new CalculateNextPeriodRequest(), Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(calculateNextPeriodResponse);
            #endregion
        }

        [Test]
        public void ApplyCustomerPromotionOk()
        {
            var actualApplyCustomerPromotionRequestDTO = new ApplyCustomerPromotionRequestDTO()
            {
                CustomerId = 1,
                PromotionPlanIds = new List<int>() { PromotionPlanId },
                user = "1644000204",
                password = "123456",
                vmno = "970100",
            };

            MockAbstractSinglePhaseOrderProcessor(actualApplyCustomerPromotionRequestDTO);

            //Remock customerinfo

            var mockCustomerRepo = MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>();
            var mockedActualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            mockedActualCustomerInfo.PropertyInfo = new List<PropertyInfo>()
            {
                CreateDefaultObject.Create<PropertyInfo>()
            };
            mockedActualCustomerInfo.PropertyInfo.First().PendingStatus = (int)PendingStatus.Active;
            mockedActualCustomerInfo.CustomerID = actualApplyCustomerPromotionRequestDTO.CustomerId;
            mockedActualCustomerInfo.ResourceMBInfo.Clear();
            mockedActualCustomerInfo.ResourceMBInfo.Add(CreateDefaultObject.Create<ResourceMBInfo>());
            mockedActualCustomerInfo.ResourceMBInfo.First().CustomerInfo.CustomerID = actualApplyCustomerPromotionRequestDTO.CustomerId;
            mockCustomerRepo.GetById(mockedActualCustomerInfo.CustomerID.Value).Returns(mockedActualCustomerInfo);

            var expectedApplyCustomerPromotionResponseDTO = new ApplyCustomerPromotionResponseDTO
            {
                Subscription = new SubscriptionDTO { CustomerId = actualApplyCustomerPromotionRequestDTO.CustomerId }
            };

            var actualApplyCustomerPromotionResponseDTO = CallBizOp(actualApplyCustomerPromotionRequestDTO);

            Assert.IsTrue(actualApplyCustomerPromotionResponseDTO.resultType == ResultTypes.Ok);
            Assert.IsTrue(expectedApplyCustomerPromotionResponseDTO.Subscription.CustomerId == actualApplyCustomerPromotionResponseDTO.Subscription.CustomerId);
        }


    
        [Test, TestCaseSource("ExpectedDates")]
        public void ApplyCustomerPromotionEndDateOk(DateTime expectedStartDate, DateTime expectedEndDate)
        {
            var request = new ApplyCustomerPromotionRequestInternal()
            {
                MVNO = _getDealerResp.DealerInfo,
                Customer = CreateDefaultObject.Create<CustomerInfo>(),
                PromotionPlans = _promotionPlans,
                StartDate = expectedStartDate
            };

            var applyCustPrombizOp = new ApplyCustomerPromotionBizOp();
            var response = applyCustPrombizOp.ProcessRequest(null, request);

            foreach (var crmCustomersPromotionInfo in response.CrmCustomersPromotionInfos) 
            {
                if (crmCustomersPromotionInfo.PromotionDetail.Periodicity==0)
                    Assert.IsTrue(crmCustomersPromotionInfo.EndDate.Equals(
                        _promotionPlans[0].RmPromotionPlanDetailInfoList.First(rd => rd.PromotionPlanDetailId == crmCustomersPromotionInfo.PromotionDetail.PromotionPlanDetailId).EndDate
                        ));
                else
                    Assert.IsTrue(crmCustomersPromotionInfo.EndDate.Equals(expectedEndDate));
            }
        }
        private static IEnumerable<object[]> ExpectedDates()
        {
            yield return
                new object[]
                {
                    new DateTime(2015, 7, 27, 13, 30, 00),
                    new DateTime(2015, 7, 31, 23, 59, 59)
                };
        }

        [Test, TestCaseSource("ExpectedRenewDates")]
        public void ApplyCustomerPromotionNextRenewDateOk(DateTime expectedStartDate, DateTime expectedNextRenewDate)
        {
            var request = new ApplyCustomerPromotionRequestInternal()
            {
                MVNO = _getDealerResp.DealerInfo,
                Customer = CreateDefaultObject.Create<CustomerInfo>(),
                PromotionPlans = _promotionPlans,
                StartDate = expectedStartDate
            };

            var applyCustPrombizOp = new ApplyCustomerPromotionBizOp();
            var response = applyCustPrombizOp.ProcessRequest(null, request);

            foreach (var crmCustomersPromotionInfo in response.CrmCustomersPromotionInfos)
            {
                Assert.IsTrue(crmCustomersPromotionInfo.NextRenewDate.Equals(expectedNextRenewDate) || 
                    crmCustomersPromotionInfo.NextRenewDate.Equals(null));
            }
        }
        private static IEnumerable<object[]> ExpectedRenewDates()
        {
            yield return
                new object[]
                {
                    new DateTime(2015, 7, 27, 13, 30, 00),
                    new DateTime(2015, 8, 1, 00, 00, 00)
                };
        }
    }
}
