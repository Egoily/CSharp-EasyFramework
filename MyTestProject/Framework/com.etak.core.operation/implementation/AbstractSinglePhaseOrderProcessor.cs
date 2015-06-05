using System;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;

namespace com.etak.core.operation.implementation
{
    /// <summary>
    /// This class creates a new order and processes it with no option to continuate it.
    /// </summary>
    /// <typeparam name="TDTOInput">The external type of the request in DTO style</typeparam>
    /// <typeparam name="TDTOOutput">The external type of the response in DTO style</typeparam>
    /// <typeparam name="TInternalInput">The internal type of the request using the core model</typeparam>
    /// <typeparam name="TInternalOutput">The internal type of the response using the core model</typeparam>
    /// <typeparam name="TOrder">The type of order to be created</typeparam>
     public abstract class AbstractSinglePhaseOrderProcessor<TDTOInput, TDTOOutput, TInternalInput, TInternalOutput, TOrder> :
        AbstractBusinessOperation<TDTOInput, TDTOOutput, TInternalInput, TInternalOutput>,
        IOrderProcessor<TOrder, TInternalInput, TInternalOutput>
        where TDTOInput : OrderRequestDTO, new()
        where TDTOOutput : OrderResponseDTO, new()
        where TInternalInput : CreateNewOrderRequest, new()
        where TInternalOutput : CreateNewOrderResponse, new()
        where TOrder : Order, new()
     {
        
         /// <summary>
         /// The implementation of IOrderProcessor
         /// </summary>
         /// <param name="order">The order to be processed</param>
         /// <param name="request">The request to process</param>
         /// <returns>The result of the process</returns>
         public abstract TInternalOutput ProcessRequest(TOrder order, TInternalInput request);

        /// <summary>
        /// This method implements Abstract order, creates the order and requests the processor to process the request. 
        /// </summary>
        /// <param name="request">The request with the input parameters</param>
        /// <param name="runningOperation">The trace for the ongoing operation</param>
        /// <param name="invoker">The information about the environment of the invokation</param>
        /// <returns>The result of the process</returns>
        protected override TInternalOutput ProcessBusinessLogic(TInternalInput request,BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            Boolean persistOrder = IsRootOperation;
            return CreateOrderHelper.GenerateOrderAndProcess<TInternalInput, TInternalOutput, TOrder>(request, runningOperation, ProcessRequest, persistOrder);
        }

         /// <summary>
         /// Maps all the inboud properties of the request that are not mapped by the framework
         /// </summary>
         /// <param name="request">the request in ET DTO Form</param>
         /// <param name="coreInput">the resquest partially mapped by the core and needs to be updated</param>
        protected abstract void MapNotAutomappedOrderInboundProperties(TDTOInput request, ref TInternalInput coreInput);

        /// <summary>
        /// Maps all the outboud properties of the response that are not mapped by the framework
        /// </summary>
        /// <param name="source">the response of the core operation that needs to be mapped</param>
        /// <param name="DTOOutput">the response of the operation in DTO format</param>
        protected abstract void MapNotAutomappedOrderOutboundProperties(TInternalOutput source, ref TDTOOutput DTOOutput);

        /// <summary>
        /// Maps all fields of the core input request related to Order automatically 
        /// </summary>
        /// <param name="request">the input request in DTO ET form</param>
        /// <param name="coreInput">the input request in ET Internal/Core form</param>
        protected override void MapNotAutomappedInboundProperties(TDTOInput request, ref TInternalInput coreInput)
        {
            #region Map order Specific fields 
            coreInput.ExternalReference = request.orderReference;
            #endregion
            MapNotAutomappedOrderInboundProperties(request, ref coreInput);
        }

        /// <summary>
        /// Maps all properties related to the order in the DTO responses
        /// </summary>
        /// <param name="source">the response in the internal/Core form</param>
        /// <param name="coreOutput">the response to be filled in the DTO ET form</param>
        protected override void MapNotAutomappedOutboundProperties(TInternalOutput source, ref TDTOOutput coreOutput)
        {
            #region Map order Specific fields
            coreOutput.orderCode = source.CreatedOrder == null ? 0 : source.CreatedOrder.Id;
            #endregion
            MapNotAutomappedOrderOutboundProperties(source, ref coreOutput);
        }
     }
}
