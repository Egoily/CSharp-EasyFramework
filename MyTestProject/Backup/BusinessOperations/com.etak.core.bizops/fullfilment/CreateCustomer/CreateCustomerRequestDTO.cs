using System;
using com.etak.core.model.dto;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.CreateCustomer
{
    /// <summary>
    /// The Request DTO for Create Customer
    /// </summary>
    public class CreateCustomerRequestDTO : OrderRequestDTO
    {
        /// <summary>
        /// The DTO Object that contains all the information needed to create the CustomerInfo
        /// </summary>
        public CustomerDTO CustomerToBeCreatedDto { get; set; }

        /// <summary>
        /// The LanguageId to be setted
        /// </summary>
        public Int32? LanguageId { get; set; }

        /// <summary>
        /// Registration Type
        /// </summary>
        public int? RegistrationType { get; set; }

        /// <summary>
        /// Payment Type to be assigned to the Customer
        /// </summary>
        public int? PaymentType { get; set; }

        /// <summary>
        /// Determine the Billing Date of the customer. If it isn't set, the billing date will be calculated in function of creating date.
        /// </summary>
        public int? BillingDate { get; set; }

        /// <summary>
        /// External Reference linked with SAP
        /// </summary>
        public string ExternalCustomerID { get; set; }

        /// <summary>
        /// Which type of Customer are we setting up (Private, Business, etc.)
        /// </summary>
        public int? BusinessType { get; set; }

        /// <summary>
        /// Which will be the Pending Status
        /// </summary>
        public int? PendingStatus { get; set; }

        /// <summary>
        /// Invoice Details (boolean)
        /// </summary>
        public bool? InvoiceDetails { get; set; }
        /// <summary>
        /// Invoice Due Date
        /// </summary>
        public int? InvoiceDueDate { get; set; }
        /// <summary>
        /// Parent billing (boolean)
        /// </summary>
        public bool ParentBilling { get; set; }
        /// <summary>
        /// WithDrawPeriod
        /// </summary>
        public int WithDrawPeriod { get; set; }
    }
}
