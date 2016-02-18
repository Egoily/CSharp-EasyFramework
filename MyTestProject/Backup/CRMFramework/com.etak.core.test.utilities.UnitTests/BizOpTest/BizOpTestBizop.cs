using System.Linq;
using com.etak.core.model.operation;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.test.utilities.UnitTests.MicroServiceTest;

namespace com.etak.core.test.utilities.UnitTests.BizOpTest
{
    public class BizOpTestBizop : AbstractBusinessOperation<BizOpTestRequestDTO, BizOpTestResponseDTO, BizOpTestRequestInternal, BizOpTestResponseInternal>
    {
        public override string OperationCode
        {
            get { return "TBD"; }
        }

        public override string OperationDiscriminator
        {
            get { return "TBD"; }
        }

        protected override void MapNotAutomappedInboundProperties(BizOpTestRequestDTO dtoRequest, ref BizOpTestRequestInternal coreInput)
        {
            if (coreInput.MSISDN == null)
                throw new BusinessLogicErrorException(string.Format("Cannot get MSISDN."), 100);
        }

        protected override void MapNotAutomappedOutboundProperties(BizOpTestResponseInternal coreOutput, ref BizOpTestResponseDTO dtoOutput)
        {
            dtoOutput.list = coreOutput.list;
        }

        protected override BizOpTestResponseInternal ProcessBusinessLogic(BizOpTestRequestInternal request, BusinessOperationExecution runningOperation,
            RequestInvokationEnvironment invoker)
        {
            var microServiceTest = MicroServiceManager.GetMicroService<MicroserviceTestRequest, MicroserviceTestResponse>();
            var actualRequestMs = new MicroserviceTestRequest()
            {
                MSISDN = request.MSISDN,
            };

            var microServiceResponse = microServiceTest.Process(actualRequestMs, invoker);
            
            var bizOpResponseInternal = new BizOpTestResponseInternal()
            {
                list = microServiceResponse.list.ToList(),
            };

            return bizOpResponseInternal;

        }
    }
}
