using System;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.opssupport.QueryProductCatalogById
{
    /// <summary>
    /// Request DTO of QueryProductCatalogById
    /// </summary>
    public class QueryProductCatalogByIdRequestDTO : RequestBaseDTO
    {
        /// <summary>
        /// ProductCatalogId requested
        /// </summary>
        public Int32 ProductCatalogId { get; set; }
    }
}
