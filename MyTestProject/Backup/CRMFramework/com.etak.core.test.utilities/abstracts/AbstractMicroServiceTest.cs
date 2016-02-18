using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.test.utilities.Helpers;

namespace com.etak.core.test.utilities.abstracts
{
   /// <summary>
   /// Utilities to create Microservice nunit 
   /// </summary>
   /// <typeparam name="TMicroService"></typeparam>
   /// <typeparam name="TRequest"></typeparam>
   /// <typeparam name="TResponse"></typeparam>
    public abstract class AbstractMicroServiceTest<TMicroService, TRequest, TResponse>
        where TMicroService : IMicroService<TRequest, TResponse> , new()
        where TRequest : RequestBase
        where TResponse : ResponseBase, new()
    {

        private readonly RequestInvokationEnvironment fakeInvoker = FakeInvoker.FakeInvokationEnvironment();

        /// <summary>
        /// Mock specific repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T MockRepository<T>()
            where T : class
        {
            var mockedRepository = MockRepositoryManager.GetMockedRepository<T>();
            return mockedRepository;
        }

        /// <summary>
        /// Call microservice
        /// </summary>
        /// <param name="request"></param>
        /// <returns>microservice response</returns>
        public virtual TResponse CallMicroservice(TRequest request)
        {
            var response = new TResponse();

            TMicroService ms = new TMicroService();

            response = ms.Process(request, fakeInvoker);

            return response;
        }

    }
}
