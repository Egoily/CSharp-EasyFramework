using System;
using com.etak.core.model;
using com.etak.core.repository.crm.drl;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.drl
{
    [TestFixture]
    public class IRoamingBlackListInfoRepositoryTest : 
         AbstractRepositoryTest<IRoamingBlackListInfoRepository<RoamingBlackListInfo>, RoamingBlackListInfo, Int32>
     {
         protected override int ExistingId
         {
             get { return 100; }
         }
     }
}
