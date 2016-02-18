using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.operation.implementation;

namespace com.etak.core.bizops.Temporal_AnnaM
{
    /// <summary>
    /// CancelCustomerProductRequestBizOp
    /// </summary>
    public class CancelCustomerProductRequestBizOp : AbstractBusinessOperation<CancelCustomerProductRequestDTO,CancelCustomerProductResponseDTO,CancelCustomerProductRequestInternal,CancelCustomerProductResponseInternal>
    {
        /// <summary>
        /// MapNotAutomappedInboundProperties
        /// </summary>
        /// <param name="dtoRequest"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedInboundProperties(CancelCustomerProductRequestDTO dtoRequest, ref CancelCustomerProductRequestInternal coreInput)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// MapNotAutomappedOutboundProperties
        /// </summary>
        /// <param name="coreOutput"></param>
        /// <param name="dtoOutput"></param>
        protected override void MapNotAutomappedOutboundProperties(CancelCustomerProductResponseInternal coreOutput, ref CancelCustomerProductResponseDTO dtoOutput)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// OperationCode for CancelCustomerProductRequestBizOp
        /// </summary>
        public override string OperationCode
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// OperationDiscrimator fro CancelCustomerProductRequestBizOp
        /// </summary>
        public override string OperationDiscriminator
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// ProcessBusinessLogic
        /// </summary>
        /// <param name="request"></param>
        /// <param name="runningOperation"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        protected override CancelCustomerProductResponseInternal ProcessBusinessLogic(CancelCustomerProductRequestInternal request, model.operation.BusinessOperationExecution runningOperation, operation.RequestInvokationEnvironment invoker)
        {
            throw new NotImplementedException();
        }
    }
}
