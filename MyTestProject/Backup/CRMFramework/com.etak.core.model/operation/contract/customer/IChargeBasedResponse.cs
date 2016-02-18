using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// Interface for a Charge Based Response
    /// </summary>
    public interface IChargeBasedResponse
    {
        /// <summary>
        /// A charge (that inherites from Charge) to be returned
        /// </summary>
        Charge Charge { get; set; }
    }
}
