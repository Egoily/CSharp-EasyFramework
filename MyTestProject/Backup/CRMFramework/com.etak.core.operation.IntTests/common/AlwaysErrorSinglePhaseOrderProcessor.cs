using System;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.operations;
using com.etak.core.operation.IntTests.operations.messages;

namespace com.etak.core.operation.IntTests.common
{
    class AlwaysErrorSinglePhaseOrderProcessor : AbstractSinglePhaseOrderProcessor<FakeOrderRequestDTO, FakeOrderResponseDTO, FakeOrderRequest, FakeOrderResponse, FakeOrder>
    {
        private readonly Exception _ex;

        public override string OperationCode
        {
            get { return "UTST"; }
        }

        public override string OperationDiscriminator
        {
            get { return "ALER"; }
        }

        public AlwaysErrorSinglePhaseOrderProcessor(Exception ex)
        {
            _ex = ex;
        }

        public AlwaysErrorSinglePhaseOrderProcessor()
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

        protected override void MapNotAutomappedOrderOutboundProperties(FakeOrderResponse source, ref FakeOrderResponseDTO DTOOutput)
        {
           
        }
    }
}
