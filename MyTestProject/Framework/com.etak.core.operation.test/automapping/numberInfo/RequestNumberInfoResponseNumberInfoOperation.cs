
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;

namespace com.etak.core.operation.test.automapping.numberInfo
{
    public class RequestNumberInfoResponseNumberInfoOperation<TRequestDTO, TResponseDTO> 
        : AbstractBusinessOperation<TRequestDTO, TResponseDTO, NumberInfoBasedRequest, NumberInfoBasedResponse> where TRequestDTO : RequestBaseDTO, new() where TResponseDTO : ResponseBaseDTO, new()
    {

        public override string OperationCode
        {
            get { return "RNRN"; }
        }

        public override string OperationDiscriminator
        {
            get { return "RNRND"; }
        }

        protected override void MapNotAutomappedInboundProperties(TRequestDTO dtoRequest, ref NumberInfoBasedRequest coreInput)
        {
        }

        protected override void MapNotAutomappedOutboundProperties(NumberInfoBasedResponse coreOutput, ref TResponseDTO dtoOutput)
        {
        }

        protected override NumberInfoBasedResponse ProcessBusinessLogic(NumberInfoBasedRequest request, model.operation.BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new NumberInfoBasedResponse
            {
                ErrorCode = 0,
                Message = "FakeOperationOK",
                NumberInPool = request.NumberInPool,
                ResultType = ResultTypes.Ok
            };
        }
    }
}
