using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.common;
using NUnit.Framework;


namespace com.etak.core.operation.IntTests.automapping.customer
{
    [TestFixture]
    public class ChargeAutomap : RepositoryBasedUnitTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            MyClassInitialize();
        }

        [Test]
        public void ChargeMappedResponse()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();

            RequestBaseDTO reqDto = GenerateRequest<RequestBaseDTO>();

            var op = new ReturnAlwaysChargeBizOp();

            var resp = op.ProcessFromCustomerModel(new NullValidator<RequestBaseDTO>(),
                       new SameTypeConverter<RequestBaseDTO>(),
                       new SameTypeConverter<ChargeBasedResponseDTO>(),
                       reqDto, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.IsNotNull(resp.ChargeCatalogDto);

        }

        [Test]
        public void MultiChargeMappedResponse()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();

            RequestBaseDTO reqDto = GenerateRequest<RequestBaseDTO>();

            var op = new ReturnAlwaysMultiChargeBizOp();

            var resp = op.ProcessFromCustomerModel(new NullValidator<RequestBaseDTO>(),
                       new SameTypeConverter<RequestBaseDTO>(),
                       new SameTypeConverter<MultiChargeBasedResponseDTO>(),
                       reqDto, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.IsNotNull(resp.ChargeCatalogDtos);
        }

    }
}
