using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.IntTests.automapping.customer
{
    public class ChargeBasedResponseDTO : ResponseBaseDTO, IChargeCatalogBasedDTOResponse
    {

        public ChargeCatalogDTO ChargeCatalogDto { get; set; }
    }
}
