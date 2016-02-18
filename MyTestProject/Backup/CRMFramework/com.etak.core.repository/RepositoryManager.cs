using System;
using System.Reflection;
using Ninject;

namespace com.etak.core.repository
{
    /// <summary>
    /// Signature to the method that will be invoked when the session is closed. 
    /// </summary>
    public delegate void ClearSessionForThread();

    /// <summary>
    /// Static class used to get Repository instances, it manages the the connection per thread and transactions.
    /// </summary>
    public static class RepositoryManager
    {
        [ThreadStatic]
        private static IPersistanceConnection _session;

        private static readonly IKernel InjectionKernel;
        private static IConnectionProvider _connectionProvider;
        private static readonly Object Lock = new Object();
       
     
        static RepositoryManager()
        {
            InjectionKernel = new StandardKernel();
        }

        /// <summary>
        /// Adds a Ninject assembly with directives to load
        /// </summary>
        /// <param name="s">the assembly containing the ninject module</param>
        static public void AddAssemby(Assembly s)
        {
            InjectionKernel.Load(s);
        }

        /// <summary>
        /// Gets factory that creates the conenctions.
        /// </summary>
        /// <returns>The connection</returns>
        static public IConnectionProvider GetConnectionProvider()
        {
            if (_connectionProvider == null)
            {
                lock (Lock)
                {
                    _connectionProvider = InjectionKernel.Get<IConnectionProvider>();
                }
            }

            return (_connectionProvider);
        }

        /// <summary>
        /// Creates a new connection to the repository,
        /// will throw an exception if the connection was already created
        /// </summary>
        /// <returns>The created connection</returns>
        public static IPersistanceConnection GetNewConnection()
        {
            if (_session != null)
                throw new ConnectionAlreadyOpened();

            _session = GetConnectionProvider().GetConnection();
            _session.SetNotifier(SesionScopeComplete);

            return (_session);
        }

        /// <summary>
        /// Closes the conenction with the DB
        /// </summary>
        public static void CloseConnection()
        {
            if (_session == null)
                throw new ConnectionNotOpened();

            _session.Close();
            _session = null;
        }

        /// <summary>
        /// This is an scope helper to allow Idisposable and closing the backing connection
        /// </summary>
        public static void SesionScopeComplete()
        {
            _session = null;
        }

        /// <summary>
        /// Gets the connection already opened for the current thread
        /// </summary>
        /// <returns>The existing connection</returns>
        public static IPersistanceConnection GetConnection()
        {
            if (_session == null)
                throw new ConnectionNotOpened();

            return _session;
        }

        /// <summary>
        /// Gets an instance of a class implementing TRepositoryOfEntity
        /// </summary>
        /// <typeparam name="TRepositoryOfEntity">The interface of the requested repository</typeparam>
        /// <returns>the instance implementing the interface</returns>
        public static TRepositoryOfEntity GetRepository<TRepositoryOfEntity>()
        {
            return (InjectionKernel.Get<TRepositoryOfEntity>());
        }

        /// <summary>
        /// Maps an Repository interface to an actual implementation
        /// </summary>
        /// <typeparam name="TInterface">The type of the repository</typeparam>
        /// <typeparam name="TClass">The class implementing the repository</typeparam>
        public static void RemapInterfaceToImplementation<TInterface, TClass>() where TClass : TInterface
        {
            InjectionKernel.Rebind<TInterface>().To<TClass>();
        }

        /// <summary>
        /// Maps an Repository interface to an actual implementation
        /// </summary>
        /// <typeparam name="TInterface">The type of the repository</typeparam>
        /// <typeparam name="TClass">The class implementing the repository</typeparam>
        /// <param name="implementation">The constant implementation to bind</param>
        public static void RemapInterfaceToConstant<TInterface, TClass>(TClass implementation) where  TClass : TInterface
        {
            InjectionKernel.Rebind<TInterface>().ToConstant(implementation);
        }
    }
}
