using com.etak.core.model.inventory;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate.inventory.mapping
{
    /// <summary>
    /// Class to map to Nhibernate class PhysicalResourceSpecification
    /// </summary>
    public class PhysicalResourceSpecificationMap: ClassMap<PhysicalResourceSpecification>
    {
        /// <summary>
        /// public constructor for fluent Nhibernate
        /// </summary>
        public PhysicalResourceSpecificationMap()
        {
            Schema("dbo");
            Table("RM_PRODUCT_SPECIFICATIONS");
            DynamicUpdate();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();
            Id(x => x.Id, "SPECIFICATIONID").GeneratedBy.Custom<PrefixIdGenerator>();

            References(x => x.Name, "NAMEID").Cascade.All();
            References(x => x.Description, "DESCRIPTIONID").Cascade.All();
            Map(x => x.SKU, "SKU").CustomType("AnsiString").Length(50);
            Map(x => x.ModelNumber, "MODELNUMBER").CustomType("AnsiString").Length(50);
            References(x => x.Color, "COLORID").Cascade.SaveUpdate();
            References(x => x.Storage, "STORAGEID").Cascade.SaveUpdate();
            References(x => x.OperationSystem, "OS_TYPEID").Cascade.SaveUpdate();
            References(x => x.Brand, "BRANDID").Cascade.SaveUpdate();
            References(x => x.FrontCamera, "FRONTCAMERA_TYPEID").Cascade.SaveUpdate();
            References(x => x.BackCamera, "BACKCAMERA_TYPEID").Cascade.SaveUpdate();
            Map(x => x.ImageUrl, "IMAGE_URL").CustomType("AnsiString").Length(1024);
            HasMany(x => x.Costs).KeyColumn("SPECIFICATIONID").Cascade.All().ForeignKeyConstraintName("FK_P_SPECIFICATIONID");
        }
    }
}
