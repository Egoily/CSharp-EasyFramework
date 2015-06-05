using System;
using com.etak.core.microservices.messages.GetHolidayInfoByDate;
using com.etak.core.microservices.microservices;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.crm;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.unittest
{
    [TestFixture]
    public class GetHolidayInfoByDateMSTest : AbstractMicroServiceTest<GetHolidayInfoByDateMS, GetHolidayInfoByDateRequest, GetHolidayInfoByDateResponse>
    {
        private IHolidayInfoRepository<HolidayInfo> mockedRepository;
        
        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            mockedRepository = MockRepository<IHolidayInfoRepository<HolidayInfo>>();
            mockedRepository.GetById(20150101).Returns(CreateDefaultObject.Create<HolidayInfo>());
            mockedRepository.GetById(20150102).Returns(x => { throw new Exception("Error"); });
            mockedRepository.GetById(20150103).Returns((HolidayInfo) null);
        }

        [TestCase(1)]
        public void GetHolidayInfoByDateMSOk(int testParameter)
        {
            var expectedObject = CreateDefaultObject.Create<HolidayInfo>();
            var request = new GetHolidayInfoByDateRequest()
            {
                Date = new DateTime(2015, 01, testParameter),
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedObject, response.HolidayInfo);
        }

        [TestCase(2)]
        public void GetHolidayInfoByDateMSNOk(int testParameter)
        {

            var request = new GetHolidayInfoByDateRequest()
            {
                Date = new DateTime(2015, 01, testParameter),
            };

            Assert.Throws<Exception>(() => CallMicroservice(request));
        }

        [TestCase(3)]
        public void GetDealerInfoByIdMSNOkNull(int testParameter)
        {
            var request = new GetHolidayInfoByDateRequest()
            {
                Date = new DateTime(2015, 01, testParameter),
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.IsNull(response.HolidayInfo);
        }
    }
}
