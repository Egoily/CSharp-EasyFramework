using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.automapping.customer;
using com.etak.core.operation.IntTests.common;
using com.etak.core.operation.IntTests.operations.messages;
using com.etak.core.operation.UnitTests.automapping.customer;
using NUnit.Framework;


namespace com.etak.core.operation.IntTests.automapping.numberInfo
{
    [TestFixture]
    public class NumberInfoAutoMap : RepositoryBasedUnitTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            MyClassInitialize();
        }

        [Test]
        public void NumberInfoBasedRequest()
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
        public void NumberInfoBasedResponse()
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
