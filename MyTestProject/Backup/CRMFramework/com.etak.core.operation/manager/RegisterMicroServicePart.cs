using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using Ninject;

namespace com.etak.core.operation.manager
{
    /// <summary>
    /// Part of micro service fluent configuration, creates the TO "extension"
    /// to map an interface to an implementation
    /// </summary>
    /// <typeparam name="TInput">The input type of the micro service to bind</typeparam>
    /// <typeparam name="TOutput">The input type of the micro service to bind</typeparam>
    public class RegisterMicroServicePart<TInput, TOutput>
        where TInput : RequestBase
        where TOutput : ResponseBase
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// constructor with the kernel to use when To is executed.
        /// </summary>
        /// <param name="kernel">the kernel where the binding is kept</param>
        public RegisterMicroServicePart(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Performs the binding of the microservice to the type provided
        /// </summary>
        /// <typeparam name="TImpl">The implementation of the micro service</typeparam>
        public void To<TImpl>() where TImpl : IMicroService<TInput, TOutput>
        {
            _kernel.Bind<IMicroService<TInput, TOutput>>().To<TImpl>();
        }

        /// <summary>
        /// Performs the binding of the microservice implementation provided
        /// </summary>
        /// <typeparam name="TMicroService"></typeparam>
        /// <param name="implementation"></param>
        public void To<TMicroService>(TMicroService implementation) where TMicroService : IMicroService<TInput, TOutput>
        {
            _kernel.Unbind<IMicroService<TInput, TOutput>>();
            _kernel.Bind<IMicroService<TInput, TOutput>>().ToConstant(implementation);
        }
    }
}