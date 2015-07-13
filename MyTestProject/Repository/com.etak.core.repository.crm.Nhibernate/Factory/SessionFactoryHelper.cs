using com.etak.core.model;
using com.etak.core.repository.NHibernate;
using NHibernate;
using NHibernate.Dialect;
using Ninject.Modules;

namespace com.etak.core.repository.crm.Nhibernate.Factory
{
    /// <summary>
    /// Helper class to force loading the ninject module defined in RealHelper Initializes
    /// Nhibernate Repository and session factories.
    /// </summary>
    public class SessionFactoryHelper
    {
    }

    /// <summary>
    /// Ninject module that initializes the Nhibernate Repository and session factories. Binds
    /// Nhibernate repositories to Repository interfaces.
    /// </summary>
    public class RealHelper : NinjectModule
    {
        /// <summary>
        /// Builds the session factory and binds the IRepositories to each RepositoryNH
        /// </summary>
        public override void Load()
        {
            ISessionFactory sesFactory = SessionManagement.GetInstance().GetSessionFactory("CRM");
            NHibernateConnectionProvider aa = new NHibernateConnectionProvider(sesFactory);

            //Set the connection provider to the session factory existing in CRM
            Bind<IConnectionProvider>().ToConstant(aa);

            ISession sesion = sesFactory.OpenSession();
            Dialect dialect = sesion.GetSessionImplementation().Factory.Dialect;

            //Map/Bind the interfaces to NH implementations

            Bind<IAddressInfoRepository<AddressInfo>>().To<AddressInfoRepositoryNH<AddressInfo>>();
            Bind<IPersonInfoRepository<PersonInfo>>().To<PersonInfoRepositoryNH<PersonInfo>>();
            Bind<IProfileInfoRepository<ProfileInfo>>().To<ProfileInfoRepositoryNH<ProfileInfo>>();
        }
    }
}