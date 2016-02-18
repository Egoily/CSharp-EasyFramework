using com.etak.core.repository;

namespace com.etak.core.test.utilities.Helpers
{
    /// <summary>
    /// Simulates a session factory, but does not create any real DB connection
    /// </summary>
    public static class FakeSessionFactorySingleton
    {
        /// <summary>
        /// Static constructors, adding a fake session factory to the repository manager. 
        /// </summary>
        static FakeSessionFactorySingleton()
        {
            RepositoryManager.AddAssemby(typeof(FakeSessionFactoryHelper).Assembly);
        }

      
        /// <summary>
        /// Initialize 
        /// </summary>
        public static void Init()
        {

        }
    }
}
