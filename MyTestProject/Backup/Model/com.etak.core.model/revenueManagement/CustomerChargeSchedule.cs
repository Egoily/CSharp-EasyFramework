﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    public enum ScheduleChargeStatus
    {
        InProcess = 0,
        Finished = 1,
        Disabled = 4,
    }

    [DataContract]
    [Serializable]
    public class CustomerChargeSchedule
    {
        /// <summary>
        /// Unique Id of the CustomerChargeSchedule
        /// </summary>
        public virtual Int64 Id { get; set; }

        /// <summary>
        /// The charge in the catalog that is being charged for this customer
        /// </summary>
        public virtual Charge ChargeDefinition { get; set; }

        /// <summary>
        /// The Customer that owns the product that will be charged
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// The account that will be charged with the amount of this charge
        /// </summary>
        public virtual Account ChargedAccount { get; set; }

        /// <summary>
        /// The Purchase that generated this charge
        /// </summary>
        public virtual CustomerProductAssignment Purchase { get; set; }

        /// <summary>
        /// The next date that charging needs to occur for that charge and that customer.
        /// </summary>
        public virtual DateTime? NextChargeDate { get; set; }

        /// <summary>
        /// The number of the period that has will be charged at nextchargedate.
        /// </summary>
        public virtual Int32 NextPeriodNumber { get; set; }

        /// <summary>
        /// The number of the cycle that will be charged at the nextchargedate
        /// </summary>
        public virtual Int32 CurrentCyclenumber { get; set; }

        /// <summary>
        /// When the agreement with the customer is to use the price set at a given date to charge him, the date for which the price needs to be used.
        /// </summary>
        public virtual DateTime ? PriceEffectiveDate { get; set; }

        /// <summary>
        /// When the schedule entity was created.
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// When the shedule was modified for the last time
        /// </summary>
        public virtual DateTime? UpdateDate { get; set; }

        /// <summary>
        /// The list of charges generated by this schedule
        /// </summary>
        public virtual IList<CustomerCharge> Charges { get; set; }

        /// <summary>
        /// Current status of the charge
        /// </summary>
        public virtual ScheduleChargeStatus Status { get; set; }
    }
}