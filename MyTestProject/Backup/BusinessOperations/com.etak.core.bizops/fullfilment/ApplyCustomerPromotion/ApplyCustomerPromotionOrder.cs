using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.ApplyCustomerPromotion
{
    /// <summary>
    /// Apply Customer Promotion Order
    /// </summary>
    public class ApplyCustomerPromotionOrder : Order
    {
        /// <summary>
        /// Discriminator for Apply Customer Promotion
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.ApplyCustomerPromotionOrder; }
        }
    }
}
