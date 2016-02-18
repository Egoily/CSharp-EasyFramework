using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.operations.messages;

namespace com.etak.core.operation.IntTests.automapping.customer
{
    public class ReceiveCustomerOutputCustomer : AbstractBusinessOperation<FakeBizOpRequestDTO, FakeBizOpResponseDTO, FakeBizOpRequest, FakeBizOpResponse>
    {
        public override string OperationCode
        {
            get { return "RCOC"; }
        }

        public override string OperationDiscriminator
        {
            get { return "TRCOC"; }
        }

        protected override void MapNotAutomappedInboundProperties(FakeBizOpRequestDTO dtoRequest, ref FakeBizOpRequest coreInput)
        {
        }

        protected override void MapNotAutomappedOutboundProperties(FakeBizOpResponse coreOutput, ref FakeBizOpResponseDTO dtoOutput)
        {
        }

        protected override FakeBizOpResponse ProcessBusinessLogic(FakeBizOpRequest request, model.operation.BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new FakeBizOpResponse
            {
                Customer = request.Customer,
                ErrorCode = 0,
                ResultType = ResultTypes.Ok,
                Message = "Fake OP OK",
            };
        }
    }
}
