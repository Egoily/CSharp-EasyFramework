using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.pipeline.builders;
using NUnit.Framework;

namespace com.etak.core.pipeline.UnitTests.builders
{
    public class RequestOKBase : RequestBase{ }

    public class ResponseOKBase : ResponseBase { } 

    public class AlwaysOKConverter : ITypeConverter<RequestBaseDTO, RequestBaseDTO>, ITypeConverter<ResponseBaseDTO, ResponseBaseDTO>
    {
        public RequestBaseDTO Convert(RequestBaseDTO source)
        {
            return null;
        }

        public ResponseBaseDTO Convert(ResponseBaseDTO source)
        {
            return null;
        }
    }



    public class AlwaysOKValidator : IValidator<RequestBaseDTO>
    {

        public bool Validate(RequestBaseDTO objectToValidate)
        {
            return true;
        }
    }

    public class DefaultMessage<T> : IDefaultResponseGenerator<T> where T : ResponseBaseDTO, new()
    {
        public T GenerateDefaultResponse()
        {
            throw new NotImplementedException();
        }

        public T GenerateDefaultResponse(ElephantTalkBaseException ex)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class QueryAbstractBusinessOperationdBizOp :
        AbstractBusinessOperation
            <RequestBaseDTO, ResponseBaseDTO,
                RequestOKBase, ResponseOKBase>
    {
        public override string OperationDiscriminator
        {
            get { throw new NotImplementedException(); }
        }

        protected override void MapNotAutomappedInboundProperties(RequestBaseDTO dtoRequest, ref RequestOKBase coreInput)
        {
            throw new NotImplementedException();
        }

        protected override void MapNotAutomappedOutboundProperties(ResponseOKBase coreOutput, ref ResponseBaseDTO dtoOutput)
        {
            throw new NotImplementedException();
        }

        protected override ResponseOKBase ProcessBusinessLogic(RequestOKBase request, BusinessOperationExecution runningOperation,
            RequestInvokationEnvironment invoker)
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class PipeLineOperationBuilderUnitTests
    {
        [TestFixtureSetUp]
        public void InitializeTest()
        {
        }

        [Test()]
        public void BuildSimpleMessageProcessor_CorrectScheduleESNSwapGiven_ShouldReturnCorrectOrderId()
        {

            PipeLineOperationBuilder.BuildSimpleMessageProcessor<RequestBaseDTO, ResponseBaseDTO, RequestBaseDTO, ResponseBaseDTO>()
               .FrontEndValiadtor<AlwaysOKValidator>()
               .InboundConverter<AlwaysOKConverter>()
               .CoreOperation<QueryAbstractBusinessOperationdBizOp>()
               .OutboundConverter<AlwaysOKConverter>()
               .DefaultReponseGenerator<DefaultMessage<ResponseBaseDTO>>()
               .UseSessionAuthoritzation()
               .Build();

            PipeLineOperationBuilder.BuildSimpleMessageProcessor<RequestBaseDTO, ResponseBaseDTO, RequestBaseDTO, ResponseBaseDTO>()
               .FrontEndValiadtor<AlwaysOKValidator>()
               .InboundConverter<AlwaysOKConverter>()
               .CoreOperation<QueryAbstractBusinessOperationdBizOp>()
               .OutboundConverter<AlwaysOKConverter>()
               .DefaultReponseGenerator<DefaultMessage<ResponseBaseDTO>>()
               .Build();
        }
    }
}
