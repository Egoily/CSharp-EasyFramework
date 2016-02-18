using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;

namespace com.etak.core.operation.IntTests.automapping.sim
{
    class SimCardRequestSimCardResponse<TRequestDTO, TResponseDTO>
        : AbstractBusinessOperation<TRequestDTO, TResponseDTO, SimCardBasedRequest, SimCardBasedResponse>
        where TRequestDTO : RequestBaseDTO, new()
        where TResponseDTO : ResponseBaseDTO, new()
    {

        public override string OperationCode
        {
            get { return "SRSR"; }
        }

        public override string OperationDiscriminator
        {
            get { return "SRSRD"; }
        }

        protected override void MapNotAutomappedInboundProperties(TRequestDTO dtoRequest, ref SimCardBasedRequest coreInput)
        {
           
        }

        protected override void MapNotAutomappedOutboundProperties(SimCardBasedResponse coreOutput, ref TResponseDTO dtoOutput)
        {
           
        }

        protected override SimCardBasedResponse ProcessBusinessLogic(SimCardBasedRequest request, model.operation.BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new SimCardBasedResponse
            {
                ErrorCode = 0,
                Message = "FakeOperationOK",
                SimCard = request.SimCard,
                ResultType = ResultTypes.Ok
            };
        }
    }
}
