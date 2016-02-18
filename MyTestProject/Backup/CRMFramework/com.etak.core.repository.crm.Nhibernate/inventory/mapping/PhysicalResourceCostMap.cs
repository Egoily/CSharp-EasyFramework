using com.etak.core.model.inventory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate.inventory.mapping
{
    /// <summary>
    /// Class to map to Nhibernate class PhysicalResourceCost
    /// </summary>
    public class PhysicalResourceCostMap: ClassMap<PhysicalResourceCost>
    {
        /// <summary>
        /// public constructor for fluent Nhibernate
        /// </summary>
        public PhysicalResourceCostMap()
        {
            Schema("dbo");
            Table("RM_PRODUCT_SPECIFICATION_COSTS");
            DynamicUpdate();
            Id(x => x.Id, "PRODUCT_COSTID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.StandardCost, "STANDARDCOST").Precision(19).Scale(8);
            Map(x => x.CurrentCost, "CURRENTCOST").Precision(19).Scale(8);
            Map(x => x.StartDate, "STARTDATE");
            Map(x => x.EndDate, "ENDDATE");
            References(x => x.Specification).Column("SPECIFICATIONID").Cascade.None().ForeignKey("FK_PS_SPECIFICATIONID").Not.Nullable();

        }
    }
}
