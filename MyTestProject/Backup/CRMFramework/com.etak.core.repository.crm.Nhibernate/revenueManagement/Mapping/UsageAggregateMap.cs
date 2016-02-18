using com.etak.core.model.revenueManagement;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// NHibernate mapping for ChargeNonRecurring entity
    /// </summary>
    public class UsageAggregateMap : SubclassMap<UsageAggregate>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public UsageAggregateMap()
        {
            DiscriminatorValue(5);
        }
    }
}
