using com.etak.core.model.operation.contract.product;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.operation.IntTests.automapping.product
{
    public class ProductOfferingBasedResponse : ResponseBase, IProductOfferingBasedResponse
    {
        public ProductOffering ProductOffering { get; set; }
    }
}
