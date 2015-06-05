using System.Reflection;
using com.etak.core.operation.implementation;
using com.etak.core.operation.test.common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.operation.test.automapping.amount
{
    [TestClass]
    public class AmountAutomap : RepositoryBasedUnitTest
    {
        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            MyClassInitialize(testContext);
        }

        [TestMethod]
        public void AmountMappedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();

            AmountBasedRequestDTO reqDto = GenerateRequest<AmountBasedRequestDTO>();
            reqDto.amount = 3m;

            var op = new AlwaysOkBusinessOperation<AmountBasedRequestDTO, AmountDTOBasedResponse, AmountBasedRequest, AmountBasedResponse>();
            var resp = op.ProcessFromCustomerModel(new NullValidator<AmountBasedRequestDTO>(),
                       new SameTypeConverter<AmountBasedRequestDTO>(),
                       new SameTypeConverter<AmountDTOBasedResponse>(),
                       reqDto, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.IsNotNull(resp.Amount);

        }

    }
}
