using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.CreateCustomerAccountCurrency
{
    /// <summary>
    /// Response DTO of CreateCustomerAccountCurrency
    /// </summary>
    public class CreateCustomerAccountCurrencyResponseDTO : OrderResponseDTO,ICustomerBasedDTOResponse,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// CustomerDTO
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        /// Customer Subscription
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
