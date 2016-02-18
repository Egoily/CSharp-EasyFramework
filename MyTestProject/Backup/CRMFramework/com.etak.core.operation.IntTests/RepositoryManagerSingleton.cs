using com.etak.core.repository;
using com.etak.core.repository.crm.Nhibernate.Factory;

namespace com.etak.core.operation.IntTests
{
    public class RepositoryManagerSingleton
    {
        private static RepositoryManagerSingleton instance;

        private RepositoryManagerSingleton()
        {
            RepositoryManager.AddAssemby(typeof(SessionFactoryHelper).Assembly);
        }

        public static RepositoryManagerSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RepositoryManagerSingleton();
                }
                return instance;
            }
        }
    }
}
