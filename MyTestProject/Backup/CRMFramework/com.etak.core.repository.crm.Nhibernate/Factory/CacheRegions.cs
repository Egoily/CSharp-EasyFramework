using System;

namespace com.etak.core.repository.crm.Nhibernate.Factory
{
    /// <summary>
    /// Helper class that contains all the known NHibernate cache regions
    /// </summary>
    public class CacheRegions
    { 
        /// <summary>
        /// The cache name for User Dealer cache 
        /// </summary>
        public const String UserDealerCacheRegion = "UserDealer";
        /// <summary>
        /// The cache name for the System Settings cache region
        /// </summary>
        public const String SystemSettingsCacheRegion = "SystemSettings";

        /// <summary>
        /// The cache name for the catalog cache region
        /// </summary>
        public const String CatalogCacheRegion = "CatalogData";

        /// <summary>
        /// The cache region for 100% staic things that are persisted in the db
        /// </summary>
        public const String Constants = "Constants";
    }
}
