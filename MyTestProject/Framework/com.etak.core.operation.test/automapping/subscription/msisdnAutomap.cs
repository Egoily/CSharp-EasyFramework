using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.test.automapping.customer;
using com.etak.core.operation.test.common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.operation.test.automapping.subscription
{
    [TestClass]
    public class MsisdnAutomap : RepositoryBasedUnitTest
    {
        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            MyClassInitialize(testContext);
        }

        [TestMethod]
        public void SubscriptionBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            MsisdnBasedRequestDTO req = GenerateRequest<MsisdnBasedRequestDTO>();
            req.MSISDN = "34611462095";
            var op = new AlwaysOkBusinessOperation<MsisdnBasedRequestDTO, ResponseBaseDTO, SubscriptioBasedRequest, CustomerBasedResponse>();
            op.ProcessFromCustomerModel(new NullValidator<MsisdnBasedRequestDTO>(),
                       new SameTypeConverter<MsisdnBasedRequestDTO>(),
                       new SameTypeConverter<ResponseBaseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }
    }
}
