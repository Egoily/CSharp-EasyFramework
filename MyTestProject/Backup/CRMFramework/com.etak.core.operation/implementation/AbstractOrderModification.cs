using System;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.repository;
using com.etak.core.repository.crm.operation;

namespace com.etak.core.operation.implementation
{
    /// <summary>
    /// Business order that updates an existing Order
    /// </summary>
    /// <typeparam name="TDTOInput">The DTO form of the request</typeparam>
    /// <typeparam name="TDTOOutput">The DTO form of the response</typeparam>
    /// <typeparam name="TInternalInput">The internal form of the request</typeparam>
    /// <typeparam name="TInternalOutput">The internal form of the response</typeparam>
    /// <typeparam name="TOrder">The type of order to be updated</typeparam>
    /// <typeparam name="TProcessor">The processor that will update the order with the request of DTOInput</typeparam>
    public abstract class AbstractOrderModification<TDTOInput, TDTOOutput, TInternalInput, TInternalOutput, TOrder, TProcessor> : AbstractBusinessOperation<TDTOInput, TDTOOutput, TInternalInput, TInternalOutput>
        where TDTOInput : ModifyOrderRequestDTO, new()
        where TDTOOutput : ModifyOrderResponseDTO, new()
        where TInternalInput : ModifyOrderRequest, new()
        where TInternalOutput : ModifyOrderReponse, new()
        where TOrder : Order, new()
        where TProcessor : IOrderProcessor<TOrder, TInternalInput, TInternalOutput>, new()
    {
        /// <summary>
        /// Loads the Order from the DB and launches the order processor
        /// </summary>
        /// <param name="request">The request to be processed</param>
        /// <param name="runningOperation">The trace for the ongoing operation</param>
        /// <param name="invoker">The request invokation environment</param>
        /// <returns></returns>
        protected override TInternalOutput ProcessBusinessLogic(TInternalInput request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
       {
            if (request == null)
                throw new DataValidationErrorException("Can't process a null request", OperationErrorCodes.NullRequestInOrderModification);

            if (request.OrderToModify == null)
                throw new DataValidationErrorException("Can't process the request, order to update was not provided", OperationErrorCodes.NullOrderInOrderModification);

            TOrder order = request.OrderToModify as TOrder;
            runningOperation.OrderManaged = order;

            if (order == null)
            {
                String errMsg = String.Format("Can't process order of type:{0}, expected type was:{1}", request.OrderToModify.GetType(), typeof(TOrder));
                throw new BusinessLogicErrorException(errMsg, OperationErrorCodes.OrderTypeMissMatch);
            }

            OrderTransition transition = new OrderTransition {SourceState = order.Status, Order = order};

            TProcessor processor = new TProcessor();
            TInternalOutput  response = processor.ProcessRequest(order, request);

            transition.Date = DateTime.Now;
            transition.DestinationState = order.Status;
            transition.TransitionCode = response.OrderTransitionCode;
            IOrderTransitionRepository<OrderTransition> transRepo = RepositoryManager.GetRepository<IOrderTransitionRepository<OrderTransition>>();
            transRepo.Create(transition);
            return response;
        }
    }
}
