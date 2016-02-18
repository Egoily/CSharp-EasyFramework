using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.operations;
using com.etak.core.operation.IntTests.operations.messages;
using com.etak.core.test.utilities;

namespace com.etak.core.operation.IntTests.common
{
    public class AlwaysOkSinglePhaseOrderCustomerBasedProcessor : AbstractSinglePhaseOrderProcessor<FakeOrderRequestDTO, FakeOrderResponseDTO, FakeOrderNoCustomerbasedRequest, FakeOrderCustomerbasedResponse, FakeOrder>
    {
        public override string OperationCode
        {
            get { return "UTS2"; }
        }

        public override string OperationDiscriminator
        {
            get { return "AOK2"; }
        }

        public override FakeOrderCustomerbasedResponse ProcessRequest(FakeOrder order, FakeOrderNoCustomerbasedRequest request)
        {
            return new FakeOrderCustomerbasedResponse()
            {
                Customer = CreateDefaultObject.Create<CustomerInfo>()
            };

        }

        protected override void MapNotAutomappedOrderInboundProperties(FakeOrderRequestDTO request, ref FakeOrderNoCustomerbasedRequest coreInput)
        {

        }

        protected override void MapNotAutomappedOrderOutboundProperties(FakeOrderCustomerbasedResponse source, ref FakeOrderResponseDTO DTOOutput)
        {

        }
    }
}
