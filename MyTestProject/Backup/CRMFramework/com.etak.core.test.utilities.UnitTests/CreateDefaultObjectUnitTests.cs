using System;
using com.etak.core.model;
using NUnit.Framework;

namespace com.etak.core.test.utilities.UnitTests
{
    [TestFixture()]
    public class CreateDefaultObjectUnitTests
    {
        [Test()]
        public void Create_ExistingCustomerInfoClass_ShouldReturnDefaultCustomerInfo()
        {
            var obj = CreateDefaultObject.Create<CustomerInfo>(1);
            
            Console.Write(obj.ToString());
        }
    }
}
