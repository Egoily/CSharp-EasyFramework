using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using NSubstitute;

namespace com.etak.core.test.utilities
{
    /// <summary>
    /// Mock for MicroServiceManager, mock MicroService and register in MicroServiceManager
    /// </summary>
    public class MockMicroServiceManager
    {
        /// <summary>
        /// Mock the specified microservice and register in MicroServiceManager
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <returns>Mocked Microservices</returns>
        public static IMicroService<TInput, TOutput> GetMockedMicroService<TInput, TOutput>()
            where TInput : RequestBase
            where TOutput : ResponseBase
        {
            var mockedMicroService = Substitute.For<IMicroService<TInput, TOutput>>();
            MicroServiceManager.RegisterTrazedMicroServiceWith<TInput, TOutput>().To<IMicroService<TInput, TOutput>>(mockedMicroService);
            return mockedMicroService;
        }
    }
}
