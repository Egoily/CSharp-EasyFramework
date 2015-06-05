using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.test.utilities.Tests
{
    [TestFixture()]
    public class MockRepositoryManagerTests
    {
        [Test()]
        public void GetMockedRepositoryTest()
        {
            var mocked = MockRepositoryManager.GetMockedRepository<IDealerInfoRepository<DealerInfo>>();
            
            mocked.GetByDealerIdAndCache(Arg.Any<int>())
                .Returns(new List<DealerInfo>()
                {
                    CreateDefaultObject.Create<DealerInfo>(1),
                });

            var DealerInfo = mocked.GetByDealerIdAndCache(1);

            NUnit.Framework.Assert.IsNotEmpty(DealerInfo.First().City);
        }
    }
}
