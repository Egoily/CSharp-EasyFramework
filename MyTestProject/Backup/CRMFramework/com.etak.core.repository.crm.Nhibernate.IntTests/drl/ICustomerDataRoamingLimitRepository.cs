using System;
using com.etak.core.model;
using com.etak.core.repository.crm.drl;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.drl
{
    [TestFixture]
    public class ICustomerDataRoamingLimitRepositoryTest : AbstractRepositoryTest<ICustomerDataRoamingLimitRepository<CustomerDataRoamingLimit>, CustomerDataRoamingLimit, Int64>
    {

        protected override long ExistingId
        {
            get { return 100; }
        }
    }

   
}
