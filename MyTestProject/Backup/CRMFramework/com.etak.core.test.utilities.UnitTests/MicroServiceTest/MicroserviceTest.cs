using com.etak.core.model;
using com.etak.core.operation.contract;
using com.etak.core.repository;
using com.etak.core.repository.crm.drl;

namespace com.etak.core.test.utilities.UnitTests.MicroServiceTest
{
    public class MicroserviceTest : IMicroService<MicroserviceTestRequest, MicroserviceTestResponse>
    {

        public MicroserviceTestResponse Process(MicroserviceTestRequest request, operation.RequestInvokationEnvironment invoker)
        {
            return new MicroserviceTestResponse()
            {
                list = RepositoryManager.GetRepository<IRoamingBlackListInfoRepository<RoamingBlackListInfo>>().GetByCustomerID(1),
            };
        }
    }
}
