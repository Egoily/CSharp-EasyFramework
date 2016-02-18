using System;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.AddNonRecurringChargeToCustomer
{
    /// <summary>
    /// RequestInternal of AddNonRecurringChargeToCustomer
    /// </summary>
    public class AddNonRecurringChargeToCustomerRequestInternal : CreateNewOrderRequest, ICustomerBasedRequest, IAccountBasedRequest
    {
        /// <summary>
        /// CustomerInfo entity to send data customer
        /// </summary>
        public CustomerInfo Customer { get; set; }
        /// <summary>
        /// Charge entity to send data ChargeInfo
        /// </summary>
        public Charge ChargeInfo { get; set; }
        /// <summary>
        /// Account entity to send data AccountInfo
        /// </summary>
        public Account Account { get; set; }
        /// <summary>
        /// ChargeDate to set for request ApplyChargeAndSchedule
        /// </summary>
        public DateTime ChargeDate { get; set; }
        /// <summary>
        /// Invoice entity to send data InvoiceInfo
        /// </summary>
        public Invoice InvoiceInfo { get; set; }
        /// <summary>
        /// Amount to set CustomAmount in ApplyChargeAndSchedule
        /// </summary>
        public decimal? Amount { get; set; }


        
    }
}
