using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.network
{
    /// <summary>
    /// Fluent nhibernate mapping for RMOperatorsInfo
    /// </summary>
    public class RMOperatorsInfoClassMap : ClassMap<RMOperatorsInfo>
    {
        /// <summary>
        /// COnstructor for fluent nhibernate so it can generate the XML on runtime.
        /// </summary>
        public RMOperatorsInfoClassMap()
        {
            Table("RM_OPERATORS");
            DynamicInsert();
            DynamicUpdate();

            Cache.NonStrictReadWrite().Region(CacheRegions.SystemSettingsCacheRegion);
            Id(x => x.OperatorCode, "OPERATORCODE").GeneratedBy.Assigned().UnsavedValue(0).Length(10);
            Map(x => x.OperaName, "OPERANAME").Length(200);
            Map(x => x.CountryName, "COUNTRYNAME").Length(200);
            Map(x => x.NetworkOperator, "NetworkOperator").Length(10);
            Map(x => x.IndividualQuota, "IndividualQuota");
            Map(x => x.MultQuota, "MultQuota");
            Map(x => x.ChangeWindowQuota, "ChangeWindowQuota");
            Map(x => x.Status, "Status").Length(10);
            Map(x => x.StatusDate, "StatusDate");
            Map(x => x.CNOperatorCode, "CNOperatorCode");
        }
    }
}
