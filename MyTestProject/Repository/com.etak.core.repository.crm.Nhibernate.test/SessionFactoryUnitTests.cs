using com.etak.core.repository.crm.Nhibernate.Factory;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.repository.crm.Nhibernate.UnitTests
{
    public enum SessionFactoryProviders
    {
        NHibernate,
        Mongo,
    }

    [TestClass]
    public class SessionFactoryUnitTests
    {
        public static readonly SessionFactoryProviders CurrentSessionFactory = SessionFactoryProviders.NHibernate;

        [ClassInitialize()]
        public static void IntializeLog(TestContext testContext)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [TestMethod]
        public void InitializeNhibernateSessionFactory()
        {
            RepositoryManager.AddAssemby(typeof(com.etak.core.repository.crm.Nhibernate.Factory.SessionFactoryHelper).Assembly);
        }

        [TestMethod]
        public void InitializeMongoSessionFactory()
        {
            //RepositoryManager.AddAssemby(typeof(com.etak.core.repository.crm.mongo.factory.SessionFactoryHelper).Assembly);
        }

        [TestMethod]
        public void GenerateSchema()
        {
            SessionManagement.GetInstance().GenerateSchemaToFile("CRM", @"C:\Users\joseluis.pedrosa\Desktop\CRM_Schema.sql");
        }

        public static void BuildSessionFactory(SessionFactoryProviders provider)
        {
            switch (provider)
            {
                case SessionFactoryProviders.Mongo:
                    // RepositoryManager.AddAssemby(typeof(com.etak.core.repository.crm.mongo.factory.SessionFactoryHelper).Assembly);
                    break;

                case SessionFactoryProviders.NHibernate:
                    RepositoryManager.AddAssemby(typeof(com.etak.core.repository.crm.Nhibernate.Factory.SessionFactoryHelper).Assembly);
                    break;
            }
        }

        public static void BuildSessionFactory()
        {
            BuildSessionFactory(CurrentSessionFactory);
        }
    }
}