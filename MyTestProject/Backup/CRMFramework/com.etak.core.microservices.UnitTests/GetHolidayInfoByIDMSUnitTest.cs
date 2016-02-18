using System;
using com.etak.core.microservices.messages.GetHolidayInfoById;
using com.etak.core.microservices.microservices;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.crm;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.UniTests
{
    [TestFixture]
    public class GetHolidayInfoByIdMSUnitTest : AbstractMicroServiceTest<GetHolidayInfoByIdMS, GetHolidayInfoByIdRequest, GetHolidayInfoByIdResponse>
    {
        private IHolidayInfoRepository<HolidayInfo> mockedRepository;

        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            mockedRepository = MockRepository<IHolidayInfoRepository<HolidayInfo>>();
            mockedRepository.GetById(20150101).Returns(CreateDefaultObject.Create<HolidayInfo>());
            mockedRepository.GetById(20150102).Returns(x => { throw new Exception("Error"); });
            mockedRepository.GetById(20150103).Returns((HolidayInfo)null);
        }

        [TestCase(20150101)]
        public void Process_CorrectHolidayId_ShouldReturnCorrectHolidayInfo(int testParameter)
        {
            var expectedObject = CreateDefaultObject.Create<HolidayInfo>();
            var request = new GetHolidayInfoByIdRequest()
            {
                HolidayId = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedObject, response.HolidayInfo);
        }

        [TestCase(20150102)]
        public void Process_RepoThrowException_ShouldThrowException(int testParameter)
        {

            var request = new GetHolidayInfoByIdRequest()
            {
                HolidayId = testParameter,
            };

            Assert.Throws<Exception>(() => CallMicroservice(request));
        }

        [TestCase(20150103)]
        public void Process_HolidayIdHasNoHolidayInfo_ShouldReturnEmptyHolidayInfo(int testParameter)
        {
            var request = new GetHolidayInfoByIdRequest()
            {
                HolidayId = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.IsNull(response.HolidayInfo);
        }
    }
}
