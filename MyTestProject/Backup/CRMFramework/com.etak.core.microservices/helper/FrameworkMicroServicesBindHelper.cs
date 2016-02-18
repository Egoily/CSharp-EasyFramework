using com.etak.core.operation.manager;

namespace com.etak.core.microservices.helper
{
    /// <summary>
    ///  Helper to bind all core microservices inside MicroServiceManager
    /// </summary>
    public static class FrameworkMicroServicesBindHelper
    {
        /// <summary>
        /// Register all microservices in MicroServiceManager
        /// </summary>
        public static void LoadMicroservices()
        {
            MicroServiceManager.RegisterMicroServicesByAssemby(typeof(FrameworkMicroServicesBindHelper).Assembly);
        }
    }
}
