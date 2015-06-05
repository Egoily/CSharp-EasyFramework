using com.etak.core.repository;
using Ninject.Modules;
using NSubstitute;

namespace com.etak.core.test.utilities.Helpers
{
    /// <summary>
    /// Fake sessionFactoryHelper for tests
    /// </summary>
    public class FakeSessionFactoryHelper : NinjectModule
    {
        /// <summary>
        /// Load the mocked connection provider
        /// </summary>
        public override void Load()
        {
            var mockedConnectionProvider = Substitute.For<IConnectionProvider>();
            Bind<IConnectionProvider>().ToConstant(mockedConnectionProvider);

        }
    }
}
