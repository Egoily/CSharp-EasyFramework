using System;
using System.Linq;
using com.etak.core.model;
using NUnit.Framework;

namespace com.etak.core.test.utilities.UnitTests
{
    [TestFixture()]
    public class AssertExtUnitTests
    {
        [Test()]
        public void ObjectPropertiesAreEqual_ExistingTwoEqualCustomerInfo_ShouldReturnTrue()
        {
            var customer = CreateDefaultObject.Create<CustomerInfo>(1);
            var customer2 = CreateDefaultObject.Create<CustomerInfo>(1);
            
            AssertExt.ObjectPropertiesAreEqual(customer, customer2, "CustomerID");
        }

        [Test()]
        public void ObjectPropertiesAreEqual_ExistingTwoDifferentCustomerInfo_ShouldThrowsException()
        {
            var customer = CreateDefaultObject.Create<CustomerInfo>(1);
            var customer2 = CreateDefaultObject.Create<CustomerInfo>(1);
            var property = customer.PropertyInfo.First();
            property.AcceptNews = !property.AcceptNews;

            Assert.Throws<Exception>(new TestDelegate(() => AssertExt.ObjectPropertiesAreEqual(customer, customer2, "CustomerID")));
        }
    }
}
