using com.etak.core.model;
using com.etak.core.repository.mongo;
using Ninject.Modules;

namespace com.etak.core.repository.crm.mongo.factory
{
    /// <summary>
    /// Helper class to force loading the ninject module defined in RealHelper
    /// Initializes Nhibernate Repository and session factories.
    /// </summary>
    public class SessionFactoryHelper
    {

    }

    /// <summary>
    /// Ninject module that initializes the Nhibernate Repository and session factories.
    /// Binds Nhibernate repositories to Repository interfaces. 
    /// </summary>
    public class RealHelper : NinjectModule
    {
        public override void Load()
        {
            MongoConnectionProvider aa = new MongoConnectionProvider("mongodb://localhost");

            //Set the connection provider to the session factory existing in CRM
            Bind<IConnectionProvider>().ToConstant(aa);
           
            Bind<IOperationLogRepository<OperationLog>>().To<OperationLogRepositoryMongo<OperationLog>>();           
        }
    }
}
