using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;

namespace com.etak.core.operation.test.common
{
    public class NestedOperation<TDTOInput, TDTOOutput, TInternalInput, TInternalOutput> :
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

        protected override TInternalOutput ProcessBusinessLogic(TInternalInput request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            AlwaysOkBusinessOperation<TDTOInput, TDTOOutput, TInternalInput, TInternalOutput> nestedOp =
                new AlwaysOkBusinessOperation<TDTOInput, TDTOOutput, TInternalInput, TInternalOutput>();
            return nestedOp.Process(request, invoker);
        }
    }
}
