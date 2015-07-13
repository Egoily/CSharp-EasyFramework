using System;
using System.Reflection;

using log4net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.repository.crm.Nhibernate.UnitTests
{
    [TestClass]
    public abstract class AbstractRepositoryUnitTests<TRepository, TEntity, TKey>
        where TRepository : IRepository<TEntity, TKey>
        where TEntity : class
    {
        protected static ILog Log;

        [ClassInitialize]
        public static void InitializeSessionFactoryAndLogging(TestContext testContext)
        {
            log4net.Config.XmlConfigurator.Configure();
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            SessionFactoryUnitTests.BuildSessionFactory();
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

        protected abstract TKey ExistingId { get; }

        [TestMethod]
        public virtual void GetById()
        {
            this.DoTransacted(repo => repo.GetById(this.ExistingId));
        }
    }
}