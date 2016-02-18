using NHibernate;

namespace com.etak.core.repository.NHibernate
{
    /// <summary>
    /// Abstraction class from NHibernate session factory to the repository framework
    /// </summary>
    public class NHibernateConnectionProvider : IConnectionProvider
    {
        /// <summary>
        /// The Hibernate session factory
        /// </summary>
        private ISessionFactory sesFactory;

        /// <summary>
        /// Initializes a new ConnectionProvider/Factory by wrapping the NHibernate session factory
        /// </summary>
        /// <param name="sesFactory"></param>
        public NHibernateConnectionProvider(ISessionFactory sesFactory)
        {            
            this.sesFactory = sesFactory;
        }

        /// <summary>
        /// returns 
        /// </summary>
        /// <returns>a new Hibernate session encapsulated in a IPersistanceConnection, implemented by SessionToPersistanceAdapter</returns>
        public IPersistanceConnection GetConnection()
        {
            return new SessionToPersistanceAdapter(sesFactory.OpenSession());
        }
    }
}
