using System;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.operations.messages;

namespace com.etak.core.operation.IntTests.operations
{
    class CreateDataAndThrowError : AbstractSinglePhaseOrderProcessor<FakeOrderRequestDTO, FakeOrderResponseDTO, FakeOrderRequest, FakeOrderResponse, FakeOrder>
    {
        private readonly Exception _ex;
        public override string OperationCode
        {
            get { return "CREROW"; }
        }

        public override string OperationDiscriminator
        {
            get { return "CREROW"; }
        }

        public CreateDataAndThrowError(Exception ex)
        {
            _ex = ex;
        }

        public CreateDataAndThrowError()
        {
            _ex = new NotFiniteNumberException();
        }

        public override FakeOrderResponse ProcessRequest(FakeOrder order, FakeOrderRequest request)
        {
            throw _ex;
        }

        protected override void MapNotAutomappedOrderInboundProperties(FakeOrderRequestDTO request, ref FakeOrderRequest coreInput)
        {
           
        }

        protected override void MapNotAutomappedOrderOutboundProperties(FakeOrderResponse source, ref FakeOrderResponseDTO coreOutput)
        {
            
        }
    }
}
