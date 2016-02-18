using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TBRSTaxCodePostCode"/> entity
    /// </summary>
    /// <typeparam name="TBRSTaxCodePostCode">The type of the entity managed is or extends BRSTaxCodePostCode</typeparam>
    public interface IBRSTaxCodePostCodeRepository<TBRSTaxCodePostCode> : IRepository<TBRSTaxCodePostCode, Int32> where TBRSTaxCodePostCode : BRSTaxCodePostCode
    {
        /// <summary>
        /// Gets the BRSTaxCodePostCode by the post code
        /// </summary>
        /// <param name="p">the post code of the BRSTaxCodePostCode</param>
        /// <returns>the BRSTaxCodePostCode found for the post code</returns>
        TBRSTaxCodePostCode GetByPostCode(string p);
    }
}
