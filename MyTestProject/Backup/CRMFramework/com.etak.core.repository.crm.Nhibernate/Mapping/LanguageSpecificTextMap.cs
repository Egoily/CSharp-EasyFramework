using System;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for LanguageSpecificText
    /// </summary>
    public class LanguageSpecificTextMap : ClassMap<LanguageSpecificText> 
    {
        private const String TableName = "TEXT_LOOKUP_TRANSLATIONS";

        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public LanguageSpecificTextMap()
        {
            Schema("dbo");
            Table(TableName);
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();
            DynamicUpdate();

            this.CompositeId().
                KeyReference(x => x.Description, "LOOKUPID").
                KeyProperty(x => x.Language, "LANGUAGEID").CustomType<Int32>();
            Map(x => x.Text,"LOOKUPSTRING");      
        }
    }
}