using System;
using com.etak.core.model;
using NUnit.Framework;

namespace com.etak.core.test.utilities.Tests
{
    [TestFixture()]
    public class CreateDefaultObjectTests
    {
        [Test()]
        public void CreateTest()
        {
            
            var obj = CreateDefaultObject.Create<CustomerInfo>(1);
            
            Console.Write(obj.ToString());
        }
    }
}
