using com.etak.core.model.operation.contract.product;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.test.automapping.product
{
    public class ProductBasedResponse : ResponseBase, IProductBasedResponse
    {
        public Product Product { get; set; }
    }
}
