using com.etak.core.model.revenueManagement;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// NHibernate mapping for ChargeNonRecurring entity
    /// </summary>
    public class ChargeAggregateMap : SubclassMap<ChargeAggregate>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public ChargeAggregateMap()
        {
            DiscriminatorValue(6);
        }
    }
}
