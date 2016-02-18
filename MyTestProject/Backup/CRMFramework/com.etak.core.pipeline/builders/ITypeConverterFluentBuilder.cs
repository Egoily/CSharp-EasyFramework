using com.etak.core.operation.contract;
using Ninject;

namespace com.etak.core.pipeline.builders
{
    /// <summary>
    /// Helper to build the validator
    /// </summary>
    /// <typeparam name="TSource">the source type of the object to be converted</typeparam>
    /// <typeparam name="TDestination">the destination type of the object converted</typeparam>
    public class ITypeConverterFluentBuilder<TSource, TDestination>
        where TSource : class, new()
        where TDestination : class, new()
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Constructor, providing a IKernel to store the mappings
        /// </summary>
        /// <param name="kernel">the kernel where the mapping will be stored</param>
        public ITypeConverterFluentBuilder(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Maps a ITypeConverter of TSource and TDestination to the operaion being built
        /// </summary>
        /// <typeparam name="TValidator">the type of the validator</typeparam>
        public void To<TValidator>() where TValidator : ITypeConverter<TSource, TDestination>
        {
            _kernel.Bind<ITypeConverter<TSource, TDestination>>().To<TValidator>();
        }
    }
}
