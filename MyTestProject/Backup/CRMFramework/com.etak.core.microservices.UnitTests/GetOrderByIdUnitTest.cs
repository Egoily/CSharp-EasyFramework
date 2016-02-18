using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.etak.core.microservices.messages.GetOrderById;
using com.etak.core.microservices.microservices;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.crm.operation;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.UniTests
{
    public class GetOrderByIdUnitTest : AbstractMicroServiceTest<GetOrderByIdMS, GetOrderByIdRequest, GetOrderByIdResponse>
    {
        private IOrderRepository<Order> mockedRepo;

        [TestFixtureSetUp]
        public void InitializeSetup()
        {
            mockedRepo = MockRepository<IOrderRepository<Order>>();
            mockedRepo.GetById(1).Returns(CreateDefaultObject.Create<TestOrder>());
            mockedRepo.GetById(2).Returns(x => { throw new Exception("Error"); });
            mockedRepo.GetById(3).Returns((Order)null);
        }

        [TestCase(1)]
        public void Process_OrderId_ShouldReturnOk(int orderId)
        {
            var expectedObject = CreateDefaultObject.Create<TestOrder>();
            var request = new GetOrderByIdRequest()
            {
                OrderId = orderId
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedObject, response.Order);

        }

        private class TestOrder : Order
        {
            public override string Discriminator
            {
                get { return "TEST"; }
            }
        }
    }
}
