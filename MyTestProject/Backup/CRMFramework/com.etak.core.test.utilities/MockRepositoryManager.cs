using com.etak.core.repository;
using NSubstitute;

namespace com.etak.core.test.utilities
{
    /// <summary>
    /// Mangager used to mock repositories
    /// </summary>
    public class MockRepositoryManager
    {
        /// <summary>
        /// Set repository class you want to get mocked and remap in RepositoryManger
        /// </summary>
        /// <typeparam name="TInterfaceRepository"></typeparam>
        /// <returns>mocked repository</returns>
        public static TInterfaceRepository GetMockedRepository<TInterfaceRepository>() 
            where TInterfaceRepository : class
        {
            var newMockedRepo = Substitute.For<TInterfaceRepository>();
            RepositoryManager.RemapInterfaceToConstant<TInterfaceRepository, TInterfaceRepository>(newMockedRepo);

            return newMockedRepo;
        }
    }
}
