using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.implementation;
using com.etak.core.operation.test.automapping.amount;
using com.etak.core.operation.test.common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.operation.test.automapping.customer
{
    [TestClass]
    public class ChargeAutomap : RepositoryBasedUnitTest
    {
        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            MyClassInitialize(testContext);
        }

        [TestMethod]
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

        [TestMethod]
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
