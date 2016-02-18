using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    [Serializable]
    public class ChargePrice
    {
        /// <summary>
        /// The composite key of the ChargePrice
        /// </summary>
        public virtual ChargePriceId Id { get; set; }

        /// <summary>
        /// The composite key of the ChargePrice
        /// </summary>
        public virtual Charge Charge { get; set; }

        /// <summary>
        /// Start date of the period to apply this price.
        /// </summary>
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// End date of the period to apply this price.
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

      
        /// <summary>
        /// Amount to be charged.
        /// </summary>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// currency of amount.
        /// </summary>
        public virtual ISO4217CurrencyCodes Currency { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ChargePrice)) return false;
            return Equals((ChargePrice)obj);
        }

        public virtual bool Equals(ChargePrice other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            if ( Id != null)
                return (Id.GetHashCode() + Id.StartDate.GetHashCode() - Amount.GetHashCode() * 33);

            return (-1);
        }
    }
}