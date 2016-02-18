using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.contract
{
    /// <summary>
    /// Interface that all operations that may be invoked externally and internally must implement.
    /// </summary>
    /// <typeparam name="TDTOInternalInput">The type of the request to be processed in ET DTO form</typeparam>
    /// <typeparam name="TDTOInternalOutput">The type of the reponse as a result of the process in ET DTO form</typeparam>
    /// <typeparam name="TInternalInput">The type of the request to be processed in Internal/Core format</typeparam>
    /// <typeparam name="TInternalOutput">The type of the response in Internal/Core format</typeparam>
    public interface IBusinessOperation<TDTOInternalInput, TDTOInternalOutput, TInternalInput, TInternalOutput> : 
        IDTOBusinessOperation<TDTOInternalInput, TDTOInternalOutput> ,
        ICoreBusinessOperation<TInternalInput, TInternalOutput>
        where TDTOInternalInput : RequestBaseDTO
        where TDTOInternalOutput : ResponseBaseDTO
        where TInternalInput : RequestBase
        where TInternalOutput : ResponseBase
    {
        
    }
}
