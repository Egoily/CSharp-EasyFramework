using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.repository;
using com.etak.core.repository.crm.operation;

namespace com.etak.core.operation.implementation
{
    static  class CreateOrderHelper
    {
        internal static TOutput GenerateOrderAndProcess<TInput, TOutput, TOrder>(TInput request,
            BusinessOperationExecution runningOperation, Func<TOrder, TInput, TOutput> func, Boolean persistOrder)
            where TOrder : Order, new()
            where TInput : CreateNewOrderRequest
            where TOutput : CreateNewOrderResponse
        {

            IOrderRepository<TOrder> orderRepo = RepositoryManager.GetRepository<IOrderRepository<TOrder>>();

            //This is not the most outer called operation, we don't need to persist the order
            if (persistOrder)
            {
                if (!String.IsNullOrWhiteSpace(request.ExternalReference))
                {
                    IEnumerable<TOrder> orders = orderRepo.GetByExternalIdAndDealer(request.MVNO,
                        request.ExternalReference);
                    TOrder previousOrder = orders.FirstOrDefault();
                    if (previousOrder != null)
                    {
                        String errMsg = String.Format("The reference {0} was already used for Order with id:{1}",
                            request.ExternalReference, previousOrder.Id);
                        throw new DuplicatedReferenceException(errMsg,
                            OperationErrorCodes.DuplicatedReferenceWhileCreatingOrder);
                    }
                }
            }

            TOrder order = new TOrder
            {
                CreationDate = DateTime.Now,
                ExternalId = request.ExternalReference,
                Dealer = request.MVNO,
                Status = "INIT",
                OperationsForOrder = new List<BusinessOperationExecution>()
            };

            order.LastUpdateDate = order.CreationDate;
            order.OperationsForOrder.Add(runningOperation);

            TOutput output = func(order, request);
            if (persistOrder)
            {
                runningOperation.OrderManaged = order;
                output.CreatedOrder = order;
                order.LastUpdateDate = DateTime.Now;
                order.CompletitionDate = order.LastUpdateDate;
                orderRepo.Create(order);
            }

            return output;
        }
    }
}
