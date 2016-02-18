using com.etak.core.operation.contract.exceptions;

namespace com.etak.core.pipeline
{
    /// <summary>
    /// Interface to define the behaviour for create messages in case of unhandled error and no information
    /// is available
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IDefaultResponseGenerator<TResponse>
    {
        /// <summary>
        /// Generates the respose in case of unhandled exception. 
        /// </summary>
        /// <returns></returns>
        TResponse GenerateDefaultResponse();

        /// <summary>
        /// Generates the response in case of a handled/generated exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        TResponse GenerateDefaultResponse(ElephantTalkBaseException ex);
    }
}