using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;
using NHinnerConfig = NHibernate.Cfg.ConfigurationSchema;

namespace com.etak.core.repository.crm.Nhibernate.Factory
{
    /// <summary>
    /// Builds the Nhibernate session factories and the configurations for NH.
    /// </summary>
    public class SessionManagement
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("ETalk-SessionManagement");
        private readonly IDictionary<String, ISessionFactory> _factorys = new Dictionary<String, ISessionFactory>();
        private readonly IDictionary<String, Configuration> _configurations = new Dictionary<String, Configuration>();
        private const string ConnectionPropertyName = "connection.connection_string";
        private volatile static Lazy<SessionManagement> _instance = new Lazy<SessionManagement>(() => new SessionManagement());

        /// <summary>
        /// Gets the only instance of the SessionManagement instance (this class implements the
        /// Singleton Pattern)
        /// </summary>
        /// <returns>the Singleton instance of SessionManagement</returns>
        public static SessionManagement GetInstance()
        {
            return _instance.Value;
        }

        private SessionManagement()
        {
            try
            {
                // Step1: Add Database connection of CRM
                BuildSessionFactoryForConnectionString("CRM25", "CRM");
            }
            catch (Exception ex)
            {
                Logger.Info("Error building session factory", ex);
                throw;
            }
        }

        /// <summary>
        /// Generates the SQL file for the session factory provided
        /// </summary>
        /// <param name="sessionFactoryName">the name of the session factory</param>
        /// <param name="fileName">the output file for the SQL</param>
        public void GenerateSchemaToFile(String sessionFactoryName, String fileName)
        {
            new SchemaExport(_configurations[sessionFactoryName]).SetOutputFile(fileName).Create(false, false);
        }

        private void BuildSessionFactoryForConnectionString(String connectionStringName, String sessionFactoryName)
        {
            Logger.Info("Building " + sessionFactoryName + " NHibernate Session factory with connection string: " +
                        connectionStringName);

            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (connectionStringSettings == null || String.IsNullOrWhiteSpace(connectionStringSettings.ConnectionString))
            {
                Logger.Info("Not building session factory:" + sessionFactoryName + " connection string not found");
                return;
            }

            String connectionValue = connectionStringSettings.ConnectionString;
            Configuration crmConfig = new Configuration();
            crmConfig.SetProperty(ConnectionPropertyName, connectionValue);

            Logger.Info("Loading configuration section \"hibernate-configuration\" from application configuration file");
            NHinnerConfig.HibernateConfiguration nhConfigSection =
                (NHinnerConfig.HibernateConfiguration)ConfigurationManager.GetSection("hibernate-configuration");
            IList<Assembly> assemblyiesToLoad = new List<Assembly>();
            IList<Type> autoMappedTypes = new List<Type>();

            Logger.Info("Loading assemblies");
            foreach (var assembly2Load in nhConfigSection.SessionFactory.Mappings.Where(x => x.Assembly != null))
                assemblyiesToLoad.Add(Assembly.Load(assembly2Load.Assembly));

            //Logger.Info("Locating auto mapped requests, responses and orders");
            //foreach (var typeInAssembly in assemblyiesToLoad.SelectMany(x => x.GetTypes()))
            //    if (typeof(Order).IsAssignableFrom(typeInAssembly))
            //        autoMappedTypes.Add(typeof(OrderDynamicMapper<>).MakeGenericType(typeInAssembly));

            Logger.Info("Building configuration for hibernate");
            crmConfig = Fluently.Configure(crmConfig.Configure()).
                Mappings(m =>
                {
                    //Load all  fluent mappings classes defined in the repo
                    m.FluentMappings.AddFromAssemblyOf<SessionFactoryHelper>();

                    //Load all hbm.xml as embebbed resources the repo assembly
                    m.HbmMappings.AddFromAssemblyOf<SessionFactoryHelper>();

                    //Load Fluent mappings defined in any upper dll
                    foreach (var assembly2Load in assemblyiesToLoad)
                        m.FluentMappings.AddFromAssembly(assembly2Load);

                    //Load all dynamic automapings created dynamically (req, response and order)
                    foreach (var type2Load in autoMappedTypes)
                        m.FluentMappings.Add(type2Load);
                })
                .BuildConfiguration();

            Logger.Info("Building session factory");
            _factorys.Add(sessionFactoryName, crmConfig.BuildSessionFactory());
            _configurations.Add(sessionFactoryName, crmConfig);
        }

        /// <summary>
        /// Gets the NHiberante configuration for the session factory
        /// </summary>
        /// <param name="factoryName">the name of the session factory</param>
        /// <returns>the configuration for the session factory</returns>
        public Configuration GetConfiguration(string factoryName)
        {
            return (_configurations[factoryName]);
        }

        /// <summary>
        /// Gets an NHiberante session for the factory
        /// </summary>
        /// <param name="factoryName">the name of the factory from which we want a session</param>
        /// <returns>the Nhibernate session</returns>
        public ISession GetSession(string factoryName)
        {
            return (_factorys[factoryName]).OpenSession();
        }

        /// <summary>
        /// Gets an NHiberante stateless session for the factory
        /// </summary>
        /// <param name="factoryName">the name of the factory from which we want a session</param>
        /// <returns>the Nhibernate session</returns>
        public IStatelessSession GetStateLessSession(String factoryName)
        {
            return (_factorys[factoryName]).OpenStatelessSession();
        }

        /// <summary>
        /// Gets the session factory for a given name
        /// </summary>
        /// <param name="factoryName">the name of the factory we want to retreive</param>
        /// <returns>the Nhibernate session factory</returns>
        public ISessionFactory GetSessionFactory(string factoryName)
        {
            return (_factorys[factoryName]);
        }
    }
}