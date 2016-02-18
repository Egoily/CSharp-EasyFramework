using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;

namespace com.etak.core.operation.IntTests.automapping.subscription
{
    public class SubscriptionResponseSubscriptionOperation<TRequestDTO, TResponseDTO> 
        : AbstractBusinessOperation<TRequestDTO, TResponseDTO, SubscriptioBasedRequest, SubscriptionBasedResponse> 
        where TRequestDTO : RequestBaseDTO, new() where TResponseDTO : ResponseBaseDTO, new()
    {

        public override string OperationCode
        {
            get { return "RNRN"; }
        }

        public override string OperationDiscriminator
        {
            get { return "RNRND"; }
        }

        protected override void MapNotAutomappedInboundProperties(TRequestDTO dtoRequest, ref SubscriptioBasedRequest coreInput)
        {
           
        }

        protected override void MapNotAutomappedOutboundProperties(SubscriptionBasedResponse coreOutput, ref TResponseDTO dtoOutput)
        {
           
        }

        protected override SubscriptionBasedResponse ProcessBusinessLogic(SubscriptioBasedRequest request, model.operation.BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new SubscriptionBasedResponse
            {
                ErrorCode = 0,
                Message = "FakeOperationOK",
                Subscription = request.Subscription,
                ResultType = ResultTypes.Ok
            };
        }
    }
}
