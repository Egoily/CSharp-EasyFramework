using com.etak.core.operation.contract;
using Ninject;

namespace com.etak.core.pipeline.builders
{
    /// <summary>
    /// Fluent builder part for the validator
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public class IValidatorFluentBuilder<TInput> where TInput : class, new()
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Creates a new fluent configuration for validation
        /// </summary>
        /// <param name="kernel">the kernel to store the configured types</param>
        public IValidatorFluentBuilder(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Maps the Validator
        /// </summary>
        /// <typeparam name="TValidator">The type that must implement IValidator&lt;TInput&gt;</typeparam>
        public void To<TValidator>() where TValidator : IValidator<TInput>
        {
            _kernel.Bind<IValidator<TInput>>().To<TValidator>();
        }
    }

}
