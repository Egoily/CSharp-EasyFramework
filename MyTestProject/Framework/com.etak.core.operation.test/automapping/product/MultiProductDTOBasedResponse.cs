using System.Collections.Generic;
using com.etak.core.model.operation.contract.product;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.test.automapping.product
{
    public class MultiProductDTOBasedResponse : ResponseBaseDTO, IMultiProductBasedDTOResponse
    {
        public IEnumerable<ProductCatalogDTO> ProductCatalogDtos { get; set; }
    }
}
