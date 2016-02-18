using System.Collections.Generic;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.IntTests.automapping.customer
{
    public class MultiChargeBasedResponseDTO : ResponseBaseDTO, IMultiChargeCatalogBasedDTOResponse
    {
        public IEnumerable<ChargeCatalogDTO> ChargeCatalogDtos { get; set; }
    }
}
