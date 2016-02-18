using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.automapping.customer;
using com.etak.core.operation.IntTests.common;
using com.etak.core.operation.IntTests.operations.messages;
using NUnit.Framework;

namespace com.etak.core.operation.IntTests.automapping.subscription
{
    [TestFixture]
    public class MsisdnAutomap : RepositoryBasedUnitTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            MyClassInitialize();
        }

        [Test]
        public void SubscriptionBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            FakeBizOpRequestDTO req = GenerateRequest<FakeBizOpRequestDTO>();
            req.MSISDN = "34611462095";
            var op = new AlwaysOkBusinessOperation();
            op.ProcessFromCustomerModel(new NullValidator<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpResponseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }

        [Test]
        public void MultSubscriptionBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            FakeBizOpRequestDTO req = GenerateRequest<FakeBizOpRequestDTO>();
            req.MSISDN = "34611462095";
            var op = new AlwaysOkBusinessOperation();
            op.ProcessFromCustomerModel(new NullValidator<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpRequestDTO>(),
                       new SameTypeConverter<FakeBizOpResponseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });
        }
    }
}
