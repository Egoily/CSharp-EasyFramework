using System;
using com.etak.core.operation.contract;

namespace com.etak.core.bizops.fullfilment.PurchaseProductForCustomer
{
    /// <summary>
    /// COnfiguration for PurchaseProductForCustomer
    /// </summary>
    public class PurchaseProductForCustomerConfiguration: BasicOperationConfiguration
    {
        /// <summary>
        /// First day of the week for nextBillrunDate calculations
        /// </summary>
        public DayOfWeek FirstDayOfWeek { get; set; }
        /// <summary>
        /// Config for Limit Promotion value
        /// </summary>
        public String CategoryMVNOConfigForPromotionLimit { get; set; }

        /// <summary>
        /// the config is used for some unlimited promotions while purchasing product
        /// that means those promotions won't be counted into the accumulation
        /// </summary>
        public String UnlimitedPromotionForPurchaseProduct { get; set; }

        /// <summary>
        /// URL for the call the the API for prorate charges
        /// </summary>
        public String UrlAPIApplyCharges { get; set; }
    }
}
