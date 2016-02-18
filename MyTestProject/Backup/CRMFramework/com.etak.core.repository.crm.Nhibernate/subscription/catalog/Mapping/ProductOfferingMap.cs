using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.subscription.catalog;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.subscription.catalog.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for ProductOffering
    /// </summary>
    public class ProductOfferingMap : ClassMap<ProductOffering>
    {
        
        /// <summary>
        /// public constructor for fluent Nhibernate
        /// </summary>
        public ProductOfferingMap()
        {
            Schema("dbo");
            Table("RM_PRODUCT_OFFERINGS");
            DynamicUpdate();
            DynamicInsert();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            Id(x => x.Id, "PRODUCT_OFFERINGID").GeneratedBy.Custom<PrefixIdGenerator>();

            References(x => x.OfferedProduct).Column("PRODUCTGK").ForeignKey("FK_PO_PRODUCTGK").Not.Nullable();

            HasMany(x => x.Options).
                KeyColumn("PRODUCT_OFFERINGID")
                .Inverse()
                .Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            HasManyToMany(x => x.OfferingChildTemplates).
                Table("RM_PRODUCT_OFFERING_TEMPLATES")
                //TODO ADD COMPOSITE PRIMARY KEY ??
                .ParentKeyColumn("PRODUCT_OFFERINGID")
                .ChildKeyColumn("CHILD_PRODUCT_OFFERINGID")
                .ForeignKeyConstraintNames("FK_POT_PRODUCT_OFFERINGID", "FK_POT_CHILD_PRODUCT_OFFERINGID")
                .Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            HasManyToMany(x => x.OfferingParentTemplates).
                Table("RM_PRODUCT_OFFERING_TEMPLATES")
                .ParentKeyColumn("CHILD_PRODUCT_OFFERINGID")
                .ChildKeyColumn("PRODUCT_OFFERINGID")
                .Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            References(x => x.Names, "NAMEID").Cascade.All();
            References(x => x.Description, "DESCRIPTIONID").Cascade.All();
            Map(x => x.IsProductOption, "ISPRODUCT_OPTION");


            References(x => x.Group).Column("PRODUCT_OFFERING_GROUPID").ForeignKey("FK_PO_PRODUCT_OFFERING_GROUPID");
           
            HasMany(x => x.ChargingOptions).
                KeyColumn("PRODUCT_OFFERINGID")
                .Inverse()
                .ForeignKeyConstraintName("FK_PCO_PRODUCT_OFFERINGID")
                .Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            HasMany(x => x.ProductOfferingTimeRanges).
                KeyColumn("PRODUCT_OFFERINGID")
                .Inverse()
                //.Cascade.SaveUpdate() //-- Remove just to prevent misuse of the entity 
                .Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();
            
        }
    }
}
