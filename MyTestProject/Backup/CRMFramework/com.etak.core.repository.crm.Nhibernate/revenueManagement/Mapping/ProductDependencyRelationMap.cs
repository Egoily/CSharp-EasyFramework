using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// Nhibernate fluent mapping class for ProductDependencyRelation entity
    /// </summary>
    public class ProductDependencyRelationMap : ClassMap<ProductDependencyRelation>
    {
        /// <summary>
        /// Constructor so fluent Nhibernate builds the HBM.XML on runtime
        /// </summary>
        public ProductDependencyRelationMap()
        {
            Schema("dbo");
            Table("RM_PRODUCT_RELATIONS");
            DynamicInsert();
            DynamicUpdate();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            Id(x => x.Id, "PRODUCT_RELATIONID").GeneratedBy.Custom<PrefixIdGenerator>();

            References(x => x.SourceProduct, "PRODUCTGK");
            References(x => x.RelatedProduct, "RELATED_PRODUCTGK");
            Map(x => x.MaxOccurs, "MAX_OCCURS");
            Map(x => x.MinOccurs, "MIN_OCCURS");
            Map(x => x.RelationType, "RELATION_TYPE").CustomType<EnumStringType<ProductRelationTypes>>();
            Map(x => x.ConflictResolutionStrategy, "CONFLICT_STRATEGY").CustomType<EnumStringType<ProductConflictResolutionsStrategies>>();
        }
    }
}
