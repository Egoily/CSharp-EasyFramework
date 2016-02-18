using System;
using com.etak.core.bizops.fullfilment.QuerySubscriptionByCustomerId;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.crm.customer;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.QuerySubscriptionByCustomerId
{
    [TestFixture()]
    public class QuerySubscriptionByCustomerIdBizOpUnitTests :
        AbstractBusinessOperationTest<QuerySubscriptionByCustomerIdBizOp,
                                       QuerySubscriptionByCustomerIdRequestDTO, QuerySubscriptionByCustomerIdResponseDTO,
                                       QuerySubscriptionByCustomerIdRequestInternal, QuerySubscriptionByCustomerIdResponseInternal>
    {
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [TearDown]
        public void ClearEverything()
        {
            MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>().ClearReceivedCalls();
        }

        [TestCase]
        public void QuerySubscriptionByCustomerIdBizOp_ExistingCustomerWithSubcriptions_ShouldReturnCorrectSubscription()
        {
            Int32 customerId = 1;
            var request = new QuerySubscriptionByCustomerIdRequestDTO
            {
                user = "123456", //ignored by test fw
                password = "123456789", //ignored by test fw
                vmno = "123456", //ignored by test fw
                CustomerId = customerId, //Mocked by the repository
            };

            MockAbsctractBusinessOperation(request);
            MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>()
                .GetById(customerId)
                .Returns(
                    new CustomerInfo
                    {
                        ResourceMBInfo = new[]
                        {
                            new ResourceMBInfo() {StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1)}
                        },
                    }
                );

            var resp = CallBizOp(request);

            Assert.AreNotEqual(resp.Subscription, null);
            Assert.AreEqual(resp.resultType, ResultTypes.Ok);
            Assert.AreEqual(resp.errorCode, 0);
        }

        [TestCase]
        public void QuerySubscriptionByCustomerIdBizOp_ExistingCustomerWithMultipleActiveSubcriptions_ShouldReturnCorrectSubscription()
        {
            Int32 customerId = 2;
            var request = new QuerySubscriptionByCustomerIdRequestDTO
            {
                user = "123456", //ignored by test fw
                password = "123456789", //ignored by test fw
                vmno = "123456", //ignored by test fw
                CustomerId = customerId, //Mocked by the repository
            };

            MockAbsctractBusinessOperation(request);
            MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>()
                .GetById(customerId).Returns(
               new CustomerInfo
               {
                   ResourceMBInfo = new[]
                    {
                        new ResourceMBInfo() { StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1)},
                         new ResourceMBInfo() { StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1)}
                    },
               }
               );

            var resp = CallBizOp(request);

            Assert.AreEqual(resp.Subscription, null);
            Assert.AreEqual(resp.resultType, ResultTypes.BussinessLogicError);
            Assert.AreEqual(resp.errorCode, BizOpsErrors.CustomerMultipleSubscriptions);
        }

        [TestCase]
        public void QuerySubscriptionByCustomerIdBizOp_ExistingCustomerWithOutSubcriptions_ShouldThrowException()
        {
            Int32 customerId = 3;
            var request = new QuerySubscriptionByCustomerIdRequestDTO
            {
                user = "123456", //ignored by test fw
                password = "123456789", //ignored by test fw
                vmno = "123456", //ignored by test fw
                CustomerId = customerId, //Mocked by the repository
            };

            MockAbsctractBusinessOperation(request);
            MockRepositoryManager.GetMockedRepository<ICustomerInfoRepository<CustomerInfo>>()
                .GetById(customerId).Returns(
               new CustomerInfo
               {
                   ResourceMBInfo = new ResourceMBInfo[] { },
               }
               );

            var resp = CallBizOp(request);

            Assert.AreEqual(resp.Subscription, null);
            Assert.AreEqual(resp.resultType, ResultTypes.BussinessLogicError);
            Assert.AreEqual(resp.errorCode, BizOpsErrors.CustomerWithoutSubscriptions);
        }
    }
}