using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.CreateCustomerAccountCurrency
{
    /// <summary>
    /// Response Internal of CreateCustomerAccountCurrency
    /// </summary>
    public class CreateCustomerAccountCurrencyResponseInternal : CreateNewOrderResponse,ICustomerBasedResponse,ISubscriptionBasedResponse
    {
        /// <summary>
        /// Customer AccountCurrency
        /// </summary>
        public model.revenueManagement.AccountCurrency CustomerAccount { get; set; }
        /// <summary>
        /// Customer info
        /// </summary>
        public CustomerInfo Customer { get; set; }
        /// <summary>
        /// Customer Subscription
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
