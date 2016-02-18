using System;

namespace com.etak.core.model.revenueManagement
{
    [Serializable]
    public class CustomerCharge
    {
        /// <summary>
        /// Unique Id of the customer charge
        /// </summary>
        public virtual Int64 Id { get; set; }

        /// <summary>
        /// Customer that who owns the product being charged
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// the account that has been used to apply the charge to the customer
        /// </summary>
        public virtual Account ChargingAccount { get; set; }

        /// <summary>
        /// The purchase of a product that generated this charge
        /// </summary>
        public virtual CustomerProductAssignment Product { get; set; }

        /// <summary>
        /// The shedule that generated this charge in case of recurring charges.
        /// </summary>
        public virtual CustomerChargeSchedule Schedule { get; set; }
        
        /// <summary>
        /// The charge definition that has been applied
        /// </summary>
        public virtual Charge ChargeDefinition { get; set; }

        /// <summary>
        /// The invoice that to which charge belongs
        /// </summary>
        public virtual Invoice Invoice { get; set; }

        /// <summary>
        /// The currency that is used in field amount
        /// </summary>
        public virtual ISO4217CurrencyCodes Currency { get; set; }

        /// <summary>
        /// The amount/value of currency for this charge
        /// </summary>
        public virtual Decimal Amount { get; set; }

        /// <summary>
        /// The amount/value of currency for this charge that needs to be displayed
        /// </summary>
        public virtual Decimal ? InformationalAmount { get; set; }

        /// <summary>
        /// The date in which charge is applied
        /// </summary>
        public virtual DateTime ChargingDate { get; set; }

        /// <summary>
        /// The number of the period the charge was applied for, for recurring charges. The period number is the period number within the cycle.
        /// </summary>
        public virtual Int64 ? PeriodNumber { get; set; }

        /// <summary>
        /// The number of the period the charge was applied for, for recurring charges. The period number is the period number within the cycle.
        /// </summary>
        public virtual Int64 ? CycleNumber { get; set; }

        /// <summary>
        /// a free format field for potential comments.
        /// </summary>
        public virtual String Comments { get; set; }

        /// <summary>
        /// An external reference for the charge, if any
        /// </summary>
        public virtual String ExternalReferenceCode { get; set; }

        /// <summary>
        /// The definition of the tax types that apply to this charge
        /// </summary>
        public virtual TaxDefinition Tax { get; set; }
        public virtual Decimal? TaxAmount { get; set; }


    }
}