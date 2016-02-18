using System;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{
    [DataContract]
    [Serializable]
    public class ChargePriceId 
    {
        /// <summary>
        /// The charge that this price refers to.
        /// </summary>
        public virtual Int32 ChargeId { get; set; }

        /// <summary>
        /// Start date of the period to apply this price.
        /// </summary>
        public virtual DateTime  StartDate { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            ChargePriceId cObj = obj as ChargePriceId;
            if (cObj == null)
                return false;

            return cObj.StartDate == StartDate && cObj.ChargeId == ChargeId;
            
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return StartDate.GetHashCode() * 386 + ChargeId.GetHashCode() * 8585;
        }
    }
}