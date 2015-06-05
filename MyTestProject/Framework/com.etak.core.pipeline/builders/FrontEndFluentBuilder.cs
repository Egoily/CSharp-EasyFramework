using Ninject;

namespace com.etak.core.pipeline.builders
{
    /// <summary>
    /// Fluent builder for front end operations
    /// </summary>
    /// <typeparam name="TInput">The input type that needs to be proccesed in customer model</typeparam>
    /// <typeparam name="TOutput">The output type that will be sent in customer model</typeparam>
    public class FrontEndFluentBuilder<TInput, TOutput> 
        where TInput : class, new()
        where TOutput : class, new()
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Creates a new builder
        /// </summary>
        /// <param name="kernel">the kernel were the configuration for types will be stored</param>
        public FrontEndFluentBuilder(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Binds the pipeline to a processor.
        /// </summary>
        /// <typeparam name="TProcessor">The type of the processor, it must impement IFrontEndMessageProcessor &lt;TInput,TOutput&gt;</typeparam>
        public void To<TProcessor>() where TProcessor : IFrontEndMessageProcessor<TInput, TOutput>
        {
            _kernel.Bind<IFrontEndMessageProcessor<TInput, TOutput>>().To<TProcessor>();
        }
    }
}
