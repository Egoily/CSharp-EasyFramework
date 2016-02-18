using com.etak.core.model.revenueManagement;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// NHibernate mapping for ChargeDiscount entity
    /// </summary>
    public class ChargeDiscountMap : SubclassMap<ChargeDiscount>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public ChargeDiscountMap()
        {
            DiscriminatorValue(4);
            Map(x => x.AdjustmentType).CustomType<AdjustmentType>().Column("ADJUSTMENTTYPEID");
            Map(x => x.AdjustmentCalculationType).CustomType<AdjustmentCalculationType>().Column("ADJUSTMENT_UNITID");
            Map(x => x.AdjustmentQuantity).Column("ADJUSTMENT_QUANTITY");
        }
    }
}
