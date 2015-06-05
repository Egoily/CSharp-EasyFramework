using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.test.utilitiesTests.Helpers
{
    [TestFixture()]
    public class AssertTests
    {
        [Test()]
        public void ObjectPropertiesAreEqualTest()
        {
            var customer = CreateDefaultObject.Create<CustomerInfo>(1);
            var customer2 = CreateDefaultObject.Create<CustomerInfo>(1);
            
            AssertExt.ObjectPropertiesAreEqual(customer, customer2, "CustomerID");
        }

        [Test()]
        public void ObjectPropertiesArentEqualTest()
        {
            var customer = CreateDefaultObject.Create<CustomerInfo>(1);
            var customer2 = CreateDefaultObject.Create<CustomerInfo>(1);
            var property = customer.PropertyInfo.First();
            property.AcceptNews = !property.AcceptNews;

            Assert.Throws<Exception>(new TestDelegate(() => AssertExt.ObjectPropertiesAreEqual(customer, customer2, "CustomerID")));
        }
    }
}
