using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.opssupport.QueryProductCatalogById
{
    /// <summary>
    /// Response Internal of QueryProductCatalogById
    /// </summary>
    public class QueryProductCatalogByIdResponseInternal : ResponseBase
    {
        /// <summary>
        /// Product that will be converted to CustomerProductCatalogDTO
        /// </summary>
        public virtual ProductOffering ProductOffering { get; set; }
    }
}
