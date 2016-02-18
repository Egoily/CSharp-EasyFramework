using System;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.AddNonRecurringChargeToCustomer
{
    /// <summary>
    /// RequestDTO of AddNonRecurringChargeToCustomer
    /// </summary>
    public class AddNonRecurringChargeToCustomerRequestDTO : OrderRequestDTO, ICustomerIdBasedDTORequest, IAccountIdBasedDTORequest
    {
        /// <summary>
        /// the charge id that will be added 
        /// </summary>
        public Int32 ChargeCatalogId;

        /// <summary>
        /// the customer whom will the charge will be added.
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Optional: the Id of the account that will held the payment, to specify a different  one than the customer account.
        /// </summary>
        public Int64 AccountId { get; set; }
        /// <summary>
        /// the date that the charge will be applied.
        /// </summary>
        public DateTime? ChargeDate;

        /// <summary>
        /// the Amount that the charge will be applied.
        /// </summary>
        public decimal? Amount;


    }
}
