using System.Collections.Generic;
using com.etak.core.model.operation.contract.product;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.IntTests.automapping.product
{
    public class MultiProductOfferingDTOBasedResponse : ResponseBaseDTO, IMultiProductOfferingBasedDTOResponse
    {
        public IEnumerable<ProductCatalogDTO> ProductCatalogDtos { get; set; }
    }
}
