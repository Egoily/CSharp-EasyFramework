using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.contract
{
    /// <summary>
    /// Interface for business operations that can be invoked externaly by with a DTO model
    /// </summary>
    /// <typeparam name="TDTOInternalInput">The type of the input parameter in the DTO form</typeparam>
    /// <typeparam name="TDTOInternalOutput">The type of the output parameter in the DTO form</typeparam>
     public interface IDTOBusinessOperation<TDTOInternalInput, TDTOInternalOutput>
        where TDTOInternalInput : RequestBaseDTO
        where TDTOInternalOutput : ResponseBaseDTO
    {

         /// <summary>
         /// Process a request in the customer form, 
         /// </summary>
         /// <typeparam name="TDTOExternalIn">The request in DTO customer model</typeparam>
         /// <typeparam name="TDTOExternalOut">The response in the DTO customer model</typeparam>
         /// <param name="validator">the validator for the request in customer model</param>
         /// <param name="inboundConverter">the ITypeConverter for the input request in DTO form to the ET DTO form</param>
         /// <param name="outboundConverter">the ITypeConverter for the output request in DTO from to the Customer DTO form</param>
         /// <param name="request">the request in customer DTO model</param>
         /// <param name="invoker">the request invokation environment</param>
         /// <returns>the result in customer model</returns>
         TDTOExternalOut ProcessFromCustomerModel<TDTOExternalIn, TDTOExternalOut>(IValidator<TDTOExternalIn> validator, 
                                                        ITypeConverter<TDTOExternalIn, TDTOInternalInput> inboundConverter,
                                                        ITypeConverter<TDTOInternalOutput, TDTOExternalOut> outboundConverter,
                                                        TDTOExternalIn request, 
                                                        RequestInvokationEnvironment invoker);


    }
}
