using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm.customer
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TAddressInfo"/> entity
    /// </summary>
    /// <typeparam name="TAddressInfo">The type of the entity managed is or extends AddressInfo</typeparam>
    public interface IAddressInfoRepository<TAddressInfo> : IRepository<TAddressInfo, Int32> where TAddressInfo : AddressInfo
    { 
    }
}
