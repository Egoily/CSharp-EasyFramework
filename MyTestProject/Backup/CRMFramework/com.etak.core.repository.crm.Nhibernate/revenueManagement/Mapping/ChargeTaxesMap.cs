using com.etak.core.model.revenueManagement;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// NHibernate mapping for ChargeNonRecurring entity
    /// </summary>
    public class ChargeTaxesMap : SubclassMap<ChargeTaxes>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public ChargeTaxesMap()
        {
            DiscriminatorValue(7);
        }
    }
}
