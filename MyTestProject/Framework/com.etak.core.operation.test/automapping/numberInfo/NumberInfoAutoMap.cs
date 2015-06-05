using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.test.automapping.customer;
using com.etak.core.operation.test.common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.operation.test.automapping.numberInfo
{
    [TestClass]
    public class NumberInfoAutoMap : RepositoryBasedUnitTest
    {
        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            MyClassInitialize(testContext);
        }

        [TestMethod]
        public void NumberInfoBasedRequest()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            MsisdnBasedRequestForNumberDTO req = GenerateRequest<MsisdnBasedRequestForNumberDTO>();
            req.MSISDN = "34611462095";
            var op = new AlwaysOkBusinessOperation<MsisdnBasedRequestForNumberDTO, ResponseBaseDTO, NumberInfoBasedRequest, CustomerBasedResponse>();
            op.ProcessFromCustomerModel(new NullValidator<MsisdnBasedRequestForNumberDTO>(),
                       new SameTypeConverter<MsisdnBasedRequestForNumberDTO>(),
                       new SameTypeConverter<ResponseBaseDTO>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }

        [TestMethod]
        public void NumberInfoBasedResponse()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            MsisdnBasedRequestForNumberDTO req = GenerateRequest<MsisdnBasedRequestForNumberDTO>();
            req.MSISDN = "34611462095";
            var op = new AlwaysOkBusinessOperation<MsisdnBasedRequestForNumberDTO, NumberInfoDTOBasedResponse, NumberInfoBasedRequest, NumberInfoBasedResponse>();
            op.ProcessFromCustomerModel(new NullValidator<MsisdnBasedRequestForNumberDTO>(),
                       new SameTypeConverter<MsisdnBasedRequestForNumberDTO>(),
                       new SameTypeConverter<NumberInfoDTOBasedResponse>(),
                       req, new RequestInvokationEnvironment { Invoker = minfo });

        }
    }
}
