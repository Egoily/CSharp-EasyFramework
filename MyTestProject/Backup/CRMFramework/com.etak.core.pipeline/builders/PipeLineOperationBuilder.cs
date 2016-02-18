using com.etak.core.model.operation.messages;

namespace com.etak.core.pipeline.builders
{
    /// <summary>
    /// Helper class to start a fluent operation build
    /// </summary>
    public static class PipeLineOperationBuilder
    {
        /// <summary>
        /// Start a Fluent message configuration
        /// </summary>
        /// <typeparam name="TExtInput">The DTO model for the input</typeparam>
        /// <typeparam name="TExtOutput">The DTO model for the output</typeparam>
        /// <typeparam name="TCoreInput">the core model for the input</typeparam>
        /// <typeparam name="TCoreOutput">the core model for the output</typeparam>
        /// <returns>the fluent builedr for the given types</returns>
        public static FluentBasicMessageProcessorLinker<TExtInput, TExtOutput, TCoreInput, TCoreOutput> BuildSimpleMessageProcessor<TExtInput, TExtOutput, TCoreInput, TCoreOutput>()
            where TExtInput : class, new()
            where TExtOutput : class, new()
            where TCoreInput : RequestBaseDTO, new()
            where TCoreOutput : ResponseBaseDTO, new()
        {
            return new FluentBasicMessageProcessorLinker<TExtInput, TExtOutput, TCoreInput, TCoreOutput>();
        }
    }
}
