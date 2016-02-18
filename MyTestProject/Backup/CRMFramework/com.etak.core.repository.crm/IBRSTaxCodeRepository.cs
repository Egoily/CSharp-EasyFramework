using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TBRSTaxCode"/> entity
    /// </summary>
    /// <typeparam name="TBRSTaxCode">The type of the entity managed is or extends BRSTaxCode</typeparam>
    public interface IBRSTaxCodeRepository<TBRSTaxCode> : IRepository<TBRSTaxCode, Int32> where TBRSTaxCode : BRSTaxCode
    {

    }
}
