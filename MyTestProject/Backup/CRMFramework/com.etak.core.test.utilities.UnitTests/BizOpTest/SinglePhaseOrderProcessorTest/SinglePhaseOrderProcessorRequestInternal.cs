using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.numberInfo;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.test.utilities.UnitTests.BizOpTest.SinglePhaseOrderProcessorTest
{
    public class SinglePhaseOrderProcessorRequestInternal : CreateNewOrderRequest, INumberInfoBasedRequest, ICustomerBasedRequest, IMultiCustomerRequestBased, ISubscriptionLastActiveBasedRequest, ISimCardBasedRequest, IJointCustomerBasedRequest, IJointSubscriptionBasedRequest, IAccountBasedRequest, IMultiSubscriptionBasedRequest
    {
        public NumberInfo NumberInPool { get; set; }
        public CustomerInfo Customer { get; set; }
        public IEnumerable<CustomerInfo> Customers { get; set; }
        public ResourceMBInfo Subscription { get; set; }
        public string MSISDN { get; set; }
        public SIMCardInfo SimCard { get; set; }
        public CustomerInfo SourceCustomerInfo { get; set; }
        public CustomerInfo DestinationCustomerInfo { get; set; }
        public ResourceMBInfo SourceSubscription { get; set; }
        public string SourceMSISDN { get; set; }
        public ResourceMBInfo DestinationSubscription { get; set; }
        public string DestinationMSISDN { get; set; }
        public Account Account { get; set; }
        public IEnumerable<ResourceMBInfo> Subscriptions { get; set; }
    }
}
