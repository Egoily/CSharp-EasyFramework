using com.etak.core.model.provisioning;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.provisioning.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping for class Carrier
    /// </summary>
    public class CarrierMap : ClassMap<Carrier>
    {
        //CREATE TABLE [dbo].[RM_CARRIER]
        //(
        //[CARRIERID] [int] NOT NULL,
        //[CARRIERCODE] [varchar] (20) COLLATE Latin1_General_CI_AS NOT NULL,
        //[DESCRIPTION] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL
        //) ON [PRIMARY]
        /// <summary>
        /// Public construsctor for Fluent Nhibernate mapping
        /// </summary>
        public CarrierMap()
        {
            Schema("dbo");
            Table("RM_CARRIER");
            DynamicUpdate();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            Id(x => x.Id, "CARRIERID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.Code, "CARRIERCODE");
            Map(x => x.Description, "DESCRIPTION");
        }
    }
}
