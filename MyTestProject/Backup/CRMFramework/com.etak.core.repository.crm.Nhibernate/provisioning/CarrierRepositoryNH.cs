using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.provisioning;
using com.etak.core.repository.crm.provisioning;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.provisioning
{
    /// <summary>
    /// Nhibernate Implementation of ICarrierRepository
    /// </summary>
    /// <typeparam name="TCarrier"></typeparam>
    public class CarrierRepositoryNH<TCarrier> : NHibernateRepository<TCarrier, Int32>, ICarrierRepository<TCarrier> 
        where TCarrier : Carrier
    {

    }
}
