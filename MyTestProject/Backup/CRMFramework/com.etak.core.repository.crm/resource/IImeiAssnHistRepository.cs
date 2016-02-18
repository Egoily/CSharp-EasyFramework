using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm.resource
{
    /// <summary>
    /// Repository interface for ImeiAssnHist
    /// </summary>
    /// <typeparam name="TImeiAssnHist">The type of the entity managed by the repository, is or extends ImeiAssnHist</typeparam>
    public interface IImeiAssnHistRepository<TImeiAssnHist> : IRepository<TImeiAssnHist, ImeiAssnHistPKInfo>
        where TImeiAssnHist : ImeiAssnHist
    {
       
    }
}
