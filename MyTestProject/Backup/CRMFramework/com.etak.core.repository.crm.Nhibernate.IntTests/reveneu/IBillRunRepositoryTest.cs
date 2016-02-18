using System;
using System.Linq;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.reveneu
{
    [TestFixture]
    public class IBillRunRepositoryTest : AbstractRepositoryTest<IBillRunRepository<BillRun>, BillRun, Int32>
    {
        protected override int ExistingId
        {
            get { return 1; }
        }

        [TestFixtureSetUp]
        public static void Init()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public void QueryBillRun()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                try
                {
                    Int32 billRunId = 1020000001;
                    var prodRepo = RepositoryManager.GetRepository<IBillRunRepository<BillRun>>();
                    BillRun billRun1 = prodRepo.GetById(billRunId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [Test]
        public void CreateBillRun()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var billCycleRepo = RepositoryManager.GetRepository<IBillCycleRepository<BillCycle>>();
                    var billRunRepo = RepositoryManager.GetRepository<IBillRunRepository<BillRun>>();

                    BillRun billRun1 = new BillRun
                    {
                        StarteDate = DateTime.Now.AddDays(-30),
                        EndDate = DateTime.Now,
                        CutOffDate = DateTime.Now.AddDays(30),
                        DueDate = DateTime.Now.AddDays(3),
                        BillingCycle = billCycleRepo.GetById(1010000000),
                        FirstUsageDetailId = 33,
                        LastUsageDetailId = 34,
                        RunDate = DateTime.Now, 
                    };
                    try
                    {
                        billRunRepo.Create(billRun1);
                        trx.Commit();
                    }
                   catch(Exception ex)
                    {
                        Console.Write(ex.Message);
                        throw ex;
                    }                   
                }
            }
        }


        [Test]
        public void GetBillRunInDatesForBillCycle()
        {
            Action<IBillRunRepository<BillRun>>  func = repo =>
            {
                IBillCycleRepository<BillCycle> billRepo = RepositoryManager.GetRepository<IBillCycleRepository<BillCycle>>();
                BillCycle cycle = billRepo.GetById(1010000000);
                DateTime dateToLook = DateTime.Now.AddMonths(-3);
                var billRuns = repo.GetBillRunInDatesForBillCycle(cycle, dateToLook);
                Int32 count = billRuns.Count();
                Console.WriteLine(count);
            };

            DoTransacted(func);
        }
    }
}
