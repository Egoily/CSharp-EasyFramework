using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.repository.crm.operation;
using NSubstitute;

namespace com.etak.core.test.utilities.abstracts
{
    /// <summary>
    /// Used to create nunit AbstractSinglePhaseOrderProcessor BizOp
    /// </summary>
    /// <typeparam name="IBusinessOperation"></typeparam>
    /// <typeparam name="TDTOInternalInput"></typeparam>
    /// <typeparam name="TDTOInternalOutput"></typeparam>
    /// <typeparam name="TInternalInput"></typeparam>
    /// <typeparam name="TInternalOutput"></typeparam>
    /// <typeparam name="TOrder"></typeparam>
    public class AbstractSinglePhaseOrderProcessorTest<IBusinessOperation, TDTOInternalInput, TDTOInternalOutput, TInternalInput, TInternalOutput, TOrder> : AbstractBusinessOperationTest<IBusinessOperation, TDTOInternalInput, TDTOInternalOutput, TInternalInput, TInternalOutput> 
        where IBusinessOperation :
            AbstractSinglePhaseOrderProcessor<TDTOInternalInput, TDTOInternalOutput, TInternalInput, TInternalOutput, TOrder>, new()
        where TDTOInternalInput : OrderRequestDTO, new()
        where TDTOInternalOutput : OrderResponseDTO, new()
        where TInternalInput : CreateNewOrderRequest, new()
        where TInternalOutput : CreateNewOrderResponse, new()
        where TOrder : Order, new()
      
    {
        /// <summary>
        /// Mock all the respositores used in AbstractSinglePhaseOrderProcessor with the given OrderRequestDTO 
        /// </summary>
        public void MockAbstractSinglePhaseOrderProcessor(OrderRequestDTO orderRequestDto)
        {
            MockAbsctractBusinessOperation(orderRequestDto);

            var mockRepoOrder = MockRepositoryManager.GetMockedRepository<IOrderRepository<TOrder>>();
            mockRepoOrder.GetByExternalIdAndDealer(Arg.Any<DealerInfo>(), Arg.Any<string>()).Returns(new List<TOrder>());
            mockRepoOrder.Create(Arg.Any<TOrder>()).Returns((TOrder) null);

        }
    }
}
