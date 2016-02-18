using System;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.assurance.ApplyChargeAndSchedule
{
    /// <summary>
    /// Input parameters for ApplyChargeAndSchedule
    /// </summary>
    public class ApplyChargeAndScheduleRequest : CreateNewOrderRequest, ICustomerBasedRequest, IAccountBasedRequest
    {
        /// <summary>
        /// The charge (at catalog leve) that needs to be applied
        /// </summary>
        public virtual Charge ChargeToAdd { get; set; }

        /// <summary>
        /// The customer that is affected by the operation that crates this charge
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// The invoice in which this charge needs to be applied
        /// </summary>
        public virtual Invoice InvoiceOfCharge { get; set; }

        /// <summary>
        /// The account where the charge will be applied
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// The relation between the customer and the product purchased
        /// </summary>
        public virtual CustomerProductAssignment CustomerProductAssignment { get; set; }

        /// <summary>
        /// Determines the Date to calculate the price of the charge (for recurrent charges)
        /// </summary>
        public virtual DateTime? PriceEffectiveDate { get; set; }

        /// <summary>
        /// Start Date when the charge will be applicable (for recurrent charges)
        /// </summary>
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// CycleNumber if applies to create the Scheduled Charge (for recurrent charges)
        /// </summary>
        public virtual long? CycleNumber { get; set; }
        
        /// <summary>
        /// Charge Schedule to be associated to the charge (Non Recurring)
        /// </summary>
        public virtual CustomerChargeSchedule Schedule { get; set; }

        /// <summary>
        /// The tax definition that corresponds to the customer
        /// </summary>
        public virtual TaxDefinition TaxDefinition { get; set; }

        /// <summary>
        /// If CustomAmount is set, the amount applied to the charge corresponds to this
        /// CustomAmount instead of the Charge's amount.
        /// </summary>
        public virtual decimal? CustomAmount { get; set; }

        /// <summary>
        /// The DealerInfo that corresponds to the Customer
        /// </summary>
        public virtual DealerInfo CustomerDealer { get; set; }
        /// <summary>
        /// Determines the type of charges to be applied
        /// </summary>
        public virtual ApplyChargeAndScheduleBizOp.TypeOfCharges TypeOfCharges { get; set; }

        /// <summary>
        /// If true the Amount value is 0, if false Amout is request value
        /// </summary>
        public bool AmountIsInformational { get; set; }
    }
}
