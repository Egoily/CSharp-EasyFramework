using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.customer
{
    public class JointCustomerBasedRequest : RequestBase, IJointCustomerBasedRequest
    {
        public model.CustomerInfo DestinationCustomerInfo { get; set; }

        public model.CustomerInfo SourceCustomerInfo { get; set; }
    }
}
