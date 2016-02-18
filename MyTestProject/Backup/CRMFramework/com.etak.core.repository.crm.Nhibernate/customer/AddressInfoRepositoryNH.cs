using System;
using com.etak.core.model;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity AddressInfo 
    /// </summary>
    /// <typeparam name="TAddressInfo">Entity managed by the repository, is or extends AddressInfo</typeparam>
    public class AddressInfoRepositoryNH<TAddressInfo> :
        NHibernateRepository<TAddressInfo, Int32>, IAddressInfoRepository<TAddressInfo> where  TAddressInfo : AddressInfo
    {
    }
}
