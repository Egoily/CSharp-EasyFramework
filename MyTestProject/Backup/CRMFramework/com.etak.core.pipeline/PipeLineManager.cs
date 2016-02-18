using System;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.pipeline.builders;
using Ninject;

namespace com.etak.core.pipeline
{
    /// <summary>
    /// Links customer front end model to ET business operations
    /// </summary>
    public static class PipeLineManager
    {
        private static readonly IKernel kernel = new Ninject.StandardKernel();

        /// <summary>
        /// Gets a registered type converter for the Types provided
        /// </summary>
        /// <typeparam name="TSource">The source of the type to be converted</typeparam>
        /// <typeparam name="TDestination">The destination type of the conversion</typeparam>
        /// <returns>The ITypeConverter for TSource to TDestination</returns>
        public static ITypeConverter<TSource, TDestination> GetTypeConverter<TSource, TDestination>()
        {
            return kernel.Get<ITypeConverter<TSource, TDestination>>();
        }

        /// <summary>
        /// Method to get the internal implementation of an operation in DTO model
        /// </summary>
        /// <typeparam name="TInput">The input parameter type in DTO ETAK form</typeparam>
        /// <typeparam name="TOutput">The output parameter type in DTO ETAK form</typeparam>
        /// <returns>the IDTOBusinessOperation for the given types</returns>
        public static IDTOBusinessOperation<TInput, TOutput> GetBusinessOperation<TInput, TOutput>()
            where TInput : RequestBaseDTO
            where TOutput : ResponseBaseDTO
        {
            return kernel.Get<IDTOBusinessOperation<TInput, TOutput>>();
        }

        #region Methods for IDefaultResponseGenerator
        /// <summary>
        /// Gets the default message generator in case of unhandled error
        /// </summary>
        /// <typeparam name="TExternalOutput">The type of message that needs to be generated</typeparam>
        /// <returns>The default message generator for the type</returns>
        public static IDefaultResponseGenerator<TExternalOutput> GetDefaultMessageGenerator<TExternalOutput>()
        {
            return kernel.Get<IDefaultResponseGenerator<TExternalOutput>>();
        }
        #endregion


        #region Methods for FrontEndMethods
       
        /// <summary>
        /// starts builing an operation with fluent configuration for the types
        /// </summary>
        /// <typeparam name="TInput">The input type of the Front end operation in customer model</typeparam>
        /// <typeparam name="TOutput">The output type of the Front end operation in customer model</typeparam>
        /// <returns>the fluent builder</returns>
        public static FrontEndFluentBuilder<TInput, TOutput> RegisterFrontEndOperation<TInput, TOutput>()
            where TInput : class, new()
            where TOutput : class, new()           
        {
            return (new FrontEndFluentBuilder<TInput, TOutput>(kernel));           
        }

        /// <summary>
        /// Gets the message processor for the Input and output types in Customer model
        /// </summary>
        /// <typeparam name="TInput">THe input type of the operation</typeparam>
        /// <typeparam name="TOutput">The output type of the operation</typeparam>
        /// <returns>The front end message processor for the given types</returns>
        public static IFrontEndMessageProcessor<TInput, TOutput> GetPipeline<TInput, TOutput>()
            where TInput : class
            where TOutput : class
        {
            return kernel.TryGet<IFrontEndMessageProcessor<TInput, TOutput>>();
        }
        #endregion

        #region Methods for IValidator
        /// <summary>
        /// Registers a validator with for the given type
        /// </summary>
        /// <typeparam name="TInput">The type to be validated</typeparam>
        /// <returns>the fluent validator builder for TInput</returns>
        public static IValidatorFluentBuilder<TInput> RegisterValidator<TInput>() where TInput : class, new()
        {
            return (new IValidatorFluentBuilder<TInput>(kernel));
        }

        /// <summary>
        /// Registers a validator with for the given type
        /// </summary>
        /// <typeparam name="TInput">The type to be validated</typeparam>
        /// <param name="t">the type of the validator, must implement Ivalidator for TInput</param>
        /// <returns>the fluent validator builder for TInput</returns>
        public static void RegisterValidator<TInput>(Type t) where TInput : class, new()
        {
            kernel.Bind<IValidator<TInput>>().To(t);            
        }

        /// <summary>
        /// Gets the IValidator for Tinput
        /// </summary>
        /// <typeparam name="TInput">the type to be validated</typeparam>
        /// <returns>the Ivalidator</returns>
        public static IValidator<TInput> GetValidator<TInput>()
        {
            return kernel.Get<IValidator<TInput>>();
        }
        #endregion

        #region Methods for ITypeConverter
        /// <summary>
        /// Register a ITypeConverter from TSource to TDestintation by Giving the type that implements the interface
        /// </summary>
        /// <typeparam name="TSource">The source to be converted</typeparam>
        /// <typeparam name="TDestination">the destination type of the conversion</typeparam>
        /// <param name="t">the class that implements ITypeConverter&lt;TSource, TDestination&gt;</param>
        internal static void RegisterTypeConverter<TSource, TDestination>(Type t)
            where TSource : class, new()
            where TDestination : class, new()
        {
            kernel.Bind<ITypeConverter<TSource, TDestination>>().To(t);
        }
        #endregion

        #region Methods for DTO Processsors
        /// <summary>
        /// Registers a processor for ET DTO model, the type provided must implement 
        /// IDTOBusinessOperation&lt;TCoreInput, TCoreOutput&gt;
        /// </summary>
        /// <typeparam name="TCoreInput">the DTO input type of the operation</typeparam>
        /// <typeparam name="TCoreOutput">the DTO output type of the operation</typeparam>
        /// <param name="TProcessor">The actual type that implements the IDTOBusinessOperation</param>
        internal static void RegisterProcessor<TCoreInput, TCoreOutput>(Type TProcessor)
            where TCoreInput : RequestBaseDTO, new()
            where TCoreOutput : ResponseBaseDTO, new()
        {
            kernel.Bind<IDTOBusinessOperation<TCoreInput, TCoreOutput>>().To(TProcessor);
        }
        #endregion


        internal static void RegisterDefaultResponseGenerator<TCoreOutput>(Type TDefaultMessageGenerator)
        {
            kernel.Bind<IDefaultResponseGenerator<TCoreOutput>>().To(TDefaultMessageGenerator);
        }
    }
}

