using com.etak.core.operation.contract;

namespace com.etak.core.bizops.fullfilment.CreateCustomer
{
    /// <summary>
    /// Configuration with the settings requierd to perfrom CreateCustomer
    /// </summary>
    public class CreateCustomerConfiguration : BasicOperationConfiguration
    {
        /// <summary>
        /// Billing date of customer
        /// </summary>
        public int? BillCycleId { get; set; }
        /// <summary>
        /// Business type of customer
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// Invoice detail of customer
        /// </summary>
        public bool InvoiceDetails { get; set; }
        /// <summary>
        /// Invoice due date of customer
        /// </summary>
        public int InvoiceDueDate { get; set; }
        /// <summary>
        /// Id of language that will use by cutsomer
        /// </summary>
        public int LanguageID { get; set; }
        /// <summary>
        /// PaymentType of customer
        /// </summary>
        public int PaymentType { get; set; }
        /// <summary>
        /// Registration type of customer
        /// </summary>
        public int RegistrationType { get; set; }
        /// <summary>
        /// Indicator for pending status
        /// </summary>
        public int PendingStatus { get; set; }
        /// <summary>
        /// OrgIg value on MappingInfo
        /// </summary>
        public int MappingInfoOrgId { get; set; }
        /// <summary>
        /// OldIg value on MappingInfo
        /// </summary>
        public int MappingInfoOldId { get; set; }
        /// <summary>
        /// Default value for status on MappingInfo
        /// </summary>
        public bool MappingInfoStatus { get; set; }

    }
}
