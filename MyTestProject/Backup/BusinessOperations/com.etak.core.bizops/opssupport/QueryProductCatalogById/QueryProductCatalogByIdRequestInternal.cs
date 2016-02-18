using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.opssupport.QueryProductCatalogById
{
    /// <summary>
    /// Request Internal of QueryProductCatalogById
    /// </summary>
    public class QueryProductCatalogByIdRequestInternal : RequestBase
    {
        /// <summary>
        /// Product
        /// </summary>
        public virtual ProductOffering ProductOffering { get; set; }
    }
}
