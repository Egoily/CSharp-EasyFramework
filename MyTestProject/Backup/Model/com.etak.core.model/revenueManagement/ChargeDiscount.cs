using System;

namespace com.etak.core.model.revenueManagement
{
    [Serializable]
    public class ChargeDiscount : ChargeNonRecurring
    {
        #region Adjustment/ discounts
        

        /// <summary>
        /// The type of adjustment to perform
        /// </summary>
        public virtual AdjustmentType AdjustmentType { get; set; }

        /// <summary>
        /// the type of calculation used to compute the adjustement-discount
        /// </summary>
        public virtual AdjustmentCalculationType AdjustmentCalculationType { get; set; }

        /// <summary>
        /// the quantity of the adjustment, expressed in the adjustment unit
        /// </summary>
        public virtual Decimal AdjustmentQuantity { get; set; }
        #endregion
    }
}
