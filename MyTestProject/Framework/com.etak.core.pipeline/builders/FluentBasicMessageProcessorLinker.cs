using System;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.operation.implementation;
using com.etak.core.pipeline.processors;

namespace com.etak.core.pipeline.builders
{
    /// <summary>
    /// Helper class to link front end messages to the core implementation fluently
    /// </summary>
    /// <typeparam name="TExtInput">The external type of the input message</typeparam>
    /// <typeparam name="TExtOutput">The external type of the output message</typeparam>
    /// <typeparam name="TCoreInput">The core type of the operation</typeparam>
    /// <typeparam name="TCoreOutput">The core type of the operation</typeparam>
    public class FluentBasicMessageProcessorLinker<TExtInput, TExtOutput, TCoreInput, TCoreOutput>
        where TExtInput : class, new()
        where TExtOutput : class, new()
        where TCoreInput : RequestBaseDTO, new()
        where TCoreOutput : ResponseBaseDTO, new()
    {     
        private Type _inputValidator;
        private Type _inboundConverter;
        private Type _processor;
        private Type _outBoundConverter;
        private Type _defaultMessageGenerator;

        /// <summary>
        /// Sets the validator for the Front end message, TValidator mus implement <![CDATA[IValidator<TExtInput>]]> 
        /// </summary>
        /// <typeparam name="TValidator">The type to be used as validator</typeparam>
        /// <returns>The fluent validator</returns>
        public FluentBasicMessageProcessorLinker<TExtInput, TExtOutput, TCoreInput, TCoreOutput> FrontEndValiadtor<TValidator>() 
            where TValidator : IValidator<TExtInput>
        {
            _inputValidator = typeof(TValidator);
            return this;
        }

        /// <summary>
        /// This method will set a <![CDATA[NullValidator<TExtInput>]]> as validator that does not perform any validation
        /// </summary>
        /// <returns></returns>
        public FluentBasicMessageProcessorLinker<TExtInput, TExtOutput, TCoreInput, TCoreOutput> NoFrontEndValidator()
        {
            _inputValidator = typeof(NullValidator<TExtInput>);
            return this;
        }

        /// <summary>
        /// The converter used to transform TExtInput to TCoreInput must implement <![CDATA[ITypeConverter<TExtInput, TCoreInput>]]> 
        /// </summary>
        /// <typeparam name="TConverter">The type that implements  <![CDATA[ITypeConverter<TExtInput, TCoreInput>]]></typeparam>
        /// <returns>The fluent validator</returns>
        public FluentBasicMessageProcessorLinker<TExtInput, TExtOutput, TCoreInput, TCoreOutput> InboundConverter<TConverter>()
            where TConverter : ITypeConverter<TExtInput, TCoreInput>
        {
            _inboundConverter = typeof(TConverter);
            return this;
        }

        /// <summary>
        /// Sets the type that will be used to process the request, the type must implement <![CDATA[IBusinessOperation<TCoreInput,TCoreOutput>]]>
        /// </summary>
        /// <typeparam name="TOperationProcessor">The type that implements IBusinessOperation TCoreInput, TCoreOutput </typeparam>
        /// <returns>The fluent validator</returns>
        public FluentBasicMessageProcessorLinker<TExtInput, TExtOutput, TCoreInput, TCoreOutput> CoreOperation<TOperationProcessor>() 
            where TOperationProcessor : IDTOBusinessOperation<TCoreInput, TCoreOutput>
        {
            _processor = typeof(TOperationProcessor);
            return this;
        }

        /// <summary>
        ///  The converter used to transform TCoreOutput to TExtOutput must <![CDATA[ITypeConverter<TCoreOutput, TExtOutput>]]>  
        /// </summary>
        /// <typeparam name="TConverter"></typeparam>
        /// <returns></returns>
        public FluentBasicMessageProcessorLinker<TExtInput, TExtOutput, TCoreInput, TCoreOutput> OutboundConverter<TConverter>()
            where TConverter : ITypeConverter<TCoreOutput, TExtOutput>
        {
            _outBoundConverter = typeof(TConverter);
            return (this);
        }

        /// <summary>
        /// Sets a type used to generate the default responses, in case of unhandled Exception, must implement  <![CDATA[IDefaultResponseGenerator<TExtOutput>]]>
        /// </summary>
        /// <typeparam name="TGen">The type implementing  <![CDATA[IDefaultResponseGenerator<TExtOutput>]]></typeparam>
        /// <returns></returns>
        public FluentBasicMessageProcessorLinker<TExtInput, TExtOutput, TCoreInput, TCoreOutput> DefaultReponseGenerator<TGen>()
            where TGen : IDefaultResponseGenerator<TExtOutput>
        {
            _defaultMessageGenerator = typeof(TGen);
            return this;
        }

        /// <summary>
        /// Registers the operation built in the pipeline, checking that contains all the elements required to process it
        /// </summary>
        public void Build()
        {
            if (_inputValidator == null)
                throw new Exception("No input validator was added");

            if (_inboundConverter == null)
                throw new Exception("No input message converter added");

            if (_processor == null)
                throw new Exception("No processor was added");

            if (_outBoundConverter == null)
                throw new Exception("No output message converter was added");

            if (_defaultMessageGenerator == null)
                throw new Exception("No output default messsage generator was added");

            //Register the front end model validator
            PipeLineManager.RegisterValidator<TExtInput>(_inputValidator);

            //Register the converter for DTO Front end model to ET front end model 
            PipeLineManager.RegisterTypeConverter<TExtInput, TCoreInput>(_inboundConverter);

            //Register the processor
            PipeLineManager.RegisterProcessor<TCoreInput, TCoreOutput>(_processor);

            //Register type for the output direction
            PipeLineManager.RegisterTypeConverter<TCoreOutput, TExtOutput>(_outBoundConverter);

            //Register the default message generator
            PipeLineManager.RegisterDefaultResponseGenerator<TExtOutput>(_defaultMessageGenerator);

            //register the core operation
            PipeLineManager.RegisterFrontEndOperation<TExtInput, TExtOutput>().To<BasicMessageProcessor<TExtInput, TExtOutput, TCoreInput, TCoreOutput>>();

        }
    }
}
