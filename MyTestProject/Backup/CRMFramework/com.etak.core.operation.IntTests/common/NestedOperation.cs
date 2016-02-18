using System;
using System.Collections.Generic;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.operations.messages;

namespace com.etak.core.operation.IntTests.common
{
    public class NestedOperation:
        AbstractBusinessOperation<FakeBizOpRequestDTO, FakeBizOpResponseDTO, FakeBizOpRequest, FakeBizOpResponse>
      
    {
        static Stack<Object> invokationStack = new Stack<Object>();

        public override string OperationCode
        {
            get { return "NEST"; }
        }

        public override string OperationDiscriminator
        {
            get { return "NEST"; }
        }

        protected override void MapNotAutomappedInboundProperties(FakeBizOpRequestDTO dtoRequest, ref FakeBizOpRequest coreInput)
        {

        }

        protected override void MapNotAutomappedOutboundProperties(FakeBizOpResponse coreOutput, ref FakeBizOpResponseDTO dtoOutput)
        {

        }

        protected override FakeBizOpResponse ProcessBusinessLogic(FakeBizOpRequest request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            if (invokationStack.Count == 0)
            {
                NestedOperation oneLevelMore = new NestedOperation();
                invokationStack.Push(new Object());
                oneLevelMore.Process(request, invoker);
                invokationStack.Pop();
            }

            return new FakeBizOpResponse {ErrorCode = 0, Message = String.Empty, ResultType = ResultTypes.Ok};
        }
    }
}
