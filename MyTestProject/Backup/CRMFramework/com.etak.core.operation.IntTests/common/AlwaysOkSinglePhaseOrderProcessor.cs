using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.operations;
using com.etak.core.operation.IntTests.operations.messages;

namespace com.etak.core.operation.IntTests.common
{
    public class AlwaysOkSinglePhaseOrderProcessor : AbstractSinglePhaseOrderProcessor<FakeOrderRequestDTO, FakeOrderResponseDTO, FakeOrderRequest, FakeOrderResponse, FakeOrder>
    {
        public override string OperationCode
        {
            get { return "UTST"; }
        }

        public override string OperationDiscriminator
        {
            get { return "AOKT"; }
        }

        public override FakeOrderResponse ProcessRequest(FakeOrder order, FakeOrderRequest request)
        {
             return new FakeOrderResponse();
        }

        protected override void MapNotAutomappedOrderInboundProperties(FakeOrderRequestDTO request, ref FakeOrderRequest coreInput)
        {
           
        }

        protected override void MapNotAutomappedOrderOutboundProperties(FakeOrderResponse source, ref FakeOrderResponseDTO DTOOutput)
        {
            
        }
    }
}
