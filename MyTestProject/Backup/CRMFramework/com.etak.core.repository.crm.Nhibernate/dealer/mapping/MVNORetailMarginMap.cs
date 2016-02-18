using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate.dealer.mapping
{
    /// <summary>
    /// Class to map to Nhibernate class MVNORetailMargin
    /// </summary>
    public class MVNORetailMarginMap: ClassMap<MVNORetailMargin>
    {
        /// <summary>
        /// The constructor called by fluent to map the class
        /// </summary>
        public MVNORetailMarginMap()
        {
            Schema("dbo");
            Table("CRM_MVNO_RETAIL_MARGINS");
            DynamicUpdate();
            Cache.Region(CacheRegions.SystemSettingsCacheRegion).NonStrictReadWrite().IncludeAll();
            Id(x => x.Id).Column("MVNO_RETAIL_MARGINID").GeneratedBy.Custom<PrefixIdGenerator>();

            References(x => x.Dealer, "MVNOID");
            References(x => x.Carrier,"CARRIERID");

            Map(x => x.MarginValue, "MARGIN_VALUE");
            Map(x => x.StartDate, "STARTDATE");
            Map(x => x.EndDate, "ENDDATE");
        }
    }
}
