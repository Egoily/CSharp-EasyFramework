using com.etak.core.operation.implementation;

namespace com.etak.core.test.utilitiesTests.BizOpTest.SinglePhaseOrderProcessorTest
{
    public class SinglePhaseOrderProcessorTestBizOp : AbstractSinglePhaseOrderProcessor<SinglePhaseOrderProcessorRequestDTO, SinglePhaseOrderProcessorResponseDTO, SinglePhaseOrderProcessorRequestInternal, SinglePhaseOrderProcessorResponseInternal, SinglePhaseOrderProcessorOrder>
    {
        public override string OperationCode
        {
            get { return "TBD"; }
        }

        public override string OperationDiscriminator
        {
            get { return "TBD"; }
        }

        public override SinglePhaseOrderProcessorResponseInternal ProcessRequest(SinglePhaseOrderProcessorOrder order,SinglePhaseOrderProcessorRequestInternal request)
        {
            return new SinglePhaseOrderProcessorResponseInternal();
        }

        protected override void MapNotAutomappedOrderInboundProperties(SinglePhaseOrderProcessorRequestDTO request,
            ref SinglePhaseOrderProcessorRequestInternal coreInput)
        {
        }

        protected override void MapNotAutomappedOrderOutboundProperties(SinglePhaseOrderProcessorResponseInternal source,
            ref SinglePhaseOrderProcessorResponseDTO DTOOutput)
        {
        }
    }
}
