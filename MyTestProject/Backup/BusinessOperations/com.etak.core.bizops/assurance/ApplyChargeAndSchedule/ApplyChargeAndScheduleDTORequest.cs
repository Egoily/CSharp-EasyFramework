
using System;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.assurance.ApplyChargeAndSchedule
{
    /// <summary>
    /// DTO Request for ApplyChargeAndSchedule BizOp.
    /// </summary>
    public class ApplyChargeAndScheduleDTORequest : OrderRequestDTO, ICustomerIdBasedDTORequest, IAccountIdBasedDTORequest
    {
        /// <summary>
        /// Charge Id to be charged
        /// </summary>
        public Int32 ChargeId { get; set; }

        /// <summary>
        /// Customer Id corresponding to the customer to be charged
        /// </summary>
        public Int32 CustomerId { get; set; }

        /// <summary>
        /// InvoiceId where the charge will be assigned
        /// </summary>
        public Int64 InvoiceId { get; set; }

        /// <summary>
        /// Account Id where the charge will be applied
        /// </summary>
        public Int64 AccountId { get; set; }

        /// <summary>
        /// Relation between the Customer and the Product
        /// </summary>
        public Int64? CustomerProductAssignmentId { get; set; }

        /// <summary>
        /// Start Date of the Charge
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The Date to take the price of the charge
        /// </summary>
        public DateTime? PriceEffectiveDate { get; set; }

        /// <summary>
        /// Custom Amount in case we don't want to use the one inside the charge
        /// </summary>
        public Decimal? Amount { get; set; }

        /// <summary>
        /// The Schedule Id to obtain the related charge scheduled
        /// </summary>
        public long? ScheduleId { get; set; }

        /// <summary>
        /// The Tax Category to be applied in the charge
        /// </summary>
        public int TaxCategory { get; set; }

        /// <summary>
        /// Type Of Charge that can be applied
        /// </summary>
        public ApplyChargeAndScheduleBizOp.TypeOfCharges TypeOfCharges { get; set; }

        /// <summary>
        /// If true the Amount value is 0, if false Amout is request value
        /// </summary>
        public bool AmountIsInformational { get; set; }

    }
}
