using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;

namespace com.etak.core.operation.test.common
{
    public class AlwaysOkBusinessOperation<TDTOInput,TDTOOutput,TInternalInput,TInternalOutput> :
        AbstractBusinessOperation<TDTOInput, TDTOOutput, TInternalInput, TInternalOutput>
         where TDTOInput : RequestBaseDTO, new()
        where TDTOOutput : ResponseBaseDTO, new()
        where TInternalInput : RequestBase, new()
        where TInternalOutput : ResponseBase, new()
    {
        public override string OperationCode
        {
            get { return "UTST"; }
        }

        public override string OperationDiscriminator
        {
            get { return "AOBO"; }
        }

        protected override void MapNotAutomappedInboundProperties(TDTOInput dtoRequest, ref TInternalInput coreInput)
        {
           
        }

        protected override void MapNotAutomappedOutboundProperties(TInternalOutput coreOutput, ref TDTOOutput dtoOutput)
        {
           
        }

        protected override TInternalOutput ProcessBusinessLogic(TInternalInput request, model.operation.BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new TInternalOutput()
            {
                ErrorCode = 0,
                Message = "OK",
                ResultType = ResultTypes.Ok
            };
        }
    }
}
