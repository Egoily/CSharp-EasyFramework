using System;
using System.Threading;
using com.etak.core.repository.crm.Nhibernate.Factory;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    /// <summary>
    /// Summary description for TableBasedSequenceProviderTest
    /// </summary>
    [TestFixture]
    public class TableBasedSequenceProviderTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void GetNextSequenceTest()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var sequenceProvider = RepositoryManager.GetRepository<ISequenceProvider>();
                    var nextSeq1 = sequenceProvider.GetNextSequence("CRM_MVNO_OPERATION_LOG.ORDERCODE:700000");
                    Console.Out.WriteLine("1st run: {0}", nextSeq1);

                    var nextSeq2 = sequenceProvider.GetNextSequence("CRM_MVNO_OPERATION_LOG.ORDERCODE:700000");
                    Console.Out.WriteLine("2nd run: {0}", nextSeq2);

                    var nextSeq3 = sequenceProvider.GetNextSequence("CRM_MVNO_OPERATION_LOG.ORDERCODE:700000");
                    Console.Out.WriteLine("3nd run: {0}", nextSeq3);

                    Assert.AreEqual(nextSeq1, nextSeq2 - 1);
                    trx.Commit();
                }
            }
        }

        [Test]
        public void GetNextSequencePerformanceTest()
        {
            //for (var i = 0; i < 10; i++)
            //{
            //    (new Thread(new ThreadStart(() =>
            //    {
            //        for (var j = 0; j < 10; j++)
            //        {
            //            using (var conn = RepositoryManager.GetNewConnection())
            //            {
            //                using (var trx = conn.BeginTransaction())
            //                {
            //                    var sequenceProvider = RepositoryManager.GetRepository<ISequenceProvider>();
            //                    var nextSeq1 = sequenceProvider.GetNextSequence("CRM_MVNO_OPERATION_LOG.ORDERCODE:700000");
            //                    Console.Out.WriteLine("Thread {0} get: {1}", Thread.CurrentThread.Name, nextSeq1);
            //                    trx.Commit();
            //                }
            //            }
            //            //Thread.CurrentThread.Join(1);
            //        }

            //    })) { Name = "Thread" +i }).Start();

            //}

            //Thread.CurrentThread.Join(40000);

            (new Thread(new ThreadStart(() =>
            {
                using (var conn = RepositoryManager.GetNewConnection())
                {
                    using (var trx = conn.BeginTransaction())
                    {
                        var sequenceProvider = RepositoryManager.GetRepository<ISequenceProvider>();
                        var nextSeq1 = sequenceProvider.GetNextSequence("CRM_MVNO_OPERATION_LOG.ORDERCODE:700000");
                        Console.Out.WriteLine("Thread {0} get: {1}", Thread.CurrentThread.Name, nextSeq1);
                        Thread.CurrentThread.Join(60000);
                        trx.Commit();
                    }
                }
            })) { Name = "Sub Thread" }).Start();

            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var sequenceProvider = RepositoryManager.GetRepository<ISequenceProvider>();
                    var nextSeq1 = sequenceProvider.GetNextSequence("CRM_MVNO_OPERATION_LOG.ORDERCODE:700000");
                    Console.Out.WriteLine("Thread {0} get: {1}", Thread.CurrentThread.Name, nextSeq1);
                    trx.Commit();
                }
            }
        }
    }
}