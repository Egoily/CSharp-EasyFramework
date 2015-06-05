using com.etak.core.model.operation.contract.product;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.test.automapping.product
{
    public class ProductDTOBasedResponse : ResponseBaseDTO, IProductBasedDTOResponse
    {
        public ProductCatalogDTO ProductCatalog { get; set; }
    }
}
