using System;
using System.Reflection;
using log4net;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public abstract class AbstractRepositoryTest<TRepository, TEntity, TKey>
        where TRepository : IRepository<TEntity, TKey> where TEntity : class
    {
        protected static ILog Log;

        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            SessionFactoryTest.BuildSessionFactory();
            
        }

        public void DoTransacted(Action<TRepository> func)
        {            
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<TRepository>();
                using (var trx = conn.BeginTransaction())
                {
                    func.Invoke(repo);
                    trx.Commit();
                }
            }
        }

        public void DoUnTransacted(Action<TRepository> func)
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<TRepository>();

                func.Invoke(repo);
            }
        }

        protected abstract TKey  ExistingId { get; } 

        [Test]
        public virtual void GetById()
        {
        //    DoTransacted(repo => repo.GetById(ExistingId));
        }


    }
}
