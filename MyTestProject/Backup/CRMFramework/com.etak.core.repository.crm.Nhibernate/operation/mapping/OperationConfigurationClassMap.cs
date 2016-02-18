using com.etak.core.model.operation;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.operation.mapping
{
    /// <summary>
    /// Nhibernate fluent mapping configuration for OperationConfiguration entity
    /// </summary>
    public class OperationConfigurationClassMap : ClassMap<OperationConfiguration>
    {
        /// <summary>
        /// Default constructor so fluent mapping builds the hmb.xml on runtime
        /// </summary>
        public OperationConfigurationClassMap()
        {
            Table("CRM_OPERATION_CONFIGURATIONS");
            DynamicInsert();
            DynamicUpdate();
            Cache.NonStrictReadWrite().Region(CacheRegions.SystemSettingsCacheRegion);

            Id(x => x.Id).GeneratedBy.Custom<PrefixIdGenerator>().Column("OPERATION_CONFIGURATIONID");
            References(x => x.MVNO, "DEALERID").
                ForeignKey("FK_CONFIG_DEALER");

            References(x => x.Operation, "OPERATIONID").
               ForeignKey("FK_OPERATIONID");

            Map(x => x.StarTime, "STARTDATE");
            Map(x => x.EndDate, "ENDDATE");
            //Map(x => x.OperationDiscriminator, "OPERATION_DISCRIMINATOR");
            //Map(x => x.OperationCode, "OPERATION_CODE");
            Map(x => x.JSonConfig, "JSON_CONFIGURATION").CustomType("StringClob").CustomSqlType("VARCHAR(MAX)");
        }
    }
}
