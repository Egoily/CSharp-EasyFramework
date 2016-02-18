using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.automapping.subscription;
using com.etak.core.operation.IntTests.operations.messages;

namespace com.etak.core.operation.IntTests.automapping.customer
{
    public class ParameterErrorBusinessOperation : AbstractBusinessOperation<MsisdnBasedRequestDTO, ResponseBaseDTO, CustomerBasedRequest, CustomerBasedResponse>
    {
        public override string OperationCode
        {
            get { return "PEBOC"; }
        }

        public override string OperationDiscriminator
        {
            get { return "PEBOD"; }
        }

        protected override void MapNotAutomappedInboundProperties(MsisdnBasedRequestDTO dtoRequest, ref CustomerBasedRequest coreInput)
        {
        }

        protected override void MapNotAutomappedOutboundProperties(CustomerBasedResponse coreOutput, ref ResponseBaseDTO dtoOutput)
        {
        }

        protected override CustomerBasedResponse ProcessBusinessLogic(CustomerBasedRequest request, model.operation.BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new CustomerBasedResponse
            {
                Customer = request.Customer,
                ErrorCode = 0,
                ResultType = ResultTypes.Ok,
                Message = "Fake OP OK",
            };
        }
    }
}
