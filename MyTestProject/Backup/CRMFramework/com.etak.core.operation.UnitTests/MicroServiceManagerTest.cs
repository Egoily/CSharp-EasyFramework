using com.etak.core.microservices.messages.GetHolidayInfoByDate;
using com.etak.core.microservices.microservices;
using com.etak.core.operation.manager;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests
{
    [TestFixture()]
    public class MicroServiceManagerTests
    {

        [Test()]
        public void GetMicroService_CorrectGetHolidayInfoByDateRequestAndGetHolidayInfoByDateResponse_ShouldReturnGetHolidayInfoByDateMS()
        {
            Assert.Throws<Ninject.ActivationException>(() => MicroServiceManager.GetMicroService<GetHolidayInfoByDateRequest, GetHolidayInfoByDateResponse>());
            MicroServiceManager.RegisterMicroServicesByAssemby(typeof(GetHolidayInfoByDateMS).Assembly);
            MicroServiceManager.RegisterMicroServicesByAssemby(typeof(GetHolidayInfoByDateMS).Assembly);
            var getHolidayInfoByDateMS = MicroServiceManager.GetMicroService<GetHolidayInfoByDateRequest, GetHolidayInfoByDateResponse>();
            Assert.IsTrue(getHolidayInfoByDateMS.GetType() == typeof(GetHolidayInfoByDateMS));
        }
    }
}
