using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// NHibernate mapping for ChargeNonRecurring entity
    /// </summary>
    public class ChargeNonRecurringMap : SubclassMap<ChargeNonRecurring>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public ChargeNonRecurringMap()
        {
            DiscriminatorValue(1);
        }
    }
}
