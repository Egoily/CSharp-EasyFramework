using com.etak.core.repository.crm.Nhibernate.Factory;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    public enum SessionFactoryProviders
    {
        NHibernate,
        Mongo,
    }

    [TestFixture]
    public class SessionFactoryTest
    {
        public static readonly SessionFactoryProviders CurrentSessionFactory = SessionFactoryProviders.NHibernate;

        [TestFixtureSetUp]
        public static void IntializeLog()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [Test]
        public void InitializeNhibernateSessionFactory()
        {
            var instance = RepositoryManagerSingleton.Instance;
        }

        
        [Test]
        public void InitializeMongoSessionFactory()
        {
            //RepositoryManager.AddAssemby(typeof(com.etak.core.repository.crm.mongo.factory.SessionFactoryHelper).Assembly);
        }
        
        [Test]
        public void GenerateSchema()
        {
            //SessionManagement.GetInstance().GenerateSchemaToFile("CRM", @"C:\CRM_Schema2.sql");
        }

        public static void BuildSessionFactory(SessionFactoryProviders provider)
        {
            switch (provider)
            {
                case SessionFactoryProviders.Mongo:
                  //  RepositoryManager.AddAssemby(typeof(com.etak.core.repository.crm.mongo.factory.SessionFactoryHelper).Assembly);
                    break;

                case SessionFactoryProviders.NHibernate:
                    var instance = RepositoryManagerSingleton.Instance;
                    break;
            }
        }

        public static void BuildSessionFactory()
        {
            BuildSessionFactory(CurrentSessionFactory);
        }
    }
}
