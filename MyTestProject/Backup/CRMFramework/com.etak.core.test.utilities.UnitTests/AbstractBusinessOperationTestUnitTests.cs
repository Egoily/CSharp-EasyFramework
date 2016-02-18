using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using com.etak.core.test.utilities.UnitTests.BizOpTest;
using com.etak.core.test.utilities.UnitTests.MicroServiceTest;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.test.utilities.UnitTests
{
    [TestFixture()]
    class AbstractBusinessOperationTestUnitTests : AbstractBusinessOperationTest<BizOpTestBizop, BizOpTestRequestDTO, BizOpTestResponseDTO, BizOpTestRequestInternal, BizOpTestResponseInternal>
    {
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test()]
        public void CallBizOp_ExistingBizOpTestRequestDTOGiven_ShouldReturnResponseWithExpectedRoamingBlackListInfos()
        {
            var actualMicroserviceTestResponse =
                CreateDefaultObject.Create<MicroserviceTestResponse>();
            var actualRoamingBlackListInfos = new List<RoamingBlackListInfo>
            {
                CreateDefaultObject.Create<RoamingBlackListInfo>(),
                CreateDefaultObject.Create<RoamingBlackListInfo>()
            };
            actualMicroserviceTestResponse.list = actualRoamingBlackListInfos;


            var expectedRoamingBlackListInfos = new List<RoamingBlackListInfo>
            {
                CreateDefaultObject.Create<RoamingBlackListInfo>(),
                CreateDefaultObject.Create<RoamingBlackListInfo>()
            };

            var actualRequestMs = Arg.Is<MicroserviceTestRequest>( x => x.MSISDN == "1675003129");

            var mocked = MockMicroServiceManager.GetMockedMicroService<MicroserviceTestRequest, MicroserviceTestResponse>();
            mocked.Process(actualRequestMs, FakeInvoker).Returns(actualMicroserviceTestResponse);

            var actualRequestBizOp = new BizOpTestRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1675003129",
            };

            MockAbsctractBusinessOperation(actualRequestBizOp);
            
            var response = CallBizOp(actualRequestBizOp);
            AssertExt.ObjectPropertiesAreEqual(expectedRoamingBlackListInfos, response.list);
        }

    }
}
