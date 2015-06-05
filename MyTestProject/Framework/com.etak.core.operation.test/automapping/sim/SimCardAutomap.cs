using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.test.automapping.customer;
using com.etak.core.operation.test.common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.operation.test.automapping.sim
{
    [TestClass]
    public class SimCardAutomap : RepositoryBasedUnitTest
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
            ICCIDBasedRequestDTO req = GenerateRequest<ICCIDBasedRequestDTO>();
            req.ICCID = "140000000000000996";
            var op = new AlwaysOkBusinessOperation<ICCIDBasedRequestDTO, ResponseBaseDTO, SimCardBasedRequest, CustomerBasedResponse>();
            op.ProcessFromCustomerModel(new NullValidator<ICCIDBasedRequestDTO>(),
                       new SameTypeConverter<ICCIDBasedRequestDTO>(),
                       new SameTypeConverter<ResponseBaseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }
    }
}
