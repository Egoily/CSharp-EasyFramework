using com.etak.core.bizops.fullfilment.QuerySubscriptionByMsisdn;
using com.etak.core.model.operation.messages;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.QuerySubscriptionByMsisdn
{
    [TestFixture()]
    public class QuerySubscriptionByMsisdnBizOpUnitTests :
        AbstractBusinessOperationTest<QuerySubscriptionByMsisdnOperation,
                                       QuerySubscriptionByMsisdnDTORequest, QuerySubscriptionByMsisdnDTOResponse,
                                       QuerySubscriptionByMsisdnRequestInternal, QuerySubscriptionByMsisdnResponseInternal>
    {
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test]
        public void QuerySubscriptionByMsisdnBizOp_ExistingCustomer_ShouldReturnCorrectSubscription()
        {
            var request = new QuerySubscriptionByMsisdnDTORequest
            {
                user = "123456", //ignored by test fw
                password = "123456789", //ignored by test fw
                vmno = "123456", //ignored by test fw
                MSISDN = "12345678" //Mocked by the repository
            };

            MockAbsctractBusinessOperation(request);
            var resp = this.CallBizOp(request);

            Assert.AreNotEqual(resp.Subscription, null);
            Assert.AreNotEqual(resp.Customer,null);
            Assert.AreEqual(resp.errorCode, 0);
            Assert.AreEqual(resp.resultType, ResultTypes.Ok);
            Assert.AreEqual(resp.errorCode, 0);
        }
    }
}