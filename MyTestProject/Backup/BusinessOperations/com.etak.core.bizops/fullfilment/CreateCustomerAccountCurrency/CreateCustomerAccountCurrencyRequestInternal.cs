using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.fullfilment.CreateCustomerAccountCurrency
{
    /// <summary>
    /// Request internal of CreateCustomerAccountCurrency
    /// </summary>
    public class CreateCustomerAccountCurrencyRequestInternal : CreateNewOrderRequest
    {
        /// <summary>
        /// CustomerInfo that Account Currency to be created
        /// </summary>
        public CustomerInfo CustomerInfo { get; set; }

        /// <summary>
        /// BillCycle for Customer
        /// </summary>
        public BillCycle BillCycleForCustomer { get; set; }
    }
}
