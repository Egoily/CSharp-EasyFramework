using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for ChargeTarget
    /// </summary>
    public class ChargeTargetMap : ClassMap<ChargeTarget>
    {
        //
       //        CREATE TABLE RM_CHARGE_TARGETS
       //( 
       //      TARGETID               INT NOT NULL,
       //      CHARGEID               INT NOT NULL ,
       //      COMPUTATION_ORDER      INT NOT NULL ,
       //      TARGET_CHARGEID        INT NULL ,
       //      TARGET_CHARGE_CATEGORYID INT NULL 
       //)
        /// <summary>
        /// Default constructor so fluent map builds the xml on runtime.
        /// </summary>
        public ChargeTargetMap()
        {
            Table("RM_CHARGE_TARGETS");
            DynamicUpdate();
            DynamicInsert();
            Cache.NonStrictReadWrite().Region(CacheRegions.CatalogCacheRegion).IncludeAll();

            Id(x => x.Id, "TARGETID").GeneratedBy.Custom<PrefixIdGenerator>();

            Map(x => x.ComputationOrder).Column("COMPUTATION_ORDER");
            Map(x => x.TargetCategory).Column("TARGET_CHARGE_CATEGORYID");

            References(x => x.OwnerCharge).Column("CHARGEID");
            References(x => x.TargetCharge).Column("TARGET_CHARGEID");
        }
    }
}
