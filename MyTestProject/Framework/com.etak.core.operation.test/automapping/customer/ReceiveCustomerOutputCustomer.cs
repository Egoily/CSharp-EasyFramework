using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;

namespace com.etak.core.operation.test.automapping.customer
{
    public class ReceiveCustomerOutputCustomer<TRequest, TResponse> : AbstractBusinessOperation<TRequest, TResponse, CustomerBasedRequest, CustomerBasedResponse> 
        where TRequest : RequestBaseDTO, new() 
        where TResponse : ResponseBaseDTO, new()
    {
        public override string OperationCode
        {
            get { return "RCOC"; }
        }

        public override string OperationDiscriminator
        {
            get { return "TRCOC"; }
        }

        protected override void MapNotAutomappedInboundProperties(TRequest dtoRequest, ref CustomerBasedRequest coreInput)
        {
            
        }

        protected override void MapNotAutomappedOutboundProperties(CustomerBasedResponse coreOutput, ref TResponse dtoOutput)
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
