using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.operations.messages;

namespace com.etak.core.operation.IntTests.common
{
    class AlwaysOkBusinessOperation :
        AbstractBusinessOperation<FakeBizOpRequestDTO,FakeBizOpResponseDTO,FakeBizOpRequest,FakeBizOpResponse>
    {
        public override string OperationCode
        {
            get { return "UTST"; }
        }

        public override string OperationDiscriminator
        {
            get { return "AOBO"; }
        }

        protected override void MapNotAutomappedInboundProperties(FakeBizOpRequestDTO dtoRequest, ref FakeBizOpRequest coreInput)
        {
        }

        protected override void MapNotAutomappedOutboundProperties(FakeBizOpResponse coreOutput, ref FakeBizOpResponseDTO dtoOutput)
        {
        }

        protected override FakeBizOpResponse ProcessBusinessLogic(FakeBizOpRequest request, model.operation.BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new FakeBizOpResponse()
            {
                Amount = request.Amount,
                ErrorCode = 0,
                Message = "OK",
                ResultType = ResultTypes.Ok
            };
        }
    }
}
