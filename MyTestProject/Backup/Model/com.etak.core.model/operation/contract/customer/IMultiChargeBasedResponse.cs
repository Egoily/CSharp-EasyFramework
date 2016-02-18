using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// Interface for a Multi Charge Based Response
    /// </summary>
    public interface IMultiChargeBasedResponse
    {
        /// <summary>
        /// A list of charges to be returned
        /// </summary>
        IEnumerable<Charge> Charges { get; set; }
    }
}
