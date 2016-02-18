using System;
using System.Collections.Generic;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryTransferences
{
    /// <summary>
    /// The response DTO for QueryTransferences
    /// </summary>
    public class QueryTransferencesResponseDTO : ResponseBaseDTO
    {
        /// <summary>
        /// All the Ok transferences for the customer, within a rime range and a type
        /// </summary>
        public IEnumerable<TransferenceExecutionDTO> Transferences { get; set; }
    }

    
}
