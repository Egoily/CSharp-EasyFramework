using System;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Fluent Nhibernate mapping class for Product
    /// </summary>
    public class ProductMap : ClassMap<Product>
    {
        //CREATE TABLE RM_PRODUCTS
        //( 
        //    PRODUCTGK            int  NOT NULL ,
        //    PRODUCTTYPEID        int  NULL ,
        //    NAMEID               int  NULL ,
        //    DESCRIPTIONID        int  NULL ,
        //    MVNOID               integer  NULL,
        //    PACKAGEID			 int  NULL,
        //    BUNDLEID			 int  NULL,
        //    PROMOTIONPLANID		 int  NULL,
        //    PROMOTIONGROUPID	 int  NULL
        //)


        //        CREATE TABLE RM_PRODUCT_GROUPS
        //( 
        //    PRODUCTGK            int  NOT NULL ,
        //    CHILD_PRODUCTGK      int  NOT NULL 
        //)

        /// <summary>
        /// public constructor for fluent Nhibernate
        /// </summary>
        public ProductMap()
        {
            Schema("dbo");
            Table("RM_PRODUCTS");
            DynamicUpdate();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();
            DiscriminateSubClassesOnColumn<Int32>("DISCRIMINATOR", 0).Not.Nullable();

            Id(x => x.Id, "PRODUCTGK").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.Status, "STATUS");
            Map(x => x.ExternalReference, "EXTERNAL_REFERENCE");
            References(x => x.Type).Column("PRODUCTTYPEID").Cascade.All();
            References(x => x.Names, "NAMEID").Cascade.All();
            References(x => x.Description, "DESCRIPTIONID").Cascade.All();
            References(x => x.VMO).Column("MVNOID");
            References(x => x.AssociatedPackage).Column("PACKAGEID");
            References(x => x.AssociatedBundle).Column("BUNDLEID");
            References(x => x.AssociatedPrmotionGroup).Column("PROMOTIONGROUPID");
            References(x => x.AssociatedPrmotionPlan).Column("PROMOTIONPLANID");
            References(x => x.Carrier).Column("CARRIERID");

            HasMany(x => x.ProductRelationDependencies)
                .KeyColumn("PRODUCTGK")
                .Inverse()
                .Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

        }
    }
}