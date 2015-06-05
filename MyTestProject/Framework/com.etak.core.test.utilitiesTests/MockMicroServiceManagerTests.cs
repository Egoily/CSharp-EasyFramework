using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.operation;
using com.etak.core.test.utilities;
using com.etak.core.test.utilitiesTests.MicroServiceTest;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.test.utilitiesTests
{
    [TestFixture()]
    public class MockMicroServiceManagerTests
    {

        [Test()]
        public void GetMockedRepositoryTest()
        {
            var mocked = MockMicroServiceManager.GetMockedMicroService< MicroserviceTestRequest, MicroserviceTestResponse>();
            var actualRequestMs = new MicroserviceTestRequest()
            {
                MSISDN = "1675003129",
            };

            var actualMicroserviceTestResponse = CreateDefaultObject.Create<MicroserviceTestResponse>();
            var actualRoamingBlackListInfos = new List<RoamingBlackListInfo>();
            actualRoamingBlackListInfos.Add(CreateDefaultObject.Create<RoamingBlackListInfo>());
            actualRoamingBlackListInfos.Add(CreateDefaultObject.Create<RoamingBlackListInfo>());
            actualMicroserviceTestResponse.list = actualRoamingBlackListInfos;

            mocked.Process(actualRequestMs, null).Returns(actualMicroserviceTestResponse);


            var msResponse = MicroServiceManager.GetMicroService<MicroserviceTestRequest, MicroserviceTestResponse>()
               .Process(actualRequestMs, null);
            Assert.IsTrue(msResponse.list.Count() == 2);

        }
    }
}
