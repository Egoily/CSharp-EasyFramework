using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.test.common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.operation.test.automapping.product
{
    [TestClass]
    public class ProductAutomap : RepositoryBasedUnitTest
    {
        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            MyClassInitialize(testContext);
        }

        [TestMethod]
        public void ProductMappedResponse()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();

            RequestBaseDTO reqDto = GenerateRequest<RequestBaseDTO>();

            var op = new ReturnAlwaysProduct<DummyRequest>();

            var resp = op.ProcessFromCustomerModel(new NullValidator<RequestBaseDTO>(),
                       new SameTypeConverter<RequestBaseDTO>(),
                       new SameTypeConverter<ProductDTOBasedResponse>(),
                       reqDto, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.IsNotNull(resp.ProductCatalog);

        }

        [TestMethod]
        public void MultiProductMappedResponse()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();

            RequestBaseDTO reqDto = GenerateRequest<RequestBaseDTO>();

            var op = new ReturnAlwaysMultiProduct<DummyRequest>();

            var resp = op.ProcessFromCustomerModel(new NullValidator<RequestBaseDTO>(),
                       new SameTypeConverter<RequestBaseDTO>(),
                       new SameTypeConverter<MultiProductDTOBasedResponse>(),
                       reqDto, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.IsNotNull(resp.ProductCatalogDtos);
        }

    }
}
