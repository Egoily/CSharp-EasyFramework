﻿using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.drl;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilitiesTests.MicroServiceTest;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.test.utilitiesTests
{
    [TestFixture()]
    class AbstractMicroserviceTestTest : AbstractMicroServiceTest<MicroserviceTest, MicroserviceTestRequest, MicroserviceTestResponse>
    {
        [Test()]
        public void AbstractTest()
        {
            var mock = MockRepository<IRoamingBlackListInfoRepository<RoamingBlackListInfo>>();
            mock.GetByCustomerID(1000).ReturnsForAnyArgs(new List<RoamingBlackListInfo>(){CreateDefaultObject.Create<RoamingBlackListInfo>(1)});

            var response = CallMicroservice(new MicroserviceTestRequest());
            var resList = response.list.ToList();
            var expected = new List<RoamingBlackListInfo>(){CreateDefaultObject.Create<RoamingBlackListInfo>(1)};

            AssertExt.ObjectPropertiesAreEqual(resList, expected);
        }

    }
}
