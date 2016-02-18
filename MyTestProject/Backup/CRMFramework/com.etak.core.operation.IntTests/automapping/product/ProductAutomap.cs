using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.common;
using NUnit.Framework;

namespace com.etak.core.operation.IntTests.automapping.product
{
    [TestFixture]
    public class ProductAutomap : RepositoryBasedUnitTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            MyClassInitialize();
        }

        [Test]
        public void ProductMappedResponse()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();

            RequestBaseDTO reqDto = GenerateRequest<RequestBaseDTO>();

            var op = new ReturnAlwaysProduct();

            var resp = op.ProcessFromCustomerModel(new NullValidator<RequestBaseDTO>(),
                       new SameTypeConverter<RequestBaseDTO>(),
                       new SameTypeConverter<ProductOfferingDTOBasedResponse>(),
                       reqDto, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.IsNotNull(resp.ProductCatalog);

        }

        [Test]
        public void MultiProductMappedResponse()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();

            RequestBaseDTO reqDto = GenerateRequest<RequestBaseDTO>();

            var op = new ReturnAlwaysMultiProduct();

            var resp = op.ProcessFromCustomerModel(new NullValidator<RequestBaseDTO>(),
                       new SameTypeConverter<RequestBaseDTO>(),
                       new SameTypeConverter<MultiProductOfferingDTOBasedResponse>(),
                       reqDto, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.IsNotNull(resp.ProductCatalogDtos);
        }

    }
}
