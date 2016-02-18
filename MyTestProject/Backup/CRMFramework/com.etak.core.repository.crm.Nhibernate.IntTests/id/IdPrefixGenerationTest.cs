using System;
using System.Reflection;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using log4net;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.id
{
    public class DummyClass
    {
        public virtual Int32 Id { get; set; }
    }

    public class DummyClass2
    {
        public virtual Int32 Id { get; set; }
    }

    public class DummyClassMap : ClassMap<DummyClass>
    {
        public DummyClassMap()
        {
            Table("DummyTable");
            Id(x => x.Id).Column("ID_COLUMN").GeneratedBy.Custom<PrefixIdGenerator>();

        }
    }

    public class DummyClassMap2 : ClassMap<DummyClass2>
    {
        public DummyClassMap2()
        {
            Table("DummyTable2");
            Id(x => x.Id).Column("ID_COLUMN2").GeneratedBy.Custom<PrefixIdGenerator>();

        }
    }


    [TestFixture]
    public class IdPrefixGenerationTest
    {
        protected static ILog Log;

        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            SessionFactoryTest.BuildSessionFactory();
            RepositoryManager
                .RemapInterfaceToImplementation<IRepository<DummyClass, Int32>, NHibernateRepository<DummyClass, Int32>>
                ();
            RepositoryManager
               .RemapInterfaceToImplementation<IRepository<DummyClass2, Int32>, NHibernateRepository<DummyClass2, Int32>>
               ();

        }

        [Test]
        public virtual void GenerateIdSequence()
        {
            using (RepositoryManager.GetNewConnection())
            {
                using (var trx = RepositoryManager.GetConnection().BeginTransaction())
                {
                    IBillRunRepository<BillRun> repo = RepositoryManager.GetRepository<IBillRunRepository<BillRun>>();
                    repo.Create(new BillRun());
                    trx.Rollback();
                }
            }

            using (RepositoryManager.GetNewConnection())
            {
                using (var trx = RepositoryManager.GetConnection().BeginTransaction())
                {
                    IBillRunRepository<BillRun> repo = RepositoryManager.GetRepository<IBillRunRepository<BillRun>>();
                    repo.Create(new BillRun());
                    trx.Rollback();
                }
            }
        }
    }
}
