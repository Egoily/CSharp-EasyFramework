using System;
using System.Linq;
using System.Reflection;
using com.etak.core.model;
using log4net;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.usage
{
    [TestFixture]
    public class UsageDetailRecordRepositoryTest
    //: AbstractRepositoryTest<IUsageDetailRecordRepository<UsageDetailRecord>, UsageDetailRecord, Int64>
    {
        protected static ILog Log;

        [TestFixtureSetUp]
        public static void Init()
        {
            log4net.Config.XmlConfigurator.Configure();
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            SessionFactoryTest.BuildSessionFactory();
        }

        [Test]
        public void GetRecordsByCustomerIdBetweenDates()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repo = RepositoryManager.GetRepository<IUsageDetailRecordRepository<UsageDetailRecord>>();

                    Log.Info("About to execute SP");
                    var usages = repo.GetRecordsByCustomerIdBetweenDates(190000, null, 1670003170,
                        DateTime.Now.Subtract(TimeSpan.FromDays(4)), DateTime.Now, FilterCdrDates.StartDate);
                    Log.InfoFormat("SP Executed, result count:{0}", usages.Count());
                }
            }
        }

        [Test]
        public void GetRecordsByMsisdnBetweenDates()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repo = RepositoryManager.GetRepository<IUsageDetailRecordRepository<UsageDetailRecord>>();
                    var usages = repo.GetRecordsByMsisdnBetweenDates(1234, null, "34600000016",
                        DateTime.Now.Subtract(TimeSpan.FromDays(4)), DateTime.Now, FilterCdrDates.StartDate);

                    foreach (var usageDetailRecord in usages)
                    {
                        Console.WriteLine(" Value " + usageDetailRecord.Bnumber);
                    }
                }
            }
        }

        //protected override Int64 ExistingId
        //{
        //    get { return (1955000923); }
        //}
    }
}
