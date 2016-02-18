using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.test.utilities.UnitTests
{
    [TestFixture()]
    public class MockRepositoryManagerUnitTests
    {
        [Test()]
        public void GetMockedRepository_ExpectedDealerInfoRepositoryWithMethodGetByDealerInCache_ShouldReturnDealerInfo()
        {
            var mocked = MockRepositoryManager.GetMockedRepository<IDealerInfoRepository<DealerInfo>>();
            
            mocked.GetByDealerIdAndCache(Arg.Any<int>())
                .Returns(new List<DealerInfo>()
                {
                    CreateDefaultObject.Create<DealerInfo>(1),
                });

            var DealerInfo = mocked.GetByDealerIdAndCache(1);

            Assert.IsNotEmpty(DealerInfo.First().City);
        }
    }
}
