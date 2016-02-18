using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.provisioning;

namespace com.etak.core.repository.crm.provisioning
{
    /// <summary>
    /// Interface for repository of entity Carrier
    /// </summary>
    /// <typeparam name="TCarrier"></typeparam>
    public interface ICarrierRepository<TCarrier> : IRepository<TCarrier, Int32> where TCarrier: Carrier
    {
    }
}
